using System;
using System.Security;
using System.Windows.Input;
using MS.Internal.PresentationCore;

namespace System.Windows.Interop
{
	// Token: 0x0200032D RID: 813
	internal class HwndSourceKeyboardInputSite : IKeyboardInputSite
	{
		// Token: 0x06001BBF RID: 7103 RVA: 0x00070198 File Offset: 0x0006F598
		[SecurityCritical]
		public HwndSourceKeyboardInputSite(HwndSource source, IKeyboardInputSink sink)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (sink == null)
			{
				throw new ArgumentNullException("sink");
			}
			if (!(sink is UIElement))
			{
				throw new ArgumentException(SR.Get("KeyboardSinkMustBeAnElement"), "sink");
			}
			this._source = source;
			this._sink = sink;
			this._sink.KeyboardInputSite = this;
			this._sinkElement = (sink as UIElement);
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x0007020C File Offset: 0x0006F60C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		void IKeyboardInputSite.Unregister()
		{
			this.CriticalUnregister();
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x00070220 File Offset: 0x0006F620
		[SecurityCritical]
		internal void CriticalUnregister()
		{
			if (this._source != null && this._sink != null)
			{
				this._source.CriticalUnregisterKeyboardInputSink(this);
				this._sink.KeyboardInputSite = null;
			}
			this._source = null;
			this._sink = null;
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x00070264 File Offset: 0x0006F664
		IKeyboardInputSink IKeyboardInputSite.Sink
		{
			get
			{
				return this._sink;
			}
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x00070278 File Offset: 0x0006F678
		bool IKeyboardInputSite.OnNoMoreTabStops(TraversalRequest request)
		{
			bool result = false;
			if (this._sinkElement != null)
			{
				result = this._sinkElement.MoveFocus(request);
			}
			return result;
		}

		// Token: 0x04000ED4 RID: 3796
		private HwndSource _source;

		// Token: 0x04000ED5 RID: 3797
		private IKeyboardInputSink _sink;

		// Token: 0x04000ED6 RID: 3798
		private UIElement _sinkElement;
	}
}
