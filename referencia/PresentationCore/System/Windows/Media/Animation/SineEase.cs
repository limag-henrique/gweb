using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma função de easing que cria uma animação que é acelerada e/ou desacelerada usando uma fórmula de seno.</summary>
	// Token: 0x02000583 RID: 1411
	public class SineEase : EasingFunctionBase
	{
		/// <summary>Fornece a parte lógica da função de easing que você pode substituir para produzir o modo <see cref="F:System.Windows.Media.Animation.EasingMode.EaseIn" /> da função de easing personalizada.</summary>
		/// <param name="normalizedTime">Tempo normalizado (andamento) da animação.</param>
		/// <returns>Um duplo que representa o progresso transformado.</returns>
		// Token: 0x06004159 RID: 16729 RVA: 0x00100F80 File Offset: 0x00100380
		protected override double EaseInCore(double normalizedTime)
		{
			return 1.0 - Math.Sin(1.5707963267948966 * (1.0 - normalizedTime));
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x0600415A RID: 16730 RVA: 0x00100FB4 File Offset: 0x001003B4
		protected override Freezable CreateInstanceCore()
		{
			return new SineEase();
		}
	}
}
