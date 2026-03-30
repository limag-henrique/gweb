using System;
using System.IO;
using System.Security;
using Microsoft.Win32.SafeHandles;
using MS.Internal;

namespace System.Windows.Media.Imaging
{
	/// <summary>Define um decodificador para imagens em formato TIFF codificadas.</summary>
	// Token: 0x020005FA RID: 1530
	public sealed class TiffBitmapDecoder : BitmapDecoder
	{
		// Token: 0x060045FF RID: 17919 RVA: 0x00111C7C File Offset: 0x0011107C
		private TiffBitmapDecoder()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.TiffBitmapDecoder" /> do <see cref="T:System.Uri" /> especificado, com o <paramref name="createOptions" /> e o <paramref name="cacheOption" /> especificados.</summary>
		/// <param name="bitmapUri">O <see cref="T:System.Uri" /> que identifica o bitmap a ser decodificado.</param>
		/// <param name="createOptions">Opções de inicialização para a imagem de bitmap.</param>
		/// <param name="cacheOption">O método de cache para a imagem de bitmap.</param>
		/// <exception cref="T:System.ArgumentNullException">O valor <paramref name="bitmapUri" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileFormatException">O <paramref name="bitmapUri" /> não é uma imagem codificada TIFF (formato TIFF).</exception>
		// Token: 0x06004600 RID: 17920 RVA: 0x00111C90 File Offset: 0x00111090
		[SecurityCritical]
		public TiffBitmapDecoder(Uri bitmapUri, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption) : base(bitmapUri, createOptions, cacheOption, MILGuidData.GUID_ContainerFormatTiff)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.TiffBitmapDecoder" /> do fluxo de arquivos especificado, com o <paramref name="createOptions" /> e <paramref name="cacheOption" /> especificados.</summary>
		/// <param name="bitmapStream">O fluxo de bitmap a ser decodificado.</param>
		/// <param name="createOptions">Opções de inicialização para a imagem de bitmap.</param>
		/// <param name="cacheOption">O método de cache para a imagem de bitmap.</param>
		/// <exception cref="T:System.ArgumentNullException">O valor <paramref name="bitmapStream" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileFormatException">O <paramref name="bitmapStream" /> não é uma imagem codificada TIFF (formato TIFF).</exception>
		// Token: 0x06004601 RID: 17921 RVA: 0x00111CAC File Offset: 0x001110AC
		[SecurityCritical]
		public TiffBitmapDecoder(Stream bitmapStream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption) : base(bitmapStream, createOptions, cacheOption, MILGuidData.GUID_ContainerFormatTiff)
		{
		}

		// Token: 0x06004602 RID: 17922 RVA: 0x00111CC8 File Offset: 0x001110C8
		[SecurityCritical]
		internal TiffBitmapDecoder(SafeMILHandle decoderHandle, BitmapDecoder decoder, Uri baseUri, Uri uri, Stream stream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption, bool insertInDecoderCache, bool originalWritable, Stream uriStream, UnmanagedMemoryStream unmanagedMemoryStream, SafeFileHandle safeFilehandle) : base(decoderHandle, decoder, baseUri, uri, stream, createOptions, cacheOption, insertInDecoderCache, originalWritable, uriStream, unmanagedMemoryStream, safeFilehandle)
		{
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06004603 RID: 17923 RVA: 0x00111CF0 File Offset: 0x001110F0
		internal override bool IsMetadataFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x00111D00 File Offset: 0x00111100
		internal override void SealObject()
		{
			throw new NotImplementedException();
		}
	}
}
