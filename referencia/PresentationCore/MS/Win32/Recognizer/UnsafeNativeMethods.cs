using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows.Ink;

namespace MS.Win32.Recognizer
{
	// Token: 0x0200064F RID: 1615
	internal static class UnsafeNativeMethods
	{
		// Token: 0x06004876 RID: 18550
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("mshwgst.dll")]
		internal static extern int CreateRecognizer([In] ref Guid clsid, out RecognizerSafeHandle hRec);

		// Token: 0x06004877 RID: 18551
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("mshwgst.dll")]
		internal static extern int DestroyRecognizer([In] IntPtr hRec);

		// Token: 0x06004878 RID: 18552
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("mshwgst.dll")]
		internal static extern int CreateContext([In] RecognizerSafeHandle hRec, out ContextSafeHandle hRecContext);

		// Token: 0x06004879 RID: 18553
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("mshwgst.dll")]
		internal static extern int DestroyContext([In] IntPtr hRecContext);

		// Token: 0x0600487A RID: 18554
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("mshwgst.dll")]
		internal static extern int AddStroke([In] ContextSafeHandle hRecContext, [In] ref PACKET_DESCRIPTION packetDesc, [In] uint cbPackets, [In] IntPtr pByte, [MarshalAs(UnmanagedType.LPStruct)] [In] NativeMethods.XFORM xForm);

		// Token: 0x0600487B RID: 18555
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("mshwgst.dll")]
		internal static extern int SetEnabledUnicodeRanges([In] ContextSafeHandle hRecContext, [In] uint cRangs, [In] CHARACTER_RANGE[] charRanges);

		// Token: 0x0600487C RID: 18556
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("mshwgst.dll")]
		internal static extern int EndInkInput([In] ContextSafeHandle hRecContext);

		// Token: 0x0600487D RID: 18557
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("mshwgst.dll")]
		internal static extern int Process([In] ContextSafeHandle hRecContext, out bool partialProcessing);

		// Token: 0x0600487E RID: 18558
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("mshwgst.dll")]
		internal static extern int GetAlternateList([In] ContextSafeHandle hRecContext, [In] [Out] ref RECO_RANGE recoRange, [In] [Out] ref uint cAlts, [In] [Out] IntPtr[] recAtls, [In] ALT_BREAKS breaks);

		// Token: 0x0600487F RID: 18559
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("mshwgst.dll")]
		internal static extern int DestroyAlternate([In] IntPtr hRecAtls);

		// Token: 0x06004880 RID: 18560
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("mshwgst.dll", CharSet = CharSet.Unicode)]
		internal static extern int GetString([In] IntPtr hRecAtls, out RECO_RANGE recoRange, [In] [Out] ref uint size, [In] [Out] StringBuilder recoString);

		// Token: 0x06004881 RID: 18561
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("mshwgst.dll")]
		internal static extern int GetConfidenceLevel([In] IntPtr hRecAtls, out RECO_RANGE recoRange, out RecognitionConfidence confidenceLevel);

		// Token: 0x06004882 RID: 18562
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("mshwgst.dll")]
		internal static extern int ResetContext([In] ContextSafeHandle hRecContext);

		// Token: 0x06004883 RID: 18563
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("mshwgst.dll")]
		internal static extern int GetLatticePtr([In] ContextSafeHandle hRecContext, [In] ref IntPtr pRecoLattice);
	}
}
