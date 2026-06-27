using System;
using System.ComponentModel;

namespace System.Windows
{
	// Token: 0x0200019E RID: 414
	internal class HwndDpiChangedAfterParentEventArgs : HandledEventArgs
	{
		// Token: 0x06000614 RID: 1556 RVA: 0x0001C8E4 File Offset: 0x0001BCE4
		internal HwndDpiChangedAfterParentEventArgs(DpiScale oldDpi, DpiScale newDpi, Rect suggestedRect) : base(false)
		{
			this.OldDpi = oldDpi;
			this.NewDpi = newDpi;
			this.SuggestedRect = suggestedRect;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001C910 File Offset: 0x0001BD10
		internal HwndDpiChangedAfterParentEventArgs(HwndDpiChangedEventArgs e) : this(e.OldDpi, e.NewDpi, e.SuggestedRect)
		{
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0001C938 File Offset: 0x0001BD38
		internal DpiScale OldDpi { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0001C94C File Offset: 0x0001BD4C
		internal DpiScale NewDpi { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0001C960 File Offset: 0x0001BD60
		internal Rect SuggestedRect { get; }

		// Token: 0x06000619 RID: 1561 RVA: 0x0001C974 File Offset: 0x0001BD74
		public static explicit operator HwndDpiChangedEventArgs(HwndDpiChangedAfterParentEventArgs e)
		{
			return new HwndDpiChangedEventArgs(e.OldDpi, e.NewDpi, e.SuggestedRect);
		}
	}
}
