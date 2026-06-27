using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> .</summary>
	// Token: 0x02000568 RID: 1384
	public class VectorKeyFrameCollection : Freezable, IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.VectorKeyFrameCollection" />.</summary>
		// Token: 0x06004043 RID: 16451 RVA: 0x000FC62C File Offset: 0x000FBA2C
		public VectorKeyFrameCollection()
		{
			this._keyFrames = new List<VectorKeyFrame>(2);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.VectorKeyFrameCollection" /> vazio.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.VectorKeyFrameCollection" /> vazio.</returns>
		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06004044 RID: 16452 RVA: 0x000FC64C File Offset: 0x000FBA4C
		public static VectorKeyFrameCollection Empty
		{
			get
			{
				if (VectorKeyFrameCollection.s_emptyCollection == null)
				{
					VectorKeyFrameCollection vectorKeyFrameCollection = new VectorKeyFrameCollection();
					vectorKeyFrameCollection._keyFrames = new List<VectorKeyFrame>(0);
					vectorKeyFrameCollection.Freeze();
					VectorKeyFrameCollection.s_emptyCollection = vectorKeyFrameCollection;
				}
				return VectorKeyFrameCollection.s_emptyCollection;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.VectorKeyFrameCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06004045 RID: 16453 RVA: 0x000FC684 File Offset: 0x000FBA84
		public new VectorKeyFrameCollection Clone()
		{
			return (VectorKeyFrameCollection)base.Clone();
		}

		/// <summary>Cria uma nova instância congelada de <see cref="T:System.Windows.Media.Animation.VectorKeyFrameCollection" />.</summary>
		/// <returns>Uma instância congelada de <see cref="T:System.Windows.Media.Animation.VectorKeyFrameCollection" />.</returns>
		// Token: 0x06004046 RID: 16454 RVA: 0x000FC69C File Offset: 0x000FBA9C
		protected override Freezable CreateInstanceCore()
		{
			return new VectorKeyFrameCollection();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.VectorKeyFrameCollection" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.VectorKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06004047 RID: 16455 RVA: 0x000FC6B0 File Offset: 0x000FBAB0
		protected override void CloneCore(Freezable sourceFreezable)
		{
			VectorKeyFrameCollection vectorKeyFrameCollection = (VectorKeyFrameCollection)sourceFreezable;
			base.CloneCore(sourceFreezable);
			int count = vectorKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<VectorKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				VectorKeyFrame vectorKeyFrame = (VectorKeyFrame)vectorKeyFrameCollection._keyFrames[i].Clone();
				this._keyFrames.Add(vectorKeyFrame);
				base.OnFreezablePropertyChanged(null, vectorKeyFrame);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.VectorKeyFrameCollection" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.VectorKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06004048 RID: 16456 RVA: 0x000FC71C File Offset: 0x000FBB1C
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			VectorKeyFrameCollection vectorKeyFrameCollection = (VectorKeyFrameCollection)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			int count = vectorKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<VectorKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				VectorKeyFrame vectorKeyFrame = (VectorKeyFrame)vectorKeyFrameCollection._keyFrames[i].CloneCurrentValue();
				this._keyFrames.Add(vectorKeyFrame);
				base.OnFreezablePropertyChanged(null, vectorKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.VectorKeyFrameCollection" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.VectorKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06004049 RID: 16457 RVA: 0x000FC788 File Offset: 0x000FBB88
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			VectorKeyFrameCollection vectorKeyFrameCollection = (VectorKeyFrameCollection)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			int count = vectorKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<VectorKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				VectorKeyFrame vectorKeyFrame = (VectorKeyFrame)vectorKeyFrameCollection._keyFrames[i].GetAsFrozen();
				this._keyFrames.Add(vectorKeyFrame);
				base.OnFreezablePropertyChanged(null, vectorKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.VectorKeyFrameCollection" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.VectorKeyFrameCollection" /> a ser copiado e congelado.</param>
		// Token: 0x0600404A RID: 16458 RVA: 0x000FC7F4 File Offset: 0x000FBBF4
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			VectorKeyFrameCollection vectorKeyFrameCollection = (VectorKeyFrameCollection)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			int count = vectorKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<VectorKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				VectorKeyFrame vectorKeyFrame = (VectorKeyFrame)vectorKeyFrameCollection._keyFrames[i].GetCurrentValueAsFrozen();
				this._keyFrames.Add(vectorKeyFrame);
				base.OnFreezablePropertyChanged(null, vectorKeyFrame);
			}
		}

		/// <summary>Torna essa instância do <see cref="T:System.Windows.Media.Animation.VectorKeyFrameCollection" /> somente leitura ou determina se ela pode ser tornada somente leitura.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> a ser verificado se esta instância puder ser congelada; <see langword="false" /> para congelar esta instância.</param>
		/// <returns>Se <paramref name="isChecking" /> for true, esse método retornará <see langword="true" /> se essa instância puder se tornar somente leitura ou <see langword="false" />, se ela não puder se tornar somente leitura.  
		/// Se <paramref name="isChecking" /> é false, este método retorna <see langword="true" /> se esta instância agora é somente leitura ou <see langword="false" />, se ela não pode tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x0600404B RID: 16459 RVA: 0x000FC860 File Offset: 0x000FBC60
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
		// Token: 0x0600404C RID: 16460 RVA: 0x000FC8A8 File Offset: 0x000FBCA8
		public IEnumerator GetEnumerator()
		{
			base.ReadPreamble();
			return this._keyFrames.GetEnumerator();
		}

		/// <summary>Obtém o número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.VectorKeyFrameCollection" />.</summary>
		/// <returns>O número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.VectorKeyFrameCollection" />.</returns>
		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x0600404D RID: 16461 RVA: 0x000FC8CC File Offset: 0x000FBCCC
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
		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x0600404E RID: 16462 RVA: 0x000FC8EC File Offset: 0x000FBCEC
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
		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x0600404F RID: 16463 RVA: 0x000FC914 File Offset: 0x000FBD14
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
		// Token: 0x06004050 RID: 16464 RVA: 0x000FC934 File Offset: 0x000FBD34
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			((ICollection)this._keyFrames).CopyTo(array, index);
		}

		/// <summary>Copia todos os objetos de <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		// Token: 0x06004051 RID: 16465 RVA: 0x000FC954 File Offset: 0x000FBD54
		public void CopyTo(VectorKeyFrame[] array, int index)
		{
			base.ReadPreamble();
			this._keyFrames.CopyTo(array, index);
		}

		/// <summary>Adiciona um item ao <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser adicionado ao <see cref="T:System.Collections.IList" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido, ou -1 para indicar que o item não foi inserido na coleção.</returns>
		// Token: 0x06004052 RID: 16466 RVA: 0x000FC974 File Offset: 0x000FBD74
		int IList.Add(object keyFrame)
		{
			return this.Add((VectorKeyFrame)keyFrame);
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> ao final da coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> a adicionar ao final da coleção.</param>
		/// <returns>O índice ao qual o <paramref name="keyFrame" /> foi adicionado.</returns>
		// Token: 0x06004053 RID: 16467 RVA: 0x000FC990 File Offset: 0x000FBD90
		public int Add(VectorKeyFrame keyFrame)
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

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> da coleção.</summary>
		// Token: 0x06004054 RID: 16468 RVA: 0x000FC9D8 File Offset: 0x000FBDD8
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
		// Token: 0x06004055 RID: 16469 RVA: 0x000FCA34 File Offset: 0x000FBE34
		bool IList.Contains(object keyFrame)
		{
			return this.Contains((VectorKeyFrame)keyFrame);
		}

		/// <summary>Obtém um valor que indica se a coleção contém o <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> especificado.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="keyFrame" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06004056 RID: 16470 RVA: 0x000FCA50 File Offset: 0x000FBE50
		public bool Contains(VectorKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.Contains(keyFrame);
		}

		/// <summary>Determina o índice de um item específico em <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06004057 RID: 16471 RVA: 0x000FCA70 File Offset: 0x000FBE70
		int IList.IndexOf(object keyFrame)
		{
			return this.IndexOf((VectorKeyFrame)keyFrame);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="keyFrame" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x06004058 RID: 16472 RVA: 0x000FCA8C File Offset: 0x000FBE8C
		public int IndexOf(VectorKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.IndexOf(keyFrame);
		}

		/// <summary>Insere um item na <see cref="T:System.Collections.IList" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor deve ser inserido.</param>
		/// <param name="keyFrame">O objeto a ser inserido no <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06004059 RID: 16473 RVA: 0x000FCAAC File Offset: 0x000FBEAC
		void IList.Insert(int index, object keyFrame)
		{
			this.Insert(index, (VectorKeyFrame)keyFrame);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> é inserido.</param>
		/// <param name="keyFrame">O objeto <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> a ser inserido na coleção.</param>
		// Token: 0x0600405A RID: 16474 RVA: 0x000FCAC8 File Offset: 0x000FBEC8
		public void Insert(int index, VectorKeyFrame keyFrame)
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
		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x0600405B RID: 16475 RVA: 0x000FCB04 File Offset: 0x000FBF04
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
		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x0600405C RID: 16476 RVA: 0x000FCB20 File Offset: 0x000FBF20
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
		// Token: 0x0600405D RID: 16477 RVA: 0x000FCB3C File Offset: 0x000FBF3C
		void IList.Remove(object keyFrame)
		{
			this.Remove((VectorKeyFrame)keyFrame);
		}

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> da coleção.</summary>
		/// <param name="keyFrame">Identifica o <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> a ser removido da coleção.</param>
		// Token: 0x0600405E RID: 16478 RVA: 0x000FCB58 File Offset: 0x000FBF58
		public void Remove(VectorKeyFrame keyFrame)
		{
			base.WritePreamble();
			if (this._keyFrames.Contains(keyFrame))
			{
				base.OnFreezablePropertyChanged(keyFrame, null);
				this._keyFrames.Remove(keyFrame);
				base.WritePostscript();
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> a ser removido.</param>
		// Token: 0x0600405F RID: 16479 RVA: 0x000FCB94 File Offset: 0x000FBF94
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
		// Token: 0x17000CEA RID: 3306
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (VectorKeyFrame)value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.VectorKeyFrameCollection.Count" />.</exception>
		// Token: 0x17000CEB RID: 3307
		public VectorKeyFrame this[int index]
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
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "VectorKeyFrameCollection[{0}]", new object[]
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

		// Token: 0x0400178C RID: 6028
		private List<VectorKeyFrame> _keyFrames;

		// Token: 0x0400178D RID: 6029
		private static VectorKeyFrameCollection s_emptyCollection;
	}
}
