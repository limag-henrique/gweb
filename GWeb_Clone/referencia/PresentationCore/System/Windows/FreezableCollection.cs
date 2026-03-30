using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Media.Animation;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.DependencyObject" />, <see cref="T:System.Windows.Freezable" /> ou <see cref="T:System.Windows.Media.Animation.Animatable" />. <see cref="T:System.Windows.FreezableCollection`1" /> é, ele próprio, de um tipo <see cref="T:System.Windows.Media.Animation.Animatable" />.</summary>
	/// <typeparam name="T">O tipo de coleção. Esse tipo deve ser um <see cref="T:System.Windows.DependencyObject" /> ou uma classe derivada.</typeparam>
	// Token: 0x020001BD RID: 445
	public class FreezableCollection<T> : Animatable, IList, ICollection, IEnumerable, IList<T>, ICollection<T>, IEnumerable<T>, INotifyCollectionChanged, INotifyPropertyChanged where T : DependencyObject
	{
		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.FreezableCollection`1" /> que está vazia e tem a capacidade inicial padrão.</summary>
		// Token: 0x06000725 RID: 1829 RVA: 0x00020394 File Offset: 0x0001F794
		public FreezableCollection()
		{
			this._collection = new List<T>();
		}

		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.FreezableCollection`1" /> que está vazia e tem a capacidade inicial especificada.</summary>
		/// <param name="capacity">Um valor que é maior ou igual a 0 que especifica o número de elementos que a nova coleção pode armazenar inicialmente.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> é menor que 0.</exception>
		// Token: 0x06000726 RID: 1830 RVA: 0x000203C0 File Offset: 0x0001F7C0
		public FreezableCollection(int capacity)
		{
			this._collection = new List<T>(capacity);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.FreezableCollection`1" /> que contém os mesmos elementos que a coleção especificada.</summary>
		/// <param name="collection">A coleção cujos itens devem ser adicionados à nova <see cref="T:System.Windows.FreezableCollection`1" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="collection" /> é <see langword="null" />.</exception>
		// Token: 0x06000727 RID: 1831 RVA: 0x000203EC File Offset: 0x0001F7EC
		public FreezableCollection(IEnumerable<T> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				int count = this.GetCount(collection);
				if (count > 0)
				{
					this._collection = new List<T>(count);
				}
				else
				{
					this._collection = new List<T>();
				}
				foreach (T t in collection)
				{
					if (t == null)
					{
						throw new ArgumentException(SR.Get("Collection_NoNull"));
					}
					base.OnFreezablePropertyChanged(null, t);
					this._collection.Add(t);
				}
				base.WritePostscript();
				return;
			}
			throw new ArgumentNullException("collection");
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.FreezableCollection`1" /> e de seu conteúdo, fazendo cópias em profundidade. Se essa coleção (ou seu conteúdo) tem propriedades de dependência animadas, o valor da propriedade base, não o seu valor animado atual, é copiado.</summary>
		/// <returns>Uma cópia modificável desta coleção e seu conteúdo. O valor <see cref="P:System.Windows.Freezable.IsFrozen" /> da cópia é <see langword="false" />.</returns>
		// Token: 0x06000728 RID: 1832 RVA: 0x000204C0 File Offset: 0x0001F8C0
		public new FreezableCollection<T> Clone()
		{
			return (FreezableCollection<T>)base.Clone();
		}

		/// <summary>Cria uma cópia modificável desse <see cref="T:System.Windows.FreezableCollection`1" /> e do respectivo conteúdo, fazendo cópias em profundidade dos valores atuais desse objeto. Se esse objeto (ou os objetos contidos nele) tiver propriedades de dependência animadas, os valores animados atuais delas serão copiados.</summary>
		/// <returns>Um clone modificável desta coleção e do respectivo conteúdo. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06000729 RID: 1833 RVA: 0x000204D8 File Offset: 0x0001F8D8
		public new FreezableCollection<T> CloneCurrentValue()
		{
			return (FreezableCollection<T>)base.CloneCurrentValue();
		}

		/// <summary>Adiciona o objeto especificado ao final da <see cref="T:System.Windows.FreezableCollection`1" />.</summary>
		/// <param name="value">O objeto a ser adicionado ao final do <see cref="T:System.Windows.FreezableCollection`1" />. Este valor pode não ser <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">O <see cref="T:System.Windows.FreezableCollection`1" /> está congelado (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dele é <see langword="true" />).</exception>
		// Token: 0x0600072A RID: 1834 RVA: 0x000204F0 File Offset: 0x0001F8F0
		public void Add(T value)
		{
			this.AddHelper(value);
		}

		/// <summary>Remove todos os elementos da coleção.</summary>
		// Token: 0x0600072B RID: 1835 RVA: 0x00020508 File Offset: 0x0001F908
		public void Clear()
		{
			this.CheckReentrancy();
			base.WritePreamble();
			for (int i = this._collection.Count - 1; i >= 0; i--)
			{
				base.OnFreezablePropertyChanged(this._collection[i], null);
			}
			this._collection.Clear();
			this._version += 1U;
			base.WritePostscript();
			this.OnCollectionChanged(NotifyCollectionChangedAction.Reset, 0, default(T), 0, default(T));
		}

		/// <summary>Determina se esse <see cref="T:System.Windows.FreezableCollection`1" /> contém o valor especificado.</summary>
		/// <param name="value">O objeto a ser localizado nessa coleção. Esse objeto pode ser <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> se o valor for encontrado no <see cref="T:System.Windows.FreezableCollection`1" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600072C RID: 1836 RVA: 0x0002058C File Offset: 0x0001F98C
		public bool Contains(T value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Pesquisa o objeto especificado e retorna o índice baseado em zero da primeira ocorrência dentro de todo o <see cref="T:System.Windows.FreezableCollection`1" />.</summary>
		/// <param name="value">O objeto a ser localizado no <see cref="T:System.Windows.FreezableCollection`1" />.</param>
		/// <returns>O índice baseado em zero da primeira ocorrência de <paramref name="value" /> em todo o <see cref="T:System.Windows.FreezableCollection`1" />, se encontrado; caso contrário, -1.</returns>
		// Token: 0x0600072D RID: 1837 RVA: 0x000205AC File Offset: 0x0001F9AC
		public int IndexOf(T value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Insere um objeto especificado no <see cref="T:System.Windows.FreezableCollection`1" /> no índice especificado.</summary>
		/// <param name="index">O índice de base zero no qual o <paramref name="value" /> deve ser inserido.</param>
		/// <param name="value">O objeto a ser inserido.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que 0.  
		///
		/// ou - 
		/// <paramref name="index" /> é maior que <see cref="P:System.Windows.FreezableCollection`1.Count" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">O <see cref="T:System.Windows.FreezableCollection`1" /> está congelado (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dele é <see langword="true" />).</exception>
		// Token: 0x0600072E RID: 1838 RVA: 0x000205CC File Offset: 0x0001F9CC
		public void Insert(int index, T value)
		{
			if (value == null)
			{
				throw new ArgumentException(SR.Get("Collection_NoNull"));
			}
			this.CheckReentrancy();
			base.WritePreamble();
			base.OnFreezablePropertyChanged(null, value);
			this._collection.Insert(index, value);
			this._version += 1U;
			base.WritePostscript();
			this.OnCollectionChanged(NotifyCollectionChangedAction.Add, 0, default(T), index, value);
		}

		/// <summary>Remove a primeira ocorrência do objeto especificado da <see cref="T:System.Windows.FreezableCollection`1" />.</summary>
		/// <param name="value">O objeto a ser removido.</param>
		/// <returns>
		///   <see langword="true" /> se uma ocorrência de <paramref name="value" /> foi encontrada na coleção e removida; <see langword="false" /> se <paramref name="value" /> não pôde ser encontrado na coleção.</returns>
		/// <exception cref="T:System.InvalidOperationException">O <see cref="T:System.Windows.FreezableCollection`1" /> está congelado (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dele é <see langword="true" />).</exception>
		// Token: 0x0600072F RID: 1839 RVA: 0x00020640 File Offset: 0x0001FA40
		public bool Remove(T value)
		{
			base.WritePreamble();
			int num = this.IndexOf(value);
			if (num >= 0)
			{
				this.CheckReentrancy();
				T t = this._collection[num];
				base.OnFreezablePropertyChanged(t, null);
				this._collection.RemoveAt(num);
				this._version += 1U;
				base.WritePostscript();
				this.OnCollectionChanged(NotifyCollectionChangedAction.Remove, num, t, 0, default(T));
				return true;
			}
			return false;
		}

		/// <summary>Remove o objeto no índice de base zero especificado da <see cref="T:System.Windows.FreezableCollection`1" />.</summary>
		/// <param name="index">O índice de base zero do objeto a ser removido.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que 0.  
		///
		/// ou - 
		/// <paramref name="index" /> é maior que <see cref="P:System.Windows.FreezableCollection`1.Count" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">O <see cref="T:System.Windows.FreezableCollection`1" /> está congelado (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dele é <see langword="true" />).</exception>
		// Token: 0x06000730 RID: 1840 RVA: 0x000206B8 File Offset: 0x0001FAB8
		public void RemoveAt(int index)
		{
			T oldValue = this._collection[index];
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
			this.OnCollectionChanged(NotifyCollectionChangedAction.Remove, index, oldValue, 0, default(T));
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x000206F4 File Offset: 0x0001FAF4
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			this.CheckReentrancy();
			base.WritePreamble();
			T t = this._collection[index];
			base.OnFreezablePropertyChanged(t, null);
			this._collection.RemoveAt(index);
			this._version += 1U;
		}

		/// <summary>Obtém ou define o elemento no índice especificado.</summary>
		/// <param name="index">O índice com base em zero do elemento a ser obtido ou definido.</param>
		/// <returns>O elemento no índice especificado.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que 0.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.FreezableCollection`1.Count" />.</exception>
		/// <exception cref="T:System.ArgumentException">O elemento especificado é <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">Tentativa de definir um item na coleção quando a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dele é <see langword="true" />).</exception>
		// Token: 0x170000D9 RID: 217
		public T this[int index]
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
				this.CheckReentrancy();
				base.WritePreamble();
				T t = this._collection[index];
				bool flag = t != value;
				if (flag)
				{
					base.OnFreezablePropertyChanged(t, value);
					this._collection[index] = value;
				}
				this._version += 1U;
				base.WritePostscript();
				if (flag)
				{
					this.OnCollectionChanged(NotifyCollectionChangedAction.Replace, index, t, index, value);
				}
			}
		}

		/// <summary>Obtém o número de elementos contidos nesse <see cref="T:System.Windows.FreezableCollection`1" />.</summary>
		/// <returns>O número de elementos contidos nesse <see cref="T:System.Windows.FreezableCollection`1" />.</returns>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x000207F8 File Offset: 0x0001FBF8
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Copia todo o <see cref="T:System.Windows.FreezableCollection`1" /> para uma matriz unidimensional compatível, iniciando no índice especificado da matriz de destino.</summary>
		/// <param name="array">A matriz unidimensional que é o destino dos elementos copiados de <see cref="T:System.Windows.FreezableCollection`1" />.</param>
		/// <param name="index">O índice com base em zero em <paramref name="array" /> no qual a cópia começa.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que 0.</exception>
		/// <exception cref="T:System.ArgumentException">O número de elementos no <see cref="T:System.Windows.FreezableCollection`1" /> de origem é maior que o espaço disponível do índice até o final do <paramref name="array" /> de destino.</exception>
		// Token: 0x06000735 RID: 1845 RVA: 0x00020818 File Offset: 0x0001FC18
		public void CopyTo(T[] array, int index)
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

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x00020868 File Offset: 0x0001FC68
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Retorna um enumerador para todo o <see cref="T:System.Windows.FreezableCollection`1" />.</summary>
		/// <returns>Um enumerador para todo o <see cref="T:System.Windows.FreezableCollection`1" />.</returns>
		// Token: 0x06000737 RID: 1847 RVA: 0x00020884 File Offset: 0x0001FC84
		public FreezableCollection<T>.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new FreezableCollection<T>.Enumerator(this);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x000208A0 File Offset: 0x0001FCA0
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.FreezableCollection`1" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x000208B8 File Offset: 0x0001FCB8
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<T>)this).IsReadOnly;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.FreezableCollection`1" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x000208CC File Offset: 0x0001FCCC
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
		// Token: 0x170000DE RID: 222
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
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.FreezableCollection`1" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x0600073D RID: 1853 RVA: 0x00020920 File Offset: 0x0001FD20
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.FreezableCollection`1" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.FreezableCollection`1" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600073E RID: 1854 RVA: 0x0002093C File Offset: 0x0001FD3C
		bool IList.Contains(object value)
		{
			return this.Contains(value as T);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.FreezableCollection`1" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x0600073F RID: 1855 RVA: 0x0002095C File Offset: 0x0001FD5C
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as T);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.FreezableCollection`1" />.</param>
		// Token: 0x06000740 RID: 1856 RVA: 0x0002097C File Offset: 0x0001FD7C
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.FreezableCollection`1" />.</param>
		// Token: 0x06000741 RID: 1857 RVA: 0x00020998 File Offset: 0x0001FD98
		void IList.Remove(object value)
		{
			this.Remove(value as T);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.FreezableCollection`1" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x06000742 RID: 1858 RVA: 0x000209B8 File Offset: 0x0001FDB8
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
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.FreezableCollection`1" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00020A90 File Offset: 0x0001FE90
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.FreezableCollection`1" />.</returns>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x00020AB8 File Offset: 0x0001FEB8
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
		// Token: 0x06000745 RID: 1861 RVA: 0x00020ACC File Offset: 0x0001FECC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desses membros, consulte <see cref="E:System.Collections.Specialized.INotifyCollectionChanged.CollectionChanged" />.</summary>
		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06000746 RID: 1862 RVA: 0x00020AE4 File Offset: 0x0001FEE4
		// (remove) Token: 0x06000747 RID: 1863 RVA: 0x00020AF8 File Offset: 0x0001FEF8
		event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
		{
			add
			{
				this.CollectionChanged += value;
			}
			remove
			{
				this.CollectionChanged -= value;
			}
		}

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06000748 RID: 1864 RVA: 0x00020B0C File Offset: 0x0001FF0C
		// (remove) Token: 0x06000749 RID: 1865 RVA: 0x00020B44 File Offset: 0x0001FF44
		private event NotifyCollectionChangedEventHandler CollectionChanged;

		// Token: 0x0600074A RID: 1866 RVA: 0x00020B7C File Offset: 0x0001FF7C
		private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			if (this.CollectionChanged != null)
			{
				using (this.BlockReentrancy())
				{
					this.CollectionChanged(this, e);
				}
			}
		}

		/// <summary>Para obter uma descrição desses membros, consulte <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged" />.</summary>
		// Token: 0x1400005A RID: 90
		// (add) Token: 0x0600074B RID: 1867 RVA: 0x00020BD0 File Offset: 0x0001FFD0
		// (remove) Token: 0x0600074C RID: 1868 RVA: 0x00020BE4 File Offset: 0x0001FFE4
		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			add
			{
				this.PrivatePropertyChanged += value;
			}
			remove
			{
				this.PrivatePropertyChanged -= value;
			}
		}

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x0600074D RID: 1869 RVA: 0x00020BF8 File Offset: 0x0001FFF8
		// (remove) Token: 0x0600074E RID: 1870 RVA: 0x00020C30 File Offset: 0x00020030
		private event PropertyChangedEventHandler PrivatePropertyChanged;

		// Token: 0x0600074F RID: 1871 RVA: 0x00020C68 File Offset: 0x00020068
		private void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (this.PrivatePropertyChanged != null)
			{
				this.PrivatePropertyChanged(this, e);
			}
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00020C8C File Offset: 0x0002008C
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

		// Token: 0x06000751 RID: 1873 RVA: 0x00020CD8 File Offset: 0x000200D8
		private T Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is T))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"T"
				}));
			}
			return (T)((object)value);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00020D3C File Offset: 0x0002013C
		private int GetCount(IEnumerable<T> enumerable)
		{
			ICollection collection = enumerable as ICollection;
			if (collection != null)
			{
				return collection.Count;
			}
			ICollection<T> collection2 = enumerable as ICollection<!0>;
			if (collection2 != null)
			{
				return collection2.Count;
			}
			return -1;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00020D6C File Offset: 0x0002016C
		private int AddHelper(T value)
		{
			this.CheckReentrancy();
			int num = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			this.OnCollectionChanged(NotifyCollectionChangedAction.Add, 0, default(T), num - 1, value);
			return num;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00020DA4 File Offset: 0x000201A4
		internal int AddWithoutFiringPublicEvents(T value)
		{
			if (value == null)
			{
				throw new ArgumentException(SR.Get("Collection_NoNull"));
			}
			base.WritePreamble();
			base.OnFreezablePropertyChanged(null, value);
			this._collection.Add(value);
			this._version += 1U;
			return this._collection.Count;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00020E04 File Offset: 0x00020204
		private void OnCollectionChanged(NotifyCollectionChangedAction action, int oldIndex, T oldValue, int newIndex, T newValue)
		{
			if (this.PrivatePropertyChanged == null && this.CollectionChanged == null)
			{
				return;
			}
			using (this.BlockReentrancy())
			{
				if (this.PrivatePropertyChanged != null)
				{
					if (action != NotifyCollectionChangedAction.Replace && action != NotifyCollectionChangedAction.Move)
					{
						this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
					}
					this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
				}
				if (this.CollectionChanged != null)
				{
					NotifyCollectionChangedEventArgs e;
					switch (action)
					{
					case NotifyCollectionChangedAction.Add:
						e = new NotifyCollectionChangedEventArgs(action, newValue, newIndex);
						goto IL_C2;
					case NotifyCollectionChangedAction.Remove:
						e = new NotifyCollectionChangedEventArgs(action, oldValue, oldIndex);
						goto IL_C2;
					case NotifyCollectionChangedAction.Replace:
						e = new NotifyCollectionChangedEventArgs(action, newValue, oldValue, newIndex);
						goto IL_C2;
					case NotifyCollectionChangedAction.Reset:
						e = new NotifyCollectionChangedEventArgs(action);
						goto IL_C2;
					}
					throw new InvalidOperationException(SR.Get("Freezable_UnexpectedChange"));
					IL_C2:
					this.OnCollectionChanged(e);
				}
			}
		}

		/// <summary>Cria uma nova instância do <see cref="T:System.Windows.FreezableCollection`1" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06000756 RID: 1878 RVA: 0x00020F04 File Offset: 0x00020304
		protected override Freezable CreateInstanceCore()
		{
			return new FreezableCollection<T>();
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00020F18 File Offset: 0x00020318
		private void CloneCommon(FreezableCollection<T> source, FreezableCollection<T>.CloneCommonType cloneType)
		{
			int count = source._collection.Count;
			this._collection = new List<T>(count);
			for (int i = 0; i < count; i++)
			{
				T t = source._collection[i];
				Freezable freezable = t as Freezable;
				if (freezable != null)
				{
					switch (cloneType)
					{
					case FreezableCollection<T>.CloneCommonType.Clone:
						t = (freezable.Clone() as T);
						break;
					case FreezableCollection<T>.CloneCommonType.CloneCurrentValue:
						t = (freezable.CloneCurrentValue() as T);
						break;
					case FreezableCollection<T>.CloneCommonType.GetAsFrozen:
						t = (freezable.GetAsFrozen() as T);
						break;
					case FreezableCollection<T>.CloneCommonType.GetCurrentValueAsFrozen:
						t = (freezable.GetCurrentValueAsFrozen() as T);
						break;
					default:
						Invariant.Assert(false, "Invalid CloneCommonType encountered.");
						break;
					}
					if (t == null)
					{
						throw new InvalidOperationException(SR.Get("Freezable_CloneInvalidType", new object[]
						{
							typeof(T).Name
						}));
					}
				}
				base.OnFreezablePropertyChanged(null, t);
				this._collection.Add(t);
			}
		}

		/// <summary>Torna essa instância um clone (cópia em profundidade) do <see cref="T:System.Windows.FreezableCollection`1" /> especificado usando valores de propriedade base (não animada).</summary>
		/// <param name="source">O <see cref="T:System.Windows.FreezableCollection`1" /> para cópia.</param>
		// Token: 0x06000758 RID: 1880 RVA: 0x00021028 File Offset: 0x00020428
		protected override void CloneCore(Freezable source)
		{
			base.CloneCore(source);
			FreezableCollection<T> source2 = (FreezableCollection<T>)source;
			this.CloneCommon(source2, FreezableCollection<T>.CloneCommonType.Clone);
		}

		/// <summary>Torna essa instância um clone modificável (cópia em profundidade) do <see cref="T:System.Windows.FreezableCollection`1" /> especificado usando os valores de propriedade atuais.</summary>
		/// <param name="source">O <see cref="T:System.Windows.FreezableCollection`1" /> a ser clonado.</param>
		// Token: 0x06000759 RID: 1881 RVA: 0x0002104C File Offset: 0x0002044C
		protected override void CloneCurrentValueCore(Freezable source)
		{
			base.CloneCurrentValueCore(source);
			FreezableCollection<T> source2 = (FreezableCollection<T>)source;
			this.CloneCommon(source2, FreezableCollection<T>.CloneCommonType.CloneCurrentValue);
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.FreezableCollection`1" /> especificado usando valores de propriedade base (não animada).</summary>
		/// <param name="source">O <see cref="T:System.Windows.FreezableCollection`1" /> para cópia.</param>
		// Token: 0x0600075A RID: 1882 RVA: 0x00021070 File Offset: 0x00020470
		protected override void GetAsFrozenCore(Freezable source)
		{
			base.GetAsFrozenCore(source);
			FreezableCollection<T> source2 = (FreezableCollection<T>)source;
			this.CloneCommon(source2, FreezableCollection<T>.CloneCommonType.GetAsFrozen);
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Freezable" /> especificado. Se esse objeto tiver propriedades de dependência animadas, seus valores animados atuais serão copiados.</summary>
		/// <param name="source">O <see cref="T:System.Windows.FreezableCollection`1" /> para cópia.</param>
		// Token: 0x0600075B RID: 1883 RVA: 0x00021094 File Offset: 0x00020494
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			base.GetCurrentValueAsFrozenCore(source);
			FreezableCollection<T> source2 = (FreezableCollection<T>)source;
			this.CloneCommon(source2, FreezableCollection<T>.CloneCommonType.GetCurrentValueAsFrozen);
		}

		/// <summary>Faz com que este objeto <see cref="T:System.Windows.FreezableCollection`1" /> não seja modificável ou determina se ele pode se tornar não modificável.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> se o <see cref="T:System.Windows.FreezableCollection`1" /> deve retornar apenas se ele pode ser congelado. <see langword="false" /> se a instância <see cref="T:System.Windows.FreezableCollection`1" />, na verdade, deve congelar a si mesma quando este método é chamado.</param>
		/// <returns>Se <paramref name="isChecking" /> for <see langword="true" />, esse método retorna <see langword="true" /> se este <see cref="T:System.Windows.FreezableCollection`1" /> puder se tornar não modificável ou <see langword="false" />, se ele não puder se tornar não modificável.  
		/// Se <paramref name="isChecking" /> for <see langword="false" />, este método retornará <see langword="true" /> se o <see cref="T:System.Windows.FreezableCollection`1" /> especificado agora não for modificável ou então <see langword="false" />, se não tiver sido possível torná-lo não modificável, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x0600075C RID: 1884 RVA: 0x000210B8 File Offset: 0x000204B8
		protected override bool FreezeCore(bool isChecking)
		{
			bool flag = base.FreezeCore(isChecking);
			int count = this._collection.Count;
			int num = 0;
			while (num < count && flag)
			{
				T t = this._collection[num];
				Freezable freezable = t as Freezable;
				if (freezable != null)
				{
					flag &= Freezable.Freeze(freezable, isChecking);
				}
				else
				{
					flag &= (t.Dispatcher == null);
				}
				num++;
			}
			return flag;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00021128 File Offset: 0x00020528
		private IDisposable BlockReentrancy()
		{
			this._monitor.Enter();
			return this._monitor;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00021148 File Offset: 0x00020548
		private void CheckReentrancy()
		{
			if (this._monitor.Busy)
			{
				throw new InvalidOperationException(SR.Get("Freezable_Reentrant"));
			}
		}

		// Token: 0x040005B2 RID: 1458
		internal List<T> _collection;

		// Token: 0x040005B3 RID: 1459
		internal uint _version;

		// Token: 0x040005B4 RID: 1460
		private const string CountPropertyName = "Count";

		// Token: 0x040005B5 RID: 1461
		private const string IndexerPropertyName = "Item[]";

		// Token: 0x040005B6 RID: 1462
		private FreezableCollection<T>.SimpleMonitor _monitor = new FreezableCollection<T>.SimpleMonitor();

		// Token: 0x020007F7 RID: 2039
		private enum CloneCommonType
		{
			// Token: 0x04002690 RID: 9872
			Clone,
			// Token: 0x04002691 RID: 9873
			CloneCurrentValue,
			// Token: 0x04002692 RID: 9874
			GetAsFrozen,
			// Token: 0x04002693 RID: 9875
			GetCurrentValueAsFrozen
		}

		/// <summary>Enumera os membros de um <see cref="T:System.Windows.FreezableCollection`1" />.</summary>
		/// <typeparam name="T" />
		// Token: 0x020007F8 RID: 2040
		public struct Enumerator : IEnumerator, IEnumerator<T>, IDisposable
		{
			// Token: 0x060055BD RID: 21949 RVA: 0x00161148 File Offset: 0x00160548
			internal Enumerator(FreezableCollection<T> list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = default(T);
			}

			/// <summary>Este membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x060055BE RID: 21950 RVA: 0x0016117C File Offset: 0x0016057C
			void IDisposable.Dispose()
			{
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador avançou com êxito para o próximo elemento na coleção; caso contrário, <see langword="false" />.</returns>
			// Token: 0x060055BF RID: 21951 RVA: 0x0016118C File Offset: 0x0016058C
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					List<T> collection = this._list._collection;
					int index = this._index + 1;
					this._index = index;
					this._current = collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Redefine o enumerador como sua posição inicial.</summary>
			// Token: 0x060055C0 RID: 21952 RVA: 0x00161220 File Offset: 0x00160620
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
			// Token: 0x17001195 RID: 4501
			// (get) Token: 0x060055C1 RID: 21953 RVA: 0x00161264 File Offset: 0x00160664
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém um valor que representa o elemento atual na coleção.</summary>
			/// <returns>O elemento atual na coleção.</returns>
			// Token: 0x17001196 RID: 4502
			// (get) Token: 0x060055C2 RID: 21954 RVA: 0x0016127C File Offset: 0x0016067C
			public T Current
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

			// Token: 0x04002694 RID: 9876
			private T _current;

			// Token: 0x04002695 RID: 9877
			private FreezableCollection<T> _list;

			// Token: 0x04002696 RID: 9878
			private uint _version;

			// Token: 0x04002697 RID: 9879
			private int _index;
		}

		// Token: 0x020007F9 RID: 2041
		private class SimpleMonitor : IDisposable
		{
			// Token: 0x060055C3 RID: 21955 RVA: 0x001612C4 File Offset: 0x001606C4
			public void Enter()
			{
				this._busyCount++;
			}

			// Token: 0x060055C4 RID: 21956 RVA: 0x001612E0 File Offset: 0x001606E0
			public void Dispose()
			{
				this._busyCount--;
				GC.SuppressFinalize(this);
			}

			// Token: 0x17001197 RID: 4503
			// (get) Token: 0x060055C5 RID: 21957 RVA: 0x00161304 File Offset: 0x00160704
			public bool Busy
			{
				get
				{
					return this._busyCount > 0;
				}
			}

			// Token: 0x04002698 RID: 9880
			private int _busyCount;
		}
	}
}
