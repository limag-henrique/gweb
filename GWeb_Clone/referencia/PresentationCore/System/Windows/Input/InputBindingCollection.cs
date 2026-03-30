using System;
using System.Collections;
using System.Collections.Generic;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Representa uma coleção ordenada de objetos de <see cref="T:System.Windows.Input.InputBinding" />.</summary>
	// Token: 0x02000212 RID: 530
	public sealed class InputBindingCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.InputBindingCollection" />.</summary>
		// Token: 0x06000E12 RID: 3602 RVA: 0x000358F8 File Offset: 0x00034CF8
		public InputBindingCollection()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.InputBindingCollection" /> usando os itens no <see cref="T:System.Collections.IList" /> especificado.</summary>
		/// <param name="inputBindings">A coleção cujos itens são copiados para o novo <see cref="T:System.Windows.Input.InputBindingCollection" />.</param>
		// Token: 0x06000E13 RID: 3603 RVA: 0x0003590C File Offset: 0x00034D0C
		public InputBindingCollection(IList inputBindings)
		{
			if (inputBindings != null && inputBindings.Count > 0)
			{
				this.AddRange(inputBindings);
			}
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00035934 File Offset: 0x00034D34
		internal InputBindingCollection(DependencyObject owner)
		{
			this._owner = owner;
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Input.InputBindingCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x06000E15 RID: 3605 RVA: 0x00035950 File Offset: 0x00034D50
		void ICollection.CopyTo(Array array, int index)
		{
			if (this._innerBindingList != null)
			{
				((ICollection)this._innerBindingList).CopyTo(array, index);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="key">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.TextEffectCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Input.InputBindingCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000E16 RID: 3606 RVA: 0x00035974 File Offset: 0x00034D74
		bool IList.Contains(object key)
		{
			return this.Contains(key as InputBinding);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Input.InputBindingCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06000E17 RID: 3607 RVA: 0x00035990 File Offset: 0x00034D90
		int IList.IndexOf(object value)
		{
			InputBinding inputBinding = value as InputBinding;
			if (inputBinding == null)
			{
				return -1;
			}
			return this.IndexOf(inputBinding);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Input.InputBindingCollection" />.</param>
		// Token: 0x06000E18 RID: 3608 RVA: 0x000359B0 File Offset: 0x00034DB0
		void IList.Insert(int index, object value)
		{
			this.Insert(index, value as InputBinding);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <param name="inputBinding">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Input.InputBindingCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x06000E19 RID: 3609 RVA: 0x000359CC File Offset: 0x00034DCC
		int IList.Add(object inputBinding)
		{
			this.Add(inputBinding as InputBinding);
			return 0;
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="inputBinding">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Input.InputBindingCollection" />.</param>
		// Token: 0x06000E1A RID: 3610 RVA: 0x000359E8 File Offset: 0x00034DE8
		void IList.Remove(object inputBinding)
		{
			this.Remove(inputBinding as InputBinding);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <param name="index">O índice com base em zero do elemento a ser obtido ou definido.</param>
		/// <returns>O elemento no índice especificado.</returns>
		// Token: 0x170001BA RID: 442
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				InputBinding inputBinding = value as InputBinding;
				if (inputBinding == null)
				{
					throw new NotSupportedException(SR.Get("CollectionOnlyAcceptsInputBindings"));
				}
				this[index] = inputBinding;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Input.InputBinding" /> no índice especificado.</summary>
		/// <param name="index">A posição na coleção.</param>
		/// <returns>O <see cref="T:System.Windows.Input.InputBinding" /> no índice especificado.</returns>
		// Token: 0x170001BB RID: 443
		public InputBinding this[int index]
		{
			get
			{
				if (this._innerBindingList != null)
				{
					return this._innerBindingList[index];
				}
				throw new ArgumentOutOfRangeException("index");
			}
			set
			{
				if (this._innerBindingList != null)
				{
					InputBinding inputBinding = null;
					if (index >= 0 && index < this._innerBindingList.Count)
					{
						inputBinding = this._innerBindingList[index];
					}
					this._innerBindingList[index] = value;
					if (inputBinding != null)
					{
						InheritanceContextHelper.RemoveContextFromObject(this._owner, inputBinding);
					}
					InheritanceContextHelper.ProvideContextForObject(this._owner, value);
					return;
				}
				throw new ArgumentOutOfRangeException("index");
			}
		}

		/// <summary>Adiciona o <see cref="T:System.Windows.Input.InputBinding" /> especificado a este <see cref="T:System.Windows.Input.InputBindingCollection" />.</summary>
		/// <param name="inputBinding">A associação a adicionar à coleção.</param>
		/// <returns>Sempre retorna 0. Isso se desvia da implementação de <see cref="T:System.Collections.IList" /> padrão para <see langword="Add" />, que deve retornar o índice em que o novo item foi adicionado à coleção.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="inputBinding" /> é <see langword="null" />.</exception>
		// Token: 0x06000E1F RID: 3615 RVA: 0x00035AE0 File Offset: 0x00034EE0
		public int Add(InputBinding inputBinding)
		{
			if (inputBinding != null)
			{
				if (this._innerBindingList == null)
				{
					this._innerBindingList = new List<InputBinding>(1);
				}
				this._innerBindingList.Add(inputBinding);
				InheritanceContextHelper.ProvideContextForObject(this._owner, inputBinding);
				return 0;
			}
			throw new NotSupportedException(SR.Get("CollectionOnlyAcceptsInputBindings"));
		}

		/// <summary>Obtém um valor que indica se o acesso essa <see cref="T:System.Windows.Input.InputBindingCollection" /> é sincronizado (thread-safe).</summary>
		/// <returns>
		///   <see langword="true" /> Se a coleção é thread-safe; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x00035B30 File Offset: 0x00034F30
		public bool IsSynchronized
		{
			get
			{
				return this._innerBindingList != null && ((ICollection)this._innerBindingList).IsSynchronized;
			}
		}

		/// <summary>Pesquisa pela primeira ocorrência do <see cref="T:System.Windows.Input.InputBinding" /> especificado nesta <see cref="T:System.Windows.Input.InputBindingCollection" />.</summary>
		/// <param name="value">O objeto a ser localizado na coleção.</param>
		/// <returns>O índice da primeira ocorrência de <paramref name="value" />, se encontrado; caso contrário, -1.</returns>
		// Token: 0x06000E21 RID: 3617 RVA: 0x00035B54 File Offset: 0x00034F54
		public int IndexOf(InputBinding value)
		{
			if (this._innerBindingList == null)
			{
				return -1;
			}
			return this._innerBindingList.IndexOf(value);
		}

		/// <summary>Adiciona os itens do <see cref="T:System.Collections.ICollection" /> especificado ao final desta <see cref="T:System.Windows.Input.InputBindingCollection" /></summary>
		/// <param name="collection">A coleção de itens a serem adicionados ao final desta <see cref="T:System.Windows.Input.InputBindingCollection" />.</param>
		// Token: 0x06000E22 RID: 3618 RVA: 0x00035B78 File Offset: 0x00034F78
		public void AddRange(ICollection collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (collection.Count > 0)
			{
				if (this._innerBindingList == null)
				{
					this._innerBindingList = new List<InputBinding>(collection.Count);
				}
				foreach (object obj in collection)
				{
					InputBinding inputBinding = obj as InputBinding;
					if (inputBinding == null)
					{
						throw new NotSupportedException(SR.Get("CollectionOnlyAcceptsInputBindings"));
					}
					this._innerBindingList.Add(inputBinding);
					InheritanceContextHelper.ProvideContextForObject(this._owner, inputBinding);
				}
			}
		}

		/// <summary>Insere o <see cref="T:System.Windows.Input.InputBinding" /> especificado nesta <see cref="T:System.Windows.Input.InputBindingCollection" /> no índice especificado.</summary>
		/// <param name="index">O índice de base zero no qual o <paramref name="inputBinding" /> será inserido.</param>
		/// <param name="inputBinding">A associação a inserir.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="inputBinding" /> é <see langword="null" />.</exception>
		// Token: 0x06000E23 RID: 3619 RVA: 0x00035C00 File Offset: 0x00035000
		public void Insert(int index, InputBinding inputBinding)
		{
			if (inputBinding == null)
			{
				throw new NotSupportedException(SR.Get("CollectionOnlyAcceptsInputBindings"));
			}
			if (this._innerBindingList != null)
			{
				this._innerBindingList.Insert(index, inputBinding);
				InheritanceContextHelper.ProvideContextForObject(this._owner, inputBinding);
			}
		}

		/// <summary>Remove a primeira ocorrência do <see cref="T:System.Windows.Input.InputBinding" /> especificado dessa <see cref="T:System.Windows.Input.InputBindingCollection" />.</summary>
		/// <param name="inputBinding">A associação a ser removida.</param>
		// Token: 0x06000E24 RID: 3620 RVA: 0x00035C44 File Offset: 0x00035044
		public void Remove(InputBinding inputBinding)
		{
			if (this._innerBindingList != null && inputBinding != null && this._innerBindingList.Remove(inputBinding))
			{
				InheritanceContextHelper.RemoveContextFromObject(this._owner, inputBinding);
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Input.InputBinding" /> especificado no índice especificado desta <see cref="T:System.Windows.Input.InputBindingCollection" />.</summary>
		/// <param name="index">O índice baseado em zero do <see cref="T:System.Windows.Input.InputBinding" /> a ser removido.</param>
		// Token: 0x06000E25 RID: 3621 RVA: 0x00035C78 File Offset: 0x00035078
		public void RemoveAt(int index)
		{
			if (this._innerBindingList != null)
			{
				InputBinding inputBinding = null;
				if (index >= 0 && index < this._innerBindingList.Count)
				{
					inputBinding = this._innerBindingList[index];
				}
				this._innerBindingList.RemoveAt(index);
				if (inputBinding != null)
				{
					InheritanceContextHelper.RemoveContextFromObject(this._owner, inputBinding);
				}
			}
		}

		/// <summary>Obtém um valor que indica se este <see cref="T:System.Windows.Input.InputBindingCollection" /> tem um tamanho fixo.</summary>
		/// <returns>
		///   <see langword="true" /> Se a coleção tem um tamanho fixo; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x00035CCC File Offset: 0x000350CC
		public bool IsFixedSize
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		/// <summary>Obtém o número de itens <see cref="T:System.Windows.Input.InputBinding" /> nesta coleção.</summary>
		/// <returns>Número de itens na coleção.</returns>
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x00035CE0 File Offset: 0x000350E0
		public int Count
		{
			get
			{
				if (this._innerBindingList == null)
				{
					return 0;
				}
				return this._innerBindingList.Count;
			}
		}

		/// <summary>Obtém um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Input.InputBindingCollection" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Input.InputBindingCollection" />.</returns>
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x00035D04 File Offset: 0x00035104
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Remove todos os itens desta <see cref="T:System.Windows.Input.InputBindingCollection" />.</summary>
		// Token: 0x06000E29 RID: 3625 RVA: 0x00035D14 File Offset: 0x00035114
		public void Clear()
		{
			if (this._innerBindingList != null)
			{
				List<InputBinding> list = new List<InputBinding>(this._innerBindingList);
				this._innerBindingList.Clear();
				this._innerBindingList = null;
				foreach (InputBinding oldValue in list)
				{
					InheritanceContextHelper.RemoveContextFromObject(this._owner, oldValue);
				}
			}
		}

		/// <summary>Obtém um enumerador que itera por esta <see cref="T:System.Windows.Input.InputBindingCollection" />.</summary>
		/// <returns>O enumerador para esta coleção.</returns>
		// Token: 0x06000E2A RID: 3626 RVA: 0x00035D9C File Offset: 0x0003519C
		public IEnumerator GetEnumerator()
		{
			if (this._innerBindingList != null)
			{
				return this._innerBindingList.GetEnumerator();
			}
			List<InputBinding> list = new List<InputBinding>(0);
			return list.GetEnumerator();
		}

		/// <summary>Obtém um valor que indica se este <see cref="T:System.Windows.Input.InputBindingCollection" /> é somente leitura.</summary>
		/// <returns>
		///   <see langword="true" /> se a coleção for somente leitura; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x00035DD4 File Offset: 0x000351D4
		public bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Input.InputBinding" /> especificado está neste <see cref="T:System.Windows.Input.InputBindingCollection" /></summary>
		/// <param name="key">A associação a ser localizada na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Input.InputBinding" /> especificado estiver na coleção; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000E2C RID: 3628 RVA: 0x00035DE8 File Offset: 0x000351E8
		public bool Contains(InputBinding key)
		{
			return this._innerBindingList != null && key != null && this._innerBindingList.Contains(key);
		}

		/// <summary>Copia todos os itens no <see cref="T:System.Windows.Input.InputBindingCollection" /> para a matriz unidimensional especificada, iniciando no índice especificado da matriz de destino.</summary>
		/// <param name="inputBindings">A matriz para dentro da qual a coleção é copiada.</param>
		/// <param name="index">A posição de índice em <paramref name="inputBindings" /> em que a cópia começa.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inputBindings" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que 0.</exception>
		// Token: 0x06000E2D RID: 3629 RVA: 0x00035E10 File Offset: 0x00035210
		public void CopyTo(InputBinding[] inputBindings, int index)
		{
			if (this._innerBindingList != null)
			{
				this._innerBindingList.CopyTo(inputBindings, index);
			}
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00035E34 File Offset: 0x00035234
		internal InputBinding FindMatch(object targetElement, InputEventArgs inputEventArgs)
		{
			for (int i = this.Count - 1; i >= 0; i--)
			{
				InputBinding inputBinding = this[i];
				if (inputBinding.Command != null && inputBinding.Gesture != null && inputBinding.Gesture.Matches(targetElement, inputEventArgs))
				{
					return inputBinding;
				}
			}
			return null;
		}

		// Token: 0x04000833 RID: 2099
		private List<InputBinding> _innerBindingList;

		// Token: 0x04000834 RID: 2100
		private bool _isReadOnly;

		// Token: 0x04000835 RID: 2101
		private DependencyObject _owner;
	}
}
