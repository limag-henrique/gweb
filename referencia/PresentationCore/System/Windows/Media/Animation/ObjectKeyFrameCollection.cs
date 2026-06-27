using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" /> .</summary>
	// Token: 0x02000524 RID: 1316
	public class ObjectKeyFrameCollection : Freezable, IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" />.</summary>
		// Token: 0x06003B7A RID: 15226 RVA: 0x000E9C28 File Offset: 0x000E9028
		public ObjectKeyFrameCollection()
		{
			this._keyFrames = new List<ObjectKeyFrame>(2);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" /> vazio.</summary>
		/// <returns>Uma coleção vazia.</returns>
		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06003B7B RID: 15227 RVA: 0x000E9C48 File Offset: 0x000E9048
		public static ObjectKeyFrameCollection Empty
		{
			get
			{
				if (ObjectKeyFrameCollection.s_emptyCollection == null)
				{
					ObjectKeyFrameCollection objectKeyFrameCollection = new ObjectKeyFrameCollection();
					objectKeyFrameCollection._keyFrames = new List<ObjectKeyFrame>(0);
					objectKeyFrameCollection.Freeze();
					ObjectKeyFrameCollection.s_emptyCollection = objectKeyFrameCollection;
				}
				return ObjectKeyFrameCollection.s_emptyCollection;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003B7C RID: 15228 RVA: 0x000E9C80 File Offset: 0x000E9080
		public new ObjectKeyFrameCollection Clone()
		{
			return (ObjectKeyFrameCollection)base.Clone();
		}

		/// <summary>Cria uma nova instância congelada de <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" />.</summary>
		/// <returns>Uma instância congelada de <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" />.</returns>
		// Token: 0x06003B7D RID: 15229 RVA: 0x000E9C98 File Offset: 0x000E9098
		protected override Freezable CreateInstanceCore()
		{
			return new ObjectKeyFrameCollection();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003B7E RID: 15230 RVA: 0x000E9CAC File Offset: 0x000E90AC
		protected override void CloneCore(Freezable sourceFreezable)
		{
			ObjectKeyFrameCollection objectKeyFrameCollection = (ObjectKeyFrameCollection)sourceFreezable;
			base.CloneCore(sourceFreezable);
			int count = objectKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<ObjectKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				ObjectKeyFrame objectKeyFrame = (ObjectKeyFrame)objectKeyFrameCollection._keyFrames[i].Clone();
				this._keyFrames.Add(objectKeyFrame);
				base.OnFreezablePropertyChanged(null, objectKeyFrame);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003B7F RID: 15231 RVA: 0x000E9D18 File Offset: 0x000E9118
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			ObjectKeyFrameCollection objectKeyFrameCollection = (ObjectKeyFrameCollection)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			int count = objectKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<ObjectKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				ObjectKeyFrame objectKeyFrame = (ObjectKeyFrame)objectKeyFrameCollection._keyFrames[i].CloneCurrentValue();
				this._keyFrames.Add(objectKeyFrame);
				base.OnFreezablePropertyChanged(null, objectKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003B80 RID: 15232 RVA: 0x000E9D84 File Offset: 0x000E9184
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			ObjectKeyFrameCollection objectKeyFrameCollection = (ObjectKeyFrameCollection)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			int count = objectKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<ObjectKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				ObjectKeyFrame objectKeyFrame = (ObjectKeyFrame)objectKeyFrameCollection._keyFrames[i].GetAsFrozen();
				this._keyFrames.Add(objectKeyFrame);
				base.OnFreezablePropertyChanged(null, objectKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" /> a ser copiado e congelado.</param>
		// Token: 0x06003B81 RID: 15233 RVA: 0x000E9DF0 File Offset: 0x000E91F0
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			ObjectKeyFrameCollection objectKeyFrameCollection = (ObjectKeyFrameCollection)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			int count = objectKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<ObjectKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				ObjectKeyFrame objectKeyFrame = (ObjectKeyFrame)objectKeyFrameCollection._keyFrames[i].GetCurrentValueAsFrozen();
				this._keyFrames.Add(objectKeyFrame);
				base.OnFreezablePropertyChanged(null, objectKeyFrame);
			}
		}

		/// <summary>Torna esta instância de <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" /> não modificável ou determina se ela pode ser tornada não modificável.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> a ser verificado se esta instância puder ser congelada; <see langword="false" /> para congelar esta instância.</param>
		/// <returns>Se <paramref name="isChecking" /> é true, este método retorna <see langword="true" /> caso esta instância possa se tornar somente leitura ou então <see langword="false" /> se ela não pode se tornar somente leitura. Se <paramref name="isChecking" /> é false, este método retorna <see langword="true" /> se esta instância agora é somente leitura ou <see langword="false" />, se ela não pode tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x06003B82 RID: 15234 RVA: 0x000E9E5C File Offset: 0x000E925C
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
		// Token: 0x06003B83 RID: 15235 RVA: 0x000E9EA4 File Offset: 0x000E92A4
		public IEnumerator GetEnumerator()
		{
			base.ReadPreamble();
			return this._keyFrames.GetEnumerator();
		}

		/// <summary>Obtém o número de quadros-chave contidos neste <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" />.</summary>
		/// <returns>O número de quadros-chave contidos neste <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" />.</returns>
		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06003B84 RID: 15236 RVA: 0x000E9EC8 File Offset: 0x000E92C8
		public int Count
		{
			get
			{
				base.ReadPreamble();
				return this._keyFrames.Count;
			}
		}

		/// <summary>Obtém um valor que indica se o acesso a este <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" /> é sincronizado (thread-safe)</summary>
		/// <returns>
		///   <see langword="true" /> Se o acesso à coleção é sincronizado (thread-safe); Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06003B85 RID: 15237 RVA: 0x000E9EE8 File Offset: 0x000E92E8
		public bool IsSynchronized
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen || base.Dispatcher != null;
			}
		}

		/// <summary>Obtém um objeto que pode ser usado para sincronizar o acesso a este <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" />.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso ao <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" />.</returns>
		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06003B86 RID: 15238 RVA: 0x000E9F10 File Offset: 0x000E9310
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
		// Token: 0x06003B87 RID: 15239 RVA: 0x000E9F30 File Offset: 0x000E9330
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			((ICollection)this._keyFrames).CopyTo(array, index);
		}

		/// <summary>Copia o <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" /> para a matriz <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" /> unidimensional especificada, com início no índice especificado da matriz de destino.</summary>
		/// <param name="array">A matriz <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" /> unidimensional que é o destino dos quadros chave copiados deste <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" />. A matriz deve ter indexação com base em zero.</param>
		/// <param name="index">O índice de base zero na matriz no qual a cópia começa.</param>
		// Token: 0x06003B88 RID: 15240 RVA: 0x000E9F50 File Offset: 0x000E9350
		public void CopyTo(ObjectKeyFrame[] array, int index)
		{
			base.ReadPreamble();
			this._keyFrames.CopyTo(array, index);
		}

		/// <summary>Adiciona um item ao <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser adicionado ao <see cref="T:System.Collections.IList" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido, ou -1 para indicar que o item não foi inserido na coleção.</returns>
		// Token: 0x06003B89 RID: 15241 RVA: 0x000E9F70 File Offset: 0x000E9370
		int IList.Add(object keyFrame)
		{
			return this.Add((ObjectKeyFrame)keyFrame);
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" /> ao fim desta coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" /> a ser adicionado.</param>
		/// <returns>O índice ao qual o <paramref name="keyFrame" /> foi adicionado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		// Token: 0x06003B8A RID: 15242 RVA: 0x000E9F8C File Offset: 0x000E938C
		public int Add(ObjectKeyFrame keyFrame)
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

		/// <summary>Remove todos os quadros chave da coleção.</summary>
		// Token: 0x06003B8B RID: 15243 RVA: 0x000E9FD4 File Offset: 0x000E93D4
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
		// Token: 0x06003B8C RID: 15244 RVA: 0x000EA030 File Offset: 0x000E9430
		bool IList.Contains(object keyFrame)
		{
			return this.Contains((ObjectKeyFrame)keyFrame);
		}

		/// <summary>Indica se a coleção contém o <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" /> especificado.</summary>
		/// <param name="keyFrame">O quadro-chave a localizar na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="keyFrame" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003B8D RID: 15245 RVA: 0x000EA04C File Offset: 0x000E944C
		public bool Contains(ObjectKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.Contains(keyFrame);
		}

		/// <summary>Determina o índice de um item específico em <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06003B8E RID: 15246 RVA: 0x000EA06C File Offset: 0x000E946C
		int IList.IndexOf(object keyFrame)
		{
			return this.IndexOf((ObjectKeyFrame)keyFrame);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="keyFrame" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x06003B8F RID: 15247 RVA: 0x000EA088 File Offset: 0x000E9488
		public int IndexOf(ObjectKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.IndexOf(keyFrame);
		}

		/// <summary>Insere um item na <see cref="T:System.Collections.IList" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor deve ser inserido.</param>
		/// <param name="keyFrame">O objeto a ser inserido no <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06003B90 RID: 15248 RVA: 0x000EA0A8 File Offset: 0x000E94A8
		void IList.Insert(int index, object keyFrame)
		{
			this.Insert(index, (ObjectKeyFrame)keyFrame);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" /> é inserido.</param>
		/// <param name="keyFrame">O objeto <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" /> a ser inserido na coleção.</param>
		// Token: 0x06003B91 RID: 15249 RVA: 0x000EA0C4 File Offset: 0x000E94C4
		public void Insert(int index, ObjectKeyFrame keyFrame)
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

		/// <summary>Obtém um valor que indica se este <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" /> tem um tamanho fixo.</summary>
		/// <returns>
		///   <see langword="true" /> Se este <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" /> tem um fixo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06003B92 RID: 15250 RVA: 0x000EA100 File Offset: 0x000E9500
		public bool IsFixedSize
		{
			get
			{
				base.ReadPreamble();
				return base.IsFrozen;
			}
		}

		/// <summary>Obtém um valor que indica se este <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" /> é somente leitura.</summary>
		/// <returns>
		///   <see langword="true" /> Se esta coleção é somente leitura; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06003B93 RID: 15251 RVA: 0x000EA11C File Offset: 0x000E951C
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
		// Token: 0x06003B94 RID: 15252 RVA: 0x000EA138 File Offset: 0x000E9538
		void IList.Remove(object keyFrame)
		{
			this.Remove((ObjectKeyFrame)keyFrame);
		}

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" /> da coleção.</summary>
		/// <param name="keyFrame">Identifica o <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" /> a ser removido da coleção.</param>
		// Token: 0x06003B95 RID: 15253 RVA: 0x000EA154 File Offset: 0x000E9554
		public void Remove(ObjectKeyFrame keyFrame)
		{
			base.WritePreamble();
			if (this._keyFrames.Contains(keyFrame))
			{
				base.OnFreezablePropertyChanged(keyFrame, null);
				this._keyFrames.Remove(keyFrame);
				base.WritePostscript();
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" /> a ser removido.</param>
		// Token: 0x06003B96 RID: 15254 RVA: 0x000EA190 File Offset: 0x000E9590
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
		// Token: 0x17000BF4 RID: 3060
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (ObjectKeyFrame)value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Animation.ObjectKeyFrameCollection" /> no índice especificado.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.ObjectKeyFrameCollection.Count" />.</exception>
		// Token: 0x17000BF5 RID: 3061
		public ObjectKeyFrame this[int index]
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
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "ObjectKeyFrameCollection[{0}]", new object[]
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

		// Token: 0x040016F0 RID: 5872
		private List<ObjectKeyFrame> _keyFrames;

		// Token: 0x040016F1 RID: 5873
		private static ObjectKeyFrameCollection s_emptyCollection;
	}
}
