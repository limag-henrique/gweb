using System;
using System.Collections;
using System.Collections.Generic;
using MS.Internal.PresentationCore;

namespace MS.Internal
{
	// Token: 0x0200067F RID: 1663
	internal class SequentialUshortCollection : ICollection<ushort>, IEnumerable<ushort>, IEnumerable
	{
		// Token: 0x06004959 RID: 18777 RVA: 0x0011E28C File Offset: 0x0011D68C
		public SequentialUshortCollection(ushort count)
		{
			this._count = count;
		}

		// Token: 0x0600495A RID: 18778 RVA: 0x0011E2A8 File Offset: 0x0011D6A8
		public void Add(ushort item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600495B RID: 18779 RVA: 0x0011E2BC File Offset: 0x0011D6BC
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600495C RID: 18780 RVA: 0x0011E2D0 File Offset: 0x0011D6D0
		public bool Contains(ushort item)
		{
			return item < this._count;
		}

		// Token: 0x0600495D RID: 18781 RVA: 0x0011E2E8 File Offset: 0x0011D6E8
		public void CopyTo(ushort[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(SR.Get("Collection_BadRank"));
			}
			if (arrayIndex < 0 || arrayIndex >= array.Length || arrayIndex + this.Count > array.Length)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			for (ushort num = 0; num < this._count; num += 1)
			{
				array[arrayIndex + (int)num] = num;
			}
		}

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x0600495E RID: 18782 RVA: 0x0011E358 File Offset: 0x0011D758
		public int Count
		{
			get
			{
				return (int)this._count;
			}
		}

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x0600495F RID: 18783 RVA: 0x0011E36C File Offset: 0x0011D76C
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004960 RID: 18784 RVA: 0x0011E37C File Offset: 0x0011D77C
		public bool Remove(ushort item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004961 RID: 18785 RVA: 0x0011E390 File Offset: 0x0011D790
		public IEnumerator<ushort> GetEnumerator()
		{
			ushort num;
			for (ushort i = 0; i < this._count; i = num)
			{
				yield return i;
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x06004962 RID: 18786 RVA: 0x0011E3AC File Offset: 0x0011D7AC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<ushort>)this).GetEnumerator();
		}

		// Token: 0x04001CD7 RID: 7383
		private ushort _count;
	}
}
