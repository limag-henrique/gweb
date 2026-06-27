using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal.Media3D;

namespace System.Windows.Media.Media3D
{
	/// <summary>Representa uma câmera de projeção de perspectiva.</summary>
	// Token: 0x02000474 RID: 1140
	public sealed class PerspectiveCamera : ProjectionCamera
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.PerspectiveCamera" />.</summary>
		// Token: 0x0600309C RID: 12444 RVA: 0x000C2248 File Offset: 0x000C1648
		public PerspectiveCamera()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.PerspectiveCamera" /> usando a posição, direção e campo de visão especificados.</summary>
		/// <param name="position">Point3D que especifica a posição da câmera.</param>
		/// <param name="lookDirection">Vector3D que especifica a direção da projeção da câmera.</param>
		/// <param name="upDirection">Vector3D que especifica a direção para cima de acordo com a perspectiva do observador.</param>
		/// <param name="fieldOfView">Largura do ângulo de projeção da câmera, especificada em graus.</param>
		// Token: 0x0600309D RID: 12445 RVA: 0x000C225C File Offset: 0x000C165C
		public PerspectiveCamera(Point3D position, Vector3D lookDirection, Vector3D upDirection, double fieldOfView)
		{
			base.Position = position;
			base.LookDirection = lookDirection;
			base.UpDirection = upDirection;
			this.FieldOfView = fieldOfView;
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x000C228C File Offset: 0x000C168C
		internal Matrix3D GetProjectionMatrix(double aspectRatio, double zn, double zf)
		{
			double num = M3DUtil.DegreesToRadians(this.FieldOfView);
			double num2 = Math.Tan(num / 2.0);
			double m = aspectRatio / num2;
			double m2 = 1.0 / num2;
			double num3 = (zf != double.PositiveInfinity) ? (zf / (zn - zf)) : -1.0;
			double offsetZ = zn * num3;
			return new Matrix3D(m2, 0.0, 0.0, 0.0, 0.0, m, 0.0, 0.0, 0.0, 0.0, num3, -1.0, 0.0, 0.0, offsetZ, 0.0);
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x000C2360 File Offset: 0x000C1760
		internal override Matrix3D GetProjectionMatrix(double aspectRatio)
		{
			return this.GetProjectionMatrix(aspectRatio, base.NearPlaneDistance, base.FarPlaneDistance);
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x000C2380 File Offset: 0x000C1780
		internal override RayHitTestParameters RayFromViewportPoint(Point p, Size viewSize, Rect3D boundingRect, out double distanceAdjustment)
		{
			Point3D position = base.Position;
			Vector3D lookDirection = base.LookDirection;
			Vector3D upDirection = base.UpDirection;
			Transform3D transform = base.Transform;
			double nearPlaneDistance = base.NearPlaneDistance;
			double farPlaneDistance = base.FarPlaneDistance;
			double num = M3DUtil.DegreesToRadians(this.FieldOfView);
			Point normalizedPoint = M3DUtil.GetNormalizedPoint(p, viewSize);
			double aspectRatio = M3DUtil.GetAspectRatio(viewSize);
			double num2 = Math.Tan(num / 2.0);
			double num3 = aspectRatio / num2;
			double num4 = 1.0 / num2;
			Vector3D vector3D = new Vector3D(normalizedPoint.X / num4, normalizedPoint.Y / num3, -1.0);
			Matrix3D matrix3D = ProjectionCamera.CreateViewMatrix(null, ref position, ref lookDirection, ref upDirection);
			Matrix3D matrix3D2 = matrix3D;
			matrix3D2.Invert();
			matrix3D2.MultiplyVector(ref vector3D);
			Point3D origin = position + nearPlaneDistance * vector3D;
			vector3D.Normalize();
			if (transform != null && transform != Transform3D.Identity)
			{
				Matrix3D value = transform.Value;
				value.MultiplyPoint(ref origin);
				value.MultiplyVector(ref vector3D);
				Camera.PrependInverseTransform(value, ref matrix3D);
			}
			RayHitTestParameters rayHitTestParameters = new RayHitTestParameters(origin, vector3D);
			Matrix3D projectionMatrix = this.GetProjectionMatrix(aspectRatio, nearPlaneDistance, farPlaneDistance);
			Matrix3D matrix = default(Matrix3D);
			matrix.TranslatePrepend(new Vector3D(-p.X, viewSize.Height - p.Y, 0.0));
			matrix.ScalePrepend(new Vector3D(viewSize.Width / 2.0, -viewSize.Height / 2.0, 1.0));
			matrix.TranslatePrepend(new Vector3D(1.0, 1.0, 0.0));
			rayHitTestParameters.HitTestProjectionMatrix = matrix3D * projectionMatrix * matrix;
			distanceAdjustment = 0.0;
			return rayHitTestParameters;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.PerspectiveCamera" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060030A1 RID: 12449 RVA: 0x000C2558 File Offset: 0x000C1958
		public new PerspectiveCamera Clone()
		{
			return (PerspectiveCamera)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.PerspectiveCamera" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060030A2 RID: 12450 RVA: 0x000C2570 File Offset: 0x000C1970
		public new PerspectiveCamera CloneCurrentValue()
		{
			return (PerspectiveCamera)base.CloneCurrentValue();
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x000C2588 File Offset: 0x000C1988
		private static void FieldOfViewPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PerspectiveCamera perspectiveCamera = (PerspectiveCamera)d;
			perspectiveCamera.PropertyChanged(PerspectiveCamera.FieldOfViewProperty);
		}

		/// <summary>Obtém ou define um valor que representa o campo de exibição horizontal da câmera.</summary>
		/// <returns>O campo de exibição horizontal da câmera, em graus. O valor padrão é 45.</returns>
		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x060030A4 RID: 12452 RVA: 0x000C25A8 File Offset: 0x000C19A8
		// (set) Token: 0x060030A5 RID: 12453 RVA: 0x000C25C8 File Offset: 0x000C19C8
		public double FieldOfView
		{
			get
			{
				return (double)base.GetValue(PerspectiveCamera.FieldOfViewProperty);
			}
			set
			{
				base.SetValueInternal(PerspectiveCamera.FieldOfViewProperty, value);
			}
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x000C25E8 File Offset: 0x000C19E8
		protected override Freezable CreateInstanceCore()
		{
			return new PerspectiveCamera();
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x000C25FC File Offset: 0x000C19FC
		[SecurityCritical]
		[SecurityTreatAsSafe]
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
				DUCE.ResourceHandle animationResourceHandle6 = base.GetAnimationResourceHandle(PerspectiveCamera.FieldOfViewProperty, channel);
				DUCE.MILCMD_PERSPECTIVECAMERA milcmd_PERSPECTIVECAMERA;
				milcmd_PERSPECTIVECAMERA.Type = MILCMD.MilCmdPerspectiveCamera;
				milcmd_PERSPECTIVECAMERA.Handle = this._duceResource.GetHandle(channel);
				milcmd_PERSPECTIVECAMERA.htransform = htransform;
				if (animationResourceHandle.IsNull)
				{
					milcmd_PERSPECTIVECAMERA.nearPlaneDistance = base.NearPlaneDistance;
				}
				milcmd_PERSPECTIVECAMERA.hNearPlaneDistanceAnimations = animationResourceHandle;
				if (animationResourceHandle2.IsNull)
				{
					milcmd_PERSPECTIVECAMERA.farPlaneDistance = base.FarPlaneDistance;
				}
				milcmd_PERSPECTIVECAMERA.hFarPlaneDistanceAnimations = animationResourceHandle2;
				if (animationResourceHandle3.IsNull)
				{
					milcmd_PERSPECTIVECAMERA.position = CompositionResourceManager.Point3DToMilPoint3F(base.Position);
				}
				milcmd_PERSPECTIVECAMERA.hPositionAnimations = animationResourceHandle3;
				if (animationResourceHandle4.IsNull)
				{
					milcmd_PERSPECTIVECAMERA.lookDirection = CompositionResourceManager.Vector3DToMilPoint3F(base.LookDirection);
				}
				milcmd_PERSPECTIVECAMERA.hLookDirectionAnimations = animationResourceHandle4;
				if (animationResourceHandle5.IsNull)
				{
					milcmd_PERSPECTIVECAMERA.upDirection = CompositionResourceManager.Vector3DToMilPoint3F(base.UpDirection);
				}
				milcmd_PERSPECTIVECAMERA.hUpDirectionAnimations = animationResourceHandle5;
				if (animationResourceHandle6.IsNull)
				{
					milcmd_PERSPECTIVECAMERA.fieldOfView = this.FieldOfView;
				}
				milcmd_PERSPECTIVECAMERA.hFieldOfViewAnimations = animationResourceHandle6;
				channel.SendCommand((byte*)(&milcmd_PERSPECTIVECAMERA), sizeof(DUCE.MILCMD_PERSPECTIVECAMERA));
			}
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x000C2794 File Offset: 0x000C1B94
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_PERSPECTIVECAMERA))
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

		// Token: 0x060030A9 RID: 12457 RVA: 0x000C27E0 File Offset: 0x000C1BE0
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

		// Token: 0x060030AA RID: 12458 RVA: 0x000C2814 File Offset: 0x000C1C14
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000C2830 File Offset: 0x000C1C30
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x000C2848 File Offset: 0x000C1C48
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x000C2864 File Offset: 0x000C1C64
		static PerspectiveCamera()
		{
			Type typeFromHandle = typeof(PerspectiveCamera);
			PerspectiveCamera.FieldOfViewProperty = Animatable.RegisterProperty("FieldOfView", typeof(double), typeFromHandle, 45.0, new PropertyChangedCallback(PerspectiveCamera.FieldOfViewPropertyChanged), null, true, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.PerspectiveCamera.FieldOfView" />.</summary>
		// Token: 0x0400155A RID: 5466
		public static readonly DependencyProperty FieldOfViewProperty;

		// Token: 0x0400155B RID: 5467
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x0400155C RID: 5468
		internal const double c_FieldOfView = 45.0;
	}
}
