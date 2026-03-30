using System;
using System.Security;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal;

namespace System.Windows.Media
{
	/// <summary>Representa uma <see cref="T:System.Windows.Media.Transform" /> de composição de outros objetos <see cref="T:System.Windows.Media.Transform" />.</summary>
	// Token: 0x020003FB RID: 1019
	[ContentProperty("Children")]
	public sealed class TransformGroup : Transform
	{
		/// <summary>Cria uma cópia modificável deste <see cref="T:System.Windows.Media.TransformGroup" /> fazendo cópias em profundidade de seus valores.</summary>
		/// <returns>Uma cópia em profundidade modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado retorna <see langword="false" /> mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem é <see langword="true." /></returns>
		// Token: 0x06002877 RID: 10359 RVA: 0x000A2480 File Offset: 0x000A1880
		public new TransformGroup Clone()
		{
			return (TransformGroup)base.Clone();
		}

		/// <summary>Cria uma cópia modificável deste objeto <see cref="T:System.Windows.Media.TransformGroup" /> fazendo cópias em profundidade de seus valores. Esse método não copia referências de recurso, associações de dados ou animações, embora ele copie os valores atuais.</summary>
		/// <returns>Uma cópia em profundidade modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado é <see langword="false" /> mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem é <see langword="true" />.</returns>
		// Token: 0x06002878 RID: 10360 RVA: 0x000A2498 File Offset: 0x000A1898
		public new TransformGroup CloneCurrentValue()
		{
			return (TransformGroup)base.CloneCurrentValue();
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x000A24B0 File Offset: 0x000A18B0
		private static void ChildrenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			TransformGroup transformGroup = (TransformGroup)d;
			TransformCollection transformCollection = null;
			TransformCollection transformCollection2 = null;
			if (e.OldValueSource != BaseValueSourceInternal.Default || e.IsOldValueModified)
			{
				transformCollection = (TransformCollection)e.OldValue;
				if (transformCollection != null && !transformCollection.IsFrozen)
				{
					transformCollection.ItemRemoved -= transformGroup.ChildrenItemRemoved;
					transformCollection.ItemInserted -= transformGroup.ChildrenItemInserted;
				}
			}
			if (e.NewValueSource != BaseValueSourceInternal.Default || e.IsNewValueModified)
			{
				transformCollection2 = (TransformCollection)e.NewValue;
				if (transformCollection2 != null && !transformCollection2.IsFrozen)
				{
					transformCollection2.ItemInserted += transformGroup.ChildrenItemInserted;
					transformCollection2.ItemRemoved += transformGroup.ChildrenItemRemoved;
				}
			}
			if (transformCollection != transformCollection2 && transformGroup.Dispatcher != null)
			{
				using (CompositionEngineLock.Acquire())
				{
					DUCE.IResource resource = transformGroup;
					int channelCount = resource.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource.GetChannel(i);
						if (transformCollection2 != null)
						{
							int count = transformCollection2.Count;
							for (int j = 0; j < count; j++)
							{
								DUCE.IResource resource2 = transformCollection2.Internal_GetItem(j);
								resource2.AddRefOnChannel(channel);
							}
						}
						if (transformCollection != null)
						{
							int count2 = transformCollection.Count;
							for (int k = 0; k < count2; k++)
							{
								DUCE.IResource resource3 = transformCollection.Internal_GetItem(k);
								resource3.ReleaseOnChannel(channel);
							}
						}
					}
				}
			}
			transformGroup.PropertyChanged(TransformGroup.ChildrenProperty);
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.TransformCollection" /> que define este <see cref="T:System.Windows.Media.TransformGroup" />.</summary>
		/// <returns>Uma coleção de <see cref="T:System.Windows.Media.Transform" /> objetos que definem este <see cref="T:System.Windows.Media.TransformGroup" />. O padrão é uma coleção vazia.</returns>
		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x0600287A RID: 10362 RVA: 0x000A2654 File Offset: 0x000A1A54
		// (set) Token: 0x0600287B RID: 10363 RVA: 0x000A2674 File Offset: 0x000A1A74
		public TransformCollection Children
		{
			get
			{
				return (TransformCollection)base.GetValue(TransformGroup.ChildrenProperty);
			}
			set
			{
				base.SetValueInternal(TransformGroup.ChildrenProperty, value);
			}
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x000A2690 File Offset: 0x000A1A90
		protected override Freezable CreateInstanceCore()
		{
			return new TransformGroup();
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x000A26A4 File Offset: 0x000A1AA4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				TransformCollection children = this.Children;
				int num = (children == null) ? 0 : children.Count;
				DUCE.MILCMD_TRANSFORMGROUP milcmd_TRANSFORMGROUP;
				milcmd_TRANSFORMGROUP.Type = MILCMD.MilCmdTransformGroup;
				milcmd_TRANSFORMGROUP.Handle = this._duceResource.GetHandle(channel);
				milcmd_TRANSFORMGROUP.ChildrenSize = (uint)(sizeof(DUCE.ResourceHandle) * num);
				channel.BeginCommand((byte*)(&milcmd_TRANSFORMGROUP), sizeof(DUCE.MILCMD_TRANSFORMGROUP), (int)milcmd_TRANSFORMGROUP.ChildrenSize);
				for (int i = 0; i < num; i++)
				{
					DUCE.ResourceHandle handle = ((DUCE.IResource)children.Internal_GetItem(i)).GetHandle(channel);
					channel.AppendCommandData((byte*)(&handle), sizeof(DUCE.ResourceHandle));
				}
				channel.EndCommand();
			}
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x000A2754 File Offset: 0x000A1B54
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_TRANSFORMGROUP))
			{
				TransformCollection children = this.Children;
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

		// Token: 0x0600287F RID: 10367 RVA: 0x000A27B8 File Offset: 0x000A1BB8
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				TransformCollection children = this.Children;
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

		// Token: 0x06002880 RID: 10368 RVA: 0x000A2804 File Offset: 0x000A1C04
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x000A2820 File Offset: 0x000A1C20
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x000A2838 File Offset: 0x000A1C38
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x000A2854 File Offset: 0x000A1C54
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

		// Token: 0x06002884 RID: 10372 RVA: 0x000A28D8 File Offset: 0x000A1CD8
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

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06002885 RID: 10373 RVA: 0x000A2958 File Offset: 0x000A1D58
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x000A2968 File Offset: 0x000A1D68
		static TransformGroup()
		{
			Type typeFromHandle = typeof(TransformGroup);
			TransformGroup.ChildrenProperty = Animatable.RegisterProperty("Children", typeof(TransformCollection), typeFromHandle, new FreezableDefaultValueFactory(TransformCollection.Empty), new PropertyChangedCallback(TransformGroup.ChildrenPropertyChanged), null, false, null);
		}

		/// <summary>Obtém a estrutura <see cref="T:System.Windows.Media.Matrix" /> que descreve a transformação representada por este <see cref="T:System.Windows.Media.TransformGroup" />.</summary>
		/// <returns>Uma composição do <see cref="T:System.Windows.Media.Transform" /> objetos nesta <see cref="T:System.Windows.Media.TransformGroup" />.</returns>
		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06002888 RID: 10376 RVA: 0x000A29D4 File Offset: 0x000A1DD4
		public override Matrix Value
		{
			get
			{
				base.ReadPreamble();
				TransformCollection children = this.Children;
				if (children == null || children.Count == 0)
				{
					return default(Matrix);
				}
				Matrix matrix = children.Internal_GetItem(0).Value;
				for (int i = 1; i < children.Count; i++)
				{
					matrix *= children.Internal_GetItem(i).Value;
				}
				return matrix;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06002889 RID: 10377 RVA: 0x000A2A38 File Offset: 0x000A1E38
		internal override bool IsIdentity
		{
			get
			{
				TransformCollection children = this.Children;
				if (children == null || children.Count == 0)
				{
					return true;
				}
				for (int i = 0; i < children.Count; i++)
				{
					if (!children.Internal_GetItem(i).IsIdentity)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x000A2A7C File Offset: 0x000A1E7C
		internal override bool CanSerializeToString()
		{
			return false;
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.TransformGroup.Children" />.</summary>
		// Token: 0x04001298 RID: 4760
		public static readonly DependencyProperty ChildrenProperty;

		// Token: 0x04001299 RID: 4761
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x0400129A RID: 4762
		internal static TransformCollection s_Children = TransformCollection.Empty;
	}
}
