using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using MS.Internal.Collections;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media.Media3D
{
	/// <summary>Representa uma coleção ordenada de objetos de <see cref="T:System.Windows.Media.Media3D.Model3D" />.</summary>
	// Token: 0x0200048C RID: 1164
	public sealed class Model3DCollection : Animatable, IList, ICollection, IEnumerable, IList<Model3D>, ICollection<Model3D>, IEnumerable<Model3D>
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.Model3DCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060033A7 RID: 13223 RVA: 0x000CD730 File Offset: 0x000CCB30
		public new Model3DCollection Clone()
		{
			return (Model3DCollection)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.Model3DCollection" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060033A8 RID: 13224 RVA: 0x000CD748 File Offset: 0x000CCB48
		public new Model3DCollection CloneCurrentValue()
		{
			return (Model3DCollection)base.CloneCurrentValue();
		}

		/// <summary>Adiciona um objeto <see cref="T:System.Windows.Media.Media3D.Model3D" /> ao final da <see cref="T:System.Windows.Media.Media3D.Model3DCollection" />.</summary>
		/// <param name="value">O item a ser adicionado ao fim dessa coleção.</param>
		// Token: 0x060033A9 RID: 13225 RVA: 0x000CD760 File Offset: 0x000CCB60
		public void Add(Model3D value)
		{
			this.AddHelper(value);
		}

		/// <summary>Remove todos os itens desta <see cref="T:System.Windows.Media.Media3D.Model3DCollection" />.</summary>
		// Token: 0x060033AA RID: 13226 RVA: 0x000CD778 File Offset: 0x000CCB78
		public void Clear()
		{
			base.WritePreamble();
			FrugalStructList<Model3D> collection = this._collection;
			this._collection = new FrugalStructList<Model3D>(this._collection.Capacity);
			for (int i = collection.Count - 1; i >= 0; i--)
			{
				base.OnFreezablePropertyChanged(collection[i], null);
				this.OnRemove(collection[i]);
			}
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Media.Media3D.Model3D" /> especificado está neste <see cref="T:System.Windows.Media.Media3D.Model3DCollection" />.</summary>
		/// <param name="value">O item a ser localizado nesta coleção.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" />, o Model3D especificado estará neste Model3DCollection; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060033AB RID: 13227 RVA: 0x000CD7F0 File Offset: 0x000CCBF0
		public bool Contains(Model3D value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Obtém a posição de índice da primeira ocorrência do <see cref="T:System.Windows.Media.Media3D.Model3D" /> especificado.</summary>
		/// <param name="value">O Model3D a pesquisar.</param>
		/// <returns>A posição do índice do Model3D especificado.</returns>
		// Token: 0x060033AC RID: 13228 RVA: 0x000CD810 File Offset: 0x000CCC10
		public int IndexOf(Model3D value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Media3D.Model3D" /> nesta <see cref="T:System.Windows.Media.Media3D.Model3DCollection" /> na posição do índice especificada.</summary>
		/// <param name="index">A posição do índice na qual inserir o Model3D especificado.</param>
		/// <param name="value">O Model3D a inserir.</param>
		// Token: 0x060033AD RID: 13229 RVA: 0x000CD830 File Offset: 0x000CCC30
		public void Insert(int index, Model3D value)
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

		/// <summary>Remove a primeira ocorrência do <see cref="T:System.Windows.Media.Media3D.Model3D" /> especificado do <see cref="T:System.Windows.Media.Media3D.Model3DCollection" />.</summary>
		/// <param name="value">O Model3D a ser removido desta coleção.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> tiver sido removido da coleção; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060033AE RID: 13230 RVA: 0x000CD888 File Offset: 0x000CCC88
		public bool Remove(Model3D value)
		{
			base.WritePreamble();
			int num = this.IndexOf(value);
			if (num >= 0)
			{
				Model3D oldValue = this._collection[num];
				base.OnFreezablePropertyChanged(oldValue, null);
				this._collection.RemoveAt(num);
				this.OnRemove(oldValue);
				this._version += 1U;
				base.WritePostscript();
				return true;
			}
			return false;
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Media3D.Model3D" /> na posição de índice especificada da <see cref="T:System.Windows.Media.Media3D.Model3DCollection" />.</summary>
		/// <param name="index">A posição do índice do Model3D a ser removido.</param>
		// Token: 0x060033AF RID: 13231 RVA: 0x000CD8E8 File Offset: 0x000CCCE8
		public void RemoveAt(int index)
		{
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x000CD904 File Offset: 0x000CCD04
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			base.WritePreamble();
			Model3D oldValue = this._collection[index];
			base.OnFreezablePropertyChanged(oldValue, null);
			this._collection.RemoveAt(index);
			this.OnRemove(oldValue);
			this._version += 1U;
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Media3D.Model3D" /> no índice de base zero especificado.</summary>
		/// <param name="index">O índice de base zero do objeto Model3D a ser obtido ou definido.</param>
		/// <returns>O item no índice especificado.</returns>
		// Token: 0x17000A82 RID: 2690
		public Model3D this[int index]
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
					Model3D oldValue = this._collection[index];
					base.OnFreezablePropertyChanged(oldValue, value);
					this._collection[index] = value;
					this.OnSet(oldValue, value);
				}
				this._version += 1U;
				base.WritePostscript();
			}
		}

		/// <summary>Obtém o número de objetos <see cref="T:System.Windows.Media.Media3D.Model3D" /> contidos no <see cref="T:System.Windows.Media.Media3D.Model3DCollection" />.</summary>
		/// <returns>O número de <see cref="T:System.Windows.Media.Media3D.Model3D" /> objetos contidos no <see cref="T:System.Windows.Media.Media3D.Model3DCollection" />.</returns>
		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x060033B3 RID: 13235 RVA: 0x000CD9E4 File Offset: 0x000CCDE4
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Copia os itens desse <see cref="T:System.Windows.Media.Media3D.Model3DCollection" />, começando com o valor de índice especificado, em uma matriz de objetos <see cref="T:System.Windows.Media.Media3D.Model3D" />.</summary>
		/// <param name="array">A matriz que é o destino dos itens copiados desta Model3DCollection.</param>
		/// <param name="index">O índice no qual a cópia é iniciada.</param>
		// Token: 0x060033B4 RID: 13236 RVA: 0x000CDA04 File Offset: 0x000CCE04
		public void CopyTo(Model3D[] array, int index)
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

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x060033B5 RID: 13237 RVA: 0x000CDA54 File Offset: 0x000CCE54
		bool ICollection<Model3D>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Retorna um enumerador que pode iterar pela coleção.</summary>
		/// <returns>Um enumerador que pode iterar por meio da coleção.</returns>
		// Token: 0x060033B6 RID: 13238 RVA: 0x000CDA70 File Offset: 0x000CCE70
		public Model3DCollection.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new Model3DCollection.Enumerator(this);
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x000CDA8C File Offset: 0x000CCE8C
		IEnumerator<Model3D> IEnumerable<Model3D>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.Media3D.Model3DCollection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x060033B8 RID: 13240 RVA: 0x000CDAA4 File Offset: 0x000CCEA4
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<Model3D>)this).IsReadOnly;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.Media3D.Model3DCollection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x060033B9 RID: 13241 RVA: 0x000CDAB8 File Offset: 0x000CCEB8
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
		// Token: 0x17000A87 RID: 2695
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
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.Media3D.Model3DCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x060033BC RID: 13244 RVA: 0x000CDB04 File Offset: 0x000CCF04
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.Media3D.Model3DCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.Media3D.Model3DCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060033BD RID: 13245 RVA: 0x000CDB20 File Offset: 0x000CCF20
		bool IList.Contains(object value)
		{
			return this.Contains(value as Model3D);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.Media3D.Model3DCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x060033BE RID: 13246 RVA: 0x000CDB3C File Offset: 0x000CCF3C
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as Model3D);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.Media3D.Model3DCollection" />.</param>
		// Token: 0x060033BF RID: 13247 RVA: 0x000CDB58 File Offset: 0x000CCF58
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.Media3D.Model3DCollection" />.</param>
		// Token: 0x060033C0 RID: 13248 RVA: 0x000CDB74 File Offset: 0x000CCF74
		void IList.Remove(object value)
		{
			this.Remove(value as Model3D);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.Media3D.Model3DCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x060033C1 RID: 13249 RVA: 0x000CDB90 File Offset: 0x000CCF90
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
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.Media3D.Model3DCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x060033C2 RID: 13250 RVA: 0x000CDC64 File Offset: 0x000CD064
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.Media3D.Model3DCollection" />.</returns>
		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x060033C3 RID: 13251 RVA: 0x000CDC8C File Offset: 0x000CD08C
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
		// Token: 0x060033C4 RID: 13252 RVA: 0x000CDCA0 File Offset: 0x000CD0A0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x060033C5 RID: 13253 RVA: 0x000CDCB8 File Offset: 0x000CD0B8
		internal static Model3DCollection Empty
		{
			get
			{
				if (Model3DCollection.s_empty == null)
				{
					Model3DCollection model3DCollection = new Model3DCollection();
					model3DCollection.Freeze();
					Model3DCollection.s_empty = model3DCollection;
				}
				return Model3DCollection.s_empty;
			}
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x000CDCE4 File Offset: 0x000CD0E4
		internal Model3D Internal_GetItem(int i)
		{
			return this._collection[i];
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x000CDD00 File Offset: 0x000CD100
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

		// Token: 0x060033C8 RID: 13256 RVA: 0x000CDD48 File Offset: 0x000CD148
		private Model3D Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is Model3D))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"Model3D"
				}));
			}
			return (Model3D)value;
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x000CDDAC File Offset: 0x000CD1AC
		private int AddHelper(Model3D value)
		{
			int result = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			return result;
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x000CDDC8 File Offset: 0x000CD1C8
		internal int AddWithoutFiringPublicEvents(Model3D value)
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

		// Token: 0x140001BC RID: 444
		// (add) Token: 0x060033CB RID: 13259 RVA: 0x000CDE20 File Offset: 0x000CD220
		// (remove) Token: 0x060033CC RID: 13260 RVA: 0x000CDE58 File Offset: 0x000CD258
		internal event ItemInsertedHandler ItemInserted;

		// Token: 0x140001BD RID: 445
		// (add) Token: 0x060033CD RID: 13261 RVA: 0x000CDE90 File Offset: 0x000CD290
		// (remove) Token: 0x060033CE RID: 13262 RVA: 0x000CDEC8 File Offset: 0x000CD2C8
		internal event ItemRemovedHandler ItemRemoved;

		// Token: 0x060033CF RID: 13263 RVA: 0x000CDF00 File Offset: 0x000CD300
		private void OnInsert(object item)
		{
			if (this.ItemInserted != null)
			{
				this.ItemInserted(this, item);
			}
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x000CDF24 File Offset: 0x000CD324
		private void OnRemove(object oldValue)
		{
			if (this.ItemRemoved != null)
			{
				this.ItemRemoved(this, oldValue);
			}
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x000CDF48 File Offset: 0x000CD348
		private void OnSet(object oldValue, object newValue)
		{
			this.OnInsert(newValue);
			this.OnRemove(oldValue);
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x000CDF64 File Offset: 0x000CD364
		protected override Freezable CreateInstanceCore()
		{
			return new Model3DCollection();
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x000CDF78 File Offset: 0x000CD378
		protected override void CloneCore(Freezable source)
		{
			Model3DCollection model3DCollection = (Model3DCollection)source;
			base.CloneCore(source);
			int count = model3DCollection._collection.Count;
			this._collection = new FrugalStructList<Model3D>(count);
			for (int i = 0; i < count; i++)
			{
				Model3D model3D = model3DCollection._collection[i].Clone();
				base.OnFreezablePropertyChanged(null, model3D);
				this._collection.Add(model3D);
				this.OnInsert(model3D);
			}
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x000CDFE8 File Offset: 0x000CD3E8
		protected override void CloneCurrentValueCore(Freezable source)
		{
			Model3DCollection model3DCollection = (Model3DCollection)source;
			base.CloneCurrentValueCore(source);
			int count = model3DCollection._collection.Count;
			this._collection = new FrugalStructList<Model3D>(count);
			for (int i = 0; i < count; i++)
			{
				Model3D model3D = model3DCollection._collection[i].CloneCurrentValue();
				base.OnFreezablePropertyChanged(null, model3D);
				this._collection.Add(model3D);
				this.OnInsert(model3D);
			}
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x000CE058 File Offset: 0x000CD458
		protected override void GetAsFrozenCore(Freezable source)
		{
			Model3DCollection model3DCollection = (Model3DCollection)source;
			base.GetAsFrozenCore(source);
			int count = model3DCollection._collection.Count;
			this._collection = new FrugalStructList<Model3D>(count);
			for (int i = 0; i < count; i++)
			{
				Model3D model3D = (Model3D)model3DCollection._collection[i].GetAsFrozen();
				base.OnFreezablePropertyChanged(null, model3D);
				this._collection.Add(model3D);
				this.OnInsert(model3D);
			}
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x000CE0CC File Offset: 0x000CD4CC
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			Model3DCollection model3DCollection = (Model3DCollection)source;
			base.GetCurrentValueAsFrozenCore(source);
			int count = model3DCollection._collection.Count;
			this._collection = new FrugalStructList<Model3D>(count);
			for (int i = 0; i < count; i++)
			{
				Model3D model3D = (Model3D)model3DCollection._collection[i].GetCurrentValueAsFrozen();
				base.OnFreezablePropertyChanged(null, model3D);
				this._collection.Add(model3D);
				this.OnInsert(model3D);
			}
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x000CE140 File Offset: 0x000CD540
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

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.Model3DCollection" />.</summary>
		// Token: 0x060033D8 RID: 13272 RVA: 0x000CE188 File Offset: 0x000CD588
		public Model3DCollection()
		{
			this._collection = default(FrugalStructList<Model3D>);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.Model3DCollection" /> com a capacidade especificada.</summary>
		/// <param name="capacity">Inteiro que especifica a capacidade do Model3DCollection.</param>
		// Token: 0x060033D9 RID: 13273 RVA: 0x000CE1A8 File Offset: 0x000CD5A8
		public Model3DCollection(int capacity)
		{
			this._collection = new FrugalStructList<Model3D>(capacity);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.Model3DCollection" /> usando a coleção especificada.</summary>
		/// <param name="collection">Coleção com a qual criar uma instância de Model3DCollection.</param>
		// Token: 0x060033DA RID: 13274 RVA: 0x000CE1C8 File Offset: 0x000CD5C8
		public Model3DCollection(IEnumerable<Model3D> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				bool flag = true;
				ICollection<Model3D> collection2 = collection as ICollection<Model3D>;
				if (collection2 != null)
				{
					this._collection = new FrugalStructList<Model3D>(collection2);
				}
				else
				{
					ICollection collection3 = collection as ICollection;
					if (collection3 != null)
					{
						this._collection = new FrugalStructList<Model3D>(collection3);
					}
					else
					{
						this._collection = default(FrugalStructList<Model3D>);
						foreach (Model3D model3D in collection)
						{
							if (model3D == null)
							{
								throw new ArgumentException(SR.Get("Collection_NoNull"));
							}
							Model3D model3D2 = model3D;
							base.OnFreezablePropertyChanged(null, model3D2);
							this._collection.Add(model3D2);
							this.OnInsert(model3D2);
						}
						flag = false;
					}
				}
				if (flag)
				{
					foreach (Model3D model3D3 in collection)
					{
						if (model3D3 == null)
						{
							throw new ArgumentException(SR.Get("Collection_NoNull"));
						}
						base.OnFreezablePropertyChanged(null, model3D3);
						this.OnInsert(model3D3);
					}
				}
				base.WritePostscript();
				return;
			}
			throw new ArgumentNullException("collection");
		}

		// Token: 0x040015F8 RID: 5624
		private static Model3DCollection s_empty;

		// Token: 0x040015F9 RID: 5625
		internal FrugalStructList<Model3D> _collection;

		// Token: 0x040015FA RID: 5626
		internal uint _version;

		/// <summary>Enumera os itens em uma coleção.</summary>
		// Token: 0x020008AF RID: 2223
		public struct Enumerator : IEnumerator, IEnumerator<Model3D>, IDisposable
		{
			// Token: 0x06005891 RID: 22673 RVA: 0x00168084 File Offset: 0x00167484
			internal Enumerator(Model3DCollection list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = null;
			}

			/// <summary>Este membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x06005892 RID: 22674 RVA: 0x001680B4 File Offset: 0x001674B4
			void IDisposable.Dispose()
			{
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>Retorna true se o enumerador foi avançado com êxito para o próximo elemento; retorna false se o enumerador passou o final da coleção.</returns>
			// Token: 0x06005893 RID: 22675 RVA: 0x001680C4 File Offset: 0x001674C4
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					Model3DCollection list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._current = list._collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Define o enumerador com sua posição inicial, que é antes do primeiro elemento da coleção.</summary>
			// Token: 0x06005894 RID: 22676 RVA: 0x00168158 File Offset: 0x00167558
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
			// Token: 0x17001242 RID: 4674
			// (get) Token: 0x06005895 RID: 22677 RVA: 0x0016819C File Offset: 0x0016759C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém o elemento atual na coleção.</summary>
			/// <returns>Elemento atual na coleção.</returns>
			// Token: 0x17001243 RID: 4675
			// (get) Token: 0x06005896 RID: 22678 RVA: 0x001681B0 File Offset: 0x001675B0
			public Model3D Current
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

			// Token: 0x04002910 RID: 10512
			private Model3D _current;

			// Token: 0x04002911 RID: 10513
			private Model3DCollection _list;

			// Token: 0x04002912 RID: 10514
			private uint _version;

			// Token: 0x04002913 RID: 10515
			private int _index;
		}
	}
}
