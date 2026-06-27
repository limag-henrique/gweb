using System;
using System.Collections.Generic;
using System.Security;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Input.StylusWisp;
using System.Windows.Input.Tracing;
using System.Windows.Interop;
using System.Windows.Media;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	// Token: 0x020002D2 RID: 722
	internal sealed class PenContexts
	{
		// Token: 0x060015AE RID: 5550 RVA: 0x00050440 File Offset: 0x0004F840
		[SecurityCritical]
		internal PenContexts(WispLogic stylusLogic, PresentationSource inputSource)
		{
			HwndSource hwndSource = inputSource as HwndSource;
			if (hwndSource == null || IntPtr.Zero == hwndSource.CriticalHandle)
			{
				throw new InvalidOperationException(SR.Get("Stylus_PenContextFailure"));
			}
			this._stylusLogic = stylusLogic;
			this._inputSource = new SecurityCriticalData<HwndSource>(hwndSource);
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x000504C8 File Offset: 0x0004F8C8
		[SecurityCritical]
		internal void Enable()
		{
			if (this._contexts == null)
			{
				this._contexts = this._stylusLogic.WispTabletDevices.CreateContexts(this._inputSource.Value.CriticalHandle, this);
				foreach (PenContext penContext in this._contexts)
				{
					penContext.Enable();
				}
			}
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x00050524 File Offset: 0x0004F924
		[SecurityCritical]
		internal void Disable(bool shutdownWorkerThread)
		{
			if (this._contexts != null)
			{
				foreach (PenContext penContext in this._contexts)
				{
					penContext.Disable(shutdownWorkerThread);
				}
				this._contexts = null;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x00050560 File Offset: 0x0004F960
		// (set) Token: 0x060015B2 RID: 5554 RVA: 0x00050574 File Offset: 0x0004F974
		internal bool IsWindowDisabled
		{
			get
			{
				return this._isWindowDisabled;
			}
			set
			{
				this._isWindowDisabled = value;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x00050588 File Offset: 0x0004F988
		// (set) Token: 0x060015B4 RID: 5556 RVA: 0x0005059C File Offset: 0x0004F99C
		internal Point DestroyedLocation
		{
			get
			{
				return this._destroyedLocation;
			}
			set
			{
				this._destroyedLocation = value;
			}
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x000505B0 File Offset: 0x0004F9B0
		[SecurityCritical]
		internal void OnPenDown(PenContext penContext, int tabletDeviceId, int stylusPointerId, int[] data, int timestamp)
		{
			this.ProcessInput(RawStylusActions.Down, penContext, tabletDeviceId, stylusPointerId, data, timestamp);
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x000505CC File Offset: 0x0004F9CC
		[SecurityCritical]
		internal void OnPenUp(PenContext penContext, int tabletDeviceId, int stylusPointerId, int[] data, int timestamp)
		{
			this.ProcessInput(RawStylusActions.Up, penContext, tabletDeviceId, stylusPointerId, data, timestamp);
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x000505E8 File Offset: 0x0004F9E8
		[SecurityCritical]
		internal void OnPackets(PenContext penContext, int tabletDeviceId, int stylusPointerId, int[] data, int timestamp)
		{
			this.ProcessInput(RawStylusActions.Move, penContext, tabletDeviceId, stylusPointerId, data, timestamp);
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00050604 File Offset: 0x0004FA04
		[SecurityCritical]
		internal void OnInAirPackets(PenContext penContext, int tabletDeviceId, int stylusPointerId, int[] data, int timestamp)
		{
			this.ProcessInput(RawStylusActions.InAirMove, penContext, tabletDeviceId, stylusPointerId, data, timestamp);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00050620 File Offset: 0x0004FA20
		[SecurityCritical]
		internal void OnPenInRange(PenContext penContext, int tabletDeviceId, int stylusPointerId, int[] data, int timestamp)
		{
			this.ProcessInput(RawStylusActions.InRange, penContext, tabletDeviceId, stylusPointerId, data, timestamp);
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x0005063C File Offset: 0x0004FA3C
		[SecurityCritical]
		internal void OnPenOutOfRange(PenContext penContext, int tabletDeviceId, int stylusPointerId, int timestamp)
		{
			this.ProcessInput(RawStylusActions.OutOfRange, penContext, tabletDeviceId, stylusPointerId, new int[0], timestamp);
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00050660 File Offset: 0x0004FA60
		[SecurityCritical]
		internal void OnSystemEvent(PenContext penContext, int tabletDeviceId, int stylusPointerId, int timestamp, SystemGesture id, int gestureX, int gestureY, int buttonState)
		{
			this._stylusLogic.ProcessSystemEvent(penContext, tabletDeviceId, stylusPointerId, timestamp, id, gestureX, gestureY, buttonState, this._inputSource.Value);
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x00050690 File Offset: 0x0004FA90
		[SecurityCritical]
		private void ProcessInput(RawStylusActions actions, PenContext penContext, int tabletDeviceId, int stylusPointerId, int[] data, int timestamp)
		{
			this._stylusLogic.ProcessInput(actions, penContext, tabletDeviceId, stylusPointerId, data, timestamp, this._inputSource.Value);
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x000506BC File Offset: 0x0004FABC
		[SecurityCritical]
		internal PenContext GetTabletDeviceIDPenContext(int tabletDeviceId)
		{
			if (this._contexts != null)
			{
				for (int i = 0; i < this._contexts.Length; i++)
				{
					PenContext penContext = this._contexts[i];
					if (penContext.TabletDeviceId == tabletDeviceId)
					{
						return penContext;
					}
				}
			}
			return null;
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x000506FC File Offset: 0x0004FAFC
		[SecurityCritical]
		internal bool ConsiderInRange(int timestamp)
		{
			if (this._contexts != null)
			{
				for (int i = 0; i < this._contexts.Length; i++)
				{
					PenContext penContext = this._contexts[i];
					if (penContext.QueuedInRangeCount > 0 || Math.Abs(timestamp - penContext.LastInRangeTime) <= 500)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x00050750 File Offset: 0x0004FB50
		[SecurityCritical]
		internal void AddContext(uint index)
		{
			if (this._contexts != null && (ulong)index <= (ulong)((long)this._contexts.Length) && this._inputSource.Value.CriticalHandle != IntPtr.Zero)
			{
				PenContext[] array = new PenContext[this._contexts.Length + 1];
				uint num = (uint)(this._contexts.Length - (int)index);
				Array.Copy(this._contexts, 0L, array, 0L, (long)((ulong)index));
				PenContext penContext = this._stylusLogic.TabletDevices[(int)index].As<WispTabletDevice>().CreateContext(this._inputSource.Value.CriticalHandle, this);
				array[(int)index] = penContext;
				Array.Copy(this._contexts, (long)((ulong)index), array, (long)((ulong)(index + 1U)), (long)((ulong)num));
				this._contexts = array;
				penContext.Enable();
			}
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00050818 File Offset: 0x0004FC18
		[SecurityCritical]
		internal void RemoveContext(uint index)
		{
			if (this._contexts != null && (ulong)index < (ulong)((long)this._contexts.Length))
			{
				PenContext penContext = this._contexts[(int)index];
				PenContext[] array = new PenContext[this._contexts.Length - 1];
				uint num = (uint)(this._contexts.Length - (int)index - 1);
				Array.Copy(this._contexts, 0L, array, 0L, (long)((ulong)index));
				Array.Copy(this._contexts, (long)((ulong)(index + 1U)), array, (long)((ulong)index), (long)((ulong)num));
				penContext.Disable(false);
				this._contexts = array;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x00050898 File Offset: 0x0004FC98
		internal object SyncRoot
		{
			get
			{
				return this.__rtiLock;
			}
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x000508AC File Offset: 0x0004FCAC
		internal void AddStylusPlugInCollection(StylusPlugInCollection pic)
		{
			this._plugInCollectionList.Insert(this.FindZOrderIndex(pic), pic);
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x000508CC File Offset: 0x0004FCCC
		internal void RemoveStylusPlugInCollection(StylusPlugInCollection pic)
		{
			this._plugInCollectionList.Remove(pic);
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x000508E8 File Offset: 0x0004FCE8
		internal int FindZOrderIndex(StylusPlugInCollection spicAdding)
		{
			DependencyObject dependencyObject = spicAdding.Element;
			int i;
			for (i = 0; i < this._plugInCollectionList.Count; i++)
			{
				DependencyObject dependencyObject2 = this._plugInCollectionList[i].Element;
				if (VisualTreeHelper.IsAncestorOf(dependencyObject, dependencyObject2))
				{
					for (i++; i < this._plugInCollectionList.Count; i++)
					{
						dependencyObject2 = this._plugInCollectionList[i].Element;
						if (!VisualTreeHelper.IsAncestorOf(dependencyObject, dependencyObject2))
						{
							break;
						}
					}
					return i;
				}
				DependencyObject dependencyObject3 = VisualTreeHelper.FindCommonAncestor(dependencyObject, dependencyObject2);
				if (dependencyObject3 != null)
				{
					if (dependencyObject2 == dependencyObject3)
					{
						return i;
					}
					while (VisualTreeHelper.GetParentInternal(dependencyObject) != dependencyObject3)
					{
						dependencyObject = VisualTreeHelper.GetParentInternal(dependencyObject);
					}
					while (VisualTreeHelper.GetParentInternal(dependencyObject2) != dependencyObject3)
					{
						dependencyObject2 = VisualTreeHelper.GetParentInternal(dependencyObject2);
					}
					int childrenCount = VisualTreeHelper.GetChildrenCount(dependencyObject3);
					for (int j = 0; j < childrenCount; j++)
					{
						DependencyObject child = VisualTreeHelper.GetChild(dependencyObject3, j);
						if (child == dependencyObject)
						{
							return i;
						}
						if (child == dependencyObject2)
						{
							break;
						}
					}
				}
			}
			return i;
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x000509CC File Offset: 0x0004FDCC
		[SecurityCritical]
		internal StylusPlugInCollection InvokeStylusPluginCollectionForMouse(RawStylusInputReport inputReport, IInputElement directlyOver, StylusPlugInCollection currentPlugInCollection)
		{
			StylusPlugInCollection stylusPlugInCollection = null;
			object _rtiLock = this.__rtiLock;
			lock (_rtiLock)
			{
				if (directlyOver != null)
				{
					UIElement uielement = InputElement.GetContainingUIElement(directlyOver as DependencyObject) as UIElement;
					if (uielement != null)
					{
						stylusPlugInCollection = this.FindPlugInCollection(uielement);
					}
				}
				if (currentPlugInCollection != null && currentPlugInCollection != stylusPlugInCollection)
				{
					RawStylusInput rawStylusInput = new RawStylusInput(inputReport, currentPlugInCollection.ViewToElement, currentPlugInCollection);
					currentPlugInCollection.FireEnterLeave(false, rawStylusInput, true);
					this._stylusLogic.Statistics.FeaturesUsed |= StylusTraceLogger.FeatureFlags.StylusPluginsUsed;
				}
				if (stylusPlugInCollection != null)
				{
					RawStylusInput rawStylusInput2 = new RawStylusInput(inputReport, stylusPlugInCollection.ViewToElement, stylusPlugInCollection);
					inputReport.RawStylusInput = rawStylusInput2;
					if (stylusPlugInCollection != currentPlugInCollection)
					{
						stylusPlugInCollection.FireEnterLeave(true, rawStylusInput2, true);
					}
					stylusPlugInCollection.FireRawStylusInput(rawStylusInput2);
					this._stylusLogic.Statistics.FeaturesUsed |= StylusTraceLogger.FeatureFlags.StylusPluginsUsed;
					foreach (RawStylusInputCustomData rawStylusInputCustomData in rawStylusInput2.CustomDataList)
					{
						rawStylusInputCustomData.Owner.FireCustomData(rawStylusInputCustomData.Data, inputReport.Actions, true);
					}
				}
			}
			return stylusPlugInCollection;
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00050B14 File Offset: 0x0004FF14
		[SecurityCritical]
		internal void InvokeStylusPluginCollection(RawStylusInputReport inputReport)
		{
			object _rtiLock = this.__rtiLock;
			lock (_rtiLock)
			{
				RawStylusActions actions = inputReport.Actions;
				if (actions == RawStylusActions.Down || actions == RawStylusActions.Up || actions == RawStylusActions.Move)
				{
					StylusPlugInCollection stylusPlugInCollection = this.TargetPlugInCollection(inputReport);
					WispStylusDevice wispStylusDevice = inputReport.StylusDevice.As<WispStylusDevice>();
					StylusPlugInCollection currentNonVerifiedTarget = wispStylusDevice.CurrentNonVerifiedTarget;
					if (currentNonVerifiedTarget != null && currentNonVerifiedTarget != stylusPlugInCollection)
					{
						GeneralTransformGroup generalTransformGroup = new GeneralTransformGroup();
						generalTransformGroup.Children.Add(new MatrixTransform(this._stylusLogic.GetTabletToViewTransform(wispStylusDevice.TabletDevice)));
						generalTransformGroup.Children.Add(currentNonVerifiedTarget.ViewToElement);
						generalTransformGroup.Freeze();
						RawStylusInput rawStylusInput = new RawStylusInput(inputReport, generalTransformGroup, currentNonVerifiedTarget);
						currentNonVerifiedTarget.FireEnterLeave(false, rawStylusInput, false);
						this._stylusLogic.Statistics.FeaturesUsed |= StylusTraceLogger.FeatureFlags.StylusPluginsUsed;
						wispStylusDevice.CurrentNonVerifiedTarget = null;
					}
					if (stylusPlugInCollection != null)
					{
						GeneralTransformGroup generalTransformGroup2 = new GeneralTransformGroup();
						generalTransformGroup2.Children.Add(new MatrixTransform(this._stylusLogic.GetTabletToViewTransform(wispStylusDevice.TabletDevice)));
						generalTransformGroup2.Children.Add(stylusPlugInCollection.ViewToElement);
						generalTransformGroup2.Freeze();
						RawStylusInput rawStylusInput2 = new RawStylusInput(inputReport, generalTransformGroup2, stylusPlugInCollection);
						inputReport.RawStylusInput = rawStylusInput2;
						if (stylusPlugInCollection != currentNonVerifiedTarget)
						{
							wispStylusDevice.CurrentNonVerifiedTarget = stylusPlugInCollection;
							stylusPlugInCollection.FireEnterLeave(true, rawStylusInput2, false);
						}
						stylusPlugInCollection.FireRawStylusInput(rawStylusInput2);
						this._stylusLogic.Statistics.FeaturesUsed |= StylusTraceLogger.FeatureFlags.StylusPluginsUsed;
					}
				}
			}
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00050CA8 File Offset: 0x000500A8
		[SecurityCritical]
		internal StylusPlugInCollection TargetPlugInCollection(RawStylusInputReport inputReport)
		{
			WispStylusDevice wispStylusDevice = inputReport.StylusDevice.As<WispStylusDevice>();
			bool flag = false;
			StylusPlugInCollection stylusPlugInCollection = wispStylusDevice.GetCapturedPlugInCollection(ref flag);
			int inputArrayLengthPerPoint = inputReport.PenContext.StylusPointDescription.GetInputArrayLengthPerPoint();
			if (flag && !this._plugInCollectionList.Contains(stylusPlugInCollection))
			{
				flag = false;
			}
			if (!flag && inputReport.Data != null && inputReport.Data.Length >= inputArrayLengthPerPoint)
			{
				int[] data = inputReport.Data;
				Point point = new Point((double)data[data.Length - inputArrayLengthPerPoint], (double)data[data.Length - inputArrayLengthPerPoint + 1]);
				point *= wispStylusDevice.TabletDevice.TabletDeviceImpl.TabletToScreen;
				point.X = (double)((int)Math.Round(point.X));
				point.Y = (double)((int)Math.Round(point.Y));
				point = this._stylusLogic.MeasureUnitsFromDeviceUnits(point);
				stylusPlugInCollection = this.HittestPlugInCollection(point);
			}
			return stylusPlugInCollection;
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00050D90 File Offset: 0x00050190
		internal StylusPlugInCollection FindPlugInCollection(UIElement element)
		{
			foreach (StylusPlugInCollection stylusPlugInCollection in this._plugInCollectionList)
			{
				if (stylusPlugInCollection.Element == element || stylusPlugInCollection.Element.IsAncestorOf(element))
				{
					return stylusPlugInCollection;
				}
			}
			return null;
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x00050E08 File Offset: 0x00050208
		private StylusPlugInCollection HittestPlugInCollection(Point pt)
		{
			foreach (StylusPlugInCollection stylusPlugInCollection in this._plugInCollectionList)
			{
				if (stylusPlugInCollection.IsHit(pt))
				{
					return stylusPlugInCollection;
				}
			}
			return null;
		}

		// Token: 0x04000BD9 RID: 3033
		internal SecurityCriticalData<HwndSource> _inputSource;

		// Token: 0x04000BDA RID: 3034
		[SecurityCritical]
		private WispLogic _stylusLogic;

		// Token: 0x04000BDB RID: 3035
		private object __rtiLock = new object();

		// Token: 0x04000BDC RID: 3036
		private List<StylusPlugInCollection> _plugInCollectionList = new List<StylusPlugInCollection>();

		// Token: 0x04000BDD RID: 3037
		[SecurityCritical]
		private PenContext[] _contexts;

		// Token: 0x04000BDE RID: 3038
		private bool _isWindowDisabled;

		// Token: 0x04000BDF RID: 3039
		private Point _destroyedLocation = new Point(0.0, 0.0);
	}
}
