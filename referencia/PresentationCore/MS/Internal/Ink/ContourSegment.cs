using System;
using System.Windows;

namespace MS.Internal.Ink
{
	// Token: 0x020007B8 RID: 1976
	internal struct ContourSegment
	{
		// Token: 0x0600532B RID: 21291 RVA: 0x0014CDB8 File Offset: 0x0014C1B8
		internal ContourSegment(Point begin, Point end)
		{
			this._begin = begin;
			this._vector = (DoubleUtil.AreClose(begin, end) ? new Vector(0.0, 0.0) : (end - begin));
			this._radius = new Vector(0.0, 0.0);
		}

		// Token: 0x0600532C RID: 21292 RVA: 0x0014CE18 File Offset: 0x0014C218
		internal ContourSegment(Point begin, Point end, Point center)
		{
			this._begin = begin;
			this._vector = end - begin;
			this._radius = center - begin;
		}

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x0600532D RID: 21293 RVA: 0x0014CE48 File Offset: 0x0014C248
		internal bool IsArc
		{
			get
			{
				return this._radius.X != 0.0 || this._radius.Y != 0.0;
			}
		}

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x0600532E RID: 21294 RVA: 0x0014CE88 File Offset: 0x0014C288
		internal Point Begin
		{
			get
			{
				return this._begin;
			}
		}

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x0600532F RID: 21295 RVA: 0x0014CE9C File Offset: 0x0014C29C
		internal Point End
		{
			get
			{
				return this._begin + this._vector;
			}
		}

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x06005330 RID: 21296 RVA: 0x0014CEBC File Offset: 0x0014C2BC
		internal Vector Vector
		{
			get
			{
				return this._vector;
			}
		}

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x06005331 RID: 21297 RVA: 0x0014CED0 File Offset: 0x0014C2D0
		internal Vector Radius
		{
			get
			{
				return this._radius;
			}
		}

		// Token: 0x040025A0 RID: 9632
		private Point _begin;

		// Token: 0x040025A1 RID: 9633
		private Vector _vector;

		// Token: 0x040025A2 RID: 9634
		private Vector _radius;
	}
}
