using System;

namespace System.Windows.Ink
{
	/// <summary>Especifica a o <paramref name="gesture" /> específico do aplicativo disponível.</summary>
	// Token: 0x02000341 RID: 833
	public enum ApplicationGesture
	{
		/// <summary>Reconhece todos os gestos específicos do aplicativo.</summary>
		// Token: 0x04000F4B RID: 3915
		AllGestures,
		/// <summary>Não tem ação nem comportamento semântico sugerido. A seta pode ser desenhada com um único traço ou em dois traços, em que um traço é a linha e o outro é a cabeça da seta. Não use mais de dois traços para desenhar a seta.</summary>
		// Token: 0x04000F4C RID: 3916
		ArrowDown = 61497,
		/// <summary>Não tem ação nem comportamento semântico sugerido. A seta pode ser desenhada com um único traço ou em dois traços, em que um traço é a linha e o outro é a cabeça da seta. Não use mais de dois traços para desenhar a seta.</summary>
		// Token: 0x04000F4D RID: 3917
		ArrowLeft,
		/// <summary>Não tem ação nem comportamento semântico sugerido. A seta pode ser desenhada com um único traço ou em dois traços, em que um traço é a linha e o outro é a cabeça da seta. Não use mais de dois traços para desenhar a seta.</summary>
		// Token: 0x04000F4E RID: 3918
		ArrowRight,
		/// <summary>Não tem ação nem comportamento semântico sugerido. A seta pode ser desenhada com um único traço ou em dois traços, em que um traço é a linha e o outro é a cabeça da seta. Não use mais de dois traços para desenhar a seta.</summary>
		// Token: 0x04000F4F RID: 3919
		ArrowUp = 61496,
		/// <summary>Não tem ação nem comportamento semântico sugerido. O traço para cima deve ser duas vezes mais longo que o traço menor para baixo.</summary>
		// Token: 0x04000F50 RID: 3920
		Check = 61445,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Ambos os lados da divisa devem ser desenhados para que sejam o mais parecidos possível. O ângulo deve ser agudo e terminar em uma ponta.</summary>
		// Token: 0x04000F51 RID: 3921
		ChevronDown = 61489,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Ambos os lados da divisa devem ser desenhados para que sejam o mais parecidos possível. O ângulo deve ser agudo e terminar em uma ponta.</summary>
		// Token: 0x04000F52 RID: 3922
		ChevronLeft,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Ambos os lados da divisa devem ser desenhados para que sejam o mais parecidos possível. O ângulo deve ser agudo e terminar em uma ponta.</summary>
		// Token: 0x04000F53 RID: 3923
		ChevronRight,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Ambos os lados da divisa devem ser desenhados para que sejam o mais parecidos possível. O ângulo deve ser agudo e terminar em uma ponta.</summary>
		// Token: 0x04000F54 RID: 3924
		ChevronUp = 61488,
		/// <summary>Não tem ação nem comportamento semântico sugerido. O círculo deve ser desenhado em um único traço sem levantar a caneta.</summary>
		// Token: 0x04000F55 RID: 3925
		Circle = 61472,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Inicie a ondulação na palavra que você pretende recortar.</summary>
		// Token: 0x04000F56 RID: 3926
		Curlicue = 61456,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Os dois círculos devem se sobrepor e ser desenhados em um único traço sem levantar a caneta.</summary>
		// Token: 0x04000F57 RID: 3927
		DoubleCircle = 61473,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Inicie o ondulado duplo na palavra que você pretende copiar.</summary>
		// Token: 0x04000F58 RID: 3928
		DoubleCurlicue = 61457,
		/// <summary>Indica clicar duas vezes com o mouse. Os dois toques devem ser feitos rapidamente para resultar na menor quantidade de deslizamento e na menor duração entre os toques. Além disso, os toques devem ser o mais próximo possível um do outro.</summary>
		// Token: 0x04000F59 RID: 3929
		DoubleTap = 61681,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Esse gesto deve ser desenhado como um único movimento rápido na direção para baixo.</summary>
		// Token: 0x04000F5A RID: 3930
		Down = 61529,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Esse gesto deve ser feito em um único traço iniciando com o traço para baixo. Os dois lados devem o mais iguais em comprimento possível e estar em um ângulo reto.</summary>
		// Token: 0x04000F5B RID: 3931
		DownLeft = 61546,
		/// <summary>Significa pressionar uma tecla ENTER. Esse gesto deve ser feito em um único traço iniciando com o traço para baixo. O traço à esquerda tem cerca de duas vezes o comprimento do traço para cima e os dois traços devem estar em um ângulo reto.</summary>
		// Token: 0x04000F5C RID: 3932
		DownLeftLong = 61542,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Esse gesto deve ser feito em um único traço iniciando com o traço para baixo. Os dois lados devem o mais iguais em comprimento possível e estar em um ângulo reto.</summary>
		// Token: 0x04000F5D RID: 3933
		DownRight = 61547,
		/// <summary>Significa pressionar a barra de espaços. Esse gesto deve ser feito em um único traço iniciando com o traço para baixo. O traço à direita deve ter cerca de duas vezes o comprimento do traço para cima e os dois traços devem estar em um ângulo reto.</summary>
		// Token: 0x04000F5E RID: 3934
		DownRightLong = 61543,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Esse gesto deve ser feito em um único traço iniciando com o traço para baixo. Os dois traços devem estar o mais próximo possível um do outro.</summary>
		// Token: 0x04000F5F RID: 3935
		DownUp = 61537,
		/// <summary>Não tem ação nem comportamento semântico sugerido. A linha deve ser desenhada primeiro e, em seguida, o ponto deve ser desenhado rapidamente e o mais próximo possível da linha.</summary>
		// Token: 0x04000F60 RID: 3936
		Exclamation = 61604,
		/// <summary>Especifica um backspace. Esse gesto deve ser desenhado como um único movimento rápido à esquerda.</summary>
		// Token: 0x04000F61 RID: 3937
		Left = 61530,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Esse gesto deve ser feito em um único traço iniciando com o traço à esquerda. Os dois lados são o mais parecidos em termos de comprimento possível e estão em um ângulo reto.</summary>
		// Token: 0x04000F62 RID: 3938
		LeftDown = 61549,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Esse gesto deve ser feito em um único traço iniciando com o traço à esquerda. Os dois lados são o mais parecidos em termos de comprimento possível e estão em um ângulo reto.</summary>
		// Token: 0x04000F63 RID: 3939
		LeftRight = 61538,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Esse gesto deve ser feito em um único traço iniciando com o traço à esquerda. Os dois lados devem o mais iguais em comprimento possível e estar em um ângulo reto.</summary>
		// Token: 0x04000F64 RID: 3940
		LeftUp = 61548,
		/// <summary>Não reconhece nenhum gesto específico do aplicativo.</summary>
		// Token: 0x04000F65 RID: 3941
		NoGesture = 61440,
		/// <summary>Representa um espaço. Esse gesto deve ser desenhado como um único movimento rápido à direita.</summary>
		// Token: 0x04000F66 RID: 3942
		Right = 61531,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Esse gesto deve ser feito em um único traço iniciando com o traço à direita. Os dois lados devem o mais iguais em comprimento possível e estar em um ângulo reto.</summary>
		// Token: 0x04000F67 RID: 3943
		RightDown = 61551,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Esse gesto deve ser feito em um único traço iniciando com o traço à direita. Os dois traços devem estar o mais próximo possível um do outro.</summary>
		// Token: 0x04000F68 RID: 3944
		RightLeft = 61539,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Esse gesto deve ser feito em um único traço iniciando com o traço à direita. Os dois lados devem o mais iguais em comprimento possível e estar em um ângulo reto.</summary>
		// Token: 0x04000F69 RID: 3945
		RightUp = 61550,
		/// <summary>Apaga o conteúdo. Esse gesto deve ser desenhado em um traço único com pelo menos três movimentos para frente e para trás.</summary>
		// Token: 0x04000F6A RID: 3946
		ScratchOut = 61441,
		/// <summary>Não tem ação nem comportamento semântico sugerido. O semicírculo deve ser desenhado da esquerda para a direita. As duas extremidades do semicírculo devem ser o mais parecidas horizontalmente possível.</summary>
		// Token: 0x04000F6B RID: 3947
		SemicircleLeft = 61480,
		/// <summary>Não tem ação nem comportamento semântico sugerido. O semicírculo deve ser desenhado da direita para esquerda. As duas extremidades do semicírculo devem ser o mais parecidas horizontalmente possível.</summary>
		// Token: 0x04000F6C RID: 3948
		SemicircleRight,
		/// <summary>Não tem ação nem comportamento semântico sugerido. O quadrado pode ser desenhado em um ou dois traços. Em um traço, desenhe todo o quadrado sem levantar a caneta. Em dois traços, desenhe três lados do quadrado e use outro traço para desenhar o outro lado. Não use mais de dois traços para desenhar o quadrado.</summary>
		// Token: 0x04000F6D RID: 3949
		Square = 61443,
		/// <summary>Não tem ação nem comportamento semântico sugerido. A estrela deve ter exatamente cinco pontas e ser desenhada em um único traço sem levantar a caneta.</summary>
		// Token: 0x04000F6E RID: 3950
		Star,
		/// <summary>Indica um clique com o mouse. Para o mínimo de deslizamento, o toque deve ser feito rapidamente.</summary>
		// Token: 0x04000F6F RID: 3951
		Tap = 61680,
		/// <summary>Não tem ação nem comportamento semântico sugerido. O triângulo deve ser desenhado em um único traço, sem levantar a caneta.</summary>
		// Token: 0x04000F70 RID: 3952
		Triangle = 61442,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Esse gesto deve ser desenhado como um único movimento rápido na direção para cima.</summary>
		// Token: 0x04000F71 RID: 3953
		Up = 61528,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Esse gesto deve ser feito em um único traço iniciando com o traço para cima. Os dois traços devem estar o mais próximo possível um do outro.</summary>
		// Token: 0x04000F72 RID: 3954
		UpDown = 61536,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Esse gesto deve ser feito em um único traço iniciando com o traço para cima. Os dois lados devem o mais iguais em comprimento possível e estar em um ângulo reto.</summary>
		// Token: 0x04000F73 RID: 3955
		UpLeft = 61544,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Esse gesto deve ser feito em um único traço iniciando com o traço para cima. O traço à esquerda deve ter cerca de duas vezes o comprimento do traço para cima e os dois traços devem estar em um ângulo reto.</summary>
		// Token: 0x04000F74 RID: 3956
		UpLeftLong = 61540,
		/// <summary>Não tem ação nem comportamento semântico sugerido. Esse gesto deve ser feito em um único traço iniciando com o traço para cima. Os dois lados devem o mais iguais em comprimento possível e estar em um ângulo reto.</summary>
		// Token: 0x04000F75 RID: 3957
		UpRight = 61545,
		/// <summary>Significa pressionar uma tecla TAB. Esse gesto deve ser feito em um único traço iniciando com o traço para cima. O traço à direita deve ter cerca de duas vezes o comprimento do traço para cima e os dois traços devem estar em um ângulo reto.</summary>
		// Token: 0x04000F76 RID: 3958
		UpRightLong = 61541
	}
}
