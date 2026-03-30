using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;

namespace System.Windows.Media
{
	/// <summary>Pinta uma área com um gradiente radial. Um ponto focal define o início do gradiente e um círculo define o ponto final do gradiente.</summary>
	// Token: 0x020003CF RID: 975
	public sealed class RadialGradientBrush : GradientBrush
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.RadialGradientBrush" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002692 RID: 9874 RVA: 0x00099998 File Offset: 0x00098D98
		public new RadialGradientBrush Clone()
		{
			return (RadialGradientBrush)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.RadialGradientBrush" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002693 RID: 9875 RVA: 0x000999B0 File Offset: 0x00098DB0
		public new RadialGradientBrush CloneCurrentValue()
		{
			return (RadialGradientBrush)base.CloneCurrentValue();
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x000999C8 File Offset: 0x00098DC8
		private static void CenterPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			RadialGradientBrush radialGradientBrush = (RadialGradientBrush)d;
			radialGradientBrush.PropertyChanged(RadialGradientBrush.CenterProperty);
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x000999E8 File Offset: 0x00098DE8
		private static void RadiusXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			RadialGradientBrush radialGradientBrush = (RadialGradientBrush)d;
			radialGradientBrush.PropertyChanged(RadialGradientBrush.RadiusXProperty);
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x00099A08 File Offset: 0x00098E08
		private static void RadiusYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			RadialGradientBrush radialGradientBrush = (RadialGradientBrush)d;
			radialGradientBrush.PropertyChanged(RadialGradientBrush.RadiusYProperty);
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x00099A28 File Offset: 0x00098E28
		private static void GradientOriginPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			RadialGradientBrush radialGradientBrush = (RadialGradientBrush)d;
			radialGradientBrush.PropertyChanged(RadialGradientBrush.GradientOriginProperty);
		}

		/// <summary>Obtém ou define o centro do círculo mais externo do gradiente radial.</summary>
		/// <returns>O ponto bidimensional localizado no centro do gradiente radial.</returns>
		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06002698 RID: 9880 RVA: 0x00099A48 File Offset: 0x00098E48
		// (set) Token: 0x06002699 RID: 9881 RVA: 0x00099A68 File Offset: 0x00098E68
		public Point Center
		{
			get
			{
				return (Point)base.GetValue(RadialGradientBrush.CenterProperty);
			}
			set
			{
				base.SetValueInternal(RadialGradientBrush.CenterProperty, value);
			}
		}

		/// <summary>Obtém ou define o raio horizontal do círculo mais externo do gradiente radial.</summary>
		/// <returns>O raio horizontal do círculo externo do gradiente radial. O padrão é 0,5.</returns>
		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x0600269A RID: 9882 RVA: 0x00099A88 File Offset: 0x00098E88
		// (set) Token: 0x0600269B RID: 9883 RVA: 0x00099AA8 File Offset: 0x00098EA8
		public double RadiusX
		{
			get
			{
				return (double)base.GetValue(RadialGradientBrush.RadiusXProperty);
			}
			set
			{
				base.SetValueInternal(RadialGradientBrush.RadiusXProperty, value);
			}
		}

		/// <summary>Obtém ou define o raio vertical do círculo mais externo de um gradiente radial.</summary>
		/// <returns>O raio vertical do círculo mais externo de um gradiente radial. O padrão é 0,5.</returns>
		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x00099AC8 File Offset: 0x00098EC8
		// (set) Token: 0x0600269D RID: 9885 RVA: 0x00099AE8 File Offset: 0x00098EE8
		public double RadiusY
		{
			get
			{
				return (double)base.GetValue(RadialGradientBrush.RadiusYProperty);
			}
			set
			{
				base.SetValueInternal(RadialGradientBrush.RadiusYProperty, value);
			}
		}

		/// <summary>Obtém ou define o local do ponto focal bidimensional que define o início do gradiente.</summary>
		/// <returns>O local do ponto focal bidimensional do gradiente. O padrão é (0,5, 0,5).</returns>
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x0600269E RID: 9886 RVA: 0x00099B08 File Offset: 0x00098F08
		// (set) Token: 0x0600269F RID: 9887 RVA: 0x00099B28 File Offset: 0x00098F28
		public Point GradientOrigin
		{
			get
			{
				return (Point)base.GetValue(RadialGradientBrush.GradientOriginProperty);
			}
			set
			{
				base.SetValueInternal(RadialGradientBrush.GradientOriginProperty, value);
			}
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x00099B48 File Offset: 0x00098F48
		protected override Freezable CreateInstanceCore()
		{
			return new RadialGradientBrush();
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x00099B5C File Offset: 0x00098F5C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			this.ManualUpdateResource(channel, skipOnChannelCheck);
			base.UpdateResource(channel, skipOnChannelCheck);
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x00099B7C File Offset: 0x00098F7C
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_RADIALGRADIENTBRUSH))
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
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x00099BDC File Offset: 0x00098FDC
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
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x00099C20 File Offset: 0x00099020
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x00099C3C File Offset: 0x0009903C
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x00099C54 File Offset: 0x00099054
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060026A7 RID: 9895 RVA: 0x00099C70 File Offset: 0x00099070
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x00099C80 File Offset: 0x00099080
		static RadialGradientBrush()
		{
			Type typeFromHandle = typeof(RadialGradientBrush);
			RadialGradientBrush.CenterProperty = Animatable.RegisterProperty("Center", typeof(Point), typeFromHandle, new Point(0.5, 0.5), new PropertyChangedCallback(RadialGradientBrush.CenterPropertyChanged), null, true, null);
			RadialGradientBrush.RadiusXProperty = Animatable.RegisterProperty("RadiusX", typeof(double), typeFromHandle, 0.5, new PropertyChangedCallback(RadialGradientBrush.RadiusXPropertyChanged), null, true, null);
			RadialGradientBrush.RadiusYProperty = Animatable.RegisterProperty("RadiusY", typeof(double), typeFromHandle, 0.5, new PropertyChangedCallback(RadialGradientBrush.RadiusYPropertyChanged), null, true, null);
			RadialGradientBrush.GradientOriginProperty = Animatable.RegisterProperty("GradientOrigin", typeof(Point), typeFromHandle, new Point(0.5, 0.5), new PropertyChangedCallback(RadialGradientBrush.GradientOriginPropertyChanged), null, true, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.RadialGradientBrush" />.</summary>
		// Token: 0x060026A9 RID: 9897 RVA: 0x00099DC8 File Offset: 0x000991C8
		public RadialGradientBrush()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.RadialGradientBrush" /> com as cores inicial e de interrupção especificadas.</summary>
		/// <param name="startColor">Valor de cor no foco (<see cref="P:System.Windows.Media.RadialGradientBrush.GradientOrigin" />) do gradiente radial.</param>
		/// <param name="endColor">Valor de cor na borda externa do gradiente radial.</param>
		// Token: 0x060026AA RID: 9898 RVA: 0x00099DDC File Offset: 0x000991DC
		public RadialGradientBrush(Color startColor, Color endColor)
		{
			base.GradientStops.Add(new GradientStop(startColor, 0.0));
			base.GradientStops.Add(new GradientStop(endColor, 1.0));
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.RadialGradientBrush" /> que tem as marcas de gradiente especificadas.</summary>
		/// <param name="gradientStopCollection">As marcas de gradiente a definir nesse pincel.</param>
		// Token: 0x060026AB RID: 9899 RVA: 0x00099E24 File Offset: 0x00099224
		public RadialGradientBrush(GradientStopCollection gradientStopCollection) : base(gradientStopCollection)
		{
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x00099E38 File Offset: 0x00099238
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private unsafe void ManualUpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				Transform transform = base.Transform;
				Transform relativeTransform = base.RelativeTransform;
				GradientStopCollection gradientStops = base.GradientStops;
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
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(Brush.OpacityProperty, channel);
				DUCE.ResourceHandle animationResourceHandle2 = base.GetAnimationResourceHandle(RadialGradientBrush.CenterProperty, channel);
				DUCE.ResourceHandle animationResourceHandle3 = base.GetAnimationResourceHandle(RadialGradientBrush.RadiusXProperty, channel);
				DUCE.ResourceHandle animationResourceHandle4 = base.GetAnimationResourceHandle(RadialGradientBrush.RadiusYProperty, channel);
				DUCE.ResourceHandle animationResourceHandle5 = base.GetAnimationResourceHandle(RadialGradientBrush.GradientOriginProperty, channel);
				DUCE.MILCMD_RADIALGRADIENTBRUSH milcmd_RADIALGRADIENTBRUSH;
				milcmd_RADIALGRADIENTBRUSH.Type = MILCMD.MilCmdRadialGradientBrush;
				milcmd_RADIALGRADIENTBRUSH.Handle = this._duceResource.GetHandle(channel);
				double opacity = base.Opacity;
				DUCE.CopyBytes((byte*)(&milcmd_RADIALGRADIENTBRUSH.Opacity), (byte*)(&opacity), 8);
				milcmd_RADIALGRADIENTBRUSH.hOpacityAnimations = animationResourceHandle;
				milcmd_RADIALGRADIENTBRUSH.hTransform = hTransform;
				milcmd_RADIALGRADIENTBRUSH.hRelativeTransform = hRelativeTransform;
				milcmd_RADIALGRADIENTBRUSH.ColorInterpolationMode = base.ColorInterpolationMode;
				milcmd_RADIALGRADIENTBRUSH.MappingMode = base.MappingMode;
				milcmd_RADIALGRADIENTBRUSH.SpreadMethod = base.SpreadMethod;
				Point center = this.Center;
				DUCE.CopyBytes((byte*)(&milcmd_RADIALGRADIENTBRUSH.Center), (byte*)(&center), 16);
				milcmd_RADIALGRADIENTBRUSH.hCenterAnimations = animationResourceHandle2;
				double radiusX = this.RadiusX;
				DUCE.CopyBytes((byte*)(&milcmd_RADIALGRADIENTBRUSH.RadiusX), (byte*)(&radiusX), 8);
				milcmd_RADIALGRADIENTBRUSH.hRadiusXAnimations = animationResourceHandle3;
				double radiusY = this.RadiusY;
				DUCE.CopyBytes((byte*)(&milcmd_RADIALGRADIENTBRUSH.RadiusY), (byte*)(&radiusY), 8);
				milcmd_RADIALGRADIENTBRUSH.hRadiusYAnimations = animationResourceHandle4;
				Point gradientOrigin = this.GradientOrigin;
				DUCE.CopyBytes((byte*)(&milcmd_RADIALGRADIENTBRUSH.GradientOrigin), (byte*)(&gradientOrigin), 16);
				milcmd_RADIALGRADIENTBRUSH.hGradientOriginAnimations = animationResourceHandle5;
				int num = (gradientStops == null) ? 0 : gradientStops.Count;
				milcmd_RADIALGRADIENTBRUSH.GradientStopsSize = (uint)(sizeof(DUCE.MIL_GRADIENTSTOP) * num);
				channel.BeginCommand((byte*)(&milcmd_RADIALGRADIENTBRUSH), sizeof(DUCE.MILCMD_RADIALGRADIENTBRUSH), sizeof(DUCE.MIL_GRADIENTSTOP) * num);
				for (int i = 0; i < num; i++)
				{
					GradientStop gradientStop = gradientStops.Internal_GetItem(i);
					double offset = gradientStop.Offset;
					DUCE.MIL_GRADIENTSTOP mil_GRADIENTSTOP;
					DUCE.CopyBytes((byte*)(&mil_GRADIENTSTOP.Position), (byte*)(&offset), 8);
					mil_GRADIENTSTOP.Color = CompositionResourceManager.ColorToMilColorF(gradientStop.Color);
					channel.AppendCommandData((byte*)(&mil_GRADIENTSTOP), sizeof(DUCE.MIL_GRADIENTSTOP));
				}
				channel.EndCommand();
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.RadialGradientBrush.Center" />.</summary>
		// Token: 0x040011CF RID: 4559
		public static readonly DependencyProperty CenterProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.RadialGradientBrush.RadiusX" />.</summary>
		// Token: 0x040011D0 RID: 4560
		public static readonly DependencyProperty RadiusXProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.RadialGradientBrush.RadiusY" />.</summary>
		// Token: 0x040011D1 RID: 4561
		public static readonly DependencyProperty RadiusYProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.RadialGradientBrush.GradientOrigin" />.</summary>
		// Token: 0x040011D2 RID: 4562
		public static readonly DependencyProperty GradientOriginProperty;

		// Token: 0x040011D3 RID: 4563
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x040011D4 RID: 4564
		internal static Point s_Center = new Point(0.5, 0.5);

		// Token: 0x040011D5 RID: 4565
		internal const double c_RadiusX = 0.5;

		// Token: 0x040011D6 RID: 4566
		internal const double c_RadiusY = 0.5;

		// Token: 0x040011D7 RID: 4567
		internal static Point s_GradientOrigin = new Point(0.5, 0.5);
	}
}
