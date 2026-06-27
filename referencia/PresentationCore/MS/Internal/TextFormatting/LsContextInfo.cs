using System;
using System.Runtime.InteropServices;
using System.Security;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000740 RID: 1856
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct LsContextInfo
	{
		// Token: 0x04002297 RID: 8855
		public uint version;

		// Token: 0x04002298 RID: 8856
		public int cInstalledHandlers;

		// Token: 0x04002299 RID: 8857
		public IntPtr plsimethods;

		// Token: 0x0400229A RID: 8858
		public int cEstimatedCharsPerLine;

		// Token: 0x0400229B RID: 8859
		public int cJustPriorityLim;

		// Token: 0x0400229C RID: 8860
		public char wchUndef;

		// Token: 0x0400229D RID: 8861
		public char wchNull;

		// Token: 0x0400229E RID: 8862
		public char wchSpace;

		// Token: 0x0400229F RID: 8863
		public char wchHyphen;

		// Token: 0x040022A0 RID: 8864
		public char wchTab;

		// Token: 0x040022A1 RID: 8865
		public char wchPosTab;

		// Token: 0x040022A2 RID: 8866
		public char wchEndPara1;

		// Token: 0x040022A3 RID: 8867
		public char wchEndPara2;

		// Token: 0x040022A4 RID: 8868
		public char wchAltEndPara;

		// Token: 0x040022A5 RID: 8869
		public char wchEndLineInPara;

		// Token: 0x040022A6 RID: 8870
		public char wchColumnBreak;

		// Token: 0x040022A7 RID: 8871
		public char wchSectionBreak;

		// Token: 0x040022A8 RID: 8872
		public char wchPageBreak;

		// Token: 0x040022A9 RID: 8873
		public char wchNonBreakSpace;

		// Token: 0x040022AA RID: 8874
		public char wchNonBreakHyphen;

		// Token: 0x040022AB RID: 8875
		public char wchNonReqHyphen;

		// Token: 0x040022AC RID: 8876
		public char wchEmDash;

		// Token: 0x040022AD RID: 8877
		public char wchEnDash;

		// Token: 0x040022AE RID: 8878
		public char wchEmSpace;

		// Token: 0x040022AF RID: 8879
		public char wchEnSpace;

		// Token: 0x040022B0 RID: 8880
		public char wchNarrowSpace;

		// Token: 0x040022B1 RID: 8881
		public char wchOptBreak;

		// Token: 0x040022B2 RID: 8882
		public char wchNoBreak;

		// Token: 0x040022B3 RID: 8883
		public char wchFESpace;

		// Token: 0x040022B4 RID: 8884
		public char wchJoiner;

		// Token: 0x040022B5 RID: 8885
		public char wchNonJoiner;

		// Token: 0x040022B6 RID: 8886
		public char wchToReplace;

		// Token: 0x040022B7 RID: 8887
		public char wchReplace;

		// Token: 0x040022B8 RID: 8888
		public char wchVisiNull;

		// Token: 0x040022B9 RID: 8889
		public char wchVisiAltEndPara;

		// Token: 0x040022BA RID: 8890
		public char wchVisiEndLineInPara;

		// Token: 0x040022BB RID: 8891
		public char wchVisiEndPara;

		// Token: 0x040022BC RID: 8892
		public char wchVisiSpace;

		// Token: 0x040022BD RID: 8893
		public char wchVisiNonBreakSpace;

		// Token: 0x040022BE RID: 8894
		public char wchVisiNonBreakHyphen;

		// Token: 0x040022BF RID: 8895
		public char wchVisiNonReqHyphen;

		// Token: 0x040022C0 RID: 8896
		public char wchVisiTab;

		// Token: 0x040022C1 RID: 8897
		public char wchVisiPosTab;

		// Token: 0x040022C2 RID: 8898
		public char wchVisiEmSpace;

		// Token: 0x040022C3 RID: 8899
		public char wchVisiEnSpace;

		// Token: 0x040022C4 RID: 8900
		public char wchVisiNarrowSpace;

		// Token: 0x040022C5 RID: 8901
		public char wchVisiOptBreak;

		// Token: 0x040022C6 RID: 8902
		public char wchVisiNoBreak;

		// Token: 0x040022C7 RID: 8903
		public char wchVisiFESpace;

		// Token: 0x040022C8 RID: 8904
		public char wchEscAnmRun;

		// Token: 0x040022C9 RID: 8905
		public char wchPad;

		// Token: 0x040022CA RID: 8906
		public IntPtr pols;

		// Token: 0x040022CB RID: 8907
		[SecurityCritical]
		public IntPtr pfnNewPtr;

		// Token: 0x040022CC RID: 8908
		[SecurityCritical]
		public IntPtr pfnDisposePtr;

		// Token: 0x040022CD RID: 8909
		[SecurityCritical]
		public IntPtr pfnReallocPtr;

		// Token: 0x040022CE RID: 8910
		[SecurityCritical]
		public IntPtr pfnFetchRun;

		// Token: 0x040022CF RID: 8911
		[SecurityCritical]
		public GetAutoNumberInfo pfnGetAutoNumberInfo;

		// Token: 0x040022D0 RID: 8912
		[SecurityCritical]
		public IntPtr pfnGetNumericSeparators;

		// Token: 0x040022D1 RID: 8913
		[SecurityCritical]
		public IntPtr pfnCheckForDigit;

		// Token: 0x040022D2 RID: 8914
		[SecurityCritical]
		public FetchPap pfnFetchPap;

		// Token: 0x040022D3 RID: 8915
		[SecurityCritical]
		public FetchLineProps pfnFetchLineProps;

		// Token: 0x040022D4 RID: 8916
		[SecurityCritical]
		public IntPtr pfnFetchTabs;

		// Token: 0x040022D5 RID: 8917
		[SecurityCritical]
		public IntPtr pfnReleaseTabsBuffer;

		// Token: 0x040022D6 RID: 8918
		[SecurityCritical]
		public IntPtr pfnGetBreakThroughTab;

		// Token: 0x040022D7 RID: 8919
		[SecurityCritical]
		public IntPtr pfnGetPosTabProps;

		// Token: 0x040022D8 RID: 8920
		[SecurityCritical]
		public IntPtr pfnFGetLastLineJustification;

		// Token: 0x040022D9 RID: 8921
		[SecurityCritical]
		public IntPtr pfnCheckParaBoundaries;

		// Token: 0x040022DA RID: 8922
		[SecurityCritical]
		public GetRunCharWidths pfnGetRunCharWidths;

		// Token: 0x040022DB RID: 8923
		[SecurityCritical]
		public IntPtr pfnCheckRunKernability;

		// Token: 0x040022DC RID: 8924
		[SecurityCritical]
		public IntPtr pfnGetRunCharKerning;

		// Token: 0x040022DD RID: 8925
		[SecurityCritical]
		public GetRunTextMetrics pfnGetRunTextMetrics;

		// Token: 0x040022DE RID: 8926
		[SecurityCritical]
		public GetRunUnderlineInfo pfnGetRunUnderlineInfo;

		// Token: 0x040022DF RID: 8927
		[SecurityCritical]
		public GetRunStrikethroughInfo pfnGetRunStrikethroughInfo;

		// Token: 0x040022E0 RID: 8928
		[SecurityCritical]
		public IntPtr pfnGetBorderInfo;

		// Token: 0x040022E1 RID: 8929
		[SecurityCritical]
		public IntPtr pfnReleaseRun;

		// Token: 0x040022E2 RID: 8930
		[SecurityCritical]
		public IntPtr pfnReleaseRunBuffer;

		// Token: 0x040022E3 RID: 8931
		[SecurityCritical]
		public Hyphenate pfnHyphenate;

		// Token: 0x040022E4 RID: 8932
		[SecurityCritical]
		public GetPrevHyphenOpp pfnGetPrevHyphenOpp;

		// Token: 0x040022E5 RID: 8933
		[SecurityCritical]
		public GetNextHyphenOpp pfnGetNextHyphenOpp;

		// Token: 0x040022E6 RID: 8934
		[SecurityCritical]
		public IntPtr pfnGetHyphenInfo;

		// Token: 0x040022E7 RID: 8935
		[SecurityCritical]
		public DrawUnderline pfnDrawUnderline;

		// Token: 0x040022E8 RID: 8936
		[SecurityCritical]
		public DrawStrikethrough pfnDrawStrikethrough;

		// Token: 0x040022E9 RID: 8937
		[SecurityCritical]
		public IntPtr pfnDrawBorder;

		// Token: 0x040022EA RID: 8938
		[SecurityCritical]
		public IntPtr pfnFInterruptUnderline;

		// Token: 0x040022EB RID: 8939
		[SecurityCritical]
		public IntPtr pfnFInterruptShade;

		// Token: 0x040022EC RID: 8940
		[SecurityCritical]
		public IntPtr pfnFInterruptBorder;

		// Token: 0x040022ED RID: 8941
		[SecurityCritical]
		public IntPtr pfnShadeRectangle;

		// Token: 0x040022EE RID: 8942
		[SecurityCritical]
		public DrawTextRun pfnDrawTextRun;

		// Token: 0x040022EF RID: 8943
		[SecurityCritical]
		public IntPtr pfnDrawSplatLine;

		// Token: 0x040022F0 RID: 8944
		[SecurityCritical]
		public FInterruptShaping pfnFInterruptShaping;

		// Token: 0x040022F1 RID: 8945
		[SecurityCritical]
		public IntPtr pfnGetGlyphs;

		// Token: 0x040022F2 RID: 8946
		[SecurityCritical]
		public GetGlyphPositions pfnGetGlyphPositions;

		// Token: 0x040022F3 RID: 8947
		[SecurityCritical]
		public DrawGlyphs pfnDrawGlyphs;

		// Token: 0x040022F4 RID: 8948
		[SecurityCritical]
		public IntPtr pfnReleaseGlyphBuffers;

		// Token: 0x040022F5 RID: 8949
		[SecurityCritical]
		public IntPtr pfnGetGlyphExpansionInfo;

		// Token: 0x040022F6 RID: 8950
		[SecurityCritical]
		public IntPtr pfnGetGlyphExpansionInkInfo;

		// Token: 0x040022F7 RID: 8951
		[SecurityCritical]
		public IntPtr pfnGetGlyphRunInk;

		// Token: 0x040022F8 RID: 8952
		[SecurityCritical]
		public IntPtr pfnGetEms;

		// Token: 0x040022F9 RID: 8953
		[SecurityCritical]
		public IntPtr pfnPunctStartLine;

		// Token: 0x040022FA RID: 8954
		[SecurityCritical]
		public IntPtr pfnModWidthOnRun;

		// Token: 0x040022FB RID: 8955
		[SecurityCritical]
		public IntPtr pfnModWidthSpace;

		// Token: 0x040022FC RID: 8956
		[SecurityCritical]
		public IntPtr pfnCompOnRun;

		// Token: 0x040022FD RID: 8957
		[SecurityCritical]
		public IntPtr pfnCompWidthSpace;

		// Token: 0x040022FE RID: 8958
		[SecurityCritical]
		public IntPtr pfnExpOnRun;

		// Token: 0x040022FF RID: 8959
		[SecurityCritical]
		public IntPtr pfnExpWidthSpace;

		// Token: 0x04002300 RID: 8960
		[SecurityCritical]
		public IntPtr pfnGetModWidthClasses;

		// Token: 0x04002301 RID: 8961
		[SecurityCritical]
		public IntPtr pfnGetBreakingClasses;

		// Token: 0x04002302 RID: 8962
		[SecurityCritical]
		public IntPtr pfnFTruncateBefore;

		// Token: 0x04002303 RID: 8963
		[SecurityCritical]
		public IntPtr pfnCanBreakBeforeChar;

		// Token: 0x04002304 RID: 8964
		[SecurityCritical]
		public IntPtr pfnCanBreakAfterChar;

		// Token: 0x04002305 RID: 8965
		[SecurityCritical]
		public IntPtr pfnFHangingPunct;

		// Token: 0x04002306 RID: 8966
		[SecurityCritical]
		public IntPtr pfnGetSnapGrid;

		// Token: 0x04002307 RID: 8967
		[SecurityCritical]
		public IntPtr pfnDrawEffects;

		// Token: 0x04002308 RID: 8968
		[SecurityCritical]
		public IntPtr pfnFCancelHangingPunct;

		// Token: 0x04002309 RID: 8969
		[SecurityCritical]
		public IntPtr pfnModifyCompAtLastChar;

		// Token: 0x0400230A RID: 8970
		[SecurityCritical]
		public GetDurMaxExpandRagged pfnGetDurMaxExpandRagged;

		// Token: 0x0400230B RID: 8971
		[SecurityCritical]
		public GetCharExpansionInfoFullMixed pfnGetCharExpansionInfoFullMixed;

		// Token: 0x0400230C RID: 8972
		[SecurityCritical]
		public GetGlyphExpansionInfoFullMixed pfnGetGlyphExpansionInfoFullMixed;

		// Token: 0x0400230D RID: 8973
		[SecurityCritical]
		public GetCharCompressionInfoFullMixed pfnGetCharCompressionInfoFullMixed;

		// Token: 0x0400230E RID: 8974
		[SecurityCritical]
		public GetGlyphCompressionInfoFullMixed pfnGetGlyphCompressionInfoFullMixed;

		// Token: 0x0400230F RID: 8975
		[SecurityCritical]
		public IntPtr pfnGetCharAlignmentStartLine;

		// Token: 0x04002310 RID: 8976
		[SecurityCritical]
		public IntPtr pfnGetCharAlignmentEndLine;

		// Token: 0x04002311 RID: 8977
		[SecurityCritical]
		public IntPtr pfnGetGlyphAlignmentStartLine;

		// Token: 0x04002312 RID: 8978
		[SecurityCritical]
		public IntPtr pfnGetGlyphAlignmentEndLine;

		// Token: 0x04002313 RID: 8979
		[SecurityCritical]
		public IntPtr pfnGetPriorityForGoodTypography;

		// Token: 0x04002314 RID: 8980
		[SecurityCritical]
		public EnumText pfnEnumText;

		// Token: 0x04002315 RID: 8981
		[SecurityCritical]
		public EnumTab pfnEnumTab;

		// Token: 0x04002316 RID: 8982
		[SecurityCritical]
		public IntPtr pfnEnumPen;

		// Token: 0x04002317 RID: 8983
		[SecurityCritical]
		public GetObjectHandlerInfo pfnGetObjectHandlerInfo;

		// Token: 0x04002318 RID: 8984
		[SecurityCritical]
		public IntPtr pfnAssertFailedPtr;

		// Token: 0x04002319 RID: 8985
		public int fDontReleaseRuns;
	}
}
