using System;
using System.Security;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Define um codificador que é usado para codificar imagens do Microsoft Windows Media Photo.</summary>
	// Token: 0x020005FE RID: 1534
	public sealed class WmpBitmapEncoder : BitmapEncoder
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.WmpBitmapEncoder" />.</summary>
		// Token: 0x06004611 RID: 17937 RVA: 0x00111ED8 File Offset: 0x001112D8
		[SecurityCritical]
		public WmpBitmapEncoder() : base(true)
		{
			this._supportsPreview = false;
			this._supportsGlobalThumbnail = false;
			this._supportsGlobalMetadata = false;
			this._supportsMultipleFrames = false;
		}

		/// <summary>Obtém ou define o nível de qualidade de imagem.</summary>
		/// <returns>O nível de qualidade de imagem. O intervalo é de 0 a 1,0 (qualidade de imagem sem perdas). O padrão é 0,9.</returns>
		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06004612 RID: 17938 RVA: 0x00111F4C File Offset: 0x0011134C
		// (set) Token: 0x06004613 RID: 17939 RVA: 0x00111F60 File Offset: 0x00111360
		public float ImageQualityLevel
		{
			get
			{
				return this._imagequalitylevel;
			}
			set
			{
				if ((double)value < 0.0 || (double)value > 1.0)
				{
					throw new ArgumentOutOfRangeException("value", SR.Get("ParameterMustBeBetween", new object[]
					{
						0.0,
						1.0
					}));
				}
				this._imagequalitylevel = value;
			}
		}

		/// <summary>Obtém ou define um valor que indica se é necessário codificar usando a compactação sem perdas.</summary>
		/// <returns>
		///   <see langword="true" /> Para usar a compactação sem perdas; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06004614 RID: 17940 RVA: 0x00111FCC File Offset: 0x001113CC
		// (set) Token: 0x06004615 RID: 17941 RVA: 0x00111FE0 File Offset: 0x001113E0
		public bool Lossless
		{
			get
			{
				return this._lossless;
			}
			set
			{
				this._lossless = value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Imaging.Rotation" /> da imagem.</summary>
		/// <returns>A rotação da imagem.</returns>
		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06004616 RID: 17942 RVA: 0x00111FF4 File Offset: 0x001113F4
		// (set) Token: 0x06004617 RID: 17943 RVA: 0x00112020 File Offset: 0x00111420
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

		/// <summary>Obtém ou define um valor que indica se a imagem é invertida horizontalmente.</summary>
		/// <returns>
		///   <see langword="true" /> Se a imagem deve ser invertida horizontalmente; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06004618 RID: 17944 RVA: 0x00112070 File Offset: 0x00111470
		// (set) Token: 0x06004619 RID: 17945 RVA: 0x0011208C File Offset: 0x0011148C
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

		/// <summary>Obtém ou define um valor que indica se é necessário inverter a imagem verticalmente.</summary>
		/// <returns>
		///   <see langword="true" /> Se a imagem deve ser invertida verticalmente; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x0600461A RID: 17946 RVA: 0x001120C4 File Offset: 0x001114C4
		// (set) Token: 0x0600461B RID: 17947 RVA: 0x001120E0 File Offset: 0x001114E0
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

		/// <summary>Obtém ou define um valor que indica as opções de codec devem ser usadas.</summary>
		/// <returns>
		///   <see langword="true" /> Se as opções de codec devem ser usados; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x0600461C RID: 17948 RVA: 0x00112118 File Offset: 0x00111518
		// (set) Token: 0x0600461D RID: 17949 RVA: 0x0011212C File Offset: 0x0011152C
		public bool UseCodecOptions
		{
			get
			{
				return this._usecodecoptions;
			}
			set
			{
				this._usecodecoptions = value;
			}
		}

		/// <summary>Obtém ou define a qualidade de compactação para a imagem principal.</summary>
		/// <returns>A qualidade de compactação para a imagem principal. Um valor de 1 é considerado sem perdas e valores mais altos indicam uma alta taxa de compactação e a qualidade de imagem inferior. O intervalo é de 1 a 255. O padrão é 1.</returns>
		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x0600461E RID: 17950 RVA: 0x00112140 File Offset: 0x00111540
		// (set) Token: 0x0600461F RID: 17951 RVA: 0x00112154 File Offset: 0x00111554
		public byte QualityLevel
		{
			get
			{
				return this._qualitylevel;
			}
			set
			{
				if (value < 1 || value > 255)
				{
					throw new ArgumentOutOfRangeException("value", SR.Get("ParameterMustBeBetween", new object[]
					{
						1,
						255
					}));
				}
				this._qualitylevel = value;
			}
		}

		/// <summary>Obtém ou define o nível de sub amostragem para codificação de imagem RGB.</summary>
		/// <returns>O nível de sub amostragem para codificação de imagem RGB. O intervalo é de 0 a 3. O padrão é 3.  
		///   Valor  
		///
		///   Descrição  
		///
		///   0  
		///
		///   4:0:0 de codificação. Conteúdo de croma é descartado; luminância é preservada.  
		///
		///   1  
		///
		///   4: codificação de 2:0. Resolução de croma é reduzida em 1 e 4 da resolução de luminância.  
		///
		///   2  
		///
		///   4: codificação de 2:2. Resolução de croma é reduzida para 1/2 da resolução de luminância.  
		///
		///   3  
		///
		///   4: codificação 4:4. Resolução de croma é preservada.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">O valor fornecido não está entre 0 e 3.</exception>
		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06004620 RID: 17952 RVA: 0x001121A8 File Offset: 0x001115A8
		// (set) Token: 0x06004621 RID: 17953 RVA: 0x001121BC File Offset: 0x001115BC
		public byte SubsamplingLevel
		{
			get
			{
				return this._subsamplinglevel;
			}
			set
			{
				if (value < 0 || value > 3)
				{
					throw new ArgumentOutOfRangeException("value", SR.Get("ParameterMustBeBetween", new object[]
					{
						0,
						3
					}));
				}
				this._subsamplinglevel = value;
			}
		}

		/// <summary>Obtém ou define o nível de processamento de sobreposição.</summary>
		/// <returns>O nível de processamento de sobreposição. O padrão é 1.  
		///   Valor  
		///
		///   Descrição  
		///
		///   0  
		///
		///   Nenhum processamento de sobreposição está habilitado.  
		///
		///   1  
		///
		///   Um nível de processamento de sobreposição está habilitado. Valores codificados de blocos de 4 x 4 são modificados com base nos valores dos blocos de vizinhos.  
		///
		///   2  
		///
		///   Dois níveis de processamento de sobreposição estão habilitados. O primeiro nível de processamento, além de valores codificados de blocos de 16 x 16 macro são modificados com base nos valores dos vizinhos macroblocos.</returns>
		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06004622 RID: 17954 RVA: 0x00112208 File Offset: 0x00111608
		// (set) Token: 0x06004623 RID: 17955 RVA: 0x0011221C File Offset: 0x0011161C
		public byte OverlapLevel
		{
			get
			{
				return this._overlaplevel;
			}
			set
			{
				if (value < 0 || value > 2)
				{
					throw new ArgumentOutOfRangeException("value", SR.Get("ParameterMustBeBetween", new object[]
					{
						0,
						2
					}));
				}
				this._overlaplevel = value;
			}
		}

		/// <summary>Obtém ou define o número de divisões horizontais a usar durante a codificação de compactação. Uma única divisão cria duas regiões horizontais.</summary>
		/// <returns>O número de divisões horizontais a usar durante a codificação de compactação. O intervalo de valores é de 0 a 4095. O padrão é 0.</returns>
		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06004624 RID: 17956 RVA: 0x00112268 File Offset: 0x00111668
		// (set) Token: 0x06004625 RID: 17957 RVA: 0x0011227C File Offset: 0x0011167C
		public short HorizontalTileSlices
		{
			get
			{
				return this._horizontaltileslices;
			}
			set
			{
				if (value < 0 || value > 4096)
				{
					throw new ArgumentOutOfRangeException("value", SR.Get("ParameterMustBeBetween", new object[]
					{
						0,
						4096
					}));
				}
				this._horizontaltileslices = value;
			}
		}

		/// <summary>Obtém ou define o número de divisões verticais a usar durante a codificação de compactação. Uma única divisão cria duas regiões verticais.</summary>
		/// <returns>O número de divisões verticais a usar durante a codificação de compactação. O intervalo de valores é de 0 a 4095. O padrão é 0.</returns>
		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06004626 RID: 17958 RVA: 0x001122D0 File Offset: 0x001116D0
		// (set) Token: 0x06004627 RID: 17959 RVA: 0x001122E4 File Offset: 0x001116E4
		public short VerticalTileSlices
		{
			get
			{
				return this._verticaltileslices;
			}
			set
			{
				if (value < 0 || value > 4096)
				{
					throw new ArgumentOutOfRangeException("value", SR.Get("ParameterMustBeBetween", new object[]
					{
						0,
						4096
					}));
				}
				this._verticaltileslices = value;
			}
		}

		/// <summary>Obtém ou define um valor que indica se é necessário codificar em ordem de frequência.</summary>
		/// <returns>
		///   <see langword="true" /> para codificar a imagem em ordem de frequência. <see langword="false" /> para codificar a imagem por sua orientação espacial. O padrão é <see langword="true" />.</returns>
		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06004628 RID: 17960 RVA: 0x00112338 File Offset: 0x00111738
		// (set) Token: 0x06004629 RID: 17961 RVA: 0x0011234C File Offset: 0x0011174C
		public bool FrequencyOrder
		{
			get
			{
				return this._frequencyorder;
			}
			set
			{
				this._frequencyorder = value;
			}
		}

		/// <summary>Obtém ou define um valor que indica se é preciso codificar os dados de canal alfa como um canal intercalado adicional.</summary>
		/// <returns>
		///   <see langword="true" /> Se a imagem é codificada com adicionais intercalados canal alfa. <see langword="false" /> se o canal alfa planar for usado. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x0600462A RID: 17962 RVA: 0x00112360 File Offset: 0x00111760
		// (set) Token: 0x0600462B RID: 17963 RVA: 0x00112374 File Offset: 0x00111774
		public bool InterleavedAlpha
		{
			get
			{
				return this._interleavedalpha;
			}
			set
			{
				this._interleavedalpha = value;
			}
		}

		/// <summary>Obtém ou define a qualidade de compactação de um canal alfa planar.</summary>
		/// <returns>A qualidade de compactação para uma imagem de canal alfa planar. Um valor de 1 é considerado sem perdas e cada vez maiores de valores resultam em taxas de compactação maior e menor qualidade de imagem. O intervalo de valores é de 1 a 255. O padrão é 1.</returns>
		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x0600462C RID: 17964 RVA: 0x00112388 File Offset: 0x00111788
		// (set) Token: 0x0600462D RID: 17965 RVA: 0x0011239C File Offset: 0x0011179C
		public byte AlphaQualityLevel
		{
			get
			{
				return this._alphaqualitylevel;
			}
			set
			{
				if (value < 0 || value > 255)
				{
					throw new ArgumentOutOfRangeException("value", SR.Get("ParameterMustBeBetween", new object[]
					{
						0,
						255
					}));
				}
				this._alphaqualitylevel = value;
			}
		}

		/// <summary>Obtém ou define um valor que indica se as operações de domínio compactadas podem ser usadas. Operações de domínio compactadas são operações de transformação feitas sem decodificação dos dados da imagem.</summary>
		/// <returns>
		///   <see langword="true" /> Se as operações de domínio compactadas podem ser usadas; Caso contrário, <see langword="false" />. O padrão é <see langword="true" />.</returns>
		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x0600462E RID: 17966 RVA: 0x001123F0 File Offset: 0x001117F0
		// (set) Token: 0x0600462F RID: 17967 RVA: 0x00112404 File Offset: 0x00111804
		public bool CompressedDomainTranscode
		{
			get
			{
				return this._compresseddomaintranscode;
			}
			set
			{
				this._compresseddomaintranscode = value;
			}
		}

		/// <summary>Obtém ou define o nível de dados de imagem a descartar durante transcodificação de domínio compactado.</summary>
		/// <returns>O nível de dados de imagem a descartar durante uma codificação de domínio compactado da imagem. O intervalo de valores é 0 (nenhum dado descartado) 3 (HighPass e LowPass descartados). O padrão é 1.  
		///   Valor  
		///
		///   Descrição  
		///
		///   0  
		///
		///   Nenhum dado de frequência da imagem é descartado.  
		///
		///   1  
		///
		///   FlexBits são descartadas. A qualidade da imagem da imagem é reduzida sem alterar a resolução efetiva da imagem.  
		///
		///   2  
		///
		///   Faixa de dados de frequência HighPass é descartada. A resolução da imagem efetiva é reduzida por um fator de 4 em ambas as dimensões.  
		///
		///   3  
		///
		///   HighPass e LowPass frequência dados as faixas são descartadas. A resolução da imagem efetiva é reduzida por um fator de 16 em ambas as dimensões.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">O valor fornecido não está entre 0 e 3.</exception>
		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06004630 RID: 17968 RVA: 0x00112418 File Offset: 0x00111818
		// (set) Token: 0x06004631 RID: 17969 RVA: 0x0011242C File Offset: 0x0011182C
		public byte ImageDataDiscardLevel
		{
			get
			{
				return this._imagedatadiscardlevel;
			}
			set
			{
				if (value < 0 || value > 3)
				{
					throw new ArgumentOutOfRangeException("value", SR.Get("ParameterMustBeBetween", new object[]
					{
						0,
						3
					}));
				}
				this._imagedatadiscardlevel = value;
			}
		}

		/// <summary>Obtém ou define o nível de dados de frequência alfa a descartar durante transcodificação de domínio compactado.</summary>
		/// <returns>O nível dos dados alfabéticos a descartar durante a codificação de imagem. O intervalo de valores é 0 (nenhum dado descartado) 4 (canal alfa completamente descartado). O padrão é 1.  
		///   Valor  
		///
		///   Descrição  
		///
		///   0  
		///
		///   Nenhum dado de frequência da imagem é descartado.  
		///
		///   1  
		///
		///   FlexBits são descartadas. A qualidade da imagem da imagem é reduzida sem alterar a resolução efetiva da imagem.  
		///
		///   2  
		///
		///   Faixa de dados de frequência HighPass é descartada. A resolução da imagem efetiva é reduzida por um fator de 4 em ambas as dimensões.  
		///
		///   3  
		///
		///   HighPass e LowPass frequência dados as faixas são descartadas. A resolução da imagem efetiva é reduzida por um fator de 16 em ambas as dimensões.  
		///
		///   4  
		///
		///   O canal alfa completamente é descartado. O formato de pixel é alterado para refletir a remoção do canal alfa.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">O valor fornecido não está entre 0 e 4.</exception>
		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06004632 RID: 17970 RVA: 0x00112478 File Offset: 0x00111878
		// (set) Token: 0x06004633 RID: 17971 RVA: 0x0011248C File Offset: 0x0011188C
		public byte AlphaDataDiscardLevel
		{
			get
			{
				return this._alphadatadiscardlevel;
			}
			set
			{
				if (value < 0 || value > 4)
				{
					throw new ArgumentOutOfRangeException("value", SR.Get("ParameterMustBeBetween", new object[]
					{
						0,
						4
					}));
				}
				this._alphadatadiscardlevel = value;
			}
		}

		/// <summary>Obtém ou define um valor que indica se é necessário ignorar pixels de sobreposição de região em codificação de domínio compactada por sub-região. Este recurso não está implementado no momento.</summary>
		/// <returns>
		///   <see langword="true" /> Se a sobreposição é ignorada; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06004634 RID: 17972 RVA: 0x001124D8 File Offset: 0x001118D8
		// (set) Token: 0x06004635 RID: 17973 RVA: 0x001124EC File Offset: 0x001118EC
		public bool IgnoreOverlap
		{
			get
			{
				return this._ignoreoverlap;
			}
			set
			{
				this._ignoreoverlap = value;
			}
		}

		// Token: 0x06004636 RID: 17974 RVA: 0x00112500 File Offset: 0x00111900
		[SecurityCritical]
		internal override void SetupFrame(SafeMILHandle frameEncodeHandle, SafeMILHandle encoderOptions)
		{
			PROPBAG2 propbag = default(PROPBAG2);
			PROPVARIANT propvariant = default(PROPVARIANT);
			if (this._imagequalitylevel != 0.9f)
			{
				try
				{
					propbag.Init("ImageQuality");
					propvariant.Init(this._imagequalitylevel);
					HRESULT.Check(UnsafeNativeMethods.IPropertyBag2.Write(encoderOptions, 1U, ref propbag, ref propvariant));
				}
				finally
				{
					propbag.Clear();
					propvariant.Clear();
				}
			}
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
			if (this._lossless)
			{
				try
				{
					propbag.Init("Lossless");
					propvariant.Init(this._lossless);
					HRESULT.Check(UnsafeNativeMethods.IPropertyBag2.Write(encoderOptions, 1U, ref propbag, ref propvariant));
				}
				finally
				{
					propbag.Clear();
					propvariant.Clear();
				}
			}
			if (this._usecodecoptions)
			{
				try
				{
					propbag.Init("UseCodecOptions");
					propvariant.Init(this._usecodecoptions);
					HRESULT.Check(UnsafeNativeMethods.IPropertyBag2.Write(encoderOptions, 1U, ref propbag, ref propvariant));
				}
				finally
				{
					propbag.Clear();
					propvariant.Clear();
				}
			}
			if (this._qualitylevel != 10)
			{
				try
				{
					propbag.Init("Quality");
					propvariant.Init(this._qualitylevel);
					HRESULT.Check(UnsafeNativeMethods.IPropertyBag2.Write(encoderOptions, 1U, ref propbag, ref propvariant));
				}
				finally
				{
					propbag.Clear();
					propvariant.Clear();
				}
			}
			if (this._subsamplinglevel != 3)
			{
				try
				{
					propbag.Init("Subsampling");
					propvariant.Init(this._subsamplinglevel);
					HRESULT.Check(UnsafeNativeMethods.IPropertyBag2.Write(encoderOptions, 1U, ref propbag, ref propvariant));
				}
				finally
				{
					propbag.Clear();
					propvariant.Clear();
				}
			}
			if (this._overlaplevel != 1)
			{
				try
				{
					propbag.Init("Overlap");
					propvariant.Init(this._overlaplevel);
					HRESULT.Check(UnsafeNativeMethods.IPropertyBag2.Write(encoderOptions, 1U, ref propbag, ref propvariant));
				}
				finally
				{
					propbag.Clear();
					propvariant.Clear();
				}
			}
			if (this._horizontaltileslices != 0)
			{
				try
				{
					propbag.Init("HorizontalTileSlices");
					propvariant.Init((ushort)this._horizontaltileslices);
					HRESULT.Check(UnsafeNativeMethods.IPropertyBag2.Write(encoderOptions, 1U, ref propbag, ref propvariant));
				}
				finally
				{
					propbag.Clear();
					propvariant.Clear();
				}
			}
			if (this._verticaltileslices != 0)
			{
				try
				{
					propbag.Init("VerticalTileSlices");
					propvariant.Init((ushort)this._verticaltileslices);
					HRESULT.Check(UnsafeNativeMethods.IPropertyBag2.Write(encoderOptions, 1U, ref propbag, ref propvariant));
				}
				finally
				{
					propbag.Clear();
					propvariant.Clear();
				}
			}
			if (!this._frequencyorder)
			{
				try
				{
					propbag.Init("FrequencyOrder");
					propvariant.Init(this._frequencyorder);
					HRESULT.Check(UnsafeNativeMethods.IPropertyBag2.Write(encoderOptions, 1U, ref propbag, ref propvariant));
				}
				finally
				{
					propbag.Clear();
					propvariant.Clear();
				}
			}
			if (this._interleavedalpha)
			{
				try
				{
					propbag.Init("InterleavedAlpha");
					propvariant.Init(this._interleavedalpha);
					HRESULT.Check(UnsafeNativeMethods.IPropertyBag2.Write(encoderOptions, 1U, ref propbag, ref propvariant));
				}
				finally
				{
					propbag.Clear();
					propvariant.Clear();
				}
			}
			if (this._alphaqualitylevel != 1)
			{
				try
				{
					propbag.Init("AlphaQuality");
					propvariant.Init(this._alphaqualitylevel);
					HRESULT.Check(UnsafeNativeMethods.IPropertyBag2.Write(encoderOptions, 1U, ref propbag, ref propvariant));
				}
				finally
				{
					propbag.Clear();
					propvariant.Clear();
				}
			}
			if (!this._compresseddomaintranscode)
			{
				try
				{
					propbag.Init("CompressedDomainTranscode");
					propvariant.Init(this._compresseddomaintranscode);
					HRESULT.Check(UnsafeNativeMethods.IPropertyBag2.Write(encoderOptions, 1U, ref propbag, ref propvariant));
				}
				finally
				{
					propbag.Clear();
					propvariant.Clear();
				}
			}
			if (this._imagedatadiscardlevel != 0)
			{
				try
				{
					propbag.Init("ImageDataDiscard");
					propvariant.Init(this._imagedatadiscardlevel);
					HRESULT.Check(UnsafeNativeMethods.IPropertyBag2.Write(encoderOptions, 1U, ref propbag, ref propvariant));
				}
				finally
				{
					propbag.Clear();
					propvariant.Clear();
				}
			}
			if (this._alphadatadiscardlevel != 0)
			{
				try
				{
					propbag.Init("AlphaDataDiscard");
					propvariant.Init(this._alphadatadiscardlevel);
					HRESULT.Check(UnsafeNativeMethods.IPropertyBag2.Write(encoderOptions, 1U, ref propbag, ref propvariant));
				}
				finally
				{
					propbag.Clear();
					propvariant.Clear();
				}
			}
			if (this._ignoreoverlap)
			{
				try
				{
					propbag.Init("IgnoreOverlap");
					propvariant.Init(this._ignoreoverlap);
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

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06004637 RID: 17975 RVA: 0x00112B2C File Offset: 0x00111F2C
		// (set) Token: 0x06004638 RID: 17976 RVA: 0x00112B54 File Offset: 0x00111F54
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

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06004639 RID: 17977 RVA: 0x00112B9C File Offset: 0x00111F9C
		// (set) Token: 0x0600463A RID: 17978 RVA: 0x00112BC4 File Offset: 0x00111FC4
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

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x0600463B RID: 17979 RVA: 0x00112C0C File Offset: 0x0011200C
		// (set) Token: 0x0600463C RID: 17980 RVA: 0x00112C2C File Offset: 0x0011202C
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

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x0600463D RID: 17981 RVA: 0x00112C74 File Offset: 0x00112074
		internal override Guid ContainerFormat
		{
			[SecurityCritical]
			get
			{
				return this._containerFormat;
			}
		}

		// Token: 0x0600463E RID: 17982 RVA: 0x00112C88 File Offset: 0x00112088
		internal override void SealObject()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400197E RID: 6526
		[SecurityCritical]
		private Guid _containerFormat = MILGuidData.GUID_ContainerFormatWmp;

		// Token: 0x0400197F RID: 6527
		private const bool c_defaultLossless = false;

		// Token: 0x04001980 RID: 6528
		private bool _lossless;

		// Token: 0x04001981 RID: 6529
		private const float c_defaultImageQualityLevel = 0.9f;

		// Token: 0x04001982 RID: 6530
		private float _imagequalitylevel = 0.9f;

		// Token: 0x04001983 RID: 6531
		private const WICBitmapTransformOptions c_defaultTransformation = WICBitmapTransformOptions.WICBitmapTransformRotate0;

		// Token: 0x04001984 RID: 6532
		private WICBitmapTransformOptions _transformation;

		// Token: 0x04001985 RID: 6533
		private const bool c_defaultUseCodecOptions = false;

		// Token: 0x04001986 RID: 6534
		private bool _usecodecoptions;

		// Token: 0x04001987 RID: 6535
		private const byte c_defaultQualityLevel = 10;

		// Token: 0x04001988 RID: 6536
		private byte _qualitylevel = 10;

		// Token: 0x04001989 RID: 6537
		private const byte c_defaultSubsamplingLevel = 3;

		// Token: 0x0400198A RID: 6538
		private byte _subsamplinglevel = 3;

		// Token: 0x0400198B RID: 6539
		private const byte c_defaultOverlapLevel = 1;

		// Token: 0x0400198C RID: 6540
		private byte _overlaplevel = 1;

		// Token: 0x0400198D RID: 6541
		private const short c_defaultHorizontalTileSlices = 0;

		// Token: 0x0400198E RID: 6542
		private short _horizontaltileslices;

		// Token: 0x0400198F RID: 6543
		private const short c_defaultVerticalTileSlices = 0;

		// Token: 0x04001990 RID: 6544
		private short _verticaltileslices;

		// Token: 0x04001991 RID: 6545
		private const bool c_defaultFrequencyOrder = true;

		// Token: 0x04001992 RID: 6546
		private bool _frequencyorder = true;

		// Token: 0x04001993 RID: 6547
		private const bool c_defaultInterleavedAlpha = false;

		// Token: 0x04001994 RID: 6548
		private bool _interleavedalpha;

		// Token: 0x04001995 RID: 6549
		private const byte c_defaultAlphaQualityLevel = 1;

		// Token: 0x04001996 RID: 6550
		private byte _alphaqualitylevel = 1;

		// Token: 0x04001997 RID: 6551
		private const bool c_defaultCompressedDomainTranscode = true;

		// Token: 0x04001998 RID: 6552
		private bool _compresseddomaintranscode = true;

		// Token: 0x04001999 RID: 6553
		private const byte c_defaultImageDataDiscardLevel = 0;

		// Token: 0x0400199A RID: 6554
		private byte _imagedatadiscardlevel;

		// Token: 0x0400199B RID: 6555
		private const byte c_defaultAlphaDataDiscardLevel = 0;

		// Token: 0x0400199C RID: 6556
		private byte _alphadatadiscardlevel;

		// Token: 0x0400199D RID: 6557
		private const bool c_defaultIgnoreOverlap = false;

		// Token: 0x0400199E RID: 6558
		private bool _ignoreoverlap;
	}
}
