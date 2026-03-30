using System;
using System.Security;
using System.Security.Permissions;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Fornece dados de eventos de entrada pós-processamento.</summary>
	// Token: 0x0200028B RID: 651
	public class ProcessInputEventArgs : NotifyInputEventArgs
	{
		// Token: 0x06001327 RID: 4903 RVA: 0x00047EA8 File Offset: 0x000472A8
		internal ProcessInputEventArgs()
		{
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x00047EBC File Offset: 0x000472BC
		[SecurityCritical]
		internal override void Reset(StagingAreaInputItem input, InputManager inputManager)
		{
			this._allowAccessToStagingArea = true;
			base.Reset(input, inputManager);
		}

		/// <summary>Coloca o evento de entrada especificado no topo da pilha da área de preparo especificada.</summary>
		/// <param name="input">O evento de entrada a ser colocado na pilha de área de preparo.</param>
		/// <param name="promote">Um item de área de preparo existente do qual promover o estado.</param>
		/// <returns>O item de entrada da área de preparo que encapsula a entrada especificada.</returns>
		// Token: 0x06001329 RID: 4905 RVA: 0x00047ED8 File Offset: 0x000472D8
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public StagingAreaInputItem PushInput(InputEventArgs input, StagingAreaInputItem promote)
		{
			if (!this._allowAccessToStagingArea)
			{
				throw new InvalidOperationException(SR.Get("NotAllowedToAccessStagingArea"));
			}
			return base.UnsecureInputManager.PushInput(input, promote);
		}

		/// <summary>Coloca o evento de entrada especificado no topo da pilha da área de preparo.</summary>
		/// <param name="input">O evento de entrada a ser colocado na pilha de área de preparo.</param>
		/// <returns>O item de entrada da área de preparo.</returns>
		// Token: 0x0600132A RID: 4906 RVA: 0x00047F0C File Offset: 0x0004730C
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public StagingAreaInputItem PushInput(StagingAreaInputItem input)
		{
			if (!this._allowAccessToStagingArea)
			{
				throw new InvalidOperationException(SR.Get("NotAllowedToAccessStagingArea"));
			}
			return base.UnsecureInputManager.PushInput(input);
		}

		/// <summary>Remove o evento de entrada do topo da pilha da área de preparo.</summary>
		/// <returns>O evento de entrada que estava no topo da pilha da área de preparo. Isso será <see langword="null" /> se a área de preparo estiver vazia.</returns>
		// Token: 0x0600132B RID: 4907 RVA: 0x00047F40 File Offset: 0x00047340
		[SecurityCritical]
		public StagingAreaInputItem PopInput()
		{
			SecurityHelper.DemandUnrestrictedUIPermission();
			if (!this._allowAccessToStagingArea)
			{
				throw new InvalidOperationException(SR.Get("NotAllowedToAccessStagingArea"));
			}
			return base.UnsecureInputManager.PopInput();
		}

		/// <summary>Obtém mas não remove o evento de entrada do topo da pilha da área de preparo.</summary>
		/// <returns>O evento de entrada que está no topo da pilha da área de preparo.</returns>
		// Token: 0x0600132C RID: 4908 RVA: 0x00047F78 File Offset: 0x00047378
		[SecurityCritical]
		public StagingAreaInputItem PeekInput()
		{
			SecurityHelper.DemandUnrestrictedUIPermission();
			if (!this._allowAccessToStagingArea)
			{
				throw new InvalidOperationException(SR.Get("NotAllowedToAccessStagingArea"));
			}
			return base.UnsecureInputManager.PeekInput();
		}

		// Token: 0x04000A4C RID: 2636
		private bool _allowAccessToStagingArea;
	}
}
