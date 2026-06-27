using System;
using System.Collections.Generic;

namespace MS.Internal
{
	// Token: 0x0200066F RID: 1647
	internal static class AppDomainShutdownMonitor
	{
		// Token: 0x060048B3 RID: 18611 RVA: 0x0011C314 File Offset: 0x0011B714
		static AppDomainShutdownMonitor()
		{
			AppDomain.CurrentDomain.DomainUnload += AppDomainShutdownMonitor.OnShutdown;
			AppDomain.CurrentDomain.ProcessExit += AppDomainShutdownMonitor.OnShutdown;
			AppDomainShutdownMonitor._dictionary = new Dictionary<WeakReference, WeakReference>();
		}

		// Token: 0x060048B4 RID: 18612 RVA: 0x0011C358 File Offset: 0x0011B758
		public static void Add(WeakReference listener)
		{
			Dictionary<WeakReference, WeakReference> dictionary = AppDomainShutdownMonitor._dictionary;
			lock (dictionary)
			{
				if (!AppDomainShutdownMonitor._shuttingDown)
				{
					AppDomainShutdownMonitor._dictionary.Add(listener, listener);
				}
			}
		}

		// Token: 0x060048B5 RID: 18613 RVA: 0x0011C3B0 File Offset: 0x0011B7B0
		public static void Remove(WeakReference listener)
		{
			Dictionary<WeakReference, WeakReference> dictionary = AppDomainShutdownMonitor._dictionary;
			lock (dictionary)
			{
				if (!AppDomainShutdownMonitor._shuttingDown)
				{
					AppDomainShutdownMonitor._dictionary.Remove(listener);
				}
			}
		}

		// Token: 0x060048B6 RID: 18614 RVA: 0x0011C408 File Offset: 0x0011B808
		private static void OnShutdown(object sender, EventArgs e)
		{
			Dictionary<WeakReference, WeakReference> dictionary = AppDomainShutdownMonitor._dictionary;
			lock (dictionary)
			{
				AppDomainShutdownMonitor._shuttingDown = true;
			}
			foreach (WeakReference weakReference in AppDomainShutdownMonitor._dictionary.Values)
			{
				IAppDomainShutdownListener appDomainShutdownListener = weakReference.Target as IAppDomainShutdownListener;
				if (appDomainShutdownListener != null)
				{
					appDomainShutdownListener.NotifyShutdown();
				}
			}
		}

		// Token: 0x04001C9E RID: 7326
		private static Dictionary<WeakReference, WeakReference> _dictionary;

		// Token: 0x04001C9F RID: 7327
		private static bool _shuttingDown;
	}
}
