using System;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Automation.Text;
using System.Windows.Threading;
using MS.Internal.PresentationCore;

namespace MS.Internal.Automation
{
	// Token: 0x0200079E RID: 1950
	internal class TextRangeProviderWrapper : MarshalByRefObject, ITextRangeProvider
	{
		// Token: 0x060051E3 RID: 20963 RVA: 0x00146FB4 File Offset: 0x001463B4
		internal TextRangeProviderWrapper(AutomationPeer peer, ITextRangeProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x060051E4 RID: 20964 RVA: 0x00146FD8 File Offset: 0x001463D8
		public ITextRangeProvider Clone()
		{
			return (ITextRangeProvider)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.Clone), null);
		}

		// Token: 0x060051E5 RID: 20965 RVA: 0x00147004 File Offset: 0x00146404
		public bool Compare(ITextRangeProvider range)
		{
			if (!(range is TextRangeProviderWrapper))
			{
				throw new ArgumentException(SR.Get("TextRangeProvider_InvalidRangeProvider", new object[]
				{
					"range"
				}));
			}
			return (bool)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.Compare), range);
		}

		// Token: 0x060051E6 RID: 20966 RVA: 0x00147054 File Offset: 0x00146454
		public int CompareEndpoints(TextPatternRangeEndpoint endpoint, ITextRangeProvider targetRange, TextPatternRangeEndpoint targetEndpoint)
		{
			if (!(targetRange is TextRangeProviderWrapper))
			{
				throw new ArgumentException(SR.Get("TextRangeProvider_InvalidRangeProvider", new object[]
				{
					"targetRange"
				}));
			}
			object[] arg = new object[]
			{
				endpoint,
				targetRange,
				targetEndpoint
			};
			return (int)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.CompareEndpoints), arg);
		}

		// Token: 0x060051E7 RID: 20967 RVA: 0x001470C4 File Offset: 0x001464C4
		public void ExpandToEnclosingUnit(TextUnit unit)
		{
			object[] arg = new object[]
			{
				unit
			};
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.ExpandToEnclosingUnit), arg);
		}

		// Token: 0x060051E8 RID: 20968 RVA: 0x001470FC File Offset: 0x001464FC
		public ITextRangeProvider FindAttribute(int attribute, object val, bool backward)
		{
			object[] arg = new object[]
			{
				attribute,
				val,
				backward
			};
			return (ITextRangeProvider)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.FindAttribute), arg);
		}

		// Token: 0x060051E9 RID: 20969 RVA: 0x00147144 File Offset: 0x00146544
		public ITextRangeProvider FindText(string text, bool backward, bool ignoreCase)
		{
			object[] arg = new object[]
			{
				text,
				backward,
				ignoreCase
			};
			return (ITextRangeProvider)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.FindText), arg);
		}

		// Token: 0x060051EA RID: 20970 RVA: 0x0014718C File Offset: 0x0014658C
		public object GetAttributeValue(int attribute)
		{
			object[] arg = new object[]
			{
				attribute
			};
			return ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetAttributeValue), arg);
		}

		// Token: 0x060051EB RID: 20971 RVA: 0x001471C4 File Offset: 0x001465C4
		public double[] GetBoundingRectangles()
		{
			return (double[])ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetBoundingRectangles), null);
		}

		// Token: 0x060051EC RID: 20972 RVA: 0x001471F0 File Offset: 0x001465F0
		public IRawElementProviderSimple GetEnclosingElement()
		{
			return (IRawElementProviderSimple)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetEnclosingElement), null);
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x0014721C File Offset: 0x0014661C
		public string GetText(int maxLength)
		{
			object[] arg = new object[]
			{
				maxLength
			};
			return (string)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetText), arg);
		}

		// Token: 0x060051EE RID: 20974 RVA: 0x00147258 File Offset: 0x00146658
		public int Move(TextUnit unit, int count)
		{
			object[] arg = new object[]
			{
				unit,
				count
			};
			return (int)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.Move), arg);
		}

		// Token: 0x060051EF RID: 20975 RVA: 0x0014729C File Offset: 0x0014669C
		public int MoveEndpointByUnit(TextPatternRangeEndpoint endpoint, TextUnit unit, int count)
		{
			object[] arg = new object[]
			{
				endpoint,
				unit,
				count
			};
			return (int)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.MoveEndpointByUnit), arg);
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x001472E8 File Offset: 0x001466E8
		public void MoveEndpointByRange(TextPatternRangeEndpoint endpoint, ITextRangeProvider targetRange, TextPatternRangeEndpoint targetEndpoint)
		{
			if (!(targetRange is TextRangeProviderWrapper))
			{
				throw new ArgumentException(SR.Get("TextRangeProvider_InvalidRangeProvider", new object[]
				{
					"targetRange"
				}));
			}
			object[] arg = new object[]
			{
				endpoint,
				targetRange,
				targetEndpoint
			};
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.MoveEndpointByRange), arg);
		}

		// Token: 0x060051F1 RID: 20977 RVA: 0x00147354 File Offset: 0x00146754
		public void Select()
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.Select), null);
		}

		// Token: 0x060051F2 RID: 20978 RVA: 0x0014737C File Offset: 0x0014677C
		public void AddToSelection()
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.AddToSelection), null);
		}

		// Token: 0x060051F3 RID: 20979 RVA: 0x001473A4 File Offset: 0x001467A4
		public void RemoveFromSelection()
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.RemoveFromSelection), null);
		}

		// Token: 0x060051F4 RID: 20980 RVA: 0x001473CC File Offset: 0x001467CC
		public void ScrollIntoView(bool alignToTop)
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.ScrollIntoView), alignToTop);
		}

		// Token: 0x060051F5 RID: 20981 RVA: 0x001473F8 File Offset: 0x001467F8
		public IRawElementProviderSimple[] GetChildren()
		{
			return (IRawElementProviderSimple[])ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetChildren), null);
		}

		// Token: 0x060051F6 RID: 20982 RVA: 0x00147424 File Offset: 0x00146824
		internal static ITextRangeProvider WrapArgument(ITextRangeProvider argument, AutomationPeer peer)
		{
			if (argument == null)
			{
				return null;
			}
			if (argument is TextRangeProviderWrapper)
			{
				return argument;
			}
			return new TextRangeProviderWrapper(peer, argument);
		}

		// Token: 0x060051F7 RID: 20983 RVA: 0x00147448 File Offset: 0x00146848
		internal static ITextRangeProvider[] WrapArgument(ITextRangeProvider[] argument, AutomationPeer peer)
		{
			if (argument == null)
			{
				return null;
			}
			if (argument is TextRangeProviderWrapper[])
			{
				return argument;
			}
			ITextRangeProvider[] array = new ITextRangeProvider[argument.Length];
			for (int i = 0; i < argument.Length; i++)
			{
				array[i] = TextRangeProviderWrapper.WrapArgument(argument[i], peer);
			}
			return array;
		}

		// Token: 0x060051F8 RID: 20984 RVA: 0x00147488 File Offset: 0x00146888
		internal static ITextRangeProvider UnwrapArgument(ITextRangeProvider argument)
		{
			if (argument is TextRangeProviderWrapper)
			{
				return ((TextRangeProviderWrapper)argument)._iface;
			}
			return argument;
		}

		// Token: 0x060051F9 RID: 20985 RVA: 0x001474AC File Offset: 0x001468AC
		private object Clone(object unused)
		{
			return TextRangeProviderWrapper.WrapArgument(this._iface.Clone(), this._peer);
		}

		// Token: 0x060051FA RID: 20986 RVA: 0x001474D0 File Offset: 0x001468D0
		private object Compare(object arg)
		{
			ITextRangeProvider argument = (ITextRangeProvider)arg;
			return this._iface.Compare(TextRangeProviderWrapper.UnwrapArgument(argument));
		}

		// Token: 0x060051FB RID: 20987 RVA: 0x001474FC File Offset: 0x001468FC
		private object CompareEndpoints(object arg)
		{
			object[] array = (object[])arg;
			TextPatternRangeEndpoint endpoint = (TextPatternRangeEndpoint)array[0];
			ITextRangeProvider argument = (ITextRangeProvider)array[1];
			TextPatternRangeEndpoint targetEndpoint = (TextPatternRangeEndpoint)array[2];
			return this._iface.CompareEndpoints(endpoint, TextRangeProviderWrapper.UnwrapArgument(argument), targetEndpoint);
		}

		// Token: 0x060051FC RID: 20988 RVA: 0x00147544 File Offset: 0x00146944
		private object ExpandToEnclosingUnit(object arg)
		{
			object[] array = (object[])arg;
			TextUnit unit = (TextUnit)array[0];
			this._iface.ExpandToEnclosingUnit(unit);
			return null;
		}

		// Token: 0x060051FD RID: 20989 RVA: 0x00147570 File Offset: 0x00146970
		private object FindAttribute(object arg)
		{
			object[] array = (object[])arg;
			int attribute = (int)array[0];
			object value = array[1];
			bool backward = (bool)array[2];
			return TextRangeProviderWrapper.WrapArgument(this._iface.FindAttribute(attribute, value, backward), this._peer);
		}

		// Token: 0x060051FE RID: 20990 RVA: 0x001475B4 File Offset: 0x001469B4
		private object FindText(object arg)
		{
			object[] array = (object[])arg;
			string text = (string)array[0];
			bool backward = (bool)array[1];
			bool ignoreCase = (bool)array[2];
			return TextRangeProviderWrapper.WrapArgument(this._iface.FindText(text, backward, ignoreCase), this._peer);
		}

		// Token: 0x060051FF RID: 20991 RVA: 0x001475FC File Offset: 0x001469FC
		private object GetAttributeValue(object arg)
		{
			object[] array = (object[])arg;
			int attribute = (int)array[0];
			return this._iface.GetAttributeValue(attribute);
		}

		// Token: 0x06005200 RID: 20992 RVA: 0x00147628 File Offset: 0x00146A28
		private object GetBoundingRectangles(object unused)
		{
			return this._iface.GetBoundingRectangles();
		}

		// Token: 0x06005201 RID: 20993 RVA: 0x00147640 File Offset: 0x00146A40
		private object GetEnclosingElement(object unused)
		{
			return this._iface.GetEnclosingElement();
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x00147658 File Offset: 0x00146A58
		private object GetText(object arg)
		{
			object[] array = (object[])arg;
			int maxLength = (int)array[0];
			return this._iface.GetText(maxLength);
		}

		// Token: 0x06005203 RID: 20995 RVA: 0x00147684 File Offset: 0x00146A84
		private object Move(object arg)
		{
			object[] array = (object[])arg;
			TextUnit unit = (TextUnit)array[0];
			int count = (int)array[1];
			return this._iface.Move(unit, count);
		}

		// Token: 0x06005204 RID: 20996 RVA: 0x001476BC File Offset: 0x00146ABC
		private object MoveEndpointByUnit(object arg)
		{
			object[] array = (object[])arg;
			TextPatternRangeEndpoint endpoint = (TextPatternRangeEndpoint)array[0];
			TextUnit unit = (TextUnit)array[1];
			int count = (int)array[2];
			return this._iface.MoveEndpointByUnit(endpoint, unit, count);
		}

		// Token: 0x06005205 RID: 20997 RVA: 0x00147700 File Offset: 0x00146B00
		private object MoveEndpointByRange(object arg)
		{
			object[] array = (object[])arg;
			TextPatternRangeEndpoint endpoint = (TextPatternRangeEndpoint)array[0];
			ITextRangeProvider argument = (ITextRangeProvider)array[1];
			TextPatternRangeEndpoint targetEndpoint = (TextPatternRangeEndpoint)array[2];
			this._iface.MoveEndpointByRange(endpoint, TextRangeProviderWrapper.UnwrapArgument(argument), targetEndpoint);
			return null;
		}

		// Token: 0x06005206 RID: 20998 RVA: 0x00147744 File Offset: 0x00146B44
		private object Select(object unused)
		{
			this._iface.Select();
			return null;
		}

		// Token: 0x06005207 RID: 20999 RVA: 0x00147760 File Offset: 0x00146B60
		private object AddToSelection(object unused)
		{
			this._iface.AddToSelection();
			return null;
		}

		// Token: 0x06005208 RID: 21000 RVA: 0x0014777C File Offset: 0x00146B7C
		private object RemoveFromSelection(object unused)
		{
			this._iface.RemoveFromSelection();
			return null;
		}

		// Token: 0x06005209 RID: 21001 RVA: 0x00147798 File Offset: 0x00146B98
		private object ScrollIntoView(object arg)
		{
			bool alignToTop = (bool)arg;
			this._iface.ScrollIntoView(alignToTop);
			return null;
		}

		// Token: 0x0600520A RID: 21002 RVA: 0x001477BC File Offset: 0x00146BBC
		private object GetChildren(object unused)
		{
			return this._iface.GetChildren();
		}

		// Token: 0x0400250E RID: 9486
		private AutomationPeer _peer;

		// Token: 0x0400250F RID: 9487
		private ITextRangeProvider _iface;
	}
}
