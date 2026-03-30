using System;
using System.Security;
using System.Windows.Input.StylusPlugIns;

namespace System.Windows.Input.StylusWisp
{
	// Token: 0x020002E5 RID: 741
	internal class WispStylusPlugInCollection : StylusPlugInCollectionBase
	{
		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x0600170E RID: 5902 RVA: 0x00059CC8 File Offset: 0x000590C8
		internal override bool IsActiveForInput
		{
			[SecuritySafeCritical]
			get
			{
				return this._penContexts != null;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x00059CE0 File Offset: 0x000590E0
		internal override object SyncRoot
		{
			[SecuritySafeCritical]
			get
			{
				if (this._penContexts == null)
				{
					return null;
				}
				return this._penContexts.SyncRoot;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x00059D04 File Offset: 0x00059104
		internal PenContexts PenContexts
		{
			[SecurityCritical]
			get
			{
				return this._penContexts;
			}
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x00059D18 File Offset: 0x00059118
		[SecuritySafeCritical]
		internal override void UpdateState(UIElement element)
		{
			bool flag = true;
			using (element.Dispatcher.DisableProcessing())
			{
				if (element.IsVisible && element.IsEnabled && element.IsHitTestVisible)
				{
					PresentationSource presentationSource = PresentationSource.CriticalFromVisual(element);
					if (presentationSource != null)
					{
						flag = false;
						if (this._penContexts == null)
						{
							InputManager inputManager = (InputManager)element.Dispatcher.InputManager;
							PenContexts penContextsFromHwnd = StylusLogic.GetCurrentStylusLogicAs<WispLogic>().GetPenContextsFromHwnd(presentationSource);
							if (penContextsFromHwnd != null)
							{
								this._penContexts = penContextsFromHwnd;
								object syncRoot = penContextsFromHwnd.SyncRoot;
								lock (syncRoot)
								{
									penContextsFromHwnd.AddStylusPlugInCollection(base.Wrapper);
									foreach (StylusPlugIn stylusPlugIn in base.Wrapper)
									{
										stylusPlugIn.InvalidateIsActiveForInput();
									}
									base.Wrapper.OnLayoutUpdated(base.Wrapper, EventArgs.Empty);
								}
							}
						}
					}
				}
				if (flag)
				{
					this.Unhook();
				}
			}
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x00059E74 File Offset: 0x00059274
		[SecuritySafeCritical]
		internal override void Unhook()
		{
			if (this._penContexts != null)
			{
				object syncRoot = this._penContexts.SyncRoot;
				lock (syncRoot)
				{
					this._penContexts.RemoveStylusPlugInCollection(base.Wrapper);
					this._penContexts = null;
					foreach (StylusPlugIn stylusPlugIn in base.Wrapper)
					{
						stylusPlugIn.InvalidateIsActiveForInput();
					}
				}
			}
		}

		// Token: 0x04000C8B RID: 3211
		[SecurityCritical]
		private PenContexts _penContexts;
	}
}
