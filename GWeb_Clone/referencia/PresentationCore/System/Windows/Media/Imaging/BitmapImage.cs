using System;
using System.ComponentModel;
using System.IO;
using System.Net.Cache;
using System.Security;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Fornece um <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> especializado que é otimizado para carregar imagens usando a linguagem XAML.</summary>
	// Token: 0x020005E5 RID: 1509
	public sealed class BitmapImage : BitmapSource, ISupportInitialize, IUriContext
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.BitmapImage" />.</summary>
		// Token: 0x060044D5 RID: 17621 RVA: 0x0010C300 File Offset: 0x0010B700
		public BitmapImage() : base(true)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.BitmapImage" /> usando o <see cref="T:System.Uri" /> fornecido.</summary>
		/// <param name="uriSource">O <see cref="T:System.Uri" /> a ser usado como a fonte da <see cref="T:System.Windows.Media.Imaging.BitmapImage" />.</param>
		/// <exception cref="T:System.ArgumentNullException">O parâmetro <paramref name="uriSource" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">O arquivo especificado pelo parâmetro <paramref name="uriSource" /> não foi encontrado.</exception>
		// Token: 0x060044D6 RID: 17622 RVA: 0x0010C314 File Offset: 0x0010B714
		public BitmapImage(Uri uriSource) : this(uriSource, null)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.BitmapImage" /> com uma imagem cuja origem é um <see cref="T:System.Uri" /> e é armazenada em cache de acordo com o <see cref="T:System.Net.Cache.RequestCachePolicy" /> fornecido.</summary>
		/// <param name="uriSource">O <see cref="T:System.Uri" /> a ser usado como a fonte da <see cref="T:System.Windows.Media.Imaging.BitmapImage" />.</param>
		/// <param name="uriCachePolicy">O <see cref="T:System.Net.Cache.RequestCachePolicy" /> que especifica os requisitos de cache de imagens que são obtidas usando HTTP.</param>
		/// <exception cref="T:System.ArgumentNullException">O parâmetro <paramref name="uriSource" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">O arquivo especificado pelo parâmetro <paramref name="uriSource" /> não foi encontrado.</exception>
		// Token: 0x060044D7 RID: 17623 RVA: 0x0010C32C File Offset: 0x0010B72C
		public BitmapImage(Uri uriSource, RequestCachePolicy uriCachePolicy) : base(true)
		{
			if (uriSource == null)
			{
				throw new ArgumentNullException("uriSource");
			}
			this.BeginInit();
			this.UriSource = uriSource;
			this.UriCachePolicy = uriCachePolicy;
			this.EndInit();
		}

		/// <summary>Sinaliza o início da inicialização <see cref="T:System.Windows.Media.Imaging.BitmapImage" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">O <see cref="T:System.Windows.Media.Imaging.BitmapImage" /> está sendo inicializado no momento. O <see cref="M:System.Windows.Media.Imaging.BitmapImage.BeginInit" /> já foi chamado.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.Imaging.BitmapImage" /> já foi inicializado.</exception>
		// Token: 0x060044D8 RID: 17624 RVA: 0x0010C370 File Offset: 0x0010B770
		public void BeginInit()
		{
			base.WritePreamble();
			this._bitmapInit.BeginInit();
		}

		/// <summary>Sinaliza o término da inicialização do <see cref="T:System.Windows.Media.Imaging.BitmapImage" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">As propriedades <see cref="P:System.Windows.Media.Imaging.BitmapImage.UriSource" /> ou <see cref="P:System.Windows.Media.Imaging.BitmapImage.StreamSource" /> são <see langword="null" />.  
		///
		/// ou - 
		/// O método <see cref="M:System.Windows.Media.Imaging.BitmapImage.EndInit" /> é chamado sem primeiro chamar <see cref="M:System.Windows.Media.Imaging.BitmapImage.BeginInit" />.</exception>
		// Token: 0x060044D9 RID: 17625 RVA: 0x0010C390 File Offset: 0x0010B790
		public void EndInit()
		{
			base.WritePreamble();
			this._bitmapInit.EndInit();
			if (this.UriSource == null && this.StreamSource == null)
			{
				throw new InvalidOperationException(SR.Get("Image_NeitherArgument", new object[]
				{
					"UriSource",
					"StreamSource"
				}));
			}
			if (this.UriSource != null && !this.UriSource.IsAbsoluteUri && this.CacheOption != BitmapCacheOption.OnLoad)
			{
				base.DelayCreation = true;
			}
			if (!base.DelayCreation && !base.CreationCompleted)
			{
				this.FinalizeCreation();
			}
		}

		/// <summary>Obtém ou define um valor que representa o <see cref="T:System.Uri" /> base do contexto de <see cref="T:System.Windows.Media.Imaging.BitmapImage" /> atual.</summary>
		/// <returns>A base <see cref="T:System.Uri" /> do contexto atual.</returns>
		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x060044DA RID: 17626 RVA: 0x0010C42C File Offset: 0x0010B82C
		// (set) Token: 0x060044DB RID: 17627 RVA: 0x0010C448 File Offset: 0x0010B848
		public Uri BaseUri
		{
			get
			{
				base.ReadPreamble();
				return this._baseUri;
			}
			set
			{
				base.WritePreamble();
				if (!base.CreationCompleted && this._baseUri != value)
				{
					this._baseUri = value;
					base.WritePostscript();
				}
			}
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.Media.Imaging.BitmapImage" /> está fazendo o download do conteúdo no momento.</summary>
		/// <returns>
		///   <see langword="true" /> Se o <see cref="T:System.Windows.Media.Imaging.BitmapImage" /> é baixar o conteúdo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x060044DC RID: 17628 RVA: 0x0010C480 File Offset: 0x0010B880
		public override bool IsDownloading
		{
			get
			{
				base.ReadPreamble();
				return this._isDownloading;
			}
		}

		/// <summary>Sem suporte. O <see cref="T:System.Windows.Media.Imaging.BitmapImage" /> não dá suporte à propriedade <see cref="P:System.Windows.Media.Imaging.BitmapImage.Metadata" /> e gerará um <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>Sem suporte.</returns>
		/// <exception cref="T:System.NotSupportedException">É feita uma tentativa de ler o <see cref="P:System.Windows.Media.Imaging.BitmapImage.Metadata" />.</exception>
		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x060044DD RID: 17629 RVA: 0x0010C49C File Offset: 0x0010B89C
		public override ImageMetadata Metadata
		{
			get
			{
				throw new NotSupportedException(SR.Get("Image_MetadataNotSupported"));
			}
		}

		// Token: 0x060044DE RID: 17630 RVA: 0x0010C4B8 File Offset: 0x0010B8B8
		internal override bool CanSerializeToString()
		{
			base.ReadPreamble();
			return this.UriSource != null && this.StreamSource == null && this.SourceRect.IsEmpty && this.DecodePixelWidth == 0 && this.DecodePixelHeight == 0 && this.Rotation == Rotation.Rotate0 && this.CreateOptions == BitmapCreateOptions.None && this.CacheOption == BitmapCacheOption.Default && this.UriCachePolicy == null;
		}

		// Token: 0x060044DF RID: 17631 RVA: 0x0010C524 File Offset: 0x0010B924
		internal override string ConvertToString(string format, IFormatProvider provider)
		{
			base.ReadPreamble();
			if (!(this.UriSource != null))
			{
				return base.ConvertToString(format, provider);
			}
			if (this._baseUri != null)
			{
				return BindUriHelper.UriToString(new Uri(this._baseUri, this.UriSource));
			}
			return BindUriHelper.UriToString(this.UriSource);
		}

		// Token: 0x060044E0 RID: 17632 RVA: 0x0010C580 File Offset: 0x0010B980
		private void ClonePrequel(BitmapImage otherBitmapImage)
		{
			this.BeginInit();
			this._isDownloading = otherBitmapImage._isDownloading;
			this._decoder = otherBitmapImage._decoder;
			this._baseUri = otherBitmapImage._baseUri;
		}

		// Token: 0x060044E1 RID: 17633 RVA: 0x0010C5B8 File Offset: 0x0010B9B8
		private void ClonePostscript(BitmapImage otherBitmapImage)
		{
			if (this._isDownloading)
			{
				this._decoder.DownloadProgress += this.OnDownloadProgress;
				this._decoder.DownloadCompleted += this.OnDownloadCompleted;
				this._decoder.DownloadFailed += this.OnDownloadFailed;
			}
			this.EndInit();
		}

		// Token: 0x060044E2 RID: 17634 RVA: 0x0010C618 File Offset: 0x0010BA18
		private BitmapImage CheckCache(Uri uri)
		{
			if (uri != null)
			{
				WeakReference weakReference = ImagingCache.CheckImageCache(uri) as WeakReference;
				if (weakReference != null)
				{
					BitmapImage bitmapImage = weakReference.Target as BitmapImage;
					if (bitmapImage != null)
					{
						return bitmapImage;
					}
					ImagingCache.RemoveFromImageCache(uri);
				}
			}
			return null;
		}

		// Token: 0x060044E3 RID: 17635 RVA: 0x0010C658 File Offset: 0x0010BA58
		private void InsertInCache(Uri uri)
		{
			if (uri != null)
			{
				ImagingCache.AddToImageCache(uri, new WeakReference(this));
			}
		}

		// Token: 0x060044E4 RID: 17636 RVA: 0x0010C67C File Offset: 0x0010BA7C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal override void FinalizeCreation()
		{
			this._bitmapInit.EnsureInitializedComplete();
			Uri uri = this.UriSource;
			if (this._baseUri != null)
			{
				uri = new Uri(this._baseUri, this.UriSource);
			}
			if ((this.CreateOptions & BitmapCreateOptions.IgnoreImageCache) != BitmapCreateOptions.None)
			{
				ImagingCache.RemoveFromImageCache(uri);
			}
			BitmapImage bitmapImage = this.CheckCache(uri);
			if (bitmapImage != null && bitmapImage.CheckAccess() && bitmapImage.SourceRect.Equals(this.SourceRect) && bitmapImage.DecodePixelWidth == this.DecodePixelWidth && bitmapImage.DecodePixelHeight == this.DecodePixelHeight && bitmapImage.Rotation == this.Rotation && (bitmapImage.CreateOptions & BitmapCreateOptions.IgnoreColorProfile) == (this.CreateOptions & BitmapCreateOptions.IgnoreColorProfile))
			{
				this._syncObject = bitmapImage.SyncObject;
				object syncObject = this._syncObject;
				lock (syncObject)
				{
					base.WicSourceHandle = bitmapImage.WicSourceHandle;
					base.IsSourceCached = bitmapImage.IsSourceCached;
					this._convertedDUCEPtr = bitmapImage._convertedDUCEPtr;
					this._cachedBitmapImage = bitmapImage;
				}
				this.UpdateCachedSettings();
				return;
			}
			BitmapDecoder bitmapDecoder = null;
			if (this._decoder == null)
			{
				bitmapDecoder = BitmapDecoder.CreateFromUriOrStream(this._baseUri, this.UriSource, this.StreamSource, this.CreateOptions & ~BitmapCreateOptions.DelayCreation, BitmapCacheOption.None, this._uriCachePolicy, false);
				if (bitmapDecoder.IsDownloading)
				{
					this._isDownloading = true;
					this._decoder = bitmapDecoder;
					bitmapDecoder.DownloadProgress += this.OnDownloadProgress;
					bitmapDecoder.DownloadCompleted += this.OnDownloadCompleted;
					bitmapDecoder.DownloadFailed += this.OnDownloadFailed;
				}
			}
			else
			{
				bitmapDecoder = this._decoder;
				this._decoder = null;
			}
			if (bitmapDecoder.Frames.Count == 0)
			{
				throw new ArgumentException(SR.Get("Image_NoDecodeFrames"));
			}
			BitmapFrame bitmapFrame = bitmapDecoder.Frames[0];
			BitmapSource bitmapSource = bitmapFrame;
			Int32Rect sourceRect = this.SourceRect;
			if (sourceRect.X == 0 && sourceRect.Y == 0 && sourceRect.Width == bitmapSource.PixelWidth && sourceRect.Height == bitmapSource.PixelHeight)
			{
				sourceRect = Int32Rect.Empty;
			}
			if (!sourceRect.IsEmpty)
			{
				CroppedBitmap croppedBitmap = new CroppedBitmap();
				croppedBitmap.BeginInit();
				croppedBitmap.Source = bitmapSource;
				croppedBitmap.SourceRect = sourceRect;
				croppedBitmap.EndInit();
				bitmapSource = croppedBitmap;
				if (this._isDownloading)
				{
					bitmapSource.UnregisterDownloadEventSource();
				}
			}
			int num = this.DecodePixelWidth;
			int num2 = this.DecodePixelHeight;
			if (num == 0 && num2 == 0)
			{
				num = bitmapSource.PixelWidth;
				num2 = bitmapSource.PixelHeight;
			}
			else if (num == 0)
			{
				num = bitmapSource.PixelWidth * num2 / bitmapSource.PixelHeight;
			}
			else if (num2 == 0)
			{
				num2 = bitmapSource.PixelHeight * num / bitmapSource.PixelWidth;
			}
			if (num != bitmapSource.PixelWidth || num2 != bitmapSource.PixelHeight || this.Rotation != Rotation.Rotate0)
			{
				TransformedBitmap transformedBitmap = new TransformedBitmap();
				transformedBitmap.BeginInit();
				transformedBitmap.Source = bitmapSource;
				TransformGroup transformGroup = new TransformGroup();
				if (num != bitmapSource.PixelWidth || num2 != bitmapSource.PixelHeight)
				{
					int pixelWidth = bitmapSource.PixelWidth;
					int pixelHeight = bitmapSource.PixelHeight;
					transformGroup.Children.Add(new ScaleTransform(1.0 * (double)num / (double)pixelWidth, 1.0 * (double)num2 / (double)pixelHeight));
				}
				if (this.Rotation != Rotation.Rotate0)
				{
					double angle = 0.0;
					switch (this.Rotation)
					{
					case Rotation.Rotate0:
						angle = 0.0;
						break;
					case Rotation.Rotate90:
						angle = 90.0;
						break;
					case Rotation.Rotate180:
						angle = 180.0;
						break;
					case Rotation.Rotate270:
						angle = 270.0;
						break;
					}
					transformGroup.Children.Add(new RotateTransform(angle));
				}
				transformedBitmap.Transform = transformGroup;
				transformedBitmap.EndInit();
				bitmapSource = transformedBitmap;
				if (this._isDownloading)
				{
					bitmapSource.UnregisterDownloadEventSource();
				}
			}
			if ((this.CreateOptions & BitmapCreateOptions.IgnoreColorProfile) == BitmapCreateOptions.None && bitmapFrame.ColorContexts != null && bitmapFrame.ColorContexts[0] != null && bitmapFrame.ColorContexts[0].IsValid && bitmapSource.Format.Format != PixelFormatEnum.Default)
			{
				PixelFormat closestDUCEFormat = BitmapSource.GetClosestDUCEFormat(bitmapSource.Format, bitmapSource.Palette);
				bool flag2 = bitmapSource.Format != closestDUCEFormat;
				ColorContext colorContext;
				try
				{
					colorContext = new ColorContext(closestDUCEFormat);
				}
				catch (NotSupportedException)
				{
					colorContext = null;
				}
				if (colorContext != null)
				{
					bool flag3 = false;
					bool flag4 = false;
					try
					{
						ColorConvertedBitmap colorConvertedBitmap = new ColorConvertedBitmap(bitmapSource, bitmapFrame.ColorContexts[0], colorContext, closestDUCEFormat);
						bitmapSource = colorConvertedBitmap;
						if (this._isDownloading)
						{
							bitmapSource.UnregisterDownloadEventSource();
						}
						flag3 = true;
					}
					catch (NotSupportedException)
					{
					}
					catch (FileFormatException)
					{
						flag4 = true;
					}
					if (!flag3 && !flag4 && flag2)
					{
						FormatConvertedBitmap source = new FormatConvertedBitmap(bitmapSource, closestDUCEFormat, bitmapSource.Palette, 0.0);
						ColorConvertedBitmap colorConvertedBitmap2 = new ColorConvertedBitmap(source, bitmapFrame.ColorContexts[0], colorContext, closestDUCEFormat);
						bitmapSource = colorConvertedBitmap2;
						if (this._isDownloading)
						{
							bitmapSource.UnregisterDownloadEventSource();
						}
					}
				}
			}
			if (this.CacheOption != BitmapCacheOption.None)
			{
				try
				{
					CachedBitmap cachedBitmap = new CachedBitmap(bitmapSource, this.CreateOptions & ~BitmapCreateOptions.DelayCreation, this.CacheOption);
					bitmapSource = cachedBitmap;
					if (this._isDownloading)
					{
						bitmapSource.UnregisterDownloadEventSource();
					}
				}
				catch (Exception e)
				{
					base.RecoverFromDecodeFailure(e);
					base.CreationCompleted = true;
					return;
				}
			}
			if (bitmapDecoder != null && this.CacheOption == BitmapCacheOption.OnLoad)
			{
				bitmapDecoder.CloseStream();
			}
			else if (this.CacheOption != BitmapCacheOption.OnLoad)
			{
				this._finalSource = bitmapSource;
			}
			base.WicSourceHandle = bitmapSource.WicSourceHandle;
			base.IsSourceCached = bitmapSource.IsSourceCached;
			base.CreationCompleted = true;
			this.UpdateCachedSettings();
			if (!this.IsDownloading)
			{
				this.InsertInCache(uri);
			}
		}

		// Token: 0x060044E5 RID: 17637 RVA: 0x0010CCAC File Offset: 0x0010C0AC
		private void UriCachePolicyPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			if (!e.IsASubPropertyChange)
			{
				this._uriCachePolicy = (e.NewValue as RequestCachePolicy);
			}
		}

		// Token: 0x060044E6 RID: 17638 RVA: 0x0010CCD4 File Offset: 0x0010C0D4
		private void UriSourcePropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			if (!e.IsASubPropertyChange)
			{
				this._uriSource = (e.NewValue as Uri);
			}
		}

		// Token: 0x060044E7 RID: 17639 RVA: 0x0010CCFC File Offset: 0x0010C0FC
		private void StreamSourcePropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			if (!e.IsASubPropertyChange)
			{
				this._streamSource = (e.NewValue as Stream);
			}
		}

		// Token: 0x060044E8 RID: 17640 RVA: 0x0010CD24 File Offset: 0x0010C124
		private void DecodePixelWidthPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			if (!e.IsASubPropertyChange)
			{
				this._decodePixelWidth = (int)e.NewValue;
			}
		}

		// Token: 0x060044E9 RID: 17641 RVA: 0x0010CD4C File Offset: 0x0010C14C
		private void DecodePixelHeightPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			if (!e.IsASubPropertyChange)
			{
				this._decodePixelHeight = (int)e.NewValue;
			}
		}

		// Token: 0x060044EA RID: 17642 RVA: 0x0010CD74 File Offset: 0x0010C174
		private void RotationPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			if (!e.IsASubPropertyChange)
			{
				this._rotation = (Rotation)e.NewValue;
			}
		}

		// Token: 0x060044EB RID: 17643 RVA: 0x0010CD9C File Offset: 0x0010C19C
		private void SourceRectPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			if (!e.IsASubPropertyChange)
			{
				this._sourceRect = (Int32Rect)e.NewValue;
			}
		}

		// Token: 0x060044EC RID: 17644 RVA: 0x0010CDC4 File Offset: 0x0010C1C4
		private void CreateOptionsPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			BitmapCreateOptions bitmapCreateOptions = (BitmapCreateOptions)e.NewValue;
			this._createOptions = bitmapCreateOptions;
			base.DelayCreation = ((bitmapCreateOptions & BitmapCreateOptions.DelayCreation) > BitmapCreateOptions.None);
		}

		// Token: 0x060044ED RID: 17645 RVA: 0x0010CDF4 File Offset: 0x0010C1F4
		private void CacheOptionPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			if (!e.IsASubPropertyChange)
			{
				this._cacheOption = (BitmapCacheOption)e.NewValue;
			}
		}

		// Token: 0x060044EE RID: 17646 RVA: 0x0010CE1C File Offset: 0x0010C21C
		private static object CoerceUriCachePolicy(DependencyObject d, object value)
		{
			BitmapImage bitmapImage = (BitmapImage)d;
			if (!bitmapImage._bitmapInit.IsInInit)
			{
				return bitmapImage._uriCachePolicy;
			}
			return value;
		}

		// Token: 0x060044EF RID: 17647 RVA: 0x0010CE48 File Offset: 0x0010C248
		private static object CoerceUriSource(DependencyObject d, object value)
		{
			BitmapImage bitmapImage = (BitmapImage)d;
			if (!bitmapImage._bitmapInit.IsInInit)
			{
				return bitmapImage._uriSource;
			}
			return value;
		}

		// Token: 0x060044F0 RID: 17648 RVA: 0x0010CE74 File Offset: 0x0010C274
		private static object CoerceStreamSource(DependencyObject d, object value)
		{
			BitmapImage bitmapImage = (BitmapImage)d;
			if (!bitmapImage._bitmapInit.IsInInit)
			{
				return bitmapImage._streamSource;
			}
			return value;
		}

		// Token: 0x060044F1 RID: 17649 RVA: 0x0010CEA0 File Offset: 0x0010C2A0
		private static object CoerceDecodePixelWidth(DependencyObject d, object value)
		{
			BitmapImage bitmapImage = (BitmapImage)d;
			if (!bitmapImage._bitmapInit.IsInInit)
			{
				return bitmapImage._decodePixelWidth;
			}
			return value;
		}

		// Token: 0x060044F2 RID: 17650 RVA: 0x0010CED0 File Offset: 0x0010C2D0
		private static object CoerceDecodePixelHeight(DependencyObject d, object value)
		{
			BitmapImage bitmapImage = (BitmapImage)d;
			if (!bitmapImage._bitmapInit.IsInInit)
			{
				return bitmapImage._decodePixelHeight;
			}
			return value;
		}

		// Token: 0x060044F3 RID: 17651 RVA: 0x0010CF00 File Offset: 0x0010C300
		private static object CoerceRotation(DependencyObject d, object value)
		{
			BitmapImage bitmapImage = (BitmapImage)d;
			if (!bitmapImage._bitmapInit.IsInInit)
			{
				return bitmapImage._rotation;
			}
			return value;
		}

		// Token: 0x060044F4 RID: 17652 RVA: 0x0010CF30 File Offset: 0x0010C330
		private static object CoerceSourceRect(DependencyObject d, object value)
		{
			BitmapImage bitmapImage = (BitmapImage)d;
			if (!bitmapImage._bitmapInit.IsInInit)
			{
				return bitmapImage._sourceRect;
			}
			return value;
		}

		// Token: 0x060044F5 RID: 17653 RVA: 0x0010CF60 File Offset: 0x0010C360
		private static object CoerceCreateOptions(DependencyObject d, object value)
		{
			BitmapImage bitmapImage = (BitmapImage)d;
			if (!bitmapImage._bitmapInit.IsInInit)
			{
				return bitmapImage._createOptions;
			}
			return value;
		}

		// Token: 0x060044F6 RID: 17654 RVA: 0x0010CF90 File Offset: 0x0010C390
		private static object CoerceCacheOption(DependencyObject d, object value)
		{
			BitmapImage bitmapImage = (BitmapImage)d;
			if (!bitmapImage._bitmapInit.IsInInit)
			{
				return bitmapImage._cacheOption;
			}
			return value;
		}

		// Token: 0x060044F7 RID: 17655 RVA: 0x0010CFC0 File Offset: 0x0010C3C0
		private void OnDownloadCompleted(object sender, EventArgs e)
		{
			this._isDownloading = false;
			this._decoder.DownloadProgress -= this.OnDownloadProgress;
			this._decoder.DownloadCompleted -= this.OnDownloadCompleted;
			this._decoder.DownloadFailed -= this.OnDownloadFailed;
			if ((this.CreateOptions & BitmapCreateOptions.DelayCreation) != BitmapCreateOptions.None)
			{
				base.DelayCreation = true;
			}
			else
			{
				this.FinalizeCreation();
				this._needsUpdate = true;
				base.RegisterForAsyncUpdateResource();
				base.FireChanged();
			}
			this._downloadEvent.InvokeEvents(this, null);
		}

		// Token: 0x060044F8 RID: 17656 RVA: 0x0010D054 File Offset: 0x0010C454
		private void OnDownloadProgress(object sender, DownloadProgressEventArgs e)
		{
			this._progressEvent.InvokeEvents(this, e);
		}

		// Token: 0x060044F9 RID: 17657 RVA: 0x0010D070 File Offset: 0x0010C470
		private void OnDownloadFailed(object sender, ExceptionEventArgs e)
		{
			this._isDownloading = false;
			this._decoder.DownloadProgress -= this.OnDownloadProgress;
			this._decoder.DownloadCompleted -= this.OnDownloadCompleted;
			this._decoder.DownloadFailed -= this.OnDownloadFailed;
			this._failedEvent.InvokeEvents(this, e);
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Imaging.BitmapImage" />, fazendo cópias em profundidade dos valores do objeto.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true." /></returns>
		// Token: 0x060044FA RID: 17658 RVA: 0x0010D0D8 File Offset: 0x0010C4D8
		public new BitmapImage Clone()
		{
			return (BitmapImage)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Imaging.BitmapImage" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true" />.</returns>
		// Token: 0x060044FB RID: 17659 RVA: 0x0010D0F0 File Offset: 0x0010C4F0
		public new BitmapImage CloneCurrentValue()
		{
			return (BitmapImage)base.CloneCurrentValue();
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x0010D108 File Offset: 0x0010C508
		private static void UriCachePolicyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BitmapImage bitmapImage = (BitmapImage)d;
			bitmapImage.UriCachePolicyPropertyChangedHook(e);
			bitmapImage.PropertyChanged(BitmapImage.UriCachePolicyProperty);
		}

		// Token: 0x060044FD RID: 17661 RVA: 0x0010D130 File Offset: 0x0010C530
		private static void UriSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BitmapImage bitmapImage = (BitmapImage)d;
			bitmapImage.UriSourcePropertyChangedHook(e);
			bitmapImage.PropertyChanged(BitmapImage.UriSourceProperty);
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x0010D158 File Offset: 0x0010C558
		private static void StreamSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BitmapImage bitmapImage = (BitmapImage)d;
			bitmapImage.StreamSourcePropertyChangedHook(e);
			bitmapImage.PropertyChanged(BitmapImage.StreamSourceProperty);
		}

		// Token: 0x060044FF RID: 17663 RVA: 0x0010D180 File Offset: 0x0010C580
		private static void DecodePixelWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BitmapImage bitmapImage = (BitmapImage)d;
			bitmapImage.DecodePixelWidthPropertyChangedHook(e);
			bitmapImage.PropertyChanged(BitmapImage.DecodePixelWidthProperty);
		}

		// Token: 0x06004500 RID: 17664 RVA: 0x0010D1A8 File Offset: 0x0010C5A8
		private static void DecodePixelHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BitmapImage bitmapImage = (BitmapImage)d;
			bitmapImage.DecodePixelHeightPropertyChangedHook(e);
			bitmapImage.PropertyChanged(BitmapImage.DecodePixelHeightProperty);
		}

		// Token: 0x06004501 RID: 17665 RVA: 0x0010D1D0 File Offset: 0x0010C5D0
		private static void RotationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BitmapImage bitmapImage = (BitmapImage)d;
			bitmapImage.RotationPropertyChangedHook(e);
			bitmapImage.PropertyChanged(BitmapImage.RotationProperty);
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x0010D1F8 File Offset: 0x0010C5F8
		private static void SourceRectPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BitmapImage bitmapImage = (BitmapImage)d;
			bitmapImage.SourceRectPropertyChangedHook(e);
			bitmapImage.PropertyChanged(BitmapImage.SourceRectProperty);
		}

		// Token: 0x06004503 RID: 17667 RVA: 0x0010D220 File Offset: 0x0010C620
		private static void CreateOptionsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BitmapImage bitmapImage = (BitmapImage)d;
			bitmapImage.CreateOptionsPropertyChangedHook(e);
			bitmapImage.PropertyChanged(BitmapImage.CreateOptionsProperty);
		}

		// Token: 0x06004504 RID: 17668 RVA: 0x0010D248 File Offset: 0x0010C648
		private static void CacheOptionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BitmapImage bitmapImage = (BitmapImage)d;
			bitmapImage.CacheOptionPropertyChangedHook(e);
			bitmapImage.PropertyChanged(BitmapImage.CacheOptionProperty);
		}

		/// <summary>Obtém ou define um valor que representa a política de caching para imagens que vêm de uma fonte HTTP.</summary>
		/// <returns>A base <see cref="T:System.Net.Cache.RequestCachePolicy" /> do contexto atual. O padrão é <see langword="null" />.</returns>
		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06004505 RID: 17669 RVA: 0x0010D270 File Offset: 0x0010C670
		// (set) Token: 0x06004506 RID: 17670 RVA: 0x0010D290 File Offset: 0x0010C690
		[TypeConverter(typeof(RequestCachePolicyConverter))]
		public RequestCachePolicy UriCachePolicy
		{
			get
			{
				return (RequestCachePolicy)base.GetValue(BitmapImage.UriCachePolicyProperty);
			}
			set
			{
				base.SetValueInternal(BitmapImage.UriCachePolicyProperty, value);
			}
		}

		/// <summary>Obtém ou define a origem <see cref="T:System.Uri" /> do <see cref="T:System.Windows.Media.Imaging.BitmapImage" />.</summary>
		/// <returns>A fonte de <see cref="T:System.Uri" /> do <see cref="T:System.Windows.Media.Imaging.BitmapImage" />. O padrão é <see langword="null" />.</returns>
		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06004507 RID: 17671 RVA: 0x0010D2AC File Offset: 0x0010C6AC
		// (set) Token: 0x06004508 RID: 17672 RVA: 0x0010D2CC File Offset: 0x0010C6CC
		public Uri UriSource
		{
			get
			{
				return (Uri)base.GetValue(BitmapImage.UriSourceProperty);
			}
			set
			{
				base.SetValueInternal(BitmapImage.UriSourceProperty, value);
			}
		}

		/// <summary>Obtém ou define a fonte do fluxo do <see cref="T:System.Windows.Media.Imaging.BitmapImage" />.</summary>
		/// <returns>A fonte do fluxo do <see cref="T:System.Windows.Media.Imaging.BitmapImage" />. O padrão é <see langword="null" />.</returns>
		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06004509 RID: 17673 RVA: 0x0010D2E8 File Offset: 0x0010C6E8
		// (set) Token: 0x0600450A RID: 17674 RVA: 0x0010D308 File Offset: 0x0010C708
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Stream StreamSource
		{
			get
			{
				return (Stream)base.GetValue(BitmapImage.StreamSourceProperty);
			}
			set
			{
				base.SetValueInternal(BitmapImage.StreamSourceProperty, value);
			}
		}

		/// <summary>Obtém ou define a largura, em pixels, para qual a imagem é decodificada.</summary>
		/// <returns>A largura, em pixels, para qual a imagem é decodificada. O valor padrão é 0.</returns>
		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x0600450B RID: 17675 RVA: 0x0010D324 File Offset: 0x0010C724
		// (set) Token: 0x0600450C RID: 17676 RVA: 0x0010D344 File Offset: 0x0010C744
		public int DecodePixelWidth
		{
			get
			{
				return (int)base.GetValue(BitmapImage.DecodePixelWidthProperty);
			}
			set
			{
				base.SetValueInternal(BitmapImage.DecodePixelWidthProperty, value);
			}
		}

		/// <summary>Obtém ou define a altura, em pixels, para qual a imagem é decodificada.</summary>
		/// <returns>A altura, em pixels, para qual a imagem é decodificada. O valor padrão é 0.</returns>
		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x0600450D RID: 17677 RVA: 0x0010D364 File Offset: 0x0010C764
		// (set) Token: 0x0600450E RID: 17678 RVA: 0x0010D384 File Offset: 0x0010C784
		public int DecodePixelHeight
		{
			get
			{
				return (int)base.GetValue(BitmapImage.DecodePixelHeightProperty);
			}
			set
			{
				base.SetValueInternal(BitmapImage.DecodePixelHeightProperty, value);
			}
		}

		/// <summary>Obtém ou define o ângulo no qual essa <see cref="T:System.Windows.Media.Imaging.BitmapImage" /> é girada.</summary>
		/// <returns>A rotação que é usada para a <see cref="T:System.Windows.Media.Imaging.BitmapImage" />. O padrão é <see cref="F:System.Windows.Media.Imaging.Rotation.Rotate0" />.</returns>
		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x0600450F RID: 17679 RVA: 0x0010D3A4 File Offset: 0x0010C7A4
		// (set) Token: 0x06004510 RID: 17680 RVA: 0x0010D3C4 File Offset: 0x0010C7C4
		public Rotation Rotation
		{
			get
			{
				return (Rotation)base.GetValue(BitmapImage.RotationProperty);
			}
			set
			{
				base.SetValueInternal(BitmapImage.RotationProperty, value);
			}
		}

		/// <summary>Obtém ou define o retângulo usado como a origem do <see cref="T:System.Windows.Media.Imaging.BitmapImage" />.</summary>
		/// <returns>O retângulo usado como a origem do <see cref="T:System.Windows.Media.Imaging.BitmapImage" />. O padrão é <see cref="P:System.Windows.Int32Rect.Empty" />.</returns>
		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06004511 RID: 17681 RVA: 0x0010D3E4 File Offset: 0x0010C7E4
		// (set) Token: 0x06004512 RID: 17682 RVA: 0x0010D404 File Offset: 0x0010C804
		public Int32Rect SourceRect
		{
			get
			{
				return (Int32Rect)base.GetValue(BitmapImage.SourceRectProperty);
			}
			set
			{
				base.SetValueInternal(BitmapImage.SourceRectProperty, value);
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Imaging.BitmapCreateOptions" /> para um <see cref="T:System.Windows.Media.Imaging.BitmapImage" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.Imaging.BitmapCreateOptions" /> usado para esse <see cref="T:System.Windows.Media.Imaging.BitmapImage" />. O padrão é <see cref="F:System.Windows.Media.Imaging.BitmapCreateOptions.None" />.</returns>
		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06004513 RID: 17683 RVA: 0x0010D424 File Offset: 0x0010C824
		// (set) Token: 0x06004514 RID: 17684 RVA: 0x0010D444 File Offset: 0x0010C844
		public BitmapCreateOptions CreateOptions
		{
			get
			{
				return (BitmapCreateOptions)base.GetValue(BitmapImage.CreateOptionsProperty);
			}
			set
			{
				base.SetValueInternal(BitmapImage.CreateOptionsProperty, value);
			}
		}

		/// <summary>Obtém ou define a <see cref="T:System.Windows.Media.Imaging.BitmapCacheOption" /> a ser usada para esta instância da <see cref="T:System.Windows.Media.Imaging.BitmapImage" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Media.Imaging.BitmapCacheOption" /> que está sendo usada para a <see cref="T:System.Windows.Media.Imaging.BitmapImage" />. O padrão é <see cref="F:System.Windows.Media.Imaging.BitmapCacheOption.Default" />.</returns>
		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06004515 RID: 17685 RVA: 0x0010D464 File Offset: 0x0010C864
		// (set) Token: 0x06004516 RID: 17686 RVA: 0x0010D484 File Offset: 0x0010C884
		public BitmapCacheOption CacheOption
		{
			get
			{
				return (BitmapCacheOption)base.GetValue(BitmapImage.CacheOptionProperty);
			}
			set
			{
				base.SetValueInternal(BitmapImage.CacheOptionProperty, value);
			}
		}

		// Token: 0x06004517 RID: 17687 RVA: 0x0010D4A4 File Offset: 0x0010C8A4
		protected override Freezable CreateInstanceCore()
		{
			return new BitmapImage();
		}

		// Token: 0x06004518 RID: 17688 RVA: 0x0010D4B8 File Offset: 0x0010C8B8
		protected override void CloneCore(Freezable source)
		{
			BitmapImage otherBitmapImage = (BitmapImage)source;
			this.ClonePrequel(otherBitmapImage);
			base.CloneCore(source);
			this.ClonePostscript(otherBitmapImage);
		}

		// Token: 0x06004519 RID: 17689 RVA: 0x0010D4E4 File Offset: 0x0010C8E4
		protected override void CloneCurrentValueCore(Freezable source)
		{
			BitmapImage otherBitmapImage = (BitmapImage)source;
			this.ClonePrequel(otherBitmapImage);
			base.CloneCurrentValueCore(source);
			this.ClonePostscript(otherBitmapImage);
		}

		// Token: 0x0600451A RID: 17690 RVA: 0x0010D510 File Offset: 0x0010C910
		protected override void GetAsFrozenCore(Freezable source)
		{
			BitmapImage otherBitmapImage = (BitmapImage)source;
			this.ClonePrequel(otherBitmapImage);
			base.GetAsFrozenCore(source);
			this.ClonePostscript(otherBitmapImage);
		}

		// Token: 0x0600451B RID: 17691 RVA: 0x0010D53C File Offset: 0x0010C93C
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			BitmapImage otherBitmapImage = (BitmapImage)source;
			this.ClonePrequel(otherBitmapImage);
			base.GetCurrentValueAsFrozenCore(source);
			this.ClonePostscript(otherBitmapImage);
		}

		// Token: 0x0600451C RID: 17692 RVA: 0x0010D568 File Offset: 0x0010C968
		static BitmapImage()
		{
			Type typeFromHandle = typeof(BitmapImage);
			BitmapImage.UriCachePolicyProperty = Animatable.RegisterProperty("UriCachePolicy", typeof(RequestCachePolicy), typeFromHandle, null, new PropertyChangedCallback(BitmapImage.UriCachePolicyPropertyChanged), null, false, new CoerceValueCallback(BitmapImage.CoerceUriCachePolicy));
			BitmapImage.UriSourceProperty = Animatable.RegisterProperty("UriSource", typeof(Uri), typeFromHandle, null, new PropertyChangedCallback(BitmapImage.UriSourcePropertyChanged), null, false, new CoerceValueCallback(BitmapImage.CoerceUriSource));
			BitmapImage.StreamSourceProperty = Animatable.RegisterProperty("StreamSource", typeof(Stream), typeFromHandle, null, new PropertyChangedCallback(BitmapImage.StreamSourcePropertyChanged), null, false, new CoerceValueCallback(BitmapImage.CoerceStreamSource));
			BitmapImage.DecodePixelWidthProperty = Animatable.RegisterProperty("DecodePixelWidth", typeof(int), typeFromHandle, 0, new PropertyChangedCallback(BitmapImage.DecodePixelWidthPropertyChanged), null, false, new CoerceValueCallback(BitmapImage.CoerceDecodePixelWidth));
			BitmapImage.DecodePixelHeightProperty = Animatable.RegisterProperty("DecodePixelHeight", typeof(int), typeFromHandle, 0, new PropertyChangedCallback(BitmapImage.DecodePixelHeightPropertyChanged), null, false, new CoerceValueCallback(BitmapImage.CoerceDecodePixelHeight));
			BitmapImage.RotationProperty = Animatable.RegisterProperty("Rotation", typeof(Rotation), typeFromHandle, Rotation.Rotate0, new PropertyChangedCallback(BitmapImage.RotationPropertyChanged), new ValidateValueCallback(ValidateEnums.IsRotationValid), false, new CoerceValueCallback(BitmapImage.CoerceRotation));
			BitmapImage.SourceRectProperty = Animatable.RegisterProperty("SourceRect", typeof(Int32Rect), typeFromHandle, Int32Rect.Empty, new PropertyChangedCallback(BitmapImage.SourceRectPropertyChanged), null, false, new CoerceValueCallback(BitmapImage.CoerceSourceRect));
			BitmapImage.CreateOptionsProperty = Animatable.RegisterProperty("CreateOptions", typeof(BitmapCreateOptions), typeFromHandle, BitmapCreateOptions.None, new PropertyChangedCallback(BitmapImage.CreateOptionsPropertyChanged), null, false, new CoerceValueCallback(BitmapImage.CoerceCreateOptions));
			BitmapImage.CacheOptionProperty = Animatable.RegisterProperty("CacheOption", typeof(BitmapCacheOption), typeFromHandle, BitmapCacheOption.Default, new PropertyChangedCallback(BitmapImage.CacheOptionPropertyChanged), null, false, new CoerceValueCallback(BitmapImage.CoerceCacheOption));
		}

		// Token: 0x04001903 RID: 6403
		private Uri _baseUri;

		// Token: 0x04001904 RID: 6404
		private bool _isDownloading;

		// Token: 0x04001905 RID: 6405
		private BitmapDecoder _decoder;

		// Token: 0x04001906 RID: 6406
		private RequestCachePolicy _uriCachePolicy;

		// Token: 0x04001907 RID: 6407
		private Uri _uriSource;

		// Token: 0x04001908 RID: 6408
		private Stream _streamSource;

		// Token: 0x04001909 RID: 6409
		private int _decodePixelWidth;

		// Token: 0x0400190A RID: 6410
		private int _decodePixelHeight;

		// Token: 0x0400190B RID: 6411
		private Rotation _rotation;

		// Token: 0x0400190C RID: 6412
		private Int32Rect _sourceRect;

		// Token: 0x0400190D RID: 6413
		private BitmapCreateOptions _createOptions;

		// Token: 0x0400190E RID: 6414
		private BitmapCacheOption _cacheOption;

		// Token: 0x0400190F RID: 6415
		private BitmapSource _finalSource;

		// Token: 0x04001910 RID: 6416
		private BitmapImage _cachedBitmapImage;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Imaging.BitmapImage.UriCachePolicy" />.</summary>
		// Token: 0x04001911 RID: 6417
		public static readonly DependencyProperty UriCachePolicyProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Imaging.BitmapImage.UriSource" />.</summary>
		// Token: 0x04001912 RID: 6418
		public static readonly DependencyProperty UriSourceProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Imaging.BitmapImage.StreamSource" />.</summary>
		// Token: 0x04001913 RID: 6419
		public static readonly DependencyProperty StreamSourceProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Imaging.BitmapImage.DecodePixelWidth" />.</summary>
		// Token: 0x04001914 RID: 6420
		public static readonly DependencyProperty DecodePixelWidthProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Imaging.BitmapImage.DecodePixelHeight" />.</summary>
		// Token: 0x04001915 RID: 6421
		public static readonly DependencyProperty DecodePixelHeightProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Imaging.BitmapImage.Rotation" />.</summary>
		// Token: 0x04001916 RID: 6422
		public static readonly DependencyProperty RotationProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Imaging.BitmapImage.SourceRect" />.</summary>
		// Token: 0x04001917 RID: 6423
		public static readonly DependencyProperty SourceRectProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Imaging.BitmapImage.CreateOptions" />.</summary>
		// Token: 0x04001918 RID: 6424
		public static readonly DependencyProperty CreateOptionsProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Imaging.BitmapImage.CacheOption" />.</summary>
		// Token: 0x04001919 RID: 6425
		public static readonly DependencyProperty CacheOptionProperty;

		// Token: 0x0400191A RID: 6426
		internal static RequestCachePolicy s_UriCachePolicy = null;

		// Token: 0x0400191B RID: 6427
		internal static Uri s_UriSource = null;

		// Token: 0x0400191C RID: 6428
		internal static Stream s_StreamSource = null;

		// Token: 0x0400191D RID: 6429
		internal const int c_DecodePixelWidth = 0;

		// Token: 0x0400191E RID: 6430
		internal const int c_DecodePixelHeight = 0;

		// Token: 0x0400191F RID: 6431
		internal const Rotation c_Rotation = Rotation.Rotate0;

		// Token: 0x04001920 RID: 6432
		internal static Int32Rect s_SourceRect = Int32Rect.Empty;

		// Token: 0x04001921 RID: 6433
		internal static BitmapCreateOptions s_CreateOptions = BitmapCreateOptions.None;

		// Token: 0x04001922 RID: 6434
		internal static BitmapCacheOption s_CacheOption = BitmapCacheOption.Default;
	}
}
