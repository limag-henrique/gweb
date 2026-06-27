using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using MS.Internal.Collections;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media
{
	/// <summary>Representa uma coleção ordenada de objetos de <see cref="T:System.Windows.Media.Drawing" />.</summary>
	// Token: 0x02000380 RID: 896
	public sealed class DrawingCollection : Animatable, IList, ICollection, IEnumerable, IList<Drawing>, ICollection<Drawing>, IEnumerable<Drawing>
	{
		// Token: 0x0600208E RID: 8334 RVA: 0x00084A88 File Offset: 0x00083E88
		internal void TransactionalAppend(DrawingCollection collectionToAppend)
		{
			int count = collectionToAppend.Count;
			for (int i = 0; i < count; i++)
			{
				this.AddWithoutFiringPublicEvents(collectionToAppend.Internal_GetItem(i));
			}
			try
			{
				base.FireChanged();
			}
			catch (Exception)
			{
				int num = this.Count - count;
				for (int j = this.Count - 1; j >= num; j--)
				{
					this.RemoveAtWithoutFiringPublicEvents(j);
				}
				throw;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.DrawingCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x0600208F RID: 8335 RVA: 0x00084B04 File Offset: 0x00083F04
		public new DrawingCollection Clone()
		{
			return (DrawingCollection)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.DrawingCollection" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002090 RID: 8336 RVA: 0x00084B1C File Offset: 0x00083F1C
		public new DrawingCollection CloneCurrentValue()
		{
			return (DrawingCollection)base.CloneCurrentValue();
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Drawing" /> ao final do <see cref="T:System.Windows.Media.DrawingCollection" />.</summary>
		/// <param name="value">O item a ser adicionado ao final desta coleção.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.DrawingCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.DrawingCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x06002091 RID: 8337 RVA: 0x00084B34 File Offset: 0x00083F34
		public void Add(Drawing value)
		{
			this.AddHelper(value);
		}

		/// <summary>Remove todos os itens desta <see cref="T:System.Windows.Media.DrawingCollection" />.</summary>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.DrawingCollection" /> é somente leitura.</exception>
		// Token: 0x06002092 RID: 8338 RVA: 0x00084B4C File Offset: 0x00083F4C
		public void Clear()
		{
			base.WritePreamble();
			FrugalStructList<Drawing> collection = this._collection;
			this._collection = new FrugalStructList<Drawing>(this._collection.Capacity);
			for (int i = collection.Count - 1; i >= 0; i--)
			{
				base.OnFreezablePropertyChanged(collection[i], null);
				this.OnRemove(collection[i]);
			}
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Determina se um determinado <see cref="T:System.Windows.Media.Drawing" /> está nesta <see cref="T:System.Windows.Media.DrawingCollection" />.</summary>
		/// <param name="value">O item a ser localizado nesta coleção.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" />, o <see cref="T:System.Windows.Media.Drawing" /> especificado, estiver neste <see cref="T:System.Windows.Media.DrawingCollection" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002093 RID: 8339 RVA: 0x00084BC4 File Offset: 0x00083FC4
		public bool Contains(Drawing value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Obtém a posição de índice da primeira ocorrência do <see cref="T:System.Windows.Media.Drawing" /> especificado.</summary>
		/// <param name="value">O item a ser procurado.</param>
		/// <returns>A posição do índice do <see cref="T:System.Windows.Media.Drawing" /> especificado.</returns>
		// Token: 0x06002094 RID: 8340 RVA: 0x00084BE4 File Offset: 0x00083FE4
		public int IndexOf(Drawing value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Drawing" /> nesta <see cref="T:System.Windows.Media.DrawingCollection" /> na posição do índice especificada.</summary>
		/// <param name="index">A posição do índice na qual inserir <paramref name="value" />, o <see cref="T:System.Windows.Media.Drawing" /> especificado.</param>
		/// <param name="value">O item a ser inserido.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> não é um índice válido no <see cref="T:System.Windows.Media.DrawingCollection" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.DrawingCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.DrawingCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x06002095 RID: 8341 RVA: 0x00084C04 File Offset: 0x00084004
		public void Insert(int index, Drawing value)
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

		/// <summary>Remove a primeira ocorrência do <see cref="T:System.Windows.Media.Drawing" /> especificado do <see cref="T:System.Windows.Media.DrawingCollection" />.</summary>
		/// <param name="value">O item a ser removido desta coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a operação tiver sido bem-sucedida; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.DrawingCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.DrawingCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x06002096 RID: 8342 RVA: 0x00084C5C File Offset: 0x0008405C
		public bool Remove(Drawing value)
		{
			base.WritePreamble();
			int num = this.IndexOf(value);
			if (num >= 0)
			{
				Drawing oldValue = this._collection[num];
				base.OnFreezablePropertyChanged(oldValue, null);
				this._collection.RemoveAt(num);
				this.OnRemove(oldValue);
				this._version += 1U;
				base.WritePostscript();
				return true;
			}
			return false;
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Drawing" /> na posição de índice especificada da <see cref="T:System.Windows.Media.DrawingCollection" />.</summary>
		/// <param name="index">A posição do índice do item a ser removido.</param>
		// Token: 0x06002097 RID: 8343 RVA: 0x00084CBC File Offset: 0x000840BC
		public void RemoveAt(int index)
		{
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x00084CD8 File Offset: 0x000840D8
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			base.WritePreamble();
			Drawing oldValue = this._collection[index];
			base.OnFreezablePropertyChanged(oldValue, null);
			this._collection.RemoveAt(index);
			this.OnRemove(oldValue);
			this._version += 1U;
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Drawing" /> no índice de base zero especificado.</summary>
		/// <param name="index">O índice baseado em zero do objeto <see cref="T:System.Windows.Media.Drawing" /> a ser obtido ou definido</param>
		/// <returns>O <see cref="T:System.Windows.Media.Drawing" /> no índice especificado.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> não é um índice válido no <see cref="T:System.Windows.Media.DrawingCollection" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.DrawingCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.DrawingCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x17000687 RID: 1671
		public Drawing this[int index]
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
					Drawing oldValue = this._collection[index];
					base.OnFreezablePropertyChanged(oldValue, value);
					this._collection[index] = value;
					this.OnSet(oldValue, value);
				}
				this._version += 1U;
				base.WritePostscript();
			}
		}

		/// <summary>Obtém o número de objetos <see cref="T:System.Windows.Media.Drawing" /> contidos no <see cref="T:System.Windows.Media.DrawingCollection" />.</summary>
		/// <returns>O número de <see cref="T:System.Windows.Media.Drawing" /> objetos contidos no <see cref="T:System.Windows.Media.DrawingCollection" />.</returns>
		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x0600209B RID: 8347 RVA: 0x00084DB8 File Offset: 0x000841B8
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Copia os itens desse <see cref="T:System.Windows.Media.DrawingCollection" />, começando com o valor de índice especificado, em uma matriz de objetos <see cref="T:System.Windows.Media.Drawing" />.</summary>
		/// <param name="array">A matriz que é o destino dos itens copiados deste <see cref="T:System.Windows.Media.DrawingCollection" />.</param>
		/// <param name="index">O índice no qual a cópia é iniciada.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> é multidimensional.  
		///
		/// ou - 
		/// O número de itens na origem <see cref="T:System.Windows.Media.DrawingCollection" /> é maior do que o espaço disponível de <paramref name="index" /> até o final do <paramref name="array" /> de destino.</exception>
		// Token: 0x0600209C RID: 8348 RVA: 0x00084DD8 File Offset: 0x000841D8
		public void CopyTo(Drawing[] array, int index)
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

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x0600209D RID: 8349 RVA: 0x00084E28 File Offset: 0x00084228
		bool ICollection<Drawing>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Retorna um enumerador que pode iterar pela coleção.</summary>
		/// <returns>Um enumerador que pode iterar a coleção.</returns>
		// Token: 0x0600209E RID: 8350 RVA: 0x00084E44 File Offset: 0x00084244
		public DrawingCollection.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new DrawingCollection.Enumerator(this);
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x00084E60 File Offset: 0x00084260
		IEnumerator<Drawing> IEnumerable<Drawing>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.DrawingCollection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060020A0 RID: 8352 RVA: 0x00084E78 File Offset: 0x00084278
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<Drawing>)this).IsReadOnly;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.DrawingCollection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060020A1 RID: 8353 RVA: 0x00084E8C File Offset: 0x0008428C
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
		// Token: 0x1700068C RID: 1676
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
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.DrawingCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x060020A4 RID: 8356 RVA: 0x00084ED8 File Offset: 0x000842D8
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.DrawingCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.DrawingCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060020A5 RID: 8357 RVA: 0x00084EF4 File Offset: 0x000842F4
		bool IList.Contains(object value)
		{
			return this.Contains(value as Drawing);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.DrawingCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x060020A6 RID: 8358 RVA: 0x00084F10 File Offset: 0x00084310
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as Drawing);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.DrawingCollection" />.</param>
		// Token: 0x060020A7 RID: 8359 RVA: 0x00084F2C File Offset: 0x0008432C
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.DrawingCollection" />.</param>
		// Token: 0x060020A8 RID: 8360 RVA: 0x00084F48 File Offset: 0x00084348
		void IList.Remove(object value)
		{
			this.Remove(value as Drawing);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.DrawingCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x060020A9 RID: 8361 RVA: 0x00084F64 File Offset: 0x00084364
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
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.DrawingCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x00085038 File Offset: 0x00084438
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.DrawingCollection" />.</returns>
		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x060020AB RID: 8363 RVA: 0x00085060 File Offset: 0x00084460
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
		// Token: 0x060020AC RID: 8364 RVA: 0x00085074 File Offset: 0x00084474
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x060020AD RID: 8365 RVA: 0x0008508C File Offset: 0x0008448C
		internal static DrawingCollection Empty
		{
			get
			{
				if (DrawingCollection.s_empty == null)
				{
					DrawingCollection drawingCollection = new DrawingCollection();
					drawingCollection.Freeze();
					DrawingCollection.s_empty = drawingCollection;
				}
				return DrawingCollection.s_empty;
			}
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x000850B8 File Offset: 0x000844B8
		internal Drawing Internal_GetItem(int i)
		{
			return this._collection[i];
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x000850D4 File Offset: 0x000844D4
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

		// Token: 0x060020B0 RID: 8368 RVA: 0x0008511C File Offset: 0x0008451C
		private Drawing Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is Drawing))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"Drawing"
				}));
			}
			return (Drawing)value;
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x00085180 File Offset: 0x00084580
		private int AddHelper(Drawing value)
		{
			int result = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			return result;
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x0008519C File Offset: 0x0008459C
		internal int AddWithoutFiringPublicEvents(Drawing value)
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

		// Token: 0x14000194 RID: 404
		// (add) Token: 0x060020B3 RID: 8371 RVA: 0x000851F4 File Offset: 0x000845F4
		// (remove) Token: 0x060020B4 RID: 8372 RVA: 0x0008522C File Offset: 0x0008462C
		internal event ItemInsertedHandler ItemInserted;

		// Token: 0x14000195 RID: 405
		// (add) Token: 0x060020B5 RID: 8373 RVA: 0x00085264 File Offset: 0x00084664
		// (remove) Token: 0x060020B6 RID: 8374 RVA: 0x0008529C File Offset: 0x0008469C
		internal event ItemRemovedHandler ItemRemoved;

		// Token: 0x060020B7 RID: 8375 RVA: 0x000852D4 File Offset: 0x000846D4
		private void OnInsert(object item)
		{
			if (this.ItemInserted != null)
			{
				this.ItemInserted(this, item);
			}
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x000852F8 File Offset: 0x000846F8
		private void OnRemove(object oldValue)
		{
			if (this.ItemRemoved != null)
			{
				this.ItemRemoved(this, oldValue);
			}
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x0008531C File Offset: 0x0008471C
		private void OnSet(object oldValue, object newValue)
		{
			this.OnInsert(newValue);
			this.OnRemove(oldValue);
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x00085338 File Offset: 0x00084738
		protected override Freezable CreateInstanceCore()
		{
			return new DrawingCollection();
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x0008534C File Offset: 0x0008474C
		protected override void CloneCore(Freezable source)
		{
			DrawingCollection drawingCollection = (DrawingCollection)source;
			base.CloneCore(source);
			int count = drawingCollection._collection.Count;
			this._collection = new FrugalStructList<Drawing>(count);
			for (int i = 0; i < count; i++)
			{
				Drawing drawing = drawingCollection._collection[i].Clone();
				base.OnFreezablePropertyChanged(null, drawing);
				this._collection.Add(drawing);
				this.OnInsert(drawing);
			}
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x000853BC File Offset: 0x000847BC
		protected override void CloneCurrentValueCore(Freezable source)
		{
			DrawingCollection drawingCollection = (DrawingCollection)source;
			base.CloneCurrentValueCore(source);
			int count = drawingCollection._collection.Count;
			this._collection = new FrugalStructList<Drawing>(count);
			for (int i = 0; i < count; i++)
			{
				Drawing drawing = drawingCollection._collection[i].CloneCurrentValue();
				base.OnFreezablePropertyChanged(null, drawing);
				this._collection.Add(drawing);
				this.OnInsert(drawing);
			}
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x0008542C File Offset: 0x0008482C
		protected override void GetAsFrozenCore(Freezable source)
		{
			DrawingCollection drawingCollection = (DrawingCollection)source;
			base.GetAsFrozenCore(source);
			int count = drawingCollection._collection.Count;
			this._collection = new FrugalStructList<Drawing>(count);
			for (int i = 0; i < count; i++)
			{
				Drawing drawing = (Drawing)drawingCollection._collection[i].GetAsFrozen();
				base.OnFreezablePropertyChanged(null, drawing);
				this._collection.Add(drawing);
				this.OnInsert(drawing);
			}
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x000854A0 File Offset: 0x000848A0
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			DrawingCollection drawingCollection = (DrawingCollection)source;
			base.GetCurrentValueAsFrozenCore(source);
			int count = drawingCollection._collection.Count;
			this._collection = new FrugalStructList<Drawing>(count);
			for (int i = 0; i < count; i++)
			{
				Drawing drawing = (Drawing)drawingCollection._collection[i].GetCurrentValueAsFrozen();
				base.OnFreezablePropertyChanged(null, drawing);
				this._collection.Add(drawing);
				this.OnInsert(drawing);
			}
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x00085514 File Offset: 0x00084914
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

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.DrawingCollection" />.</summary>
		// Token: 0x060020C0 RID: 8384 RVA: 0x0008555C File Offset: 0x0008495C
		public DrawingCollection()
		{
			this._collection = default(FrugalStructList<Drawing>);
		}

		/// <summary>Inicializa uma nova instância da <see cref="T:System.Windows.Media.DrawingCollection" /> com a capacidade especificada.</summary>
		/// <param name="capacity">A capacidade total da coleção.</param>
		// Token: 0x060020C1 RID: 8385 RVA: 0x0008557C File Offset: 0x0008497C
		public DrawingCollection(int capacity)
		{
			this._collection = new FrugalStructList<Drawing>(capacity);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.DrawingCollection" /> com a coleção especificada de objetos <see cref="T:System.Windows.Media.Drawing" />.</summary>
		/// <param name="collection">A coleção de objetos <see cref="T:System.Windows.Media.Drawing" /> que compõem o <see cref="T:System.Windows.Media.DrawingCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> é <see langword="null" />.</exception>
		// Token: 0x060020C2 RID: 8386 RVA: 0x0008559C File Offset: 0x0008499C
		public DrawingCollection(IEnumerable<Drawing> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				bool flag = true;
				ICollection<Drawing> collection2 = collection as ICollection<Drawing>;
				if (collection2 != null)
				{
					this._collection = new FrugalStructList<Drawing>(collection2);
				}
				else
				{
					ICollection collection3 = collection as ICollection;
					if (collection3 != null)
					{
						this._collection = new FrugalStructList<Drawing>(collection3);
					}
					else
					{
						this._collection = default(FrugalStructList<Drawing>);
						foreach (Drawing drawing in collection)
						{
							if (drawing == null)
							{
								throw new ArgumentException(SR.Get("Collection_NoNull"));
							}
							Drawing drawing2 = drawing;
							base.OnFreezablePropertyChanged(null, drawing2);
							this._collection.Add(drawing2);
							this.OnInsert(drawing2);
						}
						flag = false;
					}
				}
				if (flag)
				{
					foreach (Drawing drawing3 in collection)
					{
						if (drawing3 == null)
						{
							throw new ArgumentException(SR.Get("Collection_NoNull"));
						}
						base.OnFreezablePropertyChanged(null, drawing3);
						this.OnInsert(drawing3);
					}
				}
				base.WritePostscript();
				return;
			}
			throw new ArgumentNullException("collection");
		}

		// Token: 0x0400109F RID: 4255
		private static DrawingCollection s_empty;

		// Token: 0x040010A0 RID: 4256
		internal FrugalStructList<Drawing> _collection;

		// Token: 0x040010A1 RID: 4257
		internal uint _version;

		/// <summary>Enumera itens <see cref="T:System.Windows.Media.Drawing" /> em um <see cref="T:System.Windows.Media.DrawingCollection" />.</summary>
		// Token: 0x02000866 RID: 2150
		public struct Enumerator : IEnumerator, IEnumerator<Drawing>, IDisposable
		{
			// Token: 0x06005739 RID: 22329 RVA: 0x00164AF8 File Offset: 0x00163EF8
			internal Enumerator(DrawingCollection list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = null;
			}

			/// <summary>Este tipo ou membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x0600573A RID: 22330 RVA: 0x00164B28 File Offset: 0x00163F28
			void IDisposable.Dispose()
			{
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador foi avançado com êxito para o próximo elemento; caso contrário, <see langword="false" />.</returns>
			// Token: 0x0600573B RID: 22331 RVA: 0x00164B38 File Offset: 0x00163F38
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					DrawingCollection list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._current = list._collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Redefine o enumerador com sua posição inicial, que é antes do primeiro elemento da coleção.</summary>
			// Token: 0x0600573C RID: 22332 RVA: 0x00164BCC File Offset: 0x00163FCC
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
			// Token: 0x170011F9 RID: 4601
			// (get) Token: 0x0600573D RID: 22333 RVA: 0x00164C10 File Offset: 0x00164010
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém o elemento atual na coleção.</summary>
			/// <returns>O elemento atual na coleção.</returns>
			// Token: 0x170011FA RID: 4602
			// (get) Token: 0x0600573E RID: 22334 RVA: 0x00164C24 File Offset: 0x00164024
			public Drawing Current
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

			// Token: 0x04002861 RID: 10337
			private Drawing _current;

			// Token: 0x04002862 RID: 10338
			private DrawingCollection _list;

			// Token: 0x04002863 RID: 10339
			private uint _version;

			// Token: 0x04002864 RID: 10340
			private int _index;
		}
	}
}
