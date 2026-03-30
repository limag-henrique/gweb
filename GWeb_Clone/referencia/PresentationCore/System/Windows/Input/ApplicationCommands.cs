using System;
using System.Security;
using System.Security.Permissions;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Fornece um conjunto padrão de comandos relacionados ao aplicativo.</summary>
	// Token: 0x02000224 RID: 548
	public static class ApplicationCommands
	{
		/// <summary>Obtém o valor que representa o comando Cut.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Ctrl + X Shift + Delete Texto de interface do usuário  Recortar</returns>
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x00038148 File Offset: 0x00037548
		public static RoutedUICommand Cut
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.Cut);
			}
		}

		/// <summary>Obtém o valor que representa o comando Copy.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Ctrl + C, Ctrl + Insert Texto de interface do usuário  cópia</returns>
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x0003815C File Offset: 0x0003755C
		public static RoutedUICommand Copy
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.Copy);
			}
		}

		/// <summary>Obtém o valor que representa o comando Paste.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Ctrl + V Shift + Insert Texto de interface do usuário  colar</returns>
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x00038170 File Offset: 0x00037570
		public static RoutedUICommand Paste
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.Paste);
			}
		}

		/// <summary>Obtém o valor que representa o comando Delete.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  /DEL  texto de interface do usuário   Excluir</returns>
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x00038184 File Offset: 0x00037584
		public static RoutedUICommand Delete
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.Delete);
			}
		}

		/// <summary>Obtém o valor que representa o comando Desfazer.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Ctrl-Z  texto de interface do usuário   Desfazer</returns>
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x00038198 File Offset: 0x00037598
		public static RoutedUICommand Undo
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.Undo);
			}
		}

		/// <summary>Obtém o valor que representa o comando Refazer.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Ctrl + Y  texto de interface do usuário   Refazer</returns>
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x000381AC File Offset: 0x000375AC
		public static RoutedUICommand Redo
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.Redo);
			}
		}

		/// <summary>Obtém o valor que representa o comando Find.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Ctrl + F  texto de interface do usuário   Localizar</returns>
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x000381C0 File Offset: 0x000375C0
		public static RoutedUICommand Find
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.Find);
			}
		}

		/// <summary>Obtém o valor que representa o comando Substituir.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Ctrl + H  texto de interface do usuário   Substituir</returns>
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x000381D4 File Offset: 0x000375D4
		public static RoutedUICommand Replace
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.Replace);
			}
		}

		/// <summary>Obtém o valor que representa o comando Select All.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Ctrl + T  texto de interface do usuário   Selecionar tudo</returns>
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x000381E8 File Offset: 0x000375E8
		public static RoutedUICommand SelectAll
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.SelectAll);
			}
		}

		/// <summary>Obtém o valor que representa o comando Help.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  F1  detextodeinterfacedousuário Ajuda</returns>
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x000381FC File Offset: 0x000375FC
		public static RoutedUICommand Help
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.Help);
			}
		}

		/// <summary>Obtém o valor que representa o comando New.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Ctrl + N  texto de interface do usuário   Novo</returns>
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x00038210 File Offset: 0x00037610
		public static RoutedUICommand New
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.New);
			}
		}

		/// <summary>Obtém o valor que representa o comando Open.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Ctrl + O  texto de interface do usuário   Aberto</returns>
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x00038224 File Offset: 0x00037624
		public static RoutedUICommand Open
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.Open);
			}
		}

		/// <summary>Obtém o valor que representa o comando Fechar.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  fechar</returns>
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00038238 File Offset: 0x00037638
		public static RoutedUICommand Close
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.Close);
			}
		}

		/// <summary>Obtém o valor que representa o comando Save.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Ctrl + S  texto de interface do usuário   Salvar</returns>
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x0003824C File Offset: 0x0003764C
		public static RoutedUICommand Save
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.Save);
			}
		}

		/// <summary>Obtém o valor que representa o comando Salvar como.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  Salvar como</returns>
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x00038260 File Offset: 0x00037660
		public static RoutedUICommand SaveAs
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.SaveAs);
			}
		}

		/// <summary>Obtém o valor que representa o comando Imprimir.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Ctrl + P  texto de interface do usuário   Impressão</returns>
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x00038274 File Offset: 0x00037674
		public static RoutedUICommand Print
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.Print);
			}
		}

		/// <summary>Obtém o valor que representa o comando Cancelar Impressão.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  Cancelar impressão</returns>
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x00038288 File Offset: 0x00037688
		public static RoutedUICommand CancelPrint
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.CancelPrint);
			}
		}

		/// <summary>Obtém o valor que representa o comando Visualizar Impressão.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Ctrl + F2  texto de interface do usuário   Visualizar impressão</returns>
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x0003829C File Offset: 0x0003769C
		public static RoutedUICommand PrintPreview
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.PrintPreview);
			}
		}

		/// <summary>Obtém o valor que representa o comando Propriedades.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  F4  detextodeinterfacedousuário Propriedades</returns>
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x000382B0 File Offset: 0x000376B0
		public static RoutedUICommand Properties
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.Properties);
			}
		}

		/// <summary>Obtém o valor que representa o comando de Menu de Contexto.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Shift + F10 aplicativos  Gesto de mouse  um gesto de Mouse não está anexado a esse comando, mas a maioria dos aplicativos seguem a convenção de como usar o gesto do botão direito do mouse para invocar o menu de contexto.   Texto da interface do usuário  Menu de contexto</returns>
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x000382C4 File Offset: 0x000376C4
		public static RoutedUICommand ContextMenu
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.ContextMenu);
			}
		}

		/// <summary>Obtém o valor que representa o comando Parar.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  Esc  texto de interface do usuário   Parar</returns>
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x000382D8 File Offset: 0x000376D8
		public static RoutedUICommand Stop
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.Stop);
			}
		}

		/// <summary>Obtém o valor que representa o comando Lista de Correção.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  Oprav</returns>
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x000382EC File Offset: 0x000376EC
		public static RoutedUICommand CorrectionList
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.CorrectionList);
			}
		}

		/// <summary>Representa um comando que é sempre ignorado.</summary>
		/// <returns>O comando.</returns>
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x00038300 File Offset: 0x00037700
		public static RoutedUICommand NotACommand
		{
			get
			{
				return ApplicationCommands._EnsureCommand(ApplicationCommands.CommandId.NotACommand);
			}
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00038314 File Offset: 0x00037714
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private static PermissionSet GetRequiredPermissions(ApplicationCommands.CommandId commandId)
		{
			PermissionSet permissionSet;
			if (commandId <= ApplicationCommands.CommandId.Paste)
			{
				permissionSet = new PermissionSet(PermissionState.None);
				permissionSet.AddPermission(new UIPermission(UIPermissionClipboard.AllClipboard));
			}
			else
			{
				permissionSet = null;
			}
			return permissionSet;
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00038340 File Offset: 0x00037740
		private static string GetPropertyName(ApplicationCommands.CommandId commandId)
		{
			string result = string.Empty;
			switch (commandId)
			{
			case ApplicationCommands.CommandId.Cut:
				result = "Cut";
				break;
			case ApplicationCommands.CommandId.Copy:
				result = "Copy";
				break;
			case ApplicationCommands.CommandId.Paste:
				result = "Paste";
				break;
			case ApplicationCommands.CommandId.Undo:
				result = "Undo";
				break;
			case ApplicationCommands.CommandId.Redo:
				result = "Redo";
				break;
			case ApplicationCommands.CommandId.Delete:
				result = "Delete";
				break;
			case ApplicationCommands.CommandId.Find:
				result = "Find";
				break;
			case ApplicationCommands.CommandId.Replace:
				result = "Replace";
				break;
			case ApplicationCommands.CommandId.Help:
				result = "Help";
				break;
			case ApplicationCommands.CommandId.SelectAll:
				result = "SelectAll";
				break;
			case ApplicationCommands.CommandId.New:
				result = "New";
				break;
			case ApplicationCommands.CommandId.Open:
				result = "Open";
				break;
			case ApplicationCommands.CommandId.Save:
				result = "Save";
				break;
			case ApplicationCommands.CommandId.SaveAs:
				result = "SaveAs";
				break;
			case ApplicationCommands.CommandId.Print:
				result = "Print";
				break;
			case ApplicationCommands.CommandId.CancelPrint:
				result = "CancelPrint";
				break;
			case ApplicationCommands.CommandId.PrintPreview:
				result = "PrintPreview";
				break;
			case ApplicationCommands.CommandId.Close:
				result = "Close";
				break;
			case ApplicationCommands.CommandId.Properties:
				result = "Properties";
				break;
			case ApplicationCommands.CommandId.ContextMenu:
				result = "ContextMenu";
				break;
			case ApplicationCommands.CommandId.CorrectionList:
				result = "CorrectionList";
				break;
			case ApplicationCommands.CommandId.Stop:
				result = "Stop";
				break;
			case ApplicationCommands.CommandId.NotACommand:
				result = "NotACommand";
				break;
			}
			return result;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0003848C File Offset: 0x0003788C
		internal static string GetUIText(byte commandId)
		{
			string result = string.Empty;
			switch (commandId)
			{
			case 0:
				result = SR.Get("CutText");
				break;
			case 1:
				result = SR.Get("CopyText");
				break;
			case 2:
				result = SR.Get("PasteText");
				break;
			case 3:
				result = SR.Get("UndoText");
				break;
			case 4:
				result = SR.Get("RedoText");
				break;
			case 5:
				result = SR.Get("DeleteText");
				break;
			case 6:
				result = SR.Get("FindText");
				break;
			case 7:
				result = SR.Get("ReplaceText");
				break;
			case 8:
				result = SR.Get("HelpText");
				break;
			case 9:
				result = SR.Get("SelectAllText");
				break;
			case 10:
				result = SR.Get("NewText");
				break;
			case 11:
				result = SR.Get("OpenText");
				break;
			case 12:
				result = SR.Get("SaveText");
				break;
			case 13:
				result = SR.Get("SaveAsText");
				break;
			case 14:
				result = SR.Get("PrintText");
				break;
			case 15:
				result = SR.Get("CancelPrintText");
				break;
			case 16:
				result = SR.Get("PrintPreviewText");
				break;
			case 17:
				result = SR.Get("CloseText");
				break;
			case 18:
				result = SR.Get("PropertiesText");
				break;
			case 19:
				result = SR.Get("ContextMenuText");
				break;
			case 20:
				result = SR.Get("CorrectionListText");
				break;
			case 21:
				result = SR.Get("StopText");
				break;
			case 22:
				result = SR.Get("NotACommandText");
				break;
			}
			return result;
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0003865C File Offset: 0x00037A5C
		internal static InputGestureCollection LoadDefaultGestureFromResource(byte commandId)
		{
			InputGestureCollection inputGestureCollection = new InputGestureCollection();
			switch (commandId)
			{
			case 0:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("CutKey"), SR.Get("CutKeyDisplayString"), inputGestureCollection);
				break;
			case 1:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("CopyKey"), SR.Get("CopyKeyDisplayString"), inputGestureCollection);
				break;
			case 2:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("PasteKey"), SR.Get("PasteKeyDisplayString"), inputGestureCollection);
				break;
			case 3:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("UndoKey"), SR.Get("UndoKeyDisplayString"), inputGestureCollection);
				break;
			case 4:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("RedoKey"), SR.Get("RedoKeyDisplayString"), inputGestureCollection);
				break;
			case 5:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("DeleteKey"), SR.Get("DeleteKeyDisplayString"), inputGestureCollection);
				break;
			case 6:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("FindKey"), SR.Get("FindKeyDisplayString"), inputGestureCollection);
				break;
			case 7:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("ReplaceKey"), SR.Get("ReplaceKeyDisplayString"), inputGestureCollection);
				break;
			case 8:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("HelpKey"), SR.Get("HelpKeyDisplayString"), inputGestureCollection);
				break;
			case 9:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("SelectAllKey"), SR.Get("SelectAllKeyDisplayString"), inputGestureCollection);
				break;
			case 10:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("NewKey"), SR.Get("NewKeyDisplayString"), inputGestureCollection);
				break;
			case 11:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("OpenKey"), SR.Get("OpenKeyDisplayString"), inputGestureCollection);
				break;
			case 12:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("SaveKey"), SR.Get("SaveKeyDisplayString"), inputGestureCollection);
				break;
			case 14:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("PrintKey"), SR.Get("PrintKeyDisplayString"), inputGestureCollection);
				break;
			case 16:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("PrintPreviewKey"), SR.Get("PrintPreviewKeyDisplayString"), inputGestureCollection);
				break;
			case 18:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("PropertiesKey"), SR.Get("PropertiesKeyDisplayString"), inputGestureCollection);
				break;
			case 19:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("ContextMenuKey"), SR.Get("ContextMenuKeyDisplayString"), inputGestureCollection);
				break;
			case 20:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("CorrectionListKey"), SR.Get("CorrectionListKeyDisplayString"), inputGestureCollection);
				break;
			case 21:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("StopKey"), SR.Get("StopKeyDisplayString"), inputGestureCollection);
				break;
			}
			return inputGestureCollection;
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x00038918 File Offset: 0x00037D18
		private static RoutedUICommand _EnsureCommand(ApplicationCommands.CommandId idCommand)
		{
			if (idCommand >= ApplicationCommands.CommandId.Cut && idCommand < ApplicationCommands.CommandId.Last)
			{
				object syncRoot = ApplicationCommands._internalCommands.SyncRoot;
				lock (syncRoot)
				{
					if (ApplicationCommands._internalCommands[(int)idCommand] == null)
					{
						RoutedUICommand routedUICommand = CommandLibraryHelper.CreateUICommand(ApplicationCommands.GetPropertyName(idCommand), typeof(ApplicationCommands), (byte)idCommand, ApplicationCommands.GetRequiredPermissions(idCommand));
						ApplicationCommands._internalCommands[(int)idCommand] = routedUICommand;
					}
				}
				return ApplicationCommands._internalCommands[(int)idCommand];
			}
			return null;
		}

		// Token: 0x0400085A RID: 2138
		private static RoutedUICommand[] _internalCommands = new RoutedUICommand[23];

		// Token: 0x02000805 RID: 2053
		private enum CommandId : byte
		{
			// Token: 0x040026B4 RID: 9908
			Cut,
			// Token: 0x040026B5 RID: 9909
			Copy,
			// Token: 0x040026B6 RID: 9910
			Paste,
			// Token: 0x040026B7 RID: 9911
			Undo,
			// Token: 0x040026B8 RID: 9912
			Redo,
			// Token: 0x040026B9 RID: 9913
			Delete,
			// Token: 0x040026BA RID: 9914
			Find,
			// Token: 0x040026BB RID: 9915
			Replace,
			// Token: 0x040026BC RID: 9916
			Help,
			// Token: 0x040026BD RID: 9917
			SelectAll,
			// Token: 0x040026BE RID: 9918
			New,
			// Token: 0x040026BF RID: 9919
			Open,
			// Token: 0x040026C0 RID: 9920
			Save,
			// Token: 0x040026C1 RID: 9921
			SaveAs,
			// Token: 0x040026C2 RID: 9922
			Print,
			// Token: 0x040026C3 RID: 9923
			CancelPrint,
			// Token: 0x040026C4 RID: 9924
			PrintPreview,
			// Token: 0x040026C5 RID: 9925
			Close,
			// Token: 0x040026C6 RID: 9926
			Properties,
			// Token: 0x040026C7 RID: 9927
			ContextMenu,
			// Token: 0x040026C8 RID: 9928
			CorrectionList,
			// Token: 0x040026C9 RID: 9929
			Stop,
			// Token: 0x040026CA RID: 9930
			NotACommand,
			// Token: 0x040026CB RID: 9931
			Last
		}
	}
}
