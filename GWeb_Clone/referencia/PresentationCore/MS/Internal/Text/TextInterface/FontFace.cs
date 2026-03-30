using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using MS.Internal.Text.TextInterface.Generics;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x0200000C RID: 12
	internal sealed class FontFace : IDisposable
	{
		// Token: 0x06000261 RID: 609 RVA: 0x00010814 File Offset: 0x0000FC14
		[SecurityCritical]
		internal unsafe FontFace(IDWriteFontFace* fontFace)
		{
			this._fontFace = new NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteFontFace>((IUnknown*)fontFace);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000F84C File Offset: 0x0000EC4C
		internal unsafe IDWriteFontFace* DWriteFontFaceNoAddRef
		{
			[SecurityCritical]
			get
			{
				return this._fontFace.Value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000F864 File Offset: 0x0000EC64
		internal unsafe IntPtr DWriteFontFaceAddRef
		{
			[SecurityCritical]
			get
			{
				IDWriteFontFace* value = this._fontFace.Value;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), value, *(*(long*)value + 8L));
				GC.KeepAlive(this);
				return (IntPtr)((void*)this._fontFace.Value);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000F8A0 File Offset: 0x0000ECA0
		internal unsafe FontFaceType Type
		{
			[SecuritySafeCritical]
			get
			{
				IDWriteFontFace* value = this._fontFace.Value;
				DWRITE_FONT_FACE_TYPE fontFaceType = calli(MS.Internal.Text.TextInterface.Native.DWRITE_FONT_FACE_TYPE modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), value, *(*(long*)value + 24L));
				GC.KeepAlive(this._fontFace);
				GC.KeepAlive(this);
				return DWriteTypeConverter.Convert(fontFaceType);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000F8DC File Offset: 0x0000ECDC
		internal unsafe uint Index
		{
			[SecuritySafeCritical]
			get
			{
				IDWriteFontFace* value = this._fontFace.Value;
				uint result = calli(System.UInt32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), value, *(*(long*)value + 40L));
				GC.KeepAlive(this._fontFace);
				GC.KeepAlive(this);
				return result;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000F914 File Offset: 0x0000ED14
		internal unsafe FontSimulations SimulationFlags
		{
			[SecuritySafeCritical]
			get
			{
				IDWriteFontFace* value = this._fontFace.Value;
				DWRITE_FONT_SIMULATIONS fontSimulations = calli(MS.Internal.Text.TextInterface.Native.DWRITE_FONT_SIMULATIONS modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), value, *(*(long*)value + 48L));
				GC.KeepAlive(this._fontFace);
				GC.KeepAlive(this);
				return DWriteTypeConverter.Convert(fontSimulations);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000F950 File Offset: 0x0000ED50
		internal unsafe bool IsSymbolFont
		{
			[SecuritySafeCritical]
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				IDWriteFontFace* value = this._fontFace.Value;
				int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), value, *(*(long*)value + 56L));
				GC.KeepAlive(this._fontFace);
				GC.KeepAlive(this);
				return ((num != 0) ? 1 : 0) != 0;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000F990 File Offset: 0x0000ED90
		internal unsafe FontMetrics Metrics
		{
			[SecuritySafeCritical]
			get
			{
				if (this._fontMetrics == null)
				{
					IDWriteFontFace* value = this._fontFace.Value;
					DWRITE_FONT_METRICS dwriteFontMetrics;
					calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.DWRITE_FONT_METRICS*), value, ref dwriteFontMetrics, *(*(long*)value + 64L));
					GC.KeepAlive(this._fontFace);
					this._fontMetrics = DWriteTypeConverter.Convert(dwriteFontMetrics);
				}
				GC.KeepAlive(this);
				return this._fontMetrics;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000F9E4 File Offset: 0x0000EDE4
		internal unsafe ushort GlyphCount
		{
			[SecuritySafeCritical]
			get
			{
				IDWriteFontFace* value = this._fontFace.Value;
				ushort result = calli(System.UInt16 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), value, *(*(long*)value + 72L));
				GC.KeepAlive(this._fontFace);
				GC.KeepAlive(this);
				return result;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000118C8 File Offset: 0x00010CC8
		[SecuritySafeCritical]
		internal unsafe FontFile GetFileZero()
		{
			uint num = 0U;
			IDWriteFontFile* fontFile = null;
			IDWriteFontFace* value = this._fontFace.Value;
			int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32*,MS.Internal.Text.TextInterface.Native.IDWriteFontFile**), value, ref num, 0L, *(*(long*)value + 32L));
			Util.ConvertHresultToException(hr);
			if (num > 0U)
			{
				IDWriteFontFile** ptr = (IDWriteFontFile**)<Module>.@new((ulong)num * 8UL);
				try
				{
					IDWriteFontFace* value2 = this._fontFace.Value;
					hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32*,MS.Internal.Text.TextInterface.Native.IDWriteFontFile**), value2, ref num, ptr, *(*(long*)value2 + 32L));
					Util.ConvertHresultToException(hr);
					fontFile = *(long*)ptr;
					for (uint num2 = 1U; num2 < num; num2 += 1U)
					{
						IDWriteFontFile** ptr2 = num2 * 8L + ptr / sizeof(IDWriteFontFile*);
						long num3 = *(long*)ptr2;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), num3, *(*num3 + 16L));
						*(long*)ptr2 = 0L;
					}
				}
				finally
				{
					<Module>.delete((void*)ptr);
				}
			}
			GC.KeepAlive(this._fontFace);
			GC.KeepAlive(this);
			return (num <= 0U) ? null : new FontFile(fontFile);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000B694 File Offset: 0x0000AA94
		internal void AddRef()
		{
			Interlocked.Increment(ref this._refCount);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000B6B0 File Offset: 0x0000AAB0
		internal void Release()
		{
			if (-1 == Interlocked.Decrement(ref this._refCount) && this != null)
			{
				((IDisposable)this).Dispose();
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00010834 File Offset: 0x0000FC34
		[SecurityCritical]
		internal unsafe void GetDesignGlyphMetrics(ushort* pGlyphIndices, uint glyphCount, GlyphMetrics* pGlyphMetrics)
		{
			IDWriteFontFace* value = this._fontFace.Value;
			int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,MS.Internal.Text.TextInterface.Native.DWRITE_GLYPH_METRICS*,System.Int32), value, pGlyphIndices, glyphCount, pGlyphMetrics, 0, *(*(long*)value + 80L));
			GC.KeepAlive(this._fontFace);
			Util.ConvertHresultToException(hr);
			GC.KeepAlive(this);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00010878 File Offset: 0x0000FC78
		[SecurityCritical]
		internal unsafe void GetDisplayGlyphMetrics(ushort* pGlyphIndices, uint glyphCount, GlyphMetrics* pGlyphMetrics, float emSize, [MarshalAs(UnmanagedType.U1)] bool useDisplayNatural, [MarshalAs(UnmanagedType.U1)] bool isSideways, float pixelsPerDip)
		{
			IDWriteFontFace* value = this._fontFace.Value;
			int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Single,System.Single,MS.Internal.Text.TextInterface.Native.DWRITE_MATRIX modopt(System.Runtime.CompilerServices.IsConst)*,System.Int32,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,MS.Internal.Text.TextInterface.Native.DWRITE_GLYPH_METRICS*,System.Int32), value, emSize, pixelsPerDip, 0L, useDisplayNatural, pGlyphIndices, glyphCount, pGlyphMetrics, isSideways, *(*(long*)value + 136L));
			GC.KeepAlive(this._fontFace);
			Util.ConvertHresultToException(hr);
			GC.KeepAlive(this);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000108C8 File Offset: 0x0000FCC8
		[SecurityCritical]
		internal unsafe void GetArrayOfGlyphIndices(uint* pCodePoints, uint glyphCount, ushort* pGlyphIndices)
		{
			ref uint uint32_u0020modopt(IsConst)& = pCodePoints;
			ref ushort uint16& = pGlyphIndices;
			IDWriteFontFace* value = this._fontFace.Value;
			int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,System.UInt16*), value, ref uint32_u0020modopt(IsConst)&, glyphCount, ref uint16&, *(*(long*)value + 88L));
			GC.KeepAlive(this._fontFace);
			Util.ConvertHresultToException(hr);
			GC.KeepAlive(this);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0001090C File Offset: 0x0000FD0C
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.U1)]
		internal unsafe bool TryGetFontTable(OpenTypeTableTag openTypeTableTag, out byte[] tableData)
		{
			uint num = 0U;
			int num2 = 0;
			tableData = null;
			IDWriteFontFace* value = this._fontFace.Value;
			void* ptr;
			void* ptr2;
			Util.ConvertHresultToException(calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32,System.Void modopt(System.Runtime.CompilerServices.IsConst)**,System.UInt32*,System.Void**,System.Int32*), value, openTypeTableTag, ref ptr, ref num, ref ptr2, ref num2, *(*(long*)value + 96L)));
			if (num2 != 0)
			{
				tableData = new byte[num];
				uint num3 = 0U;
				if (0U < num)
				{
					do
					{
						tableData[(int)num3] = num3[ptr];
						num3 += 1U;
					}
					while (num3 < num);
				}
				IDWriteFontFace* value2 = this._fontFace.Value;
				calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Void*), value2, ptr2, *(*(long*)value2 + 104L));
			}
			GC.KeepAlive(this._fontFace);
			GC.KeepAlive(this);
			return ((num2 != 0) ? 1 : 0) != 0;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000109A8 File Offset: 0x0000FDA8
		[SecuritySafeCritical]
		[return: MarshalAs(UnmanagedType.U1)]
		internal unsafe bool ReadFontEmbeddingRights(out ushort fsType)
		{
			uint num = 0U;
			int num2 = 0;
			fsType = 0;
			IDWriteFontFace* value = this._fontFace.Value;
			void* ptr;
			void* ptr2;
			Util.ConvertHresultToException(calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32,System.Void modopt(System.Runtime.CompilerServices.IsConst)**,System.UInt32*,System.Void**,System.Int32*), value, 841962319, ref ptr, ref num, ref ptr2, ref num2, *(*(long*)value + 96L)));
			bool result = false;
			if (num2 != 0)
			{
				if (num >= 9U)
				{
					byte* ptr3 = (byte*)ptr + 8L;
					fsType = (ushort)(*ptr3) * 256 + (ushort)ptr3[1L];
					result = true;
				}
				IDWriteFontFace* value2 = this._fontFace.Value;
				calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Void*), value2, ptr2, *(*(long*)value2 + 104L));
			}
			GC.KeepAlive(this._fontFace);
			GC.KeepAlive(this);
			return result;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000BBA4 File Offset: 0x0000AFA4
		[SecuritySafeCritical]
		private void ~FontFace()
		{
			NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteFontFace> fontFace = this._fontFace;
			if (fontFace != null)
			{
				((IDisposable)fontFace).Dispose();
				this._fontFace = null;
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000F3D4 File Offset: 0x0000E7D4
		protected void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.~FontFace();
			}
			else
			{
				base.Finalize();
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000104D4 File Offset: 0x0000F8D4
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000312 RID: 786
		[SecurityCritical]
		private NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteFontFace> _fontFace;

		// Token: 0x04000313 RID: 787
		private FontMetrics _fontMetrics;

		// Token: 0x04000314 RID: 788
		private int _refCount;
	}
}
