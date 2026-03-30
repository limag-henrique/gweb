using System;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para o evento de <see cref="E:System.Windows.Input.Touch.FrameReported" /> .</summary>
	// Token: 0x020002A0 RID: 672
	public sealed class TouchFrameEventArgs : EventArgs
	{
		// Token: 0x060013B7 RID: 5047 RVA: 0x00049DC8 File Offset: 0x000491C8
		internal TouchFrameEventArgs(int timestamp)
		{
			this.Timestamp = timestamp;
		}

		/// <summary>Obtém o carimbo de hora para esse evento.</summary>
		/// <returns>O carimbo de hora para esse evento.</returns>
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x00049DE4 File Offset: 0x000491E4
		// (set) Token: 0x060013B9 RID: 5049 RVA: 0x00049DF8 File Offset: 0x000491F8
		public int Timestamp { get; private set; }

		/// <summary>Retorna uma coleção que contém o ponto de toque atual para cada dispositivo de toque ativo em relação ao elemento especificado.</summary>
		/// <param name="relativeTo">O elemento que define o espaço de coordenadas. Para usar coordenadas absolutas de WPF, especifique <paramref name="relativeTo" /> como <see langword="null" />.</param>
		/// <returns>Uma coleção que contém o <see cref="T:System.Windows.Input.TouchPoint" /> atual para cada <see cref="T:System.Windows.Input.TouchDevice" /> ativo.</returns>
		// Token: 0x060013BA RID: 5050 RVA: 0x00049E0C File Offset: 0x0004920C
		public TouchPointCollection GetTouchPoints(IInputElement relativeTo)
		{
			return TouchDevice.GetTouchPoints(relativeTo);
		}

		/// <summary>Retorna o ponto de toque atual do dispositivo primário toque em relação ao elemento especificado.</summary>
		/// <param name="relativeTo">O elemento que define o espaço de coordenadas. Para usar coordenadas absolutas de WPF, especifique <paramref name="relativeTo" /> como <see langword="null" />.</param>
		/// <returns>A posição atual do primário <see cref="T:System.Windows.Input.TouchDevice" /> em relação ao elemento especificado; ou <see langword="null" /> se o primário <see cref="T:System.Windows.Input.TouchDevice" /> não estiver ativo.</returns>
		// Token: 0x060013BB RID: 5051 RVA: 0x00049E20 File Offset: 0x00049220
		public TouchPoint GetPrimaryTouchPoint(IInputElement relativeTo)
		{
			return TouchDevice.GetPrimaryTouchPoint(relativeTo);
		}

		/// <summary>Este membro não está implementado.</summary>
		// Token: 0x060013BC RID: 5052 RVA: 0x00049E34 File Offset: 0x00049234
		public void SuspendMousePromotionUntilTouchUp()
		{
		}
	}
}
