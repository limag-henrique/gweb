using System;
using System.Security;
using MS.Internal;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para entrada bruta sendo processada pelo <see cref="P:System.Windows.Input.NotifyInputEventArgs.InputManager" />.</summary>
	// Token: 0x02000287 RID: 647
	public class NotifyInputEventArgs : EventArgs
	{
		// Token: 0x06001316 RID: 4886 RVA: 0x00047DE0 File Offset: 0x000471E0
		internal NotifyInputEventArgs()
		{
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00047DF4 File Offset: 0x000471F4
		[SecurityCritical]
		internal virtual void Reset(StagingAreaInputItem input, InputManager inputManager)
		{
			this._input = input;
			this._inputManager = inputManager;
		}

		/// <summary>Obtém o item de entrada de área de preparo que está sendo processado pelo gerenciador de entrada.</summary>
		/// <returns>A área de preparo.</returns>
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x00047E10 File Offset: 0x00047210
		public StagingAreaInputItem StagingItem
		{
			get
			{
				return this._input;
			}
		}

		/// <summary>Obtém o gerenciador de entrada que processa o evento de entrada.</summary>
		/// <returns>O Gerenciador de entrada.</returns>
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x00047E24 File Offset: 0x00047224
		public InputManager InputManager
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandUnrestrictedUIPermission();
				return this._inputManager;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x00047E3C File Offset: 0x0004723C
		internal InputManager UnsecureInputManager
		{
			[SecurityCritical]
			get
			{
				return this._inputManager;
			}
		}

		// Token: 0x04000A49 RID: 2633
		private StagingAreaInputItem _input;

		// Token: 0x04000A4A RID: 2634
		[SecurityCritical]
		private InputManager _inputManager;
	}
}
