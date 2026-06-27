using System;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Define um tipo de conteúdo de texto no qual a medição, os teste de acertos e o desenho de todo o conteúdo são realizados como um todo.</summary>
	// Token: 0x0200059F RID: 1439
	public abstract class TextEmbeddedObject : TextRun
	{
		/// <summary>Obtém a condição de quebra de linha antes do objeto de texto.</summary>
		/// <returns>Um valor enumerado de <see cref="T:System.Windows.LineBreakCondition" />.</returns>
		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06004210 RID: 16912
		public abstract LineBreakCondition BreakBefore { get; }

		/// <summary>Obtém a condição de quebra de linha após o objeto de texto.</summary>
		/// <returns>Um valor enumerado de <see cref="T:System.Windows.LineBreakCondition" />.</returns>
		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06004211 RID: 16913
		public abstract LineBreakCondition BreakAfter { get; }

		/// <summary>Determina se o objeto de texto tem um tamanho fixo, independentemente de onde ele é colocado dentro de uma linha.</summary>
		/// <returns>
		///   <see langword="true" /> Se o objeto de texto tem um tamanho fixo; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x06004212 RID: 16914
		public abstract bool HasFixedSize { get; }

		/// <summary>Obtém as métricas de medida do objeto de texto que se ajustarão na largura restante do parágrafo especificada.</summary>
		/// <param name="remainingParagraphWidth">Um <see cref="T:System.Double" /> que representa a largura restante do parágrafo.</param>
		/// <returns>Um valor <see cref="T:System.Windows.Media.TextFormatting.TextEmbeddedObjectMetrics" /> que representa as métricas do objeto de texto.</returns>
		// Token: 0x06004213 RID: 16915
		public abstract TextEmbeddedObjectMetrics Format(double remainingParagraphWidth);

		/// <summary>Obtém a caixa delimitadora de computação do objeto de texto.</summary>
		/// <param name="rightToLeft">Um valor <see cref="T:System.Boolean" /> que determina se o objeto de texto é desenhado da direita para a esquerda.</param>
		/// <param name="sideways">Um <see cref="T:System.Boolean" /> valor que determina se o objeto de texto é desenhado com seu lado paralelo à linha de base.</param>
		/// <returns>Um valor <see cref="T:System.Windows.Rect" /> que representa o tamanho da caixa delimitadora do objeto de texto.</returns>
		// Token: 0x06004214 RID: 16916
		public abstract Rect ComputeBoundingBox(bool rightToLeft, bool sideways);

		/// <summary>Desenha o objeto de texto.</summary>
		/// <param name="drawingContext">O <see cref="T:System.Windows.Media.DrawingContext" /> a ser usado para renderizar o objeto de texto.</param>
		/// <param name="origin">O valor <see cref="T:System.Windows.Point" /> que representa a origem em que o objeto de texto é desenhado.</param>
		/// <param name="rightToLeft">Um valor <see cref="T:System.Boolean" /> que determina se o objeto de texto é desenhado da direita para a esquerda.</param>
		/// <param name="sideways">Um <see cref="T:System.Boolean" /> valor que determina se o objeto de texto é desenhado com seu lado paralelo à linha de base.</param>
		// Token: 0x06004215 RID: 16917
		public abstract void Draw(DrawingContext drawingContext, Point origin, bool rightToLeft, bool sideways);
	}
}
