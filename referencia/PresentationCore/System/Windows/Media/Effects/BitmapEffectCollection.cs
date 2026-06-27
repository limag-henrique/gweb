using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media.Effects
{
	/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Representa uma coleção de objetos <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> . Essa coleção é usada como parte de um <see cref="T:System.Windows.Media.Effects.BitmapEffectGroup" /> para aplicar vários efeitos de bitmap ao conteúdo visual.</summary>
	// Token: 0x02000616 RID: 1558
	public sealed class BitmapEffectCollection : Animatable, IList, ICollection, IEnumerable, IList<BitmapEffect>, ICollection<BitmapEffect>, IEnumerable<BitmapEffect>
	{
		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Cria um clone modificável desse <see cref="T:System.Windows.Media.Effects.BitmapEffectCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060047C6 RID: 18374 RVA: 0x00118F88 File Offset: 0x00118388
		public new BitmapEffectCollection Clone()
		{
			return (BitmapEffectCollection)base.Clone();
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Effects.BitmapEffectCollection" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060047C7 RID: 18375 RVA: 0x00118FA0 File Offset: 0x001183A0
		public new BitmapEffectCollection CloneCurrentValue()
		{
			return (BitmapEffectCollection)base.CloneCurrentValue();
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Adiciona um <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> no fim da coleção.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> a adicionar ao final da coleção.</param>
		// Token: 0x060047C8 RID: 18376 RVA: 0x00118FB8 File Offset: 0x001183B8
		public void Add(BitmapEffect value)
		{
			this.AddHelper(value);
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Remove todos os efeitos da coleção.</summary>
		// Token: 0x060047C9 RID: 18377 RVA: 0x00118FD0 File Offset: 0x001183D0
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

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Indica se a coleção contém o <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> especificado.</summary>
		/// <param name="value">O efeito de bitmap a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver um valor; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060047CA RID: 18378 RVA: 0x00119030 File Offset: 0x00118430
		public bool Contains(BitmapEffect value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Recupera o índice da primeira instância do <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> especificado.</summary>
		/// <param name="value">O efeito a ser encontrado na coleção.</param>
		/// <returns>O índice do efeito especificado.</returns>
		// Token: 0x060047CB RID: 18379 RVA: 0x00119050 File Offset: 0x00118450
		public int IndexOf(BitmapEffect value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Insere um <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> nesta coleção no índice especificado.</summary>
		/// <param name="index">O índice no qual inserir o efeito.</param>
		/// <param name="value">O efeito especificado a ser inserido.</param>
		// Token: 0x060047CC RID: 18380 RVA: 0x00119070 File Offset: 0x00118470
		public void Insert(int index, BitmapEffect value)
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

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Remove a primeira ocorrência do <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> especificado para esta coleção.</summary>
		/// <param name="value">O efeito a ser removido da coleção</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> tiver sido removido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060047CD RID: 18381 RVA: 0x001190C0 File Offset: 0x001184C0
		public bool Remove(BitmapEffect value)
		{
			base.WritePreamble();
			int num = this.IndexOf(value);
			if (num >= 0)
			{
				BitmapEffect oldValue = this._collection[num];
				base.OnFreezablePropertyChanged(oldValue, null);
				this._collection.RemoveAt(num);
				this._version += 1U;
				base.WritePostscript();
				return true;
			}
			return false;
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Remove o <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> no índice especificado da coleção.</summary>
		/// <param name="index">O índice do efeito a ser removido.</param>
		// Token: 0x060047CE RID: 18382 RVA: 0x00119118 File Offset: 0x00118518
		public void RemoveAt(int index)
		{
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
		}

		// Token: 0x060047CF RID: 18383 RVA: 0x00119134 File Offset: 0x00118534
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			base.WritePreamble();
			BitmapEffect oldValue = this._collection[index];
			base.OnFreezablePropertyChanged(oldValue, null);
			this._collection.RemoveAt(index);
			this._version += 1U;
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Obtém ou define o <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> no índice especificado.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> no índice especificado.</returns>
		// Token: 0x17000EFC RID: 3836
		public BitmapEffect this[int index]
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
					BitmapEffect oldValue = this._collection[index];
					base.OnFreezablePropertyChanged(oldValue, value);
					this._collection[index] = value;
				}
				this._version += 1U;
				base.WritePostscript();
			}
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Obtém o número de efeitos contidos no <see cref="T:System.Windows.Media.Effects.BitmapEffectCollection" />.</summary>
		/// <returns>O número de efeitos contidos no <see cref="T:System.Windows.Media.Effects.BitmapEffectCollection" />.</returns>
		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x060047D2 RID: 18386 RVA: 0x00119204 File Offset: 0x00118604
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Copia os elementos da coleção para uma matriz, começando no índice determinado.</summary>
		/// <param name="array">A matriz para a qual copiar.</param>
		/// <param name="index">O índice da coleção para começar a cópia.</param>
		// Token: 0x060047D3 RID: 18387 RVA: 0x00119224 File Offset: 0x00118624
		public void CopyTo(BitmapEffect[] array, int index)
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

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x060047D4 RID: 18388 RVA: 0x00119274 File Offset: 0x00118674
		bool ICollection<BitmapEffect>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Retorna um enumerador que pode iterar pela coleção.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Effects.BitmapEffectCollection.Enumerator" /> que pode iterar pela coleção.</returns>
		// Token: 0x060047D5 RID: 18389 RVA: 0x00119290 File Offset: 0x00118690
		public BitmapEffectCollection.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new BitmapEffectCollection.Enumerator(this);
		}

		// Token: 0x060047D6 RID: 18390 RVA: 0x001192AC File Offset: 0x001186AC
		IEnumerator<BitmapEffect> IEnumerable<BitmapEffect>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.Effects.BitmapEffectCollection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x060047D7 RID: 18391 RVA: 0x001192C4 File Offset: 0x001186C4
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<BitmapEffect>)this).IsReadOnly;
			}
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.Effects.BitmapEffectCollection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x060047D8 RID: 18392 RVA: 0x001192D8 File Offset: 0x001186D8
		bool IList.IsFixedSize
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <param name="index">O índice com base em zero do elemento a ser obtido ou definido.</param>
		/// <returns>O elemento no índice especificado.</returns>
		// Token: 0x17000F01 RID: 3841
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

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.Effects.BitmapEffectCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x060047DB RID: 18395 RVA: 0x00119324 File Offset: 0x00118724
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.Effects.BitmapEffectCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.Effects.BitmapEffectCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060047DC RID: 18396 RVA: 0x00119340 File Offset: 0x00118740
		bool IList.Contains(object value)
		{
			return this.Contains(value as BitmapEffect);
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.Effects.BitmapEffectCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x060047DD RID: 18397 RVA: 0x0011935C File Offset: 0x0011875C
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as BitmapEffect);
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.Effects.BitmapEffectCollection" />.</param>
		// Token: 0x060047DE RID: 18398 RVA: 0x00119378 File Offset: 0x00118778
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.Effects.BitmapEffectCollection" />.</param>
		// Token: 0x060047DF RID: 18399 RVA: 0x00119394 File Offset: 0x00118794
		void IList.Remove(object value)
		{
			this.Remove(value as BitmapEffect);
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.Effects.BitmapEffectCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x060047E0 RID: 18400 RVA: 0x001193B0 File Offset: 0x001187B0
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

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.Effects.BitmapEffectCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x060047E1 RID: 18401 RVA: 0x00119484 File Offset: 0x00118884
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.Effects.BitmapEffectCollection" />.</returns>
		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x060047E2 RID: 18402 RVA: 0x001194AC File Offset: 0x001188AC
		object ICollection.SyncRoot
		{
			get
			{
				base.ReadPreamble();
				return this;
			}
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
		/// <returns>Um objeto <see cref="T:System.Collections.IEnumerator" /> que pode ser usado para iterar pela coleção.</returns>
		// Token: 0x060047E3 RID: 18403 RVA: 0x001194C0 File Offset: 0x001188C0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x060047E4 RID: 18404 RVA: 0x001194D8 File Offset: 0x001188D8
		internal static BitmapEffectCollection Empty
		{
			get
			{
				if (BitmapEffectCollection.s_empty == null)
				{
					BitmapEffectCollection bitmapEffectCollection = new BitmapEffectCollection();
					bitmapEffectCollection.Freeze();
					BitmapEffectCollection.s_empty = bitmapEffectCollection;
				}
				return BitmapEffectCollection.s_empty;
			}
		}

		// Token: 0x060047E5 RID: 18405 RVA: 0x00119504 File Offset: 0x00118904
		internal BitmapEffect Internal_GetItem(int i)
		{
			return this._collection[i];
		}

		// Token: 0x060047E6 RID: 18406 RVA: 0x00119520 File Offset: 0x00118920
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

		// Token: 0x060047E7 RID: 18407 RVA: 0x00119568 File Offset: 0x00118968
		private BitmapEffect Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is BitmapEffect))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"BitmapEffect"
				}));
			}
			return (BitmapEffect)value;
		}

		// Token: 0x060047E8 RID: 18408 RVA: 0x001195CC File Offset: 0x001189CC
		private int AddHelper(BitmapEffect value)
		{
			int result = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			return result;
		}

		// Token: 0x060047E9 RID: 18409 RVA: 0x001195E8 File Offset: 0x001189E8
		internal int AddWithoutFiringPublicEvents(BitmapEffect value)
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

		// Token: 0x060047EA RID: 18410 RVA: 0x00119638 File Offset: 0x00118A38
		protected override Freezable CreateInstanceCore()
		{
			return new BitmapEffectCollection();
		}

		// Token: 0x060047EB RID: 18411 RVA: 0x0011964C File Offset: 0x00118A4C
		protected override void CloneCore(Freezable source)
		{
			BitmapEffectCollection bitmapEffectCollection = (BitmapEffectCollection)source;
			base.CloneCore(source);
			int count = bitmapEffectCollection._collection.Count;
			this._collection = new FrugalStructList<BitmapEffect>(count);
			for (int i = 0; i < count; i++)
			{
				BitmapEffect bitmapEffect = bitmapEffectCollection._collection[i].Clone();
				base.OnFreezablePropertyChanged(null, bitmapEffect);
				this._collection.Add(bitmapEffect);
			}
		}

		// Token: 0x060047EC RID: 18412 RVA: 0x001196B4 File Offset: 0x00118AB4
		protected override void CloneCurrentValueCore(Freezable source)
		{
			BitmapEffectCollection bitmapEffectCollection = (BitmapEffectCollection)source;
			base.CloneCurrentValueCore(source);
			int count = bitmapEffectCollection._collection.Count;
			this._collection = new FrugalStructList<BitmapEffect>(count);
			for (int i = 0; i < count; i++)
			{
				BitmapEffect bitmapEffect = bitmapEffectCollection._collection[i].CloneCurrentValue();
				base.OnFreezablePropertyChanged(null, bitmapEffect);
				this._collection.Add(bitmapEffect);
			}
		}

		// Token: 0x060047ED RID: 18413 RVA: 0x0011971C File Offset: 0x00118B1C
		protected override void GetAsFrozenCore(Freezable source)
		{
			BitmapEffectCollection bitmapEffectCollection = (BitmapEffectCollection)source;
			base.GetAsFrozenCore(source);
			int count = bitmapEffectCollection._collection.Count;
			this._collection = new FrugalStructList<BitmapEffect>(count);
			for (int i = 0; i < count; i++)
			{
				BitmapEffect bitmapEffect = (BitmapEffect)bitmapEffectCollection._collection[i].GetAsFrozen();
				base.OnFreezablePropertyChanged(null, bitmapEffect);
				this._collection.Add(bitmapEffect);
			}
		}

		// Token: 0x060047EE RID: 18414 RVA: 0x00119788 File Offset: 0x00118B88
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			BitmapEffectCollection bitmapEffectCollection = (BitmapEffectCollection)source;
			base.GetCurrentValueAsFrozenCore(source);
			int count = bitmapEffectCollection._collection.Count;
			this._collection = new FrugalStructList<BitmapEffect>(count);
			for (int i = 0; i < count; i++)
			{
				BitmapEffect bitmapEffect = (BitmapEffect)bitmapEffectCollection._collection[i].GetCurrentValueAsFrozen();
				base.OnFreezablePropertyChanged(null, bitmapEffect);
				this._collection.Add(bitmapEffect);
			}
		}

		// Token: 0x060047EF RID: 18415 RVA: 0x001197F4 File Offset: 0x00118BF4
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

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Effects.BitmapEffectCollection" />.</summary>
		// Token: 0x060047F0 RID: 18416 RVA: 0x0011983C File Offset: 0x00118C3C
		public BitmapEffectCollection()
		{
			this._collection = default(FrugalStructList<BitmapEffect>);
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Effects.BitmapEffectCollection" /> com uma capacidade especificada ou o número de objetos <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> que a coleção é capaz de armazenar inicialmente.</summary>
		/// <param name="capacity">A capacidade inicial da coleção.</param>
		// Token: 0x060047F1 RID: 18417 RVA: 0x0011985C File Offset: 0x00118C5C
		public BitmapEffectCollection(int capacity)
		{
			this._collection = new FrugalStructList<BitmapEffect>(capacity);
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Effects.BitmapEffectCollection" /> usando a coleção fornecida.</summary>
		/// <param name="collection">A coleção usada para inicialização.</param>
		// Token: 0x060047F2 RID: 18418 RVA: 0x0011987C File Offset: 0x00118C7C
		public BitmapEffectCollection(IEnumerable<BitmapEffect> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				bool flag = true;
				ICollection<BitmapEffect> collection2 = collection as ICollection<BitmapEffect>;
				if (collection2 != null)
				{
					this._collection = new FrugalStructList<BitmapEffect>(collection2);
				}
				else
				{
					ICollection collection3 = collection as ICollection;
					if (collection3 != null)
					{
						this._collection = new FrugalStructList<BitmapEffect>(collection3);
					}
					else
					{
						this._collection = default(FrugalStructList<BitmapEffect>);
						foreach (BitmapEffect bitmapEffect in collection)
						{
							if (bitmapEffect == null)
							{
								throw new ArgumentException(SR.Get("Collection_NoNull"));
							}
							BitmapEffect bitmapEffect2 = bitmapEffect;
							base.OnFreezablePropertyChanged(null, bitmapEffect2);
							this._collection.Add(bitmapEffect2);
						}
						flag = false;
					}
				}
				if (flag)
				{
					foreach (BitmapEffect bitmapEffect3 in collection)
					{
						if (bitmapEffect3 == null)
						{
							throw new ArgumentException(SR.Get("Collection_NoNull"));
						}
						base.OnFreezablePropertyChanged(null, bitmapEffect3);
					}
				}
				base.WritePostscript();
				return;
			}
			throw new ArgumentNullException("collection");
		}

		// Token: 0x04001A25 RID: 6693
		private static BitmapEffectCollection s_empty;

		// Token: 0x04001A26 RID: 6694
		internal FrugalStructList<BitmapEffect> _collection;

		// Token: 0x04001A27 RID: 6695
		internal uint _version;

		/// <summary>Observação: esta API agora é obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Enumera objetos de <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> em um <see cref="T:System.Windows.Media.Effects.BitmapEffectCollection" />.</summary>
		// Token: 0x020008E0 RID: 2272
		public struct Enumerator : IEnumerator, IEnumerator<BitmapEffect>, IDisposable
		{
			// Token: 0x06005913 RID: 22803 RVA: 0x00169720 File Offset: 0x00168B20
			internal Enumerator(BitmapEffectCollection list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = null;
			}

			/// <summary>Observação: esta API agora é obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Este tipo ou membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x06005914 RID: 22804 RVA: 0x00169750 File Offset: 0x00168B50
			void IDisposable.Dispose()
			{
			}

			/// <summary>Observação: esta API agora é obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Avança o enumerador para o próximo elemento na coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador foi avançado com êxito para o próximo elemento; caso contrário, <see langword="false" />.</returns>
			// Token: 0x06005915 RID: 22805 RVA: 0x00169760 File Offset: 0x00168B60
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					BitmapEffectCollection list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._current = list._collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Observação: esta API agora é obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Redefine o enumerador com sua posição inicial, que é antes do primeiro elemento da coleção.</summary>
			// Token: 0x06005916 RID: 22806 RVA: 0x001697F4 File Offset: 0x00168BF4
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

			/// <summary>Observação: esta API agora é obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Este tipo ou membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x1700125F RID: 4703
			// (get) Token: 0x06005917 RID: 22807 RVA: 0x00169838 File Offset: 0x00168C38
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Observação: esta API agora é obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Obtém o elemento atual na coleção.</summary>
			/// <returns>O elemento atual na coleção.</returns>
			// Token: 0x17001260 RID: 4704
			// (get) Token: 0x06005918 RID: 22808 RVA: 0x0016984C File Offset: 0x00168C4C
			public BitmapEffect Current
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

			// Token: 0x040029A1 RID: 10657
			private BitmapEffect _current;

			// Token: 0x040029A2 RID: 10658
			private BitmapEffectCollection _list;

			// Token: 0x040029A3 RID: 10659
			private uint _version;

			// Token: 0x040029A4 RID: 10660
			private int _index;
		}
	}
}
