using System;
using System.Windows.Automation.Peers;
using System.Windows.Media;
using System.Windows.Threading;
using MS.Internal.FontCache;
using MS.Internal.PresentationCore;
using MS.Internal.Text.TextInterface;
using MS.Utility;

namespace System.Windows
{
	// Token: 0x020001C7 RID: 455
	internal sealed class ContextLayoutManager : DispatcherObject
	{
		// Token: 0x06000C0B RID: 3083 RVA: 0x0002DD44 File Offset: 0x0002D144
		internal ContextLayoutManager()
		{
			this._shutdownHandler = new EventHandler(this.OnDispatcherShutdown);
			base.Dispatcher.ShutdownFinished += this._shutdownHandler;
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0002DD7C File Offset: 0x0002D17C
		private void OnDispatcherShutdown(object sender, EventArgs e)
		{
			if (this._shutdownHandler != null)
			{
				base.Dispatcher.ShutdownFinished -= this._shutdownHandler;
			}
			this._shutdownHandler = null;
			this._layoutEvents = null;
			this._measureQueue = null;
			this._arrangeQueue = null;
			this._sizeChangedChain = null;
			this._isDead = true;
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0002DDCC File Offset: 0x0002D1CC
		internal static ContextLayoutManager From(Dispatcher dispatcher)
		{
			ContextLayoutManager contextLayoutManager = dispatcher.Reserved3 as ContextLayoutManager;
			if (contextLayoutManager == null)
			{
				if (Dispatcher.CurrentDispatcher != dispatcher)
				{
					throw new InvalidOperationException();
				}
				contextLayoutManager = new ContextLayoutManager();
				dispatcher.Reserved3 = contextLayoutManager;
			}
			return contextLayoutManager;
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0002DE04 File Offset: 0x0002D204
		private void setForceLayout(UIElement e)
		{
			this._forceLayoutElement = e;
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0002DE18 File Offset: 0x0002D218
		private void markTreeDirty(UIElement e)
		{
			for (;;)
			{
				UIElement uielement = e.GetUIParentNo3DTraversal() as UIElement;
				if (uielement == null)
				{
					break;
				}
				e = uielement;
			}
			this.markTreeDirtyHelper(e);
			this.MeasureQueue.Add(e);
			this.ArrangeQueue.Add(e);
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002DE58 File Offset: 0x0002D258
		private void markTreeDirtyHelper(Visual v)
		{
			if (v != null)
			{
				if (v.CheckFlagsAnd(VisualFlags.IsUIElement))
				{
					UIElement uielement = (UIElement)v;
					uielement.InvalidateMeasureInternal();
					uielement.InvalidateArrangeInternal();
				}
				int internalVisualChildrenCount = v.InternalVisualChildrenCount;
				for (int i = 0; i < internalVisualChildrenCount; i++)
				{
					Visual visual = v.InternalGetVisualChild(i);
					if (visual != null)
					{
						this.markTreeDirtyHelper(visual);
					}
				}
			}
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0002DEAC File Offset: 0x0002D2AC
		private void NeedsRecalc()
		{
			if (!this._layoutRequestPosted && !this._isUpdating)
			{
				MediaContext.From(base.Dispatcher).BeginInvokeOnRender(ContextLayoutManager._updateCallback, this);
				this._layoutRequestPosted = true;
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002DEE8 File Offset: 0x0002D2E8
		private static object UpdateLayoutBackground(object arg)
		{
			((ContextLayoutManager)arg).NeedsRecalc();
			return null;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x0002DF04 File Offset: 0x0002D304
		private bool hasDirtiness
		{
			get
			{
				return !this.MeasureQueue.IsEmpty || !this.ArrangeQueue.IsEmpty;
			}
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002DF30 File Offset: 0x0002D330
		internal void EnterMeasure()
		{
			base.Dispatcher._disableProcessingCount++;
			this._lastExceptionElement = null;
			this._measuresOnStack++;
			if (this._measuresOnStack > ContextLayoutManager.s_LayoutRecursionLimit)
			{
				throw new InvalidOperationException(SR.Get("LayoutManager_DeepRecursion", new object[]
				{
					ContextLayoutManager.s_LayoutRecursionLimit
				}));
			}
			this._firePostLayoutEvents = true;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0002DF9C File Offset: 0x0002D39C
		internal void ExitMeasure()
		{
			this._measuresOnStack--;
			base.Dispatcher._disableProcessingCount--;
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0002DFCC File Offset: 0x0002D3CC
		internal void EnterArrange()
		{
			base.Dispatcher._disableProcessingCount++;
			this._lastExceptionElement = null;
			this._arrangesOnStack++;
			if (this._arrangesOnStack > ContextLayoutManager.s_LayoutRecursionLimit)
			{
				throw new InvalidOperationException(SR.Get("LayoutManager_DeepRecursion", new object[]
				{
					ContextLayoutManager.s_LayoutRecursionLimit
				}));
			}
			this._firePostLayoutEvents = true;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0002E038 File Offset: 0x0002D438
		internal void ExitArrange()
		{
			this._arrangesOnStack--;
			base.Dispatcher._disableProcessingCount--;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002E068 File Offset: 0x0002D468
		internal void UpdateLayout()
		{
			base.VerifyAccess();
			if (this._isInUpdateLayout || this._measuresOnStack > 0 || this._arrangesOnStack > 0 || this._isDead)
			{
				return;
			}
			bool flag = false;
			long num = 0L;
			if (!this._isUpdating && EventTrace.IsEnabled(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info))
			{
				flag = true;
				num = PerfService.GetPerfElementID(this);
				EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientLayoutBegin, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, new object[]
				{
					num,
					EventTrace.LayoutSource.LayoutManager
				});
			}
			int num2 = 0;
			bool flag2 = true;
			UIElement uielement = null;
			try
			{
				this.invalidateTreeIfRecovering();
				while (this.hasDirtiness || this._firePostLayoutEvents)
				{
					if (++num2 > 153)
					{
						base.Dispatcher.BeginInvoke(DispatcherPriority.Background, ContextLayoutManager._updateLayoutBackground, this);
						uielement = null;
						flag2 = false;
						if (flag)
						{
							EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientLayoutAbort, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, new object[]
							{
								0,
								num2
							});
						}
						return;
					}
					this._isUpdating = true;
					this._isInUpdateLayout = true;
					if (flag)
					{
						EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientMeasureBegin, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, num);
					}
					using (base.Dispatcher.DisableProcessing())
					{
						int num3 = 0;
						DateTime utcNow = new DateTime(0L);
						TimeSpan timeSpan;
						for (;;)
						{
							if (++num3 > 153)
							{
								num3 = 0;
								if (utcNow.Ticks == 0L)
								{
									utcNow = DateTime.UtcNow;
								}
								else
								{
									timeSpan = DateTime.UtcNow - utcNow;
									if (timeSpan.Milliseconds > 306)
									{
										break;
									}
								}
							}
							uielement = this.MeasureQueue.GetTopMost();
							if (uielement == null)
							{
								goto IL_1E3;
							}
							uielement.Measure(uielement.PreviousConstraint);
						}
						base.Dispatcher.BeginInvoke(DispatcherPriority.Background, ContextLayoutManager._updateLayoutBackground, this);
						uielement = null;
						flag2 = false;
						if (flag)
						{
							EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientMeasureAbort, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, new object[]
							{
								timeSpan.Milliseconds,
								num3
							});
						}
						return;
						IL_1E3:
						if (flag)
						{
							EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientMeasureEnd, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, num3);
							EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientArrangeBegin, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, num);
						}
						num3 = 0;
						utcNow = new DateTime(0L);
						while (this.MeasureQueue.IsEmpty)
						{
							if (++num3 > 153)
							{
								num3 = 0;
								if (utcNow.Ticks == 0L)
								{
									utcNow = DateTime.UtcNow;
								}
								else
								{
									TimeSpan timeSpan2 = DateTime.UtcNow - utcNow;
									if (timeSpan2.Milliseconds > 306)
									{
										base.Dispatcher.BeginInvoke(DispatcherPriority.Background, ContextLayoutManager._updateLayoutBackground, this);
										uielement = null;
										flag2 = false;
										if (flag)
										{
											EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientArrangeAbort, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, new object[]
											{
												timeSpan2.Milliseconds,
												num3
											});
										}
										return;
									}
								}
							}
							uielement = this.ArrangeQueue.GetTopMost();
							if (uielement == null)
							{
								break;
							}
							Rect properArrangeRect = this.getProperArrangeRect(uielement);
							uielement.Arrange(properArrangeRect);
						}
						if (flag)
						{
							EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientArrangeEnd, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, num3);
						}
						if (!this.MeasureQueue.IsEmpty)
						{
							continue;
						}
						this._isInUpdateLayout = false;
					}
					this.fireSizeChangedEvents();
					if (!this.hasDirtiness)
					{
						this.fireLayoutUpdateEvent();
						if (!this.hasDirtiness)
						{
							this.fireAutomationEvents();
							if (!this.hasDirtiness)
							{
								this.fireSizeChangedEvents();
							}
						}
					}
				}
				uielement = null;
				flag2 = false;
			}
			finally
			{
				this._isUpdating = false;
				this._layoutRequestPosted = false;
				this._isInUpdateLayout = false;
				if (flag2)
				{
					if (flag)
					{
						EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientLayoutException, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, PerfService.GetPerfElementID(uielement));
					}
					this._gotException = true;
					this._forceLayoutElement = uielement;
					base.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, ContextLayoutManager._updateLayoutBackground, this);
				}
			}
			Font.ResetFontFaceCache();
			BufferCache.Reset();
			if (flag)
			{
				EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientLayoutEnd, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info);
			}
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002E4AC File Offset: 0x0002D8AC
		private Rect getProperArrangeRect(UIElement element)
		{
			Rect previousArrangeRect = element.PreviousArrangeRect;
			if (element.GetUIParentNo3DTraversal() == null)
			{
				previousArrangeRect.X = (previousArrangeRect.Y = 0.0);
				if (double.IsPositiveInfinity(element.PreviousConstraint.Width))
				{
					previousArrangeRect.Width = element.DesiredSize.Width;
				}
				if (double.IsPositiveInfinity(element.PreviousConstraint.Height))
				{
					previousArrangeRect.Height = element.DesiredSize.Height;
				}
			}
			return previousArrangeRect;
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0002E538 File Offset: 0x0002D938
		private void invalidateTreeIfRecovering()
		{
			if (this._forceLayoutElement != null || this._gotException)
			{
				if (this._forceLayoutElement != null)
				{
					this.markTreeDirty(this._forceLayoutElement);
				}
				this._forceLayoutElement = null;
				this._gotException = false;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0002E578 File Offset: 0x0002D978
		internal ContextLayoutManager.LayoutQueue MeasureQueue
		{
			get
			{
				if (this._measureQueue == null)
				{
					this._measureQueue = new ContextLayoutManager.InternalMeasureQueue();
				}
				return this._measureQueue;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0002E5A0 File Offset: 0x0002D9A0
		internal ContextLayoutManager.LayoutQueue ArrangeQueue
		{
			get
			{
				if (this._arrangeQueue == null)
				{
					this._arrangeQueue = new ContextLayoutManager.InternalArrangeQueue();
				}
				return this._arrangeQueue;
			}
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0002E5C8 File Offset: 0x0002D9C8
		private static object UpdateLayoutCallback(object arg)
		{
			ContextLayoutManager contextLayoutManager = arg as ContextLayoutManager;
			if (contextLayoutManager != null)
			{
				contextLayoutManager.UpdateLayout();
			}
			return null;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0002E5E8 File Offset: 0x0002D9E8
		private void fireLayoutUpdateEvent()
		{
			if (this._inFireLayoutUpdated)
			{
				return;
			}
			EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordLayout, EventTrace.Level.Verbose, EventTrace.Event.WClientLayoutFireLayoutUpdatedBegin);
			try
			{
				this._inFireLayoutUpdated = true;
				foreach (LayoutEventList.ListItem listItem in this.LayoutEvents.CopyToArray())
				{
					EventHandler eventHandler = null;
					try
					{
						eventHandler = (EventHandler)listItem.Target;
					}
					catch (InvalidOperationException)
					{
						eventHandler = null;
					}
					if (eventHandler != null)
					{
						eventHandler(null, EventArgs.Empty);
						if (this.hasDirtiness)
						{
							break;
						}
					}
					else
					{
						this.LayoutEvents.Remove(listItem);
					}
				}
			}
			finally
			{
				this._inFireLayoutUpdated = false;
				EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordLayout, EventTrace.Level.Verbose, EventTrace.Event.WClientLayoutFireLayoutUpdatedEnd);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x0002E6BC File Offset: 0x0002DABC
		internal LayoutEventList LayoutEvents
		{
			get
			{
				if (this._layoutEvents == null)
				{
					this._layoutEvents = new LayoutEventList();
				}
				return this._layoutEvents;
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0002E6E4 File Offset: 0x0002DAE4
		internal void AddToSizeChangedChain(SizeChangedInfo info)
		{
			info.Next = this._sizeChangedChain;
			this._sizeChangedChain = info;
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0002E704 File Offset: 0x0002DB04
		private void fireSizeChangedEvents()
		{
			if (this._inFireSizeChanged)
			{
				return;
			}
			EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordLayout, EventTrace.Level.Verbose, EventTrace.Event.WClientLayoutFireSizeChangedBegin);
			try
			{
				this._inFireSizeChanged = true;
				while (this._sizeChangedChain != null)
				{
					SizeChangedInfo sizeChangedChain = this._sizeChangedChain;
					this._sizeChangedChain = sizeChangedChain.Next;
					sizeChangedChain.Element.sizeChangedInfo = null;
					sizeChangedChain.Element.OnRenderSizeChanged(sizeChangedChain);
					if (this.hasDirtiness)
					{
						break;
					}
				}
			}
			finally
			{
				this._inFireSizeChanged = false;
				EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordLayout, EventTrace.Level.Verbose, EventTrace.Event.WClientLayoutFireSizeChangedEnd);
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0002E7A8 File Offset: 0x0002DBA8
		private void fireAutomationEvents()
		{
			if (this._inFireAutomationEvents)
			{
				return;
			}
			EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordLayout, EventTrace.Level.Verbose, EventTrace.Event.WClientLayoutFireAutomationEventsBegin);
			try
			{
				this._inFireAutomationEvents = true;
				this._firePostLayoutEvents = false;
				foreach (LayoutEventList.ListItem listItem in this.AutomationEvents.CopyToArray())
				{
					AutomationPeer automationPeer = null;
					try
					{
						automationPeer = (AutomationPeer)listItem.Target;
					}
					catch (InvalidOperationException)
					{
						automationPeer = null;
					}
					if (automationPeer != null)
					{
						automationPeer.FireAutomationEvents();
						if (this.hasDirtiness)
						{
							break;
						}
					}
					else
					{
						this.AutomationEvents.Remove(listItem);
					}
				}
			}
			finally
			{
				this._inFireAutomationEvents = false;
				EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordLayout, EventTrace.Level.Verbose, EventTrace.Event.WClientLayoutFireAutomationEventsEnd);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0002E87C File Offset: 0x0002DC7C
		internal LayoutEventList AutomationEvents
		{
			get
			{
				if (this._automationEvents == null)
				{
					this._automationEvents = new LayoutEventList();
				}
				return this._automationEvents;
			}
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0002E8A4 File Offset: 0x0002DCA4
		internal AutomationPeer[] GetAutomationRoots()
		{
			LayoutEventList.ListItem[] array = this.AutomationEvents.CopyToArray();
			AutomationPeer[] array2 = new AutomationPeer[array.Length];
			int num = 0;
			foreach (LayoutEventList.ListItem listItem in array)
			{
				AutomationPeer automationPeer = null;
				try
				{
					automationPeer = (AutomationPeer)listItem.Target;
				}
				catch (InvalidOperationException)
				{
					automationPeer = null;
				}
				if (automationPeer != null)
				{
					array2[num++] = automationPeer;
				}
			}
			return array2;
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0002E91C File Offset: 0x0002DD1C
		// (set) Token: 0x06000C26 RID: 3110 RVA: 0x0002E930 File Offset: 0x0002DD30
		internal int AutomationSyncUpdateCounter
		{
			get
			{
				return this._automationSyncUpdateCounter;
			}
			set
			{
				this._automationSyncUpdateCounter = value;
			}
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0002E944 File Offset: 0x0002DD44
		internal UIElement GetLastExceptionElement()
		{
			return this._lastExceptionElement;
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0002E958 File Offset: 0x0002DD58
		internal void SetLastExceptionElement(UIElement e)
		{
			this._lastExceptionElement = e;
		}

		// Token: 0x040006E9 RID: 1769
		private static DispatcherOperationCallback _updateCallback = new DispatcherOperationCallback(ContextLayoutManager.UpdateLayoutCallback);

		// Token: 0x040006EA RID: 1770
		private LayoutEventList _layoutEvents;

		// Token: 0x040006EB RID: 1771
		private LayoutEventList _automationEvents;

		// Token: 0x040006EC RID: 1772
		private UIElement _forceLayoutElement;

		// Token: 0x040006ED RID: 1773
		private UIElement _lastExceptionElement;

		// Token: 0x040006EE RID: 1774
		private ContextLayoutManager.InternalMeasureQueue _measureQueue;

		// Token: 0x040006EF RID: 1775
		private ContextLayoutManager.InternalArrangeQueue _arrangeQueue;

		// Token: 0x040006F0 RID: 1776
		private SizeChangedInfo _sizeChangedChain;

		// Token: 0x040006F1 RID: 1777
		private static DispatcherOperationCallback _updateLayoutBackground = new DispatcherOperationCallback(ContextLayoutManager.UpdateLayoutBackground);

		// Token: 0x040006F2 RID: 1778
		private EventHandler _shutdownHandler;

		// Token: 0x040006F3 RID: 1779
		internal static int s_LayoutRecursionLimit = 4096;

		// Token: 0x040006F4 RID: 1780
		private int _arrangesOnStack;

		// Token: 0x040006F5 RID: 1781
		private int _measuresOnStack;

		// Token: 0x040006F6 RID: 1782
		private int _automationSyncUpdateCounter;

		// Token: 0x040006F7 RID: 1783
		private bool _isDead;

		// Token: 0x040006F8 RID: 1784
		private bool _isUpdating;

		// Token: 0x040006F9 RID: 1785
		private bool _isInUpdateLayout;

		// Token: 0x040006FA RID: 1786
		private bool _gotException;

		// Token: 0x040006FB RID: 1787
		private bool _layoutRequestPosted;

		// Token: 0x040006FC RID: 1788
		private bool _inFireLayoutUpdated;

		// Token: 0x040006FD RID: 1789
		private bool _inFireSizeChanged;

		// Token: 0x040006FE RID: 1790
		private bool _firePostLayoutEvents;

		// Token: 0x040006FF RID: 1791
		private bool _inFireAutomationEvents;

		// Token: 0x020007FB RID: 2043
		internal class InternalMeasureQueue : ContextLayoutManager.LayoutQueue
		{
			// Token: 0x060055CB RID: 21963 RVA: 0x00161394 File Offset: 0x00160794
			internal override void setRequest(UIElement e, ContextLayoutManager.LayoutQueue.Request r)
			{
				e.MeasureRequest = r;
			}

			// Token: 0x060055CC RID: 21964 RVA: 0x001613A8 File Offset: 0x001607A8
			internal override ContextLayoutManager.LayoutQueue.Request getRequest(UIElement e)
			{
				return e.MeasureRequest;
			}

			// Token: 0x060055CD RID: 21965 RVA: 0x001613BC File Offset: 0x001607BC
			internal override bool canRelyOnParentRecalc(UIElement parent)
			{
				return !parent.IsMeasureValid && !parent.MeasureInProgress;
			}

			// Token: 0x060055CE RID: 21966 RVA: 0x001613DC File Offset: 0x001607DC
			internal override void invalidate(UIElement e)
			{
				e.InvalidateMeasureInternal();
			}
		}

		// Token: 0x020007FC RID: 2044
		internal class InternalArrangeQueue : ContextLayoutManager.LayoutQueue
		{
			// Token: 0x060055D0 RID: 21968 RVA: 0x00161404 File Offset: 0x00160804
			internal override void setRequest(UIElement e, ContextLayoutManager.LayoutQueue.Request r)
			{
				e.ArrangeRequest = r;
			}

			// Token: 0x060055D1 RID: 21969 RVA: 0x00161418 File Offset: 0x00160818
			internal override ContextLayoutManager.LayoutQueue.Request getRequest(UIElement e)
			{
				return e.ArrangeRequest;
			}

			// Token: 0x060055D2 RID: 21970 RVA: 0x0016142C File Offset: 0x0016082C
			internal override bool canRelyOnParentRecalc(UIElement parent)
			{
				return !parent.IsArrangeValid && !parent.ArrangeInProgress;
			}

			// Token: 0x060055D3 RID: 21971 RVA: 0x0016144C File Offset: 0x0016084C
			internal override void invalidate(UIElement e)
			{
				e.InvalidateArrangeInternal();
			}
		}

		// Token: 0x020007FD RID: 2045
		internal abstract class LayoutQueue
		{
			// Token: 0x060055D5 RID: 21973
			internal abstract ContextLayoutManager.LayoutQueue.Request getRequest(UIElement e);

			// Token: 0x060055D6 RID: 21974
			internal abstract void setRequest(UIElement e, ContextLayoutManager.LayoutQueue.Request r);

			// Token: 0x060055D7 RID: 21975
			internal abstract bool canRelyOnParentRecalc(UIElement parent);

			// Token: 0x060055D8 RID: 21976
			internal abstract void invalidate(UIElement e);

			// Token: 0x060055D9 RID: 21977 RVA: 0x00161474 File Offset: 0x00160874
			internal LayoutQueue()
			{
				for (int i = 0; i < 153; i++)
				{
					this._pocket = new ContextLayoutManager.LayoutQueue.Request
					{
						Next = this._pocket
					};
				}
				this._pocketSize = 153;
			}

			// Token: 0x060055DA RID: 21978 RVA: 0x001614BC File Offset: 0x001608BC
			private void _addRequest(UIElement e)
			{
				ContextLayoutManager.LayoutQueue.Request request = this._getNewRequest(e);
				if (request != null)
				{
					request.Next = this._head;
					if (this._head != null)
					{
						this._head.Prev = request;
					}
					this._head = request;
					this.setRequest(e, request);
				}
			}

			// Token: 0x060055DB RID: 21979 RVA: 0x00161504 File Offset: 0x00160904
			internal void Add(UIElement e)
			{
				if (this.getRequest(e) != null)
				{
					return;
				}
				if (e.CheckFlagsAnd(VisualFlags.IsLayoutSuspended))
				{
					return;
				}
				this.RemoveOrphans(e);
				UIElement uiparentWithinLayoutIsland = e.GetUIParentWithinLayoutIsland();
				if (uiparentWithinLayoutIsland != null && this.canRelyOnParentRecalc(uiparentWithinLayoutIsland))
				{
					return;
				}
				ContextLayoutManager contextLayoutManager = ContextLayoutManager.From(e.Dispatcher);
				if (contextLayoutManager._isDead)
				{
					return;
				}
				if (this._pocketSize > 8)
				{
					this._addRequest(e);
				}
				else
				{
					while (e != null)
					{
						UIElement uiparentWithinLayoutIsland2 = e.GetUIParentWithinLayoutIsland();
						this.invalidate(e);
						if (uiparentWithinLayoutIsland2 != null && uiparentWithinLayoutIsland2.Visibility != Visibility.Collapsed)
						{
							this.Remove(e);
						}
						else if (this.getRequest(e) == null)
						{
							this.RemoveOrphans(e);
							this._addRequest(e);
						}
						e = uiparentWithinLayoutIsland2;
					}
				}
				contextLayoutManager.NeedsRecalc();
			}

			// Token: 0x060055DC RID: 21980 RVA: 0x001615B0 File Offset: 0x001609B0
			internal void Remove(UIElement e)
			{
				ContextLayoutManager.LayoutQueue.Request request = this.getRequest(e);
				if (request == null)
				{
					return;
				}
				this._removeRequest(request);
				this.setRequest(e, null);
			}

			// Token: 0x060055DD RID: 21981 RVA: 0x001615D8 File Offset: 0x001609D8
			internal void RemoveOrphans(UIElement parent)
			{
				ContextLayoutManager.LayoutQueue.Request next;
				for (ContextLayoutManager.LayoutQueue.Request request = this._head; request != null; request = next)
				{
					UIElement target = request.Target;
					next = request.Next;
					ulong num = (ulong)parent.TreeLevel;
					if ((ulong)target.TreeLevel == num + 1UL && target.GetUIParentWithinLayoutIsland() == parent)
					{
						this._removeRequest(this.getRequest(target));
						this.setRequest(target, null);
					}
				}
			}

			// Token: 0x1700119A RID: 4506
			// (get) Token: 0x060055DE RID: 21982 RVA: 0x00161634 File Offset: 0x00160A34
			internal bool IsEmpty
			{
				get
				{
					return this._head == null;
				}
			}

			// Token: 0x060055DF RID: 21983 RVA: 0x0016164C File Offset: 0x00160A4C
			internal UIElement GetTopMost()
			{
				UIElement result = null;
				ulong num = ulong.MaxValue;
				for (ContextLayoutManager.LayoutQueue.Request request = this._head; request != null; request = request.Next)
				{
					UIElement target = request.Target;
					ulong num2 = (ulong)target.TreeLevel;
					if (num2 < num)
					{
						num = num2;
						result = request.Target;
					}
				}
				return result;
			}

			// Token: 0x060055E0 RID: 21984 RVA: 0x00161690 File Offset: 0x00160A90
			private void _removeRequest(ContextLayoutManager.LayoutQueue.Request entry)
			{
				if (entry.Prev == null)
				{
					this._head = entry.Next;
				}
				else
				{
					entry.Prev.Next = entry.Next;
				}
				if (entry.Next != null)
				{
					entry.Next.Prev = entry.Prev;
				}
				this.ReuseRequest(entry);
			}

			// Token: 0x060055E1 RID: 21985 RVA: 0x001616E4 File Offset: 0x00160AE4
			private ContextLayoutManager.LayoutQueue.Request _getNewRequest(UIElement e)
			{
				ContextLayoutManager.LayoutQueue.Request request;
				if (this._pocket != null)
				{
					request = this._pocket;
					this._pocket = request.Next;
					this._pocketSize--;
					request.Next = (request.Prev = null);
				}
				else
				{
					ContextLayoutManager contextLayoutManager = ContextLayoutManager.From(e.Dispatcher);
					try
					{
						request = new ContextLayoutManager.LayoutQueue.Request();
					}
					catch (OutOfMemoryException ex)
					{
						if (contextLayoutManager != null)
						{
							contextLayoutManager.setForceLayout(e);
						}
						throw ex;
					}
				}
				request.Target = e;
				return request;
			}

			// Token: 0x060055E2 RID: 21986 RVA: 0x00161774 File Offset: 0x00160B74
			private void ReuseRequest(ContextLayoutManager.LayoutQueue.Request r)
			{
				r.Target = null;
				if (this._pocketSize < 153)
				{
					r.Next = this._pocket;
					this._pocket = r;
					this._pocketSize++;
				}
			}

			// Token: 0x0400269A RID: 9882
			private const int PocketCapacity = 153;

			// Token: 0x0400269B RID: 9883
			private const int PocketReserve = 8;

			// Token: 0x0400269C RID: 9884
			private ContextLayoutManager.LayoutQueue.Request _head;

			// Token: 0x0400269D RID: 9885
			private ContextLayoutManager.LayoutQueue.Request _pocket;

			// Token: 0x0400269E RID: 9886
			private int _pocketSize;

			// Token: 0x02000A20 RID: 2592
			internal class Request
			{
				// Token: 0x04002F91 RID: 12177
				internal UIElement Target;

				// Token: 0x04002F92 RID: 12178
				internal ContextLayoutManager.LayoutQueue.Request Next;

				// Token: 0x04002F93 RID: 12179
				internal ContextLayoutManager.LayoutQueue.Request Prev;
			}
		}
	}
}
