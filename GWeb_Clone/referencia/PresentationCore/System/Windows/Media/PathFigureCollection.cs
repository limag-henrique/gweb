using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media.Converters;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.PathFigure" /> que reunidos formam a geometria de uma <see cref="T:System.Windows.Media.PathGeometry" />.</summary>
	// Token: 0x020003C1 RID: 961
	[ValueSerializer(typeof(PathFigureCollectionValueSerializer))]
	[TypeConverter(typeof(PathFigureCollectionConverter))]
	public sealed class PathFigureCollection : Animatable, IFormattable, IList, ICollection, IEnumerable, IList<PathFigure>, ICollection<PathFigure>, IEnumerable<PathFigure>
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.PathFigureCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002559 RID: 9561 RVA: 0x0009532C File Offset: 0x0009472C
		public new PathFigureCollection Clone()
		{
			return (PathFigureCollection)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.PathFigureCollection" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x0600255A RID: 9562 RVA: 0x00095344 File Offset: 0x00094744
		public new PathFigureCollection CloneCurrentValue()
		{
			return (PathFigureCollection)base.CloneCurrentValue();
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.PathFigure" /> ao final da coleção.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.PathFigure" /> a adicionar à coleção.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.PathFigureCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.PathFigureCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x0600255B RID: 9563 RVA: 0x0009535C File Offset: 0x0009475C
		public void Add(PathFigure value)
		{
			this.AddHelper(value);
		}

		/// <summary>Remove todos os itens do <see cref="T:System.Windows.Media.PathFigureCollection" />.</summary>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.PathFigureCollection" /> é somente leitura.</exception>
		// Token: 0x0600255C RID: 9564 RVA: 0x00095374 File Offset: 0x00094774
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

		/// <summary>Determina se a coleção contém o <see cref="T:System.Windows.Media.PathFigure" /> especificado.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.PathFigure" /> que está sendo consultado.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> estiver no <see cref="T:System.Windows.Media.PathFigureCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600255D RID: 9565 RVA: 0x000953D4 File Offset: 0x000947D4
		public bool Contains(PathFigure value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.PathFigure" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.PathFigure" /> a ser localizado na coleção.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na <see cref="T:System.Windows.Media.PathFigureCollection" />, caso contrário, -1.</returns>
		// Token: 0x0600255E RID: 9566 RVA: 0x000953F4 File Offset: 0x000947F4
		public int IndexOf(PathFigure value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.PathFigure" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.PathFigure" /> é inserido.</param>
		/// <param name="value">O objeto <see cref="T:System.Windows.Media.PathFigure" /> a ser inserido na coleção.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> não é um índice válido no <see cref="T:System.Windows.Media.PathFigureCollection" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.PathFigureCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.PathFigureCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x0600255F RID: 9567 RVA: 0x00095414 File Offset: 0x00094814
		public void Insert(int index, PathFigure value)
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

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.PathFigure" /> da coleção.</summary>
		/// <param name="value">Identifica o <see cref="T:System.Windows.Media.PathFigure" /> a ser removido da coleção.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> tiver sido removido da <see cref="T:System.Windows.Media.PathFigureCollection" />, caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.PathFigureCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.PathFigureCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x06002560 RID: 9568 RVA: 0x00095464 File Offset: 0x00094864
		public bool Remove(PathFigure value)
		{
			base.WritePreamble();
			int num = this.IndexOf(value);
			if (num >= 0)
			{
				PathFigure oldValue = this._collection[num];
				base.OnFreezablePropertyChanged(oldValue, null);
				this._collection.RemoveAt(num);
				this._version += 1U;
				base.WritePostscript();
				return true;
			}
			return false;
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.PathFigure" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.PathFigure" /> a ser removido.</param>
		// Token: 0x06002561 RID: 9569 RVA: 0x000954BC File Offset: 0x000948BC
		public void RemoveAt(int index)
		{
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000954D8 File Offset: 0x000948D8
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			base.WritePreamble();
			PathFigure oldValue = this._collection[index];
			base.OnFreezablePropertyChanged(oldValue, null);
			this._collection.RemoveAt(index);
			this._version += 1U;
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.PathFigure" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.PathFigure" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.PathFigure" /> no índice especificado.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> não é um índice válido no <see cref="T:System.Windows.Media.PathFigureCollection" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.PathFigureCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.PathFigureCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x1700074C RID: 1868
		public PathFigure this[int index]
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
					PathFigure oldValue = this._collection[index];
					base.OnFreezablePropertyChanged(oldValue, value);
					this._collection[index] = value;
				}
				this._version += 1U;
				base.WritePostscript();
			}
		}

		/// <summary>Obtém o número de figuras de caminho contidas no <see cref="T:System.Windows.Media.PathFigureCollection" />.</summary>
		/// <returns>O número de figuras de caminho contidos no <see cref="T:System.Windows.Media.PathFigureCollection" />.</returns>
		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06002565 RID: 9573 RVA: 0x000955A8 File Offset: 0x000949A8
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Copia todo o <see cref="T:System.Windows.Media.PathFigureCollection" /> para uma matriz unidimensional do tipo <see cref="T:System.Windows.Media.PathFigure" />, iniciando no índice especificado da matriz de destino.</summary>
		/// <param name="array">A matriz para a qual os itens da coleção devem ser copiados.</param>
		/// <param name="index">O índice da <paramref name="array" /> na qual começar a copiar o conteúdo do <see cref="T:System.Windows.Media.PathFigureCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> é multidimensional.  
		///
		/// ou - 
		/// O número de itens na origem <see cref="T:System.Windows.Media.PathFigureCollection" /> é maior do que o espaço disponível de <paramref name="index" /> até o final do <paramref name="array" /> de destino.</exception>
		// Token: 0x06002566 RID: 9574 RVA: 0x000955C8 File Offset: 0x000949C8
		public void CopyTo(PathFigure[] array, int index)
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

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002567 RID: 9575 RVA: 0x00095618 File Offset: 0x00094A18
		bool ICollection<PathFigure>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Retorna um enumerador que pode iterar pela coleção.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.PathFigureCollection.Enumerator" /> que pode iterar pela coleção.</returns>
		// Token: 0x06002568 RID: 9576 RVA: 0x00095634 File Offset: 0x00094A34
		public PathFigureCollection.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new PathFigureCollection.Enumerator(this);
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x00095650 File Offset: 0x00094A50
		IEnumerator<PathFigure> IEnumerable<PathFigure>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.PathFigureCollection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x0600256A RID: 9578 RVA: 0x00095668 File Offset: 0x00094A68
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<PathFigure>)this).IsReadOnly;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.PathFigureCollection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x0600256B RID: 9579 RVA: 0x0009567C File Offset: 0x00094A7C
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
		// Token: 0x17000751 RID: 1873
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
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.PathFigureCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x0600256E RID: 9582 RVA: 0x000956C8 File Offset: 0x00094AC8
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.PathFigureCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.PathFigureCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600256F RID: 9583 RVA: 0x000956E4 File Offset: 0x00094AE4
		bool IList.Contains(object value)
		{
			return this.Contains(value as PathFigure);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.PathFigureCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06002570 RID: 9584 RVA: 0x00095700 File Offset: 0x00094B00
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as PathFigure);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.PathFigureCollection" />.</param>
		// Token: 0x06002571 RID: 9585 RVA: 0x0009571C File Offset: 0x00094B1C
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.PathFigureCollection" />.</param>
		// Token: 0x06002572 RID: 9586 RVA: 0x00095738 File Offset: 0x00094B38
		void IList.Remove(object value)
		{
			this.Remove(value as PathFigure);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.PathFigureCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x06002573 RID: 9587 RVA: 0x00095754 File Offset: 0x00094B54
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
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.PathFigureCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06002574 RID: 9588 RVA: 0x00095828 File Offset: 0x00094C28
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.PathFigureCollection" />.</returns>
		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06002575 RID: 9589 RVA: 0x00095850 File Offset: 0x00094C50
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
		// Token: 0x06002576 RID: 9590 RVA: 0x00095864 File Offset: 0x00094C64
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06002577 RID: 9591 RVA: 0x0009587C File Offset: 0x00094C7C
		internal static PathFigureCollection Empty
		{
			get
			{
				if (PathFigureCollection.s_empty == null)
				{
					PathFigureCollection pathFigureCollection = new PathFigureCollection();
					pathFigureCollection.Freeze();
					PathFigureCollection.s_empty = pathFigureCollection;
				}
				return PathFigureCollection.s_empty;
			}
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x000958A8 File Offset: 0x00094CA8
		internal PathFigure Internal_GetItem(int i)
		{
			return this._collection[i];
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x000958C4 File Offset: 0x00094CC4
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

		// Token: 0x0600257A RID: 9594 RVA: 0x0009590C File Offset: 0x00094D0C
		private PathFigure Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is PathFigure))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"PathFigure"
				}));
			}
			return (PathFigure)value;
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x00095970 File Offset: 0x00094D70
		private int AddHelper(PathFigure value)
		{
			int result = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			return result;
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x0009598C File Offset: 0x00094D8C
		internal int AddWithoutFiringPublicEvents(PathFigure value)
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

		// Token: 0x0600257D RID: 9597 RVA: 0x000959DC File Offset: 0x00094DDC
		protected override Freezable CreateInstanceCore()
		{
			return new PathFigureCollection();
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x000959F0 File Offset: 0x00094DF0
		protected override void CloneCore(Freezable source)
		{
			PathFigureCollection pathFigureCollection = (PathFigureCollection)source;
			base.CloneCore(source);
			int count = pathFigureCollection._collection.Count;
			this._collection = new FrugalStructList<PathFigure>(count);
			for (int i = 0; i < count; i++)
			{
				PathFigure pathFigure = pathFigureCollection._collection[i].Clone();
				base.OnFreezablePropertyChanged(null, pathFigure);
				this._collection.Add(pathFigure);
			}
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x00095A58 File Offset: 0x00094E58
		protected override void CloneCurrentValueCore(Freezable source)
		{
			PathFigureCollection pathFigureCollection = (PathFigureCollection)source;
			base.CloneCurrentValueCore(source);
			int count = pathFigureCollection._collection.Count;
			this._collection = new FrugalStructList<PathFigure>(count);
			for (int i = 0; i < count; i++)
			{
				PathFigure pathFigure = pathFigureCollection._collection[i].CloneCurrentValue();
				base.OnFreezablePropertyChanged(null, pathFigure);
				this._collection.Add(pathFigure);
			}
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x00095AC0 File Offset: 0x00094EC0
		protected override void GetAsFrozenCore(Freezable source)
		{
			PathFigureCollection pathFigureCollection = (PathFigureCollection)source;
			base.GetAsFrozenCore(source);
			int count = pathFigureCollection._collection.Count;
			this._collection = new FrugalStructList<PathFigure>(count);
			for (int i = 0; i < count; i++)
			{
				PathFigure pathFigure = (PathFigure)pathFigureCollection._collection[i].GetAsFrozen();
				base.OnFreezablePropertyChanged(null, pathFigure);
				this._collection.Add(pathFigure);
			}
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x00095B2C File Offset: 0x00094F2C
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			PathFigureCollection pathFigureCollection = (PathFigureCollection)source;
			base.GetCurrentValueAsFrozenCore(source);
			int count = pathFigureCollection._collection.Count;
			this._collection = new FrugalStructList<PathFigure>(count);
			for (int i = 0; i < count; i++)
			{
				PathFigure pathFigure = (PathFigure)pathFigureCollection._collection[i].GetCurrentValueAsFrozen();
				base.OnFreezablePropertyChanged(null, pathFigure);
				this._collection.Add(pathFigure);
			}
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x00095B98 File Offset: 0x00094F98
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

		/// <summary>Converte o valor atual de um <see cref="T:System.Windows.Media.PathFigureCollection" /> em um <see cref="T:System.String" />.</summary>
		/// <returns>Uma representação da cadeia de caracteres do <see cref="T:System.Windows.Media.PathFigureCollection" />.</returns>
		// Token: 0x06002583 RID: 9603 RVA: 0x00095BE0 File Offset: 0x00094FE0
		public override string ToString()
		{
			base.ReadPreamble();
			return this.ConvertToString(null, null);
		}

		/// <summary>Converte o valor atual de um <see cref="T:System.Windows.Media.PathFigureCollection" /> em um <see cref="T:System.String" /> usando as informações de formatação específicas da cultura especificadas.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura.</param>
		/// <returns>Uma representação da cadeia de caracteres do <see cref="T:System.Windows.Media.PathFigureCollection" />.</returns>
		// Token: 0x06002584 RID: 9604 RVA: 0x00095BFC File Offset: 0x00094FFC
		public string ToString(IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(null, provider);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.IFormattable.ToString(System.String,System.IFormatProvider)" />.</summary>
		/// <param name="format">O <see cref="T:System.String" /> especificando o formato a ser usado.  
		///
		/// ou - 
		/// <see langword="null" /> (<see langword="Nothing" /> no Visual Basic) para usar o formato padrão definido para o tipo da implementação de <see cref="T:System.IFormattable" />.</param>
		/// <param name="provider">O <see cref="T:System.IFormatProvider" /> a ser usado para formatar o valor.  
		///
		/// ou - 
		/// <see langword="null" /> (<see langword="Nothing" /> no Visual Basic) para obter as informações de formato numérico da configuração de localidade atual do sistema operacional.</param>
		/// <returns>Uma <see cref="T:System.String" /> que contém o valor da instância atual no formato especificado.</returns>
		// Token: 0x06002585 RID: 9605 RVA: 0x00095C18 File Offset: 0x00095018
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(format, provider);
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x00095C34 File Offset: 0x00095034
		internal string ConvertToString(string format, IFormatProvider provider)
		{
			if (this._collection.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this._collection.Count; i++)
			{
				stringBuilder.AppendFormat(provider, "{0:" + format + "}", new object[]
				{
					this._collection[i]
				});
				if (i != this._collection.Count - 1)
				{
					stringBuilder.Append(" ");
				}
			}
			return stringBuilder.ToString();
		}

		/// <summary>Retorna uma instância do <see cref="T:System.Windows.Media.PathFigureCollection" /> criado com base em uma cadeia de caracteres especificada.</summary>
		/// <param name="source">A cadeia de caracteres convertida em um <see cref="T:System.Windows.Media.PathFigureCollection" />.</param>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.PathFigureCollection" /> criada com base no <paramref name="source" />.</returns>
		// Token: 0x06002587 RID: 9607 RVA: 0x00095CC0 File Offset: 0x000950C0
		public static PathFigureCollection Parse(string source)
		{
			IFormatProvider invariantEnglishUS = TypeConverterHelper.InvariantEnglishUS;
			return Parsers.ParsePathFigureCollection(source, invariantEnglishUS);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.PathFigureCollection" />.</summary>
		// Token: 0x06002588 RID: 9608 RVA: 0x00095CDC File Offset: 0x000950DC
		public PathFigureCollection()
		{
			this._collection = default(FrugalStructList<PathFigure>);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.PathFigureCollection" /> que pode conter inicialmente o número especificado de objetos <see cref="T:System.Windows.Media.PathFigure" />.</summary>
		/// <param name="capacity">A capacidade inicial deste <see cref="T:System.Windows.Media.PathFigureCollection" />.</param>
		// Token: 0x06002589 RID: 9609 RVA: 0x00095CFC File Offset: 0x000950FC
		public PathFigureCollection(int capacity)
		{
			this._collection = new FrugalStructList<PathFigure>(capacity);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.PathFigureCollection" /> que contém os objetos <see cref="T:System.Windows.Media.PathFigure" /> especificados.</summary>
		/// <param name="collection">A coleção de objetos <see cref="T:System.Windows.Media.PathFigure" /> que, reunidos, formam a geometria do <see cref="T:System.Windows.Shapes.Path" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> é <see langword="null" />.</exception>
		// Token: 0x0600258A RID: 9610 RVA: 0x00095D1C File Offset: 0x0009511C
		public PathFigureCollection(IEnumerable<PathFigure> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				bool flag = true;
				ICollection<PathFigure> collection2 = collection as ICollection<PathFigure>;
				if (collection2 != null)
				{
					this._collection = new FrugalStructList<PathFigure>(collection2);
				}
				else
				{
					ICollection collection3 = collection as ICollection;
					if (collection3 != null)
					{
						this._collection = new FrugalStructList<PathFigure>(collection3);
					}
					else
					{
						this._collection = default(FrugalStructList<PathFigure>);
						foreach (PathFigure pathFigure in collection)
						{
							if (pathFigure == null)
							{
								throw new ArgumentException(SR.Get("Collection_NoNull"));
							}
							PathFigure pathFigure2 = pathFigure;
							base.OnFreezablePropertyChanged(null, pathFigure2);
							this._collection.Add(pathFigure2);
						}
						flag = false;
					}
				}
				if (flag)
				{
					foreach (PathFigure pathFigure3 in collection)
					{
						if (pathFigure3 == null)
						{
							throw new ArgumentException(SR.Get("Collection_NoNull"));
						}
						base.OnFreezablePropertyChanged(null, pathFigure3);
					}
				}
				base.WritePostscript();
				return;
			}
			throw new ArgumentNullException("collection");
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x00095E60 File Offset: 0x00095260
		internal bool CanSerializeToString()
		{
			bool flag = true;
			int num = 0;
			while (num < this._collection.Count && flag)
			{
				flag &= this._collection[num].CanSerializeToString();
				num++;
			}
			return flag;
		}

		// Token: 0x04001197 RID: 4503
		private static PathFigureCollection s_empty;

		// Token: 0x04001198 RID: 4504
		internal FrugalStructList<PathFigure> _collection;

		// Token: 0x04001199 RID: 4505
		internal uint _version;

		/// <summary>Enumera itens <see cref="T:System.Windows.Media.PathFigure" /> em um <see cref="T:System.Windows.Media.PathFigureCollection" />.</summary>
		// Token: 0x02000877 RID: 2167
		public struct Enumerator : IEnumerator, IEnumerator<PathFigure>, IDisposable
		{
			// Token: 0x06005795 RID: 22421 RVA: 0x00165FA0 File Offset: 0x001653A0
			internal Enumerator(PathFigureCollection list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = null;
			}

			/// <summary>Este tipo ou membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x06005796 RID: 22422 RVA: 0x00165FD0 File Offset: 0x001653D0
			void IDisposable.Dispose()
			{
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador foi avançado com êxito para o próximo elemento; caso contrário, <see langword="false" />.</returns>
			// Token: 0x06005797 RID: 22423 RVA: 0x00165FE0 File Offset: 0x001653E0
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					PathFigureCollection list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._current = list._collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Redefine o enumerador com sua posição inicial, que é antes do primeiro elemento da coleção.</summary>
			// Token: 0x06005798 RID: 22424 RVA: 0x00166074 File Offset: 0x00165474
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
			// Token: 0x17001214 RID: 4628
			// (get) Token: 0x06005799 RID: 22425 RVA: 0x001660B8 File Offset: 0x001654B8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém o elemento atual na coleção.</summary>
			/// <returns>O elemento atual na coleção.</returns>
			// Token: 0x17001215 RID: 4629
			// (get) Token: 0x0600579A RID: 22426 RVA: 0x001660CC File Offset: 0x001654CC
			public PathFigure Current
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

			// Token: 0x04002899 RID: 10393
			private PathFigure _current;

			// Token: 0x0400289A RID: 10394
			private PathFigureCollection _list;

			// Token: 0x0400289B RID: 10395
			private uint _version;

			// Token: 0x0400289C RID: 10396
			private int _index;
		}
	}
}
