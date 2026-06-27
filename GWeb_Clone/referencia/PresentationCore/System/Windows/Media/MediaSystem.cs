using System;
using System.Collections;
using System.Security;
using System.Threading;
using System.Windows.Media.Composition;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.Composition;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media
{
	// Token: 0x02000428 RID: 1064
	internal static class MediaSystem
	{
		// Token: 0x06002BA1 RID: 11169 RVA: 0x000ADE98 File Offset: 0x000AD298
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public static bool Startup(MediaContext mc)
		{
			HRESULT.Check(UnsafeNativeMethods.MilCoreApi.MilVersionCheck(MS.Internal.Composition.Version.MilSdkVersion));
			using (CompositionEngineLock.Acquire())
			{
				MediaSystem._mediaContexts.Add(mc);
				if (MediaSystem.s_refCount == 0)
				{
					HRESULT.Check(SafeNativeMethods.MilCompositionEngine_InitializePartitionManager(0));
					MediaSystem.s_forceSoftareForGraphicsStreamMagnifier = UnsafeNativeMethods.MilCoreApi.WgxConnection_ShouldForceSoftwareForGraphicsStreamClient();
					MediaSystem.ConnectTransport();
					MediaSystem.ReadAnimationSmoothingSetting();
				}
				MediaSystem.s_refCount++;
			}
			return true;
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000ADF28 File Offset: 0x000AD328
		internal static bool ConnectChannels(MediaContext mc)
		{
			bool result = false;
			using (CompositionEngineLock.Acquire())
			{
				if (MediaSystem.IsTransportConnected)
				{
					mc.CreateChannels();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x000ADF7C File Offset: 0x000AD37C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private static void ReadAnimationSmoothingSetting()
		{
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x000ADF8C File Offset: 0x000AD38C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static void Shutdown(MediaContext mc)
		{
			using (CompositionEngineLock.Acquire())
			{
				MediaSystem._mediaContexts.Remove(mc);
				MediaSystem.s_refCount--;
				if (MediaSystem.s_refCount == 0)
				{
					if (MediaSystem.IsTransportConnected)
					{
						MediaSystem.DisconnectTransport();
					}
					HRESULT.Check(SafeNativeMethods.MilCompositionEngine_DeinitializePartitionManager());
				}
			}
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x000AE000 File Offset: 0x000AD400
		internal static void PropagateDirtyRectangleSettings()
		{
			int num = MediaSystem.s_DisableDirtyRectangles;
			int num2 = CoreAppContextSwitches.DisableDirtyRectangles ? 1 : 0;
			if (num2 != num && Interlocked.CompareExchange(ref MediaSystem.s_DisableDirtyRectangles, num2, num) == num)
			{
				MediaSystem.NotifyRedirectionEnvironmentChanged();
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06002BA6 RID: 11174 RVA: 0x000AE038 File Offset: 0x000AD438
		internal static bool DisableDirtyRectangles
		{
			get
			{
				return MediaSystem.s_DisableDirtyRectangles != 0;
			}
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x000AE050 File Offset: 0x000AD450
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static void NotifyRedirectionEnvironmentChanged()
		{
			using (CompositionEngineLock.Acquire())
			{
				MediaSystem.s_forceSoftareForGraphicsStreamMagnifier = UnsafeNativeMethods.MilCoreApi.WgxConnection_ShouldForceSoftwareForGraphicsStreamClient();
				foreach (object obj in MediaSystem._mediaContexts)
				{
					MediaContext mediaContext = (MediaContext)obj;
					mediaContext.PostInvalidateRenderMode();
				}
			}
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000AE0EC File Offset: 0x000AD4EC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private static void ConnectTransport()
		{
			if (MediaSystem.IsTransportConnected)
			{
				throw new InvalidOperationException(SR.Get("MediaSystem_OutOfOrderConnectOrDisconnect"));
			}
			HRESULT.Check(UnsafeNativeMethods.MilCoreApi.WgxConnection_Create(false, out MediaSystem.s_pConnection));
			MediaSystem.s_serviceChannel = new DUCE.Channel(null, false, MediaSystem.s_pConnection, false);
			MediaSystem.IsTransportConnected = true;
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x000AE138 File Offset: 0x000AD538
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private static void DisconnectTransport()
		{
			if (!MediaSystem.IsTransportConnected)
			{
				return;
			}
			MediaSystem.s_serviceChannel.Close();
			HRESULT.Check(UnsafeNativeMethods.MilCoreApi.WgxConnection_Disconnect(MediaSystem.s_pConnection));
			MediaSystem.s_serviceChannel = null;
			MediaSystem.s_pConnection = IntPtr.Zero;
			MediaSystem.IsTransportConnected = false;
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x000AE17C File Offset: 0x000AD57C
		internal static void AssertSameContext(DispatcherObject reference, DispatcherObject other)
		{
			if (other != null && reference.Dispatcher != null && other.Dispatcher != null && reference.Dispatcher != other.Dispatcher)
			{
				throw new ArgumentException(SR.Get("MediaSystem_ApiInvalidContext"));
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06002BAB RID: 11179 RVA: 0x000AE1BC File Offset: 0x000AD5BC
		// (set) Token: 0x06002BAC RID: 11180 RVA: 0x000AE1D0 File Offset: 0x000AD5D0
		internal static bool IsTransportConnected
		{
			get
			{
				return MediaSystem.s_isConnected;
			}
			set
			{
				MediaSystem.s_isConnected = value;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06002BAD RID: 11181 RVA: 0x000AE1E4 File Offset: 0x000AD5E4
		internal static bool ForceSoftwareRendering
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				bool result;
				using (CompositionEngineLock.Acquire())
				{
					result = MediaSystem.s_forceSoftareForGraphicsStreamMagnifier;
				}
				return result;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002BAE RID: 11182 RVA: 0x000AE22C File Offset: 0x000AD62C
		internal static DUCE.Channel ServiceChannel
		{
			[SecurityCritical]
			get
			{
				return MediaSystem.s_serviceChannel;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06002BAF RID: 11183 RVA: 0x000AE240 File Offset: 0x000AD640
		internal static IntPtr Connection
		{
			[SecurityCritical]
			get
			{
				return MediaSystem.s_pConnection;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002BB0 RID: 11184 RVA: 0x000AE254 File Offset: 0x000AD654
		internal static bool AnimationSmoothing
		{
			get
			{
				return MediaSystem.s_animationSmoothing;
			}
		}

		// Token: 0x040013D8 RID: 5080
		private static int s_refCount = 0;

		// Token: 0x040013D9 RID: 5081
		private static ArrayList _mediaContexts = new ArrayList();

		// Token: 0x040013DA RID: 5082
		private static bool s_isConnected = false;

		// Token: 0x040013DB RID: 5083
		[SecurityCritical]
		private static DUCE.Channel s_serviceChannel;

		// Token: 0x040013DC RID: 5084
		private static bool s_animationSmoothing = true;

		// Token: 0x040013DD RID: 5085
		[SecurityCritical]
		private static IntPtr s_pConnection;

		// Token: 0x040013DE RID: 5086
		[SecurityCritical]
		private static bool s_forceSoftareForGraphicsStreamMagnifier;

		// Token: 0x040013DF RID: 5087
		private static int s_DisableDirtyRectangles = 0;
	}
}
