using System;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para o evento de <see cref="E:System.Windows.Input.Keyboard.KeyboardInputProviderAcquireFocus" /> .</summary>
	// Token: 0x0200026A RID: 618
	public class KeyboardInputProviderAcquireFocusEventArgs : KeyboardEventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.KeyboardInputProviderAcquireFocusEventArgs" />.</summary>
		/// <param name="keyboard">O dispositivo de teclado lógico associado a este evento.</param>
		/// <param name="timestamp">A hora em que ocorreu a entrada.</param>
		/// <param name="focusAcquired">
		///   <see langword="true" /> para indicar que o foco de interoperação foi adquirido; caso contrário, <see langword="false" />.</param>
		// Token: 0x0600117E RID: 4478 RVA: 0x000421F8 File Offset: 0x000415F8
		public KeyboardInputProviderAcquireFocusEventArgs(KeyboardDevice keyboard, int timestamp, bool focusAcquired) : base(keyboard, timestamp)
		{
			this._focusAcquired = focusAcquired;
		}

		/// <summary>Obtém um valor que indica se o foco de interoperação foi adquirido.</summary>
		/// <returns>
		///   <see langword="true" /> Se o foco de interoperação foi adquirido; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x00042214 File Offset: 0x00041614
		public bool FocusAcquired
		{
			get
			{
				return this._focusAcquired;
			}
		}

		/// <summary>Chama o manipulador específico ao tipo no destino.</summary>
		/// <param name="genericHandler">O manipulador genérico a ser chamado de uma forma específica ao tipo.</param>
		/// <param name="genericTarget">O destino no qual chamar o manipulador.</param>
		// Token: 0x06001180 RID: 4480 RVA: 0x00042228 File Offset: 0x00041628
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			KeyboardInputProviderAcquireFocusEventHandler keyboardInputProviderAcquireFocusEventHandler = (KeyboardInputProviderAcquireFocusEventHandler)genericHandler;
			keyboardInputProviderAcquireFocusEventHandler(genericTarget, this);
		}

		// Token: 0x04000998 RID: 2456
		private bool _focusAcquired;
	}
}
