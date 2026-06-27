using System;
using System.ComponentModel;
using MS.Internal.PresentationCore;

namespace System.Windows
{
	/// <summary>Fornece métodos de utilitário relacionados a eventos que registram eventos roteados de proprietários de classe e adicionam manipuladores de classe.</summary>
	// Token: 0x020001AF RID: 431
	public static class EventManager
	{
		/// <summary>Registra um novo evento roteado com o sistema de eventos Windows Presentation Foundation (WPF).</summary>
		/// <param name="name">O nome do evento roteado. O nome deve ser exclusivo dentro do tipo de proprietário e não pode ser <see langword="null" /> ou uma cadeia de caracteres vazia.</param>
		/// <param name="routingStrategy">A estratégia de roteamento do evento como um valor da enumeração.</param>
		/// <param name="handlerType">O tipo de manipulador de eventos. Esse deve ser um tipo de delegado e não pode ser <see langword="null" />.</param>
		/// <param name="ownerType">O tipo de classe do proprietário do evento roteado. Esse não pode ser <see langword="null" />.</param>
		/// <returns>O identificador para o evento roteado registrado recentemente. Esse objeto de identificador agora pode ser armazenado como um campo estático em uma classe e, em seguida, usado como um parâmetro para métodos que anexam manipuladores ao evento. O identificador de evento roteado também é usado para outro sistema de evento APIs.</returns>
		// Token: 0x060006A9 RID: 1705 RVA: 0x0001E77C File Offset: 0x0001DB7C
		public static RoutedEvent RegisterRoutedEvent(string name, RoutingStrategy routingStrategy, Type handlerType, Type ownerType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (routingStrategy != RoutingStrategy.Tunnel && routingStrategy != RoutingStrategy.Bubble && routingStrategy != RoutingStrategy.Direct)
			{
				throw new InvalidEnumArgumentException("routingStrategy", (int)routingStrategy, typeof(RoutingStrategy));
			}
			if (handlerType == null)
			{
				throw new ArgumentNullException("handlerType");
			}
			if (ownerType == null)
			{
				throw new ArgumentNullException("ownerType");
			}
			if (GlobalEventManager.GetRoutedEventFromName(name, ownerType, false) != null)
			{
				throw new ArgumentException(SR.Get("DuplicateEventName", new object[]
				{
					name,
					ownerType
				}));
			}
			return GlobalEventManager.RegisterRoutedEvent(name, routingStrategy, handlerType, ownerType);
		}

		/// <summary>Registra um manipulador de classes para um evento roteado particular.</summary>
		/// <param name="classType">O tipo da classe que está declarando a manipulação de classe.</param>
		/// <param name="routedEvent">O identificador de evento roteado do evento a ser manipulado.</param>
		/// <param name="handler">Uma referência à implementação do manipulador de classe.</param>
		// Token: 0x060006AA RID: 1706 RVA: 0x0001E814 File Offset: 0x0001DC14
		public static void RegisterClassHandler(Type classType, RoutedEvent routedEvent, Delegate handler)
		{
			EventManager.RegisterClassHandler(classType, routedEvent, handler, false);
		}

		/// <summary>Registra um manipulador de classes para um evento roteado específico, com a opção de manipular eventos em que os dados do evento já estão marcados como manipulados.</summary>
		/// <param name="classType">O tipo da classe que está declarando a manipulação de classe.</param>
		/// <param name="routedEvent">O identificador de evento roteado do evento a ser manipulado.</param>
		/// <param name="handler">Uma referência à implementação do manipulador de classe.</param>
		/// <param name="handledEventsToo">
		///   <see langword="true" /> para invocar esse manipulador de classe, mesmo se os argumentos do evento roteado tiverem sido marcados como manipulados; <see langword="false" /> para reter o comportamento padrão de não invocar o manipulador em nenhum evento manipulado marcado.</param>
		// Token: 0x060006AB RID: 1707 RVA: 0x0001E82C File Offset: 0x0001DC2C
		public static void RegisterClassHandler(Type classType, RoutedEvent routedEvent, Delegate handler, bool handledEventsToo)
		{
			if (classType == null)
			{
				throw new ArgumentNullException("classType");
			}
			if (routedEvent == null)
			{
				throw new ArgumentNullException("routedEvent");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (!typeof(UIElement).IsAssignableFrom(classType) && !typeof(ContentElement).IsAssignableFrom(classType) && !typeof(UIElement3D).IsAssignableFrom(classType))
			{
				throw new ArgumentException(SR.Get("ClassTypeIllegal"));
			}
			if (!routedEvent.IsLegalHandler(handler))
			{
				throw new ArgumentException(SR.Get("HandlerTypeIllegal"));
			}
			GlobalEventManager.RegisterClassHandler(classType, routedEvent, handler, handledEventsToo);
		}

		/// <summary>Retorna identificadores para eventos roteados que foram registrados para o sistema de eventos.</summary>
		/// <returns>Uma matriz do tipo <see cref="T:System.Windows.RoutedEvent" /> que contém os objetos registrados.</returns>
		// Token: 0x060006AC RID: 1708 RVA: 0x0001E8D4 File Offset: 0x0001DCD4
		public static RoutedEvent[] GetRoutedEvents()
		{
			return GlobalEventManager.GetRoutedEvents();
		}

		/// <summary>Localiza todos os identificadores de eventos roteados para eventos que são registrados com o tipo de proprietário fornecido.</summary>
		/// <param name="ownerType">O tipo com o qual iniciar a pesquisa. Classes base são incluídas na pesquisa.</param>
		/// <returns>Uma matriz de identificadores de evento roteado correspondente se nenhuma correspondência for encontrada; caso contrário, <see langword="null" />.</returns>
		// Token: 0x060006AD RID: 1709 RVA: 0x0001E8E8 File Offset: 0x0001DCE8
		public static RoutedEvent[] GetRoutedEventsForOwner(Type ownerType)
		{
			if (ownerType == null)
			{
				throw new ArgumentNullException("ownerType");
			}
			return GlobalEventManager.GetRoutedEventsForOwner(ownerType);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001E910 File Offset: 0x0001DD10
		[FriendAccessAllowed]
		internal static RoutedEvent GetRoutedEventFromName(string name, Type ownerType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (ownerType == null)
			{
				throw new ArgumentNullException("ownerType");
			}
			return GlobalEventManager.GetRoutedEventFromName(name, ownerType, true);
		}
	}
}
