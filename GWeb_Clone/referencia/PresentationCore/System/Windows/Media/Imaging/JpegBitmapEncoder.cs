using System;
using System.Security;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Define um codificador que é usado para codificar imagens em formato JPEG (Joint Photographics Experts Group).</summary>
	// Token: 0x020005F3 RID: 1523
	public sealed class JpegBitmapEncoder : BitmapEncoder
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.JpegBitmapEncoder" />.</summary>
		// Token: 0x060045CD RID: 17869 RVA: 0x00110238 File Offset: 0x0010F638
		[SecurityCritical]
		public JpegBitmapEncoder() : base(true)
		{
			this._supportsPreview = false;
			this._supportsGlobalThumbnail = false;
			this._supportsGlobalMetadata = false;
			this._supportsFrameThumbnails = true;
			this._supportsMultipleFrames = false;
			this._supportsFrameMetadata = true;
		}

		/// <summary>Obtém ou define um valor que indica o nível de qualidade da imagem JPEG (Joint Photographics Experts Group) resultante.</summary>
		/// <returns>O nível de qualidade da imagem JPEG. O intervalo de valor é 1 (qualidade mais baixa) a 100 (qualidade mais alta), inclusive.</returns>
		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x060045CE RID: 17870 RVA: 0x0011028C File Offset: 0x0010F68C
		// (set) Token: 0x060045CF RID: 17871 RVA: 0x001102A0 File Offset: 0x0010F6A0
		public int QualityLevel
		{
			get
			{
				return this._qualityLevel;
			}
			set
			{
				if (value < 1 || value > 100)
				{
					throw new ArgumentOutOfRangeException("value", SR.Get("ParameterMustBeBetween", new object[]
					{
						1,
						100
					}));
				}
				this._qualityLevel = value;
			}
		}

		/// <summary>Obtém ou define um valor que representa o grau com o qual uma imagem JPEG (Joint Photographics Experts Group) é girada.</summary>
		/// <returns>O grau com o qual a imagem é girada.</returns>
		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x060045D0 RID: 17872 RVA: 0x001102EC File Offset: 0x0010F6EC
		// (set) Token: 0x060045D1 RID: 17873 RVA: 0x00110318 File Offset: 0x0010F718
		public Rotation Rotation
		{
			get
			{
				if (this.Rotate90)
				{
					return Rotation.Rotate90;
				}
				if (this.Rotate180)
				{
					return Rotation.Rotate180;
				}
				if (this.Rotate270)
				{
					return Rotation.Rotate270;
				}
				return Rotation.Rotate0;
			}
			set
			{
				this.Rotate90 = false;
				this.Rotate180 = false;
				this.Rotate270 = false;
				switch (value)
				{
				case Rotation.Rotate0:
					break;
				case Rotation.Rotate90:
					this.Rotate90 = true;
					return;
				case Rotation.Rotate180:
					this.Rotate180 = true;
					return;
				case Rotation.Rotate270:
					this.Rotate270 = true;
					break;
				default:
					return;
				}
			}
		}

		/// <summary>Obtém ou define um valor que indica se uma imagem JPEG (Joint Photographics Experts Group) deve ser invertida horizontalmente durante a codificação.</summary>
		/// <returns>
		///   <see langword="true" /> Se a imagem é invertida horizontalmente durante a codificação; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x060045D2 RID: 17874 RVA: 0x00110368 File Offset: 0x0010F768
		// (set) Token: 0x060045D3 RID: 17875 RVA: 0x00110384 File Offset: 0x0010F784
		public bool FlipHorizontal
		{
			get
			{
				return Convert.ToBoolean((int)(this._transformation & WICBitmapTransformOptions.WICBitmapTransformFlipHorizontal));
			}
			set
			{
				if (value != this.FlipHorizontal)
				{
					if (value)
					{
						this._transformation |= WICBitmapTransformOptions.WICBitmapTransformFlipHorizontal;
						return;
					}
					this._transformation &= (WICBitmapTransformOptions)(-9);
				}
			}
		}

		/// <summary>Obtém ou define um valor que indica se uma imagem JPEG (Joint Photographics Experts Group) deve ser invertida verticalmente durante a codificação.</summary>
		/// <returns>
		///   <see langword="true" /> Se a imagem é invertida verticalmente durante a codificação; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x060045D4 RID: 17876 RVA: 0x001103BC File Offset: 0x0010F7BC
		// (set) Token: 0x060045D5 RID: 17877 RVA: 0x001103D8 File Offset: 0x0010F7D8
		public bool FlipVertical
		{
			get
			{
				return Convert.ToBoolean((int)(this._transformation & WICBitmapTransformOptions.WICBitmapTransformFlipVertical));
			}
			set
			{
				if (value != this.FlipVertical)
				{
					if (value)
					{
						this._transformation |= WICBitmapTransformOptions.WICBitmapTransformFlipVertical;
						return;
					}
					this._transformation &= (WICBitmapTransformOptions)(-17);
				}
			}
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x060045D6 RID: 17878 RVA: 0x00110410 File Offset: 0x0010F810
		internal override Guid ContainerFormat
		{
			[SecurityCritical]
			get
			{
				return this._containerFormat;
			}
		}

		// Token: 0x060045D7 RID: 17879 RVA: 0x00110424 File Offset: 0x0010F824
		[SecurityCritical]
		internal override void SetupFrame(SafeMILHandle frameEncodeHandle, SafeMILHandle encoderOptions)
		{
			PROPBAG2 propbag = default(PROPBAG2);
			PROPVARIANT propvariant = default(PROPVARIANT);
			if (this._transformation != WICBitmapTransformOptions.WICBitmapTransformRotate0)
			{
				try
				{
					propbag.Init("BitmapTransform");
					propvariant.Init((byte)this._transformation);
					HRESULT.Check(UnsafeNativeMethods.IPropertyBag2.Write(encoderOptions, 1U, ref propbag, ref propvariant));
				}
				finally
				{
					propbag.Clear();
					propvariant.Clear();
				}
			}
			if (this._qualityLevel != 75)
			{
				try
				{
					propbag.Init("ImageQuality");
					propvariant.Init((float)this._qualityLevel / 100f);
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

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x060045D8 RID: 17880 RVA: 0x0011051C File Offset: 0x0010F91C
		// (set) Token: 0x060045D9 RID: 17881 RVA: 0x00110544 File Offset: 0x0010F944
		private bool Rotate90
		{
			get
			{
				return Convert.ToBoolean((int)(this._transformation & WICBitmapTransformOptions.WICBitmapTransformRotate90)) && !this.Rotate270;
			}
			set
			{
				if (value != this.Rotate90)
				{
					bool flipHorizontal = this.FlipHorizontal;
					bool flipVertical = this.FlipVertical;
					if (value)
					{
						this._transformation = WICBitmapTransformOptions.WICBitmapTransformRotate90;
					}
					else
					{
						this._transformation = WICBitmapTransformOptions.WICBitmapTransformRotate0;
					}
					this.FlipHorizontal = flipHorizontal;
					this.FlipVertical = flipVertical;
				}
			}
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x060045DA RID: 17882 RVA: 0x0011058C File Offset: 0x0010F98C
		// (set) Token: 0x060045DB RID: 17883 RVA: 0x001105B4 File Offset: 0x0010F9B4
		private bool Rotate180
		{
			get
			{
				return Convert.ToBoolean((int)(this._transformation & WICBitmapTransformOptions.WICBitmapTransformRotate180)) && !this.Rotate270;
			}
			set
			{
				if (value != this.Rotate180)
				{
					bool flipHorizontal = this.FlipHorizontal;
					bool flipVertical = this.FlipVertical;
					if (value)
					{
						this._transformation = WICBitmapTransformOptions.WICBitmapTransformRotate180;
					}
					else
					{
						this._transformation = WICBitmapTransformOptions.WICBitmapTransformRotate0;
					}
					this.FlipHorizontal = flipHorizontal;
					this.FlipVertical = flipVertical;
				}
			}
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x060045DC RID: 17884 RVA: 0x001105FC File Offset: 0x0010F9FC
		// (set) Token: 0x060045DD RID: 17885 RVA: 0x0011061C File Offset: 0x0010FA1C
		private bool Rotate270
		{
			get
			{
				return Convert.ToBoolean((this._transformation & WICBitmapTransformOptions.WICBitmapTransformRotate270) == WICBitmapTransformOptions.WICBitmapTransformRotate270);
			}
			set
			{
				if (value != this.Rotate270)
				{
					bool flipHorizontal = this.FlipHorizontal;
					bool flipVertical = this.FlipVertical;
					if (value)
					{
						this._transformation = WICBitmapTransformOptions.WICBitmapTransformRotate270;
					}
					else
					{
						this._transformation = WICBitmapTransformOptions.WICBitmapTransformRotate0;
					}
					this.FlipHorizontal = flipHorizontal;
					this.FlipVertical = flipVertical;
				}
			}
		}

		// Token: 0x060045DE RID: 17886 RVA: 0x00110664 File Offset: 0x0010FA64
		internal override void SealObject()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400194F RID: 6479
		[SecurityCritical]
		private Guid _containerFormat = MILGuidData.GUID_ContainerFormatJpeg;

		// Token: 0x04001950 RID: 6480
		private const int c_defaultQualityLevel = 75;

		// Token: 0x04001951 RID: 6481
		private int _qualityLevel = 75;

		// Token: 0x04001952 RID: 6482
		private const WICBitmapTransformOptions c_defaultTransformation = WICBitmapTransformOptions.WICBitmapTransformRotate0;

		// Token: 0x04001953 RID: 6483
		private WICBitmapTransformOptions _transformation;
	}
}
