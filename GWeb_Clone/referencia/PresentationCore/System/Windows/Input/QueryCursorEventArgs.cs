using System;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para o evento de <see cref="E:System.Windows.Input.Mouse.QueryCursor" /> .</summary>
	// Token: 0x0200028D RID: 653
	public class QueryCursorEventArgs : MouseEventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.QueryCursorEventArgs" /> usando o dispositivo de mouse especificado e o carimbo de data/hora especificado.</summary>
		/// <param name="mouse">O dispositivo de mouse lógico associado a este evento.</param>
		/// <param name="timestamp">A hora em que ocorreu a entrada.</param>
		// Token: 0x06001331 RID: 4913 RVA: 0x00047FB0 File Offset: 0x000473B0
		public QueryCursorEventArgs(MouseDevice mouse, int timestamp) : base(mouse, timestamp)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.QueryCursorEventArgs" /> usando o dispositivo de mouse, o carimbo de data/hora e o dispositivo de caneta especificados.</summary>
		/// <param name="mouse">O dispositivo de mouse lógico associado a este evento.</param>
		/// <param name="timestamp">A hora em que ocorreu a entrada.</param>
		/// <param name="stylusDevice">O ponteiro de caneta associado a este evento.</param>
		// Token: 0x06001332 RID: 4914 RVA: 0x00047FC8 File Offset: 0x000473C8
		public QueryCursorEventArgs(MouseDevice mouse, int timestamp, StylusDevice stylusDevice) : base(mouse, timestamp, stylusDevice)
		{
		}

		/// <summary>Obtém ou define o cursor associado a este evento.</summary>
		/// <returns>O cursor.</returns>
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x00047FE0 File Offset: 0x000473E0
		// (set) Token: 0x06001334 RID: 4916 RVA: 0x00047FF4 File Offset: 0x000473F4
		public Cursor Cursor
		{
			get
			{
				return this._cursor;
			}
			set
			{
				this._cursor = ((value == null) ? Cursors.None : value);
			}
		}

		/// <summary>Invoca manipuladores de eventos em uma forma específica de tipo, o que pode aumentar a eficiência do sistema de eventos.</summary>
		/// <param name="genericHandler">O manipulador genérico a ser chamado de uma forma específica ao tipo.</param>
		/// <param name="genericTarget">O destino no qual chamar o manipulador.</param>
		// Token: 0x06001335 RID: 4917 RVA: 0x00048014 File Offset: 0x00047414
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			QueryCursorEventHandler queryCursorEventHandler = (QueryCursorEventHandler)genericHandler;
			queryCursorEventHandler(genericTarget, this);
		}

		// Token: 0x04000A4D RID: 2637
		private Cursor _cursor;
	}
}
