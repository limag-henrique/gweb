using System;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para eventos roteados relacionados ao mouse que não envolvem especificamente os botões do mouse ou a roda do mouse, por exemplo <see cref="E:System.Windows.UIElement.MouseMove" />.</summary>
	// Token: 0x02000283 RID: 643
	public class MouseEventArgs : InputEventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.MouseEventArgs" /> usando o <see cref="T:System.Windows.Input.MouseDevice" /> especificado e o carimbo de data/hora</summary>
		/// <param name="mouse">O dispositivo de mouse associado a este evento.</param>
		/// <param name="timestamp">A hora em que ocorreu a entrada.</param>
		// Token: 0x06001300 RID: 4864 RVA: 0x00047C60 File Offset: 0x00047060
		public MouseEventArgs(MouseDevice mouse, int timestamp) : base(mouse, timestamp)
		{
			if (mouse == null)
			{
				throw new ArgumentNullException("mouse");
			}
			this._stylusDevice = null;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.MouseEventArgs" /> usando o <see cref="T:System.Windows.Input.MouseDevice" />, o carimbo de data/hora e o <see cref="T:System.Windows.Input.StylusDevice" /> especificados.</summary>
		/// <param name="mouse">O dispositivo de mouse associado a este evento.</param>
		/// <param name="timestamp">A hora em que ocorreu a entrada.</param>
		/// <param name="stylusDevice">O dispositivo de caneta lógico associado a este evento.</param>
		// Token: 0x06001301 RID: 4865 RVA: 0x00047C8C File Offset: 0x0004708C
		public MouseEventArgs(MouseDevice mouse, int timestamp, StylusDevice stylusDevice) : base(mouse, timestamp)
		{
			if (mouse == null)
			{
				throw new ArgumentNullException("mouse");
			}
			this._stylusDevice = stylusDevice;
		}

		/// <summary>Obtém o dispositivo de mouse associado a este evento.</summary>
		/// <returns>O dispositivo de mouse associado a este evento.  Nenhum valor padrão.</returns>
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x00047CB8 File Offset: 0x000470B8
		public MouseDevice MouseDevice
		{
			get
			{
				return (MouseDevice)base.Device;
			}
		}

		/// <summary>Obtém o dispositivo de caneta associado a este evento.</summary>
		/// <returns>A caneta associada ao evento.  Nenhum valor padrão.</returns>
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x00047CD0 File Offset: 0x000470D0
		public StylusDevice StylusDevice
		{
			get
			{
				return this._stylusDevice;
			}
		}

		/// <summary>Retorna a posição do ponteiro do mouse em relação a um elemento especificado.</summary>
		/// <param name="relativeTo">O elemento a ser usado como o quadro de referência para calcular a posição do ponteiro do mouse.</param>
		/// <returns>As coordenadas x e y da posição do ponteiro do mouse com relação ao objeto especificado.</returns>
		// Token: 0x06001304 RID: 4868 RVA: 0x00047CE4 File Offset: 0x000470E4
		public Point GetPosition(IInputElement relativeTo)
		{
			return this.MouseDevice.GetPosition(relativeTo);
		}

		/// <summary>Obtém o estado atual do botão esquerdo do mouse.</summary>
		/// <returns>O estado atual do botão esquerdo do mouse, que é <see cref="F:System.Windows.Input.MouseButtonState.Pressed" /> ou <see cref="F:System.Windows.Input.MouseButtonState.Released" />.  Nenhum valor padrão.</returns>
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x00047D00 File Offset: 0x00047100
		public MouseButtonState LeftButton
		{
			get
			{
				return this.MouseDevice.LeftButton;
			}
		}

		/// <summary>Obtém o estado atual do botão direito do mouse.</summary>
		/// <returns>O estado atual do botão direito do mouse, que é <see cref="F:System.Windows.Input.MouseButtonState.Pressed" /> ou <see cref="F:System.Windows.Input.MouseButtonState.Released" />.  Nenhum valor padrão.</returns>
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x00047D18 File Offset: 0x00047118
		public MouseButtonState RightButton
		{
			get
			{
				return this.MouseDevice.RightButton;
			}
		}

		/// <summary>Obtém o estado atual do botão do meio do mouse.</summary>
		/// <returns>O estado atual do botão do meio do mouse, que é <see cref="F:System.Windows.Input.MouseButtonState.Pressed" /> ou <see cref="F:System.Windows.Input.MouseButtonState.Released" />. Nenhum valor padrão.</returns>
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x00047D30 File Offset: 0x00047130
		public MouseButtonState MiddleButton
		{
			get
			{
				return this.MouseDevice.MiddleButton;
			}
		}

		/// <summary>Obtém o estado atual do primeiro botão de mouse estendido.</summary>
		/// <returns>O estado atual do primeiro botão do mouse estendido, que é <see cref="F:System.Windows.Input.MouseButtonState.Pressed" /> ou <see cref="F:System.Windows.Input.MouseButtonState.Released" />.  Nenhum valor padrão.</returns>
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x00047D48 File Offset: 0x00047148
		public MouseButtonState XButton1
		{
			get
			{
				return this.MouseDevice.XButton1;
			}
		}

		/// <summary>Obtém o estado do segundo botão do mouse estendido.</summary>
		/// <returns>O estado atual do segundo botão do mouse estendido, que é <see cref="F:System.Windows.Input.MouseButtonState.Pressed" /> ou <see cref="F:System.Windows.Input.MouseButtonState.Released" />.  Nenhum valor padrão.</returns>
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x00047D60 File Offset: 0x00047160
		public MouseButtonState XButton2
		{
			get
			{
				return this.MouseDevice.XButton2;
			}
		}

		/// <summary>Invoca manipuladores de eventos em uma forma específica de tipo, o que pode aumentar a eficiência do sistema de eventos.</summary>
		/// <param name="genericHandler">O manipulador genérico a ser chamado de uma forma específica ao tipo.</param>
		/// <param name="genericTarget">O destino no qual chamar o manipulador.</param>
		// Token: 0x0600130A RID: 4874 RVA: 0x00047D78 File Offset: 0x00047178
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)genericHandler;
			mouseEventHandler(genericTarget, this);
		}

		// Token: 0x04000A47 RID: 2631
		private StylusDevice _stylusDevice;
	}
}
