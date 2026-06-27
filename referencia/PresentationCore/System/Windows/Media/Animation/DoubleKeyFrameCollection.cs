using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" /> .</summary>
	// Token: 0x020004DC RID: 1244
	public class DoubleKeyFrameCollection : Freezable, IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DoubleKeyFrameCollection" />.</summary>
		// Token: 0x060037F9 RID: 14329 RVA: 0x000DED38 File Offset: 0x000DE138
		public DoubleKeyFrameCollection()
		{
			this._keyFrames = new List<DoubleKeyFrame>(2);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.DoubleKeyFrameCollection" /> vazio.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.DoubleKeyFrameCollection" /> vazio.</returns>
		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x060037FA RID: 14330 RVA: 0x000DED58 File Offset: 0x000DE158
		public static DoubleKeyFrameCollection Empty
		{
			get
			{
				if (DoubleKeyFrameCollection.s_emptyCollection == null)
				{
					DoubleKeyFrameCollection doubleKeyFrameCollection = new DoubleKeyFrameCollection();
					doubleKeyFrameCollection._keyFrames = new List<DoubleKeyFrame>(0);
					doubleKeyFrameCollection.Freeze();
					DoubleKeyFrameCollection.s_emptyCollection = doubleKeyFrameCollection;
				}
				return DoubleKeyFrameCollection.s_emptyCollection;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.DoubleKeyFrameCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060037FB RID: 14331 RVA: 0x000DED90 File Offset: 0x000DE190
		public new DoubleKeyFrameCollection Clone()
		{
			return (DoubleKeyFrameCollection)base.Clone();
		}

		/// <summary>Cria uma nova instância congelada de <see cref="T:System.Windows.Media.Animation.DoubleKeyFrameCollection" />.</summary>
		/// <returns>Uma instância congelada de <see cref="T:System.Windows.Media.Animation.DoubleKeyFrameCollection" />.</returns>
		// Token: 0x060037FC RID: 14332 RVA: 0x000DEDA8 File Offset: 0x000DE1A8
		protected override Freezable CreateInstanceCore()
		{
			return new DoubleKeyFrameCollection();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.DoubleKeyFrameCollection" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.DoubleKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x060037FD RID: 14333 RVA: 0x000DEDBC File Offset: 0x000DE1BC
		protected override void CloneCore(Freezable sourceFreezable)
		{
			DoubleKeyFrameCollection doubleKeyFrameCollection = (DoubleKeyFrameCollection)sourceFreezable;
			base.CloneCore(sourceFreezable);
			int count = doubleKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<DoubleKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				DoubleKeyFrame doubleKeyFrame = (DoubleKeyFrame)doubleKeyFrameCollection._keyFrames[i].Clone();
				this._keyFrames.Add(doubleKeyFrame);
				base.OnFreezablePropertyChanged(null, doubleKeyFrame);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.DoubleKeyFrameCollection" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.DoubleKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x060037FE RID: 14334 RVA: 0x000DEE28 File Offset: 0x000DE228
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			DoubleKeyFrameCollection doubleKeyFrameCollection = (DoubleKeyFrameCollection)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			int count = doubleKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<DoubleKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				DoubleKeyFrame doubleKeyFrame = (DoubleKeyFrame)doubleKeyFrameCollection._keyFrames[i].CloneCurrentValue();
				this._keyFrames.Add(doubleKeyFrame);
				base.OnFreezablePropertyChanged(null, doubleKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.DoubleKeyFrameCollection" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.DoubleKeyFrameCollection" /> a ser clonado e congelado.</param>
		// Token: 0x060037FF RID: 14335 RVA: 0x000DEE94 File Offset: 0x000DE294
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			DoubleKeyFrameCollection doubleKeyFrameCollection = (DoubleKeyFrameCollection)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			int count = doubleKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<DoubleKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				DoubleKeyFrame doubleKeyFrame = (DoubleKeyFrame)doubleKeyFrameCollection._keyFrames[i].GetAsFrozen();
				this._keyFrames.Add(doubleKeyFrame);
				base.OnFreezablePropertyChanged(null, doubleKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.DoubleKeyFrameCollection" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.DoubleKeyFrameCollection" /> a ser copiado e congelado.</param>
		// Token: 0x06003800 RID: 14336 RVA: 0x000DEF00 File Offset: 0x000DE300
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			DoubleKeyFrameCollection doubleKeyFrameCollection = (DoubleKeyFrameCollection)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			int count = doubleKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<DoubleKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				DoubleKeyFrame doubleKeyFrame = (DoubleKeyFrame)doubleKeyFrameCollection._keyFrames[i].GetCurrentValueAsFrozen();
				this._keyFrames.Add(doubleKeyFrame);
				base.OnFreezablePropertyChanged(null, doubleKeyFrame);
			}
		}

		/// <summary>Torna essa instância do <see cref="T:System.Windows.Media.Animation.DoubleKeyFrameCollection" /> somente leitura ou determina se ela pode ser tornada somente leitura.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> se este método deve simplesmente determinar se esta instância pode ser congelada. <see langword="false" /> se esta instância, na verdade, deve congelar a si mesma quando este método é chamado.</param>
		/// <returns>Se <paramref name="isChecking" /> for true, esse método retornará <see langword="true" /> se essa instância puder se tornar somente leitura ou <see langword="false" />, se ela não puder se tornar somente leitura. Se <paramref name="isChecking" /> for false, esse método retornará <see langword="true" /> se essa instância agora for somente leitura ou <see langword="false" />, se ela não puder se tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento desse objeto.</returns>
		// Token: 0x06003801 RID: 14337 RVA: 0x000DEF6C File Offset: 0x000DE36C
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
		// Token: 0x06003802 RID: 14338 RVA: 0x000DEFB4 File Offset: 0x000DE3B4
		public IEnumerator GetEnumerator()
		{
			base.ReadPreamble();
			return this._keyFrames.GetEnumerator();
		}

		/// <summary>Obtém o número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.DoubleKeyFrameCollection" />.</summary>
		/// <returns>O número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.DoubleKeyFrameCollection" />.</returns>
		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06003803 RID: 14339 RVA: 0x000DEFD8 File Offset: 0x000DE3D8
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
		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06003804 RID: 14340 RVA: 0x000DEFF8 File Offset: 0x000DE3F8
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
		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06003805 RID: 14341 RVA: 0x000DF020 File Offset: 0x000DE420
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
		// Token: 0x06003806 RID: 14342 RVA: 0x000DF040 File Offset: 0x000DE440
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			((ICollection)this._keyFrames).CopyTo(array, index);
		}

		/// <summary>Copia todos os objetos de <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		// Token: 0x06003807 RID: 14343 RVA: 0x000DF060 File Offset: 0x000DE460
		public void CopyTo(DoubleKeyFrame[] array, int index)
		{
			base.ReadPreamble();
			this._keyFrames.CopyTo(array, index);
		}

		/// <summary>Adiciona um item ao <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser adicionado ao <see cref="T:System.Collections.IList" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido, ou -1 para indicar que o item não foi inserido na coleção.</returns>
		// Token: 0x06003808 RID: 14344 RVA: 0x000DF080 File Offset: 0x000DE480
		int IList.Add(object keyFrame)
		{
			return this.Add((DoubleKeyFrame)keyFrame);
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" /> ao final da coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" /> a adicionar ao final da coleção.</param>
		/// <returns>O índice ao qual o <paramref name="keyFrame" /> foi adicionado.</returns>
		// Token: 0x06003809 RID: 14345 RVA: 0x000DF09C File Offset: 0x000DE49C
		public int Add(DoubleKeyFrame keyFrame)
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

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" /> da coleção.</summary>
		// Token: 0x0600380A RID: 14346 RVA: 0x000DF0E4 File Offset: 0x000DE4E4
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
		// Token: 0x0600380B RID: 14347 RVA: 0x000DF140 File Offset: 0x000DE540
		bool IList.Contains(object keyFrame)
		{
			return this.Contains((DoubleKeyFrame)keyFrame);
		}

		/// <summary>Obtém um valor que indica se a coleção contém o <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" /> especificado.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="keyFrame" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600380C RID: 14348 RVA: 0x000DF15C File Offset: 0x000DE55C
		public bool Contains(DoubleKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.Contains(keyFrame);
		}

		/// <summary>Determina o índice de um item específico em <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x0600380D RID: 14349 RVA: 0x000DF17C File Offset: 0x000DE57C
		int IList.IndexOf(object keyFrame)
		{
			return this.IndexOf((DoubleKeyFrame)keyFrame);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="keyFrame" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x0600380E RID: 14350 RVA: 0x000DF198 File Offset: 0x000DE598
		public int IndexOf(DoubleKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.IndexOf(keyFrame);
		}

		/// <summary>Insere um item na <see cref="T:System.Collections.IList" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor deve ser inserido.</param>
		/// <param name="keyFrame">O objeto a ser inserido no <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x0600380F RID: 14351 RVA: 0x000DF1B8 File Offset: 0x000DE5B8
		void IList.Insert(int index, object keyFrame)
		{
			this.Insert(index, (DoubleKeyFrame)keyFrame);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" /> é inserido.</param>
		/// <param name="keyFrame">O objeto <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" /> a ser inserido na coleção.</param>
		// Token: 0x06003810 RID: 14352 RVA: 0x000DF1D4 File Offset: 0x000DE5D4
		public void Insert(int index, DoubleKeyFrame keyFrame)
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
		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06003811 RID: 14353 RVA: 0x000DF210 File Offset: 0x000DE610
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
		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06003812 RID: 14354 RVA: 0x000DF22C File Offset: 0x000DE62C
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
		// Token: 0x06003813 RID: 14355 RVA: 0x000DF248 File Offset: 0x000DE648
		void IList.Remove(object keyFrame)
		{
			this.Remove((DoubleKeyFrame)keyFrame);
		}

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" /> da coleção.</summary>
		/// <param name="keyFrame">Identifica o <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" /> a ser removido da coleção.</param>
		// Token: 0x06003814 RID: 14356 RVA: 0x000DF264 File Offset: 0x000DE664
		public void Remove(DoubleKeyFrame keyFrame)
		{
			base.WritePreamble();
			if (this._keyFrames.Contains(keyFrame))
			{
				base.OnFreezablePropertyChanged(keyFrame, null);
				this._keyFrames.Remove(keyFrame);
				base.WritePostscript();
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" /> a ser removido.</param>
		// Token: 0x06003815 RID: 14357 RVA: 0x000DF2A0 File Offset: 0x000DE6A0
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
		// Token: 0x17000B4E RID: 2894
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (DoubleKeyFrame)value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.DoubleKeyFrameCollection.Count" />.</exception>
		// Token: 0x17000B4F RID: 2895
		public DoubleKeyFrame this[int index]
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
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "DoubleKeyFrameCollection[{0}]", new object[]
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

		// Token: 0x04001686 RID: 5766
		private List<DoubleKeyFrame> _keyFrames;

		// Token: 0x04001687 RID: 5767
		private static DoubleKeyFrameCollection s_emptyCollection;
	}
}
