using System;
using System.Security;
using System.Windows.Media.Composition;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	// Token: 0x020005E6 RID: 1510
	internal class BitmapVisualManager : DispatcherObject
	{
		// Token: 0x0600451D RID: 17693 RVA: 0x0010D7B4 File Offset: 0x0010CBB4
		private BitmapVisualManager()
		{
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x0010D7C8 File Offset: 0x0010CBC8
		public BitmapVisualManager(RenderTargetBitmap bitmapTarget)
		{
			if (bitmapTarget == null)
			{
				throw new ArgumentNullException("bitmapTarget");
			}
			if (bitmapTarget.IsFrozen)
			{
				throw new ArgumentException(SR.Get("Image_CantBeFrozen", null));
			}
			this._bitmapTarget = bitmapTarget;
		}

		// Token: 0x0600451F RID: 17695 RVA: 0x0010D80C File Offset: 0x0010CC0C
		public void Render(Visual visual)
		{
			this.Render(visual, Matrix.Identity, Rect.Empty);
		}

		// Token: 0x06004520 RID: 17696 RVA: 0x0010D82C File Offset: 0x0010CC2C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal void Render(Visual visual, Matrix worldTransform, Rect windowClip)
		{
			if (visual == null)
			{
				throw new ArgumentNullException("visual");
			}
			if (this._bitmapTarget.IsFrozen)
			{
				throw new ArgumentException(SR.Get("Image_CantBeFrozen"));
			}
			int pixelWidth = this._bitmapTarget.PixelWidth;
			int pixelHeight = this._bitmapTarget.PixelHeight;
			double num = this._bitmapTarget.DpiX;
			double num2 = this._bitmapTarget.DpiY;
			if (pixelWidth <= 0 || pixelHeight <= 0)
			{
				return;
			}
			if (num <= 0.0 || num2 <= 0.0)
			{
				num = 96.0;
				num2 = 96.0;
			}
			SafeMILHandle milrenderTarget = this._bitmapTarget.MILRenderTarget;
			IntPtr zero = IntPtr.Zero;
			try
			{
				MediaContext currentMediaContext = MediaContext.CurrentMediaContext;
				DUCE.Channel channel = currentMediaContext.AllocateSyncChannel();
				Guid iid_IMILRenderTargetBitmap = MILGuidData.IID_IMILRenderTargetBitmap;
				HRESULT.Check(UnsafeNativeMethods.MILUnknown.QueryInterface(milrenderTarget, ref iid_IMILRenderTargetBitmap, out zero));
				Renderer.Render(zero, channel, visual, pixelWidth, pixelHeight, num, num2, worldTransform, windowClip);
				currentMediaContext.ReleaseSyncChannel(channel);
			}
			finally
			{
				UnsafeNativeMethods.MILUnknown.ReleaseInterface(ref zero);
			}
			this._bitmapTarget.RenderTargetContentsChanged();
		}

		// Token: 0x04001923 RID: 6435
		private RenderTargetBitmap _bitmapTarget;
	}
}
