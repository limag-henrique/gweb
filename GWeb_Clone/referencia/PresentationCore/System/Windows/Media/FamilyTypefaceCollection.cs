using System;
using System.Collections;
using System.Collections.Generic;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Representa uma coleção de instâncias <see cref="T:System.Windows.Media.FamilyTypeface" />.</summary>
	// Token: 0x02000393 RID: 915
	public sealed class FamilyTypefaceCollection : IList<FamilyTypeface>, ICollection<FamilyTypeface>, IEnumerable<FamilyTypeface>, IEnumerable, IList, ICollection
	{
		// Token: 0x06002220 RID: 8736 RVA: 0x00089CF4 File Offset: 0x000890F4
		internal FamilyTypefaceCollection()
		{
			this._innerList = null;
			this._items = null;
			this._count = 0;
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x00089D1C File Offset: 0x0008911C
		internal FamilyTypefaceCollection(ICollection<Typeface> innerList)
		{
			this._innerList = innerList;
			this._items = null;
			this._count = innerList.Count;
		}

		/// <summary>Retorna um enumerador que pode iterar pela coleção.</summary>
		/// <returns>Um enumerador que pode iterar por meio da coleção.</returns>
		// Token: 0x06002222 RID: 8738 RVA: 0x00089D4C File Offset: 0x0008914C
		public IEnumerator<FamilyTypeface> GetEnumerator()
		{
			return new FamilyTypefaceCollection.Enumerator(this);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
		/// <returns>Um objeto <see cref="T:System.Collections.IEnumerator" /> que pode ser usado para iterar pela coleção.</returns>
		// Token: 0x06002223 RID: 8739 RVA: 0x00089D60 File Offset: 0x00089160
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new FamilyTypefaceCollection.Enumerator(this);
		}

		/// <summary>Insere o objeto <see cref="T:System.Windows.Media.FamilyTypeface" /> especificado na coleção.</summary>
		/// <param name="item">O objeto <see cref="T:System.Windows.Media.FamilyTypeface" /> a ser inserido.</param>
		// Token: 0x06002224 RID: 8740 RVA: 0x00089D74 File Offset: 0x00089174
		public void Add(FamilyTypeface item)
		{
			this.InsertItem(this._count, item);
		}

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.FamilyTypeface" /> de <see cref="T:System.Windows.Media.FamilyTypefaceCollection" />.</summary>
		// Token: 0x06002225 RID: 8741 RVA: 0x00089D90 File Offset: 0x00089190
		public void Clear()
		{
			this.ClearItems();
		}

		/// <summary>Determina se a coleção contém o <see cref="T:System.Windows.Media.FamilyTypeface" /> especificado.</summary>
		/// <param name="item">O objeto <see cref="T:System.Windows.Media.FamilyTypeface" /> a ser localizado.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="item" /> está na coleção; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002226 RID: 8742 RVA: 0x00089DA4 File Offset: 0x000891A4
		public bool Contains(FamilyTypeface item)
		{
			return this.FindItem(item) >= 0;
		}

		/// <summary>Copia os objetos <see cref="T:System.Windows.Media.FamilyTypeface" /> na coleção para uma matriz de <see cref="T:System.Windows.Media.FamilyTypefaceCollection" />, começando na posição de índice especificada.</summary>
		/// <param name="array">A matriz de destino.</param>
		/// <param name="index">A posição de índice baseado em zero em que a cópia é iniciada.</param>
		// Token: 0x06002227 RID: 8743 RVA: 0x00089DC0 File Offset: 0x000891C0
		public void CopyTo(FamilyTypeface[] array, int index)
		{
			this.CopyItems(array, index);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.FamilyTypefaceCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x06002228 RID: 8744 RVA: 0x00089DD8 File Offset: 0x000891D8
		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyItems(array, index);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.FamilyTypefaceCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06002229 RID: 8745 RVA: 0x00089DF0 File Offset: 0x000891F0
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.FamilyTypefaceCollection" />.</returns>
		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x0600222A RID: 8746 RVA: 0x00089E00 File Offset: 0x00089200
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Remove o objeto <see cref="T:System.Windows.Media.FamilyTypeface" /> especificado da coleção.</summary>
		/// <param name="item">O objeto <see cref="T:System.Windows.Media.FamilyTypeface" /> a ser removido.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="item" /> foi excluído com êxito; caso contrário <see langword="false" />.</returns>
		// Token: 0x0600222B RID: 8747 RVA: 0x00089E10 File Offset: 0x00089210
		public bool Remove(FamilyTypeface item)
		{
			this.VerifyChangeable();
			int num = this.FindItem(item);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		/// <summary>Obtém o número de objetos <see cref="T:System.Windows.Media.FamilyTypeface" /> no <see cref="T:System.Windows.Media.FamilyTypefaceCollection" />.</summary>
		/// <returns>O número de objetos na coleção.</returns>
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x0600222C RID: 8748 RVA: 0x00089E3C File Offset: 0x0008923C
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.Media.FamilyTypefaceCollection" /> é somente leitura.</summary>
		/// <returns>
		///   <see langword="true" /> se a coleção for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x0600222D RID: 8749 RVA: 0x00089E50 File Offset: 0x00089250
		public bool IsReadOnly
		{
			get
			{
				return this._innerList != null;
			}
		}

		/// <summary>Retorna o índice do objeto <see cref="T:System.Windows.Media.FamilyTypeface" /> especificado na coleção.</summary>
		/// <param name="item">O objeto <see cref="T:System.Windows.Media.FamilyTypeface" /> a ser localizado.</param>
		/// <returns>O índice de base zero de <paramref name="item" />, se encontrado; caso contrário, -1;</returns>
		// Token: 0x0600222E RID: 8750 RVA: 0x00089E68 File Offset: 0x00089268
		public int IndexOf(FamilyTypeface item)
		{
			return this.FindItem(item);
		}

		/// <summary>Insere o objeto <see cref="T:System.Windows.Media.FamilyTypeface" /> especificado na posição do índice indicada na coleção.</summary>
		/// <param name="index">A posição de índice de base zero para inserir o objeto.</param>
		/// <param name="item">O objeto <see cref="T:System.Windows.Media.FamilyTypeface" /> a ser inserido.</param>
		// Token: 0x0600222F RID: 8751 RVA: 0x00089E7C File Offset: 0x0008927C
		public void Insert(int index, FamilyTypeface item)
		{
			this.InsertItem(index, item);
		}

		/// <summary>Remove o objeto <see cref="T:System.Windows.Media.FamilyTypeface" /> especificado da coleção no índice indicado.</summary>
		/// <param name="index">A posição de índice de base zero de onde o objeto será excluído.</param>
		// Token: 0x06002230 RID: 8752 RVA: 0x00089E94 File Offset: 0x00089294
		public void RemoveAt(int index)
		{
			this.RemoveItem(index);
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.FamilyTypeface" /> que é armazenado no índice de base zero da <see cref="T:System.Windows.Media.FamilyTypefaceCollection" />.</summary>
		/// <param name="index">O índice de base zero da <see cref="T:System.Windows.Media.FamilyTypefaceCollection" /> da qual o <see cref="T:System.Windows.Media.FamilyTypeface" /> deve ser obtido ou definido.</param>
		/// <returns>O elemento no índice especificado.</returns>
		// Token: 0x170006C8 RID: 1736
		public FamilyTypeface this[int index]
		{
			get
			{
				return this.GetItem(index);
			}
			set
			{
				this.SetItem(index, value);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.FamilyTypefaceCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x06002233 RID: 8755 RVA: 0x00089ED4 File Offset: 0x000892D4
		int IList.Add(object value)
		{
			return this.InsertItem(this._count, this.ConvertValue(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.TextEffectCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.FamilyTypefaceCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002234 RID: 8756 RVA: 0x00089EF4 File Offset: 0x000892F4
		bool IList.Contains(object value)
		{
			return this.FindItem(value as FamilyTypeface) >= 0;
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.FamilyTypefaceCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06002235 RID: 8757 RVA: 0x00089F14 File Offset: 0x00089314
		int IList.IndexOf(object value)
		{
			return this.FindItem(value as FamilyTypeface);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="item">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.FamilyTypefaceCollection" />.</param>
		// Token: 0x06002236 RID: 8758 RVA: 0x00089F30 File Offset: 0x00089330
		void IList.Insert(int index, object item)
		{
			this.InsertItem(index, this.ConvertValue(item));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.FamilyTypefaceCollection" />.</param>
		// Token: 0x06002237 RID: 8759 RVA: 0x00089F4C File Offset: 0x0008934C
		void IList.Remove(object value)
		{
			this.VerifyChangeable();
			int num = this.FindItem(value as FamilyTypeface);
			if (num >= 0)
			{
				this.RemoveItem(num);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.FamilyTypefaceCollection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06002238 RID: 8760 RVA: 0x00089F78 File Offset: 0x00089378
		bool IList.IsFixedSize
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <param name="index">O índice com base em zero do elemento a ser obtido ou definido.</param>
		/// <returns>O elemento no índice especificado.</returns>
		// Token: 0x170006CA RID: 1738
		object IList.this[int index]
		{
			get
			{
				return this.GetItem(index);
			}
			set
			{
				this.SetItem(index, this.ConvertValue(value));
			}
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x00089FBC File Offset: 0x000893BC
		private int InsertItem(int index, FamilyTypeface item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.VerifyChangeable();
			if (index < 0 || index > this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this.FindItem(item) >= 0)
			{
				throw new ArgumentException(SR.Get("CompositeFont_DuplicateTypeface"));
			}
			if (this._items == null)
			{
				this._items = new FamilyTypeface[2];
			}
			else if (this._count == this._items.Length)
			{
				FamilyTypeface[] array = new FamilyTypeface[this._count * 2];
				for (int i = 0; i < index; i++)
				{
					array[i] = this._items[i];
				}
				for (int j = index; j < this._count; j++)
				{
					array[j + 1] = this._items[j];
				}
				this._items = array;
			}
			else if (index < this._count)
			{
				for (int k = this._count - 1; k >= index; k--)
				{
					this._items[k + 1] = this._items[k];
				}
			}
			this._items[index] = item;
			this._count++;
			return index;
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x0008A0CC File Offset: 0x000894CC
		private void InitializeItemsFromInnerList()
		{
			if (this._innerList != null && this._items == null)
			{
				FamilyTypeface[] array = new FamilyTypeface[this._count];
				int num = 0;
				foreach (Typeface face in this._innerList)
				{
					array[num++] = new FamilyTypeface(face);
				}
				this._items = array;
			}
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x0008A150 File Offset: 0x00089550
		private FamilyTypeface GetItem(int index)
		{
			this.RangeCheck(index);
			this.InitializeItemsFromInnerList();
			return this._items[index];
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x0008A174 File Offset: 0x00089574
		private void SetItem(int index, FamilyTypeface item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.VerifyChangeable();
			this.RangeCheck(index);
			this._items[index] = item;
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x0008A1A8 File Offset: 0x000895A8
		private void ClearItems()
		{
			this.VerifyChangeable();
			this._count = 0;
			this._items = null;
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x0008A1CC File Offset: 0x000895CC
		private void RemoveItem(int index)
		{
			this.VerifyChangeable();
			this.RangeCheck(index);
			this._count--;
			for (int i = index; i < this._count; i++)
			{
				this._items[i] = this._items[i + 1];
			}
			this._items[this._count] = null;
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x0008A228 File Offset: 0x00089628
		private int FindItem(FamilyTypeface item)
		{
			this.InitializeItemsFromInnerList();
			if (this._count != 0 && item != null)
			{
				for (int i = 0; i < this._count; i++)
				{
					if (this.GetItem(i).Equals(item))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x0008A26C File Offset: 0x0008966C
		private void RangeCheck(int index)
		{
			if (index < 0 || index >= this._count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x0008A294 File Offset: 0x00089694
		private void VerifyChangeable()
		{
			if (this._innerList != null)
			{
				throw new NotSupportedException(SR.Get("General_ObjectIsReadOnly"));
			}
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x0008A2BC File Offset: 0x000896BC
		private FamilyTypeface ConvertValue(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			FamilyTypeface familyTypeface = obj as FamilyTypeface;
			if (familyTypeface == null)
			{
				throw new ArgumentException(SR.Get("CannotConvertType", new object[]
				{
					obj.GetType(),
					typeof(FamilyTypeface)
				}));
			}
			return familyTypeface;
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x0008A310 File Offset: 0x00089710
		private void CopyItems(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(SR.Get("Collection_CopyTo_ArrayCannotBeMultidimensional"));
			}
			Type elementType = array.GetType().GetElementType();
			if (!elementType.IsAssignableFrom(typeof(FamilyTypeface)))
			{
				throw new ArgumentException(SR.Get("CannotConvertType", new object[]
				{
					typeof(FamilyTypeface[]),
					elementType
				}));
			}
			if (index >= array.Length)
			{
				throw new ArgumentException(SR.Get("Collection_CopyTo_IndexGreaterThanOrEqualToArrayLength", new object[]
				{
					"index",
					"array"
				}));
			}
			if (this._count > array.Length - index)
			{
				throw new ArgumentException(SR.Get("Collection_CopyTo_NumberOfElementsExceedsArrayLength", new object[]
				{
					index,
					"array"
				}));
			}
			if (this._count != 0)
			{
				this.InitializeItemsFromInnerList();
				Array.Copy(this._items, 0, array, index, this._count);
			}
		}

		// Token: 0x040010ED RID: 4333
		private const int InitialCapacity = 2;

		// Token: 0x040010EE RID: 4334
		private ICollection<Typeface> _innerList;

		// Token: 0x040010EF RID: 4335
		private FamilyTypeface[] _items;

		// Token: 0x040010F0 RID: 4336
		private int _count;

		// Token: 0x0200086B RID: 2155
		private class Enumerator : IEnumerator<FamilyTypeface>, IDisposable, IEnumerator
		{
			// Token: 0x06005752 RID: 22354 RVA: 0x00164DE0 File Offset: 0x001641E0
			internal Enumerator(FamilyTypefaceCollection list)
			{
				this._list = list;
				this._index = -1;
				this._current = null;
			}

			// Token: 0x06005753 RID: 22355 RVA: 0x00164E08 File Offset: 0x00164208
			public bool MoveNext()
			{
				int count = this._list.Count;
				if (this._index < count)
				{
					this._index++;
					if (this._index < count)
					{
						this._current = this._list[this._index];
						return true;
					}
				}
				this._current = null;
				return false;
			}

			// Token: 0x06005754 RID: 22356 RVA: 0x00164E64 File Offset: 0x00164264
			void IEnumerator.Reset()
			{
				this._index = -1;
			}

			// Token: 0x17001200 RID: 4608
			// (get) Token: 0x06005755 RID: 22357 RVA: 0x00164E78 File Offset: 0x00164278
			public FamilyTypeface Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x17001201 RID: 4609
			// (get) Token: 0x06005756 RID: 22358 RVA: 0x00164E8C File Offset: 0x0016428C
			object IEnumerator.Current
			{
				get
				{
					if (this._current == null)
					{
						throw new InvalidOperationException(SR.Get("Enumerator_VerifyContext"));
					}
					return this._current;
				}
			}

			// Token: 0x06005757 RID: 22359 RVA: 0x00164EB8 File Offset: 0x001642B8
			public void Dispose()
			{
			}

			// Token: 0x0400286B RID: 10347
			private FamilyTypefaceCollection _list;

			// Token: 0x0400286C RID: 10348
			private int _index;

			// Token: 0x0400286D RID: 10349
			private FamilyTypeface _current;
		}
	}
}
