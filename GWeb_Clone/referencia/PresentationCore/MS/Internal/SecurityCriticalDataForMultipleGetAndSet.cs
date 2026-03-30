using System;
using System.Security;
using MS.Internal.PresentationCore;

namespace MS.Internal
{
	// Token: 0x02000670 RID: 1648
	[FriendAccessAllowed]
	internal class SecurityCriticalDataForMultipleGetAndSet<T>
	{
		// Token: 0x060048B7 RID: 18615 RVA: 0x0011C4B4 File Offset: 0x0011B8B4
		[SecurityCritical]
		internal SecurityCriticalDataForMultipleGetAndSet(T value)
		{
			this._value = value;
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x060048B8 RID: 18616 RVA: 0x0011C4D0 File Offset: 0x0011B8D0
		// (set) Token: 0x060048B9 RID: 18617 RVA: 0x0011C4E4 File Offset: 0x0011B8E4
		internal T Value
		{
			[SecurityCritical]
			get
			{
				return this._value;
			}
			[SecurityCritical]
			set
			{
				this._value = value;
			}
		}

		// Token: 0x04001CA0 RID: 7328
		[SecurityCritical]
		private T _value;
	}
}
