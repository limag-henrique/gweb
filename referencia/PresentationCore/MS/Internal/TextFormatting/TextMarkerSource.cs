using System;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200075B RID: 1883
	internal sealed class TextMarkerSource : TextSource
	{
		// Token: 0x06004F28 RID: 20264 RVA: 0x0013AF90 File Offset: 0x0013A390
		internal TextMarkerSource(TextParagraphProperties textParagraphProperties, TextMarkerStyle markerStyle, int autoNumberingIndex)
		{
			this._textParagraphProperties = textParagraphProperties;
			TextRunProperties defaultTextRunProperties = this._textParagraphProperties.DefaultTextRunProperties;
			base.PixelsPerDip = defaultTextRunProperties.PixelsPerDip;
			string text = null;
			if (TextMarkerSource.IsKnownSymbolMarkerStyle(markerStyle))
			{
				switch (markerStyle)
				{
				case TextMarkerStyle.Disc:
					text = "\u009f";
					break;
				case TextMarkerStyle.Circle:
					text = "¡";
					break;
				case TextMarkerStyle.Square:
					text = "q";
					break;
				case TextMarkerStyle.Box:
					text = "§";
					break;
				}
				Typeface typeface = defaultTextRunProperties.Typeface;
				this._textRunProperties = new GenericTextRunProperties(new Typeface(new FontFamily("Wingdings"), typeface.Style, typeface.Weight, typeface.Stretch), defaultTextRunProperties.FontRenderingEmSize, defaultTextRunProperties.FontHintingEmSize, base.PixelsPerDip, defaultTextRunProperties.TextDecorations, defaultTextRunProperties.ForegroundBrush, defaultTextRunProperties.BackgroundBrush, defaultTextRunProperties.BaselineAlignment, CultureMapper.GetSpecificCulture(defaultTextRunProperties.CultureInfo), null);
			}
			else if (TextMarkerSource.IsKnownIndexMarkerStyle(markerStyle))
			{
				this._textRunProperties = defaultTextRunProperties;
				switch (markerStyle)
				{
				case TextMarkerStyle.LowerRoman:
					text = TextMarkerSource.ConvertNumberToRomanString(autoNumberingIndex, false);
					break;
				case TextMarkerStyle.UpperRoman:
					text = TextMarkerSource.ConvertNumberToRomanString(autoNumberingIndex, true);
					break;
				case TextMarkerStyle.LowerLatin:
					this._characterArray = TextMarkerSource.ConvertNumberToString(autoNumberingIndex, true, "abcdefghijklmnopqrstuvwxyz");
					break;
				case TextMarkerStyle.UpperLatin:
					this._characterArray = TextMarkerSource.ConvertNumberToString(autoNumberingIndex, true, "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
					break;
				case TextMarkerStyle.Decimal:
					this._characterArray = TextMarkerSource.ConvertNumberToString(autoNumberingIndex, false, "0123456789");
					break;
				}
			}
			if (text != null)
			{
				this._characterArray = text.ToCharArray();
			}
		}

		// Token: 0x06004F29 RID: 20265 RVA: 0x0013B104 File Offset: 0x0013A504
		public override TextRun GetTextRun(int textSourceCharacterIndex)
		{
			if (textSourceCharacterIndex < this._characterArray.Length)
			{
				this._textRunProperties.PixelsPerDip = base.PixelsPerDip;
				return new TextCharacters(this._characterArray, textSourceCharacterIndex, this._characterArray.Length - textSourceCharacterIndex, this._textRunProperties);
			}
			return new TextEndOfParagraph(1);
		}

		// Token: 0x06004F2A RID: 20266 RVA: 0x0013B150 File Offset: 0x0013A550
		public override TextSpan<CultureSpecificCharacterBufferRange> GetPrecedingText(int textSourceCharacterIndexLimit)
		{
			CharacterBufferRange empty = CharacterBufferRange.Empty;
			if (textSourceCharacterIndexLimit > 0)
			{
				empty = new CharacterBufferRange(new CharacterBufferReference(this._characterArray, 0), Math.Min(this._characterArray.Length, textSourceCharacterIndexLimit));
			}
			return new TextSpan<CultureSpecificCharacterBufferRange>(textSourceCharacterIndexLimit, new CultureSpecificCharacterBufferRange(CultureMapper.GetSpecificCulture(this._textRunProperties.CultureInfo), empty));
		}

		// Token: 0x06004F2B RID: 20267 RVA: 0x0013B1A4 File Offset: 0x0013A5A4
		public override int GetTextEffectCharacterIndexFromTextSourceCharacterIndex(int textSourceCharacterIndex)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004F2C RID: 20268 RVA: 0x0013B1B8 File Offset: 0x0013A5B8
		private static char[] ConvertNumberToString(int number, bool oneBased, string numericSymbols)
		{
			if (oneBased)
			{
				number--;
			}
			int length = numericSymbols.Length;
			char[] array;
			if (number < length)
			{
				array = new char[]
				{
					numericSymbols[number],
					'.'
				};
			}
			else
			{
				int num = oneBased ? 1 : 0;
				int num2 = 1;
				long num3 = (long)length;
				long num4 = (long)length;
				while ((long)number >= num3)
				{
					num4 *= (long)length;
					num3 = num4 + num3 * (long)num;
					num2++;
				}
				array = new char[num2 + 1];
				array[num2] = '.';
				for (int i = num2 - 1; i >= 0; i--)
				{
					array[i] = numericSymbols[number % length];
					number = number / length - num;
				}
			}
			return array;
		}

		// Token: 0x06004F2D RID: 20269 RVA: 0x0013B254 File Offset: 0x0013A654
		private static string ConvertNumberToRomanString(int number, bool uppercase)
		{
			if (number > 3999)
			{
				return number.ToString(CultureInfo.InvariantCulture);
			}
			StringBuilder stringBuilder = new StringBuilder();
			TextMarkerSource.AddRomanNumeric(stringBuilder, number / 1000, TextMarkerSource.RomanNumerics[uppercase ? 1 : 0][0]);
			number %= 1000;
			TextMarkerSource.AddRomanNumeric(stringBuilder, number / 100, TextMarkerSource.RomanNumerics[uppercase ? 1 : 0][1]);
			number %= 100;
			TextMarkerSource.AddRomanNumeric(stringBuilder, number / 10, TextMarkerSource.RomanNumerics[uppercase ? 1 : 0][2]);
			number %= 10;
			TextMarkerSource.AddRomanNumeric(stringBuilder, number, TextMarkerSource.RomanNumerics[uppercase ? 1 : 0][3]);
			stringBuilder.Append('.');
			return stringBuilder.ToString();
		}

		// Token: 0x06004F2E RID: 20270 RVA: 0x0013B304 File Offset: 0x0013A704
		private static void AddRomanNumeric(StringBuilder builder, int number, string oneFiveTen)
		{
			if (number >= 1 && number <= 9)
			{
				if (number == 4 || number == 9)
				{
					builder.Append(oneFiveTen[0]);
				}
				if (number == 9)
				{
					builder.Append(oneFiveTen[2]);
					return;
				}
				if (number >= 4)
				{
					builder.Append(oneFiveTen[1]);
				}
				int num = number % 5;
				while (num > 0 && num < 4)
				{
					builder.Append(oneFiveTen[0]);
					num--;
				}
			}
		}

		// Token: 0x06004F2F RID: 20271 RVA: 0x0013B378 File Offset: 0x0013A778
		internal static bool IsKnownSymbolMarkerStyle(TextMarkerStyle markerStyle)
		{
			return markerStyle == TextMarkerStyle.Disc || markerStyle == TextMarkerStyle.Circle || markerStyle == TextMarkerStyle.Square || markerStyle == TextMarkerStyle.Box;
		}

		// Token: 0x06004F30 RID: 20272 RVA: 0x0013B398 File Offset: 0x0013A798
		internal static bool IsKnownIndexMarkerStyle(TextMarkerStyle markerStyle)
		{
			return markerStyle == TextMarkerStyle.Decimal || markerStyle == TextMarkerStyle.LowerLatin || markerStyle == TextMarkerStyle.UpperLatin || markerStyle == TextMarkerStyle.LowerRoman || markerStyle == TextMarkerStyle.UpperRoman;
		}

		// Token: 0x040023E4 RID: 9188
		private char[] _characterArray;

		// Token: 0x040023E5 RID: 9189
		private TextRunProperties _textRunProperties;

		// Token: 0x040023E6 RID: 9190
		private TextParagraphProperties _textParagraphProperties;

		// Token: 0x040023E7 RID: 9191
		private const char NumberSuffix = '.';

		// Token: 0x040023E8 RID: 9192
		private const string DecimalNumerics = "0123456789";

		// Token: 0x040023E9 RID: 9193
		private const string LowerLatinNumerics = "abcdefghijklmnopqrstuvwxyz";

		// Token: 0x040023EA RID: 9194
		private const string UpperLatinNumerics = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		// Token: 0x040023EB RID: 9195
		private static string[][] RomanNumerics = new string[][]
		{
			new string[]
			{
				"m??",
				"cdm",
				"xlc",
				"ivx"
			},
			new string[]
			{
				"M??",
				"CDM",
				"XLC",
				"IVX"
			}
		};
	}
}
