using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" /> .</summary>
	// Token: 0x02000549 RID: 1353
	public class SizeKeyFrameCollection : Freezable, IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SizeKeyFrameCollection" />.</summary>
		// Token: 0x06003E39 RID: 15929 RVA: 0x000F5424 File Offset: 0x000F4824
		public SizeKeyFrameCollection()
		{
			this._keyFrames = new List<SizeKeyFrame>(2);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.SizeKeyFrameCollection" /> vazio.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.SizeKeyFrameCollection" /> vazio.</returns>
		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06003E3A RID: 15930 RVA: 0x000F5444 File Offset: 0x000F4844
		public static SizeKeyFrameCollection Empty
		{
			get
			{
				if (SizeKeyFrameCollection.s_emptyCollection == null)
				{
					SizeKeyFrameCollection sizeKeyFrameCollection = new SizeKeyFrameCollection();
					sizeKeyFrameCollection._keyFrames = new List<SizeKeyFrame>(0);
					sizeKeyFrameCollection.Freeze();
					SizeKeyFrameCollection.s_emptyCollection = sizeKeyFrameCollection;
				}
				return SizeKeyFrameCollection.s_emptyCollection;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.SizeKeyFrameCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003E3B RID: 15931 RVA: 0x000F547C File Offset: 0x000F487C
		public new SizeKeyFrameCollection Clone()
		{
			return (SizeKeyFrameCollection)base.Clone();
		}

		/// <summary>Cria uma nova instância congelada de <see cref="T:System.Windows.Media.Animation.SizeKeyFrameCollection" />.</summary>
		/// <returns>Uma instância congelada de <see cref="T:System.Windows.Media.Animation.SizeKeyFrameCollection" />.</returns>
		// Token: 0x06003E3C RID: 15932 RVA: 0x000F5494 File Offset: 0x000F4894
		protected override Freezable CreateInstanceCore()
		{
			return new SizeKeyFrameCollection();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.SizeKeyFrameCollection" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.SizeKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003E3D RID: 15933 RVA: 0x000F54A8 File Offset: 0x000F48A8
		protected override void CloneCore(Freezable sourceFreezable)
		{
			SizeKeyFrameCollection sizeKeyFrameCollection = (SizeKeyFrameCollection)sourceFreezable;
			base.CloneCore(sourceFreezable);
			int count = sizeKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<SizeKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				SizeKeyFrame sizeKeyFrame = (SizeKeyFrame)sizeKeyFrameCollection._keyFrames[i].Clone();
				this._keyFrames.Add(sizeKeyFrame);
				base.OnFreezablePropertyChanged(null, sizeKeyFrame);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.SizeKeyFrameCollection" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.SizeKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003E3E RID: 15934 RVA: 0x000F5514 File Offset: 0x000F4914
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			SizeKeyFrameCollection sizeKeyFrameCollection = (SizeKeyFrameCollection)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			int count = sizeKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<SizeKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				SizeKeyFrame sizeKeyFrame = (SizeKeyFrame)sizeKeyFrameCollection._keyFrames[i].CloneCurrentValue();
				this._keyFrames.Add(sizeKeyFrame);
				base.OnFreezablePropertyChanged(null, sizeKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.SizeKeyFrameCollection" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.SizeKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003E3F RID: 15935 RVA: 0x000F5580 File Offset: 0x000F4980
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			SizeKeyFrameCollection sizeKeyFrameCollection = (SizeKeyFrameCollection)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			int count = sizeKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<SizeKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				SizeKeyFrame sizeKeyFrame = (SizeKeyFrame)sizeKeyFrameCollection._keyFrames[i].GetAsFrozen();
				this._keyFrames.Add(sizeKeyFrame);
				base.OnFreezablePropertyChanged(null, sizeKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.SizeKeyFrameCollection" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.SizeKeyFrameCollection" /> a ser copiado e congelado.</param>
		// Token: 0x06003E40 RID: 15936 RVA: 0x000F55EC File Offset: 0x000F49EC
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			SizeKeyFrameCollection sizeKeyFrameCollection = (SizeKeyFrameCollection)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			int count = sizeKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<SizeKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				SizeKeyFrame sizeKeyFrame = (SizeKeyFrame)sizeKeyFrameCollection._keyFrames[i].GetCurrentValueAsFrozen();
				this._keyFrames.Add(sizeKeyFrame);
				base.OnFreezablePropertyChanged(null, sizeKeyFrame);
			}
		}

		/// <summary>Torna esta instância de <see cref="T:System.Windows.Media.Animation.SizeKeyFrameCollection" /> não modificável ou determina se ela pode ser tornada não modificável.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> a ser verificado se esta instância puder ser congelada; <see langword="false" /> para congelar esta instância.</param>
		/// <returns>Se <paramref name="isChecking" /> é true, este método retorna <see langword="true" /> caso esta instância possa se tornar somente leitura ou então <see langword="false" /> se ela não pode se tornar somente leitura. Se <paramref name="isChecking" /> é false, este método retorna <see langword="true" /> se esta instância agora é somente leitura ou <see langword="false" />, se ela não pode tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x06003E41 RID: 15937 RVA: 0x000F5658 File Offset: 0x000F4A58
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
		// Token: 0x06003E42 RID: 15938 RVA: 0x000F56A0 File Offset: 0x000F4AA0
		public IEnumerator GetEnumerator()
		{
			base.ReadPreamble();
			return this._keyFrames.GetEnumerator();
		}

		/// <summary>Obtém o número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.SizeKeyFrameCollection" />.</summary>
		/// <returns>O número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.SizeKeyFrameCollection" />.</returns>
		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06003E43 RID: 15939 RVA: 0x000F56C4 File Offset: 0x000F4AC4
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
		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06003E44 RID: 15940 RVA: 0x000F56E4 File Offset: 0x000F4AE4
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
		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06003E45 RID: 15941 RVA: 0x000F570C File Offset: 0x000F4B0C
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
		// Token: 0x06003E46 RID: 15942 RVA: 0x000F572C File Offset: 0x000F4B2C
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			((ICollection)this._keyFrames).CopyTo(array, index);
		}

		/// <summary>Copia todos os objetos de <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		// Token: 0x06003E47 RID: 15943 RVA: 0x000F574C File Offset: 0x000F4B4C
		public void CopyTo(SizeKeyFrame[] array, int index)
		{
			base.ReadPreamble();
			this._keyFrames.CopyTo(array, index);
		}

		/// <summary>Adiciona um item ao <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser adicionado ao <see cref="T:System.Collections.IList" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido, ou -1 para indicar que o item não foi inserido na coleção.</returns>
		// Token: 0x06003E48 RID: 15944 RVA: 0x000F576C File Offset: 0x000F4B6C
		int IList.Add(object keyFrame)
		{
			return this.Add((SizeKeyFrame)keyFrame);
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" /> ao final da coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" /> a adicionar ao final da coleção.</param>
		/// <returns>O índice ao qual o <paramref name="keyFrame" /> foi adicionado.</returns>
		// Token: 0x06003E49 RID: 15945 RVA: 0x000F5788 File Offset: 0x000F4B88
		public int Add(SizeKeyFrame keyFrame)
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

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" /> da coleção.</summary>
		// Token: 0x06003E4A RID: 15946 RVA: 0x000F57D0 File Offset: 0x000F4BD0
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
		// Token: 0x06003E4B RID: 15947 RVA: 0x000F582C File Offset: 0x000F4C2C
		bool IList.Contains(object keyFrame)
		{
			return this.Contains((SizeKeyFrame)keyFrame);
		}

		/// <summary>Obtém um valor que indica se a coleção contém o <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" /> especificado.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="keyFrame" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003E4C RID: 15948 RVA: 0x000F5848 File Offset: 0x000F4C48
		public bool Contains(SizeKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.Contains(keyFrame);
		}

		/// <summary>Determina o índice de um item específico em <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06003E4D RID: 15949 RVA: 0x000F5868 File Offset: 0x000F4C68
		int IList.IndexOf(object keyFrame)
		{
			return this.IndexOf((SizeKeyFrame)keyFrame);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="keyFrame" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x06003E4E RID: 15950 RVA: 0x000F5884 File Offset: 0x000F4C84
		public int IndexOf(SizeKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.IndexOf(keyFrame);
		}

		/// <summary>Insere um item na <see cref="T:System.Collections.IList" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor deve ser inserido.</param>
		/// <param name="keyFrame">O objeto a ser inserido no <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06003E4F RID: 15951 RVA: 0x000F58A4 File Offset: 0x000F4CA4
		void IList.Insert(int index, object keyFrame)
		{
			this.Insert(index, (SizeKeyFrame)keyFrame);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" /> é inserido.</param>
		/// <param name="keyFrame">O objeto <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" /> a ser inserido na coleção.</param>
		// Token: 0x06003E50 RID: 15952 RVA: 0x000F58C0 File Offset: 0x000F4CC0
		public void Insert(int index, SizeKeyFrame keyFrame)
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
		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06003E51 RID: 15953 RVA: 0x000F58FC File Offset: 0x000F4CFC
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
		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06003E52 RID: 15954 RVA: 0x000F5918 File Offset: 0x000F4D18
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
		// Token: 0x06003E53 RID: 15955 RVA: 0x000F5934 File Offset: 0x000F4D34
		void IList.Remove(object keyFrame)
		{
			this.Remove((SizeKeyFrame)keyFrame);
		}

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" /> da coleção.</summary>
		/// <param name="keyFrame">Identifica o <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" /> a ser removido da coleção.</param>
		// Token: 0x06003E54 RID: 15956 RVA: 0x000F5950 File Offset: 0x000F4D50
		public void Remove(SizeKeyFrame keyFrame)
		{
			base.WritePreamble();
			if (this._keyFrames.Contains(keyFrame))
			{
				base.OnFreezablePropertyChanged(keyFrame, null);
				this._keyFrames.Remove(keyFrame);
				base.WritePostscript();
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" /> a ser removido.</param>
		// Token: 0x06003E55 RID: 15957 RVA: 0x000F598C File Offset: 0x000F4D8C
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
		// Token: 0x17000C90 RID: 3216
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (SizeKeyFrame)value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.SizeKeyFrameCollection.Count" />.</exception>
		// Token: 0x17000C91 RID: 3217
		public SizeKeyFrame this[int index]
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
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "SizeKeyFrameCollection[{0}]", new object[]
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

		// Token: 0x04001749 RID: 5961
		private List<SizeKeyFrame> _keyFrames;

		// Token: 0x0400174A RID: 5962
		private static SizeKeyFrameCollection s_emptyCollection;
	}
}
