using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Permissions;
using System.Windows.Diagnostics;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal;
using MS.Internal.Media;
using MS.Internal.Media3D;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Media3D
{
	/// <summary>Fornece serviços e propriedades que são comuns a objetos visuais 3D, incluindo testes de clique, transformação de coordenada e cálculos de caixa delimitadora.</summary>
	// Token: 0x02000487 RID: 1159
	public abstract class Visual3D : DependencyObject, DUCE.IResource, IVisual3DContainer, IAnimatable
	{
		// Token: 0x060032AC RID: 12972 RVA: 0x000CA0DC File Offset: 0x000C94DC
		internal Visual3D()
		{
			this._internalIsVisible = true;
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x000CA108 File Offset: 0x000C9508
		internal bool IsOnChannel(DUCE.Channel channel)
		{
			return this._proxy.IsOnChannel(channel);
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x000CA124 File Offset: 0x000C9524
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			return this._proxy.GetHandle(channel);
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x000CA140 File Offset: 0x000C9540
		DUCE.ResourceHandle DUCE.IResource.Get3DHandle(DUCE.Channel channel)
		{
			return this._proxy.GetHandle(channel);
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x000CA15C File Offset: 0x000C955C
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			this._proxy.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_VISUAL3D);
			return this._proxy.GetHandle(channel);
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x000CA188 File Offset: 0x000C9588
		void DUCE.IResource.RemoveChildFromParent(DUCE.IResource parent, DUCE.Channel channel)
		{
			DUCE.Visual3DNode.RemoveChild(parent.Get3DHandle(channel), this._proxy.GetHandle(channel), channel);
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x000CA1B0 File Offset: 0x000C95B0
		int DUCE.IResource.GetChannelCount()
		{
			return this._proxy.Count;
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x000CA1C8 File Offset: 0x000C95C8
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this._proxy.GetChannel(index);
		}

		// Token: 0x060032B4 RID: 12980 RVA: 0x000CA1E4 File Offset: 0x000C95E4
		private static void TransformPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Visual3D visual3D = (Visual3D)d;
			if (!e.IsASubPropertyChange)
			{
				if (e.OldValue != null)
				{
					visual3D.DisconnectAttachedResource(VisualProxyFlags.IsTransformDirty, (DUCE.IResource)e.OldValue);
				}
				visual3D.SetFlagsOnAllChannels(true, VisualProxyFlags.IsTransformDirty);
			}
			visual3D.RenderChanged(visual3D, EventArgs.Empty);
		}

		/// <summary>Obtém ou define a transformação que é aplicada ao objeto 3D.</summary>
		/// <returns>A transformação a ser aplicada ao objeto 3D. O padrão é o <see cref="P:System.Windows.Media.Media3D.Transform3D.Identity" /> transformação.</returns>
		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060032B5 RID: 12981 RVA: 0x000CA234 File Offset: 0x000C9634
		// (set) Token: 0x060032B6 RID: 12982 RVA: 0x000CA254 File Offset: 0x000C9654
		public Transform3D Transform
		{
			get
			{
				return (Transform3D)base.GetValue(Visual3D.TransformProperty);
			}
			set
			{
				base.SetValue(Visual3D.TransformProperty, value);
			}
		}

		/// <summary>Define a relação pai-filho entre dois visuais 3D.</summary>
		/// <param name="child">O objeto visual 3D filho a adicionar ao objeto visual 3D pai.</param>
		/// <exception cref="T:System.InvalidOperationException">A coleção de filhos não pode ser modificada quando uma iteração filho visual está em andamento.</exception>
		// Token: 0x060032B7 RID: 12983 RVA: 0x000CA270 File Offset: 0x000C9670
		protected void AddVisual3DChild(Visual3D child)
		{
			if (this.IsVisualChildrenIterationInProgress)
			{
				throw new InvalidOperationException(SR.Get("CannotModifyVisualChildrenDuringTreeWalk"));
			}
			VisualDiagnostics.VerifyVisualTreeChange(this);
			child.SetParent(this);
			base.ProvideSelfAsInheritanceContext(child, null);
			Visual3D.PropagateFlags(this, VisualFlags.IsSubtreeDirtyForPrecompute, VisualProxyFlags.IsSubtreeDirtyForRender);
			Visual3D.PropagateFlags(child, VisualFlags.IsSubtreeDirtyForPrecompute, VisualProxyFlags.IsSubtreeDirtyForRender);
			this.OnVisualChildrenChanged(child, null);
			child.FireOnVisualParentChanged(null);
			VisualDiagnostics.OnVisualChildChanged(this, child, true);
		}

		/// <summary>Remove a relação pai-filho entre dois visuais 3D.</summary>
		/// <param name="child">O objeto visual 3D filho a ser removido do visual pai.</param>
		// Token: 0x060032B8 RID: 12984 RVA: 0x000CA2D4 File Offset: 0x000C96D4
		protected void RemoveVisual3DChild(Visual3D child)
		{
			if (this.IsVisualChildrenIterationInProgress)
			{
				throw new InvalidOperationException(SR.Get("CannotModifyVisualChildrenDuringTreeWalk"));
			}
			VisualDiagnostics.VerifyVisualTreeChange(this);
			VisualDiagnostics.OnVisualChildChanged(this, child, false);
			child.SetParent(null);
			base.RemoveSelfAsInheritanceContext(child, null);
			int i = 0;
			int count = this._proxy.Count;
			while (i < count)
			{
				DUCE.Channel channel = this._proxy.GetChannel(i);
				if (child.CheckFlagsAnd(channel, VisualProxyFlags.IsConnectedToParent))
				{
					child.SetFlags(channel, false, VisualProxyFlags.IsConnectedToParent);
					((DUCE.IResource)child).RemoveChildFromParent(this, channel);
					((DUCE.IResource)child).ReleaseOnChannel(channel);
				}
				i++;
			}
			Visual3D.PropagateFlags(this, VisualFlags.IsSubtreeDirtyForPrecompute, VisualProxyFlags.IsSubtreeDirtyForRender);
			child.FireOnVisualParentChanged(this);
			this.OnVisualChildrenChanged(null, child);
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x060032B9 RID: 12985 RVA: 0x000CA380 File Offset: 0x000C9780
		// (set) Token: 0x060032BA RID: 12986 RVA: 0x000CA394 File Offset: 0x000C9794
		internal bool InternalIsVisible
		{
			get
			{
				return this._internalIsVisible;
			}
			set
			{
				if (this._internalIsVisible != value)
				{
					if (value)
					{
						this.DisconnectAttachedResource(VisualProxyFlags.IsTransformDirty, Visual3D._zeroScale);
					}
					else
					{
						Transform3D transform = this.Transform;
						if (transform != null)
						{
							this.DisconnectAttachedResource(VisualProxyFlags.IsTransformDirty, transform);
						}
					}
					this.SetFlagsOnAllChannels(true, VisualProxyFlags.IsTransformDirty);
					this.RenderChanged(this, EventArgs.Empty);
					this._internalIsVisible = value;
				}
			}
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x000CA3E8 File Offset: 0x000C97E8
		private void Visual3DModelPropertyChanged(Model3D oldValue, bool isSubpropertyChange)
		{
			if (!isSubpropertyChange)
			{
				if (oldValue != null)
				{
					this.DisconnectAttachedResource(VisualProxyFlags.IsContentDirty, oldValue);
				}
				this.SetFlagsOnAllChannels(true, VisualProxyFlags.IsContentDirty);
			}
			this.SetFlags(false, VisualFlags.Are3DContentBoundsValid);
			this.RenderChanged(this, EventArgs.Empty);
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x000CA420 File Offset: 0x000C9820
		private void Visual3DModelPropertyChanged(object o, EventArgs e)
		{
			this.Visual3DModelPropertyChanged(null, true);
		}

		/// <summary>Obtém ou define o objeto <see cref="T:System.Windows.Media.Media3D.Model3D" /> a ser renderizado.</summary>
		/// <returns>O objeto <see cref="T:System.Windows.Media.Media3D.Model3D" /> a ser renderizado.</returns>
		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x060032BD RID: 12989 RVA: 0x000CA438 File Offset: 0x000C9838
		// (set) Token: 0x060032BE RID: 12990 RVA: 0x000CA454 File Offset: 0x000C9854
		protected Model3D Visual3DModel
		{
			get
			{
				this.VerifyAPIReadOnly();
				return this._visual3DModel;
			}
			set
			{
				this.VerifyAPIReadWrite();
				if (value != this._visual3DModel)
				{
					if (this._visual3DModel != null && !this._visual3DModel.IsFrozenInternal)
					{
						this._visual3DModel.ChangedInternal -= this.Visual3DModelPropertyChanged;
					}
					this.Visual3DModelPropertyChanged(this._visual3DModel, false);
					this._visual3DModel = value;
					if (this._visual3DModel != null && !this._visual3DModel.IsFrozenInternal)
					{
						this._visual3DModel.ChangedInternal += this.Visual3DModelPropertyChanged;
					}
				}
			}
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x000CA4DC File Offset: 0x000C98DC
		internal virtual void FireOnVisualParentChanged(DependencyObject oldParent)
		{
			this.OnVisualParentChanged(oldParent);
			if (oldParent == null)
			{
				if (this.CheckFlagsAnd(VisualFlags.SubTreeHoldsAncestorChanged))
				{
					Visual.SetTreeBits(VisualTreeHelper.GetParent(this), VisualFlags.SubTreeHoldsAncestorChanged, VisualFlags.RegisteredForAncestorChanged);
				}
			}
			else if (this.CheckFlagsAnd(VisualFlags.SubTreeHoldsAncestorChanged))
			{
				Visual.ClearTreeBits(oldParent, VisualFlags.SubTreeHoldsAncestorChanged, VisualFlags.RegisteredForAncestorChanged);
			}
			AncestorChangedEventArgs args = new AncestorChangedEventArgs(this, oldParent);
			Visual3D.ProcessAncestorChangedNotificationRecursive(this, args);
		}

		// Token: 0x140001B9 RID: 441
		// (add) Token: 0x060032C0 RID: 12992 RVA: 0x000CA544 File Offset: 0x000C9944
		// (remove) Token: 0x060032C1 RID: 12993 RVA: 0x000CA590 File Offset: 0x000C9990
		internal event Visual.AncestorChangedEventHandler VisualAncestorChanged
		{
			add
			{
				Visual.AncestorChangedEventHandler ancestorChangedEventHandler = Visual3D.AncestorChangedEventField.GetValue(this);
				if (ancestorChangedEventHandler == null)
				{
					ancestorChangedEventHandler = value;
				}
				else
				{
					ancestorChangedEventHandler = (Visual.AncestorChangedEventHandler)Delegate.Combine(ancestorChangedEventHandler, value);
				}
				Visual3D.AncestorChangedEventField.SetValue(this, ancestorChangedEventHandler);
				Visual.SetTreeBits(this, VisualFlags.SubTreeHoldsAncestorChanged, VisualFlags.RegisteredForAncestorChanged);
			}
			remove
			{
				if (this.CheckFlagsAnd(VisualFlags.SubTreeHoldsAncestorChanged))
				{
					Visual.ClearTreeBits(this, VisualFlags.SubTreeHoldsAncestorChanged, VisualFlags.RegisteredForAncestorChanged);
				}
				Visual.AncestorChangedEventHandler ancestorChangedEventHandler = Visual3D.AncestorChangedEventField.GetValue(this);
				if (ancestorChangedEventHandler != null)
				{
					ancestorChangedEventHandler = (Visual.AncestorChangedEventHandler)Delegate.Remove(ancestorChangedEventHandler, value);
					if (ancestorChangedEventHandler == null)
					{
						Visual3D.AncestorChangedEventField.ClearValue(this);
						return;
					}
					Visual3D.AncestorChangedEventField.SetValue(this, ancestorChangedEventHandler);
				}
			}
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x000CA5F4 File Offset: 0x000C99F4
		internal static void ProcessAncestorChangedNotificationRecursive(DependencyObject e, AncestorChangedEventArgs args)
		{
			if (e is Visual)
			{
				Visual.ProcessAncestorChangedNotificationRecursive(e, args);
				return;
			}
			Visual3D visual3D = e as Visual3D;
			if (!visual3D.CheckFlagsAnd(VisualFlags.SubTreeHoldsAncestorChanged))
			{
				return;
			}
			Visual.AncestorChangedEventHandler value = Visual3D.AncestorChangedEventField.GetValue(visual3D);
			if (value != null)
			{
				value(visual3D, args);
			}
			int internalVisual2DOr3DChildrenCount = visual3D.InternalVisual2DOr3DChildrenCount;
			for (int i = 0; i < internalVisual2DOr3DChildrenCount; i++)
			{
				DependencyObject dependencyObject = visual3D.InternalGet2DOr3DVisualChild(i);
				if (dependencyObject != null)
				{
					Visual3D.ProcessAncestorChangedNotificationRecursive(dependencyObject, args);
				}
			}
		}

		/// <summary>Chamado quando o pai do objeto visual 3D é alterado.</summary>
		/// <param name="oldParent">Um valor do tipo <see cref="T:System.Windows.DependencyObject" /> que representa o pai anterior do objeto <see cref="T:System.Windows.Media.Media3D.Visual3D" />. Se o objeto <see cref="T:System.Windows.Media.Media3D.Visual3D" /> não tiver um pai anterior, o valor do parâmetro será <see langword="null" />.</param>
		// Token: 0x060032C3 RID: 12995 RVA: 0x000CA664 File Offset: 0x000C9A64
		protected internal virtual void OnVisualParentChanged(DependencyObject oldParent)
		{
		}

		/// <summary>Chamado quando o <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" /> de um objeto visual é modificado.</summary>
		/// <param name="visualAdded">O <see cref="T:System.Windows.Media.Media3D.Visual3D" /> adicionado à coleção.</param>
		/// <param name="visualRemoved">O <see cref="T:System.Windows.Media.Media3D.Visual3D" /> removido da coleção.</param>
		// Token: 0x060032C4 RID: 12996 RVA: 0x000CA674 File Offset: 0x000C9A74
		protected internal virtual void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
		{
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x000CA684 File Offset: 0x000C9A84
		internal bool DoesRayHitSubgraphBounds(RayHitTestParameters rayParams)
		{
			Point3D point3D;
			Vector3D vector3D;
			rayParams.GetLocalLine(out point3D, out vector3D);
			Rect3D visualDescendantBounds = this.VisualDescendantBounds;
			return LineUtil.ComputeLineBoxIntersection(ref point3D, ref vector3D, ref visualDescendantBounds, rayParams.IsRay);
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x000CA6B4 File Offset: 0x000C9AB4
		internal void HitTest(HitTestFilterCallback filterCallback, HitTestResultCallback resultCallback, HitTestParameters3D hitTestParameters)
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
			RayHitTestParameters rayHitTestParameters = hitTestParameters as RayHitTestParameters;
			if (rayHitTestParameters != null)
			{
				rayHitTestParameters.ClearResults();
				HitTestResultBehavior lastResult = this.RayHitTest(filterCallback, rayHitTestParameters);
				rayHitTestParameters.RaiseCallback(resultCallback, filterCallback, lastResult);
				return;
			}
			Invariant.Assert(false, string.Format(CultureInfo.InvariantCulture, "'{0}' HitTestParameters3D are not supported on {1}.", new object[]
			{
				hitTestParameters.GetType().Name,
				base.GetType().Name
			}));
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x000CA740 File Offset: 0x000C9B40
		internal HitTestResultBehavior RayHitTest(HitTestFilterCallback filterCallback, RayHitTestParameters rayParams)
		{
			if (this.DoesRayHitSubgraphBounds(rayParams))
			{
				HitTestFilterBehavior behavior = HitTestFilterBehavior.Continue;
				if (filterCallback != null)
				{
					behavior = filterCallback(this);
					if (HTFBInterpreter.SkipSubgraph(behavior))
					{
						return HitTestResultBehavior.Continue;
					}
					if (HTFBInterpreter.Stop(behavior))
					{
						return HitTestResultBehavior.Stop;
					}
				}
				if (HTFBInterpreter.IncludeChildren(behavior) && this.HitTestChildren(filterCallback, rayParams) == HitTestResultBehavior.Stop)
				{
					return HitTestResultBehavior.Stop;
				}
				if (HTFBInterpreter.DoHitTest(behavior))
				{
					this.RayHitTestInternal(filterCallback, rayParams);
				}
			}
			return HitTestResultBehavior.Continue;
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000CA7A0 File Offset: 0x000C9BA0
		internal HitTestResultBehavior HitTestChildren(HitTestFilterCallback filterCallback, RayHitTestParameters rayParams)
		{
			return Visual3D.HitTestChildren(filterCallback, rayParams, this);
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x000CA7B8 File Offset: 0x000C9BB8
		internal static HitTestResultBehavior HitTestChildren(HitTestFilterCallback filterCallback, RayHitTestParameters rayParams, IVisual3DContainer container)
		{
			if (container != null)
			{
				int childrenCount = container.GetChildrenCount();
				for (int i = childrenCount - 1; i >= 0; i--)
				{
					Visual3D child = container.GetChild(i);
					Transform3D transform = child.Transform;
					rayParams.PushVisualTransform(transform);
					HitTestResultBehavior hitTestResultBehavior = child.RayHitTest(filterCallback, rayParams);
					rayParams.PopTransform(transform);
					if (hitTestResultBehavior == HitTestResultBehavior.Stop)
					{
						return HitTestResultBehavior.Stop;
					}
				}
			}
			return HitTestResultBehavior.Continue;
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x000CA80C File Offset: 0x000C9C0C
		internal void RayHitTestInternal(HitTestFilterCallback filterCallback, RayHitTestParameters rayParams)
		{
			Model3D visual3DModel = this._visual3DModel;
			if (visual3DModel != null)
			{
				rayParams.CurrentVisual = this;
				visual3DModel.RayHitTest(rayParams);
			}
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x000CA834 File Offset: 0x000C9C34
		internal void RenderChanged(object sender, EventArgs e)
		{
			Visual3D.PropagateFlags(this, VisualFlags.IsSubtreeDirtyForPrecompute, VisualProxyFlags.IsSubtreeDirtyForRender);
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x060032CC RID: 13004 RVA: 0x000CA84C File Offset: 0x000C9C4C
		internal Rect3D VisualContentBounds
		{
			get
			{
				this.VerifyAPIReadWrite();
				return this.GetContentBounds();
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x060032CD RID: 13005 RVA: 0x000CA868 File Offset: 0x000C9C68
		[FriendAccessAllowed]
		internal Rect Visual2DContentBounds
		{
			get
			{
				this.VerifyAPIReadWrite();
				Rect result = Rect.Empty;
				Viewport3DVisual viewport3DVisual = (Viewport3DVisual)VisualTreeHelper.GetContainingVisual2D(this);
				if (viewport3DVisual != null)
				{
					GeneralTransform3DTo2D generalTransform3DTo2D = this.TransformToAncestor(viewport3DVisual);
					result = generalTransform3DTo2D.TransformBounds(this.VisualContentBounds);
				}
				return result;
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060032CE RID: 13006 RVA: 0x000CA8A8 File Offset: 0x000C9CA8
		internal Rect3D BBoxSubgraph
		{
			get
			{
				if (this.CheckFlagsAnd(VisualFlags.IsSubtreeDirtyForPrecompute))
				{
					Rect3D rect3D;
					this.PrecomputeRecursive(out rect3D);
				}
				return this._bboxSubgraph;
			}
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x000CA8CC File Offset: 0x000C9CCC
		internal Rect3D GetContentBounds()
		{
			Model3D visual3DModel = this._visual3DModel;
			if (visual3DModel == null)
			{
				return Rect3D.Empty;
			}
			if (!this.CheckFlagsAnd(VisualFlags.Are3DContentBoundsValid))
			{
				this._bboxContent = visual3DModel.CalculateSubgraphBoundsOuterSpace();
				this.SetFlags(true, VisualFlags.Are3DContentBoundsValid);
			}
			return this._bboxContent;
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x000CA910 File Offset: 0x000C9D10
		internal Rect3D CalculateSubgraphBoundsOuterSpace()
		{
			Rect3D rect3D = this.CalculateSubgraphBoundsInnerSpace();
			return M3DUtil.ComputeTransformedAxisAlignedBoundingBox(ref rect3D, this.Transform);
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x000CA934 File Offset: 0x000C9D34
		internal Rect3D CalculateSubgraphBoundsInnerSpace()
		{
			return this.BBoxSubgraph;
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x060032D2 RID: 13010 RVA: 0x000CA948 File Offset: 0x000C9D48
		internal Rect3D VisualDescendantBounds
		{
			get
			{
				this.VerifyAPIReadWrite();
				return this.CalculateSubgraphBoundsInnerSpace();
			}
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x000CA964 File Offset: 0x000C9D64
		void IVisual3DContainer.VerifyAPIReadOnly()
		{
			this.VerifyAPIReadOnly();
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x000CA978 File Offset: 0x000C9D78
		void IVisual3DContainer.VerifyAPIReadOnly(DependencyObject other)
		{
			this.VerifyAPIReadOnly(other);
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x000CA98C File Offset: 0x000C9D8C
		void IVisual3DContainer.VerifyAPIReadWrite()
		{
			this.VerifyAPIReadWrite();
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x000CA9A0 File Offset: 0x000C9DA0
		void IVisual3DContainer.VerifyAPIReadWrite(DependencyObject other)
		{
			this.VerifyAPIReadWrite(other);
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x000CA9B4 File Offset: 0x000C9DB4
		internal void VerifyAPIReadOnly()
		{
			base.VerifyAccess();
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x000CA9C8 File Offset: 0x000C9DC8
		internal void VerifyAPIReadOnly(DependencyObject other)
		{
			this.VerifyAPIReadOnly();
			if (other != null)
			{
				MediaSystem.AssertSameContext(this, other);
			}
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x000CA9E8 File Offset: 0x000C9DE8
		internal void VerifyAPIReadWrite()
		{
			this.VerifyAPIReadOnly();
			MediaContext.From(base.Dispatcher).VerifyWriteAccess();
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x000CAA0C File Offset: 0x000C9E0C
		internal void VerifyAPIReadWrite(DependencyObject other)
		{
			this.VerifyAPIReadWrite();
			if (other != null)
			{
				MediaSystem.AssertSameContext(this, other);
			}
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x000CAA2C File Offset: 0x000C9E2C
		internal void SetParent(Visual newParent)
		{
			Visual3D._2DParent.SetValue(this, newParent);
			this._3DParent = null;
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x000CAA4C File Offset: 0x000C9E4C
		internal void SetParent(Visual3D newParent)
		{
			Visual3D._2DParent.ClearValue(this);
			this._3DParent = newParent;
		}

		/// <summary>Obtém o número de elementos filho para o objeto <see cref="T:System.Windows.Media.Media3D.Visual3D" />.</summary>
		/// <returns>O número de elementos filho.</returns>
		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x060032DD RID: 13021 RVA: 0x000CAA6C File Offset: 0x000C9E6C
		protected virtual int Visual3DChildrenCount
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Retorna o <see cref="T:System.Windows.Media.Media3D.Visual3D" /> especificado no <see cref="T:System.Windows.Media.Media3D.Visual3DCollection" /> pai.</summary>
		/// <param name="index">O índice do objeto visual 3D na coleção.</param>
		/// <returns>O filho na coleção no valor de <paramref name="index" /> especificado.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">O valor <paramref name="index" /> não é válido.</exception>
		// Token: 0x060032DE RID: 13022 RVA: 0x000CAA7C File Offset: 0x000C9E7C
		protected virtual Visual3D GetVisual3DChild(int index)
		{
			throw new ArgumentOutOfRangeException("index", index, SR.Get("Visual_ArgumentOutOfRange"));
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x000CAAA4 File Offset: 0x000C9EA4
		void IVisual3DContainer.AddChild(Visual3D child)
		{
			this.AddVisual3DChild(child);
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x000CAAB8 File Offset: 0x000C9EB8
		void IVisual3DContainer.RemoveChild(Visual3D child)
		{
			this.RemoveVisual3DChild(child);
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x000CAACC File Offset: 0x000C9ECC
		int IVisual3DContainer.GetChildrenCount()
		{
			return this.Visual3DChildrenCount;
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x000CAAE0 File Offset: 0x000C9EE0
		Visual3D IVisual3DContainer.GetChild(int index)
		{
			return this.GetVisual3DChild(index);
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x000CAAF4 File Offset: 0x000C9EF4
		internal virtual void InvalidateForceInheritPropertyOnChildren(DependencyProperty property)
		{
			UIElement3D.InvalidateForceInheritPropertyOnChildren(this, property);
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x000CAB08 File Offset: 0x000C9F08
		[Conditional("DEBUG")]
		internal void Debug_VerifyBoundsEqual(Rect3D bounds1, Rect3D bounds2, string errorString)
		{
			bool flag = bounds1.X >= bounds2.X && bounds1.X <= bounds2.X && bounds1.Y >= bounds2.Y && bounds1.Y <= bounds2.Y && bounds1.Z >= bounds2.Z && bounds1.Z <= bounds2.Z && bounds1.SizeX >= bounds2.SizeX && bounds1.SizeX <= bounds2.SizeX && bounds1.SizeY >= bounds2.SizeY && bounds1.SizeY <= bounds2.SizeY && bounds1.SizeZ >= bounds2.SizeZ && bounds1.SizeZ <= bounds2.SizeZ;
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x000CABF0 File Offset: 0x000C9FF0
		[Conditional("DEBUG")]
		internal void Debug_VerifyCachedSubgraphBounds()
		{
			Rect3D empty = Rect3D.Empty;
			Rect3D rect3D = M3DUtil.ComputeTransformedAxisAlignedBoundingBox(ref this._bboxSubgraph, this.Transform);
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x000CAC18 File Offset: 0x000CA018
		[Conditional("DEBUG")]
		internal void Debug_VerifyCachedContentBounds()
		{
			Model3D visual3DModel = this._visual3DModel;
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x000CAC2C File Offset: 0x000CA02C
		internal void PrecomputeRecursive(out Rect3D bboxSubgraph)
		{
			if (this.CheckFlagsAnd(VisualFlags.IsSubtreeDirtyForPrecompute))
			{
				this._bboxSubgraph = this.GetContentBounds();
				int i = 0;
				int visual3DChildrenCount = this.Visual3DChildrenCount;
				while (i < visual3DChildrenCount)
				{
					Visual3D visual3DChild = this.GetVisual3DChild(i);
					Rect3D rect;
					visual3DChild.PrecomputeRecursive(out rect);
					this._bboxSubgraph.Union(rect);
					i++;
				}
				this.SetFlags(false, VisualFlags.IsSubtreeDirtyForPrecompute);
			}
			bboxSubgraph = M3DUtil.ComputeTransformedAxisAlignedBoundingBox(ref this._bboxSubgraph, this.Transform);
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x000CAC9C File Offset: 0x000CA09C
		internal void RenderRecursive(RenderContext ctx)
		{
			DUCE.Channel channel = ctx.Channel;
			DUCE.ResourceHandle hCompositionNode = DUCE.ResourceHandle.Null;
			VisualProxyFlags visualProxyFlags = VisualProxyFlags.IsSubtreeDirtyForRender | VisualProxyFlags.IsTransformDirty | VisualProxyFlags.IsContentDirty;
			bool flag = this.IsOnChannel(channel);
			if (flag)
			{
				hCompositionNode = this._proxy.GetHandle(channel);
				visualProxyFlags = this._proxy.GetFlags(channel);
			}
			else
			{
				hCompositionNode = ((DUCE.IResource)this).AddRefOnChannel(channel);
			}
			if ((visualProxyFlags & VisualProxyFlags.IsContentDirty) != VisualProxyFlags.None)
			{
				this.RenderContent(ctx, flag);
			}
			if ((visualProxyFlags & VisualProxyFlags.IsTransformDirty) != VisualProxyFlags.None)
			{
				Transform3D transform = this.Transform;
				if (transform != null && this.InternalIsVisible)
				{
					DUCE.Visual3DNode.SetTransform(hCompositionNode, ((DUCE.IResource)transform).AddRefOnChannel(channel), channel);
				}
				else if (!this.InternalIsVisible)
				{
					DUCE.Visual3DNode.SetTransform(hCompositionNode, ((DUCE.IResource)Visual3D._zeroScale).AddRefOnChannel(channel), channel);
				}
				else if (!flag)
				{
					DUCE.Visual3DNode.SetTransform(hCompositionNode, DUCE.ResourceHandle.Null, channel);
				}
			}
			for (int i = 0; i < this.Visual3DChildrenCount; i++)
			{
				Visual3D visual3DChild = this.GetVisual3DChild(i);
				if (visual3DChild != null)
				{
					if (visual3DChild.CheckFlagsAnd(channel, VisualProxyFlags.IsSubtreeDirtyForRender) || !visual3DChild.IsOnChannel(channel))
					{
						visual3DChild.RenderRecursive(ctx);
					}
					if (visual3DChild.IsOnChannel(ctx.Channel) && !visual3DChild.CheckFlagsAnd(channel, VisualProxyFlags.IsConnectedToParent))
					{
						DUCE.Visual3DNode.InsertChildAt(hCompositionNode, ((DUCE.IResource)visual3DChild).GetHandle(channel), (uint)i, ctx.Channel);
						visual3DChild.SetFlags(channel, true, VisualProxyFlags.IsConnectedToParent);
					}
				}
			}
			this.SetFlags(channel, false, VisualProxyFlags.IsSubtreeDirtyForRender | VisualProxyFlags.IsTransformDirty | VisualProxyFlags.IsContentDirty);
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x000CADD0 File Offset: 0x000CA1D0
		internal void RenderContent(RenderContext ctx, bool isOnChannel)
		{
			DUCE.Channel channel = ctx.Channel;
			if (this._visual3DModel != null)
			{
				DUCE.Visual3DNode.SetContent(((DUCE.IResource)this).GetHandle(channel), ((DUCE.IResource)this._visual3DModel).AddRefOnChannel(channel), channel);
				this.SetFlags(channel, true, VisualProxyFlags.IsContentConnected);
				return;
			}
			if (isOnChannel)
			{
				DUCE.Visual3DNode.SetContent(((DUCE.IResource)this).GetHandle(channel), DUCE.ResourceHandle.Null, channel);
			}
		}

		/// <summary>Determina se o objeto visual é um ancestral do objeto visual descendente.</summary>
		/// <param name="descendant">Um objeto visual que é um possível descendente.</param>
		/// <returns>
		///   <see langword="true" /> se o objeto visual for um ancestral de <paramref name="descendant" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060032EA RID: 13034 RVA: 0x000CAE28 File Offset: 0x000CA228
		public bool IsAncestorOf(DependencyObject descendant)
		{
			Visual visual;
			Visual3D visual3D;
			VisualTreeUtils.AsNonNullVisual(descendant, out visual, out visual3D);
			if (visual != null)
			{
				return visual.IsDescendantOf(this);
			}
			return visual3D.IsDescendantOf(this);
		}

		/// <summary>Determina se o objeto visual é um descendente do objeto visual ancestral.</summary>
		/// <param name="ancestor">Um objeto visual que é um possível ancestral.</param>
		/// <returns>
		///   <see langword="true" /> se o objeto visual for um descendente do <paramref name="ancestor" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060032EB RID: 13035 RVA: 0x000CAE54 File Offset: 0x000CA254
		public bool IsDescendantOf(DependencyObject ancestor)
		{
			if (ancestor == null)
			{
				throw new ArgumentNullException("ancestor");
			}
			VisualTreeUtils.EnsureVisual(ancestor);
			Visual3D visual3D = this;
			while (visual3D != null && visual3D != ancestor)
			{
				if (visual3D._3DParent == null)
				{
					DependencyObject internalVisualParent = visual3D.InternalVisualParent;
					if (internalVisualParent != null)
					{
						return ((Visual)internalVisualParent).IsDescendantOf(ancestor);
					}
				}
				visual3D = visual3D._3DParent;
			}
			return visual3D == ancestor;
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x000CAEAC File Offset: 0x000CA2AC
		internal void SetFlagsToRoot(bool value, VisualFlags flag)
		{
			Visual3D visual3D = this;
			for (;;)
			{
				visual3D.SetFlags(value, flag);
				if (visual3D._3DParent == null)
				{
					break;
				}
				visual3D = visual3D._3DParent;
				if (visual3D == null)
				{
					return;
				}
			}
			VisualTreeUtils.SetFlagsToRoot(this.InternalVisualParent, value, flag);
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x000CAEE4 File Offset: 0x000CA2E4
		internal DependencyObject FindFirstAncestorWithFlagsAnd(VisualFlags flag)
		{
			Visual3D visual3D = this;
			while (!visual3D.CheckFlagsAnd(flag))
			{
				if (visual3D._3DParent == null)
				{
					return VisualTreeUtils.FindFirstAncestorWithFlagsAnd(this.InternalVisualParent, flag);
				}
				visual3D = visual3D._3DParent;
				if (visual3D == null)
				{
					return null;
				}
			}
			return visual3D;
		}

		/// <summary>Retorna o ancestral comum do objeto visual e outro objeto visual especificado.</summary>
		/// <param name="otherVisual">O objeto visual com o qual encontrar um ancestral em comum.</param>
		/// <returns>O ancestral comum do objeto visual atual e <paramref name="otherVisual" />; ou <see langword="null" />, se nenhum ancestral comum for encontrado.</returns>
		// Token: 0x060032EE RID: 13038 RVA: 0x000CAF20 File Offset: 0x000CA320
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

		// Token: 0x060032EF RID: 13039 RVA: 0x000CAF5C File Offset: 0x000CA35C
		internal void FreeDUCEResources(DUCE.Channel channel)
		{
			Transform3D transform = this.Transform;
			if (!this.CheckFlagsAnd(channel, VisualProxyFlags.IsTransformDirty))
			{
				if (this.InternalIsVisible)
				{
					if (transform != null)
					{
						((DUCE.IResource)transform).ReleaseOnChannel(channel);
					}
				}
				else
				{
					((DUCE.IResource)Visual3D._zeroScale).ReleaseOnChannel(channel);
				}
			}
			Model3D visual3DModel = this._visual3DModel;
			if (visual3DModel != null && !this.CheckFlagsAnd(channel, VisualProxyFlags.IsContentDirty))
			{
				((DUCE.IResource)visual3DModel).ReleaseOnChannel(channel);
			}
			this._proxy.ReleaseOnChannel(channel);
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x000CAFC4 File Offset: 0x000CA3C4
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			this.ReleaseOnChannelCore(channel);
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x000CAFD8 File Offset: 0x000CA3D8
		internal void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (!this.IsOnChannel(channel))
			{
				return;
			}
			this.SetFlags(channel, false, VisualProxyFlags.IsConnectedToParent);
			this.FreeDUCEResources(channel);
			for (int i = 0; i < this.Visual3DChildrenCount; i++)
			{
				Visual3D visual3DChild = this.GetVisual3DChild(i);
				((DUCE.IResource)visual3DChild).ReleaseOnChannel(channel);
			}
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x000CB024 File Offset: 0x000CA424
		internal void DisconnectAttachedResource(VisualProxyFlags correspondingFlag, DUCE.IResource attachedResource)
		{
			for (int i = 0; i < this._proxy.Count; i++)
			{
				VisualProxyFlags flags = this._proxy.GetFlags(i);
				if ((flags & correspondingFlag) == VisualProxyFlags.None)
				{
					DUCE.Channel channel = this._proxy.GetChannel(i);
					this.SetFlags(channel, true, correspondingFlag);
					if (correspondingFlag == VisualProxyFlags.IsContentDirty)
					{
						this._proxy.SetFlags(i, false, VisualProxyFlags.IsContentConnected);
					}
					attachedResource.ReleaseOnChannel(channel);
				}
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x060032F3 RID: 13043 RVA: 0x000CB08C File Offset: 0x000CA48C
		internal override DependencyObject InheritanceContext
		{
			get
			{
				DependencyObject value = Visual3D._inheritanceContext.GetValue(this);
				if (value == Visual3D.UseParentAsContext)
				{
					return this.InternalVisualParent;
				}
				return value;
			}
		}

		// Token: 0x060032F4 RID: 13044 RVA: 0x000CB0B8 File Offset: 0x000CA4B8
		internal override void AddInheritanceContext(DependencyObject context, DependencyProperty property)
		{
			base.AddInheritanceContext(context, property);
			this.AddOrRemoveInheritanceContext(context);
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x000CB0D4 File Offset: 0x000CA4D4
		internal override void RemoveInheritanceContext(DependencyObject context, DependencyProperty property)
		{
			base.RemoveInheritanceContext(context, property);
			this.AddOrRemoveInheritanceContext(null);
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x000CB0F0 File Offset: 0x000CA4F0
		private void AddOrRemoveInheritanceContext(DependencyObject newInheritanceContext)
		{
			bool flag = this.InheritanceContext != newInheritanceContext || (Visual3D._inheritanceContext.GetValue(this) == Visual3D.UseParentAsContext && newInheritanceContext == this.InternalVisualParent);
			if (flag)
			{
				this.SetInheritanceContext(newInheritanceContext);
				base.OnInheritanceContextChanged(EventArgs.Empty);
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x060032F7 RID: 13047 RVA: 0x000CB140 File Offset: 0x000CA540
		internal override bool HasMultipleInheritanceContexts
		{
			get
			{
				return base.HasMultipleInheritanceContexts;
			}
		}

		// Token: 0x060032F8 RID: 13048 RVA: 0x000CB154 File Offset: 0x000CA554
		internal override void OnInheritanceContextChangedCore(EventArgs args)
		{
			base.OnInheritanceContextChangedCore(args);
			for (int i = 0; i < this.Visual3DChildrenCount; i++)
			{
				DependencyObject visual3DChild = this.GetVisual3DChild(i);
				visual3DChild.OnInheritanceContextChanged(args);
			}
		}

		/// <summary>Retorna uma transformação que pode ser usada para transformar as coordenadas deste objeto <see cref="T:System.Windows.Media.Media3D.Visual3D" /> no ancestral <see cref="T:System.Windows.Media.Media3D.Visual3D" /> especificado do objeto.</summary>
		/// <param name="ancestor">O <see cref="T:System.Windows.Media.Media3D.Visual3D" /> para o qual as coordenadas são transformadas.</param>
		/// <returns>Um objeto <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" />; ou <see langword="null" />, se a transformação não puder ser criada.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ancestor" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">O objeto <paramref name="ancestor" /> especificado não é um ancestral deste objeto.</exception>
		/// <exception cref="T:System.InvalidOperationException">Os objetos <see cref="T:System.Windows.Media.Media3D.Visual3D" /> não estão relacionados.</exception>
		// Token: 0x060032F9 RID: 13049 RVA: 0x000CB188 File Offset: 0x000CA588
		public GeneralTransform3D TransformToAncestor(Visual3D ancestor)
		{
			if (ancestor == null)
			{
				throw new ArgumentNullException("ancestor");
			}
			this.VerifyAPIReadOnly(ancestor);
			return this.InternalTransformToAncestor(ancestor, false);
		}

		/// <summary>Retorna uma transformação que pode ser usada para transformar as coordenadas deste objeto <see cref="T:System.Windows.Media.Media3D.Visual3D" /> no objeto descendente <see cref="T:System.Windows.Media.Media3D.Visual3D" /> especificado.</summary>
		/// <param name="descendant">O <see cref="T:System.Windows.Media.Media3D.Visual3D" /> para o qual as coordenadas são transformadas.</param>
		/// <returns>Um objeto <see cref="T:System.Windows.Media.Media3D.GeneralTransform3D" />; ou <see langword="null" />, se a transformação de <paramref name="descendant" /> a este objeto não puder ser invertida.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="descendant" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">Esse objeto não é um ancestral do objeto <paramref name="descendant" /> especificado.</exception>
		/// <exception cref="T:System.InvalidOperationException">Os objetos <see cref="T:System.Windows.Media.Media3D.Visual3D" /> não estão relacionados.</exception>
		// Token: 0x060032FA RID: 13050 RVA: 0x000CB1B4 File Offset: 0x000CA5B4
		public GeneralTransform3D TransformToDescendant(Visual3D descendant)
		{
			if (descendant == null)
			{
				throw new ArgumentNullException("descendant");
			}
			this.VerifyAPIReadOnly(descendant);
			return descendant.InternalTransformToAncestor(this, true);
		}

		// Token: 0x060032FB RID: 13051 RVA: 0x000CB1E0 File Offset: 0x000CA5E0
		private GeneralTransform3D InternalTransformToAncestor(Visual3D ancestor, bool inverse)
		{
			bool flag = true;
			DependencyObject dependencyObject = this;
			Visual3D visual3D = null;
			Matrix3D identity = Matrix3D.Identity;
			GeneralTransform3DGroup generalTransform3DGroup = null;
			while (VisualTreeHelper.GetParent(dependencyObject) != null && dependencyObject != ancestor)
			{
				Visual3D visual3D2 = dependencyObject as Visual3D;
				if (visual3D2 != null)
				{
					Transform3D transform = visual3D2.Transform;
					if (transform != null)
					{
						transform.Append(ref identity);
					}
					visual3D = visual3D2;
					dependencyObject = VisualTreeHelper.GetParent(visual3D2);
				}
				else
				{
					if (generalTransform3DGroup == null)
					{
						generalTransform3DGroup = new GeneralTransform3DGroup();
					}
					generalTransform3DGroup.Children.Add(new MatrixTransform3D(identity));
					identity = Matrix3D.Identity;
					Visual visual = dependencyObject as Visual;
					GeneralTransform3DTo2D generalTransform3DTo2D = visual3D.TransformToAncestor(visual);
					Visual3D containingVisual3D = VisualTreeHelper.GetContainingVisual3D(visual);
					if (containingVisual3D == null)
					{
						break;
					}
					GeneralTransform2DTo3D generalTransform2DTo3D = visual.TransformToAncestor(containingVisual3D);
					if (generalTransform3DTo2D == null || generalTransform2DTo3D == null)
					{
						flag = false;
					}
					else
					{
						generalTransform3DGroup.Children.Add(new GeneralTransform3DTo2DTo3D(generalTransform3DTo2D, generalTransform2DTo3D));
					}
					dependencyObject = containingVisual3D;
				}
			}
			if (dependencyObject != ancestor)
			{
				throw new InvalidOperationException(SR.Get(inverse ? "Visual_NotADescendant" : "Visual_NotAnAncestor"));
			}
			GeneralTransform3D generalTransform3D = null;
			if (flag)
			{
				if (generalTransform3DGroup != null)
				{
					generalTransform3D = generalTransform3DGroup;
				}
				else
				{
					generalTransform3D = new MatrixTransform3D(identity);
				}
				if (inverse)
				{
					generalTransform3D = generalTransform3D.Inverse;
				}
			}
			if (generalTransform3D != null)
			{
				generalTransform3D.Freeze();
			}
			return generalTransform3D;
		}

		/// <summary>Retorna uma transformação que pode ser usada para transformar as coordenadas deste objeto <see cref="T:System.Windows.Media.Media3D.Visual3D" /> no ancestral <see cref="T:System.Windows.Media.Visual" /> especificado do objeto.</summary>
		/// <param name="ancestor">O <see cref="T:System.Windows.Media.Visual" /> para o qual as coordenadas são transformadas.</param>
		/// <returns>Um objeto <see cref="T:System.Windows.Media.Media3D.GeneralTransform3DTo2D" />; ou <see langword="null" />, se a transformação não puder ser criada.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ancestor" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">O objeto <paramref name="ancestor" /> especificado não é um ancestral deste objeto.</exception>
		// Token: 0x060032FC RID: 13052 RVA: 0x000CB2F8 File Offset: 0x000CA6F8
		public GeneralTransform3DTo2D TransformToAncestor(Visual ancestor)
		{
			if (ancestor == null)
			{
				throw new ArgumentNullException("ancestor");
			}
			this.VerifyAPIReadOnly(ancestor);
			return this.InternalTransformToAncestor(ancestor);
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x000CB324 File Offset: 0x000CA724
		internal GeneralTransform3DTo2D InternalTransformToAncestor(Visual ancestor)
		{
			Viewport3DVisual viewport3DVisual;
			Matrix3D projectionTransform;
			if (!M3DUtil.TryTransformToViewport3DVisual(this, out viewport3DVisual, out projectionTransform))
			{
				return null;
			}
			GeneralTransform transformBetween2D = viewport3DVisual.TransformToAncestor(ancestor);
			GeneralTransform3DTo2D generalTransform3DTo2D = new GeneralTransform3DTo2D(projectionTransform, transformBetween2D);
			generalTransform3DTo2D.Freeze();
			return generalTransform3DTo2D;
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x060032FE RID: 13054 RVA: 0x000CB358 File Offset: 0x000CA758
		internal DependencyObject InternalVisualParent
		{
			get
			{
				if (this._3DParent != null)
				{
					return this._3DParent;
				}
				return Visual3D._2DParent.GetValue(this);
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x060032FF RID: 13055 RVA: 0x000CB384 File Offset: 0x000CA784
		// (set) Token: 0x06003300 RID: 13056 RVA: 0x000CB398 File Offset: 0x000CA798
		internal int ParentIndex
		{
			get
			{
				return this._parentIndex;
			}
			set
			{
				this._parentIndex = value;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06003301 RID: 13057 RVA: 0x000CB3AC File Offset: 0x000CA7AC
		// (set) Token: 0x06003302 RID: 13058 RVA: 0x000CB3C4 File Offset: 0x000CA7C4
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

		// Token: 0x06003303 RID: 13059 RVA: 0x000CB3DC File Offset: 0x000CA7DC
		internal void SetFlagsOnAllChannels(bool value, VisualProxyFlags flagsToChange)
		{
			this._proxy.SetFlagsOnAllChannels(value, flagsToChange);
		}

		// Token: 0x06003304 RID: 13060 RVA: 0x000CB3F8 File Offset: 0x000CA7F8
		internal void SetFlags(DUCE.Channel channel, bool value, VisualProxyFlags flagsToChange)
		{
			this._proxy.SetFlags(channel, value, flagsToChange);
		}

		// Token: 0x06003305 RID: 13061 RVA: 0x000CB414 File Offset: 0x000CA814
		internal void SetFlags(bool value, VisualFlags Flags)
		{
			this._flags = (value ? (this._flags | Flags) : (this._flags & ~Flags));
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x000CB440 File Offset: 0x000CA840
		internal bool CheckFlagsOnAllChannels(VisualProxyFlags flagsToCheck)
		{
			return this._proxy.CheckFlagsOnAllChannels(flagsToCheck);
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x000CB45C File Offset: 0x000CA85C
		internal bool CheckFlagsAnd(DUCE.Channel channel, VisualProxyFlags flagsToCheck)
		{
			return (this._proxy.GetFlags(channel) & flagsToCheck) == flagsToCheck;
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x000CB47C File Offset: 0x000CA87C
		internal bool CheckFlagsAnd(VisualFlags flags)
		{
			return (this._flags & flags) == flags;
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x000CB494 File Offset: 0x000CA894
		internal virtual DependencyObject InternalGet2DOr3DVisualChild(int index)
		{
			return this.GetVisual3DChild(index);
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x0600330A RID: 13066 RVA: 0x000CB4A8 File Offset: 0x000CA8A8
		internal virtual int InternalVisual2DOr3DChildrenCount
		{
			get
			{
				return this.Visual3DChildrenCount;
			}
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x000CB4BC File Offset: 0x000CA8BC
		internal bool CheckFlagsOr(DUCE.Channel channel, VisualProxyFlags flagsToCheck)
		{
			return (this._proxy.GetFlags(channel) & flagsToCheck) > VisualProxyFlags.None;
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x000CB4DC File Offset: 0x000CA8DC
		internal bool CheckFlagsOr(VisualFlags flags)
		{
			return flags == VisualFlags.None || (this._flags & flags) > VisualFlags.None;
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x000CB4FC File Offset: 0x000CA8FC
		internal static bool DoAnyChildrenHaveABitSet(Visual3D pe, VisualFlags flag)
		{
			int internalVisual2DOr3DChildrenCount = pe.InternalVisual2DOr3DChildrenCount;
			for (int i = 0; i < internalVisual2DOr3DChildrenCount; i++)
			{
				DependencyObject element = pe.InternalGet2DOr3DVisualChild(i);
				Visual visual = null;
				Visual3D visual3D = null;
				VisualTreeUtils.AsNonNullVisual(element, out visual, out visual3D);
				if (visual != null && visual.CheckFlagsAnd(flag))
				{
					return true;
				}
				if (visual3D != null && visual3D.CheckFlagsAnd(flag))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x000CB554 File Offset: 0x000CA954
		internal static void PropagateFlags(Visual3D e, VisualFlags flags, VisualProxyFlags proxyFlags)
		{
			while (e != null && (!e.CheckFlagsAnd(flags) || !e.CheckFlagsOnAllChannels(proxyFlags)))
			{
				e.SetFlags(true, flags);
				e.SetFlagsOnAllChannels(true, proxyFlags);
				if (e._3DParent == null)
				{
					Viewport3DVisual viewport3DVisual = e.InternalVisualParent as Viewport3DVisual;
					if (viewport3DVisual != null)
					{
						viewport3DVisual.Visual3DTreeChanged();
						Visual.PropagateFlags(viewport3DVisual, flags, proxyFlags);
					}
					return;
				}
				e = e._3DParent;
			}
		}

		// Token: 0x0600330F RID: 13071 RVA: 0x000CB5B8 File Offset: 0x000CA9B8
		private void SetInheritanceContext(DependencyObject newInheritanceContext)
		{
			if (newInheritanceContext == this.InternalVisualParent)
			{
				Visual3D._inheritanceContext.ClearValue(this);
				return;
			}
			Visual3D._inheritanceContext.SetValue(this, newInheritanceContext);
		}

		/// <summary>Aplica o efeito de um determinado <see cref="T:System.Windows.Media.Animation.AnimationClock" /> a uma propriedade de dependência.</summary>
		/// <param name="dp">O <see cref="T:System.Windows.DependencyProperty" /> a ser animado.</param>
		/// <param name="clock">O <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que anima a propriedade.</param>
		// Token: 0x06003310 RID: 13072 RVA: 0x000CB5E8 File Offset: 0x000CA9E8
		public void ApplyAnimationClock(DependencyProperty dp, AnimationClock clock)
		{
			this.ApplyAnimationClock(dp, clock, HandoffBehavior.SnapshotAndReplace);
		}

		/// <summary>Aplica o efeito de um determinado <see cref="T:System.Windows.Media.Animation.AnimationClock" /> a uma propriedade de dependência. O efeito do novo <see cref="T:System.Windows.Media.Animation.AnimationClock" /> em quaisquer animações atuais é determinado pelo valor do parâmetro <paramref name="handoffBehavior" />.</summary>
		/// <param name="dp">O <see cref="T:System.Windows.DependencyProperty" /> a ser animado.</param>
		/// <param name="clock">O <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que anima a propriedade.</param>
		/// <param name="handoffBehavior">O objeto que especifica como interagir com todas as sequências de animação relevantes.</param>
		// Token: 0x06003311 RID: 13073 RVA: 0x000CB600 File Offset: 0x000CAA00
		public void ApplyAnimationClock(DependencyProperty dp, AnimationClock clock, HandoffBehavior handoffBehavior)
		{
			if (dp == null)
			{
				throw new ArgumentNullException("dp");
			}
			if (!AnimationStorage.IsPropertyAnimatable(this, dp))
			{
				throw new ArgumentException(SR.Get("Animation_DependencyPropertyIsNotAnimatable", new object[]
				{
					dp.Name,
					base.GetType()
				}), "dp");
			}
			if (clock != null && !AnimationStorage.IsAnimationValid(dp, clock.Timeline))
			{
				throw new ArgumentException(SR.Get("Animation_AnimationTimelineTypeMismatch", new object[]
				{
					clock.Timeline.GetType(),
					dp.Name,
					dp.PropertyType
				}), "clock");
			}
			if (!HandoffBehaviorEnum.IsDefined(handoffBehavior))
			{
				throw new ArgumentException(SR.Get("Animation_UnrecognizedHandoffBehavior"));
			}
			if (base.IsSealed)
			{
				throw new InvalidOperationException(SR.Get("IAnimatable_CantAnimateSealedDO", new object[]
				{
					dp,
					base.GetType()
				}));
			}
			AnimationStorage.ApplyAnimationClock(this, dp, clock, handoffBehavior);
		}

		/// <summary>Inicia uma sequência de animação para o objeto <see cref="T:System.Windows.DependencyProperty" /> com base no <see cref="T:System.Windows.Media.Animation.AnimationTimeline" /> especificado.</summary>
		/// <param name="dp">O objeto <see cref="T:System.Windows.DependencyProperty" /> a ser animado.</param>
		/// <param name="animation">A linha do tempo que tem a funcionalidade necessária para animar a propriedade.</param>
		// Token: 0x06003312 RID: 13074 RVA: 0x000CB6EC File Offset: 0x000CAAEC
		public void BeginAnimation(DependencyProperty dp, AnimationTimeline animation)
		{
			this.BeginAnimation(dp, animation, HandoffBehavior.SnapshotAndReplace);
		}

		/// <summary>Inicia uma sequência de animação para o objeto <see cref="T:System.Windows.DependencyProperty" /> com base no <see cref="T:System.Windows.Media.Animation.AnimationTimeline" /> e no <see cref="T:System.Windows.Media.Animation.HandoffBehavior" /> especificados.</summary>
		/// <param name="dp">O objeto <see cref="T:System.Windows.DependencyProperty" /> a ser animado.</param>
		/// <param name="animation">A linha de tempo que tem a funcionalidade necessária para personalizar a nova animação.</param>
		/// <param name="handoffBehavior">O objeto que especifica como interagir com todas as sequências de animação relevantes.</param>
		// Token: 0x06003313 RID: 13075 RVA: 0x000CB704 File Offset: 0x000CAB04
		public void BeginAnimation(DependencyProperty dp, AnimationTimeline animation, HandoffBehavior handoffBehavior)
		{
			if (dp == null)
			{
				throw new ArgumentNullException("dp");
			}
			if (!AnimationStorage.IsPropertyAnimatable(this, dp))
			{
				throw new ArgumentException(SR.Get("Animation_DependencyPropertyIsNotAnimatable", new object[]
				{
					dp.Name,
					base.GetType()
				}), "dp");
			}
			if (animation != null && !AnimationStorage.IsAnimationValid(dp, animation))
			{
				throw new ArgumentException(SR.Get("Animation_AnimationTimelineTypeMismatch", new object[]
				{
					animation.GetType(),
					dp.Name,
					dp.PropertyType
				}), "animation");
			}
			if (!HandoffBehaviorEnum.IsDefined(handoffBehavior))
			{
				throw new ArgumentException(SR.Get("Animation_UnrecognizedHandoffBehavior"));
			}
			if (base.IsSealed)
			{
				throw new InvalidOperationException(SR.Get("IAnimatable_CantAnimateSealedDO", new object[]
				{
					dp,
					base.GetType()
				}));
			}
			AnimationStorage.BeginAnimation(this, dp, animation, handoffBehavior);
		}

		/// <summary>Obtém um valor que indica se esse <see cref="T:System.Windows.Media.Media3D.Visual3D" /> tem propriedades animadas.</summary>
		/// <returns>
		///   <see langword="true" /> Se este elemento tem animações; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06003314 RID: 13076 RVA: 0x000CB7E4 File Offset: 0x000CABE4
		public bool HasAnimatedProperties
		{
			get
			{
				base.VerifyAccess();
				return base.IAnimatable_HasAnimatedProperties;
			}
		}

		/// <summary>Recupera o valor base do objeto <see cref="T:System.Windows.DependencyProperty" /> especificado.</summary>
		/// <param name="dp">O objeto para o qual o valor base está sendo solicitado.</param>
		/// <returns>O objeto que representa o valor base de <paramref name="dp" />.</returns>
		// Token: 0x06003315 RID: 13077 RVA: 0x000CB800 File Offset: 0x000CAC00
		public object GetAnimationBaseValue(DependencyProperty dp)
		{
			if (dp == null)
			{
				throw new ArgumentNullException("dp");
			}
			return base.GetValueEntry(base.LookupEntry(dp.GlobalIndex), dp, null, RequestFlags.AnimationBaseValue).Value;
		}

		// Token: 0x06003316 RID: 13078 RVA: 0x000CB838 File Offset: 0x000CAC38
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		internal sealed override void EvaluateAnimatedValueCore(DependencyProperty dp, PropertyMetadata metadata, ref EffectiveValueEntry entry)
		{
			if (base.IAnimatable_HasAnimatedProperties)
			{
				AnimationStorage storage = AnimationStorage.GetStorage(this, dp);
				if (storage != null)
				{
					storage.EvaluateAnimatedValue(metadata, ref entry);
				}
			}
		}

		// Token: 0x040015DC RID: 5596
		private const VisualProxyFlags c_Model3DVisualProxyFlagsDirtyMask = VisualProxyFlags.IsSubtreeDirtyForRender | VisualProxyFlags.IsTransformDirty | VisualProxyFlags.IsContentDirty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.Visual3D.Transform" />.</summary>
		// Token: 0x040015DD RID: 5597
		public static readonly DependencyProperty TransformProperty = DependencyProperty.Register("Transform", typeof(Transform3D), typeof(Visual3D), new PropertyMetadata(Transform3D.Identity, new PropertyChangedCallback(Visual3D.TransformPropertyChanged)), (object <p0>) => MediaContext.CurrentMediaContext.WriteAccessEnabled);

		// Token: 0x040015DE RID: 5598
		internal VisualProxy _proxy;

		// Token: 0x040015DF RID: 5599
		private static readonly UncommonField<Visual> _2DParent = new UncommonField<Visual>(null);

		// Token: 0x040015E0 RID: 5600
		private static readonly DependencyObject UseParentAsContext = new DependencyObject();

		// Token: 0x040015E1 RID: 5601
		private static readonly UncommonField<DependencyObject> _inheritanceContext = new UncommonField<DependencyObject>(Visual3D.UseParentAsContext);

		// Token: 0x040015E2 RID: 5602
		private static readonly UncommonField<Visual.AncestorChangedEventHandler> AncestorChangedEventField = new UncommonField<Visual.AncestorChangedEventHandler>();

		// Token: 0x040015E3 RID: 5603
		private Visual3D _3DParent;

		// Token: 0x040015E4 RID: 5604
		private int _parentIndex = -1;

		// Token: 0x040015E5 RID: 5605
		private VisualFlags _flags;

		// Token: 0x040015E6 RID: 5606
		private Rect3D _bboxContent;

		// Token: 0x040015E7 RID: 5607
		private Rect3D _bboxSubgraph = Rect3D.Empty;

		// Token: 0x040015E8 RID: 5608
		private bool _internalIsVisible;

		// Token: 0x040015E9 RID: 5609
		private static readonly ScaleTransform3D _zeroScale = new ScaleTransform3D(0.0, 0.0, 0.0);

		// Token: 0x040015EA RID: 5610
		private Model3D _visual3DModel;
	}
}
