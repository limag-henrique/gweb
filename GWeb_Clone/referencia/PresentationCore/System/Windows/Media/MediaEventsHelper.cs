using System;
using System.IO;
using System.Security;
using System.Text;
using System.Windows.Threading;
using MS.Internal;

namespace System.Windows.Media
{
	// Token: 0x02000425 RID: 1061
	internal class MediaEventsHelper : IInvokable
	{
		// Token: 0x06002AF2 RID: 10994 RVA: 0x000ABD68 File Offset: 0x000AB168
		internal MediaEventsHelper(MediaPlayer mediaPlayer)
		{
			this._mediaOpened = new DispatcherOperationCallback(this.OnMediaOpened);
			this.DispatcherMediaOpened += this._mediaOpened;
			this._mediaFailed = new DispatcherOperationCallback(this.OnMediaFailed);
			this.DispatcherMediaFailed += this._mediaFailed;
			this._mediaPrerolled = new DispatcherOperationCallback(this.OnMediaPrerolled);
			this.DispatcherMediaPrerolled += this._mediaPrerolled;
			this._mediaEnded = new DispatcherOperationCallback(this.OnMediaEnded);
			this.DispatcherMediaEnded += this._mediaEnded;
			this._bufferingStarted = new DispatcherOperationCallback(this.OnBufferingStarted);
			this.DispatcherBufferingStarted += this._bufferingStarted;
			this._bufferingEnded = new DispatcherOperationCallback(this.OnBufferingEnded);
			this.DispatcherBufferingEnded += this._bufferingEnded;
			this._scriptCommand = new DispatcherOperationCallback(this.OnScriptCommand);
			this.DispatcherScriptCommand += this._scriptCommand;
			this._newFrame = new DispatcherOperationCallback(this.OnNewFrame);
			this.DispatcherMediaNewFrame += this._newFrame;
			this.SetSender(mediaPlayer);
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x000ABECC File Offset: 0x000AB2CC
		[SecurityCritical]
		internal static void CreateMediaEventsHelper(MediaPlayer mediaPlayer, out MediaEventsHelper eventsHelper, out SafeMILHandle unmanagedProxy)
		{
			eventsHelper = new MediaEventsHelper(mediaPlayer);
			unmanagedProxy = EventProxyWrapper.CreateEventProxyWrapper(eventsHelper);
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x000ABEEC File Offset: 0x000AB2EC
		internal void SetSender(MediaPlayer sender)
		{
			this._sender = sender;
			this._dispatcher = sender.Dispatcher;
		}

		// Token: 0x1400019A RID: 410
		// (add) Token: 0x06002AF5 RID: 10997 RVA: 0x000ABF0C File Offset: 0x000AB30C
		// (remove) Token: 0x06002AF6 RID: 10998 RVA: 0x000ABF28 File Offset: 0x000AB328
		internal event EventHandler<ExceptionEventArgs> MediaFailed
		{
			add
			{
				this._mediaFailedHelper.AddEvent(value);
			}
			remove
			{
				this._mediaFailedHelper.RemoveEvent(value);
			}
		}

		// Token: 0x1400019B RID: 411
		// (add) Token: 0x06002AF7 RID: 10999 RVA: 0x000ABF44 File Offset: 0x000AB344
		// (remove) Token: 0x06002AF8 RID: 11000 RVA: 0x000ABF60 File Offset: 0x000AB360
		internal event EventHandler MediaOpened
		{
			add
			{
				this._mediaOpenedHelper.AddEvent(value);
			}
			remove
			{
				this._mediaOpenedHelper.RemoveEvent(value);
			}
		}

		// Token: 0x1400019C RID: 412
		// (add) Token: 0x06002AF9 RID: 11001 RVA: 0x000ABF7C File Offset: 0x000AB37C
		// (remove) Token: 0x06002AFA RID: 11002 RVA: 0x000ABF98 File Offset: 0x000AB398
		internal event EventHandler MediaPrerolled
		{
			add
			{
				this._mediaPrerolledHelper.AddEvent(value);
			}
			remove
			{
				this._mediaPrerolledHelper.RemoveEvent(value);
			}
		}

		// Token: 0x1400019D RID: 413
		// (add) Token: 0x06002AFB RID: 11003 RVA: 0x000ABFB4 File Offset: 0x000AB3B4
		// (remove) Token: 0x06002AFC RID: 11004 RVA: 0x000ABFD0 File Offset: 0x000AB3D0
		internal event EventHandler MediaEnded
		{
			add
			{
				this._mediaEndedHelper.AddEvent(value);
			}
			remove
			{
				this._mediaEndedHelper.RemoveEvent(value);
			}
		}

		// Token: 0x1400019E RID: 414
		// (add) Token: 0x06002AFD RID: 11005 RVA: 0x000ABFEC File Offset: 0x000AB3EC
		// (remove) Token: 0x06002AFE RID: 11006 RVA: 0x000AC008 File Offset: 0x000AB408
		internal event EventHandler BufferingStarted
		{
			add
			{
				this._bufferingStartedHelper.AddEvent(value);
			}
			remove
			{
				this._bufferingStartedHelper.RemoveEvent(value);
			}
		}

		// Token: 0x1400019F RID: 415
		// (add) Token: 0x06002AFF RID: 11007 RVA: 0x000AC024 File Offset: 0x000AB424
		// (remove) Token: 0x06002B00 RID: 11008 RVA: 0x000AC040 File Offset: 0x000AB440
		internal event EventHandler BufferingEnded
		{
			add
			{
				this._bufferingEndedHelper.AddEvent(value);
			}
			remove
			{
				this._bufferingEndedHelper.RemoveEvent(value);
			}
		}

		// Token: 0x140001A0 RID: 416
		// (add) Token: 0x06002B01 RID: 11009 RVA: 0x000AC05C File Offset: 0x000AB45C
		// (remove) Token: 0x06002B02 RID: 11010 RVA: 0x000AC078 File Offset: 0x000AB478
		internal event EventHandler<MediaScriptCommandEventArgs> ScriptCommand
		{
			add
			{
				this._scriptCommandHelper.AddEvent(value);
			}
			remove
			{
				this._scriptCommandHelper.RemoveEvent(value);
			}
		}

		// Token: 0x140001A1 RID: 417
		// (add) Token: 0x06002B03 RID: 11011 RVA: 0x000AC094 File Offset: 0x000AB494
		// (remove) Token: 0x06002B04 RID: 11012 RVA: 0x000AC0B0 File Offset: 0x000AB4B0
		internal event EventHandler NewFrame
		{
			add
			{
				this._newFrameHelper.AddEvent(value);
			}
			remove
			{
				this._newFrameHelper.RemoveEvent(value);
			}
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x000AC0CC File Offset: 0x000AB4CC
		internal void RaiseMediaFailed(Exception e)
		{
			if (this.DispatcherMediaFailed != null)
			{
				this._dispatcher.BeginInvoke(DispatcherPriority.Normal, this.DispatcherMediaFailed, new ExceptionEventArgs(e));
			}
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x000AC0FC File Offset: 0x000AB4FC
		void IInvokable.RaiseEvent(byte[] buffer, int cb)
		{
			int num = 16;
			if (cb < num)
			{
				return;
			}
			MemoryStream input = new MemoryStream(buffer);
			using (BinaryReader binaryReader = new BinaryReader(input))
			{
				AVEvent avevent = (AVEvent)binaryReader.ReadUInt32();
				int hr = (int)binaryReader.ReadUInt32();
				switch (avevent)
				{
				case AVEvent.AVMediaOpened:
					if (this.DispatcherMediaOpened != null)
					{
						this._dispatcher.BeginInvoke(DispatcherPriority.Normal, this.DispatcherMediaOpened, null);
					}
					break;
				case AVEvent.AVMediaEnded:
					if (this.DispatcherMediaEnded != null)
					{
						this._dispatcher.BeginInvoke(DispatcherPriority.Normal, this.DispatcherMediaEnded, null);
					}
					break;
				case AVEvent.AVMediaFailed:
					this.RaiseMediaFailed(HRESULT.ConvertHRToException(hr));
					break;
				case AVEvent.AVMediaBufferingStarted:
					if (this.DispatcherBufferingStarted != null)
					{
						this._dispatcher.BeginInvoke(DispatcherPriority.Normal, this.DispatcherBufferingStarted, null);
					}
					break;
				case AVEvent.AVMediaBufferingEnded:
					if (this.DispatcherBufferingEnded != null)
					{
						this._dispatcher.BeginInvoke(DispatcherPriority.Normal, this.DispatcherBufferingEnded, null);
					}
					break;
				case AVEvent.AVMediaPrerolled:
					if (this.DispatcherMediaPrerolled != null)
					{
						this._dispatcher.BeginInvoke(DispatcherPriority.Normal, this.DispatcherMediaPrerolled, null);
					}
					break;
				case AVEvent.AVMediaScriptCommand:
					this.HandleScriptCommand(binaryReader);
					break;
				case AVEvent.AVMediaNewFrame:
					if (this.DispatcherMediaNewFrame != null)
					{
						this._dispatcher.BeginInvoke(DispatcherPriority.Background, this.DispatcherMediaNewFrame, null);
					}
					break;
				}
			}
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x000AC280 File Offset: 0x000AB680
		private void HandleScriptCommand(BinaryReader reader)
		{
			int stringLength = (int)reader.ReadUInt32();
			int stringLength2 = (int)reader.ReadUInt32();
			if (this.DispatcherScriptCommand != null)
			{
				string stringFromReader = this.GetStringFromReader(reader, stringLength);
				string stringFromReader2 = this.GetStringFromReader(reader, stringLength2);
				this._dispatcher.BeginInvoke(DispatcherPriority.Normal, this.DispatcherScriptCommand, new MediaScriptCommandEventArgs(stringFromReader, stringFromReader2));
			}
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000AC2D0 File Offset: 0x000AB6D0
		private string GetStringFromReader(BinaryReader reader, int stringLength)
		{
			StringBuilder stringBuilder = new StringBuilder(stringLength);
			stringBuilder.Length = stringLength;
			for (int i = 0; i < stringLength; i++)
			{
				stringBuilder[i] = (char)reader.ReadUInt16();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000AC30C File Offset: 0x000AB70C
		private object OnMediaOpened(object o)
		{
			this._mediaOpenedHelper.InvokeEvents(this._sender, null);
			return null;
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x000AC32C File Offset: 0x000AB72C
		private object OnMediaPrerolled(object o)
		{
			this._mediaPrerolledHelper.InvokeEvents(this._sender, null);
			return null;
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x000AC34C File Offset: 0x000AB74C
		private object OnMediaEnded(object o)
		{
			this._mediaEndedHelper.InvokeEvents(this._sender, null);
			return null;
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x000AC36C File Offset: 0x000AB76C
		private object OnBufferingStarted(object o)
		{
			this._bufferingStartedHelper.InvokeEvents(this._sender, null);
			return null;
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x000AC38C File Offset: 0x000AB78C
		private object OnBufferingEnded(object o)
		{
			this._bufferingEndedHelper.InvokeEvents(this._sender, null);
			return null;
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x000AC3AC File Offset: 0x000AB7AC
		private object OnMediaFailed(object o)
		{
			ExceptionEventArgs args = (ExceptionEventArgs)o;
			this._mediaFailedHelper.InvokeEvents(this._sender, args);
			return null;
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x000AC3D4 File Offset: 0x000AB7D4
		private object OnScriptCommand(object o)
		{
			MediaScriptCommandEventArgs args = (MediaScriptCommandEventArgs)o;
			this._scriptCommandHelper.InvokeEvents(this._sender, args);
			return null;
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x000AC3FC File Offset: 0x000AB7FC
		private object OnNewFrame(object e)
		{
			this._newFrameHelper.InvokeEvents(this._sender, null);
			return null;
		}

		// Token: 0x140001A2 RID: 418
		// (add) Token: 0x06002B11 RID: 11025 RVA: 0x000AC41C File Offset: 0x000AB81C
		// (remove) Token: 0x06002B12 RID: 11026 RVA: 0x000AC454 File Offset: 0x000AB854
		private event DispatcherOperationCallback DispatcherMediaFailed;

		// Token: 0x140001A3 RID: 419
		// (add) Token: 0x06002B13 RID: 11027 RVA: 0x000AC48C File Offset: 0x000AB88C
		// (remove) Token: 0x06002B14 RID: 11028 RVA: 0x000AC4C4 File Offset: 0x000AB8C4
		private event DispatcherOperationCallback DispatcherMediaOpened;

		// Token: 0x140001A4 RID: 420
		// (add) Token: 0x06002B15 RID: 11029 RVA: 0x000AC4FC File Offset: 0x000AB8FC
		// (remove) Token: 0x06002B16 RID: 11030 RVA: 0x000AC534 File Offset: 0x000AB934
		private event DispatcherOperationCallback DispatcherMediaPrerolled;

		// Token: 0x140001A5 RID: 421
		// (add) Token: 0x06002B17 RID: 11031 RVA: 0x000AC56C File Offset: 0x000AB96C
		// (remove) Token: 0x06002B18 RID: 11032 RVA: 0x000AC5A4 File Offset: 0x000AB9A4
		private event DispatcherOperationCallback DispatcherMediaEnded;

		// Token: 0x140001A6 RID: 422
		// (add) Token: 0x06002B19 RID: 11033 RVA: 0x000AC5DC File Offset: 0x000AB9DC
		// (remove) Token: 0x06002B1A RID: 11034 RVA: 0x000AC614 File Offset: 0x000ABA14
		private event DispatcherOperationCallback DispatcherBufferingStarted;

		// Token: 0x140001A7 RID: 423
		// (add) Token: 0x06002B1B RID: 11035 RVA: 0x000AC64C File Offset: 0x000ABA4C
		// (remove) Token: 0x06002B1C RID: 11036 RVA: 0x000AC684 File Offset: 0x000ABA84
		private event DispatcherOperationCallback DispatcherBufferingEnded;

		// Token: 0x140001A8 RID: 424
		// (add) Token: 0x06002B1D RID: 11037 RVA: 0x000AC6BC File Offset: 0x000ABABC
		// (remove) Token: 0x06002B1E RID: 11038 RVA: 0x000AC6F4 File Offset: 0x000ABAF4
		private event DispatcherOperationCallback DispatcherScriptCommand;

		// Token: 0x140001A9 RID: 425
		// (add) Token: 0x06002B1F RID: 11039 RVA: 0x000AC72C File Offset: 0x000ABB2C
		// (remove) Token: 0x06002B20 RID: 11040 RVA: 0x000AC764 File Offset: 0x000ABB64
		private event DispatcherOperationCallback DispatcherMediaNewFrame;

		// Token: 0x040013B1 RID: 5041
		private MediaPlayer _sender;

		// Token: 0x040013B2 RID: 5042
		private Dispatcher _dispatcher;

		// Token: 0x040013B3 RID: 5043
		private DispatcherOperationCallback _mediaOpened;

		// Token: 0x040013B4 RID: 5044
		private DispatcherOperationCallback _mediaFailed;

		// Token: 0x040013B5 RID: 5045
		private DispatcherOperationCallback _mediaPrerolled;

		// Token: 0x040013B6 RID: 5046
		private DispatcherOperationCallback _mediaEnded;

		// Token: 0x040013B7 RID: 5047
		private DispatcherOperationCallback _bufferingStarted;

		// Token: 0x040013B8 RID: 5048
		private DispatcherOperationCallback _bufferingEnded;

		// Token: 0x040013B9 RID: 5049
		private DispatcherOperationCallback _scriptCommand;

		// Token: 0x040013BA RID: 5050
		private DispatcherOperationCallback _newFrame;

		// Token: 0x040013BB RID: 5051
		private UniqueEventHelper<ExceptionEventArgs> _mediaFailedHelper = new UniqueEventHelper<ExceptionEventArgs>();

		// Token: 0x040013BC RID: 5052
		private UniqueEventHelper _mediaOpenedHelper = new UniqueEventHelper();

		// Token: 0x040013BD RID: 5053
		private UniqueEventHelper _mediaPrerolledHelper = new UniqueEventHelper();

		// Token: 0x040013BE RID: 5054
		private UniqueEventHelper _mediaEndedHelper = new UniqueEventHelper();

		// Token: 0x040013BF RID: 5055
		private UniqueEventHelper _bufferingStartedHelper = new UniqueEventHelper();

		// Token: 0x040013C0 RID: 5056
		private UniqueEventHelper _bufferingEndedHelper = new UniqueEventHelper();

		// Token: 0x040013C1 RID: 5057
		private UniqueEventHelper<MediaScriptCommandEventArgs> _scriptCommandHelper = new UniqueEventHelper<MediaScriptCommandEventArgs>();

		// Token: 0x040013C2 RID: 5058
		private UniqueEventHelper _newFrameHelper = new UniqueEventHelper();
	}
}
