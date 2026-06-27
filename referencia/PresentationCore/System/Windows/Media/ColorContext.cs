using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32;
using MS.Win32.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Representa o perfil de cor ICC (International Color Consortium) ou ICM (Gerenciamento de cores de imagem) que está associado uma imagem de bitmap.</summary>
	// Token: 0x0200036F RID: 879
	public class ColorContext
	{
		// Token: 0x06001F6F RID: 8047 RVA: 0x0008023C File Offset: 0x0007F63C
		[SecurityCritical]
		private ColorContext(SafeMILHandle colorContextHandle)
		{
			this._colorContextHandle = colorContextHandle;
			UnsafeNativeMethods.IWICColorContext.WICColorContextType wiccolorContextType;
			if (HRESULT.Failed(UnsafeNativeMethods.IWICColorContext.GetType(this._colorContextHandle, out wiccolorContextType)))
			{
				return;
			}
			if (wiccolorContextType != UnsafeNativeMethods.IWICColorContext.WICColorContextType.WICColorContextProfile)
			{
				if (wiccolorContextType != UnsafeNativeMethods.IWICColorContext.WICColorContextType.WICColorContextExifColorSpace)
				{
					if (Invariant.Strict)
					{
						Invariant.Assert(false, "IWICColorContext::GetType() returned WICColorContextUninitialized.");
					}
				}
				else
				{
					uint num;
					if (HRESULT.Failed(UnsafeNativeMethods.IWICColorContext.GetExifColorSpace(this._colorContextHandle, out num)))
					{
						return;
					}
					if (num == 1U || num == 65535U)
					{
						ResourceManager resourceManager = new ResourceManager(ColorContext._colorProfileResources, Assembly.GetAssembly(typeof(ColorContext)));
						byte[] array = (byte[])resourceManager.GetObject(ColorContext._sRGBProfileName);
						using (FactoryMaker factoryMaker = new FactoryMaker())
						{
							this._colorContextHandle.Dispose();
							this._colorContextHandle = null;
							if (HRESULT.Failed(UnsafeNativeMethods.WICCodec.CreateColorContext(factoryMaker.ImagingFactoryPtr, out this._colorContextHandle)))
							{
								return;
							}
							if (HRESULT.Failed(UnsafeNativeMethods.IWICColorContext.InitializeFromMemory(this._colorContextHandle, array, (uint)array.Length)))
							{
								return;
							}
						}
						this.FromRawBytes(array, array.Length, true);
						return;
					}
					if (Invariant.Strict)
					{
						Invariant.Assert(false, string.Format(CultureInfo.InvariantCulture, "IWICColorContext::GetExifColorSpace returned {0}.", new object[]
						{
							num
						}));
						return;
					}
				}
			}
			else
			{
				uint num2;
				int profileBytes = UnsafeNativeMethods.IWICColorContext.GetProfileBytes(this._colorContextHandle, 0U, null, out num2);
				if (HRESULT.Succeeded(profileBytes) && num2 != 0U)
				{
					byte[] array2 = new byte[num2];
					if (HRESULT.Failed(UnsafeNativeMethods.IWICColorContext.GetProfileBytes(this._colorContextHandle, num2, array2, out num2)))
					{
						return;
					}
					this.FromRawBytes(array2, (int)num2, true);
					return;
				}
			}
		}

		/// <summary>Inicializa uma nova instância da <see cref="T:System.Windows.Media.ColorContext" /> com o perfil de cor ICC (International Color Consortium) ou ICM (Gerenciamento de cores de imagem) localizado em um determinado <see cref="T:System.Uri" />.</summary>
		/// <param name="profileUri">O <see cref="T:System.Uri" /> que identifica o local do perfil de cor desejado.</param>
		// Token: 0x06001F70 RID: 8048 RVA: 0x000803D8 File Offset: 0x0007F7D8
		[SecurityCritical]
		public ColorContext(Uri profileUri)
		{
			this.Initialize(profileUri, false);
		}

		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.Media.ColorContext" /> com o perfil de cor padrão (sRGB ou RGB) que mais se aproxima do <see cref="T:System.Windows.Media.PixelFormat" /> fornecido.</summary>
		/// <param name="pixelFormat">O <see cref="T:System.Windows.Media.PixelFormat" /> do qual o <see cref="T:System.Windows.Media.ColorContext" /> é derivado.</param>
		// Token: 0x06001F71 RID: 8049 RVA: 0x000803F4 File Offset: 0x0007F7F4
		[SecurityCritical]
		public ColorContext(PixelFormat pixelFormat)
		{
			switch (pixelFormat.Format)
			{
			default:
				this.Initialize(ColorContext.GetStandardColorSpaceProfile(), true);
				return;
			case PixelFormatEnum.BlackWhite:
			case PixelFormatEnum.Gray2:
			case PixelFormatEnum.Gray4:
			case PixelFormatEnum.Gray8:
			case PixelFormatEnum.Gray32Float:
			case PixelFormatEnum.Rgba64:
			case PixelFormatEnum.Prgba64:
			case PixelFormatEnum.Rgba128Float:
			case PixelFormatEnum.Prgba128Float:
			case PixelFormatEnum.Cmyk32:
				throw new NotSupportedException();
			}
		}

		/// <summary>Retorna um <see cref="T:System.IO.Stream" /> legível dos dados brutos de perfil de cor.</summary>
		/// <returns>Um <see cref="T:System.IO.Stream" /> legível dos dados brutos de perfil de cor.</returns>
		// Token: 0x06001F72 RID: 8050 RVA: 0x0008049C File Offset: 0x0007F89C
		[SecurityCritical]
		public Stream OpenProfileStream()
		{
			if (this._colorContextHelper.IsInvalid)
			{
				throw new NullReferenceException();
			}
			uint num = 0U;
			this._colorContextHelper.GetColorProfileFromHandle(null, ref num);
			byte[] buffer = new byte[num];
			this._colorContextHelper.GetColorProfileFromHandle(buffer, ref num);
			return new MemoryStream(buffer);
		}

		/// <summary>Obtém um <see cref="T:System.Uri" /> que representa o local de um perfil de cor ICC (International Color Consortium) ou ICM (Gerenciamento de cores de imagem).</summary>
		/// <returns>Um <see cref="T:System.Uri" /> que representa o local de um ICC (International Color Consortium) ou ICM (Gerenciamento de cores de imagem) perfil de cor.</returns>
		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001F73 RID: 8051 RVA: 0x000804E8 File Offset: 0x0007F8E8
		public Uri ProfileUri
		{
			[SecurityCritical]
			get
			{
				Uri value = this._profileUri.Value;
				if (this._isProfileUriNotFromUser.Value)
				{
					Invariant.Assert(value.IsFile);
					SecurityHelper.DemandPathDiscovery(value.LocalPath);
				}
				return value;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001F74 RID: 8052 RVA: 0x00080528 File Offset: 0x0007F928
		internal SafeProfileHandle ProfileHandle
		{
			[SecurityCritical]
			get
			{
				return this._colorContextHelper.ProfileHandle;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001F75 RID: 8053 RVA: 0x00080540 File Offset: 0x0007F940
		internal SafeMILHandle ColorContextHandle
		{
			[SecurityCritical]
			get
			{
				return this._colorContextHandle;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001F76 RID: 8054 RVA: 0x00080554 File Offset: 0x0007F954
		internal int NumChannels
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				if (this._colorContextHelper.IsInvalid)
				{
					return 3;
				}
				return this._numChannels;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001F77 RID: 8055 RVA: 0x00080578 File Offset: 0x0007F978
		internal uint ColorType
		{
			get
			{
				return (uint)ColorContext._colorTypeFromChannels[this.NumChannels];
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001F78 RID: 8056 RVA: 0x00080594 File Offset: 0x0007F994
		internal ColorContext.StandardColorSpace ColorSpaceFamily
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				if (this._colorContextHelper.IsInvalid)
				{
					return ColorContext.StandardColorSpace.Srgb;
				}
				return this._colorSpaceFamily;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001F79 RID: 8057 RVA: 0x000805B8 File Offset: 0x0007F9B8
		internal bool IsValid
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				return !this._colorContextHelper.IsInvalid;
			}
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x000805D4 File Offset: 0x0007F9D4
		[SecurityCritical]
		internal static IList<ColorContext> GetColorContextsHelper(ColorContext.GetColorContextsDelegate getColorContexts)
		{
			uint num = 0U;
			List<ColorContext> list = null;
			int num2 = getColorContexts(ref num, null);
			if (num2 != -2003292287)
			{
				HRESULT.Check(num2);
			}
			if (num > 0U)
			{
				SafeMILHandle[] array = new SafeMILHandle[num];
				using (FactoryMaker factoryMaker = new FactoryMaker())
				{
					for (uint num3 = 0U; num3 < num; num3 += 1U)
					{
						HRESULT.Check(UnsafeNativeMethods.WICCodec.CreateColorContext(factoryMaker.ImagingFactoryPtr, out array[(int)num3]));
					}
				}
				IntPtr[] array2 = new IntPtr[num];
				for (uint num4 = 0U; num4 < num; num4 += 1U)
				{
					array2[(int)num4] = array[(int)num4].DangerousGetHandle();
				}
				HRESULT.Check(getColorContexts(ref num, array2));
				list = new List<ColorContext>((int)num);
				for (uint num5 = 0U; num5 < num; num5 += 1U)
				{
					list.Add(new ColorContext(array[(int)num5]));
				}
			}
			return list;
		}

		/// <summary>Determina se um <see cref="T:System.Object" /> é igual a uma instância de <see cref="T:System.Windows.Media.ColorContext" />.</summary>
		/// <param name="obj">Identifica o <see cref="T:System.Object" /> a comparar quanto à igualdade.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> fornecido é igual a esta instância de <see cref="T:System.Windows.Media.ColorContext" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001F7B RID: 8059 RVA: 0x000806BC File Offset: 0x0007FABC
		public override bool Equals(object obj)
		{
			ColorContext context = obj as ColorContext;
			return context == this;
		}

		/// <summary>Obtém o código hash para esta instância de <see cref="T:System.Windows.Media.ColorContext" />.</summary>
		/// <returns>Um <see cref="T:System.Int32" /> que representa o código hash para o objeto.</returns>
		// Token: 0x06001F7C RID: 8060 RVA: 0x000806D8 File Offset: 0x0007FAD8
		[SecurityCritical]
		public override int GetHashCode()
		{
			return (int)this._profileHeader.phDateTime_2;
		}

		/// <summary>Opera em duas instâncias de <see cref="T:System.Windows.Media.ColorContext" /> para determinar igualdade.</summary>
		/// <param name="context1">A primeira instância de <see cref="T:System.Windows.Media.ColorContext" /> a ser comparada.</param>
		/// <param name="context2">A segunda instância de <see cref="T:System.Windows.Media.ColorContext" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se as instâncias de <see cref="T:System.Windows.Media.ColorContext" /> forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001F7D RID: 8061 RVA: 0x000806F0 File Offset: 0x0007FAF0
		[SecurityCritical]
		public static bool operator ==(ColorContext context1, ColorContext context2)
		{
			return (context1 == null && context2 == null) || (context1 != null && context2 != null && (context1._profileHeader.phSize == context2._profileHeader.phSize && context1._profileHeader.phCMMType == context2._profileHeader.phCMMType && context1._profileHeader.phVersion == context2._profileHeader.phVersion && context1._profileHeader.phClass == context2._profileHeader.phClass && context1._profileHeader.phDataColorSpace == context2._profileHeader.phDataColorSpace && context1._profileHeader.phConnectionSpace == context2._profileHeader.phConnectionSpace && context1._profileHeader.phDateTime_0 == context2._profileHeader.phDateTime_0 && context1._profileHeader.phDateTime_1 == context2._profileHeader.phDateTime_1 && context1._profileHeader.phDateTime_2 == context2._profileHeader.phDateTime_2 && context1._profileHeader.phSignature == context2._profileHeader.phSignature && context1._profileHeader.phPlatform == context2._profileHeader.phPlatform && context1._profileHeader.phProfileFlags == context2._profileHeader.phProfileFlags && context1._profileHeader.phManufacturer == context2._profileHeader.phManufacturer && context1._profileHeader.phModel == context2._profileHeader.phModel && context1._profileHeader.phAttributes_0 == context2._profileHeader.phAttributes_0 && context1._profileHeader.phAttributes_1 == context2._profileHeader.phAttributes_1 && context1._profileHeader.phRenderingIntent == context2._profileHeader.phRenderingIntent && context1._profileHeader.phIlluminant_0 == context2._profileHeader.phIlluminant_0 && context1._profileHeader.phIlluminant_1 == context2._profileHeader.phIlluminant_1 && context1._profileHeader.phIlluminant_2 == context2._profileHeader.phIlluminant_2) && context1._profileHeader.phCreator == context2._profileHeader.phCreator);
		}

		/// <summary>Opera em duas instâncias de <see cref="T:System.Windows.Media.ColorContext" /> para determinar que elas não são iguais.</summary>
		/// <param name="context1">A primeira instância de <see cref="T:System.Windows.Media.ColorContext" /> a ser comparada.</param>
		/// <param name="context2">A segunda instância de <see cref="T:System.Windows.Media.ColorContext" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se as instâncias de <see cref="T:System.Windows.Media.ColorContext" /> não forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001F7E RID: 8062 RVA: 0x00080944 File Offset: 0x0007FD44
		public static bool operator !=(ColorContext context1, ColorContext context2)
		{
			return !(context1 == context2);
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x0008095C File Offset: 0x0007FD5C
		[SecurityCritical]
		private void Initialize(Uri profileUri, bool isStandardProfileUriNotFromUser)
		{
			bool flag = false;
			if (profileUri == null)
			{
				throw new ArgumentNullException("profileUri");
			}
			if (!profileUri.IsAbsoluteUri)
			{
				throw new ArgumentException(SR.Get("UriNotAbsolute"), "profileUri");
			}
			this._profileUri = new SecurityCriticalData<Uri>(profileUri);
			this._isProfileUriNotFromUser = new SecurityCriticalDataForSet<bool>(isStandardProfileUriNotFromUser);
			Stream stream = null;
			try
			{
				stream = WpfWebRequestHelper.CreateRequestAndGetResponseStream(profileUri);
			}
			catch (WebException)
			{
				if (isStandardProfileUriNotFromUser)
				{
					flag = true;
				}
			}
			if (stream == null)
			{
				if (!flag)
				{
					Invariant.Assert(!isStandardProfileUriNotFromUser);
					throw new FileNotFoundException(SR.Get("FileNotFoundExceptionWithFileName", new object[]
					{
						profileUri.AbsolutePath
					}), profileUri.AbsolutePath);
				}
				ResourceManager resourceManager = new ResourceManager(ColorContext._colorProfileResources, Assembly.GetAssembly(typeof(ColorContext)));
				byte[] buffer = (byte[])resourceManager.GetObject(ColorContext._sRGBProfileName);
				stream = new MemoryStream(buffer);
			}
			this.FromStream(stream, profileUri.AbsolutePath);
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x00080A58 File Offset: 0x0007FE58
		[SecurityCritical]
		private static Uri GetStandardColorSpaceProfile()
		{
			uint dwProfileID = 1934772034U;
			uint num = 260U;
			StringBuilder stringBuilder = new StringBuilder(260);
			HRESULT.Check(UnsafeNativeMethods.Mscms.GetStandardColorSpaceProfile(IntPtr.Zero, dwProfileID, stringBuilder, out num));
			string text = stringBuilder.ToString();
			Uri result;
			if (!Uri.TryCreate(text, UriKind.Absolute, out result))
			{
				num = 260U;
				HRESULT.Check(UnsafeNativeMethods.Mscms.GetColorDirectory(IntPtr.Zero, stringBuilder, out num));
				result = new Uri(Path.Combine(stringBuilder.ToString(), text));
			}
			return result;
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x00080AD0 File Offset: 0x0007FED0
		[SecurityCritical]
		private void FromStream(Stream stm, string filename)
		{
			int i = 1048576;
			if (stm.CanSeek)
			{
				i = (int)stm.Length + 1;
			}
			byte[] array = new byte[i];
			int num = 0;
			while (i < 33554432)
			{
				num += stm.Read(array, num, i - num);
				if (num < i)
				{
					this.FromRawBytes(array, num, false);
					using (FactoryMaker factoryMaker = new FactoryMaker())
					{
						HRESULT.Check(UnsafeNativeMethods.WICCodec.CreateColorContext(factoryMaker.ImagingFactoryPtr, out this._colorContextHandle));
						HRESULT.Check(UnsafeNativeMethods.IWICColorContext.InitializeFromMemory(this._colorContextHandle, array, (uint)num));
					}
					return;
				}
				i += 1048576;
				byte[] array2 = new byte[i];
				array.CopyTo(array2, 0);
				array = array2;
			}
			throw new ArgumentException(SR.Get("ColorContext_FileTooLarge"), filename);
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x00080BA8 File Offset: 0x0007FFA8
		[SecurityCritical]
		private unsafe void FromRawBytes(byte[] data, int dataLength, bool dontThrowException)
		{
			Invariant.Assert(dataLength <= data.Length);
			Invariant.Assert(dataLength >= 0);
			fixed (byte[] array = data)
			{
				void* pProfileData;
				if (data == null || array.Length == 0)
				{
					pProfileData = null;
				}
				else
				{
					pProfileData = (void*)(&array[0]);
				}
				UnsafeNativeMethods.PROFILE profile;
				profile.dwType = NativeMethods.ProfileType.PROFILE_MEMBUFFER;
				profile.pProfileData = pProfileData;
				profile.cbDataSize = (uint)dataLength;
				this._colorContextHelper.OpenColorProfile(ref profile);
				if (this._colorContextHelper.IsInvalid)
				{
					if (dontThrowException)
					{
						return;
					}
					HRESULT.Check(Marshal.GetHRForLastWin32Error());
				}
			}
			UnsafeNativeMethods.PROFILEHEADER profileheader;
			if (!this._colorContextHelper.GetColorProfileHeader(out profileheader))
			{
				if (dontThrowException)
				{
					return;
				}
				HRESULT.Check(Marshal.GetHRForLastWin32Error());
			}
			this._profileHeader.phSize = profileheader.phSize;
			this._profileHeader.phCMMType = profileheader.phCMMType;
			this._profileHeader.phVersion = profileheader.phVersion;
			this._profileHeader.phClass = profileheader.phClass;
			this._profileHeader.phDataColorSpace = profileheader.phDataColorSpace;
			this._profileHeader.phConnectionSpace = profileheader.phConnectionSpace;
			this._profileHeader.phDateTime_0 = profileheader.phDateTime_0;
			this._profileHeader.phDateTime_1 = profileheader.phDateTime_1;
			this._profileHeader.phDateTime_2 = profileheader.phDateTime_2;
			this._profileHeader.phSignature = profileheader.phSignature;
			this._profileHeader.phPlatform = profileheader.phPlatform;
			this._profileHeader.phProfileFlags = profileheader.phProfileFlags;
			this._profileHeader.phManufacturer = profileheader.phManufacturer;
			this._profileHeader.phModel = profileheader.phModel;
			this._profileHeader.phAttributes_0 = profileheader.phAttributes_0;
			this._profileHeader.phAttributes_1 = profileheader.phAttributes_1;
			this._profileHeader.phRenderingIntent = profileheader.phRenderingIntent;
			this._profileHeader.phIlluminant_0 = profileheader.phIlluminant_0;
			this._profileHeader.phIlluminant_1 = profileheader.phIlluminant_1;
			this._profileHeader.phIlluminant_2 = profileheader.phIlluminant_2;
			this._profileHeader.phCreator = profileheader.phCreator;
			NativeMethods.ColorSpace phDataColorSpace = this._profileHeader.phDataColorSpace;
			if (phDataColorSpace <= NativeMethods.ColorSpace.SPACE_CMY)
			{
				if (phDataColorSpace <= NativeMethods.ColorSpace.SPACE_7_CHANNEL)
				{
					if (phDataColorSpace <= NativeMethods.ColorSpace.SPACE_4_CHANNEL)
					{
						if (phDataColorSpace == NativeMethods.ColorSpace.SPACE_2_CHANNEL)
						{
							this._colorSpaceFamily = ColorContext.StandardColorSpace.Multichannel;
							this._numChannels = 2;
							return;
						}
						if (phDataColorSpace == NativeMethods.ColorSpace.SPACE_3_CHANNEL)
						{
							this._colorSpaceFamily = ColorContext.StandardColorSpace.Multichannel;
							this._numChannels = 3;
							return;
						}
						if (phDataColorSpace != NativeMethods.ColorSpace.SPACE_4_CHANNEL)
						{
							goto IL_480;
						}
						this._colorSpaceFamily = ColorContext.StandardColorSpace.Multichannel;
						this._numChannels = 4;
						return;
					}
					else
					{
						if (phDataColorSpace == NativeMethods.ColorSpace.SPACE_5_CHANNEL)
						{
							this._numChannels = 5;
							this._colorSpaceFamily = ColorContext.StandardColorSpace.Multichannel;
							return;
						}
						if (phDataColorSpace == NativeMethods.ColorSpace.SPACE_6_CHANNEL)
						{
							this._numChannels = 6;
							this._colorSpaceFamily = ColorContext.StandardColorSpace.Multichannel;
							return;
						}
						if (phDataColorSpace != NativeMethods.ColorSpace.SPACE_7_CHANNEL)
						{
							goto IL_480;
						}
						this._numChannels = 7;
						this._colorSpaceFamily = ColorContext.StandardColorSpace.Multichannel;
						return;
					}
				}
				else if (phDataColorSpace <= NativeMethods.ColorSpace.SPACE_A_CHANNEL)
				{
					if (phDataColorSpace == NativeMethods.ColorSpace.SPACE_8_CHANNEL)
					{
						this._numChannels = 8;
						this._colorSpaceFamily = ColorContext.StandardColorSpace.Multichannel;
						return;
					}
					if (phDataColorSpace == NativeMethods.ColorSpace.SPACE_9_CHANNEL)
					{
						this._numChannels = 9;
						this._colorSpaceFamily = ColorContext.StandardColorSpace.Multichannel;
						return;
					}
					if (phDataColorSpace != NativeMethods.ColorSpace.SPACE_A_CHANNEL)
					{
						goto IL_480;
					}
					this._numChannels = 10;
					this._colorSpaceFamily = ColorContext.StandardColorSpace.Multichannel;
					return;
				}
				else
				{
					if (phDataColorSpace == NativeMethods.ColorSpace.SPACE_B_CHANNEL)
					{
						this._numChannels = 11;
						this._colorSpaceFamily = ColorContext.StandardColorSpace.Multichannel;
						return;
					}
					if (phDataColorSpace == NativeMethods.ColorSpace.SPACE_C_CHANNEL)
					{
						this._numChannels = 12;
						this._colorSpaceFamily = ColorContext.StandardColorSpace.Multichannel;
						return;
					}
					if (phDataColorSpace != NativeMethods.ColorSpace.SPACE_CMY)
					{
						goto IL_480;
					}
				}
			}
			else if (phDataColorSpace <= NativeMethods.ColorSpace.SPACE_HLS)
			{
				if (phDataColorSpace <= NativeMethods.ColorSpace.SPACE_E_CHANNEL)
				{
					if (phDataColorSpace == NativeMethods.ColorSpace.SPACE_CMYK)
					{
						this._colorSpaceFamily = ColorContext.StandardColorSpace.Cmyk;
						this._numChannels = 4;
						return;
					}
					if (phDataColorSpace == NativeMethods.ColorSpace.SPACE_D_CHANNEL)
					{
						this._numChannels = 13;
						this._colorSpaceFamily = ColorContext.StandardColorSpace.Multichannel;
						return;
					}
					if (phDataColorSpace != NativeMethods.ColorSpace.SPACE_E_CHANNEL)
					{
						goto IL_480;
					}
					this._numChannels = 14;
					this._colorSpaceFamily = ColorContext.StandardColorSpace.Multichannel;
					return;
				}
				else
				{
					if (phDataColorSpace == NativeMethods.ColorSpace.SPACE_F_CHANNEL)
					{
						this._numChannels = 15;
						this._colorSpaceFamily = ColorContext.StandardColorSpace.Multichannel;
						return;
					}
					if (phDataColorSpace == NativeMethods.ColorSpace.SPACE_GRAY)
					{
						this._colorSpaceFamily = ColorContext.StandardColorSpace.Gray;
						this._numChannels = 1;
						return;
					}
					if (phDataColorSpace != NativeMethods.ColorSpace.SPACE_HLS)
					{
						goto IL_480;
					}
				}
			}
			else if (phDataColorSpace <= NativeMethods.ColorSpace.SPACE_Luv)
			{
				if (phDataColorSpace != NativeMethods.ColorSpace.SPACE_HSV && phDataColorSpace != NativeMethods.ColorSpace.SPACE_Lab && phDataColorSpace != NativeMethods.ColorSpace.SPACE_Luv)
				{
					goto IL_480;
				}
			}
			else if (phDataColorSpace <= NativeMethods.ColorSpace.SPACE_XYZ)
			{
				if (phDataColorSpace == NativeMethods.ColorSpace.SPACE_RGB)
				{
					this._colorSpaceFamily = ColorContext.StandardColorSpace.Rgb;
					this._numChannels = 3;
					return;
				}
				if (phDataColorSpace != NativeMethods.ColorSpace.SPACE_XYZ)
				{
					goto IL_480;
				}
			}
			else if (phDataColorSpace != NativeMethods.ColorSpace.SPACE_YCbCr && phDataColorSpace != NativeMethods.ColorSpace.SPACE_Yxy)
			{
				goto IL_480;
			}
			this._numChannels = 3;
			this._colorSpaceFamily = ColorContext.StandardColorSpace.Unknown;
			return;
			IL_480:
			this._numChannels = 0;
			this._colorSpaceFamily = ColorContext.StandardColorSpace.Unknown;
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x00081044 File Offset: 0x00080444
		// Note: this type is marked as 'beforefieldinit'.
		static ColorContext()
		{
			NativeMethods.COLORTYPE[] array = new NativeMethods.COLORTYPE[9];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails><PresentationCoreCSharp_netmodule>.0AD352765C5CCE0BF65A16EE5F0BEA1356F4747AE40E39B7AA0CFD74C60DC059).FieldHandle);
			ColorContext._colorTypeFromChannels = array;
			ColorContext._colorProfileResources = "ColorProfiles";
			ColorContext._sRGBProfileName = "sRGB_icm";
		}

		// Token: 0x04001055 RID: 4181
		[SecurityCritical]
		private ColorContextHelper _colorContextHelper;

		// Token: 0x04001056 RID: 4182
		private ColorContext.StandardColorSpace _colorSpaceFamily;

		// Token: 0x04001057 RID: 4183
		private int _numChannels;

		// Token: 0x04001058 RID: 4184
		private SecurityCriticalData<Uri> _profileUri;

		// Token: 0x04001059 RID: 4185
		private SecurityCriticalDataForSet<bool> _isProfileUriNotFromUser;

		// Token: 0x0400105A RID: 4186
		[SecurityCritical]
		private ColorContext.AbbreviatedPROFILEHEADER _profileHeader;

		// Token: 0x0400105B RID: 4187
		[SecurityCritical]
		private SafeMILHandle _colorContextHandle;

		// Token: 0x0400105C RID: 4188
		private const int _bufferSizeIncrement = 1048576;

		// Token: 0x0400105D RID: 4189
		private const int _maximumColorContextLength = 33554432;

		// Token: 0x0400105E RID: 4190
		private static readonly NativeMethods.COLORTYPE[] _colorTypeFromChannels;

		// Token: 0x0400105F RID: 4191
		private static readonly string _colorProfileResources;

		// Token: 0x04001060 RID: 4192
		private static readonly string _sRGBProfileName;

		// Token: 0x0200085C RID: 2140
		// (Invoke) Token: 0x0600571A RID: 22298
		internal delegate int GetColorContextsDelegate(ref uint numContexts, IntPtr[] colorContextPtrs);

		// Token: 0x0200085D RID: 2141
		private struct AbbreviatedPROFILEHEADER
		{
			// Token: 0x04002832 RID: 10290
			public uint phSize;

			// Token: 0x04002833 RID: 10291
			public uint phCMMType;

			// Token: 0x04002834 RID: 10292
			public uint phVersion;

			// Token: 0x04002835 RID: 10293
			public uint phClass;

			// Token: 0x04002836 RID: 10294
			public NativeMethods.ColorSpace phDataColorSpace;

			// Token: 0x04002837 RID: 10295
			public uint phConnectionSpace;

			// Token: 0x04002838 RID: 10296
			public uint phDateTime_0;

			// Token: 0x04002839 RID: 10297
			public uint phDateTime_1;

			// Token: 0x0400283A RID: 10298
			public uint phDateTime_2;

			// Token: 0x0400283B RID: 10299
			public uint phSignature;

			// Token: 0x0400283C RID: 10300
			public uint phPlatform;

			// Token: 0x0400283D RID: 10301
			public uint phProfileFlags;

			// Token: 0x0400283E RID: 10302
			public uint phManufacturer;

			// Token: 0x0400283F RID: 10303
			public uint phModel;

			// Token: 0x04002840 RID: 10304
			public uint phAttributes_0;

			// Token: 0x04002841 RID: 10305
			public uint phAttributes_1;

			// Token: 0x04002842 RID: 10306
			public uint phRenderingIntent;

			// Token: 0x04002843 RID: 10307
			public uint phIlluminant_0;

			// Token: 0x04002844 RID: 10308
			public uint phIlluminant_1;

			// Token: 0x04002845 RID: 10309
			public uint phIlluminant_2;

			// Token: 0x04002846 RID: 10310
			public uint phCreator;
		}

		// Token: 0x0200085E RID: 2142
		internal enum StandardColorSpace
		{
			// Token: 0x04002848 RID: 10312
			Unknown,
			// Token: 0x04002849 RID: 10313
			Srgb,
			// Token: 0x0400284A RID: 10314
			ScRgb,
			// Token: 0x0400284B RID: 10315
			Rgb,
			// Token: 0x0400284C RID: 10316
			Cmyk,
			// Token: 0x0400284D RID: 10317
			Gray = 6,
			// Token: 0x0400284E RID: 10318
			Multichannel
		}
	}
}
