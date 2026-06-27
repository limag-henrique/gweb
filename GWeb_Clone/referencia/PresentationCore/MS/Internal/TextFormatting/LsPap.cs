using System;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000743 RID: 1859
	internal struct LsPap
	{
		// Token: 0x04002333 RID: 9011
		public int cpFirst;

		// Token: 0x04002334 RID: 9012
		public int cpFirstContent;

		// Token: 0x04002335 RID: 9013
		public LsPap.Flags grpf;

		// Token: 0x04002336 RID: 9014
		public LsBreakJust lsbrj;

		// Token: 0x04002337 RID: 9015
		public LsKJust lskj;

		// Token: 0x04002338 RID: 9016
		public int fJustify;

		// Token: 0x04002339 RID: 9017
		public int durAutoDecimalTab;

		// Token: 0x0400233A RID: 9018
		public LsKEOP lskeop;

		// Token: 0x0400233B RID: 9019
		public LsTFlow lstflow;

		// Token: 0x020009DC RID: 2524
		[Flags]
		public enum Flags : uint
		{
			// Token: 0x04002E73 RID: 11891
			None = 0U,
			// Token: 0x04002E74 RID: 11892
			fFmiVisiCondHyphens = 1U,
			// Token: 0x04002E75 RID: 11893
			fFmiVisiParaMarks = 2U,
			// Token: 0x04002E76 RID: 11894
			fFmiVisiSpaces = 4U,
			// Token: 0x04002E77 RID: 11895
			fFmiVisiTabs = 8U,
			// Token: 0x04002E78 RID: 11896
			fFmiVisiSplats = 16U,
			// Token: 0x04002E79 RID: 11897
			fFmiVisiBreaks = 32U,
			// Token: 0x04002E7A RID: 11898
			fFmiApplyBreakingRules = 64U,
			// Token: 0x04002E7B RID: 11899
			fFmiApplyOpticalAlignment = 128U,
			// Token: 0x04002E7C RID: 11900
			fFmiPunctStartLine = 256U,
			// Token: 0x04002E7D RID: 11901
			fFmiHangingPunct = 512U,
			// Token: 0x04002E7E RID: 11902
			fFmiPresSuppressWiggle = 1024U,
			// Token: 0x04002E7F RID: 11903
			fFmiPresExactSync = 2048U,
			// Token: 0x04002E80 RID: 11904
			fFmiAnm = 4096U,
			// Token: 0x04002E81 RID: 11905
			fFmiAutoDecimalTab = 8192U,
			// Token: 0x04002E82 RID: 11906
			fFmiUnderlineTrailSpacesRM = 16384U,
			// Token: 0x04002E83 RID: 11907
			fFmiSpacesInfluenceHeight = 32768U,
			// Token: 0x04002E84 RID: 11908
			fFmiIgnoreSplatBreak = 65536U,
			// Token: 0x04002E85 RID: 11909
			fFmiLimSplat = 131072U,
			// Token: 0x04002E86 RID: 11910
			fFmiAllowSplatLine = 262144U,
			// Token: 0x04002E87 RID: 11911
			fFmiForceBreakAsNext = 524288U,
			// Token: 0x04002E88 RID: 11912
			fFmiAllowHyphenation = 1048576U,
			// Token: 0x04002E89 RID: 11913
			fFmiDrawInCharCodes = 2097152U,
			// Token: 0x04002E8A RID: 11914
			fFmiTreatHyphenAsRegular = 4194304U,
			// Token: 0x04002E8B RID: 11915
			fFmiWrapTrailingSpaces = 8388608U,
			// Token: 0x04002E8C RID: 11916
			fFmiWrapAllSpaces = 16777216U,
			// Token: 0x04002E8D RID: 11917
			fFmiFCheckTruncateBefore = 33554432U,
			// Token: 0x04002E8E RID: 11918
			fFmiForgetLastTabAlignment = 268435456U,
			// Token: 0x04002E8F RID: 11919
			fFmiIndentChangesHyphenZone = 536870912U,
			// Token: 0x04002E90 RID: 11920
			fFmiNoPunctAfterAutoNumber = 1073741824U,
			// Token: 0x04002E91 RID: 11921
			fFmiResolveTabsAsWord97 = 2147483648U
		}
	}
}
