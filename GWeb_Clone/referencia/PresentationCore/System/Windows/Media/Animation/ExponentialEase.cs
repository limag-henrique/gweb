using System;
using MS.Internal;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma função de easing que cria uma animação que acelera e/ou desacelera usando uma fórmula exponencial.</summary>
	// Token: 0x02000588 RID: 1416
	public class ExponentialEase : EasingFunctionBase
	{
		/// <summary>Obtém ou define o expoente usado para determinar a interpolação da animação.</summary>
		/// <returns>O expoente usado para determinar a interpolação da animação. O padrão é 2.</returns>
		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06004176 RID: 16758 RVA: 0x00101544 File Offset: 0x00100944
		// (set) Token: 0x06004177 RID: 16759 RVA: 0x00101564 File Offset: 0x00100964
		public double Exponent
		{
			get
			{
				return (double)base.GetValue(ExponentialEase.ExponentProperty);
			}
			set
			{
				base.SetValueInternal(ExponentialEase.ExponentProperty, value);
			}
		}

		/// <summary>Fornece a parte lógica da função de easing que você pode substituir para produzir o modo <see cref="F:System.Windows.Media.Animation.EasingMode.EaseIn" /> da função de easing personalizada.</summary>
		/// <param name="normalizedTime">Tempo normalizado (andamento) da animação.</param>
		/// <returns>Um duplo que representa o progresso transformado.</returns>
		// Token: 0x06004178 RID: 16760 RVA: 0x00101584 File Offset: 0x00100984
		protected override double EaseInCore(double normalizedTime)
		{
			double exponent = this.Exponent;
			if (DoubleUtil.IsZero(exponent))
			{
				return normalizedTime;
			}
			return (Math.Exp(exponent * normalizedTime) - 1.0) / (Math.Exp(exponent) - 1.0);
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06004179 RID: 16761 RVA: 0x001015C8 File Offset: 0x001009C8
		protected override Freezable CreateInstanceCore()
		{
			return new ExponentialEase();
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.ExponentialEase.Exponent" />.</summary>
		// Token: 0x040017EF RID: 6127
		public static readonly DependencyProperty ExponentProperty = DependencyProperty.Register("Exponent", typeof(double), typeof(ExponentialEase), new PropertyMetadata(2.0));
	}
}
