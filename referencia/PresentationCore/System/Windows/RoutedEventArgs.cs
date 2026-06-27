using System;
using System.Collections.Specialized;
using System.Security;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows
{
	/// <summary>Contém informações de estado e dados de eventos associados a um evento roteado.</summary>
	// Token: 0x020001D4 RID: 468
	public class RoutedEventArgs : EventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.RoutedEventArgs" />.</summary>
		// Token: 0x06000C82 RID: 3202 RVA: 0x0002FDC8 File Offset: 0x0002F1C8
		public RoutedEventArgs()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.RoutedEventArgs" />, usando o identificador de evento roteado fornecido.</summary>
		/// <param name="routedEvent">O identificador de evento roteado para essa instância da classe <see cref="T:System.Windows.RoutedEventArgs" />.</param>
		// Token: 0x06000C83 RID: 3203 RVA: 0x0002FDDC File Offset: 0x0002F1DC
		public RoutedEventArgs(RoutedEvent routedEvent) : this(routedEvent, null)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.RoutedEventArgs" /> usando o identificador de evento roteado fornecido e dando a possibilidade de declarar uma origem diferente para o evento.</summary>
		/// <param name="routedEvent">O identificador de evento roteado para essa instância da classe <see cref="T:System.Windows.RoutedEventArgs" />.</param>
		/// <param name="source">Uma origem alternativa será relatada quando o evento for manipulado. Isso preenche previamente a propriedade <see cref="P:System.Windows.RoutedEventArgs.Source" />.</param>
		// Token: 0x06000C84 RID: 3204 RVA: 0x0002FDF4 File Offset: 0x0002F1F4
		public RoutedEventArgs(RoutedEvent routedEvent, object source)
		{
			this._routedEvent = routedEvent;
			this._originalSource = source;
			this._source = source;
		}

		/// <summary>Obtém ou define o <see cref="P:System.Windows.RoutedEventArgs.RoutedEvent" /> associado a esta instância do <see cref="T:System.Windows.RoutedEventArgs" />.</summary>
		/// <returns>O identificador do evento que foi invocado.</returns>
		/// <exception cref="T:System.InvalidOperationException">Tentou alterar o valor <see cref="P:System.Windows.RoutedEventArgs.RoutedEvent" /> enquanto o evento estava sendo roteado.</exception>
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0002FE20 File Offset: 0x0002F220
		// (set) Token: 0x06000C86 RID: 3206 RVA: 0x0002FE34 File Offset: 0x0002F234
		public RoutedEvent RoutedEvent
		{
			get
			{
				return this._routedEvent;
			}
			set
			{
				if (this.UserInitiated && this.InvokingHandler)
				{
					throw new InvalidOperationException(SR.Get("RoutedEventCannotChangeWhileRouting"));
				}
				this._routedEvent = value;
			}
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0002FE68 File Offset: 0x0002F268
		internal void OverrideRoutedEvent(RoutedEvent newRoutedEvent)
		{
			this._routedEvent = newRoutedEvent;
		}

		/// <summary>Obtém ou define um valor que indica o estado atual de manipulação de eventos para um evento roteado enquanto ele trafega na rota.</summary>
		/// <returns>Se a configuração, definida como <see langword="true" /> se o evento deve ser marcado como tratado; caso contrário <see langword="false" />. Se for ler esse valor, <see langword="true" /> indica que um manipulador de classe ou algum manipulador de instância ao longo da rota já marcou este evento como manipulado. <see langword="false" />.indica que nenhum manipulador marcou o evento como manipulado.  
		/// O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0002FE7C File Offset: 0x0002F27C
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x0002FE98 File Offset: 0x0002F298
		public bool Handled
		{
			[SecurityCritical]
			get
			{
				return this._flags[1];
			}
			[SecurityCritical]
			set
			{
				if (this._routedEvent == null)
				{
					throw new InvalidOperationException(SR.Get("RoutedEventArgsMustHaveRoutedEvent"));
				}
				if (TraceRoutedEvent.IsEnabled)
				{
					TraceRoutedEvent.TraceActivityItem(TraceRoutedEvent.HandleEvent, new object[]
					{
						value,
						this.RoutedEvent.OwnerType.Name,
						this.RoutedEvent.Name,
						this
					});
				}
				this._flags[1] = value;
			}
		}

		/// <summary>Obtém ou define uma referência ao objeto que ativou o evento.</summary>
		/// <returns>O objeto que gerencie o evento.</returns>
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0002FF10 File Offset: 0x0002F310
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x0002FF24 File Offset: 0x0002F324
		public object Source
		{
			get
			{
				return this._source;
			}
			set
			{
				if (this.InvokingHandler && this.UserInitiated)
				{
					throw new InvalidOperationException(SR.Get("RoutedEventCannotChangeWhileRouting"));
				}
				if (this._routedEvent == null)
				{
					throw new InvalidOperationException(SR.Get("RoutedEventArgsMustHaveRoutedEvent"));
				}
				if (this._source == null && this._originalSource == null)
				{
					this._originalSource = value;
					this._source = value;
					this.OnSetSource(value);
					return;
				}
				if (this._source != value)
				{
					this._source = value;
					this.OnSetSource(value);
				}
			}
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0002FFAC File Offset: 0x0002F3AC
		internal void OverrideSource(object source)
		{
			this._source = source;
		}

		/// <summary>Obtém a origem de relatório original conforme determinada pelo teste de clique puro, antes de qualquer ajuste possível da <see cref="P:System.Windows.RoutedEventArgs.Source" /> por parte de uma classe pai.</summary>
		/// <returns>A origem de relatório original, antes de qualquer ajuste <see cref="P:System.Windows.RoutedEventArgs.Source" /> possível realizado pelo tratamento de classe, que pode ter sido feito para mesclar árvores de elementos compostos.</returns>
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0002FFC0 File Offset: 0x0002F3C0
		public object OriginalSource
		{
			get
			{
				return this._originalSource;
			}
		}

		/// <summary>Quando substituído em uma classe derivada, fornece um ponto de entrada de retorno de chamada de notificação sempre que o valor da propriedade <see cref="P:System.Windows.RoutedEventArgs.Source" /> de uma instância muda.</summary>
		/// <param name="source">O novo valor para o qual o <see cref="P:System.Windows.RoutedEventArgs.Source" /> está sendo definido.</param>
		// Token: 0x06000C8E RID: 3214 RVA: 0x0002FFD4 File Offset: 0x0002F3D4
		protected virtual void OnSetSource(object source)
		{
		}

		/// <summary>Quando substituído em uma classe derivada, fornece um modo de se invocar os manipuladores de eventos de uma maneira específica a um tipo, que pode aumentar a eficiência com relação à implementação base.</summary>
		/// <param name="genericHandler">A implementação de manipulador/delegado genérica a ser invocada.</param>
		/// <param name="genericTarget">O destino no qual o manipulador fornecido deve ser invocado.</param>
		// Token: 0x06000C8F RID: 3215 RVA: 0x0002FFE4 File Offset: 0x0002F3E4
		protected virtual void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			if (genericHandler == null)
			{
				throw new ArgumentNullException("genericHandler");
			}
			if (genericTarget == null)
			{
				throw new ArgumentNullException("genericTarget");
			}
			if (this._routedEvent == null)
			{
				throw new InvalidOperationException(SR.Get("RoutedEventArgsMustHaveRoutedEvent"));
			}
			this.InvokingHandler = true;
			try
			{
				if (genericHandler is RoutedEventHandler)
				{
					((RoutedEventHandler)genericHandler)(genericTarget, this);
				}
				else
				{
					genericHandler.DynamicInvoke(new object[]
					{
						genericTarget,
						this
					});
				}
			}
			finally
			{
				this.InvokingHandler = false;
			}
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00030080 File Offset: 0x0002F480
		internal void InvokeHandler(Delegate handler, object target)
		{
			this.InvokingHandler = true;
			try
			{
				this.InvokeEventHandler(handler, target);
			}
			finally
			{
				this.InvokingHandler = false;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x000300C4 File Offset: 0x0002F4C4
		internal bool UserInitiated
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			[FriendAccessAllowed]
			get
			{
				return this._flags[2] && SecurityHelper.CallerHasUserInitiatedRoutedEventPermission();
			}
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x000300E8 File Offset: 0x0002F4E8
		[SecurityCritical]
		internal void MarkAsUserInitiated()
		{
			this._flags[2] = true;
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00030104 File Offset: 0x0002F504
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal void ClearUserInitiated()
		{
			this._flags[2] = false;
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x00030120 File Offset: 0x0002F520
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x0003013C File Offset: 0x0002F53C
		private bool InvokingHandler
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				return this._flags[4];
			}
			[SecurityCritical]
			[SecurityTreatAsSafe]
			set
			{
				this._flags[4] = value;
			}
		}

		// Token: 0x04000735 RID: 1845
		private RoutedEvent _routedEvent;

		// Token: 0x04000736 RID: 1846
		private object _source;

		// Token: 0x04000737 RID: 1847
		private object _originalSource;

		// Token: 0x04000738 RID: 1848
		[SecurityCritical]
		private BitVector32 _flags;

		// Token: 0x04000739 RID: 1849
		private const int HandledIndex = 1;

		// Token: 0x0400073A RID: 1850
		private const int UserInitiatedIndex = 2;

		// Token: 0x0400073B RID: 1851
		private const int InvokingHandlerIndex = 4;
	}
}
