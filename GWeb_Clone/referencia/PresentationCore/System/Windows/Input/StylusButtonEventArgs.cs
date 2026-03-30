using System;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para os eventos de <see cref="E:System.Windows.UIElement.StylusButtonDown" /> e de <see cref="E:System.Windows.UIElement.StylusButtonUp" /> .</summary>
	// Token: 0x020002AE RID: 686
	public class StylusButtonEventArgs : StylusEventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.StylusButtonEventArgs" />.</summary>
		/// <param name="stylusDevice">O <see cref="T:System.Windows.Input.StylusDevice" /> a ser associado a este evento.</param>
		/// <param name="timestamp">A hora em que o evento ocorre.</param>
		/// <param name="button">O <see cref="T:System.Windows.Input.StylusButton" /> que gera o evento.</param>
		// Token: 0x0600143D RID: 5181 RVA: 0x0004B424 File Offset: 0x0004A824
		public StylusButtonEventArgs(StylusDevice stylusDevice, int timestamp, StylusButton button) : base(stylusDevice, timestamp)
		{
			this._button = button;
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.StylusButton" /> que gera o evento.</summary>
		/// <returns>O <see cref="T:System.Windows.Input.StylusButton" /> que gera o evento.</returns>
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x0004B440 File Offset: 0x0004A840
		public StylusButton StylusButton
		{
			get
			{
				return this._button;
			}
		}

		/// <summary>Invoca manipuladores de eventos em uma forma específica de tipo, o que pode aumentar a eficiência do sistema de eventos.</summary>
		/// <param name="genericHandler">O manipulador genérico a ser chamado de uma forma específica ao tipo.</param>
		/// <param name="genericTarget">O destino no qual chamar o manipulador.</param>
		// Token: 0x0600143F RID: 5183 RVA: 0x0004B454 File Offset: 0x0004A854
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			StylusButtonEventHandler stylusButtonEventHandler = (StylusButtonEventHandler)genericHandler;
			stylusButtonEventHandler(genericTarget, this);
		}

		// Token: 0x04000AF9 RID: 2809
		private StylusButton _button;
	}
}
