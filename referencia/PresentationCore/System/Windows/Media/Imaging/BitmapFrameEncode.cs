using System;
using System.Collections.ObjectModel;
using System.Security;

namespace System.Windows.Media.Imaging
{
	// Token: 0x020005DB RID: 1499
	internal sealed class BitmapFrameEncode : BitmapFrame
	{
		// Token: 0x060043F2 RID: 17394 RVA: 0x00108B44 File Offset: 0x00107F44
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal BitmapFrameEncode(BitmapSource source, BitmapSource thumbnail, BitmapMetadata metadata, ReadOnlyCollection<ColorContext> colorContexts) : base(true)
		{
			this._bitmapInit.BeginInit();
			this._source = source;
			base.WicSourceHandle = this._source.WicSourceHandle;
			base.IsSourceCached = this._source.IsSourceCached;
			this._isColorCorrected = this._source._isColorCorrected;
			this._thumbnail = thumbnail;
			this._readOnlycolorContexts = colorContexts;
			this.InternalMetadata = metadata;
			this._syncObject = source.SyncObject;
			this._bitmapInit.EndInit();
			this.FinalizeCreation();
		}

		// Token: 0x060043F3 RID: 17395 RVA: 0x00108BD0 File Offset: 0x00107FD0
		private BitmapFrameEncode() : base(true)
		{
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x060043F4 RID: 17396 RVA: 0x00108BE4 File Offset: 0x00107FE4
		// (set) Token: 0x060043F5 RID: 17397 RVA: 0x00108BF8 File Offset: 0x00107FF8
		public override Uri BaseUri
		{
			get
			{
				base.ReadPreamble();
				return null;
			}
			set
			{
				base.WritePreamble();
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x060043F6 RID: 17398 RVA: 0x00108C0C File Offset: 0x0010800C
		public override BitmapSource Thumbnail
		{
			get
			{
				base.ReadPreamble();
				return this._thumbnail;
			}
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x060043F7 RID: 17399 RVA: 0x00108C28 File Offset: 0x00108028
		public override ImageMetadata Metadata
		{
			get
			{
				base.ReadPreamble();
				return this.InternalMetadata;
			}
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x060043F8 RID: 17400 RVA: 0x00108C44 File Offset: 0x00108044
		public override BitmapDecoder Decoder
		{
			get
			{
				base.ReadPreamble();
				return null;
			}
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x060043F9 RID: 17401 RVA: 0x00108C58 File Offset: 0x00108058
		public override ReadOnlyCollection<ColorContext> ColorContexts
		{
			get
			{
				base.ReadPreamble();
				return this._readOnlycolorContexts;
			}
		}

		// Token: 0x060043FA RID: 17402 RVA: 0x00108C74 File Offset: 0x00108074
		public override InPlaceBitmapMetadataWriter CreateInPlaceBitmapMetadataWriter()
		{
			base.ReadPreamble();
			return null;
		}

		// Token: 0x060043FB RID: 17403 RVA: 0x00108C88 File Offset: 0x00108088
		protected override Freezable CreateInstanceCore()
		{
			return new BitmapFrameEncode();
		}

		// Token: 0x060043FC RID: 17404 RVA: 0x00108C9C File Offset: 0x0010809C
		private void CopyCommon(BitmapFrameEncode sourceBitmapFrameEncode)
		{
			this._bitmapInit.BeginInit();
			this._source = sourceBitmapFrameEncode._source;
			this._thumbnail = sourceBitmapFrameEncode._thumbnail;
			this._readOnlycolorContexts = sourceBitmapFrameEncode.ColorContexts;
			if (sourceBitmapFrameEncode.InternalMetadata != null)
			{
				this.InternalMetadata = sourceBitmapFrameEncode.InternalMetadata.Clone();
			}
			this._bitmapInit.EndInit();
		}

		// Token: 0x060043FD RID: 17405 RVA: 0x00108CFC File Offset: 0x001080FC
		protected override void CloneCore(Freezable sourceFreezable)
		{
			BitmapFrameEncode sourceBitmapFrameEncode = (BitmapFrameEncode)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(sourceBitmapFrameEncode);
		}

		// Token: 0x060043FE RID: 17406 RVA: 0x00108D20 File Offset: 0x00108120
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			BitmapFrameEncode sourceBitmapFrameEncode = (BitmapFrameEncode)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(sourceBitmapFrameEncode);
		}

		// Token: 0x060043FF RID: 17407 RVA: 0x00108D44 File Offset: 0x00108144
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			BitmapFrameEncode sourceBitmapFrameEncode = (BitmapFrameEncode)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			this.CopyCommon(sourceBitmapFrameEncode);
		}

		// Token: 0x06004400 RID: 17408 RVA: 0x00108D68 File Offset: 0x00108168
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			BitmapFrameEncode sourceBitmapFrameEncode = (BitmapFrameEncode)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			this.CopyCommon(sourceBitmapFrameEncode);
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x00108D8C File Offset: 0x0010818C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal override void FinalizeCreation()
		{
			base.CreationCompleted = true;
			this.UpdateCachedSettings();
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06004402 RID: 17410 RVA: 0x00108DA8 File Offset: 0x001081A8
		// (set) Token: 0x06004403 RID: 17411 RVA: 0x00108DC4 File Offset: 0x001081C4
		internal override BitmapMetadata InternalMetadata
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				base.CheckIfSiteOfOrigin();
				return this._metadata;
			}
			[SecurityCritical]
			[SecurityTreatAsSafe]
			set
			{
				base.CheckIfSiteOfOrigin();
				this._metadata = value;
			}
		}

		// Token: 0x040018C9 RID: 6345
		private BitmapSource _source;
	}
}
