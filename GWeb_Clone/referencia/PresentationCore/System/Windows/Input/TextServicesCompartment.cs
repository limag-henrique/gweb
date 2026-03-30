using System;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal;
using MS.Win32;

namespace System.Windows.Input
{
	// Token: 0x020002E0 RID: 736
	internal class TextServicesCompartment
	{
		// Token: 0x0600165B RID: 5723 RVA: 0x00054180 File Offset: 0x00053580
		[SecurityCritical]
		internal TextServicesCompartment(Guid guid, UnsafeNativeMethods.ITfCompartmentMgr compartmentmgr)
		{
			this._guid = guid;
			this._compartmentmgr = new SecurityCriticalData<UnsafeNativeMethods.ITfCompartmentMgr>(compartmentmgr);
			this._cookie = -1;
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x000541B0 File Offset: 0x000535B0
		[SecurityCritical]
		internal void AdviseNotifySink(UnsafeNativeMethods.ITfCompartmentEventSink sink)
		{
			UnsafeNativeMethods.ITfCompartment itfCompartment = this.GetITfCompartment();
			if (itfCompartment == null)
			{
				return;
			}
			UnsafeNativeMethods.ITfSource tfSource = itfCompartment as UnsafeNativeMethods.ITfSource;
			Guid iid_ITfCompartmentEventSink = UnsafeNativeMethods.IID_ITfCompartmentEventSink;
			tfSource.AdviseSink(ref iid_ITfCompartmentEventSink, sink, out this._cookie);
			Marshal.ReleaseComObject(itfCompartment);
			Marshal.ReleaseComObject(tfSource);
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x000541F4 File Offset: 0x000535F4
		[SecurityCritical]
		internal void UnadviseNotifySink()
		{
			UnsafeNativeMethods.ITfCompartment itfCompartment = this.GetITfCompartment();
			if (itfCompartment == null)
			{
				return;
			}
			UnsafeNativeMethods.ITfSource tfSource = itfCompartment as UnsafeNativeMethods.ITfSource;
			tfSource.UnadviseSink(this._cookie);
			this._cookie = -1;
			Marshal.ReleaseComObject(itfCompartment);
			Marshal.ReleaseComObject(tfSource);
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00054234 File Offset: 0x00053634
		[SecurityCritical]
		internal UnsafeNativeMethods.ITfCompartment GetITfCompartment()
		{
			UnsafeNativeMethods.ITfCompartment result;
			this._compartmentmgr.Value.GetCompartment(ref this._guid, out result);
			return result;
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x00054260 File Offset: 0x00053660
		// (set) Token: 0x06001660 RID: 5728 RVA: 0x00054284 File Offset: 0x00053684
		internal bool BooleanValue
		{
			get
			{
				object value = this.Value;
				return value != null && (int)value != 0;
			}
			set
			{
				this.Value = (value ? 1 : 0);
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x000542A4 File Offset: 0x000536A4
		// (set) Token: 0x06001662 RID: 5730 RVA: 0x000542C4 File Offset: 0x000536C4
		internal int IntValue
		{
			get
			{
				object value = this.Value;
				if (value == null)
				{
					return 0;
				}
				return (int)value;
			}
			set
			{
				this.Value = value;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x000542E0 File Offset: 0x000536E0
		// (set) Token: 0x06001664 RID: 5732 RVA: 0x0005430C File Offset: 0x0005370C
		internal object Value
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				UnsafeNativeMethods.ITfCompartment itfCompartment = this.GetITfCompartment();
				if (itfCompartment == null)
				{
					return null;
				}
				object result;
				itfCompartment.GetValue(out result);
				Marshal.ReleaseComObject(itfCompartment);
				return result;
			}
			[SecurityCritical]
			[SecurityTreatAsSafe]
			set
			{
				UnsafeNativeMethods.ITfCompartment itfCompartment = this.GetITfCompartment();
				if (itfCompartment == null)
				{
					return;
				}
				itfCompartment.SetValue(0, ref value);
				Marshal.ReleaseComObject(itfCompartment);
			}
		}

		// Token: 0x04000C2B RID: 3115
		[SecurityCritical]
		private readonly SecurityCriticalData<UnsafeNativeMethods.ITfCompartmentMgr> _compartmentmgr;

		// Token: 0x04000C2C RID: 3116
		private Guid _guid;

		// Token: 0x04000C2D RID: 3117
		private int _cookie;
	}
}
