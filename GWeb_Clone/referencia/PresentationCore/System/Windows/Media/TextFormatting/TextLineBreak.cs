using System;
using System.Security;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Internal.TextFormatting;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Especifica as propriedades de texto e o estado no ponto em que o texto é dividido pela processo de quebra de linha.</summary>
	// Token: 0x020005AC RID: 1452
	public sealed class TextLineBreak : IDisposable
	{
		// Token: 0x0600427D RID: 17021 RVA: 0x001039C0 File Offset: 0x00102DC0
		internal TextLineBreak(TextModifierScope currentScope, SecurityCriticalDataForSet<IntPtr> breakRecord)
		{
			this._currentScope = currentScope;
			this._breakRecord = breakRecord;
			if (breakRecord.Value == IntPtr.Zero)
			{
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>Finaliza o objeto para executar operações de limpeza em recursos não gerenciados mantidos pelo objeto atual antes do objeto atual ser destruído.</summary>
		// Token: 0x0600427E RID: 17022 RVA: 0x001039FC File Offset: 0x00102DFC
		~TextLineBreak()
		{
			this.DisposeInternal(true);
		}

		/// <summary>Libera os recursos usados pela classe <see cref="T:System.Windows.Media.TextFormatting.TextLineBreak" />.</summary>
		// Token: 0x0600427F RID: 17023 RVA: 0x00103A38 File Offset: 0x00102E38
		public void Dispose()
		{
			this.DisposeInternal(false);
			GC.SuppressFinalize(this);
		}

		/// <summary>Clona uma nova instância do objeto <see cref="T:System.Windows.Media.TextFormatting.TextLineBreak" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.TextFormatting.TextLineBreak" />.</returns>
		// Token: 0x06004280 RID: 17024 RVA: 0x00103A54 File Offset: 0x00102E54
		[SecurityCritical]
		public TextLineBreak Clone()
		{
			IntPtr zero = IntPtr.Zero;
			if (this._breakRecord.Value != IntPtr.Zero)
			{
				LsErr lsErr = UnsafeNativeMethods.LoCloneBreakRecord(this._breakRecord.Value, out zero);
				if (lsErr != LsErr.None)
				{
					TextFormatterContext.ThrowExceptionFromLsError(SR.Get("CloneBreakRecordFailure", new object[]
					{
						lsErr
					}), lsErr);
				}
			}
			return new TextLineBreak(this._currentScope, new SecurityCriticalDataForSet<IntPtr>(zero));
		}

		// Token: 0x06004281 RID: 17025 RVA: 0x00103AC4 File Offset: 0x00102EC4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void DisposeInternal(bool finalizing)
		{
			if (this._breakRecord.Value != IntPtr.Zero)
			{
				UnsafeNativeMethods.LoDisposeBreakRecord(this._breakRecord.Value, finalizing);
				this._breakRecord.Value = IntPtr.Zero;
				GC.KeepAlive(this);
			}
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06004282 RID: 17026 RVA: 0x00103B10 File Offset: 0x00102F10
		internal TextModifierScope TextModifierScope
		{
			get
			{
				return this._currentScope;
			}
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06004283 RID: 17027 RVA: 0x00103B24 File Offset: 0x00102F24
		internal SecurityCriticalDataForSet<IntPtr> BreakRecord
		{
			get
			{
				return this._breakRecord;
			}
		}

		// Token: 0x04001835 RID: 6197
		private TextModifierScope _currentScope;

		// Token: 0x04001836 RID: 6198
		private SecurityCriticalDataForSet<IntPtr> _breakRecord;
	}
}
