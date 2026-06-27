using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Windows.Interop;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.Penimc;

namespace System.Windows.Input
{
	// Token: 0x020002D5 RID: 725
	internal sealed class PenThreadWorker
	{
		// Token: 0x060015DB RID: 5595 RVA: 0x0005110C File Offset: 0x0005050C
		[SecurityCritical]
		internal PenThreadWorker()
		{
			IntPtr value;
			UnsafeNativeMethods.CreateResetEvent(out value);
			this._pimcResetHandle = new SecurityCriticalData<IntPtr>(value);
			PenThreadWorker.WorkerOperationThreadStart workerOperationThreadStart = new PenThreadWorker.WorkerOperationThreadStart();
			object workerOperationLock = this._workerOperationLock;
			lock (workerOperationLock)
			{
				this._workerOperation.Add(workerOperationThreadStart);
			}
			new Thread(new ThreadStart(this.ThreadProc))
			{
				IsBackground = true
			}.Start();
			workerOperationThreadStart.DoneEvent.WaitOne();
			workerOperationThreadStart.DoneEvent.Close();
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x000511FC File Offset: 0x000505FC
		[SecurityCritical]
		internal void Dispose()
		{
			if (!this.__disposed)
			{
				this.__disposed = true;
				UnsafeNativeMethods.RaiseResetEvent(this._pimcResetHandle.Value);
			}
			GC.KeepAlive(this);
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00051234 File Offset: 0x00050634
		[SecurityCritical]
		internal bool WorkerAddPenContext(PenContext penContext)
		{
			if (this.__disposed)
			{
				throw new ObjectDisposedException(null, SR.Get("Penservice_Disposed"));
			}
			PenThreadWorker.WorkerOperationAddContext workerOperationAddContext = new PenThreadWorker.WorkerOperationAddContext(penContext, this);
			object workerOperationLock = this._workerOperationLock;
			lock (workerOperationLock)
			{
				this._workerOperation.Add(workerOperationAddContext);
			}
			UnsafeNativeMethods.RaiseResetEvent(this._pimcResetHandle.Value);
			workerOperationAddContext.DoneEvent.WaitOne();
			workerOperationAddContext.DoneEvent.Close();
			return workerOperationAddContext.Result;
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x000512D8 File Offset: 0x000506D8
		[SecurityCritical]
		internal bool WorkerRemovePenContext(PenContext penContext)
		{
			if (this.__disposed)
			{
				return true;
			}
			PenThreadWorker.WorkerOperationRemoveContext workerOperationRemoveContext = new PenThreadWorker.WorkerOperationRemoveContext(penContext, this);
			object workerOperationLock = this._workerOperationLock;
			lock (workerOperationLock)
			{
				this._workerOperation.Add(workerOperationRemoveContext);
			}
			UnsafeNativeMethods.RaiseResetEvent(this._pimcResetHandle.Value);
			workerOperationRemoveContext.DoneEvent.WaitOne();
			workerOperationRemoveContext.DoneEvent.Close();
			return workerOperationRemoveContext.Result;
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0005136C File Offset: 0x0005076C
		[SecurityCritical]
		internal TabletDeviceInfo[] WorkerGetTabletsInfo()
		{
			PenThreadWorker.WorkerOperationGetTabletsInfo workerOperationGetTabletsInfo = new PenThreadWorker.WorkerOperationGetTabletsInfo();
			object workerOperationLock = this._workerOperationLock;
			lock (workerOperationLock)
			{
				this._workerOperation.Add(workerOperationGetTabletsInfo);
			}
			UnsafeNativeMethods.RaiseResetEvent(this._pimcResetHandle.Value);
			workerOperationGetTabletsInfo.DoneEvent.WaitOne();
			workerOperationGetTabletsInfo.DoneEvent.Close();
			return workerOperationGetTabletsInfo.TabletDevicesInfo;
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x000513F4 File Offset: 0x000507F4
		[SecurityCritical]
		internal PenContextInfo WorkerCreateContext(IntPtr hwnd, IPimcTablet2 pimcTablet)
		{
			PenThreadWorker.WorkerOperationCreateContext workerOperationCreateContext = new PenThreadWorker.WorkerOperationCreateContext(hwnd, pimcTablet);
			object workerOperationLock = this._workerOperationLock;
			lock (workerOperationLock)
			{
				this._workerOperation.Add(workerOperationCreateContext);
			}
			UnsafeNativeMethods.RaiseResetEvent(this._pimcResetHandle.Value);
			workerOperationCreateContext.DoneEvent.WaitOne();
			workerOperationCreateContext.DoneEvent.Close();
			return workerOperationCreateContext.Result;
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x0005147C File Offset: 0x0005087C
		[SecurityCritical]
		internal bool WorkerAcquireTabletLocks(IPimcTablet2 tablet, uint wispTabletKey)
		{
			PenThreadWorker.WorkerOperationAcquireTabletLocks workerOperationAcquireTabletLocks = new PenThreadWorker.WorkerOperationAcquireTabletLocks(tablet, wispTabletKey);
			object workerOperationLock = this._workerOperationLock;
			lock (workerOperationLock)
			{
				this._workerOperation.Add(workerOperationAcquireTabletLocks);
			}
			UnsafeNativeMethods.RaiseResetEvent(this._pimcResetHandle.Value);
			workerOperationAcquireTabletLocks.DoneEvent.WaitOne();
			workerOperationAcquireTabletLocks.DoneEvent.Close();
			return workerOperationAcquireTabletLocks.Result;
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00051504 File Offset: 0x00050904
		[SecurityCritical]
		internal bool WorkerReleaseTabletLocks(IPimcTablet2 tablet, uint wispTabletKey)
		{
			PenThreadWorker.WorkerOperationReleaseTabletLocks workerOperationReleaseTabletLocks = new PenThreadWorker.WorkerOperationReleaseTabletLocks(tablet, wispTabletKey);
			object workerOperationLock = this._workerOperationLock;
			lock (workerOperationLock)
			{
				this._workerOperation.Add(workerOperationReleaseTabletLocks);
			}
			UnsafeNativeMethods.RaiseResetEvent(this._pimcResetHandle.Value);
			workerOperationReleaseTabletLocks.DoneEvent.WaitOne();
			workerOperationReleaseTabletLocks.DoneEvent.Close();
			return workerOperationReleaseTabletLocks.Result;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x0005158C File Offset: 0x0005098C
		[SecurityCritical]
		internal StylusDeviceInfo[] WorkerRefreshCursorInfo(IPimcTablet2 pimcTablet)
		{
			PenThreadWorker.WorkerOperationRefreshCursorInfo workerOperationRefreshCursorInfo = new PenThreadWorker.WorkerOperationRefreshCursorInfo(pimcTablet);
			object workerOperationLock = this._workerOperationLock;
			lock (workerOperationLock)
			{
				this._workerOperation.Add(workerOperationRefreshCursorInfo);
			}
			UnsafeNativeMethods.RaiseResetEvent(this._pimcResetHandle.Value);
			workerOperationRefreshCursorInfo.DoneEvent.WaitOne();
			workerOperationRefreshCursorInfo.DoneEvent.Close();
			return workerOperationRefreshCursorInfo.StylusDevicesInfo;
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00051614 File Offset: 0x00050A14
		[SecurityCritical]
		internal TabletDeviceInfo WorkerGetTabletInfo(uint index)
		{
			PenThreadWorker.WorkerOperationGetTabletInfo workerOperationGetTabletInfo = new PenThreadWorker.WorkerOperationGetTabletInfo(index);
			object workerOperationLock = this._workerOperationLock;
			lock (workerOperationLock)
			{
				this._workerOperation.Add(workerOperationGetTabletInfo);
			}
			UnsafeNativeMethods.RaiseResetEvent(this._pimcResetHandle.Value);
			workerOperationGetTabletInfo.DoneEvent.WaitOne();
			workerOperationGetTabletInfo.DoneEvent.Close();
			return workerOperationGetTabletInfo.TabletDeviceInfo;
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x0005169C File Offset: 0x00050A9C
		[SecurityCritical]
		internal TabletDeviceSizeInfo WorkerGetUpdatedSizes(IPimcTablet2 pimcTablet)
		{
			PenThreadWorker.WorkerOperationWorkerGetUpdatedSizes workerOperationWorkerGetUpdatedSizes = new PenThreadWorker.WorkerOperationWorkerGetUpdatedSizes(pimcTablet);
			object workerOperationLock = this._workerOperationLock;
			lock (workerOperationLock)
			{
				this._workerOperation.Add(workerOperationWorkerGetUpdatedSizes);
			}
			UnsafeNativeMethods.RaiseResetEvent(this._pimcResetHandle.Value);
			workerOperationWorkerGetUpdatedSizes.DoneEvent.WaitOne();
			workerOperationWorkerGetUpdatedSizes.DoneEvent.Close();
			return workerOperationWorkerGetUpdatedSizes.TabletDeviceSizeInfo;
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00051724 File Offset: 0x00050B24
		[SecurityCritical]
		private void FlushCache(bool goingOutOfRange)
		{
			if (this._cachedMoveData != null)
			{
				if (!goingOutOfRange || this._cachedMovePenContext.IsInRange(this._cachedMoveStylusPointerId))
				{
					this._cachedMovePenContext.FirePenInRange(this._cachedMoveStylusPointerId, this._cachedMoveData, this._cachedMoveStartTimestamp);
					this._cachedMovePenContext.FirePackets(this._cachedMoveStylusPointerId, this._cachedMoveData, this._cachedMoveStartTimestamp);
				}
				this._cachedMoveData = null;
				this._cachedMovePenContext = null;
				this._cachedMoveStylusPointerId = 0;
			}
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x000517A0 File Offset: 0x00050BA0
		[SecurityCritical]
		private bool DoCacheEvent(int evt, PenContext penContext, int stylusPointerId, int[] data, int timestamp)
		{
			if (evt == 711)
			{
				if (this._cachedMoveData == null)
				{
					this._cachedMovePenContext = penContext;
					this._cachedMoveStylusPointerId = stylusPointerId;
					this._cachedMoveStartTimestamp = timestamp;
					this._cachedMoveData = data;
					return true;
				}
				if (this._cachedMovePenContext == penContext && stylusPointerId == this._cachedMoveStylusPointerId)
				{
					int num = timestamp - this._cachedMoveStartTimestamp;
					if (timestamp < this._cachedMoveStartTimestamp)
					{
						num = int.MaxValue - this._cachedMoveStartTimestamp + timestamp;
					}
					if (8 > num)
					{
						int[] cachedMoveData = this._cachedMoveData;
						this._cachedMoveData = new int[cachedMoveData.Length + data.Length];
						cachedMoveData.CopyTo(this._cachedMoveData, 0);
						data.CopyTo(this._cachedMoveData, cachedMoveData.Length);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00051854 File Offset: 0x00050C54
		[SecurityCritical]
		internal void FireEvent(PenContext penContext, int evt, int stylusPointerId, int cPackets, int cbPacket, IntPtr pPackets)
		{
			if (this.__disposed)
			{
				return;
			}
			if (cbPacket % 4 != 0)
			{
				throw new InvalidOperationException(SR.Get("PenService_InvalidPacketData"));
			}
			int num = cPackets * (cbPacket / 4);
			int[] array;
			if (0 < num)
			{
				array = new int[num];
				Marshal.Copy(pPackets, array, 0, num);
				penContext.CheckForRectMappingChanged(array, cPackets);
			}
			else
			{
				array = null;
			}
			int tickCount = Environment.TickCount;
			if (this.DoCacheEvent(evt, penContext, stylusPointerId, array, tickCount))
			{
				return;
			}
			this.FlushCache(false);
			switch (evt)
			{
			case 707:
				penContext.FirePenInRange(stylusPointerId, null, tickCount);
				return;
			case 708:
				penContext.FirePenOutOfRange(stylusPointerId, tickCount);
				return;
			case 709:
				penContext.FirePenInRange(stylusPointerId, array, tickCount);
				penContext.FirePenDown(stylusPointerId, array, tickCount);
				return;
			case 710:
				penContext.FirePenInRange(stylusPointerId, array, tickCount);
				penContext.FirePenUp(stylusPointerId, array, tickCount);
				return;
			case 711:
				penContext.FirePenInRange(stylusPointerId, array, tickCount);
				penContext.FirePackets(stylusPointerId, array, tickCount);
				return;
			case 712:
			case 713:
				break;
			case 714:
				penContext.FireSystemGesture(stylusPointerId, tickCount);
				break;
			default:
				return;
			}
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x0005194C File Offset: 0x00050D4C
		[SecurityCritical]
		private static TabletDeviceInfo GetTabletInfoHelper(IPimcTablet2 pimcTablet)
		{
			TabletDeviceInfo tabletDeviceInfo = new TabletDeviceInfo();
			tabletDeviceInfo.PimcTablet = new SecurityCriticalDataClass<IPimcTablet2>(pimcTablet);
			pimcTablet.GetKey(out tabletDeviceInfo.Id);
			pimcTablet.GetName(out tabletDeviceInfo.Name);
			pimcTablet.GetPlugAndPlayId(out tabletDeviceInfo.PlugAndPlayId);
			int num;
			int num2;
			int num3;
			int num4;
			pimcTablet.GetTabletAndDisplaySize(out num, out num2, out num3, out num4);
			tabletDeviceInfo.SizeInfo = new TabletDeviceSizeInfo(new Size((double)num, (double)num2), new Size((double)num3, (double)num4));
			int hardwareCapabilities;
			pimcTablet.GetHardwareCaps(out hardwareCapabilities);
			tabletDeviceInfo.HardwareCapabilities = (TabletHardwareCapabilities)hardwareCapabilities;
			int num5;
			pimcTablet.GetDeviceType(out num5);
			tabletDeviceInfo.DeviceType = (TabletDeviceType)(num5 - 1);
			PenThreadWorker.InitializeSupportedStylusPointProperties(pimcTablet, tabletDeviceInfo);
			tabletDeviceInfo.StylusDevicesInfo = PenThreadWorker.GetStylusDevicesInfo(pimcTablet);
			tabletDeviceInfo.WispTabletKey = UnsafeNativeMethods.QueryWispTabletKey(pimcTablet);
			UnsafeNativeMethods.SetWispManagerKey(pimcTablet);
			UnsafeNativeMethods.LockWispManager();
			return tabletDeviceInfo;
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x00051A08 File Offset: 0x00050E08
		[SecurityCritical]
		private static void InitializeSupportedStylusPointProperties(IPimcTablet2 pimcTablet, TabletDeviceInfo tabletInfo)
		{
			int num = -1;
			int num2;
			int num3;
			pimcTablet.GetPacketDescriptionInfo(out num2, out num3);
			List<StylusPointProperty> list = new List<StylusPointProperty>(num2 + num3 + 3);
			for (int i = 0; i < num2; i++)
			{
				Guid guid;
				int num4;
				int num5;
				int num6;
				float num7;
				pimcTablet.GetPacketPropertyInfo(i, out guid, out num4, out num5, out num6, out num7);
				if (num == -1 && guid == StylusPointPropertyIds.NormalPressure)
				{
					num = i;
				}
				StylusPointProperty item = new StylusPointProperty(guid, false);
				list.Add(item);
			}
			for (int j = 0; j < num3; j++)
			{
				Guid identifier;
				pimcTablet.GetPacketButtonInfo(j, out identifier);
				StylusPointProperty item2 = new StylusPointProperty(identifier, true);
				list.Add(item2);
			}
			if (num == -1)
			{
				list.Insert(StylusPointDescription.RequiredPressureIndex, StylusPointProperties.NormalPressure);
			}
			else
			{
				tabletInfo.HardwareCapabilities |= TabletHardwareCapabilities.SupportsPressure;
			}
			tabletInfo.StylusPointProperties = new ReadOnlyCollection<StylusPointProperty>(list);
			tabletInfo.PressureIndex = num;
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00051AD8 File Offset: 0x00050ED8
		[SecurityCritical]
		private static StylusDeviceInfo[] GetStylusDevicesInfo(IPimcTablet2 pimcTablet)
		{
			int num;
			pimcTablet.GetCursorCount(out num);
			StylusDeviceInfo[] array = new StylusDeviceInfo[num];
			for (int i = 0; i < num; i++)
			{
				string cursorName;
				int cursorId;
				bool cursorInverted;
				pimcTablet.GetCursorInfo(i, out cursorName, out cursorId, out cursorInverted);
				int num2;
				pimcTablet.GetCursorButtonCount(i, out num2);
				StylusButton[] array2 = new StylusButton[num2];
				for (int j = 0; j < num2; j++)
				{
					string name;
					Guid id;
					pimcTablet.GetCursorButtonInfo(i, j, out name, out id);
					array2[j] = new StylusButton(name, id);
				}
				StylusButtonCollection buttonCollection = new StylusButtonCollection(array2);
				array[i].CursorName = cursorName;
				array[i].CursorId = cursorId;
				array[i].CursorInverted = cursorInverted;
				array[i].ButtonCollection = buttonCollection;
			}
			return array;
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00051B8C File Offset: 0x00050F8C
		[SecurityCritical]
		internal bool AddPenContext(PenContext penContext)
		{
			List<PenContext> list = new List<PenContext>();
			bool result = false;
			for (int i = 0; i < this._penContexts.Length; i++)
			{
				if (this._penContexts[i].IsAlive)
				{
					PenContext penContext2 = this._penContexts[i].Target as PenContext;
					if (penContext2 != null)
					{
						list.Add(penContext2);
					}
				}
			}
			if (list.Count < 31)
			{
				list.Add(penContext);
				UnsafeNativeMethods.CheckedLockWispObjectFromGit(penContext.WispContextKey);
				this._pimcContexts = new IPimcContext2[list.Count];
				this._penContexts = new WeakReference[list.Count];
				this._handles = new IntPtr[list.Count];
				this._wispContextKeys = new uint[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					PenContext penContext3 = list[i];
					this._handles[i] = penContext3.CommHandle;
					this._pimcContexts[i] = penContext3._pimcContext.Value;
					this._penContexts[i] = new WeakReference(penContext3);
					this._wispContextKeys[i] = penContext3.WispContextKey;
				}
				result = true;
			}
			list.Clear();
			return result;
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00051CAC File Offset: 0x000510AC
		[SecurityCritical]
		internal bool RemovePenContext(PenContext penContext)
		{
			List<PenContext> list = new List<PenContext>();
			for (int i = 0; i < this._penContexts.Length; i++)
			{
				if (this._penContexts[i].IsAlive)
				{
					PenContext penContext2 = this._penContexts[i].Target as PenContext;
					if (penContext2 != null && (penContext2 != penContext || penContext2.IsInRange(0)))
					{
						list.Add(penContext2);
					}
				}
			}
			bool flag = !list.Contains(penContext);
			this._pimcContexts = new IPimcContext2[list.Count];
			this._penContexts = new WeakReference[list.Count];
			this._handles = new IntPtr[list.Count];
			this._wispContextKeys = new uint[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				PenContext penContext3 = list[i];
				this._handles[i] = penContext3.CommHandle;
				this._pimcContexts[i] = penContext3._pimcContext.Value;
				this._penContexts[i] = new WeakReference(penContext3);
				this._wispContextKeys[i] = penContext3.WispContextKey;
			}
			list.Clear();
			if (flag)
			{
				UnsafeNativeMethods.CheckedUnlockWispObjectFromGit(penContext.WispContextKey);
				penContext._pimcContext.Value.ShutdownComm();
				if (!OSVersionHelper.IsOsWindows8OrGreater)
				{
					Marshal.ReleaseComObject(penContext._pimcContext.Value);
				}
			}
			return flag;
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00051DF8 File Offset: 0x000511F8
		private static bool IsKnownException(Exception e)
		{
			return e is COMException || e is ArgumentException || e is UnauthorizedAccessException || e is InvalidCastException;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00051E28 File Offset: 0x00051228
		[SecurityCritical]
		internal void ThreadProc()
		{
			Thread.CurrentThread.Name = "Stylus Input";
			try
			{
				while (!this.__disposed)
				{
					PenThreadWorker.WorkerOperation[] array = null;
					object workerOperationLock = this._workerOperationLock;
					lock (workerOperationLock)
					{
						if (this._workerOperation.Count > 0)
						{
							array = this._workerOperation.ToArray();
							this._workerOperation.Clear();
						}
					}
					if (array != null)
					{
						for (int i = 0; i < array.Length; i++)
						{
							array[i].DoWork();
						}
						array = null;
					}
					for (;;)
					{
						int num;
						int stylusPointerId;
						int cPackets;
						int cbPacket;
						IntPtr pPackets;
						int num2;
						if (this._handles.Length == 1)
						{
							if (!UnsafeNativeMethods.GetPenEvent(this._handles[0], this._pimcResetHandle.Value, out num, out stylusPointerId, out cPackets, out cbPacket, out pPackets))
							{
								break;
							}
							num2 = 0;
						}
						else if (!UnsafeNativeMethods.GetPenEventMultiple(this._handles.Length, this._handles, this._pimcResetHandle.Value, out num2, out num, out stylusPointerId, out cPackets, out cbPacket, out pPackets))
						{
							break;
						}
						if (num != 1)
						{
							PenContext penContext = this._penContexts[num2].Target as PenContext;
							if (penContext != null)
							{
								this.FireEvent(penContext, num, stylusPointerId, cPackets, cbPacket, pPackets);
							}
						}
						else
						{
							this.FlushCache(true);
							for (int j = 0; j < this._penContexts.Length; j++)
							{
								PenContext penContext2 = this._penContexts[j].Target as PenContext;
								if (penContext2 != null)
								{
									penContext2.FirePenOutOfRange(0, Environment.TickCount);
								}
							}
						}
					}
				}
			}
			finally
			{
				this.__disposed = true;
				UnsafeNativeMethods.DestroyResetEvent(this._pimcResetHandle.Value);
				UnsafeNativeMethods.UnlockWispManager();
				UnsafeNativeMethods.ReleaseManagerExternalLock();
				for (int k = 0; k < this._pimcContexts.Length; k++)
				{
					UnsafeNativeMethods.CheckedUnlockWispObjectFromGit(this._wispContextKeys[k]);
					this._pimcContexts[k].ShutdownComm();
				}
				GC.KeepAlive(this);
			}
		}

		// Token: 0x04000BE4 RID: 3044
		private const int PenEventNone = 0;

		// Token: 0x04000BE5 RID: 3045
		private const int PenEventTimeout = 1;

		// Token: 0x04000BE6 RID: 3046
		private const int PenEventPenInRange = 707;

		// Token: 0x04000BE7 RID: 3047
		private const int PenEventPenOutOfRange = 708;

		// Token: 0x04000BE8 RID: 3048
		private const int PenEventPenDown = 709;

		// Token: 0x04000BE9 RID: 3049
		private const int PenEventPenUp = 710;

		// Token: 0x04000BEA RID: 3050
		private const int PenEventPackets = 711;

		// Token: 0x04000BEB RID: 3051
		private const int PenEventSystem = 714;

		// Token: 0x04000BEC RID: 3052
		private const int MaxContextPerThread = 31;

		// Token: 0x04000BED RID: 3053
		private const int EventsFrequency = 8;

		// Token: 0x04000BEE RID: 3054
		[SecurityCritical]
		private IntPtr[] _handles = new IntPtr[0];

		// Token: 0x04000BEF RID: 3055
		[SecurityCritical]
		private WeakReference[] _penContexts = new WeakReference[0];

		// Token: 0x04000BF0 RID: 3056
		[SecurityCritical]
		private IPimcContext2[] _pimcContexts = new IPimcContext2[0];

		// Token: 0x04000BF1 RID: 3057
		[SecurityCritical]
		private uint[] _wispContextKeys = new uint[0];

		// Token: 0x04000BF2 RID: 3058
		private SecurityCriticalData<IntPtr> _pimcResetHandle;

		// Token: 0x04000BF3 RID: 3059
		private volatile bool __disposed;

		// Token: 0x04000BF4 RID: 3060
		private List<PenThreadWorker.WorkerOperation> _workerOperation = new List<PenThreadWorker.WorkerOperation>();

		// Token: 0x04000BF5 RID: 3061
		private object _workerOperationLock = new object();

		// Token: 0x04000BF6 RID: 3062
		[SecurityCritical]
		private PenContext _cachedMovePenContext;

		// Token: 0x04000BF7 RID: 3063
		private int _cachedMoveStylusPointerId;

		// Token: 0x04000BF8 RID: 3064
		private int _cachedMoveStartTimestamp;

		// Token: 0x04000BF9 RID: 3065
		private int[] _cachedMoveData;

		// Token: 0x02000814 RID: 2068
		private abstract class WorkerOperation
		{
			// Token: 0x06005611 RID: 22033 RVA: 0x00161F3C File Offset: 0x0016133C
			internal WorkerOperation()
			{
				this._doneEvent = new AutoResetEvent(false);
			}

			// Token: 0x06005612 RID: 22034 RVA: 0x00161F5C File Offset: 0x0016135C
			[SecurityCritical]
			internal void DoWork()
			{
				try
				{
					this.OnDoWork();
				}
				finally
				{
					this._doneEvent.Set();
				}
			}

			// Token: 0x06005613 RID: 22035
			[SecurityCritical]
			protected abstract void OnDoWork();

			// Token: 0x170011A6 RID: 4518
			// (get) Token: 0x06005614 RID: 22036 RVA: 0x00161F9C File Offset: 0x0016139C
			internal AutoResetEvent DoneEvent
			{
				get
				{
					return this._doneEvent;
				}
			}

			// Token: 0x0400275B RID: 10075
			private AutoResetEvent _doneEvent;
		}

		// Token: 0x02000815 RID: 2069
		private class WorkerOperationThreadStart : PenThreadWorker.WorkerOperation
		{
			// Token: 0x06005615 RID: 22037 RVA: 0x00161FB0 File Offset: 0x001613B0
			protected override void OnDoWork()
			{
			}
		}

		// Token: 0x02000816 RID: 2070
		private class WorkerOperationGetTabletsInfo : PenThreadWorker.WorkerOperation
		{
			// Token: 0x170011A7 RID: 4519
			// (get) Token: 0x06005617 RID: 22039 RVA: 0x00161FD4 File Offset: 0x001613D4
			internal TabletDeviceInfo[] TabletDevicesInfo
			{
				get
				{
					return this._tabletDevicesInfo;
				}
			}

			// Token: 0x06005618 RID: 22040 RVA: 0x00161FE8 File Offset: 0x001613E8
			[SecurityCritical]
			protected override void OnDoWork()
			{
				try
				{
					IPimcManager2 pimcManager = UnsafeNativeMethods.PimcManager;
					uint num;
					pimcManager.GetTabletCount(out num);
					TabletDeviceInfo[] array = new TabletDeviceInfo[num];
					for (uint num2 = 0U; num2 < num; num2 += 1U)
					{
						IPimcTablet2 pimcTablet;
						pimcManager.GetTablet(num2, out pimcTablet);
						array[(int)num2] = PenThreadWorker.GetTabletInfoHelper(pimcTablet);
					}
					this._tabletDevicesInfo = array;
				}
				catch (Exception e) when (PenThreadWorker.IsKnownException(e))
				{
				}
			}

			// Token: 0x0400275C RID: 10076
			private TabletDeviceInfo[] _tabletDevicesInfo = new TabletDeviceInfo[0];
		}

		// Token: 0x02000817 RID: 2071
		private class WorkerOperationCreateContext : PenThreadWorker.WorkerOperation
		{
			// Token: 0x0600561A RID: 22042 RVA: 0x0016208C File Offset: 0x0016148C
			[SecurityCritical]
			internal WorkerOperationCreateContext(IntPtr hwnd, IPimcTablet2 pimcTablet)
			{
				this._hwnd = hwnd;
				this._pimcTablet = pimcTablet;
			}

			// Token: 0x170011A8 RID: 4520
			// (get) Token: 0x0600561B RID: 22043 RVA: 0x001620B0 File Offset: 0x001614B0
			internal PenContextInfo Result
			{
				get
				{
					return this._result;
				}
			}

			// Token: 0x0600561C RID: 22044 RVA: 0x001620C4 File Offset: 0x001614C4
			[SecurityCritical]
			protected override void OnDoWork()
			{
				try
				{
					IPimcContext2 pimcContext;
					int contextId;
					long num;
					this._pimcTablet.CreateContext(this._hwnd, true, 250U, out pimcContext, out contextId, out num);
					PenContextInfo result;
					result.ContextId = contextId;
					result.PimcContext = new SecurityCriticalDataClass<IPimcContext2>(pimcContext);
					result.CommHandle = new SecurityCriticalDataClass<IntPtr>((IntPtr.Size == 4) ? new IntPtr((int)num) : new IntPtr(num));
					result.WispContextKey = UnsafeNativeMethods.QueryWispContextKey(pimcContext);
					this._result = result;
				}
				catch (Exception e) when (PenThreadWorker.IsKnownException(e))
				{
				}
			}

			// Token: 0x0400275D RID: 10077
			[SecurityCritical]
			private IntPtr _hwnd;

			// Token: 0x0400275E RID: 10078
			[SecurityCritical]
			private IPimcTablet2 _pimcTablet;

			// Token: 0x0400275F RID: 10079
			private PenContextInfo _result;
		}

		// Token: 0x02000818 RID: 2072
		private class WorkerOperationAcquireTabletLocks : PenThreadWorker.WorkerOperation
		{
			// Token: 0x0600561D RID: 22045 RVA: 0x00162174 File Offset: 0x00161574
			[SecurityCritical]
			internal WorkerOperationAcquireTabletLocks(IPimcTablet2 tablet, uint wispTabletKey)
			{
				this._tablet = tablet;
				this._wispTabletKey = wispTabletKey;
			}

			// Token: 0x170011A9 RID: 4521
			// (get) Token: 0x0600561E RID: 22046 RVA: 0x00162198 File Offset: 0x00161598
			// (set) Token: 0x0600561F RID: 22047 RVA: 0x001621AC File Offset: 0x001615AC
			internal bool Result { get; private set; }

			// Token: 0x06005620 RID: 22048 RVA: 0x001621C0 File Offset: 0x001615C0
			[SecurityCritical]
			protected override void OnDoWork()
			{
				UnsafeNativeMethods.AcquireTabletExternalLock(this._tablet);
				UnsafeNativeMethods.CheckedLockWispObjectFromGit(this._wispTabletKey);
				this.Result = true;
			}

			// Token: 0x04002761 RID: 10081
			[SecurityCritical]
			private IPimcTablet2 _tablet;

			// Token: 0x04002762 RID: 10082
			[SecurityCritical]
			private uint _wispTabletKey;
		}

		// Token: 0x02000819 RID: 2073
		private class WorkerOperationReleaseTabletLocks : PenThreadWorker.WorkerOperation
		{
			// Token: 0x06005621 RID: 22049 RVA: 0x001621EC File Offset: 0x001615EC
			[SecurityCritical]
			internal WorkerOperationReleaseTabletLocks(IPimcTablet2 tablet, uint wispTabletKey)
			{
				this._tablet = tablet;
				this._wispTabletKey = wispTabletKey;
			}

			// Token: 0x170011AA RID: 4522
			// (get) Token: 0x06005622 RID: 22050 RVA: 0x00162210 File Offset: 0x00161610
			// (set) Token: 0x06005623 RID: 22051 RVA: 0x00162224 File Offset: 0x00161624
			internal bool Result { get; private set; }

			// Token: 0x06005624 RID: 22052 RVA: 0x00162238 File Offset: 0x00161638
			[SecurityCritical]
			protected override void OnDoWork()
			{
				UnsafeNativeMethods.CheckedUnlockWispObjectFromGit(this._wispTabletKey);
				UnsafeNativeMethods.ReleaseTabletExternalLock(this._tablet);
				this.Result = true;
			}

			// Token: 0x04002764 RID: 10084
			[SecurityCritical]
			private IPimcTablet2 _tablet;

			// Token: 0x04002765 RID: 10085
			[SecurityCritical]
			private uint _wispTabletKey;
		}

		// Token: 0x0200081A RID: 2074
		private class WorkerOperationRefreshCursorInfo : PenThreadWorker.WorkerOperation
		{
			// Token: 0x06005625 RID: 22053 RVA: 0x00162264 File Offset: 0x00161664
			[SecurityCritical]
			internal WorkerOperationRefreshCursorInfo(IPimcTablet2 pimcTablet)
			{
				this._pimcTablet = pimcTablet;
			}

			// Token: 0x170011AB RID: 4523
			// (get) Token: 0x06005626 RID: 22054 RVA: 0x0016228C File Offset: 0x0016168C
			internal StylusDeviceInfo[] StylusDevicesInfo
			{
				get
				{
					return this._stylusDevicesInfo;
				}
			}

			// Token: 0x06005627 RID: 22055 RVA: 0x001622A0 File Offset: 0x001616A0
			[SecurityCritical]
			protected override void OnDoWork()
			{
				try
				{
					this._pimcTablet.RefreshCursorInfo();
					this._stylusDevicesInfo = PenThreadWorker.GetStylusDevicesInfo(this._pimcTablet);
				}
				catch (Exception e) when (PenThreadWorker.IsKnownException(e))
				{
				}
			}

			// Token: 0x04002766 RID: 10086
			[SecurityCritical]
			private IPimcTablet2 _pimcTablet;

			// Token: 0x04002767 RID: 10087
			private StylusDeviceInfo[] _stylusDevicesInfo = new StylusDeviceInfo[0];
		}

		// Token: 0x0200081B RID: 2075
		private class WorkerOperationGetTabletInfo : PenThreadWorker.WorkerOperation
		{
			// Token: 0x06005628 RID: 22056 RVA: 0x00162304 File Offset: 0x00161704
			internal WorkerOperationGetTabletInfo(uint index)
			{
				this._index = index;
			}

			// Token: 0x170011AC RID: 4524
			// (get) Token: 0x06005629 RID: 22057 RVA: 0x0016232C File Offset: 0x0016172C
			internal TabletDeviceInfo TabletDeviceInfo
			{
				get
				{
					return this._tabletDeviceInfo;
				}
			}

			// Token: 0x0600562A RID: 22058 RVA: 0x00162340 File Offset: 0x00161740
			[SecurityCritical]
			protected override void OnDoWork()
			{
				try
				{
					IPimcManager2 pimcManager = UnsafeNativeMethods.PimcManager;
					IPimcTablet2 pimcTablet;
					pimcManager.GetTablet(this._index, out pimcTablet);
					this._tabletDeviceInfo = PenThreadWorker.GetTabletInfoHelper(pimcTablet);
				}
				catch (Exception e) when (PenThreadWorker.IsKnownException(e))
				{
				}
			}

			// Token: 0x04002768 RID: 10088
			private uint _index;

			// Token: 0x04002769 RID: 10089
			private TabletDeviceInfo _tabletDeviceInfo = new TabletDeviceInfo();
		}

		// Token: 0x0200081C RID: 2076
		private class WorkerOperationWorkerGetUpdatedSizes : PenThreadWorker.WorkerOperation
		{
			// Token: 0x0600562B RID: 22059 RVA: 0x001623A8 File Offset: 0x001617A8
			[SecurityCritical]
			internal WorkerOperationWorkerGetUpdatedSizes(IPimcTablet2 pimcTablet)
			{
				this._pimcTablet = pimcTablet;
			}

			// Token: 0x170011AD RID: 4525
			// (get) Token: 0x0600562C RID: 22060 RVA: 0x001623FC File Offset: 0x001617FC
			internal TabletDeviceSizeInfo TabletDeviceSizeInfo
			{
				get
				{
					return this._tabletDeviceSizeInfo;
				}
			}

			// Token: 0x0600562D RID: 22061 RVA: 0x00162410 File Offset: 0x00161810
			[SecurityCritical]
			protected override void OnDoWork()
			{
				try
				{
					int num;
					int num2;
					int num3;
					int num4;
					this._pimcTablet.GetTabletAndDisplaySize(out num, out num2, out num3, out num4);
					this._tabletDeviceSizeInfo = new TabletDeviceSizeInfo(new Size((double)num, (double)num2), new Size((double)num3, (double)num4));
				}
				catch (Exception e) when (PenThreadWorker.IsKnownException(e))
				{
				}
			}

			// Token: 0x0400276A RID: 10090
			[SecurityCritical]
			private IPimcTablet2 _pimcTablet;

			// Token: 0x0400276B RID: 10091
			private TabletDeviceSizeInfo _tabletDeviceSizeInfo = new TabletDeviceSizeInfo(new Size(1.0, 1.0), new Size(1.0, 1.0));
		}

		// Token: 0x0200081D RID: 2077
		private class WorkerOperationAddContext : PenThreadWorker.WorkerOperation
		{
			// Token: 0x0600562E RID: 22062 RVA: 0x00162488 File Offset: 0x00161888
			[SecurityCritical]
			internal WorkerOperationAddContext(PenContext penContext, PenThreadWorker penThreadWorker)
			{
				this._newPenContext = penContext;
				this._penThreadWorker = penThreadWorker;
			}

			// Token: 0x170011AE RID: 4526
			// (get) Token: 0x0600562F RID: 22063 RVA: 0x001624AC File Offset: 0x001618AC
			internal bool Result
			{
				get
				{
					return this._result;
				}
			}

			// Token: 0x06005630 RID: 22064 RVA: 0x001624C0 File Offset: 0x001618C0
			[SecurityCritical]
			protected override void OnDoWork()
			{
				this._result = this._penThreadWorker.AddPenContext(this._newPenContext);
			}

			// Token: 0x0400276C RID: 10092
			[SecurityCritical]
			private PenContext _newPenContext;

			// Token: 0x0400276D RID: 10093
			[SecurityCritical]
			private PenThreadWorker _penThreadWorker;

			// Token: 0x0400276E RID: 10094
			private bool _result;
		}

		// Token: 0x0200081E RID: 2078
		private class WorkerOperationRemoveContext : PenThreadWorker.WorkerOperation
		{
			// Token: 0x06005631 RID: 22065 RVA: 0x001624E4 File Offset: 0x001618E4
			[SecurityCritical]
			internal WorkerOperationRemoveContext(PenContext penContext, PenThreadWorker penThreadWorker)
			{
				this._penContextToRemove = penContext;
				this._penThreadWorker = penThreadWorker;
			}

			// Token: 0x170011AF RID: 4527
			// (get) Token: 0x06005632 RID: 22066 RVA: 0x00162508 File Offset: 0x00161908
			internal bool Result
			{
				get
				{
					return this._result;
				}
			}

			// Token: 0x06005633 RID: 22067 RVA: 0x0016251C File Offset: 0x0016191C
			[SecurityCritical]
			protected override void OnDoWork()
			{
				this._result = this._penThreadWorker.RemovePenContext(this._penContextToRemove);
			}

			// Token: 0x0400276F RID: 10095
			[SecurityCritical]
			private PenContext _penContextToRemove;

			// Token: 0x04002770 RID: 10096
			[SecurityCritical]
			private PenThreadWorker _penThreadWorker;

			// Token: 0x04002771 RID: 10097
			private bool _result;
		}
	}
}
