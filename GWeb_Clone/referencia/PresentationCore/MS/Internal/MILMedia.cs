using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Media;

namespace MS.Internal
{
	// Token: 0x02000669 RID: 1641
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal static class MILMedia
	{
		// Token: 0x06004896 RID: 18582
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILMediaOpen")]
		internal static extern int Open(SafeMediaHandle THIS_PTR, [MarshalAs(UnmanagedType.BStr)] [In] string src);

		// Token: 0x06004897 RID: 18583
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILMediaStop")]
		internal static extern int Stop(SafeMediaHandle THIS_PTR);

		// Token: 0x06004898 RID: 18584
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILMediaClose")]
		internal static extern int Close(SafeMediaHandle THIS_PTR);

		// Token: 0x06004899 RID: 18585
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILMediaGetPosition")]
		internal static extern int GetPosition(SafeMediaHandle THIS_PTR, ref long pllTime);

		// Token: 0x0600489A RID: 18586
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILMediaSetPosition")]
		internal static extern int SetPosition(SafeMediaHandle THIS_PTR, long llTime);

		// Token: 0x0600489B RID: 18587
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILMediaSetVolume")]
		internal static extern int SetVolume(SafeMediaHandle THIS_PTR, double dblVolume);

		// Token: 0x0600489C RID: 18588
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILMediaSetBalance")]
		internal static extern int SetBalance(SafeMediaHandle THIS_PTR, double dblBalance);

		// Token: 0x0600489D RID: 18589
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILMediaSetIsScrubbingEnabled")]
		internal static extern int SetIsScrubbingEnabled(SafeMediaHandle THIS_PTR, bool isScrubbingEnabled);

		// Token: 0x0600489E RID: 18590
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILMediaIsBuffering")]
		internal static extern int IsBuffering(SafeMediaHandle THIS_PTR, ref bool pIsBuffering);

		// Token: 0x0600489F RID: 18591
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILMediaCanPause")]
		internal static extern int CanPause(SafeMediaHandle THIS_PTR, ref bool pCanPause);

		// Token: 0x060048A0 RID: 18592
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILMediaGetDownloadProgress")]
		internal static extern int GetDownloadProgress(SafeMediaHandle THIS_PTR, ref double pProgress);

		// Token: 0x060048A1 RID: 18593
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILMediaGetBufferingProgress")]
		internal static extern int GetBufferingProgress(SafeMediaHandle THIS_PTR, ref double pProgress);

		// Token: 0x060048A2 RID: 18594
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILMediaSetRate")]
		internal static extern int SetRate(SafeMediaHandle THIS_PTR, double dblRate);

		// Token: 0x060048A3 RID: 18595
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILMediaHasVideo")]
		internal static extern int HasVideo(SafeMediaHandle THIS_PTR, ref bool pfHasVideo);

		// Token: 0x060048A4 RID: 18596
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILMediaHasAudio")]
		internal static extern int HasAudio(SafeMediaHandle THIS_PTR, ref bool pfHasAudio);

		// Token: 0x060048A5 RID: 18597
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILMediaGetNaturalHeight")]
		internal static extern int GetNaturalHeight(SafeMediaHandle THIS_PTR, ref uint puiHeight);

		// Token: 0x060048A6 RID: 18598
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILMediaGetNaturalWidth")]
		internal static extern int GetNaturalWidth(SafeMediaHandle THIS_PTR, ref uint puiWidth);

		// Token: 0x060048A7 RID: 18599
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILMediaGetMediaLength")]
		internal static extern int GetMediaLength(SafeMediaHandle THIS_PTR, ref long pllLength);

		// Token: 0x060048A8 RID: 18600
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILMediaNeedUIFrameUpdate")]
		internal static extern int NeedUIFrameUpdate(SafeMediaHandle THIS_PTR);

		// Token: 0x060048A9 RID: 18601
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILMediaShutdown")]
		internal static extern int Shutdown(IntPtr THIS_PTR);

		// Token: 0x060048AA RID: 18602
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILMediaProcessExitHandler")]
		internal static extern int ProcessExitHandler(SafeMediaHandle THIS_PTR);
	}
}
