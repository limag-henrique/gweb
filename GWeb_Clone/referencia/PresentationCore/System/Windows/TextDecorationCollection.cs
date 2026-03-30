using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media.Animation;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows
{
	/// <summary>Representa uma coleção de instâncias <see cref="T:System.Windows.TextDecoration" />.</summary>
	// Token: 0x020001F2 RID: 498
	[TypeConverter(typeof(TextDecorationCollectionConverter))]
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public sealed class TextDecorationCollection : Animatable, IList, ICollection, IEnumerable, IList<TextDecoration>, ICollection<TextDecoration>, IEnumerable<TextDecoration>
	{
		// Token: 0x06000CE8 RID: 3304 RVA: 0x00030C50 File Offset: 0x00030050
		[FriendAccessAllowed]
		internal bool ValueEquals(TextDecorationCollection textDecorations)
		{
			if (textDecorations == null)
			{
				return false;
			}
			if (this == textDecorations)
			{
				return true;
			}
			if (this.Count != textDecorations.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Count; i++)
			{
				if (!this[i].ValueEquals(textDecorations[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Adiciona um <see cref="T:System.Collections.Generic.IEnumerable`1" /> genérico à coleção.</summary>
		/// <param name="textDecorations">Um <see cref="T:System.Collections.Generic.IEnumerable`1" /> genérico do tipo <see cref="T:System.Windows.TextDecoration" />.</param>
		// Token: 0x06000CE9 RID: 3305 RVA: 0x00030CA4 File Offset: 0x000300A4
		[CLSCompliant(false)]
		public void Add(IEnumerable<TextDecoration> textDecorations)
		{
			if (textDecorations == null)
			{
				throw new ArgumentNullException("textDecorations");
			}
			foreach (TextDecoration value in textDecorations)
			{
				this.Add(value);
			}
		}

		/// <summary>Remove uma coleção de <see cref="T:System.Windows.TextDecorations" /> da coleção atual e retorna a (nova) coleção resultante.</summary>
		/// <param name="textDecorations">A coleção a ser removida</param>
		/// <param name="result">O parâmetro de saída que contém o resultado. Se nenhum elemento foi removido da coleção atual, <see langword="result" /> será uma nova coleção idêntica à original.</param>
		/// <returns>
		///   <see langword="true" /> se pelo menos um item foi removido da coleção atual; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000CEA RID: 3306 RVA: 0x00030D08 File Offset: 0x00030108
		public bool TryRemove(IEnumerable<TextDecoration> textDecorations, out TextDecorationCollection result)
		{
			if (textDecorations == null)
			{
				throw new ArgumentNullException("textDecorations");
			}
			bool result2 = false;
			result = this.Clone();
			foreach (TextDecoration textDecoration in textDecorations)
			{
				for (int i = result.Count - 1; i >= 0; i--)
				{
					if (result[i].ValueEquals(textDecoration))
					{
						result.RemoveAt(i);
						result2 = true;
					}
				}
			}
			return result2;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.TextDecorationCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06000CEB RID: 3307 RVA: 0x00030D9C File Offset: 0x0003019C
		public new TextDecorationCollection Clone()
		{
			return (TextDecorationCollection)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.TextDecorationCollection" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06000CEC RID: 3308 RVA: 0x00030DB4 File Offset: 0x000301B4
		public new TextDecorationCollection CloneCurrentValue()
		{
			return (TextDecorationCollection)base.CloneCurrentValue();
		}

		/// <summary>Insere o objeto <see cref="T:System.Windows.TextDecoration" /> especificado na coleção.</summary>
		/// <param name="value">O objeto <see cref="T:System.Windows.TextDecoration" /> a ser inserido.</param>
		// Token: 0x06000CED RID: 3309 RVA: 0x00030DCC File Offset: 0x000301CC
		public void Add(TextDecoration value)
		{
			this.AddHelper(value);
		}

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.TextDecoration" /> de <see cref="T:System.Windows.TextDecorationCollection" />.</summary>
		// Token: 0x06000CEE RID: 3310 RVA: 0x00030DE4 File Offset: 0x000301E4
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

		/// <summary>Determina se a <see cref="T:System.Windows.TextDecorationCollection" /> contém a <see cref="T:System.Windows.TextDecoration" /> especificada.</summary>
		/// <param name="value">O objeto <see cref="T:System.Windows.TextDecoration" /> a ser localizado.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> está na coleção; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000CEF RID: 3311 RVA: 0x00030E44 File Offset: 0x00030244
		public bool Contains(TextDecoration value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Retorna o índice do objeto <see cref="T:System.Windows.TextDecoration" /> especificado na coleção.</summary>
		/// <param name="value">O objeto <see cref="T:System.Windows.TextDecoration" /> a ser localizado.</param>
		/// <returns>O índice de base zero de <paramref name="value" />, se encontrado; caso contrário, -1;</returns>
		// Token: 0x06000CF0 RID: 3312 RVA: 0x00030E64 File Offset: 0x00030264
		public int IndexOf(TextDecoration value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Insere o objeto <see cref="T:System.Windows.TextDecoration" /> especificado na posição do índice indicada na coleção.</summary>
		/// <param name="index">A posição de índice de base zero para inserir o objeto.</param>
		/// <param name="value">O objeto <see cref="T:System.Windows.TextDecoration" /> a ser inserido.</param>
		// Token: 0x06000CF1 RID: 3313 RVA: 0x00030E84 File Offset: 0x00030284
		public void Insert(int index, TextDecoration value)
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

		/// <summary>Remove o objeto <see cref="T:System.Windows.TextDecoration" /> especificado da coleção.</summary>
		/// <param name="value">O objeto <see cref="T:System.Windows.TextDecoration" /> a ser removido.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> foi excluído com êxito; caso contrário <see langword="false" />.</returns>
		// Token: 0x06000CF2 RID: 3314 RVA: 0x00030ED4 File Offset: 0x000302D4
		public bool Remove(TextDecoration value)
		{
			base.WritePreamble();
			int num = this.IndexOf(value);
			if (num >= 0)
			{
				TextDecoration oldValue = this._collection[num];
				base.OnFreezablePropertyChanged(oldValue, null);
				this._collection.RemoveAt(num);
				this._version += 1U;
				base.WritePostscript();
				return true;
			}
			return false;
		}

		/// <summary>Remove o objeto <see cref="T:System.Windows.TextDecoration" /> especificado da coleção no índice indicado.</summary>
		/// <param name="index">A posição de índice de base zero de onde o objeto será excluído.</param>
		// Token: 0x06000CF3 RID: 3315 RVA: 0x00030F2C File Offset: 0x0003032C
		public void RemoveAt(int index)
		{
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00030F48 File Offset: 0x00030348
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			base.WritePreamble();
			TextDecoration oldValue = this._collection[index];
			base.OnFreezablePropertyChanged(oldValue, null);
			this._collection.RemoveAt(index);
			this._version += 1U;
		}

		/// <summary>Obtém ou define o objeto <see cref="T:System.Windows.TextDecoration" /> na posição de índice especificada.</summary>
		/// <param name="index">A posição de índice de base zero do objeto a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.TextDecoration" /> do objeto no <paramref name="index" /> posição.</returns>
		// Token: 0x1700018A RID: 394
		public TextDecoration this[int index]
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
					TextDecoration oldValue = this._collection[index];
					base.OnFreezablePropertyChanged(oldValue, value);
					this._collection[index] = value;
				}
				this._version += 1U;
				base.WritePostscript();
			}
		}

		/// <summary>Obtém o número de objetos <see cref="T:System.Windows.TextDecoration" /> no <see cref="T:System.Windows.TextDecorationCollection" />.</summary>
		/// <returns>O número de objetos na coleção.</returns>
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x00031018 File Offset: 0x00030418
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Copia os objetos <see cref="T:System.Windows.TextDecoration" /> na coleção para uma matriz de <see cref="T:System.Windows.TextDecorationCollection" />, começando na posição de índice especificada.</summary>
		/// <param name="array">A matriz de destino.</param>
		/// <param name="index">A posição de índice baseado em zero em que a cópia é iniciada.</param>
		// Token: 0x06000CF8 RID: 3320 RVA: 0x00031038 File Offset: 0x00030438
		public void CopyTo(TextDecoration[] array, int index)
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

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x00031088 File Offset: 0x00030488
		bool ICollection<TextDecoration>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Retorna um enumerador que pode iterar pela coleção.</summary>
		/// <returns>Um enumerador que pode iterar por meio da coleção.</returns>
		// Token: 0x06000CFA RID: 3322 RVA: 0x000310A4 File Offset: 0x000304A4
		public TextDecorationCollection.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new TextDecorationCollection.Enumerator(this);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x000310C0 File Offset: 0x000304C0
		IEnumerator<TextDecoration> IEnumerable<TextDecoration>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Obtém um valor que indica se a coleção é somente leitura.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.TextDecorationCollection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x000310D8 File Offset: 0x000304D8
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<TextDecoration>)this).IsReadOnly;
			}
		}

		/// <summary>Obtém um valor que indica se a coleção tem um tamanho fixo.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.TextDecorationCollection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x000310EC File Offset: 0x000304EC
		bool IList.IsFixedSize
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Obtém ou define o elemento no índice especificado.</summary>
		/// <param name="index">O índice com base em zero do elemento a ser obtido ou definido.</param>
		/// <returns>O elemento no índice especificado.</returns>
		// Token: 0x1700018F RID: 399
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

		/// <summary>Adiciona um item à coleção.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.TextDecorationCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x06000D00 RID: 3328 RVA: 0x00031138 File Offset: 0x00030538
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Determina se a coleção contém um valor específico.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.TextEffectCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.TextDecorationCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000D01 RID: 3329 RVA: 0x00031154 File Offset: 0x00030554
		bool IList.Contains(object value)
		{
			return this.Contains(value as TextDecoration);
		}

		/// <summary>Determina o índice de um item específico na coleção.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.TextDecorationCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06000D02 RID: 3330 RVA: 0x00031170 File Offset: 0x00030570
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as TextDecoration);
		}

		/// <summary>Insere um item na coleção no índice especificado.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.TextDecorationCollection" />.</param>
		// Token: 0x06000D03 RID: 3331 RVA: 0x0003118C File Offset: 0x0003058C
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Remove a primeira ocorrência de um objeto específico da coleção.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.TextDecorationCollection" />.</param>
		// Token: 0x06000D04 RID: 3332 RVA: 0x000311A8 File Offset: 0x000305A8
		void IList.Remove(object value)
		{
			this.Remove(value as TextDecoration);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.TextDecorationCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x06000D05 RID: 3333 RVA: 0x000311C4 File Offset: 0x000305C4
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

		/// <summary>Obtém um valor que indica se o acesso à coleção é sincronizado (thread-safe).</summary>
		/// <returns>
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.TextDecorationCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00031298 File Offset: 0x00030698
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Obtém um objeto que pode ser usado para sincronizar o acesso à coleção.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.TextDecorationCollection" />.</returns>
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x000312C0 File Offset: 0x000306C0
		object ICollection.SyncRoot
		{
			get
			{
				base.ReadPreamble();
				return this;
			}
		}

		/// <summary>Retorna um enumerador que itera pela coleção.</summary>
		/// <returns>Um objeto <see cref="T:System.Collections.IEnumerator" /> que pode ser usado para iterar pela coleção.</returns>
		// Token: 0x06000D08 RID: 3336 RVA: 0x000312D4 File Offset: 0x000306D4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x000312EC File Offset: 0x000306EC
		internal static TextDecorationCollection Empty
		{
			get
			{
				if (TextDecorationCollection.s_empty == null)
				{
					TextDecorationCollection textDecorationCollection = new TextDecorationCollection();
					textDecorationCollection.Freeze();
					TextDecorationCollection.s_empty = textDecorationCollection;
				}
				return TextDecorationCollection.s_empty;
			}
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00031318 File Offset: 0x00030718
		internal TextDecoration Internal_GetItem(int i)
		{
			return this._collection[i];
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00031334 File Offset: 0x00030734
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

		// Token: 0x06000D0C RID: 3340 RVA: 0x0003137C File Offset: 0x0003077C
		private TextDecoration Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is TextDecoration))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"TextDecoration"
				}));
			}
			return (TextDecoration)value;
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x000313E0 File Offset: 0x000307E0
		private int AddHelper(TextDecoration value)
		{
			int result = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			return result;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x000313FC File Offset: 0x000307FC
		internal int AddWithoutFiringPublicEvents(TextDecoration value)
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

		// Token: 0x06000D0F RID: 3343 RVA: 0x0003144C File Offset: 0x0003084C
		protected override Freezable CreateInstanceCore()
		{
			return new TextDecorationCollection();
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00031460 File Offset: 0x00030860
		protected override void CloneCore(Freezable source)
		{
			TextDecorationCollection textDecorationCollection = (TextDecorationCollection)source;
			base.CloneCore(source);
			int count = textDecorationCollection._collection.Count;
			this._collection = new FrugalStructList<TextDecoration>(count);
			for (int i = 0; i < count; i++)
			{
				TextDecoration textDecoration = textDecorationCollection._collection[i].Clone();
				base.OnFreezablePropertyChanged(null, textDecoration);
				this._collection.Add(textDecoration);
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x000314C8 File Offset: 0x000308C8
		protected override void CloneCurrentValueCore(Freezable source)
		{
			TextDecorationCollection textDecorationCollection = (TextDecorationCollection)source;
			base.CloneCurrentValueCore(source);
			int count = textDecorationCollection._collection.Count;
			this._collection = new FrugalStructList<TextDecoration>(count);
			for (int i = 0; i < count; i++)
			{
				TextDecoration textDecoration = textDecorationCollection._collection[i].CloneCurrentValue();
				base.OnFreezablePropertyChanged(null, textDecoration);
				this._collection.Add(textDecoration);
			}
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00031530 File Offset: 0x00030930
		protected override void GetAsFrozenCore(Freezable source)
		{
			TextDecorationCollection textDecorationCollection = (TextDecorationCollection)source;
			base.GetAsFrozenCore(source);
			int count = textDecorationCollection._collection.Count;
			this._collection = new FrugalStructList<TextDecoration>(count);
			for (int i = 0; i < count; i++)
			{
				TextDecoration textDecoration = (TextDecoration)textDecorationCollection._collection[i].GetAsFrozen();
				base.OnFreezablePropertyChanged(null, textDecoration);
				this._collection.Add(textDecoration);
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0003159C File Offset: 0x0003099C
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			TextDecorationCollection textDecorationCollection = (TextDecorationCollection)source;
			base.GetCurrentValueAsFrozenCore(source);
			int count = textDecorationCollection._collection.Count;
			this._collection = new FrugalStructList<TextDecoration>(count);
			for (int i = 0; i < count; i++)
			{
				TextDecoration textDecoration = (TextDecoration)textDecorationCollection._collection[i].GetCurrentValueAsFrozen();
				base.OnFreezablePropertyChanged(null, textDecoration);
				this._collection.Add(textDecoration);
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00031608 File Offset: 0x00030A08
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

		/// <summary>Inicializa uma nova instância <see cref="T:System.Windows.TextDecorationCollection" /> que está vazia.</summary>
		// Token: 0x06000D15 RID: 3349 RVA: 0x00031650 File Offset: 0x00030A50
		public TextDecorationCollection()
		{
			this._collection = default(FrugalStructList<TextDecoration>);
		}

		/// <summary>Inicializa uma nova instância <see cref="T:System.Windows.TextDecorationCollection" /> que está vazia e tem a capacidade inicial especificada.</summary>
		/// <param name="capacity">O número de elementos que a nova coleção é capaz de armazenar inicialmente.</param>
		// Token: 0x06000D16 RID: 3350 RVA: 0x00031670 File Offset: 0x00030A70
		public TextDecorationCollection(int capacity)
		{
			this._collection = new FrugalStructList<TextDecoration>(capacity);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.TextDecorationCollection" /> especificando um enumerador.</summary>
		/// <param name="collection">Um enumerador do tipo <see cref="T:System.Collections.Generic.IEnumerable`1" />.</param>
		// Token: 0x06000D17 RID: 3351 RVA: 0x00031690 File Offset: 0x00030A90
		public TextDecorationCollection(IEnumerable<TextDecoration> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				bool flag = true;
				ICollection<TextDecoration> collection2 = collection as ICollection<TextDecoration>;
				if (collection2 != null)
				{
					this._collection = new FrugalStructList<TextDecoration>(collection2);
				}
				else
				{
					ICollection collection3 = collection as ICollection;
					if (collection3 != null)
					{
						this._collection = new FrugalStructList<TextDecoration>(collection3);
					}
					else
					{
						this._collection = default(FrugalStructList<TextDecoration>);
						foreach (TextDecoration textDecoration in collection)
						{
							if (textDecoration == null)
							{
								throw new ArgumentException(SR.Get("Collection_NoNull"));
							}
							TextDecoration textDecoration2 = textDecoration;
							base.OnFreezablePropertyChanged(null, textDecoration2);
							this._collection.Add(textDecoration2);
						}
						flag = false;
					}
				}
				if (flag)
				{
					foreach (TextDecoration textDecoration3 in collection)
					{
						if (textDecoration3 == null)
						{
							throw new ArgumentException(SR.Get("Collection_NoNull"));
						}
						base.OnFreezablePropertyChanged(null, textDecoration3);
					}
				}
				base.WritePostscript();
				return;
			}
			throw new ArgumentNullException("collection");
		}

		// Token: 0x040007C3 RID: 1987
		private static TextDecorationCollection s_empty;

		// Token: 0x040007C4 RID: 1988
		internal FrugalStructList<TextDecoration> _collection;

		// Token: 0x040007C5 RID: 1989
		internal uint _version;

		/// <summary>Enumera itens <see cref="T:System.Windows.TextDecoration" /> em um <see cref="T:System.Windows.TextDecoration" />.</summary>
		// Token: 0x020007FF RID: 2047
		public struct Enumerator : IEnumerator, IEnumerator<TextDecoration>, IDisposable
		{
			// Token: 0x060055E4 RID: 21988 RVA: 0x001617CC File Offset: 0x00160BCC
			internal Enumerator(TextDecorationCollection list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = null;
			}

			/// <summary>Para obter uma descrição desses membros, consulte <see cref="M:System.IDisposable.Dispose" />.</summary>
			// Token: 0x060055E5 RID: 21989 RVA: 0x001617FC File Offset: 0x00160BFC
			void IDisposable.Dispose()
			{
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador foi avançado com êxito para o próximo elemento; caso contrário, <see langword="false" />.</returns>
			// Token: 0x060055E6 RID: 21990 RVA: 0x0016180C File Offset: 0x00160C0C
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					TextDecorationCollection list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._current = list._collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Redefine o enumerador com sua posição inicial, que é antes do primeiro elemento da coleção.</summary>
			// Token: 0x060055E7 RID: 21991 RVA: 0x001618A0 File Offset: 0x00160CA0
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
			// Token: 0x1700119B RID: 4507
			// (get) Token: 0x060055E8 RID: 21992 RVA: 0x001618E4 File Offset: 0x00160CE4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém o elemento atual na coleção.</summary>
			/// <returns>O <see cref="T:System.Windows.TextDecoration" /> atual na coleção.</returns>
			// Token: 0x1700119C RID: 4508
			// (get) Token: 0x060055E9 RID: 21993 RVA: 0x001618F8 File Offset: 0x00160CF8
			public TextDecoration Current
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

			// Token: 0x040026A2 RID: 9890
			private TextDecoration _current;

			// Token: 0x040026A3 RID: 9891
			private TextDecorationCollection _list;

			// Token: 0x040026A4 RID: 9892
			private uint _version;

			// Token: 0x040026A5 RID: 9893
			private int _index;
		}
	}
}
