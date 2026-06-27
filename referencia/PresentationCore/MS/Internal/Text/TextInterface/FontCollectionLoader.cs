using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using MS.Internal.Text.TextInterface.Interfaces;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x02000020 RID: 32
	[ComVisible(true)]
	[SecurityCritical(SecurityCriticalScope.Everything)]
	[ClassInterface(ClassInterfaceType.None)]
	internal class FontCollectionLoader : IDWriteFontCollectionLoaderMirror
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x0000BC60 File Offset: 0x0000B060
		public FontCollectionLoader(IFontSourceCollectionFactory fontSourceCollectionFactory, FontFileLoader fontFileLoader)
		{
			this._fontSourceCollectionFactory = fontSourceCollectionFactory;
			this._fontFileLoader = fontFileLoader;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000B870 File Offset: 0x0000AC70
		public FontCollectionLoader()
		{
			Debug.Assert(false);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000FB3C File Offset: 0x0000EF3C
		[SecurityCritical]
		[ComVisible(true)]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public unsafe virtual int CreateEnumeratorFromKey(IntPtr factory, void* collectionKey, uint collectionKeySize, IntPtr* fontFileEnumerator)
		{
			uint num = collectionKeySize >> 1;
			if (fontFileEnumerator != null && (collectionKeySize & 1U) == 0U && num > 1U && ((num - 1U) * 2L)[collectionKey / 2] == 0)
			{
				*fontFileEnumerator = IntPtr.Zero;
				string a_ = new string((char*)collectionKey);
				int result = 0;
				try
				{
					IntPtr comInterfaceForObject = Marshal.GetComInterfaceForObject(new FontFileEnumerator(this._fontSourceCollectionFactory.Create(a_), this._fontFileLoader, (IDWriteFactory*)factory.ToPointer()), typeof(IDWriteFontFileEnumeratorMirror));
					*fontFileEnumerator = comInterfaceForObject;
				}
				catch (Exception e)
				{
					result = Marshal.GetHRForException(e);
				}
				return result;
			}
			return -2147024809;
		}

		// Token: 0x04000330 RID: 816
		private IFontSourceCollectionFactory _fontSourceCollectionFactory;

		// Token: 0x04000331 RID: 817
		private FontFileLoader _fontFileLoader;
	}
}
