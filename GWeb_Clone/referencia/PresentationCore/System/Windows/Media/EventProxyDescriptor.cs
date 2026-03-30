using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Windows.Media
{
	// Token: 0x0200038C RID: 908
	internal struct EventProxyDescriptor
	{
		// Token: 0x060021BA RID: 8634 RVA: 0x000888EC File Offset: 0x00087CEC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static void StaticDispose(ref EventProxyDescriptor pEPD)
		{
			EventProxyWrapper eventProxyWrapper = (EventProxyWrapper)pEPD.m_handle.Target;
			GCHandle handle = pEPD.m_handle;
			handle.Free();
		}

		// Token: 0x040010CA RID: 4298
		internal EventProxyDescriptor.Dispose pfnDispose;

		// Token: 0x040010CB RID: 4299
		internal EventProxyDescriptor.RaiseEvent pfnRaiseEvent;

		// Token: 0x040010CC RID: 4300
		internal GCHandle m_handle;

		// Token: 0x02000867 RID: 2151
		// (Invoke) Token: 0x06005740 RID: 22336
		internal delegate void Dispose(ref EventProxyDescriptor pEPD);

		// Token: 0x02000868 RID: 2152
		// (Invoke) Token: 0x06005744 RID: 22340
		internal delegate int RaiseEvent(ref EventProxyDescriptor pEPD, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer, uint cb);
	}
}
