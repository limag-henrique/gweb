using System;
using System.Globalization;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000755 RID: 1877
	internal struct DigitMap
	{
		// Token: 0x06004EA6 RID: 20134 RVA: 0x00138684 File Offset: 0x00137A84
		internal DigitMap(CultureInfo digitCulture)
		{
			if (digitCulture != null)
			{
				this._format = digitCulture.NumberFormat;
				this._digits = this._format.NativeDigits;
				return;
			}
			this._format = null;
			this._digits = null;
		}

		// Token: 0x17001030 RID: 4144
		internal int this[int ch]
		{
			get
			{
				if (this._format != null && DigitMap.IsDigitOrSymbol(ch))
				{
					uint num = (uint)(ch - 48);
					if (num < 10U)
					{
						ch = DigitMap.StringToScalar(this._digits[(int)num], ch);
					}
					else if (ch == 37)
					{
						ch = DigitMap.StringToScalar(this._format.PercentSymbol, ch);
					}
					else if (ch == 44)
					{
						ch = DigitMap.StringToScalar(this._format.NumberGroupSeparator, ch);
					}
					else
					{
						ch = DigitMap.StringToScalar(this._format.NumberDecimalSeparator, ch);
					}
				}
				return ch;
			}
		}

		// Token: 0x06004EA8 RID: 20136 RVA: 0x00138744 File Offset: 0x00137B44
		internal static int GetFallbackCharacter(int ch)
		{
			if (ch == 1643)
			{
				return 44;
			}
			if (ch == 1644)
			{
				return 1548;
			}
			if (ch != 3046)
			{
				return 0;
			}
			return 48;
		}

		// Token: 0x06004EA9 RID: 20137 RVA: 0x00138778 File Offset: 0x00137B78
		private static int StringToScalar(string s, int defaultValue)
		{
			if (s.Length == 1)
			{
				return (int)s[0];
			}
			if (s.Length == 2 && DigitMap.IsHighSurrogate((int)s[0]) && DigitMap.IsLowSurrogate((int)s[1]))
			{
				return DigitMap.MakeUnicodeScalar((int)s[0], (int)s[1]);
			}
			return defaultValue;
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x001387D0 File Offset: 0x00137BD0
		internal static bool IsHighSurrogate(int ch)
		{
			return ch >= 55296 && ch < 56320;
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x001387F0 File Offset: 0x00137BF0
		internal static bool IsLowSurrogate(int ch)
		{
			return ch >= 56320 && ch < 57344;
		}

		// Token: 0x06004EAC RID: 20140 RVA: 0x00138810 File Offset: 0x00137C10
		internal static bool IsSurrogate(int ch)
		{
			return DigitMap.IsHighSurrogate(ch) || DigitMap.IsLowSurrogate(ch);
		}

		// Token: 0x06004EAD RID: 20141 RVA: 0x00138830 File Offset: 0x00137C30
		internal static int MakeUnicodeScalar(int hi, int lo)
		{
			return ((hi & 1023) << 10 | (lo & 1023)) + 65536;
		}

		// Token: 0x06004EAE RID: 20142 RVA: 0x00138858 File Offset: 0x00137C58
		private static bool IsDigitOrSymbol(int ch)
		{
			return ch - 37 <= 20 && (2095745U >> ch - 37 & 1U) > 0U;
		}

		// Token: 0x040023C2 RID: 9154
		private NumberFormatInfo _format;

		// Token: 0x040023C3 RID: 9155
		private string[] _digits;
	}
}
