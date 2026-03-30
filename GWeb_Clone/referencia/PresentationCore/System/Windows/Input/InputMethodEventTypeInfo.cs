using System;
using MS.Win32;

namespace System.Windows.Input
{
	// Token: 0x02000257 RID: 599
	internal class InputMethodEventTypeInfo
	{
		// Token: 0x060010F0 RID: 4336 RVA: 0x0003FF6C File Offset: 0x0003F36C
		internal InputMethodEventTypeInfo(InputMethodStateType type, Guid guid, CompartmentScope scope)
		{
			this._inputmethodstatetype = type;
			this._guid = guid;
			this._scope = scope;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0003FF94 File Offset: 0x0003F394
		internal static InputMethodStateType ToType(ref Guid rguid)
		{
			for (int i = 0; i < InputMethodEventTypeInfo._iminfo.Length; i++)
			{
				InputMethodEventTypeInfo inputMethodEventTypeInfo = InputMethodEventTypeInfo._iminfo[i];
				if (rguid == inputMethodEventTypeInfo._guid)
				{
					return inputMethodEventTypeInfo._inputmethodstatetype;
				}
			}
			return InputMethodStateType.Invalid;
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x0003FFD8 File Offset: 0x0003F3D8
		internal InputMethodStateType Type
		{
			get
			{
				return this._inputmethodstatetype;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x0003FFEC File Offset: 0x0003F3EC
		internal Guid Guid
		{
			get
			{
				return this._guid;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x00040000 File Offset: 0x0003F400
		internal CompartmentScope Scope
		{
			get
			{
				return this._scope;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x00040014 File Offset: 0x0003F414
		internal static InputMethodEventTypeInfo[] InfoList
		{
			get
			{
				return InputMethodEventTypeInfo._iminfo;
			}
		}

		// Token: 0x04000923 RID: 2339
		private static readonly InputMethodEventTypeInfo _iminfoImeState = new InputMethodEventTypeInfo(InputMethodStateType.ImeState, UnsafeNativeMethods.GUID_COMPARTMENT_KEYBOARD_OPENCLOSE, CompartmentScope.Thread);

		// Token: 0x04000924 RID: 2340
		private static readonly InputMethodEventTypeInfo _iminfoHandwritingState = new InputMethodEventTypeInfo(InputMethodStateType.HandwritingState, UnsafeNativeMethods.GUID_COMPARTMENT_HANDWRITING_OPENCLOSE, CompartmentScope.Thread);

		// Token: 0x04000925 RID: 2341
		private static readonly InputMethodEventTypeInfo _iminfoMicrophoneState = new InputMethodEventTypeInfo(InputMethodStateType.MicrophoneState, UnsafeNativeMethods.GUID_COMPARTMENT_SPEECH_OPENCLOSE, CompartmentScope.Global);

		// Token: 0x04000926 RID: 2342
		private static readonly InputMethodEventTypeInfo _iminfoSpeechMode = new InputMethodEventTypeInfo(InputMethodStateType.SpeechMode, UnsafeNativeMethods.GUID_COMPARTMENT_SPEECH_GLOBALSTATE, CompartmentScope.Global);

		// Token: 0x04000927 RID: 2343
		private static readonly InputMethodEventTypeInfo _iminfoImeConversionMode = new InputMethodEventTypeInfo(InputMethodStateType.ImeConversionModeValues, UnsafeNativeMethods.GUID_COMPARTMENT_KEYBOARD_INPUTMODE_CONVERSION, CompartmentScope.Thread);

		// Token: 0x04000928 RID: 2344
		private static readonly InputMethodEventTypeInfo _iminfoImeSentenceMode = new InputMethodEventTypeInfo(InputMethodStateType.ImeSentenceModeValues, UnsafeNativeMethods.GUID_COMPARTMENT_KEYBOARD_INPUTMODE_SENTENCE, CompartmentScope.Thread);

		// Token: 0x04000929 RID: 2345
		private static readonly InputMethodEventTypeInfo[] _iminfo = new InputMethodEventTypeInfo[]
		{
			InputMethodEventTypeInfo._iminfoImeState,
			InputMethodEventTypeInfo._iminfoHandwritingState,
			InputMethodEventTypeInfo._iminfoMicrophoneState,
			InputMethodEventTypeInfo._iminfoSpeechMode,
			InputMethodEventTypeInfo._iminfoImeConversionMode,
			InputMethodEventTypeInfo._iminfoImeSentenceMode
		};

		// Token: 0x0400092A RID: 2346
		private InputMethodStateType _inputmethodstatetype;

		// Token: 0x0400092B RID: 2347
		private Guid _guid;

		// Token: 0x0400092C RID: 2348
		private CompartmentScope _scope;
	}
}
