using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Diagnostics;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Composition;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;
using MS.Internal;
using MS.Internal.Media;
using MS.Internal.Media3D;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Fornece suporte à renderização no WPF, que inclui teste de clique, transformação de coordenadas e cálculos da caixa delimitadora.</summary>
	// Token: 0x0200044A RID: 1098
	public abstract class Visual : DependencyObject, DUCE.IResource
	{
		// Token: 0x06002CBC RID: 11452 RVA: 0x000B28A4 File Offset: 0x000B1CA4
		internal Visual(DUCE.ResourceType resourceType)
		{
			if (resourceType != DUCE.ResourceType.TYPE_VISUAL && resourceType == DUCE.ResourceType.TYPE_VIEWPORT3DVISUAL)
			{
				this.SetFlags(true, VisualFlags.IsViewport3DVisual);
			}
		}

		/// <summary>Fornece a inicialização de base para objetos derivados da classe <see cref="T:System.Windows.Media.Visual" />.</summary>
		// Token: 0x06002CBD RID: 11453 RVA: 0x000B28D8 File Offset: 0x000B1CD8
		protected Visual() : this(DUCE.ResourceType.TYPE_VISUAL)
		{
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x000B28F0 File Offset: 0x000B1CF0
		internal bool IsOnChannel(DUCE.Channel channel)
		{
			return this._proxy.IsOnChannel(channel);
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x000B290C File Offset: 0x000B1D0C
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			return this._proxy.GetHandle(channel);
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x000B2928 File Offset: 0x000B1D28
		DUCE.ResourceHandle DUCE.IResource.Get3DHandle(DUCE.Channel channel)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x000B293C File Offset: 0x000B1D3C
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			return this.AddRefOnChannelCore(channel);
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x000B2950 File Offset: 0x000B1D50
		internal virtual DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			DUCE.ResourceType resourceType = DUCE.ResourceType.TYPE_VISUAL;
			if (this.CheckFlagsAnd(VisualFlags.IsViewport3DVisual))
			{
				resourceType = DUCE.ResourceType.TYPE_VIEWPORT3DVISUAL;
			}
			this._proxy.CreateOrAddRefOnChannel(this, channel, resourceType);
			return this._proxy.GetHandle(channel);
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x000B298C File Offset: 0x000B1D8C
		internal virtual void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			this._proxy.ReleaseOnChannel(channel);
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x000B29A8 File Offset: 0x000B1DA8
		void DUCE.IResource.RemoveChildFromParent(DUCE.IResource parent, DUCE.Channel channel)
		{
			DUCE.CompositionNode.RemoveChild(parent.GetHandle(channel), this._proxy.GetHandle(channel), channel);
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x000B29D0 File Offset: 0x000B1DD0
		int DUCE.IResource.GetChannelCount()
		{
			return this._proxy.Count;
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x000B29E8 File Offset: 0x000B1DE8
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this._proxy.GetChannel(index);
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06002CC7 RID: 11463 RVA: 0x000B2A04 File Offset: 0x000B1E04
		// (set) Token: 0x06002CC8 RID: 11464 RVA: 0x000B2A1C File Offset: 0x000B1E1C
		internal bool IsVisualChildrenIterationInProgress
		{
			[FriendAccessAllowed]
			get
			{
				return this.CheckFlagsAnd(VisualFlags.IsVisualChildrenIterationInProgress);
			}
			[FriendAccessAllowed]
			set
			{
				this.SetFlags(value, VisualFlags.IsVisualChildrenIterationInProgress);
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06002CC9 RID: 11465 RVA: 0x000B2A34 File Offset: 0x000B1E34
		// (set) Token: 0x06002CCA RID: 11466 RVA: 0x000B2A48 File Offset: 0x000B1E48
		internal bool IsRootElement
		{
			get
			{
				return this.CheckFlagsAnd(VisualFlags.ShouldPostRender);
			}
			set
			{
				this.SetFlags(value, VisualFlags.ShouldPostRender);
			}
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x000B2A60 File Offset: 0x000B1E60
		internal virtual Rect GetContentBounds()
		{
			return Rect.Empty;
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x000B2A74 File Offset: 0x000B1E74
		internal virtual void RenderContent(RenderContext ctx, bool isOnChannel)
		{
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x000B2A84 File Offset: 0x000B1E84
		internal virtual void RenderClose(IDrawingContent newContent)
		{
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002CCE RID: 11470 RVA: 0x000B2A94 File Offset: 0x000B1E94
		internal Rect VisualContentBounds
		{
			get
			{
				this.VerifyAPIReadWrite();
				return this.GetContentBounds();
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002CCF RID: 11471 RVA: 0x000B2AB0 File Offset: 0x000B1EB0
		internal Rect VisualDescendantBounds
		{
			get
			{
				this.VerifyAPIReadWrite();
				Rect rect = this.CalculateSubgraphBoundsInnerSpace();
				if (DoubleUtil.RectHasNaN(rect))
				{
					rect.X = double.NegativeInfinity;
					rect.Y = double.NegativeInfinity;
					rect.Width = double.PositiveInfinity;
					rect.Height = double.PositiveInfinity;
				}
				return rect;
			}
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x000B2B14 File Offset: 0x000B1F14
		internal Rect CalculateSubgraphBoundsInnerSpace()
		{
			return this.CalculateSubgraphBoundsInnerSpace(false);
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x000B2B28 File Offset: 0x000B1F28
		internal Rect CalculateSubgraphRenderBoundsInnerSpace()
		{
			return this.CalculateSubgraphBoundsInnerSpace(true);
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x000B2B3C File Offset: 0x000B1F3C
		internal virtual Rect CalculateSubgraphBoundsInnerSpace(bool renderBounds)
		{
			Rect empty = Rect.Empty;
			int visualChildrenCount = this.VisualChildrenCount;
			for (int i = 0; i < visualChildrenCount; i++)
			{
				Visual visualChild = this.GetVisualChild(i);
				if (visualChild != null)
				{
					Rect rect = visualChild.CalculateSubgraphBoundsOuterSpace(renderBounds);
					empty.Union(rect);
				}
			}
			Rect rect2 = this.GetContentBounds();
			if (renderBounds && this.IsEmptyRenderBounds(ref rect2))
			{
				rect2 = Rect.Empty;
			}
			empty.Union(rect2);
			return empty;
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x000B2BA4 File Offset: 0x000B1FA4
		internal Rect CalculateSubgraphBoundsOuterSpace()
		{
			return this.CalculateSubgraphBoundsOuterSpace(false);
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x000B2BB8 File Offset: 0x000B1FB8
		internal Rect CalculateSubgraphRenderBoundsOuterSpace()
		{
			return this.CalculateSubgraphBoundsOuterSpace(true);
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x000B2BCC File Offset: 0x000B1FCC
		private Rect CalculateSubgraphBoundsOuterSpace(bool renderBounds)
		{
			Rect rect = Rect.Empty;
			rect = this.CalculateSubgraphBoundsInnerSpace(renderBounds);
			if (this.CheckFlagsAnd(VisualFlags.NodeHasEffect))
			{
				Effect value = Visual.EffectField.GetValue(this);
				if (value != null)
				{
					Rect rect2 = new Rect(0.0, 0.0, 1.0, 1.0);
					Rect unitRect = value.EffectMapping.TransformBounds(rect2);
					Rect rect3 = Effect.UnitToWorld(unitRect, rect);
					rect.Union(rect3);
				}
			}
			Geometry value2 = Visual.ClipField.GetValue(this);
			if (value2 != null)
			{
				rect.Intersect(value2.Bounds);
			}
			Transform value3 = Visual.TransformField.GetValue(this);
			if (value3 != null && !value3.IsIdentity)
			{
				Matrix value4 = value3.Value;
				MatrixUtil.TransformRect(ref rect, ref value4);
			}
			if (!rect.IsEmpty)
			{
				rect.X += this._offset.X;
				rect.Y += this._offset.Y;
			}
			Rect? value5 = Visual.ScrollableAreaClipField.GetValue(this);
			if (value5 != null)
			{
				rect.Intersect(value5.Value);
			}
			if (DoubleUtil.RectHasNaN(rect))
			{
				rect.X = double.NegativeInfinity;
				rect.Y = double.NegativeInfinity;
				rect.Width = double.PositiveInfinity;
				rect.Height = double.PositiveInfinity;
			}
			return rect;
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x000B2D3C File Offset: 0x000B213C
		private bool IsEmptyRenderBounds(ref Rect bounds)
		{
			return bounds.Width <= 0.0 || bounds.Height <= 0.0;
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x000B2D70 File Offset: 0x000B2170
		internal virtual void FreeContent(DUCE.Channel channel)
		{
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x000B2D80 File Offset: 0x000B2180
		private bool IsCyclicBrushRootOnChannel(DUCE.Channel channel)
		{
			bool result = false;
			Dictionary<DUCE.Channel, int> value = Visual.ChannelsToCyclicBrushMapField.GetValue(this);
			int num;
			if (value != null && value.TryGetValue(channel, out num))
			{
				result = (num > 0);
			}
			return result;
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x000B2DB0 File Offset: 0x000B21B0
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			if (!this.IsOnChannel(channel) || this.CheckFlagsAnd(channel, VisualProxyFlags.IsDeleteResourceInProgress))
			{
				return;
			}
			this.SetFlags(channel, true, VisualProxyFlags.IsDeleteResourceInProgress);
			try
			{
				this.SetFlags(channel, false, VisualProxyFlags.IsConnectedToParent);
				if (!this.CheckFlagsOr(VisualFlags.NodeIsCyclicBrushRoot) || !channel.IsConnected || channel.IsSynchronous || !this.IsCyclicBrushRootOnChannel(channel))
				{
					this.FreeContent(channel);
					Transform value = Visual.TransformField.GetValue(this);
					if (value != null && !this.CheckFlagsAnd(channel, VisualProxyFlags.IsTransformDirty))
					{
						((DUCE.IResource)value).ReleaseOnChannel(channel);
					}
					Effect value2 = Visual.EffectField.GetValue(this);
					if (value2 != null && !this.CheckFlagsAnd(channel, VisualProxyFlags.IsEffectDirty))
					{
						((DUCE.IResource)value2).ReleaseOnChannel(channel);
					}
					Geometry value3 = Visual.ClipField.GetValue(this);
					if (value3 != null && !this.CheckFlagsAnd(channel, VisualProxyFlags.IsClipDirty))
					{
						((DUCE.IResource)value3).ReleaseOnChannel(channel);
					}
					Brush value4 = Visual.OpacityMaskField.GetValue(this);
					if (value4 != null && !this.CheckFlagsAnd(channel, VisualProxyFlags.IsOpacityMaskDirty))
					{
						((DUCE.IResource)value4).ReleaseOnChannel(channel);
					}
					CacheMode value5 = Visual.CacheModeField.GetValue(this);
					if (value5 != null && !this.CheckFlagsAnd(channel, VisualProxyFlags.IsCacheModeDirty))
					{
						((DUCE.IResource)value5).ReleaseOnChannel(channel);
					}
					this.ReleaseOnChannelCore(channel);
					int visualChildrenCount = this.VisualChildrenCount;
					for (int i = 0; i < visualChildrenCount; i++)
					{
						Visual visualChild = this.GetVisualChild(i);
						if (visualChild != null)
						{
							((DUCE.IResource)visualChild).ReleaseOnChannel(channel);
						}
					}
				}
			}
			finally
			{
				if (this.IsOnChannel(channel))
				{
					this.SetFlags(channel, false, VisualProxyFlags.IsDeleteResourceInProgress);
				}
			}
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x000B2F34 File Offset: 0x000B2334
		internal virtual void AddRefOnChannelForCyclicBrush(ICyclicBrush cyclicBrush, DUCE.Channel channel)
		{
			Dictionary<DUCE.Channel, int> dictionary = Visual.ChannelsToCyclicBrushMapField.GetValue(this);
			if (dictionary == null)
			{
				dictionary = new Dictionary<DUCE.Channel, int>();
				Visual.ChannelsToCyclicBrushMapField.SetValue(this, dictionary);
			}
			if (!dictionary.ContainsKey(channel))
			{
				this.SetFlags(true, VisualFlags.NodeIsCyclicBrushRoot);
				dictionary[channel] = 1;
			}
			else
			{
				Dictionary<DUCE.Channel, int> dictionary2 = dictionary;
				dictionary2[channel]++;
			}
			Dictionary<ICyclicBrush, int> dictionary3 = Visual.CyclicBrushToChannelsMapField.GetValue(this);
			if (dictionary3 == null)
			{
				dictionary3 = new Dictionary<ICyclicBrush, int>();
				Visual.CyclicBrushToChannelsMapField.SetValue(this, dictionary3);
			}
			if (!dictionary3.ContainsKey(cyclicBrush))
			{
				dictionary3[cyclicBrush] = 1;
			}
			else
			{
				Dictionary<ICyclicBrush, int> dictionary4 = dictionary3;
				dictionary4[cyclicBrush]++;
			}
			cyclicBrush.RenderForCyclicBrush(channel, false);
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x000B2FEC File Offset: 0x000B23EC
		internal virtual void ReleaseOnChannelForCyclicBrush(ICyclicBrush cyclicBrush, DUCE.Channel channel)
		{
			Dictionary<ICyclicBrush, int> value = Visual.CyclicBrushToChannelsMapField.GetValue(this);
			if (value[cyclicBrush] == 1)
			{
				value.Remove(cyclicBrush);
			}
			else
			{
				value[cyclicBrush]--;
			}
			Dictionary<DUCE.Channel, int> value2 = Visual.ChannelsToCyclicBrushMapField.GetValue(this);
			value2[channel]--;
			if (value2[channel] == 0)
			{
				value2.Remove(channel);
				this.SetFlags(false, VisualFlags.NodeIsCyclicBrushRoot);
				Visual.PropagateFlags(this, VisualFlags.None, VisualProxyFlags.IsSubtreeDirtyForRender);
				if ((this._parent == null || !this.CheckFlagsAnd(channel, VisualProxyFlags.IsConnectedToParent)) && !this.IsRootElement)
				{
					((DUCE.IResource)this).ReleaseOnChannel(channel);
				}
			}
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x000B3090 File Offset: 0x000B2490
		internal void VerifyAPIReadOnly()
		{
			base.VerifyAccess();
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x000B30A4 File Offset: 0x000B24A4
		internal void VerifyAPIReadOnly(DependencyObject value)
		{
			this.VerifyAPIReadOnly();
			MediaSystem.AssertSameContext(this, value);
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x000B30C0 File Offset: 0x000B24C0
		internal void VerifyAPIReadWrite()
		{
			this.VerifyAPIReadOnly();
			MediaContext.From(base.Dispatcher).VerifyWriteAccess();
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x000B30E4 File Offset: 0x000B24E4
		internal void VerifyAPIReadWrite(DependencyObject value)
		{
			this.VerifyAPIReadWrite();
			MediaSystem.AssertSameContext(this, value);
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x000B3100 File Offset: 0x000B2500
		internal void Precompute()
		{
			if (this.CheckFlagsAnd(VisualFlags.IsSubtreeDirtyForPrecompute))
			{
				using (base.Dispatcher.DisableProcessing())
				{
					MediaContext mediaContext = MediaContext.From(base.Dispatcher);
					try
					{
						mediaContext.PushReadOnlyAccess();
						Rect rect;
						this.PrecomputeRecursive(out rect);
					}
					finally
					{
						mediaContext.PopReadOnlyAccess();
					}
				}
			}
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x000B318C File Offset: 0x000B258C
		internal virtual void PrecomputeContent()
		{
			this._bboxSubgraph = this.GetHitTestBounds();
			if (DoubleUtil.RectHasNaN(this._bboxSubgraph))
			{
				this._bboxSubgraph.X = double.NegativeInfinity;
				this._bboxSubgraph.Y = double.NegativeInfinity;
				this._bboxSubgraph.Width = double.PositiveInfinity;
				this._bboxSubgraph.Height = double.PositiveInfinity;
			}
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x000B3204 File Offset: 0x000B2604
		internal void PrecomputeRecursive(out Rect bboxSubgraph)
		{
			bool flag = this.Enter();
			if (flag)
			{
				try
				{
					if (this.CheckFlagsAnd(VisualFlags.IsSubtreeDirtyForPrecompute))
					{
						this.PrecomputeContent();
						int visualChildrenCount = this.VisualChildrenCount;
						for (int i = 0; i < visualChildrenCount; i++)
						{
							Visual visualChild = this.GetVisualChild(i);
							if (visualChild != null)
							{
								Rect rect;
								visualChild.PrecomputeRecursive(out rect);
								this._bboxSubgraph.Union(rect);
							}
						}
						this.SetFlags(false, VisualFlags.IsSubtreeDirtyForPrecompute);
					}
					bboxSubgraph = this._bboxSubgraph;
					Geometry value = Visual.ClipField.GetValue(this);
					if (value != null)
					{
						bboxSubgraph.Intersect(value.Bounds);
					}
					Transform value2 = Visual.TransformField.GetValue(this);
					if (value2 != null && !value2.IsIdentity)
					{
						Matrix value3 = value2.Value;
						MatrixUtil.TransformRect(ref bboxSubgraph, ref value3);
					}
					if (!bboxSubgraph.IsEmpty)
					{
						bboxSubgraph.X += this._offset.X;
						bboxSubgraph.Y += this._offset.Y;
					}
					Rect? value4 = Visual.ScrollableAreaClipField.GetValue(this);
					if (value4 != null)
					{
						bboxSubgraph.Intersect(value4.Value);
					}
					if (DoubleUtil.RectHasNaN(bboxSubgraph))
					{
						bboxSubgraph.X = double.NegativeInfinity;
						bboxSubgraph.Y = double.NegativeInfinity;
						bboxSubgraph.Width = double.PositiveInfinity;
						bboxSubgraph.Height = double.PositiveInfinity;
					}
					return;
				}
				finally
				{
					this.Exit();
				}
			}
			bboxSubgraph = default(Rect);
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x000B338C File Offset: 0x000B278C
		internal void Render(RenderContext ctx, uint childIndex)
		{
			DUCE.Channel channel = ctx.Channel;
			if (this.CheckFlagsAnd(channel, VisualProxyFlags.IsSubtreeDirtyForRender) || !this.IsOnChannel(channel))
			{
				this.RenderRecursive(ctx);
			}
			if (this.IsOnChannel(channel) && !this.CheckFlagsAnd(channel, VisualProxyFlags.IsConnectedToParent) && !ctx.Root.IsNull)
			{
				DUCE.CompositionNode.InsertChildAt(ctx.Root, this._proxy.GetHandle(channel), childIndex, channel);
				this.SetFlags(channel, true, VisualProxyFlags.IsConnectedToParent);
			}
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x000B3408 File Offset: 0x000B2808
		internal virtual void RenderRecursive(RenderContext ctx)
		{
			bool flag = this.Enter();
			if (flag)
			{
				try
				{
					DUCE.Channel channel = ctx.Channel;
					DUCE.ResourceHandle handle = DUCE.ResourceHandle.Null;
					bool flag2 = this.IsOnChannel(channel);
					VisualProxyFlags flags;
					if (flag2)
					{
						handle = this._proxy.GetHandle(channel);
						flags = this._proxy.GetFlags(channel);
					}
					else
					{
						handle = ((DUCE.IResource)this).AddRefOnChannel(channel);
						this.SetFlags(channel, true, VisualProxyFlags.Viewport3DVisual_IsCameraDirty | VisualProxyFlags.Viewport3DVisual_IsViewportDirty);
						flags = (VisualProxyFlags.IsSubtreeDirtyForRender | VisualProxyFlags.IsTransformDirty | VisualProxyFlags.IsClipDirty | VisualProxyFlags.IsContentDirty | VisualProxyFlags.IsOpacityDirty | VisualProxyFlags.IsOpacityMaskDirty | VisualProxyFlags.IsOffsetDirty | VisualProxyFlags.IsClearTypeHintDirty | VisualProxyFlags.IsGuidelineCollectionDirty | VisualProxyFlags.IsEdgeModeDirty | VisualProxyFlags.IsBitmapScalingModeDirty | VisualProxyFlags.IsEffectDirty | VisualProxyFlags.IsCacheModeDirty | VisualProxyFlags.IsScrollableAreaClipDirty | VisualProxyFlags.IsTextRenderingModeDirty | VisualProxyFlags.IsTextHintingModeDirty);
					}
					this.UpdateCacheMode(channel, handle, flags, flag2);
					this.UpdateTransform(channel, handle, flags, flag2);
					this.UpdateClip(channel, handle, flags, flag2);
					this.UpdateOffset(channel, handle, flags, flag2);
					this.UpdateEffect(channel, handle, flags, flag2);
					this.UpdateGuidelines(channel, handle, flags, flag2);
					this.UpdateContent(ctx, flags, flag2);
					this.UpdateOpacity(channel, handle, flags, flag2);
					this.UpdateOpacityMask(channel, handle, flags, flag2);
					this.UpdateRenderOptions(channel, handle, flags, flag2);
					this.UpdateChildren(ctx, handle);
					this.UpdateScrollableAreaClip(channel, handle, flags, flag2);
					this.SetFlags(channel, false, VisualProxyFlags.IsSubtreeDirtyForRender);
				}
				finally
				{
					this.Exit();
				}
			}
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x000B3518 File Offset: 0x000B2918
		internal bool Enter()
		{
			if (this.CheckFlagsAnd(VisualFlags.ReentrancyFlag))
			{
				return false;
			}
			this.SetFlags(true, VisualFlags.ReentrancyFlag);
			return true;
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x000B3544 File Offset: 0x000B2944
		internal void Exit()
		{
			this.SetFlags(false, VisualFlags.ReentrancyFlag);
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x000B3560 File Offset: 0x000B2960
		private void UpdateOpacity(DUCE.Channel channel, DUCE.ResourceHandle handle, VisualProxyFlags flags, bool isOnChannel)
		{
			if ((flags & VisualProxyFlags.IsOpacityDirty) != VisualProxyFlags.None)
			{
				double value = Visual.OpacityField.GetValue(this);
				if (isOnChannel || value < 1.0)
				{
					DUCE.CompositionNode.SetAlpha(handle, value, channel);
				}
				this.SetFlags(channel, false, VisualProxyFlags.IsOpacityDirty);
			}
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x000B35A4 File Offset: 0x000B29A4
		private void UpdateOpacityMask(DUCE.Channel channel, DUCE.ResourceHandle handle, VisualProxyFlags flags, bool isOnChannel)
		{
			if ((flags & VisualProxyFlags.IsOpacityMaskDirty) != VisualProxyFlags.None)
			{
				Brush value = Visual.OpacityMaskField.GetValue(this);
				if (value != null)
				{
					DUCE.CompositionNode.SetAlphaMask(handle, ((DUCE.IResource)value).AddRefOnChannel(channel), channel);
				}
				else if (isOnChannel)
				{
					DUCE.CompositionNode.SetAlphaMask(handle, DUCE.ResourceHandle.Null, channel);
				}
				this.SetFlags(channel, false, VisualProxyFlags.IsOpacityMaskDirty);
			}
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x000B35F0 File Offset: 0x000B29F0
		private void UpdateTransform(DUCE.Channel channel, DUCE.ResourceHandle handle, VisualProxyFlags flags, bool isOnChannel)
		{
			if ((flags & VisualProxyFlags.IsTransformDirty) != VisualProxyFlags.None)
			{
				Transform value = Visual.TransformField.GetValue(this);
				if (value != null)
				{
					DUCE.CompositionNode.SetTransform(handle, ((DUCE.IResource)value).AddRefOnChannel(channel), channel);
				}
				else if (isOnChannel)
				{
					DUCE.CompositionNode.SetTransform(handle, DUCE.ResourceHandle.Null, channel);
				}
				this.SetFlags(channel, false, VisualProxyFlags.IsTransformDirty);
			}
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x000B363C File Offset: 0x000B2A3C
		private void UpdateEffect(DUCE.Channel channel, DUCE.ResourceHandle handle, VisualProxyFlags flags, bool isOnChannel)
		{
			if ((flags & VisualProxyFlags.IsEffectDirty) != VisualProxyFlags.None)
			{
				Effect value = Visual.EffectField.GetValue(this);
				if (value != null)
				{
					DUCE.CompositionNode.SetEffect(handle, ((DUCE.IResource)value).AddRefOnChannel(channel), channel);
				}
				else if (isOnChannel)
				{
					DUCE.CompositionNode.SetEffect(handle, DUCE.ResourceHandle.Null, channel);
				}
				this.SetFlags(channel, false, VisualProxyFlags.IsEffectDirty);
			}
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x000B3690 File Offset: 0x000B2A90
		private void UpdateCacheMode(DUCE.Channel channel, DUCE.ResourceHandle handle, VisualProxyFlags flags, bool isOnChannel)
		{
			if ((flags & VisualProxyFlags.IsCacheModeDirty) != VisualProxyFlags.None)
			{
				CacheMode value = Visual.CacheModeField.GetValue(this);
				if (value != null)
				{
					DUCE.CompositionNode.SetCacheMode(handle, ((DUCE.IResource)value).AddRefOnChannel(channel), channel);
				}
				else if (isOnChannel)
				{
					DUCE.CompositionNode.SetCacheMode(handle, DUCE.ResourceHandle.Null, channel);
				}
				this.SetFlags(channel, false, VisualProxyFlags.IsCacheModeDirty);
			}
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x000B36E4 File Offset: 0x000B2AE4
		private void UpdateClip(DUCE.Channel channel, DUCE.ResourceHandle handle, VisualProxyFlags flags, bool isOnChannel)
		{
			if ((flags & VisualProxyFlags.IsClipDirty) != VisualProxyFlags.None)
			{
				Geometry value = Visual.ClipField.GetValue(this);
				if (value != null)
				{
					DUCE.CompositionNode.SetClip(handle, ((DUCE.IResource)value).AddRefOnChannel(channel), channel);
				}
				else if (isOnChannel)
				{
					DUCE.CompositionNode.SetClip(handle, DUCE.ResourceHandle.Null, channel);
				}
				this.SetFlags(channel, false, VisualProxyFlags.IsClipDirty);
			}
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x000B3730 File Offset: 0x000B2B30
		private void UpdateScrollableAreaClip(DUCE.Channel channel, DUCE.ResourceHandle handle, VisualProxyFlags flags, bool isOnChannel)
		{
			if ((flags & VisualProxyFlags.IsScrollableAreaClipDirty) != VisualProxyFlags.None)
			{
				Rect? value = Visual.ScrollableAreaClipField.GetValue(this);
				if (isOnChannel || value != null)
				{
					DUCE.CompositionNode.SetScrollableAreaClip(handle, value, channel);
				}
				this.SetFlags(channel, false, VisualProxyFlags.IsScrollableAreaClipDirty);
			}
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x000B3774 File Offset: 0x000B2B74
		private void UpdateOffset(DUCE.Channel channel, DUCE.ResourceHandle handle, VisualProxyFlags flags, bool isOnChannel)
		{
			if ((flags & VisualProxyFlags.IsOffsetDirty) != VisualProxyFlags.None)
			{
				if (isOnChannel || this._offset != default(Vector))
				{
					DUCE.CompositionNode.SetOffset(handle, this._offset.X, this._offset.Y, channel);
				}
				this.SetFlags(channel, false, VisualProxyFlags.IsOffsetDirty);
			}
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x000B37C8 File Offset: 0x000B2BC8
		private void UpdateGuidelines(DUCE.Channel channel, DUCE.ResourceHandle handle, VisualProxyFlags flags, bool isOnChannel)
		{
			if ((flags & VisualProxyFlags.IsGuidelineCollectionDirty) != VisualProxyFlags.None)
			{
				DoubleCollection value = Visual.GuidelinesXField.GetValue(this);
				DoubleCollection value2 = Visual.GuidelinesYField.GetValue(this);
				if (isOnChannel || value != null || value2 != null)
				{
					DUCE.CompositionNode.SetGuidelineCollection(handle, value, value2, channel);
				}
				this.SetFlags(channel, false, VisualProxyFlags.IsGuidelineCollectionDirty);
			}
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x000B3818 File Offset: 0x000B2C18
		private void UpdateRenderOptions(DUCE.Channel channel, DUCE.ResourceHandle handle, VisualProxyFlags flags, bool isOnChannel)
		{
			if ((flags & VisualProxyFlags.IsEdgeModeDirty) != VisualProxyFlags.None || (flags & VisualProxyFlags.IsBitmapScalingModeDirty) != VisualProxyFlags.None || (flags & VisualProxyFlags.IsClearTypeHintDirty) != VisualProxyFlags.None || (flags & VisualProxyFlags.IsTextRenderingModeDirty) != VisualProxyFlags.None || (flags & VisualProxyFlags.IsTextHintingModeDirty) != VisualProxyFlags.None)
			{
				MilRenderOptions milRenderOptions = default(MilRenderOptions);
				EdgeMode value = Visual.EdgeModeField.GetValue(this);
				if (isOnChannel || value != EdgeMode.Unspecified)
				{
					milRenderOptions.Flags |= MilRenderOptionFlags.EdgeMode;
					milRenderOptions.EdgeMode = value;
				}
				BitmapScalingMode value2 = Visual.BitmapScalingModeField.GetValue(this);
				if (isOnChannel || value2 != BitmapScalingMode.Unspecified)
				{
					milRenderOptions.Flags |= MilRenderOptionFlags.BitmapScalingMode;
					milRenderOptions.BitmapScalingMode = value2;
				}
				ClearTypeHint value3 = Visual.ClearTypeHintField.GetValue(this);
				if (isOnChannel || value3 != ClearTypeHint.Auto)
				{
					milRenderOptions.Flags |= MilRenderOptionFlags.ClearTypeHint;
					milRenderOptions.ClearTypeHint = value3;
				}
				TextRenderingMode value4 = Visual.TextRenderingModeField.GetValue(this);
				if (isOnChannel || value4 != TextRenderingMode.Auto)
				{
					milRenderOptions.Flags |= MilRenderOptionFlags.TextRenderingMode;
					milRenderOptions.TextRenderingMode = value4;
				}
				TextHintingMode value5 = Visual.TextHintingModeField.GetValue(this);
				if (isOnChannel || value5 != TextHintingMode.Auto)
				{
					milRenderOptions.Flags |= MilRenderOptionFlags.TextHintingMode;
					milRenderOptions.TextHintingMode = value5;
				}
				if (milRenderOptions.Flags != (MilRenderOptionFlags)0)
				{
					DUCE.CompositionNode.SetRenderOptions(handle, milRenderOptions, channel);
				}
				this.SetFlags(channel, false, VisualProxyFlags.IsClearTypeHintDirty | VisualProxyFlags.IsEdgeModeDirty | VisualProxyFlags.IsBitmapScalingModeDirty | VisualProxyFlags.IsTextRenderingModeDirty | VisualProxyFlags.IsTextHintingModeDirty);
			}
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x000B3944 File Offset: 0x000B2D44
		private void UpdateContent(RenderContext ctx, VisualProxyFlags flags, bool isOnChannel)
		{
			if ((flags & VisualProxyFlags.IsContentDirty) != VisualProxyFlags.None)
			{
				this.RenderContent(ctx, isOnChannel);
				this.SetFlags(ctx.Channel, false, VisualProxyFlags.IsContentDirty);
			}
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x000B396C File Offset: 0x000B2D6C
		private void UpdateChildren(RenderContext ctx, DUCE.ResourceHandle handle)
		{
			DUCE.Channel channel = ctx.Channel;
			uint num = this.CheckFlagsAnd(channel, VisualProxyFlags.IsContentNodeConnected) ? 1U : 0U;
			bool flag = this.CheckFlagsAnd(channel, VisualProxyFlags.IsChildrenZOrderDirty);
			int visualChildrenCount = this.VisualChildrenCount;
			if (flag)
			{
				DUCE.CompositionNode.RemoveAllChildren(handle, channel);
			}
			for (int i = 0; i < visualChildrenCount; i++)
			{
				Visual visualChild = this.GetVisualChild(i);
				if (visualChild != null)
				{
					if (visualChild.CheckFlagsAnd(channel, VisualProxyFlags.IsSubtreeDirtyForRender) || !visualChild.IsOnChannel(channel))
					{
						visualChild.RenderRecursive(ctx);
					}
					if (visualChild.IsOnChannel(channel))
					{
						bool flag2 = visualChild.CheckFlagsAnd(channel, VisualProxyFlags.IsConnectedToParent);
						if (!flag2 || flag)
						{
							DUCE.CompositionNode.InsertChildAt(handle, ((DUCE.IResource)visualChild).GetHandle(channel), num, channel);
							visualChild.SetFlags(channel, true, VisualProxyFlags.IsConnectedToParent);
						}
						num += 1U;
					}
				}
			}
			this.SetFlags(channel, false, VisualProxyFlags.IsChildrenZOrderDirty);
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x000B3A34 File Offset: 0x000B2E34
		internal void InvalidateHitTestBounds()
		{
			this.VerifyAPIReadWrite();
			Visual.PropagateFlags(this, VisualFlags.IsSubtreeDirtyForPrecompute, VisualProxyFlags.None);
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x000B3A50 File Offset: 0x000B2E50
		internal virtual Rect GetHitTestBounds()
		{
			return this.GetContentBounds();
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000B3A64 File Offset: 0x000B2E64
		internal HitTestResult HitTest(Point point)
		{
			return this.HitTest(point, true);
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x000B3A7C File Offset: 0x000B2E7C
		internal HitTestResult HitTest(Point point, bool include2DOn3D)
		{
			Visual.TopMostHitResult topMostHitResult = new Visual.TopMostHitResult();
			VisualTreeHelper.HitTest(this, include2DOn3D ? null : new HitTestFilterCallback(topMostHitResult.NoNested2DFilter), new HitTestResultCallback(topMostHitResult.HitTestResult), new PointHitTestParameters(point));
			return topMostHitResult._hitResult;
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x000B3AC0 File Offset: 0x000B2EC0
		internal void HitTest(HitTestFilterCallback filterCallback, HitTestResultCallback resultCallback, HitTestParameters hitTestParameters)
		{
			if (resultCallback == null)
			{
				throw new ArgumentNullException("resultCallback");
			}
			if (hitTestParameters == null)
			{
				throw new ArgumentNullException("hitTestParameters");
			}
			this.VerifyAPIReadWrite();
			this.Precompute();
			PointHitTestParameters pointHitTestParameters = hitTestParameters as PointHitTestParameters;
			if (pointHitTestParameters != null)
			{
				Point hitPoint = pointHitTestParameters.HitPoint;
				try
				{
					this.HitTestPoint(filterCallback, resultCallback, pointHitTestParameters);
					return;
				}
				catch
				{
					pointHitTestParameters.SetHitPoint(hitPoint);
					throw;
				}
				finally
				{
				}
			}
			GeometryHitTestParameters geometryHitTestParameters = hitTestParameters as GeometryHitTestParameters;
			if (geometryHitTestParameters != null)
			{
				try
				{
					this.HitTestGeometry(filterCallback, resultCallback, geometryHitTestParameters);
					return;
				}
				catch
				{
					geometryHitTestParameters.EmergencyRestoreOriginalTransform();
					throw;
				}
			}
			Invariant.Assert(false, string.Format(CultureInfo.InvariantCulture, "'{0}' HitTestParameters are not supported on {1}.", new object[]
			{
				hitTestParameters.GetType().Name,
				base.GetType().Name
			}));
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000B3BC8 File Offset: 0x000B2FC8
		internal HitTestResultBehavior HitTestPoint(HitTestFilterCallback filterCallback, HitTestResultCallback resultCallback, PointHitTestParameters pointParams)
		{
			Geometry visualClip = this.VisualClip;
			if (this._bboxSubgraph.Contains(pointParams.HitPoint) && (visualClip == null || visualClip.FillContains(pointParams.HitPoint)))
			{
				HitTestFilterBehavior hitTestFilterBehavior = HitTestFilterBehavior.Continue;
				if (filterCallback != null)
				{
					hitTestFilterBehavior = filterCallback(this);
					if (hitTestFilterBehavior == HitTestFilterBehavior.ContinueSkipSelfAndChildren)
					{
						return HitTestResultBehavior.Continue;
					}
					if (hitTestFilterBehavior == HitTestFilterBehavior.Stop)
					{
						return HitTestResultBehavior.Stop;
					}
				}
				Point hitPoint = pointParams.HitPoint;
				Point point = hitPoint;
				if (this.CheckFlagsAnd(VisualFlags.NodeHasEffect))
				{
					Effect value = Visual.EffectField.GetValue(this);
					if (value != null)
					{
						GeneralTransform inverse = value.EffectMapping.Inverse;
						if (inverse != Transform.Identity)
						{
							bool flag = false;
							Point? point2 = Effect.WorldToUnit(hitPoint, this._bboxSubgraph);
							if (point2 != null)
							{
								Point unitPoint = default(Point);
								if (inverse.TryTransform(point2.Value, out unitPoint))
								{
									Point? point3 = Effect.UnitToWorld(unitPoint, this._bboxSubgraph);
									if (point3 != null)
									{
										point = point3.Value;
										flag = true;
									}
								}
							}
							if (!flag)
							{
								return HitTestResultBehavior.Continue;
							}
						}
					}
				}
				if (hitTestFilterBehavior != HitTestFilterBehavior.ContinueSkipChildren)
				{
					int visualChildrenCount = this.VisualChildrenCount;
					for (int i = visualChildrenCount - 1; i >= 0; i--)
					{
						Visual visualChild = this.GetVisualChild(i);
						if (visualChild != null)
						{
							Rect? value2 = Visual.ScrollableAreaClipField.GetValue(visualChild);
							if (value2 == null || value2.Value.Contains(point))
							{
								Point point4 = point;
								point4 -= visualChild._offset;
								Transform value3 = Visual.TransformField.GetValue(visualChild);
								if (value3 != null)
								{
									Matrix value4 = value3.Value;
									if (!value4.HasInverse)
									{
										goto IL_1A6;
									}
									value4.Invert();
									point4 *= value4;
								}
								pointParams.SetHitPoint(point4);
								HitTestResultBehavior hitTestResultBehavior = visualChild.HitTestPoint(filterCallback, resultCallback, pointParams);
								pointParams.SetHitPoint(hitPoint);
								if (hitTestResultBehavior == HitTestResultBehavior.Stop)
								{
									return HitTestResultBehavior.Stop;
								}
							}
						}
						IL_1A6:;
					}
				}
				if (hitTestFilterBehavior != HitTestFilterBehavior.ContinueSkipSelf)
				{
					pointParams.SetHitPoint(point);
					HitTestResultBehavior hitTestResultBehavior2 = this.HitTestPointInternal(filterCallback, resultCallback, pointParams);
					pointParams.SetHitPoint(hitPoint);
					if (hitTestResultBehavior2 == HitTestResultBehavior.Stop)
					{
						return HitTestResultBehavior.Stop;
					}
				}
			}
			return HitTestResultBehavior.Continue;
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000B3DAC File Offset: 0x000B31AC
		internal GeneralTransform TransformToOuterSpace()
		{
			Matrix identity = Matrix.Identity;
			GeneralTransformGroup generalTransformGroup = null;
			if (this.CheckFlagsAnd(VisualFlags.NodeHasEffect))
			{
				Effect value = Visual.EffectField.GetValue(this);
				if (value != null)
				{
					GeneralTransform generalTransform = value.CoerceToUnitSpaceGeneralTransform(value.EffectMapping, this.VisualDescendantBounds);
					Transform affineTransform = generalTransform.AffineTransform;
					if (affineTransform != null)
					{
						Matrix value2 = affineTransform.Value;
						MatrixUtil.MultiplyMatrix(ref identity, ref value2);
					}
					else
					{
						generalTransformGroup = new GeneralTransformGroup();
						generalTransformGroup.Children.Add(generalTransform);
					}
				}
				else
				{
					BitmapEffectState value3 = Visual.BitmapEffectStateField.GetValue(this);
				}
			}
			Transform value4 = Visual.TransformField.GetValue(this);
			if (value4 != null)
			{
				Matrix value5 = value4.Value;
				MatrixUtil.MultiplyMatrix(ref identity, ref value5);
			}
			identity.Translate(this._offset.X, this._offset.Y);
			GeneralTransform generalTransform2;
			if (generalTransformGroup == null)
			{
				generalTransform2 = new MatrixTransform(identity);
			}
			else
			{
				generalTransformGroup.Children.Add(new MatrixTransform(identity));
				generalTransform2 = generalTransformGroup;
			}
			generalTransform2.Freeze();
			return generalTransform2;
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000B3E9C File Offset: 0x000B329C
		internal HitTestResultBehavior HitTestGeometry(HitTestFilterCallback filterCallback, HitTestResultCallback resultCallback, GeometryHitTestParameters geometryParams)
		{
			Geometry visualClip = this.VisualClip;
			if (visualClip != null)
			{
				IntersectionDetail intersectionDetail = visualClip.FillContainsWithDetail(geometryParams.InternalHitGeometry);
				if (intersectionDetail == IntersectionDetail.Empty)
				{
					return HitTestResultBehavior.Continue;
				}
			}
			if (this._bboxSubgraph.IntersectsWith(geometryParams.Bounds))
			{
				HitTestFilterBehavior hitTestFilterBehavior = HitTestFilterBehavior.Continue;
				if (filterCallback != null)
				{
					hitTestFilterBehavior = filterCallback(this);
					if (hitTestFilterBehavior == HitTestFilterBehavior.ContinueSkipSelfAndChildren)
					{
						return HitTestResultBehavior.Continue;
					}
					if (hitTestFilterBehavior == HitTestFilterBehavior.Stop)
					{
						return HitTestResultBehavior.Stop;
					}
				}
				int visualChildrenCount = this.VisualChildrenCount;
				if (hitTestFilterBehavior != HitTestFilterBehavior.ContinueSkipChildren)
				{
					for (int i = visualChildrenCount - 1; i >= 0; i--)
					{
						Visual visualChild = this.GetVisualChild(i);
						if (visualChild != null)
						{
							Rect? value = Visual.ScrollableAreaClipField.GetValue(visualChild);
							if (value != null)
							{
								RectangleGeometry rectangleGeometry = new RectangleGeometry(value.Value);
								IntersectionDetail intersectionDetail2 = rectangleGeometry.FillContainsWithDetail(geometryParams.InternalHitGeometry);
								if (intersectionDetail2 == IntersectionDetail.Empty)
								{
									goto IL_12A;
								}
							}
							Matrix identity = Matrix.Identity;
							identity.Translate(-visualChild._offset.X, -visualChild._offset.Y);
							Transform value2 = Visual.TransformField.GetValue(visualChild);
							if (value2 != null)
							{
								Matrix value3 = value2.Value;
								if (!value3.HasInverse)
								{
									goto IL_12A;
								}
								value3.Invert();
								MatrixUtil.MultiplyMatrix(ref identity, ref value3);
							}
							geometryParams.PushMatrix(ref identity);
							HitTestResultBehavior hitTestResultBehavior = visualChild.HitTestGeometry(filterCallback, resultCallback, geometryParams);
							geometryParams.PopMatrix();
							if (hitTestResultBehavior == HitTestResultBehavior.Stop)
							{
								return HitTestResultBehavior.Stop;
							}
						}
						IL_12A:;
					}
				}
				if (hitTestFilterBehavior != HitTestFilterBehavior.ContinueSkipSelf)
				{
					GeometryHitTestResult geometryHitTestResult = this.HitTestCore(geometryParams);
					if (geometryHitTestResult != null)
					{
						return resultCallback(geometryHitTestResult);
					}
				}
			}
			return HitTestResultBehavior.Continue;
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x000B3FF8 File Offset: 0x000B33F8
		internal virtual HitTestResultBehavior HitTestPointInternal(HitTestFilterCallback filterCallback, HitTestResultCallback resultCallback, PointHitTestParameters hitTestParameters)
		{
			HitTestResult hitTestResult = this.HitTestCore(hitTestParameters);
			if (hitTestResult != null)
			{
				return resultCallback(hitTestResult);
			}
			return HitTestResultBehavior.Continue;
		}

		/// <summary>Determina se um valor de coordenadas de ponto está dentro dos limites do objeto visual.</summary>
		/// <param name="hitTestParameters">Um objeto <see cref="T:System.Windows.Media.PointHitTestParameters" /> que especifica o <see cref="T:System.Windows.Point" /> em relação ao qual realizar o teste de clique.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.HitTestResult" /> que representa o <see cref="T:System.Windows.Media.Visual" /> que é retornado de um teste de clique.</returns>
		// Token: 0x06002CFC RID: 11516 RVA: 0x000B401C File Offset: 0x000B341C
		protected virtual HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
		{
			if (hitTestParameters == null)
			{
				throw new ArgumentNullException("hitTestParameters");
			}
			if (this.GetHitTestBounds().Contains(hitTestParameters.HitPoint))
			{
				return new PointHitTestResult(this, hitTestParameters.HitPoint);
			}
			return null;
		}

		/// <summary>Determina se um valor de geometria está dentro dos limites do objeto visual.</summary>
		/// <param name="hitTestParameters">Um objeto <see cref="T:System.Windows.Media.GeometryHitTestParameters" /> que especifica o <see cref="T:System.Windows.Media.Geometry" /> em relação ao qual realizar o teste de clique.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.GeometryHitTestResult" /> que representa o resultado do teste de clique.</returns>
		// Token: 0x06002CFD RID: 11517 RVA: 0x000B405C File Offset: 0x000B345C
		protected virtual GeometryHitTestResult HitTestCore(GeometryHitTestParameters hitTestParameters)
		{
			if (hitTestParameters == null)
			{
				throw new ArgumentNullException("hitTestParameters");
			}
			RectangleGeometry rectangleGeometry = new RectangleGeometry(this.GetHitTestBounds());
			IntersectionDetail intersectionDetail = rectangleGeometry.FillContainsWithDetail(hitTestParameters.InternalHitGeometry);
			if (intersectionDetail != IntersectionDetail.Empty)
			{
				return new GeometryHitTestResult(this, intersectionDetail);
			}
			return null;
		}

		/// <summary>Obtém o número de elementos filhos do <see cref="T:System.Windows.Media.Visual" />.</summary>
		/// <returns>O número de elementos filho.</returns>
		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06002CFE RID: 11518 RVA: 0x000B40A0 File Offset: 0x000B34A0
		protected virtual int VisualChildrenCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06002CFF RID: 11519 RVA: 0x000B40B0 File Offset: 0x000B34B0
		internal int InternalVisualChildrenCount
		{
			get
			{
				return this.VisualChildrenCount;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06002D00 RID: 11520 RVA: 0x000B40C4 File Offset: 0x000B34C4
		internal virtual int InternalVisual2DOr3DChildrenCount
		{
			get
			{
				return this.VisualChildrenCount;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06002D01 RID: 11521 RVA: 0x000B40D8 File Offset: 0x000B34D8
		internal bool HasVisualChildren
		{
			get
			{
				return (this._flags & VisualFlags.HasChildren) > VisualFlags.None;
			}
		}

		/// <summary>Retorna o <see cref="T:System.Windows.Media.Visual" /> especificado na <see cref="T:System.Windows.Media.VisualCollection" /> pai.</summary>
		/// <param name="index">O índice do objeto visual na <see cref="T:System.Windows.Media.VisualCollection" />.</param>
		/// <returns>O filho na <see cref="T:System.Windows.Media.VisualCollection" /> no valor de <paramref name="index" /> especificado.</returns>
		// Token: 0x06002D02 RID: 11522 RVA: 0x000B40F4 File Offset: 0x000B34F4
		protected virtual Visual GetVisualChild(int index)
		{
			throw new ArgumentOutOfRangeException("index", index, SR.Get("Visual_ArgumentOutOfRange"));
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x000B411C File Offset: 0x000B351C
		internal Visual InternalGetVisualChild(int index)
		{
			return this.GetVisualChild(index);
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x000B4130 File Offset: 0x000B3530
		internal virtual DependencyObject InternalGet2DOr3DVisualChild(int index)
		{
			return this.GetVisualChild(index);
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x000B4144 File Offset: 0x000B3544
		internal void InternalAddVisualChild(Visual child)
		{
			this.AddVisualChild(child);
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x000B4158 File Offset: 0x000B3558
		internal void InternalRemoveVisualChild(Visual child)
		{
			this.RemoveVisualChild(child);
		}

		/// <summary>Define a relação pai-filho entre dois visuais.</summary>
		/// <param name="child">O objeto visual filho a ser adicionado ao visual pai.</param>
		// Token: 0x06002D07 RID: 11527 RVA: 0x000B416C File Offset: 0x000B356C
		protected void AddVisualChild(Visual child)
		{
			if (child == null)
			{
				return;
			}
			if (child._parent != null)
			{
				throw new ArgumentException(SR.Get("Visual_HasParent"));
			}
			VisualDiagnostics.VerifyVisualTreeChange(this);
			this.SetFlags(true, VisualFlags.HasChildren);
			child._parent = this;
			Visual.PropagateFlags(this, VisualFlags.IsSubtreeDirtyForPrecompute, VisualProxyFlags.IsSubtreeDirtyForRender);
			Visual.PropagateFlags(child, VisualFlags.IsSubtreeDirtyForPrecompute, VisualProxyFlags.IsSubtreeDirtyForRender);
			UIElement.PropagateResumeLayout(this, child);
			bool? isProcessPerMonitorDpiAware = HwndTarget.IsProcessPerMonitorDpiAware;
			bool flag = true;
			if ((isProcessPerMonitorDpiAware.GetValueOrDefault() == flag & isProcessPerMonitorDpiAware != null) && HwndTarget.IsPerMonitorDpiScalingEnabled)
			{
				bool flag2 = this.CheckFlagsAnd(VisualFlags.DpiScaleFlag1);
				bool flag3 = this.CheckFlagsAnd(VisualFlags.DpiScaleFlag2);
				int index = 0;
				if (flag2 && flag3)
				{
					index = Visual.DpiIndex.GetValue(this);
				}
				child.RecursiveSetDpiScaleVisualFlags(new DpiRecursiveChangeArgs(new DpiFlags(flag2, flag3, index), child.GetDpi(), this.GetDpi()));
			}
			this.OnVisualChildrenChanged(child, null);
			child.FireOnVisualParentChanged(null);
			VisualDiagnostics.OnVisualChildChanged(this, child, true);
		}

		/// <summary>Remove a relação pai-filho entre dois visuais.</summary>
		/// <param name="child">O objeto visual filho a ser removido ao visual pai.</param>
		// Token: 0x06002D08 RID: 11528 RVA: 0x000B424C File Offset: 0x000B364C
		protected void RemoveVisualChild(Visual child)
		{
			if (child == null || child._parent == null)
			{
				return;
			}
			if (child._parent != this)
			{
				throw new ArgumentException(SR.Get("Visual_NotChild"));
			}
			VisualDiagnostics.VerifyVisualTreeChange(this);
			VisualDiagnostics.OnVisualChildChanged(this, child, false);
			if (this.InternalVisual2DOr3DChildrenCount == 0)
			{
				this.SetFlags(false, VisualFlags.HasChildren);
			}
			for (int i = 0; i < this._proxy.Count; i++)
			{
				DUCE.Channel channel = this._proxy.GetChannel(i);
				if (child.CheckFlagsAnd(channel, VisualProxyFlags.IsConnectedToParent))
				{
					child.SetFlags(channel, false, VisualProxyFlags.IsConnectedToParent);
					((DUCE.IResource)child).RemoveChildFromParent(this, channel);
					((DUCE.IResource)child).ReleaseOnChannel(channel);
				}
			}
			child._parent = null;
			Visual.PropagateFlags(this, VisualFlags.IsSubtreeDirtyForPrecompute, VisualProxyFlags.IsSubtreeDirtyForRender);
			UIElement.PropagateSuspendLayout(child);
			child.FireOnVisualParentChanged(this);
			this.OnVisualChildrenChanged(null, child);
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x000B4314 File Offset: 0x000B3714
		[FriendAccessAllowed]
		internal void InvalidateZOrder()
		{
			if (this.VisualChildrenCount == 0)
			{
				return;
			}
			Visual.PropagateFlags(this, VisualFlags.IsSubtreeDirtyForPrecompute, VisualProxyFlags.IsSubtreeDirtyForRender);
			this.SetFlagsOnAllChannels(true, VisualProxyFlags.IsChildrenZOrderDirty);
			InputManager.SafeCurrentNotifyHitTestInvalidated();
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06002D0A RID: 11530 RVA: 0x000B4344 File Offset: 0x000B3744
		// (set) Token: 0x06002D0B RID: 11531 RVA: 0x000B4360 File Offset: 0x000B3760
		internal uint TreeLevel
		{
			get
			{
				return (uint)((this._flags & ~(VisualFlags.IsSubtreeDirtyForPrecompute | VisualFlags.ShouldPostRender | VisualFlags.IsUIElement | VisualFlags.IsLayoutSuspended | VisualFlags.IsVisualChildrenIterationInProgress | VisualFlags.Are3DContentBoundsValid | VisualFlags.FindCommonAncestor | VisualFlags.IsLayoutIslandRoot | VisualFlags.UseLayoutRounding | VisualFlags.VisibilityCache_Visible | VisualFlags.VisibilityCache_TakesSpace | VisualFlags.RegisteredForAncestorChanged | VisualFlags.SubTreeHoldsAncestorChanged | VisualFlags.NodeIsCyclicBrushRoot | VisualFlags.NodeHasEffect | VisualFlags.IsViewport3DVisual | VisualFlags.ReentrancyFlag | VisualFlags.HasChildren | VisualFlags.BitmapEffectEmulationDisabled | VisualFlags.DpiScaleFlag1 | VisualFlags.DpiScaleFlag2)) >> 21);
			}
			set
			{
				if (value > 2047U)
				{
					throw new InvalidOperationException(SR.Get("LayoutManager_DeepRecursion", new object[]
					{
						2047U
					}));
				}
				this._flags = ((this._flags & ~(VisualFlags.TreeLevelBit0 | VisualFlags.TreeLevelBit1 | VisualFlags.TreeLevelBit2 | VisualFlags.TreeLevelBit3 | VisualFlags.TreeLevelBit4 | VisualFlags.TreeLevelBit5 | VisualFlags.TreeLevelBit6 | VisualFlags.TreeLevelBit7 | VisualFlags.TreeLevelBit8 | VisualFlags.TreeLevelBit9 | VisualFlags.TreeLevelBit10)) | (VisualFlags)(value << 21));
			}
		}

		/// <summary>Obtém o pai da árvore visual do objeto visual.</summary>
		/// <returns>O pai <see cref="T:System.Windows.Media.Visual" />.</returns>
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06002D0C RID: 11532 RVA: 0x000B43B0 File Offset: 0x000B37B0
		protected DependencyObject VisualParent
		{
			get
			{
				this.VerifyAPIReadOnly();
				return this.InternalVisualParent;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06002D0D RID: 11533 RVA: 0x000B43CC File Offset: 0x000B37CC
		internal DependencyObject InternalVisualParent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x000B43E0 File Offset: 0x000B37E0
		[FriendAccessAllowed]
		internal void InternalSetOffsetWorkaround(Vector offset)
		{
			this.VisualOffset = offset;
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x000B43F4 File Offset: 0x000B37F4
		[FriendAccessAllowed]
		internal void InternalSetTransformWorkaround(Transform transform)
		{
			this.VisualTransform = transform;
		}

		/// <summary>Obtém ou define o valor <see cref="T:System.Windows.Media.Transform" /> para o <see cref="T:System.Windows.Media.Visual" />.</summary>
		/// <returns>O valor de transformação do visual.</returns>
		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06002D10 RID: 11536 RVA: 0x000B4408 File Offset: 0x000B3808
		// (set) Token: 0x06002D11 RID: 11537 RVA: 0x000B4428 File Offset: 0x000B3828
		protected internal Transform VisualTransform
		{
			get
			{
				this.VerifyAPIReadOnly();
				return Visual.TransformField.GetValue(this);
			}
			protected set
			{
				this.VerifyAPIReadWrite(value);
				Transform value2 = Visual.TransformField.GetValue(this);
				if (value2 == value)
				{
					return;
				}
				if (value != null && !value.IsFrozen)
				{
					value.Changed += this.TransformChangedHandler;
				}
				if (value2 != null)
				{
					if (!value2.IsFrozen)
					{
						value2.Changed -= this.TransformChangedHandler;
					}
					this.DisconnectAttachedResource(VisualProxyFlags.IsTransformDirty, value2);
				}
				Visual.TransformField.SetValue(this, value);
				this.SetFlagsOnAllChannels(true, VisualProxyFlags.IsTransformDirty);
				this.TransformChanged(null, null);
			}
		}

		/// <summary>Obtém ou define o efeito de bitmap a ser aplicado ao <see cref="T:System.Windows.Media.Visual" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Effects.Effect" /> que representa o efeito de bitmap.</returns>
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06002D12 RID: 11538 RVA: 0x000B44A4 File Offset: 0x000B38A4
		// (set) Token: 0x06002D13 RID: 11539 RVA: 0x000B44C0 File Offset: 0x000B38C0
		protected internal Effect VisualEffect
		{
			get
			{
				this.VerifyAPIReadOnly();
				return this.VisualEffectInternal;
			}
			protected set
			{
				this.VerifyAPIReadWrite(value);
				BitmapEffectState value2 = Visual.UserProvidedBitmapEffectData.GetValue(this);
				if (value2 == null)
				{
					this.VisualEffectInternal = value;
					return;
				}
				if (value != null)
				{
					throw new Exception(SR.Get("Effect_CombinedLegacyAndNew"));
				}
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06002D14 RID: 11540 RVA: 0x000B4500 File Offset: 0x000B3900
		// (set) Token: 0x06002D15 RID: 11541 RVA: 0x000B4524 File Offset: 0x000B3924
		internal Effect VisualEffectInternal
		{
			get
			{
				if (this.NodeHasLegacyBitmapEffect)
				{
					return null;
				}
				return Visual.EffectField.GetValue(this);
			}
			set
			{
				Effect value2 = Visual.EffectField.GetValue(this);
				if (value2 == value)
				{
					return;
				}
				if (value != null && !value.IsFrozen)
				{
					value.Changed += this.EffectChangedHandler;
				}
				if (value2 != null)
				{
					if (!value2.IsFrozen)
					{
						value2.Changed -= this.EffectChangedHandler;
					}
					this.DisconnectAttachedResource(VisualProxyFlags.IsEffectDirty, value2);
				}
				this.SetFlags(value != null, VisualFlags.NodeHasEffect);
				Visual.EffectField.SetValue(this, value);
				this.SetFlagsOnAllChannels(true, VisualProxyFlags.IsEffectDirty);
				this.EffectChanged(null, null);
			}
		}

		/// <summary>Obtém ou define o valor <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> para o <see cref="T:System.Windows.Media.Visual" />.</summary>
		/// <returns>O efeito de bitmap para este objeto visual.</returns>
		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06002D16 RID: 11542 RVA: 0x000B45B0 File Offset: 0x000B39B0
		// (set) Token: 0x06002D17 RID: 11543 RVA: 0x000B45DC File Offset: 0x000B39DC
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		protected internal BitmapEffect VisualBitmapEffect
		{
			get
			{
				this.VerifyAPIReadOnly();
				BitmapEffectState value = Visual.UserProvidedBitmapEffectData.GetValue(this);
				if (value != null)
				{
					return value.BitmapEffect;
				}
				return null;
			}
			protected set
			{
				this.VerifyAPIReadWrite(value);
				Effect value2 = Visual.EffectField.GetValue(this);
				BitmapEffectState bitmapEffectState = Visual.UserProvidedBitmapEffectData.GetValue(this);
				if (bitmapEffectState == null && value2 != null)
				{
					if (value != null)
					{
						throw new Exception(SR.Get("Effect_CombinedLegacyAndNew"));
					}
					return;
				}
				else
				{
					BitmapEffect bitmapEffect = (bitmapEffectState == null) ? null : bitmapEffectState.BitmapEffect;
					if (bitmapEffect == value)
					{
						return;
					}
					if (value == null)
					{
						Visual.UserProvidedBitmapEffectData.SetValue(this, null);
					}
					else
					{
						if (bitmapEffectState == null)
						{
							bitmapEffectState = new BitmapEffectState();
							Visual.UserProvidedBitmapEffectData.SetValue(this, bitmapEffectState);
						}
						bitmapEffectState.BitmapEffect = value;
					}
					if (value != null && !value.IsFrozen)
					{
						value.Changed += this.BitmapEffectEmulationChanged;
					}
					if (bitmapEffect != null && !bitmapEffect.IsFrozen)
					{
						bitmapEffect.Changed -= this.BitmapEffectEmulationChanged;
					}
					this.BitmapEffectEmulationChanged(null, null);
					return;
				}
			}
		}

		/// <summary>Obtém ou define o valor <see cref="T:System.Windows.Media.Effects.BitmapEffectInput" /> para o <see cref="T:System.Windows.Media.Visual" />.</summary>
		/// <returns>O valor de entrada do efeito de bitmap para este objeto visual.</returns>
		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06002D18 RID: 11544 RVA: 0x000B46A8 File Offset: 0x000B3AA8
		// (set) Token: 0x06002D19 RID: 11545 RVA: 0x000B46D4 File Offset: 0x000B3AD4
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		protected internal BitmapEffectInput VisualBitmapEffectInput
		{
			get
			{
				this.VerifyAPIReadOnly();
				BitmapEffectState value = Visual.UserProvidedBitmapEffectData.GetValue(this);
				if (value != null)
				{
					return value.BitmapEffectInput;
				}
				return null;
			}
			protected set
			{
				this.VerifyAPIReadWrite(value);
				Effect value2 = Visual.EffectField.GetValue(this);
				BitmapEffectState bitmapEffectState = Visual.UserProvidedBitmapEffectData.GetValue(this);
				if (bitmapEffectState == null && value2 != null)
				{
					if (value != null)
					{
						throw new Exception(SR.Get("Effect_CombinedLegacyAndNew"));
					}
					return;
				}
				else
				{
					BitmapEffectInput bitmapEffectInput = (bitmapEffectState == null) ? null : bitmapEffectState.BitmapEffectInput;
					if (bitmapEffectInput == value)
					{
						return;
					}
					if (bitmapEffectState == null)
					{
						bitmapEffectState = new BitmapEffectState();
						Visual.UserProvidedBitmapEffectData.SetValue(this, bitmapEffectState);
					}
					bitmapEffectState.BitmapEffectInput = value;
					if (value != null && !value.IsFrozen)
					{
						value.Changed += this.BitmapEffectEmulationChanged;
					}
					if (bitmapEffectInput != null && !bitmapEffectInput.IsFrozen)
					{
						bitmapEffectInput.Changed -= this.BitmapEffectEmulationChanged;
					}
					this.BitmapEffectEmulationChanged(null, null);
					return;
				}
			}
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x000B478C File Offset: 0x000B3B8C
		internal void BitmapEffectEmulationChanged(object sender, EventArgs e)
		{
			BitmapEffectState value = Visual.UserProvidedBitmapEffectData.GetValue(this);
			BitmapEffect bitmapEffect = (value == null) ? null : value.BitmapEffect;
			BitmapEffectInput bitmapEffectInput = (value == null) ? null : value.BitmapEffectInput;
			if (bitmapEffect == null)
			{
				this.VisualBitmapEffectInternal = null;
				this.VisualBitmapEffectInputInternal = null;
				this.VisualEffectInternal = null;
				return;
			}
			if (bitmapEffectInput != null)
			{
				this.VisualEffectInternal = null;
				this.VisualBitmapEffectInternal = bitmapEffect;
				this.VisualBitmapEffectInputInternal = bitmapEffectInput;
				return;
			}
			if (RenderCapability.IsShaderEffectSoftwareRenderingSupported && bitmapEffect.CanBeEmulatedUsingEffectPipeline() && !this.CheckFlagsAnd(VisualFlags.BitmapEffectEmulationDisabled))
			{
				this.VisualBitmapEffectInternal = null;
				this.VisualBitmapEffectInputInternal = null;
				Effect emulatingEffect = bitmapEffect.GetEmulatingEffect();
				this.VisualEffectInternal = emulatingEffect;
				return;
			}
			this.VisualEffectInternal = null;
			this.VisualBitmapEffectInputInternal = null;
			this.VisualBitmapEffectInternal = bitmapEffect;
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06002D1B RID: 11547 RVA: 0x000B4840 File Offset: 0x000B3C40
		// (set) Token: 0x06002D1C RID: 11548 RVA: 0x000B4858 File Offset: 0x000B3C58
		internal bool BitmapEffectEmulationDisabled
		{
			get
			{
				return this.CheckFlagsAnd(VisualFlags.BitmapEffectEmulationDisabled);
			}
			set
			{
				if (value != this.CheckFlagsAnd(VisualFlags.BitmapEffectEmulationDisabled))
				{
					this.SetFlags(value, VisualFlags.BitmapEffectEmulationDisabled);
					this.BitmapEffectEmulationChanged(null, null);
				}
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06002D1D RID: 11549 RVA: 0x000B4888 File Offset: 0x000B3C88
		// (set) Token: 0x06002D1E RID: 11550 RVA: 0x000B48B8 File Offset: 0x000B3CB8
		internal BitmapEffect VisualBitmapEffectInternal
		{
			get
			{
				this.VerifyAPIReadOnly();
				if (this.NodeHasLegacyBitmapEffect)
				{
					return Visual.BitmapEffectStateField.GetValue(this).BitmapEffect;
				}
				return null;
			}
			set
			{
				BitmapEffectState bitmapEffectState = Visual.BitmapEffectStateField.GetValue(this);
				BitmapEffect bitmapEffect = (bitmapEffectState == null) ? null : bitmapEffectState.BitmapEffect;
				if (bitmapEffect == value)
				{
					return;
				}
				if (value == null)
				{
					Visual.BitmapEffectStateField.SetValue(this, null);
					return;
				}
				if (bitmapEffectState == null)
				{
					bitmapEffectState = new BitmapEffectState();
					Visual.BitmapEffectStateField.SetValue(this, bitmapEffectState);
				}
				bitmapEffectState.BitmapEffect = value;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06002D1F RID: 11551 RVA: 0x000B4914 File Offset: 0x000B3D14
		// (set) Token: 0x06002D20 RID: 11552 RVA: 0x000B4940 File Offset: 0x000B3D40
		internal BitmapEffectInput VisualBitmapEffectInputInternal
		{
			get
			{
				this.VerifyAPIReadOnly();
				BitmapEffectState value = Visual.BitmapEffectStateField.GetValue(this);
				if (value != null)
				{
					return value.BitmapEffectInput;
				}
				return null;
			}
			set
			{
				this.VerifyAPIReadWrite();
				BitmapEffectState bitmapEffectState = Visual.BitmapEffectStateField.GetValue(this);
				BitmapEffectInput bitmapEffectInput = (bitmapEffectState == null) ? null : bitmapEffectState.BitmapEffectInput;
				if (bitmapEffectInput == value)
				{
					return;
				}
				if (bitmapEffectState == null)
				{
					bitmapEffectState = new BitmapEffectState();
					Visual.BitmapEffectStateField.SetValue(this, bitmapEffectState);
				}
				bitmapEffectState.BitmapEffectInput = value;
			}
		}

		/// <summary>Obtém ou define uma representação armazenada em cache do <see cref="T:System.Windows.Media.Visual" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.CacheMode" /> que contém uma representação armazenada em cache do <see cref="T:System.Windows.Media.Visual" />.</returns>
		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06002D21 RID: 11553 RVA: 0x000B4990 File Offset: 0x000B3D90
		// (set) Token: 0x06002D22 RID: 11554 RVA: 0x000B49B0 File Offset: 0x000B3DB0
		protected internal CacheMode VisualCacheMode
		{
			get
			{
				this.VerifyAPIReadOnly();
				return Visual.CacheModeField.GetValue(this);
			}
			protected set
			{
				this.VerifyAPIReadWrite(value);
				CacheMode value2 = Visual.CacheModeField.GetValue(this);
				if (value2 == value)
				{
					return;
				}
				if (value != null && !value.IsFrozen)
				{
					value.Changed += this.CacheModeChangedHandler;
				}
				if (value2 != null)
				{
					if (!value2.IsFrozen)
					{
						value2.Changed -= this.CacheModeChangedHandler;
					}
					this.DisconnectAttachedResource(VisualProxyFlags.IsCacheModeDirty, value2);
				}
				Visual.CacheModeField.SetValue(this, value);
				this.SetFlagsOnAllChannels(true, VisualProxyFlags.IsCacheModeDirty);
				this.CacheModeChanged(null, null);
			}
		}

		/// <summary>Obtém ou define uma área rolável recortada para o <see cref="T:System.Windows.Media.Visual" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Rect" /> que representa a área de recorte rolável ou <see langword="null" /> se nenhuma área de recorte for atribuída.</returns>
		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06002D23 RID: 11555 RVA: 0x000B4A34 File Offset: 0x000B3E34
		// (set) Token: 0x06002D24 RID: 11556 RVA: 0x000B4A54 File Offset: 0x000B3E54
		protected internal Rect? VisualScrollableAreaClip
		{
			get
			{
				this.VerifyAPIReadOnly();
				return Visual.ScrollableAreaClipField.GetValue(this);
			}
			protected set
			{
				this.VerifyAPIReadWrite();
				if (Visual.ScrollableAreaClipField.GetValue(this) != value)
				{
					Visual.ScrollableAreaClipField.SetValue(this, value);
					this.SetFlagsOnAllChannels(true, VisualProxyFlags.IsScrollableAreaClipDirty);
					this.ScrollableAreaClipChanged(null, null);
				}
			}
		}

		/// <summary>Obtém ou define a região de corte do <see cref="T:System.Windows.Media.Visual" /> como um valor <see cref="T:System.Windows.Media.Geometry" />.</summary>
		/// <returns>O valor da região de recorte do visual como um tipo <see cref="T:System.Windows.Media.Geometry" />.</returns>
		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06002D25 RID: 11557 RVA: 0x000B4ACC File Offset: 0x000B3ECC
		// (set) Token: 0x06002D26 RID: 11558 RVA: 0x000B4AEC File Offset: 0x000B3EEC
		protected internal Geometry VisualClip
		{
			get
			{
				this.VerifyAPIReadOnly();
				return Visual.ClipField.GetValue(this);
			}
			protected set
			{
				this.ChangeVisualClip(value, false);
			}
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x000B4B04 File Offset: 0x000B3F04
		internal void ChangeVisualClip(Geometry newClip, bool dontSetWhenClose)
		{
			this.VerifyAPIReadWrite(newClip);
			Geometry value = Visual.ClipField.GetValue(this);
			if (value == newClip || (dontSetWhenClose && value != null && newClip != null && value.AreClose(newClip)))
			{
				return;
			}
			if (newClip != null && !newClip.IsFrozen)
			{
				newClip.Changed += this.ClipChangedHandler;
			}
			if (value != null)
			{
				if (!value.IsFrozen)
				{
					value.Changed -= this.ClipChangedHandler;
				}
				this.DisconnectAttachedResource(VisualProxyFlags.IsClipDirty, value);
			}
			Visual.ClipField.SetValue(this, newClip);
			this.SetFlagsOnAllChannels(true, VisualProxyFlags.IsClipDirty);
			this.ClipChanged(null, null);
		}

		/// <summary>Obtém ou define o valor de deslocamento do objeto visual.</summary>
		/// <returns>Um <see cref="T:System.Windows.Vector" /> que especifica o valor de deslocamento.</returns>
		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06002D28 RID: 11560 RVA: 0x000B4B90 File Offset: 0x000B3F90
		// (set) Token: 0x06002D29 RID: 11561 RVA: 0x000B4BA4 File Offset: 0x000B3FA4
		protected internal Vector VisualOffset
		{
			get
			{
				return this._offset;
			}
			protected set
			{
				this.VerifyAPIReadWrite();
				if (value != this._offset)
				{
					this._offset = value;
					this.SetFlagsOnAllChannels(true, VisualProxyFlags.IsOffsetDirty);
					VisualFlags flags = VisualFlags.IsSubtreeDirtyForPrecompute;
					Visual.PropagateFlags(this, flags, VisualProxyFlags.IsSubtreeDirtyForRender);
				}
			}
		}

		/// <summary>Obtém ou define a opacidade do <see cref="T:System.Windows.Media.Visual" />.</summary>
		/// <returns>O valor da opacidade do visual.</returns>
		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x000B4BE0 File Offset: 0x000B3FE0
		// (set) Token: 0x06002D2B RID: 11563 RVA: 0x000B4C00 File Offset: 0x000B4000
		protected internal double VisualOpacity
		{
			get
			{
				this.VerifyAPIReadOnly();
				return Visual.OpacityField.GetValue(this);
			}
			protected set
			{
				this.VerifyAPIReadWrite();
				if (Visual.OpacityField.GetValue(this) == value)
				{
					return;
				}
				Visual.OpacityField.SetValue(this, value);
				this.SetFlagsOnAllChannels(true, VisualProxyFlags.IsOpacityDirty);
				Visual.PropagateFlags(this, VisualFlags.None, VisualProxyFlags.IsSubtreeDirtyForRender);
			}
		}

		/// <summary>Obtém ou define o modo de borda do <see cref="T:System.Windows.Media.Visual" /> com um valor <see cref="T:System.Windows.Media.EdgeMode" />.</summary>
		/// <returns>O valor <see cref="T:System.Windows.Media.EdgeMode" /> do visual.</returns>
		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06002D2C RID: 11564 RVA: 0x000B4C40 File Offset: 0x000B4040
		// (set) Token: 0x06002D2D RID: 11565 RVA: 0x000B4C60 File Offset: 0x000B4060
		protected internal EdgeMode VisualEdgeMode
		{
			get
			{
				this.VerifyAPIReadOnly();
				return Visual.EdgeModeField.GetValue(this);
			}
			protected set
			{
				this.VerifyAPIReadWrite();
				if (Visual.EdgeModeField.GetValue(this) == value)
				{
					return;
				}
				Visual.EdgeModeField.SetValue(this, value);
				this.SetFlagsOnAllChannels(true, VisualProxyFlags.IsEdgeModeDirty);
				Visual.PropagateFlags(this, VisualFlags.None, VisualProxyFlags.IsSubtreeDirtyForRender);
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.BitmapScalingMode" /> do <see cref="T:System.Windows.Media.Visual" />.</summary>
		/// <returns>O valor de <see cref="T:System.Windows.Media.BitmapScalingMode" /> para <see cref="T:System.Windows.Media.Visual" />.</returns>
		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002D2E RID: 11566 RVA: 0x000B4CA4 File Offset: 0x000B40A4
		// (set) Token: 0x06002D2F RID: 11567 RVA: 0x000B4CC4 File Offset: 0x000B40C4
		protected internal BitmapScalingMode VisualBitmapScalingMode
		{
			get
			{
				this.VerifyAPIReadOnly();
				return Visual.BitmapScalingModeField.GetValue(this);
			}
			protected set
			{
				this.VerifyAPIReadWrite();
				if (Visual.BitmapScalingModeField.GetValue(this) == value)
				{
					return;
				}
				Visual.BitmapScalingModeField.SetValue(this, value);
				this.SetFlagsOnAllChannels(true, VisualProxyFlags.IsBitmapScalingModeDirty);
				Visual.PropagateFlags(this, VisualFlags.None, VisualProxyFlags.IsSubtreeDirtyForRender);
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.ClearTypeHint" /> que determina como o ClearType é renderizado no <see cref="T:System.Windows.Media.Visual" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.ClearTypeHint" /> do <see cref="T:System.Windows.Media.Visual" />.</returns>
		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06002D30 RID: 11568 RVA: 0x000B4D08 File Offset: 0x000B4108
		// (set) Token: 0x06002D31 RID: 11569 RVA: 0x000B4D28 File Offset: 0x000B4128
		protected internal ClearTypeHint VisualClearTypeHint
		{
			get
			{
				this.VerifyAPIReadOnly();
				return Visual.ClearTypeHintField.GetValue(this);
			}
			set
			{
				this.VerifyAPIReadWrite();
				if (Visual.ClearTypeHintField.GetValue(this) == value)
				{
					return;
				}
				Visual.ClearTypeHintField.SetValue(this, value);
				this.SetFlagsOnAllChannels(true, VisualProxyFlags.IsClearTypeHintDirty);
				Visual.PropagateFlags(this, VisualFlags.None, VisualProxyFlags.IsSubtreeDirtyForRender);
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.TextRenderingMode" /> do <see cref="T:System.Windows.Media.Visual" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.TextRenderingMode" /> aplicado ao <see cref="T:System.Windows.Media.Visual" />.</returns>
		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06002D32 RID: 11570 RVA: 0x000B4D6C File Offset: 0x000B416C
		// (set) Token: 0x06002D33 RID: 11571 RVA: 0x000B4D8C File Offset: 0x000B418C
		protected internal TextRenderingMode VisualTextRenderingMode
		{
			get
			{
				this.VerifyAPIReadOnly();
				return Visual.TextRenderingModeField.GetValue(this);
			}
			set
			{
				this.VerifyAPIReadWrite();
				if (Visual.TextRenderingModeField.GetValue(this) == value)
				{
					return;
				}
				Visual.TextRenderingModeField.SetValue(this, value);
				this.SetFlagsOnAllChannels(true, VisualProxyFlags.IsTextRenderingModeDirty);
				Visual.PropagateFlags(this, VisualFlags.None, VisualProxyFlags.IsSubtreeDirtyForRender);
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.TextHintingMode" /> do <see cref="T:System.Windows.Media.Visual" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.TextHintingMode" /> aplicado ao <see cref="T:System.Windows.Media.Visual" />.</returns>
		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06002D34 RID: 11572 RVA: 0x000B4DD0 File Offset: 0x000B41D0
		// (set) Token: 0x06002D35 RID: 11573 RVA: 0x000B4DF0 File Offset: 0x000B41F0
		protected internal TextHintingMode VisualTextHintingMode
		{
			get
			{
				this.VerifyAPIReadOnly();
				return Visual.TextHintingModeField.GetValue(this);
			}
			set
			{
				this.VerifyAPIReadWrite();
				if (Visual.TextHintingModeField.GetValue(this) == value)
				{
					return;
				}
				Visual.TextHintingModeField.SetValue(this, value);
				this.SetFlagsOnAllChannels(true, VisualProxyFlags.IsTextHintingModeDirty);
				Visual.PropagateFlags(this, VisualFlags.None, VisualProxyFlags.IsSubtreeDirtyForRender);
			}
		}

		/// <summary>Obtém ou define o valor <see cref="T:System.Windows.Media.Brush" /> que representa a máscara de opacidade do <see cref="T:System.Windows.Media.Visual" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Brush" /> que representa o valor de máscara de opacidade do visual.</returns>
		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06002D36 RID: 11574 RVA: 0x000B4E34 File Offset: 0x000B4234
		// (set) Token: 0x06002D37 RID: 11575 RVA: 0x000B4E54 File Offset: 0x000B4254
		protected internal Brush VisualOpacityMask
		{
			get
			{
				this.VerifyAPIReadOnly();
				return Visual.OpacityMaskField.GetValue(this);
			}
			protected set
			{
				this.VerifyAPIReadWrite(value);
				Brush value2 = Visual.OpacityMaskField.GetValue(this);
				if (value2 == value)
				{
					return;
				}
				if (value != null && !value.IsFrozen)
				{
					value.Changed += this.OpacityMaskChangedHandler;
				}
				if (value2 != null)
				{
					if (!value2.IsFrozen)
					{
						value2.Changed -= this.OpacityMaskChangedHandler;
					}
					this.DisconnectAttachedResource(VisualProxyFlags.IsOpacityMaskDirty, value2);
				}
				Visual.OpacityMaskField.SetValue(this, value);
				this.SetFlagsOnAllChannels(true, VisualProxyFlags.IsOpacityMaskDirty);
				this.OpacityMaskChanged(null, null);
			}
		}

		/// <summary>Obtém ou define a coleção de diretrizes (vertical) da coordenada X.</summary>
		/// <returns>A coleção de diretrizes de coordenada X do elemento visual.</returns>
		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06002D38 RID: 11576 RVA: 0x000B4ED0 File Offset: 0x000B42D0
		// (set) Token: 0x06002D39 RID: 11577 RVA: 0x000B4EF0 File Offset: 0x000B42F0
		protected internal DoubleCollection VisualXSnappingGuidelines
		{
			get
			{
				this.VerifyAPIReadOnly();
				return Visual.GuidelinesXField.GetValue(this);
			}
			protected set
			{
				this.VerifyAPIReadWrite(value);
				DoubleCollection value2 = Visual.GuidelinesXField.GetValue(this);
				if (value2 == value)
				{
					return;
				}
				if (value != null && !value.IsFrozen)
				{
					value.Changed += this.GuidelinesChangedHandler;
				}
				if (value2 != null && !value2.IsFrozen)
				{
					value2.Changed -= this.GuidelinesChangedHandler;
				}
				Visual.GuidelinesXField.SetValue(this, value);
				this.GuidelinesChanged(null, null);
			}
		}

		/// <summary>Obtém ou define a coleção de diretrizes (horizontal) da coordenada y.</summary>
		/// <returns>A coleção de diretrizes de coordenada y do visual.</returns>
		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06002D3A RID: 11578 RVA: 0x000B4F5C File Offset: 0x000B435C
		// (set) Token: 0x06002D3B RID: 11579 RVA: 0x000B4F7C File Offset: 0x000B437C
		protected internal DoubleCollection VisualYSnappingGuidelines
		{
			get
			{
				this.VerifyAPIReadOnly();
				return Visual.GuidelinesYField.GetValue(this);
			}
			protected set
			{
				this.VerifyAPIReadWrite(value);
				DoubleCollection value2 = Visual.GuidelinesYField.GetValue(this);
				if (value2 == value)
				{
					return;
				}
				if (value != null && !value.IsFrozen)
				{
					value.Changed += this.GuidelinesChangedHandler;
				}
				if (value2 != null && !value2.IsFrozen)
				{
					value2.Changed -= this.GuidelinesChangedHandler;
				}
				Visual.GuidelinesYField.SetValue(this, value);
				this.GuidelinesChanged(null, null);
			}
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x000B4FE8 File Offset: 0x000B43E8
		internal void DisconnectAttachedResource(VisualProxyFlags correspondingFlag, DUCE.IResource attachedResource)
		{
			bool flag = correspondingFlag == VisualProxyFlags.IsContentConnected;
			for (int i = 0; i < this._proxy.Count; i++)
			{
				DUCE.Channel channel = this._proxy.GetChannel(i);
				VisualProxyFlags flags = this._proxy.GetFlags(i);
				bool flag2 = (flags & correspondingFlag) > VisualProxyFlags.None;
				if (flag2 == flag)
				{
					this.SetFlags(channel, true, correspondingFlag);
					attachedResource.ReleaseOnChannel(channel);
					if (flag)
					{
						this._proxy.SetFlags(i, false, VisualProxyFlags.IsContentConnected);
					}
				}
			}
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x000B5060 File Offset: 0x000B4460
		internal virtual DrawingGroup GetDrawing()
		{
			this.VerifyAPIReadOnly();
			return null;
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x000B5074 File Offset: 0x000B4474
		internal virtual void FireOnVisualParentChanged(DependencyObject oldParent)
		{
			this.OnVisualParentChanged(oldParent);
			if (oldParent == null)
			{
				if (this.CheckFlagsAnd(VisualFlags.SubTreeHoldsAncestorChanged))
				{
					Visual.SetTreeBits(this._parent, VisualFlags.SubTreeHoldsAncestorChanged, VisualFlags.RegisteredForAncestorChanged);
				}
			}
			else if (this.CheckFlagsAnd(VisualFlags.SubTreeHoldsAncestorChanged))
			{
				Visual.ClearTreeBits(oldParent, VisualFlags.SubTreeHoldsAncestorChanged, VisualFlags.RegisteredForAncestorChanged);
			}
			AncestorChangedEventArgs args = new AncestorChangedEventArgs(this, oldParent);
			Visual.ProcessAncestorChangedNotificationRecursive(this, args);
		}

		/// <summary>Chamado quando o pai do objeto visual for alterado.</summary>
		/// <param name="oldParent">Um valor do tipo <see cref="T:System.Windows.DependencyObject" /> que representa o pai anterior do objeto <see cref="T:System.Windows.Media.Visual" />. Se o objeto <see cref="T:System.Windows.Media.Visual" /> não tiver um pai anterior, o valor do parâmetro será <see langword="null" />.</param>
		// Token: 0x06002D3F RID: 11583 RVA: 0x000B50DC File Offset: 0x000B44DC
		protected internal virtual void OnVisualParentChanged(DependencyObject oldParent)
		{
		}

		/// <summary>Chamado quando o <see cref="T:System.Windows.Media.VisualCollection" /> de um objeto visual é modificado.</summary>
		/// <param name="visualAdded">O <see cref="T:System.Windows.Media.Visual" /> que foi adicionado à coleção</param>
		/// <param name="visualRemoved">O <see cref="T:System.Windows.Media.Visual" /> que foi removido da coleção</param>
		// Token: 0x06002D40 RID: 11584 RVA: 0x000B50EC File Offset: 0x000B44EC
		protected internal virtual void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
		{
		}

		/// <summary>Chamado quando o DPI em que esse modo de exibição é renderizado é alterada.</summary>
		/// <param name="oldDpi">A configuração da escala de DPI anterior.</param>
		/// <param name="newDpi">A nova configuração da escala de DPI.</param>
		// Token: 0x06002D41 RID: 11585 RVA: 0x000B50FC File Offset: 0x000B44FC
		protected virtual void OnDpiChanged(DpiScale oldDpi, DpiScale newDpi)
		{
		}

		// Token: 0x140001B8 RID: 440
		// (add) Token: 0x06002D42 RID: 11586 RVA: 0x000B510C File Offset: 0x000B450C
		// (remove) Token: 0x06002D43 RID: 11587 RVA: 0x000B5158 File Offset: 0x000B4558
		internal event Visual.AncestorChangedEventHandler VisualAncestorChanged
		{
			add
			{
				Visual.AncestorChangedEventHandler ancestorChangedEventHandler = Visual.AncestorChangedEventField.GetValue(this);
				if (ancestorChangedEventHandler == null)
				{
					ancestorChangedEventHandler = value;
				}
				else
				{
					ancestorChangedEventHandler = (Visual.AncestorChangedEventHandler)Delegate.Combine(ancestorChangedEventHandler, value);
				}
				Visual.AncestorChangedEventField.SetValue(this, ancestorChangedEventHandler);
				Visual.SetTreeBits(this, VisualFlags.SubTreeHoldsAncestorChanged, VisualFlags.RegisteredForAncestorChanged);
			}
			remove
			{
				if (this.CheckFlagsAnd(VisualFlags.SubTreeHoldsAncestorChanged))
				{
					Visual.ClearTreeBits(this, VisualFlags.SubTreeHoldsAncestorChanged, VisualFlags.RegisteredForAncestorChanged);
				}
				Visual.AncestorChangedEventHandler ancestorChangedEventHandler = Visual.AncestorChangedEventField.GetValue(this);
				if (ancestorChangedEventHandler != null)
				{
					ancestorChangedEventHandler = (Visual.AncestorChangedEventHandler)Delegate.Remove(ancestorChangedEventHandler, value);
					if (ancestorChangedEventHandler == null)
					{
						Visual.AncestorChangedEventField.ClearValue(this);
						return;
					}
					Visual.AncestorChangedEventField.SetValue(this, ancestorChangedEventHandler);
				}
			}
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x000B51BC File Offset: 0x000B45BC
		internal static void ProcessAncestorChangedNotificationRecursive(DependencyObject e, AncestorChangedEventArgs args)
		{
			if (e is Visual3D)
			{
				Visual3D.ProcessAncestorChangedNotificationRecursive(e, args);
				return;
			}
			Visual visual = e as Visual;
			if (!visual.CheckFlagsAnd(VisualFlags.SubTreeHoldsAncestorChanged))
			{
				return;
			}
			Visual.AncestorChangedEventHandler value = Visual.AncestorChangedEventField.GetValue(visual);
			if (value != null)
			{
				value(visual, args);
			}
			int internalVisual2DOr3DChildrenCount = visual.InternalVisual2DOr3DChildrenCount;
			for (int i = 0; i < internalVisual2DOr3DChildrenCount; i++)
			{
				DependencyObject dependencyObject = visual.InternalGet2DOr3DVisualChild(i);
				if (dependencyObject != null)
				{
					Visual.ProcessAncestorChangedNotificationRecursive(dependencyObject, args);
				}
			}
		}

		/// <summary>Determina se o objeto visual é um ancestral do objeto visual descendente.</summary>
		/// <param name="descendant">Um valor do tipo <see cref="T:System.Windows.DependencyObject" />.</param>
		/// <returns>
		///   <see langword="true" /> se o objeto visual for um ancestral de <paramref name="descendant" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002D45 RID: 11589 RVA: 0x000B522C File Offset: 0x000B462C
		public bool IsAncestorOf(DependencyObject descendant)
		{
			Visual visual;
			Visual3D visual3D;
			VisualTreeUtils.AsNonNullVisual(descendant, out visual, out visual3D);
			if (visual3D != null)
			{
				return visual3D.IsDescendantOf(this);
			}
			return visual.IsDescendantOf(this);
		}

		/// <summary>Determina se o objeto visual é um descendente do objeto visual ancestral.</summary>
		/// <param name="ancestor">Um valor do tipo <see cref="T:System.Windows.DependencyObject" />.</param>
		/// <returns>
		///   <see langword="true" /> se o objeto visual for um descendente do <paramref name="ancestor" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002D46 RID: 11590 RVA: 0x000B5258 File Offset: 0x000B4658
		public bool IsDescendantOf(DependencyObject ancestor)
		{
			if (ancestor == null)
			{
				throw new ArgumentNullException("ancestor");
			}
			VisualTreeUtils.EnsureVisual(ancestor);
			DependencyObject dependencyObject = this;
			while (dependencyObject != null && dependencyObject != ancestor)
			{
				Visual visual = dependencyObject as Visual;
				if (visual != null)
				{
					dependencyObject = visual._parent;
				}
				else
				{
					Visual3D visual3D = dependencyObject as Visual3D;
					if (visual3D != null)
					{
						dependencyObject = visual3D.InternalVisualParent;
					}
					else
					{
						dependencyObject = null;
					}
				}
			}
			return dependencyObject == ancestor;
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x000B52B0 File Offset: 0x000B46B0
		internal void SetFlagsToRoot(bool value, VisualFlags flag)
		{
			Visual visual = this;
			for (;;)
			{
				visual.SetFlags(value, flag);
				Visual visual2 = visual._parent as Visual;
				if (visual._parent != null && visual2 == null)
				{
					break;
				}
				visual = visual2;
				if (visual == null)
				{
					return;
				}
			}
			((Visual3D)visual._parent).SetFlagsToRoot(value, flag);
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x000B52F8 File Offset: 0x000B46F8
		internal DependencyObject FindFirstAncestorWithFlagsAnd(VisualFlags flag)
		{
			Visual visual = this;
			while (!visual.CheckFlagsAnd(flag))
			{
				DependencyObject parent = visual._parent;
				visual = (parent as Visual);
				if (visual == null)
				{
					Visual3D visual3D = parent as Visual3D;
					if (visual3D != null)
					{
						return visual3D.FindFirstAncestorWithFlagsAnd(flag);
					}
				}
				if (visual == null)
				{
					return null;
				}
			}
			return visual;
		}

		/// <summary>Retorna o ancestral comum de dois objetos visuais.</summary>
		/// <param name="otherVisual">Um objeto visual do tipo <see cref="T:System.Windows.DependencyObject" />.</param>
		/// <returns>O ancestral comum do objeto visual e <paramref name="otherVisual" />, se houver; caso contrário, <see langword="null" />.</returns>
		// Token: 0x06002D49 RID: 11593 RVA: 0x000B533C File Offset: 0x000B473C
		public DependencyObject FindCommonVisualAncestor(DependencyObject otherVisual)
		{
			this.VerifyAPIReadOnly(otherVisual);
			if (otherVisual == null)
			{
				throw new ArgumentNullException("otherVisual");
			}
			this.SetFlagsToRoot(false, VisualFlags.FindCommonAncestor);
			VisualTreeUtils.SetFlagsToRoot(otherVisual, true, VisualFlags.FindCommonAncestor);
			return this.FindFirstAncestorWithFlagsAnd(VisualFlags.FindCommonAncestor);
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x000B5378 File Offset: 0x000B4778
		internal virtual void InvalidateForceInheritPropertyOnChildren(DependencyProperty property)
		{
			UIElement.InvalidateForceInheritPropertyOnChildren(this, property);
		}

		/// <summary>Retorna uma transformação que pode ser usada para transformar as coordenadas do <see cref="T:System.Windows.Media.Visual" /> para o ancestral <see cref="T:System.Windows.Media.Visual" /> especificado do objeto visual.</summary>
		/// <param name="ancestor">O <see cref="T:System.Windows.Media.Visual" /> para o qual as coordenadas são transformadas.</param>
		/// <returns>Um valor do tipo <see cref="T:System.Windows.Media.GeneralTransform" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ancestor" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">O <paramref name="ancestor" /> não é um ancestral de visual.</exception>
		/// <exception cref="T:System.InvalidOperationException">Os objetos visuais não estão relacionados.</exception>
		// Token: 0x06002D4B RID: 11595 RVA: 0x000B538C File Offset: 0x000B478C
		public GeneralTransform TransformToAncestor(Visual ancestor)
		{
			if (ancestor == null)
			{
				throw new ArgumentNullException("ancestor");
			}
			this.VerifyAPIReadOnly(ancestor);
			return this.InternalTransformToAncestor(ancestor, false);
		}

		/// <summary>Retorna uma transformação que pode ser usada para transformar as coordenadas do <see cref="T:System.Windows.Media.Visual" /> para o ancestral <see cref="T:System.Windows.Media.Media3D.Visual3D" /> especificado do objeto visual.</summary>
		/// <param name="ancestor">O <see cref="T:System.Windows.Media.Media3D.Visual3D" /> para o qual as coordenadas são transformadas.</param>
		/// <returns>Uma transformação que pode ser usada para transformar as coordenadas do <see cref="T:System.Windows.Media.Visual" /> para o ancestral <see cref="T:System.Windows.Media.Media3D.Visual3D" /> especificado do objeto visual.</returns>
		// Token: 0x06002D4C RID: 11596 RVA: 0x000B53B8 File Offset: 0x000B47B8
		public GeneralTransform2DTo3D TransformToAncestor(Visual3D ancestor)
		{
			if (ancestor == null)
			{
				throw new ArgumentNullException("ancestor");
			}
			this.VerifyAPIReadOnly(ancestor);
			return this.InternalTransformToAncestor(ancestor, false);
		}

		/// <summary>Retorna uma transformação que pode ser usada para transformar as coordenadas do <see cref="T:System.Windows.Media.Visual" /> até o descendente do objeto visual especificado.</summary>
		/// <param name="descendant">O <see cref="T:System.Windows.Media.Visual" /> para o qual as coordenadas são transformadas.</param>
		/// <returns>Um valor do tipo <see cref="T:System.Windows.Media.GeneralTransform" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="descendant" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">O visual especificado não é um ancestral do visual <paramref name="descendant" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">Os objetos visuais não estão relacionados.</exception>
		// Token: 0x06002D4D RID: 11597 RVA: 0x000B53E4 File Offset: 0x000B47E4
		public GeneralTransform TransformToDescendant(Visual descendant)
		{
			if (descendant == null)
			{
				throw new ArgumentNullException("descendant");
			}
			this.VerifyAPIReadOnly(descendant);
			return descendant.InternalTransformToAncestor(this, true);
		}

		/// <summary>Retorna uma transformação que pode ser usada para transformar as coordenadas do <see cref="T:System.Windows.Media.Visual" /> até o objeto visual especificado.</summary>
		/// <param name="visual">O <see cref="T:System.Windows.Media.Visual" /> para o qual as coordenadas são transformadas.</param>
		/// <returns>Um valor do tipo <see cref="T:System.Windows.Media.GeneralTransform" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="visual" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">Os objetos visuais não estão relacionados.</exception>
		// Token: 0x06002D4E RID: 11598 RVA: 0x000B5410 File Offset: 0x000B4810
		public GeneralTransform TransformToVisual(Visual visual)
		{
			DependencyObject dependencyObject = this.FindCommonVisualAncestor(visual);
			Visual visual2 = dependencyObject as Visual;
			if (visual2 == null)
			{
				throw new InvalidOperationException(SR.Get("Visual_NoCommonAncestor"));
			}
			GeneralTransform generalTransform;
			Matrix matrix;
			bool flag = this.TrySimpleTransformToAncestor(visual2, false, out generalTransform, out matrix);
			GeneralTransform generalTransform2;
			Matrix matrix2;
			bool flag2 = visual.TrySimpleTransformToAncestor(visual2, true, out generalTransform2, out matrix2);
			if (flag && flag2)
			{
				MatrixUtil.MultiplyMatrix(ref matrix, ref matrix2);
				MatrixTransform matrixTransform = new MatrixTransform(matrix);
				matrixTransform.Freeze();
				return matrixTransform;
			}
			if (flag)
			{
				generalTransform = new MatrixTransform(matrix);
				generalTransform.Freeze();
			}
			else if (flag2)
			{
				generalTransform2 = new MatrixTransform(matrix2);
				generalTransform2.Freeze();
			}
			if (generalTransform2 != null)
			{
				GeneralTransformGroup generalTransformGroup = new GeneralTransformGroup();
				generalTransformGroup.Children.Add(generalTransform);
				generalTransformGroup.Children.Add(generalTransform2);
				generalTransformGroup.Freeze();
				return generalTransformGroup;
			}
			return generalTransform;
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000B54D0 File Offset: 0x000B48D0
		private GeneralTransform InternalTransformToAncestor(Visual ancestor, bool inverse)
		{
			GeneralTransform result;
			Matrix matrix;
			bool flag = this.TrySimpleTransformToAncestor(ancestor, inverse, out result, out matrix);
			if (flag)
			{
				MatrixTransform matrixTransform = new MatrixTransform(matrix);
				matrixTransform.Freeze();
				return matrixTransform;
			}
			return result;
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000B5500 File Offset: 0x000B4900
		internal bool TrySimpleTransformToAncestor(Visual ancestor, bool inverse, out GeneralTransform generalTransform, out Matrix simpleTransform)
		{
			bool flag = false;
			DependencyObject dependencyObject = this;
			Matrix identity = Matrix.Identity;
			GeneralTransformGroup generalTransformGroup = null;
			while (VisualTreeHelper.GetParent(dependencyObject) != null && dependencyObject != ancestor)
			{
				Visual visual = dependencyObject as Visual;
				if (visual != null)
				{
					if (visual.CheckFlagsAnd(VisualFlags.NodeHasEffect))
					{
						Effect value = Visual.EffectField.GetValue(visual);
						if (value != null)
						{
							GeneralTransform generalTransform2 = value.CoerceToUnitSpaceGeneralTransform(value.EffectMapping, visual.VisualDescendantBounds);
							Transform affineTransform = generalTransform2.AffineTransform;
							if (affineTransform != null)
							{
								Matrix value2 = affineTransform.Value;
								MatrixUtil.MultiplyMatrix(ref identity, ref value2);
							}
							else
							{
								if (generalTransformGroup == null)
								{
									generalTransformGroup = new GeneralTransformGroup();
								}
								generalTransformGroup.Children.Add(new MatrixTransform(identity));
								identity = Matrix.Identity;
								generalTransformGroup.Children.Add(generalTransform2);
							}
						}
					}
					Transform value3 = Visual.TransformField.GetValue(visual);
					if (value3 != null)
					{
						Matrix value4 = value3.Value;
						MatrixUtil.MultiplyMatrix(ref identity, ref value4);
					}
					identity.Translate(visual._offset.X, visual._offset.Y);
					dependencyObject = visual._parent;
				}
				else
				{
					Viewport2DVisual3D viewport2DVisual3D = dependencyObject as Viewport2DVisual3D;
					if (generalTransformGroup == null)
					{
						generalTransformGroup = new GeneralTransformGroup();
					}
					generalTransformGroup.Children.Add(new MatrixTransform(identity));
					identity = Matrix.Identity;
					Visual fromVisual;
					if (flag)
					{
						fromVisual = viewport2DVisual3D.Visual;
					}
					else
					{
						fromVisual = this;
						flag = true;
					}
					generalTransformGroup.Children.Add(new GeneralTransform2DTo3DTo2D(viewport2DVisual3D, fromVisual));
					dependencyObject = VisualTreeHelper.GetContainingVisual2D(viewport2DVisual3D);
				}
			}
			if (dependencyObject != ancestor)
			{
				throw new InvalidOperationException(SR.Get(inverse ? "Visual_NotADescendant" : "Visual_NotAnAncestor"));
			}
			if (generalTransformGroup != null)
			{
				if (!identity.IsIdentity)
				{
					generalTransformGroup.Children.Add(new MatrixTransform(identity));
				}
				if (inverse)
				{
					generalTransformGroup = (GeneralTransformGroup)generalTransformGroup.Inverse;
				}
				if (generalTransformGroup != null)
				{
					generalTransformGroup.Freeze();
				}
				generalTransform = generalTransformGroup;
				simpleTransform = default(Matrix);
				return false;
			}
			generalTransform = null;
			if (inverse)
			{
				if (!identity.HasInverse)
				{
					simpleTransform = default(Matrix);
					return false;
				}
				identity.Invert();
			}
			simpleTransform = identity;
			return true;
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x000B56F0 File Offset: 0x000B4AF0
		private GeneralTransform2DTo3D InternalTransformToAncestor(Visual3D ancestor, bool inverse)
		{
			GeneralTransform2DTo3D generalTransform2DTo3D = null;
			if (this.TrySimpleTransformToAncestor(ancestor, out generalTransform2DTo3D))
			{
				generalTransform2DTo3D.Freeze();
				return generalTransform2DTo3D;
			}
			return null;
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x000B5714 File Offset: 0x000B4B14
		internal bool TrySimpleTransformToAncestor(Visual3D ancestor, out GeneralTransform2DTo3D transformTo3D)
		{
			Viewport2DVisual3D viewport2DVisual3D = VisualTreeHelper.GetContainingVisual3D(this) as Viewport2DVisual3D;
			if (viewport2DVisual3D == null)
			{
				throw new InvalidOperationException(SR.Get("Visual_NotAnAncestor"));
			}
			GeneralTransform transform2D = this.TransformToAncestor(viewport2DVisual3D.Visual);
			GeneralTransform3D transform3D = viewport2DVisual3D.TransformToAncestor(ancestor);
			transformTo3D = new GeneralTransform2DTo3D(transform2D, viewport2DVisual3D, transform3D);
			return true;
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x000B5760 File Offset: 0x000B4B60
		internal DpiScale GetDpi()
		{
			object dpiLock = UIElement.DpiLock;
			DpiScale result;
			lock (dpiLock)
			{
				if (UIElement.DpiScaleXValues.Count == 0)
				{
					return UIElement.EnsureDpiScale();
				}
				result = new DpiScale(UIElement.DpiScaleXValues[0], UIElement.DpiScaleYValues[0]);
				int num = 0;
				num = (this.CheckFlagsAnd(VisualFlags.DpiScaleFlag1) ? (num | 1) : num);
				num = (this.CheckFlagsAnd(VisualFlags.DpiScaleFlag2) ? (num | 2) : num);
				if (num < 3 && UIElement.DpiScaleXValues[num] != 0.0 && UIElement.DpiScaleYValues[num] != 0.0)
				{
					result = new DpiScale(UIElement.DpiScaleXValues[num], UIElement.DpiScaleYValues[num]);
				}
				else if (num >= 3)
				{
					int value = Visual.DpiIndex.GetValue(this);
					result = new DpiScale(UIElement.DpiScaleXValues[value], UIElement.DpiScaleYValues[value]);
				}
			}
			return result;
		}

		/// <summary>Converte um <see cref="T:System.Windows.Point" /> que representa o sistema de coordenadas atual do <see cref="T:System.Windows.Media.Visual" /> em um <see cref="T:System.Windows.Point" /> nas coordenadas da tela.</summary>
		/// <param name="point">O valor <see cref="T:System.Windows.Point" /> que representa o sistema de coordenadas atual do <see cref="T:System.Windows.Media.Visual" />.</param>
		/// <returns>O valor <see cref="T:System.Windows.Point" /> convertido em coordenadas da tela.</returns>
		// Token: 0x06002D54 RID: 11604 RVA: 0x000B5884 File Offset: 0x000B4C84
		public Point PointToScreen(Point point)
		{
			this.VerifyAPIReadOnly();
			PresentationSource presentationSource = PresentationSource.FromVisual(this);
			if (presentationSource == null)
			{
				throw new InvalidOperationException(SR.Get("Visual_NoPresentationSource"));
			}
			GeneralTransform generalTransform = this.TransformToAncestor(presentationSource.RootVisual);
			if (generalTransform == null || !generalTransform.TryTransform(point, out point))
			{
				throw new InvalidOperationException(SR.Get("Visual_CannotTransformPoint"));
			}
			point = PointUtil.RootToClient(point, presentationSource);
			point = PointUtil.ClientToScreen(point, presentationSource);
			return point;
		}

		/// <summary>Converte um <see cref="T:System.Windows.Point" /> em coordenadas de tela em um <see cref="T:System.Windows.Point" /> que representa o sistema de coordenadas atual do <see cref="T:System.Windows.Media.Visual" />.</summary>
		/// <param name="point">O valor de <see cref="T:System.Windows.Point" /> em coordenadas de tela.</param>
		/// <returns>O valor de <see cref="T:System.Windows.Point" /> convertido que representa o sistema de coordenadas atual do <see cref="T:System.Windows.Media.Visual" />.</returns>
		// Token: 0x06002D55 RID: 11605 RVA: 0x000B58F0 File Offset: 0x000B4CF0
		public Point PointFromScreen(Point point)
		{
			this.VerifyAPIReadOnly();
			PresentationSource presentationSource = PresentationSource.FromVisual(this);
			if (presentationSource == null)
			{
				throw new InvalidOperationException(SR.Get("Visual_NoPresentationSource"));
			}
			point = PointUtil.ScreenToClient(point, presentationSource);
			point = PointUtil.ClientToRoot(point, presentationSource);
			GeneralTransform generalTransform = presentationSource.RootVisual.TransformToDescendant(this);
			if (generalTransform == null || !generalTransform.TryTransform(point, out point))
			{
				throw new InvalidOperationException(SR.Get("Visual_CannotTransformPoint"));
			}
			return point;
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06002D56 RID: 11606 RVA: 0x000B595C File Offset: 0x000B4D5C
		internal EventHandler ClipChangedHandler
		{
			get
			{
				return new EventHandler(this.ClipChanged);
			}
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x000B5978 File Offset: 0x000B4D78
		internal void ClipChanged(object sender, EventArgs e)
		{
			this.PropagateChangedFlags();
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06002D58 RID: 11608 RVA: 0x000B598C File Offset: 0x000B4D8C
		internal EventHandler ScrollableAreaClipChangedHandler
		{
			get
			{
				return new EventHandler(this.ScrollableAreaClipChanged);
			}
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x000B59A8 File Offset: 0x000B4DA8
		internal void ScrollableAreaClipChanged(object sender, EventArgs e)
		{
			this.PropagateChangedFlags();
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x000B59BC File Offset: 0x000B4DBC
		internal EventHandler TransformChangedHandler
		{
			get
			{
				return new EventHandler(this.TransformChanged);
			}
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x000B59D8 File Offset: 0x000B4DD8
		internal void TransformChanged(object sender, EventArgs e)
		{
			this.PropagateChangedFlags();
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x000B59EC File Offset: 0x000B4DEC
		internal EventHandler EffectChangedHandler
		{
			get
			{
				return new EventHandler(this.EffectChanged);
			}
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x000B5A08 File Offset: 0x000B4E08
		internal void EffectChanged(object sender, EventArgs e)
		{
			this.PropagateChangedFlags();
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06002D5E RID: 11614 RVA: 0x000B5A1C File Offset: 0x000B4E1C
		internal EventHandler CacheModeChangedHandler
		{
			get
			{
				return new EventHandler(this.EffectChanged);
			}
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x000B5A38 File Offset: 0x000B4E38
		internal void CacheModeChanged(object sender, EventArgs e)
		{
			this.PropagateChangedFlags();
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06002D60 RID: 11616 RVA: 0x000B5A4C File Offset: 0x000B4E4C
		internal EventHandler GuidelinesChangedHandler
		{
			get
			{
				return new EventHandler(this.GuidelinesChanged);
			}
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x000B5A68 File Offset: 0x000B4E68
		internal void GuidelinesChanged(object sender, EventArgs e)
		{
			this.SetFlagsOnAllChannels(true, VisualProxyFlags.IsGuidelineCollectionDirty);
			this.PropagateChangedFlags();
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06002D62 RID: 11618 RVA: 0x000B5A88 File Offset: 0x000B4E88
		internal EventHandler OpacityMaskChangedHandler
		{
			get
			{
				return new EventHandler(this.OpacityMaskChanged);
			}
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x000B5AA4 File Offset: 0x000B4EA4
		internal void OpacityMaskChanged(object sender, EventArgs e)
		{
			this.PropagateChangedFlags();
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06002D64 RID: 11620 RVA: 0x000B5AB8 File Offset: 0x000B4EB8
		internal EventHandler ContentsChangedHandler
		{
			get
			{
				return new EventHandler(this.ContentsChanged);
			}
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x000B5AD4 File Offset: 0x000B4ED4
		internal virtual void ContentsChanged(object sender, EventArgs e)
		{
			this.PropagateChangedFlags();
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000B5AE8 File Offset: 0x000B4EE8
		internal void SetFlagsOnAllChannels(bool value, VisualProxyFlags flagsToChange)
		{
			this._proxy.SetFlagsOnAllChannels(value, flagsToChange);
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x000B5B04 File Offset: 0x000B4F04
		internal void SetFlags(DUCE.Channel channel, bool value, VisualProxyFlags flagsToChange)
		{
			this._proxy.SetFlags(channel, value, flagsToChange);
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x000B5B20 File Offset: 0x000B4F20
		internal void SetFlags(bool value, VisualFlags flags)
		{
			this._flags = (value ? (this._flags | flags) : (this._flags & ~flags));
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000B5B4C File Offset: 0x000B4F4C
		internal void SetDpiScaleVisualFlags(DpiRecursiveChangeArgs args)
		{
			this._flags = (args.DpiScaleFlag1 ? (this._flags | VisualFlags.DpiScaleFlag1) : (this._flags & ~VisualFlags.DpiScaleFlag1));
			this._flags = (args.DpiScaleFlag2 ? (this._flags | VisualFlags.DpiScaleFlag2) : (this._flags & ~VisualFlags.DpiScaleFlag2));
			if (args.DpiScaleFlag1 && args.DpiScaleFlag2)
			{
				Visual.DpiIndex.SetValue(this, args.Index);
			}
			if (!args.OldDpiScale.Equals(args.NewDpiScale))
			{
				this.OnDpiChanged(args.OldDpiScale, args.NewDpiScale);
			}
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000B5C00 File Offset: 0x000B5000
		internal void RecursiveSetDpiScaleVisualFlags(DpiRecursiveChangeArgs args)
		{
			this.SetDpiScaleVisualFlags(args);
			int internalVisualChildrenCount = this.InternalVisualChildrenCount;
			for (int i = 0; i < internalVisualChildrenCount; i++)
			{
				Visual visual = this.InternalGetVisualChild(i);
				if (visual != null)
				{
					visual.RecursiveSetDpiScaleVisualFlags(args);
				}
			}
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x000B5C3C File Offset: 0x000B503C
		internal bool CheckFlagsOnAllChannels(VisualProxyFlags flagsToCheck)
		{
			return this._proxy.CheckFlagsOnAllChannels(flagsToCheck);
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000B5C58 File Offset: 0x000B5058
		internal bool CheckFlagsAnd(DUCE.Channel channel, VisualProxyFlags flagsToCheck)
		{
			return (this._proxy.GetFlags(channel) & flagsToCheck) == flagsToCheck;
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000B5C78 File Offset: 0x000B5078
		internal bool CheckFlagsAnd(VisualFlags flags)
		{
			return (this._flags & flags) == flags;
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x000B5C90 File Offset: 0x000B5090
		internal bool CheckFlagsOr(DUCE.Channel channel, VisualProxyFlags flagsToCheck)
		{
			return (this._proxy.GetFlags(channel) & flagsToCheck) > VisualProxyFlags.None;
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x000B5CB0 File Offset: 0x000B50B0
		internal bool CheckFlagsOr(VisualFlags flags)
		{
			return flags == VisualFlags.None || (this._flags & flags) > VisualFlags.None;
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x000B5CD0 File Offset: 0x000B50D0
		internal static void SetTreeBits(DependencyObject e, VisualFlags treeFlag, VisualFlags nodeFlag)
		{
			if (e != null)
			{
				Visual visual = e as Visual;
				if (visual != null)
				{
					visual.SetFlags(true, nodeFlag);
				}
				else
				{
					((Visual3D)e).SetFlags(true, nodeFlag);
				}
			}
			while (e != null)
			{
				Visual visual = e as Visual;
				if (visual != null)
				{
					if (visual.CheckFlagsAnd(treeFlag))
					{
						return;
					}
					visual.SetFlags(true, treeFlag);
				}
				else
				{
					Visual3D visual3D = e as Visual3D;
					if (visual3D.CheckFlagsAnd(treeFlag))
					{
						return;
					}
					visual3D.SetFlags(true, treeFlag);
				}
				e = VisualTreeHelper.GetParent(e);
			}
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x000B5D48 File Offset: 0x000B5148
		internal static void ClearTreeBits(DependencyObject e, VisualFlags treeFlag, VisualFlags nodeFlag)
		{
			if (e != null)
			{
				Visual visual = e as Visual;
				if (visual != null)
				{
					visual.SetFlags(false, nodeFlag);
				}
				else
				{
					((Visual3D)e).SetFlags(false, nodeFlag);
				}
			}
			while (e != null)
			{
				Visual visual = e as Visual;
				if (visual != null)
				{
					if (visual.CheckFlagsAnd(nodeFlag))
					{
						return;
					}
					if (Visual.DoAnyChildrenHaveABitSet(visual, treeFlag))
					{
						return;
					}
					visual.SetFlags(false, treeFlag);
				}
				else
				{
					Visual3D visual3D = e as Visual3D;
					if (visual3D.CheckFlagsAnd(nodeFlag))
					{
						return;
					}
					if (Visual3D.DoAnyChildrenHaveABitSet(visual3D, treeFlag))
					{
						return;
					}
					visual3D.SetFlags(false, treeFlag);
				}
				e = VisualTreeHelper.GetParent(e);
			}
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x000B5DD4 File Offset: 0x000B51D4
		private static bool DoAnyChildrenHaveABitSet(Visual pe, VisualFlags flag)
		{
			int visualChildrenCount = pe.VisualChildrenCount;
			for (int i = 0; i < visualChildrenCount; i++)
			{
				Visual visualChild = pe.GetVisualChild(i);
				if (visualChild != null && visualChild.CheckFlagsAnd(flag))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x000B5E0C File Offset: 0x000B520C
		internal static void PropagateFlags(Visual e, VisualFlags flags, VisualProxyFlags proxyFlags)
		{
			while (e != null && (!e.CheckFlagsAnd(flags) || !e.CheckFlagsOnAllChannels(proxyFlags)))
			{
				if (e.CheckFlagsOr(VisualFlags.ShouldPostRender))
				{
					MediaContext mediaContext = MediaContext.From(e.Dispatcher);
					if (mediaContext.Channel != null)
					{
						mediaContext.PostRender();
					}
				}
				else if (e.CheckFlagsAnd(VisualFlags.NodeIsCyclicBrushRoot))
				{
					Dictionary<ICyclicBrush, int> value = Visual.CyclicBrushToChannelsMapField.GetValue(e);
					foreach (ICyclicBrush cyclicBrush in value.Keys)
					{
						cyclicBrush.FireOnChanged();
					}
				}
				e.SetFlags(true, flags);
				e.SetFlagsOnAllChannels(true, proxyFlags);
				if (e._parent == null)
				{
					return;
				}
				Visual visual = e._parent as Visual;
				if (visual == null)
				{
					Visual3D.PropagateFlags((Visual3D)e._parent, flags, proxyFlags);
					return;
				}
				e = visual;
			}
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x000B5F08 File Offset: 0x000B5308
		internal void PropagateChangedFlags()
		{
			Visual.PropagateFlags(this, VisualFlags.IsSubtreeDirtyForPrecompute, VisualProxyFlags.IsSubtreeDirtyForRender);
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002D75 RID: 11637 RVA: 0x000B5F20 File Offset: 0x000B5320
		private bool NodeHasLegacyBitmapEffect
		{
			get
			{
				return this.CheckFlagsAnd(VisualFlags.NodeHasEffect) && Visual.BitmapEffectStateField.GetValue(this) != null;
			}
		}

		// Token: 0x0400146B RID: 5227
		private const VisualProxyFlags c_ProxyFlagsDirtyMask = VisualProxyFlags.IsSubtreeDirtyForRender | VisualProxyFlags.IsTransformDirty | VisualProxyFlags.IsClipDirty | VisualProxyFlags.IsContentDirty | VisualProxyFlags.IsOpacityDirty | VisualProxyFlags.IsOpacityMaskDirty | VisualProxyFlags.IsOffsetDirty | VisualProxyFlags.IsClearTypeHintDirty | VisualProxyFlags.IsGuidelineCollectionDirty | VisualProxyFlags.IsEdgeModeDirty | VisualProxyFlags.IsBitmapScalingModeDirty | VisualProxyFlags.IsEffectDirty | VisualProxyFlags.IsCacheModeDirty | VisualProxyFlags.IsScrollableAreaClipDirty | VisualProxyFlags.IsTextRenderingModeDirty | VisualProxyFlags.IsTextHintingModeDirty;

		// Token: 0x0400146C RID: 5228
		private const VisualProxyFlags c_Viewport3DProxyFlagsDirtyMask = VisualProxyFlags.Viewport3DVisual_IsCameraDirty | VisualProxyFlags.Viewport3DVisual_IsViewportDirty;

		// Token: 0x0400146D RID: 5229
		internal static readonly UncommonField<BitmapEffectState> BitmapEffectStateField = new UncommonField<BitmapEffectState>();

		// Token: 0x0400146E RID: 5230
		internal int _parentIndex;

		// Token: 0x0400146F RID: 5231
		internal DependencyObject _parent;

		// Token: 0x04001470 RID: 5232
		internal VisualProxy _proxy;

		// Token: 0x04001471 RID: 5233
		private Rect _bboxSubgraph = Rect.Empty;

		// Token: 0x04001472 RID: 5234
		private static readonly UncommonField<Dictionary<ICyclicBrush, int>> CyclicBrushToChannelsMapField = new UncommonField<Dictionary<ICyclicBrush, int>>();

		// Token: 0x04001473 RID: 5235
		private static readonly UncommonField<Dictionary<DUCE.Channel, int>> ChannelsToCyclicBrushMapField = new UncommonField<Dictionary<DUCE.Channel, int>>();

		// Token: 0x04001474 RID: 5236
		internal static readonly UncommonField<int> DpiIndex = new UncommonField<int>();

		// Token: 0x04001475 RID: 5237
		private static readonly UncommonField<Geometry> ClipField = new UncommonField<Geometry>();

		// Token: 0x04001476 RID: 5238
		private static readonly UncommonField<double> OpacityField = new UncommonField<double>(1.0);

		// Token: 0x04001477 RID: 5239
		private static readonly UncommonField<Brush> OpacityMaskField = new UncommonField<Brush>();

		// Token: 0x04001478 RID: 5240
		private static readonly UncommonField<EdgeMode> EdgeModeField = new UncommonField<EdgeMode>();

		// Token: 0x04001479 RID: 5241
		private static readonly UncommonField<BitmapScalingMode> BitmapScalingModeField = new UncommonField<BitmapScalingMode>();

		// Token: 0x0400147A RID: 5242
		private static readonly UncommonField<ClearTypeHint> ClearTypeHintField = new UncommonField<ClearTypeHint>();

		// Token: 0x0400147B RID: 5243
		private static readonly UncommonField<Transform> TransformField = new UncommonField<Transform>();

		// Token: 0x0400147C RID: 5244
		private static readonly UncommonField<Effect> EffectField = new UncommonField<Effect>();

		// Token: 0x0400147D RID: 5245
		private static readonly UncommonField<CacheMode> CacheModeField = new UncommonField<CacheMode>();

		// Token: 0x0400147E RID: 5246
		private static readonly UncommonField<DoubleCollection> GuidelinesXField = new UncommonField<DoubleCollection>();

		// Token: 0x0400147F RID: 5247
		private static readonly UncommonField<DoubleCollection> GuidelinesYField = new UncommonField<DoubleCollection>();

		// Token: 0x04001480 RID: 5248
		private static readonly UncommonField<Visual.AncestorChangedEventHandler> AncestorChangedEventField = new UncommonField<Visual.AncestorChangedEventHandler>();

		// Token: 0x04001481 RID: 5249
		private static readonly UncommonField<BitmapEffectState> UserProvidedBitmapEffectData = new UncommonField<BitmapEffectState>();

		// Token: 0x04001482 RID: 5250
		private static readonly UncommonField<Rect?> ScrollableAreaClipField = new UncommonField<Rect?>(null);

		// Token: 0x04001483 RID: 5251
		private static readonly UncommonField<TextRenderingMode> TextRenderingModeField = new UncommonField<TextRenderingMode>();

		// Token: 0x04001484 RID: 5252
		private static readonly UncommonField<TextHintingMode> TextHintingModeField = new UncommonField<TextHintingMode>();

		// Token: 0x04001485 RID: 5253
		private Vector _offset;

		// Token: 0x04001486 RID: 5254
		private VisualFlags _flags;

		// Token: 0x04001487 RID: 5255
		private const uint TreeLevelLimit = 2047U;

		// Token: 0x020008A3 RID: 2211
		internal class TopMostHitResult
		{
			// Token: 0x0600585A RID: 22618 RVA: 0x001676A0 File Offset: 0x00166AA0
			internal HitTestResultBehavior HitTestResult(HitTestResult result)
			{
				this._hitResult = result;
				return HitTestResultBehavior.Stop;
			}

			// Token: 0x0600585B RID: 22619 RVA: 0x001676B8 File Offset: 0x00166AB8
			internal HitTestFilterBehavior NoNested2DFilter(DependencyObject potentialHitTestTarget)
			{
				if (potentialHitTestTarget is Viewport2DVisual3D)
				{
					return HitTestFilterBehavior.ContinueSkipChildren;
				}
				return HitTestFilterBehavior.Continue;
			}

			// Token: 0x040028F4 RID: 10484
			internal HitTestResult _hitResult;
		}

		// Token: 0x020008A4 RID: 2212
		// (Invoke) Token: 0x0600585E RID: 22622
		internal delegate void AncestorChangedEventHandler(object sender, AncestorChangedEventArgs e);
	}
}
