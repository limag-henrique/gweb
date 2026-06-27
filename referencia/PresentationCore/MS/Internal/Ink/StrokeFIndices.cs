using System;
using System.Globalization;

namespace MS.Internal.Ink
{
	// Token: 0x020007BF RID: 1983
	internal struct StrokeFIndices : IEquatable<StrokeFIndices>
	{
		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x0600537F RID: 21375 RVA: 0x0014FDD0 File Offset: 0x0014F1D0
		internal static double BeforeFirst
		{
			get
			{
				return double.MinValue;
			}
		}

		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x06005380 RID: 21376 RVA: 0x0014FDE8 File Offset: 0x0014F1E8
		internal static double AfterLast
		{
			get
			{
				return double.MaxValue;
			}
		}

		// Token: 0x06005381 RID: 21377 RVA: 0x0014FE00 File Offset: 0x0014F200
		internal StrokeFIndices(double beginFIndex, double endFIndex)
		{
			this._beginFIndex = beginFIndex;
			this._endFIndex = endFIndex;
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x06005382 RID: 21378 RVA: 0x0014FE1C File Offset: 0x0014F21C
		// (set) Token: 0x06005383 RID: 21379 RVA: 0x0014FE30 File Offset: 0x0014F230
		internal double BeginFIndex
		{
			get
			{
				return this._beginFIndex;
			}
			set
			{
				this._beginFIndex = value;
			}
		}

		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x06005384 RID: 21380 RVA: 0x0014FE44 File Offset: 0x0014F244
		// (set) Token: 0x06005385 RID: 21381 RVA: 0x0014FE58 File Offset: 0x0014F258
		internal double EndFIndex
		{
			get
			{
				return this._endFIndex;
			}
			set
			{
				this._endFIndex = value;
			}
		}

		// Token: 0x06005386 RID: 21382 RVA: 0x0014FE6C File Offset: 0x0014F26C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{",
				StrokeFIndices.GetStringRepresentation(this._beginFIndex),
				",",
				StrokeFIndices.GetStringRepresentation(this._endFIndex),
				"}"
			});
		}

		// Token: 0x06005387 RID: 21383 RVA: 0x0014FEB8 File Offset: 0x0014F2B8
		public bool Equals(StrokeFIndices strokeFIndices)
		{
			return strokeFIndices == this;
		}

		// Token: 0x06005388 RID: 21384 RVA: 0x0014FED4 File Offset: 0x0014F2D4
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && (StrokeFIndices)obj == this;
		}

		// Token: 0x06005389 RID: 21385 RVA: 0x0014FF14 File Offset: 0x0014F314
		public override int GetHashCode()
		{
			return this._beginFIndex.GetHashCode() ^ this._endFIndex.GetHashCode();
		}

		// Token: 0x0600538A RID: 21386 RVA: 0x0014FF38 File Offset: 0x0014F338
		public static bool operator ==(StrokeFIndices sfiLeft, StrokeFIndices sfiRight)
		{
			return DoubleUtil.AreClose(sfiLeft._beginFIndex, sfiRight._beginFIndex) && DoubleUtil.AreClose(sfiLeft._endFIndex, sfiRight._endFIndex);
		}

		// Token: 0x0600538B RID: 21387 RVA: 0x0014FF6C File Offset: 0x0014F36C
		public static bool operator !=(StrokeFIndices sfiLeft, StrokeFIndices sfiRight)
		{
			return !(sfiLeft == sfiRight);
		}

		// Token: 0x0600538C RID: 21388 RVA: 0x0014FF84 File Offset: 0x0014F384
		internal static string GetStringRepresentation(double fIndex)
		{
			if (DoubleUtil.AreClose(fIndex, StrokeFIndices.BeforeFirst))
			{
				return "BeforeFirst";
			}
			if (DoubleUtil.AreClose(fIndex, StrokeFIndices.AfterLast))
			{
				return "AfterLast";
			}
			return fIndex.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x0600538D RID: 21389 RVA: 0x0014FFC4 File Offset: 0x0014F3C4
		internal static StrokeFIndices Empty
		{
			get
			{
				return StrokeFIndices.s_empty;
			}
		}

		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x0600538E RID: 21390 RVA: 0x0014FFD8 File Offset: 0x0014F3D8
		internal static StrokeFIndices Full
		{
			get
			{
				return StrokeFIndices.s_full;
			}
		}

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x0600538F RID: 21391 RVA: 0x0014FFEC File Offset: 0x0014F3EC
		internal bool IsEmpty
		{
			get
			{
				return DoubleUtil.GreaterThanOrClose(this._beginFIndex, this._endFIndex);
			}
		}

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x06005390 RID: 21392 RVA: 0x0015000C File Offset: 0x0014F40C
		internal bool IsFull
		{
			get
			{
				return DoubleUtil.AreClose(this._beginFIndex, StrokeFIndices.BeforeFirst) && DoubleUtil.AreClose(this._endFIndex, StrokeFIndices.AfterLast);
			}
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x00150040 File Offset: 0x0014F440
		internal int CompareTo(StrokeFIndices fIndices)
		{
			if (DoubleUtil.AreClose(this.BeginFIndex, fIndices.BeginFIndex))
			{
				return 0;
			}
			if (DoubleUtil.GreaterThan(this.BeginFIndex, fIndices.BeginFIndex))
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x040025BD RID: 9661
		private static StrokeFIndices s_empty = new StrokeFIndices(StrokeFIndices.AfterLast, StrokeFIndices.BeforeFirst);

		// Token: 0x040025BE RID: 9662
		private static StrokeFIndices s_full = new StrokeFIndices(StrokeFIndices.BeforeFirst, StrokeFIndices.AfterLast);

		// Token: 0x040025BF RID: 9663
		private double _beginFIndex;

		// Token: 0x040025C0 RID: 9664
		private double _endFIndex;
	}
}
