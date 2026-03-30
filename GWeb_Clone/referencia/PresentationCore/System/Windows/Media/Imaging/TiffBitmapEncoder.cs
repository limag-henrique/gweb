using System;
using System.Security;
using MS.Internal;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Define um codificador usado para codificar imagens em formato TIFF.</summary>
	// Token: 0x020005FC RID: 1532
	public sealed class TiffBitmapEncoder : BitmapEncoder
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.TiffBitmapEncoder" />.</summary>
		// Token: 0x06004605 RID: 17925 RVA: 0x00111D14 File Offset: 0x00111114
		[SecurityCritical]
		public TiffBitmapEncoder() : base(true)
		{
			this._supportsPreview = false;
			this._supportsGlobalThumbnail = false;
			this._supportsGlobalMetadata = false;
			this._supportsFrameThumbnails = true;
			this._supportsMultipleFrames = true;
			this._supportsFrameMetadata = true;
		}

		/// <summary>Obtém ou define um valor que indica o tipo de compactação usado por esta imagem TIFF (formato TIFF).</summary>
		/// <returns>Um dos valores de <see cref="T:System.Windows.Media.Imaging.TiffCompressOption" />. O padrão é <see cref="F:System.Windows.Media.Imaging.TiffCompressOption.Default" />.</returns>
		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06004606 RID: 17926 RVA: 0x00111D60 File Offset: 0x00111160
		// (set) Token: 0x06004607 RID: 17927 RVA: 0x00111D74 File Offset: 0x00111174
		public TiffCompressOption Compression
		{
			get
			{
				return this._compressionMethod;
			}
			set
			{
				this._compressionMethod = value;
			}
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06004608 RID: 17928 RVA: 0x00111D88 File Offset: 0x00111188
		internal override Guid ContainerFormat
		{
			[SecurityCritical]
			get
			{
				return this._containerFormat;
			}
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06004609 RID: 17929 RVA: 0x00111D9C File Offset: 0x0011119C
		internal override bool IsMetadataFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600460A RID: 17930 RVA: 0x00111DAC File Offset: 0x001111AC
		[SecurityCritical]
		internal override void SetupFrame(SafeMILHandle frameEncodeHandle, SafeMILHandle encoderOptions)
		{
			PROPBAG2 propbag = default(PROPBAG2);
			PROPVARIANT propvariant = default(PROPVARIANT);
			if (this._compressionMethod != TiffCompressOption.Default)
			{
				try
				{
					propbag.Init("TiffCompressionMethod");
					propvariant.Init((byte)this._compressionMethod);
					HRESULT.Check(UnsafeNativeMethods.IPropertyBag2.Write(encoderOptions, 1U, ref propbag, ref propvariant));
				}
				finally
				{
					propbag.Clear();
					propvariant.Clear();
				}
			}
			HRESULT.Check(UnsafeNativeMethods.WICBitmapFrameEncode.Initialize(frameEncodeHandle, encoderOptions));
		}

		// Token: 0x0600460B RID: 17931 RVA: 0x00111E3C File Offset: 0x0011123C
		internal override void SealObject()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400197B RID: 6523
		[SecurityCritical]
		private Guid _containerFormat = MILGuidData.GUID_ContainerFormatTiff;

		// Token: 0x0400197C RID: 6524
		private const TiffCompressOption c_defaultCompressionMethod = TiffCompressOption.Default;

		// Token: 0x0400197D RID: 6525
		private TiffCompressOption _compressionMethod;
	}
}
