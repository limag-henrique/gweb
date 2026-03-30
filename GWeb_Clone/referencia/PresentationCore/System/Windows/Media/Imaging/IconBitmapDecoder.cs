using System;
using System.IO;
using System.Security;
using Microsoft.Win32.SafeHandles;
using MS.Internal;

namespace System.Windows.Media.Imaging
{
	/// <summary>Define um decodificador especializado para imagens codificadas por ícone (.ico).</summary>
	// Token: 0x020005EE RID: 1518
	public sealed class IconBitmapDecoder : BitmapDecoder
	{
		// Token: 0x060045A0 RID: 17824 RVA: 0x0010F840 File Offset: 0x0010EC40
		private IconBitmapDecoder()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.IconBitmapDecoder" /> do <see cref="T:System.Uri" /> especificado, com o <paramref name="createOptions" /> e o <paramref name="cacheOption" /> especificados.</summary>
		/// <param name="bitmapUri">O <see cref="T:System.Uri" /> que identifica o bitmap a ser decodificado.</param>
		/// <param name="createOptions">Opções de inicialização para a imagem de bitmap.</param>
		/// <param name="cacheOption">O método de cache a ser usado para a imagem de bitmap.</param>
		/// <exception cref="T:System.ArgumentNullException">O valor <paramref name="bitmapUri" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileFormatException">O <paramref name="bitmapUri" /> não é uma imagem de ícone codificada.</exception>
		// Token: 0x060045A1 RID: 17825 RVA: 0x0010F854 File Offset: 0x0010EC54
		[SecurityCritical]
		public IconBitmapDecoder(Uri bitmapUri, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption) : base(bitmapUri, createOptions, cacheOption, MILGuidData.GUID_ContainerFormatIco)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.IconBitmapDecoder" /> do fluxo de arquivos especificado, com o <paramref name="createOptions" /> e <paramref name="cacheOption" /> especificados.</summary>
		/// <param name="bitmapStream">O fluxo de bitmap a ser decodificado.</param>
		/// <param name="createOptions">Opções de inicialização para a imagem de bitmap.</param>
		/// <param name="cacheOption">O método de cache a ser usado para a imagem de bitmap.</param>
		/// <exception cref="T:System.ArgumentNullException">O valor <paramref name="bitmapStream" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileFormatException">O <paramref name="bitmapStream" /> não é uma imagem de ícone codificada.</exception>
		// Token: 0x060045A2 RID: 17826 RVA: 0x0010F870 File Offset: 0x0010EC70
		[SecurityCritical]
		public IconBitmapDecoder(Stream bitmapStream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption) : base(bitmapStream, createOptions, cacheOption, MILGuidData.GUID_ContainerFormatIco)
		{
		}

		// Token: 0x060045A3 RID: 17827 RVA: 0x0010F88C File Offset: 0x0010EC8C
		[SecurityCritical]
		internal IconBitmapDecoder(SafeMILHandle decoderHandle, BitmapDecoder decoder, Uri baseUri, Uri uri, Stream stream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption, bool insertInDecoderCache, bool originalWritable, Stream uriStream, UnmanagedMemoryStream unmanagedMemoryStream, SafeFileHandle safeFilehandle) : base(decoderHandle, decoder, baseUri, uri, stream, createOptions, cacheOption, insertInDecoderCache, originalWritable, uriStream, unmanagedMemoryStream, safeFilehandle)
		{
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x0010F8B4 File Offset: 0x0010ECB4
		internal override void SealObject()
		{
			throw new NotImplementedException();
		}
	}
}
