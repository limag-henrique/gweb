using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> .</summary>
	// Token: 0x02000543 RID: 1347
	public class SingleKeyFrameCollection : Freezable, IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SingleKeyFrameCollection" />.</summary>
		// Token: 0x06003DD3 RID: 15827 RVA: 0x000F3964 File Offset: 0x000F2D64
		public SingleKeyFrameCollection()
		{
			this._keyFrames = new List<SingleKeyFrame>(2);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.SingleKeyFrameCollection" /> vazio.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.SingleKeyFrameCollection" /> vazio.</returns>
		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06003DD4 RID: 15828 RVA: 0x000F3984 File Offset: 0x000F2D84
		public static SingleKeyFrameCollection Empty
		{
			get
			{
				if (SingleKeyFrameCollection.s_emptyCollection == null)
				{
					SingleKeyFrameCollection singleKeyFrameCollection = new SingleKeyFrameCollection();
					singleKeyFrameCollection._keyFrames = new List<SingleKeyFrame>(0);
					singleKeyFrameCollection.Freeze();
					SingleKeyFrameCollection.s_emptyCollection = singleKeyFrameCollection;
				}
				return SingleKeyFrameCollection.s_emptyCollection;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.SingleKeyFrameCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003DD5 RID: 15829 RVA: 0x000F39BC File Offset: 0x000F2DBC
		public new SingleKeyFrameCollection Clone()
		{
			return (SingleKeyFrameCollection)base.Clone();
		}

		/// <summary>Cria uma nova instância congelada de <see cref="T:System.Windows.Media.Animation.SingleKeyFrameCollection" />.</summary>
		/// <returns>Uma instância congelada de <see cref="T:System.Windows.Media.Animation.SingleKeyFrameCollection" />.</returns>
		// Token: 0x06003DD6 RID: 15830 RVA: 0x000F39D4 File Offset: 0x000F2DD4
		protected override Freezable CreateInstanceCore()
		{
			return new SingleKeyFrameCollection();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.SingleKeyFrameCollection" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.SingleKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003DD7 RID: 15831 RVA: 0x000F39E8 File Offset: 0x000F2DE8
		protected override void CloneCore(Freezable sourceFreezable)
		{
			SingleKeyFrameCollection singleKeyFrameCollection = (SingleKeyFrameCollection)sourceFreezable;
			base.CloneCore(sourceFreezable);
			int count = singleKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<SingleKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				SingleKeyFrame singleKeyFrame = (SingleKeyFrame)singleKeyFrameCollection._keyFrames[i].Clone();
				this._keyFrames.Add(singleKeyFrame);
				base.OnFreezablePropertyChanged(null, singleKeyFrame);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.SingleKeyFrameCollection" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.SingleKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003DD8 RID: 15832 RVA: 0x000F3A54 File Offset: 0x000F2E54
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			SingleKeyFrameCollection singleKeyFrameCollection = (SingleKeyFrameCollection)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			int count = singleKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<SingleKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				SingleKeyFrame singleKeyFrame = (SingleKeyFrame)singleKeyFrameCollection._keyFrames[i].CloneCurrentValue();
				this._keyFrames.Add(singleKeyFrame);
				base.OnFreezablePropertyChanged(null, singleKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.SingleKeyFrameCollection" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.SingleKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003DD9 RID: 15833 RVA: 0x000F3AC0 File Offset: 0x000F2EC0
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			SingleKeyFrameCollection singleKeyFrameCollection = (SingleKeyFrameCollection)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			int count = singleKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<SingleKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				SingleKeyFrame singleKeyFrame = (SingleKeyFrame)singleKeyFrameCollection._keyFrames[i].GetAsFrozen();
				this._keyFrames.Add(singleKeyFrame);
				base.OnFreezablePropertyChanged(null, singleKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.SingleKeyFrameCollection" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.SingleKeyFrameCollection" /> a ser copiado e congelado.</param>
		// Token: 0x06003DDA RID: 15834 RVA: 0x000F3B2C File Offset: 0x000F2F2C
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			SingleKeyFrameCollection singleKeyFrameCollection = (SingleKeyFrameCollection)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			int count = singleKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<SingleKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				SingleKeyFrame singleKeyFrame = (SingleKeyFrame)singleKeyFrameCollection._keyFrames[i].GetCurrentValueAsFrozen();
				this._keyFrames.Add(singleKeyFrame);
				base.OnFreezablePropertyChanged(null, singleKeyFrame);
			}
		}

		/// <summary>Torna essa instância do <see cref="T:System.Windows.Media.Animation.SingleKeyFrameCollection" /> somente leitura ou determina se ela pode ser tornada somente leitura.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> a ser verificado se esta instância puder ser congelada; <see langword="false" /> para congelar esta instância.</param>
		/// <returns>Se <paramref name="isChecking" /> for true, esse método retornará <see langword="true" /> se essa instância puder se tornar somente leitura ou <see langword="false" />, se ela não puder se tornar somente leitura. Se <paramref name="isChecking" /> é false, este método retorna <see langword="true" /> se esta instância agora é somente leitura ou <see langword="false" />, se ela não pode tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x06003DDB RID: 15835 RVA: 0x000F3B98 File Offset: 0x000F2F98
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
		// Token: 0x06003DDC RID: 15836 RVA: 0x000F3BE0 File Offset: 0x000F2FE0
		public IEnumerator GetEnumerator()
		{
			base.ReadPreamble();
			return this._keyFrames.GetEnumerator();
		}

		/// <summary>Obtém o número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.SingleKeyFrameCollection" />.</summary>
		/// <returns>O número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.SingleKeyFrameCollection" />.</returns>
		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06003DDD RID: 15837 RVA: 0x000F3C04 File Offset: 0x000F3004
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
		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06003DDE RID: 15838 RVA: 0x000F3C24 File Offset: 0x000F3024
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
		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06003DDF RID: 15839 RVA: 0x000F3C4C File Offset: 0x000F304C
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
		// Token: 0x06003DE0 RID: 15840 RVA: 0x000F3C6C File Offset: 0x000F306C
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			((ICollection)this._keyFrames).CopyTo(array, index);
		}

		/// <summary>Copia todos os objetos de <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		// Token: 0x06003DE1 RID: 15841 RVA: 0x000F3C8C File Offset: 0x000F308C
		public void CopyTo(SingleKeyFrame[] array, int index)
		{
			base.ReadPreamble();
			this._keyFrames.CopyTo(array, index);
		}

		/// <summary>Adiciona um item ao <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser adicionado ao <see cref="T:System.Collections.IList" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido, ou -1 para indicar que o item não foi inserido na coleção.</returns>
		// Token: 0x06003DE2 RID: 15842 RVA: 0x000F3CAC File Offset: 0x000F30AC
		int IList.Add(object keyFrame)
		{
			return this.Add((SingleKeyFrame)keyFrame);
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> ao final da coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> a adicionar ao final da coleção.</param>
		/// <returns>O índice ao qual o <paramref name="keyFrame" /> foi adicionado.</returns>
		// Token: 0x06003DE3 RID: 15843 RVA: 0x000F3CC8 File Offset: 0x000F30C8
		public int Add(SingleKeyFrame keyFrame)
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

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> da coleção.</summary>
		// Token: 0x06003DE4 RID: 15844 RVA: 0x000F3D10 File Offset: 0x000F3110
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
		// Token: 0x06003DE5 RID: 15845 RVA: 0x000F3D6C File Offset: 0x000F316C
		bool IList.Contains(object keyFrame)
		{
			return this.Contains((SingleKeyFrame)keyFrame);
		}

		/// <summary>Obtém um valor que indica se a coleção contém o <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> especificado.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="keyFrame" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003DE6 RID: 15846 RVA: 0x000F3D88 File Offset: 0x000F3188
		public bool Contains(SingleKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.Contains(keyFrame);
		}

		/// <summary>Determina o índice de um item específico em <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06003DE7 RID: 15847 RVA: 0x000F3DA8 File Offset: 0x000F31A8
		int IList.IndexOf(object keyFrame)
		{
			return this.IndexOf((SingleKeyFrame)keyFrame);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="keyFrame" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x06003DE8 RID: 15848 RVA: 0x000F3DC4 File Offset: 0x000F31C4
		public int IndexOf(SingleKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.IndexOf(keyFrame);
		}

		/// <summary>Insere um item na <see cref="T:System.Collections.IList" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor deve ser inserido.</param>
		/// <param name="keyFrame">O objeto a ser inserido no <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06003DE9 RID: 15849 RVA: 0x000F3DE4 File Offset: 0x000F31E4
		void IList.Insert(int index, object keyFrame)
		{
			this.Insert(index, (SingleKeyFrame)keyFrame);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> é inserido.</param>
		/// <param name="keyFrame">O objeto <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> a ser inserido na coleção.</param>
		// Token: 0x06003DEA RID: 15850 RVA: 0x000F3E00 File Offset: 0x000F3200
		public void Insert(int index, SingleKeyFrame keyFrame)
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
		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06003DEB RID: 15851 RVA: 0x000F3E3C File Offset: 0x000F323C
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
		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06003DEC RID: 15852 RVA: 0x000F3E58 File Offset: 0x000F3258
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
		// Token: 0x06003DED RID: 15853 RVA: 0x000F3E74 File Offset: 0x000F3274
		void IList.Remove(object keyFrame)
		{
			this.Remove((SingleKeyFrame)keyFrame);
		}

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> da coleção.</summary>
		/// <param name="keyFrame">Identifica o <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> a ser removido da coleção.</param>
		// Token: 0x06003DEE RID: 15854 RVA: 0x000F3E90 File Offset: 0x000F3290
		public void Remove(SingleKeyFrame keyFrame)
		{
			base.WritePreamble();
			if (this._keyFrames.Contains(keyFrame))
			{
				base.OnFreezablePropertyChanged(keyFrame, null);
				this._keyFrames.Remove(keyFrame);
				base.WritePostscript();
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> a ser removido.</param>
		// Token: 0x06003DEF RID: 15855 RVA: 0x000F3ECC File Offset: 0x000F32CC
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
		// Token: 0x17000C78 RID: 3192
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (SingleKeyFrame)value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.SingleKeyFrameCollection.Count" />.</exception>
		// Token: 0x17000C79 RID: 3193
		public SingleKeyFrame this[int index]
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
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "SingleKeyFrameCollection[{0}]", new object[]
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

		// Token: 0x0400173C RID: 5948
		private List<SingleKeyFrame> _keyFrames;

		// Token: 0x0400173D RID: 5949
		private static SingleKeyFrameCollection s_emptyCollection;
	}
}
