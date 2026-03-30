using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Threading;

namespace System.Windows.Media
{
	/// <summary>Representa um objeto <see cref="T:System.Windows.Media.Drawing" /> que renderiza um <see cref="T:System.Windows.Media.GlyphRun" />.</summary>
	// Token: 0x020003B1 RID: 945
	public sealed class GlyphRunDrawing : Drawing
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.GlyphRunDrawing" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060023F4 RID: 9204 RVA: 0x00090E14 File Offset: 0x00090214
		public new GlyphRunDrawing Clone()
		{
			return (GlyphRunDrawing)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.GlyphRunDrawing" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060023F5 RID: 9205 RVA: 0x00090E2C File Offset: 0x0009022C
		public new GlyphRunDrawing CloneCurrentValue()
		{
			return (GlyphRunDrawing)base.CloneCurrentValue();
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x00090E44 File Offset: 0x00090244
		private static void GlyphRunPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			GlyphRunDrawing glyphRunDrawing = (GlyphRunDrawing)d;
			GlyphRun resource = (GlyphRun)e.OldValue;
			GlyphRun resource2 = (GlyphRun)e.NewValue;
			Dispatcher dispatcher = glyphRunDrawing.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = glyphRunDrawing;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						glyphRunDrawing.ReleaseResource(resource, channel);
						glyphRunDrawing.AddRefResource(resource2, channel);
					}
				}
			}
			glyphRunDrawing.PropertyChanged(GlyphRunDrawing.GlyphRunProperty);
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x00090EF0 File Offset: 0x000902F0
		private static void ForegroundBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			GlyphRunDrawing glyphRunDrawing = (GlyphRunDrawing)d;
			Brush resource = (Brush)e.OldValue;
			Brush resource2 = (Brush)e.NewValue;
			Dispatcher dispatcher = glyphRunDrawing.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = glyphRunDrawing;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						glyphRunDrawing.ReleaseResource(resource, channel);
						glyphRunDrawing.AddRefResource(resource2, channel);
					}
				}
			}
			glyphRunDrawing.PropertyChanged(GlyphRunDrawing.ForegroundBrushProperty);
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.GlyphRun" /> que descreve o texto a ser desenhado.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.GlyphRun" /> que descreve o texto a ser desenhado. O valor padrão é <see langword="null" />.</returns>
		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x00090FB8 File Offset: 0x000903B8
		// (set) Token: 0x060023F9 RID: 9209 RVA: 0x00090FD8 File Offset: 0x000903D8
		public GlyphRun GlyphRun
		{
			get
			{
				return (GlyphRun)base.GetValue(GlyphRunDrawing.GlyphRunProperty);
			}
			set
			{
				base.SetValueInternal(GlyphRunDrawing.GlyphRunProperty, value);
			}
		}

		/// <summary>Obtém ou define o pincel de primeiro plano do <see cref="T:System.Windows.Media.GlyphRunDrawing" />.</summary>
		/// <returns>O pincel que pinta o <see cref="T:System.Windows.Media.GlyphRun" />. O valor padrão é <see langword="null" />.</returns>
		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x00090FF4 File Offset: 0x000903F4
		// (set) Token: 0x060023FB RID: 9211 RVA: 0x00091014 File Offset: 0x00090414
		public Brush ForegroundBrush
		{
			get
			{
				return (Brush)base.GetValue(GlyphRunDrawing.ForegroundBrushProperty);
			}
			set
			{
				base.SetValueInternal(GlyphRunDrawing.ForegroundBrushProperty, value);
			}
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x00091030 File Offset: 0x00090430
		protected override Freezable CreateInstanceCore()
		{
			return new GlyphRunDrawing();
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x00091044 File Offset: 0x00090444
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				GlyphRun glyphRun = this.GlyphRun;
				Brush foregroundBrush = this.ForegroundBrush;
				DUCE.ResourceHandle hGlyphRun = (glyphRun != null) ? ((DUCE.IResource)glyphRun).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.ResourceHandle hForegroundBrush = (foregroundBrush != null) ? ((DUCE.IResource)foregroundBrush).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.MILCMD_GLYPHRUNDRAWING milcmd_GLYPHRUNDRAWING;
				milcmd_GLYPHRUNDRAWING.Type = MILCMD.MilCmdGlyphRunDrawing;
				milcmd_GLYPHRUNDRAWING.Handle = this._duceResource.GetHandle(channel);
				milcmd_GLYPHRUNDRAWING.hGlyphRun = hGlyphRun;
				milcmd_GLYPHRUNDRAWING.hForegroundBrush = hForegroundBrush;
				channel.SendCommand((byte*)(&milcmd_GLYPHRUNDRAWING), sizeof(DUCE.MILCMD_GLYPHRUNDRAWING));
			}
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x000910E0 File Offset: 0x000904E0
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_GLYPHRUNDRAWING))
			{
				GlyphRun glyphRun = this.GlyphRun;
				if (glyphRun != null)
				{
					((DUCE.IResource)glyphRun).AddRefOnChannel(channel);
				}
				Brush foregroundBrush = this.ForegroundBrush;
				if (foregroundBrush != null)
				{
					((DUCE.IResource)foregroundBrush).AddRefOnChannel(channel);
				}
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x00091140 File Offset: 0x00090540
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				GlyphRun glyphRun = this.GlyphRun;
				if (glyphRun != null)
				{
					((DUCE.IResource)glyphRun).ReleaseOnChannel(channel);
				}
				Brush foregroundBrush = this.ForegroundBrush;
				if (foregroundBrush != null)
				{
					((DUCE.IResource)foregroundBrush).ReleaseOnChannel(channel);
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x00091184 File Offset: 0x00090584
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x000911A0 File Offset: 0x000905A0
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x000911B8 File Offset: 0x000905B8
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x000911D4 File Offset: 0x000905D4
		static GlyphRunDrawing()
		{
			Type typeFromHandle = typeof(GlyphRunDrawing);
			GlyphRunDrawing.GlyphRunProperty = Animatable.RegisterProperty("GlyphRun", typeof(GlyphRun), typeFromHandle, null, new PropertyChangedCallback(GlyphRunDrawing.GlyphRunPropertyChanged), null, false, null);
			GlyphRunDrawing.ForegroundBrushProperty = Animatable.RegisterProperty("ForegroundBrush", typeof(Brush), typeFromHandle, null, new PropertyChangedCallback(GlyphRunDrawing.ForegroundBrushPropertyChanged), null, false, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GlyphRunDrawing" />.</summary>
		// Token: 0x06002404 RID: 9220 RVA: 0x00091240 File Offset: 0x00090640
		public GlyphRunDrawing()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GlyphRunDrawing" /> especificando o pincel de primeiro plano e <see cref="T:System.Windows.Media.GlyphRun" />.</summary>
		/// <param name="foregroundBrush">O pincel de primeiro plano a ser usado para o <see cref="T:System.Windows.Media.GlyphRunDrawing" />.</param>
		/// <param name="glyphRun">O <see cref="T:System.Windows.Media.GlyphRun" /> usar neste <see cref="T:System.Windows.Media.GlyphRunDrawing" />.</param>
		// Token: 0x06002405 RID: 9221 RVA: 0x00091254 File Offset: 0x00090654
		public GlyphRunDrawing(Brush foregroundBrush, GlyphRun glyphRun)
		{
			this.GlyphRun = glyphRun;
			this.ForegroundBrush = foregroundBrush;
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x00091278 File Offset: 0x00090678
		internal override void WalkCurrentValue(DrawingContextWalker ctx)
		{
			ctx.DrawGlyphRun(this.ForegroundBrush, this.GlyphRun);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.GlyphRunDrawing.GlyphRun" />.</summary>
		// Token: 0x04001159 RID: 4441
		public static readonly DependencyProperty GlyphRunProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.GlyphRunDrawing.ForegroundBrush" />.</summary>
		// Token: 0x0400115A RID: 4442
		public static readonly DependencyProperty ForegroundBrushProperty;

		// Token: 0x0400115B RID: 4443
		internal DUCE.MultiChannelResource _duceResource;
	}
}
