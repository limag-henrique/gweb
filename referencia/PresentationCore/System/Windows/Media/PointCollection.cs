using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Markup;
using System.Windows.Media.Converters;
using MS.Internal;
using MS.Internal.Media;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media
{
	/// <summary>Representa uma coleção de valores de <see cref="T:System.Windows.Point" /> que podem ser acessados individualmente por índice.</summary>
	// Token: 0x020003C9 RID: 969
	[TypeConverter(typeof(PointCollectionConverter))]
	[ValueSerializer(typeof(PointCollectionValueSerializer))]
	public sealed class PointCollection : Freezable, IFormattable, IList, ICollection, IEnumerable, IList<Point>, ICollection<Point>, IEnumerable<Point>
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.PointCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002620 RID: 9760 RVA: 0x00098458 File Offset: 0x00097858
		public new PointCollection Clone()
		{
			return (PointCollection)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.PointCollection" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002621 RID: 9761 RVA: 0x00098470 File Offset: 0x00097870
		public new PointCollection CloneCurrentValue()
		{
			return (PointCollection)base.CloneCurrentValue();
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Point" /> ao final do <see cref="T:System.Windows.Media.PointCollection" />.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Point" /> a ser adicionado ao final da <see cref="T:System.Windows.Media.PointCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.PointCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.PointCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x06002622 RID: 9762 RVA: 0x00098488 File Offset: 0x00097888
		public void Add(Point value)
		{
			this.AddHelper(value);
		}

		/// <summary>Remove todos os itens do <see cref="T:System.Windows.Media.PointCollection" />.</summary>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.PointCollection" /> é somente leitura.</exception>
		// Token: 0x06002623 RID: 9763 RVA: 0x000984A0 File Offset: 0x000978A0
		public void Clear()
		{
			base.WritePreamble();
			this._collection.Clear();
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Media.PointCollection" /> atual contém o <see cref="T:System.Windows.Point" /> especificado.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Point" /> a ser localizado no <see cref="T:System.Windows.Media.PointCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Point" /> for encontrado no <see cref="T:System.Windows.Media.PointCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002624 RID: 9764 RVA: 0x000984D4 File Offset: 0x000978D4
		public bool Contains(Point value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Determina o índice do item especificado na <see cref="T:System.Windows.Media.PointCollection" />.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Point" /> a ser localizado no <see cref="T:System.Windows.Media.PointCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na <see cref="T:System.Windows.Media.PointCollection" />, caso contrário, -1.</returns>
		// Token: 0x06002625 RID: 9765 RVA: 0x000984F4 File Offset: 0x000978F4
		public int IndexOf(Point value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Point" /> no <see cref="T:System.Windows.Media.PointCollection" /> no índice especificado.</summary>
		/// <param name="index">O índice de base zero no qual o <paramref name="value" /> deve ser inserido.</param>
		/// <param name="value">O <see cref="T:System.Windows.Point" /> a ser inserido no <see cref="T:System.Windows.Media.PointCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> não é um índice válido no <see cref="T:System.Windows.Media.PointCollection" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.PointCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.PointCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x06002626 RID: 9766 RVA: 0x00098514 File Offset: 0x00097914
		public void Insert(int index, Point value)
		{
			base.WritePreamble();
			this._collection.Insert(index, value);
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Remove a primeira ocorrência do <see cref="T:System.Windows.Point" /> especificado do <see cref="T:System.Windows.Media.PointCollection" />.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Point" /> a ser removido de <see cref="T:System.Windows.Media.PointCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> tiver sido removido da <see cref="T:System.Windows.Media.PointCollection" />, caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.PointCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.PointCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x06002627 RID: 9767 RVA: 0x00098548 File Offset: 0x00097948
		public bool Remove(Point value)
		{
			base.WritePreamble();
			int num = this.IndexOf(value);
			if (num >= 0)
			{
				this._collection.RemoveAt(num);
				this._version += 1U;
				base.WritePostscript();
				return true;
			}
			return false;
		}

		/// <summary>Remove o <see cref="T:System.Windows.Point" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero do <see cref="T:System.Windows.Point" /> a ser removido.</param>
		// Token: 0x06002628 RID: 9768 RVA: 0x0009858C File Offset: 0x0009798C
		public void RemoveAt(int index)
		{
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x000985A8 File Offset: 0x000979A8
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			base.WritePreamble();
			this._collection.RemoveAt(index);
			this._version += 1U;
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Point" /> no índice especificado.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Point" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Point" /> no índice especificado.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> não é um índice válido no <see cref="T:System.Windows.Media.PointCollection" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.PointCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.PointCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x1700076D RID: 1901
		public Point this[int index]
		{
			get
			{
				base.ReadPreamble();
				return this._collection[index];
			}
			set
			{
				base.WritePreamble();
				this._collection[index] = value;
				this._version += 1U;
				base.WritePostscript();
			}
		}

		/// <summary>Obtém o número de itens contidos no <see cref="T:System.Windows.Media.PointCollection" />.</summary>
		/// <returns>O número de itens no <see cref="T:System.Windows.Media.PointCollection" />.</returns>
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x0600262C RID: 9772 RVA: 0x0009862C File Offset: 0x00097A2C
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Copia os itens da <see cref="T:System.Windows.Media.PointCollection" /> para uma matriz, começando no índice de matriz especificado.</summary>
		/// <param name="array">A matriz unidimensional que é o destino dos itens copiados do <see cref="T:System.Windows.Media.PointCollection" />. A matriz deve ter indexação com base em zero.</param>
		/// <param name="index">O índice com base em zero em <paramref name="array" /> no qual a cópia começa.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> é multidimensional.  
		///
		/// ou - 
		/// O número de itens na origem <see cref="T:System.Windows.Media.PointCollection" /> é maior do que o espaço disponível de <paramref name="index" /> até o final do <paramref name="array" /> de destino.</exception>
		// Token: 0x0600262D RID: 9773 RVA: 0x0009864C File Offset: 0x00097A4C
		public void CopyTo(Point[] array, int index)
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

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x0600262E RID: 9774 RVA: 0x0009869C File Offset: 0x00097A9C
		bool ICollection<Point>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Retorna um enumerador que pode ser iterado por meio de <see cref="T:System.Windows.Media.PointCollection" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.PointCollection.Enumerator" /> que pode ser usado para iterar por meio de <see cref="T:System.Windows.Media.PointCollection" />.</returns>
		// Token: 0x0600262F RID: 9775 RVA: 0x000986B8 File Offset: 0x00097AB8
		public PointCollection.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new PointCollection.Enumerator(this);
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x000986D4 File Offset: 0x00097AD4
		IEnumerator<Point> IEnumerable<Point>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.PointCollection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06002631 RID: 9777 RVA: 0x000986EC File Offset: 0x00097AEC
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<Point>)this).IsReadOnly;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.PointCollection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06002632 RID: 9778 RVA: 0x00098700 File Offset: 0x00097B00
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
		// Token: 0x17000772 RID: 1906
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
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.PointCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x06002635 RID: 9781 RVA: 0x00098754 File Offset: 0x00097B54
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.PointCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.PointCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002636 RID: 9782 RVA: 0x00098770 File Offset: 0x00097B70
		bool IList.Contains(object value)
		{
			return value is Point && this.Contains((Point)value);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.PointCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06002637 RID: 9783 RVA: 0x00098794 File Offset: 0x00097B94
		int IList.IndexOf(object value)
		{
			if (value is Point)
			{
				return this.IndexOf((Point)value);
			}
			return -1;
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.PointCollection" />.</param>
		// Token: 0x06002638 RID: 9784 RVA: 0x000987B8 File Offset: 0x00097BB8
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.PointCollection" />.</param>
		// Token: 0x06002639 RID: 9785 RVA: 0x000987D4 File Offset: 0x00097BD4
		void IList.Remove(object value)
		{
			if (value is Point)
			{
				this.Remove((Point)value);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.PointCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x0600263A RID: 9786 RVA: 0x000987F8 File Offset: 0x00097BF8
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
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.PointCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x000988D0 File Offset: 0x00097CD0
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.PointCollection" />.</returns>
		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x0600263C RID: 9788 RVA: 0x000988F8 File Offset: 0x00097CF8
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
		// Token: 0x0600263D RID: 9789 RVA: 0x0009890C File Offset: 0x00097D0C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x00098924 File Offset: 0x00097D24
		internal static PointCollection Empty
		{
			get
			{
				if (PointCollection.s_empty == null)
				{
					PointCollection pointCollection = new PointCollection();
					pointCollection.Freeze();
					PointCollection.s_empty = pointCollection;
				}
				return PointCollection.s_empty;
			}
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x00098950 File Offset: 0x00097D50
		internal Point Internal_GetItem(int i)
		{
			return this._collection[i];
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x0009896C File Offset: 0x00097D6C
		private Point Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is Point))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"Point"
				}));
			}
			return (Point)value;
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x000989D0 File Offset: 0x00097DD0
		private int AddHelper(Point value)
		{
			int result = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			return result;
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x000989EC File Offset: 0x00097DEC
		internal int AddWithoutFiringPublicEvents(Point value)
		{
			base.WritePreamble();
			int result = this._collection.Add(value);
			this._version += 1U;
			return result;
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x00098A20 File Offset: 0x00097E20
		protected override Freezable CreateInstanceCore()
		{
			return new PointCollection();
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x00098A34 File Offset: 0x00097E34
		protected override void CloneCore(Freezable source)
		{
			PointCollection pointCollection = (PointCollection)source;
			base.CloneCore(source);
			int count = pointCollection._collection.Count;
			this._collection = new FrugalStructList<Point>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(pointCollection._collection[i]);
			}
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x00098A8C File Offset: 0x00097E8C
		protected override void CloneCurrentValueCore(Freezable source)
		{
			PointCollection pointCollection = (PointCollection)source;
			base.CloneCurrentValueCore(source);
			int count = pointCollection._collection.Count;
			this._collection = new FrugalStructList<Point>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(pointCollection._collection[i]);
			}
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x00098AE4 File Offset: 0x00097EE4
		protected override void GetAsFrozenCore(Freezable source)
		{
			PointCollection pointCollection = (PointCollection)source;
			base.GetAsFrozenCore(source);
			int count = pointCollection._collection.Count;
			this._collection = new FrugalStructList<Point>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(pointCollection._collection[i]);
			}
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x00098B3C File Offset: 0x00097F3C
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			PointCollection pointCollection = (PointCollection)source;
			base.GetCurrentValueAsFrozenCore(source);
			int count = pointCollection._collection.Count;
			this._collection = new FrugalStructList<Point>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(pointCollection._collection[i]);
			}
		}

		/// <summary>Cria uma representação de <see cref="T:System.String" /> deste <see cref="T:System.Windows.Media.PointCollection" />.</summary>
		/// <returns>Retorna uma <see cref="T:System.String" /> que contém os valores <see cref="P:System.Windows.Point.X" /> e <see cref="P:System.Windows.Point.Y" /> das estruturas <see cref="T:System.Windows.Point" /> nesta <see cref="T:System.Windows.Media.PointCollection" />.</returns>
		// Token: 0x06002648 RID: 9800 RVA: 0x00098B94 File Offset: 0x00097F94
		public override string ToString()
		{
			base.ReadPreamble();
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação de <see cref="T:System.String" /> deste <see cref="T:System.Windows.Media.PointCollection" />.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura.</param>
		/// <returns>Retorna uma <see cref="T:System.String" /> que contém os valores <see cref="P:System.Windows.Point.X" /> e <see cref="P:System.Windows.Point.Y" /> das estruturas <see cref="T:System.Windows.Point" /> nesta <see cref="T:System.Windows.Media.PointCollection" />.</returns>
		// Token: 0x06002649 RID: 9801 RVA: 0x00098BB0 File Offset: 0x00097FB0
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
		// Token: 0x0600264A RID: 9802 RVA: 0x00098BCC File Offset: 0x00097FCC
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(format, provider);
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x00098BE8 File Offset: 0x00097FE8
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

		/// <summary>Converte uma representação <see cref="T:System.String" /> de uma coleção de pontos em um <see cref="T:System.Windows.Media.PointCollection" /> equivalente.</summary>
		/// <param name="source">A representação <see cref="T:System.String" /> da coleção de pontos.</param>
		/// <returns>O <see cref="T:System.Windows.Media.PointCollection" /> equivalente.</returns>
		// Token: 0x0600264C RID: 9804 RVA: 0x00098C78 File Offset: 0x00098078
		public static PointCollection Parse(string source)
		{
			IFormatProvider invariantEnglishUS = TypeConverterHelper.InvariantEnglishUS;
			TokenizerHelper tokenizerHelper = new TokenizerHelper(source, invariantEnglishUS);
			PointCollection pointCollection = new PointCollection();
			while (tokenizerHelper.NextToken())
			{
				Point value = new Point(Convert.ToDouble(tokenizerHelper.GetCurrentToken(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS));
				pointCollection.Add(value);
			}
			return pointCollection;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.PointCollection" />.</summary>
		// Token: 0x0600264D RID: 9805 RVA: 0x00098CCC File Offset: 0x000980CC
		public PointCollection()
		{
			this._collection = default(FrugalStructList<Point>);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.PointCollection" /> com a capacidade especificada.</summary>
		/// <param name="capacity">O número de valores de <see cref="T:System.Windows.Point" /> que a coleção é capaz de armazenar inicialmente.</param>
		// Token: 0x0600264E RID: 9806 RVA: 0x00098CEC File Offset: 0x000980EC
		public PointCollection(int capacity)
		{
			this._collection = new FrugalStructList<Point>(capacity);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.PointCollection" />, que contém os itens copiados da coleção especificada de valores <see cref="T:System.Windows.Point" /> e tem a mesma capacidade inicial que o número de itens copiados.</summary>
		/// <param name="collection">A coleção cujos itens são copiados para o novo <see cref="T:System.Windows.Media.PointCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> é <see langword="null" />.</exception>
		// Token: 0x0600264F RID: 9807 RVA: 0x00098D0C File Offset: 0x0009810C
		public PointCollection(IEnumerable<Point> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				ICollection<Point> collection2 = collection as ICollection<Point>;
				if (collection2 != null)
				{
					this._collection = new FrugalStructList<Point>(collection2);
				}
				else
				{
					ICollection collection3 = collection as ICollection;
					if (collection3 != null)
					{
						this._collection = new FrugalStructList<Point>(collection3);
					}
					else
					{
						this._collection = default(FrugalStructList<Point>);
						foreach (Point value in collection)
						{
							this._collection.Add(value);
						}
					}
				}
				base.WritePostscript();
				return;
			}
			throw new ArgumentNullException("collection");
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x00098DC8 File Offset: 0x000981C8
		[FriendAccessAllowed]
		internal static object DeserializeFrom(BinaryReader reader)
		{
			uint num = reader.ReadUInt32();
			PointCollection pointCollection = new PointCollection((int)num);
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				Point value = new Point(XamlSerializationHelper.ReadDouble(reader), XamlSerializationHelper.ReadDouble(reader));
				pointCollection.Add(value);
			}
			return pointCollection;
		}

		// Token: 0x040011C2 RID: 4546
		private static PointCollection s_empty;

		// Token: 0x040011C3 RID: 4547
		internal FrugalStructList<Point> _collection;

		// Token: 0x040011C4 RID: 4548
		internal uint _version;

		/// <summary>Enumera itens <see cref="T:System.Windows.Point" /> em um <see cref="T:System.Windows.Media.PointCollection" />.</summary>
		// Token: 0x0200087B RID: 2171
		public struct Enumerator : IEnumerator, IEnumerator<Point>, IDisposable
		{
			// Token: 0x060057A8 RID: 22440 RVA: 0x001665A8 File Offset: 0x001659A8
			internal Enumerator(PointCollection list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = default(Point);
			}

			/// <summary>Este tipo ou membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x060057A9 RID: 22441 RVA: 0x001665DC File Offset: 0x001659DC
			void IDisposable.Dispose()
			{
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador foi avançado com êxito para o próximo elemento; caso contrário, <see langword="false" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Windows.Media.PointCollection" /> foi modificada depois da criação do enumerador.</exception>
			// Token: 0x060057AA RID: 22442 RVA: 0x001665EC File Offset: 0x001659EC
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					PointCollection list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._current = list._collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Redefine o enumerador com sua posição inicial, que é antes do primeiro elemento da coleção.</summary>
			/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Windows.Media.PointCollection" /> foi modificada depois da criação do enumerador.</exception>
			// Token: 0x060057AB RID: 22443 RVA: 0x00166680 File Offset: 0x00165A80
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
			// Token: 0x17001219 RID: 4633
			// (get) Token: 0x060057AC RID: 22444 RVA: 0x001666C4 File Offset: 0x00165AC4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém o elemento atual na coleção.</summary>
			/// <returns>O elemento atual na coleção.</returns>
			/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Windows.Media.PointCollection" /> foi modificada depois da criação do enumerador.</exception>
			// Token: 0x1700121A RID: 4634
			// (get) Token: 0x060057AD RID: 22445 RVA: 0x001666DC File Offset: 0x00165ADC
			public Point Current
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

			// Token: 0x040028A2 RID: 10402
			private Point _current;

			// Token: 0x040028A3 RID: 10403
			private PointCollection _list;

			// Token: 0x040028A4 RID: 10404
			private uint _version;

			// Token: 0x040028A5 RID: 10405
			private int _index;
		}
	}
}
