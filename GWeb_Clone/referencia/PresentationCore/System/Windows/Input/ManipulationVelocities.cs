using System;

namespace System.Windows.Input
{
	/// <summary>Descreve a velocidade à qual a manipulações ocorre.</summary>
	// Token: 0x0200027B RID: 635
	public class ManipulationVelocities
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.ManipulationVelocities" />.</summary>
		/// <param name="linearVelocity">A velocidade do movimento linear em unidades independentes de dispositivo (1/96 polegada por unidade) por milissegundo.</param>
		/// <param name="angularVelocity">A velocidade de rotação em graus por milissegundo.</param>
		/// <param name="expansionVelocity">A taxa à qual a manipulação é redimensionada em unidades independentes de dispositivo (1/96 polegada por unidade) por milissegundo.</param>
		// Token: 0x06001278 RID: 4728 RVA: 0x00044E68 File Offset: 0x00044268
		public ManipulationVelocities(Vector linearVelocity, double angularVelocity, Vector expansionVelocity)
		{
			this.LinearVelocity = linearVelocity;
			this.AngularVelocity = angularVelocity;
			this.ExpansionVelocity = expansionVelocity;
		}

		/// <summary>Obtém ou define a velocidade do movimento linear.</summary>
		/// <returns>A velocidade do movimento linear em unidades independentes de dispositivo (1/96 polegada por unidade) por milissegundo.</returns>
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x00044E90 File Offset: 0x00044290
		// (set) Token: 0x0600127A RID: 4730 RVA: 0x00044EA4 File Offset: 0x000442A4
		public Vector LinearVelocity { get; private set; }

		/// <summary>Obtém ou define a velocidade de rotação.</summary>
		/// <returns>A velocidade de rotação em graus por milissegundo.</returns>
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x00044EB8 File Offset: 0x000442B8
		// (set) Token: 0x0600127C RID: 4732 RVA: 0x00044ECC File Offset: 0x000442CC
		public double AngularVelocity { get; private set; }

		/// <summary>Obtém ou define a taxa à qual a manipulação é redimensionada.</summary>
		/// <returns>A taxa à qual a manipulação é redimensionada em unidades independentes de dispositivo (1/96 polegada por unidade) por milissegundo.</returns>
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x00044EE0 File Offset: 0x000442E0
		// (set) Token: 0x0600127E RID: 4734 RVA: 0x00044EF4 File Offset: 0x000442F4
		public Vector ExpansionVelocity { get; private set; }
	}
}
