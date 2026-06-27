using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma função de easing que cria uma animação que é acelerada e/ou desacelerada usando a fórmula f(t) = t2</summary>
	// Token: 0x0200058D RID: 1421
	public class QuadraticEase : EasingFunctionBase
	{
		/// <summary>Fornece a parte lógica da função de easing que você pode substituir para produzir o modo <see cref="F:System.Windows.Media.Animation.EasingMode.EaseIn" /> da função de easing personalizada.</summary>
		/// <param name="normalizedTime">Tempo normalizado (andamento) da animação.</param>
		/// <returns>Um duplo que representa o progresso transformado.</returns>
		// Token: 0x0600418A RID: 16778 RVA: 0x001017B0 File Offset: 0x00100BB0
		protected override double EaseInCore(double normalizedTime)
		{
			return normalizedTime * normalizedTime;
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x0600418B RID: 16779 RVA: 0x001017C0 File Offset: 0x00100BC0
		protected override Freezable CreateInstanceCore()
		{
			return new QuadraticEase();
		}
	}
}
