using System;
using System.Collections;
using System.Security;
using MS.Internal;
using MS.Win32;

namespace System.Windows.Input
{
	// Token: 0x020002E1 RID: 737
	internal class TextServicesCompartmentContext
	{
		// Token: 0x06001665 RID: 5733 RVA: 0x00054338 File Offset: 0x00053738
		private TextServicesCompartmentContext()
		{
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0005434C File Offset: 0x0005374C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal TextServicesCompartment GetCompartment(InputMethodStateType statetype)
		{
			for (int i = 0; i < InputMethodEventTypeInfo.InfoList.Length; i++)
			{
				InputMethodEventTypeInfo inputMethodEventTypeInfo = InputMethodEventTypeInfo.InfoList[i];
				if (inputMethodEventTypeInfo.Type == statetype)
				{
					if (inputMethodEventTypeInfo.Scope == CompartmentScope.Thread)
					{
						return this.GetThreadCompartment(inputMethodEventTypeInfo.Guid);
					}
					if (inputMethodEventTypeInfo.Scope == CompartmentScope.Global)
					{
						return this.GetGlobalCompartment(inputMethodEventTypeInfo.Guid);
					}
				}
			}
			return null;
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x000543AC File Offset: 0x000537AC
		[SecurityCritical]
		internal TextServicesCompartment GetThreadCompartment(Guid guid)
		{
			if (!TextServicesLoader.ServicesInstalled || TextServicesContext.DispatcherCurrent == null)
			{
				return null;
			}
			UnsafeNativeMethods.ITfThreadMgr threadManager = TextServicesContext.DispatcherCurrent.ThreadManager;
			if (threadManager == null)
			{
				return null;
			}
			if (this._compartmentTable == null)
			{
				this._compartmentTable = new Hashtable();
			}
			TextServicesCompartment textServicesCompartment = this._compartmentTable[guid] as TextServicesCompartment;
			if (textServicesCompartment == null)
			{
				textServicesCompartment = new TextServicesCompartment(guid, threadManager as UnsafeNativeMethods.ITfCompartmentMgr);
				this._compartmentTable[guid] = textServicesCompartment;
			}
			return textServicesCompartment;
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00054428 File Offset: 0x00053828
		[SecurityCritical]
		internal TextServicesCompartment GetGlobalCompartment(Guid guid)
		{
			if (!TextServicesLoader.ServicesInstalled || TextServicesContext.DispatcherCurrent == null)
			{
				return null;
			}
			if (this._globalcompartmentTable == null)
			{
				this._globalcompartmentTable = new Hashtable();
			}
			if (this._globalcompartmentmanager == null)
			{
				UnsafeNativeMethods.ITfThreadMgr threadManager = TextServicesContext.DispatcherCurrent.ThreadManager;
				if (threadManager == null)
				{
					return null;
				}
				threadManager.GetGlobalCompartment(out this._globalcompartmentmanager);
			}
			TextServicesCompartment textServicesCompartment = this._globalcompartmentTable[guid] as TextServicesCompartment;
			if (textServicesCompartment == null)
			{
				textServicesCompartment = new TextServicesCompartment(guid, this._globalcompartmentmanager);
				this._globalcompartmentTable[guid] = textServicesCompartment;
			}
			return textServicesCompartment;
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x000544B8 File Offset: 0x000538B8
		internal static TextServicesCompartmentContext Current
		{
			get
			{
				if (InputMethod.Current.TextServicesCompartmentContext == null)
				{
					InputMethod.Current.TextServicesCompartmentContext = new TextServicesCompartmentContext();
				}
				return InputMethod.Current.TextServicesCompartmentContext;
			}
		}

		// Token: 0x04000C2E RID: 3118
		private Hashtable _compartmentTable;

		// Token: 0x04000C2F RID: 3119
		private Hashtable _globalcompartmentTable;

		// Token: 0x04000C30 RID: 3120
		[SecurityCritical]
		private UnsafeNativeMethods.ITfCompartmentMgr _globalcompartmentmanager;
	}
}
