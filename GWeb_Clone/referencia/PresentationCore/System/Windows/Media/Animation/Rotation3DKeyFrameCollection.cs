using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" /> .</summary>
	// Token: 0x0200053F RID: 1343
	public class Rotation3DKeyFrameCollection : Freezable, IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrameCollection" />.</summary>
		// Token: 0x06003D75 RID: 15733 RVA: 0x000F2044 File Offset: 0x000F1444
		public Rotation3DKeyFrameCollection()
		{
			this._keyFrames = new List<Rotation3DKeyFrame>(2);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrameCollection" /> vazio.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrameCollection" /> vazio.</returns>
		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06003D76 RID: 15734 RVA: 0x000F2064 File Offset: 0x000F1464
		public static Rotation3DKeyFrameCollection Empty
		{
			get
			{
				if (Rotation3DKeyFrameCollection.s_emptyCollection == null)
				{
					Rotation3DKeyFrameCollection rotation3DKeyFrameCollection = new Rotation3DKeyFrameCollection();
					rotation3DKeyFrameCollection._keyFrames = new List<Rotation3DKeyFrame>(0);
					rotation3DKeyFrameCollection.Freeze();
					Rotation3DKeyFrameCollection.s_emptyCollection = rotation3DKeyFrameCollection;
				}
				return Rotation3DKeyFrameCollection.s_emptyCollection;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrameCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003D77 RID: 15735 RVA: 0x000F209C File Offset: 0x000F149C
		public new Rotation3DKeyFrameCollection Clone()
		{
			return (Rotation3DKeyFrameCollection)base.Clone();
		}

		/// <summary>Cria uma nova instância congelada de <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrameCollection" />.</summary>
		/// <returns>Uma instância congelada de <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrameCollection" />.</returns>
		// Token: 0x06003D78 RID: 15736 RVA: 0x000F20B4 File Offset: 0x000F14B4
		protected override Freezable CreateInstanceCore()
		{
			return new Rotation3DKeyFrameCollection();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrameCollection" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003D79 RID: 15737 RVA: 0x000F20C8 File Offset: 0x000F14C8
		protected override void CloneCore(Freezable sourceFreezable)
		{
			Rotation3DKeyFrameCollection rotation3DKeyFrameCollection = (Rotation3DKeyFrameCollection)sourceFreezable;
			base.CloneCore(sourceFreezable);
			int count = rotation3DKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Rotation3DKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Rotation3DKeyFrame rotation3DKeyFrame = (Rotation3DKeyFrame)rotation3DKeyFrameCollection._keyFrames[i].Clone();
				this._keyFrames.Add(rotation3DKeyFrame);
				base.OnFreezablePropertyChanged(null, rotation3DKeyFrame);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrameCollection" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003D7A RID: 15738 RVA: 0x000F2134 File Offset: 0x000F1534
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			Rotation3DKeyFrameCollection rotation3DKeyFrameCollection = (Rotation3DKeyFrameCollection)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			int count = rotation3DKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Rotation3DKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Rotation3DKeyFrame rotation3DKeyFrame = (Rotation3DKeyFrame)rotation3DKeyFrameCollection._keyFrames[i].CloneCurrentValue();
				this._keyFrames.Add(rotation3DKeyFrame);
				base.OnFreezablePropertyChanged(null, rotation3DKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrameCollection" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003D7B RID: 15739 RVA: 0x000F21A0 File Offset: 0x000F15A0
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			Rotation3DKeyFrameCollection rotation3DKeyFrameCollection = (Rotation3DKeyFrameCollection)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			int count = rotation3DKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Rotation3DKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Rotation3DKeyFrame rotation3DKeyFrame = (Rotation3DKeyFrame)rotation3DKeyFrameCollection._keyFrames[i].GetAsFrozen();
				this._keyFrames.Add(rotation3DKeyFrame);
				base.OnFreezablePropertyChanged(null, rotation3DKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrameCollection" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrameCollection" /> a ser copiado e congelado.</param>
		// Token: 0x06003D7C RID: 15740 RVA: 0x000F220C File Offset: 0x000F160C
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			Rotation3DKeyFrameCollection rotation3DKeyFrameCollection = (Rotation3DKeyFrameCollection)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			int count = rotation3DKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Rotation3DKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Rotation3DKeyFrame rotation3DKeyFrame = (Rotation3DKeyFrame)rotation3DKeyFrameCollection._keyFrames[i].GetCurrentValueAsFrozen();
				this._keyFrames.Add(rotation3DKeyFrame);
				base.OnFreezablePropertyChanged(null, rotation3DKeyFrame);
			}
		}

		/// <summary>Torna esta instância de <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrameCollection" /> não modificável ou determina se ela pode ser tornada não modificável.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> a ser verificado se esta instância puder ser congelada; <see langword="false" /> para congelar esta instância.</param>
		/// <returns>Se <paramref name="isChecking" /> é true, este método retorna <see langword="true" /> caso esta instância possa se tornar somente leitura ou então <see langword="false" /> se ela não pode se tornar somente leitura. Se <paramref name="isChecking" /> é false, este método retorna <see langword="true" /> se esta instância agora é somente leitura ou <see langword="false" />, se ela não pode tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x06003D7D RID: 15741 RVA: 0x000F2278 File Offset: 0x000F1678
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
		// Token: 0x06003D7E RID: 15742 RVA: 0x000F22C0 File Offset: 0x000F16C0
		public IEnumerator GetEnumerator()
		{
			base.ReadPreamble();
			return this._keyFrames.GetEnumerator();
		}

		/// <summary>Obtém o número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrameCollection" />.</summary>
		/// <returns>O número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrameCollection" />.</returns>
		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06003D7F RID: 15743 RVA: 0x000F22E4 File Offset: 0x000F16E4
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
		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06003D80 RID: 15744 RVA: 0x000F2304 File Offset: 0x000F1704
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
		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06003D81 RID: 15745 RVA: 0x000F232C File Offset: 0x000F172C
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
		// Token: 0x06003D82 RID: 15746 RVA: 0x000F234C File Offset: 0x000F174C
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			((ICollection)this._keyFrames).CopyTo(array, index);
		}

		/// <summary>Copia todos os objetos de <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		// Token: 0x06003D83 RID: 15747 RVA: 0x000F236C File Offset: 0x000F176C
		public void CopyTo(Rotation3DKeyFrame[] array, int index)
		{
			base.ReadPreamble();
			this._keyFrames.CopyTo(array, index);
		}

		/// <summary>Adiciona um item ao <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser adicionado ao <see cref="T:System.Collections.IList" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido, ou -1 para indicar que o item não foi inserido na coleção.</returns>
		// Token: 0x06003D84 RID: 15748 RVA: 0x000F238C File Offset: 0x000F178C
		int IList.Add(object keyFrame)
		{
			return this.Add((Rotation3DKeyFrame)keyFrame);
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" /> ao final da coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" /> a adicionar ao final da coleção.</param>
		/// <returns>O índice ao qual o <paramref name="keyFrame" /> foi adicionado.</returns>
		// Token: 0x06003D85 RID: 15749 RVA: 0x000F23A8 File Offset: 0x000F17A8
		public int Add(Rotation3DKeyFrame keyFrame)
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

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" /> da coleção.</summary>
		// Token: 0x06003D86 RID: 15750 RVA: 0x000F23F0 File Offset: 0x000F17F0
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
		// Token: 0x06003D87 RID: 15751 RVA: 0x000F244C File Offset: 0x000F184C
		bool IList.Contains(object keyFrame)
		{
			return this.Contains((Rotation3DKeyFrame)keyFrame);
		}

		/// <summary>Obtém um valor que indica se a coleção contém o <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" /> especificado.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="keyFrame" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003D88 RID: 15752 RVA: 0x000F2468 File Offset: 0x000F1868
		public bool Contains(Rotation3DKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.Contains(keyFrame);
		}

		/// <summary>Determina o índice de um item específico em <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06003D89 RID: 15753 RVA: 0x000F2488 File Offset: 0x000F1888
		int IList.IndexOf(object keyFrame)
		{
			return this.IndexOf((Rotation3DKeyFrame)keyFrame);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="keyFrame" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x06003D8A RID: 15754 RVA: 0x000F24A4 File Offset: 0x000F18A4
		public int IndexOf(Rotation3DKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.IndexOf(keyFrame);
		}

		/// <summary>Insere um item na <see cref="T:System.Collections.IList" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor deve ser inserido.</param>
		/// <param name="keyFrame">O objeto a ser inserido no <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06003D8B RID: 15755 RVA: 0x000F24C4 File Offset: 0x000F18C4
		void IList.Insert(int index, object keyFrame)
		{
			this.Insert(index, (Rotation3DKeyFrame)keyFrame);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" /> é inserido.</param>
		/// <param name="keyFrame">O objeto <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" /> a ser inserido na coleção.</param>
		// Token: 0x06003D8C RID: 15756 RVA: 0x000F24E0 File Offset: 0x000F18E0
		public void Insert(int index, Rotation3DKeyFrame keyFrame)
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

		/// <summary>Obtém um valor que indica se a coleção está congelada.</summary>
		/// <returns>
		///   <see langword="true" /> Se a coleção está congelada; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06003D8D RID: 15757 RVA: 0x000F251C File Offset: 0x000F191C
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
		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06003D8E RID: 15758 RVA: 0x000F2538 File Offset: 0x000F1938
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
		// Token: 0x06003D8F RID: 15759 RVA: 0x000F2554 File Offset: 0x000F1954
		void IList.Remove(object keyFrame)
		{
			this.Remove((Rotation3DKeyFrame)keyFrame);
		}

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" /> da coleção.</summary>
		/// <param name="keyFrame">Identifica o <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" /> a ser removido da coleção.</param>
		// Token: 0x06003D90 RID: 15760 RVA: 0x000F2570 File Offset: 0x000F1970
		public void Remove(Rotation3DKeyFrame keyFrame)
		{
			base.WritePreamble();
			if (this._keyFrames.Contains(keyFrame))
			{
				base.OnFreezablePropertyChanged(keyFrame, null);
				this._keyFrames.Remove(keyFrame);
				base.WritePostscript();
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" /> a ser removido.</param>
		// Token: 0x06003D91 RID: 15761 RVA: 0x000F25AC File Offset: 0x000F19AC
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
		// Token: 0x17000C64 RID: 3172
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (Rotation3DKeyFrame)value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.Vector3DKeyFrameCollection.Count" />.</exception>
		// Token: 0x17000C65 RID: 3173
		public Rotation3DKeyFrame this[int index]
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
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "Rotation3DKeyFrameCollection[{0}]", new object[]
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

		// Token: 0x04001730 RID: 5936
		private List<Rotation3DKeyFrame> _keyFrames;

		// Token: 0x04001731 RID: 5937
		private static Rotation3DKeyFrameCollection s_emptyCollection;
	}
}
