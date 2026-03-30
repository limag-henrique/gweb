using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Media.Effects;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	// Token: 0x020003EB RID: 1003
	internal class RenderDataDrawingContext : DrawingContext, IDisposable
	{
		// Token: 0x06002710 RID: 10000 RVA: 0x0009D668 File Offset: 0x0009CA68
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public unsafe override void DrawLine(Pen pen, Point point0, Point point1)
		{
			this.VerifyApiNonstructuralChange();
			if (pen == null)
			{
				return;
			}
			this.EnsureRenderData();
			MILCMD_DRAW_LINE milcmd_DRAW_LINE = new MILCMD_DRAW_LINE(this._renderData.AddDependentResource(pen), point0, point1);
			this._renderData.WriteDataRecord(MILCMD.MilDrawLine, (byte*)(&milcmd_DRAW_LINE), 40);
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x0009D6AC File Offset: 0x0009CAAC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public unsafe override void DrawLine(Pen pen, Point point0, AnimationClock point0Animations, Point point1, AnimationClock point1Animations)
		{
			this.VerifyApiNonstructuralChange();
			if (pen == null)
			{
				return;
			}
			this.EnsureRenderData();
			uint hPoint0Animations = this.UseAnimations(point0, point0Animations);
			uint hPoint1Animations = this.UseAnimations(point1, point1Animations);
			MILCMD_DRAW_LINE_ANIMATE milcmd_DRAW_LINE_ANIMATE = new MILCMD_DRAW_LINE_ANIMATE(this._renderData.AddDependentResource(pen), point0, hPoint0Animations, point1, hPoint1Animations);
			this._renderData.WriteDataRecord(MILCMD.MilDrawLineAnimate, (byte*)(&milcmd_DRAW_LINE_ANIMATE), 48);
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x0009D70C File Offset: 0x0009CB0C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public unsafe override void DrawRectangle(Brush brush, Pen pen, Rect rectangle)
		{
			this.VerifyApiNonstructuralChange();
			if (brush == null && pen == null)
			{
				return;
			}
			this.EnsureRenderData();
			MILCMD_DRAW_RECTANGLE milcmd_DRAW_RECTANGLE = new MILCMD_DRAW_RECTANGLE(this._renderData.AddDependentResource(brush), this._renderData.AddDependentResource(pen), rectangle);
			this._renderData.WriteDataRecord(MILCMD.MilDrawRectangle, (byte*)(&milcmd_DRAW_RECTANGLE), 40);
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x0009D760 File Offset: 0x0009CB60
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public unsafe override void DrawRectangle(Brush brush, Pen pen, Rect rectangle, AnimationClock rectangleAnimations)
		{
			this.VerifyApiNonstructuralChange();
			if (brush == null && pen == null)
			{
				return;
			}
			this.EnsureRenderData();
			uint hRectangleAnimations = this.UseAnimations(rectangle, rectangleAnimations);
			MILCMD_DRAW_RECTANGLE_ANIMATE milcmd_DRAW_RECTANGLE_ANIMATE = new MILCMD_DRAW_RECTANGLE_ANIMATE(this._renderData.AddDependentResource(brush), this._renderData.AddDependentResource(pen), rectangle, hRectangleAnimations);
			this._renderData.WriteDataRecord(MILCMD.MilDrawRectangleAnimate, (byte*)(&milcmd_DRAW_RECTANGLE_ANIMATE), 48);
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x0009D7C0 File Offset: 0x0009CBC0
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public unsafe override void DrawRoundedRectangle(Brush brush, Pen pen, Rect rectangle, double radiusX, double radiusY)
		{
			this.VerifyApiNonstructuralChange();
			if (brush == null && pen == null)
			{
				return;
			}
			this.EnsureRenderData();
			MILCMD_DRAW_ROUNDED_RECTANGLE milcmd_DRAW_ROUNDED_RECTANGLE = new MILCMD_DRAW_ROUNDED_RECTANGLE(this._renderData.AddDependentResource(brush), this._renderData.AddDependentResource(pen), rectangle, radiusX, radiusY);
			this._renderData.WriteDataRecord(MILCMD.MilDrawRoundedRectangle, (byte*)(&milcmd_DRAW_ROUNDED_RECTANGLE), 56);
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x0009D818 File Offset: 0x0009CC18
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public unsafe override void DrawRoundedRectangle(Brush brush, Pen pen, Rect rectangle, AnimationClock rectangleAnimations, double radiusX, AnimationClock radiusXAnimations, double radiusY, AnimationClock radiusYAnimations)
		{
			this.VerifyApiNonstructuralChange();
			if (brush == null && pen == null)
			{
				return;
			}
			this.EnsureRenderData();
			uint hRectangleAnimations = this.UseAnimations(rectangle, rectangleAnimations);
			uint hRadiusXAnimations = this.UseAnimations(radiusX, radiusXAnimations);
			uint hRadiusYAnimations = this.UseAnimations(radiusY, radiusYAnimations);
			MILCMD_DRAW_ROUNDED_RECTANGLE_ANIMATE milcmd_DRAW_ROUNDED_RECTANGLE_ANIMATE = new MILCMD_DRAW_ROUNDED_RECTANGLE_ANIMATE(this._renderData.AddDependentResource(brush), this._renderData.AddDependentResource(pen), rectangle, hRectangleAnimations, radiusX, hRadiusXAnimations, radiusY, hRadiusYAnimations);
			this._renderData.WriteDataRecord(MILCMD.MilDrawRoundedRectangleAnimate, (byte*)(&milcmd_DRAW_ROUNDED_RECTANGLE_ANIMATE), 72);
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x0009D898 File Offset: 0x0009CC98
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public unsafe override void DrawEllipse(Brush brush, Pen pen, Point center, double radiusX, double radiusY)
		{
			this.VerifyApiNonstructuralChange();
			if (brush == null && pen == null)
			{
				return;
			}
			this.EnsureRenderData();
			MILCMD_DRAW_ELLIPSE milcmd_DRAW_ELLIPSE = new MILCMD_DRAW_ELLIPSE(this._renderData.AddDependentResource(brush), this._renderData.AddDependentResource(pen), center, radiusX, radiusY);
			this._renderData.WriteDataRecord(MILCMD.MilDrawEllipse, (byte*)(&milcmd_DRAW_ELLIPSE), 40);
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x0009D8F0 File Offset: 0x0009CCF0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public unsafe override void DrawEllipse(Brush brush, Pen pen, Point center, AnimationClock centerAnimations, double radiusX, AnimationClock radiusXAnimations, double radiusY, AnimationClock radiusYAnimations)
		{
			this.VerifyApiNonstructuralChange();
			if (brush == null && pen == null)
			{
				return;
			}
			this.EnsureRenderData();
			uint hCenterAnimations = this.UseAnimations(center, centerAnimations);
			uint hRadiusXAnimations = this.UseAnimations(radiusX, radiusXAnimations);
			uint hRadiusYAnimations = this.UseAnimations(radiusY, radiusYAnimations);
			MILCMD_DRAW_ELLIPSE_ANIMATE milcmd_DRAW_ELLIPSE_ANIMATE = new MILCMD_DRAW_ELLIPSE_ANIMATE(this._renderData.AddDependentResource(brush), this._renderData.AddDependentResource(pen), center, hCenterAnimations, radiusX, hRadiusXAnimations, radiusY, hRadiusYAnimations);
			this._renderData.WriteDataRecord(MILCMD.MilDrawEllipseAnimate, (byte*)(&milcmd_DRAW_ELLIPSE_ANIMATE), 56);
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x0009D970 File Offset: 0x0009CD70
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public unsafe override void DrawGeometry(Brush brush, Pen pen, Geometry geometry)
		{
			this.VerifyApiNonstructuralChange();
			if ((brush == null && pen == null) || geometry == null)
			{
				return;
			}
			this.EnsureRenderData();
			MILCMD_DRAW_GEOMETRY milcmd_DRAW_GEOMETRY = new MILCMD_DRAW_GEOMETRY(this._renderData.AddDependentResource(brush), this._renderData.AddDependentResource(pen), this._renderData.AddDependentResource(geometry));
			this._renderData.WriteDataRecord(MILCMD.MilDrawGeometry, (byte*)(&milcmd_DRAW_GEOMETRY), 16);
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x0009D9D0 File Offset: 0x0009CDD0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public unsafe override void DrawImage(ImageSource imageSource, Rect rectangle)
		{
			this.VerifyApiNonstructuralChange();
			if (imageSource == null)
			{
				return;
			}
			this.EnsureRenderData();
			MILCMD_DRAW_IMAGE milcmd_DRAW_IMAGE = new MILCMD_DRAW_IMAGE(this._renderData.AddDependentResource(imageSource), rectangle);
			this._renderData.WriteDataRecord(MILCMD.MilDrawImage, (byte*)(&milcmd_DRAW_IMAGE), 40);
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x0009DA14 File Offset: 0x0009CE14
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public unsafe override void DrawImage(ImageSource imageSource, Rect rectangle, AnimationClock rectangleAnimations)
		{
			this.VerifyApiNonstructuralChange();
			if (imageSource == null)
			{
				return;
			}
			this.EnsureRenderData();
			uint hRectangleAnimations = this.UseAnimations(rectangle, rectangleAnimations);
			MILCMD_DRAW_IMAGE_ANIMATE milcmd_DRAW_IMAGE_ANIMATE = new MILCMD_DRAW_IMAGE_ANIMATE(this._renderData.AddDependentResource(imageSource), rectangle, hRectangleAnimations);
			this._renderData.WriteDataRecord(MILCMD.MilDrawImageAnimate, (byte*)(&milcmd_DRAW_IMAGE_ANIMATE), 40);
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x0009DA64 File Offset: 0x0009CE64
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public unsafe override void DrawGlyphRun(Brush foregroundBrush, GlyphRun glyphRun)
		{
			this.VerifyApiNonstructuralChange();
			if (foregroundBrush == null || glyphRun == null)
			{
				return;
			}
			this.EnsureRenderData();
			MILCMD_DRAW_GLYPH_RUN milcmd_DRAW_GLYPH_RUN = new MILCMD_DRAW_GLYPH_RUN(this._renderData.AddDependentResource(foregroundBrush), this._renderData.AddDependentResource(glyphRun));
			this._renderData.WriteDataRecord(MILCMD.MilDrawGlyphRun, (byte*)(&milcmd_DRAW_GLYPH_RUN), 8);
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x0009DAB4 File Offset: 0x0009CEB4
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public unsafe override void DrawDrawing(Drawing drawing)
		{
			this.VerifyApiNonstructuralChange();
			if (drawing == null)
			{
				return;
			}
			this.EnsureRenderData();
			MILCMD_DRAW_DRAWING milcmd_DRAW_DRAWING = new MILCMD_DRAW_DRAWING(this._renderData.AddDependentResource(drawing));
			this._renderData.WriteDataRecord(MILCMD.MilDrawDrawing, (byte*)(&milcmd_DRAW_DRAWING), 8);
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x0009DAF8 File Offset: 0x0009CEF8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public unsafe override void DrawVideo(MediaPlayer player, Rect rectangle)
		{
			this.VerifyApiNonstructuralChange();
			if (player == null)
			{
				return;
			}
			this.EnsureRenderData();
			MILCMD_DRAW_VIDEO milcmd_DRAW_VIDEO = new MILCMD_DRAW_VIDEO(this._renderData.AddDependentResource(player), rectangle);
			this._renderData.WriteDataRecord(MILCMD.MilDrawVideo, (byte*)(&milcmd_DRAW_VIDEO), 40);
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x0009DB3C File Offset: 0x0009CF3C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public unsafe override void DrawVideo(MediaPlayer player, Rect rectangle, AnimationClock rectangleAnimations)
		{
			this.VerifyApiNonstructuralChange();
			if (player == null)
			{
				return;
			}
			this.EnsureRenderData();
			uint hRectangleAnimations = this.UseAnimations(rectangle, rectangleAnimations);
			MILCMD_DRAW_VIDEO_ANIMATE milcmd_DRAW_VIDEO_ANIMATE = new MILCMD_DRAW_VIDEO_ANIMATE(this._renderData.AddDependentResource(player), rectangle, hRectangleAnimations);
			this._renderData.WriteDataRecord(MILCMD.MilDrawVideoAnimate, (byte*)(&milcmd_DRAW_VIDEO_ANIMATE), 40);
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x0009DB8C File Offset: 0x0009CF8C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public unsafe override void PushClip(Geometry clipGeometry)
		{
			this.VerifyApiNonstructuralChange();
			this.EnsureRenderData();
			MILCMD_PUSH_CLIP milcmd_PUSH_CLIP = new MILCMD_PUSH_CLIP(this._renderData.AddDependentResource(clipGeometry));
			this._renderData.WriteDataRecord(MILCMD.MilPushClip, (byte*)(&milcmd_PUSH_CLIP), 8);
			this._stackDepth++;
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x0009DBD8 File Offset: 0x0009CFD8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public unsafe override void PushOpacityMask(Brush opacityMask)
		{
			this.VerifyApiNonstructuralChange();
			this.EnsureRenderData();
			MILCMD_PUSH_OPACITY_MASK milcmd_PUSH_OPACITY_MASK = new MILCMD_PUSH_OPACITY_MASK(this._renderData.AddDependentResource(opacityMask));
			this._renderData.WriteDataRecord(MILCMD.MilPushOpacityMask, (byte*)(&milcmd_PUSH_OPACITY_MASK), 24);
			this._stackDepth++;
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x0009DC24 File Offset: 0x0009D024
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public unsafe override void PushOpacity(double opacity)
		{
			this.VerifyApiNonstructuralChange();
			this.EnsureRenderData();
			MILCMD_PUSH_OPACITY milcmd_PUSH_OPACITY = new MILCMD_PUSH_OPACITY(opacity);
			this._renderData.WriteDataRecord(MILCMD.MilPushOpacity, (byte*)(&milcmd_PUSH_OPACITY), 8);
			this._stackDepth++;
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x0009DC64 File Offset: 0x0009D064
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public unsafe override void PushOpacity(double opacity, AnimationClock opacityAnimations)
		{
			this.VerifyApiNonstructuralChange();
			this.EnsureRenderData();
			uint hOpacityAnimations = this.UseAnimations(opacity, opacityAnimations);
			MILCMD_PUSH_OPACITY_ANIMATE milcmd_PUSH_OPACITY_ANIMATE = new MILCMD_PUSH_OPACITY_ANIMATE(opacity, hOpacityAnimations);
			this._renderData.WriteDataRecord(MILCMD.MilPushOpacityAnimate, (byte*)(&milcmd_PUSH_OPACITY_ANIMATE), 16);
			this._stackDepth++;
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x0009DCB4 File Offset: 0x0009D0B4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public unsafe override void PushTransform(Transform transform)
		{
			this.VerifyApiNonstructuralChange();
			this.EnsureRenderData();
			MILCMD_PUSH_TRANSFORM milcmd_PUSH_TRANSFORM = new MILCMD_PUSH_TRANSFORM(this._renderData.AddDependentResource(transform));
			this._renderData.WriteDataRecord(MILCMD.MilPushTransform, (byte*)(&milcmd_PUSH_TRANSFORM), 8);
			this._stackDepth++;
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x0009DD00 File Offset: 0x0009D100
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public unsafe override void PushGuidelineSet(GuidelineSet guidelines)
		{
			this.VerifyApiNonstructuralChange();
			this.EnsureRenderData();
			if (guidelines != null && guidelines.IsFrozen && guidelines.IsDynamic)
			{
				DoubleCollection guidelinesX = guidelines.GuidelinesX;
				DoubleCollection guidelinesY = guidelines.GuidelinesY;
				int num = (guidelinesX == null) ? 0 : guidelinesX.Count;
				int num2 = (guidelinesY == null) ? 0 : guidelinesY.Count;
				if (num == 0 && (num2 == 1 || num2 == 2))
				{
					if (num2 == 1)
					{
						MILCMD_PUSH_GUIDELINE_Y1 milcmd_PUSH_GUIDELINE_Y = new MILCMD_PUSH_GUIDELINE_Y1(guidelinesY[0]);
						this._renderData.WriteDataRecord(MILCMD.MilPushGuidelineY1, (byte*)(&milcmd_PUSH_GUIDELINE_Y), sizeof(MILCMD_PUSH_GUIDELINE_Y1));
					}
					else
					{
						MILCMD_PUSH_GUIDELINE_Y2 milcmd_PUSH_GUIDELINE_Y2 = new MILCMD_PUSH_GUIDELINE_Y2(guidelinesY[0], guidelinesY[1] - guidelinesY[0]);
						this._renderData.WriteDataRecord(MILCMD.MilPushGuidelineY2, (byte*)(&milcmd_PUSH_GUIDELINE_Y2), sizeof(MILCMD_PUSH_GUIDELINE_Y2));
					}
				}
			}
			else
			{
				MILCMD_PUSH_GUIDELINE_SET milcmd_PUSH_GUIDELINE_SET = new MILCMD_PUSH_GUIDELINE_SET(this._renderData.AddDependentResource(guidelines));
				this._renderData.WriteDataRecord(MILCMD.MilPushGuidelineSet, (byte*)(&milcmd_PUSH_GUIDELINE_SET), 8);
			}
			this._stackDepth++;
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x0009DE00 File Offset: 0x0009D200
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void PushGuidelineY1(double coordinate)
		{
			this.VerifyApiNonstructuralChange();
			this.EnsureRenderData();
			MILCMD_PUSH_GUIDELINE_Y1 milcmd_PUSH_GUIDELINE_Y = new MILCMD_PUSH_GUIDELINE_Y1(coordinate);
			this._renderData.WriteDataRecord(MILCMD.MilPushGuidelineY1, (byte*)(&milcmd_PUSH_GUIDELINE_Y), 8);
			this._stackDepth++;
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x0009DE40 File Offset: 0x0009D240
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void PushGuidelineY2(double leadingCoordinate, double offsetToDrivenCoordinate)
		{
			this.VerifyApiNonstructuralChange();
			this.EnsureRenderData();
			MILCMD_PUSH_GUIDELINE_Y2 milcmd_PUSH_GUIDELINE_Y = new MILCMD_PUSH_GUIDELINE_Y2(leadingCoordinate, offsetToDrivenCoordinate);
			this._renderData.WriteDataRecord(MILCMD.MilPushGuidelineY2, (byte*)(&milcmd_PUSH_GUIDELINE_Y), 16);
			this._stackDepth++;
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x0009DE84 File Offset: 0x0009D284
		[SecurityCritical]
		[SecurityTreatAsSafe]
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		public unsafe override void PushEffect(BitmapEffect effect, BitmapEffectInput effectInput)
		{
			this.VerifyApiNonstructuralChange();
			this.EnsureRenderData();
			MILCMD_PUSH_EFFECT milcmd_PUSH_EFFECT = new MILCMD_PUSH_EFFECT(this._renderData.AddDependentResource(effect), this._renderData.AddDependentResource(effectInput));
			this._renderData.WriteDataRecord(MILCMD.MilPushEffect, (byte*)(&milcmd_PUSH_EFFECT), 8);
			this._stackDepth++;
			if (this._renderData.BitmapEffectStackDepth == 0)
			{
				this._renderData.BeginTopLevelBitmapEffect(this._stackDepth);
			}
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x0009DEFC File Offset: 0x0009D2FC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public unsafe override void Pop()
		{
			this.VerifyApiNonstructuralChange();
			if (this._stackDepth <= 0)
			{
				throw new InvalidOperationException(SR.Get("DrawingContext_TooManyPops"));
			}
			this.EnsureRenderData();
			MILCMD_POP milcmd_POP = default(MILCMD_POP);
			this._renderData.WriteDataRecord(MILCMD.MilPop, (byte*)(&milcmd_POP), 0);
			this._stackDepth--;
			if (this._renderData.BitmapEffectStackDepth == this._stackDepth + 1)
			{
				this._renderData.EndTopLevelBitmapEffect();
			}
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x0009DF78 File Offset: 0x0009D378
		private uint UseAnimations(double baseValue, AnimationClock animations)
		{
			if (animations == null)
			{
				return 0U;
			}
			return this._renderData.AddDependentResource(new DoubleAnimationClockResource(baseValue, animations));
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x0009DF9C File Offset: 0x0009D39C
		private uint UseAnimations(Point baseValue, AnimationClock animations)
		{
			if (animations == null)
			{
				return 0U;
			}
			return this._renderData.AddDependentResource(new PointAnimationClockResource(baseValue, animations));
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x0009DFC0 File Offset: 0x0009D3C0
		private uint UseAnimations(Size baseValue, AnimationClock animations)
		{
			if (animations == null)
			{
				return 0U;
			}
			return this._renderData.AddDependentResource(new SizeAnimationClockResource(baseValue, animations));
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x0009DFE4 File Offset: 0x0009D3E4
		private uint UseAnimations(Rect baseValue, AnimationClock animations)
		{
			if (animations == null)
			{
				return 0U;
			}
			return this._renderData.AddDependentResource(new RectAnimationClockResource(baseValue, animations));
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x0009E008 File Offset: 0x0009D408
		internal RenderDataDrawingContext()
		{
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x0009E01C File Offset: 0x0009D41C
		internal RenderData GetRenderData()
		{
			return this._renderData;
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x0009E030 File Offset: 0x0009D430
		public override void Close()
		{
			this.VerifyApiNonstructuralChange();
			((IDisposable)this).Dispose();
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x0009E04C File Offset: 0x0009D44C
		protected override void DisposeCore()
		{
			if (!this._disposed)
			{
				this.EnsureCorrectNesting();
				this.CloseCore(this._renderData);
				this._disposed = true;
			}
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x0009E07C File Offset: 0x0009D47C
		protected virtual void CloseCore(RenderData renderData)
		{
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x0009E08C File Offset: 0x0009D48C
		private void EnsureRenderData()
		{
			if (this._renderData == null)
			{
				this._renderData = new RenderData();
			}
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x0009E0AC File Offset: 0x0009D4AC
		protected override void VerifyApiNonstructuralChange()
		{
			base.VerifyApiNonstructuralChange();
			if (this._disposed)
			{
				throw new ObjectDisposedException("RenderDataDrawingContext");
			}
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x0009E0D4 File Offset: 0x0009D4D4
		private void EnsureCorrectNesting()
		{
			if (this._renderData != null && this._stackDepth > 0)
			{
				int stackDepth = this._stackDepth;
				for (int i = 0; i < stackDepth; i++)
				{
					this.Pop();
				}
			}
			this._stackDepth = 0;
		}

		// Token: 0x04001240 RID: 4672
		private RenderData _renderData;

		// Token: 0x04001241 RID: 4673
		private bool _disposed;

		// Token: 0x04001242 RID: 4674
		private int _stackDepth;
	}
}
