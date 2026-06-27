using System;
using System.Security;
using System.Security.Permissions;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	// Token: 0x0200025B RID: 603
	internal class InputProviderSite : IDisposable
	{
		// Token: 0x060010FF RID: 4351 RVA: 0x000402E4 File Offset: 0x0003F6E4
		[SecurityCritical]
		internal InputProviderSite(InputManager inputManager, IInputProvider inputProvider)
		{
			this._inputManager = new SecurityCriticalDataClass<InputManager>(inputManager);
			this._inputProvider = new SecurityCriticalDataClass<IInputProvider>(inputProvider);
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x00040310 File Offset: 0x0003F710
		public InputManager InputManager
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandUnrestrictedUIPermission();
				return this.CriticalInputManager;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x00040328 File Offset: 0x0003F728
		internal InputManager CriticalInputManager
		{
			[SecurityCritical]
			get
			{
				return this._inputManager.Value;
			}
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00040340 File Offset: 0x0003F740
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			if (!this._isDisposed)
			{
				this._isDisposed = true;
				if (this._inputManager != null && this._inputProvider != null)
				{
					this._inputManager.Value.UnregisterInputProvider(this._inputProvider.Value);
				}
				this._inputManager = null;
				this._inputProvider = null;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x0004039C File Offset: 0x0003F79C
		public bool IsDisposed
		{
			get
			{
				return this._isDisposed;
			}
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x000403B0 File Offset: 0x0003F7B0
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public bool ReportInput(InputReport inputReport)
		{
			if (this.IsDisposed)
			{
				throw new ObjectDisposedException(SR.Get("InputProviderSiteDisposed"));
			}
			bool result = false;
			InputReportEventArgs inputReportEventArgs = new InputReportEventArgs(null, inputReport);
			inputReportEventArgs.RoutedEvent = InputManager.PreviewInputReportEvent;
			if (this._inputManager != null)
			{
				result = this._inputManager.Value.ProcessInput(inputReportEventArgs);
			}
			return result;
		}

		// Token: 0x04000932 RID: 2354
		private bool _isDisposed;

		// Token: 0x04000933 RID: 2355
		private SecurityCriticalDataClass<InputManager> _inputManager;

		// Token: 0x04000934 RID: 2356
		private SecurityCriticalDataClass<IInputProvider> _inputProvider;
	}
}
