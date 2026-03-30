using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.PointKeyFrame" /> .</summary>
	// Token: 0x02000530 RID: 1328
	public class PointKeyFrameCollection : Freezable, IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.PointKeyFrameCollection" />.</summary>
		// Token: 0x06003C4E RID: 15438 RVA: 0x000ED2D0 File Offset: 0x000EC6D0
		public PointKeyFrameCollection()
		{
			this._keyFrames = new List<PointKeyFrame>(2);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.PointKeyFrameCollection" /> vazio.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.PointKeyFrameCollection" /> vazio.</returns>
		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06003C4F RID: 15439 RVA: 0x000ED2F0 File Offset: 0x000EC6F0
		public static PointKeyFrameCollection Empty
		{
			get
			{
				if (PointKeyFrameCollection.s_emptyCollection == null)
				{
					PointKeyFrameCollection pointKeyFrameCollection = new PointKeyFrameCollection();
					pointKeyFrameCollection._keyFrames = new List<PointKeyFrame>(0);
					pointKeyFrameCollection.Freeze();
					PointKeyFrameCollection.s_emptyCollection = pointKeyFrameCollection;
				}
				return PointKeyFrameCollection.s_emptyCollection;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.PointKeyFrameCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003C50 RID: 15440 RVA: 0x000ED328 File Offset: 0x000EC728
		public new PointKeyFrameCollection Clone()
		{
			return (PointKeyFrameCollection)base.Clone();
		}

		/// <summary>Cria uma nova instância congelada de <see cref="T:System.Windows.Media.Animation.PointKeyFrameCollection" />.</summary>
		/// <returns>Uma instância congelada de <see cref="T:System.Windows.Media.Animation.PointKeyFrameCollection" />.</returns>
		// Token: 0x06003C51 RID: 15441 RVA: 0x000ED340 File Offset: 0x000EC740
		protected override Freezable CreateInstanceCore()
		{
			return new PointKeyFrameCollection();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.PointKeyFrameCollection" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.PointKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003C52 RID: 15442 RVA: 0x000ED354 File Offset: 0x000EC754
		protected override void CloneCore(Freezable sourceFreezable)
		{
			PointKeyFrameCollection pointKeyFrameCollection = (PointKeyFrameCollection)sourceFreezable;
			base.CloneCore(sourceFreezable);
			int count = pointKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<PointKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				PointKeyFrame pointKeyFrame = (PointKeyFrame)pointKeyFrameCollection._keyFrames[i].Clone();
				this._keyFrames.Add(pointKeyFrame);
				base.OnFreezablePropertyChanged(null, pointKeyFrame);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.PointKeyFrameCollection" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.PointKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003C53 RID: 15443 RVA: 0x000ED3C0 File Offset: 0x000EC7C0
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			PointKeyFrameCollection pointKeyFrameCollection = (PointKeyFrameCollection)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			int count = pointKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<PointKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				PointKeyFrame pointKeyFrame = (PointKeyFrame)pointKeyFrameCollection._keyFrames[i].CloneCurrentValue();
				this._keyFrames.Add(pointKeyFrame);
				base.OnFreezablePropertyChanged(null, pointKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.PointKeyFrameCollection" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.PointKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003C54 RID: 15444 RVA: 0x000ED42C File Offset: 0x000EC82C
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			PointKeyFrameCollection pointKeyFrameCollection = (PointKeyFrameCollection)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			int count = pointKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<PointKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				PointKeyFrame pointKeyFrame = (PointKeyFrame)pointKeyFrameCollection._keyFrames[i].GetAsFrozen();
				this._keyFrames.Add(pointKeyFrame);
				base.OnFreezablePropertyChanged(null, pointKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.PointKeyFrameCollection" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.PointKeyFrameCollection" /> a ser copiado e congelado.</param>
		// Token: 0x06003C55 RID: 15445 RVA: 0x000ED498 File Offset: 0x000EC898
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			PointKeyFrameCollection pointKeyFrameCollection = (PointKeyFrameCollection)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			int count = pointKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<PointKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				PointKeyFrame pointKeyFrame = (PointKeyFrame)pointKeyFrameCollection._keyFrames[i].GetCurrentValueAsFrozen();
				this._keyFrames.Add(pointKeyFrame);
				base.OnFreezablePropertyChanged(null, pointKeyFrame);
			}
		}

		/// <summary>Torna essa instância do <see cref="T:System.Windows.Media.Animation.PointKeyFrameCollection" /> somente leitura ou determina se ela pode ser tornada somente leitura.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> a ser verificado se esta instância puder ser congelada; <see langword="false" /> para congelar esta instância.</param>
		/// <returns>Se <paramref name="isChecking" /> for true, esse método retornará <see langword="true" /> se essa instância puder se tornar somente leitura ou <see langword="false" />, se ela não puder se tornar somente leitura. Se <paramref name="isChecking" /> é false, este método retorna <see langword="true" /> se esta instância agora é somente leitura ou <see langword="false" />, se ela não pode tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x06003C56 RID: 15446 RVA: 0x000ED504 File Offset: 0x000EC904
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
		// Token: 0x06003C57 RID: 15447 RVA: 0x000ED54C File Offset: 0x000EC94C
		public IEnumerator GetEnumerator()
		{
			base.ReadPreamble();
			return this._keyFrames.GetEnumerator();
		}

		/// <summary>Obtém o número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.PointKeyFrameCollection" />.</summary>
		/// <returns>O número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.PointKeyFrameCollection" />.</returns>
		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06003C58 RID: 15448 RVA: 0x000ED570 File Offset: 0x000EC970
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
		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06003C59 RID: 15449 RVA: 0x000ED590 File Offset: 0x000EC990
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
		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06003C5A RID: 15450 RVA: 0x000ED5B8 File Offset: 0x000EC9B8
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
		// Token: 0x06003C5B RID: 15451 RVA: 0x000ED5D8 File Offset: 0x000EC9D8
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			((ICollection)this._keyFrames).CopyTo(array, index);
		}

		/// <summary>Copia todos os objetos de <see cref="T:System.Windows.Media.Animation.PointKeyFrame" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		// Token: 0x06003C5C RID: 15452 RVA: 0x000ED5F8 File Offset: 0x000EC9F8
		public void CopyTo(PointKeyFrame[] array, int index)
		{
			base.ReadPreamble();
			this._keyFrames.CopyTo(array, index);
		}

		/// <summary>Adiciona um item ao <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser adicionado ao <see cref="T:System.Collections.IList" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido, ou -1 para indicar que o item não foi inserido na coleção.</returns>
		// Token: 0x06003C5D RID: 15453 RVA: 0x000ED618 File Offset: 0x000ECA18
		int IList.Add(object keyFrame)
		{
			return this.Add((PointKeyFrame)keyFrame);
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.PointKeyFrame" /> ao final da coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.PointKeyFrame" /> a adicionar ao final da coleção.</param>
		/// <returns>O índice ao qual o <paramref name="keyFrame" /> foi adicionado.</returns>
		// Token: 0x06003C5E RID: 15454 RVA: 0x000ED634 File Offset: 0x000ECA34
		public int Add(PointKeyFrame keyFrame)
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

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Animation.PointKeyFrame" /> da coleção.</summary>
		// Token: 0x06003C5F RID: 15455 RVA: 0x000ED67C File Offset: 0x000ECA7C
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
		// Token: 0x06003C60 RID: 15456 RVA: 0x000ED6D8 File Offset: 0x000ECAD8
		bool IList.Contains(object keyFrame)
		{
			return this.Contains((PointKeyFrame)keyFrame);
		}

		/// <summary>Obtém um valor que indica se a coleção contém o <see cref="T:System.Windows.Media.Animation.PointKeyFrame" /> especificado.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.PointKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="keyFrame" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003C61 RID: 15457 RVA: 0x000ED6F4 File Offset: 0x000ECAF4
		public bool Contains(PointKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.Contains(keyFrame);
		}

		/// <summary>Determina o índice de um item específico em <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06003C62 RID: 15458 RVA: 0x000ED714 File Offset: 0x000ECB14
		int IList.IndexOf(object keyFrame)
		{
			return this.IndexOf((PointKeyFrame)keyFrame);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Animation.PointKeyFrame" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.PointKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="keyFrame" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x06003C63 RID: 15459 RVA: 0x000ED730 File Offset: 0x000ECB30
		public int IndexOf(PointKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.IndexOf(keyFrame);
		}

		/// <summary>Insere um item na <see cref="T:System.Collections.IList" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor deve ser inserido.</param>
		/// <param name="keyFrame">O objeto a ser inserido no <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06003C64 RID: 15460 RVA: 0x000ED750 File Offset: 0x000ECB50
		void IList.Insert(int index, object keyFrame)
		{
			this.Insert(index, (PointKeyFrame)keyFrame);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Animation.PointKeyFrame" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Animation.PointKeyFrame" /> é inserido.</param>
		/// <param name="keyFrame">O objeto <see cref="T:System.Windows.Media.Animation.PointKeyFrame" /> a ser inserido na coleção.</param>
		// Token: 0x06003C65 RID: 15461 RVA: 0x000ED76C File Offset: 0x000ECB6C
		public void Insert(int index, PointKeyFrame keyFrame)
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
		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06003C66 RID: 15462 RVA: 0x000ED7A8 File Offset: 0x000ECBA8
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
		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06003C67 RID: 15463 RVA: 0x000ED7C4 File Offset: 0x000ECBC4
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
		// Token: 0x06003C68 RID: 15464 RVA: 0x000ED7E0 File Offset: 0x000ECBE0
		void IList.Remove(object keyFrame)
		{
			this.Remove((PointKeyFrame)keyFrame);
		}

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.Animation.PointKeyFrame" /> da coleção.</summary>
		/// <param name="keyFrame">Identifica o <see cref="T:System.Windows.Media.Animation.PointKeyFrame" /> a ser removido da coleção.</param>
		// Token: 0x06003C69 RID: 15465 RVA: 0x000ED7FC File Offset: 0x000ECBFC
		public void Remove(PointKeyFrame keyFrame)
		{
			base.WritePreamble();
			if (this._keyFrames.Contains(keyFrame))
			{
				base.OnFreezablePropertyChanged(keyFrame, null);
				this._keyFrames.Remove(keyFrame);
				base.WritePostscript();
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.PointKeyFrame" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Animation.PointKeyFrame" /> a ser removido.</param>
		// Token: 0x06003C6A RID: 15466 RVA: 0x000ED838 File Offset: 0x000ECC38
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
		// Token: 0x17000C22 RID: 3106
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (PointKeyFrame)value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Animation.PointKeyFrame" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Animation.PointKeyFrame" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.PointKeyFrame" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.PointKeyFrameCollection.Count" />.</exception>
		// Token: 0x17000C23 RID: 3107
		public PointKeyFrame this[int index]
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
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "PointKeyFrameCollection[{0}]", new object[]
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

		// Token: 0x0400170A RID: 5898
		private List<PointKeyFrame> _keyFrames;

		// Token: 0x0400170B RID: 5899
		private static PointKeyFrameCollection s_emptyCollection;
	}
}
