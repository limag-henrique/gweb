using System;

namespace System.Windows.Input
{
	// Token: 0x0200029E RID: 670
	internal class TouchesCapturedWithinProperty : ReverseInheritProperty
	{
		// Token: 0x060013B3 RID: 5043 RVA: 0x00049D58 File Offset: 0x00049158
		internal TouchesCapturedWithinProperty() : base(UIElement.AreAnyTouchesCapturedWithinPropertyKey, CoreFlags.TouchesCapturedWithinCache, CoreFlags.TouchesCapturedWithinChanged)
		{
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x00049D7C File Offset: 0x0004917C
		internal override void FireNotifications(UIElement uie, ContentElement ce, UIElement3D uie3D, bool oldValue)
		{
		}
	}
}
