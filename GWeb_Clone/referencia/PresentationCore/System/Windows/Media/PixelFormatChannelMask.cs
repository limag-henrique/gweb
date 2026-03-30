using System;
using System.Collections.Generic;
using MS.Internal;

namespace System.Windows.Media
{
	/// <summary>Define a máscara de bits e o deslocamento para formatos de pixel específicos</summary>
	// Token: 0x0200042D RID: 1069
	public struct PixelFormatChannelMask
	{
		// Token: 0x06002BCB RID: 11211 RVA: 0x000AEB18 File Offset: 0x000ADF18
		internal PixelFormatChannelMask(byte[] mask)
		{
			this._mask = mask;
		}

		/// <summary>Obtém uma bitmask para um canal de cor. O valor nunca será maior do que 0xffffffff</summary>
		/// <returns>A máscara de bits para um canal de cor.</returns>
		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x000AEB2C File Offset: 0x000ADF2C
		public IList<byte> Mask
		{
			get
			{
				if (this._mask == null)
				{
					return null;
				}
				return new PartialList<byte>((byte[])this._mask.Clone());
			}
		}

		/// <summary>Compara duas instâncias de <see cref="T:System.Windows.Media.PixelFormatChannelMask" /> quanto à igualdade.</summary>
		/// <param name="left">A primeira máscara a ser comparada.</param>
		/// <param name="right">A segunda máscara a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se as duas máscaras forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002BCD RID: 11213 RVA: 0x000AEB58 File Offset: 0x000ADF58
		public static bool operator ==(PixelFormatChannelMask left, PixelFormatChannelMask right)
		{
			return PixelFormatChannelMask.Equals(left, right);
		}

		/// <summary>Determina se duas máscaras de canal de formato de pixel são iguais.</summary>
		/// <param name="left">A primeira máscara a ser comparada.</param>
		/// <param name="right">A segunda máscara a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se as máscaras forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002BCE RID: 11214 RVA: 0x000AEB6C File Offset: 0x000ADF6C
		public static bool Equals(PixelFormatChannelMask left, PixelFormatChannelMask right)
		{
			int num = (left._mask != null) ? left._mask.Length : 0;
			int num2 = (right._mask != null) ? right._mask.Length : 0;
			if (num != num2)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (left._mask[i] != right._mask[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Compara a desigualdade de duas instâncias <see cref="T:System.Windows.Media.PixelFormatChannelMask" />.</summary>
		/// <param name="left">A primeira máscara a ser comparada.</param>
		/// <param name="right">A segunda máscara a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se os dois objetos <see cref="T:System.Windows.Media.PixelFormatChannelMask" /> não forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002BCF RID: 11215 RVA: 0x000AEBC8 File Offset: 0x000ADFC8
		public static bool operator !=(PixelFormatChannelMask left, PixelFormatChannelMask right)
		{
			return !(left == right);
		}

		/// <summary>Determina se o objeto especificado é igual ao objeto atual.</summary>
		/// <param name="obj">O <see cref="T:System.Object" /> a ser comparado com a máscara atual.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.Media.PixelFormatChannelMask" /> for igual a <paramref name="obj" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002BD0 RID: 11216 RVA: 0x000AEBE0 File Offset: 0x000ADFE0
		public override bool Equals(object obj)
		{
			return obj is PixelFormatChannelMask && this == (PixelFormatChannelMask)obj;
		}

		/// <summary>Recupera um código hash para a máscara.</summary>
		/// <returns>O código hash de uma máscara.</returns>
		// Token: 0x06002BD1 RID: 11217 RVA: 0x000AEC08 File Offset: 0x000AE008
		public override int GetHashCode()
		{
			int num = 0;
			if (this._mask != null)
			{
				int i = 0;
				int num2 = this._mask.Length;
				while (i < num2)
				{
					num += (int)this._mask[i] * 256 * i;
					i++;
				}
			}
			return num;
		}

		// Token: 0x04001413 RID: 5139
		private byte[] _mask;
	}
}
