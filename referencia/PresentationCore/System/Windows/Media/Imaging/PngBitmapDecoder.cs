using System;
using System.IO;
using System.Security;
using Microsoft.Win32.SafeHandles;
using MS.Internal;

namespace System.Windows.Media.Imaging
{
	/// <summary>Define um decodificador para imagens codificadas em PNG (Portable Network Graphics).</summary>
	// Token: 0x020005F4 RID: 1524
	public sealed class PngBitmapDecoder : BitmapDecoder
	{
		// Token: 0x060045DF RID: 17887 RVA: 0x00110678 File Offset: 0x0010FA78
		private PngBitmapDecoder()
		{
		}

		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.Media.Imaging.PngBitmapDecoder" /> do <see cref="T:System.Uri" /> especificado com os <paramref name="createOptions" /> e <paramref name="cacheOption" /> especificados.</summary>
		/// <param name="bitmapUri">O <see cref="T:System.Uri" /> que identifica o bitmap a ser decodificado.</param>
		/// <param name="createOptions">Opções de inicialização para a imagem de bitmap.</param>
		/// <param name="cacheOption">O método de cache a ser usado para a imagem de bitmap.</param>
		/// <exception cref="T:System.ArgumentNullException">O valor <paramref name="bitmapUri" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileFormatException">O <paramref name="bitmapUri" /> não é uma imagem codificada PNG (formato PNG).</exception>
		// Token: 0x060045E0 RID: 17888 RVA: 0x0011068C File Offset: 0x0010FA8C
		[SecurityCritical]
		public PngBitmapDecoder(Uri bitmapUri, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption) : base(bitmapUri, createOptions, cacheOption, MILGuidData.GUID_ContainerFormatPng)
		{
		}

		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.Media.Imaging.PngBitmapDecoder" /> do fluxo de arquivos especificado, com o <paramref name="createOptions" /> e <paramref name="cacheOption" /> especificados.</summary>
		/// <param name="bitmapStream">O fluxo de bitmap a ser decodificado.</param>
		/// <param name="createOptions">Opções de inicialização para a imagem de bitmap.</param>
		/// <param name="cacheOption">O método de cache a ser usado para a imagem de bitmap.</param>
		/// <exception cref="T:System.ArgumentNullException">O valor <paramref name="bitmapStream" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileFormatException">O <paramref name="bitmapStream" /> não é uma imagem codificada PNG (formato PNG).</exception>
		// Token: 0x060045E1 RID: 17889 RVA: 0x001106A8 File Offset: 0x0010FAA8
		[SecurityCritical]
		public PngBitmapDecoder(Stream bitmapStream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption) : base(bitmapStream, createOptions, cacheOption, MILGuidData.GUID_ContainerFormatPng)
		{
		}

		// Token: 0x060045E2 RID: 17890 RVA: 0x001106C4 File Offset: 0x0010FAC4
		[SecurityCritical]
		internal PngBitmapDecoder(SafeMILHandle decoderHandle, BitmapDecoder decoder, Uri baseUri, Uri uri, Stream stream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption, bool insertInDecoderCache, bool originalWritable, Stream uriStream, UnmanagedMemoryStream unmanagedMemoryStream, SafeFileHandle safeFilehandle) : base(decoderHandle, decoder, baseUri, uri, stream, createOptions, cacheOption, insertInDecoderCache, originalWritable, uriStream, unmanagedMemoryStream, safeFilehandle)
		{
		}

		// Token: 0x060045E3 RID: 17891 RVA: 0x001106EC File Offset: 0x0010FAEC
		internal override void SealObject()
		{
			throw new NotImplementedException();
		}
	}
}
