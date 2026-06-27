using System;
using System.Windows.Media.Animation;

namespace System.Windows.Media.Media3D
{
	/// <summary>Uma classe base abstrata para câmeras projeção de perspectiva e ortográficas.</summary>
	// Token: 0x02000475 RID: 1141
	public abstract class ProjectionCamera : Camera
	{
		// Token: 0x060030AE RID: 12462 RVA: 0x000C28B4 File Offset: 0x000C1CB4
		internal ProjectionCamera()
		{
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x000C28C8 File Offset: 0x000C1CC8
		internal override Matrix3D GetViewMatrix()
		{
			Point3D position = this.Position;
			Vector3D lookDirection = this.LookDirection;
			Vector3D upDirection = this.UpDirection;
			return ProjectionCamera.CreateViewMatrix(base.Transform, ref position, ref lookDirection, ref upDirection);
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000C28FC File Offset: 0x000C1CFC
		internal static Matrix3D CreateViewMatrix(Transform3D transform, ref Point3D position, ref Vector3D lookDirection, ref Vector3D upDirection)
		{
			Vector3D vector3D = -lookDirection;
			vector3D.Normalize();
			Vector3D vector3D2 = Vector3D.CrossProduct(upDirection, vector3D);
			vector3D2.Normalize();
			Vector3D vector = Vector3D.CrossProduct(vector3D, vector3D2);
			Vector3D vector2 = (Vector3D)position;
			double offsetX = -Vector3D.DotProduct(vector3D2, vector2);
			double offsetY = -Vector3D.DotProduct(vector, vector2);
			double offsetZ = -Vector3D.DotProduct(vector3D, vector2);
			Matrix3D result = new Matrix3D(vector3D2.X, vector.X, vector3D.X, 0.0, vector3D2.Y, vector.Y, vector3D.Y, 0.0, vector3D2.Z, vector.Z, vector3D.Z, 0.0, offsetX, offsetY, offsetZ, 1.0);
			Camera.PrependInverseTransform(transform, ref result);
			return result;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.ProjectionCamera" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060030B1 RID: 12465 RVA: 0x000C29E0 File Offset: 0x000C1DE0
		public new ProjectionCamera Clone()
		{
			return (ProjectionCamera)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.ProjectionCamera" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060030B2 RID: 12466 RVA: 0x000C29F8 File Offset: 0x000C1DF8
		public new ProjectionCamera CloneCurrentValue()
		{
			return (ProjectionCamera)base.CloneCurrentValue();
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x000C2A10 File Offset: 0x000C1E10
		private static void NearPlaneDistancePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ProjectionCamera projectionCamera = (ProjectionCamera)d;
			projectionCamera.PropertyChanged(ProjectionCamera.NearPlaneDistanceProperty);
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x000C2A30 File Offset: 0x000C1E30
		private static void FarPlaneDistancePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ProjectionCamera projectionCamera = (ProjectionCamera)d;
			projectionCamera.PropertyChanged(ProjectionCamera.FarPlaneDistanceProperty);
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x000C2A50 File Offset: 0x000C1E50
		private static void PositionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ProjectionCamera projectionCamera = (ProjectionCamera)d;
			projectionCamera.PropertyChanged(ProjectionCamera.PositionProperty);
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x000C2A70 File Offset: 0x000C1E70
		private static void LookDirectionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ProjectionCamera projectionCamera = (ProjectionCamera)d;
			projectionCamera.PropertyChanged(ProjectionCamera.LookDirectionProperty);
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x000C2A90 File Offset: 0x000C1E90
		private static void UpDirectionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ProjectionCamera projectionCamera = (ProjectionCamera)d;
			projectionCamera.PropertyChanged(ProjectionCamera.UpDirectionProperty);
		}

		/// <summary>Obtém ou define um valor que especifica a distância entre o plano de recorte próximo da câmera e a câmera propriamente dita.</summary>
		/// <returns>Double que especifica a distância da câmera de perto o plano de recorte da câmera.</returns>
		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x060030B8 RID: 12472 RVA: 0x000C2AB0 File Offset: 0x000C1EB0
		// (set) Token: 0x060030B9 RID: 12473 RVA: 0x000C2AD0 File Offset: 0x000C1ED0
		public double NearPlaneDistance
		{
			get
			{
				return (double)base.GetValue(ProjectionCamera.NearPlaneDistanceProperty);
			}
			set
			{
				base.SetValueInternal(ProjectionCamera.NearPlaneDistanceProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que especifica a distância entre o plano de recorte distante da câmera e a câmera propriamente dita.</summary>
		/// <returns>Double que especifica a distância da câmera do plano de recorte distante da câmera.</returns>
		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x060030BA RID: 12474 RVA: 0x000C2AF0 File Offset: 0x000C1EF0
		// (set) Token: 0x060030BB RID: 12475 RVA: 0x000C2B10 File Offset: 0x000C1F10
		public double FarPlaneDistance
		{
			get
			{
				return (double)base.GetValue(ProjectionCamera.FarPlaneDistanceProperty);
			}
			set
			{
				base.SetValueInternal(ProjectionCamera.FarPlaneDistanceProperty, value);
			}
		}

		/// <summary>Obtém ou define a posição da câmera em coordenadas de mundo.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Media3D.Point3D" /> que especifica a posição da câmera.</returns>
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x060030BC RID: 12476 RVA: 0x000C2B30 File Offset: 0x000C1F30
		// (set) Token: 0x060030BD RID: 12477 RVA: 0x000C2B50 File Offset: 0x000C1F50
		public Point3D Position
		{
			get
			{
				return (Point3D)base.GetValue(ProjectionCamera.PositionProperty);
			}
			set
			{
				base.SetValueInternal(ProjectionCamera.PositionProperty, value);
			}
		}

		/// <summary>Obtém ou define um <see cref="T:System.Windows.Media.Media3D.Vector3D" /> que define a direção na qual a câmera está apontando nas coordenadas de mundo.</summary>
		/// <returns>Vector3D que representa a direção do campo de visão da câmera.</returns>
		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x060030BE RID: 12478 RVA: 0x000C2B70 File Offset: 0x000C1F70
		// (set) Token: 0x060030BF RID: 12479 RVA: 0x000C2B90 File Offset: 0x000C1F90
		public Vector3D LookDirection
		{
			get
			{
				return (Vector3D)base.GetValue(ProjectionCamera.LookDirectionProperty);
			}
			set
			{
				base.SetValueInternal(ProjectionCamera.LookDirectionProperty, value);
			}
		}

		/// <summary>Obtém ou define um <see cref="T:System.Windows.Media.Media3D.Vector3D" /> que define a direção para cima da câmera.</summary>
		/// <returns>Vector3D que representa a direção para cima na projeção de cena.</returns>
		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x060030C0 RID: 12480 RVA: 0x000C2BB0 File Offset: 0x000C1FB0
		// (set) Token: 0x060030C1 RID: 12481 RVA: 0x000C2BD0 File Offset: 0x000C1FD0
		public Vector3D UpDirection
		{
			get
			{
				return (Vector3D)base.GetValue(ProjectionCamera.UpDirectionProperty);
			}
			set
			{
				base.SetValueInternal(ProjectionCamera.UpDirectionProperty, value);
			}
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x000C2BF0 File Offset: 0x000C1FF0
		static ProjectionCamera()
		{
			Type typeFromHandle = typeof(ProjectionCamera);
			ProjectionCamera.NearPlaneDistanceProperty = Animatable.RegisterProperty("NearPlaneDistance", typeof(double), typeFromHandle, 0.125, new PropertyChangedCallback(ProjectionCamera.NearPlaneDistancePropertyChanged), null, true, null);
			ProjectionCamera.FarPlaneDistanceProperty = Animatable.RegisterProperty("FarPlaneDistance", typeof(double), typeFromHandle, double.PositiveInfinity, new PropertyChangedCallback(ProjectionCamera.FarPlaneDistancePropertyChanged), null, true, null);
			ProjectionCamera.PositionProperty = Animatable.RegisterProperty("Position", typeof(Point3D), typeFromHandle, default(Point3D), new PropertyChangedCallback(ProjectionCamera.PositionPropertyChanged), null, true, null);
			ProjectionCamera.LookDirectionProperty = Animatable.RegisterProperty("LookDirection", typeof(Vector3D), typeFromHandle, new Vector3D(0.0, 0.0, -1.0), new PropertyChangedCallback(ProjectionCamera.LookDirectionPropertyChanged), null, true, null);
			ProjectionCamera.UpDirectionProperty = Animatable.RegisterProperty("UpDirection", typeof(Vector3D), typeFromHandle, new Vector3D(0.0, 1.0, 0.0), new PropertyChangedCallback(ProjectionCamera.UpDirectionPropertyChanged), null, true, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.ProjectionCamera.NearPlaneDistance" />.</summary>
		// Token: 0x0400155D RID: 5469
		public static readonly DependencyProperty NearPlaneDistanceProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.ProjectionCamera.FarPlaneDistance" />.</summary>
		// Token: 0x0400155E RID: 5470
		public static readonly DependencyProperty FarPlaneDistanceProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.ProjectionCamera.Position" />.</summary>
		// Token: 0x0400155F RID: 5471
		public static readonly DependencyProperty PositionProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.ProjectionCamera.LookDirection" />.</summary>
		// Token: 0x04001560 RID: 5472
		public static readonly DependencyProperty LookDirectionProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.ProjectionCamera.UpDirection" />.</summary>
		// Token: 0x04001561 RID: 5473
		public static readonly DependencyProperty UpDirectionProperty;

		// Token: 0x04001562 RID: 5474
		internal const double c_NearPlaneDistance = 0.125;

		// Token: 0x04001563 RID: 5475
		internal const double c_FarPlaneDistance = double.PositiveInfinity;

		// Token: 0x04001564 RID: 5476
		internal static Point3D s_Position = default(Point3D);

		// Token: 0x04001565 RID: 5477
		internal static Vector3D s_LookDirection = new Vector3D(0.0, 0.0, -1.0);

		// Token: 0x04001566 RID: 5478
		internal static Vector3D s_UpDirection = new Vector3D(0.0, 1.0, 0.0);
	}
}
