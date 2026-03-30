using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using MS.Internal.KnownBoxes;

namespace System.Windows.Media
{
	/// <summary>Pinta uma área com uma <see cref="P:System.Windows.Media.VisualBrush.Visual" />.</summary>
	// Token: 0x020003FF RID: 1023
	public sealed class VisualBrush : TileBrush, ICyclicBrush
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.VisualBrush" />, fazendo cópias em profundidade dos valores do objeto.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true." /></returns>
		// Token: 0x060028D6 RID: 10454 RVA: 0x000A38DC File Offset: 0x000A2CDC
		public new VisualBrush Clone()
		{
			return (VisualBrush)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.VisualBrush" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060028D7 RID: 10455 RVA: 0x000A38F4 File Offset: 0x000A2CF4
		public new VisualBrush CloneCurrentValue()
		{
			return (VisualBrush)base.CloneCurrentValue();
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000A390C File Offset: 0x000A2D0C
		private static void VisualPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			VisualBrush visualBrush = (VisualBrush)d;
			Visual visual = (Visual)e.OldValue;
			if (visualBrush._pendingLayout)
			{
				UIElement uielement = (UIElement)visual;
				uielement.LayoutUpdated -= visualBrush.OnLayoutUpdated;
				bool flag = visualBrush._DispatcherLayoutResult.Abort();
				visualBrush._pendingLayout = false;
			}
			Visual visual2 = (Visual)e.NewValue;
			Dispatcher dispatcher = visualBrush.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource = visualBrush;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource.GetChannel(i);
						visualBrush.ReleaseResource(visual, channel);
						visualBrush.AddRefResource(visual2, channel);
					}
				}
			}
			visualBrush.PropertyChanged(VisualBrush.VisualProperty);
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000A39F0 File Offset: 0x000A2DF0
		private static void AutoLayoutContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			VisualBrush visualBrush = (VisualBrush)d;
			visualBrush.PropertyChanged(VisualBrush.AutoLayoutContentProperty);
		}

		/// <summary>Obtém ou define o conteúdo do pincel.</summary>
		/// <returns>O conteúdo do pincel. O padrão é <see langword="null" />.</returns>
		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x060028DA RID: 10458 RVA: 0x000A3A10 File Offset: 0x000A2E10
		// (set) Token: 0x060028DB RID: 10459 RVA: 0x000A3A30 File Offset: 0x000A2E30
		public Visual Visual
		{
			get
			{
				return (Visual)base.GetValue(VisualBrush.VisualProperty);
			}
			set
			{
				base.SetValueInternal(VisualBrush.VisualProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que especifica se este <see cref="T:System.Windows.Media.VisualBrush" /> executará o layout de seu <see cref="P:System.Windows.Media.VisualBrush.Visual" />.</summary>
		/// <returns>
		///   <see langword="true" /> Se esse pincel deve executar o layout no seu <see cref="P:System.Windows.Media.VisualBrush.Visual" />; caso contrário, <see langword="false" />. O padrão é <see langword="true" />.</returns>
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x060028DC RID: 10460 RVA: 0x000A3A4C File Offset: 0x000A2E4C
		// (set) Token: 0x060028DD RID: 10461 RVA: 0x000A3A6C File Offset: 0x000A2E6C
		public bool AutoLayoutContent
		{
			get
			{
				return (bool)base.GetValue(VisualBrush.AutoLayoutContentProperty);
			}
			set
			{
				base.SetValueInternal(VisualBrush.AutoLayoutContentProperty, BooleanBoxes.Box(value));
			}
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x000A3A8C File Offset: 0x000A2E8C
		protected override Freezable CreateInstanceCore()
		{
			return new VisualBrush();
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x000A3AA0 File Offset: 0x000A2EA0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Transform transform = base.Transform;
				Transform relativeTransform = base.RelativeTransform;
				Visual visual = this.Visual;
				DUCE.ResourceHandle hTransform;
				if (transform == null || transform == Transform.Identity)
				{
					hTransform = DUCE.ResourceHandle.Null;
				}
				else
				{
					hTransform = ((DUCE.IResource)transform).GetHandle(channel);
				}
				DUCE.ResourceHandle hRelativeTransform;
				if (relativeTransform == null || relativeTransform == Transform.Identity)
				{
					hRelativeTransform = DUCE.ResourceHandle.Null;
				}
				else
				{
					hRelativeTransform = ((DUCE.IResource)relativeTransform).GetHandle(channel);
				}
				DUCE.ResourceHandle hVisual = (visual != null) ? ((DUCE.IResource)visual).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(Brush.OpacityProperty, channel);
				DUCE.ResourceHandle animationResourceHandle2 = base.GetAnimationResourceHandle(TileBrush.ViewportProperty, channel);
				DUCE.ResourceHandle animationResourceHandle3 = base.GetAnimationResourceHandle(TileBrush.ViewboxProperty, channel);
				DUCE.MILCMD_VISUALBRUSH milcmd_VISUALBRUSH;
				milcmd_VISUALBRUSH.Type = MILCMD.MilCmdVisualBrush;
				milcmd_VISUALBRUSH.Handle = this._duceResource.GetHandle(channel);
				if (animationResourceHandle.IsNull)
				{
					milcmd_VISUALBRUSH.Opacity = base.Opacity;
				}
				milcmd_VISUALBRUSH.hOpacityAnimations = animationResourceHandle;
				milcmd_VISUALBRUSH.hTransform = hTransform;
				milcmd_VISUALBRUSH.hRelativeTransform = hRelativeTransform;
				milcmd_VISUALBRUSH.ViewportUnits = base.ViewportUnits;
				milcmd_VISUALBRUSH.ViewboxUnits = base.ViewboxUnits;
				if (animationResourceHandle2.IsNull)
				{
					milcmd_VISUALBRUSH.Viewport = base.Viewport;
				}
				milcmd_VISUALBRUSH.hViewportAnimations = animationResourceHandle2;
				if (animationResourceHandle3.IsNull)
				{
					milcmd_VISUALBRUSH.Viewbox = base.Viewbox;
				}
				milcmd_VISUALBRUSH.hViewboxAnimations = animationResourceHandle3;
				milcmd_VISUALBRUSH.Stretch = base.Stretch;
				milcmd_VISUALBRUSH.TileMode = base.TileMode;
				milcmd_VISUALBRUSH.AlignmentX = base.AlignmentX;
				milcmd_VISUALBRUSH.AlignmentY = base.AlignmentY;
				milcmd_VISUALBRUSH.CachingHint = (CachingHint)base.GetValue(RenderOptions.CachingHintProperty);
				milcmd_VISUALBRUSH.CacheInvalidationThresholdMinimum = (double)base.GetValue(RenderOptions.CacheInvalidationThresholdMinimumProperty);
				milcmd_VISUALBRUSH.CacheInvalidationThresholdMaximum = (double)base.GetValue(RenderOptions.CacheInvalidationThresholdMaximumProperty);
				milcmd_VISUALBRUSH.hVisual = hVisual;
				channel.SendCommand((byte*)(&milcmd_VISUALBRUSH), sizeof(DUCE.MILCMD_VISUALBRUSH));
			}
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x000A3C8C File Offset: 0x000A308C
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_VISUALBRUSH))
			{
				Transform transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).AddRefOnChannel(channel);
				}
				Transform relativeTransform = base.RelativeTransform;
				if (relativeTransform != null)
				{
					((DUCE.IResource)relativeTransform).AddRefOnChannel(channel);
				}
				Visual visual = this.Visual;
				if (visual != null)
				{
					visual.AddRefOnChannelForCyclicBrush(this, channel);
				}
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x000A3CFC File Offset: 0x000A30FC
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				Transform transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).ReleaseOnChannel(channel);
				}
				Transform relativeTransform = base.RelativeTransform;
				if (relativeTransform != null)
				{
					((DUCE.IResource)relativeTransform).ReleaseOnChannel(channel);
				}
				Visual visual = this.Visual;
				if (visual != null)
				{
					visual.ReleaseOnChannelForCyclicBrush(this, channel);
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x000A3D54 File Offset: 0x000A3154
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x000A3D70 File Offset: 0x000A3170
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000A3D88 File Offset: 0x000A3188
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x060028E5 RID: 10469 RVA: 0x000A3DA4 File Offset: 0x000A31A4
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x000A3DB4 File Offset: 0x000A31B4
		static VisualBrush()
		{
			Type typeFromHandle = typeof(VisualBrush);
			VisualBrush.VisualProperty = Animatable.RegisterProperty("Visual", typeof(Visual), typeFromHandle, null, new PropertyChangedCallback(VisualBrush.VisualPropertyChanged), null, false, null);
			VisualBrush.AutoLayoutContentProperty = Animatable.RegisterProperty("AutoLayoutContent", typeof(bool), typeFromHandle, true, new PropertyChangedCallback(VisualBrush.AutoLayoutContentPropertyChanged), null, false, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.VisualBrush" />.</summary>
		// Token: 0x060028E7 RID: 10471 RVA: 0x000A3E28 File Offset: 0x000A3228
		public VisualBrush()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.VisualBrush" /> que contém o <see cref="P:System.Windows.Media.VisualBrush.Visual" /> especificado.</summary>
		/// <param name="visual">O conteúdo do novo <see cref="T:System.Windows.Media.VisualBrush" />.</param>
		// Token: 0x060028E8 RID: 10472 RVA: 0x000A3E50 File Offset: 0x000A3250
		public VisualBrush(Visual visual)
		{
			if (base.Dispatcher != null)
			{
				MediaSystem.AssertSameContext(this, visual);
				this.Visual = visual;
			}
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000A3E8C File Offset: 0x000A328C
		void ICyclicBrush.FireOnChanged()
		{
			bool flag = this.Enter();
			if (flag)
			{
				try
				{
					this._isCacheDirty = true;
					base.FireChanged();
					this.RegisterForAsyncRenderForCyclicBrush();
				}
				finally
				{
					this.Exit();
				}
			}
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x000A3EDC File Offset: 0x000A32DC
		private void RegisterForAsyncRenderForCyclicBrush()
		{
			if (this != null && base.Dispatcher != null && !this._isAsyncRenderRegistered)
			{
				MediaContext mediaContext = MediaContext.From(base.Dispatcher);
				if (!((DUCE.IResource)this).GetHandle(mediaContext.Channel).IsNull)
				{
					mediaContext.ResourcesUpdated += this.RenderForCyclicBrush;
					this._isAsyncRenderRegistered = true;
				}
			}
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x000A3F3C File Offset: 0x000A333C
		void ICyclicBrush.RenderForCyclicBrush(DUCE.Channel channel, bool skipChannelCheck)
		{
			Visual visual = this.Visual;
			if (visual != null && visual.CheckFlagsAnd(VisualFlags.NodeIsCyclicBrushRoot))
			{
				visual.Precompute();
				RenderContext renderContext = new RenderContext();
				renderContext.Initialize(channel, DUCE.ResourceHandle.Null);
				if (channel.IsConnected)
				{
					visual.Render(renderContext, 0U);
				}
				else
				{
					((DUCE.IResource)visual).ReleaseOnChannel(channel);
				}
			}
			this._isAsyncRenderRegistered = false;
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x000A3F98 File Offset: 0x000A3398
		internal void AddRefResource(Visual visual, DUCE.Channel channel)
		{
			if (visual != null)
			{
				visual.AddRefOnChannelForCyclicBrush(this, channel);
			}
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x000A3FB0 File Offset: 0x000A33B0
		internal void ReleaseResource(Visual visual, DUCE.Channel channel)
		{
			if (visual != null)
			{
				visual.ReleaseOnChannelForCyclicBrush(this, channel);
			}
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x000A3FC8 File Offset: 0x000A33C8
		protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(e);
			if ((e.IsAValueChange || e.IsASubPropertyChange) && (e.Property == VisualBrush.VisualProperty || e.Property == VisualBrush.AutoLayoutContentProperty) && this.AutoLayoutContent)
			{
				UIElement uielement = this.Visual as UIElement;
				if (uielement != null && ((VisualTreeHelper.GetParent(uielement) == null && !uielement.IsRootElement) || VisualTreeHelper.GetParent(uielement) is Visual3D))
				{
					uielement.LayoutUpdated += this.OnLayoutUpdated;
					this._DispatcherLayoutResult = base.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new DispatcherOperationCallback(this.LayoutCallback), uielement);
					this._pendingLayout = true;
				}
			}
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x000A407C File Offset: 0x000A347C
		private void DoLayout(UIElement element)
		{
			DependencyObject parent = VisualTreeHelper.GetParent(element);
			if (!element.IsRootElement && (parent == null || parent is Visual3D))
			{
				UIElement.PropagateResumeLayout(null, element);
				element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
				element.Arrange(new Rect(element.DesiredSize));
			}
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x000A40D8 File Offset: 0x000A34D8
		private void OnLayoutUpdated(object sender, EventArgs args)
		{
			UIElement uielement = (UIElement)this.Visual;
			uielement.LayoutUpdated -= this.OnLayoutUpdated;
			this._pendingLayout = false;
			bool flag = this._DispatcherLayoutResult.Abort();
			this.DoLayout(uielement);
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x000A4120 File Offset: 0x000A3520
		private object LayoutCallback(object arg)
		{
			UIElement uielement = arg as UIElement;
			uielement.LayoutUpdated -= this.OnLayoutUpdated;
			this._pendingLayout = false;
			this.DoLayout(uielement);
			return null;
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x000A4158 File Offset: 0x000A3558
		internal bool Enter()
		{
			if (this._reentrancyFlag)
			{
				return false;
			}
			this._reentrancyFlag = true;
			return true;
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x000A4178 File Offset: 0x000A3578
		internal void Exit()
		{
			this._reentrancyFlag = false;
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x000A418C File Offset: 0x000A358C
		protected override void GetContentBounds(out Rect contentBounds)
		{
			if (this._isCacheDirty)
			{
				this._bbox = this.Visual.CalculateSubgraphBoundsOuterSpace();
				this._isCacheDirty = false;
			}
			contentBounds = this._bbox;
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.VisualBrush.Visual" />.</summary>
		// Token: 0x040012A3 RID: 4771
		public static readonly DependencyProperty VisualProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.VisualBrush.AutoLayoutContent" />.</summary>
		// Token: 0x040012A4 RID: 4772
		public static readonly DependencyProperty AutoLayoutContentProperty;

		// Token: 0x040012A5 RID: 4773
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x040012A6 RID: 4774
		internal const bool c_AutoLayoutContent = true;

		// Token: 0x040012A7 RID: 4775
		private DispatcherOperation _DispatcherLayoutResult;

		// Token: 0x040012A8 RID: 4776
		private bool _pendingLayout;

		// Token: 0x040012A9 RID: 4777
		private bool _reentrancyFlag;

		// Token: 0x040012AA RID: 4778
		private bool _isAsyncRenderRegistered;

		// Token: 0x040012AB RID: 4779
		private bool _isCacheDirty = true;

		// Token: 0x040012AC RID: 4780
		private Rect _bbox = Rect.Empty;
	}
}
