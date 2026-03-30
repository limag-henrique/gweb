using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> .</summary>
	// Token: 0x020004F8 RID: 1272
	public class Int64KeyFrameCollection : Freezable, IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int64KeyFrameCollection" />.</summary>
		// Token: 0x060039A5 RID: 14757 RVA: 0x000E4D9C File Offset: 0x000E419C
		public Int64KeyFrameCollection()
		{
			this._keyFrames = new List<Int64KeyFrame>(2);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.Int64KeyFrameCollection" /> vazio.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.Int64KeyFrameCollection" /> vazio.</returns>
		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x060039A6 RID: 14758 RVA: 0x000E4DBC File Offset: 0x000E41BC
		public static Int64KeyFrameCollection Empty
		{
			get
			{
				if (Int64KeyFrameCollection.s_emptyCollection == null)
				{
					Int64KeyFrameCollection int64KeyFrameCollection = new Int64KeyFrameCollection();
					int64KeyFrameCollection._keyFrames = new List<Int64KeyFrame>(0);
					int64KeyFrameCollection.Freeze();
					Int64KeyFrameCollection.s_emptyCollection = int64KeyFrameCollection;
				}
				return Int64KeyFrameCollection.s_emptyCollection;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.Int64KeyFrameCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060039A7 RID: 14759 RVA: 0x000E4DF4 File Offset: 0x000E41F4
		public new Int64KeyFrameCollection Clone()
		{
			return (Int64KeyFrameCollection)base.Clone();
		}

		/// <summary>Cria uma nova instância congelada de <see cref="T:System.Windows.Media.Animation.Int64KeyFrameCollection" />.</summary>
		/// <returns>Uma instância congelada de <see cref="T:System.Windows.Media.Animation.Int64KeyFrameCollection" />.</returns>
		// Token: 0x060039A8 RID: 14760 RVA: 0x000E4E0C File Offset: 0x000E420C
		protected override Freezable CreateInstanceCore()
		{
			return new Int64KeyFrameCollection();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.Int64KeyFrameCollection" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Int64KeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x060039A9 RID: 14761 RVA: 0x000E4E20 File Offset: 0x000E4220
		protected override void CloneCore(Freezable sourceFreezable)
		{
			Int64KeyFrameCollection int64KeyFrameCollection = (Int64KeyFrameCollection)sourceFreezable;
			base.CloneCore(sourceFreezable);
			int count = int64KeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Int64KeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Int64KeyFrame int64KeyFrame = (Int64KeyFrame)int64KeyFrameCollection._keyFrames[i].Clone();
				this._keyFrames.Add(int64KeyFrame);
				base.OnFreezablePropertyChanged(null, int64KeyFrame);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.Int64KeyFrameCollection" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Int64KeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x060039AA RID: 14762 RVA: 0x000E4E8C File Offset: 0x000E428C
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			Int64KeyFrameCollection int64KeyFrameCollection = (Int64KeyFrameCollection)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			int count = int64KeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Int64KeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Int64KeyFrame int64KeyFrame = (Int64KeyFrame)int64KeyFrameCollection._keyFrames[i].CloneCurrentValue();
				this._keyFrames.Add(int64KeyFrame);
				base.OnFreezablePropertyChanged(null, int64KeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.Int64KeyFrameCollection" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.Int64KeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x060039AB RID: 14763 RVA: 0x000E4EF8 File Offset: 0x000E42F8
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			Int64KeyFrameCollection int64KeyFrameCollection = (Int64KeyFrameCollection)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			int count = int64KeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Int64KeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Int64KeyFrame int64KeyFrame = (Int64KeyFrame)int64KeyFrameCollection._keyFrames[i].GetAsFrozen();
				this._keyFrames.Add(int64KeyFrame);
				base.OnFreezablePropertyChanged(null, int64KeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.Int64KeyFrameCollection" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Int64KeyFrameCollection" /> a ser copiado e congelado.</param>
		// Token: 0x060039AC RID: 14764 RVA: 0x000E4F64 File Offset: 0x000E4364
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			Int64KeyFrameCollection int64KeyFrameCollection = (Int64KeyFrameCollection)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			int count = int64KeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Int64KeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Int64KeyFrame int64KeyFrame = (Int64KeyFrame)int64KeyFrameCollection._keyFrames[i].GetCurrentValueAsFrozen();
				this._keyFrames.Add(int64KeyFrame);
				base.OnFreezablePropertyChanged(null, int64KeyFrame);
			}
		}

		/// <summary>Torna esta instância de <see cref="T:System.Windows.Media.Animation.Int64KeyFrameCollection" /> não modificável ou determina se ela pode ser tornada não modificável.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> se este método deve simplesmente determinar se esta instância pode ser congelada. <see langword="false" /> se esta instância, na verdade, deve congelar a si mesma quando este método é chamado.</param>
		/// <returns>Se <paramref name="isChecking" /> é true, este método retorna <see langword="true" /> caso esta instância possa se tornar somente leitura ou então <see langword="false" /> se ela não pode se tornar somente leitura. Se <paramref name="isChecking" /> é false, este método retorna <see langword="true" /> se esta instância agora é somente leitura ou <see langword="false" />, se ela não pode tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x060039AD RID: 14765 RVA: 0x000E4FD0 File Offset: 0x000E43D0
		protected override bool FreezeCore(bool isChecking)
		{
			bool flag = base.FreezeCore(isChecking);
			int num = 0;
			while (num < this._keyFrames.Count && flag)
			{
				flag &= Freezable.Freeze(this._keyFrames[num], isChecking);
				num++;
			}
			return flag;
		}

		/// <summary>Retorna um enumerador que pode iterar pela coleção.</summary>
		/// <returns>Um <see cref="T:System.Collections.IEnumerator" /> que pode iterar pela coleção.</returns>
		// Token: 0x060039AE RID: 14766 RVA: 0x000E5018 File Offset: 0x000E4418
		public IEnumerator GetEnumerator()
		{
			base.ReadPreamble();
			return this._keyFrames.GetEnumerator();
		}

		/// <summary>Obtém o número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.Int64KeyFrameCollection" />.</summary>
		/// <returns>O número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.Int64KeyFrameCollection" />.</returns>
		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x060039AF RID: 14767 RVA: 0x000E503C File Offset: 0x000E443C
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._keyFrames.Count;
			}
		}

		/// <summary>Obtém um valor que indica se o acesso à coleção é sincronizado (thread-safe).</summary>
		/// <returns>
		///   <see langword="true" /> Se o acesso à coleção é sincronizado (thread-safe); Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x060039B0 RID: 14768 RVA: 0x000E505C File Offset: 0x000E445C
		public bool IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Obtém um objeto que pode ser usado para sincronizar o acesso à coleção.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso à coleção.</returns>
		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x060039B1 RID: 14769 RVA: 0x000E5084 File Offset: 0x000E4484
		public object SyncRoot
		{
			get
			{
				base.ReadPreamble();
				return ((ICollection)this._keyFrames).SyncRoot;
			}
		}

		/// <summary>Copia os elementos do <see cref="T:System.Collections.ICollection" /> para um <see cref="T:System.Array" />, começando em um determinado índice <see cref="T:System.Array" />.</summary>
		/// <param name="array">O <see cref="T:System.Array" /> unidimensional que é o destino dos elementos copiados de <see cref="T:System.Collections.ICollection" />. O <see cref="T:System.Array" /> deve ter indexação com base em zero.</param>
		/// <param name="index">O índice com base em zero em <paramref name="array" /> no qual a cópia começa.</param>
		// Token: 0x060039B2 RID: 14770 RVA: 0x000E50A4 File Offset: 0x000E44A4
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			((ICollection)this._keyFrames).CopyTo(array, index);
		}

		/// <summary>Copia todos os objetos de <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		// Token: 0x060039B3 RID: 14771 RVA: 0x000E50C4 File Offset: 0x000E44C4
		public void CopyTo(Int64KeyFrame[] array, int index)
		{
			base.ReadPreamble();
			this._keyFrames.CopyTo(array, index);
		}

		/// <summary>Adiciona um item ao <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser adicionado ao <see cref="T:System.Collections.IList" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido, ou -1 para indicar que o item não foi inserido na coleção.</returns>
		// Token: 0x060039B4 RID: 14772 RVA: 0x000E50E4 File Offset: 0x000E44E4
		int IList.Add(object keyFrame)
		{
			return this.Add((Int64KeyFrame)keyFrame);
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> ao final da coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> a adicionar ao final da coleção.</param>
		/// <returns>O índice ao qual o <paramref name="keyFrame" /> foi adicionado.</returns>
		// Token: 0x060039B5 RID: 14773 RVA: 0x000E5100 File Offset: 0x000E4500
		public int Add(Int64KeyFrame keyFrame)
		{
			if (keyFrame == null)
			{
				throw new ArgumentNullException("keyFrame");
			}
			base.WritePreamble();
			base.OnFreezablePropertyChanged(null, keyFrame);
			this._keyFrames.Add(keyFrame);
			base.WritePostscript();
			return this._keyFrames.Count - 1;
		}

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> da coleção.</summary>
		// Token: 0x060039B6 RID: 14774 RVA: 0x000E5148 File Offset: 0x000E4548
		public void Clear()
		{
			base.WritePreamble();
			if (this._keyFrames.Count > 0)
			{
				for (int i = 0; i < this._keyFrames.Count; i++)
				{
					base.OnFreezablePropertyChanged(this._keyFrames[i], null);
				}
				this._keyFrames.Clear();
				base.WritePostscript();
			}
		}

		/// <summary>Determinará se o <see cref="T:System.Collections.IList" /> contiver um valor específico.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Collections.IList" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060039B7 RID: 14775 RVA: 0x000E51A4 File Offset: 0x000E45A4
		bool IList.Contains(object keyFrame)
		{
			return this.Contains((Int64KeyFrame)keyFrame);
		}

		/// <summary>Obtém um valor que indica se a coleção contém o <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> especificado.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="keyFrame" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060039B8 RID: 14776 RVA: 0x000E51C0 File Offset: 0x000E45C0
		public bool Contains(Int64KeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.Contains(keyFrame);
		}

		/// <summary>Determina o índice de um item específico em <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x060039B9 RID: 14777 RVA: 0x000E51E0 File Offset: 0x000E45E0
		int IList.IndexOf(object keyFrame)
		{
			return this.IndexOf((Int64KeyFrame)keyFrame);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="keyFrame" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x060039BA RID: 14778 RVA: 0x000E51FC File Offset: 0x000E45FC
		public int IndexOf(Int64KeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.IndexOf(keyFrame);
		}

		/// <summary>Insere um item na <see cref="T:System.Collections.IList" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor deve ser inserido.</param>
		/// <param name="keyFrame">O objeto a ser inserido no <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x060039BB RID: 14779 RVA: 0x000E521C File Offset: 0x000E461C
		void IList.Insert(int index, object keyFrame)
		{
			this.Insert(index, (Int64KeyFrame)keyFrame);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> é inserido.</param>
		/// <param name="keyFrame">O objeto <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> a ser inserido na coleção.</param>
		// Token: 0x060039BC RID: 14780 RVA: 0x000E5238 File Offset: 0x000E4638
		public void Insert(int index, Int64KeyFrame keyFrame)
		{
			if (keyFrame == null)
			{
				throw new ArgumentNullException("keyFrame");
			}
			base.WritePreamble();
			base.OnFreezablePropertyChanged(null, keyFrame);
			this._keyFrames.Insert(index, keyFrame);
			base.WritePostscript();
		}

		/// <summary>Obtém um valor que indica se o tamanho da coleção pode ser alterado.</summary>
		/// <returns>
		///   <see langword="true" /> Se a coleção está congelada; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x060039BD RID: 14781 RVA: 0x000E5274 File Offset: 0x000E4674
		public bool IsFixedSize
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Obtém um valor que indica se a coleção é somente leitura.</summary>
		/// <returns>
		///   <see langword="true" /> se a coleção for somente leitura; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x060039BE RID: 14782 RVA: 0x000E5290 File Offset: 0x000E4690
		public bool IsReadOnly
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Remove a primeira ocorrência de um objeto específico do <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a remover do <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x060039BF RID: 14783 RVA: 0x000E52AC File Offset: 0x000E46AC
		void IList.Remove(object keyFrame)
		{
			this.Remove((Int64KeyFrame)keyFrame);
		}

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> da coleção.</summary>
		/// <param name="keyFrame">Identifica o <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> a ser removido da coleção.</param>
		// Token: 0x060039C0 RID: 14784 RVA: 0x000E52C8 File Offset: 0x000E46C8
		public void Remove(Int64KeyFrame keyFrame)
		{
			base.WritePreamble();
			if (this._keyFrames.Contains(keyFrame))
			{
				base.OnFreezablePropertyChanged(keyFrame, null);
				this._keyFrames.Remove(keyFrame);
				base.WritePostscript();
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> a ser removido.</param>
		// Token: 0x060039C1 RID: 14785 RVA: 0x000E5304 File Offset: 0x000E4704
		public void RemoveAt(int index)
		{
			base.WritePreamble();
			base.OnFreezablePropertyChanged(this._keyFrames[index], null);
			this._keyFrames.RemoveAt(index);
			base.WritePostscript();
		}

		/// <summary>Obtém ou define o elemento no índice especificado.</summary>
		/// <param name="index">O índice com base em zero do elemento a ser obtido ou definido.</param>
		/// <returns>O elemento no índice especificado.</returns>
		// Token: 0x17000B9B RID: 2971
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (Int64KeyFrame)value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.Int64KeyFrameCollection.Count" />.</exception>
		// Token: 0x17000B9C RID: 2972
		public Int64KeyFrame this[int index]
		{
			get
			{
				base.ReadPreamble();
				return this._keyFrames[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "Int64KeyFrameCollection[{0}]", new object[]
					{
						index
					}));
				}
				base.WritePreamble();
				if (value != this._keyFrames[index])
				{
					base.OnFreezablePropertyChanged(this._keyFrames[index], value);
					this._keyFrames[index] = value;
					base.WritePostscript();
				}
			}
		}

		// Token: 0x040016BB RID: 5819
		private List<Int64KeyFrame> _keyFrames;

		// Token: 0x040016BC RID: 5820
		private static Int64KeyFrameCollection s_emptyCollection;
	}
}
