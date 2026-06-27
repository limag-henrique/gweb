using System;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;

namespace MS.Internal.Ink
{
	// Token: 0x020007C5 RID: 1989
	internal class StrokeInfo
	{
		// Token: 0x060053F0 RID: 21488 RVA: 0x00154220 File Offset: 0x00153620
		internal StrokeInfo(Stroke stroke)
		{
			this._stroke = stroke;
			this._bounds = stroke.GetBounds();
			this._stroke.DrawingAttributesChanged += this.OnStrokeDrawingAttributesChanged;
			this._stroke.StylusPointsReplaced += this.OnStylusPointsReplaced;
			this._stroke.StylusPoints.Changed += this.OnStylusPointsChanged;
			this._stroke.DrawingAttributesReplaced += this.OnDrawingAttributesReplaced;
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x060053F1 RID: 21489 RVA: 0x001542B0 File Offset: 0x001536B0
		internal Stroke Stroke
		{
			get
			{
				return this._stroke;
			}
		}

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x060053F2 RID: 21490 RVA: 0x001542C4 File Offset: 0x001536C4
		internal Rect StrokeBounds
		{
			get
			{
				return this._bounds;
			}
		}

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x060053F3 RID: 21491 RVA: 0x001542D8 File Offset: 0x001536D8
		// (set) Token: 0x060053F4 RID: 21492 RVA: 0x001542EC File Offset: 0x001536EC
		internal bool IsDirty
		{
			get
			{
				return this._isDirty;
			}
			set
			{
				this._isDirty = value;
			}
		}

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x060053F5 RID: 21493 RVA: 0x00154300 File Offset: 0x00153700
		// (set) Token: 0x060053F6 RID: 21494 RVA: 0x00154314 File Offset: 0x00153714
		internal bool IsHit
		{
			get
			{
				return this._isHit;
			}
			set
			{
				this._isHit = value;
			}
		}

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x060053F7 RID: 21495 RVA: 0x00154328 File Offset: 0x00153728
		internal StylusPointCollection StylusPoints
		{
			get
			{
				if (this._stylusPoints == null)
				{
					if (this._stroke.DrawingAttributes.FitToCurve)
					{
						this._stylusPoints = this._stroke.GetBezierStylusPoints();
					}
					else
					{
						this._stylusPoints = this._stroke.StylusPoints;
					}
				}
				return this._stylusPoints;
			}
		}

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x060053F8 RID: 21496 RVA: 0x0015437C File Offset: 0x0015377C
		// (set) Token: 0x060053F9 RID: 21497 RVA: 0x00154390 File Offset: 0x00153790
		internal double HitWeight
		{
			get
			{
				return this._hitWeight;
			}
			set
			{
				if (DoubleUtil.GreaterThan(value, this.TotalWeight))
				{
					this._hitWeight = this.TotalWeight;
					return;
				}
				if (DoubleUtil.LessThan(value, 0.0))
				{
					this._hitWeight = 0.0;
					return;
				}
				this._hitWeight = value;
			}
		}

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x060053FA RID: 21498 RVA: 0x001543E0 File Offset: 0x001537E0
		internal double TotalWeight
		{
			get
			{
				if (!this._totalWeightCached)
				{
					this._totalWeight = 0.0;
					for (int i = 0; i < this.StylusPoints.Count; i++)
					{
						this._totalWeight += this.GetPointWeight(i);
					}
					this._totalWeightCached = true;
				}
				return this._totalWeight;
			}
		}

		// Token: 0x060053FB RID: 21499 RVA: 0x0015443C File Offset: 0x0015383C
		internal double GetPointWeight(int index)
		{
			StylusPointCollection stylusPoints = this.StylusPoints;
			DrawingAttributes drawingAttributes = this.Stroke.DrawingAttributes;
			double num = 0.0;
			if (index == 0)
			{
				num += Math.Sqrt(drawingAttributes.Width * drawingAttributes.Width + drawingAttributes.Height * drawingAttributes.Height) / 2.0;
			}
			else
			{
				num += Math.Sqrt(((Point)stylusPoints[index] - (Point)stylusPoints[index - 1]).LengthSquared) / 2.0;
			}
			if (index == stylusPoints.Count - 1)
			{
				num += Math.Sqrt(drawingAttributes.Width * drawingAttributes.Width + drawingAttributes.Height * drawingAttributes.Height) / 2.0;
			}
			else
			{
				num += Math.Sqrt(((Point)stylusPoints[index + 1] - (Point)stylusPoints[index]).LengthSquared) / 2.0;
			}
			return num;
		}

		// Token: 0x060053FC RID: 21500 RVA: 0x00154548 File Offset: 0x00153948
		internal void Detach()
		{
			if (this._stroke != null)
			{
				this._stroke.DrawingAttributesChanged -= this.OnStrokeDrawingAttributesChanged;
				this._stroke.StylusPointsReplaced -= this.OnStylusPointsReplaced;
				this._stroke.StylusPoints.Changed -= this.OnStylusPointsChanged;
				this._stroke.DrawingAttributesReplaced -= this.OnDrawingAttributesReplaced;
				this._stroke = null;
			}
		}

		// Token: 0x060053FD RID: 21501 RVA: 0x001545C8 File Offset: 0x001539C8
		private void OnStylusPointsChanged(object sender, EventArgs args)
		{
			this.Invalidate();
		}

		// Token: 0x060053FE RID: 21502 RVA: 0x001545DC File Offset: 0x001539DC
		private void OnStylusPointsReplaced(object sender, StylusPointsReplacedEventArgs args)
		{
			this.Invalidate();
		}

		// Token: 0x060053FF RID: 21503 RVA: 0x001545F0 File Offset: 0x001539F0
		private void OnStrokeDrawingAttributesChanged(object sender, PropertyDataChangedEventArgs args)
		{
			if (DrawingAttributes.IsGeometricalDaGuid(args.PropertyGuid))
			{
				this.Invalidate();
			}
		}

		// Token: 0x06005400 RID: 21504 RVA: 0x00154610 File Offset: 0x00153A10
		private void OnDrawingAttributesReplaced(object sender, DrawingAttributesReplacedEventArgs args)
		{
			if (!DrawingAttributes.GeometricallyEqual(args.NewDrawingAttributes, args.PreviousDrawingAttributes))
			{
				this.Invalidate();
			}
		}

		// Token: 0x06005401 RID: 21505 RVA: 0x00154638 File Offset: 0x00153A38
		private void Invalidate()
		{
			this._totalWeightCached = false;
			this._stylusPoints = null;
			this._hitWeight = 0.0;
			this._isDirty = true;
			this._bounds = this._stroke.GetBounds();
		}

		// Token: 0x040025D3 RID: 9683
		private Stroke _stroke;

		// Token: 0x040025D4 RID: 9684
		private Rect _bounds;

		// Token: 0x040025D5 RID: 9685
		private double _hitWeight;

		// Token: 0x040025D6 RID: 9686
		private bool _isHit;

		// Token: 0x040025D7 RID: 9687
		private bool _isDirty = true;

		// Token: 0x040025D8 RID: 9688
		private StylusPointCollection _stylusPoints;

		// Token: 0x040025D9 RID: 9689
		private double _totalWeight;

		// Token: 0x040025DA RID: 9690
		private bool _totalWeightCached;
	}
}
