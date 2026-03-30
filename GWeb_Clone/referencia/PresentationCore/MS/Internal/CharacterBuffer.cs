using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace MS.Internal
{
	// Token: 0x02000672 RID: 1650
	internal abstract class CharacterBuffer : IList<char>, ICollection<char>, IEnumerable<char>, IEnumerable
	{
		// Token: 0x060048E6 RID: 18662
		[SecurityCritical]
		public unsafe abstract char* GetCharacterPointer();

		// Token: 0x060048E7 RID: 18663
		public abstract IntPtr PinAndGetCharacterPointer(int offset, out GCHandle gcHandle);

		// Token: 0x060048E8 RID: 18664
		public abstract void UnpinCharacterPointer(GCHandle gcHandle);

		// Token: 0x060048E9 RID: 18665
		public abstract void AppendToStringBuilder(StringBuilder stringBuilder, int characterOffset, int length);

		// Token: 0x060048EA RID: 18666 RVA: 0x0011CE78 File Offset: 0x0011C278
		public int IndexOf(char item)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (item == this[i])
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060048EB RID: 18667 RVA: 0x0011CEA4 File Offset: 0x0011C2A4
		public void Insert(int index, char item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000F18 RID: 3864
		public abstract char this[int index]
		{
			get;
			set;
		}

		// Token: 0x060048EE RID: 18670 RVA: 0x0011CEB8 File Offset: 0x0011C2B8
		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060048EF RID: 18671 RVA: 0x0011CECC File Offset: 0x0011C2CC
		public void Add(char item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060048F0 RID: 18672 RVA: 0x0011CEE0 File Offset: 0x0011C2E0
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060048F1 RID: 18673 RVA: 0x0011CEF4 File Offset: 0x0011C2F4
		public bool Contains(char item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x060048F2 RID: 18674 RVA: 0x0011CF10 File Offset: 0x0011C310
		public void CopyTo(char[] array, int arrayIndex)
		{
			for (int i = 0; i < this.Count; i++)
			{
				array[arrayIndex + i] = this[i];
			}
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x060048F3 RID: 18675
		public abstract int Count { get; }

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x060048F4 RID: 18676 RVA: 0x0011CF3C File Offset: 0x0011C33C
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060048F5 RID: 18677 RVA: 0x0011CF4C File Offset: 0x0011C34C
		public bool Remove(char item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060048F6 RID: 18678 RVA: 0x0011CF60 File Offset: 0x0011C360
		IEnumerator<char> IEnumerable<char>.GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.Count; i = num)
			{
				yield return this[i];
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x060048F7 RID: 18679 RVA: 0x0011CF7C File Offset: 0x0011C37C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<char>)this).GetEnumerator();
		}
	}
}
