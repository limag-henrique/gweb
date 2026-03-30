using System;
using System.IO;
using System.Security;
using Microsoft.Win32.SafeHandles;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	// Token: 0x02000600 RID: 1536
	internal sealed class UnknownBitmapDecoder : BitmapDecoder
	{
		// Token: 0x0600465B RID: 18011 RVA: 0x00113514 File Offset: 0x00112914
		private UnknownBitmapDecoder()
		{
		}

		// Token: 0x0600465C RID: 18012 RVA: 0x00113534 File Offset: 0x00112934
		[SecurityCritical]
		internal UnknownBitmapDecoder(SafeMILHandle decoderHandle, BitmapDecoder decoder, Uri baseUri, Uri uri, Stream stream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption, bool insertInDecoderCache, bool originalWritable, Stream uriStream, UnmanagedMemoryStream unmanagedMemoryStream, SafeFileHandle safeFilehandle) : base(decoderHandle, decoder, baseUri, uri, stream, createOptions, cacheOption, insertInDecoderCache, originalWritable, uriStream, unmanagedMemoryStream, safeFilehandle)
		{
		}

		// Token: 0x0600465D RID: 18013 RVA: 0x00113568 File Offset: 0x00112968
		internal override void SealObject()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040019A5 RID: 6565
		private UnknownBitmapDecoder.CoInitSafeHandle _safeHandle = new UnknownBitmapDecoder.CoInitSafeHandle();

		// Token: 0x020008DB RID: 2267
		private class CoInitSafeHandle : SafeMILHandle
		{
			// Token: 0x06005907 RID: 22791 RVA: 0x001694BC File Offset: 0x001688BC
			[SecurityTreatAsSafe]
			[SecurityCritical]
			public CoInitSafeHandle()
			{
				UnsafeNativeMethods.WICCodec.CoInitialize(IntPtr.Zero);
			}

			// Token: 0x06005908 RID: 22792 RVA: 0x001694DC File Offset: 0x001688DC
			[SecurityTreatAsSafe]
			[SecurityCritical]
			protected override bool ReleaseHandle()
			{
				UnsafeNativeMethods.WICCodec.CoUninitialize();
				return true;
			}
		}
	}
}
