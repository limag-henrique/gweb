using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para eventos relacionados à entrada.</summary>
	// Token: 0x02000244 RID: 580
	[FriendAccessAllowed]
	public class InputEventArgs : RoutedEventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.InputEventArgs" />.</summary>
		/// <param name="inputDevice">O dispositivo de entrada a ser associado a este evento.</param>
		/// <param name="timestamp">A hora em que ocorreu a entrada.</param>
		// Token: 0x06001022 RID: 4130 RVA: 0x0003CFCC File Offset: 0x0003C3CC
		public InputEventArgs(InputDevice inputDevice, int timestamp)
		{
			this._inputDevice = inputDevice;
			InputEventArgs._timestamp = timestamp;
		}

		/// <summary>Obtém o dispositivo de entrada que iniciou esse evento.</summary>
		/// <returns>O dispositivo de entrada associado a este evento.</returns>
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x0003CFEC File Offset: 0x0003C3EC
		// (set) Token: 0x06001024 RID: 4132 RVA: 0x0003D000 File Offset: 0x0003C400
		public InputDevice Device
		{
			get
			{
				return this._inputDevice;
			}
			internal set
			{
				this._inputDevice = value;
			}
		}

		/// <summary>Obtém a hora em que esse evento ocorreu.</summary>
		/// <returns>O número de milissegundos decorridos desde a última reinicialização. Depois de aproximadamente 24.9 dias esse valor atingirá <see cref="F:System.Int32.MaxValue" /> e for reiniciado (negativo) <see cref="F:System.Int32.MinValue" />.</returns>
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x0003D014 File Offset: 0x0003C414
		public int Timestamp
		{
			get
			{
				return InputEventArgs._timestamp;
			}
		}

		/// <summary>Invoca manipuladores de eventos em uma forma específica de tipo, o que pode aumentar a eficiência do sistema de eventos.</summary>
		/// <param name="genericHandler">O manipulador genérico a ser chamado de uma forma específica ao tipo.</param>
		/// <param name="genericTarget">O destino no qual chamar o manipulador.</param>
		// Token: 0x06001026 RID: 4134 RVA: 0x0003D028 File Offset: 0x0003C428
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			InputEventHandler inputEventHandler = (InputEventHandler)genericHandler;
			inputEventHandler(genericTarget, this);
		}

		// Token: 0x040008B9 RID: 2233
		private InputDevice _inputDevice;

		// Token: 0x040008BA RID: 2234
		private static int _timestamp;
	}
}
