using System;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Fornece um conjunto padrão de comandos relacionados a navegação.</summary>
	// Token: 0x02000227 RID: 551
	public static class NavigationCommands
	{
		/// <summary>Obtém o valor que representa o comando <see langword="Browse Back" />.</summary>
		/// <returns>O comando Interface de Usuário roteado.  
		///   Valores padrão   gesto de chave  ALT + LEFT  interface do usuário Texto  novamente</returns>
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00039D14 File Offset: 0x00039114
		public static RoutedUICommand BrowseBack
		{
			get
			{
				return NavigationCommands._EnsureCommand(NavigationCommands.CommandId.BrowseBack);
			}
		}

		/// <summary>Obtém o valor que representa o comando <see langword="Browse Forward" />.</summary>
		/// <returns>O comando Interface de Usuário roteado.  
		///   Valores padrão   gesto de chave  ALT + RIGHT  interface do usuário Texto  para frente</returns>
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x00039D28 File Offset: 0x00039128
		public static RoutedUICommand BrowseForward
		{
			get
			{
				return NavigationCommands._EnsureCommand(NavigationCommands.CommandId.BrowseForward);
			}
		}

		/// <summary>Obtém o valor que representa o comando <see langword="Browse Home" />.</summary>
		/// <returns>O comando Interface de Usuário roteado.  
		///   Valores padrão   gesto de chave  ALT + HOME  interface do usuário Texto  Home</returns>
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x00039D3C File Offset: 0x0003913C
		public static RoutedUICommand BrowseHome
		{
			get
			{
				return NavigationCommands._EnsureCommand(NavigationCommands.CommandId.BrowseHome);
			}
		}

		/// <summary>Obtém o valor que representa o comando <see langword="Browse Stop" />.</summary>
		/// <returns>O comando Interface de Usuário roteado.  
		///   Valores padrão   gesto de chave  ALT + ESC  texto de interface do usuário   Parar</returns>
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x00039D50 File Offset: 0x00039150
		public static RoutedUICommand BrowseStop
		{
			get
			{
				return NavigationCommands._EnsureCommand(NavigationCommands.CommandId.BrowseStop);
			}
		}

		/// <summary>Obtém o valor que representa o comando <see langword="Refresh" />.</summary>
		/// <returns>O comando Interface de Usuário roteado.  
		///   Valores padrão   gesto de chave  F5  detextodeinterfacedousuário Atualizar</returns>
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x00039D64 File Offset: 0x00039164
		public static RoutedUICommand Refresh
		{
			get
			{
				return NavigationCommands._EnsureCommand(NavigationCommands.CommandId.Refresh);
			}
		}

		/// <summary>Obtém o valor que representa o comando <see langword="Favorites" />.</summary>
		/// <returns>O comando Interface de Usuário roteado.  
		///   Valores padrão   gesto de chave  CTRL + I  texto de interface do usuário   Favoritos</returns>
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x00039D78 File Offset: 0x00039178
		public static RoutedUICommand Favorites
		{
			get
			{
				return NavigationCommands._EnsureCommand(NavigationCommands.CommandId.Favorites);
			}
		}

		/// <summary>Obtém o valor que representa o comando <see langword="Search" />.</summary>
		/// <returns>O comando Interface de Usuário roteado.  
		///   Valores padrão   gesto de chave  F3  detextodeinterfacedousuário Pesquisa</returns>
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x00039D8C File Offset: 0x0003918C
		public static RoutedUICommand Search
		{
			get
			{
				return NavigationCommands._EnsureCommand(NavigationCommands.CommandId.Search);
			}
		}

		/// <summary>Obtém o valor que representa o comando <see langword="Increase Zoom" />.</summary>
		/// <returns>O comando Interface de Usuário roteado.  
		///   Valores padrão   gesto de chave  n/d  texto de interface do usuário   Aumentar o Zoom</returns>
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x00039DA0 File Offset: 0x000391A0
		public static RoutedUICommand IncreaseZoom
		{
			get
			{
				return NavigationCommands._EnsureCommand(NavigationCommands.CommandId.IncreaseZoom);
			}
		}

		/// <summary>Obtém o valor que representa o comando <see langword="Decrease Zoom" />.</summary>
		/// <returns>O comando Interface de Usuário roteado.  
		///   Valores padrão   gesto de chave  n/d  texto de interface do usuário   Diminuir o Zoom</returns>
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x00039DB4 File Offset: 0x000391B4
		public static RoutedUICommand DecreaseZoom
		{
			get
			{
				return NavigationCommands._EnsureCommand(NavigationCommands.CommandId.DecreaseZoom);
			}
		}

		/// <summary>Obtém o valor que representa o comando <see langword="Zoom" />.</summary>
		/// <returns>O comando Interface de Usuário roteado.  
		///   Valores padrão   gesto de chave  n/d  texto de interface do usuário   Zoom</returns>
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x00039DC8 File Offset: 0x000391C8
		public static RoutedUICommand Zoom
		{
			get
			{
				return NavigationCommands._EnsureCommand(NavigationCommands.CommandId.Zoom);
			}
		}

		/// <summary>Obtém o valor que representa o comando <see langword="Next Page" />.</summary>
		/// <returns>O comando Interface de Usuário roteado.  
		///   Valores padrão   gesto de chave  n/d  texto de interface do usuário   Próxima página</returns>
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x00039DDC File Offset: 0x000391DC
		public static RoutedUICommand NextPage
		{
			get
			{
				return NavigationCommands._EnsureCommand(NavigationCommands.CommandId.NextPage);
			}
		}

		/// <summary>Obtém o valor que representa o comando <see langword="Previous Page" />.</summary>
		/// <returns>O comando Interface de Usuário roteado.  
		///   Valores padrão   gesto de chave  n/d  texto de interface do usuário   Página anterior</returns>
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x00039DF0 File Offset: 0x000391F0
		public static RoutedUICommand PreviousPage
		{
			get
			{
				return NavigationCommands._EnsureCommand(NavigationCommands.CommandId.PreviousPage);
			}
		}

		/// <summary>Obtém o valor que representa o comando <see langword="First Page" />.</summary>
		/// <returns>O comando Interface de Usuário roteado.  
		///   Valores padrão   gesto de chave  n/d  texto de interface do usuário   Da primeira página</returns>
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x00039E04 File Offset: 0x00039204
		public static RoutedUICommand FirstPage
		{
			get
			{
				return NavigationCommands._EnsureCommand(NavigationCommands.CommandId.FirstPage);
			}
		}

		/// <summary>Obtém o valor que representa o comando <see langword="Last Page" />.</summary>
		/// <returns>O comando Interface de Usuário roteado.  
		///   Valores padrão   gesto de chave  n/d  texto de interface do usuário   Última página</returns>
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x00039E18 File Offset: 0x00039218
		public static RoutedUICommand LastPage
		{
			get
			{
				return NavigationCommands._EnsureCommand(NavigationCommands.CommandId.LastPage);
			}
		}

		/// <summary>Obtém o valor que representa o comando <see langword="Go To Page" />.</summary>
		/// <returns>O comando Interface de Usuário roteado.  
		///   Valores padrão   gesto de chave  n/d  texto de interface do usuário   Ir para página</returns>
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000F3B RID: 3899 RVA: 0x00039E2C File Offset: 0x0003922C
		public static RoutedUICommand GoToPage
		{
			get
			{
				return NavigationCommands._EnsureCommand(NavigationCommands.CommandId.GoToPage);
			}
		}

		/// <summary>Obtém o valor que representa o comando <see langword="Navigate Journal" />.</summary>
		/// <returns>O comando Interface de Usuário roteado.  
		///   Valores padrão   gesto de chave  n/d  texto de interface do usuário   Diário de navegação</returns>
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x00039E40 File Offset: 0x00039240
		public static RoutedUICommand NavigateJournal
		{
			get
			{
				return NavigationCommands._EnsureCommand(NavigationCommands.CommandId.NavigateJournal);
			}
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00039E54 File Offset: 0x00039254
		private static string GetPropertyName(NavigationCommands.CommandId commandId)
		{
			string result = string.Empty;
			switch (commandId)
			{
			case NavigationCommands.CommandId.BrowseBack:
				result = "BrowseBack";
				break;
			case NavigationCommands.CommandId.BrowseForward:
				result = "BrowseForward";
				break;
			case NavigationCommands.CommandId.BrowseHome:
				result = "BrowseHome";
				break;
			case NavigationCommands.CommandId.BrowseStop:
				result = "BrowseStop";
				break;
			case NavigationCommands.CommandId.Refresh:
				result = "Refresh";
				break;
			case NavigationCommands.CommandId.Favorites:
				result = "Favorites";
				break;
			case NavigationCommands.CommandId.Search:
				result = "Search";
				break;
			case NavigationCommands.CommandId.IncreaseZoom:
				result = "IncreaseZoom";
				break;
			case NavigationCommands.CommandId.DecreaseZoom:
				result = "DecreaseZoom";
				break;
			case NavigationCommands.CommandId.Zoom:
				result = "Zoom";
				break;
			case NavigationCommands.CommandId.NextPage:
				result = "NextPage";
				break;
			case NavigationCommands.CommandId.PreviousPage:
				result = "PreviousPage";
				break;
			case NavigationCommands.CommandId.FirstPage:
				result = "FirstPage";
				break;
			case NavigationCommands.CommandId.LastPage:
				result = "LastPage";
				break;
			case NavigationCommands.CommandId.GoToPage:
				result = "GoToPage";
				break;
			case NavigationCommands.CommandId.NavigateJournal:
				result = "NavigateJournal";
				break;
			}
			return result;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x00039F38 File Offset: 0x00039338
		internal static string GetUIText(byte commandId)
		{
			string result = string.Empty;
			switch (commandId)
			{
			case 1:
				result = SR.Get("BrowseBackText");
				break;
			case 2:
				result = SR.Get("BrowseForwardText");
				break;
			case 3:
				result = SR.Get("BrowseHomeText");
				break;
			case 4:
				result = SR.Get("BrowseStopText");
				break;
			case 5:
				result = SR.Get("RefreshText");
				break;
			case 6:
				result = SR.Get("FavoritesText");
				break;
			case 7:
				result = SR.Get("SearchText");
				break;
			case 8:
				result = SR.Get("IncreaseZoomText");
				break;
			case 9:
				result = SR.Get("DecreaseZoomText");
				break;
			case 10:
				result = SR.Get("ZoomText");
				break;
			case 11:
				result = SR.Get("NextPageText");
				break;
			case 12:
				result = SR.Get("PreviousPageText");
				break;
			case 13:
				result = SR.Get("FirstPageText");
				break;
			case 14:
				result = SR.Get("LastPageText");
				break;
			case 15:
				result = SR.Get("GoToPageText");
				break;
			case 16:
				result = SR.Get("NavigateJournalText");
				break;
			}
			return result;
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0003A07C File Offset: 0x0003947C
		internal static InputGestureCollection LoadDefaultGestureFromResource(byte commandId)
		{
			InputGestureCollection inputGestureCollection = new InputGestureCollection();
			switch (commandId)
			{
			case 1:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("BrowseBackKey"), SR.Get("BrowseBackKeyDisplayString"), inputGestureCollection);
				break;
			case 2:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("BrowseForwardKey"), SR.Get("BrowseForwardKeyDisplayString"), inputGestureCollection);
				break;
			case 3:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("BrowseHomeKey"), SR.Get("BrowseHomeKeyDisplayString"), inputGestureCollection);
				break;
			case 4:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("BrowseStopKey"), SR.Get("BrowseStopKeyDisplayString"), inputGestureCollection);
				break;
			case 5:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("RefreshKey"), SR.Get("RefreshKeyDisplayString"), inputGestureCollection);
				break;
			case 6:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("FavoritesKey"), SR.Get("FavoritesKeyDisplayString"), inputGestureCollection);
				break;
			case 7:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("SearchKey"), SR.Get("SearchKeyDisplayString"), inputGestureCollection);
				break;
			case 8:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("IncreaseZoomKey"), SR.Get("IncreaseZoomKeyDisplayString"), inputGestureCollection);
				break;
			case 9:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("DecreaseZoomKey"), SR.Get("DecreaseZoomKeyDisplayString"), inputGestureCollection);
				break;
			case 10:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("ZoomKey"), SR.Get("ZoomKeyDisplayString"), inputGestureCollection);
				break;
			case 11:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("NextPageKey"), SR.Get("NextPageKeyDisplayString"), inputGestureCollection);
				break;
			case 12:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("PreviousPageKey"), SR.Get("PreviousPageKeyDisplayString"), inputGestureCollection);
				break;
			case 13:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("FirstPageKey"), SR.Get("FirstPageKeyDisplayString"), inputGestureCollection);
				break;
			case 14:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("LastPageKey"), SR.Get("LastPageKeyDisplayString"), inputGestureCollection);
				break;
			case 15:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("GoToPageKey"), SR.Get("GoToPageKeyDisplayString"), inputGestureCollection);
				break;
			case 16:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("NavigateJournalKey"), SR.Get("NavigateJournalKeyDisplayString"), inputGestureCollection);
				break;
			}
			return inputGestureCollection;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0003A2C0 File Offset: 0x000396C0
		private static RoutedUICommand _EnsureCommand(NavigationCommands.CommandId idCommand)
		{
			if (idCommand >= (NavigationCommands.CommandId)0 && idCommand < NavigationCommands.CommandId.Last)
			{
				object syncRoot = NavigationCommands._internalCommands.SyncRoot;
				lock (syncRoot)
				{
					if (NavigationCommands._internalCommands[(int)idCommand] == null)
					{
						RoutedUICommand routedUICommand = CommandLibraryHelper.CreateUICommand(NavigationCommands.GetPropertyName(idCommand), typeof(NavigationCommands), (byte)idCommand, null);
						NavigationCommands._internalCommands[(int)idCommand] = routedUICommand;
					}
				}
				return NavigationCommands._internalCommands[(int)idCommand];
			}
			return null;
		}

		// Token: 0x0400085D RID: 2141
		private static RoutedUICommand[] _internalCommands = new RoutedUICommand[17];

		// Token: 0x02000808 RID: 2056
		private enum CommandId : byte
		{
			// Token: 0x04002704 RID: 9988
			BrowseBack = 1,
			// Token: 0x04002705 RID: 9989
			BrowseForward,
			// Token: 0x04002706 RID: 9990
			BrowseHome,
			// Token: 0x04002707 RID: 9991
			BrowseStop,
			// Token: 0x04002708 RID: 9992
			Refresh,
			// Token: 0x04002709 RID: 9993
			Favorites,
			// Token: 0x0400270A RID: 9994
			Search,
			// Token: 0x0400270B RID: 9995
			IncreaseZoom,
			// Token: 0x0400270C RID: 9996
			DecreaseZoom,
			// Token: 0x0400270D RID: 9997
			Zoom,
			// Token: 0x0400270E RID: 9998
			NextPage,
			// Token: 0x0400270F RID: 9999
			PreviousPage,
			// Token: 0x04002710 RID: 10000
			FirstPage,
			// Token: 0x04002711 RID: 10001
			LastPage,
			// Token: 0x04002712 RID: 10002
			GoToPage,
			// Token: 0x04002713 RID: 10003
			NavigateJournal,
			// Token: 0x04002714 RID: 10004
			Last
		}
	}
}
