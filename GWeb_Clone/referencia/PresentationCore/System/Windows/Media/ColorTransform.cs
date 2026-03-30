using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Windows.Media
{
	// Token: 0x02000373 RID: 883
	internal class ColorTransform
	{
		// Token: 0x06001F92 RID: 8082 RVA: 0x00081388 File Offset: 0x00080788
		private ColorTransform()
		{
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x0008139C File Offset: 0x0008079C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal ColorTransform(ColorContext srcContext, ColorContext dstContext)
		{
			this.InitializeICM();
			if (srcContext == null)
			{
				srcContext = new ColorContext(PixelFormats.Bgra32);
			}
			if (dstContext == null)
			{
				dstContext = new ColorContext(PixelFormats.Bgra32);
			}
			this._inputColorType = srcContext.ColorType;
			this._outputColorType = dstContext.ColorType;
			this._colorTransformHelper.CreateTransform(srcContext.ProfileHandle, dstContext.ProfileHandle);
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x00081410 File Offset: 0x00080810
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal ColorTransform(SafeMILHandle bitmapSource, ColorContext srcContext, ColorContext dstContext, PixelFormat pixelFormat)
		{
			this.InitializeICM();
			if (srcContext == null)
			{
				srcContext = new ColorContext(pixelFormat);
			}
			if (dstContext == null)
			{
				dstContext = new ColorContext(pixelFormat);
			}
			this._inputColorType = srcContext.ColorType;
			this._outputColorType = dstContext.ColorType;
			if (srcContext.ProfileHandle != null && !srcContext.ProfileHandle.IsInvalid && dstContext.ProfileHandle != null && !dstContext.ProfileHandle.IsInvalid)
			{
				this._colorTransformHelper.CreateTransform(srcContext.ProfileHandle, dstContext.ProfileHandle);
			}
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x000814A8 File Offset: 0x000808A8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal void Translate(float[] srcValue, float[] dstValue)
		{
			IntPtr[] array = new IntPtr[2];
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			try
			{
				uint numColors = 1U;
				long val = this.ICM2Color(srcValue);
				intPtr = Marshal.AllocHGlobal(64);
				Marshal.WriteInt64(intPtr, val);
				intPtr2 = Marshal.AllocHGlobal(64);
				long num = 0L;
				Marshal.WriteInt64(intPtr2, num);
				this._colorTransformHelper.TranslateColors(intPtr, numColors, this._inputColorType, intPtr2, this._outputColorType);
				num = Marshal.ReadInt64(intPtr2);
				for (int i = 0; i < dstValue.GetLength(0); i++)
				{
					uint num2 = 65535U & (uint)(num >> 16 * i);
					float num3 = (num2 & 2147483647U) / 65536f;
					if (num2 < 0U)
					{
						dstValue[i] = -num3;
					}
					else
					{
						dstValue[i] = num3;
					}
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
				Marshal.FreeHGlobal(intPtr2);
			}
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x0008158C File Offset: 0x0008098C
		[SecurityCritical]
		private void InitializeICM()
		{
			this._colorTransformHelper = new ColorTransformHelper();
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x000815A4 File Offset: 0x000809A4
		private long ICM2Color(float[] srcValue)
		{
			if (srcValue.GetLength(0) < 3 || srcValue.GetLength(0) > 8)
			{
				throw new NotSupportedException();
			}
			long result;
			if (srcValue.GetLength(0) <= 4)
			{
				ushort[] array = new ushort[4];
				array[0] = (array[1] = (array[2] = (array[3] = 0)));
				for (int i = 0; i < srcValue.GetLength(0); i++)
				{
					if ((double)srcValue[i] >= 1.0)
					{
						array[i] = ushort.MaxValue;
					}
					else if ((double)srcValue[i] <= 0.0)
					{
						array[i] = 0;
					}
					else
					{
						array[i] = (ushort)(srcValue[i] * 65535f);
					}
				}
				result = (long)(((ulong)array[3] << 48) + ((ulong)array[2] << 32) + ((ulong)array[1] << 16) + (ulong)array[0]);
			}
			else
			{
				byte[] array2 = new byte[8];
				array2[0] = (array2[1] = (array2[2] = (array2[3] = (array2[4] = (array2[5] = (array2[6] = (array2[7] = 0)))))));
				for (int j = 0; j < srcValue.GetLength(0); j++)
				{
					if ((double)srcValue[j] >= 1.0)
					{
						array2[j] = byte.MaxValue;
					}
					else if ((double)srcValue[j] <= 0.0)
					{
						array2[j] = 0;
					}
					else
					{
						array2[j] = (byte)(srcValue[j] * 255f);
					}
				}
				result = (long)(((ulong)array2[7] << 56) + ((ulong)array2[6] << 48) + ((ulong)array2[5] << 40) + ((ulong)array2[4] << 32) + ((ulong)array2[3] << 24) + ((ulong)array2[2] << 16) + ((ulong)array2[1] << 8) + (ulong)array2[0]);
			}
			return result;
		}

		// Token: 0x04001062 RID: 4194
		[SecurityCritical]
		private ColorTransformHelper _colorTransformHelper;

		// Token: 0x04001063 RID: 4195
		private uint _inputColorType;

		// Token: 0x04001064 RID: 4196
		private uint _outputColorType;
	}
}
