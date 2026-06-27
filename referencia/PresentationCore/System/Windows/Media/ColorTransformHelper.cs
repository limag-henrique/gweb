using System;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media
{
	// Token: 0x02000375 RID: 885
	internal class ColorTransformHelper
	{
		// Token: 0x06001F9B RID: 8091 RVA: 0x00081780 File Offset: 0x00080B80
		internal ColorTransformHelper()
		{
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x00081794 File Offset: 0x00080B94
		[SecurityCritical]
		internal void CreateTransform(SafeProfileHandle sourceProfile, SafeProfileHandle destinationProfile)
		{
			if (sourceProfile == null || sourceProfile.IsInvalid)
			{
				throw new ArgumentNullException("sourceProfile");
			}
			if (destinationProfile == null || destinationProfile.IsInvalid)
			{
				throw new ArgumentNullException("destinationProfile");
			}
			IntPtr[] array = new IntPtr[2];
			bool flag = true;
			sourceProfile.DangerousAddRef(ref flag);
			destinationProfile.DangerousAddRef(ref flag);
			try
			{
				array[0] = sourceProfile.DangerousGetHandle();
				array[1] = destinationProfile.DangerousGetHandle();
				uint[] array2 = new uint[2];
				this._transformHandle = UnsafeNativeMethods.Mscms.CreateMultiProfileTransform(array, (uint)array.Length, array2, (uint)array2.Length, 131075U, 0U);
			}
			finally
			{
				sourceProfile.DangerousRelease();
				destinationProfile.DangerousRelease();
			}
			if (this._transformHandle == null || this._transformHandle.IsInvalid)
			{
				HRESULT.Check(Marshal.GetHRForLastWin32Error());
			}
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x00081864 File Offset: 0x00080C64
		[SecurityCritical]
		internal void TranslateColors(IntPtr paInputColors, uint numColors, uint inputColorType, IntPtr paOutputColors, uint outputColorType)
		{
			if (this._transformHandle == null || this._transformHandle.IsInvalid)
			{
				throw new InvalidOperationException(SR.Get("Image_ColorTransformInvalid"));
			}
			HRESULT.Check(UnsafeNativeMethods.Mscms.TranslateColors(this._transformHandle, paInputColors, numColors, inputColorType, paOutputColors, outputColorType));
		}

		// Token: 0x04001065 RID: 4197
		private ColorTransformHandle _transformHandle;

		// Token: 0x04001066 RID: 4198
		private const uint INTENT_PERCEPTUAL = 0U;

		// Token: 0x04001067 RID: 4199
		private const uint INTENT_RELATIVE_COLORIMETRIC = 1U;

		// Token: 0x04001068 RID: 4200
		private const uint INTENT_SATURATION = 2U;

		// Token: 0x04001069 RID: 4201
		private const uint INTENT_ABSOLUTE_COLORIMETRIC = 3U;

		// Token: 0x0400106A RID: 4202
		private const uint PROOF_MODE = 1U;

		// Token: 0x0400106B RID: 4203
		private const uint NORMAL_MODE = 2U;

		// Token: 0x0400106C RID: 4204
		private const uint BEST_MODE = 3U;

		// Token: 0x0400106D RID: 4205
		private const uint ENABLE_GAMUT_CHECKING = 65536U;

		// Token: 0x0400106E RID: 4206
		private const uint USE_RELATIVE_COLORIMETRIC = 131072U;

		// Token: 0x0400106F RID: 4207
		private const uint FAST_TRANSLATE = 262144U;
	}
}
