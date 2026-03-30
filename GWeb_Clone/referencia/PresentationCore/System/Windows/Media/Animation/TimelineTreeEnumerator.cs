using System;
using System.Collections;
using System.Collections.Generic;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	// Token: 0x0200057E RID: 1406
	[FriendAccessAllowed]
	internal struct TimelineTreeEnumerator
	{
		// Token: 0x060040E7 RID: 16615 RVA: 0x000FE868 File Offset: 0x000FDC68
		internal TimelineTreeEnumerator(Timeline root, bool processRoot)
		{
			this._rootTimeline = root;
			this._flags = (processRoot ? (SubtreeFlag.Reset | SubtreeFlag.ProcessRoot) : SubtreeFlag.Reset);
			this._indexStack = new Stack(9);
			this._timelineStack = new Stack<Timeline>(10);
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x000FE8A4 File Offset: 0x000FDCA4
		internal void SkipSubtree()
		{
			this._flags |= SubtreeFlag.SkipSubtree;
		}

		// Token: 0x060040E9 RID: 16617 RVA: 0x000FE8C0 File Offset: 0x000FDCC0
		public bool MoveNext()
		{
			if ((this._flags & SubtreeFlag.Reset) != (SubtreeFlag)0)
			{
				this._flags &= ~SubtreeFlag.Reset;
				this._timelineStack.Push(this._rootTimeline);
				if ((this._flags & SubtreeFlag.ProcessRoot) == (SubtreeFlag)0)
				{
					this.MoveNext();
				}
			}
			else if (this._timelineStack.Count > 0)
			{
				TimelineGroup timelineGroup = this._timelineStack.Peek() as TimelineGroup;
				TimelineCollection children;
				if ((this._flags & SubtreeFlag.SkipSubtree) == (SubtreeFlag)0 && timelineGroup != null && (children = timelineGroup.Children) != null && children.Count > 0)
				{
					this._timelineStack.Push(children[0]);
					this._indexStack.Push(0);
				}
				else
				{
					this._flags &= ~SubtreeFlag.SkipSubtree;
					this._timelineStack.Pop();
					while (this._timelineStack.Count > 0)
					{
						timelineGroup = (this._timelineStack.Peek() as TimelineGroup);
						children = timelineGroup.Children;
						int num = (int)this._indexStack.Pop() + 1;
						if (num < children.Count)
						{
							this._timelineStack.Push(children[num]);
							this._indexStack.Push(num);
							break;
						}
						this._timelineStack.Pop();
					}
				}
			}
			return this._timelineStack.Count > 0;
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x060040EA RID: 16618 RVA: 0x000FEA18 File Offset: 0x000FDE18
		internal Timeline Current
		{
			get
			{
				return this._timelineStack.Peek();
			}
		}

		// Token: 0x040017C9 RID: 6089
		private Timeline _rootTimeline;

		// Token: 0x040017CA RID: 6090
		private SubtreeFlag _flags;

		// Token: 0x040017CB RID: 6091
		private Stack _indexStack;

		// Token: 0x040017CC RID: 6092
		private Stack<Timeline> _timelineStack;
	}
}
