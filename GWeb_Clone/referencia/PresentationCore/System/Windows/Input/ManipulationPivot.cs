using System;

namespace System.Windows.Input
{
	/// <summary>Especifica como uma rotação ocorre com um ponto de entrada do usuário.</summary>
	// Token: 0x02000278 RID: 632
	public class ManipulationPivot
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.ManipulationPivot" />.</summary>
		// Token: 0x06001255 RID: 4693 RVA: 0x00044A68 File Offset: 0x00043E68
		public ManipulationPivot()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.ManipulationPivot" /> com o ponto especificado de manipulação de um único ponto.</summary>
		/// <param name="center">O centro de uma manipulação de único ponto.</param>
		/// <param name="radius">A área ao redor do pivô que é usado para determinar quanta rotação e translação ocorre quando um ponto único de contato inicia a manipulação.</param>
		// Token: 0x06001256 RID: 4694 RVA: 0x00044A7C File Offset: 0x00043E7C
		public ManipulationPivot(Point center, double radius)
		{
			this.Center = center;
			this.Radius = radius;
		}

		/// <summary>Obtém ou define o centro de uma manipulação de ponto único.</summary>
		/// <returns>O centro de uma manipulação de único ponto.</returns>
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x00044AA0 File Offset: 0x00043EA0
		// (set) Token: 0x06001258 RID: 4696 RVA: 0x00044AB4 File Offset: 0x00043EB4
		public Point Center { get; set; }

		/// <summary>Obtém ou define a área ao redor do pivô que é usado para determinar quanta rotação e translação ocorre quando um ponto único de contato inicia a manipulação.</summary>
		/// <returns>A área ao redor do pivô que é usado para determinar quanta rotação e translação ocorre quando um ponto único de contato inicia a manipulação.</returns>
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x00044AC8 File Offset: 0x00043EC8
		// (set) Token: 0x0600125A RID: 4698 RVA: 0x00044ADC File Offset: 0x00043EDC
		public double Radius { get; set; }
	}
}
