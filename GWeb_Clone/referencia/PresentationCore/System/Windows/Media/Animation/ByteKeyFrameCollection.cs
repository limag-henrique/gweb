using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" /> .</summary>
	// Token: 0x020004B5 RID: 1205
	public class ByteKeyFrameCollection : Freezable, IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ByteKeyFrameCollection" />.</summary>
		// Token: 0x06003629 RID: 13865 RVA: 0x000D846C File Offset: 0x000D786C
		public ByteKeyFrameCollection()
		{
			this._keyFrames = new List<ByteKeyFrame>(2);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.ByteKeyFrameCollection" /> vazio.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.ByteKeyFrameCollection" /> vazio.</returns>
		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x0600362A RID: 13866 RVA: 0x000D848C File Offset: 0x000D788C
		public static ByteKeyFrameCollection Empty
		{
			get
			{
				if (ByteKeyFrameCollection.s_emptyCollection == null)
				{
					ByteKeyFrameCollection byteKeyFrameCollection = new ByteKeyFrameCollection();
					byteKeyFrameCollection._keyFrames = new List<ByteKeyFrame>(0);
					byteKeyFrameCollection.Freeze();
					ByteKeyFrameCollection.s_emptyCollection = byteKeyFrameCollection;
				}
				return ByteKeyFrameCollection.s_emptyCollection;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.ByteKeyFrameCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x0600362B RID: 13867 RVA: 0x000D84C4 File Offset: 0x000D78C4
		public new ByteKeyFrameCollection Clone()
		{
			return (ByteKeyFrameCollection)base.Clone();
		}

		/// <summary>Cria uma nova instância congelada de <see cref="T:System.Windows.Media.Animation.ByteKeyFrameCollection" />.</summary>
		/// <returns>Uma instância congelada de <see cref="T:System.Windows.Media.Animation.ByteKeyFrameCollection" />.</returns>
		// Token: 0x0600362C RID: 13868 RVA: 0x000D84DC File Offset: 0x000D78DC
		protected override Freezable CreateInstanceCore()
		{
			return new ByteKeyFrameCollection();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.ByteKeyFrameCollection" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.ByteKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x0600362D RID: 13869 RVA: 0x000D84F0 File Offset: 0x000D78F0
		protected override void CloneCore(Freezable sourceFreezable)
		{
			ByteKeyFrameCollection byteKeyFrameCollection = (ByteKeyFrameCollection)sourceFreezable;
			base.CloneCore(sourceFreezable);
			int count = byteKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<ByteKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				ByteKeyFrame byteKeyFrame = (ByteKeyFrame)byteKeyFrameCollection._keyFrames[i].Clone();
				this._keyFrames.Add(byteKeyFrame);
				base.OnFreezablePropertyChanged(null, byteKeyFrame);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.ByteKeyFrameCollection" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.ByteKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x0600362E RID: 13870 RVA: 0x000D855C File Offset: 0x000D795C
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			ByteKeyFrameCollection byteKeyFrameCollection = (ByteKeyFrameCollection)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			int count = byteKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<ByteKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				ByteKeyFrame byteKeyFrame = (ByteKeyFrame)byteKeyFrameCollection._keyFrames[i].CloneCurrentValue();
				this._keyFrames.Add(byteKeyFrame);
				base.OnFreezablePropertyChanged(null, byteKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.ByteKeyFrameCollection" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.ByteKeyFrameCollection" /> a ser clonado e congelado.</param>
		// Token: 0x0600362F RID: 13871 RVA: 0x000D85C8 File Offset: 0x000D79C8
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			ByteKeyFrameCollection byteKeyFrameCollection = (ByteKeyFrameCollection)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			int count = byteKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<ByteKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				ByteKeyFrame byteKeyFrame = (ByteKeyFrame)byteKeyFrameCollection._keyFrames[i].GetAsFrozen();
				this._keyFrames.Add(byteKeyFrame);
				base.OnFreezablePropertyChanged(null, byteKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.ByteKeyFrameCollection" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.ByteKeyFrameCollection" /> a ser copiado e congelado.</param>
		// Token: 0x06003630 RID: 13872 RVA: 0x000D8634 File Offset: 0x000D7A34
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			ByteKeyFrameCollection byteKeyFrameCollection = (ByteKeyFrameCollection)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			int count = byteKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<ByteKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				ByteKeyFrame byteKeyFrame = (ByteKeyFrame)byteKeyFrameCollection._keyFrames[i].GetCurrentValueAsFrozen();
				this._keyFrames.Add(byteKeyFrame);
				base.OnFreezablePropertyChanged(null, byteKeyFrame);
			}
		}

		/// <summary>Torna essa instância do <see cref="T:System.Windows.Media.Animation.ByteKeyFrameCollection" /> somente leitura ou determina se ela pode ser tornada somente leitura.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> se este método deve simplesmente determinar se esta instância pode ser congelada. <see langword="false" /> se esta instância, na verdade, deve congelar a si mesma quando este método é chamado.</param>
		/// <returns>Se <paramref name="isChecking" /> for true, esse método retornará <see langword="true" /> se essa instância puder se tornar somente leitura ou <see langword="false" />, se ela não puder se tornar somente leitura. Se <paramref name="isChecking" /> for false, esse método retornará <see langword="true" /> se essa instância agora for somente leitura ou <see langword="false" />, se ela não puder se tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento desse objeto.</returns>
		// Token: 0x06003631 RID: 13873 RVA: 0x000D86A0 File Offset: 0x000D7AA0
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
		// Token: 0x06003632 RID: 13874 RVA: 0x000D86E8 File Offset: 0x000D7AE8
		public IEnumerator GetEnumerator()
		{
			base.ReadPreamble();
			return this._keyFrames.GetEnumerator();
		}

		/// <summary>Obtém o número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.ByteKeyFrameCollection" />.</summary>
		/// <returns>O número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.ByteKeyFrameCollection" />.</returns>
		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06003633 RID: 13875 RVA: 0x000D870C File Offset: 0x000D7B0C
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
		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06003634 RID: 13876 RVA: 0x000D872C File Offset: 0x000D7B2C
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
		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06003635 RID: 13877 RVA: 0x000D8754 File Offset: 0x000D7B54
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
		// Token: 0x06003636 RID: 13878 RVA: 0x000D8774 File Offset: 0x000D7B74
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			((ICollection)this._keyFrames).CopyTo(array, index);
		}

		/// <summary>Copia todos os objetos de <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		// Token: 0x06003637 RID: 13879 RVA: 0x000D8794 File Offset: 0x000D7B94
		public void CopyTo(ByteKeyFrame[] array, int index)
		{
			base.ReadPreamble();
			this._keyFrames.CopyTo(array, index);
		}

		/// <summary>Adiciona um item ao <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser adicionado ao <see cref="T:System.Collections.IList" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido, ou -1 para indicar que o item não foi inserido na coleção.</returns>
		// Token: 0x06003638 RID: 13880 RVA: 0x000D87B4 File Offset: 0x000D7BB4
		int IList.Add(object keyFrame)
		{
			return this.Add((ByteKeyFrame)keyFrame);
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" /> ao final da coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" /> a adicionar ao final da coleção.</param>
		/// <returns>O índice ao qual o <paramref name="keyFrame" /> foi adicionado.</returns>
		// Token: 0x06003639 RID: 13881 RVA: 0x000D87D0 File Offset: 0x000D7BD0
		public int Add(ByteKeyFrame keyFrame)
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

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" /> da coleção.</summary>
		// Token: 0x0600363A RID: 13882 RVA: 0x000D8818 File Offset: 0x000D7C18
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
		// Token: 0x0600363B RID: 13883 RVA: 0x000D8874 File Offset: 0x000D7C74
		bool IList.Contains(object keyFrame)
		{
			return this.Contains((ByteKeyFrame)keyFrame);
		}

		/// <summary>Indica se a coleção contém o <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" /> especificado.</summary>
		/// <param name="keyFrame">O quadro-chave a localizar na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="keyFrame" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600363C RID: 13884 RVA: 0x000D8890 File Offset: 0x000D7C90
		public bool Contains(ByteKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.Contains(keyFrame);
		}

		/// <summary>Determina o índice de um item específico em <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x0600363D RID: 13885 RVA: 0x000D88B0 File Offset: 0x000D7CB0
		int IList.IndexOf(object keyFrame)
		{
			return this.IndexOf((ByteKeyFrame)keyFrame);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="keyFrame" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x0600363E RID: 13886 RVA: 0x000D88CC File Offset: 0x000D7CCC
		public int IndexOf(ByteKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.IndexOf(keyFrame);
		}

		/// <summary>Insere um item na <see cref="T:System.Collections.IList" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor deve ser inserido.</param>
		/// <param name="keyFrame">O objeto a ser inserido no <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x0600363F RID: 13887 RVA: 0x000D88EC File Offset: 0x000D7CEC
		void IList.Insert(int index, object keyFrame)
		{
			this.Insert(index, (ByteKeyFrame)keyFrame);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" /> é inserido.</param>
		/// <param name="keyFrame">O objeto <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" /> a ser inserido na coleção.</param>
		// Token: 0x06003640 RID: 13888 RVA: 0x000D8908 File Offset: 0x000D7D08
		public void Insert(int index, ByteKeyFrame keyFrame)
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
		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06003641 RID: 13889 RVA: 0x000D8944 File Offset: 0x000D7D44
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
		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06003642 RID: 13890 RVA: 0x000D8960 File Offset: 0x000D7D60
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
		// Token: 0x06003643 RID: 13891 RVA: 0x000D897C File Offset: 0x000D7D7C
		void IList.Remove(object keyFrame)
		{
			this.Remove((ByteKeyFrame)keyFrame);
		}

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" /> da coleção.</summary>
		/// <param name="keyFrame">Identifica o <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" /> a ser removido da coleção.</param>
		// Token: 0x06003644 RID: 13892 RVA: 0x000D8998 File Offset: 0x000D7D98
		public void Remove(ByteKeyFrame keyFrame)
		{
			base.WritePreamble();
			if (this._keyFrames.Contains(keyFrame))
			{
				base.OnFreezablePropertyChanged(keyFrame, null);
				this._keyFrames.Remove(keyFrame);
				base.WritePostscript();
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" /> a ser removido.</param>
		// Token: 0x06003645 RID: 13893 RVA: 0x000D89D4 File Offset: 0x000D7DD4
		public void RemoveAt(int index)
		{
			base.WritePreamble();
			base.OnFreezablePropertyChanged(this._keyFrames[index], null);
			this._keyFrames.RemoveAt(index);
			base.WritePostscript();
		}

		/// <summary>Obtém ou define o elemento no índice especificado.</summary>
		/// <param name="index">O índice com base em zero do elemento a ser obtido ou definido</param>
		/// <returns>O elemento no índice especificado.</returns>
		// Token: 0x17000B01 RID: 2817
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (ByteKeyFrame)value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.ByteKeyFrameCollection.Count" />.</exception>
		// Token: 0x17000B02 RID: 2818
		public ByteKeyFrame this[int index]
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
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "ByteKeyFrameCollection[{0}]", new object[]
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

		// Token: 0x0400165C RID: 5724
		private List<ByteKeyFrame> _keyFrames;

		// Token: 0x0400165D RID: 5725
		private static ByteKeyFrameCollection s_emptyCollection;
	}
}
