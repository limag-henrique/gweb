using System;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace System.Windows.Media
{
	// Token: 0x02000383 RID: 899
	internal abstract class DrawingContextWalker : DrawingContext
	{
		// Token: 0x060020FD RID: 8445 RVA: 0x00085A84 File Offset: 0x00084E84
		public sealed override void Close()
		{
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x00085A94 File Offset: 0x00084E94
		protected override void DisposeCore()
		{
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x00085AA4 File Offset: 0x00084EA4
		protected void StopWalking()
		{
			this._stopWalking = true;
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06002100 RID: 8448 RVA: 0x00085AB8 File Offset: 0x00084EB8
		// (set) Token: 0x06002101 RID: 8449 RVA: 0x00085ACC File Offset: 0x00084ECC
		internal bool ShouldStopWalking
		{
			get
			{
				return this._stopWalking;
			}
			set
			{
				this._stopWalking = value;
			}
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x00085AE0 File Offset: 0x00084EE0
		public override void DrawLine(Pen pen, Point point0, Point point1)
		{
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x00085AF0 File Offset: 0x00084EF0
		public override void DrawLine(Pen pen, Point point0, AnimationClock point0Animations, Point point1, AnimationClock point1Animations)
		{
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x00085B00 File Offset: 0x00084F00
		public override void DrawRectangle(Brush brush, Pen pen, Rect rectangle)
		{
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x00085B10 File Offset: 0x00084F10
		public override void DrawRectangle(Brush brush, Pen pen, Rect rectangle, AnimationClock rectangleAnimations)
		{
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x00085B20 File Offset: 0x00084F20
		public override void DrawRoundedRectangle(Brush brush, Pen pen, Rect rectangle, double radiusX, double radiusY)
		{
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x00085B30 File Offset: 0x00084F30
		public override void DrawRoundedRectangle(Brush brush, Pen pen, Rect rectangle, AnimationClock rectangleAnimations, double radiusX, AnimationClock radiusXAnimations, double radiusY, AnimationClock radiusYAnimations)
		{
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x00085B40 File Offset: 0x00084F40
		public override void DrawEllipse(Brush brush, Pen pen, Point center, double radiusX, double radiusY)
		{
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x00085B50 File Offset: 0x00084F50
		public override void DrawEllipse(Brush brush, Pen pen, Point center, AnimationClock centerAnimations, double radiusX, AnimationClock radiusXAnimations, double radiusY, AnimationClock radiusYAnimations)
		{
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x00085B60 File Offset: 0x00084F60
		public override void DrawGeometry(Brush brush, Pen pen, Geometry geometry)
		{
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x00085B70 File Offset: 0x00084F70
		public override void DrawImage(ImageSource imageSource, Rect rectangle)
		{
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x00085B80 File Offset: 0x00084F80
		public override void DrawImage(ImageSource imageSource, Rect rectangle, AnimationClock rectangleAnimations)
		{
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x00085B90 File Offset: 0x00084F90
		public override void DrawGlyphRun(Brush foregroundBrush, GlyphRun glyphRun)
		{
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x00085BA0 File Offset: 0x00084FA0
		public override void DrawDrawing(Drawing drawing)
		{
			if (drawing != null)
			{
				drawing.WalkCurrentValue(this);
			}
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x00085BB8 File Offset: 0x00084FB8
		public override void DrawVideo(MediaPlayer player, Rect rectangle)
		{
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x00085BC8 File Offset: 0x00084FC8
		public override void DrawVideo(MediaPlayer player, Rect rectangle, AnimationClock rectangleAnimations)
		{
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x00085BD8 File Offset: 0x00084FD8
		public override void PushClip(Geometry clipGeometry)
		{
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x00085BE8 File Offset: 0x00084FE8
		public override void PushOpacityMask(Brush opacityMask)
		{
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x00085BF8 File Offset: 0x00084FF8
		public override void PushOpacity(double opacity)
		{
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x00085C08 File Offset: 0x00085008
		public override void PushOpacity(double opacity, AnimationClock opacityAnimations)
		{
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x00085C18 File Offset: 0x00085018
		public override void PushTransform(Transform transform)
		{
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x00085C28 File Offset: 0x00085028
		public override void PushGuidelineSet(GuidelineSet guidelines)
		{
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x00085C38 File Offset: 0x00085038
		internal override void PushGuidelineY1(double coordinate)
		{
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x00085C48 File Offset: 0x00085048
		internal override void PushGuidelineY2(double leadingCoordinate, double offsetToDrivenCoordinate)
		{
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x00085C58 File Offset: 0x00085058
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		public override void PushEffect(BitmapEffect effect, BitmapEffectInput effectInput)
		{
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x00085C68 File Offset: 0x00085068
		public override void Pop()
		{
		}

		// Token: 0x040010A3 RID: 4259
		private bool _stopWalking;
	}
}
