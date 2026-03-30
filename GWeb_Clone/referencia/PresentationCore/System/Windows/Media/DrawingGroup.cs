using System;
using System.Security;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Media.Effects;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Representa uma coleção de desenhos que pode ser operada como um único desenho.</summary>
	// Token: 0x02000385 RID: 901
	[ContentProperty("Children")]
	public sealed class DrawingGroup : Drawing
	{
		/// <summary>Abre o <see cref="T:System.Windows.Media.DrawingGroup" /> para popular seu <see cref="P:System.Windows.Media.DrawingGroup.Children" /> e limpa todos os <see cref="P:System.Windows.Media.DrawingGroup.Children" /> existentes.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.DrawingContext" /> que pode ser usado para descrever o conteúdo desse objeto <see cref="T:System.Windows.Media.DrawingGroup" />.</returns>
		// Token: 0x06002141 RID: 8513 RVA: 0x000864EC File Offset: 0x000858EC
		public DrawingContext Open()
		{
			this.VerifyOpen();
			this._openedForAppend = false;
			return new DrawingGroupDrawingContext(this);
		}

		/// <summary>Abre o <see cref="T:System.Windows.Media.DrawingGroup" /> para popular seu <see cref="P:System.Windows.Media.DrawingGroup.Children" />. Esse método permite que você acrescente <see cref="P:System.Windows.Media.DrawingGroup.Children" /> adicionais a este <see cref="T:System.Windows.Media.DrawingGroup" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.DrawingContext" /> que você pode usar para descrever o conteúdo desse objeto <see cref="T:System.Windows.Media.DrawingGroup" />.</returns>
		// Token: 0x06002142 RID: 8514 RVA: 0x0008650C File Offset: 0x0008590C
		public DrawingContext Append()
		{
			this.VerifyOpen();
			this._openedForAppend = true;
			return new DrawingGroupDrawingContext(this);
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x0008652C File Offset: 0x0008592C
		internal void Close(DrawingCollection rootDrawingGroupChildren)
		{
			base.WritePreamble();
			if (!this._openedForAppend)
			{
				this.Children = rootDrawingGroupChildren;
			}
			else
			{
				DrawingCollection children = this.Children;
				if (children == null)
				{
					throw new InvalidOperationException(SR.Get("DrawingGroup_CannotAppendToNullCollection"));
				}
				if (children.IsFrozen)
				{
					throw new InvalidOperationException(SR.Get("DrawingGroup_CannotAppendToFrozenCollection"));
				}
				children.TransactionalAppend(rootDrawingGroupChildren);
			}
			this._open = false;
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x00086590 File Offset: 0x00085990
		internal override void WalkCurrentValue(DrawingContextWalker ctx)
		{
			int num = 0;
			if (!base.IsBaseValueDefault(DrawingGroup.TransformProperty) || AnimationStorage.GetStorage(this, DrawingGroup.TransformProperty) != null)
			{
				ctx.PushTransform(this.Transform);
				num++;
			}
			if (!base.IsBaseValueDefault(DrawingGroup.ClipGeometryProperty) || AnimationStorage.GetStorage(this, DrawingGroup.ClipGeometryProperty) != null)
			{
				ctx.PushClip(this.ClipGeometry);
				num++;
			}
			if (!base.IsBaseValueDefault(DrawingGroup.OpacityProperty) || AnimationStorage.GetStorage(this, DrawingGroup.OpacityProperty) != null)
			{
				ctx.PushOpacity(this.Opacity);
				num++;
			}
			if (this.OpacityMask != null)
			{
				ctx.PushOpacityMask(this.OpacityMask);
				num++;
			}
			if (this.BitmapEffect != null)
			{
				ctx.PushEffect(this.BitmapEffect, this.BitmapEffectInput);
				num++;
			}
			DrawingCollection children = this.Children;
			if (children != null)
			{
				for (int i = 0; i < children.Count; i++)
				{
					Drawing drawing = children.Internal_GetItem(i);
					if (drawing != null)
					{
						drawing.WalkCurrentValue(ctx);
						if (ctx.ShouldStopWalking)
						{
							break;
						}
					}
				}
			}
			for (int j = 0; j < num; j++)
			{
				ctx.Pop();
			}
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x000866A0 File Offset: 0x00085AA0
		private void VerifyOpen()
		{
			base.WritePreamble();
			if (this._open)
			{
				throw new InvalidOperationException(SR.Get("DrawingGroup_AlreadyOpen"));
			}
			this._open = true;
		}

		/// <summary>Cria uma cópia profunda modificável deste <see cref="T:System.Windows.Media.DrawingGroup" /> e faz cópias profundas de seus valores.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado retorna <see langword="false" /> mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem é <see langword="true." /></returns>
		// Token: 0x06002146 RID: 8518 RVA: 0x000866D4 File Offset: 0x00085AD4
		public new DrawingGroup Clone()
		{
			return (DrawingGroup)base.Clone();
		}

		/// <summary>Cria uma cópia profunda modificável deste objeto <see cref="T:System.Windows.Media.DrawingGroup" /> e faz cópias profundas de seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado é <see langword="false" /> mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem é <see langword="true" />.</returns>
		// Token: 0x06002147 RID: 8519 RVA: 0x000866EC File Offset: 0x00085AEC
		public new DrawingGroup CloneCurrentValue()
		{
			return (DrawingGroup)base.CloneCurrentValue();
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x00086704 File Offset: 0x00085B04
		private static void ChildrenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			DrawingGroup drawingGroup = (DrawingGroup)d;
			DrawingCollection drawingCollection = null;
			DrawingCollection drawingCollection2 = null;
			if (e.OldValueSource != BaseValueSourceInternal.Default || e.IsOldValueModified)
			{
				drawingCollection = (DrawingCollection)e.OldValue;
				if (drawingCollection != null && !drawingCollection.IsFrozen)
				{
					drawingCollection.ItemRemoved -= drawingGroup.ChildrenItemRemoved;
					drawingCollection.ItemInserted -= drawingGroup.ChildrenItemInserted;
				}
			}
			if (e.NewValueSource != BaseValueSourceInternal.Default || e.IsNewValueModified)
			{
				drawingCollection2 = (DrawingCollection)e.NewValue;
				if (drawingCollection2 != null && !drawingCollection2.IsFrozen)
				{
					drawingCollection2.ItemInserted += drawingGroup.ChildrenItemInserted;
					drawingCollection2.ItemRemoved += drawingGroup.ChildrenItemRemoved;
				}
			}
			if (drawingCollection != drawingCollection2 && drawingGroup.Dispatcher != null)
			{
				using (CompositionEngineLock.Acquire())
				{
					DUCE.IResource resource = drawingGroup;
					int channelCount = resource.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource.GetChannel(i);
						if (drawingCollection2 != null)
						{
							int count = drawingCollection2.Count;
							for (int j = 0; j < count; j++)
							{
								DUCE.IResource resource2 = drawingCollection2.Internal_GetItem(j);
								resource2.AddRefOnChannel(channel);
							}
						}
						if (drawingCollection != null)
						{
							int count2 = drawingCollection.Count;
							for (int k = 0; k < count2; k++)
							{
								DUCE.IResource resource3 = drawingCollection.Internal_GetItem(k);
								resource3.ReleaseOnChannel(channel);
							}
						}
					}
				}
			}
			drawingGroup.PropertyChanged(DrawingGroup.ChildrenProperty);
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x000868A8 File Offset: 0x00085CA8
		private static void ClipGeometryPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			DrawingGroup drawingGroup = (DrawingGroup)d;
			Geometry resource = (Geometry)e.OldValue;
			Geometry resource2 = (Geometry)e.NewValue;
			Dispatcher dispatcher = drawingGroup.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = drawingGroup;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						drawingGroup.ReleaseResource(resource, channel);
						drawingGroup.AddRefResource(resource2, channel);
					}
				}
			}
			drawingGroup.PropertyChanged(DrawingGroup.ClipGeometryProperty);
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x00086970 File Offset: 0x00085D70
		private static void OpacityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DrawingGroup drawingGroup = (DrawingGroup)d;
			drawingGroup.PropertyChanged(DrawingGroup.OpacityProperty);
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x00086990 File Offset: 0x00085D90
		private static void OpacityMaskPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			DrawingGroup drawingGroup = (DrawingGroup)d;
			Brush resource = (Brush)e.OldValue;
			Brush resource2 = (Brush)e.NewValue;
			Dispatcher dispatcher = drawingGroup.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = drawingGroup;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						drawingGroup.ReleaseResource(resource, channel);
						drawingGroup.AddRefResource(resource2, channel);
					}
				}
			}
			drawingGroup.PropertyChanged(DrawingGroup.OpacityMaskProperty);
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x00086A58 File Offset: 0x00085E58
		private static void TransformPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			DrawingGroup drawingGroup = (DrawingGroup)d;
			Transform resource = (Transform)e.OldValue;
			Transform resource2 = (Transform)e.NewValue;
			Dispatcher dispatcher = drawingGroup.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = drawingGroup;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						drawingGroup.ReleaseResource(resource, channel);
						drawingGroup.AddRefResource(resource2, channel);
					}
				}
			}
			drawingGroup.PropertyChanged(DrawingGroup.TransformProperty);
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x00086B20 File Offset: 0x00085F20
		private static void GuidelineSetPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			DrawingGroup drawingGroup = (DrawingGroup)d;
			GuidelineSet resource = (GuidelineSet)e.OldValue;
			GuidelineSet resource2 = (GuidelineSet)e.NewValue;
			Dispatcher dispatcher = drawingGroup.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = drawingGroup;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						drawingGroup.ReleaseResource(resource, channel);
						drawingGroup.AddRefResource(resource2, channel);
					}
				}
			}
			drawingGroup.PropertyChanged(DrawingGroup.GuidelineSetProperty);
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x00086BE8 File Offset: 0x00085FE8
		private static void EdgeModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DrawingGroup drawingGroup = (DrawingGroup)d;
			drawingGroup.PropertyChanged(RenderOptions.EdgeModeProperty);
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x00086C08 File Offset: 0x00086008
		private static void BitmapEffectPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DrawingGroup drawingGroup = (DrawingGroup)d;
			drawingGroup.PropertyChanged(DrawingGroup.BitmapEffectProperty);
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x00086C28 File Offset: 0x00086028
		private static void BitmapEffectInputPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DrawingGroup drawingGroup = (DrawingGroup)d;
			drawingGroup.PropertyChanged(DrawingGroup.BitmapEffectInputProperty);
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x00086C48 File Offset: 0x00086048
		private static void BitmapScalingModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DrawingGroup drawingGroup = (DrawingGroup)d;
			drawingGroup.PropertyChanged(RenderOptions.BitmapScalingModeProperty);
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x00086C68 File Offset: 0x00086068
		private static void ClearTypeHintPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DrawingGroup drawingGroup = (DrawingGroup)d;
			drawingGroup.PropertyChanged(RenderOptions.ClearTypeHintProperty);
		}

		/// <summary>Obtém ou define os objetos <see cref="T:System.Windows.Media.Drawing" /> contidos neste <see cref="T:System.Windows.Media.DrawingGroup" />.</summary>
		/// <returns>Uma coleção dos objetos <see cref="T:System.Windows.Media.Drawing" /> neste <see cref="T:System.Windows.Media.DrawingGroup" />. O padrão é um <see cref="T:System.Windows.Media.DrawingCollection" /> vazio.</returns>
		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06002153 RID: 8531 RVA: 0x00086C88 File Offset: 0x00086088
		// (set) Token: 0x06002154 RID: 8532 RVA: 0x00086CA8 File Offset: 0x000860A8
		public DrawingCollection Children
		{
			get
			{
				return (DrawingCollection)base.GetValue(DrawingGroup.ChildrenProperty);
			}
			set
			{
				base.SetValueInternal(DrawingGroup.ChildrenProperty, value);
			}
		}

		/// <summary>Obtém ou define a região de corte desse <see cref="T:System.Windows.Media.DrawingGroup" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.Geometry" /> que é usado para recortar esse <see cref="T:System.Windows.Media.DrawingGroup" />. O padrão é <see langword="null" />.</returns>
		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06002155 RID: 8533 RVA: 0x00086CC4 File Offset: 0x000860C4
		// (set) Token: 0x06002156 RID: 8534 RVA: 0x00086CE4 File Offset: 0x000860E4
		public Geometry ClipGeometry
		{
			get
			{
				return (Geometry)base.GetValue(DrawingGroup.ClipGeometryProperty);
			}
			set
			{
				base.SetValueInternal(DrawingGroup.ClipGeometryProperty, value);
			}
		}

		/// <summary>Obtém ou define a opacidade deste <see cref="T:System.Windows.Media.DrawingGroup" />.</summary>
		/// <returns>Um valor entre 0 e 1, inclusive, que descreve a opacidade deste <see cref="T:System.Windows.Media.DrawingGroup" />. O padrão é 1.</returns>
		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06002157 RID: 8535 RVA: 0x00086D00 File Offset: 0x00086100
		// (set) Token: 0x06002158 RID: 8536 RVA: 0x00086D20 File Offset: 0x00086120
		public double Opacity
		{
			get
			{
				return (double)base.GetValue(DrawingGroup.OpacityProperty);
			}
			set
			{
				base.SetValueInternal(DrawingGroup.OpacityProperty, value);
			}
		}

		/// <summary>Obtém ou define o pincel usado para alterar a opacidade de regiões selecionados deste <see cref="T:System.Windows.Media.DrawingGroup" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Brush" /> que descreve a opacidade deste <see cref="T:System.Windows.Media.DrawingGroup" />; <see langword="null" /> indica que existe sem máscara de opacidade e a opacidade é uniforme. O padrão é <see langword="null" />.</returns>
		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06002159 RID: 8537 RVA: 0x00086D40 File Offset: 0x00086140
		// (set) Token: 0x0600215A RID: 8538 RVA: 0x00086D60 File Offset: 0x00086160
		public Brush OpacityMask
		{
			get
			{
				return (Brush)base.GetValue(DrawingGroup.OpacityMaskProperty);
			}
			set
			{
				base.SetValueInternal(DrawingGroup.OpacityMaskProperty, value);
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Transform" /> aplicado a este <see cref="T:System.Windows.Media.DrawingGroup" />.</summary>
		/// <returns>A transformação a ser aplicada a este <see cref="T:System.Windows.Media.DrawingGroup" />. O padrão é <see langword="null" />.</returns>
		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600215B RID: 8539 RVA: 0x00086D7C File Offset: 0x0008617C
		// (set) Token: 0x0600215C RID: 8540 RVA: 0x00086D9C File Offset: 0x0008619C
		public Transform Transform
		{
			get
			{
				return (Transform)base.GetValue(DrawingGroup.TransformProperty);
			}
			set
			{
				base.SetValueInternal(DrawingGroup.TransformProperty, value);
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.GuidelineSet" /> a ser aplicado a este <see cref="T:System.Windows.Media.DrawingGroup" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.GuidelineSet" /> a ser aplicado a este <see cref="T:System.Windows.Media.DrawingGroup" />. O padrão é <see langword="null" />.</returns>
		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x0600215D RID: 8541 RVA: 0x00086DB8 File Offset: 0x000861B8
		// (set) Token: 0x0600215E RID: 8542 RVA: 0x00086DD8 File Offset: 0x000861D8
		public GuidelineSet GuidelineSet
		{
			get
			{
				return (GuidelineSet)base.GetValue(DrawingGroup.GuidelineSetProperty);
			}
			set
			{
				base.SetValueInternal(DrawingGroup.GuidelineSetProperty, value);
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> a ser aplicado a este <see cref="T:System.Windows.Media.DrawingGroup" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> a ser aplicado a este <see cref="T:System.Windows.Media.DrawingGroup" />. O padrão é <see langword="null" />.</returns>
		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x0600215F RID: 8543 RVA: 0x00086DF4 File Offset: 0x000861F4
		// (set) Token: 0x06002160 RID: 8544 RVA: 0x00086E14 File Offset: 0x00086214
		public BitmapEffect BitmapEffect
		{
			get
			{
				return (BitmapEffect)base.GetValue(DrawingGroup.BitmapEffectProperty);
			}
			set
			{
				base.SetValueInternal(DrawingGroup.BitmapEffectProperty, value);
			}
		}

		/// <summary>Obtém ou define a região em que o <see cref="T:System.Windows.Media.DrawingGroup" /> aplica seu <see cref="P:System.Windows.Media.DrawingGroup.BitmapEffect" /> e, opcionalmente, um <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> para usar como entrada para seu <see cref="P:System.Windows.Media.DrawingGroup.BitmapEffect" />.</summary>
		/// <returns>A região em que o <see cref="P:System.Windows.Media.DrawingGroup.BitmapEffect" /> do <see cref="T:System.Windows.Media.DrawingGroup" /> é aplicado e, opcionalmente, o <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> para usar como entrada; ou <see langword="null" /> se o <see cref="P:System.Windows.Media.DrawingGroup.BitmapEffect" /> se aplica a todo o <see cref="T:System.Windows.Media.DrawingGroup" /> e usa o <see cref="T:System.Windows.Media.DrawingGroup" /> como sua entrada. O padrão é <see langword="null" />.</returns>
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06002161 RID: 8545 RVA: 0x00086E30 File Offset: 0x00086230
		// (set) Token: 0x06002162 RID: 8546 RVA: 0x00086E50 File Offset: 0x00086250
		public BitmapEffectInput BitmapEffectInput
		{
			get
			{
				return (BitmapEffectInput)base.GetValue(DrawingGroup.BitmapEffectInputProperty);
			}
			set
			{
				base.SetValueInternal(DrawingGroup.BitmapEffectInputProperty, value);
			}
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x00086E6C File Offset: 0x0008626C
		protected override Freezable CreateInstanceCore()
		{
			return new DrawingGroup();
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x00086E80 File Offset: 0x00086280
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				DrawingCollection children = this.Children;
				Geometry clipGeometry = this.ClipGeometry;
				Brush opacityMask = this.OpacityMask;
				Transform transform = this.Transform;
				GuidelineSet guidelineSet = this.GuidelineSet;
				DUCE.ResourceHandle hClipGeometry = (clipGeometry != null) ? ((DUCE.IResource)clipGeometry).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.ResourceHandle hOpacityMask = (opacityMask != null) ? ((DUCE.IResource)opacityMask).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.ResourceHandle hTransform;
				if (transform == null || transform == Transform.Identity)
				{
					hTransform = DUCE.ResourceHandle.Null;
				}
				else
				{
					hTransform = ((DUCE.IResource)transform).GetHandle(channel);
				}
				DUCE.ResourceHandle hGuidelineSet = (guidelineSet != null) ? ((DUCE.IResource)guidelineSet).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(DrawingGroup.OpacityProperty, channel);
				int num = (children == null) ? 0 : children.Count;
				DUCE.MILCMD_DRAWINGGROUP milcmd_DRAWINGGROUP;
				milcmd_DRAWINGGROUP.Type = MILCMD.MilCmdDrawingGroup;
				milcmd_DRAWINGGROUP.Handle = this._duceResource.GetHandle(channel);
				milcmd_DRAWINGGROUP.ChildrenSize = (uint)(sizeof(DUCE.ResourceHandle) * num);
				milcmd_DRAWINGGROUP.hClipGeometry = hClipGeometry;
				if (animationResourceHandle.IsNull)
				{
					milcmd_DRAWINGGROUP.Opacity = this.Opacity;
				}
				milcmd_DRAWINGGROUP.hOpacityAnimations = animationResourceHandle;
				milcmd_DRAWINGGROUP.hOpacityMask = hOpacityMask;
				milcmd_DRAWINGGROUP.hTransform = hTransform;
				milcmd_DRAWINGGROUP.hGuidelineSet = hGuidelineSet;
				milcmd_DRAWINGGROUP.EdgeMode = (EdgeMode)base.GetValue(RenderOptions.EdgeModeProperty);
				milcmd_DRAWINGGROUP.bitmapScalingMode = (BitmapScalingMode)base.GetValue(RenderOptions.BitmapScalingModeProperty);
				milcmd_DRAWINGGROUP.ClearTypeHint = (ClearTypeHint)base.GetValue(RenderOptions.ClearTypeHintProperty);
				channel.BeginCommand((byte*)(&milcmd_DRAWINGGROUP), sizeof(DUCE.MILCMD_DRAWINGGROUP), (int)milcmd_DRAWINGGROUP.ChildrenSize);
				for (int i = 0; i < num; i++)
				{
					DUCE.ResourceHandle handle = ((DUCE.IResource)children.Internal_GetItem(i)).GetHandle(channel);
					channel.AppendCommandData((byte*)(&handle), sizeof(DUCE.ResourceHandle));
				}
				channel.EndCommand();
			}
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x00087044 File Offset: 0x00086444
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_DRAWINGGROUP))
			{
				Geometry clipGeometry = this.ClipGeometry;
				if (clipGeometry != null)
				{
					((DUCE.IResource)clipGeometry).AddRefOnChannel(channel);
				}
				Brush opacityMask = this.OpacityMask;
				if (opacityMask != null)
				{
					((DUCE.IResource)opacityMask).AddRefOnChannel(channel);
				}
				Transform transform = this.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).AddRefOnChannel(channel);
				}
				GuidelineSet guidelineSet = this.GuidelineSet;
				if (guidelineSet != null)
				{
					((DUCE.IResource)guidelineSet).AddRefOnChannel(channel);
				}
				DrawingCollection children = this.Children;
				if (children != null)
				{
					int count = children.Count;
					for (int i = 0; i < count; i++)
					{
						((DUCE.IResource)children.Internal_GetItem(i)).AddRefOnChannel(channel);
					}
				}
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x000870FC File Offset: 0x000864FC
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				Geometry clipGeometry = this.ClipGeometry;
				if (clipGeometry != null)
				{
					((DUCE.IResource)clipGeometry).ReleaseOnChannel(channel);
				}
				Brush opacityMask = this.OpacityMask;
				if (opacityMask != null)
				{
					((DUCE.IResource)opacityMask).ReleaseOnChannel(channel);
				}
				Transform transform = this.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).ReleaseOnChannel(channel);
				}
				GuidelineSet guidelineSet = this.GuidelineSet;
				if (guidelineSet != null)
				{
					((DUCE.IResource)guidelineSet).ReleaseOnChannel(channel);
				}
				DrawingCollection children = this.Children;
				if (children != null)
				{
					int count = children.Count;
					for (int i = 0; i < count; i++)
					{
						((DUCE.IResource)children.Internal_GetItem(i)).ReleaseOnChannel(channel);
					}
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x00087198 File Offset: 0x00086598
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x000871B4 File Offset: 0x000865B4
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x000871CC File Offset: 0x000865CC
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x000871E8 File Offset: 0x000865E8
		private void ChildrenItemInserted(object sender, object item)
		{
			if (base.Dispatcher != null)
			{
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = ((DUCE.IResource)this).GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = ((DUCE.IResource)this).GetChannel(i);
						DUCE.IResource resource = item as DUCE.IResource;
						if (resource != null)
						{
							resource.AddRefOnChannel(channel);
						}
						this.UpdateResource(channel, true);
					}
				}
			}
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x0008726C File Offset: 0x0008666C
		private void ChildrenItemRemoved(object sender, object item)
		{
			if (base.Dispatcher != null)
			{
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = ((DUCE.IResource)this).GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = ((DUCE.IResource)this).GetChannel(i);
						this.UpdateResource(channel, true);
						DUCE.IResource resource = item as DUCE.IResource;
						if (resource != null)
						{
							resource.ReleaseOnChannel(channel);
						}
					}
				}
			}
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x000872EC File Offset: 0x000866EC
		static DrawingGroup()
		{
			RenderOptions.EdgeModeProperty.OverrideMetadata(typeof(DrawingGroup), new UIPropertyMetadata(EdgeMode.Unspecified, new PropertyChangedCallback(DrawingGroup.EdgeModePropertyChanged)));
			RenderOptions.BitmapScalingModeProperty.OverrideMetadata(typeof(DrawingGroup), new UIPropertyMetadata(BitmapScalingMode.Unspecified, new PropertyChangedCallback(DrawingGroup.BitmapScalingModePropertyChanged)));
			RenderOptions.ClearTypeHintProperty.OverrideMetadata(typeof(DrawingGroup), new UIPropertyMetadata(ClearTypeHint.Auto, new PropertyChangedCallback(DrawingGroup.ClearTypeHintPropertyChanged)));
			Type typeFromHandle = typeof(DrawingGroup);
			DrawingGroup.ChildrenProperty = Animatable.RegisterProperty("Children", typeof(DrawingCollection), typeFromHandle, new FreezableDefaultValueFactory(DrawingCollection.Empty), new PropertyChangedCallback(DrawingGroup.ChildrenPropertyChanged), null, false, null);
			DrawingGroup.ClipGeometryProperty = Animatable.RegisterProperty("ClipGeometry", typeof(Geometry), typeFromHandle, null, new PropertyChangedCallback(DrawingGroup.ClipGeometryPropertyChanged), null, false, null);
			DrawingGroup.OpacityProperty = Animatable.RegisterProperty("Opacity", typeof(double), typeFromHandle, 1.0, new PropertyChangedCallback(DrawingGroup.OpacityPropertyChanged), null, true, null);
			DrawingGroup.OpacityMaskProperty = Animatable.RegisterProperty("OpacityMask", typeof(Brush), typeFromHandle, null, new PropertyChangedCallback(DrawingGroup.OpacityMaskPropertyChanged), null, false, null);
			DrawingGroup.TransformProperty = Animatable.RegisterProperty("Transform", typeof(Transform), typeFromHandle, null, new PropertyChangedCallback(DrawingGroup.TransformPropertyChanged), null, false, null);
			DrawingGroup.GuidelineSetProperty = Animatable.RegisterProperty("GuidelineSet", typeof(GuidelineSet), typeFromHandle, null, new PropertyChangedCallback(DrawingGroup.GuidelineSetPropertyChanged), null, false, null);
			DrawingGroup.BitmapEffectProperty = Animatable.RegisterProperty("BitmapEffect", typeof(BitmapEffect), typeFromHandle, null, new PropertyChangedCallback(DrawingGroup.BitmapEffectPropertyChanged), null, false, null);
			DrawingGroup.BitmapEffectInputProperty = Animatable.RegisterProperty("BitmapEffectInput", typeof(BitmapEffectInput), typeFromHandle, null, new PropertyChangedCallback(DrawingGroup.BitmapEffectInputPropertyChanged), null, false, null);
		}

		// Token: 0x040010A9 RID: 4265
		private bool _openedForAppend;

		// Token: 0x040010AA RID: 4266
		private bool _open;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.DrawingGroup.Children" />.</summary>
		// Token: 0x040010AB RID: 4267
		public static readonly DependencyProperty ChildrenProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.DrawingGroup.ClipGeometry" />.</summary>
		// Token: 0x040010AC RID: 4268
		public static readonly DependencyProperty ClipGeometryProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.DrawingGroup.Opacity" />.</summary>
		// Token: 0x040010AD RID: 4269
		public static readonly DependencyProperty OpacityProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.DrawingGroup.OpacityMask" />.</summary>
		// Token: 0x040010AE RID: 4270
		public static readonly DependencyProperty OpacityMaskProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.DrawingGroup.Transform" />.</summary>
		// Token: 0x040010AF RID: 4271
		public static readonly DependencyProperty TransformProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.DrawingGroup.GuidelineSet" />.</summary>
		// Token: 0x040010B0 RID: 4272
		public static readonly DependencyProperty GuidelineSetProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.DrawingGroup.BitmapEffect" />.</summary>
		// Token: 0x040010B1 RID: 4273
		public static readonly DependencyProperty BitmapEffectProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.DrawingGroup.BitmapEffectInput" />.</summary>
		// Token: 0x040010B2 RID: 4274
		public static readonly DependencyProperty BitmapEffectInputProperty;

		// Token: 0x040010B3 RID: 4275
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x040010B4 RID: 4276
		internal static DrawingCollection s_Children = DrawingCollection.Empty;

		// Token: 0x040010B5 RID: 4277
		internal const double c_Opacity = 1.0;

		// Token: 0x040010B6 RID: 4278
		internal const EdgeMode c_EdgeMode = EdgeMode.Unspecified;

		// Token: 0x040010B7 RID: 4279
		internal const BitmapScalingMode c_BitmapScalingMode = BitmapScalingMode.Unspecified;

		// Token: 0x040010B8 RID: 4280
		internal const ClearTypeHint c_ClearTypeHint = ClearTypeHint.Auto;
	}
}
