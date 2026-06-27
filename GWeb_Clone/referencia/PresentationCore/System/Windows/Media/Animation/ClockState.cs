using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Descreve os estados possíveis do objeto <see cref="T:System.Windows.Media.Animation.Clock" /> de um cronograma.</summary>
	// Token: 0x020004AA RID: 1194
	public enum ClockState
	{
		/// <summary>A hora <see cref="T:System.Windows.Media.Animation.Clock" /> atual é alterada em relação direta à de seu pai. Se a linha do tempo é uma animação, ela está afetando ativamente propriedades de destino, de modo que o valor delas pode ser alterado entre um tique (um ponto de amostragem no tempo) e outro. Se a linha do tempo tiver filhos, eles poderão ser <see cref="F:System.Windows.Media.Animation.ClockState.Active" />, <see cref="F:System.Windows.Media.Animation.ClockState.Filling" /> ou <see cref="F:System.Windows.Media.Animation.ClockState.Stopped" />.</summary>
		// Token: 0x0400163C RID: 5692
		Active,
		/// <summary>O tempo de <see cref="T:System.Windows.Media.Animation.Clock" /> continua, mas não é alterado em relação àquele do seu pai. Se a linha do tempo é uma animação, ela está afetando ativamente propriedades de destino, mas os valores dela não são alterados entre um tique e outro. Se a linha do tempo tiver filhos, eles poderão ser <see cref="F:System.Windows.Media.Animation.ClockState.Active" />, <see cref="F:System.Windows.Media.Animation.ClockState.Filling" /> ou <see cref="F:System.Windows.Media.Animation.ClockState.Stopped" />.</summary>
		// Token: 0x0400163D RID: 5693
		Filling,
		/// <summary>O tempo de <see cref="T:System.Windows.Media.Animation.Clock" /> é interrompido, tornando os valores de hora e progresso atuais do relógio indefinidos. Se essa linha do tempo é uma animação, ele já não afeta mais as propriedades de destino. Se a linha do tempo tiver filhos, eles também serão <see cref="F:System.Windows.Media.Animation.ClockState.Stopped" />.</summary>
		// Token: 0x0400163E RID: 5694
		Stopped
	}
}
