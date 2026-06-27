using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Markup;
using System.Windows.Media;
using MS.Internal.Media;
using MS.Internal.PresentationCore;

namespace MS.Internal
{
	// Token: 0x020006A1 RID: 1697
	internal static class Parsers
	{
		// Token: 0x06004A4A RID: 19018 RVA: 0x00120FB4 File Offset: 0x001203B4
		private static int ParseHexChar(char c)
		{
			if (c >= '0' && c <= '9')
			{
				return (int)(c - '0');
			}
			if (c >= 'a' && c <= 'f')
			{
				return (int)(c - 'a' + '\n');
			}
			if (c >= 'A' && c <= 'F')
			{
				return (int)(c - 'A' + '\n');
			}
			throw new FormatException(SR.Get("Parsers_IllegalToken"));
		}

		// Token: 0x06004A4B RID: 19019 RVA: 0x00121008 File Offset: 0x00120408
		private static Color ParseHexColor(string trimmedColor)
		{
			int num = 255;
			int num2;
			int num3;
			int num4;
			if (trimmedColor.Length > 7)
			{
				num = Parsers.ParseHexChar(trimmedColor[1]) * 16 + Parsers.ParseHexChar(trimmedColor[2]);
				num2 = Parsers.ParseHexChar(trimmedColor[3]) * 16 + Parsers.ParseHexChar(trimmedColor[4]);
				num3 = Parsers.ParseHexChar(trimmedColor[5]) * 16 + Parsers.ParseHexChar(trimmedColor[6]);
				num4 = Parsers.ParseHexChar(trimmedColor[7]) * 16 + Parsers.ParseHexChar(trimmedColor[8]);
			}
			else if (trimmedColor.Length > 5)
			{
				num2 = Parsers.ParseHexChar(trimmedColor[1]) * 16 + Parsers.ParseHexChar(trimmedColor[2]);
				num3 = Parsers.ParseHexChar(trimmedColor[3]) * 16 + Parsers.ParseHexChar(trimmedColor[4]);
				num4 = Parsers.ParseHexChar(trimmedColor[5]) * 16 + Parsers.ParseHexChar(trimmedColor[6]);
			}
			else if (trimmedColor.Length > 4)
			{
				num = Parsers.ParseHexChar(trimmedColor[1]);
				num += num * 16;
				num2 = Parsers.ParseHexChar(trimmedColor[2]);
				num2 += num2 * 16;
				num3 = Parsers.ParseHexChar(trimmedColor[3]);
				num3 += num3 * 16;
				num4 = Parsers.ParseHexChar(trimmedColor[4]);
				num4 += num4 * 16;
			}
			else
			{
				num2 = Parsers.ParseHexChar(trimmedColor[1]);
				num2 += num2 * 16;
				num3 = Parsers.ParseHexChar(trimmedColor[2]);
				num3 += num3 * 16;
				num4 = Parsers.ParseHexChar(trimmedColor[3]);
				num4 += num4 * 16;
			}
			return Color.FromArgb((byte)num, (byte)num2, (byte)num3, (byte)num4);
		}

		// Token: 0x06004A4C RID: 19020 RVA: 0x001211AC File Offset: 0x001205AC
		private static Color ParseContextColor(string trimmedColor, IFormatProvider formatProvider, ITypeDescriptorContext context)
		{
			if (!trimmedColor.StartsWith("ContextColor ", StringComparison.OrdinalIgnoreCase))
			{
				throw new FormatException(SR.Get("Parsers_IllegalToken"));
			}
			string text = trimmedColor.Substring("ContextColor ".Length);
			text = text.Trim();
			string[] array = text.Split(new char[]
			{
				' '
			});
			if (array.GetLength(0) < 2)
			{
				throw new FormatException(SR.Get("Parsers_IllegalToken"));
			}
			text = text.Substring(array[0].Length);
			TokenizerHelper tokenizerHelper = new TokenizerHelper(text, formatProvider);
			string[] array2 = text.Split(new char[]
			{
				',',
				' '
			}, StringSplitOptions.RemoveEmptyEntries);
			int length = array2.GetLength(0);
			float a = Convert.ToSingle(tokenizerHelper.NextTokenRequired(), formatProvider);
			float[] array3 = new float[length - 1];
			for (int i = 0; i < length - 1; i++)
			{
				array3[i] = Convert.ToSingle(tokenizerHelper.NextTokenRequired(), formatProvider);
			}
			string inputString = array[0];
			UriHolder uriFromUriContext = TypeConverterHelper.GetUriFromUriContext(context, inputString);
			Uri profileUri;
			if (uriFromUriContext.BaseUri != null)
			{
				profileUri = new Uri(uriFromUriContext.BaseUri, uriFromUriContext.OriginalUri);
			}
			else
			{
				profileUri = uriFromUriContext.OriginalUri;
			}
			Color result = Color.FromAValues(a, array3, profileUri);
			if (result.ColorContext.NumChannels != array3.Length)
			{
				throw new FormatException(SR.Get("Parsers_IllegalToken"));
			}
			return result;
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x001212FC File Offset: 0x001206FC
		private static Color ParseScRgbColor(string trimmedColor, IFormatProvider formatProvider)
		{
			if (!trimmedColor.StartsWith("sc#", StringComparison.Ordinal))
			{
				throw new FormatException(SR.Get("Parsers_IllegalToken"));
			}
			string str = trimmedColor.Substring(3, trimmedColor.Length - 3);
			TokenizerHelper tokenizerHelper = new TokenizerHelper(str, formatProvider);
			float[] array = new float[4];
			for (int i = 0; i < 3; i++)
			{
				array[i] = Convert.ToSingle(tokenizerHelper.NextTokenRequired(), formatProvider);
			}
			if (!tokenizerHelper.NextToken())
			{
				return Color.FromScRgb(1f, array[0], array[1], array[2]);
			}
			array[3] = Convert.ToSingle(tokenizerHelper.GetCurrentToken(), formatProvider);
			if (tokenizerHelper.NextToken())
			{
				throw new FormatException(SR.Get("Parsers_IllegalToken"));
			}
			return Color.FromScRgb(array[0], array[1], array[2], array[3]);
		}

		// Token: 0x06004A4E RID: 19022 RVA: 0x001213B8 File Offset: 0x001207B8
		internal static Color ParseColor(string color, IFormatProvider formatProvider)
		{
			return Parsers.ParseColor(color, formatProvider, null);
		}

		// Token: 0x06004A4F RID: 19023 RVA: 0x001213D0 File Offset: 0x001207D0
		internal static Color ParseColor(string color, IFormatProvider formatProvider, ITypeDescriptorContext context)
		{
			bool flag;
			bool flag2;
			bool flag3;
			bool flag4;
			string text = KnownColors.MatchColor(color, out flag, out flag2, out flag3, out flag4);
			if (!flag && !flag2 && !flag4 && !flag3)
			{
				throw new FormatException(SR.Get("Parsers_IllegalToken"));
			}
			if (flag2)
			{
				return Parsers.ParseHexColor(text);
			}
			if (flag3)
			{
				return Parsers.ParseContextColor(text, formatProvider, context);
			}
			if (flag4)
			{
				return Parsers.ParseScRgbColor(text, formatProvider);
			}
			KnownColor knownColor = KnownColors.ColorStringToKnownColor(text);
			if (knownColor == KnownColor.UnknownColor)
			{
				throw new FormatException(SR.Get("Parsers_IllegalToken"));
			}
			return Color.FromUInt32((uint)knownColor);
		}

		// Token: 0x06004A50 RID: 19024 RVA: 0x00121450 File Offset: 0x00120850
		internal static Brush ParseBrush(string brush, IFormatProvider formatProvider, ITypeDescriptorContext context)
		{
			bool flag;
			bool flag2;
			bool flag3;
			bool flag4;
			string text = KnownColors.MatchColor(brush, out flag, out flag2, out flag3, out flag4);
			if (text.Length == 0)
			{
				throw new FormatException(SR.Get("Parser_Empty"));
			}
			if (flag2)
			{
				return new SolidColorBrush(Parsers.ParseHexColor(text));
			}
			if (flag3)
			{
				return new SolidColorBrush(Parsers.ParseContextColor(text, formatProvider, context));
			}
			if (flag4)
			{
				return new SolidColorBrush(Parsers.ParseScRgbColor(text, formatProvider));
			}
			if (flag)
			{
				SolidColorBrush solidColorBrush = KnownColors.ColorStringToKnownBrush(text);
				if (solidColorBrush != null)
				{
					return solidColorBrush;
				}
			}
			throw new FormatException(SR.Get("Parsers_IllegalToken"));
		}

		// Token: 0x06004A51 RID: 19025 RVA: 0x001214D4 File Offset: 0x001208D4
		internal static Transform ParseTransform(string transformString, IFormatProvider formatProvider)
		{
			Matrix matrix = Matrix.Parse(transformString);
			return new MatrixTransform(matrix);
		}

		// Token: 0x06004A52 RID: 19026 RVA: 0x001214F0 File Offset: 0x001208F0
		internal static PathFigureCollection ParsePathFigureCollection(string pathString, IFormatProvider formatProvider)
		{
			PathStreamGeometryContext pathStreamGeometryContext = new PathStreamGeometryContext();
			AbbreviatedGeometryParser abbreviatedGeometryParser = new AbbreviatedGeometryParser();
			abbreviatedGeometryParser.ParseToGeometryContext(pathStreamGeometryContext, pathString, 0);
			PathGeometry pathGeometry = pathStreamGeometryContext.GetPathGeometry();
			return pathGeometry.Figures;
		}

		// Token: 0x06004A53 RID: 19027 RVA: 0x00121520 File Offset: 0x00120920
		internal static object DeserializeStreamGeometry(BinaryReader reader)
		{
			StreamGeometry streamGeometry = new StreamGeometry();
			using (StreamGeometryContext streamGeometryContext = streamGeometry.Open())
			{
				ParserStreamGeometryContext.Deserialize(reader, streamGeometryContext, streamGeometry);
			}
			streamGeometry.Freeze();
			return streamGeometry;
		}

		// Token: 0x06004A54 RID: 19028 RVA: 0x00121574 File Offset: 0x00120974
		internal static void PathMinilanguageToBinary(BinaryWriter bw, string stringValue)
		{
			ParserStreamGeometryContext parserStreamGeometryContext = new ParserStreamGeometryContext(bw);
			FillRule fillRule = FillRule.EvenOdd;
			Parsers.ParseStringToStreamGeometryContext(parserStreamGeometryContext, stringValue, TypeConverterHelper.InvariantEnglishUS, ref fillRule);
			parserStreamGeometryContext.SetFillRule(fillRule);
			parserStreamGeometryContext.MarkEOF();
		}

		// Token: 0x06004A55 RID: 19029 RVA: 0x001215A8 File Offset: 0x001209A8
		internal static Geometry ParseGeometry(string pathString, IFormatProvider formatProvider)
		{
			FillRule fillRule = FillRule.EvenOdd;
			StreamGeometry streamGeometry = new StreamGeometry();
			StreamGeometryContext context = streamGeometry.Open();
			Parsers.ParseStringToStreamGeometryContext(context, pathString, formatProvider, ref fillRule);
			streamGeometry.FillRule = fillRule;
			streamGeometry.Freeze();
			return streamGeometry;
		}

		// Token: 0x06004A56 RID: 19030 RVA: 0x001215DC File Offset: 0x001209DC
		private static void ParseStringToStreamGeometryContext(StreamGeometryContext context, string pathString, IFormatProvider formatProvider, ref FillRule fillRule)
		{
			try
			{
				if (pathString != null)
				{
					int num = 0;
					while (num < pathString.Length && char.IsWhiteSpace(pathString, num))
					{
						num++;
					}
					if (num < pathString.Length && pathString[num] == 'F')
					{
						num++;
						while (num < pathString.Length && char.IsWhiteSpace(pathString, num))
						{
							num++;
						}
						if (num == pathString.Length || (pathString[num] != '0' && pathString[num] != '1'))
						{
							throw new FormatException(SR.Get("Parsers_IllegalToken"));
						}
						fillRule = ((pathString[num] == '0') ? FillRule.EvenOdd : FillRule.Nonzero);
						num++;
					}
					AbbreviatedGeometryParser abbreviatedGeometryParser = new AbbreviatedGeometryParser();
					abbreviatedGeometryParser.ParseToGeometryContext(context, pathString, num);
				}
			}
			finally
			{
				if (context != null)
				{
					((IDisposable)context).Dispose();
				}
			}
		}

		// Token: 0x04001F6A RID: 8042
		private const int s_zeroChar = 48;

		// Token: 0x04001F6B RID: 8043
		private const int s_aLower = 97;

		// Token: 0x04001F6C RID: 8044
		private const int s_aUpper = 65;

		// Token: 0x04001F6D RID: 8045
		internal const string s_ContextColor = "ContextColor ";

		// Token: 0x04001F6E RID: 8046
		internal const string s_ContextColorNoSpace = "ContextColor";
	}
}
