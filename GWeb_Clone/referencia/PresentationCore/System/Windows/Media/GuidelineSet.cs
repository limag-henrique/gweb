using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal;
using MS.Internal.KnownBoxes;

namespace System.Windows.Media
{
	/// <summary>Representa uma coleção de linhas da guia que pode ajudar no ajuste das figuras renderizadas em uma grade de pixels do dispositivo.</summary>
	// Token: 0x02000401 RID: 1025
	public sealed class GuidelineSet : Animatable, DUCE.IResource
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.GuidelineSet" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002907 RID: 10503 RVA: 0x000A457C File Offset: 0x000A397C
		public new GuidelineSet Clone()
		{
			return (GuidelineSet)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.GuidelineSet" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002908 RID: 10504 RVA: 0x000A4594 File Offset: 0x000A3994
		public new GuidelineSet CloneCurrentValue()
		{
			return (GuidelineSet)base.CloneCurrentValue();
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x000A45AC File Offset: 0x000A39AC
		private static void GuidelinesXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			GuidelineSet guidelineSet = (GuidelineSet)d;
			guidelineSet.PropertyChanged(GuidelineSet.GuidelinesXProperty);
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x000A45CC File Offset: 0x000A39CC
		private static void GuidelinesYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			GuidelineSet guidelineSet = (GuidelineSet)d;
			guidelineSet.PropertyChanged(GuidelineSet.GuidelinesYProperty);
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x000A45EC File Offset: 0x000A39EC
		private static void IsDynamicPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			GuidelineSet guidelineSet = (GuidelineSet)d;
			guidelineSet.PropertyChanged(GuidelineSet.IsDynamicProperty);
		}

		/// <summary>Obtém ou define uma série de valores de coordenada que representam as linhas de guia no eixo X.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.DoubleCollection" /> de valores que representam as linhas de guia no eixo x.</returns>
		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x0600290C RID: 10508 RVA: 0x000A460C File Offset: 0x000A3A0C
		// (set) Token: 0x0600290D RID: 10509 RVA: 0x000A462C File Offset: 0x000A3A2C
		public DoubleCollection GuidelinesX
		{
			get
			{
				return (DoubleCollection)base.GetValue(GuidelineSet.GuidelinesXProperty);
			}
			set
			{
				base.SetValueInternal(GuidelineSet.GuidelinesXProperty, value);
			}
		}

		/// <summary>Obtém ou define uma série de valores de coordenada que representam as linhas de guia no eixo Y.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.DoubleCollection" /> de valores que representam as linhas de guia no eixo y.</returns>
		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x0600290E RID: 10510 RVA: 0x000A4648 File Offset: 0x000A3A48
		// (set) Token: 0x0600290F RID: 10511 RVA: 0x000A4668 File Offset: 0x000A3A68
		public DoubleCollection GuidelinesY
		{
			get
			{
				return (DoubleCollection)base.GetValue(GuidelineSet.GuidelinesYProperty);
			}
			set
			{
				base.SetValueInternal(GuidelineSet.GuidelinesYProperty, value);
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06002910 RID: 10512 RVA: 0x000A4684 File Offset: 0x000A3A84
		// (set) Token: 0x06002911 RID: 10513 RVA: 0x000A46A4 File Offset: 0x000A3AA4
		internal bool IsDynamic
		{
			get
			{
				return (bool)base.GetValue(GuidelineSet.IsDynamicProperty);
			}
			set
			{
				base.SetValueInternal(GuidelineSet.IsDynamicProperty, BooleanBoxes.Box(value));
			}
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x000A46C4 File Offset: 0x000A3AC4
		protected override Freezable CreateInstanceCore()
		{
			return new GuidelineSet();
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x000A46D8 File Offset: 0x000A3AD8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				DoubleCollection guidelinesX = this.GuidelinesX;
				DoubleCollection guidelinesY = this.GuidelinesY;
				int num = (guidelinesX == null) ? 0 : guidelinesX.Count;
				int num2 = (guidelinesY == null) ? 0 : guidelinesY.Count;
				DUCE.MILCMD_GUIDELINESET milcmd_GUIDELINESET;
				milcmd_GUIDELINESET.Type = MILCMD.MilCmdGuidelineSet;
				milcmd_GUIDELINESET.Handle = this._duceResource.GetHandle(channel);
				milcmd_GUIDELINESET.GuidelinesXSize = (uint)(8 * num);
				milcmd_GUIDELINESET.GuidelinesYSize = (uint)(8 * num2);
				milcmd_GUIDELINESET.IsDynamic = CompositionResourceManager.BooleanToUInt32(this.IsDynamic);
				channel.BeginCommand((byte*)(&milcmd_GUIDELINESET), sizeof(DUCE.MILCMD_GUIDELINESET), (int)(milcmd_GUIDELINESET.GuidelinesXSize + milcmd_GUIDELINESET.GuidelinesYSize));
				for (int i = 0; i < num; i++)
				{
					double num3 = guidelinesX.Internal_GetItem(i);
					channel.AppendCommandData((byte*)(&num3), 8);
				}
				for (int j = 0; j < num2; j++)
				{
					double num4 = guidelinesY.Internal_GetItem(j);
					channel.AppendCommandData((byte*)(&num4), 8);
				}
				channel.EndCommand();
			}
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x000A47D8 File Offset: 0x000A3BD8
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handle;
			using (CompositionEngineLock.Acquire())
			{
				if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_GUIDELINESET))
				{
					this.AddRefOnChannelAnimations(channel);
					this.UpdateResource(channel, true);
				}
				handle = this._duceResource.GetHandle(channel);
			}
			return handle;
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x000A4848 File Offset: 0x000A3C48
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			using (CompositionEngineLock.Acquire())
			{
				if (this._duceResource.ReleaseOnChannel(channel))
				{
					this.ReleaseOnChannelAnimations(channel);
				}
			}
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x000A489C File Offset: 0x000A3C9C
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handle;
			using (CompositionEngineLock.Acquire())
			{
				handle = this._duceResource.GetHandle(channel);
			}
			return handle;
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x000A48EC File Offset: 0x000A3CEC
		int DUCE.IResource.GetChannelCount()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x000A4904 File Offset: 0x000A3D04
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x000A4920 File Offset: 0x000A3D20
		static GuidelineSet()
		{
			Type typeFromHandle = typeof(GuidelineSet);
			GuidelineSet.GuidelinesXProperty = Animatable.RegisterProperty("GuidelinesX", typeof(DoubleCollection), typeFromHandle, new FreezableDefaultValueFactory(DoubleCollection.Empty), new PropertyChangedCallback(GuidelineSet.GuidelinesXPropertyChanged), null, false, null);
			GuidelineSet.GuidelinesYProperty = Animatable.RegisterProperty("GuidelinesY", typeof(DoubleCollection), typeFromHandle, new FreezableDefaultValueFactory(DoubleCollection.Empty), new PropertyChangedCallback(GuidelineSet.GuidelinesYPropertyChanged), null, false, null);
			GuidelineSet.IsDynamicProperty = Animatable.RegisterProperty("IsDynamic", typeof(bool), typeFromHandle, false, new PropertyChangedCallback(GuidelineSet.IsDynamicPropertyChanged), null, false, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GuidelineSet" />.</summary>
		// Token: 0x0600291A RID: 10522 RVA: 0x000A49E4 File Offset: 0x000A3DE4
		public GuidelineSet()
		{
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x000A49F8 File Offset: 0x000A3DF8
		internal GuidelineSet(double[] guidelinesX, double[] guidelinesY, bool isDynamic)
		{
			if (guidelinesX != null)
			{
				this.GuidelinesX = new DoubleCollection(guidelinesX);
			}
			if (guidelinesY != null)
			{
				this.GuidelinesY = new DoubleCollection(guidelinesY);
			}
			this.IsDynamic = isDynamic;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GuidelineSet" /> com os valores <see cref="P:System.Windows.Media.GuidelineSet.GuidelinesX" /> e <see cref="P:System.Windows.Media.GuidelineSet.GuidelinesY" /> especificados.</summary>
		/// <param name="guidelinesX">O valor da propriedade <see cref="P:System.Windows.Media.GuidelineSet.GuidelinesX" />.</param>
		/// <param name="guidelinesY">O valor da propriedade <see cref="P:System.Windows.Media.GuidelineSet.GuidelinesY" />.</param>
		// Token: 0x0600291C RID: 10524 RVA: 0x000A4A30 File Offset: 0x000A3E30
		public GuidelineSet(double[] guidelinesX, double[] guidelinesY)
		{
			if (guidelinesX != null)
			{
				this.GuidelinesX = new DoubleCollection(guidelinesX);
			}
			if (guidelinesY != null)
			{
				this.GuidelinesY = new DoubleCollection(guidelinesY);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.GuidelineSet.GuidelinesX" />.</summary>
		// Token: 0x040012B1 RID: 4785
		public static readonly DependencyProperty GuidelinesXProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.GuidelineSet.GuidelinesY" />.</summary>
		// Token: 0x040012B2 RID: 4786
		public static readonly DependencyProperty GuidelinesYProperty;

		// Token: 0x040012B3 RID: 4787
		internal static readonly DependencyProperty IsDynamicProperty;

		// Token: 0x040012B4 RID: 4788
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x040012B5 RID: 4789
		internal static DoubleCollection s_GuidelinesX = DoubleCollection.Empty;

		// Token: 0x040012B6 RID: 4790
		internal static DoubleCollection s_GuidelinesY = DoubleCollection.Empty;

		// Token: 0x040012B7 RID: 4791
		internal const bool c_IsDynamic = false;
	}
}
