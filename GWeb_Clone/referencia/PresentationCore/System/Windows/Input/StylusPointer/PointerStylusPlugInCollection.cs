using System;
using System.Security;
using System.Windows.Input.StylusPlugIns;

namespace System.Windows.Input.StylusPointer
{
	// Token: 0x020002ED RID: 749
	internal class PointerStylusPlugInCollection : StylusPlugInCollectionBase
	{
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x0005E620 File Offset: 0x0005DA20
		internal override bool IsActiveForInput
		{
			[SecuritySafeCritical]
			get
			{
				return this._manager != null;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x0005E638 File Offset: 0x0005DA38
		internal override object SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0005E648 File Offset: 0x0005DA48
		[SecuritySafeCritical]
		internal override void UpdateState(UIElement element)
		{
			bool flag = true;
			if (element.IsVisible && element.IsEnabled && element.IsHitTestVisible)
			{
				PresentationSource presentationSource = PresentationSource.CriticalFromVisual(element);
				if (presentationSource != null)
				{
					flag = false;
					if (this._manager == null)
					{
						this._manager = StylusLogic.GetCurrentStylusLogicAs<PointerLogic>().PlugInManagers[presentationSource];
						if (this._manager != null)
						{
							this._manager.AddStylusPlugInCollection(base.Wrapper);
							foreach (StylusPlugIn stylusPlugIn in base.Wrapper)
							{
								stylusPlugIn.InvalidateIsActiveForInput();
							}
							base.Wrapper.OnLayoutUpdated(base.Wrapper, EventArgs.Empty);
						}
					}
				}
			}
			if (flag)
			{
				this.Unhook();
			}
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0005E72C File Offset: 0x0005DB2C
		[SecuritySafeCritical]
		internal override void Unhook()
		{
			if (this._manager != null)
			{
				this._manager.RemoveStylusPlugInCollection(base.Wrapper);
				this._manager = null;
				foreach (StylusPlugIn stylusPlugIn in base.Wrapper)
				{
					stylusPlugIn.InvalidateIsActiveForInput();
				}
			}
		}

		// Token: 0x04000D02 RID: 3330
		[SecurityCritical]
		private PointerStylusPlugInManager _manager;
	}
}
