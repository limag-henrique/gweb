using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Animation;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.PathSegment" /> que podem ser acessados individualmente por índice.</summary>
	// Token: 0x020003C5 RID: 965
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public sealed class PathSegmentCollection : Animatable, IList, ICollection, IEnumerable, IList<PathSegment>, ICollection<PathSegment>, IEnumerable<PathSegment>
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.PathSegmentCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060025C7 RID: 9671 RVA: 0x00096F08 File Offset: 0x00096308
		public new PathSegmentCollection Clone()
		{
			return (PathSegmentCollection)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.PathSegmentCollection" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060025C8 RID: 9672 RVA: 0x00096F20 File Offset: 0x00096320
		public new PathSegmentCollection CloneCurrentValue()
		{
			return (PathSegmentCollection)base.CloneCurrentValue();
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.PathSegment" /> ao final da coleção.</summary>
		/// <param name="value">O segmento a ser adicionado à coleção.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.PathSegmentCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.PathSegmentCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x060025C9 RID: 9673 RVA: 0x00096F38 File Offset: 0x00096338
		public void Add(PathSegment value)
		{
			this.AddHelper(value);
		}

		/// <summary>Limpa a coleção de todos os segmentos e redefine <see cref="P:System.Windows.Media.PathSegmentCollection.Count" /> como zero.</summary>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.PathSegmentCollection" /> é somente leitura.</exception>
		// Token: 0x060025CA RID: 9674 RVA: 0x00096F50 File Offset: 0x00096350
		public void Clear()
		{
			base.WritePreamble();
			for (int i = this._collection.Count - 1; i >= 0; i--)
			{
				base.OnFreezablePropertyChanged(this._collection[i], null);
			}
			this._collection.Clear();
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Retorna um <see cref="T:System.Boolean" /> que indica se o <see cref="T:System.Windows.Media.PathSegment" /> especificado está contido dentro da coleção.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.PathSegment" /> a ser pesquisado.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.PathSegment" /> especificado estiver contido na coleção; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060025CB RID: 9675 RVA: 0x00096FB0 File Offset: 0x000963B0
		public bool Contains(PathSegment value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Retorna o índice da primeira ocorrência do <see cref="T:System.Windows.Media.PathSegment" /> especificado.</summary>
		/// <param name="value">O item a ser procurado.</param>
		/// <returns>O índice do <see cref="T:System.Windows.Media.PathSegment" /> especificado.</returns>
		// Token: 0x060025CC RID: 9676 RVA: 0x00096FD0 File Offset: 0x000963D0
		public int IndexOf(PathSegment value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.PathSegment" /> nesta <see cref="T:System.Windows.Media.PathSegmentCollection" /> no índice especificado.</summary>
		/// <param name="index">O índice no qual inserir o <paramref name="value" />, o <see cref="T:System.Windows.Media.PathSegment" /> especificado.</param>
		/// <param name="value">O item a ser inserido.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> não é um índice válido no <see cref="T:System.Windows.Media.PathSegmentCollection" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.PathSegmentCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.PathSegmentCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x060025CD RID: 9677 RVA: 0x00096FF0 File Offset: 0x000963F0
		public void Insert(int index, PathSegment value)
		{
			if (value == null)
			{
				throw new ArgumentException(SR.Get("Collection_NoNull"));
			}
			base.WritePreamble();
			base.OnFreezablePropertyChanged(null, value);
			this._collection.Insert(index, value);
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Remove a primeira ocorrência do <see cref="T:System.Windows.Media.PathSegment" /> especificado dessa <see cref="T:System.Windows.Media.PathSegmentCollection" />.</summary>
		/// <param name="value">O item a ser removido desta coleção.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.PathSegment" /> especificado puder ser removido da coleção; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.PathSegmentCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.PathSegmentCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x060025CE RID: 9678 RVA: 0x00097040 File Offset: 0x00096440
		public bool Remove(PathSegment value)
		{
			base.WritePreamble();
			int num = this.IndexOf(value);
			if (num >= 0)
			{
				PathSegment oldValue = this._collection[num];
				base.OnFreezablePropertyChanged(oldValue, null);
				this._collection.RemoveAt(num);
				this._version += 1U;
				base.WritePostscript();
				return true;
			}
			return false;
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.PathSegment" /> no índice especificado dessa <see cref="T:System.Windows.Media.PathSegmentCollection" />.</summary>
		/// <param name="index">O índice do item a ser removido.</param>
		// Token: 0x060025CF RID: 9679 RVA: 0x00097098 File Offset: 0x00096498
		public void RemoveAt(int index)
		{
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x000970B4 File Offset: 0x000964B4
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			base.WritePreamble();
			PathSegment oldValue = this._collection[index];
			base.OnFreezablePropertyChanged(oldValue, null);
			this._collection.RemoveAt(index);
			this._version += 1U;
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.PathSegment" /> no índice de base zero especificado.</summary>
		/// <param name="index">O índice baseado em zero do objeto <see cref="T:System.Windows.Media.PathSegment" /> a ser obtido ou definido.</param>
		/// <returns>O item no índice especificado.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> não é um índice válido no <see cref="T:System.Windows.Media.PathSegmentCollection" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.PathSegmentCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.PathSegmentCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x1700075B RID: 1883
		public PathSegment this[int index]
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
					PathSegment oldValue = this._collection[index];
					base.OnFreezablePropertyChanged(oldValue, value);
					this._collection[index] = value;
				}
				this._version += 1U;
				base.WritePostscript();
			}
		}

		/// <summary>Obtém o número de segmentos de caminho contidos no <see cref="T:System.Windows.Media.PathSegmentCollection" />.</summary>
		/// <returns>O número de segmentos de caminho contidos no <see cref="T:System.Windows.Media.PathSegmentCollection" />.</returns>
		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x00097184 File Offset: 0x00096584
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Copia todo o <see cref="T:System.Windows.Media.PathSegmentCollection" /> para uma matriz <see cref="T:System.Windows.Media.PathSegment" /> unidimensional, iniciando no índice especificado da matriz de destino.</summary>
		/// <param name="array">A matriz para a qual os itens da coleção devem ser copiados.</param>
		/// <param name="index">O índice da <paramref name="array" /> na qual começar a copiar o conteúdo do <see cref="T:System.Windows.Media.PathSegmentCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> é multidimensional.  
		///
		/// ou - 
		/// O número de itens na origem <see cref="T:System.Windows.Media.PathSegmentCollection" /> é maior do que o espaço disponível de <paramref name="index" /> até o final do <paramref name="array" /> de destino.</exception>
		// Token: 0x060025D4 RID: 9684 RVA: 0x000971A4 File Offset: 0x000965A4
		public void CopyTo(PathSegment[] array, int index)
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

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x000971F4 File Offset: 0x000965F4
		bool ICollection<PathSegment>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Retorna um enumerador que pode iterar pela coleção.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.PathSegmentCollection.Enumerator" /> que pode iterar pela coleção.</returns>
		// Token: 0x060025D6 RID: 9686 RVA: 0x00097210 File Offset: 0x00096610
		public PathSegmentCollection.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new PathSegmentCollection.Enumerator(this);
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x0009722C File Offset: 0x0009662C
		IEnumerator<PathSegment> IEnumerable<PathSegment>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.PathSegmentCollection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060025D8 RID: 9688 RVA: 0x00097244 File Offset: 0x00096644
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<PathSegment>)this).IsReadOnly;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.PathSegmentCollection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060025D9 RID: 9689 RVA: 0x00097258 File Offset: 0x00096658
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
		// Token: 0x17000760 RID: 1888
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
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.PathSegmentCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x060025DC RID: 9692 RVA: 0x000972A4 File Offset: 0x000966A4
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.PathSegmentCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.PathSegmentCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060025DD RID: 9693 RVA: 0x000972C0 File Offset: 0x000966C0
		bool IList.Contains(object value)
		{
			return this.Contains(value as PathSegment);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.PathSegmentCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x060025DE RID: 9694 RVA: 0x000972DC File Offset: 0x000966DC
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as PathSegment);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.PathSegmentCollection" />.</param>
		// Token: 0x060025DF RID: 9695 RVA: 0x000972F8 File Offset: 0x000966F8
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.PathSegmentCollection" />.</param>
		// Token: 0x060025E0 RID: 9696 RVA: 0x00097314 File Offset: 0x00096714
		void IList.Remove(object value)
		{
			this.Remove(value as PathSegment);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.PathSegmentCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x060025E1 RID: 9697 RVA: 0x00097330 File Offset: 0x00096730
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
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.PathSegmentCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060025E2 RID: 9698 RVA: 0x00097404 File Offset: 0x00096804
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.PathSegmentCollection" />.</returns>
		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060025E3 RID: 9699 RVA: 0x0009742C File Offset: 0x0009682C
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
		// Token: 0x060025E4 RID: 9700 RVA: 0x00097440 File Offset: 0x00096840
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060025E5 RID: 9701 RVA: 0x00097458 File Offset: 0x00096858
		internal static PathSegmentCollection Empty
		{
			get
			{
				if (PathSegmentCollection.s_empty == null)
				{
					PathSegmentCollection pathSegmentCollection = new PathSegmentCollection();
					pathSegmentCollection.Freeze();
					PathSegmentCollection.s_empty = pathSegmentCollection;
				}
				return PathSegmentCollection.s_empty;
			}
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x00097484 File Offset: 0x00096884
		internal PathSegment Internal_GetItem(int i)
		{
			return this._collection[i];
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x000974A0 File Offset: 0x000968A0
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

		// Token: 0x060025E8 RID: 9704 RVA: 0x000974E8 File Offset: 0x000968E8
		private PathSegment Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is PathSegment))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"PathSegment"
				}));
			}
			return (PathSegment)value;
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x0009754C File Offset: 0x0009694C
		private int AddHelper(PathSegment value)
		{
			int result = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			return result;
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x00097568 File Offset: 0x00096968
		internal int AddWithoutFiringPublicEvents(PathSegment value)
		{
			if (value == null)
			{
				throw new ArgumentException(SR.Get("Collection_NoNull"));
			}
			base.WritePreamble();
			base.OnFreezablePropertyChanged(null, value);
			int result = this._collection.Add(value);
			this._version += 1U;
			return result;
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x000975B8 File Offset: 0x000969B8
		protected override Freezable CreateInstanceCore()
		{
			return new PathSegmentCollection();
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x000975CC File Offset: 0x000969CC
		protected override void CloneCore(Freezable source)
		{
			PathSegmentCollection pathSegmentCollection = (PathSegmentCollection)source;
			base.CloneCore(source);
			int count = pathSegmentCollection._collection.Count;
			this._collection = new FrugalStructList<PathSegment>(count);
			for (int i = 0; i < count; i++)
			{
				PathSegment pathSegment = pathSegmentCollection._collection[i].Clone();
				base.OnFreezablePropertyChanged(null, pathSegment);
				this._collection.Add(pathSegment);
			}
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x00097634 File Offset: 0x00096A34
		protected override void CloneCurrentValueCore(Freezable source)
		{
			PathSegmentCollection pathSegmentCollection = (PathSegmentCollection)source;
			base.CloneCurrentValueCore(source);
			int count = pathSegmentCollection._collection.Count;
			this._collection = new FrugalStructList<PathSegment>(count);
			for (int i = 0; i < count; i++)
			{
				PathSegment pathSegment = pathSegmentCollection._collection[i].CloneCurrentValue();
				base.OnFreezablePropertyChanged(null, pathSegment);
				this._collection.Add(pathSegment);
			}
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x0009769C File Offset: 0x00096A9C
		protected override void GetAsFrozenCore(Freezable source)
		{
			PathSegmentCollection pathSegmentCollection = (PathSegmentCollection)source;
			base.GetAsFrozenCore(source);
			int count = pathSegmentCollection._collection.Count;
			this._collection = new FrugalStructList<PathSegment>(count);
			for (int i = 0; i < count; i++)
			{
				PathSegment pathSegment = (PathSegment)pathSegmentCollection._collection[i].GetAsFrozen();
				base.OnFreezablePropertyChanged(null, pathSegment);
				this._collection.Add(pathSegment);
			}
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x00097708 File Offset: 0x00096B08
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			PathSegmentCollection pathSegmentCollection = (PathSegmentCollection)source;
			base.GetCurrentValueAsFrozenCore(source);
			int count = pathSegmentCollection._collection.Count;
			this._collection = new FrugalStructList<PathSegment>(count);
			for (int i = 0; i < count; i++)
			{
				PathSegment pathSegment = (PathSegment)pathSegmentCollection._collection[i].GetCurrentValueAsFrozen();
				base.OnFreezablePropertyChanged(null, pathSegment);
				this._collection.Add(pathSegment);
			}
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x00097774 File Offset: 0x00096B74
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

		/// <summary>Inicializa uma nova instância de um <see cref="T:System.Windows.Media.PathSegmentCollection" />.</summary>
		// Token: 0x060025F1 RID: 9713 RVA: 0x000977BC File Offset: 0x00096BBC
		public PathSegmentCollection()
		{
			this._collection = default(FrugalStructList<PathSegment>);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.PathSegmentCollection" /> com a capacidade especificada ou o número de objetos <see cref="T:System.Windows.Media.PathSegment" /> que a coleção é capaz de armazenar inicialmente.</summary>
		/// <param name="capacity">O número de objetos <see cref="T:System.Windows.Media.PathSegment" /> que a coleção é capaz de armazenar inicialmente.</param>
		// Token: 0x060025F2 RID: 9714 RVA: 0x000977DC File Offset: 0x00096BDC
		public PathSegmentCollection(int capacity)
		{
			this._collection = new FrugalStructList<PathSegment>(capacity);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.PathSegmentCollection" /> com a coleção especificada de objetos <see cref="T:System.Windows.Media.PathSegment" />.</summary>
		/// <param name="collection">A coleção de objetos <see cref="T:System.Windows.Media.PathSegment" /> que compõem o <see cref="T:System.Windows.Media.PathSegmentCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> é <see langword="null" />.</exception>
		// Token: 0x060025F3 RID: 9715 RVA: 0x000977FC File Offset: 0x00096BFC
		public PathSegmentCollection(IEnumerable<PathSegment> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				bool flag = true;
				ICollection<PathSegment> collection2 = collection as ICollection<PathSegment>;
				if (collection2 != null)
				{
					this._collection = new FrugalStructList<PathSegment>(collection2);
				}
				else
				{
					ICollection collection3 = collection as ICollection;
					if (collection3 != null)
					{
						this._collection = new FrugalStructList<PathSegment>(collection3);
					}
					else
					{
						this._collection = default(FrugalStructList<PathSegment>);
						foreach (PathSegment pathSegment in collection)
						{
							if (pathSegment == null)
							{
								throw new ArgumentException(SR.Get("Collection_NoNull"));
							}
							PathSegment pathSegment2 = pathSegment;
							base.OnFreezablePropertyChanged(null, pathSegment2);
							this._collection.Add(pathSegment2);
						}
						flag = false;
					}
				}
				if (flag)
				{
					foreach (PathSegment pathSegment3 in collection)
					{
						if (pathSegment3 == null)
						{
							throw new ArgumentException(SR.Get("Collection_NoNull"));
						}
						base.OnFreezablePropertyChanged(null, pathSegment3);
					}
				}
				base.WritePostscript();
				return;
			}
			throw new ArgumentNullException("collection");
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x00097940 File Offset: 0x00096D40
		internal bool CanSerializeToString()
		{
			bool result = true;
			for (int i = 0; i < this._collection.Count; i++)
			{
				if (!this._collection[i].IsStroked)
				{
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x00097980 File Offset: 0x00096D80
		internal string ConvertToString(string format, IFormatProvider provider)
		{
			if (this._collection.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this._collection.Count; i++)
			{
				stringBuilder.Append(this._collection[i].ConvertToString(format, provider));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040011A6 RID: 4518
		private static PathSegmentCollection s_empty;

		// Token: 0x040011A7 RID: 4519
		internal FrugalStructList<PathSegment> _collection;

		// Token: 0x040011A8 RID: 4520
		internal uint _version;

		/// <summary>Dá suporte a uma iteração simples em um <see cref="T:System.Windows.Media.PathSegmentCollection" />.</summary>
		// Token: 0x0200087A RID: 2170
		public struct Enumerator : IEnumerator, IEnumerator<PathSegment>, IDisposable
		{
			// Token: 0x060057A2 RID: 22434 RVA: 0x00166434 File Offset: 0x00165834
			internal Enumerator(PathSegmentCollection list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = null;
			}

			/// <summary>Este tipo ou membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x060057A3 RID: 22435 RVA: 0x00166464 File Offset: 0x00165864
			void IDisposable.Dispose()
			{
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador avançou com êxito para o próximo elemento; <see langword="false" /> se o enumerador passou o final da coleção.</returns>
			/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Windows.Media.PathSegmentCollection" /> foi modificada depois da criação do enumerador.</exception>
			// Token: 0x060057A4 RID: 22436 RVA: 0x00166474 File Offset: 0x00165874
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					PathSegmentCollection list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._current = list._collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Redefine o enumerador para sua posição inicial, que é antes do primeiro item no <see cref="T:System.Windows.Media.PathSegmentCollection" />.</summary>
			/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Windows.Media.PathSegmentCollection" /> foi modificada depois da criação do enumerador.</exception>
			// Token: 0x060057A5 RID: 22437 RVA: 0x00166508 File Offset: 0x00165908
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
			// Token: 0x17001217 RID: 4631
			// (get) Token: 0x060057A6 RID: 22438 RVA: 0x0016654C File Offset: 0x0016594C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém o item atual no <see cref="T:System.Windows.Media.PathSegmentCollection" />.</summary>
			/// <returns>O item atual no <see cref="T:System.Windows.Media.PathSegmentCollection" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Windows.Media.PathSegmentCollection" /> foi modificada depois da criação do enumerador.</exception>
			// Token: 0x17001218 RID: 4632
			// (get) Token: 0x060057A7 RID: 22439 RVA: 0x00166560 File Offset: 0x00165960
			public PathSegment Current
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

			// Token: 0x0400289E RID: 10398
			private PathSegment _current;

			// Token: 0x0400289F RID: 10399
			private PathSegmentCollection _list;

			// Token: 0x040028A0 RID: 10400
			private uint _version;

			// Token: 0x040028A1 RID: 10401
			private int _index;
		}
	}
}
