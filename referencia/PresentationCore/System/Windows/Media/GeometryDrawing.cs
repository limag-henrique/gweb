using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Threading;

namespace System.Windows.Media
{
	/// <summary>Desenha um <see cref="T:System.Windows.Media.Geometry" /> usando o <see cref="P:System.Windows.Media.GeometryDrawing.Brush" /> e o <see cref="P:System.Windows.Media.GeometryDrawing.Pen" /> especificados.</summary>
	// Token: 0x020003AF RID: 943
	public sealed class GeometryDrawing : Drawing
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.GeometryDrawing" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060023C4 RID: 9156 RVA: 0x00090050 File Offset: 0x0008F450
		public new GeometryDrawing Clone()
		{
			return (GeometryDrawing)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.GeometryDrawing" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060023C5 RID: 9157 RVA: 0x00090068 File Offset: 0x0008F468
		public new GeometryDrawing CloneCurrentValue()
		{
			return (GeometryDrawing)base.CloneCurrentValue();
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x00090080 File Offset: 0x0008F480
		private static void BrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			GeometryDrawing geometryDrawing = (GeometryDrawing)d;
			Brush resource = (Brush)e.OldValue;
			Brush resource2 = (Brush)e.NewValue;
			Dispatcher dispatcher = geometryDrawing.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = geometryDrawing;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						geometryDrawing.ReleaseResource(resource, channel);
						geometryDrawing.AddRefResource(resource2, channel);
					}
				}
			}
			geometryDrawing.PropertyChanged(GeometryDrawing.BrushProperty);
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x00090148 File Offset: 0x0008F548
		private static void PenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			GeometryDrawing geometryDrawing = (GeometryDrawing)d;
			Pen resource = (Pen)e.OldValue;
			Pen resource2 = (Pen)e.NewValue;
			Dispatcher dispatcher = geometryDrawing.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = geometryDrawing;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						geometryDrawing.ReleaseResource(resource, channel);
						geometryDrawing.AddRefResource(resource2, channel);
					}
				}
			}
			geometryDrawing.PropertyChanged(GeometryDrawing.PenProperty);
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x00090210 File Offset: 0x0008F610
		private static void GeometryPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			GeometryDrawing geometryDrawing = (GeometryDrawing)d;
			Geometry resource = (Geometry)e.OldValue;
			Geometry resource2 = (Geometry)e.NewValue;
			Dispatcher dispatcher = geometryDrawing.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = geometryDrawing;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						geometryDrawing.ReleaseResource(resource, channel);
						geometryDrawing.AddRefResource(resource2, channel);
					}
				}
			}
			geometryDrawing.PropertyChanged(GeometryDrawing.GeometryProperty);
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Brush" /> usado para preencher o interior da forma descrita por este <see cref="T:System.Windows.Media.GeometryDrawing" />.</summary>
		/// <returns>O pincel usado para preencher este <see cref="T:System.Windows.Media.GeometryDrawing" />. O valor padrão é <see langword="null" />.</returns>
		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060023C9 RID: 9161 RVA: 0x000902D8 File Offset: 0x0008F6D8
		// (set) Token: 0x060023CA RID: 9162 RVA: 0x000902F8 File Offset: 0x0008F6F8
		public Brush Brush
		{
			get
			{
				return (Brush)base.GetValue(GeometryDrawing.BrushProperty);
			}
			set
			{
				base.SetValueInternal(GeometryDrawing.BrushProperty, value);
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Pen" /> usado para traçar este <see cref="T:System.Windows.Media.GeometryDrawing" />.</summary>
		/// <returns>A caneta usada para traçar este <see cref="T:System.Windows.Media.GeometryDrawing" />. O valor padrão é <see langword="null" />.</returns>
		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060023CB RID: 9163 RVA: 0x00090314 File Offset: 0x0008F714
		// (set) Token: 0x060023CC RID: 9164 RVA: 0x00090334 File Offset: 0x0008F734
		public Pen Pen
		{
			get
			{
				return (Pen)base.GetValue(GeometryDrawing.PenProperty);
			}
			set
			{
				base.SetValueInternal(GeometryDrawing.PenProperty, value);
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Geometry" /> que descreve a forma deste <see cref="T:System.Windows.Media.GeometryDrawing" />.</summary>
		/// <returns>A forma descrita por este <see cref="T:System.Windows.Media.GeometryDrawing" />. O valor padrão é <see langword="null" />.</returns>
		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060023CD RID: 9165 RVA: 0x00090350 File Offset: 0x0008F750
		// (set) Token: 0x060023CE RID: 9166 RVA: 0x00090370 File Offset: 0x0008F770
		public Geometry Geometry
		{
			get
			{
				return (Geometry)base.GetValue(GeometryDrawing.GeometryProperty);
			}
			set
			{
				base.SetValueInternal(GeometryDrawing.GeometryProperty, value);
			}
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x0009038C File Offset: 0x0008F78C
		protected override Freezable CreateInstanceCore()
		{
			return new GeometryDrawing();
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x000903A0 File Offset: 0x0008F7A0
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Brush brush = this.Brush;
				Pen pen = this.Pen;
				Geometry geometry = this.Geometry;
				DUCE.ResourceHandle hBrush = (brush != null) ? ((DUCE.IResource)brush).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.ResourceHandle hPen = (pen != null) ? ((DUCE.IResource)pen).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.ResourceHandle hGeometry = (geometry != null) ? ((DUCE.IResource)geometry).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.MILCMD_GEOMETRYDRAWING milcmd_GEOMETRYDRAWING;
				milcmd_GEOMETRYDRAWING.Type = MILCMD.MilCmdGeometryDrawing;
				milcmd_GEOMETRYDRAWING.Handle = this._duceResource.GetHandle(channel);
				milcmd_GEOMETRYDRAWING.hBrush = hBrush;
				milcmd_GEOMETRYDRAWING.hPen = hPen;
				milcmd_GEOMETRYDRAWING.hGeometry = hGeometry;
				channel.SendCommand((byte*)(&milcmd_GEOMETRYDRAWING), sizeof(DUCE.MILCMD_GEOMETRYDRAWING));
			}
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x00090460 File Offset: 0x0008F860
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_GEOMETRYDRAWING))
			{
				Brush brush = this.Brush;
				if (brush != null)
				{
					((DUCE.IResource)brush).AddRefOnChannel(channel);
				}
				Pen pen = this.Pen;
				if (pen != null)
				{
					((DUCE.IResource)pen).AddRefOnChannel(channel);
				}
				Geometry geometry = this.Geometry;
				if (geometry != null)
				{
					((DUCE.IResource)geometry).AddRefOnChannel(channel);
				}
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x000904D0 File Offset: 0x0008F8D0
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				Brush brush = this.Brush;
				if (brush != null)
				{
					((DUCE.IResource)brush).ReleaseOnChannel(channel);
				}
				Pen pen = this.Pen;
				if (pen != null)
				{
					((DUCE.IResource)pen).ReleaseOnChannel(channel);
				}
				Geometry geometry = this.Geometry;
				if (geometry != null)
				{
					((DUCE.IResource)geometry).ReleaseOnChannel(channel);
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x00090528 File Offset: 0x0008F928
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x00090544 File Offset: 0x0008F944
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x0009055C File Offset: 0x0008F95C
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x00090578 File Offset: 0x0008F978
		static GeometryDrawing()
		{
			Type typeFromHandle = typeof(GeometryDrawing);
			GeometryDrawing.BrushProperty = Animatable.RegisterProperty("Brush", typeof(Brush), typeFromHandle, null, new PropertyChangedCallback(GeometryDrawing.BrushPropertyChanged), null, false, null);
			GeometryDrawing.PenProperty = Animatable.RegisterProperty("Pen", typeof(Pen), typeFromHandle, null, new PropertyChangedCallback(GeometryDrawing.PenPropertyChanged), null, false, null);
			GeometryDrawing.GeometryProperty = Animatable.RegisterProperty("Geometry", typeof(Geometry), typeFromHandle, null, new PropertyChangedCallback(GeometryDrawing.GeometryPropertyChanged), null, false, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GeometryDrawing" />.</summary>
		// Token: 0x060023D7 RID: 9175 RVA: 0x00090610 File Offset: 0x0008FA10
		public GeometryDrawing()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GeometryDrawing" /> com o <see cref="T:System.Windows.Media.Brush" />, <see cref="T:System.Windows.Media.Pen" /> e <see cref="T:System.Windows.Media.Geometry" /> especificados.</summary>
		/// <param name="brush">O pincel usado para preencher este <see cref="T:System.Windows.Media.GeometryDrawing" />.</param>
		/// <param name="pen">A caneta usada para traçar este <see cref="T:System.Windows.Media.GeometryDrawing" />.</param>
		/// <param name="geometry">A geometria</param>
		// Token: 0x060023D8 RID: 9176 RVA: 0x00090624 File Offset: 0x0008FA24
		public GeometryDrawing(Brush brush, Pen pen, Geometry geometry)
		{
			this.Brush = brush;
			this.Pen = pen;
			this.Geometry = geometry;
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x0009064C File Offset: 0x0008FA4C
		internal override void WalkCurrentValue(DrawingContextWalker ctx)
		{
			ctx.DrawGeometry(this.Brush, this.Pen, this.Geometry);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.GeometryDrawing.Brush" />.</summary>
		// Token: 0x04001150 RID: 4432
		public static readonly DependencyProperty BrushProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.GeometryDrawing.Pen" />.</summary>
		// Token: 0x04001151 RID: 4433
		public static readonly DependencyProperty PenProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.GeometryDrawing.Geometry" />.</summary>
		// Token: 0x04001152 RID: 4434
		public static readonly DependencyProperty GeometryProperty;

		// Token: 0x04001153 RID: 4435
		internal DUCE.MultiChannelResource _duceResource;
	}
}
