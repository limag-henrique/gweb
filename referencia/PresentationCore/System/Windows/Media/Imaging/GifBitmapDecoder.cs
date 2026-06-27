using System;
using System.IO;
using System.Security;
using Microsoft.Win32.SafeHandles;
using MS.Internal;

namespace System.Windows.Media.Imaging
{
	/// <summary>Define um decodificador para imagens codificadas em GIF (Graphics Interchange Format).</summary>
	// Token: 0x020005EC RID: 1516
	public sealed class GifBitmapDecoder : BitmapDecoder
	{
		// Token: 0x06004597 RID: 17815 RVA: 0x0010F728 File Offset: 0x0010EB28
		private GifBitmapDecoder()
		{
		}

		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.Media.Imaging.GifBitmapDecoder" /> do <see cref="T:System.Uri" /> especificado com os <paramref name="createOptions" /> e <paramref name="cacheOption" /> especificados.</summary>
		/// <param name="bitmapUri">O <see cref="T:System.Uri" /> que identifica o bitmap a ser decodificado.</param>
		/// <param name="createOptions">Opções de inicialização para a imagem de bitmap.</param>
		/// <param name="cacheOption">O método de cache a ser usado para a imagem de bitmap.</param>
		/// <exception cref="T:System.ArgumentNullException">O valor <paramref name="bitmapUri" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileFormatException">O <paramref name="bitmapUri" /> não é uma imagem codificada GIF (Graphics Interchange Format).</exception>
		// Token: 0x06004598 RID: 17816 RVA: 0x0010F73C File Offset: 0x0010EB3C
		[SecurityCritical]
		public GifBitmapDecoder(Uri bitmapUri, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption) : base(bitmapUri, createOptions, cacheOption, MILGuidData.GUID_ContainerFormatGif)
		{
		}

		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.Media.Imaging.GifBitmapDecoder" /> do fluxo de arquivos especificado, com o <paramref name="createOptions" /> e <paramref name="cacheOption" /> especificados.</summary>
		/// <param name="bitmapStream">O fluxo de bitmap a ser decodificado.</param>
		/// <param name="createOptions">Opções de inicialização para a imagem de bitmap.</param>
		/// <param name="cacheOption">O método de cache a ser usado para a imagem de bitmap.</param>
		/// <exception cref="T:System.ArgumentNullException">O valor <paramref name="bitmapStream" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileFormatException">O <paramref name="bitmapStream" /> não é uma imagem codificada GIF (Graphics Interchange Format).</exception>
		// Token: 0x06004599 RID: 17817 RVA: 0x0010F758 File Offset: 0x0010EB58
		[SecurityCritical]
		public GifBitmapDecoder(Stream bitmapStream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption) : base(bitmapStream, createOptions, cacheOption, MILGuidData.GUID_ContainerFormatGif)
		{
		}

		// Token: 0x0600459A RID: 17818 RVA: 0x0010F774 File Offset: 0x0010EB74
		[SecurityCritical]
		internal GifBitmapDecoder(SafeMILHandle decoderHandle, BitmapDecoder decoder, Uri baseUri, Uri uri, Stream stream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption, bool insertInDecoderCache, bool originalWritable, Stream uriStream, UnmanagedMemoryStream unmanagedMemoryStream, SafeFileHandle safeFilehandle) : base(decoderHandle, decoder, baseUri, uri, stream, createOptions, cacheOption, insertInDecoderCache, originalWritable, uriStream, unmanagedMemoryStream, safeFilehandle)
		{
		}

		// Token: 0x0600459B RID: 17819 RVA: 0x0010F79C File Offset: 0x0010EB9C
		internal override void SealObject()
		{
			throw new NotImplementedException();
		}
	}
}
