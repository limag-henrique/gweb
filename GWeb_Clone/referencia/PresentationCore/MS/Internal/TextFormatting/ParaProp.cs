using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200075C RID: 1884
	internal sealed class ParaProp
	{
		// Token: 0x06004F32 RID: 20274 RVA: 0x0013B428 File Offset: 0x0013A828
		internal ParaProp(TextFormatterImp formatter, TextParagraphProperties paragraphProperties, bool optimalBreak)
		{
			this._paragraphProperties = paragraphProperties;
			this._emSize = TextFormatterImp.RealToIdeal(paragraphProperties.DefaultTextRunProperties.FontRenderingEmSize);
			this._indent = TextFormatterImp.RealToIdeal(paragraphProperties.Indent);
			this._paragraphIndent = TextFormatterImp.RealToIdeal(paragraphProperties.ParagraphIndent);
			this._height = TextFormatterImp.RealToIdeal(paragraphProperties.LineHeight);
			if (this._paragraphProperties.FlowDirection == FlowDirection.RightToLeft)
			{
				this._statusFlags |= ParaProp.StatusFlags.Rtl;
			}
			if (optimalBreak)
			{
				this._statusFlags |= ParaProp.StatusFlags.OptimalBreak;
			}
		}

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x06004F33 RID: 20275 RVA: 0x0013B4B8 File Offset: 0x0013A8B8
		internal bool RightToLeft
		{
			get
			{
				return (this._statusFlags & ParaProp.StatusFlags.Rtl) > (ParaProp.StatusFlags)0;
			}
		}

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x06004F34 RID: 20276 RVA: 0x0013B4D0 File Offset: 0x0013A8D0
		internal bool OptimalBreak
		{
			get
			{
				return (this._statusFlags & ParaProp.StatusFlags.OptimalBreak) > (ParaProp.StatusFlags)0;
			}
		}

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x06004F35 RID: 20277 RVA: 0x0013B4E8 File Offset: 0x0013A8E8
		internal bool FirstLineInParagraph
		{
			get
			{
				return this._paragraphProperties.FirstLineInParagraph;
			}
		}

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x06004F36 RID: 20278 RVA: 0x0013B500 File Offset: 0x0013A900
		internal bool AlwaysCollapsible
		{
			get
			{
				return this._paragraphProperties.AlwaysCollapsible;
			}
		}

		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x06004F37 RID: 20279 RVA: 0x0013B518 File Offset: 0x0013A918
		internal int Indent
		{
			get
			{
				return this._indent;
			}
		}

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x06004F38 RID: 20280 RVA: 0x0013B52C File Offset: 0x0013A92C
		internal int ParagraphIndent
		{
			get
			{
				return this._paragraphIndent;
			}
		}

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x06004F39 RID: 20281 RVA: 0x0013B540 File Offset: 0x0013A940
		internal double DefaultIncrementalTab
		{
			get
			{
				return this._paragraphProperties.DefaultIncrementalTab;
			}
		}

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x06004F3A RID: 20282 RVA: 0x0013B558 File Offset: 0x0013A958
		internal IList<TextTabProperties> Tabs
		{
			get
			{
				return this._paragraphProperties.Tabs;
			}
		}

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x06004F3B RID: 20283 RVA: 0x0013B570 File Offset: 0x0013A970
		internal TextAlignment Align
		{
			get
			{
				return this._paragraphProperties.TextAlignment;
			}
		}

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x06004F3C RID: 20284 RVA: 0x0013B588 File Offset: 0x0013A988
		internal bool Justify
		{
			get
			{
				return this._paragraphProperties.TextAlignment == TextAlignment.Justify;
			}
		}

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x06004F3D RID: 20285 RVA: 0x0013B5A4 File Offset: 0x0013A9A4
		internal bool EmergencyWrap
		{
			get
			{
				return this._paragraphProperties.TextWrapping == TextWrapping.Wrap;
			}
		}

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x06004F3E RID: 20286 RVA: 0x0013B5C0 File Offset: 0x0013A9C0
		internal bool Wrap
		{
			get
			{
				return this._paragraphProperties.TextWrapping == TextWrapping.WrapWithOverflow || this.EmergencyWrap;
			}
		}

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x06004F3F RID: 20287 RVA: 0x0013B5E4 File Offset: 0x0013A9E4
		internal Typeface DefaultTypeface
		{
			get
			{
				return this._paragraphProperties.DefaultTextRunProperties.Typeface;
			}
		}

		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x06004F40 RID: 20288 RVA: 0x0013B604 File Offset: 0x0013AA04
		internal int EmSize
		{
			get
			{
				return this._emSize;
			}
		}

		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x06004F41 RID: 20289 RVA: 0x0013B618 File Offset: 0x0013AA18
		internal int LineHeight
		{
			get
			{
				return this._height;
			}
		}

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x06004F42 RID: 20290 RVA: 0x0013B62C File Offset: 0x0013AA2C
		internal TextMarkerProperties TextMarkerProperties
		{
			get
			{
				return this._paragraphProperties.TextMarkerProperties;
			}
		}

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x06004F43 RID: 20291 RVA: 0x0013B644 File Offset: 0x0013AA44
		internal TextLexicalService Hyphenator
		{
			get
			{
				return this._paragraphProperties.Hyphenator;
			}
		}

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x06004F44 RID: 20292 RVA: 0x0013B65C File Offset: 0x0013AA5C
		internal TextDecorationCollection TextDecorations
		{
			get
			{
				return this._paragraphProperties.TextDecorations;
			}
		}

		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x06004F45 RID: 20293 RVA: 0x0013B674 File Offset: 0x0013AA74
		internal Brush DefaultTextDecorationsBrush
		{
			get
			{
				return this._paragraphProperties.DefaultTextRunProperties.ForegroundBrush;
			}
		}

		// Token: 0x040023EC RID: 9196
		private ParaProp.StatusFlags _statusFlags;

		// Token: 0x040023ED RID: 9197
		private TextParagraphProperties _paragraphProperties;

		// Token: 0x040023EE RID: 9198
		private int _emSize;

		// Token: 0x040023EF RID: 9199
		private int _indent;

		// Token: 0x040023F0 RID: 9200
		private int _paragraphIndent;

		// Token: 0x040023F1 RID: 9201
		private int _height;

		// Token: 0x020009E1 RID: 2529
		[Flags]
		private enum StatusFlags
		{
			// Token: 0x04002EB7 RID: 11959
			Rtl = 1,
			// Token: 0x04002EB8 RID: 11960
			OptimalBreak = 2
		}
	}
}
