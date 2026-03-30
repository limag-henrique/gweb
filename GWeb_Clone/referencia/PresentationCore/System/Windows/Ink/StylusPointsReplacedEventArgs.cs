using System;
using System.Windows.Input;

namespace System.Windows.Ink
{
	/// <summary>Fornece dados para o evento de <see cref="E:System.Windows.Ink.Stroke.StylusPointsReplaced" /> .</summary>
	// Token: 0x0200034C RID: 844
	public class StylusPointsReplacedEventArgs : EventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Ink.StylusPointsReplacedEventArgs" />.</summary>
		/// <param name="newStylusPoints">O <see cref="T:System.Windows.Input.StylusPointCollection" /> novo para o <see cref="T:System.Windows.Ink.Stroke" />.</param>
		/// <param name="previousStylusPoints">O <see cref="T:System.Windows.Input.StylusPointCollection" /> substituído.</param>
		// Token: 0x06001C6D RID: 7277 RVA: 0x00073A5C File Offset: 0x00072E5C
		public StylusPointsReplacedEventArgs(StylusPointCollection newStylusPoints, StylusPointCollection previousStylusPoints)
		{
			if (newStylusPoints == null)
			{
				throw new ArgumentNullException("newStylusPoints");
			}
			if (previousStylusPoints == null)
			{
				throw new ArgumentNullException("previousStylusPoints");
			}
			this._newStylusPoints = newStylusPoints;
			this._previousStylusPoints = previousStylusPoints;
		}

		/// <summary>Obtém o novo <see cref="T:System.Windows.Input.StylusPointCollection" /> para o <see cref="T:System.Windows.Ink.Stroke" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Input.StylusPointCollection" /> novo para o <see cref="T:System.Windows.Ink.Stroke" />.</returns>
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x00073A9C File Offset: 0x00072E9C
		public StylusPointCollection NewStylusPoints
		{
			get
			{
				return this._newStylusPoints;
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.StylusPointCollection" /> substituído.</summary>
		/// <returns>O <see cref="T:System.Windows.Input.StylusPointCollection" /> substituído.</returns>
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001C6F RID: 7279 RVA: 0x00073AB0 File Offset: 0x00072EB0
		public StylusPointCollection PreviousStylusPoints
		{
			get
			{
				return this._previousStylusPoints;
			}
		}

		// Token: 0x04000F82 RID: 3970
		private StylusPointCollection _newStylusPoints;

		// Token: 0x04000F83 RID: 3971
		private StylusPointCollection _previousStylusPoints;
	}
}
