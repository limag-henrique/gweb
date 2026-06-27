using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa uma função de facilitação que retrai o movimento de uma animação um pouco antes do início da animação no caminho indicado.</summary>
	// Token: 0x02000586 RID: 1414
	public class BackEase : EasingFunctionBase
	{
		/// <summary>Obtém ou define a amplitude da retração associada a uma animação <see cref="T:System.Windows.Media.Animation.BackEase" />.</summary>
		/// <returns>A amplitude da retração associada com um <see cref="T:System.Windows.Media.Animation.BackEase" /> animação. Esse valor deve ser maior ou igual a 0.  
		/// O valor padrão é 1.</returns>
		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06004168 RID: 16744 RVA: 0x001012AC File Offset: 0x001006AC
		// (set) Token: 0x06004169 RID: 16745 RVA: 0x001012CC File Offset: 0x001006CC
		public double Amplitude
		{
			get
			{
				return (double)base.GetValue(BackEase.AmplitudeProperty);
			}
			set
			{
				base.SetValueInternal(BackEase.AmplitudeProperty, value);
			}
		}

		/// <summary>Fornece a parte lógica da função de easing que você pode substituir para produzir o modo <see cref="F:System.Windows.Media.Animation.EasingMode.EaseIn" /> da função de easing personalizada.</summary>
		/// <param name="normalizedTime">Tempo normalizado (andamento) da animação.</param>
		/// <returns>Um duplo que representa o progresso transformado.</returns>
		// Token: 0x0600416A RID: 16746 RVA: 0x001012EC File Offset: 0x001006EC
		protected override double EaseInCore(double normalizedTime)
		{
			double num = Math.Max(0.0, this.Amplitude);
			return Math.Pow(normalizedTime, 3.0) - normalizedTime * num * Math.Sin(3.1415926535897931 * normalizedTime);
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x0600416B RID: 16747 RVA: 0x00101334 File Offset: 0x00100734
		protected override Freezable CreateInstanceCore()
		{
			return new BackEase();
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.BackEase.Amplitude" />.</summary>
		// Token: 0x040017EC RID: 6124
		public static readonly DependencyProperty AmplitudeProperty = DependencyProperty.Register("Amplitude", typeof(double), typeof(BackEase), new PropertyMetadata(1.0));
	}
}
