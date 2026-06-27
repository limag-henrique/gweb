using System;
using System.Collections.Generic;
using System.Windows.Media.Effects;

namespace System.Windows.Media
{
	// Token: 0x0200040A RID: 1034
	internal class HitTestWithPointDrawingContextWalker : HitTestDrawingContextWalker
	{
		// Token: 0x060029E7 RID: 10727 RVA: 0x000A8738 File Offset: 0x000A7B38
		internal HitTestWithPointDrawingContextWalker(Point point)
		{
			this._point = point;
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x060029E8 RID: 10728 RVA: 0x000A8754 File Offset: 0x000A7B54
		internal override bool IsHit
		{
			get
			{
				return this._contains;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x060029E9 RID: 10729 RVA: 0x000A8768 File Offset: 0x000A7B68
		internal override IntersectionDetail IntersectionDetail
		{
			get
			{
				if (!this._contains)
				{
					return IntersectionDetail.Empty;
				}
				return IntersectionDetail.FullyInside;
			}
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x000A8780 File Offset: 0x000A7B80
		public override void DrawGeometry(Brush brush, Pen pen, Geometry geometry)
		{
			if (this.IsCurrentLayerNoOp || geometry == null || geometry.IsEmpty())
			{
				return;
			}
			if (brush != null)
			{
				this._contains |= geometry.FillContains(this._point);
			}
			if (pen != null && !this._contains)
			{
				this._contains |= geometry.StrokeContains(pen, this._point);
			}
			if (this._contains)
			{
				base.StopWalking();
			}
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x000A87F0 File Offset: 0x000A7BF0
		public override void DrawGlyphRun(Brush foregroundBrush, GlyphRun glyphRun)
		{
			if (!this.IsCurrentLayerNoOp && glyphRun != null)
			{
				Rect rect = glyphRun.ComputeInkBoundingBox();
				if (!rect.IsEmpty)
				{
					rect.Offset((Vector)glyphRun.BaselineOrigin);
					this._contains |= rect.Contains(this._point);
					if (this._contains)
					{
						base.StopWalking();
					}
				}
			}
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x000A8854 File Offset: 0x000A7C54
		public override void PushClip(Geometry clipGeometry)
		{
			if (!this.IsPushNoOp())
			{
				this.PushPointStack(this._point);
				if (clipGeometry != null && !clipGeometry.FillContains(this._point))
				{
					this.IsCurrentLayerNoOp = true;
				}
			}
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x000A8890 File Offset: 0x000A7C90
		public override void PushOpacityMask(Brush brush)
		{
			if (!this.IsPushNoOp())
			{
				this.PushPointStack(this._point);
			}
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x000A88B4 File Offset: 0x000A7CB4
		public override void PushOpacity(double opacity)
		{
			if (!this.IsPushNoOp())
			{
				this.PushPointStack(this._point);
			}
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x000A88D8 File Offset: 0x000A7CD8
		public override void PushTransform(Transform transform)
		{
			if (!this.IsPushNoOp())
			{
				if (transform == null || transform.IsIdentity)
				{
					this.PushPointStack(this._point);
					return;
				}
				Matrix value = transform.Value;
				if (value.HasInverse)
				{
					value.Invert();
					this.PushPointStack(this._point * value);
					return;
				}
				this.IsCurrentLayerNoOp = true;
			}
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x000A8938 File Offset: 0x000A7D38
		public override void PushGuidelineSet(GuidelineSet guidelines)
		{
			if (!this.IsPushNoOp())
			{
				this.PushPointStack(this._point);
			}
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x000A895C File Offset: 0x000A7D5C
		internal override void PushGuidelineY1(double coordinate)
		{
			if (!this.IsPushNoOp())
			{
				this.PushPointStack(this._point);
			}
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x000A8980 File Offset: 0x000A7D80
		internal override void PushGuidelineY2(double leadingCoordinate, double offsetToDrivenCoordinate)
		{
			if (!this.IsPushNoOp())
			{
				this.PushPointStack(this._point);
			}
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x000A89A4 File Offset: 0x000A7DA4
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		public override void PushEffect(BitmapEffect effect, BitmapEffectInput effectInput)
		{
			if (!this.IsPushNoOp())
			{
				this.PushPointStack(this._point);
			}
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x000A89C8 File Offset: 0x000A7DC8
		public override void Pop()
		{
			if (!this.IsPopNoOp())
			{
				this.PopPointStack();
			}
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x000A89E4 File Offset: 0x000A7DE4
		private void PushPointStack(Point point)
		{
			if (this._pointStack == null)
			{
				this._pointStack = new Stack<Point>(2);
			}
			this._pointStack.Push(this._point);
			this._point = point;
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x000A8A20 File Offset: 0x000A7E20
		private void PopPointStack()
		{
			this._point = this._pointStack.Pop();
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x000A8A40 File Offset: 0x000A7E40
		private bool IsPushNoOp()
		{
			if (this.IsCurrentLayerNoOp)
			{
				this._noOpLayerDepth++;
				return true;
			}
			return false;
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x000A8A68 File Offset: 0x000A7E68
		private bool IsPopNoOp()
		{
			if (this.IsCurrentLayerNoOp)
			{
				this._noOpLayerDepth--;
				if (this._noOpLayerDepth == 0)
				{
					this.IsCurrentLayerNoOp = false;
				}
				return true;
			}
			return false;
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060029FA RID: 10746 RVA: 0x000A8AD0 File Offset: 0x000A7ED0
		// (set) Token: 0x060029F9 RID: 10745 RVA: 0x000A8AA0 File Offset: 0x000A7EA0
		private bool IsCurrentLayerNoOp
		{
			get
			{
				return this._currentLayerIsNoOp;
			}
			set
			{
				if (value)
				{
					this._currentLayerIsNoOp = true;
					this._noOpLayerDepth++;
					return;
				}
				this._currentLayerIsNoOp = false;
			}
		}

		// Token: 0x040012EC RID: 4844
		private Point _point;

		// Token: 0x040012ED RID: 4845
		private Stack<Point> _pointStack;

		// Token: 0x040012EE RID: 4846
		private bool _currentLayerIsNoOp;

		// Token: 0x040012EF RID: 4847
		private int _noOpLayerDepth;
	}
}
