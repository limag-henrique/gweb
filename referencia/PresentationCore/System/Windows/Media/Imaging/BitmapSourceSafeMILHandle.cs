using System;
using System.Security;
using MS.Internal;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	// Token: 0x020005E4 RID: 1508
	internal class BitmapSourceSafeMILHandle : SafeMILHandle
	{
		// Token: 0x060044CF RID: 17615 RVA: 0x0010C200 File Offset: 0x0010B600
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal BitmapSourceSafeMILHandle()
		{
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x0010C214 File Offset: 0x0010B614
		[SecurityCritical]
		internal BitmapSourceSafeMILHandle(IntPtr handle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x060044D1 RID: 17617 RVA: 0x0010C230 File Offset: 0x0010B630
		[SecurityCritical]
		internal BitmapSourceSafeMILHandle(IntPtr handle, SafeMILHandle copyMemoryPressureFrom) : this(handle)
		{
			base.CopyMemoryPressure(copyMemoryPressureFrom);
		}

		// Token: 0x060044D2 RID: 17618 RVA: 0x0010C24C File Offset: 0x0010B64C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal void CalculateSize()
		{
			base.UpdateEstimatedSize(BitmapSourceSafeMILHandle.ComputeEstimatedSize(this.handle));
		}

		// Token: 0x060044D3 RID: 17619 RVA: 0x0010C26C File Offset: 0x0010B66C
		[SecurityCritical]
		private static long ComputeEstimatedSize(IntPtr bitmapObject)
		{
			long result = 0L;
			IntPtr handle;
			if (bitmapObject != IntPtr.Zero && UnsafeNativeMethods.MILUnknown.QueryInterface(bitmapObject, ref BitmapSourceSafeMILHandle._uuidBitmap, out handle) == 0)
			{
				SafeMILHandle this_PTR = new SafeMILHandle(handle);
				uint num = 0U;
				uint num2 = 0U;
				Guid guidPixelFormat;
				if (UnsafeNativeMethods.WICBitmapSource.GetSize(this_PTR, out num, out num2) == 0 && UnsafeNativeMethods.WICBitmapSource.GetPixelFormat(this_PTR, out guidPixelFormat) == 0)
				{
					PixelFormat pixelFormat = new PixelFormat(guidPixelFormat);
					long num3 = (long)((ulong)num * (ulong)((long)pixelFormat.InternalBitsPerPixel) / 8UL);
					if (num3 < 1073741824L)
					{
						result = (long)((ulong)num2 * (ulong)num3);
					}
				}
			}
			return result;
		}

		// Token: 0x060044D4 RID: 17620 RVA: 0x0010C2EC File Offset: 0x0010B6EC
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return base.ReleaseHandle();
		}

		// Token: 0x04001902 RID: 6402
		[SecurityCritical]
		private static Guid _uuidBitmap = MILGuidData.IID_IWICBitmapSource;
	}
}
