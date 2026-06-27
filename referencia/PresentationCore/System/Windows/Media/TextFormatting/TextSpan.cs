using System;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Representa uma classe genérica que permite que um emparelhamento simples de um objeto do tipo T e um comprimento de execução especificado.</summary>
	/// <typeparam name="T">O tipo de objeto para o par.</typeparam>
	// Token: 0x020005B8 RID: 1464
	public class TextSpan<T>
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextFormatting.TextSpan`1" /> especificando o comprimento do intervalo de texto e o valor associado a ele.</summary>
		/// <param name="length">Um valor <see cref="T:System.Int32" /> que representa o número de caracteres do trecho de texto.</param>
		/// <param name="value">O objeto associado com o intervalo de texto.</param>
		// Token: 0x060042FA RID: 17146 RVA: 0x00104208 File Offset: 0x00103608
		public TextSpan(int length, T value)
		{
			this._length = length;
			this._value = value;
		}

		/// <summary>Obtém o número de caracteres na extensão de texto.</summary>
		/// <returns>Um <see cref="T:System.Int32" /> valor que representa o comprimento do intervalo de texto.</returns>
		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x060042FB RID: 17147 RVA: 0x0010422C File Offset: 0x0010362C
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		/// <summary>Obtém o objeto associado com o intervalo de texto.</summary>
		/// <returns>Um objeto do tipo <paramref name="T" />.</returns>
		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x060042FC RID: 17148 RVA: 0x00104240 File Offset: 0x00103640
		public T Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x04001845 RID: 6213
		private int _length;

		// Token: 0x04001846 RID: 6214
		private T _value;
	}
}
