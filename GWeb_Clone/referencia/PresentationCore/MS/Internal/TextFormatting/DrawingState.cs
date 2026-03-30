using System;
using System.Windows;
using System.Windows.Media;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000701 RID: 1793
	internal sealed class DrawingState : IDisposable
	{
		// Token: 0x06004D1D RID: 19741 RVA: 0x001317A4 File Offset: 0x00130BA4
		internal DrawingState(DrawingContext drawingContext, Point lineOrigin, MatrixTransform antiInversion, TextMetrics.FullTextLine currentLine)
		{
			this._drawingContext = drawingContext;
			this._antiInversion = antiInversion;
			this._currentLine = currentLine;
			if (antiInversion == null)
			{
				this._lineOrigin = lineOrigin;
			}
			else
			{
				this._vectorToLineOrigin = lineOrigin;
			}
			if (this._drawingContext != null)
			{
				this._baseGuidelineY = lineOrigin.Y + currentLine.Baseline;
				this._drawingContext.PushGuidelineY1(this._baseGuidelineY);
			}
		}

		// Token: 0x06004D1E RID: 19742 RVA: 0x00131810 File Offset: 0x00130C10
		internal void SetGuidelineY(double runGuidelineY)
		{
			if (this._drawingContext == null)
			{
				return;
			}
			Invariant.Assert(!this._overrideBaseGuidelineY);
			if (runGuidelineY != this._baseGuidelineY)
			{
				this._drawingContext.PushGuidelineY1(runGuidelineY);
				this._overrideBaseGuidelineY = true;
			}
		}

		// Token: 0x06004D1F RID: 19743 RVA: 0x00131850 File Offset: 0x00130C50
		internal void UnsetGuidelineY()
		{
			if (this._overrideBaseGuidelineY)
			{
				this._drawingContext.Pop();
				this._overrideBaseGuidelineY = false;
			}
		}

		// Token: 0x06004D20 RID: 19744 RVA: 0x00131878 File Offset: 0x00130C78
		public void Dispose()
		{
			if (this._drawingContext != null)
			{
				this._drawingContext.Pop();
			}
		}

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x06004D21 RID: 19745 RVA: 0x00131898 File Offset: 0x00130C98
		internal DrawingContext DrawingContext
		{
			get
			{
				return this._drawingContext;
			}
		}

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x06004D22 RID: 19746 RVA: 0x001318AC File Offset: 0x00130CAC
		internal MatrixTransform AntiInversion
		{
			get
			{
				return this._antiInversion;
			}
		}

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x06004D23 RID: 19747 RVA: 0x001318C0 File Offset: 0x00130CC0
		internal Point LineOrigin
		{
			get
			{
				return this._lineOrigin;
			}
		}

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06004D24 RID: 19748 RVA: 0x001318D4 File Offset: 0x00130CD4
		internal Point VectorToLineOrigin
		{
			get
			{
				return this._vectorToLineOrigin;
			}
		}

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06004D25 RID: 19749 RVA: 0x001318E8 File Offset: 0x00130CE8
		internal TextMetrics.FullTextLine CurrentLine
		{
			get
			{
				return this._currentLine;
			}
		}

		// Token: 0x04002181 RID: 8577
		private TextMetrics.FullTextLine _currentLine;

		// Token: 0x04002182 RID: 8578
		private DrawingContext _drawingContext;

		// Token: 0x04002183 RID: 8579
		private Point _lineOrigin;

		// Token: 0x04002184 RID: 8580
		private Point _vectorToLineOrigin;

		// Token: 0x04002185 RID: 8581
		private MatrixTransform _antiInversion;

		// Token: 0x04002186 RID: 8582
		private bool _overrideBaseGuidelineY;

		// Token: 0x04002187 RID: 8583
		private double _baseGuidelineY;
	}
}
