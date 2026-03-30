using System;
using System.Security;
using System.Security.Permissions;
using System.Windows.Media.Composition;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media
{
	/// <summary>Representa a superfície de exibição do seu aplicativo.</summary>
	// Token: 0x02000377 RID: 887
	[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
	public abstract class CompositionTarget : DispatcherObject, IDisposable, ICompositionTarget
	{
		// Token: 0x06001FC0 RID: 8128 RVA: 0x00082114 File Offset: 0x00081514
		internal CompositionTarget()
		{
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x00082164 File Offset: 0x00081564
		internal virtual void CreateUCEResources(DUCE.Channel channel, DUCE.Channel outOfBandChannel)
		{
			bool flag = this._contentRoot.CreateOrAddRefOnChannel(this, outOfBandChannel, DUCE.ResourceType.TYPE_VISUAL);
			this._contentRoot.DuplicateHandle(outOfBandChannel, channel);
			outOfBandChannel.CloseBatch();
			outOfBandChannel.Commit();
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0008219C File Offset: 0x0008159C
		internal virtual void ReleaseUCEResources(DUCE.Channel channel, DUCE.Channel outOfBandChannel)
		{
			if (this._rootVisual.Value != null)
			{
				((DUCE.IResource)this._rootVisual.Value).ReleaseOnChannel(channel);
			}
			if (this._contentRoot.IsOnChannel(channel))
			{
				this._contentRoot.ReleaseOnChannel(channel);
			}
			if (this._contentRoot.IsOnChannel(outOfBandChannel))
			{
				this._contentRoot.ReleaseOnChannel(outOfBandChannel);
			}
		}

		/// <summary>Descarta <see cref="T:System.Windows.Media.CompositionTarget" />.</summary>
		// Token: 0x06001FC3 RID: 8131 RVA: 0x00082200 File Offset: 0x00081600
		public virtual void Dispose()
		{
			base.VerifyAccess();
			if (!this._isDisposed)
			{
				this._isDisposed = true;
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x00082228 File Offset: 0x00081628
		internal bool IsDisposed
		{
			get
			{
				return this._isDisposed;
			}
		}

		/// <summary>Obtém ou define o visual raiz do <see cref="T:System.Windows.Media.CompositionTarget" />.</summary>
		/// <returns>O visual raiz do <see cref="T:System.Windows.Media.CompositionTarget" />.</returns>
		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x0008223C File Offset: 0x0008163C
		// (set) Token: 0x06001FC6 RID: 8134 RVA: 0x0008225C File Offset: 0x0008165C
		public virtual Visual RootVisual
		{
			[SecurityCritical]
			get
			{
				this.VerifyAPIReadOnly();
				return this._rootVisual.Value;
			}
			[SecurityCritical]
			[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
			set
			{
				this.VerifyAPIReadWrite();
				if (this._rootVisual.Value != value)
				{
					this.SetRootVisual(value);
					MediaContext.From(base.Dispatcher).PostRender();
				}
			}
		}

		/// <summary>Obtém uma matriz que pode ser usada para transformar as coordenadas desse destino para o dispositivo de destino de renderização.</summary>
		/// <returns>A matriz de transformação.</returns>
		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001FC7 RID: 8135
		public abstract Matrix TransformToDevice { get; }

		/// <summary>Obtém uma matriz que pode ser usada para transformar as coordenadas do dispositivo de destino da renderização para este destino.</summary>
		/// <returns>A matriz de transformação.</returns>
		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001FC8 RID: 8136
		public abstract Matrix TransformFromDevice { get; }

		// Token: 0x06001FC9 RID: 8137 RVA: 0x00082294 File Offset: 0x00081694
		internal object StateChangedCallback(object arg)
		{
			object[] array = arg as object[];
			CompositionTarget.HostStateFlags hostStateFlags = (CompositionTarget.HostStateFlags)array[0];
			if ((hostStateFlags & CompositionTarget.HostStateFlags.WorldTransform) != CompositionTarget.HostStateFlags.None)
			{
				this._worldTransform = (Matrix)array[1];
			}
			if ((hostStateFlags & CompositionTarget.HostStateFlags.ClipBounds) != CompositionTarget.HostStateFlags.None)
			{
				this._worldClipBounds = (Rect)array[2];
			}
			if (this._rootVisual.Value != null)
			{
				Visual.PropagateFlags(this._rootVisual.Value, VisualFlags.IsSubtreeDirtyForPrecompute, VisualProxyFlags.IsSubtreeDirtyForRender);
			}
			return null;
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x000822F8 File Offset: 0x000816F8
		void ICompositionTarget.AddRefOnChannel(DUCE.Channel channel, DUCE.Channel outOfBandChannel)
		{
			this.CreateUCEResources(channel, outOfBandChannel);
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x00082310 File Offset: 0x00081710
		void ICompositionTarget.ReleaseOnChannel(DUCE.Channel channel, DUCE.Channel outOfBandChannel)
		{
			this.ReleaseUCEResources(channel, outOfBandChannel);
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x00082328 File Offset: 0x00081728
		void ICompositionTarget.Render(bool inResize, DUCE.Channel channel)
		{
			if (this._rootVisual.Value != null)
			{
				bool flag = false;
				if (EventTrace.IsEnabled(EventTrace.Keyword.KeywordGeneral | EventTrace.Keyword.KeywordPerf, EventTrace.Level.Info))
				{
					flag = true;
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientPrecomputeSceneBegin, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Level.Info, PerfService.GetPerfElementID(this));
				}
				this._rootVisual.Value.Precompute();
				if (flag)
				{
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientPrecomputeSceneEnd, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Level.Info);
				}
				if (flag)
				{
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientCompileSceneBegin, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Level.Info, PerfService.GetPerfElementID(this));
				}
				this.Compile(channel);
				if (flag)
				{
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientCompileSceneEnd, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Level.Info);
				}
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001FCD RID: 8141 RVA: 0x000823E0 File Offset: 0x000817E0
		internal Matrix WorldTransform
		{
			get
			{
				return this._worldTransform;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x000823F4 File Offset: 0x000817F4
		internal Rect WorldClipBounds
		{
			get
			{
				return this._worldClipBounds;
			}
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x00082408 File Offset: 0x00081808
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void Compile(DUCE.Channel channel)
		{
			MediaContext mediaContext = MediaContext.From(base.Dispatcher);
			Invariant.Assert(this._rootVisual.Value != null);
			Invariant.Assert(channel != null);
			RenderContext renderContext;
			if (this._cachedRenderContext != null)
			{
				renderContext = this._cachedRenderContext;
				this._cachedRenderContext = null;
			}
			else
			{
				renderContext = new RenderContext();
			}
			renderContext.Initialize(channel, this._contentRoot.GetHandle(channel));
			if (mediaContext.IsConnected)
			{
				this._rootVisual.Value.Render(renderContext, 0U);
			}
			this._cachedRenderContext = renderContext;
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x00082494 File Offset: 0x00081894
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void SetRootVisual(Visual visual)
		{
			if (visual != null && (visual._parent != null || visual.IsRootElement))
			{
				throw new ArgumentException(SR.Get("CompositionTarget_RootVisual_HasParent"));
			}
			DUCE.ChannelSet channels = MediaContext.From(base.Dispatcher).GetChannels();
			DUCE.Channel channel = channels.Channel;
			if (this._rootVisual.Value != null && this._contentRoot.IsOnChannel(channel))
			{
				this.ClearRootNode(channel);
				((DUCE.IResource)this._rootVisual.Value).ReleaseOnChannel(channel);
				this._rootVisual.Value.IsRootElement = false;
			}
			this._rootVisual.Value = visual;
			if (this._rootVisual.Value != null)
			{
				this._rootVisual.Value.IsRootElement = true;
				this._rootVisual.Value.SetFlagsOnAllChannels(true, VisualProxyFlags.IsSubtreeDirtyForRender);
			}
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x0008255C File Offset: 0x0008195C
		private void ClearRootNode(DUCE.Channel channel)
		{
			DUCE.CompositionNode.RemoveAllChildren(this._contentRoot.GetHandle(channel), channel);
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x0008257C File Offset: 0x0008197C
		internal void VerifyAPIReadOnly()
		{
			base.VerifyAccess();
			if (this._isDisposed)
			{
				throw new ObjectDisposedException("CompositionTarget");
			}
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x000825A4 File Offset: 0x000819A4
		internal void VerifyAPIReadWrite()
		{
			base.VerifyAccess();
			if (this._isDisposed)
			{
				throw new ObjectDisposedException("CompositionTarget");
			}
			MediaContext.From(base.Dispatcher).VerifyWriteAccess();
		}

		/// <summary>Ocorre antes de os objetos na árvore de composição serem renderizados.</summary>
		// Token: 0x14000193 RID: 403
		// (add) Token: 0x06001FD4 RID: 8148 RVA: 0x000825DC File Offset: 0x000819DC
		// (remove) Token: 0x06001FD5 RID: 8149 RVA: 0x00082604 File Offset: 0x00081A04
		public static event EventHandler Rendering
		{
			add
			{
				MediaContext mediaContext = MediaContext.From(Dispatcher.CurrentDispatcher);
				mediaContext.Rendering += value;
				mediaContext.PostRender();
			}
			remove
			{
				MediaContext mediaContext = MediaContext.From(Dispatcher.CurrentDispatcher);
				mediaContext.Rendering -= value;
			}
		}

		// Token: 0x04001077 RID: 4215
		internal DUCE.MultiChannelResource _contentRoot;

		// Token: 0x04001078 RID: 4216
		internal const DUCE.ResourceType s_contentRootType = DUCE.ResourceType.TYPE_VISUAL;

		// Token: 0x04001079 RID: 4217
		private bool _isDisposed;

		// Token: 0x0400107A RID: 4218
		private SecurityCriticalDataForSet<Visual> _rootVisual;

		// Token: 0x0400107B RID: 4219
		private RenderContext _cachedRenderContext;

		// Token: 0x0400107C RID: 4220
		private Matrix _worldTransform = Matrix.Identity;

		// Token: 0x0400107D RID: 4221
		private Rect _worldClipBounds = new Rect(-8.9884656743115785E+307, -8.9884656743115785E+307, double.MaxValue, double.MaxValue);

		// Token: 0x0200085F RID: 2143
		internal enum HostStateFlags : uint
		{
			// Token: 0x04002850 RID: 10320
			None,
			// Token: 0x04002851 RID: 10321
			WorldTransform,
			// Token: 0x04002852 RID: 10322
			ClipBounds
		}
	}
}
