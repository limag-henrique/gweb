using System;
using System.Collections;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Representa uma coleção ordenada de objetos de <see cref="T:System.Windows.Media.Visual" />.</summary>
	// Token: 0x0200044C RID: 1100
	public sealed class VisualCollection : ICollection, IEnumerable
	{
		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002D77 RID: 11639 RVA: 0x000B6034 File Offset: 0x000B5434
		internal int InternalCount
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06002D78 RID: 11640 RVA: 0x000B6048 File Offset: 0x000B5448
		internal Visual[] InternalArray
		{
			get
			{
				return this._items;
			}
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.VisualCollection" />.</summary>
		/// <param name="parent">O objeto visual pai cuja <see cref="T:System.Windows.Media.VisualCollection" /> é retornada.</param>
		// Token: 0x06002D79 RID: 11641 RVA: 0x000B605C File Offset: 0x000B545C
		public VisualCollection(Visual parent)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			this._owner = parent;
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x000B6084 File Offset: 0x000B5484
		internal void VerifyAPIReadOnly()
		{
			this._owner.VerifyAPIReadOnly();
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x000B609C File Offset: 0x000B549C
		internal void VerifyAPIReadOnly(Visual other)
		{
			this._owner.VerifyAPIReadOnly(other);
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x000B60B8 File Offset: 0x000B54B8
		internal void VerifyAPIReadWrite()
		{
			this._owner.VerifyAPIReadWrite();
			this.VerifyNotReadOnly();
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x000B60D8 File Offset: 0x000B54D8
		internal void VerifyAPIReadWrite(Visual other)
		{
			this._owner.VerifyAPIReadWrite(other);
			this.VerifyNotReadOnly();
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x000B60F8 File Offset: 0x000B54F8
		internal void VerifyNotReadOnly()
		{
			if (this.IsReadOnlyInternal)
			{
				throw new InvalidOperationException(SR.Get("VisualCollection_ReadOnly"));
			}
		}

		/// <summary>Obtém o número de elementos na coleção.</summary>
		/// <returns>O número de elementos que o <see cref="T:System.Windows.Media.VisualCollection" /> contém.</returns>
		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06002D7F RID: 11647 RVA: 0x000B6120 File Offset: 0x000B5520
		public int Count
		{
			get
			{
				this.VerifyAPIReadOnly();
				return this.InternalCount;
			}
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.Media.VisualCollection" /> é somente leitura.</summary>
		/// <returns>O valor que indica se o <see cref="T:System.Windows.Media.VisualCollection" /> é somente leitura.</returns>
		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06002D80 RID: 11648 RVA: 0x000B613C File Offset: 0x000B553C
		public bool IsReadOnly
		{
			get
			{
				this.VerifyAPIReadOnly();
				return this.IsReadOnlyInternal;
			}
		}

		/// <summary>Obtém um valor que indica se o acesso a <see cref="T:System.Windows.Media.VisualCollection" /> é sincronizado (thread-safe).</summary>
		/// <returns>O valor que indica se o <see cref="T:System.Windows.Media.VisualCollection" /> é sincronizado (thread-safe).</returns>
		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06002D81 RID: 11649 RVA: 0x000B6158 File Offset: 0x000B5558
		public bool IsSynchronized
		{
			get
			{
				this.VerifyAPIReadOnly();
				return false;
			}
		}

		/// <summary>Obtém um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.VisualCollection" />.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Object" />.</returns>
		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06002D82 RID: 11650 RVA: 0x000B616C File Offset: 0x000B556C
		public object SyncRoot
		{
			get
			{
				this.VerifyAPIReadOnly();
				return this;
			}
		}

		/// <summary>Copia os itens na coleção para uma matriz, começando em um índice de matriz específico.</summary>
		/// <param name="array">A <see cref="T:System.Array" /> unidimensional que é o destino dos elementos copiados da <see cref="T:System.Windows.Media.VisualCollection" />.</param>
		/// <param name="index">O índice com base em zero em <paramref name="array" /> no qual a cópia começa.</param>
		// Token: 0x06002D83 RID: 11651 RVA: 0x000B6180 File Offset: 0x000B5580
		public void CopyTo(Array array, int index)
		{
			this.VerifyAPIReadOnly();
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(SR.Get("Collection_BadRank"));
			}
			if (index < 0 || array.Length - index < this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			for (int i = 0; i < this._size; i++)
			{
				array.SetValue(this._items[i], i + index);
			}
		}

		/// <summary>Copia a coleção atual na matriz <see cref="T:System.Windows.Media.Visual" /> passada.</summary>
		/// <param name="array">Uma matriz de objetos <see cref="T:System.Windows.Media.Visual" /> (que devem ter indexação de base zero).</param>
		/// <param name="index">O índice no qual a cópia deve ser iniciada de dentro da <paramref name="array" />.</param>
		// Token: 0x06002D84 RID: 11652 RVA: 0x000B61FC File Offset: 0x000B55FC
		public void CopyTo(Visual[] array, int index)
		{
			this.VerifyAPIReadOnly();
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || array.Length - index < this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			for (int i = 0; i < this._size; i++)
			{
				array[i + index] = this._items[i];
			}
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x000B6258 File Offset: 0x000B5658
		private void EnsureCapacity(int min)
		{
			if (this.InternalCapacity < min)
			{
				this.InternalCapacity = Math.Max(min, (int)((float)this.InternalCapacity * 1.5f));
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06002D86 RID: 11654 RVA: 0x000B6288 File Offset: 0x000B5688
		// (set) Token: 0x06002D87 RID: 11655 RVA: 0x000B62A8 File Offset: 0x000B56A8
		internal int InternalCapacity
		{
			get
			{
				if (this._items == null)
				{
					return 0;
				}
				return this._items.Length;
			}
			set
			{
				int num = (this._items != null) ? this._items.Length : 0;
				if (value != num)
				{
					if (value < this._size)
					{
						throw new ArgumentOutOfRangeException("value", SR.Get("VisualCollection_NotEnoughCapacity"));
					}
					if (value > 0)
					{
						Visual[] array = new Visual[value];
						if (this._size > 0)
						{
							Array.Copy(this._items, 0, array, 0, this._size);
						}
						this._items = array;
						return;
					}
					this._items = null;
				}
			}
		}

		/// <summary>Obtém ou define o número de elementos que o <see cref="T:System.Windows.Media.VisualCollection" /> pode conter.</summary>
		/// <returns>O número de elementos que o <see cref="T:System.Windows.Media.VisualCollection" /> pode conter.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Windows.Media.VisualCollection.Capacity" /> é definido como um valor menor que <see cref="P:System.Windows.Media.VisualCollection.Count" />.</exception>
		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002D88 RID: 11656 RVA: 0x000B6324 File Offset: 0x000B5724
		// (set) Token: 0x06002D89 RID: 11657 RVA: 0x000B6340 File Offset: 0x000B5740
		public int Capacity
		{
			get
			{
				this.VerifyAPIReadOnly();
				return this.InternalCapacity;
			}
			set
			{
				this.VerifyAPIReadWrite();
				this.InternalCapacity = value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Visual" /> que é armazenado no índice de base zero da <see cref="T:System.Windows.Media.VisualCollection" />.</summary>
		/// <param name="index">O índice de base zero da <see cref="T:System.Windows.Media.VisualCollection" /> da qual o <see cref="T:System.Windows.Media.Visual" /> deve ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Visual" /> que é armazenado no <paramref name="index" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero ou <paramref name="index" /> é maior ou igual à <see cref="P:System.Windows.Media.VisualCollection.Count" />.</exception>
		/// <exception cref="T:System.ArgumentException">O novo elemento filho já tem um pai ou o valor no índice especificado não é <see langword="null" />.</exception>
		// Token: 0x1700096E RID: 2414
		public Visual this[int index]
		{
			get
			{
				if (index < 0 || index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this._items[index];
			}
			set
			{
				this.VerifyAPIReadWrite(value);
				if (index < 0 || index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				Visual visual = this._items[index];
				if (value == null && visual != null)
				{
					this.DisconnectChild(index);
					return;
				}
				if (value != null)
				{
					if (visual != null)
					{
						throw new ArgumentException(SR.Get("VisualCollection_EntryInUse"));
					}
					if (value._parent != null || value.IsRootElement)
					{
						throw new ArgumentException(SR.Get("VisualCollection_VisualHasParent"));
					}
					this.ConnectChild(index, value);
				}
			}
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x000B6410 File Offset: 0x000B5810
		private void ConnectChild(int index, Visual value)
		{
			this._owner.VerifyAccess();
			value.VerifyAccess();
			if (this._owner.IsVisualChildrenIterationInProgress)
			{
				throw new InvalidOperationException(SR.Get("CannotModifyVisualChildrenDuringTreeWalk"));
			}
			value._parentIndex = index;
			this._items[index] = value;
			this.IncrementVersion();
			this._owner.InternalAddVisualChild(value);
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x000B6470 File Offset: 0x000B5870
		private void DisconnectChild(int index)
		{
			Visual visual = this._items[index];
			visual.VerifyAccess();
			Visual containingVisual2D = VisualTreeHelper.GetContainingVisual2D(visual._parent);
			int parentIndex = visual._parentIndex;
			if (containingVisual2D.IsVisualChildrenIterationInProgress)
			{
				throw new InvalidOperationException(SR.Get("CannotModifyVisualChildrenDuringTreeWalk"));
			}
			this._items[index] = null;
			this.IncrementVersion();
			this._owner.InternalRemoveVisualChild(visual);
		}

		/// <summary>Acrescenta um <see cref="T:System.Windows.Media.Visual" /> ao final da <see cref="T:System.Windows.Media.VisualCollection" />.</summary>
		/// <param name="visual">O <see cref="T:System.Windows.Media.Visual" /> a acrescentar ao <see cref="T:System.Windows.Media.VisualCollection" />.</param>
		/// <returns>O índice na coleção em que o <paramref name="visual" /> foi adicionado.</returns>
		/// <exception cref="T:System.ArgumentException">Uma <see cref="T:System.ArgumentException" /> será gerada se o <see cref="T:System.Windows.Media.Visual" /> for um elemento raiz.</exception>
		// Token: 0x06002D8E RID: 11662 RVA: 0x000B64D4 File Offset: 0x000B58D4
		public int Add(Visual visual)
		{
			this.VerifyAPIReadWrite(visual);
			if (visual != null && (visual._parent != null || visual.IsRootElement))
			{
				throw new ArgumentException(SR.Get("VisualCollection_VisualHasParent"));
			}
			if (this._items == null || this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			int size = this._size;
			this._size = size + 1;
			int num = size;
			if (visual != null)
			{
				this.ConnectChild(num, visual);
			}
			this.IncrementVersion();
			return num;
		}

		/// <summary>Retorna o índice de base zero de <see cref="T:System.Windows.Media.Visual" />.</summary>
		/// <param name="visual">O <see cref="T:System.Windows.Media.Visual" /> a ser localizado no <see cref="T:System.Windows.Media.VisualCollection" />.</param>
		/// <returns>O índice do <see cref="T:System.Windows.Media.Visual" />.</returns>
		// Token: 0x06002D8F RID: 11663 RVA: 0x000B6558 File Offset: 0x000B5958
		public int IndexOf(Visual visual)
		{
			this.VerifyAPIReadOnly();
			if (visual == null)
			{
				for (int i = 0; i < this._size; i++)
				{
					if (this._items[i] == null)
					{
						return i;
					}
				}
				return -1;
			}
			if (visual._parent != this._owner)
			{
				return -1;
			}
			return visual._parentIndex;
		}

		/// <summary>Remove o objeto <see cref="T:System.Windows.Media.Visual" /> especificado da <see cref="T:System.Windows.Media.VisualCollection" />.</summary>
		/// <param name="visual">O <see cref="T:System.Windows.Media.Visual" /> a ser removido de <see cref="T:System.Windows.Media.VisualCollection" />.</param>
		// Token: 0x06002D90 RID: 11664 RVA: 0x000B65A4 File Offset: 0x000B59A4
		public void Remove(Visual visual)
		{
			this.VerifyAPIReadWrite(visual);
			this.InternalRemove(visual);
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x000B65C0 File Offset: 0x000B59C0
		private void InternalRemove(Visual visual)
		{
			int num = -1;
			if (visual != null)
			{
				if (visual._parent != this._owner)
				{
					return;
				}
				num = visual._parentIndex;
				this.DisconnectChild(num);
			}
			else
			{
				for (int i = 0; i < this._size; i++)
				{
					if (this._items[i] == null)
					{
						num = i;
						break;
					}
				}
			}
			if (num != -1)
			{
				this._size--;
				for (int j = num; j < this._size; j++)
				{
					Visual visual2 = this._items[j + 1];
					if (visual2 != null)
					{
						visual2._parentIndex = j;
					}
					this._items[j] = visual2;
				}
				this._items[this._size] = null;
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06002D92 RID: 11666 RVA: 0x000B6660 File Offset: 0x000B5A60
		private uint Version
		{
			get
			{
				return this._data >> 1;
			}
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x000B6678 File Offset: 0x000B5A78
		private void IncrementVersion()
		{
			this._data += 2U;
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06002D94 RID: 11668 RVA: 0x000B6694 File Offset: 0x000B5A94
		private bool IsReadOnlyInternal
		{
			get
			{
				return (this._data & 1U) == 1U;
			}
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x000B66AC File Offset: 0x000B5AAC
		internal void SetReadOnly()
		{
			this._data |= 1U;
		}

		/// <summary>Retorna um valor <see cref="T:System.Boolean" /> que indica se o <see cref="T:System.Windows.Media.Visual" /> especificado está contido na coleção.</summary>
		/// <param name="visual">O <see cref="T:System.Windows.Media.Visual" /> a ser pesquisado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se o <paramref name="visual" /> estiver contido na coleção, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002D96 RID: 11670 RVA: 0x000B66C8 File Offset: 0x000B5AC8
		public bool Contains(Visual visual)
		{
			this.VerifyAPIReadOnly(visual);
			if (visual == null)
			{
				for (int i = 0; i < this._size; i++)
				{
					if (this._items[i] == null)
					{
						return true;
					}
				}
				return false;
			}
			return visual._parent == this._owner;
		}

		/// <summary>Remove todos os elementos do <see cref="T:System.Windows.Media.VisualCollection" />.</summary>
		// Token: 0x06002D97 RID: 11671 RVA: 0x000B670C File Offset: 0x000B5B0C
		public void Clear()
		{
			this.VerifyAPIReadWrite();
			for (int i = 0; i < this._size; i++)
			{
				if (this._items[i] != null)
				{
					this.DisconnectChild(i);
				}
				this._items[i] = null;
			}
			this._size = 0;
			this.IncrementVersion();
		}

		/// <summary>Insere um elemento no <see cref="T:System.Windows.Media.VisualCollection" />, no índice especificado.</summary>
		/// <param name="index">O índice de base zero no qual o valor deve ser inserido.</param>
		/// <param name="visual">O <see cref="T:System.Windows.Media.Visual" /> a ser inserido no <see cref="T:System.Windows.Media.VisualCollection" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero ou maior que <see cref="P:System.Windows.Media.VisualCollection.Count" />.</exception>
		// Token: 0x06002D98 RID: 11672 RVA: 0x000B6758 File Offset: 0x000B5B58
		public void Insert(int index, Visual visual)
		{
			this.VerifyAPIReadWrite(visual);
			if (index < 0 || index > this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (visual != null && (visual._parent != null || visual.IsRootElement))
			{
				throw new ArgumentException(SR.Get("VisualCollection_VisualHasParent"));
			}
			if (this._items == null || this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			for (int i = this._size - 1; i >= index; i--)
			{
				Visual visual2 = this._items[i];
				if (visual2 != null)
				{
					visual2._parentIndex = i + 1;
				}
				this._items[i + 1] = visual2;
			}
			this._items[index] = null;
			this._size++;
			if (visual != null)
			{
				this.ConnectChild(index, visual);
			}
		}

		/// <summary>Remove o objeto visual no índice especificado na <see cref="T:System.Windows.Media.VisualCollection" />.</summary>
		/// <param name="index">O índice de base zero do visual a ser removido.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero ou <paramref name="index" /> é maior ou igual à <see cref="P:System.Windows.Media.VisualCollection.Count" />.</exception>
		// Token: 0x06002D99 RID: 11673 RVA: 0x000B6824 File Offset: 0x000B5C24
		public void RemoveAt(int index)
		{
			this.VerifyAPIReadWrite();
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.InternalRemove(this._items[index]);
		}

		/// <summary>Remove um intervalo de objetos visuais da <see cref="T:System.Windows.Media.VisualCollection" />.</summary>
		/// <param name="index">O índice de base zero do intervalo de elementos a serem removidos.</param>
		/// <param name="count">O número de elementos a serem removidos.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero ou <paramref name="count" /> é menor que zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> e <paramref name="count" /> não referenciam um intervalo válido de elementos na <see cref="T:System.Windows.Media.VisualCollection" />.</exception>
		// Token: 0x06002D9A RID: 11674 RVA: 0x000B6860 File Offset: 0x000B5C60
		public void RemoveRange(int index, int count)
		{
			this.VerifyAPIReadWrite();
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this._size - index < count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count > 0)
			{
				for (int i = index; i < index + count; i++)
				{
					if (this._items[i] != null)
					{
						this.DisconnectChild(i);
						this._items[i] = null;
					}
				}
				this._size -= count;
				for (int j = index; j < this._size; j++)
				{
					Visual visual = this._items[j + count];
					if (visual != null)
					{
						visual._parentIndex = j;
					}
					this._items[j] = visual;
					this._items[j + count] = null;
				}
				this.IncrementVersion();
			}
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x000B6924 File Offset: 0x000B5D24
		internal void Move(Visual visual, Visual destination)
		{
			Invariant.Assert(visual != null, "we don't support moving a null visual");
			if (visual._parent == this._owner)
			{
				int parentIndex = visual._parentIndex;
				int num = (destination != null) ? destination._parentIndex : this._size;
				if (parentIndex != num)
				{
					if (parentIndex < num)
					{
						num--;
						for (int i = parentIndex; i < num; i++)
						{
							Visual visual2 = this._items[i + 1];
							if (visual2 != null)
							{
								visual2._parentIndex = i;
							}
							this._items[i] = visual2;
						}
					}
					else
					{
						for (int j = parentIndex; j > num; j--)
						{
							Visual visual3 = this._items[j - 1];
							if (visual3 != null)
							{
								visual3._parentIndex = j;
							}
							this._items[j] = visual3;
						}
					}
					visual._parentIndex = num;
					this._items[num] = visual;
				}
			}
		}

		/// <summary>Este membro dá suporte à infraestrutura WPF e não se destina a ser usado diretamente do código. Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
		/// <returns>Um objeto que pode ser usado para iterar na coleção.</returns>
		// Token: 0x06002D9C RID: 11676 RVA: 0x000B69E8 File Offset: 0x000B5DE8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Recupera um enumerador que pode iterar na <see cref="T:System.Windows.Media.VisualCollection" />.</summary>
		/// <returns>Um enumerador que pode ser usado para iterar pela coleção.</returns>
		// Token: 0x06002D9D RID: 11677 RVA: 0x000B6A00 File Offset: 0x000B5E00
		public VisualCollection.Enumerator GetEnumerator()
		{
			this.VerifyAPIReadOnly();
			return new VisualCollection.Enumerator(this);
		}

		// Token: 0x040014AA RID: 5290
		private Visual[] _items;

		// Token: 0x040014AB RID: 5291
		private int _size;

		// Token: 0x040014AC RID: 5292
		private Visual _owner;

		// Token: 0x040014AD RID: 5293
		private uint _data;

		// Token: 0x040014AE RID: 5294
		private const int c_defaultCapacity = 4;

		// Token: 0x040014AF RID: 5295
		private const float c_growFactor = 1.5f;

		/// <summary>Enumera itens <see cref="T:System.Windows.Media.Visual" /> em um <see cref="T:System.Windows.Media.VisualCollection" />.</summary>
		// Token: 0x020008A5 RID: 2213
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06005861 RID: 22625 RVA: 0x001676E4 File Offset: 0x00166AE4
			internal Enumerator(VisualCollection collection)
			{
				this._collection = collection;
				this._index = -1;
				this._version = this._collection.Version;
				this._currentElement = null;
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador foi avançado com êxito para o próximo elemento; caso contrário, <see langword="false" />.</returns>
			// Token: 0x06005862 RID: 22626 RVA: 0x00167718 File Offset: 0x00166B18
			public bool MoveNext()
			{
				this._collection.VerifyAPIReadOnly();
				if (this._version != this._collection.Version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._collection.InternalCount - 1)
				{
					this._index++;
					this._currentElement = this._collection[this._index];
					return true;
				}
				this._currentElement = null;
				this._index = -2;
				return false;
			}

			/// <summary>Para obter uma descrição desses membros, consulte <see cref="P:System.Collections.IEnumerator.Current" />.</summary>
			/// <returns>O elemento atual na coleção.</returns>
			// Token: 0x17001236 RID: 4662
			// (get) Token: 0x06005863 RID: 22627 RVA: 0x001677AC File Offset: 0x00166BAC
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém o elemento atual na coleção.</summary>
			/// <returns>O elemento atual na coleção.</returns>
			// Token: 0x17001237 RID: 4663
			// (get) Token: 0x06005864 RID: 22628 RVA: 0x001677C0 File Offset: 0x00166BC0
			public Visual Current
			{
				get
				{
					if (this._index >= 0)
					{
						return this._currentElement;
					}
					if (this._index == -1)
					{
						throw new InvalidOperationException(SR.Get("Enumerator_NotStarted"));
					}
					throw new InvalidOperationException(SR.Get("Enumerator_ReachedEnd"));
				}
			}

			/// <summary>Redefine o enumerador com sua posição inicial, que é antes do primeiro elemento da coleção.</summary>
			// Token: 0x06005865 RID: 22629 RVA: 0x00167808 File Offset: 0x00166C08
			public void Reset()
			{
				this._collection.VerifyAPIReadOnly();
				if (this._version != this._collection.Version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				this._index = -1;
			}

			// Token: 0x040028F5 RID: 10485
			private VisualCollection _collection;

			// Token: 0x040028F6 RID: 10486
			private int _index;

			// Token: 0x040028F7 RID: 10487
			private uint _version;

			// Token: 0x040028F8 RID: 10488
			private Visual _currentElement;
		}
	}
}
