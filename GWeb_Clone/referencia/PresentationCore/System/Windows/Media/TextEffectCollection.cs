using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media
{
	/// <summary>Oferece suporte de coleção para uma coleção de objetos <see cref="T:System.Windows.Media.TextEffect" />.</summary>
	// Token: 0x020003F4 RID: 1012
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public sealed class TextEffectCollection : Animatable, IList, ICollection, IEnumerable, IList<TextEffect>, ICollection<TextEffect>, IEnumerable<TextEffect>
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.TextEffectCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060027D4 RID: 10196 RVA: 0x000A02B4 File Offset: 0x0009F6B4
		public new TextEffectCollection Clone()
		{
			return (TextEffectCollection)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.TextEffectCollection" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060027D5 RID: 10197 RVA: 0x000A02CC File Offset: 0x0009F6CC
		public new TextEffectCollection CloneCurrentValue()
		{
			return (TextEffectCollection)base.CloneCurrentValue();
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.TextEffect" /> ao final da coleção.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.TextEffect" /> a adicionar à coleção.</param>
		// Token: 0x060027D6 RID: 10198 RVA: 0x000A02E4 File Offset: 0x0009F6E4
		public void Add(TextEffect value)
		{
			this.AddHelper(value);
		}

		/// <summary>Remove todos os elementos da lista.</summary>
		// Token: 0x060027D7 RID: 10199 RVA: 0x000A02FC File Offset: 0x0009F6FC
		public void Clear()
		{
			base.WritePreamble();
			for (int i = this._collection.Count - 1; i >= 0; i--)
			{
				base.OnFreezablePropertyChanged(this._collection[i], null);
			}
			this._collection.Clear();
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Determina se o item especificado está na coleção.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.TextEffect" /> a ser localizado na coleção</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="value" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060027D8 RID: 10200 RVA: 0x000A035C File Offset: 0x0009F75C
		public bool Contains(TextEffect value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.TextEffect" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.TextEffect" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="value" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x060027D9 RID: 10201 RVA: 0x000A037C File Offset: 0x0009F77C
		public int IndexOf(TextEffect value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.TextEffect" /> em um local específico na coleção.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor <see cref="T:System.Windows.Media.TextEffect" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Windows.Media.TextEffect" /> a ser inserido na coleção.</param>
		// Token: 0x060027DA RID: 10202 RVA: 0x000A039C File Offset: 0x0009F79C
		public void Insert(int index, TextEffect value)
		{
			if (value == null)
			{
				throw new ArgumentException(SR.Get("Collection_NoNull"));
			}
			base.WritePreamble();
			base.OnFreezablePropertyChanged(null, value);
			this._collection.Insert(index, value);
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Remove o objeto <see cref="T:System.Windows.Media.TextEffect" /> especificado da coleção.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.TextEffect" /> a ser removido da coleção.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> tiver sido removido da coleção; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060027DB RID: 10203 RVA: 0x000A03EC File Offset: 0x0009F7EC
		public bool Remove(TextEffect value)
		{
			base.WritePreamble();
			int num = this.IndexOf(value);
			if (num >= 0)
			{
				TextEffect oldValue = this._collection[num];
				base.OnFreezablePropertyChanged(oldValue, null);
				this._collection.RemoveAt(num);
				this._version += 1U;
				base.WritePostscript();
				return true;
			}
			return false;
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.TextEffect" /> no índice especificado da coleção.</summary>
		/// <param name="index">O índice baseado em zero do <see cref="T:System.Windows.Media.TextEffect" /> a ser removido.</param>
		// Token: 0x060027DC RID: 10204 RVA: 0x000A0444 File Offset: 0x0009F844
		public void RemoveAt(int index)
		{
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x000A0460 File Offset: 0x0009F860
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			base.WritePreamble();
			TextEffect oldValue = this._collection[index];
			base.OnFreezablePropertyChanged(oldValue, null);
			this._collection.RemoveAt(index);
			this._version += 1U;
		}

		/// <summary>Obtém ou define o item armazenado como índice baseado em zero da coleção.</summary>
		/// <param name="index">O índice baseado em zero da coleção da qual o item deve ser obtido ou definido.</param>
		/// <returns>O elemento no índice especificado.</returns>
		// Token: 0x170007A4 RID: 1956
		public TextEffect this[int index]
		{
			get
			{
				base.ReadPreamble();
				return this._collection[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentException(SR.Get("Collection_NoNull"));
				}
				base.WritePreamble();
				if (this._collection[index] != value)
				{
					TextEffect oldValue = this._collection[index];
					base.OnFreezablePropertyChanged(oldValue, value);
					this._collection[index] = value;
				}
				this._version += 1U;
				base.WritePostscript();
			}
		}

		/// <summary>Obtém o número de elementos na coleção.</summary>
		/// <returns>O número de elementos na coleção.</returns>
		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x060027E0 RID: 10208 RVA: 0x000A0530 File Offset: 0x0009F930
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Copia todo o <see cref="T:System.Windows.Media.TextEffectCollection" /> para uma matriz unidimensional do tipo <see cref="T:System.Windows.Media.TextEffect" />, iniciando no índice especificado da matriz de destino.</summary>
		/// <param name="array">A matriz para a qual os itens da coleção devem ser copiados.</param>
		/// <param name="index">O índice da <paramref name="array" /> na qual começar a copiar o conteúdo do <see cref="T:System.Windows.Media.TextEffectCollection" />.</param>
		// Token: 0x060027E1 RID: 10209 RVA: 0x000A0550 File Offset: 0x0009F950
		public void CopyTo(TextEffect[] array, int index)
		{
			base.ReadPreamble();
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || index + this._collection.Count > array.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this._collection.CopyTo(array, index);
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x060027E2 RID: 10210 RVA: 0x000A05A0 File Offset: 0x0009F9A0
		bool ICollection<TextEffect>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Retorna um enumerador que pode iterar pela coleção.</summary>
		/// <returns>Um <see cref="T:System.Collections.IEnumerator" /> que pode iterar pela coleção.</returns>
		// Token: 0x060027E3 RID: 10211 RVA: 0x000A05BC File Offset: 0x0009F9BC
		public TextEffectCollection.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new TextEffectCollection.Enumerator(this);
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x000A05D8 File Offset: 0x0009F9D8
		IEnumerator<TextEffect> IEnumerable<TextEffect>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.TextEffectCollection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x060027E5 RID: 10213 RVA: 0x000A05F0 File Offset: 0x0009F9F0
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<TextEffect>)this).IsReadOnly;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.TextEffectCollection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x060027E6 RID: 10214 RVA: 0x000A0604 File Offset: 0x0009FA04
		bool IList.IsFixedSize
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <param name="index">O índice com base em zero do elemento a ser obtido ou definido.</param>
		/// <returns>O elemento no índice especificado.</returns>
		// Token: 0x170007A9 RID: 1961
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = this.Cast(value);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.TextEffectCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x060027E9 RID: 10217 RVA: 0x000A0650 File Offset: 0x0009FA50
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.TextEffectCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.TextEffectCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060027EA RID: 10218 RVA: 0x000A066C File Offset: 0x0009FA6C
		bool IList.Contains(object value)
		{
			return this.Contains(value as TextEffect);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.TextEffectCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x060027EB RID: 10219 RVA: 0x000A0688 File Offset: 0x0009FA88
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as TextEffect);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.TextEffectCollection" />.</param>
		// Token: 0x060027EC RID: 10220 RVA: 0x000A06A4 File Offset: 0x0009FAA4
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.TextEffectCollection" />.</param>
		// Token: 0x060027ED RID: 10221 RVA: 0x000A06C0 File Offset: 0x0009FAC0
		void IList.Remove(object value)
		{
			this.Remove(value as TextEffect);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.TextEffectCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x060027EE RID: 10222 RVA: 0x000A06DC File Offset: 0x0009FADC
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || index + this._collection.Count > array.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(SR.Get("Collection_BadRank"));
			}
			try
			{
				int count = this._collection.Count;
				for (int i = 0; i < count; i++)
				{
					array.SetValue(this._collection[i], index + i);
				}
			}
			catch (InvalidCastException innerException)
			{
				throw new ArgumentException(SR.Get("Collection_BadDestArray", new object[]
				{
					base.GetType().Name
				}), innerException);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.TextEffectCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x060027EF RID: 10223 RVA: 0x000A07B0 File Offset: 0x0009FBB0
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.TextEffectCollection" />.</returns>
		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x060027F0 RID: 10224 RVA: 0x000A07D8 File Offset: 0x0009FBD8
		object ICollection.SyncRoot
		{
			get
			{
				base.ReadPreamble();
				return this;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
		/// <returns>Um objeto <see cref="T:System.Collections.IEnumerator" /> que pode ser usado para iterar pela coleção.</returns>
		// Token: 0x060027F1 RID: 10225 RVA: 0x000A07EC File Offset: 0x0009FBEC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x060027F2 RID: 10226 RVA: 0x000A0804 File Offset: 0x0009FC04
		internal static TextEffectCollection Empty
		{
			get
			{
				if (TextEffectCollection.s_empty == null)
				{
					TextEffectCollection textEffectCollection = new TextEffectCollection();
					textEffectCollection.Freeze();
					TextEffectCollection.s_empty = textEffectCollection;
				}
				return TextEffectCollection.s_empty;
			}
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x000A0830 File Offset: 0x0009FC30
		internal TextEffect Internal_GetItem(int i)
		{
			return this._collection[i];
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x000A084C File Offset: 0x0009FC4C
		internal override void OnInheritanceContextChangedCore(EventArgs args)
		{
			base.OnInheritanceContextChangedCore(args);
			for (int i = 0; i < this.Count; i++)
			{
				DependencyObject dependencyObject = this._collection[i];
				if (dependencyObject != null && dependencyObject.InheritanceContext == this)
				{
					dependencyObject.OnInheritanceContextChanged(args);
				}
			}
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x000A0894 File Offset: 0x0009FC94
		private TextEffect Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is TextEffect))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"TextEffect"
				}));
			}
			return (TextEffect)value;
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x000A08F8 File Offset: 0x0009FCF8
		private int AddHelper(TextEffect value)
		{
			int result = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			return result;
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x000A0914 File Offset: 0x0009FD14
		internal int AddWithoutFiringPublicEvents(TextEffect value)
		{
			if (value == null)
			{
				throw new ArgumentException(SR.Get("Collection_NoNull"));
			}
			base.WritePreamble();
			base.OnFreezablePropertyChanged(null, value);
			int result = this._collection.Add(value);
			this._version += 1U;
			return result;
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x000A0964 File Offset: 0x0009FD64
		protected override Freezable CreateInstanceCore()
		{
			return new TextEffectCollection();
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x000A0978 File Offset: 0x0009FD78
		protected override void CloneCore(Freezable source)
		{
			TextEffectCollection textEffectCollection = (TextEffectCollection)source;
			base.CloneCore(source);
			int count = textEffectCollection._collection.Count;
			this._collection = new FrugalStructList<TextEffect>(count);
			for (int i = 0; i < count; i++)
			{
				TextEffect textEffect = textEffectCollection._collection[i].Clone();
				base.OnFreezablePropertyChanged(null, textEffect);
				this._collection.Add(textEffect);
			}
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x000A09E0 File Offset: 0x0009FDE0
		protected override void CloneCurrentValueCore(Freezable source)
		{
			TextEffectCollection textEffectCollection = (TextEffectCollection)source;
			base.CloneCurrentValueCore(source);
			int count = textEffectCollection._collection.Count;
			this._collection = new FrugalStructList<TextEffect>(count);
			for (int i = 0; i < count; i++)
			{
				TextEffect textEffect = textEffectCollection._collection[i].CloneCurrentValue();
				base.OnFreezablePropertyChanged(null, textEffect);
				this._collection.Add(textEffect);
			}
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x000A0A48 File Offset: 0x0009FE48
		protected override void GetAsFrozenCore(Freezable source)
		{
			TextEffectCollection textEffectCollection = (TextEffectCollection)source;
			base.GetAsFrozenCore(source);
			int count = textEffectCollection._collection.Count;
			this._collection = new FrugalStructList<TextEffect>(count);
			for (int i = 0; i < count; i++)
			{
				TextEffect textEffect = (TextEffect)textEffectCollection._collection[i].GetAsFrozen();
				base.OnFreezablePropertyChanged(null, textEffect);
				this._collection.Add(textEffect);
			}
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x000A0AB4 File Offset: 0x0009FEB4
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			TextEffectCollection textEffectCollection = (TextEffectCollection)source;
			base.GetCurrentValueAsFrozenCore(source);
			int count = textEffectCollection._collection.Count;
			this._collection = new FrugalStructList<TextEffect>(count);
			for (int i = 0; i < count; i++)
			{
				TextEffect textEffect = (TextEffect)textEffectCollection._collection[i].GetCurrentValueAsFrozen();
				base.OnFreezablePropertyChanged(null, textEffect);
				this._collection.Add(textEffect);
			}
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x000A0B20 File Offset: 0x0009FF20
		protected override bool FreezeCore(bool isChecking)
		{
			bool flag = base.FreezeCore(isChecking);
			int count = this._collection.Count;
			int num = 0;
			while (num < count && flag)
			{
				flag &= Freezable.Freeze(this._collection[num], isChecking);
				num++;
			}
			return flag;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextEffectCollection" />.</summary>
		// Token: 0x060027FE RID: 10238 RVA: 0x000A0B68 File Offset: 0x0009FF68
		public TextEffectCollection()
		{
			this._collection = default(FrugalStructList<TextEffect>);
		}

		/// <summary>Inicializa uma nova instância <see cref="T:System.Windows.Media.TextEffectCollection" /> que está vazia e tem a capacidade inicial especificada.</summary>
		/// <param name="capacity">O número de elementos que a nova coleção é capaz de armazenar inicialmente.</param>
		// Token: 0x060027FF RID: 10239 RVA: 0x000A0B88 File Offset: 0x0009FF88
		public TextEffectCollection(int capacity)
		{
			this._collection = new FrugalStructList<TextEffect>(capacity);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextEffectCollection" />.</summary>
		/// <param name="collection">Um enumerador do tipo <see cref="T:System.Collections.IEnumerable" />.</param>
		// Token: 0x06002800 RID: 10240 RVA: 0x000A0BA8 File Offset: 0x0009FFA8
		public TextEffectCollection(IEnumerable<TextEffect> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				bool flag = true;
				ICollection<TextEffect> collection2 = collection as ICollection<TextEffect>;
				if (collection2 != null)
				{
					this._collection = new FrugalStructList<TextEffect>(collection2);
				}
				else
				{
					ICollection collection3 = collection as ICollection;
					if (collection3 != null)
					{
						this._collection = new FrugalStructList<TextEffect>(collection3);
					}
					else
					{
						this._collection = default(FrugalStructList<TextEffect>);
						foreach (TextEffect textEffect in collection)
						{
							if (textEffect == null)
							{
								throw new ArgumentException(SR.Get("Collection_NoNull"));
							}
							TextEffect textEffect2 = textEffect;
							base.OnFreezablePropertyChanged(null, textEffect2);
							this._collection.Add(textEffect2);
						}
						flag = false;
					}
				}
				if (flag)
				{
					foreach (TextEffect textEffect3 in collection)
					{
						if (textEffect3 == null)
						{
							throw new ArgumentException(SR.Get("Collection_NoNull"));
						}
						base.OnFreezablePropertyChanged(null, textEffect3);
					}
				}
				base.WritePostscript();
				return;
			}
			throw new ArgumentNullException("collection");
		}

		// Token: 0x04001273 RID: 4723
		private static TextEffectCollection s_empty;

		// Token: 0x04001274 RID: 4724
		internal FrugalStructList<TextEffect> _collection;

		// Token: 0x04001275 RID: 4725
		internal uint _version;

		/// <summary>Enumera itens <see cref="T:System.Windows.Media.TextEffect" /> em um <see cref="T:System.Windows.Media.TextEffectCollection" />.</summary>
		// Token: 0x02000881 RID: 2177
		public struct Enumerator : IEnumerator, IEnumerator<TextEffect>, IDisposable
		{
			// Token: 0x060057BD RID: 22461 RVA: 0x001668E0 File Offset: 0x00165CE0
			internal Enumerator(TextEffectCollection list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = null;
			}

			/// <summary>Para obter uma descrição desses membros, consulte <see cref="M:System.IDisposable.Dispose" />.</summary>
			// Token: 0x060057BE RID: 22462 RVA: 0x00166910 File Offset: 0x00165D10
			void IDisposable.Dispose()
			{
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador foi avançado com êxito para o próximo elemento; caso contrário, <see langword="false" />.</returns>
			// Token: 0x060057BF RID: 22463 RVA: 0x00166920 File Offset: 0x00165D20
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					TextEffectCollection list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._current = list._collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Redefine o enumerador com sua posição inicial, que é antes do primeiro elemento da coleção.</summary>
			// Token: 0x060057C0 RID: 22464 RVA: 0x001669B4 File Offset: 0x00165DB4
			public void Reset()
			{
				this._list.ReadPreamble();
				if (this._version == this._list._version)
				{
					this._index = -1;
					return;
				}
				throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
			}

			/// <summary>Para obter uma descrição desses membros, consulte <see cref="P:System.Collections.IEnumerator.Current" />.</summary>
			/// <returns>O elemento atual na coleção.</returns>
			// Token: 0x1700121C RID: 4636
			// (get) Token: 0x060057C1 RID: 22465 RVA: 0x001669F8 File Offset: 0x00165DF8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém o elemento atual na coleção.</summary>
			/// <returns>O elemento atual na coleção.</returns>
			// Token: 0x1700121D RID: 4637
			// (get) Token: 0x060057C2 RID: 22466 RVA: 0x00166A0C File Offset: 0x00165E0C
			public TextEffect Current
			{
				get
				{
					if (this._index > -1)
					{
						return this._current;
					}
					if (this._index == -1)
					{
						throw new InvalidOperationException(SR.Get("Enumerator_NotStarted"));
					}
					throw new InvalidOperationException(SR.Get("Enumerator_ReachedEnd"));
				}
			}

			// Token: 0x040028B2 RID: 10418
			private TextEffect _current;

			// Token: 0x040028B3 RID: 10419
			private TextEffectCollection _list;

			// Token: 0x040028B4 RID: 10420
			private uint _version;

			// Token: 0x040028B5 RID: 10421
			private int _index;
		}
	}
}
