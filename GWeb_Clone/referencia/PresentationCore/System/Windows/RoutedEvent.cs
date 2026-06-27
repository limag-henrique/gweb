using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Markup;

namespace System.Windows
{
	/// <summary>Representa e identifica um evento roteado e declara suas características.</summary>
	// Token: 0x020001D7 RID: 471
	[TypeConverter("System.Windows.Markup.RoutedEventConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
	[ValueSerializer("System.Windows.Markup.RoutedEventValueSerializer, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
	public sealed class RoutedEvent
	{
		/// <summary>Associa o outro tipo de proprietário ao evento roteado representado por uma instância <see cref="T:System.Windows.RoutedEvent" /> e habilita o roteamento do evento e sua manipulação.</summary>
		/// <param name="ownerType">O tipo em que o evento roteado é adicionado.</param>
		/// <returns>O campo de identificador para o evento. Esse valor retornado deve ser usado para definir um campo somente leitura estático público que armazenará o identificador para a representação do evento roteado no tipo proprietário. Esse campo geralmente é definido com acesso público, pois o código do usuário deve fazer referência ao campo para anexar os manipuladores de instância para o evento encaminhado ao usar o método utilitário <see cref="M:System.Windows.UIElement.AddHandler(System.Windows.RoutedEvent,System.Delegate,System.Boolean)" />.</returns>
		// Token: 0x06000CA3 RID: 3235 RVA: 0x00030290 File Offset: 0x0002F690
		public RoutedEvent AddOwner(Type ownerType)
		{
			GlobalEventManager.AddOwner(this, ownerType);
			return this;
		}

		/// <summary>Obtém o nome de identificação do evento roteado.</summary>
		/// <returns>O nome do evento roteado.</returns>
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x000302A8 File Offset: 0x0002F6A8
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Obtém a estratégia de roteamento do evento roteado.</summary>
		/// <returns>Um dos valores de enumeração. O padrão é o padrão de enumeração, <see cref="F:System.Windows.RoutingStrategy.Bubble" />.</returns>
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x000302BC File Offset: 0x0002F6BC
		public RoutingStrategy RoutingStrategy
		{
			get
			{
				return this._routingStrategy;
			}
		}

		/// <summary>Obtém o tipo de manipulador do evento roteado.</summary>
		/// <returns>O tipo de manipulador do evento roteado.</returns>
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x000302D0 File Offset: 0x0002F6D0
		public Type HandlerType
		{
			get
			{
				return this._handlerType;
			}
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x000302E4 File Offset: 0x0002F6E4
		internal bool IsLegalHandler(Delegate handler)
		{
			Type type = handler.GetType();
			return type == this.HandlerType || type == typeof(RoutedEventHandler);
		}

		/// <summary>Obtém o tipo de proprietário registrado do evento roteado.</summary>
		/// <returns>O tipo de proprietário do evento roteado.</returns>
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00030318 File Offset: 0x0002F718
		public Type OwnerType
		{
			get
			{
				return this._ownerType;
			}
		}

		/// <summary>Retorna a representação de cadeia de caracteres deste <see cref="T:System.Windows.RoutedEvent" />.</summary>
		/// <returns>Uma representação de cadeia de caracteres para este objeto, que é idêntico ao valor retornado por <see cref="P:System.Windows.RoutedEvent.Name" />.</returns>
		// Token: 0x06000CA9 RID: 3241 RVA: 0x0003032C File Offset: 0x0002F72C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				this._ownerType.Name,
				this._name
			});
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00030368 File Offset: 0x0002F768
		internal RoutedEvent(string name, RoutingStrategy routingStrategy, Type handlerType, Type ownerType)
		{
			this._name = name;
			this._routingStrategy = routingStrategy;
			this._handlerType = handlerType;
			this._ownerType = ownerType;
			this._globalIndex = GlobalEventManager.GetNextAvailableGlobalIndex(this);
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x000303A4 File Offset: 0x0002F7A4
		internal int GlobalIndex
		{
			get
			{
				return this._globalIndex;
			}
		}

		// Token: 0x0400073E RID: 1854
		private string _name;

		// Token: 0x0400073F RID: 1855
		private RoutingStrategy _routingStrategy;

		// Token: 0x04000740 RID: 1856
		private Type _handlerType;

		// Token: 0x04000741 RID: 1857
		private Type _ownerType;

		// Token: 0x04000742 RID: 1858
		private int _globalIndex;
	}
}
