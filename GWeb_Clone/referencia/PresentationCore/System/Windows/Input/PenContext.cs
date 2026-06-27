using System;
using System.Collections.Generic;
using System.Security;
using MS.Internal;
using MS.Win32.Penimc;

namespace System.Windows.Input
{
	// Token: 0x020002D0 RID: 720
	internal sealed class PenContext
	{
		// Token: 0x06001592 RID: 5522 RVA: 0x0004FD20 File Offset: 0x0004F120
		[SecurityCritical]
		internal PenContext(IPimcContext2 pimcContext, IntPtr hwnd, PenContexts contexts, bool supportInRange, bool isIntegrated, int id, IntPtr commHandle, int tabletDeviceId, uint wispContextKey)
		{
			this._contexts = contexts;
			this._pimcContext = new SecurityCriticalDataClass<IPimcContext2>(pimcContext);
			this._id = id;
			this._tabletDeviceId = tabletDeviceId;
			this._commHandle = new SecurityCriticalData<IntPtr>(commHandle);
			this._hwnd = new SecurityCriticalData<IntPtr>(hwnd);
			this._supportInRange = supportInRange;
			this._isIntegrated = isIntegrated;
			this.WispContextKey = wispContextKey;
			this.UpdateScreenMeasurementsPending = false;
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x0004FD98 File Offset: 0x0004F198
		[SecuritySafeCritical]
		~PenContext()
		{
			this.TryRemove(false);
			this._pimcContext = null;
			this._contexts = null;
			GC.KeepAlive(this);
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x0004FDE8 File Offset: 0x0004F1E8
		internal PenContexts Contexts
		{
			[SecurityCritical]
			get
			{
				return this._contexts;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x0004FDFC File Offset: 0x0004F1FC
		internal IntPtr CommHandle
		{
			[SecurityCritical]
			get
			{
				return this._commHandle.Value;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x0004FE14 File Offset: 0x0004F214
		internal int Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x0004FE28 File Offset: 0x0004F228
		internal int TabletDeviceId
		{
			get
			{
				return this._tabletDeviceId;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x0004FE3C File Offset: 0x0004F23C
		internal StylusPointDescription StylusPointDescription
		{
			get
			{
				if (this._stylusPointDescription == null)
				{
					this.InitStylusPointDescription();
				}
				return this._stylusPointDescription;
			}
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x0004FE60 File Offset: 0x0004F260
		[SecuritySafeCritical]
		private void InitStylusPointDescription()
		{
			int num = -1;
			int num2;
			int num3;
			this._pimcContext.Value.GetPacketDescriptionInfo(out num2, out num3);
			List<StylusPointPropertyInfo> list = new List<StylusPointPropertyInfo>(num2 + num3 + 3);
			for (int i = 0; i < num2; i++)
			{
				Guid guid;
				int minimum;
				int maximum;
				int unit;
				float resolution;
				this._pimcContext.Value.GetPacketPropertyInfo(i, out guid, out minimum, out maximum, out unit, out resolution);
				if (num == -1 && guid == StylusPointPropertyIds.NormalPressure)
				{
					num = i;
				}
				if (this._statusPropertyIndex == -1 && guid == StylusPointPropertyIds.PacketStatus)
				{
					this._statusPropertyIndex = i;
				}
				StylusPointPropertyInfo item = new StylusPointPropertyInfo(new StylusPointProperty(guid, false), minimum, maximum, (StylusPointPropertyUnit)unit, resolution);
				list.Add(item);
			}
			if (list != null)
			{
				for (int j = 0; j < num3; j++)
				{
					Guid identifier;
					this._pimcContext.Value.GetPacketButtonInfo(j, out identifier);
					StylusPointProperty stylusPointProperty = new StylusPointProperty(identifier, true);
					StylusPointPropertyInfo item2 = new StylusPointPropertyInfo(stylusPointProperty);
					list.Add(item2);
				}
				if (num == -1)
				{
					list.Insert(StylusPointDescription.RequiredPressureIndex, StylusPointPropertyInfoDefaults.NormalPressure);
				}
				this._infoX = list[0];
				this._infoY = list[1];
				this._stylusPointDescription = new StylusPointDescription(list, num);
			}
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x0004FF88 File Offset: 0x0004F388
		[SecurityCritical]
		internal void Enable()
		{
			if (this._pimcContext != null && this._pimcContext.Value != null)
			{
				this._penThreadPenContext = PenThreadPool.GetPenThreadForPenContext(this);
			}
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x0004FFB8 File Offset: 0x0004F3B8
		[SecurityCritical]
		internal void Disable(bool shutdownWorkerThread)
		{
			if (this.TryRemove(shutdownWorkerThread))
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x0004FFD4 File Offset: 0x0004F3D4
		[SecurityCritical]
		private bool TryRemove(bool shutdownWorkerThread)
		{
			if (this._penThreadPenContext != null && this._penThreadPenContext.RemovePenContext(this))
			{
				if (shutdownWorkerThread)
				{
					PenThread penThreadPenContext = this._penThreadPenContext;
					if (penThreadPenContext != null)
					{
						penThreadPenContext.Dispose();
					}
				}
				this._penThreadPenContext = null;
				return true;
			}
			return false;
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x00050018 File Offset: 0x0004F418
		internal bool SupportInRange
		{
			get
			{
				return this._supportInRange;
			}
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x0005002C File Offset: 0x0004F42C
		internal bool IsInRange(int stylusPointerId)
		{
			if (stylusPointerId == 0)
			{
				return this._stylusDevicesInRange != null && this._stylusDevicesInRange.Count > 0;
			}
			return this._stylusDevicesInRange != null && this._stylusDevicesInRange.Contains(stylusPointerId);
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x0005006C File Offset: 0x0004F46C
		[SecurityCritical]
		internal void FirePenDown(int stylusPointerId, int[] data, int timestamp)
		{
			timestamp = this.EnsureTimestampUnique(timestamp);
			this._lastInRangeTime = timestamp;
			if (this._stylusPointDescription == null)
			{
				this.InitStylusPointDescription();
			}
			this._contexts.OnPenDown(this, this._tabletDeviceId, stylusPointerId, data, timestamp);
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x000500AC File Offset: 0x0004F4AC
		[SecurityCritical]
		internal void FirePenUp(int stylusPointerId, int[] data, int timestamp)
		{
			timestamp = this.EnsureTimestampUnique(timestamp);
			this._lastInRangeTime = timestamp;
			if (this._stylusPointDescription == null)
			{
				this.InitStylusPointDescription();
			}
			this._contexts.OnPenUp(this, this._tabletDeviceId, stylusPointerId, data, timestamp);
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x000500EC File Offset: 0x0004F4EC
		[SecurityCritical]
		internal void FirePackets(int stylusPointerId, int[] data, int timestamp)
		{
			timestamp = this.EnsureTimestampUnique(timestamp);
			this._lastInRangeTime = timestamp;
			if (this._stylusPointDescription == null)
			{
				this.InitStylusPointDescription();
			}
			bool flag = false;
			if (this._statusPropertyIndex != -1)
			{
				int num = data[this._statusPropertyIndex];
				flag = ((num & 1) != 0);
			}
			if (flag)
			{
				this._contexts.OnPackets(this, this._tabletDeviceId, stylusPointerId, data, timestamp);
				return;
			}
			this._contexts.OnInAirPackets(this, this._tabletDeviceId, stylusPointerId, data, timestamp);
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00050160 File Offset: 0x0004F560
		[SecurityCritical]
		internal void FirePenInRange(int stylusPointerId, int[] data, int timestamp)
		{
			if (this._stylusPointDescription == null)
			{
				this.InitStylusPointDescription();
			}
			if (data == null)
			{
				this._lastInRangeTime = timestamp;
				this._queuedInRangeCount++;
				this._contexts.OnPenInRange(this, this._tabletDeviceId, stylusPointerId, data, timestamp);
				return;
			}
			if (!this.IsInRange(stylusPointerId))
			{
				this._lastInRangeTime = timestamp;
				if (this._stylusDevicesInRange == null)
				{
					this._stylusDevicesInRange = new List<int>();
				}
				this._stylusDevicesInRange.Add(stylusPointerId);
				this._contexts.OnPenInRange(this, this._tabletDeviceId, stylusPointerId, data, timestamp);
			}
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x000501F0 File Offset: 0x0004F5F0
		[SecurityCritical]
		internal void FirePenOutOfRange(int stylusPointerId, int timestamp)
		{
			if (stylusPointerId != 0)
			{
				if (this.IsInRange(stylusPointerId))
				{
					timestamp = this.EnsureTimestampUnique(timestamp);
					this._lastInRangeTime = timestamp;
					if (this._stylusPointDescription == null)
					{
						this.InitStylusPointDescription();
					}
					this._stylusDevicesInRange.Remove(stylusPointerId);
					this._contexts.OnPenOutOfRange(this, this._tabletDeviceId, stylusPointerId, timestamp);
					if (this._stylusDevicesInRange.Count == 0)
					{
						this._stylusDevicesInRange = null;
						return;
					}
				}
			}
			else if (this._stylusDevicesInRange != null)
			{
				timestamp = this.EnsureTimestampUnique(timestamp);
				this._lastInRangeTime = timestamp;
				if (this._stylusPointDescription == null)
				{
					this.InitStylusPointDescription();
				}
				for (int i = 0; i < this._stylusDevicesInRange.Count; i++)
				{
					this._contexts.OnPenOutOfRange(this, this._tabletDeviceId, this._stylusDevicesInRange[i], timestamp);
				}
				this._stylusDevicesInRange = null;
			}
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x000502C4 File Offset: 0x0004F6C4
		[SecurityCritical]
		internal void FireSystemGesture(int stylusPointerId, int timestamp)
		{
			timestamp = this.EnsureTimestampUnique(timestamp);
			this._lastInRangeTime = timestamp;
			if (this._stylusPointDescription == null)
			{
				this.InitStylusPointDescription();
			}
			int id;
			int num;
			int num2;
			int gestureX;
			int gestureY;
			int num3;
			int buttonState;
			UnsafeNativeMethods.GetLastSystemEventData(this._commHandle.Value, out id, out num, out num2, out gestureX, out gestureY, out num3, out buttonState);
			this._contexts.OnSystemEvent(this, this._tabletDeviceId, stylusPointerId, timestamp, (SystemGesture)id, gestureX, gestureY, buttonState);
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00050328 File Offset: 0x0004F728
		internal void CheckForRectMappingChanged(int[] data, int numPackets)
		{
			if (this.UpdateScreenMeasurementsPending)
			{
				return;
			}
			if (this._stylusPointDescription == null)
			{
				this.InitStylusPointDescription();
			}
			if (this._statusPropertyIndex == -1)
			{
				return;
			}
			int num = data.Length / numPackets;
			for (int i = 0; i < numPackets; i++)
			{
				int num2 = data[i * num + this._statusPropertyIndex];
				if ((num2 & 16) != 0)
				{
					this.UpdateScreenMeasurementsPending = true;
					return;
				}
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x00050384 File Offset: 0x0004F784
		// (set) Token: 0x060015A7 RID: 5543 RVA: 0x00050398 File Offset: 0x0004F798
		internal bool UpdateScreenMeasurementsPending { get; set; }

		// Token: 0x060015A8 RID: 5544 RVA: 0x000503AC File Offset: 0x0004F7AC
		private int EnsureTimestampUnique(int timestamp)
		{
			int num = this._lastInRangeTime - timestamp;
			if (num >= 0)
			{
				timestamp = this._lastInRangeTime + 1;
			}
			return timestamp;
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x000503D4 File Offset: 0x0004F7D4
		internal int LastInRangeTime
		{
			get
			{
				return this._lastInRangeTime;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x000503E8 File Offset: 0x0004F7E8
		internal int QueuedInRangeCount
		{
			get
			{
				return this._queuedInRangeCount;
			}
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x000503FC File Offset: 0x0004F7FC
		internal void DecrementQueuedInRangeCount()
		{
			this._queuedInRangeCount--;
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x00050418 File Offset: 0x0004F818
		// (set) Token: 0x060015AD RID: 5549 RVA: 0x0005042C File Offset: 0x0004F82C
		internal uint WispContextKey { get; private set; }

		// Token: 0x04000BC5 RID: 3013
		[SecurityCritical]
		internal SecurityCriticalDataClass<IPimcContext2> _pimcContext;

		// Token: 0x04000BC6 RID: 3014
		[SecurityCritical]
		private SecurityCriticalData<IntPtr> _hwnd;

		// Token: 0x04000BC7 RID: 3015
		[SecurityCritical]
		private SecurityCriticalData<IntPtr> _commHandle;

		// Token: 0x04000BC8 RID: 3016
		[SecurityCritical]
		private PenContexts _contexts;

		// Token: 0x04000BC9 RID: 3017
		[SecurityCritical]
		private PenThread _penThreadPenContext;

		// Token: 0x04000BCA RID: 3018
		private int _id;

		// Token: 0x04000BCB RID: 3019
		private int _tabletDeviceId;

		// Token: 0x04000BCC RID: 3020
		private StylusPointPropertyInfo _infoX;

		// Token: 0x04000BCD RID: 3021
		private StylusPointPropertyInfo _infoY;

		// Token: 0x04000BCE RID: 3022
		private bool _supportInRange;

		// Token: 0x04000BCF RID: 3023
		private List<int> _stylusDevicesInRange;

		// Token: 0x04000BD0 RID: 3024
		private bool _isIntegrated;

		// Token: 0x04000BD1 RID: 3025
		private StylusPointDescription _stylusPointDescription;

		// Token: 0x04000BD2 RID: 3026
		private int _statusPropertyIndex = -1;

		// Token: 0x04000BD3 RID: 3027
		private int _lastInRangeTime;

		// Token: 0x04000BD4 RID: 3028
		private int _queuedInRangeCount;
	}
}
