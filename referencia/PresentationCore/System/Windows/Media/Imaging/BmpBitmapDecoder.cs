using System;
using System.IO;
using System.Security;
using Microsoft.Win32.SafeHandles;
using MS.Internal;

namespace System.Windows.Media.Imaging
{
	/// <summary>Define um decodificador para imagens codificadas por bitmap (BMP).</summary>
	// Token: 0x020005D7 RID: 1495
	public sealed class BmpBitmapDecoder : BitmapDecoder
	{
		// Token: 0x060043B3 RID: 17331 RVA: 0x00107CD4 File Offset: 0x001070D4
		private BmpBitmapDecoder()
		{
		}

		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.Media.Imaging.BmpBitmapDecoder" /> do <see cref="T:System.Uri" /> especificado com os <paramref name="createOptions" /> e <paramref name="cacheOption" /> especificados.</summary>
		/// <param name="bitmapUri">O <see cref="T:System.Uri" /> que identifica o bitmap a ser decodificado.</param>
		/// <param name="createOptions">Opções de inicialização para a imagem de bitmap.</param>
		/// <param name="cacheOption">O método de cache a ser usado para a imagem de bitmap.</param>
		/// <exception cref="T:System.ArgumentNullException">O valor <paramref name="bitmapUri" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileFormatException">O <paramref name="bitmapUri" /> não é uma imagem codificada BMP (bitmap).</exception>
		// Token: 0x060043B4 RID: 17332 RVA: 0x00107CE8 File Offset: 0x001070E8
		[SecurityCritical]
		public BmpBitmapDecoder(Uri bitmapUri, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption) : base(bitmapUri, createOptions, cacheOption, MILGuidData.GUID_ContainerFormatBmp)
		{
		}

		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.Media.Imaging.BmpBitmapDecoder" /> do fluxo de arquivos especificado, com o <paramref name="createOptions" /> e <paramref name="cacheOption" /> especificados.</summary>
		/// <param name="bitmapStream">O fluxo de bitmap a ser decodificado.</param>
		/// <param name="createOptions">Opções de inicialização para a imagem de bitmap.</param>
		/// <param name="cacheOption">O método de cache a ser usado para a imagem de bitmap.</param>
		/// <exception cref="T:System.ArgumentNullException">O valor <paramref name="bitmapStream" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileFormatException">O <paramref name="bitmapStream" /> não é uma imagem codificada BMP (bitmap).</exception>
		// Token: 0x060043B5 RID: 17333 RVA: 0x00107D04 File Offset: 0x00107104
		[SecurityCritical]
		public BmpBitmapDecoder(Stream bitmapStream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption) : base(bitmapStream, createOptions, cacheOption, MILGuidData.GUID_ContainerFormatBmp)
		{
		}

		// Token: 0x060043B6 RID: 17334 RVA: 0x00107D20 File Offset: 0x00107120
		[SecurityCritical]
		internal BmpBitmapDecoder(SafeMILHandle decoderHandle, BitmapDecoder decoder, Uri baseUri, Uri uri, Stream stream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption, bool insertInDecoderCache, bool originalWritable, Stream uriStream, UnmanagedMemoryStream unmanagedMemoryStream, SafeFileHandle safeFilehandle) : base(decoderHandle, decoder, baseUri, uri, stream, createOptions, cacheOption, insertInDecoderCache, originalWritable, uriStream, unmanagedMemoryStream, safeFilehandle)
		{
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x00107D48 File Offset: 0x00107148
		internal override void SealObject()
		{
			throw new NotImplementedException();
		}
	}
}
