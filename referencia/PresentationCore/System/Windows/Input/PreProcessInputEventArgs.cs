using System;
using System.Security;

namespace System.Windows.Input
{
	/// <summary>Fornece dados de eventos de entrada pós-processamento.</summary>
	// Token: 0x02000289 RID: 649
	public sealed class PreProcessInputEventArgs : ProcessInputEventArgs
	{
		// Token: 0x0600131F RID: 4895 RVA: 0x00047E50 File Offset: 0x00047250
		internal PreProcessInputEventArgs()
		{
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00047E64 File Offset: 0x00047264
		[SecurityCritical]
		internal override void Reset(StagingAreaInputItem input, InputManager inputManager)
		{
			this._canceled = false;
			base.Reset(input, inputManager);
		}

		/// <summary>Cancela o processamento do evento de entrada.</summary>
		// Token: 0x06001321 RID: 4897 RVA: 0x00047E80 File Offset: 0x00047280
		public void Cancel()
		{
			this._canceled = true;
		}

		/// <summary>Determina se o processamento do evento de entrada foi cancelado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o processamento tiver sido cancelado; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x00047E94 File Offset: 0x00047294
		public bool Canceled
		{
			get
			{
				return this._canceled;
			}
		}

		// Token: 0x04000A4B RID: 2635
		private bool _canceled;
	}
}
