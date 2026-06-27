using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Threading;
using MS.Internal.Media3D;

namespace System.Windows.Media.Media3D
{
	/// <summary>Renderiza um <see cref="T:System.Windows.Media.Media3D.Geometry3D" /> com o <see cref="T:System.Windows.Media.Media3D.Material" /> especificado.</summary>
	// Token: 0x0200045E RID: 1118
	public sealed class GeometryModel3D : Model3D
	{
		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Media3D.GeometryModel3D" />.</summary>
		// Token: 0x06002E87 RID: 11911 RVA: 0x000B95F0 File Offset: 0x000B89F0
		public GeometryModel3D()
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Media3D.GeometryModel3D" /> com Geometry3D e Material especificados.</summary>
		/// <param name="geometry">Geometria do novo primitivo de malha.</param>
		/// <param name="material">Material do novo primitivo de malha.</param>
		// Token: 0x06002E88 RID: 11912 RVA: 0x000B9604 File Offset: 0x000B8A04
		public GeometryModel3D(Geometry3D geometry, Material material)
		{
			this.Geometry = geometry;
			this.Material = material;
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x000B9628 File Offset: 0x000B8A28
		internal override Rect3D CalculateSubgraphBoundsInnerSpace()
		{
			Geometry3D geometry = this.Geometry;
			if (geometry == null)
			{
				return Rect3D.Empty;
			}
			return geometry.Bounds;
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x000B964C File Offset: 0x000B8A4C
		internal override void RayHitTestCore(RayHitTestParameters rayParams)
		{
			Geometry3D geometry = this.Geometry;
			if (geometry != null)
			{
				rayParams.CurrentModel = this;
				FaceType faceType = FaceType.None;
				if (this.Material != null)
				{
					faceType |= FaceType.Front;
				}
				if (this.BackMaterial != null)
				{
					faceType |= FaceType.Back;
				}
				if (faceType != FaceType.None)
				{
					geometry.RayHitTest(rayParams, faceType);
				}
			}
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x000B9690 File Offset: 0x000B8A90
		internal void MaterialPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x000B96A0 File Offset: 0x000B8AA0
		internal void BackMaterialPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			this.MaterialPropertyChangedHook(e);
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.GeometryModel3D" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002E8D RID: 11917 RVA: 0x000B96B4 File Offset: 0x000B8AB4
		public new GeometryModel3D Clone()
		{
			return (GeometryModel3D)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.GeometryModel3D" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002E8E RID: 11918 RVA: 0x000B96CC File Offset: 0x000B8ACC
		public new GeometryModel3D CloneCurrentValue()
		{
			return (GeometryModel3D)base.CloneCurrentValue();
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x000B96E4 File Offset: 0x000B8AE4
		private static void GeometryPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			GeometryModel3D geometryModel3D = (GeometryModel3D)d;
			Geometry3D resource = (Geometry3D)e.OldValue;
			Geometry3D resource2 = (Geometry3D)e.NewValue;
			Dispatcher dispatcher = geometryModel3D.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = geometryModel3D;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						geometryModel3D.ReleaseResource(resource, channel);
						geometryModel3D.AddRefResource(resource2, channel);
					}
				}
			}
			geometryModel3D.PropertyChanged(GeometryModel3D.GeometryProperty);
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x000B97AC File Offset: 0x000B8BAC
		private static void MaterialPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			GeometryModel3D geometryModel3D = (GeometryModel3D)d;
			geometryModel3D.MaterialPropertyChangedHook(e);
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			Material resource = (Material)e.OldValue;
			Material resource2 = (Material)e.NewValue;
			Dispatcher dispatcher = geometryModel3D.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = geometryModel3D;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						geometryModel3D.ReleaseResource(resource, channel);
						geometryModel3D.AddRefResource(resource2, channel);
					}
				}
			}
			geometryModel3D.PropertyChanged(GeometryModel3D.MaterialProperty);
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x000B9878 File Offset: 0x000B8C78
		private static void BackMaterialPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			GeometryModel3D geometryModel3D = (GeometryModel3D)d;
			geometryModel3D.BackMaterialPropertyChangedHook(e);
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			Material resource = (Material)e.OldValue;
			Material resource2 = (Material)e.NewValue;
			Dispatcher dispatcher = geometryModel3D.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = geometryModel3D;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						geometryModel3D.ReleaseResource(resource, channel);
						geometryModel3D.AddRefResource(resource2, channel);
					}
				}
			}
			geometryModel3D.PropertyChanged(GeometryModel3D.BackMaterialProperty);
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Media3D.Geometry3D" /> que descreve a forma deste <see cref="T:System.Windows.Media.Media3D.GeometryModel3D" />.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Media3D.Geometry3D" /> que compõe o modelo.</returns>
		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06002E92 RID: 11922 RVA: 0x000B9944 File Offset: 0x000B8D44
		// (set) Token: 0x06002E93 RID: 11923 RVA: 0x000B9964 File Offset: 0x000B8D64
		public Geometry3D Geometry
		{
			get
			{
				return (Geometry3D)base.GetValue(GeometryModel3D.GeometryProperty);
			}
			set
			{
				base.SetValueInternal(GeometryModel3D.GeometryProperty, value);
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Media3D.Material" /> usado para renderizar a parte dianteira desse <see cref="T:System.Windows.Media.Media3D.GeometryModel3D" />.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Media3D.Material" /> aplicado ao <see cref="T:System.Windows.Media.Media3D.GeometryModel3D" />.</returns>
		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06002E94 RID: 11924 RVA: 0x000B9980 File Offset: 0x000B8D80
		// (set) Token: 0x06002E95 RID: 11925 RVA: 0x000B99A0 File Offset: 0x000B8DA0
		public Material Material
		{
			get
			{
				return (Material)base.GetValue(GeometryModel3D.MaterialProperty);
			}
			set
			{
				base.SetValueInternal(GeometryModel3D.MaterialProperty, value);
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Media3D.Material" /> usado para renderizar a parte traseira desse <see cref="T:System.Windows.Media.Media3D.GeometryModel3D" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.Media3D.Material" /> aplicado à parte traseira do <see cref="T:System.Windows.Media.Media3D.Model3D" />. O valor padrão é nulo.</returns>
		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06002E96 RID: 11926 RVA: 0x000B99BC File Offset: 0x000B8DBC
		// (set) Token: 0x06002E97 RID: 11927 RVA: 0x000B99DC File Offset: 0x000B8DDC
		public Material BackMaterial
		{
			get
			{
				return (Material)base.GetValue(GeometryModel3D.BackMaterialProperty);
			}
			set
			{
				base.SetValueInternal(GeometryModel3D.BackMaterialProperty, value);
			}
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x000B99F8 File Offset: 0x000B8DF8
		protected override Freezable CreateInstanceCore()
		{
			return new GeometryModel3D();
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x000B9A0C File Offset: 0x000B8E0C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Transform3D transform = base.Transform;
				Geometry3D geometry = this.Geometry;
				Material material = this.Material;
				Material backMaterial = this.BackMaterial;
				DUCE.ResourceHandle htransform;
				if (transform == null || transform == Transform3D.Identity)
				{
					htransform = DUCE.ResourceHandle.Null;
				}
				else
				{
					htransform = ((DUCE.IResource)transform).GetHandle(channel);
				}
				DUCE.ResourceHandle hgeometry = (geometry != null) ? ((DUCE.IResource)geometry).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.ResourceHandle hmaterial = (material != null) ? ((DUCE.IResource)material).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.ResourceHandle hbackMaterial = (backMaterial != null) ? ((DUCE.IResource)backMaterial).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.MILCMD_GEOMETRYMODEL3D milcmd_GEOMETRYMODEL3D;
				milcmd_GEOMETRYMODEL3D.Type = MILCMD.MilCmdGeometryModel3D;
				milcmd_GEOMETRYMODEL3D.Handle = this._duceResource.GetHandle(channel);
				milcmd_GEOMETRYMODEL3D.htransform = htransform;
				milcmd_GEOMETRYMODEL3D.hgeometry = hgeometry;
				milcmd_GEOMETRYMODEL3D.hmaterial = hmaterial;
				milcmd_GEOMETRYMODEL3D.hbackMaterial = hbackMaterial;
				channel.SendCommand((byte*)(&milcmd_GEOMETRYMODEL3D), sizeof(DUCE.MILCMD_GEOMETRYMODEL3D));
			}
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x000B9AF8 File Offset: 0x000B8EF8
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_GEOMETRYMODEL3D))
			{
				Transform3D transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).AddRefOnChannel(channel);
				}
				Geometry3D geometry = this.Geometry;
				if (geometry != null)
				{
					((DUCE.IResource)geometry).AddRefOnChannel(channel);
				}
				Material material = this.Material;
				if (material != null)
				{
					((DUCE.IResource)material).AddRefOnChannel(channel);
				}
				Material backMaterial = this.BackMaterial;
				if (backMaterial != null)
				{
					((DUCE.IResource)backMaterial).AddRefOnChannel(channel);
				}
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x000B9B7C File Offset: 0x000B8F7C
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				Transform3D transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).ReleaseOnChannel(channel);
				}
				Geometry3D geometry = this.Geometry;
				if (geometry != null)
				{
					((DUCE.IResource)geometry).ReleaseOnChannel(channel);
				}
				Material material = this.Material;
				if (material != null)
				{
					((DUCE.IResource)material).ReleaseOnChannel(channel);
				}
				Material backMaterial = this.BackMaterial;
				if (backMaterial != null)
				{
					((DUCE.IResource)backMaterial).ReleaseOnChannel(channel);
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x000B9BE4 File Offset: 0x000B8FE4
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x000B9C00 File Offset: 0x000B9000
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x000B9C18 File Offset: 0x000B9018
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x000B9C34 File Offset: 0x000B9034
		static GeometryModel3D()
		{
			Type typeFromHandle = typeof(GeometryModel3D);
			GeometryModel3D.GeometryProperty = Animatable.RegisterProperty("Geometry", typeof(Geometry3D), typeFromHandle, null, new PropertyChangedCallback(GeometryModel3D.GeometryPropertyChanged), null, false, null);
			GeometryModel3D.MaterialProperty = Animatable.RegisterProperty("Material", typeof(Material), typeFromHandle, null, new PropertyChangedCallback(GeometryModel3D.MaterialPropertyChanged), null, false, null);
			GeometryModel3D.BackMaterialProperty = Animatable.RegisterProperty("BackMaterial", typeof(Material), typeFromHandle, null, new PropertyChangedCallback(GeometryModel3D.BackMaterialPropertyChanged), null, false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.GeometryModel3D.Geometry" />.</summary>
		// Token: 0x040014FF RID: 5375
		public static readonly DependencyProperty GeometryProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.GeometryModel3D.Material" />.</summary>
		// Token: 0x04001500 RID: 5376
		public static readonly DependencyProperty MaterialProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.GeometryModel3D.BackMaterial" />.</summary>
		// Token: 0x04001501 RID: 5377
		public static readonly DependencyProperty BackMaterialProperty;

		// Token: 0x04001502 RID: 5378
		internal DUCE.MultiChannelResource _duceResource;
	}
}
