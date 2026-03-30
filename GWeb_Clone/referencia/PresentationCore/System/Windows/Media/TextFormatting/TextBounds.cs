using System;
using System.Collections.Generic;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Representa o retângulo delimitador de um intervalo de caracteres.</summary>
	// Token: 0x02000598 RID: 1432
	public sealed class TextBounds
	{
		// Token: 0x060041E3 RID: 16867 RVA: 0x00102784 File Offset: 0x00101B84
		internal TextBounds(Rect bounds, FlowDirection flowDirection, IList<TextRunBounds> runBounds)
		{
			this._bounds = bounds;
			this._flowDirection = flowDirection;
			this._runBounds = runBounds;
		}

		/// <summary>Obtém o retângulo delimitador do objeto <see cref="T:System.Windows.Media.TextFormatting.TextBounds" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Rect" /> valor que representa o retângulo delimitador de um intervalo de caracteres.</returns>
		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x060041E4 RID: 16868 RVA: 0x001027AC File Offset: 0x00101BAC
		public Rect Rectangle
		{
			get
			{
				return this._bounds;
			}
		}

		/// <summary>Obtém uma lista de objetos <see cref="T:System.Windows.Media.TextFormatting.TextRunBounds" />.</summary>
		/// <returns>Uma lista de objetos <see cref="T:System.Windows.Media.TextFormatting.TextRunBounds" />.</returns>
		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x060041E5 RID: 16869 RVA: 0x001027C0 File Offset: 0x00101BC0
		public IList<TextRunBounds> TextRunBounds
		{
			get
			{
				return this._runBounds;
			}
		}

		/// <summary>Obtém a direção do fluxo de texto do objeto <see cref="T:System.Windows.Media.TextFormatting.TextBounds" />.</summary>
		/// <returns>Um valor enumerado de <see cref="T:System.Windows.FlowDirection" />.</returns>
		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x060041E6 RID: 16870 RVA: 0x001027D4 File Offset: 0x00101BD4
		public FlowDirection FlowDirection
		{
			get
			{
				return this._flowDirection;
			}
		}

		// Token: 0x0400180D RID: 6157
		private FlowDirection _flowDirection;

		// Token: 0x0400180E RID: 6158
		private Rect _bounds;

		// Token: 0x0400180F RID: 6159
		private IList<TextRunBounds> _runBounds;
	}
}
