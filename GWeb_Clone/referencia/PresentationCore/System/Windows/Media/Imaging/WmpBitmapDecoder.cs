using System;
using System.IO;
using System.Security;
using Microsoft.Win32.SafeHandles;
using MS.Internal;

namespace System.Windows.Media.Imaging
{
	/// <summary>Define um decodificador de imagens codificadas do Microsoft Windows Media Photo.</summary>
	// Token: 0x020005FD RID: 1533
	public sealed class WmpBitmapDecoder : BitmapDecoder
	{
		// Token: 0x0600460C RID: 17932 RVA: 0x00111E50 File Offset: 0x00111250
		private WmpBitmapDecoder()
		{
		}

		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.Media.Imaging.WmpBitmapDecoder" /> do <see cref="T:System.Uri" /> especificado com os <paramref name="createOptions" /> e <paramref name="cacheOption" /> especificados.</summary>
		/// <param name="bitmapUri">O <see cref="T:System.Uri" /> que identifica o bitmap a ser decodificado.</param>
		/// <param name="createOptions">Opções de inicialização para a imagem de bitmap.</param>
		/// <param name="cacheOption">O método de cache para a imagem de bitmap.</param>
		/// <exception cref="T:System.ArgumentNullException">O valor <paramref name="bitmapUri" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileFormatException">O <paramref name="bitmapUri" /> não é uma imagem codificada Windows Media Photo.</exception>
		// Token: 0x0600460D RID: 17933 RVA: 0x00111E64 File Offset: 0x00111264
		[SecurityCritical]
		public WmpBitmapDecoder(Uri bitmapUri, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption) : base(bitmapUri, createOptions, cacheOption, MILGuidData.GUID_ContainerFormatWmp)
		{
		}

		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.Media.Imaging.WmpBitmapDecoder" /> do fluxo de arquivos especificado, com o <paramref name="createOptions" /> e <paramref name="cacheOption" /> especificados.</summary>
		/// <param name="bitmapStream">O fluxo de bitmap a ser decodificado.</param>
		/// <param name="createOptions">Opções de inicialização para a imagem de bitmap.</param>
		/// <param name="cacheOption">O método de cache para a imagem de bitmap.</param>
		/// <exception cref="T:System.ArgumentNullException">O valor <paramref name="bitmapStream" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileFormatException">O <paramref name="bitmapStream" /> não é uma imagem codificada Windows Media Photo.</exception>
		// Token: 0x0600460E RID: 17934 RVA: 0x00111E80 File Offset: 0x00111280
		[SecurityCritical]
		public WmpBitmapDecoder(Stream bitmapStream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption) : base(bitmapStream, createOptions, cacheOption, MILGuidData.GUID_ContainerFormatWmp)
		{
		}

		// Token: 0x0600460F RID: 17935 RVA: 0x00111E9C File Offset: 0x0011129C
		[SecurityCritical]
		internal WmpBitmapDecoder(SafeMILHandle decoderHandle, BitmapDecoder decoder, Uri baseUri, Uri uri, Stream stream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption, bool insertInDecoderCache, bool originalWritable, Stream uriStream, UnmanagedMemoryStream unmanagedMemoryStream, SafeFileHandle safeFilehandle) : base(decoderHandle, decoder, baseUri, uri, stream, createOptions, cacheOption, insertInDecoderCache, originalWritable, uriStream, unmanagedMemoryStream, safeFilehandle)
		{
		}

		// Token: 0x06004610 RID: 17936 RVA: 0x00111EC4 File Offset: 0x001112C4
		internal override void SealObject()
		{
			throw new NotImplementedException();
		}
	}
}
