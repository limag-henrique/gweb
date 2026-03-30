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
	/// <summary>Representa uma coleção ordenada de objetos de <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
	// Token: 0x0200046E RID: 1134
	[ValueSerializer(typeof(Point3DCollectionValueSerializer))]
	[TypeConverter(typeof(Point3DCollectionConverter))]
	public sealed class Point3DCollection : Freezable, IFormattable, IList, ICollection, IEnumerable, IList<Point3D>, ICollection<Point3D>, IEnumerable<Point3D>
	{
		// Token: 0x06002FEE RID: 12270 RVA: 0x000BFCFC File Offset: 0x000BF0FC
		[FriendAccessAllowed]
		internal static object DeserializeFrom(BinaryReader reader)
		{
			uint num = reader.ReadUInt32();
			Point3DCollection point3DCollection = new Point3DCollection((int)num);
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				Point3D value = new Point3D(XamlSerializationHelper.ReadDouble(reader), XamlSerializationHelper.ReadDouble(reader), XamlSerializationHelper.ReadDouble(reader));
				point3DCollection.Add(value);
			}
			return point3DCollection;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002FEF RID: 12271 RVA: 0x000BFD44 File Offset: 0x000BF144
		public new Point3DCollection Clone()
		{
			return (Point3DCollection)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002FF0 RID: 12272 RVA: 0x000BFD5C File Offset: 0x000BF15C
		public new Point3DCollection CloneCurrentValue()
		{
			return (Point3DCollection)base.CloneCurrentValue();
		}

		/// <summary>Adiciona um objeto <see cref="T:System.Windows.Media.Media3D.Point3D" /> ao final da <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</summary>
		/// <param name="value">O item a ser adicionado ao fim dessa coleção.</param>
		// Token: 0x06002FF1 RID: 12273 RVA: 0x000BFD74 File Offset: 0x000BF174
		public void Add(Point3D value)
		{
			this.AddHelper(value);
		}

		/// <summary>Remove todos os itens desta <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</summary>
		// Token: 0x06002FF2 RID: 12274 RVA: 0x000BFD8C File Offset: 0x000BF18C
		public void Clear()
		{
			base.WritePreamble();
			this._collection.Clear();
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Media.Media3D.Point3D" /> especificado está neste <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</summary>
		/// <param name="value">O item a ser localizado nesta coleção.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" />, o Point3D especificado, estiver neste Point3DCollection; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002FF3 RID: 12275 RVA: 0x000BFDC0 File Offset: 0x000BF1C0
		public bool Contains(Point3D value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Obtém a posição de índice da primeira ocorrência do <see cref="T:System.Windows.Media.Media3D.Point3D" /> especificado.</summary>
		/// <param name="value">O Point3D a ser pesquisado.</param>
		/// <returns>A posição do índice do Point3D especificado.</returns>
		// Token: 0x06002FF4 RID: 12276 RVA: 0x000BFDE0 File Offset: 0x000BF1E0
		public int IndexOf(Point3D value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Media3D.Point3D" /> nesta <see cref="T:System.Windows.Media.Media3D.Point3DCollection" /> na posição do índice especificada.</summary>
		/// <param name="index">A posição do índice na qual inserir o Point3D especificado.</param>
		/// <param name="value">O Point3D a ser inserido.</param>
		// Token: 0x06002FF5 RID: 12277 RVA: 0x000BFE00 File Offset: 0x000BF200
		public void Insert(int index, Point3D value)
		{
			base.WritePreamble();
			this._collection.Insert(index, value);
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Remove a primeira ocorrência do <see cref="T:System.Windows.Media.Media3D.Point3D" /> especificado do <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</summary>
		/// <param name="value">O Point3D a ser removido desta coleção.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> tiver sido removido da coleção; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002FF6 RID: 12278 RVA: 0x000BFE34 File Offset: 0x000BF234
		public bool Remove(Point3D value)
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

		/// <summary>Remove o <see cref="T:System.Windows.Media.Media3D.Point3D" /> na posição de índice especificada da <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</summary>
		/// <param name="index">A posição do índice do Point3D a ser removido.</param>
		// Token: 0x06002FF7 RID: 12279 RVA: 0x000BFE78 File Offset: 0x000BF278
		public void RemoveAt(int index)
		{
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x000BFE94 File Offset: 0x000BF294
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			base.WritePreamble();
			this._collection.RemoveAt(index);
			this._version += 1U;
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Media3D.Point3D" /> no índice de base zero especificado.</summary>
		/// <param name="index">O índice de base zero do objeto Point3D a ser obtido ou definido.</param>
		/// <returns>O item no índice especificado.</returns>
		// Token: 0x170009CB RID: 2507
		public Point3D this[int index]
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

		/// <summary>Obtém o número de objetos <see cref="T:System.Windows.Media.Media3D.Point3D" /> contidos no <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</summary>
		/// <returns>O número de <see cref="T:System.Windows.Media.Media3D.Point3D" /> objetos contidos no <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</returns>
		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06002FFB RID: 12283 RVA: 0x000BFF18 File Offset: 0x000BF318
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Copia os itens desse <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />, começando com o valor de índice especificado, em uma matriz de objetos <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <param name="array">A matriz que é o destino dos itens copiados deste Point3DCollection.</param>
		/// <param name="index">O índice no qual a cópia é iniciada.</param>
		// Token: 0x06002FFC RID: 12284 RVA: 0x000BFF38 File Offset: 0x000BF338
		public void CopyTo(Point3D[] array, int index)
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

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06002FFD RID: 12285 RVA: 0x000BFF88 File Offset: 0x000BF388
		bool ICollection<Point3D>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Retorna um enumerador que pode iterar pela coleção.</summary>
		/// <returns>Um enumerador que pode iterar por meio da coleção.</returns>
		// Token: 0x06002FFE RID: 12286 RVA: 0x000BFFA4 File Offset: 0x000BF3A4
		public Point3DCollection.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new Point3DCollection.Enumerator(this);
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x000BFFC0 File Offset: 0x000BF3C0
		IEnumerator<Point3D> IEnumerable<Point3D>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.Media3D.Point3DCollection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06003000 RID: 12288 RVA: 0x000BFFD8 File Offset: 0x000BF3D8
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<Point3D>)this).IsReadOnly;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</returns>
		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06003001 RID: 12289 RVA: 0x000BFFEC File Offset: 0x000BF3EC
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
		// Token: 0x170009D0 RID: 2512
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
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x06003004 RID: 12292 RVA: 0x000C0040 File Offset: 0x000BF440
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003005 RID: 12293 RVA: 0x000C005C File Offset: 0x000BF45C
		bool IList.Contains(object value)
		{
			return value is Point3D && this.Contains((Point3D)value);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06003006 RID: 12294 RVA: 0x000C0080 File Offset: 0x000BF480
		int IList.IndexOf(object value)
		{
			if (value is Point3D)
			{
				return this.IndexOf((Point3D)value);
			}
			return -1;
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</param>
		// Token: 0x06003007 RID: 12295 RVA: 0x000C00A4 File Offset: 0x000BF4A4
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</param>
		// Token: 0x06003008 RID: 12296 RVA: 0x000C00C0 File Offset: 0x000BF4C0
		void IList.Remove(object value)
		{
			if (value is Point3D)
			{
				this.Remove((Point3D)value);
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x06003009 RID: 12297 RVA: 0x000C00E4 File Offset: 0x000BF4E4
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
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.Media3D.Point3DCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x0600300A RID: 12298 RVA: 0x000C01BC File Offset: 0x000BF5BC
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</returns>
		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x0600300B RID: 12299 RVA: 0x000C01E4 File Offset: 0x000BF5E4
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
		// Token: 0x0600300C RID: 12300 RVA: 0x000C01F8 File Offset: 0x000BF5F8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x0600300D RID: 12301 RVA: 0x000C0210 File Offset: 0x000BF610
		internal static Point3DCollection Empty
		{
			get
			{
				if (Point3DCollection.s_empty == null)
				{
					Point3DCollection point3DCollection = new Point3DCollection();
					point3DCollection.Freeze();
					Point3DCollection.s_empty = point3DCollection;
				}
				return Point3DCollection.s_empty;
			}
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x000C023C File Offset: 0x000BF63C
		internal Point3D Internal_GetItem(int i)
		{
			return this._collection[i];
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x000C0258 File Offset: 0x000BF658
		private Point3D Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is Point3D))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"Point3D"
				}));
			}
			return (Point3D)value;
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000C02BC File Offset: 0x000BF6BC
		private int AddHelper(Point3D value)
		{
			int result = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			return result;
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000C02D8 File Offset: 0x000BF6D8
		internal int AddWithoutFiringPublicEvents(Point3D value)
		{
			base.WritePreamble();
			int result = this._collection.Add(value);
			this._version += 1U;
			return result;
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x000C030C File Offset: 0x000BF70C
		protected override Freezable CreateInstanceCore()
		{
			return new Point3DCollection();
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x000C0320 File Offset: 0x000BF720
		protected override void CloneCore(Freezable source)
		{
			Point3DCollection point3DCollection = (Point3DCollection)source;
			base.CloneCore(source);
			int count = point3DCollection._collection.Count;
			this._collection = new FrugalStructList<Point3D>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(point3DCollection._collection[i]);
			}
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x000C0378 File Offset: 0x000BF778
		protected override void CloneCurrentValueCore(Freezable source)
		{
			Point3DCollection point3DCollection = (Point3DCollection)source;
			base.CloneCurrentValueCore(source);
			int count = point3DCollection._collection.Count;
			this._collection = new FrugalStructList<Point3D>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(point3DCollection._collection[i]);
			}
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x000C03D0 File Offset: 0x000BF7D0
		protected override void GetAsFrozenCore(Freezable source)
		{
			Point3DCollection point3DCollection = (Point3DCollection)source;
			base.GetAsFrozenCore(source);
			int count = point3DCollection._collection.Count;
			this._collection = new FrugalStructList<Point3D>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(point3DCollection._collection[i]);
			}
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x000C0428 File Offset: 0x000BF828
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			Point3DCollection point3DCollection = (Point3DCollection)source;
			base.GetCurrentValueAsFrozenCore(source);
			int count = point3DCollection._collection.Count;
			this._collection = new FrugalStructList<Point3D>(count);
			for (int i = 0; i < count; i++)
			{
				this._collection.Add(point3DCollection._collection[i]);
			}
		}

		/// <summary>Cria uma representação de cadeia de caracteres desse <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</summary>
		/// <returns>Representação de cadeia de caracteres do objeto.</returns>
		// Token: 0x06003017 RID: 12311 RVA: 0x000C0480 File Offset: 0x000BF880
		public override string ToString()
		{
			base.ReadPreamble();
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação de cadeia de caracteres desse <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura.</param>
		/// <returns>Representação de cadeia de caracteres do objeto.</returns>
		// Token: 0x06003018 RID: 12312 RVA: 0x000C049C File Offset: 0x000BF89C
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
		// Token: 0x06003019 RID: 12313 RVA: 0x000C04B8 File Offset: 0x000BF8B8
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(format, provider);
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x000C04D4 File Offset: 0x000BF8D4
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

		/// <summary>Converte uma representação de cadeia de caracteres de uma coleção de objetos <see cref="T:System.Windows.Media.Media3D.Point3D" /> em um <see cref="T:System.Windows.Media.Media3D.Point3DCollection" /> equivalente.</summary>
		/// <param name="source">A representação de cadeia de caracteres da coleção de objetos Point3D.</param>
		/// <returns>O Point3DCollection equivalente.</returns>
		// Token: 0x0600301B RID: 12315 RVA: 0x000C0564 File Offset: 0x000BF964
		public static Point3DCollection Parse(string source)
		{
			IFormatProvider invariantEnglishUS = TypeConverterHelper.InvariantEnglishUS;
			TokenizerHelper tokenizerHelper = new TokenizerHelper(source, invariantEnglishUS);
			Point3DCollection point3DCollection = new Point3DCollection();
			while (tokenizerHelper.NextToken())
			{
				Point3D value = new Point3D(Convert.ToDouble(tokenizerHelper.GetCurrentToken(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS));
				point3DCollection.Add(value);
			}
			return point3DCollection;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</summary>
		// Token: 0x0600301C RID: 12316 RVA: 0x000C05C4 File Offset: 0x000BF9C4
		public Point3DCollection()
		{
			this._collection = default(FrugalStructList<Point3D>);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.Point3DCollection" /> com a capacidade especificada.</summary>
		/// <param name="capacity">Inteiro que especifica a capacidade do Point3DCollection.</param>
		// Token: 0x0600301D RID: 12317 RVA: 0x000C05E4 File Offset: 0x000BF9E4
		public Point3DCollection(int capacity)
		{
			this._collection = new FrugalStructList<Point3D>(capacity);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.Point3DCollection" /> usando a coleção especificada.</summary>
		/// <param name="collection">Coleção com a qual criar uma instância de Point3DCollection.</param>
		// Token: 0x0600301E RID: 12318 RVA: 0x000C0604 File Offset: 0x000BFA04
		public Point3DCollection(IEnumerable<Point3D> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				ICollection<Point3D> collection2 = collection as ICollection<Point3D>;
				if (collection2 != null)
				{
					this._collection = new FrugalStructList<Point3D>(collection2);
				}
				else
				{
					ICollection collection3 = collection as ICollection;
					if (collection3 != null)
					{
						this._collection = new FrugalStructList<Point3D>(collection3);
					}
					else
					{
						this._collection = default(FrugalStructList<Point3D>);
						foreach (Point3D value in collection)
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

		// Token: 0x04001542 RID: 5442
		private static Point3DCollection s_empty;

		// Token: 0x04001543 RID: 5443
		internal FrugalStructList<Point3D> _collection;

		// Token: 0x04001544 RID: 5444
		internal uint _version;

		/// <summary>Enumera itens em uma <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</summary>
		// Token: 0x020008A8 RID: 2216
		public struct Enumerator : IEnumerator, IEnumerator<Point3D>, IDisposable
		{
			// Token: 0x0600586C RID: 22636 RVA: 0x001678D4 File Offset: 0x00166CD4
			internal Enumerator(Point3DCollection list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = default(Point3D);
			}

			/// <summary>Este membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x0600586D RID: 22637 RVA: 0x00167908 File Offset: 0x00166D08
			void IDisposable.Dispose()
			{
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador foi avançado com êxito para o próximo elemento; caso contrário, <see langword="false" />.</returns>
			// Token: 0x0600586E RID: 22638 RVA: 0x00167918 File Offset: 0x00166D18
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					Point3DCollection list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._current = list._collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Redefine o enumerador com sua posição inicial, que é antes do primeiro elemento da coleção.</summary>
			// Token: 0x0600586F RID: 22639 RVA: 0x001679AC File Offset: 0x00166DAC
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
			// Token: 0x17001238 RID: 4664
			// (get) Token: 0x06005870 RID: 22640 RVA: 0x001679F0 File Offset: 0x00166DF0
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém o <see cref="T:System.Windows.Media.Media3D.Point3D" /> atual na coleção.</summary>
			/// <returns>
			///   <see cref="T:System.Windows.Media.Media3D.Point3D" /> na posição atual na coleção.</returns>
			// Token: 0x17001239 RID: 4665
			// (get) Token: 0x06005871 RID: 22641 RVA: 0x00167A08 File Offset: 0x00166E08
			public Point3D Current
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

			// Token: 0x040028FB RID: 10491
			private Point3D _current;

			// Token: 0x040028FC RID: 10492
			private Point3DCollection _list;

			// Token: 0x040028FD RID: 10493
			private uint _version;

			// Token: 0x040028FE RID: 10494
			private int _index;
		}
	}
}
