using System;
using System.Collections.Generic;
using System.Security;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using MS.Internal.Text.TextInterface;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000757 RID: 1879
	internal abstract class TextShapeableSymbols : TextRun
	{
		// Token: 0x06004EB4 RID: 20148
		internal abstract GlyphRun ComputeShapedGlyphRun(Point origin, char[] characterString, ushort[] clusterMap, ushort[] glyphIndices, IList<double> glyphAdvances, IList<Point> glyphOffsets, bool rightToLeft, bool sideways);

		// Token: 0x06004EB5 RID: 20149
		internal abstract bool CanShapeTogether(TextShapeableSymbols shapeable);

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x06004EB6 RID: 20150
		internal abstract bool IsShapingRequired { get; }

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x06004EB7 RID: 20151
		internal abstract bool NeedsMaxClusterSize { get; }

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x06004EB8 RID: 20152
		internal abstract ushort MaxClusterSize { get; }

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x06004EB9 RID: 20153
		internal abstract bool NeedsCaretInfo { get; }

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x06004EBA RID: 20154
		internal abstract bool HasExtendedCharacter { get; }

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x06004EBB RID: 20155
		internal abstract GlyphTypeface GlyphTypeFace { get; }

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x06004EBC RID: 20156
		internal abstract double EmSize { get; }

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x06004EBD RID: 20157
		internal abstract ItemProps ItemProps { get; }

		// Token: 0x06004EBE RID: 20158
		[SecurityCritical]
		internal unsafe abstract void GetAdvanceWidthsUnshaped(char* characterString, int characterLength, double scalingFactor, int* advanceWidthsUnshaped);

		// Token: 0x06004EBF RID: 20159
		internal abstract GlyphRun ComputeUnshapedGlyphRun(Point origin, char[] characterString, IList<double> characterAdvances);

		// Token: 0x06004EC0 RID: 20160
		internal abstract void Draw(DrawingContext drawingContext, Brush foregroundBrush, GlyphRun glyphRun);

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x06004EC1 RID: 20161
		internal abstract double Height { get; }

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x06004EC2 RID: 20162
		internal abstract double Baseline { get; }

		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x06004EC3 RID: 20163
		internal abstract double UnderlinePosition { get; }

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x06004EC4 RID: 20164
		internal abstract double UnderlineThickness { get; }

		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x06004EC5 RID: 20165
		internal abstract double StrikethroughPosition { get; }

		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x06004EC6 RID: 20166
		internal abstract double StrikethroughThickness { get; }
	}
}
