using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Permissions;
using System.Windows.Media.Composition;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que fornece suporte a animação.</summary>
	// Token: 0x0200049F RID: 1183
	public abstract class Animatable : Freezable, IAnimatable, DUCE.IResource
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.Animatable" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência desse objeto, esse método copia associações de dados e referências de recurso (mas talvez eles não possam mais se resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável dessa instância. O clone retornado é efetivamente uma cópia profunda do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do clone é <see langword="false" />.</returns>
		// Token: 0x06003465 RID: 13413 RVA: 0x000CFBE0 File Offset: 0x000CEFE0
		public new Animatable Clone()
		{
			return (Animatable)base.Clone();
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x000CFBF8 File Offset: 0x000CEFF8
		internal void PropertyChanged(DependencyProperty dp)
		{
			AnimationStorage storage = AnimationStorage.GetStorage(this, dp);
			IndependentAnimationStorage independentAnimationStorage = storage as IndependentAnimationStorage;
			if (independentAnimationStorage != null)
			{
				independentAnimationStorage.InvalidateResource();
				return;
			}
			this.RegisterForAsyncUpdateResource();
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x000CFC24 File Offset: 0x000CF024
		internal virtual void AddRefOnChannelAnimations(DUCE.Channel channel)
		{
			if (base.IAnimatable_HasAnimatedProperties)
			{
				FrugalMap animatedPropertiesMap = AnimationStorage.GetAnimatedPropertiesMap(this);
				for (int i = 0; i < animatedPropertiesMap.Count; i++)
				{
					int num;
					object obj;
					animatedPropertiesMap.GetKeyValuePair(i, out num, out obj);
					DUCE.IResource resource = obj as DUCE.IResource;
					if (resource != null)
					{
						resource.AddRefOnChannel(channel);
					}
				}
			}
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x000CFC74 File Offset: 0x000CF074
		internal virtual void ReleaseOnChannelAnimations(DUCE.Channel channel)
		{
			if (base.IAnimatable_HasAnimatedProperties)
			{
				FrugalMap animatedPropertiesMap = AnimationStorage.GetAnimatedPropertiesMap(this);
				for (int i = 0; i < animatedPropertiesMap.Count; i++)
				{
					int num;
					object obj;
					animatedPropertiesMap.GetKeyValuePair(i, out num, out obj);
					DUCE.IResource resource = obj as DUCE.IResource;
					if (resource != null)
					{
						resource.ReleaseOnChannel(channel);
					}
				}
			}
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x000CFCC0 File Offset: 0x000CF0C0
		internal static DependencyProperty RegisterProperty(string name, Type propertyType, Type ownerType, object defaultValue, PropertyChangedCallback changed, ValidateValueCallback validate, bool isIndependentlyAnimated, CoerceValueCallback coerced)
		{
			UIPropertyMetadata uipropertyMetadata;
			if (isIndependentlyAnimated)
			{
				uipropertyMetadata = new IndependentlyAnimatedPropertyMetadata(defaultValue);
			}
			else
			{
				uipropertyMetadata = new UIPropertyMetadata(defaultValue);
			}
			uipropertyMetadata.PropertyChangedCallback = changed;
			if (coerced != null)
			{
				uipropertyMetadata.CoerceValueCallback = coerced;
			}
			return DependencyProperty.Register(name, propertyType, ownerType, uipropertyMetadata, validate);
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x000CFD04 File Offset: 0x000CF104
		internal void AddRefResource(DUCE.IResource resource, DUCE.Channel channel)
		{
			if (resource != null)
			{
				resource.AddRefOnChannel(channel);
			}
		}

		// Token: 0x0600346B RID: 13419 RVA: 0x000CFD1C File Offset: 0x000CF11C
		internal void ReleaseResource(DUCE.IResource resource, DUCE.Channel channel)
		{
			if (resource != null)
			{
				resource.ReleaseOnChannel(channel);
			}
		}

		/// <summary>Faz com que este objeto <see cref="T:System.Windows.Media.Animation.Animatable" /> não seja modificável ou determina se ele pode se tornar não modificável.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> se este método deve simplesmente determinar se esta instância pode ser congelada. <see langword="false" /> se esta instância, na verdade, deve congelar a si mesma quando este método é chamado.</param>
		/// <returns>Se <paramref name="isChecking" /> for <see langword="true" />, esse método retorna <see langword="true" /> se este <see cref="T:System.Windows.Media.Animation.Animatable" /> puder se tornar não modificável ou <see langword="false" />, se ele não puder se tornar não modificável.  
		/// Se <paramref name="isChecking" /> for <see langword="false" />, este método retorna <see langword="true" /> se esse <see cref="T:System.Windows.Media.Animation.Animatable" /> agora for não modificável ou <see langword="false" />, se não puder se tornar não modificável, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x0600346C RID: 13420 RVA: 0x000CFD34 File Offset: 0x000CF134
		protected override bool FreezeCore(bool isChecking)
		{
			if (base.IAnimatable_HasAnimatedProperties)
			{
				if (TraceFreezable.IsEnabled)
				{
					TraceFreezable.Trace(TraceEventType.Warning, TraceFreezable.UnableToFreezeAnimatedProperties, this);
				}
				return false;
			}
			return base.FreezeCore(isChecking);
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x000CFD68 File Offset: 0x000CF168
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			return DUCE.ResourceHandle.Null;
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x000CFD7C File Offset: 0x000CF17C
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x000CFD8C File Offset: 0x000CF18C
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			return DUCE.ResourceHandle.Null;
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x000CFDA0 File Offset: 0x000CF1A0
		int DUCE.IResource.GetChannelCount()
		{
			return 0;
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x000CFDB0 File Offset: 0x000CF1B0
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return null;
		}

		// Token: 0x06003472 RID: 13426 RVA: 0x000CFDC0 File Offset: 0x000CF1C0
		DUCE.ResourceHandle DUCE.IResource.Get3DHandle(DUCE.Channel channel)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x000CFDD4 File Offset: 0x000CF1D4
		void DUCE.IResource.RemoveChildFromParent(DUCE.IResource parent, DUCE.Channel channel)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x000CFDE8 File Offset: 0x000CF1E8
		internal DUCE.ResourceHandle GetAnimationResourceHandle(DependencyProperty dp, DUCE.Channel channel)
		{
			if (channel != null && base.IAnimatable_HasAnimatedProperties)
			{
				return IndependentAnimationStorage.GetResourceHandle(this, dp, channel);
			}
			return DUCE.ResourceHandle.Null;
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x000CFE10 File Offset: 0x000CF210
		internal WeakReference GetWeakReference()
		{
			object obj = Animatable.StoredWeakReferenceField.GetValue(this);
			if (obj == null)
			{
				obj = new WeakReference(this);
				Animatable.StoredWeakReferenceField.SetValue(this, (WeakReference)obj);
			}
			return (WeakReference)obj;
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x000CFE4C File Offset: 0x000CF24C
		internal bool IsBaseValueDefault(DependencyProperty dp)
		{
			return base.ReadLocalValue(dp) == DependencyProperty.UnsetValue;
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x000CFE68 File Offset: 0x000CF268
		internal void RegisterForAsyncUpdateResource()
		{
			if (this != null && base.Dispatcher != null && base.Animatable_IsResourceInvalidationNecessary)
			{
				MediaContext mediaContext = MediaContext.From(base.Dispatcher);
				if (!((DUCE.IResource)this).GetHandle(mediaContext.Channel).IsNull)
				{
					mediaContext.ResourcesUpdated += this.UpdateResource;
					base.Animatable_IsResourceInvalidationNecessary = false;
				}
			}
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x000CFEC8 File Offset: 0x000CF2C8
		internal virtual void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			base.Animatable_IsResourceInvalidationNecessary = true;
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x000CFEDC File Offset: 0x000CF2DC
		internal void InternalWritePreamble()
		{
			base.WritePreamble();
		}

		/// <summary>Especifica se um objeto de dependência deve ser serializado.</summary>
		/// <param name="target">Representa um objeto que participa do sistema de propriedade de dependência.</param>
		/// <returns>
		///   <see langword="true" /> para serializar <paramref name="target" />; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x0600347A RID: 13434 RVA: 0x000CFEF0 File Offset: 0x000CF2F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool ShouldSerializeStoredWeakReference(DependencyObject target)
		{
			return false;
		}

		/// <summary>Aplica um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> ao <see cref="T:System.Windows.DependencyProperty" /> especificado. Se a propriedade já tiver sido animada, o comportamento de entrega de <see cref="F:System.Windows.Media.Animation.HandoffBehavior.SnapshotAndReplace" /> será usado.</summary>
		/// <param name="dp">A propriedade a ser animada.</param>
		/// <param name="clock">O relógio com o qual animar a propriedade especificada. Se <paramref name="clock" /> for <see langword="null" />, todas as animações serão removidas da propriedade especificada (mas não paradas).</param>
		// Token: 0x0600347B RID: 13435 RVA: 0x000CFF00 File Offset: 0x000CF300
		public void ApplyAnimationClock(DependencyProperty dp, AnimationClock clock)
		{
			this.ApplyAnimationClock(dp, clock, HandoffBehavior.SnapshotAndReplace);
		}

		/// <summary>Aplica um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> ao <see cref="T:System.Windows.DependencyProperty" /> especificado. Se a propriedade já for animada, o <see cref="T:System.Windows.Media.Animation.HandoffBehavior" /> especificado será usado.</summary>
		/// <param name="dp">A propriedade a ser animada.</param>
		/// <param name="clock">O relógio com o qual animar a propriedade especificada. Se <paramref name="handoffBehavior" /> for <see cref="F:System.Windows.Media.Animation.HandoffBehavior.SnapshotAndReplace" /> e <paramref name="clock" /> for <see langword="null" />, todas as animações serão removidas da propriedade especificada (mas não paradas). Se <paramref name="handoffBehavior" /> for <see cref="F:System.Windows.Media.Animation.HandoffBehavior.Compose" /> e o relógio for <see langword="null" />, esse método não terá nenhum efeito.</param>
		/// <param name="handoffBehavior">Um valor que especifica como a nova animação deve interagir com qualquer animação atual que já afete o valor da propriedade.</param>
		// Token: 0x0600347C RID: 13436 RVA: 0x000CFF18 File Offset: 0x000CF318
		public void ApplyAnimationClock(DependencyProperty dp, AnimationClock clock, HandoffBehavior handoffBehavior)
		{
			if (dp == null)
			{
				throw new ArgumentNullException("dp");
			}
			if (!AnimationStorage.IsPropertyAnimatable(this, dp))
			{
				throw new ArgumentException(SR.Get("Animation_DependencyPropertyIsNotAnimatable", new object[]
				{
					dp.Name,
					base.GetType()
				}), "dp");
			}
			if (clock != null && !AnimationStorage.IsAnimationValid(dp, clock.Timeline))
			{
				throw new ArgumentException(SR.Get("Animation_AnimationTimelineTypeMismatch", new object[]
				{
					clock.Timeline.GetType(),
					dp.Name,
					dp.PropertyType
				}), "clock");
			}
			if (!HandoffBehaviorEnum.IsDefined(handoffBehavior))
			{
				throw new ArgumentException(SR.Get("Animation_UnrecognizedHandoffBehavior"));
			}
			if (base.IsSealed)
			{
				throw new InvalidOperationException(SR.Get("IAnimatable_CantAnimateSealedDO", new object[]
				{
					dp,
					base.GetType()
				}));
			}
			AnimationStorage.ApplyAnimationClock(this, dp, clock, handoffBehavior);
		}

		/// <summary>Aplica uma animação ao <see cref="T:System.Windows.DependencyProperty" /> especificado. A animação é iniciada quando o próximo quadro for renderizado. Se a propriedade especificada já tiver sido animada, o comportamento de entrega de <see cref="F:System.Windows.Media.Animation.HandoffBehavior.SnapshotAndReplace" /> será usado.</summary>
		/// <param name="dp">A propriedade a ser animada.</param>
		/// <param name="animation">A animação usada para animar a propriedade especificada.  
		/// Se o <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" /> da animação for <see langword="null" />, todas as animações atuais serão removidas e o valor atual da propriedade será mantido.  
		/// Se <paramref name="animation" /> for <see langword="null" />, todas as animações serão removidas da propriedade e o valor da propriedade será revertido para seu valor de base.</param>
		// Token: 0x0600347D RID: 13437 RVA: 0x000D0004 File Offset: 0x000CF404
		public void BeginAnimation(DependencyProperty dp, AnimationTimeline animation)
		{
			this.BeginAnimation(dp, animation, HandoffBehavior.SnapshotAndReplace);
		}

		/// <summary>Aplica uma animação ao <see cref="T:System.Windows.DependencyProperty" /> especificado. A animação é iniciada quando o próximo quadro for renderizado. Se a propriedade especifica já for animada, o <see cref="T:System.Windows.Media.Animation.HandoffBehavior" /> especificado será usado.</summary>
		/// <param name="dp">A propriedade a ser animada.</param>
		/// <param name="animation">A animação usada para animar a propriedade especificada.  
		/// Se <paramref name="handoffBehavior" /> for <see cref="F:System.Windows.Media.Animation.HandoffBehavior.SnapshotAndReplace" /> e o <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" /> da animação for <see langword="null" />, todas as animações atuais serão removidas e o valor atual da propriedade será mantido.  
		/// Se <paramref name="handoffBehavior" /> for <see cref="F:System.Windows.Media.Animation.HandoffBehavior.SnapshotAndReplace" /> e <paramref name="animation" /> for uma referência de <see langword="null" />, todas as animações serão removidas da propriedade e o valor da propriedade será revertido para seu valor de base.  
		/// Se <paramref name="handoffBehavior" /> for <see cref="F:System.Windows.Media.Animation.HandoffBehavior.Compose" />, esse método não terá efeito se a animação ou seu <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" /> for <see langword="null" />.</param>
		/// <param name="handoffBehavior">Um valor que especifica como a nova animação deve interagir com qualquer animação atual que já afete o valor da propriedade.</param>
		// Token: 0x0600347E RID: 13438 RVA: 0x000D001C File Offset: 0x000CF41C
		public void BeginAnimation(DependencyProperty dp, AnimationTimeline animation, HandoffBehavior handoffBehavior)
		{
			if (dp == null)
			{
				throw new ArgumentNullException("dp");
			}
			if (!AnimationStorage.IsPropertyAnimatable(this, dp))
			{
				throw new ArgumentException(SR.Get("Animation_DependencyPropertyIsNotAnimatable", new object[]
				{
					dp.Name,
					base.GetType()
				}), "dp");
			}
			if (animation != null && !AnimationStorage.IsAnimationValid(dp, animation))
			{
				throw new ArgumentException(SR.Get("Animation_AnimationTimelineTypeMismatch", new object[]
				{
					animation.GetType(),
					dp.Name,
					dp.PropertyType
				}), "animation");
			}
			if (!HandoffBehaviorEnum.IsDefined(handoffBehavior))
			{
				throw new ArgumentException(SR.Get("Animation_UnrecognizedHandoffBehavior"));
			}
			if (base.IsSealed)
			{
				throw new InvalidOperationException(SR.Get("IAnimatable_CantAnimateSealedDO", new object[]
				{
					dp,
					base.GetType()
				}));
			}
			AnimationStorage.BeginAnimation(this, dp, animation, handoffBehavior);
		}

		/// <summary>Obtém um valor que indica se um ou mais objetos <see cref="T:System.Windows.Media.Animation.AnimationClock" /> está associado a qualquer uma das propriedades de dependência do objeto.</summary>
		/// <returns>
		///   <see langword="true" /> Se um ou mais <see cref="T:System.Windows.Media.Animation.AnimationClock" /> objetos estiver associado a qualquer uma das propriedades de dependência desse objeto; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x0600347F RID: 13439 RVA: 0x000D00FC File Offset: 0x000CF4FC
		public bool HasAnimatedProperties
		{
			get
			{
				base.VerifyAccess();
				return base.IAnimatable_HasAnimatedProperties;
			}
		}

		/// <summary>Retorna o valor não animado do <see cref="T:System.Windows.DependencyProperty" /> especificado.</summary>
		/// <param name="dp">Identifica a propriedade cujo valor base (não animado) deve ser recuperado.</param>
		/// <returns>O valor que será retornado se a propriedade especificada não for animada.</returns>
		// Token: 0x06003480 RID: 13440 RVA: 0x000D0118 File Offset: 0x000CF518
		public object GetAnimationBaseValue(DependencyProperty dp)
		{
			if (dp == null)
			{
				throw new ArgumentNullException("dp");
			}
			return base.GetValueEntry(base.LookupEntry(dp.GlobalIndex), dp, null, RequestFlags.AnimationBaseValue).Value;
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x000D0150 File Offset: 0x000CF550
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		internal sealed override void EvaluateAnimatedValueCore(DependencyProperty dp, PropertyMetadata metadata, ref EffectiveValueEntry entry)
		{
			if (base.IAnimatable_HasAnimatedProperties)
			{
				AnimationStorage storage = AnimationStorage.GetStorage(this, dp);
				if (storage != null)
				{
					storage.EvaluateAnimatedValue(metadata, ref entry);
				}
			}
		}

		// Token: 0x04001600 RID: 5632
		private static readonly UncommonField<WeakReference> StoredWeakReferenceField = new UncommonField<WeakReference>();
	}
}
