using System;

namespace System.Windows.Input
{
	// Token: 0x0200029F RID: 671
	internal class TouchesOverProperty : ReverseInheritProperty
	{
		// Token: 0x060013B5 RID: 5045 RVA: 0x00049D8C File Offset: 0x0004918C
		internal TouchesOverProperty() : base(UIElement.AreAnyTouchesOverPropertyKey, CoreFlags.TouchesOverCache, CoreFlags.TouchesOverChanged, CoreFlags.TouchLeaveCache, ~(CoreFlags.SnapsToDevicePixelsCache | CoreFlags.ClipToBoundsCache | CoreFlags.MeasureDirty | CoreFlags.ArrangeDirty | CoreFlags.MeasureInProgress | CoreFlags.ArrangeInProgress | CoreFlags.NeverMeasured | CoreFlags.NeverArranged | CoreFlags.MeasureDuringArrange | CoreFlags.IsCollapsed | CoreFlags.IsKeyboardFocusWithinCache | CoreFlags.IsKeyboardFocusWithinChanged | CoreFlags.IsMouseOverCache | CoreFlags.IsMouseOverChanged | CoreFlags.IsMouseCaptureWithinCache | CoreFlags.IsMouseCaptureWithinChanged | CoreFlags.IsStylusOverCache | CoreFlags.IsStylusOverChanged | CoreFlags.IsStylusCaptureWithinCache | CoreFlags.IsStylusCaptureWithinChanged | CoreFlags.HasAutomationPeer | CoreFlags.RenderingInvalidated | CoreFlags.IsVisibleCache | CoreFlags.AreTransformsClean | CoreFlags.IsOpacitySuppressed | CoreFlags.ExistsEventHandlersStore | CoreFlags.TouchesOverCache | CoreFlags.TouchesOverChanged | CoreFlags.TouchesCapturedWithinCache | CoreFlags.TouchesCapturedWithinChanged | CoreFlags.TouchLeaveCache))
		{
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00049DB8 File Offset: 0x000491B8
		internal override void FireNotifications(UIElement uie, ContentElement ce, UIElement3D uie3D, bool oldValue)
		{
		}
	}
}
