using System;

namespace System.Windows.Documents
{
	/// <summary>Classe abstrata que representa a posição do conteúdo. Esta posição é específica do conteúdo.</summary>
	// Token: 0x020002FE RID: 766
	public abstract class ContentPosition
	{
		/// <summary>Representação estática de um ContentPosition não existente.</summary>
		// Token: 0x04000D49 RID: 3401
		public static readonly ContentPosition Missing = new ContentPosition.MissingContentPosition();

		// Token: 0x0200083D RID: 2109
		private class MissingContentPosition : ContentPosition
		{
		}
	}
}
