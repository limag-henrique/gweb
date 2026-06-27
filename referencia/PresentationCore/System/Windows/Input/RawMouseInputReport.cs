using System;
using System.ComponentModel;
using System.Security;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	// Token: 0x02000291 RID: 657
	[FriendAccessAllowed]
	internal class RawMouseInputReport : InputReport
	{
		// Token: 0x0600133D RID: 4925 RVA: 0x00048080 File Offset: 0x00047480
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public RawMouseInputReport(InputMode mode, int timestamp, PresentationSource inputSource, RawMouseActions actions, int x, int y, int wheel, IntPtr extraInformation) : base(inputSource, InputType.Mouse, mode, timestamp)
		{
			if (!RawMouseInputReport.IsValidRawMouseActions(actions))
			{
				throw new InvalidEnumArgumentException("actions", (int)actions, typeof(RawMouseActions));
			}
			this._actions = actions;
			this._x = x;
			this._y = y;
			this._wheel = wheel;
			this._extraInformation = new SecurityCriticalData<IntPtr>(extraInformation);
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x000480E4 File Offset: 0x000474E4
		public RawMouseActions Actions
		{
			get
			{
				return this._actions;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x000480F8 File Offset: 0x000474F8
		public int X
		{
			get
			{
				return this._x;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x0004810C File Offset: 0x0004750C
		public int Y
		{
			get
			{
				return this._y;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x00048120 File Offset: 0x00047520
		public int Wheel
		{
			get
			{
				return this._wheel;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x00048134 File Offset: 0x00047534
		public IntPtr ExtraInformation
		{
			[SecurityCritical]
			get
			{
				return this._extraInformation.Value;
			}
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0004814C File Offset: 0x0004754C
		internal static bool IsValidRawMouseActions(RawMouseActions actions)
		{
			return actions == RawMouseActions.None || (((RawMouseActions.AttributesChanged | RawMouseActions.Activate | RawMouseActions.Deactivate | RawMouseActions.RelativeMove | RawMouseActions.AbsoluteMove | RawMouseActions.VirtualDesktopMove | RawMouseActions.Button1Press | RawMouseActions.Button1Release | RawMouseActions.Button2Press | RawMouseActions.Button2Release | RawMouseActions.Button3Press | RawMouseActions.Button3Release | RawMouseActions.Button4Press | RawMouseActions.Button4Release | RawMouseActions.Button5Press | RawMouseActions.Button5Release | RawMouseActions.VerticalWheelRotate | RawMouseActions.HorizontalWheelRotate | RawMouseActions.QueryCursor | RawMouseActions.CancelCapture) & actions) == actions && ((RawMouseActions.Deactivate & actions) != actions || RawMouseActions.Deactivate == actions) && ((RawMouseActions.Button1Press | RawMouseActions.Button1Release) & actions) != (RawMouseActions.Button1Press | RawMouseActions.Button1Release) && ((RawMouseActions.Button2Press | RawMouseActions.Button2Release) & actions) != (RawMouseActions.Button2Press | RawMouseActions.Button2Release) && ((RawMouseActions.Button3Press | RawMouseActions.Button3Release) & actions) != (RawMouseActions.Button3Press | RawMouseActions.Button3Release) && ((RawMouseActions.Button4Press | RawMouseActions.Button4Release) & actions) != (RawMouseActions.Button4Press | RawMouseActions.Button4Release) && ((RawMouseActions.Button5Press | RawMouseActions.Button5Release) & actions) != (RawMouseActions.Button5Press | RawMouseActions.Button5Release));
		}

		// Token: 0x04000A66 RID: 2662
		private RawMouseActions _actions;

		// Token: 0x04000A67 RID: 2663
		private int _x;

		// Token: 0x04000A68 RID: 2664
		private int _y;

		// Token: 0x04000A69 RID: 2665
		private int _wheel;

		// Token: 0x04000A6A RID: 2666
		internal bool _isSynchronize;

		// Token: 0x04000A6B RID: 2667
		private SecurityCriticalData<IntPtr> _extraInformation;
	}
}
