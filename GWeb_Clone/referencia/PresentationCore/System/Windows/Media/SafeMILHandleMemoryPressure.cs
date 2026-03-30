using System;
using System.Security;
using System.Threading;

namespace System.Windows.Media
{
	// Token: 0x0200043B RID: 1083
	internal class SafeMILHandleMemoryPressure
	{
		// Token: 0x06002C3F RID: 11327 RVA: 0x000B0BFC File Offset: 0x000AFFFC
		[SecurityCritical]
		internal SafeMILHandleMemoryPressure(long gcPressure)
		{
			this._gcPressure = gcPressure;
			this._refCount = 0;
			GC.AddMemoryPressure(this._gcPressure);
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x000B0C28 File Offset: 0x000B0028
		internal void AddRef()
		{
			Interlocked.Increment(ref this._refCount);
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x000B0C44 File Offset: 0x000B0044
		[SecurityCritical]
		internal void Release()
		{
			if (Interlocked.Decrement(ref this._refCount) == 0)
			{
				GC.RemoveMemoryPressure(this._gcPressure);
				this._gcPressure = 0L;
			}
		}

		// Token: 0x04001427 RID: 5159
		private long _gcPressure;

		// Token: 0x04001428 RID: 5160
		private int _refCount;
	}
}
