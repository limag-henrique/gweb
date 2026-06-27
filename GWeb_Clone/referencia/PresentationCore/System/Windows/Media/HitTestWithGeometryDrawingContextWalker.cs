using System;
using System.Collections;

namespace System.Windows.Media
{
	// Token: 0x02000409 RID: 1033
	internal class HitTestWithGeometryDrawingContextWalker : HitTestDrawingContextWalker
	{
		// Token: 0x060029D8 RID: 10712 RVA: 0x000A83B4 File Offset: 0x000A77B4
		internal HitTestWithGeometryDrawingContextWalker(PathGeometry geometry)
		{
			this._geometry = geometry;
			this._currentTransform = null;
			this._currentClip = null;
			this._intersectionDetail = IntersectionDetail.NotCalculated;
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x060029D9 RID: 10713 RVA: 0x000A83E4 File Offset: 0x000A77E4
		internal override bool IsHit
		{
			get
			{
				return this._intersectionDetail != IntersectionDetail.Empty && this._intersectionDetail > IntersectionDetail.NotCalculated;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x060029DA RID: 10714 RVA: 0x000A8408 File Offset: 0x000A7808
		internal override IntersectionDetail IntersectionDetail
		{
			get
			{
				if (this._intersectionDetail == IntersectionDetail.NotCalculated)
				{
					return IntersectionDetail.Empty;
				}
				return this._intersectionDetail;
			}
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x000A8428 File Offset: 0x000A7828
		public override void DrawGeometry(Brush brush, Pen pen, Geometry geometry)
		{
			if (geometry == null || geometry.IsEmpty())
			{
				return;
			}
			Geometry geometry2;
			if (this._currentTransform != null && !this._currentTransform.IsIdentity)
			{
				geometry2 = geometry.GetTransformedCopy(this._currentTransform);
			}
			else
			{
				geometry2 = geometry;
			}
			if (this._currentClip != null)
			{
				geometry2 = Geometry.Combine(geometry2, this._currentClip, GeometryCombineMode.Intersect, null);
			}
			if (brush != null)
			{
				this.AccumulateIntersectionDetail(geometry2.FillContainsWithDetail(this._geometry));
			}
			if (pen != null && !this._contains)
			{
				this.AccumulateIntersectionDetail(geometry2.StrokeContainsWithDetail(pen, this._geometry));
			}
			if (this._contains)
			{
				base.StopWalking();
			}
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x000A84C0 File Offset: 0x000A78C0
		public override void DrawGlyphRun(Brush foregroundBrush, GlyphRun glyphRun)
		{
			if (glyphRun != null)
			{
				Rect rect = glyphRun.ComputeInkBoundingBox();
				if (!rect.IsEmpty)
				{
					rect.Offset((Vector)glyphRun.BaselineOrigin);
					this.DrawGeometry(Brushes.Black, null, new RectangleGeometry(rect));
				}
			}
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x000A8504 File Offset: 0x000A7904
		public override void PushClip(Geometry clipGeometry)
		{
			if (clipGeometry == null || (this._currentClip != null && this._currentClip.IsEmpty()))
			{
				clipGeometry = this._currentClip;
			}
			else
			{
				if (this._currentTransform != null && !this._currentTransform.IsIdentity)
				{
					clipGeometry = clipGeometry.GetTransformedCopy(this._currentTransform);
				}
				if (this._currentClip != null)
				{
					clipGeometry = Geometry.Combine(this._currentClip, clipGeometry, GeometryCombineMode.Intersect, null);
				}
			}
			this.PushModifierStack(new HitTestWithGeometryDrawingContextWalker.ClipModifierNode(this._currentClip));
			this._currentClip = clipGeometry;
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x000A8588 File Offset: 0x000A7988
		public override void PushOpacityMask(Brush brush)
		{
			this.PushModifierStack(null);
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x000A859C File Offset: 0x000A799C
		public override void PushOpacity(double opacity)
		{
			this.PushModifierStack(null);
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x000A85B0 File Offset: 0x000A79B0
		public override void PushTransform(Transform transform)
		{
			if (transform == null || transform.IsIdentity)
			{
				transform = this._currentTransform;
			}
			else if (this._currentTransform != null && !this._currentTransform.IsIdentity)
			{
				Matrix matrix = this._currentTransform.Value * transform.Value;
				transform = new MatrixTransform(matrix);
			}
			this.PushModifierStack(new HitTestWithGeometryDrawingContextWalker.TransformModifierNode(this._currentTransform));
			this._currentTransform = transform;
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000A8620 File Offset: 0x000A7A20
		public override void PushGuidelineSet(GuidelineSet guidelines)
		{
			this.PushModifierStack(null);
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x000A8634 File Offset: 0x000A7A34
		internal override void PushGuidelineY1(double coordinate)
		{
			this.PushModifierStack(null);
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x000A8648 File Offset: 0x000A7A48
		internal override void PushGuidelineY2(double leadingCoordinate, double offsetToDrivenCoordinate)
		{
			this.PushModifierStack(null);
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x000A865C File Offset: 0x000A7A5C
		public override void Pop()
		{
			object obj = this._modifierStack.Pop();
			if (obj is HitTestWithGeometryDrawingContextWalker.TransformModifierNode)
			{
				this._currentTransform = ((HitTestWithGeometryDrawingContextWalker.TransformModifierNode)obj)._transform;
				return;
			}
			if (obj is HitTestWithGeometryDrawingContextWalker.ClipModifierNode)
			{
				this._currentClip = ((HitTestWithGeometryDrawingContextWalker.ClipModifierNode)obj)._clip;
			}
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x000A86A8 File Offset: 0x000A7AA8
		private void AccumulateIntersectionDetail(IntersectionDetail intersectionDetail)
		{
			if (this._intersectionDetail == IntersectionDetail.NotCalculated)
			{
				this._intersectionDetail = intersectionDetail;
			}
			else if (intersectionDetail == IntersectionDetail.FullyInside && this._intersectionDetail != IntersectionDetail.FullyInside)
			{
				this._intersectionDetail = IntersectionDetail.Intersects;
			}
			else if (intersectionDetail == IntersectionDetail.Empty && this._intersectionDetail != IntersectionDetail.Empty)
			{
				this._intersectionDetail = IntersectionDetail.Intersects;
			}
			else
			{
				this._intersectionDetail = intersectionDetail;
			}
			if (this._intersectionDetail == IntersectionDetail.FullyContains)
			{
				this._contains = true;
			}
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x000A870C File Offset: 0x000A7B0C
		private void PushModifierStack(HitTestWithGeometryDrawingContextWalker.ModifierNode modifier)
		{
			if (this._modifierStack == null)
			{
				this._modifierStack = new Stack();
			}
			this._modifierStack.Push(modifier);
		}

		// Token: 0x040012E7 RID: 4839
		private PathGeometry _geometry;

		// Token: 0x040012E8 RID: 4840
		private Stack _modifierStack;

		// Token: 0x040012E9 RID: 4841
		private Transform _currentTransform;

		// Token: 0x040012EA RID: 4842
		private Geometry _currentClip;

		// Token: 0x040012EB RID: 4843
		private IntersectionDetail _intersectionDetail;

		// Token: 0x0200088C RID: 2188
		private class ModifierNode
		{
		}

		// Token: 0x0200088D RID: 2189
		private class TransformModifierNode : HitTestWithGeometryDrawingContextWalker.ModifierNode
		{
			// Token: 0x06005808 RID: 22536 RVA: 0x00167434 File Offset: 0x00166834
			public TransformModifierNode(Transform transform)
			{
				this._transform = transform;
			}

			// Token: 0x040028D5 RID: 10453
			public Transform _transform;
		}

		// Token: 0x0200088E RID: 2190
		private class ClipModifierNode : HitTestWithGeometryDrawingContextWalker.ModifierNode
		{
			// Token: 0x06005809 RID: 22537 RVA: 0x00167450 File Offset: 0x00166850
			public ClipModifierNode(Geometry clip)
			{
				this._clip = clip;
			}

			// Token: 0x040028D6 RID: 10454
			public Geometry _clip;
		}
	}
}
