using System;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal;

namespace System.Windows.Media
{
	// Token: 0x0200038E RID: 910
	internal class EventProxyWrapper
	{
		// Token: 0x060021BC RID: 8636 RVA: 0x00088948 File Offset: 0x00087D48
		private EventProxyWrapper(IInvokable invokable)
		{
			this.target = new WeakReference(invokable);
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x00088968 File Offset: 0x00087D68
		private void Verify()
		{
			if (this.target == null)
			{
				throw new ObjectDisposedException("EventProxyWrapper");
			}
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x00088988 File Offset: 0x00087D88
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public int RaiseEvent(byte[] buffer, uint cb)
		{
			try
			{
				this.Verify();
				IInvokable invokable = (IInvokable)this.target.Target;
				if (invokable == null)
				{
					return -2147024890;
				}
				invokable.RaiseEvent(buffer, (int)cb);
			}
			catch (Exception e)
			{
				return Marshal.GetHRForException(e);
			}
			return 0;
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x000889EC File Offset: 0x00087DEC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static EventProxyWrapper FromEPD(ref EventProxyDescriptor epd)
		{
			GCHandle handle = epd.m_handle;
			return (EventProxyWrapper)handle.Target;
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x00088A0C File Offset: 0x00087E0C
		internal static int RaiseEvent(ref EventProxyDescriptor pEPD, byte[] buffer, uint cb)
		{
			EventProxyWrapper eventProxyWrapper = EventProxyWrapper.FromEPD(ref pEPD);
			if (eventProxyWrapper != null)
			{
				return eventProxyWrapper.RaiseEvent(buffer, cb);
			}
			return -2147024890;
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x00088A34 File Offset: 0x00087E34
		[SecurityCritical]
		internal static SafeMILHandle CreateEventProxyWrapper(IInvokable invokable)
		{
			if (invokable == null)
			{
				throw new ArgumentNullException("invokable");
			}
			SafeMILHandle result = null;
			EventProxyWrapper value = new EventProxyWrapper(invokable);
			EventProxyDescriptor eventProxyDescriptor = default(EventProxyDescriptor);
			eventProxyDescriptor.pfnDispose = EventProxyStaticPtrs.pfnDispose;
			eventProxyDescriptor.pfnRaiseEvent = EventProxyStaticPtrs.pfnRaiseEvent;
			eventProxyDescriptor.m_handle = GCHandle.Alloc(value, GCHandleType.Normal);
			HRESULT.Check(EventProxyWrapper.MILCreateEventProxy(ref eventProxyDescriptor, out result));
			return result;
		}

		// Token: 0x060021C2 RID: 8642
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("wpfgfx_v0400.dll")]
		private static extern int MILCreateEventProxy(ref EventProxyDescriptor pEPD, out SafeMILHandle ppEventProxy);

		// Token: 0x040010CF RID: 4303
		private WeakReference target;
	}
}
