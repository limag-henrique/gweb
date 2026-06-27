using System;
using System.Security;
using System.Text;
using MS.Internal;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Fornece informações sobre um codec de geração de imagens.</summary>
	// Token: 0x020005CF RID: 1487
	public abstract class BitmapCodecInfo
	{
		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.Media.Imaging.BitmapCodecInfo" />.</summary>
		// Token: 0x0600434E RID: 17230 RVA: 0x00104E44 File Offset: 0x00104244
		protected BitmapCodecInfo()
		{
		}

		// Token: 0x0600434F RID: 17231 RVA: 0x00104E58 File Offset: 0x00104258
		internal BitmapCodecInfo(SafeMILHandle codecInfoHandle)
		{
			this._isBuiltIn = true;
			this._codecInfoHandle = codecInfoHandle;
		}

		/// <summary>Obtém um valor que identifica o formato de contêiner do codec.</summary>
		/// <returns>O formato de contêiner do codec.</returns>
		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06004350 RID: 17232 RVA: 0x00104E7C File Offset: 0x0010427C
		public virtual Guid ContainerFormat
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandRegistryPermission();
				this.EnsureBuiltIn();
				Guid result;
				HRESULT.Check(UnsafeNativeMethods.WICBitmapCodecInfo.GetContainerFormat(this._codecInfoHandle, out result));
				return result;
			}
		}

		/// <summary>Obtém um valor que identifica o criador do codec.</summary>
		/// <returns>O autor do codec.</returns>
		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06004351 RID: 17233 RVA: 0x00104EA8 File Offset: 0x001042A8
		public virtual string Author
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandRegistryPermission();
				this.EnsureBuiltIn();
				StringBuilder stringBuilder = null;
				uint num = 0U;
				HRESULT.Check(UnsafeNativeMethods.WICComponentInfo.GetAuthor(this._codecInfoHandle, 0U, stringBuilder, out num));
				if (num > 0U)
				{
					stringBuilder = new StringBuilder((int)num);
					HRESULT.Check(UnsafeNativeMethods.WICComponentInfo.GetAuthor(this._codecInfoHandle, num, stringBuilder, out num));
				}
				if (stringBuilder != null)
				{
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		/// <summary>Obtém um valor que identifica a versão do codec.</summary>
		/// <returns>A versão do codec.</returns>
		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06004352 RID: 17234 RVA: 0x00104F08 File Offset: 0x00104308
		public virtual Version Version
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandRegistryPermission();
				this.EnsureBuiltIn();
				StringBuilder stringBuilder = null;
				uint num = 0U;
				HRESULT.Check(UnsafeNativeMethods.WICComponentInfo.GetVersion(this._codecInfoHandle, 0U, stringBuilder, out num));
				if (num > 0U)
				{
					stringBuilder = new StringBuilder((int)num);
					HRESULT.Check(UnsafeNativeMethods.WICComponentInfo.GetVersion(this._codecInfoHandle, num, stringBuilder, out num));
				}
				if (stringBuilder != null)
				{
					return new Version(stringBuilder.ToString());
				}
				return new Version();
			}
		}

		/// <summary>Obtém um valor que identifica a versão de especificação do codec.</summary>
		/// <returns>A versão de especificação do codec.</returns>
		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06004353 RID: 17235 RVA: 0x00104F6C File Offset: 0x0010436C
		public virtual Version SpecificationVersion
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandRegistryPermission();
				this.EnsureBuiltIn();
				StringBuilder stringBuilder = null;
				uint num = 0U;
				HRESULT.Check(UnsafeNativeMethods.WICComponentInfo.GetSpecVersion(this._codecInfoHandle, 0U, stringBuilder, out num));
				if (num > 0U)
				{
					stringBuilder = new StringBuilder((int)num);
					HRESULT.Check(UnsafeNativeMethods.WICComponentInfo.GetSpecVersion(this._codecInfoHandle, num, stringBuilder, out num));
				}
				if (stringBuilder != null)
				{
					return new Version(stringBuilder.ToString());
				}
				return new Version();
			}
		}

		/// <summary>Obtém um valor que representa o nome amigável do codec.</summary>
		/// <returns>O nome amigável do codec.</returns>
		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06004354 RID: 17236 RVA: 0x00104FD0 File Offset: 0x001043D0
		public virtual string FriendlyName
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandRegistryPermission();
				this.EnsureBuiltIn();
				StringBuilder stringBuilder = null;
				uint num = 0U;
				HRESULT.Check(UnsafeNativeMethods.WICComponentInfo.GetFriendlyName(this._codecInfoHandle, 0U, stringBuilder, out num));
				if (num > 0U)
				{
					stringBuilder = new StringBuilder((int)num);
					HRESULT.Check(UnsafeNativeMethods.WICComponentInfo.GetFriendlyName(this._codecInfoHandle, num, stringBuilder, out num));
				}
				if (stringBuilder != null)
				{
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		/// <summary>Obtém um valor que identifica o fabricante do dispositivo do codec.</summary>
		/// <returns>O fabricante do dispositivo do codec.</returns>
		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06004355 RID: 17237 RVA: 0x00105030 File Offset: 0x00104430
		public virtual string DeviceManufacturer
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandRegistryPermission();
				this.EnsureBuiltIn();
				StringBuilder stringBuilder = null;
				uint num = 0U;
				HRESULT.Check(UnsafeNativeMethods.WICBitmapCodecInfo.GetDeviceManufacturer(this._codecInfoHandle, 0U, stringBuilder, out num));
				if (num > 0U)
				{
					stringBuilder = new StringBuilder((int)num);
					HRESULT.Check(UnsafeNativeMethods.WICBitmapCodecInfo.GetDeviceManufacturer(this._codecInfoHandle, num, stringBuilder, out num));
				}
				if (stringBuilder != null)
				{
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		/// <summary>Obtém um valor que identifica os modelos de dispositivo do codec.</summary>
		/// <returns>O modelo do dispositivo do codec.</returns>
		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06004356 RID: 17238 RVA: 0x00105090 File Offset: 0x00104490
		public virtual string DeviceModels
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandRegistryPermission();
				this.EnsureBuiltIn();
				StringBuilder stringBuilder = null;
				uint num = 0U;
				HRESULT.Check(UnsafeNativeMethods.WICBitmapCodecInfo.GetDeviceModels(this._codecInfoHandle, 0U, stringBuilder, out num));
				if (num > 0U)
				{
					stringBuilder = new StringBuilder((int)num);
					HRESULT.Check(UnsafeNativeMethods.WICBitmapCodecInfo.GetDeviceModels(this._codecInfoHandle, num, stringBuilder, out num));
				}
				if (stringBuilder != null)
				{
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		/// <summary>Obtém um valor que identifica o MIME (Multipurpose Internet Mail Extensions) associado ao codec.</summary>
		/// <returns>O MIME (Multipurpose Internet Mail Extensions) tipos associados com o codec.</returns>
		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06004357 RID: 17239 RVA: 0x001050F0 File Offset: 0x001044F0
		public virtual string MimeTypes
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandRegistryPermission();
				this.EnsureBuiltIn();
				StringBuilder stringBuilder = null;
				uint num = 0U;
				HRESULT.Check(UnsafeNativeMethods.WICBitmapCodecInfo.GetMimeTypes(this._codecInfoHandle, 0U, stringBuilder, out num));
				if (num > 0U)
				{
					stringBuilder = new StringBuilder((int)num);
					HRESULT.Check(UnsafeNativeMethods.WICBitmapCodecInfo.GetMimeTypes(this._codecInfoHandle, num, stringBuilder, out num));
				}
				if (stringBuilder != null)
				{
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		/// <summary>Obtém um valor que identifica as extensões de arquivo associadas ao codec.</summary>
		/// <returns>As extensões de arquivo associadas ao codec.</returns>
		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06004358 RID: 17240 RVA: 0x00105150 File Offset: 0x00104550
		public virtual string FileExtensions
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandRegistryPermission();
				this.EnsureBuiltIn();
				StringBuilder stringBuilder = null;
				uint num = 0U;
				HRESULT.Check(UnsafeNativeMethods.WICBitmapCodecInfo.GetFileExtensions(this._codecInfoHandle, 0U, stringBuilder, out num));
				if (num > 0U)
				{
					stringBuilder = new StringBuilder((int)num);
					HRESULT.Check(UnsafeNativeMethods.WICBitmapCodecInfo.GetFileExtensions(this._codecInfoHandle, num, stringBuilder, out num));
				}
				if (stringBuilder != null)
				{
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		/// <summary>Obtém um valor que indica se o codec dá suporte a animação.</summary>
		/// <returns>
		///   <see langword="true" /> Se o codec dá suporte a animação; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06004359 RID: 17241 RVA: 0x001051B0 File Offset: 0x001045B0
		public virtual bool SupportsAnimation
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandRegistryPermission();
				this.EnsureBuiltIn();
				bool result;
				HRESULT.Check(UnsafeNativeMethods.WICBitmapCodecInfo.DoesSupportAnimation(this._codecInfoHandle, out result));
				return result;
			}
		}

		/// <summary>Obtém um valor que indica se o codec dá suporte sem perda de imagens.</summary>
		/// <returns>
		///   <see langword="true" /> Se o codec dá suporte sem perda de imagens; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x0600435A RID: 17242 RVA: 0x001051DC File Offset: 0x001045DC
		public virtual bool SupportsLossless
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandRegistryPermission();
				this.EnsureBuiltIn();
				bool result;
				HRESULT.Check(UnsafeNativeMethods.WICBitmapCodecInfo.DoesSupportLossless(this._codecInfoHandle, out result));
				return result;
			}
		}

		/// <summary>Obtém um valor que identifica se o codec dá suporte a vários quadros.</summary>
		/// <returns>
		///   <see langword="true" /> Se o codec dá suporte a vários quadros. Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x0600435B RID: 17243 RVA: 0x00105208 File Offset: 0x00104608
		public virtual bool SupportsMultipleFrames
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandRegistryPermission();
				this.EnsureBuiltIn();
				bool result;
				HRESULT.Check(UnsafeNativeMethods.WICBitmapCodecInfo.DoesSupportMultiframe(this._codecInfoHandle, out result));
				return result;
			}
		}

		// Token: 0x0600435C RID: 17244 RVA: 0x00105234 File Offset: 0x00104634
		private void EnsureBuiltIn()
		{
			if (!this._isBuiltIn)
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x04001867 RID: 6247
		private bool _isBuiltIn;

		// Token: 0x04001868 RID: 6248
		private SafeMILHandle _codecInfoHandle;
	}
}
