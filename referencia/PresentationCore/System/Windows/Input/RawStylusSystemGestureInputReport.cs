using System;
using System.ComponentModel;
using System.Security;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	// Token: 0x020002AA RID: 682
	internal class RawStylusSystemGestureInputReport : RawStylusInputReport
	{
		// Token: 0x060013EE RID: 5102 RVA: 0x0004A644 File Offset: 0x00049A44
		internal static bool IsValidSystemGesture(SystemGesture systemGesture, bool allowFlick, bool allowDoubleTap)
		{
			if (systemGesture != SystemGesture.None)
			{
				switch (systemGesture)
				{
				case SystemGesture.Tap:
				case SystemGesture.RightTap:
				case SystemGesture.Drag:
				case SystemGesture.RightDrag:
				case SystemGesture.HoldEnter:
				case SystemGesture.HoldLeave:
				case SystemGesture.HoverEnter:
				case SystemGesture.HoverLeave:
					return true;
				case (SystemGesture)17:
					return allowDoubleTap;
				case (SystemGesture)25:
				case (SystemGesture)26:
				case (SystemGesture)27:
				case (SystemGesture)28:
				case (SystemGesture)29:
				case (SystemGesture)30:
					break;
				case SystemGesture.Flick:
					return allowFlick;
				default:
					if (systemGesture == SystemGesture.TwoFingerTap)
					{
						return true;
					}
					break;
				}
				return false;
			}
			return true;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0004A6AC File Offset: 0x00049AAC
		[SecuritySafeCritical]
		internal RawStylusSystemGestureInputReport(InputMode mode, int timestamp, PresentationSource inputSource, Func<StylusPointDescription> stylusPointDescGenerator, int tabletId, int stylusDeviceId, SystemGesture systemGesture, int gestureX, int gestureY, int buttonState) : base(mode, timestamp, inputSource, RawStylusActions.SystemGesture, stylusPointDescGenerator, tabletId, stylusDeviceId, new int[0])
		{
			this.Initialize(systemGesture, gestureX, gestureY, buttonState);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0004A6E4 File Offset: 0x00049AE4
		[SecuritySafeCritical]
		internal RawStylusSystemGestureInputReport(InputMode mode, int timestamp, PresentationSource inputSource, PenContext penContext, int tabletId, int stylusDeviceId, SystemGesture systemGesture, int gestureX, int gestureY, int buttonState) : base(mode, timestamp, inputSource, penContext, RawStylusActions.SystemGesture, tabletId, stylusDeviceId, new int[0])
		{
			this.Initialize(systemGesture, gestureX, gestureY, buttonState);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0004A71C File Offset: 0x00049B1C
		private void Initialize(SystemGesture systemGesture, int gestureX, int gestureY, int buttonState)
		{
			if (!RawStylusSystemGestureInputReport.IsValidSystemGesture(systemGesture, true, true))
			{
				throw new InvalidEnumArgumentException(SR.Get("Enum_Invalid", new object[]
				{
					"systemGesture"
				}));
			}
			this._id = systemGesture;
			this._gestureX = gestureX;
			this._gestureY = gestureY;
			this._buttonState = buttonState;
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x0004A770 File Offset: 0x00049B70
		internal SystemGesture SystemGesture
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x0004A784 File Offset: 0x00049B84
		internal int GestureX
		{
			get
			{
				return this._gestureX;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x0004A798 File Offset: 0x00049B98
		internal int GestureY
		{
			get
			{
				return this._gestureY;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x0004A7AC File Offset: 0x00049BAC
		internal int ButtonState
		{
			get
			{
				return this._buttonState;
			}
		}

		// Token: 0x04000AD6 RID: 2774
		internal const SystemGesture InternalSystemGestureDoubleTap = (SystemGesture)17;

		// Token: 0x04000AD7 RID: 2775
		private SystemGesture _id;

		// Token: 0x04000AD8 RID: 2776
		private int _gestureX;

		// Token: 0x04000AD9 RID: 2777
		private int _gestureY;

		// Token: 0x04000ADA RID: 2778
		private int _buttonState;
	}
}
