using System;
using System.Security;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Internal.TextFormatting;

namespace System.Windows.Media.TextFormatting
{
	// Token: 0x0200059A RID: 1434
	[FriendAccessAllowed]
	internal abstract class TextBreakpoint : ITextMetrics, IDisposable
	{
		// Token: 0x060041EC RID: 16876 RVA: 0x0010286C File Offset: 0x00101C6C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060041ED RID: 16877 RVA: 0x00102888 File Offset: 0x00101C88
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060041EE RID: 16878
		public abstract TextLineBreak GetTextLineBreak();

		// Token: 0x060041EF RID: 16879
		[SecurityCritical]
		internal abstract SecurityCriticalDataForSet<IntPtr> GetTextPenaltyResource();

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x060041F0 RID: 16880
		public abstract bool IsTruncated { get; }

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x060041F1 RID: 16881
		public abstract int Length { get; }

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x060041F2 RID: 16882
		public abstract int DependentLength { get; }

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x060041F3 RID: 16883
		public abstract int NewlineLength { get; }

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x060041F4 RID: 16884
		public abstract double Start { get; }

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x060041F5 RID: 16885
		public abstract double Width { get; }

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x060041F6 RID: 16886
		public abstract double WidthIncludingTrailingWhitespace { get; }

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x060041F7 RID: 16887
		public abstract double Height { get; }

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x060041F8 RID: 16888
		public abstract double TextHeight { get; }

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x060041F9 RID: 16889
		public abstract double Baseline { get; }

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x060041FA RID: 16890
		public abstract double TextBaseline { get; }

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x060041FB RID: 16891
		public abstract double MarkerBaseline { get; }

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x060041FC RID: 16892
		public abstract double MarkerHeight { get; }
	}
}
