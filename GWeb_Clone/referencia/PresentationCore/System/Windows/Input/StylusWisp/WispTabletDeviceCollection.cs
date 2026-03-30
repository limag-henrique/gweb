using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Input.Tracing;
using Microsoft.Win32;
using MS.Win32;

namespace System.Windows.Input.StylusWisp
{
	/// <summary>Contém os objetos TabletDevice que representam os dispositivos digitalizadores de um Tablet WISP.</summary>
	// Token: 0x020002E7 RID: 743
	public class WispTabletDeviceCollection : TabletDeviceCollection
	{
		// Token: 0x0600171C RID: 5916 RVA: 0x0005A118 File Offset: 0x00059518
		[SecurityCritical]
		internal WispTabletDeviceCollection()
		{
			WispLogic currentStylusLogicAs = StylusLogic.GetCurrentStylusLogicAs<WispLogic>();
			bool flag = currentStylusLogicAs.Enabled;
			if (!flag)
			{
				flag = WispTabletDeviceCollection.ShouldEnableTablets();
			}
			if (flag)
			{
				this.UpdateTablets();
				if (!currentStylusLogicAs.Enabled)
				{
					currentStylusLogicAs.EnableCore();
				}
			}
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x0005A178 File Offset: 0x00059578
		[SecurityCritical]
		internal static bool ShouldEnableTablets()
		{
			bool result = false;
			if (StylusLogic.IsStylusAndTouchSupportEnabled && WispTabletDeviceCollection.IsWisptisRegistered() && WispTabletDeviceCollection.HasTabletDevices())
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x0005A1A0 File Offset: 0x000595A0
		[SecurityCritical]
		private static bool IsWisptisRegistered()
		{
			bool result = false;
			RegistryKey registryKey = null;
			object obj = null;
			bool flag = Environment.OSVersion.Version.Major >= 6;
			string pathList = flag ? "HKEY_CLASSES_ROOT\\Interface\\{C247F616-BBEB-406A-AED3-F75E656599AE}" : "HKEY_CLASSES_ROOT\\CLSID\\{A5B020FD-E04B-4e67-B65A-E7DEED25B2CF}\\LocalServer32";
			string name = flag ? "Interface\\{C247F616-BBEB-406A-AED3-F75E656599AE}" : "CLSID\\{A5B020FD-E04B-4e67-B65A-E7DEED25B2CF}\\LocalServer32";
			string value = flag ? "ITablet2" : "wisptis.exe";
			new RegistryPermission(RegistryPermissionAccess.Read, pathList).Assert();
			try
			{
				registryKey = Registry.ClassesRoot.OpenSubKey(name);
				if (registryKey != null)
				{
					obj = registryKey.GetValue("");
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			if (registryKey != null)
			{
				string text = obj as string;
				if (text != null && text.LastIndexOf(value, StringComparison.OrdinalIgnoreCase) != -1)
				{
					result = true;
				}
				registryKey.Close();
			}
			return result;
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0005A270 File Offset: 0x00059670
		[SecurityCritical]
		private static bool HasTabletDevices()
		{
			uint num = 0U;
			int rawInputDeviceList = (int)UnsafeNativeMethods.GetRawInputDeviceList(null, ref num, (uint)Marshal.SizeOf(typeof(NativeMethods.RAWINPUTDEVICELIST)));
			if (rawInputDeviceList >= 0 && num != 0U)
			{
				NativeMethods.RAWINPUTDEVICELIST[] array = new NativeMethods.RAWINPUTDEVICELIST[num];
				int rawInputDeviceList2 = (int)UnsafeNativeMethods.GetRawInputDeviceList(array, ref num, (uint)Marshal.SizeOf(typeof(NativeMethods.RAWINPUTDEVICELIST)));
				if (rawInputDeviceList2 > 0)
				{
					for (int i = 0; i < rawInputDeviceList2; i++)
					{
						if (array[i].dwType == 2U)
						{
							NativeMethods.RID_DEVICE_INFO rid_DEVICE_INFO = new NativeMethods.RID_DEVICE_INFO
							{
								cbSize = (uint)Marshal.SizeOf(typeof(NativeMethods.RID_DEVICE_INFO))
							};
							uint cbSize = rid_DEVICE_INFO.cbSize;
							int rawInputDeviceInfo = (int)UnsafeNativeMethods.GetRawInputDeviceInfo(array[i].hDevice, 536870923U, ref rid_DEVICE_INFO, ref cbSize);
							if (rawInputDeviceInfo > 0 && rid_DEVICE_INFO.hid.usUsagePage == 13)
							{
								ushort usUsage = rid_DEVICE_INFO.hid.usUsage;
								if (usUsage - 1 <= 3)
								{
									return true;
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x0005A364 File Offset: 0x00059764
		[SecurityCritical]
		internal void UpdateTablets()
		{
			if (this._tablets == null)
			{
				throw new ObjectDisposedException("TabletDeviceCollection");
			}
			if (this._inUpdateTablets)
			{
				StylusTraceLogger.LogReentrancy("UpdateTablets");
				this._hasUpdateTabletsBeenCalledReentrantly = true;
				return;
			}
			try
			{
				this._inUpdateTablets = true;
				do
				{
					this._hasUpdateTabletsBeenCalledReentrantly = false;
					this.UpdateTabletsImpl();
				}
				while (this._hasUpdateTabletsBeenCalledReentrantly);
			}
			finally
			{
				this._inUpdateTablets = false;
				this._hasUpdateTabletsBeenCalledReentrantly = false;
			}
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x0005A3E8 File Offset: 0x000597E8
		[SecurityCritical]
		private void UpdateTabletsImpl()
		{
			PenThread penThread = (this._tablets.Length != 0) ? this._tablets[0].As<WispTabletDevice>().PenThread : PenThreadPool.GetPenThreadForPenContext(null);
			if (penThread == null)
			{
				return;
			}
			TabletDeviceInfo[] array = penThread.WorkerGetTabletsInfo();
			uint indexMouseTablet = uint.MaxValue;
			uint num = 0U;
			while ((ulong)num < (ulong)((long)array.Length))
			{
				if (array[(int)num].PimcTablet != null && array[(int)num].DeviceType == (TabletDeviceType)(-1))
				{
					indexMouseTablet = num;
					array[(int)num].PimcTablet = null;
				}
				num += 1U;
			}
			uint num2 = 0U;
			uint num3 = 0U;
			while ((ulong)num3 < (ulong)((long)array.Length))
			{
				if (array[(int)num3].PimcTablet != null)
				{
					num2 += 1U;
				}
				num3 += 1U;
			}
			TabletDevice[] array2 = new TabletDevice[num2];
			uint num4 = 0U;
			uint num5 = 0U;
			uint num6 = 0U;
			while ((ulong)num6 < (ulong)((long)array.Length))
			{
				if (array[(int)num6].PimcTablet != null)
				{
					int id = array[(int)num6].Id;
					if ((ulong)num4 < (ulong)((long)this._tablets.Length) && this._tablets[(int)num4] != null && this._tablets[(int)num4].Id == id)
					{
						array2[(int)num4] = this._tablets[(int)num4];
						this._tablets[(int)num4] = null;
						num5 += 1U;
					}
					else
					{
						TabletDevice tabletDevice = null;
						uint num7 = 0U;
						while ((ulong)num7 < (ulong)((long)this._tablets.Length))
						{
							if (this._tablets[(int)num7] != null && this._tablets[(int)num7].Id == id)
							{
								tabletDevice = this._tablets[(int)num7];
								this._tablets[(int)num7] = null;
								break;
							}
							num7 += 1U;
						}
						if (tabletDevice == null)
						{
							try
							{
								tabletDevice = new TabletDevice(new WispTabletDevice(array[(int)num6], penThread));
							}
							catch (InvalidOperationException ex)
							{
								if (ex.Data.Contains("System.Windows.Input.StylusLogic"))
								{
									goto IL_18A;
								}
								throw;
							}
						}
						array2[(int)num4] = tabletDevice;
					}
					num4 += 1U;
				}
				IL_18A:
				num6 += 1U;
			}
			if ((ulong)num5 == (ulong)((long)this._tablets.Length) && num5 == num4 && num4 == num2)
			{
				Array.Copy(array2, 0L, this._tablets, 0L, (long)((ulong)num2));
				this._indexMouseTablet = indexMouseTablet;
			}
			else
			{
				if (num4 != num2)
				{
					TabletDevice[] array3 = new TabletDevice[num4];
					Array.Copy(array2, 0L, array3, 0L, (long)((ulong)num4));
					array2 = array3;
				}
				this.DisposeTablets();
				this._tablets = array2;
				base.TabletDevices = new List<TabletDevice>(this._tablets);
				this._indexMouseTablet = indexMouseTablet;
			}
			this.DisposeDeferredTablets();
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x0005A630 File Offset: 0x00059A30
		[SecurityCritical]
		internal bool HandleTabletAdded(uint wisptisIndex, ref uint tabletIndexChanged)
		{
			if (this._tablets == null)
			{
				throw new ObjectDisposedException("TabletDeviceCollection");
			}
			tabletIndexChanged = uint.MaxValue;
			PenThread penThread = (this._tablets.Length != 0) ? this._tablets[0].As<WispTabletDevice>().PenThread : PenThreadPool.GetPenThreadForPenContext(null);
			if (penThread == null)
			{
				return true;
			}
			TabletDeviceInfo tabletDeviceInfo = penThread.WorkerGetTabletInfo(wisptisIndex);
			if (tabletDeviceInfo.PimcTablet == null)
			{
				return true;
			}
			if (tabletDeviceInfo.DeviceType == (TabletDeviceType)(-1))
			{
				this._indexMouseTablet = wisptisIndex;
				return false;
			}
			uint num = uint.MaxValue;
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)this._tablets.Length))
			{
				if (this._tablets[(int)num2].Id == tabletDeviceInfo.Id)
				{
					num = num2;
					break;
				}
				num2 += 1U;
			}
			uint num3 = uint.MaxValue;
			if (num != 4294967295U)
			{
				return false;
			}
			num3 = wisptisIndex;
			if (num3 > this._indexMouseTablet)
			{
				num3 -= 1U;
			}
			else
			{
				this._indexMouseTablet += 1U;
			}
			if ((ulong)num3 <= (ulong)((long)this._tablets.Length))
			{
				try
				{
					this.AddTablet(num3, new TabletDevice(new WispTabletDevice(tabletDeviceInfo, penThread)));
				}
				catch (InvalidOperationException ex)
				{
					if (ex.Data.Contains("System.Windows.Input.StylusLogic"))
					{
						return true;
					}
					throw;
				}
				tabletIndexChanged = num3;
				return true;
			}
			return true;
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x0005A760 File Offset: 0x00059B60
		[SecurityCritical]
		internal uint HandleTabletRemoved(uint wisptisIndex)
		{
			if (this._tablets == null)
			{
				throw new ObjectDisposedException("TabletDeviceCollection");
			}
			if (wisptisIndex == this._indexMouseTablet)
			{
				this._indexMouseTablet = uint.MaxValue;
				return uint.MaxValue;
			}
			uint num = wisptisIndex;
			if (wisptisIndex > this._indexMouseTablet)
			{
				num -= 1U;
			}
			else
			{
				this._indexMouseTablet -= 1U;
			}
			if ((ulong)num >= (ulong)((long)this._tablets.Length))
			{
				return uint.MaxValue;
			}
			this.RemoveTablet(num);
			return num;
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0005A7C8 File Offset: 0x00059BC8
		[SecurityCritical]
		private void AddTablet(uint index, TabletDevice tabletDevice)
		{
			TabletDevice[] array = new TabletDevice[base.Count + 1];
			uint num = (uint)(this._tablets.Length - (int)index);
			Array.Copy(this._tablets, 0L, array, 0L, (long)((ulong)index));
			array[(int)index] = tabletDevice;
			Array.Copy(this._tablets, (long)((ulong)index), array, (long)((ulong)(index + 1U)), (long)((ulong)num));
			this._tablets = array;
			base.TabletDevices = new List<TabletDevice>(this._tablets);
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x0005A834 File Offset: 0x00059C34
		[SecurityCritical]
		private void RemoveTablet(uint index)
		{
			WispTabletDevice wispTabletDevice = this._tablets[(int)index].As<WispTabletDevice>();
			TabletDevice[] array = new TabletDevice[this._tablets.Length - 1];
			uint num = (uint)(this._tablets.Length - (int)index - 1);
			Array.Copy(this._tablets, 0L, array, 0L, (long)((ulong)index));
			Array.Copy(this._tablets, (long)((ulong)(index + 1U)), array, (long)((ulong)index), (long)((ulong)num));
			this._tablets = array;
			base.TabletDevices = new List<TabletDevice>(this._tablets);
			wispTabletDevice.DisposeOrDeferDisposal();
			if (wispTabletDevice.IsDisposalPending)
			{
				this._deferredTablets.Add(wispTabletDevice.TabletDevice);
			}
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x0005A8CC File Offset: 0x00059CCC
		[SecurityCritical]
		internal WispStylusDevice UpdateStylusDevices(int tabletId, int stylusId)
		{
			if (this._tablets == null)
			{
				throw new ObjectDisposedException("TabletDeviceCollection");
			}
			int i = 0;
			int num = this._tablets.Length;
			while (i < num)
			{
				WispTabletDevice wispTabletDevice = this._tablets[i].As<WispTabletDevice>();
				if (wispTabletDevice.Id == tabletId)
				{
					return wispTabletDevice.UpdateStylusDevices(stylusId);
				}
				i++;
			}
			return null;
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x0005A924 File Offset: 0x00059D24
		[SecurityCritical]
		internal void DisposeTablets()
		{
			if (this._tablets != null)
			{
				int i = 0;
				int num = this._tablets.Length;
				while (i < num)
				{
					if (this._tablets[i] != null)
					{
						WispTabletDevice wispTabletDevice = this._tablets[i].TabletDeviceImpl.As<WispTabletDevice>();
						wispTabletDevice.DisposeOrDeferDisposal();
						if (wispTabletDevice.IsDisposalPending)
						{
							this._deferredTablets.Add(wispTabletDevice.TabletDevice);
						}
					}
					i++;
				}
				this._tablets = null;
				base.TabletDevices = null;
			}
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0005A998 File Offset: 0x00059D98
		[SecurityCritical]
		internal void DisposeDeferredTablets()
		{
			List<TabletDevice> list = new List<TabletDevice>();
			foreach (TabletDevice tabletDevice in this._deferredTablets)
			{
				WispTabletDevice wispTabletDevice = tabletDevice.TabletDeviceImpl.As<WispTabletDevice>();
				wispTabletDevice.DisposeOrDeferDisposal();
				if (wispTabletDevice.IsDisposalPending)
				{
					list.Add(tabletDevice);
				}
			}
			this._deferredTablets = list;
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0005AA20 File Offset: 0x00059E20
		[SecurityCritical]
		internal PenContext[] CreateContexts(IntPtr hwnd, PenContexts contexts)
		{
			int num = base.Count + this._deferredTablets.Count;
			PenContext[] array = new PenContext[num];
			int num2 = 0;
			foreach (TabletDevice tabletDevice in this._tablets)
			{
				array[num2++] = tabletDevice.As<WispTabletDevice>().CreateContext(hwnd, contexts);
			}
			foreach (TabletDevice tabletDevice2 in this._deferredTablets)
			{
				array[num2++] = tabletDevice2.As<WispTabletDevice>().CreateContext(hwnd, contexts);
			}
			return array;
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x0005AAE0 File Offset: 0x00059EE0
		internal List<TabletDevice> DeferredTablets
		{
			get
			{
				return this._deferredTablets;
			}
		}

		// Token: 0x04000C8F RID: 3215
		private const int VistaMajorVersion = 6;

		// Token: 0x04000C90 RID: 3216
		private TabletDevice[] _tablets = new TabletDevice[0];

		// Token: 0x04000C91 RID: 3217
		private uint _indexMouseTablet = uint.MaxValue;

		// Token: 0x04000C92 RID: 3218
		private bool _inUpdateTablets;

		// Token: 0x04000C93 RID: 3219
		private bool _hasUpdateTabletsBeenCalledReentrantly;

		// Token: 0x04000C94 RID: 3220
		private List<TabletDevice> _deferredTablets = new List<TabletDevice>();
	}
}
