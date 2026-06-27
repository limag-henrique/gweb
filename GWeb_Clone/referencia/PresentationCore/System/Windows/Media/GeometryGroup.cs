using System;
using System.Security;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal;
using MS.Internal.KnownBoxes;

namespace System.Windows.Media
{
	/// <summary>Representa uma geometria de composição, formada por outros objetos <see cref="T:System.Windows.Media.Geometry" />.</summary>
	// Token: 0x020003B0 RID: 944
	[ContentProperty("Children")]
	public sealed class GeometryGroup : Geometry
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.GeometryGroup" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060023DA RID: 9178 RVA: 0x00090674 File Offset: 0x0008FA74
		public new GeometryGroup Clone()
		{
			return (GeometryGroup)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.GeometryGroup" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060023DB RID: 9179 RVA: 0x0009068C File Offset: 0x0008FA8C
		public new GeometryGroup CloneCurrentValue()
		{
			return (GeometryGroup)base.CloneCurrentValue();
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000906A4 File Offset: 0x0008FAA4
		private static void FillRulePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			GeometryGroup geometryGroup = (GeometryGroup)d;
			geometryGroup.PropertyChanged(GeometryGroup.FillRuleProperty);
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000906C4 File Offset: 0x0008FAC4
		private static void ChildrenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			GeometryGroup geometryGroup = (GeometryGroup)d;
			GeometryCollection geometryCollection = null;
			GeometryCollection geometryCollection2 = null;
			if (e.OldValueSource != BaseValueSourceInternal.Default || e.IsOldValueModified)
			{
				geometryCollection = (GeometryCollection)e.OldValue;
				if (geometryCollection != null && !geometryCollection.IsFrozen)
				{
					geometryCollection.ItemRemoved -= geometryGroup.ChildrenItemRemoved;
					geometryCollection.ItemInserted -= geometryGroup.ChildrenItemInserted;
				}
			}
			if (e.NewValueSource != BaseValueSourceInternal.Default || e.IsNewValueModified)
			{
				geometryCollection2 = (GeometryCollection)e.NewValue;
				if (geometryCollection2 != null && !geometryCollection2.IsFrozen)
				{
					geometryCollection2.ItemInserted += geometryGroup.ChildrenItemInserted;
					geometryCollection2.ItemRemoved += geometryGroup.ChildrenItemRemoved;
				}
			}
			if (geometryCollection != geometryCollection2 && geometryGroup.Dispatcher != null)
			{
				using (CompositionEngineLock.Acquire())
				{
					DUCE.IResource resource = geometryGroup;
					int channelCount = resource.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource.GetChannel(i);
						if (geometryCollection2 != null)
						{
							int count = geometryCollection2.Count;
							for (int j = 0; j < count; j++)
							{
								DUCE.IResource resource2 = geometryCollection2.Internal_GetItem(j);
								resource2.AddRefOnChannel(channel);
							}
						}
						if (geometryCollection != null)
						{
							int count2 = geometryCollection.Count;
							for (int k = 0; k < count2; k++)
							{
								DUCE.IResource resource3 = geometryCollection.Internal_GetItem(k);
								resource3.ReleaseOnChannel(channel);
							}
						}
					}
				}
			}
			geometryGroup.PropertyChanged(GeometryGroup.ChildrenProperty);
		}

		/// <summary>Obtém ou define como as áreas de interseção dos objetos contidos neste <see cref="T:System.Windows.Media.GeometryGroup" /> são combinadas.</summary>
		/// <returns>Especifica como as áreas de interseção são combinadas para formar a área resultante.  O valor padrão é EvenOdd.</returns>
		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060023DE RID: 9182 RVA: 0x00090868 File Offset: 0x0008FC68
		// (set) Token: 0x060023DF RID: 9183 RVA: 0x00090888 File Offset: 0x0008FC88
		public FillRule FillRule
		{
			get
			{
				return (FillRule)base.GetValue(GeometryGroup.FillRuleProperty);
			}
			set
			{
				base.SetValueInternal(GeometryGroup.FillRuleProperty, FillRuleBoxes.Box(value));
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.GeometryCollection" /> que contém os objetos que definem este <see cref="T:System.Windows.Media.GeometryGroup" />.</summary>
		/// <returns>Uma coleção que contém os filhos deste <see cref="T:System.Windows.Media.GeometryGroup" />.</returns>
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060023E0 RID: 9184 RVA: 0x000908A8 File Offset: 0x0008FCA8
		// (set) Token: 0x060023E1 RID: 9185 RVA: 0x000908C8 File Offset: 0x0008FCC8
		public GeometryCollection Children
		{
			get
			{
				return (GeometryCollection)base.GetValue(GeometryGroup.ChildrenProperty);
			}
			set
			{
				base.SetValueInternal(GeometryGroup.ChildrenProperty, value);
			}
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x000908E4 File Offset: 0x0008FCE4
		protected override Freezable CreateInstanceCore()
		{
			return new GeometryGroup();
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x000908F8 File Offset: 0x0008FCF8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Transform transform = base.Transform;
				GeometryCollection children = this.Children;
				DUCE.ResourceHandle hTransform;
				if (transform == null || transform == Transform.Identity)
				{
					hTransform = DUCE.ResourceHandle.Null;
				}
				else
				{
					hTransform = ((DUCE.IResource)transform).GetHandle(channel);
				}
				int num = (children == null) ? 0 : children.Count;
				DUCE.MILCMD_GEOMETRYGROUP milcmd_GEOMETRYGROUP;
				milcmd_GEOMETRYGROUP.Type = MILCMD.MilCmdGeometryGroup;
				milcmd_GEOMETRYGROUP.Handle = this._duceResource.GetHandle(channel);
				milcmd_GEOMETRYGROUP.hTransform = hTransform;
				milcmd_GEOMETRYGROUP.FillRule = this.FillRule;
				milcmd_GEOMETRYGROUP.ChildrenSize = (uint)(sizeof(DUCE.ResourceHandle) * num);
				channel.BeginCommand((byte*)(&milcmd_GEOMETRYGROUP), sizeof(DUCE.MILCMD_GEOMETRYGROUP), (int)milcmd_GEOMETRYGROUP.ChildrenSize);
				for (int i = 0; i < num; i++)
				{
					DUCE.ResourceHandle handle = ((DUCE.IResource)children.Internal_GetItem(i)).GetHandle(channel);
					channel.AppendCommandData((byte*)(&handle), sizeof(DUCE.ResourceHandle));
				}
				channel.EndCommand();
			}
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000909E0 File Offset: 0x0008FDE0
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_GEOMETRYGROUP))
			{
				Transform transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).AddRefOnChannel(channel);
				}
				GeometryCollection children = this.Children;
				if (children != null)
				{
					int count = children.Count;
					for (int i = 0; i < count; i++)
					{
						((DUCE.IResource)children.Internal_GetItem(i)).AddRefOnChannel(channel);
					}
				}
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x00090A58 File Offset: 0x0008FE58
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				Transform transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).ReleaseOnChannel(channel);
				}
				GeometryCollection children = this.Children;
				if (children != null)
				{
					int count = children.Count;
					for (int i = 0; i < count; i++)
					{
						((DUCE.IResource)children.Internal_GetItem(i)).ReleaseOnChannel(channel);
					}
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x00090AB8 File Offset: 0x0008FEB8
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x00090AD4 File Offset: 0x0008FED4
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x00090AEC File Offset: 0x0008FEEC
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x00090B08 File Offset: 0x0008FF08
		private void ChildrenItemInserted(object sender, object item)
		{
			if (base.Dispatcher != null)
			{
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = ((DUCE.IResource)this).GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = ((DUCE.IResource)this).GetChannel(i);
						DUCE.IResource resource = item as DUCE.IResource;
						if (resource != null)
						{
							resource.AddRefOnChannel(channel);
						}
						this.UpdateResource(channel, true);
					}
				}
			}
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x00090B8C File Offset: 0x0008FF8C
		private void ChildrenItemRemoved(object sender, object item)
		{
			if (base.Dispatcher != null)
			{
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = ((DUCE.IResource)this).GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = ((DUCE.IResource)this).GetChannel(i);
						this.UpdateResource(channel, true);
						DUCE.IResource resource = item as DUCE.IResource;
						if (resource != null)
						{
							resource.ReleaseOnChannel(channel);
						}
					}
				}
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060023EB RID: 9195 RVA: 0x00090C0C File Offset: 0x0009000C
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x00090C1C File Offset: 0x0009001C
		static GeometryGroup()
		{
			Type typeFromHandle = typeof(GeometryGroup);
			GeometryGroup.FillRuleProperty = Animatable.RegisterProperty("FillRule", typeof(FillRule), typeFromHandle, FillRule.EvenOdd, new PropertyChangedCallback(GeometryGroup.FillRulePropertyChanged), new ValidateValueCallback(ValidateEnums.IsFillRuleValid), false, null);
			GeometryGroup.ChildrenProperty = Animatable.RegisterProperty("Children", typeof(GeometryCollection), typeFromHandle, new FreezableDefaultValueFactory(GeometryCollection.Empty), new PropertyChangedCallback(GeometryGroup.ChildrenPropertyChanged), null, false, null);
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x00090CC0 File Offset: 0x000900C0
		internal override Geometry.PathGeometryData GetPathGeometryData()
		{
			PathGeometry asPathGeometry = this.GetAsPathGeometry();
			return asPathGeometry.GetPathGeometryData();
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x00090CDC File Offset: 0x000900DC
		internal override PathGeometry GetAsPathGeometry()
		{
			PathGeometry pathGeometry = new PathGeometry();
			pathGeometry.AddGeometry(this);
			pathGeometry.FillRule = this.FillRule;
			return pathGeometry;
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x00090D04 File Offset: 0x00090104
		internal override PathFigureCollection GetTransformedFigureCollection(Transform transform)
		{
			Transform transform2 = new MatrixTransform(base.GetCombinedMatrix(transform));
			PathFigureCollection pathFigureCollection = new PathFigureCollection();
			GeometryCollection children = this.Children;
			if (children != null)
			{
				for (int i = 0; i < children.Count; i++)
				{
					PathFigureCollection transformedFigureCollection = children.Internal_GetItem(i).GetTransformedFigureCollection(transform2);
					if (transformedFigureCollection != null)
					{
						int count = transformedFigureCollection.Count;
						for (int j = 0; j < count; j++)
						{
							pathFigureCollection.Add(transformedFigureCollection[j]);
						}
					}
				}
			}
			return pathFigureCollection;
		}

		/// <summary>Determina se este objeto <see cref="T:System.Windows.Media.GeometryGroup" /> está vazio.</summary>
		/// <returns>
		///   <see langword="true" /> se este <see cref="T:System.Windows.Media.GeometryGroup" /> estiver vazio, caso contrário, <see langword="false" />.</returns>
		// Token: 0x060023F1 RID: 9201 RVA: 0x00090D78 File Offset: 0x00090178
		public override bool IsEmpty()
		{
			GeometryCollection children = this.Children;
			if (children == null)
			{
				return true;
			}
			for (int i = 0; i < children.Count; i++)
			{
				if (!children[i].IsEmpty())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x00090DB4 File Offset: 0x000901B4
		internal override bool IsObviouslyEmpty()
		{
			GeometryCollection children = this.Children;
			return children == null || children.Count == 0;
		}

		/// <summary>Determina se este objeto <see cref="T:System.Windows.Media.GeometryGroup" /> pode ter segmentos curvos.</summary>
		/// <returns>
		///   <see langword="true" /> caso este objeto <see cref="T:System.Windows.Media.GeometryGroup" /> possa ter segmentos de curva; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060023F3 RID: 9203 RVA: 0x00090DD8 File Offset: 0x000901D8
		public override bool MayHaveCurves()
		{
			GeometryCollection children = this.Children;
			if (children == null)
			{
				return false;
			}
			for (int i = 0; i < children.Count; i++)
			{
				if (children[i].MayHaveCurves())
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.GeometryGroup.FillRule" />.</summary>
		// Token: 0x04001154 RID: 4436
		public static readonly DependencyProperty FillRuleProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.GeometryGroup.Children" />.</summary>
		// Token: 0x04001155 RID: 4437
		public static readonly DependencyProperty ChildrenProperty;

		// Token: 0x04001156 RID: 4438
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x04001157 RID: 4439
		internal const FillRule c_FillRule = FillRule.EvenOdd;

		// Token: 0x04001158 RID: 4440
		internal static GeometryCollection s_Children = GeometryCollection.Empty;
	}
}
