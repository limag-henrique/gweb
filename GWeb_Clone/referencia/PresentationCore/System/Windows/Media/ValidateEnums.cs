using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	// Token: 0x0200035F RID: 863
	[FriendAccessAllowed]
	internal static class ValidateEnums
	{
		// Token: 0x06001D69 RID: 7529 RVA: 0x000788C8 File Offset: 0x00077CC8
		public static bool IsAlignmentXValid(object valueObject)
		{
			AlignmentX alignmentX = (AlignmentX)valueObject;
			return alignmentX == AlignmentX.Left || alignmentX == AlignmentX.Center || alignmentX == AlignmentX.Right;
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x000788EC File Offset: 0x00077CEC
		public static bool IsAlignmentYValid(object valueObject)
		{
			AlignmentY alignmentY = (AlignmentY)valueObject;
			return alignmentY == AlignmentY.Top || alignmentY == AlignmentY.Center || alignmentY == AlignmentY.Bottom;
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x00078910 File Offset: 0x00077D10
		public static bool IsBrushMappingModeValid(object valueObject)
		{
			BrushMappingMode brushMappingMode = (BrushMappingMode)valueObject;
			return brushMappingMode == BrushMappingMode.Absolute || brushMappingMode == BrushMappingMode.RelativeToBoundingBox;
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x00078930 File Offset: 0x00077D30
		public static bool IsCachingHintValid(object valueObject)
		{
			CachingHint cachingHint = (CachingHint)valueObject;
			return cachingHint == CachingHint.Unspecified || cachingHint == CachingHint.Cache;
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x00078950 File Offset: 0x00077D50
		public static bool IsColorInterpolationModeValid(object valueObject)
		{
			ColorInterpolationMode colorInterpolationMode = (ColorInterpolationMode)valueObject;
			return colorInterpolationMode == ColorInterpolationMode.ScRgbLinearInterpolation || colorInterpolationMode == ColorInterpolationMode.SRgbLinearInterpolation;
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x00078970 File Offset: 0x00077D70
		public static bool IsGeometryCombineModeValid(object valueObject)
		{
			GeometryCombineMode geometryCombineMode = (GeometryCombineMode)valueObject;
			return geometryCombineMode == GeometryCombineMode.Union || geometryCombineMode == GeometryCombineMode.Intersect || geometryCombineMode == GeometryCombineMode.Xor || geometryCombineMode == GeometryCombineMode.Exclude;
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x00078998 File Offset: 0x00077D98
		public static bool IsEdgeModeValid(object valueObject)
		{
			EdgeMode edgeMode = (EdgeMode)valueObject;
			return edgeMode == EdgeMode.Unspecified || edgeMode == EdgeMode.Aliased;
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x000789B8 File Offset: 0x00077DB8
		public static bool IsBitmapScalingModeValid(object valueObject)
		{
			BitmapScalingMode bitmapScalingMode = (BitmapScalingMode)valueObject;
			return bitmapScalingMode == BitmapScalingMode.Unspecified || bitmapScalingMode == BitmapScalingMode.LowQuality || bitmapScalingMode == BitmapScalingMode.HighQuality || bitmapScalingMode == BitmapScalingMode.LowQuality || bitmapScalingMode == BitmapScalingMode.HighQuality || bitmapScalingMode == BitmapScalingMode.NearestNeighbor;
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x000789E8 File Offset: 0x00077DE8
		public static bool IsClearTypeHintValid(object valueObject)
		{
			ClearTypeHint clearTypeHint = (ClearTypeHint)valueObject;
			return clearTypeHint == ClearTypeHint.Auto || clearTypeHint == ClearTypeHint.Enabled;
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x00078A08 File Offset: 0x00077E08
		public static bool IsTextRenderingModeValid(object valueObject)
		{
			TextRenderingMode textRenderingMode = (TextRenderingMode)valueObject;
			return textRenderingMode == TextRenderingMode.Auto || textRenderingMode == TextRenderingMode.Aliased || textRenderingMode == TextRenderingMode.Grayscale || textRenderingMode == TextRenderingMode.ClearType;
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x00078A30 File Offset: 0x00077E30
		public static bool IsTextHintingModeValid(object valueObject)
		{
			TextHintingMode textHintingMode = (TextHintingMode)valueObject;
			return textHintingMode == TextHintingMode.Auto || textHintingMode == TextHintingMode.Fixed || textHintingMode == TextHintingMode.Animated;
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x00078A54 File Offset: 0x00077E54
		public static bool IsFillRuleValid(object valueObject)
		{
			FillRule fillRule = (FillRule)valueObject;
			return fillRule == FillRule.EvenOdd || fillRule == FillRule.Nonzero;
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x00078A74 File Offset: 0x00077E74
		public static bool IsGradientSpreadMethodValid(object valueObject)
		{
			GradientSpreadMethod gradientSpreadMethod = (GradientSpreadMethod)valueObject;
			return gradientSpreadMethod == GradientSpreadMethod.Pad || gradientSpreadMethod == GradientSpreadMethod.Reflect || gradientSpreadMethod == GradientSpreadMethod.Repeat;
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x00078A98 File Offset: 0x00077E98
		public static bool IsPenLineCapValid(object valueObject)
		{
			PenLineCap penLineCap = (PenLineCap)valueObject;
			return penLineCap == PenLineCap.Flat || penLineCap == PenLineCap.Square || penLineCap == PenLineCap.Round || penLineCap == PenLineCap.Triangle;
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x00078AC0 File Offset: 0x00077EC0
		public static bool IsPenLineJoinValid(object valueObject)
		{
			PenLineJoin penLineJoin = (PenLineJoin)valueObject;
			return penLineJoin == PenLineJoin.Miter || penLineJoin == PenLineJoin.Bevel || penLineJoin == PenLineJoin.Round;
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x00078AE4 File Offset: 0x00077EE4
		public static bool IsStretchValid(object valueObject)
		{
			Stretch stretch = (Stretch)valueObject;
			return stretch == Stretch.None || stretch == Stretch.Fill || stretch == Stretch.Uniform || stretch == Stretch.UniformToFill;
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x00078B0C File Offset: 0x00077F0C
		public static bool IsTileModeValid(object valueObject)
		{
			TileMode tileMode = (TileMode)valueObject;
			return tileMode == TileMode.None || tileMode == TileMode.Tile || tileMode == TileMode.FlipX || tileMode == TileMode.FlipY || tileMode == TileMode.FlipXY;
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x00078B38 File Offset: 0x00077F38
		public static bool IsSweepDirectionValid(object valueObject)
		{
			SweepDirection sweepDirection = (SweepDirection)valueObject;
			return sweepDirection == SweepDirection.Counterclockwise || sweepDirection == SweepDirection.Clockwise;
		}
	}
}
