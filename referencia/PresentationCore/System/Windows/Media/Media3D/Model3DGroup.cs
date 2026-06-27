using System;
using System.Security;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal;

namespace System.Windows.Media.Media3D
{
	/// <summary>Habilita o uso de vários modelos 3D como uma unidade.</summary>
	// Token: 0x0200046A RID: 1130
	[ContentProperty("Children")]
	public sealed class Model3DGroup : Model3D
	{
		// Token: 0x06002FAB RID: 12203 RVA: 0x000BEE38 File Offset: 0x000BE238
		internal override void RayHitTestCore(RayHitTestParameters rayParams)
		{
			Model3DCollection children = this.Children;
			if (children == null)
			{
				return;
			}
			for (int i = children.Count - 1; i >= 0; i--)
			{
				Model3D model3D = children.Internal_GetItem(i);
				model3D.RayHitTest(rayParams);
			}
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x000BEE74 File Offset: 0x000BE274
		internal override Rect3D CalculateSubgraphBoundsInnerSpace()
		{
			Model3DCollection children = this.Children;
			if (children == null)
			{
				return Rect3D.Empty;
			}
			Rect3D empty = Rect3D.Empty;
			int i = 0;
			int count = children.Count;
			while (i < count)
			{
				Model3D model3D = children.Internal_GetItem(i);
				empty.Union(model3D.CalculateSubgraphBoundsOuterSpace());
				i++;
			}
			return empty;
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06002FAD RID: 12205 RVA: 0x000BEEC4 File Offset: 0x000BE2C4
		internal static Model3DGroup EmptyGroup
		{
			get
			{
				if (Model3DGroup.s_empty == null)
				{
					Model3DGroup.s_empty = new Model3DGroup();
					Model3DGroup.s_empty.Freeze();
				}
				return Model3DGroup.s_empty;
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.Model3DGroup" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002FAE RID: 12206 RVA: 0x000BEEF4 File Offset: 0x000BE2F4
		public new Model3DGroup Clone()
		{
			return (Model3DGroup)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.Model3DGroup" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002FAF RID: 12207 RVA: 0x000BEF0C File Offset: 0x000BE30C
		public new Model3DGroup CloneCurrentValue()
		{
			return (Model3DGroup)base.CloneCurrentValue();
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x000BEF24 File Offset: 0x000BE324
		private static void ChildrenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			Model3DGroup model3DGroup = (Model3DGroup)d;
			Model3DCollection model3DCollection = null;
			Model3DCollection model3DCollection2 = null;
			if (e.OldValueSource != BaseValueSourceInternal.Default || e.IsOldValueModified)
			{
				model3DCollection = (Model3DCollection)e.OldValue;
				if (model3DCollection != null && !model3DCollection.IsFrozen)
				{
					model3DCollection.ItemRemoved -= model3DGroup.ChildrenItemRemoved;
					model3DCollection.ItemInserted -= model3DGroup.ChildrenItemInserted;
				}
			}
			if (e.NewValueSource != BaseValueSourceInternal.Default || e.IsNewValueModified)
			{
				model3DCollection2 = (Model3DCollection)e.NewValue;
				if (model3DCollection2 != null && !model3DCollection2.IsFrozen)
				{
					model3DCollection2.ItemInserted += model3DGroup.ChildrenItemInserted;
					model3DCollection2.ItemRemoved += model3DGroup.ChildrenItemRemoved;
				}
			}
			if (model3DCollection != model3DCollection2 && model3DGroup.Dispatcher != null)
			{
				using (CompositionEngineLock.Acquire())
				{
					DUCE.IResource resource = model3DGroup;
					int channelCount = resource.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource.GetChannel(i);
						if (model3DCollection2 != null)
						{
							int count = model3DCollection2.Count;
							for (int j = 0; j < count; j++)
							{
								DUCE.IResource resource2 = model3DCollection2.Internal_GetItem(j);
								resource2.AddRefOnChannel(channel);
							}
						}
						if (model3DCollection != null)
						{
							int count2 = model3DCollection.Count;
							for (int k = 0; k < count2; k++)
							{
								DUCE.IResource resource3 = model3DCollection.Internal_GetItem(k);
								resource3.ReleaseOnChannel(channel);
							}
						}
					}
				}
			}
			model3DGroup.PropertyChanged(Model3DGroup.ChildrenProperty);
		}

		/// <summary>Obtém ou define uma coleção de objetos <see cref="T:System.Windows.Media.Media3D.Model3D" />.</summary>
		/// <returns>Coleção de objetos <see cref="T:System.Windows.Media.Media3D.Model3D" />.</returns>
		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x000BF0C8 File Offset: 0x000BE4C8
		// (set) Token: 0x06002FB2 RID: 12210 RVA: 0x000BF0E8 File Offset: 0x000BE4E8
		public Model3DCollection Children
		{
			get
			{
				return (Model3DCollection)base.GetValue(Model3DGroup.ChildrenProperty);
			}
			set
			{
				base.SetValueInternal(Model3DGroup.ChildrenProperty, value);
			}
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x000BF104 File Offset: 0x000BE504
		protected override Freezable CreateInstanceCore()
		{
			return new Model3DGroup();
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000BF118 File Offset: 0x000BE518
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Transform3D transform = base.Transform;
				Model3DCollection children = this.Children;
				DUCE.ResourceHandle htransform;
				if (transform == null || transform == Transform3D.Identity)
				{
					htransform = DUCE.ResourceHandle.Null;
				}
				else
				{
					htransform = ((DUCE.IResource)transform).GetHandle(channel);
				}
				int num = (children == null) ? 0 : children.Count;
				DUCE.MILCMD_MODEL3DGROUP milcmd_MODEL3DGROUP;
				milcmd_MODEL3DGROUP.Type = MILCMD.MilCmdModel3DGroup;
				milcmd_MODEL3DGROUP.Handle = this._duceResource.GetHandle(channel);
				milcmd_MODEL3DGROUP.htransform = htransform;
				milcmd_MODEL3DGROUP.ChildrenSize = (uint)(sizeof(DUCE.ResourceHandle) * num);
				channel.BeginCommand((byte*)(&milcmd_MODEL3DGROUP), sizeof(DUCE.MILCMD_MODEL3DGROUP), (int)milcmd_MODEL3DGROUP.ChildrenSize);
				for (int i = 0; i < num; i++)
				{
					DUCE.ResourceHandle handle = ((DUCE.IResource)children.Internal_GetItem(i)).GetHandle(channel);
					channel.AppendCommandData((byte*)(&handle), sizeof(DUCE.ResourceHandle));
				}
				channel.EndCommand();
			}
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000BF1F4 File Offset: 0x000BE5F4
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_MODEL3DGROUP))
			{
				Transform3D transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).AddRefOnChannel(channel);
				}
				Model3DCollection children = this.Children;
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

		// Token: 0x06002FB6 RID: 12214 RVA: 0x000BF26C File Offset: 0x000BE66C
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				Transform3D transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).ReleaseOnChannel(channel);
				}
				Model3DCollection children = this.Children;
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

		// Token: 0x06002FB7 RID: 12215 RVA: 0x000BF2CC File Offset: 0x000BE6CC
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000BF2E8 File Offset: 0x000BE6E8
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000BF300 File Offset: 0x000BE700
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x000BF31C File Offset: 0x000BE71C
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

		// Token: 0x06002FBB RID: 12219 RVA: 0x000BF3A0 File Offset: 0x000BE7A0
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

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06002FBC RID: 12220 RVA: 0x000BF420 File Offset: 0x000BE820
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x000BF430 File Offset: 0x000BE830
		static Model3DGroup()
		{
			Type typeFromHandle = typeof(Model3DGroup);
			Model3DGroup.ChildrenProperty = Animatable.RegisterProperty("Children", typeof(Model3DCollection), typeFromHandle, new FreezableDefaultValueFactory(Model3DCollection.Empty), new PropertyChangedCallback(Model3DGroup.ChildrenPropertyChanged), null, false, null);
		}

		// Token: 0x04001537 RID: 5431
		private static Model3DGroup s_empty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.Model3DGroup.Children" />.</summary>
		// Token: 0x04001538 RID: 5432
		public static readonly DependencyProperty ChildrenProperty;

		// Token: 0x04001539 RID: 5433
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x0400153A RID: 5434
		internal static Model3DCollection s_Children = Model3DCollection.Empty;
	}
}
