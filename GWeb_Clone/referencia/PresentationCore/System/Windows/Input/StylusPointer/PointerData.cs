using System;
using System.Security;
using MS.Win32.Pointer;

namespace System.Windows.Input.StylusPointer
{
	// Token: 0x020002E8 RID: 744
	internal class PointerData
	{
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x0005AAF4 File Offset: 0x00059EF4
		// (set) Token: 0x0600172C RID: 5932 RVA: 0x0005AB08 File Offset: 0x00059F08
		internal bool IsValid { get; private set; }

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x0005AB1C File Offset: 0x00059F1C
		internal UnsafeNativeMethods.POINTER_INFO Info
		{
			get
			{
				return this._info;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x0005AB30 File Offset: 0x00059F30
		internal UnsafeNativeMethods.POINTER_TOUCH_INFO TouchInfo
		{
			get
			{
				return this._touchInfo;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x0005AB44 File Offset: 0x00059F44
		internal UnsafeNativeMethods.POINTER_PEN_INFO PenInfo
		{
			get
			{
				return this._penInfo;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x0005AB58 File Offset: 0x00059F58
		internal UnsafeNativeMethods.POINTER_INFO[] History
		{
			get
			{
				return this._history;
			}
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0005AB6C File Offset: 0x00059F6C
		[SecuritySafeCritical]
		internal PointerData(uint pointerId)
		{
			if (this.IsValid = UnsafeNativeMethods.GetPointerInfo(pointerId, ref this._info))
			{
				this._history = new UnsafeNativeMethods.POINTER_INFO[this._info.historyCount];
				if (!UnsafeNativeMethods.GetPointerInfoHistory(pointerId, ref this._info.historyCount, this._history))
				{
					this._history = new UnsafeNativeMethods.POINTER_INFO[0];
				}
				UnsafeNativeMethods.POINTER_INPUT_TYPE pointerType = this._info.pointerType;
				if (pointerType == UnsafeNativeMethods.POINTER_INPUT_TYPE.PT_TOUCH)
				{
					this.IsValid &= UnsafeNativeMethods.GetPointerTouchInfo(pointerId, ref this._touchInfo);
					return;
				}
				if (pointerType == UnsafeNativeMethods.POINTER_INPUT_TYPE.PT_PEN)
				{
					this.IsValid &= UnsafeNativeMethods.GetPointerPenInfo(pointerId, ref this._penInfo);
					return;
				}
				this.IsValid = false;
			}
		}

		// Token: 0x04000C95 RID: 3221
		private UnsafeNativeMethods.POINTER_INFO _info;

		// Token: 0x04000C96 RID: 3222
		private UnsafeNativeMethods.POINTER_TOUCH_INFO _touchInfo;

		// Token: 0x04000C97 RID: 3223
		private UnsafeNativeMethods.POINTER_PEN_INFO _penInfo;

		// Token: 0x04000C98 RID: 3224
		private UnsafeNativeMethods.POINTER_INFO[] _history;
	}
}
