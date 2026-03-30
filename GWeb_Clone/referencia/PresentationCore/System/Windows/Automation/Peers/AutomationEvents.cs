using System;

namespace System.Windows.Automation.Peers
{
	/// <summary>Especifica o evento gerado pelo elemento por meio do <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> associado.</summary>
	// Token: 0x02000312 RID: 786
	public enum AutomationEvents
	{
		/// <summary>
		///   <see cref="F:System.Windows.Automation.AutomationElementIdentifiers.ToolTipOpenedEvent" />
		/// </summary>
		// Token: 0x04000DC8 RID: 3528
		ToolTipOpened,
		/// <summary>
		///   <see cref="F:System.Windows.Automation.AutomationElementIdentifiers.ToolTipClosedEvent" />
		/// </summary>
		// Token: 0x04000DC9 RID: 3529
		ToolTipClosed,
		/// <summary>
		///   <see cref="F:System.Windows.Automation.AutomationElementIdentifiers.MenuOpenedEvent" />
		/// </summary>
		// Token: 0x04000DCA RID: 3530
		MenuOpened,
		/// <summary>
		///   <see cref="F:System.Windows.Automation.AutomationElementIdentifiers.MenuClosedEvent" />
		/// </summary>
		// Token: 0x04000DCB RID: 3531
		MenuClosed,
		/// <summary>
		///   <see cref="F:System.Windows.Automation.AutomationElementIdentifiers.AutomationFocusChangedEvent" />
		/// </summary>
		// Token: 0x04000DCC RID: 3532
		AutomationFocusChanged,
		/// <summary>
		///   <see cref="F:System.Windows.Automation.InvokePatternIdentifiers.InvokedEvent" />
		/// </summary>
		// Token: 0x04000DCD RID: 3533
		InvokePatternOnInvoked,
		/// <summary>
		///   <see cref="F:System.Windows.Automation.SelectionItemPatternIdentifiers.ElementAddedToSelectionEvent" />
		/// </summary>
		// Token: 0x04000DCE RID: 3534
		SelectionItemPatternOnElementAddedToSelection,
		/// <summary>
		///   <see cref="F:System.Windows.Automation.SelectionItemPatternIdentifiers.ElementRemovedFromSelectionEvent" />
		/// </summary>
		// Token: 0x04000DCF RID: 3535
		SelectionItemPatternOnElementRemovedFromSelection,
		/// <summary>
		///   <see cref="F:System.Windows.Automation.SelectionItemPatternIdentifiers.ElementSelectedEvent" />
		/// </summary>
		// Token: 0x04000DD0 RID: 3536
		SelectionItemPatternOnElementSelected,
		/// <summary>
		///   <see cref="F:System.Windows.Automation.SelectionPatternIdentifiers.InvalidatedEvent" />
		/// </summary>
		// Token: 0x04000DD1 RID: 3537
		SelectionPatternOnInvalidated,
		/// <summary>
		///   <see cref="F:System.Windows.Automation.TextPatternIdentifiers.TextSelectionChangedEvent" />
		/// </summary>
		// Token: 0x04000DD2 RID: 3538
		TextPatternOnTextSelectionChanged,
		/// <summary>
		///   <see cref="F:System.Windows.Automation.TextPatternIdentifiers.TextChangedEvent" />
		/// </summary>
		// Token: 0x04000DD3 RID: 3539
		TextPatternOnTextChanged,
		/// <summary>
		///   <see cref="F:System.Windows.Automation.AutomationElementIdentifiers.AsyncContentLoadedEvent" />
		/// </summary>
		// Token: 0x04000DD4 RID: 3540
		AsyncContentLoaded,
		/// <summary>Usado para gerar uma notificação de que uma propriedade foi alterada.</summary>
		// Token: 0x04000DD5 RID: 3541
		PropertyChanged,
		/// <summary>
		///   <see cref="F:System.Windows.Automation.AutomationElementIdentifiers.StructureChangedEvent" />
		/// </summary>
		// Token: 0x04000DD6 RID: 3542
		StructureChanged,
		/// <summary>
		///   <see cref="F:System.Windows.Automation.SynchronizedInputPatternIdentifiers.InputReachedTargetEvent" />
		/// </summary>
		// Token: 0x04000DD7 RID: 3543
		InputReachedTarget,
		/// <summary>
		///   <see cref="F:System.Windows.Automation.SynchronizedInputPatternIdentifiers.InputReachedOtherElementEvent" />
		/// </summary>
		// Token: 0x04000DD8 RID: 3544
		InputReachedOtherElement,
		/// <summary>
		///   <see cref="F:System.Windows.Automation.SynchronizedInputPatternIdentifiers.InputDiscardedEvent" />
		/// </summary>
		// Token: 0x04000DD9 RID: 3545
		InputDiscarded,
		/// <summary>
		///   <see cref="F:System.Windows.Automation.AutomationElementIdentifiers.LiveRegionChangedEvent" />. Disponível do .NET Framework 4.7.1 em diante.</summary>
		// Token: 0x04000DDA RID: 3546
		LiveRegionChanged,
		// Token: 0x04000DDB RID: 3547
		Notification,
		// Token: 0x04000DDC RID: 3548
		ActiveTextPositionChanged
	}
}
