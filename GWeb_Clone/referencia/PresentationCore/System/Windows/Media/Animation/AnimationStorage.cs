using System;
using System.Collections.Generic;
using System.Windows.Media.Composition;
using System.Windows.Media.Media3D;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media.Animation
{
	// Token: 0x020004A4 RID: 1188
	internal class AnimationStorage
	{
		// Token: 0x060034A4 RID: 13476 RVA: 0x000D08A8 File Offset: 0x000CFCA8
		protected AnimationStorage()
		{
			this._currentTimeInvalidatedHandler = new EventHandler(this.OnCurrentTimeInvalidated);
			this._removeRequestedHandler = new EventHandler(this.OnRemoveRequested);
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x060034A5 RID: 13477 RVA: 0x000D08F8 File Offset: 0x000CFCF8
		internal bool IsEmpty
		{
			get
			{
				return this._animationClocks == null && this._propertyTriggerLayers == null && this._snapshotValue == DependencyProperty.UnsetValue;
			}
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000D0924 File Offset: 0x000CFD24
		internal void AttachAnimationClock(AnimationClock animationClock, EventHandler removeRequestedHandler)
		{
			animationClock.CurrentTimeInvalidated += this._currentTimeInvalidatedHandler;
			if (animationClock.HasControllableRoot)
			{
				animationClock.RemoveRequested += removeRequestedHandler;
			}
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000D094C File Offset: 0x000CFD4C
		internal void DetachAnimationClock(AnimationClock animationClock, EventHandler removeRequestedHandler)
		{
			animationClock.CurrentTimeInvalidated -= this._currentTimeInvalidatedHandler;
			if (animationClock.HasControllableRoot)
			{
				animationClock.RemoveRequested -= removeRequestedHandler;
			}
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x000D0974 File Offset: 0x000CFD74
		internal void Initialize(DependencyObject d, DependencyProperty dp)
		{
			Animatable animatable = d as Animatable;
			if (animatable != null)
			{
				this._dependencyObject = animatable.GetWeakReference();
			}
			else
			{
				this._dependencyObject = new WeakReference(d);
			}
			this._dependencyProperty = dp;
		}

		// Token: 0x060034A9 RID: 13481 RVA: 0x000D09AC File Offset: 0x000CFDAC
		internal void RemoveLayer(AnimationLayer layer)
		{
			int index = this._propertyTriggerLayers.IndexOfValue(layer);
			this._propertyTriggerLayers.RemoveAt(index);
			if (this._propertyTriggerLayers.Count == 0)
			{
				this._propertyTriggerLayers = null;
			}
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x000D09E8 File Offset: 0x000CFDE8
		internal void WritePostscript()
		{
			DependencyObject dependencyObject = (DependencyObject)this._dependencyObject.Target;
			if (dependencyObject == null)
			{
				return;
			}
			FrugalMap value = AnimationStorage.AnimatedPropertyMapField.GetValue(dependencyObject);
			if (value.Count == 0 || value[this._dependencyProperty.GlobalIndex] == DependencyProperty.UnsetValue)
			{
				if (this.IsEmpty)
				{
					goto IL_1F1;
				}
				value[this._dependencyProperty.GlobalIndex] = this;
				AnimationStorage.AnimatedPropertyMapField.SetValue(dependencyObject, value);
				if (value.Count == 1)
				{
					dependencyObject.IAnimatable_HasAnimatedProperties = true;
				}
				Animatable animatable = dependencyObject as Animatable;
				if (animatable != null)
				{
					animatable.RegisterForAsyncUpdateResource();
				}
				DUCE.IResource resource = this as DUCE.IResource;
				if (resource == null)
				{
					goto IL_1F1;
				}
				DUCE.IResource resource2 = dependencyObject as DUCE.IResource;
				if (resource2 == null)
				{
					goto IL_1F1;
				}
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource2.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource2.GetChannel(i);
						if (!resource2.GetHandle(channel).IsNull)
						{
							resource.AddRefOnChannel(channel);
						}
					}
					goto IL_1F1;
				}
			}
			if (this.IsEmpty)
			{
				DUCE.IResource resource3 = this as DUCE.IResource;
				if (resource3 != null)
				{
					DUCE.IResource resource4 = dependencyObject as DUCE.IResource;
					if (resource4 != null)
					{
						using (CompositionEngineLock.Acquire())
						{
							int channelCount2 = resource4.GetChannelCount();
							for (int j = 0; j < channelCount2; j++)
							{
								DUCE.Channel channel2 = resource4.GetChannel(j);
								if (!resource4.GetHandle(channel2).IsNull)
								{
									resource3.ReleaseOnChannel(channel2);
								}
							}
						}
					}
				}
				Animatable animatable2 = dependencyObject as Animatable;
				if (animatable2 != null)
				{
					animatable2.RegisterForAsyncUpdateResource();
				}
				value[this._dependencyProperty.GlobalIndex] = DependencyProperty.UnsetValue;
				if (value.Count == 0)
				{
					AnimationStorage.AnimatedPropertyMapField.ClearValue(dependencyObject);
					dependencyObject.IAnimatable_HasAnimatedProperties = false;
				}
				else
				{
					AnimationStorage.AnimatedPropertyMapField.SetValue(dependencyObject, value);
				}
				if (this._baseValue != DependencyProperty.UnsetValue)
				{
					dependencyObject.SetValue(this._dependencyProperty, this._baseValue);
				}
			}
			IL_1F1:
			dependencyObject.InvalidateProperty(this._dependencyProperty);
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x000D0C28 File Offset: 0x000D0028
		internal void EvaluateAnimatedValue(PropertyMetadata metadata, ref EffectiveValueEntry entry)
		{
			DependencyObject dependencyObject = (DependencyObject)this._dependencyObject.Target;
			if (dependencyObject == null)
			{
				return;
			}
			object value = entry.GetFlattenedEntry(RequestFlags.FullyResolved).Value;
			if (entry.IsDeferredReference)
			{
				DeferredReference deferredReference = (DeferredReference)value;
				value = deferredReference.GetValue(entry.BaseValueSourceInternal);
				entry.SetAnimationBaseValue(value);
			}
			object currentPropertyValue = AnimationStorage.GetCurrentPropertyValue(this, dependencyObject, this._dependencyProperty, metadata, value);
			if (!this._dependencyProperty.IsValidValueInternal(currentPropertyValue))
			{
				string id = "Animation_CalculatedValueIsInvalidForProperty";
				object[] array = new object[2];
				array[0] = this._dependencyProperty.Name;
				throw new InvalidOperationException(SR.Get(id, array));
			}
			entry.SetAnimatedValue(currentPropertyValue, value);
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x000D0CC8 File Offset: 0x000D00C8
		private void OnCurrentTimeInvalidated(object sender, EventArgs args)
		{
			object target = this._dependencyObject.Target;
			if (target == null)
			{
				this.DetachAnimationClock((AnimationClock)sender, this._removeRequestedHandler);
				return;
			}
			try
			{
				DependencyObject dependencyObject = (DependencyObject)target;
				EffectiveValueEntry valueEntry = dependencyObject.GetValueEntry(dependencyObject.LookupEntry(this._dependencyProperty.GlobalIndex), this._dependencyProperty, null, RequestFlags.RawEntry);
				EffectiveValueEntry effectiveValueEntry;
				object obj;
				if (!valueEntry.HasModifiers)
				{
					effectiveValueEntry = valueEntry;
					obj = effectiveValueEntry.Value;
					if (effectiveValueEntry.IsDeferredReference)
					{
						obj = ((DeferredReference)obj).GetValue(effectiveValueEntry.BaseValueSourceInternal);
						effectiveValueEntry.Value = obj;
					}
				}
				else
				{
					effectiveValueEntry = default(EffectiveValueEntry);
					effectiveValueEntry.BaseValueSourceInternal = valueEntry.BaseValueSourceInternal;
					effectiveValueEntry.PropertyIndex = valueEntry.PropertyIndex;
					effectiveValueEntry.HasExpressionMarker = valueEntry.HasExpressionMarker;
					obj = valueEntry.ModifiedValue.BaseValue;
					if (valueEntry.IsDeferredReference)
					{
						DeferredReference deferredReference = obj as DeferredReference;
						if (deferredReference != null)
						{
							obj = deferredReference.GetValue(effectiveValueEntry.BaseValueSourceInternal);
						}
					}
					effectiveValueEntry.Value = obj;
					if (valueEntry.IsExpression)
					{
						obj = valueEntry.ModifiedValue.ExpressionValue;
						if (valueEntry.IsDeferredReference)
						{
							DeferredReference deferredReference2 = obj as DeferredReference;
							if (deferredReference2 != null)
							{
								obj = deferredReference2.GetValue(effectiveValueEntry.BaseValueSourceInternal);
							}
						}
						effectiveValueEntry.SetExpressionValue(obj, effectiveValueEntry.Value);
					}
				}
				PropertyMetadata metadata = this._dependencyProperty.GetMetadata(dependencyObject.DependencyObjectType);
				object currentPropertyValue = AnimationStorage.GetCurrentPropertyValue(this, dependencyObject, this._dependencyProperty, metadata, obj);
				if (this._dependencyProperty.IsValidValueInternal(currentPropertyValue))
				{
					effectiveValueEntry.SetAnimatedValue(currentPropertyValue, obj);
					dependencyObject.UpdateEffectiveValue(dependencyObject.LookupEntry(this._dependencyProperty.GlobalIndex), this._dependencyProperty, metadata, valueEntry, ref effectiveValueEntry, false, false, OperationType.Unknown);
					if (this._hadValidationError && TraceAnimation.IsEnabled)
					{
						TraceAnimation.TraceActivityItem(TraceAnimation.AnimateStorageValidationNoLongerFailing, new object[]
						{
							this,
							currentPropertyValue,
							target,
							this._dependencyProperty
						});
						this._hadValidationError = false;
					}
				}
				else if (!this._hadValidationError)
				{
					if (TraceAnimation.IsEnabled)
					{
						TraceAnimation.TraceActivityItem(TraceAnimation.AnimateStorageValidationFailed, new object[]
						{
							this,
							currentPropertyValue,
							target,
							this._dependencyProperty
						});
					}
					this._hadValidationError = true;
				}
			}
			catch (Exception innerException)
			{
				throw new AnimationException((AnimationClock)sender, this._dependencyProperty, (IAnimatable)target, SR.Get("Animation_Exception", new object[]
				{
					this._dependencyProperty.Name,
					target.GetType().FullName,
					((AnimationClock)sender).Timeline.GetType().FullName
				}), innerException);
			}
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x000D0F70 File Offset: 0x000D0370
		private void OnRemoveRequested(object sender, EventArgs args)
		{
			AnimationClock animationClock = (AnimationClock)sender;
			int num = this._animationClocks.IndexOf(animationClock);
			this._animationClocks.RemoveAt(num);
			if (this._hasStickySnapshotValue && num == 0)
			{
				this._hasStickySnapshotValue = false;
				animationClock.CurrentStateInvalidated -= this.OnCurrentStateInvalidated;
			}
			if (this._animationClocks.Count == 0)
			{
				this._animationClocks = null;
				this._snapshotValue = DependencyProperty.UnsetValue;
			}
			this.DetachAnimationClock(animationClock, this._removeRequestedHandler);
			this.WritePostscript();
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x000D0FF4 File Offset: 0x000D03F4
		private void OnCurrentStateInvalidated(object sender, EventArgs args)
		{
			this._hasStickySnapshotValue = false;
			((AnimationClock)sender).CurrentStateInvalidated -= this.OnCurrentStateInvalidated;
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000D1020 File Offset: 0x000D0420
		private void ClearAnimations()
		{
			if (this._animationClocks != null)
			{
				for (int i = 0; i < this._animationClocks.Count; i++)
				{
					this.DetachAnimationClock(this._animationClocks[i], this._removeRequestedHandler);
				}
				this._animationClocks = null;
			}
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x000D106C File Offset: 0x000D046C
		internal static void ApplyAnimationClock(DependencyObject d, DependencyProperty dp, AnimationClock animationClock, HandoffBehavior handoffBehavior)
		{
			if (animationClock == null)
			{
				AnimationStorage.BeginAnimation(d, dp, null, handoffBehavior);
				return;
			}
			AnimationStorage.ApplyAnimationClocks(d, dp, new AnimationClock[]
			{
				animationClock
			}, handoffBehavior);
		}

		// Token: 0x060034B1 RID: 13489 RVA: 0x000D1098 File Offset: 0x000D0498
		[FriendAccessAllowed]
		internal static void ApplyAnimationClocks(DependencyObject d, DependencyProperty dp, IList<AnimationClock> animationClocks, HandoffBehavior handoffBehavior)
		{
			AnimationStorage animationStorage = AnimationStorage.GetStorage(d, dp);
			if (handoffBehavior == HandoffBehavior.SnapshotAndReplace || animationStorage == null || animationStorage._animationClocks == null)
			{
				if (animationStorage != null)
				{
					EventHandler value = new EventHandler(animationStorage.OnCurrentStateInvalidated);
					if (animationStorage._hasStickySnapshotValue)
					{
						animationStorage._animationClocks[0].CurrentStateInvalidated -= value;
					}
					else
					{
						animationStorage._snapshotValue = d.GetValue(dp);
					}
					if (animationClocks[0].CurrentState == ClockState.Stopped)
					{
						animationStorage._hasStickySnapshotValue = true;
						animationClocks[0].CurrentStateInvalidated += animationStorage.OnCurrentStateInvalidated;
					}
					else
					{
						animationStorage._hasStickySnapshotValue = false;
					}
					animationStorage.ClearAnimations();
				}
				else
				{
					animationStorage = AnimationStorage.CreateStorage(d, dp);
				}
				animationStorage._animationClocks = new FrugalObjectList<AnimationClock>(animationClocks.Count);
				for (int i = 0; i < animationClocks.Count; i++)
				{
					animationStorage._animationClocks.Add(animationClocks[i]);
					animationStorage.AttachAnimationClock(animationClocks[i], animationStorage._removeRequestedHandler);
				}
			}
			else
			{
				FrugalObjectList<AnimationClock> frugalObjectList = new FrugalObjectList<AnimationClock>(animationStorage._animationClocks.Count + animationClocks.Count);
				for (int j = 0; j < animationStorage._animationClocks.Count; j++)
				{
					frugalObjectList.Add(animationStorage._animationClocks[j]);
				}
				animationStorage._animationClocks = frugalObjectList;
				for (int k = 0; k < animationClocks.Count; k++)
				{
					frugalObjectList.Add(animationClocks[k]);
					animationStorage.AttachAnimationClock(animationClocks[k], animationStorage._removeRequestedHandler);
				}
			}
			animationStorage.WritePostscript();
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x000D1214 File Offset: 0x000D0614
		[FriendAccessAllowed]
		internal static void ApplyAnimationClocksToLayer(DependencyObject d, DependencyProperty dp, IList<AnimationClock> animationClocks, HandoffBehavior handoffBehavior, long propertyTriggerLayerIndex)
		{
			if (propertyTriggerLayerIndex == 1L)
			{
				AnimationStorage.ApplyAnimationClocks(d, dp, animationClocks, handoffBehavior);
				return;
			}
			AnimationStorage animationStorage = AnimationStorage.GetStorage(d, dp);
			if (animationStorage == null)
			{
				animationStorage = AnimationStorage.CreateStorage(d, dp);
			}
			SortedList<long, AnimationLayer> sortedList = animationStorage._propertyTriggerLayers;
			if (sortedList == null)
			{
				sortedList = new SortedList<long, AnimationLayer>(1);
				animationStorage._propertyTriggerLayers = sortedList;
			}
			AnimationLayer animationLayer;
			if (sortedList.ContainsKey(propertyTriggerLayerIndex))
			{
				animationLayer = sortedList[propertyTriggerLayerIndex];
			}
			else
			{
				animationLayer = new AnimationLayer(animationStorage);
				sortedList[propertyTriggerLayerIndex] = animationLayer;
			}
			object defaultDestinationValue = DependencyProperty.UnsetValue;
			if (handoffBehavior == HandoffBehavior.SnapshotAndReplace)
			{
				defaultDestinationValue = ((IAnimatable)d).GetAnimationBaseValue(dp);
				int count = sortedList.Count;
				if (count > 1)
				{
					IList<long> keys = sortedList.Keys;
					int num = 0;
					while (num < count && keys[num] < propertyTriggerLayerIndex)
					{
						AnimationLayer animationLayer2;
						sortedList.TryGetValue(keys[num], out animationLayer2);
						defaultDestinationValue = animationLayer2.GetCurrentValue(defaultDestinationValue);
						num++;
					}
				}
			}
			animationLayer.ApplyAnimationClocks(animationClocks, handoffBehavior, defaultDestinationValue);
			animationStorage.WritePostscript();
		}

		// Token: 0x060034B3 RID: 13491 RVA: 0x000D12F4 File Offset: 0x000D06F4
		internal static void BeginAnimation(DependencyObject d, DependencyProperty dp, AnimationTimeline animation, HandoffBehavior handoffBehavior)
		{
			AnimationStorage storage = AnimationStorage.GetStorage(d, dp);
			if (animation == null)
			{
				if (storage == null || handoffBehavior == HandoffBehavior.Compose)
				{
					return;
				}
				if (storage._hasStickySnapshotValue)
				{
					storage._hasStickySnapshotValue = false;
					storage._animationClocks[0].CurrentStateInvalidated -= storage.OnCurrentStateInvalidated;
				}
				storage._snapshotValue = DependencyProperty.UnsetValue;
				storage.ClearAnimations();
			}
			else
			{
				if (animation.BeginTime != null)
				{
					AnimationClock animationClock = animation.CreateClock();
					AnimationStorage.ApplyAnimationClocks(d, dp, new AnimationClock[]
					{
						animationClock
					}, handoffBehavior);
					return;
				}
				if (storage == null)
				{
					return;
				}
				if (handoffBehavior == HandoffBehavior.SnapshotAndReplace)
				{
					if (storage._hasStickySnapshotValue)
					{
						storage._hasStickySnapshotValue = false;
						storage._animationClocks[0].CurrentStateInvalidated -= storage.OnCurrentStateInvalidated;
					}
					else
					{
						storage._snapshotValue = d.GetValue(dp);
					}
					storage.ClearAnimations();
				}
			}
			storage.WritePostscript();
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x000D13CC File Offset: 0x000D07CC
		internal static AnimationStorage EnsureStorage(DependencyObject d, DependencyProperty dp)
		{
			object obj = AnimationStorage.AnimatedPropertyMapField.GetValue(d)[dp.GlobalIndex];
			if (obj == DependencyProperty.UnsetValue)
			{
				return AnimationStorage.CreateStorage(d, dp);
			}
			return (AnimationStorage)obj;
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x000D140C File Offset: 0x000D080C
		internal static object GetCurrentPropertyValue(AnimationStorage storage, DependencyObject d, DependencyProperty dp, PropertyMetadata metadata, object baseValue)
		{
			if (storage._hasStickySnapshotValue && storage._animationClocks[0].CurrentState == ClockState.Stopped)
			{
				return storage._snapshotValue;
			}
			if (storage._animationClocks == null && storage._propertyTriggerLayers == null)
			{
				return storage._snapshotValue;
			}
			object obj = baseValue;
			if (obj == DependencyProperty.UnsetValue)
			{
				obj = metadata.GetDefaultValue(d, dp);
			}
			if (storage._propertyTriggerLayers != null)
			{
				int count = storage._propertyTriggerLayers.Count;
				IList<AnimationLayer> values = storage._propertyTriggerLayers.Values;
				for (int i = 0; i < count; i++)
				{
					obj = values[i].GetCurrentValue(obj);
				}
			}
			if (storage._animationClocks != null)
			{
				FrugalObjectList<AnimationClock> animationClocks = storage._animationClocks;
				int count2 = animationClocks.Count;
				bool flag = false;
				object defaultDestinationValue = obj;
				object obj2 = obj;
				if (storage._snapshotValue != DependencyProperty.UnsetValue)
				{
					obj2 = storage._snapshotValue;
				}
				for (int j = 0; j < count2; j++)
				{
					if (animationClocks[j].CurrentState != ClockState.Stopped)
					{
						flag = true;
						obj2 = animationClocks[j].GetCurrentValue(obj2, defaultDestinationValue);
						if (obj2 == DependencyProperty.UnsetValue)
						{
							throw new InvalidOperationException(SR.Get("Animation_ReturnedUnsetValueInstance", new object[]
							{
								animationClocks[j].Timeline.GetType().FullName,
								dp.Name,
								d.GetType().FullName
							}));
						}
					}
				}
				if (flag)
				{
					obj = obj2;
				}
			}
			if (DependencyProperty.IsValidType(obj, dp.PropertyType))
			{
				return obj;
			}
			throw new InvalidOperationException(SR.Get("Animation_CalculatedValueIsInvalidForProperty", new object[]
			{
				dp.Name,
				(obj == null) ? "null" : obj.ToString()
			}));
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x000D15A8 File Offset: 0x000D09A8
		[FriendAccessAllowed]
		internal static bool IsPropertyAnimatable(DependencyObject d, DependencyProperty dp)
		{
			if (dp.PropertyType != typeof(Visual3DCollection) && dp.ReadOnly)
			{
				return false;
			}
			UIPropertyMetadata uipropertyMetadata = dp.GetMetadata(d.DependencyObjectType) as UIPropertyMetadata;
			return uipropertyMetadata == null || !uipropertyMetadata.IsAnimationProhibited;
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x000D15F8 File Offset: 0x000D09F8
		internal static bool IsAnimationValid(DependencyProperty dp, AnimationTimeline animation)
		{
			return dp.PropertyType.IsAssignableFrom(animation.TargetPropertyType) || animation.TargetPropertyType == typeof(object);
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x000D1630 File Offset: 0x000D0A30
		[FriendAccessAllowed]
		internal static bool IsAnimationClockValid(DependencyProperty dp, AnimationClock animation)
		{
			return AnimationStorage.IsAnimationValid(dp, animation.Timeline);
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x000D164C File Offset: 0x000D0A4C
		internal static FrugalMap GetAnimatedPropertiesMap(DependencyObject d)
		{
			return AnimationStorage.AnimatedPropertyMapField.GetValue(d);
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x000D1664 File Offset: 0x000D0A64
		internal static AnimationStorage GetStorage(DependencyObject d, DependencyProperty dp)
		{
			return AnimationStorage.AnimatedPropertyMapField.GetValue(d)[dp.GlobalIndex] as AnimationStorage;
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x000D1690 File Offset: 0x000D0A90
		private static AnimationStorage CreateStorage(DependencyObject d, DependencyProperty dp)
		{
			AnimationStorage animationStorage;
			if (dp.GetMetadata(d.DependencyObjectType) is IndependentlyAnimatedPropertyMetadata)
			{
				animationStorage = AnimationStorage.CreateIndependentAnimationStorageForType(dp.PropertyType);
			}
			else
			{
				animationStorage = new AnimationStorage();
			}
			animationStorage.Initialize(d, dp);
			return animationStorage;
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x000D16D0 File Offset: 0x000D0AD0
		private static IndependentAnimationStorage CreateIndependentAnimationStorageForType(Type type)
		{
			if (type == typeof(double))
			{
				return new DoubleIndependentAnimationStorage();
			}
			if (type == typeof(Color))
			{
				return new ColorIndependentAnimationStorage();
			}
			if (type == typeof(Matrix))
			{
				return new MatrixIndependentAnimationStorage();
			}
			if (type == typeof(Point3D))
			{
				return new Point3DIndependentAnimationStorage();
			}
			if (type == typeof(Point))
			{
				return new PointIndependentAnimationStorage();
			}
			if (type == typeof(Quaternion))
			{
				return new QuaternionIndependentAnimationStorage();
			}
			if (type == typeof(Rect))
			{
				return new RectIndependentAnimationStorage();
			}
			if (type == typeof(Size))
			{
				return new SizeIndependentAnimationStorage();
			}
			return new Vector3DIndependentAnimationStorage();
		}

		// Token: 0x0400160C RID: 5644
		private static readonly UncommonField<FrugalMap> AnimatedPropertyMapField = new UncommonField<FrugalMap>();

		// Token: 0x0400160D RID: 5645
		protected WeakReference _dependencyObject;

		// Token: 0x0400160E RID: 5646
		protected DependencyProperty _dependencyProperty;

		// Token: 0x0400160F RID: 5647
		protected FrugalObjectList<AnimationClock> _animationClocks;

		// Token: 0x04001610 RID: 5648
		private SortedList<long, AnimationLayer> _propertyTriggerLayers;

		// Token: 0x04001611 RID: 5649
		private EventHandler _currentTimeInvalidatedHandler;

		// Token: 0x04001612 RID: 5650
		private EventHandler _removeRequestedHandler;

		// Token: 0x04001613 RID: 5651
		private object _snapshotValue = DependencyProperty.UnsetValue;

		// Token: 0x04001614 RID: 5652
		private bool _hasStickySnapshotValue;

		// Token: 0x04001615 RID: 5653
		private bool _hadValidationError;

		// Token: 0x04001616 RID: 5654
		internal object _baseValue = DependencyProperty.UnsetValue;
	}
}
