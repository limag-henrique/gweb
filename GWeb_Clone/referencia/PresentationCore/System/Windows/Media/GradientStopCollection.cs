using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.GradientStop" /> que podem ser acessados individualmente por índice.</summary>
	// Token: 0x020003B5 RID: 949
	public sealed class GradientStopCollection : Animatable, IFormattable, IList, ICollection, IEnumerable, IList<GradientStop>, ICollection<GradientStop>, IEnumerable<GradientStop>
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.GradientStopCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002427 RID: 9255 RVA: 0x000917C8 File Offset: 0x00090BC8
		public new GradientStopCollection Clone()
		{
			return (GradientStopCollection)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.GradientStopCollection" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002428 RID: 9256 RVA: 0x000917E0 File Offset: 0x00090BE0
		public new GradientStopCollection CloneCurrentValue()
		{
			return (GradientStopCollection)base.CloneCurrentValue();
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.GradientStop" /> à coleção de marcas de gradiente.</summary>
		/// <param name="value">A <see cref="T:System.Windows.Media.GradientStop" /> a ser adicionada ao final da <see cref="T:System.Windows.Media.GradientStopCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.GradientStopCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.GradientStopCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x06002429 RID: 9257 RVA: 0x000917F8 File Offset: 0x00090BF8
		public void Add(GradientStop value)
		{
			this.AddHelper(value);
		}

		/// <summary>Remove todos os itens da lista de marcas de gradiente.</summary>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.GradientStopCollection" /> é somente leitura.</exception>
		// Token: 0x0600242A RID: 9258 RVA: 0x00091810 File Offset: 0x00090C10
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

		/// <summary>Determina se a coleção contém o <see cref="T:System.Windows.Media.GradientStop" /> especificado.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.GradientStop" /> a ser localizado no <see cref="T:System.Windows.Media.GradientStopCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.GradientStop" /> for encontrado no <see cref="T:System.Windows.Media.GradientStopCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600242B RID: 9259 RVA: 0x00091870 File Offset: 0x00090C70
		public bool Contains(GradientStop value)
		{
			base.ReadPreamble();
			return this._collection.Contains(value);
		}

		/// <summary>Retorna o índice baseado em zero da <see cref="T:System.Windows.Media.GradientStop" /> especificada.</summary>
		/// <param name="value">O item a ser procurado.</param>
		/// <returns>O índice se o objeto foi encontrado; caso contrário, -1.</returns>
		// Token: 0x0600242C RID: 9260 RVA: 0x00091890 File Offset: 0x00090C90
		public int IndexOf(GradientStop value)
		{
			base.ReadPreamble();
			return this._collection.IndexOf(value);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.GradientStop" /> na posição especificada na lista de marcas de gradiente.</summary>
		/// <param name="index">O índice baseado em zero no qual inserir o objeto.</param>
		/// <param name="value">O <see cref="T:System.Windows.Media.GradientStop" /> a ser inserido.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> não é um índice válido no <see cref="T:System.Windows.Media.GradientStopCollection" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.GradientStopCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.GradientStopCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x0600242D RID: 9261 RVA: 0x000918B0 File Offset: 0x00090CB0
		public void Insert(int index, GradientStop value)
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

		/// <summary>Remove a primeira ocorrência do <see cref="T:System.Windows.Media.GradientStop" /> especificado dessa <see cref="T:System.Windows.Media.GradientStopCollection" />.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.GradientStop" /> a ser removido deste <see cref="T:System.Windows.Media.GradientStopCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> tiver sido removido da <see cref="T:System.Windows.Media.GradientStopCollection" />, caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.GradientStopCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.GradientStopCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x0600242E RID: 9262 RVA: 0x00091900 File Offset: 0x00090D00
		public bool Remove(GradientStop value)
		{
			base.WritePreamble();
			int num = this.IndexOf(value);
			if (num >= 0)
			{
				GradientStop oldValue = this._collection[num];
				base.OnFreezablePropertyChanged(oldValue, null);
				this._collection.RemoveAt(num);
				this._version += 1U;
				base.WritePostscript();
				return true;
			}
			return false;
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.GradientStop" /> no índice especificado dessa <see cref="T:System.Windows.Media.GradientStopCollection" />.</summary>
		/// <param name="index">O índice do <see cref="T:System.Windows.Media.GradientStop" /> a ser removido.</param>
		// Token: 0x0600242F RID: 9263 RVA: 0x00091958 File Offset: 0x00090D58
		public void RemoveAt(int index)
		{
			this.RemoveAtWithoutFiringPublicEvents(index);
			base.WritePostscript();
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x00091974 File Offset: 0x00090D74
		internal void RemoveAtWithoutFiringPublicEvents(int index)
		{
			base.WritePreamble();
			GradientStop oldValue = this._collection[index];
			base.OnFreezablePropertyChanged(oldValue, null);
			this._collection.RemoveAt(index);
			this._version += 1U;
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.GradientStop" /> no índice de base zero especificado.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.GradientStop" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.GradientStop" /> no índice especificado.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> não é um índice válido no <see cref="T:System.Windows.Media.GradientStopCollection" />.</exception>
		/// <exception cref="T:System.NotSupportedException">O <see cref="T:System.Windows.Media.GradientStopCollection" /> é somente leitura.  
		///
		/// ou - 
		/// O <see cref="T:System.Windows.Media.GradientStopCollection" /> tem um tamanho fixo.</exception>
		// Token: 0x1700071E RID: 1822
		public GradientStop this[int index]
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
					GradientStop oldValue = this._collection[index];
					base.OnFreezablePropertyChanged(oldValue, value);
					this._collection[index] = value;
				}
				this._version += 1U;
				base.WritePostscript();
			}
		}

		/// <summary>Obtém o número de itens contidos em uma <see cref="T:System.Windows.Media.GradientStopCollection" />.</summary>
		/// <returns>O número de itens em um <see cref="T:System.Windows.Media.GradientStopCollection" />.</returns>
		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06002433 RID: 9267 RVA: 0x00091A44 File Offset: 0x00090E44
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._collection.Count;
			}
		}

		/// <summary>Copia todo o <see cref="T:System.Windows.Media.GradientStopCollection" /> em um <see cref="T:System.Array" /> unidimensional compatível, começando no índice especificado da matriz de destino.</summary>
		/// <param name="array">A matriz unidimensional que é o destino dos itens copiados do <see cref="T:System.Windows.Media.GradientStopCollection" />. A matriz deve ter indexação com base em zero.</param>
		/// <param name="index">O índice com base em zero em <paramref name="array" /> no qual a cópia começa.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> é multidimensional.  
		///
		/// ou - 
		/// O número de itens na origem <see cref="T:System.Windows.Media.GradientStopCollection" /> é maior do que o espaço disponível de <paramref name="index" /> até o final do <paramref name="array" /> de destino.</exception>
		// Token: 0x06002434 RID: 9268 RVA: 0x00091A64 File Offset: 0x00090E64
		public void CopyTo(GradientStop[] array, int index)
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

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06002435 RID: 9269 RVA: 0x00091AB4 File Offset: 0x00090EB4
		bool ICollection<GradientStop>.IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Retorna um enumerador que pode iterar pela coleção.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.GradientStopCollection.Enumerator" /> que pode iterar pela coleção.</returns>
		// Token: 0x06002436 RID: 9270 RVA: 0x00091AD0 File Offset: 0x00090ED0
		public GradientStopCollection.Enumerator GetEnumerator()
		{
			base.ReadPreamble();
			return new GradientStopCollection.Enumerator(this);
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x00091AEC File Offset: 0x00090EEC
		IEnumerator<GradientStop> IEnumerable<GradientStop>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.GradientStopCollection" /> for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06002438 RID: 9272 RVA: 0x00091B04 File Offset: 0x00090F04
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<GradientStop>)this).IsReadOnly;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.GradientStopCollection" /> tiver um valor fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06002439 RID: 9273 RVA: 0x00091B18 File Offset: 0x00090F18
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
		// Token: 0x17000723 RID: 1827
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
		/// <param name="value">O <see cref="T:System.Object" /> para adicionar ao <see cref="T:System.Windows.Media.GradientStopCollection" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido.</returns>
		// Token: 0x0600243C RID: 9276 RVA: 0x00091B64 File Offset: 0x00090F64
		int IList.Add(object value)
		{
			return this.AddHelper(this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.GradientStopCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Windows.Media.GradientStopCollection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600243D RID: 9277 RVA: 0x00091B80 File Offset: 0x00090F80
		bool IList.Contains(object value)
		{
			return this.Contains(value as GradientStop);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser localizado no <see cref="T:System.Windows.Media.GradientStopCollection" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x0600243E RID: 9278 RVA: 0x00091B9C File Offset: 0x00090F9C
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as GradientStop);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">O índice de base zero no qual o <see cref="T:System.Object" /> será inserido.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser inserido no <see cref="T:System.Windows.Media.GradientStopCollection" />.</param>
		// Token: 0x0600243F RID: 9279 RVA: 0x00091BB8 File Offset: 0x00090FB8
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.Cast(value));
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">O <see cref="T:System.Object" /> a ser removido de <see cref="T:System.Windows.Media.GradientStopCollection" />.</param>
		// Token: 0x06002440 RID: 9280 RVA: 0x00091BD4 File Offset: 0x00090FD4
		void IList.Remove(object value)
		{
			this.Remove(value as GradientStop);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">Um <see cref="T:System.Array" /> de base zero que recebe os itens copiados do <see cref="T:System.Windows.Media.GradientStopCollection" />.</param>
		/// <param name="index">A primeira posição no <see cref="T:System.Array" /> especificada para receber o conteúdo copiado.</param>
		// Token: 0x06002441 RID: 9281 RVA: 0x00091BF0 File Offset: 0x00090FF0
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
		///   <see langword="true" /> caso o acesso ao <see cref="T:System.Windows.Media.GradientStopCollection" /> seja sincronizado (thread-safe); do contrário, <see langword="false" />.</returns>
		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06002442 RID: 9282 RVA: 0x00091CC4 File Offset: 0x000910C4
		bool ICollection.IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.GradientStopCollection" />.</returns>
		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06002443 RID: 9283 RVA: 0x00091CEC File Offset: 0x000910EC
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
		// Token: 0x06002444 RID: 9284 RVA: 0x00091D00 File Offset: 0x00091100
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002445 RID: 9285 RVA: 0x00091D18 File Offset: 0x00091118
		internal static GradientStopCollection Empty
		{
			get
			{
				if (GradientStopCollection.s_empty == null)
				{
					GradientStopCollection gradientStopCollection = new GradientStopCollection();
					gradientStopCollection.Freeze();
					GradientStopCollection.s_empty = gradientStopCollection;
				}
				return GradientStopCollection.s_empty;
			}
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x00091D44 File Offset: 0x00091144
		internal GradientStop Internal_GetItem(int i)
		{
			return this._collection[i];
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x00091D60 File Offset: 0x00091160
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

		// Token: 0x06002448 RID: 9288 RVA: 0x00091DA8 File Offset: 0x000911A8
		private GradientStop Cast(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is GradientStop))
			{
				throw new ArgumentException(SR.Get("Collection_BadType", new object[]
				{
					base.GetType().Name,
					value.GetType().Name,
					"GradientStop"
				}));
			}
			return (GradientStop)value;
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x00091E0C File Offset: 0x0009120C
		private int AddHelper(GradientStop value)
		{
			int result = this.AddWithoutFiringPublicEvents(value);
			base.WritePostscript();
			return result;
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x00091E28 File Offset: 0x00091228
		internal int AddWithoutFiringPublicEvents(GradientStop value)
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

		// Token: 0x0600244B RID: 9291 RVA: 0x00091E78 File Offset: 0x00091278
		protected override Freezable CreateInstanceCore()
		{
			return new GradientStopCollection();
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x00091E8C File Offset: 0x0009128C
		protected override void CloneCore(Freezable source)
		{
			GradientStopCollection gradientStopCollection = (GradientStopCollection)source;
			base.CloneCore(source);
			int count = gradientStopCollection._collection.Count;
			this._collection = new FrugalStructList<GradientStop>(count);
			for (int i = 0; i < count; i++)
			{
				GradientStop gradientStop = gradientStopCollection._collection[i].Clone();
				base.OnFreezablePropertyChanged(null, gradientStop);
				this._collection.Add(gradientStop);
			}
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x00091EF4 File Offset: 0x000912F4
		protected override void CloneCurrentValueCore(Freezable source)
		{
			GradientStopCollection gradientStopCollection = (GradientStopCollection)source;
			base.CloneCurrentValueCore(source);
			int count = gradientStopCollection._collection.Count;
			this._collection = new FrugalStructList<GradientStop>(count);
			for (int i = 0; i < count; i++)
			{
				GradientStop gradientStop = gradientStopCollection._collection[i].CloneCurrentValue();
				base.OnFreezablePropertyChanged(null, gradientStop);
				this._collection.Add(gradientStop);
			}
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x00091F5C File Offset: 0x0009135C
		protected override void GetAsFrozenCore(Freezable source)
		{
			GradientStopCollection gradientStopCollection = (GradientStopCollection)source;
			base.GetAsFrozenCore(source);
			int count = gradientStopCollection._collection.Count;
			this._collection = new FrugalStructList<GradientStop>(count);
			for (int i = 0; i < count; i++)
			{
				GradientStop gradientStop = (GradientStop)gradientStopCollection._collection[i].GetAsFrozen();
				base.OnFreezablePropertyChanged(null, gradientStop);
				this._collection.Add(gradientStop);
			}
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x00091FC8 File Offset: 0x000913C8
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			GradientStopCollection gradientStopCollection = (GradientStopCollection)source;
			base.GetCurrentValueAsFrozenCore(source);
			int count = gradientStopCollection._collection.Count;
			this._collection = new FrugalStructList<GradientStop>(count);
			for (int i = 0; i < count; i++)
			{
				GradientStop gradientStop = (GradientStop)gradientStopCollection._collection[i].GetCurrentValueAsFrozen();
				base.OnFreezablePropertyChanged(null, gradientStop);
				this._collection.Add(gradientStop);
			}
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x00092034 File Offset: 0x00091434
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

		/// <summary>Cria uma representação de <see cref="T:System.String" /> deste <see cref="T:System.Windows.Media.GradientStopCollection" />.</summary>
		/// <returns>Retorna um <see cref="T:System.String" /> que contém os valores deste <see cref="T:System.Windows.Media.GradientStopCollection" />.</returns>
		// Token: 0x06002451 RID: 9297 RVA: 0x0009207C File Offset: 0x0009147C
		public override string ToString()
		{
			base.ReadPreamble();
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação de <see cref="T:System.String" /> deste <see cref="T:System.Windows.Media.GradientStopCollection" />.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura.</param>
		/// <returns>Retorna um <see cref="T:System.String" /> que contém os valores deste <see cref="T:System.Windows.Media.GradientStopCollection" />.</returns>
		// Token: 0x06002452 RID: 9298 RVA: 0x00092098 File Offset: 0x00091498
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
		// Token: 0x06002453 RID: 9299 RVA: 0x000920B4 File Offset: 0x000914B4
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(format, provider);
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x000920D0 File Offset: 0x000914D0
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

		/// <summary>Converte uma representação de <see cref="T:System.String" /> de uma GradientStopCollection no <see cref="T:System.Windows.Media.GradientStopCollection" /> equivalente.</summary>
		/// <param name="source">A representação de <see cref="T:System.String" /> da GradientStopCollection.</param>
		/// <returns>Retorna o <see cref="T:System.Windows.Media.GradientStopCollection" /> equivalente.</returns>
		// Token: 0x06002455 RID: 9301 RVA: 0x0009215C File Offset: 0x0009155C
		public static GradientStopCollection Parse(string source)
		{
			IFormatProvider invariantEnglishUS = TypeConverterHelper.InvariantEnglishUS;
			TokenizerHelper tokenizerHelper = new TokenizerHelper(source, invariantEnglishUS);
			GradientStopCollection gradientStopCollection = new GradientStopCollection();
			while (tokenizerHelper.NextToken())
			{
				GradientStop value = new GradientStop(Parsers.ParseColor(tokenizerHelper.GetCurrentToken(), invariantEnglishUS), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), invariantEnglishUS));
				gradientStopCollection.Add(value);
			}
			return gradientStopCollection;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GradientStopCollection" />.</summary>
		// Token: 0x06002456 RID: 9302 RVA: 0x000921B0 File Offset: 0x000915B0
		public GradientStopCollection()
		{
			this._collection = default(FrugalStructList<GradientStop>);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GradientStopCollection" /> que é inicialmente capaz de armazenar o número especificado de itens.</summary>
		/// <param name="capacity">O número de objetos <see cref="T:System.Windows.Media.GradientStop" /> que a coleção é capaz de armazenar inicialmente.</param>
		// Token: 0x06002457 RID: 9303 RVA: 0x000921D0 File Offset: 0x000915D0
		public GradientStopCollection(int capacity)
		{
			this._collection = new FrugalStructList<GradientStop>(capacity);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GradientStopCollection" />, que contém os elementos na coleção especificada.</summary>
		/// <param name="collection">A coleção a ser copiada.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> é <see langword="null" />.</exception>
		// Token: 0x06002458 RID: 9304 RVA: 0x000921F0 File Offset: 0x000915F0
		public GradientStopCollection(IEnumerable<GradientStop> collection)
		{
			base.WritePreamble();
			if (collection != null)
			{
				bool flag = true;
				ICollection<GradientStop> collection2 = collection as ICollection<GradientStop>;
				if (collection2 != null)
				{
					this._collection = new FrugalStructList<GradientStop>(collection2);
				}
				else
				{
					ICollection collection3 = collection as ICollection;
					if (collection3 != null)
					{
						this._collection = new FrugalStructList<GradientStop>(collection3);
					}
					else
					{
						this._collection = default(FrugalStructList<GradientStop>);
						foreach (GradientStop gradientStop in collection)
						{
							if (gradientStop == null)
							{
								throw new ArgumentException(SR.Get("Collection_NoNull"));
							}
							GradientStop gradientStop2 = gradientStop;
							base.OnFreezablePropertyChanged(null, gradientStop2);
							this._collection.Add(gradientStop2);
						}
						flag = false;
					}
				}
				if (flag)
				{
					foreach (GradientStop gradientStop3 in collection)
					{
						if (gradientStop3 == null)
						{
							throw new ArgumentException(SR.Get("Collection_NoNull"));
						}
						base.OnFreezablePropertyChanged(null, gradientStop3);
					}
				}
				base.WritePostscript();
				return;
			}
			throw new ArgumentNullException("collection");
		}

		// Token: 0x0400116C RID: 4460
		private static GradientStopCollection s_empty;

		// Token: 0x0400116D RID: 4461
		internal FrugalStructList<GradientStop> _collection;

		// Token: 0x0400116E RID: 4462
		internal uint _version;

		/// <summary>Enumera itens <see cref="T:System.Windows.Media.GradientStop" /> em um <see cref="T:System.Windows.Media.GradientStopCollection" />.</summary>
		// Token: 0x02000875 RID: 2165
		public struct Enumerator : IEnumerator, IEnumerator<GradientStop>, IDisposable
		{
			// Token: 0x06005789 RID: 22409 RVA: 0x00165CB4 File Offset: 0x001650B4
			internal Enumerator(GradientStopCollection list)
			{
				this._list = list;
				this._version = list._version;
				this._index = -1;
				this._current = null;
			}

			/// <summary>Este tipo ou membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
			// Token: 0x0600578A RID: 22410 RVA: 0x00165CE4 File Offset: 0x001650E4
			void IDisposable.Dispose()
			{
			}

			/// <summary>Avança o enumerador para o próximo elemento da coleção.</summary>
			/// <returns>
			///   <see langword="true" /> se o enumerador foi avançado com êxito para o próximo elemento; caso contrário, <see langword="false" />.</returns>
			// Token: 0x0600578B RID: 22411 RVA: 0x00165CF4 File Offset: 0x001650F4
			public bool MoveNext()
			{
				this._list.ReadPreamble();
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
				}
				if (this._index > -2 && this._index < this._list._collection.Count - 1)
				{
					GradientStopCollection list = this._list;
					int index = this._index + 1;
					this._index = index;
					this._current = list._collection[index];
					return true;
				}
				this._index = -2;
				return false;
			}

			/// <summary>Redefine o enumerador com sua posição inicial, que é antes do primeiro elemento da coleção.</summary>
			// Token: 0x0600578C RID: 22412 RVA: 0x00165D88 File Offset: 0x00165188
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
			// Token: 0x17001210 RID: 4624
			// (get) Token: 0x0600578D RID: 22413 RVA: 0x00165DCC File Offset: 0x001651CC
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Obtém o elemento atual na coleção.</summary>
			/// <returns>O elemento atual na coleção.</returns>
			// Token: 0x17001211 RID: 4625
			// (get) Token: 0x0600578E RID: 22414 RVA: 0x00165DE0 File Offset: 0x001651E0
			public GradientStop Current
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

			// Token: 0x04002891 RID: 10385
			private GradientStop _current;

			// Token: 0x04002892 RID: 10386
			private GradientStopCollection _list;

			// Token: 0x04002893 RID: 10387
			private uint _version;

			// Token: 0x04002894 RID: 10388
			private int _index;
		}
	}
}
