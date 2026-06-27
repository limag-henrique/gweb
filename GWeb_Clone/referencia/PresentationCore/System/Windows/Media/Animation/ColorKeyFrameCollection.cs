using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" /> .</summary>
	// Token: 0x020004BD RID: 1213
	public class ColorKeyFrameCollection : Freezable, IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ColorKeyFrameCollection" />.</summary>
		// Token: 0x060036CC RID: 14028 RVA: 0x000DAF70 File Offset: 0x000DA370
		public ColorKeyFrameCollection()
		{
			this._keyFrames = new List<ColorKeyFrame>(2);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.ColorKeyFrameCollection" /> vazio.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.ColorKeyFrameCollection" /> vazio.</returns>
		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x060036CD RID: 14029 RVA: 0x000DAF90 File Offset: 0x000DA390
		public static ColorKeyFrameCollection Empty
		{
			get
			{
				if (ColorKeyFrameCollection.s_emptyCollection == null)
				{
					ColorKeyFrameCollection colorKeyFrameCollection = new ColorKeyFrameCollection();
					colorKeyFrameCollection._keyFrames = new List<ColorKeyFrame>(0);
					colorKeyFrameCollection.Freeze();
					ColorKeyFrameCollection.s_emptyCollection = colorKeyFrameCollection;
				}
				return ColorKeyFrameCollection.s_emptyCollection;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.ColorAnimationUsingKeyFrames" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060036CE RID: 14030 RVA: 0x000DAFC8 File Offset: 0x000DA3C8
		public new ColorKeyFrameCollection Clone()
		{
			return (ColorKeyFrameCollection)base.Clone();
		}

		/// <summary>Cria uma nova instância congelada de <see cref="T:System.Windows.Media.Animation.ColorKeyFrameCollection" />.</summary>
		/// <returns>Uma instância congelada de <see cref="T:System.Windows.Media.Animation.ColorKeyFrameCollection" />.</returns>
		// Token: 0x060036CF RID: 14031 RVA: 0x000DAFE0 File Offset: 0x000DA3E0
		protected override Freezable CreateInstanceCore()
		{
			return new ColorKeyFrameCollection();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.ColorKeyFrameCollection" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.ColorKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x060036D0 RID: 14032 RVA: 0x000DAFF4 File Offset: 0x000DA3F4
		protected override void CloneCore(Freezable sourceFreezable)
		{
			ColorKeyFrameCollection colorKeyFrameCollection = (ColorKeyFrameCollection)sourceFreezable;
			base.CloneCore(sourceFreezable);
			int count = colorKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<ColorKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				ColorKeyFrame colorKeyFrame = (ColorKeyFrame)colorKeyFrameCollection._keyFrames[i].Clone();
				this._keyFrames.Add(colorKeyFrame);
				base.OnFreezablePropertyChanged(null, colorKeyFrame);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.ColorKeyFrameCollection" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.ColorKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x060036D1 RID: 14033 RVA: 0x000DB060 File Offset: 0x000DA460
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			ColorKeyFrameCollection colorKeyFrameCollection = (ColorKeyFrameCollection)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			int count = colorKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<ColorKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				ColorKeyFrame colorKeyFrame = (ColorKeyFrame)colorKeyFrameCollection._keyFrames[i].CloneCurrentValue();
				this._keyFrames.Add(colorKeyFrame);
				base.OnFreezablePropertyChanged(null, colorKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.ColorKeyFrameCollection" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.ColorKeyFrameCollection" /> a ser clonado e congelado.</param>
		// Token: 0x060036D2 RID: 14034 RVA: 0x000DB0CC File Offset: 0x000DA4CC
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			ColorKeyFrameCollection colorKeyFrameCollection = (ColorKeyFrameCollection)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			int count = colorKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<ColorKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				ColorKeyFrame colorKeyFrame = (ColorKeyFrame)colorKeyFrameCollection._keyFrames[i].GetAsFrozen();
				this._keyFrames.Add(colorKeyFrame);
				base.OnFreezablePropertyChanged(null, colorKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.ColorKeyFrameCollection" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.ColorKeyFrameCollection" /> a ser copiado e congelado.</param>
		// Token: 0x060036D3 RID: 14035 RVA: 0x000DB138 File Offset: 0x000DA538
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			ColorKeyFrameCollection colorKeyFrameCollection = (ColorKeyFrameCollection)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			int count = colorKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<ColorKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				ColorKeyFrame colorKeyFrame = (ColorKeyFrame)colorKeyFrameCollection._keyFrames[i].GetCurrentValueAsFrozen();
				this._keyFrames.Add(colorKeyFrame);
				base.OnFreezablePropertyChanged(null, colorKeyFrame);
			}
		}

		/// <summary>Torna essa instância do <see cref="T:System.Windows.Media.Animation.ColorKeyFrameCollection" /> somente leitura ou determina se ela pode ser tornada somente leitura.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> se este método deve simplesmente determinar se esta instância pode ser congelada. <see langword="false" /> se esta instância, na verdade, deve congelar a si mesma quando este método é chamado.</param>
		/// <returns>Se <paramref name="isChecking" /> for true, esse método retornará <see langword="true" /> se essa instância puder se tornar somente leitura ou <see langword="false" />, se ela não puder se tornar somente leitura. Se <paramref name="isChecking" /> for false, esse método retornará <see langword="true" /> se essa instância agora for somente leitura ou <see langword="false" />, se ela não puder se tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento desse objeto.</returns>
		// Token: 0x060036D4 RID: 14036 RVA: 0x000DB1A4 File Offset: 0x000DA5A4
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
		// Token: 0x060036D5 RID: 14037 RVA: 0x000DB1EC File Offset: 0x000DA5EC
		public IEnumerator GetEnumerator()
		{
			base.ReadPreamble();
			return this._keyFrames.GetEnumerator();
		}

		/// <summary>Obtém o número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.ColorKeyFrameCollection" />.</summary>
		/// <returns>O número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.ColorKeyFrameCollection" />.</returns>
		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x060036D6 RID: 14038 RVA: 0x000DB210 File Offset: 0x000DA610
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
		///   <see langword="true" /> Se o acesso à coleção é sincronizado (thread-safe); Caso contrário, <see langword="false" />.  
		/// .</returns>
		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x060036D7 RID: 14039 RVA: 0x000DB230 File Offset: 0x000DA630
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
		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x060036D8 RID: 14040 RVA: 0x000DB258 File Offset: 0x000DA658
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
		// Token: 0x060036D9 RID: 14041 RVA: 0x000DB278 File Offset: 0x000DA678
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			((ICollection)this._keyFrames).CopyTo(array, index);
		}

		/// <summary>Copia todos os objetos de <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		// Token: 0x060036DA RID: 14042 RVA: 0x000DB298 File Offset: 0x000DA698
		public void CopyTo(ColorKeyFrame[] array, int index)
		{
			base.ReadPreamble();
			this._keyFrames.CopyTo(array, index);
		}

		/// <summary>Adiciona um item ao <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser adicionado ao <see cref="T:System.Collections.IList" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido, ou -1 para indicar que o item não foi inserido na coleção.</returns>
		// Token: 0x060036DB RID: 14043 RVA: 0x000DB2B8 File Offset: 0x000DA6B8
		int IList.Add(object keyFrame)
		{
			return this.Add((ColorKeyFrame)keyFrame);
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" /> ao final da coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" /> a adicionar ao final da coleção.</param>
		/// <returns>O índice ao qual o <paramref name="keyFrame" /> foi adicionado.</returns>
		// Token: 0x060036DC RID: 14044 RVA: 0x000DB2D4 File Offset: 0x000DA6D4
		public int Add(ColorKeyFrame keyFrame)
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

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" /> da coleção.</summary>
		// Token: 0x060036DD RID: 14045 RVA: 0x000DB31C File Offset: 0x000DA71C
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
		// Token: 0x060036DE RID: 14046 RVA: 0x000DB378 File Offset: 0x000DA778
		bool IList.Contains(object keyFrame)
		{
			return this.Contains((ColorKeyFrame)keyFrame);
		}

		/// <summary>Obtém um valor que indica se a coleção contém o <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" /> especificado.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="keyFrame" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060036DF RID: 14047 RVA: 0x000DB394 File Offset: 0x000DA794
		public bool Contains(ColorKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.Contains(keyFrame);
		}

		/// <summary>Determina o índice de um item específico em <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x060036E0 RID: 14048 RVA: 0x000DB3B4 File Offset: 0x000DA7B4
		int IList.IndexOf(object keyFrame)
		{
			return this.IndexOf((ColorKeyFrame)keyFrame);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="keyFrame" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x060036E1 RID: 14049 RVA: 0x000DB3D0 File Offset: 0x000DA7D0
		public int IndexOf(ColorKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.IndexOf(keyFrame);
		}

		/// <summary>Insere um item na <see cref="T:System.Collections.IList" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor deve ser inserido.</param>
		/// <param name="keyFrame">O objeto a ser inserido no <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x060036E2 RID: 14050 RVA: 0x000DB3F0 File Offset: 0x000DA7F0
		void IList.Insert(int index, object keyFrame)
		{
			this.Insert(index, (ColorKeyFrame)keyFrame);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" /> é inserido.</param>
		/// <param name="keyFrame">O objeto <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" /> a ser inserido na coleção.</param>
		// Token: 0x060036E3 RID: 14051 RVA: 0x000DB40C File Offset: 0x000DA80C
		public void Insert(int index, ColorKeyFrame keyFrame)
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
		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x060036E4 RID: 14052 RVA: 0x000DB448 File Offset: 0x000DA848
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
		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x060036E5 RID: 14053 RVA: 0x000DB464 File Offset: 0x000DA864
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
		// Token: 0x060036E6 RID: 14054 RVA: 0x000DB480 File Offset: 0x000DA880
		void IList.Remove(object keyFrame)
		{
			this.Remove((ColorKeyFrame)keyFrame);
		}

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" /> da coleção.</summary>
		/// <param name="keyFrame">Identifica o <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" /> a ser removido da coleção.</param>
		// Token: 0x060036E7 RID: 14055 RVA: 0x000DB49C File Offset: 0x000DA89C
		public void Remove(ColorKeyFrame keyFrame)
		{
			base.WritePreamble();
			if (this._keyFrames.Contains(keyFrame))
			{
				base.OnFreezablePropertyChanged(keyFrame, null);
				this._keyFrames.Remove(keyFrame);
				base.WritePostscript();
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" /> a ser removido.</param>
		// Token: 0x060036E8 RID: 14056 RVA: 0x000DB4D8 File Offset: 0x000DA8D8
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
		// Token: 0x17000B22 RID: 2850
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (ColorKeyFrame)value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.ColorKeyFrameCollection.Count" />.</exception>
		// Token: 0x17000B23 RID: 2851
		public ColorKeyFrame this[int index]
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
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "ColorKeyFrameCollection[{0}]", new object[]
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

		// Token: 0x0400166D RID: 5741
		private List<ColorKeyFrame> _keyFrames;

		// Token: 0x0400166E RID: 5742
		private static ColorKeyFrameCollection s_emptyCollection;
	}
}
