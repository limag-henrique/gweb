using System;

namespace System.Windows.Input
{
	/// <summary>Classe abstrata que descreve os gestos do dispositivo de entrada.</summary>
	// Token: 0x02000213 RID: 531
	public abstract class InputGesture
	{
		/// <summary>Quando substituído em uma classe derivada, determina se o <see cref="T:System.Windows.Input.InputGesture" /> especificado corresponde à entrada associada ao objeto <see cref="T:System.Windows.Input.InputEventArgs" /> especificado.</summary>
		/// <param name="targetElement">O destino do comando.</param>
		/// <param name="inputEventArgs">Os dados de evento de entrada aos quais comparar esse gesto.</param>
		/// <returns>
		///   <see langword="true" /> se o gesto corresponde à entrada; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000E2F RID: 3631
		public abstract bool Matches(object targetElement, InputEventArgs inputEventArgs);
	}
}
