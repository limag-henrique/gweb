using System;

namespace System.Windows.Input
{
	/// <summary>Define um objeto que sabe como invocar um comando.</summary>
	// Token: 0x02000210 RID: 528
	public interface ICommandSource
	{
		/// <summary>Obtém o comando que será executado quando a fonte do comando for invocada.</summary>
		/// <returns>O comando que será executado quando a fonte do comando for invocada.</returns>
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000DF9 RID: 3577
		ICommand Command { get; }

		/// <summary>Representa um valor de dados definido pelo usuário que pode ser passado para o comando quando ele é executado.</summary>
		/// <returns>Os dados específicos do comando.</returns>
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000DFA RID: 3578
		object CommandParameter { get; }

		/// <summary>O objeto no qual o comando está sendo executado.</summary>
		/// <returns>O objeto no qual o comando está sendo executado.</returns>
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000DFB RID: 3579
		IInputElement CommandTarget { get; }
	}
}
