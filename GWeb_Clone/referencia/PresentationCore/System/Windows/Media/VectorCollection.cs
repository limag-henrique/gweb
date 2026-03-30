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
	/// <summary>Representa uma coleção ordenada de valores <see cref="T:System.Windows.Vector" />.</summary>
	// Token: 0x020003FD RID: 1021
	[TypeConverter(typeof(VectorCollectionConverter))]
	[ValueSerializer(typeof(VectorCollectionValueSerializer))]
	public sealed class VectorCollection : Freezable, IFormattable, IList, ICollection, IEnumerable, IList<Vector>, ICollection<Vector>, IEnumerable<Vector>
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.VectorCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060028A1 RID: 10401 RVA: 0x000A2E7C File Offset: 0x000A227C
		public new VectorCollection Clone()
		{
			return (VectorCollection)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.VectorCollection" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060028A2 RID: 10402 RVA: 0x000A2E94 File Offset: 0x000A2294
		public new VectorCollection CloneCurrentValue()
		{
			return (VectorCollection)base.CloneCurrentValue();
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Vector" /> ao final do <see cref="T:System.Windows.Media.VectorCollection" />.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Vector" /> a ser adicionado ao final da <see cref="T:System.Windows.Media.VectorCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.VectorCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.VectorCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x060028A3 RID: 10403 RVA: 0x000A2EAC File Offset: 0x000A22AC
		public void Add(Vector value)
		{
			this.AddHelper(value);
		}

		/// <summary>Remove todos os itens do <see cref="T:System.Windows.Media.VectorCollection" />.</summary>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.VectorCollection" /> é somente leitura.</exception>
		// Token: 0x060028A4 RID: 10404 RVA: 0x000A2EC4 File Offset: 0x000A22C4
		public void Clear()
		{
			base.WritePreamble();
			this._collection.Clear();
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Media.VectorCollection" /> atual contém o <see cref="T:System.Windows.Vector" /> especificado.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Vector" /> a ser localizado no <see cref="T:System.Windows.Media.VectorCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Vector" /> for encontrado no <see cref="T:System.Windows.Media.VectorCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060028A5 RID: 10405 RVA: 0x000A2EF8 File Offset: 0x000A22F8
		public bool Contains(Vector value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Determina o índice do item especificado na <see cref="T:System.Windows.Media.VectorCollection" />.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Vector" /> a ser localizado no <see cref="T:System.Windows.Media.VectorCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na <see cref="T:System.Windows.Media.VectorCollection" />, caso contrário, -1.</returns>
		// Token: 0x060028A6 RID: 10406 RVA: 0x000A2F18 File Offset: 0x000A2318
		public int IndexOf(Vector value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Vector" /> no <see cref="T:System.Windows.Media.VectorCollection" /> no índice especificado.</summary>
		/// <param name="index">O índice de base zero no qual o <paramref name="value" /> deve ser inserido.</param>
		/// <param name="value">O <see cref="T:System.Windows.Vector" /> a ser inserido no <see cref="T:System.Windows.Media.VectorCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> não é um índice válido no <see cref="T:System.Windows.Media.VectorCollection" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.VectorCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.VectorCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x060028A7 RID: 10407 RVA: 0x000A2F38 File Offset: 0x000A2338
		public void Insert(int index, Vector value)
		{
			base.WritePreamble();
			this._collection.Insert(index, value);
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Remove a primeira ocorrência do <see cref="T:System.Windows.Vector" /> especificado do <see cref="T:System.Windows.Media.VectorCollection" />.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Vector" /> a ser removido de <see cref="T:System.Windows.Media.VectorCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> tiver sido removido da <see cref="T:System.Windows.Media.VectorCollection" />, caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.VectorCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.VectorCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x060028A8 RID: 10408 RVA: 0x000A2F6C File Offset: 0x000A236C
		public bool Remove(Vector value)
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

		/// <summary>Remove o <see cref="T:System.Windows.Vector" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero do <see cref="T:System.Windows.Vector" /> a ser removido.</param>
		// Token: 0x060028A9 RID: 10409 RVA: 0x000A2FB0 File Offset: 0x000A23B0
		public void RemoveAt(int index)
		{
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x000A2FCC File Offset: 0x000A23CC
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			base.WritePreamble();
			this._collection.RemoveAt(index);
			this._version += 1U;
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Vector" /> no índice especificado.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Vector" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Vector" /> no índice especificado.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> não é um índice válido no <see cref="T:System.Windows.Media.VectorCollection" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.VectorCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.VectorCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x170007CB RID: 1995
		public Vector this[int index]
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

		/// <summary>Obtém o número de itens contidos no <see cref="T:System.Windows.Media.VectorCollection" />.</summary>
		/// <returns>O número de itens no <see cref="T:System.Windows.Media.VectorCollection" />.</returns>
		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x060028AD RID: 10413 RVA: 0x000A3050 File Offset: 0x000A2450
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Copia os itens da <see cref="T:System.Windows.Media.VectorCollection" /> para uma matriz, começando no índice de matriz especificado.</summary>
		/// <param name="array">A matriz unidimensional que é o destino dos itens copiados do <see cref="T:System.Windows.Media.VectorCollection" />. A matriz deve ter indexação com base em zero.</param>
		/// <param name="index">O índice com base em zero em <paramref name="array" /> no qual a cópia começa.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> é multidimensional.  
		///
		/// ou - 
		/// O número de itens na origem <see cref="T:System.Windows.Media.VectorCollection" /> é maior do que o espaço disponível de <paramref name="index" /> até o final do <paramref name="array" /> de destino.</exception>
		// Token: 0x060028AE RID: 10414 RVA: 0x000A3070 File Offset: 0x000A2470
		public void CopyTo(Vector[] array, int index)
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

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x060028AF RID: 10415 RVA: 0x000A30C0 File Offset: 0x000A24C0
		bool ICollection<Vector>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Retorna um enumerador que pode ser iterado por meio de <see cref="T:System.Windows.Media.VectorCollection" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.VectorCollection.Enumerator" /> que pode ser usado para iterar por meio de <see cref="T:System.Windows.Media.VectorCollection" />.</returns>
		// Token: 0x060028B0 RID: 10416 RVA: 0x000A30DC File Offset: 0x000A24DC
		public VectorCollection.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new VectorCollection.Enumerator(this);
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x000A30F8 File Offset: 0x000A24F8
		IEnumerator<Vector> IEnumerable<Vector>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.VectorCollection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x000A3110 File Offset: 0x000A2510
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<Vector>)this).IsReadOnly;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.VectorCollection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x060028B3 RID: 10419 RVA: 0x000A3124 File Offset: 0x000A2524
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
		// Token: 0x170007D0 RID: 2000
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
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.VectorCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x060028B6 RID: 10422 RVA: 0x000A3178 File Offset: 0x000A2578
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.VectorCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.VectorCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060028B7 RID: 10423 RVA: 0x000A3194 File Offset: 0x000A2594
		bool IList.Contains(object value)
		{
			return value is Vector && this.Contains((Vector)value);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.VectorCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x060028B8 RID: 10424 RVA: 0x000A31B8 File Offset: 0x000A25B8
		int IList.IndexOf(object value)
		{
			if (value is Vector)
			{
				return this.IndexOf((Vector)value);
			}
			return -1;
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.VectorCollection" />.</param>
		// Token: 0x060028B9 RID: 10425 RVA: 0x000A31DC File Offset: 0x000A25DC
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.VectorCollection" />.</param>
		// Token: 0x060028BA RID: 10426 RVA: 0x000A31F8 File Offset: 0x000A25F8
		void IList.Remove(object value)
		{
			if (value is Vector)
			{
				this.Remove((Vector)value);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.VectorCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x060028BB RID: 10427 RVA: 0x000A321C File Offset: 0x000A261C
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
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.VectorCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x060028BC RID: 10428 RVA: 0x000A32F4 File Offset: 0x000A26F4
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.VectorCollection" />.</returns>
		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x060028BD RID: 10429 RVA: 0x000A331C File Offset: 0x000A271C
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
		// Token: 0x060028BE RID: 10430 RVA: 0x000A3330 File Offset: 0x000A2730
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x060028BF RID: 10431 RVA: 0x000A3348 File Offset: 0x000A2748
		internal static VectorCollection Empty
		{
			get
			{
				if (VectorCollection.s_empty == null)
				{
					VectorCollection vectorCollection = new VectorCollection();
					vectorCollection.Freeze();
					VectorCollection.s_empty = vectorCollection;
				}
				return VectorCollection.s_empty;
			}
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000A3374 File Offset: 0x000A2774
		internal Vector Internal_GetItem(int i)
		{
			return this._collection[i];
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x000A3390 File Offset: 0x000A2790
		private Vector Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is Vector))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"Vector"
				}));
			}
			return (Vector)value;
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x000A33F4 File Offset: 0x000A27F4
		private int AddHelper(Vector value)
		{
			int result = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			return result;
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x000A3410 File Offset: 0x000A2810
		internal int AddWithoutFiringPublicEvents(Vector value)
		{
			base.WritePreamble();
			int result = this._collection.Add(value);
			this._version += 1U;
			return result;
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x000A3444 File Offset: 0x000A2844
		protected override Freezable CreateInstanceCore()
		{
			return new VectorCollection();
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x000A3458 File Offset: 0x000A2858
		protected override void CloneCore(Freezable source)
		{
			VectorCollection vectorCollection = (VectorCollection)source;
			base.CloneCore(source);
			int count = vectorCollection._collection.Count;
			this._collection = new FrugalStructList<Vector>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(vectorCollection._collection[i]);
			}
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x000A34B0 File Offset: 0x000A28B0
		protected override void CloneCurrentValueCore(Freezable source)
		{
			VectorCollection vectorCollection = (VectorCollection)source;
			base.CloneCurrentValueCore(source);
			int count = vectorCollection._collection.Count;
			this._collection = new FrugalStructList<Vector>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(vectorCollection._collection[i]);
			}
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x000A3508 File Offset: 0x000A2908
		protected override void GetAsFrozenCore(Freezable source)
		{
			VectorCollection vectorCollection = (VectorCollection)source;
			base.GetAsFrozenCore(source);
			int count = vectorCollection._collection.Count;
			this._collection = new FrugalStructList<Vector>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(vectorCollection._collection[i]);
			}
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x000A3560 File Offset: 0x000A2960
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			VectorCollection vectorCollection = (VectorCollection)source;
			base.GetCurrentValueAsFrozenCore(source);
			int count = vectorCollection._collection.Count;
			this._collection = new FrugalStructList<Vector>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(vectorCollection._collection[i]);
			}
		}

		/// <summary>Cria uma representação de <see cref="T:System.String" /> deste <see cref="T:System.Windows.Media.VectorCollection" />.</summary>
		/// <returns>Retorna uma <see cref="T:System.String" /> que contém os valores <see cref="P:System.Windows.Vector.X" /> e <see cref="P:System.Windows.Vector.Y" /> das estruturas <see cref="T:System.Windows.Vector" /> nesta <see cref="T:System.Windows.Media.VectorCollection" />.</returns>
		// Token: 0x060028C9 RID: 10441 RVA: 0x000A35B8 File Offset: 0x000A29B8
		public override string ToString()
		{
			base.ReadPreamble();
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação de <see cref="T:System.String" /> deste <see cref="T:System.Windows.Media.VectorCollection" />.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura.</param>
		/// <returns>Retorna uma <see cref="T:System.String" /> que contém os valores <see cref="P:System.Windows.Vector.X" /> e <see cref="P:System.Windows.Vector.Y" /> das estruturas <see cref="T:System.Windows.Vector" /> nesta <see cref="T:System.Windows.Media.VectorCollection" />.</returns>
		// Token: 0x060028CA RID: 10442 RVA: 0x000A35D4 File Offset: 0x000A29D4
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
		// Token: 0x060028CB RID: 10443 RVA: 0x000A35F0 File Offset: 0x000A29F0
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(format, provider);
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x000A360C File Offset: 0x000A2A0C
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

		/// <summary>Converte uma representação <see cref="T:System.String" /> de uma coleção de vetores em uma <see cref="T:System.Windows.Media.VectorCollection" /> equivalente.</summary>
		/// <param name="source">A representação de <see cref="T:System.String" /> da coleção de vetores.</param>
		/// <returns>Retorna a <see cref="T:System.Windows.Media.VectorCollection" /> equivalente.</returns>
		// Token: 0x060028CD RID: 10445 RVA: 0x000A369C File Offset: 0x000A2A9C
		public static VectorCollection Parse(string source)
		{
			IFormatProvider invariantEnglishUS = TypeConverterHelper.InvariantEnglishUS;
			TokenizerHelper tokenizerHelper = new TokenizerHelper(source, invariantEnglishUS);
			VectorCollection vectorCollection = new VectorCollection();
			while (tokenizerHelper.NextToken())
			{
				Vector value = new Vector(Convert.ToDouble(tokenizerHelper.GetCurrentToken(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS));
				vectorCollection.Add(value);
			}
			return vectorCollection;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.VectorCollection" />.</summary>
		// Token: 0x060028CE RID: 10446 RVA: 0x000A36F0 File Offset: 0x000A2AF0
		public VectorCollection()
		{
			this._collection = default(FrugalStructList<Vector>);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.VectorCollection" /> com a capacidade especificada.</summary>
		/// <param name="capacity">O número de valores de <see cref="T:System.Windows.Vector" /> que a coleção é capaz de armazenar inicialmente.</param>
		// Token: 0x060028CF RID: 10447 RVA: 0x000A3710 File Offset: 0x000A2B10
		public VectorCollection(int capacity)
		{
			this._collection = new FrugalStructList<Vector>(capacity);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.VectorCollection" />, que contém os itens copiados da coleção especificada de valores <see cref="T:System.Windows.Vector" /> e tem a mesma capacidade inicial que o número de itens copiados.</summary>
		/// <param name="collection">A coleção cujos itens são copiados para o novo <see cref="T:System.Windows.Media.VectorCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> é <see langword="null" />.</exception>
		// Token: 0x060028D0 RID: 10448 RVA: 0x000A3730 File Offset: 0x000A2B30
		public VectorCollection(IEnumerable<Vector> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				ICollection<Vector> collection2 = collection as ICollection<Vector>;
				if (collection2 != null)
				{
					this._collection = new FrugalStructList<Vector>(collection2);
				}
				else
				{
					ICollection collection3 = collection as ICollection;
					if (collection3 != null)
					{
						this._collection = new FrugalStructList<Vector>(collection3);
					}
					else
					{
						this._collection = default(FrugalStructList<Vector>);
						foreach (Vector value in collection)
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

		// Token: 0x040012A0 RID: 4768
		private static VectorCollection s_empty;

		// Token: 0x040012A1 RID: 4769
		internal FrugalStructList<Vector> _collection;

		// Token: 0x040012A2 RID: 4770
		internal uint _version;

		/// <summary>Enumera itens <see cref="T:System.Windows.Vector" /> em um <see cref="T:System.Windows.Media.VectorCollection" />.</summary>
		// Token: 0x02000883 RID: 2179
		public struct Enumerator : IEnumerator, IEnumerator<Vector>, IDisposable
		{
			// Token: 0x060057C9 RID: 22473 RVA: 0x00166BC8 File Offset: 0x00165FC8
			internal Enumerator(VectorCollection list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = default(Vector);
			}

			/// <summary>Este tipo ou membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x060057CA RID: 22474 RVA: 0x00166BFC File Offset: 0x00165FFC
			void IDisposable.Dispose()
			{
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador foi avançado com êxito para o próximo elemento; caso contrário, <see langword="false" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Windows.Media.VectorCollection" /> foi modificada depois da criação do enumerador.</exception>
			// Token: 0x060057CB RID: 22475 RVA: 0x00166C0C File Offset: 0x0016600C
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					VectorCollection list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._current = list._collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Redefine o enumerador com sua posição inicial, que é antes do primeiro elemento da coleção.</summary>
			/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Windows.Media.VectorCollection" /> foi modificada depois da criação do enumerador.</exception>
			// Token: 0x060057CC RID: 22476 RVA: 0x00166CA0 File Offset: 0x001660A0
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
			// Token: 0x17001220 RID: 4640
			// (get) Token: 0x060057CD RID: 22477 RVA: 0x00166CE4 File Offset: 0x001660E4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém o elemento atual na coleção.</summary>
			/// <returns>O elemento atual na coleção.</returns>
			/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Windows.Media.VectorCollection" /> foi modificada depois da criação do enumerador.</exception>
			// Token: 0x17001221 RID: 4641
			// (get) Token: 0x060057CE RID: 22478 RVA: 0x00166CFC File Offset: 0x001660FC
			public Vector Current
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

			// Token: 0x040028BA RID: 10426
			private Vector _current;

			// Token: 0x040028BB RID: 10427
			private VectorCollection _list;

			// Token: 0x040028BC RID: 10428
			private uint _version;

			// Token: 0x040028BD RID: 10429
			private int _index;
		}
	}
}
