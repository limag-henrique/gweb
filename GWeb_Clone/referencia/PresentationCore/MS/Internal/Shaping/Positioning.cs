using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006D4 RID: 1748
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal static class Positioning
	{
		// Token: 0x06004BB0 RID: 19376 RVA: 0x00128270 File Offset: 0x00127670
		public static int DesignToPixels(ushort DesignUnitsPerEm, ushort PixelsPerEm, int Value)
		{
			if (DesignUnitsPerEm == 0)
			{
				return Value;
			}
			int num = (int)(DesignUnitsPerEm / 2);
			if (Value >= 0)
			{
				num = (int)(DesignUnitsPerEm / 2);
			}
			else
			{
				num = -(DesignUnitsPerEm >> 1) + 1;
			}
			return (Value * (int)PixelsPerEm + num) / (int)DesignUnitsPerEm;
		}

		// Token: 0x06004BB1 RID: 19377 RVA: 0x001282A0 File Offset: 0x001276A0
		public unsafe static void AlignAnchors(IOpenTypeFont Font, FontTable Table, LayoutMetrics Metrics, GlyphInfoList GlyphInfo, int* Advances, LayoutOffset* Offsets, int StaticGlyph, int MobileGlyph, AnchorTable StaticAnchor, AnchorTable MobileAnchor, bool UseAdvances)
		{
			Invariant.Assert(StaticGlyph >= 0 && StaticGlyph < GlyphInfo.Length);
			Invariant.Assert(MobileGlyph >= 0 && MobileGlyph < GlyphInfo.Length);
			Invariant.Assert(!StaticAnchor.IsNull());
			Invariant.Assert(!MobileAnchor.IsNull());
			LayoutOffset contourPoint = default(LayoutOffset);
			if (StaticAnchor.NeedContourPoint(Table))
			{
				contourPoint = Font.GetGlyphPointCoord(GlyphInfo.Glyphs[MobileGlyph], StaticAnchor.ContourPointIndex(Table));
			}
			LayoutOffset layoutOffset = StaticAnchor.AnchorCoordinates(Table, Metrics, contourPoint);
			if (MobileAnchor.NeedContourPoint(Table))
			{
				contourPoint = Font.GetGlyphPointCoord(GlyphInfo.Glyphs[MobileGlyph], MobileAnchor.ContourPointIndex(Table));
			}
			LayoutOffset layoutOffset2 = MobileAnchor.AnchorCoordinates(Table, Metrics, contourPoint);
			int num = 0;
			if (StaticGlyph < MobileGlyph)
			{
				for (int i = StaticGlyph + 1; i < MobileGlyph; i++)
				{
					num += Advances[i];
				}
			}
			else
			{
				for (int j = MobileGlyph + 1; j < StaticGlyph; j++)
				{
					num += Advances[j];
				}
			}
			if (Metrics.Direction == TextFlowDirection.LTR || Metrics.Direction == TextFlowDirection.RTL)
			{
				Offsets[MobileGlyph].dy = Offsets[StaticGlyph].dy + layoutOffset.dy - layoutOffset2.dy;
				if (Metrics.Direction == TextFlowDirection.LTR == StaticGlyph < MobileGlyph)
				{
					int num2 = Offsets[StaticGlyph].dx - Advances[StaticGlyph] + layoutOffset.dx - num - layoutOffset2.dx;
					if (UseAdvances)
					{
						Advances[StaticGlyph] += num2;
						return;
					}
					Offsets[MobileGlyph].dx = num2;
					return;
				}
				else
				{
					int num3 = Offsets[StaticGlyph].dx + Advances[MobileGlyph] + layoutOffset.dx + num - layoutOffset2.dx;
					if (UseAdvances)
					{
						Advances[MobileGlyph] -= num3;
						return;
					}
					Offsets[MobileGlyph].dx = num3;
				}
			}
		}
	}
}
