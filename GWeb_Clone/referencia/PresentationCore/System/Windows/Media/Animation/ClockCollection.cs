using System;
using System.Collections;
using System.Collections.Generic;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma coleção ordenada de objetos de <see cref="T:System.Windows.Media.Animation.Clock" />.</summary>
	// Token: 0x02000580 RID: 1408
	public class ClockCollection : ICollection<Clock>, IEnumerable<Clock>, IEnumerable
	{
		/// <summary>Obtém o número de itens contidos neste <see cref="T:System.Windows.Media.Animation.ClockCollection" />.</summary>
		/// <returns>O número de itens contidos nesta instância.</returns>
		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06004121 RID: 16673 RVA: 0x00100470 File Offset: 0x000FF870
		public int Count
		{
			get
			{
				ClockGroup clockGroup = this._owner as ClockGroup;
				if (clockGroup != null)
				{
					List<Clock> internalChildren = clockGroup.InternalChildren;
					if (internalChildren != null)
					{
						return internalChildren.Count;
					}
				}
				return 0;
			}
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.Media.Animation.ClockCollection" /> é somente leitura.</summary>
		/// <returns>
		///   <see langword="true" /> Se esta instância for somente leitura; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06004122 RID: 16674 RVA: 0x001004A0 File Offset: 0x000FF8A0
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Remove todos os itens desta <see cref="T:System.Windows.Media.Animation.ClockCollection" />.</summary>
		// Token: 0x06004123 RID: 16675 RVA: 0x001004B0 File Offset: 0x000FF8B0
		public void Clear()
		{
			throw new NotSupportedException();
		}

		/// <summary>Adiciona um objeto <see cref="T:System.Windows.Media.Animation.Clock" /> ao final deste <see cref="T:System.Windows.Media.Animation.ClockCollection" />.</summary>
		/// <param name="item">O objeto a adicionar.</param>
		// Token: 0x06004124 RID: 16676 RVA: 0x001004C4 File Offset: 0x000FF8C4
		public void Add(Clock item)
		{
			throw new NotSupportedException();
		}

		/// <summary>Remove o <see cref="T:System.Windows.Media.Animation.Clock" /> especificado do <see cref="T:System.Windows.Media.Animation.ClockCollection" />.</summary>
		/// <param name="item">O objeto a ser removido.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="item" /> foi removido com êxito; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06004125 RID: 16677 RVA: 0x001004D8 File Offset: 0x000FF8D8
		public bool Remove(Clock item)
		{
			throw new NotSupportedException();
		}

		/// <summary>Indica se a <see cref="T:System.Windows.Media.Animation.ClockCollection" /> contém o objeto <see cref="T:System.Windows.Media.Animation.Clock" /> especificado.</summary>
		/// <param name="item">O objeto a ser localizado.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="item" /> for encontrado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06004126 RID: 16678 RVA: 0x001004EC File Offset: 0x000FF8EC
		public bool Contains(Clock item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			foreach (Clock clock in ((IEnumerable<Clock>)this))
			{
				if (clock.Equals(item))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copia os objetos <see cref="T:System.Windows.Media.Animation.Clock" /> deste <see cref="T:System.Windows.Media.Animation.ClockCollection" /> para uma matriz de Clocks, iniciando na posição de índice especificada.</summary>
		/// <param name="array">A matriz de destino.</param>
		/// <param name="index">A posição de índice baseado em zero em que a cópia é iniciada.</param>
		// Token: 0x06004127 RID: 16679 RVA: 0x00100558 File Offset: 0x000FF958
		public void CopyTo(Clock[] array, int index)
		{
			ClockGroup clockGroup = this._owner as ClockGroup;
			if (clockGroup != null)
			{
				List<Clock> internalChildren = clockGroup.InternalChildren;
				if (internalChildren != null)
				{
					internalChildren.CopyTo(array, index);
				}
			}
		}

		// Token: 0x06004128 RID: 16680 RVA: 0x00100588 File Offset: 0x000FF988
		IEnumerator<Clock> IEnumerable<Clock>.GetEnumerator()
		{
			List<Clock> list = null;
			ClockGroup clockGroup = this._owner as ClockGroup;
			if (clockGroup != null)
			{
				list = clockGroup.InternalChildren;
			}
			if (list != null)
			{
				return list.GetEnumerator();
			}
			return new ClockCollection.ClockEnumerator(this._owner);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
		/// <returns>Um objeto <see cref="T:System.Collections.IEnumerator" /> que pode ser usado para iterar pela coleção.</returns>
		// Token: 0x06004129 RID: 16681 RVA: 0x001005CC File Offset: 0x000FF9CC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ClockCollection.ClockEnumerator(this._owner);
		}

		/// <summary>Indica se essa instância é igual ao objeto especificado.</summary>
		/// <param name="obj">O objeto a ser comparado com essa instância.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="obj" /> for igual a essa instância; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600412A RID: 16682 RVA: 0x001005EC File Offset: 0x000FF9EC
		public override bool Equals(object obj)
		{
			return obj is ClockCollection && this == (ClockCollection)obj;
		}

		/// <summary>Indica se as duas coleções <see cref="T:System.Windows.Media.Animation.ClockCollection" /> especificadas são iguais.</summary>
		/// <param name="objA">O primeiro valor a ser comparado.</param>
		/// <param name="objB">O segundo valor a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="objA" /> e <paramref name="objB" /> forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600412B RID: 16683 RVA: 0x00100610 File Offset: 0x000FFA10
		public static bool Equals(ClockCollection objA, ClockCollection objB)
		{
			return objA == objB;
		}

		/// <summary>Operador sobrecarregado que compara duas coleções <see cref="T:System.Windows.Media.Animation.ClockCollection" /> quanto à igualdade.</summary>
		/// <param name="objA">O primeiro objeto a ser comparado.</param>
		/// <param name="objB">O segundo objeto a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="objA" /> e <paramref name="objB" /> forem iguais; caso contrário <see langword="false" />.</returns>
		// Token: 0x0600412C RID: 16684 RVA: 0x00100624 File Offset: 0x000FFA24
		public static bool operator ==(ClockCollection objA, ClockCollection objB)
		{
			return objA == objB || (objA != null && objB != null && objA._owner == objB._owner);
		}

		/// <summary>Operador sobrecarregado que compara duas coleções <see cref="T:System.Windows.Media.Animation.ClockCollection" /> quanto à desigualdade.</summary>
		/// <param name="objA">O primeiro objeto a ser comparado.</param>
		/// <param name="objB">O segundo objeto a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="objA" /> e <paramref name="objB" /> não forem iguais; caso contrário <see langword="false" />.</returns>
		// Token: 0x0600412D RID: 16685 RVA: 0x00100650 File Offset: 0x000FFA50
		public static bool operator !=(ClockCollection objA, ClockCollection objB)
		{
			return !(objA == objB);
		}

		/// <summary>Retorna um código hash de inteiro com sinal de 32 bits que representa esta instância.</summary>
		/// <returns>Um inteiro com sinal de 32 bits.</returns>
		// Token: 0x0600412E RID: 16686 RVA: 0x00100668 File Offset: 0x000FFA68
		public override int GetHashCode()
		{
			return this._owner.GetHashCode();
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Media.Animation.Clock" /> na posição de índice especificada.</summary>
		/// <param name="index">A posição de índice a acessar.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.Clock" /> objeto no local especificado <paramref name="index" /> posição.</returns>
		// Token: 0x17000D18 RID: 3352
		public Clock this[int index]
		{
			get
			{
				List<Clock> list = null;
				ClockGroup clockGroup = this._owner as ClockGroup;
				if (clockGroup != null)
				{
					list = clockGroup.InternalChildren;
				}
				if (list == null)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return list[index];
			}
		}

		// Token: 0x06004130 RID: 16688 RVA: 0x001006BC File Offset: 0x000FFABC
		internal ClockCollection(Clock owner)
		{
			this._owner = owner;
		}

		// Token: 0x06004131 RID: 16689 RVA: 0x001006D8 File Offset: 0x000FFAD8
		private ClockCollection()
		{
		}

		// Token: 0x040017D5 RID: 6101
		private Clock _owner;

		// Token: 0x020008CC RID: 2252
		internal struct ClockEnumerator : IEnumerator<Clock>, IDisposable, IEnumerator
		{
			// Token: 0x060058C2 RID: 22722 RVA: 0x00168834 File Offset: 0x00167C34
			internal ClockEnumerator(Clock owner)
			{
				this._owner = owner;
			}

			// Token: 0x060058C3 RID: 22723 RVA: 0x00168848 File Offset: 0x00167C48
			public void Dispose()
			{
			}

			// Token: 0x17001258 RID: 4696
			// (get) Token: 0x060058C4 RID: 22724 RVA: 0x00168858 File Offset: 0x00167C58
			Clock IEnumerator<Clock>.Current
			{
				get
				{
					throw new InvalidOperationException(SR.Get("Timing_EnumeratorOutOfRange"));
				}
			}

			// Token: 0x17001259 RID: 4697
			// (get) Token: 0x060058C5 RID: 22725 RVA: 0x00168874 File Offset: 0x00167C74
			object IEnumerator.Current
			{
				get
				{
					return ((IEnumerator<Clock>)this).Current;
				}
			}

			// Token: 0x060058C6 RID: 22726 RVA: 0x00168894 File Offset: 0x00167C94
			void IEnumerator.Reset()
			{
				throw new NotImplementedException();
			}

			// Token: 0x060058C7 RID: 22727 RVA: 0x001688A8 File Offset: 0x00167CA8
			public bool MoveNext()
			{
				ClockGroup clockGroup = this._owner as ClockGroup;
				if (clockGroup != null && clockGroup.InternalChildren != null)
				{
					throw new InvalidOperationException(SR.Get("Timing_EnumeratorInvalidated"));
				}
				return false;
			}

			// Token: 0x04002976 RID: 10614
			private Clock _owner;
		}
	}
}
