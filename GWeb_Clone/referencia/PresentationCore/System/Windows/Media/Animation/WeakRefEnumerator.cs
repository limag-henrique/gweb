using System;
using System.Collections.Generic;

namespace System.Windows.Media.Animation
{
	// Token: 0x02000582 RID: 1410
	internal struct WeakRefEnumerator<T>
	{
		// Token: 0x06004154 RID: 16724 RVA: 0x00100E18 File Offset: 0x00100218
		internal WeakRefEnumerator(List<WeakReference> list)
		{
			this._list = list;
			this._readIndex = 0;
			this._writeIndex = 0;
			this._current = default(T);
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06004155 RID: 16725 RVA: 0x00100E48 File Offset: 0x00100248
		internal T Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06004156 RID: 16726 RVA: 0x00100E5C File Offset: 0x0010025C
		internal int CurrentIndex
		{
			get
			{
				return this._writeIndex - 1;
			}
		}

		// Token: 0x06004157 RID: 16727 RVA: 0x00100E74 File Offset: 0x00100274
		internal void Dispose()
		{
			if (this._readIndex != this._writeIndex)
			{
				this._list.RemoveRange(this._writeIndex, this._readIndex - this._writeIndex);
				this._readIndex = (this._writeIndex = this._list.Count);
			}
			this._current = default(T);
		}

		// Token: 0x06004158 RID: 16728 RVA: 0x00100ED4 File Offset: 0x001002D4
		internal bool MoveNext()
		{
			while (this._readIndex < this._list.Count)
			{
				WeakReference weakReference = this._list[this._readIndex];
				this._current = (T)((object)weakReference.Target);
				if (this._current != null)
				{
					if (this._writeIndex != this._readIndex)
					{
						this._list[this._writeIndex] = weakReference;
					}
					this._readIndex++;
					this._writeIndex++;
					return true;
				}
				this._readIndex++;
			}
			this.Dispose();
			return false;
		}

		// Token: 0x040017E6 RID: 6118
		private List<WeakReference> _list;

		// Token: 0x040017E7 RID: 6119
		private T _current;

		// Token: 0x040017E8 RID: 6120
		private int _readIndex;

		// Token: 0x040017E9 RID: 6121
		private int _writeIndex;
	}
}
