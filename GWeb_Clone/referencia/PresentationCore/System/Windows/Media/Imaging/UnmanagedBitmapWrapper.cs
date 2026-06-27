using System;
using System.Security;

namespace System.Windows.Media.Imaging
{
	// Token: 0x02000602 RID: 1538
	internal sealed class UnmanagedBitmapWrapper : BitmapSource
	{
		// Token: 0x06004662 RID: 18018 RVA: 0x00113608 File Offset: 0x00112A08
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public UnmanagedBitmapWrapper(BitmapSourceSafeMILHandle bitmapSource) : base(true)
		{
			this._bitmapInit.BeginInit();
			base.WicSourceHandle = bitmapSource;
			this._bitmapInit.EndInit();
			this.UpdateCachedSettings();
		}

		// Token: 0x06004663 RID: 18019 RVA: 0x00113640 File Offset: 0x00112A40
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal UnmanagedBitmapWrapper(bool initialize) : base(true)
		{
			if (initialize)
			{
				this._bitmapInit.BeginInit();
				this._bitmapInit.EndInit();
			}
		}

		// Token: 0x06004664 RID: 18020 RVA: 0x00113670 File Offset: 0x00112A70
		protected override Freezable CreateInstanceCore()
		{
			return new UnmanagedBitmapWrapper(false);
		}

		// Token: 0x06004665 RID: 18021 RVA: 0x00113684 File Offset: 0x00112A84
		private void CopyCommon(UnmanagedBitmapWrapper sourceBitmap)
		{
			this._bitmapInit.BeginInit();
			this._bitmapInit.EndInit();
		}

		// Token: 0x06004666 RID: 18022 RVA: 0x001136A8 File Offset: 0x00112AA8
		protected override void CloneCore(Freezable sourceFreezable)
		{
			UnmanagedBitmapWrapper sourceBitmap = (UnmanagedBitmapWrapper)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(sourceBitmap);
		}

		// Token: 0x06004667 RID: 18023 RVA: 0x001136CC File Offset: 0x00112ACC
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			UnmanagedBitmapWrapper sourceBitmap = (UnmanagedBitmapWrapper)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(sourceBitmap);
		}

		// Token: 0x06004668 RID: 18024 RVA: 0x001136F0 File Offset: 0x00112AF0
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			UnmanagedBitmapWrapper sourceBitmap = (UnmanagedBitmapWrapper)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			this.CopyCommon(sourceBitmap);
		}

		// Token: 0x06004669 RID: 18025 RVA: 0x00113714 File Offset: 0x00112B14
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			UnmanagedBitmapWrapper sourceBitmap = (UnmanagedBitmapWrapper)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			this.CopyCommon(sourceBitmap);
		}
	}
}
