using System;
using System.Security;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal;

namespace System.Windows.Media.Media3D
{
	/// <summary>Representa um <see cref="T:System.Windows.Media.Media3D.Material" /> que é uma composição de Materiais em sua coleção.</summary>
	// Token: 0x02000462 RID: 1122
	[ContentProperty("Children")]
	public sealed class MaterialGroup : Material
	{
		/// <summary>Retorna uma cópia modificável do <see cref="T:System.Windows.Media.Media3D.MaterialGroup" />.</summary>
		/// <returns>Uma cópia modificável do <see cref="T:System.Windows.Media.Media3D.MaterialGroup" />. A cópia retornada é efetivamente um clone em profundidade do objeto atual, embora algumas cópias possam ser adiadas pelo período necessário para melhorar o desempenho. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da cópia é <see langword="false" />.</returns>
		// Token: 0x06002EC7 RID: 11975 RVA: 0x000BA188 File Offset: 0x000B9588
		public new MaterialGroup Clone()
		{
			return (MaterialGroup)base.Clone();
		}

		/// <summary>Retorna uma versão não animada do <see cref="T:System.Windows.Media.Media3D.MaterialGroup" />. O <see cref="T:System.Windows.Media.Media3D.MaterialGroup" /> retornado representa o estado do objeto atual no momento em que esse método foi chamado.</summary>
		/// <returns>Retorna o valor atual de <see cref="T:System.Windows.Media.Media3D.MaterialGroup" />. O <see cref="T:System.Windows.Media.Media3D.MaterialGroup" /> retornado tem o mesmo valor que o valor instantâneo do objeto atual, mas não é animado.</returns>
		// Token: 0x06002EC8 RID: 11976 RVA: 0x000BA1A0 File Offset: 0x000B95A0
		public new MaterialGroup CloneCurrentValue()
		{
			return (MaterialGroup)base.CloneCurrentValue();
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x000BA1B8 File Offset: 0x000B95B8
		private static void ChildrenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			MaterialGroup materialGroup = (MaterialGroup)d;
			MaterialCollection materialCollection = null;
			MaterialCollection materialCollection2 = null;
			if (e.OldValueSource != BaseValueSourceInternal.Default || e.IsOldValueModified)
			{
				materialCollection = (MaterialCollection)e.OldValue;
				if (materialCollection != null && !materialCollection.IsFrozen)
				{
					materialCollection.ItemRemoved -= materialGroup.ChildrenItemRemoved;
					materialCollection.ItemInserted -= materialGroup.ChildrenItemInserted;
				}
			}
			if (e.NewValueSource != BaseValueSourceInternal.Default || e.IsNewValueModified)
			{
				materialCollection2 = (MaterialCollection)e.NewValue;
				if (materialCollection2 != null && !materialCollection2.IsFrozen)
				{
					materialCollection2.ItemInserted += materialGroup.ChildrenItemInserted;
					materialCollection2.ItemRemoved += materialGroup.ChildrenItemRemoved;
				}
			}
			if (materialCollection != materialCollection2 && materialGroup.Dispatcher != null)
			{
				using (CompositionEngineLock.Acquire())
				{
					DUCE.IResource resource = materialGroup;
					int channelCount = resource.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource.GetChannel(i);
						if (materialCollection2 != null)
						{
							int count = materialCollection2.Count;
							for (int j = 0; j < count; j++)
							{
								DUCE.IResource resource2 = materialCollection2.Internal_GetItem(j);
								resource2.AddRefOnChannel(channel);
							}
						}
						if (materialCollection != null)
						{
							int count2 = materialCollection.Count;
							for (int k = 0; k < count2; k++)
							{
								DUCE.IResource resource3 = materialCollection.Internal_GetItem(k);
								resource3.ReleaseOnChannel(channel);
							}
						}
					}
				}
			}
			materialGroup.PropertyChanged(MaterialGroup.ChildrenProperty);
		}

		/// <summary>Obtém ou define uma coleção de objetos <see cref="T:System.Windows.Media.Media3D.Material" /> filho.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Media3D.MaterialCollection" /> que contém o filho <see cref="T:System.Windows.Media.Media3D.Material" /> objetos.</returns>
		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06002ECA RID: 11978 RVA: 0x000BA35C File Offset: 0x000B975C
		// (set) Token: 0x06002ECB RID: 11979 RVA: 0x000BA37C File Offset: 0x000B977C
		public MaterialCollection Children
		{
			get
			{
				return (MaterialCollection)base.GetValue(MaterialGroup.ChildrenProperty);
			}
			set
			{
				base.SetValueInternal(MaterialGroup.ChildrenProperty, value);
			}
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x000BA398 File Offset: 0x000B9798
		protected override Freezable CreateInstanceCore()
		{
			return new MaterialGroup();
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x000BA3AC File Offset: 0x000B97AC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				MaterialCollection children = this.Children;
				int num = (children == null) ? 0 : children.Count;
				DUCE.MILCMD_MATERIALGROUP milcmd_MATERIALGROUP;
				milcmd_MATERIALGROUP.Type = MILCMD.MilCmdMaterialGroup;
				milcmd_MATERIALGROUP.Handle = this._duceResource.GetHandle(channel);
				milcmd_MATERIALGROUP.ChildrenSize = (uint)(sizeof(DUCE.ResourceHandle) * num);
				channel.BeginCommand((byte*)(&milcmd_MATERIALGROUP), sizeof(DUCE.MILCMD_MATERIALGROUP), (int)milcmd_MATERIALGROUP.ChildrenSize);
				for (int i = 0; i < num; i++)
				{
					DUCE.ResourceHandle handle = ((DUCE.IResource)children.Internal_GetItem(i)).GetHandle(channel);
					channel.AppendCommandData((byte*)(&handle), sizeof(DUCE.ResourceHandle));
				}
				channel.EndCommand();
			}
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x000BA45C File Offset: 0x000B985C
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_MATERIALGROUP))
			{
				MaterialCollection children = this.Children;
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

		// Token: 0x06002ECF RID: 11983 RVA: 0x000BA4C0 File Offset: 0x000B98C0
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				MaterialCollection children = this.Children;
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

		// Token: 0x06002ED0 RID: 11984 RVA: 0x000BA50C File Offset: 0x000B990C
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x000BA528 File Offset: 0x000B9928
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x000BA540 File Offset: 0x000B9940
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x000BA55C File Offset: 0x000B995C
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

		// Token: 0x06002ED4 RID: 11988 RVA: 0x000BA5E0 File Offset: 0x000B99E0
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

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06002ED5 RID: 11989 RVA: 0x000BA660 File Offset: 0x000B9A60
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000BA670 File Offset: 0x000B9A70
		static MaterialGroup()
		{
			Type typeFromHandle = typeof(MaterialGroup);
			MaterialGroup.ChildrenProperty = Animatable.RegisterProperty("Children", typeof(MaterialCollection), typeFromHandle, new FreezableDefaultValueFactory(MaterialCollection.Empty), new PropertyChangedCallback(MaterialGroup.ChildrenPropertyChanged), null, false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.MaterialGroup.Children" />.</summary>
		// Token: 0x0400150B RID: 5387
		public static readonly DependencyProperty ChildrenProperty;

		// Token: 0x0400150C RID: 5388
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x0400150D RID: 5389
		internal static MaterialCollection s_Children = MaterialCollection.Empty;
	}
}
