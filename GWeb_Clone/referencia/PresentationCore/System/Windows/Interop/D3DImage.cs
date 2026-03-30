using System;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Media;
using System.Windows.Media.Composition;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;
using MS.Win32;
using MS.Win32.PresentationCore;

namespace System.Windows.Interop
{
	/// <summary>Um <see cref="T:System.Windows.Media.ImageSource" /> que exibe uma superfície de Direct3D criada pelo usuário.</summary>
	// Token: 0x02000322 RID: 802
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public class D3DImage : ImageSource, IAppDomainShutdownListener
	{
		// Token: 0x06001A84 RID: 6788 RVA: 0x000681E8 File Offset: 0x000675E8
		static D3DImage()
		{
			D3DImage.IsFrontBufferAvailableProperty = D3DImage.IsFrontBufferAvailablePropertyKey.DependencyProperty;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Interop.D3DImage" />.</summary>
		// Token: 0x06001A85 RID: 6789 RVA: 0x00068240 File Offset: 0x00067640
		public D3DImage() : this(96.0, 96.0)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Interop.D3DImage" /> com a resolução de vídeo especificada.</summary>
		/// <param name="dpiX">A resolução de vídeo no eixo x.</param>
		/// <param name="dpiY">A resolução de vídeo no eixo y.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="dpiX" /> ou <paramref name="dpiY" /> é menor que zero.</exception>
		// Token: 0x06001A86 RID: 6790 RVA: 0x00068268 File Offset: 0x00067668
		[SecurityCritical]
		public D3DImage(double dpiX, double dpiY)
		{
			SecurityHelper.DemandUnmanagedCode();
			if (dpiX < 0.0)
			{
				throw new ArgumentOutOfRangeException("dpiX", SR.Get("ParameterMustBeGreaterThanZero"));
			}
			if (dpiY < 0.0)
			{
				throw new ArgumentOutOfRangeException("dpiY", SR.Get("ParameterMustBeGreaterThanZero"));
			}
			this._canWriteEvent = new ManualResetEvent(true);
			this._availableCallback = new UnsafeNativeMethods.InteropDeviceBitmap.FrontBufferAvailableCallback(this.Callback);
			this._sendPresentDelegate = new EventHandler(this.SendPresent);
			this._dpiX = dpiX;
			this._dpiY = dpiY;
			this._listener = new WeakReference(this);
			AppDomainShutdownMonitor.Add(this._listener);
		}

		/// <summary>Libera recursos e executa outras operações de limpeza antes que o <see cref="T:System.Windows.Interop.D3DImage" /> seja reivindicado pela coleta de lixo.</summary>
		// Token: 0x06001A87 RID: 6791 RVA: 0x00068318 File Offset: 0x00067718
		[SecurityTreatAsSafe]
		[SecurityCritical]
		~D3DImage()
		{
			if (this._pInteropDeviceBitmap != null)
			{
				UnsafeNativeMethods.InteropDeviceBitmap.Detach(this._pInteropDeviceBitmap);
			}
			AppDomainShutdownMonitor.Remove(this._listener);
		}

		/// <summary>Atribui uma superfície Direct3D como a origem do buffer de fundo.</summary>
		/// <param name="backBufferType">O tipo da superfície do Direct3D. Deve ser um <see cref="T:System.Windows.Interop.D3DResourceType" /> válida.</param>
		/// <param name="backBuffer">A superfície do Direct3D a ser atribuída como o buffer de fundo.</param>
		/// <exception cref="T:System.InvalidOperationException">O <see cref="T:System.Windows.Interop.D3DImage" /> não foi bloqueado por uma chamada para os métodos <see cref="M:System.Windows.Interop.D3DImage.Lock" /> ou <see cref="M:System.Windows.Interop.D3DImage.TryLock(System.Windows.Duration)" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="backBufferType" /> não é um <see cref="T:System.Windows.Interop.D3DResourceType" /> válido.</exception>
		/// <exception cref="T:System.ArgumentException">Os parâmetros de criação para <paramref name="backBuffer" /> não atendem aos requisitos para o <paramref name="backBufferType" /> ou o dispositivo do <paramref name="backBuffer" /> não é válido.</exception>
		// Token: 0x06001A88 RID: 6792 RVA: 0x00068368 File Offset: 0x00067768
		public void SetBackBuffer(D3DResourceType backBufferType, IntPtr backBuffer)
		{
			this.SetBackBuffer(backBufferType, backBuffer, false);
		}

		/// <summary>Atribui uma superfície Direct3D como a origem do buffer de fundo.</summary>
		/// <param name="backBufferType">O tipo da superfície do Direct3D. Deve ser um <see cref="T:System.Windows.Interop.D3DResourceType" /> válida.</param>
		/// <param name="backBuffer">A superfície do Direct3D a ser atribuída como o buffer de fundo.</param>
		/// <param name="enableSoftwareFallback">
		///   <see langword="true" /> para recorrer à renderização de software; caso contrário, <see langword="false" />.</param>
		// Token: 0x06001A89 RID: 6793 RVA: 0x00068380 File Offset: 0x00067780
		[SecurityCritical]
		public void SetBackBuffer(D3DResourceType backBufferType, IntPtr backBuffer, bool enableSoftwareFallback)
		{
			SecurityHelper.DemandUnmanagedCode();
			base.WritePreamble();
			if (this._lockCount == 0U)
			{
				throw new InvalidOperationException(SR.Get("Image_MustBeLocked"));
			}
			if (backBufferType != D3DResourceType.IDirect3DSurface9)
			{
				throw new ArgumentOutOfRangeException("backBufferType");
			}
			if (backBuffer != IntPtr.Zero && backBuffer == this._pUserSurfaceUnsafe)
			{
				return;
			}
			SafeMILHandle pInteropDeviceBitmap = null;
			uint pixelWidth = 0U;
			uint pixelHeight = 0U;
			if (backBuffer != IntPtr.Zero)
			{
				double dpiX = this._dpiX;
				double dpiY = this._dpiY;
				uint version = this._version + 1U;
				this._version = version;
				HRESULT.Check(UnsafeNativeMethods.InteropDeviceBitmap.Create(backBuffer, dpiX, dpiY, version, this._availableCallback, enableSoftwareFallback, out pInteropDeviceBitmap, out pixelWidth, out pixelHeight));
			}
			if (this._pInteropDeviceBitmap != null)
			{
				UnsafeNativeMethods.InteropDeviceBitmap.Detach(this._pInteropDeviceBitmap);
				this.UnsubscribeFromCommittingBatch();
				this._isDirty = false;
			}
			this._pInteropDeviceBitmap = pInteropDeviceBitmap;
			this._pUserSurfaceUnsafe = backBuffer;
			this._pixelWidth = pixelWidth;
			this._pixelHeight = pixelHeight;
			this._isSoftwareFallbackEnabled = enableSoftwareFallback;
			if (this._pInteropDeviceBitmap == null)
			{
				this._isChangePending = true;
			}
			base.RegisterForAsyncUpdateResource();
			this._waitingForUpdateResourceBecauseBitmapChanged = true;
		}

		/// <summary>Bloqueia o <see cref="T:System.Windows.Interop.D3DImage" /> e permite operações no buffer de fundo.</summary>
		/// <exception cref="T:System.InvalidOperationException">A contagem de bloqueio é igual a <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x06001A8A RID: 6794 RVA: 0x00068484 File Offset: 0x00067884
		public void Lock()
		{
			base.WritePreamble();
			this.LockImpl(Duration.Forever);
		}

		/// <summary>Tenta bloquear a <see cref="T:System.Windows.Interop.D3DImage" /> e aguarda a duração especificada.</summary>
		/// <param name="timeout">A duração de espera para a aquisição do bloqueio.</param>
		/// <returns>
		///   <see langword="true" /> se o bloqueio foi adquirido, caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> é definido como <see cref="P:System.Windows.Duration.Automatic" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">A contagem de bloqueio é igual a <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x06001A8B RID: 6795 RVA: 0x000684A4 File Offset: 0x000678A4
		public bool TryLock(Duration timeout)
		{
			base.WritePreamble();
			if (timeout == Duration.Automatic)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.LockImpl(timeout);
		}

		/// <summary>Diminui a contagem de bloqueio da <see cref="T:System.Windows.Interop.D3DImage" />.</summary>
		// Token: 0x06001A8C RID: 6796 RVA: 0x000684D8 File Offset: 0x000678D8
		public void Unlock()
		{
			base.WritePreamble();
			if (this._lockCount == 0U)
			{
				throw new InvalidOperationException(SR.Get("Image_MustBeLocked"));
			}
			this._lockCount -= 1U;
			if (this._isDirty && this._lockCount == 0U)
			{
				this.SubscribeToCommittingBatch();
			}
			if (this._isChangePending)
			{
				this._isChangePending = false;
				base.WritePostscript();
			}
		}

		/// <summary>Especifica a área do buffer de fundo que foi alterada.</summary>
		/// <param name="dirtyRect">Um <see cref="T:System.Windows.Int32Rect" /> que representa a área alterada.</param>
		/// <exception cref="T:System.InvalidOperationException">O bitmap não foi bloqueado por uma chamada aos métodos <see cref="M:System.Windows.Interop.D3DImage.Lock" /> ou <see cref="M:System.Windows.Interop.D3DImage.TryLock(System.Windows.Duration)" />.  
		///
		/// ou - 
		/// O buffer de fundo não foi atribuído por uma chamada ao método <see cref="M:System.Windows.Interop.D3DImage.SetBackBuffer(System.Windows.Interop.D3DResourceType,System.IntPtr)" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Uma ou mais das seguintes condições é verdadeira.  
		///  <paramref name="dirtyRect.X" /> &lt; 0 
		///  <paramref name="dirtyRect.Y" /> &lt; 0 
		///  <paramref name="dirtyRect.Width" /> &lt; 0 ou <paramref name="dirtyRect.Width" /> &gt; <see cref="P:System.Windows.Interop.D3DImage.PixelWidth" /><paramref name="dirtyRect.Height" /> &lt; 0 ou <paramref name="dirtyRect.Height" /> &gt; <see cref="P:System.Windows.Interop.D3DImage.PixelHeight" /></exception>
		// Token: 0x06001A8D RID: 6797 RVA: 0x0006853C File Offset: 0x0006793C
		[SecurityCritical]
		public void AddDirtyRect(Int32Rect dirtyRect)
		{
			base.WritePreamble();
			if (this._lockCount == 0U)
			{
				throw new InvalidOperationException(SR.Get("Image_MustBeLocked"));
			}
			if (this._pInteropDeviceBitmap == null)
			{
				throw new InvalidOperationException(SR.Get("D3DImage_MustHaveBackBuffer"));
			}
			dirtyRect.ValidateForDirtyRect("dirtyRect", this.PixelWidth, this.PixelHeight);
			if (dirtyRect.HasArea)
			{
				HRESULT.Check(UnsafeNativeMethods.InteropDeviceBitmap.AddDirtyRect(dirtyRect.X, dirtyRect.Y, dirtyRect.Width, dirtyRect.Height, this._pInteropDeviceBitmap));
				this._isDirty = true;
				this._isChangePending = true;
			}
		}

		/// <summary>Obtém um valor que indica se existe um buffer frontal.</summary>
		/// <returns>
		///   <see langword="true" /> Se existir um buffer frontal. Caso contrário, <see langword="false" />.</returns>
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001A8E RID: 6798 RVA: 0x000685DC File Offset: 0x000679DC
		public bool IsFrontBufferAvailable
		{
			get
			{
				return (bool)base.GetValue(D3DImage.IsFrontBufferAvailableProperty);
			}
		}

		/// <summary>Ocorre quando a propriedade <see cref="P:System.Windows.Interop.D3DImage.IsFrontBufferAvailable" /> muda.</summary>
		// Token: 0x14000173 RID: 371
		// (add) Token: 0x06001A8F RID: 6799 RVA: 0x000685FC File Offset: 0x000679FC
		// (remove) Token: 0x06001A90 RID: 6800 RVA: 0x0006862C File Offset: 0x00067A2C
		public event DependencyPropertyChangedEventHandler IsFrontBufferAvailableChanged
		{
			add
			{
				base.WritePreamble();
				if (value != null)
				{
					this._isFrontBufferAvailableChangedHandlers = (DependencyPropertyChangedEventHandler)Delegate.Combine(this._isFrontBufferAvailableChangedHandlers, value);
				}
			}
			remove
			{
				base.WritePreamble();
				if (value != null)
				{
					this._isFrontBufferAvailableChangedHandlers = (DependencyPropertyChangedEventHandler)Delegate.Remove(this._isFrontBufferAvailableChangedHandlers, value);
				}
			}
		}

		/// <summary>Obtém a largura da <see cref="T:System.Windows.Interop.D3DImage" />, em pixels.</summary>
		/// <returns>A largura do <see cref="T:System.Windows.Interop.D3DImage" />, em pixels.</returns>
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x0006865C File Offset: 0x00067A5C
		public int PixelWidth
		{
			get
			{
				base.ReadPreamble();
				return (int)this._pixelWidth;
			}
		}

		/// <summary>Obtém a altura da <see cref="T:System.Windows.Interop.D3DImage" />, em pixels.</summary>
		/// <returns>A altura do <see cref="T:System.Windows.Interop.D3DImage" />, em pixels.</returns>
		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001A92 RID: 6802 RVA: 0x00068678 File Offset: 0x00067A78
		public int PixelHeight
		{
			get
			{
				base.ReadPreamble();
				return (int)this._pixelHeight;
			}
		}

		/// <summary>Obtém a largura da <see cref="T:System.Windows.Interop.D3DImage" />.</summary>
		/// <returns>A largura do <see cref="T:System.Windows.Interop.D3DImage" />, em unidades de medida. Uma unidade de medida é 1/96 polegada.</returns>
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x00068694 File Offset: 0x00067A94
		public sealed override double Width
		{
			get
			{
				base.ReadPreamble();
				return ImageSource.PixelsToDIPs(this._dpiX, (int)this._pixelWidth);
			}
		}

		/// <summary>Obtém a altura da <see cref="T:System.Windows.Interop.D3DImage" />.</summary>
		/// <returns>A altura do <see cref="T:System.Windows.Interop.D3DImage" />, em unidades de medida. Uma unidade de medida é 1/96 polegada.</returns>
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x000686B8 File Offset: 0x00067AB8
		public sealed override double Height
		{
			get
			{
				base.ReadPreamble();
				return ImageSource.PixelsToDIPs(this._dpiY, (int)this._pixelHeight);
			}
		}

		/// <summary>Obtém os metadados associados à fonte da imagem.</summary>
		/// <returns>
		///   <see langword="null" /> em todos os casos.</returns>
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x000686DC File Offset: 0x00067ADC
		public sealed override ImageMetadata Metadata
		{
			get
			{
				base.ReadPreamble();
				return null;
			}
		}

		/// <summary>Cria um clone modificável deste objeto <see cref="T:System.Windows.Interop.D3DImage" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recursos (que podem não mais ser resolvidos), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06001A96 RID: 6806 RVA: 0x000686F0 File Offset: 0x00067AF0
		public new D3DImage Clone()
		{
			return (D3DImage)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Interop.D3DImage" />, fazendo cópias em profundidade dos valores do objeto atual. Referências de recursos, associações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06001A97 RID: 6807 RVA: 0x00068708 File Offset: 0x00067B08
		public new D3DImage CloneCurrentValue()
		{
			return (D3DImage)base.CloneCurrentValue();
		}

		/// <summary>Quando implementado em uma classe derivada, cria uma nova instância da classe derivada <see cref="T:System.Windows.Interop.D3DImage" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06001A98 RID: 6808 RVA: 0x00068720 File Offset: 0x00067B20
		protected override Freezable CreateInstanceCore()
		{
			return new D3DImage();
		}

		/// <summary>Torna a <see cref="T:System.Windows.Interop.D3DImage" /> não modificável ou determina se ela pode se tornar não modificável.</summary>
		/// <param name="isChecking">Não tem efeito.</param>
		/// <returns>
		///   <see langword="false" /> em todos os casos.</returns>
		// Token: 0x06001A99 RID: 6809 RVA: 0x00068734 File Offset: 0x00067B34
		protected sealed override bool FreezeCore(bool isChecking)
		{
			return false;
		}

		/// <summary>Faz com que a instância seja um clone (cópia em profundidade) do <see cref="T:System.Windows.Freezable" /> especificado usando valores de propriedade base (não animados).</summary>
		/// <param name="sourceFreezable">O objeto a ser clonado.</param>
		// Token: 0x06001A9A RID: 6810 RVA: 0x00068744 File Offset: 0x00067B44
		protected override void CloneCore(Freezable sourceFreezable)
		{
			base.CloneCore(sourceFreezable);
			this.CloneCommon(sourceFreezable);
		}

		/// <summary>Torna a instância um clone modificável (cópia em profundidade) do <see cref="T:System.Windows.Freezable" /> especificado usando os valores de propriedade atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Freezable" /> a ser clonado.</param>
		// Token: 0x06001A9B RID: 6811 RVA: 0x00068760 File Offset: 0x00067B60
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			base.CloneCurrentValueCore(sourceFreezable);
			this.CloneCommon(sourceFreezable);
		}

		/// <summary>Torna a instância um clone congelado do <see cref="T:System.Windows.Freezable" /> especificado usando valores de propriedade base (não animados).</summary>
		/// <param name="sourceFreezable">A instância a ser copiada.</param>
		// Token: 0x06001A9C RID: 6812 RVA: 0x0006877C File Offset: 0x00067B7C
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			base.GetAsFrozenCore(sourceFreezable);
			this.CloneCommon(sourceFreezable);
		}

		/// <summary>Torna a instância atual um clone congelado do <see cref="T:System.Windows.Freezable" /> especificado. Se o objeto tiver propriedades de dependência animadas, seus valores animados atuais serão copiados.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Freezable" /> a ser copiado e congelado.</param>
		// Token: 0x06001A9D RID: 6813 RVA: 0x00068798 File Offset: 0x00067B98
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			this.CloneCommon(sourceFreezable);
		}

		/// <summary>Cria uma cópia de software da <see cref="T:System.Windows.Interop.D3DImage" />.</summary>
		/// <returns>Uma <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que é uma cópia de software do estado atual do buffer de fundo; caso contrário, <see langword="null" /> se o buffer de fundo não puder ser lido.</returns>
		// Token: 0x06001A9E RID: 6814 RVA: 0x000687B4 File Offset: 0x00067BB4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected internal virtual BitmapSource CopyBackBuffer()
		{
			SecurityHelper.DemandUnmanagedCode();
			BitmapSource result = null;
			BitmapSourceSafeMILHandle bitmap;
			if (this._pInteropDeviceBitmap != null && HRESULT.Succeeded(UnsafeNativeMethods.InteropDeviceBitmap.GetAsSoftwareBitmap(this._pInteropDeviceBitmap, out bitmap)))
			{
				result = new CachedBitmap(bitmap);
			}
			return result;
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x000687EC File Offset: 0x00067BEC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void CloneCommon(Freezable sourceFreezable)
		{
			D3DImage d3DImage = (D3DImage)sourceFreezable;
			this._dpiX = d3DImage._dpiX;
			this._dpiY = d3DImage._dpiY;
			this.Lock();
			this.SetBackBuffer(D3DResourceType.IDirect3DSurface9, d3DImage._pUserSurfaceUnsafe);
			this.Unlock();
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x00068834 File Offset: 0x00067C34
		private void SubscribeToCommittingBatch()
		{
			if (!this._isWaitingForPresent)
			{
				MediaContext mediaContext = MediaContext.From(base.Dispatcher);
				if (this._duceResource.IsOnChannel(mediaContext.Channel))
				{
					mediaContext.CommittingBatch += this._sendPresentDelegate;
					this._isWaitingForPresent = true;
				}
			}
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x0006887C File Offset: 0x00067C7C
		private void UnsubscribeFromCommittingBatch()
		{
			if (this._isWaitingForPresent)
			{
				MediaContext mediaContext = MediaContext.From(base.Dispatcher);
				mediaContext.CommittingBatch -= this._sendPresentDelegate;
				this._isWaitingForPresent = false;
			}
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x000688B0 File Offset: 0x00067CB0
		private bool LockImpl(Duration timeout)
		{
			bool result = false;
			if (this._lockCount == 4294967295U)
			{
				throw new InvalidOperationException(SR.Get("Image_LockCountLimit"));
			}
			if (this._lockCount == 0U)
			{
				if (timeout == Duration.Forever)
				{
					result = this._canWriteEvent.WaitOne();
				}
				else
				{
					result = this._canWriteEvent.WaitOne(timeout.TimeSpan, false);
				}
				this.UnsubscribeFromCommittingBatch();
			}
			this._lockCount += 1U;
			return result;
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x00068924 File Offset: 0x00067D24
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private static void IsFrontBufferAvailablePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			bool flag = (bool)e.NewValue;
			D3DImage d3DImage = (D3DImage)d;
			if (!flag && !d3DImage._isSoftwareFallbackEnabled)
			{
				d3DImage._pUserSurfaceUnsafe = IntPtr.Zero;
			}
			if (d3DImage._isFrontBufferAvailableChangedHandlers != null)
			{
				d3DImage._isFrontBufferAvailableChangedHandlers(d3DImage, e);
			}
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x00068970 File Offset: 0x00067D70
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private unsafe void SendPresent(object sender, EventArgs args)
		{
			if (this._waitingForUpdateResourceBecauseBitmapChanged)
			{
				return;
			}
			this.UnsubscribeFromCommittingBatch();
			DUCE.Channel channel = sender as DUCE.Channel;
			DUCE.MILCMD_D3DIMAGE_PRESENT milcmd_D3DIMAGE_PRESENT;
			milcmd_D3DIMAGE_PRESENT.Type = MILCMD.MilCmdD3DImagePresent;
			milcmd_D3DIMAGE_PRESENT.Handle = this._duceResource.GetHandle(channel);
			IntPtr currentProcess = UnsafeNativeMethods.GetCurrentProcess();
			IntPtr intPtr;
			if (!UnsafeNativeMethods.DuplicateHandle(currentProcess, this._canWriteEvent.SafeWaitHandle, currentProcess, out intPtr, 0U, false, 2U))
			{
				throw new Win32Exception();
			}
			milcmd_D3DIMAGE_PRESENT.hEvent = intPtr.ToPointer();
			channel.SendCommand((byte*)(&milcmd_D3DIMAGE_PRESENT), sizeof(DUCE.MILCMD_D3DIMAGE_PRESENT), true);
			this._isDirty = false;
			this._canWriteEvent.Reset();
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x00068A08 File Offset: 0x00067E08
		private object SetIsFrontBufferAvailable(object isAvailableVersionPair)
		{
			Pair pair = (Pair)isAvailableVersionPair;
			uint num = (uint)pair.Second;
			if (num == this._version)
			{
				bool value = (bool)pair.First;
				base.SetValue(D3DImage.IsFrontBufferAvailablePropertyKey, value);
			}
			return null;
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x00068A4C File Offset: 0x00067E4C
		private void Callback(bool isFrontBufferAvailable, uint version)
		{
			base.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new DispatcherOperationCallback(this.SetIsFrontBufferAvailable), new Pair(BooleanBoxes.Box(isFrontBufferAvailable), version));
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x00068A84 File Offset: 0x00067E84
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				bool isSynchronous = channel.IsSynchronous;
				DUCE.MILCMD_D3DIMAGE milcmd_D3DIMAGE;
				milcmd_D3DIMAGE.Type = MILCMD.MilCmdD3DImage;
				milcmd_D3DIMAGE.Handle = this._duceResource.GetHandle(channel);
				if (this._pInteropDeviceBitmap != null)
				{
					UnsafeNativeMethods.MILUnknown.AddRef(this._pInteropDeviceBitmap);
					milcmd_D3DIMAGE.pInteropDeviceBitmap = this._pInteropDeviceBitmap.DangerousGetHandle().ToPointer();
				}
				else
				{
					milcmd_D3DIMAGE.pInteropDeviceBitmap = 0UL;
				}
				milcmd_D3DIMAGE.pSoftwareBitmap = 0UL;
				if (isSynchronous)
				{
					this._softwareCopy = this.CopyBackBuffer();
					if (this._softwareCopy != null)
					{
						UnsafeNativeMethods.MILUnknown.AddRef(this._softwareCopy.WicSourceHandle);
						milcmd_D3DIMAGE.pSoftwareBitmap = this._softwareCopy.WicSourceHandle.DangerousGetHandle().ToPointer();
					}
				}
				channel.SendCommand((byte*)(&milcmd_D3DIMAGE), sizeof(DUCE.MILCMD_D3DIMAGE), false);
				if (!isSynchronous)
				{
					this._waitingForUpdateResourceBecauseBitmapChanged = false;
				}
			}
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x00068B78 File Offset: 0x00067F78
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_D3DIMAGE))
			{
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
				if (!channel.IsSynchronous && this._isDirty)
				{
					this.SubscribeToCommittingBatch();
				}
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x00068BC8 File Offset: 0x00067FC8
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				this.ReleaseOnChannelAnimations(channel);
				if (!channel.IsSynchronous)
				{
					this.UnsubscribeFromCommittingBatch();
				}
			}
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x00068BF8 File Offset: 0x00067FF8
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x00068C14 File Offset: 0x00068014
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x00068C2C File Offset: 0x0006802C
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x00068C48 File Offset: 0x00068048
		[SecurityTreatAsSafe]
		[SecurityCritical]
		void IAppDomainShutdownListener.NotifyShutdown()
		{
			if (this._pInteropDeviceBitmap != null)
			{
				UnsafeNativeMethods.InteropDeviceBitmap.Detach(this._pInteropDeviceBitmap);
			}
			GC.SuppressFinalize(this);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Interop.D3DImage.IsFrontBufferAvailable" />.</summary>
		// Token: 0x04000E36 RID: 3638
		public static readonly DependencyProperty IsFrontBufferAvailableProperty;

		// Token: 0x04000E37 RID: 3639
		private static readonly DependencyPropertyKey IsFrontBufferAvailablePropertyKey = DependencyProperty.RegisterReadOnly("IsFrontBufferAvailable", typeof(bool), typeof(D3DImage), new UIPropertyMetadata(BooleanBoxes.TrueBox, new PropertyChangedCallback(D3DImage.IsFrontBufferAvailablePropertyChanged)));

		// Token: 0x04000E38 RID: 3640
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x04000E39 RID: 3641
		private double _dpiX;

		// Token: 0x04000E3A RID: 3642
		private double _dpiY;

		// Token: 0x04000E3B RID: 3643
		private SafeMILHandle _pInteropDeviceBitmap;

		// Token: 0x04000E3C RID: 3644
		private BitmapSource _softwareCopy;

		// Token: 0x04000E3D RID: 3645
		[SecurityCritical]
		private IntPtr _pUserSurfaceUnsafe;

		// Token: 0x04000E3E RID: 3646
		private bool _isSoftwareFallbackEnabled;

		// Token: 0x04000E3F RID: 3647
		private ManualResetEvent _canWriteEvent;

		// Token: 0x04000E40 RID: 3648
		[SecurityCritical]
		private UnsafeNativeMethods.InteropDeviceBitmap.FrontBufferAvailableCallback _availableCallback;

		// Token: 0x04000E41 RID: 3649
		private DependencyPropertyChangedEventHandler _isFrontBufferAvailableChangedHandlers;

		// Token: 0x04000E42 RID: 3650
		private EventHandler _sendPresentDelegate;

		// Token: 0x04000E43 RID: 3651
		private WeakReference _listener;

		// Token: 0x04000E44 RID: 3652
		private uint _lockCount;

		// Token: 0x04000E45 RID: 3653
		private uint _pixelWidth;

		// Token: 0x04000E46 RID: 3654
		private uint _pixelHeight;

		// Token: 0x04000E47 RID: 3655
		private uint _version;

		// Token: 0x04000E48 RID: 3656
		private bool _isDirty;

		// Token: 0x04000E49 RID: 3657
		private bool _isWaitingForPresent;

		// Token: 0x04000E4A RID: 3658
		private bool _isChangePending;

		// Token: 0x04000E4B RID: 3659
		private bool _waitingForUpdateResourceBecauseBitmapChanged;
	}
}
