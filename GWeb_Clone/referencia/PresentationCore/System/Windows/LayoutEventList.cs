using System;

namespace System.Windows
{
	// Token: 0x020001C8 RID: 456
	internal class LayoutEventList
	{
		// Token: 0x06000C2A RID: 3114 RVA: 0x0002E9A8 File Offset: 0x0002DDA8
		internal LayoutEventList()
		{
			for (int i = 0; i < 153; i++)
			{
				this._pocket = new LayoutEventList.ListItem
				{
					Next = this._pocket
				};
			}
			this._pocketSize = 153;
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0002E9F0 File Offset: 0x0002DDF0
		internal LayoutEventList.ListItem Add(object target)
		{
			LayoutEventList.ListItem newListItem = this.getNewListItem(target);
			newListItem.Next = this._head;
			if (this._head != null)
			{
				this._head.Prev = newListItem;
			}
			this._head = newListItem;
			this._count++;
			return newListItem;
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0002EA3C File Offset: 0x0002DE3C
		internal void Remove(LayoutEventList.ListItem t)
		{
			if (!t.InUse)
			{
				return;
			}
			if (t.Prev == null)
			{
				this._head = t.Next;
			}
			else
			{
				t.Prev.Next = t.Next;
			}
			if (t.Next != null)
			{
				t.Next.Prev = t.Prev;
			}
			this.reuseListItem(t);
			this._count--;
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0002EAA8 File Offset: 0x0002DEA8
		private LayoutEventList.ListItem getNewListItem(object target)
		{
			LayoutEventList.ListItem listItem;
			if (this._pocket != null)
			{
				listItem = this._pocket;
				this._pocket = listItem.Next;
				this._pocketSize--;
				listItem.Next = (listItem.Prev = null);
			}
			else
			{
				listItem = new LayoutEventList.ListItem();
			}
			listItem.Target = target;
			listItem.InUse = true;
			return listItem;
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0002EB08 File Offset: 0x0002DF08
		private void reuseListItem(LayoutEventList.ListItem t)
		{
			t.Target = null;
			t.Next = (t.Prev = null);
			t.InUse = false;
			if (this._pocketSize < 153)
			{
				t.Next = this._pocket;
				this._pocket = t;
				this._pocketSize++;
			}
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0002EB64 File Offset: 0x0002DF64
		internal LayoutEventList.ListItem[] CopyToArray()
		{
			LayoutEventList.ListItem[] array = new LayoutEventList.ListItem[this._count];
			LayoutEventList.ListItem listItem = this._head;
			int num = 0;
			while (listItem != null)
			{
				array[num++] = listItem;
				listItem = listItem.Next;
			}
			return array;
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0002EB9C File Offset: 0x0002DF9C
		internal int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x04000700 RID: 1792
		private const int PocketCapacity = 153;

		// Token: 0x04000701 RID: 1793
		private LayoutEventList.ListItem _head;

		// Token: 0x04000702 RID: 1794
		private LayoutEventList.ListItem _pocket;

		// Token: 0x04000703 RID: 1795
		private int _pocketSize;

		// Token: 0x04000704 RID: 1796
		private int _count;

		// Token: 0x020007FE RID: 2046
		internal class ListItem : WeakReference
		{
			// Token: 0x060055E3 RID: 21987 RVA: 0x001617B8 File Offset: 0x00160BB8
			internal ListItem() : base(null)
			{
			}

			// Token: 0x0400269F RID: 9887
			internal LayoutEventList.ListItem Next;

			// Token: 0x040026A0 RID: 9888
			internal LayoutEventList.ListItem Prev;

			// Token: 0x040026A1 RID: 9889
			internal bool InUse;
		}
	}
}
