using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> .</summary>
	// Token: 0x02000564 RID: 1380
	public class Vector3DKeyFrameCollection : Freezable, IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrameCollection" />.</summary>
		// Token: 0x06003FE5 RID: 16357 RVA: 0x000FACC8 File Offset: 0x000FA0C8
		public Vector3DKeyFrameCollection()
		{
			this._keyFrames = new List<Vector3DKeyFrame>(2);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrameCollection" /> vazio.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrameCollection" /> vazio.</returns>
		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06003FE6 RID: 16358 RVA: 0x000FACE8 File Offset: 0x000FA0E8
		public static Vector3DKeyFrameCollection Empty
		{
			get
			{
				if (Vector3DKeyFrameCollection.s_emptyCollection == null)
				{
					Vector3DKeyFrameCollection vector3DKeyFrameCollection = new Vector3DKeyFrameCollection();
					vector3DKeyFrameCollection._keyFrames = new List<Vector3DKeyFrame>(0);
					vector3DKeyFrameCollection.Freeze();
					Vector3DKeyFrameCollection.s_emptyCollection = vector3DKeyFrameCollection;
				}
				return Vector3DKeyFrameCollection.s_emptyCollection;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrameCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003FE7 RID: 16359 RVA: 0x000FAD20 File Offset: 0x000FA120
		public new Vector3DKeyFrameCollection Clone()
		{
			return (Vector3DKeyFrameCollection)base.Clone();
		}

		/// <summary>Cria uma nova instância congelada de <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrameCollection" />.</summary>
		/// <returns>Uma instância congelada de <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrameCollection" />.</returns>
		// Token: 0x06003FE8 RID: 16360 RVA: 0x000FAD38 File Offset: 0x000FA138
		protected override Freezable CreateInstanceCore()
		{
			return new Vector3DKeyFrameCollection();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrameCollection" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003FE9 RID: 16361 RVA: 0x000FAD4C File Offset: 0x000FA14C
		protected override void CloneCore(Freezable sourceFreezable)
		{
			Vector3DKeyFrameCollection vector3DKeyFrameCollection = (Vector3DKeyFrameCollection)sourceFreezable;
			base.CloneCore(sourceFreezable);
			int count = vector3DKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Vector3DKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Vector3DKeyFrame vector3DKeyFrame = (Vector3DKeyFrame)vector3DKeyFrameCollection._keyFrames[i].Clone();
				this._keyFrames.Add(vector3DKeyFrame);
				base.OnFreezablePropertyChanged(null, vector3DKeyFrame);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrameCollection" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003FEA RID: 16362 RVA: 0x000FADB8 File Offset: 0x000FA1B8
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			Vector3DKeyFrameCollection vector3DKeyFrameCollection = (Vector3DKeyFrameCollection)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			int count = vector3DKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Vector3DKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Vector3DKeyFrame vector3DKeyFrame = (Vector3DKeyFrame)vector3DKeyFrameCollection._keyFrames[i].CloneCurrentValue();
				this._keyFrames.Add(vector3DKeyFrame);
				base.OnFreezablePropertyChanged(null, vector3DKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrameCollection" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003FEB RID: 16363 RVA: 0x000FAE24 File Offset: 0x000FA224
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			Vector3DKeyFrameCollection vector3DKeyFrameCollection = (Vector3DKeyFrameCollection)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			int count = vector3DKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Vector3DKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Vector3DKeyFrame vector3DKeyFrame = (Vector3DKeyFrame)vector3DKeyFrameCollection._keyFrames[i].GetAsFrozen();
				this._keyFrames.Add(vector3DKeyFrame);
				base.OnFreezablePropertyChanged(null, vector3DKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrameCollection" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrameCollection" /> a ser copiado e congelado.</param>
		// Token: 0x06003FEC RID: 16364 RVA: 0x000FAE90 File Offset: 0x000FA290
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			Vector3DKeyFrameCollection vector3DKeyFrameCollection = (Vector3DKeyFrameCollection)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			int count = vector3DKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Vector3DKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Vector3DKeyFrame vector3DKeyFrame = (Vector3DKeyFrame)vector3DKeyFrameCollection._keyFrames[i].GetCurrentValueAsFrozen();
				this._keyFrames.Add(vector3DKeyFrame);
				base.OnFreezablePropertyChanged(null, vector3DKeyFrame);
			}
		}

		/// <summary>Torna esta instância de <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrameCollection" /> não modificável ou determina se ela pode ser tornada não modificável.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> a ser verificado se esta instância puder ser congelada; <see langword="false" /> para congelar esta instância.</param>
		/// <returns>Se <paramref name="isChecking" /> é true, este método retorna <see langword="true" /> caso esta instância possa se tornar somente leitura ou então <see langword="false" /> se ela não pode se tornar somente leitura.  
		/// Se <paramref name="isChecking" /> é false, este método retorna <see langword="true" /> se esta instância agora é somente leitura ou <see langword="false" />, se ela não pode tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x06003FED RID: 16365 RVA: 0x000FAEFC File Offset: 0x000FA2FC
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
		// Token: 0x06003FEE RID: 16366 RVA: 0x000FAF44 File Offset: 0x000FA344
		public IEnumerator GetEnumerator()
		{
			base.ReadPreamble();
			return this._keyFrames.GetEnumerator();
		}

		/// <summary>Obtém o número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrameCollection" />.</summary>
		/// <returns>O número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrameCollection" />.</returns>
		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06003FEF RID: 16367 RVA: 0x000FAF68 File Offset: 0x000FA368
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
		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06003FF0 RID: 16368 RVA: 0x000FAF88 File Offset: 0x000FA388
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
		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06003FF1 RID: 16369 RVA: 0x000FAFB0 File Offset: 0x000FA3B0
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
		// Token: 0x06003FF2 RID: 16370 RVA: 0x000FAFD0 File Offset: 0x000FA3D0
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			((ICollection)this._keyFrames).CopyTo(array, index);
		}

		/// <summary>Copia todos os objetos de <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		// Token: 0x06003FF3 RID: 16371 RVA: 0x000FAFF0 File Offset: 0x000FA3F0
		public void CopyTo(Vector3DKeyFrame[] array, int index)
		{
			base.ReadPreamble();
			this._keyFrames.CopyTo(array, index);
		}

		/// <summary>Adiciona um item ao <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser adicionado ao <see cref="T:System.Collections.IList" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido, ou -1 para indicar que o item não foi inserido na coleção.</returns>
		// Token: 0x06003FF4 RID: 16372 RVA: 0x000FB010 File Offset: 0x000FA410
		int IList.Add(object keyFrame)
		{
			return this.Add((Vector3DKeyFrame)keyFrame);
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> ao final da coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> a adicionar ao final da coleção.</param>
		/// <returns>O índice ao qual o <paramref name="keyFrame" /> foi adicionado.</returns>
		// Token: 0x06003FF5 RID: 16373 RVA: 0x000FB02C File Offset: 0x000FA42C
		public int Add(Vector3DKeyFrame keyFrame)
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

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> da coleção.</summary>
		// Token: 0x06003FF6 RID: 16374 RVA: 0x000FB074 File Offset: 0x000FA474
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
		// Token: 0x06003FF7 RID: 16375 RVA: 0x000FB0D0 File Offset: 0x000FA4D0
		bool IList.Contains(object keyFrame)
		{
			return this.Contains((Vector3DKeyFrame)keyFrame);
		}

		/// <summary>Obtém um valor que indica se a coleção contém o <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> especificado.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="keyFrame" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003FF8 RID: 16376 RVA: 0x000FB0EC File Offset: 0x000FA4EC
		public bool Contains(Vector3DKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.Contains(keyFrame);
		}

		/// <summary>Determina o índice de um item específico em <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06003FF9 RID: 16377 RVA: 0x000FB10C File Offset: 0x000FA50C
		int IList.IndexOf(object keyFrame)
		{
			return this.IndexOf((Vector3DKeyFrame)keyFrame);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="keyFrame" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x06003FFA RID: 16378 RVA: 0x000FB128 File Offset: 0x000FA528
		public int IndexOf(Vector3DKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.IndexOf(keyFrame);
		}

		/// <summary>Insere um item na <see cref="T:System.Collections.IList" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor deve ser inserido.</param>
		/// <param name="keyFrame">O objeto a ser inserido no <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06003FFB RID: 16379 RVA: 0x000FB148 File Offset: 0x000FA548
		void IList.Insert(int index, object keyFrame)
		{
			this.Insert(index, (Vector3DKeyFrame)keyFrame);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> é inserido.</param>
		/// <param name="keyFrame">O objeto <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> a ser inserido na coleção.</param>
		// Token: 0x06003FFC RID: 16380 RVA: 0x000FB164 File Offset: 0x000FA564
		public void Insert(int index, Vector3DKeyFrame keyFrame)
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
		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06003FFD RID: 16381 RVA: 0x000FB1A0 File Offset: 0x000FA5A0
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
		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06003FFE RID: 16382 RVA: 0x000FB1BC File Offset: 0x000FA5BC
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
		// Token: 0x06003FFF RID: 16383 RVA: 0x000FB1D8 File Offset: 0x000FA5D8
		void IList.Remove(object keyFrame)
		{
			this.Remove((Vector3DKeyFrame)keyFrame);
		}

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> da coleção.</summary>
		/// <param name="keyFrame">Identifica o <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> a ser removido da coleção.</param>
		// Token: 0x06004000 RID: 16384 RVA: 0x000FB1F4 File Offset: 0x000FA5F4
		public void Remove(Vector3DKeyFrame keyFrame)
		{
			base.WritePreamble();
			if (this._keyFrames.Contains(keyFrame))
			{
				base.OnFreezablePropertyChanged(keyFrame, null);
				this._keyFrames.Remove(keyFrame);
				base.WritePostscript();
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> a ser removido.</param>
		// Token: 0x06004001 RID: 16385 RVA: 0x000FB230 File Offset: 0x000FA630
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
		// Token: 0x17000CD6 RID: 3286
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (Vector3DKeyFrame)value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.Vector3DKeyFrameCollection.Count" />.</exception>
		// Token: 0x17000CD7 RID: 3287
		public Vector3DKeyFrame this[int index]
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
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "Vector3DKeyFrameCollection[{0}]", new object[]
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

		// Token: 0x04001780 RID: 6016
		private List<Vector3DKeyFrame> _keyFrames;

		// Token: 0x04001781 RID: 6017
		private static Vector3DKeyFrameCollection s_emptyCollection;
	}
}
