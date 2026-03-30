using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> .</summary>
	// Token: 0x020004F4 RID: 1268
	public class Int32KeyFrameCollection : Freezable, IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int32KeyFrameCollection" />.</summary>
		// Token: 0x06003947 RID: 14663 RVA: 0x000E3488 File Offset: 0x000E2888
		public Int32KeyFrameCollection()
		{
			this._keyFrames = new List<Int32KeyFrame>(2);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.Int32KeyFrameCollection" /> vazio.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.Int32KeyFrameCollection" /> vazio.</returns>
		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06003948 RID: 14664 RVA: 0x000E34A8 File Offset: 0x000E28A8
		public static Int32KeyFrameCollection Empty
		{
			get
			{
				if (Int32KeyFrameCollection.s_emptyCollection == null)
				{
					Int32KeyFrameCollection int32KeyFrameCollection = new Int32KeyFrameCollection();
					int32KeyFrameCollection._keyFrames = new List<Int32KeyFrame>(0);
					int32KeyFrameCollection.Freeze();
					Int32KeyFrameCollection.s_emptyCollection = int32KeyFrameCollection;
				}
				return Int32KeyFrameCollection.s_emptyCollection;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.Int32KeyFrameCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003949 RID: 14665 RVA: 0x000E34E0 File Offset: 0x000E28E0
		public new Int32KeyFrameCollection Clone()
		{
			return (Int32KeyFrameCollection)base.Clone();
		}

		/// <summary>Cria uma nova instância congelada de <see cref="T:System.Windows.Media.Animation.Int32KeyFrameCollection" />.</summary>
		/// <returns>Uma instância congelada de <see cref="T:System.Windows.Media.Animation.Int32KeyFrameCollection" />.</returns>
		// Token: 0x0600394A RID: 14666 RVA: 0x000E34F8 File Offset: 0x000E28F8
		protected override Freezable CreateInstanceCore()
		{
			return new Int32KeyFrameCollection();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.Int32KeyFrameCollection" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Int32KeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x0600394B RID: 14667 RVA: 0x000E350C File Offset: 0x000E290C
		protected override void CloneCore(Freezable sourceFreezable)
		{
			Int32KeyFrameCollection int32KeyFrameCollection = (Int32KeyFrameCollection)sourceFreezable;
			base.CloneCore(sourceFreezable);
			int count = int32KeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Int32KeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Int32KeyFrame int32KeyFrame = (Int32KeyFrame)int32KeyFrameCollection._keyFrames[i].Clone();
				this._keyFrames.Add(int32KeyFrame);
				base.OnFreezablePropertyChanged(null, int32KeyFrame);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.Int32KeyFrameCollection" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Int32KeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x0600394C RID: 14668 RVA: 0x000E3578 File Offset: 0x000E2978
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			Int32KeyFrameCollection int32KeyFrameCollection = (Int32KeyFrameCollection)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			int count = int32KeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Int32KeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Int32KeyFrame int32KeyFrame = (Int32KeyFrame)int32KeyFrameCollection._keyFrames[i].CloneCurrentValue();
				this._keyFrames.Add(int32KeyFrame);
				base.OnFreezablePropertyChanged(null, int32KeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.Int32KeyFrameCollection" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.Int32KeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x0600394D RID: 14669 RVA: 0x000E35E4 File Offset: 0x000E29E4
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			Int32KeyFrameCollection int32KeyFrameCollection = (Int32KeyFrameCollection)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			int count = int32KeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Int32KeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Int32KeyFrame int32KeyFrame = (Int32KeyFrame)int32KeyFrameCollection._keyFrames[i].GetAsFrozen();
				this._keyFrames.Add(int32KeyFrame);
				base.OnFreezablePropertyChanged(null, int32KeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.Int32KeyFrameCollection" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Int32KeyFrameCollection" /> a ser copiado e congelado.</param>
		// Token: 0x0600394E RID: 14670 RVA: 0x000E3650 File Offset: 0x000E2A50
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			Int32KeyFrameCollection int32KeyFrameCollection = (Int32KeyFrameCollection)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			int count = int32KeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Int32KeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Int32KeyFrame int32KeyFrame = (Int32KeyFrame)int32KeyFrameCollection._keyFrames[i].GetCurrentValueAsFrozen();
				this._keyFrames.Add(int32KeyFrame);
				base.OnFreezablePropertyChanged(null, int32KeyFrame);
			}
		}

		/// <summary>Torna esta instância de <see cref="T:System.Windows.Media.Animation.Int32KeyFrameCollection" /> não modificável ou determina se ela pode ser tornada não modificável.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> se este método deve simplesmente determinar se esta instância pode ser congelada. <see langword="false" /> se esta instância, na verdade, deve congelar a si mesma quando este método é chamado.</param>
		/// <returns>Se <paramref name="isChecking" /> é true, este método retorna <see langword="true" /> caso esta instância possa se tornar somente leitura ou então <see langword="false" /> se ela não pode se tornar somente leitura. Se <paramref name="isChecking" /> é false, este método retorna <see langword="true" /> se esta instância agora é somente leitura ou <see langword="false" />, se ela não pode tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x0600394F RID: 14671 RVA: 0x000E36BC File Offset: 0x000E2ABC
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
		// Token: 0x06003950 RID: 14672 RVA: 0x000E3704 File Offset: 0x000E2B04
		public IEnumerator GetEnumerator()
		{
			base.ReadPreamble();
			return this._keyFrames.GetEnumerator();
		}

		/// <summary>Obtém o número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.Int32KeyFrameCollection" />.</summary>
		/// <returns>O número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.Int32KeyFrameCollection" />.</returns>
		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06003951 RID: 14673 RVA: 0x000E3728 File Offset: 0x000E2B28
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
		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06003952 RID: 14674 RVA: 0x000E3748 File Offset: 0x000E2B48
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
		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06003953 RID: 14675 RVA: 0x000E3770 File Offset: 0x000E2B70
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
		// Token: 0x06003954 RID: 14676 RVA: 0x000E3790 File Offset: 0x000E2B90
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			((ICollection)this._keyFrames).CopyTo(array, index);
		}

		/// <summary>Copia todos os objetos de <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		// Token: 0x06003955 RID: 14677 RVA: 0x000E37B0 File Offset: 0x000E2BB0
		public void CopyTo(Int32KeyFrame[] array, int index)
		{
			base.ReadPreamble();
			this._keyFrames.CopyTo(array, index);
		}

		/// <summary>Adiciona um item ao <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser adicionado ao <see cref="T:System.Collections.IList" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido, ou -1 para indicar que o item não foi inserido na coleção.</returns>
		// Token: 0x06003956 RID: 14678 RVA: 0x000E37D0 File Offset: 0x000E2BD0
		int IList.Add(object keyFrame)
		{
			return this.Add((Int32KeyFrame)keyFrame);
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> ao final da coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> a adicionar ao final da coleção.</param>
		/// <returns>O índice ao qual o <paramref name="keyFrame" /> foi adicionado.</returns>
		// Token: 0x06003957 RID: 14679 RVA: 0x000E37EC File Offset: 0x000E2BEC
		public int Add(Int32KeyFrame keyFrame)
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

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> da coleção.</summary>
		// Token: 0x06003958 RID: 14680 RVA: 0x000E3834 File Offset: 0x000E2C34
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
		// Token: 0x06003959 RID: 14681 RVA: 0x000E3890 File Offset: 0x000E2C90
		bool IList.Contains(object keyFrame)
		{
			return this.Contains((Int32KeyFrame)keyFrame);
		}

		/// <summary>Obtém um valor que indica se a coleção contém o <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> especificado.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="keyFrame" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600395A RID: 14682 RVA: 0x000E38AC File Offset: 0x000E2CAC
		public bool Contains(Int32KeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.Contains(keyFrame);
		}

		/// <summary>Determina o índice de um item específico em <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x0600395B RID: 14683 RVA: 0x000E38CC File Offset: 0x000E2CCC
		int IList.IndexOf(object keyFrame)
		{
			return this.IndexOf((Int32KeyFrame)keyFrame);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="keyFrame" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x0600395C RID: 14684 RVA: 0x000E38E8 File Offset: 0x000E2CE8
		public int IndexOf(Int32KeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.IndexOf(keyFrame);
		}

		/// <summary>Insere um item na <see cref="T:System.Collections.IList" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor deve ser inserido.</param>
		/// <param name="keyFrame">O objeto a ser inserido no <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x0600395D RID: 14685 RVA: 0x000E3908 File Offset: 0x000E2D08
		void IList.Insert(int index, object keyFrame)
		{
			this.Insert(index, (Int32KeyFrame)keyFrame);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> é inserido.</param>
		/// <param name="keyFrame">O objeto <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> a ser inserido na coleção.</param>
		// Token: 0x0600395E RID: 14686 RVA: 0x000E3924 File Offset: 0x000E2D24
		public void Insert(int index, Int32KeyFrame keyFrame)
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

		/// <summary>Obtém um valor que indica se a coleção está congelada.</summary>
		/// <returns>
		///   <see langword="true" /> Se a coleção está congelada; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x0600395F RID: 14687 RVA: 0x000E3960 File Offset: 0x000E2D60
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
		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06003960 RID: 14688 RVA: 0x000E397C File Offset: 0x000E2D7C
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
		// Token: 0x06003961 RID: 14689 RVA: 0x000E3998 File Offset: 0x000E2D98
		void IList.Remove(object keyFrame)
		{
			this.Remove((Int32KeyFrame)keyFrame);
		}

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> da coleção.</summary>
		/// <param name="keyFrame">Identifica o <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> a ser removido da coleção.</param>
		// Token: 0x06003962 RID: 14690 RVA: 0x000E39B4 File Offset: 0x000E2DB4
		public void Remove(Int32KeyFrame keyFrame)
		{
			base.WritePreamble();
			if (this._keyFrames.Contains(keyFrame))
			{
				base.OnFreezablePropertyChanged(keyFrame, null);
				this._keyFrames.Remove(keyFrame);
				base.WritePostscript();
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> a ser removido.</param>
		// Token: 0x06003963 RID: 14691 RVA: 0x000E39F0 File Offset: 0x000E2DF0
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
		// Token: 0x17000B87 RID: 2951
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (Int32KeyFrame)value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.Int32KeyFrameCollection.Count" />.</exception>
		// Token: 0x17000B88 RID: 2952
		public Int32KeyFrame this[int index]
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
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "Int32KeyFrameCollection[{0}]", new object[]
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

		// Token: 0x040016AF RID: 5807
		private List<Int32KeyFrame> _keyFrames;

		// Token: 0x040016B0 RID: 5808
		private static Int32KeyFrameCollection s_emptyCollection;
	}
}
