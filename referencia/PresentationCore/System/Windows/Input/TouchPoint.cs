using System;

namespace System.Windows.Input
{
	/// <summary>Representa um ponto de toque único de uma fonte de mensagem multitoque.</summary>
	// Token: 0x020002A2 RID: 674
	public class TouchPoint : IEquatable<TouchPoint>
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.TouchPoint" />.</summary>
		/// <param name="device">O dispositivo de toque que gerou este <see cref="T:System.Windows.Input.TouchPoint" />.</param>
		/// <param name="position">O local do ponto de toque.</param>
		/// <param name="bounds">Os limites da área que tem o dedo em contato com a tela.</param>
		/// <param name="action">A última ação que ocorreram por esse dispositivo neste local.</param>
		/// <exception cref="T:System.ArgumentNullException">O <paramref name="device" /> é <see langword="null" />.</exception>
		// Token: 0x060013C1 RID: 5057 RVA: 0x00049E44 File Offset: 0x00049244
		public TouchPoint(TouchDevice device, Point position, Rect bounds, TouchAction action)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			this.TouchDevice = device;
			this.Position = position;
			this.Bounds = bounds;
			this.Action = action;
		}

		/// <summary>Obtém o dispositivo de toque que gerou este <see cref="T:System.Windows.Input.TouchPoint" />.</summary>
		/// <returns>O dispositivo de toque que gerou este <see cref="T:System.Windows.Input.TouchPoint" />.</returns>
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x00049E84 File Offset: 0x00049284
		// (set) Token: 0x060013C3 RID: 5059 RVA: 0x00049E98 File Offset: 0x00049298
		public TouchDevice TouchDevice { get; private set; }

		/// <summary>Obtém o local do ponto de toque.</summary>
		/// <returns>O local do ponto de toque.</returns>
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x00049EAC File Offset: 0x000492AC
		// (set) Token: 0x060013C5 RID: 5061 RVA: 0x00049EC0 File Offset: 0x000492C0
		public Point Position { get; private set; }

		/// <summary>Obtém os limites da área que tem o dedo em contato com a tela.</summary>
		/// <returns>Os limites da área que tem o dedo em contato com a tela.</returns>
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x00049ED4 File Offset: 0x000492D4
		// (set) Token: 0x060013C7 RID: 5063 RVA: 0x00049EE8 File Offset: 0x000492E8
		public Rect Bounds { get; private set; }

		/// <summary>Obtém o tamanho do <see cref="P:System.Windows.Input.TouchPoint.Bounds" /> propriedade.</summary>
		/// <returns>O tamanho do <see cref="P:System.Windows.Input.TouchPoint.Bounds" /> propriedade.</returns>
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x00049EFC File Offset: 0x000492FC
		public Size Size
		{
			get
			{
				return this.Bounds.Size;
			}
		}

		/// <summary>Obtém a última ação que ocorreram nesse local.</summary>
		/// <returns>A última ação que ocorreram nesse local.</returns>
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x00049F18 File Offset: 0x00049318
		// (set) Token: 0x060013CA RID: 5066 RVA: 0x00049F2C File Offset: 0x0004932C
		public TouchAction Action { get; private set; }

		// Token: 0x060013CB RID: 5067 RVA: 0x00049F40 File Offset: 0x00049340
		bool IEquatable<TouchPoint>.Equals(TouchPoint other)
		{
			return other != null && (other.TouchDevice == this.TouchDevice && other.Position == this.Position && other.Bounds == this.Bounds) && other.Action == this.Action;
		}
	}
}
