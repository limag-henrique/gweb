using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Text;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Dá suporte para ler e gravar metadados para e de uma imagem de bitmap.</summary>
	// Token: 0x020005DD RID: 1501
	public class BitmapMetadata : ImageMetadata, IEnumerable, IEnumerable<string>
	{
		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" /> para uso com o formato de imagem especificado.</summary>
		/// <param name="containerFormat">O formato da imagem de bitmap, especificado como "gif", "jpg", "png" ou "tiff".</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="containerFormat" /> é <see langword="null" />.</exception>
		// Token: 0x0600440C RID: 17420 RVA: 0x00108F1C File Offset: 0x0010831C
		[SecurityCritical]
		public BitmapMetadata(string containerFormat)
		{
			if (containerFormat == null)
			{
				throw new ArgumentNullException("containerFormat");
			}
			Guid containerFormat2 = default(Guid);
			HRESULT.Check(UnsafeNativeMethods.WICCodec.WICMapShortNameToGuid(containerFormat, ref containerFormat2));
			this.Init(containerFormat2, false, false);
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x00108F68 File Offset: 0x00108368
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal BitmapMetadata()
		{
			this._metadataHandle = null;
			this._readOnly = true;
			this._fixedSize = false;
			this._blockWriter = null;
		}

		// Token: 0x0600440E RID: 17422 RVA: 0x00108FA4 File Offset: 0x001083A4
		[SecurityCritical]
		internal BitmapMetadata(SafeMILHandle metadataHandle, bool readOnly, bool fixedSize, object syncObject)
		{
			this._metadataHandle = metadataHandle;
			this._readOnly = readOnly;
			this._fixedSize = fixedSize;
			this._blockWriter = null;
			this._syncObject = syncObject;
		}

		// Token: 0x0600440F RID: 17423 RVA: 0x00108FE8 File Offset: 0x001083E8
		internal BitmapMetadata(BitmapMetadata bitmapMetadata)
		{
			if (bitmapMetadata == null)
			{
				throw new ArgumentNullException("bitmapMetadata");
			}
			this.Init(bitmapMetadata.GuidFormat, false, bitmapMetadata._fixedSize);
		}

		// Token: 0x06004410 RID: 17424 RVA: 0x00109028 File Offset: 0x00108428
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void Init(Guid containerFormat, bool readOnly, bool fixedSize)
		{
			int hr = 0;
			IntPtr zero = IntPtr.Zero;
			using (FactoryMaker factoryMaker = new FactoryMaker())
			{
				Guid guid = new Guid(MILGuidData.GUID_VendorMicrosoft);
				hr = UnsafeNativeMethods.WICImagingFactory.CreateQueryWriter(factoryMaker.ImagingFactoryPtr, ref containerFormat, ref guid, out zero);
			}
			if (HRESULT.Succeeded(hr))
			{
				this._readOnly = readOnly;
				this._fixedSize = fixedSize;
				this._blockWriter = null;
				this._metadataHandle = new SafeMILHandle(zero);
				this._syncObject = this._metadataHandle;
				return;
			}
			this.InitializeFromBlockWriter(containerFormat, readOnly, fixedSize);
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x001090C8 File Offset: 0x001084C8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void InitializeFromBlockWriter(Guid containerFormat, bool readOnly, bool fixedSize)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr zero = IntPtr.Zero;
			using (FactoryMaker factoryMaker = new FactoryMaker())
			{
				try
				{
					this._blockWriter = new BitmapMetadata.BitmapMetadataBlockWriter(containerFormat, fixedSize);
					intPtr = Marshal.GetComInterfaceForObject(this._blockWriter, typeof(BitmapMetadata.IWICMetadataBlockWriter));
					HRESULT.Check(UnsafeNativeMethods.WICComponentFactory.CreateQueryWriterFromBlockWriter(factoryMaker.ImagingFactoryPtr, intPtr, ref zero));
					this._readOnly = readOnly;
					this._fixedSize = fixedSize;
					this._metadataHandle = new SafeMILHandle(zero);
					zero = IntPtr.Zero;
					this._syncObject = this._metadataHandle;
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						UnsafeNativeMethods.MILUnknown.Release(intPtr);
					}
					if (zero != IntPtr.Zero)
					{
						UnsafeNativeMethods.MILUnknown.Release(zero);
					}
				}
			}
		}

		// Token: 0x06004412 RID: 17426 RVA: 0x001091B4 File Offset: 0x001085B4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void InitializeFromBlockWriter(BitmapMetadata.BitmapMetadataBlockWriter sourceBlockWriter, object syncObject)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr zero = IntPtr.Zero;
			using (FactoryMaker factoryMaker = new FactoryMaker())
			{
				try
				{
					this._blockWriter = new BitmapMetadata.BitmapMetadataBlockWriter(sourceBlockWriter, syncObject);
					intPtr = Marshal.GetComInterfaceForObject(this._blockWriter, typeof(BitmapMetadata.IWICMetadataBlockWriter));
					HRESULT.Check(UnsafeNativeMethods.WICComponentFactory.CreateQueryWriterFromBlockWriter(factoryMaker.ImagingFactoryPtr, intPtr, ref zero));
					this._readOnly = false;
					this._fixedSize = false;
					this._metadataHandle = new SafeMILHandle(zero);
					zero = IntPtr.Zero;
					this._syncObject = this._metadataHandle;
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						UnsafeNativeMethods.MILUnknown.Release(intPtr);
					}
					if (zero != IntPtr.Zero)
					{
						UnsafeNativeMethods.MILUnknown.Release(zero);
					}
				}
			}
		}

		// Token: 0x06004413 RID: 17427 RVA: 0x001092A0 File Offset: 0x001086A0
		[SecurityCritical]
		private void InitializeFromMetadataWriter(SafeMILHandle metadataHandle, object syncObject)
		{
			IntPtr zero = IntPtr.Zero;
			Guid guid = new Guid(MILGuidData.GUID_VendorMicrosoft);
			try
			{
				int hr;
				using (FactoryMaker factoryMaker = new FactoryMaker())
				{
					lock (syncObject)
					{
						hr = UnsafeNativeMethods.WICImagingFactory.CreateQueryWriterFromReader(factoryMaker.ImagingFactoryPtr, metadataHandle, ref guid, out zero);
					}
				}
				if (HRESULT.Succeeded(hr))
				{
					this._readOnly = false;
					this._fixedSize = false;
					this._blockWriter = null;
					this._metadataHandle = new SafeMILHandle(zero);
					zero = IntPtr.Zero;
					this._syncObject = this._metadataHandle;
				}
				else if (!HRESULT.IsWindowsCodecError(hr))
				{
					HRESULT.Check(hr);
				}
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					UnsafeNativeMethods.MILUnknown.Release(zero);
				}
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" />, fazendo cópias em profundidade dos valores do objeto.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true." /></returns>
		// Token: 0x06004414 RID: 17428 RVA: 0x001093AC File Offset: 0x001087AC
		public new BitmapMetadata Clone()
		{
			return (BitmapMetadata)base.Clone();
		}

		/// <summary>Uma implementação de <see cref="M:System.Windows.Freezable.CreateInstance" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06004415 RID: 17429 RVA: 0x001093C4 File Offset: 0x001087C4
		protected override Freezable CreateInstanceCore()
		{
			return new BitmapMetadata();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" /> especificado.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" /> a ser clonado.</param>
		// Token: 0x06004416 RID: 17430 RVA: 0x001093D8 File Offset: 0x001087D8
		protected override void CloneCore(Freezable sourceFreezable)
		{
			BitmapMetadata sourceBitmapMetadata = (BitmapMetadata)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(sourceBitmapMetadata);
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" /> a ser clonado.</param>
		// Token: 0x06004417 RID: 17431 RVA: 0x001093FC File Offset: 0x001087FC
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			BitmapMetadata sourceBitmapMetadata = (BitmapMetadata)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(sourceBitmapMetadata);
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" /> a ser clonado e congelado.</param>
		// Token: 0x06004418 RID: 17432 RVA: 0x00109420 File Offset: 0x00108820
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			BitmapMetadata sourceBitmapMetadata = (BitmapMetadata)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			this.CopyCommon(sourceBitmapMetadata);
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" /> a ser copiado e congelado.</param>
		// Token: 0x06004419 RID: 17433 RVA: 0x00109444 File Offset: 0x00108844
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			BitmapMetadata sourceBitmapMetadata = (BitmapMetadata)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			this.CopyCommon(sourceBitmapMetadata);
		}

		/// <summary>Obtém um valor que identifica o formato da imagem.</summary>
		/// <returns>O formato da imagem bitmap.</returns>
		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x0600441A RID: 17434 RVA: 0x00109468 File Offset: 0x00108868
		public string Format
		{
			[SecurityCritical]
			get
			{
				this.EnsureBitmapMetadata();
				StringBuilder stringBuilder = null;
				uint num = 0U;
				Guid guidFormat = this.GuidFormat;
				object syncObject = this._syncObject;
				lock (syncObject)
				{
					HRESULT.Check(UnsafeNativeMethods.WICCodec.WICMapGuidToShortName(ref guidFormat, 0U, stringBuilder, ref num));
					if (num > 0U)
					{
						stringBuilder = new StringBuilder((int)num);
						HRESULT.Check(UnsafeNativeMethods.WICCodec.WICMapGuidToShortName(ref guidFormat, num, stringBuilder, ref num));
					}
				}
				return stringBuilder.ToString();
			}
		}

		/// <summary>Obtém um valor que identifica o local base dos metadados associados a uma imagem.</summary>
		/// <returns>O local base dos metadados de imagem.</returns>
		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x0600441B RID: 17435 RVA: 0x001094F4 File Offset: 0x001088F4
		public string Location
		{
			[SecurityCritical]
			get
			{
				StringBuilder stringBuilder = null;
				uint num = 0U;
				this.EnsureBitmapMetadata();
				object syncObject = this._syncObject;
				lock (syncObject)
				{
					HRESULT.Check(UnsafeNativeMethods.WICMetadataQueryReader.GetLocation(this._metadataHandle, 0U, stringBuilder, out num));
					if (num > 0U)
					{
						stringBuilder = new StringBuilder((int)num);
						HRESULT.Check(UnsafeNativeMethods.WICMetadataQueryReader.GetLocation(this._metadataHandle, num, stringBuilder, out num));
					}
				}
				return stringBuilder.ToString();
			}
		}

		/// <summary>Obtém um valor que indica se os metadados associados a uma imagem são somente leitura.</summary>
		/// <returns>
		///   <see langword="true" /> Se os metadados são somente leitura; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x0600441C RID: 17436 RVA: 0x00109580 File Offset: 0x00108980
		public bool IsReadOnly
		{
			get
			{
				this.EnsureBitmapMetadata();
				return this._readOnly;
			}
		}

		/// <summary>Obtém um valor que determina se o objeto de <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" /> tem um tamanho fixo.</summary>
		/// <returns>
		///   <see langword="true" /> Se o <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" /> é fixo; caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x0600441D RID: 17437 RVA: 0x0010959C File Offset: 0x0010899C
		public bool IsFixedSize
		{
			get
			{
				this.EnsureBitmapMetadata();
				return this._fixedSize;
			}
		}

		/// <summary>Fornece acesso a um gravador de consultas de metadados que pode gravar metadados em um arquivo de imagem de bitmap.</summary>
		/// <param name="query">Identifica o local dos metadados a serem gravados.</param>
		/// <param name="value">O valor dos metadados a serem gravados.</param>
		// Token: 0x0600441E RID: 17438 RVA: 0x001095B8 File Offset: 0x001089B8
		[SecurityCritical]
		public void SetQuery(string query, object value)
		{
			base.WritePreamble();
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this._readOnly)
			{
				throw new InvalidOperationException(SR.Get("Image_MetadataReadOnly"));
			}
			this._setQueryString = query;
			this._setQueryValue = value;
			this.EnsureBitmapMetadata();
			PROPVARIANT propvariant = default(PROPVARIANT);
			try
			{
				propvariant.Init(value);
				if (propvariant.RequiresSyncObject)
				{
					BitmapMetadata bitmapMetadata = value as BitmapMetadata;
					Invariant.Assert(bitmapMetadata != null);
					bitmapMetadata.VerifyAccess();
					object syncObject = bitmapMetadata._syncObject;
					lock (syncObject)
					{
						object syncObject2 = this._syncObject;
						lock (syncObject2)
						{
							HRESULT.Check(UnsafeNativeMethods.WICMetadataQueryWriter.SetMetadataByName(this._metadataHandle, query, ref propvariant));
							goto IL_10A;
						}
					}
				}
				object syncObject3 = this._syncObject;
				lock (syncObject3)
				{
					HRESULT.Check(UnsafeNativeMethods.WICMetadataQueryWriter.SetMetadataByName(this._metadataHandle, query, ref propvariant));
				}
			}
			finally
			{
				propvariant.Clear();
			}
			IL_10A:
			base.WritePostscript();
		}

		/// <summary>Fornece acesso a um leitor de consultas de metadados que pode extrair metadados de um arquivo de imagem de bitmap.</summary>
		/// <param name="query">Identifica a cadeia de caracteres que está sendo consultada no objeto <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" /> atual.</param>
		/// <returns>Os metadados no local de consulta especificado.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="query" /> é <see langword="null" />.</exception>
		// Token: 0x0600441F RID: 17439 RVA: 0x0010973C File Offset: 0x00108B3C
		[SecurityCritical]
		public object GetQuery(string query)
		{
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}
			this.EnsureBitmapMetadata();
			PROPVARIANT propvariant = default(PROPVARIANT);
			try
			{
				propvariant.Init(null);
				object syncObject = this._syncObject;
				int metadataByName;
				lock (syncObject)
				{
					metadataByName = UnsafeNativeMethods.WICMetadataQueryReader.GetMetadataByName(this._metadataHandle, query, ref propvariant);
				}
				if (metadataByName != -2003292352)
				{
					HRESULT.Check(metadataByName);
					object obj = propvariant.ToObject(this._syncObject);
					if (base.IsFrozenInternal)
					{
						BitmapMetadata bitmapMetadata = obj as BitmapMetadata;
						if (bitmapMetadata != null)
						{
							bitmapMetadata.Freeze();
						}
					}
					return obj;
				}
			}
			finally
			{
				propvariant.Clear();
			}
			return null;
		}

		/// <summary>Remove uma consulta de metadados de uma instância de <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" />.</summary>
		/// <param name="query">A consulta de metadados a ser removida.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="query" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">Ocorre quando os metadados de imagem são somente leitura.</exception>
		// Token: 0x06004420 RID: 17440 RVA: 0x00109818 File Offset: 0x00108C18
		[SecurityCritical]
		public void RemoveQuery(string query)
		{
			base.WritePreamble();
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}
			if (this._readOnly)
			{
				throw new InvalidOperationException(SR.Get("Image_MetadataReadOnly"));
			}
			this.EnsureBitmapMetadata();
			object syncObject = this._syncObject;
			int num;
			lock (syncObject)
			{
				num = UnsafeNativeMethods.WICMetadataQueryWriter.RemoveMetadataByName(this._metadataHandle, query);
			}
			if (num != -2003292352)
			{
				HRESULT.Check(num);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
		/// <returns>Um objeto <see cref="T:System.Collections.IEnumerator" /> que pode ser usado para iterar pela coleção.</returns>
		// Token: 0x06004421 RID: 17441 RVA: 0x001098AC File Offset: 0x00108CAC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		IEnumerator IEnumerable.GetEnumerator()
		{
			this.EnsureBitmapMetadata();
			return new BitmapMetadataEnumerator(this._metadataHandle);
		}

		// Token: 0x06004422 RID: 17442 RVA: 0x001098D0 File Offset: 0x00108CD0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		IEnumerator<string> IEnumerable<string>.GetEnumerator()
		{
			this.EnsureBitmapMetadata();
			return new BitmapMetadataEnumerator(this._metadataHandle);
		}

		/// <summary>Determina se uma cadeia de caracteres de consulta existe dentro de um objeto de <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" />.</summary>
		/// <param name="query">Identifica a cadeia de caracteres que está sendo consultada no objeto <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" /> atual.</param>
		/// <returns>
		///   <see langword="true" /> se a cadeia de caracteres de consulta é encontrada no repositório de metadados; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="query" /> é <see langword="null" />.</exception>
		// Token: 0x06004423 RID: 17443 RVA: 0x001098F4 File Offset: 0x00108CF4
		[SecurityCritical]
		public bool ContainsQuery(string query)
		{
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}
			this.EnsureBitmapMetadata();
			object syncObject = this._syncObject;
			int hr;
			lock (syncObject)
			{
				hr = UnsafeNativeMethods.WICMetadataQueryReader.ContainsMetadataByName(this._metadataHandle, query, IntPtr.Zero);
			}
			if (HRESULT.IsWindowsCodecError(hr))
			{
				return false;
			}
			HRESULT.Check(hr);
			return true;
		}

		/// <summary>Obtém ou define um valor que representa o autor de uma imagem.</summary>
		/// <returns>O autor da imagem.</returns>
		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06004424 RID: 17444 RVA: 0x00109974 File Offset: 0x00108D74
		// (set) Token: 0x06004425 RID: 17445 RVA: 0x001099A0 File Offset: 0x00108DA0
		public ReadOnlyCollection<string> Author
		{
			get
			{
				string[] array = this.GetQuery("System.Author") as string[];
				if (array != null)
				{
					return new ReadOnlyCollection<string>(array);
				}
				return null;
			}
			set
			{
				string[] array = null;
				if (value != null)
				{
					array = new string[value.Count];
					value.CopyTo(array, 0);
				}
				this.SetQuery("System.Author", array);
			}
		}

		/// <summary>Obtém ou define um valor que identifica o título de um arquivo de imagem.</summary>
		/// <returns>O título de um arquivo de imagem.</returns>
		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06004426 RID: 17446 RVA: 0x001099D4 File Offset: 0x00108DD4
		// (set) Token: 0x06004427 RID: 17447 RVA: 0x001099F4 File Offset: 0x00108DF4
		public string Title
		{
			get
			{
				return this.GetQuery("System.Title") as string;
			}
			set
			{
				this.SetQuery("System.Title", value);
			}
		}

		/// <summary>Obtém ou define um valor que identifica a classificação da imagem.</summary>
		/// <returns>O valor de classificação entre 0 e 5.</returns>
		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06004428 RID: 17448 RVA: 0x00109A10 File Offset: 0x00108E10
		// (set) Token: 0x06004429 RID: 17449 RVA: 0x00109A50 File Offset: 0x00108E50
		public int Rating
		{
			get
			{
				object query = this.GetQuery("System.SimpleRating");
				if (query != null && query.GetType() == typeof(ushort))
				{
					return Convert.ToInt32(query, CultureInfo.InvariantCulture);
				}
				return 0;
			}
			set
			{
				this.SetQuery("System.SimpleRating", Convert.ToUInt16(value, CultureInfo.InvariantCulture));
			}
		}

		/// <summary>Obtém ou define um valor que indica o assunto da imagem bitmap.</summary>
		/// <returns>O assunto da imagem bitmap.</returns>
		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x0600442A RID: 17450 RVA: 0x00109A80 File Offset: 0x00108E80
		// (set) Token: 0x0600442B RID: 17451 RVA: 0x00109AA0 File Offset: 0x00108EA0
		public string Subject
		{
			get
			{
				return this.GetQuery("System.Subject") as string;
			}
			set
			{
				this.SetQuery("System.Subject", value);
			}
		}

		/// <summary>Obtém ou define um valor que representa um comentário associado ao arquivo de imagem.</summary>
		/// <returns>Um comentário associado ao arquivo de imagem.</returns>
		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x0600442C RID: 17452 RVA: 0x00109ABC File Offset: 0x00108EBC
		// (set) Token: 0x0600442D RID: 17453 RVA: 0x00109ADC File Offset: 0x00108EDC
		public string Comment
		{
			get
			{
				return this.GetQuery("System.Comment") as string;
			}
			set
			{
				this.SetQuery("System.Comment", value);
			}
		}

		/// <summary>Obtém ou define um valor que indica a data em que a imagem foi capturada.</summary>
		/// <returns>A data em que a imagem foi capturada.</returns>
		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x0600442E RID: 17454 RVA: 0x00109AF8 File Offset: 0x00108EF8
		// (set) Token: 0x0600442F RID: 17455 RVA: 0x00109B54 File Offset: 0x00108F54
		public string DateTaken
		{
			get
			{
				object query = this.GetQuery("System.Photo.DateTaken");
				if (query != null && query.GetType() == typeof(System.Runtime.InteropServices.ComTypes.FILETIME))
				{
					System.Runtime.InteropServices.ComTypes.FILETIME filetime = (System.Runtime.InteropServices.ComTypes.FILETIME)query;
					return DateTime.FromFileTime(((long)filetime.dwHighDateTime << 32) + (long)((ulong)filetime.dwLowDateTime)).ToString();
				}
				return null;
			}
			set
			{
				DateTime dateTime = Convert.ToDateTime(value, CultureInfo.InvariantCulture);
				PROPVARIANT propvariant = default(PROPVARIANT);
				propvariant.varType = 64;
				long num = dateTime.ToFileTime();
				propvariant.filetime.dwLowDateTime = (int)num;
				propvariant.filetime.dwHighDateTime = (int)(num >> 32 & (long)((ulong)-1));
				object value2 = propvariant.ToObject(this._syncObject);
				this.SetQuery("System.Photo.DateTaken", value2);
			}
		}

		/// <summary>Obtém ou define um valor que identifica o nome do aplicativo que foi usado para construir ou alterar um arquivo de imagem.</summary>
		/// <returns>O nome do aplicativo que foi usado para construir ou alterar um arquivo de imagem.</returns>
		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06004430 RID: 17456 RVA: 0x00109BC4 File Offset: 0x00108FC4
		// (set) Token: 0x06004431 RID: 17457 RVA: 0x00109BE4 File Offset: 0x00108FE4
		public string ApplicationName
		{
			get
			{
				return this.GetQuery("System.ApplicationName") as string;
			}
			set
			{
				this.SetQuery("System.ApplicationName", value);
			}
		}

		/// <summary>Obtém ou define um valor que indica as informações de direitos autorais que estão associadas ao arquivo de imagem.</summary>
		/// <returns>As informações de direitos autorais que está associadas com o arquivo de imagem.</returns>
		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06004432 RID: 17458 RVA: 0x00109C00 File Offset: 0x00109000
		// (set) Token: 0x06004433 RID: 17459 RVA: 0x00109C20 File Offset: 0x00109020
		public string Copyright
		{
			get
			{
				return this.GetQuery("System.Copyright") as string;
			}
			set
			{
				this.SetQuery("System.Copyright", value);
			}
		}

		/// <summary>Obtém ou define um valor que identifica o fabricante da câmera que está associada a uma imagem.</summary>
		/// <returns>O fabricante da câmera que está associada a uma imagem.</returns>
		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06004434 RID: 17460 RVA: 0x00109C3C File Offset: 0x0010903C
		// (set) Token: 0x06004435 RID: 17461 RVA: 0x00109C5C File Offset: 0x0010905C
		public string CameraManufacturer
		{
			get
			{
				return this.GetQuery("System.Photo.CameraManufacturer") as string;
			}
			set
			{
				this.SetQuery("System.Photo.CameraManufacturer", value);
			}
		}

		/// <summary>Obtém ou define um valor que identifica o modelo de câmera que foi usado para capturar a imagem.</summary>
		/// <returns>O modelo de câmera que foi usado para capturar a imagem.</returns>
		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06004436 RID: 17462 RVA: 0x00109C78 File Offset: 0x00109078
		// (set) Token: 0x06004437 RID: 17463 RVA: 0x00109C98 File Offset: 0x00109098
		public string CameraModel
		{
			get
			{
				return this.GetQuery("System.Photo.CameraModel") as string;
			}
			set
			{
				this.SetQuery("System.Photo.CameraModel", value);
			}
		}

		/// <summary>Obtém ou define uma coleção de palavras-chave que descrevem a imagem de bitmap.</summary>
		/// <returns>Uma coleção somente leitura de cadeias de caracteres.</returns>
		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06004438 RID: 17464 RVA: 0x00109CB4 File Offset: 0x001090B4
		// (set) Token: 0x06004439 RID: 17465 RVA: 0x00109CE0 File Offset: 0x001090E0
		public ReadOnlyCollection<string> Keywords
		{
			get
			{
				string[] array = this.GetQuery("System.Keywords") as string[];
				if (array != null)
				{
					return new ReadOnlyCollection<string>(array);
				}
				return null;
			}
			set
			{
				string[] array = null;
				if (value != null)
				{
					array = new string[value.Count];
					value.CopyTo(array, 0);
				}
				this.SetQuery("System.Keywords", array);
			}
		}

		// Token: 0x0600443A RID: 17466 RVA: 0x00109D14 File Offset: 0x00109114
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void CopyCommon(BitmapMetadata sourceBitmapMetadata)
		{
			BitmapMetadata.BitmapMetadataBlockWriter blockWriter = sourceBitmapMetadata.BlockWriter;
			if (blockWriter == null)
			{
				this.InitializeFromMetadataWriter(sourceBitmapMetadata._metadataHandle, sourceBitmapMetadata._syncObject);
			}
			if (this._metadataHandle == null)
			{
				if (blockWriter != null)
				{
					this.InitializeFromBlockWriter(blockWriter, sourceBitmapMetadata._syncObject);
				}
				else
				{
					this.InitializeFromBlockWriter(sourceBitmapMetadata.GuidFormat, false, false);
					this.SetQuery("/", sourceBitmapMetadata);
				}
			}
			this._fixedSize = sourceBitmapMetadata._fixedSize;
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x0600443B RID: 17467 RVA: 0x00109D80 File Offset: 0x00109180
		internal Guid GuidFormat
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				Guid result = default(Guid);
				this.EnsureBitmapMetadata();
				HRESULT.Check(UnsafeNativeMethods.WICMetadataQueryReader.GetContainerFormat(this._metadataHandle, out result));
				return result;
			}
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x0600443C RID: 17468 RVA: 0x00109DB0 File Offset: 0x001091B0
		internal SafeMILHandle InternalMetadataHandle
		{
			[SecurityCritical]
			get
			{
				return this._metadataHandle;
			}
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x0600443D RID: 17469 RVA: 0x00109DC4 File Offset: 0x001091C4
		internal object SyncObject
		{
			get
			{
				return this._syncObject;
			}
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x0600443E RID: 17470 RVA: 0x00109DD8 File Offset: 0x001091D8
		internal BitmapMetadata.BitmapMetadataBlockWriter BlockWriter
		{
			get
			{
				return this._blockWriter;
			}
		}

		// Token: 0x0600443F RID: 17471 RVA: 0x00109DEC File Offset: 0x001091EC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void EnsureBitmapMetadata()
		{
			base.ReadPreamble();
			if (this._metadataHandle == null)
			{
				throw new InvalidOperationException(SR.Get("Image_MetadataInitializationIncomplete"));
			}
		}

		// Token: 0x040018CC RID: 6348
		private const string policy_Author = "System.Author";

		// Token: 0x040018CD RID: 6349
		private const string policy_Title = "System.Title";

		// Token: 0x040018CE RID: 6350
		private const string policy_Subject = "System.Subject";

		// Token: 0x040018CF RID: 6351
		private const string policy_Comment = "System.Comment";

		// Token: 0x040018D0 RID: 6352
		private const string policy_Keywords = "System.Keywords";

		// Token: 0x040018D1 RID: 6353
		private const string policy_DateTaken = "System.Photo.DateTaken";

		// Token: 0x040018D2 RID: 6354
		private const string policy_ApplicationName = "System.ApplicationName";

		// Token: 0x040018D3 RID: 6355
		private const string policy_Copyright = "System.Copyright";

		// Token: 0x040018D4 RID: 6356
		private const string policy_CameraManufacturer = "System.Photo.CameraManufacturer";

		// Token: 0x040018D5 RID: 6357
		private const string policy_CameraModel = "System.Photo.CameraModel";

		// Token: 0x040018D6 RID: 6358
		private const string policy_Rating = "System.SimpleRating";

		// Token: 0x040018D7 RID: 6359
		[SecurityCritical]
		private SafeMILHandle _metadataHandle;

		// Token: 0x040018D8 RID: 6360
		private BitmapMetadata.BitmapMetadataBlockWriter _blockWriter;

		// Token: 0x040018D9 RID: 6361
		private bool _readOnly;

		// Token: 0x040018DA RID: 6362
		private bool _fixedSize;

		// Token: 0x040018DB RID: 6363
		private object _setQueryValue;

		// Token: 0x040018DC RID: 6364
		private string _setQueryString;

		// Token: 0x040018DD RID: 6365
		private object _syncObject = new object();

		// Token: 0x020008D2 RID: 2258
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("FEAA2A8D-B3F3-43E4-B25C-D1DE990A1AE1")]
		[ComImport]
		internal interface IWICMetadataBlockReader
		{
			// Token: 0x060058D2 RID: 22738
			[PreserveSig]
			int GetContainerFormat(out Guid containerFormat);

			// Token: 0x060058D3 RID: 22739
			[PreserveSig]
			int GetCount(out uint count);

			// Token: 0x060058D4 RID: 22740
			[PreserveSig]
			int GetReaderByIndex(uint index, out IntPtr ppIMetadataReader);

			// Token: 0x060058D5 RID: 22741
			[PreserveSig]
			int GetEnumerator(out IntPtr pIEnumMetadata);
		}

		// Token: 0x020008D3 RID: 2259
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("08FB9676-B444-41E8-8DBE-6A53A542BFF1")]
		[ComImport]
		internal interface IWICMetadataBlockWriter : BitmapMetadata.IWICMetadataBlockReader
		{
			// Token: 0x060058D6 RID: 22742
			[PreserveSig]
			int GetContainerFormat(out Guid containerFormat);

			// Token: 0x060058D7 RID: 22743
			[PreserveSig]
			int GetCount(out uint count);

			// Token: 0x060058D8 RID: 22744
			[PreserveSig]
			int GetReaderByIndex(uint index, out IntPtr ppIMetadataReader);

			// Token: 0x060058D9 RID: 22745
			[PreserveSig]
			int GetEnumerator(out IntPtr pIEnumMetadata);

			// Token: 0x060058DA RID: 22746
			[PreserveSig]
			int InitializeFromBlockReader(IntPtr pIBlockReader);

			// Token: 0x060058DB RID: 22747
			[PreserveSig]
			int GetWriterByIndex(uint index, out IntPtr ppIMetadataWriter);

			// Token: 0x060058DC RID: 22748
			[PreserveSig]
			int AddWriter(IntPtr pIMetadataWriter);

			// Token: 0x060058DD RID: 22749
			[PreserveSig]
			int SetWriterByIndex(uint index, IntPtr pIMetadataWriter);

			// Token: 0x060058DE RID: 22750
			[PreserveSig]
			int RemoveWriterByIndex(uint index);
		}

		// Token: 0x020008D4 RID: 2260
		[ClassInterface(ClassInterfaceType.None)]
		internal sealed class BitmapMetadataBlockWriter : BitmapMetadata.IWICMetadataBlockWriter, BitmapMetadata.IWICMetadataBlockReader
		{
			// Token: 0x060058DF RID: 22751 RVA: 0x00168AA8 File Offset: 0x00167EA8
			internal BitmapMetadataBlockWriter(Guid containerFormat, bool fixedSize)
			{
				this._fixedSize = fixedSize;
				this._containerFormat = containerFormat;
				this._metadataBlocks = new ArrayList();
			}

			// Token: 0x060058E0 RID: 22752 RVA: 0x00168AD4 File Offset: 0x00167ED4
			[SecurityCritical]
			[SecurityTreatAsSafe]
			internal BitmapMetadataBlockWriter(BitmapMetadata.BitmapMetadataBlockWriter blockWriter, object syncObject)
			{
				Guid guid = new Guid(MILGuidData.GUID_VendorMicrosoft);
				this._fixedSize = blockWriter._fixedSize;
				this._containerFormat = blockWriter._containerFormat;
				this._metadataBlocks = new ArrayList();
				ArrayList metadataBlocks = blockWriter.MetadataBlocks;
				using (FactoryMaker factoryMaker = new FactoryMaker())
				{
					foreach (object obj in metadataBlocks)
					{
						SafeMILHandle pIMetadataReader = (SafeMILHandle)obj;
						lock (syncObject)
						{
							IntPtr zero = IntPtr.Zero;
							try
							{
								HRESULT.Check(UnsafeNativeMethods.WICComponentFactory.CreateMetadataWriterFromReader(factoryMaker.ImagingFactoryPtr, pIMetadataReader, ref guid, out zero));
								SafeMILHandle value = new SafeMILHandle(zero);
								zero = IntPtr.Zero;
								this._metadataBlocks.Add(value);
							}
							finally
							{
								if (zero != IntPtr.Zero)
								{
									UnsafeNativeMethods.MILUnknown.Release(zero);
								}
							}
						}
					}
				}
			}

			// Token: 0x060058E1 RID: 22753 RVA: 0x00168C38 File Offset: 0x00168038
			public int GetContainerFormat(out Guid containerFormat)
			{
				containerFormat = this._containerFormat;
				return 0;
			}

			// Token: 0x060058E2 RID: 22754 RVA: 0x00168C54 File Offset: 0x00168054
			public int GetCount(out uint count)
			{
				count = (uint)this._metadataBlocks.Count;
				return 0;
			}

			// Token: 0x060058E3 RID: 22755 RVA: 0x00168C70 File Offset: 0x00168070
			[SecurityCritical]
			public int GetReaderByIndex(uint index, out IntPtr pIMetadataReader)
			{
				if ((ulong)index >= (ulong)((long)this._metadataBlocks.Count))
				{
					pIMetadataReader = IntPtr.Zero;
					return -2003292352;
				}
				SafeMILHandle pIUnknown = (SafeMILHandle)this._metadataBlocks[(int)index];
				Guid iid_IWICMetadataReader = MILGuidData.IID_IWICMetadataReader;
				return UnsafeNativeMethods.MILUnknown.QueryInterface(pIUnknown, ref iid_IWICMetadataReader, out pIMetadataReader);
			}

			// Token: 0x060058E4 RID: 22756 RVA: 0x00168CBC File Offset: 0x001680BC
			[SecurityCritical]
			public int GetEnumerator(out IntPtr pIEnumMetadata)
			{
				BitmapMetadata.BitmapMetadataBlockWriterEnumerator o = new BitmapMetadata.BitmapMetadataBlockWriterEnumerator(this);
				pIEnumMetadata = Marshal.GetComInterfaceForObject(o, typeof(BitmapMetadata.IEnumUnknown));
				return 0;
			}

			// Token: 0x060058E5 RID: 22757 RVA: 0x00168CE4 File Offset: 0x001680E4
			[SecurityCritical]
			public int InitializeFromBlockReader(IntPtr pIBlockReader)
			{
				Invariant.Assert(pIBlockReader != IntPtr.Zero);
				int num = 0;
				uint num2 = 0U;
				Guid guid = new Guid(MILGuidData.GUID_VendorMicrosoft);
				ArrayList arrayList = new ArrayList();
				num = UnsafeNativeMethods.WICMetadataBlockReader.GetCount(pIBlockReader, out num2);
				if (HRESULT.Succeeded(num))
				{
					using (FactoryMaker factoryMaker = new FactoryMaker())
					{
						for (uint num3 = 0U; num3 < num2; num3 += 1U)
						{
							SafeMILHandle safeMILHandle = null;
							IntPtr zero = IntPtr.Zero;
							try
							{
								num = UnsafeNativeMethods.WICMetadataBlockReader.GetReaderByIndex(pIBlockReader, num3, out safeMILHandle);
								if (HRESULT.Failed(num))
								{
									break;
								}
								num = UnsafeNativeMethods.WICComponentFactory.CreateMetadataWriterFromReader(factoryMaker.ImagingFactoryPtr, safeMILHandle, ref guid, out zero);
								if (HRESULT.Failed(num))
								{
									break;
								}
								SafeMILHandle value = new SafeMILHandle(zero);
								zero = IntPtr.Zero;
								arrayList.Add(value);
							}
							finally
							{
								if (safeMILHandle != null)
								{
									safeMILHandle.Dispose();
								}
								if (zero != IntPtr.Zero)
								{
									UnsafeNativeMethods.MILUnknown.Release(zero);
								}
							}
						}
					}
					this._metadataBlocks = arrayList;
				}
				return num;
			}

			// Token: 0x060058E6 RID: 22758 RVA: 0x00168E00 File Offset: 0x00168200
			[SecurityCritical]
			public int GetWriterByIndex(uint index, out IntPtr pIMetadataWriter)
			{
				if ((ulong)index >= (ulong)((long)this._metadataBlocks.Count))
				{
					pIMetadataWriter = IntPtr.Zero;
					return -2003292352;
				}
				SafeMILHandle pIUnknown = (SafeMILHandle)this._metadataBlocks[(int)index];
				Guid iid_IWICMetadataWriter = MILGuidData.IID_IWICMetadataWriter;
				return UnsafeNativeMethods.MILUnknown.QueryInterface(pIUnknown, ref iid_IWICMetadataWriter, out pIMetadataWriter);
			}

			// Token: 0x060058E7 RID: 22759 RVA: 0x00168E4C File Offset: 0x0016824C
			[SecurityCritical]
			public int AddWriter(IntPtr pIMetadataWriter)
			{
				if (pIMetadataWriter == IntPtr.Zero)
				{
					return -2147483645;
				}
				if (this._fixedSize && this._metadataBlocks.Count > 0)
				{
					return -2003292287;
				}
				SafeMILHandle safeMILHandle = new SafeMILHandle(pIMetadataWriter);
				UnsafeNativeMethods.MILUnknown.AddRef(safeMILHandle);
				this._metadataBlocks.Add(safeMILHandle);
				return 0;
			}

			// Token: 0x060058E8 RID: 22760 RVA: 0x00168EA4 File Offset: 0x001682A4
			[SecurityCritical]
			public int SetWriterByIndex(uint index, IntPtr pIMetadataWriter)
			{
				if ((ulong)index >= (ulong)((long)this._metadataBlocks.Count))
				{
					return -2003292352;
				}
				if (pIMetadataWriter == IntPtr.Zero)
				{
					return -2147483645;
				}
				if (this._fixedSize)
				{
					return -2003292287;
				}
				SafeMILHandle safeMILHandle = new SafeMILHandle(pIMetadataWriter);
				UnsafeNativeMethods.MILUnknown.AddRef(safeMILHandle);
				this._metadataBlocks[(int)index] = safeMILHandle;
				return 0;
			}

			// Token: 0x060058E9 RID: 22761 RVA: 0x00168F04 File Offset: 0x00168304
			public int RemoveWriterByIndex(uint index)
			{
				if ((ulong)index >= (ulong)((long)this._metadataBlocks.Count))
				{
					return -2003292352;
				}
				if (this._fixedSize)
				{
					return -2003292287;
				}
				this._metadataBlocks.Remove(index);
				return 0;
			}

			// Token: 0x1700125C RID: 4700
			// (get) Token: 0x060058EA RID: 22762 RVA: 0x00168F48 File Offset: 0x00168348
			internal ArrayList MetadataBlocks
			{
				get
				{
					return this._metadataBlocks;
				}
			}

			// Token: 0x0400298C RID: 10636
			private bool _fixedSize;

			// Token: 0x0400298D RID: 10637
			private Guid _containerFormat;

			// Token: 0x0400298E RID: 10638
			private ArrayList _metadataBlocks;
		}

		// Token: 0x020008D5 RID: 2261
		[Guid("00000100-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IEnumUnknown
		{
			// Token: 0x060058EB RID: 22763
			[PreserveSig]
			int Next(uint celt, out IntPtr rgelt, ref uint pceltFetched);

			// Token: 0x060058EC RID: 22764
			[PreserveSig]
			int Skip(uint celt);

			// Token: 0x060058ED RID: 22765
			[PreserveSig]
			int Reset();

			// Token: 0x060058EE RID: 22766
			[PreserveSig]
			int Clone(ref IntPtr ppEnum);
		}

		// Token: 0x020008D6 RID: 2262
		[ClassInterface(ClassInterfaceType.None)]
		internal sealed class BitmapMetadataBlockWriterEnumerator : BitmapMetadata.IEnumUnknown
		{
			// Token: 0x060058EF RID: 22767 RVA: 0x00168F5C File Offset: 0x0016835C
			internal BitmapMetadataBlockWriterEnumerator(BitmapMetadata.BitmapMetadataBlockWriter blockWriter)
			{
				this._metadataBlocks = blockWriter.MetadataBlocks;
				this._index = 0U;
			}

			// Token: 0x060058F0 RID: 22768 RVA: 0x00168F84 File Offset: 0x00168384
			[SecurityCritical]
			public int Next(uint celt, out IntPtr rgelt, ref uint pceltFetched)
			{
				if (celt > 1U)
				{
					rgelt = IntPtr.Zero;
					pceltFetched = 0U;
					return -2003292287;
				}
				if ((ulong)this._index >= (ulong)((long)this._metadataBlocks.Count) || celt == 0U)
				{
					rgelt = IntPtr.Zero;
					pceltFetched = 0U;
					return 1;
				}
				SafeMILHandle pIUnknown = (SafeMILHandle)this._metadataBlocks[(int)this._index];
				Guid iid_IWICMetadataReader = MILGuidData.IID_IWICMetadataReader;
				int num = UnsafeNativeMethods.MILUnknown.QueryInterface(pIUnknown, ref iid_IWICMetadataReader, out rgelt);
				if (HRESULT.Succeeded(num))
				{
					pceltFetched = 1U;
					this._index += 1U;
				}
				return num;
			}

			// Token: 0x060058F1 RID: 22769 RVA: 0x0016900C File Offset: 0x0016840C
			[SecurityTreatAsSafe]
			[SecurityCritical]
			public int Skip(uint celt)
			{
				uint num = this._index + celt;
				if ((ulong)num > (ulong)((long)this._metadataBlocks.Count))
				{
					this._index = (uint)this._metadataBlocks.Count;
					return 1;
				}
				this._index += celt;
				return 0;
			}

			// Token: 0x060058F2 RID: 22770 RVA: 0x00169054 File Offset: 0x00168454
			[SecurityCritical]
			[SecurityTreatAsSafe]
			public int Reset()
			{
				this._index = 0U;
				return 0;
			}

			// Token: 0x060058F3 RID: 22771 RVA: 0x0016906C File Offset: 0x0016846C
			[SecurityCritical]
			public int Clone(ref IntPtr ppEnum)
			{
				ppEnum = IntPtr.Zero;
				return -2003292287;
			}

			// Token: 0x0400298F RID: 10639
			private ArrayList _metadataBlocks;

			// Token: 0x04002990 RID: 10640
			private uint _index;
		}
	}
}
