using System;
using System.Security;
using MS.Internal;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Define um codificador que é usado para codificar imagens em formato GIF (Graphics Interchange Format).</summary>
	// Token: 0x020005ED RID: 1517
	public sealed class GifBitmapEncoder : BitmapEncoder
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Imaging.GifBitmapEncoder" />.</summary>
		// Token: 0x0600459C RID: 17820 RVA: 0x0010F7B0 File Offset: 0x0010EBB0
		[SecurityCritical]
		public GifBitmapEncoder() : base(true)
		{
			this._supportsPreview = false;
			this._supportsGlobalThumbnail = false;
			this._supportsGlobalMetadata = false;
			this._supportsFrameThumbnails = false;
			this._supportsMultipleFrames = true;
			this._supportsFrameMetadata = false;
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x0600459D RID: 17821 RVA: 0x0010F7FC File Offset: 0x0010EBFC
		internal override Guid ContainerFormat
		{
			[SecurityCritical]
			get
			{
				return this._containerFormat;
			}
		}

		// Token: 0x0600459E RID: 17822 RVA: 0x0010F810 File Offset: 0x0010EC10
		[SecurityCritical]
		internal override void SetupFrame(SafeMILHandle frameEncodeHandle, SafeMILHandle encoderOptions)
		{
			HRESULT.Check(UnsafeNativeMethods.WICBitmapFrameEncode.Initialize(frameEncodeHandle, encoderOptions));
		}

		// Token: 0x0600459F RID: 17823 RVA: 0x0010F82C File Offset: 0x0010EC2C
		internal override void SealObject()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001946 RID: 6470
		[SecurityCritical]
		private Guid _containerFormat = MILGuidData.GUID_ContainerFormatGif;
	}
}
