using System;

namespace System.Windows.Automation.Peers
{
	/// <summary>Especifica o padrão de controle retornado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetPattern(System.Windows.Automation.Peers.PatternInterface)" />.</summary>
	// Token: 0x0200030F RID: 783
	public enum PatternInterface
	{
		/// <summary>Valor correspondente à interface de padrão do controle <see cref="T:System.Windows.Automation.Provider.IInvokeProvider" />.</summary>
		// Token: 0x04000D86 RID: 3462
		Invoke,
		/// <summary>Valor correspondente à interface de padrão do controle <see cref="T:System.Windows.Automation.Provider.ISelectionProvider" />.</summary>
		// Token: 0x04000D87 RID: 3463
		Selection,
		/// <summary>Valor correspondente à interface de padrão do controle <see cref="T:System.Windows.Automation.Provider.IValueProvider" />.</summary>
		// Token: 0x04000D88 RID: 3464
		Value,
		/// <summary>Valor correspondente à interface de padrão do controle <see cref="T:System.Windows.Automation.Provider.IRangeValueProvider" />.</summary>
		// Token: 0x04000D89 RID: 3465
		RangeValue,
		/// <summary>Valor correspondente à interface de padrão do controle <see cref="T:System.Windows.Automation.Provider.IScrollProvider" />.</summary>
		// Token: 0x04000D8A RID: 3466
		Scroll,
		/// <summary>Valor correspondente à interface de padrão do controle <see cref="T:System.Windows.Automation.Provider.IScrollItemProvider" />.</summary>
		// Token: 0x04000D8B RID: 3467
		ScrollItem,
		/// <summary>Valor correspondente à interface de padrão do controle <see cref="T:System.Windows.Automation.Provider.IExpandCollapseProvider" />.</summary>
		// Token: 0x04000D8C RID: 3468
		ExpandCollapse,
		/// <summary>Valor correspondente à interface de padrão do controle <see cref="T:System.Windows.Automation.Provider.IGridProvider" />.</summary>
		// Token: 0x04000D8D RID: 3469
		Grid,
		/// <summary>Valor correspondente à interface de padrão do controle <see cref="T:System.Windows.Automation.Provider.IGridItemProvider" />.</summary>
		// Token: 0x04000D8E RID: 3470
		GridItem,
		/// <summary>Valor correspondente à interface de padrão do controle <see cref="T:System.Windows.Automation.Provider.IMultipleViewProvider" />.</summary>
		// Token: 0x04000D8F RID: 3471
		MultipleView,
		/// <summary>Valor correspondente à interface de padrão do controle <see cref="T:System.Windows.Automation.Provider.IWindowProvider" />.</summary>
		// Token: 0x04000D90 RID: 3472
		Window,
		/// <summary>Valor correspondente à interface de padrão do controle <see cref="T:System.Windows.Automation.Provider.ISelectionItemProvider" />.</summary>
		// Token: 0x04000D91 RID: 3473
		SelectionItem,
		/// <summary>Valor correspondente à interface de padrão do controle <see cref="T:System.Windows.Automation.Provider.IDockProvider" />.</summary>
		// Token: 0x04000D92 RID: 3474
		Dock,
		/// <summary>Valor correspondente à interface de padrão do controle <see cref="T:System.Windows.Automation.Provider.ITableProvider" />.</summary>
		// Token: 0x04000D93 RID: 3475
		Table,
		/// <summary>Valor correspondente à interface de padrão do controle <see cref="T:System.Windows.Automation.Provider.ITableItemProvider" />.</summary>
		// Token: 0x04000D94 RID: 3476
		TableItem,
		/// <summary>Valor correspondente à interface de padrão do controle <see cref="T:System.Windows.Automation.Provider.IToggleProvider" />.</summary>
		// Token: 0x04000D95 RID: 3477
		Toggle,
		/// <summary>Valor correspondente à interface de padrão do controle <see cref="T:System.Windows.Automation.Provider.ITransformProvider" />.</summary>
		// Token: 0x04000D96 RID: 3478
		Transform,
		/// <summary>Valor correspondente à interface de padrão do controle <see cref="T:System.Windows.Automation.Provider.ITextProvider" />.</summary>
		// Token: 0x04000D97 RID: 3479
		Text,
		/// <summary>Valor correspondente à interface de padrão do controle <see cref="T:System.Windows.Automation.Provider.IItemContainerProvider" />.</summary>
		// Token: 0x04000D98 RID: 3480
		ItemContainer,
		/// <summary>Valor correspondente à interface de padrão do controle <see cref="T:System.Windows.Automation.Provider.IVirtualizedItemProvider" />.</summary>
		// Token: 0x04000D99 RID: 3481
		VirtualizedItem,
		/// <summary>Valor correspondente à interface de padrão do controle <see cref="T:System.Windows.Automation.Provider.ISynchronizedInputProvider" />.</summary>
		// Token: 0x04000D9A RID: 3482
		SynchronizedInput
	}
}
