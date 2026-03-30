using System;
using System.Windows.Threading;

namespace System.Windows.Input
{
	/// <summary>Classe abstrata que descreve um dispositivo de entrada.</summary>
	// Token: 0x02000242 RID: 578
	public abstract class InputDevice : DispatcherObject
	{
		/// <summary>Quando substituído em uma classe derivada, obtém o elemento que recebe entrada deste dispositivo.</summary>
		/// <returns>O elemento que recebe entrada.</returns>
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06001011 RID: 4113
		public abstract IInputElement Target { get; }

		/// <summary>Quando substituído em uma classe derivada, obtém o <see cref="T:System.Windows.PresentationSource" /> que está relatando a entrada para este dispositivo.</summary>
		/// <returns>A fonte que está relatando a entrada para este dispositivo.</returns>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06001012 RID: 4114
		public abstract PresentationSource ActiveSource { get; }
	}
}
