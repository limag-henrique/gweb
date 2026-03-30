using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Define a funcionalidade básica de uma função de easing.</summary>
	// Token: 0x02000590 RID: 1424
	public interface IEasingFunction
	{
		/// <summary>Transforma o tempo normalizado para controlar o ritmo de uma animação.</summary>
		/// <param name="normalizedTime">Tempo normalizado (andamento) da animação.</param>
		/// <returns>O progresso transformado.</returns>
		// Token: 0x06004193 RID: 16787
		double Ease(double normalizedTime);
	}
}
