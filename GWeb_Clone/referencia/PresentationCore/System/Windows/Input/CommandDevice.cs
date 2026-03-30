using System;
using System.Security;
using MS.Internal;

namespace System.Windows.Input
{
	// Token: 0x02000208 RID: 520
	internal sealed class CommandDevice : InputDevice
	{
		// Token: 0x06000D92 RID: 3474 RVA: 0x000339E4 File Offset: 0x00032DE4
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal CommandDevice(InputManager inputManager)
		{
			this._inputManager = new SecurityCriticalData<InputManager>(inputManager);
			this._inputManager.Value.PreProcessInput += this.PreProcessInput;
			this._inputManager.Value.PostProcessInput += this.PostProcessInput;
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x00033A3C File Offset: 0x00032E3C
		public override IInputElement Target
		{
			get
			{
				base.VerifyAccess();
				return Keyboard.FocusedElement;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x00033A54 File Offset: 0x00032E54
		public override PresentationSource ActiveSource
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				SecurityHelper.DemandUnrestrictedUIPermission();
				return null;
			}
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00033A68 File Offset: 0x00032E68
		[SecurityCritical]
		private void PreProcessInput(object sender, PreProcessInputEventArgs e)
		{
			InputReportEventArgs inputReportEventArgs = e.StagingItem.Input as InputReportEventArgs;
			if (inputReportEventArgs != null && inputReportEventArgs.Report.Type == InputType.Command)
			{
				RawAppCommandInputReport rawAppCommandInputReport = inputReportEventArgs.Report as RawAppCommandInputReport;
				if (rawAppCommandInputReport != null)
				{
					inputReportEventArgs.Device = this;
					inputReportEventArgs.Source = this.GetSourceFromDevice(rawAppCommandInputReport.Device);
				}
			}
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00033AC0 File Offset: 0x00032EC0
		[SecurityCritical]
		private void PostProcessInput(object sender, ProcessInputEventArgs e)
		{
			if (e.StagingItem.Input.RoutedEvent == InputManager.InputReportEvent)
			{
				if (!e.StagingItem.Input.Handled)
				{
					InputReportEventArgs inputReportEventArgs = e.StagingItem.Input as InputReportEventArgs;
					if (inputReportEventArgs != null)
					{
						RawAppCommandInputReport rawAppCommandInputReport = inputReportEventArgs.Report as RawAppCommandInputReport;
						if (rawAppCommandInputReport != null)
						{
							IInputElement inputElement = e.StagingItem.Input.OriginalSource as IInputElement;
							if (inputElement != null)
							{
								RoutedCommand routedCommand = this.GetRoutedCommand(rawAppCommandInputReport.AppCommand);
								if (routedCommand != null)
								{
									e.PushInput(new CommandDeviceEventArgs(this, rawAppCommandInputReport.Timestamp, routedCommand)
									{
										RoutedEvent = CommandDevice.CommandDeviceEvent,
										Source = inputElement
									}, e.StagingItem);
									return;
								}
							}
						}
					}
				}
			}
			else if (e.StagingItem.Input.RoutedEvent == Keyboard.KeyUpEvent || e.StagingItem.Input.RoutedEvent == Mouse.MouseUpEvent || e.StagingItem.Input.RoutedEvent == Keyboard.GotKeyboardFocusEvent || e.StagingItem.Input.RoutedEvent == Keyboard.LostKeyboardFocusEvent)
			{
				CommandManager.InvalidateRequerySuggested();
			}
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00033BE8 File Offset: 0x00032FE8
		private RoutedCommand GetRoutedCommand(int appCommandId)
		{
			RoutedCommand result = null;
			switch (appCommandId)
			{
			case 1:
				result = NavigationCommands.BrowseBack;
				break;
			case 2:
				result = NavigationCommands.BrowseForward;
				break;
			case 3:
				result = NavigationCommands.Refresh;
				break;
			case 4:
				result = NavigationCommands.BrowseStop;
				break;
			case 5:
				result = NavigationCommands.Search;
				break;
			case 6:
				result = NavigationCommands.Favorites;
				break;
			case 7:
				result = NavigationCommands.BrowseHome;
				break;
			case 8:
				result = MediaCommands.MuteVolume;
				break;
			case 9:
				result = MediaCommands.DecreaseVolume;
				break;
			case 10:
				result = MediaCommands.IncreaseVolume;
				break;
			case 11:
				result = MediaCommands.NextTrack;
				break;
			case 12:
				result = MediaCommands.PreviousTrack;
				break;
			case 13:
				result = MediaCommands.Stop;
				break;
			case 14:
				result = MediaCommands.TogglePlayPause;
				break;
			case 16:
				result = MediaCommands.Select;
				break;
			case 19:
				result = MediaCommands.DecreaseBass;
				break;
			case 20:
				result = MediaCommands.BoostBass;
				break;
			case 21:
				result = MediaCommands.IncreaseBass;
				break;
			case 22:
				result = MediaCommands.DecreaseTreble;
				break;
			case 23:
				result = MediaCommands.IncreaseTreble;
				break;
			case 24:
				result = MediaCommands.MuteMicrophoneVolume;
				break;
			case 25:
				result = MediaCommands.DecreaseMicrophoneVolume;
				break;
			case 26:
				result = MediaCommands.IncreaseMicrophoneVolume;
				break;
			case 27:
				result = ApplicationCommands.Help;
				break;
			case 28:
				result = ApplicationCommands.Find;
				break;
			case 29:
				result = ApplicationCommands.New;
				break;
			case 30:
				result = ApplicationCommands.Open;
				break;
			case 31:
				result = ApplicationCommands.Close;
				break;
			case 32:
				result = ApplicationCommands.Save;
				break;
			case 33:
				result = ApplicationCommands.Print;
				break;
			case 34:
				result = ApplicationCommands.Undo;
				break;
			case 35:
				result = ApplicationCommands.Redo;
				break;
			case 36:
				result = ApplicationCommands.Copy;
				break;
			case 37:
				result = ApplicationCommands.Cut;
				break;
			case 38:
				result = ApplicationCommands.Paste;
				break;
			case 44:
				result = MediaCommands.ToggleMicrophoneOnOff;
				break;
			case 45:
				result = ApplicationCommands.CorrectionList;
				break;
			case 46:
				result = MediaCommands.Play;
				break;
			case 47:
				result = MediaCommands.Pause;
				break;
			case 48:
				result = MediaCommands.Record;
				break;
			case 49:
				result = MediaCommands.FastForward;
				break;
			case 50:
				result = MediaCommands.Rewind;
				break;
			case 51:
				result = MediaCommands.ChannelUp;
				break;
			case 52:
				result = MediaCommands.ChannelDown;
				break;
			}
			return result;
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00033E8C File Offset: 0x0003328C
		private IInputElement GetSourceFromDevice(InputType device)
		{
			if (device == InputType.Mouse)
			{
				return Mouse.DirectlyOver;
			}
			return Keyboard.FocusedElement;
		}

		// Token: 0x04000818 RID: 2072
		internal static readonly RoutedEvent CommandDeviceEvent = EventManager.RegisterRoutedEvent("CommandDevice", RoutingStrategy.Bubble, typeof(CommandDeviceEventHandler), typeof(CommandDevice));

		// Token: 0x04000819 RID: 2073
		private SecurityCriticalData<InputManager> _inputManager;
	}
}
