using System;

namespace System.Windows.Input
{
	// Token: 0x020002A6 RID: 678
	internal class MultiTouchSystemGestureLogic
	{
		// Token: 0x060013D1 RID: 5073 RVA: 0x0004A090 File Offset: 0x00049490
		internal MultiTouchSystemGestureLogic()
		{
			this._currentState = MultiTouchSystemGestureLogic.State.Idle;
			this.Reset();
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0004A0B0 File Offset: 0x000494B0
		internal SystemGesture? GenerateStaticGesture(RawStylusInputReport stylusInputReport)
		{
			RawStylusActions actions = stylusInputReport.Actions;
			if (actions == RawStylusActions.Down)
			{
				this.OnTouchDown(stylusInputReport);
				return null;
			}
			if (actions == RawStylusActions.Up)
			{
				return this.OnTouchUp(stylusInputReport);
			}
			if (actions != RawStylusActions.SystemGesture)
			{
				return null;
			}
			this.OnSystemGesture((RawStylusSystemGestureInputReport)stylusInputReport);
			return null;
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0004A110 File Offset: 0x00049510
		private void OnTouchDown(RawStylusInputReport stylusInputReport)
		{
			MultiTouchSystemGestureLogic.State currentState = this._currentState;
			if (currentState == MultiTouchSystemGestureLogic.State.Idle)
			{
				this.Reset();
				this._firstStylusDeviceId = new int?(stylusInputReport.StylusDeviceId);
				this._currentState = MultiTouchSystemGestureLogic.State.OneFingerDown;
				this._firstDownTime = Environment.TickCount;
				return;
			}
			if (currentState != MultiTouchSystemGestureLogic.State.OneFingerDown)
			{
				return;
			}
			this._secondStylusDeviceId = new int?(stylusInputReport.StylusDeviceId);
			this._currentState = MultiTouchSystemGestureLogic.State.TwoFingersDown;
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0004A170 File Offset: 0x00049570
		private SystemGesture? OnTouchUp(RawStylusInputReport stylusInputReport)
		{
			switch (this._currentState)
			{
			case MultiTouchSystemGestureLogic.State.OneFingerDown:
				if (this.IsTrackedStylusId(stylusInputReport.StylusDeviceId))
				{
					this._currentState = MultiTouchSystemGestureLogic.State.Idle;
				}
				break;
			case MultiTouchSystemGestureLogic.State.TwoFingersDown:
				if (this.IsTrackedStylusId(stylusInputReport.StylusDeviceId))
				{
					this._firstUpTime = Environment.TickCount;
					this._currentState = MultiTouchSystemGestureLogic.State.OneFingerInStaticGesture;
				}
				break;
			case MultiTouchSystemGestureLogic.State.OneFingerInStaticGesture:
				this._currentState = MultiTouchSystemGestureLogic.State.Idle;
				if (this.IsTwoFingerTap())
				{
					return new SystemGesture?(SystemGesture.TwoFingerTap);
				}
				break;
			case MultiTouchSystemGestureLogic.State.TwoFingersInWisptisGesture:
				if (this.IsTrackedStylusId(stylusInputReport.StylusDeviceId))
				{
					this._currentState = MultiTouchSystemGestureLogic.State.OneFingerInWisptisGesture;
				}
				break;
			case MultiTouchSystemGestureLogic.State.OneFingerInWisptisGesture:
				if (this.IsTrackedStylusId(stylusInputReport.StylusDeviceId))
				{
					this._currentState = MultiTouchSystemGestureLogic.State.Idle;
				}
				break;
			}
			return null;
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0004A230 File Offset: 0x00049630
		private void OnSystemGesture(RawStylusSystemGestureInputReport stylusInputReport)
		{
			switch (this._currentState)
			{
			case MultiTouchSystemGestureLogic.State.OneFingerDown:
			case MultiTouchSystemGestureLogic.State.OneFingerInStaticGesture:
			{
				SystemGesture systemGesture = stylusInputReport.SystemGesture;
				if (systemGesture - SystemGesture.Drag <= 1 || systemGesture == SystemGesture.Flick)
				{
					this._currentState = MultiTouchSystemGestureLogic.State.OneFingerInWisptisGesture;
				}
				break;
			}
			case MultiTouchSystemGestureLogic.State.TwoFingersDown:
			{
				SystemGesture systemGesture2 = stylusInputReport.SystemGesture;
				if (systemGesture2 - SystemGesture.Drag <= 1 || systemGesture2 == SystemGesture.Flick)
				{
					this._currentState = MultiTouchSystemGestureLogic.State.TwoFingersInWisptisGesture;
					return;
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0004A290 File Offset: 0x00049690
		private void Reset()
		{
			this._firstStylusDeviceId = null;
			this._secondStylusDeviceId = null;
			this._firstDownTime = 0;
			this._firstUpTime = 0;
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0004A2C4 File Offset: 0x000496C4
		private bool IsTrackedStylusId(int id)
		{
			int? num = this._firstStylusDeviceId;
			if (!(id == num.GetValueOrDefault() & num != null))
			{
				num = this._secondStylusDeviceId;
				return id == num.GetValueOrDefault() & num != null;
			}
			return true;
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0004A308 File Offset: 0x00049708
		private bool IsTwoFingerTap()
		{
			int tickCount = Environment.TickCount;
			int num = tickCount - this._firstDownTime;
			int num2 = tickCount - this._firstUpTime;
			return num2 < 150 && num < 1158;
		}

		// Token: 0x04000AB9 RID: 2745
		private MultiTouchSystemGestureLogic.State _currentState;

		// Token: 0x04000ABA RID: 2746
		private int? _firstStylusDeviceId;

		// Token: 0x04000ABB RID: 2747
		private int? _secondStylusDeviceId;

		// Token: 0x04000ABC RID: 2748
		private int _firstDownTime;

		// Token: 0x04000ABD RID: 2749
		private int _firstUpTime;

		// Token: 0x04000ABE RID: 2750
		private const int TwoFingerTapTime = 150;

		// Token: 0x04000ABF RID: 2751
		private const int RolloverTime = 1158;

		// Token: 0x0200080E RID: 2062
		private enum State
		{
			// Token: 0x04002727 RID: 10023
			Idle,
			// Token: 0x04002728 RID: 10024
			OneFingerDown,
			// Token: 0x04002729 RID: 10025
			TwoFingersDown,
			// Token: 0x0400272A RID: 10026
			OneFingerInStaticGesture,
			// Token: 0x0400272B RID: 10027
			TwoFingersInWisptisGesture,
			// Token: 0x0400272C RID: 10028
			OneFingerInWisptisGesture
		}
	}
}
