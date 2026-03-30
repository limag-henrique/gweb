using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" /> .</summary>
	// Token: 0x020004F0 RID: 1264
	public class Int16KeyFrameCollection : Freezable, IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int16KeyFrameCollection" />.</summary>
		// Token: 0x060038E9 RID: 14569 RVA: 0x000E1B78 File Offset: 0x000E0F78
		public Int16KeyFrameCollection()
		{
			this._keyFrames = new List<Int16KeyFrame>(2);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.Int16KeyFrameCollection" /> vazio.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.Int16KeyFrameCollection" /> vazio.</returns>
		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x060038EA RID: 14570 RVA: 0x000E1B98 File Offset: 0x000E0F98
		public static Int16KeyFrameCollection Empty
		{
			get
			{
				if (Int16KeyFrameCollection.s_emptyCollection == null)
				{
					Int16KeyFrameCollection int16KeyFrameCollection = new Int16KeyFrameCollection();
					int16KeyFrameCollection._keyFrames = new List<Int16KeyFrame>(0);
					int16KeyFrameCollection.Freeze();
					Int16KeyFrameCollection.s_emptyCollection = int16KeyFrameCollection;
				}
				return Int16KeyFrameCollection.s_emptyCollection;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.Int16KeyFrameCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060038EB RID: 14571 RVA: 0x000E1BD0 File Offset: 0x000E0FD0
		public new Int16KeyFrameCollection Clone()
		{
			return (Int16KeyFrameCollection)base.Clone();
		}

		/// <summary>Cria uma nova instância congelada de <see cref="T:System.Windows.Media.Animation.Int16KeyFrameCollection" />.</summary>
		/// <returns>Uma instância congelada de <see cref="T:System.Windows.Media.Animation.Int16KeyFrameCollection" />.</returns>
		// Token: 0x060038EC RID: 14572 RVA: 0x000E1BE8 File Offset: 0x000E0FE8
		protected override Freezable CreateInstanceCore()
		{
			return new Int16KeyFrameCollection();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.Int16KeyFrameCollection" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Int16KeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x060038ED RID: 14573 RVA: 0x000E1BFC File Offset: 0x000E0FFC
		protected override void CloneCore(Freezable sourceFreezable)
		{
			Int16KeyFrameCollection int16KeyFrameCollection = (Int16KeyFrameCollection)sourceFreezable;
			base.CloneCore(sourceFreezable);
			int count = int16KeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Int16KeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Int16KeyFrame int16KeyFrame = (Int16KeyFrame)int16KeyFrameCollection._keyFrames[i].Clone();
				this._keyFrames.Add(int16KeyFrame);
				base.OnFreezablePropertyChanged(null, int16KeyFrame);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.Int16KeyFrameCollection" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Int16KeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x060038EE RID: 14574 RVA: 0x000E1C68 File Offset: 0x000E1068
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			Int16KeyFrameCollection int16KeyFrameCollection = (Int16KeyFrameCollection)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			int count = int16KeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Int16KeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Int16KeyFrame int16KeyFrame = (Int16KeyFrame)int16KeyFrameCollection._keyFrames[i].CloneCurrentValue();
				this._keyFrames.Add(int16KeyFrame);
				base.OnFreezablePropertyChanged(null, int16KeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.Int16KeyFrameCollection" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.Int16KeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x060038EF RID: 14575 RVA: 0x000E1CD4 File Offset: 0x000E10D4
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			Int16KeyFrameCollection int16KeyFrameCollection = (Int16KeyFrameCollection)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			int count = int16KeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Int16KeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Int16KeyFrame int16KeyFrame = (Int16KeyFrame)int16KeyFrameCollection._keyFrames[i].GetAsFrozen();
				this._keyFrames.Add(int16KeyFrame);
				base.OnFreezablePropertyChanged(null, int16KeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.Int16KeyFrameCollection" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Int16KeyFrameCollection" /> a ser copiado e congelado.</param>
		// Token: 0x060038F0 RID: 14576 RVA: 0x000E1D40 File Offset: 0x000E1140
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			Int16KeyFrameCollection int16KeyFrameCollection = (Int16KeyFrameCollection)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			int count = int16KeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Int16KeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Int16KeyFrame int16KeyFrame = (Int16KeyFrame)int16KeyFrameCollection._keyFrames[i].GetCurrentValueAsFrozen();
				this._keyFrames.Add(int16KeyFrame);
				base.OnFreezablePropertyChanged(null, int16KeyFrame);
			}
		}

		/// <summary>Torna essa instância do <see cref="T:System.Windows.Media.Animation.Int16KeyFrameCollection" /> somente leitura ou determina se ela pode ser tornada somente leitura.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> se este método deve simplesmente determinar se esta instância pode ser congelada. <see langword="false" /> se esta instância, na verdade, deve congelar a si mesma quando este método é chamado.</param>
		/// <returns>Se <paramref name="isChecking" /> for true, esse método retornará <see langword="true" /> se essa instância puder se tornar somente leitura ou <see langword="false" />, se ela não puder se tornar somente leitura. Se <paramref name="isChecking" /> é false, este método retorna <see langword="true" /> se esta instância agora é somente leitura ou <see langword="false" />, se ela não pode tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x060038F1 RID: 14577 RVA: 0x000E1DAC File Offset: 0x000E11AC
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
		// Token: 0x060038F2 RID: 14578 RVA: 0x000E1DF4 File Offset: 0x000E11F4
		public IEnumerator GetEnumerator()
		{
			base.ReadPreamble();
			return this._keyFrames.GetEnumerator();
		}

		/// <summary>Obtém o número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.Int16KeyFrameCollection" />.</summary>
		/// <returns>O número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.Int16KeyFrameCollection" />.</returns>
		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x060038F3 RID: 14579 RVA: 0x000E1E18 File Offset: 0x000E1218
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
		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x060038F4 RID: 14580 RVA: 0x000E1E38 File Offset: 0x000E1238
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
		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x060038F5 RID: 14581 RVA: 0x000E1E60 File Offset: 0x000E1260
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
		// Token: 0x060038F6 RID: 14582 RVA: 0x000E1E80 File Offset: 0x000E1280
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			((ICollection)this._keyFrames).CopyTo(array, index);
		}

		/// <summary>Copia todos os objetos de <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		// Token: 0x060038F7 RID: 14583 RVA: 0x000E1EA0 File Offset: 0x000E12A0
		public void CopyTo(Int16KeyFrame[] array, int index)
		{
			base.ReadPreamble();
			this._keyFrames.CopyTo(array, index);
		}

		/// <summary>Adiciona um item ao <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser adicionado ao <see cref="T:System.Collections.IList" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido, ou -1 para indicar que o item não foi inserido na coleção.</returns>
		// Token: 0x060038F8 RID: 14584 RVA: 0x000E1EC0 File Offset: 0x000E12C0
		int IList.Add(object keyFrame)
		{
			return this.Add((Int16KeyFrame)keyFrame);
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" /> ao final da coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" /> a adicionar ao final da coleção.</param>
		/// <returns>O índice ao qual o <paramref name="keyFrame" /> foi adicionado.</returns>
		// Token: 0x060038F9 RID: 14585 RVA: 0x000E1EDC File Offset: 0x000E12DC
		public int Add(Int16KeyFrame keyFrame)
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

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" /> da coleção.</summary>
		// Token: 0x060038FA RID: 14586 RVA: 0x000E1F24 File Offset: 0x000E1324
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
		// Token: 0x060038FB RID: 14587 RVA: 0x000E1F80 File Offset: 0x000E1380
		bool IList.Contains(object keyFrame)
		{
			return this.Contains((Int16KeyFrame)keyFrame);
		}

		/// <summary>Obtém um valor que indica se a coleção contém o <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" /> especificado.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="keyFrame" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060038FC RID: 14588 RVA: 0x000E1F9C File Offset: 0x000E139C
		public bool Contains(Int16KeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.Contains(keyFrame);
		}

		/// <summary>Determina o índice de um item específico em <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x060038FD RID: 14589 RVA: 0x000E1FBC File Offset: 0x000E13BC
		int IList.IndexOf(object keyFrame)
		{
			return this.IndexOf((Int16KeyFrame)keyFrame);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="keyFrame" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x060038FE RID: 14590 RVA: 0x000E1FD8 File Offset: 0x000E13D8
		public int IndexOf(Int16KeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.IndexOf(keyFrame);
		}

		/// <summary>Insere um item na <see cref="T:System.Collections.IList" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor deve ser inserido.</param>
		/// <param name="keyFrame">O objeto a ser inserido no <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x060038FF RID: 14591 RVA: 0x000E1FF8 File Offset: 0x000E13F8
		void IList.Insert(int index, object keyFrame)
		{
			this.Insert(index, (Int16KeyFrame)keyFrame);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" /> é inserido.</param>
		/// <param name="keyFrame">O objeto <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" /> a ser inserido na coleção.</param>
		// Token: 0x06003900 RID: 14592 RVA: 0x000E2014 File Offset: 0x000E1414
		public void Insert(int index, Int16KeyFrame keyFrame)
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
		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06003901 RID: 14593 RVA: 0x000E2050 File Offset: 0x000E1450
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
		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06003902 RID: 14594 RVA: 0x000E206C File Offset: 0x000E146C
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
		// Token: 0x06003903 RID: 14595 RVA: 0x000E2088 File Offset: 0x000E1488
		void IList.Remove(object keyFrame)
		{
			this.Remove((Int16KeyFrame)keyFrame);
		}

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" /> da coleção.</summary>
		/// <param name="keyFrame">Identifica o <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" /> a ser removido da coleção.</param>
		// Token: 0x06003904 RID: 14596 RVA: 0x000E20A4 File Offset: 0x000E14A4
		public void Remove(Int16KeyFrame keyFrame)
		{
			base.WritePreamble();
			if (this._keyFrames.Contains(keyFrame))
			{
				base.OnFreezablePropertyChanged(keyFrame, null);
				this._keyFrames.Remove(keyFrame);
				base.WritePostscript();
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" /> a ser removido.</param>
		// Token: 0x06003905 RID: 14597 RVA: 0x000E20E0 File Offset: 0x000E14E0
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
		// Token: 0x17000B73 RID: 2931
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (Int16KeyFrame)value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.Int16KeyFrameCollection.Count" />.</exception>
		// Token: 0x17000B74 RID: 2932
		public Int16KeyFrame this[int index]
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
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "Int16KeyFrameCollection[{0}]", new object[]
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

		// Token: 0x040016A3 RID: 5795
		private List<Int16KeyFrame> _keyFrames;

		// Token: 0x040016A4 RID: 5796
		private static Int16KeyFrameCollection s_emptyCollection;
	}
}
