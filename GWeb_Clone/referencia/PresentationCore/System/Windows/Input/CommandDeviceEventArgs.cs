using System;

namespace System.Windows.Input
{
	// Token: 0x02000209 RID: 521
	internal class CommandDeviceEventArgs : InputEventArgs
	{
		// Token: 0x06000D9A RID: 3482 RVA: 0x00033EDC File Offset: 0x000332DC
		internal CommandDeviceEventArgs(CommandDevice commandDevice, int timestamp, ICommand command) : base(commandDevice, timestamp)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			this._command = command;
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x00033F08 File Offset: 0x00033308
		internal ICommand Command
		{
			get
			{
				return this._command;
			}
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00033F1C File Offset: 0x0003331C
		protected override void InvokeEventHandler(Delegate execHandler, object target)
		{
			CommandDeviceEventHandler commandDeviceEventHandler = (CommandDeviceEventHandler)execHandler;
			commandDeviceEventHandler(target, this);
		}

		// Token: 0x0400081A RID: 2074
		private ICommand _command;
	}
}
