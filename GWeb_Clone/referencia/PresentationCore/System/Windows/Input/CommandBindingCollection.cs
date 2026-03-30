using System;
using System.Collections;
using System.Collections.Generic;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Input.CommandBinding" /> .</summary>
	// Token: 0x0200020C RID: 524
	public sealed class CommandBindingCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.CommandBindingCollection" />.</summary>
		// Token: 0x06000DB2 RID: 3506 RVA: 0x00034300 File Offset: 0x00033700
		public CommandBindingCollection()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.CommandBindingCollection" /> usando os itens no <see cref="T:System.Collections.IList" /> especificado.</summary>
		/// <param name="commandBindings">A coleção cujos itens são copiados para o novo <see cref="T:System.Windows.Input.CommandBindingCollection" />.</param>
		// Token: 0x06000DB3 RID: 3507 RVA: 0x00034314 File Offset: 0x00033714
		public CommandBindingCollection(IList commandBindings)
		{
			if (commandBindings != null && commandBindings.Count > 0)
			{
				this.AddRange(commandBindings);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Input.CommandBindingCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x06000DB4 RID: 3508 RVA: 0x0003433C File Offset: 0x0003373C
		void ICollection.CopyTo(Array array, int index)
		{
			if (this._innerCBList != null)
			{
				((ICollection)this._innerCBList).CopyTo(array, index);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="key">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.TextEffectCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Input.CommandBindingCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000DB5 RID: 3509 RVA: 0x00034360 File Offset: 0x00033760
		bool IList.Contains(object key)
		{
			return this.Contains(key as CommandBinding);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Input.CommandBindingCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06000DB6 RID: 3510 RVA: 0x0003437C File Offset: 0x0003377C
		int IList.IndexOf(object value)
		{
			CommandBinding commandBinding = value as CommandBinding;
			if (commandBinding == null)
			{
				return -1;
			}
			return this.IndexOf(commandBinding);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Input.CommandBindingCollection" />.</param>
		// Token: 0x06000DB7 RID: 3511 RVA: 0x0003439C File Offset: 0x0003379C
		void IList.Insert(int index, object value)
		{
			this.Insert(index, value as CommandBinding);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <param name="commandBinding">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Input.CommandBindingCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x06000DB8 RID: 3512 RVA: 0x000343B8 File Offset: 0x000337B8
		int IList.Add(object commandBinding)
		{
			return this.Add(commandBinding as CommandBinding);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="commandBinding">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Input.CommandBindingCollection" />.</param>
		// Token: 0x06000DB9 RID: 3513 RVA: 0x000343D4 File Offset: 0x000337D4
		void IList.Remove(object commandBinding)
		{
			this.Remove(commandBinding as CommandBinding);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <param name="index">O índice com base em zero do elemento a ser obtido ou definido.</param>
		/// <returns>O elemento no índice especificado.</returns>
		// Token: 0x170001A7 RID: 423
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				CommandBinding commandBinding = value as CommandBinding;
				if (commandBinding == null)
				{
					throw new NotSupportedException(SR.Get("CollectionOnlyAcceptsCommandBindings"));
				}
				this[index] = commandBinding;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Input.CommandBinding" /> no índice especificado.</summary>
		/// <param name="index">A posição na coleção.</param>
		/// <returns>A associação no índice especificado.</returns>
		// Token: 0x170001A8 RID: 424
		public CommandBinding this[int index]
		{
			get
			{
				if (this._innerCBList == null)
				{
					return null;
				}
				return this._innerCBList[index];
			}
			set
			{
				if (this._innerCBList != null)
				{
					this._innerCBList[index] = value;
				}
			}
		}

		/// <summary>Adiciona o <see cref="T:System.Windows.Input.CommandBinding" /> especificado a este <see cref="T:System.Windows.Input.CommandBindingCollection" />.</summary>
		/// <param name="commandBinding">A associação a adicionar à coleção.</param>
		/// <returns>0, se a operação foi bem-sucedida (observe que isso não é o índice do item adicionado).</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="commandBinding" /> é nulo.</exception>
		// Token: 0x06000DBE RID: 3518 RVA: 0x0003447C File Offset: 0x0003387C
		public int Add(CommandBinding commandBinding)
		{
			if (commandBinding != null)
			{
				if (this._innerCBList == null)
				{
					this._innerCBList = new List<CommandBinding>(1);
				}
				this._innerCBList.Add(commandBinding);
				return 0;
			}
			throw new NotSupportedException(SR.Get("CollectionOnlyAcceptsCommandBindings"));
		}

		/// <summary>Adiciona os itens do <see cref="T:System.Collections.ICollection" /> especificado ao final desta <see cref="T:System.Windows.Input.CommandBindingCollection" />.</summary>
		/// <param name="collection">A coleção de itens a serem adicionados ao final desta <see cref="T:System.Windows.Input.CommandBindingCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">A coleção para adicionar é <see langword="null" />.</exception>
		// Token: 0x06000DBF RID: 3519 RVA: 0x000344C0 File Offset: 0x000338C0
		public void AddRange(ICollection collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (collection.Count > 0)
			{
				if (this._innerCBList == null)
				{
					this._innerCBList = new List<CommandBinding>(collection.Count);
				}
				foreach (object obj in collection)
				{
					CommandBinding commandBinding = obj as CommandBinding;
					if (commandBinding == null)
					{
						throw new NotSupportedException(SR.Get("CollectionOnlyAcceptsCommandBindings"));
					}
					this._innerCBList.Add(commandBinding);
				}
			}
		}

		/// <summary>Insere o <see cref="T:System.Windows.Input.CommandBinding" /> especificado nesta <see cref="T:System.Windows.Input.CommandBindingCollection" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual inserir <paramref name="commandBinding" /></param>
		/// <param name="commandBinding">A associação a inserir.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="commandBinding" /> é <see langword="null" />.</exception>
		// Token: 0x06000DC0 RID: 3520 RVA: 0x0003453C File Offset: 0x0003393C
		public void Insert(int index, CommandBinding commandBinding)
		{
			if (commandBinding == null)
			{
				throw new NotSupportedException(SR.Get("CollectionOnlyAcceptsCommandBindings"));
			}
			if (this._innerCBList != null)
			{
				this._innerCBList.Insert(index, commandBinding);
				return;
			}
		}

		/// <summary>Remove a primeira ocorrência do <see cref="T:System.Windows.Input.CommandBinding" /> especificado dessa <see cref="T:System.Windows.Input.CommandBindingCollection" />.</summary>
		/// <param name="commandBinding">A associação a ser removida.</param>
		// Token: 0x06000DC1 RID: 3521 RVA: 0x00034574 File Offset: 0x00033974
		public void Remove(CommandBinding commandBinding)
		{
			if (this._innerCBList != null && commandBinding != null)
			{
				this._innerCBList.Remove(commandBinding);
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Input.CommandBinding" /> especificado no índice especificado desta <see cref="T:System.Windows.Input.CommandBindingCollection" />.</summary>
		/// <param name="index">O índice baseado em zero do <see cref="T:System.Windows.Input.CommandBinding" /> a ser removido.</param>
		// Token: 0x06000DC2 RID: 3522 RVA: 0x0003459C File Offset: 0x0003399C
		public void RemoveAt(int index)
		{
			if (this._innerCBList != null)
			{
				this._innerCBList.RemoveAt(index);
			}
		}

		/// <summary>Obtém um valor que indica se este <see cref="T:System.Windows.Input.CommandBindingCollection" /> tem um tamanho fixo.</summary>
		/// <returns>
		///   <see langword="true" /> Se a coleção tem um tamanho fixo; Caso contrário, <see langword="false" />.  O valor padrão é <see langword="false" />.</returns>
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x000345C0 File Offset: 0x000339C0
		public bool IsFixedSize
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		/// <summary>Obtém um valor que indica se o acesso essa <see cref="T:System.Windows.Input.CommandBindingCollection" /> é sincronizado (thread-safe).</summary>
		/// <returns>
		///   <see langword="true" /> Se a coleção é thread-safe; Caso contrário, <see langword="false" />.  O valor padrão é <see langword="false" />.</returns>
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x000345D4 File Offset: 0x000339D4
		public bool IsSynchronized
		{
			get
			{
				return this._innerCBList != null && ((ICollection)this._innerCBList).IsSynchronized;
			}
		}

		/// <summary>Obtém um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Input.CommandBindingCollection" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Input.CommandBindingCollection" />.</returns>
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x000345F8 File Offset: 0x000339F8
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.Input.CommandBindingCollection" /> é somente leitura.</summary>
		/// <returns>
		///   <see langword="true" /> se a coleção for somente leitura; caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x00034608 File Offset: 0x00033A08
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Obtém o número de itens <see cref="T:System.Windows.Input.CommandBinding" /> nesta <see cref="T:System.Windows.Input.CommandBindingCollection" />.</summary>
		/// <returns>O número de associações na coleção.</returns>
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00034618 File Offset: 0x00033A18
		public int Count
		{
			get
			{
				if (this._innerCBList == null)
				{
					return 0;
				}
				return this._innerCBList.Count;
			}
		}

		/// <summary>Remove todos os itens desta <see cref="T:System.Windows.Input.CommandBindingCollection" />.</summary>
		// Token: 0x06000DC8 RID: 3528 RVA: 0x0003463C File Offset: 0x00033A3C
		public void Clear()
		{
			if (this._innerCBList != null)
			{
				this._innerCBList.Clear();
				this._innerCBList = null;
			}
		}

		/// <summary>Pesquisa pela primeira ocorrência do <see cref="T:System.Windows.Input.CommandBinding" /> especificado nesta <see cref="T:System.Windows.Input.CommandBindingCollection" />.</summary>
		/// <param name="value">A associação a ser localizada na coleção.</param>
		/// <returns>O índice da primeira ocorrência de <paramref name="value" />, se encontrado; caso contrário, -1.</returns>
		// Token: 0x06000DC9 RID: 3529 RVA: 0x00034664 File Offset: 0x00033A64
		public int IndexOf(CommandBinding value)
		{
			if (this._innerCBList == null)
			{
				return -1;
			}
			return this._innerCBList.IndexOf(value);
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Input.CommandBinding" /> especificado está neste <see cref="T:System.Windows.Input.CommandBindingCollection" />.</summary>
		/// <param name="commandBinding">A associação a ser localizada na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Input.CommandBinding" /> especificado estiver na coleção; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000DCA RID: 3530 RVA: 0x00034688 File Offset: 0x00033A88
		public bool Contains(CommandBinding commandBinding)
		{
			return this._innerCBList != null && commandBinding != null && this._innerCBList.Contains(commandBinding);
		}

		/// <summary>Copia todos os itens no <see cref="T:System.Windows.Input.CommandBindingCollection" /> para a matriz unidimensional especificada, iniciando no índice especificado da matriz de destino.</summary>
		/// <param name="commandBindings">A matriz para dentro da qual a coleção é copiada.</param>
		/// <param name="index">A posição de índice em <paramref name="commandBindings" /> em que a cópia começa.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="commandBindings" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que 0.</exception>
		// Token: 0x06000DCB RID: 3531 RVA: 0x000346B0 File Offset: 0x00033AB0
		public void CopyTo(CommandBinding[] commandBindings, int index)
		{
			if (this._innerCBList != null)
			{
				this._innerCBList.CopyTo(commandBindings, index);
			}
		}

		/// <summary>Obtém um enumerador que itera por esta <see cref="T:System.Windows.Input.CommandBindingCollection" />.</summary>
		/// <returns>O enumerador para esta coleção.</returns>
		// Token: 0x06000DCC RID: 3532 RVA: 0x000346D4 File Offset: 0x00033AD4
		public IEnumerator GetEnumerator()
		{
			if (this._innerCBList != null)
			{
				return this._innerCBList.GetEnumerator();
			}
			List<CommandBinding> list = new List<CommandBinding>(0);
			return list.GetEnumerator();
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0003470C File Offset: 0x00033B0C
		internal ICommand FindMatch(object targetElement, InputEventArgs inputEventArgs)
		{
			for (int i = 0; i < this.Count; i++)
			{
				CommandBinding commandBinding = this[i];
				RoutedCommand routedCommand = commandBinding.Command as RoutedCommand;
				if (routedCommand != null)
				{
					InputGestureCollection inputGesturesInternal = routedCommand.InputGesturesInternal;
					if (inputGesturesInternal != null && inputGesturesInternal.FindMatch(targetElement, inputEventArgs) != null)
					{
						return routedCommand;
					}
				}
			}
			return null;
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00034758 File Offset: 0x00033B58
		internal CommandBinding FindMatch(ICommand command, ref int index)
		{
			while (index < this.Count)
			{
				int num = index;
				index = num + 1;
				CommandBinding commandBinding = this[num];
				if (commandBinding.Command == command)
				{
					return commandBinding;
				}
			}
			return null;
		}

		// Token: 0x04000820 RID: 2080
		private List<CommandBinding> _innerCBList;
	}
}
