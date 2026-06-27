using System;
using System.Collections.Generic;
using System.Security;
using System.Windows.Media;
using MS.Internal;
using MS.Win32.Pointer;

namespace System.Windows.Input.StylusPointer
{
	// Token: 0x020002EA RID: 746
	internal class PointerInteractionEngine : IDisposable
	{
		// Token: 0x14000169 RID: 361
		// (add) Token: 0x06001742 RID: 5954 RVA: 0x0005B6EC File Offset: 0x0005AAEC
		// (remove) Token: 0x06001743 RID: 5955 RVA: 0x0005B724 File Offset: 0x0005AB24
		internal event EventHandler<RawStylusSystemGestureInputReport> InteractionDetected;

		// Token: 0x06001744 RID: 5956 RVA: 0x0005B75C File Offset: 0x0005AB5C
		[SecurityCritical]
		internal PointerInteractionEngine(PointerStylusDevice stylusDevice, List<UnsafeNativeMethods.INTERACTION_CONTEXT_CONFIGURATION> configuration = null)
		{
			this._stylusDevice = stylusDevice;
			TabletDeviceType type = this._stylusDevice.TabletDevice.Type;
			IntPtr zero = IntPtr.Zero;
			UnsafeNativeMethods.CreateInteractionContext(out zero);
			this._interactionContext = new SecurityCriticalDataForSet<IntPtr>(zero);
			if (configuration == null)
			{
				configuration = PointerInteractionEngine.DefaultConfiguration;
			}
			if (this._interactionContext.Value != IntPtr.Zero)
			{
				UnsafeNativeMethods.SetPropertyInteractionContext(this._interactionContext.Value, UnsafeNativeMethods.INTERACTION_CONTEXT_PROPERTY.INTERACTION_CONTEXT_PROPERTY_FILTER_POINTERS, Convert.ToUInt32(false));
				UnsafeNativeMethods.SetPropertyInteractionContext(this._interactionContext.Value, UnsafeNativeMethods.INTERACTION_CONTEXT_PROPERTY.INTERACTION_CONTEXT_PROPERTY_MEASUREMENT_UNITS, 1U);
				UnsafeNativeMethods.SetInteractionConfigurationInteractionContext(this._interactionContext.Value, (uint)configuration.Count, configuration.ToArray());
				this._callbackDelegate = new UnsafeNativeMethods.INTERACTION_CONTEXT_OUTPUT_CALLBACK(this.Callback);
				UnsafeNativeMethods.RegisterOutputCallbackInteractionContext(this._interactionContext.Value, this._callbackDelegate, (IntPtr)0);
			}
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0005B840 File Offset: 0x0005AC40
		[SecurityCritical]
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (this._interactionContext.Value != IntPtr.Zero)
				{
					UnsafeNativeMethods.DestroyInteractionContext(this._interactionContext.Value);
					this._interactionContext.Value = IntPtr.Zero;
				}
				this._disposed = true;
			}
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x0005B894 File Offset: 0x0005AC94
		[SecurityCritical]
		~PointerInteractionEngine()
		{
			this.Dispose(false);
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x0005B8D0 File Offset: 0x0005ACD0
		[SecurityCritical]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x0005B8EC File Offset: 0x0005ACEC
		[SecurityCritical]
		internal void Update(RawStylusInputReport rsir)
		{
			try
			{
				UnsafeNativeMethods.BufferPointerPacketsInteractionContext(this._interactionContext.Value, 1U, new UnsafeNativeMethods.POINTER_INFO[]
				{
					this._stylusDevice.CurrentPointerInfo
				});
				this.DetectHover();
				this.DetectFlick(rsir);
				UnsafeNativeMethods.ProcessBufferedPacketsInteractionContext(this._interactionContext.Value);
			}
			catch
			{
			}
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0005B960 File Offset: 0x0005AD60
		[SecurityCritical]
		private void Callback(IntPtr clientData, ref UnsafeNativeMethods.INTERACTION_CONTEXT_OUTPUT output)
		{
			SystemGesture systemGesture = SystemGesture.None;
			switch (output.interactionId)
			{
			case UnsafeNativeMethods.INTERACTION_ID.INTERACTION_ID_MANIPULATION:
				systemGesture = this.DetectDragOrFlick(output);
				break;
			case UnsafeNativeMethods.INTERACTION_ID.INTERACTION_ID_TAP:
				systemGesture = SystemGesture.Tap;
				break;
			case UnsafeNativeMethods.INTERACTION_ID.INTERACTION_ID_SECONDARY_TAP:
				systemGesture = SystemGesture.RightTap;
				break;
			case UnsafeNativeMethods.INTERACTION_ID.INTERACTION_ID_HOLD:
				this._firedHold = true;
				if (output.interactionFlags.HasFlag(UnsafeNativeMethods.INTERACTION_FLAGS.INTERACTION_FLAG_BEGIN))
				{
					systemGesture = SystemGesture.HoldEnter;
				}
				else
				{
					systemGesture = SystemGesture.HoldLeave;
				}
				break;
			}
			if (systemGesture != SystemGesture.None)
			{
				EventHandler<RawStylusSystemGestureInputReport> interactionDetected = this.InteractionDetected;
				if (interactionDetected == null)
				{
					return;
				}
				interactionDetected(this, new RawStylusSystemGestureInputReport(InputMode.Foreground, Environment.TickCount, this._stylusDevice.CriticalActiveSource, null, -1, -1, systemGesture, Convert.ToInt32(output.x), Convert.ToInt32(output.y), 0));
			}
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x0005BA18 File Offset: 0x0005AE18
		[SecurityCritical]
		private void DetectFlick(RawStylusInputReport rsir)
		{
			PointerFlickEngine flickEngine = this._flickEngine;
			if (flickEngine != null)
			{
				flickEngine.Update(rsir, false);
			}
			if (rsir.Actions == RawStylusActions.Up)
			{
				PointerFlickEngine flickEngine2 = this._flickEngine;
				bool? flag;
				if (flickEngine2 == null)
				{
					flag = null;
				}
				else
				{
					PointerFlickEngine.FlickResult result = flickEngine2.Result;
					flag = ((result != null) ? new bool?(result.CanBeFlick) : null);
				}
				bool? flag2 = flag;
				if (flag2.GetValueOrDefault())
				{
					EventHandler<RawStylusSystemGestureInputReport> interactionDetected = this.InteractionDetected;
					if (interactionDetected != null)
					{
						interactionDetected(this, new RawStylusSystemGestureInputReport(InputMode.Foreground, Environment.TickCount, this._stylusDevice.CriticalActiveSource, null, -1, -1, SystemGesture.Flick, Convert.ToInt32(this._flickEngine.Result.TabletStart.X), Convert.ToInt32(this._flickEngine.Result.TabletStart.Y), 0));
					}
					this._firedFlick = true;
				}
			}
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x0005BAF0 File Offset: 0x0005AEF0
		[SecurityCritical]
		private void DetectHover()
		{
			if (this._stylusDevice.TabletDevice.Type == TabletDeviceType.Stylus)
			{
				SystemGesture systemGesture = SystemGesture.None;
				if (this._stylusDevice.IsNew)
				{
					this._hoverState = PointerInteractionEngine.HoverState.AwaitingHover;
				}
				switch (this._hoverState)
				{
				case PointerInteractionEngine.HoverState.AwaitingHover:
					if (this._stylusDevice.InAir)
					{
						this._hoverStartTicks = this._stylusDevice.TimeStamp;
						this._hoverState = PointerInteractionEngine.HoverState.TimingHover;
					}
					break;
				case PointerInteractionEngine.HoverState.TimingHover:
					if (this._stylusDevice.InAir)
					{
						if (this._stylusDevice.TimeStamp < this._hoverStartTicks)
						{
							this._hoverStartTicks = this._stylusDevice.TimeStamp;
						}
						else if (this._stylusDevice.TimeStamp - this._hoverStartTicks > 275U)
						{
							systemGesture = SystemGesture.HoverEnter;
							this._hoverState = PointerInteractionEngine.HoverState.InHover;
						}
					}
					else if (this._stylusDevice.IsDown)
					{
						this._hoverState = PointerInteractionEngine.HoverState.HoverCancelled;
					}
					break;
				case PointerInteractionEngine.HoverState.HoverCancelled:
					if (this._stylusDevice.InAir)
					{
						this._hoverState = PointerInteractionEngine.HoverState.AwaitingHover;
					}
					break;
				case PointerInteractionEngine.HoverState.InHover:
					if (this._stylusDevice.IsDown || !this._stylusDevice.InRange)
					{
						systemGesture = SystemGesture.HoverLeave;
						this._hoverState = PointerInteractionEngine.HoverState.HoverCancelled;
					}
					break;
				}
				if (systemGesture != SystemGesture.None)
				{
					EventHandler<RawStylusSystemGestureInputReport> interactionDetected = this.InteractionDetected;
					if (interactionDetected == null)
					{
						return;
					}
					interactionDetected(this, new RawStylusSystemGestureInputReport(InputMode.Foreground, Environment.TickCount, this._stylusDevice.CriticalActiveSource, null, -1, -1, systemGesture, Convert.ToInt32(this._stylusDevice.RawStylusPoint.X), Convert.ToInt32(this._stylusDevice.RawStylusPoint.Y), 0));
				}
			}
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0005BC84 File Offset: 0x0005B084
		[SecurityCritical]
		private SystemGesture DetectDragOrFlick(UnsafeNativeMethods.INTERACTION_CONTEXT_OUTPUT output)
		{
			SystemGesture result = SystemGesture.None;
			if (output.interactionFlags.HasFlag(UnsafeNativeMethods.INTERACTION_FLAGS.INTERACTION_FLAG_END))
			{
				this._firedDrag = false;
				this._firedHold = false;
				this._firedFlick = false;
			}
			else if (!this._firedDrag && !this._firedFlick)
			{
				PointerFlickEngine flickEngine = this._flickEngine;
				bool? flag;
				if (flickEngine == null)
				{
					flag = null;
				}
				else
				{
					PointerFlickEngine.FlickResult result2 = flickEngine.Result;
					flag = ((result2 != null) ? new bool?(!result2.CanBeFlick) : null);
				}
				if (flag ?? true)
				{
					DpiScale dpi = VisualTreeHelper.GetDpi(this._stylusDevice.CriticalActiveSource.RootVisual);
					double num = (double)output.arguments.manipulation.cumulative.translationX / dpi.PixelsPerInchX;
					double num2 = (double)output.arguments.manipulation.cumulative.translationY / dpi.PixelsPerInchY;
					if (num > 0.106299 || num2 > 0.106299)
					{
						result = (this._firedHold ? SystemGesture.RightDrag : SystemGesture.Drag);
						this._firedDrag = true;
					}
				}
			}
			return result;
		}

		// Token: 0x04000CCA RID: 3274
		private const int HoverActivationThresholdTicks = 275;

		// Token: 0x04000CCB RID: 3275
		private const double DragThresholdInches = 0.106299;

		// Token: 0x04000CCC RID: 3276
		private static List<UnsafeNativeMethods.INTERACTION_CONTEXT_CONFIGURATION> DefaultConfiguration = new List<UnsafeNativeMethods.INTERACTION_CONTEXT_CONFIGURATION>
		{
			new UnsafeNativeMethods.INTERACTION_CONTEXT_CONFIGURATION
			{
				enable = UnsafeNativeMethods.INTERACTION_CONFIGURATION_FLAGS.INTERACTION_CONFIGURATION_FLAG_MANIPULATION,
				interactionId = UnsafeNativeMethods.INTERACTION_ID.INTERACTION_ID_TAP
			},
			new UnsafeNativeMethods.INTERACTION_CONTEXT_CONFIGURATION
			{
				enable = UnsafeNativeMethods.INTERACTION_CONFIGURATION_FLAGS.INTERACTION_CONFIGURATION_FLAG_MANIPULATION,
				interactionId = UnsafeNativeMethods.INTERACTION_ID.INTERACTION_ID_HOLD
			},
			new UnsafeNativeMethods.INTERACTION_CONTEXT_CONFIGURATION
			{
				enable = UnsafeNativeMethods.INTERACTION_CONFIGURATION_FLAGS.INTERACTION_CONFIGURATION_FLAG_MANIPULATION,
				interactionId = UnsafeNativeMethods.INTERACTION_ID.INTERACTION_ID_SECONDARY_TAP
			},
			new UnsafeNativeMethods.INTERACTION_CONTEXT_CONFIGURATION
			{
				enable = (UnsafeNativeMethods.INTERACTION_CONFIGURATION_FLAGS.INTERACTION_CONFIGURATION_FLAG_MANIPULATION | UnsafeNativeMethods.INTERACTION_CONFIGURATION_FLAGS.INTERACTION_CONFIGURATION_FLAG_MANIPULATION_TRANSLATION_X | UnsafeNativeMethods.INTERACTION_CONFIGURATION_FLAGS.INTERACTION_CONFIGURATION_FLAG_MANIPULATION_TRANSLATION_Y | UnsafeNativeMethods.INTERACTION_CONFIGURATION_FLAGS.INTERACTION_CONFIGURATION_FLAG_MANIPULATION_TRANSLATION_INERTIA),
				interactionId = UnsafeNativeMethods.INTERACTION_ID.INTERACTION_ID_MANIPULATION
			}
		};

		// Token: 0x04000CCD RID: 3277
		private SecurityCriticalDataForSet<IntPtr> _interactionContext = new SecurityCriticalDataForSet<IntPtr>(IntPtr.Zero);

		// Token: 0x04000CCE RID: 3278
		private PointerStylusDevice _stylusDevice;

		// Token: 0x04000CCF RID: 3279
		private UnsafeNativeMethods.INTERACTION_CONTEXT_OUTPUT_CALLBACK _callbackDelegate;

		// Token: 0x04000CD0 RID: 3280
		private bool _firedDrag;

		// Token: 0x04000CD1 RID: 3281
		private bool _firedHold;

		// Token: 0x04000CD2 RID: 3282
		private bool _firedFlick;

		// Token: 0x04000CD3 RID: 3283
		private PointerInteractionEngine.HoverState _hoverState;

		// Token: 0x04000CD4 RID: 3284
		private uint _hoverStartTicks;

		// Token: 0x04000CD5 RID: 3285
		private PointerFlickEngine _flickEngine;

		// Token: 0x04000CD7 RID: 3287
		private bool _disposed;

		// Token: 0x02000825 RID: 2085
		private enum HoverState
		{
			// Token: 0x04002794 RID: 10132
			AwaitingHover,
			// Token: 0x04002795 RID: 10133
			TimingHover,
			// Token: 0x04002796 RID: 10134
			HoverCancelled,
			// Token: 0x04002797 RID: 10135
			InHover
		}
	}
}
