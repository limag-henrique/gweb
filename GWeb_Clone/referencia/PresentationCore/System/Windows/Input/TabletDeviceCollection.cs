using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Contém os objetos <see cref="T:System.Windows.Input.TabletDevice" /> que representam os dispositivos digitalizadores de um tablet.</summary>
	// Token: 0x020002CB RID: 715
	public class TabletDeviceCollection : ICollection, IEnumerable
	{
		// Token: 0x06001582 RID: 5506 RVA: 0x0004FB44 File Offset: 0x0004EF44
		internal T As<T>() where T : TabletDeviceCollection
		{
			return this as T;
		}

		/// <summary>Obtém o número de objetos <see cref="T:System.Windows.Input.TabletDevice" /> na coleção.</summary>
		/// <returns>O número de <see cref="T:System.Windows.Input.TabletDevice" /> objetos na coleção.</returns>
		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x0004FB5C File Offset: 0x0004EF5C
		public int Count
		{
			get
			{
				if (this.TabletDevices == null)
				{
					throw new ObjectDisposedException("TabletDeviceCollection");
				}
				return this.TabletDevices.Count;
			}
		}

		/// <summary>Esse membro dá suporte ao .NET Framework e não destina-se a ser usado do seu código.</summary>
		/// <param name="array">A matriz.</param>
		/// <param name="index">O índice.</param>
		// Token: 0x06001584 RID: 5508 RVA: 0x0004FB88 File Offset: 0x0004EF88
		void ICollection.CopyTo(Array array, int index)
		{
			Array.Copy(this.TabletDevices.ToArray(), 0, array, index, this.Count);
		}

		/// <summary>Copia todos os elementos na coleção atual para a matriz unidimensional especificada, começando no índice da matriz de destino especificada.</summary>
		/// <param name="array">A matriz unidimensional que é o destino dos elementos copiados da coleção. A matriz deve ter indexação com base em zero.</param>
		/// <param name="index">O índice de base zero no parâmetro de matriz em que a cópia é iniciada.</param>
		/// <exception cref="T:System.ArgumentException">Se <paramref name="index" /> + contagem de coleções for maior ou igual a <paramref name="array.length" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">Se <paramref name="array" /> for <see langword="null" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Se <paramref name="index" /> for menor que 0 ou <paramref name="index" /> for maior ou igual a <paramref name="array.length" />.</exception>
		// Token: 0x06001585 RID: 5509 RVA: 0x0004FBB0 File Offset: 0x0004EFB0
		public void CopyTo(TabletDevice[] array, int index)
		{
			this.TabletDevices.CopyTo(array, index);
		}

		/// <summary>Obtém o objeto <see cref="T:System.Windows.Input.TabletDevice" /> no índice especificado dentro da coleção.</summary>
		/// <param name="index">O índice de base zero do <see cref="T:System.Windows.Input.TabletDevice" /> recuperado da coleção.</param>
		/// <returns>O <see cref="T:System.Windows.Input.TabletDevice" /> objeto no índice especificado dentro da coleção.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">Se <paramref name="index" /> for menor que 0 ou <paramref name="index" /> for maior ou igual ao número de objetos <see cref="T:System.Windows.Input.TabletDeviceCollection" /> na coleção.</exception>
		// Token: 0x170003EF RID: 1007
		public TabletDevice this[int index]
		{
			get
			{
				if (index >= this.Count || index < 0)
				{
					throw new ArgumentException(SR.Get("Stylus_IndexOutOfRange", new object[]
					{
						index.ToString(CultureInfo.InvariantCulture)
					}), "index");
				}
				return this.TabletDevices[index];
			}
		}

		/// <summary>Obtém um objeto que pode ser usado para sincronizar o acesso à coleção.</summary>
		/// <returns>Um objeto que pode ser usado para sincronizar o acesso à coleção.</returns>
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x0004FC1C File Offset: 0x0004F01C
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Obtém um valor que indica se o acesso à coleção é sincronizado (thread-safe).</summary>
		/// <returns>
		///   <see langword="true" /> Se o acesso à coleção for sincronizado (thread-safe); Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x0004FC2C File Offset: 0x0004F02C
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Esse membro dá suporte ao .NET Framework e não destina-se a ser usado do seu código.</summary>
		// Token: 0x06001589 RID: 5513 RVA: 0x0004FC3C File Offset: 0x0004F03C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.TabletDevices.GetEnumerator();
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x0004FC5C File Offset: 0x0004F05C
		// (set) Token: 0x0600158B RID: 5515 RVA: 0x0004FC70 File Offset: 0x0004F070
		internal List<TabletDevice> TabletDevices { get; set; } = new List<TabletDevice>();

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x0004FC84 File Offset: 0x0004F084
		internal static TabletDeviceCollection EmptyTabletDeviceCollection
		{
			get
			{
				if (TabletDeviceCollection._emptyTabletDeviceCollection == null)
				{
					TabletDeviceCollection._emptyTabletDeviceCollection = new TabletDeviceCollection();
				}
				return TabletDeviceCollection._emptyTabletDeviceCollection;
			}
		}

		// Token: 0x04000BAB RID: 2987
		private static TabletDeviceCollection _emptyTabletDeviceCollection;
	}
}
