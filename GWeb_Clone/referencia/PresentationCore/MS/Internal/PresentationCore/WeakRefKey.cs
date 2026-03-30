using System;

namespace MS.Internal.PresentationCore
{
	// Token: 0x020007E6 RID: 2022
	internal class WeakRefKey : WeakReference
	{
		// Token: 0x060054D6 RID: 21718 RVA: 0x0015E21C File Offset: 0x0015D61C
		public WeakRefKey(object target) : base(target)
		{
			this._hashCode = target.GetHashCode();
		}

		// Token: 0x060054D7 RID: 21719 RVA: 0x0015E23C File Offset: 0x0015D63C
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x060054D8 RID: 21720 RVA: 0x0015E250 File Offset: 0x0015D650
		public override bool Equals(object o)
		{
			WeakRefKey weakRefKey = o as WeakRefKey;
			if (weakRefKey != null)
			{
				object target = this.Target;
				object target2 = weakRefKey.Target;
				if (target != null && target2 != null)
				{
					return target == target2;
				}
			}
			return base.Equals(o);
		}

		// Token: 0x060054D9 RID: 21721 RVA: 0x0015E290 File Offset: 0x0015D690
		public static bool operator ==(WeakRefKey left, WeakRefKey right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x060054DA RID: 21722 RVA: 0x0015E2AC File Offset: 0x0015D6AC
		public static bool operator !=(WeakRefKey left, WeakRefKey right)
		{
			return !(left == right);
		}

		// Token: 0x0400264F RID: 9807
		private int _hashCode;
	}
}
