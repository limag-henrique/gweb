using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Media.Composition;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Representa um único e constante conjunto de pixels em um determinado tamanho e resolução.</summary>
	// Token: 0x020005E3 RID: 1507
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public abstract class BitmapSource : ImageSource, DUCE.IResource
	{
		/// <summary>Cria um novo <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> de uma matriz de pixels.</summary>
		/// <param name="pixelWidth">A largura do bitmap.</param>
		/// <param name="pixelHeight">A altura do bitmap.</param>
		/// <param name="dpiX">O dpi (pontos por polegada) horizontal do bitmap.</param>
		/// <param name="dpiY">O dpi (pontos por polegada) vertical do bitmap.</param>
		/// <param name="pixelFormat">O formato de pixel do bitmap.</param>
		/// <param name="palette">A paleta do bitmap.</param>
		/// <param name="pixels">Uma matriz de bytes que representa o conteúdo de uma imagem de bitmap.</param>
		/// <param name="stride">A distância do bitmap.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que é criado com base na matriz de pixels especificada.</returns>
		// Token: 0x0600447D RID: 17533 RVA: 0x0010AA88 File Offset: 0x00109E88
		public static BitmapSource Create(int pixelWidth, int pixelHeight, double dpiX, double dpiY, PixelFormat pixelFormat, BitmapPalette palette, Array pixels, int stride)
		{
			return new CachedBitmap(pixelWidth, pixelHeight, dpiX, dpiY, pixelFormat, palette, pixels, stride);
		}

		/// <summary>Cria um novo <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> com base em uma matriz de pixels armazenados na memória não gerenciada.</summary>
		/// <param name="pixelWidth">A largura do bitmap.</param>
		/// <param name="pixelHeight">A altura do bitmap.</param>
		/// <param name="dpiX">O dpi (pontos por polegada) horizontal do bitmap.</param>
		/// <param name="dpiY">O dpi (pontos por polegada) vertical do bitmap.</param>
		/// <param name="pixelFormat">O formato de pixel do bitmap.</param>
		/// <param name="palette">A paleta do bitmap.</param>
		/// <param name="buffer">Um ponteiro para o buffer que contém os dados de bitmap na memória.</param>
		/// <param name="bufferSize">O tamanho do buffer.</param>
		/// <param name="stride">A distância do bitmap.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> criado com base na matriz de pixels na memória não gerenciada.</returns>
		// Token: 0x0600447E RID: 17534 RVA: 0x0010AAA8 File Offset: 0x00109EA8
		[SecurityCritical]
		public static BitmapSource Create(int pixelWidth, int pixelHeight, double dpiX, double dpiY, PixelFormat pixelFormat, BitmapPalette palette, IntPtr buffer, int bufferSize, int stride)
		{
			SecurityHelper.DemandUnmanagedCode();
			return new CachedBitmap(pixelWidth, pixelHeight, dpiX, dpiY, pixelFormat, palette, buffer, bufferSize, stride);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.BitmapSource" />.</summary>
		// Token: 0x0600447F RID: 17535 RVA: 0x0010AAD0 File Offset: 0x00109ED0
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected BitmapSource()
		{
			this._syncObject = this._bitmapInit;
			this._isSourceCached = false;
		}

		// Token: 0x06004480 RID: 17536 RVA: 0x0010AB58 File Offset: 0x00109F58
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal BitmapSource(bool useVirtuals)
		{
			this._useVirtuals = true;
			this._isSourceCached = false;
			this._syncObject = this._bitmapInit;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Imaging.BitmapSource" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06004481 RID: 17537 RVA: 0x0010ABE8 File Offset: 0x00109FE8
		public new BitmapSource Clone()
		{
			return (BitmapSource)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Imaging.BitmapSource" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06004482 RID: 17538 RVA: 0x0010AC00 File Offset: 0x0010A000
		public new BitmapSource CloneCurrentValue()
		{
			return (BitmapSource)base.CloneCurrentValue();
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Media.PixelFormat" /> nativo dos dados de bitmap.</summary>
		/// <returns>O formato de pixel dos dados de bitmap.</returns>
		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06004483 RID: 17539 RVA: 0x0010AC18 File Offset: 0x0010A018
		public virtual PixelFormat Format
		{
			get
			{
				base.ReadPreamble();
				this.EnsureShouldUseVirtuals();
				this._bitmapInit.EnsureInitializedComplete();
				this.CompleteDelayedCreation();
				return this._format;
			}
		}

		/// <summary>Obtém a largura do bitmap em pixels.</summary>
		/// <returns>A largura do bitmap em pixels.</returns>
		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06004484 RID: 17540 RVA: 0x0010AC48 File Offset: 0x0010A048
		public virtual int PixelWidth
		{
			get
			{
				base.ReadPreamble();
				this.EnsureShouldUseVirtuals();
				this._bitmapInit.EnsureInitializedComplete();
				this.CompleteDelayedCreation();
				return this._pixelWidth;
			}
		}

		/// <summary>Obtém a altura do bitmap em pixels.</summary>
		/// <returns>A altura do bitmap em pixels.</returns>
		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06004485 RID: 17541 RVA: 0x0010AC78 File Offset: 0x0010A078
		public virtual int PixelHeight
		{
			get
			{
				base.ReadPreamble();
				this.EnsureShouldUseVirtuals();
				this._bitmapInit.EnsureInitializedComplete();
				this.CompleteDelayedCreation();
				return this._pixelHeight;
			}
		}

		/// <summary>Obtém o dpi (pontos por polegada) horizontal da imagem.</summary>
		/// <returns>O dpi (pontos por polegada) horizontal da imagem, isto é, o dpi (pontos por polegada) ao longo do eixo x.</returns>
		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06004486 RID: 17542 RVA: 0x0010ACA8 File Offset: 0x0010A0A8
		public virtual double DpiX
		{
			get
			{
				base.ReadPreamble();
				this.EnsureShouldUseVirtuals();
				this._bitmapInit.EnsureInitializedComplete();
				this.CompleteDelayedCreation();
				return this._dpiX;
			}
		}

		/// <summary>Obtém o dpi (pontos por polegada) vertical da imagem.</summary>
		/// <returns>O dpi (pontos por polegada) vertical da imagem, isto é, o dpi (pontos por polegada) ao longo do eixo y.</returns>
		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06004487 RID: 17543 RVA: 0x0010ACD8 File Offset: 0x0010A0D8
		public virtual double DpiY
		{
			get
			{
				base.ReadPreamble();
				this.EnsureShouldUseVirtuals();
				this._bitmapInit.EnsureInitializedComplete();
				this.CompleteDelayedCreation();
				return this._dpiY;
			}
		}

		/// <summary>Obtém a paleta de cores do bitmap, se especificada.</summary>
		/// <returns>A paleta de cores do bitmap.</returns>
		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x06004488 RID: 17544 RVA: 0x0010AD08 File Offset: 0x0010A108
		public virtual BitmapPalette Palette
		{
			get
			{
				base.ReadPreamble();
				this.EnsureShouldUseVirtuals();
				this._bitmapInit.EnsureInitializedComplete();
				this.CompleteDelayedCreation();
				if (this._palette == null && this._format.Palettized)
				{
					this._palette = BitmapPalette.CreateFromBitmapSource(this);
				}
				return this._palette;
			}
		}

		/// <summary>Obtém um valor que indica se o conteúdo <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> está sendo baixado no momento.</summary>
		/// <returns>
		///   <see langword="true" /> se a fonte de bitmap está sendo baixada no momento; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06004489 RID: 17545 RVA: 0x0010AD5C File Offset: 0x0010A15C
		public virtual bool IsDownloading
		{
			get
			{
				base.ReadPreamble();
				return false;
			}
		}

		/// <summary>Ocorre após o download completo do conteúdo do bitmap.</summary>
		// Token: 0x140001CE RID: 462
		// (add) Token: 0x0600448A RID: 17546 RVA: 0x0010AD70 File Offset: 0x0010A170
		// (remove) Token: 0x0600448B RID: 17547 RVA: 0x0010AD90 File Offset: 0x0010A190
		public virtual event EventHandler DownloadCompleted
		{
			add
			{
				base.WritePreamble();
				this._downloadEvent.AddEvent(value);
			}
			remove
			{
				base.WritePreamble();
				this._downloadEvent.RemoveEvent(value);
			}
		}

		/// <summary>Ocorre quando há alteração no andamento do download do conteúdo do bitmap.</summary>
		// Token: 0x140001CF RID: 463
		// (add) Token: 0x0600448C RID: 17548 RVA: 0x0010ADB0 File Offset: 0x0010A1B0
		// (remove) Token: 0x0600448D RID: 17549 RVA: 0x0010ADD0 File Offset: 0x0010A1D0
		public virtual event EventHandler<DownloadProgressEventArgs> DownloadProgress
		{
			add
			{
				base.WritePreamble();
				this._progressEvent.AddEvent(value);
			}
			remove
			{
				base.WritePreamble();
				this._progressEvent.RemoveEvent(value);
			}
		}

		/// <summary>Ocorre quando há falha no download do conteúdo do bitmap.</summary>
		// Token: 0x140001D0 RID: 464
		// (add) Token: 0x0600448E RID: 17550 RVA: 0x0010ADF0 File Offset: 0x0010A1F0
		// (remove) Token: 0x0600448F RID: 17551 RVA: 0x0010AE10 File Offset: 0x0010A210
		public virtual event EventHandler<ExceptionEventArgs> DownloadFailed
		{
			add
			{
				base.WritePreamble();
				this._failedEvent.AddEvent(value);
			}
			remove
			{
				base.WritePreamble();
				this._failedEvent.RemoveEvent(value);
			}
		}

		/// <summary>Ocorre há uma falha de carregamento da imagem devido a um cabeçalho da imagem corrompido.</summary>
		// Token: 0x140001D1 RID: 465
		// (add) Token: 0x06004490 RID: 17552 RVA: 0x0010AE30 File Offset: 0x0010A230
		// (remove) Token: 0x06004491 RID: 17553 RVA: 0x0010AE58 File Offset: 0x0010A258
		public virtual event EventHandler<ExceptionEventArgs> DecodeFailed
		{
			add
			{
				base.WritePreamble();
				this.EnsureShouldUseVirtuals();
				this._decodeFailedEvent.AddEvent(value);
			}
			remove
			{
				base.WritePreamble();
				this.EnsureShouldUseVirtuals();
				this._decodeFailedEvent.RemoveEvent(value);
			}
		}

		/// <summary>Copia os dados de pixel de bitmap no retângulo especificado em uma matriz de pixels que tem a distância especificada começando no deslocamento especificado.</summary>
		/// <param name="sourceRect">O retângulo de origem a ser copiado. Um valor <see cref="P:System.Windows.Int32Rect.Empty" /> especifica o bitmap inteiro.</param>
		/// <param name="pixels">A matriz de destino.</param>
		/// <param name="stride">A distância do bitmap.</param>
		/// <param name="offset">O local de pixel em que a cópia é iniciada.</param>
		// Token: 0x06004492 RID: 17554 RVA: 0x0010AE80 File Offset: 0x0010A280
		[SecurityCritical]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public virtual void CopyPixels(Int32Rect sourceRect, Array pixels, int stride, int offset)
		{
			this.EnsureShouldUseVirtuals();
			this.CheckIfSiteOfOrigin();
			this.CriticalCopyPixels(sourceRect, pixels, stride, offset);
		}

		/// <summary>Copia os dados de pixel de bitmap para a matriz de pixes que tem a distância especificada, começando no deslocamento especificado.</summary>
		/// <param name="pixels">A matriz de destino.</param>
		/// <param name="stride">A distância do bitmap.</param>
		/// <param name="offset">O local de pixel em que a cópia é iniciada.</param>
		// Token: 0x06004493 RID: 17555 RVA: 0x0010AEA4 File Offset: 0x0010A2A4
		[SecurityCritical]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public virtual void CopyPixels(Array pixels, int stride, int offset)
		{
			Int32Rect empty = Int32Rect.Empty;
			this.EnsureShouldUseVirtuals();
			this.CheckIfSiteOfOrigin();
			this.CopyPixels(empty, pixels, stride, offset);
		}

		/// <summary>Copia os dados de pixel do bitmap dentro do retângulo especificado</summary>
		/// <param name="sourceRect">O retângulo de origem a ser copiado. Um valor <see cref="P:System.Windows.Int32Rect.Empty" /> especifica o bitmap inteiro.</param>
		/// <param name="buffer">Um ponteiro para o buffer.</param>
		/// <param name="bufferSize">O tamanho do buffer.</param>
		/// <param name="stride">A distância do bitmap.</param>
		// Token: 0x06004494 RID: 17556 RVA: 0x0010AED0 File Offset: 0x0010A2D0
		[SecurityCritical]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public virtual void CopyPixels(Int32Rect sourceRect, IntPtr buffer, int bufferSize, int stride)
		{
			base.ReadPreamble();
			this.EnsureShouldUseVirtuals();
			this._bitmapInit.EnsureInitializedComplete();
			this.CompleteDelayedCreation();
			this.CheckIfSiteOfOrigin();
			this.CriticalCopyPixels(sourceRect, buffer, bufferSize, stride);
		}

		/// <summary>Obtém a largura do bitmap em unidades independentes de dispositivo (1/96 polegada por unidade).</summary>
		/// <returns>A largura do bitmap em unidades independentes de dispositivo (1/96 polegada por unidade).</returns>
		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06004495 RID: 17557 RVA: 0x0010AF0C File Offset: 0x0010A30C
		public override double Width
		{
			get
			{
				base.ReadPreamble();
				return this.GetWidthInternal();
			}
		}

		/// <summary>Obtém a altura do bitmap de origem em unidades independentes de dispositivo (1/96 polegada por unidade).</summary>
		/// <returns>A altura do bitmap em unidades independentes de dispositivo (1/96 polegada por unidade).</returns>
		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06004496 RID: 17558 RVA: 0x0010AF28 File Offset: 0x0010A328
		public override double Height
		{
			get
			{
				base.ReadPreamble();
				return this.GetHeightInternal();
			}
		}

		/// <summary>Obtém os metadados associados a esta imagem de bitmap.</summary>
		/// <returns>Os metadados associados à imagem de bitmap.</returns>
		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06004497 RID: 17559 RVA: 0x0010AF44 File Offset: 0x0010A344
		public override ImageMetadata Metadata
		{
			get
			{
				base.ReadPreamble();
				return null;
			}
		}

		// Token: 0x06004498 RID: 17560 RVA: 0x0010AF58 File Offset: 0x0010A358
		private double GetWidthInternal()
		{
			return ImageSource.PixelsToDIPs(this.DpiX, this.PixelWidth);
		}

		// Token: 0x06004499 RID: 17561 RVA: 0x0010AF78 File Offset: 0x0010A378
		private double GetHeightInternal()
		{
			return ImageSource.PixelsToDIPs(this.DpiY, this.PixelHeight);
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x0600449A RID: 17562 RVA: 0x0010AF98 File Offset: 0x0010A398
		internal override Size Size
		{
			get
			{
				base.ReadPreamble();
				return new Size(Math.Max(0.0, this.GetWidthInternal()), Math.Max(0.0, this.GetHeightInternal()));
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x0600449B RID: 17563 RVA: 0x0010AFD8 File Offset: 0x0010A3D8
		// (set) Token: 0x0600449C RID: 17564 RVA: 0x0010AFEC File Offset: 0x0010A3EC
		internal bool DelayCreation
		{
			get
			{
				return this._delayCreation;
			}
			set
			{
				this._delayCreation = value;
				if (this._delayCreation)
				{
					this.CreationCompleted = false;
				}
			}
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x0600449D RID: 17565 RVA: 0x0010B010 File Offset: 0x0010A410
		// (set) Token: 0x0600449E RID: 17566 RVA: 0x0010B024 File Offset: 0x0010A424
		internal bool CreationCompleted
		{
			get
			{
				return this._creationComplete;
			}
			set
			{
				this._creationComplete = value;
			}
		}

		// Token: 0x0600449F RID: 17567 RVA: 0x0010B038 File Offset: 0x0010A438
		internal void CompleteDelayedCreation()
		{
			if (this.DelayCreation)
			{
				object syncObject = this._syncObject;
				lock (syncObject)
				{
					if (this.DelayCreation)
					{
						this.EnsureShouldUseVirtuals();
						this.DelayCreation = false;
						try
						{
							this.FinalizeCreation();
						}
						catch
						{
							this.DelayCreation = true;
							throw;
						}
						this.CreationCompleted = true;
					}
				}
			}
		}

		// Token: 0x060044A0 RID: 17568 RVA: 0x0010B0D0 File Offset: 0x0010A4D0
		internal virtual void FinalizeCreation()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060044A1 RID: 17569 RVA: 0x0010B0E4 File Offset: 0x0010A4E4
		private void EnsureShouldUseVirtuals()
		{
			if (!this._useVirtuals)
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x060044A2 RID: 17570 RVA: 0x0010B100 File Offset: 0x0010A500
		internal object SyncObject
		{
			get
			{
				return this._syncObject;
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x060044A3 RID: 17571 RVA: 0x0010B114 File Offset: 0x0010A514
		// (set) Token: 0x060044A4 RID: 17572 RVA: 0x0010B128 File Offset: 0x0010A528
		internal bool IsSourceCached
		{
			get
			{
				return this._isSourceCached;
			}
			set
			{
				this._isSourceCached = value;
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x060044A5 RID: 17573 RVA: 0x0010B13C File Offset: 0x0010A53C
		// (set) Token: 0x060044A6 RID: 17574 RVA: 0x0010B18C File Offset: 0x0010A58C
		internal BitmapSourceSafeMILHandle WicSourceHandle
		{
			[SecurityCritical]
			get
			{
				this.CompleteDelayedCreation();
				if (this._wicSource == null || this._wicSource.IsInvalid)
				{
					BitmapSource.ManagedBitmapSource o = new BitmapSource.ManagedBitmapSource(this);
					this._wicSource = new BitmapSourceSafeMILHandle(Marshal.GetComInterfaceForObject(o, typeof(BitmapSource.IWICBitmapSource)));
				}
				return this._wicSource;
			}
			[SecurityTreatAsSafe]
			[SecurityCritical]
			set
			{
				if (value != null)
				{
					IntPtr zero = IntPtr.Zero;
					Guid iid_IWICBitmapSource = MILGuidData.IID_IWICBitmapSource;
					HRESULT.Check(UnsafeNativeMethods.MILUnknown.QueryInterface(value, ref iid_IWICBitmapSource, out zero));
					this._wicSource = new BitmapSourceSafeMILHandle(zero, value);
					this.UpdateCachedSettings();
					return;
				}
				this._wicSource = null;
			}
		}

		// Token: 0x060044A7 RID: 17575 RVA: 0x0010B1D4 File Offset: 0x0010A5D4
		[SecurityCritical]
		internal virtual void UpdateCachedSettings()
		{
			this.EnsureShouldUseVirtuals();
			object syncObject = this._syncObject;
			uint pixelWidth;
			uint pixelHeight;
			lock (syncObject)
			{
				this._format = PixelFormat.GetPixelFormat(this._wicSource);
				HRESULT.Check(UnsafeNativeMethods.WICBitmapSource.GetSize(this._wicSource, out pixelWidth, out pixelHeight));
				HRESULT.Check(UnsafeNativeMethods.WICBitmapSource.GetResolution(this._wicSource, out this._dpiX, out this._dpiY));
			}
			this._pixelWidth = (int)pixelWidth;
			this._pixelHeight = (int)pixelHeight;
		}

		// Token: 0x060044A8 RID: 17576 RVA: 0x0010B270 File Offset: 0x0010A670
		[FriendAccessAllowed]
		[SecurityCritical]
		internal unsafe void CriticalCopyPixels(Int32Rect sourceRect, Array pixels, int stride, int offset)
		{
			base.ReadPreamble();
			this._bitmapInit.EnsureInitializedComplete();
			this.CompleteDelayedCreation();
			if (pixels == null)
			{
				throw new ArgumentNullException("pixels");
			}
			if (pixels.Rank != 1)
			{
				throw new ArgumentException(SR.Get("Collection_BadRank"), "pixels");
			}
			if (offset < 0)
			{
				HRESULT.Check(-2147024362);
			}
			int num = -1;
			if (pixels is byte[])
			{
				num = 1;
			}
			else if (pixels is short[] || pixels is ushort[])
			{
				num = 2;
			}
			else if (pixels is int[] || pixels is uint[] || pixels is float[])
			{
				num = 4;
			}
			else if (pixels is double[])
			{
				num = 8;
			}
			if (num == -1)
			{
				throw new ArgumentException(SR.Get("Image_InvalidArrayForPixel"));
			}
			int bufferSize = checked(num * (pixels.Length - offset));
			if (pixels is byte[])
			{
				fixed (byte* ptr = &((byte[])pixels)[offset])
				{
					void* value = (void*)ptr;
					this.CriticalCopyPixels(sourceRect, (IntPtr)value, bufferSize, stride);
				}
				return;
			}
			if (pixels is short[])
			{
				fixed (short* ptr2 = &((short[])pixels)[offset])
				{
					void* value2 = (void*)ptr2;
					this.CriticalCopyPixels(sourceRect, (IntPtr)value2, bufferSize, stride);
				}
				return;
			}
			if (pixels is ushort[])
			{
				fixed (ushort* ptr3 = &((ushort[])pixels)[offset])
				{
					void* value3 = (void*)ptr3;
					this.CriticalCopyPixels(sourceRect, (IntPtr)value3, bufferSize, stride);
				}
				return;
			}
			if (pixels is int[])
			{
				fixed (int* ptr4 = &((int[])pixels)[offset])
				{
					void* value4 = (void*)ptr4;
					this.CriticalCopyPixels(sourceRect, (IntPtr)value4, bufferSize, stride);
				}
				return;
			}
			if (pixels is uint[])
			{
				fixed (uint* ptr5 = &((uint[])pixels)[offset])
				{
					void* value5 = (void*)ptr5;
					this.CriticalCopyPixels(sourceRect, (IntPtr)value5, bufferSize, stride);
				}
				return;
			}
			if (pixels is float[])
			{
				fixed (float* ptr6 = &((float[])pixels)[offset])
				{
					void* value6 = (void*)ptr6;
					this.CriticalCopyPixels(sourceRect, (IntPtr)value6, bufferSize, stride);
				}
				return;
			}
			if (pixels is double[])
			{
				fixed (double* ptr7 = &((double[])pixels)[offset])
				{
					void* value7 = (void*)ptr7;
					this.CriticalCopyPixels(sourceRect, (IntPtr)value7, bufferSize, stride);
				}
			}
		}

		// Token: 0x060044A9 RID: 17577 RVA: 0x0010B488 File Offset: 0x0010A888
		[SecurityCritical]
		internal void CriticalCopyPixels(Int32Rect sourceRect, IntPtr buffer, int bufferSize, int stride)
		{
			if (buffer == IntPtr.Zero)
			{
				throw new ArgumentNullException("buffer");
			}
			if (stride <= 0)
			{
				throw new ArgumentOutOfRangeException("stride", SR.Get("ParameterMustBeGreaterThanZero"));
			}
			if (sourceRect.Width <= 0)
			{
				sourceRect.Width = this.PixelWidth;
			}
			if (sourceRect.Height <= 0)
			{
				sourceRect.Height = this.PixelHeight;
			}
			if (sourceRect.Width > this.PixelWidth)
			{
				throw new ArgumentOutOfRangeException("sourceRect.Width", SR.Get("ParameterCannotBeGreaterThan", new object[]
				{
					this.PixelWidth
				}));
			}
			if (sourceRect.Height > this.PixelHeight)
			{
				throw new ArgumentOutOfRangeException("sourceRect.Height", SR.Get("ParameterCannotBeGreaterThan", new object[]
				{
					this.PixelHeight
				}));
			}
			checked
			{
				int num = (sourceRect.Width * this.Format.BitsPerPixel + 7) / 8;
				if (stride < num)
				{
					throw new ArgumentOutOfRangeException("stride", SR.Get("ParameterCannotBeLessThan", new object[]
					{
						num
					}));
				}
				int num2 = stride * (sourceRect.Height - 1) + num;
				if (bufferSize < num2)
				{
					throw new ArgumentOutOfRangeException("buffer", SR.Get("ParameterCannotBeLessThan", new object[]
					{
						num2
					}));
				}
				object syncObject = this._syncObject;
				lock (syncObject)
				{
					HRESULT.Check(UnsafeNativeMethods.WICBitmapSource.CopyPixels(this.WicSourceHandle, ref sourceRect, (uint)stride, (uint)bufferSize, buffer));
				}
			}
		}

		/// <summary>Verifica se o conteúdo de origem de bitmap é de um site conhecido de origem. Esse método é usado para certificar-se de que essas operações de cópia de pixel são seguras.</summary>
		// Token: 0x060044AA RID: 17578 RVA: 0x0010B630 File Offset: 0x0010AA30
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected void CheckIfSiteOfOrigin()
		{
			string uri = null;
			if (this.CanSerializeToString())
			{
				uri = this.ConvertToString(null, null);
			}
			SecurityHelper.DemandMediaAccessPermission(uri);
		}

		// Token: 0x060044AB RID: 17579 RVA: 0x0010B658 File Offset: 0x0010AA58
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			base.UpdateResource(channel, skipOnChannelCheck);
			this.UpdateBitmapSourceResource(channel, skipOnChannelCheck);
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x060044AC RID: 17580 RVA: 0x0010B678 File Offset: 0x0010AA78
		internal unsafe virtual BitmapSourceSafeMILHandle DUCECompatiblePtr
		{
			[SecurityCritical]
			get
			{
				BitmapSourceSafeMILHandle bitmapSourceSafeMILHandle = this.WicSourceHandle;
				BitmapSourceSafeMILHandle bitmapSourceSafeMILHandle2 = null;
				if (this._convertedDUCEPtr == null || this._convertedDUCEPtr.IsInvalid)
				{
					if (this.UsableWithoutCache)
					{
						Int32Rect int32Rect = new Int32Rect(0, 0, 1, 1);
						int num = (this.Format.BitsPerPixel + 7) / 8;
						byte[] array = new byte[num];
						try
						{
							try
							{
								fixed (byte* ptr = &array[0])
								{
									void* value = (void*)ptr;
									HRESULT.Check(UnsafeNativeMethods.WICBitmapSource.CopyPixels(bitmapSourceSafeMILHandle, ref int32Rect, (uint)num, (uint)num, (IntPtr)value));
								}
							}
							finally
							{
								byte* ptr = null;
							}
							goto IL_144;
						}
						catch (Exception e)
						{
							this.RecoverFromDecodeFailure(e);
							bitmapSourceSafeMILHandle = this.WicSourceHandle;
							goto IL_144;
						}
					}
					BitmapSourceSafeMILHandle bitmapSourceSafeMILHandle3 = null;
					using (FactoryMaker factoryMaker = new FactoryMaker())
					{
						try
						{
							if (!this.HasCompatibleFormat)
							{
								Guid guid = BitmapSource.GetClosestDUCEFormat(this.Format, this.Palette).Guid;
								HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreateFormatConverter(factoryMaker.ImagingFactoryPtr, out bitmapSourceSafeMILHandle3));
								HRESULT.Check(UnsafeNativeMethods.WICFormatConverter.Initialize(bitmapSourceSafeMILHandle3, bitmapSourceSafeMILHandle, ref guid, DitherType.DitherTypeNone, new SafeMILHandle(IntPtr.Zero), 0.0, WICPaletteType.WICPaletteTypeCustom));
								bitmapSourceSafeMILHandle = bitmapSourceSafeMILHandle3;
							}
							try
							{
								HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreateBitmapFromSource(factoryMaker.ImagingFactoryPtr, bitmapSourceSafeMILHandle, WICBitmapCreateCacheOptions.WICBitmapCacheOnLoad, out bitmapSourceSafeMILHandle));
							}
							catch (Exception e2)
							{
								this.RecoverFromDecodeFailure(e2);
								bitmapSourceSafeMILHandle = this.WicSourceHandle;
							}
							this._isSourceCached = true;
						}
						finally
						{
							if (bitmapSourceSafeMILHandle3 != null)
							{
								bitmapSourceSafeMILHandle3.Close();
							}
						}
					}
					IL_144:
					HRESULT.Check(UnsafeNativeMethods.MilCoreApi.CreateCWICWrapperBitmap(bitmapSourceSafeMILHandle, out bitmapSourceSafeMILHandle2));
					UnsafeNativeMethods.MILUnknown.AddRef(bitmapSourceSafeMILHandle2);
					this._convertedDUCEPtr = new BitmapSourceSafeMILHandle(bitmapSourceSafeMILHandle2.DangerousGetHandle(), bitmapSourceSafeMILHandle);
				}
				return this._convertedDUCEPtr;
			}
		}

		// Token: 0x060044AD RID: 17581 RVA: 0x0010B874 File Offset: 0x0010AC74
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_BITMAPSOURCE))
			{
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060044AE RID: 17582 RVA: 0x0010B8A8 File Offset: 0x0010ACA8
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			DUCE.ResourceHandle result;
			using (CompositionEngineLock.Acquire())
			{
				result = this.AddRefOnChannelCore(channel);
			}
			return result;
		}

		// Token: 0x060044AF RID: 17583 RVA: 0x0010B8F0 File Offset: 0x0010ACF0
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x060044B0 RID: 17584 RVA: 0x0010B908 File Offset: 0x0010AD08
		int DUCE.IResource.GetChannelCount()
		{
			return this.GetChannelCountCore();
		}

		// Token: 0x060044B1 RID: 17585 RVA: 0x0010B91C File Offset: 0x0010AD1C
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x060044B2 RID: 17586 RVA: 0x0010B938 File Offset: 0x0010AD38
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this.GetChannelCore(index);
		}

		// Token: 0x060044B3 RID: 17587 RVA: 0x0010B94C File Offset: 0x0010AD4C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal virtual void UpdateBitmapSourceResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (this._needsUpdate)
			{
				this._convertedDUCEPtr = null;
				this._needsUpdate = false;
			}
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				object syncObject = this._syncObject;
				lock (syncObject)
				{
					channel.SendCommandBitmapSource(this._duceResource.GetHandle(channel), this.DUCECompatiblePtr);
				}
			}
		}

		// Token: 0x060044B4 RID: 17588 RVA: 0x0010B9D4 File Offset: 0x0010ADD4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal void RecoverFromDecodeFailure(Exception e)
		{
			byte[] pixels = new byte[4];
			this.WicSourceHandle = BitmapSource.Create(1, 1, 96.0, 96.0, PixelFormats.Pbgra32, null, pixels, 4).WicSourceHandle;
			this.IsSourceCached = true;
			this.OnDecodeFailed(this, new ExceptionEventArgs(e));
		}

		// Token: 0x060044B5 RID: 17589 RVA: 0x0010BA28 File Offset: 0x0010AE28
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			this._duceResource.ReleaseOnChannel(channel);
		}

		// Token: 0x060044B6 RID: 17590 RVA: 0x0010BA44 File Offset: 0x0010AE44
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			using (CompositionEngineLock.Acquire())
			{
				this.ReleaseOnChannelCore(channel);
			}
		}

		// Token: 0x060044B7 RID: 17591 RVA: 0x0010BA8C File Offset: 0x0010AE8C
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060044B8 RID: 17592 RVA: 0x0010BAA8 File Offset: 0x0010AEA8
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handleCore;
			using (CompositionEngineLock.Acquire())
			{
				handleCore = this.GetHandleCore(channel);
			}
			return handleCore;
		}

		// Token: 0x060044B9 RID: 17593 RVA: 0x0010BAF0 File Offset: 0x0010AEF0
		internal static PixelFormat GetClosestDUCEFormat(PixelFormat format, BitmapPalette palette)
		{
			int num = Array.IndexOf<PixelFormat>(BitmapSource.s_supportedDUCEFormats, format);
			if (num != -1)
			{
				return BitmapSource.s_supportedDUCEFormats[num];
			}
			int internalBitsPerPixel = format.InternalBitsPerPixel;
			if (internalBitsPerPixel == 1)
			{
				return PixelFormats.Indexed1;
			}
			if (internalBitsPerPixel == 2)
			{
				return PixelFormats.Indexed2;
			}
			if (internalBitsPerPixel <= 4)
			{
				return PixelFormats.Indexed4;
			}
			if (internalBitsPerPixel <= 8)
			{
				return PixelFormats.Indexed8;
			}
			if (internalBitsPerPixel <= 16 && format.Format != PixelFormatEnum.Gray16)
			{
				return PixelFormats.Bgr555;
			}
			if (format.HasAlpha || BitmapPalette.DoesPaletteHaveAlpha(palette))
			{
				return PixelFormats.Pbgra32;
			}
			return PixelFormats.Bgr32;
		}

		// Token: 0x060044BA RID: 17594 RVA: 0x0010BB7C File Offset: 0x0010AF7C
		[SecurityCritical]
		internal static BitmapSourceSafeMILHandle CreateCachedBitmap(BitmapFrame frame, BitmapSourceSafeMILHandle wicSource, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption, BitmapPalette palette)
		{
			BitmapSourceSafeMILHandle pIBitmapSource = null;
			BitmapSourceSafeMILHandle bitmapSourceSafeMILHandle = null;
			if (cacheOption == BitmapCacheOption.None)
			{
				return wicSource;
			}
			using (FactoryMaker factoryMaker = new FactoryMaker())
			{
				IntPtr imagingFactoryPtr = factoryMaker.ImagingFactoryPtr;
				bool flag = false;
				PixelFormat pixelFormat = PixelFormats.Pbgra32;
				WICBitmapCreateCacheOptions options = WICBitmapCreateCacheOptions.WICBitmapCacheOnLoad;
				if (cacheOption == BitmapCacheOption.Default)
				{
					options = WICBitmapCreateCacheOptions.WICBitmapCacheOnDemand;
				}
				pixelFormat = PixelFormat.GetPixelFormat(wicSource);
				PixelFormat pixelFormat2 = pixelFormat;
				if ((createOptions & BitmapCreateOptions.PreservePixelFormat) == BitmapCreateOptions.None)
				{
					if (!BitmapSource.IsCompatibleFormat(pixelFormat))
					{
						flag = true;
					}
					pixelFormat2 = BitmapSource.GetClosestDUCEFormat(pixelFormat, palette);
				}
				if (frame != null && (createOptions & BitmapCreateOptions.IgnoreColorProfile) == BitmapCreateOptions.None && frame.ColorContexts != null && frame.ColorContexts[0] != null && frame.ColorContexts[0].IsValid && !frame._isColorCorrected && PixelFormat.GetPixelFormat(wicSource).Format != PixelFormatEnum.Default)
				{
					ColorContext colorContext;
					try
					{
						colorContext = new ColorContext(pixelFormat2);
					}
					catch (NotSupportedException)
					{
						colorContext = null;
					}
					if (colorContext != null)
					{
						bool flag2 = false;
						bool flag3 = false;
						try
						{
							ColorConvertedBitmap colorConvertedBitmap = new ColorConvertedBitmap(frame, frame.ColorContexts[0], colorContext, pixelFormat2);
							wicSource = colorConvertedBitmap.WicSourceHandle;
							frame._isColorCorrected = true;
							flag2 = true;
							flag = false;
						}
						catch (NotSupportedException)
						{
						}
						catch (FileFormatException)
						{
							flag3 = true;
						}
						if (!flag2 && flag && !flag3)
						{
							flag = false;
							FormatConvertedBitmap source = new FormatConvertedBitmap(frame, pixelFormat2, null, 0.0);
							ColorConvertedBitmap colorConvertedBitmap2 = new ColorConvertedBitmap(source, frame.ColorContexts[0], colorContext, pixelFormat2);
							wicSource = colorConvertedBitmap2.WicSourceHandle;
							frame._isColorCorrected = true;
							flag = false;
						}
					}
				}
				if (flag)
				{
					Guid guid = pixelFormat2.Guid;
					HRESULT.Check(UnsafeNativeMethods.WICCodec.WICConvertBitmapSource(ref guid, wicSource, out pIBitmapSource));
					HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreateBitmapFromSource(imagingFactoryPtr, pIBitmapSource, options, out bitmapSourceSafeMILHandle));
				}
				else
				{
					HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreateBitmapFromSource(imagingFactoryPtr, wicSource, options, out bitmapSourceSafeMILHandle));
				}
				bitmapSourceSafeMILHandle.CalculateSize();
			}
			return bitmapSourceSafeMILHandle;
		}

		// Token: 0x060044BB RID: 17595 RVA: 0x0010BD98 File Offset: 0x0010B198
		private void OnDecodeFailed(object sender, ExceptionEventArgs e)
		{
			this._decodeFailedEvent.InvokeEvents(this, e);
		}

		// Token: 0x060044BC RID: 17596 RVA: 0x0010BDB4 File Offset: 0x0010B1B4
		private void OnSourceDownloadCompleted(object sender, EventArgs e)
		{
			if (this._weakBitmapSourceEventSink != null)
			{
				this.CleanUpWeakEventSink();
				if (this._bitmapInit.IsInitAtLeastOnce && this.IsValidForFinalizeCreation(false))
				{
					try
					{
						this.FinalizeCreation();
						this._needsUpdate = true;
					}
					catch
					{
					}
					this._downloadEvent.InvokeEvents(this, e);
				}
			}
		}

		// Token: 0x060044BD RID: 17597 RVA: 0x0010BE20 File Offset: 0x0010B220
		private void OnSourceDownloadFailed(object sender, ExceptionEventArgs e)
		{
			if (this._weakBitmapSourceEventSink != null)
			{
				this.CleanUpWeakEventSink();
				this._failedEvent.InvokeEvents(this, e);
			}
		}

		// Token: 0x060044BE RID: 17598 RVA: 0x0010BE48 File Offset: 0x0010B248
		private void OnSourceDownloadProgress(object sender, DownloadProgressEventArgs e)
		{
			this._progressEvent.InvokeEvents(this, e);
		}

		// Token: 0x060044BF RID: 17599 RVA: 0x0010BE64 File Offset: 0x0010B264
		private void CleanUpWeakEventSink()
		{
			this._weakBitmapSourceEventSink.EventSource = null;
			this._weakBitmapSourceEventSink = null;
		}

		// Token: 0x060044C0 RID: 17600 RVA: 0x0010BE84 File Offset: 0x0010B284
		internal void RegisterDownloadEventSource(BitmapSource eventSource)
		{
			if (this._weakBitmapSourceEventSink == null)
			{
				this._weakBitmapSourceEventSink = new BitmapSource.WeakBitmapSourceEventSink(this);
			}
			this._weakBitmapSourceEventSink.EventSource = eventSource;
		}

		// Token: 0x060044C1 RID: 17601 RVA: 0x0010BEB4 File Offset: 0x0010B2B4
		internal void UnregisterDownloadEventSource()
		{
			if (this._weakBitmapSourceEventSink != null)
			{
				this.CleanUpWeakEventSink();
			}
		}

		// Token: 0x060044C2 RID: 17602 RVA: 0x0010BED0 File Offset: 0x0010B2D0
		internal virtual bool IsValidForFinalizeCreation(bool throwIfInvalid)
		{
			return true;
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x060044C3 RID: 17603 RVA: 0x0010BEE0 File Offset: 0x0010B2E0
		internal virtual bool ShouldCloneEventDelegates
		{
			get
			{
				return true;
			}
		}

		/// <summary>Torna uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> ou uma classe derivada imutável.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> se esta instância, na verdade, deve congelar a si mesma quando este método é chamado; caso contrário, <see langword="false" />.</param>
		/// <returns>Se <paramref name="isChecking" /> for <see langword="true" />, esse método retorna <see langword="true" /> se este <see cref="T:System.Windows.Media.Animation.Animatable" /> puder se tornar não modificável ou <see langword="false" />, se ele não puder se tornar não modificável.  
		/// Se <paramref name="isChecking" /> for <see langword="false" />, este método retorna <see langword="true" /> se esse <see cref="T:System.Windows.Media.Animation.Animatable" /> agora for não modificável ou <see langword="false" />, se não puder se tornar não modificável, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x060044C4 RID: 17604 RVA: 0x0010BEF0 File Offset: 0x0010B2F0
		protected override bool FreezeCore(bool isChecking)
		{
			return base.FreezeCore(isChecking) && !this.IsDownloading;
		}

		// Token: 0x060044C5 RID: 17605 RVA: 0x0010BF14 File Offset: 0x0010B314
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void CopyCommon(BitmapSource sourceBitmap)
		{
			this._useVirtuals = sourceBitmap._useVirtuals;
			this._delayCreation = sourceBitmap.DelayCreation;
			this._creationComplete = sourceBitmap.CreationCompleted;
			this.WicSourceHandle = sourceBitmap.WicSourceHandle;
			this._syncObject = sourceBitmap.SyncObject;
			this.IsSourceCached = sourceBitmap.IsSourceCached;
			if (this.ShouldCloneEventDelegates)
			{
				if (sourceBitmap._downloadEvent != null)
				{
					this._downloadEvent = sourceBitmap._downloadEvent.Clone();
				}
				if (sourceBitmap._progressEvent != null)
				{
					this._progressEvent = sourceBitmap._progressEvent.Clone();
				}
				if (sourceBitmap._failedEvent != null)
				{
					this._failedEvent = sourceBitmap._failedEvent.Clone();
				}
				if (sourceBitmap._decodeFailedEvent != null)
				{
					this._decodeFailedEvent = sourceBitmap._decodeFailedEvent.Clone();
				}
			}
			this._format = sourceBitmap.Format;
			this._pixelWidth = sourceBitmap.PixelWidth;
			this._pixelHeight = sourceBitmap.PixelHeight;
			this._dpiX = sourceBitmap.DpiX;
			this._dpiY = sourceBitmap.DpiY;
			this._palette = sourceBitmap.Palette;
			if (this._weakBitmapSourceEventSink != null && sourceBitmap._weakBitmapSourceEventSink != null)
			{
				sourceBitmap._weakBitmapSourceEventSink.DetachSourceDownloadHandlers(this._weakBitmapSourceEventSink.EventSource);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> a ser clonado.</param>
		// Token: 0x060044C6 RID: 17606 RVA: 0x0010C044 File Offset: 0x0010B444
		protected override void CloneCore(Freezable sourceFreezable)
		{
			BitmapSource sourceBitmap = (BitmapSource)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(sourceBitmap);
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> a ser clonado.</param>
		// Token: 0x060044C7 RID: 17607 RVA: 0x0010C068 File Offset: 0x0010B468
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			BitmapSource sourceBitmap = (BitmapSource)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(sourceBitmap);
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> a ser clonado e congelado.</param>
		// Token: 0x060044C8 RID: 17608 RVA: 0x0010C08C File Offset: 0x0010B48C
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			BitmapSource sourceBitmap = (BitmapSource)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			this.CopyCommon(sourceBitmap);
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> a ser copiado e congelado.</param>
		// Token: 0x060044C9 RID: 17609 RVA: 0x0010C0B0 File Offset: 0x0010B4B0
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			BitmapSource sourceBitmap = (BitmapSource)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			this.CopyCommon(sourceBitmap);
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x060044CA RID: 17610 RVA: 0x0010C0D4 File Offset: 0x0010B4D4
		internal bool UsableWithoutCache
		{
			get
			{
				return this.HasCompatibleFormat && this._isSourceCached;
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x060044CB RID: 17611 RVA: 0x0010C0F4 File Offset: 0x0010B4F4
		internal bool HasCompatibleFormat
		{
			get
			{
				return BitmapSource.IsCompatibleFormat(this.Format);
			}
		}

		// Token: 0x060044CC RID: 17612 RVA: 0x0010C10C File Offset: 0x0010B50C
		internal static bool IsCompatibleFormat(PixelFormat format)
		{
			return Array.IndexOf<PixelFormat>(BitmapSource.s_supportedDUCEFormats, format) != -1;
		}

		// Token: 0x040018EB RID: 6379
		private bool _delayCreation;

		// Token: 0x040018EC RID: 6380
		private bool _creationComplete;

		// Token: 0x040018ED RID: 6381
		private bool _useVirtuals;

		// Token: 0x040018EE RID: 6382
		internal BitmapInitialize _bitmapInit = new BitmapInitialize();

		// Token: 0x040018EF RID: 6383
		[SecurityCritical]
		internal BitmapSourceSafeMILHandle _wicSource;

		// Token: 0x040018F0 RID: 6384
		[SecurityCritical]
		internal BitmapSourceSafeMILHandle _convertedDUCEPtr;

		// Token: 0x040018F1 RID: 6385
		internal object _syncObject;

		// Token: 0x040018F2 RID: 6386
		internal bool _isSourceCached;

		// Token: 0x040018F3 RID: 6387
		internal bool _needsUpdate;

		// Token: 0x040018F4 RID: 6388
		internal bool _isColorCorrected;

		// Token: 0x040018F5 RID: 6389
		internal UniqueEventHelper _downloadEvent = new UniqueEventHelper();

		// Token: 0x040018F6 RID: 6390
		internal UniqueEventHelper<DownloadProgressEventArgs> _progressEvent = new UniqueEventHelper<DownloadProgressEventArgs>();

		// Token: 0x040018F7 RID: 6391
		internal UniqueEventHelper<ExceptionEventArgs> _failedEvent = new UniqueEventHelper<ExceptionEventArgs>();

		// Token: 0x040018F8 RID: 6392
		internal UniqueEventHelper<ExceptionEventArgs> _decodeFailedEvent = new UniqueEventHelper<ExceptionEventArgs>();

		// Token: 0x040018F9 RID: 6393
		internal PixelFormat _format = PixelFormats.Default;

		// Token: 0x040018FA RID: 6394
		internal int _pixelWidth;

		// Token: 0x040018FB RID: 6395
		internal int _pixelHeight;

		// Token: 0x040018FC RID: 6396
		internal double _dpiX = 96.0;

		// Token: 0x040018FD RID: 6397
		internal double _dpiY = 96.0;

		// Token: 0x040018FE RID: 6398
		internal BitmapPalette _palette;

		// Token: 0x040018FF RID: 6399
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x04001900 RID: 6400
		private static readonly PixelFormat[] s_supportedDUCEFormats = new PixelFormat[]
		{
			PixelFormats.Indexed1,
			PixelFormats.BlackWhite,
			PixelFormats.Indexed2,
			PixelFormats.Gray2,
			PixelFormats.Indexed4,
			PixelFormats.Gray4,
			PixelFormats.Indexed8,
			PixelFormats.Gray8,
			PixelFormats.Bgr555,
			PixelFormats.Bgr565,
			PixelFormats.Bgr32,
			PixelFormats.Bgra32,
			PixelFormats.Pbgra32
		};

		// Token: 0x04001901 RID: 6401
		private BitmapSource.WeakBitmapSourceEventSink _weakBitmapSourceEventSink;

		// Token: 0x020008D8 RID: 2264
		[Guid("00000120-a8f2-4877-ba0a-fd2b6645fb94")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IWICBitmapSource
		{
			// Token: 0x060058F4 RID: 22772
			[PreserveSig]
			int GetSize(out int puiWidth, out int puiHeight);

			// Token: 0x060058F5 RID: 22773
			[PreserveSig]
			int GetPixelFormat(out Guid guidFormat);

			// Token: 0x060058F6 RID: 22774
			[PreserveSig]
			int GetResolution(out double pDpiX, out double pDpiY);

			// Token: 0x060058F7 RID: 22775
			[PreserveSig]
			int GetPalette(IntPtr pIPalette);

			// Token: 0x060058F8 RID: 22776
			[PreserveSig]
			int CopyPixels(IntPtr prc, int cbStride, int cbPixels, IntPtr pvPixels);
		}

		// Token: 0x020008D9 RID: 2265
		[ClassInterface(ClassInterfaceType.None)]
		internal class ManagedBitmapSource : BitmapSource.IWICBitmapSource
		{
			// Token: 0x060058F9 RID: 22777 RVA: 0x00169088 File Offset: 0x00168488
			public ManagedBitmapSource(BitmapSource bitmapSource)
			{
				if (bitmapSource == null)
				{
					throw new ArgumentNullException("bitmapSource");
				}
				this._bitmapSource = new WeakReference<BitmapSource>(bitmapSource);
			}

			// Token: 0x060058FA RID: 22778 RVA: 0x001690B8 File Offset: 0x001684B8
			int BitmapSource.IWICBitmapSource.GetSize(out int puiWidth, out int puiHeight)
			{
				BitmapSource bitmapSource;
				if (this._bitmapSource.TryGetTarget(out bitmapSource))
				{
					puiWidth = bitmapSource.PixelWidth;
					puiHeight = bitmapSource.PixelHeight;
					return 0;
				}
				puiWidth = 0;
				puiHeight = 0;
				return -2147467259;
			}

			// Token: 0x060058FB RID: 22779 RVA: 0x001690F4 File Offset: 0x001684F4
			int BitmapSource.IWICBitmapSource.GetPixelFormat(out Guid guidFormat)
			{
				BitmapSource bitmapSource;
				if (this._bitmapSource.TryGetTarget(out bitmapSource))
				{
					guidFormat = bitmapSource.Format.Guid;
					return 0;
				}
				guidFormat = Guid.Empty;
				return -2147467259;
			}

			// Token: 0x060058FC RID: 22780 RVA: 0x00169138 File Offset: 0x00168538
			int BitmapSource.IWICBitmapSource.GetResolution(out double pDpiX, out double pDpiY)
			{
				BitmapSource bitmapSource;
				if (this._bitmapSource.TryGetTarget(out bitmapSource))
				{
					pDpiX = bitmapSource.DpiX;
					pDpiY = bitmapSource.DpiY;
					return 0;
				}
				pDpiX = 0.0;
				pDpiY = 0.0;
				return -2147467259;
			}

			// Token: 0x060058FD RID: 22781 RVA: 0x00169184 File Offset: 0x00168584
			[SecurityCritical]
			int BitmapSource.IWICBitmapSource.GetPalette(IntPtr pIPalette)
			{
				BitmapSource bitmapSource;
				if (!this._bitmapSource.TryGetTarget(out bitmapSource))
				{
					return -2147467259;
				}
				BitmapPalette palette = bitmapSource.Palette;
				if (palette == null || palette.InternalPalette == null || palette.InternalPalette.IsInvalid)
				{
					return -2003292347;
				}
				HRESULT.Check(UnsafeNativeMethods.WICPalette.InitializeFromPalette(pIPalette, palette.InternalPalette));
				return 0;
			}

			// Token: 0x060058FE RID: 22782 RVA: 0x001691E0 File Offset: 0x001685E0
			[SecurityCritical]
			int BitmapSource.IWICBitmapSource.CopyPixels(IntPtr prc, int cbStride, int cbPixels, IntPtr pvPixels)
			{
				if (cbStride < 0)
				{
					return -2147024809;
				}
				if (pvPixels == IntPtr.Zero)
				{
					return -2147024809;
				}
				BitmapSource bitmapSource;
				if (!this._bitmapSource.TryGetTarget(out bitmapSource))
				{
					return -2147467259;
				}
				Int32Rect sourceRect;
				if (prc == IntPtr.Zero)
				{
					sourceRect = new Int32Rect(0, 0, bitmapSource.PixelWidth, bitmapSource.PixelHeight);
				}
				else
				{
					sourceRect = (Int32Rect)Marshal.PtrToStructure(prc, typeof(Int32Rect));
				}
				int height = sourceRect.Height;
				int width = sourceRect.Width;
				if (sourceRect.Width < 1 || sourceRect.Height < 1)
				{
					return -2147024809;
				}
				PixelFormat format = bitmapSource.Format;
				if (format.Format == PixelFormatEnum.Default || format.Format == PixelFormatEnum.Default)
				{
					return -2003292288;
				}
				int num;
				byte[] array;
				long num3;
				checked
				{
					num = (width * format.InternalBitsPerPixel + 7) / 8;
					if (cbPixels < (height - 1) * cbStride + num)
					{
						return -2003292276;
					}
					int num2 = height * num;
					array = new byte[num2];
					bitmapSource.CopyPixels(sourceRect, array, num, 0);
					num3 = pvPixels.ToInt64();
				}
				for (int i = 0; i < height; i++)
				{
					Marshal.Copy(array, i * num, new IntPtr(num3), num);
					num3 += (long)cbStride;
				}
				return 0;
			}

			// Token: 0x04002995 RID: 10645
			private WeakReference<BitmapSource> _bitmapSource;
		}

		// Token: 0x020008DA RID: 2266
		private class WeakBitmapSourceEventSink : WeakReference
		{
			// Token: 0x060058FF RID: 22783 RVA: 0x00169314 File Offset: 0x00168714
			public WeakBitmapSourceEventSink(BitmapSource bitmapSource) : base(bitmapSource)
			{
			}

			// Token: 0x06005900 RID: 22784 RVA: 0x00169328 File Offset: 0x00168728
			public void OnSourceDownloadCompleted(object sender, EventArgs e)
			{
				BitmapSource bitmapSource = this.Target as BitmapSource;
				if (bitmapSource != null)
				{
					bitmapSource.OnSourceDownloadCompleted(bitmapSource, e);
					return;
				}
				this.DetachSourceDownloadHandlers(this.EventSource);
			}

			// Token: 0x06005901 RID: 22785 RVA: 0x0016935C File Offset: 0x0016875C
			public void OnSourceDownloadFailed(object sender, ExceptionEventArgs e)
			{
				BitmapSource bitmapSource = this.Target as BitmapSource;
				if (bitmapSource != null)
				{
					bitmapSource.OnSourceDownloadFailed(bitmapSource, e);
					return;
				}
				this.DetachSourceDownloadHandlers(this.EventSource);
			}

			// Token: 0x06005902 RID: 22786 RVA: 0x00169390 File Offset: 0x00168790
			public void OnSourceDownloadProgress(object sender, DownloadProgressEventArgs e)
			{
				BitmapSource bitmapSource = this.Target as BitmapSource;
				if (bitmapSource != null)
				{
					bitmapSource.OnSourceDownloadProgress(bitmapSource, e);
					return;
				}
				this.DetachSourceDownloadHandlers(this.EventSource);
			}

			// Token: 0x06005903 RID: 22787 RVA: 0x001693C4 File Offset: 0x001687C4
			public void DetachSourceDownloadHandlers(BitmapSource source)
			{
				if (!source.IsFrozen)
				{
					source.DownloadCompleted -= this.OnSourceDownloadCompleted;
					source.DownloadFailed -= this.OnSourceDownloadFailed;
					source.DownloadProgress -= this.OnSourceDownloadProgress;
				}
			}

			// Token: 0x06005904 RID: 22788 RVA: 0x00169410 File Offset: 0x00168810
			public void AttachSourceDownloadHandlers()
			{
				if (!this._eventSource.IsFrozen)
				{
					this._eventSource.DownloadCompleted += this.OnSourceDownloadCompleted;
					this._eventSource.DownloadFailed += this.OnSourceDownloadFailed;
					this._eventSource.DownloadProgress += this.OnSourceDownloadProgress;
				}
			}

			// Token: 0x1700125D RID: 4701
			// (get) Token: 0x06005905 RID: 22789 RVA: 0x00169470 File Offset: 0x00168870
			// (set) Token: 0x06005906 RID: 22790 RVA: 0x00169484 File Offset: 0x00168884
			public BitmapSource EventSource
			{
				get
				{
					return this._eventSource;
				}
				set
				{
					if (this._eventSource != null)
					{
						this.DetachSourceDownloadHandlers(this._eventSource);
					}
					this._eventSource = value;
					if (this._eventSource != null)
					{
						this.AttachSourceDownloadHandlers();
					}
				}
			}

			// Token: 0x04002996 RID: 10646
			private BitmapSource _eventSource;
		}
	}
}
