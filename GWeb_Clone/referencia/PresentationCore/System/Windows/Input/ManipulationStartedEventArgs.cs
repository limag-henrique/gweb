using System;
using System.Collections.Generic;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para o evento de <see cref="E:System.Windows.UIElement.ManipulationStarted" /> .</summary>
	// Token: 0x02000279 RID: 633
	public sealed class ManipulationStartedEventArgs : InputEventArgs
	{
		// Token: 0x0600125B RID: 4699 RVA: 0x00044AF0 File Offset: 0x00043EF0
		internal ManipulationStartedEventArgs(ManipulationDevice manipulationDevice, int timestamp, IInputElement manipulationContainer, Point origin) : base(manipulationDevice, timestamp)
		{
			base.RoutedEvent = Manipulation.ManipulationStartedEvent;
			this.ManipulationContainer = manipulationContainer;
			this.ManipulationOrigin = origin;
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00044B20 File Offset: 0x00043F20
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			if (genericHandler == null)
			{
				throw new ArgumentNullException("genericHandler");
			}
			if (genericTarget == null)
			{
				throw new ArgumentNullException("genericTarget");
			}
			if (base.RoutedEvent == Manipulation.ManipulationStartedEvent)
			{
				((EventHandler<ManipulationStartedEventArgs>)genericHandler)(genericTarget, this);
				return;
			}
			base.InvokeEventHandler(genericHandler, genericTarget);
		}

		/// <summary>Obtém o contêiner a que a propriedade <see cref="P:System.Windows.Input.ManipulationStartedEventArgs.ManipulationOrigin" /> é relativa.</summary>
		/// <returns>O contêiner a que a propriedade <see cref="P:System.Windows.Input.ManipulationStartedEventArgs.ManipulationOrigin" /> é relativa.</returns>
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x00044B6C File Offset: 0x00043F6C
		// (set) Token: 0x0600125E RID: 4702 RVA: 0x00044B80 File Offset: 0x00043F80
		public IInputElement ManipulationContainer { get; private set; }

		/// <summary>Obtém o ponto que deu origem à manipulação.</summary>
		/// <returns>O ponto que deu origem à manipulação.</returns>
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x00044B94 File Offset: 0x00043F94
		// (set) Token: 0x06001260 RID: 4704 RVA: 0x00044BA8 File Offset: 0x00043FA8
		public Point ManipulationOrigin { get; private set; }

		/// <summary>Conclui a manipulação sem inércia.</summary>
		// Token: 0x06001261 RID: 4705 RVA: 0x00044BBC File Offset: 0x00043FBC
		public void Complete()
		{
			this.RequestedComplete = true;
			this.RequestedCancel = false;
		}

		/// <summary>Cancela a manipulação.</summary>
		/// <returns>
		///   <see langword="true" /> se a manipulação foi bem-sucedida; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001262 RID: 4706 RVA: 0x00044BD8 File Offset: 0x00043FD8
		public bool Cancel()
		{
			this.RequestedCancel = true;
			this.RequestedComplete = false;
			return true;
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x00044BF4 File Offset: 0x00043FF4
		// (set) Token: 0x06001264 RID: 4708 RVA: 0x00044C08 File Offset: 0x00044008
		internal bool RequestedComplete { get; private set; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x00044C1C File Offset: 0x0004401C
		// (set) Token: 0x06001266 RID: 4710 RVA: 0x00044C30 File Offset: 0x00044030
		internal bool RequestedCancel { get; private set; }

		/// <summary>Obtém uma coleção de objetos que representa os contatos de toque da manipulação.</summary>
		/// <returns>Uma coleção de objetos que representa os contatos de manipulação de toque.</returns>
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x00044C44 File Offset: 0x00044044
		public IEnumerable<IManipulator> Manipulators
		{
			get
			{
				if (this._manipulators == null)
				{
					this._manipulators = ((ManipulationDevice)base.Device).GetManipulatorsReadOnly();
				}
				return this._manipulators;
			}
		}

		// Token: 0x040009FD RID: 2557
		private IEnumerable<IManipulator> _manipulators;
	}
}
