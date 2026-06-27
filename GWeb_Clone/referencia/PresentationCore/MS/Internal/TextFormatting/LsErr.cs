using System;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000736 RID: 1846
	internal enum LsErr
	{
		// Token: 0x0400222A RID: 8746
		None,
		// Token: 0x0400222B RID: 8747
		InvalidParameter = -1,
		// Token: 0x0400222C RID: 8748
		OutOfMemory = -2,
		// Token: 0x0400222D RID: 8749
		NullOutputParameter = -3,
		// Token: 0x0400222E RID: 8750
		InvalidContext = -4,
		// Token: 0x0400222F RID: 8751
		InvalidLine = -5,
		// Token: 0x04002230 RID: 8752
		InvalidDnode = -6,
		// Token: 0x04002231 RID: 8753
		InvalidDeviceResolution = -7,
		// Token: 0x04002232 RID: 8754
		InvalidRun = -8,
		// Token: 0x04002233 RID: 8755
		MismatchLineContext = -9,
		// Token: 0x04002234 RID: 8756
		ContextInUse = -10,
		// Token: 0x04002235 RID: 8757
		DuplicateSpecialCharacter = -11,
		// Token: 0x04002236 RID: 8758
		InvalidAutonumRun = -12,
		// Token: 0x04002237 RID: 8759
		FormattingFunctionDisabled = -13,
		// Token: 0x04002238 RID: 8760
		UnfinishedDnode = -14,
		// Token: 0x04002239 RID: 8761
		InvalidDnodeType = -15,
		// Token: 0x0400223A RID: 8762
		InvalidPenDnode = -16,
		// Token: 0x0400223B RID: 8763
		InvalidNonPenDnode = -17,
		// Token: 0x0400223C RID: 8764
		InvalidBaselinePenDnode = -18,
		// Token: 0x0400223D RID: 8765
		InvalidFormatterResult = -19,
		// Token: 0x0400223E RID: 8766
		InvalidObjectIdFetched = -20,
		// Token: 0x0400223F RID: 8767
		InvalidDcpFetched = -21,
		// Token: 0x04002240 RID: 8768
		InvalidCpContentFetched = -22,
		// Token: 0x04002241 RID: 8769
		InvalidBookmarkType = -23,
		// Token: 0x04002242 RID: 8770
		SetDocDisabled = -24,
		// Token: 0x04002243 RID: 8771
		FiniFunctionDisabled = -25,
		// Token: 0x04002244 RID: 8772
		CurrentDnodeIsNotTab = -26,
		// Token: 0x04002245 RID: 8773
		PendingTabIsNotResolved = -27,
		// Token: 0x04002246 RID: 8774
		WrongFiniFunction = -28,
		// Token: 0x04002247 RID: 8775
		InvalidBreakingClass = -29,
		// Token: 0x04002248 RID: 8776
		BreakingTableNotSet = -30,
		// Token: 0x04002249 RID: 8777
		InvalidModWidthClass = -31,
		// Token: 0x0400224A RID: 8778
		ModWidthPairsNotSet = -32,
		// Token: 0x0400224B RID: 8779
		WrongTruncationPoint = -33,
		// Token: 0x0400224C RID: 8780
		WrongBreak = -34,
		// Token: 0x0400224D RID: 8781
		DupInvalid = -35,
		// Token: 0x0400224E RID: 8782
		RubyInvalidVersion = -36,
		// Token: 0x0400224F RID: 8783
		TatenakayokoInvalidVersion = -37,
		// Token: 0x04002250 RID: 8784
		WarichuInvalidVersion = -38,
		// Token: 0x04002251 RID: 8785
		WarichuInvalidData = -39,
		// Token: 0x04002252 RID: 8786
		CreateSublineDisabled = -40,
		// Token: 0x04002253 RID: 8787
		CurrentSublineDoesNotExist = -41,
		// Token: 0x04002254 RID: 8788
		CpOutsideSubline = -42,
		// Token: 0x04002255 RID: 8789
		HihInvalidVersion = -43,
		// Token: 0x04002256 RID: 8790
		InsufficientQueryDepth = -44,
		// Token: 0x04002257 RID: 8791
		InvalidBreakRecord = -45,
		// Token: 0x04002258 RID: 8792
		InvalidPap = -46,
		// Token: 0x04002259 RID: 8793
		ContradictoryQueryInput = -47,
		// Token: 0x0400225A RID: 8794
		LineIsNotActive = -48,
		// Token: 0x0400225B RID: 8795
		TooLongParagraph = -49,
		// Token: 0x0400225C RID: 8796
		TooManyCharsToGlyph = -50,
		// Token: 0x0400225D RID: 8797
		WrongHyphenationPosition = -51,
		// Token: 0x0400225E RID: 8798
		TooManyPriorities = -52,
		// Token: 0x0400225F RID: 8799
		WrongGivenCp = -53,
		// Token: 0x04002260 RID: 8800
		WrongCpFirstForGetBreaks = -54,
		// Token: 0x04002261 RID: 8801
		WrongJustTypeForGetBreaks = -55,
		// Token: 0x04002262 RID: 8802
		WrongJustTypeForCreateLineGivenCp = -56,
		// Token: 0x04002263 RID: 8803
		TooLongGlyphContext = -57,
		// Token: 0x04002264 RID: 8804
		InvalidCharToGlyphMapping = -58,
		// Token: 0x04002265 RID: 8805
		InvalidMathUsage = -59,
		// Token: 0x04002266 RID: 8806
		InconsistentChp = -60,
		// Token: 0x04002267 RID: 8807
		StoppedInSubline = -61,
		// Token: 0x04002268 RID: 8808
		PenPositionCouldNotBeUsed = -62,
		// Token: 0x04002269 RID: 8809
		DebugFlagsInShip = -63,
		// Token: 0x0400226A RID: 8810
		InvalidOrderTabs = -64,
		// Token: 0x0400226B RID: 8811
		OutputArrayTooSmall = -110,
		// Token: 0x0400226C RID: 8812
		SystemRestrictionsExceeded = -100,
		// Token: 0x0400226D RID: 8813
		LsInternalError = -1000,
		// Token: 0x0400226E RID: 8814
		NotImplemented = -10000,
		// Token: 0x0400226F RID: 8815
		ClientAbort = -100000
	}
}
