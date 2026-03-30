using System;

namespace System.Windows.Input
{
	/// <summary>Fornece dados de eventos de entrada por toque.</summary>
	// Token: 0x0200029D RID: 669
	public class TouchEventArgs : InputEventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.TouchEventArgs" />.</summary>
		/// <param name="touchDevice">O dispositivo de entrada a ser associado a este evento.</param>
		/// <param name="timestamp">A hora em que ocorreu a entrada.</param>
		// Token: 0x060013AE RID: 5038 RVA: 0x00049CD4 File Offset: 0x000490D4
		public TouchEventArgs(TouchDevice touchDevice, int timestamp) : base(touchDevice, timestamp)
		{
		}

		/// <summary>Obtém o toque que gerou o evento.</summary>
		/// <returns>O toque que gerou o evento.</returns>
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x00049CEC File Offset: 0x000490EC
		public TouchDevice TouchDevice
		{
			get
			{
				return (TouchDevice)base.Device;
			}
		}

		/// <summary>Retorna a posição atual do dispositivo de toque em relação ao elemento especificado.</summary>
		/// <param name="relativeTo">O elemento que define o espaço de coordenadas.</param>
		/// <returns>A posição atual do dispositivo de toque em relação ao elemento especificado.</returns>
		// Token: 0x060013B0 RID: 5040 RVA: 0x00049D04 File Offset: 0x00049104
		public TouchPoint GetTouchPoint(IInputElement relativeTo)
		{
			return this.TouchDevice.GetTouchPoint(relativeTo);
		}

		/// <summary>Retorna todos os pontos de toque que foram coletados entre os eventos de toque mais recentes e anteriores.</summary>
		/// <param name="relativeTo">O elemento que define o espaço de coordenadas.</param>
		/// <returns>Todos os pontos de toque que foram coletados entre os eventos de toque mais recentes e anteriores.</returns>
		// Token: 0x060013B1 RID: 5041 RVA: 0x00049D20 File Offset: 0x00049120
		public TouchPointCollection GetIntermediateTouchPoints(IInputElement relativeTo)
		{
			return this.TouchDevice.GetIntermediateTouchPoints(relativeTo);
		}

		/// <summary>Invoca manipuladores de eventos em uma forma específica de tipo, o que pode aumentar a eficiência do sistema de eventos.</summary>
		/// <param name="genericHandler">O manipulador genérico a ser chamado de uma forma específica ao tipo.</param>
		/// <param name="genericTarget">O destino no qual chamar o manipulador.</param>
		// Token: 0x060013B2 RID: 5042 RVA: 0x00049D3C File Offset: 0x0004913C
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			EventHandler<TouchEventArgs> eventHandler = (EventHandler<TouchEventArgs>)genericHandler;
			eventHandler(genericTarget, this);
		}
	}
}
