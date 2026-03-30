using System;
using System.Security;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal;

namespace System.Windows.Media.Media3D
{
	/// <summary>Representa uma transformação que é uma composição dos filhos da <see cref="T:System.Windows.Media.Media3D.Transform3D" /> em sua <see cref="T:System.Windows.Media.Media3D.Transform3DCollection" />.</summary>
	// Token: 0x02000482 RID: 1154
	[ContentProperty("Children")]
	public sealed class Transform3DGroup : Transform3D
	{
		/// <summary>Obtém um <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> que indica o valor atual de transformação.</summary>
		/// <returns>Matrix3D que indica o valor de transformação atual.</returns>
		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x060031ED RID: 12781 RVA: 0x000C746C File Offset: 0x000C686C
		public override Matrix3D Value
		{
			get
			{
				base.ReadPreamble();
				Matrix3D result = default(Matrix3D);
				this.Append(ref result);
				return result;
			}
		}

		/// <summary>Obtém um valor que indica se a transformação é afim.</summary>
		/// <returns>True se a transformação é afim; False caso contrário.</returns>
		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x060031EE RID: 12782 RVA: 0x000C7490 File Offset: 0x000C6890
		public override bool IsAffine
		{
			get
			{
				base.ReadPreamble();
				Transform3DCollection children = this.Children;
				if (children != null)
				{
					int i = 0;
					int count = children.Count;
					while (i < count)
					{
						Transform3D transform3D = children.Internal_GetItem(i);
						if (!transform3D.IsAffine)
						{
							return false;
						}
						i++;
					}
				}
				return true;
			}
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x000C74D4 File Offset: 0x000C68D4
		internal override void Append(ref Matrix3D matrix)
		{
			Transform3DCollection children = this.Children;
			if (children != null)
			{
				int i = 0;
				int count = children.Count;
				while (i < count)
				{
					children.Internal_GetItem(i).Append(ref matrix);
					i++;
				}
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.Transform3DGroup" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060031F0 RID: 12784 RVA: 0x000C750C File Offset: 0x000C690C
		public new Transform3DGroup Clone()
		{
			return (Transform3DGroup)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.Transform3DGroup" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060031F1 RID: 12785 RVA: 0x000C7524 File Offset: 0x000C6924
		public new Transform3DGroup CloneCurrentValue()
		{
			return (Transform3DGroup)base.CloneCurrentValue();
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x000C753C File Offset: 0x000C693C
		private static void ChildrenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			Transform3DGroup transform3DGroup = (Transform3DGroup)d;
			Transform3DCollection transform3DCollection = null;
			Transform3DCollection transform3DCollection2 = null;
			if (e.OldValueSource != BaseValueSourceInternal.Default || e.IsOldValueModified)
			{
				transform3DCollection = (Transform3DCollection)e.OldValue;
				if (transform3DCollection != null && !transform3DCollection.IsFrozen)
				{
					transform3DCollection.ItemRemoved -= transform3DGroup.ChildrenItemRemoved;
					transform3DCollection.ItemInserted -= transform3DGroup.ChildrenItemInserted;
				}
			}
			if (e.NewValueSource != BaseValueSourceInternal.Default || e.IsNewValueModified)
			{
				transform3DCollection2 = (Transform3DCollection)e.NewValue;
				if (transform3DCollection2 != null && !transform3DCollection2.IsFrozen)
				{
					transform3DCollection2.ItemInserted += transform3DGroup.ChildrenItemInserted;
					transform3DCollection2.ItemRemoved += transform3DGroup.ChildrenItemRemoved;
				}
			}
			if (transform3DCollection != transform3DCollection2 && transform3DGroup.Dispatcher != null)
			{
				using (CompositionEngineLock.Acquire())
				{
					DUCE.IResource resource = transform3DGroup;
					int channelCount = resource.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource.GetChannel(i);
						if (transform3DCollection2 != null)
						{
							int count = transform3DCollection2.Count;
							for (int j = 0; j < count; j++)
							{
								DUCE.IResource resource2 = transform3DCollection2.Internal_GetItem(j);
								resource2.AddRefOnChannel(channel);
							}
						}
						if (transform3DCollection != null)
						{
							int count2 = transform3DCollection.Count;
							for (int k = 0; k < count2; k++)
							{
								DUCE.IResource resource3 = transform3DCollection.Internal_GetItem(k);
								resource3.ReleaseOnChannel(channel);
							}
						}
					}
				}
			}
			transform3DGroup.PropertyChanged(Transform3DGroup.ChildrenProperty);
		}

		/// <summary>Obtém ou define uma coleção de objetos <see cref="T:System.Windows.Media.Media3D.Transform3D" />.</summary>
		/// <returns>Coleção de objetos <see cref="T:System.Windows.Media.Media3D.Transform3D" />. O valor padrão é uma coleção vazia.</returns>
		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x060031F3 RID: 12787 RVA: 0x000C76E0 File Offset: 0x000C6AE0
		// (set) Token: 0x060031F4 RID: 12788 RVA: 0x000C7700 File Offset: 0x000C6B00
		public Transform3DCollection Children
		{
			get
			{
				return (Transform3DCollection)base.GetValue(Transform3DGroup.ChildrenProperty);
			}
			set
			{
				base.SetValueInternal(Transform3DGroup.ChildrenProperty, value);
			}
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x000C771C File Offset: 0x000C6B1C
		protected override Freezable CreateInstanceCore()
		{
			return new Transform3DGroup();
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x000C7730 File Offset: 0x000C6B30
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Transform3DCollection children = this.Children;
				int num = (children == null) ? 0 : children.Count;
				DUCE.MILCMD_TRANSFORM3DGROUP milcmd_TRANSFORM3DGROUP;
				milcmd_TRANSFORM3DGROUP.Type = MILCMD.MilCmdTransform3DGroup;
				milcmd_TRANSFORM3DGROUP.Handle = this._duceResource.GetHandle(channel);
				milcmd_TRANSFORM3DGROUP.ChildrenSize = (uint)(sizeof(DUCE.ResourceHandle) * num);
				channel.BeginCommand((byte*)(&milcmd_TRANSFORM3DGROUP), sizeof(DUCE.MILCMD_TRANSFORM3DGROUP), (int)milcmd_TRANSFORM3DGROUP.ChildrenSize);
				for (int i = 0; i < num; i++)
				{
					DUCE.ResourceHandle handle = ((DUCE.IResource)children.Internal_GetItem(i)).GetHandle(channel);
					channel.AppendCommandData((byte*)(&handle), sizeof(DUCE.ResourceHandle));
				}
				channel.EndCommand();
			}
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x000C77E0 File Offset: 0x000C6BE0
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_TRANSFORM3DGROUP))
			{
				Transform3DCollection children = this.Children;
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

		// Token: 0x060031F8 RID: 12792 RVA: 0x000C7844 File Offset: 0x000C6C44
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				Transform3DCollection children = this.Children;
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

		// Token: 0x060031F9 RID: 12793 RVA: 0x000C7890 File Offset: 0x000C6C90
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x000C78AC File Offset: 0x000C6CAC
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x000C78C4 File Offset: 0x000C6CC4
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x000C78E0 File Offset: 0x000C6CE0
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

		// Token: 0x060031FD RID: 12797 RVA: 0x000C7964 File Offset: 0x000C6D64
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

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x060031FE RID: 12798 RVA: 0x000C79E4 File Offset: 0x000C6DE4
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x000C79F4 File Offset: 0x000C6DF4
		static Transform3DGroup()
		{
			Type typeFromHandle = typeof(Transform3DGroup);
			Transform3DGroup.ChildrenProperty = Animatable.RegisterProperty("Children", typeof(Transform3DCollection), typeFromHandle, new FreezableDefaultValueFactory(Transform3DCollection.Empty), new PropertyChangedCallback(Transform3DGroup.ChildrenPropertyChanged), null, false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.Transform3DGroup.Children" />.</summary>
		// Token: 0x040015B9 RID: 5561
		public static readonly DependencyProperty ChildrenProperty;

		// Token: 0x040015BA RID: 5562
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x040015BB RID: 5563
		internal static Transform3DCollection s_Children = Transform3DCollection.Empty;
	}
}
