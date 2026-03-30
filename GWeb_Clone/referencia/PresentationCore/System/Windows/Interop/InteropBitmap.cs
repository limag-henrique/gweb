using System;
using System.Security;
using System.Windows.Media;
using System.Windows.Media.Composition;
using System.Windows.Media.Imaging;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Interop
{
	/// <summary>
	///   <see cref="T:System.Windows.Interop.InteropBitmap" /> habilita os desenvolvedores a melhorar o desempenho de renderização de interfaces do usuário que não sejam do WPF, hospedadas pelo WPF em cenários de interoperabilidade.</summary>
	// Token: 0x02000333 RID: 819
	public sealed class InteropBitmap : BitmapSource
	{
		// Token: 0x06001BDB RID: 7131 RVA: 0x00070D60 File Offset: 0x00070160
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private InteropBitmap() : base(true)
		{
			SecurityHelper.DemandUnmanagedCode();
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x00070D84 File Offset: 0x00070184
		[SecurityCritical]
		internal InteropBitmap(IntPtr hbitmap, IntPtr hpalette, Int32Rect sourceRect, BitmapSizeOptions sizeOptions, WICBitmapAlphaChannelOption alphaOptions) : base(true)
		{
			this._bitmapInit.BeginInit();
			using (FactoryMaker factoryMaker = new FactoryMaker())
			{
				HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreateBitmapFromHBITMAP(factoryMaker.ImagingFactoryPtr, hbitmap, hpalette, alphaOptions, out this._unmanagedSource));
			}
			this._unmanagedSource.CalculateSize();
			this._sizeOptions = sizeOptions;
			this._sourceRect = sourceRect;
			this._syncObject = this._unmanagedSource;
			this._bitmapInit.EndInit();
			this.FinalizeCreation();
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x00070E30 File Offset: 0x00070230
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal InteropBitmap(IntPtr hicon, Int32Rect sourceRect, BitmapSizeOptions sizeOptions) : base(true)
		{
			SecurityHelper.DemandUnmanagedCode();
			this._bitmapInit.BeginInit();
			using (FactoryMaker factoryMaker = new FactoryMaker())
			{
				HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreateBitmapFromHICON(factoryMaker.ImagingFactoryPtr, hicon, out this._unmanagedSource));
			}
			this._unmanagedSource.CalculateSize();
			this._sourceRect = sourceRect;
			this._sizeOptions = sizeOptions;
			this._syncObject = this._unmanagedSource;
			this._bitmapInit.EndInit();
			this.FinalizeCreation();
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x00070EDC File Offset: 0x000702DC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal InteropBitmap(IntPtr section, int pixelWidth, int pixelHeight, PixelFormat format, int stride, int offset) : base(true)
		{
			SecurityHelper.DemandUnmanagedCode();
			this._bitmapInit.BeginInit();
			if (pixelWidth <= 0)
			{
				throw new ArgumentOutOfRangeException("pixelWidth", SR.Get("ParameterMustBeGreaterThanZero"));
			}
			if (pixelHeight <= 0)
			{
				throw new ArgumentOutOfRangeException("pixelHeight", SR.Get("ParameterMustBeGreaterThanZero"));
			}
			Guid guid = format.Guid;
			HRESULT.Check(UnsafeNativeMethods.WindowsCodecApi.CreateBitmapFromSection((uint)pixelWidth, (uint)pixelHeight, ref guid, section, (uint)stride, (uint)offset, out this._unmanagedSource));
			this._unmanagedSource.CalculateSize();
			this._sourceRect = Int32Rect.Empty;
			this._sizeOptions = null;
			this._syncObject = this._unmanagedSource;
			this._bitmapInit.EndInit();
			this.FinalizeCreation();
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x00070F98 File Offset: 0x00070398
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected override Freezable CreateInstanceCore()
		{
			return new InteropBitmap();
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x00070FAC File Offset: 0x000703AC
		[SecurityCritical]
		private void CopyCommon(InteropBitmap sourceBitmapSource)
		{
			base.Animatable_IsResourceInvalidationNecessary = false;
			this._unmanagedSource = sourceBitmapSource._unmanagedSource;
			this._sourceRect = sourceBitmapSource._sourceRect;
			this._sizeOptions = sourceBitmapSource._sizeOptions;
			this.InitFromWICSource(sourceBitmapSource.WicSourceHandle);
			base.Animatable_IsResourceInvalidationNecessary = true;
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x00070FF8 File Offset: 0x000703F8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected override void CloneCore(Freezable sourceFreezable)
		{
			InteropBitmap sourceBitmapSource = (InteropBitmap)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(sourceBitmapSource);
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x0007101C File Offset: 0x0007041C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			InteropBitmap sourceBitmapSource = (InteropBitmap)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(sourceBitmapSource);
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x00071040 File Offset: 0x00070440
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			InteropBitmap sourceBitmapSource = (InteropBitmap)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			this.CopyCommon(sourceBitmapSource);
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x00071064 File Offset: 0x00070464
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			InteropBitmap sourceBitmapSource = (InteropBitmap)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			this.CopyCommon(sourceBitmapSource);
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x00071088 File Offset: 0x00070488
		[SecurityCritical]
		private void InitFromWICSource(SafeMILHandle wicSource)
		{
			this._bitmapInit.BeginInit();
			BitmapSourceSafeMILHandle bitmapSourceSafeMILHandle = null;
			object syncObject = this._syncObject;
			lock (syncObject)
			{
				using (FactoryMaker factoryMaker = new FactoryMaker())
				{
					HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreateBitmapFromSource(factoryMaker.ImagingFactoryPtr, wicSource, WICBitmapCreateCacheOptions.WICBitmapCacheOnLoad, out bitmapSourceSafeMILHandle));
				}
				bitmapSourceSafeMILHandle.CalculateSize();
			}
			base.WicSourceHandle = bitmapSourceSafeMILHandle;
			this._isSourceCached = true;
			this._bitmapInit.EndInit();
			this.UpdateCachedSettings();
		}

		/// <summary>Força a Interface de Usuário hospedada que não seja do WPF a ser renderizada.</summary>
		/// <exception cref="T:System.InvalidOperationException">A instância do <see cref="T:System.Windows.Interop.InteropBitmap" /> está congelada e não pode ter seus membros gravados.</exception>
		// Token: 0x06001BE6 RID: 7142 RVA: 0x00071140 File Offset: 0x00070540
		[SecurityCritical]
		public void Invalidate()
		{
			this.Invalidate(null);
		}

		/// <summary>Força a área especificada da Interface de Usuário hospedada que não seja do WPF a ser renderizada.</summary>
		/// <param name="dirtyRect">A área da Interface de Usuário hospedada que não seja do WPF a ser renderizada. Se esse parâmetro for null, toda a Interface de Usuário que não seja do WPF será renderizada.</param>
		/// <exception cref="T:System.InvalidOperationException">A instância do <see cref="T:System.Windows.Interop.InteropBitmap" /> está congelada e não pode ter seus membros gravados.</exception>
		// Token: 0x06001BE7 RID: 7143 RVA: 0x0007115C File Offset: 0x0007055C
		[SecurityCritical]
		public unsafe void Invalidate(Int32Rect? dirtyRect)
		{
			SecurityHelper.DemandUnmanagedCode();
			if (dirtyRect != null)
			{
				dirtyRect.Value.ValidateForDirtyRect("dirtyRect", this._pixelWidth, this._pixelHeight);
				if (!dirtyRect.Value.HasArea)
				{
					return;
				}
			}
			base.WritePreamble();
			if (this._unmanagedSource != null)
			{
				if (base.UsableWithoutCache)
				{
					int i = 0;
					int channelCount = this._duceResource.GetChannelCount();
					while (i < channelCount)
					{
						DUCE.Channel channel = this._duceResource.GetChannel(i);
						DUCE.MILCMD_BITMAP_INVALIDATE milcmd_BITMAP_INVALIDATE;
						milcmd_BITMAP_INVALIDATE.Type = MILCMD.MilCmdBitmapInvalidate;
						milcmd_BITMAP_INVALIDATE.Handle = this._duceResource.GetHandle(channel);
						bool flag = dirtyRect != null;
						if (flag)
						{
							milcmd_BITMAP_INVALIDATE.DirtyRect.left = dirtyRect.Value.X;
							milcmd_BITMAP_INVALIDATE.DirtyRect.top = dirtyRect.Value.Y;
							milcmd_BITMAP_INVALIDATE.DirtyRect.right = dirtyRect.Value.X + dirtyRect.Value.Width;
							milcmd_BITMAP_INVALIDATE.DirtyRect.bottom = dirtyRect.Value.Y + dirtyRect.Value.Height;
						}
						milcmd_BITMAP_INVALIDATE.UseDirtyRect = (flag ? 1U : 0U);
						channel.SendCommand((byte*)(&milcmd_BITMAP_INVALIDATE), sizeof(DUCE.MILCMD_BITMAP_INVALIDATE));
						i++;
					}
				}
				else
				{
					this._needsUpdate = true;
					base.RegisterForAsyncUpdateResource();
				}
			}
			base.WritePostscript();
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x000712DC File Offset: 0x000706DC
		[SecurityCritical]
		internal override void FinalizeCreation()
		{
			BitmapSourceSafeMILHandle bitmapSourceSafeMILHandle = null;
			BitmapSourceSafeMILHandle bitmapSourceSafeMILHandle2 = null;
			BitmapSourceSafeMILHandle bitmapSourceSafeMILHandle3 = this._unmanagedSource;
			HRESULT.Check(UnsafeNativeMethods.WICBitmap.SetResolution(this._unmanagedSource, 96.0, 96.0));
			using (FactoryMaker factoryMaker = new FactoryMaker())
			{
				IntPtr imagingFactoryPtr = factoryMaker.ImagingFactoryPtr;
				if (!this._sourceRect.IsEmpty)
				{
					HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreateBitmapClipper(imagingFactoryPtr, out bitmapSourceSafeMILHandle));
					object syncObject = this._syncObject;
					lock (syncObject)
					{
						HRESULT.Check(UnsafeNativeMethods.WICBitmapClipper.Initialize(bitmapSourceSafeMILHandle, bitmapSourceSafeMILHandle3, ref this._sourceRect));
					}
					bitmapSourceSafeMILHandle3 = bitmapSourceSafeMILHandle;
				}
				if (this._sizeOptions != null)
				{
					if (this._sizeOptions.DoesScale)
					{
						uint width;
						uint height;
						this._sizeOptions.GetScaledWidthAndHeight((uint)this._sizeOptions.PixelWidth, (uint)this._sizeOptions.PixelHeight, out width, out height);
						HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreateBitmapScaler(imagingFactoryPtr, out bitmapSourceSafeMILHandle2));
						object syncObject2 = this._syncObject;
						lock (syncObject2)
						{
							HRESULT.Check(UnsafeNativeMethods.WICBitmapScaler.Initialize(bitmapSourceSafeMILHandle2, bitmapSourceSafeMILHandle3, width, height, WICInterpolationMode.Fant));
							goto IL_15D;
						}
					}
					if (this._sizeOptions.Rotation != Rotation.Rotate0)
					{
						HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreateBitmapFlipRotator(imagingFactoryPtr, out bitmapSourceSafeMILHandle2));
						object syncObject3 = this._syncObject;
						lock (syncObject3)
						{
							HRESULT.Check(UnsafeNativeMethods.WICBitmapFlipRotator.Initialize(bitmapSourceSafeMILHandle2, bitmapSourceSafeMILHandle3, this._sizeOptions.WICTransformOptions));
						}
					}
					IL_15D:
					if (bitmapSourceSafeMILHandle2 != null)
					{
						bitmapSourceSafeMILHandle3 = bitmapSourceSafeMILHandle2;
					}
				}
				base.WicSourceHandle = bitmapSourceSafeMILHandle3;
				this._isSourceCached = true;
			}
			base.CreationCompleted = true;
			this.UpdateCachedSettings();
		}

		// Token: 0x04000EE7 RID: 3815
		[SecurityCritical]
		private BitmapSourceSafeMILHandle _unmanagedSource;

		// Token: 0x04000EE8 RID: 3816
		private Int32Rect _sourceRect = Int32Rect.Empty;

		// Token: 0x04000EE9 RID: 3817
		private BitmapSizeOptions _sizeOptions;
	}
}
