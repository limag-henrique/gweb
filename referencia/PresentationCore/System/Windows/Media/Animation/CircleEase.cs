using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma função de easing que cria uma animação que acelera e/ou desacelera usando uma função circular.</summary>
	// Token: 0x02000585 RID: 1413
	public class CircleEase : EasingFunctionBase
	{
		/// <summary>Fornece a parte lógica da função de easing que você pode substituir para produzir o modo <see cref="F:System.Windows.Media.Animation.EasingMode.EaseIn" /> da função de easing personalizada.</summary>
		/// <param name="normalizedTime">Tempo normalizado (andamento) da animação.</param>
		/// <returns>Um duplo que representa o progresso transformado.</returns>
		// Token: 0x06004164 RID: 16740 RVA: 0x00101228 File Offset: 0x00100628
		protected override double EaseInCore(double normalizedTime)
		{
			normalizedTime = Math.Max(0.0, Math.Min(1.0, normalizedTime));
			return 1.0 - Math.Sqrt(1.0 - normalizedTime * normalizedTime);
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06004165 RID: 16741 RVA: 0x00101270 File Offset: 0x00100670
		protected override Freezable CreateInstanceCore()
		{
			return new CircleEase();
		}
	}
}
