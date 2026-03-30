using System;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para o evento de <see cref="E:System.Windows.UIElement.StylusDown" /> .</summary>
	// Token: 0x020002B5 RID: 693
	public class StylusDownEventArgs : StylusEventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.StylusDownEventArgs" />.</summary>
		/// <param name="stylusDevice">A instância de dispositivo à qual o evento está associado.</param>
		/// <param name="timestamp">Um carimbo de data/hora usado para desambiguar instâncias do evento.</param>
		// Token: 0x0600148D RID: 5261 RVA: 0x0004B9B8 File Offset: 0x0004ADB8
		public StylusDownEventArgs(StylusDevice stylusDevice, int timestamp) : base(stylusDevice, timestamp)
		{
		}

		/// <summary>Obtém o número de vezes em que a caneta do tablet foi tocada.</summary>
		/// <returns>O número de vezes que a caneta de tablet foi tocada.</returns>
		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x0004B9D0 File Offset: 0x0004ADD0
		public int TapCount
		{
			get
			{
				return base.StylusDeviceImpl.TapCount;
			}
		}

		/// <summary>Invoca manipuladores de eventos em uma forma específica de tipo, o que pode aumentar a eficiência do sistema de eventos.</summary>
		/// <param name="genericHandler">O manipulador genérico a ser chamado de uma forma específica ao tipo.</param>
		/// <param name="genericTarget">O destino no qual chamar o manipulador.</param>
		// Token: 0x0600148F RID: 5263 RVA: 0x0004B9E8 File Offset: 0x0004ADE8
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			StylusDownEventHandler stylusDownEventHandler = (StylusDownEventHandler)genericHandler;
			stylusDownEventHandler(genericTarget, this);
		}
	}
}
