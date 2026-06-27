using System;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Input
{
	/// <summary>Fornece a posição de entrada necessária para criar uma manipulação.</summary>
	// Token: 0x0200023D RID: 573
	public interface IManipulator
	{
		/// <summary>Obtém ou define um identificador exclusivo para o objeto.</summary>
		/// <returns>Um identificador exclusivo para o objeto.</returns>
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000FE7 RID: 4071
		int Id { get; }

		/// <summary>Retorna a posição do objeto <see cref="T:System.Windows.Input.IManipulator" />.</summary>
		/// <param name="relativeTo">O elemento a ser usado como o quadro de referência para calcular a posição do <see cref="T:System.Windows.Input.IManipulator" />.</param>
		/// <returns>A posição do objeto <see cref="T:System.Windows.Input.IManipulator" />.</returns>
		// Token: 0x06000FE8 RID: 4072
		Point GetPosition(IInputElement relativeTo);

		/// <summary>Ocorre quando o objeto <see cref="T:System.Windows.Input.IManipulator" /> muda de posição.</summary>
		// Token: 0x1400014E RID: 334
		// (add) Token: 0x06000FE9 RID: 4073
		// (remove) Token: 0x06000FEA RID: 4074
		event EventHandler Updated;

		/// <summary>Chamado quando a manipulação termina.</summary>
		/// <param name="cancel">
		///   <see langword="true" /> se a manipulação estiver cancelada; caso contrário, <see langword="false" />.</param>
		// Token: 0x06000FEB RID: 4075
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		void ManipulationEnded(bool cancel);
	}
}
