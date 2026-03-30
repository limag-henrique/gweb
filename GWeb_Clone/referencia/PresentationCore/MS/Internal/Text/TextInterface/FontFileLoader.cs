using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using MS.Internal.Text.TextInterface.Interfaces;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x0200001E RID: 30
	[SecurityCritical(SecurityCriticalScope.Everything)]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	internal class FontFileLoader : IDWriteFontFileLoaderMirror
	{
		// Token: 0x060002E2 RID: 738 RVA: 0x0000BD08 File Offset: 0x0000B108
		public FontFileLoader(IFontSourceFactory fontSourceFactory)
		{
			this._fontSourceFactory = fontSourceFactory;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000B870 File Offset: 0x0000AC70
		public FontFileLoader()
		{
			Debug.Assert(false);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000FC18 File Offset: 0x0000F018
		[ComVisible(true)]
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public unsafe virtual int CreateStreamFromKey(void* fontFileReferenceKey, uint fontFileReferenceKeySize, IntPtr* fontFileStream)
		{
			uint num = fontFileReferenceKeySize >> 1;
			if (fontFileStream != null && (fontFileReferenceKeySize & 1U) == 0U && num > 1U && ((num - 1U) * 2L)[fontFileReferenceKey / 2] == 0)
			{
				*fontFileStream = IntPtr.Zero;
				string a_ = new string((char*)fontFileReferenceKey);
				int result = 0;
				try
				{
					IntPtr comInterfaceForObject = Marshal.GetComInterfaceForObject(new FontFileStream(this._fontSourceFactory.Create(a_)), typeof(IDWriteFontFileStreamMirror));
					*fontFileStream = comInterfaceForObject;
				}
				catch (Exception e)
				{
					result = Marshal.GetHRForException(e);
				}
				return result;
			}
			return -2147024809;
		}

		// Token: 0x0400032C RID: 812
		private IFontSourceFactory _fontSourceFactory;
	}
}
