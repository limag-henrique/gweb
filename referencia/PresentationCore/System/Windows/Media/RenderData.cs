using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Media.Effects;
using MS.Internal;
using MS.Utility;

namespace System.Windows.Media
{
	// Token: 0x020003EA RID: 1002
	internal class RenderData : Freezable, DUCE.IResource, IDrawingContent
	{
		// Token: 0x060026F0 RID: 9968 RVA: 0x0009B630 File Offset: 0x0009AA30
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private unsafe void MarshalToDUCE(DUCE.Channel channel)
		{
			DUCE.MILCMD_RENDERDATA milcmd_RENDERDATA;
			milcmd_RENDERDATA.Type = MILCMD.MilCmdRenderData;
			milcmd_RENDERDATA.Handle = this._duceResource.GetHandle(channel);
			milcmd_RENDERDATA.cbData = (uint)this.DataSize;
			uint cbData = milcmd_RENDERDATA.cbData;
			channel.BeginCommand((byte*)(&milcmd_RENDERDATA), sizeof(DUCE.MILCMD_RENDERDATA), (int)cbData);
			Stack<RenderData.PushType> stack = new Stack<RenderData.PushType>();
			int num = 0;
			if (this._curOffset > 0)
			{
				byte[] array;
				byte* ptr;
				if ((array = this._buffer) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				byte* ptr2 = ptr;
				byte* ptr3 = ptr + this._curOffset;
				while (ptr2 < ptr3)
				{
					RenderData.RecordHeader* ptr4 = (RenderData.RecordHeader*)ptr2;
					channel.AppendCommandData((byte*)ptr4, sizeof(RenderData.RecordHeader));
					switch (ptr4->Id)
					{
					case MILCMD.MilDrawLine:
					{
						MILCMD_DRAW_LINE milcmd_DRAW_LINE = *(MILCMD_DRAW_LINE*)(ptr2 + sizeof(RenderData.RecordHeader));
						if (milcmd_DRAW_LINE.hPen != 0U)
						{
							milcmd_DRAW_LINE.hPen = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_LINE.hPen - 1U)]).GetHandle(channel);
						}
						channel.AppendCommandData((byte*)(&milcmd_DRAW_LINE), 40);
						break;
					}
					case MILCMD.MilDrawLineAnimate:
					{
						MILCMD_DRAW_LINE_ANIMATE milcmd_DRAW_LINE_ANIMATE = *(MILCMD_DRAW_LINE_ANIMATE*)(ptr2 + sizeof(RenderData.RecordHeader));
						if (milcmd_DRAW_LINE_ANIMATE.hPen != 0U)
						{
							milcmd_DRAW_LINE_ANIMATE.hPen = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_LINE_ANIMATE.hPen - 1U)]).GetHandle(channel);
						}
						if (milcmd_DRAW_LINE_ANIMATE.hPoint0Animations != 0U)
						{
							milcmd_DRAW_LINE_ANIMATE.hPoint0Animations = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_LINE_ANIMATE.hPoint0Animations - 1U)]).GetHandle(channel);
						}
						if (milcmd_DRAW_LINE_ANIMATE.hPoint1Animations != 0U)
						{
							milcmd_DRAW_LINE_ANIMATE.hPoint1Animations = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_LINE_ANIMATE.hPoint1Animations - 1U)]).GetHandle(channel);
						}
						channel.AppendCommandData((byte*)(&milcmd_DRAW_LINE_ANIMATE), 48);
						break;
					}
					case MILCMD.MilDrawRectangle:
					{
						MILCMD_DRAW_RECTANGLE milcmd_DRAW_RECTANGLE = *(MILCMD_DRAW_RECTANGLE*)(ptr2 + sizeof(RenderData.RecordHeader));
						if (milcmd_DRAW_RECTANGLE.hBrush != 0U)
						{
							milcmd_DRAW_RECTANGLE.hBrush = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_RECTANGLE.hBrush - 1U)]).GetHandle(channel);
						}
						if (milcmd_DRAW_RECTANGLE.hPen != 0U)
						{
							milcmd_DRAW_RECTANGLE.hPen = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_RECTANGLE.hPen - 1U)]).GetHandle(channel);
						}
						channel.AppendCommandData((byte*)(&milcmd_DRAW_RECTANGLE), 40);
						break;
					}
					case MILCMD.MilDrawRectangleAnimate:
					{
						MILCMD_DRAW_RECTANGLE_ANIMATE milcmd_DRAW_RECTANGLE_ANIMATE = *(MILCMD_DRAW_RECTANGLE_ANIMATE*)(ptr2 + sizeof(RenderData.RecordHeader));
						if (milcmd_DRAW_RECTANGLE_ANIMATE.hBrush != 0U)
						{
							milcmd_DRAW_RECTANGLE_ANIMATE.hBrush = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_RECTANGLE_ANIMATE.hBrush - 1U)]).GetHandle(channel);
						}
						if (milcmd_DRAW_RECTANGLE_ANIMATE.hPen != 0U)
						{
							milcmd_DRAW_RECTANGLE_ANIMATE.hPen = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_RECTANGLE_ANIMATE.hPen - 1U)]).GetHandle(channel);
						}
						if (milcmd_DRAW_RECTANGLE_ANIMATE.hRectangleAnimations != 0U)
						{
							milcmd_DRAW_RECTANGLE_ANIMATE.hRectangleAnimations = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_RECTANGLE_ANIMATE.hRectangleAnimations - 1U)]).GetHandle(channel);
						}
						channel.AppendCommandData((byte*)(&milcmd_DRAW_RECTANGLE_ANIMATE), 48);
						break;
					}
					case MILCMD.MilDrawRoundedRectangle:
					{
						MILCMD_DRAW_ROUNDED_RECTANGLE milcmd_DRAW_ROUNDED_RECTANGLE = *(MILCMD_DRAW_ROUNDED_RECTANGLE*)(ptr2 + sizeof(RenderData.RecordHeader));
						if (milcmd_DRAW_ROUNDED_RECTANGLE.hBrush != 0U)
						{
							milcmd_DRAW_ROUNDED_RECTANGLE.hBrush = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_ROUNDED_RECTANGLE.hBrush - 1U)]).GetHandle(channel);
						}
						if (milcmd_DRAW_ROUNDED_RECTANGLE.hPen != 0U)
						{
							milcmd_DRAW_ROUNDED_RECTANGLE.hPen = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_ROUNDED_RECTANGLE.hPen - 1U)]).GetHandle(channel);
						}
						channel.AppendCommandData((byte*)(&milcmd_DRAW_ROUNDED_RECTANGLE), 56);
						break;
					}
					case MILCMD.MilDrawRoundedRectangleAnimate:
					{
						MILCMD_DRAW_ROUNDED_RECTANGLE_ANIMATE milcmd_DRAW_ROUNDED_RECTANGLE_ANIMATE = *(MILCMD_DRAW_ROUNDED_RECTANGLE_ANIMATE*)(ptr2 + sizeof(RenderData.RecordHeader));
						if (milcmd_DRAW_ROUNDED_RECTANGLE_ANIMATE.hBrush != 0U)
						{
							milcmd_DRAW_ROUNDED_RECTANGLE_ANIMATE.hBrush = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_ROUNDED_RECTANGLE_ANIMATE.hBrush - 1U)]).GetHandle(channel);
						}
						if (milcmd_DRAW_ROUNDED_RECTANGLE_ANIMATE.hPen != 0U)
						{
							milcmd_DRAW_ROUNDED_RECTANGLE_ANIMATE.hPen = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_ROUNDED_RECTANGLE_ANIMATE.hPen - 1U)]).GetHandle(channel);
						}
						if (milcmd_DRAW_ROUNDED_RECTANGLE_ANIMATE.hRectangleAnimations != 0U)
						{
							milcmd_DRAW_ROUNDED_RECTANGLE_ANIMATE.hRectangleAnimations = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_ROUNDED_RECTANGLE_ANIMATE.hRectangleAnimations - 1U)]).GetHandle(channel);
						}
						if (milcmd_DRAW_ROUNDED_RECTANGLE_ANIMATE.hRadiusXAnimations != 0U)
						{
							milcmd_DRAW_ROUNDED_RECTANGLE_ANIMATE.hRadiusXAnimations = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_ROUNDED_RECTANGLE_ANIMATE.hRadiusXAnimations - 1U)]).GetHandle(channel);
						}
						if (milcmd_DRAW_ROUNDED_RECTANGLE_ANIMATE.hRadiusYAnimations != 0U)
						{
							milcmd_DRAW_ROUNDED_RECTANGLE_ANIMATE.hRadiusYAnimations = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_ROUNDED_RECTANGLE_ANIMATE.hRadiusYAnimations - 1U)]).GetHandle(channel);
						}
						channel.AppendCommandData((byte*)(&milcmd_DRAW_ROUNDED_RECTANGLE_ANIMATE), 72);
						break;
					}
					case MILCMD.MilDrawEllipse:
					{
						MILCMD_DRAW_ELLIPSE milcmd_DRAW_ELLIPSE = *(MILCMD_DRAW_ELLIPSE*)(ptr2 + sizeof(RenderData.RecordHeader));
						if (milcmd_DRAW_ELLIPSE.hBrush != 0U)
						{
							milcmd_DRAW_ELLIPSE.hBrush = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_ELLIPSE.hBrush - 1U)]).GetHandle(channel);
						}
						if (milcmd_DRAW_ELLIPSE.hPen != 0U)
						{
							milcmd_DRAW_ELLIPSE.hPen = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_ELLIPSE.hPen - 1U)]).GetHandle(channel);
						}
						channel.AppendCommandData((byte*)(&milcmd_DRAW_ELLIPSE), 40);
						break;
					}
					case MILCMD.MilDrawEllipseAnimate:
					{
						MILCMD_DRAW_ELLIPSE_ANIMATE milcmd_DRAW_ELLIPSE_ANIMATE = *(MILCMD_DRAW_ELLIPSE_ANIMATE*)(ptr2 + sizeof(RenderData.RecordHeader));
						if (milcmd_DRAW_ELLIPSE_ANIMATE.hBrush != 0U)
						{
							milcmd_DRAW_ELLIPSE_ANIMATE.hBrush = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_ELLIPSE_ANIMATE.hBrush - 1U)]).GetHandle(channel);
						}
						if (milcmd_DRAW_ELLIPSE_ANIMATE.hPen != 0U)
						{
							milcmd_DRAW_ELLIPSE_ANIMATE.hPen = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_ELLIPSE_ANIMATE.hPen - 1U)]).GetHandle(channel);
						}
						if (milcmd_DRAW_ELLIPSE_ANIMATE.hCenterAnimations != 0U)
						{
							milcmd_DRAW_ELLIPSE_ANIMATE.hCenterAnimations = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_ELLIPSE_ANIMATE.hCenterAnimations - 1U)]).GetHandle(channel);
						}
						if (milcmd_DRAW_ELLIPSE_ANIMATE.hRadiusXAnimations != 0U)
						{
							milcmd_DRAW_ELLIPSE_ANIMATE.hRadiusXAnimations = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_ELLIPSE_ANIMATE.hRadiusXAnimations - 1U)]).GetHandle(channel);
						}
						if (milcmd_DRAW_ELLIPSE_ANIMATE.hRadiusYAnimations != 0U)
						{
							milcmd_DRAW_ELLIPSE_ANIMATE.hRadiusYAnimations = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_ELLIPSE_ANIMATE.hRadiusYAnimations - 1U)]).GetHandle(channel);
						}
						channel.AppendCommandData((byte*)(&milcmd_DRAW_ELLIPSE_ANIMATE), 56);
						break;
					}
					case MILCMD.MilDrawGeometry:
					{
						MILCMD_DRAW_GEOMETRY milcmd_DRAW_GEOMETRY = *(MILCMD_DRAW_GEOMETRY*)(ptr2 + sizeof(RenderData.RecordHeader));
						if (milcmd_DRAW_GEOMETRY.hBrush != 0U)
						{
							milcmd_DRAW_GEOMETRY.hBrush = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_GEOMETRY.hBrush - 1U)]).GetHandle(channel);
						}
						if (milcmd_DRAW_GEOMETRY.hPen != 0U)
						{
							milcmd_DRAW_GEOMETRY.hPen = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_GEOMETRY.hPen - 1U)]).GetHandle(channel);
						}
						if (milcmd_DRAW_GEOMETRY.hGeometry != 0U)
						{
							milcmd_DRAW_GEOMETRY.hGeometry = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_GEOMETRY.hGeometry - 1U)]).GetHandle(channel);
						}
						channel.AppendCommandData((byte*)(&milcmd_DRAW_GEOMETRY), 16);
						break;
					}
					case MILCMD.MilDrawImage:
					{
						MILCMD_DRAW_IMAGE milcmd_DRAW_IMAGE = *(MILCMD_DRAW_IMAGE*)(ptr2 + sizeof(RenderData.RecordHeader));
						if (milcmd_DRAW_IMAGE.hImageSource != 0U)
						{
							milcmd_DRAW_IMAGE.hImageSource = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_IMAGE.hImageSource - 1U)]).GetHandle(channel);
						}
						channel.AppendCommandData((byte*)(&milcmd_DRAW_IMAGE), 40);
						break;
					}
					case MILCMD.MilDrawImageAnimate:
					{
						MILCMD_DRAW_IMAGE_ANIMATE milcmd_DRAW_IMAGE_ANIMATE = *(MILCMD_DRAW_IMAGE_ANIMATE*)(ptr2 + sizeof(RenderData.RecordHeader));
						if (milcmd_DRAW_IMAGE_ANIMATE.hImageSource != 0U)
						{
							milcmd_DRAW_IMAGE_ANIMATE.hImageSource = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_IMAGE_ANIMATE.hImageSource - 1U)]).GetHandle(channel);
						}
						if (milcmd_DRAW_IMAGE_ANIMATE.hRectangleAnimations != 0U)
						{
							milcmd_DRAW_IMAGE_ANIMATE.hRectangleAnimations = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_IMAGE_ANIMATE.hRectangleAnimations - 1U)]).GetHandle(channel);
						}
						channel.AppendCommandData((byte*)(&milcmd_DRAW_IMAGE_ANIMATE), 40);
						break;
					}
					case MILCMD.MilDrawGlyphRun:
					{
						MILCMD_DRAW_GLYPH_RUN milcmd_DRAW_GLYPH_RUN = *(MILCMD_DRAW_GLYPH_RUN*)(ptr2 + sizeof(RenderData.RecordHeader));
						if (milcmd_DRAW_GLYPH_RUN.hForegroundBrush != 0U)
						{
							milcmd_DRAW_GLYPH_RUN.hForegroundBrush = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_GLYPH_RUN.hForegroundBrush - 1U)]).GetHandle(channel);
						}
						if (milcmd_DRAW_GLYPH_RUN.hGlyphRun != 0U)
						{
							milcmd_DRAW_GLYPH_RUN.hGlyphRun = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_GLYPH_RUN.hGlyphRun - 1U)]).GetHandle(channel);
						}
						channel.AppendCommandData((byte*)(&milcmd_DRAW_GLYPH_RUN), 8);
						break;
					}
					case MILCMD.MilDrawDrawing:
					{
						MILCMD_DRAW_DRAWING milcmd_DRAW_DRAWING = *(MILCMD_DRAW_DRAWING*)(ptr2 + sizeof(RenderData.RecordHeader));
						if (milcmd_DRAW_DRAWING.hDrawing != 0U)
						{
							milcmd_DRAW_DRAWING.hDrawing = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_DRAWING.hDrawing - 1U)]).GetHandle(channel);
						}
						channel.AppendCommandData((byte*)(&milcmd_DRAW_DRAWING), 8);
						break;
					}
					case MILCMD.MilDrawVideo:
					{
						MILCMD_DRAW_VIDEO milcmd_DRAW_VIDEO = *(MILCMD_DRAW_VIDEO*)(ptr2 + sizeof(RenderData.RecordHeader));
						if (milcmd_DRAW_VIDEO.hPlayer != 0U)
						{
							milcmd_DRAW_VIDEO.hPlayer = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_VIDEO.hPlayer - 1U)]).GetHandle(channel);
						}
						channel.AppendCommandData((byte*)(&milcmd_DRAW_VIDEO), 40);
						break;
					}
					case MILCMD.MilDrawVideoAnimate:
					{
						MILCMD_DRAW_VIDEO_ANIMATE milcmd_DRAW_VIDEO_ANIMATE = *(MILCMD_DRAW_VIDEO_ANIMATE*)(ptr2 + sizeof(RenderData.RecordHeader));
						if (milcmd_DRAW_VIDEO_ANIMATE.hPlayer != 0U)
						{
							milcmd_DRAW_VIDEO_ANIMATE.hPlayer = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_VIDEO_ANIMATE.hPlayer - 1U)]).GetHandle(channel);
						}
						if (milcmd_DRAW_VIDEO_ANIMATE.hRectangleAnimations != 0U)
						{
							milcmd_DRAW_VIDEO_ANIMATE.hRectangleAnimations = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_DRAW_VIDEO_ANIMATE.hRectangleAnimations - 1U)]).GetHandle(channel);
						}
						channel.AppendCommandData((byte*)(&milcmd_DRAW_VIDEO_ANIMATE), 40);
						break;
					}
					case MILCMD.MilPushClip:
					{
						stack.Push(RenderData.PushType.Other);
						MILCMD_PUSH_CLIP milcmd_PUSH_CLIP = *(MILCMD_PUSH_CLIP*)(ptr2 + sizeof(RenderData.RecordHeader));
						if (milcmd_PUSH_CLIP.hClipGeometry != 0U)
						{
							milcmd_PUSH_CLIP.hClipGeometry = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_PUSH_CLIP.hClipGeometry - 1U)]).GetHandle(channel);
						}
						channel.AppendCommandData((byte*)(&milcmd_PUSH_CLIP), 8);
						break;
					}
					case MILCMD.MilPushOpacityMask:
					{
						stack.Push(RenderData.PushType.Other);
						MILCMD_PUSH_OPACITY_MASK milcmd_PUSH_OPACITY_MASK = *(MILCMD_PUSH_OPACITY_MASK*)(ptr2 + sizeof(RenderData.RecordHeader));
						if (milcmd_PUSH_OPACITY_MASK.hOpacityMask != 0U)
						{
							milcmd_PUSH_OPACITY_MASK.hOpacityMask = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_PUSH_OPACITY_MASK.hOpacityMask - 1U)]).GetHandle(channel);
						}
						channel.AppendCommandData((byte*)(&milcmd_PUSH_OPACITY_MASK), 24);
						break;
					}
					case MILCMD.MilPushOpacity:
					{
						stack.Push(RenderData.PushType.Other);
						MILCMD_PUSH_OPACITY milcmd_PUSH_OPACITY = *(MILCMD_PUSH_OPACITY*)(ptr2 + sizeof(RenderData.RecordHeader));
						channel.AppendCommandData((byte*)(&milcmd_PUSH_OPACITY), 8);
						break;
					}
					case MILCMD.MilPushOpacityAnimate:
					{
						stack.Push(RenderData.PushType.Other);
						MILCMD_PUSH_OPACITY_ANIMATE milcmd_PUSH_OPACITY_ANIMATE = *(MILCMD_PUSH_OPACITY_ANIMATE*)(ptr2 + sizeof(RenderData.RecordHeader));
						if (milcmd_PUSH_OPACITY_ANIMATE.hOpacityAnimations != 0U)
						{
							milcmd_PUSH_OPACITY_ANIMATE.hOpacityAnimations = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_PUSH_OPACITY_ANIMATE.hOpacityAnimations - 1U)]).GetHandle(channel);
						}
						channel.AppendCommandData((byte*)(&milcmd_PUSH_OPACITY_ANIMATE), 16);
						break;
					}
					case MILCMD.MilPushTransform:
					{
						stack.Push(RenderData.PushType.Other);
						MILCMD_PUSH_TRANSFORM milcmd_PUSH_TRANSFORM = *(MILCMD_PUSH_TRANSFORM*)(ptr2 + sizeof(RenderData.RecordHeader));
						if (milcmd_PUSH_TRANSFORM.hTransform != 0U)
						{
							milcmd_PUSH_TRANSFORM.hTransform = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_PUSH_TRANSFORM.hTransform - 1U)]).GetHandle(channel);
						}
						channel.AppendCommandData((byte*)(&milcmd_PUSH_TRANSFORM), 8);
						break;
					}
					case MILCMD.MilPushGuidelineSet:
					{
						stack.Push(RenderData.PushType.Other);
						MILCMD_PUSH_GUIDELINE_SET milcmd_PUSH_GUIDELINE_SET = *(MILCMD_PUSH_GUIDELINE_SET*)(ptr2 + sizeof(RenderData.RecordHeader));
						if (milcmd_PUSH_GUIDELINE_SET.hGuidelines != 0U)
						{
							milcmd_PUSH_GUIDELINE_SET.hGuidelines = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_PUSH_GUIDELINE_SET.hGuidelines - 1U)]).GetHandle(channel);
						}
						channel.AppendCommandData((byte*)(&milcmd_PUSH_GUIDELINE_SET), 8);
						break;
					}
					case MILCMD.MilPushGuidelineY1:
					{
						stack.Push(RenderData.PushType.Other);
						MILCMD_PUSH_GUIDELINE_Y1 milcmd_PUSH_GUIDELINE_Y = *(MILCMD_PUSH_GUIDELINE_Y1*)(ptr2 + sizeof(RenderData.RecordHeader));
						channel.AppendCommandData((byte*)(&milcmd_PUSH_GUIDELINE_Y), 8);
						break;
					}
					case MILCMD.MilPushGuidelineY2:
					{
						stack.Push(RenderData.PushType.Other);
						MILCMD_PUSH_GUIDELINE_Y2 milcmd_PUSH_GUIDELINE_Y2 = *(MILCMD_PUSH_GUIDELINE_Y2*)(ptr2 + sizeof(RenderData.RecordHeader));
						channel.AppendCommandData((byte*)(&milcmd_PUSH_GUIDELINE_Y2), 16);
						break;
					}
					case MILCMD.MilPushEffect:
					{
						stack.Push(RenderData.PushType.BitmapEffect);
						num++;
						MILCMD_PUSH_EFFECT milcmd_PUSH_EFFECT = *(MILCMD_PUSH_EFFECT*)(ptr2 + sizeof(RenderData.RecordHeader));
						if (milcmd_PUSH_EFFECT.hEffect != 0U)
						{
							milcmd_PUSH_EFFECT.hEffect = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_PUSH_EFFECT.hEffect - 1U)]).GetHandle(channel);
						}
						if (milcmd_PUSH_EFFECT.hEffectInput != 0U)
						{
							milcmd_PUSH_EFFECT.hEffectInput = (uint)((DUCE.IResource)this._dependentResources[(int)(milcmd_PUSH_EFFECT.hEffectInput - 1U)]).GetHandle(channel);
						}
						channel.AppendCommandData((byte*)(&milcmd_PUSH_EFFECT), 8);
						break;
					}
					case MILCMD.MilPop:
						if (stack.Pop() == RenderData.PushType.BitmapEffect)
						{
							num--;
						}
						break;
					}
					ptr2 += ptr4->Size;
				}
				array = null;
			}
			channel.EndCommand();
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x0009C314 File Offset: 0x0009B714
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public unsafe void DrawingContextWalk(DrawingContextWalker ctx)
		{
			if (this._curOffset > 0)
			{
				byte[] array;
				byte* ptr;
				if ((array = this._buffer) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				byte* ptr2 = ptr;
				byte* ptr3 = ptr + this._curOffset;
				while (ptr2 < ptr3 && !ctx.ShouldStopWalking)
				{
					RenderData.RecordHeader* ptr4 = (RenderData.RecordHeader*)ptr2;
					switch (ptr4->Id)
					{
					case MILCMD.MilDrawLine:
					{
						MILCMD_DRAW_LINE* ptr5 = (MILCMD_DRAW_LINE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawLine((Pen)this.DependentLookup(ptr5->hPen), ptr5->point0, ptr5->point1);
						break;
					}
					case MILCMD.MilDrawLineAnimate:
					{
						MILCMD_DRAW_LINE_ANIMATE* ptr6 = (MILCMD_DRAW_LINE_ANIMATE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawLine((ptr6->hPen == 0U) ? null : ((Pen)this.DependentLookup(ptr6->hPen)), (ptr6->hPoint0Animations == 0U) ? ptr6->point0 : ((PointAnimationClockResource)this.DependentLookup(ptr6->hPoint0Animations)).CurrentValue, (ptr6->hPoint1Animations == 0U) ? ptr6->point1 : ((PointAnimationClockResource)this.DependentLookup(ptr6->hPoint1Animations)).CurrentValue);
						break;
					}
					case MILCMD.MilDrawRectangle:
					{
						MILCMD_DRAW_RECTANGLE* ptr7 = (MILCMD_DRAW_RECTANGLE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawRectangle((Brush)this.DependentLookup(ptr7->hBrush), (Pen)this.DependentLookup(ptr7->hPen), ptr7->rectangle);
						break;
					}
					case MILCMD.MilDrawRectangleAnimate:
					{
						MILCMD_DRAW_RECTANGLE_ANIMATE* ptr8 = (MILCMD_DRAW_RECTANGLE_ANIMATE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawRectangle((ptr8->hBrush == 0U) ? null : ((Brush)this.DependentLookup(ptr8->hBrush)), (ptr8->hPen == 0U) ? null : ((Pen)this.DependentLookup(ptr8->hPen)), (ptr8->hRectangleAnimations == 0U) ? ptr8->rectangle : ((RectAnimationClockResource)this.DependentLookup(ptr8->hRectangleAnimations)).CurrentValue);
						break;
					}
					case MILCMD.MilDrawRoundedRectangle:
					{
						MILCMD_DRAW_ROUNDED_RECTANGLE* ptr9 = (MILCMD_DRAW_ROUNDED_RECTANGLE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawRoundedRectangle((Brush)this.DependentLookup(ptr9->hBrush), (Pen)this.DependentLookup(ptr9->hPen), ptr9->rectangle, ptr9->radiusX, ptr9->radiusY);
						break;
					}
					case MILCMD.MilDrawRoundedRectangleAnimate:
					{
						MILCMD_DRAW_ROUNDED_RECTANGLE_ANIMATE* ptr10 = (MILCMD_DRAW_ROUNDED_RECTANGLE_ANIMATE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawRoundedRectangle((ptr10->hBrush == 0U) ? null : ((Brush)this.DependentLookup(ptr10->hBrush)), (ptr10->hPen == 0U) ? null : ((Pen)this.DependentLookup(ptr10->hPen)), (ptr10->hRectangleAnimations == 0U) ? ptr10->rectangle : ((RectAnimationClockResource)this.DependentLookup(ptr10->hRectangleAnimations)).CurrentValue, (ptr10->hRadiusXAnimations == 0U) ? ptr10->radiusX : ((DoubleAnimationClockResource)this.DependentLookup(ptr10->hRadiusXAnimations)).CurrentValue, (ptr10->hRadiusYAnimations == 0U) ? ptr10->radiusY : ((DoubleAnimationClockResource)this.DependentLookup(ptr10->hRadiusYAnimations)).CurrentValue);
						break;
					}
					case MILCMD.MilDrawEllipse:
					{
						MILCMD_DRAW_ELLIPSE* ptr11 = (MILCMD_DRAW_ELLIPSE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawEllipse((Brush)this.DependentLookup(ptr11->hBrush), (Pen)this.DependentLookup(ptr11->hPen), ptr11->center, ptr11->radiusX, ptr11->radiusY);
						break;
					}
					case MILCMD.MilDrawEllipseAnimate:
					{
						MILCMD_DRAW_ELLIPSE_ANIMATE* ptr12 = (MILCMD_DRAW_ELLIPSE_ANIMATE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawEllipse((ptr12->hBrush == 0U) ? null : ((Brush)this.DependentLookup(ptr12->hBrush)), (ptr12->hPen == 0U) ? null : ((Pen)this.DependentLookup(ptr12->hPen)), (ptr12->hCenterAnimations == 0U) ? ptr12->center : ((PointAnimationClockResource)this.DependentLookup(ptr12->hCenterAnimations)).CurrentValue, (ptr12->hRadiusXAnimations == 0U) ? ptr12->radiusX : ((DoubleAnimationClockResource)this.DependentLookup(ptr12->hRadiusXAnimations)).CurrentValue, (ptr12->hRadiusYAnimations == 0U) ? ptr12->radiusY : ((DoubleAnimationClockResource)this.DependentLookup(ptr12->hRadiusYAnimations)).CurrentValue);
						break;
					}
					case MILCMD.MilDrawGeometry:
					{
						MILCMD_DRAW_GEOMETRY* ptr13 = (MILCMD_DRAW_GEOMETRY*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawGeometry((Brush)this.DependentLookup(ptr13->hBrush), (Pen)this.DependentLookup(ptr13->hPen), (Geometry)this.DependentLookup(ptr13->hGeometry));
						break;
					}
					case MILCMD.MilDrawImage:
					{
						MILCMD_DRAW_IMAGE* ptr14 = (MILCMD_DRAW_IMAGE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawImage((ImageSource)this.DependentLookup(ptr14->hImageSource), ptr14->rectangle);
						break;
					}
					case MILCMD.MilDrawImageAnimate:
					{
						MILCMD_DRAW_IMAGE_ANIMATE* ptr15 = (MILCMD_DRAW_IMAGE_ANIMATE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawImage((ptr15->hImageSource == 0U) ? null : ((ImageSource)this.DependentLookup(ptr15->hImageSource)), (ptr15->hRectangleAnimations == 0U) ? ptr15->rectangle : ((RectAnimationClockResource)this.DependentLookup(ptr15->hRectangleAnimations)).CurrentValue);
						break;
					}
					case MILCMD.MilDrawGlyphRun:
					{
						MILCMD_DRAW_GLYPH_RUN* ptr16 = (MILCMD_DRAW_GLYPH_RUN*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawGlyphRun((Brush)this.DependentLookup(ptr16->hForegroundBrush), (GlyphRun)this.DependentLookup(ptr16->hGlyphRun));
						break;
					}
					case MILCMD.MilDrawDrawing:
					{
						MILCMD_DRAW_DRAWING* ptr17 = (MILCMD_DRAW_DRAWING*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawDrawing((Drawing)this.DependentLookup(ptr17->hDrawing));
						break;
					}
					case MILCMD.MilDrawVideo:
					{
						MILCMD_DRAW_VIDEO* ptr18 = (MILCMD_DRAW_VIDEO*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawVideo((MediaPlayer)this.DependentLookup(ptr18->hPlayer), ptr18->rectangle);
						break;
					}
					case MILCMD.MilDrawVideoAnimate:
					{
						MILCMD_DRAW_VIDEO_ANIMATE* ptr19 = (MILCMD_DRAW_VIDEO_ANIMATE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawVideo((MediaPlayer)this.DependentLookup(ptr19->hPlayer), (ptr19->hRectangleAnimations == 0U) ? ptr19->rectangle : ((RectAnimationClockResource)this.DependentLookup(ptr19->hRectangleAnimations)).CurrentValue);
						break;
					}
					case MILCMD.MilPushClip:
					{
						MILCMD_PUSH_CLIP* ptr20 = (MILCMD_PUSH_CLIP*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.PushClip((Geometry)this.DependentLookup(ptr20->hClipGeometry));
						break;
					}
					case MILCMD.MilPushOpacityMask:
					{
						MILCMD_PUSH_OPACITY_MASK* ptr21 = (MILCMD_PUSH_OPACITY_MASK*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.PushOpacityMask((Brush)this.DependentLookup(ptr21->hOpacityMask));
						break;
					}
					case MILCMD.MilPushOpacity:
					{
						MILCMD_PUSH_OPACITY* ptr22 = (MILCMD_PUSH_OPACITY*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.PushOpacity(ptr22->opacity);
						break;
					}
					case MILCMD.MilPushOpacityAnimate:
					{
						MILCMD_PUSH_OPACITY_ANIMATE* ptr23 = (MILCMD_PUSH_OPACITY_ANIMATE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.PushOpacity((ptr23->hOpacityAnimations == 0U) ? ptr23->opacity : ((DoubleAnimationClockResource)this.DependentLookup(ptr23->hOpacityAnimations)).CurrentValue);
						break;
					}
					case MILCMD.MilPushTransform:
					{
						MILCMD_PUSH_TRANSFORM* ptr24 = (MILCMD_PUSH_TRANSFORM*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.PushTransform((Transform)this.DependentLookup(ptr24->hTransform));
						break;
					}
					case MILCMD.MilPushGuidelineSet:
					{
						MILCMD_PUSH_GUIDELINE_SET* ptr25 = (MILCMD_PUSH_GUIDELINE_SET*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.PushGuidelineSet((GuidelineSet)this.DependentLookup(ptr25->hGuidelines));
						break;
					}
					case MILCMD.MilPushGuidelineY1:
					{
						MILCMD_PUSH_GUIDELINE_Y1* ptr26 = (MILCMD_PUSH_GUIDELINE_Y1*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.PushGuidelineY1(ptr26->coordinate);
						break;
					}
					case MILCMD.MilPushGuidelineY2:
					{
						MILCMD_PUSH_GUIDELINE_Y2* ptr27 = (MILCMD_PUSH_GUIDELINE_Y2*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.PushGuidelineY2(ptr27->leadingCoordinate, ptr27->offsetToDrivenCoordinate);
						break;
					}
					case MILCMD.MilPushEffect:
					{
						MILCMD_PUSH_EFFECT* ptr28 = (MILCMD_PUSH_EFFECT*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.PushEffect((BitmapEffect)this.DependentLookup(ptr28->hEffect), (BitmapEffectInput)this.DependentLookup(ptr28->hEffectInput));
						break;
					}
					case MILCMD.MilPop:
					{
						MILCMD_POP* ptr29 = (MILCMD_POP*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.Pop();
						break;
					}
					}
					ptr2 += ptr4->Size;
				}
				array = null;
			}
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x0009CAAC File Offset: 0x0009BEAC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public unsafe void BaseValueDrawingContextWalk(DrawingContextWalker ctx)
		{
			if (this._curOffset > 0)
			{
				byte[] array;
				byte* ptr;
				if ((array = this._buffer) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				byte* ptr2 = ptr;
				byte* ptr3 = ptr + this._curOffset;
				while (ptr2 < ptr3 && !ctx.ShouldStopWalking)
				{
					RenderData.RecordHeader* ptr4 = (RenderData.RecordHeader*)ptr2;
					switch (ptr4->Id)
					{
					case MILCMD.MilDrawLine:
					{
						MILCMD_DRAW_LINE* ptr5 = (MILCMD_DRAW_LINE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawLine((Pen)this.DependentLookup(ptr5->hPen), ptr5->point0, ptr5->point1);
						break;
					}
					case MILCMD.MilDrawLineAnimate:
					{
						MILCMD_DRAW_LINE_ANIMATE* ptr6 = (MILCMD_DRAW_LINE_ANIMATE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawLine((Pen)this.DependentLookup(ptr6->hPen), ptr6->point0, ((PointAnimationClockResource)this.DependentLookup(ptr6->hPoint0Animations)).AnimationClock, ptr6->point1, ((PointAnimationClockResource)this.DependentLookup(ptr6->hPoint1Animations)).AnimationClock);
						break;
					}
					case MILCMD.MilDrawRectangle:
					{
						MILCMD_DRAW_RECTANGLE* ptr7 = (MILCMD_DRAW_RECTANGLE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawRectangle((Brush)this.DependentLookup(ptr7->hBrush), (Pen)this.DependentLookup(ptr7->hPen), ptr7->rectangle);
						break;
					}
					case MILCMD.MilDrawRectangleAnimate:
					{
						MILCMD_DRAW_RECTANGLE_ANIMATE* ptr8 = (MILCMD_DRAW_RECTANGLE_ANIMATE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawRectangle((Brush)this.DependentLookup(ptr8->hBrush), (Pen)this.DependentLookup(ptr8->hPen), ptr8->rectangle, ((RectAnimationClockResource)this.DependentLookup(ptr8->hRectangleAnimations)).AnimationClock);
						break;
					}
					case MILCMD.MilDrawRoundedRectangle:
					{
						MILCMD_DRAW_ROUNDED_RECTANGLE* ptr9 = (MILCMD_DRAW_ROUNDED_RECTANGLE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawRoundedRectangle((Brush)this.DependentLookup(ptr9->hBrush), (Pen)this.DependentLookup(ptr9->hPen), ptr9->rectangle, ptr9->radiusX, ptr9->radiusY);
						break;
					}
					case MILCMD.MilDrawRoundedRectangleAnimate:
					{
						MILCMD_DRAW_ROUNDED_RECTANGLE_ANIMATE* ptr10 = (MILCMD_DRAW_ROUNDED_RECTANGLE_ANIMATE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawRoundedRectangle((Brush)this.DependentLookup(ptr10->hBrush), (Pen)this.DependentLookup(ptr10->hPen), ptr10->rectangle, ((RectAnimationClockResource)this.DependentLookup(ptr10->hRectangleAnimations)).AnimationClock, ptr10->radiusX, ((DoubleAnimationClockResource)this.DependentLookup(ptr10->hRadiusXAnimations)).AnimationClock, ptr10->radiusY, ((DoubleAnimationClockResource)this.DependentLookup(ptr10->hRadiusYAnimations)).AnimationClock);
						break;
					}
					case MILCMD.MilDrawEllipse:
					{
						MILCMD_DRAW_ELLIPSE* ptr11 = (MILCMD_DRAW_ELLIPSE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawEllipse((Brush)this.DependentLookup(ptr11->hBrush), (Pen)this.DependentLookup(ptr11->hPen), ptr11->center, ptr11->radiusX, ptr11->radiusY);
						break;
					}
					case MILCMD.MilDrawEllipseAnimate:
					{
						MILCMD_DRAW_ELLIPSE_ANIMATE* ptr12 = (MILCMD_DRAW_ELLIPSE_ANIMATE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawEllipse((Brush)this.DependentLookup(ptr12->hBrush), (Pen)this.DependentLookup(ptr12->hPen), ptr12->center, ((PointAnimationClockResource)this.DependentLookup(ptr12->hCenterAnimations)).AnimationClock, ptr12->radiusX, ((DoubleAnimationClockResource)this.DependentLookup(ptr12->hRadiusXAnimations)).AnimationClock, ptr12->radiusY, ((DoubleAnimationClockResource)this.DependentLookup(ptr12->hRadiusYAnimations)).AnimationClock);
						break;
					}
					case MILCMD.MilDrawGeometry:
					{
						MILCMD_DRAW_GEOMETRY* ptr13 = (MILCMD_DRAW_GEOMETRY*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawGeometry((Brush)this.DependentLookup(ptr13->hBrush), (Pen)this.DependentLookup(ptr13->hPen), (Geometry)this.DependentLookup(ptr13->hGeometry));
						break;
					}
					case MILCMD.MilDrawImage:
					{
						MILCMD_DRAW_IMAGE* ptr14 = (MILCMD_DRAW_IMAGE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawImage((ImageSource)this.DependentLookup(ptr14->hImageSource), ptr14->rectangle);
						break;
					}
					case MILCMD.MilDrawImageAnimate:
					{
						MILCMD_DRAW_IMAGE_ANIMATE* ptr15 = (MILCMD_DRAW_IMAGE_ANIMATE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawImage((ImageSource)this.DependentLookup(ptr15->hImageSource), ptr15->rectangle, ((RectAnimationClockResource)this.DependentLookup(ptr15->hRectangleAnimations)).AnimationClock);
						break;
					}
					case MILCMD.MilDrawGlyphRun:
					{
						MILCMD_DRAW_GLYPH_RUN* ptr16 = (MILCMD_DRAW_GLYPH_RUN*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawGlyphRun((Brush)this.DependentLookup(ptr16->hForegroundBrush), (GlyphRun)this.DependentLookup(ptr16->hGlyphRun));
						break;
					}
					case MILCMD.MilDrawDrawing:
					{
						MILCMD_DRAW_DRAWING* ptr17 = (MILCMD_DRAW_DRAWING*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawDrawing((Drawing)this.DependentLookup(ptr17->hDrawing));
						break;
					}
					case MILCMD.MilDrawVideo:
					{
						MILCMD_DRAW_VIDEO* ptr18 = (MILCMD_DRAW_VIDEO*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawVideo((MediaPlayer)this.DependentLookup(ptr18->hPlayer), ptr18->rectangle);
						break;
					}
					case MILCMD.MilDrawVideoAnimate:
					{
						MILCMD_DRAW_VIDEO_ANIMATE* ptr19 = (MILCMD_DRAW_VIDEO_ANIMATE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.DrawVideo((MediaPlayer)this.DependentLookup(ptr19->hPlayer), ptr19->rectangle, ((RectAnimationClockResource)this.DependentLookup(ptr19->hRectangleAnimations)).AnimationClock);
						break;
					}
					case MILCMD.MilPushClip:
					{
						MILCMD_PUSH_CLIP* ptr20 = (MILCMD_PUSH_CLIP*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.PushClip((Geometry)this.DependentLookup(ptr20->hClipGeometry));
						break;
					}
					case MILCMD.MilPushOpacityMask:
					{
						MILCMD_PUSH_OPACITY_MASK* ptr21 = (MILCMD_PUSH_OPACITY_MASK*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.PushOpacityMask((Brush)this.DependentLookup(ptr21->hOpacityMask));
						break;
					}
					case MILCMD.MilPushOpacity:
					{
						MILCMD_PUSH_OPACITY* ptr22 = (MILCMD_PUSH_OPACITY*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.PushOpacity(ptr22->opacity);
						break;
					}
					case MILCMD.MilPushOpacityAnimate:
					{
						MILCMD_PUSH_OPACITY_ANIMATE* ptr23 = (MILCMD_PUSH_OPACITY_ANIMATE*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.PushOpacity(ptr23->opacity, ((DoubleAnimationClockResource)this.DependentLookup(ptr23->hOpacityAnimations)).AnimationClock);
						break;
					}
					case MILCMD.MilPushTransform:
					{
						MILCMD_PUSH_TRANSFORM* ptr24 = (MILCMD_PUSH_TRANSFORM*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.PushTransform((Transform)this.DependentLookup(ptr24->hTransform));
						break;
					}
					case MILCMD.MilPushGuidelineSet:
					{
						MILCMD_PUSH_GUIDELINE_SET* ptr25 = (MILCMD_PUSH_GUIDELINE_SET*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.PushGuidelineSet((GuidelineSet)this.DependentLookup(ptr25->hGuidelines));
						break;
					}
					case MILCMD.MilPushGuidelineY1:
					{
						MILCMD_PUSH_GUIDELINE_Y1* ptr26 = (MILCMD_PUSH_GUIDELINE_Y1*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.PushGuidelineY1(ptr26->coordinate);
						break;
					}
					case MILCMD.MilPushGuidelineY2:
					{
						MILCMD_PUSH_GUIDELINE_Y2* ptr27 = (MILCMD_PUSH_GUIDELINE_Y2*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.PushGuidelineY2(ptr27->leadingCoordinate, ptr27->offsetToDrivenCoordinate);
						break;
					}
					case MILCMD.MilPushEffect:
					{
						MILCMD_PUSH_EFFECT* ptr28 = (MILCMD_PUSH_EFFECT*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.PushEffect((BitmapEffect)this.DependentLookup(ptr28->hEffect), (BitmapEffectInput)this.DependentLookup(ptr28->hEffectInput));
						break;
					}
					case MILCMD.MilPop:
					{
						MILCMD_POP* ptr29 = (MILCMD_POP*)(ptr2 + sizeof(RenderData.RecordHeader));
						ctx.Pop();
						break;
					}
					}
					ptr2 += ptr4->Size;
				}
				array = null;
			}
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x0009D16C File Offset: 0x0009C56C
		internal RenderData()
		{
			base.CanBeInheritanceContext = false;
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x0009D188 File Offset: 0x0009C588
		[SecurityCritical]
		public unsafe void WriteDataRecord(MILCMD id, byte* pbRecord, int cbRecordSize)
		{
			int num;
			RenderData.RecordHeader recordHeader;
			checked
			{
				num = cbRecordSize + sizeof(RenderData.RecordHeader);
				int num2 = this._curOffset + num;
				if (this._buffer == null || num2 > this._buffer.Length)
				{
					this.EnsureBuffer(num2);
				}
				recordHeader.Size = num;
				recordHeader.Id = id;
			}
			Marshal.Copy((IntPtr)((void*)(&recordHeader)), this._buffer, this._curOffset, sizeof(RenderData.RecordHeader));
			Marshal.Copy((IntPtr)((void*)pbRecord), this._buffer, this._curOffset + sizeof(RenderData.RecordHeader), cbRecordSize);
			this._curOffset += num;
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x0009D220 File Offset: 0x0009C620
		public Rect GetContentBounds(BoundsDrawingContextWalker ctx)
		{
			this.DrawingContextWalk(ctx);
			return ctx.Bounds;
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x0009D23C File Offset: 0x0009C63C
		public void WalkContent(DrawingContextWalker walker)
		{
			this.DrawingContextWalk(walker);
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x0009D250 File Offset: 0x0009C650
		public bool HitTestPoint(Point point)
		{
			HitTestDrawingContextWalker hitTestDrawingContextWalker = new HitTestWithPointDrawingContextWalker(point);
			this.DrawingContextWalk(hitTestDrawingContextWalker);
			return hitTestDrawingContextWalker.IsHit;
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x0009D274 File Offset: 0x0009C674
		public IntersectionDetail HitTestGeometry(PathGeometry geometry)
		{
			HitTestDrawingContextWalker hitTestDrawingContextWalker = new HitTestWithGeometryDrawingContextWalker(geometry);
			this.DrawingContextWalk(hitTestDrawingContextWalker);
			return hitTestDrawingContextWalker.IntersectionDetail;
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x0009D298 File Offset: 0x0009C698
		protected override Freezable CreateInstanceCore()
		{
			return new RenderData();
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x0009D2AC File Offset: 0x0009C6AC
		protected override void CloneCore(Freezable source)
		{
			Invariant.Assert(false);
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x0009D2C0 File Offset: 0x0009C6C0
		protected override void CloneCurrentValueCore(Freezable source)
		{
			Invariant.Assert(false);
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x0009D2D4 File Offset: 0x0009C6D4
		protected override bool FreezeCore(bool isChecking)
		{
			return false;
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x0009D2E4 File Offset: 0x0009C6E4
		protected override void GetAsFrozenCore(Freezable source)
		{
			Invariant.Assert(false);
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x0009D2F8 File Offset: 0x0009C6F8
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			Invariant.Assert(false);
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x0009D30C File Offset: 0x0009C70C
		public void PropagateChangedHandler(EventHandler handler, bool adding)
		{
			if (adding)
			{
				base.Changed += handler;
			}
			else
			{
				base.Changed -= handler;
			}
			int i = 0;
			int count = this._dependentResources.Count;
			while (i < count)
			{
				Freezable freezable = this._dependentResources[i] as Freezable;
				if (freezable != null)
				{
					if (adding)
					{
						base.OnFreezablePropertyChanged(null, freezable);
					}
					else
					{
						base.OnFreezablePropertyChanged(freezable, null);
					}
				}
				else
				{
					AnimationClockResource animationClockResource = this._dependentResources[i] as AnimationClockResource;
					if (animationClockResource != null)
					{
						animationClockResource.PropagateChangedHandlersCore(handler, adding);
					}
				}
				i++;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06002700 RID: 9984 RVA: 0x0009D390 File Offset: 0x0009C790
		// (set) Token: 0x06002701 RID: 9985 RVA: 0x0009D3A4 File Offset: 0x0009C7A4
		internal int BitmapEffectStackDepth
		{
			get
			{
				return this._bitmapEffectStackDepth;
			}
			set
			{
				this._bitmapEffectStackDepth = value;
			}
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x0009D3B8 File Offset: 0x0009C7B8
		internal void BeginTopLevelBitmapEffect(int stackDepth)
		{
			this.BitmapEffectStackDepth = stackDepth;
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x0009D3CC File Offset: 0x0009C7CC
		internal void EndTopLevelBitmapEffect()
		{
			this.BitmapEffectStackDepth = 0;
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06002704 RID: 9988 RVA: 0x0009D3E0 File Offset: 0x0009C7E0
		public int DataSize
		{
			get
			{
				return this._curOffset;
			}
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x0009D3F4 File Offset: 0x0009C7F4
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handle;
			using (CompositionEngineLock.Acquire())
			{
				if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_RENDERDATA))
				{
					for (int i = 0; i < this._dependentResources.Count; i++)
					{
						DUCE.IResource resource = this._dependentResources[i] as DUCE.IResource;
						if (resource != null)
						{
							resource.AddRefOnChannel(channel);
						}
					}
					this.UpdateResource(channel);
				}
				handle = this._duceResource.GetHandle(channel);
			}
			return handle;
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x0009D48C File Offset: 0x0009C88C
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			using (CompositionEngineLock.Acquire())
			{
				if (this._duceResource.ReleaseOnChannel(channel))
				{
					for (int i = 0; i < this._dependentResources.Count; i++)
					{
						DUCE.IResource resource = this._dependentResources[i] as DUCE.IResource;
						if (resource != null)
						{
							resource.ReleaseOnChannel(channel);
						}
					}
				}
			}
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x0009D50C File Offset: 0x0009C90C
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handle;
			using (CompositionEngineLock.Acquire())
			{
				handle = this._duceResource.GetHandle(channel);
			}
			return handle;
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x0009D55C File Offset: 0x0009C95C
		int DUCE.IResource.GetChannelCount()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x0009D574 File Offset: 0x0009C974
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x0009D590 File Offset: 0x0009C990
		void DUCE.IResource.RemoveChildFromParent(DUCE.IResource parent, DUCE.Channel channel)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x0009D5A4 File Offset: 0x0009C9A4
		DUCE.ResourceHandle DUCE.IResource.Get3DHandle(DUCE.Channel channel)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x0009D5B8 File Offset: 0x0009C9B8
		public uint AddDependentResource(object o)
		{
			if (o == null)
			{
				return 0U;
			}
			return (uint)(this._dependentResources.Add(o) + 1);
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x0009D5D8 File Offset: 0x0009C9D8
		private void UpdateResource(DUCE.Channel channel)
		{
			this.MarshalToDUCE(channel);
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x0009D5EC File Offset: 0x0009C9EC
		private void EnsureBuffer(int cbRequiredSize)
		{
			if (this._buffer == null)
			{
				this._buffer = new byte[cbRequiredSize];
				return;
			}
			int num = Math.Max((this._buffer.Length << 1) - (this._buffer.Length >> 1), cbRequiredSize);
			byte[] array = new byte[num];
			this._buffer.CopyTo(array, 0);
			this._buffer = array;
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x0009D648 File Offset: 0x0009CA48
		private object DependentLookup(uint index)
		{
			if (index == 0U)
			{
				return null;
			}
			return this._dependentResources[(int)(index - 1U)];
		}

		// Token: 0x0400123B RID: 4667
		private byte[] _buffer;

		// Token: 0x0400123C RID: 4668
		private int _curOffset;

		// Token: 0x0400123D RID: 4669
		private int _bitmapEffectStackDepth;

		// Token: 0x0400123E RID: 4670
		private FrugalStructList<object> _dependentResources;

		// Token: 0x0400123F RID: 4671
		private DUCE.MultiChannelResource _duceResource;

		// Token: 0x0200087C RID: 2172
		internal struct RecordHeader
		{
			// Token: 0x040028A6 RID: 10406
			public int Size;

			// Token: 0x040028A7 RID: 10407
			public MILCMD Id;
		}

		// Token: 0x0200087D RID: 2173
		private enum PushType
		{
			// Token: 0x040028A9 RID: 10409
			BitmapEffect,
			// Token: 0x040028AA RID: 10410
			Other
		}
	}
}
