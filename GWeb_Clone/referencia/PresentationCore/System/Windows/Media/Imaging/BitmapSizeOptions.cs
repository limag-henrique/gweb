using System;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Define atributos relacionados ao tamanho de uma imagem de bitmap em cache. Um bitmap é dimensionado com base nos valores definidos por essa classe.</summary>
	// Token: 0x020005E2 RID: 1506
	public class BitmapSizeOptions
	{
		// Token: 0x06004470 RID: 17520 RVA: 0x0010A7C0 File Offset: 0x00109BC0
		private BitmapSizeOptions()
		{
		}

		/// <summary>Obtém um valor que determina se a taxa de proporção da imagem de bitmap original é preservada.</summary>
		/// <returns>
		///   <see langword="true" /> Se a taxa de proporção original é mantida; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06004471 RID: 17521 RVA: 0x0010A7D4 File Offset: 0x00109BD4
		public bool PreservesAspectRatio
		{
			get
			{
				return this._preservesAspectRatio;
			}
		}

		/// <summary>A largura, em pixels, da imagem de bitmap.</summary>
		/// <returns>A largura do bitmap.</returns>
		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06004472 RID: 17522 RVA: 0x0010A7E8 File Offset: 0x00109BE8
		public int PixelWidth
		{
			get
			{
				return this._pixelWidth;
			}
		}

		/// <summary>A altura, em pixels, da imagem bitmap.</summary>
		/// <returns>A altura do bitmap.</returns>
		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06004473 RID: 17523 RVA: 0x0010A7FC File Offset: 0x00109BFC
		public int PixelHeight
		{
			get
			{
				return this._pixelHeight;
			}
		}

		/// <summary>Obtém um valor que representa o ângulo de rotação é aplicado a um bitmap.</summary>
		/// <returns>O ângulo de rotação é aplicado à imagem.</returns>
		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06004474 RID: 17524 RVA: 0x0010A810 File Offset: 0x00109C10
		public Rotation Rotation
		{
			get
			{
				return this._rotationAngle;
			}
		}

		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.Media.Imaging.BitmapSizeOptions" /> com propriedades de dimensionamento vazias.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapSizeOptions" />.</returns>
		// Token: 0x06004475 RID: 17525 RVA: 0x0010A824 File Offset: 0x00109C24
		public static BitmapSizeOptions FromEmptyOptions()
		{
			return new BitmapSizeOptions
			{
				_rotationAngle = Rotation.Rotate0,
				_preservesAspectRatio = true,
				_pixelHeight = 0,
				_pixelWidth = 0
			};
		}

		/// <summary>Inicializa uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapSizeOptions" /> que preserva a taxa de proporção do bitmap de origem e especifica um <see cref="P:System.Windows.Media.Imaging.BitmapSizeOptions.PixelHeight" /> inicial.</summary>
		/// <param name="pixelHeight">A altura, em pixels, do bitmap resultante.</param>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapSizeOptions" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre quando <paramref name="pixelHeight" /> é menor que zero.</exception>
		// Token: 0x06004476 RID: 17526 RVA: 0x0010A854 File Offset: 0x00109C54
		public static BitmapSizeOptions FromHeight(int pixelHeight)
		{
			if (pixelHeight <= 0)
			{
				throw new ArgumentOutOfRangeException("pixelHeight", SR.Get("ParameterMustBeGreaterThanZero"));
			}
			return new BitmapSizeOptions
			{
				_rotationAngle = Rotation.Rotate0,
				_preservesAspectRatio = true,
				_pixelHeight = pixelHeight,
				_pixelWidth = 0
			};
		}

		/// <summary>Inicializa uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapSizeOptions" /> que preserva a taxa de proporção do bitmap de origem e especifica um <see cref="P:System.Windows.Media.Imaging.BitmapSizeOptions.PixelWidth" /> inicial.</summary>
		/// <param name="pixelWidth">A largura, em pixels, do bitmap resultante.</param>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapSizeOptions" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre quando <paramref name="pixelWidth" /> é menor que zero.</exception>
		// Token: 0x06004477 RID: 17527 RVA: 0x0010A8A0 File Offset: 0x00109CA0
		public static BitmapSizeOptions FromWidth(int pixelWidth)
		{
			if (pixelWidth <= 0)
			{
				throw new ArgumentOutOfRangeException("pixelWidth", SR.Get("ParameterMustBeGreaterThanZero"));
			}
			return new BitmapSizeOptions
			{
				_rotationAngle = Rotation.Rotate0,
				_preservesAspectRatio = true,
				_pixelWidth = pixelWidth,
				_pixelHeight = 0
			};
		}

		/// <summary>Inicializa uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapSizeOptions" /> que não preserva a taxa de proporção original do bitmap.</summary>
		/// <param name="pixelWidth">A largura, em pixels, do bitmap resultante.</param>
		/// <param name="pixelHeight">A altura, em pixels, do bitmap resultante.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Imaging.BitmapSizeOptions" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre quando <paramref name="pixelHeight" /> é menor que zero.</exception>
		// Token: 0x06004478 RID: 17528 RVA: 0x0010A8EC File Offset: 0x00109CEC
		public static BitmapSizeOptions FromWidthAndHeight(int pixelWidth, int pixelHeight)
		{
			if (pixelWidth <= 0)
			{
				throw new ArgumentOutOfRangeException("pixelWidth", SR.Get("ParameterMustBeGreaterThanZero"));
			}
			if (pixelHeight <= 0)
			{
				throw new ArgumentOutOfRangeException("pixelHeight", SR.Get("ParameterMustBeGreaterThanZero"));
			}
			return new BitmapSizeOptions
			{
				_rotationAngle = Rotation.Rotate0,
				_preservesAspectRatio = false,
				_pixelWidth = pixelWidth,
				_pixelHeight = pixelHeight
			};
		}

		/// <summary>Inicializa uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapSizeOptions" /> que preserva a taxa de proporção do bitmap de origem e especifica um <see cref="T:System.Windows.Media.Imaging.Rotation" /> inicial a ser aplicado.</summary>
		/// <param name="rotation">O valor inicial de rotação a ser aplicado. Há suporte apenas para incrementos de 90 graus.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Imaging.BitmapSizeOptions" />.</returns>
		// Token: 0x06004479 RID: 17529 RVA: 0x0010A950 File Offset: 0x00109D50
		public static BitmapSizeOptions FromRotation(Rotation rotation)
		{
			if (rotation > Rotation.Rotate270)
			{
				throw new ArgumentException(SR.Get("Image_SizeOptionsAngle"), "rotation");
			}
			return new BitmapSizeOptions
			{
				_rotationAngle = rotation,
				_preservesAspectRatio = true,
				_pixelWidth = 0,
				_pixelHeight = 0
			};
		}

		// Token: 0x0600447A RID: 17530 RVA: 0x0010A99C File Offset: 0x00109D9C
		internal void GetScaledWidthAndHeight(uint width, uint height, out uint newWidth, out uint newHeight)
		{
			if (this._pixelWidth == 0 && this._pixelHeight != 0)
			{
				newWidth = (uint)((long)this._pixelHeight * (long)((ulong)width) / (long)((ulong)height));
				newHeight = (uint)this._pixelHeight;
				return;
			}
			if (this._pixelWidth != 0 && this._pixelHeight == 0)
			{
				newWidth = (uint)this._pixelWidth;
				newHeight = (uint)((long)this._pixelWidth * (long)((ulong)height) / (long)((ulong)width));
				return;
			}
			if (this._pixelWidth != 0 && this._pixelHeight != 0)
			{
				newWidth = (uint)this._pixelWidth;
				newHeight = (uint)this._pixelHeight;
				return;
			}
			newWidth = width;
			newHeight = height;
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x0600447B RID: 17531 RVA: 0x0010AA28 File Offset: 0x00109E28
		internal bool DoesScale
		{
			get
			{
				return this._pixelWidth != 0 || this._pixelHeight != 0;
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x0600447C RID: 17532 RVA: 0x0010AA48 File Offset: 0x00109E48
		internal WICBitmapTransformOptions WICTransformOptions
		{
			get
			{
				WICBitmapTransformOptions result = WICBitmapTransformOptions.WICBitmapTransformRotate0;
				switch (this._rotationAngle)
				{
				case Rotation.Rotate0:
					result = WICBitmapTransformOptions.WICBitmapTransformRotate0;
					break;
				case Rotation.Rotate90:
					result = WICBitmapTransformOptions.WICBitmapTransformRotate90;
					break;
				case Rotation.Rotate180:
					result = WICBitmapTransformOptions.WICBitmapTransformRotate180;
					break;
				case Rotation.Rotate270:
					result = WICBitmapTransformOptions.WICBitmapTransformRotate270;
					break;
				}
				return result;
			}
		}

		// Token: 0x040018E7 RID: 6375
		private bool _preservesAspectRatio;

		// Token: 0x040018E8 RID: 6376
		private int _pixelWidth;

		// Token: 0x040018E9 RID: 6377
		private int _pixelHeight;

		// Token: 0x040018EA RID: 6378
		private Rotation _rotationAngle;
	}
}
