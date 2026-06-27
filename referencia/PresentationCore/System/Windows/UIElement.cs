using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Input;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;
using MS.Internal;
using MS.Internal.Interop;
using MS.Internal.KnownBoxes;
using MS.Internal.Media;
using MS.Internal.Permissions;
using MS.Internal.PresentationCore;
using MS.Utility;
using MS.Win32;

namespace System.Windows
{
	/// <summary>
	///   <see cref="T:System.Windows.UIElement" /> é uma classe base para implementações no nível do núcleo WPF baseada em elementos WPF (Windows Presentation Foundation) e características de apresentação básicas.</summary>
	// Token: 0x020001BE RID: 446
	[UidProperty("Uid")]
	public class UIElement : Visual, IAnimatable, IInputElement
	{
		/// <summary>Aplica uma animação a uma propriedade de dependência especificada neste elemento. Todas as animações existentes são interrompidas e substituídas pela nova animação.</summary>
		/// <param name="dp">O identificador para a propriedade a ser animada.</param>
		/// <param name="clock">O relógio de animação que controla e declara a animação.</param>
		// Token: 0x0600075F RID: 1887 RVA: 0x00021174 File Offset: 0x00020574
		public void ApplyAnimationClock(DependencyProperty dp, AnimationClock clock)
		{
			this.ApplyAnimationClock(dp, clock, HandoffBehavior.SnapshotAndReplace);
		}

		/// <summary>Aplica uma animação a uma propriedade de dependência especificada nesse elemento, com a capacidade de especificar o que ocorrerá se a propriedade já tiver uma animação em execução.</summary>
		/// <param name="dp">A propriedade a ser animada.</param>
		/// <param name="clock">O relógio de animação que controla e declara a animação.</param>
		/// <param name="handoffBehavior">Um valor da enumeração. O padrão é <see cref="F:System.Windows.Media.Animation.HandoffBehavior.SnapshotAndReplace" />, que interromperá a animação existente, substituindo-a pela nova.</param>
		// Token: 0x06000760 RID: 1888 RVA: 0x0002118C File Offset: 0x0002058C
		public void ApplyAnimationClock(DependencyProperty dp, AnimationClock clock, HandoffBehavior handoffBehavior)
		{
			if (dp == null)
			{
				throw new ArgumentNullException("dp");
			}
			if (!AnimationStorage.IsPropertyAnimatable(this, dp))
			{
				throw new ArgumentException(SR.Get("Animation_DependencyPropertyIsNotAnimatable", new object[]
				{
					dp.Name,
					base.GetType()
				}), "dp");
			}
			if (clock != null && !AnimationStorage.IsAnimationValid(dp, clock.Timeline))
			{
				throw new ArgumentException(SR.Get("Animation_AnimationTimelineTypeMismatch", new object[]
				{
					clock.Timeline.GetType(),
					dp.Name,
					dp.PropertyType
				}), "clock");
			}
			if (!HandoffBehaviorEnum.IsDefined(handoffBehavior))
			{
				throw new ArgumentException(SR.Get("Animation_UnrecognizedHandoffBehavior"));
			}
			if (base.IsSealed)
			{
				throw new InvalidOperationException(SR.Get("IAnimatable_CantAnimateSealedDO", new object[]
				{
					dp,
					base.GetType()
				}));
			}
			AnimationStorage.ApplyAnimationClock(this, dp, clock, handoffBehavior);
		}

		/// <summary>Inicia uma animação de uma propriedade animada especificada neste elemento.</summary>
		/// <param name="dp">A propriedade a ser animada, que é especificada como um identificador da propriedade de dependência.</param>
		/// <param name="animation">A linha do tempo da animação a ser iniciada.</param>
		// Token: 0x06000761 RID: 1889 RVA: 0x00021278 File Offset: 0x00020678
		public void BeginAnimation(DependencyProperty dp, AnimationTimeline animation)
		{
			this.BeginAnimation(dp, animation, HandoffBehavior.SnapshotAndReplace);
		}

		/// <summary>Inicia uma animação específica para uma propriedade animada especificada neste elemento, com a opção de especificar o que acontece se a propriedade já tiver uma animação em execução.</summary>
		/// <param name="dp">A propriedade a ser animada, que é especificada como o identificador da propriedade de dependência.</param>
		/// <param name="animation">A linha do tempo da animação a ser aplicada.</param>
		/// <param name="handoffBehavior">Um valor de enumeração que especifica como a nova animação interage com todas as animações atuais (em execução) que já estão afetando o valor da propriedade.</param>
		// Token: 0x06000762 RID: 1890 RVA: 0x00021290 File Offset: 0x00020690
		public void BeginAnimation(DependencyProperty dp, AnimationTimeline animation, HandoffBehavior handoffBehavior)
		{
			if (dp == null)
			{
				throw new ArgumentNullException("dp");
			}
			if (!AnimationStorage.IsPropertyAnimatable(this, dp))
			{
				throw new ArgumentException(SR.Get("Animation_DependencyPropertyIsNotAnimatable", new object[]
				{
					dp.Name,
					base.GetType()
				}), "dp");
			}
			if (animation != null && !AnimationStorage.IsAnimationValid(dp, animation))
			{
				throw new ArgumentException(SR.Get("Animation_AnimationTimelineTypeMismatch", new object[]
				{
					animation.GetType(),
					dp.Name,
					dp.PropertyType
				}), "animation");
			}
			if (!HandoffBehaviorEnum.IsDefined(handoffBehavior))
			{
				throw new ArgumentException(SR.Get("Animation_UnrecognizedHandoffBehavior"));
			}
			if (base.IsSealed)
			{
				throw new InvalidOperationException(SR.Get("IAnimatable_CantAnimateSealedDO", new object[]
				{
					dp,
					base.GetType()
				}));
			}
			AnimationStorage.BeginAnimation(this, dp, animation, handoffBehavior);
		}

		/// <summary>Obtém um valor que indica se este elemento tem todas as propriedades animadas.</summary>
		/// <returns>
		///   <see langword="true" /> se este elemento tem animações anexadas a uma de suas propriedades; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x00021370 File Offset: 0x00020770
		public bool HasAnimatedProperties
		{
			get
			{
				base.VerifyAccess();
				return base.IAnimatable_HasAnimatedProperties;
			}
		}

		/// <summary>Retorna o valor da propriedade base da propriedade especificada neste elemento, desconsiderando qualquer possível valor animado de uma animação parada ou em execução.</summary>
		/// <param name="dp">A propriedade de dependência a ser verificada.</param>
		/// <returns>O valor da propriedade como se não houvesse nenhuma animação anexada à propriedade de dependência especificada.</returns>
		// Token: 0x06000764 RID: 1892 RVA: 0x0002138C File Offset: 0x0002078C
		public object GetAnimationBaseValue(DependencyProperty dp)
		{
			if (dp == null)
			{
				throw new ArgumentNullException("dp");
			}
			return base.GetValueEntry(base.LookupEntry(dp.GlobalIndex), dp, null, RequestFlags.AnimationBaseValue).Value;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x000213C4 File Offset: 0x000207C4
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		internal sealed override void EvaluateAnimatedValueCore(DependencyProperty dp, PropertyMetadata metadata, ref EffectiveValueEntry entry)
		{
			if (base.IAnimatable_HasAnimatedProperties)
			{
				AnimationStorage storage = AnimationStorage.GetStorage(this, dp);
				if (storage != null)
				{
					storage.EvaluateAnimatedValue(metadata, ref entry);
				}
			}
		}

		/// <summary>Obtém a coleção de ligações de entrada associadas a este elemento.</summary>
		/// <returns>A coleção de ligações de entrada.</returns>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x000213EC File Offset: 0x000207EC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public InputBindingCollection InputBindings
		{
			get
			{
				base.VerifyAccess();
				InputBindingCollection inputBindingCollection = UIElement.InputBindingCollectionField.GetValue(this);
				if (inputBindingCollection == null)
				{
					inputBindingCollection = new InputBindingCollection(this);
					UIElement.InputBindingCollectionField.SetValue(this, inputBindingCollection);
				}
				return inputBindingCollection;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x00021424 File Offset: 0x00020824
		internal InputBindingCollection InputBindingsInternal
		{
			get
			{
				base.VerifyAccess();
				return UIElement.InputBindingCollectionField.GetValue(this);
			}
		}

		/// <summary>Indica se os processos de serialização devem serializar o conteúdo da propriedade <see cref="P:System.Windows.UIElement.InputBindings" /> em instâncias dessa classe.</summary>
		/// <returns>
		///   <see langword="true" /> se o valor da propriedade <see cref="P:System.Windows.UIElement.InputBindings" /> precisar ser serializado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000768 RID: 1896 RVA: 0x00021444 File Offset: 0x00020844
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeInputBindings()
		{
			InputBindingCollection value = UIElement.InputBindingCollectionField.GetValue(this);
			return value != null && value.Count > 0;
		}

		/// <summary>Obtém uma coleção de objetos <see cref="T:System.Windows.Input.CommandBinding" /> associados a esse elemento. Um <see cref="T:System.Windows.Input.CommandBinding" /> permite a manipulação de comando desse elemento e declara a ligação entre um comando, seus eventos e os manipuladores anexados por esse elemento.</summary>
		/// <returns>A coleção de todos os objetos <see cref="T:System.Windows.Input.CommandBinding" />.</returns>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0002146C File Offset: 0x0002086C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CommandBindingCollection CommandBindings
		{
			get
			{
				base.VerifyAccess();
				CommandBindingCollection commandBindingCollection = UIElement.CommandBindingCollectionField.GetValue(this);
				if (commandBindingCollection == null)
				{
					commandBindingCollection = new CommandBindingCollection();
					UIElement.CommandBindingCollectionField.SetValue(this, commandBindingCollection);
				}
				return commandBindingCollection;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x000214A4 File Offset: 0x000208A4
		internal CommandBindingCollection CommandBindingsInternal
		{
			get
			{
				base.VerifyAccess();
				return UIElement.CommandBindingCollectionField.GetValue(this);
			}
		}

		/// <summary>Indica se os processos de serialização devem serializar o conteúdo da propriedade <see cref="P:System.Windows.UIElement.CommandBindings" /> em instâncias dessa classe.</summary>
		/// <returns>
		///   <see langword="true" /> se o valor da propriedade <see cref="P:System.Windows.UIElement.CommandBindings" /> precisar ser serializado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600076B RID: 1899 RVA: 0x000214C4 File Offset: 0x000208C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeCommandBindings()
		{
			CommandBindingCollection value = UIElement.CommandBindingCollectionField.GetValue(this);
			return value != null && value.Count > 0;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000214EC File Offset: 0x000208EC
		internal virtual bool BuildRouteCore(EventRoute route, RoutedEventArgs args)
		{
			return false;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000214FC File Offset: 0x000208FC
		internal void BuildRoute(EventRoute route, RoutedEventArgs args)
		{
			UIElement.BuildRouteHelper(this, route, args);
		}

		/// <summary>Aciona um evento roteado específico. O <see cref="T:System.Windows.RoutedEvent" /> a ser gerado é identificado na instância <see cref="T:System.Windows.RoutedEventArgs" /> fornecida (como a propriedade <see cref="P:System.Windows.RoutedEventArgs.RoutedEvent" /> desses dados de eventos).</summary>
		/// <param name="e">Um <see cref="T:System.Windows.RoutedEventArgs" /> que contém os dados do evento e também identifica o evento a ser acionado.</param>
		// Token: 0x0600076E RID: 1902 RVA: 0x00021514 File Offset: 0x00020914
		public void RaiseEvent(RoutedEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			e.ClearUserInitiated();
			UIElement.RaiseEventImpl(this, e);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0002153C File Offset: 0x0002093C
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

		// Token: 0x06000770 RID: 1904 RVA: 0x00021570 File Offset: 0x00020970
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

		// Token: 0x06000771 RID: 1905 RVA: 0x000215C0 File Offset: 0x000209C0
		internal virtual object AdjustEventSource(RoutedEventArgs args)
		{
			return null;
		}

		/// <summary>Adiciona um manipulador de eventos roteados de um evento roteado especificado, adicionando o manipulador à coleção de manipuladores no elemento atual.</summary>
		/// <param name="routedEvent">Um identificador do evento roteado a ser manipulado.</param>
		/// <param name="handler">Uma referência à implementação do manipulador.</param>
		// Token: 0x06000772 RID: 1906 RVA: 0x000215D0 File Offset: 0x000209D0
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
		// Token: 0x06000773 RID: 1907 RVA: 0x000215E8 File Offset: 0x000209E8
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

		// Token: 0x06000774 RID: 1908 RVA: 0x00021648 File Offset: 0x00020A48
		internal virtual void OnAddHandler(RoutedEvent routedEvent, Delegate handler)
		{
		}

		/// <summary>Remove o manipulador de eventos roteados especificado desse elemento.</summary>
		/// <param name="routedEvent">O identificador do evento roteado ao qual o manipulador está anexado.</param>
		/// <param name="handler">A implementação do manipulador específico para remover da coleção de manipuladores de eventos neste elemento.</param>
		// Token: 0x06000775 RID: 1909 RVA: 0x00021658 File Offset: 0x00020A58
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
					UIElement.EventHandlersStoreField.ClearValue(this);
					this.WriteFlag(CoreFlags.ExistsEventHandlersStore, false);
				}
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000216D4 File Offset: 0x00020AD4
		internal virtual void OnRemoveHandler(RoutedEvent routedEvent, Delegate handler)
		{
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000216E4 File Offset: 0x00020AE4
		private void EventHandlersStoreAdd(EventPrivateKey key, Delegate handler)
		{
			this.EnsureEventHandlersStore();
			this.EventHandlersStore.Add(key, handler);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00021704 File Offset: 0x00020B04
		private void EventHandlersStoreRemove(EventPrivateKey key, Delegate handler)
		{
			EventHandlersStore eventHandlersStore = this.EventHandlersStore;
			if (eventHandlersStore != null)
			{
				eventHandlersStore.Remove(key, handler);
				if (eventHandlersStore.Count == 0)
				{
					UIElement.EventHandlersStoreField.ClearValue(this);
					this.WriteFlag(CoreFlags.ExistsEventHandlersStore, false);
				}
			}
		}

		/// <summary>Adiciona manipuladores ao <see cref="T:System.Windows.EventRoute" /> especificado para a coleção do manipulador de eventos <see cref="T:System.Windows.UIElement" /> atual.</summary>
		/// <param name="route">A rota de eventos à qual os manipuladores são adicionados.</param>
		/// <param name="e">Os dados de evento usados para adicionar os manipuladores. Esse método usa a propriedade <see cref="P:System.Windows.RoutedEventArgs.RoutedEvent" /> dos dados do evento para criar os manipuladores.</param>
		// Token: 0x06000779 RID: 1913 RVA: 0x00021744 File Offset: 0x00020B44
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

		// Token: 0x0600077A RID: 1914 RVA: 0x00021828 File Offset: 0x00020C28
		internal virtual void AddToEventRouteCore(EventRoute route, RoutedEventArgs args)
		{
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x00021838 File Offset: 0x00020C38
		internal EventHandlersStore EventHandlersStore
		{
			[FriendAccessAllowed]
			get
			{
				if (!this.ReadFlag(CoreFlags.ExistsEventHandlersStore))
				{
					return null;
				}
				return UIElement.EventHandlersStoreField.GetValue(this);
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00021860 File Offset: 0x00020C60
		[FriendAccessAllowed]
		internal void EnsureEventHandlersStore()
		{
			if (this.EventHandlersStore == null)
			{
				UIElement.EventHandlersStoreField.SetValue(this, new EventHandlersStore());
				this.WriteFlag(CoreFlags.ExistsEventHandlersStore, true);
			}
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00021894 File Offset: 0x00020C94
		internal virtual bool InvalidateAutomationAncestorsCore(Stack<DependencyObject> branchNodeStack, out bool continuePastVisualTree)
		{
			continuePastVisualTree = false;
			return true;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x000218A8 File Offset: 0x00020CA8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static void RegisterEvents(Type type)
		{
			EventManager.RegisterClassHandler(type, Mouse.PreviewMouseDownEvent, new MouseButtonEventHandler(UIElement.OnPreviewMouseDownThunk), true);
			EventManager.RegisterClassHandler(type, Mouse.MouseDownEvent, new MouseButtonEventHandler(UIElement.OnMouseDownThunk), true);
			EventManager.RegisterClassHandler(type, Mouse.PreviewMouseUpEvent, new MouseButtonEventHandler(UIElement.OnPreviewMouseUpThunk), true);
			EventManager.RegisterClassHandler(type, Mouse.MouseUpEvent, new MouseButtonEventHandler(UIElement.OnMouseUpThunk), true);
			EventManager.RegisterClassHandler(type, UIElement.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(UIElement.OnPreviewMouseLeftButtonDownThunk), false);
			EventManager.RegisterClassHandler(type, UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(UIElement.OnMouseLeftButtonDownThunk), false);
			EventManager.RegisterClassHandler(type, UIElement.PreviewMouseLeftButtonUpEvent, new MouseButtonEventHandler(UIElement.OnPreviewMouseLeftButtonUpThunk), false);
			EventManager.RegisterClassHandler(type, UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(UIElement.OnMouseLeftButtonUpThunk), false);
			EventManager.RegisterClassHandler(type, UIElement.PreviewMouseRightButtonDownEvent, new MouseButtonEventHandler(UIElement.OnPreviewMouseRightButtonDownThunk), false);
			EventManager.RegisterClassHandler(type, UIElement.MouseRightButtonDownEvent, new MouseButtonEventHandler(UIElement.OnMouseRightButtonDownThunk), false);
			EventManager.RegisterClassHandler(type, UIElement.PreviewMouseRightButtonUpEvent, new MouseButtonEventHandler(UIElement.OnPreviewMouseRightButtonUpThunk), false);
			EventManager.RegisterClassHandler(type, UIElement.MouseRightButtonUpEvent, new MouseButtonEventHandler(UIElement.OnMouseRightButtonUpThunk), false);
			EventManager.RegisterClassHandler(type, Mouse.PreviewMouseMoveEvent, new MouseEventHandler(UIElement.OnPreviewMouseMoveThunk), false);
			EventManager.RegisterClassHandler(type, Mouse.MouseMoveEvent, new MouseEventHandler(UIElement.OnMouseMoveThunk), false);
			EventManager.RegisterClassHandler(type, Mouse.PreviewMouseWheelEvent, new MouseWheelEventHandler(UIElement.OnPreviewMouseWheelThunk), false);
			EventManager.RegisterClassHandler(type, Mouse.MouseWheelEvent, new MouseWheelEventHandler(UIElement.OnMouseWheelThunk), false);
			EventManager.RegisterClassHandler(type, Mouse.MouseEnterEvent, new MouseEventHandler(UIElement.OnMouseEnterThunk), false);
			EventManager.RegisterClassHandler(type, Mouse.MouseLeaveEvent, new MouseEventHandler(UIElement.OnMouseLeaveThunk), false);
			EventManager.RegisterClassHandler(type, Mouse.GotMouseCaptureEvent, new MouseEventHandler(UIElement.OnGotMouseCaptureThunk), false);
			EventManager.RegisterClassHandler(type, Mouse.LostMouseCaptureEvent, new MouseEventHandler(UIElement.OnLostMouseCaptureThunk), false);
			EventManager.RegisterClassHandler(type, Mouse.QueryCursorEvent, new QueryCursorEventHandler(UIElement.OnQueryCursorThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.PreviewStylusDownEvent, new StylusDownEventHandler(UIElement.OnPreviewStylusDownThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.StylusDownEvent, new StylusDownEventHandler(UIElement.OnStylusDownThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.PreviewStylusUpEvent, new StylusEventHandler(UIElement.OnPreviewStylusUpThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.StylusUpEvent, new StylusEventHandler(UIElement.OnStylusUpThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.PreviewStylusMoveEvent, new StylusEventHandler(UIElement.OnPreviewStylusMoveThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.StylusMoveEvent, new StylusEventHandler(UIElement.OnStylusMoveThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.PreviewStylusInAirMoveEvent, new StylusEventHandler(UIElement.OnPreviewStylusInAirMoveThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.StylusInAirMoveEvent, new StylusEventHandler(UIElement.OnStylusInAirMoveThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.StylusEnterEvent, new StylusEventHandler(UIElement.OnStylusEnterThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.StylusLeaveEvent, new StylusEventHandler(UIElement.OnStylusLeaveThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.PreviewStylusInRangeEvent, new StylusEventHandler(UIElement.OnPreviewStylusInRangeThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.StylusInRangeEvent, new StylusEventHandler(UIElement.OnStylusInRangeThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.PreviewStylusOutOfRangeEvent, new StylusEventHandler(UIElement.OnPreviewStylusOutOfRangeThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.StylusOutOfRangeEvent, new StylusEventHandler(UIElement.OnStylusOutOfRangeThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.PreviewStylusSystemGestureEvent, new StylusSystemGestureEventHandler(UIElement.OnPreviewStylusSystemGestureThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.StylusSystemGestureEvent, new StylusSystemGestureEventHandler(UIElement.OnStylusSystemGestureThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.GotStylusCaptureEvent, new StylusEventHandler(UIElement.OnGotStylusCaptureThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.LostStylusCaptureEvent, new StylusEventHandler(UIElement.OnLostStylusCaptureThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.StylusButtonDownEvent, new StylusButtonEventHandler(UIElement.OnStylusButtonDownThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.StylusButtonUpEvent, new StylusButtonEventHandler(UIElement.OnStylusButtonUpThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.PreviewStylusButtonDownEvent, new StylusButtonEventHandler(UIElement.OnPreviewStylusButtonDownThunk), false);
			EventManager.RegisterClassHandler(type, Stylus.PreviewStylusButtonUpEvent, new StylusButtonEventHandler(UIElement.OnPreviewStylusButtonUpThunk), false);
			EventManager.RegisterClassHandler(type, Keyboard.PreviewKeyDownEvent, new KeyEventHandler(UIElement.OnPreviewKeyDownThunk), false);
			EventManager.RegisterClassHandler(type, Keyboard.KeyDownEvent, new KeyEventHandler(UIElement.OnKeyDownThunk), false);
			EventManager.RegisterClassHandler(type, Keyboard.PreviewKeyUpEvent, new KeyEventHandler(UIElement.OnPreviewKeyUpThunk), false);
			EventManager.RegisterClassHandler(type, Keyboard.KeyUpEvent, new KeyEventHandler(UIElement.OnKeyUpThunk), false);
			EventManager.RegisterClassHandler(type, Keyboard.PreviewGotKeyboardFocusEvent, new KeyboardFocusChangedEventHandler(UIElement.OnPreviewGotKeyboardFocusThunk), false);
			EventManager.RegisterClassHandler(type, Keyboard.GotKeyboardFocusEvent, new KeyboardFocusChangedEventHandler(UIElement.OnGotKeyboardFocusThunk), false);
			EventManager.RegisterClassHandler(type, Keyboard.PreviewLostKeyboardFocusEvent, new KeyboardFocusChangedEventHandler(UIElement.OnPreviewLostKeyboardFocusThunk), false);
			EventManager.RegisterClassHandler(type, Keyboard.LostKeyboardFocusEvent, new KeyboardFocusChangedEventHandler(UIElement.OnLostKeyboardFocusThunk), false);
			EventManager.RegisterClassHandler(type, TextCompositionManager.PreviewTextInputEvent, new TextCompositionEventHandler(UIElement.OnPreviewTextInputThunk), false);
			EventManager.RegisterClassHandler(type, TextCompositionManager.TextInputEvent, new TextCompositionEventHandler(UIElement.OnTextInputThunk), false);
			EventManager.RegisterClassHandler(type, CommandManager.PreviewExecutedEvent, new ExecutedRoutedEventHandler(UIElement.OnPreviewExecutedThunk), false);
			EventManager.RegisterClassHandler(type, CommandManager.ExecutedEvent, new ExecutedRoutedEventHandler(UIElement.OnExecutedThunk), false);
			EventManager.RegisterClassHandler(type, CommandManager.PreviewCanExecuteEvent, new CanExecuteRoutedEventHandler(UIElement.OnPreviewCanExecuteThunk), false);
			EventManager.RegisterClassHandler(type, CommandManager.CanExecuteEvent, new CanExecuteRoutedEventHandler(UIElement.OnCanExecuteThunk), false);
			EventManager.RegisterClassHandler(type, CommandDevice.CommandDeviceEvent, new CommandDeviceEventHandler(UIElement.OnCommandDeviceThunk), false);
			EventManager.RegisterClassHandler(type, DragDrop.PreviewQueryContinueDragEvent, new QueryContinueDragEventHandler(UIElement.OnPreviewQueryContinueDragThunk), false);
			EventManager.RegisterClassHandler(type, DragDrop.QueryContinueDragEvent, new QueryContinueDragEventHandler(UIElement.OnQueryContinueDragThunk), false);
			EventManager.RegisterClassHandler(type, DragDrop.PreviewGiveFeedbackEvent, new GiveFeedbackEventHandler(UIElement.OnPreviewGiveFeedbackThunk), false);
			EventManager.RegisterClassHandler(type, DragDrop.GiveFeedbackEvent, new GiveFeedbackEventHandler(UIElement.OnGiveFeedbackThunk), false);
			EventManager.RegisterClassHandler(type, DragDrop.PreviewDragEnterEvent, new DragEventHandler(UIElement.OnPreviewDragEnterThunk), false);
			EventManager.RegisterClassHandler(type, DragDrop.DragEnterEvent, new DragEventHandler(UIElement.OnDragEnterThunk), false);
			EventManager.RegisterClassHandler(type, DragDrop.PreviewDragOverEvent, new DragEventHandler(UIElement.OnPreviewDragOverThunk), false);
			EventManager.RegisterClassHandler(type, DragDrop.DragOverEvent, new DragEventHandler(UIElement.OnDragOverThunk), false);
			EventManager.RegisterClassHandler(type, DragDrop.PreviewDragLeaveEvent, new DragEventHandler(UIElement.OnPreviewDragLeaveThunk), false);
			EventManager.RegisterClassHandler(type, DragDrop.DragLeaveEvent, new DragEventHandler(UIElement.OnDragLeaveThunk), false);
			EventManager.RegisterClassHandler(type, DragDrop.PreviewDropEvent, new DragEventHandler(UIElement.OnPreviewDropThunk), false);
			EventManager.RegisterClassHandler(type, DragDrop.DropEvent, new DragEventHandler(UIElement.OnDropThunk), false);
			EventManager.RegisterClassHandler(type, Touch.PreviewTouchDownEvent, new EventHandler<TouchEventArgs>(UIElement.OnPreviewTouchDownThunk), false);
			EventManager.RegisterClassHandler(type, Touch.TouchDownEvent, new EventHandler<TouchEventArgs>(UIElement.OnTouchDownThunk), false);
			EventManager.RegisterClassHandler(type, Touch.PreviewTouchMoveEvent, new EventHandler<TouchEventArgs>(UIElement.OnPreviewTouchMoveThunk), false);
			EventManager.RegisterClassHandler(type, Touch.TouchMoveEvent, new EventHandler<TouchEventArgs>(UIElement.OnTouchMoveThunk), false);
			EventManager.RegisterClassHandler(type, Touch.PreviewTouchUpEvent, new EventHandler<TouchEventArgs>(UIElement.OnPreviewTouchUpThunk), false);
			EventManager.RegisterClassHandler(type, Touch.TouchUpEvent, new EventHandler<TouchEventArgs>(UIElement.OnTouchUpThunk), false);
			EventManager.RegisterClassHandler(type, Touch.GotTouchCaptureEvent, new EventHandler<TouchEventArgs>(UIElement.OnGotTouchCaptureThunk), false);
			EventManager.RegisterClassHandler(type, Touch.LostTouchCaptureEvent, new EventHandler<TouchEventArgs>(UIElement.OnLostTouchCaptureThunk), false);
			EventManager.RegisterClassHandler(type, Touch.TouchEnterEvent, new EventHandler<TouchEventArgs>(UIElement.OnTouchEnterThunk), false);
			EventManager.RegisterClassHandler(type, Touch.TouchLeaveEvent, new EventHandler<TouchEventArgs>(UIElement.OnTouchLeaveThunk), false);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00022038 File Offset: 0x00021438
		[SecurityCritical]
		private static void OnPreviewMouseDownThunk(object sender, MouseButtonEventArgs e)
		{
			if (!e.Handled)
			{
				UIElement uielement = sender as UIElement;
				if (uielement != null)
				{
					uielement.OnPreviewMouseDown(e);
				}
				else
				{
					ContentElement contentElement = sender as ContentElement;
					if (contentElement != null)
					{
						contentElement.OnPreviewMouseDown(e);
					}
					else
					{
						((UIElement3D)sender).OnPreviewMouseDown(e);
					}
				}
			}
			UIElement.CrackMouseButtonEventAndReRaiseEvent((DependencyObject)sender, e);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0002208C File Offset: 0x0002148C
		[SecurityCritical]
		private static void OnMouseDownThunk(object sender, MouseButtonEventArgs e)
		{
			if (!e.Handled)
			{
				CommandManager.TranslateInput((IInputElement)sender, e);
			}
			if (!e.Handled)
			{
				UIElement uielement = sender as UIElement;
				if (uielement != null)
				{
					uielement.OnMouseDown(e);
				}
				else
				{
					ContentElement contentElement = sender as ContentElement;
					if (contentElement != null)
					{
						contentElement.OnMouseDown(e);
					}
					else
					{
						((UIElement3D)sender).OnMouseDown(e);
					}
				}
			}
			UIElement.CrackMouseButtonEventAndReRaiseEvent((DependencyObject)sender, e);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x000220F4 File Offset: 0x000214F4
		[SecurityCritical]
		private static void OnPreviewMouseUpThunk(object sender, MouseButtonEventArgs e)
		{
			if (!e.Handled)
			{
				UIElement uielement = sender as UIElement;
				if (uielement != null)
				{
					uielement.OnPreviewMouseUp(e);
				}
				else
				{
					ContentElement contentElement = sender as ContentElement;
					if (contentElement != null)
					{
						contentElement.OnPreviewMouseUp(e);
					}
					else
					{
						((UIElement3D)sender).OnPreviewMouseUp(e);
					}
				}
			}
			UIElement.CrackMouseButtonEventAndReRaiseEvent((DependencyObject)sender, e);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00022148 File Offset: 0x00021548
		[SecurityCritical]
		private static void OnMouseUpThunk(object sender, MouseButtonEventArgs e)
		{
			if (!e.Handled)
			{
				UIElement uielement = sender as UIElement;
				if (uielement != null)
				{
					uielement.OnMouseUp(e);
				}
				else
				{
					ContentElement contentElement = sender as ContentElement;
					if (contentElement != null)
					{
						contentElement.OnMouseUp(e);
					}
					else
					{
						((UIElement3D)sender).OnMouseUp(e);
					}
				}
			}
			UIElement.CrackMouseButtonEventAndReRaiseEvent((DependencyObject)sender, e);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0002219C File Offset: 0x0002159C
		[SecurityCritical]
		private static void OnPreviewMouseLeftButtonDownThunk(object sender, MouseButtonEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewMouseLeftButtonDown(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewMouseLeftButtonDown(e);
				return;
			}
			((UIElement3D)sender).OnPreviewMouseLeftButtonDown(e);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x000221EC File Offset: 0x000215EC
		[SecurityCritical]
		private static void OnMouseLeftButtonDownThunk(object sender, MouseButtonEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnMouseLeftButtonDown(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnMouseLeftButtonDown(e);
				return;
			}
			((UIElement3D)sender).OnMouseLeftButtonDown(e);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0002223C File Offset: 0x0002163C
		[SecurityCritical]
		private static void OnPreviewMouseLeftButtonUpThunk(object sender, MouseButtonEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewMouseLeftButtonUp(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewMouseLeftButtonUp(e);
				return;
			}
			((UIElement3D)sender).OnPreviewMouseLeftButtonUp(e);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0002228C File Offset: 0x0002168C
		[SecurityCritical]
		private static void OnMouseLeftButtonUpThunk(object sender, MouseButtonEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnMouseLeftButtonUp(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnMouseLeftButtonUp(e);
				return;
			}
			((UIElement3D)sender).OnMouseLeftButtonUp(e);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x000222DC File Offset: 0x000216DC
		[SecurityCritical]
		private static void OnPreviewMouseRightButtonDownThunk(object sender, MouseButtonEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewMouseRightButtonDown(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewMouseRightButtonDown(e);
				return;
			}
			((UIElement3D)sender).OnPreviewMouseRightButtonDown(e);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0002232C File Offset: 0x0002172C
		[SecurityCritical]
		private static void OnMouseRightButtonDownThunk(object sender, MouseButtonEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnMouseRightButtonDown(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnMouseRightButtonDown(e);
				return;
			}
			((UIElement3D)sender).OnMouseRightButtonDown(e);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0002237C File Offset: 0x0002177C
		[SecurityCritical]
		private static void OnPreviewMouseRightButtonUpThunk(object sender, MouseButtonEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewMouseRightButtonUp(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewMouseRightButtonUp(e);
				return;
			}
			((UIElement3D)sender).OnPreviewMouseRightButtonUp(e);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x000223CC File Offset: 0x000217CC
		[SecurityCritical]
		private static void OnMouseRightButtonUpThunk(object sender, MouseButtonEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnMouseRightButtonUp(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnMouseRightButtonUp(e);
				return;
			}
			((UIElement3D)sender).OnMouseRightButtonUp(e);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0002241C File Offset: 0x0002181C
		[SecurityCritical]
		private static void OnPreviewMouseMoveThunk(object sender, MouseEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewMouseMove(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewMouseMove(e);
				return;
			}
			((UIElement3D)sender).OnPreviewMouseMove(e);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0002246C File Offset: 0x0002186C
		[SecurityCritical]
		private static void OnMouseMoveThunk(object sender, MouseEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnMouseMove(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnMouseMove(e);
				return;
			}
			((UIElement3D)sender).OnMouseMove(e);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x000224BC File Offset: 0x000218BC
		[SecurityCritical]
		private static void OnPreviewMouseWheelThunk(object sender, MouseWheelEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewMouseWheel(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewMouseWheel(e);
				return;
			}
			((UIElement3D)sender).OnPreviewMouseWheel(e);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0002250C File Offset: 0x0002190C
		[SecurityCritical]
		private static void OnMouseWheelThunk(object sender, MouseWheelEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			CommandManager.TranslateInput((IInputElement)sender, e);
			if (!e.Handled)
			{
				UIElement uielement = sender as UIElement;
				if (uielement != null)
				{
					uielement.OnMouseWheel(e);
					return;
				}
				ContentElement contentElement = sender as ContentElement;
				if (contentElement != null)
				{
					contentElement.OnMouseWheel(e);
					return;
				}
				((UIElement3D)sender).OnMouseWheel(e);
			}
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00022570 File Offset: 0x00021970
		[SecurityCritical]
		private static void OnMouseEnterThunk(object sender, MouseEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnMouseEnter(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnMouseEnter(e);
				return;
			}
			((UIElement3D)sender).OnMouseEnter(e);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000225C0 File Offset: 0x000219C0
		[SecurityCritical]
		private static void OnMouseLeaveThunk(object sender, MouseEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnMouseLeave(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnMouseLeave(e);
				return;
			}
			((UIElement3D)sender).OnMouseLeave(e);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00022610 File Offset: 0x00021A10
		[SecurityCritical]
		private static void OnGotMouseCaptureThunk(object sender, MouseEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnGotMouseCapture(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnGotMouseCapture(e);
				return;
			}
			((UIElement3D)sender).OnGotMouseCapture(e);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00022660 File Offset: 0x00021A60
		[SecurityCritical]
		private static void OnLostMouseCaptureThunk(object sender, MouseEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnLostMouseCapture(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnLostMouseCapture(e);
				return;
			}
			((UIElement3D)sender).OnLostMouseCapture(e);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x000226B0 File Offset: 0x00021AB0
		[SecurityCritical]
		private static void OnQueryCursorThunk(object sender, QueryCursorEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnQueryCursor(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnQueryCursor(e);
				return;
			}
			((UIElement3D)sender).OnQueryCursor(e);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00022700 File Offset: 0x00021B00
		[SecurityCritical]
		private static void OnPreviewStylusDownThunk(object sender, StylusDownEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewStylusDown(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewStylusDown(e);
				return;
			}
			((UIElement3D)sender).OnPreviewStylusDown(e);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00022750 File Offset: 0x00021B50
		[SecurityCritical]
		private static void OnStylusDownThunk(object sender, StylusDownEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnStylusDown(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnStylusDown(e);
				return;
			}
			((UIElement3D)sender).OnStylusDown(e);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x000227A0 File Offset: 0x00021BA0
		[SecurityCritical]
		private static void OnPreviewStylusUpThunk(object sender, StylusEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewStylusUp(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewStylusUp(e);
				return;
			}
			((UIElement3D)sender).OnPreviewStylusUp(e);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x000227F0 File Offset: 0x00021BF0
		[SecurityCritical]
		private static void OnStylusUpThunk(object sender, StylusEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnStylusUp(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnStylusUp(e);
				return;
			}
			((UIElement3D)sender).OnStylusUp(e);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00022840 File Offset: 0x00021C40
		[SecurityCritical]
		private static void OnPreviewStylusMoveThunk(object sender, StylusEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewStylusMove(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewStylusMove(e);
				return;
			}
			((UIElement3D)sender).OnPreviewStylusMove(e);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00022890 File Offset: 0x00021C90
		[SecurityCritical]
		private static void OnStylusMoveThunk(object sender, StylusEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnStylusMove(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnStylusMove(e);
				return;
			}
			((UIElement3D)sender).OnStylusMove(e);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x000228E0 File Offset: 0x00021CE0
		[SecurityCritical]
		private static void OnPreviewStylusInAirMoveThunk(object sender, StylusEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewStylusInAirMove(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewStylusInAirMove(e);
				return;
			}
			((UIElement3D)sender).OnPreviewStylusInAirMove(e);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00022930 File Offset: 0x00021D30
		[SecurityCritical]
		private static void OnStylusInAirMoveThunk(object sender, StylusEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnStylusInAirMove(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnStylusInAirMove(e);
				return;
			}
			((UIElement3D)sender).OnStylusInAirMove(e);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00022980 File Offset: 0x00021D80
		[SecurityCritical]
		private static void OnStylusEnterThunk(object sender, StylusEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnStylusEnter(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnStylusEnter(e);
				return;
			}
			((UIElement3D)sender).OnStylusEnter(e);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x000229D0 File Offset: 0x00021DD0
		[SecurityCritical]
		private static void OnStylusLeaveThunk(object sender, StylusEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnStylusLeave(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnStylusLeave(e);
				return;
			}
			((UIElement3D)sender).OnStylusLeave(e);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00022A20 File Offset: 0x00021E20
		[SecurityCritical]
		private static void OnPreviewStylusInRangeThunk(object sender, StylusEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewStylusInRange(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewStylusInRange(e);
				return;
			}
			((UIElement3D)sender).OnPreviewStylusInRange(e);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00022A70 File Offset: 0x00021E70
		[SecurityCritical]
		private static void OnStylusInRangeThunk(object sender, StylusEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnStylusInRange(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnStylusInRange(e);
				return;
			}
			((UIElement3D)sender).OnStylusInRange(e);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00022AC0 File Offset: 0x00021EC0
		[SecurityCritical]
		private static void OnPreviewStylusOutOfRangeThunk(object sender, StylusEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewStylusOutOfRange(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewStylusOutOfRange(e);
				return;
			}
			((UIElement3D)sender).OnPreviewStylusOutOfRange(e);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00022B10 File Offset: 0x00021F10
		[SecurityCritical]
		private static void OnStylusOutOfRangeThunk(object sender, StylusEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnStylusOutOfRange(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnStylusOutOfRange(e);
				return;
			}
			((UIElement3D)sender).OnStylusOutOfRange(e);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00022B60 File Offset: 0x00021F60
		[SecurityCritical]
		private static void OnPreviewStylusSystemGestureThunk(object sender, StylusSystemGestureEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewStylusSystemGesture(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewStylusSystemGesture(e);
				return;
			}
			((UIElement3D)sender).OnPreviewStylusSystemGesture(e);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00022BB0 File Offset: 0x00021FB0
		[SecurityCritical]
		private static void OnStylusSystemGestureThunk(object sender, StylusSystemGestureEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnStylusSystemGesture(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnStylusSystemGesture(e);
				return;
			}
			((UIElement3D)sender).OnStylusSystemGesture(e);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00022C00 File Offset: 0x00022000
		[SecurityCritical]
		private static void OnGotStylusCaptureThunk(object sender, StylusEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnGotStylusCapture(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnGotStylusCapture(e);
				return;
			}
			((UIElement3D)sender).OnGotStylusCapture(e);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00022C50 File Offset: 0x00022050
		[SecurityCritical]
		private static void OnLostStylusCaptureThunk(object sender, StylusEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnLostStylusCapture(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnLostStylusCapture(e);
				return;
			}
			((UIElement3D)sender).OnLostStylusCapture(e);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00022CA0 File Offset: 0x000220A0
		[SecurityCritical]
		private static void OnStylusButtonDownThunk(object sender, StylusButtonEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnStylusButtonDown(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnStylusButtonDown(e);
				return;
			}
			((UIElement3D)sender).OnStylusButtonDown(e);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00022CF0 File Offset: 0x000220F0
		[SecurityCritical]
		private static void OnStylusButtonUpThunk(object sender, StylusButtonEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnStylusButtonUp(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnStylusButtonUp(e);
				return;
			}
			((UIElement3D)sender).OnStylusButtonUp(e);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00022D40 File Offset: 0x00022140
		[SecurityCritical]
		private static void OnPreviewStylusButtonDownThunk(object sender, StylusButtonEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewStylusButtonDown(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewStylusButtonDown(e);
				return;
			}
			((UIElement3D)sender).OnPreviewStylusButtonDown(e);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00022D90 File Offset: 0x00022190
		[SecurityCritical]
		private static void OnPreviewStylusButtonUpThunk(object sender, StylusButtonEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewStylusButtonUp(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewStylusButtonUp(e);
				return;
			}
			((UIElement3D)sender).OnPreviewStylusButtonUp(e);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00022DE0 File Offset: 0x000221E0
		[SecurityCritical]
		private static void OnPreviewKeyDownThunk(object sender, KeyEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewKeyDown(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewKeyDown(e);
				return;
			}
			((UIElement3D)sender).OnPreviewKeyDown(e);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00022E30 File Offset: 0x00022230
		[SecurityCritical]
		private static void OnKeyDownThunk(object sender, KeyEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			CommandManager.TranslateInput((IInputElement)sender, e);
			if (!e.Handled)
			{
				UIElement uielement = sender as UIElement;
				if (uielement != null)
				{
					uielement.OnKeyDown(e);
					return;
				}
				ContentElement contentElement = sender as ContentElement;
				if (contentElement != null)
				{
					contentElement.OnKeyDown(e);
					return;
				}
				((UIElement3D)sender).OnKeyDown(e);
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00022E94 File Offset: 0x00022294
		[SecurityCritical]
		private static void OnPreviewKeyUpThunk(object sender, KeyEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewKeyUp(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewKeyUp(e);
				return;
			}
			((UIElement3D)sender).OnPreviewKeyUp(e);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00022EE4 File Offset: 0x000222E4
		[SecurityCritical]
		private static void OnKeyUpThunk(object sender, KeyEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnKeyUp(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnKeyUp(e);
				return;
			}
			((UIElement3D)sender).OnKeyUp(e);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00022F34 File Offset: 0x00022334
		[SecurityCritical]
		private static void OnPreviewGotKeyboardFocusThunk(object sender, KeyboardFocusChangedEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewGotKeyboardFocus(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewGotKeyboardFocus(e);
				return;
			}
			((UIElement3D)sender).OnPreviewGotKeyboardFocus(e);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00022F84 File Offset: 0x00022384
		[SecurityCritical]
		private static void OnGotKeyboardFocusThunk(object sender, KeyboardFocusChangedEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnGotKeyboardFocus(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnGotKeyboardFocus(e);
				return;
			}
			((UIElement3D)sender).OnGotKeyboardFocus(e);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00022FD4 File Offset: 0x000223D4
		[SecurityCritical]
		private static void OnPreviewLostKeyboardFocusThunk(object sender, KeyboardFocusChangedEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewLostKeyboardFocus(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewLostKeyboardFocus(e);
				return;
			}
			((UIElement3D)sender).OnPreviewLostKeyboardFocus(e);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00023024 File Offset: 0x00022424
		[SecurityCritical]
		private static void OnLostKeyboardFocusThunk(object sender, KeyboardFocusChangedEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnLostKeyboardFocus(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnLostKeyboardFocus(e);
				return;
			}
			((UIElement3D)sender).OnLostKeyboardFocus(e);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00023074 File Offset: 0x00022474
		[SecurityCritical]
		private static void OnPreviewTextInputThunk(object sender, TextCompositionEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewTextInput(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewTextInput(e);
				return;
			}
			((UIElement3D)sender).OnPreviewTextInput(e);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x000230C4 File Offset: 0x000224C4
		[SecurityCritical]
		private static void OnTextInputThunk(object sender, TextCompositionEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnTextInput(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnTextInput(e);
				return;
			}
			((UIElement3D)sender).OnTextInput(e);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00023114 File Offset: 0x00022514
		[SecurityCritical]
		private static void OnPreviewExecutedThunk(object sender, ExecutedRoutedEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			CommandManager.OnExecuted(sender, e);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0002313C File Offset: 0x0002253C
		[SecurityCritical]
		private static void OnExecutedThunk(object sender, ExecutedRoutedEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			CommandManager.OnExecuted(sender, e);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00023164 File Offset: 0x00022564
		[SecurityCritical]
		private static void OnPreviewCanExecuteThunk(object sender, CanExecuteRoutedEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			CommandManager.OnCanExecute(sender, e);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0002318C File Offset: 0x0002258C
		[SecurityCritical]
		private static void OnCanExecuteThunk(object sender, CanExecuteRoutedEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			CommandManager.OnCanExecute(sender, e);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x000231B4 File Offset: 0x000225B4
		[SecurityCritical]
		private static void OnCommandDeviceThunk(object sender, CommandDeviceEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			CommandManager.OnCommandDevice(sender, e);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x000231DC File Offset: 0x000225DC
		[SecurityCritical]
		private static void OnPreviewQueryContinueDragThunk(object sender, QueryContinueDragEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewQueryContinueDrag(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewQueryContinueDrag(e);
				return;
			}
			((UIElement3D)sender).OnPreviewQueryContinueDrag(e);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0002322C File Offset: 0x0002262C
		[SecurityCritical]
		private static void OnQueryContinueDragThunk(object sender, QueryContinueDragEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnQueryContinueDrag(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnQueryContinueDrag(e);
				return;
			}
			((UIElement3D)sender).OnQueryContinueDrag(e);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0002327C File Offset: 0x0002267C
		[SecurityCritical]
		private static void OnPreviewGiveFeedbackThunk(object sender, GiveFeedbackEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewGiveFeedback(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewGiveFeedback(e);
				return;
			}
			((UIElement3D)sender).OnPreviewGiveFeedback(e);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x000232CC File Offset: 0x000226CC
		[SecurityCritical]
		private static void OnGiveFeedbackThunk(object sender, GiveFeedbackEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnGiveFeedback(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnGiveFeedback(e);
				return;
			}
			((UIElement3D)sender).OnGiveFeedback(e);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0002331C File Offset: 0x0002271C
		[SecurityCritical]
		private static void OnPreviewDragEnterThunk(object sender, DragEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewDragEnter(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewDragEnter(e);
				return;
			}
			((UIElement3D)sender).OnPreviewDragEnter(e);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0002336C File Offset: 0x0002276C
		[SecurityCritical]
		private static void OnDragEnterThunk(object sender, DragEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnDragEnter(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnDragEnter(e);
				return;
			}
			((UIElement3D)sender).OnDragEnter(e);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x000233BC File Offset: 0x000227BC
		[SecurityCritical]
		private static void OnPreviewDragOverThunk(object sender, DragEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewDragOver(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewDragOver(e);
				return;
			}
			((UIElement3D)sender).OnPreviewDragOver(e);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0002340C File Offset: 0x0002280C
		[SecurityCritical]
		private static void OnDragOverThunk(object sender, DragEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnDragOver(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnDragOver(e);
				return;
			}
			((UIElement3D)sender).OnDragOver(e);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0002345C File Offset: 0x0002285C
		[SecurityCritical]
		private static void OnPreviewDragLeaveThunk(object sender, DragEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewDragLeave(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewDragLeave(e);
				return;
			}
			((UIElement3D)sender).OnPreviewDragLeave(e);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x000234AC File Offset: 0x000228AC
		[SecurityCritical]
		private static void OnDragLeaveThunk(object sender, DragEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnDragLeave(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnDragLeave(e);
				return;
			}
			((UIElement3D)sender).OnDragLeave(e);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x000234FC File Offset: 0x000228FC
		[SecurityCritical]
		private static void OnPreviewDropThunk(object sender, DragEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewDrop(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewDrop(e);
				return;
			}
			((UIElement3D)sender).OnPreviewDrop(e);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0002354C File Offset: 0x0002294C
		[SecurityCritical]
		private static void OnDropThunk(object sender, DragEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnDrop(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnDrop(e);
				return;
			}
			((UIElement3D)sender).OnDrop(e);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0002359C File Offset: 0x0002299C
		[SecurityCritical]
		private static void OnPreviewTouchDownThunk(object sender, TouchEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewTouchDown(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewTouchDown(e);
				return;
			}
			((UIElement3D)sender).OnPreviewTouchDown(e);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x000235EC File Offset: 0x000229EC
		[SecurityCritical]
		private static void OnTouchDownThunk(object sender, TouchEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnTouchDown(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnTouchDown(e);
				return;
			}
			((UIElement3D)sender).OnTouchDown(e);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0002363C File Offset: 0x00022A3C
		[SecurityCritical]
		private static void OnPreviewTouchMoveThunk(object sender, TouchEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewTouchMove(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewTouchMove(e);
				return;
			}
			((UIElement3D)sender).OnPreviewTouchMove(e);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0002368C File Offset: 0x00022A8C
		[SecurityCritical]
		private static void OnTouchMoveThunk(object sender, TouchEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnTouchMove(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnTouchMove(e);
				return;
			}
			((UIElement3D)sender).OnTouchMove(e);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x000236DC File Offset: 0x00022ADC
		[SecurityCritical]
		private static void OnPreviewTouchUpThunk(object sender, TouchEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnPreviewTouchUp(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnPreviewTouchUp(e);
				return;
			}
			((UIElement3D)sender).OnPreviewTouchUp(e);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0002372C File Offset: 0x00022B2C
		[SecurityCritical]
		private static void OnTouchUpThunk(object sender, TouchEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnTouchUp(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnTouchUp(e);
				return;
			}
			((UIElement3D)sender).OnTouchUp(e);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0002377C File Offset: 0x00022B7C
		[SecurityCritical]
		private static void OnGotTouchCaptureThunk(object sender, TouchEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnGotTouchCapture(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnGotTouchCapture(e);
				return;
			}
			((UIElement3D)sender).OnGotTouchCapture(e);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000237CC File Offset: 0x00022BCC
		[SecurityCritical]
		private static void OnLostTouchCaptureThunk(object sender, TouchEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnLostTouchCapture(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnLostTouchCapture(e);
				return;
			}
			((UIElement3D)sender).OnLostTouchCapture(e);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0002381C File Offset: 0x00022C1C
		[SecurityCritical]
		private static void OnTouchEnterThunk(object sender, TouchEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnTouchEnter(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnTouchEnter(e);
				return;
			}
			((UIElement3D)sender).OnTouchEnter(e);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0002386C File Offset: 0x00022C6C
		[SecurityCritical]
		private static void OnTouchLeaveThunk(object sender, TouchEventArgs e)
		{
			Invariant.Assert(!e.Handled, "Unexpected: Event has already been handled.");
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				uielement.OnTouchLeave(e);
				return;
			}
			ContentElement contentElement = sender as ContentElement;
			if (contentElement != null)
			{
				contentElement.OnTouchLeave(e);
				return;
			}
			((UIElement3D)sender).OnTouchLeave(e);
		}

		/// <summary>Ocorre quando qualquer botão do mouse é pressionado enquanto o ponteiro está sobre este elemento.</summary>
		// Token: 0x1400005C RID: 92
		// (add) Token: 0x060007CF RID: 1999 RVA: 0x000238BC File Offset: 0x00022CBC
		// (remove) Token: 0x060007D0 RID: 2000 RVA: 0x000238D8 File Offset: 0x00022CD8
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
		// Token: 0x060007D1 RID: 2001 RVA: 0x000238F4 File Offset: 0x00022CF4
		protected virtual void OnPreviewMouseDown(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando qualquer botão do mouse é pressionado enquanto o ponteiro está sobre este elemento.</summary>
		// Token: 0x1400005D RID: 93
		// (add) Token: 0x060007D2 RID: 2002 RVA: 0x00023904 File Offset: 0x00022D04
		// (remove) Token: 0x060007D3 RID: 2003 RVA: 0x00023920 File Offset: 0x00022D20
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
		// Token: 0x060007D4 RID: 2004 RVA: 0x0002393C File Offset: 0x00022D3C
		protected virtual void OnMouseDown(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando qualquer botão do mouse é liberado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x1400005E RID: 94
		// (add) Token: 0x060007D5 RID: 2005 RVA: 0x0002394C File Offset: 0x00022D4C
		// (remove) Token: 0x060007D6 RID: 2006 RVA: 0x00023968 File Offset: 0x00022D68
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
		// Token: 0x060007D7 RID: 2007 RVA: 0x00023984 File Offset: 0x00022D84
		protected virtual void OnPreviewMouseUp(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando qualquer botão do mouse é liberado sobre este elemento.</summary>
		// Token: 0x1400005F RID: 95
		// (add) Token: 0x060007D8 RID: 2008 RVA: 0x00023994 File Offset: 0x00022D94
		// (remove) Token: 0x060007D9 RID: 2009 RVA: 0x000239B0 File Offset: 0x00022DB0
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
		// Token: 0x060007DA RID: 2010 RVA: 0x000239CC File Offset: 0x00022DCC
		protected virtual void OnMouseUp(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão esquerdo do mouse é pressionado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x14000060 RID: 96
		// (add) Token: 0x060007DB RID: 2011 RVA: 0x000239DC File Offset: 0x00022DDC
		// (remove) Token: 0x060007DC RID: 2012 RVA: 0x000239F8 File Offset: 0x00022DF8
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

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.UIElement.PreviewMouseLeftButtonDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão esquerdo do mouse foi pressionado.</param>
		// Token: 0x060007DD RID: 2013 RVA: 0x00023A14 File Offset: 0x00022E14
		protected virtual void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão esquerdo do mouse é pressionado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x14000061 RID: 97
		// (add) Token: 0x060007DE RID: 2014 RVA: 0x00023A24 File Offset: 0x00022E24
		// (remove) Token: 0x060007DF RID: 2015 RVA: 0x00023A40 File Offset: 0x00022E40
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

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.UIElement.MouseLeftButtonDown" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão esquerdo do mouse foi pressionado.</param>
		// Token: 0x060007E0 RID: 2016 RVA: 0x00023A5C File Offset: 0x00022E5C
		protected virtual void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão esquerdo do mouse é liberado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x14000062 RID: 98
		// (add) Token: 0x060007E1 RID: 2017 RVA: 0x00023A6C File Offset: 0x00022E6C
		// (remove) Token: 0x060007E2 RID: 2018 RVA: 0x00023A88 File Offset: 0x00022E88
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

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.UIElement.PreviewMouseLeftButtonUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão esquerdo foi liberado.</param>
		// Token: 0x060007E3 RID: 2019 RVA: 0x00023AA4 File Offset: 0x00022EA4
		protected virtual void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão esquerdo do mouse é liberado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x14000063 RID: 99
		// (add) Token: 0x060007E4 RID: 2020 RVA: 0x00023AB4 File Offset: 0x00022EB4
		// (remove) Token: 0x060007E5 RID: 2021 RVA: 0x00023AD0 File Offset: 0x00022ED0
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

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.UIElement.MouseLeftButtonUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão esquerdo foi liberado.</param>
		// Token: 0x060007E6 RID: 2022 RVA: 0x00023AEC File Offset: 0x00022EEC
		protected virtual void OnMouseLeftButtonUp(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão direito do mouse é pressionado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x14000064 RID: 100
		// (add) Token: 0x060007E7 RID: 2023 RVA: 0x00023AFC File Offset: 0x00022EFC
		// (remove) Token: 0x060007E8 RID: 2024 RVA: 0x00023B18 File Offset: 0x00022F18
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

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.UIElement.PreviewMouseRightButtonDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão direito do mouse foi pressionado.</param>
		// Token: 0x060007E9 RID: 2025 RVA: 0x00023B34 File Offset: 0x00022F34
		protected virtual void OnPreviewMouseRightButtonDown(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão direito do mouse é pressionado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x14000065 RID: 101
		// (add) Token: 0x060007EA RID: 2026 RVA: 0x00023B44 File Offset: 0x00022F44
		// (remove) Token: 0x060007EB RID: 2027 RVA: 0x00023B60 File Offset: 0x00022F60
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

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.UIElement.MouseRightButtonDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão direito do mouse foi pressionado.</param>
		// Token: 0x060007EC RID: 2028 RVA: 0x00023B7C File Offset: 0x00022F7C
		protected virtual void OnMouseRightButtonDown(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão direito do mouse é liberado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x14000066 RID: 102
		// (add) Token: 0x060007ED RID: 2029 RVA: 0x00023B8C File Offset: 0x00022F8C
		// (remove) Token: 0x060007EE RID: 2030 RVA: 0x00023BA8 File Offset: 0x00022FA8
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

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.UIElement.PreviewMouseRightButtonUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão direito do mouse foi liberado.</param>
		// Token: 0x060007EF RID: 2031 RVA: 0x00023BC4 File Offset: 0x00022FC4
		protected virtual void OnPreviewMouseRightButtonUp(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão direito do mouse é liberado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x14000067 RID: 103
		// (add) Token: 0x060007F0 RID: 2032 RVA: 0x00023BD4 File Offset: 0x00022FD4
		// (remove) Token: 0x060007F1 RID: 2033 RVA: 0x00023BF0 File Offset: 0x00022FF0
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

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.UIElement.MouseRightButtonUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão direito do mouse foi liberado.</param>
		// Token: 0x060007F2 RID: 2034 RVA: 0x00023C0C File Offset: 0x0002300C
		protected virtual void OnMouseRightButtonUp(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o ponteiro do mouse se move enquanto está sobre este elemento.</summary>
		// Token: 0x14000068 RID: 104
		// (add) Token: 0x060007F3 RID: 2035 RVA: 0x00023C1C File Offset: 0x0002301C
		// (remove) Token: 0x060007F4 RID: 2036 RVA: 0x00023C38 File Offset: 0x00023038
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
		// Token: 0x060007F5 RID: 2037 RVA: 0x00023C54 File Offset: 0x00023054
		protected virtual void OnPreviewMouseMove(MouseEventArgs e)
		{
		}

		/// <summary>Ocorre quando o ponteiro do mouse se move enquanto está sobre este elemento.</summary>
		// Token: 0x14000069 RID: 105
		// (add) Token: 0x060007F6 RID: 2038 RVA: 0x00023C64 File Offset: 0x00023064
		// (remove) Token: 0x060007F7 RID: 2039 RVA: 0x00023C80 File Offset: 0x00023080
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
		// Token: 0x060007F8 RID: 2040 RVA: 0x00023C9C File Offset: 0x0002309C
		protected virtual void OnMouseMove(MouseEventArgs e)
		{
		}

		/// <summary>Ocorre quando o usuário gira a roda do mouse enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x1400006A RID: 106
		// (add) Token: 0x060007F9 RID: 2041 RVA: 0x00023CAC File Offset: 0x000230AC
		// (remove) Token: 0x060007FA RID: 2042 RVA: 0x00023CC8 File Offset: 0x000230C8
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
		// Token: 0x060007FB RID: 2043 RVA: 0x00023CE4 File Offset: 0x000230E4
		protected virtual void OnPreviewMouseWheel(MouseWheelEventArgs e)
		{
		}

		/// <summary>Ocorre quando o usuário gira a roda do mouse enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x1400006B RID: 107
		// (add) Token: 0x060007FC RID: 2044 RVA: 0x00023CF4 File Offset: 0x000230F4
		// (remove) Token: 0x060007FD RID: 2045 RVA: 0x00023D10 File Offset: 0x00023110
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
		// Token: 0x060007FE RID: 2046 RVA: 0x00023D2C File Offset: 0x0002312C
		protected virtual void OnMouseWheel(MouseWheelEventArgs e)
		{
		}

		/// <summary>Ocorre quando o ponteiro do mouse entra nos limites deste elemento.</summary>
		// Token: 0x1400006C RID: 108
		// (add) Token: 0x060007FF RID: 2047 RVA: 0x00023D3C File Offset: 0x0002313C
		// (remove) Token: 0x06000800 RID: 2048 RVA: 0x00023D58 File Offset: 0x00023158
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
		// Token: 0x06000801 RID: 2049 RVA: 0x00023D74 File Offset: 0x00023174
		protected virtual void OnMouseEnter(MouseEventArgs e)
		{
		}

		/// <summary>Ocorre quando o ponteiro do mouse sai dos limites deste elemento.</summary>
		// Token: 0x1400006D RID: 109
		// (add) Token: 0x06000802 RID: 2050 RVA: 0x00023D84 File Offset: 0x00023184
		// (remove) Token: 0x06000803 RID: 2051 RVA: 0x00023DA0 File Offset: 0x000231A0
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
		// Token: 0x06000804 RID: 2052 RVA: 0x00023DBC File Offset: 0x000231BC
		protected virtual void OnMouseLeave(MouseEventArgs e)
		{
		}

		/// <summary>Ocorre quando este elemento captura o mouse.</summary>
		// Token: 0x1400006E RID: 110
		// (add) Token: 0x06000805 RID: 2053 RVA: 0x00023DCC File Offset: 0x000231CC
		// (remove) Token: 0x06000806 RID: 2054 RVA: 0x00023DE8 File Offset: 0x000231E8
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
		// Token: 0x06000807 RID: 2055 RVA: 0x00023E04 File Offset: 0x00023204
		protected virtual void OnGotMouseCapture(MouseEventArgs e)
		{
		}

		/// <summary>Ocorre quando este elemento perde a captura do mouse.</summary>
		// Token: 0x1400006F RID: 111
		// (add) Token: 0x06000808 RID: 2056 RVA: 0x00023E14 File Offset: 0x00023214
		// (remove) Token: 0x06000809 RID: 2057 RVA: 0x00023E30 File Offset: 0x00023230
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
		// Token: 0x0600080A RID: 2058 RVA: 0x00023E4C File Offset: 0x0002324C
		protected virtual void OnLostMouseCapture(MouseEventArgs e)
		{
		}

		/// <summary>Ocorre quando a exibição do cursor é solicitada. Este evento é gerado em um elemento toda vez que o ponteiro do mouse se move para uma nova localização, o que significa que o objeto de cursor talvez precise ser alterado de acordo com sua nova posição.</summary>
		// Token: 0x14000070 RID: 112
		// (add) Token: 0x0600080B RID: 2059 RVA: 0x00023E5C File Offset: 0x0002325C
		// (remove) Token: 0x0600080C RID: 2060 RVA: 0x00023E78 File Offset: 0x00023278
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
		// Token: 0x0600080D RID: 2061 RVA: 0x00023E94 File Offset: 0x00023294
		protected virtual void OnQueryCursor(QueryCursorEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta toca o digitalizador enquanto está sobre este elemento.</summary>
		// Token: 0x14000071 RID: 113
		// (add) Token: 0x0600080E RID: 2062 RVA: 0x00023EA4 File Offset: 0x000232A4
		// (remove) Token: 0x0600080F RID: 2063 RVA: 0x00023EC0 File Offset: 0x000232C0
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
		// Token: 0x06000810 RID: 2064 RVA: 0x00023EDC File Offset: 0x000232DC
		protected virtual void OnPreviewStylusDown(StylusDownEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta toca o digitalizador enquanto está sobre este elemento.</summary>
		// Token: 0x14000072 RID: 114
		// (add) Token: 0x06000811 RID: 2065 RVA: 0x00023EEC File Offset: 0x000232EC
		// (remove) Token: 0x06000812 RID: 2066 RVA: 0x00023F08 File Offset: 0x00023308
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
		// Token: 0x06000813 RID: 2067 RVA: 0x00023F24 File Offset: 0x00023324
		protected virtual void OnStylusDown(StylusDownEventArgs e)
		{
		}

		/// <summary>Ocorre quando o usuário retira a caneta do digitalizador enquanto ela está sobre esse elemento.</summary>
		// Token: 0x14000073 RID: 115
		// (add) Token: 0x06000814 RID: 2068 RVA: 0x00023F34 File Offset: 0x00023334
		// (remove) Token: 0x06000815 RID: 2069 RVA: 0x00023F50 File Offset: 0x00023350
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
		// Token: 0x06000816 RID: 2070 RVA: 0x00023F6C File Offset: 0x0002336C
		protected virtual void OnPreviewStylusUp(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando o usuário retira a caneta do digitalizador enquanto ela está sobre este elemento.</summary>
		// Token: 0x14000074 RID: 116
		// (add) Token: 0x06000817 RID: 2071 RVA: 0x00023F7C File Offset: 0x0002337C
		// (remove) Token: 0x06000818 RID: 2072 RVA: 0x00023F98 File Offset: 0x00023398
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
		// Token: 0x06000819 RID: 2073 RVA: 0x00023FB4 File Offset: 0x000233B4
		protected virtual void OnStylusUp(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta se move enquanto está sobre o elemento. A caneta deverá se mover enquanto estiver sendo detectada pelo digitalizador para gerar este evento, caso contrário, <see cref="E:System.Windows.UIElement.PreviewStylusInAirMove" /> será gerado.</summary>
		// Token: 0x14000075 RID: 117
		// (add) Token: 0x0600081A RID: 2074 RVA: 0x00023FC4 File Offset: 0x000233C4
		// (remove) Token: 0x0600081B RID: 2075 RVA: 0x00023FE0 File Offset: 0x000233E0
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
		// Token: 0x0600081C RID: 2076 RVA: 0x00023FFC File Offset: 0x000233FC
		protected virtual void OnPreviewStylusMove(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta se move sobre este elemento. A caneta deve mover-se enquanto está no digitalizador para gerar este evento. Caso contrário, <see cref="E:System.Windows.UIElement.StylusInAirMove" /> será gerado.</summary>
		// Token: 0x14000076 RID: 118
		// (add) Token: 0x0600081D RID: 2077 RVA: 0x0002400C File Offset: 0x0002340C
		// (remove) Token: 0x0600081E RID: 2078 RVA: 0x00024028 File Offset: 0x00023428
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
		// Token: 0x0600081F RID: 2079 RVA: 0x00024044 File Offset: 0x00023444
		protected virtual void OnStylusMove(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta se move sobre um elemento sem tocar de fato o digitalizador.</summary>
		// Token: 0x14000077 RID: 119
		// (add) Token: 0x06000820 RID: 2080 RVA: 0x00024054 File Offset: 0x00023454
		// (remove) Token: 0x06000821 RID: 2081 RVA: 0x00024070 File Offset: 0x00023470
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
		// Token: 0x06000822 RID: 2082 RVA: 0x0002408C File Offset: 0x0002348C
		protected virtual void OnPreviewStylusInAirMove(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta se move sobre um elemento sem tocar de fato o digitalizador.</summary>
		// Token: 0x14000078 RID: 120
		// (add) Token: 0x06000823 RID: 2083 RVA: 0x0002409C File Offset: 0x0002349C
		// (remove) Token: 0x06000824 RID: 2084 RVA: 0x000240B8 File Offset: 0x000234B8
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
		// Token: 0x06000825 RID: 2085 RVA: 0x000240D4 File Offset: 0x000234D4
		protected virtual void OnStylusInAirMove(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta entra nos limites deste elemento.</summary>
		// Token: 0x14000079 RID: 121
		// (add) Token: 0x06000826 RID: 2086 RVA: 0x000240E4 File Offset: 0x000234E4
		// (remove) Token: 0x06000827 RID: 2087 RVA: 0x00024100 File Offset: 0x00023500
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
		// Token: 0x06000828 RID: 2088 RVA: 0x0002411C File Offset: 0x0002351C
		protected virtual void OnStylusEnter(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta sai dos limites do elemento.</summary>
		// Token: 0x1400007A RID: 122
		// (add) Token: 0x06000829 RID: 2089 RVA: 0x0002412C File Offset: 0x0002352C
		// (remove) Token: 0x0600082A RID: 2090 RVA: 0x00024148 File Offset: 0x00023548
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
		// Token: 0x0600082B RID: 2091 RVA: 0x00024164 File Offset: 0x00023564
		protected virtual void OnStylusLeave(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta está sobre este elemento e perto o suficiente do digitalizador para ser detectada.</summary>
		// Token: 0x1400007B RID: 123
		// (add) Token: 0x0600082C RID: 2092 RVA: 0x00024174 File Offset: 0x00023574
		// (remove) Token: 0x0600082D RID: 2093 RVA: 0x00024190 File Offset: 0x00023590
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
		// Token: 0x0600082E RID: 2094 RVA: 0x000241AC File Offset: 0x000235AC
		protected virtual void OnPreviewStylusInRange(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta está sobre este elemento e perto o suficiente do digitalizador para ser detectada.</summary>
		// Token: 0x1400007C RID: 124
		// (add) Token: 0x0600082F RID: 2095 RVA: 0x000241BC File Offset: 0x000235BC
		// (remove) Token: 0x06000830 RID: 2096 RVA: 0x000241D8 File Offset: 0x000235D8
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
		// Token: 0x06000831 RID: 2097 RVA: 0x000241F4 File Offset: 0x000235F4
		protected virtual void OnStylusInRange(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta está longe demais do digitalizador para ser detectada.</summary>
		// Token: 0x1400007D RID: 125
		// (add) Token: 0x06000832 RID: 2098 RVA: 0x00024204 File Offset: 0x00023604
		// (remove) Token: 0x06000833 RID: 2099 RVA: 0x00024220 File Offset: 0x00023620
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
		// Token: 0x06000834 RID: 2100 RVA: 0x0002423C File Offset: 0x0002363C
		protected virtual void OnPreviewStylusOutOfRange(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta está sobre o elemento e longe demais do digitalizador para ser detectada.</summary>
		// Token: 0x1400007E RID: 126
		// (add) Token: 0x06000835 RID: 2101 RVA: 0x0002424C File Offset: 0x0002364C
		// (remove) Token: 0x06000836 RID: 2102 RVA: 0x00024268 File Offset: 0x00023668
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
		// Token: 0x06000837 RID: 2103 RVA: 0x00024284 File Offset: 0x00023684
		protected virtual void OnStylusOutOfRange(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando o usuário executa um dos diversos gestos da caneta.</summary>
		// Token: 0x1400007F RID: 127
		// (add) Token: 0x06000838 RID: 2104 RVA: 0x00024294 File Offset: 0x00023694
		// (remove) Token: 0x06000839 RID: 2105 RVA: 0x000242B0 File Offset: 0x000236B0
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
		// Token: 0x0600083A RID: 2106 RVA: 0x000242CC File Offset: 0x000236CC
		protected virtual void OnPreviewStylusSystemGesture(StylusSystemGestureEventArgs e)
		{
		}

		/// <summary>Ocorre quando o usuário executa um dos diversos gestos da caneta.</summary>
		// Token: 0x14000080 RID: 128
		// (add) Token: 0x0600083B RID: 2107 RVA: 0x000242DC File Offset: 0x000236DC
		// (remove) Token: 0x0600083C RID: 2108 RVA: 0x000242F8 File Offset: 0x000236F8
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
		// Token: 0x0600083D RID: 2109 RVA: 0x00024314 File Offset: 0x00023714
		protected virtual void OnStylusSystemGesture(StylusSystemGestureEventArgs e)
		{
		}

		/// <summary>Ocorre quando este elemento captura a caneta.</summary>
		// Token: 0x14000081 RID: 129
		// (add) Token: 0x0600083E RID: 2110 RVA: 0x00024324 File Offset: 0x00023724
		// (remove) Token: 0x0600083F RID: 2111 RVA: 0x00024340 File Offset: 0x00023740
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
		// Token: 0x06000840 RID: 2112 RVA: 0x0002435C File Offset: 0x0002375C
		protected virtual void OnGotStylusCapture(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando este elemento perde a captura da caneta.</summary>
		// Token: 0x14000082 RID: 130
		// (add) Token: 0x06000841 RID: 2113 RVA: 0x0002436C File Offset: 0x0002376C
		// (remove) Token: 0x06000842 RID: 2114 RVA: 0x00024388 File Offset: 0x00023788
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
		// Token: 0x06000843 RID: 2115 RVA: 0x000243A4 File Offset: 0x000237A4
		protected virtual void OnLostStylusCapture(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão da caneta é pressionado enquanto o ponteiro está sobre este elemento.</summary>
		// Token: 0x14000083 RID: 131
		// (add) Token: 0x06000844 RID: 2116 RVA: 0x000243B4 File Offset: 0x000237B4
		// (remove) Token: 0x06000845 RID: 2117 RVA: 0x000243D0 File Offset: 0x000237D0
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
		// Token: 0x06000846 RID: 2118 RVA: 0x000243EC File Offset: 0x000237EC
		protected virtual void OnStylusButtonDown(StylusButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão da caneta é liberado enquanto o ponteiro está sobre este elemento.</summary>
		// Token: 0x14000084 RID: 132
		// (add) Token: 0x06000847 RID: 2119 RVA: 0x000243FC File Offset: 0x000237FC
		// (remove) Token: 0x06000848 RID: 2120 RVA: 0x00024418 File Offset: 0x00023818
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
		// Token: 0x06000849 RID: 2121 RVA: 0x00024434 File Offset: 0x00023834
		protected virtual void OnStylusButtonUp(StylusButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão da caneta é pressionado enquanto o ponteiro está sobre este elemento.</summary>
		// Token: 0x14000085 RID: 133
		// (add) Token: 0x0600084A RID: 2122 RVA: 0x00024444 File Offset: 0x00023844
		// (remove) Token: 0x0600084B RID: 2123 RVA: 0x00024460 File Offset: 0x00023860
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
		// Token: 0x0600084C RID: 2124 RVA: 0x0002447C File Offset: 0x0002387C
		protected virtual void OnPreviewStylusButtonDown(StylusButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão da caneta é liberado enquanto o ponteiro está sobre este elemento.</summary>
		// Token: 0x14000086 RID: 134
		// (add) Token: 0x0600084D RID: 2125 RVA: 0x0002448C File Offset: 0x0002388C
		// (remove) Token: 0x0600084E RID: 2126 RVA: 0x000244A8 File Offset: 0x000238A8
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
		// Token: 0x0600084F RID: 2127 RVA: 0x000244C4 File Offset: 0x000238C4
		protected virtual void OnPreviewStylusButtonUp(StylusButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando uma tecla é pressionada enquanto o foco está neste elemento.</summary>
		// Token: 0x14000087 RID: 135
		// (add) Token: 0x06000850 RID: 2128 RVA: 0x000244D4 File Offset: 0x000238D4
		// (remove) Token: 0x06000851 RID: 2129 RVA: 0x000244F0 File Offset: 0x000238F0
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
		// Token: 0x06000852 RID: 2130 RVA: 0x0002450C File Offset: 0x0002390C
		protected virtual void OnPreviewKeyDown(KeyEventArgs e)
		{
		}

		/// <summary>Ocorre quando uma tecla é pressionada enquanto o foco está neste elemento.</summary>
		// Token: 0x14000088 RID: 136
		// (add) Token: 0x06000853 RID: 2131 RVA: 0x0002451C File Offset: 0x0002391C
		// (remove) Token: 0x06000854 RID: 2132 RVA: 0x00024538 File Offset: 0x00023938
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
		// Token: 0x06000855 RID: 2133 RVA: 0x00024554 File Offset: 0x00023954
		protected virtual void OnKeyDown(KeyEventArgs e)
		{
		}

		/// <summary>Ocorre quando uma chave é liberada enquanto o foco está neste elemento.</summary>
		// Token: 0x14000089 RID: 137
		// (add) Token: 0x06000856 RID: 2134 RVA: 0x00024564 File Offset: 0x00023964
		// (remove) Token: 0x06000857 RID: 2135 RVA: 0x00024580 File Offset: 0x00023980
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
		// Token: 0x06000858 RID: 2136 RVA: 0x0002459C File Offset: 0x0002399C
		protected virtual void OnPreviewKeyUp(KeyEventArgs e)
		{
		}

		/// <summary>Ocorre quando uma chave é liberada enquanto o foco está neste elemento.</summary>
		// Token: 0x1400008A RID: 138
		// (add) Token: 0x06000859 RID: 2137 RVA: 0x000245AC File Offset: 0x000239AC
		// (remove) Token: 0x0600085A RID: 2138 RVA: 0x000245C8 File Offset: 0x000239C8
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
		// Token: 0x0600085B RID: 2139 RVA: 0x000245E4 File Offset: 0x000239E4
		protected virtual void OnKeyUp(KeyEventArgs e)
		{
		}

		/// <summary>Ocorre quando o teclado está focalizado neste elemento.</summary>
		// Token: 0x1400008B RID: 139
		// (add) Token: 0x0600085C RID: 2140 RVA: 0x000245F4 File Offset: 0x000239F4
		// (remove) Token: 0x0600085D RID: 2141 RVA: 0x00024610 File Offset: 0x00023A10
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
		// Token: 0x0600085E RID: 2142 RVA: 0x0002462C File Offset: 0x00023A2C
		protected virtual void OnPreviewGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
		{
		}

		/// <summary>Ocorre quando o teclado está focalizado neste elemento.</summary>
		// Token: 0x1400008C RID: 140
		// (add) Token: 0x0600085F RID: 2143 RVA: 0x0002463C File Offset: 0x00023A3C
		// (remove) Token: 0x06000860 RID: 2144 RVA: 0x00024658 File Offset: 0x00023A58
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
		// Token: 0x06000861 RID: 2145 RVA: 0x00024674 File Offset: 0x00023A74
		protected virtual void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
		{
		}

		/// <summary>Ocorre quando o teclado não está mais focalizado neste elemento.</summary>
		// Token: 0x1400008D RID: 141
		// (add) Token: 0x06000862 RID: 2146 RVA: 0x00024684 File Offset: 0x00023A84
		// (remove) Token: 0x06000863 RID: 2147 RVA: 0x000246A0 File Offset: 0x00023AA0
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

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Keyboard.PreviewKeyDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.KeyboardFocusChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000864 RID: 2148 RVA: 0x000246BC File Offset: 0x00023ABC
		protected virtual void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
		{
		}

		/// <summary>Ocorre quando o teclado não está mais focalizado no elemento.</summary>
		// Token: 0x1400008E RID: 142
		// (add) Token: 0x06000865 RID: 2149 RVA: 0x000246CC File Offset: 0x00023ACC
		// (remove) Token: 0x06000866 RID: 2150 RVA: 0x000246E8 File Offset: 0x00023AE8
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
		// Token: 0x06000867 RID: 2151 RVA: 0x00024704 File Offset: 0x00023B04
		protected virtual void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
		{
		}

		/// <summary>Ocorre quando este elemento obtém texto de forma independente de dispositivo.</summary>
		// Token: 0x1400008F RID: 143
		// (add) Token: 0x06000868 RID: 2152 RVA: 0x00024714 File Offset: 0x00023B14
		// (remove) Token: 0x06000869 RID: 2153 RVA: 0x00024730 File Offset: 0x00023B30
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
		// Token: 0x0600086A RID: 2154 RVA: 0x0002474C File Offset: 0x00023B4C
		protected virtual void OnPreviewTextInput(TextCompositionEventArgs e)
		{
		}

		/// <summary>Ocorre quando este elemento obtém texto de forma independente de dispositivo.</summary>
		// Token: 0x14000090 RID: 144
		// (add) Token: 0x0600086B RID: 2155 RVA: 0x0002475C File Offset: 0x00023B5C
		// (remove) Token: 0x0600086C RID: 2156 RVA: 0x00024778 File Offset: 0x00023B78
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
		// Token: 0x0600086D RID: 2157 RVA: 0x00024794 File Offset: 0x00023B94
		protected virtual void OnTextInput(TextCompositionEventArgs e)
		{
		}

		/// <summary>Ocorre quando há uma alteração no estado do botão do teclado ou do mouse durante uma operação de arrastar e soltar.</summary>
		// Token: 0x14000091 RID: 145
		// (add) Token: 0x0600086E RID: 2158 RVA: 0x000247A4 File Offset: 0x00023BA4
		// (remove) Token: 0x0600086F RID: 2159 RVA: 0x000247C0 File Offset: 0x00023BC0
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
		// Token: 0x06000870 RID: 2160 RVA: 0x000247DC File Offset: 0x00023BDC
		protected virtual void OnPreviewQueryContinueDrag(QueryContinueDragEventArgs e)
		{
		}

		/// <summary>Ocorre quando há uma alteração no estado do botão do teclado ou do mouse durante uma operação de arrastar e soltar.</summary>
		// Token: 0x14000092 RID: 146
		// (add) Token: 0x06000871 RID: 2161 RVA: 0x000247EC File Offset: 0x00023BEC
		// (remove) Token: 0x06000872 RID: 2162 RVA: 0x00024808 File Offset: 0x00023C08
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
		// Token: 0x06000873 RID: 2163 RVA: 0x00024824 File Offset: 0x00023C24
		protected virtual void OnQueryContinueDrag(QueryContinueDragEventArgs e)
		{
		}

		/// <summary>Ocorre quando uma operação de arrastar e soltar se inicia.</summary>
		// Token: 0x14000093 RID: 147
		// (add) Token: 0x06000874 RID: 2164 RVA: 0x00024834 File Offset: 0x00023C34
		// (remove) Token: 0x06000875 RID: 2165 RVA: 0x00024850 File Offset: 0x00023C50
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
		// Token: 0x06000876 RID: 2166 RVA: 0x0002486C File Offset: 0x00023C6C
		protected virtual void OnPreviewGiveFeedback(GiveFeedbackEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento de arrastar e soltar subjacente que envolve este elemento.</summary>
		// Token: 0x14000094 RID: 148
		// (add) Token: 0x06000877 RID: 2167 RVA: 0x0002487C File Offset: 0x00023C7C
		// (remove) Token: 0x06000878 RID: 2168 RVA: 0x00024898 File Offset: 0x00023C98
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
		// Token: 0x06000879 RID: 2169 RVA: 0x000248B4 File Offset: 0x00023CB4
		protected virtual void OnGiveFeedback(GiveFeedbackEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento arrastar subjacente com este elemento como o destino de arrastar.</summary>
		// Token: 0x14000095 RID: 149
		// (add) Token: 0x0600087A RID: 2170 RVA: 0x000248C4 File Offset: 0x00023CC4
		// (remove) Token: 0x0600087B RID: 2171 RVA: 0x000248E0 File Offset: 0x00023CE0
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
		// Token: 0x0600087C RID: 2172 RVA: 0x000248FC File Offset: 0x00023CFC
		protected virtual void OnPreviewDragEnter(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento arrastar subjacente com este elemento como o destino de arrastar.</summary>
		// Token: 0x14000096 RID: 150
		// (add) Token: 0x0600087D RID: 2173 RVA: 0x0002490C File Offset: 0x00023D0C
		// (remove) Token: 0x0600087E RID: 2174 RVA: 0x00024928 File Offset: 0x00023D28
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
		// Token: 0x0600087F RID: 2175 RVA: 0x00024944 File Offset: 0x00023D44
		protected virtual void OnDragEnter(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento do tipo "arrastar" subjacente com esse elemento como a reprodução automática potencial.</summary>
		// Token: 0x14000097 RID: 151
		// (add) Token: 0x06000880 RID: 2176 RVA: 0x00024954 File Offset: 0x00023D54
		// (remove) Token: 0x06000881 RID: 2177 RVA: 0x00024970 File Offset: 0x00023D70
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
		// Token: 0x06000882 RID: 2178 RVA: 0x0002498C File Offset: 0x00023D8C
		protected virtual void OnPreviewDragOver(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento do tipo "arrastar" subjacente com esse elemento como a reprodução automática potencial.</summary>
		// Token: 0x14000098 RID: 152
		// (add) Token: 0x06000883 RID: 2179 RVA: 0x0002499C File Offset: 0x00023D9C
		// (remove) Token: 0x06000884 RID: 2180 RVA: 0x000249B8 File Offset: 0x00023DB8
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
		// Token: 0x06000885 RID: 2181 RVA: 0x000249D4 File Offset: 0x00023DD4
		protected virtual void OnDragOver(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento arrastar subjacente com este elemento como a origem de arrastar.</summary>
		// Token: 0x14000099 RID: 153
		// (add) Token: 0x06000886 RID: 2182 RVA: 0x000249E4 File Offset: 0x00023DE4
		// (remove) Token: 0x06000887 RID: 2183 RVA: 0x00024A00 File Offset: 0x00023E00
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
		// Token: 0x06000888 RID: 2184 RVA: 0x00024A1C File Offset: 0x00023E1C
		protected virtual void OnPreviewDragLeave(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento arrastar subjacente com este elemento como a origem de arrastar.</summary>
		// Token: 0x1400009A RID: 154
		// (add) Token: 0x06000889 RID: 2185 RVA: 0x00024A2C File Offset: 0x00023E2C
		// (remove) Token: 0x0600088A RID: 2186 RVA: 0x00024A48 File Offset: 0x00023E48
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
		// Token: 0x0600088B RID: 2187 RVA: 0x00024A64 File Offset: 0x00023E64
		protected virtual void OnDragLeave(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento soltar subjacente com esse elemento sendo uma reprodução automática.</summary>
		// Token: 0x1400009B RID: 155
		// (add) Token: 0x0600088C RID: 2188 RVA: 0x00024A74 File Offset: 0x00023E74
		// (remove) Token: 0x0600088D RID: 2189 RVA: 0x00024A90 File Offset: 0x00023E90
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
		// Token: 0x0600088E RID: 2190 RVA: 0x00024AAC File Offset: 0x00023EAC
		protected virtual void OnPreviewDrop(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento soltar subjacente com esse elemento sendo uma reprodução automática.</summary>
		// Token: 0x1400009C RID: 156
		// (add) Token: 0x0600088F RID: 2191 RVA: 0x00024ABC File Offset: 0x00023EBC
		// (remove) Token: 0x06000890 RID: 2192 RVA: 0x00024AD8 File Offset: 0x00023ED8
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

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.DragEnter" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DragEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000891 RID: 2193 RVA: 0x00024AF4 File Offset: 0x00023EF4
		protected virtual void OnDrop(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando um dedo toca a tela enquanto está sobre esse elemento.</summary>
		// Token: 0x1400009D RID: 157
		// (add) Token: 0x06000892 RID: 2194 RVA: 0x00024B04 File Offset: 0x00023F04
		// (remove) Token: 0x06000893 RID: 2195 RVA: 0x00024B20 File Offset: 0x00023F20
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

		/// <summary>Fornece tratamento de classes para os eventos roteados <see cref="E:System.Windows.UIElement.PreviewTouchDown" /> que ocorrem quando um toque pressiona esse elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000894 RID: 2196 RVA: 0x00024B3C File Offset: 0x00023F3C
		protected virtual void OnPreviewTouchDown(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um dedo toca a tela enquanto está sobre esse elemento.</summary>
		// Token: 0x1400009E RID: 158
		// (add) Token: 0x06000895 RID: 2197 RVA: 0x00024B4C File Offset: 0x00023F4C
		// (remove) Token: 0x06000896 RID: 2198 RVA: 0x00024B68 File Offset: 0x00023F68
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

		/// <summary>Fornece tratamento de classes para os eventos roteados <see cref="E:System.Windows.UIElement.TouchDown" /> que ocorrem quando há um toque nesse elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000897 RID: 2199 RVA: 0x00024B84 File Offset: 0x00023F84
		protected virtual void OnTouchDown(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um dedo se move na tela enquanto está sobre esse elemento.</summary>
		// Token: 0x1400009F RID: 159
		// (add) Token: 0x06000898 RID: 2200 RVA: 0x00024B94 File Offset: 0x00023F94
		// (remove) Token: 0x06000899 RID: 2201 RVA: 0x00024BB0 File Offset: 0x00023FB0
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

		/// <summary>Fornece manipulação de classes para o evento roteado <see cref="E:System.Windows.UIElement.PreviewTouchMove" /> que ocorre quando há uma movimentação de toque nesse elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x0600089A RID: 2202 RVA: 0x00024BCC File Offset: 0x00023FCC
		protected virtual void OnPreviewTouchMove(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um dedo se move na tela enquanto está sobre esse elemento.</summary>
		// Token: 0x140000A0 RID: 160
		// (add) Token: 0x0600089B RID: 2203 RVA: 0x00024BDC File Offset: 0x00023FDC
		// (remove) Token: 0x0600089C RID: 2204 RVA: 0x00024BF8 File Offset: 0x00023FF8
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

		/// <summary>Fornece manipulação de classes para o evento roteado <see cref="E:System.Windows.UIElement.TouchMove" /> que ocorre quando há uma movimentação de toque nesse elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x0600089D RID: 2205 RVA: 0x00024C14 File Offset: 0x00024014
		protected virtual void OnTouchMove(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um dedo é gerado fora da tela enquanto o dedo está sobre este elemento.</summary>
		// Token: 0x140000A1 RID: 161
		// (add) Token: 0x0600089E RID: 2206 RVA: 0x00024C24 File Offset: 0x00024024
		// (remove) Token: 0x0600089F RID: 2207 RVA: 0x00024C40 File Offset: 0x00024040
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

		/// <summary>Fornece tratamento de classe para os eventos roteados <see cref="E:System.Windows.UIElement.PreviewTouchUp" /> que ocorrem quando um toque é liberado dentro desse elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060008A0 RID: 2208 RVA: 0x00024C5C File Offset: 0x0002405C
		protected virtual void OnPreviewTouchUp(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um dedo é gerado fora da tela enquanto o dedo está sobre este elemento.</summary>
		// Token: 0x140000A2 RID: 162
		// (add) Token: 0x060008A1 RID: 2209 RVA: 0x00024C6C File Offset: 0x0002406C
		// (remove) Token: 0x060008A2 RID: 2210 RVA: 0x00024C88 File Offset: 0x00024088
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

		/// <summary>Fornece tratamento de classe para os eventos roteados <see cref="E:System.Windows.UIElement.TouchUp" /> que ocorrem quando um toque é liberado dentro desse elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060008A3 RID: 2211 RVA: 0x00024CA4 File Offset: 0x000240A4
		protected virtual void OnTouchUp(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um toque é capturado para esse elemento.</summary>
		// Token: 0x140000A3 RID: 163
		// (add) Token: 0x060008A4 RID: 2212 RVA: 0x00024CB4 File Offset: 0x000240B4
		// (remove) Token: 0x060008A5 RID: 2213 RVA: 0x00024CD0 File Offset: 0x000240D0
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

		/// <summary>Fornece tratamento de classes para os eventos roteados <see cref="E:System.Windows.UIElement.GotTouchCapture" /> que ocorrem quando um toque é capturado para esse elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060008A6 RID: 2214 RVA: 0x00024CEC File Offset: 0x000240EC
		protected virtual void OnGotTouchCapture(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando este elemento perde a captura do toque.</summary>
		// Token: 0x140000A4 RID: 164
		// (add) Token: 0x060008A7 RID: 2215 RVA: 0x00024CFC File Offset: 0x000240FC
		// (remove) Token: 0x060008A8 RID: 2216 RVA: 0x00024D18 File Offset: 0x00024118
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

		/// <summary>Fornece tratamento de classes para o evento roteado <see cref="E:System.Windows.UIElement.LostTouchCapture" /> que ocorre quando este elemento perde a captura de toque.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060008A9 RID: 2217 RVA: 0x00024D34 File Offset: 0x00024134
		protected virtual void OnLostTouchCapture(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um toque é movido de fora para dentro dos limites deste elemento.</summary>
		// Token: 0x140000A5 RID: 165
		// (add) Token: 0x060008AA RID: 2218 RVA: 0x00024D44 File Offset: 0x00024144
		// (remove) Token: 0x060008AB RID: 2219 RVA: 0x00024D60 File Offset: 0x00024160
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

		/// <summary>Fornece tratamento de classes para os eventos roteados de <see cref="E:System.Windows.UIElement.TouchEnter" /> que ocorre quando um toque é movido de fora para dentro dos limites deste elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060008AC RID: 2220 RVA: 0x00024D7C File Offset: 0x0002417C
		protected virtual void OnTouchEnter(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um toque é movido de dentro para fora dos limites deste elemento.</summary>
		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x060008AD RID: 2221 RVA: 0x00024D8C File Offset: 0x0002418C
		// (remove) Token: 0x060008AE RID: 2222 RVA: 0x00024DA8 File Offset: 0x000241A8
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

		/// <summary>Fornece tratamento de classe para os eventos roteados <see cref="E:System.Windows.UIElement.TouchLeave" /> que ocorre quando um toque é movido de dentro para fora dos limites deste <see cref="T:System.Windows.UIElement" />.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060008AF RID: 2223 RVA: 0x00024DC4 File Offset: 0x000241C4
		protected virtual void OnTouchLeave(TouchEventArgs e)
		{
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00024DD4 File Offset: 0x000241D4
		private static void IsMouseDirectlyOver_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((UIElement)d).RaiseIsMouseDirectlyOverChanged(e);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.UIElement.IsMouseDirectlyOver" /> é alterado neste elemento.</summary>
		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x060008B1 RID: 2225 RVA: 0x00024DF0 File Offset: 0x000241F0
		// (remove) Token: 0x060008B2 RID: 2226 RVA: 0x00024E0C File Offset: 0x0002420C
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

		/// <summary>Invocado quando um evento <see cref="E:System.Windows.UIElement.IsMouseDirectlyOverChanged" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060008B3 RID: 2227 RVA: 0x00024E28 File Offset: 0x00024228
		protected virtual void OnIsMouseDirectlyOverChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00024E38 File Offset: 0x00024238
		private void RaiseIsMouseDirectlyOverChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsMouseDirectlyOverChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsMouseDirectlyOverChangedKey, args);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="E:System.Windows.UIElement.IsKeyboardFocusWithinChanged" /> é alterado neste elemento.</summary>
		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x060008B5 RID: 2229 RVA: 0x00024E58 File Offset: 0x00024258
		// (remove) Token: 0x060008B6 RID: 2230 RVA: 0x00024E74 File Offset: 0x00024274
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

		/// <summary>Invocado pouco antes do evento <see cref="E:System.Windows.UIElement.IsKeyboardFocusWithinChanged" /> ser gerado por este elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060008B7 RID: 2231 RVA: 0x00024E90 File Offset: 0x00024290
		protected virtual void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00024EA0 File Offset: 0x000242A0
		internal void RaiseIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsKeyboardFocusWithinChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsKeyboardFocusWithinChangedKey, args);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00024EC0 File Offset: 0x000242C0
		private static void IsMouseCaptured_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((UIElement)d).RaiseIsMouseCapturedChanged(e);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.UIElement.IsMouseCaptured" /> é alterado neste elemento.</summary>
		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x060008BA RID: 2234 RVA: 0x00024EDC File Offset: 0x000242DC
		// (remove) Token: 0x060008BB RID: 2235 RVA: 0x00024EF8 File Offset: 0x000242F8
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

		/// <summary>Invocado quando um evento <see cref="E:System.Windows.UIElement.IsMouseCapturedChanged" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060008BC RID: 2236 RVA: 0x00024F14 File Offset: 0x00024314
		protected virtual void OnIsMouseCapturedChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00024F24 File Offset: 0x00024324
		private void RaiseIsMouseCapturedChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsMouseCapturedChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsMouseCapturedChangedKey, args);
		}

		/// <summary>Ocorre quando o valor do <see cref="F:System.Windows.UIElement.IsMouseCaptureWithinProperty" /> é alterado nesse elemento.</summary>
		// Token: 0x140000AA RID: 170
		// (add) Token: 0x060008BE RID: 2238 RVA: 0x00024F44 File Offset: 0x00024344
		// (remove) Token: 0x060008BF RID: 2239 RVA: 0x00024F60 File Offset: 0x00024360
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

		/// <summary>Invocado quando um evento <see cref="E:System.Windows.UIElement.IsMouseCaptureWithinChanged" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060008C0 RID: 2240 RVA: 0x00024F7C File Offset: 0x0002437C
		protected virtual void OnIsMouseCaptureWithinChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00024F8C File Offset: 0x0002438C
		internal void RaiseIsMouseCaptureWithinChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsMouseCaptureWithinChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsMouseCaptureWithinChangedKey, args);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00024FAC File Offset: 0x000243AC
		private static void IsStylusDirectlyOver_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((UIElement)d).RaiseIsStylusDirectlyOverChanged(e);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.UIElement.IsStylusDirectlyOver" /> é alterado neste elemento.</summary>
		// Token: 0x140000AB RID: 171
		// (add) Token: 0x060008C3 RID: 2243 RVA: 0x00024FC8 File Offset: 0x000243C8
		// (remove) Token: 0x060008C4 RID: 2244 RVA: 0x00024FE4 File Offset: 0x000243E4
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

		/// <summary>Invocado quando um evento <see cref="E:System.Windows.UIElement.IsStylusDirectlyOverChanged" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060008C5 RID: 2245 RVA: 0x00025000 File Offset: 0x00024400
		protected virtual void OnIsStylusDirectlyOverChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00025010 File Offset: 0x00024410
		private void RaiseIsStylusDirectlyOverChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsStylusDirectlyOverChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsStylusDirectlyOverChangedKey, args);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00025030 File Offset: 0x00024430
		private static void IsStylusCaptured_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((UIElement)d).RaiseIsStylusCapturedChanged(e);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.UIElement.IsStylusCaptured" /> é alterado neste elemento.</summary>
		// Token: 0x140000AC RID: 172
		// (add) Token: 0x060008C8 RID: 2248 RVA: 0x0002504C File Offset: 0x0002444C
		// (remove) Token: 0x060008C9 RID: 2249 RVA: 0x00025068 File Offset: 0x00024468
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

		/// <summary>Invocado quando um evento <see cref="E:System.Windows.UIElement.IsStylusCapturedChanged" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060008CA RID: 2250 RVA: 0x00025084 File Offset: 0x00024484
		protected virtual void OnIsStylusCapturedChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00025094 File Offset: 0x00024494
		private void RaiseIsStylusCapturedChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsStylusCapturedChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsStylusCapturedChangedKey, args);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.UIElement.IsStylusCaptureWithin" /> é alterado neste elemento.</summary>
		// Token: 0x140000AD RID: 173
		// (add) Token: 0x060008CC RID: 2252 RVA: 0x000250B4 File Offset: 0x000244B4
		// (remove) Token: 0x060008CD RID: 2253 RVA: 0x000250D0 File Offset: 0x000244D0
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

		/// <summary>Invocado quando um evento <see cref="E:System.Windows.UIElement.IsStylusCaptureWithinChanged" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060008CE RID: 2254 RVA: 0x000250EC File Offset: 0x000244EC
		protected virtual void OnIsStylusCaptureWithinChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x000250FC File Offset: 0x000244FC
		internal void RaiseIsStylusCaptureWithinChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsStylusCaptureWithinChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsStylusCaptureWithinChangedKey, args);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0002511C File Offset: 0x0002451C
		private static void IsKeyboardFocused_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((UIElement)d).RaiseIsKeyboardFocusedChanged(e);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.UIElement.IsKeyboardFocused" /> é alterado neste elemento.</summary>
		// Token: 0x140000AE RID: 174
		// (add) Token: 0x060008D1 RID: 2257 RVA: 0x00025138 File Offset: 0x00024538
		// (remove) Token: 0x060008D2 RID: 2258 RVA: 0x00025154 File Offset: 0x00024554
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

		/// <summary>Invocado quando um evento <see cref="E:System.Windows.UIElement.IsKeyboardFocusedChanged" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060008D3 RID: 2259 RVA: 0x00025170 File Offset: 0x00024570
		protected virtual void OnIsKeyboardFocusedChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00025180 File Offset: 0x00024580
		private void RaiseIsKeyboardFocusedChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsKeyboardFocusedChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsKeyboardFocusedChangedKey, args);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x000251A0 File Offset: 0x000245A0
		internal bool ReadFlag(CoreFlags field)
		{
			return (this._flags & field) > CoreFlags.None;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x000251B8 File Offset: 0x000245B8
		internal void WriteFlag(CoreFlags field, bool value)
		{
			if (value)
			{
				this._flags |= field;
				return;
			}
			this._flags &= ~field;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x000251E8 File Offset: 0x000245E8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		static UIElement()
		{
			UIElement.PreviewMouseDownEvent = Mouse.PreviewMouseDownEvent.AddOwner(UIElement._typeofThis);
			UIElement.MouseDownEvent = Mouse.MouseDownEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewMouseUpEvent = Mouse.PreviewMouseUpEvent.AddOwner(UIElement._typeofThis);
			UIElement.MouseUpEvent = Mouse.MouseUpEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewMouseLeftButtonDownEvent = EventManager.RegisterRoutedEvent("PreviewMouseLeftButtonDown", RoutingStrategy.Direct, typeof(MouseButtonEventHandler), UIElement._typeofThis);
			UIElement.MouseLeftButtonDownEvent = EventManager.RegisterRoutedEvent("MouseLeftButtonDown", RoutingStrategy.Direct, typeof(MouseButtonEventHandler), UIElement._typeofThis);
			UIElement.PreviewMouseLeftButtonUpEvent = EventManager.RegisterRoutedEvent("PreviewMouseLeftButtonUp", RoutingStrategy.Direct, typeof(MouseButtonEventHandler), UIElement._typeofThis);
			UIElement.MouseLeftButtonUpEvent = EventManager.RegisterRoutedEvent("MouseLeftButtonUp", RoutingStrategy.Direct, typeof(MouseButtonEventHandler), UIElement._typeofThis);
			UIElement.PreviewMouseRightButtonDownEvent = EventManager.RegisterRoutedEvent("PreviewMouseRightButtonDown", RoutingStrategy.Direct, typeof(MouseButtonEventHandler), UIElement._typeofThis);
			UIElement.MouseRightButtonDownEvent = EventManager.RegisterRoutedEvent("MouseRightButtonDown", RoutingStrategy.Direct, typeof(MouseButtonEventHandler), UIElement._typeofThis);
			UIElement.PreviewMouseRightButtonUpEvent = EventManager.RegisterRoutedEvent("PreviewMouseRightButtonUp", RoutingStrategy.Direct, typeof(MouseButtonEventHandler), UIElement._typeofThis);
			UIElement.MouseRightButtonUpEvent = EventManager.RegisterRoutedEvent("MouseRightButtonUp", RoutingStrategy.Direct, typeof(MouseButtonEventHandler), UIElement._typeofThis);
			UIElement.PreviewMouseMoveEvent = Mouse.PreviewMouseMoveEvent.AddOwner(UIElement._typeofThis);
			UIElement.MouseMoveEvent = Mouse.MouseMoveEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewMouseWheelEvent = Mouse.PreviewMouseWheelEvent.AddOwner(UIElement._typeofThis);
			UIElement.MouseWheelEvent = Mouse.MouseWheelEvent.AddOwner(UIElement._typeofThis);
			UIElement.MouseEnterEvent = Mouse.MouseEnterEvent.AddOwner(UIElement._typeofThis);
			UIElement.MouseLeaveEvent = Mouse.MouseLeaveEvent.AddOwner(UIElement._typeofThis);
			UIElement.GotMouseCaptureEvent = Mouse.GotMouseCaptureEvent.AddOwner(UIElement._typeofThis);
			UIElement.LostMouseCaptureEvent = Mouse.LostMouseCaptureEvent.AddOwner(UIElement._typeofThis);
			UIElement.QueryCursorEvent = Mouse.QueryCursorEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewStylusDownEvent = Stylus.PreviewStylusDownEvent.AddOwner(UIElement._typeofThis);
			UIElement.StylusDownEvent = Stylus.StylusDownEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewStylusUpEvent = Stylus.PreviewStylusUpEvent.AddOwner(UIElement._typeofThis);
			UIElement.StylusUpEvent = Stylus.StylusUpEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewStylusMoveEvent = Stylus.PreviewStylusMoveEvent.AddOwner(UIElement._typeofThis);
			UIElement.StylusMoveEvent = Stylus.StylusMoveEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewStylusInAirMoveEvent = Stylus.PreviewStylusInAirMoveEvent.AddOwner(UIElement._typeofThis);
			UIElement.StylusInAirMoveEvent = Stylus.StylusInAirMoveEvent.AddOwner(UIElement._typeofThis);
			UIElement.StylusEnterEvent = Stylus.StylusEnterEvent.AddOwner(UIElement._typeofThis);
			UIElement.StylusLeaveEvent = Stylus.StylusLeaveEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewStylusInRangeEvent = Stylus.PreviewStylusInRangeEvent.AddOwner(UIElement._typeofThis);
			UIElement.StylusInRangeEvent = Stylus.StylusInRangeEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewStylusOutOfRangeEvent = Stylus.PreviewStylusOutOfRangeEvent.AddOwner(UIElement._typeofThis);
			UIElement.StylusOutOfRangeEvent = Stylus.StylusOutOfRangeEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewStylusSystemGestureEvent = Stylus.PreviewStylusSystemGestureEvent.AddOwner(UIElement._typeofThis);
			UIElement.StylusSystemGestureEvent = Stylus.StylusSystemGestureEvent.AddOwner(UIElement._typeofThis);
			UIElement.GotStylusCaptureEvent = Stylus.GotStylusCaptureEvent.AddOwner(UIElement._typeofThis);
			UIElement.LostStylusCaptureEvent = Stylus.LostStylusCaptureEvent.AddOwner(UIElement._typeofThis);
			UIElement.StylusButtonDownEvent = Stylus.StylusButtonDownEvent.AddOwner(UIElement._typeofThis);
			UIElement.StylusButtonUpEvent = Stylus.StylusButtonUpEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewStylusButtonDownEvent = Stylus.PreviewStylusButtonDownEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewStylusButtonUpEvent = Stylus.PreviewStylusButtonUpEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewKeyDownEvent = Keyboard.PreviewKeyDownEvent.AddOwner(UIElement._typeofThis);
			UIElement.KeyDownEvent = Keyboard.KeyDownEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewKeyUpEvent = Keyboard.PreviewKeyUpEvent.AddOwner(UIElement._typeofThis);
			UIElement.KeyUpEvent = Keyboard.KeyUpEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewGotKeyboardFocusEvent = Keyboard.PreviewGotKeyboardFocusEvent.AddOwner(UIElement._typeofThis);
			UIElement.GotKeyboardFocusEvent = Keyboard.GotKeyboardFocusEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewLostKeyboardFocusEvent = Keyboard.PreviewLostKeyboardFocusEvent.AddOwner(UIElement._typeofThis);
			UIElement.LostKeyboardFocusEvent = Keyboard.LostKeyboardFocusEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewTextInputEvent = TextCompositionManager.PreviewTextInputEvent.AddOwner(UIElement._typeofThis);
			UIElement.TextInputEvent = TextCompositionManager.TextInputEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewQueryContinueDragEvent = DragDrop.PreviewQueryContinueDragEvent.AddOwner(UIElement._typeofThis);
			UIElement.QueryContinueDragEvent = DragDrop.QueryContinueDragEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewGiveFeedbackEvent = DragDrop.PreviewGiveFeedbackEvent.AddOwner(UIElement._typeofThis);
			UIElement.GiveFeedbackEvent = DragDrop.GiveFeedbackEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewDragEnterEvent = DragDrop.PreviewDragEnterEvent.AddOwner(UIElement._typeofThis);
			UIElement.DragEnterEvent = DragDrop.DragEnterEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewDragOverEvent = DragDrop.PreviewDragOverEvent.AddOwner(UIElement._typeofThis);
			UIElement.DragOverEvent = DragDrop.DragOverEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewDragLeaveEvent = DragDrop.PreviewDragLeaveEvent.AddOwner(UIElement._typeofThis);
			UIElement.DragLeaveEvent = DragDrop.DragLeaveEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewDropEvent = DragDrop.PreviewDropEvent.AddOwner(UIElement._typeofThis);
			UIElement.DropEvent = DragDrop.DropEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewTouchDownEvent = Touch.PreviewTouchDownEvent.AddOwner(UIElement._typeofThis);
			UIElement.TouchDownEvent = Touch.TouchDownEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewTouchMoveEvent = Touch.PreviewTouchMoveEvent.AddOwner(UIElement._typeofThis);
			UIElement.TouchMoveEvent = Touch.TouchMoveEvent.AddOwner(UIElement._typeofThis);
			UIElement.PreviewTouchUpEvent = Touch.PreviewTouchUpEvent.AddOwner(UIElement._typeofThis);
			UIElement.TouchUpEvent = Touch.TouchUpEvent.AddOwner(UIElement._typeofThis);
			UIElement.GotTouchCaptureEvent = Touch.GotTouchCaptureEvent.AddOwner(UIElement._typeofThis);
			UIElement.LostTouchCaptureEvent = Touch.LostTouchCaptureEvent.AddOwner(UIElement._typeofThis);
			UIElement.TouchEnterEvent = Touch.TouchEnterEvent.AddOwner(UIElement._typeofThis);
			UIElement.TouchLeaveEvent = Touch.TouchLeaveEvent.AddOwner(UIElement._typeofThis);
			UIElement.IsMouseDirectlyOverPropertyKey = DependencyProperty.RegisterReadOnly("IsMouseDirectlyOver", typeof(bool), UIElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(UIElement.IsMouseDirectlyOver_Changed)));
			UIElement.IsMouseDirectlyOverProperty = UIElement.IsMouseDirectlyOverPropertyKey.DependencyProperty;
			UIElement.IsMouseDirectlyOverChangedKey = new EventPrivateKey();
			UIElement.IsMouseOverPropertyKey = DependencyProperty.RegisterReadOnly("IsMouseOver", typeof(bool), UIElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox));
			UIElement.IsMouseOverProperty = UIElement.IsMouseOverPropertyKey.DependencyProperty;
			UIElement.IsStylusOverPropertyKey = DependencyProperty.RegisterReadOnly("IsStylusOver", typeof(bool), UIElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox));
			UIElement.IsStylusOverProperty = UIElement.IsStylusOverPropertyKey.DependencyProperty;
			UIElement.IsKeyboardFocusWithinPropertyKey = DependencyProperty.RegisterReadOnly("IsKeyboardFocusWithin", typeof(bool), UIElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox));
			UIElement.IsKeyboardFocusWithinProperty = UIElement.IsKeyboardFocusWithinPropertyKey.DependencyProperty;
			UIElement.IsKeyboardFocusWithinChangedKey = new EventPrivateKey();
			UIElement.IsMouseCapturedPropertyKey = DependencyProperty.RegisterReadOnly("IsMouseCaptured", typeof(bool), UIElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(UIElement.IsMouseCaptured_Changed)));
			UIElement.IsMouseCapturedProperty = UIElement.IsMouseCapturedPropertyKey.DependencyProperty;
			UIElement.IsMouseCapturedChangedKey = new EventPrivateKey();
			UIElement.IsMouseCaptureWithinPropertyKey = DependencyProperty.RegisterReadOnly("IsMouseCaptureWithin", typeof(bool), UIElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox));
			UIElement.IsMouseCaptureWithinProperty = UIElement.IsMouseCaptureWithinPropertyKey.DependencyProperty;
			UIElement.IsMouseCaptureWithinChangedKey = new EventPrivateKey();
			UIElement.IsStylusDirectlyOverPropertyKey = DependencyProperty.RegisterReadOnly("IsStylusDirectlyOver", typeof(bool), UIElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(UIElement.IsStylusDirectlyOver_Changed)));
			UIElement.IsStylusDirectlyOverProperty = UIElement.IsStylusDirectlyOverPropertyKey.DependencyProperty;
			UIElement.IsStylusDirectlyOverChangedKey = new EventPrivateKey();
			UIElement.IsStylusCapturedPropertyKey = DependencyProperty.RegisterReadOnly("IsStylusCaptured", typeof(bool), UIElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(UIElement.IsStylusCaptured_Changed)));
			UIElement.IsStylusCapturedProperty = UIElement.IsStylusCapturedPropertyKey.DependencyProperty;
			UIElement.IsStylusCapturedChangedKey = new EventPrivateKey();
			UIElement.IsStylusCaptureWithinPropertyKey = DependencyProperty.RegisterReadOnly("IsStylusCaptureWithin", typeof(bool), UIElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox));
			UIElement.IsStylusCaptureWithinProperty = UIElement.IsStylusCaptureWithinPropertyKey.DependencyProperty;
			UIElement.IsStylusCaptureWithinChangedKey = new EventPrivateKey();
			UIElement.IsKeyboardFocusedPropertyKey = DependencyProperty.RegisterReadOnly("IsKeyboardFocused", typeof(bool), UIElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(UIElement.IsKeyboardFocused_Changed)));
			UIElement.IsKeyboardFocusedProperty = UIElement.IsKeyboardFocusedPropertyKey.DependencyProperty;
			UIElement.IsKeyboardFocusedChangedKey = new EventPrivateKey();
			UIElement.AreAnyTouchesDirectlyOverPropertyKey = DependencyProperty.RegisterReadOnly("AreAnyTouchesDirectlyOver", typeof(bool), UIElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox));
			UIElement.AreAnyTouchesDirectlyOverProperty = UIElement.AreAnyTouchesDirectlyOverPropertyKey.DependencyProperty;
			UIElement.AreAnyTouchesOverPropertyKey = DependencyProperty.RegisterReadOnly("AreAnyTouchesOver", typeof(bool), UIElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox));
			UIElement.AreAnyTouchesOverProperty = UIElement.AreAnyTouchesOverPropertyKey.DependencyProperty;
			UIElement.AreAnyTouchesCapturedPropertyKey = DependencyProperty.RegisterReadOnly("AreAnyTouchesCaptured", typeof(bool), UIElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox));
			UIElement.AreAnyTouchesCapturedProperty = UIElement.AreAnyTouchesCapturedPropertyKey.DependencyProperty;
			UIElement.AreAnyTouchesCapturedWithinPropertyKey = DependencyProperty.RegisterReadOnly("AreAnyTouchesCapturedWithin", typeof(bool), UIElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox));
			UIElement.AreAnyTouchesCapturedWithinProperty = UIElement.AreAnyTouchesCapturedWithinPropertyKey.DependencyProperty;
			UIElement.AllowDropProperty = DependencyProperty.Register("AllowDrop", typeof(bool), typeof(UIElement), new PropertyMetadata(BooleanBoxes.FalseBox));
			UIElement.RenderTransformProperty = DependencyProperty.Register("RenderTransform", typeof(Transform), typeof(UIElement), new PropertyMetadata(Transform.Identity, new PropertyChangedCallback(UIElement.RenderTransform_Changed)));
			UIElement.RenderTransformOriginProperty = DependencyProperty.Register("RenderTransformOrigin", typeof(Point), typeof(UIElement), new PropertyMetadata(new Point(0.0, 0.0), new PropertyChangedCallback(UIElement.RenderTransformOrigin_Changed)), new ValidateValueCallback(UIElement.IsRenderTransformOriginValid));
			UIElement.OpacityProperty = DependencyProperty.Register("Opacity", typeof(double), typeof(UIElement), new UIPropertyMetadata(1.0, new PropertyChangedCallback(UIElement.Opacity_Changed)));
			UIElement.OpacityMaskProperty = DependencyProperty.Register("OpacityMask", typeof(Brush), typeof(UIElement), new UIPropertyMetadata(new PropertyChangedCallback(UIElement.OpacityMask_Changed)));
			UIElement.BitmapEffectProperty = DependencyProperty.Register("BitmapEffect", typeof(BitmapEffect), typeof(UIElement), new UIPropertyMetadata(new PropertyChangedCallback(UIElement.OnBitmapEffectChanged)));
			UIElement.EffectProperty = DependencyProperty.Register("Effect", typeof(Effect), typeof(UIElement), new UIPropertyMetadata(new PropertyChangedCallback(UIElement.OnEffectChanged)));
			UIElement.BitmapEffectInputProperty = DependencyProperty.Register("BitmapEffectInput", typeof(BitmapEffectInput), typeof(UIElement), new UIPropertyMetadata(new PropertyChangedCallback(UIElement.OnBitmapEffectInputChanged)));
			UIElement.CacheModeProperty = DependencyProperty.Register("CacheMode", typeof(CacheMode), typeof(UIElement), new UIPropertyMetadata(new PropertyChangedCallback(UIElement.OnCacheModeChanged)));
			UIElement.UidProperty = DependencyProperty.Register("Uid", typeof(string), typeof(UIElement), new UIPropertyMetadata(string.Empty));
			UIElement.VisibilityProperty = DependencyProperty.Register("Visibility", typeof(Visibility), typeof(UIElement), new PropertyMetadata(VisibilityBoxes.VisibleBox, new PropertyChangedCallback(UIElement.OnVisibilityChanged)), new ValidateValueCallback(UIElement.ValidateVisibility));
			UIElement.ClipToBoundsProperty = DependencyProperty.Register("ClipToBounds", typeof(bool), typeof(UIElement), new PropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(UIElement.ClipToBounds_Changed)));
			UIElement.ClipProperty = DependencyProperty.Register("Clip", typeof(Geometry), typeof(UIElement), new PropertyMetadata(null, new PropertyChangedCallback(UIElement.Clip_Changed)));
			UIElement.SnapsToDevicePixelsProperty = DependencyProperty.Register("SnapsToDevicePixels", typeof(bool), typeof(UIElement), new PropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(UIElement.SnapsToDevicePixels_Changed)));
			UIElement.GotFocusEvent = FocusManager.GotFocusEvent.AddOwner(typeof(UIElement));
			UIElement.LostFocusEvent = FocusManager.LostFocusEvent.AddOwner(typeof(UIElement));
			UIElement.IsFocusedPropertyKey = DependencyProperty.RegisterReadOnly("IsFocused", typeof(bool), typeof(UIElement), new PropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(UIElement.IsFocused_Changed)));
			UIElement.IsFocusedProperty = UIElement.IsFocusedPropertyKey.DependencyProperty;
			UIElement.IsEnabledProperty = DependencyProperty.Register("IsEnabled", typeof(bool), typeof(UIElement), new UIPropertyMetadata(BooleanBoxes.TrueBox, new PropertyChangedCallback(UIElement.OnIsEnabledChanged), new CoerceValueCallback(UIElement.CoerceIsEnabled)));
			UIElement.IsEnabledChangedKey = new EventPrivateKey();
			UIElement.IsHitTestVisibleProperty = DependencyProperty.Register("IsHitTestVisible", typeof(bool), typeof(UIElement), new UIPropertyMetadata(BooleanBoxes.TrueBox, new PropertyChangedCallback(UIElement.OnIsHitTestVisibleChanged), new CoerceValueCallback(UIElement.CoerceIsHitTestVisible)));
			UIElement.IsHitTestVisibleChangedKey = new EventPrivateKey();
			UIElement._isVisibleMetadata = new ReadOnlyPropertyMetadata(BooleanBoxes.FalseBox, new GetReadOnlyValueCallback(UIElement.GetIsVisible), new PropertyChangedCallback(UIElement.OnIsVisibleChanged));
			UIElement.IsVisiblePropertyKey = DependencyProperty.RegisterReadOnly("IsVisible", typeof(bool), typeof(UIElement), UIElement._isVisibleMetadata);
			UIElement.IsVisibleProperty = UIElement.IsVisiblePropertyKey.DependencyProperty;
			UIElement.IsVisibleChangedKey = new EventPrivateKey();
			UIElement.FocusableProperty = DependencyProperty.Register("Focusable", typeof(bool), typeof(UIElement), new UIPropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(UIElement.OnFocusableChanged)));
			UIElement.FocusableChangedKey = new EventPrivateKey();
			UIElement.IsManipulationEnabledProperty = DependencyProperty.Register("IsManipulationEnabled", typeof(bool), typeof(UIElement), new PropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(UIElement.OnIsManipulationEnabledChanged)));
			UIElement.ManipulationStartingEvent = Manipulation.ManipulationStartingEvent.AddOwner(typeof(UIElement));
			UIElement.ManipulationStartedEvent = Manipulation.ManipulationStartedEvent.AddOwner(typeof(UIElement));
			UIElement.ManipulationDeltaEvent = Manipulation.ManipulationDeltaEvent.AddOwner(typeof(UIElement));
			UIElement.ManipulationInertiaStartingEvent = Manipulation.ManipulationInertiaStartingEvent.AddOwner(typeof(UIElement));
			UIElement.ManipulationBoundaryFeedbackEvent = Manipulation.ManipulationBoundaryFeedbackEvent.AddOwner(typeof(UIElement));
			UIElement.ManipulationCompletedEvent = Manipulation.ManipulationCompletedEvent.AddOwner(typeof(UIElement));
			UIElement.DpiScaleXValues = new List<double>(3);
			UIElement.DpiScaleYValues = new List<double>(3);
			UIElement.DpiLock = new object();
			UIElement._dpiScaleX = 1.0;
			UIElement._dpiScaleY = 1.0;
			UIElement._setDpi = true;
			UIElement.EventHandlersStoreField = new UncommonField<EventHandlersStore>();
			UIElement.InputBindingCollectionField = new UncommonField<InputBindingCollection>();
			UIElement.CommandBindingCollectionField = new UncommonField<CommandBindingCollection>();
			UIElement.LayoutUpdatedListItemsField = new UncommonField<object>();
			UIElement.LayoutUpdatedHandlersField = new UncommonField<EventHandler>();
			UIElement.StylusPlugInsField = new UncommonField<StylusPlugInCollection>();
			UIElement.AutomationPeerField = new UncommonField<AutomationPeer>();
			UIElement._positionAndSizeOfSetController = new UncommonField<WeakReference<UIElement>>();
			UIElement.AutomationNotSupportedByDefaultField = new UncommonField<bool>();
			UIElement.FocusWithinProperty = new FocusWithinProperty();
			UIElement.MouseOverProperty = new MouseOverProperty();
			UIElement.MouseCaptureWithinProperty = new MouseCaptureWithinProperty();
			UIElement.StylusOverProperty = new StylusOverProperty();
			UIElement.StylusCaptureWithinProperty = new StylusCaptureWithinProperty();
			UIElement.TouchesOverProperty = new TouchesOverProperty();
			UIElement.TouchesCapturedWithinProperty = new TouchesCapturedWithinProperty();
			UIElement.RegisterEvents(typeof(UIElement));
			RenderOptions.EdgeModeProperty.OverrideMetadata(typeof(UIElement), new UIPropertyMetadata(new PropertyChangedCallback(UIElement.EdgeMode_Changed)));
			RenderOptions.BitmapScalingModeProperty.OverrideMetadata(typeof(UIElement), new UIPropertyMetadata(new PropertyChangedCallback(UIElement.BitmapScalingMode_Changed)));
			RenderOptions.ClearTypeHintProperty.OverrideMetadata(typeof(UIElement), new UIPropertyMetadata(new PropertyChangedCallback(UIElement.ClearTypeHint_Changed)));
			TextOptionsInternal.TextHintingModeProperty.OverrideMetadata(typeof(UIElement), new UIPropertyMetadata(new PropertyChangedCallback(UIElement.TextHintingMode_Changed)));
			EventManager.RegisterClassHandler(typeof(UIElement), UIElement.ManipulationStartingEvent, new EventHandler<ManipulationStartingEventArgs>(UIElement.OnManipulationStartingThunk));
			EventManager.RegisterClassHandler(typeof(UIElement), UIElement.ManipulationStartedEvent, new EventHandler<ManipulationStartedEventArgs>(UIElement.OnManipulationStartedThunk));
			EventManager.RegisterClassHandler(typeof(UIElement), UIElement.ManipulationDeltaEvent, new EventHandler<ManipulationDeltaEventArgs>(UIElement.OnManipulationDeltaThunk));
			EventManager.RegisterClassHandler(typeof(UIElement), UIElement.ManipulationInertiaStartingEvent, new EventHandler<ManipulationInertiaStartingEventArgs>(UIElement.OnManipulationInertiaStartingThunk));
			EventManager.RegisterClassHandler(typeof(UIElement), UIElement.ManipulationBoundaryFeedbackEvent, new EventHandler<ManipulationBoundaryFeedbackEventArgs>(UIElement.OnManipulationBoundaryFeedbackThunk));
			EventManager.RegisterClassHandler(typeof(UIElement), UIElement.ManipulationCompletedEvent, new EventHandler<ManipulationCompletedEventArgs>(UIElement.OnManipulationCompletedThunk));
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.UIElement" />.</summary>
		// Token: 0x060008D8 RID: 2264 RVA: 0x000263C0 File Offset: 0x000257C0
		public UIElement()
		{
			this.Initialize();
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x000263DC File Offset: 0x000257DC
		private void Initialize()
		{
			base.BeginPropertyInitialization();
			this.NeverMeasured = true;
			this.NeverArranged = true;
			this.SnapsToDevicePixelsCache = (bool)UIElement.SnapsToDevicePixelsProperty.GetDefaultValue(base.DependencyObjectType);
			this.ClipToBoundsCache = (bool)UIElement.ClipToBoundsProperty.GetDefaultValue(base.DependencyObjectType);
			this.VisibilityCache = (Visibility)UIElement.VisibilityProperty.GetDefaultValue(base.DependencyObjectType);
			base.SetFlags(true, VisualFlags.IsUIElement);
			if (EventTrace.IsEnabled(EventTrace.Keyword.KeywordGeneral, EventTrace.Level.Verbose))
			{
				PerfService.GetPerfElementID(this);
			}
		}

		/// <summary>Obtém ou define um valor indicando se um elemento pode ser usado como o destino de uma operação de arrastar e soltar.  É uma propriedade de dependência.</summary>
		/// <returns>
		///   <see langword="true" /> se um elemento pode ser usado como o destino de uma operação do tipo "arrastar e soltar"; caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x00026468 File Offset: 0x00025868
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x00026488 File Offset: 0x00025888
		public bool AllowDrop
		{
			get
			{
				return (bool)base.GetValue(UIElement.AllowDropProperty);
			}
			set
			{
				base.SetValue(UIElement.AllowDropProperty, BooleanBoxes.Box(value));
			}
		}

		/// <summary>Obtém uma coleção de todos os objetos de plug-in (personalização) de caneta associados a esse elemento.</summary>
		/// <returns>A coleção de plug-ins de caneta como uma coleção especializada.</returns>
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x000264A8 File Offset: 0x000258A8
		protected StylusPlugInCollection StylusPlugIns
		{
			get
			{
				StylusPlugInCollection stylusPlugInCollection = UIElement.StylusPlugInsField.GetValue(this);
				if (stylusPlugInCollection == null)
				{
					stylusPlugInCollection = new StylusPlugInCollection(this);
					UIElement.StylusPlugInsField.SetValue(this, stylusPlugInCollection);
				}
				return stylusPlugInCollection;
			}
		}

		/// <summary>Obtém o tamanho que esse elemento calculou durante o passo de medição do processo de layout.</summary>
		/// <returns>O tamanho calculado, que se torna o tamanho desejado para o passo de organização.</returns>
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x000264D8 File Offset: 0x000258D8
		public Size DesiredSize
		{
			get
			{
				if (this.Visibility == Visibility.Collapsed)
				{
					return new Size(0.0, 0.0);
				}
				return this._desiredSize;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x0002650C File Offset: 0x0002590C
		internal Size PreviousConstraint
		{
			get
			{
				return this._previousAvailableSize;
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00026520 File Offset: 0x00025920
		private bool IsRenderable()
		{
			return !this.NeverMeasured && !this.NeverArranged && !this.ReadFlag(CoreFlags.IsCollapsed) && this.IsMeasureValid && this.IsArrangeValid;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00026560 File Offset: 0x00025960
		internal void InvalidateMeasureInternal()
		{
			this.MeasureDirty = true;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00026574 File Offset: 0x00025974
		internal void InvalidateArrangeInternal()
		{
			this.ArrangeDirty = true;
		}

		/// <summary>Obtém um valor que indica se o tamanho atual retornado pela medida de layout é válido.</summary>
		/// <returns>
		///   <see langword="true" /> se o cálculo da medida de layout retornou um valor válido e atual; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00026588 File Offset: 0x00025988
		public bool IsMeasureValid
		{
			get
			{
				return !this.MeasureDirty;
			}
		}

		/// <summary>Obtém um valor que indica se o tamanho e a posição calculados dos elementos filho no layout do elemento são válidos.</summary>
		/// <returns>
		///   <see langword="true" /> se o tamanho e a posição de layout forem válidos; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x000265A0 File Offset: 0x000259A0
		public bool IsArrangeValid
		{
			get
			{
				return !this.ArrangeDirty;
			}
		}

		/// <summary>Invalida o estado da medida (layout) do elemento.</summary>
		// Token: 0x060008E4 RID: 2276 RVA: 0x000265B8 File Offset: 0x000259B8
		public void InvalidateMeasure()
		{
			if (!this.MeasureDirty && !this.MeasureInProgress)
			{
				if (!this.NeverMeasured)
				{
					ContextLayoutManager contextLayoutManager = ContextLayoutManager.From(base.Dispatcher);
					if (EventTrace.IsEnabled(EventTrace.Keyword.KeywordLayout, EventTrace.Level.Verbose) && contextLayoutManager.MeasureQueue.IsEmpty)
					{
						EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientLayoutInvalidated, EventTrace.Keyword.KeywordLayout, EventTrace.Level.Verbose, PerfService.GetPerfElementID(this));
					}
					contextLayoutManager.MeasureQueue.Add(this);
				}
				this.MeasureDirty = true;
			}
		}

		/// <summary>Invalida o estado da organização (layout) do elemento. Após a invalidação, o elemento terá seu layout atualizado, o que ocorrerá de forma assíncrona a menos que posteriormente seja forçado por <see cref="M:System.Windows.UIElement.UpdateLayout" />.</summary>
		// Token: 0x060008E5 RID: 2277 RVA: 0x00026638 File Offset: 0x00025A38
		public void InvalidateArrange()
		{
			if (!this.ArrangeDirty && !this.ArrangeInProgress)
			{
				if (!this.NeverArranged)
				{
					ContextLayoutManager contextLayoutManager = ContextLayoutManager.From(base.Dispatcher);
					contextLayoutManager.ArrangeQueue.Add(this);
				}
				this.ArrangeDirty = true;
			}
		}

		/// <summary>Invalida a renderização do elemento e força uma nova passagem de layout completa. <see cref="M:System.Windows.UIElement.OnRender(System.Windows.Media.DrawingContext)" /> é chamado após a conclusão do ciclo de layout.</summary>
		// Token: 0x060008E6 RID: 2278 RVA: 0x0002667C File Offset: 0x00025A7C
		public void InvalidateVisual()
		{
			this.InvalidateArrange();
			this.RenderingInvalidated = true;
		}

		/// <summary>Dá suporte ao comportamento de layout quando um elemento filho é redimensionado.</summary>
		/// <param name="child">O elemento filho que está sendo redimensionado.</param>
		// Token: 0x060008E7 RID: 2279 RVA: 0x00026698 File Offset: 0x00025A98
		protected virtual void OnChildDesiredSizeChanged(UIElement child)
		{
			if (this.IsMeasureValid)
			{
				this.InvalidateMeasure();
			}
		}

		/// <summary>Ocorre quando o layout dos vários elementos visuais associados ao <see cref="T:System.Windows.Threading.Dispatcher" /> atual é alterado.</summary>
		// Token: 0x140000AF RID: 175
		// (add) Token: 0x060008E8 RID: 2280 RVA: 0x000266B4 File Offset: 0x00025AB4
		// (remove) Token: 0x060008E9 RID: 2281 RVA: 0x000266EC File Offset: 0x00025AEC
		public event EventHandler LayoutUpdated
		{
			add
			{
				if (this.getLayoutUpdatedHandler(value) == null)
				{
					LayoutEventList.ListItem item = ContextLayoutManager.From(base.Dispatcher).LayoutEvents.Add(value);
					this.addLayoutUpdatedHandler(value, item);
				}
			}
			remove
			{
				LayoutEventList.ListItem layoutUpdatedHandler = this.getLayoutUpdatedHandler(value);
				if (layoutUpdatedHandler != null)
				{
					this.removeLayoutUpdatedHandler(value);
					ContextLayoutManager.From(base.Dispatcher).LayoutEvents.Remove(layoutUpdatedHandler);
				}
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00026724 File Offset: 0x00025B24
		private void addLayoutUpdatedHandler(EventHandler handler, LayoutEventList.ListItem item)
		{
			object value = UIElement.LayoutUpdatedListItemsField.GetValue(this);
			if (value == null)
			{
				UIElement.LayoutUpdatedListItemsField.SetValue(this, item);
				UIElement.LayoutUpdatedHandlersField.SetValue(this, handler);
				return;
			}
			EventHandler value2 = UIElement.LayoutUpdatedHandlersField.GetValue(this);
			if (value2 != null)
			{
				Hashtable hashtable = new Hashtable(2);
				hashtable.Add(value2, value);
				hashtable.Add(handler, item);
				UIElement.LayoutUpdatedHandlersField.ClearValue(this);
				UIElement.LayoutUpdatedListItemsField.SetValue(this, hashtable);
				return;
			}
			Hashtable hashtable2 = (Hashtable)value;
			hashtable2.Add(handler, item);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x000267A8 File Offset: 0x00025BA8
		private LayoutEventList.ListItem getLayoutUpdatedHandler(EventHandler d)
		{
			object value = UIElement.LayoutUpdatedListItemsField.GetValue(this);
			if (value == null)
			{
				return null;
			}
			EventHandler value2 = UIElement.LayoutUpdatedHandlersField.GetValue(this);
			if (value2 == null)
			{
				Hashtable hashtable = (Hashtable)value;
				return (LayoutEventList.ListItem)hashtable[d];
			}
			if (value2 == d)
			{
				return (LayoutEventList.ListItem)value;
			}
			return null;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x000267FC File Offset: 0x00025BFC
		private void removeLayoutUpdatedHandler(EventHandler d)
		{
			object value = UIElement.LayoutUpdatedListItemsField.GetValue(this);
			EventHandler value2 = UIElement.LayoutUpdatedHandlersField.GetValue(this);
			if (value2 != null)
			{
				if (value2 == d)
				{
					UIElement.LayoutUpdatedListItemsField.ClearValue(this);
					UIElement.LayoutUpdatedHandlersField.ClearValue(this);
					return;
				}
			}
			else
			{
				Hashtable hashtable = (Hashtable)value;
				hashtable.Remove(d);
			}
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00026854 File Offset: 0x00025C54
		internal static void PropagateSuspendLayout(Visual v)
		{
			if (v.CheckFlagsAnd(VisualFlags.IsLayoutIslandRoot))
			{
				return;
			}
			if (v.CheckFlagsAnd(VisualFlags.IsLayoutSuspended))
			{
				return;
			}
			if (Invariant.Strict && v.CheckFlagsAnd(VisualFlags.IsUIElement))
			{
				UIElement uielement = (UIElement)v;
				Invariant.Assert(!uielement.MeasureInProgress && !uielement.ArrangeInProgress);
			}
			v.SetFlags(true, VisualFlags.IsLayoutSuspended);
			v.TreeLevel = 0U;
			int internalVisualChildrenCount = v.InternalVisualChildrenCount;
			for (int i = 0; i < internalVisualChildrenCount; i++)
			{
				Visual visual = v.InternalGetVisualChild(i);
				if (visual != null)
				{
					UIElement.PropagateSuspendLayout(visual);
				}
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x000268DC File Offset: 0x00025CDC
		internal static void PropagateResumeLayout(Visual parent, Visual v)
		{
			if (v.CheckFlagsAnd(VisualFlags.IsLayoutIslandRoot))
			{
				return;
			}
			bool flag = parent != null && parent.CheckFlagsAnd(VisualFlags.IsLayoutSuspended);
			uint num = (parent == null) ? 0U : parent.TreeLevel;
			if (flag)
			{
				return;
			}
			v.SetFlags(false, VisualFlags.IsLayoutSuspended);
			v.TreeLevel = num + 1U;
			if (v.CheckFlagsAnd(VisualFlags.IsUIElement))
			{
				UIElement uielement = (UIElement)v;
				Invariant.Assert(!uielement.MeasureInProgress && !uielement.ArrangeInProgress);
				bool flag2 = uielement.MeasureDirty && !uielement.NeverMeasured && uielement.MeasureRequest == null;
				bool flag3 = uielement.ArrangeDirty && !uielement.NeverArranged && uielement.ArrangeRequest == null;
				ContextLayoutManager contextLayoutManager = (flag2 || flag3) ? ContextLayoutManager.From(uielement.Dispatcher) : null;
				if (flag2)
				{
					contextLayoutManager.MeasureQueue.Add(uielement);
				}
				if (flag3)
				{
					contextLayoutManager.ArrangeQueue.Add(uielement);
				}
			}
			int internalVisualChildrenCount = v.InternalVisualChildrenCount;
			for (int i = 0; i < internalVisualChildrenCount; i++)
			{
				Visual visual = v.InternalGetVisualChild(i);
				if (visual != null)
				{
					UIElement.PropagateResumeLayout(v, visual);
				}
			}
		}

		/// <summary>Atualiza <see cref="P:System.Windows.UIElement.DesiredSize" /> de um <see cref="T:System.Windows.UIElement" />. Elementos pai chamam esse método de suas próprias implementações <see cref="M:System.Windows.UIElement.MeasureCore(System.Windows.Size)" /> para formar uma atualização de layout recursiva. Chamar esse método constitui a primeira passagem (a passagem "Medida") de uma atualização de layout.</summary>
		/// <param name="availableSize">O espaço disponível que um elemento pai pode alocar um elemento filho. Um elemento filho pode solicitar um espaço maior do que o disponível. O tamanho fornecido pode ser acomodado se a rolagem for possível no modelo de conteúdo do elemento atual.</param>
		// Token: 0x060008EF RID: 2287 RVA: 0x000269F0 File Offset: 0x00025DF0
		public void Measure(Size availableSize)
		{
			bool flag = false;
			long num = 0L;
			ContextLayoutManager contextLayoutManager = ContextLayoutManager.From(base.Dispatcher);
			if (contextLayoutManager.AutomationEvents.Count != 0)
			{
				UIElementHelper.InvalidateAutomationAncestors(this);
			}
			if (EventTrace.IsEnabled(EventTrace.Keyword.KeywordLayout, EventTrace.Level.Verbose))
			{
				num = PerfService.GetPerfElementID(this);
				flag = true;
				EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientMeasureElementBegin, EventTrace.Keyword.KeywordLayout, EventTrace.Level.Verbose, new object[]
				{
					num,
					availableSize.Width,
					availableSize.Height
				});
			}
			try
			{
				using (base.Dispatcher.DisableProcessing())
				{
					if (DoubleUtil.IsNaN(availableSize.Width) || DoubleUtil.IsNaN(availableSize.Height))
					{
						throw new InvalidOperationException(SR.Get("UIElement_Layout_NaNMeasure"));
					}
					bool neverMeasured = this.NeverMeasured;
					if (neverMeasured)
					{
						this.switchVisibilityIfNeeded(this.Visibility);
						this.pushVisualEffects();
					}
					bool flag2 = DoubleUtil.AreClose(availableSize, this._previousAvailableSize);
					if (this.Visibility == Visibility.Collapsed || base.CheckFlagsAnd(VisualFlags.IsLayoutSuspended))
					{
						if (this.MeasureRequest != null)
						{
							ContextLayoutManager.From(base.Dispatcher).MeasureQueue.Remove(this);
						}
						if (!flag2)
						{
							this.InvalidateMeasureInternal();
							this._previousAvailableSize = availableSize;
						}
					}
					else if (!this.IsMeasureValid || neverMeasured || !flag2)
					{
						this.NeverMeasured = false;
						Size desiredSize = this._desiredSize;
						this.InvalidateArrange();
						this.MeasureInProgress = true;
						Size size = new Size(0.0, 0.0);
						ContextLayoutManager contextLayoutManager2 = ContextLayoutManager.From(base.Dispatcher);
						bool flag3 = true;
						try
						{
							contextLayoutManager2.EnterMeasure();
							size = this.MeasureCore(availableSize);
							flag3 = false;
						}
						finally
						{
							this.MeasureInProgress = false;
							this._previousAvailableSize = availableSize;
							contextLayoutManager2.ExitMeasure();
							if (flag3 && contextLayoutManager2.GetLastExceptionElement() == null)
							{
								contextLayoutManager2.SetLastExceptionElement(this);
							}
						}
						if (double.IsPositiveInfinity(size.Width) || double.IsPositiveInfinity(size.Height))
						{
							throw new InvalidOperationException(SR.Get("UIElement_Layout_PositiveInfinityReturned", new object[]
							{
								base.GetType().FullName
							}));
						}
						if (DoubleUtil.IsNaN(size.Width) || DoubleUtil.IsNaN(size.Height))
						{
							throw new InvalidOperationException(SR.Get("UIElement_Layout_NaNReturned", new object[]
							{
								base.GetType().FullName
							}));
						}
						this.MeasureDirty = false;
						if (this.MeasureRequest != null)
						{
							ContextLayoutManager.From(base.Dispatcher).MeasureQueue.Remove(this);
						}
						this._desiredSize = size;
						if (!this.MeasureDuringArrange && !DoubleUtil.AreClose(desiredSize, size))
						{
							UIElement uielement;
							IContentHost contentHost;
							this.GetUIParentOrICH(out uielement, out contentHost);
							if (uielement != null && !uielement.MeasureInProgress)
							{
								uielement.OnChildDesiredSizeChanged(this);
							}
							else if (contentHost != null)
							{
								contentHost.OnChildDesiredSizeChanged(this);
							}
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientMeasureElementEnd, EventTrace.Keyword.KeywordLayout, EventTrace.Level.Verbose, new object[]
					{
						num,
						this._desiredSize.Width,
						this._desiredSize.Height
					});
				}
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00026D5C File Offset: 0x0002615C
		internal void GetUIParentOrICH(out UIElement uiParent, out IContentHost ich)
		{
			ich = null;
			uiParent = null;
			for (Visual visual = VisualTreeHelper.GetParent(this) as Visual; visual != null; visual = (VisualTreeHelper.GetParent(visual) as Visual))
			{
				ich = (visual as IContentHost);
				if (ich != null)
				{
					break;
				}
				if (visual.CheckFlagsAnd(VisualFlags.IsUIElement))
				{
					uiParent = (UIElement)visual;
					return;
				}
			}
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00026DAC File Offset: 0x000261AC
		internal UIElement GetUIParentWithinLayoutIsland()
		{
			UIElement result = null;
			Visual visual = VisualTreeHelper.GetParent(this) as Visual;
			while (visual != null && !visual.CheckFlagsAnd(VisualFlags.IsLayoutIslandRoot))
			{
				if (visual.CheckFlagsAnd(VisualFlags.IsUIElement))
				{
					result = (UIElement)visual;
					break;
				}
				visual = (VisualTreeHelper.GetParent(visual) as Visual);
			}
			return result;
		}

		/// <summary>Posiciona elementos filho e determina um tamanho para um <see cref="T:System.Windows.UIElement" />. Elementos pai chamam esse método de sua própria implementação <see cref="M:System.Windows.UIElement.ArrangeCore(System.Windows.Rect)" /> (ou um equivalente no nível de estrutura WPF) para formar uma atualização de layout recursiva. Esse método constitui a segunda passagem de uma atualização de layout.</summary>
		/// <param name="finalRect">O tamanho final que o elemento pai computa para o filho, fornecido como uma instância <see cref="T:System.Windows.Rect" />.</param>
		// Token: 0x060008F2 RID: 2290 RVA: 0x00026DF8 File Offset: 0x000261F8
		public void Arrange(Rect finalRect)
		{
			bool flag = false;
			long num = 0L;
			ContextLayoutManager contextLayoutManager = ContextLayoutManager.From(base.Dispatcher);
			if (contextLayoutManager.AutomationEvents.Count != 0)
			{
				UIElementHelper.InvalidateAutomationAncestors(this);
			}
			if (EventTrace.IsEnabled(EventTrace.Keyword.KeywordLayout, EventTrace.Level.Verbose))
			{
				num = PerfService.GetPerfElementID(this);
				flag = true;
				EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientArrangeElementBegin, EventTrace.Keyword.KeywordLayout, EventTrace.Level.Verbose, new object[]
				{
					num,
					finalRect.Top,
					finalRect.Left,
					finalRect.Width,
					finalRect.Height
				});
			}
			try
			{
				using (base.Dispatcher.DisableProcessing())
				{
					if (double.IsPositiveInfinity(finalRect.Width) || double.IsPositiveInfinity(finalRect.Height) || DoubleUtil.IsNaN(finalRect.Width) || DoubleUtil.IsNaN(finalRect.Height))
					{
						DependencyObject dependencyObject = this.GetUIParent() as UIElement;
						throw new InvalidOperationException(SR.Get("UIElement_Layout_InfinityArrange", new object[]
						{
							(dependencyObject == null) ? "" : dependencyObject.GetType().FullName,
							base.GetType().FullName
						}));
					}
					if (this.Visibility == Visibility.Collapsed || base.CheckFlagsAnd(VisualFlags.IsLayoutSuspended))
					{
						if (this.ArrangeRequest != null)
						{
							ContextLayoutManager.From(base.Dispatcher).ArrangeQueue.Remove(this);
						}
						this._finalRect = finalRect;
					}
					else
					{
						if (this.MeasureDirty || this.NeverMeasured)
						{
							try
							{
								this.MeasureDuringArrange = true;
								if (this.NeverMeasured)
								{
									this.Measure(finalRect.Size);
								}
								else
								{
									this.Measure(this.PreviousConstraint);
								}
							}
							finally
							{
								this.MeasureDuringArrange = false;
							}
						}
						if (!this.IsArrangeValid || this.NeverArranged || !DoubleUtil.AreClose(finalRect, this._finalRect))
						{
							bool neverArranged = this.NeverArranged;
							this.NeverArranged = false;
							this.ArrangeInProgress = true;
							ContextLayoutManager contextLayoutManager2 = ContextLayoutManager.From(base.Dispatcher);
							Size renderSize = this.RenderSize;
							bool flag2 = false;
							bool flag3 = true;
							if (base.CheckFlagsAnd(VisualFlags.UseLayoutRounding))
							{
								DpiScale dpi = base.GetDpi();
								finalRect = UIElement.RoundLayoutRect(finalRect, dpi.DpiScaleX, dpi.DpiScaleY);
							}
							try
							{
								contextLayoutManager2.EnterArrange();
								this.ArrangeCore(finalRect);
								this.ensureClip(finalRect.Size);
								flag2 = this.markForSizeChangedIfNeeded(renderSize, this.RenderSize);
								flag3 = false;
							}
							finally
							{
								this.ArrangeInProgress = false;
								contextLayoutManager2.ExitArrange();
								if (flag3 && contextLayoutManager2.GetLastExceptionElement() == null)
								{
									contextLayoutManager2.SetLastExceptionElement(this);
								}
							}
							this._finalRect = finalRect;
							this.ArrangeDirty = false;
							if (this.ArrangeRequest != null)
							{
								ContextLayoutManager.From(base.Dispatcher).ArrangeQueue.Remove(this);
							}
							if ((flag2 || this.RenderingInvalidated || neverArranged) && this.IsRenderable())
							{
								DrawingContext drawingContext = this.RenderOpen();
								try
								{
									bool flag4 = EventTrace.IsEnabled(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Level.Verbose);
									if (flag4)
									{
										EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientOnRenderBegin, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Level.Verbose, num);
									}
									try
									{
										this.OnRender(drawingContext);
									}
									finally
									{
										if (flag4)
										{
											EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientOnRenderEnd, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Level.Verbose, num);
										}
									}
								}
								finally
								{
									drawingContext.Close();
									this.RenderingInvalidated = false;
								}
								this.updatePixelSnappingGuidelines();
							}
							if (neverArranged)
							{
								base.EndPropertyInitialization();
							}
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientArrangeElementEnd, EventTrace.Keyword.KeywordLayout, EventTrace.Level.Verbose, new object[]
					{
						num,
						finalRect.Top,
						finalRect.Left,
						finalRect.Width,
						finalRect.Height
					});
				}
			}
		}

		/// <summary>Quando substituído em uma classe derivada, participa de operações de renderização direcionadas pelo sistema de layout. As instruções de renderização para esse elemento não são usadas diretamente quando este método é invocado e, em vez disso, são preservadas para serem usadas posteriormente de forma assíncrona pelo layout e desenho.</summary>
		/// <param name="drawingContext">As instruções de desenho para um elemento específico. Esse contexto é fornecido para o sistema de layout.</param>
		// Token: 0x060008F3 RID: 2291 RVA: 0x00027258 File Offset: 0x00026658
		protected virtual void OnRender(DrawingContext drawingContext)
		{
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00027268 File Offset: 0x00026668
		private void updatePixelSnappingGuidelines()
		{
			if (!this.SnapsToDevicePixels || this._drawingContent == null)
			{
				base.VisualXSnappingGuidelines = (base.VisualYSnappingGuidelines = null);
				return;
			}
			DoubleCollection visualXSnappingGuidelines = base.VisualXSnappingGuidelines;
			if (visualXSnappingGuidelines == null)
			{
				base.VisualXSnappingGuidelines = new DoubleCollection
				{
					0.0,
					this.RenderSize.Width
				};
			}
			else
			{
				int index = visualXSnappingGuidelines.Count - 1;
				if (!DoubleUtil.AreClose(visualXSnappingGuidelines[index], this.RenderSize.Width))
				{
					visualXSnappingGuidelines[index] = this.RenderSize.Width;
				}
			}
			DoubleCollection visualYSnappingGuidelines = base.VisualYSnappingGuidelines;
			if (visualYSnappingGuidelines == null)
			{
				base.VisualYSnappingGuidelines = new DoubleCollection
				{
					0.0,
					this.RenderSize.Height
				};
				return;
			}
			int index2 = visualYSnappingGuidelines.Count - 1;
			if (!DoubleUtil.AreClose(visualYSnappingGuidelines[index2], this.RenderSize.Height))
			{
				visualYSnappingGuidelines[index2] = this.RenderSize.Height;
			}
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00027388 File Offset: 0x00026788
		private bool markForSizeChangedIfNeeded(Size oldSize, Size newSize)
		{
			bool flag = !DoubleUtil.AreClose(oldSize.Width, newSize.Width);
			bool flag2 = !DoubleUtil.AreClose(oldSize.Height, newSize.Height);
			SizeChangedInfo sizeChangedInfo = this.sizeChangedInfo;
			if (sizeChangedInfo != null)
			{
				sizeChangedInfo.Update(flag, flag2);
				return true;
			}
			if (flag || flag2)
			{
				sizeChangedInfo = new SizeChangedInfo(this, oldSize, flag, flag2);
				this.sizeChangedInfo = sizeChangedInfo;
				ContextLayoutManager.From(base.Dispatcher).AddToSizeChangedChain(sizeChangedInfo);
				Visual.PropagateFlags(this, VisualFlags.IsSubtreeDirtyForPrecompute, VisualProxyFlags.IsSubtreeDirtyForRender);
				return true;
			}
			return false;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0002740C File Offset: 0x0002680C
		internal static Size RoundLayoutSize(Size size, double dpiScaleX, double dpiScaleY)
		{
			return new Size(UIElement.RoundLayoutValue(size.Width, dpiScaleX), UIElement.RoundLayoutValue(size.Height, dpiScaleY));
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00027438 File Offset: 0x00026838
		internal static double RoundLayoutValue(double value, double dpiScale)
		{
			double num;
			if (!DoubleUtil.AreClose(dpiScale, 1.0))
			{
				num = Math.Round(value * dpiScale) / dpiScale;
				if (DoubleUtil.IsNaN(num) || double.IsInfinity(num) || DoubleUtil.AreClose(num, 1.7976931348623157E+308))
				{
					num = value;
				}
			}
			else
			{
				num = Math.Round(value);
			}
			return num;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00027490 File Offset: 0x00026890
		internal static Rect RoundLayoutRect(Rect rect, double dpiScaleX, double dpiScaleY)
		{
			return new Rect(UIElement.RoundLayoutValue(rect.X, dpiScaleX), UIElement.RoundLayoutValue(rect.Y, dpiScaleY), UIElement.RoundLayoutValue(rect.Width, dpiScaleX), UIElement.RoundLayoutValue(rect.Height, dpiScaleY));
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x000274D8 File Offset: 0x000268D8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static DpiScale EnsureDpiScale()
		{
			if (UIElement._setDpi)
			{
				UIElement._setDpi = false;
				HandleRef hWnd = new HandleRef(null, IntPtr.Zero);
				IntPtr dc = UnsafeNativeMethods.GetDC(hWnd);
				if (dc == IntPtr.Zero)
				{
					throw new Win32Exception();
				}
				try
				{
					int deviceCaps = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, dc), 88);
					int deviceCaps2 = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, dc), 90);
					UIElement._dpiScaleX = (double)deviceCaps / 96.0;
					UIElement._dpiScaleY = (double)deviceCaps2 / 96.0;
				}
				finally
				{
					UnsafeNativeMethods.ReleaseDC(hWnd, new HandleRef(null, dc));
				}
			}
			return new DpiScale(UIElement._dpiScaleX, UIElement._dpiScaleY);
		}

		/// <summary>Quando substituído em uma classe derivada, participa de operações de renderização direcionadas pelo sistema de layout. Esse método será invocado após a atualização do layout e antes da renderização se o <see cref="P:System.Windows.UIElement.RenderSize" /> do elemento tiver sido alterado como resultado da atualização do layout.</summary>
		/// <param name="info">Os parâmetros empacotados (<see cref="T:System.Windows.SizeChangedInfo" />), que incluem tamanhos novos e antigos e cuja dimensão é realmente alterada.</param>
		// Token: 0x060008FA RID: 2298 RVA: 0x0002759C File Offset: 0x0002699C
		protected internal virtual void OnRenderSizeChanged(SizeChangedInfo info)
		{
		}

		/// <summary>Quando substituído em uma classe derivada, fornece uma lógica de medida para o dimensionamento correto desse elemento, levando em consideração o tamanho do conteúdo de elementos filho.</summary>
		/// <param name="availableSize">O tamanho disponível que o elemento pai pode alocar para o filho.</param>
		/// <returns>O tamanho desejado desse elemento no layout.</returns>
		// Token: 0x060008FB RID: 2299 RVA: 0x000275AC File Offset: 0x000269AC
		protected virtual Size MeasureCore(Size availableSize)
		{
			return new Size(0.0, 0.0);
		}

		/// <summary>Define o modelo para a definição de layout de disposição de nível de núcleo do WPF.</summary>
		/// <param name="finalRect">A área final no pai que esse elemento deve usar para organizar a si próprio e seus filhos.</param>
		// Token: 0x060008FC RID: 2300 RVA: 0x000275D0 File Offset: 0x000269D0
		protected virtual void ArrangeCore(Rect finalRect)
		{
			this.RenderSize = finalRect.Size;
			Transform transform = this.RenderTransform;
			if (transform == Transform.Identity)
			{
				transform = null;
			}
			Vector visualOffset = base.VisualOffset;
			if (!DoubleUtil.AreClose(visualOffset.X, finalRect.X) || !DoubleUtil.AreClose(visualOffset.Y, finalRect.Y))
			{
				base.VisualOffset = new Vector(finalRect.X, finalRect.Y);
			}
			if (transform != null)
			{
				TransformGroup transformGroup = new TransformGroup();
				Point renderTransformOrigin = this.RenderTransformOrigin;
				bool flag = renderTransformOrigin.X != 0.0 || renderTransformOrigin.Y != 0.0;
				if (flag)
				{
					transformGroup.Children.Add(new TranslateTransform(-(finalRect.Width * renderTransformOrigin.X), -(finalRect.Height * renderTransformOrigin.Y)));
				}
				transformGroup.Children.Add(transform);
				if (flag)
				{
					transformGroup.Children.Add(new TranslateTransform(finalRect.Width * renderTransformOrigin.X, finalRect.Height * renderTransformOrigin.Y));
				}
				base.VisualTransform = transformGroup;
				return;
			}
			base.VisualTransform = null;
		}

		/// <summary>Obtém (ou define) o tamanho de renderização final deste elemento.</summary>
		/// <returns>O tamanho renderizado para este elemento.</returns>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00027704 File Offset: 0x00026B04
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x0002772C File Offset: 0x00026B2C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Size RenderSize
		{
			get
			{
				if (this.Visibility == Visibility.Collapsed)
				{
					return default(Size);
				}
				return this._size;
			}
			set
			{
				this._size = value;
				base.InvalidateHitTestBounds();
			}
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00027748 File Offset: 0x00026B48
		internal override Rect GetHitTestBounds()
		{
			Rect result = new Rect(this._size);
			if (this._drawingContent != null)
			{
				MediaContext mediaContext = MediaContext.From(base.Dispatcher);
				BoundsDrawingContextWalker ctx = mediaContext.AcquireBoundsDrawingContextWalker();
				Rect contentBounds = this._drawingContent.GetContentBounds(ctx);
				mediaContext.ReleaseBoundsDrawingContextWalker(ctx);
				result.Union(contentBounds);
			}
			return result;
		}

		/// <summary>Obtém ou define informações de transformação que afetam a posição da renderização desse elemento.  É uma propriedade de dependência.</summary>
		/// <returns>Descreve as especificações de transformação de renderização desejadas. O padrão é <see cref="P:System.Windows.Media.Transform.Identity" />.</returns>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x0002779C File Offset: 0x00026B9C
		// (set) Token: 0x06000901 RID: 2305 RVA: 0x000277BC File Offset: 0x00026BBC
		public Transform RenderTransform
		{
			get
			{
				return (Transform)base.GetValue(UIElement.RenderTransformProperty);
			}
			set
			{
				base.SetValue(UIElement.RenderTransformProperty, value);
			}
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x000277D8 File Offset: 0x00026BD8
		private static void RenderTransform_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement uielement = (UIElement)d;
			if (!uielement.NeverMeasured && !uielement.NeverArranged && !e.IsASubPropertyChange)
			{
				uielement.InvalidateArrange();
				uielement.AreTransformsClean = false;
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00027814 File Offset: 0x00026C14
		private static bool IsRenderTransformOriginValid(object value)
		{
			Point point = (Point)value;
			return !DoubleUtil.IsNaN(point.X) && !double.IsPositiveInfinity(point.X) && !double.IsNegativeInfinity(point.X) && (!DoubleUtil.IsNaN(point.Y) && !double.IsPositiveInfinity(point.Y)) && !double.IsNegativeInfinity(point.Y);
		}

		/// <summary>Obtém ou define o ponto central de qualquer transformação de renderização possível declarada por <see cref="P:System.Windows.UIElement.RenderTransform" />, em relação aos limites do elemento.  É uma propriedade de dependência.</summary>
		/// <returns>O valor que declara a transformação de renderização. O valor padrão é um <see cref="T:System.Windows.Point" /> com coordenadas (0,0).</returns>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x00027884 File Offset: 0x00026C84
		// (set) Token: 0x06000905 RID: 2309 RVA: 0x000278A4 File Offset: 0x00026CA4
		public Point RenderTransformOrigin
		{
			get
			{
				return (Point)base.GetValue(UIElement.RenderTransformOriginProperty);
			}
			set
			{
				base.SetValue(UIElement.RenderTransformOriginProperty, value);
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x000278C4 File Offset: 0x00026CC4
		private static void RenderTransformOrigin_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement uielement = (UIElement)d;
			if (!uielement.NeverMeasured && !uielement.NeverArranged)
			{
				uielement.InvalidateArrange();
				uielement.AreTransformsClean = false;
			}
		}

		/// <summary>Invocado quando o elemento pai desse <see cref="T:System.Windows.UIElement" /> relata uma alteração ao seu pai visual subjacente.</summary>
		/// <param name="oldParent">O pai anterior. Pode ser fornecido como <see langword="null" /> se o <see cref="T:System.Windows.DependencyObject" /> não teve um elemento pai anteriormente.</param>
		// Token: 0x06000907 RID: 2311 RVA: 0x000278F8 File Offset: 0x00026CF8
		protected internal override void OnVisualParentChanged(DependencyObject oldParent)
		{
			if (this._parent != null)
			{
				DependencyObject dependencyObject = this._parent;
				if (!InputElement.IsUIElement(dependencyObject) && !InputElement.IsUIElement3D(dependencyObject))
				{
					Visual visual = dependencyObject as Visual;
					if (visual != null)
					{
						visual.VisualAncestorChanged += this.OnVisualAncestorChanged_ForceInherit;
						dependencyObject = InputElement.GetContainingUIElement(visual);
					}
					else
					{
						Visual3D visual3D = dependencyObject as Visual3D;
						if (visual3D != null)
						{
							visual3D.VisualAncestorChanged += this.OnVisualAncestorChanged_ForceInherit;
							dependencyObject = InputElement.GetContainingUIElement(visual3D);
						}
					}
				}
				if (dependencyObject != null)
				{
					UIElement.SynchronizeForceInheritProperties(this, null, null, dependencyObject);
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
					UIElement.SynchronizeForceInheritProperties(this, null, null, dependencyObject2);
				}
			}
			this.SynchronizeReverseInheritPropertyFlags(oldParent, true);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x000279E8 File Offset: 0x00026DE8
		private void OnVisualAncestorChanged_ForceInherit(object sender, AncestorChangedEventArgs e)
		{
			DependencyObject dependencyObject;
			if (e.OldParent == null)
			{
				dependencyObject = InputElement.GetContainingUIElement(this._parent);
				if (dependencyObject != null && VisualTreeHelper.IsAncestorOf(e.Ancestor, dependencyObject))
				{
					dependencyObject = null;
				}
			}
			else
			{
				dependencyObject = InputElement.GetContainingUIElement(this._parent);
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
				UIElement.SynchronizeForceInheritProperties(this, null, null, dependencyObject);
			}
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00027A4C File Offset: 0x00026E4C
		internal void OnVisualAncestorChanged(object sender, AncestorChangedEventArgs e)
		{
			UIElement uielement = sender as UIElement;
			if (uielement != null)
			{
				PresentationSource.OnVisualAncestorChanged(uielement, e);
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00027A6C File Offset: 0x00026E6C
		internal DependencyObject GetUIParent()
		{
			return UIElementHelper.GetUIParent(this, false);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00027A80 File Offset: 0x00026E80
		internal DependencyObject GetUIParent(bool continuePastVisualTree)
		{
			return UIElementHelper.GetUIParent(this, continuePastVisualTree);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00027A94 File Offset: 0x00026E94
		internal DependencyObject GetUIParentNo3DTraversal()
		{
			DependencyObject internalVisualParent = base.InternalVisualParent;
			return InputElement.GetContainingUIElement(internalVisualParent, true);
		}

		/// <summary>Quando substituído em uma classe derivada, retornará um pai UI (interface do usuário) alternativo para esse elemento se nenhum pai visual existir.</summary>
		/// <returns>Um objeto se a implementação de uma classe derivada tiver uma conexão alternativa pai com o relatório.</returns>
		// Token: 0x0600090D RID: 2317 RVA: 0x00027AB4 File Offset: 0x00026EB4
		protected internal virtual DependencyObject GetUIParentCore()
		{
			return null;
		}

		/// <summary>Garante que todos os elementos filho visuais desse elemento tenham o layout atualizado corretamente.</summary>
		// Token: 0x0600090E RID: 2318 RVA: 0x00027AC4 File Offset: 0x00026EC4
		public void UpdateLayout()
		{
			ContextLayoutManager.From(base.Dispatcher).UpdateLayout();
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00027AE4 File Offset: 0x00026EE4
		internal static void BuildRouteHelper(DependencyObject e, EventRoute route, RoutedEventArgs args)
		{
			if (route == null)
			{
				throw new ArgumentNullException("route");
			}
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			if (args.Source == null)
			{
				throw new ArgumentException(SR.Get("SourceNotSet"));
			}
			if (args.RoutedEvent != route.RoutedEvent)
			{
				throw new ArgumentException(SR.Get("Mismatched_RoutedEvent"));
			}
			if (args.RoutedEvent.RoutingStrategy == RoutingStrategy.Direct)
			{
				UIElement uielement = e as UIElement;
				ContentElement contentElement = null;
				UIElement3D uielement3D = null;
				if (uielement == null)
				{
					contentElement = (e as ContentElement);
					if (contentElement == null)
					{
						uielement3D = (e as UIElement3D);
					}
				}
				if (uielement != null)
				{
					uielement.AddToEventRoute(route, args);
					return;
				}
				if (contentElement != null)
				{
					contentElement.AddToEventRoute(route, args);
					return;
				}
				if (uielement3D != null)
				{
					uielement3D.AddToEventRoute(route, args);
					return;
				}
			}
			else
			{
				int num = 0;
				while (e != null)
				{
					UIElement uielement2 = e as UIElement;
					ContentElement contentElement2 = null;
					UIElement3D uielement3D2 = null;
					if (uielement2 == null)
					{
						contentElement2 = (e as ContentElement);
						if (contentElement2 == null)
						{
							uielement3D2 = (e as UIElement3D);
						}
					}
					if (num++ > 4096)
					{
						throw new InvalidOperationException(SR.Get("TreeLoop"));
					}
					object obj = null;
					if (uielement2 != null)
					{
						obj = uielement2.AdjustEventSource(args);
					}
					else if (contentElement2 != null)
					{
						obj = contentElement2.AdjustEventSource(args);
					}
					else if (uielement3D2 != null)
					{
						obj = uielement3D2.AdjustEventSource(args);
					}
					if (obj != null)
					{
						route.AddSource(obj);
					}
					if (uielement2 != null)
					{
						uielement2.AddSynchronizedInputPreOpportunityHandler(route, args);
						bool continuePastVisualTree = uielement2.BuildRouteCore(route, args);
						uielement2.AddToEventRoute(route, args);
						uielement2.AddSynchronizedInputPostOpportunityHandler(route, args);
						e = uielement2.GetUIParent(continuePastVisualTree);
					}
					else if (contentElement2 != null)
					{
						contentElement2.AddSynchronizedInputPreOpportunityHandler(route, args);
						bool continuePastVisualTree = contentElement2.BuildRouteCore(route, args);
						contentElement2.AddToEventRoute(route, args);
						contentElement2.AddSynchronizedInputPostOpportunityHandler(route, args);
						e = contentElement2.GetUIParent(continuePastVisualTree);
					}
					else if (uielement3D2 != null)
					{
						uielement3D2.AddSynchronizedInputPreOpportunityHandler(route, args);
						bool continuePastVisualTree = uielement3D2.BuildRouteCore(route, args);
						uielement3D2.AddToEventRoute(route, args);
						uielement3D2.AddSynchronizedInputPostOpportunityHandler(route, args);
						e = uielement3D2.GetUIParent(continuePastVisualTree);
					}
					if (e == args.Source)
					{
						route.AddSource(e);
					}
				}
			}
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00027CC4 File Offset: 0x000270C4
		internal void AddSynchronizedInputPreOpportunityHandler(EventRoute route, RoutedEventArgs args)
		{
			if (InputManager.IsSynchronizedInput)
			{
				if (SynchronizedInputHelper.IsListening(this, args))
				{
					RoutedEventHandler eventHandler = new RoutedEventHandler(this.SynchronizedInputPreOpportunityHandler);
					SynchronizedInputHelper.AddHandlerToRoute(this, route, eventHandler, false);
					return;
				}
				this.AddSynchronizedInputPreOpportunityHandlerCore(route, args);
			}
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00027D00 File Offset: 0x00027100
		internal virtual void AddSynchronizedInputPreOpportunityHandlerCore(EventRoute route, RoutedEventArgs args)
		{
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00027D10 File Offset: 0x00027110
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

		// Token: 0x06000913 RID: 2323 RVA: 0x00027D4C File Offset: 0x0002714C
		internal void SynchronizedInputPreOpportunityHandler(object sender, RoutedEventArgs args)
		{
			SynchronizedInputHelper.PreOpportunityHandler(sender, args);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00027D60 File Offset: 0x00027160
		internal void SynchronizedInputPostOpportunityHandler(object sender, RoutedEventArgs args)
		{
			if (args.Handled && InputManager.SynchronizedInputState == SynchronizedInputStates.HadOpportunity)
			{
				SynchronizedInputHelper.PostOpportunityHandler(sender, args);
			}
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00027D84 File Offset: 0x00027184
		internal bool StartListeningSynchronizedInput(SynchronizedInputType inputType)
		{
			if (InputManager.IsSynchronizedInput)
			{
				return false;
			}
			InputManager.StartListeningSynchronizedInput(this, inputType);
			return true;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00027DA4 File Offset: 0x000271A4
		internal void CancelSynchronizedInput()
		{
			InputManager.CancelSynchronizedInput();
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00027DB8 File Offset: 0x000271B8
		[FriendAccessAllowed]
		internal static void AddHandler(DependencyObject d, RoutedEvent routedEvent, Delegate handler)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			UIElement uielement = d as UIElement;
			if (uielement != null)
			{
				uielement.AddHandler(routedEvent, handler);
				return;
			}
			ContentElement contentElement = d as ContentElement;
			if (contentElement != null)
			{
				contentElement.AddHandler(routedEvent, handler);
				return;
			}
			UIElement3D uielement3D = d as UIElement3D;
			if (uielement3D != null)
			{
				uielement3D.AddHandler(routedEvent, handler);
				return;
			}
			throw new ArgumentException(SR.Get("Invalid_IInputElement", new object[]
			{
				d.GetType()
			}));
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00027E2C File Offset: 0x0002722C
		[FriendAccessAllowed]
		internal static void RemoveHandler(DependencyObject d, RoutedEvent routedEvent, Delegate handler)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			UIElement uielement = d as UIElement;
			if (uielement != null)
			{
				uielement.RemoveHandler(routedEvent, handler);
				return;
			}
			ContentElement contentElement = d as ContentElement;
			if (contentElement != null)
			{
				contentElement.RemoveHandler(routedEvent, handler);
				return;
			}
			UIElement3D uielement3D = d as UIElement3D;
			if (uielement3D != null)
			{
				uielement3D.RemoveHandler(routedEvent, handler);
				return;
			}
			throw new ArgumentException(SR.Get("Invalid_IInputElement", new object[]
			{
				d.GetType()
			}));
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00027EA0 File Offset: 0x000272A0
		internal virtual void OnPresentationSourceChanged(bool attached)
		{
			if (!attached && FocusManager.GetFocusedElement(this) != null)
			{
				FocusManager.SetFocusedElement(this, null);
			}
		}

		/// <summary>Converte um ponto em relação a esse elemento para coordenadas que são relativas ao elemento especificado.</summary>
		/// <param name="point">O valor do ponto, como relativo a esse elemento.</param>
		/// <param name="relativeTo">O elemento no qual converter o determinado ponto.</param>
		/// <returns>Um valor de ponto, agora relativo ao elemento de destino em vez desse elemento de origem.</returns>
		// Token: 0x0600091A RID: 2330 RVA: 0x00027EC0 File Offset: 0x000272C0
		public Point TranslatePoint(Point point, UIElement relativeTo)
		{
			return InputElement.TranslatePoint(point, this, relativeTo);
		}

		/// <summary>Retorna o elemento de entrada no elemento atual que está nas coordenadas especificadas em relação à origem do elemento atual.</summary>
		/// <param name="point">As coordenadas de deslocamento dentro desse elemento.</param>
		/// <returns>O filho do elemento que está localizado na posição especificada.</returns>
		// Token: 0x0600091B RID: 2331 RVA: 0x00027ED8 File Offset: 0x000272D8
		public IInputElement InputHitTest(Point point)
		{
			IInputElement result;
			IInputElement inputElement;
			this.InputHitTest(point, out result, out inputElement);
			return result;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00027EF4 File Offset: 0x000272F4
		internal void InputHitTest(Point pt, out IInputElement enabledHit, out IInputElement rawHit)
		{
			HitTestResult hitTestResult;
			this.InputHitTest(pt, out enabledHit, out rawHit, out hitTestResult);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00027F0C File Offset: 0x0002730C
		internal void InputHitTest(Point pt, out IInputElement enabledHit, out IInputElement rawHit, out HitTestResult rawHitResult)
		{
			PointHitTestParameters hitTestParameters = new PointHitTestParameters(pt);
			UIElement.InputHitTestResult inputHitTestResult = new UIElement.InputHitTestResult();
			VisualTreeHelper.HitTest(this, new HitTestFilterCallback(this.InputHitTestFilterCallback), new HitTestResultCallback(inputHitTestResult.InputHitTestResultCallback), hitTestParameters);
			DependencyObject dependencyObject = inputHitTestResult.Result;
			rawHit = (dependencyObject as IInputElement);
			rawHitResult = inputHitTestResult.HitTestResult;
			enabledHit = null;
			while (dependencyObject != null)
			{
				IContentHost contentHost = dependencyObject as IContentHost;
				if (contentHost != null)
				{
					DependencyObject containingUIElement = InputElement.GetContainingUIElement(dependencyObject);
					if ((bool)containingUIElement.GetValue(UIElement.IsEnabledProperty))
					{
						pt = InputElement.TranslatePoint(pt, this, dependencyObject);
						IInputElement inputElement;
						rawHit = (inputElement = contentHost.InputHitTest(pt));
						enabledHit = inputElement;
						rawHitResult = null;
						if (enabledHit != null)
						{
							break;
						}
					}
				}
				UIElement uielement = dependencyObject as UIElement;
				if (uielement != null)
				{
					if (rawHit == null)
					{
						rawHit = uielement;
						rawHitResult = null;
					}
					if (uielement.IsEnabled)
					{
						enabledHit = uielement;
						return;
					}
				}
				UIElement3D uielement3D = dependencyObject as UIElement3D;
				if (uielement3D != null)
				{
					if (rawHit == null)
					{
						rawHit = uielement3D;
						rawHitResult = null;
					}
					if (uielement3D.IsEnabled)
					{
						enabledHit = uielement3D;
						return;
					}
				}
				if (dependencyObject == this)
				{
					break;
				}
				dependencyObject = VisualTreeHelper.GetParentInternal(dependencyObject);
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00028004 File Offset: 0x00027404
		private HitTestFilterBehavior InputHitTestFilterCallback(DependencyObject currentNode)
		{
			HitTestFilterBehavior result = HitTestFilterBehavior.Continue;
			if (UIElementHelper.IsUIElementOrUIElement3D(currentNode))
			{
				if (!UIElementHelper.IsVisible(currentNode))
				{
					result = HitTestFilterBehavior.ContinueSkipSelfAndChildren;
				}
				if (!UIElementHelper.IsHitTestVisible(currentNode))
				{
					result = HitTestFilterBehavior.ContinueSkipSelfAndChildren;
				}
			}
			else
			{
				result = HitTestFilterBehavior.Continue;
			}
			return result;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00028034 File Offset: 0x00027434
		private static RoutedEvent CrackMouseButtonEvent(MouseButtonEventArgs e)
		{
			RoutedEvent result = null;
			MouseButton changedButton = e.ChangedButton;
			if (changedButton != MouseButton.Left)
			{
				if (changedButton == MouseButton.Right)
				{
					if (e.RoutedEvent == Mouse.PreviewMouseDownEvent)
					{
						result = UIElement.PreviewMouseRightButtonDownEvent;
					}
					else if (e.RoutedEvent == Mouse.MouseDownEvent)
					{
						result = UIElement.MouseRightButtonDownEvent;
					}
					else if (e.RoutedEvent == Mouse.PreviewMouseUpEvent)
					{
						result = UIElement.PreviewMouseRightButtonUpEvent;
					}
					else
					{
						result = UIElement.MouseRightButtonUpEvent;
					}
				}
			}
			else if (e.RoutedEvent == Mouse.PreviewMouseDownEvent)
			{
				result = UIElement.PreviewMouseLeftButtonDownEvent;
			}
			else if (e.RoutedEvent == Mouse.MouseDownEvent)
			{
				result = UIElement.MouseLeftButtonDownEvent;
			}
			else if (e.RoutedEvent == Mouse.PreviewMouseUpEvent)
			{
				result = UIElement.PreviewMouseLeftButtonUpEvent;
			}
			else
			{
				result = UIElement.MouseLeftButtonUpEvent;
			}
			return result;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x000280E8 File Offset: 0x000274E8
		private static void CrackMouseButtonEventAndReRaiseEvent(DependencyObject sender, MouseButtonEventArgs e)
		{
			RoutedEvent routedEvent = UIElement.CrackMouseButtonEvent(e);
			if (routedEvent != null)
			{
				UIElement.ReRaiseEventAs(sender, e, routedEvent);
			}
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00028108 File Offset: 0x00027508
		private static void ReRaiseEventAs(DependencyObject sender, RoutedEventArgs args, RoutedEvent newEvent)
		{
			RoutedEvent routedEvent = args.RoutedEvent;
			args.OverrideRoutedEvent(newEvent);
			object source = args.Source;
			EventRoute eventRoute = EventRouteFactory.FetchObject(args.RoutedEvent);
			if (TraceRoutedEvent.IsEnabled)
			{
				TraceRoutedEvent.Trace(TraceEventType.Start, TraceRoutedEvent.ReRaiseEventAs, new object[]
				{
					args.RoutedEvent,
					sender,
					args,
					args.Handled
				});
			}
			try
			{
				UIElement.BuildRouteHelper(sender, eventRoute, args);
				eventRoute.ReInvokeHandlers(sender, args);
				args.OverrideSource(source);
				args.OverrideRoutedEvent(routedEvent);
			}
			finally
			{
				if (TraceRoutedEvent.IsEnabled)
				{
					TraceRoutedEvent.Trace(TraceEventType.Stop, TraceRoutedEvent.ReRaiseEventAs, new object[]
					{
						args.RoutedEvent,
						sender,
						args,
						args.Handled
					});
				}
			}
			EventRouteFactory.RecycleObject(eventRoute);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x000281F0 File Offset: 0x000275F0
		internal static void RaiseEventImpl(DependencyObject sender, RoutedEventArgs args)
		{
			EventRoute eventRoute = EventRouteFactory.FetchObject(args.RoutedEvent);
			if (TraceRoutedEvent.IsEnabled)
			{
				TraceRoutedEvent.Trace(TraceEventType.Start, TraceRoutedEvent.RaiseEvent, new object[]
				{
					args.RoutedEvent,
					sender,
					args,
					args.Handled
				});
			}
			try
			{
				args.Source = sender;
				UIElement.BuildRouteHelper(sender, eventRoute, args);
				eventRoute.InvokeHandlers(sender, args);
				args.Source = args.OriginalSource;
			}
			finally
			{
				if (TraceRoutedEvent.IsEnabled)
				{
					TraceRoutedEvent.Trace(TraceEventType.Stop, TraceRoutedEvent.RaiseEvent, new object[]
					{
						args.RoutedEvent,
						sender,
						args,
						args.Handled
					});
				}
			}
			EventRouteFactory.RecycleObject(eventRoute);
		}

		/// <summary>Obtém um valor que indica se a posição do ponteiro do mouse corresponde aos resultados de teste de clique, que levam em consideração a composição de elementos.  É uma propriedade de dependência.</summary>
		/// <returns>
		///   <see langword="true" /> se o ponteiro do mouse estiver sobre o mesmo resultado do elemento que um teste de clique; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x000282C8 File Offset: 0x000276C8
		public bool IsMouseDirectlyOver
		{
			get
			{
				return this.IsMouseDirectlyOver_ComputeValue();
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x000282DC File Offset: 0x000276DC
		private bool IsMouseDirectlyOver_ComputeValue()
		{
			return Mouse.DirectlyOver == this;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x000282F4 File Offset: 0x000276F4
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

		// Token: 0x06000926 RID: 2342 RVA: 0x00028380 File Offset: 0x00027780
		internal virtual bool BlockReverseInheritance()
		{
			return false;
		}

		/// <summary>Obtém um valor que indica se o ponteiro do mouse está localizado sobre esse elemento (incluindo os elementos filho na árvore visual).  É uma propriedade de dependência.</summary>
		/// <returns>
		///   <see langword="true" /> se o ponteiro do mouse estiver sobre o elemento ou seus elementos filho; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x00028390 File Offset: 0x00027790
		public bool IsMouseOver
		{
			get
			{
				return this.ReadFlag(CoreFlags.IsMouseOverCache);
			}
		}

		/// <summary>Obtém um valor que indica se o cursor da caneta está localizado sobre esse elemento (incluindo elementos filho visuais).  É uma propriedade de dependência.</summary>
		/// <returns>
		///   <see langword="true" /> se o cursor da caneta está sobre o elemento ou seus elementos filho; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x000283A8 File Offset: 0x000277A8
		public bool IsStylusOver
		{
			get
			{
				return this.ReadFlag(CoreFlags.IsStylusOverCache);
			}
		}

		/// <summary>Obtém um valor que indica se o foco do teclado é em qualquer lugar dentro do elemento ou de seus elementos filho de árvore visual.  É uma propriedade de dependência.</summary>
		/// <returns>
		///   <see langword="true" /> se o foco do teclado está no elemento ou em seus elementos filho; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x000283C0 File Offset: 0x000277C0
		public bool IsKeyboardFocusWithin
		{
			get
			{
				return this.ReadFlag(CoreFlags.IsKeyboardFocusWithinCache);
			}
		}

		/// <summary>Obtém um valor que indica se o mouse é capturado para esse elemento.  É uma propriedade de dependência.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento tiver a captura do mouse; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x000283D8 File Offset: 0x000277D8
		public bool IsMouseCaptured
		{
			get
			{
				return (bool)base.GetValue(UIElement.IsMouseCapturedProperty);
			}
		}

		/// <summary>Tenta forçar a captura do mouse para esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se o mouse for capturado com êxito; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600092B RID: 2347 RVA: 0x000283F8 File Offset: 0x000277F8
		public bool CaptureMouse()
		{
			return Mouse.Capture(this);
		}

		/// <summary>Libera a captura do mouse, se esse elemento tiver mantido a captura.</summary>
		// Token: 0x0600092C RID: 2348 RVA: 0x0002840C File Offset: 0x0002780C
		public void ReleaseMouseCapture()
		{
			if (Mouse.Captured == this)
			{
				Mouse.Capture(null);
			}
		}

		/// <summary>Obtém um valor que determina se a captura do mouse é mantida por esse elemento ou elementos filho em sua árvore visual. É uma propriedade de dependência.</summary>
		/// <returns>
		///   <see langword="true" /> se esse elemento ou um elemento contido tiver captura do mouse; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x00028428 File Offset: 0x00027828
		public bool IsMouseCaptureWithin
		{
			get
			{
				return this.ReadFlag(CoreFlags.IsMouseCaptureWithinCache);
			}
		}

		/// <summary>Obtém um valor que indica se a posição da caneta corresponde aos resultados de teste de clique, que levam em consideração a composição dos elementos.  É uma propriedade de dependência.</summary>
		/// <returns>
		///   <see langword="true" /> se o ponteiro da caneta estiver sobre o mesmo resultado do elemento que um teste de clique; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00028440 File Offset: 0x00027840
		public bool IsStylusDirectlyOver
		{
			get
			{
				return this.IsStylusDirectlyOver_ComputeValue();
			}
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00028454 File Offset: 0x00027854
		private bool IsStylusDirectlyOver_ComputeValue()
		{
			return Stylus.DirectlyOver == this;
		}

		/// <summary>Obtém um valor que indica se a caneta é capturada por este elemento.  É uma propriedade de dependência.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento tem captura da caneta; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x0002846C File Offset: 0x0002786C
		public bool IsStylusCaptured
		{
			get
			{
				return (bool)base.GetValue(UIElement.IsStylusCapturedProperty);
			}
		}

		/// <summary>Tenta forçar a captura da caneta para esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se a caneta for capturada com êxito; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000931 RID: 2353 RVA: 0x0002848C File Offset: 0x0002788C
		public bool CaptureStylus()
		{
			return Stylus.Capture(this);
		}

		/// <summary>Libera a captura do dispositivo de caneta, se esse elemento tiver mantido a captura.</summary>
		// Token: 0x06000932 RID: 2354 RVA: 0x000284A0 File Offset: 0x000278A0
		public void ReleaseStylusCapture()
		{
			Stylus.Capture(null);
		}

		/// <summary>Obtém um valor que determina se a captura da caneta é mantida por esse elemento ou um elemento nos limites do elemento e sua árvore visual. É uma propriedade de dependência.</summary>
		/// <returns>
		///   <see langword="true" /> se esse elemento ou um elemento contido tiver captura de caneta; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x000284B4 File Offset: 0x000278B4
		public bool IsStylusCaptureWithin
		{
			get
			{
				return this.ReadFlag(CoreFlags.IsStylusCaptureWithinCache);
			}
		}

		/// <summary>Obtém um valor que indica se este elemento tem foco do controle.  É uma propriedade de dependência.</summary>
		/// <returns>
		///   <see langword="true" /> se esse elemento tiver o foco do teclado; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x000284CC File Offset: 0x000278CC
		public bool IsKeyboardFocused
		{
			get
			{
				return this.IsKeyboardFocused_ComputeValue();
			}
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x000284E0 File Offset: 0x000278E0
		private bool IsKeyboardFocused_ComputeValue()
		{
			return Keyboard.FocusedElement == this;
		}

		/// <summary>Tenta definir o foco para esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se o foco do teclado e o foco lógico foram definidos para esse elemento; <see langword="false" /> somente se o foco lógico foi definido para esse elemento ou se a chamada para esse método não forçou a mudança de foco.</returns>
		// Token: 0x06000936 RID: 2358 RVA: 0x000284F8 File Offset: 0x000278F8
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
		// Token: 0x06000937 RID: 2359 RVA: 0x00028540 File Offset: 0x00027940
		public virtual bool MoveFocus(TraversalRequest request)
		{
			return false;
		}

		/// <summary>Quando substituído em uma classe derivada, retorna o elemento que deve receber o foco para uma direção de passagem do foco especificada, sem realmente mover o foco para esse elemento.</summary>
		/// <param name="direction">A direção da passagem do foco solicitada.</param>
		/// <returns>O elemento que teria recebido foco, se <see cref="M:System.Windows.UIElement.MoveFocus(System.Windows.Input.TraversalRequest)" /> realmente fosse invocado.</returns>
		// Token: 0x06000938 RID: 2360 RVA: 0x00028550 File Offset: 0x00027950
		public virtual DependencyObject PredictFocus(FocusNavigationDirection direction)
		{
			return null;
		}

		/// <summary>Fornece tratamento de classes para quando uma chave de acesso que seja significativa para esse elemento é chamada.</summary>
		/// <param name="e">Os dados de evento para o evento de chave de acesso. Os relatórios de dados de evento cuja chave foi chamada e indica se o objeto <see cref="T:System.Windows.Input.AccessKeyManager" /> que controla o envio desses eventos também envia essa chamada de chave de acesso a outros elementos.</param>
		// Token: 0x06000939 RID: 2361 RVA: 0x00028560 File Offset: 0x00027960
		protected virtual void OnAccessKey(AccessKeyEventArgs e)
		{
			this.Focus();
		}

		/// <summary>Obtém um valor que indica se um sistema de método de entrada, como um Input Method Editor (IME), está habilitado para processamento de entrada para esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se um método de entrada estiver ativo; caso contrário, <see langword="false" />. O valor padrão da propriedade anexada subjacente é <see langword="true;" />, no entanto, isso será influenciado pelo estado real dos métodos de entrada no tempo de execução.</returns>
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x00028574 File Offset: 0x00027974
		public bool IsInputMethodEnabled
		{
			get
			{
				return (bool)base.GetValue(InputMethod.IsInputMethodEnabledProperty);
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00028594 File Offset: 0x00027994
		private static void Opacity_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement uielement = (UIElement)d;
			uielement.pushOpacity();
		}

		/// <summary>Obtém ou define o fator de opacidade aplicado a todo o <see cref="T:System.Windows.UIElement" /> quando ele é renderizado no UI (interface do usuário).  É uma propriedade de dependência.</summary>
		/// <returns>O fator de opacidade. A opacidade padrão é 1.0. Os valores esperados estão entre 0.0 e 1.0.</returns>
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x000285B0 File Offset: 0x000279B0
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x000285D0 File Offset: 0x000279D0
		[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
		public double Opacity
		{
			get
			{
				return (double)base.GetValue(UIElement.OpacityProperty);
			}
			set
			{
				base.SetValue(UIElement.OpacityProperty, value);
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x000285F0 File Offset: 0x000279F0
		private void pushOpacity()
		{
			if (this.Visibility == Visibility.Visible)
			{
				base.VisualOpacity = this.Opacity;
			}
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00028614 File Offset: 0x00027A14
		private static void OpacityMask_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement uielement = (UIElement)d;
			uielement.pushOpacityMask();
		}

		/// <summary>Obtém ou define uma máscara de opacidade como uma implementação de <see cref="T:System.Windows.Media.Brush" /> que é aplicada a qualquer mascaramento de canal alfa para o conteúdo renderizado deste elemento.  É uma propriedade de dependência.</summary>
		/// <returns>O pincel a ser usado para o mascaramento de opacidade.</returns>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00028630 File Offset: 0x00027A30
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x00028650 File Offset: 0x00027A50
		public Brush OpacityMask
		{
			get
			{
				return (Brush)base.GetValue(UIElement.OpacityMaskProperty);
			}
			set
			{
				base.SetValue(UIElement.OpacityMaskProperty, value);
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0002866C File Offset: 0x00027A6C
		private void pushOpacityMask()
		{
			base.VisualOpacityMask = this.OpacityMask;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00028688 File Offset: 0x00027A88
		private static void OnBitmapEffectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement uielement = (UIElement)d;
			uielement.pushBitmapEffect();
		}

		/// <summary>Obtém ou define um efeito de bitmap aplicado diretamente ao conteúdo renderizado para este elemento.  É uma propriedade de dependência.</summary>
		/// <returns>O efeito de bitmap a ser aplicado.</returns>
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x000286A4 File Offset: 0x00027AA4
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x000286C4 File Offset: 0x00027AC4
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		public BitmapEffect BitmapEffect
		{
			get
			{
				return (BitmapEffect)base.GetValue(UIElement.BitmapEffectProperty);
			}
			set
			{
				base.SetValue(UIElement.BitmapEffectProperty, value);
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x000286E0 File Offset: 0x00027AE0
		private void pushBitmapEffect()
		{
			base.VisualBitmapEffect = this.BitmapEffect;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x000286FC File Offset: 0x00027AFC
		private static void OnEffectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement uielement = (UIElement)d;
			uielement.pushEffect();
		}

		/// <summary>Obtém ou define o efeito de bitmap a ser aplicado ao <see cref="T:System.Windows.UIElement" />. É uma propriedade de dependência.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Effects.Effect" /> que representa o efeito de bitmap.</returns>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x00028718 File Offset: 0x00027B18
		// (set) Token: 0x06000949 RID: 2377 RVA: 0x00028738 File Offset: 0x00027B38
		public Effect Effect
		{
			get
			{
				return (Effect)base.GetValue(UIElement.EffectProperty);
			}
			set
			{
				base.SetValue(UIElement.EffectProperty, value);
			}
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00028754 File Offset: 0x00027B54
		private void pushEffect()
		{
			base.VisualEffect = this.Effect;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00028770 File Offset: 0x00027B70
		private static void OnBitmapEffectInputChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((UIElement)d).pushBitmapEffectInput((BitmapEffectInput)e.NewValue);
		}

		/// <summary>Obtém ou define uma fonte de entrada para o efeito de bitmap aplicado diretamente ao conteúdo renderizado para este elemento.  É uma propriedade de dependência.</summary>
		/// <returns>A fonte dos efeitos de bitmap.</returns>
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00028794 File Offset: 0x00027B94
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x000287B4 File Offset: 0x00027BB4
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		public BitmapEffectInput BitmapEffectInput
		{
			get
			{
				return (BitmapEffectInput)base.GetValue(UIElement.BitmapEffectInputProperty);
			}
			set
			{
				base.SetValue(UIElement.BitmapEffectInputProperty, value);
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x000287D0 File Offset: 0x00027BD0
		private void pushBitmapEffectInput(BitmapEffectInput newValue)
		{
			base.VisualBitmapEffectInput = newValue;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x000287E4 File Offset: 0x00027BE4
		private static void EdgeMode_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement uielement = (UIElement)d;
			uielement.pushEdgeMode();
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00028800 File Offset: 0x00027C00
		private void pushEdgeMode()
		{
			base.VisualEdgeMode = RenderOptions.GetEdgeMode(this);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0002881C File Offset: 0x00027C1C
		private static void BitmapScalingMode_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement uielement = (UIElement)d;
			uielement.pushBitmapScalingMode();
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00028838 File Offset: 0x00027C38
		private void pushBitmapScalingMode()
		{
			base.VisualBitmapScalingMode = RenderOptions.GetBitmapScalingMode(this);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00028854 File Offset: 0x00027C54
		private static void ClearTypeHint_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement uielement = (UIElement)d;
			uielement.pushClearTypeHint();
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00028870 File Offset: 0x00027C70
		private void pushClearTypeHint()
		{
			base.VisualClearTypeHint = RenderOptions.GetClearTypeHint(this);
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0002888C File Offset: 0x00027C8C
		private static void TextHintingMode_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement uielement = (UIElement)d;
			uielement.pushTextHintingMode();
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x000288A8 File Offset: 0x00027CA8
		private void pushTextHintingMode()
		{
			base.VisualTextHintingMode = TextOptionsInternal.GetTextHintingMode(this);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x000288C4 File Offset: 0x00027CC4
		private static void OnCacheModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement uielement = (UIElement)d;
			uielement.pushCacheMode();
		}

		/// <summary>Obtém ou define uma representação armazenada em cache do <see cref="T:System.Windows.UIElement" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.CacheMode" /> que contém uma representação armazenada em cache do <see cref="T:System.Windows.UIElement" />.</returns>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x000288E0 File Offset: 0x00027CE0
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x00028900 File Offset: 0x00027D00
		public CacheMode CacheMode
		{
			get
			{
				return (CacheMode)base.GetValue(UIElement.CacheModeProperty);
			}
			set
			{
				base.SetValue(UIElement.CacheModeProperty, value);
			}
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0002891C File Offset: 0x00027D1C
		private void pushCacheMode()
		{
			base.VisualCacheMode = this.CacheMode;
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00028938 File Offset: 0x00027D38
		private void pushVisualEffects()
		{
			this.pushCacheMode();
			this.pushOpacity();
			this.pushOpacityMask();
			this.pushBitmapEffect();
			this.pushEdgeMode();
			this.pushBitmapScalingMode();
			this.pushClearTypeHint();
			this.pushTextHintingMode();
		}

		/// <summary>Obtém ou define o identificador exclusivo (para localização) para esse elemento. É uma propriedade de dependência.</summary>
		/// <returns>Uma cadeia de caracteres que é o identificador exclusivo deste elemento.</returns>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x00028978 File Offset: 0x00027D78
		// (set) Token: 0x0600095D RID: 2397 RVA: 0x00028998 File Offset: 0x00027D98
		public string Uid
		{
			get
			{
				return (string)base.GetValue(UIElement.UidProperty);
			}
			set
			{
				base.SetValue(UIElement.UidProperty, value);
			}
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x000289B4 File Offset: 0x00027DB4
		private static void OnVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement uielement = (UIElement)d;
			Visibility visibility = (Visibility)e.NewValue;
			uielement.VisibilityCache = visibility;
			uielement.switchVisibilityIfNeeded(visibility);
			uielement.UpdateIsVisibleCache();
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x000289EC File Offset: 0x00027DEC
		private static bool ValidateVisibility(object o)
		{
			Visibility visibility = (Visibility)o;
			return visibility == Visibility.Visible || visibility == Visibility.Hidden || visibility == Visibility.Collapsed;
		}

		/// <summary>Obtém ou define a visibilidade UI (interface do usuário) desse elemento.  É uma propriedade de dependência.</summary>
		/// <returns>Um valor da enumeração. O valor padrão é <see cref="F:System.Windows.Visibility.Visible" />.</returns>
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00028A10 File Offset: 0x00027E10
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x00028A24 File Offset: 0x00027E24
		[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
		public Visibility Visibility
		{
			get
			{
				return this.VisibilityCache;
			}
			set
			{
				base.SetValue(UIElement.VisibilityProperty, VisibilityBoxes.Box(value));
			}
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00028A44 File Offset: 0x00027E44
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

		// Token: 0x06000963 RID: 2403 RVA: 0x00028A7C File Offset: 0x00027E7C
		private void ensureVisible()
		{
			if (this.ReadFlag(CoreFlags.IsOpacitySuppressed))
			{
				base.VisualOpacity = this.Opacity;
				if (this.ReadFlag(CoreFlags.IsCollapsed))
				{
					this.WriteFlag(CoreFlags.IsCollapsed, false);
					this.signalDesiredSizeChange();
					this.InvalidateVisual();
				}
				this.WriteFlag(CoreFlags.IsOpacitySuppressed, false);
			}
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00028AD4 File Offset: 0x00027ED4
		private void ensureInvisible(bool collapsed)
		{
			if (!this.ReadFlag(CoreFlags.IsOpacitySuppressed))
			{
				base.VisualOpacity = 0.0;
				this.WriteFlag(CoreFlags.IsOpacitySuppressed, true);
			}
			if (!this.ReadFlag(CoreFlags.IsCollapsed) && collapsed)
			{
				this.WriteFlag(CoreFlags.IsCollapsed, true);
				this.signalDesiredSizeChange();
				return;
			}
			if (this.ReadFlag(CoreFlags.IsCollapsed) && !collapsed)
			{
				this.WriteFlag(CoreFlags.IsCollapsed, false);
				this.signalDesiredSizeChange();
			}
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00028B50 File Offset: 0x00027F50
		private void signalDesiredSizeChange()
		{
			UIElement uielement;
			IContentHost contentHost;
			this.GetUIParentOrICH(out uielement, out contentHost);
			if (uielement != null)
			{
				uielement.OnChildDesiredSizeChanged(this);
				return;
			}
			if (contentHost != null)
			{
				contentHost.OnChildDesiredSizeChanged(this);
			}
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00028B7C File Offset: 0x00027F7C
		private void ensureClip(Size layoutSlotSize)
		{
			Geometry geometry = this.GetLayoutClip(layoutSlotSize);
			if (this.Clip != null)
			{
				if (geometry == null)
				{
					geometry = this.Clip;
				}
				else
				{
					CombinedGeometry combinedGeometry = new CombinedGeometry(GeometryCombineMode.Intersect, geometry, this.Clip);
					geometry = combinedGeometry;
				}
			}
			base.ChangeVisualClip(geometry, true);
		}

		/// <summary>Implementa <see cref="M:System.Windows.Media.Visual.HitTestCore(System.Windows.Media.PointHitTestParameters)" /> para fornecer o comportamento do teste de clique do elemento base (retornando <see cref="T:System.Windows.Media.HitTestResult" />).</summary>
		/// <param name="hitTestParameters">Descreve o teste de clique a ser executado, incluindo o ponto de clique inicial.</param>
		/// <returns>Resultados do teste, incluindo o ponto avaliado.</returns>
		// Token: 0x06000967 RID: 2407 RVA: 0x00028BC0 File Offset: 0x00027FC0
		protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
		{
			if (this._drawingContent != null && this._drawingContent.HitTestPoint(hitTestParameters.HitPoint))
			{
				return new PointHitTestResult(this, hitTestParameters.HitPoint);
			}
			return null;
		}

		/// <summary>Implementa <see cref="M:System.Windows.Media.Visual.HitTestCore(System.Windows.Media.GeometryHitTestParameters)" /> para fornecer o comportamento do teste de clique do elemento base (retornando <see cref="T:System.Windows.Media.GeometryHitTestResult" />).</summary>
		/// <param name="hitTestParameters">Descreve o teste de clique a ser executado, incluindo o ponto de clique inicial.</param>
		/// <returns>Resultados do teste, incluindo a geometria avaliada.</returns>
		// Token: 0x06000968 RID: 2408 RVA: 0x00028BF8 File Offset: 0x00027FF8
		protected override GeometryHitTestResult HitTestCore(GeometryHitTestParameters hitTestParameters)
		{
			if (this._drawingContent != null && this.GetHitTestBounds().IntersectsWith(hitTestParameters.Bounds))
			{
				IntersectionDetail intersectionDetail = this._drawingContent.HitTestGeometry(hitTestParameters.InternalHitGeometry);
				if (intersectionDetail != IntersectionDetail.Empty)
				{
					return new GeometryHitTestResult(this, intersectionDetail);
				}
			}
			return null;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00028C44 File Offset: 0x00028044
		[FriendAccessAllowed]
		internal DrawingContext RenderOpen()
		{
			return new VisualDrawingContext(this);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00028C58 File Offset: 0x00028058
		internal override void RenderClose(IDrawingContent newContent)
		{
			IDrawingContent drawingContent = this._drawingContent;
			if (drawingContent == null && newContent == null)
			{
				return;
			}
			this._drawingContent = null;
			if (drawingContent != null)
			{
				drawingContent.PropagateChangedHandler(base.ContentsChangedHandler, false);
				base.DisconnectAttachedResource(VisualProxyFlags.IsContentConnected, drawingContent);
			}
			if (newContent != null)
			{
				newContent.PropagateChangedHandler(base.ContentsChangedHandler, true);
			}
			this._drawingContent = newContent;
			base.SetFlagsOnAllChannels(true, VisualProxyFlags.IsContentDirty);
			Visual.PropagateFlags(this, VisualFlags.IsSubtreeDirtyForPrecompute, VisualProxyFlags.IsSubtreeDirtyForRender);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00028CC0 File Offset: 0x000280C0
		[SecurityCritical]
		internal override void FreeContent(DUCE.Channel channel)
		{
			if (this._drawingContent != null && base.CheckFlagsAnd(channel, VisualProxyFlags.IsContentConnected))
			{
				DUCE.CompositionNode.SetContent(this._proxy.GetHandle(channel), DUCE.ResourceHandle.Null, channel);
				this._drawingContent.ReleaseOnChannel(channel);
				base.SetFlags(channel, false, VisualProxyFlags.IsContentConnected);
			}
			base.FreeContent(channel);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00028D1C File Offset: 0x0002811C
		internal override Rect GetContentBounds()
		{
			if (this._drawingContent != null)
			{
				Rect result = Rect.Empty;
				MediaContext mediaContext = MediaContext.From(base.Dispatcher);
				BoundsDrawingContextWalker ctx = mediaContext.AcquireBoundsDrawingContextWalker();
				result = this._drawingContent.GetContentBounds(ctx);
				mediaContext.ReleaseBoundsDrawingContextWalker(ctx);
				return result;
			}
			return Rect.Empty;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00028D68 File Offset: 0x00028168
		internal void WalkContent(DrawingContextWalker walker)
		{
			base.VerifyAPIReadOnly();
			if (this._drawingContent != null)
			{
				this._drawingContent.WalkContent(walker);
			}
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00028D90 File Offset: 0x00028190
		internal override void RenderContent(RenderContext ctx, bool isOnChannel)
		{
			DUCE.Channel channel = ctx.Channel;
			if (this._drawingContent != null)
			{
				DUCE.IResource drawingContent = this._drawingContent;
				drawingContent.AddRefOnChannel(channel);
				DUCE.CompositionNode.SetContent(this._proxy.GetHandle(channel), drawingContent.GetHandle(channel), channel);
				base.SetFlags(channel, true, VisualProxyFlags.IsContentConnected);
				return;
			}
			if (isOnChannel)
			{
				DUCE.CompositionNode.SetContent(this._proxy.GetHandle(channel), DUCE.ResourceHandle.Null, channel);
			}
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00028DFC File Offset: 0x000281FC
		internal override DrawingGroup GetDrawing()
		{
			base.VerifyAPIReadOnly();
			DrawingGroup result = null;
			if (this._drawingContent != null)
			{
				result = DrawingServices.DrawingGroupFromRenderData((RenderData)this._drawingContent);
			}
			return result;
		}

		/// <summary>Retorna uma geometria de recorte alternativa que representa a região que seria recortada se <see cref="P:System.Windows.UIElement.ClipToBounds" /> estivesse definido como <see langword="true" />.</summary>
		/// <param name="layoutSlotSize">O tamanho disponível fornecido pelo elemento.</param>
		/// <returns>A geometria de recorte em potencial.</returns>
		// Token: 0x06000970 RID: 2416 RVA: 0x00028E2C File Offset: 0x0002822C
		protected virtual Geometry GetLayoutClip(Size layoutSlotSize)
		{
			if (this.ClipToBounds)
			{
				RectangleGeometry rectangleGeometry = new RectangleGeometry(new Rect(this.RenderSize));
				rectangleGeometry.Freeze();
				return rectangleGeometry;
			}
			return null;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00028E5C File Offset: 0x0002825C
		private static void ClipToBounds_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement uielement = (UIElement)d;
			uielement.ClipToBoundsCache = (bool)e.NewValue;
			if (!uielement.NeverMeasured || !uielement.NeverArranged)
			{
				uielement.InvalidateArrange();
			}
		}

		/// <summary>Obtém ou define um valor indicando se o conteúdo deste elemento (ou conteúdo proveniente dos filhos deste elemento) deve ser recortado para caber dentro do espaço do elemento que o contém.   É uma propriedade de dependência.</summary>
		/// <returns>
		///   <see langword="true" /> se for necessário recortar o conteúdo; caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00028E98 File Offset: 0x00028298
		// (set) Token: 0x06000973 RID: 2419 RVA: 0x00028EAC File Offset: 0x000282AC
		public bool ClipToBounds
		{
			get
			{
				return this.ClipToBoundsCache;
			}
			set
			{
				base.SetValue(UIElement.ClipToBoundsProperty, BooleanBoxes.Box(value));
			}
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00028ECC File Offset: 0x000282CC
		private static void Clip_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement uielement = (UIElement)d;
			if (!uielement.NeverMeasured || !uielement.NeverArranged)
			{
				uielement.InvalidateArrange();
			}
		}

		/// <summary>Obtém ou define a geometria usada para definir o contorno do conteúdo de um elemento.  É uma propriedade de dependência.</summary>
		/// <returns>A geometria a ser usada para o dimensionamento de área de recorte. O padrão é um <see cref="T:System.Windows.Media.Geometry" /> nulo.</returns>
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x00028EF8 File Offset: 0x000282F8
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x00028F18 File Offset: 0x00028318
		public Geometry Clip
		{
			get
			{
				return (Geometry)base.GetValue(UIElement.ClipProperty);
			}
			set
			{
				base.SetValue(UIElement.ClipProperty, value);
			}
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00028F34 File Offset: 0x00028334
		private static void SnapsToDevicePixels_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement uielement = (UIElement)d;
			uielement.SnapsToDevicePixelsCache = (bool)e.NewValue;
			if (!uielement.NeverMeasured || !uielement.NeverArranged)
			{
				uielement.InvalidateArrange();
			}
		}

		/// <summary>Obtém ou define um valor que determina se a renderização para esse elemento deve usar configurações de pixel específica do dispositivo durante a renderização.  É uma propriedade de dependência.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento precisar renderizar de acordo com os pixels do dispositivo; caso contrário, <see langword="false" />. O padrão como declarado em <see cref="T:System.Windows.UIElement" /> é <see langword="false" />.</returns>
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00028F70 File Offset: 0x00028370
		// (set) Token: 0x06000979 RID: 2425 RVA: 0x00028F84 File Offset: 0x00028384
		public bool SnapsToDevicePixels
		{
			get
			{
				return this.SnapsToDevicePixelsCache;
			}
			set
			{
				base.SetValue(UIElement.SnapsToDevicePixelsProperty, value);
			}
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.UIElement" /> tem foco.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.UIElement" /> tiver o foco; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00028FA0 File Offset: 0x000283A0
		protected internal virtual bool HasEffectiveKeyboardFocus
		{
			get
			{
				return this.IsKeyboardFocused;
			}
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00028FB4 File Offset: 0x000283B4
		internal void InvokeAccessKey(AccessKeyEventArgs e)
		{
			this.OnAccessKey(e);
		}

		/// <summary>Ocorre quando este elemento tem foco lógico.</summary>
		// Token: 0x140000B0 RID: 176
		// (add) Token: 0x0600097C RID: 2428 RVA: 0x00028FC8 File Offset: 0x000283C8
		// (remove) Token: 0x0600097D RID: 2429 RVA: 0x00028FE4 File Offset: 0x000283E4
		public event RoutedEventHandler GotFocus
		{
			add
			{
				this.AddHandler(UIElement.GotFocusEvent, value);
			}
			remove
			{
				this.RemoveHandler(UIElement.GotFocusEvent, value);
			}
		}

		/// <summary>Ocorre quando este elemento perde o foco lógico.</summary>
		// Token: 0x140000B1 RID: 177
		// (add) Token: 0x0600097E RID: 2430 RVA: 0x00029000 File Offset: 0x00028400
		// (remove) Token: 0x0600097F RID: 2431 RVA: 0x0002901C File Offset: 0x0002841C
		public event RoutedEventHandler LostFocus
		{
			add
			{
				this.AddHandler(UIElement.LostFocusEvent, value);
			}
			remove
			{
				this.RemoveHandler(UIElement.LostFocusEvent, value);
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00029038 File Offset: 0x00028438
		private static void IsFocused_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement uielement = (UIElement)d;
			if ((bool)e.NewValue)
			{
				uielement.OnGotFocus(new RoutedEventArgs(UIElement.GotFocusEvent, uielement));
				return;
			}
			uielement.OnLostFocus(new RoutedEventArgs(UIElement.LostFocusEvent, uielement));
		}

		/// <summary>Gera o evento roteado <see cref="E:System.Windows.UIElement.GotFocus" /> usando os dados de evento fornecidos.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.RoutedEventArgs" /> que contém dados do evento. Esses dados de evento devem conter o identificador para o evento <see cref="E:System.Windows.UIElement.GotFocus" />.</param>
		// Token: 0x06000981 RID: 2433 RVA: 0x00029080 File Offset: 0x00028480
		protected virtual void OnGotFocus(RoutedEventArgs e)
		{
			this.RaiseEvent(e);
		}

		/// <summary>Gera o evento roteado <see cref="E:System.Windows.UIElement.LostFocus" /> usando os dados de evento fornecidos.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.RoutedEventArgs" /> que contém dados do evento. Esses dados de evento devem conter o identificador para o evento <see cref="E:System.Windows.UIElement.LostFocus" />.</param>
		// Token: 0x06000982 RID: 2434 RVA: 0x00029094 File Offset: 0x00028494
		protected virtual void OnLostFocus(RoutedEventArgs e)
		{
			this.RaiseEvent(e);
		}

		/// <summary>Obtém um valor que determina se esse elemento tem foco lógico.  É uma propriedade de dependência.</summary>
		/// <returns>
		///   <see langword="true" /> se esse elemento tiver o foco lógico; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x000290A8 File Offset: 0x000284A8
		public bool IsFocused
		{
			get
			{
				return (bool)base.GetValue(UIElement.IsFocusedProperty);
			}
		}

		/// <summary>Obtém ou define um valor que indica se esse elemento está habilitado no UI (interface do usuário).  É uma propriedade de dependência.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento estiver habilitado; caso contrário, <see langword="false" />. O valor padrão é <see langword="true" />.</returns>
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x000290C8 File Offset: 0x000284C8
		// (set) Token: 0x06000985 RID: 2437 RVA: 0x000290E8 File Offset: 0x000284E8
		public bool IsEnabled
		{
			get
			{
				return (bool)base.GetValue(UIElement.IsEnabledProperty);
			}
			set
			{
				base.SetValue(UIElement.IsEnabledProperty, BooleanBoxes.Box(value));
			}
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.UIElement.IsEnabled" /> neste elemento é alterado.</summary>
		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x06000986 RID: 2438 RVA: 0x00029108 File Offset: 0x00028508
		// (remove) Token: 0x06000987 RID: 2439 RVA: 0x00029124 File Offset: 0x00028524
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

		/// <summary>Obtém um valor que se torna o valor retornado de <see cref="P:System.Windows.UIElement.IsEnabled" /> em classes derivadas.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento estiver habilitado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x00029140 File Offset: 0x00028540
		protected virtual bool IsEnabledCore
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00029150 File Offset: 0x00028550
		private static object CoerceIsEnabled(DependencyObject d, object value)
		{
			UIElement uielement = (UIElement)d;
			if (!(bool)value)
			{
				return BooleanBoxes.FalseBox;
			}
			DependencyObject dependencyObject = uielement.GetUIParentCore() as ContentElement;
			if (dependencyObject == null)
			{
				dependencyObject = InputElement.GetContainingUIElement(uielement._parent);
			}
			if (dependencyObject == null || (bool)dependencyObject.GetValue(UIElement.IsEnabledProperty))
			{
				return BooleanBoxes.Box(uielement.IsEnabledCore);
			}
			return BooleanBoxes.FalseBox;
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x000291B4 File Offset: 0x000285B4
		private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement uielement = (UIElement)d;
			uielement.RaiseDependencyPropertyChanged(UIElement.IsEnabledChangedKey, e);
			uielement.InvalidateForceInheritPropertyOnChildren(e.Property);
			InputManager.SafeCurrentNotifyHitTestInvalidated();
			AutomationPeer automationPeer = uielement.GetAutomationPeer();
			if (automationPeer != null)
			{
				automationPeer.InvalidatePeer();
			}
		}

		/// <summary>Obtém ou define um valor que declara se este elemento tem possibilidade de ser retornado como um resultado de teste de clique de alguma parte de seu conteúdo renderizado. É uma propriedade de dependência.</summary>
		/// <returns>
		///   <see langword="true" /> se esse elemento puder ser retornado como um resultado do teste de clique de, pelo menos, um ponto; caso contrário, <see langword="false" />. O valor padrão é <see langword="true" />.</returns>
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x000291F8 File Offset: 0x000285F8
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x00029218 File Offset: 0x00028618
		public bool IsHitTestVisible
		{
			get
			{
				return (bool)base.GetValue(UIElement.IsHitTestVisibleProperty);
			}
			set
			{
				base.SetValue(UIElement.IsHitTestVisibleProperty, BooleanBoxes.Box(value));
			}
		}

		/// <summary>Ocorre quando o valor da propriedade de dependência <see cref="P:System.Windows.UIElement.IsHitTestVisible" /> é alterado neste elemento.</summary>
		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x0600098D RID: 2445 RVA: 0x00029238 File Offset: 0x00028638
		// (remove) Token: 0x0600098E RID: 2446 RVA: 0x00029254 File Offset: 0x00028654
		public event DependencyPropertyChangedEventHandler IsHitTestVisibleChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.IsHitTestVisibleChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.IsHitTestVisibleChangedKey, value);
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00029270 File Offset: 0x00028670
		private static object CoerceIsHitTestVisible(DependencyObject d, object value)
		{
			UIElement uielement = (UIElement)d;
			if (!(bool)value)
			{
				return BooleanBoxes.FalseBox;
			}
			DependencyObject containingUIElement = InputElement.GetContainingUIElement(uielement._parent);
			if (containingUIElement == null || UIElementHelper.IsHitTestVisible(containingUIElement))
			{
				return BooleanBoxes.TrueBox;
			}
			return BooleanBoxes.FalseBox;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x000292B4 File Offset: 0x000286B4
		private static void OnIsHitTestVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement uielement = (UIElement)d;
			uielement.RaiseDependencyPropertyChanged(UIElement.IsHitTestVisibleChangedKey, e);
			uielement.InvalidateForceInheritPropertyOnChildren(e.Property);
			InputManager.SafeCurrentNotifyHitTestInvalidated();
		}

		/// <summary>Obtém um valor que indica se esse elemento está visível no UI (interface do usuário).  É uma propriedade de dependência.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento estiver visível; caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x000292E8 File Offset: 0x000286E8
		public bool IsVisible
		{
			get
			{
				return this.ReadFlag(CoreFlags.IsVisibleCache);
			}
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00029300 File Offset: 0x00028700
		private static object GetIsVisible(DependencyObject d, out BaseValueSourceInternal source)
		{
			source = BaseValueSourceInternal.Local;
			if (!((UIElement)d).IsVisible)
			{
				return BooleanBoxes.FalseBox;
			}
			return BooleanBoxes.TrueBox;
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.UIElement.IsVisible" /> é alterado neste elemento.</summary>
		// Token: 0x140000B4 RID: 180
		// (add) Token: 0x06000993 RID: 2451 RVA: 0x0002932C File Offset: 0x0002872C
		// (remove) Token: 0x06000994 RID: 2452 RVA: 0x00029348 File Offset: 0x00028748
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

		// Token: 0x06000995 RID: 2453 RVA: 0x00029364 File Offset: 0x00028764
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal void UpdateIsVisibleCache()
		{
			bool flag = this.Visibility == Visibility.Visible;
			if (flag)
			{
				bool flag2 = false;
				DependencyObject containingUIElement = InputElement.GetContainingUIElement(this._parent);
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
				base.NotifyPropertyChange(new DependencyPropertyChangedEventArgs(UIElement.IsVisibleProperty, UIElement._isVisibleMetadata, BooleanBoxes.Box(!flag), BooleanBoxes.Box(flag)));
			}
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x000293E4 File Offset: 0x000287E4
		private static void OnIsVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement uielement = (UIElement)d;
			uielement.RaiseDependencyPropertyChanged(UIElement.IsVisibleChangedKey, e);
			uielement.InvalidateForceInheritPropertyOnChildren(e.Property);
			InputManager.SafeCurrentNotifyHitTestInvalidated();
		}

		/// <summary>Obtém ou define um valor que indica se um elemento pode receber foco.  É uma propriedade de dependência.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento for focalizável; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x00029418 File Offset: 0x00028818
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x00029438 File Offset: 0x00028838
		public bool Focusable
		{
			get
			{
				return (bool)base.GetValue(UIElement.FocusableProperty);
			}
			set
			{
				base.SetValue(UIElement.FocusableProperty, BooleanBoxes.Box(value));
			}
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.UIElement.Focusable" /> muda.</summary>
		// Token: 0x140000B5 RID: 181
		// (add) Token: 0x06000999 RID: 2457 RVA: 0x00029458 File Offset: 0x00028858
		// (remove) Token: 0x0600099A RID: 2458 RVA: 0x00029474 File Offset: 0x00028874
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

		// Token: 0x0600099B RID: 2459 RVA: 0x00029490 File Offset: 0x00028890
		private static void OnFocusableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement uielement = (UIElement)d;
			uielement.RaiseDependencyPropertyChanged(UIElement.FocusableChangedKey, e);
		}

		/// <summary>Retorna implementações de <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> específicas à classe para a infra-estrutura de Windows Presentation Foundation (WPF).</summary>
		/// <returns>A implementação de <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> específica ao tipo.</returns>
		// Token: 0x0600099C RID: 2460 RVA: 0x000294B0 File Offset: 0x000288B0
		protected virtual AutomationPeer OnCreateAutomationPeer()
		{
			if (!AccessibilitySwitches.ItemsControlDoesNotSupportAutomation)
			{
				UIElement.AutomationNotSupportedByDefaultField.SetValue(this, true);
			}
			return null;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x000294D4 File Offset: 0x000288D4
		internal virtual AutomationPeer OnCreateAutomationPeerInternal()
		{
			return null;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x000294E4 File Offset: 0x000288E4
		internal AutomationPeer CreateAutomationPeer()
		{
			base.VerifyAccess();
			AutomationPeer automationPeer;
			if (this.HasAutomationPeer)
			{
				automationPeer = UIElement.AutomationPeerField.GetValue(this);
			}
			else
			{
				if (!AccessibilitySwitches.ItemsControlDoesNotSupportAutomation)
				{
					UIElement.AutomationNotSupportedByDefaultField.ClearValue(this);
					automationPeer = this.OnCreateAutomationPeer();
					if (automationPeer == null && !UIElement.AutomationNotSupportedByDefaultField.GetValue(this))
					{
						automationPeer = this.OnCreateAutomationPeerInternal();
					}
				}
				else
				{
					automationPeer = this.OnCreateAutomationPeer();
				}
				if (automationPeer != null)
				{
					UIElement.AutomationPeerField.SetValue(this, automationPeer);
					this.HasAutomationPeer = true;
				}
			}
			return automationPeer;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00029560 File Offset: 0x00028960
		internal AutomationPeer GetAutomationPeer()
		{
			base.VerifyAccess();
			if (this.HasAutomationPeer)
			{
				return UIElement.AutomationPeerField.GetValue(this);
			}
			return null;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00029588 File Offset: 0x00028988
		internal AutomationPeer CreateGenericRootAutomationPeer()
		{
			base.VerifyAccess();
			AutomationPeer automationPeer;
			if (this.HasAutomationPeer)
			{
				automationPeer = UIElement.AutomationPeerField.GetValue(this);
			}
			else
			{
				automationPeer = new GenericRootAutomationPeer(this);
				UIElement.AutomationPeerField.SetValue(this, automationPeer);
				this.HasAutomationPeer = true;
			}
			return automationPeer;
		}

		/// <summary>Obtém um valor que identifica esse objeto de forma exclusiva.</summary>
		/// <returns>O identificador exclusivo desse elemento.</returns>
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x000295D0 File Offset: 0x000289D0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Obsolete("PersistId is an obsolete property and may be removed in a future release.  The value of this property is not defined.")]
		public int PersistId
		{
			get
			{
				return this._persistId;
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x000295E4 File Offset: 0x000289E4
		[FriendAccessAllowed]
		internal void SetPersistId(int value)
		{
			this._persistId = value;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x000295F8 File Offset: 0x000289F8
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

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00029628 File Offset: 0x00028A28
		internal Rect PreviousArrangeRect
		{
			[FriendAccessAllowed]
			get
			{
				return this._finalRect;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0002963C File Offset: 0x00028A3C
		// (set) Token: 0x060009A6 RID: 2470 RVA: 0x00029668 File Offset: 0x00028A68
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

		// Token: 0x060009A7 RID: 2471 RVA: 0x000296D4 File Offset: 0x00028AD4
		internal static void SynchronizeForceInheritProperties(UIElement uiElement, ContentElement contentElement, UIElement3D uiElement3D, DependencyObject parent)
		{
			if (uiElement != null || uiElement3D != null)
			{
				if (!(bool)parent.GetValue(UIElement.IsEnabledProperty))
				{
					if (uiElement != null)
					{
						uiElement.CoerceValue(UIElement.IsEnabledProperty);
					}
					else
					{
						uiElement3D.CoerceValue(UIElement.IsEnabledProperty);
					}
				}
				if (!(bool)parent.GetValue(UIElement.IsHitTestVisibleProperty))
				{
					if (uiElement != null)
					{
						uiElement.CoerceValue(UIElement.IsHitTestVisibleProperty);
					}
					else
					{
						uiElement3D.CoerceValue(UIElement.IsHitTestVisibleProperty);
					}
				}
				bool flag = (bool)parent.GetValue(UIElement.IsVisibleProperty);
				if (flag)
				{
					if (uiElement != null)
					{
						uiElement.UpdateIsVisibleCache();
						return;
					}
					uiElement3D.UpdateIsVisibleCache();
					return;
				}
			}
			else if (contentElement != null && !(bool)parent.GetValue(UIElement.IsEnabledProperty))
			{
				contentElement.CoerceValue(UIElement.IsEnabledProperty);
			}
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00029790 File Offset: 0x00028B90
		internal static void InvalidateForceInheritPropertyOnChildren(Visual v, DependencyProperty property)
		{
			int internalVisual2DOr3DChildrenCount = v.InternalVisual2DOr3DChildrenCount;
			for (int i = 0; i < internalVisual2DOr3DChildrenCount; i++)
			{
				DependencyObject dependencyObject = v.InternalGet2DOr3DVisualChild(i);
				Visual visual = dependencyObject as Visual;
				if (visual != null)
				{
					UIElement uielement = visual as UIElement;
					if (uielement != null)
					{
						if (property == UIElement.IsVisibleProperty)
						{
							uielement.UpdateIsVisibleCache();
						}
						else
						{
							uielement.CoerceValue(property);
						}
					}
					else
					{
						visual.InvalidateForceInheritPropertyOnChildren(property);
					}
				}
				else
				{
					Visual3D visual3D = dependencyObject as Visual3D;
					if (visual3D != null)
					{
						UIElement3D uielement3D = visual3D as UIElement3D;
						if (uielement3D != null)
						{
							if (property == UIElement.IsVisibleProperty)
							{
								uielement3D.UpdateIsVisibleCache();
							}
							else
							{
								uielement3D.CoerceValue(property);
							}
						}
						else
						{
							visual3D.InvalidateForceInheritPropertyOnChildren(property);
						}
					}
				}
			}
		}

		/// <summary>Obtém ou define um valor que indica se os eventos de manipulação estão habilitados neste <see cref="T:System.Windows.UIElement" />.</summary>
		/// <returns>
		///   <see langword="true" /> se os eventos de manipulação estiverem habilitados neste <see cref="T:System.Windows.UIElement" />; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x00029834 File Offset: 0x00028C34
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x00029854 File Offset: 0x00028C54
		[CustomCategory("Touch_Category")]
		public bool IsManipulationEnabled
		{
			get
			{
				return (bool)base.GetValue(UIElement.IsManipulationEnabledProperty);
			}
			set
			{
				base.SetValue(UIElement.IsManipulationEnabledProperty, value);
			}
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00029870 File Offset: 0x00028C70
		private static void OnIsManipulationEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if ((bool)e.NewValue)
			{
				((UIElement)d).CoerceStylusProperties();
				return;
			}
			Manipulation.TryCompleteManipulation((UIElement)d);
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x000298A4 File Offset: 0x00028CA4
		private void CoerceStylusProperties()
		{
			if (UIElement.IsDefaultValue(this, Stylus.IsFlicksEnabledProperty))
			{
				base.SetCurrentValueInternal(Stylus.IsFlicksEnabledProperty, BooleanBoxes.FalseBox);
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x000298D0 File Offset: 0x00028CD0
		private static bool IsDefaultValue(DependencyObject dependencyObject, DependencyProperty dependencyProperty)
		{
			bool flag;
			bool flag2;
			bool flag3;
			bool flag4;
			bool flag5;
			BaseValueSourceInternal valueSource = dependencyObject.GetValueSource(dependencyProperty, null, out flag, out flag2, out flag3, out flag4, out flag5);
			return valueSource == BaseValueSourceInternal.Default && !flag2 && !flag3 && !flag4;
		}

		/// <summary>Ocorre quando o processador de manipulação é criado.</summary>
		// Token: 0x140000B6 RID: 182
		// (add) Token: 0x060009AE RID: 2478 RVA: 0x00029900 File Offset: 0x00028D00
		// (remove) Token: 0x060009AF RID: 2479 RVA: 0x0002991C File Offset: 0x00028D1C
		[CustomCategory("Touch_Category")]
		public event EventHandler<ManipulationStartingEventArgs> ManipulationStarting
		{
			add
			{
				this.AddHandler(UIElement.ManipulationStartingEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.ManipulationStartingEvent, value);
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00029938 File Offset: 0x00028D38
		private static void OnManipulationStartingThunk(object sender, ManipulationStartingEventArgs e)
		{
			((UIElement)sender).OnManipulationStarting(e);
		}

		/// <summary>Fornece tratamento de classes para o evento <see cref="E:System.Windows.UIElement.ManipulationStarting" /> roteado que ocorre quando o processador de manipulação é criado pela primeira vez.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.ManipulationStartingEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060009B1 RID: 2481 RVA: 0x00029954 File Offset: 0x00028D54
		protected virtual void OnManipulationStarting(ManipulationStartingEventArgs e)
		{
		}

		/// <summary>Ocorre quando um dispositivo de entrada começa uma manipulação no objeto <see cref="T:System.Windows.UIElement" />.</summary>
		// Token: 0x140000B7 RID: 183
		// (add) Token: 0x060009B2 RID: 2482 RVA: 0x00029964 File Offset: 0x00028D64
		// (remove) Token: 0x060009B3 RID: 2483 RVA: 0x00029980 File Offset: 0x00028D80
		[CustomCategory("Touch_Category")]
		public event EventHandler<ManipulationStartedEventArgs> ManipulationStarted
		{
			add
			{
				this.AddHandler(UIElement.ManipulationStartedEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.ManipulationStartedEvent, value);
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0002999C File Offset: 0x00028D9C
		private static void OnManipulationStartedThunk(object sender, ManipulationStartedEventArgs e)
		{
			((UIElement)sender).OnManipulationStarted(e);
		}

		/// <summary>Chamado quando o evento <see cref="E:System.Windows.UIElement.ManipulationStarted" /> ocorre.</summary>
		/// <param name="e">Os dados do evento.</param>
		// Token: 0x060009B5 RID: 2485 RVA: 0x000299B8 File Offset: 0x00028DB8
		protected virtual void OnManipulationStarted(ManipulationStartedEventArgs e)
		{
		}

		/// <summary>Ocorre quando o dispositivo de entrada muda de posição durante uma manipulação.</summary>
		// Token: 0x140000B8 RID: 184
		// (add) Token: 0x060009B6 RID: 2486 RVA: 0x000299C8 File Offset: 0x00028DC8
		// (remove) Token: 0x060009B7 RID: 2487 RVA: 0x000299E4 File Offset: 0x00028DE4
		[CustomCategory("Touch_Category")]
		public event EventHandler<ManipulationDeltaEventArgs> ManipulationDelta
		{
			add
			{
				this.AddHandler(UIElement.ManipulationDeltaEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.ManipulationDeltaEvent, value);
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00029A00 File Offset: 0x00028E00
		private static void OnManipulationDeltaThunk(object sender, ManipulationDeltaEventArgs e)
		{
			((UIElement)sender).OnManipulationDelta(e);
		}

		/// <summary>Chamado quando o evento <see cref="E:System.Windows.UIElement.ManipulationDelta" /> ocorre.</summary>
		/// <param name="e">Os dados do evento.</param>
		// Token: 0x060009B9 RID: 2489 RVA: 0x00029A1C File Offset: 0x00028E1C
		protected virtual void OnManipulationDelta(ManipulationDeltaEventArgs e)
		{
		}

		/// <summary>Ocorre quando o dispositivo de entrada perde o contato com o objeto <see cref="T:System.Windows.UIElement" /> durante uma manipulação e a inércia começa.</summary>
		// Token: 0x140000B9 RID: 185
		// (add) Token: 0x060009BA RID: 2490 RVA: 0x00029A2C File Offset: 0x00028E2C
		// (remove) Token: 0x060009BB RID: 2491 RVA: 0x00029A48 File Offset: 0x00028E48
		[CustomCategory("Touch_Category")]
		public event EventHandler<ManipulationInertiaStartingEventArgs> ManipulationInertiaStarting
		{
			add
			{
				this.AddHandler(UIElement.ManipulationInertiaStartingEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.ManipulationInertiaStartingEvent, value);
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00029A64 File Offset: 0x00028E64
		private static void OnManipulationInertiaStartingThunk(object sender, ManipulationInertiaStartingEventArgs e)
		{
			((UIElement)sender).OnManipulationInertiaStarting(e);
		}

		/// <summary>Chamado quando o evento <see cref="E:System.Windows.UIElement.ManipulationInertiaStarting" /> ocorre.</summary>
		/// <param name="e">Os dados do evento.</param>
		// Token: 0x060009BD RID: 2493 RVA: 0x00029A80 File Offset: 0x00028E80
		protected virtual void OnManipulationInertiaStarting(ManipulationInertiaStartingEventArgs e)
		{
		}

		/// <summary>Ocorre quando a manipulação atinge um limite.</summary>
		// Token: 0x140000BA RID: 186
		// (add) Token: 0x060009BE RID: 2494 RVA: 0x00029A90 File Offset: 0x00028E90
		// (remove) Token: 0x060009BF RID: 2495 RVA: 0x00029AAC File Offset: 0x00028EAC
		[CustomCategory("Touch_Category")]
		public event EventHandler<ManipulationBoundaryFeedbackEventArgs> ManipulationBoundaryFeedback
		{
			add
			{
				this.AddHandler(UIElement.ManipulationBoundaryFeedbackEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.ManipulationBoundaryFeedbackEvent, value);
			}
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00029AC8 File Offset: 0x00028EC8
		private static void OnManipulationBoundaryFeedbackThunk(object sender, ManipulationBoundaryFeedbackEventArgs e)
		{
			((UIElement)sender).OnManipulationBoundaryFeedback(e);
		}

		/// <summary>Chamado quando o evento <see cref="E:System.Windows.UIElement.ManipulationBoundaryFeedback" /> ocorre.</summary>
		/// <param name="e">Os dados do evento.</param>
		// Token: 0x060009C1 RID: 2497 RVA: 0x00029AE4 File Offset: 0x00028EE4
		protected virtual void OnManipulationBoundaryFeedback(ManipulationBoundaryFeedbackEventArgs e)
		{
		}

		/// <summary>Ocorre quando uma manipulação e inércia no objeto <see cref="T:System.Windows.UIElement" /> é concluída.</summary>
		// Token: 0x140000BB RID: 187
		// (add) Token: 0x060009C2 RID: 2498 RVA: 0x00029AF4 File Offset: 0x00028EF4
		// (remove) Token: 0x060009C3 RID: 2499 RVA: 0x00029B10 File Offset: 0x00028F10
		[CustomCategory("Touch_Category")]
		public event EventHandler<ManipulationCompletedEventArgs> ManipulationCompleted
		{
			add
			{
				this.AddHandler(UIElement.ManipulationCompletedEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.ManipulationCompletedEvent, value);
			}
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00029B2C File Offset: 0x00028F2C
		private static void OnManipulationCompletedThunk(object sender, ManipulationCompletedEventArgs e)
		{
			((UIElement)sender).OnManipulationCompleted(e);
		}

		/// <summary>Chamado quando o evento <see cref="E:System.Windows.UIElement.ManipulationCompleted" /> ocorre.</summary>
		/// <param name="e">Os dados do evento.</param>
		// Token: 0x060009C5 RID: 2501 RVA: 0x00029B48 File Offset: 0x00028F48
		protected virtual void OnManipulationCompleted(ManipulationCompletedEventArgs e)
		{
		}

		/// <summary>Obtém um valor que indica se pelo menos um toque for pressionado sobre esse elemento ou elementos filho na sua árvore visual.</summary>
		/// <returns>
		///   <see langword="true" /> se pelo menos um toque for pressionado sobre esse elemento ou elementos filho na sua árvore visual; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00029B58 File Offset: 0x00028F58
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
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00029B70 File Offset: 0x00028F70
		public bool AreAnyTouchesDirectlyOver
		{
			get
			{
				return (bool)base.GetValue(UIElement.AreAnyTouchesDirectlyOverProperty);
			}
		}

		/// <summary>Obtém um valor que indica se ao menos um toque é capturado nesse elemento ou elementos filho na sua árvore visual.</summary>
		/// <returns>
		///   <see langword="true" /> Se pelo menos um toque é capturado para esse elemento ou elementos filho na árvore visual; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x00029B90 File Offset: 0x00028F90
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
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x00029BA8 File Offset: 0x00028FA8
		public bool AreAnyTouchesCaptured
		{
			get
			{
				return (bool)base.GetValue(UIElement.AreAnyTouchesCapturedProperty);
			}
		}

		/// <summary>Tenta forçar a captura de um toque para esse elemento.</summary>
		/// <param name="touchDevice">O dispositivo a ser capturado.</param>
		/// <returns>
		///   <see langword="true" /> se o toque especificado for capturado para esse elemento; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="touchDevice" /> é <see langword="null" />.</exception>
		// Token: 0x060009CA RID: 2506 RVA: 0x00029BC8 File Offset: 0x00028FC8
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
		// Token: 0x060009CB RID: 2507 RVA: 0x00029BEC File Offset: 0x00028FEC
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
		// Token: 0x060009CC RID: 2508 RVA: 0x00029C1C File Offset: 0x0002901C
		public void ReleaseAllTouchCaptures()
		{
			TouchDevice.ReleaseAllCaptures(this);
		}

		/// <summary>Obtém todos os dispositivos de toque capturados para esse elemento.</summary>
		/// <returns>Uma enumeração de objetos <see cref="T:System.Windows.Input.TouchDevice" /> capturados para esse elemento.</returns>
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x00029C30 File Offset: 0x00029030
		public IEnumerable<TouchDevice> TouchesCaptured
		{
			get
			{
				return TouchDevice.GetCapturedTouches(this, false);
			}
		}

		/// <summary>Obtém todos os dispositivos de toque que são capturados para esse elemento ou os elementos filho na árvore visual.</summary>
		/// <returns>Uma enumeração de objetos <see cref="T:System.Windows.Input.TouchDevice" /> que são capturados para esse elemento ou elementos filho na árvore visual.</returns>
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x00029C44 File Offset: 0x00029044
		public IEnumerable<TouchDevice> TouchesCapturedWithin
		{
			get
			{
				return TouchDevice.GetCapturedTouches(this, true);
			}
		}

		/// <summary>Obtém todos os dispositivos de toque que estão sobre esse elemento ou sobre os elementos filho na árvore visual.</summary>
		/// <returns>Uma enumeração de objetos <see cref="T:System.Windows.Input.TouchDevice" /> que estão acima desse elemento ou dos elementos filho na árvore visual.</returns>
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x00029C58 File Offset: 0x00029058
		public IEnumerable<TouchDevice> TouchesOver
		{
			get
			{
				return TouchDevice.GetTouchesOver(this, true);
			}
		}

		/// <summary>Obtém todos os dispositivos de toque nesse elemento.</summary>
		/// <returns>Uma enumeração de objetos <see cref="T:System.Windows.Input.TouchDevice" /> nesse elemento.</returns>
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00029C6C File Offset: 0x0002906C
		public IEnumerable<TouchDevice> TouchesDirectlyOver
		{
			get
			{
				return TouchDevice.GetTouchesOver(this, false);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x00029C80 File Offset: 0x00029080
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x00029CA8 File Offset: 0x000290A8
		internal UIElement PositionAndSizeOfSetController
		{
			get
			{
				UIElement result = null;
				WeakReference<UIElement> value = UIElement._positionAndSizeOfSetController.GetValue(this);
				if (value != null)
				{
					value.TryGetTarget(out result);
				}
				return result;
			}
			set
			{
				if (value != null)
				{
					UIElement._positionAndSizeOfSetController.SetValue(this, new WeakReference<UIElement>(value));
					return;
				}
				UIElement._positionAndSizeOfSetController.ClearValue(this);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00029CD8 File Offset: 0x000290D8
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x00029CF0 File Offset: 0x000290F0
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

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x00029D0C File Offset: 0x0002910C
		// (set) Token: 0x060009D6 RID: 2518 RVA: 0x00029D24 File Offset: 0x00029124
		private bool RenderingInvalidated
		{
			get
			{
				return this.ReadFlag(CoreFlags.RenderingInvalidated);
			}
			set
			{
				this.WriteFlag(CoreFlags.RenderingInvalidated, value);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x00029D40 File Offset: 0x00029140
		// (set) Token: 0x060009D8 RID: 2520 RVA: 0x00029D54 File Offset: 0x00029154
		internal bool SnapsToDevicePixelsCache
		{
			get
			{
				return this.ReadFlag(CoreFlags.SnapsToDevicePixelsCache);
			}
			set
			{
				this.WriteFlag(CoreFlags.SnapsToDevicePixelsCache, value);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x00029D6C File Offset: 0x0002916C
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x00029D80 File Offset: 0x00029180
		internal bool ClipToBoundsCache
		{
			get
			{
				return this.ReadFlag(CoreFlags.ClipToBoundsCache);
			}
			set
			{
				this.WriteFlag(CoreFlags.ClipToBoundsCache, value);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x00029D98 File Offset: 0x00029198
		// (set) Token: 0x060009DC RID: 2524 RVA: 0x00029DAC File Offset: 0x000291AC
		internal bool MeasureDirty
		{
			get
			{
				return this.ReadFlag(CoreFlags.MeasureDirty);
			}
			set
			{
				this.WriteFlag(CoreFlags.MeasureDirty, value);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x00029DC4 File Offset: 0x000291C4
		// (set) Token: 0x060009DE RID: 2526 RVA: 0x00029DD8 File Offset: 0x000291D8
		internal bool ArrangeDirty
		{
			get
			{
				return this.ReadFlag(CoreFlags.ArrangeDirty);
			}
			set
			{
				this.WriteFlag(CoreFlags.ArrangeDirty, value);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x00029DF0 File Offset: 0x000291F0
		// (set) Token: 0x060009E0 RID: 2528 RVA: 0x00029E08 File Offset: 0x00029208
		internal bool MeasureInProgress
		{
			get
			{
				return this.ReadFlag(CoreFlags.MeasureInProgress);
			}
			set
			{
				this.WriteFlag(CoreFlags.MeasureInProgress, value);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00029E20 File Offset: 0x00029220
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x00029E38 File Offset: 0x00029238
		internal bool ArrangeInProgress
		{
			get
			{
				return this.ReadFlag(CoreFlags.ArrangeInProgress);
			}
			set
			{
				this.WriteFlag(CoreFlags.ArrangeInProgress, value);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00029E50 File Offset: 0x00029250
		// (set) Token: 0x060009E4 RID: 2532 RVA: 0x00029E68 File Offset: 0x00029268
		internal bool NeverMeasured
		{
			get
			{
				return this.ReadFlag(CoreFlags.NeverMeasured);
			}
			set
			{
				this.WriteFlag(CoreFlags.NeverMeasured, value);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00029E80 File Offset: 0x00029280
		// (set) Token: 0x060009E6 RID: 2534 RVA: 0x00029E98 File Offset: 0x00029298
		internal bool NeverArranged
		{
			get
			{
				return this.ReadFlag(CoreFlags.NeverArranged);
			}
			set
			{
				this.WriteFlag(CoreFlags.NeverArranged, value);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x00029EB4 File Offset: 0x000292B4
		// (set) Token: 0x060009E8 RID: 2536 RVA: 0x00029ECC File Offset: 0x000292CC
		internal bool MeasureDuringArrange
		{
			get
			{
				return this.ReadFlag(CoreFlags.MeasureDuringArrange);
			}
			set
			{
				this.WriteFlag(CoreFlags.MeasureDuringArrange, value);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00029EE8 File Offset: 0x000292E8
		// (set) Token: 0x060009EA RID: 2538 RVA: 0x00029F00 File Offset: 0x00029300
		internal bool AreTransformsClean
		{
			get
			{
				return this.ReadFlag(CoreFlags.AreTransformsClean);
			}
			set
			{
				this.WriteFlag(CoreFlags.AreTransformsClean, value);
			}
		}

		// Token: 0x040005B7 RID: 1463
		private static readonly Type _typeofThis = typeof(UIElement);

		// Token: 0x04000603 RID: 1539
		internal static readonly DependencyPropertyKey IsMouseDirectlyOverPropertyKey;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.IsMouseDirectlyOver" />.</summary>
		// Token: 0x04000604 RID: 1540
		public static readonly DependencyProperty IsMouseDirectlyOverProperty;

		// Token: 0x04000605 RID: 1541
		internal static readonly EventPrivateKey IsMouseDirectlyOverChangedKey;

		// Token: 0x04000606 RID: 1542
		internal static readonly DependencyPropertyKey IsMouseOverPropertyKey;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.IsMouseOver" />.</summary>
		// Token: 0x04000607 RID: 1543
		public static readonly DependencyProperty IsMouseOverProperty;

		// Token: 0x04000608 RID: 1544
		internal static readonly DependencyPropertyKey IsStylusOverPropertyKey;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.IsStylusOver" />.</summary>
		// Token: 0x04000609 RID: 1545
		public static readonly DependencyProperty IsStylusOverProperty;

		// Token: 0x0400060A RID: 1546
		internal static readonly DependencyPropertyKey IsKeyboardFocusWithinPropertyKey;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.IsKeyboardFocusWithin" />.</summary>
		// Token: 0x0400060B RID: 1547
		public static readonly DependencyProperty IsKeyboardFocusWithinProperty;

		// Token: 0x0400060C RID: 1548
		internal static readonly EventPrivateKey IsKeyboardFocusWithinChangedKey;

		// Token: 0x0400060D RID: 1549
		internal static readonly DependencyPropertyKey IsMouseCapturedPropertyKey;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.IsMouseCaptured" />.</summary>
		// Token: 0x0400060E RID: 1550
		public static readonly DependencyProperty IsMouseCapturedProperty;

		// Token: 0x0400060F RID: 1551
		internal static readonly EventPrivateKey IsMouseCapturedChangedKey;

		// Token: 0x04000610 RID: 1552
		internal static readonly DependencyPropertyKey IsMouseCaptureWithinPropertyKey;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.IsMouseCaptureWithin" />.</summary>
		// Token: 0x04000611 RID: 1553
		public static readonly DependencyProperty IsMouseCaptureWithinProperty;

		// Token: 0x04000612 RID: 1554
		internal static readonly EventPrivateKey IsMouseCaptureWithinChangedKey;

		// Token: 0x04000613 RID: 1555
		internal static readonly DependencyPropertyKey IsStylusDirectlyOverPropertyKey;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.IsStylusDirectlyOver" />.</summary>
		// Token: 0x04000614 RID: 1556
		public static readonly DependencyProperty IsStylusDirectlyOverProperty;

		// Token: 0x04000615 RID: 1557
		internal static readonly EventPrivateKey IsStylusDirectlyOverChangedKey;

		// Token: 0x04000616 RID: 1558
		internal static readonly DependencyPropertyKey IsStylusCapturedPropertyKey;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.IsStylusCaptured" />.</summary>
		// Token: 0x04000617 RID: 1559
		public static readonly DependencyProperty IsStylusCapturedProperty;

		// Token: 0x04000618 RID: 1560
		internal static readonly EventPrivateKey IsStylusCapturedChangedKey;

		// Token: 0x04000619 RID: 1561
		internal static readonly DependencyPropertyKey IsStylusCaptureWithinPropertyKey;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.IsStylusCaptureWithin" />.</summary>
		// Token: 0x0400061A RID: 1562
		public static readonly DependencyProperty IsStylusCaptureWithinProperty;

		// Token: 0x0400061B RID: 1563
		internal static readonly EventPrivateKey IsStylusCaptureWithinChangedKey;

		// Token: 0x0400061C RID: 1564
		internal static readonly DependencyPropertyKey IsKeyboardFocusedPropertyKey;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.IsKeyboardFocused" />.</summary>
		// Token: 0x0400061D RID: 1565
		public static readonly DependencyProperty IsKeyboardFocusedProperty;

		// Token: 0x0400061E RID: 1566
		internal static readonly EventPrivateKey IsKeyboardFocusedChangedKey;

		// Token: 0x0400061F RID: 1567
		internal static readonly DependencyPropertyKey AreAnyTouchesDirectlyOverPropertyKey;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.AreAnyTouchesDirectlyOver" />.</summary>
		// Token: 0x04000620 RID: 1568
		public static readonly DependencyProperty AreAnyTouchesDirectlyOverProperty;

		// Token: 0x04000621 RID: 1569
		internal static readonly DependencyPropertyKey AreAnyTouchesOverPropertyKey;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.AreAnyTouchesOver" />.</summary>
		// Token: 0x04000622 RID: 1570
		public static readonly DependencyProperty AreAnyTouchesOverProperty;

		// Token: 0x04000623 RID: 1571
		internal static readonly DependencyPropertyKey AreAnyTouchesCapturedPropertyKey;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.AreAnyTouchesCaptured" />.</summary>
		// Token: 0x04000624 RID: 1572
		public static readonly DependencyProperty AreAnyTouchesCapturedProperty;

		// Token: 0x04000625 RID: 1573
		internal static readonly DependencyPropertyKey AreAnyTouchesCapturedWithinPropertyKey;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.AreAnyTouchesCapturedWithin" />.</summary>
		// Token: 0x04000626 RID: 1574
		public static readonly DependencyProperty AreAnyTouchesCapturedWithinProperty;

		// Token: 0x04000627 RID: 1575
		private CoreFlags _flags;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.AllowDrop" />.</summary>
		// Token: 0x04000628 RID: 1576
		public static readonly DependencyProperty AllowDropProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.RenderTransform" />.</summary>
		// Token: 0x04000629 RID: 1577
		[CommonDependencyProperty]
		public static readonly DependencyProperty RenderTransformProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.RenderTransformOrigin" />.</summary>
		// Token: 0x0400062A RID: 1578
		public static readonly DependencyProperty RenderTransformOriginProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.Opacity" />.</summary>
		// Token: 0x0400062B RID: 1579
		public static readonly DependencyProperty OpacityProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.OpacityMask" />.</summary>
		// Token: 0x0400062C RID: 1580
		public static readonly DependencyProperty OpacityMaskProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.BitmapEffect" />.</summary>
		// Token: 0x0400062D RID: 1581
		public static readonly DependencyProperty BitmapEffectProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.Effect" />.</summary>
		// Token: 0x0400062E RID: 1582
		public static readonly DependencyProperty EffectProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.BitmapEffectInput" />.</summary>
		// Token: 0x0400062F RID: 1583
		public static readonly DependencyProperty BitmapEffectInputProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.CacheMode" />.</summary>
		// Token: 0x04000630 RID: 1584
		public static readonly DependencyProperty CacheModeProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.Uid" />.</summary>
		// Token: 0x04000631 RID: 1585
		public static readonly DependencyProperty UidProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.Visibility" />.</summary>
		// Token: 0x04000632 RID: 1586
		[CommonDependencyProperty]
		public static readonly DependencyProperty VisibilityProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.ClipToBounds" />.</summary>
		// Token: 0x04000633 RID: 1587
		[CommonDependencyProperty]
		public static readonly DependencyProperty ClipToBoundsProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.Clip" />.</summary>
		// Token: 0x04000634 RID: 1588
		public static readonly DependencyProperty ClipProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.SnapsToDevicePixels" />.</summary>
		// Token: 0x04000635 RID: 1589
		public static readonly DependencyProperty SnapsToDevicePixelsProperty;

		// Token: 0x04000638 RID: 1592
		internal static readonly DependencyPropertyKey IsFocusedPropertyKey;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.IsFocused" />.</summary>
		// Token: 0x04000639 RID: 1593
		public static readonly DependencyProperty IsFocusedProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.IsEnabled" />.</summary>
		// Token: 0x0400063A RID: 1594
		[CommonDependencyProperty]
		public static readonly DependencyProperty IsEnabledProperty;

		// Token: 0x0400063B RID: 1595
		internal static readonly EventPrivateKey IsEnabledChangedKey;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.IsHitTestVisible" />.</summary>
		// Token: 0x0400063C RID: 1596
		public static readonly DependencyProperty IsHitTestVisibleProperty;

		// Token: 0x0400063D RID: 1597
		internal static readonly EventPrivateKey IsHitTestVisibleChangedKey;

		// Token: 0x0400063E RID: 1598
		private static PropertyMetadata _isVisibleMetadata;

		// Token: 0x0400063F RID: 1599
		internal static readonly DependencyPropertyKey IsVisiblePropertyKey;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.IsVisible" />.</summary>
		// Token: 0x04000640 RID: 1600
		public static readonly DependencyProperty IsVisibleProperty;

		// Token: 0x04000641 RID: 1601
		internal static readonly EventPrivateKey IsVisibleChangedKey;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.Focusable" />.</summary>
		// Token: 0x04000642 RID: 1602
		[CommonDependencyProperty]
		public static readonly DependencyProperty FocusableProperty;

		// Token: 0x04000643 RID: 1603
		internal static readonly EventPrivateKey FocusableChangedKey;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.UIElement.IsManipulationEnabled" />.</summary>
		// Token: 0x04000644 RID: 1604
		public static readonly DependencyProperty IsManipulationEnabledProperty;

		// Token: 0x0400064B RID: 1611
		private Rect _finalRect;

		// Token: 0x0400064C RID: 1612
		private Size _desiredSize;

		// Token: 0x0400064D RID: 1613
		private Size _previousAvailableSize;

		// Token: 0x0400064E RID: 1614
		private IDrawingContent _drawingContent;

		// Token: 0x0400064F RID: 1615
		internal ContextLayoutManager.LayoutQueue.Request MeasureRequest;

		// Token: 0x04000650 RID: 1616
		internal ContextLayoutManager.LayoutQueue.Request ArrangeRequest;

		// Token: 0x04000651 RID: 1617
		private int _persistId;

		// Token: 0x04000652 RID: 1618
		internal static List<double> DpiScaleXValues;

		// Token: 0x04000653 RID: 1619
		internal static List<double> DpiScaleYValues;

		// Token: 0x04000654 RID: 1620
		internal static object DpiLock;

		// Token: 0x04000655 RID: 1621
		private static double _dpiScaleX;

		// Token: 0x04000656 RID: 1622
		private static double _dpiScaleY;

		// Token: 0x04000657 RID: 1623
		private static bool _setDpi;

		// Token: 0x04000658 RID: 1624
		internal static readonly UncommonField<EventHandlersStore> EventHandlersStoreField;

		// Token: 0x04000659 RID: 1625
		internal static readonly UncommonField<InputBindingCollection> InputBindingCollectionField;

		// Token: 0x0400065A RID: 1626
		internal static readonly UncommonField<CommandBindingCollection> CommandBindingCollectionField;

		// Token: 0x0400065B RID: 1627
		private static readonly UncommonField<object> LayoutUpdatedListItemsField;

		// Token: 0x0400065C RID: 1628
		private static readonly UncommonField<EventHandler> LayoutUpdatedHandlersField;

		// Token: 0x0400065D RID: 1629
		private static readonly UncommonField<StylusPlugInCollection> StylusPlugInsField;

		// Token: 0x0400065E RID: 1630
		private static readonly UncommonField<AutomationPeer> AutomationPeerField;

		// Token: 0x0400065F RID: 1631
		private static readonly UncommonField<WeakReference<UIElement>> _positionAndSizeOfSetController;

		// Token: 0x04000660 RID: 1632
		private static readonly UncommonField<bool> AutomationNotSupportedByDefaultField;

		// Token: 0x04000661 RID: 1633
		internal SizeChangedInfo sizeChangedInfo;

		// Token: 0x04000662 RID: 1634
		internal static readonly FocusWithinProperty FocusWithinProperty;

		// Token: 0x04000663 RID: 1635
		internal static readonly MouseOverProperty MouseOverProperty;

		// Token: 0x04000664 RID: 1636
		internal static readonly MouseCaptureWithinProperty MouseCaptureWithinProperty;

		// Token: 0x04000665 RID: 1637
		internal static readonly StylusOverProperty StylusOverProperty;

		// Token: 0x04000666 RID: 1638
		internal static readonly StylusCaptureWithinProperty StylusCaptureWithinProperty;

		// Token: 0x04000667 RID: 1639
		internal static readonly TouchesOverProperty TouchesOverProperty;

		// Token: 0x04000668 RID: 1640
		internal static readonly TouchesCapturedWithinProperty TouchesCapturedWithinProperty;

		// Token: 0x04000669 RID: 1641
		private Size _size;

		// Token: 0x0400066A RID: 1642
		internal const int MAX_ELEMENTS_IN_ROUTE = 4096;

		// Token: 0x020007FA RID: 2042
		private class InputHitTestResult
		{
			// Token: 0x060055C7 RID: 21959 RVA: 0x00161330 File Offset: 0x00160730
			public HitTestResultBehavior InputHitTestResultCallback(HitTestResult result)
			{
				this._result = result;
				return HitTestResultBehavior.Stop;
			}

			// Token: 0x17001198 RID: 4504
			// (get) Token: 0x060055C8 RID: 21960 RVA: 0x00161348 File Offset: 0x00160748
			public DependencyObject Result
			{
				get
				{
					if (this._result == null)
					{
						return null;
					}
					return this._result.VisualHit;
				}
			}

			// Token: 0x17001199 RID: 4505
			// (get) Token: 0x060055C9 RID: 21961 RVA: 0x0016136C File Offset: 0x0016076C
			public HitTestResult HitTestResult
			{
				get
				{
					return this._result;
				}
			}

			// Token: 0x04002699 RID: 9881
			private HitTestResult _result;
		}
	}
}
