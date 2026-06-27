using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.StringKeyFrame" /> .</summary>
	// Token: 0x0200055C RID: 1372
	public class StringKeyFrameCollection : Freezable, IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.StringKeyFrameCollection" />.</summary>
		// Token: 0x06003F0D RID: 16141 RVA: 0x000F7C40 File Offset: 0x000F7040
		public StringKeyFrameCollection()
		{
			this._keyFrames = new List<StringKeyFrame>(2);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.StringKeyFrameCollection" /> vazio.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.StringKeyFrameCollection" /> vazio.</returns>
		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06003F0E RID: 16142 RVA: 0x000F7C60 File Offset: 0x000F7060
		public static StringKeyFrameCollection Empty
		{
			get
			{
				if (StringKeyFrameCollection.s_emptyCollection == null)
				{
					StringKeyFrameCollection stringKeyFrameCollection = new StringKeyFrameCollection();
					stringKeyFrameCollection._keyFrames = new List<StringKeyFrame>(0);
					stringKeyFrameCollection.Freeze();
					StringKeyFrameCollection.s_emptyCollection = stringKeyFrameCollection;
				}
				return StringKeyFrameCollection.s_emptyCollection;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.StringKeyFrameCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003F0F RID: 16143 RVA: 0x000F7C98 File Offset: 0x000F7098
		public new StringKeyFrameCollection Clone()
		{
			return (StringKeyFrameCollection)base.Clone();
		}

		/// <summary>Cria uma nova instância congelada de <see cref="T:System.Windows.Media.Animation.StringKeyFrameCollection" />.</summary>
		/// <returns>Uma instância congelada de <see cref="T:System.Windows.Media.Animation.StringKeyFrameCollection" />.</returns>
		// Token: 0x06003F10 RID: 16144 RVA: 0x000F7CB0 File Offset: 0x000F70B0
		protected override Freezable CreateInstanceCore()
		{
			return new StringKeyFrameCollection();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.StringKeyFrameCollection" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.StringKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003F11 RID: 16145 RVA: 0x000F7CC4 File Offset: 0x000F70C4
		protected override void CloneCore(Freezable sourceFreezable)
		{
			StringKeyFrameCollection stringKeyFrameCollection = (StringKeyFrameCollection)sourceFreezable;
			base.CloneCore(sourceFreezable);
			int count = stringKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<StringKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				StringKeyFrame stringKeyFrame = (StringKeyFrame)stringKeyFrameCollection._keyFrames[i].Clone();
				this._keyFrames.Add(stringKeyFrame);
				base.OnFreezablePropertyChanged(null, stringKeyFrame);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.StringKeyFrameCollection" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.StringKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003F12 RID: 16146 RVA: 0x000F7D30 File Offset: 0x000F7130
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			StringKeyFrameCollection stringKeyFrameCollection = (StringKeyFrameCollection)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			int count = stringKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<StringKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				StringKeyFrame stringKeyFrame = (StringKeyFrame)stringKeyFrameCollection._keyFrames[i].CloneCurrentValue();
				this._keyFrames.Add(stringKeyFrame);
				base.OnFreezablePropertyChanged(null, stringKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.StringKeyFrameCollection" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.StringKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003F13 RID: 16147 RVA: 0x000F7D9C File Offset: 0x000F719C
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			StringKeyFrameCollection stringKeyFrameCollection = (StringKeyFrameCollection)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			int count = stringKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<StringKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				StringKeyFrame stringKeyFrame = (StringKeyFrame)stringKeyFrameCollection._keyFrames[i].GetAsFrozen();
				this._keyFrames.Add(stringKeyFrame);
				base.OnFreezablePropertyChanged(null, stringKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.StringKeyFrameCollection" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.StringKeyFrameCollection" /> a ser copiado e congelado.</param>
		// Token: 0x06003F14 RID: 16148 RVA: 0x000F7E08 File Offset: 0x000F7208
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			StringKeyFrameCollection stringKeyFrameCollection = (StringKeyFrameCollection)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			int count = stringKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<StringKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				StringKeyFrame stringKeyFrame = (StringKeyFrame)stringKeyFrameCollection._keyFrames[i].GetCurrentValueAsFrozen();
				this._keyFrames.Add(stringKeyFrame);
				base.OnFreezablePropertyChanged(null, stringKeyFrame);
			}
		}

		/// <summary>Torna esta instância de <see cref="T:System.Windows.Media.Animation.StringKeyFrameCollection" /> não modificável ou determina se ela pode ser tornada não modificável.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> a ser verificado se esta instância puder ser congelada; <see langword="false" /> para congelar esta instância.</param>
		/// <returns>Se <paramref name="isChecking" /> é true, este método retorna <see langword="true" /> caso esta instância possa se tornar somente leitura ou então <see langword="false" /> se ela não pode se tornar somente leitura. Se <paramref name="isChecking" /> é false, este método retorna <see langword="true" /> se esta instância agora é somente leitura ou <see langword="false" />, se ela não pode tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x06003F15 RID: 16149 RVA: 0x000F7E74 File Offset: 0x000F7274
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
		// Token: 0x06003F16 RID: 16150 RVA: 0x000F7EBC File Offset: 0x000F72BC
		public IEnumerator GetEnumerator()
		{
			base.ReadPreamble();
			return this._keyFrames.GetEnumerator();
		}

		/// <summary>Obtém o número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.StringKeyFrameCollection" />.</summary>
		/// <returns>O número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.StringKeyFrameCollection" />.</returns>
		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06003F17 RID: 16151 RVA: 0x000F7EE0 File Offset: 0x000F72E0
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
		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06003F18 RID: 16152 RVA: 0x000F7F00 File Offset: 0x000F7300
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
		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06003F19 RID: 16153 RVA: 0x000F7F28 File Offset: 0x000F7328
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
		// Token: 0x06003F1A RID: 16154 RVA: 0x000F7F48 File Offset: 0x000F7348
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			((ICollection)this._keyFrames).CopyTo(array, index);
		}

		/// <summary>Copia todos os objetos de <see cref="T:System.Windows.Media.Animation.StringKeyFrame" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		// Token: 0x06003F1B RID: 16155 RVA: 0x000F7F68 File Offset: 0x000F7368
		public void CopyTo(StringKeyFrame[] array, int index)
		{
			base.ReadPreamble();
			this._keyFrames.CopyTo(array, index);
		}

		/// <summary>Adiciona um item ao <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser adicionado ao <see cref="T:System.Collections.IList" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido, ou -1 para indicar que o item não foi inserido na coleção.</returns>
		// Token: 0x06003F1C RID: 16156 RVA: 0x000F7F88 File Offset: 0x000F7388
		int IList.Add(object keyFrame)
		{
			return this.Add((StringKeyFrame)keyFrame);
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.StringKeyFrame" /> ao final da coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.StringKeyFrame" /> a adicionar ao final da coleção.</param>
		/// <returns>O índice ao qual o <paramref name="keyFrame" /> foi adicionado.</returns>
		// Token: 0x06003F1D RID: 16157 RVA: 0x000F7FA4 File Offset: 0x000F73A4
		public int Add(StringKeyFrame keyFrame)
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

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Animation.StringKeyFrame" /> da coleção.</summary>
		// Token: 0x06003F1E RID: 16158 RVA: 0x000F7FEC File Offset: 0x000F73EC
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
		// Token: 0x06003F1F RID: 16159 RVA: 0x000F8048 File Offset: 0x000F7448
		bool IList.Contains(object keyFrame)
		{
			return this.Contains((StringKeyFrame)keyFrame);
		}

		/// <summary>Obtém um valor que indica se a coleção contém o <see cref="T:System.Windows.Media.Animation.StringKeyFrame" /> especificado.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.StringKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="keyFrame" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003F20 RID: 16160 RVA: 0x000F8064 File Offset: 0x000F7464
		public bool Contains(StringKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.Contains(keyFrame);
		}

		/// <summary>Determina o índice de um item específico em <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06003F21 RID: 16161 RVA: 0x000F8084 File Offset: 0x000F7484
		int IList.IndexOf(object keyFrame)
		{
			return this.IndexOf((StringKeyFrame)keyFrame);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Animation.StringKeyFrame" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.StringKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="keyFrame" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x06003F22 RID: 16162 RVA: 0x000F80A0 File Offset: 0x000F74A0
		public int IndexOf(StringKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.IndexOf(keyFrame);
		}

		/// <summary>Insere um item na <see cref="T:System.Collections.IList" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor deve ser inserido.</param>
		/// <param name="keyFrame">O objeto a ser inserido no <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06003F23 RID: 16163 RVA: 0x000F80C0 File Offset: 0x000F74C0
		void IList.Insert(int index, object keyFrame)
		{
			this.Insert(index, (StringKeyFrame)keyFrame);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Animation.StringKeyFrame" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Animation.StringKeyFrame" /> é inserido.</param>
		/// <param name="keyFrame">O objeto <see cref="T:System.Windows.Media.Animation.StringKeyFrame" /> a ser inserido na coleção.</param>
		// Token: 0x06003F24 RID: 16164 RVA: 0x000F80DC File Offset: 0x000F74DC
		public void Insert(int index, StringKeyFrame keyFrame)
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
		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06003F25 RID: 16165 RVA: 0x000F8118 File Offset: 0x000F7518
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
		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06003F26 RID: 16166 RVA: 0x000F8134 File Offset: 0x000F7534
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
		// Token: 0x06003F27 RID: 16167 RVA: 0x000F8150 File Offset: 0x000F7550
		void IList.Remove(object keyFrame)
		{
			this.Remove((StringKeyFrame)keyFrame);
		}

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.Animation.StringKeyFrame" /> da coleção.</summary>
		/// <param name="keyFrame">Identifica o <see cref="T:System.Windows.Media.Animation.StringKeyFrame" /> a ser removido da coleção.</param>
		// Token: 0x06003F28 RID: 16168 RVA: 0x000F816C File Offset: 0x000F756C
		public void Remove(StringKeyFrame keyFrame)
		{
			base.WritePreamble();
			if (this._keyFrames.Contains(keyFrame))
			{
				base.OnFreezablePropertyChanged(keyFrame, null);
				this._keyFrames.Remove(keyFrame);
				base.WritePostscript();
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.StringKeyFrame" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Animation.StringKeyFrame" /> a ser removido.</param>
		// Token: 0x06003F29 RID: 16169 RVA: 0x000F81A8 File Offset: 0x000F75A8
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
		// Token: 0x17000CAD RID: 3245
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (StringKeyFrame)value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Animation.StringKeyFrame" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Animation.StringKeyFrame" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.StringKeyFrame" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.StringKeyFrameCollection.Count" />.</exception>
		// Token: 0x17000CAE RID: 3246
		public StringKeyFrame this[int index]
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
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "StringKeyFrameCollection[{0}]", new object[]
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

		// Token: 0x0400175F RID: 5983
		private List<StringKeyFrame> _keyFrames;

		// Token: 0x04001760 RID: 5984
		private static StringKeyFrameCollection s_emptyCollection;
	}
}
