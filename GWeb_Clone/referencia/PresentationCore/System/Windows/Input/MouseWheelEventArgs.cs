using System;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para vários eventos que relatam alterações ao valor de delta da roda do mouse de um dispositivo de mouse.</summary>
	// Token: 0x02000285 RID: 645
	public class MouseWheelEventArgs : MouseEventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.MouseWheelEventArgs" />.</summary>
		/// <param name="mouse">O dispositivo de mouse associado a este evento.</param>
		/// <param name="timestamp">A hora em que ocorreu a entrada.</param>
		/// <param name="delta">O quanto a roda do mouse mudou.</param>
		// Token: 0x0600130F RID: 4879 RVA: 0x00047D94 File Offset: 0x00047194
		public MouseWheelEventArgs(MouseDevice mouse, int timestamp, int delta) : base(mouse, timestamp)
		{
			MouseWheelEventArgs._delta = delta;
		}

		/// <summary>Obtém um valor que indica o quanto a roda do mouse mudou.</summary>
		/// <returns>O quanto a roda do mouse mudou. Esse valor é positivo se a roda do mouse é girada em uma direção para cima (afastando-se do usuário) ou negativo se a roda do mouse é girada em uma direção para baixo (em direção ao usuário).</returns>
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x00047DB0 File Offset: 0x000471B0
		public int Delta
		{
			get
			{
				return MouseWheelEventArgs._delta;
			}
		}

		/// <summary>Invoca manipuladores de eventos em uma forma específica de tipo, o que pode aumentar a eficiência do sistema de eventos.</summary>
		/// <param name="genericHandler">O manipulador genérico a ser chamado de uma forma específica ao tipo.</param>
		/// <param name="genericTarget">O destino no qual chamar o manipulador.</param>
		// Token: 0x06001311 RID: 4881 RVA: 0x00047DC4 File Offset: 0x000471C4
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			MouseWheelEventHandler mouseWheelEventHandler = (MouseWheelEventHandler)genericHandler;
			mouseWheelEventHandler(genericTarget, this);
		}

		// Token: 0x04000A48 RID: 2632
		private static int _delta;
	}
}
