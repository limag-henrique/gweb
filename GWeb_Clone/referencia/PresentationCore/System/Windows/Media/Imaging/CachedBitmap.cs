using System;
using System.Security;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Fornece a funcionalidade de cache para um <see cref="T:System.Windows.Media.Imaging.BitmapSource" />.</summary>
	// Token: 0x020005E7 RID: 1511
	public sealed class CachedBitmap : BitmapSource
	{
		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.Media.Imaging.CachedBitmap" /> que tem a origem especificada, as opções de criação de bitmap e a opção de cache de bitmap.</summary>
		/// <param name="source">O bitmap de origem que está sendo armazenado em cache.</param>
		/// <param name="createOptions">Opções de inicialização para a imagem bitmap.</param>
		/// <param name="cacheOption">Especifica como o bitmap é armazenada em cache na memória.</param>
		/// <exception cref="T:System.ArgumentNullException">Ocorre quando <paramref name="source" /> é <see langword="null" />.</exception>
		// Token: 0x06004521 RID: 17697 RVA: 0x0010D950 File Offset: 0x0010CD50
		public CachedBitmap(BitmapSource source, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption) : base(true)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.BeginInit();
			this._source = source;
			base.RegisterDownloadEventSource(this._source);
			this._createOptions = createOptions;
			this._cacheOption = cacheOption;
			this._syncObject = source.SyncObject;
			this.EndInit();
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x0010D9AC File Offset: 0x0010CDAC
		[SecurityCritical]
		internal CachedBitmap(int pixelWidth, int pixelHeight, double dpiX, double dpiY, PixelFormat pixelFormat, BitmapPalette palette, IntPtr buffer, int bufferSize, int stride) : base(true)
		{
			this.InitFromMemoryPtr(pixelWidth, pixelHeight, dpiX, dpiY, pixelFormat, palette, buffer, bufferSize, stride);
		}

		// Token: 0x06004523 RID: 17699 RVA: 0x0010D9D8 File Offset: 0x0010CDD8
		[SecurityCritical]
		internal CachedBitmap(BitmapSourceSafeMILHandle bitmap) : base(true)
		{
			if (bitmap == null)
			{
				throw new ArgumentNullException("bitmap");
			}
			this._bitmapInit.BeginInit();
			this._source = null;
			this._createOptions = BitmapCreateOptions.None;
			this._cacheOption = BitmapCacheOption.OnLoad;
			bitmap.CalculateSize();
			base.WicSourceHandle = bitmap;
			this._syncObject = base.WicSourceHandle;
			base.IsSourceCached = true;
			base.CreationCompleted = true;
			this._bitmapInit.EndInit();
		}

		// Token: 0x06004524 RID: 17700 RVA: 0x0010DA4C File Offset: 0x0010CE4C
		private CachedBitmap() : base(true)
		{
		}

		// Token: 0x06004525 RID: 17701 RVA: 0x0010DA60 File Offset: 0x0010CE60
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe CachedBitmap(int pixelWidth, int pixelHeight, double dpiX, double dpiY, PixelFormat pixelFormat, BitmapPalette palette, Array pixels, int stride) : base(true)
		{
			if (pixels == null)
			{
				throw new ArgumentNullException("pixels");
			}
			if (pixels.Rank != 1)
			{
				throw new ArgumentException(SR.Get("Collection_BadRank"), "pixels");
			}
			int num = -1;
			if (pixels is byte[])
			{
				num = 1;
			}
			else if (pixels is short[] || pixels is ushort[])
			{
				num = 2;
			}
			else if (pixels is int[] || pixels is uint[] || pixels is float[])
			{
				num = 4;
			}
			else if (pixels is double[])
			{
				num = 8;
			}
			if (num == -1)
			{
				throw new ArgumentException(SR.Get("Image_InvalidArrayForPixel"));
			}
			int bufferSize = num * pixels.Length;
			if (pixels is byte[])
			{
				byte[] array;
				void* value;
				if ((array = (byte[])pixels) == null || array.Length == 0)
				{
					value = null;
				}
				else
				{
					value = (void*)(&array[0]);
				}
				this.InitFromMemoryPtr(pixelWidth, pixelHeight, dpiX, dpiY, pixelFormat, palette, (IntPtr)value, bufferSize, stride);
				array = null;
				return;
			}
			if (pixels is short[])
			{
				short[] array2;
				void* value2;
				if ((array2 = (short[])pixels) == null || array2.Length == 0)
				{
					value2 = null;
				}
				else
				{
					value2 = (void*)(&array2[0]);
				}
				this.InitFromMemoryPtr(pixelWidth, pixelHeight, dpiX, dpiY, pixelFormat, palette, (IntPtr)value2, bufferSize, stride);
				array2 = null;
				return;
			}
			if (pixels is ushort[])
			{
				ushort[] array3;
				void* value3;
				if ((array3 = (ushort[])pixels) == null || array3.Length == 0)
				{
					value3 = null;
				}
				else
				{
					value3 = (void*)(&array3[0]);
				}
				this.InitFromMemoryPtr(pixelWidth, pixelHeight, dpiX, dpiY, pixelFormat, palette, (IntPtr)value3, bufferSize, stride);
				array3 = null;
				return;
			}
			if (pixels is int[])
			{
				int[] array4;
				void* value4;
				if ((array4 = (int[])pixels) == null || array4.Length == 0)
				{
					value4 = null;
				}
				else
				{
					value4 = (void*)(&array4[0]);
				}
				this.InitFromMemoryPtr(pixelWidth, pixelHeight, dpiX, dpiY, pixelFormat, palette, (IntPtr)value4, bufferSize, stride);
				array4 = null;
				return;
			}
			if (pixels is uint[])
			{
				uint[] array5;
				void* value5;
				if ((array5 = (uint[])pixels) == null || array5.Length == 0)
				{
					value5 = null;
				}
				else
				{
					value5 = (void*)(&array5[0]);
				}
				this.InitFromMemoryPtr(pixelWidth, pixelHeight, dpiX, dpiY, pixelFormat, palette, (IntPtr)value5, bufferSize, stride);
				array5 = null;
				return;
			}
			if (pixels is float[])
			{
				float[] array6;
				void* value6;
				if ((array6 = (float[])pixels) == null || array6.Length == 0)
				{
					value6 = null;
				}
				else
				{
					value6 = (void*)(&array6[0]);
				}
				this.InitFromMemoryPtr(pixelWidth, pixelHeight, dpiX, dpiY, pixelFormat, palette, (IntPtr)value6, bufferSize, stride);
				array6 = null;
				return;
			}
			if (pixels is double[])
			{
				double[] array7;
				void* value7;
				if ((array7 = (double[])pixels) == null || array7.Length == 0)
				{
					value7 = null;
				}
				else
				{
					value7 = (void*)(&array7[0]);
				}
				this.InitFromMemoryPtr(pixelWidth, pixelHeight, dpiX, dpiY, pixelFormat, palette, (IntPtr)value7, bufferSize, stride);
				array7 = null;
			}
		}

		// Token: 0x06004526 RID: 17702 RVA: 0x0010DD08 File Offset: 0x0010D108
		[SecurityCritical]
		private void CopyCommon(CachedBitmap sourceBitmap)
		{
			base.Animatable_IsResourceInvalidationNecessary = false;
			if (sourceBitmap._source != null)
			{
				this.BeginInit();
				this._syncObject = sourceBitmap._syncObject;
				this._source = sourceBitmap._source;
				base.RegisterDownloadEventSource(this._source);
				this._createOptions = sourceBitmap._createOptions;
				this._cacheOption = sourceBitmap._cacheOption;
				if (this._cacheOption == BitmapCacheOption.Default)
				{
					this._cacheOption = BitmapCacheOption.OnLoad;
				}
				this.EndInit();
			}
			else
			{
				this.InitFromWICSource(sourceBitmap.WicSourceHandle);
			}
			base.Animatable_IsResourceInvalidationNecessary = true;
		}

		// Token: 0x06004527 RID: 17703 RVA: 0x0010DD90 File Offset: 0x0010D190
		private void BeginInit()
		{
			this._bitmapInit.BeginInit();
		}

		// Token: 0x06004528 RID: 17704 RVA: 0x0010DDA8 File Offset: 0x0010D1A8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void EndInit()
		{
			this._bitmapInit.EndInit();
			if (!base.DelayCreation)
			{
				this.FinalizeCreation();
			}
		}

		// Token: 0x06004529 RID: 17705 RVA: 0x0010DDD0 File Offset: 0x0010D1D0
		[SecurityCritical]
		internal override void FinalizeCreation()
		{
			object syncObject = this._syncObject;
			lock (syncObject)
			{
				base.WicSourceHandle = BitmapSource.CreateCachedBitmap(this._source as BitmapFrame, this._source.WicSourceHandle, this._createOptions, this._cacheOption, this._source.Palette);
			}
			base.IsSourceCached = (this._cacheOption != BitmapCacheOption.None);
			base.CreationCompleted = true;
			this.UpdateCachedSettings();
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Imaging.CachedBitmap" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x0600452A RID: 17706 RVA: 0x0010DE70 File Offset: 0x0010D270
		public new CachedBitmap Clone()
		{
			return (CachedBitmap)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Imaging.CachedBitmap" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x0600452B RID: 17707 RVA: 0x0010DE88 File Offset: 0x0010D288
		public new CachedBitmap CloneCurrentValue()
		{
			return (CachedBitmap)base.CloneCurrentValue();
		}

		// Token: 0x0600452C RID: 17708 RVA: 0x0010DEA0 File Offset: 0x0010D2A0
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected override Freezable CreateInstanceCore()
		{
			return new CachedBitmap();
		}

		// Token: 0x0600452D RID: 17709 RVA: 0x0010DEB4 File Offset: 0x0010D2B4
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected override void CloneCore(Freezable sourceFreezable)
		{
			CachedBitmap sourceBitmap = (CachedBitmap)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(sourceBitmap);
		}

		// Token: 0x0600452E RID: 17710 RVA: 0x0010DED8 File Offset: 0x0010D2D8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			CachedBitmap sourceBitmap = (CachedBitmap)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(sourceBitmap);
		}

		// Token: 0x0600452F RID: 17711 RVA: 0x0010DEFC File Offset: 0x0010D2FC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			CachedBitmap sourceBitmap = (CachedBitmap)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			this.CopyCommon(sourceBitmap);
		}

		// Token: 0x06004530 RID: 17712 RVA: 0x0010DF20 File Offset: 0x0010D320
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			CachedBitmap sourceBitmap = (CachedBitmap)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			this.CopyCommon(sourceBitmap);
		}

		// Token: 0x06004531 RID: 17713 RVA: 0x0010DF44 File Offset: 0x0010D344
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

		// Token: 0x06004532 RID: 17714 RVA: 0x0010DFFC File Offset: 0x0010D3FC
		[SecurityCritical]
		private void InitFromMemoryPtr(int pixelWidth, int pixelHeight, double dpiX, double dpiY, PixelFormat pixelFormat, BitmapPalette palette, IntPtr buffer, int bufferSize, int stride)
		{
			if (pixelFormat.Palettized && palette == null)
			{
				throw new InvalidOperationException(SR.Get("Image_IndexedPixelFormatRequiresPalette"));
			}
			if (pixelFormat.Format == PixelFormatEnum.Default && pixelFormat.Guid == WICPixelFormatGUIDs.WICPixelFormatDontCare)
			{
				throw new ArgumentException(SR.Get("Effect_PixelFormat", new object[]
				{
					pixelFormat
				}), "pixelFormat");
			}
			this._bitmapInit.BeginInit();
			try
			{
				Guid guid = pixelFormat.Guid;
				BitmapSourceSafeMILHandle bitmapSourceSafeMILHandle;
				using (FactoryMaker factoryMaker = new FactoryMaker())
				{
					HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreateBitmapFromMemory(factoryMaker.ImagingFactoryPtr, (uint)pixelWidth, (uint)pixelHeight, ref guid, (uint)stride, (uint)bufferSize, buffer, out bitmapSourceSafeMILHandle));
					bitmapSourceSafeMILHandle.CalculateSize();
				}
				HRESULT.Check(UnsafeNativeMethods.WICBitmap.SetResolution(bitmapSourceSafeMILHandle, dpiX, dpiY));
				if (pixelFormat.Palettized)
				{
					HRESULT.Check(UnsafeNativeMethods.WICBitmap.SetPalette(bitmapSourceSafeMILHandle, palette.InternalPalette));
				}
				base.WicSourceHandle = bitmapSourceSafeMILHandle;
				this._isSourceCached = true;
			}
			catch
			{
				this._bitmapInit.Reset();
				throw;
			}
			this._createOptions = BitmapCreateOptions.PreservePixelFormat;
			this._cacheOption = BitmapCacheOption.OnLoad;
			this._syncObject = base.WicSourceHandle;
			this._bitmapInit.EndInit();
			this.UpdateCachedSettings();
		}

		// Token: 0x04001924 RID: 6436
		private BitmapSource _source;

		// Token: 0x04001925 RID: 6437
		private BitmapCreateOptions _createOptions;

		// Token: 0x04001926 RID: 6438
		private BitmapCacheOption _cacheOption;
	}
}
