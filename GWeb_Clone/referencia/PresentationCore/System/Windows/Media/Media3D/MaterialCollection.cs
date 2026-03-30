using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using MS.Internal.Collections;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media.Media3D
{
	/// <summary>Coleção de objetos <see cref="T:System.Windows.Media.Media3D.Material" />.</summary>
	// Token: 0x0200048A RID: 1162
	public sealed class MaterialCollection : Animatable, IList, ICollection, IEnumerable, IList<Material>, ICollection<Material>, IEnumerable<Material>
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.MaterialCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x0600336E RID: 13166 RVA: 0x000CCA50 File Offset: 0x000CBE50
		public new MaterialCollection Clone()
		{
			return (MaterialCollection)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.MaterialCollection" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x0600336F RID: 13167 RVA: 0x000CCA68 File Offset: 0x000CBE68
		public new MaterialCollection CloneCurrentValue()
		{
			return (MaterialCollection)base.CloneCurrentValue();
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Media3D.Material" /> ao final do <see cref="T:System.Windows.Media.Media3D.MaterialCollection" />.</summary>
		/// <param name="value">O item a ser adicionado ao fim dessa coleção.</param>
		// Token: 0x06003370 RID: 13168 RVA: 0x000CCA80 File Offset: 0x000CBE80
		public void Add(Material value)
		{
			this.AddHelper(value);
		}

		/// <summary>Remove todos os itens desta <see cref="T:System.Windows.Media.Media3D.MaterialCollection" />.</summary>
		// Token: 0x06003371 RID: 13169 RVA: 0x000CCA98 File Offset: 0x000CBE98
		public void Clear()
		{
			base.WritePreamble();
			FrugalStructList<Material> collection = this._collection;
			this._collection = new FrugalStructList<Material>(this._collection.Capacity);
			for (int i = collection.Count - 1; i >= 0; i--)
			{
				base.OnFreezablePropertyChanged(collection[i], null);
				this.OnRemove(collection[i]);
			}
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Determina se um determinado <see cref="T:System.Windows.Media.Media3D.Material" /> está nesta <see cref="T:System.Windows.Media.Media3D.MaterialCollection" />.</summary>
		/// <param name="value">O item a ser localizado nesta coleção.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" />, o Material especificado, estará nesta MaterialCollection; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003372 RID: 13170 RVA: 0x000CCB10 File Offset: 0x000CBF10
		public bool Contains(Material value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Obtém a posição de índice da primeira ocorrência do <see cref="T:System.Windows.Media.Media3D.Material" /> especificado.</summary>
		/// <param name="value">O Material pelo qual pesquisar.</param>
		/// <returns>A posição do índice do Material especificado.</returns>
		// Token: 0x06003373 RID: 13171 RVA: 0x000CCB30 File Offset: 0x000CBF30
		public int IndexOf(Material value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Media3D.Material" /> nesta <see cref="T:System.Windows.Media.Media3D.MaterialCollection" /> na posição do índice especificada.</summary>
		/// <param name="index">A posição do índice na qual inserir o Material especificado.</param>
		/// <param name="value">O Material a inserir.</param>
		// Token: 0x06003374 RID: 13172 RVA: 0x000CCB50 File Offset: 0x000CBF50
		public void Insert(int index, Material value)
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

		/// <summary>Remove a primeira ocorrência do <see cref="T:System.Windows.Media.Media3D.Material" /> especificado do <see cref="T:System.Windows.Media.Media3D.MaterialCollection" />.</summary>
		/// <param name="value">O Material a ser removido desta coleção.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> tiver sido removido da coleção; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003375 RID: 13173 RVA: 0x000CCBA8 File Offset: 0x000CBFA8
		public bool Remove(Material value)
		{
			base.WritePreamble();
			int num = this.IndexOf(value);
			if (num >= 0)
			{
				Material oldValue = this._collection[num];
				base.OnFreezablePropertyChanged(oldValue, null);
				this._collection.RemoveAt(num);
				this.OnRemove(oldValue);
				this._version += 1U;
				base.WritePostscript();
				return true;
			}
			return false;
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Media3D.Material" /> na posição de índice especificada da <see cref="T:System.Windows.Media.Media3D.MaterialCollection" />.</summary>
		/// <param name="index">A posição do índice do Material a ser removido.</param>
		// Token: 0x06003376 RID: 13174 RVA: 0x000CCC08 File Offset: 0x000CC008
		public void RemoveAt(int index)
		{
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x000CCC24 File Offset: 0x000CC024
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			base.WritePreamble();
			Material oldValue = this._collection[index];
			base.OnFreezablePropertyChanged(oldValue, null);
			this._collection.RemoveAt(index);
			this.OnRemove(oldValue);
			this._version += 1U;
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Media3D.Material" /> no índice de base zero especificado.</summary>
		/// <param name="index">O índice de base zero do objeto Material a ser obtido ou definido.</param>
		/// <returns>O item no índice especificado.</returns>
		// Token: 0x17000A79 RID: 2681
		public Material this[int index]
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
					Material oldValue = this._collection[index];
					base.OnFreezablePropertyChanged(oldValue, value);
					this._collection[index] = value;
					this.OnSet(oldValue, value);
				}
				this._version += 1U;
				base.WritePostscript();
			}
		}

		/// <summary>Obtém o número de objetos <see cref="T:System.Windows.Media.Media3D.Material" /> contidos no <see cref="T:System.Windows.Media.Media3D.MaterialCollection" />.</summary>
		/// <returns>O número de objetos Material contido no <see cref="T:System.Windows.Media.Media3D.MaterialCollection" />.</returns>
		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x0600337A RID: 13178 RVA: 0x000CCD04 File Offset: 0x000CC104
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Copia os itens desse <see cref="T:System.Windows.Media.Media3D.MaterialCollection" />, começando com o valor de índice especificado, em uma matriz de objetos <see cref="T:System.Windows.Media.Media3D.Material" />.</summary>
		/// <param name="array">A matriz que é o destino dos itens copiados desta MaterialCollection.</param>
		/// <param name="index">O índice no qual a cópia é iniciada.</param>
		// Token: 0x0600337B RID: 13179 RVA: 0x000CCD24 File Offset: 0x000CC124
		public void CopyTo(Material[] array, int index)
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

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x0600337C RID: 13180 RVA: 0x000CCD74 File Offset: 0x000CC174
		bool ICollection<Material>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Retorna um enumerador que pode iterar pela coleção.</summary>
		/// <returns>Um enumerador que pode iterar a coleção.</returns>
		// Token: 0x0600337D RID: 13181 RVA: 0x000CCD90 File Offset: 0x000CC190
		public MaterialCollection.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new MaterialCollection.Enumerator(this);
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x000CCDAC File Offset: 0x000CC1AC
		IEnumerator<Material> IEnumerable<Material>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.Media3D.MaterialCollection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x0600337F RID: 13183 RVA: 0x000CCDC4 File Offset: 0x000CC1C4
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<Material>)this).IsReadOnly;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.Media3D.MaterialCollection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06003380 RID: 13184 RVA: 0x000CCDD8 File Offset: 0x000CC1D8
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
		// Token: 0x17000A7E RID: 2686
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
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.Media3D.MaterialCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x06003383 RID: 13187 RVA: 0x000CCE24 File Offset: 0x000CC224
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.Media3D.MaterialCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.Media3D.MaterialCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003384 RID: 13188 RVA: 0x000CCE40 File Offset: 0x000CC240
		bool IList.Contains(object value)
		{
			return this.Contains(value as Material);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.Media3D.MaterialCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06003385 RID: 13189 RVA: 0x000CCE5C File Offset: 0x000CC25C
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as Material);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.Media3D.MaterialCollection" />.</param>
		// Token: 0x06003386 RID: 13190 RVA: 0x000CCE78 File Offset: 0x000CC278
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.Media3D.MaterialCollection" />.</param>
		// Token: 0x06003387 RID: 13191 RVA: 0x000CCE94 File Offset: 0x000CC294
		void IList.Remove(object value)
		{
			this.Remove(value as Material);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.Media3D.MaterialCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x06003388 RID: 13192 RVA: 0x000CCEB0 File Offset: 0x000CC2B0
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
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.Media3D.MaterialCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06003389 RID: 13193 RVA: 0x000CCF84 File Offset: 0x000CC384
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.Media3D.MaterialCollection" />.</returns>
		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x0600338A RID: 13194 RVA: 0x000CCFAC File Offset: 0x000CC3AC
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
		// Token: 0x0600338B RID: 13195 RVA: 0x000CCFC0 File Offset: 0x000CC3C0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x0600338C RID: 13196 RVA: 0x000CCFD8 File Offset: 0x000CC3D8
		internal static MaterialCollection Empty
		{
			get
			{
				if (MaterialCollection.s_empty == null)
				{
					MaterialCollection materialCollection = new MaterialCollection();
					materialCollection.Freeze();
					MaterialCollection.s_empty = materialCollection;
				}
				return MaterialCollection.s_empty;
			}
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x000CD004 File Offset: 0x000CC404
		internal Material Internal_GetItem(int i)
		{
			return this._collection[i];
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x000CD020 File Offset: 0x000CC420
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

		// Token: 0x0600338F RID: 13199 RVA: 0x000CD068 File Offset: 0x000CC468
		private Material Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is Material))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"Material"
				}));
			}
			return (Material)value;
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x000CD0CC File Offset: 0x000CC4CC
		private int AddHelper(Material value)
		{
			int result = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			return result;
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x000CD0E8 File Offset: 0x000CC4E8
		internal int AddWithoutFiringPublicEvents(Material value)
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

		// Token: 0x140001BA RID: 442
		// (add) Token: 0x06003392 RID: 13202 RVA: 0x000CD140 File Offset: 0x000CC540
		// (remove) Token: 0x06003393 RID: 13203 RVA: 0x000CD178 File Offset: 0x000CC578
		internal event ItemInsertedHandler ItemInserted;

		// Token: 0x140001BB RID: 443
		// (add) Token: 0x06003394 RID: 13204 RVA: 0x000CD1B0 File Offset: 0x000CC5B0
		// (remove) Token: 0x06003395 RID: 13205 RVA: 0x000CD1E8 File Offset: 0x000CC5E8
		internal event ItemRemovedHandler ItemRemoved;

		// Token: 0x06003396 RID: 13206 RVA: 0x000CD220 File Offset: 0x000CC620
		private void OnInsert(object item)
		{
			if (this.ItemInserted != null)
			{
				this.ItemInserted(this, item);
			}
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x000CD244 File Offset: 0x000CC644
		private void OnRemove(object oldValue)
		{
			if (this.ItemRemoved != null)
			{
				this.ItemRemoved(this, oldValue);
			}
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x000CD268 File Offset: 0x000CC668
		private void OnSet(object oldValue, object newValue)
		{
			this.OnInsert(newValue);
			this.OnRemove(oldValue);
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x000CD284 File Offset: 0x000CC684
		protected override Freezable CreateInstanceCore()
		{
			return new MaterialCollection();
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x000CD298 File Offset: 0x000CC698
		protected override void CloneCore(Freezable source)
		{
			MaterialCollection materialCollection = (MaterialCollection)source;
			base.CloneCore(source);
			int count = materialCollection._collection.Count;
			this._collection = new FrugalStructList<Material>(count);
			for (int i = 0; i < count; i++)
			{
				Material material = materialCollection._collection[i].Clone();
				base.OnFreezablePropertyChanged(null, material);
				this._collection.Add(material);
				this.OnInsert(material);
			}
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x000CD308 File Offset: 0x000CC708
		protected override void CloneCurrentValueCore(Freezable source)
		{
			MaterialCollection materialCollection = (MaterialCollection)source;
			base.CloneCurrentValueCore(source);
			int count = materialCollection._collection.Count;
			this._collection = new FrugalStructList<Material>(count);
			for (int i = 0; i < count; i++)
			{
				Material material = materialCollection._collection[i].CloneCurrentValue();
				base.OnFreezablePropertyChanged(null, material);
				this._collection.Add(material);
				this.OnInsert(material);
			}
		}

		// Token: 0x0600339C RID: 13212 RVA: 0x000CD378 File Offset: 0x000CC778
		protected override void GetAsFrozenCore(Freezable source)
		{
			MaterialCollection materialCollection = (MaterialCollection)source;
			base.GetAsFrozenCore(source);
			int count = materialCollection._collection.Count;
			this._collection = new FrugalStructList<Material>(count);
			for (int i = 0; i < count; i++)
			{
				Material material = (Material)materialCollection._collection[i].GetAsFrozen();
				base.OnFreezablePropertyChanged(null, material);
				this._collection.Add(material);
				this.OnInsert(material);
			}
		}

		// Token: 0x0600339D RID: 13213 RVA: 0x000CD3EC File Offset: 0x000CC7EC
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			MaterialCollection materialCollection = (MaterialCollection)source;
			base.GetCurrentValueAsFrozenCore(source);
			int count = materialCollection._collection.Count;
			this._collection = new FrugalStructList<Material>(count);
			for (int i = 0; i < count; i++)
			{
				Material material = (Material)materialCollection._collection[i].GetCurrentValueAsFrozen();
				base.OnFreezablePropertyChanged(null, material);
				this._collection.Add(material);
				this.OnInsert(material);
			}
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x000CD460 File Offset: 0x000CC860
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

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.MaterialCollection" />.</summary>
		// Token: 0x0600339F RID: 13215 RVA: 0x000CD4A8 File Offset: 0x000CC8A8
		public MaterialCollection()
		{
			this._collection = default(FrugalStructList<Material>);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.MaterialCollection" /> com a capacidade especificada.</summary>
		/// <param name="capacity">Inteiro que especifica a capacidade da MaterialCollection.</param>
		// Token: 0x060033A0 RID: 13216 RVA: 0x000CD4C8 File Offset: 0x000CC8C8
		public MaterialCollection(int capacity)
		{
			this._collection = new FrugalStructList<Material>(capacity);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.MaterialCollection" /> usando a coleção especificada.</summary>
		/// <param name="collection">Coleção com a qual criar uma instância de MaterialCollection.</param>
		// Token: 0x060033A1 RID: 13217 RVA: 0x000CD4E8 File Offset: 0x000CC8E8
		public MaterialCollection(IEnumerable<Material> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				bool flag = true;
				ICollection<Material> collection2 = collection as ICollection<Material>;
				if (collection2 != null)
				{
					this._collection = new FrugalStructList<Material>(collection2);
				}
				else
				{
					ICollection collection3 = collection as ICollection;
					if (collection3 != null)
					{
						this._collection = new FrugalStructList<Material>(collection3);
					}
					else
					{
						this._collection = default(FrugalStructList<Material>);
						foreach (Material material in collection)
						{
							if (material == null)
							{
								throw new ArgumentException(SR.Get("Collection_NoNull"));
							}
							Material material2 = material;
							base.OnFreezablePropertyChanged(null, material2);
							this._collection.Add(material2);
							this.OnInsert(material2);
						}
						flag = false;
					}
				}
				if (flag)
				{
					foreach (Material material3 in collection)
					{
						if (material3 == null)
						{
							throw new ArgumentException(SR.Get("Collection_NoNull"));
						}
						base.OnFreezablePropertyChanged(null, material3);
						this.OnInsert(material3);
					}
				}
				base.WritePostscript();
				return;
			}
			throw new ArgumentNullException("collection");
		}

		// Token: 0x040015F3 RID: 5619
		private static MaterialCollection s_empty;

		// Token: 0x040015F4 RID: 5620
		internal FrugalStructList<Material> _collection;

		// Token: 0x040015F5 RID: 5621
		internal uint _version;

		/// <summary>Enumera itens <see cref="T:System.Windows.Media.Media3D.Material" /> em um <see cref="T:System.Windows.Media.Media3D.MaterialCollection" />.</summary>
		// Token: 0x020008AE RID: 2222
		public struct Enumerator : IEnumerator, IEnumerator<Material>, IDisposable
		{
			// Token: 0x0600588B RID: 22667 RVA: 0x00167F10 File Offset: 0x00167310
			internal Enumerator(MaterialCollection list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = null;
			}

			/// <summary>Este membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x0600588C RID: 22668 RVA: 0x00167F40 File Offset: 0x00167340
			void IDisposable.Dispose()
			{
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador foi avançado com êxito para o próximo elemento; caso contrário, <see langword="false" />.</returns>
			// Token: 0x0600588D RID: 22669 RVA: 0x00167F50 File Offset: 0x00167350
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					MaterialCollection list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._current = list._collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Redefine o enumerador com sua posição inicial, que é antes do primeiro elemento da coleção.</summary>
			// Token: 0x0600588E RID: 22670 RVA: 0x00167FE4 File Offset: 0x001673E4
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
			// Token: 0x17001240 RID: 4672
			// (get) Token: 0x0600588F RID: 22671 RVA: 0x00168028 File Offset: 0x00167428
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém o elemento atual no <see cref="T:System.Windows.Media.Media3D.MaterialCollection" />.</summary>
			/// <returns>O elemento atual no MaterialCollection.</returns>
			// Token: 0x17001241 RID: 4673
			// (get) Token: 0x06005890 RID: 22672 RVA: 0x0016803C File Offset: 0x0016743C
			public Material Current
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

			// Token: 0x0400290C RID: 10508
			private Material _current;

			// Token: 0x0400290D RID: 10509
			private MaterialCollection _list;

			// Token: 0x0400290E RID: 10510
			private uint _version;

			// Token: 0x0400290F RID: 10511
			private int _index;
		}
	}
}
