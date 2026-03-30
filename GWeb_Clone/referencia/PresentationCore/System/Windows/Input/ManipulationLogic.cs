using System;
using System.Collections.Generic;
using System.Security;
using System.Windows.Input.Manipulations;
using System.Windows.Media;
using System.Windows.Threading;
using MS.Internal;
using MS.Win32;

namespace System.Windows.Input
{
	// Token: 0x02000276 RID: 630
	internal sealed class ManipulationLogic
	{
		// Token: 0x06001227 RID: 4647 RVA: 0x00043CE4 File Offset: 0x000430E4
		internal ManipulationLogic(ManipulationDevice manipulationDevice)
		{
			this._manipulationDevice = manipulationDevice;
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00043D18 File Offset: 0x00043118
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void OnManipulationStarted(object sender, Manipulation2DStartedEventArgs e)
		{
			this.PushEvent(new ManipulationStartedEventArgs(this._manipulationDevice, this.LastTimestamp, this._currentContainer, new Point((double)e.OriginX, (double)e.OriginY)));
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00043D58 File Offset: 0x00043158
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void OnManipulationDelta(object sender, Manipulation2DDeltaEventArgs e)
		{
			ManipulationDeltaEventArgs e2 = new ManipulationDeltaEventArgs(this._manipulationDevice, this.LastTimestamp, this._currentContainer, new Point((double)e.OriginX, (double)e.OriginY), ManipulationLogic.ConvertDelta(e.Delta, null), ManipulationLogic.ConvertDelta(e.Cumulative, this._lastManipulationBeforeInertia), ManipulationLogic.ConvertVelocities(e.Velocities), this.IsInertiaActive);
			this.PushEvent(e2);
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00043DC8 File Offset: 0x000431C8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void OnManipulationCompleted(object sender, Manipulation2DCompletedEventArgs e)
		{
			if (this._manualComplete && !this._manualCompleteWithInertia)
			{
				ManipulationCompletedEventArgs e2 = this.ConvertCompletedArguments(e);
				this.RaiseManipulationCompleted(e2);
			}
			else
			{
				this._lastManipulationBeforeInertia = ManipulationLogic.ConvertDelta(e.Total, null);
				ManipulationInertiaStartingEventArgs e3 = new ManipulationInertiaStartingEventArgs(this._manipulationDevice, this.LastTimestamp, this._currentContainer, new Point((double)e.OriginX, (double)e.OriginY), ManipulationLogic.ConvertVelocities(e.Velocities), false);
				this.PushEvent(e3);
			}
			this._manipulationProcessor = null;
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00043E50 File Offset: 0x00043250
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void OnInertiaCompleted(object sender, Manipulation2DCompletedEventArgs e)
		{
			this.ClearTimer();
			if (this._manualComplete && this._manualCompleteWithInertia)
			{
				this._lastManipulationBeforeInertia = ManipulationLogic.ConvertDelta(e.Total, this._lastManipulationBeforeInertia);
				ManipulationInertiaStartingEventArgs e2 = new ManipulationInertiaStartingEventArgs(this._manipulationDevice, this.LastTimestamp, this._currentContainer, new Point((double)e.OriginX, (double)e.OriginY), ManipulationLogic.ConvertVelocities(e.Velocities), true);
				this.PushEvent(e2);
			}
			else
			{
				ManipulationCompletedEventArgs e3 = this.ConvertCompletedArguments(e);
				this.RaiseManipulationCompleted(e3);
			}
			this._inertiaProcessor = null;
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00043EE0 File Offset: 0x000432E0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void RaiseManipulationCompleted(ManipulationCompletedEventArgs e)
		{
			this.PushEvent(e);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00043EF4 File Offset: 0x000432F4
		internal void OnCompleted()
		{
			this._lastManipulationBeforeInertia = null;
			this.SetContainer(null);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00043F10 File Offset: 0x00043310
		private ManipulationCompletedEventArgs ConvertCompletedArguments(Manipulation2DCompletedEventArgs e)
		{
			return new ManipulationCompletedEventArgs(this._manipulationDevice, this.LastTimestamp, this._currentContainer, new Point((double)e.OriginX, (double)e.OriginY), ManipulationLogic.ConvertDelta(e.Total, this._lastManipulationBeforeInertia), ManipulationLogic.ConvertVelocities(e.Velocities), this.IsInertiaActive);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00043F6C File Offset: 0x0004336C
		private static ManipulationDelta ConvertDelta(ManipulationDelta2D delta, ManipulationDelta add)
		{
			if (add != null)
			{
				return new ManipulationDelta(new Vector((double)delta.TranslationX + add.Translation.X, (double)delta.TranslationY + add.Translation.Y), AngleUtil.RadiansToDegrees((double)delta.Rotation) + add.Rotation, new Vector((double)delta.ScaleX * add.Scale.X, (double)delta.ScaleY * add.Scale.Y), new Vector((double)delta.ExpansionX + add.Expansion.X, (double)delta.ExpansionY + add.Expansion.Y));
			}
			return new ManipulationDelta(new Vector((double)delta.TranslationX, (double)delta.TranslationY), AngleUtil.RadiansToDegrees((double)delta.Rotation), new Vector((double)delta.ScaleX, (double)delta.ScaleY), new Vector((double)delta.ExpansionX, (double)delta.ExpansionY));
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00044078 File Offset: 0x00043478
		private static ManipulationVelocities ConvertVelocities(ManipulationVelocities2D velocities)
		{
			return new ManipulationVelocities(new Vector((double)velocities.LinearVelocityX, (double)velocities.LinearVelocityY), AngleUtil.RadiansToDegrees((double)velocities.AngularVelocity), new Vector((double)velocities.ExpansionVelocityX, (double)velocities.ExpansionVelocityY));
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x000440BC File Offset: 0x000434BC
		internal void Complete(bool withInertia)
		{
			try
			{
				this._manualComplete = true;
				this._manualCompleteWithInertia = withInertia;
				if (this.IsManipulationActive)
				{
					this._manipulationProcessor.CompleteManipulation(ManipulationLogic.GetCurrentTimestamp());
				}
				else if (this.IsInertiaActive)
				{
					this._inertiaProcessor.Complete(ManipulationLogic.GetCurrentTimestamp());
				}
			}
			finally
			{
				this._manualComplete = false;
				this._manualCompleteWithInertia = false;
			}
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00044138 File Offset: 0x00043538
		private ManipulationCompletedEventArgs GetManipulationCompletedArguments(ManipulationInertiaStartingEventArgs e)
		{
			return new ManipulationCompletedEventArgs(this._manipulationDevice, this.LastTimestamp, this._currentContainer, new Point(e.ManipulationOrigin.X, e.ManipulationOrigin.Y), this._lastManipulationBeforeInertia, e.InitialVelocities, this.IsInertiaActive);
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00044190 File Offset: 0x00043590
		internal void BeginInertia(ManipulationInertiaStartingEventArgs e)
		{
			if (e.CanBeginInertia())
			{
				this._inertiaProcessor = new InertiaProcessor2D();
				this._inertiaProcessor.Delta += this.OnManipulationDelta;
				this._inertiaProcessor.Completed += this.OnInertiaCompleted;
				e.ApplyParameters(this._inertiaProcessor);
				this._inertiaTimer = new DispatcherTimer();
				this._inertiaTimer.Interval = TimeSpan.FromMilliseconds(15.0);
				this._inertiaTimer.Tick += this.OnInertiaTick;
				this._inertiaTimer.Start();
				return;
			}
			ManipulationCompletedEventArgs manipulationCompletedArguments = this.GetManipulationCompletedArguments(e);
			this.RaiseManipulationCompleted(manipulationCompletedArguments);
			this.PushEventsToDevice();
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0004424C File Offset: 0x0004364C
		internal static long GetCurrentTimestamp()
		{
			return MediaContext.CurrentTicks;
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00044260 File Offset: 0x00043660
		private void OnInertiaTick(object sender, EventArgs e)
		{
			if (this.IsInertiaActive)
			{
				if (!this._inertiaProcessor.Process(ManipulationLogic.GetCurrentTimestamp()))
				{
					this.ClearTimer();
				}
				this.PushEventsToDevice();
				return;
			}
			this.ClearTimer();
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0004429C File Offset: 0x0004369C
		private void ClearTimer()
		{
			if (this._inertiaTimer != null)
			{
				this._inertiaTimer.Stop();
				this._inertiaTimer = null;
			}
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x000442C4 File Offset: 0x000436C4
		[SecurityCritical]
		private void PushEvent(InputEventArgs e)
		{
			this._generatedEvent = e;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x000442D8 File Offset: 0x000436D8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal void PushEventsToDevice()
		{
			if (this._generatedEvent != null)
			{
				InputEventArgs generatedEvent = this._generatedEvent;
				this._generatedEvent = null;
				this._manipulationDevice.ProcessManipulationInput(generatedEvent);
			}
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00044308 File Offset: 0x00043708
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal void RaiseBoundaryFeedback(ManipulationDelta unusedManipulation, bool requestedComplete)
		{
			bool flag = unusedManipulation != null;
			if ((!flag || requestedComplete) && this.HasPendingBoundaryFeedback)
			{
				unusedManipulation = new ManipulationDelta(default(Vector), 0.0, new Vector(1.0, 1.0), default(Vector));
				this.HasPendingBoundaryFeedback = false;
			}
			else if (flag)
			{
				this.HasPendingBoundaryFeedback = true;
			}
			if (unusedManipulation != null)
			{
				this.PushEvent(new ManipulationBoundaryFeedbackEventArgs(this._manipulationDevice, this.LastTimestamp, this._currentContainer, unusedManipulation));
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x00044398 File Offset: 0x00043798
		// (set) Token: 0x0600123B RID: 4667 RVA: 0x000443AC File Offset: 0x000437AC
		private bool HasPendingBoundaryFeedback { get; set; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x000443C0 File Offset: 0x000437C0
		// (set) Token: 0x0600123D RID: 4669 RVA: 0x000443D4 File Offset: 0x000437D4
		private int LastTimestamp { get; set; }

		// Token: 0x0600123E RID: 4670 RVA: 0x000443E8 File Offset: 0x000437E8
		internal void ReportFrame(ICollection<IManipulator> manipulators)
		{
			long currentTimestamp = ManipulationLogic.GetCurrentTimestamp();
			this.LastTimestamp = SafeNativeMethods.GetMessageTime();
			int count = manipulators.Count;
			if (this.IsInertiaActive && count > 0)
			{
				this._inertiaProcessor.Complete(currentTimestamp);
				this.PushEventsToDevice();
			}
			if (!this.IsManipulationActive && count > 0)
			{
				ManipulationStartingEventArgs manipulationStartingEventArgs = this.RaiseStarting();
				if (!manipulationStartingEventArgs.RequestedCancel && manipulationStartingEventArgs.Mode != ManipulationModes.None && (manipulationStartingEventArgs.IsSingleTouchEnabled || count >= 2))
				{
					this.SetContainer(manipulationStartingEventArgs.ManipulationContainer);
					this._mode = manipulationStartingEventArgs.Mode;
					this._pivot = manipulationStartingEventArgs.Pivot;
					IList<ManipulationParameters2D> parameters = manipulationStartingEventArgs.Parameters;
					this._manipulationProcessor = new ManipulationProcessor2D(ManipulationLogic.ConvertMode(this._mode), ManipulationLogic.ConvertPivot(this._pivot));
					if (parameters != null)
					{
						int count2 = parameters.Count;
						for (int i = 0; i < parameters.Count; i++)
						{
							this._manipulationProcessor.SetParameters(parameters[i]);
						}
					}
					this._manipulationProcessor.Started += this.OnManipulationStarted;
					this._manipulationProcessor.Delta += this.OnManipulationDelta;
					this._manipulationProcessor.Completed += this.OnManipulationCompleted;
					this._currentManipulators.Clear();
				}
			}
			if (this.IsManipulationActive)
			{
				this.UpdateManipulators(manipulators);
				this._manipulationProcessor.ProcessManipulators(currentTimestamp, this.CurrentManipulators);
				this.PushEventsToDevice();
			}
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00044560 File Offset: 0x00043960
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private ManipulationStartingEventArgs RaiseStarting()
		{
			ManipulationStartingEventArgs manipulationStartingEventArgs = new ManipulationStartingEventArgs(this._manipulationDevice, Environment.TickCount);
			manipulationStartingEventArgs.ManipulationContainer = this._manipulationDevice.Target;
			this._manipulationDevice.ProcessManipulationInput(manipulationStartingEventArgs);
			return manipulationStartingEventArgs;
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x0004459C File Offset: 0x0004399C
		// (set) Token: 0x06001241 RID: 4673 RVA: 0x000445B0 File Offset: 0x000439B0
		internal IInputElement ManipulationContainer
		{
			get
			{
				return this._currentContainer;
			}
			set
			{
				this.SetContainer(value);
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x000445C4 File Offset: 0x000439C4
		// (set) Token: 0x06001243 RID: 4675 RVA: 0x000445D8 File Offset: 0x000439D8
		internal ManipulationModes ManipulationMode
		{
			get
			{
				return this._mode;
			}
			set
			{
				this._mode = value;
				if (this._manipulationProcessor != null)
				{
					this._manipulationProcessor.SupportedManipulations = ManipulationLogic.ConvertMode(this._mode);
				}
			}
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0004460C File Offset: 0x00043A0C
		private static Manipulations2D ConvertMode(ManipulationModes mode)
		{
			Manipulations2D manipulations2D = Manipulations2D.None;
			if ((mode & ManipulationModes.TranslateX) != ManipulationModes.None)
			{
				manipulations2D |= Manipulations2D.TranslateX;
			}
			if ((mode & ManipulationModes.TranslateY) != ManipulationModes.None)
			{
				manipulations2D |= Manipulations2D.TranslateY;
			}
			if ((mode & ManipulationModes.Scale) != ManipulationModes.None)
			{
				manipulations2D |= Manipulations2D.Scale;
			}
			if ((mode & ManipulationModes.Rotate) != ManipulationModes.None)
			{
				manipulations2D |= Manipulations2D.Rotate;
			}
			return manipulations2D;
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001245 RID: 4677 RVA: 0x00044640 File Offset: 0x00043A40
		// (set) Token: 0x06001246 RID: 4678 RVA: 0x00044654 File Offset: 0x00043A54
		internal ManipulationPivot ManipulationPivot
		{
			get
			{
				return this._pivot;
			}
			set
			{
				this._pivot = value;
				if (this._manipulationProcessor != null)
				{
					this._manipulationProcessor.Pivot = ManipulationLogic.ConvertPivot(value);
				}
			}
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00044684 File Offset: 0x00043A84
		private static ManipulationPivot2D ConvertPivot(ManipulationPivot pivot)
		{
			if (pivot != null)
			{
				Point center = pivot.Center;
				return new ManipulationPivot2D
				{
					X = (float)center.X,
					Y = (float)center.Y,
					Radius = (float)Math.Max(1.0, pivot.Radius)
				};
			}
			return null;
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x000446DC File Offset: 0x00043ADC
		internal void SetManipulationParameters(ManipulationParameters2D parameter)
		{
			if (this._manipulationProcessor != null)
			{
				this._manipulationProcessor.SetParameters(parameter);
			}
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00044700 File Offset: 0x00043B00
		private void UpdateManipulators(ICollection<IManipulator> updatedManipulators)
		{
			this._removedManipulators.Clear();
			Dictionary<int, Manipulator2D> removedManipulators = this._removedManipulators;
			this._removedManipulators = this._currentManipulators;
			this._currentManipulators = removedManipulators;
			UIElement uielement = this._currentContainer as UIElement;
			if (uielement != null)
			{
				if (!uielement.IsVisible)
				{
					return;
				}
			}
			else
			{
				UIElement3D uielement3D = this._currentContainer as UIElement3D;
				if (uielement3D != null && !uielement3D.IsVisible)
				{
					return;
				}
			}
			foreach (IManipulator manipulator in updatedManipulators)
			{
				int id = manipulator.Id;
				this._removedManipulators.Remove(id);
				Point point = manipulator.GetPosition(this._currentContainer);
				point = this._manipulationDevice.GetTransformedManipulatorPosition(point);
				this._currentManipulators[id] = new Manipulator2D(id, (float)point.X, (float)point.Y);
			}
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x000447FC File Offset: 0x00043BFC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void SetContainer(IInputElement newContainer)
		{
			this.UnsubscribeFromLayoutUpdated();
			this._containerPivotPoint = default(Point);
			this._containerSize = default(Size);
			this._root = null;
			this._currentContainer = newContainer;
			if (newContainer != null)
			{
				PresentationSource presentationSource = PresentationSource.CriticalFromVisual((Visual)newContainer);
				if (presentationSource != null)
				{
					this._root = (presentationSource.RootVisual as UIElement);
				}
				if (this._containerLayoutUpdated != null)
				{
					this.SubscribeToLayoutUpdated();
				}
			}
		}

		// Token: 0x14000162 RID: 354
		// (add) Token: 0x0600124B RID: 4683 RVA: 0x00044868 File Offset: 0x00043C68
		// (remove) Token: 0x0600124C RID: 4684 RVA: 0x000448A8 File Offset: 0x00043CA8
		internal event EventHandler<EventArgs> ContainerLayoutUpdated
		{
			add
			{
				bool flag = this._containerLayoutUpdated == null;
				this._containerLayoutUpdated = (EventHandler<EventArgs>)Delegate.Combine(this._containerLayoutUpdated, value);
				if (flag && this._containerLayoutUpdated != null)
				{
					this.SubscribeToLayoutUpdated();
				}
			}
			remove
			{
				bool flag = this._containerLayoutUpdated == null;
				this._containerLayoutUpdated = (EventHandler<EventArgs>)Delegate.Remove(this._containerLayoutUpdated, value);
				if (!flag && this._containerLayoutUpdated == null)
				{
					this.UnsubscribeFromLayoutUpdated();
				}
			}
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x000448E8 File Offset: 0x00043CE8
		private void SubscribeToLayoutUpdated()
		{
			UIElement uielement = this._currentContainer as UIElement;
			if (uielement != null)
			{
				uielement.LayoutUpdated += this.OnLayoutUpdated;
			}
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00044918 File Offset: 0x00043D18
		private void UnsubscribeFromLayoutUpdated()
		{
			UIElement uielement = this._currentContainer as UIElement;
			if (uielement != null)
			{
				uielement.LayoutUpdated -= this.OnLayoutUpdated;
			}
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00044948 File Offset: 0x00043D48
		private void OnLayoutUpdated(object sender, EventArgs e)
		{
			if (this.UpdateCachedPositionAndSize())
			{
				this._containerLayoutUpdated(this, EventArgs.Empty);
			}
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00044970 File Offset: 0x00043D70
		private bool UpdateCachedPositionAndSize()
		{
			if (this._root == null)
			{
				return false;
			}
			UIElement uielement = this._currentContainer as UIElement;
			if (uielement == null)
			{
				return false;
			}
			Size renderSize = uielement.RenderSize;
			Point point = this._root.TranslatePoint(ManipulationLogic.LayoutUpdateDetectionPivotPoint, uielement);
			bool flag = !DoubleUtil.AreClose(renderSize, this._containerSize) || !DoubleUtil.AreClose(point, this._containerPivotPoint);
			if (flag)
			{
				this._containerSize = renderSize;
				this._containerPivotPoint = point;
			}
			return flag;
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x000449E4 File Offset: 0x00043DE4
		private IEnumerable<Manipulator2D> CurrentManipulators
		{
			get
			{
				if (this._currentManipulators.Count <= 0)
				{
					return null;
				}
				return this._currentManipulators.Values;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x00044A0C File Offset: 0x00043E0C
		internal bool IsManipulationActive
		{
			get
			{
				return this._manipulationProcessor != null;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x00044A24 File Offset: 0x00043E24
		private bool IsInertiaActive
		{
			get
			{
				return this._inertiaProcessor != null;
			}
		}

		// Token: 0x040009DD RID: 2525
		private ManipulationDevice _manipulationDevice;

		// Token: 0x040009DE RID: 2526
		private IInputElement _currentContainer;

		// Token: 0x040009DF RID: 2527
		private ManipulationPivot _pivot;

		// Token: 0x040009E0 RID: 2528
		private ManipulationModes _mode;

		// Token: 0x040009E1 RID: 2529
		private ManipulationProcessor2D _manipulationProcessor;

		// Token: 0x040009E2 RID: 2530
		private InertiaProcessor2D _inertiaProcessor;

		// Token: 0x040009E3 RID: 2531
		private Dictionary<int, Manipulator2D> _currentManipulators = new Dictionary<int, Manipulator2D>(2);

		// Token: 0x040009E4 RID: 2532
		private Dictionary<int, Manipulator2D> _removedManipulators = new Dictionary<int, Manipulator2D>(2);

		// Token: 0x040009E5 RID: 2533
		private ManipulationDelta _lastManipulationBeforeInertia;

		// Token: 0x040009E6 RID: 2534
		[SecurityCritical]
		private InputEventArgs _generatedEvent;

		// Token: 0x040009E7 RID: 2535
		private DispatcherTimer _inertiaTimer;

		// Token: 0x040009E8 RID: 2536
		private bool _manualComplete;

		// Token: 0x040009E9 RID: 2537
		private bool _manualCompleteWithInertia;

		// Token: 0x040009EA RID: 2538
		private EventHandler<EventArgs> _containerLayoutUpdated;

		// Token: 0x040009EB RID: 2539
		private static readonly Point LayoutUpdateDetectionPivotPoint = new Point(-10234.1234, -10234.1234);

		// Token: 0x040009EC RID: 2540
		private Point _containerPivotPoint;

		// Token: 0x040009ED RID: 2541
		private Size _containerSize;

		// Token: 0x040009EE RID: 2542
		private UIElement _root;
	}
}
