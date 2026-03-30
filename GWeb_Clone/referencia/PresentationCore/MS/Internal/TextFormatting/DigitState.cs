using System;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000754 RID: 1876
	internal class DigitState
	{
		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x06004E99 RID: 20121 RVA: 0x00137F64 File Offset: 0x00137364
		internal CultureInfo DigitCulture
		{
			get
			{
				return this._digitCulture;
			}
		}

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x06004E9A RID: 20122 RVA: 0x00137F78 File Offset: 0x00137378
		internal bool RequiresNumberSubstitution
		{
			get
			{
				return this._digitCulture != null;
			}
		}

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x06004E9B RID: 20123 RVA: 0x00137F90 File Offset: 0x00137390
		internal bool Contextual
		{
			get
			{
				return this._contextual;
			}
		}

		// Token: 0x06004E9C RID: 20124 RVA: 0x00137FA4 File Offset: 0x001373A4
		internal static NumberSubstitutionMethod GetResolvedSubstitutionMethod(TextRunProperties properties, CultureInfo digitCulture, out bool ignoreUserOverride)
		{
			ignoreUserOverride = true;
			NumberSubstitutionMethod numberSubstitutionMethod = NumberSubstitutionMethod.European;
			if (digitCulture != null)
			{
				NumberSubstitutionMethod numberSubstitutionMethod2;
				CultureInfo numberCulture = DigitState.GetNumberCulture(properties, out numberSubstitutionMethod2, out ignoreUserOverride);
				if (numberCulture != null)
				{
					if (numberSubstitutionMethod2 == NumberSubstitutionMethod.AsCulture)
					{
						DigitShapes digitSubstitution = numberCulture.NumberFormat.DigitSubstitution;
						if (digitSubstitution != DigitShapes.Context)
						{
							if (digitSubstitution != DigitShapes.NativeNational)
							{
								numberSubstitutionMethod2 = NumberSubstitutionMethod.European;
							}
							else
							{
								numberSubstitutionMethod2 = NumberSubstitutionMethod.NativeNational;
							}
						}
						else
						{
							numberSubstitutionMethod2 = NumberSubstitutionMethod.Context;
						}
					}
					numberSubstitutionMethod = numberSubstitutionMethod2;
					if (numberSubstitutionMethod == NumberSubstitutionMethod.Context)
					{
						numberSubstitutionMethod = NumberSubstitutionMethod.Traditional;
					}
				}
			}
			return numberSubstitutionMethod;
		}

		// Token: 0x06004E9D RID: 20125 RVA: 0x00137FF4 File Offset: 0x001373F4
		internal void SetTextRunProperties(TextRunProperties properties)
		{
			NumberSubstitutionMethod numberSubstitutionMethod;
			bool flag;
			CultureInfo numberCulture = DigitState.GetNumberCulture(properties, out numberSubstitutionMethod, out flag);
			if (numberCulture != this._lastNumberCulture || numberSubstitutionMethod != this._lastMethod)
			{
				this._lastNumberCulture = numberCulture;
				this._lastMethod = numberSubstitutionMethod;
				this._digitCulture = this.GetDigitCulture(numberCulture, numberSubstitutionMethod, out this._contextual);
			}
		}

		// Token: 0x06004E9E RID: 20126 RVA: 0x00138040 File Offset: 0x00137440
		private static CultureInfo GetNumberCulture(TextRunProperties properties, out NumberSubstitutionMethod method, out bool ignoreUserOverride)
		{
			ignoreUserOverride = true;
			NumberSubstitution numberSubstitution = properties.NumberSubstitution;
			if (numberSubstitution == null)
			{
				method = NumberSubstitutionMethod.AsCulture;
				return CultureMapper.GetSpecificCulture(properties.CultureInfo);
			}
			method = numberSubstitution.Substitution;
			switch (numberSubstitution.CultureSource)
			{
			case NumberCultureSource.Text:
				return CultureMapper.GetSpecificCulture(properties.CultureInfo);
			case NumberCultureSource.User:
				ignoreUserOverride = false;
				return CultureInfo.CurrentCulture;
			case NumberCultureSource.Override:
				return numberSubstitution.CultureOverride;
			default:
				return null;
			}
		}

		// Token: 0x06004E9F RID: 20127 RVA: 0x001380AC File Offset: 0x001374AC
		private CultureInfo GetDigitCulture(CultureInfo numberCulture, NumberSubstitutionMethod method, out bool contextual)
		{
			contextual = false;
			if (numberCulture == null)
			{
				return null;
			}
			if (method == NumberSubstitutionMethod.AsCulture)
			{
				DigitShapes digitSubstitution = numberCulture.NumberFormat.DigitSubstitution;
				if (digitSubstitution != DigitShapes.Context)
				{
					if (digitSubstitution != DigitShapes.NativeNational)
					{
						return null;
					}
					method = NumberSubstitutionMethod.NativeNational;
				}
				else
				{
					method = NumberSubstitutionMethod.Context;
				}
			}
			switch (method)
			{
			case NumberSubstitutionMethod.Context:
				if (DigitState.IsArabic(numberCulture) || DigitState.IsFarsi(numberCulture))
				{
					contextual = true;
					return this.GetTraditionalCulture(numberCulture);
				}
				return null;
			case NumberSubstitutionMethod.NativeNational:
				if (!DigitState.HasLatinDigits(numberCulture))
				{
					return numberCulture;
				}
				return null;
			case NumberSubstitutionMethod.Traditional:
				return this.GetTraditionalCulture(numberCulture);
			}
			return null;
		}

		// Token: 0x06004EA0 RID: 20128 RVA: 0x00138140 File Offset: 0x00137540
		private static bool HasLatinDigits(CultureInfo culture)
		{
			string[] nativeDigits = culture.NumberFormat.NativeDigits;
			for (int i = 0; i < 10; i++)
			{
				string text = nativeDigits[i];
				if (text.Length != 1 || text[0] != (char)(48 + i))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004EA1 RID: 20129 RVA: 0x00138184 File Offset: 0x00137584
		private static bool IsArabic(CultureInfo culture)
		{
			return (culture.LCID & 255) == 1;
		}

		// Token: 0x06004EA2 RID: 20130 RVA: 0x001381A0 File Offset: 0x001375A0
		private static bool IsFarsi(CultureInfo culture)
		{
			return (culture.LCID & 255) == 41;
		}

		// Token: 0x06004EA3 RID: 20131 RVA: 0x001381C0 File Offset: 0x001375C0
		private CultureInfo GetTraditionalCulture(CultureInfo numberCulture)
		{
			int lcid = numberCulture.LCID;
			if (this._lastTraditionalCulture != null && this._lastTraditionalCulture.LCID == lcid)
			{
				return this._lastTraditionalCulture;
			}
			CultureInfo cultureInfo = null;
			int num = lcid & 255;
			if (num <= 32)
			{
				if (num == 1)
				{
					cultureInfo = this.CreateTraditionalCulture(numberCulture, 1632, true);
					goto IL_38B;
				}
				if (num == 30)
				{
					cultureInfo = this.CreateTraditionalCulture(numberCulture, 3664, false);
					goto IL_38B;
				}
				if (num != 32)
				{
					goto IL_38B;
				}
			}
			else if (num != 41)
			{
				switch (num)
				{
				case 57:
					cultureInfo = this.CreateTraditionalCulture(numberCulture, 2406, false);
					goto IL_38B;
				case 58:
				case 59:
				case 60:
				case 61:
				case 62:
				case 63:
				case 64:
				case 65:
				case 66:
				case 67:
				case 68:
				case 82:
				case 86:
				case 90:
				case 91:
				case 92:
				case 93:
				case 94:
				case 98:
					goto IL_38B;
				case 69:
					cultureInfo = this.CreateTraditionalCulture(numberCulture, 2534, false);
					goto IL_38B;
				case 70:
					if (lcid == 1094)
					{
						cultureInfo = this.CreateTraditionalCulture(numberCulture, 2662, false);
						goto IL_38B;
					}
					if (lcid == 2118)
					{
						cultureInfo = this.CreateTraditionalCulture(numberCulture, 1776, true);
						goto IL_38B;
					}
					goto IL_38B;
				case 71:
					cultureInfo = this.CreateTraditionalCulture(numberCulture, 2790, false);
					goto IL_38B;
				case 72:
					cultureInfo = this.CreateTraditionalCulture(numberCulture, 2918, false);
					goto IL_38B;
				case 73:
					cultureInfo = this.CreateTraditionalCulture(numberCulture, 3046, false);
					goto IL_38B;
				case 74:
					cultureInfo = this.CreateTraditionalCulture(numberCulture, 3174, false);
					goto IL_38B;
				case 75:
					cultureInfo = this.CreateTraditionalCulture(numberCulture, 3302, false);
					goto IL_38B;
				case 76:
					cultureInfo = this.CreateTraditionalCulture(numberCulture, 3430, false);
					goto IL_38B;
				case 77:
					cultureInfo = this.CreateTraditionalCulture(numberCulture, 2534, false);
					goto IL_38B;
				case 78:
				case 79:
					cultureInfo = this.CreateTraditionalCulture(numberCulture, 2406, false);
					goto IL_38B;
				case 80:
					if (lcid == 2128)
					{
						cultureInfo = this.CreateTraditionalCulture(numberCulture, 6160, false);
						goto IL_38B;
					}
					goto IL_38B;
				case 81:
					cultureInfo = this.CreateTraditionalCulture(numberCulture, 3872, false);
					goto IL_38B;
				case 83:
					cultureInfo = this.CreateTraditionalCulture(numberCulture, 6112, false);
					goto IL_38B;
				case 84:
					cultureInfo = this.CreateTraditionalCulture(numberCulture, 3792, false);
					goto IL_38B;
				case 85:
					cultureInfo = this.CreateTraditionalCulture(numberCulture, 4160, false);
					goto IL_38B;
				case 87:
					cultureInfo = this.CreateTraditionalCulture(numberCulture, 2406, false);
					goto IL_38B;
				case 88:
					cultureInfo = this.CreateTraditionalCulture(numberCulture, 2534, false);
					goto IL_38B;
				case 89:
					if (lcid == 1113)
					{
						cultureInfo = this.CreateTraditionalCulture(numberCulture, 2406, false);
						goto IL_38B;
					}
					if (lcid == 2137)
					{
						cultureInfo = this.CreateTraditionalCulture(numberCulture, 1776, true);
						goto IL_38B;
					}
					goto IL_38B;
				case 95:
					if (lcid == 1119)
					{
						cultureInfo = this.CreateTraditionalCulture(numberCulture, 1632, true);
						goto IL_38B;
					}
					goto IL_38B;
				case 96:
					if (lcid == 1120)
					{
						cultureInfo = this.CreateTraditionalCulture(numberCulture, 1776, true);
						goto IL_38B;
					}
					if (lcid == 2144)
					{
						cultureInfo = this.CreateTraditionalCulture(numberCulture, 2406, false);
						goto IL_38B;
					}
					goto IL_38B;
				case 97:
					cultureInfo = this.CreateTraditionalCulture(numberCulture, 2406, false);
					goto IL_38B;
				case 99:
					cultureInfo = this.CreateTraditionalCulture(numberCulture, 1776, true);
					goto IL_38B;
				default:
					if (num != 140)
					{
						goto IL_38B;
					}
					cultureInfo = this.CreateTraditionalCulture(numberCulture, 1776, true);
					goto IL_38B;
				}
			}
			cultureInfo = this.CreateTraditionalCulture(numberCulture, 1776, true);
			IL_38B:
			if (cultureInfo == null)
			{
				if (!DigitState.HasLatinDigits(numberCulture))
				{
					cultureInfo = numberCulture;
				}
			}
			else
			{
				this._lastTraditionalCulture = cultureInfo;
			}
			return cultureInfo;
		}

		// Token: 0x06004EA4 RID: 20132 RVA: 0x00138570 File Offset: 0x00137970
		private CultureInfo CreateTraditionalCulture(CultureInfo numberCulture, int firstDigit, bool arabic)
		{
			CultureInfo cultureInfo = (CultureInfo)numberCulture.Clone();
			string[] array = new string[10];
			if (firstDigit < 65536)
			{
				for (int i = 0; i < 10; i++)
				{
					array[i] = new string((char)(firstDigit + i), 1);
				}
			}
			else
			{
				for (int j = 0; j < 10; j++)
				{
					int num = firstDigit + j - 65536;
					array[j] = new string(new char[]
					{
						(char)(num >> 10 | 55296),
						(char)((num & 1023) | 56320)
					});
				}
			}
			cultureInfo.NumberFormat.NativeDigits = array;
			if (arabic)
			{
				cultureInfo.NumberFormat.PercentSymbol = "٪";
				cultureInfo.NumberFormat.NumberDecimalSeparator = "٫";
				cultureInfo.NumberFormat.NumberGroupSeparator = "٬";
			}
			else
			{
				cultureInfo.NumberFormat.PercentSymbol = "%";
				cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
				cultureInfo.NumberFormat.NumberGroupSeparator = ",";
			}
			return cultureInfo;
		}

		// Token: 0x040023BD RID: 9149
		private CultureInfo _lastTraditionalCulture;

		// Token: 0x040023BE RID: 9150
		private NumberSubstitutionMethod _lastMethod;

		// Token: 0x040023BF RID: 9151
		private CultureInfo _lastNumberCulture;

		// Token: 0x040023C0 RID: 9152
		private CultureInfo _digitCulture;

		// Token: 0x040023C1 RID: 9153
		private bool _contextual;
	}
}
