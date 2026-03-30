using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;
using MS.Internal.PresentationCore;

namespace MS.Internal.Automation
{
	// Token: 0x0200079D RID: 1949
	internal class TextProviderWrapper : MarshalByRefObject, ITextProvider
	{
		// Token: 0x060051D5 RID: 20949 RVA: 0x00146D60 File Offset: 0x00146160
		private TextProviderWrapper(AutomationPeer peer, ITextProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x060051D6 RID: 20950 RVA: 0x00146D84 File Offset: 0x00146184
		public ITextRangeProvider[] GetSelection()
		{
			return (ITextRangeProvider[])ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetSelection), null);
		}

		// Token: 0x060051D7 RID: 20951 RVA: 0x00146DB0 File Offset: 0x001461B0
		public ITextRangeProvider[] GetVisibleRanges()
		{
			return (ITextRangeProvider[])ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetVisibleRanges), null);
		}

		// Token: 0x060051D8 RID: 20952 RVA: 0x00146DDC File Offset: 0x001461DC
		public ITextRangeProvider RangeFromChild(IRawElementProviderSimple childElement)
		{
			if (!(childElement is ElementProxy))
			{
				throw new ArgumentException(SR.Get("TextProvider_InvalidChild", new object[]
				{
					"childElement"
				}));
			}
			return (ITextRangeProvider)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.RangeFromChild), childElement);
		}

		// Token: 0x060051D9 RID: 20953 RVA: 0x00146E2C File Offset: 0x0014622C
		public ITextRangeProvider RangeFromPoint(Point screenLocation)
		{
			return (ITextRangeProvider)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.RangeFromPoint), screenLocation);
		}

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x060051DA RID: 20954 RVA: 0x00146E5C File Offset: 0x0014625C
		public ITextRangeProvider DocumentRange
		{
			get
			{
				return (ITextRangeProvider)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetDocumentRange), null);
			}
		}

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x060051DB RID: 20955 RVA: 0x00146E88 File Offset: 0x00146288
		public SupportedTextSelection SupportedTextSelection
		{
			get
			{
				return (SupportedTextSelection)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetSupportedTextSelection), null);
			}
		}

		// Token: 0x060051DC RID: 20956 RVA: 0x00146EB4 File Offset: 0x001462B4
		internal static object Wrap(AutomationPeer peer, object iface)
		{
			return new TextProviderWrapper(peer, (ITextProvider)iface);
		}

		// Token: 0x060051DD RID: 20957 RVA: 0x00146ED0 File Offset: 0x001462D0
		private object GetSelection(object unused)
		{
			return TextRangeProviderWrapper.WrapArgument(this._iface.GetSelection(), this._peer);
		}

		// Token: 0x060051DE RID: 20958 RVA: 0x00146EF4 File Offset: 0x001462F4
		private object GetVisibleRanges(object unused)
		{
			return TextRangeProviderWrapper.WrapArgument(this._iface.GetVisibleRanges(), this._peer);
		}

		// Token: 0x060051DF RID: 20959 RVA: 0x00146F18 File Offset: 0x00146318
		private object RangeFromChild(object arg)
		{
			IRawElementProviderSimple childElement = (IRawElementProviderSimple)arg;
			return TextRangeProviderWrapper.WrapArgument(this._iface.RangeFromChild(childElement), this._peer);
		}

		// Token: 0x060051E0 RID: 20960 RVA: 0x00146F44 File Offset: 0x00146344
		private object RangeFromPoint(object arg)
		{
			Point screenLocation = (Point)arg;
			return TextRangeProviderWrapper.WrapArgument(this._iface.RangeFromPoint(screenLocation), this._peer);
		}

		// Token: 0x060051E1 RID: 20961 RVA: 0x00146F70 File Offset: 0x00146370
		private object GetDocumentRange(object unused)
		{
			return TextRangeProviderWrapper.WrapArgument(this._iface.DocumentRange, this._peer);
		}

		// Token: 0x060051E2 RID: 20962 RVA: 0x00146F94 File Offset: 0x00146394
		private object GetSupportedTextSelection(object unused)
		{
			return this._iface.SupportedTextSelection;
		}

		// Token: 0x0400250C RID: 9484
		private AutomationPeer _peer;

		// Token: 0x0400250D RID: 9485
		private ITextProvider _iface;
	}
}
