using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Media3D
{
	/// <summary>Cria uma transformação especificada por um <see cref="T:System.Windows.Media.Media3D.Matrix3D" />, usada para manipular objetos ou coordenar sistemas no espaço do mundo 3D.</summary>
	// Token: 0x02000466 RID: 1126
	public sealed class MatrixTransform3D : Transform3D
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.MatrixTransform3D" />.</summary>
		// Token: 0x06002F48 RID: 12104 RVA: 0x000BD958 File Offset: 0x000BCD58
		public MatrixTransform3D()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.MatrixTransform3D" /> usando o <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> especificado.</summary>
		/// <param name="matrix">Uma Matrix3D que especifica a transformação.</param>
		// Token: 0x06002F49 RID: 12105 RVA: 0x000BD96C File Offset: 0x000BCD6C
		public MatrixTransform3D(Matrix3D matrix)
		{
			this.Matrix = matrix;
		}

		/// <summary>Obtém uma representação da matriz da transformação 3D.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> representação o 3D transformação.</returns>
		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06002F4A RID: 12106 RVA: 0x000BD988 File Offset: 0x000BCD88
		public override Matrix3D Value
		{
			get
			{
				return this.Matrix;
			}
		}

		/// <summary>Obtém um valor que indica se a transformação é afim.</summary>
		/// <returns>
		///   <see langword="true" /> Se a transformação é afim; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06002F4B RID: 12107 RVA: 0x000BD99C File Offset: 0x000BCD9C
		public override bool IsAffine
		{
			get
			{
				return this.Matrix.IsAffine;
			}
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x000BD9B8 File Offset: 0x000BCDB8
		internal override void Append(ref Matrix3D matrix)
		{
			matrix *= this.Matrix;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.MatrixTransform3D" /> e faz cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (embora não possam mais ser resolvidas), mas não copia animações nem seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002F4D RID: 12109 RVA: 0x000BD9DC File Offset: 0x000BCDDC
		public new MatrixTransform3D Clone()
		{
			return (MatrixTransform3D)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.MatrixTransform3D" /> e faz cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002F4E RID: 12110 RVA: 0x000BD9F4 File Offset: 0x000BCDF4
		public new MatrixTransform3D CloneCurrentValue()
		{
			return (MatrixTransform3D)base.CloneCurrentValue();
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x000BDA0C File Offset: 0x000BCE0C
		private static void MatrixPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			MatrixTransform3D matrixTransform3D = (MatrixTransform3D)d;
			matrixTransform3D.PropertyChanged(MatrixTransform3D.MatrixProperty);
		}

		/// <summary>Obtém ou define um <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> que especifica uma transformação 3D.</summary>
		/// <returns>Uma Matrix3D que especifica um 3D transformação.</returns>
		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06002F50 RID: 12112 RVA: 0x000BDA2C File Offset: 0x000BCE2C
		// (set) Token: 0x06002F51 RID: 12113 RVA: 0x000BDA4C File Offset: 0x000BCE4C
		public Matrix3D Matrix
		{
			get
			{
				return (Matrix3D)base.GetValue(MatrixTransform3D.MatrixProperty);
			}
			set
			{
				base.SetValueInternal(MatrixTransform3D.MatrixProperty, value);
			}
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x000BDA6C File Offset: 0x000BCE6C
		protected override Freezable CreateInstanceCore()
		{
			return new MatrixTransform3D();
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x000BDA80 File Offset: 0x000BCE80
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				DUCE.MILCMD_MATRIXTRANSFORM3D milcmd_MATRIXTRANSFORM3D;
				milcmd_MATRIXTRANSFORM3D.Type = MILCMD.MilCmdMatrixTransform3D;
				milcmd_MATRIXTRANSFORM3D.Handle = this._duceResource.GetHandle(channel);
				milcmd_MATRIXTRANSFORM3D.matrix = CompositionResourceManager.Matrix3DToD3DMATRIX(this.Matrix);
				channel.SendCommand((byte*)(&milcmd_MATRIXTRANSFORM3D), sizeof(DUCE.MILCMD_MATRIXTRANSFORM3D));
			}
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x000BDAE4 File Offset: 0x000BCEE4
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_MATRIXTRANSFORM3D))
			{
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x000BDB20 File Offset: 0x000BCF20
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000BDB44 File Offset: 0x000BCF44
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x000BDB60 File Offset: 0x000BCF60
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x000BDB78 File Offset: 0x000BCF78
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06002F59 RID: 12121 RVA: 0x000BDB94 File Offset: 0x000BCF94
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x000BDBA4 File Offset: 0x000BCFA4
		static MatrixTransform3D()
		{
			Type typeFromHandle = typeof(MatrixTransform3D);
			MatrixTransform3D.MatrixProperty = Animatable.RegisterProperty("Matrix", typeof(Matrix3D), typeFromHandle, Matrix3D.Identity, new PropertyChangedCallback(MatrixTransform3D.MatrixPropertyChanged), null, false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.MatrixTransform3D.Matrix" />.</summary>
		// Token: 0x04001527 RID: 5415
		public static readonly DependencyProperty MatrixProperty;

		// Token: 0x04001528 RID: 5416
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x04001529 RID: 5417
		internal static Matrix3D s_Matrix = Matrix3D.Identity;
	}
}
