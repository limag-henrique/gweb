using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma função de easing que cria uma animação que é acelerada e/ou desacelerada usando a fórmula f(t) = t3.</summary>
	// Token: 0x0200058C RID: 1420
	public class CubicEase : EasingFunctionBase
	{
		/// <summary>Fornece a parte lógica da função de easing que você pode substituir para produzir o modo <see cref="F:System.Windows.Media.Animation.EasingMode.EaseIn" /> da função de easing personalizada.</summary>
		/// <param name="normalizedTime">Tempo normalizado (andamento) da animação.</param>
		/// <returns>Um duplo que representa o progresso transformado.</returns>
		// Token: 0x06004187 RID: 16775 RVA: 0x00101774 File Offset: 0x00100B74
		protected override double EaseInCore(double normalizedTime)
		{
			return normalizedTime * normalizedTime * normalizedTime;
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06004188 RID: 16776 RVA: 0x00101788 File Offset: 0x00100B88
		protected override Freezable CreateInstanceCore()
		{
			return new CubicEase();
		}
	}
}
