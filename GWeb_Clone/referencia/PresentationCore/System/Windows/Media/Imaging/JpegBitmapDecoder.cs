using System;
using System.IO;
using System.Security;
using Microsoft.Win32.SafeHandles;
using MS.Internal;

namespace System.Windows.Media.Imaging
{
	/// <summary>Define um decodificador para imagens codificadas em JPEG (Joint Photographics Experts Group).</summary>
	// Token: 0x020005F2 RID: 1522
	public sealed class JpegBitmapDecoder : BitmapDecoder
	{
		// Token: 0x060045C8 RID: 17864 RVA: 0x001101B0 File Offset: 0x0010F5B0
		private JpegBitmapDecoder()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.JpegBitmapDecoder" /> do <see cref="T:System.Uri" /> especificado, com o <paramref name="createOptions" /> e o <paramref name="cacheOption" /> especificados.</summary>
		/// <param name="bitmapUri">O <see cref="T:System.Uri" /> que identifica o bitmap a ser decodificado.</param>
		/// <param name="createOptions">Opções de inicialização para a imagem de bitmap.</param>
		/// <param name="cacheOption">O método de cache a ser usado para a imagem de bitmap.</param>
		/// <exception cref="T:System.ArgumentNullException">O valor <paramref name="bitmapUri" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileFormatException">O <paramref name="bitmapUri" /> não é uma imagem codificada JPEG (Joint Photographics Experts Group).</exception>
		// Token: 0x060045C9 RID: 17865 RVA: 0x001101C4 File Offset: 0x0010F5C4
		[SecurityCritical]
		public JpegBitmapDecoder(Uri bitmapUri, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption) : base(bitmapUri, createOptions, cacheOption, MILGuidData.GUID_ContainerFormatJpeg)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.JpegBitmapDecoder" /> do fluxo de arquivos especificado, com o <paramref name="createOptions" /> e <paramref name="cacheOption" /> especificados.</summary>
		/// <param name="bitmapStream">O fluxo de bitmap a ser decodificado.</param>
		/// <param name="createOptions">Opções de inicialização para a imagem de bitmap.</param>
		/// <param name="cacheOption">O método de cache a ser usado para a imagem de bitmap.</param>
		/// <exception cref="T:System.ArgumentNullException">O valor <paramref name="bitmapStream" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileFormatException">O <paramref name="bitmapStream" /> não é uma imagem codificada JPEG (Joint Photographics Experts Group).</exception>
		// Token: 0x060045CA RID: 17866 RVA: 0x001101E0 File Offset: 0x0010F5E0
		[SecurityCritical]
		public JpegBitmapDecoder(Stream bitmapStream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption) : base(bitmapStream, createOptions, cacheOption, MILGuidData.GUID_ContainerFormatJpeg)
		{
		}

		// Token: 0x060045CB RID: 17867 RVA: 0x001101FC File Offset: 0x0010F5FC
		[SecurityCritical]
		internal JpegBitmapDecoder(SafeMILHandle decoderHandle, BitmapDecoder decoder, Uri baseUri, Uri uri, Stream stream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption, bool insertInDecoderCache, bool originalWritable, Stream uriStream, UnmanagedMemoryStream unmanagedMemoryStream, SafeFileHandle safeFilehandle) : base(decoderHandle, decoder, baseUri, uri, stream, createOptions, cacheOption, insertInDecoderCache, originalWritable, uriStream, unmanagedMemoryStream, safeFilehandle)
		{
		}

		// Token: 0x060045CC RID: 17868 RVA: 0x00110224 File Offset: 0x0010F624
		internal override void SealObject()
		{
			throw new NotImplementedException();
		}
	}
}
