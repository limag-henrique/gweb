using System;
using System.Collections.Generic;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal;

namespace System.Windows.Media
{
	/// <summary>Representa a sequência de traços e lacunas que será aplicada por uma <see cref="T:System.Windows.Media.Pen" />.</summary>
	// Token: 0x0200037A RID: 890
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public sealed class DashStyle : Animatable, DUCE.IResource
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.DashStyle" />.</summary>
		// Token: 0x06002017 RID: 8215 RVA: 0x00082F28 File Offset: 0x00082328
		public DashStyle()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.DashStyle" /> com o <see cref="P:System.Windows.Media.DashStyle.Dashes" /> e <see cref="P:System.Windows.Media.DashStyle.Offset" /> especificados.</summary>
		/// <param name="dashes">O <see cref="P:System.Windows.Media.DashStyle.Dashes" /> do <see cref="T:System.Windows.Media.DashStyle" />.</param>
		/// <param name="offset">O <see cref="P:System.Windows.Media.DashStyle.Offset" /> do <see cref="T:System.Windows.Media.DashStyle" />.</param>
		// Token: 0x06002018 RID: 8216 RVA: 0x00082F3C File Offset: 0x0008233C
		public DashStyle(IEnumerable<double> dashes, double offset)
		{
			this.Offset = offset;
			if (dashes != null)
			{
				this.Dashes = new DoubleCollection(dashes);
			}
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x00082F68 File Offset: 0x00082368
		[SecurityCritical]
		internal unsafe void GetDashData(MIL_PEN_DATA* pData, out double[] dashArray)
		{
			DoubleCollection dashes = this.Dashes;
			int num = 0;
			if (dashes != null)
			{
				num = dashes.Count;
			}
			pData->DashArraySize = (uint)(num * 8);
			pData->DashOffset = this.Offset;
			if (num > 0)
			{
				dashArray = dashes._collection.ToArray();
				return;
			}
			dashArray = null;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.DashStyle" />, fazendo cópias em profundidade dos valores do objeto.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true." /></returns>
		// Token: 0x0600201A RID: 8218 RVA: 0x00082FB4 File Offset: 0x000823B4
		public new DashStyle Clone()
		{
			return (DashStyle)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.DashStyle" />, fazendo cópias em profundidade dos valores do objeto atual.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true" />.</returns>
		// Token: 0x0600201B RID: 8219 RVA: 0x00082FCC File Offset: 0x000823CC
		public new DashStyle CloneCurrentValue()
		{
			return (DashStyle)base.CloneCurrentValue();
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x00082FE4 File Offset: 0x000823E4
		private static void OffsetPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DashStyle dashStyle = (DashStyle)d;
			dashStyle.PropertyChanged(DashStyle.OffsetProperty);
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x00083004 File Offset: 0x00082404
		private static void DashesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DashStyle dashStyle = (DashStyle)d;
			dashStyle.PropertyChanged(DashStyle.DashesProperty);
		}

		/// <summary>Obtém ou define a distância na sequência de traços na qual o traço será iniciado.</summary>
		/// <returns>O deslocamento para a sequência de traços.  O padrão é 0.</returns>
		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x0600201E RID: 8222 RVA: 0x00083024 File Offset: 0x00082424
		// (set) Token: 0x0600201F RID: 8223 RVA: 0x00083044 File Offset: 0x00082444
		public double Offset
		{
			get
			{
				return (double)base.GetValue(DashStyle.OffsetProperty);
			}
			set
			{
				base.SetValueInternal(DashStyle.OffsetProperty, value);
			}
		}

		/// <summary>Obtém ou define a coleção de traços e lacunas neste <see cref="T:System.Windows.Media.DashStyle" />.</summary>
		/// <returns>A coleção de traços e lacunas.  O padrão é um <see cref="T:System.Windows.Media.DoubleCollection" /> vazio.</returns>
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06002020 RID: 8224 RVA: 0x00083064 File Offset: 0x00082464
		// (set) Token: 0x06002021 RID: 8225 RVA: 0x00083084 File Offset: 0x00082484
		public DoubleCollection Dashes
		{
			get
			{
				return (DoubleCollection)base.GetValue(DashStyle.DashesProperty);
			}
			set
			{
				base.SetValueInternal(DashStyle.DashesProperty, value);
			}
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x000830A0 File Offset: 0x000824A0
		protected override Freezable CreateInstanceCore()
		{
			return new DashStyle();
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x000830B4 File Offset: 0x000824B4
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				DoubleCollection dashes = this.Dashes;
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(DashStyle.OffsetProperty, channel);
				int num = (dashes == null) ? 0 : dashes.Count;
				DUCE.MILCMD_DASHSTYLE milcmd_DASHSTYLE;
				milcmd_DASHSTYLE.Type = MILCMD.MilCmdDashStyle;
				milcmd_DASHSTYLE.Handle = this._duceResource.GetHandle(channel);
				if (animationResourceHandle.IsNull)
				{
					milcmd_DASHSTYLE.Offset = this.Offset;
				}
				milcmd_DASHSTYLE.hOffsetAnimations = animationResourceHandle;
				milcmd_DASHSTYLE.DashesSize = (uint)(8 * num);
				channel.BeginCommand((byte*)(&milcmd_DASHSTYLE), sizeof(DUCE.MILCMD_DASHSTYLE), (int)milcmd_DASHSTYLE.DashesSize);
				for (int i = 0; i < num; i++)
				{
					double num2 = dashes.Internal_GetItem(i);
					channel.AppendCommandData((byte*)(&num2), 8);
				}
				channel.EndCommand();
			}
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x00083184 File Offset: 0x00082584
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handle;
			using (CompositionEngineLock.Acquire())
			{
				if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_DASHSTYLE))
				{
					this.AddRefOnChannelAnimations(channel);
					this.UpdateResource(channel, true);
				}
				handle = this._duceResource.GetHandle(channel);
			}
			return handle;
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x000831F4 File Offset: 0x000825F4
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

		// Token: 0x06002026 RID: 8230 RVA: 0x00083248 File Offset: 0x00082648
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handle;
			using (CompositionEngineLock.Acquire())
			{
				handle = this._duceResource.GetHandle(channel);
			}
			return handle;
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x00083298 File Offset: 0x00082698
		int DUCE.IResource.GetChannelCount()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x000832B0 File Offset: 0x000826B0
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06002029 RID: 8233 RVA: 0x000832CC File Offset: 0x000826CC
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x000832DC File Offset: 0x000826DC
		static DashStyle()
		{
			Type typeFromHandle = typeof(DashStyle);
			DashStyle.OffsetProperty = Animatable.RegisterProperty("Offset", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(DashStyle.OffsetPropertyChanged), null, true, null);
			DashStyle.DashesProperty = Animatable.RegisterProperty("Dashes", typeof(DoubleCollection), typeFromHandle, new FreezableDefaultValueFactory(DoubleCollection.Empty), new PropertyChangedCallback(DashStyle.DashesPropertyChanged), null, false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.DashStyle.Offset" />.</summary>
		// Token: 0x04001080 RID: 4224
		public static readonly DependencyProperty OffsetProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.DashStyle.Dashes" />.</summary>
		// Token: 0x04001081 RID: 4225
		public static readonly DependencyProperty DashesProperty;

		// Token: 0x04001082 RID: 4226
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x04001083 RID: 4227
		internal const double c_Offset = 0.0;

		// Token: 0x04001084 RID: 4228
		internal static DoubleCollection s_Dashes = DoubleCollection.Empty;
	}
}
