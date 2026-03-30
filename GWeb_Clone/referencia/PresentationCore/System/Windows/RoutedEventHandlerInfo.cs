using System;

namespace System.Windows
{
	/// <summary>Fornece informações de tratamento especial para informar ouvintes de evento se os manipuladores específicos devem ser chamados.</summary>
	// Token: 0x020001D6 RID: 470
	public struct RoutedEventHandlerInfo
	{
		// Token: 0x06000C9A RID: 3226 RVA: 0x00030158 File Offset: 0x0002F558
		internal RoutedEventHandlerInfo(Delegate handler, bool handledEventsToo)
		{
			this._handler = handler;
			this._handledEventsToo = handledEventsToo;
		}

		/// <summary>Obtém o manipulador de eventos.</summary>
		/// <returns>O manipulador de eventos.</returns>
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x00030174 File Offset: 0x0002F574
		public Delegate Handler
		{
			get
			{
				return this._handler;
			}
		}

		/// <summary>Obtém um valor que indica se o manipulador de eventos é invocado quando o evento roteado é marcado como manipulado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o manipulador de eventos é invocado quando o evento roteado estiver marcado como tratado; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00030188 File Offset: 0x0002F588
		public bool InvokeHandledEventsToo
		{
			get
			{
				return this._handledEventsToo;
			}
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0003019C File Offset: 0x0002F59C
		internal void InvokeHandler(object target, RoutedEventArgs routedEventArgs)
		{
			if (!routedEventArgs.Handled || this._handledEventsToo)
			{
				if (this._handler is RoutedEventHandler)
				{
					((RoutedEventHandler)this._handler)(target, routedEventArgs);
					return;
				}
				routedEventArgs.InvokeHandler(this._handler, target);
			}
		}

		/// <summary>Determina se o objeto especificado é equivalente ao <see cref="T:System.Windows.RoutedEventHandlerInfo" /> atual.</summary>
		/// <param name="obj">O objeto a ser comparado ao <see cref="T:System.Windows.RoutedEventHandlerInfo" /> atual.</param>
		/// <returns>
		///   <see langword="true" /> se o objeto especificado for equivalente ao <see cref="T:System.Windows.RoutedEventHandlerInfo" /> atual; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000C9E RID: 3230 RVA: 0x000301E8 File Offset: 0x0002F5E8
		public override bool Equals(object obj)
		{
			return obj != null && obj is RoutedEventHandlerInfo && this.Equals((RoutedEventHandlerInfo)obj);
		}

		/// <summary>Determina se o <see cref="T:System.Windows.RoutedEventHandlerInfo" /> especificado é equivalente ao <see cref="T:System.Windows.RoutedEventHandlerInfo" /> atual.</summary>
		/// <param name="handlerInfo">O <see cref="T:System.Windows.RoutedEventHandlerInfo" /> a ser comparado com o <see cref="T:System.Windows.RoutedEventHandlerInfo" /> atual.</param>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.RoutedEventHandlerInfo" /> especificado for equivalente ao <see cref="T:System.Windows.RoutedEventHandlerInfo" /> atual; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000C9F RID: 3231 RVA: 0x00030210 File Offset: 0x0002F610
		public bool Equals(RoutedEventHandlerInfo handlerInfo)
		{
			return this._handler == handlerInfo._handler && this._handledEventsToo == handlerInfo._handledEventsToo;
		}

		/// <summary>Retorna um código hash para o <see cref="T:System.Windows.RoutedEventHandlerInfo" /> atual.</summary>
		/// <returns>Um código hash do <see cref="T:System.Windows.RoutedEventHandlerInfo" /> atual.</returns>
		// Token: 0x06000CA0 RID: 3232 RVA: 0x00030240 File Offset: 0x0002F640
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determina se os objetos especificados são equivalentes.</summary>
		/// <param name="handlerInfo1">O primeiro objeto a ser comparado.</param>
		/// <param name="handlerInfo2">O segundo objeto a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se os objetos forem equivalentes, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000CA1 RID: 3233 RVA: 0x00030260 File Offset: 0x0002F660
		public static bool operator ==(RoutedEventHandlerInfo handlerInfo1, RoutedEventHandlerInfo handlerInfo2)
		{
			return handlerInfo1.Equals(handlerInfo2);
		}

		/// <summary>Determina se os objetos especificados não são equivalentes.</summary>
		/// <param name="handlerInfo1">O primeiro objeto a ser comparado.</param>
		/// <param name="handlerInfo2">O segundo objeto a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se os objetos não forem equivalentes, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000CA2 RID: 3234 RVA: 0x00030278 File Offset: 0x0002F678
		public static bool operator !=(RoutedEventHandlerInfo handlerInfo1, RoutedEventHandlerInfo handlerInfo2)
		{
			return !handlerInfo1.Equals(handlerInfo2);
		}

		// Token: 0x0400073C RID: 1852
		private Delegate _handler;

		// Token: 0x0400073D RID: 1853
		private bool _handledEventsToo;
	}
}
