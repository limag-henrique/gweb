using System;

namespace System.Windows.Media
{
	/// <summary>Argumentos necessários para o evento <see cref="E:System.Windows.Media.CompositionTarget.Rendering" />.</summary>
	// Token: 0x02000436 RID: 1078
	public class RenderingEventArgs : EventArgs
	{
		// Token: 0x06002C21 RID: 11297 RVA: 0x000B04D8 File Offset: 0x000AF8D8
		internal RenderingEventArgs(TimeSpan renderingTime)
		{
			this._renderingTime = renderingTime;
		}

		/// <summary>Obtém o tempo de destino estimado no qual o próximo quadro de uma animação será renderizado.</summary>
		/// <returns>O tempo estimado de destino no qual o próximo quadro de uma animação será renderizado.</returns>
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06002C22 RID: 11298 RVA: 0x000B04F4 File Offset: 0x000AF8F4
		public TimeSpan RenderingTime
		{
			get
			{
				return this._renderingTime;
			}
		}

		// Token: 0x0400141F RID: 5151
		private TimeSpan _renderingTime;
	}
}
