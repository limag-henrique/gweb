using System;
using System.Collections.Generic;
using System.Security;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Define a paleta de cores disponíveis para um tipo de imagem com suporte.</summary>
	// Token: 0x020005E0 RID: 1504
	public sealed class BitmapPalette : DispatcherObject
	{
		// Token: 0x06004449 RID: 17481 RVA: 0x00109FD8 File Offset: 0x001093D8
		private BitmapPalette()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.BitmapPalette" /> com as cores especificadas.</summary>
		/// <param name="colors">As cores para adicionar à paleta personalizada.</param>
		/// <exception cref="T:System.ArgumentNullException">O parâmetro <paramref name="colors" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">O parâmetro <paramref name="colors" /> é menor que -1 ou maior que 256.</exception>
		// Token: 0x0600444A RID: 17482 RVA: 0x00109FFC File Offset: 0x001093FC
		public BitmapPalette(IList<Color> colors)
		{
			if (colors == null)
			{
				throw new ArgumentNullException("colors");
			}
			int count = colors.Count;
			if (count < 1 || count > 256)
			{
				throw new InvalidOperationException(SR.Get("Image_PaletteZeroColors", null));
			}
			Color[] array = new Color[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = colors[i];
			}
			this._colors = new PartialList<Color>(array);
			this._palette = BitmapPalette.CreateInternalPalette();
			this.UpdateUnmanaged();
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.BitmapPalette" /> com base no <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> especificado. O novo <see cref="T:System.Windows.Media.Imaging.BitmapPalette" /> é limitado a uma contagem máxima de cores especificada.</summary>
		/// <param name="bitmapSource">O bitmap de origem do qual a paleta é lida ou construída.</param>
		/// <param name="maxColorCount">O número máximo de cores que o novo <see cref="T:System.Windows.Media.Imaging.BitmapPalette" /> pode usar.</param>
		/// <exception cref="T:System.ArgumentNullException">O parâmetro <paramref name="bitmapSource" /> é <see langword="null" />.</exception>
		// Token: 0x0600444B RID: 17483 RVA: 0x0010A090 File Offset: 0x00109490
		[SecurityCritical]
		public BitmapPalette(BitmapSource bitmapSource, int maxColorCount)
		{
			if (bitmapSource == null)
			{
				throw new ArgumentNullException("bitmapSource");
			}
			SafeMILHandle wicSourceHandle = bitmapSource.WicSourceHandle;
			this._palette = BitmapPalette.CreateInternalPalette();
			object syncObject = bitmapSource.SyncObject;
			lock (syncObject)
			{
				HRESULT.Check(UnsafeNativeMethods.WICPalette.InitializeFromBitmap(this._palette, wicSourceHandle, maxColorCount, false));
			}
			this.UpdateManaged();
		}

		// Token: 0x0600444C RID: 17484 RVA: 0x0010A128 File Offset: 0x00109528
		[SecuritySafeCritical]
		internal BitmapPalette(WICPaletteType paletteType, bool addtransparentColor)
		{
			if (paletteType - WICPaletteType.WICPaletteTypeFixedBW > 10)
			{
				throw new ArgumentException(SR.Get("Image_PaletteFixedType", new object[]
				{
					paletteType
				}));
			}
			this._palette = BitmapPalette.CreateInternalPalette();
			HRESULT.Check(UnsafeNativeMethods.WICPalette.InitializePredefined(this._palette, paletteType, addtransparentColor));
			this.UpdateManaged();
		}

		// Token: 0x0600444D RID: 17485 RVA: 0x0010A194 File Offset: 0x00109594
		internal BitmapPalette(SafeMILHandle unmanagedPalette)
		{
			this._palette = unmanagedPalette;
			this.UpdateManaged();
		}

		// Token: 0x0600444E RID: 17486 RVA: 0x0010A1C4 File Offset: 0x001095C4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static BitmapPalette CreateFromBitmapSource(BitmapSource source)
		{
			SafeMILHandle wicSourceHandle = source.WicSourceHandle;
			SafeMILHandle safeMILHandle = BitmapPalette.CreateInternalPalette();
			object syncObject = source.SyncObject;
			lock (syncObject)
			{
				int num = UnsafeNativeMethods.WICBitmapSource.CopyPalette(wicSourceHandle, safeMILHandle);
				if (num != 0)
				{
					return null;
				}
			}
			WICPaletteType wicpaletteType;
			HRESULT.Check(UnsafeNativeMethods.WICPalette.GetType(safeMILHandle, out wicpaletteType));
			bool hasAlpha;
			HRESULT.Check(UnsafeNativeMethods.WICPalette.HasAlpha(safeMILHandle, out hasAlpha));
			BitmapPalette result;
			if (wicpaletteType == WICPaletteType.WICPaletteTypeCustom || wicpaletteType == WICPaletteType.WICPaletteTypeOptimal)
			{
				result = new BitmapPalette(safeMILHandle);
			}
			else
			{
				result = BitmapPalettes.FromMILPaletteType(wicpaletteType, hasAlpha);
			}
			return result;
		}

		/// <summary>Obtenha as cores definidas em uma paleta.</summary>
		/// <returns>A lista de cores definidas em uma paleta.</returns>
		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x0600444F RID: 17487 RVA: 0x0010A264 File Offset: 0x00109664
		public IList<Color> Colors
		{
			get
			{
				return this._colors;
			}
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06004450 RID: 17488 RVA: 0x0010A278 File Offset: 0x00109678
		internal SafeMILHandle InternalPalette
		{
			[SecurityCritical]
			get
			{
				if (this._palette == null || this._palette.IsInvalid)
				{
					this._palette = BitmapPalette.CreateInternalPalette();
				}
				return this._palette;
			}
		}

		// Token: 0x06004451 RID: 17489 RVA: 0x0010A2AC File Offset: 0x001096AC
		internal static bool DoesPaletteHaveAlpha(BitmapPalette palette)
		{
			if (palette != null)
			{
				foreach (Color color in palette.Colors)
				{
					if (color.A != 255)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06004452 RID: 17490 RVA: 0x0010A318 File Offset: 0x00109718
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static SafeMILHandle CreateInternalPalette()
		{
			SafeMILHandle result = null;
			using (FactoryMaker factoryMaker = new FactoryMaker())
			{
				HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreatePalette(factoryMaker.ImagingFactoryPtr, out result));
			}
			return result;
		}

		// Token: 0x06004453 RID: 17491 RVA: 0x0010A368 File Offset: 0x00109768
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private unsafe void UpdateUnmanaged()
		{
			int num = Math.Min(256, this._colors.Count);
			BitmapPalette.ImagePaletteColor[] array = new BitmapPalette.ImagePaletteColor[num];
			for (int i = 0; i < num; i++)
			{
				Color color = this._colors[i];
				array[i].B = color.B;
				array[i].G = color.G;
				array[i].R = color.R;
				array[i].A = color.A;
			}
			BitmapPalette.ImagePaletteColor[] array2;
			void* value;
			if ((array2 = array) == null || array2.Length == 0)
			{
				value = null;
			}
			else
			{
				value = (void*)(&array2[0]);
			}
			HRESULT.Check(UnsafeNativeMethods.WICPalette.InitializeCustom(this._palette, (IntPtr)value, num));
			array2 = null;
		}

		// Token: 0x06004454 RID: 17492 RVA: 0x0010A42C File Offset: 0x0010982C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private unsafe void UpdateManaged()
		{
			int num = 0;
			int num2 = 0;
			HRESULT.Check(UnsafeNativeMethods.WICPalette.GetColorCount(this._palette, out num));
			List<Color> list = new List<Color>();
			if (num < 1 || num > 256)
			{
				throw new InvalidOperationException(SR.Get("Image_PaletteZeroColors", null));
			}
			BitmapPalette.ImagePaletteColor[] array = new BitmapPalette.ImagePaletteColor[num];
			BitmapPalette.ImagePaletteColor[] array2;
			void* value;
			if ((array2 = array) == null || array2.Length == 0)
			{
				value = null;
			}
			else
			{
				value = (void*)(&array2[0]);
			}
			HRESULT.Check(UnsafeNativeMethods.WICPalette.GetColors(this._palette, num, (IntPtr)value, out num2));
			array2 = null;
			for (int i = 0; i < num; i++)
			{
				BitmapPalette.ImagePaletteColor imagePaletteColor = array[i];
				list.Add(Color.FromArgb(imagePaletteColor.A, imagePaletteColor.R, imagePaletteColor.G, imagePaletteColor.B));
			}
			this._colors = new PartialList<Color>(list);
		}

		// Token: 0x040018E2 RID: 6370
		private SafeMILHandle _palette;

		// Token: 0x040018E3 RID: 6371
		private IList<Color> _colors = new PartialList<Color>(new List<Color>());

		// Token: 0x020008D7 RID: 2263
		private struct ImagePaletteColor
		{
			// Token: 0x04002991 RID: 10641
			public byte B;

			// Token: 0x04002992 RID: 10642
			public byte G;

			// Token: 0x04002993 RID: 10643
			public byte R;

			// Token: 0x04002994 RID: 10644
			public byte A;
		}
	}
}
