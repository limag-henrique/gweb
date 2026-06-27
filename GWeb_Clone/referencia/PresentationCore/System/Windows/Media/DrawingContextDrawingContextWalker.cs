using System;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace System.Windows.Media
{
	// Token: 0x02000382 RID: 898
	internal class DrawingContextDrawingContextWalker : DrawingContextWalker
	{
		// Token: 0x060020E2 RID: 8418 RVA: 0x00085774 File Offset: 0x00084B74
		public DrawingContextDrawingContextWalker(DrawingContext drawingContext)
		{
			this._drawingContext = drawingContext;
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x00085790 File Offset: 0x00084B90
		public override void DrawLine(Pen pen, Point point0, Point point1)
		{
			this._drawingContext.DrawLine(pen, point0, point1);
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x000857AC File Offset: 0x00084BAC
		public override void DrawLine(Pen pen, Point point0, AnimationClock point0Animations, Point point1, AnimationClock point1Animations)
		{
			this._drawingContext.DrawLine(pen, point0, point0Animations, point1, point1Animations);
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x000857CC File Offset: 0x00084BCC
		public override void DrawRectangle(Brush brush, Pen pen, Rect rectangle)
		{
			this._drawingContext.DrawRectangle(brush, pen, rectangle);
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x000857E8 File Offset: 0x00084BE8
		public override void DrawRectangle(Brush brush, Pen pen, Rect rectangle, AnimationClock rectangleAnimations)
		{
			this._drawingContext.DrawRectangle(brush, pen, rectangle, rectangleAnimations);
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x00085808 File Offset: 0x00084C08
		public override void DrawRoundedRectangle(Brush brush, Pen pen, Rect rectangle, double radiusX, double radiusY)
		{
			this._drawingContext.DrawRoundedRectangle(brush, pen, rectangle, radiusX, radiusY);
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x00085828 File Offset: 0x00084C28
		public override void DrawRoundedRectangle(Brush brush, Pen pen, Rect rectangle, AnimationClock rectangleAnimations, double radiusX, AnimationClock radiusXAnimations, double radiusY, AnimationClock radiusYAnimations)
		{
			this._drawingContext.DrawRoundedRectangle(brush, pen, rectangle, rectangleAnimations, radiusX, radiusXAnimations, radiusY, radiusYAnimations);
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x00085850 File Offset: 0x00084C50
		public override void DrawEllipse(Brush brush, Pen pen, Point center, double radiusX, double radiusY)
		{
			this._drawingContext.DrawEllipse(brush, pen, center, radiusX, radiusY);
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x00085870 File Offset: 0x00084C70
		public override void DrawEllipse(Brush brush, Pen pen, Point center, AnimationClock centerAnimations, double radiusX, AnimationClock radiusXAnimations, double radiusY, AnimationClock radiusYAnimations)
		{
			this._drawingContext.DrawEllipse(brush, pen, center, centerAnimations, radiusX, radiusXAnimations, radiusY, radiusYAnimations);
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x00085898 File Offset: 0x00084C98
		public override void DrawGeometry(Brush brush, Pen pen, Geometry geometry)
		{
			this._drawingContext.DrawGeometry(brush, pen, geometry);
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x000858B4 File Offset: 0x00084CB4
		public override void DrawImage(ImageSource imageSource, Rect rectangle)
		{
			this._drawingContext.DrawImage(imageSource, rectangle);
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x000858D0 File Offset: 0x00084CD0
		public override void DrawImage(ImageSource imageSource, Rect rectangle, AnimationClock rectangleAnimations)
		{
			this._drawingContext.DrawImage(imageSource, rectangle, rectangleAnimations);
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x000858EC File Offset: 0x00084CEC
		public override void DrawGlyphRun(Brush foregroundBrush, GlyphRun glyphRun)
		{
			this._drawingContext.DrawGlyphRun(foregroundBrush, glyphRun);
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x00085908 File Offset: 0x00084D08
		public override void DrawDrawing(Drawing drawing)
		{
			this._drawingContext.DrawDrawing(drawing);
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x00085924 File Offset: 0x00084D24
		public override void DrawVideo(MediaPlayer player, Rect rectangle)
		{
			this._drawingContext.DrawVideo(player, rectangle);
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x00085940 File Offset: 0x00084D40
		public override void DrawVideo(MediaPlayer player, Rect rectangle, AnimationClock rectangleAnimations)
		{
			this._drawingContext.DrawVideo(player, rectangle, rectangleAnimations);
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x0008595C File Offset: 0x00084D5C
		public override void PushClip(Geometry clipGeometry)
		{
			this._drawingContext.PushClip(clipGeometry);
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x00085978 File Offset: 0x00084D78
		public override void PushOpacityMask(Brush opacityMask)
		{
			this._drawingContext.PushOpacityMask(opacityMask);
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x00085994 File Offset: 0x00084D94
		public override void PushOpacity(double opacity)
		{
			this._drawingContext.PushOpacity(opacity);
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x000859B0 File Offset: 0x00084DB0
		public override void PushOpacity(double opacity, AnimationClock opacityAnimations)
		{
			this._drawingContext.PushOpacity(opacity, opacityAnimations);
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x000859CC File Offset: 0x00084DCC
		public override void PushTransform(Transform transform)
		{
			this._drawingContext.PushTransform(transform);
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x000859E8 File Offset: 0x00084DE8
		public override void PushGuidelineSet(GuidelineSet guidelines)
		{
			this._drawingContext.PushGuidelineSet(guidelines);
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x00085A04 File Offset: 0x00084E04
		internal override void PushGuidelineY1(double coordinate)
		{
			this._drawingContext.PushGuidelineY1(coordinate);
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x00085A20 File Offset: 0x00084E20
		internal override void PushGuidelineY2(double leadingCoordinate, double offsetToDrivenCoordinate)
		{
			this._drawingContext.PushGuidelineY2(leadingCoordinate, offsetToDrivenCoordinate);
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x00085A3C File Offset: 0x00084E3C
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		public override void PushEffect(BitmapEffect effect, BitmapEffectInput effectInput)
		{
			this._drawingContext.PushEffect(effect, effectInput);
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x00085A58 File Offset: 0x00084E58
		public override void Pop()
		{
			this._drawingContext.Pop();
		}

		// Token: 0x040010A2 RID: 4258
		private DrawingContext _drawingContext;
	}
}
