using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using MS.Internal.Collections;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media.Media3D
{
	/// <summary>Representa uma coleção ordenada de objetos de <see cref="T:System.Windows.Media.Media3D.Transform3D" />.</summary>
	// Token: 0x02000493 RID: 1171
	public sealed class Transform3DCollection : Animatable, IList, ICollection, IEnumerable, IList<Transform3D>, ICollection<Transform3D>, IEnumerable<Transform3D>
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060033F9 RID: 13305 RVA: 0x000CE8D0 File Offset: 0x000CDCD0
		public new Transform3DCollection Clone()
		{
			return (Transform3DCollection)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060033FA RID: 13306 RVA: 0x000CE8E8 File Offset: 0x000CDCE8
		public new Transform3DCollection CloneCurrentValue()
		{
			return (Transform3DCollection)base.CloneCurrentValue();
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Media3D.Transform3D" /> ao final do <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" />.</summary>
		/// <param name="value">O item a ser adicionado ao fim dessa coleção.</param>
		// Token: 0x060033FB RID: 13307 RVA: 0x000CE900 File Offset: 0x000CDD00
		public void Add(Transform3D value)
		{
			this.AddHelper(value);
		}

		/// <summary>Remove todos os itens desta <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" />.</summary>
		// Token: 0x060033FC RID: 13308 RVA: 0x000CE918 File Offset: 0x000CDD18
		public void Clear()
		{
			base.WritePreamble();
			FrugalStructList<Transform3D> collection = this._collection;
			this._collection = new FrugalStructList<Transform3D>(this._collection.Capacity);
			for (int i = collection.Count - 1; i >= 0; i--)
			{
				base.OnFreezablePropertyChanged(collection[i], null);
				this.OnRemove(collection[i]);
			}
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Media.Media3D.Transform3D" /> especificado está neste <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" />.</summary>
		/// <param name="value">O item a ser localizado nesta coleção.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" />, o Transform3D especificado, estiver nesse Transform3DCollection; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060033FD RID: 13309 RVA: 0x000CE990 File Offset: 0x000CDD90
		public bool Contains(Transform3D value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Obtém a posição de índice da primeira ocorrência do <see cref="T:System.Windows.Media.Media3D.Transform3D" /> especificado.</summary>
		/// <param name="value">O Transform3D a ser pesquisado.</param>
		/// <returns>A posição do índice do Transform3D especificado.</returns>
		// Token: 0x060033FE RID: 13310 RVA: 0x000CE9B0 File Offset: 0x000CDDB0
		public int IndexOf(Transform3D value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Media3D.Transform3D" /> nesta <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" /> na posição do índice especificada.</summary>
		/// <param name="index">A posição do índice na qual inserir o Transform3D especificado.</param>
		/// <param name="value">O Transform3D a ser inserido.</param>
		// Token: 0x060033FF RID: 13311 RVA: 0x000CE9D0 File Offset: 0x000CDDD0
		public void Insert(int index, Transform3D value)
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

		/// <summary>Remove a primeira ocorrência do <see cref="T:System.Windows.Media.Media3D.Transform3D" /> especificado do <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" />.</summary>
		/// <param name="value">O Transform3D a ser removido dessa coleção.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> tiver sido removido da coleção; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003400 RID: 13312 RVA: 0x000CEA28 File Offset: 0x000CDE28
		public bool Remove(Transform3D value)
		{
			base.WritePreamble();
			int num = this.IndexOf(value);
			if (num >= 0)
			{
				Transform3D oldValue = this._collection[num];
				base.OnFreezablePropertyChanged(oldValue, null);
				this._collection.RemoveAt(num);
				this.OnRemove(oldValue);
				this._version += 1U;
				base.WritePostscript();
				return true;
			}
			return false;
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Media3D.Transform3D" /> na posição de índice especificada da <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" />.</summary>
		/// <param name="index">A posição do índice do Transform3D a ser removido.</param>
		// Token: 0x06003401 RID: 13313 RVA: 0x000CEA88 File Offset: 0x000CDE88
		public void RemoveAt(int index)
		{
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x000CEAA4 File Offset: 0x000CDEA4
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			base.WritePreamble();
			Transform3D oldValue = this._collection[index];
			base.OnFreezablePropertyChanged(oldValue, null);
			this._collection.RemoveAt(index);
			this.OnRemove(oldValue);
			this._version += 1U;
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Media3D.Transform3D" /> no índice de base zero especificado.</summary>
		/// <param name="index">O índice baseado em zero do objeto Transform3D a ser obtido ou definido.</param>
		/// <returns>O item no índice especificado.</returns>
		// Token: 0x17000A8B RID: 2699
		public Transform3D this[int index]
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
					Transform3D oldValue = this._collection[index];
					base.OnFreezablePropertyChanged(oldValue, value);
					this._collection[index] = value;
					this.OnSet(oldValue, value);
				}
				this._version += 1U;
				base.WritePostscript();
			}
		}

		/// <summary>Obtém o número de objetos <see cref="T:System.Windows.Media.Media3D.Transform3D" /> contidos no <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" />.</summary>
		/// <returns>O número de objetos de Transform3D contidos no Transform3DCollection.</returns>
		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06003405 RID: 13317 RVA: 0x000CEB84 File Offset: 0x000CDF84
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Copia os itens desse <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" />, começando com o valor de índice especificado, em uma matriz de objetos <see cref="T:System.Windows.Media.Media3D.Transform3D" />.</summary>
		/// <param name="array">A matriz que é o destino dos itens copiados deste Transform3DCollection.</param>
		/// <param name="index">O índice no qual a cópia é iniciada.</param>
		// Token: 0x06003406 RID: 13318 RVA: 0x000CEBA4 File Offset: 0x000CDFA4
		public void CopyTo(Transform3D[] array, int index)
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

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06003407 RID: 13319 RVA: 0x000CEBF4 File Offset: 0x000CDFF4
		bool ICollection<Transform3D>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Retorna um enumerador que pode iterar pela coleção.</summary>
		/// <returns>Um enumerador que pode iterar a coleção.</returns>
		// Token: 0x06003408 RID: 13320 RVA: 0x000CEC10 File Offset: 0x000CE010
		public Transform3DCollection.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new Transform3DCollection.Enumerator(this);
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x000CEC2C File Offset: 0x000CE02C
		IEnumerator<Transform3D> IEnumerable<Transform3D>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x0600340A RID: 13322 RVA: 0x000CEC44 File Offset: 0x000CE044
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<Transform3D>)this).IsReadOnly;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</returns>
		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x0600340B RID: 13323 RVA: 0x000CEC58 File Offset: 0x000CE058
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
		// Token: 0x17000A90 RID: 2704
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
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x0600340E RID: 13326 RVA: 0x000CECA4 File Offset: 0x000CE0A4
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600340F RID: 13327 RVA: 0x000CECC0 File Offset: 0x000CE0C0
		bool IList.Contains(object value)
		{
			return this.Contains(value as Transform3D);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06003410 RID: 13328 RVA: 0x000CECDC File Offset: 0x000CE0DC
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as Transform3D);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" />.</param>
		// Token: 0x06003411 RID: 13329 RVA: 0x000CECF8 File Offset: 0x000CE0F8
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" />.</param>
		// Token: 0x06003412 RID: 13330 RVA: 0x000CED14 File Offset: 0x000CE114
		void IList.Remove(object value)
		{
			this.Remove(value as Transform3D);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x06003413 RID: 13331 RVA: 0x000CED30 File Offset: 0x000CE130
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
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06003414 RID: 13332 RVA: 0x000CEE04 File Offset: 0x000CE204
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" />.</returns>
		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06003415 RID: 13333 RVA: 0x000CEE2C File Offset: 0x000CE22C
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
		// Token: 0x06003416 RID: 13334 RVA: 0x000CEE40 File Offset: 0x000CE240
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06003417 RID: 13335 RVA: 0x000CEE58 File Offset: 0x000CE258
		internal static Transform3DCollection Empty
		{
			get
			{
				if (Transform3DCollection.s_empty == null)
				{
					Transform3DCollection transform3DCollection = new Transform3DCollection();
					transform3DCollection.Freeze();
					Transform3DCollection.s_empty = transform3DCollection;
				}
				return Transform3DCollection.s_empty;
			}
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x000CEE84 File Offset: 0x000CE284
		internal Transform3D Internal_GetItem(int i)
		{
			return this._collection[i];
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x000CEEA0 File Offset: 0x000CE2A0
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

		// Token: 0x0600341A RID: 13338 RVA: 0x000CEEE8 File Offset: 0x000CE2E8
		private Transform3D Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is Transform3D))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"Transform3D"
				}));
			}
			return (Transform3D)value;
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x000CEF4C File Offset: 0x000CE34C
		private int AddHelper(Transform3D value)
		{
			int result = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			return result;
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x000CEF68 File Offset: 0x000CE368
		internal int AddWithoutFiringPublicEvents(Transform3D value)
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

		// Token: 0x140001BE RID: 446
		// (add) Token: 0x0600341D RID: 13341 RVA: 0x000CEFC0 File Offset: 0x000CE3C0
		// (remove) Token: 0x0600341E RID: 13342 RVA: 0x000CEFF8 File Offset: 0x000CE3F8
		internal event ItemInsertedHandler ItemInserted;

		// Token: 0x140001BF RID: 447
		// (add) Token: 0x0600341F RID: 13343 RVA: 0x000CF030 File Offset: 0x000CE430
		// (remove) Token: 0x06003420 RID: 13344 RVA: 0x000CF068 File Offset: 0x000CE468
		internal event ItemRemovedHandler ItemRemoved;

		// Token: 0x06003421 RID: 13345 RVA: 0x000CF0A0 File Offset: 0x000CE4A0
		private void OnInsert(object item)
		{
			if (this.ItemInserted != null)
			{
				this.ItemInserted(this, item);
			}
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x000CF0C4 File Offset: 0x000CE4C4
		private void OnRemove(object oldValue)
		{
			if (this.ItemRemoved != null)
			{
				this.ItemRemoved(this, oldValue);
			}
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x000CF0E8 File Offset: 0x000CE4E8
		private void OnSet(object oldValue, object newValue)
		{
			this.OnInsert(newValue);
			this.OnRemove(oldValue);
		}

		// Token: 0x06003424 RID: 13348 RVA: 0x000CF104 File Offset: 0x000CE504
		protected override Freezable CreateInstanceCore()
		{
			return new Transform3DCollection();
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x000CF118 File Offset: 0x000CE518
		protected override void CloneCore(Freezable source)
		{
			Transform3DCollection transform3DCollection = (Transform3DCollection)source;
			base.CloneCore(source);
			int count = transform3DCollection._collection.Count;
			this._collection = new FrugalStructList<Transform3D>(count);
			for (int i = 0; i < count; i++)
			{
				Transform3D transform3D = transform3DCollection._collection[i].Clone();
				base.OnFreezablePropertyChanged(null, transform3D);
				this._collection.Add(transform3D);
				this.OnInsert(transform3D);
			}
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x000CF188 File Offset: 0x000CE588
		protected override void CloneCurrentValueCore(Freezable source)
		{
			Transform3DCollection transform3DCollection = (Transform3DCollection)source;
			base.CloneCurrentValueCore(source);
			int count = transform3DCollection._collection.Count;
			this._collection = new FrugalStructList<Transform3D>(count);
			for (int i = 0; i < count; i++)
			{
				Transform3D transform3D = transform3DCollection._collection[i].CloneCurrentValue();
				base.OnFreezablePropertyChanged(null, transform3D);
				this._collection.Add(transform3D);
				this.OnInsert(transform3D);
			}
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x000CF1F8 File Offset: 0x000CE5F8
		protected override void GetAsFrozenCore(Freezable source)
		{
			Transform3DCollection transform3DCollection = (Transform3DCollection)source;
			base.GetAsFrozenCore(source);
			int count = transform3DCollection._collection.Count;
			this._collection = new FrugalStructList<Transform3D>(count);
			for (int i = 0; i < count; i++)
			{
				Transform3D transform3D = (Transform3D)transform3DCollection._collection[i].GetAsFrozen();
				base.OnFreezablePropertyChanged(null, transform3D);
				this._collection.Add(transform3D);
				this.OnInsert(transform3D);
			}
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x000CF26C File Offset: 0x000CE66C
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			Transform3DCollection transform3DCollection = (Transform3DCollection)source;
			base.GetCurrentValueAsFrozenCore(source);
			int count = transform3DCollection._collection.Count;
			this._collection = new FrugalStructList<Transform3D>(count);
			for (int i = 0; i < count; i++)
			{
				Transform3D transform3D = (Transform3D)transform3DCollection._collection[i].GetCurrentValueAsFrozen();
				base.OnFreezablePropertyChanged(null, transform3D);
				this._collection.Add(transform3D);
				this.OnInsert(transform3D);
			}
		}

		// Token: 0x06003429 RID: 13353 RVA: 0x000CF2E0 File Offset: 0x000CE6E0
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

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" />.</summary>
		// Token: 0x0600342A RID: 13354 RVA: 0x000CF328 File Offset: 0x000CE728
		public Transform3DCollection()
		{
			this._collection = default(FrugalStructList<Transform3D>);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" /> com a capacidade especificada.</summary>
		/// <param name="capacity">Inteiro que especifica a capacidade do Transform3DCollection.</param>
		// Token: 0x0600342B RID: 13355 RVA: 0x000CF348 File Offset: 0x000CE748
		public Transform3DCollection(int capacity)
		{
			this._collection = new FrugalStructList<Transform3D>(capacity);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" /> usando a coleção especificada.</summary>
		/// <param name="collection">Coleção com a qual criar uma instância de Transform3DCollection.</param>
		// Token: 0x0600342C RID: 13356 RVA: 0x000CF368 File Offset: 0x000CE768
		public Transform3DCollection(IEnumerable<Transform3D> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				bool flag = true;
				ICollection<Transform3D> collection2 = collection as ICollection<Transform3D>;
				if (collection2 != null)
				{
					this._collection = new FrugalStructList<Transform3D>(collection2);
				}
				else
				{
					ICollection collection3 = collection as ICollection;
					if (collection3 != null)
					{
						this._collection = new FrugalStructList<Transform3D>(collection3);
					}
					else
					{
						this._collection = default(FrugalStructList<Transform3D>);
						foreach (Transform3D transform3D in collection)
						{
							if (transform3D == null)
							{
								throw new ArgumentException(SR.Get("Collection_NoNull"));
							}
							Transform3D transform3D2 = transform3D;
							base.OnFreezablePropertyChanged(null, transform3D2);
							this._collection.Add(transform3D2);
							this.OnInsert(transform3D2);
						}
						flag = false;
					}
				}
				if (flag)
				{
					foreach (Transform3D transform3D3 in collection)
					{
						if (transform3D3 == null)
						{
							throw new ArgumentException(SR.Get("Collection_NoNull"));
						}
						base.OnFreezablePropertyChanged(null, transform3D3);
						this.OnInsert(transform3D3);
					}
				}
				base.WritePostscript();
				return;
			}
			throw new ArgumentNullException("collection");
		}

		// Token: 0x040015FD RID: 5629
		private static Transform3DCollection s_empty;

		// Token: 0x040015FE RID: 5630
		internal FrugalStructList<Transform3D> _collection;

		// Token: 0x040015FF RID: 5631
		internal uint _version;

		/// <summary>Enumera itens <see cref="T:System.Windows.Media.Media3D.Transform3D" /> em um <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" />.</summary>
		// Token: 0x020008B0 RID: 2224
		public struct Enumerator : IEnumerator, IEnumerator<Transform3D>, IDisposable
		{
			// Token: 0x06005897 RID: 22679 RVA: 0x001681F8 File Offset: 0x001675F8
			internal Enumerator(Transform3DCollection list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = null;
			}

			/// <summary>Este membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x06005898 RID: 22680 RVA: 0x00168228 File Offset: 0x00167628
			void IDisposable.Dispose()
			{
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador foi avançado com êxito para o próximo elemento; caso contrário, <see langword="false" />.</returns>
			// Token: 0x06005899 RID: 22681 RVA: 0x00168238 File Offset: 0x00167638
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					Transform3DCollection list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._current = list._collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Redefine o enumerador com sua posição inicial, que é antes do primeiro elemento da coleção.</summary>
			// Token: 0x0600589A RID: 22682 RVA: 0x001682CC File Offset: 0x001676CC
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
			// Token: 0x17001244 RID: 4676
			// (get) Token: 0x0600589B RID: 22683 RVA: 0x00168310 File Offset: 0x00167710
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém o elemento atual na coleção.</summary>
			/// <returns>O elemento atual na coleção.</returns>
			// Token: 0x17001245 RID: 4677
			// (get) Token: 0x0600589C RID: 22684 RVA: 0x00168324 File Offset: 0x00167724
			public Transform3D Current
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

			// Token: 0x04002914 RID: 10516
			private Transform3D _current;

			// Token: 0x04002915 RID: 10517
			private Transform3DCollection _list;

			// Token: 0x04002916 RID: 10518
			private uint _version;

			// Token: 0x04002917 RID: 10519
			private int _index;
		}
	}
}
