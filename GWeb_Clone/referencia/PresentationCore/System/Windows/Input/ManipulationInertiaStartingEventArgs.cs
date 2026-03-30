using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input.Manipulations;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para o evento de <see cref="E:System.Windows.UIElement.ManipulationInertiaStarting" /> .</summary>
	// Token: 0x02000275 RID: 629
	public sealed class ManipulationInertiaStartingEventArgs : InputEventArgs
	{
		// Token: 0x06001210 RID: 4624 RVA: 0x000438E0 File Offset: 0x00042CE0
		internal ManipulationInertiaStartingEventArgs(ManipulationDevice manipulationDevice, int timestamp, IInputElement manipulationContainer, Point origin, ManipulationVelocities initialVelocities, bool isInInertia) : base(manipulationDevice, timestamp)
		{
			if (initialVelocities == null)
			{
				throw new ArgumentNullException("initialVelocities");
			}
			base.RoutedEvent = Manipulation.ManipulationInertiaStartingEvent;
			this.ManipulationContainer = manipulationContainer;
			this.ManipulationOrigin = origin;
			this.InitialVelocities = initialVelocities;
			this._isInInertia = isInInertia;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00043930 File Offset: 0x00042D30
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
			if (base.RoutedEvent == Manipulation.ManipulationInertiaStartingEvent)
			{
				((EventHandler<ManipulationInertiaStartingEventArgs>)genericHandler)(genericTarget, this);
				return;
			}
			base.InvokeEventHandler(genericHandler, genericTarget);
		}

		/// <summary>Obtém o contêiner a que a propriedade <see cref="P:System.Windows.Input.ManipulationStartedEventArgs.ManipulationOrigin" /> é relativa.</summary>
		/// <returns>O contêiner a que a propriedade <see cref="P:System.Windows.Input.ManipulationStartedEventArgs.ManipulationOrigin" /> é relativa.</returns>
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x0004397C File Offset: 0x00042D7C
		// (set) Token: 0x06001213 RID: 4627 RVA: 0x00043990 File Offset: 0x00042D90
		public IInputElement ManipulationContainer { get; private set; }

		/// <summary>Obtém ou define o ponto que deu origem à manipulação.</summary>
		/// <returns>O ponto que deu origem à manipulação.</returns>
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x000439A4 File Offset: 0x00042DA4
		// (set) Token: 0x06001215 RID: 4629 RVA: 0x000439B8 File Offset: 0x00042DB8
		public Point ManipulationOrigin { get; set; }

		/// <summary>Obtém as taxas de alterações para a manipulação que ocorrem antes do início de inércia.</summary>
		/// <returns>As taxas de alterações para a manipulação que ocorrem antes do início de inércia.</returns>
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x000439CC File Offset: 0x00042DCC
		// (set) Token: 0x06001217 RID: 4631 RVA: 0x000439E0 File Offset: 0x00042DE0
		public ManipulationVelocities InitialVelocities { get; private set; }

		/// <summary>Obtém ou define a taxa de desaceleração do movimento de inércia linear.</summary>
		/// <returns>A taxa de desaceleração do movimento de inércia linear.</returns>
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x000439F4 File Offset: 0x00042DF4
		// (set) Token: 0x06001219 RID: 4633 RVA: 0x00043A30 File Offset: 0x00042E30
		public InertiaTranslationBehavior TranslationBehavior
		{
			get
			{
				if (!this.IsBehaviorSet(ManipulationInertiaStartingEventArgs.Behaviors.Translation))
				{
					this._translationBehavior = new InertiaTranslationBehavior(this.InitialVelocities.LinearVelocity);
					this.SetBehavior(ManipulationInertiaStartingEventArgs.Behaviors.Translation);
				}
				return this._translationBehavior;
			}
			set
			{
				this._translationBehavior = value;
			}
		}

		/// <summary>Obtém ou define a taxa de desaceleração do movimento de inércia rotacional.</summary>
		/// <returns>A taxa de desaceleração do movimento de inércia rotacional.</returns>
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x00043A44 File Offset: 0x00042E44
		// (set) Token: 0x0600121B RID: 4635 RVA: 0x00043A80 File Offset: 0x00042E80
		public InertiaRotationBehavior RotationBehavior
		{
			get
			{
				if (!this.IsBehaviorSet(ManipulationInertiaStartingEventArgs.Behaviors.Rotation))
				{
					this._rotationBehavior = new InertiaRotationBehavior(this.InitialVelocities.AngularVelocity);
					this.SetBehavior(ManipulationInertiaStartingEventArgs.Behaviors.Rotation);
				}
				return this._rotationBehavior;
			}
			set
			{
				this._rotationBehavior = value;
			}
		}

		/// <summary>Obtém ou define a taxa de desaceleração do movimento de inércia de expansão.</summary>
		/// <returns>A taxa de desaceleração do movimento de inércia de expansão</returns>
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x00043A94 File Offset: 0x00042E94
		// (set) Token: 0x0600121D RID: 4637 RVA: 0x00043AD0 File Offset: 0x00042ED0
		public InertiaExpansionBehavior ExpansionBehavior
		{
			get
			{
				if (!this.IsBehaviorSet(ManipulationInertiaStartingEventArgs.Behaviors.Expansion))
				{
					this._expansionBehavior = new InertiaExpansionBehavior(this.InitialVelocities.ExpansionVelocity);
					this.SetBehavior(ManipulationInertiaStartingEventArgs.Behaviors.Expansion);
				}
				return this._expansionBehavior;
			}
			set
			{
				this._expansionBehavior = value;
			}
		}

		/// <summary>Cancela a manipulação.</summary>
		/// <returns>
		///   <see langword="true" /> se a manipulação tiver sido cancelada com êxito; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600121E RID: 4638 RVA: 0x00043AE4 File Offset: 0x00042EE4
		public bool Cancel()
		{
			if (!this._isInInertia)
			{
				this.RequestedCancel = true;
				return true;
			}
			return false;
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x00043B04 File Offset: 0x00042F04
		// (set) Token: 0x06001220 RID: 4640 RVA: 0x00043B18 File Offset: 0x00042F18
		internal bool RequestedCancel { get; private set; }

		/// <summary>Obtém uma coleção de objetos que representa os contatos de toque da manipulação.</summary>
		/// <returns>Uma coleção de objetos que representa os contatos de manipulação de toque.</returns>
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x00043B2C File Offset: 0x00042F2C
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

		/// <summary>Especifica o comportamento de uma manipulação durante inércia.</summary>
		/// <param name="parameter">O objeto que especifica o comportamento de uma manipulação durante inércia.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="parameter" /> é <see langword="null" />.</exception>
		// Token: 0x06001222 RID: 4642 RVA: 0x00043B60 File Offset: 0x00042F60
		[Browsable(false)]
		public void SetInertiaParameter(InertiaParameters2D parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException("parameter");
			}
			if (this._inertiaParameters == null)
			{
				this._inertiaParameters = new List<InertiaParameters2D>();
			}
			this._inertiaParameters.Add(parameter);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00043B9C File Offset: 0x00042F9C
		internal bool CanBeginInertia()
		{
			return (this._inertiaParameters != null && this._inertiaParameters.Count > 0) || (this._translationBehavior != null && this._translationBehavior.CanUseForInertia()) || (this._rotationBehavior != null && this._rotationBehavior.CanUseForInertia()) || (this._expansionBehavior != null && this._expansionBehavior.CanUseForInertia());
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00043C08 File Offset: 0x00043008
		internal void ApplyParameters(InertiaProcessor2D processor)
		{
			processor.InitialOriginX = (float)this.ManipulationOrigin.X;
			processor.InitialOriginY = (float)this.ManipulationOrigin.Y;
			ManipulationVelocities initialVelocities = this.InitialVelocities;
			InertiaTranslationBehavior.ApplyParameters(this._translationBehavior, processor, initialVelocities.LinearVelocity);
			InertiaRotationBehavior.ApplyParameters(this._rotationBehavior, processor, initialVelocities.AngularVelocity);
			InertiaExpansionBehavior.ApplyParameters(this._expansionBehavior, processor, initialVelocities.ExpansionVelocity);
			if (this._inertiaParameters != null)
			{
				int i = 0;
				int count = this._inertiaParameters.Count;
				while (i < count)
				{
					processor.SetParameters(this._inertiaParameters[i]);
					i++;
				}
			}
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00043CB0 File Offset: 0x000430B0
		private bool IsBehaviorSet(ManipulationInertiaStartingEventArgs.Behaviors behavior)
		{
			return (this._behaviors & behavior) == behavior;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00043CC8 File Offset: 0x000430C8
		private void SetBehavior(ManipulationInertiaStartingEventArgs.Behaviors behavior)
		{
			this._behaviors |= behavior;
		}

		// Token: 0x040009D4 RID: 2516
		private List<InertiaParameters2D> _inertiaParameters;

		// Token: 0x040009D5 RID: 2517
		private InertiaTranslationBehavior _translationBehavior;

		// Token: 0x040009D6 RID: 2518
		private InertiaRotationBehavior _rotationBehavior;

		// Token: 0x040009D7 RID: 2519
		private InertiaExpansionBehavior _expansionBehavior;

		// Token: 0x040009D8 RID: 2520
		private ManipulationInertiaStartingEventArgs.Behaviors _behaviors;

		// Token: 0x040009D9 RID: 2521
		private bool _isInInertia;

		// Token: 0x040009DA RID: 2522
		private IEnumerable<IManipulator> _manipulators;

		// Token: 0x0200080D RID: 2061
		[Flags]
		private enum Behaviors
		{
			// Token: 0x04002722 RID: 10018
			None = 0,
			// Token: 0x04002723 RID: 10019
			Translation = 1,
			// Token: 0x04002724 RID: 10020
			Rotation = 2,
			// Token: 0x04002725 RID: 10021
			Expansion = 4
		}
	}
}
