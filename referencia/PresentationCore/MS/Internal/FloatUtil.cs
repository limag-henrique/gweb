using System;

namespace MS.Internal
{
	// Token: 0x02000677 RID: 1655
	internal static class FloatUtil
	{
		// Token: 0x06004916 RID: 18710 RVA: 0x0011D3CC File Offset: 0x0011C7CC
		public static bool AreClose(float a, float b)
		{
			if (a == b)
			{
				return true;
			}
			float num = (Math.Abs(a) + Math.Abs(b) + 10f) * FloatUtil.FLT_EPSILON;
			float num2 = a - b;
			return -num < num2 && num > num2;
		}

		// Token: 0x06004917 RID: 18711 RVA: 0x0011D40C File Offset: 0x0011C80C
		public static bool IsOne(float a)
		{
			return Math.Abs(a - 1f) < 10f * FloatUtil.FLT_EPSILON;
		}

		// Token: 0x06004918 RID: 18712 RVA: 0x0011D434 File Offset: 0x0011C834
		public static bool IsZero(float a)
		{
			return Math.Abs(a) < 10f * FloatUtil.FLT_EPSILON;
		}

		// Token: 0x06004919 RID: 18713 RVA: 0x0011D458 File Offset: 0x0011C858
		public static bool IsCloseToDivideByZero(float numerator, float denominator)
		{
			return Math.Abs(denominator) <= Math.Abs(numerator) * FloatUtil.INVERSE_FLT_MAX_PRECISION;
		}

		// Token: 0x04001CB3 RID: 7347
		internal static float FLT_EPSILON = 1.1920929E-07f;

		// Token: 0x04001CB4 RID: 7348
		internal static float FLT_MAX_PRECISION = 16777215f;

		// Token: 0x04001CB5 RID: 7349
		internal static float INVERSE_FLT_MAX_PRECISION = 1f / FloatUtil.FLT_MAX_PRECISION;
	}
}
