using System;
using System.Collections;
using System.Collections.Generic;
using MS.Internal.FontFace;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Representa uma coleção ordenada de objetos de <see cref="T:System.Windows.Media.FontFamilyMap" />.</summary>
	// Token: 0x02000391 RID: 913
	public sealed class FontFamilyMapCollection : IList<FontFamilyMap>, ICollection<FontFamilyMap>, IEnumerable<FontFamilyMap>, IEnumerable, IList, ICollection
	{
		// Token: 0x060021DD RID: 8669 RVA: 0x00089130 File Offset: 0x00088530
		internal FontFamilyMapCollection(CompositeFontInfo fontInfo)
		{
			this._fontInfo = fontInfo;
			this._items = null;
			this._count = 0;
		}

		/// <summary>Retorna um enumerador que pode iterar pela coleção.</summary>
		/// <returns>Um enumerador que pode iterar por meio da coleção.</returns>
		// Token: 0x060021DE RID: 8670 RVA: 0x00089158 File Offset: 0x00088558
		public IEnumerator<FontFamilyMap> GetEnumerator()
		{
			return new FontFamilyMapCollection.Enumerator(this._items, this._count);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
		/// <returns>Um objeto <see cref="T:System.Collections.IEnumerator" /> que pode ser usado para iterar pela coleção.</returns>
		// Token: 0x060021DF RID: 8671 RVA: 0x00089178 File Offset: 0x00088578
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new FontFamilyMapCollection.Enumerator(this._items, this._count);
		}

		/// <summary>Insere o objeto <see cref="T:System.Windows.Media.FontFamilyMap" /> especificado na coleção.</summary>
		/// <param name="item">O objeto a ser inserido.</param>
		// Token: 0x060021E0 RID: 8672 RVA: 0x00089198 File Offset: 0x00088598
		public void Add(FontFamilyMap item)
		{
			this.InsertItem(this._count, item);
		}

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.FontFamilyMap" /> de <see cref="T:System.Windows.Media.FontFamilyMapCollection" />.</summary>
		// Token: 0x060021E1 RID: 8673 RVA: 0x000891B4 File Offset: 0x000885B4
		public void Clear()
		{
			this.ClearItems();
		}

		/// <summary>Indica se a <see cref="T:System.Windows.Media.FontFamilyMapCollection" /> contém o objeto <see cref="T:System.Windows.Media.FontFamilyMap" /> especificado.</summary>
		/// <param name="item">O objeto a ser localizado.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="item" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060021E2 RID: 8674 RVA: 0x000891C8 File Offset: 0x000885C8
		public bool Contains(FontFamilyMap item)
		{
			return this.FindItem(item) >= 0;
		}

		/// <summary>Copia os objetos <see cref="T:System.Windows.Media.FontFamilyMap" /> na coleção para uma matriz de FontFamilyMaps, começando na posição de índice especificada.</summary>
		/// <param name="array">A matriz de destino.</param>
		/// <param name="index">A posição de índice baseado em zero em que a cópia é iniciada.</param>
		// Token: 0x060021E3 RID: 8675 RVA: 0x000891E4 File Offset: 0x000885E4
		public void CopyTo(FontFamilyMap[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
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
				Array.Copy(this._items, 0, array, index, this._count);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.FontFamilyMapCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x060021E4 RID: 8676 RVA: 0x0008927C File Offset: 0x0008867C
		void ICollection.CopyTo(Array array, int index)
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
					typeof(FamilyTypeface),
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
				Array.Copy(this._items, 0, array, index, this._count);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.FontFamilyMapCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x00089378 File Offset: 0x00088778
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.FontFamilyMapCollection" />.</returns>
		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x00089388 File Offset: 0x00088788
		object ICollection.SyncRoot
		{
			get
			{
				if (this._fontInfo == null)
				{
					return this;
				}
				return this._fontInfo;
			}
		}

		/// <summary>Remove o objeto <see cref="T:System.Windows.Media.FontFamilyMap" /> especificado da coleção.</summary>
		/// <param name="item">O objeto a ser removido.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="item" /> foi excluído com êxito; caso contrário <see langword="false" />.</returns>
		// Token: 0x060021E7 RID: 8679 RVA: 0x000893A8 File Offset: 0x000887A8
		public bool Remove(FontFamilyMap item)
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

		/// <summary>Obtém o número de objetos <see cref="T:System.Windows.Media.FontFamilyMap" /> no <see cref="T:System.Windows.Media.FontFamilyMapCollection" />.</summary>
		/// <returns>O número de objetos na coleção.</returns>
		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x000893D4 File Offset: 0x000887D4
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		/// <summary>Obtém um valor que indica se um <see cref="T:System.Windows.Media.FontFamilyMapCollection" /> é somente leitura.</summary>
		/// <returns>
		///   <see langword="true" /> Se a coleção é somente leitura; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060021E9 RID: 8681 RVA: 0x000893E8 File Offset: 0x000887E8
		public bool IsReadOnly
		{
			get
			{
				return this._fontInfo == null;
			}
		}

		/// <summary>Retorna o índice do objeto <see cref="T:System.Windows.Media.FontFamilyMap" /> especificado na coleção.</summary>
		/// <param name="item">O objeto a ser localizado.</param>
		/// <returns>O índice de base zero de <paramref name="item" />, se encontrado; caso contrário, -1;</returns>
		// Token: 0x060021EA RID: 8682 RVA: 0x00089400 File Offset: 0x00088800
		public int IndexOf(FontFamilyMap item)
		{
			return this.FindItem(item);
		}

		/// <summary>Insere o objeto <see cref="T:System.Windows.Media.FontFamilyMap" /> especificado na posição do índice indicada na coleção.</summary>
		/// <param name="index">A posição de índice de base zero para inserir o objeto.</param>
		/// <param name="item">O objeto a ser inserido.</param>
		// Token: 0x060021EB RID: 8683 RVA: 0x00089414 File Offset: 0x00088814
		public void Insert(int index, FontFamilyMap item)
		{
			this.InsertItem(index, item);
		}

		/// <summary>Exclui um objeto <see cref="T:System.Windows.Media.FontFamilyMap" /> da <see cref="T:System.Windows.Media.FontFamilyMapCollection" />.</summary>
		/// <param name="index">A posição de índice de base zero para remover o objeto.</param>
		// Token: 0x060021EC RID: 8684 RVA: 0x0008942C File Offset: 0x0008882C
		public void RemoveAt(int index)
		{
			this.RemoveItem(index);
		}

		/// <summary>Obtém ou define o objeto <see cref="T:System.Windows.Media.FontFamilyMap" /> na posição de índice especificada.</summary>
		/// <param name="index">A posição de índice de base zero do objeto a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.FontFamilyMap" /> do objeto no <paramref name="index" /> posição.</returns>
		// Token: 0x170006B2 RID: 1714
		public FontFamilyMap this[int index]
		{
			get
			{
				this.RangeCheck(index);
				return this._items[index];
			}
			set
			{
				this.SetItem(index, value);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.FontFamilyMapCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x060021EF RID: 8687 RVA: 0x00089474 File Offset: 0x00088874
		int IList.Add(object value)
		{
			return this.InsertItem(this._count, this.ConvertValue(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.TextEffectCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.FontFamilyMapCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060021F0 RID: 8688 RVA: 0x00089494 File Offset: 0x00088894
		bool IList.Contains(object value)
		{
			return this.FindItem(value as FontFamilyMap) >= 0;
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.FontFamilyMapCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x060021F1 RID: 8689 RVA: 0x000894B4 File Offset: 0x000888B4
		int IList.IndexOf(object value)
		{
			return this.FindItem(value as FontFamilyMap);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="item">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.FontFamilyMapCollection" />.</param>
		// Token: 0x060021F2 RID: 8690 RVA: 0x000894D0 File Offset: 0x000888D0
		void IList.Insert(int index, object item)
		{
			this.InsertItem(index, this.ConvertValue(item));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.FontFamilyMapCollection" />.</param>
		// Token: 0x060021F3 RID: 8691 RVA: 0x000894EC File Offset: 0x000888EC
		void IList.Remove(object value)
		{
			this.VerifyChangeable();
			int num = this.FindItem(value as FontFamilyMap);
			if (num >= 0)
			{
				this.RemoveItem(num);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.FontFamilyMapCollection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x00089518 File Offset: 0x00088918
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
		// Token: 0x170006B4 RID: 1716
		object IList.this[int index]
		{
			get
			{
				this.RangeCheck(index);
				return this._items[index];
			}
			set
			{
				this.SetItem(index, this.ConvertValue(value));
			}
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x00089564 File Offset: 0x00088964
		private int InsertItem(int index, FontFamilyMap item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.VerifyChangeable();
			if (this._count + 1 >= 65535)
			{
				throw new InvalidOperationException(SR.Get("CompositeFont_TooManyFamilyMaps"));
			}
			if (index < 0 || index > this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this._fontInfo.PrepareToAddFamilyMap(item);
			if (this._items == null)
			{
				this._items = new FontFamilyMap[8];
			}
			else if (this._count == this._items.Length)
			{
				FontFamilyMap[] array = new FontFamilyMap[this._count * 2];
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

		// Token: 0x060021F8 RID: 8696 RVA: 0x00089684 File Offset: 0x00088A84
		private void SetItem(int index, FontFamilyMap item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.VerifyChangeable();
			this.RangeCheck(index);
			this._fontInfo.PrepareToAddFamilyMap(item);
			if (item.Language != this._items[index].Language)
			{
				this._fontInfo.InvalidateFamilyMapRanges();
			}
			this._items[index] = item;
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x000896E4 File Offset: 0x00088AE4
		private void ClearItems()
		{
			this.VerifyChangeable();
			this._fontInfo.InvalidateFamilyMapRanges();
			this._count = 0;
			this._items = null;
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x00089710 File Offset: 0x00088B10
		private void RemoveItem(int index)
		{
			this.VerifyChangeable();
			this.RangeCheck(index);
			this._fontInfo.InvalidateFamilyMapRanges();
			this._count--;
			for (int i = index; i < this._count; i++)
			{
				this._items[i] = this._items[i + 1];
			}
			this._items[this._count] = null;
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x00089774 File Offset: 0x00088B74
		private int FindItem(FontFamilyMap item)
		{
			if (this._count != 0 && item != null)
			{
				for (int i = 0; i < this._count; i++)
				{
					if (this._items[i].Equals(item))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x000897B0 File Offset: 0x00088BB0
		private void RangeCheck(int index)
		{
			if (index < 0 || index >= this._count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x000897D8 File Offset: 0x00088BD8
		private void VerifyChangeable()
		{
			if (this._fontInfo == null)
			{
				throw new NotSupportedException(SR.Get("General_ObjectIsReadOnly"));
			}
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x00089800 File Offset: 0x00088C00
		private FontFamilyMap ConvertValue(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			FontFamilyMap fontFamilyMap = obj as FontFamilyMap;
			if (fontFamilyMap == null)
			{
				throw new ArgumentException(SR.Get("CannotConvertType", new object[]
				{
					obj.GetType(),
					typeof(FontFamilyMap)
				}));
			}
			return fontFamilyMap;
		}

		// Token: 0x040010DD RID: 4317
		private const int InitialCapacity = 8;

		// Token: 0x040010DE RID: 4318
		private CompositeFontInfo _fontInfo;

		// Token: 0x040010DF RID: 4319
		private FontFamilyMap[] _items;

		// Token: 0x040010E0 RID: 4320
		private int _count;

		// Token: 0x0200086A RID: 2154
		private class Enumerator : IEnumerator<FontFamilyMap>, IDisposable, IEnumerator
		{
			// Token: 0x0600574C RID: 22348 RVA: 0x00164CF8 File Offset: 0x001640F8
			internal Enumerator(FontFamilyMap[] items, int count)
			{
				this._items = items;
				this._count = count;
				this._index = -1;
				this._current = null;
			}

			// Token: 0x0600574D RID: 22349 RVA: 0x00164D28 File Offset: 0x00164128
			public bool MoveNext()
			{
				if (this._index < this._count)
				{
					this._index++;
					if (this._index < this._count)
					{
						this._current = this._items[this._index];
						return true;
					}
				}
				this._current = null;
				return false;
			}

			// Token: 0x0600574E RID: 22350 RVA: 0x00164D7C File Offset: 0x0016417C
			void IEnumerator.Reset()
			{
				this._index = -1;
			}

			// Token: 0x170011FE RID: 4606
			// (get) Token: 0x0600574F RID: 22351 RVA: 0x00164D90 File Offset: 0x00164190
			public FontFamilyMap Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x170011FF RID: 4607
			// (get) Token: 0x06005750 RID: 22352 RVA: 0x00164DA4 File Offset: 0x001641A4
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

			// Token: 0x06005751 RID: 22353 RVA: 0x00164DD0 File Offset: 0x001641D0
			public void Dispose()
			{
			}

			// Token: 0x04002867 RID: 10343
			private FontFamilyMap[] _items;

			// Token: 0x04002868 RID: 10344
			private int _count;

			// Token: 0x04002869 RID: 10345
			private int _index;

			// Token: 0x0400286A RID: 10346
			private FontFamilyMap _current;
		}
	}
}
