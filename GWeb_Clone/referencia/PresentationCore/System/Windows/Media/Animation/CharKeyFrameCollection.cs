using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.CharKeyFrame" /> .</summary>
	// Token: 0x020004B8 RID: 1208
	public class CharKeyFrameCollection : Freezable, IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.CharKeyFrameCollection" />.</summary>
		// Token: 0x0600366B RID: 13931 RVA: 0x000D957C File Offset: 0x000D897C
		public CharKeyFrameCollection()
		{
			this._keyFrames = new List<CharKeyFrame>(2);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.CharKeyFrameCollection" /> vazio.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.CharKeyFrameCollection" /> vazio.</returns>
		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x0600366C RID: 13932 RVA: 0x000D959C File Offset: 0x000D899C
		public static CharKeyFrameCollection Empty
		{
			get
			{
				if (CharKeyFrameCollection.s_emptyCollection == null)
				{
					CharKeyFrameCollection charKeyFrameCollection = new CharKeyFrameCollection();
					charKeyFrameCollection._keyFrames = new List<CharKeyFrame>(0);
					charKeyFrameCollection.Freeze();
					CharKeyFrameCollection.s_emptyCollection = charKeyFrameCollection;
				}
				return CharKeyFrameCollection.s_emptyCollection;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.CharKeyFrameCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x0600366D RID: 13933 RVA: 0x000D95D4 File Offset: 0x000D89D4
		public new CharKeyFrameCollection Clone()
		{
			return (CharKeyFrameCollection)base.Clone();
		}

		/// <summary>Cria uma nova instância congelada de <see cref="T:System.Windows.Media.Animation.CharKeyFrameCollection" />.</summary>
		/// <returns>Uma instância congelada de <see cref="T:System.Windows.Media.Animation.CharKeyFrameCollection" />.</returns>
		// Token: 0x0600366E RID: 13934 RVA: 0x000D95EC File Offset: 0x000D89EC
		protected override Freezable CreateInstanceCore()
		{
			return new CharKeyFrameCollection();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.CharKeyFrameCollection" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.CharKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x0600366F RID: 13935 RVA: 0x000D9600 File Offset: 0x000D8A00
		protected override void CloneCore(Freezable sourceFreezable)
		{
			CharKeyFrameCollection charKeyFrameCollection = (CharKeyFrameCollection)sourceFreezable;
			base.CloneCore(sourceFreezable);
			int count = charKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<CharKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				CharKeyFrame charKeyFrame = (CharKeyFrame)charKeyFrameCollection._keyFrames[i].Clone();
				this._keyFrames.Add(charKeyFrame);
				base.OnFreezablePropertyChanged(null, charKeyFrame);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.CharKeyFrameCollection" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.CharKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003670 RID: 13936 RVA: 0x000D966C File Offset: 0x000D8A6C
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			CharKeyFrameCollection charKeyFrameCollection = (CharKeyFrameCollection)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			int count = charKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<CharKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				CharKeyFrame charKeyFrame = (CharKeyFrame)charKeyFrameCollection._keyFrames[i].CloneCurrentValue();
				this._keyFrames.Add(charKeyFrame);
				base.OnFreezablePropertyChanged(null, charKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.CharKeyFrameCollection" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.CharKeyFrameCollection" /> a ser clonado e congelado.</param>
		// Token: 0x06003671 RID: 13937 RVA: 0x000D96D8 File Offset: 0x000D8AD8
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			CharKeyFrameCollection charKeyFrameCollection = (CharKeyFrameCollection)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			int count = charKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<CharKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				CharKeyFrame charKeyFrame = (CharKeyFrame)charKeyFrameCollection._keyFrames[i].GetAsFrozen();
				this._keyFrames.Add(charKeyFrame);
				base.OnFreezablePropertyChanged(null, charKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.CharKeyFrameCollection" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.CharKeyFrameCollection" /> a ser copiado e congelado.</param>
		// Token: 0x06003672 RID: 13938 RVA: 0x000D9744 File Offset: 0x000D8B44
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			CharKeyFrameCollection charKeyFrameCollection = (CharKeyFrameCollection)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			int count = charKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<CharKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				CharKeyFrame charKeyFrame = (CharKeyFrame)charKeyFrameCollection._keyFrames[i].GetCurrentValueAsFrozen();
				this._keyFrames.Add(charKeyFrame);
				base.OnFreezablePropertyChanged(null, charKeyFrame);
			}
		}

		/// <summary>Torna este <see cref="T:System.Windows.Media.Animation.CharKeyFrameCollection" /> somente leitura ou determina se ela pode ser tornada somente leitura.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> se este método deve simplesmente determinar se esta instância pode ser congelada. <see langword="false" /> se esta instância, na verdade, deve congelar a si mesma quando este método é chamado.</param>
		/// <returns>Se <paramref name="isChecking" /> é true, este método retorna <see langword="true" /> caso esta instância possa se tornar somente leitura ou então <see langword="false" /> se ela não pode se tornar somente leitura. Se <paramref name="isChecking" /> for false, esse método retornará <see langword="true" /> se essa instância agora for somente leitura ou <see langword="false" />, se ela não puder se tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento desse objeto.</returns>
		// Token: 0x06003673 RID: 13939 RVA: 0x000D97B0 File Offset: 0x000D8BB0
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
		// Token: 0x06003674 RID: 13940 RVA: 0x000D97F8 File Offset: 0x000D8BF8
		public IEnumerator GetEnumerator()
		{
			base.ReadPreamble();
			return this._keyFrames.GetEnumerator();
		}

		/// <summary>Obtém o número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.CharKeyFrameCollection" />.</summary>
		/// <returns>O número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.CharKeyFrameCollection" />.</returns>
		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06003675 RID: 13941 RVA: 0x000D981C File Offset: 0x000D8C1C
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
		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06003676 RID: 13942 RVA: 0x000D983C File Offset: 0x000D8C3C
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
		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06003677 RID: 13943 RVA: 0x000D9864 File Offset: 0x000D8C64
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
		// Token: 0x06003678 RID: 13944 RVA: 0x000D9884 File Offset: 0x000D8C84
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			((ICollection)this._keyFrames).CopyTo(array, index);
		}

		/// <summary>Copia todos os objetos de <see cref="T:System.Windows.Media.Animation.CharKeyFrame" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		// Token: 0x06003679 RID: 13945 RVA: 0x000D98A4 File Offset: 0x000D8CA4
		public void CopyTo(CharKeyFrame[] array, int index)
		{
			base.ReadPreamble();
			this._keyFrames.CopyTo(array, index);
		}

		/// <summary>Adiciona um item ao <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser adicionado ao <see cref="T:System.Collections.IList" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido, ou -1 para indicar que o item não foi inserido na coleção.</returns>
		// Token: 0x0600367A RID: 13946 RVA: 0x000D98C4 File Offset: 0x000D8CC4
		int IList.Add(object keyFrame)
		{
			return this.Add((CharKeyFrame)keyFrame);
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.CharKeyFrame" /> ao final da coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.CharKeyFrame" /> a adicionar ao final da coleção.</param>
		/// <returns>O índice ao qual o <paramref name="keyFrame" /> foi adicionado.</returns>
		// Token: 0x0600367B RID: 13947 RVA: 0x000D98E0 File Offset: 0x000D8CE0
		public int Add(CharKeyFrame keyFrame)
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

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Animation.CharKeyFrame" /> da coleção.</summary>
		// Token: 0x0600367C RID: 13948 RVA: 0x000D9928 File Offset: 0x000D8D28
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
		// Token: 0x0600367D RID: 13949 RVA: 0x000D9984 File Offset: 0x000D8D84
		bool IList.Contains(object keyFrame)
		{
			return this.Contains((CharKeyFrame)keyFrame);
		}

		/// <summary>Obtém um valor que indica se a coleção contém o <see cref="T:System.Windows.Media.Animation.CharKeyFrame" /> especificado.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.CharKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="keyFrame" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600367E RID: 13950 RVA: 0x000D99A0 File Offset: 0x000D8DA0
		public bool Contains(CharKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.Contains(keyFrame);
		}

		/// <summary>Determina o índice de um item específico em <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x0600367F RID: 13951 RVA: 0x000D99C0 File Offset: 0x000D8DC0
		int IList.IndexOf(object keyFrame)
		{
			return this.IndexOf((CharKeyFrame)keyFrame);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Animation.CharKeyFrame" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.CharKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="keyFrame" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x06003680 RID: 13952 RVA: 0x000D99DC File Offset: 0x000D8DDC
		public int IndexOf(CharKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.IndexOf(keyFrame);
		}

		/// <summary>Insere um item na <see cref="T:System.Collections.IList" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor deve ser inserido.</param>
		/// <param name="keyFrame">O objeto a ser inserido no <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06003681 RID: 13953 RVA: 0x000D99FC File Offset: 0x000D8DFC
		void IList.Insert(int index, object keyFrame)
		{
			this.Insert(index, (CharKeyFrame)keyFrame);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Animation.CharKeyFrame" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Animation.CharKeyFrame" /> é inserido.</param>
		/// <param name="keyFrame">O objeto <see cref="T:System.Windows.Media.Animation.CharKeyFrame" /> a ser inserido na coleção.</param>
		// Token: 0x06003682 RID: 13954 RVA: 0x000D9A18 File Offset: 0x000D8E18
		public void Insert(int index, CharKeyFrame keyFrame)
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
		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06003683 RID: 13955 RVA: 0x000D9A54 File Offset: 0x000D8E54
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
		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06003684 RID: 13956 RVA: 0x000D9A70 File Offset: 0x000D8E70
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
		// Token: 0x06003685 RID: 13957 RVA: 0x000D9A8C File Offset: 0x000D8E8C
		void IList.Remove(object keyFrame)
		{
			this.Remove((CharKeyFrame)keyFrame);
		}

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.Animation.CharKeyFrame" /> da coleção.</summary>
		/// <param name="keyFrame">Identifica o <see cref="T:System.Windows.Media.Animation.CharKeyFrame" /> a ser removido da coleção.</param>
		// Token: 0x06003686 RID: 13958 RVA: 0x000D9AA8 File Offset: 0x000D8EA8
		public void Remove(CharKeyFrame keyFrame)
		{
			base.WritePreamble();
			if (this._keyFrames.Contains(keyFrame))
			{
				base.OnFreezablePropertyChanged(keyFrame, null);
				this._keyFrames.Remove(keyFrame);
				base.WritePostscript();
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.CharKeyFrame" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Animation.CharKeyFrame" /> a ser removido.</param>
		// Token: 0x06003687 RID: 13959 RVA: 0x000D9AE4 File Offset: 0x000D8EE4
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
		// Token: 0x17000B0D RID: 2829
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (CharKeyFrame)value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Animation.CharKeyFrame" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Animation.CharKeyFrame" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.CharKeyFrame" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.CharKeyFrameCollection.Count" />.</exception>
		// Token: 0x17000B0E RID: 2830
		public CharKeyFrame this[int index]
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
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "CharKeyFrameCollection[{0}]", new object[]
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

		// Token: 0x04001661 RID: 5729
		private List<CharKeyFrame> _keyFrames;

		// Token: 0x04001662 RID: 5730
		private static CharKeyFrameCollection s_emptyCollection;
	}
}
