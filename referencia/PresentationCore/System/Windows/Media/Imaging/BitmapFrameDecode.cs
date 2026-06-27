using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security;
using MS.Internal;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	// Token: 0x020005DA RID: 1498
	internal sealed class BitmapFrameDecode : BitmapFrame
	{
		// Token: 0x060043D0 RID: 17360 RVA: 0x0010807C File Offset: 0x0010747C
		internal BitmapFrameDecode(int frameNumber, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption, BitmapDecoder decoder) : base(true)
		{
			this._bitmapInit.BeginInit();
			this._frameNumber = frameNumber;
			this._isThumbnailCached = false;
			this._isMetadataCached = false;
			this._frameSource = null;
			this._decoder = decoder;
			this._syncObject = decoder.SyncObject;
			this._createOptions = createOptions;
			this._cacheOption = cacheOption;
			this._bitmapInit.EndInit();
			if ((createOptions & BitmapCreateOptions.DelayCreation) != BitmapCreateOptions.None)
			{
				base.DelayCreation = true;
				return;
			}
			this.FinalizeCreation();
		}

		// Token: 0x060043D1 RID: 17361 RVA: 0x001080F8 File Offset: 0x001074F8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal BitmapFrameDecode(int frameNumber, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption, BitmapFrameDecode frameDecode) : base(true)
		{
			this._bitmapInit.BeginInit();
			this._frameNumber = frameNumber;
			base.WicSourceHandle = frameDecode.WicSourceHandle;
			base.IsSourceCached = frameDecode.IsSourceCached;
			base.CreationCompleted = frameDecode.CreationCompleted;
			this._frameSource = frameDecode._frameSource;
			this._decoder = frameDecode.Decoder;
			this._syncObject = this._decoder.SyncObject;
			this._createOptions = createOptions;
			this._cacheOption = cacheOption;
			this._thumbnail = frameDecode._thumbnail;
			this._isThumbnailCached = frameDecode._isThumbnailCached;
			this._metadata = frameDecode._metadata;
			this._isMetadataCached = frameDecode._isMetadataCached;
			this._readOnlycolorContexts = frameDecode._readOnlycolorContexts;
			this._isColorContextCached = frameDecode._isColorContextCached;
			this._bitmapInit.EndInit();
			if ((createOptions & BitmapCreateOptions.DelayCreation) != BitmapCreateOptions.None)
			{
				base.DelayCreation = true;
				return;
			}
			if (!base.CreationCompleted)
			{
				this.FinalizeCreation();
				return;
			}
			this.UpdateCachedSettings();
		}

		// Token: 0x060043D2 RID: 17362 RVA: 0x001081FC File Offset: 0x001075FC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal BitmapFrameDecode(int frameNumber, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption, LateBoundBitmapDecoder decoder) : base(true)
		{
			this._bitmapInit.BeginInit();
			this._frameNumber = frameNumber;
			byte[] pixels = new byte[4];
			BitmapSource bitmapSource = BitmapSource.Create(1, 1, 96.0, 96.0, PixelFormats.Pbgra32, null, pixels, 4);
			base.WicSourceHandle = bitmapSource.WicSourceHandle;
			this._decoder = decoder;
			this._createOptions = createOptions;
			this._cacheOption = cacheOption;
			this._decoder.DownloadCompleted += this.OnDownloadCompleted;
			this._decoder.DownloadProgress += this.OnDownloadProgress;
			this._decoder.DownloadFailed += this.OnDownloadFailed;
			this._bitmapInit.EndInit();
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x001082C0 File Offset: 0x001076C0
		private BitmapFrameDecode() : base(true)
		{
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x060043D4 RID: 17364 RVA: 0x001082D4 File Offset: 0x001076D4
		// (set) Token: 0x060043D5 RID: 17365 RVA: 0x001082F4 File Offset: 0x001076F4
		public override Uri BaseUri
		{
			get
			{
				base.ReadPreamble();
				return this._decoder._baseUri;
			}
			set
			{
				base.WritePreamble();
			}
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x060043D6 RID: 17366 RVA: 0x00108308 File Offset: 0x00107708
		public override BitmapSource Thumbnail
		{
			get
			{
				base.ReadPreamble();
				this.EnsureThumbnail();
				return this._thumbnail;
			}
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x060043D7 RID: 17367 RVA: 0x00108328 File Offset: 0x00107728
		public override ImageMetadata Metadata
		{
			get
			{
				base.ReadPreamble();
				return this.InternalMetadata;
			}
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x060043D8 RID: 17368 RVA: 0x00108344 File Offset: 0x00107744
		public override BitmapDecoder Decoder
		{
			get
			{
				base.ReadPreamble();
				return this._decoder;
			}
		}

		// Token: 0x060043D9 RID: 17369 RVA: 0x00108360 File Offset: 0x00107760
		[SecurityCritical]
		private int GetColorContexts(ref uint numContexts, IntPtr[] colorContextPtrs)
		{
			Invariant.Assert(colorContextPtrs == null || (ulong)numContexts <= (ulong)((long)colorContextPtrs.Length));
			return UnsafeNativeMethods.WICBitmapFrameDecode.GetColorContexts(this._frameSource, numContexts, colorContextPtrs, out numContexts);
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x060043DA RID: 17370 RVA: 0x00108394 File Offset: 0x00107794
		public override ReadOnlyCollection<ColorContext> ColorContexts
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				base.ReadPreamble();
				if (!this._isColorContextCached && !this.IsDownloading)
				{
					this.EnsureSource();
					object syncObject = this._syncObject;
					lock (syncObject)
					{
						IList<ColorContext> colorContextsHelper = ColorContext.GetColorContextsHelper(new ColorContext.GetColorContextsDelegate(this.GetColorContexts));
						if (colorContextsHelper != null)
						{
							this._readOnlycolorContexts = new ReadOnlyCollection<ColorContext>(colorContextsHelper);
						}
						this._isColorContextCached = true;
					}
				}
				return this._readOnlycolorContexts;
			}
		}

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x060043DB RID: 17371 RVA: 0x00108424 File Offset: 0x00107824
		public override bool IsDownloading
		{
			get
			{
				base.ReadPreamble();
				return this.Decoder.IsDownloading;
			}
		}

		// Token: 0x060043DC RID: 17372 RVA: 0x00108444 File Offset: 0x00107844
		public override InPlaceBitmapMetadataWriter CreateInPlaceBitmapMetadataWriter()
		{
			base.ReadPreamble();
			if (this._decoder != null)
			{
				this._decoder.CheckOriginalWritable();
			}
			base.CheckIfSiteOfOrigin();
			this.EnsureSource();
			return InPlaceBitmapMetadataWriter.CreateFromFrameDecode(this._frameSource, this._syncObject);
		}

		// Token: 0x060043DD RID: 17373 RVA: 0x00108488 File Offset: 0x00107888
		internal override bool CanSerializeToString()
		{
			base.ReadPreamble();
			return this._decoder.CanConvertToString();
		}

		// Token: 0x060043DE RID: 17374 RVA: 0x001084A8 File Offset: 0x001078A8
		internal override string ConvertToString(string format, IFormatProvider provider)
		{
			base.ReadPreamble();
			if (this._decoder != null)
			{
				return this._decoder.ToString();
			}
			return base.ConvertToString(format, provider);
		}

		// Token: 0x060043DF RID: 17375 RVA: 0x001084D8 File Offset: 0x001078D8
		protected override Freezable CreateInstanceCore()
		{
			return new BitmapFrameDecode();
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x001084EC File Offset: 0x001078EC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void CopyCommon(BitmapFrameDecode sourceBitmapFrameDecode)
		{
			this._bitmapInit.BeginInit();
			this._frameNumber = sourceBitmapFrameDecode._frameNumber;
			this._isThumbnailCached = sourceBitmapFrameDecode._isThumbnailCached;
			this._isMetadataCached = sourceBitmapFrameDecode._isMetadataCached;
			this._isColorContextCached = sourceBitmapFrameDecode._isColorContextCached;
			this._frameSource = sourceBitmapFrameDecode._frameSource;
			this._thumbnail = sourceBitmapFrameDecode._thumbnail;
			this._metadata = sourceBitmapFrameDecode.InternalMetadata;
			this._readOnlycolorContexts = sourceBitmapFrameDecode._readOnlycolorContexts;
			this._decoder = sourceBitmapFrameDecode._decoder;
			if (this._decoder != null && this._decoder.IsDownloading)
			{
				this._weakBitmapFrameDecodeEventSink = new BitmapFrameDecode.WeakBitmapFrameDecodeEventSink(this, sourceBitmapFrameDecode);
			}
			this._syncObject = this._decoder.SyncObject;
			this._createOptions = sourceBitmapFrameDecode._createOptions;
			this._cacheOption = sourceBitmapFrameDecode._cacheOption;
			this._bitmapInit.EndInit();
		}

		// Token: 0x060043E1 RID: 17377 RVA: 0x001085C8 File Offset: 0x001079C8
		protected override void CloneCore(Freezable sourceFreezable)
		{
			BitmapFrameDecode sourceBitmapFrameDecode = (BitmapFrameDecode)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(sourceBitmapFrameDecode);
		}

		// Token: 0x060043E2 RID: 17378 RVA: 0x001085EC File Offset: 0x001079EC
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			BitmapFrameDecode sourceBitmapFrameDecode = (BitmapFrameDecode)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(sourceBitmapFrameDecode);
		}

		// Token: 0x060043E3 RID: 17379 RVA: 0x00108610 File Offset: 0x00107A10
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			BitmapFrameDecode sourceBitmapFrameDecode = (BitmapFrameDecode)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			this.CopyCommon(sourceBitmapFrameDecode);
		}

		// Token: 0x060043E4 RID: 17380 RVA: 0x00108634 File Offset: 0x00107A34
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			BitmapFrameDecode sourceBitmapFrameDecode = (BitmapFrameDecode)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			this.CopyCommon(sourceBitmapFrameDecode);
		}

		// Token: 0x060043E5 RID: 17381 RVA: 0x00108658 File Offset: 0x00107A58
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal void UpdateDecoder(BitmapDecoder decoder)
		{
			this._decoder = decoder;
			this._syncObject = decoder.SyncObject;
			base.WicSourceHandle = null;
			this._needsUpdate = true;
			this.FinalizeCreation();
			base.RegisterForAsyncUpdateResource();
		}

		// Token: 0x060043E6 RID: 17382 RVA: 0x00108694 File Offset: 0x00107A94
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal override void FinalizeCreation()
		{
			this.EnsureSource();
			base.WicSourceHandle = this._frameSource;
			this.UpdateCachedSettings();
			object syncObject = this._syncObject;
			lock (syncObject)
			{
				base.WicSourceHandle = BitmapSource.CreateCachedBitmap(this, this._frameSource, this._createOptions, this._cacheOption, this.Palette);
			}
			base.IsSourceCached = (this._cacheOption != BitmapCacheOption.None);
			base.CreationCompleted = true;
			this.UpdateCachedSettings();
			this.EnsureThumbnail();
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x060043E7 RID: 17383 RVA: 0x0010873C File Offset: 0x00107B3C
		internal override bool ShouldCloneEventDelegates
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060043E8 RID: 17384 RVA: 0x0010874C File Offset: 0x00107B4C
		private void OnDownloadCompleted(object sender, EventArgs e)
		{
			LateBoundBitmapDecoder lateBoundBitmapDecoder = (LateBoundBitmapDecoder)sender;
			lateBoundBitmapDecoder.DownloadCompleted -= this.OnDownloadCompleted;
			lateBoundBitmapDecoder.DownloadProgress -= this.OnDownloadProgress;
			lateBoundBitmapDecoder.DownloadFailed -= this.OnDownloadFailed;
			base.FireChanged();
			this._downloadEvent.InvokeEvents(this, null);
		}

		// Token: 0x060043E9 RID: 17385 RVA: 0x001087AC File Offset: 0x00107BAC
		private void OnDownloadProgress(object sender, DownloadProgressEventArgs e)
		{
			this._progressEvent.InvokeEvents(this, e);
		}

		// Token: 0x060043EA RID: 17386 RVA: 0x001087C8 File Offset: 0x00107BC8
		private void OnDownloadFailed(object sender, ExceptionEventArgs e)
		{
			LateBoundBitmapDecoder lateBoundBitmapDecoder = (LateBoundBitmapDecoder)sender;
			lateBoundBitmapDecoder.DownloadCompleted -= this.OnDownloadCompleted;
			lateBoundBitmapDecoder.DownloadProgress -= this.OnDownloadProgress;
			lateBoundBitmapDecoder.DownloadFailed -= this.OnDownloadFailed;
			this._failedEvent.InvokeEvents(this, e);
		}

		// Token: 0x060043EB RID: 17387 RVA: 0x00108820 File Offset: 0x00107C20
		private void OnOriginalDownloadCompleted(BitmapFrameDecode original, EventArgs e)
		{
			this.CleanUpWeakEventSink();
			this.UpdateDecoder(original.Decoder);
			base.FireChanged();
			this._downloadEvent.InvokeEvents(this, e);
		}

		// Token: 0x060043EC RID: 17388 RVA: 0x00108854 File Offset: 0x00107C54
		private void OnOriginalDownloadFailed(ExceptionEventArgs e)
		{
			this.CleanUpWeakEventSink();
			this._failedEvent.InvokeEvents(this, e);
		}

		// Token: 0x060043ED RID: 17389 RVA: 0x00108874 File Offset: 0x00107C74
		private void CleanUpWeakEventSink()
		{
			this._weakBitmapFrameDecodeEventSink.DetachSourceDownloadHandlers();
			this._weakBitmapFrameDecodeEventSink = null;
		}

		// Token: 0x060043EE RID: 17390 RVA: 0x00108894 File Offset: 0x00107C94
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void EnsureThumbnail()
		{
			if (this._isThumbnailCached || this.IsDownloading)
			{
				return;
			}
			this.EnsureSource();
			IntPtr zero = IntPtr.Zero;
			object syncObject = this._syncObject;
			lock (syncObject)
			{
				int thumbnail = UnsafeNativeMethods.WICBitmapFrameDecode.GetThumbnail(this._frameSource, out zero);
				if (thumbnail != -2003292348)
				{
					HRESULT.Check(thumbnail);
				}
			}
			this._isThumbnailCached = true;
			if (zero != IntPtr.Zero)
			{
				BitmapSourceSafeMILHandle bitmapSourceSafeMILHandle = new BitmapSourceSafeMILHandle(zero);
				SafeMILHandle safeMILHandle = BitmapPalette.CreateInternalPalette();
				BitmapPalette palette = null;
				if (UnsafeNativeMethods.WICBitmapSource.CopyPalette(bitmapSourceSafeMILHandle, safeMILHandle) == 0)
				{
					palette = new BitmapPalette(safeMILHandle);
				}
				this._thumbnail = new UnmanagedBitmapWrapper(BitmapSource.CreateCachedBitmap(null, bitmapSourceSafeMILHandle, BitmapCreateOptions.PreservePixelFormat, this._cacheOption, palette));
				this._thumbnail.Freeze();
			}
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x060043EF RID: 17391 RVA: 0x00108978 File Offset: 0x00107D78
		// (set) Token: 0x060043F0 RID: 17392 RVA: 0x00108A5C File Offset: 0x00107E5C
		internal override BitmapMetadata InternalMetadata
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				base.CheckIfSiteOfOrigin();
				if (!this._isMetadataCached && !this.IsDownloading)
				{
					this.EnsureSource();
					IntPtr zero = IntPtr.Zero;
					object syncObject = this._syncObject;
					lock (syncObject)
					{
						int metadataQueryReader = UnsafeNativeMethods.WICBitmapFrameDecode.GetMetadataQueryReader(this._frameSource, out zero);
						if (metadataQueryReader != -2003292287)
						{
							HRESULT.Check(metadataQueryReader);
						}
					}
					if (zero != IntPtr.Zero)
					{
						SafeMILHandle metadataHandle = new SafeMILHandle(zero);
						this._metadata = new BitmapMetadata(metadataHandle, true, this._decoder != null && this._decoder.IsMetadataFixedSize, this._syncObject);
						this._metadata.Freeze();
					}
					this._isMetadataCached = true;
				}
				return this._metadata;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060043F1 RID: 17393 RVA: 0x00108A70 File Offset: 0x00107E70
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void EnsureSource()
		{
			if (this._frameSource == null)
			{
				if (this._decoder == null)
				{
					HRESULT.Check(-2003292404);
				}
				if (this._decoder.InternalDecoder == null)
				{
					this._decoder = ((LateBoundBitmapDecoder)this._decoder).Decoder;
					this._syncObject = this._decoder.SyncObject;
				}
				IntPtr zero = IntPtr.Zero;
				object syncObject = this._syncObject;
				lock (syncObject)
				{
					HRESULT.Check(UnsafeNativeMethods.WICBitmapDecoder.GetFrame(this._decoder.InternalDecoder, (uint)this._frameNumber, out zero));
					this._frameSource = new BitmapSourceSafeMILHandle(zero);
					this._frameSource.CalculateSize();
				}
			}
		}

		// Token: 0x040018C0 RID: 6336
		private BitmapSourceSafeMILHandle _frameSource;

		// Token: 0x040018C1 RID: 6337
		private int _frameNumber;

		// Token: 0x040018C2 RID: 6338
		private bool _isThumbnailCached;

		// Token: 0x040018C3 RID: 6339
		private bool _isMetadataCached;

		// Token: 0x040018C4 RID: 6340
		private bool _isColorContextCached;

		// Token: 0x040018C5 RID: 6341
		private BitmapCreateOptions _createOptions;

		// Token: 0x040018C6 RID: 6342
		private BitmapCacheOption _cacheOption;

		// Token: 0x040018C7 RID: 6343
		private BitmapDecoder _decoder;

		// Token: 0x040018C8 RID: 6344
		private BitmapFrameDecode.WeakBitmapFrameDecodeEventSink _weakBitmapFrameDecodeEventSink;

		// Token: 0x020008D1 RID: 2257
		private class WeakBitmapFrameDecodeEventSink : WeakReference
		{
			// Token: 0x060058CD RID: 22733 RVA: 0x00168950 File Offset: 0x00167D50
			public WeakBitmapFrameDecodeEventSink(BitmapFrameDecode cloned, BitmapFrameDecode original) : base(cloned)
			{
				this._original = original;
				if (!this._original.IsFrozen)
				{
					this._original.DownloadCompleted += this.OnSourceDownloadCompleted;
					this._original.DownloadFailed += this.OnSourceDownloadFailed;
					this._original.DownloadProgress += this.OnSourceDownloadProgress;
				}
			}

			// Token: 0x060058CE RID: 22734 RVA: 0x001689C0 File Offset: 0x00167DC0
			public void OnSourceDownloadCompleted(object sender, EventArgs e)
			{
				BitmapFrameDecode bitmapFrameDecode = this.Target as BitmapFrameDecode;
				if (bitmapFrameDecode != null)
				{
					bitmapFrameDecode.OnOriginalDownloadCompleted(this._original, e);
					return;
				}
				this.DetachSourceDownloadHandlers();
			}

			// Token: 0x060058CF RID: 22735 RVA: 0x001689F0 File Offset: 0x00167DF0
			public void OnSourceDownloadFailed(object sender, ExceptionEventArgs e)
			{
				BitmapFrameDecode bitmapFrameDecode = this.Target as BitmapFrameDecode;
				if (bitmapFrameDecode != null)
				{
					bitmapFrameDecode.OnOriginalDownloadFailed(e);
					return;
				}
				this.DetachSourceDownloadHandlers();
			}

			// Token: 0x060058D0 RID: 22736 RVA: 0x00168A1C File Offset: 0x00167E1C
			public void OnSourceDownloadProgress(object sender, DownloadProgressEventArgs e)
			{
				BitmapFrameDecode bitmapFrameDecode = this.Target as BitmapFrameDecode;
				if (bitmapFrameDecode != null)
				{
					bitmapFrameDecode.OnDownloadProgress(sender, e);
					return;
				}
				this.DetachSourceDownloadHandlers();
			}

			// Token: 0x060058D1 RID: 22737 RVA: 0x00168A48 File Offset: 0x00167E48
			public void DetachSourceDownloadHandlers()
			{
				if (!this._original.IsFrozen)
				{
					this._original.DownloadCompleted -= this.OnSourceDownloadCompleted;
					this._original.DownloadFailed -= this.OnSourceDownloadFailed;
					this._original.DownloadProgress -= this.OnSourceDownloadProgress;
				}
			}

			// Token: 0x0400298B RID: 10635
			private BitmapFrameDecode _original;
		}
	}
}
