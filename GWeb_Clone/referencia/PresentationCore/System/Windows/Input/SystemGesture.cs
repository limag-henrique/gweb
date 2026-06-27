using System;

namespace System.Windows.Input
{
	/// <summary>Define os gestos do sistema disponíveis.</summary>
	// Token: 0x020002C3 RID: 707
	public enum SystemGesture
	{
		/// <summary>Nenhum gesto do sistema.</summary>
		// Token: 0x04000B6F RID: 2927
		None,
		/// <summary>Mapeia para um clique com o botão esquerdo do mouse. Isso poderá ser usado para escolher um comando do menu ou da barra de ferramentas, para realizar uma ação se um comando for escolhido, para definir um ponto de inserção ou para mostrar os comentários da seleção.</summary>
		// Token: 0x04000B70 RID: 2928
		Tap = 16,
		/// <summary>Mapeia para um clique com o botão direito do mouse. Isso pode ser usado para exibir um menu de atalho.</summary>
		// Token: 0x04000B71 RID: 2929
		RightTap = 18,
		/// <summary>Mapeia para a ação de arrastar para a esquerda com o mouse.</summary>
		// Token: 0x04000B72 RID: 2930
		Drag,
		/// <summary>Mapeia para a ação de arrastar para a direita com o mouse. Isso pode ser usado para arrastar um objeto ou a seleção para uma área diferente e é seguido pela aparência do menu de atalho que fornece opções para mover o objeto.</summary>
		// Token: 0x04000B73 RID: 2931
		RightDrag,
		/// <summary>Indica que ocorre uma ação de pressionar e segurar.</summary>
		// Token: 0x04000B74 RID: 2932
		HoldEnter,
		/// <summary>Não implementado.</summary>
		// Token: 0x04000B75 RID: 2933
		HoldLeave,
		/// <summary>Mapeia para a ação de focalizar o mouse. Isso pode ser usado para mostrar os efeitos de substituição de Dica de Ferramenta ou outros comportamentos de focalizar o mouse.</summary>
		// Token: 0x04000B76 RID: 2934
		HoverEnter,
		/// <summary>Mapeia para o mouse saindo de um foco. Isso pode ser usado para encerrar os efeitos de substituição de Dica de Ferramenta ou outros comportamentos de focalizar o mouse.</summary>
		// Token: 0x04000B77 RID: 2935
		HoverLeave,
		/// <summary>Ocorre com um gesto curto e rápido se traduz em um comando específico. A ação tomada por um movimento é definida em todo o sistema. Um aplicativo pode escutar um <see cref="F:System.Windows.Input.SystemGesture.Flick" /> e impedir que ele se torne um <see cref="T:System.Windows.Input.ApplicationCommands" /> padrão definindo a propriedade <see cref="P:System.Windows.RoutedEventArgs.Handled" /> como true no evento <see cref="E:System.Windows.UIElement.StylusSystemGesture" />. Somente o Windows Vista dá suporte a movimentos.</summary>
		// Token: 0x04000B78 RID: 2936
		Flick = 31,
		/// <summary>Mapeia para uma ação clicar duas vezes do mouse.</summary>
		// Token: 0x04000B79 RID: 2937
		TwoFingerTap = 4352
	}
}
