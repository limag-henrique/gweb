using System;
using MS.Internal;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma função de easing que cria um efeito de balanço animado.</summary>
	// Token: 0x02000584 RID: 1412
	public class BounceEase : EasingFunctionBase
	{
		/// <summary>Obtém ou define o número de bounces.</summary>
		/// <returns>O número de bounces. O valor deve ser maior que ou igual a zero. Resolvem valores negativos como zero. O padrão é 3.</returns>
		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x0600415D RID: 16733 RVA: 0x00100FF0 File Offset: 0x001003F0
		// (set) Token: 0x0600415E RID: 16734 RVA: 0x00101010 File Offset: 0x00100410
		public int Bounces
		{
			get
			{
				return (int)base.GetValue(BounceEase.BouncesProperty);
			}
			set
			{
				base.SetValueInternal(BounceEase.BouncesProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que especifica a flexibilidade da animação de bounce. Valores baixos dessa propriedade resultam em bounces com pouca perda de altura entre bounces (mais animado) enquanto valores altos resultam em bounces reduzidos (menos animado).</summary>
		/// <returns>O valor que especifica a flexibilidade da animação de bounce. Esse valor deve ser positivo. O valor padrão é 2.</returns>
		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x0600415F RID: 16735 RVA: 0x00101030 File Offset: 0x00100430
		// (set) Token: 0x06004160 RID: 16736 RVA: 0x00101050 File Offset: 0x00100450
		public double Bounciness
		{
			get
			{
				return (double)base.GetValue(BounceEase.BouncinessProperty);
			}
			set
			{
				base.SetValueInternal(BounceEase.BouncinessProperty, value);
			}
		}

		/// <summary>Fornece a parte lógica da função de easing que você pode substituir para produzir o modo <see cref="F:System.Windows.Media.Animation.EasingMode.EaseIn" /> da função de easing personalizada.</summary>
		/// <param name="normalizedTime">Tempo normalizado (andamento) da animação.</param>
		/// <returns>Um duplo que representa o progresso transformado.</returns>
		// Token: 0x06004161 RID: 16737 RVA: 0x00101070 File Offset: 0x00100470
		protected override double EaseInCore(double normalizedTime)
		{
			double num = Math.Max(0.0, (double)this.Bounces);
			double num2 = this.Bounciness;
			if (num2 < 1.0 || DoubleUtil.IsOne(num2))
			{
				num2 = 1.001;
			}
			double num3 = Math.Pow(num2, num);
			double num4 = 1.0 - num2;
			double num5 = (1.0 - num3) / num4 + num3 * 0.5;
			double num6 = normalizedTime * num5;
			double d = Math.Log(-num6 * (1.0 - num2) + 1.0, num2);
			double num7 = Math.Floor(d);
			double y = num7 + 1.0;
			double num8 = (1.0 - Math.Pow(num2, num7)) / (num4 * num5);
			double num9 = (1.0 - Math.Pow(num2, y)) / (num4 * num5);
			double num10 = (num8 + num9) * 0.5;
			double num11 = normalizedTime - num10;
			double num12 = num10 - num8;
			double num13 = Math.Pow(1.0 / num2, num - num7);
			return -num13 / (num12 * num12) * (num11 - num12) * (num11 + num12);
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06004162 RID: 16738 RVA: 0x001011A0 File Offset: 0x001005A0
		protected override Freezable CreateInstanceCore()
		{
			return new BounceEase();
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.BounceEase.Bounces" />.</summary>
		// Token: 0x040017EA RID: 6122
		public static readonly DependencyProperty BouncesProperty = DependencyProperty.Register("Bounces", typeof(int), typeof(BounceEase), new PropertyMetadata(3));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.BounceEase.Bounciness" />.</summary>
		// Token: 0x040017EB RID: 6123
		public static readonly DependencyProperty BouncinessProperty = DependencyProperty.Register("Bounciness", typeof(double), typeof(BounceEase), new PropertyMetadata(2.0));
	}
}
