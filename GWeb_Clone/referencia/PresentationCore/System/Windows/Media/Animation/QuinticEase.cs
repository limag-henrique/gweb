using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma função de easing que cria uma animação que é acelerada e/ou desacelerada usando a fórmula f(t) = t5.</summary>
	// Token: 0x0200058A RID: 1418
	public class QuinticEase : EasingFunctionBase
	{
		/// <summary>Fornece a parte lógica da função de easing que você pode substituir para produzir o modo <see cref="F:System.Windows.Media.Animation.EasingMode.EaseIn" /> da função de easing personalizada.</summary>
		/// <param name="normalizedTime">Tempo normalizado (andamento) da animação.</param>
		/// <returns>Um duplo que representa o progresso transformado.</returns>
		// Token: 0x06004181 RID: 16769 RVA: 0x001016F8 File Offset: 0x00100AF8
		protected override double EaseInCore(double normalizedTime)
		{
			return normalizedTime * normalizedTime * normalizedTime * normalizedTime * normalizedTime;
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06004182 RID: 16770 RVA: 0x00101710 File Offset: 0x00100B10
		protected override Freezable CreateInstanceCore()
		{
			return new QuinticEase();
		}
	}
}
