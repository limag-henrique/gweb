using System;
using System.Collections.Generic;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para o evento de <see cref="E:System.Windows.UIElement.ManipulationCompleted" /> .</summary>
	// Token: 0x02000271 RID: 625
	public sealed class ManipulationCompletedEventArgs : InputEventArgs
	{
		// Token: 0x060011BA RID: 4538 RVA: 0x00042A34 File Offset: 0x00041E34
		internal ManipulationCompletedEventArgs(ManipulationDevice manipulationDevice, int timestamp, IInputElement manipulationContainer, Point origin, ManipulationDelta total, ManipulationVelocities velocities, bool isInertial) : base(manipulationDevice, timestamp)
		{
			if (total == null)
			{
				throw new ArgumentNullException("total");
			}
			if (velocities == null)
			{
				throw new ArgumentNullException("velocities");
			}
			base.RoutedEvent = Manipulation.ManipulationCompletedEvent;
			this.ManipulationContainer = manipulationContainer;
			this.ManipulationOrigin = origin;
			this.TotalManipulation = total;
			this.FinalVelocities = velocities;
			this.IsInertial = isInertial;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00042A9C File Offset: 0x00041E9C
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
			if (base.RoutedEvent == Manipulation.ManipulationCompletedEvent)
			{
				((EventHandler<ManipulationCompletedEventArgs>)genericHandler)(genericTarget, this);
				return;
			}
			base.InvokeEventHandler(genericHandler, genericTarget);
		}

		/// <summary>Obtém um valor que indica se o evento <see cref="E:System.Windows.UIElement.ManipulationCompleted" /> ocorre durante a inercia.</summary>
		/// <returns>
		///   <see langword="true" /> se o evento <see cref="E:System.Windows.UIElement.ManipulationCompleted" /> ocorrer durante a inércia; <see langword="false" /> se o evento ocorrer quando o usuário tiver contato com o <see cref="T:System.Windows.UIElement" />.</returns>
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x00042AE8 File Offset: 0x00041EE8
		// (set) Token: 0x060011BD RID: 4541 RVA: 0x00042AFC File Offset: 0x00041EFC
		public bool IsInertial { get; private set; }

		/// <summary>Obtém o contêiner que define as coordenadas para a manipulação.</summary>
		/// <returns>O contêiner que define as coordenadas para a manipulação.</returns>
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x00042B10 File Offset: 0x00041F10
		// (set) Token: 0x060011BF RID: 4543 RVA: 0x00042B24 File Offset: 0x00041F24
		public IInputElement ManipulationContainer { get; private set; }

		/// <summary>Obtém o ponto que deu origem à manipulação.</summary>
		/// <returns>O ponto que deu origem à manipulação.</returns>
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x00042B38 File Offset: 0x00041F38
		// (set) Token: 0x060011C1 RID: 4545 RVA: 0x00042B4C File Offset: 0x00041F4C
		public Point ManipulationOrigin { get; private set; }

		/// <summary>Obtém a transformação total que ocorre durante a manipulação atual.</summary>
		/// <returns>A transformação total que ocorre durante a manipulação atual.</returns>
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x00042B60 File Offset: 0x00041F60
		// (set) Token: 0x060011C3 RID: 4547 RVA: 0x00042B74 File Offset: 0x00041F74
		public ManipulationDelta TotalManipulation { get; private set; }

		/// <summary>Obtém as velocidades que são usadas para a manipulação.</summary>
		/// <returns>As velocidades que são usadas para a manipulação.</returns>
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x00042B88 File Offset: 0x00041F88
		// (set) Token: 0x060011C5 RID: 4549 RVA: 0x00042B9C File Offset: 0x00041F9C
		public ManipulationVelocities FinalVelocities { get; private set; }

		/// <summary>Cancela a manipulação.</summary>
		/// <returns>
		///   <see langword="true" /> se a manipulação tiver sido cancelada com êxito; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060011C6 RID: 4550 RVA: 0x00042BB0 File Offset: 0x00041FB0
		public bool Cancel()
		{
			if (!this.IsInertial)
			{
				this.RequestedCancel = true;
				return true;
			}
			return false;
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00042BD0 File Offset: 0x00041FD0
		// (set) Token: 0x060011C8 RID: 4552 RVA: 0x00042BE4 File Offset: 0x00041FE4
		internal bool RequestedCancel { get; private set; }

		/// <summary>Obtém uma coleção de objetos que representa os contatos de toque da manipulação.</summary>
		/// <returns>Uma coleção de objetos que representa os contatos de manipulação de toque.</returns>
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x00042BF8 File Offset: 0x00041FF8
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

		// Token: 0x040009B3 RID: 2483
		private IEnumerable<IManipulator> _manipulators;
	}
}
