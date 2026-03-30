using System;

namespace System.Windows.Ink
{
	/// <summary>Fornece dados para o evento de <see cref="E:System.Windows.Controls.InkCanvas.DefaultDrawingAttributesReplaced" /> .</summary>
	// Token: 0x0200034A RID: 842
	public class DrawingAttributesReplacedEventArgs : EventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Ink.DrawingAttributesReplacedEventArgs" />.</summary>
		/// <param name="newDrawingAttributes">O novo <see cref="T:System.Windows.Ink.DrawingAttributes" />.</param>
		/// <param name="previousDrawingAttributes">O <see cref="T:System.Windows.Ink.DrawingAttributes" /> antigo.</param>
		// Token: 0x06001C66 RID: 7270 RVA: 0x000739E8 File Offset: 0x00072DE8
		public DrawingAttributesReplacedEventArgs(DrawingAttributes newDrawingAttributes, DrawingAttributes previousDrawingAttributes)
		{
			if (newDrawingAttributes == null)
			{
				throw new ArgumentNullException("newDrawingAttributes");
			}
			if (previousDrawingAttributes == null)
			{
				throw new ArgumentNullException("previousDrawingAttributes");
			}
			this._newDrawingAttributes = newDrawingAttributes;
			this._previousDrawingAttributes = previousDrawingAttributes;
		}

		/// <summary>Obtém o novo <see cref="T:System.Windows.Ink.DrawingAttributes" />.</summary>
		/// <returns>O novo <see cref="T:System.Windows.Ink.DrawingAttributes" />.</returns>
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001C67 RID: 7271 RVA: 0x00073A34 File Offset: 0x00072E34
		public DrawingAttributes NewDrawingAttributes
		{
			get
			{
				return this._newDrawingAttributes;
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Ink.DrawingAttributes" /> antigo.</summary>
		/// <returns>O <see cref="T:System.Windows.Ink.DrawingAttributes" /> antigo.</returns>
		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x00073A48 File Offset: 0x00072E48
		public DrawingAttributes PreviousDrawingAttributes
		{
			get
			{
				return this._previousDrawingAttributes;
			}
		}

		// Token: 0x04000F80 RID: 3968
		private DrawingAttributes _newDrawingAttributes;

		// Token: 0x04000F81 RID: 3969
		private DrawingAttributes _previousDrawingAttributes;
	}
}
