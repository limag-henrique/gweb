using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Markup;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Representa um dicionário de cadeias de caracteres que são usados para representar o nome de um objeto em idiomas diferentes.</summary>
	// Token: 0x02000379 RID: 889
	public sealed class LanguageSpecificStringDictionary : IDictionary<XmlLanguage, string>, ICollection<KeyValuePair<XmlLanguage, string>>, IEnumerable<KeyValuePair<XmlLanguage, string>>, IEnumerable, IDictionary, ICollection
	{
		// Token: 0x06001FF5 RID: 8181 RVA: 0x000828B4 File Offset: 0x00081CB4
		internal LanguageSpecificStringDictionary(IDictionary<XmlLanguage, string> innerDictionary)
		{
			this._innerDictionary = innerDictionary;
		}

		/// <summary>Retorna um enumerador que itera pela coleção.</summary>
		/// <returns>Um enumerador que itera por meio da coleção.</returns>
		// Token: 0x06001FF6 RID: 8182 RVA: 0x000828D0 File Offset: 0x00081CD0
		[CLSCompliant(false)]
		public IEnumerator<KeyValuePair<XmlLanguage, string>> GetEnumerator()
		{
			return this._innerDictionary.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
		/// <returns>Um objeto <see cref="T:System.Collections.IEnumerator" /> que pode ser usado para iterar pela coleção.</returns>
		// Token: 0x06001FF7 RID: 8183 RVA: 0x000828E8 File Offset: 0x00081CE8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new LanguageSpecificStringDictionary.EntryEnumerator(this._innerDictionary);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IDictionary.GetEnumerator" />.</summary>
		/// <returns>Um objeto <see cref="T:System.Collections.IEnumerator" /> que pode ser usado para iterar pela coleção.</returns>
		// Token: 0x06001FF8 RID: 8184 RVA: 0x00082900 File Offset: 0x00081D00
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new LanguageSpecificStringDictionary.EntryEnumerator(this._innerDictionary);
		}

		/// <summary>Recupera o valor de cadeia de caracteres no <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" /> para uma chave ou idioma especificado.</summary>
		/// <param name="key">Um valor do tipo <see cref="T:System.Windows.Markup.XmlLanguage" />.</param>
		/// <param name="value">Um valor do tipo <see cref="T:System.String" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" /> contiver uma entrada para <paramref name="key" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001FF9 RID: 8185 RVA: 0x00082918 File Offset: 0x00081D18
		public bool TryGetValue(XmlLanguage key, out string value)
		{
			return this._innerDictionary.TryGetValue(key, out value);
		}

		/// <summary>Obtém o número de cadeias de caracteres no <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" />.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Int32" /> que representa o número total de cadeias de caracteres.</returns>
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001FFA RID: 8186 RVA: 0x00082934 File Offset: 0x00081D34
		public int Count
		{
			get
			{
				return this._innerDictionary.Count;
			}
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" /> é somente leitura.</summary>
		/// <returns>
		///   <see langword="true" /> Se o dicionário for somente leitura; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x0008294C File Offset: 0x00081D4C
		public bool IsReadOnly
		{
			get
			{
				return this._innerDictionary.IsReadOnly;
			}
		}

		/// <summary>Adiciona um par chave/valor ao <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" />.</summary>
		/// <param name="item">Uma matriz de pares chave/valor. A chave é um objeto do tipo <see cref="T:System.Windows.Markup.XmlLanguage" />. O valor é uma cadeia de caracteres associada.</param>
		// Token: 0x06001FFC RID: 8188 RVA: 0x00082964 File Offset: 0x00081D64
		[CLSCompliant(false)]
		public void Add(KeyValuePair<XmlLanguage, string> item)
		{
			this.Add(item.Key, item.Value);
		}

		/// <summary>Remove todos os elementos da coleção.</summary>
		// Token: 0x06001FFD RID: 8189 RVA: 0x00082988 File Offset: 0x00081D88
		public void Clear()
		{
			this._innerDictionary.Clear();
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" /> contém o par chave/valor.</summary>
		/// <param name="item">O par chave/valor a localizar. A chave é um objeto do tipo <see cref="T:System.Windows.Markup.XmlLanguage" />. O valor é uma cadeia de caracteres associada.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver o par chave/valor; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001FFE RID: 8190 RVA: 0x000829A0 File Offset: 0x00081DA0
		[CLSCompliant(false)]
		public bool Contains(KeyValuePair<XmlLanguage, string> item)
		{
			return this._innerDictionary.Contains(item);
		}

		/// <summary>Copia os elementos do <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" /> para uma matriz, começando em um índice de matriz específico.</summary>
		/// <param name="array">A matriz de destino para a qual copiar.</param>
		/// <param name="index">O índice dentro no <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" /> de origem do qual começar a copiar.</param>
		// Token: 0x06001FFF RID: 8191 RVA: 0x000829BC File Offset: 0x00081DBC
		[CLSCompliant(false)]
		public void CopyTo(KeyValuePair<XmlLanguage, string>[] array, int index)
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
			if (this._innerDictionary.Count > array.Length - index)
			{
				throw new ArgumentException(SR.Get("Collection_CopyTo_NumberOfElementsExceedsArrayLength", new object[]
				{
					index,
					"array"
				}));
			}
			this._innerDictionary.CopyTo(array, index);
		}

		/// <summary>Remove da coleção o elemento com o par chave/valor especificado.</summary>
		/// <param name="item">O par chave/valor do elemento a ser removido.</param>
		/// <returns>
		///   <see langword="true" /> se o elemento for removido com êxito; caso contrário, <see langword="false" />. Esse método também retornará <see langword="false" /> se <paramref name="item" /> não for encontrado na coleção original.</returns>
		// Token: 0x06002000 RID: 8192 RVA: 0x00082A58 File Offset: 0x00081E58
		[CLSCompliant(false)]
		public bool Remove(KeyValuePair<XmlLanguage, string> item)
		{
			return this._innerDictionary.Remove(item);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x00082A74 File Offset: 0x00081E74
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" />.</returns>
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06002002 RID: 8194 RVA: 0x00082A84 File Offset: 0x00081E84
		object ICollection.SyncRoot
		{
			get
			{
				return this._innerDictionary;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x06002003 RID: 8195 RVA: 0x00082A98 File Offset: 0x00081E98
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
			if (this._innerDictionary.Count > array.Length - index)
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
				using (IEnumerator<KeyValuePair<XmlLanguage, string>> enumerator = this._innerDictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<XmlLanguage, string> keyValuePair = enumerator.Current;
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
			foreach (KeyValuePair<XmlLanguage, string> keyValuePair2 in this._innerDictionary)
			{
				array.SetValue(new DictionaryEntry(keyValuePair2.Key, keyValuePair2.Value), index++);
			}
		}

		/// <summary>Adiciona um idioma e uma cadeia de caracteres associada para o <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" />.</summary>
		/// <param name="key">Um valor do tipo <see cref="T:System.Windows.Markup.XmlLanguage" />.</param>
		/// <param name="value">Um valor do tipo <see cref="T:System.String" />.</param>
		// Token: 0x06002004 RID: 8196 RVA: 0x00082C64 File Offset: 0x00082064
		public void Add(XmlLanguage key, string value)
		{
			this._innerDictionary.Add(key, this.ValidateValue(value));
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" /> contém no idioma especificado.</summary>
		/// <param name="key">Um valor do tipo <see cref="T:System.Windows.Markup.XmlLanguage" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" /> contiver <paramref name="key" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002005 RID: 8197 RVA: 0x00082C84 File Offset: 0x00082084
		public bool ContainsKey(XmlLanguage key)
		{
			return this._innerDictionary.ContainsKey(key);
		}

		/// <summary>Remove o elemento de <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" /> com base no par chave-valor especificado.</summary>
		/// <param name="key">Um valor do tipo <see cref="T:System.Windows.Markup.XmlLanguage" />.</param>
		/// <returns>
		///   <see langword="true" /> se o elemento referenciado por <paramref name="key" /> tiver sido excluído com êxito; caso contrário <see langword="false" />.</returns>
		// Token: 0x06002006 RID: 8198 RVA: 0x00082CA0 File Offset: 0x000820A0
		public bool Remove(XmlLanguage key)
		{
			return this._innerDictionary.Remove(key);
		}

		/// <summary>Obtém ou define a cadeia de caracteres associada ao idioma especificado.</summary>
		/// <param name="key">Um valor do tipo <see cref="T:System.Windows.Markup.XmlLanguage" />.</param>
		/// <returns>Um valor do tipo <see cref="T:System.String" />.</returns>
		// Token: 0x17000662 RID: 1634
		public string this[XmlLanguage key]
		{
			get
			{
				return this._innerDictionary[key];
			}
			set
			{
				this._innerDictionary[key] = this.ValidateValue(value);
			}
		}

		/// <summary>Obtém uma coleção que contém as chaves ou objetos <see cref="T:System.Windows.Markup.XmlLanguage" /> no dicionário.</summary>
		/// <returns>Uma coleção de objetos do tipo <see cref="T:System.Windows.Markup.XmlLanguage" />.</returns>
		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06002009 RID: 8201 RVA: 0x00082CF8 File Offset: 0x000820F8
		[CLSCompliant(false)]
		public ICollection<XmlLanguage> Keys
		{
			get
			{
				return this._innerDictionary.Keys;
			}
		}

		/// <summary>Obtém uma coleção contendo os valores ou cadeias de caracteres no dicionário.</summary>
		/// <returns>Uma coleção que contém as cadeias de caracteres no dicionário.</returns>
		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x0600200A RID: 8202 RVA: 0x00082D10 File Offset: 0x00082110
		[CLSCompliant(false)]
		public ICollection<string> Values
		{
			get
			{
				return this._innerDictionary.Values;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IDictionary.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600200B RID: 8203 RVA: 0x00082D28 File Offset: 0x00082128
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
		// Token: 0x17000666 RID: 1638
		object IDictionary.this[object key]
		{
			get
			{
				XmlLanguage xmlLanguage = this.TryConvertKey(key);
				if (xmlLanguage == null)
				{
					return null;
				}
				return this._innerDictionary[xmlLanguage];
			}
			set
			{
				this._innerDictionary[this.ConvertKey(key)] = this.ConvertValue(value);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IDictionary.Keys" />.</summary>
		/// <returns>Um objeto <see cref="T:System.Collections.ICollection" /> que contém as chaves do objeto <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x0600200E RID: 8206 RVA: 0x00082D88 File Offset: 0x00082188
		ICollection IDictionary.Keys
		{
			get
			{
				return new LanguageSpecificStringDictionary.KeyCollection(this._innerDictionary);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IDictionary.Values" />.</summary>
		/// <returns>Um objeto <see cref="T:System.Collections.ICollection" /> que contém os valores no objeto <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x0600200F RID: 8207 RVA: 0x00082DA0 File Offset: 0x000821A0
		ICollection IDictionary.Values
		{
			get
			{
				return new LanguageSpecificStringDictionary.ValueCollection(this._innerDictionary);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IDictionary.Add(System.Object,System.Object)" />.</summary>
		/// <param name="key">O <see cref="T:System.Object" /> a ser usado como chave do elemento a ser adicionado.</param>
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" />.</param>
		// Token: 0x06002010 RID: 8208 RVA: 0x00082DB8 File Offset: 0x000821B8
		void IDictionary.Add(object key, object value)
		{
			this._innerDictionary.Add(this.ConvertKey(key), this.ConvertValue(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IDictionary.Contains(System.Object)" />.</summary>
		/// <param name="key">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002011 RID: 8209 RVA: 0x00082DE0 File Offset: 0x000821E0
		bool IDictionary.Contains(object key)
		{
			XmlLanguage xmlLanguage = this.TryConvertKey(key);
			return xmlLanguage != null && this._innerDictionary.ContainsKey(xmlLanguage);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IDictionary.Remove(System.Object)" />.</summary>
		/// <param name="key">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" />.</param>
		// Token: 0x06002012 RID: 8210 RVA: 0x00082E08 File Offset: 0x00082208
		void IDictionary.Remove(object key)
		{
			XmlLanguage xmlLanguage = this.TryConvertKey(key);
			if (xmlLanguage != null)
			{
				this._innerDictionary.Remove(xmlLanguage);
			}
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x00082E30 File Offset: 0x00082230
		private string ValidateValue(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return value;
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x00082E4C File Offset: 0x0008224C
		private string ConvertValue(object value)
		{
			string text = value as string;
			if (text != null)
			{
				return text;
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			throw new ArgumentException(SR.Get("UnexpectedParameterType", new object[]
			{
				value.GetType(),
				typeof(string)
			}), "value");
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x00082EA4 File Offset: 0x000822A4
		private XmlLanguage ConvertKey(object key)
		{
			XmlLanguage xmlLanguage = this.TryConvertKey(key);
			if (xmlLanguage != null)
			{
				return xmlLanguage;
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			throw new ArgumentException(SR.Get("CannotConvertType", new object[]
			{
				key.GetType(),
				typeof(XmlLanguage)
			}), "key");
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x00082EFC File Offset: 0x000822FC
		private XmlLanguage TryConvertKey(object key)
		{
			XmlLanguage xmlLanguage = key as XmlLanguage;
			if (xmlLanguage != null)
			{
				return xmlLanguage;
			}
			string text = key as string;
			if (text != null)
			{
				return XmlLanguage.GetLanguage(text);
			}
			return null;
		}

		// Token: 0x0400107F RID: 4223
		private IDictionary<XmlLanguage, string> _innerDictionary;

		// Token: 0x02000860 RID: 2144
		private class EntryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x0600571D RID: 22301 RVA: 0x00164760 File Offset: 0x00163B60
			internal EntryEnumerator(IDictionary<XmlLanguage, string> names)
			{
				this._innerDictionary = names;
				this._enumerator = names.GetEnumerator();
			}

			// Token: 0x0600571E RID: 22302 RVA: 0x00164788 File Offset: 0x00163B88
			public bool MoveNext()
			{
				return this._enumerator.MoveNext();
			}

			// Token: 0x0600571F RID: 22303 RVA: 0x001647A0 File Offset: 0x00163BA0
			public void Reset()
			{
				this._enumerator = this._innerDictionary.GetEnumerator();
			}

			// Token: 0x170011ED RID: 4589
			// (get) Token: 0x06005720 RID: 22304 RVA: 0x001647C0 File Offset: 0x00163BC0
			public virtual object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x06005721 RID: 22305 RVA: 0x001647D8 File Offset: 0x00163BD8
			private KeyValuePair<XmlLanguage, string> GetCurrentEntry()
			{
				KeyValuePair<XmlLanguage, string> result = this._enumerator.Current;
				if (result.Key == null)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_VerifyContext"));
				}
				return result;
			}

			// Token: 0x170011EE RID: 4590
			// (get) Token: 0x06005722 RID: 22306 RVA: 0x0016480C File Offset: 0x00163C0C
			public DictionaryEntry Entry
			{
				get
				{
					KeyValuePair<XmlLanguage, string> currentEntry = this.GetCurrentEntry();
					return new DictionaryEntry(currentEntry.Key, currentEntry.Value);
				}
			}

			// Token: 0x170011EF RID: 4591
			// (get) Token: 0x06005723 RID: 22307 RVA: 0x00164834 File Offset: 0x00163C34
			public object Key
			{
				get
				{
					return this.GetCurrentEntry().Key;
				}
			}

			// Token: 0x170011F0 RID: 4592
			// (get) Token: 0x06005724 RID: 22308 RVA: 0x00164850 File Offset: 0x00163C50
			public object Value
			{
				get
				{
					return this.GetCurrentEntry().Value;
				}
			}

			// Token: 0x04002853 RID: 10323
			protected IDictionary<XmlLanguage, string> _innerDictionary;

			// Token: 0x04002854 RID: 10324
			protected IEnumerator<KeyValuePair<XmlLanguage, string>> _enumerator;
		}

		// Token: 0x02000861 RID: 2145
		private abstract class BaseCollection : ICollection, IEnumerable
		{
			// Token: 0x06005725 RID: 22309 RVA: 0x0016486C File Offset: 0x00163C6C
			internal BaseCollection(IDictionary<XmlLanguage, string> names)
			{
				this._innerDictionary = names;
			}

			// Token: 0x170011F1 RID: 4593
			// (get) Token: 0x06005726 RID: 22310 RVA: 0x00164888 File Offset: 0x00163C88
			public int Count
			{
				get
				{
					return this._innerDictionary.Count;
				}
			}

			// Token: 0x06005727 RID: 22311 RVA: 0x001648A0 File Offset: 0x00163CA0
			public void CopyTo(Array array, int index)
			{
				foreach (object value in this)
				{
					array.SetValue(value, index++);
				}
			}

			// Token: 0x170011F2 RID: 4594
			// (get) Token: 0x06005728 RID: 22312 RVA: 0x00164904 File Offset: 0x00163D04
			public bool IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170011F3 RID: 4595
			// (get) Token: 0x06005729 RID: 22313 RVA: 0x00164914 File Offset: 0x00163D14
			public object SyncRoot
			{
				get
				{
					return this._innerDictionary;
				}
			}

			// Token: 0x0600572A RID: 22314
			public abstract IEnumerator GetEnumerator();

			// Token: 0x04002855 RID: 10325
			protected IDictionary<XmlLanguage, string> _innerDictionary;
		}

		// Token: 0x02000862 RID: 2146
		private class KeyCollection : LanguageSpecificStringDictionary.BaseCollection
		{
			// Token: 0x0600572B RID: 22315 RVA: 0x00164928 File Offset: 0x00163D28
			internal KeyCollection(IDictionary<XmlLanguage, string> names) : base(names)
			{
			}

			// Token: 0x0600572C RID: 22316 RVA: 0x0016493C File Offset: 0x00163D3C
			public override IEnumerator GetEnumerator()
			{
				return new LanguageSpecificStringDictionary.KeyCollection.KeyEnumerator(this._innerDictionary);
			}

			// Token: 0x02000A21 RID: 2593
			private class KeyEnumerator : LanguageSpecificStringDictionary.EntryEnumerator
			{
				// Token: 0x06005C19 RID: 23577 RVA: 0x00172044 File Offset: 0x00171444
				internal KeyEnumerator(IDictionary<XmlLanguage, string> names) : base(names)
				{
				}

				// Token: 0x170012DA RID: 4826
				// (get) Token: 0x06005C1A RID: 23578 RVA: 0x00172058 File Offset: 0x00171458
				public override object Current
				{
					get
					{
						return base.Key;
					}
				}
			}
		}

		// Token: 0x02000863 RID: 2147
		private class ValueCollection : LanguageSpecificStringDictionary.BaseCollection
		{
			// Token: 0x0600572D RID: 22317 RVA: 0x00164954 File Offset: 0x00163D54
			internal ValueCollection(IDictionary<XmlLanguage, string> names) : base(names)
			{
			}

			// Token: 0x0600572E RID: 22318 RVA: 0x00164968 File Offset: 0x00163D68
			public override IEnumerator GetEnumerator()
			{
				return new LanguageSpecificStringDictionary.ValueCollection.ValueEnumerator(this._innerDictionary);
			}

			// Token: 0x02000A22 RID: 2594
			private class ValueEnumerator : LanguageSpecificStringDictionary.EntryEnumerator
			{
				// Token: 0x06005C1B RID: 23579 RVA: 0x0017206C File Offset: 0x0017146C
				internal ValueEnumerator(IDictionary<XmlLanguage, string> names) : base(names)
				{
				}

				// Token: 0x170012DB RID: 4827
				// (get) Token: 0x06005C1C RID: 23580 RVA: 0x00172080 File Offset: 0x00171480
				public override object Current
				{
					get
					{
						return base.Value;
					}
				}
			}
		}
	}
}
