using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Threading;
using MS.Internal;
using MS.Win32;

namespace System.Windows.Input
{
	// Token: 0x020002DE RID: 734
	internal class TextServicesContext
	{
		// Token: 0x06001649 RID: 5705 RVA: 0x00053A3C File Offset: 0x00052E3C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private TextServicesContext()
		{
			TextServicesContext.TextServicesContextShutDownListener textServicesContextShutDownListener = new TextServicesContext.TextServicesContextShutDownListener(this, ShutDownEvents.DomainUnload | ShutDownEvents.DispatcherShutdown);
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00053A58 File Offset: 0x00052E58
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal void Uninitialize(bool appDomainShutdown)
		{
			if (this._defaultTextStore != null)
			{
				this.StopTransitoryExtension();
				if (this._defaultTextStore.DocumentManager != null)
				{
					this._defaultTextStore.DocumentManager.Pop(UnsafeNativeMethods.PopFlags.TF_POPF_ALL);
					Marshal.ReleaseComObject(this._defaultTextStore.DocumentManager);
					this._defaultTextStore.DocumentManager = null;
				}
				if (!appDomainShutdown)
				{
					InputMethod.Current.DefaultTextStore = null;
				}
				this._defaultTextStore = null;
			}
			if (this._istimactivated)
			{
				if (!appDomainShutdown || Environment.OSVersion.Version.Major >= 6)
				{
					this._threadManager.Value.Deactivate();
				}
				this._istimactivated = false;
			}
			if (this._dimEmpty != null)
			{
				if (this._dimEmpty.Value != null)
				{
					Marshal.ReleaseComObject(this._dimEmpty.Value);
				}
				this._dimEmpty = null;
			}
			if (this._threadManager != null)
			{
				if (this._threadManager.Value != null)
				{
					Marshal.ReleaseComObject(this._threadManager.Value);
				}
				this._threadManager = null;
			}
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x00053B54 File Offset: 0x00052F54
		[SecurityCritical]
		internal bool Keystroke(int wParam, int lParam, TextServicesContext.KeyOp op)
		{
			if (this._threadManager == null || this._threadManager.Value == null)
			{
				return false;
			}
			UnsafeNativeMethods.ITfKeystrokeMgr tfKeystrokeMgr = this._threadManager.Value as UnsafeNativeMethods.ITfKeystrokeMgr;
			bool result;
			switch (op)
			{
			case TextServicesContext.KeyOp.TestUp:
				tfKeystrokeMgr.TestKeyUp(wParam, lParam, out result);
				break;
			case TextServicesContext.KeyOp.TestDown:
				tfKeystrokeMgr.TestKeyDown(wParam, lParam, out result);
				break;
			case TextServicesContext.KeyOp.Up:
				tfKeystrokeMgr.KeyUp(wParam, lParam, out result);
				break;
			case TextServicesContext.KeyOp.Down:
				tfKeystrokeMgr.KeyDown(wParam, lParam, out result);
				break;
			default:
				result = false;
				break;
			}
			return result;
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00053BD4 File Offset: 0x00052FD4
		[SecurityCritical]
		internal void RegisterTextStore(DefaultTextStore defaultTextStore)
		{
			this._defaultTextStore = defaultTextStore;
			UnsafeNativeMethods.ITfThreadMgr threadManager = this.ThreadManager;
			if (threadManager != null)
			{
				int editCookie = -1;
				if (!this._istimactivated)
				{
					int value;
					threadManager.Activate(out value);
					this._clientId = new SecurityCriticalData<int>(value);
					this._istimactivated = true;
				}
				UnsafeNativeMethods.ITfDocumentMgr tfDocumentMgr;
				threadManager.CreateDocumentMgr(out tfDocumentMgr);
				UnsafeNativeMethods.ITfContext tfContext;
				tfDocumentMgr.CreateContext(this._clientId.Value, (UnsafeNativeMethods.CreateContextFlags)0, this._defaultTextStore, out tfContext, out editCookie);
				tfDocumentMgr.Push(tfContext);
				Marshal.ReleaseComObject(tfContext);
				this._defaultTextStore.DocumentManager = tfDocumentMgr;
				this._defaultTextStore.EditCookie = editCookie;
				this.StartTransitoryExtension();
			}
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x00053C6C File Offset: 0x0005306C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal void SetFocusOnDefaultTextStore()
		{
			this.SetFocusOnDim(DefaultTextStore.Current.DocumentManager);
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x00053C8C File Offset: 0x0005308C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal void SetFocusOnEmptyDim()
		{
			this.SetFocusOnDim(this.EmptyDocumentManager);
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x00053CA8 File Offset: 0x000530A8
		internal static TextServicesContext DispatcherCurrent
		{
			get
			{
				if (InputMethod.Current.TextServicesContext == null)
				{
					InputMethod.Current.TextServicesContext = new TextServicesContext();
				}
				return InputMethod.Current.TextServicesContext;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x00053CDC File Offset: 0x000530DC
		internal UnsafeNativeMethods.ITfThreadMgr ThreadManager
		{
			[SecurityCritical]
			get
			{
				if (this._threadManager == null)
				{
					this._threadManager = new SecurityCriticalDataClass<UnsafeNativeMethods.ITfThreadMgr>(TextServicesLoader.Load());
				}
				return this._threadManager.Value;
			}
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x00053D0C File Offset: 0x0005310C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void SetFocusOnDim(UnsafeNativeMethods.ITfDocumentMgr dim)
		{
			UnsafeNativeMethods.ITfThreadMgr threadManager = this.ThreadManager;
			if (threadManager != null)
			{
				threadManager.SetFocus(dim);
			}
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00053D2C File Offset: 0x0005312C
		[SecurityCritical]
		private void StartTransitoryExtension()
		{
			UnsafeNativeMethods.ITfCompartmentMgr tfCompartmentMgr = this._defaultTextStore.DocumentManager as UnsafeNativeMethods.ITfCompartmentMgr;
			Guid guid = UnsafeNativeMethods.GUID_COMPARTMENT_TRANSITORYEXTENSION;
			UnsafeNativeMethods.ITfCompartment tfCompartment;
			tfCompartmentMgr.GetCompartment(ref guid, out tfCompartment);
			object obj = 1;
			tfCompartment.SetValue(0, ref obj);
			guid = UnsafeNativeMethods.IID_ITfTransitoryExtensionSink;
			UnsafeNativeMethods.ITfSource tfSource = this._defaultTextStore.DocumentManager as UnsafeNativeMethods.ITfSource;
			if (tfSource != null)
			{
				int transitoryExtensionSinkCookie;
				tfSource.AdviseSink(ref guid, this._defaultTextStore, out transitoryExtensionSinkCookie);
				this._defaultTextStore.TransitoryExtensionSinkCookie = transitoryExtensionSinkCookie;
			}
			Marshal.ReleaseComObject(tfCompartment);
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00053DAC File Offset: 0x000531AC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void StopTransitoryExtension()
		{
			if (this._defaultTextStore.TransitoryExtensionSinkCookie != -1)
			{
				UnsafeNativeMethods.ITfSource tfSource = this._defaultTextStore.DocumentManager as UnsafeNativeMethods.ITfSource;
				if (tfSource != null)
				{
					tfSource.UnadviseSink(this._defaultTextStore.TransitoryExtensionSinkCookie);
				}
				this._defaultTextStore.TransitoryExtensionSinkCookie = -1;
			}
			UnsafeNativeMethods.ITfCompartmentMgr tfCompartmentMgr = this._defaultTextStore.DocumentManager as UnsafeNativeMethods.ITfCompartmentMgr;
			if (tfCompartmentMgr != null)
			{
				Guid guid_COMPARTMENT_TRANSITORYEXTENSION = UnsafeNativeMethods.GUID_COMPARTMENT_TRANSITORYEXTENSION;
				UnsafeNativeMethods.ITfCompartment tfCompartment;
				tfCompartmentMgr.GetCompartment(ref guid_COMPARTMENT_TRANSITORYEXTENSION, out tfCompartment);
				if (tfCompartment != null)
				{
					object obj = 0;
					tfCompartment.SetValue(0, ref obj);
					Marshal.ReleaseComObject(tfCompartment);
				}
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x00053E38 File Offset: 0x00053238
		private UnsafeNativeMethods.ITfDocumentMgr EmptyDocumentManager
		{
			[SecurityCritical]
			get
			{
				if (this._dimEmpty == null)
				{
					UnsafeNativeMethods.ITfThreadMgr threadManager = this.ThreadManager;
					if (threadManager == null)
					{
						return null;
					}
					UnsafeNativeMethods.ITfDocumentMgr value;
					threadManager.CreateDocumentMgr(out value);
					this._dimEmpty = new SecurityCriticalDataClass<UnsafeNativeMethods.ITfDocumentMgr>(value);
				}
				return this._dimEmpty.Value;
			}
		}

		// Token: 0x04000C25 RID: 3109
		private DefaultTextStore _defaultTextStore;

		// Token: 0x04000C26 RID: 3110
		private bool _istimactivated;

		// Token: 0x04000C27 RID: 3111
		[SecurityCritical]
		private SecurityCriticalDataClass<UnsafeNativeMethods.ITfThreadMgr> _threadManager;

		// Token: 0x04000C28 RID: 3112
		[SecurityCritical]
		private SecurityCriticalData<int> _clientId;

		// Token: 0x04000C29 RID: 3113
		[SecurityCritical]
		private SecurityCriticalDataClass<UnsafeNativeMethods.ITfDocumentMgr> _dimEmpty;

		// Token: 0x02000820 RID: 2080
		internal enum KeyOp
		{
			// Token: 0x0400277F RID: 10111
			TestUp,
			// Token: 0x04002780 RID: 10112
			TestDown,
			// Token: 0x04002781 RID: 10113
			Up,
			// Token: 0x04002782 RID: 10114
			Down
		}

		// Token: 0x02000821 RID: 2081
		private sealed class TextServicesContextShutDownListener : ShutDownListener
		{
			// Token: 0x06005635 RID: 22069 RVA: 0x001625A0 File Offset: 0x001619A0
			[SecurityTreatAsSafe]
			[SecurityCritical]
			public TextServicesContextShutDownListener(TextServicesContext target, ShutDownEvents events) : base(target, events)
			{
			}

			// Token: 0x06005636 RID: 22070 RVA: 0x001625B8 File Offset: 0x001619B8
			internal override void OnShutDown(object target, object sender, EventArgs e)
			{
				TextServicesContext textServicesContext = (TextServicesContext)target;
				textServicesContext.Uninitialize(!(sender is Dispatcher));
			}
		}
	}
}
