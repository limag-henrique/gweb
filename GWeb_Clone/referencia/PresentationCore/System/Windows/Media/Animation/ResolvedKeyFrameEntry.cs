using System;

namespace System.Windows.Media.Animation
{
	// Token: 0x02000579 RID: 1401
	internal struct ResolvedKeyFrameEntry : IComparable
	{
		// Token: 0x060040DE RID: 16606 RVA: 0x000FE578 File Offset: 0x000FD978
		public int CompareTo(object other)
		{
			ResolvedKeyFrameEntry resolvedKeyFrameEntry = (ResolvedKeyFrameEntry)other;
			if (resolvedKeyFrameEntry._resolvedKeyTime > this._resolvedKeyTime)
			{
				return -1;
			}
			if (resolvedKeyFrameEntry._resolvedKeyTime < this._resolvedKeyTime)
			{
				return 1;
			}
			if (resolvedKeyFrameEntry._originalKeyFrameIndex > this._originalKeyFrameIndex)
			{
				return -1;
			}
			if (resolvedKeyFrameEntry._originalKeyFrameIndex < this._originalKeyFrameIndex)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x040017BA RID: 6074
		internal int _originalKeyFrameIndex;

		// Token: 0x040017BB RID: 6075
		internal TimeSpan _resolvedKeyTime;
	}
}
