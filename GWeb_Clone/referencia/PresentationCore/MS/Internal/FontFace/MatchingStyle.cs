using System;
using System.Windows;

namespace MS.Internal.FontFace
{
	// Token: 0x0200076C RID: 1900
	internal struct MatchingStyle
	{
		// Token: 0x06005019 RID: 20505 RVA: 0x0014069C File Offset: 0x0013FA9C
		internal MatchingStyle(FontStyle style, FontWeight weight, FontStretch stretch)
		{
			this._vector = new MatchingStyle.Vector((double)(stretch.ToOpenTypeStretch() - FontStretches.Normal.ToOpenTypeStretch()) * 11.0, (double)style.GetStyleForInternalConstruction() * 7.0, (double)(weight.ToOpenTypeWeight() - FontWeights.Normal.ToOpenTypeWeight()) / 100.0 * 5.0);
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x00140710 File Offset: 0x0013FB10
		public static bool operator ==(MatchingStyle l, MatchingStyle r)
		{
			return l._vector == r._vector;
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x00140730 File Offset: 0x0013FB30
		public static bool operator !=(MatchingStyle l, MatchingStyle r)
		{
			return l._vector != r._vector;
		}

		// Token: 0x0600501C RID: 20508 RVA: 0x00140750 File Offset: 0x0013FB50
		public override bool Equals(object o)
		{
			return o != null && o is MatchingStyle && this == (MatchingStyle)o;
		}

		// Token: 0x0600501D RID: 20509 RVA: 0x00140780 File Offset: 0x0013FB80
		public override int GetHashCode()
		{
			return this._vector.GetHashCode();
		}

		// Token: 0x0600501E RID: 20510 RVA: 0x001407A0 File Offset: 0x0013FBA0
		internal static bool IsBetterMatch(MatchingStyle target, MatchingStyle best, ref MatchingStyle matching)
		{
			return matching.IsBetterMatch(target, best);
		}

		// Token: 0x0600501F RID: 20511 RVA: 0x001407B8 File Offset: 0x0013FBB8
		internal bool IsBetterMatch(MatchingStyle target, MatchingStyle best)
		{
			double lengthSquared = (this._vector - target._vector).LengthSquared;
			double lengthSquared2 = (best._vector - target._vector).LengthSquared;
			if (lengthSquared < lengthSquared2)
			{
				return true;
			}
			if (lengthSquared == lengthSquared2)
			{
				double num = MatchingStyle.Vector.DotProduct(this._vector, target._vector);
				double num2 = MatchingStyle.Vector.DotProduct(best._vector, target._vector);
				if (num > num2)
				{
					return true;
				}
				if (num == num2 && (this._vector.X > best._vector.X || (this._vector.X == best._vector.X && (this._vector.Y > best._vector.Y || (this._vector.Y == best._vector.Y && this._vector.Z > best._vector.Z)))))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04002472 RID: 9330
		private MatchingStyle.Vector _vector;

		// Token: 0x04002473 RID: 9331
		private const double FontWeightScale = 5.0;

		// Token: 0x04002474 RID: 9332
		private const double FontStyleScale = 7.0;

		// Token: 0x04002475 RID: 9333
		private const double FontStretchScale = 11.0;

		// Token: 0x020009EB RID: 2539
		private struct Vector
		{
			// Token: 0x06005B99 RID: 23449 RVA: 0x001704C0 File Offset: 0x0016F8C0
			internal Vector(double x, double y, double z)
			{
				this._x = x;
				this._y = y;
				this._z = z;
			}

			// Token: 0x170012B7 RID: 4791
			// (get) Token: 0x06005B9A RID: 23450 RVA: 0x001704E4 File Offset: 0x0016F8E4
			internal double X
			{
				get
				{
					return this._x;
				}
			}

			// Token: 0x170012B8 RID: 4792
			// (get) Token: 0x06005B9B RID: 23451 RVA: 0x001704F8 File Offset: 0x0016F8F8
			internal double Y
			{
				get
				{
					return this._y;
				}
			}

			// Token: 0x170012B9 RID: 4793
			// (get) Token: 0x06005B9C RID: 23452 RVA: 0x0017050C File Offset: 0x0016F90C
			internal double Z
			{
				get
				{
					return this._z;
				}
			}

			// Token: 0x170012BA RID: 4794
			// (get) Token: 0x06005B9D RID: 23453 RVA: 0x00170520 File Offset: 0x0016F920
			internal double LengthSquared
			{
				get
				{
					return this._x * this._x + this._y * this._y + this._z * this._z;
				}
			}

			// Token: 0x06005B9E RID: 23454 RVA: 0x00170558 File Offset: 0x0016F958
			internal static double DotProduct(MatchingStyle.Vector l, MatchingStyle.Vector r)
			{
				return l._x * r._x + l._y * r._y + l._z * r._z;
			}

			// Token: 0x06005B9F RID: 23455 RVA: 0x00170590 File Offset: 0x0016F990
			public static MatchingStyle.Vector operator -(MatchingStyle.Vector l, MatchingStyle.Vector r)
			{
				return new MatchingStyle.Vector(l._x - r._x, l._y - r._y, l._z - r._z);
			}

			// Token: 0x06005BA0 RID: 23456 RVA: 0x001705CC File Offset: 0x0016F9CC
			public static bool operator ==(MatchingStyle.Vector l, MatchingStyle.Vector r)
			{
				return l._x == r._x && l._y == r._y && l._z == r._z;
			}

			// Token: 0x06005BA1 RID: 23457 RVA: 0x00170608 File Offset: 0x0016FA08
			public static bool operator !=(MatchingStyle.Vector l, MatchingStyle.Vector r)
			{
				return !(l == r);
			}

			// Token: 0x06005BA2 RID: 23458 RVA: 0x00170620 File Offset: 0x0016FA20
			public override bool Equals(object o)
			{
				if (o == null)
				{
					return false;
				}
				if (o is MatchingStyle.Vector)
				{
					MatchingStyle.Vector r = (MatchingStyle.Vector)o;
					return this == r;
				}
				return false;
			}

			// Token: 0x06005BA3 RID: 23459 RVA: 0x00170650 File Offset: 0x0016FA50
			public override int GetHashCode()
			{
				return this._x.GetHashCode() ^ this._y.GetHashCode() ^ this._z.GetHashCode();
			}

			// Token: 0x04002EF2 RID: 12018
			private double _x;

			// Token: 0x04002EF3 RID: 12019
			private double _y;

			// Token: 0x04002EF4 RID: 12020
			private double _z;
		}
	}
}
