using System;
using System.Windows.Media.Composition;

namespace System.Windows.Media
{
	/// <summary>
	///   <see cref="T:System.Windows.Media.DrawingVisual" /> é um objeto visual que pode ser usado para renderizar gráficos vetoriais na tela. O conteúdo é mantido pelo sistema.</summary>
	// Token: 0x02000389 RID: 905
	public class DrawingVisual : ContainerVisual
	{
		/// <summary>Determina se um valor de coordenadas de ponto está dentro dos limites do objeto <see cref="T:System.Windows.Media.DrawingVisual" />.</summary>
		/// <param name="hitTestParameters">Um valor do tipo <see cref="T:System.Windows.Media.PointHitTestParameters" /> que especifica o <see cref="T:System.Windows.Point" /> em relação ao qual realizar o teste de clique.</param>
		/// <returns>Um valor do tipo <see cref="T:System.Windows.Media.HitTestResult" />, representando o <see cref="T:System.Windows.Media.Visual" /> retornado de um teste de clique.</returns>
		// Token: 0x06002186 RID: 8582 RVA: 0x00087970 File Offset: 0x00086D70
		protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
		{
			if (hitTestParameters == null)
			{
				throw new ArgumentNullException("hitTestParameters");
			}
			if (this._content != null && this._content.HitTestPoint(hitTestParameters.HitPoint))
			{
				return new PointHitTestResult(this, hitTestParameters.HitPoint);
			}
			return null;
		}

		/// <summary>Determina se um valor de geometria está dentro dos limites do objeto visual.</summary>
		/// <param name="hitTestParameters">Um valor do tipo <see cref="T:System.Windows.Media.GeometryHitTestParameters" /> que especifica o <see cref="T:System.Windows.Media.Geometry" /> em relação ao qual realizar o teste de clique.</param>
		/// <returns>Um valor do tipo <see cref="T:System.Windows.Media.GeometryHitTestResult" />.</returns>
		// Token: 0x06002187 RID: 8583 RVA: 0x000879B4 File Offset: 0x00086DB4
		protected override GeometryHitTestResult HitTestCore(GeometryHitTestParameters hitTestParameters)
		{
			if (hitTestParameters == null)
			{
				throw new ArgumentNullException("hitTestParameters");
			}
			if (this._content != null && this.GetHitTestBounds().IntersectsWith(hitTestParameters.Bounds))
			{
				IntersectionDetail intersectionDetail = this._content.HitTestGeometry(hitTestParameters.InternalHitGeometry);
				if (intersectionDetail != IntersectionDetail.Empty)
				{
					return new GeometryHitTestResult(this, intersectionDetail);
				}
			}
			return null;
		}

		/// <summary>Abre o objeto <see cref="T:System.Windows.Media.DrawingVisual" /> para renderização. O valor <see cref="T:System.Windows.Media.DrawingContext" /> retornado pode ser usado para renderizar no <see cref="T:System.Windows.Media.DrawingVisual" />.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Windows.Media.DrawingContext" />.</returns>
		// Token: 0x06002188 RID: 8584 RVA: 0x00087A0C File Offset: 0x00086E0C
		public DrawingContext RenderOpen()
		{
			base.VerifyAPIReadWrite();
			return new VisualDrawingContext(this);
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x00087A28 File Offset: 0x00086E28
		internal override void RenderClose(IDrawingContent newContent)
		{
			IDrawingContent content = this._content;
			this._content = null;
			if (content != null)
			{
				content.PropagateChangedHandler(base.ContentsChangedHandler, false);
				base.DisconnectAttachedResource(VisualProxyFlags.IsContentConnected, content);
			}
			if (newContent != null)
			{
				newContent.PropagateChangedHandler(base.ContentsChangedHandler, true);
			}
			this._content = newContent;
			base.SetFlagsOnAllChannels(true, VisualProxyFlags.IsContentDirty);
			Visual.PropagateFlags(this, VisualFlags.IsSubtreeDirtyForPrecompute, VisualProxyFlags.IsSubtreeDirtyForRender);
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x00087A88 File Offset: 0x00086E88
		internal override void FreeContent(DUCE.Channel channel)
		{
			if (this._content != null && base.CheckFlagsAnd(channel, VisualProxyFlags.IsContentConnected))
			{
				DUCE.CompositionNode.SetContent(this._proxy.GetHandle(channel), DUCE.ResourceHandle.Null, channel);
				base.SetFlags(channel, false, VisualProxyFlags.IsContentConnected);
				this._content.ReleaseOnChannel(channel);
			}
			base.FreeContent(channel);
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x00087AE4 File Offset: 0x00086EE4
		internal override Rect GetContentBounds()
		{
			if (this._content != null)
			{
				Rect result = Rect.Empty;
				MediaContext mediaContext = MediaContext.From(base.Dispatcher);
				BoundsDrawingContextWalker ctx = mediaContext.AcquireBoundsDrawingContextWalker();
				result = this._content.GetContentBounds(ctx);
				mediaContext.ReleaseBoundsDrawingContextWalker(ctx);
				return result;
			}
			return Rect.Empty;
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x00087B30 File Offset: 0x00086F30
		internal void WalkContent(DrawingContextWalker walker)
		{
			base.VerifyAPIReadOnly();
			if (this._content != null)
			{
				this._content.WalkContent(walker);
			}
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x00087B58 File Offset: 0x00086F58
		internal override void RenderContent(RenderContext ctx, bool isOnChannel)
		{
			DUCE.Channel channel = ctx.Channel;
			if (this._content != null)
			{
				DUCE.CompositionNode.SetContent(this._proxy.GetHandle(channel), this._content.AddRefOnChannel(channel), channel);
				base.SetFlags(channel, true, VisualProxyFlags.IsContentConnected);
				return;
			}
			if (isOnChannel)
			{
				DUCE.CompositionNode.SetContent(this._proxy.GetHandle(channel), DUCE.ResourceHandle.Null, channel);
			}
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x00087BBC File Offset: 0x00086FBC
		internal override DrawingGroup GetDrawing()
		{
			base.VerifyAPIReadOnly();
			DrawingGroup result = null;
			if (this._content != null)
			{
				result = DrawingServices.DrawingGroupFromRenderData((RenderData)this._content);
			}
			return result;
		}

		/// <summary>Obtém o conteúdo do desenho do objeto <see cref="T:System.Windows.Media.DrawingVisual" />.</summary>
		/// <returns>Obtém um valor do tipo <see cref="T:System.Windows.Media.DrawingGroup" /> que representa a coleção de <see cref="T:System.Windows.Media.Drawing" /> objetos no <see cref="T:System.Windows.Media.DrawingVisual" />.</returns>
		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x0600218F RID: 8591 RVA: 0x00087BEC File Offset: 0x00086FEC
		public DrawingGroup Drawing
		{
			get
			{
				return this.GetDrawing();
			}
		}

		// Token: 0x040010BC RID: 4284
		private IDrawingContent _content;
	}
}
