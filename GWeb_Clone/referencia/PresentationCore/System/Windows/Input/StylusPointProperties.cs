using System;

namespace System.Windows.Input
{
	/// <summary>Contém um <see cref="T:System.Windows.Input.StylusPointProperty" /> para cada propriedade que é compatível com o WPF.</summary>
	// Token: 0x020002BC RID: 700
	public static class StylusPointProperties
	{
		/// <summary>Representa a coordenada x no espaço de coordenadas do tablet.</summary>
		// Token: 0x04000B18 RID: 2840
		public static readonly StylusPointProperty X = new StylusPointProperty(StylusPointPropertyIds.X, false);

		/// <summary>Representa a coordenada y no espaço de coordenadas do tablet.</summary>
		// Token: 0x04000B19 RID: 2841
		public static readonly StylusPointProperty Y = new StylusPointProperty(StylusPointPropertyIds.Y, false);

		/// <summary>Representa a coordenada z ou a distância da ponta da caneta da superfície do tablet.</summary>
		// Token: 0x04000B1A RID: 2842
		public static readonly StylusPointProperty Z = new StylusPointProperty(StylusPointPropertyIds.Z, false);

		/// <summary>Representa a largura do ponto de contato no digitalizador.</summary>
		// Token: 0x04000B1B RID: 2843
		public static readonly StylusPointProperty Width = new StylusPointProperty(StylusPointPropertyIds.Width, false);

		/// <summary>Representa a altura do ponto de contato no digitalizador.</summary>
		// Token: 0x04000B1C RID: 2844
		public static readonly StylusPointProperty Height = new StylusPointProperty(StylusPointPropertyIds.Height, false);

		/// <summary>Representa o ponto de contato que gera o <see cref="T:System.Windows.Input.StylusPoint" />, seja iniciado por um dedo, pela palma ou qualquer outro contato.</summary>
		// Token: 0x04000B1D RID: 2845
		public static readonly StylusPointProperty SystemTouch = new StylusPointProperty(StylusPointPropertyIds.SystemTouch, false);

		/// <summary>Representa o status atual do cursor.</summary>
		// Token: 0x04000B1E RID: 2846
		public static readonly StylusPointProperty PacketStatus = new StylusPointProperty(StylusPointPropertyIds.PacketStatus, false);

		/// <summary>Identifica o <see cref="T:System.Windows.Input.StylusPoint" />.</summary>
		// Token: 0x04000B1F RID: 2847
		public static readonly StylusPointProperty SerialNumber = new StylusPointProperty(StylusPointPropertyIds.SerialNumber, false);

		/// <summary>Representa a pressão da ponta da caneta perpendicular à superfície do Tablet PC.</summary>
		// Token: 0x04000B20 RID: 2848
		public static readonly StylusPointProperty NormalPressure = new StylusPointProperty(StylusPointPropertyIds.NormalPressure, false);

		/// <summary>Representa a pressão da ponta da caneta ao longo do plano da superfície do Tablet PC.</summary>
		// Token: 0x04000B21 RID: 2849
		public static readonly StylusPointProperty TangentPressure = new StylusPointProperty(StylusPointPropertyIds.TangentPressure, false);

		/// <summary>Representa a pressão sobre um botão sensível à pressão.</summary>
		// Token: 0x04000B22 RID: 2850
		public static readonly StylusPointProperty ButtonPressure = new StylusPointProperty(StylusPointPropertyIds.ButtonPressure, false);

		/// <summary>Representa o ângulo entre o plano (<paramref name="y,z" />) e a caneta e o plano do eixo y.</summary>
		// Token: 0x04000B23 RID: 2851
		public static readonly StylusPointProperty XTiltOrientation = new StylusPointProperty(StylusPointPropertyIds.XTiltOrientation, false);

		/// <summary>Representa o ângulo entre o plano (x, z) e a caneta e o plano do eixo x.</summary>
		// Token: 0x04000B24 RID: 2852
		public static readonly StylusPointProperty YTiltOrientation = new StylusPointProperty(StylusPointPropertyIds.YTiltOrientation, false);

		/// <summary>Representa a rotação horária do cursor em todo um intervalo circular em torno do eixo z.</summary>
		// Token: 0x04000B25 RID: 2853
		public static readonly StylusPointProperty AzimuthOrientation = new StylusPointProperty(StylusPointPropertyIds.AzimuthOrientation, false);

		/// <summary>Representa o ângulo entre o eixo da caneta e a superfície do Tablet PC.</summary>
		// Token: 0x04000B26 RID: 2854
		public static readonly StylusPointProperty AltitudeOrientation = new StylusPointProperty(StylusPointPropertyIds.AltitudeOrientation, false);

		/// <summary>Representa a rotação horária do cursor em torno de seu próprio eixo.</summary>
		// Token: 0x04000B27 RID: 2855
		public static readonly StylusPointProperty TwistOrientation = new StylusPointProperty(StylusPointPropertyIds.TwistOrientation, false);

		/// <summary>Indica se a ponta está acima ou abaixo de uma linha horizontal perpendicular à superfície de escrita.</summary>
		// Token: 0x04000B28 RID: 2856
		public static readonly StylusPointProperty PitchRotation = new StylusPointProperty(StylusPointPropertyIds.PitchRotation, false);

		/// <summary>Representa a rotação horária da caneta em torno de seu próprio eixo.</summary>
		// Token: 0x04000B29 RID: 2857
		public static readonly StylusPointProperty RollRotation = new StylusPointProperty(StylusPointPropertyIds.RollRotation, false);

		/// <summary>Representa o ângulo da caneta à esquerda ou à direita em torno do centro do eixo horizontal quando a caneta está na horizontal.</summary>
		// Token: 0x04000B2A RID: 2858
		public static readonly StylusPointProperty YawRotation = new StylusPointProperty(StylusPointPropertyIds.YawRotation, false);

		/// <summary>Representa o botão de ponta de uma caneta.</summary>
		// Token: 0x04000B2B RID: 2859
		public static readonly StylusPointProperty TipButton = new StylusPointProperty(StylusPointPropertyIds.TipButton, true);

		/// <summary>Representa o botão da caneta em uma caneta.</summary>
		// Token: 0x04000B2C RID: 2860
		public static readonly StylusPointProperty BarrelButton = new StylusPointProperty(StylusPointPropertyIds.BarrelButton, true);

		/// <summary>Representa o botão de ponta secundário de uma caneta.</summary>
		// Token: 0x04000B2D RID: 2861
		public static readonly StylusPointProperty SecondaryTipButton = new StylusPointProperty(StylusPointPropertyIds.SecondaryTipButton, true);
	}
}
