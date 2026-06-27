using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Define um formato de pixel para imagens e superfícies de pixel.</summary>
	// Token: 0x0200042E RID: 1070
	[TypeConverter(typeof(PixelFormatConverter))]
	[Serializable]
	public struct PixelFormat : IEquatable<PixelFormat>
	{
		// Token: 0x06002BD2 RID: 11218 RVA: 0x000AEC48 File Offset: 0x000AE048
		[SecurityCritical]
		internal unsafe PixelFormat(Guid guidPixelFormat)
		{
			Guid wicpixelFormatDontCare = WICPixelFormatGUIDs.WICPixelFormatDontCare;
			byte* ptr = (byte*)(&guidPixelFormat);
			byte* ptr2 = (byte*)(&wicpixelFormatDontCare);
			int num = 15;
			bool flag = true;
			for (int i = 0; i < num; i++)
			{
				if (ptr[i] != ptr2[i])
				{
					flag = false;
					break;
				}
			}
			if (flag && ptr[num] <= 28)
			{
				this._format = (PixelFormatEnum)ptr[num];
			}
			else
			{
				this._format = PixelFormatEnum.Default;
			}
			this._flags = (PixelFormat.GetPixelFormatFlagsFromEnum(this._format) | PixelFormat.GetPixelFormatFlagsFromGuid(guidPixelFormat));
			this._bitsPerPixel = PixelFormat.GetBitsPerPixelFromEnum(this._format);
			this._guidFormat = new SecurityCriticalDataForSet<Guid>(guidPixelFormat);
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x000AECD8 File Offset: 0x000AE0D8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal PixelFormat(PixelFormatEnum format)
		{
			this._format = format;
			this._flags = PixelFormat.GetPixelFormatFlagsFromEnum(format);
			this._bitsPerPixel = PixelFormat.GetBitsPerPixelFromEnum(format);
			this._guidFormat = new SecurityCriticalDataForSet<Guid>(PixelFormat.GetGuidFromFormat(format));
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x000AED18 File Offset: 0x000AE118
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal PixelFormat(string pixelFormatString)
		{
			if (pixelFormatString == null)
			{
				throw new ArgumentNullException("pixelFormatString");
			}
			string text = pixelFormatString.ToUpper(CultureInfo.InvariantCulture);
			uint num = <PrivateImplementationDetails><PresentationCoreCSharp_netmodule>.ComputeStringHash(text);
			PixelFormatEnum pixelFormatEnum;
			if (num <= 1762844733U)
			{
				if (num <= 574174817U)
				{
					if (num <= 350733258U)
					{
						if (num != 65143215U)
						{
							if (num != 306043868U)
							{
								if (num == 350733258U)
								{
									if (text == "BGRA32")
									{
										pixelFormatEnum = PixelFormatEnum.Bgra32;
										goto IL_4C4;
									}
								}
							}
							else if (text == "PRGBA128FLOAT")
							{
								pixelFormatEnum = PixelFormatEnum.Prgba128Float;
								goto IL_4C4;
							}
						}
						else if (text == "GRAY32FLOAT")
						{
							pixelFormatEnum = PixelFormatEnum.Gray32Float;
							goto IL_4C4;
						}
					}
					else if (num != 423176246U)
					{
						if (num != 523841960U)
						{
							if (num == 574174817U)
							{
								if (text == "INDEXED1")
								{
									pixelFormatEnum = PixelFormatEnum.Indexed1;
									goto IL_4C4;
								}
							}
						}
						else if (text == "INDEXED2")
						{
							pixelFormatEnum = PixelFormatEnum.Indexed2;
							goto IL_4C4;
						}
					}
					else if (text == "INDEXED8")
					{
						pixelFormatEnum = PixelFormatEnum.Indexed8;
						goto IL_4C4;
					}
				}
				else if (num <= 1378349157U)
				{
					if (num != 624507674U)
					{
						if (num != 646699810U)
						{
							if (num == 1378349157U)
							{
								if (text == "RGB128FLOAT")
								{
									pixelFormatEnum = PixelFormatEnum.Rgb128Float;
									goto IL_4C4;
								}
							}
						}
						else if (text == "RGBA128FLOAT")
						{
							pixelFormatEnum = PixelFormatEnum.Rgba128Float;
							goto IL_4C4;
						}
					}
					else if (text == "INDEXED4")
					{
						pixelFormatEnum = PixelFormatEnum.Indexed4;
						goto IL_4C4;
					}
				}
				else if (num <= 1494255734U)
				{
					if (num != 1447314567U)
					{
						if (num == 1494255734U)
						{
							if (text == "BGR565")
							{
								pixelFormatEnum = PixelFormatEnum.Bgr565;
								goto IL_4C4;
							}
						}
					}
					else if (text == "PRGBA64")
					{
						pixelFormatEnum = PixelFormatEnum.Prgba64;
						goto IL_4C4;
					}
				}
				else if (num != 1731590586U)
				{
					if (num == 1762844733U)
					{
						if (text == "BGR555")
						{
							pixelFormatEnum = PixelFormatEnum.Bgr555;
							goto IL_4C4;
						}
					}
				}
				else if (text == "BGR24")
				{
					pixelFormatEnum = PixelFormatEnum.Bgr24;
					goto IL_4C4;
				}
			}
			else if (num <= 3346824533U)
			{
				if (num <= 2368543792U)
				{
					if (num != 1798848157U)
					{
						if (num != 2095186942U)
						{
							if (num == 2368543792U)
							{
								if (text == "PBGRA32")
								{
									pixelFormatEnum = PixelFormatEnum.Pbgra32;
									goto IL_4C4;
								}
							}
						}
						else if (text == "DEFAULT")
						{
							pixelFormatEnum = PixelFormatEnum.Default;
							goto IL_4C4;
						}
					}
					else if (text == "BGR32")
					{
						pixelFormatEnum = PixelFormatEnum.Bgr32;
						goto IL_4C4;
					}
				}
				else if (num <= 2690820613U)
				{
					if (num != 2598479921U)
					{
						if (num == 2690820613U)
						{
							if (text == "GRAY16")
							{
								pixelFormatEnum = PixelFormatEnum.Gray16;
								goto IL_4C4;
							}
						}
					}
					else if (text == "BLACKWHITE")
					{
						pixelFormatEnum = PixelFormatEnum.BlackWhite;
						goto IL_4C4;
					}
				}
				else if (num != 3298921965U)
				{
					if (num == 3346824533U)
					{
						if (text == "RGBA64")
						{
							pixelFormatEnum = PixelFormatEnum.Rgba64;
							goto IL_4C4;
						}
					}
				}
				else if (text == "BGR101010")
				{
					pixelFormatEnum = PixelFormatEnum.Bgr101010;
					goto IL_4C4;
				}
			}
			else if (num <= 3791142272U)
			{
				if (num != 3409171936U)
				{
					if (num != 3543145856U)
					{
						if (num == 3791142272U)
						{
							if (text == "CMYK32")
							{
								pixelFormatEnum = PixelFormatEnum.Cmyk32;
								goto IL_4C4;
							}
						}
					}
					else if (text == "RGB48")
					{
						pixelFormatEnum = PixelFormatEnum.Rgb48;
						goto IL_4C4;
					}
				}
				else if (text == "EXTENDED")
				{
					pixelFormatEnum = PixelFormatEnum.Default;
					goto IL_4C4;
				}
			}
			else if (num <= 3958519558U)
			{
				if (num != 3812573498U)
				{
					if (num == 3958519558U)
					{
						if (text == "GRAY8")
						{
							pixelFormatEnum = PixelFormatEnum.Gray8;
							goto IL_4C4;
						}
					}
				}
				else if (text == "RGB24")
				{
					pixelFormatEnum = PixelFormatEnum.Rgb24;
					goto IL_4C4;
				}
			}
			else if (num != 4059185272U)
			{
				if (num == 4159850986U)
				{
					if (text == "GRAY4")
					{
						pixelFormatEnum = PixelFormatEnum.Gray4;
						goto IL_4C4;
					}
				}
			}
			else if (text == "GRAY2")
			{
				pixelFormatEnum = PixelFormatEnum.Gray2;
				goto IL_4C4;
			}
			throw new ArgumentException(SR.Get("Image_BadPixelFormat", new object[]
			{
				pixelFormatString
			}), "pixelFormatString");
			IL_4C4:
			this._format = pixelFormatEnum;
			this._flags = PixelFormat.GetPixelFormatFlagsFromEnum(pixelFormatEnum);
			this._bitsPerPixel = PixelFormat.GetBitsPerPixelFromEnum(pixelFormatEnum);
			this._guidFormat = new SecurityCriticalDataForSet<Guid>(PixelFormat.GetGuidFromFormat(pixelFormatEnum));
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x000AF21C File Offset: 0x000AE61C
		private static Guid GetGuidFromFormat(PixelFormatEnum format)
		{
			switch (format)
			{
			case PixelFormatEnum.Default:
				return WICPixelFormatGUIDs.WICPixelFormatDontCare;
			case PixelFormatEnum.Indexed1:
				return WICPixelFormatGUIDs.WICPixelFormat1bppIndexed;
			case PixelFormatEnum.Indexed2:
				return WICPixelFormatGUIDs.WICPixelFormat2bppIndexed;
			case PixelFormatEnum.Indexed4:
				return WICPixelFormatGUIDs.WICPixelFormat4bppIndexed;
			case PixelFormatEnum.Indexed8:
				return WICPixelFormatGUIDs.WICPixelFormat8bppIndexed;
			case PixelFormatEnum.BlackWhite:
				return WICPixelFormatGUIDs.WICPixelFormatBlackWhite;
			case PixelFormatEnum.Gray2:
				return WICPixelFormatGUIDs.WICPixelFormat2bppGray;
			case PixelFormatEnum.Gray4:
				return WICPixelFormatGUIDs.WICPixelFormat4bppGray;
			case PixelFormatEnum.Gray8:
				return WICPixelFormatGUIDs.WICPixelFormat8bppGray;
			case PixelFormatEnum.Bgr555:
				return WICPixelFormatGUIDs.WICPixelFormat16bppBGR555;
			case PixelFormatEnum.Bgr565:
				return WICPixelFormatGUIDs.WICPixelFormat16bppBGR565;
			case PixelFormatEnum.Gray16:
				return WICPixelFormatGUIDs.WICPixelFormat16bppGray;
			case PixelFormatEnum.Bgr24:
				return WICPixelFormatGUIDs.WICPixelFormat24bppBGR;
			case PixelFormatEnum.Rgb24:
				return WICPixelFormatGUIDs.WICPixelFormat24bppRGB;
			case PixelFormatEnum.Bgr32:
				return WICPixelFormatGUIDs.WICPixelFormat32bppBGR;
			case PixelFormatEnum.Bgra32:
				return WICPixelFormatGUIDs.WICPixelFormat32bppBGRA;
			case PixelFormatEnum.Pbgra32:
				return WICPixelFormatGUIDs.WICPixelFormat32bppPBGRA;
			case PixelFormatEnum.Gray32Float:
				return WICPixelFormatGUIDs.WICPixelFormat32bppGrayFloat;
			case PixelFormatEnum.Bgr101010:
				return WICPixelFormatGUIDs.WICPixelFormat32bppBGR101010;
			case PixelFormatEnum.Rgb48:
				return WICPixelFormatGUIDs.WICPixelFormat48bppRGB;
			case PixelFormatEnum.Rgba64:
				return WICPixelFormatGUIDs.WICPixelFormat64bppRGBA;
			case PixelFormatEnum.Prgba64:
				return WICPixelFormatGUIDs.WICPixelFormat64bppPRGBA;
			case PixelFormatEnum.Rgba128Float:
				return WICPixelFormatGUIDs.WICPixelFormat128bppRGBAFloat;
			case PixelFormatEnum.Prgba128Float:
				return WICPixelFormatGUIDs.WICPixelFormat128bppPRGBAFloat;
			case PixelFormatEnum.Rgb128Float:
				return WICPixelFormatGUIDs.WICPixelFormat128bppRGBFloat;
			case PixelFormatEnum.Cmyk32:
				return WICPixelFormatGUIDs.WICPixelFormat32bppCMYK;
			}
			throw new ArgumentException(SR.Get("Image_BadPixelFormat", new object[]
			{
				format
			}), "format");
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06002BD6 RID: 11222 RVA: 0x000AF368 File Offset: 0x000AE768
		private PixelFormatFlags FormatFlags
		{
			get
			{
				return this._flags;
			}
		}

		/// <summary>Compara duas instâncias de <see cref="T:System.Windows.Media.PixelFormat" /> quanto à igualdade.</summary>
		/// <param name="left">O primeiro <see cref="T:System.Windows.Media.PixelFormat" /> a ser comparado.</param>
		/// <param name="right">O segundo <see cref="T:System.Windows.Media.PixelFormat" /> a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se os dois objetos <see cref="T:System.Windows.Media.PixelFormat" /> forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002BD7 RID: 11223 RVA: 0x000AF37C File Offset: 0x000AE77C
		public static bool operator ==(PixelFormat left, PixelFormat right)
		{
			return left.Guid == right.Guid;
		}

		/// <summary>Compara a desigualdade de duas instâncias <see cref="T:System.Windows.Media.PixelFormat" />.</summary>
		/// <param name="left">O primeiro <see cref="T:System.Windows.Media.PixelFormat" /> a ser comparado.</param>
		/// <param name="right">O segundo <see cref="T:System.Windows.Media.PixelFormat" /> a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se os objetos <see cref="T:System.Windows.Media.PixelFormat" /> não forem iguais, do contrário, <see langword="false" />.</returns>
		// Token: 0x06002BD8 RID: 11224 RVA: 0x000AF39C File Offset: 0x000AE79C
		public static bool operator !=(PixelFormat left, PixelFormat right)
		{
			return left.Guid != right.Guid;
		}

		/// <summary>Determina se as instâncias <see cref="T:System.Windows.Media.PixelFormat" /> especificadas são consideradas iguais.</summary>
		/// <param name="left">Os primeiros objetos <see cref="T:System.Windows.Media.PixelFormat" /> a serem comparados quanto à igualdade.</param>
		/// <param name="right">O segundo objeto <see cref="T:System.Windows.Media.PixelFormat" /> a ser comparado quanto à igualdade.</param>
		/// <returns>
		///   <see langword="true" /> se os dois parâmetros forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002BD9 RID: 11225 RVA: 0x000AF3BC File Offset: 0x000AE7BC
		public static bool Equals(PixelFormat left, PixelFormat right)
		{
			return left.Guid == right.Guid;
		}

		/// <summary>Determina se o formato de pixel é igual ao <see cref="T:System.Windows.Media.PixelFormat" /> determinado.</summary>
		/// <param name="pixelFormat">O formato de pixel a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se os formatos de pixel forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002BDA RID: 11226 RVA: 0x000AF3DC File Offset: 0x000AE7DC
		public bool Equals(PixelFormat pixelFormat)
		{
			return this == pixelFormat;
		}

		/// <summary>Determina se o objeto especificado é igual ao objeto atual.</summary>
		/// <param name="obj">O Object a ser comparado com o Object atual.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.PixelFormat" /> for igual a <paramref name="obj" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002BDB RID: 11227 RVA: 0x000AF3F8 File Offset: 0x000AE7F8
		public override bool Equals(object obj)
		{
			return obj != null && obj is PixelFormat && this == (PixelFormat)obj;
		}

		/// <summary>Cria um código hash do valor <see cref="P:System.Windows.Media.PixelFormat.Masks" /> deste formato de pixel.</summary>
		/// <returns>O código hash do formato de pixel.</returns>
		// Token: 0x06002BDC RID: 11228 RVA: 0x000AF424 File Offset: 0x000AE824
		public override int GetHashCode()
		{
			return this.Guid.GetHashCode();
		}

		/// <summary>Obtém o número de bpp (bits por pixel) para este <see cref="T:System.Windows.Media.PixelFormat" />.</summary>
		/// <returns>O número de bpp (bits por pixel) para este <see cref="T:System.Windows.Media.PixelFormat" />.</returns>
		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06002BDD RID: 11229 RVA: 0x000AF448 File Offset: 0x000AE848
		public int BitsPerPixel
		{
			get
			{
				return this.InternalBitsPerPixel;
			}
		}

		/// <summary>Obtém uma coleção de máscaras de bits associadas a <see cref="T:System.Windows.Media.PixelFormat" />.</summary>
		/// <returns>A coleção de máscaras de bits e deslocamentos associados a <see cref="T:System.Windows.Media.PixelFormat" />.</returns>
		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002BDE RID: 11230 RVA: 0x000AF45C File Offset: 0x000AE85C
		public unsafe IList<PixelFormatChannelMask> Masks
		{
			[SecurityCritical]
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				IntPtr intPtr = this.CreatePixelFormatInfo();
				uint num = 0U;
				PixelFormatChannelMask[] array = null;
				uint num2 = 0U;
				try
				{
					HRESULT.Check(UnsafeNativeMethods.WICPixelFormatInfo.GetChannelCount(intPtr, out num));
					array = new PixelFormatChannelMask[num];
					for (uint num3 = 0U; num3 < num; num3 += 1U)
					{
						HRESULT.Check(UnsafeNativeMethods.WICPixelFormatInfo.GetChannelMask(intPtr, num3, 0U, null, out num2));
						byte[] array2 = new byte[num2];
						try
						{
							byte[] array3;
							byte* pbMaskBuffer;
							if ((array3 = array2) == null || array3.Length == 0)
							{
								pbMaskBuffer = null;
							}
							else
							{
								pbMaskBuffer = &array3[0];
							}
							HRESULT.Check(UnsafeNativeMethods.WICPixelFormatInfo.GetChannelMask(intPtr, num3, num2, pbMaskBuffer, out num2));
						}
						finally
						{
							byte[] array3 = null;
						}
						array[(int)num3] = new PixelFormatChannelMask(array2);
					}
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						UnsafeNativeMethods.MILUnknown.ReleaseInterface(ref intPtr);
					}
				}
				return new PartialList<PixelFormatChannelMask>(array);
			}
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x000AF548 File Offset: 0x000AE948
		[SecurityCritical]
		internal IntPtr CreatePixelFormatInfo()
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			using (FactoryMaker factoryMaker = new FactoryMaker())
			{
				try
				{
					Guid guid = this.Guid;
					int num = UnsafeNativeMethods.WICImagingFactory.CreateComponentInfo(factoryMaker.ImagingFactoryPtr, ref guid, out zero);
					if (num == -2003292277 || num == -2003292336)
					{
						throw new NotSupportedException(SR.Get("Image_NoPixelFormatFound"));
					}
					HRESULT.Check(num);
					Guid iid_IWICPixelFormatInfo = MILGuidData.IID_IWICPixelFormatInfo;
					HRESULT.Check(UnsafeNativeMethods.MILUnknown.QueryInterface(zero, ref iid_IWICPixelFormatInfo, out zero2));
				}
				finally
				{
					if (zero != IntPtr.Zero)
					{
						UnsafeNativeMethods.MILUnknown.ReleaseInterface(ref zero);
					}
				}
			}
			return zero2;
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002BE0 RID: 11232 RVA: 0x000AF614 File Offset: 0x000AEA14
		internal int InternalBitsPerPixel
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				if (this._bitsPerPixel == 0U)
				{
					uint bitsPerPixel = 0U;
					IntPtr intPtr = this.CreatePixelFormatInfo();
					try
					{
						HRESULT.Check(UnsafeNativeMethods.WICPixelFormatInfo.GetBitsPerPixel(intPtr, out bitsPerPixel));
					}
					finally
					{
						if (intPtr != IntPtr.Zero)
						{
							UnsafeNativeMethods.MILUnknown.ReleaseInterface(ref intPtr);
						}
					}
					this._bitsPerPixel = bitsPerPixel;
				}
				return (int)this._bitsPerPixel;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002BE1 RID: 11233 RVA: 0x000AF680 File Offset: 0x000AEA80
		internal bool HasAlpha
		{
			get
			{
				return (this.FormatFlags & PixelFormatFlags.ChannelOrderABGR) != PixelFormatFlags.BitsPerPixelUndefined || (this.FormatFlags & PixelFormatFlags.ChannelOrderARGB) != PixelFormatFlags.BitsPerPixelUndefined || (this.FormatFlags & PixelFormatFlags.NChannelAlpha) > PixelFormatFlags.BitsPerPixelUndefined;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06002BE2 RID: 11234 RVA: 0x000AF6BC File Offset: 0x000AEABC
		internal bool Palettized
		{
			get
			{
				return (this.FormatFlags & PixelFormatFlags.Palettized) > PixelFormatFlags.BitsPerPixelUndefined;
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06002BE3 RID: 11235 RVA: 0x000AF6D8 File Offset: 0x000AEAD8
		internal PixelFormatEnum Format
		{
			get
			{
				return this._format;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06002BE4 RID: 11236 RVA: 0x000AF6EC File Offset: 0x000AEAEC
		internal Guid Guid
		{
			get
			{
				return this._guidFormat.Value;
			}
		}

		/// <summary>Cria uma representação de cadeia de caracteres desse <see cref="T:System.Windows.Media.PixelFormat" />.</summary>
		/// <returns>Um <see cref="T:System.String" /> que contém uma representação do <see cref="T:System.Windows.Media.PixelFormat" />.</returns>
		// Token: 0x06002BE5 RID: 11237 RVA: 0x000AF704 File Offset: 0x000AEB04
		public override string ToString()
		{
			return this._format.ToString();
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x000AF724 File Offset: 0x000AEB24
		[SecurityCritical]
		internal static PixelFormat GetPixelFormat(SafeMILHandle bitmapSource)
		{
			Guid wicpixelFormatDontCare = WICPixelFormatGUIDs.WICPixelFormatDontCare;
			HRESULT.Check(UnsafeNativeMethods.WICBitmapSource.GetPixelFormat(bitmapSource, out wicpixelFormatDontCare));
			return new PixelFormat(wicpixelFormatDontCare);
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x000AF74C File Offset: 0x000AEB4C
		internal static PixelFormat GetPixelFormat(Guid pixelFormatGuid)
		{
			byte[] array = pixelFormatGuid.ToByteArray();
			return PixelFormat.GetPixelFormat((PixelFormatEnum)array[array.Length - 1]);
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x000AF770 File Offset: 0x000AEB70
		internal static PixelFormat GetPixelFormat(PixelFormatEnum pixelFormatEnum)
		{
			switch (pixelFormatEnum)
			{
			case PixelFormatEnum.Indexed1:
				return PixelFormats.Indexed1;
			case PixelFormatEnum.Indexed2:
				return PixelFormats.Indexed2;
			case PixelFormatEnum.Indexed4:
				return PixelFormats.Indexed4;
			case PixelFormatEnum.Indexed8:
				return PixelFormats.Indexed8;
			case PixelFormatEnum.BlackWhite:
				return PixelFormats.BlackWhite;
			case PixelFormatEnum.Gray2:
				return PixelFormats.Gray2;
			case PixelFormatEnum.Gray4:
				return PixelFormats.Gray4;
			case PixelFormatEnum.Gray8:
				return PixelFormats.Gray8;
			case PixelFormatEnum.Bgr555:
				return PixelFormats.Bgr555;
			case PixelFormatEnum.Bgr565:
				return PixelFormats.Bgr565;
			case PixelFormatEnum.Gray16:
				return PixelFormats.Gray16;
			case PixelFormatEnum.Bgr24:
				return PixelFormats.Bgr24;
			case PixelFormatEnum.Rgb24:
				return PixelFormats.Rgb24;
			case PixelFormatEnum.Bgr32:
				return PixelFormats.Bgr32;
			case PixelFormatEnum.Bgra32:
				return PixelFormats.Bgra32;
			case PixelFormatEnum.Pbgra32:
				return PixelFormats.Pbgra32;
			case PixelFormatEnum.Gray32Float:
				return PixelFormats.Gray32Float;
			case PixelFormatEnum.Bgr101010:
				return PixelFormats.Bgr101010;
			case PixelFormatEnum.Rgb48:
				return PixelFormats.Rgb48;
			case PixelFormatEnum.Rgba64:
				return PixelFormats.Rgba64;
			case PixelFormatEnum.Prgba64:
				return PixelFormats.Prgba64;
			case PixelFormatEnum.Rgba128Float:
				return PixelFormats.Rgba128Float;
			case PixelFormatEnum.Prgba128Float:
				return PixelFormats.Prgba128Float;
			case PixelFormatEnum.Rgb128Float:
				return PixelFormats.Rgb128Float;
			case PixelFormatEnum.Cmyk32:
				return PixelFormats.Cmyk32;
			}
			return PixelFormats.Default;
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x000AF898 File Offset: 0x000AEC98
		private static PixelFormatFlags GetPixelFormatFlagsFromGuid(Guid pixelFormatGuid)
		{
			PixelFormatFlags result = PixelFormatFlags.BitsPerPixelUndefined;
			if (pixelFormatGuid.CompareTo(PixelFormat.WICPixelFormatPhotonFirst) >= 0 && pixelFormatGuid.CompareTo(PixelFormat.WICPixelFormatPhotonLast) <= 0)
			{
				byte[] array = pixelFormatGuid.ToByteArray();
				switch (array[15])
				{
				case 29:
					result = (PixelFormatFlags.IsScRGB | PixelFormatFlags.ChannelOrderARGB);
					break;
				case 30:
					result = (PixelFormatFlags.IsScRGB | PixelFormatFlags.ChannelOrderARGB);
					break;
				case 31:
					result = PixelFormatFlags.IsCMYK;
					break;
				case 32:
					result = PixelFormatFlags.IsNChannel;
					break;
				case 33:
					result = PixelFormatFlags.IsNChannel;
					break;
				case 34:
					result = PixelFormatFlags.IsNChannel;
					break;
				case 35:
					result = PixelFormatFlags.IsNChannel;
					break;
				case 36:
					result = PixelFormatFlags.IsNChannel;
					break;
				case 37:
					result = PixelFormatFlags.IsNChannel;
					break;
				case 38:
					result = PixelFormatFlags.IsNChannel;
					break;
				case 39:
					result = PixelFormatFlags.IsNChannel;
					break;
				case 40:
					result = PixelFormatFlags.IsNChannel;
					break;
				case 41:
					result = PixelFormatFlags.IsNChannel;
					break;
				case 42:
					result = PixelFormatFlags.IsNChannel;
					break;
				case 43:
					result = PixelFormatFlags.IsNChannel;
					break;
				case 44:
					result = (PixelFormatFlags.IsCMYK | PixelFormatFlags.NChannelAlpha);
					break;
				case 45:
					result = (PixelFormatFlags.IsCMYK | PixelFormatFlags.NChannelAlpha);
					break;
				case 46:
					result = (PixelFormatFlags.NChannelAlpha | PixelFormatFlags.IsNChannel);
					break;
				case 47:
					result = (PixelFormatFlags.NChannelAlpha | PixelFormatFlags.IsNChannel);
					break;
				case 48:
					result = (PixelFormatFlags.NChannelAlpha | PixelFormatFlags.IsNChannel);
					break;
				case 49:
					result = (PixelFormatFlags.NChannelAlpha | PixelFormatFlags.IsNChannel);
					break;
				case 50:
					result = (PixelFormatFlags.NChannelAlpha | PixelFormatFlags.IsNChannel);
					break;
				case 51:
					result = (PixelFormatFlags.NChannelAlpha | PixelFormatFlags.IsNChannel);
					break;
				case 52:
					result = (PixelFormatFlags.NChannelAlpha | PixelFormatFlags.IsNChannel);
					break;
				case 53:
					result = (PixelFormatFlags.NChannelAlpha | PixelFormatFlags.IsNChannel);
					break;
				case 54:
					result = (PixelFormatFlags.NChannelAlpha | PixelFormatFlags.IsNChannel);
					break;
				case 55:
					result = (PixelFormatFlags.NChannelAlpha | PixelFormatFlags.IsNChannel);
					break;
				case 56:
					result = (PixelFormatFlags.NChannelAlpha | PixelFormatFlags.IsNChannel);
					break;
				case 57:
					result = (PixelFormatFlags.NChannelAlpha | PixelFormatFlags.IsNChannel);
					break;
				case 58:
					result = (PixelFormatFlags.IsScRGB | PixelFormatFlags.ChannelOrderARGB);
					break;
				case 59:
					result = (PixelFormatFlags.IsScRGB | PixelFormatFlags.ChannelOrderRGB);
					break;
				case 61:
					result = (PixelFormatFlags.IsScRGB | PixelFormatFlags.ChannelOrderRGB);
					break;
				case 62:
					result = (PixelFormatFlags.IsGray | PixelFormatFlags.IsScRGB);
					break;
				case 63:
					result = (PixelFormatFlags.IsGray | PixelFormatFlags.IsScRGB);
					break;
				case 64:
					result = (PixelFormatFlags.IsScRGB | PixelFormatFlags.ChannelOrderRGB);
					break;
				case 65:
					result = (PixelFormatFlags.IsScRGB | PixelFormatFlags.ChannelOrderRGB);
					break;
				case 66:
					result = (PixelFormatFlags.IsScRGB | PixelFormatFlags.ChannelOrderRGB);
					break;
				}
			}
			return result;
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x000AFAE8 File Offset: 0x000AEEE8
		private static PixelFormatFlags GetPixelFormatFlagsFromEnum(PixelFormatEnum pixelFormatEnum)
		{
			switch (pixelFormatEnum)
			{
			case PixelFormatEnum.Default:
				return PixelFormatFlags.BitsPerPixelUndefined;
			case PixelFormatEnum.Indexed1:
				return PixelFormatFlags.BitsPerPixel1 | PixelFormatFlags.Palettized;
			case PixelFormatEnum.Indexed2:
				return PixelFormatFlags.BitsPerPixel2 | PixelFormatFlags.Palettized;
			case PixelFormatEnum.Indexed4:
				return PixelFormatFlags.BitsPerPixel4 | PixelFormatFlags.Palettized;
			case PixelFormatEnum.Indexed8:
				return PixelFormatFlags.BitsPerPixel8 | PixelFormatFlags.Palettized;
			case PixelFormatEnum.BlackWhite:
				return PixelFormatFlags.BitsPerPixel1 | PixelFormatFlags.IsGray;
			case PixelFormatEnum.Gray2:
				return PixelFormatFlags.BitsPerPixel2 | PixelFormatFlags.IsGray;
			case PixelFormatEnum.Gray4:
				return PixelFormatFlags.BitsPerPixel4 | PixelFormatFlags.IsGray;
			case PixelFormatEnum.Gray8:
				return PixelFormatFlags.BitsPerPixel8 | PixelFormatFlags.IsGray;
			case PixelFormatEnum.Bgr555:
				return PixelFormatFlags.BitsPerPixel16 | PixelFormatFlags.IsSRGB | PixelFormatFlags.ChannelOrderBGR;
			case PixelFormatEnum.Bgr565:
				return PixelFormatFlags.BitsPerPixel16 | PixelFormatFlags.IsSRGB | PixelFormatFlags.ChannelOrderBGR;
			case PixelFormatEnum.Gray16:
				return PixelFormatFlags.BitsPerPixel16 | PixelFormatFlags.IsGray | PixelFormatFlags.IsSRGB;
			case PixelFormatEnum.Bgr24:
				return PixelFormatFlags.BitsPerPixel8 | PixelFormatFlags.BitsPerPixel16 | PixelFormatFlags.IsSRGB | PixelFormatFlags.ChannelOrderBGR;
			case PixelFormatEnum.Rgb24:
				return PixelFormatFlags.BitsPerPixel8 | PixelFormatFlags.BitsPerPixel16 | PixelFormatFlags.IsSRGB | PixelFormatFlags.ChannelOrderRGB;
			case PixelFormatEnum.Bgr32:
				return PixelFormatFlags.BitsPerPixel32 | PixelFormatFlags.IsSRGB | PixelFormatFlags.ChannelOrderBGR;
			case PixelFormatEnum.Bgra32:
				return PixelFormatFlags.BitsPerPixel32 | PixelFormatFlags.IsSRGB | PixelFormatFlags.ChannelOrderABGR;
			case PixelFormatEnum.Pbgra32:
				return PixelFormatFlags.BitsPerPixel32 | PixelFormatFlags.IsSRGB | PixelFormatFlags.Premultiplied | PixelFormatFlags.ChannelOrderABGR;
			case PixelFormatEnum.Gray32Float:
				return PixelFormatFlags.BitsPerPixel32 | PixelFormatFlags.IsGray | PixelFormatFlags.IsScRGB;
			case PixelFormatEnum.Bgr101010:
				return PixelFormatFlags.BitsPerPixel32 | PixelFormatFlags.IsSRGB | PixelFormatFlags.ChannelOrderBGR;
			case PixelFormatEnum.Rgb48:
				return PixelFormatFlags.BitsPerPixel16 | PixelFormatFlags.BitsPerPixel32 | PixelFormatFlags.IsSRGB | PixelFormatFlags.ChannelOrderRGB;
			case PixelFormatEnum.Rgba64:
				return PixelFormatFlags.BitsPerPixel64 | PixelFormatFlags.IsSRGB | PixelFormatFlags.ChannelOrderARGB;
			case PixelFormatEnum.Prgba64:
				return PixelFormatFlags.BitsPerPixel64 | PixelFormatFlags.IsSRGB | PixelFormatFlags.Premultiplied | PixelFormatFlags.ChannelOrderARGB;
			case PixelFormatEnum.Rgba128Float:
				return PixelFormatFlags.BitsPerPixel128 | PixelFormatFlags.IsScRGB | PixelFormatFlags.ChannelOrderARGB;
			case PixelFormatEnum.Prgba128Float:
				return PixelFormatFlags.BitsPerPixel128 | PixelFormatFlags.IsScRGB | PixelFormatFlags.Premultiplied | PixelFormatFlags.ChannelOrderARGB;
			case PixelFormatEnum.Rgb128Float:
				return PixelFormatFlags.BitsPerPixel128 | PixelFormatFlags.IsScRGB | PixelFormatFlags.ChannelOrderRGB;
			case PixelFormatEnum.Cmyk32:
				return PixelFormatFlags.BitsPerPixel32 | PixelFormatFlags.IsCMYK;
			}
			return PixelFormatFlags.BitsPerPixelUndefined;
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x000AFC10 File Offset: 0x000AF010
		private static uint GetBitsPerPixelFromEnum(PixelFormatEnum pixelFormatEnum)
		{
			switch (pixelFormatEnum)
			{
			case PixelFormatEnum.Default:
				return 0U;
			case PixelFormatEnum.Indexed1:
				return 1U;
			case PixelFormatEnum.Indexed2:
				return 2U;
			case PixelFormatEnum.Indexed4:
				return 4U;
			case PixelFormatEnum.Indexed8:
				return 8U;
			case PixelFormatEnum.BlackWhite:
				return 1U;
			case PixelFormatEnum.Gray2:
				return 2U;
			case PixelFormatEnum.Gray4:
				return 4U;
			case PixelFormatEnum.Gray8:
				return 8U;
			case PixelFormatEnum.Bgr555:
			case PixelFormatEnum.Bgr565:
				return 16U;
			case PixelFormatEnum.Gray16:
				return 16U;
			case PixelFormatEnum.Bgr24:
			case PixelFormatEnum.Rgb24:
				return 24U;
			case PixelFormatEnum.Bgr32:
			case PixelFormatEnum.Bgra32:
			case PixelFormatEnum.Pbgra32:
				return 32U;
			case PixelFormatEnum.Gray32Float:
				return 32U;
			case PixelFormatEnum.Bgr101010:
				return 32U;
			case PixelFormatEnum.Rgb48:
				return 48U;
			case PixelFormatEnum.Rgba64:
			case PixelFormatEnum.Prgba64:
				return 64U;
			case PixelFormatEnum.Rgba128Float:
			case PixelFormatEnum.Prgba128Float:
			case PixelFormatEnum.Rgb128Float:
				return 128U;
			case PixelFormatEnum.Cmyk32:
				return 32U;
			}
			return 0U;
		}

		// Token: 0x04001414 RID: 5140
		[NonSerialized]
		private PixelFormatFlags _flags;

		// Token: 0x04001415 RID: 5141
		[NonSerialized]
		private PixelFormatEnum _format;

		// Token: 0x04001416 RID: 5142
		[NonSerialized]
		private uint _bitsPerPixel;

		// Token: 0x04001417 RID: 5143
		[NonSerialized]
		private SecurityCriticalDataForSet<Guid> _guidFormat;

		// Token: 0x04001418 RID: 5144
		[NonSerialized]
		private static readonly Guid WICPixelFormatPhotonFirst = new Guid(1876804388, 19971, 19454, 177, 133, 61, 119, 118, 141, 201, 29);

		// Token: 0x04001419 RID: 5145
		[NonSerialized]
		private static readonly Guid WICPixelFormatPhotonLast = new Guid(1876804388, 19971, 19454, 177, 133, 61, 119, 118, 141, 201, 66);
	}
}
