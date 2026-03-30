using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" /> .</summary>
	// Token: 0x02000521 RID: 1313
	public class MatrixKeyFrameCollection : Freezable, IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.MatrixKeyFrameCollection" />.</summary>
		// Token: 0x06003B39 RID: 15161 RVA: 0x000E8B5C File Offset: 0x000E7F5C
		public MatrixKeyFrameCollection()
		{
			this._keyFrames = new List<MatrixKeyFrame>(2);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.MatrixKeyFrameCollection" /> vazio.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.MatrixKeyFrameCollection" /> vazio.</returns>
		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06003B3A RID: 15162 RVA: 0x000E8B7C File Offset: 0x000E7F7C
		public static MatrixKeyFrameCollection Empty
		{
			get
			{
				if (MatrixKeyFrameCollection.s_emptyCollection == null)
				{
					MatrixKeyFrameCollection matrixKeyFrameCollection = new MatrixKeyFrameCollection();
					matrixKeyFrameCollection._keyFrames = new List<MatrixKeyFrame>(0);
					matrixKeyFrameCollection.Freeze();
					MatrixKeyFrameCollection.s_emptyCollection = matrixKeyFrameCollection;
				}
				return MatrixKeyFrameCollection.s_emptyCollection;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.MatrixKeyFrameCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003B3B RID: 15163 RVA: 0x000E8BB4 File Offset: 0x000E7FB4
		public new MatrixKeyFrameCollection Clone()
		{
			return (MatrixKeyFrameCollection)base.Clone();
		}

		/// <summary>Cria uma nova instância congelada de <see cref="T:System.Windows.Media.Animation.MatrixKeyFrameCollection" />.</summary>
		/// <returns>Uma instância congelada de <see cref="T:System.Windows.Media.Animation.MatrixKeyFrameCollection" />.</returns>
		// Token: 0x06003B3C RID: 15164 RVA: 0x000E8BCC File Offset: 0x000E7FCC
		protected override Freezable CreateInstanceCore()
		{
			return new MatrixKeyFrameCollection();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.MatrixKeyFrameCollection" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.MatrixKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003B3D RID: 15165 RVA: 0x000E8BE0 File Offset: 0x000E7FE0
		protected override void CloneCore(Freezable sourceFreezable)
		{
			MatrixKeyFrameCollection matrixKeyFrameCollection = (MatrixKeyFrameCollection)sourceFreezable;
			base.CloneCore(sourceFreezable);
			int count = matrixKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<MatrixKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				MatrixKeyFrame matrixKeyFrame = (MatrixKeyFrame)matrixKeyFrameCollection._keyFrames[i].Clone();
				this._keyFrames.Add(matrixKeyFrame);
				base.OnFreezablePropertyChanged(null, matrixKeyFrame);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.MatrixKeyFrameCollection" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.MatrixKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003B3E RID: 15166 RVA: 0x000E8C4C File Offset: 0x000E804C
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			MatrixKeyFrameCollection matrixKeyFrameCollection = (MatrixKeyFrameCollection)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			int count = matrixKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<MatrixKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				MatrixKeyFrame matrixKeyFrame = (MatrixKeyFrame)matrixKeyFrameCollection._keyFrames[i].CloneCurrentValue();
				this._keyFrames.Add(matrixKeyFrame);
				base.OnFreezablePropertyChanged(null, matrixKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.MatrixKeyFrameCollection" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.MatrixKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003B3F RID: 15167 RVA: 0x000E8CB8 File Offset: 0x000E80B8
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			MatrixKeyFrameCollection matrixKeyFrameCollection = (MatrixKeyFrameCollection)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			int count = matrixKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<MatrixKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				MatrixKeyFrame matrixKeyFrame = (MatrixKeyFrame)matrixKeyFrameCollection._keyFrames[i].GetAsFrozen();
				this._keyFrames.Add(matrixKeyFrame);
				base.OnFreezablePropertyChanged(null, matrixKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.MatrixKeyFrameCollection" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.MatrixKeyFrameCollection" /> a ser copiado e congelado.</param>
		// Token: 0x06003B40 RID: 15168 RVA: 0x000E8D24 File Offset: 0x000E8124
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			MatrixKeyFrameCollection matrixKeyFrameCollection = (MatrixKeyFrameCollection)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			int count = matrixKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<MatrixKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				MatrixKeyFrame matrixKeyFrame = (MatrixKeyFrame)matrixKeyFrameCollection._keyFrames[i].GetCurrentValueAsFrozen();
				this._keyFrames.Add(matrixKeyFrame);
				base.OnFreezablePropertyChanged(null, matrixKeyFrame);
			}
		}

		/// <summary>Torna esta instância de <see cref="T:System.Windows.Media.Animation.MatrixKeyFrameCollection" /> não modificável ou determina se ela pode ser tornada não modificável.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> se este método deve simplesmente determinar se esta instância pode ser congelada. <see langword="false" /> se esta instância, na verdade, deve congelar a si mesma quando este método é chamado.</param>
		/// <returns>Se <paramref name="isChecking" /> é true, este método retorna <see langword="true" /> caso esta instância possa se tornar somente leitura ou então <see langword="false" /> se ela não pode se tornar somente leitura. Se <paramref name="isChecking" /> é false, este método retorna <see langword="true" /> se esta instância agora é somente leitura ou <see langword="false" />, se ela não pode tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x06003B41 RID: 15169 RVA: 0x000E8D90 File Offset: 0x000E8190
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
		// Token: 0x06003B42 RID: 15170 RVA: 0x000E8DD8 File Offset: 0x000E81D8
		public IEnumerator GetEnumerator()
		{
			base.ReadPreamble();
			return this._keyFrames.GetEnumerator();
		}

		/// <summary>Obtém o número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.MatrixKeyFrameCollection" />.</summary>
		/// <returns>O número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.MatrixKeyFrameCollection" />.</returns>
		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06003B43 RID: 15171 RVA: 0x000E8DFC File Offset: 0x000E81FC
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
		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06003B44 RID: 15172 RVA: 0x000E8E1C File Offset: 0x000E821C
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
		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06003B45 RID: 15173 RVA: 0x000E8E44 File Offset: 0x000E8244
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
		// Token: 0x06003B46 RID: 15174 RVA: 0x000E8E64 File Offset: 0x000E8264
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			((ICollection)this._keyFrames).CopyTo(array, index);
		}

		/// <summary>Copia todos os objetos de <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		// Token: 0x06003B47 RID: 15175 RVA: 0x000E8E84 File Offset: 0x000E8284
		public void CopyTo(MatrixKeyFrame[] array, int index)
		{
			base.ReadPreamble();
			this._keyFrames.CopyTo(array, index);
		}

		/// <summary>Adiciona um item ao <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser adicionado ao <see cref="T:System.Collections.IList" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido, ou -1 para indicar que o item não foi inserido na coleção.</returns>
		// Token: 0x06003B48 RID: 15176 RVA: 0x000E8EA4 File Offset: 0x000E82A4
		int IList.Add(object keyFrame)
		{
			return this.Add((MatrixKeyFrame)keyFrame);
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" /> ao final da coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" /> a adicionar ao final da coleção.</param>
		/// <returns>O índice ao qual o <paramref name="keyFrame" /> foi adicionado.</returns>
		// Token: 0x06003B49 RID: 15177 RVA: 0x000E8EC0 File Offset: 0x000E82C0
		public int Add(MatrixKeyFrame keyFrame)
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

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" /> da coleção.</summary>
		// Token: 0x06003B4A RID: 15178 RVA: 0x000E8F08 File Offset: 0x000E8308
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
		// Token: 0x06003B4B RID: 15179 RVA: 0x000E8F64 File Offset: 0x000E8364
		bool IList.Contains(object keyFrame)
		{
			return this.Contains((MatrixKeyFrame)keyFrame);
		}

		/// <summary>Obtém um valor que indica se a coleção contém o <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" /> especificado.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="keyFrame" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003B4C RID: 15180 RVA: 0x000E8F80 File Offset: 0x000E8380
		public bool Contains(MatrixKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.Contains(keyFrame);
		}

		/// <summary>Determina o índice de um item específico em <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06003B4D RID: 15181 RVA: 0x000E8FA0 File Offset: 0x000E83A0
		int IList.IndexOf(object keyFrame)
		{
			return this.IndexOf((MatrixKeyFrame)keyFrame);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="keyFrame" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x06003B4E RID: 15182 RVA: 0x000E8FBC File Offset: 0x000E83BC
		public int IndexOf(MatrixKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.IndexOf(keyFrame);
		}

		/// <summary>Insere um item na <see cref="T:System.Collections.IList" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor deve ser inserido.</param>
		/// <param name="keyFrame">O objeto a ser inserido no <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06003B4F RID: 15183 RVA: 0x000E8FDC File Offset: 0x000E83DC
		void IList.Insert(int index, object keyFrame)
		{
			this.Insert(index, (MatrixKeyFrame)keyFrame);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" /> é inserido.</param>
		/// <param name="keyFrame">O objeto <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" /> a ser inserido na coleção.</param>
		// Token: 0x06003B50 RID: 15184 RVA: 0x000E8FF8 File Offset: 0x000E83F8
		public void Insert(int index, MatrixKeyFrame keyFrame)
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
		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06003B51 RID: 15185 RVA: 0x000E9034 File Offset: 0x000E8434
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
		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06003B52 RID: 15186 RVA: 0x000E9050 File Offset: 0x000E8450
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
		// Token: 0x06003B53 RID: 15187 RVA: 0x000E906C File Offset: 0x000E846C
		void IList.Remove(object keyFrame)
		{
			this.Remove((MatrixKeyFrame)keyFrame);
		}

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" /> da coleção.</summary>
		/// <param name="keyFrame">Identifica o <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" /> a ser removido da coleção.</param>
		// Token: 0x06003B54 RID: 15188 RVA: 0x000E9088 File Offset: 0x000E8488
		public void Remove(MatrixKeyFrame keyFrame)
		{
			base.WritePreamble();
			if (this._keyFrames.Contains(keyFrame))
			{
				base.OnFreezablePropertyChanged(keyFrame, null);
				this._keyFrames.Remove(keyFrame);
				base.WritePostscript();
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" /> a ser removido.</param>
		// Token: 0x06003B55 RID: 15189 RVA: 0x000E90C4 File Offset: 0x000E84C4
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
		// Token: 0x17000BE8 RID: 3048
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (MatrixKeyFrame)value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.MatrixKeyFrameCollection.Count" />.</exception>
		// Token: 0x17000BE9 RID: 3049
		public MatrixKeyFrame this[int index]
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
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "MatrixKeyFrameCollection[{0}]", new object[]
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

		// Token: 0x040016EB RID: 5867
		private List<MatrixKeyFrame> _keyFrames;

		// Token: 0x040016EC RID: 5868
		private static MatrixKeyFrameCollection s_emptyCollection;
	}
}
