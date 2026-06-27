using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" /> .</summary>
	// Token: 0x0200052A RID: 1322
	public class Point3DKeyFrameCollection : Freezable, IList, ICollection, IEnumerable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Point3DKeyFrameCollection" />.</summary>
		// Token: 0x06003BE8 RID: 15336 RVA: 0x000EB810 File Offset: 0x000EAC10
		public Point3DKeyFrameCollection()
		{
			this._keyFrames = new List<Point3DKeyFrame>(2);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Animation.Point3DKeyFrameCollection" /> vazio.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.Point3DKeyFrameCollection" /> vazio.</returns>
		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06003BE9 RID: 15337 RVA: 0x000EB830 File Offset: 0x000EAC30
		public static Point3DKeyFrameCollection Empty
		{
			get
			{
				if (Point3DKeyFrameCollection.s_emptyCollection == null)
				{
					Point3DKeyFrameCollection point3DKeyFrameCollection = new Point3DKeyFrameCollection();
					point3DKeyFrameCollection._keyFrames = new List<Point3DKeyFrame>(0);
					point3DKeyFrameCollection.Freeze();
					Point3DKeyFrameCollection.s_emptyCollection = point3DKeyFrameCollection;
				}
				return Point3DKeyFrameCollection.s_emptyCollection;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.Point3DKeyFrameCollection" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003BEA RID: 15338 RVA: 0x000EB868 File Offset: 0x000EAC68
		public new Point3DKeyFrameCollection Clone()
		{
			return (Point3DKeyFrameCollection)base.Clone();
		}

		/// <summary>Cria uma nova instância congelada de <see cref="T:System.Windows.Media.Animation.Point3DKeyFrameCollection" />.</summary>
		/// <returns>Uma instância congelada de <see cref="T:System.Windows.Media.Animation.Point3DKeyFrameCollection" />.</returns>
		// Token: 0x06003BEB RID: 15339 RVA: 0x000EB880 File Offset: 0x000EAC80
		protected override Freezable CreateInstanceCore()
		{
			return new Point3DKeyFrameCollection();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.Point3DKeyFrameCollection" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Point3DKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003BEC RID: 15340 RVA: 0x000EB894 File Offset: 0x000EAC94
		protected override void CloneCore(Freezable sourceFreezable)
		{
			Point3DKeyFrameCollection point3DKeyFrameCollection = (Point3DKeyFrameCollection)sourceFreezable;
			base.CloneCore(sourceFreezable);
			int count = point3DKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Point3DKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Point3DKeyFrame point3DKeyFrame = (Point3DKeyFrame)point3DKeyFrameCollection._keyFrames[i].Clone();
				this._keyFrames.Add(point3DKeyFrame);
				base.OnFreezablePropertyChanged(null, point3DKeyFrame);
			}
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.Point3DKeyFrameCollection" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Point3DKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003BED RID: 15341 RVA: 0x000EB900 File Offset: 0x000EAD00
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			Point3DKeyFrameCollection point3DKeyFrameCollection = (Point3DKeyFrameCollection)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			int count = point3DKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Point3DKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Point3DKeyFrame point3DKeyFrame = (Point3DKeyFrame)point3DKeyFrameCollection._keyFrames[i].CloneCurrentValue();
				this._keyFrames.Add(point3DKeyFrame);
				base.OnFreezablePropertyChanged(null, point3DKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.Point3DKeyFrameCollection" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.Point3DKeyFrameCollection" /> a ser clonado.</param>
		// Token: 0x06003BEE RID: 15342 RVA: 0x000EB96C File Offset: 0x000EAD6C
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			Point3DKeyFrameCollection point3DKeyFrameCollection = (Point3DKeyFrameCollection)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			int count = point3DKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Point3DKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Point3DKeyFrame point3DKeyFrame = (Point3DKeyFrame)point3DKeyFrameCollection._keyFrames[i].GetAsFrozen();
				this._keyFrames.Add(point3DKeyFrame);
				base.OnFreezablePropertyChanged(null, point3DKeyFrame);
			}
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.Point3DKeyFrameCollection" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Point3DKeyFrameCollection" /> a ser copiado e congelado.</param>
		// Token: 0x06003BEF RID: 15343 RVA: 0x000EB9D8 File Offset: 0x000EADD8
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			Point3DKeyFrameCollection point3DKeyFrameCollection = (Point3DKeyFrameCollection)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			int count = point3DKeyFrameCollection._keyFrames.Count;
			this._keyFrames = new List<Point3DKeyFrame>(count);
			for (int i = 0; i < count; i++)
			{
				Point3DKeyFrame point3DKeyFrame = (Point3DKeyFrame)point3DKeyFrameCollection._keyFrames[i].GetCurrentValueAsFrozen();
				this._keyFrames.Add(point3DKeyFrame);
				base.OnFreezablePropertyChanged(null, point3DKeyFrame);
			}
		}

		/// <summary>Torna esta instância de <see cref="T:System.Windows.Media.Animation.Point3DKeyFrameCollection" /> não modificável ou determina se ela pode se tornar não modificável.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> se a instância <see cref="T:System.Windows.Freezable" />, na verdade, deve congelar a si mesma quando este método é chamado. <see langword="false" /> se o <see cref="T:System.Windows.Freezable" /> deve retornar apenas se ele pode ser congelado.</param>
		/// <returns>Se <paramref name="isChecking" /> for <see langword="true" />, esse método retornará true se o <see cref="T:System.Windows.Freezable" /> especificado puder se tornar não modificável ou false se ele não puder se tornar não modificável. Se <paramref name="isChecking" /> for false, este método retornará true se o <see cref="T:System.Windows.Freezable" /> especificado agora não for modificável ou false, se ele não puder se tornar não modificável, com o efeito colateral de ter feito a alteração real no status congelado para este objeto.</returns>
		// Token: 0x06003BF0 RID: 15344 RVA: 0x000EBA44 File Offset: 0x000EAE44
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
		// Token: 0x06003BF1 RID: 15345 RVA: 0x000EBA8C File Offset: 0x000EAE8C
		public IEnumerator GetEnumerator()
		{
			base.ReadPreamble();
			return this._keyFrames.GetEnumerator();
		}

		/// <summary>Obtém o número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.Point3DKeyFrameCollection" />.</summary>
		/// <returns>O número de quadros-chave contidos no <see cref="T:System.Windows.Media.Animation.Point3DKeyFrameCollection" />.</returns>
		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06003BF2 RID: 15346 RVA: 0x000EBAB0 File Offset: 0x000EAEB0
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
		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06003BF3 RID: 15347 RVA: 0x000EBAD0 File Offset: 0x000EAED0
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
		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06003BF4 RID: 15348 RVA: 0x000EBAF8 File Offset: 0x000EAEF8
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
		// Token: 0x06003BF5 RID: 15349 RVA: 0x000EBB18 File Offset: 0x000EAF18
		void ICollection.CopyTo(Array array, int index)
		{
			base.ReadPreamble();
			((ICollection)this._keyFrames).CopyTo(array, index);
		}

		/// <summary>Copia todos os objetos de <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" /> em uma coleção para uma matriz especificada.</summary>
		/// <param name="array">Identifica a matriz para a qual o conteúdo é copiado.</param>
		/// <param name="index">Posição de índice na matriz para a qual o conteúdo da coleção é copiado.</param>
		// Token: 0x06003BF6 RID: 15350 RVA: 0x000EBB38 File Offset: 0x000EAF38
		public void CopyTo(Point3DKeyFrame[] array, int index)
		{
			base.ReadPreamble();
			this._keyFrames.CopyTo(array, index);
		}

		/// <summary>Adiciona um item ao <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser adicionado ao <see cref="T:System.Collections.IList" />.</param>
		/// <returns>A posição na qual o novo elemento foi inserido, ou -1 para indicar que o item não foi inserido na coleção.</returns>
		// Token: 0x06003BF7 RID: 15351 RVA: 0x000EBB58 File Offset: 0x000EAF58
		int IList.Add(object keyFrame)
		{
			return this.Add((Point3DKeyFrame)keyFrame);
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" /> ao final da coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" /> a adicionar ao final da coleção.</param>
		/// <returns>O índice ao qual o <paramref name="keyFrame" /> foi adicionado.</returns>
		// Token: 0x06003BF8 RID: 15352 RVA: 0x000EBB74 File Offset: 0x000EAF74
		public int Add(Point3DKeyFrame keyFrame)
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

		/// <summary>Remove todos os objetos <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" /> da coleção.</summary>
		// Token: 0x06003BF9 RID: 15353 RVA: 0x000EBBBC File Offset: 0x000EAFBC
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
		// Token: 0x06003BFA RID: 15354 RVA: 0x000EBC18 File Offset: 0x000EB018
		bool IList.Contains(object keyFrame)
		{
			return this.Contains((Point3DKeyFrame)keyFrame);
		}

		/// <summary>Obtém um valor que indica se a coleção contém o <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" /> especificado.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>
		///   <see langword="true" /> se a coleção contiver <paramref name="keyFrame" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003BFB RID: 15355 RVA: 0x000EBC34 File Offset: 0x000EB034
		public bool Contains(Point3DKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.Contains(keyFrame);
		}

		/// <summary>Determina o índice de um item específico em <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="keyFrame">O objeto a ser localizado no <see cref="T:System.Collections.IList" />.</param>
		/// <returns>O índice de <paramref name="value" /> se encontrado na lista; caso contrário, -1.</returns>
		// Token: 0x06003BFC RID: 15356 RVA: 0x000EBC54 File Offset: 0x000EB054
		int IList.IndexOf(object keyFrame)
		{
			return this.IndexOf((Point3DKeyFrame)keyFrame);
		}

		/// <summary>Pesquisa pelo <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" /> especificado e retorna o índice de base zero da primeira ocorrência dentro de toda a coleção.</summary>
		/// <param name="keyFrame">O <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" /> a ser localizado na coleção.</param>
		/// <returns>O índice de base zero da primeira ocorrência de <paramref name="keyFrame" /> em toda a coleção, se encontrado, caso contrário, -1.</returns>
		// Token: 0x06003BFD RID: 15357 RVA: 0x000EBC70 File Offset: 0x000EB070
		public int IndexOf(Point3DKeyFrame keyFrame)
		{
			base.ReadPreamble();
			return this._keyFrames.IndexOf(keyFrame);
		}

		/// <summary>Insere um item na <see cref="T:System.Collections.IList" /> no índice especificado.</summary>
		/// <param name="index">O índice baseado em zero no qual o valor deve ser inserido.</param>
		/// <param name="keyFrame">O objeto a ser inserido no <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06003BFE RID: 15358 RVA: 0x000EBC90 File Offset: 0x000EB090
		void IList.Insert(int index, object keyFrame)
		{
			this.Insert(index, (Point3DKeyFrame)keyFrame);
		}

		/// <summary>Insere um <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" /> em um local específico na coleção.</summary>
		/// <param name="index">A posição de índice na qual o <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" /> é inserido.</param>
		/// <param name="keyFrame">O objeto <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" /> a ser inserido na coleção.</param>
		// Token: 0x06003BFF RID: 15359 RVA: 0x000EBCAC File Offset: 0x000EB0AC
		public void Insert(int index, Point3DKeyFrame keyFrame)
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
		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06003C00 RID: 15360 RVA: 0x000EBCE8 File Offset: 0x000EB0E8
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
		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06003C01 RID: 15361 RVA: 0x000EBD04 File Offset: 0x000EB104
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
		// Token: 0x06003C02 RID: 15362 RVA: 0x000EBD20 File Offset: 0x000EB120
		void IList.Remove(object keyFrame)
		{
			this.Remove((Point3DKeyFrame)keyFrame);
		}

		/// <summary>Remove um objeto <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" /> da coleção.</summary>
		/// <param name="keyFrame">Identifica o <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" /> a ser removido da coleção.</param>
		// Token: 0x06003C03 RID: 15363 RVA: 0x000EBD3C File Offset: 0x000EB13C
		public void Remove(Point3DKeyFrame keyFrame)
		{
			base.WritePreamble();
			if (this._keyFrames.Contains(keyFrame))
			{
				base.OnFreezablePropertyChanged(keyFrame, null);
				this._keyFrames.Remove(keyFrame);
				base.WritePostscript();
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" /> na posição de índice especificada da coleção.</summary>
		/// <param name="index">A posição de índice do <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" /> a ser removido.</param>
		// Token: 0x06003C04 RID: 15364 RVA: 0x000EBD78 File Offset: 0x000EB178
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
		// Token: 0x17000C0A RID: 3082
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (Point3DKeyFrame)value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" /> na posição de índice especificada.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" /> a ser obtido ou definido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" /> no índice especificado.</returns>
		/// <exception cref="T:System.InvalidOperationException">A tentativa de modificar a coleção é inválida porque a coleção está congelada (a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> dela é <see langword="true" />).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> é menor que zero.  
		///
		/// ou - 
		/// <paramref name="index" /> é igual a ou maior que <see cref="P:System.Windows.Media.Animation.Vector3DKeyFrameCollection.Count" />.</exception>
		// Token: 0x17000C0B RID: 3083
		public Point3DKeyFrame this[int index]
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
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "Point3DKeyFrameCollection[{0}]", new object[]
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

		// Token: 0x040016FD RID: 5885
		private List<Point3DKeyFrame> _keyFrames;

		// Token: 0x040016FE RID: 5886
		private static Point3DKeyFrameCollection s_emptyCollection;
	}
}
