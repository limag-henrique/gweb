using System;
using System.Security;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using MS.Internal.PresentationCore;

namespace MS.Internal.Automation
{
	// Token: 0x02000791 RID: 1937
	[FriendAccessAllowed]
	internal class InteropAutomationProvider : IRawElementProviderFragmentRoot, IRawElementProviderFragment, IRawElementProviderSimple
	{
		// Token: 0x06005159 RID: 20825 RVA: 0x00145BAC File Offset: 0x00144FAC
		internal InteropAutomationProvider(HostedWindowWrapper wrapper, AutomationPeer parent)
		{
			if (wrapper == null)
			{
				throw new ArgumentNullException("wrapper");
			}
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			this._wrapper = wrapper;
			this._parent = parent;
		}

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x0600515A RID: 20826 RVA: 0x00145BEC File Offset: 0x00144FEC
		ProviderOptions IRawElementProviderSimple.ProviderOptions
		{
			get
			{
				return ProviderOptions.ServerSideProvider | ProviderOptions.OverrideProvider;
			}
		}

		// Token: 0x0600515B RID: 20827 RVA: 0x00145BFC File Offset: 0x00144FFC
		object IRawElementProviderSimple.GetPatternProvider(int patternId)
		{
			return null;
		}

		// Token: 0x0600515C RID: 20828 RVA: 0x00145C0C File Offset: 0x0014500C
		object IRawElementProviderSimple.GetPropertyValue(int propertyId)
		{
			return null;
		}

		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x0600515D RID: 20829 RVA: 0x00145C1C File Offset: 0x0014501C
		IRawElementProviderSimple IRawElementProviderSimple.HostRawElementProvider
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				return AutomationInteropProvider.HostProviderFromHandle(this._wrapper.Handle);
			}
		}

		// Token: 0x0600515E RID: 20830 RVA: 0x00145C3C File Offset: 0x0014503C
		IRawElementProviderFragment IRawElementProviderFragment.Navigate(NavigateDirection direction)
		{
			if (direction == NavigateDirection.Parent)
			{
				return (IRawElementProviderFragment)this._parent.ProviderFromPeer(this._parent);
			}
			return null;
		}

		// Token: 0x0600515F RID: 20831 RVA: 0x00145C64 File Offset: 0x00145064
		int[] IRawElementProviderFragment.GetRuntimeId()
		{
			return null;
		}

		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x06005160 RID: 20832 RVA: 0x00145C74 File Offset: 0x00145074
		Rect IRawElementProviderFragment.BoundingRectangle
		{
			get
			{
				return Rect.Empty;
			}
		}

		// Token: 0x06005161 RID: 20833 RVA: 0x00145C88 File Offset: 0x00145088
		IRawElementProviderSimple[] IRawElementProviderFragment.GetEmbeddedFragmentRoots()
		{
			return null;
		}

		// Token: 0x06005162 RID: 20834 RVA: 0x00145C98 File Offset: 0x00145098
		void IRawElementProviderFragment.SetFocus()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x06005163 RID: 20835 RVA: 0x00145CAC File Offset: 0x001450AC
		IRawElementProviderFragmentRoot IRawElementProviderFragment.FragmentRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005164 RID: 20836 RVA: 0x00145CBC File Offset: 0x001450BC
		IRawElementProviderFragment IRawElementProviderFragmentRoot.ElementProviderFromPoint(double x, double y)
		{
			return null;
		}

		// Token: 0x06005165 RID: 20837 RVA: 0x00145CCC File Offset: 0x001450CC
		IRawElementProviderFragment IRawElementProviderFragmentRoot.GetFocus()
		{
			return null;
		}

		// Token: 0x040024F5 RID: 9461
		private HostedWindowWrapper _wrapper;

		// Token: 0x040024F6 RID: 9462
		private AutomationPeer _parent;
	}
}
