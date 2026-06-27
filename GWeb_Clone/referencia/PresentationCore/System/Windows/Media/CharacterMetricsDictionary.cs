using System;
using System.Collections;
using System.Collections.Generic;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Representa um dicionário de objetos <see cref="T:System.Windows.Media.CharacterMetrics" /> para uma fonte de dispositivo que é indexada por valores escalares Unicode.</summary>
	// Token: 0x0200037D RID: 893
	public sealed class CharacterMetricsDictionary : IDictionary<int, CharacterMetrics>, ICollection<KeyValuePair<int, CharacterMetrics>>, IEnumerable<KeyValuePair<int, CharacterMetrics>>, IEnumerable, IDictionary, ICollection
	{
		// Token: 0x0600203F RID: 8255 RVA: 0x000839A4 File Offset: 0x00082DA4
		internal CharacterMetricsDictionary()
		{
		}

		/// <summary>Retorna um enumerador que itera pela coleção.</summary>
		/// <returns>Um enumerador que itera por meio da coleção.</returns>
		// Token: 0x06002040 RID: 8256 RVA: 0x000839B8 File Offset: 0x00082DB8
		[CLSCompliant(false)]
		public IEnumerator<KeyValuePair<int, CharacterMetrics>> GetEnumerator()
		{
			return new CharacterMetricsDictionary.Enumerator(this);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
		/// <returns>Um objeto <see cref="T:System.Collections.IEnumerator" /> que pode ser usado para iterar pela coleção.</returns>
		// Token: 0x06002041 RID: 8257 RVA: 0x000839D0 File Offset: 0x00082DD0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new CharacterMetricsDictionary.Enumerator(this);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IDictionary.GetEnumerator" />.</summary>
		/// <returns>Um objeto <see cref="T:System.Collections.IEnumerator" /> que pode ser usado para iterar pela coleção.</returns>
		// Token: 0x06002042 RID: 8258 RVA: 0x000839E8 File Offset: 0x00082DE8
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new CharacterMetricsDictionary.Enumerator(this);
		}

		/// <summary>Recupera o valor <see cref="T:System.Windows.Media.CharacterMetrics" /> no dicionário para um valor de código de caractere especificado.</summary>
		/// <param name="key">Um valor do tipo <see cref="T:System.Int32" />.</param>
		/// <param name="value">Um valor do tipo <see cref="T:System.Windows.Media.CharacterMetrics" />.</param>
		/// <returns>
		///   <see langword="true" /> se o dicionário contiver uma entrada para <paramref name="key" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002043 RID: 8259 RVA: 0x00083A00 File Offset: 0x00082E00
		public bool TryGetValue(int key, out CharacterMetrics value)
		{
			value = this.GetValue(key);
			return value != null;
		}

		/// <summary>Obtém o número de elementos na coleção.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Int32" />.</returns>
		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06002044 RID: 8260 RVA: 0x00083A1C File Offset: 0x00082E1C
		public int Count
		{
			get
			{
				if (this._count == 0)
				{
					this._count = this.CountValues();
				}
				return this._count;
			}
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.Media.CharacterMetricsDictionary" /> é somente leitura.</summary>
		/// <returns>
		///   <see langword="true" /> Se o dicionário for somente leitura; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x00083A44 File Offset: 0x00082E44
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adiciona um código de caractere e o valor <see cref="T:System.Windows.Media.CharacterMetrics" /> associado à coleção usando um par chave-valor.</summary>
		/// <param name="item">O par chave-valor que representa o caractere de código e o valor <see cref="T:System.Windows.Media.CharacterMetrics" /> associado.</param>
		// Token: 0x06002046 RID: 8262 RVA: 0x00083A54 File Offset: 0x00082E54
		[CLSCompliant(false)]
		public void Add(KeyValuePair<int, CharacterMetrics> item)
		{
			this.SetValue(item.Key, item.Value, true);
		}

		/// <summary>Remove todos os elementos da coleção.</summary>
		// Token: 0x06002047 RID: 8263 RVA: 0x00083A78 File Offset: 0x00082E78
		public void Clear()
		{
			this._count = 0;
			this._pageTable = null;
		}

		/// <summary>Determina se a coleção contém o par chave-valor especificado.</summary>
		/// <param name="item">O par chave-valor que representa o caractere de código e o valor <see cref="T:System.Windows.Media.CharacterMetrics" /> associado.</param>
		/// <returns>
		///   <see langword="true" /> se o dicionário contiver o <see cref="T:System.Windows.Media.CharacterMetrics" /> representado pelo código do caractere em <paramref name="item" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002048 RID: 8264 RVA: 0x00083A94 File Offset: 0x00082E94
		[CLSCompliant(false)]
		public bool Contains(KeyValuePair<int, CharacterMetrics> item)
		{
			return item.Value != null && item.Value.Equals(this.GetValue(item.Key));
		}

		/// <summary>Copia os itens na coleção para uma matriz, começando em um índice de matriz específico.</summary>
		/// <param name="array">A matriz unidimensional que é o destino dos elementos copiados de <see cref="T:System.Windows.Media.CharacterMetricsDictionary" />.</param>
		/// <param name="index">O índice com base em zero em <paramref name="array" /> no qual a cópia começa.</param>
		// Token: 0x06002049 RID: 8265 RVA: 0x00083AC8 File Offset: 0x00082EC8
		[CLSCompliant(false)]
		public void CopyTo(KeyValuePair<int, CharacterMetrics>[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (index >= array.Length)
			{
				throw new ArgumentException(SR.Get("Collection_CopyTo_IndexGreaterThanOrEqualToArrayLength", new object[]
				{
					"index",
					"array"
				}));
			}
			CharacterMetrics[][] pageTable = this._pageTable;
			if (pageTable != null)
			{
				int num = index;
				for (int i = 0; i < pageTable.Length; i++)
				{
					CharacterMetrics[] array2 = pageTable[i];
					if (array2 != null)
					{
						for (int j = 0; j < array2.Length; j++)
						{
							CharacterMetrics characterMetrics = array2[j];
							if (characterMetrics != null)
							{
								if (num >= array.Length)
								{
									throw new ArgumentException(SR.Get("Collection_CopyTo_NumberOfElementsExceedsArrayLength", new object[]
									{
										index,
										"array"
									}));
								}
								array[num++] = new KeyValuePair<int, CharacterMetrics>(i << 8 | j, characterMetrics);
							}
						}
					}
				}
			}
		}

		/// <summary>Remove o elemento de <see cref="T:System.Windows.Media.CharacterMetricsDictionary" /> com base no par chave-valor especificado.</summary>
		/// <param name="item">O par chave-valor que representa o caractere de código e o valor <see cref="T:System.Windows.Media.CharacterMetrics" /> associado.</param>
		/// <returns>
		///   <see langword="true" /> se o item <see cref="T:System.Windows.Media.CharacterMetrics" /> foi removido com êxito; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600204A RID: 8266 RVA: 0x00083BA4 File Offset: 0x00082FA4
		[CLSCompliant(false)]
		public bool Remove(KeyValuePair<int, CharacterMetrics> item)
		{
			return item.Value != null && this.RemoveValue(item.Key, item.Value);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.CharacterMetricsDictionary" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x0600204B RID: 8267 RVA: 0x00083BD0 File Offset: 0x00082FD0
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.CharacterMetricsDictionary" />.</returns>
		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x0600204C RID: 8268 RVA: 0x00083BE0 File Offset: 0x00082FE0
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.CharacterMetricsDictionary" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x0600204D RID: 8269 RVA: 0x00083BF0 File Offset: 0x00082FF0
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (index >= array.Length)
			{
				throw new ArgumentException(SR.Get("Collection_CopyTo_IndexGreaterThanOrEqualToArrayLength", new object[]
				{
					"index",
					"array"
				}));
			}
			if (this.Count > array.Length - index)
			{
				throw new ArgumentException(SR.Get("Collection_CopyTo_NumberOfElementsExceedsArrayLength", new object[]
				{
					index,
					"array"
				}));
			}
			DictionaryEntry[] array2 = array as DictionaryEntry[];
			if (array2 != null)
			{
				using (IEnumerator<KeyValuePair<int, CharacterMetrics>> enumerator = this.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, CharacterMetrics> keyValuePair = enumerator.Current;
						array2[index++] = new DictionaryEntry(keyValuePair.Key, keyValuePair.Value);
					}
					return;
				}
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(SR.Get("Collection_CopyTo_ArrayCannotBeMultidimensional"));
			}
			Type elementType = array.GetType().GetElementType();
			if (!elementType.IsAssignableFrom(typeof(DictionaryEntry)))
			{
				throw new ArgumentException(SR.Get("CannotConvertType", new object[]
				{
					typeof(DictionaryEntry),
					elementType
				}));
			}
			foreach (KeyValuePair<int, CharacterMetrics> keyValuePair2 in this)
			{
				array.SetValue(new DictionaryEntry(keyValuePair2.Key, keyValuePair2.Value), index++);
			}
		}

		/// <summary>Adiciona um código de caractere e o valor <see cref="T:System.Windows.Media.CharacterMetrics" /> associado à coleção.</summary>
		/// <param name="key">Um valor do tipo <see cref="T:System.Int32" /> que representa o código de caractere.</param>
		/// <param name="value">Um valor do tipo <see cref="T:System.Windows.Media.CharacterMetrics" />.</param>
		// Token: 0x0600204E RID: 8270 RVA: 0x00083DB8 File Offset: 0x000831B8
		public void Add(int key, CharacterMetrics value)
		{
			this.SetValue(key, value, true);
		}

		/// <summary>Determina se a coleção contém o código de caractere especificado.</summary>
		/// <param name="key">Um valor do tipo <see cref="T:System.Int32" /> que representa o código de caractere.</param>
		/// <returns>
		///   <see langword="true" /> se o dicionário contém <paramref name="key" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600204F RID: 8271 RVA: 0x00083DD0 File Offset: 0x000831D0
		public bool ContainsKey(int key)
		{
			return this.GetValue(key) != null;
		}

		/// <summary>Remove o elemento de <see cref="T:System.Windows.Media.CharacterMetricsDictionary" /> com base no código de caractere especificado.</summary>
		/// <param name="key">Um valor do tipo <see cref="T:System.Int32" /> que representa o código de caractere.</param>
		/// <returns>
		///   <see langword="true" /> se o item <see cref="T:System.Windows.Media.CharacterMetrics" /> foi removido com êxito; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002050 RID: 8272 RVA: 0x00083DE8 File Offset: 0x000831E8
		public bool Remove(int key)
		{
			return this.RemoveValue(key, null);
		}

		/// <summary>Obtém ou define um valor para o item na coleção que corresponde a uma chave especificada.</summary>
		/// <param name="key">Um valor do tipo <see cref="T:System.Int32" />.</param>
		/// <returns>Um valor do tipo <see cref="T:System.Windows.Media.CharacterMetrics" />.</returns>
		// Token: 0x1700067D RID: 1661
		public CharacterMetrics this[int key]
		{
			get
			{
				return this.GetValue(key);
			}
			set
			{
				this.SetValue(key, value, false);
			}
		}

		/// <summary>Obtém uma coleção de códigos de caractere de <see cref="T:System.Windows.Media.CharacterMetricsDictionary" />.</summary>
		/// <returns>Uma coleção de chaves do tipo <see cref="T:System.Int32" />.</returns>
		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x00083E2C File Offset: 0x0008322C
		[CLSCompliant(false)]
		public ICollection<int> Keys
		{
			get
			{
				return this.GetKeys();
			}
		}

		/// <summary>Obtém uma coleção de valores <see cref="T:System.Windows.Media.CharacterMetrics" /> no <see cref="T:System.Windows.Media.CharacterMetricsDictionary" />.</summary>
		/// <returns>Uma coleção do tipo <see cref="T:System.Windows.Media.CharacterMetrics" />.</returns>
		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06002054 RID: 8276 RVA: 0x00083E40 File Offset: 0x00083240
		[CLSCompliant(false)]
		public ICollection<CharacterMetrics> Values
		{
			get
			{
				return this.GetValues();
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IDictionary.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.CharacterMetricsDictionary" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06002055 RID: 8277 RVA: 0x00083E54 File Offset: 0x00083254
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IDictionary.Item(System.Object)" />.</summary>
		/// <param name="key">A chave do elemento a ser obtida ou adicionada.</param>
		/// <returns>O elemento com a chave especificada.</returns>
		// Token: 0x17000681 RID: 1665
		object IDictionary.this[object key]
		{
			get
			{
				if (!(key is int))
				{
					return null;
				}
				return this.GetValue((int)key);
			}
			set
			{
				this.SetValue(CharacterMetricsDictionary.ConvertKey(key), this.ConvertValue(value), false);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IDictionary.Keys" />.</summary>
		/// <returns>Um objeto <see cref="T:System.Collections.ICollection" /> que contém as chaves do objeto <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06002058 RID: 8280 RVA: 0x00083EAC File Offset: 0x000832AC
		ICollection IDictionary.Keys
		{
			get
			{
				return this.GetKeys();
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IDictionary.Values" />.</summary>
		/// <returns>Um objeto <see cref="T:System.Collections.ICollection" /> que contém os valores no objeto <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x00083EC0 File Offset: 0x000832C0
		ICollection IDictionary.Values
		{
			get
			{
				return this.GetValues();
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IDictionary.Add(System.Object,System.Object)" />.</summary>
		/// <param name="key">O <see cref="T:System.Object" /> a ser usado como chave do elemento a ser adicionado.</param>
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.CharacterMetricsDictionary" />.</param>
		// Token: 0x0600205A RID: 8282 RVA: 0x00083ED4 File Offset: 0x000832D4
		void IDictionary.Add(object key, object value)
		{
			this.SetValue(CharacterMetricsDictionary.ConvertKey(key), this.ConvertValue(value), false);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IDictionary.Contains(System.Object)" />.</summary>
		/// <param name="key">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.CharacterMetricsDictionary" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.CharacterMetricsDictionary" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600205B RID: 8283 RVA: 0x00083EF8 File Offset: 0x000832F8
		bool IDictionary.Contains(object key)
		{
			return key is int && this.GetValue((int)key) != null;
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IDictionary.Remove(System.Object)" />.</summary>
		/// <param name="key">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.CharacterMetricsDictionary" />.</param>
		// Token: 0x0600205C RID: 8284 RVA: 0x00083F20 File Offset: 0x00083320
		void IDictionary.Remove(object key)
		{
			if (key is int)
			{
				this.RemoveValue((int)key, null);
			}
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x00083F44 File Offset: 0x00083344
		internal CharacterMetrics[] GetPage(int i)
		{
			if (this._pageTable == null)
			{
				return null;
			}
			return this._pageTable[i];
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x00083F64 File Offset: 0x00083364
		private CharacterMetrics[] GetPageFromUnicodeScalar(int unicodeScalar)
		{
			int num = unicodeScalar >> 8;
			CharacterMetrics[] array;
			if (this._pageTable != null)
			{
				array = this._pageTable[num];
				if (array == null)
				{
					array = (this._pageTable[num] = new CharacterMetrics[256]);
				}
			}
			else
			{
				this._pageTable = new CharacterMetrics[256][];
				array = (this._pageTable[num] = new CharacterMetrics[256]);
			}
			return array;
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x00083FC4 File Offset: 0x000833C4
		private void SetValue(int key, CharacterMetrics value, bool failIfExists)
		{
			if (key < 0 || key > 65535)
			{
				throw new ArgumentOutOfRangeException(SR.Get("CodePointOutOfRange", new object[]
				{
					key
				}));
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			CharacterMetrics[] pageFromUnicodeScalar = this.GetPageFromUnicodeScalar(key);
			int num = key & 255;
			if (failIfExists && pageFromUnicodeScalar[num] != null)
			{
				throw new ArgumentException(SR.Get("CollectionDuplicateKey", new object[]
				{
					key
				}));
			}
			pageFromUnicodeScalar[num] = value;
			this._count = 0;
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x0008404C File Offset: 0x0008344C
		internal CharacterMetrics GetValue(int key)
		{
			CharacterMetrics result = null;
			if (key >= 0 && key <= 1114111 && this._pageTable != null)
			{
				CharacterMetrics[] array = this._pageTable[key >> 8];
				if (array != null)
				{
					result = array[key & 255];
				}
			}
			return result;
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x00084088 File Offset: 0x00083488
		private bool RemoveValue(int key, CharacterMetrics value)
		{
			if (key >= 0 && key <= 1114111 && this._pageTable != null)
			{
				CharacterMetrics[] array = this._pageTable[key >> 8];
				if (array != null)
				{
					int num = key & 255;
					CharacterMetrics characterMetrics = array[num];
					if (characterMetrics != null && (value == null || characterMetrics.Equals(value)))
					{
						array[num] = null;
						this._count = 0;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x000840E0 File Offset: 0x000834E0
		private CharacterMetrics GetNextValue(ref int unicodeScalar)
		{
			CharacterMetrics[][] pageTable = this._pageTable;
			if (pageTable != null)
			{
				int i = unicodeScalar + 1 & 255;
				for (int j = unicodeScalar + 1 >> 8; j < 256; j++)
				{
					CharacterMetrics[] array = pageTable[j];
					if (array != null)
					{
						while (i < 256)
						{
							CharacterMetrics characterMetrics = array[i];
							if (characterMetrics != null)
							{
								unicodeScalar = (j << 8 | i);
								return characterMetrics;
							}
							i++;
						}
						i = 0;
					}
				}
			}
			unicodeScalar = int.MaxValue;
			return null;
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x0008414C File Offset: 0x0008354C
		private int CountValues()
		{
			int num = 0;
			CharacterMetrics[][] pageTable = this._pageTable;
			if (pageTable != null)
			{
				foreach (CharacterMetrics[] array in pageTable)
				{
					if (array != null)
					{
						for (int j = 0; j < array.Length; j++)
						{
							if (array[j] != null)
							{
								num++;
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x00084198 File Offset: 0x00083598
		private int[] GetKeys()
		{
			int[] array = new int[this.Count];
			int num = 0;
			foreach (KeyValuePair<int, CharacterMetrics> keyValuePair in this)
			{
				array[num++] = keyValuePair.Key;
			}
			return array;
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x00084204 File Offset: 0x00083604
		private CharacterMetrics[] GetValues()
		{
			CharacterMetrics[] array = new CharacterMetrics[this.Count];
			int num = 0;
			foreach (KeyValuePair<int, CharacterMetrics> keyValuePair in this)
			{
				array[num++] = keyValuePair.Value;
			}
			return array;
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x00084270 File Offset: 0x00083670
		internal static int ConvertKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			string text = key as string;
			int num2;
			if (text != null)
			{
				int num = 0;
				if (!FontFamilyMap.ParseHexNumber(text, ref num, out num2) || num < text.Length)
				{
					throw new ArgumentException(SR.Get("CannotConvertStringToType", new object[]
					{
						text,
						"int"
					}), "key");
				}
			}
			else
			{
				if (!(key is int))
				{
					throw new ArgumentException(SR.Get("CannotConvertType", new object[]
					{
						key.GetType(),
						"int"
					}), "key");
				}
				num2 = (int)key;
			}
			if (num2 < 0 || num2 > 1114111)
			{
				throw new ArgumentException(SR.Get("CodePointOutOfRange", new object[]
				{
					num2
				}), "key");
			}
			return num2;
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x00084344 File Offset: 0x00083744
		private CharacterMetrics ConvertValue(object value)
		{
			CharacterMetrics characterMetrics = value as CharacterMetrics;
			if (characterMetrics != null)
			{
				return characterMetrics;
			}
			if (value != null)
			{
				throw new ArgumentException(SR.Get("CannotConvertType", new object[]
				{
					typeof(CharacterMetrics),
					value.GetType()
				}));
			}
			throw new ArgumentNullException("value");
		}

		// Token: 0x04001094 RID: 4244
		internal const int LastDeviceFontCharacterCode = 65535;

		// Token: 0x04001095 RID: 4245
		internal const int PageShift = 8;

		// Token: 0x04001096 RID: 4246
		internal const int PageSize = 256;

		// Token: 0x04001097 RID: 4247
		internal const int PageMask = 255;

		// Token: 0x04001098 RID: 4248
		internal const int PageCount = 256;

		// Token: 0x04001099 RID: 4249
		private CharacterMetrics[][] _pageTable;

		// Token: 0x0400109A RID: 4250
		private int _count;

		// Token: 0x02000865 RID: 2149
		private struct Enumerator : IDictionaryEnumerator, IEnumerator, IEnumerator<KeyValuePair<int, CharacterMetrics>>, IDisposable
		{
			// Token: 0x0600572F RID: 22319 RVA: 0x00164980 File Offset: 0x00163D80
			internal Enumerator(CharacterMetricsDictionary dictionary)
			{
				this._dictionary = dictionary;
				this._unicodeScalar = -1;
				this._value = null;
			}

			// Token: 0x06005730 RID: 22320 RVA: 0x001649A4 File Offset: 0x00163DA4
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06005731 RID: 22321 RVA: 0x001649B4 File Offset: 0x00163DB4
			public bool MoveNext()
			{
				if (this._unicodeScalar < 2147483647)
				{
					this._value = this._dictionary.GetNextValue(ref this._unicodeScalar);
				}
				return this._value != null;
			}

			// Token: 0x06005732 RID: 22322 RVA: 0x001649F0 File Offset: 0x00163DF0
			void IEnumerator.Reset()
			{
				this._unicodeScalar = -1;
			}

			// Token: 0x170011F4 RID: 4596
			// (get) Token: 0x06005733 RID: 22323 RVA: 0x00164A04 File Offset: 0x00163E04
			object IEnumerator.Current
			{
				get
				{
					KeyValuePair<int, CharacterMetrics> currentEntry = this.GetCurrentEntry();
					return new DictionaryEntry(currentEntry.Key, currentEntry.Value);
				}
			}

			// Token: 0x170011F5 RID: 4597
			// (get) Token: 0x06005734 RID: 22324 RVA: 0x00164A38 File Offset: 0x00163E38
			public KeyValuePair<int, CharacterMetrics> Current
			{
				get
				{
					return new KeyValuePair<int, CharacterMetrics>(this._unicodeScalar, this._value);
				}
			}

			// Token: 0x06005735 RID: 22325 RVA: 0x00164A58 File Offset: 0x00163E58
			private KeyValuePair<int, CharacterMetrics> GetCurrentEntry()
			{
				if (this._value != null)
				{
					return new KeyValuePair<int, CharacterMetrics>(this._unicodeScalar, this._value);
				}
				throw new InvalidOperationException(SR.Get("Enumerator_VerifyContext"));
			}

			// Token: 0x170011F6 RID: 4598
			// (get) Token: 0x06005736 RID: 22326 RVA: 0x00164A90 File Offset: 0x00163E90
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					KeyValuePair<int, CharacterMetrics> currentEntry = this.GetCurrentEntry();
					return new DictionaryEntry(currentEntry.Key, currentEntry.Value);
				}
			}

			// Token: 0x170011F7 RID: 4599
			// (get) Token: 0x06005737 RID: 22327 RVA: 0x00164ABC File Offset: 0x00163EBC
			object IDictionaryEnumerator.Key
			{
				get
				{
					return this.GetCurrentEntry().Key;
				}
			}

			// Token: 0x170011F8 RID: 4600
			// (get) Token: 0x06005738 RID: 22328 RVA: 0x00164ADC File Offset: 0x00163EDC
			object IDictionaryEnumerator.Value
			{
				get
				{
					return this.GetCurrentEntry().Value;
				}
			}

			// Token: 0x0400285E RID: 10334
			private CharacterMetricsDictionary _dictionary;

			// Token: 0x0400285F RID: 10335
			private int _unicodeScalar;

			// Token: 0x04002860 RID: 10336
			private CharacterMetrics _value;
		}
	}
}
