using System;

namespace System.Windows.Media.Animation
{
	// Token: 0x0200057D RID: 1405
	internal struct PostfixSubtreeEnumerator
	{
		// Token: 0x060040E4 RID: 16612 RVA: 0x000FE754 File Offset: 0x000FDB54
		internal PostfixSubtreeEnumerator(Clock root, bool processRoot)
		{
			this._rootClock = root;
			this._currentClock = null;
			this._flags = (processRoot ? (SubtreeFlag.Reset | SubtreeFlag.ProcessRoot) : SubtreeFlag.Reset);
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x000FE77C File Offset: 0x000FDB7C
		public bool MoveNext()
		{
			if ((this._flags & SubtreeFlag.Reset) != (SubtreeFlag)0)
			{
				this._currentClock = this._rootClock;
				this._flags &= ~SubtreeFlag.Reset;
			}
			else if (this._currentClock == this._rootClock)
			{
				this._currentClock = null;
			}
			if (this._currentClock != null)
			{
				Clock currentClock = this._currentClock;
				if (this._currentClock != this._rootClock && (currentClock = this._currentClock.NextSibling) == null)
				{
					this._currentClock = this._currentClock.InternalParent;
				}
				else
				{
					do
					{
						this._currentClock = currentClock;
						ClockGroup clockGroup = this._currentClock as ClockGroup;
					}
					while ((currentClock = this._currentClock.FirstChild) != null);
				}
				if (this._currentClock == this._rootClock && (this._flags & SubtreeFlag.ProcessRoot) == (SubtreeFlag)0)
				{
					this._currentClock = null;
				}
			}
			return this._currentClock != null;
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x060040E6 RID: 16614 RVA: 0x000FE854 File Offset: 0x000FDC54
		internal Clock Current
		{
			get
			{
				return this._currentClock;
			}
		}

		// Token: 0x040017C6 RID: 6086
		private Clock _rootClock;

		// Token: 0x040017C7 RID: 6087
		private Clock _currentClock;

		// Token: 0x040017C8 RID: 6088
		private SubtreeFlag _flags;
	}
}
