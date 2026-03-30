using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Markup;
using MS.Internal.FontFace;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Define quais <see cref="T:System.Windows.Media.FontFamily" /> usar para um conjunto de pontos de código Unicode e uma linguagem específica de cultura especificados.</summary>
	// Token: 0x02000390 RID: 912
	public class FontFamilyMap
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.FontFamilyMap" />.</summary>
		// Token: 0x060021CA RID: 8650 RVA: 0x00088CCC File Offset: 0x000880CC
		public FontFamilyMap() : this(0, 1114111, null, null, 1.0)
		{
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x00088CF0 File Offset: 0x000880F0
		internal FontFamilyMap(int firstChar, int lastChar, XmlLanguage language, string targetFamilyName, double scaleInEm)
		{
			if (firstChar == 0 && lastChar == 1114111)
			{
				this._ranges = FontFamilyMap._defaultRanges;
			}
			else
			{
				this._ranges = new FontFamilyMap.Range[]
				{
					new FontFamilyMap.Range(firstChar, lastChar)
				};
			}
			this._language = language;
			this._scaleInEm = scaleInEm;
			this._targetFamilyName = targetFamilyName;
		}

		/// <summary>Obtém ou define um valor de cadeia de caracteres que representa um ou mais intervalos de ponto de código Unicode.</summary>
		/// <returns>Um <see cref="T:System.String" /> valor que representa os intervalos de ponto de código Unicode. O valor padrão é "0000 10ffff".</returns>
		/// <exception cref="T:System.FormatException">Intervalo Unicode inválido.</exception>
		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060021CD RID: 8653 RVA: 0x00088D70 File Offset: 0x00088170
		// (set) Token: 0x060021CC RID: 8652 RVA: 0x00088D48 File Offset: 0x00088148
		[DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
		public string Unicode
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < this._ranges.Length; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(',');
					}
					stringBuilder.AppendFormat(NumberFormatInfo.InvariantInfo, "{0:x4}-{1:x4}", new object[]
					{
						this._ranges[i].First,
						this._ranges[i].Last
					});
				}
				return stringBuilder.ToString();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._ranges = FontFamilyMap.ParseUnicodeRanges(value);
			}
		}

		/// <summary>Obtém ou define o nome da família de fontes de destino para a qual o intervalo Unicode se aplica.</summary>
		/// <returns>Um <see cref="T:System.String" /> valor que representa o nome da família de fonte. O valor padrão é um <see langword="null" /> cadeia de caracteres.</returns>
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x00088DEC File Offset: 0x000881EC
		// (set) Token: 0x060021CF RID: 8655 RVA: 0x00088E00 File Offset: 0x00088200
		[DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
		public string Target
		{
			get
			{
				return this._targetFamilyName;
			}
			set
			{
				this._targetFamilyName = value;
			}
		}

		/// <summary>Obtém ou define o fator de escala de fonte para o destino <see cref="T:System.Windows.Media.FontFamily" />.</summary>
		/// <returns>Um <see cref="T:System.Double" /> valor que representa o fator de escala. O valor padrão é 1.0.</returns>
		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x00088E14 File Offset: 0x00088214
		// (set) Token: 0x060021D1 RID: 8657 RVA: 0x00088E28 File Offset: 0x00088228
		public double Scale
		{
			get
			{
				return this._scaleInEm;
			}
			set
			{
				CompositeFontParser.VerifyPositiveMultiplierOfEm("Scale", ref value);
				this._scaleInEm = value;
			}
		}

		/// <summary>Obtém ou define o idioma específico da cultura para o <see cref="T:System.Windows.Media.FontFamilyMap" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Markup.XmlLanguage" /> valor que representa o idioma específicas da cultura. O valor padrão é um <see langword="null" /> cadeia de caracteres.</returns>
		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x00088E48 File Offset: 0x00088248
		// (set) Token: 0x060021D3 RID: 8659 RVA: 0x00088E5C File Offset: 0x0008825C
		public XmlLanguage Language
		{
			get
			{
				return this._language;
			}
			set
			{
				this._language = ((value == XmlLanguage.Empty) ? null : value);
				this._language = value;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060021D4 RID: 8660 RVA: 0x00088E84 File Offset: 0x00088284
		internal bool IsSimpleFamilyMap
		{
			get
			{
				return this._language == null && this._scaleInEm == 1.0 && this._ranges == FontFamilyMap._defaultRanges;
			}
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x00088EBC File Offset: 0x000882BC
		internal static bool MatchLanguage(XmlLanguage familyMapLanguage, XmlLanguage language)
		{
			return familyMapLanguage == null || (language != null && familyMapLanguage.RangeIncludes(language));
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x00088EDC File Offset: 0x000882DC
		internal static bool MatchCulture(XmlLanguage familyMapLanguage, CultureInfo culture)
		{
			return familyMapLanguage == null || (culture != null && familyMapLanguage.RangeIncludes(culture));
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x00088EFC File Offset: 0x000882FC
		internal FontFamilyMap.Range[] Ranges
		{
			get
			{
				return this._ranges;
			}
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x00088F10 File Offset: 0x00088310
		private static void ThrowInvalidUnicodeRange()
		{
			throw new FormatException(SR.Get("CompositeFontInvalidUnicodeRange"));
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x00088F2C File Offset: 0x0008832C
		private static FontFamilyMap.Range[] ParseUnicodeRanges(string unicodeRanges)
		{
			List<FontFamilyMap.Range> list = new List<FontFamilyMap.Range>(3);
			for (int i = 0; i < unicodeRanges.Length; i++)
			{
				int num;
				if (!FontFamilyMap.ParseHexNumber(unicodeRanges, ref i, out num))
				{
					FontFamilyMap.ThrowInvalidUnicodeRange();
				}
				int num2 = num;
				if (i < unicodeRanges.Length)
				{
					if (unicodeRanges[i] == '?')
					{
						do
						{
							num *= 16;
							num2 = num2 * 16 + 15;
							i++;
							if (i >= unicodeRanges.Length || unicodeRanges[i] != '?')
							{
								break;
							}
						}
						while (num2 <= 1114111);
					}
					else if (unicodeRanges[i] == '-')
					{
						i++;
						if (!FontFamilyMap.ParseHexNumber(unicodeRanges, ref i, out num2))
						{
							FontFamilyMap.ThrowInvalidUnicodeRange();
						}
					}
				}
				if (num > num2 || num2 > 1114111 || (i < unicodeRanges.Length && unicodeRanges[i] != ','))
				{
					FontFamilyMap.ThrowInvalidUnicodeRange();
				}
				list.Add(new FontFamilyMap.Range(num, num2));
			}
			return list.ToArray();
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x00089008 File Offset: 0x00088408
		internal static bool ParseHexNumber(string numString, ref int index, out int number)
		{
			while (index < numString.Length && numString[index] == ' ')
			{
				index++;
			}
			int num = index;
			number = 0;
			while (index < numString.Length)
			{
				int num2 = (int)numString[index];
				if (num2 >= 48 && num2 <= 57)
				{
					number = number * 16 + (num2 - 48);
					index++;
				}
				else
				{
					num2 |= 32;
					if (num2 < 97 || num2 > 102)
					{
						break;
					}
					number = number * 16 + (num2 - 87);
					index++;
				}
			}
			bool result = index > num;
			while (index < numString.Length && numString[index] == ' ')
			{
				index++;
			}
			return result;
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000890B4 File Offset: 0x000884B4
		internal bool InRange(int ch)
		{
			for (int i = 0; i < this._ranges.Length; i++)
			{
				FontFamilyMap.Range range = this._ranges[i];
				if (range.InRange(ch))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040010D6 RID: 4310
		private FontFamilyMap.Range[] _ranges;

		// Token: 0x040010D7 RID: 4311
		private XmlLanguage _language;

		// Token: 0x040010D8 RID: 4312
		private double _scaleInEm;

		// Token: 0x040010D9 RID: 4313
		private string _targetFamilyName;

		// Token: 0x040010DA RID: 4314
		internal const int LastUnicodeScalar = 1114111;

		// Token: 0x040010DB RID: 4315
		private static readonly FontFamilyMap.Range[] _defaultRanges = new FontFamilyMap.Range[]
		{
			new FontFamilyMap.Range(0, 1114111)
		};

		// Token: 0x040010DC RID: 4316
		internal static readonly FontFamilyMap Default = new FontFamilyMap(0, 1114111, null, null, 1.0);

		// Token: 0x02000869 RID: 2153
		internal class Range
		{
			// Token: 0x06005747 RID: 22343 RVA: 0x00164C6C File Offset: 0x0016406C
			internal Range(int first, int last)
			{
				this._first = first;
				this._delta = (uint)(last - this._first);
			}

			// Token: 0x06005748 RID: 22344 RVA: 0x00164C94 File Offset: 0x00164094
			internal bool InRange(int ch)
			{
				return ch - this._first <= (int)this._delta;
			}

			// Token: 0x170011FB RID: 4603
			// (get) Token: 0x06005749 RID: 22345 RVA: 0x00164CB4 File Offset: 0x001640B4
			internal int First
			{
				get
				{
					return this._first;
				}
			}

			// Token: 0x170011FC RID: 4604
			// (get) Token: 0x0600574A RID: 22346 RVA: 0x00164CC8 File Offset: 0x001640C8
			internal int Last
			{
				get
				{
					return this._first + (int)this._delta;
				}
			}

			// Token: 0x170011FD RID: 4605
			// (get) Token: 0x0600574B RID: 22347 RVA: 0x00164CE4 File Offset: 0x001640E4
			internal uint Delta
			{
				get
				{
					return this._delta;
				}
			}

			// Token: 0x04002865 RID: 10341
			private int _first;

			// Token: 0x04002866 RID: 10342
			private uint _delta;
		}
	}
}
