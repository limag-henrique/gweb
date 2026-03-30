using System;
using System.Security;
using MS.Internal;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Define um codificador que é usado para codificar imagens em formato PNG (Portable Network Graphics).</summary>
	// Token: 0x020005F6 RID: 1526
	public sealed class PngBitmapEncoder : BitmapEncoder
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.PngBitmapEncoder" />.</summary>
		// Token: 0x060045E4 RID: 17892 RVA: 0x00110700 File Offset: 0x0010FB00
		[SecurityCritical]
		public PngBitmapEncoder() : base(true)
		{
			this._supportsPreview = false;
			this._supportsGlobalThumbnail = false;
			this._supportsGlobalMetadata = false;
			this._supportsFrameThumbnails = false;
			this._supportsMultipleFrames = false;
			this._supportsFrameMetadata = true;
		}

		/// <summary>Obtém ou define um valor que indica se o bitmap PNG (formato PNG) deve ser entrelaçado.</summary>
		/// <returns>Um dos valores de <see cref="P:System.Windows.Media.Imaging.PngBitmapEncoder.Interlace" />. O padrão é <see cref="F:System.Windows.Media.Imaging.PngInterlaceOption.Default" />.</returns>
		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x060045E5 RID: 17893 RVA: 0x0011074C File Offset: 0x0010FB4C
		// (set) Token: 0x060045E6 RID: 17894 RVA: 0x00110760 File Offset: 0x0010FB60
		public PngInterlaceOption Interlace
		{
			get
			{
				return this._interlaceOption;
			}
			set
			{
				this._interlaceOption = value;
			}
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x060045E7 RID: 17895 RVA: 0x00110774 File Offset: 0x0010FB74
		internal override Guid ContainerFormat
		{
			[SecurityCritical]
			get
			{
				return this._containerFormat;
			}
		}

		// Token: 0x060045E8 RID: 17896 RVA: 0x00110788 File Offset: 0x0010FB88
		[SecurityCritical]
		internal override void SetupFrame(SafeMILHandle frameEncodeHandle, SafeMILHandle encoderOptions)
		{
			PROPBAG2 propbag = default(PROPBAG2);
			PROPVARIANT propvariant = default(PROPVARIANT);
			if (this._interlaceOption != PngInterlaceOption.Default)
			{
				try
				{
					propbag.Init("InterlaceOption");
					propvariant.Init(this._interlaceOption == PngInterlaceOption.On);
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

		// Token: 0x060045E9 RID: 17897 RVA: 0x00110818 File Offset: 0x0010FC18
		internal override void SealObject()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001958 RID: 6488
		[SecurityCritical]
		private Guid _containerFormat = MILGuidData.GUID_ContainerFormatPng;

		// Token: 0x04001959 RID: 6489
		private const PngInterlaceOption c_defaultInterlaceOption = PngInterlaceOption.Default;

		// Token: 0x0400195A RID: 6490
		private PngInterlaceOption _interlaceOption;
	}
}
