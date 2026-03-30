using System;
using System.Collections.Generic;
using System.Windows;

namespace MS.Internal.Ink
{
	// Token: 0x020007BE RID: 1982
	internal struct Quad
	{
		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x06005370 RID: 21360 RVA: 0x0014FB78 File Offset: 0x0014EF78
		internal static Quad Empty
		{
			get
			{
				return Quad.s_empty;
			}
		}

		// Token: 0x06005371 RID: 21361 RVA: 0x0014FB8C File Offset: 0x0014EF8C
		internal Quad(Point a, Point b, Point c, Point d)
		{
			this._A = a;
			this._B = b;
			this._C = c;
			this._D = d;
		}

		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x06005372 RID: 21362 RVA: 0x0014FBB8 File Offset: 0x0014EFB8
		// (set) Token: 0x06005373 RID: 21363 RVA: 0x0014FBCC File Offset: 0x0014EFCC
		internal Point A
		{
			get
			{
				return this._A;
			}
			set
			{
				this._A = value;
			}
		}

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x06005374 RID: 21364 RVA: 0x0014FBE0 File Offset: 0x0014EFE0
		// (set) Token: 0x06005375 RID: 21365 RVA: 0x0014FBF4 File Offset: 0x0014EFF4
		internal Point B
		{
			get
			{
				return this._B;
			}
			set
			{
				this._B = value;
			}
		}

		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x06005376 RID: 21366 RVA: 0x0014FC08 File Offset: 0x0014F008
		// (set) Token: 0x06005377 RID: 21367 RVA: 0x0014FC1C File Offset: 0x0014F01C
		internal Point C
		{
			get
			{
				return this._C;
			}
			set
			{
				this._C = value;
			}
		}

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x06005378 RID: 21368 RVA: 0x0014FC30 File Offset: 0x0014F030
		// (set) Token: 0x06005379 RID: 21369 RVA: 0x0014FC44 File Offset: 0x0014F044
		internal Point D
		{
			get
			{
				return this._D;
			}
			set
			{
				this._D = value;
			}
		}

		// Token: 0x17001156 RID: 4438
		internal Point this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this._A;
				case 1:
					return this._B;
				case 2:
					return this._C;
				case 3:
					return this._D;
				default:
					throw new IndexOutOfRangeException("index");
				}
			}
		}

		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x0600537B RID: 21371 RVA: 0x0014FCA4 File Offset: 0x0014F0A4
		internal bool IsEmpty
		{
			get
			{
				return this._A == this._B && this._C == this._D;
			}
		}

		// Token: 0x0600537C RID: 21372 RVA: 0x0014FCD8 File Offset: 0x0014F0D8
		internal void GetPoints(List<Point> pointBuffer)
		{
			pointBuffer.Add(this._A);
			pointBuffer.Add(this._B);
			pointBuffer.Add(this._C);
			pointBuffer.Add(this._D);
		}

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x0600537D RID: 21373 RVA: 0x0014FD18 File Offset: 0x0014F118
		internal Rect Bounds
		{
			get
			{
				if (!this.IsEmpty)
				{
					return Rect.Union(new Rect(this._A, this._B), new Rect(this._C, this._D));
				}
				return Rect.Empty;
			}
		}

		// Token: 0x040025B8 RID: 9656
		private static Quad s_empty = new Quad(new Point(0.0, 0.0), new Point(0.0, 0.0), new Point(0.0, 0.0), new Point(0.0, 0.0));

		// Token: 0x040025B9 RID: 9657
		private Point _A;

		// Token: 0x040025BA RID: 9658
		private Point _B;

		// Token: 0x040025BB RID: 9659
		private Point _C;

		// Token: 0x040025BC RID: 9660
		private Point _D;
	}
}
