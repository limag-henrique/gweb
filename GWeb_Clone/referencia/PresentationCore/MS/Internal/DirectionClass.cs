using System;

namespace MS.Internal
{
	// Token: 0x0200069A RID: 1690
	internal enum DirectionClass : byte
	{
		// Token: 0x04001EF5 RID: 7925
		Left,
		// Token: 0x04001EF6 RID: 7926
		Right,
		// Token: 0x04001EF7 RID: 7927
		ArabicNumber,
		// Token: 0x04001EF8 RID: 7928
		EuropeanNumber,
		// Token: 0x04001EF9 RID: 7929
		ArabicLetter,
		// Token: 0x04001EFA RID: 7930
		EuropeanSeparator,
		// Token: 0x04001EFB RID: 7931
		CommonSeparator,
		// Token: 0x04001EFC RID: 7932
		EuropeanTerminator,
		// Token: 0x04001EFD RID: 7933
		NonSpacingMark,
		// Token: 0x04001EFE RID: 7934
		BoundaryNeutral,
		// Token: 0x04001EFF RID: 7935
		GenericNeutral,
		// Token: 0x04001F00 RID: 7936
		ParagraphSeparator,
		// Token: 0x04001F01 RID: 7937
		LeftToRightEmbedding,
		// Token: 0x04001F02 RID: 7938
		LeftToRightOverride,
		// Token: 0x04001F03 RID: 7939
		RightToLeftEmbedding,
		// Token: 0x04001F04 RID: 7940
		RightToLeftOverride,
		// Token: 0x04001F05 RID: 7941
		PopDirectionalFormat,
		// Token: 0x04001F06 RID: 7942
		SegmentSeparator,
		// Token: 0x04001F07 RID: 7943
		WhiteSpace,
		// Token: 0x04001F08 RID: 7944
		OtherNeutral,
		// Token: 0x04001F09 RID: 7945
		ClassInvalid,
		// Token: 0x04001F0A RID: 7946
		ClassMax
	}
}
