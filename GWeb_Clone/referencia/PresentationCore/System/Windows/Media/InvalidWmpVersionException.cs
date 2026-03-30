using System;
using System.Runtime.Serialization;

namespace System.Windows.Media
{
	/// <summary>A exceção que é gerada quando a versão instalada do Microsoft Windows Media Player não é compatível.</summary>
	// Token: 0x0200041A RID: 1050
	[Serializable]
	public class InvalidWmpVersionException : SystemException
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.InvalidWmpVersionException" />.</summary>
		// Token: 0x06002A36 RID: 10806 RVA: 0x000A95D8 File Offset: 0x000A89D8
		public InvalidWmpVersionException()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.InvalidWmpVersionException" /> com a mensagem de erro especificada.</summary>
		/// <param name="message">A mensagem de erro usada para inicializar a exceção.</param>
		// Token: 0x06002A37 RID: 10807 RVA: 0x000A95EC File Offset: 0x000A89EC
		public InvalidWmpVersionException(string message) : base(message)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.InvalidWmpVersionException" /> com informações de serialização.</summary>
		/// <param name="info">Informações de serialização sobre o objeto.</param>
		/// <param name="context">Informações de contexto sobre o fluxo serializado.</param>
		// Token: 0x06002A38 RID: 10808 RVA: 0x000A9600 File Offset: 0x000A8A00
		protected InvalidWmpVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.InvalidWmpVersionException" /> com a mensagem de erro especificada e uma referência à exceção interna que provocou essa exceção.</summary>
		/// <param name="message">A descrição do erro.</param>
		/// <param name="innerException">A exceção interna que causou esta exceção.</param>
		// Token: 0x06002A39 RID: 10809 RVA: 0x000A9618 File Offset: 0x000A8A18
		public InvalidWmpVersionException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
