using System;
using System.Collections.Generic;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para o evento de <see cref="E:System.Windows.UIElement.ManipulationBoundaryFeedback" /> .</summary>
	// Token: 0x02000270 RID: 624
	public sealed class ManipulationBoundaryFeedbackEventArgs : InputEventArgs
	{
		// Token: 0x060011B1 RID: 4529 RVA: 0x0004290C File Offset: 0x00041D0C
		internal ManipulationBoundaryFeedbackEventArgs(ManipulationDevice manipulationDevice, int timestamp, IInputElement manipulationContainer, ManipulationDelta boundaryFeedback) : base(manipulationDevice, timestamp)
		{
			base.RoutedEvent = Manipulation.ManipulationBoundaryFeedbackEvent;
			this.ManipulationContainer = manipulationContainer;
			this.BoundaryFeedback = boundaryFeedback;
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0004293C File Offset: 0x00041D3C
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
			if (base.RoutedEvent == Manipulation.ManipulationBoundaryFeedbackEvent)
			{
				((EventHandler<ManipulationBoundaryFeedbackEventArgs>)genericHandler)(genericTarget, this);
				return;
			}
			base.InvokeEventHandler(genericHandler, genericTarget);
		}

		/// <summary>Obtém o contêiner a que a propriedade <see cref="P:System.Windows.Input.ManipulationBoundaryFeedbackEventArgs.BoundaryFeedback" /> é relativa.</summary>
		/// <returns>O contêiner a que a propriedade <see cref="P:System.Windows.Input.ManipulationBoundaryFeedbackEventArgs.BoundaryFeedback" /> é relativa.</returns>
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x00042988 File Offset: 0x00041D88
		// (set) Token: 0x060011B4 RID: 4532 RVA: 0x0004299C File Offset: 0x00041D9C
		public IInputElement ManipulationContainer { get; private set; }

		/// <summary>Obtém a parte não usada da manipulação direta.</summary>
		/// <returns>A parte não usada da manipulação direta.</returns>
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x000429B0 File Offset: 0x00041DB0
		// (set) Token: 0x060011B6 RID: 4534 RVA: 0x000429C4 File Offset: 0x00041DC4
		public ManipulationDelta BoundaryFeedback { get; private set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x000429D8 File Offset: 0x00041DD8
		// (set) Token: 0x060011B8 RID: 4536 RVA: 0x000429EC File Offset: 0x00041DEC
		internal Func<Point, Point> CompensateForBoundaryFeedback { get; set; }

		/// <summary>Obtém uma coleção de objetos que representa os contatos de toque da manipulação.</summary>
		/// <returns>Uma coleção de objetos que representa os contatos de manipulação de toque.</returns>
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x00042A00 File Offset: 0x00041E00
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

		// Token: 0x040009AC RID: 2476
		private IEnumerable<IManipulator> _manipulators;
	}
}
