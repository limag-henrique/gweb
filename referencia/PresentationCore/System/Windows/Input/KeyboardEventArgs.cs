using System;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para eventos relacionados ao teclado.</summary>
	// Token: 0x02000268 RID: 616
	public class KeyboardEventArgs : InputEventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.KeyboardEventArgs" />.</summary>
		/// <param name="keyboard">O dispositivo de teclado lógico associado a este evento.</param>
		/// <param name="timestamp">A hora em que ocorreu a entrada.</param>
		// Token: 0x06001177 RID: 4471 RVA: 0x000421AC File Offset: 0x000415AC
		public KeyboardEventArgs(KeyboardDevice keyboard, int timestamp) : base(keyboard, timestamp)
		{
		}

		/// <summary>O dispositivo de teclado lógico associado ao evento de entrada.</summary>
		/// <returns>O dispositivo de teclado lógico associado ao evento.</returns>
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x000421C4 File Offset: 0x000415C4
		public KeyboardDevice KeyboardDevice
		{
			get
			{
				return (KeyboardDevice)base.Device;
			}
		}

		/// <summary>Invoca manipuladores de eventos em uma forma específica de tipo, o que pode aumentar a eficiência do sistema de eventos.</summary>
		/// <param name="genericHandler">O manipulador genérico a ser chamado de uma forma específica ao tipo.</param>
		/// <param name="genericTarget">O destino no qual chamar o manipulador.</param>
		// Token: 0x06001179 RID: 4473 RVA: 0x000421DC File Offset: 0x000415DC
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			KeyboardEventHandler keyboardEventHandler = (KeyboardEventHandler)genericHandler;
			keyboardEventHandler(genericTarget, this);
		}
	}
}
