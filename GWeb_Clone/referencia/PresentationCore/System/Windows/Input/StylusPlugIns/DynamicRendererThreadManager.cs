using System;
using System.ComponentModel;
using System.Security;
using System.Threading;
using System.Windows.Threading;

namespace System.Windows.Input.StylusPlugIns
{
	// Token: 0x020002F6 RID: 758
	internal class DynamicRendererThreadManager : IWeakEventListener, IDisposable
	{
		// Token: 0x06001821 RID: 6177 RVA: 0x00061118 File Offset: 0x00060518
		internal static DynamicRendererThreadManager GetCurrentThreadInstance()
		{
			if (DynamicRendererThreadManager._tsDRTMWeakRef == null || DynamicRendererThreadManager._tsDRTMWeakRef.Target == null)
			{
				DynamicRendererThreadManager._tsDRTMWeakRef = new WeakReference(new DynamicRendererThreadManager());
			}
			return DynamicRendererThreadManager._tsDRTMWeakRef.Target as DynamicRendererThreadManager;
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x00061158 File Offset: 0x00060558
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private DynamicRendererThreadManager()
		{
			DynamicRendererThreadManager.DynamicRendererThreadManagerWorker dynamicRendererThreadManagerWorker = new DynamicRendererThreadManager.DynamicRendererThreadManagerWorker();
			this.__inkingDispatcher = dynamicRendererThreadManagerWorker.StartUpAndReturnDispatcher();
			DispatcherShutdownStartedEventManager.AddListener(Dispatcher.CurrentDispatcher, this);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0006118C File Offset: 0x0006058C
		~DynamicRendererThreadManager()
		{
			this.Dispose(false);
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x000611C8 File Offset: 0x000605C8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x000611E4 File Offset: 0x000605E4
		bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs args)
		{
			if (managerType == typeof(DispatcherShutdownStartedEventManager))
			{
				this.OnAppDispatcherShutdown(sender, args);
				return true;
			}
			return false;
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00061210 File Offset: 0x00060610
		private void OnAppDispatcherShutdown(object sender, EventArgs e)
		{
			Dispatcher _inkingDispatcher = this.__inkingDispatcher;
			if (_inkingDispatcher == null)
			{
				return;
			}
			_inkingDispatcher.Invoke(DispatcherPriority.Send, new DispatcherOperationCallback(delegate(object unused)
			{
				this.Dispose();
				return null;
			}), null);
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00061240 File Offset: 0x00060640
		[SecuritySafeCritical]
		private void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				this._disposed = true;
				if (this.__inkingDispatcher != null && !Environment.HasShutdownStarted)
				{
					try
					{
						this.__inkingDispatcher.CriticalInvokeShutdown();
					}
					catch (Win32Exception ex)
					{
						int nativeErrorCode = ex.NativeErrorCode;
					}
					finally
					{
						this.__inkingDispatcher = null;
					}
				}
			}
			GC.KeepAlive(this);
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x000612D4 File Offset: 0x000606D4
		internal Dispatcher ThreadDispatcher
		{
			get
			{
				return this.__inkingDispatcher;
			}
		}

		// Token: 0x04000D27 RID: 3367
		[ThreadStatic]
		private static WeakReference _tsDRTMWeakRef;

		// Token: 0x04000D28 RID: 3368
		private volatile Dispatcher __inkingDispatcher;

		// Token: 0x04000D29 RID: 3369
		private bool _disposed;

		// Token: 0x0200082F RID: 2095
		private class DynamicRendererThreadManagerWorker
		{
			// Token: 0x06005686 RID: 22150 RVA: 0x00163064 File Offset: 0x00162464
			internal DynamicRendererThreadManagerWorker()
			{
			}

			// Token: 0x06005687 RID: 22151 RVA: 0x00163078 File Offset: 0x00162478
			[SecurityCritical]
			internal Dispatcher StartUpAndReturnDispatcher()
			{
				this._startupCompleted = new AutoResetEvent(false);
				Thread thread = new Thread(new ThreadStart(this.InkingThreadProc));
				thread.SetApartmentState(ApartmentState.STA);
				thread.IsBackground = true;
				thread.Start();
				this._startupCompleted.WaitOne();
				this._startupCompleted.Close();
				this._startupCompleted = null;
				return this._dispatcher;
			}

			// Token: 0x06005688 RID: 22152 RVA: 0x001630DC File Offset: 0x001624DC
			[SecurityCritical]
			public void InkingThreadProc()
			{
				Thread.CurrentThread.Name = "DynamicRenderer";
				this._dispatcher = Dispatcher.CurrentDispatcher;
				this._startupCompleted.Set();
				Dispatcher.Run();
			}

			// Token: 0x040027B6 RID: 10166
			private Dispatcher _dispatcher;

			// Token: 0x040027B7 RID: 10167
			private AutoResetEvent _startupCompleted;
		}
	}
}
