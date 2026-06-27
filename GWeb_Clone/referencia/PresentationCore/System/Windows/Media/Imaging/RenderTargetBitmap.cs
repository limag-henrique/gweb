using System;
using System.Security;
using System.Windows.Media.Composition;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Converte um objeto <see cref="T:System.Windows.Media.Visual" /> em um bitmap.</summary>
	// Token: 0x020005F9 RID: 1529
	public sealed class RenderTargetBitmap : BitmapSource
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.RenderTargetBitmap" /> que tem os parâmetros especificados.</summary>
		/// <param name="pixelWidth">A largura do bitmap.</param>
		/// <param name="pixelHeight">A altura do bitmap.</param>
		/// <param name="dpiX">O DPI horizontal do bitmap.</param>
		/// <param name="dpiY">O DPI vertical do bitmap.</param>
		/// <param name="pixelFormat">O formato do bitmap.</param>
		// Token: 0x060045F2 RID: 17906 RVA: 0x0011183C File Offset: 0x00110C3C
		[SecurityCritical]
		public RenderTargetBitmap(int pixelWidth, int pixelHeight, double dpiX, double dpiY, PixelFormat pixelFormat) : base(true)
		{
			if (pixelFormat.Format == PixelFormatEnum.Default)
			{
				pixelFormat = PixelFormats.Pbgra32;
			}
			else if (pixelFormat.Format != PixelFormatEnum.Pbgra32)
			{
				throw new ArgumentException(SR.Get("Effect_PixelFormat", new object[]
				{
					pixelFormat
				}), "pixelFormat");
			}
			if (pixelWidth <= 0)
			{
				throw new ArgumentOutOfRangeException("pixelWidth", SR.Get("ParameterMustBeGreaterThanZero"));
			}
			if (pixelHeight <= 0)
			{
				throw new ArgumentOutOfRangeException("pixelHeight", SR.Get("ParameterMustBeGreaterThanZero"));
			}
			if (dpiX < 2.2204460492503131E-16)
			{
				dpiX = 96.0;
			}
			if (dpiY < 2.2204460492503131E-16)
			{
				dpiY = 96.0;
			}
			this._bitmapInit.BeginInit();
			this._pixelWidth = pixelWidth;
			this._pixelHeight = pixelHeight;
			this._dpiX = dpiX;
			this._dpiY = dpiY;
			this._format = pixelFormat;
			this._bitmapInit.EndInit();
			this.FinalizeCreation();
		}

		// Token: 0x060045F3 RID: 17907 RVA: 0x00111934 File Offset: 0x00110D34
		internal RenderTargetBitmap() : base(true)
		{
		}

		// Token: 0x060045F4 RID: 17908 RVA: 0x00111948 File Offset: 0x00110D48
		protected override Freezable CreateInstanceCore()
		{
			return new RenderTargetBitmap();
		}

		// Token: 0x060045F5 RID: 17909 RVA: 0x0011195C File Offset: 0x00110D5C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void CopyCommon(RenderTargetBitmap sourceBitmap)
		{
			this._bitmapInit.BeginInit();
			this._pixelWidth = sourceBitmap._pixelWidth;
			this._pixelHeight = sourceBitmap._pixelHeight;
			this._dpiX = sourceBitmap._dpiX;
			this._dpiY = sourceBitmap._dpiY;
			this._format = sourceBitmap._format;
			using (FactoryMaker factoryMaker = new FactoryMaker())
			{
				BitmapSourceSafeMILHandle bitmapSourceSafeMILHandle = BitmapSource.CreateCachedBitmap(null, base.WicSourceHandle, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad, null);
				object syncObject = this._syncObject;
				lock (syncObject)
				{
					base.WicSourceHandle = bitmapSourceSafeMILHandle;
				}
				HRESULT.Check(UnsafeNativeMethods.MILFactory2.CreateBitmapRenderTargetForBitmap(factoryMaker.FactoryPtr, bitmapSourceSafeMILHandle, out this._renderTargetBitmap));
			}
			this._bitmapInit.EndInit();
		}

		// Token: 0x060045F6 RID: 17910 RVA: 0x00111A4C File Offset: 0x00110E4C
		protected override void CloneCore(Freezable sourceFreezable)
		{
			RenderTargetBitmap sourceBitmap = (RenderTargetBitmap)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(sourceBitmap);
		}

		// Token: 0x060045F7 RID: 17911 RVA: 0x00111A70 File Offset: 0x00110E70
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			RenderTargetBitmap sourceBitmap = (RenderTargetBitmap)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(sourceBitmap);
		}

		// Token: 0x060045F8 RID: 17912 RVA: 0x00111A94 File Offset: 0x00110E94
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			RenderTargetBitmap sourceBitmap = (RenderTargetBitmap)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			this.CopyCommon(sourceBitmap);
		}

		// Token: 0x060045F9 RID: 17913 RVA: 0x00111AB8 File Offset: 0x00110EB8
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			RenderTargetBitmap sourceBitmap = (RenderTargetBitmap)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			this.CopyCommon(sourceBitmap);
		}

		/// <summary>Renderiza o objeto <see cref="T:System.Windows.Media.Visual" />.</summary>
		/// <param name="visual">O objeto <see cref="T:System.Windows.Media.Visual" /> a ser usado como um bitmap.</param>
		// Token: 0x060045FA RID: 17914 RVA: 0x00111ADC File Offset: 0x00110EDC
		public void Render(Visual visual)
		{
			BitmapVisualManager bitmapVisualManager = new BitmapVisualManager(this);
			bitmapVisualManager.Render(visual);
		}

		/// <summary>Limpa o destino de renderização e define todos os pixels para preto transparente.</summary>
		// Token: 0x060045FB RID: 17915 RVA: 0x00111AF8 File Offset: 0x00110EF8
		[SecurityCritical]
		public void Clear()
		{
			HRESULT.Check(MILRenderTargetBitmap.Clear(this._renderTargetBitmap));
			this.RenderTargetContentsChanged();
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x060045FC RID: 17916 RVA: 0x00111B1C File Offset: 0x00110F1C
		internal SafeMILHandle MILRenderTarget
		{
			[SecurityCritical]
			get
			{
				return this._renderTargetBitmap;
			}
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x00111B30 File Offset: 0x00110F30
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal void RenderTargetContentsChanged()
		{
			this._isSourceCached = false;
			if (this._convertedDUCEPtr != null)
			{
				this._convertedDUCEPtr.Close();
				this._convertedDUCEPtr = null;
			}
			base.RegisterForAsyncUpdateResource();
			base.FireChanged();
		}

		// Token: 0x060045FE RID: 17918 RVA: 0x00111B6C File Offset: 0x00110F6C
		[SecurityCritical]
		internal override void FinalizeCreation()
		{
			try
			{
				using (FactoryMaker factoryMaker = new FactoryMaker())
				{
					SafeMILHandle safeMILHandle = null;
					HRESULT.Check(UnsafeNativeMethods.MILFactory2.CreateBitmapRenderTarget(factoryMaker.FactoryPtr, (uint)this._pixelWidth, (uint)this._pixelHeight, this._format.Format, (float)this._dpiX, (float)this._dpiY, MILRTInitializationFlags.MIL_RT_INITIALIZE_DEFAULT, out safeMILHandle));
					BitmapSourceSafeMILHandle bitmapSourceSafeMILHandle = null;
					HRESULT.Check(MILRenderTargetBitmap.GetBitmap(safeMILHandle, out bitmapSourceSafeMILHandle));
					object syncObject = this._syncObject;
					lock (syncObject)
					{
						this._renderTargetBitmap = safeMILHandle;
						bitmapSourceSafeMILHandle.CalculateSize();
						base.WicSourceHandle = bitmapSourceSafeMILHandle;
						this._isSourceCached = false;
					}
				}
				base.CreationCompleted = true;
				this.UpdateCachedSettings();
			}
			catch
			{
				this._bitmapInit.Reset();
				throw;
			}
		}

		// Token: 0x04001972 RID: 6514
		[SecurityCritical]
		private SafeMILHandle _renderTargetBitmap;
	}
}
