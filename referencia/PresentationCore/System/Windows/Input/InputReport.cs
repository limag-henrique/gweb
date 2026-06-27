using System;
using System.ComponentModel;
using System.Security;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	// Token: 0x0200025C RID: 604
	[FriendAccessAllowed]
	internal abstract class InputReport
	{
		// Token: 0x06001105 RID: 4357 RVA: 0x00040408 File Offset: 0x0003F808
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected InputReport(PresentationSource inputSource, InputType type, InputMode mode, int timestamp)
		{
			if (inputSource == null)
			{
				throw new ArgumentNullException("inputSource");
			}
			this.Validate_InputType(type);
			this.Validate_InputMode(mode);
			this._inputSource = new SecurityCriticalData<PresentationSource>(inputSource);
			this._type = type;
			this._mode = mode;
			this._timestamp = timestamp;
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x0004045C File Offset: 0x0003F85C
		public PresentationSource InputSource
		{
			[SecurityCritical]
			get
			{
				return this._inputSource.Value;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x00040474 File Offset: 0x0003F874
		public InputType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x00040488 File Offset: 0x0003F888
		public InputMode Mode
		{
			get
			{
				return this._mode;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x0004049C File Offset: 0x0003F89C
		public int Timestamp
		{
			get
			{
				return this._timestamp;
			}
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x000404B0 File Offset: 0x0003F8B0
		private void Validate_InputMode(InputMode mode)
		{
			if (mode > InputMode.Sink)
			{
				throw new InvalidEnumArgumentException("mode", (int)mode, typeof(InputMode));
			}
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x000404D8 File Offset: 0x0003F8D8
		private void Validate_InputType(InputType type)
		{
			if (type > InputType.Command)
			{
				throw new InvalidEnumArgumentException("type", (int)type, typeof(InputType));
			}
		}

		// Token: 0x04000935 RID: 2357
		private SecurityCriticalData<PresentationSource> _inputSource;

		// Token: 0x04000936 RID: 2358
		private InputType _type;

		// Token: 0x04000937 RID: 2359
		private InputMode _mode;

		// Token: 0x04000938 RID: 2360
		private int _timestamp;
	}
}
