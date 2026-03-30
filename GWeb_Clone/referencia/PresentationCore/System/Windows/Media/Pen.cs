using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Threading;
using MS.Internal;

namespace System.Windows.Media
{
	/// <summary>Descreve como uma forma é contornada.</summary>
	// Token: 0x020003C6 RID: 966
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public sealed class Pen : Animatable, DUCE.IResource
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Pen" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060025F6 RID: 9718 RVA: 0x000979DC File Offset: 0x00096DDC
		public new Pen Clone()
		{
			return (Pen)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Pen" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060025F7 RID: 9719 RVA: 0x000979F4 File Offset: 0x00096DF4
		public new Pen CloneCurrentValue()
		{
			return (Pen)base.CloneCurrentValue();
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x00097A0C File Offset: 0x00096E0C
		private static void BrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			Pen pen = (Pen)d;
			Brush resource = (Brush)e.OldValue;
			Brush resource2 = (Brush)e.NewValue;
			Dispatcher dispatcher = pen.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = pen;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						pen.ReleaseResource(resource, channel);
						pen.AddRefResource(resource2, channel);
					}
				}
			}
			pen.PropertyChanged(Pen.BrushProperty);
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x00097AD4 File Offset: 0x00096ED4
		private static void ThicknessPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Pen pen = (Pen)d;
			pen.PropertyChanged(Pen.ThicknessProperty);
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x00097AF4 File Offset: 0x00096EF4
		private static void StartLineCapPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Pen pen = (Pen)d;
			pen.PropertyChanged(Pen.StartLineCapProperty);
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x00097B14 File Offset: 0x00096F14
		private static void EndLineCapPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Pen pen = (Pen)d;
			pen.PropertyChanged(Pen.EndLineCapProperty);
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x00097B34 File Offset: 0x00096F34
		private static void DashCapPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Pen pen = (Pen)d;
			pen.PropertyChanged(Pen.DashCapProperty);
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x00097B54 File Offset: 0x00096F54
		private static void LineJoinPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Pen pen = (Pen)d;
			pen.PropertyChanged(Pen.LineJoinProperty);
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x00097B74 File Offset: 0x00096F74
		private static void MiterLimitPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Pen pen = (Pen)d;
			pen.PropertyChanged(Pen.MiterLimitProperty);
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x00097B94 File Offset: 0x00096F94
		private static void DashStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			Pen pen = (Pen)d;
			DashStyle resource = (DashStyle)e.OldValue;
			DashStyle resource2 = (DashStyle)e.NewValue;
			Dispatcher dispatcher = pen.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = pen;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						pen.ReleaseResource(resource, channel);
						pen.AddRefResource(resource2, channel);
					}
				}
			}
			pen.PropertyChanged(Pen.DashStyleProperty);
		}

		/// <summary>Obtém ou define o preenchimento da estrutura de tópicos produzida por este <see cref="T:System.Windows.Media.Pen" />.</summary>
		/// <returns>O preenchimento da estrutura de tópicos produzida por este <see cref="T:System.Windows.Media.Pen" />. O valor padrão é <see langword="null" />.</returns>
		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06002600 RID: 9728 RVA: 0x00097C5C File Offset: 0x0009705C
		// (set) Token: 0x06002601 RID: 9729 RVA: 0x00097C7C File Offset: 0x0009707C
		public Brush Brush
		{
			get
			{
				return (Brush)base.GetValue(Pen.BrushProperty);
			}
			set
			{
				base.SetValueInternal(Pen.BrushProperty, value);
			}
		}

		/// <summary>Obtém ou define a espessura do traço produzido por este <see cref="T:System.Windows.Media.Pen" />.</summary>
		/// <returns>A espessura do traço produzido por este <see cref="T:System.Windows.Media.Pen" />. O padrão é 1.</returns>
		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06002602 RID: 9730 RVA: 0x00097C98 File Offset: 0x00097098
		// (set) Token: 0x06002603 RID: 9731 RVA: 0x00097CB8 File Offset: 0x000970B8
		public double Thickness
		{
			get
			{
				return (double)base.GetValue(Pen.ThicknessProperty);
			}
			set
			{
				base.SetValueInternal(Pen.ThicknessProperty, value);
			}
		}

		/// <summary>Obtém ou define o tipo de forma a ser usado no início de um traço.</summary>
		/// <returns>O tipo de forma que inicia o traço. O valor padrão é <see cref="F:System.Windows.Media.PenLineCap.Flat" />.</returns>
		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06002604 RID: 9732 RVA: 0x00097CD8 File Offset: 0x000970D8
		// (set) Token: 0x06002605 RID: 9733 RVA: 0x00097CF8 File Offset: 0x000970F8
		public PenLineCap StartLineCap
		{
			get
			{
				return (PenLineCap)base.GetValue(Pen.StartLineCapProperty);
			}
			set
			{
				base.SetValueInternal(Pen.StartLineCapProperty, value);
			}
		}

		/// <summary>Obtém ou define o tipo de forma a ser usado no fim de um traço.</summary>
		/// <returns>O tipo de forma que termina o traço. O valor padrão é <see cref="F:System.Windows.Media.PenLineCap.Flat" />.</returns>
		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06002606 RID: 9734 RVA: 0x00097D18 File Offset: 0x00097118
		// (set) Token: 0x06002607 RID: 9735 RVA: 0x00097D38 File Offset: 0x00097138
		public PenLineCap EndLineCap
		{
			get
			{
				return (PenLineCap)base.GetValue(Pen.EndLineCapProperty);
			}
			set
			{
				base.SetValueInternal(Pen.EndLineCapProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que especifica como as extremidades de cada traço são desenhadas.</summary>
		/// <returns>Especifica como as extremidades de cada traço são desenhadas.  
		/// Essa configuração se aplica a ambas as extremidades de cada traço. O valor padrão é <see cref="F:System.Windows.Media.PenLineCap.Square" />.</returns>
		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002608 RID: 9736 RVA: 0x00097D58 File Offset: 0x00097158
		// (set) Token: 0x06002609 RID: 9737 RVA: 0x00097D78 File Offset: 0x00097178
		public PenLineCap DashCap
		{
			get
			{
				return (PenLineCap)base.GetValue(Pen.DashCapProperty);
			}
			set
			{
				base.SetValueInternal(Pen.DashCapProperty, value);
			}
		}

		/// <summary>Obtém ou define o tipo de união usada nos vértices de contorno de uma forma.</summary>
		/// <returns>O tipo de união usada nos vértices de contorno da forma. O valor padrão é <see cref="F:System.Windows.Media.PenLineJoin.Miter" />.</returns>
		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x0600260A RID: 9738 RVA: 0x00097D98 File Offset: 0x00097198
		// (set) Token: 0x0600260B RID: 9739 RVA: 0x00097DB8 File Offset: 0x000971B8
		public PenLineJoin LineJoin
		{
			get
			{
				return (PenLineJoin)base.GetValue(Pen.LineJoinProperty);
			}
			set
			{
				base.SetValueInternal(Pen.LineJoinProperty, value);
			}
		}

		/// <summary>Obtém ou define o limite para a proporção entre o comprimento do malhete e a metade do <see cref="P:System.Windows.Media.Pen.Thickness" /> desta caneta.</summary>
		/// <returns>O limite a proporção entre o comprimento do Malhete e a metade da caneta <see cref="P:System.Windows.Media.Pen.Thickness" />. Esse valor é sempre um número positivo maior que ou igual a 1.  O valor padrão é 10.0.</returns>
		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x0600260C RID: 9740 RVA: 0x00097DD8 File Offset: 0x000971D8
		// (set) Token: 0x0600260D RID: 9741 RVA: 0x00097DF8 File Offset: 0x000971F8
		public double MiterLimit
		{
			get
			{
				return (double)base.GetValue(Pen.MiterLimitProperty);
			}
			set
			{
				base.SetValueInternal(Pen.MiterLimitProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que descreve o padrão de traços gerado por este <see cref="T:System.Windows.Media.Pen" />.</summary>
		/// <returns>Um valor que descreve o padrão de traços gerado por este <see cref="T:System.Windows.Media.Pen" />. O padrão é <see cref="P:System.Windows.Media.DashStyles.Solid" />, que indica que não deve haver traços.</returns>
		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x0600260E RID: 9742 RVA: 0x00097E18 File Offset: 0x00097218
		// (set) Token: 0x0600260F RID: 9743 RVA: 0x00097E38 File Offset: 0x00097238
		public DashStyle DashStyle
		{
			get
			{
				return (DashStyle)base.GetValue(Pen.DashStyleProperty);
			}
			set
			{
				base.SetValueInternal(Pen.DashStyleProperty, value);
			}
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x00097E54 File Offset: 0x00097254
		protected override Freezable CreateInstanceCore()
		{
			return new Pen();
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x00097E68 File Offset: 0x00097268
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Brush brush = this.Brush;
				DashStyle dashStyle = this.DashStyle;
				DUCE.ResourceHandle hBrush = (brush != null) ? ((DUCE.IResource)brush).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.ResourceHandle hDashStyle = (dashStyle != null) ? ((DUCE.IResource)dashStyle).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(Pen.ThicknessProperty, channel);
				DUCE.MILCMD_PEN milcmd_PEN;
				milcmd_PEN.Type = MILCMD.MilCmdPen;
				milcmd_PEN.Handle = this._duceResource.GetHandle(channel);
				milcmd_PEN.hBrush = hBrush;
				if (animationResourceHandle.IsNull)
				{
					milcmd_PEN.Thickness = this.Thickness;
				}
				milcmd_PEN.hThicknessAnimations = animationResourceHandle;
				milcmd_PEN.StartLineCap = this.StartLineCap;
				milcmd_PEN.EndLineCap = this.EndLineCap;
				milcmd_PEN.DashCap = this.DashCap;
				milcmd_PEN.LineJoin = this.LineJoin;
				milcmd_PEN.MiterLimit = this.MiterLimit;
				milcmd_PEN.hDashStyle = hDashStyle;
				channel.SendCommand((byte*)(&milcmd_PEN), sizeof(DUCE.MILCMD_PEN));
			}
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x00097F70 File Offset: 0x00097370
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handle;
			using (CompositionEngineLock.Acquire())
			{
				if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_PEN))
				{
					Brush brush = this.Brush;
					if (brush != null)
					{
						((DUCE.IResource)brush).AddRefOnChannel(channel);
					}
					DashStyle dashStyle = this.DashStyle;
					if (dashStyle != null)
					{
						((DUCE.IResource)dashStyle).AddRefOnChannel(channel);
					}
					this.AddRefOnChannelAnimations(channel);
					this.UpdateResource(channel, true);
				}
				handle = this._duceResource.GetHandle(channel);
			}
			return handle;
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x00098004 File Offset: 0x00097404
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			using (CompositionEngineLock.Acquire())
			{
				if (this._duceResource.ReleaseOnChannel(channel))
				{
					Brush brush = this.Brush;
					if (brush != null)
					{
						((DUCE.IResource)brush).ReleaseOnChannel(channel);
					}
					DashStyle dashStyle = this.DashStyle;
					if (dashStyle != null)
					{
						((DUCE.IResource)dashStyle).ReleaseOnChannel(channel);
					}
					this.ReleaseOnChannelAnimations(channel);
				}
			}
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x0009807C File Offset: 0x0009747C
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handle;
			using (CompositionEngineLock.Acquire())
			{
				handle = this._duceResource.GetHandle(channel);
			}
			return handle;
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x000980CC File Offset: 0x000974CC
		int DUCE.IResource.GetChannelCount()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x000980E4 File Offset: 0x000974E4
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x00098100 File Offset: 0x00097500
		static Pen()
		{
			Type typeFromHandle = typeof(Pen);
			Pen.BrushProperty = Animatable.RegisterProperty("Brush", typeof(Brush), typeFromHandle, null, new PropertyChangedCallback(Pen.BrushPropertyChanged), null, false, null);
			Pen.ThicknessProperty = Animatable.RegisterProperty("Thickness", typeof(double), typeFromHandle, 1.0, new PropertyChangedCallback(Pen.ThicknessPropertyChanged), null, true, null);
			Pen.StartLineCapProperty = Animatable.RegisterProperty("StartLineCap", typeof(PenLineCap), typeFromHandle, PenLineCap.Flat, new PropertyChangedCallback(Pen.StartLineCapPropertyChanged), new ValidateValueCallback(ValidateEnums.IsPenLineCapValid), false, null);
			Pen.EndLineCapProperty = Animatable.RegisterProperty("EndLineCap", typeof(PenLineCap), typeFromHandle, PenLineCap.Flat, new PropertyChangedCallback(Pen.EndLineCapPropertyChanged), new ValidateValueCallback(ValidateEnums.IsPenLineCapValid), false, null);
			Pen.DashCapProperty = Animatable.RegisterProperty("DashCap", typeof(PenLineCap), typeFromHandle, PenLineCap.Square, new PropertyChangedCallback(Pen.DashCapPropertyChanged), new ValidateValueCallback(ValidateEnums.IsPenLineCapValid), false, null);
			Pen.LineJoinProperty = Animatable.RegisterProperty("LineJoin", typeof(PenLineJoin), typeFromHandle, PenLineJoin.Miter, new PropertyChangedCallback(Pen.LineJoinPropertyChanged), new ValidateValueCallback(ValidateEnums.IsPenLineJoinValid), false, null);
			Pen.MiterLimitProperty = Animatable.RegisterProperty("MiterLimit", typeof(double), typeFromHandle, 10.0, new PropertyChangedCallback(Pen.MiterLimitPropertyChanged), null, false, null);
			Pen.DashStyleProperty = Animatable.RegisterProperty("DashStyle", typeof(DashStyle), typeFromHandle, DashStyles.Solid, new PropertyChangedCallback(Pen.DashStylePropertyChanged), null, false, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Pen" />.</summary>
		// Token: 0x06002618 RID: 9752 RVA: 0x000982D0 File Offset: 0x000976D0
		public Pen()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Pen" /> com o <see cref="T:System.Windows.Media.Brush" /> e espessura especificadas.</summary>
		/// <param name="brush">O pincel para essa caneta.</param>
		/// <param name="thickness">A espessura da caneta.</param>
		// Token: 0x06002619 RID: 9753 RVA: 0x000982E4 File Offset: 0x000976E4
		public Pen(Brush brush, double thickness)
		{
			this.Brush = brush;
			this.Thickness = thickness;
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x00098308 File Offset: 0x00097708
		internal Pen(Brush brush, double thickness, PenLineCap startLineCap, PenLineCap endLineCap, PenLineCap dashCap, PenLineJoin lineJoin, double miterLimit, DashStyle dashStyle)
		{
			this.Thickness = thickness;
			this.StartLineCap = startLineCap;
			this.EndLineCap = endLineCap;
			this.DashCap = dashCap;
			this.LineJoin = lineJoin;
			this.MiterLimit = miterLimit;
			this.Brush = brush;
			this.DashStyle = dashStyle;
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x00098358 File Offset: 0x00097758
		private MIL_PEN_CAP GetInternalCapType(PenLineCap cap)
		{
			return (MIL_PEN_CAP)cap;
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x00098368 File Offset: 0x00097768
		private MIL_PEN_JOIN GetInternalJoinType(PenLineJoin join)
		{
			return (MIL_PEN_JOIN)join;
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x00098378 File Offset: 0x00097778
		[SecurityCritical]
		internal unsafe void GetBasicPenData(MIL_PEN_DATA* pData, out double[] dashArray)
		{
			dashArray = null;
			Invariant.Assert(pData != null);
			pData->Thickness = this.Thickness;
			pData->StartLineCap = this.GetInternalCapType(this.StartLineCap);
			pData->EndLineCap = this.GetInternalCapType(this.EndLineCap);
			pData->DashCap = this.GetInternalCapType(this.DashCap);
			pData->LineJoin = this.GetInternalJoinType(this.LineJoin);
			pData->MiterLimit = this.MiterLimit;
			if (this.DashStyle != null)
			{
				this.DashStyle.GetDashData(pData, out dashArray);
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x0600261E RID: 9758 RVA: 0x0009840C File Offset: 0x0009780C
		internal bool DoesNotContainGaps
		{
			get
			{
				DashStyle dashStyle = this.DashStyle;
				if (dashStyle != null)
				{
					DoubleCollection dashes = dashStyle.Dashes;
					if (dashes != null && dashes.Count > 0)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x0009843C File Offset: 0x0009783C
		internal static bool ContributesToBounds(Pen pen)
		{
			return pen != null && pen.Brush != null;
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Pen.Brush" />.</summary>
		// Token: 0x040011A9 RID: 4521
		public static readonly DependencyProperty BrushProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Pen.Thickness" />.</summary>
		// Token: 0x040011AA RID: 4522
		public static readonly DependencyProperty ThicknessProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Pen.StartLineCap" />.</summary>
		// Token: 0x040011AB RID: 4523
		public static readonly DependencyProperty StartLineCapProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Pen.EndLineCap" />.</summary>
		// Token: 0x040011AC RID: 4524
		public static readonly DependencyProperty EndLineCapProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Pen.DashCap" />.</summary>
		// Token: 0x040011AD RID: 4525
		public static readonly DependencyProperty DashCapProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Pen.LineJoin" />.</summary>
		// Token: 0x040011AE RID: 4526
		public static readonly DependencyProperty LineJoinProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Pen.MiterLimit" />.</summary>
		// Token: 0x040011AF RID: 4527
		public static readonly DependencyProperty MiterLimitProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Pen.DashStyle" />.</summary>
		// Token: 0x040011B0 RID: 4528
		public static readonly DependencyProperty DashStyleProperty;

		// Token: 0x040011B1 RID: 4529
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x040011B2 RID: 4530
		internal const double c_Thickness = 1.0;

		// Token: 0x040011B3 RID: 4531
		internal const PenLineCap c_StartLineCap = PenLineCap.Flat;

		// Token: 0x040011B4 RID: 4532
		internal const PenLineCap c_EndLineCap = PenLineCap.Flat;

		// Token: 0x040011B5 RID: 4533
		internal const PenLineCap c_DashCap = PenLineCap.Square;

		// Token: 0x040011B6 RID: 4534
		internal const PenLineJoin c_LineJoin = PenLineJoin.Miter;

		// Token: 0x040011B7 RID: 4535
		internal const double c_MiterLimit = 10.0;

		// Token: 0x040011B8 RID: 4536
		internal static DashStyle s_DashStyle = DashStyles.Solid;
	}
}
