using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Input.Tracing;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.Interop;
using MS.Internal.PresentationCore;
using MS.Utility;
using MS.Win32;

namespace System.Windows.Input.StylusWisp
{
	// Token: 0x020002E3 RID: 739
	internal class WispLogic : StylusLogic
	{
		// Token: 0x0600166C RID: 5740 RVA: 0x00054524 File Offset: 0x00053924
		[SecurityCritical]
		internal WispLogic(InputManager inputManager)
		{
			base.Statistics.FeaturesUsed |= StylusTraceLogger.FeatureFlags.WispStackEnabled;
			this._inputManager = new SecurityCriticalData<InputManager>(inputManager);
			this._inputManager.Value.PreProcessInput += this.PreProcessInput;
			this._inputManager.Value.PreNotifyInput += this.PreNotifyInput;
			this._inputManager.Value.PostProcessInput += this.PostProcessInput;
			this._overIsEnabledChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnOverIsEnabledChanged);
			this._overIsVisibleChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnOverIsVisibleChanged);
			this._overIsHitTestVisibleChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnOverIsHitTestVisibleChanged);
			this._reevaluateStylusOverDelegate = new DispatcherOperationCallback(this.ReevaluateStylusOverAsync);
			this._reevaluateStylusOverOperation = null;
			this._captureIsEnabledChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnCaptureIsEnabledChanged);
			this._captureIsVisibleChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnCaptureIsVisibleChanged);
			this._captureIsHitTestVisibleChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnCaptureIsHitTestVisibleChanged);
			this._reevaluateCaptureDelegate = new DispatcherOperationCallback(this.ReevaluateCaptureAsync);
			this._reevaluateCaptureOperation = null;
			this._shutdownHandler = new EventHandler(this.OnDispatcherShutdown);
			this._processDisplayChanged = new DispatcherOperationCallback(this.ProcessDisplayChanged);
			this._processDeferredMouseMove = new DispatcherOperationCallback(this.ProcessDeferredMouseMove);
			base.ReadSystemConfig();
			this._dlgInputManagerProcessInput = new DispatcherOperationCallback(this.InputManagerProcessInput);
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00054718 File Offset: 0x00053B18
		[SecurityCritical]
		private void OnDispatcherShutdown(object sender, EventArgs e)
		{
			if (this._shutdownHandler != null)
			{
				this._inputManager.Value.Dispatcher.ShutdownFinished -= this._shutdownHandler;
			}
			if (this._tabletDeviceCollection != null)
			{
				this._tabletDeviceCollection.DisposeTablets();
				this._tabletDeviceCollection = null;
				this._tabletDeviceCollectionDisposed = true;
			}
			this._currentStylusDevice = null;
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x00054770 File Offset: 0x00053B70
		[SecurityCritical]
		internal void ProcessSystemEvent(PenContext penContext, int tabletDeviceId, int stylusDeviceId, int timestamp, SystemGesture systemGesture, int gestureX, int gestureY, int buttonState, PresentationSource inputSource)
		{
			if (systemGesture == SystemGesture.Tap || systemGesture == SystemGesture.RightTap || systemGesture == SystemGesture.Drag || systemGesture == SystemGesture.RightDrag || systemGesture == SystemGesture.HoldEnter || systemGesture == SystemGesture.HoldLeave || systemGesture == SystemGesture.HoverEnter || systemGesture == SystemGesture.HoverLeave || systemGesture == SystemGesture.Flick || systemGesture == (SystemGesture)17 || systemGesture == SystemGesture.None)
			{
				RawStylusSystemGestureInputReport inputReport = new RawStylusSystemGestureInputReport(InputMode.Foreground, timestamp, inputSource, penContext, tabletDeviceId, stylusDeviceId, systemGesture, gestureX, gestureY, buttonState);
				this.ProcessInputReport(inputReport);
			}
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x000547DC File Offset: 0x00053BDC
		[SecurityCritical]
		internal void ProcessInput(RawStylusActions actions, PenContext penContext, int tabletDeviceId, int stylusDeviceId, int[] data, int timestamp, PresentationSource inputSource)
		{
			RawStylusInputReport inputReport = new RawStylusInputReport(InputMode.Foreground, timestamp, inputSource, penContext, actions, tabletDeviceId, stylusDeviceId, data);
			this.ProcessInputReport(inputReport);
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x00054804 File Offset: 0x00053C04
		[SecurityCritical]
		private void CoalesceAndQueueStylusEvent(RawStylusInputReport inputReport)
		{
			StylusDeviceBase stylusDeviceBase;
			if (inputReport == null)
			{
				stylusDeviceBase = null;
			}
			else
			{
				StylusDevice stylusDevice = inputReport.StylusDevice;
				stylusDeviceBase = ((stylusDevice != null) ? stylusDevice.StylusDeviceImpl : null);
			}
			StylusDeviceBase stylusDeviceBase2 = stylusDeviceBase;
			if (stylusDeviceBase2 == null)
			{
				return;
			}
			RawStylusInputReport rawStylusInputReport = null;
			RawStylusInputReport rawStylusInputReport2 = null;
			object coalesceLock = this._coalesceLock;
			lock (coalesceLock)
			{
				this._lastMovesQueued.TryGetValue(stylusDeviceBase2, out rawStylusInputReport);
				this._coalescedMoves.TryGetValue(stylusDeviceBase2, out rawStylusInputReport2);
				if (inputReport.Actions == RawStylusActions.Move)
				{
					if (rawStylusInputReport2 == null)
					{
						this._coalescedMoves[stylusDeviceBase2] = inputReport;
						rawStylusInputReport2 = inputReport;
					}
					else
					{
						int[] rawPacketData = rawStylusInputReport2.GetRawPacketData();
						int[] rawPacketData2 = inputReport.GetRawPacketData();
						int[] array = new int[rawPacketData.Length + rawPacketData2.Length];
						rawPacketData.CopyTo(array, 0);
						rawPacketData2.CopyTo(array, rawPacketData.Length);
						rawStylusInputReport2 = new RawStylusInputReport(rawStylusInputReport2.Mode, rawStylusInputReport2.Timestamp, rawStylusInputReport2.InputSource, rawStylusInputReport2.PenContext, rawStylusInputReport2.Actions, rawStylusInputReport2.TabletDeviceId, rawStylusInputReport2.StylusDeviceId, array);
						rawStylusInputReport2.StylusDevice = stylusDeviceBase2.StylusDevice;
						this._coalescedMoves[stylusDeviceBase2] = rawStylusInputReport2;
					}
					if (rawStylusInputReport != null && rawStylusInputReport.IsQueued)
					{
						return;
					}
				}
				if (rawStylusInputReport2 != null)
				{
					this.QueueStylusEvent(rawStylusInputReport2);
					this._lastMovesQueued[stylusDeviceBase2] = rawStylusInputReport2;
					this._coalescedMoves.Remove(stylusDeviceBase2);
				}
				if (inputReport.Actions != RawStylusActions.Move)
				{
					this.QueueStylusEvent(inputReport);
					this._lastMovesQueued.Remove(stylusDeviceBase2);
				}
			}
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x00054980 File Offset: 0x00053D80
		[SecurityCritical]
		private void ProcessInputReport(RawStylusInputReport inputReport)
		{
			WispStylusDevice wispStylusDevice = this.FindStylusDeviceWithLock(inputReport.StylusDeviceId);
			inputReport.StylusDevice = ((wispStylusDevice != null) ? wispStylusDevice.StylusDevice : null);
			if (!this._inDragDrop || !inputReport.PenContext.Contexts.IsWindowDisabled)
			{
				this.InvokeStylusPluginCollection(inputReport);
			}
			this.CoalesceAndQueueStylusEvent(inputReport);
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x000549D4 File Offset: 0x00053DD4
		[SecurityCritical]
		private void QueueStylusEvent(RawStylusInputReport report)
		{
			EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordInput, EventTrace.Level.Info, EventTrace.Event.StylusEventQueued, report.StylusDeviceId);
			report.IsQueued = true;
			object stylusEventQueueLock = this._stylusEventQueueLock;
			lock (stylusEventQueueLock)
			{
				if (report.StylusDevice != null)
				{
					WispTabletDevice wispTabletDevice = report.StylusDevice.TabletDevice.As<WispTabletDevice>();
					if (wispTabletDevice != null)
					{
						WispTabletDevice wispTabletDevice2 = wispTabletDevice;
						int queuedEventCount = wispTabletDevice2.QueuedEventCount;
						wispTabletDevice2.QueuedEventCount = queuedEventCount + 1;
					}
				}
				this._queueStylusEvents.Enqueue(report);
			}
			base.Dispatcher.BeginInvoke(DispatcherPriority.Input, this._dlgInputManagerProcessInput, null);
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x00054A88 File Offset: 0x00053E88
		[SecurityCritical]
		internal object InputManagerProcessInput(object oInput)
		{
			RawStylusInputReport rawStylusInputReport = null;
			WispTabletDevice wispTabletDevice = null;
			object stylusEventQueueLock = this._stylusEventQueueLock;
			lock (stylusEventQueueLock)
			{
				if (this._queueStylusEvents.Count > 0)
				{
					rawStylusInputReport = this._queueStylusEvents.Dequeue();
					WispTabletDevice wispTabletDevice2;
					if (rawStylusInputReport == null)
					{
						wispTabletDevice2 = null;
					}
					else
					{
						StylusDevice stylusDevice = rawStylusInputReport.StylusDevice;
						if (stylusDevice == null)
						{
							wispTabletDevice2 = null;
						}
						else
						{
							TabletDevice tabletDevice = stylusDevice.TabletDevice;
							wispTabletDevice2 = ((tabletDevice != null) ? tabletDevice.As<WispTabletDevice>() : null);
						}
					}
					wispTabletDevice = wispTabletDevice2;
					if (wispTabletDevice != null)
					{
						WispTabletDevice wispTabletDevice3 = wispTabletDevice;
						int queuedEventCount = wispTabletDevice3.QueuedEventCount;
						wispTabletDevice3.QueuedEventCount = queuedEventCount - 1;
					}
				}
			}
			if (rawStylusInputReport != null && rawStylusInputReport.StylusDevice != null && rawStylusInputReport.StylusDevice.IsValid)
			{
				rawStylusInputReport.IsQueued = false;
				PenContext penContext = rawStylusInputReport.PenContext;
				if (wispTabletDevice != null && penContext.UpdateScreenMeasurementsPending)
				{
					bool flag2 = wispTabletDevice.AreSizeDeltasValid();
					penContext.UpdateScreenMeasurementsPending = false;
					wispTabletDevice.UpdateScreenMeasurements();
					if (flag2)
					{
						wispTabletDevice.UpdateSizeDeltas(penContext.StylusPointDescription, this);
					}
				}
				InputReportEventArgs inputReportEventArgs = new InputReportEventArgs(null, rawStylusInputReport);
				inputReportEventArgs.RoutedEvent = InputManager.PreviewInputReportEvent;
				this._processingQueuedEvent = true;
				try
				{
					this.InputManagerProcessInputEventArgs(inputReportEventArgs);
				}
				finally
				{
					this._processingQueuedEvent = false;
				}
			}
			return null;
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x00054BCC File Offset: 0x00053FCC
		[SecurityCritical]
		internal void InputManagerProcessInputEventArgs(InputEventArgs input)
		{
			this._inputManager.Value.ProcessInput(input);
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x00054BEC File Offset: 0x00053FEC
		[SecurityCritical]
		private bool DeferMouseMove(RawMouseInputReport mouseInputReport)
		{
			if (this._triedDeferringMouseMove)
			{
				return false;
			}
			if (this._deferredMouseMove != null)
			{
				return false;
			}
			this._deferredMouseMove = mouseInputReport;
			base.Dispatcher.BeginInvoke(DispatcherPriority.Background, this._processDeferredMouseMove, null);
			return true;
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x00054C2C File Offset: 0x0005402C
		[SecuritySafeCritical]
		internal object ProcessDeferredMouseMove(object oInput)
		{
			if (this._deferredMouseMove != null)
			{
				if (this.CurrentStylusDevice == null || !this.CurrentStylusDevice.InRange)
				{
					this.SendDeferredMouseEvent(true);
				}
				else
				{
					this.SendDeferredMouseEvent(false);
				}
			}
			return null;
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x00054C68 File Offset: 0x00054068
		[SecurityCritical]
		private void SendDeferredMouseEvent(bool sendInput)
		{
			if (sendInput)
			{
				this._triedDeferringMouseMove = true;
				if (this._deferredMouseMove != null && this._deferredMouseMove.InputSource != null && this._deferredMouseMove.InputSource.CompositionTarget != null && !this._deferredMouseMove.InputSource.CompositionTarget.IsDisposed)
				{
					InputReportEventArgs inputReportEventArgs = new InputReportEventArgs(this._inputManager.Value.PrimaryMouseDevice, this._deferredMouseMove);
					inputReportEventArgs.RoutedEvent = InputManager.PreviewInputReportEvent;
					this._deferredMouseMove = null;
					this._inputManager.Value.ProcessInput(inputReportEventArgs);
				}
			}
			this._deferredMouseMove = null;
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x00054D0C File Offset: 0x0005410C
		[SecurityCritical]
		private void PreProcessInput(object sender, PreProcessInputEventArgs e)
		{
			if (this._inputEnabled && e.StagingItem.Input.RoutedEvent == InputManager.PreviewInputReportEvent)
			{
				InputReportEventArgs inputReportEventArgs = e.StagingItem.Input as InputReportEventArgs;
				if (inputReportEventArgs != null && !inputReportEventArgs.Handled)
				{
					if (this._inDragDrop != this._inputManager.Value.InDragDrop)
					{
						this._inDragDrop = this._inputManager.Value.InDragDrop;
						if (!this._inDragDrop && this._stylusDeviceInRange)
						{
							this.UpdateMouseState();
							this._leavingDragDrop = true;
						}
					}
					if (inputReportEventArgs.Report.Type == InputType.Mouse)
					{
						if (!(inputReportEventArgs.Device is StylusDevice))
						{
							if (this._tabletDeviceCollectionDisposed || this.TabletDevices.Count == 0)
							{
								this._lastMouseMoveFromStylus = false;
								return;
							}
							RawMouseInputReport rawMouseInputReport = (RawMouseInputReport)inputReportEventArgs.Report;
							RawMouseActions actions = rawMouseInputReport.Actions;
							int num = NativeMethods.IntPtrToInt32(rawMouseInputReport.ExtraInformation);
							bool flag = StylusLogic.IsPromotedMouseEvent(rawMouseInputReport);
							if (flag)
							{
								this._lastMouseMoveFromStylus = true;
								this._lastStylusDeviceId = (num & 255);
							}
							if ((actions & RawMouseActions.Deactivate) == RawMouseActions.Deactivate)
							{
								this._seenRealMouseActivate = false;
								if (this.CurrentStylusDevice != null)
								{
									PenContexts penContextsFromHwnd = this.GetPenContextsFromHwnd(rawMouseInputReport.InputSource);
									if (this._stylusDeviceInRange && !this._inDragDrop && (penContextsFromHwnd == null || !penContextsFromHwnd.IsWindowDisabled))
									{
										this._mouseDeactivateInputReport = rawMouseInputReport;
										e.Cancel();
										inputReportEventArgs.Handled = true;
										return;
									}
									if (this.CurrentStylusDevice.DirectlyOver != null)
									{
										MouseDevice primaryMouseDevice = this._inputManager.Value.PrimaryMouseDevice;
										if (primaryMouseDevice.CriticalActiveSource == rawMouseInputReport.InputSource)
										{
											this._currentStylusDevice.ChangeStylusOver(null);
											return;
										}
									}
								}
							}
							else if ((actions & RawMouseActions.CancelCapture) != RawMouseActions.None)
							{
								if (this.CurrentStylusDevice != null && this.CurrentStylusDevice.InRange)
								{
									RawMouseInputReport report = new RawMouseInputReport(rawMouseInputReport.Mode, rawMouseInputReport.Timestamp, rawMouseInputReport.InputSource, rawMouseInputReport.Actions, 0, 0, 0, IntPtr.Zero);
									InputReportEventArgs inputReportEventArgs2 = new InputReportEventArgs(this.CurrentStylusDevice.StylusDevice, report);
									inputReportEventArgs2.RoutedEvent = InputManager.PreviewInputReportEvent;
									e.Cancel();
									this._inputManager.Value.ProcessInput(inputReportEventArgs2);
									return;
								}
							}
							else if ((actions & RawMouseActions.Activate) != RawMouseActions.None)
							{
								this._mouseDeactivateInputReport = null;
								WispStylusDevice wispStylusDevice = null;
								this._seenRealMouseActivate = true;
								if (this.CurrentStylusDevice != null && this.CurrentStylusDevice.InRange)
								{
									wispStylusDevice = this._currentStylusDevice;
								}
								else if (flag || this.ShouldConsiderStylusInRange(rawMouseInputReport))
								{
									wispStylusDevice = this.FindStylusDevice(this._lastStylusDeviceId);
								}
								if (wispStylusDevice != null)
								{
									if (rawMouseInputReport.InputSource != this._inputManager.Value.PrimaryMouseDevice.CriticalActiveSource)
									{
										Point pointScreen = wispStylusDevice.LastMouseScreenPoint;
										pointScreen = PointUtil.ScreenToClient(pointScreen, rawMouseInputReport.InputSource);
										RawMouseInputReport report2 = new RawMouseInputReport(rawMouseInputReport.Mode, rawMouseInputReport.Timestamp, rawMouseInputReport.InputSource, RawMouseActions.Activate, (int)pointScreen.X, (int)pointScreen.Y, rawMouseInputReport.Wheel, rawMouseInputReport.ExtraInformation);
										InputReportEventArgs inputReportEventArgs3 = new InputReportEventArgs(wispStylusDevice.StylusDevice, report2);
										inputReportEventArgs3.RoutedEvent = InputManager.PreviewInputReportEvent;
										this._inputManager.Value.ProcessInput(inputReportEventArgs3);
									}
									e.Cancel();
									return;
								}
							}
							else if ((actions & (RawMouseActions.AbsoluteMove | RawMouseActions.Button1Press | RawMouseActions.Button1Release | RawMouseActions.Button2Press | RawMouseActions.Button2Release | RawMouseActions.QueryCursor)) != RawMouseActions.None)
							{
								if ((actions & RawMouseActions.Button1Press) != RawMouseActions.None && this.CurrentStylusDevice != null && !this.CurrentStylusDevice.InAir)
								{
									HwndSource hwndSource = rawMouseInputReport.InputSource as HwndSource;
									IntPtr intPtr = (hwndSource != null) ? hwndSource.CriticalHandle : IntPtr.Zero;
									if (intPtr != IntPtr.Zero && this._inputManager.Value.PrimaryMouseDevice.Captured != null && UnsafeNativeMethods.GetParent(new HandleRef(this, intPtr)) == IntPtr.Zero && intPtr != UnsafeNativeMethods.GetForegroundWindow())
									{
										int windowLong = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, intPtr), -20);
										if ((windowLong & 134217728) == 0)
										{
											UnsafeNativeMethods.SetForegroundWindow(new HandleRef(this, hwndSource.Handle));
										}
									}
									if (!this._currentStylusDevice.SentMouseDown && flag && this.ShouldPromoteToMouse(this._currentStylusDevice))
									{
										WispStylusTouchDevice touchDevice = this._currentStylusDevice.TouchDevice;
										if (touchDevice.PromotingToManipulation)
										{
											touchDevice.StoredStagingAreaItems.AddItem(e.StagingItem);
										}
										else if (touchDevice.PromotingToOther)
										{
											this._currentStylusDevice.PlayBackCachedDownInputReport(rawMouseInputReport.Timestamp);
										}
									}
								}
								if (flag)
								{
									bool flag2 = true;
									Point ptClient = new Point((double)rawMouseInputReport.X, (double)rawMouseInputReport.Y);
									if ((this.CurrentStylusDevice == null || this.CurrentStylusDevice.InAir) && Mouse.Captured != null && !WispLogic.InWindowClientRect(ptClient, rawMouseInputReport.InputSource))
									{
										flag2 = false;
									}
									if (flag2)
									{
										if ((actions & (RawMouseActions.Button1Press | RawMouseActions.Button2Press)) == RawMouseActions.None)
										{
											inputReportEventArgs.Handled = true;
										}
										e.Cancel();
										if ((actions & RawMouseActions.Button1Press) != RawMouseActions.None && this.CurrentStylusDevice != null && this.CurrentStylusDevice.InAir)
										{
											this._currentStylusDevice.SetSawMouseButton1Down(true);
										}
										if (!this._processingQueuedEvent)
										{
											this.InputManagerProcessInput(null);
											return;
										}
									}
								}
								else
								{
									bool flag3 = false;
									bool flag4 = true;
									if (this._stylusDeviceInRange)
									{
										flag3 = true;
										if ((actions & (RawMouseActions.Button1Press | RawMouseActions.Button2Press)) == RawMouseActions.None)
										{
											flag4 = false;
										}
									}
									else if ((actions & ~(RawMouseActions.AbsoluteMove | RawMouseActions.QueryCursor)) == RawMouseActions.None)
									{
										if (this.DeferMouseMove(rawMouseInputReport))
										{
											flag3 = true;
										}
										else if (this._lastMouseMoveFromStylus && this.ShouldConsiderStylusInRange(rawMouseInputReport))
										{
											this.SendDeferredMouseEvent(false);
											flag3 = true;
										}
										else
										{
											this._lastMouseMoveFromStylus = false;
											if (!this._triedDeferringMouseMove)
											{
												this.SendDeferredMouseEvent(true);
											}
											if (this.CurrentStylusDevice != null)
											{
												this.SelectStylusDevice(null, null, true);
											}
										}
									}
									else
									{
										this._lastMouseMoveFromStylus = false;
										this.SendDeferredMouseEvent(true);
										if (this.CurrentStylusDevice != null)
										{
											this.SelectStylusDevice(null, null, true);
										}
									}
									if (flag3)
									{
										e.Cancel();
										if (flag4)
										{
											inputReportEventArgs.Handled = true;
											return;
										}
									}
								}
							}
							else
							{
								if (this._stylusDeviceInRange)
								{
									this.SendDeferredMouseEvent(true);
									return;
								}
								this._lastMouseMoveFromStylus = false;
								this.SendDeferredMouseEvent(true);
								if (this.CurrentStylusDevice != null)
								{
									this.SelectStylusDevice(null, null, true);
									return;
								}
							}
						}
						else
						{
							this._lastMouseMoveFromStylus = true;
							RawMouseInputReport rawMouseInputReport2 = (RawMouseInputReport)inputReportEventArgs.Report;
							StylusDevice stylusDevice = (StylusDevice)inputReportEventArgs.Device;
							if (!stylusDevice.InRange && rawMouseInputReport2._isSynchronize)
							{
								e.Cancel();
								inputReportEventArgs.Handled = true;
								return;
							}
						}
					}
					else if (inputReportEventArgs.Report.Type == InputType.Stylus)
					{
						RawStylusInputReport rawStylusInputReport = (RawStylusInputReport)inputReportEventArgs.Report;
						WispStylusDevice wispStylusDevice2;
						if (rawStylusInputReport == null)
						{
							wispStylusDevice2 = null;
						}
						else
						{
							StylusDevice stylusDevice2 = rawStylusInputReport.StylusDevice;
							wispStylusDevice2 = ((stylusDevice2 != null) ? stylusDevice2.As<WispStylusDevice>() : null);
						}
						WispStylusDevice wispStylusDevice3 = wispStylusDevice2;
						bool flag5 = true;
						if (rawStylusInputReport.InputSource != null && rawStylusInputReport.PenContext != null)
						{
							if (wispStylusDevice3 == null)
							{
								wispStylusDevice3 = this.FindStylusDevice(rawStylusInputReport.StylusDeviceId);
								if (wispStylusDevice3 == null)
								{
									wispStylusDevice3 = this.WispTabletDevices.UpdateStylusDevices(rawStylusInputReport.TabletDeviceId, rawStylusInputReport.StylusDeviceId);
								}
								rawStylusInputReport.StylusDevice = wispStylusDevice3.StylusDevice;
							}
							this._triedDeferringMouseMove = false;
							if (rawStylusInputReport.Actions == RawStylusActions.InRange && rawStylusInputReport.Data == null)
							{
								rawStylusInputReport.PenContext.DecrementQueuedInRangeCount();
								e.Cancel();
								inputReportEventArgs.Handled = true;
								this._lastInRangeTime = Environment.TickCount;
								return;
							}
							if (rawStylusInputReport.Actions == RawStylusActions.SystemGesture && wispStylusDevice3 != null)
							{
								RawStylusSystemGestureInputReport rawStylusSystemGestureInputReport = (RawStylusSystemGestureInputReport)rawStylusInputReport;
								if (rawStylusSystemGestureInputReport.SystemGesture == (SystemGesture)17)
								{
									wispStylusDevice3.SeenDoubleTapGesture = true;
									e.Cancel();
									inputReportEventArgs.Handled = true;
									return;
								}
							}
							if (wispStylusDevice3 != null && this.IsValidStylusAction(rawStylusInputReport))
							{
								flag5 = false;
								TabletDevice tabletDevice = wispStylusDevice3.TabletDevice;
								WispTabletDevice wispTabletDevice = (tabletDevice != null) ? tabletDevice.As<WispTabletDevice>() : null;
								if (wispTabletDevice != null)
								{
									SystemGesture? systemGesture = wispTabletDevice.GenerateStaticGesture(rawStylusInputReport);
									if (systemGesture != null)
									{
										this.GenerateGesture(rawStylusInputReport, systemGesture.Value);
									}
								}
								if (rawStylusInputReport.Actions == RawStylusActions.Up)
								{
									if (!wispStylusDevice3.GestureWasFired)
									{
										this.GenerateGesture(rawStylusInputReport, wispStylusDevice3.LastTapBarrelDown ? SystemGesture.RightTap : SystemGesture.Tap);
									}
									if (!this._inDragDrop && !rawStylusInputReport.PenContext.Contexts.IsWindowDisabled)
									{
										this.ProcessMouseMove(wispStylusDevice3, rawStylusInputReport.Timestamp, false);
									}
								}
								inputReportEventArgs.Device = wispStylusDevice3.StylusDevice;
							}
						}
						if (flag5)
						{
							e.Cancel();
						}
					}
				}
			}
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x00055520 File Offset: 0x00054920
		[SecurityCritical]
		private void PreNotifyInput(object sender, NotifyInputEventArgs e)
		{
			if (e.StagingItem.Input.RoutedEvent == InputManager.PreviewInputReportEvent)
			{
				InputReportEventArgs inputReportEventArgs = e.StagingItem.Input as InputReportEventArgs;
				if (!inputReportEventArgs.Handled && inputReportEventArgs.Report.Type == InputType.Stylus)
				{
					RawStylusInputReport rawStylusInputReport = (RawStylusInputReport)inputReportEventArgs.Report;
					StylusDevice stylusDevice = rawStylusInputReport.StylusDevice;
					WispStylusDevice wispStylusDevice = (stylusDevice != null) ? stylusDevice.As<WispStylusDevice>() : null;
					if (wispStylusDevice != null && wispStylusDevice.IsValid)
					{
						RawStylusActions actions = rawStylusInputReport.Actions;
						if (actions != RawStylusActions.InRange)
						{
							if (actions != RawStylusActions.OutOfRange)
							{
								if (actions == RawStylusActions.SystemGesture)
								{
									wispStylusDevice.UpdateStateForSystemGesture((RawStylusSystemGestureInputReport)rawStylusInputReport);
								}
								else
								{
									wispStylusDevice.UpdateState(rawStylusInputReport);
								}
							}
							else
							{
								this._lastInRangeTime = Environment.TickCount;
								wispStylusDevice.UpdateInRange(false, rawStylusInputReport.PenContext);
								this.UpdateIsStylusInRange(false);
							}
						}
						else
						{
							this._lastInRangeTime = Environment.TickCount;
							wispStylusDevice.UpdateInRange(true, rawStylusInputReport.PenContext);
							wispStylusDevice.UpdateState(rawStylusInputReport);
							this.UpdateIsStylusInRange(true);
						}
						if (!this._inDragDrop && !rawStylusInputReport.PenContext.Contexts.IsWindowDisabled && !wispStylusDevice.IgnoreStroke)
						{
							Point point = wispStylusDevice.GetRawPosition(null);
							point = this.DeviceUnitsFromMeasureUnits(point);
							IInputElement newOver = wispStylusDevice.FindTarget(wispStylusDevice.CriticalActiveSource, point);
							this.SelectStylusDevice(wispStylusDevice, newOver, true);
						}
						else
						{
							this.SelectStylusDevice(wispStylusDevice, null, false);
						}
						if (rawStylusInputReport.Actions == RawStylusActions.Down && wispStylusDevice.Target == null)
						{
							wispStylusDevice.IgnoreStroke = true;
						}
						this._inputManager.Value.MostRecentInputDevice = wispStylusDevice.StylusDevice;
						this.VerifyStylusPlugInCollectionTarget(rawStylusInputReport);
					}
				}
			}
			if (e.StagingItem.Input.RoutedEvent == Stylus.PreviewStylusDownEvent)
			{
				StylusEventArgs stylusEventArgs = e.StagingItem.Input as StylusDownEventArgs;
				WispStylusDevice wispStylusDevice2 = stylusEventArgs.StylusDeviceImpl.As<WispStylusDevice>();
				if (wispStylusDevice2 != null && wispStylusDevice2.IsValid)
				{
					Point rawPosition = wispStylusDevice2.GetRawPosition(null);
					WispTabletDevice wispTabletDevice = wispStylusDevice2.TabletDevice.As<WispTabletDevice>();
					bool flag = false;
					int buttonBitPosition = wispTabletDevice.StylusPointDescription.GetButtonBitPosition(StylusPointProperties.BarrelButton);
					if (buttonBitPosition != -1 && wispStylusDevice2.StylusButtons[buttonBitPosition].StylusButtonState == StylusButtonState.Down)
					{
						flag = true;
					}
					Point point2 = this.DeviceUnitsFromMeasureUnits(rawPosition);
					Point point3 = this.DeviceUnitsFromMeasureUnits(wispStylusDevice2.LastTapPoint);
					int num = Math.Abs(stylusEventArgs.Timestamp - wispStylusDevice2.LastTapTime);
					Size doubleTapSize = wispTabletDevice.DoubleTapSize;
					bool flag2 = Math.Abs(point2.X - point3.X) < doubleTapSize.Width && Math.Abs(point2.Y - point3.Y) < doubleTapSize.Height;
					if (num < this.DoubleTapDeltaTime && flag2 && flag == wispStylusDevice2.LastTapBarrelDown)
					{
						WispStylusDevice wispStylusDevice3 = wispStylusDevice2;
						int tapCount = wispStylusDevice3.TapCount;
						wispStylusDevice3.TapCount = tapCount + 1;
					}
					else
					{
						wispStylusDevice2.TapCount = 1;
						wispStylusDevice2.LastTapPoint = new Point(rawPosition.X, rawPosition.Y);
						wispStylusDevice2.LastTapTime = stylusEventArgs.Timestamp;
						wispStylusDevice2.LastTapBarrelDown = flag;
					}
					this.ProcessMouseMove(wispStylusDevice2, stylusEventArgs.Timestamp, true);
				}
			}
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x0005582C File Offset: 0x00054C2C
		[SecurityCritical]
		private void PostProcessInput(object sender, ProcessInputEventArgs e)
		{
			if (this._inputEnabled && (e.StagingItem.Input.RoutedEvent == Mouse.LostMouseCaptureEvent || e.StagingItem.Input.RoutedEvent == Mouse.GotMouseCaptureEvent))
			{
				foreach (object obj in ((IEnumerable)this.TabletDevices))
				{
					TabletDevice tabletDevice = (TabletDevice)obj;
					foreach (StylusDevice stylusDevice in tabletDevice.StylusDevices)
					{
						stylusDevice.Capture(Mouse.Captured, Mouse.CapturedMode);
					}
				}
			}
			if (e.StagingItem.Input.RoutedEvent == InputManager.InputReportEvent)
			{
				InputReportEventArgs inputReportEventArgs = e.StagingItem.Input as InputReportEventArgs;
				if (!inputReportEventArgs.Handled && inputReportEventArgs.Report.Type == InputType.Stylus)
				{
					RawStylusInputReport rawStylusInputReport = (RawStylusInputReport)inputReportEventArgs.Report;
					WispStylusDevice wispStylusDevice = rawStylusInputReport.StylusDevice.As<WispStylusDevice>();
					if (!this._inDragDrop)
					{
						if (!rawStylusInputReport.PenContext.Contexts.IsWindowDisabled)
						{
							this.PromoteRawToPreview(rawStylusInputReport, e);
							if (rawStylusInputReport.Actions == RawStylusActions.Up)
							{
								wispStylusDevice.IgnoreStroke = false;
							}
						}
						else if ((rawStylusInputReport.Actions & RawStylusActions.Up) != RawStylusActions.None && wispStylusDevice != null)
						{
							wispStylusDevice.ResetStateForStylusUp();
							WispStylusTouchDevice touchDevice = wispStylusDevice.TouchDevice;
							if (touchDevice.IsActive)
							{
								touchDevice.OnDeactivate();
							}
						}
					}
					else if (wispStylusDevice != null && wispStylusDevice != this.CurrentMousePromotionStylusDevice && (rawStylusInputReport.Actions & RawStylusActions.Up) != RawStylusActions.None)
					{
						WispStylusTouchDevice touchDevice2 = wispStylusDevice.TouchDevice;
						if (touchDevice2.IsActive)
						{
							touchDevice2.OnDeactivate();
						}
					}
				}
			}
			if (e.StagingItem.Input.RoutedEvent == Stylus.StylusOutOfRangeEvent)
			{
				RawMouseInputReport mouseDeactivateInputReport = this._mouseDeactivateInputReport;
				this._mouseDeactivateInputReport = null;
				StylusEventArgs stylusEventArgs = (StylusEventArgs)e.StagingItem.Input;
				PresentationSource criticalActiveSource = this._inputManager.Value.PrimaryMouseDevice.CriticalActiveSource;
				if (mouseDeactivateInputReport != null || (!this._seenRealMouseActivate && criticalActiveSource != null))
				{
					WispStylusDevice wispStylusDevice2 = stylusEventArgs.StylusDeviceImpl.As<WispStylusDevice>();
					wispStylusDevice2.ChangeStylusOver(null);
					RawMouseInputReport report = (mouseDeactivateInputReport != null) ? new RawMouseInputReport(mouseDeactivateInputReport.Mode, stylusEventArgs.Timestamp, mouseDeactivateInputReport.InputSource, mouseDeactivateInputReport.Actions, mouseDeactivateInputReport.X, mouseDeactivateInputReport.Y, mouseDeactivateInputReport.Wheel, mouseDeactivateInputReport.ExtraInformation) : new RawMouseInputReport(InputMode.Foreground, stylusEventArgs.Timestamp, criticalActiveSource, RawMouseActions.Deactivate, 0, 0, 0, IntPtr.Zero);
					InputReportEventArgs inputReportEventArgs2 = new InputReportEventArgs(wispStylusDevice2.StylusDevice, report);
					inputReportEventArgs2.RoutedEvent = InputManager.PreviewInputReportEvent;
					this._inputManager.Value.ProcessInput(inputReportEventArgs2);
				}
			}
			this.CallPlugInsForMouse(e);
			this.PromotePreviewToMain(e);
			this.UpdateButtonStates(e);
			this.PromoteMainToOther(e);
			if (e.StagingItem.Input.RoutedEvent == Stylus.StylusMoveEvent)
			{
				StylusEventArgs stylusEventArgs2 = (StylusEventArgs)e.StagingItem.Input;
				WispStylusDevice wispStylusDevice3 = stylusEventArgs2.StylusDeviceImpl.As<WispStylusDevice>();
				if (wispStylusDevice3.SeenDoubleTapGesture && !wispStylusDevice3.GestureWasFired && wispStylusDevice3.DetectedDrag)
				{
					this.GenerateGesture(stylusEventArgs2.InputReport, SystemGesture.Drag);
				}
			}
			if (e.StagingItem.Input.RoutedEvent == Stylus.StylusSystemGestureEvent)
			{
				StylusSystemGestureEventArgs stylusSystemGestureEventArgs = (StylusSystemGestureEventArgs)e.StagingItem.Input;
				if (stylusSystemGestureEventArgs.SystemGesture == SystemGesture.Flick)
				{
					base.HandleFlick(stylusSystemGestureEventArgs.ButtonState, stylusSystemGestureEventArgs.StylusDevice.DirectlyOver);
				}
			}
			if (e.StagingItem.Input.RoutedEvent == Stylus.StylusOutOfRangeEvent)
			{
				StylusEventArgs stylusEventArgs3 = e.StagingItem.Input as StylusEventArgs;
				WispTabletDevice wispTabletDevice;
				if (stylusEventArgs3 == null)
				{
					wispTabletDevice = null;
				}
				else
				{
					StylusDeviceBase stylusDeviceImpl = stylusEventArgs3.StylusDeviceImpl;
					wispTabletDevice = ((stylusDeviceImpl != null) ? stylusDeviceImpl.TabletDevice.As<WispTabletDevice>() : null);
				}
				WispTabletDevice wispTabletDevice2 = wispTabletDevice;
				if (wispTabletDevice2.IsDisposalPending && wispTabletDevice2.CanDispose)
				{
					this.RefreshTablets();
				}
			}
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x00055C44 File Offset: 0x00055044
		[SecurityCritical]
		private void PromoteRawToPreview(RawStylusInputReport report, ProcessInputEventArgs e)
		{
			RoutedEvent previewEventFromRawStylusActions = StylusLogic.GetPreviewEventFromRawStylusActions(report.Actions);
			if (previewEventFromRawStylusActions != null && report.StylusDevice != null && !report.StylusDevice.As<WispStylusDevice>().IgnoreStroke)
			{
				StylusEventArgs stylusEventArgs;
				if (previewEventFromRawStylusActions != Stylus.PreviewStylusSystemGestureEvent)
				{
					if (previewEventFromRawStylusActions == Stylus.PreviewStylusDownEvent)
					{
						stylusEventArgs = new StylusDownEventArgs(report.StylusDevice, report.Timestamp);
					}
					else
					{
						stylusEventArgs = new StylusEventArgs(report.StylusDevice, report.Timestamp);
					}
				}
				else
				{
					RawStylusSystemGestureInputReport rawStylusSystemGestureInputReport = (RawStylusSystemGestureInputReport)report;
					stylusEventArgs = new StylusSystemGestureEventArgs(report.StylusDevice, report.Timestamp, rawStylusSystemGestureInputReport.SystemGesture, rawStylusSystemGestureInputReport.GestureX, rawStylusSystemGestureInputReport.GestureY, rawStylusSystemGestureInputReport.ButtonState);
				}
				stylusEventArgs.InputReport = report;
				stylusEventArgs.RoutedEvent = previewEventFromRawStylusActions;
				e.PushInput(stylusEventArgs, e.StagingItem);
			}
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x00055D08 File Offset: 0x00055108
		[SecurityCritical]
		private void PromotePreviewToMain(ProcessInputEventArgs e)
		{
			if (!e.StagingItem.Input.Handled)
			{
				RoutedEvent mainEventFromPreviewEvent = StylusLogic.GetMainEventFromPreviewEvent(e.StagingItem.Input.RoutedEvent);
				if (mainEventFromPreviewEvent != null)
				{
					StylusEventArgs stylusEventArgs = (StylusEventArgs)e.StagingItem.Input;
					StylusDevice stylusDevice = stylusEventArgs.InputReport.StylusDevice;
					StylusEventArgs stylusEventArgs2;
					if (mainEventFromPreviewEvent == Stylus.StylusDownEvent || mainEventFromPreviewEvent == Stylus.PreviewStylusDownEvent)
					{
						StylusDownEventArgs stylusDownEventArgs = (StylusDownEventArgs)stylusEventArgs;
						stylusEventArgs2 = new StylusDownEventArgs(stylusDevice, stylusEventArgs.Timestamp);
					}
					else if (mainEventFromPreviewEvent == Stylus.StylusButtonDownEvent || mainEventFromPreviewEvent == Stylus.StylusButtonUpEvent)
					{
						StylusButtonEventArgs stylusButtonEventArgs = (StylusButtonEventArgs)stylusEventArgs;
						stylusEventArgs2 = new StylusButtonEventArgs(stylusDevice, stylusEventArgs.Timestamp, stylusButtonEventArgs.StylusButton);
					}
					else if (mainEventFromPreviewEvent != Stylus.StylusSystemGestureEvent)
					{
						stylusEventArgs2 = new StylusEventArgs(stylusDevice, stylusEventArgs.Timestamp);
					}
					else
					{
						StylusSystemGestureEventArgs stylusSystemGestureEventArgs = (StylusSystemGestureEventArgs)stylusEventArgs;
						stylusEventArgs2 = new StylusSystemGestureEventArgs(stylusDevice, stylusSystemGestureEventArgs.Timestamp, stylusSystemGestureEventArgs.SystemGesture, stylusSystemGestureEventArgs.GestureX, stylusSystemGestureEventArgs.GestureY, stylusSystemGestureEventArgs.ButtonState);
					}
					stylusEventArgs2.InputReport = stylusEventArgs.InputReport;
					stylusEventArgs2.RoutedEvent = mainEventFromPreviewEvent;
					e.PushInput(stylusEventArgs2, e.StagingItem);
					return;
				}
			}
			else
			{
				StylusEventArgs stylusEventArgs3 = e.StagingItem.Input as StylusEventArgs;
				if (((stylusEventArgs3 != null) ? stylusEventArgs3.RoutedEvent : null) == Stylus.PreviewStylusUpEvent && stylusEventArgs3.StylusDeviceImpl.As<WispStylusDevice>().TouchDevice.IsActive)
				{
					stylusEventArgs3.StylusDeviceImpl.As<WispStylusDevice>().TouchDevice.OnDeactivate();
				}
			}
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x00055E7C File Offset: 0x0005527C
		[SecurityCritical]
		private void PromoteMainToOther(ProcessInputEventArgs e)
		{
			StagingAreaInputItem stagingItem = e.StagingItem;
			StylusEventArgs stylusEventArgs = stagingItem.Input as StylusEventArgs;
			if (stylusEventArgs == null)
			{
				return;
			}
			WispStylusDevice wispStylusDevice = stylusEventArgs.StylusDeviceImpl.As<WispStylusDevice>();
			WispStylusTouchDevice touchDevice = wispStylusDevice.TouchDevice;
			bool flag = this.ShouldPromoteToMouse(wispStylusDevice);
			if (WispLogic.IsTouchPromotionEvent(stylusEventArgs))
			{
				if (!e.StagingItem.Input.Handled)
				{
					this.PromoteMainToTouch(e, stylusEventArgs);
					return;
				}
				if (stylusEventArgs.RoutedEvent == Stylus.StylusUpEvent && touchDevice.IsActive)
				{
					touchDevice.OnDeactivate();
					return;
				}
			}
			else if (e.StagingItem.Input.RoutedEvent == Stylus.StylusSystemGestureEvent)
			{
				if (flag)
				{
					if (touchDevice.PromotingToManipulation)
					{
						touchDevice.StoredStagingAreaItems.AddItem(stagingItem);
						return;
					}
					if (touchDevice.PromotingToOther)
					{
						this.PromoteMainToMouse(stagingItem);
						return;
					}
				}
			}
			else if (flag && touchDevice.PromotingToOther)
			{
				this.PromoteMainToMouse(stagingItem);
			}
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x00055F50 File Offset: 0x00055350
		[SecurityCritical]
		private static bool IsTouchPromotionEvent(StylusEventArgs stylusEventArgs)
		{
			if (stylusEventArgs != null)
			{
				RoutedEvent routedEvent = stylusEventArgs.RoutedEvent;
				return WispLogic.IsTouchStylusDevice(stylusEventArgs.StylusDeviceImpl.As<WispStylusDevice>()) && (routedEvent == Stylus.StylusMoveEvent || routedEvent == Stylus.StylusDownEvent || routedEvent == Stylus.StylusUpEvent);
			}
			return false;
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00055F98 File Offset: 0x00055398
		private static bool IsTouchStylusDevice(WispStylusDevice stylusDevice)
		{
			return stylusDevice != null && stylusDevice.TabletDevice != null && stylusDevice.TabletDevice.Type == TabletDeviceType.Touch;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00055FC0 File Offset: 0x000553C0
		[SecurityCritical]
		private void PromoteMainToTouch(ProcessInputEventArgs e, StylusEventArgs stylusEventArgs)
		{
			WispStylusDevice wispStylusDevice = stylusEventArgs.StylusDeviceImpl.As<WispStylusDevice>();
			wispStylusDevice.UpdateTouchActiveSource();
			if (stylusEventArgs.RoutedEvent == Stylus.StylusMoveEvent)
			{
				this.PromoteMainMoveToTouch(wispStylusDevice, e.StagingItem);
				return;
			}
			if (stylusEventArgs.RoutedEvent == Stylus.StylusDownEvent)
			{
				this.PromoteMainDownToTouch(wispStylusDevice, e.StagingItem);
				return;
			}
			if (stylusEventArgs.RoutedEvent == Stylus.StylusUpEvent)
			{
				this.PromoteMainUpToTouch(wispStylusDevice, e.StagingItem);
			}
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00056030 File Offset: 0x00055430
		[SecurityCritical]
		private void PromoteMainDownToTouch(WispStylusDevice stylusDevice, StagingAreaInputItem stagingItem)
		{
			WispStylusTouchDevice touchDevice = stylusDevice.TouchDevice;
			if (touchDevice.IsActive)
			{
				touchDevice.OnDeactivate();
			}
			touchDevice.OnActivate();
			bool flag = this.ShouldPromoteToMouse(stylusDevice);
			if (!touchDevice.OnDown() && flag)
			{
				if (touchDevice.PromotingToManipulation)
				{
					touchDevice.StoredStagingAreaItems.AddItem(stagingItem);
					return;
				}
				if (touchDevice.PromotingToOther)
				{
					this.PromoteMainToMouse(stagingItem);
				}
			}
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00056094 File Offset: 0x00055494
		[SecurityCritical]
		private void PromoteMainMoveToTouch(WispStylusDevice stylusDevice, StagingAreaInputItem stagingItem)
		{
			WispStylusTouchDevice touchDevice = stylusDevice.TouchDevice;
			bool flag = this.ShouldPromoteToMouse(stylusDevice);
			if (touchDevice.IsActive)
			{
				if (!touchDevice.OnMove() && flag)
				{
					if (touchDevice.PromotingToManipulation)
					{
						WispLogic.StagingAreaInputItemList storedStagingAreaItems = touchDevice.StoredStagingAreaItems;
						int count = storedStagingAreaItems.Count;
						if (count > 0 && storedStagingAreaItems[count - 1].Input.RoutedEvent == Stylus.StylusMoveEvent)
						{
							storedStagingAreaItems[count - 1] = stagingItem;
							storedStagingAreaItems.IncrementVersion();
							return;
						}
						touchDevice.StoredStagingAreaItems.AddItem(stagingItem);
						return;
					}
					else if (touchDevice.PromotingToOther)
					{
						this.PromoteMainToMouse(stagingItem);
						return;
					}
				}
			}
			else if (flag)
			{
				this.PromoteMainToMouse(stagingItem);
			}
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00056134 File Offset: 0x00055534
		[SecurityCritical]
		private void PromoteMainUpToTouch(WispStylusDevice stylusDevice, StagingAreaInputItem stagingItem)
		{
			WispStylusTouchDevice touchDevice = stylusDevice.TouchDevice;
			bool flag = this.ShouldPromoteToMouse(stylusDevice);
			if (touchDevice.IsActive)
			{
				touchDevice.OnUp();
				bool promotingToOther = touchDevice.PromotingToOther;
				if (touchDevice.IsActive)
				{
					touchDevice.OnDeactivate();
				}
				if (flag && promotingToOther && (this._mouseLeftButtonState == MouseButtonState.Pressed || this._mouseRightButtonState == MouseButtonState.Pressed || this._leavingDragDrop))
				{
					this.PromoteMainToMouse(stagingItem);
				}
			}
			else if (flag)
			{
				this.PromoteMainToMouse(stagingItem);
			}
			this._leavingDragDrop = false;
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x000561B0 File Offset: 0x000555B0
		[SecurityCritical]
		internal void PromoteStoredItemsToMouse(WispStylusTouchDevice touchDevice)
		{
			if (!this.ShouldPromoteToMouse(touchDevice.StylusDevice.As<WispStylusDevice>()))
			{
				return;
			}
			int count = touchDevice.StoredStagingAreaItems.Count;
			if (count > 0)
			{
				WispLogic.StagingAreaInputItemList storedStagingAreaItems = touchDevice.StoredStagingAreaItems;
				StagingAreaInputItem[] array = new StagingAreaInputItem[count];
				storedStagingAreaItems.CopyTo(array, 0);
				storedStagingAreaItems.Clear();
				long num = storedStagingAreaItems.IncrementVersion();
				int num2 = 0;
				while (num2 < count && num == storedStagingAreaItems.Version)
				{
					StagingAreaInputItem stagingAreaInputItem = array[num2];
					InputReportEventArgs inputReportEventArgs = stagingAreaInputItem.Input as InputReportEventArgs;
					if (inputReportEventArgs != null && inputReportEventArgs.Report.Type == InputType.Mouse && !(inputReportEventArgs.Device is StylusDevice))
					{
						touchDevice.StylusDevice.As<WispStylusDevice>().PlayBackCachedDownInputReport(inputReportEventArgs.Report.Timestamp);
					}
					else
					{
						this.PromoteMainToMouse(stagingAreaInputItem);
					}
					num2++;
				}
			}
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00056278 File Offset: 0x00055678
		private bool ShouldPromoteToMouse(WispStylusDevice stylusDevice)
		{
			return this.CurrentMousePromotionStylusDevice == null || this.CurrentMousePromotionStylusDevice == stylusDevice;
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x0005629C File Offset: 0x0005569C
		// (set) Token: 0x06001687 RID: 5767 RVA: 0x000562B0 File Offset: 0x000556B0
		internal object CurrentMousePromotionStylusDevice { get; set; }

		// Token: 0x06001688 RID: 5768 RVA: 0x000562C4 File Offset: 0x000556C4
		[SecurityCritical]
		private void PromoteMainToMouse(StagingAreaInputItem stagingItem)
		{
			if (!stagingItem.Input.Handled)
			{
				StylusEventArgs stylusEventArgs = stagingItem.Input as StylusEventArgs;
				if (stylusEventArgs != null)
				{
					WispStylusDevice wispStylusDevice = stylusEventArgs.StylusDevice.As<WispStylusDevice>();
					if (wispStylusDevice != null)
					{
						if (this.IgnoreGestureToMousePromotion(stylusEventArgs as StylusSystemGestureEventArgs, wispStylusDevice.TouchDevice))
						{
							return;
						}
						RawMouseActions rawMouseActions = wispStylusDevice.GetMouseActionsFromStylusEventAndPlaybackCachedDown(stagingItem.Input.RoutedEvent, stylusEventArgs);
						if (rawMouseActions != RawMouseActions.None)
						{
							PresentationSource mousePresentationSource = wispStylusDevice.GetMousePresentationSource();
							if (mousePresentationSource != null)
							{
								Point point = PointUtil.ScreenToClient(wispStylusDevice.LastMouseScreenPoint, mousePresentationSource);
								if (this._inputManager.Value.PrimaryMouseDevice.CriticalActiveSource != mousePresentationSource)
								{
									rawMouseActions |= RawMouseActions.Activate;
								}
								RawMouseInputReport report = new RawMouseInputReport(InputMode.Foreground, stylusEventArgs.Timestamp, mousePresentationSource, rawMouseActions, (int)point.X, (int)point.Y, 0, IntPtr.Zero);
								InputReportEventArgs inputReportEventArgs = new InputReportEventArgs(wispStylusDevice.StylusDevice, report);
								inputReportEventArgs.RoutedEvent = InputManager.PreviewInputReportEvent;
								this._inputManager.Value.ProcessInput(inputReportEventArgs);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x000563C0 File Offset: 0x000557C0
		private bool IgnoreGestureToMousePromotion(StylusSystemGestureEventArgs gestureArgs, WispStylusTouchDevice touchDevice)
		{
			if (gestureArgs != null && touchDevice.DownHandled)
			{
				SystemGesture systemGesture = gestureArgs.SystemGesture;
				if (systemGesture == SystemGesture.Tap || systemGesture == SystemGesture.Drag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x000563EC File Offset: 0x000557EC
		[SecurityCritical]
		private void CallPlugInsForMouse(ProcessInputEventArgs e)
		{
			if (!e.StagingItem.Input.Handled)
			{
				if (e.StagingItem.Input.RoutedEvent != Mouse.PreviewMouseDownEvent && e.StagingItem.Input.RoutedEvent != Mouse.PreviewMouseUpEvent && e.StagingItem.Input.RoutedEvent != Mouse.PreviewMouseMoveEvent && e.StagingItem.Input.RoutedEvent != InputManager.InputReportEvent)
				{
					return;
				}
				RawStylusActions actions = RawStylusActions.None;
				MouseDevice mouseDevice;
				bool flag;
				bool flag2;
				int timestamp;
				PresentationSource presentationSource;
				if (e.StagingItem.Input.RoutedEvent == InputManager.InputReportEvent)
				{
					if (this._activeMousePlugInCollection == null || this._activeMousePlugInCollection.Element == null)
					{
						return;
					}
					InputReportEventArgs inputReportEventArgs = e.StagingItem.Input as InputReportEventArgs;
					if (inputReportEventArgs.Report.Type != InputType.Mouse)
					{
						return;
					}
					RawMouseInputReport rawMouseInputReport = (RawMouseInputReport)inputReportEventArgs.Report;
					if ((rawMouseInputReport.Actions & RawMouseActions.Deactivate) != RawMouseActions.Deactivate)
					{
						return;
					}
					mouseDevice = this._inputManager.Value.PrimaryMouseDevice;
					if (mouseDevice == null || mouseDevice.DirectlyOver != null)
					{
						return;
					}
					flag = (mouseDevice.LeftButton == MouseButtonState.Pressed);
					flag2 = (mouseDevice.RightButton == MouseButtonState.Pressed);
					timestamp = rawMouseInputReport.Timestamp;
					presentationSource = PresentationSource.CriticalFromVisual(this._activeMousePlugInCollection.Element);
				}
				else
				{
					MouseEventArgs mouseEventArgs = e.StagingItem.Input as MouseEventArgs;
					mouseDevice = mouseEventArgs.MouseDevice;
					flag = (mouseDevice.LeftButton == MouseButtonState.Pressed);
					flag2 = (mouseDevice.RightButton == MouseButtonState.Pressed);
					if (mouseEventArgs.StylusDevice != null && e.StagingItem.Input.RoutedEvent != Mouse.PreviewMouseUpEvent)
					{
						return;
					}
					if (e.StagingItem.Input.RoutedEvent == Mouse.PreviewMouseMoveEvent)
					{
						if (!flag)
						{
							return;
						}
						actions = RawStylusActions.Move;
					}
					if (e.StagingItem.Input.RoutedEvent == Mouse.PreviewMouseDownEvent)
					{
						MouseButtonEventArgs mouseButtonEventArgs = mouseEventArgs as MouseButtonEventArgs;
						if (mouseButtonEventArgs.ChangedButton != MouseButton.Left)
						{
							return;
						}
						actions = RawStylusActions.Down;
					}
					if (e.StagingItem.Input.RoutedEvent == Mouse.PreviewMouseUpEvent)
					{
						MouseButtonEventArgs mouseButtonEventArgs2 = mouseEventArgs as MouseButtonEventArgs;
						if (mouseButtonEventArgs2.ChangedButton != MouseButton.Left)
						{
							return;
						}
						actions = RawStylusActions.Up;
					}
					timestamp = mouseEventArgs.Timestamp;
					Visual visual = mouseDevice.DirectlyOver as Visual;
					if (visual == null)
					{
						return;
					}
					presentationSource = PresentationSource.CriticalFromVisual(visual);
				}
				PenContexts penContextsFromHwnd = this.GetPenContextsFromHwnd(presentationSource);
				if (penContextsFromHwnd != null && presentationSource != null && presentationSource.CompositionTarget != null && !presentationSource.CompositionTarget.IsDisposed)
				{
					IInputElement directlyOver = mouseDevice.DirectlyOver;
					int num = (flag ? 1 : 0) | (flag2 ? 9 : 0);
					Point point = mouseDevice.GetPosition(presentationSource.RootVisual as IInputElement);
					point = presentationSource.CompositionTarget.TransformToDevice.Transform(point);
					int num2 = (flag ? 1 : 0) | (flag2 ? 3 : 0);
					int[] data = new int[]
					{
						(int)point.X,
						(int)point.Y,
						num,
						num2
					};
					RawStylusInputReport inputReport = new RawStylusInputReport(InputMode.Foreground, timestamp, presentationSource, actions, () => this.GetMousePointDescription, 0, 0, data);
					using (base.Dispatcher.DisableProcessing())
					{
						this._activeMousePlugInCollection = penContextsFromHwnd.InvokeStylusPluginCollectionForMouse(inputReport, directlyOver, this._activeMousePlugInCollection);
					}
				}
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x00056730 File Offset: 0x00055B30
		internal StylusPointDescription GetMousePointDescription
		{
			get
			{
				if (this._mousePointDescription == null)
				{
					this._mousePointDescription = new StylusPointDescription(new StylusPointPropertyInfo[]
					{
						StylusPointPropertyInfoDefaults.X,
						StylusPointPropertyInfoDefaults.Y,
						StylusPointPropertyInfoDefaults.NormalPressure,
						StylusPointPropertyInfoDefaults.PacketStatus,
						StylusPointPropertyInfoDefaults.TipButton,
						StylusPointPropertyInfoDefaults.BarrelButton
					}, -1);
				}
				return this._mousePointDescription;
			}
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00056790 File Offset: 0x00055B90
		internal MouseButtonState GetMouseLeftOrRightButtonState(bool leftButton)
		{
			if (leftButton)
			{
				return this._mouseLeftButtonState;
			}
			return this._mouseRightButtonState;
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x000567B0 File Offset: 0x00055BB0
		internal bool UpdateMouseButtonState(RawMouseActions actions)
		{
			bool result = false;
			if (actions <= RawMouseActions.Button1Release)
			{
				if (actions != RawMouseActions.Button1Press)
				{
					if (actions == RawMouseActions.Button1Release)
					{
						if (this._mouseLeftButtonState != MouseButtonState.Released)
						{
							result = true;
							this._mouseLeftButtonState = MouseButtonState.Released;
						}
					}
				}
				else if (this._mouseLeftButtonState != MouseButtonState.Pressed)
				{
					result = true;
					this._mouseLeftButtonState = MouseButtonState.Pressed;
				}
			}
			else if (actions != RawMouseActions.Button2Press)
			{
				if (actions == RawMouseActions.Button2Release)
				{
					if (this._mouseRightButtonState != MouseButtonState.Released)
					{
						result = true;
						this._mouseRightButtonState = MouseButtonState.Released;
					}
				}
			}
			else if (this._mouseRightButtonState != MouseButtonState.Pressed)
			{
				result = true;
				this._mouseRightButtonState = MouseButtonState.Pressed;
			}
			return result;
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x00056838 File Offset: 0x00055C38
		[SecurityCritical]
		private void UpdateMouseState()
		{
			MouseDevice primaryMouseDevice = this._inputManager.Value.PrimaryMouseDevice;
			this._mouseLeftButtonState = primaryMouseDevice.GetButtonStateFromSystem(MouseButton.Left);
			this._mouseRightButtonState = primaryMouseDevice.GetButtonStateFromSystem(MouseButton.Right);
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x00056870 File Offset: 0x00055C70
		private void UpdateIsStylusInRange(bool forceInRange)
		{
			bool flag = false;
			if (forceInRange)
			{
				flag = true;
			}
			else
			{
				foreach (object obj in ((IEnumerable)Tablet.TabletDevices))
				{
					TabletDevice tabletDevice = (TabletDevice)obj;
					foreach (StylusDevice stylusDevice in tabletDevice.StylusDevices)
					{
						if (stylusDevice.InRange)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						break;
					}
				}
			}
			this._stylusDeviceInRange = flag;
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x00056934 File Offset: 0x00055D34
		internal override void UpdateStylusCapture(StylusDeviceBase stylusDevice, IInputElement oldStylusDeviceCapture, IInputElement newStylusDeviceCapture, int timestamp)
		{
			if (newStylusDeviceCapture != this._stylusCapture)
			{
				IInputElement stylusCapture = this._stylusCapture;
				this._stylusCapture = newStylusDeviceCapture;
				if (stylusCapture != null)
				{
					DependencyObject dependencyObject = stylusCapture as DependencyObject;
					if (InputElement.IsUIElement(dependencyObject))
					{
						((UIElement)dependencyObject).IsEnabledChanged -= this._captureIsEnabledChangedEventHandler;
						((UIElement)dependencyObject).IsVisibleChanged -= this._captureIsVisibleChangedEventHandler;
						((UIElement)dependencyObject).IsHitTestVisibleChanged -= this._captureIsHitTestVisibleChangedEventHandler;
					}
					else if (InputElement.IsContentElement(dependencyObject))
					{
						((ContentElement)dependencyObject).IsEnabledChanged -= this._captureIsEnabledChangedEventHandler;
					}
					else
					{
						((UIElement3D)dependencyObject).IsEnabledChanged -= this._captureIsEnabledChangedEventHandler;
						((UIElement3D)dependencyObject).IsVisibleChanged -= this._captureIsVisibleChangedEventHandler;
						((UIElement3D)dependencyObject).IsHitTestVisibleChanged -= this._captureIsHitTestVisibleChangedEventHandler;
					}
				}
				if (this._stylusCapture != null)
				{
					DependencyObject dependencyObject = this._stylusCapture as DependencyObject;
					if (InputElement.IsUIElement(dependencyObject))
					{
						((UIElement)dependencyObject).IsEnabledChanged += this._captureIsEnabledChangedEventHandler;
						((UIElement)dependencyObject).IsVisibleChanged += this._captureIsVisibleChangedEventHandler;
						((UIElement)dependencyObject).IsHitTestVisibleChanged += this._captureIsHitTestVisibleChangedEventHandler;
					}
					else if (InputElement.IsContentElement(dependencyObject))
					{
						((ContentElement)dependencyObject).IsEnabledChanged += this._captureIsEnabledChangedEventHandler;
					}
					else
					{
						((UIElement3D)dependencyObject).IsEnabledChanged += this._captureIsEnabledChangedEventHandler;
						((UIElement3D)dependencyObject).IsVisibleChanged += this._captureIsVisibleChangedEventHandler;
						((UIElement3D)dependencyObject).IsHitTestVisibleChanged += this._captureIsHitTestVisibleChangedEventHandler;
					}
				}
				UIElement.StylusCaptureWithinProperty.OnOriginValueChanged(stylusCapture as DependencyObject, this._stylusCapture as DependencyObject, ref this._stylusCaptureWithinTreeState);
				if (stylusCapture != null)
				{
					DependencyObject dependencyObject = stylusCapture as DependencyObject;
					dependencyObject.SetValue(UIElement.IsStylusCapturedPropertyKey, false);
				}
				if (this._stylusCapture != null)
				{
					DependencyObject dependencyObject = this._stylusCapture as DependencyObject;
					dependencyObject.SetValue(UIElement.IsStylusCapturedPropertyKey, true);
				}
			}
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x00056AF0 File Offset: 0x00055EF0
		internal override void UpdateOverProperty(StylusDeviceBase stylusDevice, IInputElement newOver)
		{
			if (stylusDevice == this._currentStylusDevice && newOver != this._stylusOver)
			{
				IInputElement stylusOver = this._stylusOver;
				this._stylusOver = newOver;
				if (stylusOver != null)
				{
					DependencyObject dependencyObject = stylusOver as DependencyObject;
					if (InputElement.IsUIElement(dependencyObject))
					{
						((UIElement)dependencyObject).IsEnabledChanged -= this._overIsEnabledChangedEventHandler;
						((UIElement)dependencyObject).IsVisibleChanged -= this._overIsVisibleChangedEventHandler;
						((UIElement)dependencyObject).IsHitTestVisibleChanged -= this._overIsHitTestVisibleChangedEventHandler;
					}
					else if (InputElement.IsContentElement(dependencyObject))
					{
						((ContentElement)dependencyObject).IsEnabledChanged -= this._overIsEnabledChangedEventHandler;
					}
					else
					{
						((UIElement3D)dependencyObject).IsEnabledChanged -= this._overIsEnabledChangedEventHandler;
						((UIElement3D)dependencyObject).IsVisibleChanged -= this._overIsVisibleChangedEventHandler;
						((UIElement3D)dependencyObject).IsHitTestVisibleChanged -= this._overIsHitTestVisibleChangedEventHandler;
					}
				}
				if (this._stylusOver != null)
				{
					DependencyObject dependencyObject = this._stylusOver as DependencyObject;
					if (InputElement.IsUIElement(dependencyObject))
					{
						((UIElement)dependencyObject).IsEnabledChanged += this._overIsEnabledChangedEventHandler;
						((UIElement)dependencyObject).IsVisibleChanged += this._overIsVisibleChangedEventHandler;
						((UIElement)dependencyObject).IsHitTestVisibleChanged += this._overIsHitTestVisibleChangedEventHandler;
					}
					else if (InputElement.IsContentElement(dependencyObject))
					{
						((ContentElement)dependencyObject).IsEnabledChanged += this._overIsEnabledChangedEventHandler;
					}
					else
					{
						((UIElement3D)dependencyObject).IsEnabledChanged += this._overIsEnabledChangedEventHandler;
						((UIElement3D)dependencyObject).IsVisibleChanged += this._overIsVisibleChangedEventHandler;
						((UIElement3D)dependencyObject).IsHitTestVisibleChanged += this._overIsHitTestVisibleChangedEventHandler;
					}
				}
				UIElement.StylusOverProperty.OnOriginValueChanged(stylusOver as DependencyObject, this._stylusOver as DependencyObject, ref this._stylusOverTreeState);
				if (stylusOver != null)
				{
					DependencyObject dependencyObject = stylusOver as DependencyObject;
					dependencyObject.SetValue(UIElement.IsStylusDirectlyOverPropertyKey, false);
				}
				if (this._stylusOver != null)
				{
					DependencyObject dependencyObject = this._stylusOver as DependencyObject;
					dependencyObject.SetValue(UIElement.IsStylusDirectlyOverPropertyKey, true);
				}
			}
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00056CB8 File Offset: 0x000560B8
		private void OnOverIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateStylusOver(null, null, true);
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00056CD0 File Offset: 0x000560D0
		private void OnOverIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateStylusOver(null, null, true);
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00056CE8 File Offset: 0x000560E8
		private void OnOverIsHitTestVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateStylusOver(null, null, true);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00056D00 File Offset: 0x00056100
		private void OnCaptureIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateCapture(null, null, true);
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x00056D18 File Offset: 0x00056118
		private void OnCaptureIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateCapture(null, null, true);
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x00056D30 File Offset: 0x00056130
		private void OnCaptureIsHitTestVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateCapture(null, null, true);
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x00056D48 File Offset: 0x00056148
		internal override void ReevaluateStylusOver(DependencyObject element, DependencyObject oldParent, bool isCoreParent)
		{
			if (element != null)
			{
				if (isCoreParent)
				{
					this.StylusOverTreeState.SetCoreParent(element, oldParent);
				}
				else
				{
					this.StylusOverTreeState.SetLogicalParent(element, oldParent);
				}
			}
			if (this._reevaluateStylusOverOperation == null)
			{
				this._reevaluateStylusOverOperation = base.Dispatcher.BeginInvoke(DispatcherPriority.Input, this._reevaluateStylusOverDelegate, null);
			}
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00056D98 File Offset: 0x00056198
		private object ReevaluateStylusOverAsync(object arg)
		{
			this._reevaluateStylusOverOperation = null;
			if (this._stylusOverTreeState != null && !this._stylusOverTreeState.IsEmpty)
			{
				UIElement.StylusOverProperty.OnOriginValueChanged(this._stylusOver as DependencyObject, this._stylusOver as DependencyObject, ref this._stylusOverTreeState);
			}
			return null;
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00056DE8 File Offset: 0x000561E8
		internal override void ReevaluateCapture(DependencyObject element, DependencyObject oldParent, bool isCoreParent)
		{
			if (element != null)
			{
				if (isCoreParent)
				{
					this.StylusCaptureWithinTreeState.SetCoreParent(element, oldParent);
				}
				else
				{
					this.StylusCaptureWithinTreeState.SetLogicalParent(element, oldParent);
				}
			}
			if (this._reevaluateCaptureOperation == null)
			{
				this._reevaluateCaptureOperation = base.Dispatcher.BeginInvoke(DispatcherPriority.Input, this._reevaluateCaptureDelegate, null);
			}
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00056E38 File Offset: 0x00056238
		private object ReevaluateCaptureAsync(object arg)
		{
			this._reevaluateCaptureOperation = null;
			if (this._stylusCapture == null)
			{
				return null;
			}
			DependencyObject o = this._stylusCapture as DependencyObject;
			bool flag;
			if (InputElement.IsUIElement(o))
			{
				flag = !base.ValidateUIElementForCapture((UIElement)this._stylusCapture);
			}
			else if (InputElement.IsContentElement(o))
			{
				flag = !base.ValidateContentElementForCapture((ContentElement)this._stylusCapture);
			}
			else
			{
				flag = !base.ValidateUIElement3DForCapture((UIElement3D)this._stylusCapture);
			}
			if (!flag)
			{
				DependencyObject containingVisual = InputElement.GetContainingVisual(o);
				flag = !base.ValidateVisualForCapture(containingVisual, this.CurrentStylusDevice);
			}
			if (flag)
			{
				Stylus.Capture(null);
			}
			if (this._stylusCaptureWithinTreeState != null && !this._stylusCaptureWithinTreeState.IsEmpty)
			{
				UIElement.StylusCaptureWithinProperty.OnOriginValueChanged(this._stylusCapture as DependencyObject, this._stylusCapture as DependencyObject, ref this._stylusCaptureWithinTreeState);
			}
			return null;
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00056F18 File Offset: 0x00056318
		[SecurityCritical]
		private bool IsValidStylusAction(RawStylusInputReport rawStylusInputReport)
		{
			bool flag = true;
			WispStylusDevice wispStylusDevice = rawStylusInputReport.StylusDevice.As<WispStylusDevice>();
			RawStylusActions actions = rawStylusInputReport.Actions;
			if (actions <= RawStylusActions.Move)
			{
				if (actions != RawStylusActions.Down)
				{
					if (actions != RawStylusActions.Up)
					{
						if (actions == RawStylusActions.Move)
						{
							flag = (rawStylusInputReport.PenContext == wispStylusDevice.ActivePenContext);
						}
					}
					else
					{
						flag = (rawStylusInputReport.PenContext == wispStylusDevice.ActivePenContext);
					}
				}
				else if (!wispStylusDevice.InRange)
				{
					this.GenerateInRange(rawStylusInputReport);
				}
				else
				{
					flag = (rawStylusInputReport.PenContext == wispStylusDevice.ActivePenContext);
				}
			}
			else if (actions <= RawStylusActions.InRange)
			{
				if (actions != RawStylusActions.InAirMove)
				{
					if (actions == RawStylusActions.InRange)
					{
						flag = (!wispStylusDevice.InRange && !rawStylusInputReport.InputSource.IsDisposed);
					}
				}
				else if (!wispStylusDevice.InRange && !rawStylusInputReport.InputSource.IsDisposed)
				{
					this.GenerateInRange(rawStylusInputReport);
				}
				else
				{
					flag = (rawStylusInputReport.PenContext == wispStylusDevice.ActivePenContext);
				}
			}
			else if (actions != RawStylusActions.OutOfRange)
			{
				if (actions == RawStylusActions.SystemGesture)
				{
					flag = (rawStylusInputReport.PenContext == wispStylusDevice.ActivePenContext);
					if (flag)
					{
						RawStylusSystemGestureInputReport rawStylusSystemGestureInputReport = (RawStylusSystemGestureInputReport)rawStylusInputReport;
						if (rawStylusSystemGestureInputReport.SystemGesture == SystemGesture.Tap && wispStylusDevice.InAir)
						{
							flag = false;
						}
					}
				}
			}
			else
			{
				flag = (rawStylusInputReport.PenContext == wispStylusDevice.ActivePenContext);
			}
			return flag;
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00057060 File Offset: 0x00056460
		[SecurityCritical]
		private void GenerateInRange(RawStylusInputReport rawStylusInputReport)
		{
			StylusDevice stylusDevice = rawStylusInputReport.StylusDevice;
			RawStylusInputReport report = new RawStylusInputReport(rawStylusInputReport.Mode, rawStylusInputReport.Timestamp, rawStylusInputReport.InputSource, rawStylusInputReport.PenContext, RawStylusActions.InRange, stylusDevice.TabletDevice.Id, stylusDevice.Id, rawStylusInputReport.Data);
			InputReportEventArgs inputReportEventArgs = new InputReportEventArgs(stylusDevice, report);
			inputReportEventArgs.RoutedEvent = InputManager.PreviewInputReportEvent;
			this._inputManager.Value.ProcessInput(inputReportEventArgs);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x000570D0 File Offset: 0x000564D0
		[SecurityCritical]
		[FriendAccessAllowed]
		internal override void HandleMessage(WindowMessage msg, IntPtr wParam, IntPtr lParam)
		{
			if (msg <= WindowMessage.WM_DISPLAYCHANGE)
			{
				if (msg != WindowMessage.WM_WININICHANGE)
				{
					if (msg != WindowMessage.WM_DISPLAYCHANGE)
					{
						return;
					}
					this.OnScreenMeasurementsChanged();
					return;
				}
				else
				{
					base.ReadSystemConfig();
					if (this._tabletDeviceCollection == null)
					{
						return;
					}
					using (IEnumerator enumerator = ((IEnumerable)this._tabletDeviceCollection).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							TabletDevice tabletDevice = (TabletDevice)obj;
							tabletDevice.As<WispTabletDevice>().InvalidateSizeDeltas();
						}
						return;
					}
				}
			}
			else if (msg != WindowMessage.WM_DEVICECHANGE)
			{
				if (msg != WindowMessage.WM_TABLET_ADDED)
				{
					if (msg != WindowMessage.WM_TABLET_DELETED)
					{
						return;
					}
					this.OnTabletRemovedImpl((uint)NativeMethods.IntPtrToInt32(wParam), true);
					return;
				}
			}
			else
			{
				if (!this._inputEnabled && NativeMethods.IntPtrToInt32(wParam) == 7)
				{
					this.OnDeviceChange();
					return;
				}
				return;
			}
			this.OnTabletAdded((uint)NativeMethods.IntPtrToInt32(wParam));
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x000571B8 File Offset: 0x000565B8
		[SecurityCritical]
		internal void InvokeStylusPluginCollection(RawStylusInputReport inputReport)
		{
			if (inputReport.StylusDevice != null)
			{
				inputReport.PenContext.Contexts.InvokeStylusPluginCollection(inputReport);
			}
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x000571E0 File Offset: 0x000565E0
		[SecurityCritical]
		private void VerifyStylusPlugInCollectionTarget(RawStylusInputReport rawStylusInputReport)
		{
			RawStylusActions actions = rawStylusInputReport.Actions;
			if (actions != RawStylusActions.Down && actions != RawStylusActions.Up && actions != RawStylusActions.Move)
			{
				return;
			}
			RawStylusInput rawStylusInput = rawStylusInputReport.RawStylusInput;
			StylusPlugInCollection stylusPlugInCollection = null;
			StylusPlugInCollection stylusPlugInCollection2 = (rawStylusInput != null) ? rawStylusInput.Target : null;
			bool flag = false;
			UIElement uielement = InputElement.GetContainingUIElement(rawStylusInputReport.StylusDevice.DirectlyOver as DependencyObject) as UIElement;
			if (uielement != null)
			{
				stylusPlugInCollection = rawStylusInputReport.PenContext.Contexts.FindPlugInCollection(uielement);
			}
			using (base.Dispatcher.DisableProcessing())
			{
				if (stylusPlugInCollection2 != null && stylusPlugInCollection2 != stylusPlugInCollection && rawStylusInput != null)
				{
					foreach (RawStylusInputCustomData rawStylusInputCustomData in rawStylusInput.CustomDataList)
					{
						rawStylusInputCustomData.Owner.FireCustomData(rawStylusInputCustomData.Data, rawStylusInputReport.Actions, false);
					}
					flag = rawStylusInput.StylusPointsModified;
					rawStylusInputReport.RawStylusInput = null;
				}
				bool flag2 = false;
				if (stylusPlugInCollection != null && rawStylusInputReport.RawStylusInput == null)
				{
					GeneralTransformGroup generalTransformGroup = new GeneralTransformGroup();
					generalTransformGroup.Children.Add(new MatrixTransform(this.GetTabletToViewTransform(rawStylusInputReport.StylusDevice.TabletDevice)));
					generalTransformGroup.Children.Add(stylusPlugInCollection.ViewToElement);
					generalTransformGroup.Freeze();
					RawStylusInput rawStylusInput2 = new RawStylusInput(rawStylusInputReport, generalTransformGroup, stylusPlugInCollection);
					rawStylusInputReport.RawStylusInput = rawStylusInput2;
					flag2 = true;
				}
				WispStylusDevice wispStylusDevice = rawStylusInputReport.StylusDevice.As<WispStylusDevice>();
				StylusPlugInCollection currentVerifiedTarget = wispStylusDevice.CurrentVerifiedTarget;
				if (stylusPlugInCollection != currentVerifiedTarget)
				{
					if (currentVerifiedTarget != null)
					{
						if (rawStylusInput == null)
						{
							GeneralTransformGroup generalTransformGroup2 = new GeneralTransformGroup();
							generalTransformGroup2.Children.Add(new MatrixTransform(this.GetTabletToViewTransform(wispStylusDevice.TabletDevice)));
							generalTransformGroup2.Children.Add(currentVerifiedTarget.ViewToElement);
							generalTransformGroup2.Freeze();
							rawStylusInput = new RawStylusInput(rawStylusInputReport, generalTransformGroup2, currentVerifiedTarget);
						}
						currentVerifiedTarget.FireEnterLeave(false, rawStylusInput, true);
					}
					if (stylusPlugInCollection != null)
					{
						stylusPlugInCollection.FireEnterLeave(true, rawStylusInputReport.RawStylusInput, true);
						base.Statistics.FeaturesUsed |= StylusTraceLogger.FeatureFlags.StylusPluginsUsed;
					}
					wispStylusDevice.CurrentVerifiedTarget = stylusPlugInCollection;
				}
				if (flag2)
				{
					stylusPlugInCollection.FireRawStylusInput(rawStylusInputReport.RawStylusInput);
					flag = (flag || rawStylusInputReport.RawStylusInput.StylusPointsModified);
					base.Statistics.FeaturesUsed |= StylusTraceLogger.FeatureFlags.StylusPluginsUsed;
				}
				if (stylusPlugInCollection != null)
				{
					foreach (RawStylusInputCustomData rawStylusInputCustomData2 in rawStylusInputReport.RawStylusInput.CustomDataList)
					{
						rawStylusInputCustomData2.Owner.FireCustomData(rawStylusInputCustomData2.Data, rawStylusInputReport.Actions, true);
					}
				}
				if (flag)
				{
					rawStylusInputReport.StylusDevice.As<WispStylusDevice>().UpdateEventStylusPoints(rawStylusInputReport, true);
				}
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x000574C0 File Offset: 0x000568C0
		internal int DoubleTapDelta
		{
			get
			{
				if (!WispLogic.IsTouchStylusDevice(this._currentStylusDevice))
				{
					return this._stylusDoubleTapDelta;
				}
				return this._touchDoubleTapDelta;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x000574EC File Offset: 0x000568EC
		internal int DoubleTapDeltaTime
		{
			get
			{
				if (!WispLogic.IsTouchStylusDevice(this._currentStylusDevice))
				{
					return this._stylusDoubleTapDeltaTime;
				}
				return this._touchDoubleTapDeltaTime;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x00057518 File Offset: 0x00056918
		internal int CancelDelta
		{
			get
			{
				return this._cancelDelta;
			}
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0005752C File Offset: 0x0005692C
		[SecurityCritical]
		private void GenerateGesture(RawStylusInputReport rawStylusInputReport, SystemGesture gesture)
		{
			StylusDevice stylusDevice = rawStylusInputReport.StylusDevice;
			this.InputManagerProcessInputEventArgs(new InputReportEventArgs(stylusDevice, new RawStylusSystemGestureInputReport(InputMode.Foreground, rawStylusInputReport.Timestamp, rawStylusInputReport.InputSource, rawStylusInputReport.PenContext, rawStylusInputReport.TabletDeviceId, rawStylusInputReport.StylusDeviceId, gesture, 0, 0, 0)
			{
				StylusDevice = stylusDevice
			})
			{
				RoutedEvent = InputManager.PreviewInputReportEvent
			});
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0005758C File Offset: 0x0005698C
		[SecurityCritical]
		private void ProcessMouseMove(WispStylusDevice stylusDevice, int timestamp, bool isSynchronize)
		{
			if (!this.ShouldPromoteToMouse(stylusDevice) || !stylusDevice.TouchDevice.PromotingToOther)
			{
				return;
			}
			PresentationSource mousePresentationSource = stylusDevice.GetMousePresentationSource();
			if (mousePresentationSource != null)
			{
				RawMouseActions rawMouseActions = RawMouseActions.AbsoluteMove;
				if (!isSynchronize && this._inputManager.Value.PrimaryMouseDevice.CriticalActiveSource != mousePresentationSource)
				{
					rawMouseActions |= RawMouseActions.Activate;
				}
				Point pointScreen = stylusDevice.LastMouseScreenPoint;
				pointScreen = PointUtil.ScreenToClient(pointScreen, mousePresentationSource);
				RawMouseInputReport rawMouseInputReport = new RawMouseInputReport(InputMode.Foreground, timestamp, mousePresentationSource, rawMouseActions, (int)pointScreen.X, (int)pointScreen.Y, 0, IntPtr.Zero);
				if (isSynchronize)
				{
					rawMouseInputReport._isSynchronize = true;
				}
				this.InputManagerProcessInputEventArgs(new InputReportEventArgs(stylusDevice.StylusDevice, rawMouseInputReport)
				{
					RoutedEvent = InputManager.PreviewInputReportEvent
				});
			}
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0005763C File Offset: 0x00056A3C
		[SecurityCritical]
		private void UpdateButtonStates(ProcessInputEventArgs e)
		{
			if (!e.StagingItem.Input.Handled)
			{
				RoutedEvent routedEvent = e.StagingItem.Input.RoutedEvent;
				if (routedEvent != null && (routedEvent == Stylus.StylusDownEvent || routedEvent == Stylus.StylusUpEvent || routedEvent == Stylus.StylusMoveEvent || routedEvent == Stylus.StylusInAirMoveEvent))
				{
					StylusEventArgs stylusEventArgs = (StylusEventArgs)e.StagingItem.Input;
					RawStylusInputReport inputReport = stylusEventArgs.InputReport;
					StylusDevice stylusDevice = inputReport.StylusDevice;
					StylusPointCollection stylusPoints = stylusDevice.GetStylusPoints(null);
					StylusPoint stylusPoint = stylusPoints[stylusPoints.Count - 1];
					foreach (StylusButton stylusButton in stylusDevice.StylusButtons)
					{
						StylusButtonState propertyValue = (StylusButtonState)stylusPoint.GetPropertyValue(new StylusPointProperty(stylusButton.Guid, true));
						if (propertyValue != stylusButton.CachedButtonState)
						{
							stylusButton.CachedButtonState = propertyValue;
							StylusButtonEventArgs stylusButtonEventArgs = new StylusButtonEventArgs(stylusDevice, inputReport.Timestamp, stylusButton);
							stylusButtonEventArgs.InputReport = inputReport;
							if (propertyValue == StylusButtonState.Down)
							{
								stylusButtonEventArgs.RoutedEvent = Stylus.PreviewStylusButtonDownEvent;
							}
							else
							{
								stylusButtonEventArgs.RoutedEvent = Stylus.PreviewStylusButtonUpEvent;
							}
							this.InputManagerProcessInputEventArgs(stylusButtonEventArgs);
						}
					}
				}
			}
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x00057784 File Offset: 0x00056B84
		[SecurityCritical]
		private static bool InWindowClientRect(Point ptClient, PresentationSource inputSource)
		{
			bool result = false;
			HwndSource hwndSource = inputSource as HwndSource;
			if (hwndSource != null && hwndSource.CompositionTarget != null && !hwndSource.IsHandleNull)
			{
				Point pointScreen = PointUtil.ClientToScreen(ptClient, hwndSource);
				IntPtr intPtr = IntPtr.Zero;
				Point point = new Point(0.0, 0.0);
				intPtr = UnsafeNativeMethods.WindowFromPoint((int)pointScreen.X, (int)pointScreen.Y);
				if (intPtr != IntPtr.Zero)
				{
					HwndSource hwndSource2 = HwndSource.CriticalFromHwnd(intPtr);
					if (hwndSource2 != null)
					{
						point = PointUtil.ScreenToClient(pointScreen, hwndSource2);
						NativeMethods.RECT rect = default(NativeMethods.RECT);
						SafeNativeMethods.GetClientRect(new HandleRef(hwndSource2, intPtr), ref rect);
						result = ((int)point.X >= rect.left && (int)point.X < rect.right && (int)point.Y >= rect.top && (int)point.Y < rect.bottom);
					}
				}
			}
			return result;
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x00057878 File Offset: 0x00056C78
		internal override TabletDeviceCollection TabletDevices
		{
			[SecurityCritical]
			get
			{
				return this.WispTabletDevices;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x0005788C File Offset: 0x00056C8C
		internal WispTabletDeviceCollection WispTabletDevices
		{
			[SecurityCritical]
			get
			{
				if (this._tabletDeviceCollection == null)
				{
					this._tabletDeviceCollection = new WispTabletDeviceCollection();
					this._inputManager.Value.Dispatcher.ShutdownFinished += this._shutdownHandler;
				}
				return this._tabletDeviceCollection;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x000578D0 File Offset: 0x00056CD0
		internal override StylusDeviceBase CurrentStylusDevice
		{
			get
			{
				return this._currentStylusDevice;
			}
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x000578E4 File Offset: 0x00056CE4
		internal void RegisterStylusDeviceCore(StylusDevice stylusDevice)
		{
			object _stylusDeviceLock = this.__stylusDeviceLock;
			lock (_stylusDeviceLock)
			{
				int id = stylusDevice.Id;
				if (this.__stylusDeviceMap.ContainsKey(id))
				{
					throw new InvalidOperationException
					{
						Data = 
						{
							{
								"System.Windows.Input.StylusLogic",
								""
							}
						}
					};
				}
				this.__stylusDeviceMap[id] = stylusDevice;
			}
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0005796C File Offset: 0x00056D6C
		internal void UnregisterStylusDeviceCore(StylusDevice stylusDevice)
		{
			object _stylusDeviceLock = this.__stylusDeviceLock;
			lock (_stylusDeviceLock)
			{
				this.__stylusDeviceMap.Remove(stylusDevice.Id);
			}
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x000579C4 File Offset: 0x00056DC4
		internal WispStylusDevice FindStylusDevice(int stylusDeviceId)
		{
			StylusDevice stylusDevice;
			this.__stylusDeviceMap.TryGetValue(stylusDeviceId, out stylusDevice);
			if (stylusDevice == null)
			{
				return null;
			}
			return stylusDevice.As<WispStylusDevice>();
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x000579EC File Offset: 0x00056DEC
		internal WispStylusDevice FindStylusDeviceWithLock(int stylusDeviceId)
		{
			object _stylusDeviceLock = this.__stylusDeviceLock;
			StylusDevice stylusDevice;
			lock (_stylusDeviceLock)
			{
				this.__stylusDeviceMap.TryGetValue(stylusDeviceId, out stylusDevice);
			}
			if (stylusDevice == null)
			{
				return null;
			}
			return stylusDevice.As<WispStylusDevice>();
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00057A4C File Offset: 0x00056E4C
		internal void SelectStylusDevice(WispStylusDevice wispStylusDevice, IInputElement newOver, bool updateOver)
		{
			bool flag = this._currentStylusDevice != wispStylusDevice;
			WispStylusDevice currentStylusDevice = this._currentStylusDevice;
			if (updateOver && wispStylusDevice == null && flag)
			{
				this._currentStylusDevice.ChangeStylusOver(newOver);
			}
			this._currentStylusDevice = wispStylusDevice;
			if (updateOver && wispStylusDevice != null)
			{
				wispStylusDevice.ChangeStylusOver(newOver);
				if (flag && currentStylusDevice != null && !currentStylusDevice.InRange)
				{
					currentStylusDevice.ChangeStylusOver(null);
				}
			}
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x00057AB0 File Offset: 0x00056EB0
		[SecurityCritical]
		internal void EnableCore()
		{
			object _penContextsLock = this.__penContextsLock;
			lock (_penContextsLock)
			{
				foreach (PenContexts penContexts in this.__penContextsMap.Values)
				{
					penContexts.Enable();
				}
				this._inputEnabled = true;
			}
			StylusTraceLogger.LogStartup();
			base.ShutdownListener = new StylusLogic.StylusLogicShutDownListener(this, ShutDownEvents.DispatcherShutdown);
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x00057B64 File Offset: 0x00056F64
		internal bool Enabled
		{
			get
			{
				return this._inputEnabled;
			}
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x00057B78 File Offset: 0x00056F78
		[SecurityCritical]
		internal void RegisterHwndForInput(InputManager inputManager, PresentationSource inputSource)
		{
			HwndSource hwndSource = (HwndSource)inputSource;
			if (!this._transformInitialized && hwndSource != null && hwndSource.CompositionTarget != null)
			{
				this._transformToDevice = hwndSource.CompositionTarget.TransformToDevice;
				this._transformInitialized = true;
			}
			bool flag = this._tabletDeviceCollection == null;
			WispTabletDeviceCollection wispTabletDevices = this.WispTabletDevices;
			object _penContextsLock = this.__penContextsLock;
			lock (_penContextsLock)
			{
				if (this.__penContextsMap.ContainsKey(inputSource))
				{
					throw new InvalidOperationException(SR.Get("PenService_WindowAlreadyRegistered"));
				}
				PenContexts penContexts = new PenContexts(StylusLogic.GetCurrentStylusLogicAs<WispLogic>(), inputSource);
				this.__penContextsMap[inputSource] = penContexts;
				if (this.__penContextsMap.Count == 1 && !flag && wispTabletDevices.Count > 0)
				{
					wispTabletDevices.UpdateTablets();
					this._lastKnownDeviceCount = this.GetDeviceCount();
				}
				int windowLong = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, hwndSource.CriticalHandle), -16);
				if ((windowLong & 134217728) != 0)
				{
					penContexts.IsWindowDisabled = true;
				}
				if (this._inputEnabled)
				{
					penContexts.Enable();
				}
			}
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00057C9C File Offset: 0x0005709C
		[SecurityCritical]
		internal void UnRegisterHwndForInput(HwndSource hwndSource)
		{
			bool hasShutdownStarted = base.Dispatcher.HasShutdownStarted;
			if (hasShutdownStarted)
			{
				this.OnDispatcherShutdown(null, null);
			}
			object _penContextsLock = this.__penContextsLock;
			lock (_penContextsLock)
			{
				PenContexts penContexts;
				if (this.__penContextsMap.TryGetValue(hwndSource, out penContexts))
				{
					this.__penContextsMap.Remove(hwndSource);
					penContexts.Disable(hasShutdownStarted);
					if (UnsafeNativeMethods.IsWindow(new HandleRef(hwndSource, hwndSource.CriticalHandle)))
					{
						penContexts.DestroyedLocation = PointUtil.ClientToScreen(new Point(0.0, 0.0), hwndSource);
					}
				}
				if (penContexts == null)
				{
					throw new InvalidOperationException(SR.Get("PenService_WindowNotRegistered"));
				}
			}
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x00057D68 File Offset: 0x00057168
		[SecurityCritical]
		internal PenContexts GetPenContextsFromHwnd(PresentationSource presentationSource)
		{
			PenContexts result = null;
			if (presentationSource != null)
			{
				this.__penContextsMap.TryGetValue(presentationSource, out result);
			}
			return result;
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x00057D8C File Offset: 0x0005718C
		[SecurityCritical]
		internal bool ShouldConsiderStylusInRange(RawMouseInputReport mouseInputReport)
		{
			int timestamp = mouseInputReport.Timestamp;
			if (Math.Abs(timestamp - this._lastInRangeTime) <= 500)
			{
				return true;
			}
			HwndSource hwndSource = mouseInputReport.InputSource as HwndSource;
			if (hwndSource != null)
			{
				PenContexts penContextsFromHwnd = this.GetPenContextsFromHwnd(hwndSource);
				if (penContextsFromHwnd != null)
				{
					return penContextsFromHwnd.ConsiderInRange(timestamp);
				}
			}
			return false;
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x00057DDC File Offset: 0x000571DC
		[SecurityCritical]
		internal PenContext GetStylusPenContextForHwnd(PresentationSource presentationSource, int tabletDeviceId)
		{
			if (presentationSource != null)
			{
				PenContexts penContexts;
				this.__penContextsMap.TryGetValue(presentationSource, out penContexts);
				if (penContexts != null)
				{
					return penContexts.GetTabletDeviceIDPenContext(tabletDeviceId);
				}
			}
			return null;
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x00057E08 File Offset: 0x00057208
		[SecurityCritical]
		private void OnDeviceChange()
		{
			if (!this._inputEnabled && WispTabletDeviceCollection.ShouldEnableTablets())
			{
				this.WispTabletDevices.UpdateTablets();
				this.EnableCore();
				this._lastKnownDeviceCount = this.GetDeviceCount();
			}
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x00057E44 File Offset: 0x00057244
		[SecurityCritical]
		private void OnTabletAdded(uint wisptisIndex)
		{
			object _penContextsLock = this.__penContextsLock;
			lock (_penContextsLock)
			{
				WispTabletDeviceCollection wispTabletDevices = this.WispTabletDevices;
				if (!this._inputEnabled)
				{
					wispTabletDevices.UpdateTablets();
					this.EnableCore();
					this._lastKnownDeviceCount = this.GetDeviceCount();
				}
				else
				{
					this._lastKnownDeviceCount = this.GetDeviceCount();
					uint maxValue = uint.MaxValue;
					if (wispTabletDevices.HandleTabletAdded(wisptisIndex, ref maxValue))
					{
						if (maxValue != 4294967295U)
						{
							using (Dictionary<object, PenContexts>.ValueCollection.Enumerator enumerator = this.__penContextsMap.Values.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									PenContexts penContexts = enumerator.Current;
									penContexts.AddContext(maxValue);
								}
								return;
							}
						}
						this.RefreshTablets();
					}
				}
			}
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x00057F2C File Offset: 0x0005732C
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnTabletRemoved(uint wisptisIndex)
		{
			this.OnTabletRemovedImpl(wisptisIndex, false);
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x00057F44 File Offset: 0x00057344
		[SecurityCritical]
		private void OnTabletRemovedImpl(uint wisptisIndex, bool isInternalCall)
		{
			if (this._inputEnabled)
			{
				object _penContextsLock = this.__penContextsLock;
				lock (_penContextsLock)
				{
					if (this._tabletDeviceCollection != null)
					{
						int deviceCount = this.GetDeviceCount();
						if (isInternalCall && (this._lastKnownDeviceCount < 0 || deviceCount != this._lastKnownDeviceCount - 1 || (ulong)wisptisIndex >= (ulong)((long)this.TabletDevices.Count)))
						{
							this.RefreshTablets();
							if (!this._inputEnabled)
							{
								this.OnTabletRemoved(uint.MaxValue);
							}
						}
						else
						{
							int count = this._tabletDeviceCollection.DeferredTablets.Count;
							uint num = this._tabletDeviceCollection.HandleTabletRemoved(wisptisIndex);
							if (num != 4294967295U && this._tabletDeviceCollection.DeferredTablets.Count == count)
							{
								foreach (PenContexts penContexts in this.__penContextsMap.Values)
								{
									penContexts.RemoveContext(num);
								}
							}
						}
						this._lastKnownDeviceCount = deviceCount;
					}
				}
			}
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0005807C File Offset: 0x0005747C
		[SecurityCritical]
		private void RefreshTablets()
		{
			foreach (PenContexts penContexts in this.__penContextsMap.Values)
			{
				penContexts.Disable(false);
			}
			this.WispTabletDevices.UpdateTablets();
			foreach (PenContexts penContexts2 in this.__penContextsMap.Values)
			{
				penContexts2.Enable();
			}
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00058140 File Offset: 0x00057540
		[SecurityCritical]
		private int GetDeviceCount()
		{
			PenThread penThread = null;
			TabletDeviceCollection tabletDevices = this.TabletDevices;
			if (tabletDevices != null && tabletDevices.Count > 0)
			{
				penThread = tabletDevices[0].As<WispTabletDevice>().PenThread;
			}
			if (penThread != null)
			{
				TabletDeviceInfo[] array = penThread.WorkerGetTabletsInfo();
				return array.Length;
			}
			return -1;
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x00058184 File Offset: 0x00057584
		[SecurityCritical]
		private void OnScreenMeasurementsChanged()
		{
			if (!this._updatingScreenMeasurements)
			{
				this._updatingScreenMeasurements = true;
				base.Dispatcher.BeginInvoke(DispatcherPriority.Background, this._processDisplayChanged, null);
			}
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x000581B4 File Offset: 0x000575B4
		[SecurityCritical]
		internal void OnWindowEnableChanged(IntPtr hwnd, bool disabled)
		{
			HwndSource hwndSource = HwndSource.CriticalFromHwnd(hwnd);
			if (hwndSource != null)
			{
				PenContexts penContextsFromHwnd = this.GetPenContextsFromHwnd(hwndSource);
				if (penContextsFromHwnd != null)
				{
					penContextsFromHwnd.IsWindowDisabled = disabled;
				}
			}
			if (!disabled && this._currentStylusDevice != null)
			{
				if (this._currentStylusDevice.InAir || !this._currentStylusDevice.GestureWasFired)
				{
					this._mouseLeftButtonState = MouseButtonState.Released;
					this._mouseRightButtonState = MouseButtonState.Released;
					return;
				}
				this._mouseLeftButtonState = (this._currentStylusDevice.LeftIsActiveMouseButton ? MouseButtonState.Pressed : MouseButtonState.Released);
				this._mouseRightButtonState = ((!this._currentStylusDevice.LeftIsActiveMouseButton) ? MouseButtonState.Pressed : MouseButtonState.Released);
			}
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x00058240 File Offset: 0x00057640
		[SecuritySafeCritical]
		internal object ProcessDisplayChanged(object oInput)
		{
			this._updatingScreenMeasurements = false;
			if (this._tabletDeviceCollection != null)
			{
				foreach (object obj in ((IEnumerable)this._tabletDeviceCollection))
				{
					TabletDevice tabletDevice = (TabletDevice)obj;
					WispTabletDevice wispTabletDevice = tabletDevice.As<WispTabletDevice>();
					if (wispTabletDevice != null)
					{
						wispTabletDevice.UpdateScreenMeasurements();
					}
				}
			}
			return null;
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x000582C0 File Offset: 0x000576C0
		internal Matrix GetTabletToViewTransform(TabletDevice tabletDevice)
		{
			Matrix transformToDevice = this._transformToDevice;
			transformToDevice.Invert();
			return transformToDevice * tabletDevice.As<TabletDeviceBase>().TabletToScreen;
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x000582EC File Offset: 0x000576EC
		internal override Point DeviceUnitsFromMeasureUnits(Point measurePoint)
		{
			Point result = measurePoint * this._transformToDevice;
			result.X = (double)((int)Math.Round(result.X));
			result.Y = (double)((int)Math.Round(result.Y));
			return result;
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00058334 File Offset: 0x00057734
		internal override Point MeasureUnitsFromDeviceUnits(Point measurePoint)
		{
			Matrix transformToDevice = this._transformToDevice;
			transformToDevice.Invert();
			return measurePoint * transformToDevice;
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060016C3 RID: 5827 RVA: 0x00058358 File Offset: 0x00057758
		private DeferredElementTreeState StylusOverTreeState
		{
			get
			{
				if (this._stylusOverTreeState == null)
				{
					this._stylusOverTreeState = new DeferredElementTreeState();
				}
				return this._stylusOverTreeState;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x00058380 File Offset: 0x00057780
		private DeferredElementTreeState StylusCaptureWithinTreeState
		{
			get
			{
				if (this._stylusCaptureWithinTreeState == null)
				{
					this._stylusCaptureWithinTreeState = new DeferredElementTreeState();
				}
				return this._stylusCaptureWithinTreeState;
			}
		}

		// Token: 0x04000C33 RID: 3123
		private Matrix _transformToDevice = Matrix.Identity;

		// Token: 0x04000C34 RID: 3124
		private bool _transformInitialized;

		// Token: 0x04000C35 RID: 3125
		private SecurityCriticalData<InputManager> _inputManager;

		// Token: 0x04000C36 RID: 3126
		private DispatcherOperationCallback _dlgInputManagerProcessInput;

		// Token: 0x04000C37 RID: 3127
		private object _stylusEventQueueLock = new object();

		// Token: 0x04000C38 RID: 3128
		[SecurityCritical]
		private Queue<RawStylusInputReport> _queueStylusEvents = new Queue<RawStylusInputReport>();

		// Token: 0x04000C39 RID: 3129
		private int _lastStylusDeviceId;

		// Token: 0x04000C3A RID: 3130
		private bool _lastMouseMoveFromStylus = true;

		// Token: 0x04000C3B RID: 3131
		private MouseButtonState _mouseLeftButtonState;

		// Token: 0x04000C3C RID: 3132
		private MouseButtonState _mouseRightButtonState;

		// Token: 0x04000C3D RID: 3133
		private StylusPlugInCollection _activeMousePlugInCollection;

		// Token: 0x04000C3E RID: 3134
		private StylusPointDescription _mousePointDescription;

		// Token: 0x04000C3F RID: 3135
		private EventHandler _shutdownHandler;

		// Token: 0x04000C40 RID: 3136
		private bool _tabletDeviceCollectionDisposed;

		// Token: 0x04000C41 RID: 3137
		private WispTabletDeviceCollection _tabletDeviceCollection;

		// Token: 0x04000C42 RID: 3138
		private WispStylusDevice _currentStylusDevice;

		// Token: 0x04000C43 RID: 3139
		private int _lastInRangeTime;

		// Token: 0x04000C44 RID: 3140
		private bool _triedDeferringMouseMove;

		// Token: 0x04000C45 RID: 3141
		[SecurityCritical]
		private RawMouseInputReport _deferredMouseMove;

		// Token: 0x04000C46 RID: 3142
		private DispatcherOperationCallback _processDeferredMouseMove;

		// Token: 0x04000C47 RID: 3143
		[SecurityCritical]
		private RawMouseInputReport _mouseDeactivateInputReport;

		// Token: 0x04000C48 RID: 3144
		private bool _inputEnabled;

		// Token: 0x04000C49 RID: 3145
		private bool _updatingScreenMeasurements;

		// Token: 0x04000C4A RID: 3146
		private DispatcherOperationCallback _processDisplayChanged;

		// Token: 0x04000C4B RID: 3147
		private object __penContextsLock = new object();

		// Token: 0x04000C4C RID: 3148
		[SecurityCritical]
		private Dictionary<object, PenContexts> __penContextsMap = new Dictionary<object, PenContexts>(2);

		// Token: 0x04000C4D RID: 3149
		private object __stylusDeviceLock = new object();

		// Token: 0x04000C4E RID: 3150
		private Dictionary<int, StylusDevice> __stylusDeviceMap = new Dictionary<int, StylusDevice>(2);

		// Token: 0x04000C4F RID: 3151
		private bool _inDragDrop;

		// Token: 0x04000C50 RID: 3152
		private bool _leavingDragDrop;

		// Token: 0x04000C51 RID: 3153
		private bool _processingQueuedEvent;

		// Token: 0x04000C52 RID: 3154
		private bool _stylusDeviceInRange;

		// Token: 0x04000C53 RID: 3155
		private bool _seenRealMouseActivate;

		// Token: 0x04000C54 RID: 3156
		private int _lastKnownDeviceCount = -1;

		// Token: 0x04000C55 RID: 3157
		private Dictionary<StylusDeviceBase, RawStylusInputReport> _lastMovesQueued = new Dictionary<StylusDeviceBase, RawStylusInputReport>();

		// Token: 0x04000C56 RID: 3158
		private Dictionary<StylusDeviceBase, RawStylusInputReport> _coalescedMoves = new Dictionary<StylusDeviceBase, RawStylusInputReport>();

		// Token: 0x04000C57 RID: 3159
		private object _coalesceLock = new object();

		// Token: 0x04000C58 RID: 3160
		private IInputElement _stylusCapture;

		// Token: 0x04000C59 RID: 3161
		private IInputElement _stylusOver;

		// Token: 0x04000C5A RID: 3162
		private DeferredElementTreeState _stylusOverTreeState;

		// Token: 0x04000C5B RID: 3163
		private DeferredElementTreeState _stylusCaptureWithinTreeState;

		// Token: 0x04000C5C RID: 3164
		private DependencyPropertyChangedEventHandler _overIsEnabledChangedEventHandler;

		// Token: 0x04000C5D RID: 3165
		private DependencyPropertyChangedEventHandler _overIsVisibleChangedEventHandler;

		// Token: 0x04000C5E RID: 3166
		private DependencyPropertyChangedEventHandler _overIsHitTestVisibleChangedEventHandler;

		// Token: 0x04000C5F RID: 3167
		private DispatcherOperationCallback _reevaluateStylusOverDelegate;

		// Token: 0x04000C60 RID: 3168
		private DispatcherOperation _reevaluateStylusOverOperation;

		// Token: 0x04000C61 RID: 3169
		private DependencyPropertyChangedEventHandler _captureIsEnabledChangedEventHandler;

		// Token: 0x04000C62 RID: 3170
		private DependencyPropertyChangedEventHandler _captureIsVisibleChangedEventHandler;

		// Token: 0x04000C63 RID: 3171
		private DependencyPropertyChangedEventHandler _captureIsHitTestVisibleChangedEventHandler;

		// Token: 0x04000C64 RID: 3172
		private DispatcherOperationCallback _reevaluateCaptureDelegate;

		// Token: 0x04000C65 RID: 3173
		private DispatcherOperation _reevaluateCaptureOperation;

		// Token: 0x02000822 RID: 2082
		internal class StagingAreaInputItemList : List<StagingAreaInputItem>
		{
			// Token: 0x06005637 RID: 22071 RVA: 0x001625E0 File Offset: 0x001619E0
			internal void AddItem(StagingAreaInputItem item)
			{
				base.Add(item);
				this.IncrementVersion();
			}

			// Token: 0x170011B0 RID: 4528
			// (get) Token: 0x06005638 RID: 22072 RVA: 0x001625FC File Offset: 0x001619FC
			internal long Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x06005639 RID: 22073 RVA: 0x00162610 File Offset: 0x00161A10
			internal long IncrementVersion()
			{
				long num = this._version + 1L;
				this._version = num;
				return num;
			}

			// Token: 0x04002783 RID: 10115
			private long _version;
		}
	}
}
