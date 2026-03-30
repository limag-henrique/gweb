using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using MS.Internal.Text.TextInterface.Interfaces;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x0200001F RID: 31
	[ComVisible(true)]
	[SecurityCritical(SecurityCriticalScope.Everything)]
	[ClassInterface(ClassInterfaceType.None)]
	internal class FontFileEnumerator : IDWriteFontFileEnumeratorMirror
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x0000BC84 File Offset: 0x0000B084
		public unsafe FontFileEnumerator(IEnumerable<IFontSource> fontSourceCollection, FontFileLoader fontFileLoader, IDWriteFactory* factory)
		{
			this._fontSourceCollectionEnumerator = fontSourceCollection.GetEnumerator();
			this._fontFileLoader = fontFileLoader;
			object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), factory, *(*(long*)factory + 8L));
			this._factory = factory;
			GC.KeepAlive(this);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000B870 File Offset: 0x0000AC70
		public FontFileEnumerator()
		{
			Debug.Assert(false);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000BCC4 File Offset: 0x0000B0C4
		[SecurityCritical]
		[ComVisible(true)]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public virtual int MoveNext(ref bool hasCurrentFile)
		{
			int result = 0;
			try
			{
				hasCurrentFile = this._fontSourceCollectionEnumerator.MoveNext();
			}
			catch (Exception e)
			{
				result = Marshal.GetHRForException(e);
			}
			return result;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000FBE0 File Offset: 0x0000EFE0
		[ComVisible(true)]
		public unsafe virtual int GetCurrentFontFile(IDWriteFontFile** fontFile)
		{
			if (fontFile == null)
			{
				return -2147024809;
			}
			return Factory.CreateFontFile(this._factory, this._fontFileLoader, this._fontSourceCollectionEnumerator.Current.Uri, fontFile);
		}

		// Token: 0x0400032D RID: 813
		private IEnumerator<IFontSource> _fontSourceCollectionEnumerator;

		// Token: 0x0400032E RID: 814
		private FontFileLoader _fontFileLoader;

		// Token: 0x0400032F RID: 815
		private unsafe IDWriteFactory* _factory;
	}
}
