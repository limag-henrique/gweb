using System;
using System.ComponentModel;
using System.Security;
using System.Windows.Input.StylusPlugIns;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	// Token: 0x020002A9 RID: 681
	internal class RawStylusInputReport : InputReport
	{
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x0004A374 File Offset: 0x00049774
		// (set) Token: 0x060013DC RID: 5084 RVA: 0x0004A38C File Offset: 0x0004978C
		internal RawStylusInput RawStylusInput
		{
			get
			{
				return this._rawStylusInput.Value;
			}
			[SecurityCritical]
			set
			{
				this._rawStylusInput.Value = value;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x0004A3A8 File Offset: 0x000497A8
		// (set) Token: 0x060013DE RID: 5086 RVA: 0x0004A3BC File Offset: 0x000497BC
		internal bool Synchronized
		{
			get
			{
				return this._isSynchronize;
			}
			set
			{
				this._isSynchronize = value;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x0004A3D0 File Offset: 0x000497D0
		internal RawStylusActions Actions
		{
			get
			{
				return this._actions;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x0004A3E4 File Offset: 0x000497E4
		internal int TabletDeviceId
		{
			get
			{
				return this._tabletDeviceId;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x0004A3F8 File Offset: 0x000497F8
		// (set) Token: 0x060013E2 RID: 5090 RVA: 0x0004A40C File Offset: 0x0004980C
		internal PenContext PenContext { [SecurityCritical] get; private set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x0004A420 File Offset: 0x00049820
		internal StylusPointDescription StylusPointDescription
		{
			[SecuritySafeCritical]
			get
			{
				return this._stylusPointDescGenerator();
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x0004A438 File Offset: 0x00049838
		internal int StylusDeviceId
		{
			get
			{
				return this._stylusDeviceId;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x0004A44C File Offset: 0x0004984C
		// (set) Token: 0x060013E6 RID: 5094 RVA: 0x0004A460 File Offset: 0x00049860
		internal StylusDevice StylusDevice
		{
			get
			{
				return this._stylusDevice;
			}
			set
			{
				this._stylusDevice = value;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x0004A474 File Offset: 0x00049874
		// (set) Token: 0x060013E8 RID: 5096 RVA: 0x0004A488 File Offset: 0x00049888
		internal bool IsQueued
		{
			get
			{
				return this._isQueued;
			}
			set
			{
				this._isQueued = value;
			}
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0004A49C File Offset: 0x0004989C
		[SecuritySafeCritical]
		internal RawStylusInputReport(InputMode mode, int timestamp, PresentationSource inputSource, PenContext penContext, RawStylusActions actions, int tabletDeviceId, int stylusDeviceId, int[] data) : this(mode, timestamp, inputSource, actions, () => penContext.StylusPointDescription, tabletDeviceId, stylusDeviceId, data)
		{
			if (!RawStylusActionsHelper.IsValid(actions))
			{
				throw new InvalidEnumArgumentException(SR.Get("Enum_Invalid", new object[]
				{
					"actions"
				}));
			}
			if (data == null && actions != RawStylusActions.InRange)
			{
				throw new ArgumentNullException("data");
			}
			this._actions = actions;
			this._data = data;
			this._isSynchronize = false;
			this._tabletDeviceId = tabletDeviceId;
			this._stylusDeviceId = stylusDeviceId;
			this.PenContext = penContext;
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0004A544 File Offset: 0x00049944
		[SecuritySafeCritical]
		internal RawStylusInputReport(InputMode mode, int timestamp, PresentationSource inputSource, RawStylusActions actions, Func<StylusPointDescription> stylusPointDescGenerator, int tabletDeviceId, int stylusDeviceId, int[] data) : base(inputSource, InputType.Stylus, mode, timestamp)
		{
			if (!RawStylusActionsHelper.IsValid(actions))
			{
				throw new InvalidEnumArgumentException(SR.Get("Enum_Invalid", new object[]
				{
					"actions"
				}));
			}
			if (data == null && actions != RawStylusActions.InRange)
			{
				throw new ArgumentNullException("data");
			}
			this._actions = actions;
			this._stylusPointDescGenerator = stylusPointDescGenerator;
			this._data = data;
			this._isSynchronize = false;
			this._tabletDeviceId = tabletDeviceId;
			this._stylusDeviceId = stylusDeviceId;
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0004A5C8 File Offset: 0x000499C8
		[SecuritySafeCritical]
		internal int[] GetRawPacketData()
		{
			if (this._data == null)
			{
				return null;
			}
			return (int[])this._data.Clone();
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0004A5F0 File Offset: 0x000499F0
		[SecuritySafeCritical]
		internal Point GetLastTabletPoint()
		{
			int inputArrayLengthPerPoint = this.StylusPointDescription.GetInputArrayLengthPerPoint();
			int num = this._data.Length - inputArrayLengthPerPoint;
			return new Point((double)this._data[num], (double)this._data[num + 1]);
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x0004A630 File Offset: 0x00049A30
		internal int[] Data
		{
			[SecurityCritical]
			get
			{
				return this._data;
			}
		}

		// Token: 0x04000ACC RID: 2764
		private RawStylusActions _actions;

		// Token: 0x04000ACD RID: 2765
		private int _tabletDeviceId;

		// Token: 0x04000ACE RID: 2766
		private int _stylusDeviceId;

		// Token: 0x04000ACF RID: 2767
		private bool _isQueued;

		// Token: 0x04000AD0 RID: 2768
		[SecurityCritical]
		private int[] _data;

		// Token: 0x04000AD1 RID: 2769
		private StylusDevice _stylusDevice;

		// Token: 0x04000AD2 RID: 2770
		private SecurityCriticalDataForSet<RawStylusInput> _rawStylusInput;

		// Token: 0x04000AD3 RID: 2771
		private bool _isSynchronize;

		// Token: 0x04000AD4 RID: 2772
		private Func<StylusPointDescription> _stylusPointDescGenerator;
	}
}
