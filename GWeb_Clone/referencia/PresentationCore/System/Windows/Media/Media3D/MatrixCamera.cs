using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal.Media3D;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Media3D
{
	/// <summary>Câmera que especifica as transformações de projeção e exibição como objetos <see cref="T:System.Windows.Media.Media3D.Matrix3D" /></summary>
	// Token: 0x02000465 RID: 1125
	public sealed class MatrixCamera : Camera
	{
		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Media3D.MatrixCamera" />.</summary>
		// Token: 0x06002F33 RID: 12083 RVA: 0x000BD400 File Offset: 0x000BC800
		public MatrixCamera()
		{
		}

		/// <summary>Cria um novo <see cref="T:System.Windows.Media.Media3D.MatrixCamera" /> de matrizes de projeção e exibição.</summary>
		/// <param name="viewMatrix">Especifica a matriz de exibição da câmera.</param>
		/// <param name="projectionMatrix">Especifica a matriz de projeção da câmera.</param>
		// Token: 0x06002F34 RID: 12084 RVA: 0x000BD414 File Offset: 0x000BC814
		public MatrixCamera(Matrix3D viewMatrix, Matrix3D projectionMatrix)
		{
			this.ViewMatrix = viewMatrix;
			this.ProjectionMatrix = projectionMatrix;
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x000BD438 File Offset: 0x000BC838
		internal override Matrix3D GetViewMatrix()
		{
			Matrix3D viewMatrix = this.ViewMatrix;
			Camera.PrependInverseTransform(base.Transform, ref viewMatrix);
			return viewMatrix;
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x000BD45C File Offset: 0x000BC85C
		internal override Matrix3D GetProjectionMatrix(double aspectRatio)
		{
			return this.ProjectionMatrix;
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x000BD470 File Offset: 0x000BC870
		internal override RayHitTestParameters RayFromViewportPoint(Point p, Size viewSize, Rect3D boundingRect, out double distanceAdjustment)
		{
			Point normalizedPoint = M3DUtil.GetNormalizedPoint(p, viewSize);
			Matrix3D matrix3D = this.GetViewMatrix() * this.ProjectionMatrix;
			Matrix3D matrix = matrix3D;
			if (!matrix.HasInverse)
			{
				throw new NotSupportedException(SR.Get("HitTest_Singular"));
			}
			matrix.Invert();
			Point4D point4D = new Point4D(normalizedPoint.X, normalizedPoint.Y, 0.0, 1.0) * matrix;
			Point3D origin = new Point3D(point4D.X / point4D.W, point4D.Y / point4D.W, point4D.Z / point4D.W);
			double x = matrix.M31 - matrix.M34 * origin.X;
			double y = matrix.M32 - matrix.M34 * origin.Y;
			double z = matrix.M33 - matrix.M34 * origin.Z;
			Vector3D vector3D = new Vector3D(x, y, z);
			vector3D.Normalize();
			if (point4D.W < 0.0)
			{
				vector3D = -vector3D;
			}
			RayHitTestParameters rayHitTestParameters = new RayHitTestParameters(origin, vector3D);
			Matrix3D matrix2 = default(Matrix3D);
			matrix2.TranslatePrepend(new Vector3D(-p.X, viewSize.Height - p.Y, 0.0));
			matrix2.ScalePrepend(new Vector3D(viewSize.Width / 2.0, -viewSize.Height / 2.0, 1.0));
			matrix2.TranslatePrepend(new Vector3D(1.0, 1.0, 0.0));
			rayHitTestParameters.HitTestProjectionMatrix = matrix3D * matrix2;
			distanceAdjustment = 0.0;
			return rayHitTestParameters;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.MatrixCamera" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002F38 RID: 12088 RVA: 0x000BD64C File Offset: 0x000BCA4C
		public new MatrixCamera Clone()
		{
			return (MatrixCamera)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.MatrixCamera" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002F39 RID: 12089 RVA: 0x000BD664 File Offset: 0x000BCA64
		public new MatrixCamera CloneCurrentValue()
		{
			return (MatrixCamera)base.CloneCurrentValue();
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x000BD67C File Offset: 0x000BCA7C
		private static void ViewMatrixPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			MatrixCamera matrixCamera = (MatrixCamera)d;
			matrixCamera.PropertyChanged(MatrixCamera.ViewMatrixProperty);
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x000BD69C File Offset: 0x000BCA9C
		private static void ProjectionMatrixPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			MatrixCamera matrixCamera = (MatrixCamera)d;
			matrixCamera.PropertyChanged(MatrixCamera.ProjectionMatrixProperty);
		}

		/// <summary>Obtém ou define um <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> como a matriz de transformação de exibição.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> que representa a posição, direção de procurar e 1&amp;gt;vetor para a câmera.</returns>
		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06002F3C RID: 12092 RVA: 0x000BD6BC File Offset: 0x000BCABC
		// (set) Token: 0x06002F3D RID: 12093 RVA: 0x000BD6DC File Offset: 0x000BCADC
		public Matrix3D ViewMatrix
		{
			get
			{
				return (Matrix3D)base.GetValue(MatrixCamera.ViewMatrixProperty);
			}
			set
			{
				base.SetValueInternal(MatrixCamera.ViewMatrixProperty, value);
			}
		}

		/// <summary>Obtém ou define um <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> como a matriz de transformação de projeção.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> que especifica a transformação de projeção.</returns>
		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06002F3E RID: 12094 RVA: 0x000BD6FC File Offset: 0x000BCAFC
		// (set) Token: 0x06002F3F RID: 12095 RVA: 0x000BD71C File Offset: 0x000BCB1C
		public Matrix3D ProjectionMatrix
		{
			get
			{
				return (Matrix3D)base.GetValue(MatrixCamera.ProjectionMatrixProperty);
			}
			set
			{
				base.SetValueInternal(MatrixCamera.ProjectionMatrixProperty, value);
			}
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x000BD73C File Offset: 0x000BCB3C
		protected override Freezable CreateInstanceCore()
		{
			return new MatrixCamera();
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x000BD750 File Offset: 0x000BCB50
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
				DUCE.MILCMD_MATRIXCAMERA milcmd_MATRIXCAMERA;
				milcmd_MATRIXCAMERA.Type = MILCMD.MilCmdMatrixCamera;
				milcmd_MATRIXCAMERA.Handle = this._duceResource.GetHandle(channel);
				milcmd_MATRIXCAMERA.htransform = htransform;
				milcmd_MATRIXCAMERA.viewMatrix = CompositionResourceManager.Matrix3DToD3DMATRIX(this.ViewMatrix);
				milcmd_MATRIXCAMERA.projectionMatrix = CompositionResourceManager.Matrix3DToD3DMATRIX(this.ProjectionMatrix);
				channel.SendCommand((byte*)(&milcmd_MATRIXCAMERA), sizeof(DUCE.MILCMD_MATRIXCAMERA));
			}
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x000BD7F4 File Offset: 0x000BCBF4
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_MATRIXCAMERA))
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

		// Token: 0x06002F43 RID: 12099 RVA: 0x000BD840 File Offset: 0x000BCC40
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

		// Token: 0x06002F44 RID: 12100 RVA: 0x000BD874 File Offset: 0x000BCC74
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x000BD890 File Offset: 0x000BCC90
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x000BD8A8 File Offset: 0x000BCCA8
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x000BD8C4 File Offset: 0x000BCCC4
		static MatrixCamera()
		{
			Type typeFromHandle = typeof(MatrixCamera);
			MatrixCamera.ViewMatrixProperty = Animatable.RegisterProperty("ViewMatrix", typeof(Matrix3D), typeFromHandle, Matrix3D.Identity, new PropertyChangedCallback(MatrixCamera.ViewMatrixPropertyChanged), null, false, null);
			MatrixCamera.ProjectionMatrixProperty = Animatable.RegisterProperty("ProjectionMatrix", typeof(Matrix3D), typeFromHandle, Matrix3D.Identity, new PropertyChangedCallback(MatrixCamera.ProjectionMatrixPropertyChanged), null, false, null);
		}

		/// <summary>Obtém a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.MatrixCamera.ViewMatrix" />.</summary>
		// Token: 0x04001522 RID: 5410
		public static readonly DependencyProperty ViewMatrixProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.MatrixCamera.ProjectionMatrix" />.</summary>
		// Token: 0x04001523 RID: 5411
		public static readonly DependencyProperty ProjectionMatrixProperty;

		// Token: 0x04001524 RID: 5412
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x04001525 RID: 5413
		internal static Matrix3D s_ViewMatrix = Matrix3D.Identity;

		// Token: 0x04001526 RID: 5414
		internal static Matrix3D s_ProjectionMatrix = Matrix3D.Identity;
	}
}
