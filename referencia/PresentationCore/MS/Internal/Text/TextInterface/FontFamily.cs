using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x02000013 RID: 19
	[DefaultMember("Item")]
	internal sealed class FontFamily : FontList
	{
		// Token: 0x060002BB RID: 699 RVA: 0x000119AC File Offset: 0x00010DAC
		[SecurityCritical]
		internal unsafe FontFamily(IDWriteFontFamily* fontFamily) : base((IDWriteFontList*)fontFamily)
		{
			this._regularFont = null;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060002BC RID: 700 RVA: 0x000119C8 File Offset: 0x00010DC8
		internal unsafe LocalizedStrings FamilyNames
		{
			[SecuritySafeCritical]
			get
			{
				IDWriteFontList* value = base.FontListObject.Value;
				IDWriteLocalizedStrings* localizedStrings;
				int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.IDWriteLocalizedStrings**), value, ref localizedStrings, *(*(long*)value + 48L));
				GC.KeepAlive(base.FontListObject);
				Util.ConvertHresultToException(hr);
				GC.KeepAlive(this);
				return new LocalizedStrings(localizedStrings);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000B738 File Offset: 0x0000AB38
		internal bool IsPhysical
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return true;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000BBC8 File Offset: 0x0000AFC8
		internal bool IsComposite
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return false;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00011A0C File Offset: 0x00010E0C
		internal string OrdinalName
		{
			get
			{
				if (this.FamilyNames.StringsCount > 0U)
				{
					return this.FamilyNames.GetString(0U);
				}
				return string.Empty;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00011A3C File Offset: 0x00010E3C
		internal FontMetrics Metrics
		{
			get
			{
				if (this._regularFont == null)
				{
					this._regularFont = this.GetFirstMatchingFont(FontWeight.Normal, FontStretch.Normal, FontStyle.Normal);
				}
				return this._regularFont.Metrics;
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00011A70 File Offset: 0x00010E70
		internal FontMetrics DisplayMetrics(float emSize, float pixelsPerDip)
		{
			return this.GetFirstMatchingFont(FontWeight.Normal, FontStretch.Normal, FontStyle.Normal).DisplayMetrics(emSize, pixelsPerDip);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00010A3C File Offset: 0x0000FE3C
		[SecuritySafeCritical]
		internal unsafe Font GetFirstMatchingFont(FontWeight weight, FontStretch stretch, FontStyle style)
		{
			IDWriteFontList* value = base.FontListObject.Value;
			IDWriteFont* font;
			int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.DWRITE_FONT_WEIGHT,MS.Internal.Text.TextInterface.Native.DWRITE_FONT_STRETCH,MS.Internal.Text.TextInterface.Native.DWRITE_FONT_STYLE,MS.Internal.Text.TextInterface.Native.IDWriteFont**), value, DWriteTypeConverter.Convert(weight), DWriteTypeConverter.Convert(stretch), DWriteTypeConverter.Convert(style), ref font, *(*(long*)value + 56L));
			GC.KeepAlive(base.FontListObject);
			Util.ConvertHresultToException(hr);
			GC.KeepAlive(this);
			return new Font(font);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00011A94 File Offset: 0x00010E94
		[SecuritySafeCritical]
		internal unsafe FontList GetMatchingFonts(FontWeight weight, FontStretch stretch, FontStyle style)
		{
			IDWriteFontList* value = base.FontListObject.Value;
			IDWriteFontList* fontList;
			int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.DWRITE_FONT_WEIGHT,MS.Internal.Text.TextInterface.Native.DWRITE_FONT_STRETCH,MS.Internal.Text.TextInterface.Native.DWRITE_FONT_STYLE,MS.Internal.Text.TextInterface.Native.IDWriteFontList**), value, DWriteTypeConverter.Convert(weight), DWriteTypeConverter.Convert(stretch), DWriteTypeConverter.Convert(style), ref fontList, *(*(long*)value + 64L));
			GC.KeepAlive(base.FontListObject);
			Util.ConvertHresultToException(hr);
			GC.KeepAlive(this);
			return new FontList(fontList);
		}

		// Token: 0x04000327 RID: 807
		private Font _regularFont;
	}
}
