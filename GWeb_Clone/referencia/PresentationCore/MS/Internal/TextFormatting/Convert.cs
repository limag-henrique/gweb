using System;
using System.Windows;
using System.Windows.Media.TextFormatting;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000750 RID: 1872
	internal sealed class Convert
	{
		// Token: 0x06004E13 RID: 19987 RVA: 0x001336DC File Offset: 0x00132ADC
		private Convert()
		{
		}

		// Token: 0x06004E14 RID: 19988 RVA: 0x001336F0 File Offset: 0x00132AF0
		public static FlowDirection LsTFlowToFlowDirection(LsTFlow lstflow)
		{
			switch (lstflow)
			{
			case LsTFlow.lstflowDefault:
			case LsTFlow.lstflowEN:
				return FlowDirection.LeftToRight;
			case LsTFlow.lstflowWS:
			case LsTFlow.lstflowWN:
				return FlowDirection.RightToLeft;
			}
			return FlowDirection.LeftToRight;
		}

		// Token: 0x06004E15 RID: 19989 RVA: 0x0013372C File Offset: 0x00132B2C
		public static LsKTab LsKTabFromTabAlignment(TextTabAlignment tabAlignment)
		{
			switch (tabAlignment)
			{
			case TextTabAlignment.Center:
				return LsKTab.lsktCenter;
			case TextTabAlignment.Right:
				return LsKTab.lsktRight;
			case TextTabAlignment.Character:
				return LsKTab.lsktChar;
			default:
				return LsKTab.lsktLeft;
			}
		}
	}
}
