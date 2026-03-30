using System;
using System.Collections.Generic;
using System.Windows.Media.Effects;
using MS.Internal;

namespace System.Windows.Media
{
	// Token: 0x02000367 RID: 871
	internal class BoundsDrawingContextWalker : DrawingContextWalker
	{
		// Token: 0x06001DE8 RID: 7656 RVA: 0x0007A538 File Offset: 0x00079938
		public BoundsDrawingContextWalker()
		{
			this._bounds = Rect.Empty;
			this._transform = Matrix.Identity;
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x0007A564 File Offset: 0x00079964
		public Rect Bounds
		{
			get
			{
				return this._bounds;
			}
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x0007A578 File Offset: 0x00079978
		public override void DrawLine(Pen pen, Point point0, Point point1)
		{
			if (Pen.ContributesToBounds(pen))
			{
				Rect boundsHelper = LineGeometry.GetBoundsHelper(pen, this._transform, point0, point1, Matrix.Identity, Geometry.StandardFlatteningTolerance, ToleranceType.Absolute);
				this.AddTransformedBounds(ref boundsHelper);
			}
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x0007A5B0 File Offset: 0x000799B0
		public override void DrawRectangle(Brush brush, Pen pen, Rect rectangle)
		{
			if (brush != null || Pen.ContributesToBounds(pen))
			{
				Rect boundsHelper = RectangleGeometry.GetBoundsHelper(pen, this._transform, rectangle, 0.0, 0.0, Matrix.Identity, Geometry.StandardFlatteningTolerance, ToleranceType.Absolute);
				this.AddTransformedBounds(ref boundsHelper);
			}
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x0007A5FC File Offset: 0x000799FC
		public override void DrawRoundedRectangle(Brush brush, Pen pen, Rect rectangle, double radiusX, double radiusY)
		{
			if (brush != null || Pen.ContributesToBounds(pen))
			{
				Rect boundsHelper = RectangleGeometry.GetBoundsHelper(pen, this._transform, rectangle, radiusX, radiusY, Matrix.Identity, Geometry.StandardFlatteningTolerance, ToleranceType.Absolute);
				this.AddTransformedBounds(ref boundsHelper);
			}
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x0007A63C File Offset: 0x00079A3C
		public override void DrawEllipse(Brush brush, Pen pen, Point center, double radiusX, double radiusY)
		{
			if (brush != null || Pen.ContributesToBounds(pen))
			{
				Rect boundsHelper = EllipseGeometry.GetBoundsHelper(pen, this._transform, center, radiusX, radiusY, Matrix.Identity, Geometry.StandardFlatteningTolerance, ToleranceType.Absolute);
				this.AddTransformedBounds(ref boundsHelper);
			}
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x0007A67C File Offset: 0x00079A7C
		public override void DrawGeometry(Brush brush, Pen pen, Geometry geometry)
		{
			if (geometry != null && (brush != null || Pen.ContributesToBounds(pen)))
			{
				Rect boundsInternal = geometry.GetBoundsInternal(pen, this._transform);
				this.AddTransformedBounds(ref boundsInternal);
			}
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x0007A6B0 File Offset: 0x00079AB0
		public override void DrawImage(ImageSource imageSource, Rect rectangle)
		{
			if (imageSource != null)
			{
				this.AddBounds(ref rectangle);
			}
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x0007A6C8 File Offset: 0x00079AC8
		public override void DrawVideo(MediaPlayer video, Rect rectangle)
		{
			if (video != null)
			{
				this.AddBounds(ref rectangle);
			}
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x0007A6E0 File Offset: 0x00079AE0
		public override void DrawGlyphRun(Brush foregroundBrush, GlyphRun glyphRun)
		{
			if (foregroundBrush != null && glyphRun != null)
			{
				Rect rect = glyphRun.ComputeInkBoundingBox();
				if (!rect.IsEmpty)
				{
					rect.Offset((Vector)glyphRun.BaselineOrigin);
					this.AddBounds(ref rect);
				}
			}
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x0007A720 File Offset: 0x00079B20
		public override void PushOpacityMask(Brush brush)
		{
			this.PushTypeStack(BoundsDrawingContextWalker.PushType.OpacityMask);
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x0007A734 File Offset: 0x00079B34
		public override void PushClip(Geometry clipGeometry)
		{
			if (this._haveClip)
			{
				if (this._clipStack == null)
				{
					this._clipStack = new Stack<Rect>(2);
				}
				this._clipStack.Push(this._clip);
			}
			this.PushTypeStack(BoundsDrawingContextWalker.PushType.Clip);
			if (clipGeometry != null)
			{
				if (!this._haveClip)
				{
					this._haveClip = true;
					this._clip = clipGeometry.GetBoundsInternal(null, this._transform);
					return;
				}
				this._clip.Intersect(clipGeometry.GetBoundsInternal(null, this._transform));
			}
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x0007A7B4 File Offset: 0x00079BB4
		public override void PushOpacity(double opacity)
		{
			this.PushTypeStack(BoundsDrawingContextWalker.PushType.Opacity);
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x0007A7C8 File Offset: 0x00079BC8
		public override void PushTransform(Transform transform)
		{
			if (this._transformStack == null)
			{
				this._transformStack = new Stack<Matrix>(2);
			}
			this._transformStack.Push(this._transform);
			this.PushTypeStack(BoundsDrawingContextWalker.PushType.Transform);
			Matrix trans = Matrix.Identity;
			if (transform != null && !transform.IsIdentity)
			{
				trans = transform.Value;
			}
			this._transform = trans * this._transform;
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x0007A82C File Offset: 0x00079C2C
		public override void PushGuidelineSet(GuidelineSet guidelines)
		{
			this.PushTypeStack(BoundsDrawingContextWalker.PushType.Guidelines);
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x0007A840 File Offset: 0x00079C40
		internal override void PushGuidelineY1(double coordinate)
		{
			this.PushTypeStack(BoundsDrawingContextWalker.PushType.Guidelines);
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x0007A854 File Offset: 0x00079C54
		internal override void PushGuidelineY2(double leadingCoordinate, double offsetToDrivenCoordinate)
		{
			this.PushTypeStack(BoundsDrawingContextWalker.PushType.Guidelines);
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x0007A868 File Offset: 0x00079C68
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		public override void PushEffect(BitmapEffect effect, BitmapEffectInput effectInput)
		{
			this.PushTypeStack(BoundsDrawingContextWalker.PushType.BitmapEffect);
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x0007A87C File Offset: 0x00079C7C
		public override void Pop()
		{
			BoundsDrawingContextWalker.PushType pushType = this._pushTypeStack.Pop();
			if (pushType == BoundsDrawingContextWalker.PushType.Transform)
			{
				this._transform = this._transformStack.Pop();
				return;
			}
			if (pushType != BoundsDrawingContextWalker.PushType.Clip)
			{
				return;
			}
			if (this._clipStack != null && this._clipStack.Count > 0)
			{
				this._clip = this._clipStack.Pop();
				return;
			}
			this._haveClip = false;
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x0007A8E4 File Offset: 0x00079CE4
		private void AddBounds(ref Rect bounds)
		{
			if (!this._transform.IsIdentity)
			{
				MatrixUtil.TransformRect(ref bounds, ref this._transform);
			}
			this.AddTransformedBounds(ref bounds);
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x0007A914 File Offset: 0x00079D14
		private void AddTransformedBounds(ref Rect bounds)
		{
			if (DoubleUtil.RectHasNaN(bounds))
			{
				bounds.X = double.NegativeInfinity;
				bounds.Y = double.NegativeInfinity;
				bounds.Width = double.PositiveInfinity;
				bounds.Height = double.PositiveInfinity;
			}
			if (this._haveClip)
			{
				bounds.Intersect(this._clip);
			}
			this._bounds.Union(bounds);
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x0007A990 File Offset: 0x00079D90
		private void PushTypeStack(BoundsDrawingContextWalker.PushType pushType)
		{
			if (this._pushTypeStack == null)
			{
				this._pushTypeStack = new Stack<BoundsDrawingContextWalker.PushType>(2);
			}
			this._pushTypeStack.Push(pushType);
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x0007A9C0 File Offset: 0x00079DC0
		internal void ClearState()
		{
			this._clip = Rect.Empty;
			this._bounds = Rect.Empty;
			this._haveClip = false;
			this._transform = default(Matrix);
			this._pushTypeStack = null;
			this._transformStack = null;
			this._clipStack = null;
		}

		// Token: 0x04000FFF RID: 4095
		private Rect _bounds;

		// Token: 0x04001000 RID: 4096
		private Rect _clip;

		// Token: 0x04001001 RID: 4097
		private bool _haveClip;

		// Token: 0x04001002 RID: 4098
		private Matrix _transform;

		// Token: 0x04001003 RID: 4099
		private Stack<BoundsDrawingContextWalker.PushType> _pushTypeStack;

		// Token: 0x04001004 RID: 4100
		private Stack<Matrix> _transformStack;

		// Token: 0x04001005 RID: 4101
		private Stack<Rect> _clipStack;

		// Token: 0x02000855 RID: 2133
		private enum PushType
		{
			// Token: 0x04002817 RID: 10263
			Transform,
			// Token: 0x04002818 RID: 10264
			Clip,
			// Token: 0x04002819 RID: 10265
			Opacity,
			// Token: 0x0400281A RID: 10266
			OpacityMask,
			// Token: 0x0400281B RID: 10267
			Guidelines,
			// Token: 0x0400281C RID: 10268
			BitmapEffect
		}
	}
}
