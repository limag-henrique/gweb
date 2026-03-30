using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Windows.Media.Composition;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Fornece um <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que pode ser gravado e atualizado.</summary>
	// Token: 0x02000603 RID: 1539
	public sealed class WriteableBitmap : BitmapSource
	{
		// Token: 0x0600466A RID: 18026 RVA: 0x00113738 File Offset: 0x00112B38
		internal WriteableBitmap()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.WriteableBitmap" /> usando a <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> fornecida.</summary>
		/// <param name="source">A <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> a ser usada para inicialização.</param>
		// Token: 0x0600466B RID: 18027 RVA: 0x00113760 File Offset: 0x00112B60
		[SecurityCritical]
		public WriteableBitmap(BitmapSource source) : base(true)
		{
			this.InitFromBitmapSource(source);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.WriteableBitmap" /> com os parâmetros especificados.</summary>
		/// <param name="pixelWidth">A largura desejada do bitmap.</param>
		/// <param name="pixelHeight">A altura desejada do bitmap.</param>
		/// <param name="dpiX">O dpi (pontos por polegada) horizontal do bitmap.</param>
		/// <param name="dpiY">O dpi (pontos por polegada) vertical do bitmap.</param>
		/// <param name="pixelFormat">O <see cref="T:System.Windows.Media.PixelFormat" /> do bitmap.</param>
		/// <param name="palette">O <see cref="T:System.Windows.Media.Imaging.BitmapPalette" /> do bitmap.</param>
		// Token: 0x0600466C RID: 18028 RVA: 0x00113790 File Offset: 0x00112B90
		[SecurityCritical]
		public WriteableBitmap(int pixelWidth, int pixelHeight, double dpiX, double dpiY, PixelFormat pixelFormat, BitmapPalette palette) : base(true)
		{
			this.BeginInit();
			if (pixelFormat.Palettized && palette == null)
			{
				throw new InvalidOperationException(SR.Get("Image_IndexedPixelFormatRequiresPalette"));
			}
			if (pixelFormat.Format == PixelFormatEnum.Default)
			{
				throw new ArgumentException(SR.Get("Effect_PixelFormat"), "pixelFormat");
			}
			if (pixelWidth < 0)
			{
				HRESULT.Check(-2147024362);
			}
			if (pixelWidth == 0)
			{
				HRESULT.Check(-2147024809);
			}
			if (pixelHeight < 0)
			{
				HRESULT.Check(-2147024362);
			}
			if (pixelHeight == 0)
			{
				HRESULT.Check(-2147024809);
			}
			Guid guid = pixelFormat.Guid;
			SafeMILHandle pPalette = new SafeMILHandle();
			if (pixelFormat.Palettized)
			{
				pPalette = palette.InternalPalette;
			}
			HRESULT.Check(MILSwDoubleBufferedBitmap.Create((uint)pixelWidth, (uint)pixelHeight, dpiX, dpiY, ref guid, pPalette, out this._pDoubleBufferedBitmap));
			this._pDoubleBufferedBitmap.UpdateEstimatedSize(this.GetEstimatedSize(pixelWidth, pixelHeight, pixelFormat));
			this.Lock();
			this.Unlock();
			this.EndInit();
		}

		/// <summary>Especifica a área do bitmap que foi alterada.</summary>
		/// <param name="dirtyRect">Um <see cref="T:System.Windows.Int32Rect" /> que representa a área que foi alterada. As dimensões estão em pixels.</param>
		/// <exception cref="T:System.InvalidOperationException">O bitmap não foi bloqueado por uma chamada aos métodos <see cref="M:System.Windows.Media.Imaging.WriteableBitmap.Lock" /> ou <see cref="M:System.Windows.Media.Imaging.WriteableBitmap.TryLock(System.Windows.Duration)" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="dirtyRect" /> está fora dos limites do <see cref="T:System.Windows.Media.Imaging.WriteableBitmap" />.</exception>
		// Token: 0x0600466D RID: 18029 RVA: 0x0011388C File Offset: 0x00112C8C
		[SecurityCritical]
		public void AddDirtyRect(Int32Rect dirtyRect)
		{
			base.WritePreamble();
			if (this._lockCount == 0U)
			{
				throw new InvalidOperationException(SR.Get("Image_MustBeLocked"));
			}
			dirtyRect.ValidateForDirtyRect("dirtyRect", this._pixelWidth, this._pixelHeight);
			if (dirtyRect.HasArea)
			{
				MILSwDoubleBufferedBitmap.AddDirtyRect(this._pDoubleBufferedBitmap, ref dirtyRect);
				this._hasDirtyRects = true;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Imaging.WriteableBitmap" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x0600466E RID: 18030 RVA: 0x001138EC File Offset: 0x00112CEC
		public new WriteableBitmap Clone()
		{
			return (WriteableBitmap)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Animation.ByteAnimationUsingKeyFrames" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x0600466F RID: 18031 RVA: 0x00113904 File Offset: 0x00112D04
		public new WriteableBitmap CloneCurrentValue()
		{
			return (WriteableBitmap)base.CloneCurrentValue();
		}

		/// <summary>Reserva o buffer de fundo para atualizações.</summary>
		// Token: 0x06004670 RID: 18032 RVA: 0x0011391C File Offset: 0x00112D1C
		public void Lock()
		{
			bool flag = this.TryLock(Duration.Forever);
		}

		/// <summary>Tenta bloquear o bitmap, aguardando não mais do que o tempo especificado.</summary>
		/// <param name="timeout">Um <see cref="T:System.Windows.Duration" /> que representa o tempo de espera. Um valor igual a 0 é retornado imediatamente. Um valor igual a <see cref="P:System.Windows.Duration.Forever" /> bloqueia indefinidamente.</param>
		/// <returns>
		///   <see langword="true" /> se o bloqueio foi adquirido, caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> é definido como <see cref="P:System.Windows.Duration.Automatic" />.</exception>
		// Token: 0x06004671 RID: 18033 RVA: 0x00113938 File Offset: 0x00112D38
		[SecurityCritical]
		public bool TryLock(Duration timeout)
		{
			base.WritePreamble();
			if (timeout == Duration.Automatic)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			TimeSpan timeout2;
			if (timeout == Duration.Forever)
			{
				timeout2 = TimeSpan.FromMilliseconds(-1.0);
			}
			else
			{
				timeout2 = timeout.TimeSpan;
			}
			if (this._lockCount == 4294967295U)
			{
				throw new InvalidOperationException(SR.Get("Image_LockCountLimit"));
			}
			if (this._lockCount == 0U)
			{
				if (!this.AcquireBackBuffer(timeout2, true))
				{
					return false;
				}
				Int32Rect int32Rect = new Int32Rect(0, 0, this._pixelWidth, this._pixelHeight);
				HRESULT.Check(UnsafeNativeMethods.WICBitmap.Lock(base.WicSourceHandle, ref int32Rect, LockFlags.MIL_LOCK_WRITE, out this._pBackBufferLock));
				if (this._backBuffer == IntPtr.Zero)
				{
					IntPtr zero = IntPtr.Zero;
					uint num = 0U;
					HRESULT.Check(UnsafeNativeMethods.WICBitmapLock.GetDataPointer(this._pBackBufferLock, ref num, ref zero));
					this.BackBuffer = zero;
					uint num2 = 0U;
					HRESULT.Check(UnsafeNativeMethods.WICBitmapLock.GetStride(this._pBackBufferLock, ref num2));
					Invariant.Assert(num2 <= 2147483647U);
					this._backBufferStride.Value = (int)num2;
				}
				this.UnsubscribeFromCommittingBatch();
			}
			this._lockCount += 1U;
			return true;
		}

		/// <summary>Libera o buffer de fundo para torná-lo disponível para exibição.</summary>
		/// <exception cref="T:System.InvalidOperationException">O bitmap não foi bloqueado por uma chamada aos métodos <see cref="M:System.Windows.Media.Imaging.WriteableBitmap.Lock" /> ou <see cref="M:System.Windows.Media.Imaging.WriteableBitmap.TryLock(System.Windows.Duration)" />.</exception>
		// Token: 0x06004672 RID: 18034 RVA: 0x00113A64 File Offset: 0x00112E64
		[SecurityCritical]
		public void Unlock()
		{
			base.WritePreamble();
			if (this._lockCount == 0U)
			{
				throw new InvalidOperationException(SR.Get("Image_MustBeLocked"));
			}
			Invariant.Assert(this._lockCount > 0U, "Lock count should never be negative!");
			this._lockCount -= 1U;
			if (this._lockCount == 0U)
			{
				this._pBackBufferLock.Dispose();
				this._pBackBufferLock = null;
				if (this._hasDirtyRects)
				{
					this.SubscribeToCommittingBatch();
					base.WritePostscript();
				}
			}
		}

		/// <summary>Atualiza os pixels na região especificada do bitmap.</summary>
		/// <param name="sourceRect">O retângulo no <paramref name="sourceBuffer" /> a ser copiado.</param>
		/// <param name="sourceBuffer">O buffer de entrada usado para atualizar o bitmap.</param>
		/// <param name="sourceBufferSize">O tamanho do buffer de entrada.</param>
		/// <param name="sourceBufferStride">A distância do buffer de entrada, em bytes.</param>
		/// <param name="destinationX">A coordenada x de destino do pixel mais à esquerda no buffer de fundo.</param>
		/// <param name="destinationY">A coordenada y de destino do pixel mais ao topo no buffer de fundo.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Uma ou mais das seguintes condições é verdadeira.  
		///  <paramref name="sourceRect" /> está fora dos limites do <see cref="T:System.Windows.Media.Imaging.WriteableBitmap" />.  
		///  <paramref name="destinationX" /> ou <paramref name="destinationY" /> está fora dos limites do <see cref="T:System.Windows.Media.Imaging.WriteableBitmap" />.  
		///  <paramref name="sourceBufferSize" /> &lt; 1 
		///  <paramref name="sourceBufferStride" /> &lt; 1</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceBuffer" /> é <see langword="null" />.</exception>
		// Token: 0x06004673 RID: 18035 RVA: 0x00113AE0 File Offset: 0x00112EE0
		[SecurityCritical]
		public void WritePixels(Int32Rect sourceRect, IntPtr sourceBuffer, int sourceBufferSize, int sourceBufferStride, int destinationX, int destinationY)
		{
			SecurityHelper.DemandUnmanagedCode();
			base.WritePreamble();
			this.WritePixelsImpl(sourceRect, sourceBuffer, sourceBufferSize, sourceBufferStride, destinationX, destinationY, false);
		}

		/// <summary>Atualiza os pixels na região especificada do bitmap.</summary>
		/// <param name="sourceRect">O retângulo no <paramref name="sourceBuffer" /> a ser copiado.</param>
		/// <param name="sourceBuffer">O buffer de entrada usado para atualizar o bitmap.</param>
		/// <param name="sourceBufferStride">A distância do buffer de entrada, em bytes.</param>
		/// <param name="destinationX">A coordenada x de destino do pixel mais à esquerda no buffer de fundo.</param>
		/// <param name="destinationY">A coordenada y de destino do pixel mais ao topo no buffer de fundo.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Uma ou mais das seguintes condições é verdadeira.  
		///  <paramref name="sourceRect" /> está fora dos limites do <see cref="T:System.Windows.Media.Imaging.WriteableBitmap" />.  
		///  <paramref name="destinationX" /> ou <paramref name="destinationY" /> está fora dos limites do <see cref="T:System.Windows.Media.Imaging.WriteableBitmap" />.  
		///  <paramref name="sourceBufferStride" /> &lt; 1</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceBuffer" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceBuffer" /> tem uma classificação diferente de 1 ou 2 ou seu tamanho é menor ou igual a 0.</exception>
		// Token: 0x06004674 RID: 18036 RVA: 0x00113B08 File Offset: 0x00112F08
		[SecurityCritical]
		public void WritePixels(Int32Rect sourceRect, Array sourceBuffer, int sourceBufferStride, int destinationX, int destinationY)
		{
			base.WritePreamble();
			int num;
			int sourceBufferSize;
			Type type;
			this.ValidateArrayAndGetInfo(sourceBuffer, false, out num, out sourceBufferSize, out type);
			if (type == null || !type.IsValueType)
			{
				throw new ArgumentException(SR.Get("Image_InvalidArrayForPixel"));
			}
			GCHandle gchandle = GCHandle.Alloc(sourceBuffer, GCHandleType.Pinned);
			try
			{
				IntPtr sourceBuffer2 = gchandle.AddrOfPinnedObject();
				this.WritePixelsImpl(sourceRect, sourceBuffer2, sourceBufferSize, sourceBufferStride, destinationX, destinationY, false);
			}
			finally
			{
				gchandle.Free();
			}
		}

		/// <summary>Atualiza os pixels na região especificada do bitmap.</summary>
		/// <param name="sourceRect">O retângulo do <see cref="T:System.Windows.Media.Imaging.WriteableBitmap" /> a ser atualizado.</param>
		/// <param name="buffer">O buffer de entrada usado para atualizar o bitmap.</param>
		/// <param name="bufferSize">O tamanho do buffer de entrada.</param>
		/// <param name="stride">A distância da região de atualização em <paramref name="buffer" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Uma ou mais das seguintes condições é verdadeira.  
		///  <paramref name="sourceRect" /> está fora dos limites do <see cref="T:System.Windows.Media.Imaging.WriteableBitmap" />.  
		///  <paramref name="bufferSize" /> &lt; 1 
		///  <paramref name="stride" /> &lt; 1</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> é <see langword="null" />.</exception>
		// Token: 0x06004675 RID: 18037 RVA: 0x00113B90 File Offset: 0x00112F90
		[SecurityCritical]
		public void WritePixels(Int32Rect sourceRect, IntPtr buffer, int bufferSize, int stride)
		{
			SecurityHelper.DemandUnmanagedCode();
			base.WritePreamble();
			if (bufferSize < 1)
			{
				throw new ArgumentOutOfRangeException("bufferSize", SR.Get("ParameterCannotBeLessThan", new object[]
				{
					1
				}));
			}
			if (stride < 1)
			{
				throw new ArgumentOutOfRangeException("stride", SR.Get("ParameterCannotBeLessThan", new object[]
				{
					1
				}));
			}
			if (sourceRect.IsEmpty || sourceRect.Width <= 0 || sourceRect.Height <= 0)
			{
				return;
			}
			int x = sourceRect.X;
			int y = sourceRect.Y;
			sourceRect.X = 0;
			sourceRect.Y = 0;
			this.WritePixelsImpl(sourceRect, buffer, bufferSize, stride, x, y, true);
		}

		/// <summary>Atualiza os pixels na região especificada do bitmap.</summary>
		/// <param name="sourceRect">O retângulo do <see cref="T:System.Windows.Media.Imaging.WriteableBitmap" /> a ser atualizado.</param>
		/// <param name="pixels">A matriz de pixel usada para atualizar o bitmap.</param>
		/// <param name="stride">A distância da região de atualização em <paramref name="pixels" />.</param>
		/// <param name="offset">O deslocamento do buffer de entrada.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Uma ou mais das seguintes condições é verdadeira.  
		///  <paramref name="sourceRect" /> está fora dos limites do <see cref="T:System.Windows.Media.Imaging.WriteableBitmap" />.  
		///  <paramref name="stride" /> &lt; 1 
		///  <paramref name="offset" /> &lt; 0</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pixels" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pixels" /> tem uma classificação diferente de 1 ou 2 ou seu tamanho é menor ou igual a 0.</exception>
		// Token: 0x06004676 RID: 18038 RVA: 0x00113C48 File Offset: 0x00113048
		[SecurityCritical]
		public void WritePixels(Int32Rect sourceRect, Array pixels, int stride, int offset)
		{
			base.WritePreamble();
			if (sourceRect.IsEmpty || sourceRect.Width <= 0 || sourceRect.Height <= 0)
			{
				return;
			}
			int num;
			int num2;
			Type type;
			this.ValidateArrayAndGetInfo(pixels, true, out num, out num2, out type);
			if (stride < 1)
			{
				throw new ArgumentOutOfRangeException("stride", SR.Get("ParameterCannotBeLessThan", new object[]
				{
					1
				}));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.Get("ParameterCannotBeLessThan", new object[]
				{
					0
				}));
			}
			if (type == null || !type.IsValueType)
			{
				throw new ArgumentException(SR.Get("Image_InvalidArrayForPixel"));
			}
			checked
			{
				int num3 = offset * num;
				if (num3 >= num2)
				{
					throw new IndexOutOfRangeException();
				}
				int x = sourceRect.X;
				int y = sourceRect.Y;
				sourceRect.X = 0;
				sourceRect.Y = 0;
				GCHandle gchandle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
				try
				{
					IntPtr intPtr = gchandle.AddrOfPinnedObject();
					intPtr = new IntPtr((long)intPtr + unchecked((long)num3));
					num2 -= num3;
					this.WritePixelsImpl(sourceRect, intPtr, num2, stride, x, y, true);
				}
				finally
				{
					gchandle.Free();
				}
			}
		}

		// Token: 0x06004677 RID: 18039 RVA: 0x00113D88 File Offset: 0x00113188
		protected override Freezable CreateInstanceCore()
		{
			return new WriteableBitmap();
		}

		// Token: 0x06004678 RID: 18040 RVA: 0x00113D9C File Offset: 0x0011319C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected override void CloneCore(Freezable sourceFreezable)
		{
			WriteableBitmap sourceBitmap = (WriteableBitmap)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(sourceBitmap);
		}

		// Token: 0x06004679 RID: 18041 RVA: 0x00113DC0 File Offset: 0x001131C0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected override bool FreezeCore(bool isChecking)
		{
			bool flag = this._lockCount == 0U && base.FreezeCore(isChecking);
			if (flag && !isChecking)
			{
				HRESULT.Check(MILSwDoubleBufferedBitmap.ProtectBackBuffer(this._pDoubleBufferedBitmap));
				this.AcquireBackBuffer(TimeSpan.Zero, false);
				this._needsUpdate = true;
				this._hasDirtyRects = false;
				base.WicSourceHandle.CopyMemoryPressure(this._pDoubleBufferedBitmap);
				this._actLikeSimpleBitmap = true;
				int channelCount = this._duceResource.GetChannelCount();
				for (int i = 0; i < channelCount; i++)
				{
					DUCE.Channel channel = this._duceResource.GetChannel(i);
					uint refCountOnChannel = this._duceResource.GetRefCountOnChannel(channel);
					for (uint num = 0U; num < refCountOnChannel; num += 1U)
					{
						((DUCE.IResource)this).ReleaseOnChannel(channel);
					}
					for (uint num2 = 0U; num2 < refCountOnChannel; num2 += 1U)
					{
						((DUCE.IResource)this).AddRefOnChannel(channel);
					}
				}
				this._pDoubleBufferedBitmap.Dispose();
				this._pDoubleBufferedBitmap = null;
				this._copyCompletedEvent.Close();
				this._copyCompletedEvent = null;
				this._committingBatchHandler = null;
				this._pBackBuffer = null;
			}
			return flag;
		}

		// Token: 0x0600467A RID: 18042 RVA: 0x00113EC8 File Offset: 0x001132C8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			WriteableBitmap sourceBitmap = (WriteableBitmap)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(sourceBitmap);
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x00113EEC File Offset: 0x001132EC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			WriteableBitmap sourceBitmap = (WriteableBitmap)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			this.CopyCommon(sourceBitmap);
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x00113F10 File Offset: 0x00113310
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			WriteableBitmap sourceBitmap = (WriteableBitmap)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			this.CopyCommon(sourceBitmap);
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x00113F34 File Offset: 0x00113334
		private long GetEstimatedSize(int pixelWidth, int pixelHeight, PixelFormat pixelFormat)
		{
			return (long)(pixelWidth * pixelHeight * pixelFormat.InternalBitsPerPixel / 8 * 2);
		}

		// Token: 0x0600467E RID: 18046 RVA: 0x00113F54 File Offset: 0x00113354
		[SecurityCritical]
		private void InitFromBitmapSource(BitmapSource source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (source.PixelWidth < 0)
			{
				HRESULT.Check(-2147024362);
			}
			if (source.PixelHeight < 0)
			{
				HRESULT.Check(-2147024362);
			}
			this.BeginInit();
			this._syncObject = source.SyncObject;
			object syncObject = this._syncObject;
			lock (syncObject)
			{
				Guid guid = source.Format.Guid;
				SafeMILHandle pPalette = new SafeMILHandle();
				if (source.Format.Palettized)
				{
					pPalette = source.Palette.InternalPalette;
				}
				HRESULT.Check(MILSwDoubleBufferedBitmap.Create((uint)source.PixelWidth, (uint)source.PixelHeight, source.DpiX, source.DpiY, ref guid, pPalette, out this._pDoubleBufferedBitmap));
				this._pDoubleBufferedBitmap.UpdateEstimatedSize(this.GetEstimatedSize(source.PixelWidth, source.PixelHeight, source.Format));
				this.Lock();
				Int32Rect int32Rect = new Int32Rect(0, 0, this._pixelWidth, this._pixelHeight);
				int bufferSize = checked(this._backBufferStride.Value * source.PixelHeight);
				source.CriticalCopyPixels(int32Rect, this._backBuffer, bufferSize, this._backBufferStride.Value);
				this.AddDirtyRect(int32Rect);
				this.Unlock();
			}
			this.EndInit();
		}

		// Token: 0x0600467F RID: 18047 RVA: 0x001140C0 File Offset: 0x001134C0
		[SecurityCritical]
		private unsafe void WritePixelsImpl(Int32Rect sourceRect, IntPtr sourceBuffer, int sourceBufferSize, int sourceBufferStride, int destinationX, int destinationY, bool backwardsCompat)
		{
			if (sourceRect.X < 0)
			{
				throw new ArgumentOutOfRangeException("sourceRect", SR.Get("ParameterCannotBeNegative"));
			}
			if (sourceRect.Y < 0)
			{
				throw new ArgumentOutOfRangeException("sourceRect", SR.Get("ParameterCannotBeNegative"));
			}
			if (sourceRect.Width < 0)
			{
				throw new ArgumentOutOfRangeException("sourceRect", SR.Get("ParameterMustBeBetween", new object[]
				{
					0,
					this._pixelWidth
				}));
			}
			if (sourceRect.Width > this._pixelWidth)
			{
				if (!backwardsCompat)
				{
					throw new ArgumentOutOfRangeException("sourceRect", SR.Get("ParameterMustBeBetween", new object[]
					{
						0,
						this._pixelWidth
					}));
				}
				HRESULT.Check(-2147024809);
			}
			if (sourceRect.Height < 0)
			{
				throw new ArgumentOutOfRangeException("sourceRect", SR.Get("ParameterMustBeBetween", new object[]
				{
					0,
					this._pixelHeight
				}));
			}
			if (sourceRect.Height > this._pixelHeight)
			{
				if (!backwardsCompat)
				{
					throw new ArgumentOutOfRangeException("sourceRect", SR.Get("ParameterMustBeBetween", new object[]
					{
						0,
						this._pixelHeight
					}));
				}
				HRESULT.Check(-2147024809);
			}
			if (destinationX < 0)
			{
				if (!backwardsCompat)
				{
					throw new ArgumentOutOfRangeException("sourceRect", SR.Get("ParameterCannotBeNegative"));
				}
				HRESULT.Check(-2147024362);
			}
			if (destinationX > this._pixelWidth - sourceRect.Width)
			{
				if (!backwardsCompat)
				{
					throw new ArgumentOutOfRangeException("destinationX", SR.Get("ParameterMustBeBetween", new object[]
					{
						0,
						this._pixelWidth - sourceRect.Width
					}));
				}
				HRESULT.Check(-2147024809);
			}
			if (destinationY < 0)
			{
				if (!backwardsCompat)
				{
					throw new ArgumentOutOfRangeException("destinationY", SR.Get("ParameterMustBeBetween", new object[]
					{
						0,
						this._pixelHeight - sourceRect.Height
					}));
				}
				HRESULT.Check(-2147024362);
			}
			if (destinationY > this._pixelHeight - sourceRect.Height)
			{
				if (!backwardsCompat)
				{
					throw new ArgumentOutOfRangeException("destinationY", SR.Get("ParameterMustBeBetween", new object[]
					{
						0,
						this._pixelHeight - sourceRect.Height
					}));
				}
				HRESULT.Check(-2147024809);
			}
			if (sourceBuffer == IntPtr.Zero)
			{
				throw new ArgumentNullException(backwardsCompat ? "buffer" : "sourceBuffer");
			}
			if (sourceBufferStride < 1)
			{
				throw new ArgumentOutOfRangeException("sourceBufferStride", SR.Get("ParameterCannotBeLessThan", new object[]
				{
					1
				}));
			}
			if (sourceRect.Width == 0 || sourceRect.Height == 0)
			{
				return;
			}
			checked
			{
				uint num = (uint)((sourceRect.X + sourceRect.Width) * this._format.InternalBitsPerPixel);
				uint num2 = (num + 7U) / 8U;
				uint num3 = (uint)((sourceRect.Y + sourceRect.Height - 1) * sourceBufferStride) + num2;
				if (unchecked((long)sourceBufferSize < (long)((ulong)num3)))
				{
					if (!backwardsCompat)
					{
						throw new ArgumentException(SR.Get("Image_InsufficientBufferSize"), "sourceBufferSize");
					}
					HRESULT.Check(-2003292276);
				}
				uint copyWidthInBits = (uint)(sourceRect.Width * this._format.InternalBitsPerPixel);
				uint num4 = (uint)(sourceRect.X * this._format.InternalBitsPerPixel / 8);
				uint inputBufferOffsetInBits = (uint)(sourceRect.X * this._format.InternalBitsPerPixel % 8);
				uint num5 = (uint)(unchecked((long)(checked(sourceRect.Y * sourceBufferStride))) + (long)(unchecked((ulong)num4)));
				uint num6 = (uint)(destinationX * this._format.InternalBitsPerPixel / 8);
				uint outputBufferOffsetInBits = (uint)(destinationX * this._format.InternalBitsPerPixel % 8);
				Int32Rect dirtyRect = sourceRect;
				dirtyRect.X = destinationX;
				dirtyRect.Y = destinationY;
				uint num7 = (uint)(destinationY * this._backBufferStride.Value) + num6;
				byte* ptr = (byte*)this._backBuffer.ToPointer();
				ptr += num7;
				uint outputBufferSize = this._backBufferSize - num7;
				byte* ptr2 = (byte*)sourceBuffer.ToPointer();
				ptr2 += num5;
				uint inputBufferSize = (uint)sourceBufferSize - num5;
				this.Lock();
				MILUtilities.MILCopyPixelBuffer(ptr, outputBufferSize, (uint)this._backBufferStride.Value, outputBufferOffsetInBits, ptr2, inputBufferSize, (uint)sourceBufferStride, inputBufferOffsetInBits, (uint)sourceRect.Height, copyWidthInBits);
				this.AddDirtyRect(dirtyRect);
				this.Unlock();
			}
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x00114530 File Offset: 0x00113930
		[SecurityCritical]
		private bool AcquireBackBuffer(TimeSpan timeout, bool waitForCopy)
		{
			bool result = false;
			if (this._pBackBuffer == null)
			{
				bool flag = true;
				if (waitForCopy)
				{
					flag = this._copyCompletedEvent.WaitOne(timeout, false);
				}
				if (flag)
				{
					MILSwDoubleBufferedBitmap.GetBackBuffer(this._pDoubleBufferedBitmap, out this._pBackBuffer, out this._backBufferSize);
					this._syncObject = (base.WicSourceHandle = this._pBackBuffer);
					result = true;
				}
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06004681 RID: 18049 RVA: 0x00114590 File Offset: 0x00113990
		[SecurityCritical]
		private void CopyCommon(WriteableBitmap sourceBitmap)
		{
			base.Animatable_IsResourceInvalidationNecessary = false;
			this._actLikeSimpleBitmap = false;
			this.InitFromBitmapSource(sourceBitmap);
			base.Animatable_IsResourceInvalidationNecessary = true;
		}

		// Token: 0x06004682 RID: 18050 RVA: 0x001145BC File Offset: 0x001139BC
		private void BeginInit()
		{
			this._bitmapInit.BeginInit();
		}

		// Token: 0x06004683 RID: 18051 RVA: 0x001145D4 File Offset: 0x001139D4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void EndInit()
		{
			this._bitmapInit.EndInit();
			this.FinalizeCreation();
		}

		// Token: 0x06004684 RID: 18052 RVA: 0x001145F4 File Offset: 0x001139F4
		[SecurityCritical]
		internal override void FinalizeCreation()
		{
			base.IsSourceCached = true;
			base.CreationCompleted = true;
			this.UpdateCachedSettings();
		}

		// Token: 0x06004685 RID: 18053 RVA: 0x00114618 File Offset: 0x00113A18
		[SecurityCritical]
		private void ValidateArrayAndGetInfo(Array sourceBuffer, bool backwardsCompat, out int elementSize, out int sourceBufferSize, out Type elementType)
		{
			if (sourceBuffer == null)
			{
				throw new ArgumentNullException(backwardsCompat ? "pixels" : "sourceBuffer");
			}
			checked
			{
				if (sourceBuffer.Rank == 1)
				{
					if (sourceBuffer.GetLength(0) > 0)
					{
						object value = sourceBuffer.GetValue(0);
						elementSize = Marshal.SizeOf(value);
						sourceBufferSize = sourceBuffer.GetLength(0) * elementSize;
						elementType = value.GetType();
						return;
					}
					if (backwardsCompat)
					{
						elementSize = 1;
						sourceBufferSize = 0;
						elementType = null;
						return;
					}
					throw new ArgumentException(SR.Get("Image_InsufficientBuffer"), "sourceBuffer");
				}
				else
				{
					if (sourceBuffer.Rank != 2)
					{
						throw new ArgumentException(SR.Get("Collection_BadRank"), backwardsCompat ? "pixels" : "sourceBuffer");
					}
					if (sourceBuffer.GetLength(0) > 0 && sourceBuffer.GetLength(1) > 0)
					{
						object value2 = sourceBuffer.GetValue(0, 0);
						elementSize = Marshal.SizeOf(value2);
						sourceBufferSize = sourceBuffer.GetLength(0) * sourceBuffer.GetLength(1) * elementSize;
						elementType = value2.GetType();
						return;
					}
					if (backwardsCompat)
					{
						elementSize = 1;
						sourceBufferSize = 0;
						elementType = null;
						return;
					}
					throw new ArgumentException(SR.Get("Image_InsufficientBuffer"), "sourceBuffer");
				}
			}
		}

		// Token: 0x06004686 RID: 18054 RVA: 0x0011472C File Offset: 0x00113B2C
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._actLikeSimpleBitmap)
			{
				return base.AddRefOnChannelCore(channel);
			}
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_DOUBLEBUFFEREDBITMAP))
			{
				if (!channel.IsSynchronous && this._hasDirtyRects)
				{
					this.SubscribeToCommittingBatch();
				}
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06004687 RID: 18055 RVA: 0x0011478C File Offset: 0x00113B8C
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				if (!channel.IsSynchronous)
				{
					this.UnsubscribeFromCommittingBatch();
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x06004688 RID: 18056 RVA: 0x001147BC File Offset: 0x00113BBC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateBitmapSourceResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (this._actLikeSimpleBitmap)
			{
				base.UpdateBitmapSourceResource(channel, skipOnChannelCheck);
				return;
			}
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				DUCE.MILCMD_DOUBLEBUFFEREDBITMAP milcmd_DOUBLEBUFFEREDBITMAP;
				milcmd_DOUBLEBUFFEREDBITMAP.Type = MILCMD.MilCmdDoubleBufferedBitmap;
				milcmd_DOUBLEBUFFEREDBITMAP.Handle = this._duceResource.GetHandle(channel);
				milcmd_DOUBLEBUFFEREDBITMAP.SwDoubleBufferedBitmap = this._pDoubleBufferedBitmap.DangerousGetHandle().ToPointer();
				milcmd_DOUBLEBUFFEREDBITMAP.UseBackBuffer = (channel.IsSynchronous ? 1U : 0U);
				UnsafeNativeMethods.MILUnknown.AddRef(this._pDoubleBufferedBitmap);
				channel.SendCommand((byte*)(&milcmd_DOUBLEBUFFEREDBITMAP), sizeof(DUCE.MILCMD_DOUBLEBUFFEREDBITMAP), false);
			}
		}

		// Token: 0x06004689 RID: 18057 RVA: 0x00114854 File Offset: 0x00113C54
		private void SubscribeToCommittingBatch()
		{
			if (!this._isWaitingForCommit)
			{
				MediaContext mediaContext = MediaContext.From(base.Dispatcher);
				if (this._duceResource.IsOnChannel(mediaContext.Channel))
				{
					mediaContext.CommittingBatch += this.CommittingBatchHandler;
					this._isWaitingForCommit = true;
				}
			}
		}

		// Token: 0x0600468A RID: 18058 RVA: 0x0011489C File Offset: 0x00113C9C
		private void UnsubscribeFromCommittingBatch()
		{
			if (this._isWaitingForCommit)
			{
				MediaContext mediaContext = MediaContext.From(base.Dispatcher);
				mediaContext.CommittingBatch -= this.CommittingBatchHandler;
				this._isWaitingForCommit = false;
			}
		}

		// Token: 0x0600468B RID: 18059 RVA: 0x001148D0 File Offset: 0x00113CD0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private unsafe void OnCommittingBatch(object sender, EventArgs args)
		{
			this.UnsubscribeFromCommittingBatch();
			this._copyCompletedEvent.Reset();
			this._pBackBuffer = null;
			DUCE.Channel channel = sender as DUCE.Channel;
			IntPtr currentProcess = UnsafeNativeMethods.GetCurrentProcess();
			IntPtr intPtr;
			if (!UnsafeNativeMethods.DuplicateHandle(currentProcess, this._copyCompletedEvent.SafeWaitHandle, currentProcess, out intPtr, 0U, false, 2U))
			{
				throw new Win32Exception();
			}
			DUCE.MILCMD_DOUBLEBUFFEREDBITMAP_COPYFORWARD milcmd_DOUBLEBUFFEREDBITMAP_COPYFORWARD;
			milcmd_DOUBLEBUFFEREDBITMAP_COPYFORWARD.Type = MILCMD.MilCmdDoubleBufferedBitmapCopyForward;
			milcmd_DOUBLEBUFFEREDBITMAP_COPYFORWARD.Handle = this._duceResource.GetHandle(channel);
			milcmd_DOUBLEBUFFEREDBITMAP_COPYFORWARD.CopyCompletedEvent = (ulong)intPtr.ToInt64();
			channel.SendCommand((byte*)(&milcmd_DOUBLEBUFFEREDBITMAP_COPYFORWARD), sizeof(DUCE.MILCMD_DOUBLEBUFFEREDBITMAP_COPYFORWARD));
			channel.CloseBatch();
			this._hasDirtyRects = false;
		}

		/// <summary>Obtém um ponteiro para o buffer de fundo.</summary>
		/// <returns>Um <see cref="T:System.IntPtr" /> que aponta para o endereço básico do buffer de fundo.</returns>
		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x0600468C RID: 18060 RVA: 0x00114968 File Offset: 0x00113D68
		// (set) Token: 0x0600468D RID: 18061 RVA: 0x00114988 File Offset: 0x00113D88
		public IntPtr BackBuffer
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandUnmanagedCode();
				base.ReadPreamble();
				return this._backBuffer;
			}
			[SecurityCritical]
			private set
			{
				this._backBuffer = value;
			}
		}

		/// <summary>Obtém um valor que indica o número de bytes em uma única linha de dados de pixel.</summary>
		/// <returns>Um inteiro que indica o número de bytes em uma única linha de dados de pixel.</returns>
		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x0600468E RID: 18062 RVA: 0x0011499C File Offset: 0x00113D9C
		public int BackBufferStride
		{
			get
			{
				base.ReadPreamble();
				return this._backBufferStride.Value;
			}
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x0600468F RID: 18063 RVA: 0x001149BC File Offset: 0x00113DBC
		private EventHandler CommittingBatchHandler
		{
			get
			{
				if (this._committingBatchHandler == null)
				{
					this._committingBatchHandler = new EventHandler(this.OnCommittingBatch);
				}
				return this._committingBatchHandler;
			}
		}

		// Token: 0x040019A7 RID: 6567
		[SecurityCritical]
		private IntPtr _backBuffer;

		// Token: 0x040019A8 RID: 6568
		[SecurityCritical]
		private uint _backBufferSize;

		// Token: 0x040019A9 RID: 6569
		private SecurityCriticalDataForSet<int> _backBufferStride;

		// Token: 0x040019AA RID: 6570
		[SecurityCritical]
		private SafeMILHandle _pDoubleBufferedBitmap;

		// Token: 0x040019AB RID: 6571
		[SecurityCritical]
		private SafeMILHandle _pBackBufferLock;

		// Token: 0x040019AC RID: 6572
		[SecurityCritical]
		private BitmapSourceSafeMILHandle _pBackBuffer;

		// Token: 0x040019AD RID: 6573
		private uint _lockCount;

		// Token: 0x040019AE RID: 6574
		private bool _hasDirtyRects = true;

		// Token: 0x040019AF RID: 6575
		private bool _isWaitingForCommit;

		// Token: 0x040019B0 RID: 6576
		private ManualResetEvent _copyCompletedEvent = new ManualResetEvent(true);

		// Token: 0x040019B1 RID: 6577
		private EventHandler _committingBatchHandler;

		// Token: 0x040019B2 RID: 6578
		private bool _actLikeSimpleBitmap;
	}
}
