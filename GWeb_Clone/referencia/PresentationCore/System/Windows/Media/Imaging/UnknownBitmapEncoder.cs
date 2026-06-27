using System;
using System.Security;
using MS.Internal;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	// Token: 0x02000601 RID: 1537
	internal sealed class UnknownBitmapEncoder : BitmapEncoder
	{
		// Token: 0x0600465E RID: 18014 RVA: 0x0011357C File Offset: 0x0011297C
		[SecurityCritical]
		public UnknownBitmapEncoder(Guid containerFormat) : base(true)
		{
			this._containerFormat = containerFormat;
			this._supportsPreview = true;
			this._supportsGlobalThumbnail = true;
			this._supportsGlobalMetadata = false;
			this._supportsFrameThumbnails = true;
			this._supportsMultipleFrames = true;
			this._supportsFrameMetadata = true;
		}

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x0600465F RID: 18015 RVA: 0x001135C4 File Offset: 0x001129C4
		internal override Guid ContainerFormat
		{
			[SecurityCritical]
			get
			{
				return this._containerFormat;
			}
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x001135D8 File Offset: 0x001129D8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal override void SetupFrame(SafeMILHandle frameEncodeHandle, SafeMILHandle encoderOptions)
		{
			HRESULT.Check(UnsafeNativeMethods.WICBitmapFrameEncode.Initialize(frameEncodeHandle, encoderOptions));
		}

		// Token: 0x06004661 RID: 18017 RVA: 0x001135F4 File Offset: 0x001129F4
		internal override void SealObject()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040019A6 RID: 6566
		[SecurityCritical]
		private Guid _containerFormat;
	}
}
