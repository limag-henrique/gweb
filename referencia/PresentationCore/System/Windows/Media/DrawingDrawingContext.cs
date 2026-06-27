using System;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	// Token: 0x02000384 RID: 900
	internal class DrawingDrawingContext : DrawingContext
	{
		// Token: 0x0600211B RID: 8475 RVA: 0x00085C78 File Offset: 0x00085078
		internal DrawingDrawingContext()
		{
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x00085C94 File Offset: 0x00085094
		public override void DrawLine(Pen pen, Point point0, Point point1)
		{
			this.DrawLine(pen, point0, null, point1, null);
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x00085CAC File Offset: 0x000850AC
		public override void DrawLine(Pen pen, Point point0, AnimationClock point0Animations, Point point1, AnimationClock point1Animations)
		{
			this.VerifyApiNonstructuralChange();
			if (pen == null)
			{
				return;
			}
			LineGeometry lineGeometry = new LineGeometry(point0, point1);
			lineGeometry.CanBeInheritanceContext = this.CanBeInheritanceContext;
			this.SetupNewFreezable(lineGeometry, point0Animations == null && point1Animations == null);
			if (point0Animations != null)
			{
				lineGeometry.ApplyAnimationClock(LineGeometry.StartPointProperty, point0Animations);
			}
			if (point1Animations != null)
			{
				lineGeometry.ApplyAnimationClock(LineGeometry.EndPointProperty, point1Animations);
			}
			this.AddNewGeometryDrawing(null, pen, lineGeometry);
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x00085D14 File Offset: 0x00085114
		public override void DrawRectangle(Brush brush, Pen pen, Rect rectangle)
		{
			this.DrawRectangle(brush, pen, rectangle, null);
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x00085D2C File Offset: 0x0008512C
		public override void DrawRectangle(Brush brush, Pen pen, Rect rectangle, AnimationClock rectangleAnimations)
		{
			this.VerifyApiNonstructuralChange();
			if (brush == null && pen == null)
			{
				return;
			}
			RectangleGeometry rectangleGeometry = new RectangleGeometry(rectangle);
			rectangleGeometry.CanBeInheritanceContext = this.CanBeInheritanceContext;
			this.SetupNewFreezable(rectangleGeometry, rectangleAnimations == null);
			if (rectangleAnimations != null)
			{
				rectangleGeometry.ApplyAnimationClock(RectangleGeometry.RectProperty, rectangleAnimations);
			}
			this.AddNewGeometryDrawing(brush, pen, rectangleGeometry);
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x00085D80 File Offset: 0x00085180
		public override void DrawRoundedRectangle(Brush brush, Pen pen, Rect rectangle, double radiusX, double radiusY)
		{
			this.DrawRoundedRectangle(brush, pen, rectangle, null, radiusX, null, radiusY, null);
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x00085DA0 File Offset: 0x000851A0
		public override void DrawRoundedRectangle(Brush brush, Pen pen, Rect rectangle, AnimationClock rectangleAnimations, double radiusX, AnimationClock radiusXAnimations, double radiusY, AnimationClock radiusYAnimations)
		{
			this.VerifyApiNonstructuralChange();
			if (brush == null && pen == null)
			{
				return;
			}
			RectangleGeometry rectangleGeometry = new RectangleGeometry(rectangle, radiusX, radiusY);
			rectangleGeometry.CanBeInheritanceContext = this.CanBeInheritanceContext;
			this.SetupNewFreezable(rectangleGeometry, rectangleAnimations == null && radiusXAnimations == null && radiusYAnimations == null);
			if (rectangleAnimations != null)
			{
				rectangleGeometry.ApplyAnimationClock(RectangleGeometry.RectProperty, rectangleAnimations);
			}
			if (radiusXAnimations != null)
			{
				rectangleGeometry.ApplyAnimationClock(RectangleGeometry.RadiusXProperty, radiusXAnimations);
			}
			if (radiusYAnimations != null)
			{
				rectangleGeometry.ApplyAnimationClock(RectangleGeometry.RadiusYProperty, radiusYAnimations);
			}
			this.AddNewGeometryDrawing(brush, pen, rectangleGeometry);
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x00085E28 File Offset: 0x00085228
		public override void DrawEllipse(Brush brush, Pen pen, Point center, double radiusX, double radiusY)
		{
			this.DrawEllipse(brush, pen, center, null, radiusX, null, radiusY, null);
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x00085E48 File Offset: 0x00085248
		public override void DrawEllipse(Brush brush, Pen pen, Point center, AnimationClock centerAnimations, double radiusX, AnimationClock radiusXAnimations, double radiusY, AnimationClock radiusYAnimations)
		{
			this.VerifyApiNonstructuralChange();
			if (brush == null && pen == null)
			{
				return;
			}
			EllipseGeometry ellipseGeometry = new EllipseGeometry(center, radiusX, radiusY);
			ellipseGeometry.CanBeInheritanceContext = this.CanBeInheritanceContext;
			this.SetupNewFreezable(ellipseGeometry, centerAnimations == null && radiusXAnimations == null && radiusYAnimations == null);
			if (centerAnimations != null)
			{
				ellipseGeometry.ApplyAnimationClock(EllipseGeometry.CenterProperty, centerAnimations);
			}
			if (radiusXAnimations != null)
			{
				ellipseGeometry.ApplyAnimationClock(EllipseGeometry.RadiusXProperty, radiusXAnimations);
			}
			if (radiusYAnimations != null)
			{
				ellipseGeometry.ApplyAnimationClock(EllipseGeometry.RadiusYProperty, radiusYAnimations);
			}
			this.AddNewGeometryDrawing(brush, pen, ellipseGeometry);
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x00085ED0 File Offset: 0x000852D0
		public override void DrawGeometry(Brush brush, Pen pen, Geometry geometry)
		{
			this.VerifyApiNonstructuralChange();
			if ((brush == null && pen == null) || geometry == null)
			{
				return;
			}
			this.AddNewGeometryDrawing(brush, pen, geometry);
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x00085EF8 File Offset: 0x000852F8
		public override void DrawImage(ImageSource imageSource, Rect rectangle)
		{
			this.DrawImage(imageSource, rectangle, null);
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x00085F10 File Offset: 0x00085310
		public override void DrawImage(ImageSource imageSource, Rect rectangle, AnimationClock rectangleAnimations)
		{
			this.VerifyApiNonstructuralChange();
			if (imageSource == null)
			{
				return;
			}
			ImageDrawing imageDrawing = new ImageDrawing();
			imageDrawing.CanBeInheritanceContext = this.CanBeInheritanceContext;
			imageDrawing.ImageSource = imageSource;
			imageDrawing.Rect = rectangle;
			this.SetupNewFreezable(imageDrawing, rectangleAnimations == null && imageSource.IsFrozen);
			if (rectangleAnimations != null)
			{
				imageDrawing.ApplyAnimationClock(ImageDrawing.RectProperty, rectangleAnimations);
			}
			this.AddDrawing(imageDrawing);
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x00085F74 File Offset: 0x00085374
		public override void DrawDrawing(Drawing drawing)
		{
			this.VerifyApiNonstructuralChange();
			if (drawing == null)
			{
				return;
			}
			this.AddDrawing(drawing);
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x00085F94 File Offset: 0x00085394
		public override void DrawVideo(MediaPlayer player, Rect rectangle)
		{
			this.DrawVideo(player, rectangle, null);
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x00085FAC File Offset: 0x000853AC
		public override void DrawVideo(MediaPlayer player, Rect rectangle, AnimationClock rectangleAnimations)
		{
			this.VerifyApiNonstructuralChange();
			if (player == null)
			{
				return;
			}
			VideoDrawing videoDrawing = new VideoDrawing();
			videoDrawing.CanBeInheritanceContext = this.CanBeInheritanceContext;
			videoDrawing.Player = player;
			videoDrawing.Rect = rectangle;
			this.SetupNewFreezable(videoDrawing, false);
			if (rectangleAnimations != null)
			{
				videoDrawing.ApplyAnimationClock(VideoDrawing.RectProperty, rectangleAnimations);
			}
			this.AddDrawing(videoDrawing);
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x00086004 File Offset: 0x00085404
		public override void PushClip(Geometry clipGeometry)
		{
			this.VerifyApiNonstructuralChange();
			this.PushNewDrawingGroup();
			this._currentDrawingGroup.ClipGeometry = clipGeometry;
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x0008602C File Offset: 0x0008542C
		public override void PushOpacityMask(Brush brush)
		{
			this.VerifyApiNonstructuralChange();
			this.PushNewDrawingGroup();
			this._currentDrawingGroup.OpacityMask = brush;
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x00086054 File Offset: 0x00085454
		public override void PushOpacity(double opacity)
		{
			this.PushOpacity(opacity, null);
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x0008606C File Offset: 0x0008546C
		public override void PushOpacity(double opacity, AnimationClock opacityAnimations)
		{
			this.VerifyApiNonstructuralChange();
			this.PushNewDrawingGroup();
			this._currentDrawingGroup.Opacity = opacity;
			if (opacityAnimations != null)
			{
				this._currentDrawingGroup.ApplyAnimationClock(DrawingGroup.OpacityProperty, opacityAnimations);
			}
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x000860A8 File Offset: 0x000854A8
		public override void PushTransform(Transform transform)
		{
			this.VerifyApiNonstructuralChange();
			this.PushNewDrawingGroup();
			this._currentDrawingGroup.Transform = transform;
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x000860D0 File Offset: 0x000854D0
		public override void PushGuidelineSet(GuidelineSet guidelines)
		{
			this.VerifyApiNonstructuralChange();
			this.PushNewDrawingGroup();
			this._currentDrawingGroup.GuidelineSet = guidelines;
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x000860F8 File Offset: 0x000854F8
		internal override void PushGuidelineY1(double coordinate)
		{
			this.VerifyApiNonstructuralChange();
			this.PushNewDrawingGroup();
			double[] guidelinesX = null;
			double[] array = new double[2];
			array[0] = coordinate;
			GuidelineSet guidelineSet = new GuidelineSet(guidelinesX, array, true);
			guidelineSet.Freeze();
			this._currentDrawingGroup.GuidelineSet = guidelineSet;
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x00086138 File Offset: 0x00085538
		internal override void PushGuidelineY2(double leadingCoordinate, double offsetToDrivenCoordinate)
		{
			this.VerifyApiNonstructuralChange();
			this.PushNewDrawingGroup();
			GuidelineSet guidelineSet = new GuidelineSet(null, new double[]
			{
				leadingCoordinate,
				offsetToDrivenCoordinate
			}, true);
			guidelineSet.Freeze();
			this._currentDrawingGroup.GuidelineSet = guidelineSet;
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x0008617C File Offset: 0x0008557C
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		public override void PushEffect(BitmapEffect effect, BitmapEffectInput effectInput)
		{
			this.VerifyApiNonstructuralChange();
			this.PushNewDrawingGroup();
			this._currentDrawingGroup.BitmapEffect = effect;
			this._currentDrawingGroup.BitmapEffectInput = ((effectInput != null) ? effectInput : new BitmapEffectInput());
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x000861B8 File Offset: 0x000855B8
		public override void Pop()
		{
			this.VerifyApiNonstructuralChange();
			if (this._previousDrawingGroupStack == null || this._previousDrawingGroupStack.Count == 0)
			{
				throw new InvalidOperationException(SR.Get("DrawingContext_TooManyPops"));
			}
			this._currentDrawingGroup = this._previousDrawingGroupStack.Pop();
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x00086204 File Offset: 0x00085604
		public override void DrawGlyphRun(Brush foregroundBrush, GlyphRun glyphRun)
		{
			this.VerifyApiNonstructuralChange();
			if (foregroundBrush == null || glyphRun == null)
			{
				return;
			}
			GlyphRunDrawing glyphRunDrawing = new GlyphRunDrawing();
			glyphRunDrawing.CanBeInheritanceContext = this.CanBeInheritanceContext;
			glyphRunDrawing.ForegroundBrush = foregroundBrush;
			glyphRunDrawing.GlyphRun = glyphRun;
			this.SetupNewFreezable(glyphRunDrawing, foregroundBrush.IsFrozen);
			this.AddDrawing(glyphRunDrawing);
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x00086254 File Offset: 0x00085654
		public override void Close()
		{
			this.VerifyNotDisposed();
			((IDisposable)this).Dispose();
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x00086270 File Offset: 0x00085670
		protected override void DisposeCore()
		{
			if (!this._disposed)
			{
				if (this._previousDrawingGroupStack != null)
				{
					int count = this._previousDrawingGroupStack.Count;
					for (int i = 0; i < count; i++)
					{
						this.Pop();
					}
				}
				DrawingCollection drawingCollection;
				if (this._currentDrawingGroup != null)
				{
					drawingCollection = this._currentDrawingGroup.Children;
				}
				else
				{
					drawingCollection = new DrawingCollection();
					drawingCollection.CanBeInheritanceContext = this.CanBeInheritanceContext;
					if (this._rootDrawing != null)
					{
						drawingCollection.Add(this._rootDrawing);
					}
				}
				this.CloseCore(drawingCollection);
				this._disposed = true;
			}
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x000862F8 File Offset: 0x000856F8
		protected virtual void CloseCore(DrawingCollection rootDrawingGroupChildren)
		{
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x00086308 File Offset: 0x00085708
		protected override void VerifyApiNonstructuralChange()
		{
			base.VerifyApiNonstructuralChange();
			this.VerifyNotDisposed();
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06002139 RID: 8505 RVA: 0x00086324 File Offset: 0x00085724
		// (set) Token: 0x0600213A RID: 8506 RVA: 0x00086338 File Offset: 0x00085738
		internal bool CanBeInheritanceContext
		{
			get
			{
				return this._canBeInheritanceContext;
			}
			set
			{
				this._canBeInheritanceContext = value;
			}
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x0008634C File Offset: 0x0008574C
		private void VerifyNotDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DrawingDrawingContext");
			}
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x0008636C File Offset: 0x0008576C
		private Freezable SetupNewFreezable(Freezable newFreezable, bool fFreeze)
		{
			if (fFreeze)
			{
				newFreezable.Freeze();
			}
			return newFreezable;
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x00086384 File Offset: 0x00085784
		private void AddNewGeometryDrawing(Brush brush, Pen pen, Geometry geometry)
		{
			GeometryDrawing geometryDrawing = new GeometryDrawing();
			geometryDrawing.CanBeInheritanceContext = this.CanBeInheritanceContext;
			geometryDrawing.Brush = brush;
			geometryDrawing.Pen = pen;
			geometryDrawing.Geometry = geometry;
			this.SetupNewFreezable(geometryDrawing, (brush == null || brush.IsFrozen) && (pen == null || pen.IsFrozen) && geometry.IsFrozen);
			this.AddDrawing(geometryDrawing);
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x000863E8 File Offset: 0x000857E8
		private void PushNewDrawingGroup()
		{
			DrawingGroup drawingGroup = new DrawingGroup();
			drawingGroup.CanBeInheritanceContext = this.CanBeInheritanceContext;
			this.SetupNewFreezable(drawingGroup, false);
			this.AddDrawing(drawingGroup);
			if (this._previousDrawingGroupStack == null)
			{
				this._previousDrawingGroupStack = new Stack<DrawingGroup>(2);
			}
			this._previousDrawingGroupStack.Push(this._currentDrawingGroup);
			this._currentDrawingGroup = drawingGroup;
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x00086444 File Offset: 0x00085844
		private void AddDrawing(Drawing newDrawing)
		{
			if (this._rootDrawing == null)
			{
				this._rootDrawing = newDrawing;
				return;
			}
			if (this._currentDrawingGroup == null)
			{
				this._currentDrawingGroup = new DrawingGroup();
				this._currentDrawingGroup.CanBeInheritanceContext = this.CanBeInheritanceContext;
				this.SetupNewFreezable(this._currentDrawingGroup, false);
				this._currentDrawingGroup.Children.Add(this._rootDrawing);
				this._currentDrawingGroup.Children.Add(newDrawing);
				this._rootDrawing = this._currentDrawingGroup;
				return;
			}
			this._currentDrawingGroup.Children.Add(newDrawing);
		}

		// Token: 0x040010A4 RID: 4260
		protected Drawing _rootDrawing;

		// Token: 0x040010A5 RID: 4261
		protected DrawingGroup _currentDrawingGroup;

		// Token: 0x040010A6 RID: 4262
		private Stack<DrawingGroup> _previousDrawingGroupStack;

		// Token: 0x040010A7 RID: 4263
		private bool _disposed;

		// Token: 0x040010A8 RID: 4264
		private bool _canBeInheritanceContext = true;
	}
}
