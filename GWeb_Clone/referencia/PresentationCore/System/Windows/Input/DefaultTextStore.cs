using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Threading;
using MS.Internal;
using MS.Win32;

namespace System.Windows.Input
{
	// Token: 0x02000233 RID: 563
	internal class DefaultTextStore : UnsafeNativeMethods.ITfContextOwner, UnsafeNativeMethods.ITfContextOwnerCompositionSink, UnsafeNativeMethods.ITfTransitoryExtensionSink
	{
		// Token: 0x06000FAE RID: 4014 RVA: 0x0003BADC File Offset: 0x0003AEDC
		internal DefaultTextStore(Dispatcher dispatcher)
		{
			this._dispatcher = dispatcher;
			this._editCookie = -1;
			this._transitoryExtensionSinkCookie = -1;
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0003BB04 File Offset: 0x0003AF04
		public void GetACPFromPoint(ref UnsafeNativeMethods.POINT point, UnsafeNativeMethods.GetPositionFromPointFlags flags, out int position)
		{
			position = 0;
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0003BB14 File Offset: 0x0003AF14
		public void GetTextExt(int start, int end, out UnsafeNativeMethods.RECT rect, out bool clipped)
		{
			rect = default(UnsafeNativeMethods.RECT);
			clipped = false;
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0003BB2C File Offset: 0x0003AF2C
		public void GetScreenExt(out UnsafeNativeMethods.RECT rect)
		{
			rect = default(UnsafeNativeMethods.RECT);
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0003BB40 File Offset: 0x0003AF40
		public void GetStatus(out UnsafeNativeMethods.TS_STATUS status)
		{
			status = default(UnsafeNativeMethods.TS_STATUS);
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0003BB54 File Offset: 0x0003AF54
		public void GetWnd(out IntPtr hwnd)
		{
			hwnd = IntPtr.Zero;
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0003BB68 File Offset: 0x0003AF68
		public void GetValue(ref Guid guidAttribute, out object varValue)
		{
			varValue = null;
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0003BB78 File Offset: 0x0003AF78
		[SecuritySafeCritical]
		public void OnStartComposition(UnsafeNativeMethods.ITfCompositionView view, out bool ok)
		{
			ok = true;
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0003BB88 File Offset: 0x0003AF88
		[SecuritySafeCritical]
		public void OnUpdateComposition(UnsafeNativeMethods.ITfCompositionView view, UnsafeNativeMethods.ITfRange rangeNew)
		{
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0003BB98 File Offset: 0x0003AF98
		[SecuritySafeCritical]
		public void OnEndComposition(UnsafeNativeMethods.ITfCompositionView view)
		{
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0003BBA8 File Offset: 0x0003AFA8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public void OnTransitoryExtensionUpdated(UnsafeNativeMethods.ITfContext context, int ecReadOnly, UnsafeNativeMethods.ITfRange rangeResult, UnsafeNativeMethods.ITfRange rangeComposition, out bool fDeleteResultRange)
		{
			SecurityHelper.DemandUnmanagedCode();
			fDeleteResultRange = true;
			TextCompositionManager textCompositionManager = InputManager.Current.PrimaryKeyboardDevice.TextCompositionManager;
			if (rangeResult != null)
			{
				string text = this.StringFromITfRange(rangeResult, ecReadOnly);
				if (text.Length > 0)
				{
					if (this._composition == null)
					{
						this._composition = new DefaultTextStoreTextComposition(InputManager.Current, Keyboard.FocusedElement, text, TextCompositionAutoComplete.On);
						TextCompositionManager.StartComposition(this._composition);
						this._composition = null;
					}
					else
					{
						this._composition.SetCompositionText("");
						this._composition.SetText(text);
						TextCompositionManager.CompleteComposition(this._composition);
						this._composition = null;
					}
				}
			}
			if (rangeComposition != null)
			{
				string text2 = this.StringFromITfRange(rangeComposition, ecReadOnly);
				if (text2.Length > 0)
				{
					if (this._composition == null)
					{
						this._composition = new DefaultTextStoreTextComposition(InputManager.Current, Keyboard.FocusedElement, "", TextCompositionAutoComplete.Off);
						this._composition.SetCompositionText(text2);
						TextCompositionManager.StartComposition(this._composition);
						return;
					}
					this._composition.SetCompositionText(text2);
					this._composition.SetText("");
					TextCompositionManager.UpdateComposition(this._composition);
				}
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x0003BCC8 File Offset: 0x0003B0C8
		internal static DefaultTextStore Current
		{
			get
			{
				DefaultTextStore defaultTextStore = InputMethod.Current.DefaultTextStore;
				if (defaultTextStore == null)
				{
					defaultTextStore = new DefaultTextStore(Dispatcher.CurrentDispatcher);
					InputMethod.Current.DefaultTextStore = defaultTextStore;
					defaultTextStore.Register();
				}
				return defaultTextStore;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x0003BD00 File Offset: 0x0003B100
		// (set) Token: 0x06000FBB RID: 4027 RVA: 0x0003BD18 File Offset: 0x0003B118
		internal UnsafeNativeMethods.ITfDocumentMgr DocumentManager
		{
			[SecurityCritical]
			get
			{
				return this._doc.Value;
			}
			[SecurityCritical]
			set
			{
				this._doc = new SecurityCriticalData<UnsafeNativeMethods.ITfDocumentMgr>(value);
			}
		}

		// Token: 0x1700025F RID: 607
		// (set) Token: 0x06000FBC RID: 4028 RVA: 0x0003BD34 File Offset: 0x0003B134
		internal int EditCookie
		{
			set
			{
				this._editCookie = value;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x0003BD48 File Offset: 0x0003B148
		// (set) Token: 0x06000FBE RID: 4030 RVA: 0x0003BD5C File Offset: 0x0003B15C
		internal int TransitoryExtensionSinkCookie
		{
			get
			{
				return this._transitoryExtensionSinkCookie;
			}
			set
			{
				this._transitoryExtensionSinkCookie = value;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x0003BD70 File Offset: 0x0003B170
		internal UnsafeNativeMethods.ITfDocumentMgr TransitoryDocumentManager
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				SecurityHelper.DemandUnmanagedCode();
				UnsafeNativeMethods.ITfCompartmentMgr tfCompartmentMgr = (UnsafeNativeMethods.ITfCompartmentMgr)this.DocumentManager;
				Guid guid_COMPARTMENT_TRANSITORYEXTENSION_DOCUMENTMANAGER = UnsafeNativeMethods.GUID_COMPARTMENT_TRANSITORYEXTENSION_DOCUMENTMANAGER;
				UnsafeNativeMethods.ITfCompartment tfCompartment;
				tfCompartmentMgr.GetCompartment(ref guid_COMPARTMENT_TRANSITORYEXTENSION_DOCUMENTMANAGER, out tfCompartment);
				object obj;
				tfCompartment.GetValue(out obj);
				UnsafeNativeMethods.ITfDocumentMgr result = obj as UnsafeNativeMethods.ITfDocumentMgr;
				Marshal.ReleaseComObject(tfCompartment);
				return result;
			}
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0003BDB8 File Offset: 0x0003B1B8
		[SecurityCritical]
		private string StringFromITfRange(UnsafeNativeMethods.ITfRange range, int ecReadOnly)
		{
			UnsafeNativeMethods.ITfRangeACP tfRangeACP = (UnsafeNativeMethods.ITfRangeACP)range;
			int num;
			int num2;
			tfRangeACP.GetExtent(out num, out num2);
			char[] array = new char[num2];
			int num3;
			tfRangeACP.GetText(ecReadOnly, 0, array, num2, out num3);
			return new string(array);
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0003BDF0 File Offset: 0x0003B1F0
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void Register()
		{
			TextServicesContext.DispatcherCurrent.RegisterTextStore(this);
		}

		// Token: 0x04000893 RID: 2195
		private readonly Dispatcher _dispatcher;

		// Token: 0x04000894 RID: 2196
		private TextComposition _composition;

		// Token: 0x04000895 RID: 2197
		[SecurityCritical]
		private SecurityCriticalData<UnsafeNativeMethods.ITfDocumentMgr> _doc;

		// Token: 0x04000896 RID: 2198
		private int _editCookie;

		// Token: 0x04000897 RID: 2199
		private int _transitoryExtensionSinkCookie;
	}
}
