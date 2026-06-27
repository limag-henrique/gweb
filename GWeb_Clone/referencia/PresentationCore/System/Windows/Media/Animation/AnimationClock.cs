using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Mantém o estado de tempo de execução de um <see cref="T:System.Windows.Media.Animation.AnimationTimeline" /> e processa seus valores de saída.</summary>
	// Token: 0x020004A0 RID: 1184
	public class AnimationClock : Clock
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.AnimationClock" />.</summary>
		/// <param name="animation">A animação que descreve os valores de saída do relógio e os comportamentos de tempo.</param>
		// Token: 0x06003483 RID: 13443 RVA: 0x000D0190 File Offset: 0x000CF590
		protected internal AnimationClock(AnimationTimeline animation) : base(animation)
		{
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Media.Animation.AnimationTimeline" /> que descreve o comportamento do relógio.</summary>
		/// <returns>A animação que descreve o comportamento do relógio.</returns>
		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06003484 RID: 13444 RVA: 0x000D01A4 File Offset: 0x000CF5A4
		public new AnimationTimeline Timeline
		{
			get
			{
				return (AnimationTimeline)base.Timeline;
			}
		}

		/// <summary>Obtém o valor de saída atual do <see cref="T:System.Windows.Media.Animation.AnimationClock" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem fornecido ao relógio se essa animação não tiver seu próprio valor inicial. Se este relógio for o primeiro em uma cadeia de composição, que ele será o valor base da propriedade que está sendo animada; caso contrário, será o valor retornado pelo relógio anterior na cadeia</param>
		/// <param name="defaultDestinationValue">O valor de destino fornecido para o relógio se essa animação não tiver seu próprio valor de destino. Se este relógio for o primeiro em uma cadeia de composição, que ele será o valor base da propriedade que está sendo animada; caso contrário, será o valor retornado pelo relógio anterior na cadeia</param>
		/// <returns>O valor atual deste <see cref="T:System.Windows.Media.Animation.AnimationClock" />.</returns>
		// Token: 0x06003485 RID: 13445 RVA: 0x000D01BC File Offset: 0x000CF5BC
		public object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue)
		{
			return ((AnimationTimeline)base.Timeline).GetCurrentValue(defaultOriginValue, defaultDestinationValue, this);
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06003486 RID: 13446 RVA: 0x000D01DC File Offset: 0x000CF5DC
		internal override bool NeedsTicksWhenActive
		{
			get
			{
				return true;
			}
		}
	}
}
