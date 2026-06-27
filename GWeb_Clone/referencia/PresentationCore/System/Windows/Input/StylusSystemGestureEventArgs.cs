using System;
using System.ComponentModel;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para o evento de <see cref="E:System.Windows.UIElement.StylusSystemGesture" /> .</summary>
	// Token: 0x020002C4 RID: 708
	public class StylusSystemGestureEventArgs : StylusEventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.StylusSystemGestureEventArgs" />.</summary>
		/// <param name="stylusDevice">O <see cref="T:System.Windows.Input.StylusDevice" /> a ser associado ao evento.</param>
		/// <param name="timestamp">A hora em que o evento ocorre.</param>
		/// <param name="systemGesture">O <see cref="T:System.Windows.Input.SystemGesture" /> que gera o evento.</param>
		// Token: 0x0600150E RID: 5390 RVA: 0x0004E8E8 File Offset: 0x0004DCE8
		public StylusSystemGestureEventArgs(StylusDevice stylusDevice, int timestamp, SystemGesture systemGesture) : base(stylusDevice, timestamp)
		{
			if (!RawStylusSystemGestureInputReport.IsValidSystemGesture(systemGesture, false, false))
			{
				throw new InvalidEnumArgumentException(SR.Get("Enum_Invalid", new object[]
				{
					"systemGesture"
				}));
			}
			this._id = systemGesture;
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0004E92C File Offset: 0x0004DD2C
		internal StylusSystemGestureEventArgs(StylusDevice stylusDevice, int timestamp, SystemGesture systemGesture, int gestureX, int gestureY, int buttonState) : base(stylusDevice, timestamp)
		{
			if (!RawStylusSystemGestureInputReport.IsValidSystemGesture(systemGesture, true, false))
			{
				throw new InvalidEnumArgumentException(SR.Get("Enum_Invalid", new object[]
				{
					"systemGesture"
				}));
			}
			this._id = systemGesture;
			this._buttonState = buttonState;
			this._gestureX = gestureX;
			this._gestureY = gestureY;
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.SystemGesture" /> que gera o evento.</summary>
		/// <returns>O <see cref="T:System.Windows.Input.SystemGesture" /> que gera o evento.</returns>
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x0004E988 File Offset: 0x0004DD88
		public SystemGesture SystemGesture
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x0004E99C File Offset: 0x0004DD9C
		internal int ButtonState
		{
			get
			{
				return this._buttonState;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x0004E9B0 File Offset: 0x0004DDB0
		internal int GestureX
		{
			get
			{
				return this._gestureX;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x0004E9C4 File Offset: 0x0004DDC4
		internal int GestureY
		{
			get
			{
				return this._gestureY;
			}
		}

		/// <summary>Invoca um manipulador de tipo específico no destino sempre que o evento <see cref="E:System.Windows.UIElement.StylusSystemGesture" /> é gerado.</summary>
		/// <param name="genericHandler">O manipulador genérico a ser chamado de uma forma específica ao tipo.</param>
		/// <param name="genericTarget">O destino no qual chamar o manipulador.</param>
		// Token: 0x06001514 RID: 5396 RVA: 0x0004E9D8 File Offset: 0x0004DDD8
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			StylusSystemGestureEventHandler stylusSystemGestureEventHandler = (StylusSystemGestureEventHandler)genericHandler;
			stylusSystemGestureEventHandler(genericTarget, this);
		}

		// Token: 0x04000B7A RID: 2938
		private SystemGesture _id;

		// Token: 0x04000B7B RID: 2939
		private int _buttonState;

		// Token: 0x04000B7C RID: 2940
		private int _gestureX;

		// Token: 0x04000B7D RID: 2941
		private int _gestureY;
	}
}
