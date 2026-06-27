using System;
using MS.Internal;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma função de facilitação que cria uma animação que se parece com uma mola oscilando para frente e para trás até parar.</summary>
	// Token: 0x02000587 RID: 1415
	public class ElasticEase : EasingFunctionBase
	{
		/// <summary>Obtém ou define o número de vezes que o destino desliza para frente e para trás no destino da animação.</summary>
		/// <returns>O número de vezes que o destino desliza a frente e para trás no destino da animação. Esse valor deve ser maior ou igual a 0. O padrão é 3.</returns>
		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x0600416E RID: 16750 RVA: 0x001013A0 File Offset: 0x001007A0
		// (set) Token: 0x0600416F RID: 16751 RVA: 0x001013C0 File Offset: 0x001007C0
		public int Oscillations
		{
			get
			{
				return (int)base.GetValue(ElasticEase.OscillationsProperty);
			}
			set
			{
				base.SetValueInternal(ElasticEase.OscillationsProperty, value);
			}
		}

		/// <summary>Obtém ou define a rigidez da mola. Quanto menor o valor de Springiness for, mais rígida a mola é e mais rápido a elasticidade diminui em intensidade em cada oscilação.</summary>
		/// <returns>Um número positivo que especifica a rigidez da mola. O valor padrão é 3.</returns>
		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06004170 RID: 16752 RVA: 0x001013E0 File Offset: 0x001007E0
		// (set) Token: 0x06004171 RID: 16753 RVA: 0x00101400 File Offset: 0x00100800
		public double Springiness
		{
			get
			{
				return (double)base.GetValue(ElasticEase.SpringinessProperty);
			}
			set
			{
				base.SetValueInternal(ElasticEase.SpringinessProperty, value);
			}
		}

		/// <summary>Fornece a parte lógica da função de easing que você pode substituir para produzir o modo <see cref="F:System.Windows.Media.Animation.EasingMode.EaseIn" /> da função de easing personalizada.</summary>
		/// <param name="normalizedTime">Tempo normalizado (andamento) da animação.</param>
		/// <returns>Um duplo que representa o progresso transformado.</returns>
		// Token: 0x06004172 RID: 16754 RVA: 0x00101420 File Offset: 0x00100820
		protected override double EaseInCore(double normalizedTime)
		{
			double num = Math.Max(0.0, (double)this.Oscillations);
			double num2 = Math.Max(0.0, this.Springiness);
			double num3;
			if (DoubleUtil.IsZero(num2))
			{
				num3 = normalizedTime;
			}
			else
			{
				num3 = (Math.Exp(num2 * normalizedTime) - 1.0) / (Math.Exp(num2) - 1.0);
			}
			return num3 * Math.Sin((6.2831853071795862 * num + 1.5707963267948966) * normalizedTime);
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06004173 RID: 16755 RVA: 0x001014A8 File Offset: 0x001008A8
		protected override Freezable CreateInstanceCore()
		{
			return new ElasticEase();
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.ElasticEase.Oscillations" />.</summary>
		// Token: 0x040017ED RID: 6125
		public static readonly DependencyProperty OscillationsProperty = DependencyProperty.Register("Oscillations", typeof(int), typeof(ElasticEase), new PropertyMetadata(3));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.ElasticEase.Springiness" />.</summary>
		// Token: 0x040017EE RID: 6126
		public static readonly DependencyProperty SpringinessProperty = DependencyProperty.Register("Springiness", typeof(double), typeof(ElasticEase), new PropertyMetadata(3.0));
	}
}
