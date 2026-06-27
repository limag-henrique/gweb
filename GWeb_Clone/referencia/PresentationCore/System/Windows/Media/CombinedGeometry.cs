using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Threading;

namespace System.Windows.Media
{
	/// <summary>Representa uma forma geométrica 2D definida pela combinação de dois objetos <see cref="T:System.Windows.Media.Geometry" />.</summary>
	// Token: 0x02000376 RID: 886
	public sealed class CombinedGeometry : Geometry
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.CombinedGeometry" />.</summary>
		// Token: 0x06001F9E RID: 8094 RVA: 0x000818B0 File Offset: 0x00080CB0
		public CombinedGeometry()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.CombinedGeometry" /> com os objetos <see cref="T:System.Windows.Media.Geometry" /> especificados.</summary>
		/// <param name="geometry1">O primeiro <see cref="T:System.Windows.Media.Geometry" /> a combinar.</param>
		/// <param name="geometry2">O segundo <see cref="T:System.Windows.Media.Geometry" /> a combinar.</param>
		// Token: 0x06001F9F RID: 8095 RVA: 0x000818C4 File Offset: 0x00080CC4
		public CombinedGeometry(Geometry geometry1, Geometry geometry2)
		{
			this.Geometry1 = geometry1;
			this.Geometry2 = geometry2;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.CombinedGeometry" /> com os objetos <see cref="T:System.Windows.Media.Geometry" /> e <see cref="P:System.Windows.Media.CombinedGeometry.GeometryCombineMode" /> especificados.</summary>
		/// <param name="geometryCombineMode">O método pelo qual <paramref name="geometry1" /> e <paramref name="geometry2" /> são combinadas.</param>
		/// <param name="geometry1">O primeiro <see cref="T:System.Windows.Media.Geometry" /> a combinar.</param>
		/// <param name="geometry2">O segundo <see cref="T:System.Windows.Media.Geometry" /> a combinar.</param>
		// Token: 0x06001FA0 RID: 8096 RVA: 0x000818E8 File Offset: 0x00080CE8
		public CombinedGeometry(GeometryCombineMode geometryCombineMode, Geometry geometry1, Geometry geometry2)
		{
			this.GeometryCombineMode = geometryCombineMode;
			this.Geometry1 = geometry1;
			this.Geometry2 = geometry2;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.CombinedGeometry" /> com os objetos <see cref="T:System.Windows.Media.Geometry" />, <see cref="P:System.Windows.Media.CombinedGeometry.GeometryCombineMode" /> e <see cref="P:System.Windows.Media.Geometry.Transform" /> especificados.</summary>
		/// <param name="geometryCombineMode">O método pelo qual <paramref name="geometry1" /> e <paramref name="geometry2" /> são combinadas.</param>
		/// <param name="geometry1">O primeiro <see cref="T:System.Windows.Media.Geometry" /> a combinar.</param>
		/// <param name="geometry2">O segundo <see cref="T:System.Windows.Media.Geometry" /> a combinar.</param>
		/// <param name="transform">O <see cref="P:System.Windows.Media.Geometry.Transform" /> aplicado ao <see cref="T:System.Windows.Media.CombinedGeometry" />.</param>
		// Token: 0x06001FA1 RID: 8097 RVA: 0x00081910 File Offset: 0x00080D10
		public CombinedGeometry(GeometryCombineMode geometryCombineMode, Geometry geometry1, Geometry geometry2, Transform transform)
		{
			this.GeometryCombineMode = geometryCombineMode;
			this.Geometry1 = geometry1;
			this.Geometry2 = geometry2;
			base.Transform = transform;
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Rect" /> que especifica a caixa delimitadora deste objeto <see cref="T:System.Windows.Media.CombinedGeometry" />.   Observação: Este método não leva em consideração nenhuma caneta.</summary>
		/// <returns>A caixa delimitadora deste <see cref="T:System.Windows.Media.CombinedGeometry" />.</returns>
		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001FA2 RID: 8098 RVA: 0x00081940 File Offset: 0x00080D40
		public override Rect Bounds
		{
			get
			{
				base.ReadPreamble();
				return this.GetAsPathGeometry().Bounds;
			}
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x00081960 File Offset: 0x00080D60
		internal override Rect GetBoundsInternal(Pen pen, Matrix matrix, double tolerance, ToleranceType type)
		{
			if (this.IsObviouslyEmpty())
			{
				return Rect.Empty;
			}
			return this.GetAsPathGeometry().GetBoundsInternal(pen, matrix, tolerance, type);
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x0008198C File Offset: 0x00080D8C
		internal override bool ContainsInternal(Pen pen, Point hitPoint, double tolerance, ToleranceType type)
		{
			if (pen != null)
			{
				return base.ContainsInternal(pen, hitPoint, tolerance, type);
			}
			base.ReadPreamble();
			bool flag = false;
			bool flag2 = false;
			Transform transform = base.Transform;
			if (transform != null && !transform.IsIdentity)
			{
				Matrix value = transform.Value;
				if (!value.HasInverse)
				{
					return false;
				}
				value.Invert();
				hitPoint *= value;
			}
			Geometry geometry = this.Geometry1;
			Geometry geometry2 = this.Geometry2;
			if (geometry != null)
			{
				flag = geometry.ContainsInternal(pen, hitPoint, tolerance, type);
			}
			if (geometry2 != null)
			{
				flag2 = geometry2.ContainsInternal(pen, hitPoint, tolerance, type);
			}
			switch (this.GeometryCombineMode)
			{
			case GeometryCombineMode.Union:
				return flag || flag2;
			case GeometryCombineMode.Intersect:
				return flag && flag2;
			case GeometryCombineMode.Xor:
				return flag != flag2;
			case GeometryCombineMode.Exclude:
				return flag && !flag2;
			default:
				return false;
			}
		}

		/// <summary>Obtém a área da região preenchida.</summary>
		/// <param name="tolerance">A tolerância computacional a erros.</param>
		/// <param name="type">Especifica como a tolerância a erros será interpretada.</param>
		/// <returns>A área da região preenchida desta geometria combinada.</returns>
		// Token: 0x06001FA5 RID: 8101 RVA: 0x00081A58 File Offset: 0x00080E58
		public override double GetArea(double tolerance, ToleranceType type)
		{
			base.ReadPreamble();
			return this.GetAsPathGeometry().GetArea(tolerance, type);
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x00081A78 File Offset: 0x00080E78
		internal override PathFigureCollection GetTransformedFigureCollection(Transform transform)
		{
			return this.GetAsPathGeometry().GetTransformedFigureCollection(transform);
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x00081A94 File Offset: 0x00080E94
		internal override Geometry.PathGeometryData GetPathGeometryData()
		{
			if (this.IsObviouslyEmpty())
			{
				return Geometry.GetEmptyPathGeometryData();
			}
			PathGeometry asPathGeometry = this.GetAsPathGeometry();
			return asPathGeometry.GetPathGeometryData();
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x00081ABC File Offset: 0x00080EBC
		internal override PathGeometry GetAsPathGeometry()
		{
			Geometry geometry = this.Geometry1;
			Geometry geometry2 = this.Geometry2;
			PathGeometry geometry3 = (geometry == null) ? new PathGeometry() : geometry.GetAsPathGeometry();
			Geometry geometry4 = (geometry2 == null) ? new PathGeometry() : geometry2.GetAsPathGeometry();
			return Geometry.Combine(geometry3, geometry4, this.GeometryCombineMode, base.Transform);
		}

		/// <summary>Determina se este objeto <see cref="T:System.Windows.Media.CombinedGeometry" /> está vazio.</summary>
		/// <returns>
		///   <see langword="true" /> se este <see cref="T:System.Windows.Media.CombinedGeometry" /> estiver vazio, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001FA9 RID: 8105 RVA: 0x00081B0C File Offset: 0x00080F0C
		public override bool IsEmpty()
		{
			return this.GetAsPathGeometry().IsEmpty();
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x00081B24 File Offset: 0x00080F24
		internal override bool IsObviouslyEmpty()
		{
			Geometry geometry = this.Geometry1;
			Geometry geometry2 = this.Geometry2;
			bool flag = geometry == null || geometry.IsObviouslyEmpty();
			bool flag2 = geometry2 == null || geometry2.IsObviouslyEmpty();
			if (this.GeometryCombineMode == GeometryCombineMode.Intersect)
			{
				return flag || flag2;
			}
			if (this.GeometryCombineMode == GeometryCombineMode.Exclude)
			{
				return flag;
			}
			return flag && flag2;
		}

		/// <summary>Determina se este objeto <see cref="T:System.Windows.Media.CombinedGeometry" /> pode ter segmentos curvos.</summary>
		/// <returns>
		///   <see langword="true" /> caso este objeto <see cref="T:System.Windows.Media.CombinedGeometry" /> possa ter segmentos de curva; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001FAB RID: 8107 RVA: 0x00081B74 File Offset: 0x00080F74
		public override bool MayHaveCurves()
		{
			Geometry geometry = this.Geometry1;
			Geometry geometry2 = this.Geometry2;
			return (geometry != null && geometry.MayHaveCurves()) || (geometry2 != null && geometry2.MayHaveCurves());
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.CombinedGeometry" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06001FAC RID: 8108 RVA: 0x00081BA8 File Offset: 0x00080FA8
		public new CombinedGeometry Clone()
		{
			return (CombinedGeometry)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.CombinedGeometry" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06001FAD RID: 8109 RVA: 0x00081BC0 File Offset: 0x00080FC0
		public new CombinedGeometry CloneCurrentValue()
		{
			return (CombinedGeometry)base.CloneCurrentValue();
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x00081BD8 File Offset: 0x00080FD8
		private static void GeometryCombineModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			CombinedGeometry combinedGeometry = (CombinedGeometry)d;
			combinedGeometry.PropertyChanged(CombinedGeometry.GeometryCombineModeProperty);
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x00081BF8 File Offset: 0x00080FF8
		private static void Geometry1PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			CombinedGeometry combinedGeometry = (CombinedGeometry)d;
			Geometry resource = (Geometry)e.OldValue;
			Geometry resource2 = (Geometry)e.NewValue;
			Dispatcher dispatcher = combinedGeometry.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = combinedGeometry;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						combinedGeometry.ReleaseResource(resource, channel);
						combinedGeometry.AddRefResource(resource2, channel);
					}
				}
			}
			combinedGeometry.PropertyChanged(CombinedGeometry.Geometry1Property);
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x00081CC0 File Offset: 0x000810C0
		private static void Geometry2PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			CombinedGeometry combinedGeometry = (CombinedGeometry)d;
			Geometry resource = (Geometry)e.OldValue;
			Geometry resource2 = (Geometry)e.NewValue;
			Dispatcher dispatcher = combinedGeometry.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = combinedGeometry;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						combinedGeometry.ReleaseResource(resource, channel);
						combinedGeometry.AddRefResource(resource2, channel);
					}
				}
			}
			combinedGeometry.PropertyChanged(CombinedGeometry.Geometry2Property);
		}

		/// <summary>Obtém ou define o método pelo qual as duas geometrias (especificadas pelas propriedades <see cref="P:System.Windows.Media.CombinedGeometry.Geometry1" /> e <see cref="P:System.Windows.Media.CombinedGeometry.Geometry2" />) são combinadas.</summary>
		/// <returns>O método pelo qual <see cref="P:System.Windows.Media.CombinedGeometry.Geometry1" /> e <see cref="P:System.Windows.Media.CombinedGeometry.Geometry2" /> são combinadas. O valor padrão é <see cref="F:System.Windows.Media.GeometryCombineMode.Union" />.</returns>
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x00081D88 File Offset: 0x00081188
		// (set) Token: 0x06001FB2 RID: 8114 RVA: 0x00081DA8 File Offset: 0x000811A8
		public GeometryCombineMode GeometryCombineMode
		{
			get
			{
				return (GeometryCombineMode)base.GetValue(CombinedGeometry.GeometryCombineModeProperty);
			}
			set
			{
				base.SetValueInternal(CombinedGeometry.GeometryCombineModeProperty, value);
			}
		}

		/// <summary>Obtém ou define o primeiro objeto <see cref="T:System.Windows.Media.Geometry" /> deste objeto <see cref="T:System.Windows.Media.CombinedGeometry" />.</summary>
		/// <returns>A primeira <see cref="T:System.Windows.Media.Geometry" /> objeto a ser combinado.</returns>
		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001FB3 RID: 8115 RVA: 0x00081DC8 File Offset: 0x000811C8
		// (set) Token: 0x06001FB4 RID: 8116 RVA: 0x00081DE8 File Offset: 0x000811E8
		public Geometry Geometry1
		{
			get
			{
				return (Geometry)base.GetValue(CombinedGeometry.Geometry1Property);
			}
			set
			{
				base.SetValueInternal(CombinedGeometry.Geometry1Property, value);
			}
		}

		/// <summary>Obtém ou define o segundo objeto <see cref="T:System.Windows.Media.Geometry" /> desse objeto <see cref="T:System.Windows.Media.CombinedGeometry" />.</summary>
		/// <returns>O segundo objeto <see cref="T:System.Windows.Media.Geometry" />.</returns>
		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x00081E04 File Offset: 0x00081204
		// (set) Token: 0x06001FB6 RID: 8118 RVA: 0x00081E24 File Offset: 0x00081224
		public Geometry Geometry2
		{
			get
			{
				return (Geometry)base.GetValue(CombinedGeometry.Geometry2Property);
			}
			set
			{
				base.SetValueInternal(CombinedGeometry.Geometry2Property, value);
			}
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x00081E40 File Offset: 0x00081240
		protected override Freezable CreateInstanceCore()
		{
			return new CombinedGeometry();
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x00081E54 File Offset: 0x00081254
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Transform transform = base.Transform;
				Geometry geometry = this.Geometry1;
				Geometry geometry2 = this.Geometry2;
				DUCE.ResourceHandle hTransform;
				if (transform == null || transform == Transform.Identity)
				{
					hTransform = DUCE.ResourceHandle.Null;
				}
				else
				{
					hTransform = ((DUCE.IResource)transform).GetHandle(channel);
				}
				DUCE.ResourceHandle hGeometry = (geometry != null) ? ((DUCE.IResource)geometry).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.ResourceHandle hGeometry2 = (geometry2 != null) ? ((DUCE.IResource)geometry2).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.MILCMD_COMBINEDGEOMETRY milcmd_COMBINEDGEOMETRY;
				milcmd_COMBINEDGEOMETRY.Type = MILCMD.MilCmdCombinedGeometry;
				milcmd_COMBINEDGEOMETRY.Handle = this._duceResource.GetHandle(channel);
				milcmd_COMBINEDGEOMETRY.hTransform = hTransform;
				milcmd_COMBINEDGEOMETRY.GeometryCombineMode = this.GeometryCombineMode;
				milcmd_COMBINEDGEOMETRY.hGeometry1 = hGeometry;
				milcmd_COMBINEDGEOMETRY.hGeometry2 = hGeometry2;
				channel.SendCommand((byte*)(&milcmd_COMBINEDGEOMETRY), sizeof(DUCE.MILCMD_COMBINEDGEOMETRY));
			}
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x00081F28 File Offset: 0x00081328
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_COMBINEDGEOMETRY))
			{
				Transform transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).AddRefOnChannel(channel);
				}
				Geometry geometry = this.Geometry1;
				if (geometry != null)
				{
					((DUCE.IResource)geometry).AddRefOnChannel(channel);
				}
				Geometry geometry2 = this.Geometry2;
				if (geometry2 != null)
				{
					((DUCE.IResource)geometry2).AddRefOnChannel(channel);
				}
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x00081F98 File Offset: 0x00081398
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				Transform transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).ReleaseOnChannel(channel);
				}
				Geometry geometry = this.Geometry1;
				if (geometry != null)
				{
					((DUCE.IResource)geometry).ReleaseOnChannel(channel);
				}
				Geometry geometry2 = this.Geometry2;
				if (geometry2 != null)
				{
					((DUCE.IResource)geometry2).ReleaseOnChannel(channel);
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x00081FF0 File Offset: 0x000813F0
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x0008200C File Offset: 0x0008140C
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x00082024 File Offset: 0x00081424
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001FBE RID: 8126 RVA: 0x00082040 File Offset: 0x00081440
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x00082050 File Offset: 0x00081450
		static CombinedGeometry()
		{
			Type typeFromHandle = typeof(CombinedGeometry);
			CombinedGeometry.GeometryCombineModeProperty = Animatable.RegisterProperty("GeometryCombineMode", typeof(GeometryCombineMode), typeFromHandle, GeometryCombineMode.Union, new PropertyChangedCallback(CombinedGeometry.GeometryCombineModePropertyChanged), new ValidateValueCallback(ValidateEnums.IsGeometryCombineModeValid), false, null);
			CombinedGeometry.Geometry1Property = Animatable.RegisterProperty("Geometry1", typeof(Geometry), typeFromHandle, Geometry.Empty, new PropertyChangedCallback(CombinedGeometry.Geometry1PropertyChanged), null, false, null);
			CombinedGeometry.Geometry2Property = Animatable.RegisterProperty("Geometry2", typeof(Geometry), typeFromHandle, Geometry.Empty, new PropertyChangedCallback(CombinedGeometry.Geometry2PropertyChanged), null, false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.CombinedGeometry.GeometryCombineMode" />.</summary>
		// Token: 0x04001070 RID: 4208
		public static readonly DependencyProperty GeometryCombineModeProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.CombinedGeometry.Geometry1" />.</summary>
		// Token: 0x04001071 RID: 4209
		public static readonly DependencyProperty Geometry1Property;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.CombinedGeometry.Geometry2" />.</summary>
		// Token: 0x04001072 RID: 4210
		public static readonly DependencyProperty Geometry2Property;

		// Token: 0x04001073 RID: 4211
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x04001074 RID: 4212
		internal const GeometryCombineMode c_GeometryCombineMode = GeometryCombineMode.Union;

		// Token: 0x04001075 RID: 4213
		internal static Geometry s_Geometry1 = Geometry.Empty;

		// Token: 0x04001076 RID: 4214
		internal static Geometry s_Geometry2 = Geometry.Empty;
	}
}
