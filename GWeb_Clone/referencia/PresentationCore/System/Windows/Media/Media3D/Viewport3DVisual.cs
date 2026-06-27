using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Diagnostics;
using System.Windows.Markup;
using System.Windows.Media.Composition;
using System.Windows.Media.Effects;
using MS.Internal;
using MS.Internal.Media3D;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Media3D
{
	/// <summary>Renderiza os filhos <see cref="T:System.Windows.Media.Media3D.Visual3D" /> nos limites do visor 2D especificado.</summary>
	// Token: 0x02000486 RID: 1158
	[ContentProperty("Children")]
	public sealed class Viewport3DVisual : Visual, DUCE.IResource, IVisual3DContainer
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.Viewport3DVisual" />.</summary>
		// Token: 0x06003274 RID: 12916 RVA: 0x000C957C File Offset: 0x000C897C
		public Viewport3DVisual() : base(DUCE.ResourceType.TYPE_VIEWPORT3DVISUAL)
		{
			this._children = new Visual3DCollection(this);
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Media.Visual" /> pai do Viewport3DVisual.</summary>
		/// <returns>Visual pai do Viewport3DVisual.</returns>
		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06003275 RID: 12917 RVA: 0x000C95A0 File Offset: 0x000C89A0
		public DependencyObject Parent
		{
			get
			{
				return base.VisualParent;
			}
		}

		/// <summary>Obtém ou define a área de recorte do <see cref="T:System.Windows.Media.Media3D.Viewport3DVisual" />.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Geometry" /> que define a área de recorte.</returns>
		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06003276 RID: 12918 RVA: 0x000C95B4 File Offset: 0x000C89B4
		// (set) Token: 0x06003277 RID: 12919 RVA: 0x000C95C8 File Offset: 0x000C89C8
		public Geometry Clip
		{
			get
			{
				return base.VisualClip;
			}
			set
			{
				base.VisualClip = value;
			}
		}

		/// <summary>Obtém ou define a opacidade do <see cref="T:System.Windows.Media.Media3D.Viewport3DVisual" />.</summary>
		/// <returns>O valor da propriedade de opacidade é expresso como um valor entre 0 e 1, especificando um intervalo de totalmente transparente a completamente opaco. Um valor de 0 indica que a opacidade é completamente transparente, enquanto um valor de 1 indica que a opacidade é completamente opaca. Um valor de 0,5 indicaria que a opacidade é 50% opaco, que um valor de 0.725 indicaria que a opacidade é 72.5% opaco e assim por diante. Valores menores que 0 são tratados como 0, enquanto valores maiores que 1 são tratados como 1.</returns>
		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06003278 RID: 12920 RVA: 0x000C95DC File Offset: 0x000C89DC
		// (set) Token: 0x06003279 RID: 12921 RVA: 0x000C95F0 File Offset: 0x000C89F0
		public double Opacity
		{
			get
			{
				return base.VisualOpacity;
			}
			set
			{
				base.VisualOpacity = value;
			}
		}

		/// <summary>Obtém ou define o valor da máscara de opacidade do <see cref="T:System.Windows.Media.Media3D.Viewport3DVisual" />.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Brush" /> que representa o valor de máscara de opacidade do Viewport3DVisual.</returns>
		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x0600327A RID: 12922 RVA: 0x000C9604 File Offset: 0x000C8A04
		// (set) Token: 0x0600327B RID: 12923 RVA: 0x000C9618 File Offset: 0x000C8A18
		public Brush OpacityMask
		{
			get
			{
				return base.VisualOpacityMask;
			}
			set
			{
				base.VisualOpacityMask = value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> aplicado ao Viewport3DVisual.</summary>
		/// <returns>O efeito de bitmap aplicado para o Viewport3DVisual.</returns>
		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x0600327C RID: 12924 RVA: 0x000C962C File Offset: 0x000C8A2C
		// (set) Token: 0x0600327D RID: 12925 RVA: 0x000C9640 File Offset: 0x000C8A40
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		public BitmapEffect BitmapEffect
		{
			get
			{
				return base.VisualBitmapEffect;
			}
			set
			{
				base.VisualBitmapEffect = value;
			}
		}

		/// <summary>Obtém ou define a <see cref="T:System.Windows.Media.Effects.BitmapEffectInput" /> aplicada ao <see cref="T:System.Windows.Media.Media3D.Viewport3DVisual" />.</summary>
		/// <returns>BitmapEffectInput aplicada para o Viewport3DVisual.</returns>
		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x0600327E RID: 12926 RVA: 0x000C9654 File Offset: 0x000C8A54
		// (set) Token: 0x0600327F RID: 12927 RVA: 0x000C9668 File Offset: 0x000C8A68
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		public BitmapEffectInput BitmapEffectInput
		{
			get
			{
				return base.VisualBitmapEffectInput;
			}
			set
			{
				base.VisualBitmapEffectInput = value;
			}
		}

		/// <summary>Retorna o objeto visual de nível mais alto de um teste de clique realizado em um <see cref="T:System.Windows.Point" /> especificado.</summary>
		/// <param name="point">Ponto no qual o teste de clique ocorrerá.</param>
		/// <returns>O resultado do teste de clique do visual retornado como um tipo <see cref="T:System.Windows.Media.HitTestResult" />.</returns>
		// Token: 0x06003280 RID: 12928 RVA: 0x000C967C File Offset: 0x000C8A7C
		public new HitTestResult HitTest(Point point)
		{
			return base.HitTest(point);
		}

		/// <summary>Iniciar um teste de clique no <see cref="T:System.Windows.Media.Media3D.Viewport3DVisual" /> usando os objetos <see cref="T:System.Windows.Media.HitTestFilterCallback" /> e <see cref="T:System.Windows.Media.HitTestResultCallback" />.</summary>
		/// <param name="filterCallback">Valor do tipo HitTestFilterCallback.</param>
		/// <param name="resultCallback">Valor do tipo HitTestResultCallback.</param>
		/// <param name="hitTestParameters">O valor do tipo <see cref="T:System.Windows.Media.HitTestParameters" />.</param>
		// Token: 0x06003281 RID: 12929 RVA: 0x000C9690 File Offset: 0x000C8A90
		public new void HitTest(HitTestFilterCallback filterCallback, HitTestResultCallback resultCallback, HitTestParameters hitTestParameters)
		{
			base.HitTest(filterCallback, resultCallback, hitTestParameters);
		}

		/// <summary>Obtém a caixa delimitadora do conteúdo do <see cref="T:System.Windows.Media.Media3D.Viewport3DVisual" />.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Rect" /> que define a caixa delimitadora.</returns>
		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06003282 RID: 12930 RVA: 0x000C96A8 File Offset: 0x000C8AA8
		public Rect ContentBounds
		{
			get
			{
				return base.VisualContentBounds;
			}
		}

		/// <summary>Obtém ou define o valor de transformação do <see cref="T:System.Windows.Media.Media3D.Viewport3DVisual" />.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Transform" /> aplicado a do Viewport3DVisual.</returns>
		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06003283 RID: 12931 RVA: 0x000C96BC File Offset: 0x000C8ABC
		// (set) Token: 0x06003284 RID: 12932 RVA: 0x000C96D0 File Offset: 0x000C8AD0
		public Transform Transform
		{
			get
			{
				return base.VisualTransform;
			}
			set
			{
				base.VisualTransform = value;
			}
		}

		/// <summary>Obtém ou define o valor de deslocamento do <see cref="T:System.Windows.Media.Media3D.Viewport3DVisual" />.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Vector" /> que representa o valor de deslocamento do Viewport3DVisual.</returns>
		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06003285 RID: 12933 RVA: 0x000C96E4 File Offset: 0x000C8AE4
		// (set) Token: 0x06003286 RID: 12934 RVA: 0x000C96F8 File Offset: 0x000C8AF8
		public Vector Offset
		{
			get
			{
				return base.VisualOffset;
			}
			set
			{
				base.VisualOffset = value;
			}
		}

		/// <summary>Obtém a união de todas as caixas delimitadoras de conteúdo de todos os descendentes do <see cref="T:System.Windows.Media.Media3D.Viewport3DVisual" />, mas sem incluir o conteúdo do Viewport3DVisual.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Rect" /> que define a união.</returns>
		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06003287 RID: 12935 RVA: 0x000C970C File Offset: 0x000C8B0C
		public Rect DescendantBounds
		{
			get
			{
				return base.VisualDescendantBounds;
			}
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000C9720 File Offset: 0x000C8B20
		private static void CameraPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Viewport3DVisual viewport3DVisual = (Viewport3DVisual)d;
			if (!e.IsASubPropertyChange)
			{
				if (e.OldValue != null)
				{
					viewport3DVisual.DisconnectAttachedResource(VisualProxyFlags.Viewport3DVisual_IsCameraDirty, (DUCE.IResource)e.OldValue);
				}
				viewport3DVisual.SetFlagsOnAllChannels(true, VisualProxyFlags.IsContentDirty | VisualProxyFlags.Viewport3DVisual_IsCameraDirty);
			}
			viewport3DVisual.ContentsChanged(viewport3DVisual, EventArgs.Empty);
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Media3D.Camera" /> usado pelo <see cref="T:System.Windows.Media.Media3D.Viewport3DVisual" />.</summary>
		/// <returns>Câmera usada pelo <see cref="T:System.Windows.Media.Media3D.Viewport3DVisual" />.</returns>
		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06003289 RID: 12937 RVA: 0x000C9778 File Offset: 0x000C8B78
		// (set) Token: 0x0600328A RID: 12938 RVA: 0x000C9798 File Offset: 0x000C8B98
		public Camera Camera
		{
			get
			{
				return (Camera)base.GetValue(Viewport3DVisual.CameraProperty);
			}
			set
			{
				base.SetValue(Viewport3DVisual.CameraProperty, value);
			}
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x000C97B4 File Offset: 0x000C8BB4
		private static void ViewportPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Viewport3DVisual viewport3DVisual = (Viewport3DVisual)d;
			viewport3DVisual.SetFlagsOnAllChannels(true, VisualProxyFlags.IsContentDirty | VisualProxyFlags.Viewport3DVisual_IsViewportDirty);
			viewport3DVisual.ContentsChanged(viewport3DVisual, EventArgs.Empty);
		}

		/// <summary>Obtém ou define o retângulo no qual o <see cref="T:System.Windows.Media.Media3D.Viewport3DVisual" /> será renderizado.</summary>
		/// <returns>Retângulo no qual o conteúdo do Viewport3D será renderizado.</returns>
		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x0600328C RID: 12940 RVA: 0x000C97E0 File Offset: 0x000C8BE0
		// (set) Token: 0x0600328D RID: 12941 RVA: 0x000C9800 File Offset: 0x000C8C00
		public Rect Viewport
		{
			get
			{
				return (Rect)base.GetValue(Viewport3DVisual.ViewportProperty);
			}
			set
			{
				base.SetValue(Viewport3DVisual.ViewportProperty, value);
			}
		}

		/// <summary>Obtém uma coleção de objetos <see cref="T:System.Windows.Media.Media3D.Visual3D" /> contidos no <see cref="T:System.Windows.Media.Media3D.Viewport3DVisual" />.</summary>
		/// <returns>Coleção de objetos contidos pelo <see cref="T:System.Windows.Media.Media3D.Viewport3DVisual" />.</returns>
		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x0600328E RID: 12942 RVA: 0x000C9820 File Offset: 0x000C8C20
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Visual3DCollection Children
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x000C9834 File Offset: 0x000C8C34
		void IVisual3DContainer.VerifyAPIReadOnly()
		{
			base.VerifyAPIReadOnly();
		}

		// Token: 0x06003290 RID: 12944 RVA: 0x000C9848 File Offset: 0x000C8C48
		void IVisual3DContainer.VerifyAPIReadOnly(DependencyObject other)
		{
			base.VerifyAPIReadOnly(other);
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x000C985C File Offset: 0x000C8C5C
		void IVisual3DContainer.VerifyAPIReadWrite()
		{
			base.VerifyAPIReadWrite();
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x000C9870 File Offset: 0x000C8C70
		void IVisual3DContainer.VerifyAPIReadWrite(DependencyObject other)
		{
			base.VerifyAPIReadWrite(other);
		}

		// Token: 0x06003293 RID: 12947 RVA: 0x000C9884 File Offset: 0x000C8C84
		void IVisual3DContainer.AddChild(Visual3D child)
		{
			if (base.IsVisualChildrenIterationInProgress)
			{
				throw new InvalidOperationException(SR.Get("CannotModifyVisualChildrenDuringTreeWalk"));
			}
			VisualDiagnostics.VerifyVisualTreeChange(this);
			child.SetParent(this);
			if (this._inheritanceContextForChildren != null)
			{
				this._inheritanceContextForChildren.ProvideSelfAsInheritanceContext(child, null);
			}
			base.SetFlagsOnAllChannels(true, VisualProxyFlags.IsContentDirty);
			Visual.PropagateFlags(this, VisualFlags.IsSubtreeDirtyForPrecompute, VisualProxyFlags.IsSubtreeDirtyForRender);
			Visual3D.PropagateFlags(child, VisualFlags.IsSubtreeDirtyForPrecompute, VisualProxyFlags.IsSubtreeDirtyForRender);
			this.OnVisualChildrenChanged(child, null);
			child.FireOnVisualParentChanged(null);
			VisualDiagnostics.OnVisualChildChanged(this, child, true);
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x000C98FC File Offset: 0x000C8CFC
		void IVisual3DContainer.RemoveChild(Visual3D child)
		{
			int parentIndex = child.ParentIndex;
			if (base.IsVisualChildrenIterationInProgress)
			{
				throw new InvalidOperationException(SR.Get("CannotModifyVisualChildrenDuringTreeWalk"));
			}
			VisualDiagnostics.VerifyVisualTreeChange(this);
			VisualDiagnostics.OnVisualChildChanged(this, child, false);
			child.SetParent(null);
			if (this._inheritanceContextForChildren != null)
			{
				this._inheritanceContextForChildren.RemoveSelfAsInheritanceContext(child, null);
			}
			int i = 0;
			int count = this._proxy3D.Count;
			while (i < count)
			{
				DUCE.Channel channel = this._proxy3D.GetChannel(i);
				if (child.CheckFlagsAnd(channel, VisualProxyFlags.IsConnectedToParent))
				{
					child.SetFlags(channel, false, VisualProxyFlags.IsConnectedToParent);
					((DUCE.IResource)child).RemoveChildFromParent(this, channel);
					((DUCE.IResource)child).ReleaseOnChannel(channel);
				}
				i++;
			}
			base.SetFlagsOnAllChannels(true, VisualProxyFlags.IsContentDirty);
			Visual.PropagateFlags(this, VisualFlags.IsSubtreeDirtyForPrecompute, VisualProxyFlags.IsSubtreeDirtyForRender);
			child.FireOnVisualParentChanged(this);
			this.OnVisualChildrenChanged(null, child);
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x000C99C4 File Offset: 0x000C8DC4
		int IVisual3DContainer.GetChildrenCount()
		{
			return this.InternalVisual2DOr3DChildrenCount;
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x000C99D8 File Offset: 0x000C8DD8
		Visual3D IVisual3DContainer.GetChild(int index)
		{
			return (Visual3D)this.InternalGet2DOr3DVisualChild(index);
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06003297 RID: 12951 RVA: 0x000C99F4 File Offset: 0x000C8DF4
		internal override int InternalVisual2DOr3DChildrenCount
		{
			get
			{
				return this.Children.Count;
			}
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x000C9A0C File Offset: 0x000C8E0C
		internal override DependencyObject InternalGet2DOr3DVisualChild(int index)
		{
			return this.Children[index];
		}

		// Token: 0x06003299 RID: 12953 RVA: 0x000C9A28 File Offset: 0x000C8E28
		internal override HitTestResultBehavior HitTestPointInternal(HitTestFilterCallback filterCallback, HitTestResultCallback resultCallback, PointHitTestParameters hitTestParameters)
		{
			if (this._children.Count != 0)
			{
				double distanceAdjustment;
				RayHitTestParameters rayHitTestParameters = this.Camera.RayFromViewportPoint(hitTestParameters.HitPoint, this.Viewport.Size, this.BBoxSubgraph, out distanceAdjustment);
				HitTestResultBehavior lastResult = Visual3D.HitTestChildren(filterCallback, rayHitTestParameters, this);
				return rayHitTestParameters.RaiseCallback(resultCallback, filterCallback, lastResult, distanceAdjustment);
			}
			return HitTestResultBehavior.Continue;
		}

		// Token: 0x0600329A RID: 12954 RVA: 0x000C9A80 File Offset: 0x000C8E80
		protected override GeometryHitTestResult HitTestCore(GeometryHitTestParameters hitTestParameters)
		{
			throw new NotSupportedException(SR.Get("HitTest_Invalid", new object[]
			{
				typeof(GeometryHitTestParameters).Name,
				base.GetType().Name
			}));
		}

		// Token: 0x0600329B RID: 12955 RVA: 0x000C9AC4 File Offset: 0x000C8EC4
		internal Point WorldToViewport(Point4D point)
		{
			double aspectRatio = M3DUtil.GetAspectRatio(this.Viewport.Size);
			Camera camera = this.Camera;
			if (camera != null)
			{
				Matrix3D matrix = camera.GetViewMatrix() * camera.GetProjectionMatrix(aspectRatio);
				point *= matrix;
				Point point2 = new Point(point.X / point.W, point.Y / point.W);
				point2 *= M3DUtil.GetHomogeneousToViewportTransform(this.Viewport);
				return point2;
			}
			return new Point(0.0, 0.0);
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x000C9B5C File Offset: 0x000C8F5C
		internal override Rect GetHitTestBounds()
		{
			return base.CalculateSubgraphBoundsInnerSpace();
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x000C9B70 File Offset: 0x000C8F70
		internal override Rect CalculateSubgraphBoundsInnerSpace(bool renderBounds)
		{
			Camera camera = this.Camera;
			if (camera == null)
			{
				return Rect.Empty;
			}
			this._bboxChildrenSubgraph3D = this.ComputeSubgraphBounds3D();
			if (this._bboxChildrenSubgraph3D.IsEmpty)
			{
				return Rect.Empty;
			}
			Rect viewport = this.Viewport;
			if (viewport.IsEmpty)
			{
				return Rect.Empty;
			}
			double aspectRatio = M3DUtil.GetAspectRatio(viewport.Size);
			Matrix3D matrix3D = camera.GetViewMatrix() * camera.GetProjectionMatrix(aspectRatio);
			Rect result = MILUtilities.ProjectBounds(ref matrix3D, ref this._bboxChildrenSubgraph3D);
			Matrix homogeneousToViewportTransform = M3DUtil.GetHomogeneousToViewportTransform(viewport);
			MatrixUtil.TransformRect(ref result, ref homogeneousToViewportTransform);
			return result;
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x0600329E RID: 12958 RVA: 0x000C9C04 File Offset: 0x000C9004
		private Rect3D BBoxSubgraph
		{
			get
			{
				return this._bboxChildrenSubgraph3D;
			}
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x000C9C18 File Offset: 0x000C9018
		internal Rect3D ComputeSubgraphBounds3D()
		{
			Rect3D empty = Rect3D.Empty;
			int i = 0;
			int internalCount = this._children.InternalCount;
			while (i < internalCount)
			{
				Visual3D visual3D = this._children.InternalGetItem(i);
				empty.Union(visual3D.CalculateSubgraphBoundsOuterSpace());
				i++;
			}
			return empty;
		}

		// Token: 0x060032A0 RID: 12960 RVA: 0x000C9C60 File Offset: 0x000C9060
		[Conditional("DEBUG")]
		private void Debug_VerifyCachedSubgraphBounds()
		{
			Rect3D rect3D = Rect3D.Empty;
			rect3D = this.ComputeSubgraphBounds3D();
			Rect3D bboxChildrenSubgraph3D = this._bboxChildrenSubgraph3D;
			bool flag = bboxChildrenSubgraph3D.X >= rect3D.X && bboxChildrenSubgraph3D.X <= rect3D.X && bboxChildrenSubgraph3D.Y >= rect3D.Y && bboxChildrenSubgraph3D.Y <= rect3D.Y && bboxChildrenSubgraph3D.Z >= rect3D.Z && bboxChildrenSubgraph3D.Z <= rect3D.Z && bboxChildrenSubgraph3D.SizeX >= rect3D.SizeX && bboxChildrenSubgraph3D.SizeX <= rect3D.SizeX && bboxChildrenSubgraph3D.SizeY >= rect3D.SizeY && bboxChildrenSubgraph3D.SizeY <= rect3D.SizeY && bboxChildrenSubgraph3D.SizeZ >= rect3D.SizeZ && bboxChildrenSubgraph3D.SizeZ <= rect3D.SizeZ;
		}

		// Token: 0x060032A1 RID: 12961 RVA: 0x000C9D5C File Offset: 0x000C915C
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			DUCE.ResourceHandle resourceHandle = base.AddRefOnChannelCore(channel);
			bool flag = this._proxy3D.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_VISUAL3D);
			if (flag)
			{
				DUCE.Viewport3DVisualNode.Set3DChild(resourceHandle, this._proxy3D.GetHandle(channel), channel);
			}
			return resourceHandle;
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x000C9D98 File Offset: 0x000C9198
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			base.ReleaseOnChannelCore(channel);
			this._proxy3D.ReleaseOnChannel(channel);
		}

		// Token: 0x060032A3 RID: 12963 RVA: 0x000C9DBC File Offset: 0x000C91BC
		int DUCE.IResource.GetChannelCount()
		{
			return this._proxy.Count;
		}

		// Token: 0x060032A4 RID: 12964 RVA: 0x000C9DD4 File Offset: 0x000C91D4
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this._proxy.GetChannel(index);
		}

		// Token: 0x060032A5 RID: 12965 RVA: 0x000C9DF0 File Offset: 0x000C91F0
		internal override void PrecomputeContent()
		{
			base.PrecomputeContent();
			if (this._children != null)
			{
				int i = 0;
				int internalCount = this._children.InternalCount;
				while (i < internalCount)
				{
					Visual3D visual3D = this._children.InternalGetItem(i);
					if (visual3D != null)
					{
						Rect3D rect3D;
						visual3D.PrecomputeRecursive(out rect3D);
					}
					i++;
				}
			}
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x000C9E3C File Offset: 0x000C923C
		internal override void RenderContent(RenderContext ctx, bool isOnChannel)
		{
			DUCE.Channel channel = ctx.Channel;
			VisualProxyFlags flags = this._proxy.GetFlags(channel);
			if ((flags & VisualProxyFlags.Viewport3DVisual_IsCameraDirty) != VisualProxyFlags.None)
			{
				Camera camera = this.Camera;
				if (camera != null)
				{
					DUCE.Viewport3DVisualNode.SetCamera(((DUCE.IResource)this).GetHandle(channel), ((DUCE.IResource)camera).AddRefOnChannel(channel), channel);
				}
				else if (isOnChannel)
				{
					DUCE.Viewport3DVisualNode.SetCamera(((DUCE.IResource)this).GetHandle(channel), DUCE.ResourceHandle.Null, channel);
				}
				base.SetFlags(channel, false, VisualProxyFlags.Viewport3DVisual_IsCameraDirty);
			}
			if ((flags & VisualProxyFlags.Viewport3DVisual_IsViewportDirty) != VisualProxyFlags.None)
			{
				DUCE.Viewport3DVisualNode.SetViewport(((DUCE.IResource)this).GetHandle(channel), this.Viewport, channel);
				base.SetFlags(channel, false, VisualProxyFlags.Viewport3DVisual_IsViewportDirty);
			}
			if (this._children != null)
			{
				uint num = 0U;
				while ((ulong)num < (ulong)((long)this._children.InternalCount))
				{
					Visual3D visual3D = this._children.InternalGetItem((int)num);
					if (visual3D != null)
					{
						if (visual3D.CheckFlagsAnd(channel, VisualProxyFlags.IsSubtreeDirtyForRender) || !visual3D.IsOnChannel(channel))
						{
							visual3D.RenderRecursive(ctx);
						}
						if (visual3D.IsOnChannel(channel) && !visual3D.CheckFlagsAnd(channel, VisualProxyFlags.IsConnectedToParent))
						{
							DUCE.Visual3DNode.InsertChildAt(this._proxy3D.GetHandle(channel), ((DUCE.IResource)visual3D).GetHandle(channel), num, channel);
							visual3D.SetFlags(channel, true, VisualProxyFlags.IsConnectedToParent);
						}
					}
					num += 1U;
				}
			}
		}

		// Token: 0x060032A7 RID: 12967 RVA: 0x000C9F60 File Offset: 0x000C9360
		internal override void FreeContent(DUCE.Channel channel)
		{
			Camera camera = this.Camera;
			if (camera != null && !base.CheckFlagsAnd(channel, VisualProxyFlags.Viewport3DVisual_IsCameraDirty))
			{
				((DUCE.IResource)camera).ReleaseOnChannel(channel);
				base.SetFlagsOnAllChannels(true, VisualProxyFlags.Viewport3DVisual_IsCameraDirty);
			}
			if (this._children != null)
			{
				for (int i = 0; i < this._children.InternalCount; i++)
				{
					Visual3D visual3D = this._children.InternalGetItem(i);
					((DUCE.IResource)visual3D).ReleaseOnChannel(channel);
				}
			}
			base.SetFlagsOnAllChannels(true, VisualProxyFlags.IsContentDirty);
			base.FreeContent(channel);
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x000C9FDC File Offset: 0x000C93DC
		internal void Visual3DTreeChanged()
		{
			base.SetFlagsOnAllChannels(true, VisualProxyFlags.IsContentDirty);
			this.ContentsChanged(this, EventArgs.Empty);
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x000CA000 File Offset: 0x000C9400
		DUCE.ResourceHandle DUCE.IResource.Get3DHandle(DUCE.Channel channel)
		{
			return this._proxy3D.GetHandle(channel);
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x000CA01C File Offset: 0x000C941C
		[FriendAccessAllowed]
		internal void SetInheritanceContextForChildren(DependencyObject inheritanceContextForChildren)
		{
			this._inheritanceContextForChildren = inheritanceContextForChildren;
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.Viewport3DVisual.Camera" />.</summary>
		// Token: 0x040015D6 RID: 5590
		public static readonly DependencyProperty CameraProperty = DependencyProperty.Register("Camera", typeof(Camera), typeof(Viewport3DVisual), new PropertyMetadata(FreezableOperations.GetAsFrozen(new PerspectiveCamera()), new PropertyChangedCallback(Viewport3DVisual.CameraPropertyChanged)), (object <p0>) => MediaContext.CurrentMediaContext.WriteAccessEnabled);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.Viewport3DVisual.Viewport" />.</summary>
		// Token: 0x040015D7 RID: 5591
		public static readonly DependencyProperty ViewportProperty = DependencyProperty.Register("Viewport", typeof(Rect), typeof(Viewport3DVisual), new PropertyMetadata(Rect.Empty, new PropertyChangedCallback(Viewport3DVisual.ViewportPropertyChanged)), (object <p0>) => MediaContext.CurrentMediaContext.WriteAccessEnabled);

		// Token: 0x040015D8 RID: 5592
		private VisualProxy _proxy3D;

		// Token: 0x040015D9 RID: 5593
		private Rect3D _bboxChildrenSubgraph3D;

		// Token: 0x040015DA RID: 5594
		private readonly Visual3DCollection _children;

		// Token: 0x040015DB RID: 5595
		private DependencyObject _inheritanceContextForChildren;
	}
}
