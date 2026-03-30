using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Threading;
using MS.Internal.PresentationCore;

namespace MS.Internal.Automation
{
	// Token: 0x0200078C RID: 1932
	internal static class EventMap
	{
		// Token: 0x0600512C RID: 20780 RVA: 0x00144E6C File Offset: 0x0014426C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool IsKnownLegacyEvent(int id)
		{
			if (id != AutomationElementIdentifiers.ToolTipOpenedEvent.Id && id != AutomationElementIdentifiers.ToolTipClosedEvent.Id && id != AutomationElementIdentifiers.MenuOpenedEvent.Id && id != AutomationElementIdentifiers.MenuClosedEvent.Id && id != AutomationElementIdentifiers.AutomationFocusChangedEvent.Id && id != InvokePatternIdentifiers.InvokedEvent.Id && id != SelectionItemPatternIdentifiers.ElementAddedToSelectionEvent.Id && id != SelectionItemPatternIdentifiers.ElementRemovedFromSelectionEvent.Id && id != SelectionItemPatternIdentifiers.ElementSelectedEvent.Id && id != SelectionPatternIdentifiers.InvalidatedEvent.Id && id != TextPatternIdentifiers.TextSelectionChangedEvent.Id && id != TextPatternIdentifiers.TextChangedEvent.Id && id != AutomationElementIdentifiers.AsyncContentLoadedEvent.Id && id != AutomationElementIdentifiers.AutomationPropertyChangedEvent.Id && id != AutomationElementIdentifiers.StructureChangedEvent.Id)
			{
				AutomationEvent inputReachedTargetEvent = SynchronizedInputPatternIdentifiers.InputReachedTargetEvent;
				int? num = (inputReachedTargetEvent != null) ? new int?(inputReachedTargetEvent.Id) : null;
				if (!(id == num.GetValueOrDefault() & num != null))
				{
					AutomationEvent inputReachedOtherElementEvent = SynchronizedInputPatternIdentifiers.InputReachedOtherElementEvent;
					num = ((inputReachedOtherElementEvent != null) ? new int?(inputReachedOtherElementEvent.Id) : null);
					if (!(id == num.GetValueOrDefault() & num != null))
					{
						AutomationEvent inputDiscardedEvent = SynchronizedInputPatternIdentifiers.InputDiscardedEvent;
						num = ((inputDiscardedEvent != null) ? new int?(inputDiscardedEvent.Id) : null);
						if (!(id == num.GetValueOrDefault() & num != null))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600512D RID: 20781 RVA: 0x00145008 File Offset: 0x00144408
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool IsKnownNewEvent(int id)
		{
			AutomationEvent liveRegionChangedEvent = AutomationElementIdentifiers.LiveRegionChangedEvent;
			int? num = (liveRegionChangedEvent != null) ? new int?(liveRegionChangedEvent.Id) : null;
			if (!(id == num.GetValueOrDefault() & num != null))
			{
				AutomationEvent notificationEvent = AutomationElementIdentifiers.NotificationEvent;
				num = ((notificationEvent != null) ? new int?(notificationEvent.Id) : null);
				if (!(id == num.GetValueOrDefault() & num != null))
				{
					AutomationEvent activeTextPositionChangedEvent = AutomationElementIdentifiers.ActiveTextPositionChangedEvent;
					num = ((activeTextPositionChangedEvent != null) ? new int?(activeTextPositionChangedEvent.Id) : null);
					if (!(id == num.GetValueOrDefault() & num != null))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600512E RID: 20782 RVA: 0x001450B4 File Offset: 0x001444B4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool IsKnownEvent(int id)
		{
			return EventMap.IsKnownLegacyEvent(id) || (!AccessibilitySwitches.UseNetFx47CompatibleAccessibilityFeatures && EventMap.IsKnownNewEvent(id));
		}

		// Token: 0x0600512F RID: 20783 RVA: 0x001450DC File Offset: 0x001444DC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static AutomationEvent GetRegisteredEventObjectHelper(AutomationEvents eventId)
		{
			AutomationEvent automationEvent;
			switch (eventId)
			{
			case AutomationEvents.ToolTipOpened:
				automationEvent = AutomationElementIdentifiers.ToolTipOpenedEvent;
				break;
			case AutomationEvents.ToolTipClosed:
				automationEvent = AutomationElementIdentifiers.ToolTipClosedEvent;
				break;
			case AutomationEvents.MenuOpened:
				automationEvent = AutomationElementIdentifiers.MenuOpenedEvent;
				break;
			case AutomationEvents.MenuClosed:
				automationEvent = AutomationElementIdentifiers.MenuClosedEvent;
				break;
			case AutomationEvents.AutomationFocusChanged:
				automationEvent = AutomationElementIdentifiers.AutomationFocusChangedEvent;
				break;
			case AutomationEvents.InvokePatternOnInvoked:
				automationEvent = InvokePatternIdentifiers.InvokedEvent;
				break;
			case AutomationEvents.SelectionItemPatternOnElementAddedToSelection:
				automationEvent = SelectionItemPatternIdentifiers.ElementAddedToSelectionEvent;
				break;
			case AutomationEvents.SelectionItemPatternOnElementRemovedFromSelection:
				automationEvent = SelectionItemPatternIdentifiers.ElementRemovedFromSelectionEvent;
				break;
			case AutomationEvents.SelectionItemPatternOnElementSelected:
				automationEvent = SelectionItemPatternIdentifiers.ElementSelectedEvent;
				break;
			case AutomationEvents.SelectionPatternOnInvalidated:
				automationEvent = SelectionPatternIdentifiers.InvalidatedEvent;
				break;
			case AutomationEvents.TextPatternOnTextSelectionChanged:
				automationEvent = TextPatternIdentifiers.TextSelectionChangedEvent;
				break;
			case AutomationEvents.TextPatternOnTextChanged:
				automationEvent = TextPatternIdentifiers.TextChangedEvent;
				break;
			case AutomationEvents.AsyncContentLoaded:
				automationEvent = AutomationElementIdentifiers.AsyncContentLoadedEvent;
				break;
			case AutomationEvents.PropertyChanged:
				automationEvent = AutomationElementIdentifiers.AutomationPropertyChangedEvent;
				break;
			case AutomationEvents.StructureChanged:
				automationEvent = AutomationElementIdentifiers.StructureChangedEvent;
				break;
			case AutomationEvents.InputReachedTarget:
				automationEvent = SynchronizedInputPatternIdentifiers.InputReachedTargetEvent;
				break;
			case AutomationEvents.InputReachedOtherElement:
				automationEvent = SynchronizedInputPatternIdentifiers.InputReachedOtherElementEvent;
				break;
			case AutomationEvents.InputDiscarded:
				automationEvent = SynchronizedInputPatternIdentifiers.InputDiscardedEvent;
				break;
			case AutomationEvents.LiveRegionChanged:
				automationEvent = AutomationElementIdentifiers.LiveRegionChangedEvent;
				break;
			case AutomationEvents.Notification:
				automationEvent = AutomationElementIdentifiers.NotificationEvent;
				break;
			case AutomationEvents.ActiveTextPositionChanged:
				automationEvent = AutomationElementIdentifiers.ActiveTextPositionChangedEvent;
				break;
			default:
				throw new ArgumentException(SR.Get("Automation_InvalidEventId"), "eventId");
			}
			if (automationEvent != null && !EventMap._eventsTable.ContainsKey(automationEvent.Id))
			{
				automationEvent = null;
			}
			return automationEvent;
		}

		// Token: 0x06005130 RID: 20784 RVA: 0x00145240 File Offset: 0x00144640
		internal static void AddEvent(int idEvent)
		{
			if (EventMap.IsKnownEvent(idEvent))
			{
				bool flag = false;
				object @lock = EventMap._lock;
				lock (@lock)
				{
					if (EventMap._eventsTable == null)
					{
						EventMap._eventsTable = new Hashtable(20, 0.1f);
						flag = true;
					}
					if (EventMap._eventsTable.ContainsKey(idEvent))
					{
						EventMap.EventInfo eventInfo = (EventMap.EventInfo)EventMap._eventsTable[idEvent];
						eventInfo.NumberOfListeners++;
					}
					else
					{
						EventMap._eventsTable[idEvent] = new EventMap.EventInfo();
					}
				}
				if (flag)
				{
					EventMap.NotifySources();
				}
			}
		}

		// Token: 0x06005131 RID: 20785 RVA: 0x00145304 File Offset: 0x00144704
		internal static void RemoveEvent(int idEvent)
		{
			object @lock = EventMap._lock;
			lock (@lock)
			{
				if (EventMap._eventsTable != null && EventMap._eventsTable.ContainsKey(idEvent))
				{
					EventMap.EventInfo eventInfo = (EventMap.EventInfo)EventMap._eventsTable[idEvent];
					eventInfo.NumberOfListeners--;
					if (eventInfo.NumberOfListeners <= 0)
					{
						EventMap._eventsTable.Remove(idEvent);
						if (EventMap._eventsTable.Count == 0)
						{
							EventMap._eventsTable = null;
						}
					}
				}
			}
		}

		// Token: 0x06005132 RID: 20786 RVA: 0x001453B4 File Offset: 0x001447B4
		internal static bool HasRegisteredEvent(AutomationEvents eventId)
		{
			object @lock = EventMap._lock;
			lock (@lock)
			{
				if (EventMap._eventsTable != null && EventMap._eventsTable.Count != 0)
				{
					return EventMap.GetRegisteredEventObjectHelper(eventId) != null;
				}
			}
			return false;
		}

		// Token: 0x06005133 RID: 20787 RVA: 0x0014541C File Offset: 0x0014481C
		internal static AutomationEvent GetRegisteredEvent(AutomationEvents eventId)
		{
			object @lock = EventMap._lock;
			lock (@lock)
			{
				if (EventMap._eventsTable != null && EventMap._eventsTable.Count != 0)
				{
					return EventMap.GetRegisteredEventObjectHelper(eventId);
				}
			}
			return null;
		}

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x06005134 RID: 20788 RVA: 0x00145480 File Offset: 0x00144880
		internal static bool HasListeners
		{
			get
			{
				return EventMap._eventsTable != null;
			}
		}

		// Token: 0x06005135 RID: 20789 RVA: 0x00145498 File Offset: 0x00144898
		[SecuritySafeCritical]
		private static void NotifySources()
		{
			foreach (object obj in PresentationSource.CriticalCurrentSources)
			{
				PresentationSource presentationSource = (PresentationSource)obj;
				if (!presentationSource.IsDisposed)
				{
					presentationSource.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new DispatcherOperationCallback(EventMap.NotifySource), new object[]
					{
						presentationSource
					});
				}
			}
		}

		// Token: 0x06005136 RID: 20790 RVA: 0x00145524 File Offset: 0x00144924
		[SecuritySafeCritical]
		private static object NotifySource(object args)
		{
			object[] array = (object[])args;
			PresentationSource presentationSource = array[0] as PresentationSource;
			if (presentationSource != null && !presentationSource.IsDisposed)
			{
				bool flag = SecurityManager.CurrentThreadRequiresSecurityContextCapture();
				if (flag)
				{
					new UIPermission(UIPermissionWindow.AllWindows).Assert();
				}
				presentationSource.RootVisual = presentationSource.RootVisual;
				if (flag)
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			return null;
		}

		// Token: 0x040024EB RID: 9451
		private static Hashtable _eventsTable;

		// Token: 0x040024EC RID: 9452
		private static readonly object _lock = new object();

		// Token: 0x020009F9 RID: 2553
		private class EventInfo
		{
			// Token: 0x06005BDE RID: 23518 RVA: 0x001712A4 File Offset: 0x001706A4
			internal EventInfo()
			{
				this.NumberOfListeners = 1;
			}

			// Token: 0x04002F16 RID: 12054
			internal int NumberOfListeners;
		}
	}
}
