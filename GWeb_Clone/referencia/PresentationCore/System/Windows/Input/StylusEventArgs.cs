using System;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para vários eventos associados à classe <see cref="T:System.Windows.Input.Stylus" />.</summary>
	// Token: 0x020002B7 RID: 695
	public class StylusEventArgs : InputEventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.StylusEventArgs" />.</summary>
		/// <param name="stylus">A caneta a ser associada ao evento.</param>
		/// <param name="timestamp">A hora em que o evento ocorre.</param>
		// Token: 0x06001494 RID: 5268 RVA: 0x0004BA04 File Offset: 0x0004AE04
		public StylusEventArgs(StylusDevice stylus, int timestamp) : base(stylus, timestamp)
		{
			if (stylus == null)
			{
				throw new ArgumentNullException("stylus");
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.StylusDevice" /> que representa a caneta.</summary>
		/// <returns>O <see cref="T:System.Windows.Input.StylusDevice" /> que representa a caneta.</returns>
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x0004BA28 File Offset: 0x0004AE28
		public StylusDevice StylusDevice
		{
			get
			{
				return (StylusDevice)base.Device;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x0004BA40 File Offset: 0x0004AE40
		internal StylusDeviceBase StylusDeviceImpl
		{
			get
			{
				return ((StylusDevice)base.Device).StylusDeviceImpl;
			}
		}

		/// <summary>Obtém a posição da caneta.</summary>
		/// <param name="relativeTo">O <see cref="T:System.Windows.IInputElement" /> para o qual as coordenadas (x, y) são mapeadas.</param>
		/// <returns>Um <see cref="T:System.Windows.Point" /> que representa a posição da caneta com base nas coordenadas de <paramref name="relativeTo" />.</returns>
		// Token: 0x06001497 RID: 5271 RVA: 0x0004BA60 File Offset: 0x0004AE60
		public Point GetPosition(IInputElement relativeTo)
		{
			return this.StylusDevice.GetPosition(relativeTo);
		}

		/// <summary>Obtém um valor que indica se a caneta está próxima ao digitalizador, mas não o está tocando.</summary>
		/// <returns>
		///   <see langword="true" /> Se a caneta está em proximidade com, mas não tocar, digitalizador; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x0004BA7C File Offset: 0x0004AE7C
		public bool InAir
		{
			get
			{
				return this.StylusDevice.InAir;
			}
		}

		/// <summary>Obtém um valor que indica se a caneta está invertida.</summary>
		/// <returns>
		///   <see langword="true" /> Se a caneta está invertida; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001499 RID: 5273 RVA: 0x0004BA94 File Offset: 0x0004AE94
		public bool Inverted
		{
			get
			{
				return this.StylusDevice.Inverted;
			}
		}

		/// <summary>Retorna um <see cref="T:System.Windows.Input.StylusPointCollection" /> que contém objetos <see cref="T:System.Windows.Input.StylusPoint" /> em relação ao elemento de entrada especificado.</summary>
		/// <param name="relativeTo">O <see cref="T:System.Windows.IInputElement" /> para o qual as coordenadas (x, y) em <see cref="T:System.Windows.Input.StylusPointCollection" /> são mapeadas.</param>
		/// <returns>Um <see cref="T:System.Windows.Input.StylusPointCollection" /> que contém os objetos <see cref="T:System.Windows.Input.StylusPoint" /> coletados no evento.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="relativeTo" /> não é <see cref="T:System.Windows.UIElement" /> nem <see cref="T:System.Windows.FrameworkContentElement" />.</exception>
		// Token: 0x0600149A RID: 5274 RVA: 0x0004BAAC File Offset: 0x0004AEAC
		public StylusPointCollection GetStylusPoints(IInputElement relativeTo)
		{
			return this.StylusDevice.GetStylusPoints(relativeTo);
		}

		/// <summary>Retorna um <see cref="T:System.Windows.Input.StylusPointCollection" /> que usa o <see cref="T:System.Windows.Input.StylusPointDescription" /> especificado e contém objetos <see cref="T:System.Windows.Input.StylusPoint" /> com relação ao elemento de entrada especificado.</summary>
		/// <param name="relativeTo">O <see cref="T:System.Windows.IInputElement" /> para o qual as coordenadas (x, y) em <see cref="T:System.Windows.Input.StylusPointCollection" /> são mapeadas.</param>
		/// <param name="subsetToReformatTo">O <see cref="T:System.Windows.Input.StylusPointDescription" /> a ser usado pelo <see cref="T:System.Windows.Input.StylusPointCollection" />.</param>
		/// <returns>Um <see cref="T:System.Windows.Input.StylusPointCollection" /> que contém os objetos <see cref="T:System.Windows.Input.StylusPoint" /> coletados durante um evento.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="relativeTo" /> não é <see cref="T:System.Windows.UIElement" /> nem <see cref="T:System.Windows.FrameworkContentElement" />.</exception>
		// Token: 0x0600149B RID: 5275 RVA: 0x0004BAC8 File Offset: 0x0004AEC8
		public StylusPointCollection GetStylusPoints(IInputElement relativeTo, StylusPointDescription subsetToReformatTo)
		{
			return this.StylusDevice.GetStylusPoints(relativeTo, subsetToReformatTo);
		}

		/// <summary>Invoca manipuladores de eventos em uma forma específica de tipo, o que pode aumentar a eficiência do sistema de eventos.</summary>
		/// <param name="genericHandler">O manipulador genérico a ser chamado de uma forma específica ao tipo.</param>
		/// <param name="genericTarget">O destino no qual chamar o manipulador.</param>
		// Token: 0x0600149C RID: 5276 RVA: 0x0004BAE4 File Offset: 0x0004AEE4
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			StylusEventHandler stylusEventHandler = (StylusEventHandler)genericHandler;
			stylusEventHandler(genericTarget, this);
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x0004BB00 File Offset: 0x0004AF00
		// (set) Token: 0x0600149E RID: 5278 RVA: 0x0004BB14 File Offset: 0x0004AF14
		internal RawStylusInputReport InputReport
		{
			get
			{
				return this._inputReport;
			}
			set
			{
				this._inputReport = value;
			}
		}

		// Token: 0x04000B04 RID: 2820
		private RawStylusInputReport _inputReport;
	}
}
