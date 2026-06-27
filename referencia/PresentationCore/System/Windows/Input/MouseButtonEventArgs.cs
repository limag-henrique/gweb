using System;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para eventos relacionados ao botão do mouse.</summary>
	// Token: 0x0200027F RID: 639
	public class MouseButtonEventArgs : MouseEventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> usando o <see cref="T:System.Windows.Input.MouseDevice" />, o carimbo de data/hora e o <see cref="T:System.Windows.Input.MouseButton" /> especificados.</summary>
		/// <param name="mouse">O dispositivo de Mouse lógico associado a este evento.</param>
		/// <param name="timestamp">A hora em que ocorreu a entrada.</param>
		/// <param name="button">O botão do mouse cujo estado está sendo descrito.</param>
		// Token: 0x060012B2 RID: 4786 RVA: 0x000456AC File Offset: 0x00044AAC
		public MouseButtonEventArgs(MouseDevice mouse, int timestamp, MouseButton button) : base(mouse, timestamp)
		{
			MouseButtonUtilities.Validate(button);
			this._button = button;
			this._count = 1;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> usando o <see cref="T:System.Windows.Input.MouseDevice" />, o carimbo de data/hora, o <see cref="T:System.Windows.Input.MouseButton" /> e o <see cref="T:System.Windows.Input.StylusDevice" /> especificados.  .</summary>
		/// <param name="mouse">O dispositivo de mouse lógico associado a este evento.</param>
		/// <param name="timestamp">A hora em que o evento ocorreu.</param>
		/// <param name="button">O botão associado a esse evento.</param>
		/// <param name="stylusDevice">O dispositivo de caneta associado a este evento.</param>
		// Token: 0x060012B3 RID: 4787 RVA: 0x000456D8 File Offset: 0x00044AD8
		public MouseButtonEventArgs(MouseDevice mouse, int timestamp, MouseButton button, StylusDevice stylusDevice) : base(mouse, timestamp, stylusDevice)
		{
			MouseButtonUtilities.Validate(button);
			this._button = button;
			this._count = 1;
		}

		/// <summary>Obtém o botão associado ao evento.</summary>
		/// <returns>O botão que foi pressionado.</returns>
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x00045704 File Offset: 0x00044B04
		public MouseButton ChangedButton
		{
			get
			{
				return this._button;
			}
		}

		/// <summary>Obtém o estado do botão associado ao evento.</summary>
		/// <returns>O estado em que o botão está.</returns>
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x00045718 File Offset: 0x00044B18
		public MouseButtonState ButtonState
		{
			get
			{
				MouseButtonState result = MouseButtonState.Released;
				switch (this._button)
				{
				case MouseButton.Left:
					result = base.MouseDevice.LeftButton;
					break;
				case MouseButton.Middle:
					result = base.MouseDevice.MiddleButton;
					break;
				case MouseButton.Right:
					result = base.MouseDevice.RightButton;
					break;
				case MouseButton.XButton1:
					result = base.MouseDevice.XButton1;
					break;
				case MouseButton.XButton2:
					result = base.MouseDevice.XButton2;
					break;
				}
				return result;
			}
		}

		/// <summary>Obtém o número de vezes que o botão recebeu um clique.</summary>
		/// <returns>O número de vezes que o botão do mouse recebeu um clique.</returns>
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x00045790 File Offset: 0x00044B90
		// (set) Token: 0x060012B7 RID: 4791 RVA: 0x000457A4 File Offset: 0x00044BA4
		public int ClickCount
		{
			get
			{
				return this._count;
			}
			internal set
			{
				this._count = value;
			}
		}

		/// <summary>Invoca manipuladores de eventos em uma forma específica de tipo, o que pode aumentar a eficiência do sistema de eventos.</summary>
		/// <param name="genericHandler">O manipulador genérico a ser chamado de uma forma específica ao tipo.</param>
		/// <param name="genericTarget">O destino no qual chamar o manipulador.</param>
		// Token: 0x060012B8 RID: 4792 RVA: 0x000457B8 File Offset: 0x00044BB8
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			MouseButtonEventHandler mouseButtonEventHandler = (MouseButtonEventHandler)genericHandler;
			mouseButtonEventHandler(genericTarget, this);
		}

		// Token: 0x04000A1E RID: 2590
		private MouseButton _button;

		// Token: 0x04000A1F RID: 2591
		private int _count;
	}
}
