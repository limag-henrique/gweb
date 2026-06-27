using System;

namespace System.Windows.Input
{
	// Token: 0x0200028F RID: 655
	internal class RawAppCommandInputReport : InputReport
	{
		// Token: 0x0600133A RID: 4922 RVA: 0x00048030 File Offset: 0x00047430
		internal RawAppCommandInputReport(PresentationSource inputSource, InputMode mode, int timestamp, int appCommand, InputType device, InputType inputType) : base(inputSource, inputType, mode, timestamp)
		{
			this._appCommand = appCommand;
			this._device = device;
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x00048058 File Offset: 0x00047458
		internal int AppCommand
		{
			get
			{
				return this._appCommand;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x0004806C File Offset: 0x0004746C
		internal InputType Device
		{
			get
			{
				return this._device;
			}
		}

		// Token: 0x04000A4E RID: 2638
		private int _appCommand;

		// Token: 0x04000A4F RID: 2639
		private InputType _device;
	}
}
