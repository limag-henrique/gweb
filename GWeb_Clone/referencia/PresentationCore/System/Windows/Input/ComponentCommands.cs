using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Fornece um conjunto padrão de comandos relacionados ao componente, que tem gestos de entrada chave predefinidos e propriedades <see cref="P:System.Windows.Input.RoutedUICommand.Text" />.</summary>
	// Token: 0x02000225 RID: 549
	public static class ComponentCommands
	{
		/// <summary>Obtém o valor que representa o comando Rolar Página para Cima.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  PageUp  texto de interface do usuário   Rolar página para cima</returns>
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x000389C0 File Offset: 0x00037DC0
		public static RoutedUICommand ScrollPageUp
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.ScrollPageUp);
			}
		}

		/// <summary>Obtém o valor que representa o comando Rolar Página para Baixo.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  PageDown  interface do usuário Texto  Rolar página para baixo</returns>
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x000389D4 File Offset: 0x00037DD4
		public static RoutedUICommand ScrollPageDown
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.ScrollPageDown);
			}
		}

		/// <summary>Obtém o valor que representa o comando Rolar Página para a Esquerda.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  rolar para a esquerda da página</returns>
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x000389E8 File Offset: 0x00037DE8
		public static RoutedUICommand ScrollPageLeft
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.ScrollPageLeft);
			}
		}

		/// <summary>Obtém o valor que representa o comando Rolar Página para a Direita.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  Rolar página para a direita</returns>
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x000389FC File Offset: 0x00037DFC
		public static RoutedUICommand ScrollPageRight
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.ScrollPageRight);
			}
		}

		/// <summary>Obtém o valor que representa o comando Rolar por Linha.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido Texto de interface do usuário  rolar por linha</returns>
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x00038A10 File Offset: 0x00037E10
		public static RoutedUICommand ScrollByLine
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.ScrollByLine);
			}
		}

		/// <summary>Obtém o valor que representa o comando Mover para a Esquerda.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  esquerda  texto de interface do usuário   Mover para a esquerda</returns>
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x00038A24 File Offset: 0x00037E24
		public static RoutedUICommand MoveLeft
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.MoveLeft);
			}
		}

		/// <summary>Obtém o valor que representa o comando Mover para a Direita.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  direita  texto de interface do usuário   Mover para a direita</returns>
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x00038A38 File Offset: 0x00037E38
		public static RoutedUICommand MoveRight
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.MoveRight);
			}
		}

		/// <summary>Obtém o valor que representa o comando Mover para Cima.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  backup  detextodeinterfacedousuário Mover para cima</returns>
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x00038A4C File Offset: 0x00037E4C
		public static RoutedUICommand MoveUp
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.MoveUp);
			}
		}

		/// <summary>Obtém o valor que representa o comando Mover para Baixo.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  para baixo  texto de interface do usuário   Mover para baixo</returns>
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x00038A60 File Offset: 0x00037E60
		public static RoutedUICommand MoveDown
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.MoveDown);
			}
		}

		/// <summary>Obtém o valor que representa o comando Mover para o Início.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  início  texto de interface do usuário   Mover para o início</returns>
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x00038A74 File Offset: 0x00037E74
		public static RoutedUICommand MoveToHome
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.MoveToHome);
			}
		}

		/// <summary>Obtém o valor que representa o comando Mover para o Fim.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  final  texto de interface do usuário   Mover para o final</returns>
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00038A88 File Offset: 0x00037E88
		public static RoutedUICommand MoveToEnd
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.MoveToEnd);
			}
		}

		/// <summary>Obtém o valor que representa o comando Mover para Page Up.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  PageUp  texto de interface do usuário   Mover para Page Up</returns>
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x00038A9C File Offset: 0x00037E9C
		public static RoutedUICommand MoveToPageUp
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.MoveToPageUp);
			}
		}

		/// <summary>Obtém o valor que representa o comando Mover para Page Down.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  PageDown  interface do usuário Texto  mover para Page Down</returns>
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x00038AB0 File Offset: 0x00037EB0
		public static RoutedUICommand MoveToPageDown
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.MoveToPageDown);
			}
		}

		/// <summary>Obtém o valor que representa o comando Estender Seleção para Cima.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Shift + seta para cima  interface do usuário Texto  estender seleção para cima</returns>
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x00038AC4 File Offset: 0x00037EC4
		public static RoutedUICommand ExtendSelectionUp
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.ExtendSelectionUp);
			}
		}

		/// <summary>Obtém o valor que representa o comando Estender Seleção para Baixo.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Shift + seta para baixo  interface do usuário Texto  estender seleção para baixo</returns>
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x00038AD8 File Offset: 0x00037ED8
		public static RoutedUICommand ExtendSelectionDown
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.ExtendSelectionDown);
			}
		}

		/// <summary>Obtém o valor que representa o comando Estender Seleção para a Esquerda.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Shift + Left  interface do usuário Texto  estender seleção para a esquerda</returns>
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x00038AEC File Offset: 0x00037EEC
		public static RoutedUICommand ExtendSelectionLeft
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.ExtendSelectionLeft);
			}
		}

		/// <summary>Obtém o valor que representa o comando Estender Seleção para a Direita.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Shift + seta para direita  interface do usuário Texto  estender seleção para a direita</returns>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x00038B00 File Offset: 0x00037F00
		public static RoutedUICommand ExtendSelectionRight
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.ExtendSelectionRight);
			}
		}

		/// <summary>Obtém o valor que representa o comando Selecionar até o Início.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Shift + Home  interface do usuário Texto  selecionar até o início</returns>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x00038B14 File Offset: 0x00037F14
		public static RoutedUICommand SelectToHome
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.SelectToHome);
			}
		}

		/// <summary>Obtém o valor que representa o comando Selecionar até o Fim.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Shift + End  interface do usuário Texto  selecionar até final</returns>
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x00038B28 File Offset: 0x00037F28
		public static RoutedUICommand SelectToEnd
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.SelectToEnd);
			}
		}

		/// <summary>Obtém o valor que representa o comando Selecionar para Page Up.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Shift + PageUp  Texto da interface do usuário  selecionar para Page Up</returns>
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x00038B3C File Offset: 0x00037F3C
		public static RoutedUICommand SelectToPageUp
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.SelectToPageUp);
			}
		}

		/// <summary>Obtém o valor que representa o comando Selecionar para Page Down.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Shift + PageDown Texto de interface do usuário  selecionar para Page Down</returns>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x00038B50 File Offset: 0x00037F50
		public static RoutedUICommand SelectToPageDown
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.SelectToPageDown);
			}
		}

		/// <summary>Obtém o valor que representa o comando Mover Foco para Cima.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Ctrl + seta para cima  texto de interface do usuário   Mover o foco para cima</returns>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x00038B64 File Offset: 0x00037F64
		public static RoutedUICommand MoveFocusUp
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.MoveFocusUp);
			}
		}

		/// <summary>Obtém o valor que representa o comando Mover Foco para Baixo.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Ctrl + seta para baixo  interface do usuário Texto  Move o foco para baixo</returns>
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x00038B78 File Offset: 0x00037F78
		public static RoutedUICommand MoveFocusDown
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.MoveFocusDown);
			}
		}

		/// <summary>Obtém o valor que representa o comando Mover Foco para Frente.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Ctrl + direita  interface do usuário Texto  Move o foco para frente</returns>
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x00038B8C File Offset: 0x00037F8C
		public static RoutedUICommand MoveFocusForward
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.MoveFocusForward);
			}
		}

		/// <summary>Obtém o valor que representa o comando Mover Foco para Trás.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Ctrl + esquerda  interface do usuário Texto  Mover foco para trás</returns>
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x00038BA0 File Offset: 0x00037FA0
		public static RoutedUICommand MoveFocusBack
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.MoveFocusBack);
			}
		}

		/// <summary>Obtém o valor que representa o comando Mover Foco com Page Up.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Ctrl + PageUp  interface do usuário Texto  Mover foco Page Up</returns>
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x00038BB4 File Offset: 0x00037FB4
		public static RoutedUICommand MoveFocusPageUp
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.MoveFocusPageUp);
			}
		}

		/// <summary>Obtém o valor que representa o comando Mover Foco com Page Down.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Ctrl + PageDown  Texto da interface do usuário  Mover foco Page Down</returns>
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x00038BC8 File Offset: 0x00037FC8
		public static RoutedUICommand MoveFocusPageDown
		{
			get
			{
				return ComponentCommands._EnsureCommand(ComponentCommands.CommandId.MoveFocusPageDown);
			}
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00038BDC File Offset: 0x00037FDC
		private static string GetPropertyName(ComponentCommands.CommandId commandId)
		{
			string result = string.Empty;
			switch (commandId)
			{
			case ComponentCommands.CommandId.ScrollPageUp:
				result = "ScrollPageUp";
				break;
			case ComponentCommands.CommandId.ScrollPageDown:
				result = "ScrollPageDown";
				break;
			case ComponentCommands.CommandId.ScrollPageLeft:
				result = "ScrollPageLeft";
				break;
			case ComponentCommands.CommandId.ScrollPageRight:
				result = "ScrollPageRight";
				break;
			case ComponentCommands.CommandId.ScrollByLine:
				result = "ScrollByLine";
				break;
			case ComponentCommands.CommandId.MoveLeft:
				result = "MoveLeft";
				break;
			case ComponentCommands.CommandId.MoveRight:
				result = "MoveRight";
				break;
			case ComponentCommands.CommandId.MoveUp:
				result = "MoveUp";
				break;
			case ComponentCommands.CommandId.MoveDown:
				result = "MoveDown";
				break;
			case ComponentCommands.CommandId.MoveToHome:
				result = "MoveToHome";
				break;
			case ComponentCommands.CommandId.MoveToEnd:
				result = "MoveToEnd";
				break;
			case ComponentCommands.CommandId.MoveToPageUp:
				result = "MoveToPageUp";
				break;
			case ComponentCommands.CommandId.MoveToPageDown:
				result = "MoveToPageDown";
				break;
			case ComponentCommands.CommandId.SelectToHome:
				result = "SelectToHome";
				break;
			case ComponentCommands.CommandId.SelectToEnd:
				result = "SelectToEnd";
				break;
			case ComponentCommands.CommandId.SelectToPageUp:
				result = "SelectToPageUp";
				break;
			case ComponentCommands.CommandId.SelectToPageDown:
				result = "SelectToPageDown";
				break;
			case ComponentCommands.CommandId.MoveFocusUp:
				result = "MoveFocusUp";
				break;
			case ComponentCommands.CommandId.MoveFocusDown:
				result = "MoveFocusDown";
				break;
			case ComponentCommands.CommandId.MoveFocusForward:
				result = "MoveFocusForward";
				break;
			case ComponentCommands.CommandId.MoveFocusBack:
				result = "MoveFocusBack";
				break;
			case ComponentCommands.CommandId.MoveFocusPageUp:
				result = "MoveFocusPageUp";
				break;
			case ComponentCommands.CommandId.MoveFocusPageDown:
				result = "MoveFocusPageDown";
				break;
			case ComponentCommands.CommandId.ExtendSelectionLeft:
				result = "ExtendSelectionLeft";
				break;
			case ComponentCommands.CommandId.ExtendSelectionRight:
				result = "ExtendSelectionRight";
				break;
			case ComponentCommands.CommandId.ExtendSelectionUp:
				result = "ExtendSelectionUp";
				break;
			case ComponentCommands.CommandId.ExtendSelectionDown:
				result = "ExtendSelectionDown";
				break;
			}
			return result;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00038D64 File Offset: 0x00038164
		internal static string GetUIText(byte commandId)
		{
			string result = string.Empty;
			switch (commandId)
			{
			case 1:
				result = SR.Get("ScrollPageUpText");
				break;
			case 2:
				result = SR.Get("ScrollPageDownText");
				break;
			case 3:
				result = SR.Get("ScrollPageLeftText");
				break;
			case 4:
				result = SR.Get("ScrollPageRightText");
				break;
			case 5:
				result = SR.Get("ScrollByLineText");
				break;
			case 6:
				result = SR.Get("MoveLeftText");
				break;
			case 7:
				result = SR.Get("MoveRightText");
				break;
			case 8:
				result = SR.Get("MoveUpText");
				break;
			case 9:
				result = SR.Get("MoveDownText");
				break;
			case 10:
				result = SR.Get("MoveToHomeText");
				break;
			case 11:
				result = SR.Get("MoveToEndText");
				break;
			case 12:
				result = SR.Get("MoveToPageUpText");
				break;
			case 13:
				result = SR.Get("MoveToPageDownText");
				break;
			case 14:
				result = SR.Get("SelectToHomeText");
				break;
			case 15:
				result = SR.Get("SelectToEndText");
				break;
			case 16:
				result = SR.Get("SelectToPageUpText");
				break;
			case 17:
				result = SR.Get("SelectToPageDownText");
				break;
			case 18:
				result = SR.Get("MoveFocusUpText");
				break;
			case 19:
				result = SR.Get("MoveFocusDownText");
				break;
			case 20:
				result = SR.Get("MoveFocusForwardText");
				break;
			case 21:
				result = SR.Get("MoveFocusBackText");
				break;
			case 22:
				result = SR.Get("MoveFocusPageUpText");
				break;
			case 23:
				result = SR.Get("MoveFocusPageDownText");
				break;
			case 24:
				result = SR.Get("ExtendSelectionLeftText");
				break;
			case 25:
				result = SR.Get("ExtendSelectionRightText");
				break;
			case 26:
				result = SR.Get("ExtendSelectionUpText");
				break;
			case 27:
				result = SR.Get("ExtendSelectionDownText");
				break;
			}
			return result;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00038F84 File Offset: 0x00038384
		internal static InputGestureCollection LoadDefaultGestureFromResource(byte commandId)
		{
			InputGestureCollection inputGestureCollection = new InputGestureCollection();
			switch (commandId)
			{
			case 1:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("ScrollPageUpKey"), SR.Get("ScrollPageUpKeyDisplayString"), inputGestureCollection);
				break;
			case 2:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("ScrollPageDownKey"), SR.Get("ScrollPageDownKeyDisplayString"), inputGestureCollection);
				break;
			case 3:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("ScrollPageLeftKey"), SR.Get("ScrollPageLeftKeyDisplayString"), inputGestureCollection);
				break;
			case 4:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("ScrollPageRightKey"), SR.Get("ScrollPageRightKeyDisplayString"), inputGestureCollection);
				break;
			case 5:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("ScrollByLineKey"), SR.Get("ScrollByLineKeyDisplayString"), inputGestureCollection);
				break;
			case 6:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MoveLeftKey"), SR.Get("MoveLeftKeyDisplayString"), inputGestureCollection);
				break;
			case 7:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MoveRightKey"), SR.Get("MoveRightKeyDisplayString"), inputGestureCollection);
				break;
			case 8:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MoveUpKey"), SR.Get("MoveUpKeyDisplayString"), inputGestureCollection);
				break;
			case 9:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MoveDownKey"), SR.Get("MoveDownKeyDisplayString"), inputGestureCollection);
				break;
			case 10:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MoveToHomeKey"), SR.Get("MoveToHomeKeyDisplayString"), inputGestureCollection);
				break;
			case 11:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MoveToEndKey"), SR.Get("MoveToEndKeyDisplayString"), inputGestureCollection);
				break;
			case 12:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MoveToPageUpKey"), SR.Get("MoveToPageUpKeyDisplayString"), inputGestureCollection);
				break;
			case 13:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MoveToPageDownKey"), SR.Get("MoveToPageDownKeyDisplayString"), inputGestureCollection);
				break;
			case 14:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("SelectToHomeKey"), SR.Get("SelectToHomeKeyDisplayString"), inputGestureCollection);
				break;
			case 15:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("SelectToEndKey"), SR.Get("SelectToEndKeyDisplayString"), inputGestureCollection);
				break;
			case 16:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("SelectToPageUpKey"), SR.Get("SelectToPageUpKeyDisplayString"), inputGestureCollection);
				break;
			case 17:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("SelectToPageDownKey"), SR.Get("SelectToPageDownKeyDisplayString"), inputGestureCollection);
				break;
			case 18:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MoveFocusUpKey"), SR.Get("MoveFocusUpKeyDisplayString"), inputGestureCollection);
				break;
			case 19:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MoveFocusDownKey"), SR.Get("MoveFocusDownKeyDisplayString"), inputGestureCollection);
				break;
			case 20:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MoveFocusForwardKey"), SR.Get("MoveFocusForwardKeyDisplayString"), inputGestureCollection);
				break;
			case 21:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MoveFocusBackKey"), SR.Get("MoveFocusBackKeyDisplayString"), inputGestureCollection);
				break;
			case 22:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MoveFocusPageUpKey"), SR.Get("MoveFocusPageUpKeyDisplayString"), inputGestureCollection);
				break;
			case 23:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MoveFocusPageDownKey"), SR.Get("MoveFocusPageDownKeyDisplayString"), inputGestureCollection);
				break;
			case 24:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("ExtendSelectionLeftKey"), SR.Get("ExtendSelectionLeftKeyDisplayString"), inputGestureCollection);
				break;
			case 25:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("ExtendSelectionRightKey"), SR.Get("ExtendSelectionRightKeyDisplayString"), inputGestureCollection);
				break;
			case 26:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("ExtendSelectionUpKey"), SR.Get("ExtendSelectionUpKeyDisplayString"), inputGestureCollection);
				break;
			case 27:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("ExtendSelectionDownKey"), SR.Get("ExtendSelectionDownKeyDisplayString"), inputGestureCollection);
				break;
			}
			return inputGestureCollection;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00039348 File Offset: 0x00038748
		private static RoutedUICommand _EnsureCommand(ComponentCommands.CommandId idCommand)
		{
			if (idCommand >= (ComponentCommands.CommandId)0 && idCommand < ComponentCommands.CommandId.Last)
			{
				object syncRoot = ComponentCommands._internalCommands.SyncRoot;
				lock (syncRoot)
				{
					if (ComponentCommands._internalCommands[(int)idCommand] == null)
					{
						RoutedUICommand routedUICommand = new RoutedUICommand(ComponentCommands.GetPropertyName(idCommand), typeof(ComponentCommands), (byte)idCommand);
						routedUICommand.AreInputGesturesDelayLoaded = true;
						ComponentCommands._internalCommands[(int)idCommand] = routedUICommand;
					}
				}
				return ComponentCommands._internalCommands[(int)idCommand];
			}
			return null;
		}

		// Token: 0x0400085B RID: 2139
		private static RoutedUICommand[] _internalCommands = new RoutedUICommand[28];

		// Token: 0x02000806 RID: 2054
		private enum CommandId : byte
		{
			// Token: 0x040026CD RID: 9933
			ScrollPageUp = 1,
			// Token: 0x040026CE RID: 9934
			ScrollPageDown,
			// Token: 0x040026CF RID: 9935
			ScrollPageLeft,
			// Token: 0x040026D0 RID: 9936
			ScrollPageRight,
			// Token: 0x040026D1 RID: 9937
			ScrollByLine,
			// Token: 0x040026D2 RID: 9938
			MoveLeft,
			// Token: 0x040026D3 RID: 9939
			MoveRight,
			// Token: 0x040026D4 RID: 9940
			MoveUp,
			// Token: 0x040026D5 RID: 9941
			MoveDown,
			// Token: 0x040026D6 RID: 9942
			MoveToHome,
			// Token: 0x040026D7 RID: 9943
			MoveToEnd,
			// Token: 0x040026D8 RID: 9944
			MoveToPageUp,
			// Token: 0x040026D9 RID: 9945
			MoveToPageDown,
			// Token: 0x040026DA RID: 9946
			SelectToHome,
			// Token: 0x040026DB RID: 9947
			SelectToEnd,
			// Token: 0x040026DC RID: 9948
			SelectToPageUp,
			// Token: 0x040026DD RID: 9949
			SelectToPageDown,
			// Token: 0x040026DE RID: 9950
			MoveFocusUp,
			// Token: 0x040026DF RID: 9951
			MoveFocusDown,
			// Token: 0x040026E0 RID: 9952
			MoveFocusForward,
			// Token: 0x040026E1 RID: 9953
			MoveFocusBack,
			// Token: 0x040026E2 RID: 9954
			MoveFocusPageUp,
			// Token: 0x040026E3 RID: 9955
			MoveFocusPageDown,
			// Token: 0x040026E4 RID: 9956
			ExtendSelectionLeft,
			// Token: 0x040026E5 RID: 9957
			ExtendSelectionRight,
			// Token: 0x040026E6 RID: 9958
			ExtendSelectionUp,
			// Token: 0x040026E7 RID: 9959
			ExtendSelectionDown,
			// Token: 0x040026E8 RID: 9960
			Last
		}
	}
}
