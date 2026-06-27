using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media.Media3D
{
	/// <summary>Representa uma coleção ordenada de objetos de <see cref="T:System.Windows.Media.Media3D.Visual3D" />.</summary>
	// Token: 0x02000488 RID: 1160
	public sealed class Visual3DCollection : IList, ICollection, IEnumerable, IList<Visual3D>, ICollection<Visual3D>, IEnumerable<Visual3D>
	{
		// Token: 0x06003318 RID: 13080 RVA: 0x000CB90C File Offset: 0x000CAD0C
		internal Visual3DCollection(IVisual3DContainer owner)
		{
			this._owner = owner;
		}

		/// <summary>Adiciona um objeto <see cref="T:System.Windows.Media.Media3D.Visual3D" /> ao final desta <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" />.</summary>
		/// <param name="value">O Visual3D a ser adicionado a esta Visual3DCollection.</param>
		// Token: 0x06003319 RID: 13081 RVA: 0x000CB928 File Offset: 0x000CAD28
		public void Add(Visual3D value)
		{
			this.VerifyAPIForAdd(value);
			int internalCount = this.InternalCount;
			this._collection.Add(value);
			this.InvalidateEnumerators();
			this.ConnectChild(internalCount, value);
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x000CB960 File Offset: 0x000CAD60
		private void ConnectChild(int index, Visual3D value)
		{
			value.ParentIndex = index;
			this._owner.AddChild(value);
		}

		/// <summary>Insere um objeto <see cref="T:System.Windows.Media.Media3D.Visual3D" /> nesta <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" /> no índice especificado.</summary>
		/// <param name="index">O índice no qual o Visual3D será inserido.</param>
		/// <param name="value">O Visual3D a ser inserido.</param>
		// Token: 0x0600331B RID: 13083 RVA: 0x000CB980 File Offset: 0x000CAD80
		public void Insert(int index, Visual3D value)
		{
			this.VerifyAPIForAdd(value);
			this.InternalInsert(index, value);
		}

		/// <summary>Remove a primeira ocorrência do objeto <see cref="T:System.Windows.Media.Media3D.Visual3D" /> especificado desta <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" />.</summary>
		/// <param name="value">O Visual3D a ser removido.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> tiver sido removido da coleção; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600331C RID: 13084 RVA: 0x000CB99C File Offset: 0x000CAD9C
		public bool Remove(Visual3D value)
		{
			this.VerifyAPIReadWrite(value);
			if (!this._collection.Contains(value))
			{
				return false;
			}
			this.InternalRemoveAt(value.ParentIndex);
			return true;
		}

		/// <summary>Remove o objeto <see cref="T:System.Windows.Media.Media3D.Visual3D" /> no índice especificado desta <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" />.</summary>
		/// <param name="index">O índice do Visual3D a ser removido.</param>
		// Token: 0x0600331D RID: 13085 RVA: 0x000CB9D0 File Offset: 0x000CADD0
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.InternalCount)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.VerifyAPIReadWrite(this._collection[index]);
			this.InternalRemoveAt(index);
		}

		/// <summary>Remove todos os itens desta <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" />.</summary>
		// Token: 0x0600331E RID: 13086 RVA: 0x000CBA10 File Offset: 0x000CAE10
		public void Clear()
		{
			this.VerifyAPIReadWrite();
			FrugalStructList<Visual3D> collection = this._collection;
			this._collection = default(FrugalStructList<Visual3D>);
			this.InvalidateEnumerators();
			for (int i = collection.Count - 1; i >= 0; i--)
			{
				this._owner.RemoveChild(collection[i]);
			}
		}

		/// <summary>Copia os itens dessa Visual3DCollection, começando com o índice especificado, em uma matriz de objetos <see cref="T:System.Windows.Media.Media3D.Visual3D" />.</summary>
		/// <param name="array">A matriz que é o destino dos itens copiados desta Visual3DCollection.</param>
		/// <param name="index">O índice no qual começar a copiar.</param>
		// Token: 0x0600331F RID: 13087 RVA: 0x000CBA64 File Offset: 0x000CAE64
		public void CopyTo(Visual3D[] array, int index)
		{
			this.VerifyAPIReadOnly();
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || index >= array.Length || index + this._collection.Count > array.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this._collection.CopyTo(array, index);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x06003320 RID: 13088 RVA: 0x000CBABC File Offset: 0x000CAEBC
		void ICollection.CopyTo(Array array, int index)
		{
			this.VerifyAPIReadOnly();
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || index >= array.Length || index + this._collection.Count > array.Length)
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
					"Visual3DCollection"
				}), innerException);
			}
		}

		/// <summary>Determina se um <see cref="T:System.Windows.Media.Media3D.Visual3D" /> especificado está nesta <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" />.</summary>
		/// <param name="value">O Visual3D a ser localizado nesta Visual3Dcollection.</param>
		/// <returns>True se <paramref name="value" />, o Visual3D especificado, estiver nesta Visual3DCollection, caso contrário, false.</returns>
		// Token: 0x06003321 RID: 13089 RVA: 0x000CBB94 File Offset: 0x000CAF94
		public bool Contains(Visual3D value)
		{
			this.VerifyAPIReadOnly(value);
			return value != null && value.InternalVisualParent == this._owner;
		}

		/// <summary>Obtém o índice da primeira ocorrência do objeto <see cref="T:System.Windows.Media.Media3D.Visual3D" />.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.Media3D.Visual3D" /> em busca do qual pesquisar.</param>
		/// <returns>O índice do <see cref="T:System.Windows.Media.Media3D.Visual3D" /> especificado ou -1 se <paramref name="value" /> for <see langword="null" /> ou tiver um pai de visual diferente.</returns>
		// Token: 0x06003322 RID: 13090 RVA: 0x000CBBBC File Offset: 0x000CAFBC
		public int IndexOf(Visual3D value)
		{
			this.VerifyAPIReadOnly(value);
			if (value == null || value.InternalVisualParent != this._owner)
			{
				return -1;
			}
			return value.ParentIndex;
		}

		/// <summary>Obtém um enumerador para a <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" />.</summary>
		/// <returns>Um objeto <see cref="T:System.Collections.IEnumerator" /> que pode ser usado para iterar pela coleção.</returns>
		// Token: 0x06003323 RID: 13091 RVA: 0x000CBBEC File Offset: 0x000CAFEC
		public Visual3DCollection.Enumerator GetEnumerator()
		{
			this.VerifyAPIReadOnly();
			return new Visual3DCollection.Enumerator(this);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
		/// <returns>Um objeto <see cref="T:System.Collections.IEnumerator" /> que pode ser usado para iterar pela coleção.</returns>
		// Token: 0x06003324 RID: 13092 RVA: 0x000CBC08 File Offset: 0x000CB008
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x000CBC20 File Offset: 0x000CB020
		IEnumerator<Visual3D> IEnumerable<Visual3D>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Media3D.Visual3D" /> no índice de base zero especificado.</summary>
		/// <param name="index">O índice de base zero do Visual3D a ser obtido ou definido.</param>
		/// <returns>O Visual3D no índice especificado.</returns>
		// Token: 0x17000A67 RID: 2663
		public Visual3D this[int index]
		{
			get
			{
				this.VerifyAPIReadOnly();
				return this.InternalGetItem(index);
			}
			set
			{
				if (index < 0 || index >= this.InternalCount)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.VerifyAPIForAdd(value);
				this.InternalRemoveAt(index);
				this.InternalInsert(index, value);
			}
		}

		/// <summary>Obtém o número de itens contidos em uma <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" />.</summary>
		/// <returns>O número de itens contidos no Visual3Dcollection.</returns>
		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06003328 RID: 13096 RVA: 0x000CBC90 File Offset: 0x000CB090
		public int Count
		{
			get
			{
				this.VerifyAPIReadOnly();
				return this.InternalCount;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06003329 RID: 13097 RVA: 0x000CBCAC File Offset: 0x000CB0AC
		bool ICollection.IsSynchronized
		{
			get
			{
				this.VerifyAPIReadOnly();
				return true;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" />.</returns>
		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x0600332A RID: 13098 RVA: 0x000CBCC0 File Offset: 0x000CB0C0
		object ICollection.SyncRoot
		{
			get
			{
				this.VerifyAPIReadOnly();
				return this._owner;
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x0600332B RID: 13099 RVA: 0x000CBCDC File Offset: 0x000CB0DC
		bool ICollection<Visual3D>.IsReadOnly
		{
			get
			{
				this.VerifyAPIReadOnly();
				return false;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x0600332C RID: 13100 RVA: 0x000CBCF0 File Offset: 0x000CB0F0
		int IList.Add(object value)
		{
			this.Add(this.Cast(value));
			return this.InternalCount - 1;
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600332D RID: 13101 RVA: 0x000CBD14 File Offset: 0x000CB114
		bool IList.Contains(object value)
		{
			return this.Contains(value as Visual3D);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x0600332E RID: 13102 RVA: 0x000CBD30 File Offset: 0x000CB130
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as Visual3D);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" />.</param>
		// Token: 0x0600332F RID: 13103 RVA: 0x000CBD4C File Offset: 0x000CB14C
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06003330 RID: 13104 RVA: 0x000CBD68 File Offset: 0x000CB168
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06003331 RID: 13105 RVA: 0x000CBD78 File Offset: 0x000CB178
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" />.</param>
		// Token: 0x06003332 RID: 13106 RVA: 0x000CBD88 File Offset: 0x000CB188
		void IList.Remove(object value)
		{
			this.Remove(value as Visual3D);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <param name="index">O índice com base em zero do elemento a ser obtido ou definido.</param>
		/// <returns>O elemento no índice especificado.</returns>
		// Token: 0x17000A6E RID: 2670
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

		// Token: 0x06003335 RID: 13109 RVA: 0x000CBDD4 File Offset: 0x000CB1D4
		internal Visual3D InternalGetItem(int index)
		{
			return this._collection[index];
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06003336 RID: 13110 RVA: 0x000CBDF0 File Offset: 0x000CB1F0
		internal int InternalCount
		{
			get
			{
				return this._collection.Count;
			}
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x000CBE08 File Offset: 0x000CB208
		private void VerifyAPIReadOnly()
		{
			this._owner.VerifyAPIReadOnly();
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x000CBE20 File Offset: 0x000CB220
		private void VerifyAPIReadOnly(Visual3D other)
		{
			this._owner.VerifyAPIReadOnly(other);
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x000CBE3C File Offset: 0x000CB23C
		private void VerifyAPIReadWrite()
		{
			this._owner.VerifyAPIReadWrite();
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x000CBE54 File Offset: 0x000CB254
		private void VerifyAPIReadWrite(Visual3D other)
		{
			this._owner.VerifyAPIReadWrite(other);
		}

		// Token: 0x0600333B RID: 13115 RVA: 0x000CBE70 File Offset: 0x000CB270
		private Visual3D Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is Visual3D))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"Visual3D"
				}));
			}
			return (Visual3D)value;
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x000CBED4 File Offset: 0x000CB2D4
		private void VerifyAPIForAdd(Visual3D value)
		{
			if (value == null)
			{
				throw new ArgumentException(SR.Get("Collection_NoNull"));
			}
			this.VerifyAPIReadWrite(value);
			if (value.InternalVisualParent != null)
			{
				throw new ArgumentException(SR.Get("VisualCollection_VisualHasParent"));
			}
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x000CBF14 File Offset: 0x000CB314
		private void InternalInsert(int index, Visual3D value)
		{
			this._collection.Insert(index, value);
			int i = index + 1;
			int internalCount = this.InternalCount;
			while (i < internalCount)
			{
				this.InternalGetItem(i).ParentIndex = i;
				i++;
			}
			this.InvalidateEnumerators();
			this.ConnectChild(index, value);
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x000CBF60 File Offset: 0x000CB360
		private void InternalRemoveAt(int index)
		{
			Visual3D child = this._collection[index];
			this._collection.RemoveAt(index);
			for (int i = index; i < this.InternalCount; i++)
			{
				this.InternalGetItem(i).ParentIndex = i;
			}
			this.InvalidateEnumerators();
			this._owner.RemoveChild(child);
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x000CBFB8 File Offset: 0x000CB3B8
		private void InvalidateEnumerators()
		{
			this._version++;
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x000CBFD4 File Offset: 0x000CB3D4
		[Conditional("DEBUG")]
		private void Debug_ICC()
		{
			Dictionary<Visual3D, string> dictionary = new Dictionary<Visual3D, string>();
			for (int i = 0; i < this._collection.Count; i++)
			{
				Visual3D key = this._collection[i];
				dictionary.Add(key, string.Empty);
			}
		}

		// Token: 0x040015EB RID: 5611
		private IVisual3DContainer _owner;

		// Token: 0x040015EC RID: 5612
		private FrugalStructList<Visual3D> _collection;

		// Token: 0x040015ED RID: 5613
		private int _version;

		/// <summary>Enumera itens <see cref="T:System.Windows.Media.Media3D.Visual3D" /> em um <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" />.</summary>
		// Token: 0x020008AC RID: 2220
		public struct Enumerator : IEnumerator<Visual3D>, IDisposable, IEnumerator
		{
			// Token: 0x0600587F RID: 22655 RVA: 0x00167C6C File Offset: 0x0016706C
			internal Enumerator(Visual3DCollection list)
			{
				this._list = list;
				this._index = -1;
				this._version = this._list._version;
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador foi avançado com êxito para o próximo elemento; caso contrário, <see langword="false" />.</returns>
			// Token: 0x06005880 RID: 22656 RVA: 0x00167C98 File Offset: 0x00167098
			public bool MoveNext()
			{
				if (this._list._version != this._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				int count = this._list.Count;
				if (this._index < count)
				{
					this._index++;
				}
				return this._index < count;
			}

			/// <summary>Redefine o enumerador com sua posição inicial, que é antes do primeiro elemento da coleção.</summary>
			// Token: 0x06005881 RID: 22657 RVA: 0x00167CF4 File Offset: 0x001670F4
			public void Reset()
			{
				if (this._list._version != this._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				this._index = -1;
			}

			/// <summary>Este membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x06005882 RID: 22658 RVA: 0x00167D2C File Offset: 0x0016712C
			void IDisposable.Dispose()
			{
			}

			/// <summary>Este membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x1700123C RID: 4668
			// (get) Token: 0x06005883 RID: 22659 RVA: 0x00167D3C File Offset: 0x0016713C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém o elemento atual na coleção.</summary>
			/// <returns>O elemento atual na coleção.</returns>
			// Token: 0x1700123D RID: 4669
			// (get) Token: 0x06005884 RID: 22660 RVA: 0x00167D50 File Offset: 0x00167150
			public Visual3D Current
			{
				get
				{
					if (this._index < 0 || this._index >= this._list.Count)
					{
						throw new InvalidOperationException(SR.Get("Enumerator_VerifyContext"));
					}
					return this._list[this._index];
				}
			}

			// Token: 0x04002905 RID: 10501
			private Visual3DCollection _list;

			// Token: 0x04002906 RID: 10502
			private int _index;

			// Token: 0x04002907 RID: 10503
			private int _version;
		}
	}
}
