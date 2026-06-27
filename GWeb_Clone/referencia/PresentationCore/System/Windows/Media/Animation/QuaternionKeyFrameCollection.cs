using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> .</summary>
	// Token: 0x02000535 RID: 1333
	public class QuaternionKeyFrameCollection : Freezable, IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrameCollection" />.</summary>
		// Token: 0x06003CB1 RID: 15537 RVA: 0x000EED2C File Offset: 0x000EE12C
		public QuaternionKeyFrameCollection()
		{
			this._keyFrames = new List<QuaternionKeyFrame>(2);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrameCollection" /> vazio.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrameCollection" /> vazio.</returns>
		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06003CB2 RID: 15538 RVA: 0x000EED4C File Offset: 0x000EE14C
		public static QuaternionKeyFrameCollection Empty
		{
			get
			{
				if (QuaternionKeyFrameCollection.s_emptyCollection == null)
				{
					QuaternionKeyFrameCollection quaternionKeyFrameCollection = new QuaternionKeyFrameCollection();
					quaternionKeyFrameCollection._keyFrames = new List<QuaternionKeyFrame>(0);
					quaternionKeyFrameCollection.Freeze();
					QuaternionKeyFrameCollection.s_emptyCollection = quaternionKeyFrameCollection;
				}
				return QuaternionKeyFrameCollection.s_emptyCollection;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrameCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003CB3 RID: 15539 RVA: 0x000EED84 File Offset: 0x000EE184
		public new QuaternionKeyFrameCollection Clone()
		{
			return (QuaternionKeyFrameCollection)base.Clone();
		}

		/// <summary>Cria uma nova instância congelada de <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrameCollection" />.</summary>
		/// <returns>Uma instância congelada de <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrameCollection" />.</returns>
		// Token: 0x06003CB4 RID: 15540 RVA: 0x000EED9C File Offset: 0x000EE19C
		protected override Freezable CreateInstanceCore()
		{
			return new QuaternionKeyFrameCollection();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrameCollection" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003CB5 RID: 15541 RVA: 0x000EEDB0 File Offset: 0x000EE1B0
		protected override void CloneCore(Freezable sourceFreezable)
		{
			QuaternionKeyFrameCollection quaternionKeyFrameCollection = (QuaternionKeyFrameCollection)sourceFreezable;
			base.CloneCore(sourceFreezable);
			int count = quaternionKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<QuaternionKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				QuaternionKeyFrame quaternionKeyFrame = (QuaternionKeyFrame)quaternionKeyFrameCollection._keyFrames[i].Clone();
				this._keyFrames.Add(quaternionKeyFrame);
				base.OnFreezablePropertyChanged(null, quaternionKeyFrame);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrameCollection" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003CB6 RID: 15542 RVA: 0x000EEE1C File Offset: 0x000EE21C
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			QuaternionKeyFrameCollection quaternionKeyFrameCollection = (QuaternionKeyFrameCollection)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			int count = quaternionKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<QuaternionKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				QuaternionKeyFrame quaternionKeyFrame = (QuaternionKeyFrame)quaternionKeyFrameCollection._keyFrames[i].CloneCurrentValue();
				this._keyFrames.Add(quaternionKeyFrame);
				base.OnFreezablePropertyChanged(null, quaternionKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrameCollection" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003CB7 RID: 15543 RVA: 0x000EEE88 File Offset: 0x000EE288
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			QuaternionKeyFrameCollection quaternionKeyFrameCollection = (QuaternionKeyFrameCollection)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			int count = quaternionKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<QuaternionKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				QuaternionKeyFrame quaternionKeyFrame = (QuaternionKeyFrame)quaternionKeyFrameCollection._keyFrames[i].GetAsFrozen();
				this._keyFrames.Add(quaternionKeyFrame);
				base.OnFreezablePropertyChanged(null, quaternionKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrameCollection" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrameCollection" /> a ser copiado e congelado.</param>
		// Token: 0x06003CB8 RID: 15544 RVA: 0x000EEEF4 File Offset: 0x000EE2F4
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			QuaternionKeyFrameCollection quaternionKeyFrameCollection = (QuaternionKeyFrameCollection)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			int count = quaternionKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<QuaternionKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				QuaternionKeyFrame quaternionKeyFrame = (QuaternionKeyFrame)quaternionKeyFrameCollection._keyFrames[i].GetCurrentValueAsFrozen();
				this._keyFrames.Add(quaternionKeyFrame);
				base.OnFreezablePropertyChanged(null, quaternionKeyFrame);
			}
		}

		/// <summary>Torna essa instância do <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrameCollection" /> somente leitura ou determina se ela pode ser tornada somente leitura.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> a ser verificado se esta instância puder ser congelada; <see langword="false" /> para congelar esta instância.</param>
		/// <returns>Se <paramref name="isChecking" /> for true, esse método retornará <see langword="true" /> se essa instância puder se tornar somente leitura ou <see langword="false" />, se ela não puder se tornar somente leitura. Se <paramref name="isChecking" /> é false, este método retorna <see langword="true" /> se esta instância agora é somente leitura ou <see langword="false" />, se ela não pode tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x06003CB9 RID: 15545 RVA: 0x000EEF60 File Offset: 0x000EE360
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
		// Token: 0x06003CBA RID: 15546 RVA: 0x000EEFA8 File Offset: 0x000EE3A8
		public IEnumerator GetEnumerator()
		{
			base.ReadPreamble();
			return this._keyFrames.GetEnumerator();
		}

		/// <summary>Obtém o número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrameCollection" />.</summary>
		/// <returns>O número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrameCollection" />.</returns>
		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06003CBB RID: 15547 RVA: 0x000EEFCC File Offset: 0x000EE3CC
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
		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06003CBC RID: 15548 RVA: 0x000EEFEC File Offset: 0x000EE3EC
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
		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06003CBD RID: 15549 RVA: 0x000EF014 File Offset: 0x000EE414
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
		// Token: 0x06003CBE RID: 15550 RVA: 0x000EF034 File Offset: 0x000EE434
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			((ICollection)this._keyFrames).CopyTo(array, index);
		}

		/// <summary>Copia todos os objetos de <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		// Token: 0x06003CBF RID: 15551 RVA: 0x000EF054 File Offset: 0x000EE454
		public void CopyTo(QuaternionKeyFrame[] array, int index)
		{
			base.ReadPreamble();
			this._keyFrames.CopyTo(array, index);
		}

		/// <summary>Adiciona um item ao <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser adicionado ao <see cref="T:System.Collections.IList" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido, ou -1 para indicar que o item não foi inserido na coleção.</returns>
		// Token: 0x06003CC0 RID: 15552 RVA: 0x000EF074 File Offset: 0x000EE474
		int IList.Add(object keyFrame)
		{
			return this.Add((QuaternionKeyFrame)keyFrame);
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> ao final da coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> a adicionar ao final da coleção.</param>
		/// <returns>O índice ao qual o <paramref name="keyFrame" /> foi adicionado.</returns>
		// Token: 0x06003CC1 RID: 15553 RVA: 0x000EF090 File Offset: 0x000EE490
		public int Add(QuaternionKeyFrame keyFrame)
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

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> da coleção.</summary>
		// Token: 0x06003CC2 RID: 15554 RVA: 0x000EF0D8 File Offset: 0x000EE4D8
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
		// Token: 0x06003CC3 RID: 15555 RVA: 0x000EF134 File Offset: 0x000EE534
		bool IList.Contains(object keyFrame)
		{
			return this.Contains((QuaternionKeyFrame)keyFrame);
		}

		/// <summary>Obtém um valor que indica se a coleção contém o <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> especificado.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="keyFrame" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003CC4 RID: 15556 RVA: 0x000EF150 File Offset: 0x000EE550
		public bool Contains(QuaternionKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.Contains(keyFrame);
		}

		/// <summary>Determina o índice de um item específico em <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06003CC5 RID: 15557 RVA: 0x000EF170 File Offset: 0x000EE570
		int IList.IndexOf(object keyFrame)
		{
			return this.IndexOf((QuaternionKeyFrame)keyFrame);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="keyFrame" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x06003CC6 RID: 15558 RVA: 0x000EF18C File Offset: 0x000EE58C
		public int IndexOf(QuaternionKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.IndexOf(keyFrame);
		}

		/// <summary>Insere um item na <see cref="T:System.Collections.IList" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor deve ser inserido.</param>
		/// <param name="keyFrame">O objeto a ser inserido no <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06003CC7 RID: 15559 RVA: 0x000EF1AC File Offset: 0x000EE5AC
		void IList.Insert(int index, object keyFrame)
		{
			this.Insert(index, (QuaternionKeyFrame)keyFrame);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> é inserido.</param>
		/// <param name="keyFrame">O objeto <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> a ser inserido na coleção.</param>
		// Token: 0x06003CC8 RID: 15560 RVA: 0x000EF1C8 File Offset: 0x000EE5C8
		public void Insert(int index, QuaternionKeyFrame keyFrame)
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
		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06003CC9 RID: 15561 RVA: 0x000EF204 File Offset: 0x000EE604
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
		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06003CCA RID: 15562 RVA: 0x000EF220 File Offset: 0x000EE620
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
		// Token: 0x06003CCB RID: 15563 RVA: 0x000EF23C File Offset: 0x000EE63C
		void IList.Remove(object keyFrame)
		{
			this.Remove((QuaternionKeyFrame)keyFrame);
		}

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> da coleção.</summary>
		/// <param name="keyFrame">Identifica o <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> a ser removido da coleção.</param>
		// Token: 0x06003CCC RID: 15564 RVA: 0x000EF258 File Offset: 0x000EE658
		public void Remove(QuaternionKeyFrame keyFrame)
		{
			base.WritePreamble();
			if (this._keyFrames.Contains(keyFrame))
			{
				base.OnFreezablePropertyChanged(keyFrame, null);
				this._keyFrames.Remove(keyFrame);
				base.WritePostscript();
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> a ser removido.</param>
		// Token: 0x06003CCD RID: 15565 RVA: 0x000EF294 File Offset: 0x000EE694
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
		// Token: 0x17000C38 RID: 3128
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (QuaternionKeyFrame)value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.QuaternionKeyFrameCollection.Count" />.</exception>
		// Token: 0x17000C39 RID: 3129
		public QuaternionKeyFrame this[int index]
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
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "QuaternionKeyFrameCollection[{0}]", new object[]
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

		// Token: 0x04001717 RID: 5911
		private List<QuaternionKeyFrame> _keyFrames;

		// Token: 0x04001718 RID: 5912
		private static QuaternionKeyFrameCollection s_emptyCollection;
	}
}
