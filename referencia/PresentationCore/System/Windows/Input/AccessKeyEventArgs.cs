using System;
using System.Security;
using MS.Internal;

namespace System.Windows.Input
{
	/// <summary>Fornece informações sobre os eventos de chaves de acesso.</summary>
	// Token: 0x0200022B RID: 555
	public class AccessKeyEventArgs : EventArgs
	{
		// Token: 0x06000F6B RID: 3947 RVA: 0x0003AE78 File Offset: 0x0003A278
		[SecurityCritical]
		internal AccessKeyEventArgs(string key, bool isMultiple, bool userInitiated)
		{
			this._key = key;
			this._isMultiple = isMultiple;
			this._userInitiated = new SecurityCriticalDataForSet<bool>(userInitiated);
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0003AEA8 File Offset: 0x0003A2A8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal void ClearUserInitiated()
		{
			this._userInitiated.Value = false;
		}

		/// <summary>Obtém as chaves de acesso que foram pressionadas.</summary>
		/// <returns>A chave de acesso.</returns>
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x0003AEC4 File Offset: 0x0003A2C4
		public string Key
		{
			get
			{
				return this._key;
			}
		}

		/// <summary>Obtém um valor que indica se os outros elementos são invocados pela chave.</summary>
		/// <returns>
		///   <see langword="true" /> Se outros elementos são invocados; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0003AED8 File Offset: 0x0003A2D8
		public bool IsMultiple
		{
			get
			{
				return this._isMultiple;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x0003AEEC File Offset: 0x0003A2EC
		internal bool UserInitiated
		{
			get
			{
				return this._userInitiated.Value;
			}
		}

		// Token: 0x04000865 RID: 2149
		private string _key;

		// Token: 0x04000866 RID: 2150
		private bool _isMultiple;

		// Token: 0x04000867 RID: 2151
		private SecurityCriticalDataForSet<bool> _userInitiated;
	}
}
