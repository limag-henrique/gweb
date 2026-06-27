using System;
using System.Windows;
using System.Windows.Media.TextFormatting;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000708 RID: 1800
	internal sealed class GenericTextParagraphProperties : TextParagraphProperties
	{
		// Token: 0x06004D82 RID: 19842 RVA: 0x001333B4 File Offset: 0x001327B4
		public GenericTextParagraphProperties(FlowDirection flowDirection, TextAlignment textAlignment, bool firstLineInParagraph, bool alwaysCollapsible, TextRunProperties defaultTextRunProperties, TextWrapping textWrap, double lineHeight, double indent)
		{
			this._flowDirection = flowDirection;
			this._textAlignment = textAlignment;
			this._firstLineInParagraph = firstLineInParagraph;
			this._alwaysCollapsible = alwaysCollapsible;
			this._defaultTextRunProperties = defaultTextRunProperties;
			this._textWrap = textWrap;
			this._lineHeight = lineHeight;
			this._indent = indent;
		}

		// Token: 0x06004D83 RID: 19843 RVA: 0x00133404 File Offset: 0x00132804
		public GenericTextParagraphProperties(TextParagraphProperties textParagraphProperties)
		{
			this._flowDirection = textParagraphProperties.FlowDirection;
			this._defaultTextRunProperties = textParagraphProperties.DefaultTextRunProperties;
			this._textAlignment = textParagraphProperties.TextAlignment;
			this._lineHeight = textParagraphProperties.LineHeight;
			this._firstLineInParagraph = textParagraphProperties.FirstLineInParagraph;
			this._alwaysCollapsible = textParagraphProperties.AlwaysCollapsible;
			this._textWrap = textParagraphProperties.TextWrapping;
			this._indent = textParagraphProperties.Indent;
		}

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06004D84 RID: 19844 RVA: 0x00133478 File Offset: 0x00132878
		public override FlowDirection FlowDirection
		{
			get
			{
				return this._flowDirection;
			}
		}

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06004D85 RID: 19845 RVA: 0x0013348C File Offset: 0x0013288C
		public override TextAlignment TextAlignment
		{
			get
			{
				return this._textAlignment;
			}
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06004D86 RID: 19846 RVA: 0x001334A0 File Offset: 0x001328A0
		public override double LineHeight
		{
			get
			{
				return this._lineHeight;
			}
		}

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06004D87 RID: 19847 RVA: 0x001334B4 File Offset: 0x001328B4
		public override bool FirstLineInParagraph
		{
			get
			{
				return this._firstLineInParagraph;
			}
		}

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06004D88 RID: 19848 RVA: 0x001334C8 File Offset: 0x001328C8
		public override bool AlwaysCollapsible
		{
			get
			{
				return this._alwaysCollapsible;
			}
		}

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06004D89 RID: 19849 RVA: 0x001334DC File Offset: 0x001328DC
		public override TextRunProperties DefaultTextRunProperties
		{
			get
			{
				return this._defaultTextRunProperties;
			}
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06004D8A RID: 19850 RVA: 0x001334F0 File Offset: 0x001328F0
		public override TextWrapping TextWrapping
		{
			get
			{
				return this._textWrap;
			}
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x06004D8B RID: 19851 RVA: 0x00133504 File Offset: 0x00132904
		public override TextMarkerProperties TextMarkerProperties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x06004D8C RID: 19852 RVA: 0x00133514 File Offset: 0x00132914
		public override double Indent
		{
			get
			{
				return this._indent;
			}
		}

		// Token: 0x06004D8D RID: 19853 RVA: 0x00133528 File Offset: 0x00132928
		internal void SetFlowDirection(FlowDirection flowDirection)
		{
			this._flowDirection = flowDirection;
		}

		// Token: 0x06004D8E RID: 19854 RVA: 0x0013353C File Offset: 0x0013293C
		internal void SetTextAlignment(TextAlignment textAlignment)
		{
			this._textAlignment = textAlignment;
		}

		// Token: 0x06004D8F RID: 19855 RVA: 0x00133550 File Offset: 0x00132950
		internal void SetLineHeight(double lineHeight)
		{
			this._lineHeight = lineHeight;
		}

		// Token: 0x06004D90 RID: 19856 RVA: 0x00133564 File Offset: 0x00132964
		internal void SetTextWrapping(TextWrapping textWrap)
		{
			this._textWrap = textWrap;
		}

		// Token: 0x040021BA RID: 8634
		private FlowDirection _flowDirection;

		// Token: 0x040021BB RID: 8635
		private TextAlignment _textAlignment;

		// Token: 0x040021BC RID: 8636
		private bool _firstLineInParagraph;

		// Token: 0x040021BD RID: 8637
		private bool _alwaysCollapsible;

		// Token: 0x040021BE RID: 8638
		private TextRunProperties _defaultTextRunProperties;

		// Token: 0x040021BF RID: 8639
		private TextWrapping _textWrap;

		// Token: 0x040021C0 RID: 8640
		private double _indent;

		// Token: 0x040021C1 RID: 8641
		private double _lineHeight;
	}
}
