using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using MS.Internal.Text.TextInterface.Generics;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x0200000B RID: 11
	internal sealed class FontFile : IDisposable
	{
		// Token: 0x06000258 RID: 600 RVA: 0x00010A94 File Offset: 0x0000FE94
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		static unsafe FontFile()
		{
			Guid guid = new Guid("b2d9f3ec-c9fe-4a11-a2ec-d86208f7c0a2");
			Guid guid2 = guid;
			_GUID* ptr = (_GUID*)<Module>.@new(16UL);
			_GUID* ptr2;
			if (ptr != null)
			{
				initblk(ptr, 0, 16L);
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
			_GUID guid3 = Util.ToGUID(ref guid2);
			cpblk(ptr2, ref guid3, 16);
			FontFile._guidForIDWriteLocalFontFileLoader = new NativePointerWrapper<_GUID>(ptr2);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000BBFC File Offset: 0x0000AFFC
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		private unsafe static void ReleaseInterface(IDWriteLocalFontFileLoader** ppInterface)
		{
			if (ppInterface != null)
			{
				ulong num = (ulong)(*(long*)ppInterface);
				if (num != 0UL)
				{
					ulong num2 = num;
					object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), num2, *(*num2 + 16L));
					*(long*)ppInterface = 0L;
				}
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00010AEC File Offset: 0x0000FEEC
		[SecurityCritical]
		internal unsafe FontFile(IDWriteFontFile* fontFile)
		{
			this._fontFile = new NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteFontFile>((IUnknown*)fontFile);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000FA84 File Offset: 0x0000EE84
		internal unsafe IDWriteFontFile* DWriteFontFileNoAddRef
		{
			[SecurityCritical]
			get
			{
				return this._fontFile.Value;
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000FA1C File Offset: 0x0000EE1C
		[SecuritySafeCritical]
		[return: MarshalAs(UnmanagedType.U1)]
		internal unsafe bool Analyze(out DWRITE_FONT_FILE_TYPE dwriteFontFileType, out DWRITE_FONT_FACE_TYPE dwriteFontFaceType, out uint numberOfFaces, int* hr)
		{
			int num = 0;
			uint num2 = 0U;
			IDWriteFontFile* value = this._fontFile.Value;
			DWRITE_FONT_FILE_TYPE dwrite_FONT_FILE_TYPE;
			DWRITE_FONT_FACE_TYPE dwrite_FONT_FACE_TYPE;
			*hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32*,MS.Internal.Text.TextInterface.Native.DWRITE_FONT_FILE_TYPE*,MS.Internal.Text.TextInterface.Native.DWRITE_FONT_FACE_TYPE*,System.UInt32*), value, ref num, ref dwrite_FONT_FILE_TYPE, ref dwrite_FONT_FACE_TYPE, ref num2, *(*(long*)value + 40L));
			GC.KeepAlive(this._fontFile);
			if (*hr < 0)
			{
				GC.KeepAlive(this);
				return false;
			}
			dwriteFontFileType = dwrite_FONT_FILE_TYPE;
			dwriteFontFaceType = dwrite_FONT_FACE_TYPE;
			numberOfFaces = num2;
			GC.KeepAlive(this);
			return ((num != 0) ? 1 : 0) != 0;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00010B0C File Offset: 0x0000FF0C
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal unsafe string GetUriPath()
		{
			IDWriteFontFileLoader* ptr = null;
			IDWriteFontFile* value = this._fontFile.Value;
			Util.ConvertHresultToException(calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.IDWriteFontFileLoader**), value, ref ptr, *(*(long*)value + 32L)));
			void* ptr2 = null;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr, FontFile._guidForIDWriteLocalFontFileLoader.Value, ref ptr2, *(*(long*)ptr));
			void* ptr3;
			uint num2;
			if (num == -2147467262)
			{
				IDWriteFontFile* value2 = this._fontFile.Value;
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Void modopt(System.Runtime.CompilerServices.IsConst)**,System.UInt32*), value2, ref ptr3, ref num2, *(*(long*)value2 + 24L));
				GC.KeepAlive(this._fontFile);
				Util.ConvertHresultToException(num);
				GC.KeepAlive(this);
				return new string((char*)ptr3);
			}
			IDWriteLocalFontFileLoader* ptr4 = (IDWriteLocalFontFileLoader*)ptr2;
			ushort* ptr5 = null;
			string result;
			try
			{
				IDWriteFontFile* value3 = this._fontFile.Value;
				int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Void modopt(System.Runtime.CompilerServices.IsConst)**,System.UInt32*), value3, ref ptr3, ref num2, *(*(long*)value3 + 24L));
				GC.KeepAlive(this._fontFile);
				Util.ConvertHresultToException(hr);
				uint num3;
				Util.ConvertHresultToException(calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Void modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,System.UInt32*), ptr4, ptr3, num2, ref num3, *(*(long*)ptr4 + 32L)));
				byte condition = (num3 < uint.MaxValue) ? 1 : 0;
				Invariant.Assert(condition != 0);
				ushort* ptr6 = (ushort*)<Module>.@new((ulong)(num3 + 1U) * 2UL);
				ptr5 = ptr6;
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Void modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,System.UInt16*,System.UInt32), ptr4, ptr3, num2, ptr6, num3 + 1U, *(*(long*)ptr4 + 40L));
				Util.ConvertHresultToException(num);
				GC.KeepAlive(this);
				result = new string((char*)ptr6);
			}
			finally
			{
				FontFile.ReleaseInterface(&ptr4);
				if (ptr5 != null)
				{
					<Module>.delete((void*)ptr5);
				}
			}
			return result;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000BBD8 File Offset: 0x0000AFD8
		[SecuritySafeCritical]
		private void ~FontFile()
		{
			NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteFontFile> fontFile = this._fontFile;
			if (fontFile != null)
			{
				((IDisposable)fontFile).Dispose();
				this._fontFile = null;
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000F3B4 File Offset: 0x0000E7B4
		protected void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.~FontFile();
			}
			else
			{
				base.Finalize();
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000104B8 File Offset: 0x0000F8B8
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000310 RID: 784
		[SecurityCritical]
		private NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteFontFile> _fontFile;

		// Token: 0x04000311 RID: 785
		[SecurityCritical]
		private static NativePointerWrapper<_GUID> _guidForIDWriteLocalFontFileLoader;
	}
}
