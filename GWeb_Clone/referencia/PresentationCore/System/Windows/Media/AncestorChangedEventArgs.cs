using System;

namespace System.Windows.Media
{
	// Token: 0x02000362 RID: 866
	internal class AncestorChangedEventArgs
	{
		// Token: 0x06001D7C RID: 7548 RVA: 0x00078C10 File Offset: 0x00078010
		public AncestorChangedEventArgs(DependencyObject subRoot, DependencyObject oldParent)
		{
			this._subRoot = subRoot;
			this._oldParent = oldParent;
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001D7D RID: 7549 RVA: 0x00078C34 File Offset: 0x00078034
		public DependencyObject Ancestor
		{
			get
			{
				return this._subRoot;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x00078C48 File Offset: 0x00078048
		public DependencyObject OldParent
		{
			get
			{
				return this._oldParent;
			}
		}

		// Token: 0x04000FD9 RID: 4057
		private DependencyObject _subRoot;

		// Token: 0x04000FDA RID: 4058
		private DependencyObject _oldParent;
	}
}
