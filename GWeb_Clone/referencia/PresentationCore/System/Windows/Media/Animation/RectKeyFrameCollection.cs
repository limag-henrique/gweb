using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.RectKeyFrame" /> .</summary>
	// Token: 0x0200053B RID: 1339
	public class RectKeyFrameCollection : Freezable, IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.RectKeyFrameCollection" />.</summary>
		// Token: 0x06003D17 RID: 15639 RVA: 0x000F07EC File Offset: 0x000EFBEC
		public RectKeyFrameCollection()
		{
			this._keyFrames = new List<RectKeyFrame>(2);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.RectKeyFrameCollection" /> vazio.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.RectKeyFrameCollection" /> vazio.</returns>
		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06003D18 RID: 15640 RVA: 0x000F080C File Offset: 0x000EFC0C
		public static RectKeyFrameCollection Empty
		{
			get
			{
				if (RectKeyFrameCollection.s_emptyCollection == null)
				{
					RectKeyFrameCollection rectKeyFrameCollection = new RectKeyFrameCollection();
					rectKeyFrameCollection._keyFrames = new List<RectKeyFrame>(0);
					rectKeyFrameCollection.Freeze();
					RectKeyFrameCollection.s_emptyCollection = rectKeyFrameCollection;
				}
				return RectKeyFrameCollection.s_emptyCollection;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.RectKeyFrameCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003D19 RID: 15641 RVA: 0x000F0844 File Offset: 0x000EFC44
		public new RectKeyFrameCollection Clone()
		{
			return (RectKeyFrameCollection)base.Clone();
		}

		/// <summary>Cria uma nova instância congelada de <see cref="T:System.Windows.Media.Animation.RectKeyFrameCollection" />.</summary>
		/// <returns>Uma instância congelada de <see cref="T:System.Windows.Media.Animation.RectKeyFrameCollection" />.</returns>
		// Token: 0x06003D1A RID: 15642 RVA: 0x000F085C File Offset: 0x000EFC5C
		protected override Freezable CreateInstanceCore()
		{
			return new RectKeyFrameCollection();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.RectKeyFrameCollection" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.RectKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003D1B RID: 15643 RVA: 0x000F0870 File Offset: 0x000EFC70
		protected override void CloneCore(Freezable sourceFreezable)
		{
			RectKeyFrameCollection rectKeyFrameCollection = (RectKeyFrameCollection)sourceFreezable;
			base.CloneCore(sourceFreezable);
			int count = rectKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<RectKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				RectKeyFrame rectKeyFrame = (RectKeyFrame)rectKeyFrameCollection._keyFrames[i].Clone();
				this._keyFrames.Add(rectKeyFrame);
				base.OnFreezablePropertyChanged(null, rectKeyFrame);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.RectKeyFrameCollection" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.RectKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003D1C RID: 15644 RVA: 0x000F08DC File Offset: 0x000EFCDC
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			RectKeyFrameCollection rectKeyFrameCollection = (RectKeyFrameCollection)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			int count = rectKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<RectKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				RectKeyFrame rectKeyFrame = (RectKeyFrame)rectKeyFrameCollection._keyFrames[i].CloneCurrentValue();
				this._keyFrames.Add(rectKeyFrame);
				base.OnFreezablePropertyChanged(null, rectKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.RectKeyFrameCollection" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.RectKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003D1D RID: 15645 RVA: 0x000F0948 File Offset: 0x000EFD48
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			RectKeyFrameCollection rectKeyFrameCollection = (RectKeyFrameCollection)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			int count = rectKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<RectKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				RectKeyFrame rectKeyFrame = (RectKeyFrame)rectKeyFrameCollection._keyFrames[i].GetAsFrozen();
				this._keyFrames.Add(rectKeyFrame);
				base.OnFreezablePropertyChanged(null, rectKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.RectKeyFrameCollection" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.RectKeyFrameCollection" /> a ser copiado e congelado.</param>
		// Token: 0x06003D1E RID: 15646 RVA: 0x000F09B4 File Offset: 0x000EFDB4
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			RectKeyFrameCollection rectKeyFrameCollection = (RectKeyFrameCollection)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			int count = rectKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<RectKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				RectKeyFrame rectKeyFrame = (RectKeyFrame)rectKeyFrameCollection._keyFrames[i].GetCurrentValueAsFrozen();
				this._keyFrames.Add(rectKeyFrame);
				base.OnFreezablePropertyChanged(null, rectKeyFrame);
			}
		}

		/// <summary>Torna esta instância de <see cref="T:System.Windows.Media.Animation.RectKeyFrameCollection" /> não modificável ou determina se ela pode ser tornada não modificável.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> a ser verificado se esta instância puder ser congelada; <see langword="false" /> para congelar esta instância.</param>
		/// <returns>Se <paramref name="isChecking" /> é true, este método retorna <see langword="true" /> caso esta instância possa se tornar somente leitura ou então <see langword="false" /> se ela não pode se tornar somente leitura. Se <paramref name="isChecking" /> é false, este método retorna <see langword="true" /> se esta instância agora é somente leitura ou <see langword="false" />, se ela não pode tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x06003D1F RID: 15647 RVA: 0x000F0A20 File Offset: 0x000EFE20
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
		// Token: 0x06003D20 RID: 15648 RVA: 0x000F0A68 File Offset: 0x000EFE68
		public IEnumerator GetEnumerator()
		{
			base.ReadPreamble();
			return this._keyFrames.GetEnumerator();
		}

		/// <summary>Obtém o número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.RectKeyFrameCollection" />.</summary>
		/// <returns>O número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.RectKeyFrameCollection" />.</returns>
		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06003D21 RID: 15649 RVA: 0x000F0A8C File Offset: 0x000EFE8C
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
		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06003D22 RID: 15650 RVA: 0x000F0AAC File Offset: 0x000EFEAC
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
		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06003D23 RID: 15651 RVA: 0x000F0AD4 File Offset: 0x000EFED4
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
		// Token: 0x06003D24 RID: 15652 RVA: 0x000F0AF4 File Offset: 0x000EFEF4
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			((ICollection)this._keyFrames).CopyTo(array, index);
		}

		/// <summary>Copia todos os objetos de <see cref="T:System.Windows.Media.Animation.RectKeyFrame" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		// Token: 0x06003D25 RID: 15653 RVA: 0x000F0B14 File Offset: 0x000EFF14
		public void CopyTo(RectKeyFrame[] array, int index)
		{
			base.ReadPreamble();
			this._keyFrames.CopyTo(array, index);
		}

		/// <summary>Adiciona um item ao <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser adicionado ao <see cref="T:System.Collections.IList" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido, ou -1 para indicar que o item não foi inserido na coleção.</returns>
		// Token: 0x06003D26 RID: 15654 RVA: 0x000F0B34 File Offset: 0x000EFF34
		int IList.Add(object keyFrame)
		{
			return this.Add((RectKeyFrame)keyFrame);
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.RectKeyFrame" /> ao final da coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.RectKeyFrame" /> a adicionar ao final da coleção.</param>
		/// <returns>O índice ao qual o <paramref name="keyFrame" /> foi adicionado.</returns>
		// Token: 0x06003D27 RID: 15655 RVA: 0x000F0B50 File Offset: 0x000EFF50
		public int Add(RectKeyFrame keyFrame)
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

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Animation.RectKeyFrame" /> da coleção.</summary>
		// Token: 0x06003D28 RID: 15656 RVA: 0x000F0B98 File Offset: 0x000EFF98
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
		// Token: 0x06003D29 RID: 15657 RVA: 0x000F0BF4 File Offset: 0x000EFFF4
		bool IList.Contains(object keyFrame)
		{
			return this.Contains((RectKeyFrame)keyFrame);
		}

		/// <summary>Obtém um valor que indica se a coleção contém o <see cref="T:System.Windows.Media.Animation.RectKeyFrame" /> especificado.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.RectKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="keyFrame" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003D2A RID: 15658 RVA: 0x000F0C10 File Offset: 0x000F0010
		public bool Contains(RectKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.Contains(keyFrame);
		}

		/// <summary>Determina o índice de um item específico em <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06003D2B RID: 15659 RVA: 0x000F0C30 File Offset: 0x000F0030
		int IList.IndexOf(object keyFrame)
		{
			return this.IndexOf((RectKeyFrame)keyFrame);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Animation.RectKeyFrame" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.RectKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="keyFrame" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x06003D2C RID: 15660 RVA: 0x000F0C4C File Offset: 0x000F004C
		public int IndexOf(RectKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.IndexOf(keyFrame);
		}

		/// <summary>Insere um item na <see cref="T:System.Collections.IList" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor deve ser inserido.</param>
		/// <param name="keyFrame">O objeto a ser inserido no <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06003D2D RID: 15661 RVA: 0x000F0C6C File Offset: 0x000F006C
		void IList.Insert(int index, object keyFrame)
		{
			this.Insert(index, (RectKeyFrame)keyFrame);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Animation.RectKeyFrame" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Animation.RectKeyFrame" /> é inserido.</param>
		/// <param name="keyFrame">O objeto <see cref="T:System.Windows.Media.Animation.RectKeyFrame" /> a ser inserido na coleção.</param>
		// Token: 0x06003D2E RID: 15662 RVA: 0x000F0C88 File Offset: 0x000F0088
		public void Insert(int index, RectKeyFrame keyFrame)
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
		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06003D2F RID: 15663 RVA: 0x000F0CC4 File Offset: 0x000F00C4
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
		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06003D30 RID: 15664 RVA: 0x000F0CE0 File Offset: 0x000F00E0
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
		// Token: 0x06003D31 RID: 15665 RVA: 0x000F0CFC File Offset: 0x000F00FC
		void IList.Remove(object keyFrame)
		{
			this.Remove((RectKeyFrame)keyFrame);
		}

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.Animation.RectKeyFrame" /> da coleção.</summary>
		/// <param name="keyFrame">Identifica o <see cref="T:System.Windows.Media.Animation.RectKeyFrame" /> a ser removido da coleção.</param>
		// Token: 0x06003D32 RID: 15666 RVA: 0x000F0D18 File Offset: 0x000F0118
		public void Remove(RectKeyFrame keyFrame)
		{
			base.WritePreamble();
			if (this._keyFrames.Contains(keyFrame))
			{
				base.OnFreezablePropertyChanged(keyFrame, null);
				this._keyFrames.Remove(keyFrame);
				base.WritePostscript();
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.RectKeyFrame" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Animation.RectKeyFrame" /> a ser removido.</param>
		// Token: 0x06003D33 RID: 15667 RVA: 0x000F0D54 File Offset: 0x000F0154
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
		// Token: 0x17000C50 RID: 3152
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (RectKeyFrame)value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Animation.RectKeyFrame" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Animation.RectKeyFrame" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.RectKeyFrame" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.RectKeyFrameCollection.Count" />.</exception>
		// Token: 0x17000C51 RID: 3153
		public RectKeyFrame this[int index]
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
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "RectKeyFrameCollection[{0}]", new object[]
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

		// Token: 0x04001724 RID: 5924
		private List<RectKeyFrame> _keyFrames;

		// Token: 0x04001725 RID: 5925
		private static RectKeyFrameCollection s_emptyCollection;
	}
}
