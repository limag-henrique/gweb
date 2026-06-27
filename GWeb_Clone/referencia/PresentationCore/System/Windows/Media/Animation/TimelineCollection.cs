using System;
using System.Collections;
using System.Collections.Generic;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.Timeline" /> .</summary>
	// Token: 0x0200055E RID: 1374
	public sealed class TimelineCollection : Animatable, IList, ICollection, IEnumerable, IList<Timeline>, ICollection<Timeline>, IEnumerable<Timeline>
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.TimelineCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003F69 RID: 16233 RVA: 0x000F8D0C File Offset: 0x000F810C
		public new TimelineCollection Clone()
		{
			return (TimelineCollection)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Animation.TimelineCollection" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06003F6A RID: 16234 RVA: 0x000F8D24 File Offset: 0x000F8124
		public new TimelineCollection CloneCurrentValue()
		{
			return (TimelineCollection)base.CloneCurrentValue();
		}

		/// <summary>Insere um novo objeto <see cref="T:System.Windows.Media.Animation.Timeline" /> no <see cref="T:System.Windows.Media.Animation.TimelineCollection" />.</summary>
		/// <param name="value">O objeto a adicionar.</param>
		// Token: 0x06003F6B RID: 16235 RVA: 0x000F8D3C File Offset: 0x000F813C
		public void Add(Timeline value)
		{
			this.AddHelper(value);
		}

		/// <summary>Remove todos os itens do <see cref="T:System.Windows.Media.Animation.TimelineCollection" />.</summary>
		// Token: 0x06003F6C RID: 16236 RVA: 0x000F8D54 File Offset: 0x000F8154
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

		/// <summary>Determina se o <see cref="T:System.Windows.Media.Animation.TimelineCollection" /> contém o objeto <see cref="T:System.Windows.Media.Animation.Timeline" /> especificado.</summary>
		/// <param name="value">O objeto a ser localizado.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> for encontrado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003F6D RID: 16237 RVA: 0x000F8DB4 File Offset: 0x000F81B4
		public bool Contains(Timeline value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Obtém a posição de índice baseado em zero de um objeto de linha do tempo no <see cref="T:System.Windows.Media.Animation.TimelineCollection" />.</summary>
		/// <param name="value">O objeto a ser localizado.</param>
		/// <returns>A posição de índice de <paramref name="value" /> dentro dessa lista.  Caso não seja encontrado, -1 será retornado.</returns>
		// Token: 0x06003F6E RID: 16238 RVA: 0x000F8DD4 File Offset: 0x000F81D4
		public int IndexOf(Timeline value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Insere o objeto de linha do tempo especificado no <see cref="T:System.Windows.Media.Animation.TimelineCollection" /> na posição de índice especificada.</summary>
		/// <param name="index">A posição de índice de base zero em que o <paramref name="value" /> será inserido.</param>
		/// <param name="value">O objeto a ser inserido.</param>
		// Token: 0x06003F6F RID: 16239 RVA: 0x000F8DF4 File Offset: 0x000F81F4
		public void Insert(int index, Timeline value)
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

		/// <summary>Remove a primeira ocorrência de um <see cref="T:System.Windows.Media.Animation.Timeline" /> especificado desse <see cref="T:System.Windows.Media.Animation.TimelineCollection" />.</summary>
		/// <param name="value">O objeto a ser removido.</param>
		/// <returns>
		///   <see langword="true" /> se a operação tiver sido bem-sucedida; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003F70 RID: 16240 RVA: 0x000F8E44 File Offset: 0x000F8244
		public bool Remove(Timeline value)
		{
			base.WritePreamble();
			int num = this.IndexOf(value);
			if (num >= 0)
			{
				Timeline oldValue = this._collection[num];
				base.OnFreezablePropertyChanged(oldValue, null);
				this._collection.RemoveAt(num);
				this._version += 1U;
				base.WritePostscript();
				return true;
			}
			return false;
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.Timeline" /> na posição do índice especificada desse <see cref="T:System.Windows.Media.Animation.TimelineCollection" />.</summary>
		/// <param name="index">A posição do índice baseado em zero do item a ser removido.</param>
		// Token: 0x06003F71 RID: 16241 RVA: 0x000F8E9C File Offset: 0x000F829C
		public void RemoveAt(int index)
		{
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x000F8EB8 File Offset: 0x000F82B8
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			base.WritePreamble();
			Timeline oldValue = this._collection[index];
			base.OnFreezablePropertyChanged(oldValue, null);
			this._collection.RemoveAt(index);
			this._version += 1U;
		}

		/// <summary>Obtém ou define um item na posição de índice especificada dentro desse <see cref="T:System.Windows.Media.Animation.TimelineCollection" />.</summary>
		/// <param name="index">A posição de índice a acessar.</param>
		/// <returns>O objeto de linha do tempo no <paramref name="index" /> posição.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero ou maior ou igual ao tamanho de <see cref="T:System.Windows.Media.Animation.TimelineCollection" />.</exception>
		// Token: 0x17000CB9 RID: 3257
		public Timeline this[int index]
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
					Timeline oldValue = this._collection[index];
					base.OnFreezablePropertyChanged(oldValue, value);
					this._collection[index] = value;
				}
				this._version += 1U;
				base.WritePostscript();
			}
		}

		/// <summary>Obtém o número de itens contidos neste <see cref="T:System.Windows.Media.Animation.TimelineCollection" />.</summary>
		/// <returns>O número de itens contidos nesta instância.</returns>
		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06003F75 RID: 16245 RVA: 0x000F8F88 File Offset: 0x000F8388
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Copia os itens do <see cref="T:System.Windows.Media.Animation.TimelineCollection" /> para a matriz passada da linha do tempo, iniciando na posição de índice especificada.</summary>
		/// <param name="array">A matriz de destino.</param>
		/// <param name="index">A posição de índice baseado em zero em que a cópia é iniciada.</param>
		// Token: 0x06003F76 RID: 16246 RVA: 0x000F8FA8 File Offset: 0x000F83A8
		public void CopyTo(Timeline[] array, int index)
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

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06003F77 RID: 16247 RVA: 0x000F8FF8 File Offset: 0x000F83F8
		bool ICollection<Timeline>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Obtém um enumerador que pode iterar os membros da coleção.</summary>
		/// <returns>Um objeto que pode iterar os membros da coleção.</returns>
		// Token: 0x06003F78 RID: 16248 RVA: 0x000F9014 File Offset: 0x000F8414
		public TimelineCollection.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new TimelineCollection.Enumerator(this);
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x000F9030 File Offset: 0x000F8430
		IEnumerator<Timeline> IEnumerable<Timeline>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.Animation.TimelineCollection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06003F7A RID: 16250 RVA: 0x000F9048 File Offset: 0x000F8448
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<Timeline>)this).IsReadOnly;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.Animation.TimelineCollection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06003F7B RID: 16251 RVA: 0x000F905C File Offset: 0x000F845C
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
		// Token: 0x17000CBE RID: 3262
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
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.Animation.TimelineCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x06003F7E RID: 16254 RVA: 0x000F90A8 File Offset: 0x000F84A8
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.Animation.TimelineCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.Animation.TimelineCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003F7F RID: 16255 RVA: 0x000F90C4 File Offset: 0x000F84C4
		bool IList.Contains(object value)
		{
			return this.Contains(value as Timeline);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.Animation.TimelineCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06003F80 RID: 16256 RVA: 0x000F90E0 File Offset: 0x000F84E0
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as Timeline);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.Animation.TimelineCollection" />.</param>
		// Token: 0x06003F81 RID: 16257 RVA: 0x000F90FC File Offset: 0x000F84FC
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.Animation.TimelineCollection" />.</param>
		// Token: 0x06003F82 RID: 16258 RVA: 0x000F9118 File Offset: 0x000F8518
		void IList.Remove(object value)
		{
			this.Remove(value as Timeline);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.Animation.TimelineCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x06003F83 RID: 16259 RVA: 0x000F9134 File Offset: 0x000F8534
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
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.Animation.TimelineCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06003F84 RID: 16260 RVA: 0x000F9208 File Offset: 0x000F8608
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.Animation.TimelineCollection" />.</returns>
		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06003F85 RID: 16261 RVA: 0x000F9230 File Offset: 0x000F8630
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
		// Token: 0x06003F86 RID: 16262 RVA: 0x000F9244 File Offset: 0x000F8644
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06003F87 RID: 16263 RVA: 0x000F925C File Offset: 0x000F865C
		internal static TimelineCollection Empty
		{
			get
			{
				if (TimelineCollection.s_empty == null)
				{
					TimelineCollection timelineCollection = new TimelineCollection();
					timelineCollection.Freeze();
					TimelineCollection.s_empty = timelineCollection;
				}
				return TimelineCollection.s_empty;
			}
		}

		// Token: 0x06003F88 RID: 16264 RVA: 0x000F9288 File Offset: 0x000F8688
		internal Timeline Internal_GetItem(int i)
		{
			return this._collection[i];
		}

		// Token: 0x06003F89 RID: 16265 RVA: 0x000F92A4 File Offset: 0x000F86A4
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

		// Token: 0x06003F8A RID: 16266 RVA: 0x000F92EC File Offset: 0x000F86EC
		private Timeline Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is Timeline))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"Timeline"
				}));
			}
			return (Timeline)value;
		}

		// Token: 0x06003F8B RID: 16267 RVA: 0x000F9350 File Offset: 0x000F8750
		private int AddHelper(Timeline value)
		{
			int result = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			return result;
		}

		// Token: 0x06003F8C RID: 16268 RVA: 0x000F936C File Offset: 0x000F876C
		internal int AddWithoutFiringPublicEvents(Timeline value)
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

		// Token: 0x06003F8D RID: 16269 RVA: 0x000F93BC File Offset: 0x000F87BC
		protected override Freezable CreateInstanceCore()
		{
			return new TimelineCollection();
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x000F93D0 File Offset: 0x000F87D0
		protected override void CloneCore(Freezable source)
		{
			TimelineCollection timelineCollection = (TimelineCollection)source;
			base.CloneCore(source);
			int count = timelineCollection._collection.Count;
			this._collection = new FrugalStructList<Timeline>(count);
			for (int i = 0; i < count; i++)
			{
				Timeline timeline = timelineCollection._collection[i].Clone();
				base.OnFreezablePropertyChanged(null, timeline);
				this._collection.Add(timeline);
			}
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x000F9438 File Offset: 0x000F8838
		protected override void CloneCurrentValueCore(Freezable source)
		{
			TimelineCollection timelineCollection = (TimelineCollection)source;
			base.CloneCurrentValueCore(source);
			int count = timelineCollection._collection.Count;
			this._collection = new FrugalStructList<Timeline>(count);
			for (int i = 0; i < count; i++)
			{
				Timeline timeline = timelineCollection._collection[i].CloneCurrentValue();
				base.OnFreezablePropertyChanged(null, timeline);
				this._collection.Add(timeline);
			}
		}

		// Token: 0x06003F90 RID: 16272 RVA: 0x000F94A0 File Offset: 0x000F88A0
		protected override void GetAsFrozenCore(Freezable source)
		{
			TimelineCollection timelineCollection = (TimelineCollection)source;
			base.GetAsFrozenCore(source);
			int count = timelineCollection._collection.Count;
			this._collection = new FrugalStructList<Timeline>(count);
			for (int i = 0; i < count; i++)
			{
				Timeline timeline = (Timeline)timelineCollection._collection[i].GetAsFrozen();
				base.OnFreezablePropertyChanged(null, timeline);
				this._collection.Add(timeline);
			}
		}

		// Token: 0x06003F91 RID: 16273 RVA: 0x000F950C File Offset: 0x000F890C
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			TimelineCollection timelineCollection = (TimelineCollection)source;
			base.GetCurrentValueAsFrozenCore(source);
			int count = timelineCollection._collection.Count;
			this._collection = new FrugalStructList<Timeline>(count);
			for (int i = 0; i < count; i++)
			{
				Timeline timeline = (Timeline)timelineCollection._collection[i].GetCurrentValueAsFrozen();
				base.OnFreezablePropertyChanged(null, timeline);
				this._collection.Add(timeline);
			}
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x000F9578 File Offset: 0x000F8978
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

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.TimelineCollection" />.</summary>
		// Token: 0x06003F93 RID: 16275 RVA: 0x000F95C0 File Offset: 0x000F89C0
		public TimelineCollection()
		{
			this._collection = default(FrugalStructList<Timeline>);
		}

		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.Media.Animation.TimelineCollection" /> com a capacidade inicial especificada.</summary>
		/// <param name="capacity">A capacidade inicial do <see cref="T:System.Windows.Media.Animation.TimelineCollection" />.</param>
		// Token: 0x06003F94 RID: 16276 RVA: 0x000F95E0 File Offset: 0x000F89E0
		public TimelineCollection(int capacity)
		{
			this._collection = new FrugalStructList<Timeline>(capacity);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.TimelineCollection" /> que inclui todos os mesmos elementos que uma coleção existente.</summary>
		/// <param name="collection">Coleção de elementos em que essa instância se baseia.</param>
		/// <exception cref="T:System.ArgumentException">Ocorre quando <paramref name="collection" /> está vazio.</exception>
		// Token: 0x06003F95 RID: 16277 RVA: 0x000F9600 File Offset: 0x000F8A00
		public TimelineCollection(IEnumerable<Timeline> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				bool flag = true;
				ICollection<Timeline> collection2 = collection as ICollection<Timeline>;
				if (collection2 != null)
				{
					this._collection = new FrugalStructList<Timeline>(collection2);
				}
				else
				{
					ICollection collection3 = collection as ICollection;
					if (collection3 != null)
					{
						this._collection = new FrugalStructList<Timeline>(collection3);
					}
					else
					{
						this._collection = default(FrugalStructList<Timeline>);
						foreach (Timeline timeline in collection)
						{
							if (timeline == null)
							{
								throw new ArgumentException(SR.Get("Collection_NoNull"));
							}
							Timeline timeline2 = timeline;
							base.OnFreezablePropertyChanged(null, timeline2);
							this._collection.Add(timeline2);
						}
						flag = false;
					}
				}
				if (flag)
				{
					foreach (Timeline timeline3 in collection)
					{
						if (timeline3 == null)
						{
							throw new ArgumentException(SR.Get("Collection_NoNull"));
						}
						base.OnFreezablePropertyChanged(null, timeline3);
					}
				}
				base.WritePostscript();
				return;
			}
			throw new ArgumentNullException("collection");
		}

		// Token: 0x04001771 RID: 6001
		private static TimelineCollection s_empty;

		// Token: 0x04001772 RID: 6002
		internal FrugalStructList<Timeline> _collection;

		// Token: 0x04001773 RID: 6003
		internal uint _version;

		/// <summary>Enumera os membros de um <see cref="T:System.Windows.Media.Animation.TimelineCollection" />.</summary>
		// Token: 0x020008C8 RID: 2248
		public struct Enumerator : IEnumerator, IEnumerator<Timeline>, IDisposable
		{
			// Token: 0x060058BC RID: 22716 RVA: 0x001686C0 File Offset: 0x00167AC0
			internal Enumerator(TimelineCollection list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = null;
			}

			/// <summary>Este membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x060058BD RID: 22717 RVA: 0x001686F0 File Offset: 0x00167AF0
			void IDisposable.Dispose()
			{
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador avançou com êxito para o próximo elemento na coleção; caso contrário, <see langword="false" />.</returns>
			// Token: 0x060058BE RID: 22718 RVA: 0x00168700 File Offset: 0x00167B00
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					TimelineCollection list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._current = list._collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Redefine o enumerador como sua posição inicial.</summary>
			// Token: 0x060058BF RID: 22719 RVA: 0x00168794 File Offset: 0x00167B94
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

			/// <summary>Este membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x17001256 RID: 4694
			// (get) Token: 0x060058C0 RID: 22720 RVA: 0x001687D8 File Offset: 0x00167BD8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém um valor que representa o elemento atual na coleção.</summary>
			/// <returns>O elemento atual na coleção.</returns>
			// Token: 0x17001257 RID: 4695
			// (get) Token: 0x060058C1 RID: 22721 RVA: 0x001687EC File Offset: 0x00167BEC
			public Timeline Current
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

			// Token: 0x0400296A RID: 10602
			private Timeline _current;

			// Token: 0x0400296B RID: 10603
			private TimelineCollection _list;

			// Token: 0x0400296C RID: 10604
			private uint _version;

			// Token: 0x0400296D RID: 10605
			private int _index;
		}
	}
}
