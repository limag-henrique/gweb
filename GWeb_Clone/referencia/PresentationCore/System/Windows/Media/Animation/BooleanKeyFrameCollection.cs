using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" /> .</summary>
	// Token: 0x020004B1 RID: 1201
	public class BooleanKeyFrameCollection : Freezable, IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.BooleanKeyFrameCollection" />.</summary>
		// Token: 0x060035CB RID: 13771 RVA: 0x000D6B5C File Offset: 0x000D5F5C
		public BooleanKeyFrameCollection()
		{
			this._keyFrames = new List<BooleanKeyFrame>(2);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.BooleanKeyFrameCollection" /> vazio.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.BooleanKeyFrameCollection" /> vazio.</returns>
		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x060035CC RID: 13772 RVA: 0x000D6B7C File Offset: 0x000D5F7C
		public static BooleanKeyFrameCollection Empty
		{
			get
			{
				if (BooleanKeyFrameCollection.s_emptyCollection == null)
				{
					BooleanKeyFrameCollection booleanKeyFrameCollection = new BooleanKeyFrameCollection();
					booleanKeyFrameCollection._keyFrames = new List<BooleanKeyFrame>(0);
					booleanKeyFrameCollection.Freeze();
					BooleanKeyFrameCollection.s_emptyCollection = booleanKeyFrameCollection;
				}
				return BooleanKeyFrameCollection.s_emptyCollection;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.BooleanKeyFrameCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060035CD RID: 13773 RVA: 0x000D6BB4 File Offset: 0x000D5FB4
		public new BooleanKeyFrameCollection Clone()
		{
			return (BooleanKeyFrameCollection)base.Clone();
		}

		/// <summary>Cria uma nova instância congelada de <see cref="T:System.Windows.Media.Animation.BooleanKeyFrameCollection" />.</summary>
		/// <returns>Uma instância congelada de <see cref="T:System.Windows.Media.Animation.BooleanKeyFrameCollection" />.</returns>
		// Token: 0x060035CE RID: 13774 RVA: 0x000D6BCC File Offset: 0x000D5FCC
		protected override Freezable CreateInstanceCore()
		{
			return new BooleanKeyFrameCollection();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.BooleanKeyFrameCollection" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.BooleanKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x060035CF RID: 13775 RVA: 0x000D6BE0 File Offset: 0x000D5FE0
		protected override void CloneCore(Freezable sourceFreezable)
		{
			BooleanKeyFrameCollection booleanKeyFrameCollection = (BooleanKeyFrameCollection)sourceFreezable;
			base.CloneCore(sourceFreezable);
			int count = booleanKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<BooleanKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				BooleanKeyFrame booleanKeyFrame = (BooleanKeyFrame)booleanKeyFrameCollection._keyFrames[i].Clone();
				this._keyFrames.Add(booleanKeyFrame);
				base.OnFreezablePropertyChanged(null, booleanKeyFrame);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.BooleanKeyFrameCollection" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.BooleanKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x060035D0 RID: 13776 RVA: 0x000D6C4C File Offset: 0x000D604C
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			BooleanKeyFrameCollection booleanKeyFrameCollection = (BooleanKeyFrameCollection)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			int count = booleanKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<BooleanKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				BooleanKeyFrame booleanKeyFrame = (BooleanKeyFrame)booleanKeyFrameCollection._keyFrames[i].CloneCurrentValue();
				this._keyFrames.Add(booleanKeyFrame);
				base.OnFreezablePropertyChanged(null, booleanKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.BooleanKeyFrameCollection" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.BooleanKeyFrameCollection" /> a ser clonado e congelado.</param>
		// Token: 0x060035D1 RID: 13777 RVA: 0x000D6CB8 File Offset: 0x000D60B8
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			BooleanKeyFrameCollection booleanKeyFrameCollection = (BooleanKeyFrameCollection)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			int count = booleanKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<BooleanKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				BooleanKeyFrame booleanKeyFrame = (BooleanKeyFrame)booleanKeyFrameCollection._keyFrames[i].GetAsFrozen();
				this._keyFrames.Add(booleanKeyFrame);
				base.OnFreezablePropertyChanged(null, booleanKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.BooleanKeyFrameCollection" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.BooleanKeyFrameCollection" /> a ser copiado e congelado.</param>
		// Token: 0x060035D2 RID: 13778 RVA: 0x000D6D24 File Offset: 0x000D6124
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			BooleanKeyFrameCollection booleanKeyFrameCollection = (BooleanKeyFrameCollection)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			int count = booleanKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<BooleanKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				BooleanKeyFrame booleanKeyFrame = (BooleanKeyFrame)booleanKeyFrameCollection._keyFrames[i].GetCurrentValueAsFrozen();
				this._keyFrames.Add(booleanKeyFrame);
				base.OnFreezablePropertyChanged(null, booleanKeyFrame);
			}
		}

		/// <summary>Torna essa instância do <see cref="T:System.Windows.Media.Animation.BooleanKeyFrameCollection" /> somente leitura ou determina se ela pode ser tornada somente leitura.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> se este método deve simplesmente determinar se esta instância pode ser congelada. <see langword="false" /> se esta instância, na verdade, deve congelar a si mesma quando este método é chamado.</param>
		/// <returns>Se <paramref name="isChecking" /> for true, esse método retornará <see langword="true" /> se essa instância puder se tornar somente leitura ou <see langword="false" />, se ela não puder se tornar somente leitura. Se <paramref name="isChecking" /> for false, esse método retornará <see langword="true" /> se essa instância agora for somente leitura ou <see langword="false" />, se ela não puder se tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento desse objeto.</returns>
		// Token: 0x060035D3 RID: 13779 RVA: 0x000D6D90 File Offset: 0x000D6190
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
		// Token: 0x060035D4 RID: 13780 RVA: 0x000D6DD8 File Offset: 0x000D61D8
		public IEnumerator GetEnumerator()
		{
			base.ReadPreamble();
			return this._keyFrames.GetEnumerator();
		}

		/// <summary>Obtém o número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.BooleanKeyFrameCollection" />.</summary>
		/// <returns>O número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.BooleanKeyFrameCollection" />.</returns>
		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x060035D5 RID: 13781 RVA: 0x000D6DFC File Offset: 0x000D61FC
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
		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x060035D6 RID: 13782 RVA: 0x000D6E1C File Offset: 0x000D621C
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
		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x060035D7 RID: 13783 RVA: 0x000D6E44 File Offset: 0x000D6244
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
		// Token: 0x060035D8 RID: 13784 RVA: 0x000D6E64 File Offset: 0x000D6264
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			((ICollection)this._keyFrames).CopyTo(array, index);
		}

		/// <summary>Copia todos os objetos de <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		// Token: 0x060035D9 RID: 13785 RVA: 0x000D6E84 File Offset: 0x000D6284
		public void CopyTo(BooleanKeyFrame[] array, int index)
		{
			base.ReadPreamble();
			this._keyFrames.CopyTo(array, index);
		}

		/// <summary>Adiciona um item ao <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser adicionado ao <see cref="T:System.Collections.IList" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido ou -1 para indicar que o item não foi inserido na coleção,</returns>
		// Token: 0x060035DA RID: 13786 RVA: 0x000D6EA4 File Offset: 0x000D62A4
		int IList.Add(object keyFrame)
		{
			return this.Add((BooleanKeyFrame)keyFrame);
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" /> ao final da coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" /> a adicionar ao final da coleção.</param>
		/// <returns>O índice ao qual o <paramref name="keyFrame" /> foi adicionado.</returns>
		// Token: 0x060035DB RID: 13787 RVA: 0x000D6EC0 File Offset: 0x000D62C0
		public int Add(BooleanKeyFrame keyFrame)
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

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" /> da coleção.</summary>
		// Token: 0x060035DC RID: 13788 RVA: 0x000D6F08 File Offset: 0x000D6308
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
		/// <returns>Informa <see langword="true" /> se o <see cref="T:System.Object" /> for encontrado no <see cref="T:System.Collections.IList" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060035DD RID: 13789 RVA: 0x000D6F64 File Offset: 0x000D6364
		bool IList.Contains(object keyFrame)
		{
			return this.Contains((BooleanKeyFrame)keyFrame);
		}

		/// <summary>Indica se a coleção contém o <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" /> especificado.</summary>
		/// <param name="keyFrame">O quadro-chave a localizar na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="keyFrame" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060035DE RID: 13790 RVA: 0x000D6F80 File Offset: 0x000D6380
		public bool Contains(BooleanKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.Contains(keyFrame);
		}

		/// <summary>Determina o índice de um item específico em <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x060035DF RID: 13791 RVA: 0x000D6FA0 File Offset: 0x000D63A0
		int IList.IndexOf(object keyFrame)
		{
			return this.IndexOf((BooleanKeyFrame)keyFrame);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="keyFrame" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x060035E0 RID: 13792 RVA: 0x000D6FBC File Offset: 0x000D63BC
		public int IndexOf(BooleanKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.IndexOf(keyFrame);
		}

		/// <summary>Insere um item na <see cref="T:System.Collections.IList" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor deve ser inserido.</param>
		/// <param name="keyFrame">O objeto a ser inserido no <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x060035E1 RID: 13793 RVA: 0x000D6FDC File Offset: 0x000D63DC
		void IList.Insert(int index, object keyFrame)
		{
			this.Insert(index, (BooleanKeyFrame)keyFrame);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" /> é inserido.</param>
		/// <param name="keyFrame">O objeto <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" /> a ser inserido na coleção.</param>
		// Token: 0x060035E2 RID: 13794 RVA: 0x000D6FF8 File Offset: 0x000D63F8
		public void Insert(int index, BooleanKeyFrame keyFrame)
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
		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x060035E3 RID: 13795 RVA: 0x000D7034 File Offset: 0x000D6434
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
		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x060035E4 RID: 13796 RVA: 0x000D7050 File Offset: 0x000D6450
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
		// Token: 0x060035E5 RID: 13797 RVA: 0x000D706C File Offset: 0x000D646C
		void IList.Remove(object keyFrame)
		{
			this.Remove((BooleanKeyFrame)keyFrame);
		}

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" /> da coleção.</summary>
		/// <param name="keyFrame">Identifica o <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" /> a ser removido da coleção.</param>
		// Token: 0x060035E6 RID: 13798 RVA: 0x000D7088 File Offset: 0x000D6488
		public void Remove(BooleanKeyFrame keyFrame)
		{
			base.WritePreamble();
			if (this._keyFrames.Contains(keyFrame))
			{
				base.OnFreezablePropertyChanged(keyFrame, null);
				this._keyFrames.Remove(keyFrame);
				base.WritePostscript();
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" /> a ser removido.</param>
		// Token: 0x060035E7 RID: 13799 RVA: 0x000D70C4 File Offset: 0x000D64C4
		public void RemoveAt(int index)
		{
			base.WritePreamble();
			base.OnFreezablePropertyChanged(this._keyFrames[index], null);
			this._keyFrames.RemoveAt(index);
			base.WritePostscript();
		}

		/// <summary>Obtém ou define o elemento no índice especificado.</summary>
		/// <param name="index">O objeto a remover do <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O elemento no índice especificado.</returns>
		// Token: 0x17000AED RID: 2797
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (BooleanKeyFrame)value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.BooleanKeyFrameCollection.Count" />.</exception>
		// Token: 0x17000AEE RID: 2798
		public BooleanKeyFrame this[int index]
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
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "BooleanKeyFrameCollection[{0}]", new object[]
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

		// Token: 0x04001650 RID: 5712
		private List<BooleanKeyFrame> _keyFrames;

		// Token: 0x04001651 RID: 5713
		private static BooleanKeyFrameCollection s_emptyCollection;
	}
}
