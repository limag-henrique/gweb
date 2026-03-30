using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Markup;
using System.Windows.Media.Media3D.Converters;
using MS.Internal;
using MS.Internal.Media;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media.Media3D
{
	/// <summary>Coleção de objetos <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
	// Token: 0x0200046F RID: 1135
	[ValueSerializer(typeof(Vector3DCollectionValueSerializer))]
	[TypeConverter(typeof(Vector3DCollectionConverter))]
	public sealed class Vector3DCollection : Freezable, IFormattable, IList, ICollection, IEnumerable, IList<Vector3D>, ICollection<Vector3D>, IEnumerable<Vector3D>
	{
		// Token: 0x0600301F RID: 12319 RVA: 0x000C06C0 File Offset: 0x000BFAC0
		[FriendAccessAllowed]
		internal static object DeserializeFrom(BinaryReader reader)
		{
			uint num = reader.ReadUInt32();
			Vector3DCollection vector3DCollection = new Vector3DCollection((int)num);
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				Vector3D value = new Vector3D(XamlSerializationHelper.ReadDouble(reader), XamlSerializationHelper.ReadDouble(reader), XamlSerializationHelper.ReadDouble(reader));
				vector3DCollection.Add(value);
			}
			return vector3DCollection;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003020 RID: 12320 RVA: 0x000C0708 File Offset: 0x000BFB08
		public new Vector3DCollection Clone()
		{
			return (Vector3DCollection)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06003021 RID: 12321 RVA: 0x000C0720 File Offset: 0x000BFB20
		public new Vector3DCollection CloneCurrentValue()
		{
			return (Vector3DCollection)base.CloneCurrentValue();
		}

		/// <summary>Adiciona um Vector3D à coleção.</summary>
		/// <param name="value">Vector3D a ser adicionado à coleção.</param>
		// Token: 0x06003022 RID: 12322 RVA: 0x000C0738 File Offset: 0x000BFB38
		public void Add(Vector3D value)
		{
			this.AddHelper(value);
		}

		/// <summary>Remove todos os elementos da coleção.</summary>
		// Token: 0x06003023 RID: 12323 RVA: 0x000C0750 File Offset: 0x000BFB50
		public void Clear()
		{
			base.WritePreamble();
			this._collection.Clear();
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Determina se a coleção contém o elemento especificado.</summary>
		/// <param name="value">Vector3D a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> for encontrado na coleção; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003024 RID: 12324 RVA: 0x000C0784 File Offset: 0x000BFB84
		public bool Contains(Vector3D value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Obtém a posição de índice da primeira ocorrência do <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificado.</summary>
		/// <param name="value">O item a ser procurado.</param>
		/// <returns>A posição de índice do Vector3D especificado.</returns>
		// Token: 0x06003025 RID: 12325 RVA: 0x000C07A4 File Offset: 0x000BFBA4
		public int IndexOf(Vector3D value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Insere um objeto <see cref="T:System.Windows.Media.Media3D.Vector3D" /> nesta <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" /> na posição de índice especificada.</summary>
		/// <param name="index">A posição do índice na qual inserir <paramref name="value" />, o Vector3D especificado.</param>
		/// <param name="value">O item a ser inserido.</param>
		// Token: 0x06003026 RID: 12326 RVA: 0x000C07C4 File Offset: 0x000BFBC4
		public void Insert(int index, Vector3D value)
		{
			base.WritePreamble();
			this._collection.Insert(index, value);
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Remove a primeira ocorrência do <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificado do <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" />.</summary>
		/// <param name="value">O Vector3D a ser removido desta coleção.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> tiver sido removido da coleção; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003027 RID: 12327 RVA: 0x000C07F8 File Offset: 0x000BFBF8
		public bool Remove(Vector3D value)
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

		/// <summary>Remove o <see cref="T:System.Windows.Media.Media3D.Vector3D" /> na posição de índice especificada da <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" />.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a ser removida.</param>
		// Token: 0x06003028 RID: 12328 RVA: 0x000C083C File Offset: 0x000BFC3C
		public void RemoveAt(int index)
		{
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x000C0858 File Offset: 0x000BFC58
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			base.WritePreamble();
			this._collection.RemoveAt(index);
			this._version += 1U;
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Media3D.Vector3D" /> no índice de base zero especificado.</summary>
		/// <param name="index">O índice de base zero do objeto Vector3D a ser obtido ou definido.</param>
		/// <returns>O item no índice especificado.</returns>
		// Token: 0x170009D4 RID: 2516
		public Vector3D this[int index]
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

		/// <summary>Obtém o número de objetos <see cref="T:System.Windows.Media.Media3D.Vector3D" /> contidos no <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" />.</summary>
		/// <returns>O número de <see cref="T:System.Windows.Media.Media3D.Vector3D" /> objetos contidos no <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" />.</returns>
		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x0600302C RID: 12332 RVA: 0x000C08DC File Offset: 0x000BFCDC
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Copia os elementos da coleção na matriz especificada, iniciando na posição de índice especificada.</summary>
		/// <param name="array">Matriz de destino da cópia.</param>
		/// <param name="index">Posição de destino da cópia.</param>
		// Token: 0x0600302D RID: 12333 RVA: 0x000C08FC File Offset: 0x000BFCFC
		public void CopyTo(Vector3D[] array, int index)
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

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x0600302E RID: 12334 RVA: 0x000C094C File Offset: 0x000BFD4C
		bool ICollection<Vector3D>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Retorna um enumerador que pode iterar pela coleção.</summary>
		/// <returns>Um enumerador que pode iterar por meio da coleção.</returns>
		// Token: 0x0600302F RID: 12335 RVA: 0x000C0968 File Offset: 0x000BFD68
		public Vector3DCollection.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new Vector3DCollection.Enumerator(this);
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x000C0984 File Offset: 0x000BFD84
		IEnumerator<Vector3D> IEnumerable<Vector3D>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06003031 RID: 12337 RVA: 0x000C099C File Offset: 0x000BFD9C
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<Vector3D>)this).IsReadOnly;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06003032 RID: 12338 RVA: 0x000C09B0 File Offset: 0x000BFDB0
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
		// Token: 0x170009D9 RID: 2521
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
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x06003035 RID: 12341 RVA: 0x000C0A04 File Offset: 0x000BFE04
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003036 RID: 12342 RVA: 0x000C0A20 File Offset: 0x000BFE20
		bool IList.Contains(object value)
		{
			return value is Vector3D && this.Contains((Vector3D)value);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06003037 RID: 12343 RVA: 0x000C0A44 File Offset: 0x000BFE44
		int IList.IndexOf(object value)
		{
			if (value is Vector3D)
			{
				return this.IndexOf((Vector3D)value);
			}
			return -1;
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" />.</param>
		// Token: 0x06003038 RID: 12344 RVA: 0x000C0A68 File Offset: 0x000BFE68
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" />.</param>
		// Token: 0x06003039 RID: 12345 RVA: 0x000C0A84 File Offset: 0x000BFE84
		void IList.Remove(object value)
		{
			if (value is Vector3D)
			{
				this.Remove((Vector3D)value);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x0600303A RID: 12346 RVA: 0x000C0AA8 File Offset: 0x000BFEA8
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
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x0600303B RID: 12347 RVA: 0x000C0B80 File Offset: 0x000BFF80
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" />.</returns>
		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x0600303C RID: 12348 RVA: 0x000C0BA8 File Offset: 0x000BFFA8
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
		// Token: 0x0600303D RID: 12349 RVA: 0x000C0BBC File Offset: 0x000BFFBC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x0600303E RID: 12350 RVA: 0x000C0BD4 File Offset: 0x000BFFD4
		internal static Vector3DCollection Empty
		{
			get
			{
				if (Vector3DCollection.s_empty == null)
				{
					Vector3DCollection vector3DCollection = new Vector3DCollection();
					vector3DCollection.Freeze();
					Vector3DCollection.s_empty = vector3DCollection;
				}
				return Vector3DCollection.s_empty;
			}
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x000C0C00 File Offset: 0x000C0000
		internal Vector3D Internal_GetItem(int i)
		{
			return this._collection[i];
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x000C0C1C File Offset: 0x000C001C
		private Vector3D Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is Vector3D))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"Vector3D"
				}));
			}
			return (Vector3D)value;
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x000C0C80 File Offset: 0x000C0080
		private int AddHelper(Vector3D value)
		{
			int result = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			return result;
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x000C0C9C File Offset: 0x000C009C
		internal int AddWithoutFiringPublicEvents(Vector3D value)
		{
			base.WritePreamble();
			int result = this._collection.Add(value);
			this._version += 1U;
			return result;
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x000C0CD0 File Offset: 0x000C00D0
		protected override Freezable CreateInstanceCore()
		{
			return new Vector3DCollection();
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x000C0CE4 File Offset: 0x000C00E4
		protected override void CloneCore(Freezable source)
		{
			Vector3DCollection vector3DCollection = (Vector3DCollection)source;
			base.CloneCore(source);
			int count = vector3DCollection._collection.Count;
			this._collection = new FrugalStructList<Vector3D>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(vector3DCollection._collection[i]);
			}
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x000C0D3C File Offset: 0x000C013C
		protected override void CloneCurrentValueCore(Freezable source)
		{
			Vector3DCollection vector3DCollection = (Vector3DCollection)source;
			base.CloneCurrentValueCore(source);
			int count = vector3DCollection._collection.Count;
			this._collection = new FrugalStructList<Vector3D>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(vector3DCollection._collection[i]);
			}
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x000C0D94 File Offset: 0x000C0194
		protected override void GetAsFrozenCore(Freezable source)
		{
			Vector3DCollection vector3DCollection = (Vector3DCollection)source;
			base.GetAsFrozenCore(source);
			int count = vector3DCollection._collection.Count;
			this._collection = new FrugalStructList<Vector3D>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(vector3DCollection._collection[i]);
			}
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x000C0DEC File Offset: 0x000C01EC
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			Vector3DCollection vector3DCollection = (Vector3DCollection)source;
			base.GetCurrentValueAsFrozenCore(source);
			int count = vector3DCollection._collection.Count;
			this._collection = new FrugalStructList<Vector3D>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(vector3DCollection._collection[i]);
			}
		}

		/// <summary>Cria uma representação de cadeia de caracteres desse <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" />.</summary>
		/// <returns>Representação de cadeia de caracteres do objeto.</returns>
		// Token: 0x06003048 RID: 12360 RVA: 0x000C0E44 File Offset: 0x000C0244
		public override string ToString()
		{
			base.ReadPreamble();
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação de cadeia de caracteres desse <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" />.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura.</param>
		/// <returns>Representação de cadeia de caracteres do objeto.</returns>
		// Token: 0x06003049 RID: 12361 RVA: 0x000C0E60 File Offset: 0x000C0260
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
		// Token: 0x0600304A RID: 12362 RVA: 0x000C0E7C File Offset: 0x000C027C
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(format, provider);
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x000C0E98 File Offset: 0x000C0298
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

		/// <summary>Converte uma representação de cadeia de caracteres de uma coleção de objetos <see cref="T:System.Windows.Media.Media3D.Vector3D" /> em um <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" /> equivalente.</summary>
		/// <param name="source">A representação de cadeia de caracteres da coleção de objetos Vector3D.</param>
		/// <returns>Retorna o Vector3DCollection equivalente.</returns>
		// Token: 0x0600304C RID: 12364 RVA: 0x000C0F28 File Offset: 0x000C0328
		public static Vector3DCollection Parse(string source)
		{
			IFormatProvider invariantEnglishUS = TypeConverterHelper.InvariantEnglishUS;
			TokenizerHelper tokenizerHelper = new TokenizerHelper(source, invariantEnglishUS);
			Vector3DCollection vector3DCollection = new Vector3DCollection();
			while (tokenizerHelper.NextToken())
			{
				Vector3D value = new Vector3D(Convert.ToDouble(tokenizerHelper.GetCurrentToken(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS));
				vector3DCollection.Add(value);
			}
			return vector3DCollection;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" />.</summary>
		// Token: 0x0600304D RID: 12365 RVA: 0x000C0F88 File Offset: 0x000C0388
		public Vector3DCollection()
		{
			this._collection = default(FrugalStructList<Vector3D>);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" /> com a capacidade especificada.</summary>
		/// <param name="capacity">Inteiro que especifica a capacidade da coleção.</param>
		// Token: 0x0600304E RID: 12366 RVA: 0x000C0FA8 File Offset: 0x000C03A8
		public Vector3DCollection(int capacity)
		{
			this._collection = new FrugalStructList<Vector3D>(capacity);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" /> usando a coleção especificada.</summary>
		/// <param name="collection">Coleção com a qual será criada uma instância de Vector3DCollection.</param>
		// Token: 0x0600304F RID: 12367 RVA: 0x000C0FC8 File Offset: 0x000C03C8
		public Vector3DCollection(IEnumerable<Vector3D> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				ICollection<Vector3D> collection2 = collection as ICollection<Vector3D>;
				if (collection2 != null)
				{
					this._collection = new FrugalStructList<Vector3D>(collection2);
				}
				else
				{
					ICollection collection3 = collection as ICollection;
					if (collection3 != null)
					{
						this._collection = new FrugalStructList<Vector3D>(collection3);
					}
					else
					{
						this._collection = default(FrugalStructList<Vector3D>);
						foreach (Vector3D value in collection)
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

		// Token: 0x04001545 RID: 5445
		private static Vector3DCollection s_empty;

		// Token: 0x04001546 RID: 5446
		internal FrugalStructList<Vector3D> _collection;

		// Token: 0x04001547 RID: 5447
		internal uint _version;

		/// <summary>Enumera itens <see cref="T:System.Windows.Vector" /> em um <see cref="T:System.Windows.Media.VectorCollection" />.</summary>
		// Token: 0x020008A9 RID: 2217
		public struct Enumerator : IEnumerator, IEnumerator<Vector3D>, IDisposable
		{
			// Token: 0x06005872 RID: 22642 RVA: 0x00167A50 File Offset: 0x00166E50
			internal Enumerator(Vector3DCollection list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = default(Vector3D);
			}

			/// <summary>Este membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x06005873 RID: 22643 RVA: 0x00167A84 File Offset: 0x00166E84
			void IDisposable.Dispose()
			{
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador foi avançado com êxito para o próximo elemento; caso contrário, <see langword="false" />.</returns>
			// Token: 0x06005874 RID: 22644 RVA: 0x00167A94 File Offset: 0x00166E94
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					Vector3DCollection list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._current = list._collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Redefine o enumerador com sua posição inicial, que é antes do primeiro elemento da coleção.</summary>
			// Token: 0x06005875 RID: 22645 RVA: 0x00167B28 File Offset: 0x00166F28
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
			// Token: 0x1700123A RID: 4666
			// (get) Token: 0x06005876 RID: 22646 RVA: 0x00167B6C File Offset: 0x00166F6C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém o elemento atual na coleção.</summary>
			/// <returns>O elemento atual na coleção.</returns>
			// Token: 0x1700123B RID: 4667
			// (get) Token: 0x06005877 RID: 22647 RVA: 0x00167B84 File Offset: 0x00166F84
			public Vector3D Current
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

			// Token: 0x040028FF RID: 10495
			private Vector3D _current;

			// Token: 0x04002900 RID: 10496
			private Vector3DCollection _list;

			// Token: 0x04002901 RID: 10497
			private uint _version;

			// Token: 0x04002902 RID: 10498
			private int _index;
		}
	}
}
