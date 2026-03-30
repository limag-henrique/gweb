using System;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007CD RID: 1997
	internal static class KnownTagCache
	{
		// Token: 0x040025F8 RID: 9720
		internal static uint MaximumPossibleKnownTags = 50U;

		// Token: 0x040025F9 RID: 9721
		internal static uint KnownTagCount = (uint)((byte)KnownTagCache.MaximumPossibleKnownTags);

		// Token: 0x02000A0C RID: 2572
		internal enum KnownTagIndex : uint
		{
			// Token: 0x04002F5D RID: 12125
			Unknown,
			// Token: 0x04002F5E RID: 12126
			InkSpaceRectangle = 0U,
			// Token: 0x04002F5F RID: 12127
			GuidTable,
			// Token: 0x04002F60 RID: 12128
			DrawingAttributesTable,
			// Token: 0x04002F61 RID: 12129
			DrawingAttributesBlock,
			// Token: 0x04002F62 RID: 12130
			StrokeDescriptorTable,
			// Token: 0x04002F63 RID: 12131
			StrokeDescriptorBlock,
			// Token: 0x04002F64 RID: 12132
			Buttons,
			// Token: 0x04002F65 RID: 12133
			NoX,
			// Token: 0x04002F66 RID: 12134
			NoY,
			// Token: 0x04002F67 RID: 12135
			DrawingAttributesTableIndex,
			// Token: 0x04002F68 RID: 12136
			Stroke,
			// Token: 0x04002F69 RID: 12137
			StrokePropertyList,
			// Token: 0x04002F6A RID: 12138
			PointProperty,
			// Token: 0x04002F6B RID: 12139
			StrokeDescriptorTableIndex,
			// Token: 0x04002F6C RID: 12140
			CompressionHeader,
			// Token: 0x04002F6D RID: 12141
			TransformTable,
			// Token: 0x04002F6E RID: 12142
			Transform,
			// Token: 0x04002F6F RID: 12143
			TransformIsotropicScale,
			// Token: 0x04002F70 RID: 12144
			TransformAnisotropicScale,
			// Token: 0x04002F71 RID: 12145
			TransformRotate,
			// Token: 0x04002F72 RID: 12146
			TransformTranslate,
			// Token: 0x04002F73 RID: 12147
			TransformScaleAndTranslate,
			// Token: 0x04002F74 RID: 12148
			TransformQuad,
			// Token: 0x04002F75 RID: 12149
			TransformTableIndex,
			// Token: 0x04002F76 RID: 12150
			MetricTable,
			// Token: 0x04002F77 RID: 12151
			MetricBlock,
			// Token: 0x04002F78 RID: 12152
			MetricTableIndex,
			// Token: 0x04002F79 RID: 12153
			Mantissa,
			// Token: 0x04002F7A RID: 12154
			PersistenceFormat,
			// Token: 0x04002F7B RID: 12155
			HimetricSize,
			// Token: 0x04002F7C RID: 12156
			StrokeIds,
			// Token: 0x04002F7D RID: 12157
			ExtendedTransformTable
		}
	}
}
