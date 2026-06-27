using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Media.TextFormatting;
using MS.Internal.PresentationCore;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200075E RID: 1886
	internal sealed class TextRunCacheImp
	{
		// Token: 0x06004F5A RID: 20314 RVA: 0x0013B954 File Offset: 0x0013AD54
		internal TextRunCacheImp()
		{
			this._textRunVector = new SpanVector(null);
			this._latestPosition = default(SpanPosition);
		}

		// Token: 0x06004F5B RID: 20315 RVA: 0x0013B980 File Offset: 0x0013AD80
		public void Change(int textSourceCharacterIndex, int addition, int removal)
		{
			if (textSourceCharacterIndex < 0)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < this._textRunVector.Count; i++)
			{
				num += this._textRunVector[i].length;
			}
			if (textSourceCharacterIndex >= num)
			{
				return;
			}
			SpanRider spanRider = new SpanRider(this._textRunVector, this._latestPosition, textSourceCharacterIndex);
			this._latestPosition = spanRider.SpanPosition;
			this._latestPosition = this._textRunVector.SetValue(spanRider.CurrentSpanStart, num - spanRider.CurrentSpanStart, this._textRunVector.Default, this._latestPosition);
		}

		// Token: 0x06004F5C RID: 20316 RVA: 0x0013BA18 File Offset: 0x0013AE18
		internal TextRun FetchTextRun(FormatSettings settings, int cpFetch, int cpFirst, out int offsetToFirstCp, out int runLength)
		{
			SpanRider spanRider = new SpanRider(this._textRunVector, this._latestPosition, cpFetch);
			this._latestPosition = spanRider.SpanPosition;
			TextRun textRun = (TextRun)spanRider.CurrentElement;
			if (textRun == null)
			{
				textRun = settings.TextSource.GetTextRun(cpFetch);
				if (textRun.Length < 1)
				{
					throw new ArgumentOutOfRangeException("textRun.Length", SR.Get("ParameterMustBeGreaterThanZero"));
				}
				Plsrun runType = TextRunInfo.GetRunType(textRun);
				if (runType == Plsrun.Text || runType == Plsrun.InlineObject)
				{
					TextRunProperties properties = textRun.Properties;
					if (properties == null)
					{
						throw new ArgumentException(SR.Get("TextRunPropertiesCannotBeNull"));
					}
					if (properties.FontRenderingEmSize <= 0.0)
					{
						throw new ArgumentException(SR.Get("PropertyOfClassMustBeGreaterThanZero", new object[]
						{
							"FontRenderingEmSize",
							"TextRunProperties"
						}));
					}
					double num = 35791.394066666668;
					if (properties.FontRenderingEmSize > num)
					{
						throw new ArgumentException(SR.Get("PropertyOfClassCannotBeGreaterThan", new object[]
						{
							"FontRenderingEmSize",
							"TextRunProperties",
							num
						}));
					}
					if (CultureMapper.GetSpecificCulture(properties.CultureInfo) == null)
					{
						throw new ArgumentException(SR.Get("PropertyOfClassCannotBeNull", new object[]
						{
							"CultureInfo",
							"TextRunProperties"
						}));
					}
					if (properties.Typeface == null)
					{
						throw new ArgumentException(SR.Get("PropertyOfClassCannotBeNull", new object[]
						{
							"Typeface",
							"TextRunProperties"
						}));
					}
				}
				spanRider.At(cpFetch + textRun.Length - 1);
				this._latestPosition = spanRider.SpanPosition;
				if (spanRider.CurrentElement != this._textRunVector.Default)
				{
					this._latestPosition = this._textRunVector.SetReference(cpFetch, spanRider.CurrentPosition + spanRider.Length - cpFetch, this._textRunVector.Default, this._latestPosition);
				}
				this._latestPosition = this._textRunVector.SetReference(cpFetch, textRun.Length, textRun, this._latestPosition);
				spanRider.At(this._latestPosition, cpFetch);
			}
			if (textRun.Properties != null)
			{
				textRun.Properties.PixelsPerDip = settings.TextSource.PixelsPerDip;
			}
			offsetToFirstCp = spanRider.CurrentPosition - spanRider.CurrentSpanStart;
			runLength = spanRider.Length;
			bool flag = textRun is ITextSymbols;
			if (flag)
			{
				int num2 = 100 - cpFetch + cpFirst;
				if (num2 <= 0)
				{
					num2 = (int)Math.Round(25.0);
				}
				if (runLength > num2)
				{
					if (TextRunInfo.GetRunType(textRun) == Plsrun.Text)
					{
						CharacterBufferReference characterBufferReference = textRun.CharacterBufferReference;
						int num3 = Math.Min(runLength, num2 + 100);
						int num4 = 0;
						bool flag2 = false;
						int i;
						for (i = num2 - 1; i < num3; i += num4)
						{
							CharacterBufferRange unicodeString = new CharacterBufferRange(characterBufferReference.CharacterBuffer, characterBufferReference.OffsetToFirstChar + offsetToFirstCp + i, runLength - i);
							int unicodeScalar = Classification.UnicodeScalar(unicodeString, out num4);
							if (flag2 && !Classification.IsCombining(unicodeScalar) && !Classification.IsJoiner(unicodeScalar))
							{
								break;
							}
							flag2 = !Classification.IsJoiner(unicodeScalar);
						}
						num2 = Math.Min(runLength, i);
					}
					runLength = num2;
				}
			}
			return textRun;
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x0013BD30 File Offset: 0x0013B130
		internal TextSpan<CultureSpecificCharacterBufferRange> GetPrecedingText(TextSource textSource, int cpLimit)
		{
			if (cpLimit > 0)
			{
				SpanRider spanRider = new SpanRider(this._textRunVector, this._latestPosition);
				if (spanRider.At(cpLimit - 1))
				{
					CharacterBufferRange empty = CharacterBufferRange.Empty;
					CultureInfo culture = null;
					TextRun textRun = spanRider.CurrentElement as TextRun;
					if (textRun != null)
					{
						if (TextRunInfo.GetRunType(textRun) == Plsrun.Text && textRun.CharacterBufferReference.CharacterBuffer != null)
						{
							empty = new CharacterBufferRange(textRun.CharacterBufferReference, cpLimit - spanRider.CurrentSpanStart);
							culture = CultureMapper.GetSpecificCulture(textRun.Properties.CultureInfo);
						}
						return new TextSpan<CultureSpecificCharacterBufferRange>(cpLimit - spanRider.CurrentSpanStart, new CultureSpecificCharacterBufferRange(culture, empty));
					}
				}
			}
			return textSource.GetPrecedingText(cpLimit);
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x0013BDD8 File Offset: 0x0013B1D8
		internal IList<TextSpan<TextRun>> GetTextRunSpans()
		{
			IList<TextSpan<TextRun>> list = new List<TextSpan<TextRun>>(this._textRunVector.Count);
			for (int i = 0; i < this._textRunVector.Count; i++)
			{
				Span span = this._textRunVector[i];
				list.Add(new TextSpan<TextRun>(span.length, span.element as TextRun));
			}
			return list;
		}

		// Token: 0x040023FC RID: 9212
		private SpanVector _textRunVector;

		// Token: 0x040023FD RID: 9213
		private SpanPosition _latestPosition;
	}
}
