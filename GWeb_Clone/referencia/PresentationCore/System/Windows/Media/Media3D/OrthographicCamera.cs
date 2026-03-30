using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal.Media3D;

namespace System.Windows.Media.Media3D
{
	/// <summary>Representa uma câmera de projeção ortográfica.</summary>
	// Token: 0x02000473 RID: 1139
	public sealed class OrthographicCamera : ProjectionCamera
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.OrthographicCamera" />.</summary>
		// Token: 0x06003089 RID: 12425 RVA: 0x000C1BD0 File Offset: 0x000C0FD0
		public OrthographicCamera()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.OrthographicCamera" /> com a posição, a direção de projeção, a direção para cima e a largura especificadas.</summary>
		/// <param name="position">Um <see cref="T:System.Windows.Media.Media3D.Point3D" /> que especifica a posição da câmera.</param>
		/// <param name="lookDirection">Um <see cref="T:System.Windows.Media.Media3D.Vector3D" /> que especifica a direção da projeção da câmera.</param>
		/// <param name="upDirection">Um <see cref="T:System.Windows.Media.Media3D.Vector3D" /> que especifica a direção para cima de acordo com a perspectiva do observador.</param>
		/// <param name="width">A largura da caixa de exibição da câmera, em unidades do mundo.</param>
		// Token: 0x0600308A RID: 12426 RVA: 0x000C1BE4 File Offset: 0x000C0FE4
		public OrthographicCamera(Point3D position, Vector3D lookDirection, Vector3D upDirection, double width)
		{
			base.Position = position;
			base.LookDirection = lookDirection;
			base.UpDirection = upDirection;
			this.Width = width;
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x000C1C14 File Offset: 0x000C1014
		internal Matrix3D GetProjectionMatrix(double aspectRatio, double zn, double zf)
		{
			double width = this.Width;
			double num = width / aspectRatio;
			double num2 = 1.0 / (zn - zf);
			double offsetZ = zn * num2;
			return new Matrix3D(2.0 / width, 0.0, 0.0, 0.0, 0.0, 2.0 / num, 0.0, 0.0, 0.0, 0.0, num2, 0.0, 0.0, 0.0, offsetZ, 1.0);
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x000C1CC8 File Offset: 0x000C10C8
		internal override Matrix3D GetProjectionMatrix(double aspectRatio)
		{
			return this.GetProjectionMatrix(aspectRatio, base.NearPlaneDistance, base.FarPlaneDistance);
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x000C1CE8 File Offset: 0x000C10E8
		internal override RayHitTestParameters RayFromViewportPoint(Point p, Size viewSize, Rect3D boundingRect, out double distanceAdjustment)
		{
			Point3D position = base.Position;
			Vector3D lookDirection = base.LookDirection;
			Vector3D upDirection = base.UpDirection;
			double num = base.NearPlaneDistance;
			double farPlaneDistance = base.FarPlaneDistance;
			double width = this.Width;
			Point normalizedPoint = M3DUtil.GetNormalizedPoint(p, viewSize);
			double aspectRatio = M3DUtil.GetAspectRatio(viewSize);
			double num2 = width;
			double num3 = num2 / aspectRatio;
			Vector3D direction = new Vector3D(0.0, 0.0, -1.0);
			Matrix3D matrix3D = ProjectionCamera.CreateViewMatrix(base.Transform, ref position, ref lookDirection, ref upDirection);
			Matrix3D matrix3D2 = matrix3D;
			matrix3D2.Invert();
			Rect3D rect3D = M3DUtil.ComputeTransformedAxisAlignedBoundingBox(ref boundingRect, ref matrix3D);
			double num4 = -this.AddEpsilon(rect3D.Z + rect3D.SizeZ);
			if (num4 > num)
			{
				distanceAdjustment = num4 - num;
				num = num4;
			}
			else
			{
				distanceAdjustment = 0.0;
			}
			Point3D origin = new Point3D(normalizedPoint.X * (num2 / 2.0), normalizedPoint.Y * (num3 / 2.0), -num);
			matrix3D2.MultiplyPoint(ref origin);
			matrix3D2.MultiplyVector(ref direction);
			RayHitTestParameters rayHitTestParameters = new RayHitTestParameters(origin, direction);
			Matrix3D projectionMatrix = this.GetProjectionMatrix(aspectRatio, num, farPlaneDistance);
			Matrix3D matrix = default(Matrix3D);
			matrix.TranslatePrepend(new Vector3D(-p.X, viewSize.Height - p.Y, 0.0));
			matrix.ScalePrepend(new Vector3D(viewSize.Width / 2.0, -viewSize.Height / 2.0, 1.0));
			matrix.TranslatePrepend(new Vector3D(1.0, 1.0, 0.0));
			rayHitTestParameters.HitTestProjectionMatrix = matrix3D * projectionMatrix * matrix;
			return rayHitTestParameters;
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x000C1EC0 File Offset: 0x000C12C0
		private double AddEpsilon(double x)
		{
			return x + 0.1 * Math.Abs(x) + 1.0;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.OrthographicCamera" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x0600308F RID: 12431 RVA: 0x000C1EEC File Offset: 0x000C12EC
		public new OrthographicCamera Clone()
		{
			return (OrthographicCamera)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.OrthographicCamera" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06003090 RID: 12432 RVA: 0x000C1F04 File Offset: 0x000C1304
		public new OrthographicCamera CloneCurrentValue()
		{
			return (OrthographicCamera)base.CloneCurrentValue();
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x000C1F1C File Offset: 0x000C131C
		private static void WidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			OrthographicCamera orthographicCamera = (OrthographicCamera)d;
			orthographicCamera.PropertyChanged(OrthographicCamera.WidthProperty);
		}

		/// <summary>Obtém ou define a largura da caixa de exibição da câmera, em unidades do mundo.</summary>
		/// <returns>Largura da caixa de exibição da câmera, em unidades do mundo.</returns>
		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06003092 RID: 12434 RVA: 0x000C1F3C File Offset: 0x000C133C
		// (set) Token: 0x06003093 RID: 12435 RVA: 0x000C1F5C File Offset: 0x000C135C
		public double Width
		{
			get
			{
				return (double)base.GetValue(OrthographicCamera.WidthProperty);
			}
			set
			{
				base.SetValueInternal(OrthographicCamera.WidthProperty, value);
			}
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x000C1F7C File Offset: 0x000C137C
		protected override Freezable CreateInstanceCore()
		{
			return new OrthographicCamera();
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x000C1F90 File Offset: 0x000C1390
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Transform3D transform = base.Transform;
				DUCE.ResourceHandle htransform;
				if (transform == null || transform == Transform3D.Identity)
				{
					htransform = DUCE.ResourceHandle.Null;
				}
				else
				{
					htransform = ((DUCE.IResource)transform).GetHandle(channel);
				}
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(ProjectionCamera.NearPlaneDistanceProperty, channel);
				DUCE.ResourceHandle animationResourceHandle2 = base.GetAnimationResourceHandle(ProjectionCamera.FarPlaneDistanceProperty, channel);
				DUCE.ResourceHandle animationResourceHandle3 = base.GetAnimationResourceHandle(ProjectionCamera.PositionProperty, channel);
				DUCE.ResourceHandle animationResourceHandle4 = base.GetAnimationResourceHandle(ProjectionCamera.LookDirectionProperty, channel);
				DUCE.ResourceHandle animationResourceHandle5 = base.GetAnimationResourceHandle(ProjectionCamera.UpDirectionProperty, channel);
				DUCE.ResourceHandle animationResourceHandle6 = base.GetAnimationResourceHandle(OrthographicCamera.WidthProperty, channel);
				DUCE.MILCMD_ORTHOGRAPHICCAMERA milcmd_ORTHOGRAPHICCAMERA;
				milcmd_ORTHOGRAPHICCAMERA.Type = MILCMD.MilCmdOrthographicCamera;
				milcmd_ORTHOGRAPHICCAMERA.Handle = this._duceResource.GetHandle(channel);
				milcmd_ORTHOGRAPHICCAMERA.htransform = htransform;
				if (animationResourceHandle.IsNull)
				{
					milcmd_ORTHOGRAPHICCAMERA.nearPlaneDistance = base.NearPlaneDistance;
				}
				milcmd_ORTHOGRAPHICCAMERA.hNearPlaneDistanceAnimations = animationResourceHandle;
				if (animationResourceHandle2.IsNull)
				{
					milcmd_ORTHOGRAPHICCAMERA.farPlaneDistance = base.FarPlaneDistance;
				}
				milcmd_ORTHOGRAPHICCAMERA.hFarPlaneDistanceAnimations = animationResourceHandle2;
				if (animationResourceHandle3.IsNull)
				{
					milcmd_ORTHOGRAPHICCAMERA.position = CompositionResourceManager.Point3DToMilPoint3F(base.Position);
				}
				milcmd_ORTHOGRAPHICCAMERA.hPositionAnimations = animationResourceHandle3;
				if (animationResourceHandle4.IsNull)
				{
					milcmd_ORTHOGRAPHICCAMERA.lookDirection = CompositionResourceManager.Vector3DToMilPoint3F(base.LookDirection);
				}
				milcmd_ORTHOGRAPHICCAMERA.hLookDirectionAnimations = animationResourceHandle4;
				if (animationResourceHandle5.IsNull)
				{
					milcmd_ORTHOGRAPHICCAMERA.upDirection = CompositionResourceManager.Vector3DToMilPoint3F(base.UpDirection);
				}
				milcmd_ORTHOGRAPHICCAMERA.hUpDirectionAnimations = animationResourceHandle5;
				if (animationResourceHandle6.IsNull)
				{
					milcmd_ORTHOGRAPHICCAMERA.width = this.Width;
				}
				milcmd_ORTHOGRAPHICCAMERA.hWidthAnimations = animationResourceHandle6;
				channel.SendCommand((byte*)(&milcmd_ORTHOGRAPHICCAMERA), sizeof(DUCE.MILCMD_ORTHOGRAPHICCAMERA));
			}
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x000C2128 File Offset: 0x000C1528
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_ORTHOGRAPHICCAMERA))
			{
				Transform3D transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).AddRefOnChannel(channel);
				}
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x000C2174 File Offset: 0x000C1574
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				Transform3D transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).ReleaseOnChannel(channel);
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x000C21A8 File Offset: 0x000C15A8
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x000C21C4 File Offset: 0x000C15C4
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x000C21DC File Offset: 0x000C15DC
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x000C21F8 File Offset: 0x000C15F8
		static OrthographicCamera()
		{
			Type typeFromHandle = typeof(OrthographicCamera);
			OrthographicCamera.WidthProperty = Animatable.RegisterProperty("Width", typeof(double), typeFromHandle, 2.0, new PropertyChangedCallback(OrthographicCamera.WidthPropertyChanged), null, true, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.OrthographicCamera.Width" />.</summary>
		// Token: 0x04001557 RID: 5463
		public static readonly DependencyProperty WidthProperty;

		// Token: 0x04001558 RID: 5464
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x04001559 RID: 5465
		internal const double c_Width = 2.0;
	}
}
