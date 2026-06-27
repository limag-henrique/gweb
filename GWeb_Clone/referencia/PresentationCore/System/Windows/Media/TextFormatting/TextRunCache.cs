using System;
using System.Collections.Generic;
using MS.Internal.PresentationCore;
using MS.Internal.TextFormatting;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Fornece serviços de armazenamento em cache para o <see cref="T:System.Windows.Media.TextFormatting.TextFormatter" /> objeto para melhorar o desempenho.</summary>
	// Token: 0x020005B3 RID: 1459
	public sealed class TextRunCache
	{
		/// <summary>Notifica o cliente do mecanismo de texto de uma alteração para o cache quando o conteúdo de texto ou texto executar as propriedades de <see cref="T:System.Windows.Media.TextFormatting.TextRun" /> adicionado, removido ou substituído.</summary>
		/// <param name="textSourceCharacterIndex">Especifica o <see cref="T:System.Windows.Media.TextFormatting.TextSource" /> posição de índice do início da alteração de caracteres.</param>
		/// <param name="addition">Indica o número de <see cref="T:System.Windows.Media.TextFormatting.TextSource" /> caracteres a ser adicionado.</param>
		/// <param name="removal">Indica o número de <see cref="T:System.Windows.Media.TextFormatting.TextSource" /> caracteres a ser removido.</param>
		// Token: 0x060042AF RID: 17071 RVA: 0x00103FA4 File Offset: 0x001033A4
		public void Change(int textSourceCharacterIndex, int addition, int removal)
		{
			if (this._imp != null)
			{
				this._imp.Change(textSourceCharacterIndex, addition, removal);
			}
		}

		/// <summary>Sinaliza o cliente do mecanismo de texto para invalidar todo o conteúdo do <see cref="T:System.Windows.Media.TextFormatting.TextFormatter" /> cache.</summary>
		// Token: 0x060042B0 RID: 17072 RVA: 0x00103FC8 File Offset: 0x001033C8
		public void Invalidate()
		{
			if (this._imp != null)
			{
				this._imp = null;
			}
		}

		// Token: 0x060042B1 RID: 17073 RVA: 0x00103FE4 File Offset: 0x001033E4
		[FriendAccessAllowed]
		internal IList<TextSpan<TextRun>> GetTextRunSpans()
		{
			if (this._imp != null)
			{
				return this._imp.GetTextRunSpans();
			}
			return new TextSpan<TextRun>[0];
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x060042B2 RID: 17074 RVA: 0x0010400C File Offset: 0x0010340C
		// (set) Token: 0x060042B3 RID: 17075 RVA: 0x00104020 File Offset: 0x00103420
		internal TextRunCacheImp Imp
		{
			get
			{
				return this._imp;
			}
			set
			{
				this._imp = value;
			}
		}

		// Token: 0x0400183F RID: 6207
		private TextRunCacheImp _imp;
	}
}
