using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security;
using System.Windows.Input.Manipulations;
using System.Windows.Media;
using MS.Internal;
using MS.Utility;

namespace System.Windows.Input
{
	// Token: 0x02000274 RID: 628
	internal sealed class ManipulationDevice : InputDevice
	{
		// Token: 0x060011EE RID: 4590 RVA: 0x00043024 File Offset: 0x00042424
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private ManipulationDevice(UIElement element)
		{
			this._target = element;
			this._activeSource = PresentationSource.CriticalFromVisual(element);
			this._inputManager = InputManager.UnsecureCurrent;
			this._inputManager.PostProcessInput += this.PostProcessInput;
			this._manipulationLogic = new ManipulationLogic(this);
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00043078 File Offset: 0x00042478
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void DetachManipulationDevice()
		{
			this._inputManager.PostProcessInput -= this.PostProcessInput;
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x0004309C File Offset: 0x0004249C
		public override IInputElement Target
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x000430B0 File Offset: 0x000424B0
		public override PresentationSource ActiveSource
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandUIWindowPermission();
				return this._activeSource;
			}
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x000430C8 File Offset: 0x000424C8
		internal static ManipulationDevice AddManipulationDevice(UIElement element)
		{
			element.VerifyAccess();
			ManipulationDevice manipulationDevice = ManipulationDevice.GetManipulationDevice(element);
			if (manipulationDevice == null)
			{
				if (ManipulationDevice._manipulationDevices == null)
				{
					ManipulationDevice._manipulationDevices = new Dictionary<UIElement, ManipulationDevice>(2);
				}
				manipulationDevice = new ManipulationDevice(element);
				ManipulationDevice._manipulationDevices[element] = manipulationDevice;
			}
			return manipulationDevice;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0004310C File Offset: 0x0004250C
		internal static ManipulationDevice GetManipulationDevice(UIElement element)
		{
			if (ManipulationDevice._manipulationDevices != null)
			{
				ManipulationDevice result;
				ManipulationDevice._manipulationDevices.TryGetValue(element, out result);
				return result;
			}
			return null;
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00043134 File Offset: 0x00042534
		private void RemoveManipulationDevice()
		{
			this._wasTicking = false;
			this.StopTicking();
			this.DetachManipulationDevice();
			this._compensateForBoundaryFeedback = null;
			this.RemoveAllManipulators();
			if (ManipulationDevice._manipulationDevices != null)
			{
				ManipulationDevice._manipulationDevices.Remove(this._target);
			}
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0004317C File Offset: 0x0004257C
		private void RemoveAllManipulators()
		{
			if (this._manipulators != null)
			{
				for (int i = this._manipulators.Count - 1; i >= 0; i--)
				{
					this._manipulators[i].Updated -= this.OnManipulatorUpdated;
				}
				this._manipulators.Clear();
			}
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x000431D4 File Offset: 0x000425D4
		internal void AddManipulator(IManipulator manipulator)
		{
			base.VerifyAccess();
			this._manipulationEnded = false;
			if (this._manipulators == null)
			{
				this._manipulators = new List<IManipulator>(2);
			}
			this._manipulators.Add(manipulator);
			manipulator.Updated += this.OnManipulatorUpdated;
			this.OnManipulatorUpdated(manipulator, EventArgs.Empty);
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0004322C File Offset: 0x0004262C
		internal void RemoveManipulator(IManipulator manipulator)
		{
			base.VerifyAccess();
			manipulator.Updated -= this.OnManipulatorUpdated;
			if (this._manipulators != null)
			{
				this._manipulators.Remove(manipulator);
			}
			this.OnManipulatorUpdated(manipulator, EventArgs.Empty);
			if (!this._manipulationEnded)
			{
				if (this._manipulators == null || this._manipulators.Count == 0)
				{
					this._removedManipulator = manipulator;
				}
				this.ReportFrame();
				this._removedManipulator = null;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x000432A4 File Offset: 0x000426A4
		// (set) Token: 0x060011F9 RID: 4601 RVA: 0x000432BC File Offset: 0x000426BC
		internal ManipulationModes ManipulationMode
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				return this._manipulationLogic.ManipulationMode;
			}
			[SecurityCritical]
			[SecurityTreatAsSafe]
			set
			{
				this._manipulationLogic.ManipulationMode = value;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x000432D8 File Offset: 0x000426D8
		// (set) Token: 0x060011FB RID: 4603 RVA: 0x000432F0 File Offset: 0x000426F0
		internal ManipulationPivot ManipulationPivot
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				return this._manipulationLogic.ManipulationPivot;
			}
			[SecurityTreatAsSafe]
			[SecurityCritical]
			set
			{
				this._manipulationLogic.ManipulationPivot = value;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x0004330C File Offset: 0x0004270C
		// (set) Token: 0x060011FD RID: 4605 RVA: 0x00043324 File Offset: 0x00042724
		internal IInputElement ManipulationContainer
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				return this._manipulationLogic.ManipulationContainer;
			}
			[SecurityTreatAsSafe]
			[SecurityCritical]
			set
			{
				this._manipulationLogic.ManipulationContainer = value;
			}
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00043340 File Offset: 0x00042740
		internal IEnumerable<IManipulator> GetManipulatorsReadOnly()
		{
			if (this._manipulators != null)
			{
				return new ReadOnlyCollection<IManipulator>(this._manipulators);
			}
			return new ReadOnlyCollection<IManipulator>(new List<IManipulator>(2));
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0004336C File Offset: 0x0004276C
		internal void OnManipulatorUpdated(object sender, EventArgs e)
		{
			ManipulationDevice.LastUpdatedTimestamp = ManipulationLogic.GetCurrentTimestamp();
			ManipulationDevice.ResumeAllTicking();
			this.StartTicking();
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00043390 File Offset: 0x00042790
		internal Point GetTransformedManipulatorPosition(Point point)
		{
			if (this._compensateForBoundaryFeedback != null)
			{
				return this._compensateForBoundaryFeedback(point);
			}
			return point;
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x000433B4 File Offset: 0x000427B4
		private static void ResumeAllTicking()
		{
			if (ManipulationDevice._manipulationDevices != null)
			{
				foreach (UIElement key in ManipulationDevice._manipulationDevices.Keys)
				{
					ManipulationDevice manipulationDevice = ManipulationDevice._manipulationDevices[key];
					if (manipulationDevice != null && manipulationDevice._wasTicking)
					{
						manipulationDevice.StartTicking();
						manipulationDevice._wasTicking = false;
					}
				}
			}
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0004343C File Offset: 0x0004283C
		private void StartTicking()
		{
			if (!this._ticking)
			{
				this._ticking = true;
				CompositionTarget.Rendering += this.OnRendering;
				this.SubscribeToLayoutUpdate();
			}
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00043470 File Offset: 0x00042870
		private void StopTicking()
		{
			if (this._ticking)
			{
				CompositionTarget.Rendering -= this.OnRendering;
				this._ticking = false;
				this.UnsubscribeFromLayoutUpdate();
			}
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x000434A4 File Offset: 0x000428A4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void SubscribeToLayoutUpdate()
		{
			this._manipulationLogic.ContainerLayoutUpdated += this.OnContainerLayoutUpdated;
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x000434C8 File Offset: 0x000428C8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void UnsubscribeFromLayoutUpdate()
		{
			this._manipulationLogic.ContainerLayoutUpdated -= this.OnContainerLayoutUpdated;
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x000434EC File Offset: 0x000428EC
		private void OnContainerLayoutUpdated(object sender, EventArgs e)
		{
			this.ReportFrame();
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00043500 File Offset: 0x00042900
		private void OnRendering(object sender, EventArgs e)
		{
			this.ReportFrame();
			if (!this.IsManipulationActive || ManipulationLogic.GetCurrentTimestamp() - ManipulationDevice.LastUpdatedTimestamp > 50000000L)
			{
				this._wasTicking = this._ticking;
				this.StopTicking();
			}
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00043540 File Offset: 0x00042940
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void ReportFrame()
		{
			if (!this._manipulationEnded)
			{
				EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordInput, EventTrace.Level.Info, EventTrace.Event.ManipulationReportFrame, 0);
				this._manipulationLogic.ReportFrame(this._manipulators);
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x0004357C File Offset: 0x0004297C
		internal bool IsManipulationActive
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				return this._manipulationLogic.IsManipulationActive;
			}
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00043594 File Offset: 0x00042994
		[SecurityCritical]
		private void PostProcessInput(object sender, ProcessInputEventArgs e)
		{
			InputEventArgs input = e.StagingItem.Input;
			if (input.Device == this)
			{
				RoutedEvent routedEvent = input.RoutedEvent;
				if (routedEvent == Manipulation.ManipulationDeltaEvent)
				{
					ManipulationDeltaEventArgs manipulationDeltaEventArgs = input as ManipulationDeltaEventArgs;
					if (manipulationDeltaEventArgs != null)
					{
						ManipulationDelta unusedManipulation = manipulationDeltaEventArgs.UnusedManipulation;
						this._manipulationLogic.RaiseBoundaryFeedback(unusedManipulation, manipulationDeltaEventArgs.RequestedComplete);
						this._manipulationLogic.PushEventsToDevice();
						if (manipulationDeltaEventArgs.RequestedComplete)
						{
							this._manipulationLogic.Complete(manipulationDeltaEventArgs.RequestedInertia);
							this._manipulationLogic.PushEventsToDevice();
							return;
						}
						if (manipulationDeltaEventArgs.RequestedCancel)
						{
							this.OnManipulationCancel();
							return;
						}
					}
				}
				else if (routedEvent == Manipulation.ManipulationStartingEvent)
				{
					ManipulationStartingEventArgs manipulationStartingEventArgs = input as ManipulationStartingEventArgs;
					if (manipulationStartingEventArgs != null && manipulationStartingEventArgs.RequestedCancel)
					{
						this.OnManipulationCancel();
						return;
					}
				}
				else if (routedEvent == Manipulation.ManipulationStartedEvent)
				{
					ManipulationStartedEventArgs manipulationStartedEventArgs = input as ManipulationStartedEventArgs;
					if (manipulationStartedEventArgs != null)
					{
						if (manipulationStartedEventArgs.RequestedComplete)
						{
							this._manipulationLogic.Complete(false);
							this._manipulationLogic.PushEventsToDevice();
							return;
						}
						if (manipulationStartedEventArgs.RequestedCancel)
						{
							this.OnManipulationCancel();
							return;
						}
						ManipulationDevice.ResumeAllTicking();
						this.StartTicking();
						return;
					}
				}
				else if (routedEvent == Manipulation.ManipulationInertiaStartingEvent)
				{
					this.StopTicking();
					this.RemoveAllManipulators();
					ManipulationInertiaStartingEventArgs manipulationInertiaStartingEventArgs = input as ManipulationInertiaStartingEventArgs;
					if (manipulationInertiaStartingEventArgs != null)
					{
						if (manipulationInertiaStartingEventArgs.RequestedCancel)
						{
							this.OnManipulationCancel();
							return;
						}
						this._manipulationLogic.BeginInertia(manipulationInertiaStartingEventArgs);
						return;
					}
				}
				else if (routedEvent == Manipulation.ManipulationCompletedEvent)
				{
					this._manipulationLogic.OnCompleted();
					ManipulationCompletedEventArgs manipulationCompletedEventArgs = input as ManipulationCompletedEventArgs;
					if (manipulationCompletedEventArgs != null)
					{
						if (manipulationCompletedEventArgs.RequestedCancel)
						{
							this.OnManipulationCancel();
							return;
						}
						if (!manipulationCompletedEventArgs.IsInertial || !this._ticking)
						{
							this.OnManipulationComplete();
							return;
						}
					}
				}
				else if (routedEvent == Manipulation.ManipulationBoundaryFeedbackEvent)
				{
					ManipulationBoundaryFeedbackEventArgs manipulationBoundaryFeedbackEventArgs = input as ManipulationBoundaryFeedbackEventArgs;
					if (manipulationBoundaryFeedbackEventArgs != null)
					{
						this._compensateForBoundaryFeedback = manipulationBoundaryFeedbackEventArgs.CompensateForBoundaryFeedback;
					}
				}
			}
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00043758 File Offset: 0x00042B58
		[SecurityCritical]
		private void OnManipulationCancel()
		{
			this._manipulationEnded = true;
			if (this._manipulators != null)
			{
				if (this._removedManipulator != null)
				{
					this._removedManipulator.ManipulationEnded(true);
				}
				else
				{
					List<IManipulator> list = new List<IManipulator>(this._manipulators);
					foreach (IManipulator manipulator in list)
					{
						manipulator.ManipulationEnded(true);
					}
				}
			}
			this.RemoveManipulationDevice();
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x000437EC File Offset: 0x00042BEC
		[SecurityCritical]
		private void OnManipulationComplete()
		{
			this._manipulationEnded = true;
			if (this._manipulators != null)
			{
				List<IManipulator> list = new List<IManipulator>(this._manipulators);
				foreach (IManipulator manipulator in list)
				{
					manipulator.ManipulationEnded(false);
				}
			}
			this.RemoveManipulationDevice();
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00043868 File Offset: 0x00042C68
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal void SetManipulationParameters(ManipulationParameters2D parameter)
		{
			this._manipulationLogic.SetManipulationParameters(parameter);
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00043884 File Offset: 0x00042C84
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal void CompleteManipulation(bool withInertia)
		{
			if (this._manipulationLogic != null)
			{
				this._manipulationLogic.Complete(withInertia);
				this._manipulationLogic.PushEventsToDevice();
			}
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x000438B0 File Offset: 0x00042CB0
		[SecurityCritical]
		internal void ProcessManipulationInput(InputEventArgs e)
		{
			EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordInput, EventTrace.Level.Info, EventTrace.Event.ManipulationEventRaised, 0);
			this._inputManager.ProcessInput(e);
		}

		// Token: 0x040009C3 RID: 2499
		[SecurityCritical]
		private InputManager _inputManager;

		// Token: 0x040009C4 RID: 2500
		[SecurityCritical]
		private ManipulationLogic _manipulationLogic;

		// Token: 0x040009C5 RID: 2501
		[SecurityCritical]
		private PresentationSource _activeSource;

		// Token: 0x040009C6 RID: 2502
		private UIElement _target;

		// Token: 0x040009C7 RID: 2503
		private List<IManipulator> _manipulators;

		// Token: 0x040009C8 RID: 2504
		private bool _ticking;

		// Token: 0x040009C9 RID: 2505
		private bool _wasTicking;

		// Token: 0x040009CA RID: 2506
		private Func<Point, Point> _compensateForBoundaryFeedback;

		// Token: 0x040009CB RID: 2507
		private bool _manipulationEnded;

		// Token: 0x040009CC RID: 2508
		private IManipulator _removedManipulator;

		// Token: 0x040009CD RID: 2509
		[ThreadStatic]
		private static long LastUpdatedTimestamp;

		// Token: 0x040009CE RID: 2510
		private const long ThrottleTimeout = 50000000L;

		// Token: 0x040009CF RID: 2511
		[ThreadStatic]
		private static Dictionary<UIElement, ManipulationDevice> _manipulationDevices;
	}
}
