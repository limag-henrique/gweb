using System;

namespace System.Windows.Input
{
	/// <summary>Contém dados de transformação acumulados quando ocorrem eventos de manipulação.</summary>
	// Token: 0x02000272 RID: 626
	public class ManipulationDelta
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.ManipulationDelta" />.</summary>
		/// <param name="translation">O movimento linear da manipulação em unidades independentes de dispositivo (1/96 polegada por unidade).</param>
		/// <param name="rotation">A rotação da manipulação em graus.</param>
		/// <param name="scale">A quantidade de redimensionamento de uma manipulação como um multiplicador.</param>
		/// <param name="expansion">A quantidade de manipulação foi redimensionada em unidades independentes de dispositivo (1/96 polegada por unidade).</param>
		// Token: 0x060011CA RID: 4554 RVA: 0x00042C2C File Offset: 0x0004202C
		public ManipulationDelta(Vector translation, double rotation, Vector scale, Vector expansion)
		{
			this.Translation = translation;
			this.Rotation = rotation;
			this.Scale = scale;
			this.Expansion = expansion;
		}

		/// <summary>Obtém ou define o movimento linear da manipulação.</summary>
		/// <returns>O movimento linear da manipulação em unidades independentes de dispositivo (1/96 polegada por unidade).</returns>
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x00042C5C File Offset: 0x0004205C
		// (set) Token: 0x060011CC RID: 4556 RVA: 0x00042C70 File Offset: 0x00042070
		public Vector Translation { get; private set; }

		/// <summary>Obtém ou define a rotação da manipulação em graus.</summary>
		/// <returns>A rotação da manipulação em graus.</returns>
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x00042C84 File Offset: 0x00042084
		// (set) Token: 0x060011CE RID: 4558 RVA: 0x00042C98 File Offset: 0x00042098
		public double Rotation { get; private set; }

		/// <summary>Obtém ou define o valor que a manipulação redimensionou como um multiplicador.</summary>
		/// <returns>O valor da manipulação foi redimensionado.</returns>
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x00042CAC File Offset: 0x000420AC
		// (set) Token: 0x060011D0 RID: 4560 RVA: 0x00042CC0 File Offset: 0x000420C0
		public Vector Scale { get; private set; }

		/// <summary>Obtém ou define a quantidade de redimensionamento da manipulação em unidades independentes de dispositivo (1/96 polegada por unidade).</summary>
		/// <returns>A quantidade de manipulação foi redimensionada em unidades independentes de dispositivo (1/96 polegada por unidade).</returns>
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x00042CD4 File Offset: 0x000420D4
		// (set) Token: 0x060011D2 RID: 4562 RVA: 0x00042CE8 File Offset: 0x000420E8
		public Vector Expansion { get; private set; }
	}
}
