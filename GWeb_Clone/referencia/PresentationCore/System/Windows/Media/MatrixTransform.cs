using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal;

namespace System.Windows.Media
{
	/// <summary>Cria uma transformação da matriz arbitrária que é usada para manipular objetos ou sistemas de coordenadas em um plano 2D.</summary>
	// Token: 0x020003BE RID: 958
	public sealed class MatrixTransform : Transform
	{
		/// <summary>Cria uma cópia modificável deste <see cref="T:System.Windows.Media.MatrixTransform" /> fazendo cópias em profundidade de seus valores.</summary>
		/// <returns>Uma cópia em profundidade modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado retorna <see langword="false" /> mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem é <see langword="true." /></returns>
		// Token: 0x06002510 RID: 9488 RVA: 0x00094778 File Offset: 0x00093B78
		public new MatrixTransform Clone()
		{
			return (MatrixTransform)base.Clone();
		}

		/// <summary>Cria uma cópia modificável deste objeto <see cref="T:System.Windows.Media.MatrixTransform" /> fazendo cópias em profundidade de seus valores. Esse método não copia referências de recurso, associações de dados ou animações, embora ele copie os valores atuais.</summary>
		/// <returns>Uma cópia em profundidade modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado é <see langword="false" /> mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem é <see langword="true" />.</returns>
		// Token: 0x06002511 RID: 9489 RVA: 0x00094790 File Offset: 0x00093B90
		public new MatrixTransform CloneCurrentValue()
		{
			return (MatrixTransform)base.CloneCurrentValue();
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000947A8 File Offset: 0x00093BA8
		private static void MatrixPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			MatrixTransform matrixTransform = (MatrixTransform)d;
			matrixTransform.PropertyChanged(MatrixTransform.MatrixProperty);
		}

		/// <summary>Obtém ou define a estrutura <see cref="T:System.Windows.Media.Matrix" /> que define essa transformação.</summary>
		/// <returns>A estrutura <see cref="T:System.Windows.Media.Matrix" /> que define essa transformação. O valor padrão é um <see cref="T:System.Windows.Media.Matrix" /> de identidade. Uma matriz de identidade tem um valor de 1 nos coeficientes [1,1], [2,2], e [3,3]; e um valor de 0no restante dos coeficientes.</returns>
		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06002513 RID: 9491 RVA: 0x000947C8 File Offset: 0x00093BC8
		// (set) Token: 0x06002514 RID: 9492 RVA: 0x000947E8 File Offset: 0x00093BE8
		public Matrix Matrix
		{
			get
			{
				return (Matrix)base.GetValue(MatrixTransform.MatrixProperty);
			}
			set
			{
				base.SetValueInternal(MatrixTransform.MatrixProperty, value);
			}
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x00094808 File Offset: 0x00093C08
		protected override Freezable CreateInstanceCore()
		{
			return new MatrixTransform();
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x0009481C File Offset: 0x00093C1C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(MatrixTransform.MatrixProperty, channel);
				DUCE.MILCMD_MATRIXTRANSFORM milcmd_MATRIXTRANSFORM;
				milcmd_MATRIXTRANSFORM.Type = MILCMD.MilCmdMatrixTransform;
				milcmd_MATRIXTRANSFORM.Handle = this._duceResource.GetHandle(channel);
				if (animationResourceHandle.IsNull)
				{
					milcmd_MATRIXTRANSFORM.Matrix = CompositionResourceManager.MatrixToMilMatrix3x2D(this.Matrix);
				}
				milcmd_MATRIXTRANSFORM.hMatrixAnimations = animationResourceHandle;
				channel.SendCommand((byte*)(&milcmd_MATRIXTRANSFORM), sizeof(DUCE.MILCMD_MATRIXTRANSFORM));
			}
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x000948A0 File Offset: 0x00093CA0
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_MATRIXTRANSFORM))
			{
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x000948DC File Offset: 0x00093CDC
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x00094900 File Offset: 0x00093D00
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x0009491C File Offset: 0x00093D1C
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x00094934 File Offset: 0x00093D34
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x0600251C RID: 9500 RVA: 0x00094950 File Offset: 0x00093D50
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x00094960 File Offset: 0x00093D60
		static MatrixTransform()
		{
			Type typeFromHandle = typeof(MatrixTransform);
			MatrixTransform.MatrixProperty = Animatable.RegisterProperty("Matrix", typeof(Matrix), typeFromHandle, default(Matrix), new PropertyChangedCallback(MatrixTransform.MatrixPropertyChanged), null, true, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.MatrixTransform" />.</summary>
		// Token: 0x0600251E RID: 9502 RVA: 0x000949B0 File Offset: 0x00093DB0
		public MatrixTransform()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.MatrixTransform" /> com os valores da matriz de transformação especificados.</summary>
		/// <param name="m11">O valor na posição (1, 1) na matriz de transformação.</param>
		/// <param name="m12">O valor na posição (1, 2) na matriz de transformação.</param>
		/// <param name="m21">O valor na posição (2, 1) na matriz de transformação.</param>
		/// <param name="m22">O valor na posição (2, 2) na matriz de transformação.</param>
		/// <param name="offsetX">O fator de translação do eixo X, que está localizado na posição (3,1) na matriz de transformação.</param>
		/// <param name="offsetY">O fator de translação do eixo Y, que está localizado na posição (3,2) na matriz de transformação.</param>
		// Token: 0x0600251F RID: 9503 RVA: 0x000949C4 File Offset: 0x00093DC4
		public MatrixTransform(double m11, double m12, double m21, double m22, double offsetX, double offsetY)
		{
			this.Matrix = new Matrix(m11, m12, m21, m22, offsetX, offsetY);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.MatrixTransform" /> com a matriz de transformação especificada.</summary>
		/// <param name="matrix">A matriz de transformação do novo <see cref="T:System.Windows.Media.MatrixTransform" />.</param>
		// Token: 0x06002520 RID: 9504 RVA: 0x000949EC File Offset: 0x00093DEC
		public MatrixTransform(Matrix matrix)
		{
			this.Matrix = matrix;
		}

		/// <summary>Obtém o <see cref="P:System.Windows.Media.MatrixTransform.Matrix" /> que representa este <see cref="T:System.Windows.Media.MatrixTransform" />.</summary>
		/// <returns>A matriz que representa este <see cref="T:System.Windows.Media.MatrixTransform" />.</returns>
		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06002521 RID: 9505 RVA: 0x00094A08 File Offset: 0x00093E08
		public override Matrix Value
		{
			get
			{
				base.ReadPreamble();
				return this.Matrix;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002522 RID: 9506 RVA: 0x00094A24 File Offset: 0x00093E24
		internal override bool IsIdentity
		{
			get
			{
				return this.Matrix.IsIdentity && base.CanFreeze;
			}
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x00094A4C File Offset: 0x00093E4C
		internal override bool CanSerializeToString()
		{
			return base.CanFreeze;
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x00094A60 File Offset: 0x00093E60
		internal override string ConvertToString(string format, IFormatProvider provider)
		{
			if (!this.CanSerializeToString())
			{
				return base.ConvertToString(format, provider);
			}
			return ((IFormattable)this.Matrix).ToString(format, provider);
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x00094A90 File Offset: 0x00093E90
		internal override void TransformRect(ref Rect rect)
		{
			Matrix matrix = this.Matrix;
			MatrixUtil.TransformRect(ref rect, ref matrix);
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x00094AAC File Offset: 0x00093EAC
		internal override void MultiplyValueByMatrix(ref Matrix result, ref Matrix matrixToMultiplyBy)
		{
			result = this.Matrix;
			MatrixUtil.MultiplyMatrix(ref result, ref matrixToMultiplyBy);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.MatrixTransform.Matrix" />.</summary>
		// Token: 0x04001187 RID: 4487
		public static readonly DependencyProperty MatrixProperty;

		// Token: 0x04001188 RID: 4488
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x04001189 RID: 4489
		internal static Matrix s_Matrix;
	}
}
