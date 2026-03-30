using System;
using System.Security;
using System.Windows.Threading;

namespace System.Windows.Input.StylusPlugIns
{
	// Token: 0x020002F5 RID: 757
	internal sealed class DispatcherShutdownStartedEventManager : WeakEventManager
	{
		// Token: 0x0600181B RID: 6171 RVA: 0x0006102C File Offset: 0x0006042C
		private DispatcherShutdownStartedEventManager()
		{
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00061040 File Offset: 0x00060440
		[SecurityCritical]
		public static void AddListener(Dispatcher source, IWeakEventListener listener)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (listener == null)
			{
				throw new ArgumentNullException("listener");
			}
			DispatcherShutdownStartedEventManager.CurrentManager.ProtectedAddListener(source, listener);
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00061078 File Offset: 0x00060478
		protected override void StartListening(object source)
		{
			Dispatcher dispatcher = (Dispatcher)source;
			dispatcher.ShutdownStarted += this.OnShutdownStarted;
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x000610A0 File Offset: 0x000604A0
		protected override void StopListening(object source)
		{
			Dispatcher dispatcher = (Dispatcher)source;
			dispatcher.ShutdownStarted -= this.OnShutdownStarted;
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x000610C8 File Offset: 0x000604C8
		private static DispatcherShutdownStartedEventManager CurrentManager
		{
			[SecurityCritical]
			get
			{
				Type typeFromHandle = typeof(DispatcherShutdownStartedEventManager);
				DispatcherShutdownStartedEventManager dispatcherShutdownStartedEventManager = (DispatcherShutdownStartedEventManager)WeakEventManager.GetCurrentManager(typeFromHandle);
				if (dispatcherShutdownStartedEventManager == null)
				{
					dispatcherShutdownStartedEventManager = new DispatcherShutdownStartedEventManager();
					WeakEventManager.SetCurrentManager(typeFromHandle, dispatcherShutdownStartedEventManager);
				}
				return dispatcherShutdownStartedEventManager;
			}
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x00061100 File Offset: 0x00060500
		private void OnShutdownStarted(object sender, EventArgs args)
		{
			base.DeliverEvent(sender, args);
		}
	}
}
