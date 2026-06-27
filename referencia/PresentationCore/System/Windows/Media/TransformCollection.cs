using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using MS.Internal.Collections;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Transform" /> que podem ser acessados individualmente por índice.</summary>
	// Token: 0x020003F9 RID: 1017
	public sealed class TransformCollection : Animatable, IList, ICollection, IEnumerable, IList<Transform>, ICollection<Transform>, IEnumerable<Transform>
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.TransformCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x0600283E RID: 10302 RVA: 0x000A1738 File Offset: 0x000A0B38
		public new TransformCollection Clone()
		{
			return (TransformCollection)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.TransformCollection" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x0600283F RID: 10303 RVA: 0x000A1750 File Offset: 0x000A0B50
		public new TransformCollection CloneCurrentValue()
		{
			return (TransformCollection)base.CloneCurrentValue();
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Transform" /> ao final do <see cref="T:System.Windows.Media.TransformCollection" />.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.Transform" /> a ser adicionado ao final da <see cref="T:System.Windows.Media.TransformCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.TransformCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.TransformCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x06002840 RID: 10304 RVA: 0x000A1768 File Offset: 0x000A0B68
		public void Add(Transform value)
		{
			this.AddHelper(value);
		}

		/// <summary>Remove todos os itens do <see cref="T:System.Windows.Media.TransformCollection" />.</summary>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.TransformCollection" /> é somente leitura.</exception>
		// Token: 0x06002841 RID: 10305 RVA: 0x000A1780 File Offset: 0x000A0B80
		public void Clear()
		{
			base.WritePreamble();
			FrugalStructList<Transform> collection = this._collection;
			this._collection = new FrugalStructList<Transform>(this._collection.Capacity);
			for (int i = collection.Count - 1; i >= 0; i--)
			{
				base.OnFreezablePropertyChanged(collection[i], null);
				this.OnRemove(collection[i]);
			}
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Media.TransformCollection" /> atual contém o <see cref="T:System.Windows.Media.Transform" /> especificado.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.Transform" /> a ser localizado no <see cref="T:System.Windows.Media.TransformCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.Transform" /> for encontrado no <see cref="T:System.Windows.Media.TransformCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002842 RID: 10306 RVA: 0x000A17F8 File Offset: 0x000A0BF8
		public bool Contains(Transform value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Determina o índice do item especificado na <see cref="T:System.Windows.Media.TransformCollection" />.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.Transform" /> a ser localizado no <see cref="T:System.Windows.Media.TransformCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na <see cref="T:System.Windows.Media.TransformCollection" />, caso contrário, -1.</returns>
		// Token: 0x06002843 RID: 10307 RVA: 0x000A1818 File Offset: 0x000A0C18
		public int IndexOf(Transform value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Transform" /> no <see cref="T:System.Windows.Media.TransformCollection" /> no índice especificado.</summary>
		/// <param name="index">O índice de base zero no qual o <paramref name="value" /> deve ser inserido.</param>
		/// <param name="value">O <see cref="T:System.Windows.Media.Transform" /> a ser inserido no <see cref="T:System.Windows.Media.TransformCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> não é um índice válido no <see cref="T:System.Windows.Media.TransformCollection" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.TransformCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.TransformCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x06002844 RID: 10308 RVA: 0x000A1838 File Offset: 0x000A0C38
		public void Insert(int index, Transform value)
		{
			if (value == null)
			{
				throw new ArgumentException(SR.Get("Collection_NoNull"));
			}
			base.WritePreamble();
			base.OnFreezablePropertyChanged(null, value);
			this._collection.Insert(index, value);
			this.OnInsert(value);
			this._version += 1U;
			base.WritePostscript();
		}

		/// <summary>Remove a primeira ocorrência do <see cref="T:System.Windows.Media.Transform" /> especificado do <see cref="T:System.Windows.Media.TransformCollection" />.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.Transform" /> a ser removido de <see cref="T:System.Windows.Media.TransformCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> tiver sido removido da <see cref="T:System.Windows.Media.TransformCollection" />, caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.TransformCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.TransformCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x06002845 RID: 10309 RVA: 0x000A1890 File Offset: 0x000A0C90
		public bool Remove(Transform value)
		{
			base.WritePreamble();
			int num = this.IndexOf(value);
			if (num >= 0)
			{
				Transform oldValue = this._collection[num];
				base.OnFreezablePropertyChanged(oldValue, null);
				this._collection.RemoveAt(num);
				this.OnRemove(oldValue);
				this._version += 1U;
				base.WritePostscript();
				return true;
			}
			return false;
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Transform" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero do <see cref="T:System.Windows.Media.Transform" /> a ser removido.</param>
		// Token: 0x06002846 RID: 10310 RVA: 0x000A18F0 File Offset: 0x000A0CF0
		public void RemoveAt(int index)
		{
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x000A190C File Offset: 0x000A0D0C
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			base.WritePreamble();
			Transform oldValue = this._collection[index];
			base.OnFreezablePropertyChanged(oldValue, null);
			this._collection.RemoveAt(index);
			this.OnRemove(oldValue);
			this._version += 1U;
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Transform" /> no índice especificado.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Transform" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Transform" /> no índice especificado.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> não é um índice válido no <see cref="T:System.Windows.Media.TransformCollection" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.TransformCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.TransformCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x170007BA RID: 1978
		public Transform this[int index]
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
					Transform oldValue = this._collection[index];
					base.OnFreezablePropertyChanged(oldValue, value);
					this._collection[index] = value;
					this.OnSet(oldValue, value);
				}
				this._version += 1U;
				base.WritePostscript();
			}
		}

		/// <summary>Obtém o número de itens contidos no <see cref="T:System.Windows.Media.TransformCollection" />.</summary>
		/// <returns>O número de itens no <see cref="T:System.Windows.Media.TransformCollection" />.</returns>
		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x0600284A RID: 10314 RVA: 0x000A19EC File Offset: 0x000A0DEC
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Copia os itens da <see cref="T:System.Windows.Media.TransformCollection" /> para uma matriz, começando no índice de matriz especificado.</summary>
		/// <param name="array">A matriz unidimensional que é o destino dos itens copiados do <see cref="T:System.Windows.Media.TransformCollection" />. A matriz deve ter indexação com base em zero.</param>
		/// <param name="index">O índice com base em zero em <paramref name="array" /> no qual a cópia começa.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> é multidimensional.  
		///
		/// ou - 
		/// O número de itens na origem <see cref="T:System.Windows.Media.TransformCollection" /> é maior do que o espaço disponível de <paramref name="index" /> até o final do <paramref name="array" /> de destino.</exception>
		// Token: 0x0600284B RID: 10315 RVA: 0x000A1A0C File Offset: 0x000A0E0C
		public void CopyTo(Transform[] array, int index)
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

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x0600284C RID: 10316 RVA: 0x000A1A5C File Offset: 0x000A0E5C
		bool ICollection<Transform>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Retorna um enumerador que pode ser iterado por meio de <see cref="T:System.Windows.Media.TransformCollection" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.TransformCollection.Enumerator" /> que pode ser usado para iterar por meio de <see cref="T:System.Windows.Media.TransformCollection" />.</returns>
		// Token: 0x0600284D RID: 10317 RVA: 0x000A1A78 File Offset: 0x000A0E78
		public TransformCollection.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new TransformCollection.Enumerator(this);
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x000A1A94 File Offset: 0x000A0E94
		IEnumerator<Transform> IEnumerable<Transform>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.TransformCollection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x0600284F RID: 10319 RVA: 0x000A1AAC File Offset: 0x000A0EAC
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<Transform>)this).IsReadOnly;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.TransformCollection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002850 RID: 10320 RVA: 0x000A1AC0 File Offset: 0x000A0EC0
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
		// Token: 0x170007BF RID: 1983
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
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.TransformCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x06002853 RID: 10323 RVA: 0x000A1B0C File Offset: 0x000A0F0C
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.TransformCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.TransformCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002854 RID: 10324 RVA: 0x000A1B28 File Offset: 0x000A0F28
		bool IList.Contains(object value)
		{
			return this.Contains(value as Transform);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.TransformCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06002855 RID: 10325 RVA: 0x000A1B44 File Offset: 0x000A0F44
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as Transform);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.TransformCollection" />.</param>
		// Token: 0x06002856 RID: 10326 RVA: 0x000A1B60 File Offset: 0x000A0F60
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.TransformCollection" />.</param>
		// Token: 0x06002857 RID: 10327 RVA: 0x000A1B7C File Offset: 0x000A0F7C
		void IList.Remove(object value)
		{
			this.Remove(value as Transform);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.TransformCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x06002858 RID: 10328 RVA: 0x000A1B98 File Offset: 0x000A0F98
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
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.TransformCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06002859 RID: 10329 RVA: 0x000A1C6C File Offset: 0x000A106C
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.TransformCollection" />.</returns>
		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x0600285A RID: 10330 RVA: 0x000A1C94 File Offset: 0x000A1094
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
		// Token: 0x0600285B RID: 10331 RVA: 0x000A1CA8 File Offset: 0x000A10A8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x0600285C RID: 10332 RVA: 0x000A1CC0 File Offset: 0x000A10C0
		internal static TransformCollection Empty
		{
			get
			{
				if (TransformCollection.s_empty == null)
				{
					TransformCollection transformCollection = new TransformCollection();
					transformCollection.Freeze();
					TransformCollection.s_empty = transformCollection;
				}
				return TransformCollection.s_empty;
			}
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x000A1CEC File Offset: 0x000A10EC
		internal Transform Internal_GetItem(int i)
		{
			return this._collection[i];
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x000A1D08 File Offset: 0x000A1108
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

		// Token: 0x0600285F RID: 10335 RVA: 0x000A1D50 File Offset: 0x000A1150
		private Transform Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is Transform))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"Transform"
				}));
			}
			return (Transform)value;
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x000A1DB4 File Offset: 0x000A11B4
		private int AddHelper(Transform value)
		{
			int result = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			return result;
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x000A1DD0 File Offset: 0x000A11D0
		internal int AddWithoutFiringPublicEvents(Transform value)
		{
			if (value == null)
			{
				throw new ArgumentException(SR.Get("Collection_NoNull"));
			}
			base.WritePreamble();
			base.OnFreezablePropertyChanged(null, value);
			int result = this._collection.Add(value);
			this.OnInsert(value);
			this._version += 1U;
			return result;
		}

		// Token: 0x14000198 RID: 408
		// (add) Token: 0x06002862 RID: 10338 RVA: 0x000A1E28 File Offset: 0x000A1228
		// (remove) Token: 0x06002863 RID: 10339 RVA: 0x000A1E60 File Offset: 0x000A1260
		internal event ItemInsertedHandler ItemInserted;

		// Token: 0x14000199 RID: 409
		// (add) Token: 0x06002864 RID: 10340 RVA: 0x000A1E98 File Offset: 0x000A1298
		// (remove) Token: 0x06002865 RID: 10341 RVA: 0x000A1ED0 File Offset: 0x000A12D0
		internal event ItemRemovedHandler ItemRemoved;

		// Token: 0x06002866 RID: 10342 RVA: 0x000A1F08 File Offset: 0x000A1308
		private void OnInsert(object item)
		{
			if (this.ItemInserted != null)
			{
				this.ItemInserted(this, item);
			}
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x000A1F2C File Offset: 0x000A132C
		private void OnRemove(object oldValue)
		{
			if (this.ItemRemoved != null)
			{
				this.ItemRemoved(this, oldValue);
			}
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x000A1F50 File Offset: 0x000A1350
		private void OnSet(object oldValue, object newValue)
		{
			this.OnInsert(newValue);
			this.OnRemove(oldValue);
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x000A1F6C File Offset: 0x000A136C
		protected override Freezable CreateInstanceCore()
		{
			return new TransformCollection();
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x000A1F80 File Offset: 0x000A1380
		protected override void CloneCore(Freezable source)
		{
			TransformCollection transformCollection = (TransformCollection)source;
			base.CloneCore(source);
			int count = transformCollection._collection.Count;
			this._collection = new FrugalStructList<Transform>(count);
			for (int i = 0; i < count; i++)
			{
				Transform transform = transformCollection._collection[i].Clone();
				base.OnFreezablePropertyChanged(null, transform);
				this._collection.Add(transform);
				this.OnInsert(transform);
			}
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x000A1FF0 File Offset: 0x000A13F0
		protected override void CloneCurrentValueCore(Freezable source)
		{
			TransformCollection transformCollection = (TransformCollection)source;
			base.CloneCurrentValueCore(source);
			int count = transformCollection._collection.Count;
			this._collection = new FrugalStructList<Transform>(count);
			for (int i = 0; i < count; i++)
			{
				Transform transform = transformCollection._collection[i].CloneCurrentValue();
				base.OnFreezablePropertyChanged(null, transform);
				this._collection.Add(transform);
				this.OnInsert(transform);
			}
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x000A2060 File Offset: 0x000A1460
		protected override void GetAsFrozenCore(Freezable source)
		{
			TransformCollection transformCollection = (TransformCollection)source;
			base.GetAsFrozenCore(source);
			int count = transformCollection._collection.Count;
			this._collection = new FrugalStructList<Transform>(count);
			for (int i = 0; i < count; i++)
			{
				Transform transform = (Transform)transformCollection._collection[i].GetAsFrozen();
				base.OnFreezablePropertyChanged(null, transform);
				this._collection.Add(transform);
				this.OnInsert(transform);
			}
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x000A20D4 File Offset: 0x000A14D4
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			TransformCollection transformCollection = (TransformCollection)source;
			base.GetCurrentValueAsFrozenCore(source);
			int count = transformCollection._collection.Count;
			this._collection = new FrugalStructList<Transform>(count);
			for (int i = 0; i < count; i++)
			{
				Transform transform = (Transform)transformCollection._collection[i].GetCurrentValueAsFrozen();
				base.OnFreezablePropertyChanged(null, transform);
				this._collection.Add(transform);
				this.OnInsert(transform);
			}
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x000A2148 File Offset: 0x000A1548
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

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TransformCollection" />.</summary>
		// Token: 0x0600286F RID: 10351 RVA: 0x000A2190 File Offset: 0x000A1590
		public TransformCollection()
		{
			this._collection = default(FrugalStructList<Transform>);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TransformCollection" /> com a capacidade especificada.</summary>
		/// <param name="capacity">O número de objetos <see cref="T:System.Windows.Media.Transform" /> que a coleção é capaz de armazenar inicialmente.</param>
		// Token: 0x06002870 RID: 10352 RVA: 0x000A21B0 File Offset: 0x000A15B0
		public TransformCollection(int capacity)
		{
			this._collection = new FrugalStructList<Transform>(capacity);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TransformCollection" />, que contém os itens copiados da coleção especificada de objetos <see cref="T:System.Windows.Media.Transform" /> e tem a mesma capacidade inicial que o número de itens copiados.</summary>
		/// <param name="collection">A coleção cujos itens são copiados para o novo <see cref="T:System.Windows.Media.TransformCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> é <see langword="null" />.</exception>
		// Token: 0x06002871 RID: 10353 RVA: 0x000A21D0 File Offset: 0x000A15D0
		public TransformCollection(IEnumerable<Transform> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				bool flag = true;
				ICollection<Transform> collection2 = collection as ICollection<Transform>;
				if (collection2 != null)
				{
					this._collection = new FrugalStructList<Transform>(collection2);
				}
				else
				{
					ICollection collection3 = collection as ICollection;
					if (collection3 != null)
					{
						this._collection = new FrugalStructList<Transform>(collection3);
					}
					else
					{
						this._collection = default(FrugalStructList<Transform>);
						foreach (Transform transform in collection)
						{
							if (transform == null)
							{
								throw new ArgumentException(SR.Get("Collection_NoNull"));
							}
							Transform transform2 = transform;
							base.OnFreezablePropertyChanged(null, transform2);
							this._collection.Add(transform2);
							this.OnInsert(transform2);
						}
						flag = false;
					}
				}
				if (flag)
				{
					foreach (Transform transform3 in collection)
					{
						if (transform3 == null)
						{
							throw new ArgumentException(SR.Get("Collection_NoNull"));
						}
						base.OnFreezablePropertyChanged(null, transform3);
						this.OnInsert(transform3);
					}
				}
				base.WritePostscript();
				return;
			}
			throw new ArgumentNullException("collection");
		}

		// Token: 0x04001295 RID: 4757
		private static TransformCollection s_empty;

		// Token: 0x04001296 RID: 4758
		internal FrugalStructList<Transform> _collection;

		// Token: 0x04001297 RID: 4759
		internal uint _version;

		/// <summary>Dá suporte a uma iteração simples em um <see cref="T:System.Windows.Media.TransformCollection" />.</summary>
		// Token: 0x02000882 RID: 2178
		public struct Enumerator : IEnumerator, IEnumerator<Transform>, IDisposable
		{
			// Token: 0x060057C3 RID: 22467 RVA: 0x00166A54 File Offset: 0x00165E54
			internal Enumerator(TransformCollection list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = null;
			}

			/// <summary>Este tipo ou membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x060057C4 RID: 22468 RVA: 0x00166A84 File Offset: 0x00165E84
			void IDisposable.Dispose()
			{
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador avançou com êxito para o próximo elemento; <see langword="false" /> se o enumerador passou o final da coleção.</returns>
			/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Windows.Media.TransformCollection" /> foi modificada depois da criação do enumerador.</exception>
			// Token: 0x060057C5 RID: 22469 RVA: 0x00166A94 File Offset: 0x00165E94
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					TransformCollection list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._current = list._collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Redefine o enumerador para sua posição inicial, que é antes do primeiro item no <see cref="T:System.Windows.Media.TransformCollection" />.</summary>
			/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Windows.Media.TransformCollection" /> foi modificada depois da criação do enumerador.</exception>
			// Token: 0x060057C6 RID: 22470 RVA: 0x00166B28 File Offset: 0x00165F28
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
			// Token: 0x1700121E RID: 4638
			// (get) Token: 0x060057C7 RID: 22471 RVA: 0x00166B6C File Offset: 0x00165F6C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém o item atual no <see cref="T:System.Windows.Media.TransformCollection" />.</summary>
			/// <returns>O item atual no <see cref="T:System.Windows.Media.TransformCollection" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Windows.Media.TransformCollection" /> foi modificada depois da criação do enumerador.</exception>
			// Token: 0x1700121F RID: 4639
			// (get) Token: 0x060057C8 RID: 22472 RVA: 0x00166B80 File Offset: 0x00165F80
			public Transform Current
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

			// Token: 0x040028B6 RID: 10422
			private Transform _current;

			// Token: 0x040028B7 RID: 10423
			private TransformCollection _list;

			// Token: 0x040028B8 RID: 10424
			private uint _version;

			// Token: 0x040028B9 RID: 10425
			private int _index;
		}
	}
}
