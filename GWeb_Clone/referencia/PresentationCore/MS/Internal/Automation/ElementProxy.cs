using System;
using System.Security;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Input;
using System.Windows.Threading;

namespace MS.Internal.Automation
{
	// Token: 0x0200078A RID: 1930
	internal class ElementProxy : IRawElementProviderFragmentRoot, IRawElementProviderFragment, IRawElementProviderSimple, IRawElementProviderAdviseEvents
	{
		// Token: 0x060050FF RID: 20735 RVA: 0x001444B4 File Offset: 0x001438B4
		private ElementProxy(AutomationPeer peer)
		{
			if (ElementProxy.AutomationInteropReferenceType == ElementProxy.ReferenceType.Weak && (peer is UIElementAutomationPeer || peer is ContentElementAutomationPeer || peer is UIElement3DAutomationPeer))
			{
				this._peer = new WeakReference(peer);
				return;
			}
			this._peer = peer;
		}

		// Token: 0x06005100 RID: 20736 RVA: 0x001444FC File Offset: 0x001438FC
		public object GetPatternProvider(int pattern)
		{
			AutomationPeer peer = this.Peer;
			if (peer == null)
			{
				throw new ElementNotAvailableException();
			}
			return ElementUtil.Invoke(peer, new DispatcherOperationCallback(this.InContextGetPatternProvider), pattern);
		}

		// Token: 0x06005101 RID: 20737 RVA: 0x00144534 File Offset: 0x00143934
		public object GetPropertyValue(int property)
		{
			AutomationPeer peer = this.Peer;
			if (peer == null)
			{
				throw new ElementNotAvailableException();
			}
			return ElementUtil.Invoke(peer, new DispatcherOperationCallback(this.InContextGetPropertyValue), property);
		}

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x06005102 RID: 20738 RVA: 0x0014456C File Offset: 0x0014396C
		public ProviderOptions ProviderOptions
		{
			get
			{
				AutomationPeer peer = this.Peer;
				if (peer == null)
				{
					return ProviderOptions.ServerSideProvider;
				}
				return (ProviderOptions)ElementUtil.Invoke(peer, new DispatcherOperationCallback(this.InContextGetProviderOptions), null);
			}
		}

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x06005103 RID: 20739 RVA: 0x001445A0 File Offset: 0x001439A0
		public IRawElementProviderSimple HostRawElementProvider
		{
			get
			{
				IRawElementProviderSimple result = null;
				AutomationPeer peer = this.Peer;
				if (peer == null)
				{
					return null;
				}
				HostedWindowWrapper hostedWindowWrapper = (HostedWindowWrapper)ElementUtil.Invoke(peer, new DispatcherOperationCallback(this.InContextGetHostRawElementProvider), null);
				if (hostedWindowWrapper != null)
				{
					result = this.GetHostHelper(hostedWindowWrapper);
				}
				return result;
			}
		}

		// Token: 0x06005104 RID: 20740 RVA: 0x001445E4 File Offset: 0x001439E4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private IRawElementProviderSimple GetHostHelper(HostedWindowWrapper hwndWrapper)
		{
			return AutomationInteropProvider.HostProviderFromHandle(hwndWrapper.Handle);
		}

		// Token: 0x06005105 RID: 20741 RVA: 0x001445FC File Offset: 0x001439FC
		public IRawElementProviderFragment Navigate(NavigateDirection direction)
		{
			AutomationPeer peer = this.Peer;
			if (peer == null)
			{
				return null;
			}
			return (IRawElementProviderFragment)ElementUtil.Invoke(peer, new DispatcherOperationCallback(this.InContextNavigate), direction);
		}

		// Token: 0x06005106 RID: 20742 RVA: 0x00144634 File Offset: 0x00143A34
		public int[] GetRuntimeId()
		{
			AutomationPeer peer = this.Peer;
			if (peer == null)
			{
				throw new ElementNotAvailableException();
			}
			return (int[])ElementUtil.Invoke(peer, new DispatcherOperationCallback(this.InContextGetRuntimeId), null);
		}

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x06005107 RID: 20743 RVA: 0x0014466C File Offset: 0x00143A6C
		public Rect BoundingRectangle
		{
			get
			{
				AutomationPeer peer = this.Peer;
				if (peer == null)
				{
					throw new ElementNotAvailableException();
				}
				return (Rect)ElementUtil.Invoke(peer, new DispatcherOperationCallback(this.InContextBoundingRectangle), null);
			}
		}

		// Token: 0x06005108 RID: 20744 RVA: 0x001446A4 File Offset: 0x00143AA4
		public IRawElementProviderSimple[] GetEmbeddedFragmentRoots()
		{
			return null;
		}

		// Token: 0x06005109 RID: 20745 RVA: 0x001446B4 File Offset: 0x00143AB4
		public void SetFocus()
		{
			AutomationPeer peer = this.Peer;
			if (peer == null)
			{
				throw new ElementNotAvailableException();
			}
			ElementUtil.Invoke(peer, new DispatcherOperationCallback(this.InContextSetFocus), null);
		}

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x0600510A RID: 20746 RVA: 0x001446E8 File Offset: 0x00143AE8
		public IRawElementProviderFragmentRoot FragmentRoot
		{
			get
			{
				AutomationPeer peer = this.Peer;
				if (peer == null)
				{
					return null;
				}
				return (IRawElementProviderFragmentRoot)ElementUtil.Invoke(peer, new DispatcherOperationCallback(this.InContextFragmentRoot), null);
			}
		}

		// Token: 0x0600510B RID: 20747 RVA: 0x0014471C File Offset: 0x00143B1C
		public IRawElementProviderFragment ElementProviderFromPoint(double x, double y)
		{
			AutomationPeer peer = this.Peer;
			if (peer == null)
			{
				return null;
			}
			return (IRawElementProviderFragment)ElementUtil.Invoke(peer, new DispatcherOperationCallback(this.InContextElementProviderFromPoint), new Point(x, y));
		}

		// Token: 0x0600510C RID: 20748 RVA: 0x00144758 File Offset: 0x00143B58
		public IRawElementProviderFragment GetFocus()
		{
			AutomationPeer peer = this.Peer;
			if (peer == null)
			{
				return null;
			}
			return (IRawElementProviderFragment)ElementUtil.Invoke(peer, new DispatcherOperationCallback(this.InContextGetFocus), null);
		}

		// Token: 0x0600510D RID: 20749 RVA: 0x0014478C File Offset: 0x00143B8C
		public void AdviseEventAdded(int eventID, int[] properties)
		{
			EventMap.AddEvent(eventID);
		}

		// Token: 0x0600510E RID: 20750 RVA: 0x001447A0 File Offset: 0x00143BA0
		public void AdviseEventRemoved(int eventID, int[] properties)
		{
			EventMap.RemoveEvent(eventID);
		}

		// Token: 0x0600510F RID: 20751 RVA: 0x001447B4 File Offset: 0x00143BB4
		internal static ElementProxy StaticWrap(AutomationPeer peer, AutomationPeer referencePeer)
		{
			ElementProxy elementProxy = null;
			if (peer != null)
			{
				peer = peer.ValidateConnected(referencePeer);
				if (peer != null)
				{
					if (peer.ElementProxyWeakReference != null)
					{
						elementProxy = (peer.ElementProxyWeakReference.Target as ElementProxy);
					}
					if (elementProxy == null)
					{
						elementProxy = new ElementProxy(peer);
						peer.ElementProxyWeakReference = new WeakReference(elementProxy);
					}
					if (elementProxy != null && peer.IsDataItemAutomationPeer())
					{
						peer.AddToParentProxyWeakRefCache();
					}
				}
			}
			return elementProxy;
		}

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x06005110 RID: 20752 RVA: 0x00144814 File Offset: 0x00143C14
		internal AutomationPeer Peer
		{
			get
			{
				if (this._peer is WeakReference)
				{
					return (AutomationPeer)((WeakReference)this._peer).Target;
				}
				return (AutomationPeer)this._peer;
			}
		}

		// Token: 0x06005111 RID: 20753 RVA: 0x00144854 File Offset: 0x00143C54
		private object InContextElementProviderFromPoint(object arg)
		{
			Point point = (Point)arg;
			AutomationPeer peer = this.Peer;
			if (peer == null)
			{
				return null;
			}
			AutomationPeer peerFromPoint = peer.GetPeerFromPoint(point);
			return ElementProxy.StaticWrap(peerFromPoint, peer);
		}

		// Token: 0x06005112 RID: 20754 RVA: 0x00144884 File Offset: 0x00143C84
		private object InContextGetFocus(object unused)
		{
			AutomationPeer peer = this.Peer;
			if (peer == null)
			{
				return null;
			}
			AutomationPeer peer2 = AutomationPeer.AutomationPeerFromInputElement(Keyboard.FocusedElement);
			return ElementProxy.StaticWrap(peer2, peer);
		}

		// Token: 0x06005113 RID: 20755 RVA: 0x001448B0 File Offset: 0x00143CB0
		private object InContextGetPatternProvider(object arg)
		{
			AutomationPeer peer = this.Peer;
			if (peer == null)
			{
				throw new ElementNotAvailableException();
			}
			return peer.GetWrappedPattern((int)arg);
		}

		// Token: 0x06005114 RID: 20756 RVA: 0x001448DC File Offset: 0x00143CDC
		private object InContextNavigate(object arg)
		{
			NavigateDirection navigateDirection = (NavigateDirection)arg;
			AutomationPeer peer = this.Peer;
			if (peer == null)
			{
				return null;
			}
			AutomationPeer peer2;
			switch (navigateDirection)
			{
			case NavigateDirection.Parent:
				peer2 = peer.GetParent();
				break;
			case NavigateDirection.NextSibling:
				peer2 = peer.GetNextSibling();
				break;
			case NavigateDirection.PreviousSibling:
				peer2 = peer.GetPreviousSibling();
				break;
			case NavigateDirection.FirstChild:
				if (peer.IsInteropPeer)
				{
					return peer.GetInteropChild();
				}
				peer2 = peer.GetFirstChild();
				break;
			case NavigateDirection.LastChild:
				if (peer.IsInteropPeer)
				{
					return peer.GetInteropChild();
				}
				peer2 = peer.GetLastChild();
				break;
			default:
				peer2 = null;
				break;
			}
			return ElementProxy.StaticWrap(peer2, peer);
		}

		// Token: 0x06005115 RID: 20757 RVA: 0x0014496C File Offset: 0x00143D6C
		private object InContextGetProviderOptions(object arg)
		{
			ProviderOptions providerOptions = ProviderOptions.ServerSideProvider;
			AutomationPeer peer = this.Peer;
			if (peer == null)
			{
				return providerOptions;
			}
			if (peer.IsHwndHost)
			{
				providerOptions |= ProviderOptions.OverrideProvider;
			}
			return providerOptions;
		}

		// Token: 0x06005116 RID: 20758 RVA: 0x001449A0 File Offset: 0x00143DA0
		private object InContextGetPropertyValue(object arg)
		{
			AutomationPeer peer = this.Peer;
			if (peer == null)
			{
				throw new ElementNotAvailableException();
			}
			return peer.GetPropertyValue((int)arg);
		}

		// Token: 0x06005117 RID: 20759 RVA: 0x001449CC File Offset: 0x00143DCC
		private object InContextGetHostRawElementProvider(object unused)
		{
			AutomationPeer peer = this.Peer;
			if (peer == null)
			{
				return null;
			}
			return peer.GetHostRawElementProvider();
		}

		// Token: 0x06005118 RID: 20760 RVA: 0x001449EC File Offset: 0x00143DEC
		private object InContextGetRuntimeId(object unused)
		{
			AutomationPeer peer = this.Peer;
			if (peer == null)
			{
				throw new ElementNotAvailableException();
			}
			return peer.GetRuntimeId();
		}

		// Token: 0x06005119 RID: 20761 RVA: 0x00144A10 File Offset: 0x00143E10
		private object InContextBoundingRectangle(object unused)
		{
			AutomationPeer peer = this.Peer;
			if (peer == null)
			{
				throw new ElementNotAvailableException();
			}
			return peer.GetBoundingRectangle();
		}

		// Token: 0x0600511A RID: 20762 RVA: 0x00144A38 File Offset: 0x00143E38
		private object InContextSetFocus(object unused)
		{
			AutomationPeer peer = this.Peer;
			if (peer == null)
			{
				throw new ElementNotAvailableException();
			}
			peer.SetFocus();
			return null;
		}

		// Token: 0x0600511B RID: 20763 RVA: 0x00144A5C File Offset: 0x00143E5C
		private object InContextFragmentRoot(object unused)
		{
			AutomationPeer peer = this.Peer;
			AutomationPeer automationPeer = peer;
			if (automationPeer == null)
			{
				return null;
			}
			for (;;)
			{
				AutomationPeer parent = automationPeer.GetParent();
				if (parent == null)
				{
					break;
				}
				automationPeer = parent;
			}
			return ElementProxy.StaticWrap(automationPeer, peer);
		}

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x0600511C RID: 20764 RVA: 0x00144A8C File Offset: 0x00143E8C
		internal static ElementProxy.ReferenceType AutomationInteropReferenceType
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				if (ElementProxy._shouldCheckInTheRegistry)
				{
					if (RegistryKeys.ReadLocalMachineBool("Software\\Microsoft\\.NETFramework\\Windows Presentation Foundation\\Features", "AutomationWeakReferenceDisallow"))
					{
						ElementProxy._automationInteropReferenceType = ElementProxy.ReferenceType.Strong;
					}
					ElementProxy._shouldCheckInTheRegistry = false;
				}
				return ElementProxy._automationInteropReferenceType;
			}
		}

		// Token: 0x040024E8 RID: 9448
		private static ElementProxy.ReferenceType _automationInteropReferenceType = ElementProxy.ReferenceType.Weak;

		// Token: 0x040024E9 RID: 9449
		private static bool _shouldCheckInTheRegistry = true;

		// Token: 0x040024EA RID: 9450
		private readonly object _peer;

		// Token: 0x020009F7 RID: 2551
		internal enum ReferenceType
		{
			// Token: 0x04002F10 RID: 12048
			Strong,
			// Token: 0x04002F11 RID: 12049
			Weak
		}
	}
}
