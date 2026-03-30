using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Esse tipo é compatível com a infraestrutura WPF e não se destina a ser usado diretamente no código. Para fazer com que uma classe possa ser animada, ela deve ser derivada de <see cref="T:System.Windows.UIElement" />, <see cref="T:System.Windows.ContentElement" /> ou <see cref="T:System.Windows.Media.Animation.Animatable" />.</summary>
	// Token: 0x0200056B RID: 1387
	public interface IAnimatable
	{
		/// <summary>Aplica o efeito de um determinado <see cref="T:System.Windows.Media.Animation.AnimationClock" /> a uma propriedade de dependência.</summary>
		/// <param name="dp">O <see cref="T:System.Windows.DependencyProperty" /> a ser animado.</param>
		/// <param name="clock">O <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que anima a propriedade.</param>
		// Token: 0x06004065 RID: 16485
		void ApplyAnimationClock(DependencyProperty dp, AnimationClock clock);

		/// <summary>Aplica o efeito de um determinado <see cref="T:System.Windows.Media.Animation.AnimationClock" /> a uma propriedade de dependência. O efeito do novo <see cref="T:System.Windows.Media.Animation.AnimationClock" /> em quaisquer animações atuais é determinado pelo valor do parâmetro <paramref name="handoffBehavior" />.</summary>
		/// <param name="dp">O <see cref="T:System.Windows.DependencyProperty" /> a ser animado.</param>
		/// <param name="clock">O <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que anima a propriedade.</param>
		/// <param name="handoffBehavior">Determina como o novo <see cref="T:System.Windows.Media.Animation.AnimationClock" /> fará a transição de quaisquer animações atuais na propriedade ou as afetará.</param>
		// Token: 0x06004066 RID: 16486
		void ApplyAnimationClock(DependencyProperty dp, AnimationClock clock, HandoffBehavior handoffBehavior);

		/// <summary>Inicia uma sequência de animação para o objeto <see cref="T:System.Windows.DependencyProperty" /> com base no <see cref="T:System.Windows.Media.Animation.AnimationTimeline" /> especificado.</summary>
		/// <param name="dp">O objeto a ser animado.</param>
		/// <param name="animation">A linha do tempo com a funcionalidade necessária para animar a propriedade.</param>
		// Token: 0x06004067 RID: 16487
		void BeginAnimation(DependencyProperty dp, AnimationTimeline animation);

		/// <summary>Inicia uma sequência de animação para o <see cref="T:System.Windows.DependencyProperty" />.object com base no <see cref="T:System.Windows.Media.Animation.AnimationTimeline" /> e no <see cref="T:System.Windows.Media.Animation.HandoffBehavior" /> especificados.</summary>
		/// <param name="dp">O objeto a ser animado.</param>
		/// <param name="animation">A linha de tempo com a funcionalidade necessária para personalizar a nova animação.</param>
		/// <param name="handoffBehavior">O objeto que especifica a maneira de interagir com todas as sequências de animação relevantes.</param>
		// Token: 0x06004068 RID: 16488
		void BeginAnimation(DependencyProperty dp, AnimationTimeline animation, HandoffBehavior handoffBehavior);

		/// <summary>Obtém um valor que indica se essa instância tem propriedades animadas.</summary>
		/// <returns>
		///   <see langword="true" /> se um <see cref="T:System.Windows.Media.Animation.Clock" /> for associado a pelo menos uma das propriedades do objeto atual; caso contrário <see langword="false" />.</returns>
		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06004069 RID: 16489
		bool HasAnimatedProperties { get; }

		/// <summary>Recupera o valor base do objeto <see cref="T:System.Windows.DependencyProperty" /> especificado.</summary>
		/// <param name="dp">O objeto para o qual o valor base está sendo solicitado.</param>
		/// <returns>O objeto que representa o valor base de <paramref name="Dp" />.</returns>
		// Token: 0x0600406A RID: 16490
		object GetAnimationBaseValue(DependencyProperty dp);
	}
}
