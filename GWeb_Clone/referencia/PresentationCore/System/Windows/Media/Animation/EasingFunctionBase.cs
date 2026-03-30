using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Fornece a classe base para todas as funções de easing.</summary>
	// Token: 0x0200058F RID: 1423
	public abstract class EasingFunctionBase : Freezable, IEasingFunction
	{
		/// <summary>Obtém ou define um valor que especifica como a animação interpola.</summary>
		/// <returns>Um dos valores de enumeração que especifica como a animação interpola. O padrão é <see cref="F:System.Windows.Media.Animation.EasingMode.EaseOut" />.</returns>
		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x0600418D RID: 16781 RVA: 0x001017E8 File Offset: 0x00100BE8
		// (set) Token: 0x0600418E RID: 16782 RVA: 0x00101808 File Offset: 0x00100C08
		public EasingMode EasingMode
		{
			get
			{
				return (EasingMode)base.GetValue(EasingFunctionBase.EasingModeProperty);
			}
			set
			{
				base.SetValueInternal(EasingFunctionBase.EasingModeProperty, value);
			}
		}

		/// <summary>Transforma o tempo normalizado para controlar o ritmo de uma animação.</summary>
		/// <param name="normalizedTime">Tempo (progresso) normalizado da animação, que é um valor de 0 a 1.</param>
		/// <returns>Um duplo que representa o progresso transformado.</returns>
		// Token: 0x0600418F RID: 16783 RVA: 0x00101828 File Offset: 0x00100C28
		public double Ease(double normalizedTime)
		{
			switch (this.EasingMode)
			{
			case EasingMode.EaseIn:
				return this.EaseInCore(normalizedTime);
			case EasingMode.EaseOut:
				return 1.0 - this.EaseInCore(1.0 - normalizedTime);
			}
			if (normalizedTime >= 0.5)
			{
				return (1.0 - this.EaseInCore((1.0 - normalizedTime) * 2.0)) * 0.5 + 0.5;
			}
			return this.EaseInCore(normalizedTime * 2.0) * 0.5;
		}

		/// <summary>Fornece a parte lógica da função de easing que você pode substituir para produzir o modo <see cref="F:System.Windows.Media.Animation.EasingMode.EaseIn" /> da função de easing personalizada.</summary>
		/// <param name="normalizedTime">Tempo (progresso) normalizado da animação, que é um valor de 0 a 1.</param>
		/// <returns>Um duplo que representa o progresso transformado.</returns>
		// Token: 0x06004190 RID: 16784
		protected abstract double EaseInCore(double normalizedTime);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.EasingFunctionBase.EasingMode" />.</summary>
		// Token: 0x040017F5 RID: 6133
		public static readonly DependencyProperty EasingModeProperty = DependencyProperty.Register("EasingMode", typeof(EasingMode), typeof(EasingFunctionBase), new PropertyMetadata(EasingMode.EaseOut));
	}
}
