using System;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.Text.TextInterface.Generics;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x02000014 RID: 20
	internal sealed class FontCollection
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x00010734 File Offset: 0x0000FB34
		[SecurityCritical]
		internal unsafe FontCollection(IDWriteFontCollection* fontCollection)
		{
			this._fontCollection = new NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteFontCollection>((IUnknown*)fontCollection);
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000F814 File Offset: 0x0000EC14
		internal unsafe uint FamilyCount
		{
			[SecuritySafeCritical]
			get
			{
				IDWriteFontCollection* value = this._fontCollection.Value;
				uint result = calli(System.UInt32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), value, *(*(long*)value + 24L));
				GC.KeepAlive(this._fontCollection);
				GC.KeepAlive(this);
				return result;
			}
		}

		// Token: 0x1700002D RID: 45
		internal FontFamily this[string familyName]
		{
			get
			{
				uint familyIndex;
				if (this.FindFamilyName(familyName, out familyIndex))
				{
					return this[familyIndex];
				}
				return null;
			}
		}

		// Token: 0x1700002E RID: 46
		internal unsafe FontFamily this[uint familyIndex]
		{
			[SecuritySafeCritical]
			get
			{
				IDWriteFontFamily* fontFamily = null;
				IDWriteFontCollection* value = this._fontCollection.Value;
				int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32,MS.Internal.Text.TextInterface.Native.IDWriteFontFamily**), value, familyIndex, ref fontFamily, *(*(long*)value + 32L));
				GC.KeepAlive(this._fontCollection);
				Util.ConvertHresultToException(hr);
				GC.KeepAlive(this);
				return new FontFamily(fontFamily);
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00010754 File Offset: 0x0000FB54
		[SecuritySafeCritical]
		[return: MarshalAs(UnmanagedType.U1)]
		internal unsafe bool FindFamilyName(string familyName, out uint index)
		{
			ref ushort ptrToStringChars = ref Util.GetPtrToStringChars(familyName);
			int num = 0;
			uint num2 = 0U;
			IDWriteFontCollection* value = this._fontCollection.Value;
			int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32*,System.Int32*), value, ref ptrToStringChars, ref num2, ref num, *(*(long*)value + 40L));
			GC.KeepAlive(this._fontCollection);
			Util.ConvertHresultToException(hr);
			index = num2;
			GC.KeepAlive(this);
			return ((num != 0) ? 1 : 0) != 0;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000107AC File Offset: 0x0000FBAC
		[SecuritySafeCritical]
		internal unsafe Font GetFontFromFontFace(FontFace fontFace)
		{
			IDWriteFontFace* dwriteFontFaceNoAddRef = fontFace.DWriteFontFaceNoAddRef;
			IDWriteFont* font = null;
			IDWriteFontCollection* value = this._fontCollection.Value;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.IDWriteFontFace*,MS.Internal.Text.TextInterface.Native.IDWriteFont**), value, dwriteFontFaceNoAddRef, ref font, *(*(long*)value + 48L));
			GC.KeepAlive(fontFace);
			GC.KeepAlive(this._fontCollection);
			if (-2003283966 == num)
			{
				GC.KeepAlive(this);
				return null;
			}
			Util.ConvertHresultToException(num);
			GC.KeepAlive(this);
			return new Font(font);
		}

		// Token: 0x04000328 RID: 808
		[SecurityCritical]
		private NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteFontCollection> _fontCollection;
	}
}
