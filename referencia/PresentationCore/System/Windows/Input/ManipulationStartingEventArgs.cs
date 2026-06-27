using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input.Manipulations;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Fornece dados para o evento <see cref="E:System.Windows.UIElement.ManipulationStarting" />.</summary>
	// Token: 0x0200027A RID: 634
	public sealed class ManipulationStartingEventArgs : InputEventArgs
	{
		// Token: 0x06001268 RID: 4712 RVA: 0x00044C78 File Offset: 0x00044078
		internal ManipulationStartingEventArgs(ManipulationDevice manipulationDevice, int timestamp) : base(manipulationDevice, timestamp)
		{
			base.RoutedEvent = Manipulation.ManipulationStartingEvent;
			this.Mode = ManipulationModes.All;
			this.IsSingleTouchEnabled = true;
		}

		/// <summary>Obtém ou define os tipos de manipulações possíveis.</summary>
		/// <returns>Um dos valores de enumeração. O padrão é <see cref="F:System.Windows.Input.ManipulationModes.All" />.</returns>
		/// <exception cref="T:System.ArgumentException">A propriedade é definida como um valor diferente de um ou mais do valor de enumerações <see cref="T:System.Windows.Input.ManipulationModes" /></exception>
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x00044CA8 File Offset: 0x000440A8
		// (set) Token: 0x0600126A RID: 4714 RVA: 0x00044CBC File Offset: 0x000440BC
		public ManipulationModes Mode
		{
			get
			{
				return this._mode;
			}
			set
			{
				if ((value & ~(ManipulationModes.TranslateX | ManipulationModes.TranslateY | ManipulationModes.Rotate | ManipulationModes.Scale)) != ManipulationModes.None)
				{
					throw new ArgumentException(SR.Get("Manipulation_InvalidManipulationMode"), "value");
				}
				this._mode = value;
			}
		}

		/// <summary>Obtém ou define o contêiner ao qual todos os eventos de manipulação e cálculos são relativos.</summary>
		/// <returns>O contêiner que todos os eventos de manipulação e cálculos são relativos. O padrão é o elemento no qual o evento ocorreu.</returns>
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x00044CEC File Offset: 0x000440EC
		// (set) Token: 0x0600126C RID: 4716 RVA: 0x00044D00 File Offset: 0x00044100
		public IInputElement ManipulationContainer { get; set; }

		/// <summary>Obtém ou define um objeto que descreve o pivô para manipulação de um único ponto.</summary>
		/// <returns>Um objeto que descreve o pivô para manipulação de um único ponto.</returns>
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x00044D14 File Offset: 0x00044114
		// (set) Token: 0x0600126E RID: 4718 RVA: 0x00044D28 File Offset: 0x00044128
		public ManipulationPivot Pivot { get; set; }

		/// <summary>Obtém ou define se um dedo pode iniciar uma manipulação.</summary>
		/// <returns>
		///   <see langword="true" /> um dedo pode iniciar uma manipulação; Caso contrário, <see langword="false" />. O padrão é <see langword="true" />.</returns>
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600126F RID: 4719 RVA: 0x00044D3C File Offset: 0x0004413C
		// (set) Token: 0x06001270 RID: 4720 RVA: 0x00044D50 File Offset: 0x00044150
		public bool IsSingleTouchEnabled { get; set; }

		/// <summary>Cancela a manipulação e promove o toque para eventos de mouse.</summary>
		/// <returns>
		///   <see langword="true" /> se o toque tiver sido promovido com êxito para eventos de mouse; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001271 RID: 4721 RVA: 0x00044D64 File Offset: 0x00044164
		public bool Cancel()
		{
			this.RequestedCancel = true;
			return true;
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x00044D7C File Offset: 0x0004417C
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x00044D90 File Offset: 0x00044190
		internal bool RequestedCancel { get; private set; }

		/// <summary>Obtém uma coleção de objetos que representa os contatos de toque da manipulação.</summary>
		/// <returns>Uma coleção de objetos que representa os contatos de manipulação de toque.</returns>
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x00044DA4 File Offset: 0x000441A4
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

		// Token: 0x06001275 RID: 4725 RVA: 0x00044DD8 File Offset: 0x000441D8
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
			if (base.RoutedEvent == Manipulation.ManipulationStartingEvent)
			{
				((EventHandler<ManipulationStartingEventArgs>)genericHandler)(genericTarget, this);
				return;
			}
			base.InvokeEventHandler(genericHandler, genericTarget);
		}

		/// <summary>Adiciona parâmetros à manipulação atual do elemento especificado.</summary>
		/// <param name="parameter">O parâmetro a adicionar.</param>
		// Token: 0x06001276 RID: 4726 RVA: 0x00044E24 File Offset: 0x00044224
		[Browsable(false)]
		public void SetManipulationParameter(ManipulationParameters2D parameter)
		{
			if (this._parameters == null)
			{
				this._parameters = new List<ManipulationParameters2D>(1);
			}
			this._parameters.Add(parameter);
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x00044E54 File Offset: 0x00044254
		internal IList<ManipulationParameters2D> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x04000A02 RID: 2562
		private List<ManipulationParameters2D> _parameters;

		// Token: 0x04000A03 RID: 2563
		private ManipulationModes _mode;

		// Token: 0x04000A04 RID: 2564
		private IEnumerable<IManipulator> _manipulators;
	}
}
