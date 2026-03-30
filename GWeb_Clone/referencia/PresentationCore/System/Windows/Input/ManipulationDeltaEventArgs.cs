using System;
using System.Collections.Generic;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para o evento de <see cref="E:System.Windows.UIElement.ManipulationDelta" /> .</summary>
	// Token: 0x02000273 RID: 627
	public sealed class ManipulationDeltaEventArgs : InputEventArgs
	{
		// Token: 0x060011D3 RID: 4563 RVA: 0x00042CFC File Offset: 0x000420FC
		internal ManipulationDeltaEventArgs(ManipulationDevice manipulationDevice, int timestamp, IInputElement manipulationContainer, Point origin, ManipulationDelta delta, ManipulationDelta cumulative, ManipulationVelocities velocities, bool isInertial) : base(manipulationDevice, timestamp)
		{
			if (delta == null)
			{
				throw new ArgumentNullException("delta");
			}
			if (cumulative == null)
			{
				throw new ArgumentNullException("cumulative");
			}
			if (velocities == null)
			{
				throw new ArgumentNullException("velocities");
			}
			base.RoutedEvent = Manipulation.ManipulationDeltaEvent;
			this.ManipulationContainer = manipulationContainer;
			this.ManipulationOrigin = origin;
			this.DeltaManipulation = delta;
			this.CumulativeManipulation = cumulative;
			this.Velocities = velocities;
			this.IsInertial = isInertial;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00042D78 File Offset: 0x00042178
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
			if (base.RoutedEvent == Manipulation.ManipulationDeltaEvent)
			{
				((EventHandler<ManipulationDeltaEventArgs>)genericHandler)(genericTarget, this);
				return;
			}
			base.InvokeEventHandler(genericHandler, genericTarget);
		}

		/// <summary>Obtém um valor que indica se o evento <see cref="E:System.Windows.UIElement.ManipulationDelta" /> ocorre durante a inercia.</summary>
		/// <returns>
		///   <see langword="true" /> se o evento <see cref="E:System.Windows.UIElement.ManipulationDelta" /> ocorrer durante a inércia; <see langword="false" /> se o evento ocorrer quando o usuário tiver contato com o <see cref="T:System.Windows.UIElement" />.</returns>
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x00042DC4 File Offset: 0x000421C4
		// (set) Token: 0x060011D6 RID: 4566 RVA: 0x00042DD8 File Offset: 0x000421D8
		public bool IsInertial { get; private set; }

		/// <summary>Obtém o contêiner que define as coordenadas para a manipulação.</summary>
		/// <returns>O contêiner que define as coordenadas para a manipulação.</returns>
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x00042DEC File Offset: 0x000421EC
		// (set) Token: 0x060011D8 RID: 4568 RVA: 0x00042E00 File Offset: 0x00042200
		public IInputElement ManipulationContainer { get; private set; }

		/// <summary>Obtém o ponto que deu origem à manipulação.</summary>
		/// <returns>O ponto que deu origem à manipulação.</returns>
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x00042E14 File Offset: 0x00042214
		// (set) Token: 0x060011DA RID: 4570 RVA: 0x00042E28 File Offset: 0x00042228
		public Point ManipulationOrigin { get; private set; }

		/// <summary>Obtém as alterações totais da manipulação atual.</summary>
		/// <returns>As alterações totais da manipulação atual.</returns>
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x00042E3C File Offset: 0x0004223C
		// (set) Token: 0x060011DC RID: 4572 RVA: 0x00042E50 File Offset: 0x00042250
		public ManipulationDelta CumulativeManipulation { get; private set; }

		/// <summary>Obtém as alterações mais recentes da manipulação de atual.</summary>
		/// <returns>As alterações mais recentes da manipulação de atual.</returns>
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x00042E64 File Offset: 0x00042264
		// (set) Token: 0x060011DE RID: 4574 RVA: 0x00042E78 File Offset: 0x00042278
		public ManipulationDelta DeltaManipulation { get; private set; }

		/// <summary>Obtém as taxas das alterações mais recentes à manipulação.</summary>
		/// <returns>As taxas das alterações mais recentes à manipulação.</returns>
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x00042E8C File Offset: 0x0004228C
		// (set) Token: 0x060011E0 RID: 4576 RVA: 0x00042EA0 File Offset: 0x000422A0
		public ManipulationVelocities Velocities { get; private set; }

		/// <summary>Especifica que a manipulação foi além de determinados limites.</summary>
		/// <param name="unusedManipulation">A parte da manipulação que representa ir além do limite.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="unusedManipulation" /> é <see langword="null" />.</exception>
		// Token: 0x060011E1 RID: 4577 RVA: 0x00042EB4 File Offset: 0x000422B4
		public void ReportBoundaryFeedback(ManipulationDelta unusedManipulation)
		{
			if (unusedManipulation == null)
			{
				throw new ArgumentNullException("unusedManipulation");
			}
			this.UnusedManipulation = unusedManipulation;
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x00042ED8 File Offset: 0x000422D8
		// (set) Token: 0x060011E3 RID: 4579 RVA: 0x00042EEC File Offset: 0x000422EC
		internal ManipulationDelta UnusedManipulation { get; private set; }

		/// <summary>Conclui a manipulação sem inércia.</summary>
		// Token: 0x060011E4 RID: 4580 RVA: 0x00042F00 File Offset: 0x00042300
		public void Complete()
		{
			this.RequestedComplete = true;
			this.RequestedInertia = false;
			this.RequestedCancel = false;
		}

		/// <summary>Inicia a inércia na manipulação ignorando movimentos de contato subsequentes e gerando o evento <see cref="E:System.Windows.UIElement.ManipulationInertiaStarting" />.</summary>
		// Token: 0x060011E5 RID: 4581 RVA: 0x00042F24 File Offset: 0x00042324
		public void StartInertia()
		{
			this.RequestedComplete = true;
			this.RequestedInertia = true;
			this.RequestedCancel = false;
		}

		/// <summary>Cancela a manipulação.</summary>
		/// <returns>
		///   <see langword="true" /> se a manipulação tiver sido cancelada com êxito; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060011E6 RID: 4582 RVA: 0x00042F48 File Offset: 0x00042348
		public bool Cancel()
		{
			if (!this.IsInertial)
			{
				this.RequestedCancel = true;
				this.RequestedComplete = false;
				this.RequestedInertia = false;
				return true;
			}
			return false;
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x00042F78 File Offset: 0x00042378
		// (set) Token: 0x060011E8 RID: 4584 RVA: 0x00042F8C File Offset: 0x0004238C
		internal bool RequestedComplete { get; private set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x00042FA0 File Offset: 0x000423A0
		// (set) Token: 0x060011EA RID: 4586 RVA: 0x00042FB4 File Offset: 0x000423B4
		internal bool RequestedInertia { get; private set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x00042FC8 File Offset: 0x000423C8
		// (set) Token: 0x060011EC RID: 4588 RVA: 0x00042FDC File Offset: 0x000423DC
		internal bool RequestedCancel { get; private set; }

		/// <summary>Obtém uma coleção de objetos que representa os contatos de toque da manipulação.</summary>
		/// <returns>Uma coleção de objetos que representa os contatos de manipulação de toque.</returns>
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00042FF0 File Offset: 0x000423F0
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

		// Token: 0x040009C2 RID: 2498
		private IEnumerable<IManipulator> _manipulators;
	}
}
