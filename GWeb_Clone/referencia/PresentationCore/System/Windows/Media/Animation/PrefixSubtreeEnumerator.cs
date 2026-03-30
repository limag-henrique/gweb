using System;

namespace System.Windows.Media.Animation
{
	// Token: 0x0200057C RID: 1404
	internal struct PrefixSubtreeEnumerator
	{
		// Token: 0x060040DF RID: 16607 RVA: 0x000FE5D8 File Offset: 0x000FD9D8
		internal PrefixSubtreeEnumerator(Clock root, bool processRoot)
		{
			this._rootClock = root;
			this._currentClock = null;
			this._flags = (processRoot ? (SubtreeFlag.Reset | SubtreeFlag.ProcessRoot) : SubtreeFlag.Reset);
		}

		// Token: 0x060040E0 RID: 16608 RVA: 0x000FE600 File Offset: 0x000FDA00
		internal void SkipSubtree()
		{
			this._flags |= SubtreeFlag.SkipSubtree;
		}

		// Token: 0x060040E1 RID: 16609 RVA: 0x000FE61C File Offset: 0x000FDA1C
		public bool MoveNext()
		{
			if ((this._flags & SubtreeFlag.Reset) != (SubtreeFlag)0)
			{
				if ((this._flags & SubtreeFlag.ProcessRoot) != (SubtreeFlag)0)
				{
					this._currentClock = this._rootClock;
				}
				else if (this._rootClock != null)
				{
					ClockGroup clockGroup = this._rootClock as ClockGroup;
					if (clockGroup != null)
					{
						this._currentClock = clockGroup.FirstChild;
					}
					else
					{
						this._currentClock = null;
					}
				}
				this._flags &= ~SubtreeFlag.Reset;
			}
			else if (this._currentClock != null)
			{
				ClockGroup clockGroup2 = this._currentClock as ClockGroup;
				Clock clock = (clockGroup2 == null) ? null : clockGroup2.FirstChild;
				if ((this._flags & SubtreeFlag.SkipSubtree) == (SubtreeFlag)0)
				{
					if (clock != null)
					{
						goto IL_E2;
					}
				}
				while (this._currentClock != this._rootClock && (clock = this._currentClock.NextSibling) == null)
				{
					this._currentClock = this._currentClock.InternalParent;
				}
				if (this._currentClock == this._rootClock)
				{
					clock = null;
				}
				this._flags &= ~SubtreeFlag.SkipSubtree;
				IL_E2:
				this._currentClock = clock;
			}
			return this._currentClock != null;
		}

		// Token: 0x060040E2 RID: 16610 RVA: 0x000FE71C File Offset: 0x000FDB1C
		public void Reset()
		{
			this._currentClock = null;
			this._flags = ((this._flags & SubtreeFlag.ProcessRoot) | SubtreeFlag.Reset);
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x060040E3 RID: 16611 RVA: 0x000FE740 File Offset: 0x000FDB40
		internal Clock Current
		{
			get
			{
				return this._currentClock;
			}
		}

		// Token: 0x040017C3 RID: 6083
		private Clock _rootClock;

		// Token: 0x040017C4 RID: 6084
		private Clock _currentClock;

		// Token: 0x040017C5 RID: 6085
		private SubtreeFlag _flags;
	}
}
