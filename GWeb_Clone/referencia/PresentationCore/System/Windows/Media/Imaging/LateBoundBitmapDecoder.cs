using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Cache;
using System.Security;
using MS.Internal;

namespace System.Windows.Media.Imaging
{
	/// <summary>Define um decodificador que exige a criação de bitmap atrasada, como downloads de imagem assíncronos.</summary>
	// Token: 0x020005F1 RID: 1521
	public sealed class LateBoundBitmapDecoder : BitmapDecoder
	{
		// Token: 0x060045BA RID: 17850 RVA: 0x0010FDF4 File Offset: 0x0010F1F4
		[SecurityCritical]
		internal LateBoundBitmapDecoder(Uri baseUri, Uri uri, Stream stream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption, RequestCachePolicy requestCachePolicy) : base(true)
		{
			this._baseUri = baseUri;
			this._uri = uri;
			this._stream = stream;
			this._createOptions = createOptions;
			this._cacheOption = cacheOption;
			this._requestCachePolicy = requestCachePolicy;
			Uri uri2 = (this._baseUri != null) ? new Uri(this._baseUri, this._uri) : this._uri;
			if (uri2 != null && (uri2.Scheme == Uri.UriSchemeHttp || uri2.Scheme == Uri.UriSchemeHttps))
			{
				BitmapDownload.BeginDownload(this, uri2, this._requestCachePolicy, this._stream);
				this._isDownloading = true;
			}
			if (this._stream != null && !this._stream.CanSeek)
			{
				BitmapDownload.BeginDownload(this, uri2, this._requestCachePolicy, this._stream);
				this._isDownloading = true;
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Media.Imaging.BitmapPalette" /> que está associado a este decodificador.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.Imaging.BitmapPalette" /> associado a este decodificador. Se o bitmap não tiver nenhuma paleta ou se o <see cref="T:System.Windows.Media.Imaging.LateBoundBitmapDecoder" /> ainda está baixando conteúdo, essa propriedade retornará <see langword="null" />. Esta propriedade não tem valor padrão.</returns>
		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x060045BB RID: 17851 RVA: 0x0010FED4 File Offset: 0x0010F2D4
		public override BitmapPalette Palette
		{
			[SecurityCritical]
			get
			{
				base.VerifyAccess();
				if (this._isDownloading)
				{
					return null;
				}
				return this.Decoder.Palette;
			}
		}

		/// <summary>Obterá um valor que representa o perfil de cor que está associado a um bitmap, se algum tiver sido definido.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.ColorContext" /> que representa o perfil de cor incorporado do bitmap. Se nenhum perfil de cor foi definida ou se o <see cref="T:System.Windows.Media.Imaging.LateBoundBitmapDecoder" /> ainda está baixando conteúdo, essa propriedade retornará <see langword="null" />. Esta propriedade não tem valor padrão.</returns>
		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x060045BC RID: 17852 RVA: 0x0010FEFC File Offset: 0x0010F2FC
		public override ReadOnlyCollection<ColorContext> ColorContexts
		{
			[SecurityCritical]
			get
			{
				base.VerifyAccess();
				if (this._isDownloading)
				{
					return null;
				}
				return this.Decoder.ColorContexts;
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que representará a miniatura do bitmap, se alguma for definida.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que representa uma miniatura do bitmap. Se nenhuma miniatura é definida, ou se o <see cref="T:System.Windows.Media.Imaging.LateBoundBitmapDecoder" /> ainda está baixando conteúdo, essa propriedade retornará <see langword="null" />. Esta propriedade não tem valor padrão.</returns>
		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x060045BD RID: 17853 RVA: 0x0010FF24 File Offset: 0x0010F324
		public override BitmapSource Thumbnail
		{
			[SecurityCritical]
			get
			{
				base.VerifyAccess();
				if (this._isDownloading)
				{
					return null;
				}
				return this.Decoder.Thumbnail;
			}
		}

		/// <summary>Obtém informações que descrevem esse codec.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapCodecInfo" />. Esta propriedade não tem valor padrão. Se o decodificador associado tardia ainda está sendo baixado, essa propriedade retornará <see langword="null" />.</returns>
		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x060045BE RID: 17854 RVA: 0x0010FF4C File Offset: 0x0010F34C
		public override BitmapCodecInfo CodecInfo
		{
			[SecurityCritical]
			get
			{
				base.VerifyAccess();
				SecurityHelper.DemandRegistryPermission();
				if (this._isDownloading)
				{
					return null;
				}
				return this.Decoder.CodecInfo;
			}
		}

		/// <summary>Obtém o conteúdo de um quadro individual dentro de um bitmap.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapFrame" />. Esta propriedade não tem valor padrão.</returns>
		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x060045BF RID: 17855 RVA: 0x0010FF7C File Offset: 0x0010F37C
		public override ReadOnlyCollection<BitmapFrame> Frames
		{
			get
			{
				base.VerifyAccess();
				if (this._isDownloading)
				{
					if (this._readOnlyFrames == null)
					{
						this._frames = new List<BitmapFrame>(1);
						this._frames.Add(new BitmapFrameDecode(0, this._createOptions, this._cacheOption, this));
						this._readOnlyFrames = new ReadOnlyCollection<BitmapFrame>(this._frames);
					}
					return this._readOnlyFrames;
				}
				return this.Decoder.Frames;
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que representará a versão prévia global deste bitmap, se alguma for definida.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que representa a versão prévia global do bitmap. Se nenhuma visualização está definida ou se o <see cref="T:System.Windows.Media.Imaging.LateBoundBitmapDecoder" /> ainda está baixando conteúdo, essa propriedade retornará <see langword="null" />. Esta propriedade não tem valor padrão.</returns>
		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x060045C0 RID: 17856 RVA: 0x0010FFEC File Offset: 0x0010F3EC
		public override BitmapSource Preview
		{
			[SecurityCritical]
			get
			{
				base.VerifyAccess();
				if (this._isDownloading)
				{
					return null;
				}
				return this.Decoder.Preview;
			}
		}

		/// <summary>Obtém o decodificador subjacente que está associado a este decodificador de associação tardia.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.Imaging.BitmapDecoder" /> subjacente. Se o <see cref="T:System.Windows.Media.Imaging.LateBoundBitmapDecoder" /> é baixar ainda um bitmap, o decodificador subjacente é <see langword="null" />. Caso contrário, o decodificador subjacente é criado no primeiro acesso.</returns>
		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x060045C1 RID: 17857 RVA: 0x00110014 File Offset: 0x0010F414
		public BitmapDecoder Decoder
		{
			get
			{
				base.VerifyAccess();
				if (this._isDownloading || this._failed)
				{
					return null;
				}
				this.EnsureDecoder();
				return this._realDecoder;
			}
		}

		/// <summary>Obtém um valor que indica se o decodificador está fazendo o download do conteúdo no momento.</summary>
		/// <returns>
		///   <see langword="true" /> Se o decodificador está baixando conteúdo; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x060045C2 RID: 17858 RVA: 0x00110048 File Offset: 0x0010F448
		public override bool IsDownloading
		{
			get
			{
				base.VerifyAccess();
				return this._isDownloading;
			}
		}

		// Token: 0x060045C3 RID: 17859 RVA: 0x00110064 File Offset: 0x0010F464
		private void EnsureDecoder()
		{
			if (this._realDecoder == null)
			{
				this._realDecoder = BitmapDecoder.CreateFromUriOrStream(this._baseUri, this._uri, this._stream, this._createOptions & ~BitmapCreateOptions.DelayCreation, this._cacheOption, this._requestCachePolicy, true);
				if (this._readOnlyFrames != null)
				{
					this._realDecoder.SetupFrames(null, this._readOnlyFrames);
					this._readOnlyFrames = null;
					this._frames = null;
				}
			}
		}

		// Token: 0x060045C4 RID: 17860 RVA: 0x001100D4 File Offset: 0x0010F4D4
		internal object DownloadCallback(object arg)
		{
			Stream stream = (Stream)arg;
			this._stream = stream;
			if ((this._createOptions & BitmapCreateOptions.DelayCreation) == BitmapCreateOptions.None)
			{
				try
				{
					this.EnsureDecoder();
				}
				catch (Exception arg2)
				{
					return this.ExceptionCallback(arg2);
				}
			}
			this._isDownloading = false;
			this._downloadEvent.InvokeEvents(this, null);
			return null;
		}

		// Token: 0x060045C5 RID: 17861 RVA: 0x00110140 File Offset: 0x0010F540
		internal object ProgressCallback(object arg)
		{
			int percentComplete = (int)arg;
			this._progressEvent.InvokeEvents(this, new DownloadProgressEventArgs(percentComplete));
			return null;
		}

		// Token: 0x060045C6 RID: 17862 RVA: 0x00110168 File Offset: 0x0010F568
		internal object ExceptionCallback(object arg)
		{
			this._isDownloading = false;
			this._failed = true;
			this._failedEvent.InvokeEvents(this, new ExceptionEventArgs((Exception)arg));
			return null;
		}

		// Token: 0x060045C7 RID: 17863 RVA: 0x0011019C File Offset: 0x0010F59C
		internal override void SealObject()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400194B RID: 6475
		private bool _isDownloading;

		// Token: 0x0400194C RID: 6476
		private bool _failed;

		// Token: 0x0400194D RID: 6477
		private BitmapDecoder _realDecoder;

		// Token: 0x0400194E RID: 6478
		private RequestCachePolicy _requestCachePolicy;
	}
}
