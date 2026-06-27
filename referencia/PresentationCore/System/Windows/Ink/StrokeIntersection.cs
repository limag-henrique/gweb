using System;
using System.Collections.Generic;
using MS.Internal.Ink;

namespace System.Windows.Ink
{
	// Token: 0x0200033D RID: 829
	internal struct StrokeIntersection
	{
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x00072C28 File Offset: 0x00072028
		internal static double BeforeFirst
		{
			get
			{
				return StrokeFIndices.BeforeFirst;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x00072C3C File Offset: 0x0007203C
		internal static double AfterLast
		{
			get
			{
				return StrokeFIndices.AfterLast;
			}
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x00072C50 File Offset: 0x00072050
		internal StrokeIntersection(double hitBegin, double inBegin, double inEnd, double hitEnd)
		{
			this._hitSegment = new StrokeFIndices(hitBegin, hitEnd);
			this._inSegment = new StrokeFIndices(inBegin, inEnd);
		}

		// Token: 0x17000540 RID: 1344
		// (set) Token: 0x06001C23 RID: 7203 RVA: 0x00072C78 File Offset: 0x00072078
		internal double HitBegin
		{
			set
			{
				this._hitSegment.BeginFIndex = value;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x00072C94 File Offset: 0x00072094
		// (set) Token: 0x06001C25 RID: 7205 RVA: 0x00072CAC File Offset: 0x000720AC
		internal double HitEnd
		{
			get
			{
				return this._hitSegment.EndFIndex;
			}
			set
			{
				this._hitSegment.EndFIndex = value;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001C26 RID: 7206 RVA: 0x00072CC8 File Offset: 0x000720C8
		// (set) Token: 0x06001C27 RID: 7207 RVA: 0x00072CE0 File Offset: 0x000720E0
		internal double InBegin
		{
			get
			{
				return this._inSegment.BeginFIndex;
			}
			set
			{
				this._inSegment.BeginFIndex = value;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001C28 RID: 7208 RVA: 0x00072CFC File Offset: 0x000720FC
		// (set) Token: 0x06001C29 RID: 7209 RVA: 0x00072D14 File Offset: 0x00072114
		internal double InEnd
		{
			get
			{
				return this._inSegment.EndFIndex;
			}
			set
			{
				this._inSegment.EndFIndex = value;
			}
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x00072D30 File Offset: 0x00072130
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{",
				StrokeFIndices.GetStringRepresentation(this._hitSegment.BeginFIndex),
				",",
				StrokeFIndices.GetStringRepresentation(this._inSegment.BeginFIndex),
				",",
				StrokeFIndices.GetStringRepresentation(this._inSegment.EndFIndex),
				",",
				StrokeFIndices.GetStringRepresentation(this._hitSegment.EndFIndex),
				"}"
			});
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x00072DC0 File Offset: 0x000721C0
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && (StrokeIntersection)obj == this;
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x00072E00 File Offset: 0x00072200
		public override int GetHashCode()
		{
			return this._hitSegment.GetHashCode() ^ this._inSegment.GetHashCode();
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x00072E30 File Offset: 0x00072230
		public static bool operator ==(StrokeIntersection left, StrokeIntersection right)
		{
			return left._hitSegment == right._hitSegment && left._inSegment == right._inSegment;
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x00072E64 File Offset: 0x00072264
		public static bool operator !=(StrokeIntersection left, StrokeIntersection right)
		{
			return !(left == right);
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001C2F RID: 7215 RVA: 0x00072E7C File Offset: 0x0007227C
		internal static StrokeIntersection Full
		{
			get
			{
				return StrokeIntersection.s_full;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001C30 RID: 7216 RVA: 0x00072E90 File Offset: 0x00072290
		internal bool IsEmpty
		{
			get
			{
				return this._hitSegment.IsEmpty;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001C31 RID: 7217 RVA: 0x00072EA8 File Offset: 0x000722A8
		internal StrokeFIndices HitSegment
		{
			get
			{
				return this._hitSegment;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001C32 RID: 7218 RVA: 0x00072EBC File Offset: 0x000722BC
		internal StrokeFIndices InSegment
		{
			get
			{
				return this._inSegment;
			}
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x00072ED0 File Offset: 0x000722D0
		internal static StrokeFIndices[] GetInSegments(StrokeIntersection[] intersections)
		{
			List<StrokeFIndices> list = new List<StrokeFIndices>(intersections.Length);
			for (int i = 0; i < intersections.Length; i++)
			{
				if (!intersections[i].InSegment.IsEmpty)
				{
					if (list.Count > 0 && list[list.Count - 1].EndFIndex >= intersections[i].InSegment.BeginFIndex)
					{
						StrokeFIndices value = list[list.Count - 1];
						value.EndFIndex = intersections[i].InSegment.EndFIndex;
						list[list.Count - 1] = value;
					}
					else
					{
						list.Add(intersections[i].InSegment);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x00072F9C File Offset: 0x0007239C
		internal static StrokeFIndices[] GetHitSegments(StrokeIntersection[] intersections)
		{
			List<StrokeFIndices> list = new List<StrokeFIndices>(intersections.Length);
			for (int i = 0; i < intersections.Length; i++)
			{
				if (!intersections[i].HitSegment.IsEmpty)
				{
					if (list.Count > 0 && list[list.Count - 1].EndFIndex >= intersections[i].HitSegment.BeginFIndex)
					{
						StrokeFIndices value = list[list.Count - 1];
						value.EndFIndex = intersections[i].HitSegment.EndFIndex;
						list[list.Count - 1] = value;
					}
					else
					{
						list.Add(intersections[i].HitSegment);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x04000F40 RID: 3904
		private static StrokeIntersection s_empty = new StrokeIntersection(StrokeIntersection.AfterLast, StrokeIntersection.AfterLast, StrokeIntersection.BeforeFirst, StrokeIntersection.BeforeFirst);

		// Token: 0x04000F41 RID: 3905
		private static StrokeIntersection s_full = new StrokeIntersection(StrokeIntersection.BeforeFirst, StrokeIntersection.BeforeFirst, StrokeIntersection.AfterLast, StrokeIntersection.AfterLast);

		// Token: 0x04000F42 RID: 3906
		private StrokeFIndices _hitSegment;

		// Token: 0x04000F43 RID: 3907
		private StrokeFIndices _inSegment;
	}
}
