using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Pinta uma área com conteúdo armazenado em cache.</summary>
	// Token: 0x02000366 RID: 870
	public sealed class BitmapCacheBrush : Brush, ICyclicBrush
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.BitmapCacheBrush" />.</summary>
		// Token: 0x06001DBC RID: 7612 RVA: 0x000798C0 File Offset: 0x00078CC0
		public BitmapCacheBrush()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.BitmapCacheBrush" /> com o <see cref="T:System.Windows.Media.Visual" /> especificado.</summary>
		/// <param name="visual">Um <see cref="T:System.Windows.Media.Visual" /> a ser armazenado em cache e usado como o <see cref="P:System.Windows.Media.BitmapCacheBrush.Target" />.</param>
		// Token: 0x06001DBD RID: 7613 RVA: 0x000798D4 File Offset: 0x00078CD4
		public BitmapCacheBrush(Visual visual)
		{
			if (base.Dispatcher != null)
			{
				MediaSystem.AssertSameContext(this, visual);
				this.Target = visual;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001DBE RID: 7614 RVA: 0x00079900 File Offset: 0x00078D00
		private ContainerVisual AutoWrapVisual
		{
			get
			{
				if (this._dummyVisual == null)
				{
					this._dummyVisual = new ContainerVisual();
				}
				return this._dummyVisual;
			}
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x00079928 File Offset: 0x00078D28
		void ICyclicBrush.FireOnChanged()
		{
			bool flag = this.Enter();
			if (flag)
			{
				try
				{
					base.FireChanged();
					this.RegisterForAsyncRenderForCyclicBrush();
				}
				finally
				{
					this.Exit();
				}
			}
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x00079970 File Offset: 0x00078D70
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

		// Token: 0x06001DC1 RID: 7617 RVA: 0x000799D0 File Offset: 0x00078DD0
		void ICyclicBrush.RenderForCyclicBrush(DUCE.Channel channel, bool skipChannelCheck)
		{
			Visual internalTarget = this.InternalTarget;
			if (internalTarget != null && internalTarget.CheckFlagsAnd(VisualFlags.NodeIsCyclicBrushRoot))
			{
				internalTarget.Precompute();
				RenderContext renderContext = new RenderContext();
				renderContext.Initialize(channel, DUCE.ResourceHandle.Null);
				if (channel.IsConnected)
				{
					internalTarget.Render(renderContext, 0U);
				}
				else
				{
					((DUCE.IResource)internalTarget).ReleaseOnChannel(channel);
				}
			}
			this._isAsyncRenderRegistered = false;
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x00079A2C File Offset: 0x00078E2C
		internal void AddRefResource(Visual visual, DUCE.Channel channel)
		{
			if (visual != null)
			{
				visual.AddRefOnChannelForCyclicBrush(this, channel);
			}
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x00079A44 File Offset: 0x00078E44
		internal void ReleaseResource(Visual visual, DUCE.Channel channel)
		{
			if (visual != null)
			{
				visual.ReleaseOnChannelForCyclicBrush(this, channel);
			}
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x00079A5C File Offset: 0x00078E5C
		protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(e);
			if (e.IsAValueChange || e.IsASubPropertyChange)
			{
				if (e.Property == BitmapCacheBrush.TargetProperty || e.Property == BitmapCacheBrush.AutoLayoutContentProperty)
				{
					if (e.Property == BitmapCacheBrush.TargetProperty && e.IsAValueChange)
					{
						if (this.AutoWrapTarget)
						{
							this.AutoWrapVisual.Children.Remove((Visual)e.OldValue);
							this.AutoWrapVisual.Children.Add((Visual)e.NewValue);
						}
						else
						{
							this.InternalTarget = this.Target;
						}
					}
					if (this.AutoLayoutContent)
					{
						UIElement uielement = this.Target as UIElement;
						if (uielement != null && ((VisualTreeHelper.GetParent(uielement) == null && !uielement.IsRootElement) || VisualTreeHelper.GetParent(uielement) is Visual3D || VisualTreeHelper.GetParent(uielement) == this.InternalTarget))
						{
							uielement.LayoutUpdated += this.OnLayoutUpdated;
							this._DispatcherLayoutResult = base.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new DispatcherOperationCallback(this.LayoutCallback), uielement);
							this._pendingLayout = true;
							return;
						}
					}
				}
				else if (e.Property == BitmapCacheBrush.AutoWrapTargetProperty)
				{
					if (this.AutoWrapTarget)
					{
						this.InternalTarget = this.AutoWrapVisual;
						this.AutoWrapVisual.Children.Add(this.Target);
						return;
					}
					this.AutoWrapVisual.Children.Remove(this.Target);
					this.InternalTarget = this.Target;
				}
			}
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x00079BEC File Offset: 0x00078FEC
		private void DoLayout(UIElement element)
		{
			DependencyObject parent = VisualTreeHelper.GetParent(element);
			if (!element.IsRootElement && (parent == null || parent is Visual3D || parent == this.InternalTarget))
			{
				UIElement.PropagateResumeLayout(null, element);
				element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
				element.Arrange(new Rect(element.DesiredSize));
			}
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x00079C54 File Offset: 0x00079054
		private void OnLayoutUpdated(object sender, EventArgs args)
		{
			UIElement uielement = (UIElement)this.Target;
			uielement.LayoutUpdated -= this.OnLayoutUpdated;
			this._pendingLayout = false;
			bool flag = this._DispatcherLayoutResult.Abort();
			this.DoLayout(uielement);
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x00079C9C File Offset: 0x0007909C
		private object LayoutCallback(object arg)
		{
			UIElement uielement = arg as UIElement;
			uielement.LayoutUpdated -= this.OnLayoutUpdated;
			this._pendingLayout = false;
			this.DoLayout(uielement);
			return null;
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x00079CD4 File Offset: 0x000790D4
		internal bool Enter()
		{
			if (this._reentrancyFlag)
			{
				return false;
			}
			this._reentrancyFlag = true;
			return true;
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x00079CF4 File Offset: 0x000790F4
		internal void Exit()
		{
			this._reentrancyFlag = false;
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x00079D08 File Offset: 0x00079108
		private static object CoerceOpacity(DependencyObject d, object value)
		{
			if ((double)value != (double)Brush.OpacityProperty.GetDefaultValue(typeof(BitmapCacheBrush)))
			{
				throw new InvalidOperationException(SR.Get("BitmapCacheBrush_OpacityChanged"));
			}
			return 1.0;
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x00079D54 File Offset: 0x00079154
		private static object CoerceTransform(DependencyObject d, object value)
		{
			if ((Transform)value != (Transform)Brush.TransformProperty.GetDefaultValue(typeof(BitmapCacheBrush)))
			{
				throw new InvalidOperationException(SR.Get("BitmapCacheBrush_TransformChanged"));
			}
			return null;
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x00079D94 File Offset: 0x00079194
		private static object CoerceRelativeTransform(DependencyObject d, object value)
		{
			if ((Transform)value != (Transform)Brush.RelativeTransformProperty.GetDefaultValue(typeof(BitmapCacheBrush)))
			{
				throw new InvalidOperationException(SR.Get("BitmapCacheBrush_RelativeTransformChanged"));
			}
			return null;
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x00079DD4 File Offset: 0x000791D4
		private static void StaticInitialize(Type typeofThis)
		{
			Brush.OpacityProperty.OverrideMetadata(typeofThis, new IndependentlyAnimatedPropertyMetadata(1.0, null, new CoerceValueCallback(BitmapCacheBrush.CoerceOpacity)));
			Brush.TransformProperty.OverrideMetadata(typeofThis, new UIPropertyMetadata(null, null, new CoerceValueCallback(BitmapCacheBrush.CoerceTransform)));
			Brush.RelativeTransformProperty.OverrideMetadata(typeofThis, new UIPropertyMetadata(null, null, new CoerceValueCallback(BitmapCacheBrush.CoerceRelativeTransform)));
		}

		/// <summary>Cria um clone modificável do <see cref="T:System.Windows.Media.BitmapCacheBrush" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência do objeto, esse método copia as expressões (que talvez não possam mais ser resolvidas), mas não as animações nem seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true." /></returns>
		// Token: 0x06001DCE RID: 7630 RVA: 0x00079E48 File Offset: 0x00079248
		public new BitmapCacheBrush Clone()
		{
			return (BitmapCacheBrush)base.Clone();
		}

		/// <summary>Cria um clone modificável (cópia profunda) do <see cref="T:System.Windows.Media.BitmapCacheBrush" /> usando seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true." /></returns>
		// Token: 0x06001DCF RID: 7631 RVA: 0x00079E60 File Offset: 0x00079260
		public new BitmapCacheBrush CloneCurrentValue()
		{
			return (BitmapCacheBrush)base.CloneCurrentValue();
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x00079E78 File Offset: 0x00079278
		private static void TargetPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BitmapCacheBrush bitmapCacheBrush = (BitmapCacheBrush)d;
			bitmapCacheBrush.PropertyChanged(BitmapCacheBrush.TargetProperty);
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x00079E98 File Offset: 0x00079298
		private static void BitmapCachePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			BitmapCacheBrush bitmapCacheBrush = (BitmapCacheBrush)d;
			BitmapCache resource = (BitmapCache)e.OldValue;
			BitmapCache resource2 = (BitmapCache)e.NewValue;
			Dispatcher dispatcher = bitmapCacheBrush.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = bitmapCacheBrush;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						bitmapCacheBrush.ReleaseResource(resource, channel);
						bitmapCacheBrush.AddRefResource(resource2, channel);
					}
				}
			}
			bitmapCacheBrush.PropertyChanged(BitmapCacheBrush.BitmapCacheProperty);
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x00079F60 File Offset: 0x00079360
		private static void AutoLayoutContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BitmapCacheBrush bitmapCacheBrush = (BitmapCacheBrush)d;
			bitmapCacheBrush.PropertyChanged(BitmapCacheBrush.AutoLayoutContentProperty);
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x00079F80 File Offset: 0x00079380
		private static void InternalTargetPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BitmapCacheBrush bitmapCacheBrush = (BitmapCacheBrush)d;
			Visual visual = (Visual)e.OldValue;
			if (bitmapCacheBrush._pendingLayout)
			{
				UIElement uielement = (UIElement)visual;
				uielement.LayoutUpdated -= bitmapCacheBrush.OnLayoutUpdated;
				bool flag = bitmapCacheBrush._DispatcherLayoutResult.Abort();
				bitmapCacheBrush._pendingLayout = false;
			}
			Visual visual2 = (Visual)e.NewValue;
			Dispatcher dispatcher = bitmapCacheBrush.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource = bitmapCacheBrush;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource.GetChannel(i);
						bitmapCacheBrush.ReleaseResource(visual, channel);
						bitmapCacheBrush.AddRefResource(visual2, channel);
					}
				}
			}
			bitmapCacheBrush.PropertyChanged(BitmapCacheBrush.InternalTargetProperty);
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x0007A064 File Offset: 0x00079464
		private static void AutoWrapTargetPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BitmapCacheBrush bitmapCacheBrush = (BitmapCacheBrush)d;
			bitmapCacheBrush.PropertyChanged(BitmapCacheBrush.AutoWrapTargetProperty);
		}

		/// <summary>Obtém ou define o visual de destino a ser armazenado em cache.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Visual" /> em cache e pintar com.</returns>
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001DD5 RID: 7637 RVA: 0x0007A084 File Offset: 0x00079484
		// (set) Token: 0x06001DD6 RID: 7638 RVA: 0x0007A0A4 File Offset: 0x000794A4
		public Visual Target
		{
			get
			{
				return (Visual)base.GetValue(BitmapCacheBrush.TargetProperty);
			}
			set
			{
				base.SetValueInternal(BitmapCacheBrush.TargetProperty, value);
			}
		}

		/// <summary>Obtém ou define um <see cref="T:System.Windows.Media.CacheMode" /> que representa o conteúdo armazenado em cache.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.BitmapCache" /> que representa o conteúdo armazenado em cache.</returns>
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x0007A0C0 File Offset: 0x000794C0
		// (set) Token: 0x06001DD8 RID: 7640 RVA: 0x0007A0E0 File Offset: 0x000794E0
		public BitmapCache BitmapCache
		{
			get
			{
				return (BitmapCache)base.GetValue(BitmapCacheBrush.BitmapCacheProperty);
			}
			set
			{
				base.SetValueInternal(BitmapCacheBrush.BitmapCacheProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o layout é aplicado ao conteúdo deste pincel.</summary>
		/// <returns>
		///   <see langword="true" /> Se o layout é aplicado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x0007A0FC File Offset: 0x000794FC
		// (set) Token: 0x06001DDA RID: 7642 RVA: 0x0007A11C File Offset: 0x0007951C
		public bool AutoLayoutContent
		{
			get
			{
				return (bool)base.GetValue(BitmapCacheBrush.AutoLayoutContentProperty);
			}
			set
			{
				base.SetValueInternal(BitmapCacheBrush.AutoLayoutContentProperty, BooleanBoxes.Box(value));
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x0007A13C File Offset: 0x0007953C
		// (set) Token: 0x06001DDC RID: 7644 RVA: 0x0007A15C File Offset: 0x0007955C
		internal Visual InternalTarget
		{
			get
			{
				return (Visual)base.GetValue(BitmapCacheBrush.InternalTargetProperty);
			}
			set
			{
				base.SetValueInternal(BitmapCacheBrush.InternalTargetProperty, value);
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x0007A178 File Offset: 0x00079578
		// (set) Token: 0x06001DDE RID: 7646 RVA: 0x0007A198 File Offset: 0x00079598
		internal bool AutoWrapTarget
		{
			get
			{
				return (bool)base.GetValue(BitmapCacheBrush.AutoWrapTargetProperty);
			}
			set
			{
				base.SetValueInternal(BitmapCacheBrush.AutoWrapTargetProperty, BooleanBoxes.Box(value));
			}
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x0007A1B8 File Offset: 0x000795B8
		protected override Freezable CreateInstanceCore()
		{
			return new BitmapCacheBrush();
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x0007A1CC File Offset: 0x000795CC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Transform transform = base.Transform;
				Transform relativeTransform = base.RelativeTransform;
				BitmapCache bitmapCache = this.BitmapCache;
				Visual internalTarget = this.InternalTarget;
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
				DUCE.ResourceHandle hBitmapCache = (bitmapCache != null) ? ((DUCE.IResource)bitmapCache).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.ResourceHandle hInternalTarget = (internalTarget != null) ? ((DUCE.IResource)internalTarget).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(Brush.OpacityProperty, channel);
				DUCE.MILCMD_BITMAPCACHEBRUSH milcmd_BITMAPCACHEBRUSH;
				milcmd_BITMAPCACHEBRUSH.Type = MILCMD.MilCmdBitmapCacheBrush;
				milcmd_BITMAPCACHEBRUSH.Handle = this._duceResource.GetHandle(channel);
				if (animationResourceHandle.IsNull)
				{
					milcmd_BITMAPCACHEBRUSH.Opacity = base.Opacity;
				}
				milcmd_BITMAPCACHEBRUSH.hOpacityAnimations = animationResourceHandle;
				milcmd_BITMAPCACHEBRUSH.hTransform = hTransform;
				milcmd_BITMAPCACHEBRUSH.hRelativeTransform = hRelativeTransform;
				milcmd_BITMAPCACHEBRUSH.hBitmapCache = hBitmapCache;
				milcmd_BITMAPCACHEBRUSH.hInternalTarget = hInternalTarget;
				channel.SendCommand((byte*)(&milcmd_BITMAPCACHEBRUSH), sizeof(DUCE.MILCMD_BITMAPCACHEBRUSH));
			}
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x0007A2F0 File Offset: 0x000796F0
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_BITMAPCACHEBRUSH))
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
				BitmapCache bitmapCache = this.BitmapCache;
				if (bitmapCache != null)
				{
					((DUCE.IResource)bitmapCache).AddRefOnChannel(channel);
				}
				Visual internalTarget = this.InternalTarget;
				if (internalTarget != null)
				{
					internalTarget.AddRefOnChannelForCyclicBrush(this, channel);
				}
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x0007A374 File Offset: 0x00079774
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
				BitmapCache bitmapCache = this.BitmapCache;
				if (bitmapCache != null)
				{
					((DUCE.IResource)bitmapCache).ReleaseOnChannel(channel);
				}
				Visual internalTarget = this.InternalTarget;
				if (internalTarget != null)
				{
					internalTarget.ReleaseOnChannelForCyclicBrush(this, channel);
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x0007A3DC File Offset: 0x000797DC
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x0007A3F8 File Offset: 0x000797F8
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x0007A410 File Offset: 0x00079810
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x0007A42C File Offset: 0x0007982C
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x0007A43C File Offset: 0x0007983C
		static BitmapCacheBrush()
		{
			Type typeFromHandle = typeof(BitmapCacheBrush);
			BitmapCacheBrush.StaticInitialize(typeFromHandle);
			BitmapCacheBrush.TargetProperty = Animatable.RegisterProperty("Target", typeof(Visual), typeFromHandle, null, new PropertyChangedCallback(BitmapCacheBrush.TargetPropertyChanged), null, false, null);
			BitmapCacheBrush.BitmapCacheProperty = Animatable.RegisterProperty("BitmapCache", typeof(BitmapCache), typeFromHandle, null, new PropertyChangedCallback(BitmapCacheBrush.BitmapCachePropertyChanged), null, false, null);
			BitmapCacheBrush.AutoLayoutContentProperty = Animatable.RegisterProperty("AutoLayoutContent", typeof(bool), typeFromHandle, true, new PropertyChangedCallback(BitmapCacheBrush.AutoLayoutContentPropertyChanged), null, false, null);
			BitmapCacheBrush.InternalTargetProperty = Animatable.RegisterProperty("InternalTarget", typeof(Visual), typeFromHandle, null, new PropertyChangedCallback(BitmapCacheBrush.InternalTargetPropertyChanged), null, false, null);
			BitmapCacheBrush.AutoWrapTargetProperty = Animatable.RegisterProperty("AutoWrapTarget", typeof(bool), typeFromHandle, false, new PropertyChangedCallback(BitmapCacheBrush.AutoWrapTargetPropertyChanged), null, false, null);
		}

		// Token: 0x04000FF2 RID: 4082
		private ContainerVisual _dummyVisual;

		// Token: 0x04000FF3 RID: 4083
		private DispatcherOperation _DispatcherLayoutResult;

		// Token: 0x04000FF4 RID: 4084
		private bool _pendingLayout;

		// Token: 0x04000FF5 RID: 4085
		private bool _reentrancyFlag;

		// Token: 0x04000FF6 RID: 4086
		private bool _isAsyncRenderRegistered;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.BitmapCacheBrush.Target" />.</summary>
		// Token: 0x04000FF7 RID: 4087
		public static readonly DependencyProperty TargetProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.BitmapCacheBrush.BitmapCache" />.</summary>
		// Token: 0x04000FF8 RID: 4088
		public static readonly DependencyProperty BitmapCacheProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.BitmapCacheBrush.AutoLayoutContent" />.</summary>
		// Token: 0x04000FF9 RID: 4089
		public static readonly DependencyProperty AutoLayoutContentProperty;

		// Token: 0x04000FFA RID: 4090
		internal static readonly DependencyProperty InternalTargetProperty;

		// Token: 0x04000FFB RID: 4091
		internal static readonly DependencyProperty AutoWrapTargetProperty;

		// Token: 0x04000FFC RID: 4092
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x04000FFD RID: 4093
		internal const bool c_AutoLayoutContent = true;

		// Token: 0x04000FFE RID: 4094
		internal const bool c_AutoWrapTarget = false;
	}
}
