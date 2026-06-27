using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.Interop;
using MS.Internal.KnownBoxes;
using MS.Internal.Permissions;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows
{
	/// <summary>
	///   <see cref="T:System.Windows.UIElement3D" /> é uma classe base para implementações no nível do núcleo WPF baseada em elementos WPF (Windows Presentation Foundation) e características de apresentação básicas.</summary>
	// Token: 0x020001BF RID: 447
	public abstract class UIElement3D : Visual3D, IInputElement
	{
		/// <summary>Obtém a coleção de ligações de entrada associadas a este elemento.</summary>
		/// <returns>A coleção de ligações de entrada.</returns>
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x00029F1C File Offset: 0x0002931C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public InputBindingCollection InputBindings
		{
			get
			{
				base.VerifyAccess();
				InputBindingCollection inputBindingCollection = UIElement3D.InputBindingCollectionField.GetValue(this);
				if (inputBindingCollection == null)
				{
					inputBindingCollection = new InputBindingCollection(this);
					UIElement3D.InputBindingCollectionField.SetValue(this, inputBindingCollection);
				}
				return inputBindingCollection;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x00029F54 File Offset: 0x00029354
		internal InputBindingCollection InputBindingsInternal
		{
			get
			{
				base.VerifyAccess();
				return UIElement3D.InputBindingCollectionField.GetValue(this);
			}
		}

		/// <summary>Indica se os processos de serialização devem serializar o conteúdo da propriedade <see cref="P:System.Windows.UIElement3D.InputBindings" /> em instâncias dessa classe.</summary>
		/// <returns>
		///   <see langword="true" /> se o valor da propriedade <see cref="P:System.Windows.UIElement3D.InputBindings" /> precisar ser serializado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060009ED RID: 2541 RVA: 0x00029F74 File Offset: 0x00029374
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeInputBindings()
		{
			InputBindingCollection value = UIElement3D.InputBindingCollectionField.GetValue(this);
			return value != null && value.Count > 0;
		}

		/// <summary>Obtém uma coleção de objetos <see cref="T:System.Windows.Input.CommandBinding" /> associados a esse elemento.</summary>
		/// <returns>A coleção de todos os objetos <see cref="T:System.Windows.Input.CommandBinding" />.</returns>
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x00029F9C File Offset: 0x0002939C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CommandBindingCollection CommandBindings
		{
			get
			{
				base.VerifyAccess();
				CommandBindingCollection commandBindingCollection = UIElement3D.CommandBindingCollectionField.GetValue(this);
				if (commandBindingCollection == null)
				{
					commandBindingCollection = new CommandBindingCollection();
					UIElement3D.CommandBindingCollectionField.SetValue(this, commandBindingCollection);
				}
				return commandBindingCollection;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00029FD4 File Offset: 0x000293D4
		internal CommandBindingCollection CommandBindingsInternal
		{
			get
			{
				base.VerifyAccess();
				return UIElement3D.CommandBindingCollectionField.GetValue(this);
			}
		}

		/// <summary>Indica se os processos de serialização devem serializar o conteúdo da propriedade <see cref="P:System.Windows.UIElement3D.CommandBindings" /> em instâncias dessa classe.</summary>
		/// <returns>
		///   <see langword="true" /> se o valor da propriedade <see cref="P:System.Windows.UIElement3D.CommandBindings" /> precisar ser serializado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060009F0 RID: 2544 RVA: 0x00029FF4 File Offset: 0x000293F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeCommandBindings()
		{
			CommandBindingCollection value = UIElement3D.CommandBindingCollectionField.GetValue(this);
			return value != null && value.Count > 0;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0002A01C File Offset: 0x0002941C
		internal virtual bool BuildRouteCore(EventRoute route, RoutedEventArgs args)
		{
			return false;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0002A02C File Offset: 0x0002942C
		internal void BuildRoute(EventRoute route, RoutedEventArgs args)
		{
			UIElement.BuildRouteHelper(this, route, args);
		}

		/// <summary>Aciona um evento roteado específico. O <see cref="T:System.Windows.RoutedEvent" /> a ser gerado é identificado na instância <see cref="T:System.Windows.RoutedEventArgs" /> fornecida (como a propriedade <see cref="P:System.Windows.RoutedEventArgs.RoutedEvent" /> desses dados de eventos).</summary>
		/// <param name="e">Um <see cref="T:System.Windows.RoutedEventArgs" /> que contém os dados do evento e também identifica o evento a ser acionado.</param>
		// Token: 0x060009F3 RID: 2547 RVA: 0x0002A044 File Offset: 0x00029444
		public void RaiseEvent(RoutedEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			e.ClearUserInitiated();
			UIElement.RaiseEventImpl(this, e);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0002A06C File Offset: 0x0002946C
		[SecurityCritical]
		internal void RaiseEvent(RoutedEventArgs args, bool trusted)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			if (trusted)
			{
				this.RaiseTrustedEvent(args);
				return;
			}
			args.ClearUserInitiated();
			UIElement.RaiseEventImpl(this, args);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002A0A0 File Offset: 0x000294A0
		[SecurityCritical]
		[UserInitiatedRoutedEventPermission(SecurityAction.Assert)]
		internal void RaiseTrustedEvent(RoutedEventArgs args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			args.MarkAsUserInitiated();
			try
			{
				UIElement.RaiseEventImpl(this, args);
			}
			finally
			{
				args.ClearUserInitiated();
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0002A0F0 File Offset: 0x000294F0
		internal virtual object AdjustEventSource(RoutedEventArgs args)
		{
			return null;
		}

		/// <summary>Adiciona um manipulador de eventos roteados de um evento roteado especificado, adicionando o manipulador à coleção de manipuladores no elemento atual.</summary>
		/// <param name="routedEvent">Um identificador do evento roteado a ser manipulado.</param>
		/// <param name="handler">Uma referência à implementação do manipulador.</param>
		// Token: 0x060009F7 RID: 2551 RVA: 0x0002A100 File Offset: 0x00029500
		public void AddHandler(RoutedEvent routedEvent, Delegate handler)
		{
			this.AddHandler(routedEvent, handler, false);
		}

		/// <summary>Adiciona um manipulador de eventos roteados de um evento roteado especificado, adicionando o manipulador à coleção de manipuladores no elemento atual. Especifique <paramref name="handledEventsToo" /> como <see langword="true" /> para que o manipulador fornecido seja invocado para eventos roteados que já tenham sido marcados como manipulados por outro elemento na rota de evento.</summary>
		/// <param name="routedEvent">Um identificador do evento roteado a ser manipulado.</param>
		/// <param name="handler">Uma referência à implementação do manipulador.</param>
		/// <param name="handledEventsToo">
		///   <see langword="true" /> para registrar o manipulador de modo que ele seja invocado mesmo quando o evento roteado estiver marcado como tratado nos dados do evento; <see langword="false" /> para registrar o manipulador com a condição padrão que não será chamado se o evento roteado já estiver marcado como tratado.  
		/// O padrão é <see langword="false" />.  
		/// Não solicite sempre para tratar novamente um evento roteado.</param>
		// Token: 0x060009F8 RID: 2552 RVA: 0x0002A118 File Offset: 0x00029518
		public void AddHandler(RoutedEvent routedEvent, Delegate handler, bool handledEventsToo)
		{
			if (routedEvent == null)
			{
				throw new ArgumentNullException("routedEvent");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (!routedEvent.IsLegalHandler(handler))
			{
				throw new ArgumentException(SR.Get("HandlerTypeIllegal"));
			}
			this.EnsureEventHandlersStore();
			this.EventHandlersStore.AddRoutedEventHandler(routedEvent, handler, handledEventsToo);
			this.OnAddHandler(routedEvent, handler);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0002A178 File Offset: 0x00029578
		internal virtual void OnAddHandler(RoutedEvent routedEvent, Delegate handler)
		{
		}

		/// <summary>Remove o manipulador de eventos roteados especificado desse elemento.</summary>
		/// <param name="routedEvent">O identificador do evento roteado ao qual o manipulador está anexado.</param>
		/// <param name="handler">A implementação do manipulador específico para remover da coleção de manipuladores de eventos neste elemento.</param>
		// Token: 0x060009FA RID: 2554 RVA: 0x0002A188 File Offset: 0x00029588
		public void RemoveHandler(RoutedEvent routedEvent, Delegate handler)
		{
			if (routedEvent == null)
			{
				throw new ArgumentNullException("routedEvent");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (!routedEvent.IsLegalHandler(handler))
			{
				throw new ArgumentException(SR.Get("HandlerTypeIllegal"));
			}
			EventHandlersStore eventHandlersStore = this.EventHandlersStore;
			if (eventHandlersStore != null)
			{
				eventHandlersStore.RemoveRoutedEventHandler(routedEvent, handler);
				this.OnRemoveHandler(routedEvent, handler);
				if (eventHandlersStore.Count == 0)
				{
					UIElement3D.EventHandlersStoreField.ClearValue(this);
					this.WriteFlag(CoreFlags.ExistsEventHandlersStore, false);
				}
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0002A204 File Offset: 0x00029604
		internal virtual void OnRemoveHandler(RoutedEvent routedEvent, Delegate handler)
		{
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0002A214 File Offset: 0x00029614
		private void EventHandlersStoreAdd(EventPrivateKey key, Delegate handler)
		{
			this.EnsureEventHandlersStore();
			this.EventHandlersStore.Add(key, handler);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0002A234 File Offset: 0x00029634
		private void EventHandlersStoreRemove(EventPrivateKey key, Delegate handler)
		{
			EventHandlersStore eventHandlersStore = this.EventHandlersStore;
			if (eventHandlersStore != null)
			{
				eventHandlersStore.Remove(key, handler);
				if (eventHandlersStore.Count == 0)
				{
					UIElement3D.EventHandlersStoreField.ClearValue(this);
					this.WriteFlag(CoreFlags.ExistsEventHandlersStore, false);
				}
			}
		}

		/// <summary>Adiciona manipuladores ao <see cref="T:System.Windows.EventRoute" /> especificado para a coleção do manipulador de eventos <see cref="T:System.Windows.UIElement3D" /> atual.</summary>
		/// <param name="route">A rota de eventos à qual os manipuladores são adicionados.</param>
		/// <param name="e">Os dados de evento usados para adicionar os manipuladores. Esse método usa a propriedade <see cref="P:System.Windows.RoutedEventArgs.RoutedEvent" /> dos dados do evento para criar os manipuladores.</param>
		// Token: 0x060009FE RID: 2558 RVA: 0x0002A274 File Offset: 0x00029674
		public void AddToEventRoute(EventRoute route, RoutedEventArgs e)
		{
			if (route == null)
			{
				throw new ArgumentNullException("route");
			}
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			for (RoutedEventHandlerInfoList routedEventHandlerInfoList = GlobalEventManager.GetDTypedClassListeners(base.DependencyObjectType, e.RoutedEvent); routedEventHandlerInfoList != null; routedEventHandlerInfoList = routedEventHandlerInfoList.Next)
			{
				for (int i = 0; i < routedEventHandlerInfoList.Handlers.Length; i++)
				{
					route.Add(this, routedEventHandlerInfoList.Handlers[i].Handler, routedEventHandlerInfoList.Handlers[i].InvokeHandledEventsToo);
				}
			}
			EventHandlersStore eventHandlersStore = this.EventHandlersStore;
			if (eventHandlersStore != null)
			{
				FrugalObjectList<RoutedEventHandlerInfo> frugalObjectList = eventHandlersStore[e.RoutedEvent];
				if (frugalObjectList != null)
				{
					for (int j = 0; j < frugalObjectList.Count; j++)
					{
						route.Add(this, frugalObjectList[j].Handler, frugalObjectList[j].InvokeHandledEventsToo);
					}
				}
			}
			this.AddToEventRouteCore(route, e);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0002A358 File Offset: 0x00029758
		internal virtual void AddToEventRouteCore(EventRoute route, RoutedEventArgs args)
		{
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0002A368 File Offset: 0x00029768
		internal EventHandlersStore EventHandlersStore
		{
			[FriendAccessAllowed]
			get
			{
				if (!this.ReadFlag(CoreFlags.ExistsEventHandlersStore))
				{
					return null;
				}
				return UIElement3D.EventHandlersStoreField.GetValue(this);
			}
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002A390 File Offset: 0x00029790
		[FriendAccessAllowed]
		internal void EnsureEventHandlersStore()
		{
			if (this.EventHandlersStore == null)
			{
				UIElement3D.EventHandlersStoreField.SetValue(this, new EventHandlersStore());
				this.WriteFlag(CoreFlags.ExistsEventHandlersStore, true);
			}
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0002A3C4 File Offset: 0x000297C4
		internal virtual bool InvalidateAutomationAncestorsCore(Stack<DependencyObject> branchNodeStack, out bool continuePastVisualTree)
		{
			continuePastVisualTree = false;
			return true;
		}

		/// <summary>Ocorre quando qualquer botão do mouse é pressionado enquanto o ponteiro está sobre este elemento.</summary>
		// Token: 0x140000BC RID: 188
		// (add) Token: 0x06000A03 RID: 2563 RVA: 0x0002A3D8 File Offset: 0x000297D8
		// (remove) Token: 0x06000A04 RID: 2564 RVA: 0x0002A3F4 File Offset: 0x000297F4
		public event MouseButtonEventHandler PreviewMouseDown
		{
			add
			{
				this.AddHandler(Mouse.PreviewMouseDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.PreviewMouseDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento roteado anexado <see cref="E:System.Windows.Input.Mouse.PreviewMouseDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados de evento relatam que um ou mais botões do mouse foram pressionados.</param>
		// Token: 0x06000A05 RID: 2565 RVA: 0x0002A410 File Offset: 0x00029810
		protected internal virtual void OnPreviewMouseDown(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando qualquer botão do mouse é pressionado enquanto o ponteiro está sobre este elemento.</summary>
		// Token: 0x140000BD RID: 189
		// (add) Token: 0x06000A06 RID: 2566 RVA: 0x0002A420 File Offset: 0x00029820
		// (remove) Token: 0x06000A07 RID: 2567 RVA: 0x0002A43C File Offset: 0x0002983C
		public event MouseButtonEventHandler MouseDown
		{
			add
			{
				this.AddHandler(Mouse.MouseDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.MouseDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.MouseDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Esses dados de evento relatam detalhes sobre o botão do mouse que foi pressionado e o estado tratado.</param>
		// Token: 0x06000A08 RID: 2568 RVA: 0x0002A458 File Offset: 0x00029858
		protected internal virtual void OnMouseDown(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando qualquer botão do mouse é liberado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x140000BE RID: 190
		// (add) Token: 0x06000A09 RID: 2569 RVA: 0x0002A468 File Offset: 0x00029868
		// (remove) Token: 0x06000A0A RID: 2570 RVA: 0x0002A484 File Offset: 0x00029884
		public event MouseButtonEventHandler PreviewMouseUp
		{
			add
			{
				this.AddHandler(Mouse.PreviewMouseUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.PreviewMouseUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.PreviewMouseUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados de evento informam que um ou mais botões do mouse foram soltos.</param>
		// Token: 0x06000A0B RID: 2571 RVA: 0x0002A4A0 File Offset: 0x000298A0
		protected internal virtual void OnPreviewMouseUp(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando qualquer botão do mouse é liberado sobre este elemento.</summary>
		// Token: 0x140000BF RID: 191
		// (add) Token: 0x06000A0C RID: 2572 RVA: 0x0002A4B0 File Offset: 0x000298B0
		// (remove) Token: 0x06000A0D RID: 2573 RVA: 0x0002A4CC File Offset: 0x000298CC
		public event MouseButtonEventHandler MouseUp
		{
			add
			{
				this.AddHandler(Mouse.MouseUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.MouseUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.Input.Mouse.MouseUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão do mouse foi liberado.</param>
		// Token: 0x06000A0E RID: 2574 RVA: 0x0002A4E8 File Offset: 0x000298E8
		protected internal virtual void OnMouseUp(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão esquerdo do mouse é pressionado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x140000C0 RID: 192
		// (add) Token: 0x06000A0F RID: 2575 RVA: 0x0002A4F8 File Offset: 0x000298F8
		// (remove) Token: 0x06000A10 RID: 2576 RVA: 0x0002A514 File Offset: 0x00029914
		public event MouseButtonEventHandler PreviewMouseLeftButtonDown
		{
			add
			{
				this.AddHandler(UIElement.PreviewMouseLeftButtonDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.PreviewMouseLeftButtonDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.UIElement3D.PreviewMouseLeftButtonDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão esquerdo do mouse foi pressionado.</param>
		// Token: 0x06000A11 RID: 2577 RVA: 0x0002A530 File Offset: 0x00029930
		protected internal virtual void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão esquerdo do mouse é pressionado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x140000C1 RID: 193
		// (add) Token: 0x06000A12 RID: 2578 RVA: 0x0002A540 File Offset: 0x00029940
		// (remove) Token: 0x06000A13 RID: 2579 RVA: 0x0002A55C File Offset: 0x0002995C
		public event MouseButtonEventHandler MouseLeftButtonDown
		{
			add
			{
				this.AddHandler(UIElement.MouseLeftButtonDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.MouseLeftButtonDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.UIElement3D.MouseLeftButtonDown" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão esquerdo do mouse foi pressionado.</param>
		// Token: 0x06000A14 RID: 2580 RVA: 0x0002A578 File Offset: 0x00029978
		protected internal virtual void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão esquerdo do mouse é liberado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x140000C2 RID: 194
		// (add) Token: 0x06000A15 RID: 2581 RVA: 0x0002A588 File Offset: 0x00029988
		// (remove) Token: 0x06000A16 RID: 2582 RVA: 0x0002A5A4 File Offset: 0x000299A4
		public event MouseButtonEventHandler PreviewMouseLeftButtonUp
		{
			add
			{
				this.AddHandler(UIElement.PreviewMouseLeftButtonUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.PreviewMouseLeftButtonUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.UIElement3D.PreviewMouseLeftButtonUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão esquerdo foi liberado.</param>
		// Token: 0x06000A17 RID: 2583 RVA: 0x0002A5C0 File Offset: 0x000299C0
		protected internal virtual void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão esquerdo do mouse é liberado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x140000C3 RID: 195
		// (add) Token: 0x06000A18 RID: 2584 RVA: 0x0002A5D0 File Offset: 0x000299D0
		// (remove) Token: 0x06000A19 RID: 2585 RVA: 0x0002A5EC File Offset: 0x000299EC
		public event MouseButtonEventHandler MouseLeftButtonUp
		{
			add
			{
				this.AddHandler(UIElement.MouseLeftButtonUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.MouseLeftButtonUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.UIElement3D.MouseLeftButtonUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão esquerdo foi liberado.</param>
		// Token: 0x06000A1A RID: 2586 RVA: 0x0002A608 File Offset: 0x00029A08
		protected internal virtual void OnMouseLeftButtonUp(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão direito do mouse é pressionado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x140000C4 RID: 196
		// (add) Token: 0x06000A1B RID: 2587 RVA: 0x0002A618 File Offset: 0x00029A18
		// (remove) Token: 0x06000A1C RID: 2588 RVA: 0x0002A634 File Offset: 0x00029A34
		public event MouseButtonEventHandler PreviewMouseRightButtonDown
		{
			add
			{
				this.AddHandler(UIElement.PreviewMouseRightButtonDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.PreviewMouseRightButtonDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.UIElement3D.PreviewMouseRightButtonDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão direito do mouse foi pressionado.</param>
		// Token: 0x06000A1D RID: 2589 RVA: 0x0002A650 File Offset: 0x00029A50
		protected internal virtual void OnPreviewMouseRightButtonDown(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão direito do mouse é pressionado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x140000C5 RID: 197
		// (add) Token: 0x06000A1E RID: 2590 RVA: 0x0002A660 File Offset: 0x00029A60
		// (remove) Token: 0x06000A1F RID: 2591 RVA: 0x0002A67C File Offset: 0x00029A7C
		public event MouseButtonEventHandler MouseRightButtonDown
		{
			add
			{
				this.AddHandler(UIElement.MouseRightButtonDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.MouseRightButtonDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.UIElement3D.MouseRightButtonDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão direito do mouse foi pressionado.</param>
		// Token: 0x06000A20 RID: 2592 RVA: 0x0002A698 File Offset: 0x00029A98
		protected internal virtual void OnMouseRightButtonDown(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão direito do mouse é liberado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x140000C6 RID: 198
		// (add) Token: 0x06000A21 RID: 2593 RVA: 0x0002A6A8 File Offset: 0x00029AA8
		// (remove) Token: 0x06000A22 RID: 2594 RVA: 0x0002A6C4 File Offset: 0x00029AC4
		public event MouseButtonEventHandler PreviewMouseRightButtonUp
		{
			add
			{
				this.AddHandler(UIElement.PreviewMouseRightButtonUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.PreviewMouseRightButtonUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.UIElement3D.PreviewMouseRightButtonUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão direito do mouse foi liberado.</param>
		// Token: 0x06000A23 RID: 2595 RVA: 0x0002A6E0 File Offset: 0x00029AE0
		protected internal virtual void OnPreviewMouseRightButtonUp(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão direito do mouse é liberado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x140000C7 RID: 199
		// (add) Token: 0x06000A24 RID: 2596 RVA: 0x0002A6F0 File Offset: 0x00029AF0
		// (remove) Token: 0x06000A25 RID: 2597 RVA: 0x0002A70C File Offset: 0x00029B0C
		public event MouseButtonEventHandler MouseRightButtonUp
		{
			add
			{
				this.AddHandler(UIElement.MouseRightButtonUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.MouseRightButtonUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.UIElement3D.MouseRightButtonUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão direito do mouse foi liberado.</param>
		// Token: 0x06000A26 RID: 2598 RVA: 0x0002A728 File Offset: 0x00029B28
		protected internal virtual void OnMouseRightButtonUp(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o ponteiro do mouse se move enquanto está sobre este elemento.</summary>
		// Token: 0x140000C8 RID: 200
		// (add) Token: 0x06000A27 RID: 2599 RVA: 0x0002A738 File Offset: 0x00029B38
		// (remove) Token: 0x06000A28 RID: 2600 RVA: 0x0002A754 File Offset: 0x00029B54
		public event MouseEventHandler PreviewMouseMove
		{
			add
			{
				this.AddHandler(Mouse.PreviewMouseMoveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.PreviewMouseMoveEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.PreviewMouseMove" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A29 RID: 2601 RVA: 0x0002A770 File Offset: 0x00029B70
		protected internal virtual void OnPreviewMouseMove(MouseEventArgs e)
		{
		}

		/// <summary>Ocorre quando o ponteiro do mouse se move enquanto está sobre este elemento.</summary>
		// Token: 0x140000C9 RID: 201
		// (add) Token: 0x06000A2A RID: 2602 RVA: 0x0002A780 File Offset: 0x00029B80
		// (remove) Token: 0x06000A2B RID: 2603 RVA: 0x0002A79C File Offset: 0x00029B9C
		public event MouseEventHandler MouseMove
		{
			add
			{
				this.AddHandler(Mouse.MouseMoveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.MouseMoveEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.MouseMove" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A2C RID: 2604 RVA: 0x0002A7B8 File Offset: 0x00029BB8
		protected internal virtual void OnMouseMove(MouseEventArgs e)
		{
		}

		/// <summary>Ocorre quando o usuário gira a roda do mouse enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x140000CA RID: 202
		// (add) Token: 0x06000A2D RID: 2605 RVA: 0x0002A7C8 File Offset: 0x00029BC8
		// (remove) Token: 0x06000A2E RID: 2606 RVA: 0x0002A7E4 File Offset: 0x00029BE4
		public event MouseWheelEventHandler PreviewMouseWheel
		{
			add
			{
				this.AddHandler(Mouse.PreviewMouseWheelEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.PreviewMouseWheelEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.PreviewMouseWheel" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseWheelEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A2F RID: 2607 RVA: 0x0002A800 File Offset: 0x00029C00
		protected internal virtual void OnPreviewMouseWheel(MouseWheelEventArgs e)
		{
		}

		/// <summary>Ocorre quando o usuário gira a roda do mouse enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x140000CB RID: 203
		// (add) Token: 0x06000A30 RID: 2608 RVA: 0x0002A810 File Offset: 0x00029C10
		// (remove) Token: 0x06000A31 RID: 2609 RVA: 0x0002A82C File Offset: 0x00029C2C
		public event MouseWheelEventHandler MouseWheel
		{
			add
			{
				this.AddHandler(Mouse.MouseWheelEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.MouseWheelEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.MouseWheel" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseWheelEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A32 RID: 2610 RVA: 0x0002A848 File Offset: 0x00029C48
		protected internal virtual void OnMouseWheel(MouseWheelEventArgs e)
		{
		}

		/// <summary>Ocorre quando o ponteiro do mouse entra nos limites deste elemento.</summary>
		// Token: 0x140000CC RID: 204
		// (add) Token: 0x06000A33 RID: 2611 RVA: 0x0002A858 File Offset: 0x00029C58
		// (remove) Token: 0x06000A34 RID: 2612 RVA: 0x0002A874 File Offset: 0x00029C74
		public event MouseEventHandler MouseEnter
		{
			add
			{
				this.AddHandler(Mouse.MouseEnterEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.MouseEnterEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.MouseEnter" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A35 RID: 2613 RVA: 0x0002A890 File Offset: 0x00029C90
		protected internal virtual void OnMouseEnter(MouseEventArgs e)
		{
		}

		/// <summary>Ocorre quando o ponteiro do mouse sai dos limites deste elemento.</summary>
		// Token: 0x140000CD RID: 205
		// (add) Token: 0x06000A36 RID: 2614 RVA: 0x0002A8A0 File Offset: 0x00029CA0
		// (remove) Token: 0x06000A37 RID: 2615 RVA: 0x0002A8BC File Offset: 0x00029CBC
		public event MouseEventHandler MouseLeave
		{
			add
			{
				this.AddHandler(Mouse.MouseLeaveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.MouseLeaveEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.MouseLeave" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A38 RID: 2616 RVA: 0x0002A8D8 File Offset: 0x00029CD8
		protected internal virtual void OnMouseLeave(MouseEventArgs e)
		{
		}

		/// <summary>Ocorre quando este elemento captura o mouse.</summary>
		// Token: 0x140000CE RID: 206
		// (add) Token: 0x06000A39 RID: 2617 RVA: 0x0002A8E8 File Offset: 0x00029CE8
		// (remove) Token: 0x06000A3A RID: 2618 RVA: 0x0002A904 File Offset: 0x00029D04
		public event MouseEventHandler GotMouseCapture
		{
			add
			{
				this.AddHandler(Mouse.GotMouseCaptureEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.GotMouseCaptureEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.GotMouseCapture" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A3B RID: 2619 RVA: 0x0002A920 File Offset: 0x00029D20
		protected internal virtual void OnGotMouseCapture(MouseEventArgs e)
		{
		}

		/// <summary>Ocorre quando este elemento perde a captura do mouse.</summary>
		// Token: 0x140000CF RID: 207
		// (add) Token: 0x06000A3C RID: 2620 RVA: 0x0002A930 File Offset: 0x00029D30
		// (remove) Token: 0x06000A3D RID: 2621 RVA: 0x0002A94C File Offset: 0x00029D4C
		public event MouseEventHandler LostMouseCapture
		{
			add
			{
				this.AddHandler(Mouse.LostMouseCaptureEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.LostMouseCaptureEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.LostMouseCapture" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseEventArgs" /> que contém dados do evento.</param>
		// Token: 0x06000A3E RID: 2622 RVA: 0x0002A968 File Offset: 0x00029D68
		protected internal virtual void OnLostMouseCapture(MouseEventArgs e)
		{
		}

		/// <summary>Ocorre quando a exibição do cursor é solicitada. Este evento é gerado em um elemento toda vez que o ponteiro do mouse se move para uma nova localização, o que significa que o objeto de cursor talvez precise ser alterado de acordo com sua nova posição.</summary>
		// Token: 0x140000D0 RID: 208
		// (add) Token: 0x06000A3F RID: 2623 RVA: 0x0002A978 File Offset: 0x00029D78
		// (remove) Token: 0x06000A40 RID: 2624 RVA: 0x0002A994 File Offset: 0x00029D94
		public event QueryCursorEventHandler QueryCursor
		{
			add
			{
				this.AddHandler(Mouse.QueryCursorEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.QueryCursorEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.QueryCursor" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.QueryCursorEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A41 RID: 2625 RVA: 0x0002A9B0 File Offset: 0x00029DB0
		protected internal virtual void OnQueryCursor(QueryCursorEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta toca o digitalizador enquanto está sobre este elemento.</summary>
		// Token: 0x140000D1 RID: 209
		// (add) Token: 0x06000A42 RID: 2626 RVA: 0x0002A9C0 File Offset: 0x00029DC0
		// (remove) Token: 0x06000A43 RID: 2627 RVA: 0x0002A9DC File Offset: 0x00029DDC
		public event StylusDownEventHandler PreviewStylusDown
		{
			add
			{
				this.AddHandler(Stylus.PreviewStylusDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.PreviewStylusDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusDownEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A44 RID: 2628 RVA: 0x0002A9F8 File Offset: 0x00029DF8
		protected internal virtual void OnPreviewStylusDown(StylusDownEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta toca o digitalizador enquanto está sobre este elemento.</summary>
		// Token: 0x140000D2 RID: 210
		// (add) Token: 0x06000A45 RID: 2629 RVA: 0x0002AA08 File Offset: 0x00029E08
		// (remove) Token: 0x06000A46 RID: 2630 RVA: 0x0002AA24 File Offset: 0x00029E24
		public event StylusDownEventHandler StylusDown
		{
			add
			{
				this.AddHandler(Stylus.StylusDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusDownEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A47 RID: 2631 RVA: 0x0002AA40 File Offset: 0x00029E40
		protected internal virtual void OnStylusDown(StylusDownEventArgs e)
		{
		}

		/// <summary>Ocorre quando o usuário retira a caneta do digitalizador enquanto ela está sobre esse elemento.</summary>
		// Token: 0x140000D3 RID: 211
		// (add) Token: 0x06000A48 RID: 2632 RVA: 0x0002AA50 File Offset: 0x00029E50
		// (remove) Token: 0x06000A49 RID: 2633 RVA: 0x0002AA6C File Offset: 0x00029E6C
		public event StylusEventHandler PreviewStylusUp
		{
			add
			{
				this.AddHandler(Stylus.PreviewStylusUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.PreviewStylusUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A4A RID: 2634 RVA: 0x0002AA88 File Offset: 0x00029E88
		protected internal virtual void OnPreviewStylusUp(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando o usuário retira a caneta do digitalizador enquanto ela está sobre este elemento.</summary>
		// Token: 0x140000D4 RID: 212
		// (add) Token: 0x06000A4B RID: 2635 RVA: 0x0002AA98 File Offset: 0x00029E98
		// (remove) Token: 0x06000A4C RID: 2636 RVA: 0x0002AAB4 File Offset: 0x00029EB4
		public event StylusEventHandler StylusUp
		{
			add
			{
				this.AddHandler(Stylus.StylusUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A4D RID: 2637 RVA: 0x0002AAD0 File Offset: 0x00029ED0
		protected internal virtual void OnStylusUp(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta se move enquanto está sobre o elemento. A caneta deverá se mover enquanto estiver sendo detectada pelo digitalizador para gerar este evento, caso contrário, <see cref="E:System.Windows.UIElement3D.PreviewStylusInAirMove" /> será gerado.</summary>
		// Token: 0x140000D5 RID: 213
		// (add) Token: 0x06000A4E RID: 2638 RVA: 0x0002AAE0 File Offset: 0x00029EE0
		// (remove) Token: 0x06000A4F RID: 2639 RVA: 0x0002AAFC File Offset: 0x00029EFC
		public event StylusEventHandler PreviewStylusMove
		{
			add
			{
				this.AddHandler(Stylus.PreviewStylusMoveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.PreviewStylusMoveEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusMove" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A50 RID: 2640 RVA: 0x0002AB18 File Offset: 0x00029F18
		protected internal virtual void OnPreviewStylusMove(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta se move sobre este elemento. A caneta deve mover-se enquanto está no digitalizador para gerar este evento. Caso contrário, <see cref="E:System.Windows.UIElement3D.StylusInAirMove" /> será gerado.</summary>
		// Token: 0x140000D6 RID: 214
		// (add) Token: 0x06000A51 RID: 2641 RVA: 0x0002AB28 File Offset: 0x00029F28
		// (remove) Token: 0x06000A52 RID: 2642 RVA: 0x0002AB44 File Offset: 0x00029F44
		public event StylusEventHandler StylusMove
		{
			add
			{
				this.AddHandler(Stylus.StylusMoveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusMoveEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusMove" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A53 RID: 2643 RVA: 0x0002AB60 File Offset: 0x00029F60
		protected internal virtual void OnStylusMove(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta se move sobre um elemento sem tocar de fato o digitalizador.</summary>
		// Token: 0x140000D7 RID: 215
		// (add) Token: 0x06000A54 RID: 2644 RVA: 0x0002AB70 File Offset: 0x00029F70
		// (remove) Token: 0x06000A55 RID: 2645 RVA: 0x0002AB8C File Offset: 0x00029F8C
		public event StylusEventHandler PreviewStylusInAirMove
		{
			add
			{
				this.AddHandler(Stylus.PreviewStylusInAirMoveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.PreviewStylusInAirMoveEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusInAirMove" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A56 RID: 2646 RVA: 0x0002ABA8 File Offset: 0x00029FA8
		protected internal virtual void OnPreviewStylusInAirMove(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta se move sobre um elemento sem tocar de fato o digitalizador.</summary>
		// Token: 0x140000D8 RID: 216
		// (add) Token: 0x06000A57 RID: 2647 RVA: 0x0002ABB8 File Offset: 0x00029FB8
		// (remove) Token: 0x06000A58 RID: 2648 RVA: 0x0002ABD4 File Offset: 0x00029FD4
		public event StylusEventHandler StylusInAirMove
		{
			add
			{
				this.AddHandler(Stylus.StylusInAirMoveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusInAirMoveEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusInAirMove" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A59 RID: 2649 RVA: 0x0002ABF0 File Offset: 0x00029FF0
		protected internal virtual void OnStylusInAirMove(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta entra nos limites deste elemento.</summary>
		// Token: 0x140000D9 RID: 217
		// (add) Token: 0x06000A5A RID: 2650 RVA: 0x0002AC00 File Offset: 0x0002A000
		// (remove) Token: 0x06000A5B RID: 2651 RVA: 0x0002AC1C File Offset: 0x0002A01C
		public event StylusEventHandler StylusEnter
		{
			add
			{
				this.AddHandler(Stylus.StylusEnterEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusEnterEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusEnter" /> sem tratamento é gerado por esse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A5C RID: 2652 RVA: 0x0002AC38 File Offset: 0x0002A038
		protected internal virtual void OnStylusEnter(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta sai dos limites do elemento.</summary>
		// Token: 0x140000DA RID: 218
		// (add) Token: 0x06000A5D RID: 2653 RVA: 0x0002AC48 File Offset: 0x0002A048
		// (remove) Token: 0x06000A5E RID: 2654 RVA: 0x0002AC64 File Offset: 0x0002A064
		public event StylusEventHandler StylusLeave
		{
			add
			{
				this.AddHandler(Stylus.StylusLeaveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusLeaveEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusLeave" /> sem tratamento é gerado por esse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A5F RID: 2655 RVA: 0x0002AC80 File Offset: 0x0002A080
		protected internal virtual void OnStylusLeave(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta está sobre este elemento e perto o suficiente do digitalizador para ser detectada.</summary>
		// Token: 0x140000DB RID: 219
		// (add) Token: 0x06000A60 RID: 2656 RVA: 0x0002AC90 File Offset: 0x0002A090
		// (remove) Token: 0x06000A61 RID: 2657 RVA: 0x0002ACAC File Offset: 0x0002A0AC
		public event StylusEventHandler PreviewStylusInRange
		{
			add
			{
				this.AddHandler(Stylus.PreviewStylusInRangeEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.PreviewStylusInRangeEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusInRange" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A62 RID: 2658 RVA: 0x0002ACC8 File Offset: 0x0002A0C8
		protected internal virtual void OnPreviewStylusInRange(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta está sobre este elemento e perto o suficiente do digitalizador para ser detectada.</summary>
		// Token: 0x140000DC RID: 220
		// (add) Token: 0x06000A63 RID: 2659 RVA: 0x0002ACD8 File Offset: 0x0002A0D8
		// (remove) Token: 0x06000A64 RID: 2660 RVA: 0x0002ACF4 File Offset: 0x0002A0F4
		public event StylusEventHandler StylusInRange
		{
			add
			{
				this.AddHandler(Stylus.StylusInRangeEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusInRangeEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusInRange" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A65 RID: 2661 RVA: 0x0002AD10 File Offset: 0x0002A110
		protected internal virtual void OnStylusInRange(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta está longe demais do digitalizador para ser detectada.</summary>
		// Token: 0x140000DD RID: 221
		// (add) Token: 0x06000A66 RID: 2662 RVA: 0x0002AD20 File Offset: 0x0002A120
		// (remove) Token: 0x06000A67 RID: 2663 RVA: 0x0002AD3C File Offset: 0x0002A13C
		public event StylusEventHandler PreviewStylusOutOfRange
		{
			add
			{
				this.AddHandler(Stylus.PreviewStylusOutOfRangeEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.PreviewStylusOutOfRangeEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusOutOfRange" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A68 RID: 2664 RVA: 0x0002AD58 File Offset: 0x0002A158
		protected internal virtual void OnPreviewStylusOutOfRange(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta está sobre o elemento e longe demais do digitalizador para ser detectada.</summary>
		// Token: 0x140000DE RID: 222
		// (add) Token: 0x06000A69 RID: 2665 RVA: 0x0002AD68 File Offset: 0x0002A168
		// (remove) Token: 0x06000A6A RID: 2666 RVA: 0x0002AD84 File Offset: 0x0002A184
		public event StylusEventHandler StylusOutOfRange
		{
			add
			{
				this.AddHandler(Stylus.StylusOutOfRangeEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusOutOfRangeEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusOutOfRange" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A6B RID: 2667 RVA: 0x0002ADA0 File Offset: 0x0002A1A0
		protected internal virtual void OnStylusOutOfRange(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando o usuário executa um dos diversos gestos da caneta.</summary>
		// Token: 0x140000DF RID: 223
		// (add) Token: 0x06000A6C RID: 2668 RVA: 0x0002ADB0 File Offset: 0x0002A1B0
		// (remove) Token: 0x06000A6D RID: 2669 RVA: 0x0002ADCC File Offset: 0x0002A1CC
		public event StylusSystemGestureEventHandler PreviewStylusSystemGesture
		{
			add
			{
				this.AddHandler(Stylus.PreviewStylusSystemGestureEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.PreviewStylusSystemGestureEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusSystemGesture" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusSystemGestureEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A6E RID: 2670 RVA: 0x0002ADE8 File Offset: 0x0002A1E8
		protected internal virtual void OnPreviewStylusSystemGesture(StylusSystemGestureEventArgs e)
		{
		}

		/// <summary>Ocorre quando o usuário executa um dos diversos gestos da caneta.</summary>
		// Token: 0x140000E0 RID: 224
		// (add) Token: 0x06000A6F RID: 2671 RVA: 0x0002ADF8 File Offset: 0x0002A1F8
		// (remove) Token: 0x06000A70 RID: 2672 RVA: 0x0002AE14 File Offset: 0x0002A214
		public event StylusSystemGestureEventHandler StylusSystemGesture
		{
			add
			{
				this.AddHandler(Stylus.StylusSystemGestureEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusSystemGestureEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusSystemGesture" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusSystemGestureEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A71 RID: 2673 RVA: 0x0002AE30 File Offset: 0x0002A230
		protected internal virtual void OnStylusSystemGesture(StylusSystemGestureEventArgs e)
		{
		}

		/// <summary>Ocorre quando este elemento captura a caneta.</summary>
		// Token: 0x140000E1 RID: 225
		// (add) Token: 0x06000A72 RID: 2674 RVA: 0x0002AE40 File Offset: 0x0002A240
		// (remove) Token: 0x06000A73 RID: 2675 RVA: 0x0002AE5C File Offset: 0x0002A25C
		public event StylusEventHandler GotStylusCapture
		{
			add
			{
				this.AddHandler(Stylus.GotStylusCaptureEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.GotStylusCaptureEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.GotStylusCapture" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A74 RID: 2676 RVA: 0x0002AE78 File Offset: 0x0002A278
		protected internal virtual void OnGotStylusCapture(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando este elemento perde a captura da caneta.</summary>
		// Token: 0x140000E2 RID: 226
		// (add) Token: 0x06000A75 RID: 2677 RVA: 0x0002AE88 File Offset: 0x0002A288
		// (remove) Token: 0x06000A76 RID: 2678 RVA: 0x0002AEA4 File Offset: 0x0002A2A4
		public event StylusEventHandler LostStylusCapture
		{
			add
			{
				this.AddHandler(Stylus.LostStylusCaptureEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.LostStylusCaptureEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.LostStylusCapture" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém dados do evento.</param>
		// Token: 0x06000A77 RID: 2679 RVA: 0x0002AEC0 File Offset: 0x0002A2C0
		protected internal virtual void OnLostStylusCapture(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão da caneta é pressionado enquanto o ponteiro está sobre este elemento.</summary>
		// Token: 0x140000E3 RID: 227
		// (add) Token: 0x06000A78 RID: 2680 RVA: 0x0002AED0 File Offset: 0x0002A2D0
		// (remove) Token: 0x06000A79 RID: 2681 RVA: 0x0002AEEC File Offset: 0x0002A2EC
		public event StylusButtonEventHandler StylusButtonDown
		{
			add
			{
				this.AddHandler(Stylus.StylusButtonDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusButtonDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusButtonDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusButtonEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A7A RID: 2682 RVA: 0x0002AF08 File Offset: 0x0002A308
		protected internal virtual void OnStylusButtonDown(StylusButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão da caneta é liberado enquanto o ponteiro está sobre este elemento.</summary>
		// Token: 0x140000E4 RID: 228
		// (add) Token: 0x06000A7B RID: 2683 RVA: 0x0002AF18 File Offset: 0x0002A318
		// (remove) Token: 0x06000A7C RID: 2684 RVA: 0x0002AF34 File Offset: 0x0002A334
		public event StylusButtonEventHandler StylusButtonUp
		{
			add
			{
				this.AddHandler(Stylus.StylusButtonUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusButtonUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusButtonUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusButtonEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A7D RID: 2685 RVA: 0x0002AF50 File Offset: 0x0002A350
		protected internal virtual void OnStylusButtonUp(StylusButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão da caneta é pressionado enquanto o ponteiro está sobre este elemento.</summary>
		// Token: 0x140000E5 RID: 229
		// (add) Token: 0x06000A7E RID: 2686 RVA: 0x0002AF60 File Offset: 0x0002A360
		// (remove) Token: 0x06000A7F RID: 2687 RVA: 0x0002AF7C File Offset: 0x0002A37C
		public event StylusButtonEventHandler PreviewStylusButtonDown
		{
			add
			{
				this.AddHandler(Stylus.PreviewStylusButtonDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.PreviewStylusButtonDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusButtonDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusButtonEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A80 RID: 2688 RVA: 0x0002AF98 File Offset: 0x0002A398
		protected internal virtual void OnPreviewStylusButtonDown(StylusButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão da caneta é liberado enquanto o ponteiro está sobre este elemento.</summary>
		// Token: 0x140000E6 RID: 230
		// (add) Token: 0x06000A81 RID: 2689 RVA: 0x0002AFA8 File Offset: 0x0002A3A8
		// (remove) Token: 0x06000A82 RID: 2690 RVA: 0x0002AFC4 File Offset: 0x0002A3C4
		public event StylusButtonEventHandler PreviewStylusButtonUp
		{
			add
			{
				this.AddHandler(Stylus.PreviewStylusButtonUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.PreviewStylusButtonUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusButtonUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusButtonEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A83 RID: 2691 RVA: 0x0002AFE0 File Offset: 0x0002A3E0
		protected internal virtual void OnPreviewStylusButtonUp(StylusButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando uma tecla é pressionada enquanto o teclado está focalizado neste elemento.</summary>
		// Token: 0x140000E7 RID: 231
		// (add) Token: 0x06000A84 RID: 2692 RVA: 0x0002AFF0 File Offset: 0x0002A3F0
		// (remove) Token: 0x06000A85 RID: 2693 RVA: 0x0002B00C File Offset: 0x0002A40C
		public event KeyEventHandler PreviewKeyDown
		{
			add
			{
				this.AddHandler(Keyboard.PreviewKeyDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Keyboard.PreviewKeyDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Keyboard.PreviewKeyDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.KeyEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A86 RID: 2694 RVA: 0x0002B028 File Offset: 0x0002A428
		protected internal virtual void OnPreviewKeyDown(KeyEventArgs e)
		{
		}

		/// <summary>Ocorre quando uma tecla é pressionada enquanto o teclado está focalizado neste elemento.</summary>
		// Token: 0x140000E8 RID: 232
		// (add) Token: 0x06000A87 RID: 2695 RVA: 0x0002B038 File Offset: 0x0002A438
		// (remove) Token: 0x06000A88 RID: 2696 RVA: 0x0002B054 File Offset: 0x0002A454
		public event KeyEventHandler KeyDown
		{
			add
			{
				this.AddHandler(Keyboard.KeyDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Keyboard.KeyDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Keyboard.KeyDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.KeyEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A89 RID: 2697 RVA: 0x0002B070 File Offset: 0x0002A470
		protected internal virtual void OnKeyDown(KeyEventArgs e)
		{
		}

		/// <summary>Ocorre quando uma tecla é liberada enquanto o teclado está focalizado neste elemento.</summary>
		// Token: 0x140000E9 RID: 233
		// (add) Token: 0x06000A8A RID: 2698 RVA: 0x0002B080 File Offset: 0x0002A480
		// (remove) Token: 0x06000A8B RID: 2699 RVA: 0x0002B09C File Offset: 0x0002A49C
		public event KeyEventHandler PreviewKeyUp
		{
			add
			{
				this.AddHandler(Keyboard.PreviewKeyUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Keyboard.PreviewKeyUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Keyboard.PreviewKeyUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.KeyEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A8C RID: 2700 RVA: 0x0002B0B8 File Offset: 0x0002A4B8
		protected internal virtual void OnPreviewKeyUp(KeyEventArgs e)
		{
		}

		/// <summary>Ocorre quando uma tecla é liberada enquanto o teclado está focalizado neste elemento.</summary>
		// Token: 0x140000EA RID: 234
		// (add) Token: 0x06000A8D RID: 2701 RVA: 0x0002B0C8 File Offset: 0x0002A4C8
		// (remove) Token: 0x06000A8E RID: 2702 RVA: 0x0002B0E4 File Offset: 0x0002A4E4
		public event KeyEventHandler KeyUp
		{
			add
			{
				this.AddHandler(Keyboard.KeyUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Keyboard.KeyUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Keyboard.KeyUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.KeyEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A8F RID: 2703 RVA: 0x0002B100 File Offset: 0x0002A500
		protected internal virtual void OnKeyUp(KeyEventArgs e)
		{
		}

		/// <summary>Ocorre quando o teclado está focalizado neste elemento.</summary>
		// Token: 0x140000EB RID: 235
		// (add) Token: 0x06000A90 RID: 2704 RVA: 0x0002B110 File Offset: 0x0002A510
		// (remove) Token: 0x06000A91 RID: 2705 RVA: 0x0002B12C File Offset: 0x0002A52C
		public event KeyboardFocusChangedEventHandler PreviewGotKeyboardFocus
		{
			add
			{
				this.AddHandler(Keyboard.PreviewGotKeyboardFocusEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Keyboard.PreviewGotKeyboardFocusEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Keyboard.PreviewGotKeyboardFocus" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.KeyboardFocusChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A92 RID: 2706 RVA: 0x0002B148 File Offset: 0x0002A548
		protected internal virtual void OnPreviewGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
		{
		}

		/// <summary>Ocorre quando o teclado está focalizado neste elemento.</summary>
		// Token: 0x140000EC RID: 236
		// (add) Token: 0x06000A93 RID: 2707 RVA: 0x0002B158 File Offset: 0x0002A558
		// (remove) Token: 0x06000A94 RID: 2708 RVA: 0x0002B174 File Offset: 0x0002A574
		public event KeyboardFocusChangedEventHandler GotKeyboardFocus
		{
			add
			{
				this.AddHandler(Keyboard.GotKeyboardFocusEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Keyboard.GotKeyboardFocusEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Keyboard.GotKeyboardFocus" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.KeyboardFocusChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A95 RID: 2709 RVA: 0x0002B190 File Offset: 0x0002A590
		protected internal virtual void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
		{
		}

		/// <summary>Ocorre quando o teclado não está mais focalizado neste elemento.</summary>
		// Token: 0x140000ED RID: 237
		// (add) Token: 0x06000A96 RID: 2710 RVA: 0x0002B1A0 File Offset: 0x0002A5A0
		// (remove) Token: 0x06000A97 RID: 2711 RVA: 0x0002B1BC File Offset: 0x0002A5BC
		public event KeyboardFocusChangedEventHandler PreviewLostKeyboardFocus
		{
			add
			{
				this.AddHandler(Keyboard.PreviewLostKeyboardFocusEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Keyboard.PreviewLostKeyboardFocusEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Keyboard.PreviewLostKeyboardFocus" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.KeyboardFocusChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A98 RID: 2712 RVA: 0x0002B1D8 File Offset: 0x0002A5D8
		protected internal virtual void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
		{
		}

		/// <summary>Ocorre quando o teclado não está mais focalizado neste elemento.</summary>
		// Token: 0x140000EE RID: 238
		// (add) Token: 0x06000A99 RID: 2713 RVA: 0x0002B1E8 File Offset: 0x0002A5E8
		// (remove) Token: 0x06000A9A RID: 2714 RVA: 0x0002B204 File Offset: 0x0002A604
		public event KeyboardFocusChangedEventHandler LostKeyboardFocus
		{
			add
			{
				this.AddHandler(Keyboard.LostKeyboardFocusEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Keyboard.LostKeyboardFocusEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Keyboard.LostKeyboardFocus" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.KeyboardFocusChangedEventArgs" /> que contém dados do evento.</param>
		// Token: 0x06000A9B RID: 2715 RVA: 0x0002B220 File Offset: 0x0002A620
		protected internal virtual void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
		{
		}

		/// <summary>Ocorre quando este elemento obtém texto de forma independente de dispositivo.</summary>
		// Token: 0x140000EF RID: 239
		// (add) Token: 0x06000A9C RID: 2716 RVA: 0x0002B230 File Offset: 0x0002A630
		// (remove) Token: 0x06000A9D RID: 2717 RVA: 0x0002B24C File Offset: 0x0002A64C
		public event TextCompositionEventHandler PreviewTextInput
		{
			add
			{
				this.AddHandler(TextCompositionManager.PreviewTextInputEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(TextCompositionManager.PreviewTextInputEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.TextCompositionManager.PreviewTextInput" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.TextCompositionEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000A9E RID: 2718 RVA: 0x0002B268 File Offset: 0x0002A668
		protected internal virtual void OnPreviewTextInput(TextCompositionEventArgs e)
		{
		}

		/// <summary>Ocorre quando este elemento obtém texto de forma independente de dispositivo.</summary>
		// Token: 0x140000F0 RID: 240
		// (add) Token: 0x06000A9F RID: 2719 RVA: 0x0002B278 File Offset: 0x0002A678
		// (remove) Token: 0x06000AA0 RID: 2720 RVA: 0x0002B294 File Offset: 0x0002A694
		public event TextCompositionEventHandler TextInput
		{
			add
			{
				this.AddHandler(TextCompositionManager.TextInputEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(TextCompositionManager.TextInputEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.TextCompositionManager.TextInput" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.TextCompositionEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AA1 RID: 2721 RVA: 0x0002B2B0 File Offset: 0x0002A6B0
		protected internal virtual void OnTextInput(TextCompositionEventArgs e)
		{
		}

		/// <summary>Ocorre quando há uma alteração no estado do botão do teclado ou do mouse durante uma operação de arrastar e soltar.</summary>
		// Token: 0x140000F1 RID: 241
		// (add) Token: 0x06000AA2 RID: 2722 RVA: 0x0002B2C0 File Offset: 0x0002A6C0
		// (remove) Token: 0x06000AA3 RID: 2723 RVA: 0x0002B2DC File Offset: 0x0002A6DC
		public event QueryContinueDragEventHandler PreviewQueryContinueDrag
		{
			add
			{
				this.AddHandler(DragDrop.PreviewQueryContinueDragEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.PreviewQueryContinueDragEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.PreviewQueryContinueDrag" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.QueryContinueDragEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002B2F8 File Offset: 0x0002A6F8
		protected internal virtual void OnPreviewQueryContinueDrag(QueryContinueDragEventArgs e)
		{
		}

		/// <summary>Ocorre quando há uma alteração no estado do botão do teclado ou do mouse durante uma operação de arrastar e soltar.</summary>
		// Token: 0x140000F2 RID: 242
		// (add) Token: 0x06000AA5 RID: 2725 RVA: 0x0002B308 File Offset: 0x0002A708
		// (remove) Token: 0x06000AA6 RID: 2726 RVA: 0x0002B324 File Offset: 0x0002A724
		public event QueryContinueDragEventHandler QueryContinueDrag
		{
			add
			{
				this.AddHandler(DragDrop.QueryContinueDragEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.QueryContinueDragEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.QueryContinueDrag" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.QueryContinueDragEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002B340 File Offset: 0x0002A740
		protected internal virtual void OnQueryContinueDrag(QueryContinueDragEventArgs e)
		{
		}

		/// <summary>Ocorre quando uma operação de arrastar e soltar se inicia.</summary>
		// Token: 0x140000F3 RID: 243
		// (add) Token: 0x06000AA8 RID: 2728 RVA: 0x0002B350 File Offset: 0x0002A750
		// (remove) Token: 0x06000AA9 RID: 2729 RVA: 0x0002B36C File Offset: 0x0002A76C
		public event GiveFeedbackEventHandler PreviewGiveFeedback
		{
			add
			{
				this.AddHandler(DragDrop.PreviewGiveFeedbackEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.PreviewGiveFeedbackEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.PreviewGiveFeedback" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.GiveFeedbackEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AAA RID: 2730 RVA: 0x0002B388 File Offset: 0x0002A788
		protected internal virtual void OnPreviewGiveFeedback(GiveFeedbackEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento de arrastar e soltar subjacente que envolve este elemento.</summary>
		// Token: 0x140000F4 RID: 244
		// (add) Token: 0x06000AAB RID: 2731 RVA: 0x0002B398 File Offset: 0x0002A798
		// (remove) Token: 0x06000AAC RID: 2732 RVA: 0x0002B3B4 File Offset: 0x0002A7B4
		public event GiveFeedbackEventHandler GiveFeedback
		{
			add
			{
				this.AddHandler(DragDrop.GiveFeedbackEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.GiveFeedbackEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.GiveFeedback" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.GiveFeedbackEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AAD RID: 2733 RVA: 0x0002B3D0 File Offset: 0x0002A7D0
		protected internal virtual void OnGiveFeedback(GiveFeedbackEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento arrastar subjacente com este elemento como o destino de arrastar.</summary>
		// Token: 0x140000F5 RID: 245
		// (add) Token: 0x06000AAE RID: 2734 RVA: 0x0002B3E0 File Offset: 0x0002A7E0
		// (remove) Token: 0x06000AAF RID: 2735 RVA: 0x0002B3FC File Offset: 0x0002A7FC
		public event DragEventHandler PreviewDragEnter
		{
			add
			{
				this.AddHandler(DragDrop.PreviewDragEnterEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.PreviewDragEnterEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.PreviewDragEnter" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DragEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002B418 File Offset: 0x0002A818
		protected internal virtual void OnPreviewDragEnter(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento arrastar subjacente com este elemento como o destino de arrastar.</summary>
		// Token: 0x140000F6 RID: 246
		// (add) Token: 0x06000AB1 RID: 2737 RVA: 0x0002B428 File Offset: 0x0002A828
		// (remove) Token: 0x06000AB2 RID: 2738 RVA: 0x0002B444 File Offset: 0x0002A844
		public event DragEventHandler DragEnter
		{
			add
			{
				this.AddHandler(DragDrop.DragEnterEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.DragEnterEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.DragEnter" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DragEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AB3 RID: 2739 RVA: 0x0002B460 File Offset: 0x0002A860
		protected internal virtual void OnDragEnter(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento do tipo "arrastar" subjacente com esse elemento como a reprodução automática potencial.</summary>
		// Token: 0x140000F7 RID: 247
		// (add) Token: 0x06000AB4 RID: 2740 RVA: 0x0002B470 File Offset: 0x0002A870
		// (remove) Token: 0x06000AB5 RID: 2741 RVA: 0x0002B48C File Offset: 0x0002A88C
		public event DragEventHandler PreviewDragOver
		{
			add
			{
				this.AddHandler(DragDrop.PreviewDragOverEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.PreviewDragOverEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.PreviewDragOver" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DragEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AB6 RID: 2742 RVA: 0x0002B4A8 File Offset: 0x0002A8A8
		protected internal virtual void OnPreviewDragOver(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento do tipo "arrastar" subjacente com esse elemento como a reprodução automática potencial.</summary>
		// Token: 0x140000F8 RID: 248
		// (add) Token: 0x06000AB7 RID: 2743 RVA: 0x0002B4B8 File Offset: 0x0002A8B8
		// (remove) Token: 0x06000AB8 RID: 2744 RVA: 0x0002B4D4 File Offset: 0x0002A8D4
		public event DragEventHandler DragOver
		{
			add
			{
				this.AddHandler(DragDrop.DragOverEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.DragOverEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.DragOver" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DragEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002B4F0 File Offset: 0x0002A8F0
		protected internal virtual void OnDragOver(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento arrastar subjacente com este elemento como a origem de arrastar.</summary>
		// Token: 0x140000F9 RID: 249
		// (add) Token: 0x06000ABA RID: 2746 RVA: 0x0002B500 File Offset: 0x0002A900
		// (remove) Token: 0x06000ABB RID: 2747 RVA: 0x0002B51C File Offset: 0x0002A91C
		public event DragEventHandler PreviewDragLeave
		{
			add
			{
				this.AddHandler(DragDrop.PreviewDragLeaveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.PreviewDragLeaveEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.PreviewDragLeave" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DragEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000ABC RID: 2748 RVA: 0x0002B538 File Offset: 0x0002A938
		protected internal virtual void OnPreviewDragLeave(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento arrastar subjacente com este elemento como a origem de arrastar.</summary>
		// Token: 0x140000FA RID: 250
		// (add) Token: 0x06000ABD RID: 2749 RVA: 0x0002B548 File Offset: 0x0002A948
		// (remove) Token: 0x06000ABE RID: 2750 RVA: 0x0002B564 File Offset: 0x0002A964
		public event DragEventHandler DragLeave
		{
			add
			{
				this.AddHandler(DragDrop.DragLeaveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.DragLeaveEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.DragLeave" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DragEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000ABF RID: 2751 RVA: 0x0002B580 File Offset: 0x0002A980
		protected internal virtual void OnDragLeave(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento soltar subjacente com esse elemento sendo uma reprodução automática.</summary>
		// Token: 0x140000FB RID: 251
		// (add) Token: 0x06000AC0 RID: 2752 RVA: 0x0002B590 File Offset: 0x0002A990
		// (remove) Token: 0x06000AC1 RID: 2753 RVA: 0x0002B5AC File Offset: 0x0002A9AC
		public event DragEventHandler PreviewDrop
		{
			add
			{
				this.AddHandler(DragDrop.PreviewDropEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.PreviewDropEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.PreviewDrop" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DragEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AC2 RID: 2754 RVA: 0x0002B5C8 File Offset: 0x0002A9C8
		protected internal virtual void OnPreviewDrop(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento soltar subjacente com esse elemento sendo uma reprodução automática.</summary>
		// Token: 0x140000FC RID: 252
		// (add) Token: 0x06000AC3 RID: 2755 RVA: 0x0002B5D8 File Offset: 0x0002A9D8
		// (remove) Token: 0x06000AC4 RID: 2756 RVA: 0x0002B5F4 File Offset: 0x0002A9F4
		public event DragEventHandler Drop
		{
			add
			{
				this.AddHandler(DragDrop.DropEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.DropEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.Drop" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DragEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002B610 File Offset: 0x0002AA10
		protected internal virtual void OnDrop(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando um dedo toca a tela enquanto está sobre esse elemento.</summary>
		// Token: 0x140000FD RID: 253
		// (add) Token: 0x06000AC6 RID: 2758 RVA: 0x0002B620 File Offset: 0x0002AA20
		// (remove) Token: 0x06000AC7 RID: 2759 RVA: 0x0002B63C File Offset: 0x0002AA3C
		[CustomCategory("Touch_Category")]
		public event EventHandler<TouchEventArgs> PreviewTouchDown
		{
			add
			{
				this.AddHandler(Touch.PreviewTouchDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Touch.PreviewTouchDownEvent, value);
			}
		}

		/// <summary>Fornece tratamento de classes para os eventos roteados <see cref="E:System.Windows.UIElement3D.PreviewTouchDown" /> que ocorrem quando um toque pressiona esse elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AC8 RID: 2760 RVA: 0x0002B658 File Offset: 0x0002AA58
		protected internal virtual void OnPreviewTouchDown(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um dedo toca a tela enquanto está sobre esse elemento.</summary>
		// Token: 0x140000FE RID: 254
		// (add) Token: 0x06000AC9 RID: 2761 RVA: 0x0002B668 File Offset: 0x0002AA68
		// (remove) Token: 0x06000ACA RID: 2762 RVA: 0x0002B684 File Offset: 0x0002AA84
		[CustomCategory("Touch_Category")]
		public event EventHandler<TouchEventArgs> TouchDown
		{
			add
			{
				this.AddHandler(Touch.TouchDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Touch.TouchDownEvent, value);
			}
		}

		/// <summary>Fornece tratamento de classes para os eventos roteados <see cref="E:System.Windows.UIElement3D.TouchDown" /> que ocorrem quando há um toque nesse elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000ACB RID: 2763 RVA: 0x0002B6A0 File Offset: 0x0002AAA0
		protected internal virtual void OnTouchDown(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um dedo se move na tela enquanto está sobre esse elemento.</summary>
		// Token: 0x140000FF RID: 255
		// (add) Token: 0x06000ACC RID: 2764 RVA: 0x0002B6B0 File Offset: 0x0002AAB0
		// (remove) Token: 0x06000ACD RID: 2765 RVA: 0x0002B6CC File Offset: 0x0002AACC
		[CustomCategory("Touch_Category")]
		public event EventHandler<TouchEventArgs> PreviewTouchMove
		{
			add
			{
				this.AddHandler(Touch.PreviewTouchMoveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Touch.PreviewTouchMoveEvent, value);
			}
		}

		/// <summary>Fornece manipulação de classes para o evento roteado <see cref="E:System.Windows.UIElement3D.PreviewTouchMove" /> que ocorre quando há uma movimentação de toque nesse elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000ACE RID: 2766 RVA: 0x0002B6E8 File Offset: 0x0002AAE8
		protected internal virtual void OnPreviewTouchMove(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um dedo se move na tela enquanto está sobre esse elemento.</summary>
		// Token: 0x14000100 RID: 256
		// (add) Token: 0x06000ACF RID: 2767 RVA: 0x0002B6F8 File Offset: 0x0002AAF8
		// (remove) Token: 0x06000AD0 RID: 2768 RVA: 0x0002B714 File Offset: 0x0002AB14
		[CustomCategory("Touch_Category")]
		public event EventHandler<TouchEventArgs> TouchMove
		{
			add
			{
				this.AddHandler(Touch.TouchMoveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Touch.TouchMoveEvent, value);
			}
		}

		/// <summary>Fornece manipulação de classes para o evento roteado <see cref="E:System.Windows.UIElement3D.TouchMove" /> que ocorre quando há uma movimentação de toque nesse elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AD1 RID: 2769 RVA: 0x0002B730 File Offset: 0x0002AB30
		protected internal virtual void OnTouchMove(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um dedo é gerado fora da tela enquanto o dedo está sobre este elemento.</summary>
		// Token: 0x14000101 RID: 257
		// (add) Token: 0x06000AD2 RID: 2770 RVA: 0x0002B740 File Offset: 0x0002AB40
		// (remove) Token: 0x06000AD3 RID: 2771 RVA: 0x0002B75C File Offset: 0x0002AB5C
		[CustomCategory("Touch_Category")]
		public event EventHandler<TouchEventArgs> PreviewTouchUp
		{
			add
			{
				this.AddHandler(Touch.PreviewTouchUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Touch.PreviewTouchUpEvent, value);
			}
		}

		/// <summary>Fornece tratamento de classe para os eventos roteados <see cref="E:System.Windows.UIElement3D.PreviewTouchUp" /> que ocorrem quando um toque é liberado dentro desse elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AD4 RID: 2772 RVA: 0x0002B778 File Offset: 0x0002AB78
		protected internal virtual void OnPreviewTouchUp(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um dedo é gerado fora da tela enquanto o dedo está sobre este elemento.</summary>
		// Token: 0x14000102 RID: 258
		// (add) Token: 0x06000AD5 RID: 2773 RVA: 0x0002B788 File Offset: 0x0002AB88
		// (remove) Token: 0x06000AD6 RID: 2774 RVA: 0x0002B7A4 File Offset: 0x0002ABA4
		[CustomCategory("Touch_Category")]
		public event EventHandler<TouchEventArgs> TouchUp
		{
			add
			{
				this.AddHandler(Touch.TouchUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Touch.TouchUpEvent, value);
			}
		}

		/// <summary>Fornece tratamento de classe para os eventos roteados <see cref="E:System.Windows.UIElement3D.TouchUp" /> que ocorrem quando um toque é liberado dentro desse elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002B7C0 File Offset: 0x0002ABC0
		protected internal virtual void OnTouchUp(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um toque é capturado para esse elemento.</summary>
		// Token: 0x14000103 RID: 259
		// (add) Token: 0x06000AD8 RID: 2776 RVA: 0x0002B7D0 File Offset: 0x0002ABD0
		// (remove) Token: 0x06000AD9 RID: 2777 RVA: 0x0002B7EC File Offset: 0x0002ABEC
		[CustomCategory("Touch_Category")]
		public event EventHandler<TouchEventArgs> GotTouchCapture
		{
			add
			{
				this.AddHandler(Touch.GotTouchCaptureEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Touch.GotTouchCaptureEvent, value);
			}
		}

		/// <summary>Fornece tratamento de classes para os eventos roteados <see cref="E:System.Windows.UIElement3D.GotTouchCapture" /> que ocorrem quando um toque é capturado para esse elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000ADA RID: 2778 RVA: 0x0002B808 File Offset: 0x0002AC08
		protected internal virtual void OnGotTouchCapture(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando este elemento perde a captura do toque.</summary>
		// Token: 0x14000104 RID: 260
		// (add) Token: 0x06000ADB RID: 2779 RVA: 0x0002B818 File Offset: 0x0002AC18
		// (remove) Token: 0x06000ADC RID: 2780 RVA: 0x0002B834 File Offset: 0x0002AC34
		[CustomCategory("Touch_Category")]
		public event EventHandler<TouchEventArgs> LostTouchCapture
		{
			add
			{
				this.AddHandler(Touch.LostTouchCaptureEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Touch.LostTouchCaptureEvent, value);
			}
		}

		/// <summary>Fornece tratamento de classes para o evento roteado <see cref="E:System.Windows.UIElement3D.LostTouchCapture" /> que ocorre quando este elemento perde a captura de toque.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000ADD RID: 2781 RVA: 0x0002B850 File Offset: 0x0002AC50
		protected internal virtual void OnLostTouchCapture(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um toque é movido de fora para dentro dos limites deste elemento.</summary>
		// Token: 0x14000105 RID: 261
		// (add) Token: 0x06000ADE RID: 2782 RVA: 0x0002B860 File Offset: 0x0002AC60
		// (remove) Token: 0x06000ADF RID: 2783 RVA: 0x0002B87C File Offset: 0x0002AC7C
		[CustomCategory("Touch_Category")]
		public event EventHandler<TouchEventArgs> TouchEnter
		{
			add
			{
				this.AddHandler(Touch.TouchEnterEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Touch.TouchEnterEvent, value);
			}
		}

		/// <summary>Fornece tratamento de classes para os eventos roteados de <see cref="E:System.Windows.UIElement3D.TouchEnter" /> que ocorre quando um toque é movido de fora para dentro dos limites deste elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002B898 File Offset: 0x0002AC98
		protected internal virtual void OnTouchEnter(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um toque é movido de dentro para fora dos limites deste elemento.</summary>
		// Token: 0x14000106 RID: 262
		// (add) Token: 0x06000AE1 RID: 2785 RVA: 0x0002B8A8 File Offset: 0x0002ACA8
		// (remove) Token: 0x06000AE2 RID: 2786 RVA: 0x0002B8C4 File Offset: 0x0002ACC4
		[CustomCategory("Touch_Category")]
		public event EventHandler<TouchEventArgs> TouchLeave
		{
			add
			{
				this.AddHandler(Touch.TouchLeaveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Touch.TouchLeaveEvent, value);
			}
		}

		/// <summary>Fornece tratamento de classe para o evento roteado <see cref="E:System.Windows.UIElement3D.TouchLeave" /> que ocorre quando um toque é movido de dentro para fora dos limites deste elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002B8E0 File Offset: 0x0002ACE0
		protected internal virtual void OnTouchLeave(TouchEventArgs e)
		{
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002B8F0 File Offset: 0x0002ACF0
		private static void IsMouseDirectlyOver_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((UIElement3D)d).RaiseIsMouseDirectlyOverChanged(e);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.UIElement3D.IsMouseDirectlyOver" /> é alterado neste elemento.</summary>
		// Token: 0x14000107 RID: 263
		// (add) Token: 0x06000AE5 RID: 2789 RVA: 0x0002B90C File Offset: 0x0002AD0C
		// (remove) Token: 0x06000AE6 RID: 2790 RVA: 0x0002B928 File Offset: 0x0002AD28
		public event DependencyPropertyChangedEventHandler IsMouseDirectlyOverChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.IsMouseDirectlyOverChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.IsMouseDirectlyOverChangedKey, value);
			}
		}

		/// <summary>Invocado quando um evento <see cref="E:System.Windows.UIElement3D.IsMouseDirectlyOverChanged" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002B944 File Offset: 0x0002AD44
		protected virtual void OnIsMouseDirectlyOverChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002B954 File Offset: 0x0002AD54
		private void RaiseIsMouseDirectlyOverChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsMouseDirectlyOverChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsMouseDirectlyOverChangedKey, args);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.UIElement3D.IsKeyboardFocusWithin" /> é alterado neste elemento.</summary>
		// Token: 0x14000108 RID: 264
		// (add) Token: 0x06000AE9 RID: 2793 RVA: 0x0002B974 File Offset: 0x0002AD74
		// (remove) Token: 0x06000AEA RID: 2794 RVA: 0x0002B990 File Offset: 0x0002AD90
		public event DependencyPropertyChangedEventHandler IsKeyboardFocusWithinChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.IsKeyboardFocusWithinChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.IsKeyboardFocusWithinChangedKey, value);
			}
		}

		/// <summary>Invocado pouco antes do evento <see cref="E:System.Windows.UIElement3D.IsKeyboardFocusWithinChanged" /> ser gerado por este elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AEB RID: 2795 RVA: 0x0002B9AC File Offset: 0x0002ADAC
		protected virtual void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0002B9BC File Offset: 0x0002ADBC
		internal void RaiseIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsKeyboardFocusWithinChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsKeyboardFocusWithinChangedKey, args);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002B9DC File Offset: 0x0002ADDC
		private static void IsMouseCaptured_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((UIElement3D)d).RaiseIsMouseCapturedChanged(e);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.UIElement3D.IsMouseCaptured" /> é alterado neste elemento.</summary>
		// Token: 0x14000109 RID: 265
		// (add) Token: 0x06000AEE RID: 2798 RVA: 0x0002B9F8 File Offset: 0x0002ADF8
		// (remove) Token: 0x06000AEF RID: 2799 RVA: 0x0002BA14 File Offset: 0x0002AE14
		public event DependencyPropertyChangedEventHandler IsMouseCapturedChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.IsMouseCapturedChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.IsMouseCapturedChangedKey, value);
			}
		}

		/// <summary>Invocado quando um evento <see cref="E:System.Windows.UIElement3D.IsMouseCapturedChanged" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002BA30 File Offset: 0x0002AE30
		protected virtual void OnIsMouseCapturedChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002BA40 File Offset: 0x0002AE40
		private void RaiseIsMouseCapturedChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsMouseCapturedChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsMouseCapturedChangedKey, args);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.UIElement3D.IsMouseCaptureWithin" /> é alterado neste elemento.</summary>
		// Token: 0x1400010A RID: 266
		// (add) Token: 0x06000AF2 RID: 2802 RVA: 0x0002BA60 File Offset: 0x0002AE60
		// (remove) Token: 0x06000AF3 RID: 2803 RVA: 0x0002BA7C File Offset: 0x0002AE7C
		public event DependencyPropertyChangedEventHandler IsMouseCaptureWithinChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.IsMouseCaptureWithinChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.IsMouseCaptureWithinChangedKey, value);
			}
		}

		/// <summary>Invocado quando um evento <see cref="E:System.Windows.UIElement3D.IsMouseCaptureWithinChanged" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002BA98 File Offset: 0x0002AE98
		protected virtual void OnIsMouseCaptureWithinChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002BAA8 File Offset: 0x0002AEA8
		internal void RaiseIsMouseCaptureWithinChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsMouseCaptureWithinChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsMouseCaptureWithinChangedKey, args);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002BAC8 File Offset: 0x0002AEC8
		private static void IsStylusDirectlyOver_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((UIElement3D)d).RaiseIsStylusDirectlyOverChanged(e);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.UIElement3D.IsStylusDirectlyOver" /> é alterado neste elemento.</summary>
		// Token: 0x1400010B RID: 267
		// (add) Token: 0x06000AF7 RID: 2807 RVA: 0x0002BAE4 File Offset: 0x0002AEE4
		// (remove) Token: 0x06000AF8 RID: 2808 RVA: 0x0002BB00 File Offset: 0x0002AF00
		public event DependencyPropertyChangedEventHandler IsStylusDirectlyOverChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.IsStylusDirectlyOverChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.IsStylusDirectlyOverChangedKey, value);
			}
		}

		/// <summary>Invocado quando um evento <see cref="E:System.Windows.UIElement3D.IsStylusDirectlyOverChanged" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AF9 RID: 2809 RVA: 0x0002BB1C File Offset: 0x0002AF1C
		protected virtual void OnIsStylusDirectlyOverChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0002BB2C File Offset: 0x0002AF2C
		private void RaiseIsStylusDirectlyOverChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsStylusDirectlyOverChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsStylusDirectlyOverChangedKey, args);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0002BB4C File Offset: 0x0002AF4C
		private static void IsStylusCaptured_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((UIElement3D)d).RaiseIsStylusCapturedChanged(e);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.UIElement3D.IsStylusCaptured" /> é alterado neste elemento.</summary>
		// Token: 0x1400010C RID: 268
		// (add) Token: 0x06000AFC RID: 2812 RVA: 0x0002BB68 File Offset: 0x0002AF68
		// (remove) Token: 0x06000AFD RID: 2813 RVA: 0x0002BB84 File Offset: 0x0002AF84
		public event DependencyPropertyChangedEventHandler IsStylusCapturedChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.IsStylusCapturedChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.IsStylusCapturedChangedKey, value);
			}
		}

		/// <summary>Invocado quando um evento <see cref="E:System.Windows.UIElement3D.IsStylusCapturedChanged" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000AFE RID: 2814 RVA: 0x0002BBA0 File Offset: 0x0002AFA0
		protected virtual void OnIsStylusCapturedChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002BBB0 File Offset: 0x0002AFB0
		private void RaiseIsStylusCapturedChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsStylusCapturedChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsStylusCapturedChangedKey, args);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.UIElement3D.IsStylusCaptureWithin" /> é alterado neste elemento.</summary>
		// Token: 0x1400010D RID: 269
		// (add) Token: 0x06000B00 RID: 2816 RVA: 0x0002BBD0 File Offset: 0x0002AFD0
		// (remove) Token: 0x06000B01 RID: 2817 RVA: 0x0002BBEC File Offset: 0x0002AFEC
		public event DependencyPropertyChangedEventHandler IsStylusCaptureWithinChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.IsStylusCaptureWithinChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.IsStylusCaptureWithinChangedKey, value);
			}
		}

		/// <summary>Invocado quando um evento <see cref="E:System.Windows.UIElement3D.IsStylusCaptureWithinChanged" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000B02 RID: 2818 RVA: 0x0002BC08 File Offset: 0x0002B008
		protected virtual void OnIsStylusCaptureWithinChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0002BC18 File Offset: 0x0002B018
		internal void RaiseIsStylusCaptureWithinChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsStylusCaptureWithinChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsStylusCaptureWithinChangedKey, args);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0002BC38 File Offset: 0x0002B038
		private static void IsKeyboardFocused_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((UIElement3D)d).RaiseIsKeyboardFocusedChanged(e);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.UIElement3D.IsKeyboardFocused" /> é alterado neste elemento.</summary>
		// Token: 0x1400010E RID: 270
		// (add) Token: 0x06000B05 RID: 2821 RVA: 0x0002BC54 File Offset: 0x0002B054
		// (remove) Token: 0x06000B06 RID: 2822 RVA: 0x0002BC70 File Offset: 0x0002B070
		public event DependencyPropertyChangedEventHandler IsKeyboardFocusedChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.IsKeyboardFocusedChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.IsKeyboardFocusedChangedKey, value);
			}
		}

		/// <summary>Invocado quando um evento <see cref="E:System.Windows.UIElement3D.IsKeyboardFocusedChanged" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000B07 RID: 2823 RVA: 0x0002BC8C File Offset: 0x0002B08C
		protected virtual void OnIsKeyboardFocusedChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0002BC9C File Offset: 0x0002B09C
		private void RaiseIsKeyboardFocusedChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsKeyboardFocusedChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsKeyboardFocusedChangedKey, args);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002BCBC File Offset: 0x0002B0BC
		internal bool ReadFlag(CoreFlags field)
		{
			return (this._flags & field) > CoreFlags.None;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0002BCD4 File Offset: 0x0002B0D4
		internal void WriteFlag(CoreFlags field, bool value)
		{
			if (value)
			{
				this._flags |= field;
				return;
			}
			this._flags &= ~field;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0002BD04 File Offset: 0x0002B104
		[SecurityCritical]
		[SecurityTreatAsSafe]
		static UIElement3D()
		{
			UIElement3D.PreviewMouseDownEvent = Mouse.PreviewMouseDownEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.MouseDownEvent = Mouse.MouseDownEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewMouseUpEvent = Mouse.PreviewMouseUpEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.MouseUpEvent = Mouse.MouseUpEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewMouseLeftButtonDownEvent = UIElement.PreviewMouseLeftButtonDownEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.MouseLeftButtonDownEvent = UIElement.MouseLeftButtonDownEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewMouseLeftButtonUpEvent = UIElement.PreviewMouseLeftButtonUpEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.MouseLeftButtonUpEvent = UIElement.MouseLeftButtonUpEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewMouseRightButtonDownEvent = UIElement.PreviewMouseRightButtonDownEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.MouseRightButtonDownEvent = UIElement.MouseRightButtonDownEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewMouseRightButtonUpEvent = UIElement.PreviewMouseRightButtonUpEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.MouseRightButtonUpEvent = UIElement.MouseRightButtonUpEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewMouseMoveEvent = Mouse.PreviewMouseMoveEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.MouseMoveEvent = Mouse.MouseMoveEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewMouseWheelEvent = Mouse.PreviewMouseWheelEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.MouseWheelEvent = Mouse.MouseWheelEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.MouseEnterEvent = Mouse.MouseEnterEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.MouseLeaveEvent = Mouse.MouseLeaveEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.GotMouseCaptureEvent = Mouse.GotMouseCaptureEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.LostMouseCaptureEvent = Mouse.LostMouseCaptureEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.QueryCursorEvent = Mouse.QueryCursorEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewStylusDownEvent = Stylus.PreviewStylusDownEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.StylusDownEvent = Stylus.StylusDownEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewStylusUpEvent = Stylus.PreviewStylusUpEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.StylusUpEvent = Stylus.StylusUpEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewStylusMoveEvent = Stylus.PreviewStylusMoveEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.StylusMoveEvent = Stylus.StylusMoveEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewStylusInAirMoveEvent = Stylus.PreviewStylusInAirMoveEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.StylusInAirMoveEvent = Stylus.StylusInAirMoveEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.StylusEnterEvent = Stylus.StylusEnterEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.StylusLeaveEvent = Stylus.StylusLeaveEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewStylusInRangeEvent = Stylus.PreviewStylusInRangeEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.StylusInRangeEvent = Stylus.StylusInRangeEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewStylusOutOfRangeEvent = Stylus.PreviewStylusOutOfRangeEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.StylusOutOfRangeEvent = Stylus.StylusOutOfRangeEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewStylusSystemGestureEvent = Stylus.PreviewStylusSystemGestureEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.StylusSystemGestureEvent = Stylus.StylusSystemGestureEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.GotStylusCaptureEvent = Stylus.GotStylusCaptureEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.LostStylusCaptureEvent = Stylus.LostStylusCaptureEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.StylusButtonDownEvent = Stylus.StylusButtonDownEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.StylusButtonUpEvent = Stylus.StylusButtonUpEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewStylusButtonDownEvent = Stylus.PreviewStylusButtonDownEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewStylusButtonUpEvent = Stylus.PreviewStylusButtonUpEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewKeyDownEvent = Keyboard.PreviewKeyDownEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.KeyDownEvent = Keyboard.KeyDownEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewKeyUpEvent = Keyboard.PreviewKeyUpEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.KeyUpEvent = Keyboard.KeyUpEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewGotKeyboardFocusEvent = Keyboard.PreviewGotKeyboardFocusEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.GotKeyboardFocusEvent = Keyboard.GotKeyboardFocusEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewLostKeyboardFocusEvent = Keyboard.PreviewLostKeyboardFocusEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.LostKeyboardFocusEvent = Keyboard.LostKeyboardFocusEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewTextInputEvent = TextCompositionManager.PreviewTextInputEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.TextInputEvent = TextCompositionManager.TextInputEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewQueryContinueDragEvent = DragDrop.PreviewQueryContinueDragEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.QueryContinueDragEvent = DragDrop.QueryContinueDragEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewGiveFeedbackEvent = DragDrop.PreviewGiveFeedbackEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.GiveFeedbackEvent = DragDrop.GiveFeedbackEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewDragEnterEvent = DragDrop.PreviewDragEnterEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.DragEnterEvent = DragDrop.DragEnterEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewDragOverEvent = DragDrop.PreviewDragOverEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.DragOverEvent = DragDrop.DragOverEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewDragLeaveEvent = DragDrop.PreviewDragLeaveEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.DragLeaveEvent = DragDrop.DragLeaveEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewDropEvent = DragDrop.PreviewDropEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.DropEvent = DragDrop.DropEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewTouchDownEvent = Touch.PreviewTouchDownEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.TouchDownEvent = Touch.TouchDownEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewTouchMoveEvent = Touch.PreviewTouchMoveEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.TouchMoveEvent = Touch.TouchMoveEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.PreviewTouchUpEvent = Touch.PreviewTouchUpEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.TouchUpEvent = Touch.TouchUpEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.GotTouchCaptureEvent = Touch.GotTouchCaptureEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.LostTouchCaptureEvent = Touch.LostTouchCaptureEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.TouchEnterEvent = Touch.TouchEnterEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.TouchLeaveEvent = Touch.TouchLeaveEvent.AddOwner(UIElement3D._typeofThis);
			UIElement3D.IsMouseDirectlyOverProperty = UIElement.IsMouseDirectlyOverProperty.AddOwner(UIElement3D._typeofThis);
			UIElement3D.IsMouseOverProperty = UIElement.IsMouseOverProperty.AddOwner(UIElement3D._typeofThis);
			UIElement3D.IsStylusOverProperty = UIElement.IsStylusOverProperty.AddOwner(UIElement3D._typeofThis);
			UIElement3D.IsKeyboardFocusWithinProperty = UIElement.IsKeyboardFocusWithinProperty.AddOwner(UIElement3D._typeofThis);
			UIElement3D.IsMouseCapturedProperty = UIElement.IsMouseCapturedProperty.AddOwner(UIElement3D._typeofThis);
			UIElement3D.IsMouseCaptureWithinProperty = UIElement.IsMouseCaptureWithinProperty.AddOwner(UIElement3D._typeofThis);
			UIElement3D.IsStylusDirectlyOverProperty = UIElement.IsStylusDirectlyOverProperty.AddOwner(UIElement3D._typeofThis);
			UIElement3D.IsStylusCapturedProperty = UIElement.IsStylusCapturedProperty.AddOwner(UIElement3D._typeofThis);
			UIElement3D.IsStylusCaptureWithinProperty = UIElement.IsStylusCaptureWithinProperty.AddOwner(UIElement3D._typeofThis);
			UIElement3D.IsKeyboardFocusedProperty = UIElement.IsKeyboardFocusedProperty.AddOwner(UIElement3D._typeofThis);
			UIElement3D.AreAnyTouchesDirectlyOverProperty = UIElement.AreAnyTouchesDirectlyOverProperty.AddOwner(UIElement3D._typeofThis);
			UIElement3D.AreAnyTouchesOverProperty = UIElement.AreAnyTouchesOverProperty.AddOwner(UIElement3D._typeofThis);
			UIElement3D.AreAnyTouchesCapturedProperty = UIElement.AreAnyTouchesCapturedProperty.AddOwner(UIElement3D._typeofThis);
			UIElement3D.AreAnyTouchesCapturedWithinProperty = UIElement.AreAnyTouchesCapturedWithinProperty.AddOwner(UIElement3D._typeofThis);
			UIElement3D.AllowDropProperty = UIElement.AllowDropProperty.AddOwner(typeof(UIElement3D), new PropertyMetadata(BooleanBoxes.FalseBox));
			UIElement3D.VisibilityProperty = UIElement.VisibilityProperty.AddOwner(typeof(UIElement3D), new PropertyMetadata(VisibilityBoxes.VisibleBox, new PropertyChangedCallback(UIElement3D.OnVisibilityChanged)));
			UIElement3D.GotFocusEvent = FocusManager.GotFocusEvent.AddOwner(typeof(UIElement3D));
			UIElement3D.LostFocusEvent = FocusManager.LostFocusEvent.AddOwner(typeof(UIElement3D));
			UIElement3D.IsEnabledProperty = UIElement.IsEnabledProperty.AddOwner(typeof(UIElement3D), new UIPropertyMetadata(BooleanBoxes.TrueBox, new PropertyChangedCallback(UIElement3D.OnIsEnabledChanged), new CoerceValueCallback(UIElement3D.CoerceIsEnabled)));
			UIElement3D.IsHitTestVisibleProperty = UIElement.IsHitTestVisibleProperty.AddOwner(typeof(UIElement3D), new UIPropertyMetadata(BooleanBoxes.TrueBox, new PropertyChangedCallback(UIElement3D.OnIsHitTestVisibleChanged), new CoerceValueCallback(UIElement3D.CoerceIsHitTestVisible)));
			UIElement3D.IsHitTestVisibleChangedKey = new EventPrivateKey();
			UIElement3D.IsVisibleChangedKey = new EventPrivateKey();
			UIElement3D.FocusableProperty = UIElement.FocusableProperty.AddOwner(typeof(UIElement3D), new UIPropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(UIElement3D.OnFocusableChanged)));
			UIElement3D.EventHandlersStoreField = new UncommonField<EventHandlersStore>();
			UIElement3D.InputBindingCollectionField = new UncommonField<InputBindingCollection>();
			UIElement3D.CommandBindingCollectionField = new UncommonField<CommandBindingCollection>();
			UIElement3D.AutomationPeerField = new UncommonField<AutomationPeer>();
			UIElement.RegisterEvents(typeof(UIElement3D));
			UIElement3D.IsVisibleProperty = UIElement.IsVisibleProperty.AddOwner(typeof(UIElement3D));
			UIElement3D._isVisibleMetadata = new ReadOnlyPropertyMetadata(BooleanBoxes.FalseBox, new GetReadOnlyValueCallback(UIElement3D.GetIsVisible), new PropertyChangedCallback(UIElement3D.OnIsVisibleChanged));
			UIElement3D.IsVisibleProperty.OverrideMetadata(typeof(UIElement3D), UIElement3D._isVisibleMetadata, UIElement.IsVisiblePropertyKey);
			UIElement3D.IsFocusedProperty = UIElement.IsFocusedProperty.AddOwner(typeof(UIElement3D));
			UIElement3D.IsFocusedProperty.OverrideMetadata(typeof(UIElement3D), new PropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(UIElement3D.IsFocused_Changed)), UIElement.IsFocusedPropertyKey);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.UIElement3D" />.</summary>
		// Token: 0x06000B0C RID: 2828 RVA: 0x0002C630 File Offset: 0x0002BA30
		protected UIElement3D()
		{
			this._children = new Visual3DCollection(this);
			this.Initialize();
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0002C658 File Offset: 0x0002BA58
		private void Initialize()
		{
			base.BeginPropertyInitialization();
			this.VisibilityCache = (Visibility)UIElement3D.VisibilityProperty.GetDefaultValue(base.DependencyObjectType);
			this.InvalidateModel();
		}

		/// <summary>Obtém ou define um valor indicando se um elemento pode ser usado como o destino de uma operação de arrastar e soltar.</summary>
		/// <returns>
		///   <see langword="true" /> se um elemento pode ser usado como o destino de uma operação do tipo "arrastar e soltar"; caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x0002C68C File Offset: 0x0002BA8C
		// (set) Token: 0x06000B0F RID: 2831 RVA: 0x0002C6AC File Offset: 0x0002BAAC
		public bool AllowDrop
		{
			get
			{
				return (bool)base.GetValue(UIElement3D.AllowDropProperty);
			}
			set
			{
				base.SetValue(UIElement3D.AllowDropProperty, BooleanBoxes.Box(value));
			}
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0002C6CC File Offset: 0x0002BACC
		private object CallRenderCallback(object o)
		{
			this.OnUpdateModel();
			this._renderRequestPosted = false;
			return null;
		}

		/// <summary>Invalida o modelo que representa o elemento.</summary>
		// Token: 0x06000B11 RID: 2833 RVA: 0x0002C6E8 File Offset: 0x0002BAE8
		public void InvalidateModel()
		{
			if (!this._renderRequestPosted)
			{
				MediaContext.From(base.Dispatcher).BeginInvokeOnRender(new DispatcherOperationCallback(this.CallRenderCallback), this);
				this._renderRequestPosted = true;
			}
		}

		/// <summary>Participa de operações de renderização quando substituído em uma classe derivada.</summary>
		// Token: 0x06000B12 RID: 2834 RVA: 0x0002C724 File Offset: 0x0002BB24
		protected virtual void OnUpdateModel()
		{
		}

		/// <summary>Invocado quando o elemento pai desse <see cref="T:System.Windows.UIElement3D" /> relata uma alteração ao seu pai visual subjacente.</summary>
		/// <param name="oldParent">O pai anterior. Pode ser fornecido como <see langword="null" /> se o <see cref="T:System.Windows.DependencyObject" /> não teve um elemento pai anteriormente.</param>
		// Token: 0x06000B13 RID: 2835 RVA: 0x0002C734 File Offset: 0x0002BB34
		protected internal override void OnVisualParentChanged(DependencyObject oldParent)
		{
			if (base.InternalVisualParent != null)
			{
				DependencyObject dependencyObject = base.InternalVisualParent;
				if (!InputElement.IsUIElement(dependencyObject) && !InputElement.IsUIElement3D(dependencyObject))
				{
					Visual visual = dependencyObject as Visual;
					if (visual != null)
					{
						visual.VisualAncestorChanged += this.OnVisualAncestorChanged_ForceInherit;
					}
					else
					{
						Visual3D visual3D = dependencyObject as Visual3D;
						if (visual3D != null)
						{
							visual3D.VisualAncestorChanged += this.OnVisualAncestorChanged_ForceInherit;
						}
					}
					dependencyObject = InputElement.GetContainingUIElement(visual);
				}
				if (dependencyObject != null)
				{
					UIElement.SynchronizeForceInheritProperties(null, null, this, dependencyObject);
				}
			}
			else
			{
				DependencyObject dependencyObject2 = oldParent;
				if (!InputElement.IsUIElement(dependencyObject2) && !InputElement.IsUIElement3D(dependencyObject2))
				{
					if (oldParent is Visual)
					{
						((Visual)oldParent).VisualAncestorChanged -= this.OnVisualAncestorChanged_ForceInherit;
					}
					else if (oldParent is Visual3D)
					{
						((Visual3D)oldParent).VisualAncestorChanged -= this.OnVisualAncestorChanged_ForceInherit;
					}
					dependencyObject2 = InputElement.GetContainingUIElement(oldParent);
				}
				if (dependencyObject2 != null)
				{
					UIElement.SynchronizeForceInheritProperties(null, null, this, dependencyObject2);
				}
			}
			this.SynchronizeReverseInheritPropertyFlags(oldParent, true);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0002C81C File Offset: 0x0002BC1C
		private void OnVisualAncestorChanged_ForceInherit(object sender, AncestorChangedEventArgs e)
		{
			DependencyObject dependencyObject;
			if (e.OldParent == null)
			{
				dependencyObject = InputElement.GetContainingUIElement(base.InternalVisualParent);
				if (dependencyObject != null && VisualTreeHelper.IsAncestorOf(e.Ancestor, dependencyObject))
				{
					dependencyObject = null;
				}
			}
			else
			{
				dependencyObject = InputElement.GetContainingUIElement(base.InternalVisualParent);
				if (dependencyObject != null)
				{
					dependencyObject = null;
				}
				else
				{
					dependencyObject = InputElement.GetContainingUIElement(e.OldParent);
				}
			}
			if (dependencyObject != null)
			{
				UIElement.SynchronizeForceInheritProperties(null, null, this, dependencyObject);
			}
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0002C880 File Offset: 0x0002BC80
		internal void OnVisualAncestorChanged(object sender, AncestorChangedEventArgs e)
		{
			UIElement3D uielement3D = sender as UIElement3D;
			if (uielement3D != null)
			{
				PresentationSource.OnVisualAncestorChanged(uielement3D, e);
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0002C8A0 File Offset: 0x0002BCA0
		internal DependencyObject GetUIParent(bool continuePastVisualTree)
		{
			return UIElementHelper.GetUIParent(this, continuePastVisualTree);
		}

		/// <summary>Quando substituído em uma classe derivada, retornará um pai UI (interface do usuário) alternativo para esse elemento se nenhum pai visual existir.</summary>
		/// <returns>Um objeto se a implementação de uma classe derivada tiver uma conexão alternativa pai com o relatório.</returns>
		// Token: 0x06000B17 RID: 2839 RVA: 0x0002C8B4 File Offset: 0x0002BCB4
		protected internal DependencyObject GetUIParentCore()
		{
			return null;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0002C8C4 File Offset: 0x0002BCC4
		internal virtual void OnPresentationSourceChanged(bool attached)
		{
			if (!attached && FocusManager.GetFocusedElement(this) != null)
			{
				FocusManager.SetFocusedElement(this, null);
			}
		}

		/// <summary>Obtém um valor que indica se a posição do ponteiro do mouse corresponde aos resultados de teste de clique, que levam em consideração a composição de elementos.</summary>
		/// <returns>
		///   <see langword="true" /> se o ponteiro do mouse estiver sobre o mesmo resultado do elemento que um teste de clique; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0002C8E4 File Offset: 0x0002BCE4
		public bool IsMouseDirectlyOver
		{
			get
			{
				return this.IsMouseDirectlyOver_ComputeValue();
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0002C8F8 File Offset: 0x0002BCF8
		private bool IsMouseDirectlyOver_ComputeValue()
		{
			return Mouse.DirectlyOver == this;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0002C910 File Offset: 0x0002BD10
		[FriendAccessAllowed]
		internal void SynchronizeReverseInheritPropertyFlags(DependencyObject oldParent, bool isCoreParent)
		{
			if (this.IsKeyboardFocusWithin)
			{
				Keyboard.PrimaryDevice.ReevaluateFocusAsync(this, oldParent, isCoreParent);
			}
			if (this.IsStylusOver)
			{
				StylusLogic.CurrentStylusLogicReevaluateStylusOver(this, oldParent, isCoreParent);
			}
			if (this.IsStylusCaptureWithin)
			{
				StylusLogic.CurrentStylusLogicReevaluateCapture(this, oldParent, isCoreParent);
			}
			if (this.IsMouseOver)
			{
				Mouse.PrimaryDevice.ReevaluateMouseOver(this, oldParent, isCoreParent);
			}
			if (this.IsMouseCaptureWithin)
			{
				Mouse.PrimaryDevice.ReevaluateCapture(this, oldParent, isCoreParent);
			}
			if (this.AreAnyTouchesOver)
			{
				TouchDevice.ReevaluateDirectlyOver(this, oldParent, isCoreParent);
			}
			if (this.AreAnyTouchesCapturedWithin)
			{
				TouchDevice.ReevaluateCapturedWithin(this, oldParent, isCoreParent);
			}
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002C99C File Offset: 0x0002BD9C
		internal virtual bool BlockReverseInheritance()
		{
			return false;
		}

		/// <summary>Obtém um valor que indica se o ponteiro do mouse está localizado sobre esse elemento (incluindo os elementos filho na árvore visual).</summary>
		/// <returns>
		///   <see langword="true" /> se o ponteiro do mouse estiver sobre o elemento ou seus elementos filho; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0002C9AC File Offset: 0x0002BDAC
		public bool IsMouseOver
		{
			get
			{
				return this.ReadFlag(CoreFlags.IsMouseOverCache);
			}
		}

		/// <summary>Obtém um valor que indica se o cursor da caneta está localizado sobre esse elemento (incluindo elementos filho visuais).</summary>
		/// <returns>
		///   <see langword="true" /> se o cursor da caneta está sobre o elemento ou seus elementos filho; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0002C9C4 File Offset: 0x0002BDC4
		public bool IsStylusOver
		{
			get
			{
				return this.ReadFlag(CoreFlags.IsStylusOverCache);
			}
		}

		/// <summary>Obtém um valor que indica se o foco do teclado é em qualquer lugar dentro do elemento ou de seus elementos filho de árvore visual.</summary>
		/// <returns>
		///   <see langword="true" /> se o foco do teclado está no elemento ou em seus elementos filho; caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0002C9DC File Offset: 0x0002BDDC
		public bool IsKeyboardFocusWithin
		{
			get
			{
				return this.ReadFlag(CoreFlags.IsKeyboardFocusWithinCache);
			}
		}

		/// <summary>Obtém um valor que indica se o mouse é capturado para esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento tiver a captura do mouse; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0002C9F4 File Offset: 0x0002BDF4
		public bool IsMouseCaptured
		{
			get
			{
				return (bool)base.GetValue(UIElement3D.IsMouseCapturedProperty);
			}
		}

		/// <summary>Tenta forçar a captura do mouse para esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se o mouse for capturado com êxito; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000B21 RID: 2849 RVA: 0x0002CA14 File Offset: 0x0002BE14
		public bool CaptureMouse()
		{
			return Mouse.Capture(this);
		}

		/// <summary>Libera a captura do mouse, se esse elemento tiver mantido a captura.</summary>
		// Token: 0x06000B22 RID: 2850 RVA: 0x0002CA28 File Offset: 0x0002BE28
		public void ReleaseMouseCapture()
		{
			if (Mouse.Captured == this)
			{
				Mouse.Capture(null);
			}
		}

		/// <summary>Obtém um valor que determina se a captura do mouse é mantida por esse elemento ou elementos filho em sua árvore visual.</summary>
		/// <returns>
		///   <see langword="true" /> se esse elemento ou um elemento contido tiver captura do mouse; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0002CA44 File Offset: 0x0002BE44
		public bool IsMouseCaptureWithin
		{
			get
			{
				return this.ReadFlag(CoreFlags.IsMouseCaptureWithinCache);
			}
		}

		/// <summary>Obtém um valor que indica se a posição da caneta corresponde aos resultados de teste de clique, que levam em consideração a composição dos elementos.</summary>
		/// <returns>
		///   <see langword="true" /> se o ponteiro da caneta estiver sobre o mesmo resultado do elemento que um teste de clique; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0002CA5C File Offset: 0x0002BE5C
		public bool IsStylusDirectlyOver
		{
			get
			{
				return this.IsStylusDirectlyOver_ComputeValue();
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0002CA70 File Offset: 0x0002BE70
		private bool IsStylusDirectlyOver_ComputeValue()
		{
			return Stylus.DirectlyOver == this;
		}

		/// <summary>Obtém um valor que indica se a caneta é capturada por este elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento tem captura da caneta; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0002CA88 File Offset: 0x0002BE88
		public bool IsStylusCaptured
		{
			get
			{
				return (bool)base.GetValue(UIElement3D.IsStylusCapturedProperty);
			}
		}

		/// <summary>Tenta forçar a captura da caneta para esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se a caneta for capturada com êxito; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000B27 RID: 2855 RVA: 0x0002CAA8 File Offset: 0x0002BEA8
		public bool CaptureStylus()
		{
			return Stylus.Capture(this);
		}

		/// <summary>Libera a captura do dispositivo de caneta, se esse elemento tiver mantido a captura.</summary>
		// Token: 0x06000B28 RID: 2856 RVA: 0x0002CABC File Offset: 0x0002BEBC
		public void ReleaseStylusCapture()
		{
			Stylus.Capture(null);
		}

		/// <summary>Obtém um valor que determina se a captura da caneta é mantida por esse elemento ou um elemento nos limites do elemento e sua árvore visual.</summary>
		/// <returns>
		///   <see langword="true" /> se esse elemento ou um elemento contido tiver captura de caneta; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0002CAD0 File Offset: 0x0002BED0
		public bool IsStylusCaptureWithin
		{
			get
			{
				return this.ReadFlag(CoreFlags.IsStylusCaptureWithinCache);
			}
		}

		/// <summary>Obtém um valor que indica se este elemento tem foco do controle.</summary>
		/// <returns>
		///   <see langword="true" /> se esse elemento tiver o foco do teclado; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0002CAE8 File Offset: 0x0002BEE8
		public bool IsKeyboardFocused
		{
			get
			{
				return this.IsKeyboardFocused_ComputeValue();
			}
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0002CAFC File Offset: 0x0002BEFC
		private bool IsKeyboardFocused_ComputeValue()
		{
			return Keyboard.FocusedElement == this;
		}

		/// <summary>Tenta definir o foco lógico neste elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se os focos lógico e do teclado foram definidos para este elemento; <see langword="false" /> se apenas o foco lógico foi definido.</returns>
		// Token: 0x06000B2C RID: 2860 RVA: 0x0002CB14 File Offset: 0x0002BF14
		public bool Focus()
		{
			if (Keyboard.Focus(this) == this)
			{
				TipTsfHelper.Show(this);
				return true;
			}
			if (this.Focusable && this.IsEnabled)
			{
				DependencyObject focusScope = FocusManager.GetFocusScope(this);
				if (FocusManager.GetFocusedElement(focusScope) == null)
				{
					FocusManager.SetFocusedElement(focusScope, this);
				}
			}
			return false;
		}

		/// <summary>Tenta mover o foco deste para outro elemento. A direção para mover o foco é especificada por uma direção de diretrizes, que é interpretada dentro da organização do pai visual deste elemento.</summary>
		/// <param name="request">Uma solicitação de passagem, que contém uma propriedade que indica um modo para percorrer uma ordem de tabulação existente ou uma direção de movimentação visualmente.</param>
		/// <returns>
		///   <see langword="true" /> se a passagem solicitada foi executada; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000B2D RID: 2861 RVA: 0x0002CB5C File Offset: 0x0002BF5C
		public virtual bool MoveFocus(TraversalRequest request)
		{
			return false;
		}

		/// <summary>Quando substituído em uma classe derivada, retorna o elemento que deve receber o foco para uma direção de passagem do foco especificada, sem realmente mover o foco para esse elemento.</summary>
		/// <param name="direction">A direção da passagem do foco solicitada.</param>
		/// <returns>O elemento que teria recebido foco, se <see cref="M:System.Windows.UIElement3D.MoveFocus(System.Windows.Input.TraversalRequest)" /> realmente fosse invocado.</returns>
		// Token: 0x06000B2E RID: 2862 RVA: 0x0002CB6C File Offset: 0x0002BF6C
		public virtual DependencyObject PredictFocus(FocusNavigationDirection direction)
		{
			return null;
		}

		/// <summary>Fornece tratamento de classes para quando uma chave de acesso que seja significativa para esse elemento é chamada.</summary>
		/// <param name="e">Os dados de evento para o evento de chave de acesso. Os relatórios de dados de evento cuja chave foi chamada e indica se o objeto <see cref="T:System.Windows.Input.AccessKeyManager" /> que controla o envio desses eventos também envia essa chamada de chave de acesso a outros elementos.</param>
		// Token: 0x06000B2F RID: 2863 RVA: 0x0002CB7C File Offset: 0x0002BF7C
		protected virtual void OnAccessKey(AccessKeyEventArgs e)
		{
			this.Focus();
		}

		/// <summary>Obtém um valor que indica se um sistema de método de entrada, como um Input Method Editor (IME), está habilitado para processamento de entrada para esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se um método de entrada estiver ativo; caso contrário, <see langword="false" />. O valor padrão da propriedade anexada subjacente é <see langword="true;" />, no entanto, isso será influenciado pelo estado real dos métodos de entrada no tempo de execução.</returns>
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0002CB90 File Offset: 0x0002BF90
		public bool IsInputMethodEnabled
		{
			get
			{
				return (bool)base.GetValue(InputMethod.IsInputMethodEnabledProperty);
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002CBB0 File Offset: 0x0002BFB0
		private static void OnVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement3D uielement3D = (UIElement3D)d;
			Visibility visibility = (Visibility)e.NewValue;
			uielement3D.VisibilityCache = visibility;
			uielement3D.switchVisibilityIfNeeded(visibility);
			uielement3D.UpdateIsVisibleCache();
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0002CBE8 File Offset: 0x0002BFE8
		private static bool ValidateVisibility(object o)
		{
			Visibility visibility = (Visibility)o;
			return visibility == Visibility.Visible || visibility == Visibility.Hidden || visibility == Visibility.Collapsed;
		}

		/// <summary>Obtém ou define a visibilidade UI (interface do usuário) desse elemento.</summary>
		/// <returns>Um valor da enumeração. O valor padrão é <see cref="F:System.Windows.Visibility.Visible" />.</returns>
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0002CC0C File Offset: 0x0002C00C
		// (set) Token: 0x06000B34 RID: 2868 RVA: 0x0002CC20 File Offset: 0x0002C020
		[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
		public Visibility Visibility
		{
			get
			{
				return this.VisibilityCache;
			}
			set
			{
				base.SetValue(UIElement3D.VisibilityProperty, VisibilityBoxes.Box(value));
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002CC40 File Offset: 0x0002C040
		private void switchVisibilityIfNeeded(Visibility visibility)
		{
			switch (visibility)
			{
			case Visibility.Visible:
				this.ensureVisible();
				return;
			case Visibility.Hidden:
				this.ensureInvisible(false);
				return;
			case Visibility.Collapsed:
				this.ensureInvisible(true);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0002CC78 File Offset: 0x0002C078
		private void ensureVisible()
		{
			base.InternalIsVisible = true;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0002CC8C File Offset: 0x0002C08C
		private void ensureInvisible(bool collapsed)
		{
			base.InternalIsVisible = false;
			if (!this.ReadFlag(CoreFlags.IsCollapsed) && collapsed)
			{
				this.WriteFlag(CoreFlags.IsCollapsed, true);
				return;
			}
			if (this.ReadFlag(CoreFlags.IsCollapsed) && !collapsed)
			{
				this.WriteFlag(CoreFlags.IsCollapsed, false);
			}
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0002CCDC File Offset: 0x0002C0DC
		internal void InvokeAccessKey(AccessKeyEventArgs e)
		{
			this.OnAccessKey(e);
		}

		/// <summary>Ocorre quando este elemento tem foco lógico.</summary>
		// Token: 0x1400010F RID: 271
		// (add) Token: 0x06000B39 RID: 2873 RVA: 0x0002CCF0 File Offset: 0x0002C0F0
		// (remove) Token: 0x06000B3A RID: 2874 RVA: 0x0002CD0C File Offset: 0x0002C10C
		public event RoutedEventHandler GotFocus
		{
			add
			{
				this.AddHandler(UIElement3D.GotFocusEvent, value);
			}
			remove
			{
				this.RemoveHandler(UIElement3D.GotFocusEvent, value);
			}
		}

		/// <summary>Ocorre quando este elemento perde o foco lógico.</summary>
		// Token: 0x14000110 RID: 272
		// (add) Token: 0x06000B3B RID: 2875 RVA: 0x0002CD28 File Offset: 0x0002C128
		// (remove) Token: 0x06000B3C RID: 2876 RVA: 0x0002CD44 File Offset: 0x0002C144
		public event RoutedEventHandler LostFocus
		{
			add
			{
				this.AddHandler(UIElement3D.LostFocusEvent, value);
			}
			remove
			{
				this.RemoveHandler(UIElement3D.LostFocusEvent, value);
			}
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0002CD60 File Offset: 0x0002C160
		private static void IsFocused_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement3D uielement3D = (UIElement3D)d;
			if ((bool)e.NewValue)
			{
				uielement3D.OnGotFocus(new RoutedEventArgs(UIElement3D.GotFocusEvent, uielement3D));
				return;
			}
			uielement3D.OnLostFocus(new RoutedEventArgs(UIElement3D.LostFocusEvent, uielement3D));
		}

		/// <summary>Gera o evento roteado <see cref="E:System.Windows.UIElement3D.GotFocus" /> usando os dados de evento fornecidos.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.RoutedEventArgs" /> que contém dados do evento. Esses dados de evento devem conter o identificador para o evento <see cref="E:System.Windows.UIElement3D.GotFocus" />.</param>
		// Token: 0x06000B3E RID: 2878 RVA: 0x0002CDA8 File Offset: 0x0002C1A8
		protected virtual void OnGotFocus(RoutedEventArgs e)
		{
			this.RaiseEvent(e);
		}

		/// <summary>Gera o evento roteado <see cref="E:System.Windows.UIElement3D.LostFocus" /> usando os dados de evento fornecidos.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.RoutedEventArgs" /> que contém dados do evento. Esses dados de evento devem conter o identificador para o evento <see cref="E:System.Windows.UIElement3D.LostFocus" />.</param>
		// Token: 0x06000B3F RID: 2879 RVA: 0x0002CDBC File Offset: 0x0002C1BC
		protected virtual void OnLostFocus(RoutedEventArgs e)
		{
			this.RaiseEvent(e);
		}

		/// <summary>Obtém um valor que determina se esse elemento tem foco lógico.</summary>
		/// <returns>
		///   <see langword="true" /> se esse elemento tiver o foco lógico; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x0002CDD0 File Offset: 0x0002C1D0
		public bool IsFocused
		{
			get
			{
				return (bool)base.GetValue(UIElement3D.IsFocusedProperty);
			}
		}

		/// <summary>Obtém ou define um valor que indica se esse elemento está habilitado no UI (interface do usuário).</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento estiver habilitado; caso contrário, <see langword="false" />. O valor padrão é <see langword="true" />.</returns>
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0002CDF0 File Offset: 0x0002C1F0
		// (set) Token: 0x06000B42 RID: 2882 RVA: 0x0002CE10 File Offset: 0x0002C210
		public bool IsEnabled
		{
			get
			{
				return (bool)base.GetValue(UIElement3D.IsEnabledProperty);
			}
			set
			{
				base.SetValue(UIElement3D.IsEnabledProperty, BooleanBoxes.Box(value));
			}
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.UIElement3D.IsEnabled" /> neste elemento é alterado.</summary>
		// Token: 0x14000111 RID: 273
		// (add) Token: 0x06000B43 RID: 2883 RVA: 0x0002CE30 File Offset: 0x0002C230
		// (remove) Token: 0x06000B44 RID: 2884 RVA: 0x0002CE4C File Offset: 0x0002C24C
		public event DependencyPropertyChangedEventHandler IsEnabledChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.IsEnabledChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.IsEnabledChangedKey, value);
			}
		}

		/// <summary>Obtém um valor que se torna o valor retornado de <see cref="P:System.Windows.UIElement3D.IsEnabled" /> em classes derivadas.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento estiver habilitado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0002CE68 File Offset: 0x0002C268
		protected virtual bool IsEnabledCore
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0002CE78 File Offset: 0x0002C278
		private static object CoerceIsEnabled(DependencyObject d, object value)
		{
			UIElement3D uielement3D = (UIElement3D)d;
			if (!(bool)value)
			{
				return BooleanBoxes.FalseBox;
			}
			DependencyObject dependencyObject = uielement3D.GetUIParentCore() as ContentElement;
			if (dependencyObject == null)
			{
				dependencyObject = InputElement.GetContainingUIElement(uielement3D.InternalVisualParent);
			}
			if (dependencyObject == null || (bool)dependencyObject.GetValue(UIElement.IsEnabledProperty))
			{
				return BooleanBoxes.Box(uielement3D.IsEnabledCore);
			}
			return BooleanBoxes.FalseBox;
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0002CEDC File Offset: 0x0002C2DC
		private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement3D uielement3D = (UIElement3D)d;
			uielement3D.RaiseDependencyPropertyChanged(UIElement.IsEnabledChangedKey, e);
			uielement3D.InvalidateForceInheritPropertyOnChildren(e.Property);
			InputManager.SafeCurrentNotifyHitTestInvalidated();
			AutomationPeer automationPeer = uielement3D.GetAutomationPeer();
			if (automationPeer != null)
			{
				automationPeer.InvalidatePeer();
			}
		}

		/// <summary>Obtém ou define um valor que declara se este elemento tem possibilidade de ser retornado como um resultado de teste de clique de alguma parte de seu conteúdo renderizado.</summary>
		/// <returns>
		///   <see langword="true" /> se esse elemento puder ser retornado como um resultado do teste de clique de, pelo menos, um ponto; caso contrário, <see langword="false" />. O valor padrão é <see langword="true" />.</returns>
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0002CF20 File Offset: 0x0002C320
		// (set) Token: 0x06000B49 RID: 2889 RVA: 0x0002CF40 File Offset: 0x0002C340
		public bool IsHitTestVisible
		{
			get
			{
				return (bool)base.GetValue(UIElement3D.IsHitTestVisibleProperty);
			}
			set
			{
				base.SetValue(UIElement3D.IsHitTestVisibleProperty, BooleanBoxes.Box(value));
			}
		}

		/// <summary>Ocorre quando o valor da propriedade de dependência <see cref="P:System.Windows.UIElement3D.IsHitTestVisible" /> é alterado neste elemento.</summary>
		// Token: 0x14000112 RID: 274
		// (add) Token: 0x06000B4A RID: 2890 RVA: 0x0002CF60 File Offset: 0x0002C360
		// (remove) Token: 0x06000B4B RID: 2891 RVA: 0x0002CF7C File Offset: 0x0002C37C
		public event DependencyPropertyChangedEventHandler IsHitTestVisibleChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement3D.IsHitTestVisibleChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement3D.IsHitTestVisibleChangedKey, value);
			}
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0002CF98 File Offset: 0x0002C398
		private static object CoerceIsHitTestVisible(DependencyObject d, object value)
		{
			UIElement3D uielement3D = (UIElement3D)d;
			if (!(bool)value)
			{
				return BooleanBoxes.FalseBox;
			}
			DependencyObject containingUIElement = InputElement.GetContainingUIElement(uielement3D.InternalVisualParent);
			if (containingUIElement == null || UIElementHelper.IsHitTestVisible(containingUIElement))
			{
				return BooleanBoxes.TrueBox;
			}
			return BooleanBoxes.FalseBox;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0002CFDC File Offset: 0x0002C3DC
		private static void OnIsHitTestVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement3D uielement3D = (UIElement3D)d;
			uielement3D.RaiseDependencyPropertyChanged(UIElement3D.IsHitTestVisibleChangedKey, e);
			uielement3D.InvalidateForceInheritPropertyOnChildren(e.Property);
			InputManager.SafeCurrentNotifyHitTestInvalidated();
		}

		/// <summary>Obtém um valor que indica se esse elemento está visível no UI (interface do usuário).</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento estiver visível; caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0002D010 File Offset: 0x0002C410
		public bool IsVisible
		{
			get
			{
				return this.ReadFlag(CoreFlags.IsVisibleCache);
			}
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0002D028 File Offset: 0x0002C428
		private static object GetIsVisible(DependencyObject d, out BaseValueSourceInternal source)
		{
			source = BaseValueSourceInternal.Local;
			if (!((UIElement3D)d).IsVisible)
			{
				return BooleanBoxes.FalseBox;
			}
			return BooleanBoxes.TrueBox;
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.UIElement3D.IsVisible" /> é alterado neste elemento.</summary>
		// Token: 0x14000113 RID: 275
		// (add) Token: 0x06000B50 RID: 2896 RVA: 0x0002D054 File Offset: 0x0002C454
		// (remove) Token: 0x06000B51 RID: 2897 RVA: 0x0002D070 File Offset: 0x0002C470
		public event DependencyPropertyChangedEventHandler IsVisibleChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.IsVisibleChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.IsVisibleChangedKey, value);
			}
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0002D08C File Offset: 0x0002C48C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal void UpdateIsVisibleCache()
		{
			bool flag = this.Visibility == Visibility.Visible;
			if (flag)
			{
				bool flag2 = false;
				DependencyObject containingUIElement = InputElement.GetContainingUIElement(base.InternalVisualParent);
				if (containingUIElement != null)
				{
					flag2 = UIElementHelper.IsVisible(containingUIElement);
				}
				else
				{
					PresentationSource presentationSource = PresentationSource.CriticalFromVisual(this);
					if (presentationSource != null)
					{
						flag2 = true;
					}
				}
				if (!flag2)
				{
					flag = false;
				}
			}
			if (flag != this.IsVisible)
			{
				this.WriteFlag(CoreFlags.IsVisibleCache, flag);
				base.NotifyPropertyChange(new DependencyPropertyChangedEventArgs(UIElement3D.IsVisibleProperty, UIElement3D._isVisibleMetadata, !flag, flag));
			}
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0002D10C File Offset: 0x0002C50C
		private static void OnIsVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement3D uielement3D = (UIElement3D)d;
			uielement3D.RaiseDependencyPropertyChanged(UIElement3D.IsVisibleChangedKey, e);
			uielement3D.InvalidateForceInheritPropertyOnChildren(e.Property);
			InputManager.SafeCurrentNotifyHitTestInvalidated();
		}

		/// <summary>Obtém ou define um valor que indica se um elemento pode receber foco.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento for focalizável; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0002D140 File Offset: 0x0002C540
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x0002D160 File Offset: 0x0002C560
		public bool Focusable
		{
			get
			{
				return (bool)base.GetValue(UIElement3D.FocusableProperty);
			}
			set
			{
				base.SetValue(UIElement3D.FocusableProperty, BooleanBoxes.Box(value));
			}
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.UIElement3D.Focusable" /> muda.</summary>
		// Token: 0x14000114 RID: 276
		// (add) Token: 0x06000B56 RID: 2902 RVA: 0x0002D180 File Offset: 0x0002C580
		// (remove) Token: 0x06000B57 RID: 2903 RVA: 0x0002D19C File Offset: 0x0002C59C
		public event DependencyPropertyChangedEventHandler FocusableChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.FocusableChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.FocusableChangedKey, value);
			}
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0002D1B8 File Offset: 0x0002C5B8
		private static void OnFocusableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement3D uielement3D = (UIElement3D)d;
			uielement3D.RaiseDependencyPropertyChanged(UIElement.FocusableChangedKey, e);
		}

		/// <summary>Retorna implementações de <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> específicas à classe para a infra-estrutura de Windows Presentation Foundation (WPF).</summary>
		/// <returns>A implementação de <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> específica ao tipo.</returns>
		// Token: 0x06000B59 RID: 2905 RVA: 0x0002D1D8 File Offset: 0x0002C5D8
		protected virtual AutomationPeer OnCreateAutomationPeer()
		{
			return null;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0002D1E8 File Offset: 0x0002C5E8
		internal AutomationPeer CreateAutomationPeer()
		{
			base.VerifyAccess();
			AutomationPeer automationPeer;
			if (this.HasAutomationPeer)
			{
				automationPeer = UIElement3D.AutomationPeerField.GetValue(this);
			}
			else
			{
				automationPeer = this.OnCreateAutomationPeer();
				if (automationPeer != null)
				{
					UIElement3D.AutomationPeerField.SetValue(this, automationPeer);
					this.HasAutomationPeer = true;
				}
			}
			return automationPeer;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0002D234 File Offset: 0x0002C634
		internal AutomationPeer GetAutomationPeer()
		{
			base.VerifyAccess();
			if (this.HasAutomationPeer)
			{
				return UIElement3D.AutomationPeerField.GetValue(this);
			}
			return null;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002D25C File Offset: 0x0002C65C
		internal void AddSynchronizedInputPreOpportunityHandler(EventRoute route, RoutedEventArgs args)
		{
			if (InputManager.IsSynchronizedInput && SynchronizedInputHelper.IsListening(this, args))
			{
				RoutedEventHandler eventHandler = new RoutedEventHandler(this.SynchronizedInputPreOpportunityHandler);
				SynchronizedInputHelper.AddHandlerToRoute(this, route, eventHandler, false);
			}
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0002D290 File Offset: 0x0002C690
		internal void AddSynchronizedInputPostOpportunityHandler(EventRoute route, RoutedEventArgs args)
		{
			if (InputManager.IsSynchronizedInput)
			{
				if (SynchronizedInputHelper.IsListening(this, args))
				{
					RoutedEventHandler eventHandler = new RoutedEventHandler(this.SynchronizedInputPostOpportunityHandler);
					SynchronizedInputHelper.AddHandlerToRoute(this, route, eventHandler, true);
					return;
				}
				SynchronizedInputHelper.AddParentPreOpportunityHandler(this, route, args);
			}
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0002D2CC File Offset: 0x0002C6CC
		internal void SynchronizedInputPreOpportunityHandler(object sender, RoutedEventArgs args)
		{
			if (!args.Handled)
			{
				SynchronizedInputHelper.PreOpportunityHandler(sender, args);
			}
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0002D2E8 File Offset: 0x0002C6E8
		internal void SynchronizedInputPostOpportunityHandler(object sender, RoutedEventArgs args)
		{
			if (args.Handled && InputManager.SynchronizedInputState == SynchronizedInputStates.HadOpportunity)
			{
				SynchronizedInputHelper.PostOpportunityHandler(sender, args);
			}
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0002D30C File Offset: 0x0002C70C
		internal bool StartListeningSynchronizedInput(SynchronizedInputType inputType)
		{
			if (InputManager.IsSynchronizedInput)
			{
				return false;
			}
			InputManager.StartListeningSynchronizedInput(this, inputType);
			return true;
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0002D32C File Offset: 0x0002C72C
		internal void CancelSynchronizedInput()
		{
			InputManager.CancelSynchronizedInput();
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0002D340 File Offset: 0x0002C740
		private void RaiseDependencyPropertyChanged(EventPrivateKey key, DependencyPropertyChangedEventArgs args)
		{
			EventHandlersStore eventHandlersStore = this.EventHandlersStore;
			if (eventHandlersStore != null)
			{
				Delegate @delegate = eventHandlersStore.Get(key);
				if (@delegate != null)
				{
					((DependencyPropertyChangedEventHandler)@delegate)(this, args);
				}
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x0002D370 File Offset: 0x0002C770
		// (set) Token: 0x06000B64 RID: 2916 RVA: 0x0002D39C File Offset: 0x0002C79C
		private Visibility VisibilityCache
		{
			get
			{
				if (base.CheckFlagsAnd(VisualFlags.VisibilityCache_Visible))
				{
					return Visibility.Visible;
				}
				if (base.CheckFlagsAnd(VisualFlags.VisibilityCache_TakesSpace))
				{
					return Visibility.Hidden;
				}
				return Visibility.Collapsed;
			}
			set
			{
				switch (value)
				{
				case Visibility.Visible:
					base.SetFlags(true, VisualFlags.VisibilityCache_Visible);
					base.SetFlags(false, VisualFlags.VisibilityCache_TakesSpace);
					return;
				case Visibility.Hidden:
					base.SetFlags(false, VisualFlags.VisibilityCache_Visible);
					base.SetFlags(true, VisualFlags.VisibilityCache_TakesSpace);
					return;
				case Visibility.Collapsed:
					base.SetFlags(false, VisualFlags.VisibilityCache_Visible);
					base.SetFlags(false, VisualFlags.VisibilityCache_TakesSpace);
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0002D408 File Offset: 0x0002C808
		internal static void InvalidateForceInheritPropertyOnChildren(Visual3D v, DependencyProperty property)
		{
			int internalVisual2DOr3DChildrenCount = v.InternalVisual2DOr3DChildrenCount;
			for (int i = 0; i < internalVisual2DOr3DChildrenCount; i++)
			{
				DependencyObject dependencyObject = v.InternalGet2DOr3DVisualChild(i);
				if (dependencyObject != null)
				{
					UIElement uielement = dependencyObject as UIElement;
					UIElement3D uielement3D = dependencyObject as UIElement3D;
					if (uielement3D != null)
					{
						if (property == UIElement3D.IsVisibleProperty)
						{
							uielement3D.UpdateIsVisibleCache();
						}
						else
						{
							uielement3D.CoerceValue(property);
						}
					}
					else if (uielement != null)
					{
						if (property == UIElement3D.IsVisibleProperty)
						{
							uielement.UpdateIsVisibleCache();
						}
						else
						{
							uielement.CoerceValue(property);
						}
					}
					else if (dependencyObject is Visual)
					{
						((Visual)dependencyObject).InvalidateForceInheritPropertyOnChildren(property);
					}
					else
					{
						((Visual3D)dependencyObject).InvalidateForceInheritPropertyOnChildren(property);
					}
				}
			}
		}

		/// <summary>Obtém um valor que indica se pelo menos um toque for pressionado sobre esse elemento ou elementos filho na sua árvore visual.</summary>
		/// <returns>
		///   <see langword="true" /> se pelo menos um toque for pressionado sobre esse elemento ou elementos filho na sua árvore visual; caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x0002D4A0 File Offset: 0x0002C8A0
		public bool AreAnyTouchesOver
		{
			get
			{
				return this.ReadFlag(CoreFlags.TouchesOverCache);
			}
		}

		/// <summary>Obtém um valor que indica se pelo menos um toque é feito sobre esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se pelo menos um toque for pressionado sobre esse elemento; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x0002D4B8 File Offset: 0x0002C8B8
		public bool AreAnyTouchesDirectlyOver
		{
			get
			{
				return (bool)base.GetValue(UIElement3D.AreAnyTouchesDirectlyOverProperty);
			}
		}

		/// <summary>Obtém um valor que indica se ao menos um toque é capturado nesse elemento ou elementos filho na sua árvore visual.</summary>
		/// <returns>
		///   <see langword="true" /> Se pelo menos um toque é capturado para esse elemento ou elementos filho na árvore visual; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0002D4D8 File Offset: 0x0002C8D8
		public bool AreAnyTouchesCapturedWithin
		{
			get
			{
				return this.ReadFlag(CoreFlags.TouchesCapturedWithinCache);
			}
		}

		/// <summary>Obtém um valor que indica se pelo menos um toque é capturado para esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se pelo menos um toque for capturado para esse elemento; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0002D4F0 File Offset: 0x0002C8F0
		public bool AreAnyTouchesCaptured
		{
			get
			{
				return (bool)base.GetValue(UIElement3D.AreAnyTouchesCapturedProperty);
			}
		}

		/// <summary>Tenta forçar a captura de um toque para esse elemento.</summary>
		/// <param name="touchDevice">O dispositivo a ser capturado.</param>
		/// <returns>
		///   <see langword="true" /> se o toque especificado for capturado para esse elemento; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="touchDevice" /> é <see langword="null" />.</exception>
		// Token: 0x06000B6A RID: 2922 RVA: 0x0002D510 File Offset: 0x0002C910
		public bool CaptureTouch(TouchDevice touchDevice)
		{
			if (touchDevice == null)
			{
				throw new ArgumentNullException("touchDevice");
			}
			return touchDevice.Capture(this);
		}

		/// <summary>Tenta liberar o dispositivo de toque especificado desse elemento.</summary>
		/// <param name="touchDevice">O dispositivo a ser liberado.</param>
		/// <returns>
		///   <see langword="true" /> se o dispositivo de toque estiver liberado; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="touchDevice" /> é <see langword="null" />.</exception>
		// Token: 0x06000B6B RID: 2923 RVA: 0x0002D534 File Offset: 0x0002C934
		public bool ReleaseTouchCapture(TouchDevice touchDevice)
		{
			if (touchDevice == null)
			{
				throw new ArgumentNullException("touchDevice");
			}
			if (touchDevice.Captured == this)
			{
				touchDevice.Capture(null);
				return true;
			}
			return false;
		}

		/// <summary>Libera todos os dispositivos de toque capturados desse elemento.</summary>
		// Token: 0x06000B6C RID: 2924 RVA: 0x0002D564 File Offset: 0x0002C964
		public void ReleaseAllTouchCaptures()
		{
			TouchDevice.ReleaseAllCaptures(this);
		}

		/// <summary>Obtém todos os dispositivos de toque capturados para esse elemento.</summary>
		/// <returns>Uma enumeração de objetos <see cref="T:System.Windows.Input.TouchDevice" /> capturados para esse elemento.</returns>
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0002D578 File Offset: 0x0002C978
		public IEnumerable<TouchDevice> TouchesCaptured
		{
			get
			{
				return TouchDevice.GetCapturedTouches(this, false);
			}
		}

		/// <summary>Obtém todos os dispositivos de toque que são capturados para esse elemento ou os elementos filho na árvore visual.</summary>
		/// <returns>Uma enumeração de objetos <see cref="T:System.Windows.Input.TouchDevice" /> que são capturados para esse elemento ou elementos filho na árvore visual.</returns>
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0002D58C File Offset: 0x0002C98C
		public IEnumerable<TouchDevice> TouchesCapturedWithin
		{
			get
			{
				return TouchDevice.GetCapturedTouches(this, true);
			}
		}

		/// <summary>Obtém todos os dispositivos de toque que estão sobre esse elemento ou sobre os elementos filho na árvore visual.</summary>
		/// <returns>Uma enumeração de objetos <see cref="T:System.Windows.Input.TouchDevice" /> que estão acima desse elemento ou dos elementos filho na árvore visual.</returns>
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x0002D5A0 File Offset: 0x0002C9A0
		public IEnumerable<TouchDevice> TouchesOver
		{
			get
			{
				return TouchDevice.GetTouchesOver(this, true);
			}
		}

		/// <summary>Obtém todos os dispositivos de toque nesse elemento.</summary>
		/// <returns>Uma enumeração de objetos <see cref="T:System.Windows.Input.TouchDevice" /> nesse elemento.</returns>
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0002D5B4 File Offset: 0x0002C9B4
		public IEnumerable<TouchDevice> TouchesDirectlyOver
		{
			get
			{
				return TouchDevice.GetTouchesOver(this, false);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x0002D5C8 File Offset: 0x0002C9C8
		// (set) Token: 0x06000B72 RID: 2930 RVA: 0x0002D5E0 File Offset: 0x0002C9E0
		internal bool HasAutomationPeer
		{
			get
			{
				return this.ReadFlag(CoreFlags.HasAutomationPeer);
			}
			set
			{
				this.WriteFlag(CoreFlags.HasAutomationPeer, value);
			}
		}

		// Token: 0x0400066B RID: 1643
		private static readonly Type _typeofThis = typeof(UIElement3D);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement3D.IsMouseDirectlyOver" />.</summary>
		// Token: 0x040006B7 RID: 1719
		public static readonly DependencyProperty IsMouseDirectlyOverProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement3D.IsMouseOver" />.</summary>
		// Token: 0x040006B8 RID: 1720
		public static readonly DependencyProperty IsMouseOverProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement3D.IsStylusOver" />.</summary>
		// Token: 0x040006B9 RID: 1721
		public static readonly DependencyProperty IsStylusOverProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement3D.IsKeyboardFocusWithin" />.</summary>
		// Token: 0x040006BA RID: 1722
		public static readonly DependencyProperty IsKeyboardFocusWithinProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement3D.IsMouseCaptured" />.</summary>
		// Token: 0x040006BB RID: 1723
		public static readonly DependencyProperty IsMouseCapturedProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement3D.IsMouseCaptureWithin" />.</summary>
		// Token: 0x040006BC RID: 1724
		public static readonly DependencyProperty IsMouseCaptureWithinProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement3D.IsStylusDirectlyOver" />.</summary>
		// Token: 0x040006BD RID: 1725
		public static readonly DependencyProperty IsStylusDirectlyOverProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement3D.IsStylusCaptured" />.</summary>
		// Token: 0x040006BE RID: 1726
		public static readonly DependencyProperty IsStylusCapturedProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement3D.IsStylusCaptureWithin" />.</summary>
		// Token: 0x040006BF RID: 1727
		public static readonly DependencyProperty IsStylusCaptureWithinProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement3D.IsKeyboardFocused" />.</summary>
		// Token: 0x040006C0 RID: 1728
		public static readonly DependencyProperty IsKeyboardFocusedProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement3D.AreAnyTouchesDirectlyOver" />.</summary>
		// Token: 0x040006C1 RID: 1729
		public static readonly DependencyProperty AreAnyTouchesDirectlyOverProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement3D.AreAnyTouchesOver" />.</summary>
		// Token: 0x040006C2 RID: 1730
		public static readonly DependencyProperty AreAnyTouchesOverProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement3D.AreAnyTouchesCaptured" />.</summary>
		// Token: 0x040006C3 RID: 1731
		public static readonly DependencyProperty AreAnyTouchesCapturedProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement3D.AreAnyTouchesCapturedWithin" />.</summary>
		// Token: 0x040006C4 RID: 1732
		public static readonly DependencyProperty AreAnyTouchesCapturedWithinProperty;

		// Token: 0x040006C5 RID: 1733
		private CoreFlags _flags;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement3D.AllowDrop" />.</summary>
		// Token: 0x040006C6 RID: 1734
		public static readonly DependencyProperty AllowDropProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement3D.Visibility" />.</summary>
		// Token: 0x040006C7 RID: 1735
		[CommonDependencyProperty]
		public static readonly DependencyProperty VisibilityProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement3D.IsFocused" />.</summary>
		// Token: 0x040006CA RID: 1738
		public static readonly DependencyProperty IsFocusedProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement3D.IsEnabled" />.</summary>
		// Token: 0x040006CB RID: 1739
		[CommonDependencyProperty]
		public static readonly DependencyProperty IsEnabledProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement3D.IsHitTestVisible" />.</summary>
		// Token: 0x040006CC RID: 1740
		public static readonly DependencyProperty IsHitTestVisibleProperty;

		// Token: 0x040006CD RID: 1741
		internal static readonly EventPrivateKey IsHitTestVisibleChangedKey;

		// Token: 0x040006CE RID: 1742
		private static PropertyMetadata _isVisibleMetadata;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement3D.IsVisible" />.</summary>
		// Token: 0x040006CF RID: 1743
		public static readonly DependencyProperty IsVisibleProperty;

		// Token: 0x040006D0 RID: 1744
		internal static readonly EventPrivateKey IsVisibleChangedKey;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement3D.Focusable" />.</summary>
		// Token: 0x040006D1 RID: 1745
		[CommonDependencyProperty]
		public static readonly DependencyProperty FocusableProperty;

		// Token: 0x040006D2 RID: 1746
		private readonly Visual3DCollection _children;

		// Token: 0x040006D3 RID: 1747
		private bool _renderRequestPosted;

		// Token: 0x040006D4 RID: 1748
		internal static readonly UncommonField<EventHandlersStore> EventHandlersStoreField;

		// Token: 0x040006D5 RID: 1749
		internal static readonly UncommonField<InputBindingCollection> InputBindingCollectionField;

		// Token: 0x040006D6 RID: 1750
		internal static readonly UncommonField<CommandBindingCollection> CommandBindingCollectionField;

		// Token: 0x040006D7 RID: 1751
		private static readonly UncommonField<AutomationPeer> AutomationPeerField;
	}
}
