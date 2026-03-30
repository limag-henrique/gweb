using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma função de easing que cria uma animação que é acelerada e/ou desacelerada usando a fórmula f(t) = tp em que p é igual à propriedade <see cref="P:System.Windows.Media.Animation.PowerEase.Power" />.</summary>
	// Token: 0x02000589 RID: 1417
	public class PowerEase : EasingFunctionBase
	{
		/// <summary>Obtém ou define a potência exponencial da interpolação de animação. Por exemplo, um valor de 7 criará uma curva de interpolação de animação que seguirá a fórmula f(t) = t7.</summary>
		/// <returns>A potência exponencial da interpolação de animação. Esse valor deve ser maior ou igual a 0. O padrão é 2.</returns>
		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x0600417C RID: 16764 RVA: 0x00101634 File Offset: 0x00100A34
		// (set) Token: 0x0600417D RID: 16765 RVA: 0x00101654 File Offset: 0x00100A54
		public double Power
		{
			get
			{
				return (double)base.GetValue(PowerEase.PowerProperty);
			}
			set
			{
				base.SetValueInternal(PowerEase.PowerProperty, value);
			}
		}

		/// <summary>Fornece a parte lógica da função de easing que você pode substituir para produzir o modo <see cref="F:System.Windows.Media.Animation.EasingMode.EaseIn" /> da função de easing personalizada.</summary>
		/// <param name="normalizedTime">Tempo normalizado (andamento) da animação.</param>
		/// <returns>Um duplo que representa o progresso transformado.</returns>
		// Token: 0x0600417E RID: 16766 RVA: 0x00101674 File Offset: 0x00100A74
		protected override double EaseInCore(double normalizedTime)
		{
			double y = Math.Max(0.0, this.Power);
			return Math.Pow(normalizedTime, y);
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x0600417F RID: 16767 RVA: 0x001016A0 File Offset: 0x00100AA0
		protected override Freezable CreateInstanceCore()
		{
			return new PowerEase();
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.PowerEase.Power" />.</summary>
		// Token: 0x040017F0 RID: 6128
		public static readonly DependencyProperty PowerProperty = DependencyProperty.Register("Power", typeof(double), typeof(PowerEase), new PropertyMetadata(2.0));
	}
}
