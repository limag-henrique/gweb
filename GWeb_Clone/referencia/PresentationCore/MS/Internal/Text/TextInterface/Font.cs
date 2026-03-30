using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using MS.Internal.Text.TextInterface.Generics;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x0200000F RID: 15
	internal sealed class Font
	{
		// Token: 0x06000298 RID: 664 RVA: 0x00011EB4 File Offset: 0x000112B4
		private FontFace AddFontFaceToCache()
		{
			FontFace fontFace = this.CreateFontFace();
			FontFace fontFace2 = null;
			if (Interlocked.Increment(ref Font._mutex) == 1)
			{
				if (null == Font._fontFaceCache)
				{
					Font._fontFaceCache = new Font.FontFaceCacheEntry[4];
				}
				Font._fontFaceCacheMRU = (Font._fontFaceCacheMRU + 1) % 4;
				int num = 0;
				while (Font._fontFaceCache[num].font != null)
				{
					num++;
					if (num >= 4)
					{
						IL_5E:
						fontFace2 = Font._fontFaceCache[Font._fontFaceCacheMRU].fontFace;
						Font._fontFaceCache[Font._fontFaceCacheMRU].font = this;
						Font._fontFaceCache[Font._fontFaceCacheMRU].fontFace = fontFace;
						fontFace.AddRef();
						goto IL_A3;
					}
				}
				Font._fontFaceCacheMRU = num;
				goto IL_5E;
			}
			IL_A3:
			Interlocked.Decrement(ref Font._mutex);
			if (fontFace2 != null)
			{
				fontFace2.Release();
			}
			return fontFace;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000BB00 File Offset: 0x0000AF00
		private FontFace LookupFontFaceSlow()
		{
			FontFace fontFace = null;
			int num = 0;
			while (Font._fontFaceCache[num].font != this)
			{
				num++;
				if (num >= 4)
				{
					return fontFace;
				}
			}
			fontFace = Font._fontFaceCache[num].fontFace;
			fontFace.AddRef();
			Font._fontFaceCacheMRU = num;
			return fontFace;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000117F8 File Offset: 0x00010BF8
		[SecuritySafeCritical]
		private unsafe FontFace CreateFontFace()
		{
			IDWriteFont* value = this._font.Value;
			IDWriteFontFace* fontFace;
			int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.IDWriteFontFace**), value, ref fontFace, *(*(long*)value + 104L));
			GC.KeepAlive(this._font);
			Util.ConvertHresultToException(hr);
			GC.KeepAlive(this);
			return new FontFace(fontFace);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00010634 File Offset: 0x0000FA34
		[SecurityCritical]
		internal unsafe Font(IDWriteFont* font)
		{
			this._font = new NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteFont>((IUnknown*)font);
			this._version = double.MinValue;
			this._flags = 0;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000F62C File Offset: 0x0000EA2C
		internal unsafe IntPtr DWriteFontAddRef
		{
			[SecurityCritical]
			get
			{
				IDWriteFont* value = this._font.Value;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), value, *(*(long*)value + 8L));
				GC.KeepAlive(this);
				return (IntPtr)((void*)this._font.Value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00011FF8 File Offset: 0x000113F8
		internal unsafe FontFamily Family
		{
			[SecuritySafeCritical]
			get
			{
				IDWriteFont* value = this._font.Value;
				IDWriteFontFamily* fontFamily;
				int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.IDWriteFontFamily**), value, ref fontFamily, *(*(long*)value + 24L));
				GC.KeepAlive(this._font);
				Util.ConvertHresultToException(hr);
				GC.KeepAlive(this);
				return new FontFamily(fontFamily);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000F668 File Offset: 0x0000EA68
		internal unsafe FontWeight Weight
		{
			[SecuritySafeCritical]
			get
			{
				IDWriteFont* value = this._font.Value;
				DWRITE_FONT_WEIGHT fontWeight = calli(MS.Internal.Text.TextInterface.Native.DWRITE_FONT_WEIGHT modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), value, *(*(long*)value + 32L));
				GC.KeepAlive(this._font);
				GC.KeepAlive(this);
				return DWriteTypeConverter.Convert(fontWeight);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000F6A4 File Offset: 0x0000EAA4
		internal unsafe FontStretch Stretch
		{
			[SecuritySafeCritical]
			get
			{
				IDWriteFont* value = this._font.Value;
				DWRITE_FONT_STRETCH fontStrech = calli(MS.Internal.Text.TextInterface.Native.DWRITE_FONT_STRETCH modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), value, *(*(long*)value + 40L));
				GC.KeepAlive(this._font);
				GC.KeepAlive(this);
				return DWriteTypeConverter.Convert(fontStrech);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000F6E0 File Offset: 0x0000EAE0
		internal unsafe FontStyle Style
		{
			[SecuritySafeCritical]
			get
			{
				IDWriteFont* value = this._font.Value;
				DWRITE_FONT_STYLE fontStyle = calli(MS.Internal.Text.TextInterface.Native.DWRITE_FONT_STYLE modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), value, *(*(long*)value + 48L));
				GC.KeepAlive(this._font);
				GC.KeepAlive(this);
				return DWriteTypeConverter.Convert(fontStyle);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000F71C File Offset: 0x0000EB1C
		internal unsafe bool IsSymbolFont
		{
			[SecuritySafeCritical]
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				if ((this._flags & 2) == 0)
				{
					IDWriteFont* value = this._font.Value;
					bool flag = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), value, *(*(long*)value + 56L));
					GC.KeepAlive(this._font);
					int num = this._flags | 2;
					this._flags = num;
					if (flag)
					{
						this._flags = (num | 4);
					}
				}
				GC.KeepAlive(this);
				return (byte)((uint)this._flags >> 2 & 1U) != 0;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00011758 File Offset: 0x00010B58
		internal unsafe LocalizedStrings FaceNames
		{
			[SecuritySafeCritical]
			get
			{
				IDWriteFont* value = this._font.Value;
				IDWriteLocalizedStrings* localizedStrings;
				int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.IDWriteLocalizedStrings**), value, ref localizedStrings, *(*(long*)value + 64L));
				GC.KeepAlive(this._font);
				Util.ConvertHresultToException(hr);
				GC.KeepAlive(this);
				return new LocalizedStrings(localizedStrings);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000F784 File Offset: 0x0000EB84
		internal unsafe FontSimulations SimulationFlags
		{
			[SecuritySafeCritical]
			get
			{
				IDWriteFont* value = this._font.Value;
				DWRITE_FONT_SIMULATIONS fontSimulations = calli(MS.Internal.Text.TextInterface.Native.DWRITE_FONT_SIMULATIONS modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), value, *(*(long*)value + 80L));
				GC.KeepAlive(this._font);
				GC.KeepAlive(this);
				return DWriteTypeConverter.Convert(fontSimulations);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000F7C0 File Offset: 0x0000EBC0
		internal unsafe FontMetrics Metrics
		{
			[SecuritySafeCritical]
			get
			{
				if (this._fontMetrics == null)
				{
					IDWriteFont* value = this._font.Value;
					DWRITE_FONT_METRICS dwriteFontMetrics;
					calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.DWRITE_FONT_METRICS*), value, ref dwriteFontMetrics, *(*(long*)value + 88L));
					GC.KeepAlive(this._font);
					this._fontMetrics = DWriteTypeConverter.Convert(dwriteFontMetrics);
				}
				GC.KeepAlive(this);
				return this._fontMetrics;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0001183C File Offset: 0x00010C3C
		internal double Version
		{
			get
			{
				if ((this._flags & 1) == 0)
				{
					LocalizedStrings localizedStrings = null;
					double version = 0.0;
					if (this.GetInformationalStrings(InformationalStringID.VersionStrings, out localizedStrings))
					{
						string text = localizedStrings.GetString(0U);
						if (text.Length > 1)
						{
							string text2 = text;
							text = text2.Substring(text2.LastIndexOf(' ') + 1);
							if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out version))
							{
								version = 0.0;
							}
						}
					}
					this._flags |= 1;
					this._version = version;
				}
				return this._version;
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000106B8 File Offset: 0x0000FAB8
		[SecuritySafeCritical]
		internal unsafe FontMetrics DisplayMetrics(float emSize, float pixelsPerDip)
		{
			IDWriteFontFace* ptr = null;
			IDWriteFont* value = this._font.Value;
			Util.ConvertHresultToException(calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.IDWriteFontFace**), value, ref ptr, *(*(long*)value + 104L)));
			DWRITE_MATRIX identityTransform = Factory.GetIdentityTransform();
			DWRITE_FONT_METRICS dwriteFontMetrics;
			int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Single,System.Single,MS.Internal.Text.TextInterface.Native.DWRITE_MATRIX modopt(System.Runtime.CompilerServices.IsConst)*,MS.Internal.Text.TextInterface.Native.DWRITE_FONT_METRICS*), ptr, emSize, pixelsPerDip, ref identityTransform, ref dwriteFontMetrics, *(*(long*)ptr + 128L));
			if (ptr != null)
			{
				IDWriteFontFace* ptr2 = ptr;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
			}
			Util.ConvertHresultToException(hr);
			GC.KeepAlive(this._font);
			GC.KeepAlive(this);
			return DWriteTypeConverter.Convert(dwriteFontMetrics);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000BB4C File Offset: 0x0000AF4C
		internal static void ResetFontFaceCache()
		{
			Font.FontFaceCacheEntry[] array = null;
			if (Interlocked.Increment(ref Font._mutex) == 1)
			{
				array = Font._fontFaceCache;
				Font._fontFaceCache = null;
			}
			Interlocked.Decrement(ref Font._mutex);
			if (array != null)
			{
				int num = 0;
				do
				{
					FontFace fontFace = array[num].fontFace;
					if (fontFace != null)
					{
						fontFace.Release();
					}
					num++;
				}
				while (num < 4);
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00011F7C File Offset: 0x0001137C
		internal FontFace GetFontFace()
		{
			FontFace fontFace = null;
			if (Interlocked.Increment(ref Font._mutex) == 1 && Font._fontFaceCache != null)
			{
				Font.FontFaceCacheEntry fontFaceCacheEntry = default(Font.FontFaceCacheEntry);
				fontFaceCacheEntry = Font._fontFaceCache[Font._fontFaceCacheMRU];
				if (fontFaceCacheEntry.font == this)
				{
					FontFace fontFace2 = fontFaceCacheEntry.fontFace;
					fontFace2.AddRef();
					fontFace = fontFace2;
				}
				else
				{
					fontFace = this.LookupFontFaceSlow();
				}
			}
			Interlocked.Decrement(ref Font._mutex);
			if (null == fontFace)
			{
				fontFace = this.AddFontFaceToCache();
			}
			return fontFace;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0001179C File Offset: 0x00010B9C
		[SecuritySafeCritical]
		[return: MarshalAs(UnmanagedType.U1)]
		internal unsafe bool GetInformationalStrings(InformationalStringID informationalStringID, out LocalizedStrings informationalStrings)
		{
			int num = 0;
			IDWriteFont* value = this._font.Value;
			IDWriteLocalizedStrings* localizedStrings;
			int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.DWRITE_INFORMATIONAL_STRING_ID,MS.Internal.Text.TextInterface.Native.IDWriteLocalizedStrings**,System.Int32*), value, DWriteTypeConverter.Convert(informationalStringID), ref localizedStrings, ref num, *(*(long*)value + 72L));
			GC.KeepAlive(this._font);
			Util.ConvertHresultToException(hr);
			informationalStrings = new LocalizedStrings(localizedStrings);
			GC.KeepAlive(this);
			return ((num != 0) ? 1 : 0) != 0;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0001066C File Offset: 0x0000FA6C
		[SecuritySafeCritical]
		[return: MarshalAs(UnmanagedType.U1)]
		internal unsafe bool HasCharacter(uint unicodeValue)
		{
			int num = 0;
			IDWriteFont* value = this._font.Value;
			int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32,System.Int32*), value, unicodeValue, ref num, *(*(long*)value + 96L));
			GC.KeepAlive(this._font);
			Util.ConvertHresultToException(hr);
			GC.KeepAlive(this);
			return ((num != 0) ? 1 : 0) != 0;
		}

		// Token: 0x0400031A RID: 794
		[SecurityCritical]
		private NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteFont> _font;

		// Token: 0x0400031B RID: 795
		private double _version;

		// Token: 0x0400031C RID: 796
		private FontMetrics _fontMetrics;

		// Token: 0x0400031D RID: 797
		private int _flags;

		// Token: 0x0400031E RID: 798
		private static int _mutex;

		// Token: 0x0400031F RID: 799
		private static int _fontFaceCacheSize = 4;

		// Token: 0x04000320 RID: 800
		private static Font.FontFaceCacheEntry[] _fontFaceCache;

		// Token: 0x04000321 RID: 801
		private static int _fontFaceCacheMRU;

		// Token: 0x02000010 RID: 16
		private struct FontFaceCacheEntry
		{
			// Token: 0x04000322 RID: 802
			public Font font;

			// Token: 0x04000323 RID: 803
			public FontFace fontFace;
		}
	}
}
