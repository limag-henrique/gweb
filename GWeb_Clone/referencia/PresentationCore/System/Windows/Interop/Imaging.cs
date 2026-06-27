using System;
using System.Security;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MS.Internal;

namespace System.Windows.Interop
{
	/// <summary>Oferece suporte à interoperação gerenciada para não gerenciada para criar objetos de imagem.</summary>
	// Token: 0x02000334 RID: 820
	public static class Imaging
	{
		/// <summary>Retorna um <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> gerenciado, com base no ponteiro fornecido para obter informações de bitmap e paleta não gerenciadas.</summary>
		/// <param name="bitmap">Um ponteiro para o bitmap não gerenciado.</param>
		/// <param name="palette">Um ponteiro para o mapa da paleta do bitmap.</param>
		/// <param name="sourceRect">O tamanho da imagem de origem.</param>
		/// <param name="sizeOptions">Um valor da enumeração que especifica como manipular conversões.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> criado.</returns>
		// Token: 0x06001BE9 RID: 7145 RVA: 0x000714D8 File Offset: 0x000708D8
		[SecurityCritical]
		public static BitmapSource CreateBitmapSourceFromHBitmap(IntPtr bitmap, IntPtr palette, Int32Rect sourceRect, BitmapSizeOptions sizeOptions)
		{
			SecurityHelper.DemandUnmanagedCode();
			return Imaging.CriticalCreateBitmapSourceFromHBitmap(bitmap, palette, sourceRect, sizeOptions, WICBitmapAlphaChannelOption.WICBitmapUseAlpha);
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x000714F4 File Offset: 0x000708F4
		[SecurityCritical]
		internal static BitmapSource CriticalCreateBitmapSourceFromHBitmap(IntPtr bitmap, IntPtr palette, Int32Rect sourceRect, BitmapSizeOptions sizeOptions, WICBitmapAlphaChannelOption alphaOptions)
		{
			if (bitmap == IntPtr.Zero)
			{
				throw new ArgumentNullException("bitmap");
			}
			return new InteropBitmap(bitmap, palette, sourceRect, sizeOptions, alphaOptions);
		}

		/// <summary>Retorna um <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> gerenciado, com base no ponteiro fornecido a uma imagem de ícone não gerenciado.</summary>
		/// <param name="icon">Um ponteiro para a fonte de ícone não gerenciado.</param>
		/// <param name="sourceRect">O tamanho da imagem de origem.</param>
		/// <param name="sizeOptions">Um valor da enumeração que especifica como manipular conversões.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> criado.</returns>
		// Token: 0x06001BEB RID: 7147 RVA: 0x00071524 File Offset: 0x00070924
		[SecurityCritical]
		public static BitmapSource CreateBitmapSourceFromHIcon(IntPtr icon, Int32Rect sourceRect, BitmapSizeOptions sizeOptions)
		{
			SecurityHelper.DemandUnmanagedCode();
			if (icon == IntPtr.Zero)
			{
				throw new ArgumentNullException("icon");
			}
			return new InteropBitmap(icon, sourceRect, sizeOptions);
		}

		/// <summary>Retorna um <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> gerenciado, com base no local de memória não gerenciada fornecido.</summary>
		/// <param name="section">Um ponteiro para uma seção de memória.</param>
		/// <param name="pixelWidth">Um inteiro que especifica a largura, em pixels, do bitmap.</param>
		/// <param name="pixelHeight">Um inteiro que especifica a altura, em pixels, do bitmap.</param>
		/// <param name="format">Um valor da enumeração.</param>
		/// <param name="stride">A distância do bitmap.</param>
		/// <param name="offset">O deslocamento de bytes no fluxo de memória em que a imagem é iniciada.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> criado.</returns>
		// Token: 0x06001BEC RID: 7148 RVA: 0x00071558 File Offset: 0x00070958
		[SecurityCritical]
		public static BitmapSource CreateBitmapSourceFromMemorySection(IntPtr section, int pixelWidth, int pixelHeight, PixelFormat format, int stride, int offset)
		{
			SecurityHelper.DemandUnmanagedCode();
			if (section == IntPtr.Zero)
			{
				throw new ArgumentNullException("section");
			}
			return new InteropBitmap(section, pixelWidth, pixelHeight, format, stride, offset);
		}
	}
}
