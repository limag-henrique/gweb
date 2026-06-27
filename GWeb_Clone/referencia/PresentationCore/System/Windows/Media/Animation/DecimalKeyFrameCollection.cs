using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" /> .</summary>
	// Token: 0x020004C1 RID: 1217
	public class DecimalKeyFrameCollection : Freezable, IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DecimalKeyFrameCollection" />.</summary>
		// Token: 0x0600372A RID: 14122 RVA: 0x000DC8D4 File Offset: 0x000DBCD4
		public DecimalKeyFrameCollection()
		{
			this._keyFrames = new List<DecimalKeyFrame>(2);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.DecimalKeyFrameCollection" /> vazio.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.DecimalKeyFrameCollection" /> vazio.</returns>
		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x0600372B RID: 14123 RVA: 0x000DC8F4 File Offset: 0x000DBCF4
		public static DecimalKeyFrameCollection Empty
		{
			get
			{
				if (DecimalKeyFrameCollection.s_emptyCollection == null)
				{
					DecimalKeyFrameCollection decimalKeyFrameCollection = new DecimalKeyFrameCollection();
					decimalKeyFrameCollection._keyFrames = new List<DecimalKeyFrame>(0);
					decimalKeyFrameCollection.Freeze();
					DecimalKeyFrameCollection.s_emptyCollection = decimalKeyFrameCollection;
				}
				return DecimalKeyFrameCollection.s_emptyCollection;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.DecimalKeyFrameCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x0600372C RID: 14124 RVA: 0x000DC92C File Offset: 0x000DBD2C
		public new DecimalKeyFrameCollection Clone()
		{
			return (DecimalKeyFrameCollection)base.Clone();
		}

		/// <summary>Cria uma nova instância congelada de <see cref="T:System.Windows.Media.Animation.DecimalKeyFrameCollection" />.</summary>
		/// <returns>Uma instância congelada de <see cref="T:System.Windows.Media.Animation.DecimalKeyFrameCollection" />.</returns>
		// Token: 0x0600372D RID: 14125 RVA: 0x000DC944 File Offset: 0x000DBD44
		protected override Freezable CreateInstanceCore()
		{
			return new DecimalKeyFrameCollection();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.DecimalKeyFrameCollection" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.DecimalKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x0600372E RID: 14126 RVA: 0x000DC958 File Offset: 0x000DBD58
		protected override void CloneCore(Freezable sourceFreezable)
		{
			DecimalKeyFrameCollection decimalKeyFrameCollection = (DecimalKeyFrameCollection)sourceFreezable;
			base.CloneCore(sourceFreezable);
			int count = decimalKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<DecimalKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				DecimalKeyFrame decimalKeyFrame = (DecimalKeyFrame)decimalKeyFrameCollection._keyFrames[i].Clone();
				this._keyFrames.Add(decimalKeyFrame);
				base.OnFreezablePropertyChanged(null, decimalKeyFrame);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.DecimalKeyFrameCollection" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.DecimalKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x0600372F RID: 14127 RVA: 0x000DC9C4 File Offset: 0x000DBDC4
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			DecimalKeyFrameCollection decimalKeyFrameCollection = (DecimalKeyFrameCollection)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			int count = decimalKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<DecimalKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				DecimalKeyFrame decimalKeyFrame = (DecimalKeyFrame)decimalKeyFrameCollection._keyFrames[i].CloneCurrentValue();
				this._keyFrames.Add(decimalKeyFrame);
				base.OnFreezablePropertyChanged(null, decimalKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.DecimalKeyFrameCollection" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.DecimalKeyFrameCollection" /> a ser clonado e congelado.</param>
		// Token: 0x06003730 RID: 14128 RVA: 0x000DCA30 File Offset: 0x000DBE30
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			DecimalKeyFrameCollection decimalKeyFrameCollection = (DecimalKeyFrameCollection)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			int count = decimalKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<DecimalKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				DecimalKeyFrame decimalKeyFrame = (DecimalKeyFrame)decimalKeyFrameCollection._keyFrames[i].GetAsFrozen();
				this._keyFrames.Add(decimalKeyFrame);
				base.OnFreezablePropertyChanged(null, decimalKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.DecimalKeyFrameCollection" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.DecimalKeyFrameCollection" /> a ser copiado e congelado.</param>
		// Token: 0x06003731 RID: 14129 RVA: 0x000DCA9C File Offset: 0x000DBE9C
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			DecimalKeyFrameCollection decimalKeyFrameCollection = (DecimalKeyFrameCollection)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			int count = decimalKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<DecimalKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				DecimalKeyFrame decimalKeyFrame = (DecimalKeyFrame)decimalKeyFrameCollection._keyFrames[i].GetCurrentValueAsFrozen();
				this._keyFrames.Add(decimalKeyFrame);
				base.OnFreezablePropertyChanged(null, decimalKeyFrame);
			}
		}

		/// <summary>Torna essa instância do <see cref="T:System.Windows.Media.Animation.DecimalKeyFrameCollection" /> somente leitura ou determina se ela pode ser tornada somente leitura.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> se este método deve simplesmente determinar se esta instância pode ser congelada. <see langword="false" /> se esta instância, na verdade, deve congelar a si mesma quando este método é chamado.</param>
		/// <returns>Se <paramref name="isChecking" /> for true, esse método retornará <see langword="true" /> se essa instância puder se tornar somente leitura ou <see langword="false" />, se ela não puder se tornar somente leitura. Se <paramref name="isChecking" /> for false, esse método retornará <see langword="true" /> se essa instância agora for somente leitura ou <see langword="false" />, se ela não puder se tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento desse objeto.</returns>
		// Token: 0x06003732 RID: 14130 RVA: 0x000DCB08 File Offset: 0x000DBF08
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
		// Token: 0x06003733 RID: 14131 RVA: 0x000DCB50 File Offset: 0x000DBF50
		public IEnumerator GetEnumerator()
		{
			base.ReadPreamble();
			return this._keyFrames.GetEnumerator();
		}

		/// <summary>Obtém o número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.DecimalKeyFrameCollection" />.</summary>
		/// <returns>O número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.DecimalKeyFrameCollection" />.</returns>
		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06003734 RID: 14132 RVA: 0x000DCB74 File Offset: 0x000DBF74
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
		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06003735 RID: 14133 RVA: 0x000DCB94 File Offset: 0x000DBF94
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
		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06003736 RID: 14134 RVA: 0x000DCBBC File Offset: 0x000DBFBC
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
		// Token: 0x06003737 RID: 14135 RVA: 0x000DCBDC File Offset: 0x000DBFDC
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			((ICollection)this._keyFrames).CopyTo(array, index);
		}

		/// <summary>Copia todos os objetos de <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		// Token: 0x06003738 RID: 14136 RVA: 0x000DCBFC File Offset: 0x000DBFFC
		public void CopyTo(DecimalKeyFrame[] array, int index)
		{
			base.ReadPreamble();
			this._keyFrames.CopyTo(array, index);
		}

		/// <summary>Adiciona um item ao <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser adicionado ao <see cref="T:System.Collections.IList" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido, ou -1 para indicar que o item não foi inserido na coleção.</returns>
		// Token: 0x06003739 RID: 14137 RVA: 0x000DCC1C File Offset: 0x000DC01C
		int IList.Add(object keyFrame)
		{
			return this.Add((DecimalKeyFrame)keyFrame);
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" /> ao final da coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" /> a adicionar ao final da coleção.</param>
		/// <returns>O índice ao qual o <paramref name="keyFrame" /> foi adicionado.</returns>
		// Token: 0x0600373A RID: 14138 RVA: 0x000DCC38 File Offset: 0x000DC038
		public int Add(DecimalKeyFrame keyFrame)
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

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" /> da coleção.</summary>
		// Token: 0x0600373B RID: 14139 RVA: 0x000DCC80 File Offset: 0x000DC080
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
		// Token: 0x0600373C RID: 14140 RVA: 0x000DCCDC File Offset: 0x000DC0DC
		bool IList.Contains(object keyFrame)
		{
			return this.Contains((DecimalKeyFrame)keyFrame);
		}

		/// <summary>Obtém um valor que indica se a coleção contém o <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" /> especificado.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="keyFrame" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600373D RID: 14141 RVA: 0x000DCCF8 File Offset: 0x000DC0F8
		public bool Contains(DecimalKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.Contains(keyFrame);
		}

		/// <summary>Determina o índice de um item específico em <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x0600373E RID: 14142 RVA: 0x000DCD18 File Offset: 0x000DC118
		int IList.IndexOf(object keyFrame)
		{
			return this.IndexOf((DecimalKeyFrame)keyFrame);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="keyFrame" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x0600373F RID: 14143 RVA: 0x000DCD34 File Offset: 0x000DC134
		public int IndexOf(DecimalKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.IndexOf(keyFrame);
		}

		/// <summary>Insere um item na <see cref="T:System.Collections.IList" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor deve ser inserido.</param>
		/// <param name="keyFrame">O objeto a ser inserido no <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06003740 RID: 14144 RVA: 0x000DCD54 File Offset: 0x000DC154
		void IList.Insert(int index, object keyFrame)
		{
			this.Insert(index, (DecimalKeyFrame)keyFrame);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" /> é inserido.</param>
		/// <param name="keyFrame">O objeto <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" /> a ser inserido na coleção.</param>
		// Token: 0x06003741 RID: 14145 RVA: 0x000DCD70 File Offset: 0x000DC170
		public void Insert(int index, DecimalKeyFrame keyFrame)
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
		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06003742 RID: 14146 RVA: 0x000DCDAC File Offset: 0x000DC1AC
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
		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06003743 RID: 14147 RVA: 0x000DCDC8 File Offset: 0x000DC1C8
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
		// Token: 0x06003744 RID: 14148 RVA: 0x000DCDE4 File Offset: 0x000DC1E4
		void IList.Remove(object keyFrame)
		{
			this.Remove((DecimalKeyFrame)keyFrame);
		}

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" /> da coleção.</summary>
		/// <param name="keyFrame">Identifica o <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" /> a ser removido da coleção.</param>
		// Token: 0x06003745 RID: 14149 RVA: 0x000DCE00 File Offset: 0x000DC200
		public void Remove(DecimalKeyFrame keyFrame)
		{
			base.WritePreamble();
			if (this._keyFrames.Contains(keyFrame))
			{
				base.OnFreezablePropertyChanged(keyFrame, null);
				this._keyFrames.Remove(keyFrame);
				base.WritePostscript();
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" /> a ser removido.</param>
		// Token: 0x06003746 RID: 14150 RVA: 0x000DCE3C File Offset: 0x000DC23C
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
		// Token: 0x17000B36 RID: 2870
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (DecimalKeyFrame)value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.DecimalKeyFrameCollection.Count" />.</exception>
		// Token: 0x17000B37 RID: 2871
		public DecimalKeyFrame this[int index]
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
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "DecimalKeyFrameCollection[{0}]", new object[]
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

		// Token: 0x04001679 RID: 5753
		private List<DecimalKeyFrame> _keyFrames;

		// Token: 0x0400167A RID: 5754
		private static DecimalKeyFrameCollection s_emptyCollection;
	}
}
