using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Codifica uma coleção de objetos <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> em um fluxo de imagem.</summary>
	// Token: 0x020005D6 RID: 1494
	public abstract class BitmapEncoder : DispatcherObject
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.BitmapEncoder" />.</summary>
		// Token: 0x0600439A RID: 17306 RVA: 0x00107194 File Offset: 0x00106594
		protected BitmapEncoder()
		{
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x001071E4 File Offset: 0x001065E4
		internal BitmapEncoder(bool isBuiltIn)
		{
			this._isBuiltIn = isBuiltIn;
		}

		/// <summary>Cria um <see cref="T:System.Windows.Media.Imaging.BitmapEncoder" /> de um <see cref="T:System.Guid" /> que identifica o formato de bitmap desejado.</summary>
		/// <param name="containerFormat">Identifica o formato de codificação de bitmap desejado.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.Imaging.BitmapEncoder" /> que pode codificar para o <paramref name="containerFormat" /> especificado.</returns>
		/// <exception cref="T:System.ArgumentException">O <paramref name="containerFormat" /> é <see langword="Empty" />.</exception>
		// Token: 0x0600439C RID: 17308 RVA: 0x0010723C File Offset: 0x0010663C
		[SecurityCritical]
		public static BitmapEncoder Create(Guid containerFormat)
		{
			if (containerFormat == Guid.Empty)
			{
				throw new ArgumentException(SR.Get("Image_GuidEmpty", new object[]
				{
					"containerFormat"
				}), "containerFormat");
			}
			if (containerFormat == MILGuidData.GUID_ContainerFormatBmp)
			{
				return new BmpBitmapEncoder();
			}
			if (containerFormat == MILGuidData.GUID_ContainerFormatGif)
			{
				return new GifBitmapEncoder();
			}
			if (containerFormat == MILGuidData.GUID_ContainerFormatJpeg)
			{
				return new JpegBitmapEncoder();
			}
			if (containerFormat == MILGuidData.GUID_ContainerFormatPng)
			{
				return new PngBitmapEncoder();
			}
			if (containerFormat == MILGuidData.GUID_ContainerFormatTiff)
			{
				return new TiffBitmapEncoder();
			}
			if (containerFormat == MILGuidData.GUID_ContainerFormatWmp)
			{
				return new WmpBitmapEncoder();
			}
			return new UnknownBitmapEncoder(containerFormat);
		}

		/// <summary>Obtém ou define um valor que representa o perfil de cor associado ao codificador.</summary>
		/// <returns>A coleção de <see cref="T:System.Windows.Media.ColorContext" /> objetos que representa os perfis de cor que este codificador usa.</returns>
		/// <exception cref="T:System.InvalidOperationException">O codificador não dá suporte a perfis de cor.</exception>
		/// <exception cref="T:System.ArgumentNullException">O valor <see cref="T:System.Windows.Media.ColorContext" /> passado ao codificador é <see langword="null" />.</exception>
		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x0600439D RID: 17309 RVA: 0x001072F4 File Offset: 0x001066F4
		// (set) Token: 0x0600439E RID: 17310 RVA: 0x00107314 File Offset: 0x00106714
		public virtual ReadOnlyCollection<ColorContext> ColorContexts
		{
			get
			{
				base.VerifyAccess();
				this.EnsureBuiltIn();
				return this._readOnlycolorContexts;
			}
			set
			{
				base.VerifyAccess();
				this.EnsureBuiltIn();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!this._supportsColorContext)
				{
					throw new InvalidOperationException(SR.Get("Image_EncoderNoColorContext"));
				}
				this._readOnlycolorContexts = value;
			}
		}

		/// <summary>Obtém ou define um <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que representa a miniatura incorporada global.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que representa a miniatura do bitmap.</returns>
		/// <exception cref="T:System.InvalidOperationException">O bitmap não oferece suporte a miniaturas.</exception>
		/// <exception cref="T:System.ArgumentNullException">O valor é definido como <see langword="null" />.</exception>
		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x0600439F RID: 17311 RVA: 0x0010735C File Offset: 0x0010675C
		// (set) Token: 0x060043A0 RID: 17312 RVA: 0x0010737C File Offset: 0x0010677C
		public virtual BitmapSource Thumbnail
		{
			get
			{
				base.VerifyAccess();
				this.EnsureBuiltIn();
				return this._thumbnail;
			}
			set
			{
				base.VerifyAccess();
				this.EnsureBuiltIn();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!this._supportsGlobalThumbnail)
				{
					throw new InvalidOperationException(SR.Get("Image_EncoderNoGlobalThumbnail"));
				}
				this._thumbnail = value;
			}
		}

		/// <summary>Obtém ou define os metadados que serão associados esse bitmap durante a codificação.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">O codificador não dá suporte a metadados globais.</exception>
		/// <exception cref="T:System.ArgumentNullException">É feita uma tentativa para gravar metadados em um formato incompatível.</exception>
		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x060043A1 RID: 17313 RVA: 0x001073C4 File Offset: 0x001067C4
		// (set) Token: 0x060043A2 RID: 17314 RVA: 0x001073EC File Offset: 0x001067EC
		public virtual BitmapMetadata Metadata
		{
			get
			{
				base.VerifyAccess();
				this.EnsureBuiltIn();
				this.EnsureMetadata(true);
				return this._metadata;
			}
			set
			{
				base.VerifyAccess();
				this.EnsureBuiltIn();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.GuidFormat != this.ContainerFormat)
				{
					throw new InvalidOperationException(SR.Get("Image_MetadataNotCompatible"));
				}
				if (!this._supportsGlobalMetadata)
				{
					throw new InvalidOperationException(SR.Get("Image_EncoderNoGlobalMetadata"));
				}
				this._metadata = value;
			}
		}

		/// <summary>Obtém ou define um <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que representa a versão prévia global de um bitmap, se houver.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> que representa a visualização de um bitmap.</returns>
		/// <exception cref="T:System.InvalidOperationException">O bitmap não oferece suporte à versão prévia.</exception>
		/// <exception cref="T:System.ArgumentNullException">O valor é definido como <see langword="null" />.</exception>
		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x060043A3 RID: 17315 RVA: 0x00107458 File Offset: 0x00106858
		// (set) Token: 0x060043A4 RID: 17316 RVA: 0x00107478 File Offset: 0x00106878
		public virtual BitmapSource Preview
		{
			get
			{
				base.VerifyAccess();
				this.EnsureBuiltIn();
				return this._preview;
			}
			set
			{
				base.VerifyAccess();
				this.EnsureBuiltIn();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!this._supportsPreview)
				{
					throw new InvalidOperationException(SR.Get("Image_EncoderNoPreview"));
				}
				this._preview = value;
			}
		}

		/// <summary>Obtém informações que descrevem esse codec.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapCodecInfo" />.</returns>
		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x060043A5 RID: 17317 RVA: 0x001074C0 File Offset: 0x001068C0
		public virtual BitmapCodecInfo CodecInfo
		{
			[SecurityCritical]
			get
			{
				base.VerifyAccess();
				this.EnsureBuiltIn();
				this.EnsureUnmanagedEncoder();
				if (this._codecInfo == null)
				{
					SafeMILHandle codecInfoHandle = new SafeMILHandle();
					HRESULT.Check(UnsafeNativeMethods.WICBitmapEncoder.GetEncoderInfo(this._encoderHandle, out codecInfoHandle));
					this._codecInfo = new BitmapCodecInfoInternal(codecInfoHandle);
				}
				return this._codecInfo;
			}
		}

		/// <summary>Obtém ou define um valor que representa o <see cref="T:System.Windows.Media.Imaging.BitmapPalette" /> de um bitmap codificado.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapPalette" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">O valor <see cref="T:System.Windows.Media.Imaging.BitmapPalette" /> passado ao codificador é <see langword="null" />.</exception>
		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x060043A6 RID: 17318 RVA: 0x00107514 File Offset: 0x00106914
		// (set) Token: 0x060043A7 RID: 17319 RVA: 0x00107534 File Offset: 0x00106934
		public virtual BitmapPalette Palette
		{
			get
			{
				base.VerifyAccess();
				this.EnsureBuiltIn();
				return this._palette;
			}
			set
			{
				base.VerifyAccess();
				this.EnsureBuiltIn();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._palette = value;
			}
		}

		/// <summary>Obtém ou define os quadros individuais dentro de uma imagem.</summary>
		/// <returns>Uma coleção de objetos <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> dentro da imagem.</returns>
		/// <exception cref="T:System.ArgumentNullException">O valor <see cref="T:System.Windows.Media.Imaging.BitmapFrame" /> passado ao codificador é <see langword="null" />.</exception>
		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x060043A8 RID: 17320 RVA: 0x00107564 File Offset: 0x00106964
		// (set) Token: 0x060043A9 RID: 17321 RVA: 0x00107598 File Offset: 0x00106998
		public virtual IList<BitmapFrame> Frames
		{
			get
			{
				base.VerifyAccess();
				this.EnsureBuiltIn();
				if (this._frames == null)
				{
					this._frames = new List<BitmapFrame>(0);
				}
				return this._frames;
			}
			set
			{
				base.VerifyAccess();
				this.EnsureBuiltIn();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._frames = value;
			}
		}

		/// <summary>Codifica uma imagem de bitmap para um <see cref="T:System.IO.Stream" /> especificado.</summary>
		/// <param name="stream">Identifica o fluxo de arquivos ao qual esse bitmap é codificado.</param>
		/// <exception cref="T:System.InvalidOperationException">O bitmap já foi codificado.</exception>
		/// <exception cref="T:System.NotSupportedException">A contagem <see cref="P:System.Windows.Media.Imaging.BitmapEncoder.Frames" /> é menor ou igual a zero.</exception>
		// Token: 0x060043AA RID: 17322 RVA: 0x001075C8 File Offset: 0x001069C8
		[SecurityCritical]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public virtual void Save(Stream stream)
		{
			base.VerifyAccess();
			this.EnsureBuiltIn();
			this.EnsureUnmanagedEncoder();
			BitmapEncoder.EncodeState encodeState = this._encodeState;
			if (this._hasSaved)
			{
				throw new InvalidOperationException(SR.Get("Image_OnlyOneSave"));
			}
			if (this._frames == null)
			{
				throw new NotSupportedException(SR.Get("Image_NoFrames", null));
			}
			int count = this._frames.Count;
			if (count <= 0)
			{
				throw new NotSupportedException(SR.Get("Image_NoFrames", null));
			}
			IntPtr pStream = IntPtr.Zero;
			SafeMILHandle encoderHandle = this._encoderHandle;
			try
			{
				pStream = StreamAsIStream.IStreamFrom(stream);
				HRESULT.Check(UnsafeNativeMethods.WICBitmapEncoder.Initialize(encoderHandle, pStream, WICBitmapEncodeCacheOption.WICBitmapEncodeNoCache));
				this._encodeState = BitmapEncoder.EncodeState.EncoderInitialized;
				if (this._thumbnail != null)
				{
					SafeMILHandle wicSourceHandle = this._thumbnail.WicSourceHandle;
					object syncObject = this._thumbnail.SyncObject;
					lock (syncObject)
					{
						HRESULT.Check(UnsafeNativeMethods.WICBitmapEncoder.SetThumbnail(encoderHandle, wicSourceHandle));
						this._encodeState = BitmapEncoder.EncodeState.EncoderThumbnailSet;
					}
				}
				if (this._palette != null && this._palette.Colors.Count > 0)
				{
					SafeMILHandle internalPalette = this._palette.InternalPalette;
					HRESULT.Check(UnsafeNativeMethods.WICBitmapEncoder.SetPalette(encoderHandle, internalPalette));
					this._encodeState = BitmapEncoder.EncodeState.EncoderPaletteSet;
				}
				if (this._metadata != null && this._metadata.GuidFormat == this.ContainerFormat)
				{
					this.EnsureMetadata(false);
					if (this._metadata.InternalMetadataHandle != this._metadataHandle)
					{
						PROPVARIANT propvariant = default(PROPVARIANT);
						try
						{
							propvariant.Init(this._metadata);
							object syncObject2 = this._metadata.SyncObject;
							lock (syncObject2)
							{
								HRESULT.Check(UnsafeNativeMethods.WICMetadataQueryWriter.SetMetadataByName(this._metadataHandle, "/", ref propvariant));
							}
						}
						finally
						{
							propvariant.Clear();
						}
					}
				}
				for (int i = 0; i < count; i++)
				{
					SafeMILHandle safeMILHandle = new SafeMILHandle();
					SafeMILHandle encoderOptions = new SafeMILHandle();
					HRESULT.Check(UnsafeNativeMethods.WICBitmapEncoder.CreateNewFrame(encoderHandle, out safeMILHandle, out encoderOptions));
					this._encodeState = BitmapEncoder.EncodeState.EncoderCreatedNewFrame;
					this._frameHandles.Add(safeMILHandle);
					this.SaveFrame(safeMILHandle, encoderOptions, this._frames[i]);
					if (!this._supportsMultipleFrames)
					{
						break;
					}
				}
				HRESULT.Check(UnsafeNativeMethods.WICBitmapEncoder.Commit(encoderHandle));
				this._encodeState = BitmapEncoder.EncodeState.EncoderCommitted;
			}
			finally
			{
				UnsafeNativeMethods.MILUnknown.ReleaseInterface(ref pStream);
			}
			this._hasSaved = true;
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x060043AB RID: 17323 RVA: 0x00107874 File Offset: 0x00106C74
		internal virtual Guid ContainerFormat
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x060043AC RID: 17324 RVA: 0x00107888 File Offset: 0x00106C88
		internal virtual bool IsMetadataFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060043AD RID: 17325 RVA: 0x00107898 File Offset: 0x00106C98
		internal virtual void SetupFrame(SafeMILHandle frameEncodeHandle, SafeMILHandle encoderOptions)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043AE RID: 17326 RVA: 0x001078AC File Offset: 0x00106CAC
		private void EnsureBuiltIn()
		{
			if (!this._isBuiltIn)
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060043AF RID: 17327 RVA: 0x001078C8 File Offset: 0x00106CC8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void EnsureMetadata(bool createBitmapMetadata)
		{
			if (!this._supportsGlobalMetadata)
			{
				return;
			}
			if (this._metadataHandle == null)
			{
				SafeMILHandle metadataHandle = new SafeMILHandle();
				int metadataQueryWriter = UnsafeNativeMethods.WICBitmapEncoder.GetMetadataQueryWriter(this._encoderHandle, out metadataHandle);
				if (metadataQueryWriter == -2003292287)
				{
					this._supportsGlobalMetadata = false;
					return;
				}
				HRESULT.Check(metadataQueryWriter);
				this._metadataHandle = metadataHandle;
			}
			if (createBitmapMetadata && this._metadata == null && this._metadataHandle != null)
			{
				this._metadata = new BitmapMetadata(this._metadataHandle, false, this.IsMetadataFixedSize, this._metadataHandle);
			}
		}

		// Token: 0x060043B0 RID: 17328 RVA: 0x00107948 File Offset: 0x00106D48
		[SecurityCritical]
		private void EnsureUnmanagedEncoder()
		{
			if (this._encoderHandle == null)
			{
				using (FactoryMaker factoryMaker = new FactoryMaker())
				{
					SafeMILHandle encoderHandle = null;
					Guid guid = new Guid(MILGuidData.GUID_VendorMicrosoft);
					Guid containerFormat = this.ContainerFormat;
					HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreateEncoder(factoryMaker.ImagingFactoryPtr, ref containerFormat, ref guid, out encoderHandle));
					this._encoderHandle = encoderHandle;
				}
			}
		}

		// Token: 0x060043B1 RID: 17329 RVA: 0x001079C0 File Offset: 0x00106DC0
		[SecurityCritical]
		private void SaveFrame(SafeMILHandle frameEncodeHandle, SafeMILHandle encoderOptions, BitmapFrame frame)
		{
			this.SetupFrame(frameEncodeHandle, encoderOptions);
			this._encodeState = BitmapEncoder.EncodeState.FrameEncodeInitialized;
			HRESULT.Check(UnsafeNativeMethods.WICBitmapFrameEncode.SetSize(frameEncodeHandle, frame.PixelWidth, frame.PixelHeight));
			this._encodeState = BitmapEncoder.EncodeState.FrameEncodeSizeSet;
			double num = frame.DpiX;
			double num2 = frame.DpiY;
			if (num <= 0.0)
			{
				num = 96.0;
			}
			if (num2 <= 0.0)
			{
				num2 = 96.0;
			}
			HRESULT.Check(UnsafeNativeMethods.WICBitmapFrameEncode.SetResolution(frameEncodeHandle, num, num2));
			this._encodeState = BitmapEncoder.EncodeState.FrameEncodeResolutionSet;
			if (this._supportsFrameThumbnails)
			{
				BitmapSource thumbnail = frame.Thumbnail;
				if (thumbnail != null)
				{
					SafeMILHandle wicSourceHandle = thumbnail.WicSourceHandle;
					object syncObject = thumbnail.SyncObject;
					lock (syncObject)
					{
						HRESULT.Check(UnsafeNativeMethods.WICBitmapFrameEncode.SetThumbnail(frameEncodeHandle, wicSourceHandle));
						this._encodeState = BitmapEncoder.EncodeState.FrameEncodeThumbnailSet;
					}
				}
			}
			if (frame._isColorCorrected)
			{
				ColorContext colorContext = new ColorContext(frame.Format);
				IntPtr[] ppIColorContext = new IntPtr[]
				{
					colorContext.ColorContextHandle.DangerousGetHandle()
				};
				if (UnsafeNativeMethods.WICBitmapFrameEncode.SetColorContexts(frameEncodeHandle, 1U, ppIColorContext) == 0)
				{
					this._encodeState = BitmapEncoder.EncodeState.FrameEncodeColorContextsSet;
				}
			}
			else
			{
				IList<ColorContext> colorContexts = frame.ColorContexts;
				if (colorContexts != null && colorContexts.Count > 0)
				{
					int count = colorContexts.Count;
					IntPtr[] array = new IntPtr[count];
					for (int i = 0; i < count; i++)
					{
						array[i] = colorContexts[i].ColorContextHandle.DangerousGetHandle();
					}
					if (UnsafeNativeMethods.WICBitmapFrameEncode.SetColorContexts(frameEncodeHandle, (uint)count, array) == 0)
					{
						this._encodeState = BitmapEncoder.EncodeState.FrameEncodeColorContextsSet;
					}
				}
			}
			object syncObject2 = frame.SyncObject;
			lock (syncObject2)
			{
				SafeMILHandle safeMILHandle = new SafeMILHandle();
				SafeMILHandle wicSourceHandle2 = frame.WicSourceHandle;
				SafeMILHandle pIPalette = new SafeMILHandle();
				HRESULT.Check(UnsafeNativeMethods.WICCodec.WICSetEncoderFormat(wicSourceHandle2, pIPalette, frameEncodeHandle, out safeMILHandle));
				this._encodeState = BitmapEncoder.EncodeState.FrameEncodeFormatSet;
				this._writeSourceHandles.Add(safeMILHandle);
				if (this._supportsFrameMetadata)
				{
					BitmapMetadata bitmapMetadata = frame.Metadata as BitmapMetadata;
					if (bitmapMetadata != null && bitmapMetadata.GuidFormat == this.ContainerFormat)
					{
						SafeMILHandle this_PTR = new SafeMILHandle();
						HRESULT.Check(UnsafeNativeMethods.WICBitmapFrameEncode.GetMetadataQueryWriter(frameEncodeHandle, out this_PTR));
						PROPVARIANT propvariant = default(PROPVARIANT);
						try
						{
							propvariant.Init(bitmapMetadata);
							object syncObject3 = bitmapMetadata.SyncObject;
							lock (syncObject3)
							{
								HRESULT.Check(UnsafeNativeMethods.WICMetadataQueryWriter.SetMetadataByName(this_PTR, "/", ref propvariant));
								this._encodeState = BitmapEncoder.EncodeState.FrameEncodeMetadataSet;
							}
						}
						finally
						{
							propvariant.Clear();
						}
					}
				}
				Int32Rect int32Rect = default(Int32Rect);
				HRESULT.Check(UnsafeNativeMethods.WICBitmapFrameEncode.WriteSource(frameEncodeHandle, safeMILHandle, ref int32Rect));
				this._encodeState = BitmapEncoder.EncodeState.FrameEncodeSourceWritten;
				HRESULT.Check(UnsafeNativeMethods.WICBitmapFrameEncode.Commit(frameEncodeHandle));
				this._encodeState = BitmapEncoder.EncodeState.FrameEncodeCommitted;
			}
		}

		// Token: 0x060043B2 RID: 17330
		internal abstract void SealObject();

		// Token: 0x040018A7 RID: 6311
		internal bool _supportsPreview = true;

		// Token: 0x040018A8 RID: 6312
		internal bool _supportsGlobalThumbnail = true;

		// Token: 0x040018A9 RID: 6313
		internal bool _supportsGlobalMetadata = true;

		// Token: 0x040018AA RID: 6314
		internal bool _supportsFrameThumbnails = true;

		// Token: 0x040018AB RID: 6315
		internal bool _supportsFrameMetadata = true;

		// Token: 0x040018AC RID: 6316
		internal bool _supportsMultipleFrames;

		// Token: 0x040018AD RID: 6317
		internal bool _supportsColorContext;

		// Token: 0x040018AE RID: 6318
		private bool _isBuiltIn;

		// Token: 0x040018AF RID: 6319
		private SafeMILHandle _encoderHandle;

		// Token: 0x040018B0 RID: 6320
		private BitmapMetadata _metadata;

		// Token: 0x040018B1 RID: 6321
		private SafeMILHandle _metadataHandle;

		// Token: 0x040018B2 RID: 6322
		private ReadOnlyCollection<ColorContext> _readOnlycolorContexts;

		// Token: 0x040018B3 RID: 6323
		private BitmapCodecInfoInternal _codecInfo;

		// Token: 0x040018B4 RID: 6324
		private BitmapSource _thumbnail;

		// Token: 0x040018B5 RID: 6325
		private BitmapSource _preview;

		// Token: 0x040018B6 RID: 6326
		private BitmapPalette _palette;

		// Token: 0x040018B7 RID: 6327
		private IList<BitmapFrame> _frames;

		// Token: 0x040018B8 RID: 6328
		private bool _hasSaved;

		// Token: 0x040018B9 RID: 6329
		private IList<SafeMILHandle> _frameHandles = new List<SafeMILHandle>(0);

		// Token: 0x040018BA RID: 6330
		private IList<SafeMILHandle> _writeSourceHandles = new List<SafeMILHandle>(0);

		// Token: 0x040018BB RID: 6331
		private BitmapEncoder.EncodeState _encodeState;

		// Token: 0x020008D0 RID: 2256
		private enum EncodeState
		{
			// Token: 0x0400297C RID: 10620
			None,
			// Token: 0x0400297D RID: 10621
			EncoderInitialized,
			// Token: 0x0400297E RID: 10622
			EncoderThumbnailSet,
			// Token: 0x0400297F RID: 10623
			EncoderPaletteSet,
			// Token: 0x04002980 RID: 10624
			EncoderCreatedNewFrame,
			// Token: 0x04002981 RID: 10625
			FrameEncodeInitialized,
			// Token: 0x04002982 RID: 10626
			FrameEncodeSizeSet,
			// Token: 0x04002983 RID: 10627
			FrameEncodeResolutionSet,
			// Token: 0x04002984 RID: 10628
			FrameEncodeThumbnailSet,
			// Token: 0x04002985 RID: 10629
			FrameEncodeMetadataSet,
			// Token: 0x04002986 RID: 10630
			FrameEncodeFormatSet,
			// Token: 0x04002987 RID: 10631
			FrameEncodeSourceWritten,
			// Token: 0x04002988 RID: 10632
			FrameEncodeCommitted,
			// Token: 0x04002989 RID: 10633
			EncoderCommitted,
			// Token: 0x0400298A RID: 10634
			FrameEncodeColorContextsSet
		}
	}
}
