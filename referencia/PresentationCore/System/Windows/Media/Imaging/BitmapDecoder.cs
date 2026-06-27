using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Packaging;
using System.Net;
using System.Net.Cache;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Windows.Navigation;
using System.Windows.Threading;
using Microsoft.Win32.SafeHandles;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Representa um contêiner para quadros de bitmap. Cada quadro de bitmap é um <see cref="T:System.Windows.Media.Imaging.BitmapSource" />. Essa classe abstrata fornece um conjunto básico de funcionalidades para todos os objetos do decodificador derivados.</summary>
	// Token: 0x020005D2 RID: 1490
	public abstract class BitmapDecoder : DispatcherObject
	{
		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.Media.Imaging.BitmapDecoder" />.</summary>
		// Token: 0x06004360 RID: 17248 RVA: 0x0010528C File Offset: 0x0010468C
		protected BitmapDecoder()
		{
		}

		// Token: 0x06004361 RID: 17249 RVA: 0x001052D4 File Offset: 0x001046D4
		internal BitmapDecoder(bool isBuiltIn)
		{
			this._isBuiltInDecoder = isBuiltIn;
		}

		// Token: 0x06004362 RID: 17250 RVA: 0x00105324 File Offset: 0x00104724
		[SecurityCritical]
		internal BitmapDecoder(Uri bitmapUri, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption, Guid expectedClsId)
		{
			Guid empty = Guid.Empty;
			bool isOriginalWritable = false;
			if (bitmapUri == null)
			{
				throw new ArgumentNullException("bitmapUri");
			}
			if ((createOptions & BitmapCreateOptions.IgnoreImageCache) != BitmapCreateOptions.None)
			{
				ImagingCache.RemoveFromDecoderCache(bitmapUri);
			}
			BitmapDecoder bitmapDecoder = BitmapDecoder.CheckCache(bitmapUri, out empty);
			if (bitmapDecoder != null)
			{
				this._decoderHandle = bitmapDecoder.InternalDecoder;
			}
			else
			{
				this._decoderHandle = BitmapDecoder.SetupDecoderFromUriOrStream(bitmapUri, null, cacheOption, out empty, out isOriginalWritable, out this._uriStream, out this._unmanagedMemoryStream, out this._safeFilehandle);
				if (this._uriStream == null)
				{
					GC.SuppressFinalize(this);
				}
			}
			if (empty != expectedClsId)
			{
				throw new FileFormatException(bitmapUri, SR.Get("Image_CantDealWithUri"));
			}
			this._uri = bitmapUri;
			this._createOptions = createOptions;
			this._cacheOption = cacheOption;
			this._syncObject = this._decoderHandle;
			this._isOriginalWritable = isOriginalWritable;
			this.Initialize(bitmapDecoder);
		}

		// Token: 0x06004363 RID: 17251 RVA: 0x00105428 File Offset: 0x00104828
		[SecurityCritical]
		internal BitmapDecoder(Stream bitmapStream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption, Guid expectedClsId)
		{
			Guid empty = Guid.Empty;
			bool isOriginalWritable = false;
			if (bitmapStream == null)
			{
				throw new ArgumentNullException("bitmapStream");
			}
			this._decoderHandle = BitmapDecoder.SetupDecoderFromUriOrStream(null, bitmapStream, cacheOption, out empty, out isOriginalWritable, out this._uriStream, out this._unmanagedMemoryStream, out this._safeFilehandle);
			if (this._uriStream == null)
			{
				GC.SuppressFinalize(this);
			}
			if (empty != Guid.Empty && empty != expectedClsId)
			{
				throw new FileFormatException(null, SR.Get("Image_CantDealWithStream"));
			}
			this._stream = bitmapStream;
			this._createOptions = createOptions;
			this._cacheOption = cacheOption;
			this._syncObject = this._decoderHandle;
			this._isOriginalWritable = isOriginalWritable;
			this.Initialize(null);
		}

		// Token: 0x06004364 RID: 17252 RVA: 0x00105510 File Offset: 0x00104910
		[SecurityCritical]
		internal BitmapDecoder(SafeMILHandle decoderHandle, BitmapDecoder decoder, Uri baseUri, Uri uri, Stream stream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption, bool insertInDecoderCache, bool isOriginalWritable, Stream uriStream, UnmanagedMemoryStream unmanagedMemoryStream, SafeFileHandle safeFilehandle)
		{
			this._decoderHandle = decoderHandle;
			this._baseUri = baseUri;
			this._uri = uri;
			this._stream = stream;
			this._createOptions = createOptions;
			this._cacheOption = cacheOption;
			this._syncObject = decoderHandle;
			this._shouldCacheDecoder = insertInDecoderCache;
			this._isOriginalWritable = isOriginalWritable;
			this._uriStream = uriStream;
			this._unmanagedMemoryStream = unmanagedMemoryStream;
			this._safeFilehandle = safeFilehandle;
			if (this._uriStream == null)
			{
				GC.SuppressFinalize(this);
			}
			this.Initialize(decoder);
		}

		/// <summary>Libera recursos e executa outras operações de limpeza antes que o <see cref="T:System.Windows.Media.Imaging.BitmapDecoder" /> seja reivindicado pela coleta de lixo.</summary>
		// Token: 0x06004365 RID: 17253 RVA: 0x001055C8 File Offset: 0x001049C8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		~BitmapDecoder()
		{
			if (this._uriStream != null)
			{
				this._uriStream.Close();
			}
		}

		// Token: 0x06004366 RID: 17254 RVA: 0x00105610 File Offset: 0x00104A10
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static BitmapDecoder CreateFromUriOrStream(Uri baseUri, Uri uri, Stream stream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption, RequestCachePolicy uriCachePolicy, bool insertInDecoderCache)
		{
			Guid empty = Guid.Empty;
			bool originalWritable = false;
			BitmapDecoder bitmapDecoder = null;
			Uri uri2 = null;
			Stream uriStream = null;
			UnmanagedMemoryStream unmanagedMemoryStream = null;
			SafeFileHandle safeFilehandle = null;
			BitmapDecoder.DemandIfImageBlocked();
			if (uri != null)
			{
				uri2 = ((baseUri != null) ? BaseUriHelper.GetResolvedUri(baseUri, uri) : uri);
				if (insertInDecoderCache)
				{
					if ((createOptions & BitmapCreateOptions.IgnoreImageCache) != BitmapCreateOptions.None)
					{
						ImagingCache.RemoveFromDecoderCache(uri2);
					}
					bitmapDecoder = BitmapDecoder.CheckCache(uri2, out empty);
				}
			}
			SafeMILHandle decoderHandle;
			if (bitmapDecoder != null)
			{
				decoderHandle = bitmapDecoder.InternalDecoder;
			}
			else
			{
				if (uri2 != null && uri2.IsAbsoluteUri && stream == null && (uri2.Scheme == Uri.UriSchemeHttp || uri2.Scheme == Uri.UriSchemeHttps))
				{
					return new LateBoundBitmapDecoder(baseUri, uri, stream, createOptions, cacheOption, uriCachePolicy);
				}
				if (stream != null && !stream.CanSeek)
				{
					return new LateBoundBitmapDecoder(baseUri, uri, stream, createOptions, cacheOption, uriCachePolicy);
				}
				decoderHandle = BitmapDecoder.SetupDecoderFromUriOrStream(uri2, stream, cacheOption, out empty, out originalWritable, out uriStream, out unmanagedMemoryStream, out safeFilehandle);
			}
			BitmapDecoder result;
			if (MILGuidData.GUID_ContainerFormatBmp == empty)
			{
				result = new BmpBitmapDecoder(decoderHandle, bitmapDecoder, baseUri, uri, stream, createOptions, cacheOption, insertInDecoderCache, originalWritable, uriStream, unmanagedMemoryStream, safeFilehandle);
			}
			else if (MILGuidData.GUID_ContainerFormatGif == empty)
			{
				result = new GifBitmapDecoder(decoderHandle, bitmapDecoder, baseUri, uri, stream, createOptions, cacheOption, insertInDecoderCache, originalWritable, uriStream, unmanagedMemoryStream, safeFilehandle);
			}
			else if (MILGuidData.GUID_ContainerFormatIco == empty)
			{
				result = new IconBitmapDecoder(decoderHandle, bitmapDecoder, baseUri, uri, stream, createOptions, cacheOption, insertInDecoderCache, originalWritable, uriStream, unmanagedMemoryStream, safeFilehandle);
			}
			else if (MILGuidData.GUID_ContainerFormatJpeg == empty)
			{
				result = new JpegBitmapDecoder(decoderHandle, bitmapDecoder, baseUri, uri, stream, createOptions, cacheOption, insertInDecoderCache, originalWritable, uriStream, unmanagedMemoryStream, safeFilehandle);
			}
			else if (MILGuidData.GUID_ContainerFormatPng == empty)
			{
				result = new PngBitmapDecoder(decoderHandle, bitmapDecoder, baseUri, uri, stream, createOptions, cacheOption, insertInDecoderCache, originalWritable, uriStream, unmanagedMemoryStream, safeFilehandle);
			}
			else if (MILGuidData.GUID_ContainerFormatTiff == empty)
			{
				result = new TiffBitmapDecoder(decoderHandle, bitmapDecoder, baseUri, uri, stream, createOptions, cacheOption, insertInDecoderCache, originalWritable, uriStream, unmanagedMemoryStream, safeFilehandle);
			}
			else if (MILGuidData.GUID_ContainerFormatWmp == empty)
			{
				result = new WmpBitmapDecoder(decoderHandle, bitmapDecoder, baseUri, uri, stream, createOptions, cacheOption, insertInDecoderCache, originalWritable, uriStream, unmanagedMemoryStream, safeFilehandle);
			}
			else
			{
				result = new UnknownBitmapDecoder(decoderHandle, bitmapDecoder, baseUri, uri, stream, createOptions, cacheOption, insertInDecoderCache, originalWritable, uriStream, unmanagedMemoryStream, safeFilehandle);
			}
			return result;
		}

		/// <summary>Cria um <see cref="T:System.Windows.Media.Imaging.BitmapDecoder" /> de um <see cref="T:System.Uri" /> usando o <see cref="T:System.Windows.Media.Imaging.BitmapCreateOptions" /> e o <see cref="T:System.Windows.Media.Imaging.BitmapCacheOption" /> especificados.</summary>
		/// <param name="bitmapUri">O <see cref="T:System.Uri" /> do bitmap a ser decodificado.</param>
		/// <param name="createOptions">Identifica o <see cref="T:System.Windows.Media.Imaging.BitmapCreateOptions" /> para este decodificador.</param>
		/// <param name="cacheOption">Identifica o <see cref="T:System.Windows.Media.Imaging.BitmapCacheOption" /> para este decodificador.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.Imaging.BitmapDecoder" /> de um <see cref="T:System.Uri" /> usando o <see cref="T:System.Windows.Media.Imaging.BitmapCreateOptions" /> e o <see cref="T:System.Windows.Media.Imaging.BitmapCacheOption" /> especificados.</returns>
		/// <exception cref="T:System.ArgumentNullException">O <paramref name="bitmapUri" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileFormatException">O <paramref name="bitmapUri" /> especifica uma ID de classe de um tipo de formato sem suporte.</exception>
		// Token: 0x06004367 RID: 17255 RVA: 0x00105834 File Offset: 0x00104C34
		public static BitmapDecoder Create(Uri bitmapUri, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption)
		{
			return BitmapDecoder.Create(bitmapUri, createOptions, cacheOption, null);
		}

		/// <summary>Cria um <see cref="T:System.Windows.Media.Imaging.BitmapDecoder" /> de um <see cref="T:System.Uri" /> usando o <see cref="T:System.Windows.Media.Imaging.BitmapCreateOptions" />, <see cref="T:System.Windows.Media.Imaging.BitmapCacheOption" /> e <see cref="T:System.Net.Cache.RequestCachePolicy" /> especificados.</summary>
		/// <param name="bitmapUri">O local do bitmap do qual o <see cref="T:System.Windows.Media.Imaging.BitmapDecoder" /> é criado.</param>
		/// <param name="createOptions">As opções usadas para criar este <see cref="T:System.Windows.Media.Imaging.BitmapDecoder" />.</param>
		/// <param name="cacheOption">As opção de cache usada para criar este <see cref="T:System.Windows.Media.Imaging.BitmapDecoder" />.</param>
		/// <param name="uriCachePolicy">Os requisitos de cache para este <see cref="T:System.Windows.Media.Imaging.BitmapDecoder" />.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.Imaging.BitmapDecoder" /> de um <see cref="T:System.Uri" /> usando o <see cref="T:System.Windows.Media.Imaging.BitmapCreateOptions" />, <see cref="T:System.Windows.Media.Imaging.BitmapCacheOption" /> e <see cref="T:System.Net.Cache.RequestCachePolicy" /> especificados.</returns>
		// Token: 0x06004368 RID: 17256 RVA: 0x0010584C File Offset: 0x00104C4C
		public static BitmapDecoder Create(Uri bitmapUri, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption, RequestCachePolicy uriCachePolicy)
		{
			if (bitmapUri == null)
			{
				throw new ArgumentNullException("bitmapUri");
			}
			return BitmapDecoder.CreateFromUriOrStream(null, bitmapUri, null, createOptions, cacheOption, uriCachePolicy, true);
		}

		/// <summary>Cria um <see cref="T:System.Windows.Media.Imaging.BitmapDecoder" /> de um <see cref="T:System.IO.Stream" /> usando o <see cref="T:System.Windows.Media.Imaging.BitmapCreateOptions" /> e o <see cref="T:System.Windows.Media.Imaging.BitmapCacheOption" /> especificados.</summary>
		/// <param name="bitmapStream">O fluxo de arquivos que identifica o bitmap a ser decodificado.</param>
		/// <param name="createOptions">Identifica o <see cref="T:System.Windows.Media.Imaging.BitmapCreateOptions" /> para este decodificador.</param>
		/// <param name="cacheOption">Identifica o <see cref="T:System.Windows.Media.Imaging.BitmapCacheOption" /> para este decodificador.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.Imaging.BitmapDecoder" /> de um <see cref="T:System.IO.Stream" /> usando o <see cref="T:System.Windows.Media.Imaging.BitmapCreateOptions" /> e o <see cref="T:System.Windows.Media.Imaging.BitmapCacheOption" /> especificados.</returns>
		// Token: 0x06004369 RID: 17257 RVA: 0x0010587C File Offset: 0x00104C7C
		public static BitmapDecoder Create(Stream bitmapStream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption)
		{
			if (bitmapStream == null)
			{
				throw new ArgumentNullException("bitmapStream");
			}
			return BitmapDecoder.CreateFromUriOrStream(null, null, bitmapStream, createOptions, cacheOption, null, true);
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Media.Imaging.BitmapPalette" /> associado a este decodificador.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.Imaging.BitmapPalette" /> associado a este decodificador. Se o bitmap não possui nenhuma paleta <see langword="null" /> é retornado. Esta propriedade não tem valor padrão.</returns>
		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x0600436A RID: 17258 RVA: 0x001058A4 File Offset: 0x00104CA4
		public virtual BitmapPalette Palette
		{
			[SecurityCritical]
			get
			{
				base.VerifyAccess();
				this.EnsureBuiltInDecoder();
				if (!this._isPaletteCached)
				{
					SafeMILHandle safeMILHandle = BitmapPalette.CreateInternalPalette();
					object syncObject = this._syncObject;
					lock (syncObject)
					{
						int num = UnsafeNativeMethods.WICBitmapDecoder.CopyPalette(this._decoderHandle, safeMILHandle);
						if (num != -2003292347)
						{
							HRESULT.Check(num);
							this._palette = new BitmapPalette(safeMILHandle);
						}
					}
					this._isPaletteCached = true;
				}
				return this._palette;
			}
		}

		/// <summary>Obterá um valor que representa o perfil de cor associado a um bitmap, se algum tiver sido definido.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.ColorContext" /> que representa o perfil de cor incorporado do bitmap. Se nenhum perfil de cor tiver sido definida, essa propriedade retornará <see langword="null" />. Esta propriedade não tem valor padrão.</returns>
		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x0600436B RID: 17259 RVA: 0x00105938 File Offset: 0x00104D38
		public virtual ReadOnlyCollection<ColorContext> ColorContexts
		{
			get
			{
				base.VerifyAccess();
				return this.InternalColorContexts;
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que representará a miniatura do bitmap, se alguma for definida.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que representa uma miniatura do bitmap. Se nenhuma miniatura é definida, essa propriedade retornará <see langword="null" />. Esta propriedade não tem valor padrão.</returns>
		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x0600436C RID: 17260 RVA: 0x00105954 File Offset: 0x00104D54
		public virtual BitmapSource Thumbnail
		{
			[SecurityCritical]
			get
			{
				base.VerifyAccess();
				this.EnsureBuiltInDecoder();
				if (!this._isThumbnailCached)
				{
					IntPtr zero = IntPtr.Zero;
					object syncObject = this._syncObject;
					lock (syncObject)
					{
						int thumbnail = UnsafeNativeMethods.WICBitmapDecoder.GetThumbnail(this._decoderHandle, out zero);
						if (thumbnail != -2003292348)
						{
							HRESULT.Check(thumbnail);
						}
					}
					if (zero != IntPtr.Zero)
					{
						BitmapSourceSafeMILHandle bitmapSourceSafeMILHandle = new BitmapSourceSafeMILHandle(zero);
						SafeMILHandle safeMILHandle = BitmapPalette.CreateInternalPalette();
						BitmapPalette palette = null;
						if (UnsafeNativeMethods.WICBitmapSource.CopyPalette(bitmapSourceSafeMILHandle, safeMILHandle) == 0)
						{
							palette = new BitmapPalette(safeMILHandle);
						}
						this._thumbnail = new UnmanagedBitmapWrapper(BitmapSource.CreateCachedBitmap(null, bitmapSourceSafeMILHandle, BitmapCreateOptions.PreservePixelFormat, this._cacheOption, palette));
						this._thumbnail.Freeze();
					}
					this._isThumbnailCached = true;
				}
				return this._thumbnail;
			}
		}

		/// <summary>Obtém uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" /> que representa os metadados globais associados a esse bitmap se os metadados estão definidos.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" /> que representa os metadados globais associados a um bitmap. Se nenhum metadado for definido, essa propriedade retornará <see langword="null" />.</returns>
		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x0600436D RID: 17261 RVA: 0x00105A40 File Offset: 0x00104E40
		public virtual BitmapMetadata Metadata
		{
			[SecurityCritical]
			get
			{
				base.VerifyAccess();
				this.EnsureBuiltInDecoder();
				this.CheckIfSiteOfOrigin();
				if (!this._isMetadataCached)
				{
					IntPtr zero = IntPtr.Zero;
					object syncObject = this._syncObject;
					lock (syncObject)
					{
						int metadataQueryReader = UnsafeNativeMethods.WICBitmapDecoder.GetMetadataQueryReader(this._decoderHandle, out zero);
						if (metadataQueryReader != -2003292287)
						{
							HRESULT.Check(metadataQueryReader);
						}
					}
					if (zero != IntPtr.Zero)
					{
						SafeMILHandle metadataHandle = new SafeMILHandle(zero);
						this._metadata = new BitmapMetadata(metadataHandle, true, this.IsMetadataFixedSize, this._syncObject);
						this._metadata.Freeze();
					}
					this._isMetadataCached = true;
				}
				return this._metadata;
			}
		}

		/// <summary>Obtém informações que descrevem esse codec.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapCodecInfo" />. Esta propriedade não tem valor padrão.</returns>
		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x0600436E RID: 17262 RVA: 0x00105B0C File Offset: 0x00104F0C
		public virtual BitmapCodecInfo CodecInfo
		{
			[SecurityCritical]
			get
			{
				base.VerifyAccess();
				this.EnsureBuiltInDecoder();
				if (this._codecInfo == null)
				{
					SafeMILHandle codecInfoHandle = new SafeMILHandle();
					HRESULT.Check(UnsafeNativeMethods.WICBitmapDecoder.GetDecoderInfo(this._decoderHandle, out codecInfoHandle));
					this._codecInfo = new BitmapCodecInfoInternal(codecInfoHandle);
				}
				return this._codecInfo;
			}
		}

		/// <summary>Obtém o conteúdo de um quadro individual dentro de um bitmap.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />. Esta propriedade não tem valor padrão.</returns>
		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x0600436F RID: 17263 RVA: 0x00105B58 File Offset: 0x00104F58
		public virtual ReadOnlyCollection<BitmapFrame> Frames
		{
			get
			{
				base.VerifyAccess();
				this.EnsureBuiltInDecoder();
				if (this._frames == null)
				{
					this.SetupFrames(null, null);
				}
				if (this._readOnlyFrames == null)
				{
					this._readOnlyFrames = new ReadOnlyCollection<BitmapFrame>(this._frames);
				}
				return this._readOnlyFrames;
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que representará a versão prévia global deste bitmap, se alguma for definida.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que representa a versão prévia global do bitmap. Se nenhuma visualização for definida, essa propriedade retornará <see langword="null" />. Esta propriedade não tem valor padrão.</returns>
		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06004370 RID: 17264 RVA: 0x00105BA0 File Offset: 0x00104FA0
		public virtual BitmapSource Preview
		{
			[SecurityCritical]
			get
			{
				base.VerifyAccess();
				this.EnsureBuiltInDecoder();
				if (!this._isPreviewCached)
				{
					IntPtr zero = IntPtr.Zero;
					object syncObject = this._syncObject;
					lock (syncObject)
					{
						int preview = UnsafeNativeMethods.WICBitmapDecoder.GetPreview(this._decoderHandle, out zero);
						if (preview != -2003292287)
						{
							HRESULT.Check(preview);
						}
					}
					if (zero != IntPtr.Zero)
					{
						BitmapSourceSafeMILHandle bitmapSourceSafeMILHandle = new BitmapSourceSafeMILHandle(zero);
						SafeMILHandle safeMILHandle = BitmapPalette.CreateInternalPalette();
						BitmapPalette palette = null;
						if (UnsafeNativeMethods.WICBitmapSource.CopyPalette(bitmapSourceSafeMILHandle, safeMILHandle) == 0)
						{
							palette = new BitmapPalette(safeMILHandle);
						}
						this._preview = new UnmanagedBitmapWrapper(BitmapSource.CreateCachedBitmap(null, bitmapSourceSafeMILHandle, BitmapCreateOptions.PreservePixelFormat, this._cacheOption, palette));
						this._preview.Freeze();
					}
					this._isPreviewCached = true;
				}
				return this._preview;
			}
		}

		/// <summary>Obtém um valor que indica se o decodificador está baixando conteúdo no momento.</summary>
		/// <returns>
		///   <see langword="true" /> Se o decodificador está baixando conteúdo; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06004371 RID: 17265 RVA: 0x00105C8C File Offset: 0x0010508C
		public virtual bool IsDownloading
		{
			get
			{
				base.VerifyAccess();
				this.EnsureBuiltInDecoder();
				return false;
			}
		}

		/// <summary>Ocorre depois que um <see cref="T:System.Windows.Media.Imaging.BitmapDecoder" /> conclui o download do conteúdo do bitmap.</summary>
		// Token: 0x140001CB RID: 459
		// (add) Token: 0x06004372 RID: 17266 RVA: 0x00105CA8 File Offset: 0x001050A8
		// (remove) Token: 0x06004373 RID: 17267 RVA: 0x00105CD0 File Offset: 0x001050D0
		public virtual event EventHandler DownloadCompleted
		{
			add
			{
				base.VerifyAccess();
				this.EnsureBuiltInDecoder();
				this._downloadEvent.AddEvent(value);
			}
			remove
			{
				base.VerifyAccess();
				this.EnsureBuiltInDecoder();
				this._downloadEvent.RemoveEvent(value);
			}
		}

		/// <summary>Ocorre quando um <see cref="T:System.Windows.Media.Imaging.BitmapDecoder" /> progrediu no download do conteúdo do bitmap.</summary>
		// Token: 0x140001CC RID: 460
		// (add) Token: 0x06004374 RID: 17268 RVA: 0x00105CF8 File Offset: 0x001050F8
		// (remove) Token: 0x06004375 RID: 17269 RVA: 0x00105D20 File Offset: 0x00105120
		public virtual event EventHandler<DownloadProgressEventArgs> DownloadProgress
		{
			add
			{
				base.VerifyAccess();
				this.EnsureBuiltInDecoder();
				this._progressEvent.AddEvent(value);
			}
			remove
			{
				base.VerifyAccess();
				this.EnsureBuiltInDecoder();
				this._progressEvent.RemoveEvent(value);
			}
		}

		/// <summary>Ocorre quando há falha no download do conteúdo do bitmap.</summary>
		// Token: 0x140001CD RID: 461
		// (add) Token: 0x06004376 RID: 17270 RVA: 0x00105D48 File Offset: 0x00105148
		// (remove) Token: 0x06004377 RID: 17271 RVA: 0x00105D70 File Offset: 0x00105170
		public virtual event EventHandler<ExceptionEventArgs> DownloadFailed
		{
			add
			{
				base.VerifyAccess();
				this.EnsureBuiltInDecoder();
				this._failedEvent.AddEvent(value);
			}
			remove
			{
				base.VerifyAccess();
				this.EnsureBuiltInDecoder();
				this._failedEvent.RemoveEvent(value);
			}
		}

		/// <summary>Cria uma instância de <see cref="T:System.Windows.Media.Imaging.InPlaceBitmapMetadataWriter" />, que pode ser usada para atualizar os metadados de um bitmap.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.InPlaceBitmapMetadataWriter" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">O fluxo de imagem original do arquivo é somente leitura.</exception>
		/// <exception cref="T:System.NotImplementedException">O decodificador não é um decodificador interno.</exception>
		// Token: 0x06004378 RID: 17272 RVA: 0x00105D98 File Offset: 0x00105198
		public virtual InPlaceBitmapMetadataWriter CreateInPlaceBitmapMetadataWriter()
		{
			base.VerifyAccess();
			this.EnsureBuiltInDecoder();
			this.CheckOriginalWritable();
			this.CheckIfSiteOfOrigin();
			return InPlaceBitmapMetadataWriter.CreateFromDecoder(this._decoderHandle, this._syncObject);
		}

		/// <summary>Converte o valor atual de um <see cref="T:System.Windows.Media.Imaging.BitmapDecoder" /> em um <see cref="T:System.String" />.</summary>
		/// <returns>Uma representação da cadeia de caracteres do <see cref="T:System.Windows.Media.Imaging.BitmapDecoder" />.</returns>
		// Token: 0x06004379 RID: 17273 RVA: 0x00105DD0 File Offset: 0x001051D0
		public override string ToString()
		{
			base.VerifyAccess();
			if (!this._isBuiltInDecoder)
			{
				return base.ToString();
			}
			if (!(this._uri != null))
			{
				return "image";
			}
			if (this._baseUri != null)
			{
				Uri uri = new Uri(this._baseUri, this._uri);
				return BindUriHelper.UriToString(uri);
			}
			return BindUriHelper.UriToString(this._uri);
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x0600437A RID: 17274 RVA: 0x00105E38 File Offset: 0x00105238
		internal SafeMILHandle InternalDecoder
		{
			get
			{
				return this._decoderHandle;
			}
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x0600437B RID: 17275 RVA: 0x00105E4C File Offset: 0x0010524C
		internal virtual bool IsMetadataFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x0600437C RID: 17276 RVA: 0x00105E5C File Offset: 0x0010525C
		internal object SyncObject
		{
			get
			{
				return this._syncObject;
			}
		}

		// Token: 0x0600437D RID: 17277 RVA: 0x00105E70 File Offset: 0x00105270
		[SecurityCritical]
		private int GetColorContexts(ref uint numContexts, IntPtr[] colorContextPtrs)
		{
			Invariant.Assert(colorContextPtrs == null || (ulong)numContexts <= (ulong)((long)colorContextPtrs.Length));
			return UnsafeNativeMethods.WICBitmapDecoder.GetColorContexts(this._decoderHandle, numContexts, colorContextPtrs, out numContexts);
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x0600437E RID: 17278 RVA: 0x00105EA4 File Offset: 0x001052A4
		internal ReadOnlyCollection<ColorContext> InternalColorContexts
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				this.EnsureBuiltInDecoder();
				if (!this._isColorContextCached)
				{
					object syncObject = this._syncObject;
					lock (syncObject)
					{
						IList<ColorContext> colorContextsHelper = ColorContext.GetColorContextsHelper(new ColorContext.GetColorContextsDelegate(this.GetColorContexts));
						if (colorContextsHelper != null)
						{
							this._readOnlycolorContexts = new ReadOnlyCollection<ColorContext>(colorContextsHelper);
						}
						this._isColorContextCached = true;
					}
				}
				return this._readOnlycolorContexts;
			}
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x00105F28 File Offset: 0x00105328
		internal void CheckOriginalWritable()
		{
			if (!this._isOriginalWritable)
			{
				throw new InvalidOperationException(SR.Get("Image_OriginalStreamReadOnly"));
			}
		}

		// Token: 0x06004380 RID: 17280 RVA: 0x00105F50 File Offset: 0x00105350
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private static void DemandIfImageBlocked()
		{
			if (!BitmapDecoder.isImageDisabledInitialized)
			{
				BitmapDecoder.isImageDisabled = new SecurityCriticalDataForSet<bool>(SafeSecurityHelper.IsFeatureDisabled(SafeSecurityHelper.KeyToRead.MediaImageDisable));
				BitmapDecoder.isImageDisabledInitialized = true;
			}
			if (BitmapDecoder.isImageDisabled.Value)
			{
				SecurityHelper.DemandMediaPermission(MediaPermissionAudio.NoAudio, MediaPermissionVideo.NoVideo, MediaPermissionImage.AllImage);
				return;
			}
			SecurityHelper.DemandMediaPermission(MediaPermissionAudio.NoAudio, MediaPermissionVideo.NoVideo, MediaPermissionImage.SafeImage);
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x00105F98 File Offset: 0x00105398
		[SecurityCritical]
		internal static SafeMILHandle SetupDecoderFromUriOrStream(Uri uri, Stream stream, BitmapCacheOption cacheOption, out Guid clsId, out bool isOriginalWritable, out Stream uriStream, out UnmanagedMemoryStream unmanagedMemoryStream, out SafeFileHandle safeFilehandle)
		{
			IntPtr zero = IntPtr.Zero;
			Stream stream2 = null;
			string text = string.Empty;
			BitmapDecoder.DemandIfImageBlocked();
			unmanagedMemoryStream = null;
			safeFilehandle = null;
			isOriginalWritable = false;
			uriStream = null;
			if (uri != null)
			{
			}
			if (uri != null)
			{
				if (uri.IsAbsoluteUri)
				{
					SecurityHelper.DemandMediaPermission(MediaPermissionAudio.NoAudio, MediaPermissionVideo.NoVideo, MediaPermissionImage.SiteOfOriginImage);
					if (string.Compare(uri.Scheme, PackUriHelper.UriSchemePack, StringComparison.OrdinalIgnoreCase) == 0)
					{
						WebResponse webResponse = WpfWebRequestHelper.CreateRequestAndGetResponse(uri);
						text = webResponse.ContentType;
						stream2 = webResponse.GetResponseStream();
						uriStream = stream2;
					}
				}
				if (stream2 == null || stream2 == Stream.Null)
				{
					if (uri.IsAbsoluteUri)
					{
						if (SecurityHelper.MapUrlToZoneWrapper(uri) == 0)
						{
							if (uri.IsFile)
							{
								stream2 = new FileStream(uri.LocalPath, FileMode.Open, FileAccess.Read, FileShare.Read);
							}
						}
						else if (uri.IsFile && uri.IsUnc)
						{
							stream2 = BitmapDecoder.ProcessUncFiles(uri);
						}
						else if (uri.Scheme == Uri.UriSchemeHttp)
						{
							stream2 = BitmapDecoder.ProcessHttpFiles(uri, stream);
						}
						else if (uri.Scheme == Uri.UriSchemeHttps)
						{
							stream2 = BitmapDecoder.ProcessHttpsFiles(uri, stream);
						}
						else
						{
							stream2 = WpfWebRequestHelper.CreateRequestAndGetResponseStream(uri);
						}
					}
					else
					{
						stream2 = new FileStream(uri.OriginalString, FileMode.Open, FileAccess.Read, FileShare.Read);
					}
					uriStream = stream2;
				}
			}
			if (stream2 != null)
			{
				stream = stream2;
			}
			else
			{
				isOriginalWritable = (stream.CanSeek && stream.CanWrite);
			}
			stream = BitmapDecoder.GetSeekableStream(stream);
			if (stream is UnmanagedMemoryStream)
			{
				unmanagedMemoryStream = (stream as UnmanagedMemoryStream);
			}
			IntPtr pIStream = IntPtr.Zero;
			if (stream is FileStream)
			{
				FileStream fileStream = stream as FileStream;
				try
				{
					safeFilehandle = fileStream.SafeFileHandle;
				}
				catch
				{
					safeFilehandle = null;
				}
			}
			SafeMILHandle safeMILHandle;
			try
			{
				Guid guid = new Guid(MILGuidData.GUID_VendorMicrosoft);
				uint metadataFlags = 0U;
				if (cacheOption == BitmapCacheOption.OnLoad)
				{
					metadataFlags = 1U;
				}
				if (safeFilehandle != null)
				{
					using (FactoryMaker factoryMaker = new FactoryMaker())
					{
						HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreateDecoderFromFileHandle(factoryMaker.ImagingFactoryPtr, safeFilehandle, ref guid, metadataFlags, out zero));
						goto IL_203;
					}
				}
				pIStream = BitmapDecoder.GetIStreamFromStream(ref stream);
				using (FactoryMaker factoryMaker2 = new FactoryMaker())
				{
					HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreateDecoderFromStream(factoryMaker2.ImagingFactoryPtr, pIStream, ref guid, metadataFlags, out zero));
				}
				IL_203:
				safeMILHandle = new SafeMILHandle(zero);
			}
			catch
			{
				safeMILHandle = null;
				throw;
			}
			finally
			{
				UnsafeNativeMethods.MILUnknown.ReleaseInterface(ref pIStream);
			}
			string text2;
			clsId = BitmapDecoder.GetCLSIDFromDecoder(safeMILHandle, out text2);
			if (text != string.Empty && text2.IndexOf(text, StringComparison.OrdinalIgnoreCase) == -1)
			{
				try
				{
					SecurityHelper.DemandUnmanagedCode();
				}
				catch (SecurityException)
				{
					throw new ArgumentException(SR.Get("Image_ContentTypeDoesNotMatchDecoder"));
				}
			}
			return safeMILHandle;
		}

		// Token: 0x06004382 RID: 17282 RVA: 0x0010629C File Offset: 0x0010569C
		[SecurityCritical]
		private static Stream ProcessHttpsFiles(Uri uri, Stream stream)
		{
			Stream stream2 = stream;
			if (stream2 == null || !stream2.CanSeek)
			{
				WebRequest request = null;
				SecurityHelper.BlockCrossDomainForHttpsApps(uri);
				new WebPermission(NetworkAccess.Connect, BindUriHelper.UriToString(uri)).Assert();
				try
				{
					request = WpfWebRequestHelper.CreateRequest(uri);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				stream2 = WpfWebRequestHelper.GetResponseStream(request);
			}
			return stream2;
		}

		// Token: 0x06004383 RID: 17283 RVA: 0x00106304 File Offset: 0x00105704
		[SecurityCritical]
		private static Stream ProcessHttpFiles(Uri uri, Stream stream)
		{
			WebRequest request = null;
			Stream stream2 = stream;
			SecurityHelper.BlockCrossDomainForHttpsApps(uri);
			if (stream2 == null || !stream2.CanSeek)
			{
				bool flag = false;
				if (SecurityHelper.CallerHasMediaPermission(MediaPermissionAudio.NoAudio, MediaPermissionVideo.NoVideo, MediaPermissionImage.SafeImage))
				{
					flag = true;
				}
				if (flag)
				{
					new WebPermission(NetworkAccess.Connect, BindUriHelper.UriToString(uri)).Assert();
				}
				try
				{
					request = WpfWebRequestHelper.CreateRequest(uri);
				}
				finally
				{
					if (flag)
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				stream2 = WpfWebRequestHelper.GetResponseStream(request);
			}
			return stream2;
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x00106380 File Offset: 0x00105780
		[SecurityCritical]
		private static Stream ProcessUncFiles(Uri uri)
		{
			Stream result = null;
			SecurityHelper.EnforceUncContentAccessRules(uri);
			bool flag = false;
			if (SecurityHelper.CallerHasMediaPermission(MediaPermissionAudio.NoAudio, MediaPermissionVideo.NoVideo, MediaPermissionImage.SafeImage))
			{
				flag = true;
			}
			if (flag)
			{
				new FileIOPermission(FileIOPermissionAccess.Read, uri.LocalPath).Assert();
			}
			try
			{
				result = new FileStream(uri.LocalPath, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			finally
			{
				if (flag)
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			return result;
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x001063F0 File Offset: 0x001057F0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void CheckIfSiteOfOrigin()
		{
			string uri = null;
			if (this.CanConvertToString())
			{
				uri = this.ToString();
			}
			SecurityHelper.DemandMediaAccessPermission(uri);
		}

		// Token: 0x06004386 RID: 17286 RVA: 0x00106414 File Offset: 0x00105814
		[SecurityCritical]
		private static Guid GetCLSIDFromDecoder(SafeMILHandle decoderHandle, out string decoderMimeTypes)
		{
			SafeMILHandle this_PTR = new SafeMILHandle();
			HRESULT.Check(UnsafeNativeMethods.WICBitmapDecoder.GetDecoderInfo(decoderHandle, out this_PTR));
			Guid result;
			HRESULT.Check(UnsafeNativeMethods.WICBitmapCodecInfo.GetContainerFormat(this_PTR, out result));
			StringBuilder stringBuilder = null;
			uint num = 0U;
			HRESULT.Check(UnsafeNativeMethods.WICBitmapCodecInfo.GetMimeTypes(this_PTR, 0U, stringBuilder, out num));
			if (num > 0U)
			{
				stringBuilder = new StringBuilder((int)num);
				HRESULT.Check(UnsafeNativeMethods.WICBitmapCodecInfo.GetMimeTypes(this_PTR, num, stringBuilder, out num));
			}
			if (stringBuilder != null)
			{
				decoderMimeTypes = stringBuilder.ToString();
			}
			else
			{
				decoderMimeTypes = string.Empty;
			}
			return result;
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x00106484 File Offset: 0x00105884
		private static Stream GetSeekableStream(Stream bitmapStream)
		{
			if (bitmapStream.CanSeek)
			{
				return bitmapStream;
			}
			MemoryStream memoryStream = new MemoryStream();
			byte[] buffer = new byte[1024];
			for (;;)
			{
				int num = bitmapStream.Read(buffer, 0, 1024);
				if (num <= 0)
				{
					break;
				}
				memoryStream.Write(buffer, 0, num);
			}
			memoryStream.Seek(0L, SeekOrigin.Begin);
			return memoryStream;
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x001064D4 File Offset: 0x001058D4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private static BitmapDecoder CheckCache(Uri uri, out Guid clsId)
		{
			clsId = Guid.Empty;
			if (uri != null)
			{
				WeakReference weakReference = ImagingCache.CheckDecoderCache(uri) as WeakReference;
				if (weakReference != null)
				{
					BitmapDecoder bitmapDecoder = weakReference.Target as BitmapDecoder;
					if (bitmapDecoder != null && bitmapDecoder.CheckAccess())
					{
						object syncObject = bitmapDecoder.SyncObject;
						lock (syncObject)
						{
							string text;
							clsId = BitmapDecoder.GetCLSIDFromDecoder(bitmapDecoder.InternalDecoder, out text);
							return bitmapDecoder;
						}
					}
					if (bitmapDecoder == null)
					{
						ImagingCache.RemoveFromDecoderCache(uri);
					}
				}
			}
			return null;
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x00106578 File Offset: 0x00105978
		[SecurityCritical]
		private void Initialize(BitmapDecoder decoder)
		{
			this._isBuiltInDecoder = true;
			if (decoder != null)
			{
				this.SetupFrames(decoder, null);
				this._cachedDecoder = decoder;
			}
			else if ((this._createOptions & BitmapCreateOptions.DelayCreation) == BitmapCreateOptions.None && this._cacheOption == BitmapCacheOption.OnLoad)
			{
				this.SetupFrames(null, null);
				this.CloseStream();
			}
			if (this._uri != null && decoder == null && this._shouldCacheDecoder)
			{
				ImagingCache.AddToDecoderCache((this._baseUri == null) ? this._uri : new Uri(this._baseUri, this._uri), new WeakReference(this));
			}
		}

		// Token: 0x0600438A RID: 17290 RVA: 0x0010660C File Offset: 0x00105A0C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal void CloseStream()
		{
			if (this._uriStream != null)
			{
				this._uriStream.Close();
				this._uriStream = null;
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0600438B RID: 17291 RVA: 0x0010663C File Offset: 0x00105A3C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal void SetupFrames(BitmapDecoder decoder, ReadOnlyCollection<BitmapFrame> frames)
		{
			uint num = 1U;
			HRESULT.Check(UnsafeNativeMethods.WICBitmapDecoder.GetFrameCount(this._decoderHandle, out num));
			this._frames = new List<BitmapFrame>((int)num);
			for (int i = 0; i < (int)num; i++)
			{
				if (i > 0 && this._cacheOption != BitmapCacheOption.OnLoad)
				{
					this._createOptions |= BitmapCreateOptions.DelayCreation;
				}
				BitmapFrameDecode bitmapFrameDecode;
				if (frames != null && frames.Count == i + 1)
				{
					bitmapFrameDecode = (frames[i] as BitmapFrameDecode);
					bitmapFrameDecode.UpdateDecoder(this);
				}
				else if (decoder == null)
				{
					bitmapFrameDecode = new BitmapFrameDecode(i, this._createOptions, this._cacheOption, this);
					bitmapFrameDecode.Freeze();
				}
				else
				{
					bitmapFrameDecode = new BitmapFrameDecode(i, this._createOptions, this._cacheOption, decoder.Frames[i] as BitmapFrameDecode);
					bitmapFrameDecode.Freeze();
				}
				this._frames.Add(bitmapFrameDecode);
			}
		}

		// Token: 0x0600438C RID: 17292 RVA: 0x00106714 File Offset: 0x00105B14
		private void EnsureBuiltInDecoder()
		{
			if (!this._isBuiltInDecoder)
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600438D RID: 17293 RVA: 0x00106730 File Offset: 0x00105B30
		[SecurityCritical]
		private unsafe static IntPtr GetIStreamFromStream(ref Stream bitmapStream)
		{
			IntPtr intPtr = IntPtr.Zero;
			bool canSeek = bitmapStream.CanSeek;
			if (bitmapStream is UnmanagedMemoryStream)
			{
				UnmanagedMemoryStream unmanagedMemoryStream = bitmapStream as UnmanagedMemoryStream;
				IntPtr intPtr2 = IntPtr.Zero;
				intPtr2 = (IntPtr)((void*)unmanagedMemoryStream.PositionPointer);
				int bufferSize = (int)unmanagedMemoryStream.Length;
				if (intPtr2 != IntPtr.Zero)
				{
					intPtr = StreamAsIStream.IStreamFrom(intPtr2, bufferSize);
				}
			}
			else
			{
				intPtr = StreamAsIStream.IStreamFrom(bitmapStream);
				if (intPtr == IntPtr.Zero)
				{
					throw new InvalidOperationException(SR.Get("Image_CantDealWithStream"));
				}
				if (!canSeek || (!bitmapStream.CanWrite && bitmapStream.Length <= 1048576L))
				{
					IntPtr intPtr3 = StreamAsIStream.IStreamMemoryFrom(intPtr);
					if (intPtr3 != IntPtr.Zero)
					{
						UnsafeNativeMethods.MILUnknown.ReleaseInterface(ref intPtr);
						bitmapStream = Stream.Null;
						return intPtr3;
					}
					if (!canSeek)
					{
						throw new InvalidOperationException(SR.Get("Image_CantDealWithStream"));
					}
				}
			}
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidOperationException(SR.Get("Image_CantDealWithStream"));
			}
			return intPtr;
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x00106830 File Offset: 0x00105C30
		internal bool CanConvertToString()
		{
			return this._uri != null;
		}

		// Token: 0x0600438F RID: 17295
		internal abstract void SealObject();

		// Token: 0x0400186F RID: 6255
		private bool _isBuiltInDecoder;

		// Token: 0x04001870 RID: 6256
		private SafeMILHandle _decoderHandle;

		// Token: 0x04001871 RID: 6257
		private bool _shouldCacheDecoder = true;

		// Token: 0x04001872 RID: 6258
		private bool _isOriginalWritable;

		// Token: 0x04001873 RID: 6259
		private bool _isPaletteCached;

		// Token: 0x04001874 RID: 6260
		private BitmapPalette _palette;

		// Token: 0x04001875 RID: 6261
		private bool _isColorContextCached;

		// Token: 0x04001876 RID: 6262
		internal ReadOnlyCollection<ColorContext> _readOnlycolorContexts;

		// Token: 0x04001877 RID: 6263
		private bool _isThumbnailCached;

		// Token: 0x04001878 RID: 6264
		[SecurityCritical]
		private BitmapMetadata _metadata;

		// Token: 0x04001879 RID: 6265
		private bool _isMetadataCached;

		// Token: 0x0400187A RID: 6266
		private BitmapSource _thumbnail;

		// Token: 0x0400187B RID: 6267
		private BitmapCodecInfo _codecInfo;

		// Token: 0x0400187C RID: 6268
		private bool _isPreviewCached;

		// Token: 0x0400187D RID: 6269
		private BitmapSource _preview;

		// Token: 0x0400187E RID: 6270
		internal List<BitmapFrame> _frames;

		// Token: 0x0400187F RID: 6271
		internal ReadOnlyCollection<BitmapFrame> _readOnlyFrames;

		// Token: 0x04001880 RID: 6272
		internal Stream _stream;

		// Token: 0x04001881 RID: 6273
		internal Uri _uri;

		// Token: 0x04001882 RID: 6274
		internal Uri _baseUri;

		// Token: 0x04001883 RID: 6275
		[SecurityCritical]
		internal Stream _uriStream;

		// Token: 0x04001884 RID: 6276
		internal BitmapCreateOptions _createOptions;

		// Token: 0x04001885 RID: 6277
		internal BitmapCacheOption _cacheOption;

		// Token: 0x04001886 RID: 6278
		internal UniqueEventHelper _downloadEvent = new UniqueEventHelper();

		// Token: 0x04001887 RID: 6279
		internal UniqueEventHelper<DownloadProgressEventArgs> _progressEvent = new UniqueEventHelper<DownloadProgressEventArgs>();

		// Token: 0x04001888 RID: 6280
		internal UniqueEventHelper<ExceptionEventArgs> _failedEvent = new UniqueEventHelper<ExceptionEventArgs>();

		// Token: 0x04001889 RID: 6281
		private object _syncObject = new object();

		// Token: 0x0400188A RID: 6282
		private static SecurityCriticalDataForSet<bool> isImageDisabled;

		// Token: 0x0400188B RID: 6283
		[SecurityCritical]
		private static bool isImageDisabledInitialized = false;

		// Token: 0x0400188C RID: 6284
		private UnmanagedMemoryStream _unmanagedMemoryStream;

		// Token: 0x0400188D RID: 6285
		[SecurityCritical]
		private SafeFileHandle _safeFilehandle;

		// Token: 0x0400188E RID: 6286
		private BitmapDecoder _cachedDecoder;
	}
}
