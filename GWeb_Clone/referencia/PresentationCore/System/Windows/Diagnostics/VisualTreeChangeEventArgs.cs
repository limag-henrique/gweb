using System;

namespace System.Windows.Diagnostics
{
	/// <summary>Fornece dados para o evento de <see cref="E:System.Windows.Diagnostics.VisualDiagnostics.VisualTreeChanged" /> .</summary>
	// Token: 0x0200031B RID: 795
	public class VisualTreeChangeEventArgs : EventArgs
	{
		/// <summary>Obtém o <see cref="T:System.Windows.DependencyObject" /> no qual um elemento (filho) foi adicionado ou removido.</summary>
		/// <returns>O elemento ao qual o elemento filho foi adicionado ou removido.</returns>
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001A2C RID: 6700 RVA: 0x00067874 File Offset: 0x00066C74
		// (set) Token: 0x06001A2D RID: 6701 RVA: 0x00067888 File Offset: 0x00066C88
		public DependencyObject Parent { get; private set; }

		/// <summary>Obtém o <see cref="T:System.Windows.DependencyObject" /> que foi adicionado a outro elemento (pai) ou removido dele.</summary>
		/// <returns>O elemento que foi adicionado ou removido do elemento pai.</returns>
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001A2E RID: 6702 RVA: 0x0006789C File Offset: 0x00066C9C
		// (set) Token: 0x06001A2F RID: 6703 RVA: 0x000678B0 File Offset: 0x00066CB0
		public DependencyObject Child { get; private set; }

		/// <summary>Obtém índice nos filhos do elemento pai no qual o elemento filho está localizado quando a operação de adicionar ou remover é concluída. Em uma remoção, esse valor sempre será igual a -1.</summary>
		/// <returns>O índice nos filhos do elemento pai onde o elemento filho está localizado quando a operação de adicionar ou remover é concluída.</returns>
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001A30 RID: 6704 RVA: 0x000678C4 File Offset: 0x00066CC4
		// (set) Token: 0x06001A31 RID: 6705 RVA: 0x000678D8 File Offset: 0x00066CD8
		public int ChildIndex { get; private set; }

		/// <summary>Obtém um valor que indica se o elemento filho foi adicionado a outro elemento (pai) ou removido dele.</summary>
		/// <returns>Um valor de enumeração que indica se o elemento filho foi adicionado ou removido.</returns>
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001A32 RID: 6706 RVA: 0x000678EC File Offset: 0x00066CEC
		// (set) Token: 0x06001A33 RID: 6707 RVA: 0x00067900 File Offset: 0x00066D00
		public VisualTreeChangeType ChangeType { get; private set; }

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Diagnostics.VisualTreeChangeEventArgs" />.</summary>
		/// <param name="parent">O <see cref="T:System.Windows.DependencyObject" /> ao qual um elemento (filho) foi adicionado ou do qual foi removido.</param>
		/// <param name="child">O <see cref="T:System.Windows.DependencyObject" /> que foi adicionado a outro elemento (pai) ou removido dele.</param>
		/// <param name="childIndex">O índice nos filhos do elemento pai no qual o elemento filho está localizado quando a operação de adicionar ou remover é concluída.</param>
		/// <param name="changeType">Um valor que indica se o elemento filho foi adicionado a outro elemento (pai) ou removido dele.</param>
		// Token: 0x06001A34 RID: 6708 RVA: 0x00067914 File Offset: 0x00066D14
		public VisualTreeChangeEventArgs(DependencyObject parent, DependencyObject child, int childIndex, VisualTreeChangeType changeType)
		{
			this.Parent = parent;
			this.Child = child;
			this.ChildIndex = childIndex;
			this.ChangeType = changeType;
		}
	}
}
