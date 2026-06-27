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
	/// <summary>Representa uma coleção de valores <see cref="T:System.Int32" />.</summary>
	// Token: 0x020003B9 RID: 953
	[TypeConverter(typeof(Int32CollectionConverter))]
	[ValueSerializer(typeof(Int32CollectionValueSerializer))]
	public sealed class Int32Collection : Freezable, IFormattable, IList, ICollection, IEnumerable, IList<int>, ICollection<int>, IEnumerable<int>
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Int32Collection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002494 RID: 9364 RVA: 0x00092E4C File Offset: 0x0009224C
		public new Int32Collection Clone()
		{
			return (Int32Collection)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Int32Collection" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002495 RID: 9365 RVA: 0x00092E64 File Offset: 0x00092264
		public new Int32Collection CloneCurrentValue()
		{
			return (Int32Collection)base.CloneCurrentValue();
		}

		/// <summary>Adiciona um <see cref="T:System.Int32" /> ao fim da coleção.</summary>
		/// <param name="value">O <see cref="T:System.Int32" /> a adicionar ao final da coleção.</param>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.Int32Collection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.Int32Collection" /> tem um tamanho fixo.</exception>
		// Token: 0x06002496 RID: 9366 RVA: 0x00092E7C File Offset: 0x0009227C
		public void Add(int value)
		{
			this.AddHelper(value);
		}

		/// <summary>Remove todos os valores <see cref="T:System.Int32" /> da coleção.</summary>
		// Token: 0x06002497 RID: 9367 RVA: 0x00092E94 File Offset: 0x00092294
		public void Clear()
		{
			base.WritePreamble();
			this._collection.Clear();
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Obtém um valor que indica se a coleção contém o <see cref="T:System.Int32" /> especificado.</summary>
		/// <param name="value">O <see cref="T:System.Int32" /> a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Int32" /> for encontrado no <see cref="T:System.Windows.Media.Int32Collection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002498 RID: 9368 RVA: 0x00092EC8 File Offset: 0x000922C8
		public bool Contains(int value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Int32" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="value">O <see cref="T:System.Int32" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="value" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x06002499 RID: 9369 RVA: 0x00092EE8 File Offset: 0x000922E8
		public int IndexOf(int value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Insere um <see cref="T:System.Int32" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Int32" /> é inserido.</param>
		/// <param name="value">O <see cref="T:System.Int32" /> a ser inserido na coleção.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> não é um índice válido no <see cref="T:System.Windows.Media.Int32Collection" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.Int32Collection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.Int32Collection" /> tem um tamanho fixo.</exception>
		// Token: 0x0600249A RID: 9370 RVA: 0x00092F08 File Offset: 0x00092308
		public void Insert(int index, int value)
		{
			base.WritePreamble();
			this._collection.Insert(index, value);
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Remove um <see cref="T:System.Int32" /> da coleção.</summary>
		/// <param name="value">Identifica o <see cref="T:System.Int32" /> a ser removido da coleção.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> tiver sido removido da <see cref="T:System.Windows.Media.Int32Collection" />, caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.Int32Collection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.Int32Collection" /> tem um tamanho fixo.</exception>
		// Token: 0x0600249B RID: 9371 RVA: 0x00092F3C File Offset: 0x0009233C
		public bool Remove(int value)
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

		/// <summary>Remove o <see cref="T:System.Int32" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Int32" /> a ser removido.</param>
		// Token: 0x0600249C RID: 9372 RVA: 0x00092F80 File Offset: 0x00092380
		public void RemoveAt(int index)
		{
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x00092F9C File Offset: 0x0009239C
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			base.WritePreamble();
			this._collection.RemoveAt(index);
			this._version += 1U;
		}

		/// <summary>Obtém ou define o <see cref="T:System.Int32" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Int32" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Int32" /> no índice especificado.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que o <see cref="P:System.Windows.Media.Int32Collection.Count" />.</exception>
		// Token: 0x1700072F RID: 1839
		public int this[int index]
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

		/// <summary>Obtém o número de valores <see cref="T:System.Int32" /> contidos no <see cref="T:System.Windows.Media.Int32Collection" />.</summary>
		/// <returns>O número de <see cref="T:System.Int32" /> valores contidos no <see cref="T:System.Windows.Media.Int32Collection" />.</returns>
		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x060024A0 RID: 9376 RVA: 0x00093020 File Offset: 0x00092420
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Copia todos os valores de <see cref="T:System.Int32" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> é multidimensional.  
		///
		/// ou - 
		/// O número de itens na origem <see cref="T:System.Windows.Media.Int32Collection" /> é maior do que o espaço disponível de <paramref name="index" /> até o final do <paramref name="array" /> de destino.</exception>
		// Token: 0x060024A1 RID: 9377 RVA: 0x00093040 File Offset: 0x00092440
		public void CopyTo(int[] array, int index)
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

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060024A2 RID: 9378 RVA: 0x00093090 File Offset: 0x00092490
		bool ICollection<int>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Retorna um <see cref="T:System.Windows.Media.Int32Collection.Enumerator" /> que pode iterar pela coleção.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Int32Collection.Enumerator" /> que pode iterar pela coleção.</returns>
		// Token: 0x060024A3 RID: 9379 RVA: 0x000930AC File Offset: 0x000924AC
		public Int32Collection.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new Int32Collection.Enumerator(this);
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000930C8 File Offset: 0x000924C8
		IEnumerator<int> IEnumerable<int>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.Int32Collection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x060024A5 RID: 9381 RVA: 0x000930E0 File Offset: 0x000924E0
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<int>)this).IsReadOnly;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.Int32Collection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x060024A6 RID: 9382 RVA: 0x000930F4 File Offset: 0x000924F4
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
		// Token: 0x17000734 RID: 1844
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
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.Int32Collection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x060024A9 RID: 9385 RVA: 0x00093148 File Offset: 0x00092548
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.Int32Collection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.Int32Collection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060024AA RID: 9386 RVA: 0x00093164 File Offset: 0x00092564
		bool IList.Contains(object value)
		{
			return value is int && this.Contains((int)value);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.Int32Collection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x060024AB RID: 9387 RVA: 0x00093188 File Offset: 0x00092588
		int IList.IndexOf(object value)
		{
			if (value is int)
			{
				return this.IndexOf((int)value);
			}
			return -1;
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.Int32Collection" />.</param>
		// Token: 0x060024AC RID: 9388 RVA: 0x000931AC File Offset: 0x000925AC
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.Int32Collection" />.</param>
		// Token: 0x060024AD RID: 9389 RVA: 0x000931C8 File Offset: 0x000925C8
		void IList.Remove(object value)
		{
			if (value is int)
			{
				this.Remove((int)value);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.Int32Collection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x060024AE RID: 9390 RVA: 0x000931EC File Offset: 0x000925EC
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
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.Int32Collection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060024AF RID: 9391 RVA: 0x000932C4 File Offset: 0x000926C4
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.Int32Collection" />.</returns>
		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060024B0 RID: 9392 RVA: 0x000932EC File Offset: 0x000926EC
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
		// Token: 0x060024B1 RID: 9393 RVA: 0x00093300 File Offset: 0x00092700
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060024B2 RID: 9394 RVA: 0x00093318 File Offset: 0x00092718
		internal static Int32Collection Empty
		{
			get
			{
				if (Int32Collection.s_empty == null)
				{
					Int32Collection int32Collection = new Int32Collection();
					int32Collection.Freeze();
					Int32Collection.s_empty = int32Collection;
				}
				return Int32Collection.s_empty;
			}
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x00093344 File Offset: 0x00092744
		internal int Internal_GetItem(int i)
		{
			return this._collection[i];
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x00093360 File Offset: 0x00092760
		private int Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is int))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"int"
				}));
			}
			return (int)value;
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x000933C4 File Offset: 0x000927C4
		private int AddHelper(int value)
		{
			int result = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			return result;
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000933E0 File Offset: 0x000927E0
		internal int AddWithoutFiringPublicEvents(int value)
		{
			base.WritePreamble();
			int result = this._collection.Add(value);
			this._version += 1U;
			return result;
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x00093414 File Offset: 0x00092814
		protected override Freezable CreateInstanceCore()
		{
			return new Int32Collection();
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x00093428 File Offset: 0x00092828
		protected override void CloneCore(Freezable source)
		{
			Int32Collection int32Collection = (Int32Collection)source;
			base.CloneCore(source);
			int count = int32Collection._collection.Count;
			this._collection = new FrugalStructList<int>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(int32Collection._collection[i]);
			}
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x00093480 File Offset: 0x00092880
		protected override void CloneCurrentValueCore(Freezable source)
		{
			Int32Collection int32Collection = (Int32Collection)source;
			base.CloneCurrentValueCore(source);
			int count = int32Collection._collection.Count;
			this._collection = new FrugalStructList<int>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(int32Collection._collection[i]);
			}
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000934D8 File Offset: 0x000928D8
		protected override void GetAsFrozenCore(Freezable source)
		{
			Int32Collection int32Collection = (Int32Collection)source;
			base.GetAsFrozenCore(source);
			int count = int32Collection._collection.Count;
			this._collection = new FrugalStructList<int>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(int32Collection._collection[i]);
			}
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x00093530 File Offset: 0x00092930
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			Int32Collection int32Collection = (Int32Collection)source;
			base.GetCurrentValueAsFrozenCore(source);
			int count = int32Collection._collection.Count;
			this._collection = new FrugalStructList<int>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(int32Collection._collection[i]);
			}
		}

		/// <summary>Converte o valor atual de um <see cref="T:System.Windows.Media.Int32Collection" /> em um <see cref="T:System.String" />.</summary>
		/// <returns>Uma representação da cadeia de caracteres do <see cref="T:System.Windows.Media.Int32Collection" />.</returns>
		// Token: 0x060024BC RID: 9404 RVA: 0x00093588 File Offset: 0x00092988
		public override string ToString()
		{
			base.ReadPreamble();
			return this.ConvertToString(null, null);
		}

		/// <summary>Converte o valor atual de um <see cref="T:System.Windows.Media.Int32Collection" /> em um <see cref="T:System.String" /> usando as informações de formatação específicas da cultura especificadas.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura.</param>
		/// <returns>Uma representação da cadeia de caracteres do <see cref="T:System.Windows.Media.Int32Collection" />.</returns>
		// Token: 0x060024BD RID: 9405 RVA: 0x000935A4 File Offset: 0x000929A4
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
		// Token: 0x060024BE RID: 9406 RVA: 0x000935C0 File Offset: 0x000929C0
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(format, provider);
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000935DC File Offset: 0x000929DC
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

		/// <summary>Retorna uma instância do <see cref="T:System.Windows.Media.Int32Collection" /> criado com base em uma cadeia de caracteres especificada.</summary>
		/// <param name="source">A cadeia de caracteres convertida em um <see cref="T:System.Int32" />.</param>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Int32Collection" /> criada com base no <paramref name="source" />.</returns>
		// Token: 0x060024C0 RID: 9408 RVA: 0x0009366C File Offset: 0x00092A6C
		public static Int32Collection Parse(string source)
		{
			IFormatProvider invariantEnglishUS = TypeConverterHelper.InvariantEnglishUS;
			TokenizerHelper tokenizerHelper = new TokenizerHelper(source, invariantEnglishUS);
			Int32Collection int32Collection = new Int32Collection();
			while (tokenizerHelper.NextToken())
			{
				int value = Convert.ToInt32(tokenizerHelper.GetCurrentToken(), invariantEnglishUS);
				int32Collection.Add(value);
			}
			return int32Collection;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Int32Collection" />.</summary>
		// Token: 0x060024C1 RID: 9409 RVA: 0x000936AC File Offset: 0x00092AAC
		public Int32Collection()
		{
			this._collection = default(FrugalStructList<int>);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Int32Collection" /> com a capacidade especificada ou o número de Valores <see cref="T:System.Int32" /> que a coleção é capaz de armazenar inicialmente.</summary>
		/// <param name="capacity">O número de valores de <see cref="T:System.Int32" /> que a coleção é capaz de armazenar inicialmente.</param>
		// Token: 0x060024C2 RID: 9410 RVA: 0x000936CC File Offset: 0x00092ACC
		public Int32Collection(int capacity)
		{
			this._collection = new FrugalStructList<int>(capacity);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Int32Collection" /> com a coleção especificada dos valores <see cref="T:System.Int32" />.</summary>
		/// <param name="collection">A coleção de valores <see cref="T:System.Int32" /> que compõem o <see cref="T:System.Windows.Media.Int32Collection" />.</param>
		// Token: 0x060024C3 RID: 9411 RVA: 0x000936EC File Offset: 0x00092AEC
		public Int32Collection(IEnumerable<int> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				ICollection<int> collection2 = collection as ICollection<int>;
				if (collection2 != null)
				{
					this._collection = new FrugalStructList<int>(collection2);
				}
				else
				{
					ICollection collection3 = collection as ICollection;
					if (collection3 != null)
					{
						this._collection = new FrugalStructList<int>(collection3);
					}
					else
					{
						this._collection = default(FrugalStructList<int>);
						foreach (int value in collection)
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

		// Token: 0x04001175 RID: 4469
		private static Int32Collection s_empty;

		// Token: 0x04001176 RID: 4470
		internal FrugalStructList<int> _collection;

		// Token: 0x04001177 RID: 4471
		internal uint _version;

		/// <summary>Enumera itens <see cref="T:System.Int32" /> em um <see cref="T:System.Windows.Media.Int32Collection" />.</summary>
		// Token: 0x02000876 RID: 2166
		public struct Enumerator : IEnumerator, IEnumerator<int>, IDisposable
		{
			// Token: 0x0600578F RID: 22415 RVA: 0x00165E28 File Offset: 0x00165228
			internal Enumerator(Int32Collection list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = 0;
			}

			/// <summary>Este tipo ou membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x06005790 RID: 22416 RVA: 0x00165E58 File Offset: 0x00165258
			void IDisposable.Dispose()
			{
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador foi avançado com êxito para o próximo elemento; caso contrário, <see langword="false" />.</returns>
			// Token: 0x06005791 RID: 22417 RVA: 0x00165E68 File Offset: 0x00165268
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					Int32Collection list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._current = list._collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Redefine o enumerador com sua posição inicial, que é antes do primeiro elemento da coleção.</summary>
			// Token: 0x06005792 RID: 22418 RVA: 0x00165EFC File Offset: 0x001652FC
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
			// Token: 0x17001212 RID: 4626
			// (get) Token: 0x06005793 RID: 22419 RVA: 0x00165F40 File Offset: 0x00165340
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém o elemento atual na coleção.</summary>
			/// <returns>O elemento atual na coleção.</returns>
			// Token: 0x17001213 RID: 4627
			// (get) Token: 0x06005794 RID: 22420 RVA: 0x00165F58 File Offset: 0x00165358
			public int Current
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

			// Token: 0x04002895 RID: 10389
			private int _current;

			// Token: 0x04002896 RID: 10390
			private Int32Collection _list;

			// Token: 0x04002897 RID: 10391
			private uint _version;

			// Token: 0x04002898 RID: 10392
			private int _index;
		}
	}
}
