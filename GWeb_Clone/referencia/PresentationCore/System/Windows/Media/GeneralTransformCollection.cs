using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media
{
	/// <summary>Representa uma coleção ordenada de objetos de <see cref="T:System.Windows.Media.GeneralTransform" />.</summary>
	// Token: 0x020003AB RID: 939
	public sealed class GeneralTransformCollection : Animatable, IList, ICollection, IEnumerable, IList<GeneralTransform>, ICollection<GeneralTransform>, IEnumerable<GeneralTransform>
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.GeneralTransformCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x0600231A RID: 8986 RVA: 0x0008DB98 File Offset: 0x0008CF98
		public new GeneralTransformCollection Clone()
		{
			return (GeneralTransformCollection)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.GeneralTransformCollection" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x0600231B RID: 8987 RVA: 0x0008DBB0 File Offset: 0x0008CFB0
		public new GeneralTransformCollection CloneCurrentValue()
		{
			return (GeneralTransformCollection)base.CloneCurrentValue();
		}

		/// <summary>Adiciona um objeto <see cref="T:System.Windows.Media.GeneralTransform" /> ao final da <see cref="T:System.Windows.Media.GeneralTransformCollection" />.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.GeneralTransform" /> a ser adicionado ao final da <see cref="T:System.Windows.Media.GeneralTransformCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.GeneralTransformCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.GeneralTransformCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x0600231C RID: 8988 RVA: 0x0008DBC8 File Offset: 0x0008CFC8
		public void Add(GeneralTransform value)
		{
			this.AddHelper(value);
		}

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.GeneralTransform" /> de <see cref="T:System.Windows.Media.GeneralTransformCollection" />.</summary>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.GeneralTransformCollection" /> é somente leitura.</exception>
		// Token: 0x0600231D RID: 8989 RVA: 0x0008DBE0 File Offset: 0x0008CFE0
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

		/// <summary>Indica se a <see cref="T:System.Windows.Media.GeneralTransformCollection" /> contém o objeto <see cref="T:System.Windows.Media.GeneralTransform" /> especificado.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.GeneralTransform" /> a ser localizado no <see cref="T:System.Windows.Media.GeneralTransformCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="value" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600231E RID: 8990 RVA: 0x0008DC40 File Offset: 0x0008D040
		public bool Contains(GeneralTransform value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Procura o objeto <see cref="T:System.Windows.Media.GeneralTransform" /> especificado dentro de uma <see cref="T:System.Windows.Media.GeneralTransformCollection" />.</summary>
		/// <param name="value">O objeto a ser localizado.</param>
		/// <returns>A posição de índice de base zero de <paramref name="value" />, se encontrado; caso contrário, -1;</returns>
		// Token: 0x0600231F RID: 8991 RVA: 0x0008DC60 File Offset: 0x0008D060
		public int IndexOf(GeneralTransform value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Insere um objeto <see cref="T:System.Windows.Media.GeneralTransform" /> na posição de índice especificada na <see cref="T:System.Windows.Media.GeneralTransformCollection" />.</summary>
		/// <param name="index">A posição de índice de base zero para inserir o objeto.</param>
		/// <param name="value">O objeto a ser inserido.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> não é um índice válido no <see cref="T:System.Windows.Media.GeneralTransformCollection" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.GeneralTransformCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.GeneralTransformCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x06002320 RID: 8992 RVA: 0x0008DC80 File Offset: 0x0008D080
		public void Insert(int index, GeneralTransform value)
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

		/// <summary>Exclui um objeto <see cref="T:System.Windows.Media.GeneralTransform" /> da <see cref="T:System.Windows.Media.GeneralTransformCollection" />.</summary>
		/// <param name="value">O objeto a ser removido.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> foi excluído com êxito; caso contrário <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.GeneralTransformCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.GeneralTransformCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x06002321 RID: 8993 RVA: 0x0008DCD0 File Offset: 0x0008D0D0
		public bool Remove(GeneralTransform value)
		{
			base.WritePreamble();
			int num = this.IndexOf(value);
			if (num >= 0)
			{
				GeneralTransform oldValue = this._collection[num];
				base.OnFreezablePropertyChanged(oldValue, null);
				this._collection.RemoveAt(num);
				this._version += 1U;
				base.WritePostscript();
				return true;
			}
			return false;
		}

		/// <summary>Exclui um objeto <see cref="T:System.Windows.Media.GeneralTransform" /> da <see cref="T:System.Windows.Media.GeneralTransformCollection" />.</summary>
		/// <param name="index">A posição de índice de base zero para remover o objeto.</param>
		// Token: 0x06002322 RID: 8994 RVA: 0x0008DD28 File Offset: 0x0008D128
		public void RemoveAt(int index)
		{
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x0008DD44 File Offset: 0x0008D144
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			base.WritePreamble();
			GeneralTransform oldValue = this._collection[index];
			base.OnFreezablePropertyChanged(oldValue, null);
			this._collection.RemoveAt(index);
			this._version += 1U;
		}

		/// <summary>Obtém ou define o objeto <see cref="T:System.Windows.Media.GeneralTransform" /> na posição de índice especificada.</summary>
		/// <param name="index">A posição de índice de base zero do objeto a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.GeneralTransform" /> do objeto no <paramref name="index" /> posição.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> não é um índice válido no <see cref="T:System.Windows.Media.GeneralTransformCollection" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.GeneralTransformCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.GeneralTransformCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x170006F9 RID: 1785
		public GeneralTransform this[int index]
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
					GeneralTransform oldValue = this._collection[index];
					base.OnFreezablePropertyChanged(oldValue, value);
					this._collection[index] = value;
				}
				this._version += 1U;
				base.WritePostscript();
			}
		}

		/// <summary>Obtém o número de objetos <see cref="T:System.Windows.Media.GeneralTransform" /> no <see cref="T:System.Windows.Media.GeneralTransformCollection" />.</summary>
		/// <returns>Número de itens na coleção.</returns>
		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06002326 RID: 8998 RVA: 0x0008DE14 File Offset: 0x0008D214
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Copia os objetos <see cref="T:System.Windows.Media.GeneralTransform" /> na coleção para uma matriz de GeneralTransforms, começando na posição de índice especificada.</summary>
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
		/// O número de itens na origem <see cref="T:System.Windows.Media.GeneralTransformCollection" /> é maior do que o espaço disponível de <paramref name="index" /> até o final do <paramref name="array" /> de destino.</exception>
		// Token: 0x06002327 RID: 8999 RVA: 0x0008DE34 File Offset: 0x0008D234
		public void CopyTo(GeneralTransform[] array, int index)
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

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06002328 RID: 9000 RVA: 0x0008DE84 File Offset: 0x0008D284
		bool ICollection<GeneralTransform>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Retorna um enumerador que pode iterar pela coleção.</summary>
		/// <returns>Um enumerador que pode iterar a coleção.</returns>
		// Token: 0x06002329 RID: 9001 RVA: 0x0008DEA0 File Offset: 0x0008D2A0
		public GeneralTransformCollection.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new GeneralTransformCollection.Enumerator(this);
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x0008DEBC File Offset: 0x0008D2BC
		IEnumerator<GeneralTransform> IEnumerable<GeneralTransform>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.GeneralTransformCollection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x0600232B RID: 9003 RVA: 0x0008DED4 File Offset: 0x0008D2D4
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<GeneralTransform>)this).IsReadOnly;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.GeneralTransformCollection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x0600232C RID: 9004 RVA: 0x0008DEE8 File Offset: 0x0008D2E8
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
		// Token: 0x170006FE RID: 1790
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
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.GeneralTransformCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x0600232F RID: 9007 RVA: 0x0008DF34 File Offset: 0x0008D334
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Determinará se o <see cref="T:System.Collections.IList" /> contiver um valor específico.</summary>
		/// <param name="value">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Collections.IList" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002330 RID: 9008 RVA: 0x0008DF50 File Offset: 0x0008D350
		bool IList.Contains(object value)
		{
			return this.Contains(value as GeneralTransform);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.GeneralTransformCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06002331 RID: 9009 RVA: 0x0008DF6C File Offset: 0x0008D36C
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as GeneralTransform);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.GeneralTransformCollection" />.</param>
		// Token: 0x06002332 RID: 9010 RVA: 0x0008DF88 File Offset: 0x0008D388
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.GeneralTransformCollection" />.</param>
		// Token: 0x06002333 RID: 9011 RVA: 0x0008DFA4 File Offset: 0x0008D3A4
		void IList.Remove(object value)
		{
			this.Remove(value as GeneralTransform);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.GeneralTransformCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x06002334 RID: 9012 RVA: 0x0008DFC0 File Offset: 0x0008D3C0
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
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.GeneralTransformCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06002335 RID: 9013 RVA: 0x0008E094 File Offset: 0x0008D494
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.GeneralTransformCollection" />.</returns>
		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06002336 RID: 9014 RVA: 0x0008E0BC File Offset: 0x0008D4BC
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
		// Token: 0x06002337 RID: 9015 RVA: 0x0008E0D0 File Offset: 0x0008D4D0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06002338 RID: 9016 RVA: 0x0008E0E8 File Offset: 0x0008D4E8
		internal static GeneralTransformCollection Empty
		{
			get
			{
				if (GeneralTransformCollection.s_empty == null)
				{
					GeneralTransformCollection generalTransformCollection = new GeneralTransformCollection();
					generalTransformCollection.Freeze();
					GeneralTransformCollection.s_empty = generalTransformCollection;
				}
				return GeneralTransformCollection.s_empty;
			}
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x0008E114 File Offset: 0x0008D514
		internal GeneralTransform Internal_GetItem(int i)
		{
			return this._collection[i];
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x0008E130 File Offset: 0x0008D530
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

		// Token: 0x0600233B RID: 9019 RVA: 0x0008E178 File Offset: 0x0008D578
		private GeneralTransform Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is GeneralTransform))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"GeneralTransform"
				}));
			}
			return (GeneralTransform)value;
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x0008E1DC File Offset: 0x0008D5DC
		private int AddHelper(GeneralTransform value)
		{
			int result = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			return result;
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x0008E1F8 File Offset: 0x0008D5F8
		internal int AddWithoutFiringPublicEvents(GeneralTransform value)
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

		// Token: 0x0600233E RID: 9022 RVA: 0x0008E248 File Offset: 0x0008D648
		protected override Freezable CreateInstanceCore()
		{
			return new GeneralTransformCollection();
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x0008E25C File Offset: 0x0008D65C
		protected override void CloneCore(Freezable source)
		{
			GeneralTransformCollection generalTransformCollection = (GeneralTransformCollection)source;
			base.CloneCore(source);
			int count = generalTransformCollection._collection.Count;
			this._collection = new FrugalStructList<GeneralTransform>(count);
			for (int i = 0; i < count; i++)
			{
				GeneralTransform generalTransform = generalTransformCollection._collection[i].Clone();
				base.OnFreezablePropertyChanged(null, generalTransform);
				this._collection.Add(generalTransform);
			}
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x0008E2C4 File Offset: 0x0008D6C4
		protected override void CloneCurrentValueCore(Freezable source)
		{
			GeneralTransformCollection generalTransformCollection = (GeneralTransformCollection)source;
			base.CloneCurrentValueCore(source);
			int count = generalTransformCollection._collection.Count;
			this._collection = new FrugalStructList<GeneralTransform>(count);
			for (int i = 0; i < count; i++)
			{
				GeneralTransform generalTransform = generalTransformCollection._collection[i].CloneCurrentValue();
				base.OnFreezablePropertyChanged(null, generalTransform);
				this._collection.Add(generalTransform);
			}
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x0008E32C File Offset: 0x0008D72C
		protected override void GetAsFrozenCore(Freezable source)
		{
			GeneralTransformCollection generalTransformCollection = (GeneralTransformCollection)source;
			base.GetAsFrozenCore(source);
			int count = generalTransformCollection._collection.Count;
			this._collection = new FrugalStructList<GeneralTransform>(count);
			for (int i = 0; i < count; i++)
			{
				GeneralTransform generalTransform = (GeneralTransform)generalTransformCollection._collection[i].GetAsFrozen();
				base.OnFreezablePropertyChanged(null, generalTransform);
				this._collection.Add(generalTransform);
			}
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x0008E398 File Offset: 0x0008D798
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			GeneralTransformCollection generalTransformCollection = (GeneralTransformCollection)source;
			base.GetCurrentValueAsFrozenCore(source);
			int count = generalTransformCollection._collection.Count;
			this._collection = new FrugalStructList<GeneralTransform>(count);
			for (int i = 0; i < count; i++)
			{
				GeneralTransform generalTransform = (GeneralTransform)generalTransformCollection._collection[i].GetCurrentValueAsFrozen();
				base.OnFreezablePropertyChanged(null, generalTransform);
				this._collection.Add(generalTransform);
			}
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x0008E404 File Offset: 0x0008D804
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

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GeneralTransformCollection" />.</summary>
		// Token: 0x06002344 RID: 9028 RVA: 0x0008E44C File Offset: 0x0008D84C
		public GeneralTransformCollection()
		{
			this._collection = default(FrugalStructList<GeneralTransform>);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GeneralTransformCollection" /> com a capacidade especificada ou o número de objetos <see cref="T:System.Windows.Media.GeneralTransform" /> que a coleção é capaz de armazenar inicialmente.</summary>
		/// <param name="capacity">O número de objetos <see cref="T:System.Windows.Media.GeneralTransform" /> que a coleção é capaz de armazenar inicialmente.</param>
		// Token: 0x06002345 RID: 9029 RVA: 0x0008E46C File Offset: 0x0008D86C
		public GeneralTransformCollection(int capacity)
		{
			this._collection = new FrugalStructList<GeneralTransform>(capacity);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GeneralTransformCollection" />.</summary>
		/// <param name="collection">Objeto inicial na nova classe de coleção.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> é <see langword="null" />.</exception>
		// Token: 0x06002346 RID: 9030 RVA: 0x0008E48C File Offset: 0x0008D88C
		public GeneralTransformCollection(IEnumerable<GeneralTransform> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				bool flag = true;
				ICollection<GeneralTransform> collection2 = collection as ICollection<GeneralTransform>;
				if (collection2 != null)
				{
					this._collection = new FrugalStructList<GeneralTransform>(collection2);
				}
				else
				{
					ICollection collection3 = collection as ICollection;
					if (collection3 != null)
					{
						this._collection = new FrugalStructList<GeneralTransform>(collection3);
					}
					else
					{
						this._collection = default(FrugalStructList<GeneralTransform>);
						foreach (GeneralTransform generalTransform in collection)
						{
							if (generalTransform == null)
							{
								throw new ArgumentException(SR.Get("Collection_NoNull"));
							}
							GeneralTransform generalTransform2 = generalTransform;
							base.OnFreezablePropertyChanged(null, generalTransform2);
							this._collection.Add(generalTransform2);
						}
						flag = false;
					}
				}
				if (flag)
				{
					foreach (GeneralTransform generalTransform3 in collection)
					{
						if (generalTransform3 == null)
						{
							throw new ArgumentException(SR.Get("Collection_NoNull"));
						}
						base.OnFreezablePropertyChanged(null, generalTransform3);
					}
				}
				base.WritePostscript();
				return;
			}
			throw new ArgumentNullException("collection");
		}

		// Token: 0x04001143 RID: 4419
		private static GeneralTransformCollection s_empty;

		// Token: 0x04001144 RID: 4420
		internal FrugalStructList<GeneralTransform> _collection;

		// Token: 0x04001145 RID: 4421
		internal uint _version;

		/// <summary>Enumera itens <see cref="T:System.Windows.Media.GeneralTransform" /> em um <see cref="T:System.Windows.Media.GeneralTransformCollection" />.</summary>
		// Token: 0x02000872 RID: 2162
		public struct Enumerator : IEnumerator, IEnumerator<GeneralTransform>, IDisposable
		{
			// Token: 0x0600577B RID: 22395 RVA: 0x0016591C File Offset: 0x00164D1C
			internal Enumerator(GeneralTransformCollection list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = null;
			}

			/// <summary>Este tipo ou membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x0600577C RID: 22396 RVA: 0x0016594C File Offset: 0x00164D4C
			void IDisposable.Dispose()
			{
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador foi avançado com êxito para o próximo elemento; caso contrário, <see langword="false" />.</returns>
			// Token: 0x0600577D RID: 22397 RVA: 0x0016595C File Offset: 0x00164D5C
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					GeneralTransformCollection list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._current = list._collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Redefine o enumerador com sua posição inicial, que é antes do primeiro elemento da coleção.</summary>
			// Token: 0x0600577E RID: 22398 RVA: 0x001659F0 File Offset: 0x00164DF0
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
			// Token: 0x1700120B RID: 4619
			// (get) Token: 0x0600577F RID: 22399 RVA: 0x00165A34 File Offset: 0x00164E34
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém o elemento atual na coleção.</summary>
			/// <returns>O elemento atual na coleção.</returns>
			// Token: 0x1700120C RID: 4620
			// (get) Token: 0x06005780 RID: 22400 RVA: 0x00165A48 File Offset: 0x00164E48
			public GeneralTransform Current
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

			// Token: 0x04002886 RID: 10374
			private GeneralTransform _current;

			// Token: 0x04002887 RID: 10375
			private GeneralTransformCollection _list;

			// Token: 0x04002888 RID: 10376
			private uint _version;

			// Token: 0x04002889 RID: 10377
			private int _index;
		}
	}
}
