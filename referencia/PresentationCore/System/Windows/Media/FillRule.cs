using System;

namespace System.Windows.Media
{
	/// <summary>Especifica como a interseção de áreas de objetos <see cref="T:System.Windows.Media.PathFigure" /> contidos em um <see cref="T:System.Windows.Media.Geometry" /> são combinados para formar a área do <see cref="T:System.Windows.Media.Geometry" />.</summary>
	// Token: 0x020003AA RID: 938
	public enum FillRule
	{
		/// <summary>Regra que determina se um ponto está na região de preenchimento desenhando um raio desse ponto até o infinito em qualquer direção e contando o número de segmentos de caminho dentro da forma especificada que o raio cruza. Se esse número for ímpar, o ponto estará dentro, se for par, o ponto estará fora.</summary>
		// Token: 0x04001141 RID: 4417
		EvenOdd,
		/// <summary>Regra que determina se um ponto está na região de preenchimento do caminho desenhando um raio desse ponto até o infinito em qualquer direção e examinando então os lugares em que um segmento da forma cruza o raio. Começando com uma contagem de zero, adicione um cada vez que um segmento cruzar o raio da esquerda para a direita e subtraia um cada vez que um segmento de linha cruzar o raio da direita para a esquerda. Após a contagem de cruzamentos, se o resultado for zero, o ponto estará fora do caminho. Caso contrário, ele estará dentro.</summary>
		// Token: 0x04001142 RID: 4418
		Nonzero
	}
}
