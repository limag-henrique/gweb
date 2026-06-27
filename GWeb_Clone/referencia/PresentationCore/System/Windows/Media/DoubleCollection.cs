using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Markup;
using System.Windows.Media.Converters;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media
{
	/// <summary>Representa uma coleção ordenada de valores <see cref="T:System.Double" />.</summary>
	// Token: 0x020003A5 RID: 933
	[ValueSerializer(typeof(DoubleCollectionValueSerializer))]
	[TypeConverter(typeof(DoubleCollectionConverter))]
	public sealed class DoubleCollection : Freezable, IFormattable, IList, ICollection, IEnumerable, IList<double>, ICollection<double>, IEnumerable<double>
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.DoubleCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060022E5 RID: 8933 RVA: 0x0008D14C File Offset: 0x0008C54C
		public new DoubleCollection Clone()
		{
			return (DoubleCollection)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.DoubleCollection" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060022E6 RID: 8934 RVA: 0x0008D164 File Offset: 0x0008C564
		public new DoubleCollection CloneCurrentValue()
		{
			return (DoubleCollection)base.CloneCurrentValue();
		}

		/// <summary>Adiciona um <see cref="T:System.Double" /> ao final deste <see cref="T:System.Windows.Media.DoubleCollection" />.</summary>
		/// <param name="value">O item a ser adicionado ao final desta coleção.</param>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.DoubleCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.DoubleCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x060022E7 RID: 8935 RVA: 0x0008D17C File Offset: 0x0008C57C
		public void Add(double value)
		{
			this.AddHelper(value);
		}

		/// <summary>Remove todos os itens desta <see cref="T:System.Windows.Media.DoubleCollection" />.</summary>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.DoubleCollection" /> é somente leitura.</exception>
		// Token: 0x060022E8 RID: 8936 RVA: 0x0008D194 File Offset: 0x0008C594
		public void Clear()
		{
			base.WritePreamble();
			this._collection.Clear();
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Determina se um <see cref="T:System.Double" /> está neste <see cref="T:System.Windows.Media.DoubleCollection" />.</summary>
		/// <param name="value">O item a ser localizado nesta coleção.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" />, o <see cref="T:System.Double" /> especificado, estiver neste <see cref="T:System.Windows.Media.DoubleCollection" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x060022E9 RID: 8937 RVA: 0x0008D1C8 File Offset: 0x0008C5C8
		public bool Contains(double value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Obtém o índice da primeira ocorrência do <see cref="T:System.Double" /> especificado.</summary>
		/// <param name="value">O <see cref="T:System.Double" /> a ser localizado no <see cref="T:System.Windows.Media.DoubleCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na <see cref="T:System.Windows.Media.DoubleCollection" />, caso contrário, -1.</returns>
		// Token: 0x060022EA RID: 8938 RVA: 0x0008D1E8 File Offset: 0x0008C5E8
		public int IndexOf(double value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Insere um <see cref="T:System.Double" /> nesta <see cref="T:System.Windows.Media.DoubleCollection" /> no índice especificado.</summary>
		/// <param name="index">O índice no qual inserir o <paramref name="value" />, o <see cref="T:System.Double" /> especificado.</param>
		/// <param name="value">O item a ser inserido.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> não é um índice válido no <see cref="T:System.Windows.Media.DoubleCollection" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.DoubleCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.DoubleCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x060022EB RID: 8939 RVA: 0x0008D208 File Offset: 0x0008C608
		public void Insert(int index, double value)
		{
			base.WritePreamble();
			this._collection.Insert(index, value);
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Remove a primeira ocorrência do <see cref="T:System.Double" /> especificado dessa <see cref="T:System.Windows.Media.DoubleCollection" />.</summary>
		/// <param name="value">O item a ser removido desta coleção.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> tiver sido removido da <see cref="T:System.Windows.Media.DoubleCollection" />, caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.DoubleCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.DoubleCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x060022EC RID: 8940 RVA: 0x0008D23C File Offset: 0x0008C63C
		public bool Remove(double value)
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

		/// <summary>Remove o <see cref="T:System.Double" /> no índice especificado dessa <see cref="T:System.Windows.Media.DoubleCollection" />.</summary>
		/// <param name="index">O índice do item a ser removido.</param>
		// Token: 0x060022ED RID: 8941 RVA: 0x0008D280 File Offset: 0x0008C680
		public void RemoveAt(int index)
		{
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x0008D29C File Offset: 0x0008C69C
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			base.WritePreamble();
			this._collection.RemoveAt(index);
			this._version += 1U;
		}

		/// <summary>Obtém ou define o <see cref="T:System.Double" /> no índice de base zero especificado.</summary>
		/// <param name="index">O índice baseado em zero do valor <see cref="T:System.Double" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Double" /> no índice especificado.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> não é um índice válido no <see cref="T:System.Windows.Media.DoubleCollection" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.DoubleCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.DoubleCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x170006F0 RID: 1776
		public double this[int index]
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

		/// <summary>Obtém o número de duplicatas contidas no <see cref="T:System.Windows.Media.DoubleCollection" />.</summary>
		/// <returns>O número de duplicatas contidas no <see cref="T:System.Windows.Media.DoubleCollection" />.</returns>
		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060022F1 RID: 8945 RVA: 0x0008D320 File Offset: 0x0008C720
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Copia os itens desse <see cref="T:System.Windows.Media.DoubleCollection" />, começando com o índice especificado, em uma matriz de valores <see cref="T:System.Double" />.</summary>
		/// <param name="array">A matriz que é o destino dos itens copiados deste <see cref="T:System.Windows.Media.DoubleCollection" />.</param>
		/// <param name="index">O índice no qual começar a copiar.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> é multidimensional.  
		///
		/// ou - 
		/// O número de itens na origem <see cref="T:System.Windows.Media.DoubleCollection" /> é maior do que o espaço disponível de <paramref name="index" /> até o final do <paramref name="array" /> de destino.</exception>
		// Token: 0x060022F2 RID: 8946 RVA: 0x0008D340 File Offset: 0x0008C740
		public void CopyTo(double[] array, int index)
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

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060022F3 RID: 8947 RVA: 0x0008D390 File Offset: 0x0008C790
		bool ICollection<double>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Retorna um enumerador que pode iterar pela coleção.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.DoubleCollection.Enumerator" /> que pode iterar pela coleção.</returns>
		// Token: 0x060022F4 RID: 8948 RVA: 0x0008D3AC File Offset: 0x0008C7AC
		public DoubleCollection.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new DoubleCollection.Enumerator(this);
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x0008D3C8 File Offset: 0x0008C7C8
		IEnumerator<double> IEnumerable<double>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.DoubleCollection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060022F6 RID: 8950 RVA: 0x0008D3E0 File Offset: 0x0008C7E0
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<double>)this).IsReadOnly;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.DoubleCollection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060022F7 RID: 8951 RVA: 0x0008D3F4 File Offset: 0x0008C7F4
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
		// Token: 0x170006F5 RID: 1781
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
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.DoubleCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x060022FA RID: 8954 RVA: 0x0008D448 File Offset: 0x0008C848
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.DoubleCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.DoubleCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060022FB RID: 8955 RVA: 0x0008D464 File Offset: 0x0008C864
		bool IList.Contains(object value)
		{
			return value is double && this.Contains((double)value);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.DoubleCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x060022FC RID: 8956 RVA: 0x0008D488 File Offset: 0x0008C888
		int IList.IndexOf(object value)
		{
			if (value is double)
			{
				return this.IndexOf((double)value);
			}
			return -1;
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.DoubleCollection" />.</param>
		// Token: 0x060022FD RID: 8957 RVA: 0x0008D4AC File Offset: 0x0008C8AC
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.DoubleCollection" />.</param>
		// Token: 0x060022FE RID: 8958 RVA: 0x0008D4C8 File Offset: 0x0008C8C8
		void IList.Remove(object value)
		{
			if (value is double)
			{
				this.Remove((double)value);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.DoubleCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x060022FF RID: 8959 RVA: 0x0008D4EC File Offset: 0x0008C8EC
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
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.DoubleCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06002300 RID: 8960 RVA: 0x0008D5C4 File Offset: 0x0008C9C4
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.DoubleCollection" />.</returns>
		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06002301 RID: 8961 RVA: 0x0008D5EC File Offset: 0x0008C9EC
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
		// Token: 0x06002302 RID: 8962 RVA: 0x0008D600 File Offset: 0x0008CA00
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06002303 RID: 8963 RVA: 0x0008D618 File Offset: 0x0008CA18
		internal static DoubleCollection Empty
		{
			get
			{
				if (DoubleCollection.s_empty == null)
				{
					DoubleCollection doubleCollection = new DoubleCollection();
					doubleCollection.Freeze();
					DoubleCollection.s_empty = doubleCollection;
				}
				return DoubleCollection.s_empty;
			}
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x0008D644 File Offset: 0x0008CA44
		internal double Internal_GetItem(int i)
		{
			return this._collection[i];
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x0008D660 File Offset: 0x0008CA60
		private double Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is double))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"double"
				}));
			}
			return (double)value;
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x0008D6C4 File Offset: 0x0008CAC4
		private int AddHelper(double value)
		{
			int result = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			return result;
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x0008D6E0 File Offset: 0x0008CAE0
		internal int AddWithoutFiringPublicEvents(double value)
		{
			base.WritePreamble();
			int result = this._collection.Add(value);
			this._version += 1U;
			return result;
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x0008D714 File Offset: 0x0008CB14
		protected override Freezable CreateInstanceCore()
		{
			return new DoubleCollection();
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x0008D728 File Offset: 0x0008CB28
		protected override void CloneCore(Freezable source)
		{
			DoubleCollection doubleCollection = (DoubleCollection)source;
			base.CloneCore(source);
			int count = doubleCollection._collection.Count;
			this._collection = new FrugalStructList<double>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(doubleCollection._collection[i]);
			}
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x0008D780 File Offset: 0x0008CB80
		protected override void CloneCurrentValueCore(Freezable source)
		{
			DoubleCollection doubleCollection = (DoubleCollection)source;
			base.CloneCurrentValueCore(source);
			int count = doubleCollection._collection.Count;
			this._collection = new FrugalStructList<double>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(doubleCollection._collection[i]);
			}
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x0008D7D8 File Offset: 0x0008CBD8
		protected override void GetAsFrozenCore(Freezable source)
		{
			DoubleCollection doubleCollection = (DoubleCollection)source;
			base.GetAsFrozenCore(source);
			int count = doubleCollection._collection.Count;
			this._collection = new FrugalStructList<double>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(doubleCollection._collection[i]);
			}
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x0008D830 File Offset: 0x0008CC30
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			DoubleCollection doubleCollection = (DoubleCollection)source;
			base.GetCurrentValueAsFrozenCore(source);
			int count = doubleCollection._collection.Count;
			this._collection = new FrugalStructList<double>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(doubleCollection._collection[i]);
			}
		}

		/// <summary>Cria uma representação de <see cref="T:System.String" /> deste <see cref="T:System.Windows.Media.DoubleCollection" />.</summary>
		/// <returns>Retorna um <see cref="T:System.String" /> que contém os valores deste <see cref="T:System.Windows.Media.DoubleCollection" />.</returns>
		// Token: 0x0600230D RID: 8973 RVA: 0x0008D888 File Offset: 0x0008CC88
		public override string ToString()
		{
			base.ReadPreamble();
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação de <see cref="T:System.String" /> deste <see cref="T:System.Windows.Media.DoubleCollection" />.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura.</param>
		/// <returns>Retorna um <see cref="T:System.String" /> que contém os valores deste <see cref="T:System.Windows.Media.DoubleCollection" />.</returns>
		// Token: 0x0600230E RID: 8974 RVA: 0x0008D8A4 File Offset: 0x0008CCA4
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
		// Token: 0x0600230F RID: 8975 RVA: 0x0008D8C0 File Offset: 0x0008CCC0
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(format, provider);
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x0008D8DC File Offset: 0x0008CCDC
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

		/// <summary>Converte uma representação <see cref="T:System.String" /> de uma coleção de duplicatas em um <see cref="T:System.Windows.Media.DoubleCollection" /> equivalente.</summary>
		/// <param name="source">A representação <see cref="T:System.String" /> da coleção de duplicatas.</param>
		/// <returns>Retorna a <see cref="T:System.Windows.Media.DoubleCollection" /> equivalente.</returns>
		// Token: 0x06002311 RID: 8977 RVA: 0x0008D96C File Offset: 0x0008CD6C
		public static DoubleCollection Parse(string source)
		{
			IFormatProvider invariantEnglishUS = TypeConverterHelper.InvariantEnglishUS;
			TokenizerHelper tokenizerHelper = new TokenizerHelper(source, invariantEnglishUS);
			DoubleCollection doubleCollection = new DoubleCollection();
			while (tokenizerHelper.NextToken())
			{
				double value = Convert.ToDouble(tokenizerHelper.GetCurrentToken(), invariantEnglishUS);
				doubleCollection.Add(value);
			}
			return doubleCollection;
		}

		/// <summary>Inicializa uma nova instância de um <see cref="T:System.Windows.Media.DoubleCollection" />.</summary>
		// Token: 0x06002312 RID: 8978 RVA: 0x0008D9AC File Offset: 0x0008CDAC
		public DoubleCollection()
		{
			this._collection = default(FrugalStructList<double>);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.DoubleCollection" /> com a capacidade especificada ou o número de Valores <see cref="T:System.Double" /> que a coleção é capaz de armazenar inicialmente.</summary>
		/// <param name="capacity">O número de valores de <see cref="T:System.Double" /> que a coleção é capaz de armazenar inicialmente.</param>
		// Token: 0x06002313 RID: 8979 RVA: 0x0008D9CC File Offset: 0x0008CDCC
		public DoubleCollection(int capacity)
		{
			this._collection = new FrugalStructList<double>(capacity);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.DoubleCollection" /> com a coleção especificada dos valores <see cref="T:System.Double" />.</summary>
		/// <param name="collection">A coleção de valores <see cref="T:System.Double" /> que compõem o <see cref="T:System.Windows.Media.DoubleCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> é <see langword="null" />.</exception>
		// Token: 0x06002314 RID: 8980 RVA: 0x0008D9EC File Offset: 0x0008CDEC
		public DoubleCollection(IEnumerable<double> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				ICollection<double> collection2 = collection as ICollection<double>;
				if (collection2 != null)
				{
					this._collection = new FrugalStructList<double>(collection2);
				}
				else
				{
					ICollection collection3 = collection as ICollection;
					if (collection3 != null)
					{
						this._collection = new FrugalStructList<double>(collection3);
					}
					else
					{
						this._collection = default(FrugalStructList<double>);
						foreach (double value in collection)
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

		// Token: 0x04001130 RID: 4400
		private static DoubleCollection s_empty;

		// Token: 0x04001131 RID: 4401
		internal FrugalStructList<double> _collection;

		// Token: 0x04001132 RID: 4402
		internal uint _version;

		/// <summary>Enumera itens <see cref="T:System.Double" /> em um <see cref="T:System.Windows.Media.DoubleCollection" />.</summary>
		// Token: 0x02000871 RID: 2161
		public struct Enumerator : IEnumerator, IEnumerator<double>, IDisposable
		{
			// Token: 0x06005775 RID: 22389 RVA: 0x0016579C File Offset: 0x00164B9C
			internal Enumerator(DoubleCollection list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = 0.0;
			}

			/// <summary>Este tipo ou membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x06005776 RID: 22390 RVA: 0x001657D4 File Offset: 0x00164BD4
			void IDisposable.Dispose()
			{
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador foi avançado com êxito para o próximo elemento; caso contrário, <see langword="false" />.</returns>
			// Token: 0x06005777 RID: 22391 RVA: 0x001657E4 File Offset: 0x00164BE4
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					DoubleCollection list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._current = list._collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Redefine o enumerador com sua posição inicial, que é antes do primeiro elemento da coleção.</summary>
			// Token: 0x06005778 RID: 22392 RVA: 0x00165878 File Offset: 0x00164C78
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
			// Token: 0x17001209 RID: 4617
			// (get) Token: 0x06005779 RID: 22393 RVA: 0x001658BC File Offset: 0x00164CBC
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém o elemento atual na coleção.</summary>
			/// <returns>O elemento atual na coleção.</returns>
			// Token: 0x1700120A RID: 4618
			// (get) Token: 0x0600577A RID: 22394 RVA: 0x001658D4 File Offset: 0x00164CD4
			public double Current
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

			// Token: 0x04002882 RID: 10370
			private double _current;

			// Token: 0x04002883 RID: 10371
			private DoubleCollection _list;

			// Token: 0x04002884 RID: 10372
			private uint _version;

			// Token: 0x04002885 RID: 10373
			private int _index;
		}
	}
}
