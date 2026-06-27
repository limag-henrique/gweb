using System;
using System.Runtime.InteropServices;
using System.Security;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000751 RID: 1873
	internal static class UnsafeNativeMethods
	{
		// Token: 0x06004E16 RID: 19990
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll")]
		internal static extern LsErr LoCreateContext(ref LsContextInfo contextInfo, ref LscbkRedefined lscbkRedef, out IntPtr ploc);

		// Token: 0x06004E17 RID: 19991
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll")]
		internal static extern LsErr LoDestroyContext(IntPtr ploc);

		// Token: 0x06004E18 RID: 19992
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll")]
		internal static extern LsErr LoCreateLine(IntPtr ploc, int cp, int ccpLim, int durColumn, uint dwLineFlags, IntPtr pInputBreakRec, out LsLInfo plslinfo, out IntPtr pploline, out int maxDepth, out LsLineWidths lineWidths);

		// Token: 0x06004E19 RID: 19993
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll")]
		internal static extern LsErr LoDisposeLine(IntPtr ploline, [MarshalAs(UnmanagedType.Bool)] bool finalizing);

		// Token: 0x06004E1A RID: 19994
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll")]
		internal static extern LsErr LoAcquireBreakRecord(IntPtr ploline, out IntPtr pbreakrec);

		// Token: 0x06004E1B RID: 19995
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll")]
		internal static extern LsErr LoDisposeBreakRecord(IntPtr pBreakRec, [MarshalAs(UnmanagedType.Bool)] bool finalizing);

		// Token: 0x06004E1C RID: 19996
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll")]
		internal static extern LsErr LoCloneBreakRecord(IntPtr pBreakRec, out IntPtr pBreakRecClone);

		// Token: 0x06004E1D RID: 19997
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll")]
		internal static extern LsErr LoRelievePenaltyResource(IntPtr ploline);

		// Token: 0x06004E1E RID: 19998
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll")]
		internal static extern LsErr LoSetBreaking(IntPtr ploc, int strategy);

		// Token: 0x06004E1F RID: 19999
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll")]
		internal static extern LsErr LoSetDoc(IntPtr ploc, int isDisplay, int isReferencePresentationEqual, ref LsDevRes deviceInfo);

		// Token: 0x06004E20 RID: 20000
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll")]
		internal unsafe static extern LsErr LoSetTabs(IntPtr ploc, int durIncrementalTab, int tabCount, LsTbd* pTabs);

		// Token: 0x06004E21 RID: 20001
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll")]
		internal static extern LsErr LoDisplayLine(IntPtr ploline, ref LSPOINT pt, uint displayMode, ref LSRECT clipRect);

		// Token: 0x06004E22 RID: 20002
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll")]
		internal static extern LsErr LoEnumLine(IntPtr ploline, bool reverseOder, bool fGeometryneeded, ref LSPOINT pt);

		// Token: 0x06004E23 RID: 20003
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll")]
		internal static extern LsErr LoQueryLineCpPpoint(IntPtr ploline, int lscpQuery, int depthQueryMax, IntPtr pSubLineInfo, out int actualDepthQuery, out LsTextCell lsTextCell);

		// Token: 0x06004E24 RID: 20004
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll")]
		internal static extern LsErr LoQueryLinePointPcp(IntPtr ploline, ref LSPOINT ptQuery, int depthQueryMax, IntPtr pSubLineInfo, out int actualDepthQuery, out LsTextCell lsTextCell);

		// Token: 0x06004E25 RID: 20005
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll")]
		internal static extern LsErr LoCreateBreaks(IntPtr ploc, int cpFirst, IntPtr previousBreakRecord, IntPtr ploparabreak, IntPtr ptslinevariantRestriction, ref LsBreaks lsbreaks, out int bestFitIndex);

		// Token: 0x06004E26 RID: 20006
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll")]
		internal static extern LsErr LoCreateParaBreakingSession(IntPtr ploc, int cpParagraphFirst, int maxWidth, IntPtr previousParaBreakRecord, ref IntPtr pploparabreak, [MarshalAs(UnmanagedType.Bool)] ref bool fParagraphJustified);

		// Token: 0x06004E27 RID: 20007
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll")]
		internal static extern LsErr LoDisposeParaBreakingSession(IntPtr ploparabreak, [MarshalAs(UnmanagedType.Bool)] bool finalizing);

		// Token: 0x06004E28 RID: 20008
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll")]
		internal unsafe static extern LsErr LocbkGetObjectHandlerInfo(IntPtr ploc, uint objectId, void* objectInfo);

		// Token: 0x06004E29 RID: 20009 RVA: 0x00133758 File Offset: 0x00132B58
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static void LoGetEscString(ref EscStringInfo escStringInfo)
		{
			UnsafeNativeMethods.LoGetEscStringImpl(ref escStringInfo);
		}

		// Token: 0x06004E2A RID: 20010
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll", EntryPoint = "LoGetEscString")]
		private static extern void LoGetEscStringImpl(ref EscStringInfo escStringInfo);

		// Token: 0x06004E2B RID: 20011
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll")]
		internal static extern LsErr LoAcquirePenaltyModule(IntPtr ploc, out IntPtr penaltyModuleHandle);

		// Token: 0x06004E2C RID: 20012
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll")]
		internal static extern LsErr LoDisposePenaltyModule(IntPtr penaltyModuleHandle);

		// Token: 0x06004E2D RID: 20013
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll")]
		internal static extern LsErr LoGetPenaltyModuleInternalHandle(IntPtr penaltyModuleHandle, out IntPtr penaltyModuleInternalHandle);

		// Token: 0x06004E2E RID: 20014
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll")]
		internal unsafe static extern void* CreateTextAnalysisSink();

		// Token: 0x06004E2F RID: 20015
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll")]
		internal unsafe static extern void* GetScriptAnalysisList(void* textAnalysisSink);

		// Token: 0x06004E30 RID: 20016
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll")]
		internal unsafe static extern void* GetNumberSubstitutionList(void* textAnalysisSink);

		// Token: 0x06004E31 RID: 20017
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll")]
		internal unsafe static extern int CreateTextAnalysisSource(ushort* text, uint length, ushort* culture, void* factory, bool isRightToLeft, ushort* numberCulture, bool ignoreUserOverride, uint numberSubstitutionMethod, void** ppTextAnalysisSource);
	}
}
