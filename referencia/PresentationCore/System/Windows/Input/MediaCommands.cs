using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Fornece um conjunto padrão de comandos relacionados à mídia.</summary>
	// Token: 0x02000226 RID: 550
	public static class MediaCommands
	{
		/// <summary>Obtém o valor que representa o comando Reproduzir.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  reproduzir</returns>
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x000393F0 File Offset: 0x000387F0
		public static RoutedUICommand Play
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.Play);
			}
		}

		/// <summary>Obtém o valor que representa o comando Pausar.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  pausar</returns>
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x00039404 File Offset: 0x00038804
		public static RoutedUICommand Pause
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.Pause);
			}
		}

		/// <summary>Obtém o valor que representa o comando Parar.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  parar</returns>
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x00039418 File Offset: 0x00038818
		public static RoutedUICommand Stop
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.Stop);
			}
		}

		/// <summary>Obtém o valor que representa o comando Gravar.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  registro</returns>
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x0003942C File Offset: 0x0003882C
		public static RoutedUICommand Record
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.Record);
			}
		}

		/// <summary>Obtém o valor que representa o comando Próxima Faixa.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  próxima faixa</returns>
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x00039440 File Offset: 0x00038840
		public static RoutedUICommand NextTrack
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.NextTrack);
			}
		}

		/// <summary>Obtém o valor que representa o comando Faixa Anterior.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  faixa anterior</returns>
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x00039454 File Offset: 0x00038854
		public static RoutedUICommand PreviousTrack
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.PreviousTrack);
			}
		}

		/// <summary>Obtém o valor que representa o comando Avançar.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  avanço rápido</returns>
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x00039468 File Offset: 0x00038868
		public static RoutedUICommand FastForward
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.FastForward);
			}
		}

		/// <summary>Obtém o valor que representa o comando Retroceder.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  Rewind</returns>
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x0003947C File Offset: 0x0003887C
		public static RoutedUICommand Rewind
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.Rewind);
			}
		}

		/// <summary>Obtém o valor que representa o comando Subir Canal.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  canal acima</returns>
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x00039490 File Offset: 0x00038890
		public static RoutedUICommand ChannelUp
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.ChannelUp);
			}
		}

		/// <summary>Obtém o valor que representa o comando Baixar Canal.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  canal abaixo</returns>
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x000394A4 File Offset: 0x000388A4
		public static RoutedUICommand ChannelDown
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.ChannelDown);
			}
		}

		/// <summary>Obtém o valor que representa o comando Alternar Executar/Pausar.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  alternar executar pausar</returns>
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x000394B8 File Offset: 0x000388B8
		public static RoutedUICommand TogglePlayPause
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.TogglePlayPause);
			}
		}

		/// <summary>Obtém o valor que representa o comando Selecionar.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  selecione</returns>
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x000394CC File Offset: 0x000388CC
		public static RoutedUICommand Select
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.Select);
			}
		}

		/// <summary>Obtém o valor que representa o comando Aumentar o Volume.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  Aumentar Volume</returns>
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x000394E0 File Offset: 0x000388E0
		public static RoutedUICommand IncreaseVolume
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.IncreaseVolume);
			}
		}

		/// <summary>Obtém o valor que representa o comando Diminuir o Volume.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  Diminuir Volume</returns>
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x000394F4 File Offset: 0x000388F4
		public static RoutedUICommand DecreaseVolume
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.DecreaseVolume);
			}
		}

		/// <summary>Obtém o valor que representa o comando Ativar Mudo.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  mudo do Volume</returns>
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x00039508 File Offset: 0x00038908
		public static RoutedUICommand MuteVolume
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.MuteVolume);
			}
		}

		/// <summary>Obtém o valor que representa o comando Aumentar Agudos.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  aumentar agudos</returns>
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0003951C File Offset: 0x0003891C
		public static RoutedUICommand IncreaseTreble
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.IncreaseTreble);
			}
		}

		/// <summary>Obtém o valor que representa o comando Diminuir Agudos.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  diminuir agudos</returns>
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x00039530 File Offset: 0x00038930
		public static RoutedUICommand DecreaseTreble
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.DecreaseTreble);
			}
		}

		/// <summary>Obtém o valor que representa o comando Aumentar Graves.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  aumentar graves</returns>
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x00039544 File Offset: 0x00038944
		public static RoutedUICommand IncreaseBass
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.IncreaseBass);
			}
		}

		/// <summary>Obtém o valor que representa o comando Diminuir Graves.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  diminuir graves</returns>
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x00039558 File Offset: 0x00038958
		public static RoutedUICommand DecreaseBass
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.DecreaseBass);
			}
		}

		/// <summary>Obtém o valor que representa o comando de Aumentar Base.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  aumentar graves</returns>
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x0003956C File Offset: 0x0003896C
		public static RoutedUICommand BoostBass
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.BoostBass);
			}
		}

		/// <summary>Obtém o valor que representa o comando Aumentar o Volume do Microfone.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  Aumentar Volume do microfone</returns>
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x00039580 File Offset: 0x00038980
		public static RoutedUICommand IncreaseMicrophoneVolume
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.IncreaseMicrophoneVolume);
			}
		}

		/// <summary>Obtém o valor que representa o comando Diminuir o Volume do Microfone.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  diminuir o Volume do microfone</returns>
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x00039594 File Offset: 0x00038994
		public static RoutedUICommand DecreaseMicrophoneVolume
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.DecreaseMicrophoneVolume);
			}
		}

		/// <summary>Obtém o valor que representa o comando Ativar Mudo do Volume do Microfone.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  Vypnutí Hlasitosti Mikrofonu</returns>
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x000395A8 File Offset: 0x000389A8
		public static RoutedUICommand MuteMicrophoneVolume
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.MuteMicrophoneVolume);
			}
		}

		/// <summary>Obtém o valor que representa o comando Ligar/Desligar Microfone.</summary>
		/// <returns>O comando.  
		///   Valores padrão   gesto de chave  nenhum gesto definido.   Texto da interface do usuário  ativar/desativar microfone</returns>
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x000395BC File Offset: 0x000389BC
		public static RoutedUICommand ToggleMicrophoneOnOff
		{
			get
			{
				return MediaCommands._EnsureCommand(MediaCommands.CommandId.ToggleMicrophoneOnOff);
			}
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x000395D0 File Offset: 0x000389D0
		private static string GetPropertyName(MediaCommands.CommandId commandId)
		{
			string result = string.Empty;
			switch (commandId)
			{
			case MediaCommands.CommandId.Play:
				result = "Play";
				break;
			case MediaCommands.CommandId.Pause:
				result = "Pause";
				break;
			case MediaCommands.CommandId.Stop:
				result = "Stop";
				break;
			case MediaCommands.CommandId.Record:
				result = "Record";
				break;
			case MediaCommands.CommandId.NextTrack:
				result = "NextTrack";
				break;
			case MediaCommands.CommandId.PreviousTrack:
				result = "PreviousTrack";
				break;
			case MediaCommands.CommandId.FastForward:
				result = "FastForward";
				break;
			case MediaCommands.CommandId.Rewind:
				result = "Rewind";
				break;
			case MediaCommands.CommandId.ChannelUp:
				result = "ChannelUp";
				break;
			case MediaCommands.CommandId.ChannelDown:
				result = "ChannelDown";
				break;
			case MediaCommands.CommandId.TogglePlayPause:
				result = "TogglePlayPause";
				break;
			case MediaCommands.CommandId.IncreaseVolume:
				result = "IncreaseVolume";
				break;
			case MediaCommands.CommandId.DecreaseVolume:
				result = "DecreaseVolume";
				break;
			case MediaCommands.CommandId.MuteVolume:
				result = "MuteVolume";
				break;
			case MediaCommands.CommandId.IncreaseTreble:
				result = "IncreaseTreble";
				break;
			case MediaCommands.CommandId.DecreaseTreble:
				result = "DecreaseTreble";
				break;
			case MediaCommands.CommandId.IncreaseBass:
				result = "IncreaseBass";
				break;
			case MediaCommands.CommandId.DecreaseBass:
				result = "DecreaseBass";
				break;
			case MediaCommands.CommandId.BoostBass:
				result = "BoostBass";
				break;
			case MediaCommands.CommandId.IncreaseMicrophoneVolume:
				result = "IncreaseMicrophoneVolume";
				break;
			case MediaCommands.CommandId.DecreaseMicrophoneVolume:
				result = "DecreaseMicrophoneVolume";
				break;
			case MediaCommands.CommandId.MuteMicrophoneVolume:
				result = "MuteMicrophoneVolume";
				break;
			case MediaCommands.CommandId.ToggleMicrophoneOnOff:
				result = "ToggleMicrophoneOnOff";
				break;
			case MediaCommands.CommandId.Select:
				result = "Select";
				break;
			}
			return result;
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0003972C File Offset: 0x00038B2C
		internal static string GetUIText(byte commandId)
		{
			string result = string.Empty;
			switch (commandId)
			{
			case 1:
				result = SR.Get("MediaPlayText");
				break;
			case 2:
				result = SR.Get("MediaPauseText");
				break;
			case 3:
				result = SR.Get("MediaStopText");
				break;
			case 4:
				result = SR.Get("MediaRecordText");
				break;
			case 5:
				result = SR.Get("MediaNextTrackText");
				break;
			case 6:
				result = SR.Get("MediaPreviousTrackText");
				break;
			case 7:
				result = SR.Get("MediaFastForwardText");
				break;
			case 8:
				result = SR.Get("MediaRewindText");
				break;
			case 9:
				result = SR.Get("MediaChannelUpText");
				break;
			case 10:
				result = SR.Get("MediaChannelDownText");
				break;
			case 11:
				result = SR.Get("MediaTogglePlayPauseText");
				break;
			case 12:
				result = SR.Get("MediaIncreaseVolumeText");
				break;
			case 13:
				result = SR.Get("MediaDecreaseVolumeText");
				break;
			case 14:
				result = SR.Get("MediaMuteVolumeText");
				break;
			case 15:
				result = SR.Get("MediaIncreaseTrebleText");
				break;
			case 16:
				result = SR.Get("MediaDecreaseTrebleText");
				break;
			case 17:
				result = SR.Get("MediaIncreaseBassText");
				break;
			case 18:
				result = SR.Get("MediaDecreaseBassText");
				break;
			case 19:
				result = SR.Get("MediaBoostBassText");
				break;
			case 20:
				result = SR.Get("MediaIncreaseMicrophoneVolumeText");
				break;
			case 21:
				result = SR.Get("MediaDecreaseMicrophoneVolumeText");
				break;
			case 22:
				result = SR.Get("MediaMuteMicrophoneVolumeText");
				break;
			case 23:
				result = SR.Get("MediaToggleMicrophoneOnOffText");
				break;
			case 24:
				result = SR.Get("MediaSelectText");
				break;
			}
			return result;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00039910 File Offset: 0x00038D10
		internal static InputGestureCollection LoadDefaultGestureFromResource(byte commandId)
		{
			InputGestureCollection inputGestureCollection = new InputGestureCollection();
			switch (commandId)
			{
			case 1:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaPlayKey"), SR.Get("MediaPlayKeyDisplayString"), inputGestureCollection);
				break;
			case 2:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaPauseKey"), SR.Get("MediaPauseKeyDisplayString"), inputGestureCollection);
				break;
			case 3:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaStopKey"), SR.Get("MediaStopKeyDisplayString"), inputGestureCollection);
				break;
			case 4:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaRecordKey"), SR.Get("MediaRecordKeyDisplayString"), inputGestureCollection);
				break;
			case 5:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaNextTrackKey"), SR.Get("MediaNextTrackKeyDisplayString"), inputGestureCollection);
				break;
			case 6:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaPreviousTrackKey"), SR.Get("MediaPreviousTrackKeyDisplayString"), inputGestureCollection);
				break;
			case 7:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaFastForwardKey"), SR.Get("MediaFastForwardKeyDisplayString"), inputGestureCollection);
				break;
			case 8:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaRewindKey"), SR.Get("MediaRewindKeyDisplayString"), inputGestureCollection);
				break;
			case 9:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaChannelUpKey"), SR.Get("MediaChannelUpKeyDisplayString"), inputGestureCollection);
				break;
			case 10:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaChannelDownKey"), SR.Get("MediaChannelDownKeyDisplayString"), inputGestureCollection);
				break;
			case 11:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaTogglePlayPauseKey"), SR.Get("MediaTogglePlayPauseKeyDisplayString"), inputGestureCollection);
				break;
			case 12:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaIncreaseVolumeKey"), SR.Get("MediaIncreaseVolumeKeyDisplayString"), inputGestureCollection);
				break;
			case 13:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaDecreaseVolumeKey"), SR.Get("MediaDecreaseVolumeKeyDisplayString"), inputGestureCollection);
				break;
			case 14:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaMuteVolumeKey"), SR.Get("MediaMuteVolumeKeyDisplayString"), inputGestureCollection);
				break;
			case 15:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaIncreaseTrebleKey"), SR.Get("MediaIncreaseTrebleKeyDisplayString"), inputGestureCollection);
				break;
			case 16:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaDecreaseTrebleKey"), SR.Get("MediaDecreaseTrebleKeyDisplayString"), inputGestureCollection);
				break;
			case 17:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaIncreaseBassKey"), SR.Get("MediaIncreaseBassKeyDisplayString"), inputGestureCollection);
				break;
			case 18:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaDecreaseBassKey"), SR.Get("MediaDecreaseBassKeyDisplayString"), inputGestureCollection);
				break;
			case 19:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaBoostBassKey"), SR.Get("MediaBoostBassKeyDisplayString"), inputGestureCollection);
				break;
			case 20:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaIncreaseMicrophoneVolumeKey"), SR.Get("MediaIncreaseMicrophoneVolumeKeyDisplayString"), inputGestureCollection);
				break;
			case 21:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaDecreaseMicrophoneVolumeKey"), SR.Get("MediaDecreaseMicrophoneVolumeKeyDisplayString"), inputGestureCollection);
				break;
			case 22:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaMuteMicrophoneVolumeKey"), SR.Get("MediaMuteMicrophoneVolumeKeyDisplayString"), inputGestureCollection);
				break;
			case 23:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaToggleMicrophoneOnOffKey"), SR.Get("MediaToggleMicrophoneOnOffKeyDisplayString"), inputGestureCollection);
				break;
			case 24:
				KeyGesture.AddGesturesFromResourceStrings(SR.Get("MediaSelectKey"), SR.Get("MediaSelectKeyDisplayString"), inputGestureCollection);
				break;
			}
			return inputGestureCollection;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x00039C6C File Offset: 0x0003906C
		private static RoutedUICommand _EnsureCommand(MediaCommands.CommandId idCommand)
		{
			if (idCommand >= (MediaCommands.CommandId)0 && idCommand < MediaCommands.CommandId.Last)
			{
				object syncRoot = MediaCommands._internalCommands.SyncRoot;
				lock (syncRoot)
				{
					if (MediaCommands._internalCommands[(int)idCommand] == null)
					{
						RoutedUICommand routedUICommand = new RoutedUICommand(MediaCommands.GetPropertyName(idCommand), typeof(MediaCommands), (byte)idCommand);
						routedUICommand.AreInputGesturesDelayLoaded = true;
						MediaCommands._internalCommands[(int)idCommand] = routedUICommand;
					}
				}
				return MediaCommands._internalCommands[(int)idCommand];
			}
			return null;
		}

		// Token: 0x0400085C RID: 2140
		private static RoutedUICommand[] _internalCommands = new RoutedUICommand[25];

		// Token: 0x02000807 RID: 2055
		private enum CommandId : byte
		{
			// Token: 0x040026EA RID: 9962
			Play = 1,
			// Token: 0x040026EB RID: 9963
			Pause,
			// Token: 0x040026EC RID: 9964
			Stop,
			// Token: 0x040026ED RID: 9965
			Record,
			// Token: 0x040026EE RID: 9966
			NextTrack,
			// Token: 0x040026EF RID: 9967
			PreviousTrack,
			// Token: 0x040026F0 RID: 9968
			FastForward,
			// Token: 0x040026F1 RID: 9969
			Rewind,
			// Token: 0x040026F2 RID: 9970
			ChannelUp,
			// Token: 0x040026F3 RID: 9971
			ChannelDown,
			// Token: 0x040026F4 RID: 9972
			TogglePlayPause,
			// Token: 0x040026F5 RID: 9973
			IncreaseVolume,
			// Token: 0x040026F6 RID: 9974
			DecreaseVolume,
			// Token: 0x040026F7 RID: 9975
			MuteVolume,
			// Token: 0x040026F8 RID: 9976
			IncreaseTreble,
			// Token: 0x040026F9 RID: 9977
			DecreaseTreble,
			// Token: 0x040026FA RID: 9978
			IncreaseBass,
			// Token: 0x040026FB RID: 9979
			DecreaseBass,
			// Token: 0x040026FC RID: 9980
			BoostBass,
			// Token: 0x040026FD RID: 9981
			IncreaseMicrophoneVolume,
			// Token: 0x040026FE RID: 9982
			DecreaseMicrophoneVolume,
			// Token: 0x040026FF RID: 9983
			MuteMicrophoneVolume,
			// Token: 0x04002700 RID: 9984
			ToggleMicrophoneOnOff,
			// Token: 0x04002701 RID: 9985
			Select,
			// Token: 0x04002702 RID: 9986
			Last
		}
	}
}
