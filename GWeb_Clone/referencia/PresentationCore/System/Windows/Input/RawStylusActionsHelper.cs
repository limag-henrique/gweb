using System;

namespace System.Windows.Input
{
	// Token: 0x020002A8 RID: 680
	internal static class RawStylusActionsHelper
	{
		// Token: 0x060013D9 RID: 5081 RVA: 0x0004A340 File Offset: 0x00049740
		internal static bool IsValid(RawStylusActions action)
		{
			return action >= RawStylusActions.None && action <= RawStylusActionsHelper.MaxActions;
		}

		// Token: 0x04000ACB RID: 2763
		private static readonly RawStylusActions MaxActions = RawStylusActions.Activate | RawStylusActions.Deactivate | RawStylusActions.Down | RawStylusActions.Up | RawStylusActions.Move | RawStylusActions.InAirMove | RawStylusActions.InRange | RawStylusActions.OutOfRange | RawStylusActions.SystemGesture;
	}
}
