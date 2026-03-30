using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;

namespace System.Windows.Media
{
	/// <summary>Pinta uma área com um gradiente linear.</summary>
	// Token: 0x020003BB RID: 955
	public sealed class LinearGradientBrush : GradientBrush
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.LinearGradientBrush" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060024C9 RID: 9417 RVA: 0x00093898 File Offset: 0x00092C98
		public new LinearGradientBrush Clone()
		{
			return (LinearGradientBrush)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.LinearGradientBrush" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060024CA RID: 9418 RVA: 0x000938B0 File Offset: 0x00092CB0
		public new LinearGradientBrush CloneCurrentValue()
		{
			return (LinearGradientBrush)base.CloneCurrentValue();
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x000938C8 File Offset: 0x00092CC8
		private static void StartPointPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LinearGradientBrush linearGradientBrush = (LinearGradientBrush)d;
			linearGradientBrush.PropertyChanged(LinearGradientBrush.StartPointProperty);
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x000938E8 File Offset: 0x00092CE8
		private static void EndPointPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LinearGradientBrush linearGradientBrush = (LinearGradientBrush)d;
			linearGradientBrush.PropertyChanged(LinearGradientBrush.EndPointProperty);
		}

		/// <summary>Obtém ou define as coordenadas bidimensionais de início do gradiente linear.</summary>
		/// <returns>As coordenadas bidimensionais de início do gradiente linear. O padrão é (0, 0).</returns>
		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060024CD RID: 9421 RVA: 0x00093908 File Offset: 0x00092D08
		// (set) Token: 0x060024CE RID: 9422 RVA: 0x00093928 File Offset: 0x00092D28
		public Point StartPoint
		{
			get
			{
				return (Point)base.GetValue(LinearGradientBrush.StartPointProperty);
			}
			set
			{
				base.SetValueInternal(LinearGradientBrush.StartPointProperty, value);
			}
		}

		/// <summary>Obtém ou define as coordenadas bidimensionais de encerramento do gradiente linear.</summary>
		/// <returns>As coordenadas bidimensionais de encerramento do gradiente linear. O padrão é (1,1).</returns>
		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060024CF RID: 9423 RVA: 0x00093948 File Offset: 0x00092D48
		// (set) Token: 0x060024D0 RID: 9424 RVA: 0x00093968 File Offset: 0x00092D68
		public Point EndPoint
		{
			get
			{
				return (Point)base.GetValue(LinearGradientBrush.EndPointProperty);
			}
			set
			{
				base.SetValueInternal(LinearGradientBrush.EndPointProperty, value);
			}
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x00093988 File Offset: 0x00092D88
		protected override Freezable CreateInstanceCore()
		{
			return new LinearGradientBrush();
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x0009399C File Offset: 0x00092D9C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			this.ManualUpdateResource(channel, skipOnChannelCheck);
			base.UpdateResource(channel, skipOnChannelCheck);
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000939BC File Offset: 0x00092DBC
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_LINEARGRADIENTBRUSH))
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

		// Token: 0x060024D4 RID: 9428 RVA: 0x00093A1C File Offset: 0x00092E1C
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

		// Token: 0x060024D5 RID: 9429 RVA: 0x00093A60 File Offset: 0x00092E60
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x00093A7C File Offset: 0x00092E7C
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x00093A94 File Offset: 0x00092E94
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060024D8 RID: 9432 RVA: 0x00093AB0 File Offset: 0x00092EB0
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x00093AC0 File Offset: 0x00092EC0
		static LinearGradientBrush()
		{
			Type typeFromHandle = typeof(LinearGradientBrush);
			LinearGradientBrush.StartPointProperty = Animatable.RegisterProperty("StartPoint", typeof(Point), typeFromHandle, new Point(0.0, 0.0), new PropertyChangedCallback(LinearGradientBrush.StartPointPropertyChanged), null, true, null);
			LinearGradientBrush.EndPointProperty = Animatable.RegisterProperty("EndPoint", typeof(Point), typeFromHandle, new Point(1.0, 1.0), new PropertyChangedCallback(LinearGradientBrush.EndPointPropertyChanged), null, true, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.LinearGradientBrush" />.</summary>
		// Token: 0x060024DA RID: 9434 RVA: 0x00093B9C File Offset: 0x00092F9C
		public LinearGradientBrush()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.LinearGradientBrush" /> que tem o início <see cref="T:System.Windows.Media.Color" />, final <see cref="T:System.Windows.Media.Color" /> e ângulo especificados.</summary>
		/// <param name="startColor">O <see cref="T:System.Windows.Media.Color" /> no deslocamento 0,0.</param>
		/// <param name="endColor">O <see cref="T:System.Windows.Media.Color" /> no deslocamento 1,0.</param>
		/// <param name="angle">Um <see cref="T:System.Double" /> que representa o ângulo, em graus, do gradiente. Um valor de 0,0 cria um gradiente horizontal e um valor 90,0 cria um gradiente vertical.</param>
		// Token: 0x060024DB RID: 9435 RVA: 0x00093BB0 File Offset: 0x00092FB0
		public LinearGradientBrush(Color startColor, Color endColor, double angle)
		{
			this.EndPoint = this.EndPointFromAngle(angle);
			base.GradientStops.Add(new GradientStop(startColor, 0.0));
			base.GradientStops.Add(new GradientStop(endColor, 1.0));
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.LinearGradientBrush" /> que tem o <see cref="T:System.Windows.Media.Color" /> inicial, o <see cref="T:System.Windows.Media.Color" /> final, o <see cref="P:System.Windows.Media.LinearGradientBrush.StartPoint" /> e o <see cref="P:System.Windows.Media.LinearGradientBrush.EndPoint" /> especificados.</summary>
		/// <param name="startColor">O <see cref="T:System.Windows.Media.Color" /> no deslocamento 0,0.</param>
		/// <param name="endColor">O <see cref="T:System.Windows.Media.Color" /> no deslocamento 1,0.</param>
		/// <param name="startPoint">O <see cref="P:System.Windows.Media.LinearGradientBrush.StartPoint" /> do gradiente.</param>
		/// <param name="endPoint">O <see cref="P:System.Windows.Media.LinearGradientBrush.EndPoint" /> do gradiente.</param>
		// Token: 0x060024DC RID: 9436 RVA: 0x00093C04 File Offset: 0x00093004
		public LinearGradientBrush(Color startColor, Color endColor, Point startPoint, Point endPoint)
		{
			this.StartPoint = startPoint;
			this.EndPoint = endPoint;
			base.GradientStops.Add(new GradientStop(startColor, 0.0));
			base.GradientStops.Add(new GradientStop(endColor, 1.0));
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.LinearGradientBrush" /> que tem as marcas de gradiente especificadas.</summary>
		/// <param name="gradientStopCollection">O <see cref="P:System.Windows.Media.GradientBrush.GradientStops" /> a definir neste pincel.</param>
		// Token: 0x060024DD RID: 9437 RVA: 0x00093C5C File Offset: 0x0009305C
		public LinearGradientBrush(GradientStopCollection gradientStopCollection) : base(gradientStopCollection)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.LinearGradientBrush" /> que tem o <see cref="T:System.Windows.Media.GradientStopCollection" /> e o ângulo especificados.</summary>
		/// <param name="gradientStopCollection">O <see cref="P:System.Windows.Media.GradientBrush.GradientStops" /> a definir neste pincel.</param>
		/// <param name="angle">Um <see cref="T:System.Double" /> que representa o ângulo, em graus, do gradiente. Um valor de 0,0 cria um gradiente horizontal e um valor 90,0 cria um gradiente vertical.</param>
		// Token: 0x060024DE RID: 9438 RVA: 0x00093C70 File Offset: 0x00093070
		public LinearGradientBrush(GradientStopCollection gradientStopCollection, double angle) : base(gradientStopCollection)
		{
			this.EndPoint = this.EndPointFromAngle(angle);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.LinearGradientBrush" /> que tem as paradas de gradiente especificadas, <see cref="P:System.Windows.Media.LinearGradientBrush.StartPoint" /> e <see cref="P:System.Windows.Media.LinearGradientBrush.EndPoint" />.</summary>
		/// <param name="gradientStopCollection">O <see cref="P:System.Windows.Media.GradientBrush.GradientStops" /> a definir neste pincel.</param>
		/// <param name="startPoint">O <see cref="P:System.Windows.Media.LinearGradientBrush.StartPoint" /> do gradiente.</param>
		/// <param name="endPoint">O <see cref="P:System.Windows.Media.LinearGradientBrush.EndPoint" /> do gradiente.</param>
		// Token: 0x060024DF RID: 9439 RVA: 0x00093C94 File Offset: 0x00093094
		public LinearGradientBrush(GradientStopCollection gradientStopCollection, Point startPoint, Point endPoint) : base(gradientStopCollection)
		{
			this.StartPoint = startPoint;
			this.EndPoint = endPoint;
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x00093CB8 File Offset: 0x000930B8
		[SecurityTreatAsSafe]
		[SecurityCritical]
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
				DUCE.ResourceHandle animationResourceHandle2 = base.GetAnimationResourceHandle(LinearGradientBrush.StartPointProperty, channel);
				DUCE.ResourceHandle animationResourceHandle3 = base.GetAnimationResourceHandle(LinearGradientBrush.EndPointProperty, channel);
				DUCE.MILCMD_LINEARGRADIENTBRUSH milcmd_LINEARGRADIENTBRUSH;
				milcmd_LINEARGRADIENTBRUSH.Type = MILCMD.MilCmdLinearGradientBrush;
				milcmd_LINEARGRADIENTBRUSH.Handle = this._duceResource.GetHandle(channel);
				double opacity = base.Opacity;
				DUCE.CopyBytes((byte*)(&milcmd_LINEARGRADIENTBRUSH.Opacity), (byte*)(&opacity), 8);
				milcmd_LINEARGRADIENTBRUSH.hOpacityAnimations = animationResourceHandle;
				milcmd_LINEARGRADIENTBRUSH.hTransform = hTransform;
				milcmd_LINEARGRADIENTBRUSH.hRelativeTransform = hRelativeTransform;
				milcmd_LINEARGRADIENTBRUSH.ColorInterpolationMode = base.ColorInterpolationMode;
				milcmd_LINEARGRADIENTBRUSH.MappingMode = base.MappingMode;
				milcmd_LINEARGRADIENTBRUSH.SpreadMethod = base.SpreadMethod;
				Point startPoint = this.StartPoint;
				DUCE.CopyBytes((byte*)(&milcmd_LINEARGRADIENTBRUSH.StartPoint), (byte*)(&startPoint), 16);
				milcmd_LINEARGRADIENTBRUSH.hStartPointAnimations = animationResourceHandle2;
				Point endPoint = this.EndPoint;
				DUCE.CopyBytes((byte*)(&milcmd_LINEARGRADIENTBRUSH.EndPoint), (byte*)(&endPoint), 16);
				milcmd_LINEARGRADIENTBRUSH.hEndPointAnimations = animationResourceHandle3;
				int num = (gradientStops == null) ? 0 : gradientStops.Count;
				milcmd_LINEARGRADIENTBRUSH.GradientStopsSize = (uint)(sizeof(DUCE.MIL_GRADIENTSTOP) * num);
				channel.BeginCommand((byte*)(&milcmd_LINEARGRADIENTBRUSH), sizeof(DUCE.MILCMD_LINEARGRADIENTBRUSH), sizeof(DUCE.MIL_GRADIENTSTOP) * num);
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

		// Token: 0x060024E1 RID: 9441 RVA: 0x00093EA0 File Offset: 0x000932A0
		private Point EndPointFromAngle(double angle)
		{
			angle = angle * 0.0055555555555555558 * 3.1415926535897931;
			return new Point(Math.Cos(angle), Math.Sin(angle));
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.LinearGradientBrush.StartPoint" />.</summary>
		// Token: 0x04001178 RID: 4472
		public static readonly DependencyProperty StartPointProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.LinearGradientBrush.EndPoint" />.</summary>
		// Token: 0x04001179 RID: 4473
		public static readonly DependencyProperty EndPointProperty;

		// Token: 0x0400117A RID: 4474
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x0400117B RID: 4475
		internal static Point s_StartPoint = new Point(0.0, 0.0);

		// Token: 0x0400117C RID: 4476
		internal static Point s_EndPoint = new Point(1.0, 1.0);
	}
}
