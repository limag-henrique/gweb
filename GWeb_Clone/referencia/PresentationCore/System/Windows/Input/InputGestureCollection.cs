using System;
using System.Collections;
using System.Collections.Generic;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Representa uma coleção ordenada de objetos de <see cref="T:System.Windows.Input.InputGesture" />.</summary>
	// Token: 0x02000214 RID: 532
	public sealed class InputGestureCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.InputGestureCollection" />.</summary>
		// Token: 0x06000E31 RID: 3633 RVA: 0x00035E94 File Offset: 0x00035294
		public InputGestureCollection()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.InputGestureCollection" /> usando os elementos na <see cref="T:System.Collections.IList" /> especificada.</summary>
		/// <param name="inputGestures">A coleção cujos elementos são copiados para o novo <see cref="T:System.Windows.Input.InputGestureCollection" />.</param>
		// Token: 0x06000E32 RID: 3634 RVA: 0x00035EA8 File Offset: 0x000352A8
		public InputGestureCollection(IList inputGestures)
		{
			if (inputGestures != null && inputGestures.Count > 0)
			{
				this.AddRange(inputGestures);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Input.InputGestureCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x06000E33 RID: 3635 RVA: 0x00035ED0 File Offset: 0x000352D0
		void ICollection.CopyTo(Array array, int index)
		{
			if (this._innerGestureList != null)
			{
				((ICollection)this._innerGestureList).CopyTo(array, index);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="key">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.TextEffectCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Input.InputGestureCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000E34 RID: 3636 RVA: 0x00035EF4 File Offset: 0x000352F4
		bool IList.Contains(object key)
		{
			return this.Contains(key as InputGesture);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Input.InputGestureCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06000E35 RID: 3637 RVA: 0x00035F10 File Offset: 0x00035310
		int IList.IndexOf(object value)
		{
			InputGesture inputGesture = value as InputGesture;
			if (inputGesture == null)
			{
				return -1;
			}
			return this.IndexOf(inputGesture);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Input.InputGestureCollection" />.</param>
		// Token: 0x06000E36 RID: 3638 RVA: 0x00035F30 File Offset: 0x00035330
		void IList.Insert(int index, object value)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.Get("ReadOnlyInputGesturesCollection"));
			}
			this.Insert(index, value as InputGesture);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <param name="inputGesture">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Input.InputGestureCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x06000E37 RID: 3639 RVA: 0x00035F64 File Offset: 0x00035364
		int IList.Add(object inputGesture)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.Get("ReadOnlyInputGesturesCollection"));
			}
			return this.Add(inputGesture as InputGesture);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="inputGesture">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Input.InputGestureCollection" />.</param>
		// Token: 0x06000E38 RID: 3640 RVA: 0x00035F98 File Offset: 0x00035398
		void IList.Remove(object inputGesture)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.Get("ReadOnlyInputGesturesCollection"));
			}
			this.Remove(inputGesture as InputGesture);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <param name="index">O índice com base em zero do elemento a ser obtido ou definido.</param>
		/// <returns>O elemento no índice especificado.</returns>
		// Token: 0x170001C1 RID: 449
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				InputGesture inputGesture = value as InputGesture;
				if (inputGesture == null)
				{
					throw new NotSupportedException(SR.Get("CollectionOnlyAcceptsInputGestures"));
				}
				this[index] = inputGesture;
			}
		}

		/// <summary>Obtém um enumerador que itera por esta <see cref="T:System.Windows.Input.InputGestureCollection" />.</summary>
		/// <returns>O enumerador para esta coleção.</returns>
		// Token: 0x06000E3B RID: 3643 RVA: 0x00036010 File Offset: 0x00035410
		public IEnumerator GetEnumerator()
		{
			if (this._innerGestureList != null)
			{
				return this._innerGestureList.GetEnumerator();
			}
			List<InputGesture> list = new List<InputGesture>(0);
			return list.GetEnumerator();
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Input.InputGesture" /> no índice especificado.</summary>
		/// <param name="index">A posição na coleção.</param>
		/// <returns>O gesto no índice especificado.</returns>
		// Token: 0x170001C2 RID: 450
		public InputGesture this[int index]
		{
			get
			{
				if (this._innerGestureList == null)
				{
					return null;
				}
				return this._innerGestureList[index];
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new NotSupportedException(SR.Get("ReadOnlyInputGesturesCollection"));
				}
				this.EnsureList();
				if (this._innerGestureList != null)
				{
					this._innerGestureList[index] = value;
				}
			}
		}

		/// <summary>Obtém um valor que indica se este <see cref="T:System.Windows.Input.InputGestureCollection" /> é sincronizado (thread-safe).</summary>
		/// <returns>
		///   <see langword="true" /> Se a coleção é thread-safe; Caso contrário, <see langword="false" />.  O valor padrão é <see langword="false" />.</returns>
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x000360AC File Offset: 0x000354AC
		public bool IsSynchronized
		{
			get
			{
				return this._innerGestureList != null && ((ICollection)this._innerGestureList).IsSynchronized;
			}
		}

		/// <summary>Obtém um objeto que pode ser usado para sincronizar o acesso a este <see cref="T:System.Windows.Input.InputGestureCollection" />.</summary>
		/// <returns>O objeto que pode ser usado para sincronizar o acesso à coleção.</returns>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x000360D0 File Offset: 0x000354D0
		public object SyncRoot
		{
			get
			{
				if (this._innerGestureList == null)
				{
					return this;
				}
				return ((ICollection)this._innerGestureList).SyncRoot;
			}
		}

		/// <summary>Pesquisa pela primeira ocorrência do <see cref="T:System.Windows.Input.InputGesture" /> especificado nesta <see cref="T:System.Windows.Input.InputGestureCollection" />.</summary>
		/// <param name="value">O gesto a localizar na coleção.</param>
		/// <returns>O índice da primeira ocorrência de <paramref name="value" />, se encontrado; caso contrário, -1.</returns>
		// Token: 0x06000E40 RID: 3648 RVA: 0x000360F4 File Offset: 0x000354F4
		public int IndexOf(InputGesture value)
		{
			if (this._innerGestureList == null)
			{
				return -1;
			}
			return this._innerGestureList.IndexOf(value);
		}

		/// <summary>Remove o <see cref="T:System.Windows.Input.InputGesture" /> especificado no índice especificado desta <see cref="T:System.Windows.Input.InputGestureCollection" />.</summary>
		/// <param name="index">O índice baseado em zero do gesto a ser removido.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">o índice é menor que 0.</exception>
		// Token: 0x06000E41 RID: 3649 RVA: 0x00036118 File Offset: 0x00035518
		public void RemoveAt(int index)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.Get("ReadOnlyInputGesturesCollection"));
			}
			if (this._innerGestureList != null)
			{
				this._innerGestureList.RemoveAt(index);
			}
		}

		/// <summary>Obtém um valor que indica se este <see cref="T:System.Windows.Input.InputGestureCollection" /> tem um tamanho fixo.</summary>
		/// <returns>
		///   <see langword="true" /> Se a coleção tem um tamanho fixo; Caso contrário, <see langword="false" />.  O valor padrão é <see langword="false" />.</returns>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x00036154 File Offset: 0x00035554
		public bool IsFixedSize
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		/// <summary>Adiciona o <see cref="T:System.Windows.Input.InputGesture" /> especificado a este <see cref="T:System.Windows.Input.InputGestureCollection" />.</summary>
		/// <param name="inputGesture">O gesto a adicionar à coleção.</param>
		/// <returns>0, se a operação foi bem-sucedida (observe que isso não é o índice do item adicionado).</returns>
		/// <exception cref="T:System.NotSupportedException">a coleção é somente leitura.</exception>
		/// <exception cref="T:System.ArgumentNullException">o gesto é <see langword="null" />.</exception>
		// Token: 0x06000E43 RID: 3651 RVA: 0x00036168 File Offset: 0x00035568
		public int Add(InputGesture inputGesture)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.Get("ReadOnlyInputGesturesCollection"));
			}
			if (inputGesture == null)
			{
				throw new ArgumentNullException("inputGesture");
			}
			this.EnsureList();
			this._innerGestureList.Add(inputGesture);
			return 0;
		}

		/// <summary>Adiciona os elementos do <see cref="T:System.Collections.ICollection" /> especificado ao final deste <see cref="T:System.Windows.Input.InputGestureCollection" />.</summary>
		/// <param name="collection">A coleção de itens a serem adicionados ao final desta <see cref="T:System.Windows.Input.InputGestureCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">A coleção é somente leitura.</exception>
		/// <exception cref="T:System.ArgumentNullException">A coleção para adicionar é <see langword="null" />.</exception>
		// Token: 0x06000E44 RID: 3652 RVA: 0x000361B0 File Offset: 0x000355B0
		public void AddRange(ICollection collection)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.Get("ReadOnlyInputGesturesCollection"));
			}
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (collection.Count > 0)
			{
				if (this._innerGestureList == null)
				{
					this._innerGestureList = new List<InputGesture>(collection.Count);
				}
				foreach (object obj in collection)
				{
					InputGesture inputGesture = obj as InputGesture;
					if (inputGesture == null)
					{
						throw new NotSupportedException(SR.Get("CollectionOnlyAcceptsInputGestures"));
					}
					this._innerGestureList.Add(inputGesture);
				}
			}
		}

		/// <summary>Insere o <see cref="T:System.Windows.Input.InputGesture" /> especificado nesta <see cref="T:System.Windows.Input.InputGestureCollection" /> no índice especificado.</summary>
		/// <param name="index">O índice no qual inserir <paramref name="inputGesture" />.</param>
		/// <param name="inputGesture">O gesto a inserir.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="inputGesture" /> é <see langword="null" />.</exception>
		// Token: 0x06000E45 RID: 3653 RVA: 0x00036244 File Offset: 0x00035644
		public void Insert(int index, InputGesture inputGesture)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.Get("ReadOnlyInputGesturesCollection"));
			}
			if (inputGesture == null)
			{
				throw new NotSupportedException(SR.Get("CollectionOnlyAcceptsInputGestures"));
			}
			if (this._innerGestureList != null)
			{
				this._innerGestureList.Insert(index, inputGesture);
			}
		}

		/// <summary>Obtém um valor que indica se este <see cref="T:System.Windows.Input.InputGestureCollection" /> é somente leitura.  O valor padrão é <see langword="false" />.</summary>
		/// <returns>
		///   <see langword="true" /> Se a coleção somente leitura; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x00036294 File Offset: 0x00035694
		public bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		/// <summary>Remove a primeira ocorrência do <see cref="T:System.Windows.Input.InputGesture" /> especificado dessa <see cref="T:System.Windows.Input.InputGestureCollection" />.</summary>
		/// <param name="inputGesture">O gesto a remover.</param>
		/// <exception cref="T:System.NotSupportedException">A coleção é somente leitura.</exception>
		/// <exception cref="T:System.ArgumentNullException">O gesto é <see langword="null" />.</exception>
		// Token: 0x06000E47 RID: 3655 RVA: 0x000362A8 File Offset: 0x000356A8
		public void Remove(InputGesture inputGesture)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.Get("ReadOnlyInputGesturesCollection"));
			}
			if (inputGesture == null)
			{
				throw new ArgumentNullException("inputGesture");
			}
			if (this._innerGestureList != null && this._innerGestureList.Contains(inputGesture))
			{
				this._innerGestureList.Remove(inputGesture);
			}
		}

		/// <summary>Obtém o número de itens <see cref="T:System.Windows.Input.InputGesture" /> nesta <see cref="T:System.Windows.Input.InputGestureCollection" />.</summary>
		/// <returns>O número de gestos na coleção.</returns>
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x00036300 File Offset: 0x00035700
		public int Count
		{
			get
			{
				if (this._innerGestureList == null)
				{
					return 0;
				}
				return this._innerGestureList.Count;
			}
		}

		/// <summary>Remove todos os elementos do <see cref="T:System.Windows.Input.InputGestureCollection" />.</summary>
		/// <exception cref="T:System.NotSupportedException">A coleção é somente leitura.</exception>
		// Token: 0x06000E49 RID: 3657 RVA: 0x00036324 File Offset: 0x00035724
		public void Clear()
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.Get("ReadOnlyInputGesturesCollection"));
			}
			if (this._innerGestureList != null)
			{
				this._innerGestureList.Clear();
				this._innerGestureList = null;
			}
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Input.InputGesture" /> especificado está na coleção.</summary>
		/// <param name="key">O gesto a localizar na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se o gesto especificado estiver na coleção; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000E4A RID: 3658 RVA: 0x00036364 File Offset: 0x00035764
		public bool Contains(InputGesture key)
		{
			return this._innerGestureList != null && key != null && this._innerGestureList.Contains(key);
		}

		/// <summary>Copia todos os itens no <see cref="T:System.Windows.Input.InputGestureCollection" /> para a matriz unidimensional especificada, iniciando no índice especificado da matriz de destino.</summary>
		/// <param name="inputGestures">Uma matriz para a qual a coleção é copiada.</param>
		/// <param name="index">A posição de índice no <paramref name="inputGestures" /> no qual a cópia começa.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inputGestures" /> é um <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que 0.</exception>
		// Token: 0x06000E4B RID: 3659 RVA: 0x0003638C File Offset: 0x0003578C
		public void CopyTo(InputGesture[] inputGestures, int index)
		{
			if (this._innerGestureList != null)
			{
				this._innerGestureList.CopyTo(inputGestures, index);
			}
		}

		/// <summary>Define este <see cref="T:System.Windows.Input.InputGestureCollection" /> como somente leitura.</summary>
		// Token: 0x06000E4C RID: 3660 RVA: 0x000363B0 File Offset: 0x000357B0
		public void Seal()
		{
			this._isReadOnly = true;
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x000363C4 File Offset: 0x000357C4
		private void EnsureList()
		{
			if (this._innerGestureList == null)
			{
				this._innerGestureList = new List<InputGesture>(1);
			}
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x000363E8 File Offset: 0x000357E8
		internal InputGesture FindMatch(object targetElement, InputEventArgs inputEventArgs)
		{
			for (int i = 0; i < this.Count; i++)
			{
				InputGesture inputGesture = this[i];
				if (inputGesture.Matches(targetElement, inputEventArgs))
				{
					return inputGesture;
				}
			}
			return null;
		}

		// Token: 0x04000836 RID: 2102
		private List<InputGesture> _innerGestureList;

		// Token: 0x04000837 RID: 2103
		private bool _isReadOnly;
	}
}
