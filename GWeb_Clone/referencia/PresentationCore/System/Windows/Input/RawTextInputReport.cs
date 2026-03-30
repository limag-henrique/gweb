using System;

namespace System.Windows.Input
{
	// Token: 0x02000294 RID: 660
	internal class RawTextInputReport : InputReport
	{
		// Token: 0x0600134C RID: 4940 RVA: 0x000482D0 File Offset: 0x000476D0
		public RawTextInputReport(PresentationSource inputSource, InputMode mode, int timestamp, bool isDeadCharacter, bool isSystemCharacter, bool isControlCharacter, char characterCode) : base(inputSource, InputType.Text, mode, timestamp)
		{
			this._isDeadCharacter = isDeadCharacter;
			this._isSystemCharacter = isSystemCharacter;
			this._isControlCharacter = isControlCharacter;
			this._characterCode = characterCode;
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x00048308 File Offset: 0x00047708
		public bool IsDeadCharacter
		{
			get
			{
				return this._isDeadCharacter;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x0004831C File Offset: 0x0004771C
		public bool IsSystemCharacter
		{
			get
			{
				return this._isSystemCharacter;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x00048330 File Offset: 0x00047730
		public bool IsControlCharacter
		{
			get
			{
				return this._isControlCharacter;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x00048344 File Offset: 0x00047744
		public char CharacterCode
		{
			get
			{
				return this._characterCode;
			}
		}

		// Token: 0x04000A79 RID: 2681
		private readonly bool _isDeadCharacter;

		// Token: 0x04000A7A RID: 2682
		private readonly bool _isSystemCharacter;

		// Token: 0x04000A7B RID: 2683
		private readonly bool _isControlCharacter;

		// Token: 0x04000A7C RID: 2684
		private readonly char _characterCode;
	}
}
