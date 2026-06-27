using System;

namespace System.Windows.Input
{
	/// <summary>Contém argumentos associados ao evento <see cref="E:System.Windows.Input.InputMethod.StateChanged" />.</summary>
	// Token: 0x02000254 RID: 596
	public class InputMethodStateChangedEventArgs : EventArgs
	{
		// Token: 0x060010E9 RID: 4329 RVA: 0x0003FEC0 File Offset: 0x0003F2C0
		internal InputMethodStateChangedEventArgs(InputMethodStateType statetype)
		{
			this._statetype = statetype;
		}

		/// <summary>Obtém um valor que indica se a propriedade <see cref="P:System.Windows.Input.InputMethod.ImeState" /> foi ou não alterada.</summary>
		/// <returns>
		///   <see langword="true" /> Se o <see cref="P:System.Windows.Input.InputMethod.ImeState" /> propriedade alterada; caso contrário, <see langword="false" />.  
		/// Esta propriedade não tem valor padrão.</returns>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x0003FEDC File Offset: 0x0003F2DC
		public bool IsImeStateChanged
		{
			get
			{
				return this._statetype == InputMethodStateType.ImeState;
			}
		}

		/// <summary>Obtém um valor que indica se a propriedade <see cref="P:System.Windows.Input.InputMethod.MicrophoneState" /> foi ou não alterada.</summary>
		/// <returns>
		///   <see langword="true" /> Se o <see cref="P:System.Windows.Input.InputMethod.MicrophoneState" /> propriedade alterada; caso contrário, <see langword="false" />.  
		/// Esta propriedade não tem valor padrão.</returns>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x0003FEF4 File Offset: 0x0003F2F4
		public bool IsMicrophoneStateChanged
		{
			get
			{
				return this._statetype == InputMethodStateType.MicrophoneState;
			}
		}

		/// <summary>Obtém um valor que indica se a propriedade <see cref="P:System.Windows.Input.InputMethod.HandwritingState" /> foi ou não alterada.</summary>
		/// <returns>
		///   <see langword="true" /> Se o <see cref="P:System.Windows.Input.InputMethod.HandwritingState" /> propriedade alterada; caso contrário, <see langword="false" />.  
		/// Esta propriedade não tem valor padrão.</returns>
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x0003FF0C File Offset: 0x0003F30C
		public bool IsHandwritingStateChanged
		{
			get
			{
				return this._statetype == InputMethodStateType.HandwritingState;
			}
		}

		/// <summary>Obtém um valor que indica se a propriedade <see cref="P:System.Windows.Input.InputMethod.SpeechMode" /> foi ou não alterada.</summary>
		/// <returns>
		///   <see langword="true" /> Se o <see cref="P:System.Windows.Input.InputMethod.SpeechMode" /> propriedade alterada; caso contrário, <see langword="false" />.  
		/// Esta propriedade não tem valor padrão.</returns>
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x0003FF24 File Offset: 0x0003F324
		public bool IsSpeechModeChanged
		{
			get
			{
				return this._statetype == InputMethodStateType.SpeechMode;
			}
		}

		/// <summary>Obtém um valor que indica se a propriedade <see cref="P:System.Windows.Input.InputMethod.ImeConversionMode" /> foi ou não alterada.</summary>
		/// <returns>
		///   <see langword="true" /> Se o <see cref="P:System.Windows.Input.InputMethod.ImeConversionMode" /> propriedade alterada; caso contrário, <see langword="false" />.  
		/// Esta propriedade não tem valor padrão.</returns>
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x0003FF3C File Offset: 0x0003F33C
		public bool IsImeConversionModeChanged
		{
			get
			{
				return this._statetype == InputMethodStateType.ImeConversionModeValues;
			}
		}

		/// <summary>Obtém um valor que indica se a propriedade <see cref="P:System.Windows.Input.InputMethod.ImeSentenceMode" /> foi ou não alterada.</summary>
		/// <returns>
		///   <see langword="true" /> Se o <see cref="P:System.Windows.Input.InputMethod.ImeSentenceMode" /> propriedade alterada; caso contrário, <see langword="false" />.  
		/// Esta propriedade não tem valor padrão.</returns>
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x0003FF54 File Offset: 0x0003F354
		public bool IsImeSentenceModeChanged
		{
			get
			{
				return this._statetype == InputMethodStateType.ImeSentenceModeValues;
			}
		}

		// Token: 0x04000916 RID: 2326
		private InputMethodStateType _statetype;
	}
}
