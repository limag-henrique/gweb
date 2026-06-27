using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using MS.Internal.PresentationCore;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000758 RID: 1880
	internal class SimpleTextLine : TextLine
	{
		// Token: 0x06004EC8 RID: 20168 RVA: 0x001389F8 File Offset: 0x00137DF8
		public static TextLine Create(FormatSettings settings, int cpFirst, int paragraphWidth, double pixelsPerDip)
		{
			ParaProp pap = settings.Pap;
			if (pap.RightToLeft || pap.Justify || (pap.FirstLineInParagraph && pap.TextMarkerProperties != null) || settings.TextIndent != 0 || pap.ParagraphIndent != 0 || pap.LineHeight > 0 || pap.AlwaysCollapsible || (pap.TextDecorations != null && pap.TextDecorations.Count != 0))
			{
				return null;
			}
			int num = cpFirst;
			int num2 = 0;
			int num3 = (pap.Wrap && paragraphWidth > 0) ? paragraphWidth : int.MaxValue;
			int num4 = 0;
			SimpleRun simpleRun = null;
			SimpleRun simpleRun2 = SimpleRun.Create(settings, num, cpFirst, num3, paragraphWidth, num4, pixelsPerDip);
			if (simpleRun2 == null)
			{
				return null;
			}
			if (!simpleRun2.EOT && simpleRun2.IdealWidth <= num3)
			{
				num += simpleRun2.Length;
				num3 -= simpleRun2.IdealWidth;
				num4 += simpleRun2.IdealWidth;
				simpleRun = simpleRun2;
				simpleRun2 = SimpleRun.Create(settings, num, cpFirst, num3, paragraphWidth, num4, pixelsPerDip);
				if (simpleRun2 == null)
				{
					return null;
				}
			}
			int num5 = 0;
			ArrayList runs = new ArrayList(2);
			if (simpleRun != null)
			{
				SimpleTextLine.AddRun(runs, simpleRun, ref num2);
			}
			while (simpleRun2.EOT || simpleRun2.IdealWidth <= num3)
			{
				SimpleTextLine.AddRun(runs, simpleRun2, ref num2);
				if (num2 >= 9600)
				{
					return null;
				}
				simpleRun = simpleRun2;
				num += simpleRun2.Length;
				num3 -= simpleRun2.IdealWidth;
				num4 += simpleRun2.IdealWidth;
				if (simpleRun2.EOT)
				{
					int num6 = 0;
					SimpleTextLine.CollectTrailingSpaces(runs, settings.Formatter, ref num5, ref num6);
					return new SimpleTextLine(settings, cpFirst, paragraphWidth, runs, ref num5, ref num6, pixelsPerDip);
				}
				simpleRun2 = SimpleRun.Create(settings, num, cpFirst, num3, paragraphWidth, num4, pixelsPerDip);
				if (simpleRun2 == null || (simpleRun2.Underline != null && simpleRun != null && simpleRun.Underline != null && !simpleRun.IsUnderlineCompatible(simpleRun2)))
				{
					return null;
				}
			}
			return null;
		}

		// Token: 0x06004EC9 RID: 20169 RVA: 0x00138B9C File Offset: 0x00137F9C
		public SimpleTextLine(FormatSettings settings, int cpFirst, int paragraphWidth, ArrayList runs, ref int trailing, ref int trailingSpaceWidth, double pixelsPerDip) : base(pixelsPerDip)
		{
			int i = 0;
			this._settings = settings;
			double num = 0.0;
			double num2 = 0.0;
			double num3 = 0.0;
			ParaProp pap = settings.Pap;
			TextFormatterImp formatter = settings.Formatter;
			int num4 = 0;
			while (i < runs.Count)
			{
				SimpleRun simpleRun = (SimpleRun)runs[i];
				if (simpleRun.Length > 0)
				{
					if (simpleRun.EOT)
					{
						trailing += simpleRun.Length;
						this._cpLengthEOT += simpleRun.Length;
					}
					else
					{
						num3 = Math.Max(num3, simpleRun.Height);
						num = Math.Max(num, simpleRun.Baseline);
						num2 = Math.Max(num2, simpleRun.Height - simpleRun.Baseline);
					}
					this._cpLength += simpleRun.Length;
					num4 += simpleRun.IdealWidth;
				}
				i++;
			}
			this._baselineOffset = formatter.IdealToReal((double)TextFormatterImp.RealToIdeal(num), base.PixelsPerDip);
			if (num + num2 == num3)
			{
				this._height = formatter.IdealToReal((double)TextFormatterImp.RealToIdeal(num3), base.PixelsPerDip);
			}
			else
			{
				this._height = formatter.IdealToReal((double)(TextFormatterImp.RealToIdeal(num) + TextFormatterImp.RealToIdeal(num2)), base.PixelsPerDip);
			}
			if (this._height <= 0.0)
			{
				this._height = formatter.IdealToReal((double)((int)Math.Round(pap.DefaultTypeface.LineSpacing((double)pap.EmSize, 0.0033333333333333335, base.PixelsPerDip, this._settings.TextFormattingMode))), base.PixelsPerDip);
				this._baselineOffset = formatter.IdealToReal((double)((int)Math.Round(pap.DefaultTypeface.Baseline((double)pap.EmSize, 0.0033333333333333335, base.PixelsPerDip, this._settings.TextFormattingMode))), base.PixelsPerDip);
			}
			this._runs = new SimpleRun[i];
			int j = i - 1;
			int num5 = trailing;
			while (j >= 0)
			{
				SimpleRun simpleRun2 = (SimpleRun)runs[j];
				if (num5 > 0)
				{
					simpleRun2.TrimTrailingUnderline = true;
					num5 -= simpleRun2.Length;
				}
				this._runs[j] = simpleRun2;
				j--;
			}
			this._cpFirst = cpFirst;
			this._trailing = trailing;
			int num6 = num4 - trailingSpaceWidth;
			if (pap.Align != TextAlignment.Left)
			{
				TextAlignment align = pap.Align;
				if (align != TextAlignment.Right)
				{
					if (align == TextAlignment.Center)
					{
						this._idealOffsetUnRounded = (int)Math.Round((double)(paragraphWidth - num6) * 0.5);
						this._offset = formatter.IdealToReal((double)this._idealOffsetUnRounded, base.PixelsPerDip);
					}
				}
				else
				{
					this._idealOffsetUnRounded = paragraphWidth - num6;
					this._offset = formatter.IdealToReal((double)this._idealOffsetUnRounded, base.PixelsPerDip);
				}
			}
			this._width = formatter.IdealToReal((double)num4, base.PixelsPerDip);
			this._widthAtTrailing = formatter.IdealToReal((double)num6, base.PixelsPerDip);
			this._paragraphWidth = formatter.IdealToReal((double)paragraphWidth, base.PixelsPerDip);
			if (paragraphWidth > 0 && this._widthAtTrailing > this._paragraphWidth)
			{
				this._statusFlags |= SimpleTextLine.StatusFlags.HasOverflowed;
			}
		}

		// Token: 0x06004ECA RID: 20170 RVA: 0x00138EDC File Offset: 0x001382DC
		public override void Dispose()
		{
		}

		// Token: 0x06004ECB RID: 20171 RVA: 0x00138EEC File Offset: 0x001382EC
		private static void CollectTrailingSpaces(ArrayList runs, TextFormatterImp formatter, ref int trailing, ref int trailingSpaceWidth)
		{
			int num = (runs != null) ? runs.Count : 0;
			bool flag = true;
			while (num > 0 && flag)
			{
				SimpleRun simpleRun = (SimpleRun)runs[--num];
				flag = simpleRun.CollectTrailingSpaces(formatter, ref trailing, ref trailingSpaceWidth);
			}
		}

		// Token: 0x06004ECC RID: 20172 RVA: 0x00138F30 File Offset: 0x00138330
		private static void AddRun(ArrayList runs, SimpleRun run, ref int nonHiddenLength)
		{
			if (run.Length > 0)
			{
				runs.Add(run);
				if (!run.Ghost)
				{
					nonHiddenLength += run.Length;
				}
			}
		}

		// Token: 0x06004ECD RID: 20173 RVA: 0x00138F64 File Offset: 0x00138364
		private double DistanceFromCp(int currentIndex)
		{
			Invariant.Assert(currentIndex >= this._cpFirst);
			int num = 0;
			int num2 = currentIndex - this._cpFirst;
			foreach (SimpleRun simpleRun in this._runs)
			{
				num += simpleRun.DistanceFromDcp(num2);
				if (num2 <= simpleRun.Length)
				{
					break;
				}
				num2 -= simpleRun.Length;
			}
			return this._settings.Formatter.IdealToReal((double)(num + this._idealOffsetUnRounded), base.PixelsPerDip);
		}

		// Token: 0x06004ECE RID: 20174 RVA: 0x00138FE4 File Offset: 0x001383E4
		public override void Draw(DrawingContext drawingContext, Point origin, InvertAxes inversion)
		{
			if (drawingContext == null)
			{
				throw new ArgumentNullException("drawingContext");
			}
			MatrixTransform matrixTransform = TextFormatterImp.CreateAntiInversionTransform(inversion, this._paragraphWidth, this._height);
			if (matrixTransform == null)
			{
				this.DrawTextLine(drawingContext, origin);
				return;
			}
			drawingContext.PushTransform(matrixTransform);
			try
			{
				this.DrawTextLine(drawingContext, origin);
			}
			finally
			{
				drawingContext.Pop();
			}
		}

		// Token: 0x06004ECF RID: 20175 RVA: 0x00139054 File Offset: 0x00138454
		public override TextLine Collapse(params TextCollapsingProperties[] collapsingPropertiesList)
		{
			if (!this.HasOverflowed)
			{
				return this;
			}
			Invariant.Assert(this._settings != null);
			TextMetrics.FullTextLine fullTextLine = new TextMetrics.FullTextLine(this._settings, this._cpFirst, 0, TextFormatterImp.RealToIdeal(this._paragraphWidth), LineFlags.None);
			if (fullTextLine.HasOverflowed)
			{
				TextLine textLine = fullTextLine.Collapse(collapsingPropertiesList);
				if (textLine != fullTextLine)
				{
					fullTextLine.Dispose();
				}
				return textLine;
			}
			return fullTextLine;
		}

		// Token: 0x06004ED0 RID: 20176 RVA: 0x001390B8 File Offset: 0x001384B8
		private void CheckBoundingBox()
		{
			if ((this._statusFlags & SimpleTextLine.StatusFlags.BoundingBoxComputed) == SimpleTextLine.StatusFlags.None)
			{
				this.DrawTextLine(null, new Point(0.0, 0.0));
			}
		}

		// Token: 0x06004ED1 RID: 20177 RVA: 0x001390F0 File Offset: 0x001384F0
		private void DrawTextLine(DrawingContext drawingContext, Point origin)
		{
			if (this._runs.Length == 0)
			{
				this._boundingBox = Rect.Empty;
				this._statusFlags |= SimpleTextLine.StatusFlags.BoundingBoxComputed;
				return;
			}
			int num = this._idealOffsetUnRounded;
			double num2 = origin.Y + this.Baseline;
			if (drawingContext != null)
			{
				drawingContext.PushGuidelineY1(num2);
			}
			Rect empty = Rect.Empty;
			try
			{
				foreach (SimpleRun simpleRun in this._runs)
				{
					empty.Union(simpleRun.Draw(drawingContext, this._settings.Formatter.IdealToReal((double)num, base.PixelsPerDip) + origin.X, num2, false));
					num += simpleRun.IdealWidth;
				}
			}
			finally
			{
				if (drawingContext != null)
				{
					drawingContext.Pop();
				}
			}
			if (empty.IsEmpty)
			{
				empty = new Rect(this.Start, 0.0, 0.0, 0.0);
			}
			else
			{
				empty.X -= origin.X;
				empty.Y -= origin.Y;
			}
			this._boundingBox = empty;
			this._statusFlags |= SimpleTextLine.StatusFlags.BoundingBoxComputed;
		}

		// Token: 0x06004ED2 RID: 20178 RVA: 0x00139238 File Offset: 0x00138638
		public override CharacterHit GetCharacterHitFromDistance(double distance)
		{
			int num = TextFormatterImp.RealToIdeal(distance) - this._idealOffsetUnRounded;
			int num2 = this._cpFirst;
			if (num < 0)
			{
				return new CharacterHit(this._cpFirst, 0);
			}
			CharacterHit characterHit = default(CharacterHit);
			for (int i = 0; i < this._runs.Length; i++)
			{
				SimpleRun simpleRun = this._runs[i];
				if (!simpleRun.EOT)
				{
					num2 += characterHit.TrailingLength;
					characterHit = simpleRun.DcpFromDistance(num);
					num2 += characterHit.FirstCharacterIndex;
				}
				if (num <= simpleRun.IdealWidth)
				{
					break;
				}
				num -= simpleRun.IdealWidth;
			}
			return new CharacterHit(num2, characterHit.TrailingLength);
		}

		// Token: 0x06004ED3 RID: 20179 RVA: 0x001392D8 File Offset: 0x001386D8
		public override double GetDistanceFromCharacterHit(CharacterHit characterHit)
		{
			TextFormatterImp.VerifyCaretCharacterHit(characterHit, this._cpFirst, this._cpLength);
			return this.DistanceFromCp(characterHit.FirstCharacterIndex + ((characterHit.TrailingLength != 0) ? 1 : 0));
		}

		// Token: 0x06004ED4 RID: 20180 RVA: 0x00139314 File Offset: 0x00138714
		public override CharacterHit GetNextCaretCharacterHit(CharacterHit characterHit)
		{
			TextFormatterImp.VerifyCaretCharacterHit(characterHit, this._cpFirst, this._cpLength);
			int firstCharacterIndex;
			bool flag;
			if (characterHit.TrailingLength == 0)
			{
				flag = this.FindNextVisibleCp(characterHit.FirstCharacterIndex, out firstCharacterIndex);
				if (flag)
				{
					return new CharacterHit(firstCharacterIndex, 1);
				}
			}
			flag = this.FindNextVisibleCp(characterHit.FirstCharacterIndex + 1, out firstCharacterIndex);
			if (flag)
			{
				return new CharacterHit(firstCharacterIndex, 1);
			}
			return characterHit;
		}

		// Token: 0x06004ED5 RID: 20181 RVA: 0x00139378 File Offset: 0x00138778
		public override CharacterHit GetPreviousCaretCharacterHit(CharacterHit characterHit)
		{
			TextFormatterImp.VerifyCaretCharacterHit(characterHit, this._cpFirst, this._cpLength);
			int num = characterHit.FirstCharacterIndex;
			bool flag = characterHit.TrailingLength != 0;
			if (num >= this._cpFirst + this._cpLength)
			{
				num = this._cpFirst + this._cpLength - 1;
				flag = true;
			}
			int firstCharacterIndex;
			bool flag2;
			if (flag)
			{
				flag2 = this.FindPreviousVisibleCp(num, out firstCharacterIndex);
				if (flag2)
				{
					return new CharacterHit(firstCharacterIndex, 0);
				}
			}
			flag2 = this.FindPreviousVisibleCp(num - 1, out firstCharacterIndex);
			if (flag2)
			{
				return new CharacterHit(firstCharacterIndex, 0);
			}
			return characterHit;
		}

		// Token: 0x06004ED6 RID: 20182 RVA: 0x001393FC File Offset: 0x001387FC
		public override CharacterHit GetBackspaceCaretCharacterHit(CharacterHit characterHit)
		{
			return this.GetPreviousCaretCharacterHit(characterHit);
		}

		// Token: 0x06004ED7 RID: 20183 RVA: 0x00139410 File Offset: 0x00138810
		public override IList<TextBounds> GetTextBounds(int firstTextSourceCharacterIndex, int textLength)
		{
			if (textLength == 0)
			{
				throw new ArgumentOutOfRangeException("textLength", SR.Get("ParameterMustBeGreaterThanZero"));
			}
			if (textLength < 0)
			{
				firstTextSourceCharacterIndex += textLength;
				textLength = -textLength;
			}
			if (firstTextSourceCharacterIndex < this._cpFirst)
			{
				textLength += firstTextSourceCharacterIndex - this._cpFirst;
				firstTextSourceCharacterIndex = this._cpFirst;
			}
			if (firstTextSourceCharacterIndex + textLength > this._cpFirst + this._cpLength)
			{
				textLength = this._cpFirst + this._cpLength - firstTextSourceCharacterIndex;
			}
			double distanceFromCharacterHit = this.GetDistanceFromCharacterHit(new CharacterHit(firstTextSourceCharacterIndex, 0));
			double distanceFromCharacterHit2 = this.GetDistanceFromCharacterHit(new CharacterHit(firstTextSourceCharacterIndex + textLength, 0));
			int num = firstTextSourceCharacterIndex - this._cpFirst;
			int num2 = 0;
			IList<TextRunBounds> list = new List<TextRunBounds>(2);
			foreach (SimpleRun simpleRun in this._runs)
			{
				if (!simpleRun.EOT && !simpleRun.Ghost && num2 + simpleRun.Length > num)
				{
					if (num2 >= num + textLength)
					{
						break;
					}
					int num3 = Math.Max(num2, num) + this._cpFirst;
					int num4 = Math.Min(num2 + simpleRun.Length, num + textLength) + this._cpFirst;
					list.Add(new TextRunBounds(new Rect(new Point(this.DistanceFromCp(num3), this._baselineOffset - simpleRun.Baseline), new Point(this.DistanceFromCp(num4), this._baselineOffset - simpleRun.Baseline + simpleRun.Height)), num3, num4, simpleRun.TextRun));
				}
				num2 += simpleRun.Length;
			}
			return new TextBounds[]
			{
				new TextBounds(new Rect(distanceFromCharacterHit, 0.0, distanceFromCharacterHit2 - distanceFromCharacterHit, this._height), FlowDirection.LeftToRight, (list == null || list.Count == 0) ? null : list)
			};
		}

		// Token: 0x06004ED8 RID: 20184 RVA: 0x001395CC File Offset: 0x001389CC
		public override IList<TextSpan<TextRun>> GetTextRunSpans()
		{
			TextSpan<TextRun>[] array = new TextSpan<TextRun>[this._runs.Length];
			for (int i = 0; i < this._runs.Length; i++)
			{
				array[i] = new TextSpan<TextRun>(this._runs[i].Length, this._runs[i].TextRun);
			}
			return array;
		}

		// Token: 0x06004ED9 RID: 20185 RVA: 0x00139620 File Offset: 0x00138A20
		[SecurityCritical]
		public override IEnumerable<IndexedGlyphRun> GetIndexedGlyphRuns()
		{
			List<IndexedGlyphRun> list = new List<IndexedGlyphRun>(this._runs.Length);
			Point origin = new Point(0.0, 0.0);
			int num = this._cpFirst;
			foreach (SimpleRun simpleRun in this._runs)
			{
				if (simpleRun.Length > 0 && !simpleRun.Ghost)
				{
					IList<double> list2;
					if (this._settings.TextFormattingMode == TextFormattingMode.Ideal)
					{
						list2 = new ThousandthOfEmRealDoubles(simpleRun.EmSize, simpleRun.NominalAdvances.Length);
						for (int j = 0; j < list2.Count; j++)
						{
							list2[j] = this._settings.Formatter.IdealToReal((double)simpleRun.NominalAdvances[j], base.PixelsPerDip);
						}
					}
					else
					{
						list2 = new List<double>(simpleRun.NominalAdvances.Length);
						for (int k = 0; k < simpleRun.NominalAdvances.Length; k++)
						{
							list2.Add(this._settings.Formatter.IdealToReal((double)simpleRun.NominalAdvances[k], base.PixelsPerDip));
						}
					}
					GlyphTypeface glyphTypeface = simpleRun.Typeface.TryGetGlyphTypeface();
					Invariant.Assert(glyphTypeface != null);
					GlyphRun glyphRun = glyphTypeface.ComputeUnshapedGlyphRun(origin, new CharacterBufferRange(simpleRun.CharBufferReference, simpleRun.Length), list2, simpleRun.EmSize, (float)base.PixelsPerDip, simpleRun.TextRun.Properties.FontHintingEmSize, simpleRun.Typeface.NullFont, CultureMapper.GetSpecificCulture(simpleRun.TextRun.Properties.CultureInfo), null, this._settings.TextFormattingMode);
					if (glyphRun != null)
					{
						list.Add(new IndexedGlyphRun(num, simpleRun.Length, glyphRun));
					}
				}
				num += simpleRun.Length;
			}
			return list;
		}

		// Token: 0x06004EDA RID: 20186 RVA: 0x001397E0 File Offset: 0x00138BE0
		public override TextLineBreak GetTextLineBreak()
		{
			return null;
		}

		// Token: 0x06004EDB RID: 20187 RVA: 0x001397F0 File Offset: 0x00138BF0
		public override IList<TextCollapsedRange> GetTextCollapsedRanges()
		{
			Invariant.Assert(!this.HasCollapsed);
			return null;
		}

		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x06004EDC RID: 20188 RVA: 0x0013980C File Offset: 0x00138C0C
		public override int Length
		{
			get
			{
				return this._cpLength;
			}
		}

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x06004EDD RID: 20189 RVA: 0x00139820 File Offset: 0x00138C20
		public override int TrailingWhitespaceLength
		{
			get
			{
				return this._trailing;
			}
		}

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x06004EDE RID: 20190 RVA: 0x00139834 File Offset: 0x00138C34
		public override int DependentLength
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x06004EDF RID: 20191 RVA: 0x00139844 File Offset: 0x00138C44
		public override int NewlineLength
		{
			get
			{
				return this._cpLengthEOT;
			}
		}

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x06004EE0 RID: 20192 RVA: 0x00139858 File Offset: 0x00138C58
		public override double Start
		{
			get
			{
				return this._offset;
			}
		}

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x06004EE1 RID: 20193 RVA: 0x0013986C File Offset: 0x00138C6C
		public override double Width
		{
			get
			{
				return this._widthAtTrailing;
			}
		}

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x06004EE2 RID: 20194 RVA: 0x00139880 File Offset: 0x00138C80
		public override double WidthIncludingTrailingWhitespace
		{
			get
			{
				return this._width;
			}
		}

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x06004EE3 RID: 20195 RVA: 0x00139894 File Offset: 0x00138C94
		public override double Height
		{
			get
			{
				return this._height;
			}
		}

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x06004EE4 RID: 20196 RVA: 0x001398A8 File Offset: 0x00138CA8
		public override double TextHeight
		{
			get
			{
				return this._height;
			}
		}

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x06004EE5 RID: 20197 RVA: 0x001398BC File Offset: 0x00138CBC
		public override double Extent
		{
			get
			{
				this.CheckBoundingBox();
				return this._boundingBox.Bottom - this._boundingBox.Top;
			}
		}

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x06004EE6 RID: 20198 RVA: 0x001398E8 File Offset: 0x00138CE8
		public override double Baseline
		{
			get
			{
				return this._baselineOffset;
			}
		}

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x06004EE7 RID: 20199 RVA: 0x001398FC File Offset: 0x00138CFC
		public override double TextBaseline
		{
			get
			{
				return this._baselineOffset;
			}
		}

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x06004EE8 RID: 20200 RVA: 0x00139910 File Offset: 0x00138D10
		public override double MarkerBaseline
		{
			get
			{
				return this.Baseline;
			}
		}

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x06004EE9 RID: 20201 RVA: 0x00139924 File Offset: 0x00138D24
		public override double MarkerHeight
		{
			get
			{
				return this.Height;
			}
		}

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x06004EEA RID: 20202 RVA: 0x00139938 File Offset: 0x00138D38
		public override double OverhangLeading
		{
			get
			{
				this.CheckBoundingBox();
				return this._boundingBox.Left - this.Start;
			}
		}

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x06004EEB RID: 20203 RVA: 0x00139960 File Offset: 0x00138D60
		public override double OverhangTrailing
		{
			get
			{
				this.CheckBoundingBox();
				return this.Start + this.Width - this._boundingBox.Right;
			}
		}

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x06004EEC RID: 20204 RVA: 0x0013998C File Offset: 0x00138D8C
		public override double OverhangAfter
		{
			get
			{
				this.CheckBoundingBox();
				return this._boundingBox.Bottom - this.Height;
			}
		}

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x06004EED RID: 20205 RVA: 0x001399B4 File Offset: 0x00138DB4
		public override bool HasOverflowed
		{
			get
			{
				return (this._statusFlags & SimpleTextLine.StatusFlags.HasOverflowed) > SimpleTextLine.StatusFlags.None;
			}
		}

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x06004EEE RID: 20206 RVA: 0x001399CC File Offset: 0x00138DCC
		public override bool HasCollapsed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004EEF RID: 20207 RVA: 0x001399DC File Offset: 0x00138DDC
		private bool FindNextVisibleCp(int cp, out int cpVisible)
		{
			cpVisible = cp;
			if (cp >= this._cpFirst + this._cpLength)
			{
				return false;
			}
			int i;
			int num;
			this.GetRunIndexAtCp(cp, out i, out num);
			while (i < this._runs.Length)
			{
				if (this._runs[i].IsVisible && !this._runs[i].EOT)
				{
					cpVisible = Math.Max(num, cp);
					return true;
				}
				num += this._runs[i++].Length;
			}
			return false;
		}

		// Token: 0x06004EF0 RID: 20208 RVA: 0x00139A54 File Offset: 0x00138E54
		private bool FindPreviousVisibleCp(int cp, out int cpVisible)
		{
			cpVisible = cp;
			if (cp < this._cpFirst)
			{
				return false;
			}
			int i;
			int num;
			this.GetRunIndexAtCp(cp, out i, out num);
			num += this._runs[i].Length - 1;
			while (i >= 0)
			{
				if (this._runs[i].IsVisible && !this._runs[i].EOT)
				{
					cpVisible = Math.Min(num, cp);
					return true;
				}
				if (this._runs[i].EOT)
				{
					cpVisible = num - this._runs[i].Length + 1;
					return true;
				}
				num -= this._runs[i--].Length;
			}
			return false;
		}

		// Token: 0x06004EF1 RID: 20209 RVA: 0x00139AF4 File Offset: 0x00138EF4
		private void GetRunIndexAtCp(int cp, out int runIndex, out int cpRunStart)
		{
			Invariant.Assert(cp >= this._cpFirst && cp < this._cpFirst + this._cpLength);
			cpRunStart = this._cpFirst;
			runIndex = 0;
			while (runIndex < this._runs.Length && cpRunStart + this._runs[runIndex].Length <= cp)
			{
				int num = cpRunStart;
				SimpleRun[] runs = this._runs;
				int num2 = runIndex;
				runIndex = num2 + 1;
				cpRunStart = num + runs[num2].Length;
			}
		}

		// Token: 0x040023C6 RID: 9158
		private SimpleRun[] _runs;

		// Token: 0x040023C7 RID: 9159
		private int _cpFirst;

		// Token: 0x040023C8 RID: 9160
		private int _cpLength;

		// Token: 0x040023C9 RID: 9161
		private int _cpLengthEOT;

		// Token: 0x040023CA RID: 9162
		private double _widthAtTrailing;

		// Token: 0x040023CB RID: 9163
		private double _width;

		// Token: 0x040023CC RID: 9164
		private double _paragraphWidth;

		// Token: 0x040023CD RID: 9165
		private double _height;

		// Token: 0x040023CE RID: 9166
		private double _offset;

		// Token: 0x040023CF RID: 9167
		private int _idealOffsetUnRounded;

		// Token: 0x040023D0 RID: 9168
		private double _baselineOffset;

		// Token: 0x040023D1 RID: 9169
		private int _trailing;

		// Token: 0x040023D2 RID: 9170
		private Rect _boundingBox;

		// Token: 0x040023D3 RID: 9171
		private SimpleTextLine.StatusFlags _statusFlags;

		// Token: 0x040023D4 RID: 9172
		private FormatSettings _settings;

		// Token: 0x020009DF RID: 2527
		[Flags]
		private enum StatusFlags
		{
			// Token: 0x04002EAD RID: 11949
			None = 0,
			// Token: 0x04002EAE RID: 11950
			BoundingBoxComputed = 1,
			// Token: 0x04002EAF RID: 11951
			HasOverflowed = 2
		}
	}
}
