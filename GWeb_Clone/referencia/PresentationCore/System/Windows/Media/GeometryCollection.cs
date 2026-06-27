using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using MS.Internal.Collections;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Geometry" /> .</summary>
	// Token: 0x020003AD RID: 941
	public sealed class GeometryCollection : Animatable, IList, ICollection, IEnumerable, IList<Geometry>, ICollection<Geometry>, IEnumerable<Geometry>
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.GeometryCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x0600238B RID: 9099 RVA: 0x0008F308 File Offset: 0x0008E708
		public new GeometryCollection Clone()
		{
			return (GeometryCollection)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.GeometryCollection" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x0600238C RID: 9100 RVA: 0x0008F320 File Offset: 0x0008E720
		public new GeometryCollection CloneCurrentValue()
		{
			return (GeometryCollection)base.CloneCurrentValue();
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Geometry" /> ao final da coleção.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.Geometry" /> a adicionar ao final da coleção.</param>
		// Token: 0x0600238D RID: 9101 RVA: 0x0008F338 File Offset: 0x0008E738
		public void Add(Geometry value)
		{
			this.AddHelper(value);
		}

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Geometry" /> da coleção.</summary>
		// Token: 0x0600238E RID: 9102 RVA: 0x0008F350 File Offset: 0x0008E750
		public void Clear()
		{
			base.WritePreamble();
			FrugalStructList<Geometry> collection = this._collection;
			this._collection = new FrugalStructList<Geometry>(this._collection.Capacity);
			for (int i = collection.Count - 1; i >= 0; i--)
			{
				base.OnFreezablePropertyChanged(collection[i], null);
				this.OnRemove(collection[i]);
			}
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Retorna um valor que indica se a coleção contém o <see cref="T:System.Windows.Media.Geometry" /> especificado.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.Geometry" /> a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> for encontrado no <see cref="T:System.Collections.IList" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600238F RID: 9103 RVA: 0x0008F3C8 File Offset: 0x0008E7C8
		public bool Contains(Geometry value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Geometry" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.Geometry" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="value" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x06002390 RID: 9104 RVA: 0x0008F3E8 File Offset: 0x0008E7E8
		public int IndexOf(Geometry value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Geometry" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Geometry" /> é inserido.</param>
		/// <param name="value">O objeto <see cref="T:System.Windows.Media.Geometry" /> a ser inserido na coleção.</param>
		// Token: 0x06002391 RID: 9105 RVA: 0x0008F408 File Offset: 0x0008E808
		public void Insert(int index, Geometry value)
		{
			if (value == null)
			{
				throw new ArgumentException(SR.Get("Collection_NoNull"));
			}
			base.WritePreamble();
			base.OnFreezablePropertyChanged(null, value);
			this._collection.Insert(index, value);
			this.OnInsert(value);
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Remove a primeira ocorrência do <see cref="T:System.Windows.Media.Geometry" /> especificado dessa <see cref="T:System.Windows.Media.GeometryCollection" />.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.Geometry" /> a ser removido deste <see cref="T:System.Windows.Media.GeometryCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> tiver sido removido da coleção; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002392 RID: 9106 RVA: 0x0008F460 File Offset: 0x0008E860
		public bool Remove(Geometry value)
		{
			base.WritePreamble();
			int num = this.IndexOf(value);
			if (num >= 0)
			{
				Geometry oldValue = this._collection[num];
				base.OnFreezablePropertyChanged(oldValue, null);
				this._collection.RemoveAt(num);
				this.OnRemove(oldValue);
				this._version += 1U;
				base.WritePostscript();
				return true;
			}
			return false;
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Geometry" /> no índice especificado dessa <see cref="T:System.Windows.Media.GeometryCollection" />.</summary>
		/// <param name="index">O índice do <see cref="T:System.Windows.Media.Geometry" /> a ser removido.</param>
		// Token: 0x06002393 RID: 9107 RVA: 0x0008F4C0 File Offset: 0x0008E8C0
		public void RemoveAt(int index)
		{
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x0008F4DC File Offset: 0x0008E8DC
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			base.WritePreamble();
			Geometry oldValue = this._collection[index];
			base.OnFreezablePropertyChanged(oldValue, null);
			this._collection.RemoveAt(index);
			this.OnRemove(oldValue);
			this._version += 1U;
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Geometry" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Geometry" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Geometry" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.DoubleKeyFrameCollection.Count" />.</exception>
		// Token: 0x17000706 RID: 1798
		public Geometry this[int index]
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
					Geometry oldValue = this._collection[index];
					base.OnFreezablePropertyChanged(oldValue, value);
					this._collection[index] = value;
					this.OnSet(oldValue, value);
				}
				this._version += 1U;
				base.WritePostscript();
			}
		}

		/// <summary>Obtém o número de geometrias contidas na <see cref="T:System.Windows.Media.GeometryCollection" />.</summary>
		/// <returns>O número de geometrias contidas no <see cref="T:System.Windows.Media.GeometryCollection" />.</returns>
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06002397 RID: 9111 RVA: 0x0008F5BC File Offset: 0x0008E9BC
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Copia todos os objetos de <see cref="T:System.Windows.Media.Geometry" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		// Token: 0x06002398 RID: 9112 RVA: 0x0008F5DC File Offset: 0x0008E9DC
		public void CopyTo(Geometry[] array, int index)
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

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06002399 RID: 9113 RVA: 0x0008F62C File Offset: 0x0008EA2C
		bool ICollection<Geometry>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Retorna um enumerador que pode iterar pela coleção.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.GeometryCollection.Enumerator" /> que pode iterar pela coleção.</returns>
		// Token: 0x0600239A RID: 9114 RVA: 0x0008F648 File Offset: 0x0008EA48
		public GeometryCollection.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new GeometryCollection.Enumerator(this);
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x0008F664 File Offset: 0x0008EA64
		IEnumerator<Geometry> IEnumerable<Geometry>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.GeometryCollection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x0600239C RID: 9116 RVA: 0x0008F67C File Offset: 0x0008EA7C
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<Geometry>)this).IsReadOnly;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.GeometryCollection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x0600239D RID: 9117 RVA: 0x0008F690 File Offset: 0x0008EA90
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
		// Token: 0x1700070B RID: 1803
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
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.GeometryCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x060023A0 RID: 9120 RVA: 0x0008F6DC File Offset: 0x0008EADC
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.GeometryCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.GeometryCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060023A1 RID: 9121 RVA: 0x0008F6F8 File Offset: 0x0008EAF8
		bool IList.Contains(object value)
		{
			return this.Contains(value as Geometry);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.GeometryCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x060023A2 RID: 9122 RVA: 0x0008F714 File Offset: 0x0008EB14
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as Geometry);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.GeometryCollection" />.</param>
		// Token: 0x060023A3 RID: 9123 RVA: 0x0008F730 File Offset: 0x0008EB30
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.GeometryCollection" />.</param>
		// Token: 0x060023A4 RID: 9124 RVA: 0x0008F74C File Offset: 0x0008EB4C
		void IList.Remove(object value)
		{
			this.Remove(value as Geometry);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.GeometryCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x060023A5 RID: 9125 RVA: 0x0008F768 File Offset: 0x0008EB68
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
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.GeometryCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x0008F83C File Offset: 0x0008EC3C
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.GeometryCollection" />.</returns>
		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060023A7 RID: 9127 RVA: 0x0008F864 File Offset: 0x0008EC64
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
		// Token: 0x060023A8 RID: 9128 RVA: 0x0008F878 File Offset: 0x0008EC78
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060023A9 RID: 9129 RVA: 0x0008F890 File Offset: 0x0008EC90
		internal static GeometryCollection Empty
		{
			get
			{
				if (GeometryCollection.s_empty == null)
				{
					GeometryCollection geometryCollection = new GeometryCollection();
					geometryCollection.Freeze();
					GeometryCollection.s_empty = geometryCollection;
				}
				return GeometryCollection.s_empty;
			}
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x0008F8BC File Offset: 0x0008ECBC
		internal Geometry Internal_GetItem(int i)
		{
			return this._collection[i];
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x0008F8D8 File Offset: 0x0008ECD8
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

		// Token: 0x060023AC RID: 9132 RVA: 0x0008F920 File Offset: 0x0008ED20
		private Geometry Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is Geometry))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"Geometry"
				}));
			}
			return (Geometry)value;
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x0008F984 File Offset: 0x0008ED84
		private int AddHelper(Geometry value)
		{
			int result = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			return result;
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x0008F9A0 File Offset: 0x0008EDA0
		internal int AddWithoutFiringPublicEvents(Geometry value)
		{
			if (value == null)
			{
				throw new ArgumentException(SR.Get("Collection_NoNull"));
			}
			base.WritePreamble();
			base.OnFreezablePropertyChanged(null, value);
			int result = this._collection.Add(value);
			this.OnInsert(value);
			this._version += 1U;
			return result;
		}

		// Token: 0x14000196 RID: 406
		// (add) Token: 0x060023AF RID: 9135 RVA: 0x0008F9F8 File Offset: 0x0008EDF8
		// (remove) Token: 0x060023B0 RID: 9136 RVA: 0x0008FA30 File Offset: 0x0008EE30
		internal event ItemInsertedHandler ItemInserted;

		// Token: 0x14000197 RID: 407
		// (add) Token: 0x060023B1 RID: 9137 RVA: 0x0008FA68 File Offset: 0x0008EE68
		// (remove) Token: 0x060023B2 RID: 9138 RVA: 0x0008FAA0 File Offset: 0x0008EEA0
		internal event ItemRemovedHandler ItemRemoved;

		// Token: 0x060023B3 RID: 9139 RVA: 0x0008FAD8 File Offset: 0x0008EED8
		private void OnInsert(object item)
		{
			if (this.ItemInserted != null)
			{
				this.ItemInserted(this, item);
			}
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x0008FAFC File Offset: 0x0008EEFC
		private void OnRemove(object oldValue)
		{
			if (this.ItemRemoved != null)
			{
				this.ItemRemoved(this, oldValue);
			}
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x0008FB20 File Offset: 0x0008EF20
		private void OnSet(object oldValue, object newValue)
		{
			this.OnInsert(newValue);
			this.OnRemove(oldValue);
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x0008FB3C File Offset: 0x0008EF3C
		protected override Freezable CreateInstanceCore()
		{
			return new GeometryCollection();
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x0008FB50 File Offset: 0x0008EF50
		protected override void CloneCore(Freezable source)
		{
			GeometryCollection geometryCollection = (GeometryCollection)source;
			base.CloneCore(source);
			int count = geometryCollection._collection.Count;
			this._collection = new FrugalStructList<Geometry>(count);
			for (int i = 0; i < count; i++)
			{
				Geometry geometry = geometryCollection._collection[i].Clone();
				base.OnFreezablePropertyChanged(null, geometry);
				this._collection.Add(geometry);
				this.OnInsert(geometry);
			}
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x0008FBC0 File Offset: 0x0008EFC0
		protected override void CloneCurrentValueCore(Freezable source)
		{
			GeometryCollection geometryCollection = (GeometryCollection)source;
			base.CloneCurrentValueCore(source);
			int count = geometryCollection._collection.Count;
			this._collection = new FrugalStructList<Geometry>(count);
			for (int i = 0; i < count; i++)
			{
				Geometry geometry = geometryCollection._collection[i].CloneCurrentValue();
				base.OnFreezablePropertyChanged(null, geometry);
				this._collection.Add(geometry);
				this.OnInsert(geometry);
			}
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x0008FC30 File Offset: 0x0008F030
		protected override void GetAsFrozenCore(Freezable source)
		{
			GeometryCollection geometryCollection = (GeometryCollection)source;
			base.GetAsFrozenCore(source);
			int count = geometryCollection._collection.Count;
			this._collection = new FrugalStructList<Geometry>(count);
			for (int i = 0; i < count; i++)
			{
				Geometry geometry = (Geometry)geometryCollection._collection[i].GetAsFrozen();
				base.OnFreezablePropertyChanged(null, geometry);
				this._collection.Add(geometry);
				this.OnInsert(geometry);
			}
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x0008FCA4 File Offset: 0x0008F0A4
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			GeometryCollection geometryCollection = (GeometryCollection)source;
			base.GetCurrentValueAsFrozenCore(source);
			int count = geometryCollection._collection.Count;
			this._collection = new FrugalStructList<Geometry>(count);
			for (int i = 0; i < count; i++)
			{
				Geometry geometry = (Geometry)geometryCollection._collection[i].GetCurrentValueAsFrozen();
				base.OnFreezablePropertyChanged(null, geometry);
				this._collection.Add(geometry);
				this.OnInsert(geometry);
			}
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x0008FD18 File Offset: 0x0008F118
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

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GeometryCollection" />.</summary>
		// Token: 0x060023BC RID: 9148 RVA: 0x0008FD60 File Offset: 0x0008F160
		public GeometryCollection()
		{
			this._collection = default(FrugalStructList<Geometry>);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GeometryCollection" /> com a capacidade especificada ou o número de objetos <see cref="T:System.Windows.Media.Geometry" /> que a coleção é capaz de armazenar inicialmente.</summary>
		/// <param name="capacity">O número de objetos <see cref="T:System.Windows.Media.Geometry" /> que a coleção é capaz de armazenar inicialmente.</param>
		// Token: 0x060023BD RID: 9149 RVA: 0x0008FD80 File Offset: 0x0008F180
		public GeometryCollection(int capacity)
		{
			this._collection = new FrugalStructList<Geometry>(capacity);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GeometryCollection" /> com a coleção especificada de objetos <see cref="T:System.Windows.Media.Geometry" />.</summary>
		/// <param name="collection">A coleção de objetos <see cref="T:System.Windows.Media.Geometry" /> que compõem o <see cref="T:System.Windows.Media.GeometryCollection" />.</param>
		// Token: 0x060023BE RID: 9150 RVA: 0x0008FDA0 File Offset: 0x0008F1A0
		public GeometryCollection(IEnumerable<Geometry> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				bool flag = true;
				ICollection<Geometry> collection2 = collection as ICollection<Geometry>;
				if (collection2 != null)
				{
					this._collection = new FrugalStructList<Geometry>(collection2);
				}
				else
				{
					ICollection collection3 = collection as ICollection;
					if (collection3 != null)
					{
						this._collection = new FrugalStructList<Geometry>(collection3);
					}
					else
					{
						this._collection = default(FrugalStructList<Geometry>);
						foreach (Geometry geometry in collection)
						{
							if (geometry == null)
							{
								throw new ArgumentException(SR.Get("Collection_NoNull"));
							}
							Geometry geometry2 = geometry;
							base.OnFreezablePropertyChanged(null, geometry2);
							this._collection.Add(geometry2);
							this.OnInsert(geometry2);
						}
						flag = false;
					}
				}
				if (flag)
				{
					foreach (Geometry geometry3 in collection)
					{
						if (geometry3 == null)
						{
							throw new ArgumentException(SR.Get("Collection_NoNull"));
						}
						base.OnFreezablePropertyChanged(null, geometry3);
						this.OnInsert(geometry3);
					}
				}
				base.WritePostscript();
				return;
			}
			throw new ArgumentNullException("collection");
		}

		// Token: 0x0400114D RID: 4429
		private static GeometryCollection s_empty;

		// Token: 0x0400114E RID: 4430
		internal FrugalStructList<Geometry> _collection;

		// Token: 0x0400114F RID: 4431
		internal uint _version;

		/// <summary>Enumera itens <see cref="T:System.Windows.Media.Geometry" /> em um <see cref="T:System.Windows.Media.GeometryCollection" />.</summary>
		// Token: 0x02000874 RID: 2164
		public struct Enumerator : IEnumerator, IEnumerator<Geometry>, IDisposable
		{
			// Token: 0x06005783 RID: 22403 RVA: 0x00165B40 File Offset: 0x00164F40
			internal Enumerator(GeometryCollection list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = null;
			}

			/// <summary>Este tipo ou membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x06005784 RID: 22404 RVA: 0x00165B70 File Offset: 0x00164F70
			void IDisposable.Dispose()
			{
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador foi avançado com êxito para o próximo elemento; caso contrário, <see langword="false" />.</returns>
			// Token: 0x06005785 RID: 22405 RVA: 0x00165B80 File Offset: 0x00164F80
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					GeometryCollection list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._current = list._collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Redefine o enumerador com sua posição inicial, que é antes do primeiro elemento da coleção.</summary>
			// Token: 0x06005786 RID: 22406 RVA: 0x00165C14 File Offset: 0x00165014
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

			/// <summary>Este tipo ou membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x1700120E RID: 4622
			// (get) Token: 0x06005787 RID: 22407 RVA: 0x00165C58 File Offset: 0x00165058
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém o elemento atual na coleção.</summary>
			/// <returns>O elemento atual na coleção.</returns>
			// Token: 0x1700120F RID: 4623
			// (get) Token: 0x06005788 RID: 22408 RVA: 0x00165C6C File Offset: 0x0016506C
			public Geometry Current
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

			// Token: 0x0400288D RID: 10381
			private Geometry _current;

			// Token: 0x0400288E RID: 10382
			private GeometryCollection _list;

			// Token: 0x0400288F RID: 10383
			private uint _version;

			// Token: 0x04002890 RID: 10384
			private int _index;
		}
	}
}
