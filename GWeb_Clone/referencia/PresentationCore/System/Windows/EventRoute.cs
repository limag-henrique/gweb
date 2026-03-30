using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows
{
	/// <summary>Representa o contêiner da rota a ser seguido por um evento roteado.</summary>
	// Token: 0x020001B1 RID: 433
	public sealed class EventRoute
	{
		/// <summary>Inicializa uma instância da classe <see cref="T:System.Windows.EventRoute" />.</summary>
		/// <param name="routedEvent">O identificador de evento não NULL a ser associado a esta rota de evento.</param>
		// Token: 0x060006B1 RID: 1713 RVA: 0x0001E97C File Offset: 0x0001DD7C
		public EventRoute(RoutedEvent routedEvent)
		{
			if (routedEvent == null)
			{
				throw new ArgumentNullException("routedEvent");
			}
			this._routedEvent = routedEvent;
			this._routeItemList = new FrugalStructList<RouteItem>(16);
			this._sourceItemList = new FrugalStructList<SourceItem>(16);
		}

		/// <summary>Adiciona o manipulador especificado para o destino especificado à rota.</summary>
		/// <param name="target">Especifica o objeto de destino do qual o manipulador é a ser adicionada à rota.</param>
		/// <param name="handler">Especifica o manipulador a ser adicionado para a rota.</param>
		/// <param name="handledEventsToo">Indica se o ouvinte detecta ou não os eventos que já foram tratados.</param>
		// Token: 0x060006B2 RID: 1714 RVA: 0x0001E9C0 File Offset: 0x0001DDC0
		public void Add(object target, Delegate handler, bool handledEventsToo)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			RouteItem value = new RouteItem(target, new RoutedEventHandlerInfo(handler, handledEventsToo));
			this._routeItemList.Add(value);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001EA08 File Offset: 0x0001DE08
		internal void InvokeHandlers(object source, RoutedEventArgs args)
		{
			this.InvokeHandlersImpl(source, args, false);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001EA20 File Offset: 0x0001DE20
		internal void ReInvokeHandlers(object source, RoutedEventArgs args)
		{
			this.InvokeHandlersImpl(source, args, true);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001EA38 File Offset: 0x0001DE38
		private void InvokeHandlersImpl(object source, RoutedEventArgs args, bool reRaised)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			if (args.Source == null)
			{
				throw new ArgumentException(SR.Get("SourceNotSet"));
			}
			if (args.RoutedEvent != this._routedEvent)
			{
				throw new ArgumentException(SR.Get("Mismatched_RoutedEvent"));
			}
			if (args.RoutedEvent.RoutingStrategy == RoutingStrategy.Bubble || args.RoutedEvent.RoutingStrategy == RoutingStrategy.Direct)
			{
				int num = 0;
				for (int i = 0; i < this._routeItemList.Count; i++)
				{
					if (i >= num)
					{
						object bubbleSource = this.GetBubbleSource(i, out num);
						if (!reRaised)
						{
							if (bubbleSource == null)
							{
								args.Source = source;
							}
							else
							{
								args.Source = bubbleSource;
							}
						}
					}
					if (TraceRoutedEvent.IsEnabled)
					{
						TraceRoutedEvent.Trace(TraceEventType.Start, TraceRoutedEvent.InvokeHandlers, this._routeItemList[i].Target, args, args.Handled);
					}
					this._routeItemList[i].InvokeHandler(args);
					if (TraceRoutedEvent.IsEnabled)
					{
						TraceRoutedEvent.Trace(TraceEventType.Stop, TraceRoutedEvent.InvokeHandlers, this._routeItemList[i].Target, args, args.Handled);
					}
				}
				return;
			}
			int count = this._routeItemList.Count;
			int num2;
			for (int j = this._routeItemList.Count - 1; j >= 0; j = num2)
			{
				object target = this._routeItemList[j].Target;
				num2 = j;
				while (num2 >= 0 && this._routeItemList[num2].Target == target)
				{
					num2--;
				}
				for (int k = num2 + 1; k <= j; k++)
				{
					if (k < count)
					{
						object tunnelSource = this.GetTunnelSource(k, out count);
						if (tunnelSource == null)
						{
							args.Source = source;
						}
						else
						{
							args.Source = tunnelSource;
						}
					}
					if (TraceRoutedEvent.IsEnabled)
					{
						TraceRoutedEvent.Trace(TraceEventType.Start, TraceRoutedEvent.InvokeHandlers, this._routeItemList[k].Target, args, args.Handled);
					}
					this._routeItemList[k].InvokeHandler(args);
					if (TraceRoutedEvent.IsEnabled)
					{
						TraceRoutedEvent.Trace(TraceEventType.Stop, TraceRoutedEvent.InvokeHandlers, this._routeItemList[k].Target, args, args.Handled);
					}
				}
			}
		}

		/// <summary>Adiciona o nó superior à pilha de rota de evento no qual duas árvores lógicos divergem.</summary>
		/// <param name="node">O elemento superior na pilha de rota de evento na qual duas árvores lógicas divergem.</param>
		/// <param name="source">A origem para o elemento superior na pilha de rota de evento no qual duas árvores lógicas divergem.</param>
		// Token: 0x060006B6 RID: 1718 RVA: 0x0001ECA4 File Offset: 0x0001E0A4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void PushBranchNode(object node, object source)
		{
			EventRoute.BranchNode item = default(EventRoute.BranchNode);
			item.Node = node;
			item.Source = source;
			this.BranchNodeStack.Push(item);
		}

		/// <summary>Retorna o nó superior na pilha de rota de evento na qual duas árvores lógicas divergem.</summary>
		/// <returns>O nó superior na pilha de rota de evento na qual duas árvores lógicas divergem.</returns>
		// Token: 0x060006B7 RID: 1719 RVA: 0x0001ECD8 File Offset: 0x0001E0D8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public object PopBranchNode()
		{
			if (this.BranchNodeStack.Count == 0)
			{
				return null;
			}
			EventRoute.BranchNode branchNode = this.BranchNodeStack.Pop();
			return branchNode.Node;
		}

		/// <summary>Retorna o elemento superior na pilha de rota de evento na qual duas árvores lógicas divergem.</summary>
		/// <returns>O elemento superior na pilha de rota de evento na qual duas árvores lógicas divergem.</returns>
		// Token: 0x060006B8 RID: 1720 RVA: 0x0001ED08 File Offset: 0x0001E108
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public object PeekBranchNode()
		{
			if (this.BranchNodeStack.Count == 0)
			{
				return null;
			}
			EventRoute.BranchNode branchNode = this.BranchNodeStack.Peek();
			return branchNode.Node;
		}

		/// <summary>Retorna a origem para o elemento superior na pilha de rota de evento no qual duas árvores lógicas divergem.</summary>
		/// <returns>A origem para o elemento superior na pilha de rota de evento no qual duas árvores lógicas divergem.</returns>
		// Token: 0x060006B9 RID: 1721 RVA: 0x0001ED38 File Offset: 0x0001E138
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public object PeekBranchSource()
		{
			if (this.BranchNodeStack.Count == 0)
			{
				return null;
			}
			EventRoute.BranchNode branchNode = this.BranchNodeStack.Peek();
			return branchNode.Source;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0001ED68 File Offset: 0x0001E168
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x0001ED7C File Offset: 0x0001E17C
		internal RoutedEvent RoutedEvent
		{
			get
			{
				return this._routedEvent;
			}
			set
			{
				this._routedEvent = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0001ED90 File Offset: 0x0001E190
		private Stack<EventRoute.BranchNode> BranchNodeStack
		{
			get
			{
				if (this._branchNodeStack == null)
				{
					this._branchNodeStack = new Stack<EventRoute.BranchNode>(1);
				}
				return this._branchNodeStack;
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001EDB8 File Offset: 0x0001E1B8
		internal void AddSource(object source)
		{
			int count = this._routeItemList.Count;
			this._sourceItemList.Add(new SourceItem(count, source));
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001EDE4 File Offset: 0x0001E1E4
		private object GetBubbleSource(int index, out int endIndex)
		{
			if (this._sourceItemList.Count == 0)
			{
				endIndex = this._routeItemList.Count;
				return null;
			}
			if (index < this._sourceItemList[0].StartIndex)
			{
				endIndex = this._sourceItemList[0].StartIndex;
				return null;
			}
			for (int i = 0; i < this._sourceItemList.Count - 1; i++)
			{
				if (index >= this._sourceItemList[i].StartIndex && index < this._sourceItemList[i + 1].StartIndex)
				{
					endIndex = this._sourceItemList[i + 1].StartIndex;
					return this._sourceItemList[i].Source;
				}
			}
			endIndex = this._routeItemList.Count;
			return this._sourceItemList[this._sourceItemList.Count - 1].Source;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001EEE0 File Offset: 0x0001E2E0
		private object GetTunnelSource(int index, out int startIndex)
		{
			if (this._sourceItemList.Count == 0)
			{
				startIndex = 0;
				return null;
			}
			if (index < this._sourceItemList[0].StartIndex)
			{
				startIndex = 0;
				return null;
			}
			for (int i = 0; i < this._sourceItemList.Count - 1; i++)
			{
				if (index >= this._sourceItemList[i].StartIndex && index < this._sourceItemList[i + 1].StartIndex)
				{
					startIndex = this._sourceItemList[i].StartIndex;
					return this._sourceItemList[i].Source;
				}
			}
			startIndex = this._sourceItemList[this._sourceItemList.Count - 1].StartIndex;
			return this._sourceItemList[this._sourceItemList.Count - 1].Source;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001EFD0 File Offset: 0x0001E3D0
		internal void Clear()
		{
			this._routedEvent = null;
			this._routeItemList.Clear();
			if (this._branchNodeStack != null)
			{
				this._branchNodeStack.Clear();
			}
			this._sourceItemList.Clear();
		}

		// Token: 0x040005A6 RID: 1446
		private RoutedEvent _routedEvent;

		// Token: 0x040005A7 RID: 1447
		private FrugalStructList<RouteItem> _routeItemList;

		// Token: 0x040005A8 RID: 1448
		private Stack<EventRoute.BranchNode> _branchNodeStack;

		// Token: 0x040005A9 RID: 1449
		private FrugalStructList<SourceItem> _sourceItemList;

		// Token: 0x020007F6 RID: 2038
		private struct BranchNode
		{
			// Token: 0x0400268D RID: 9869
			public object Node;

			// Token: 0x0400268E RID: 9870
			public object Source;
		}
	}
}
