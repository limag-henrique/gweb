using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media.Media3D
{
	/// <summary>Representa uma coleção ordenada de objetos de <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" />.</summary>
	// Token: 0x02000489 RID: 1161
	public sealed class GeneralTransform3DCollection : Animatable, IList, ICollection, IEnumerable, IList<GeneralTransform3D>, ICollection<GeneralTransform3D>, IEnumerable<GeneralTransform3D>
	{
		/// <summary>Cria um clone modificável dessa coleção, fazendo cópias em profundidade dos valores desse objeto.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true." /></returns>
		// Token: 0x06003341 RID: 13121 RVA: 0x000CC018 File Offset: 0x000CB418
		public new GeneralTransform3DCollection Clone()
		{
			return (GeneralTransform3DCollection)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto de coleção, fazendo cópias em profundidade dos valores atuais do objeto. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true" />.</returns>
		// Token: 0x06003342 RID: 13122 RVA: 0x000CC030 File Offset: 0x000CB430
		public new GeneralTransform3DCollection CloneCurrentValue()
		{
			return (GeneralTransform3DCollection)base.CloneCurrentValue();
		}

		/// <summary>Adiciona um objeto <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" /> ao fim da coleção.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" /> a adicionar ao final da coleção.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">A coleção é somente leitura.  
		///
		/// ou - 
		/// A coleção tem um tamanho fixo.</exception>
		// Token: 0x06003343 RID: 13123 RVA: 0x000CC048 File Offset: 0x000CB448
		public void Add(GeneralTransform3D value)
		{
			this.AddHelper(value);
		}

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" /> da coleção.</summary>
		/// <exception cref="T:System.NotSupportedException">A coleção é somente leitura.</exception>
		// Token: 0x06003344 RID: 13124 RVA: 0x000CC060 File Offset: 0x000CB460
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

		/// <summary>Indica se a coleção contém o objeto <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" /> especificado.</summary>
		/// <param name="value">O objeto <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" /> a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="value" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003345 RID: 13125 RVA: 0x000CC0C0 File Offset: 0x000CB4C0
		public bool Contains(GeneralTransform3D value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Procura o objeto <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" /> especificado dentro da coleção.</summary>
		/// <param name="value">O objeto a ser localizado.</param>
		/// <returns>A posição do índice baseado em zero de <paramref name="value" /> ou -1, se o objeto não for encontrado na coleção.</returns>
		// Token: 0x06003346 RID: 13126 RVA: 0x000CC0E0 File Offset: 0x000CB4E0
		public int IndexOf(GeneralTransform3D value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Insere um objeto <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" /> na posição de índice especificada na coleção.</summary>
		/// <param name="index">A posição de índice de base zero para inserir o objeto.</param>
		/// <param name="value">O objeto a ser inserido.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> não é um índice válido na coleção.</exception>
		/// <exception cref="T:System.NotSupportedException">A coleção é somente leitura.  
		///
		/// ou - 
		/// A coleção tem um tamanho fixo.</exception>
		// Token: 0x06003347 RID: 13127 RVA: 0x000CC100 File Offset: 0x000CB500
		public void Insert(int index, GeneralTransform3D value)
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

		/// <summary>Exclui um objeto <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" /> da coleção.</summary>
		/// <param name="value">O objeto a ser removido.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> for excluído com êxito; caso contrário <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">A coleção é somente leitura.  
		///
		/// ou - 
		/// A coleção tem um tamanho fixo.</exception>
		// Token: 0x06003348 RID: 13128 RVA: 0x000CC150 File Offset: 0x000CB550
		public bool Remove(GeneralTransform3D value)
		{
			base.WritePreamble();
			int num = this.IndexOf(value);
			if (num >= 0)
			{
				GeneralTransform3D oldValue = this._collection[num];
				base.OnFreezablePropertyChanged(oldValue, null);
				this._collection.RemoveAt(num);
				this._version += 1U;
				base.WritePostscript();
				return true;
			}
			return false;
		}

		/// <summary>Exclui um objeto <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" /> da <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DCollection" />.</summary>
		/// <param name="index">A posição de índice de base zero para remover o objeto.</param>
		// Token: 0x06003349 RID: 13129 RVA: 0x000CC1A8 File Offset: 0x000CB5A8
		public void RemoveAt(int index)
		{
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x000CC1C4 File Offset: 0x000CB5C4
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			base.WritePreamble();
			GeneralTransform3D oldValue = this._collection[index];
			base.OnFreezablePropertyChanged(oldValue, null);
			this._collection.RemoveAt(index);
			this._version += 1U;
		}

		/// <summary>Obtém ou define o objeto <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" /> na posição de índice especificada.</summary>
		/// <param name="index">A posição de índice de base zero do objeto a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" /> do objeto no <paramref name="index" /> posição.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> não é um índice válido na coleção.</exception>
		/// <exception cref="T:System.NotSupportedException">A coleção é somente leitura.  
		///
		/// ou - 
		/// A coleção tem um tamanho fixo.</exception>
		// Token: 0x17000A70 RID: 2672
		public GeneralTransform3D this[int index]
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
					GeneralTransform3D oldValue = this._collection[index];
					base.OnFreezablePropertyChanged(oldValue, value);
					this._collection[index] = value;
				}
				this._version += 1U;
				base.WritePostscript();
			}
		}

		/// <summary>Obtém o número de objetos nesta coleção.</summary>
		/// <returns>Número de itens na coleção.</returns>
		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x0600334D RID: 13133 RVA: 0x000CC294 File Offset: 0x000CB694
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Copia os objetos <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" /> da coleção, em uma matriz de objetos <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" />, começando na posição de índice especificada.</summary>
		/// <param name="array">A matriz de destino.</param>
		/// <param name="index">A posição de índice baseado em zero em que a cópia é iniciada.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> é multidimensional.  
		///
		/// ou - 
		/// O número de itens na coleção de origem é maior do que o espaço disponível do <paramref name="index" /> até o final da <paramref name="array" /> de destino.</exception>
		// Token: 0x0600334E RID: 13134 RVA: 0x000CC2B4 File Offset: 0x000CB6B4
		public void CopyTo(GeneralTransform3D[] array, int index)
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

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x0600334F RID: 13135 RVA: 0x000CC304 File Offset: 0x000CB704
		bool ICollection<GeneralTransform3D>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Retorna um enumerador que pode iterar pela coleção.</summary>
		/// <returns>Um enumerador que pode iterar a coleção.</returns>
		// Token: 0x06003350 RID: 13136 RVA: 0x000CC320 File Offset: 0x000CB720
		public GeneralTransform3DCollection.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new GeneralTransform3DCollection.Enumerator(this);
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x000CC33C File Offset: 0x000CB73C
		IEnumerator<GeneralTransform3D> IEnumerable<GeneralTransform3D>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DCollection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06003352 RID: 13138 RVA: 0x000CC354 File Offset: 0x000CB754
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<GeneralTransform3D>)this).IsReadOnly;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DCollection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06003353 RID: 13139 RVA: 0x000CC368 File Offset: 0x000CB768
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
		// Token: 0x17000A75 RID: 2677
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
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x06003356 RID: 13142 RVA: 0x000CC3B4 File Offset: 0x000CB7B4
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003357 RID: 13143 RVA: 0x000CC3D0 File Offset: 0x000CB7D0
		bool IList.Contains(object value)
		{
			return this.Contains(value as GeneralTransform3D);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06003358 RID: 13144 RVA: 0x000CC3EC File Offset: 0x000CB7EC
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as GeneralTransform3D);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DCollection" />.</param>
		// Token: 0x06003359 RID: 13145 RVA: 0x000CC408 File Offset: 0x000CB808
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DCollection" />.</param>
		// Token: 0x0600335A RID: 13146 RVA: 0x000CC424 File Offset: 0x000CB824
		void IList.Remove(object value)
		{
			this.Remove(value as GeneralTransform3D);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x0600335B RID: 13147 RVA: 0x000CC440 File Offset: 0x000CB840
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
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x0600335C RID: 13148 RVA: 0x000CC514 File Offset: 0x000CB914
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DCollection" />.</returns>
		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x0600335D RID: 13149 RVA: 0x000CC53C File Offset: 0x000CB93C
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
		// Token: 0x0600335E RID: 13150 RVA: 0x000CC550 File Offset: 0x000CB950
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x0600335F RID: 13151 RVA: 0x000CC568 File Offset: 0x000CB968
		internal static GeneralTransform3DCollection Empty
		{
			get
			{
				if (GeneralTransform3DCollection.s_empty == null)
				{
					GeneralTransform3DCollection generalTransform3DCollection = new GeneralTransform3DCollection();
					generalTransform3DCollection.Freeze();
					GeneralTransform3DCollection.s_empty = generalTransform3DCollection;
				}
				return GeneralTransform3DCollection.s_empty;
			}
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x000CC594 File Offset: 0x000CB994
		internal GeneralTransform3D Internal_GetItem(int i)
		{
			return this._collection[i];
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x000CC5B0 File Offset: 0x000CB9B0
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

		// Token: 0x06003362 RID: 13154 RVA: 0x000CC5F8 File Offset: 0x000CB9F8
		private GeneralTransform3D Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is GeneralTransform3D))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"GeneralTransform3D"
				}));
			}
			return (GeneralTransform3D)value;
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x000CC65C File Offset: 0x000CBA5C
		private int AddHelper(GeneralTransform3D value)
		{
			int result = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			return result;
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x000CC678 File Offset: 0x000CBA78
		internal int AddWithoutFiringPublicEvents(GeneralTransform3D value)
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

		// Token: 0x06003365 RID: 13157 RVA: 0x000CC6C8 File Offset: 0x000CBAC8
		protected override Freezable CreateInstanceCore()
		{
			return new GeneralTransform3DCollection();
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x000CC6DC File Offset: 0x000CBADC
		protected override void CloneCore(Freezable source)
		{
			GeneralTransform3DCollection generalTransform3DCollection = (GeneralTransform3DCollection)source;
			base.CloneCore(source);
			int count = generalTransform3DCollection._collection.Count;
			this._collection = new FrugalStructList<GeneralTransform3D>(count);
			for (int i = 0; i < count; i++)
			{
				GeneralTransform3D generalTransform3D = generalTransform3DCollection._collection[i].Clone();
				base.OnFreezablePropertyChanged(null, generalTransform3D);
				this._collection.Add(generalTransform3D);
			}
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x000CC744 File Offset: 0x000CBB44
		protected override void CloneCurrentValueCore(Freezable source)
		{
			GeneralTransform3DCollection generalTransform3DCollection = (GeneralTransform3DCollection)source;
			base.CloneCurrentValueCore(source);
			int count = generalTransform3DCollection._collection.Count;
			this._collection = new FrugalStructList<GeneralTransform3D>(count);
			for (int i = 0; i < count; i++)
			{
				GeneralTransform3D generalTransform3D = generalTransform3DCollection._collection[i].CloneCurrentValue();
				base.OnFreezablePropertyChanged(null, generalTransform3D);
				this._collection.Add(generalTransform3D);
			}
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x000CC7AC File Offset: 0x000CBBAC
		protected override void GetAsFrozenCore(Freezable source)
		{
			GeneralTransform3DCollection generalTransform3DCollection = (GeneralTransform3DCollection)source;
			base.GetAsFrozenCore(source);
			int count = generalTransform3DCollection._collection.Count;
			this._collection = new FrugalStructList<GeneralTransform3D>(count);
			for (int i = 0; i < count; i++)
			{
				GeneralTransform3D generalTransform3D = (GeneralTransform3D)generalTransform3DCollection._collection[i].GetAsFrozen();
				base.OnFreezablePropertyChanged(null, generalTransform3D);
				this._collection.Add(generalTransform3D);
			}
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x000CC818 File Offset: 0x000CBC18
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			GeneralTransform3DCollection generalTransform3DCollection = (GeneralTransform3DCollection)source;
			base.GetCurrentValueAsFrozenCore(source);
			int count = generalTransform3DCollection._collection.Count;
			this._collection = new FrugalStructList<GeneralTransform3D>(count);
			for (int i = 0; i < count; i++)
			{
				GeneralTransform3D generalTransform3D = (GeneralTransform3D)generalTransform3DCollection._collection[i].GetCurrentValueAsFrozen();
				base.OnFreezablePropertyChanged(null, generalTransform3D);
				this._collection.Add(generalTransform3D);
			}
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x000CC884 File Offset: 0x000CBC84
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

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DCollection" />.</summary>
		// Token: 0x0600336B RID: 13163 RVA: 0x000CC8CC File Offset: 0x000CBCCC
		public GeneralTransform3DCollection()
		{
			this._collection = default(FrugalStructList<GeneralTransform3D>);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DCollection" /> com a capacidade especificada ou o número de objetos <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" /> que a coleção é capaz de armazenar inicialmente.</summary>
		/// <param name="capacity">O número de objetos <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" /> que a coleção é capaz de armazenar inicialmente.</param>
		// Token: 0x0600336C RID: 13164 RVA: 0x000CC8EC File Offset: 0x000CBCEC
		public GeneralTransform3DCollection(int capacity)
		{
			this._collection = new FrugalStructList<GeneralTransform3D>(capacity);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DCollection" /> usando o objeto inicial especificado.</summary>
		/// <param name="collection">Objeto inicial na nova classe de coleção.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> é <see langword="null" />.</exception>
		// Token: 0x0600336D RID: 13165 RVA: 0x000CC90C File Offset: 0x000CBD0C
		public GeneralTransform3DCollection(IEnumerable<GeneralTransform3D> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				bool flag = true;
				ICollection<GeneralTransform3D> collection2 = collection as ICollection<GeneralTransform3D>;
				if (collection2 != null)
				{
					this._collection = new FrugalStructList<GeneralTransform3D>(collection2);
				}
				else
				{
					ICollection collection3 = collection as ICollection;
					if (collection3 != null)
					{
						this._collection = new FrugalStructList<GeneralTransform3D>(collection3);
					}
					else
					{
						this._collection = default(FrugalStructList<GeneralTransform3D>);
						foreach (GeneralTransform3D generalTransform3D in collection)
						{
							if (generalTransform3D == null)
							{
								throw new ArgumentException(SR.Get("Collection_NoNull"));
							}
							GeneralTransform3D generalTransform3D2 = generalTransform3D;
							base.OnFreezablePropertyChanged(null, generalTransform3D2);
							this._collection.Add(generalTransform3D2);
						}
						flag = false;
					}
				}
				if (flag)
				{
					foreach (GeneralTransform3D generalTransform3D3 in collection)
					{
						if (generalTransform3D3 == null)
						{
							throw new ArgumentException(SR.Get("Collection_NoNull"));
						}
						base.OnFreezablePropertyChanged(null, generalTransform3D3);
					}
				}
				base.WritePostscript();
				return;
			}
			throw new ArgumentNullException("collection");
		}

		// Token: 0x040015EE RID: 5614
		private static GeneralTransform3DCollection s_empty;

		// Token: 0x040015EF RID: 5615
		internal FrugalStructList<GeneralTransform3D> _collection;

		// Token: 0x040015F0 RID: 5616
		internal uint _version;

		/// <summary>Enumera itens <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" /> em um <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DCollection" />.</summary>
		// Token: 0x020008AD RID: 2221
		public struct Enumerator : IEnumerator, IEnumerator<GeneralTransform3D>, IDisposable
		{
			// Token: 0x06005885 RID: 22661 RVA: 0x00167D9C File Offset: 0x0016719C
			internal Enumerator(GeneralTransform3DCollection list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = null;
			}

			/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.IDisposable.Dispose" />.</summary>
			// Token: 0x06005886 RID: 22662 RVA: 0x00167DCC File Offset: 0x001671CC
			void IDisposable.Dispose()
			{
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador avança com êxito para o próximo elemento; caso contrário, <see langword="false" />.</returns>
			// Token: 0x06005887 RID: 22663 RVA: 0x00167DDC File Offset: 0x001671DC
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					GeneralTransform3DCollection list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._current = list._collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Redefine o enumerador com sua posição inicial, que é antes do primeiro elemento da coleção.</summary>
			// Token: 0x06005888 RID: 22664 RVA: 0x00167E70 File Offset: 0x00167270
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

			/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IEnumerator.Current" />.</summary>
			// Token: 0x1700123E RID: 4670
			// (get) Token: 0x06005889 RID: 22665 RVA: 0x00167EB4 File Offset: 0x001672B4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém o elemento atual na coleção.</summary>
			/// <returns>O elemento atual na coleção.</returns>
			// Token: 0x1700123F RID: 4671
			// (get) Token: 0x0600588A RID: 22666 RVA: 0x00167EC8 File Offset: 0x001672C8
			public GeneralTransform3D Current
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

			// Token: 0x04002908 RID: 10504
			private GeneralTransform3D _current;

			// Token: 0x04002909 RID: 10505
			private GeneralTransform3DCollection _list;

			// Token: 0x0400290A RID: 10506
			private uint _version;

			// Token: 0x0400290B RID: 10507
			private int _index;
		}
	}
}
