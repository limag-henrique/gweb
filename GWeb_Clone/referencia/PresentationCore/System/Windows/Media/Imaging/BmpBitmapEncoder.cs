using System;
using System.Security;
using MS.Internal;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Define um codificador usado para codificar imagens do formato bitmap (BMP).</summary>
	// Token: 0x020005D8 RID: 1496
	public sealed class BmpBitmapEncoder : BitmapEncoder
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.BmpBitmapEncoder" />.</summary>
		// Token: 0x060043B8 RID: 17336 RVA: 0x00107D5C File Offset: 0x0010715C
		[SecurityCritical]
		public BmpBitmapEncoder() : base(true)
		{
			this._supportsPreview = false;
			this._supportsGlobalThumbnail = false;
			this._supportsGlobalMetadata = false;
			this._supportsFrameThumbnails = false;
			this._supportsMultipleFrames = false;
			this._supportsFrameMetadata = false;
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x060043B9 RID: 17337 RVA: 0x00107DA8 File Offset: 0x001071A8
		internal override Guid ContainerFormat
		{
			[SecurityCritical]
			get
			{
				return this._containerFormat;
			}
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x00107DBC File Offset: 0x001071BC
		[SecurityCritical]
		internal override void SetupFrame(SafeMILHandle frameEncodeHandle, SafeMILHandle encoderOptions)
		{
			HRESULT.Check(UnsafeNativeMethods.WICBitmapFrameEncode.Initialize(frameEncodeHandle, encoderOptions));
		}

		// Token: 0x060043BB RID: 17339 RVA: 0x00107DD8 File Offset: 0x001071D8
		internal override void SealObject()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040018BC RID: 6332
		[SecurityCritical]
		private Guid _containerFormat = MILGuidData.GUID_ContainerFormatBmp;
	}
}
