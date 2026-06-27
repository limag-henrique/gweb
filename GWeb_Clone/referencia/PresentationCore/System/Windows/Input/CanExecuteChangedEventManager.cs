using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MS.Internal;

namespace System.Windows.Input
{
	/// <summary>Fornece uma implementação <see cref="T:System.Windows.WeakEventManager" /> para que seja possível usar o padrão “ouvinte de eventos fraco” para anexar ouvintes ao evento <see cref="E:System.Windows.Input.ICommand.CanExecuteChanged" />.</summary>
	// Token: 0x02000205 RID: 517
	public class CanExecuteChangedEventManager : WeakEventManager
	{
		// Token: 0x06000D7A RID: 3450 RVA: 0x00033510 File Offset: 0x00032910
		private CanExecuteChangedEventManager()
		{
		}

		/// <summary>Adiciona o delegado especificado como um manipulador de eventos de origem especificada.</summary>
		/// <param name="source">O objeto de origem que gera o evento <see cref="E:System.Windows.Input.ICommand.CanExecuteChanged" />.</param>
		/// <param name="handler">O delegado que manipula o evento <see cref="E:System.Windows.Input.ICommand.CanExecuteChanged" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> é <see langword="null" />.  
		///
		/// ou - 
		/// <paramref name="handler" /> é <see langword="null" />.</exception>
		// Token: 0x06000D7B RID: 3451 RVA: 0x00033530 File Offset: 0x00032930
		public static void AddHandler(ICommand source, EventHandler<EventArgs> handler)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			CanExecuteChangedEventManager.CurrentManager.PrivateAddHandler(source, handler);
		}

		/// <summary>Remove o manipulador de eventos especificado da fonte especificada.</summary>
		/// <param name="source">O objeto de origem que gera o evento <see cref="E:System.Windows.Input.ICommand.CanExecuteChanged" />.</param>
		/// <param name="handler">O delegado que manipula o evento <see cref="E:System.Windows.Input.ICommand.CanExecuteChanged" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> é <see langword="null" />.  
		///
		/// ou - 
		/// <paramref name="handler" /> é <see langword="null" />.</exception>
		// Token: 0x06000D7C RID: 3452 RVA: 0x00033568 File Offset: 0x00032968
		public static void RemoveHandler(ICommand source, EventHandler<EventArgs> handler)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			CanExecuteChangedEventManager.CurrentManager.PrivateRemoveHandler(source, handler);
		}

		/// <summary>Começa a escutar o evento <see cref="E:System.Windows.Input.ICommand.CanExecuteChanged" /> na origem especificada.</summary>
		/// <param name="source">O objeto com o evento.</param>
		// Token: 0x06000D7D RID: 3453 RVA: 0x000335A0 File Offset: 0x000329A0
		protected override void StartListening(object source)
		{
		}

		/// <summary>Para de escutar o evento <see cref="E:System.Windows.Input.ICommand.CanExecuteChanged" /> na origem especificada.</summary>
		/// <param name="source">O objeto de origem para o qual interromper a escuta.</param>
		// Token: 0x06000D7E RID: 3454 RVA: 0x000335B0 File Offset: 0x000329B0
		protected override void StopListening(object source)
		{
		}

		/// <summary>Remove entradas do ouvinte inativo da lista de dados para a origem fornecida.</summary>
		/// <param name="source">A origem de eventos que está sendo ouvida.</param>
		/// <param name="data">Os dados a serem verificados. Espera-se que este objeto seja uma implementação de <see cref="T:System.Windows.WeakEventManager.ListenerList" />.</param>
		/// <param name="purgeAll">O <see langword="true" /> para parar de escutar <paramref name="source" /> e remover completamente todas as entradas de <paramref name="data" />.</param>
		/// <returns>
		///   <see langword="true" /> se algumas entradas foram removidas, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000D7F RID: 3455 RVA: 0x000335C0 File Offset: 0x000329C0
		protected override bool Purge(object source, object data, bool purgeAll)
		{
			bool isOnOriginalThread = !purgeAll || base.CheckAccess();
			ICommand command = source as ICommand;
			List<CanExecuteChangedEventManager.HandlerSink> list = data as List<CanExecuteChangedEventManager.HandlerSink>;
			List<CanExecuteChangedEventManager.HandlerSink> list2 = null;
			bool flag = false;
			bool flag2 = purgeAll || source == null;
			if (!flag2)
			{
				foreach (CanExecuteChangedEventManager.HandlerSink handlerSink in list)
				{
					if (handlerSink.IsInactive)
					{
						if (list2 == null)
						{
							list2 = new List<CanExecuteChangedEventManager.HandlerSink>();
						}
						list2.Add(handlerSink);
					}
				}
				flag2 = (list2 != null && list2.Count == list.Count);
			}
			if (flag2)
			{
				list2 = list;
			}
			flag = (list2 != null);
			if (flag2 && !purgeAll && source != null)
			{
				base.Remove(source);
			}
			if (flag)
			{
				foreach (CanExecuteChangedEventManager.HandlerSink handlerSink2 in list2)
				{
					EventHandler<EventArgs> handler = handlerSink2.Handler;
					handlerSink2.Detach(isOnOriginalThread);
					if (!flag2)
					{
						list.Remove(handlerSink2);
					}
					if (handler != null)
					{
						this.RemoveHandlerFromCWT(handler, this._cwt);
					}
				}
			}
			return flag;
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x00033708 File Offset: 0x00032B08
		private static CanExecuteChangedEventManager CurrentManager
		{
			get
			{
				Type typeFromHandle = typeof(CanExecuteChangedEventManager);
				CanExecuteChangedEventManager canExecuteChangedEventManager = (CanExecuteChangedEventManager)WeakEventManager.GetCurrentManager(typeFromHandle);
				if (canExecuteChangedEventManager == null)
				{
					canExecuteChangedEventManager = new CanExecuteChangedEventManager();
					WeakEventManager.SetCurrentManager(typeFromHandle, canExecuteChangedEventManager);
				}
				return canExecuteChangedEventManager;
			}
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00033740 File Offset: 0x00032B40
		private void PrivateAddHandler(ICommand source, EventHandler<EventArgs> handler)
		{
			List<CanExecuteChangedEventManager.HandlerSink> list = (List<CanExecuteChangedEventManager.HandlerSink>)base[source];
			if (list == null)
			{
				list = new List<CanExecuteChangedEventManager.HandlerSink>();
				base[source] = list;
			}
			CanExecuteChangedEventManager.HandlerSink item = new CanExecuteChangedEventManager.HandlerSink(this, source, handler);
			list.Add(item);
			this.AddHandlerToCWT(handler, this._cwt);
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00033788 File Offset: 0x00032B88
		private void PrivateRemoveHandler(ICommand source, EventHandler<EventArgs> handler)
		{
			List<CanExecuteChangedEventManager.HandlerSink> list = (List<CanExecuteChangedEventManager.HandlerSink>)base[source];
			if (list != null)
			{
				CanExecuteChangedEventManager.HandlerSink handlerSink = null;
				bool flag = false;
				foreach (CanExecuteChangedEventManager.HandlerSink handlerSink2 in list)
				{
					if (handlerSink2.Matches(source, handler))
					{
						handlerSink = handlerSink2;
						break;
					}
					if (handlerSink2.IsInactive)
					{
						flag = true;
					}
				}
				if (handlerSink != null)
				{
					list.Remove(handlerSink);
					handlerSink.Detach(true);
					this.RemoveHandlerFromCWT(handler, this._cwt);
				}
				if (flag)
				{
					base.ScheduleCleanup();
				}
			}
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00033830 File Offset: 0x00032C30
		private void AddHandlerToCWT(Delegate handler, ConditionalWeakTable<object, object> cwt)
		{
			object obj = handler.Target;
			if (obj == null)
			{
				obj = CanExecuteChangedEventManager.StaticSource;
			}
			object obj2;
			if (!cwt.TryGetValue(obj, out obj2))
			{
				cwt.Add(obj, handler);
				return;
			}
			List<Delegate> list = obj2 as List<Delegate>;
			if (list == null)
			{
				Delegate item = obj2 as Delegate;
				list = new List<Delegate>();
				list.Add(item);
				cwt.Remove(obj);
				cwt.Add(obj, list);
			}
			list.Add(handler);
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00033898 File Offset: 0x00032C98
		private void RemoveHandlerFromCWT(Delegate handler, ConditionalWeakTable<object, object> cwt)
		{
			object obj = handler.Target;
			if (obj == null)
			{
				obj = CanExecuteChangedEventManager.StaticSource;
			}
			object obj2;
			if (this._cwt.TryGetValue(obj, out obj2))
			{
				List<Delegate> list = obj2 as List<Delegate>;
				if (list == null)
				{
					this._cwt.Remove(obj);
					return;
				}
				list.Remove(handler);
				if (list.Count == 0)
				{
					this._cwt.Remove(obj);
				}
			}
		}

		// Token: 0x04000812 RID: 2066
		private ConditionalWeakTable<object, object> _cwt = new ConditionalWeakTable<object, object>();

		// Token: 0x04000813 RID: 2067
		private static readonly object StaticSource = new NamedObject("StaticSource");

		// Token: 0x02000802 RID: 2050
		private class HandlerSink
		{
			// Token: 0x060055F4 RID: 22004 RVA: 0x00161B34 File Offset: 0x00160F34
			public HandlerSink(CanExecuteChangedEventManager manager, ICommand source, EventHandler<EventArgs> originalHandler)
			{
				this._manager = manager;
				this._source = new WeakReference(source);
				this._originalHandler = new WeakReference(originalHandler);
				this._onCanExecuteChangedHandler = new EventHandler(this.OnCanExecuteChanged);
				source.CanExecuteChanged += this._onCanExecuteChangedHandler;
			}

			// Token: 0x1700119F RID: 4511
			// (get) Token: 0x060055F5 RID: 22005 RVA: 0x00161B84 File Offset: 0x00160F84
			public bool IsInactive
			{
				get
				{
					return this._source == null || !this._source.IsAlive || this._originalHandler == null || !this._originalHandler.IsAlive;
				}
			}

			// Token: 0x170011A0 RID: 4512
			// (get) Token: 0x060055F6 RID: 22006 RVA: 0x00161BC0 File Offset: 0x00160FC0
			public EventHandler<EventArgs> Handler
			{
				get
				{
					if (this._originalHandler == null)
					{
						return null;
					}
					return (EventHandler<EventArgs>)this._originalHandler.Target;
				}
			}

			// Token: 0x060055F7 RID: 22007 RVA: 0x00161BE8 File Offset: 0x00160FE8
			public bool Matches(ICommand source, EventHandler<EventArgs> handler)
			{
				return this._source != null && (ICommand)this._source.Target == source && this._originalHandler != null && (EventHandler<EventArgs>)this._originalHandler.Target == handler;
			}

			// Token: 0x060055F8 RID: 22008 RVA: 0x00161C34 File Offset: 0x00161034
			public void Detach(bool isOnOriginalThread)
			{
				if (this._source != null)
				{
					ICommand command = (ICommand)this._source.Target;
					if (command != null && isOnOriginalThread)
					{
						command.CanExecuteChanged -= this._onCanExecuteChangedHandler;
					}
					this._source = null;
					this._originalHandler = null;
				}
			}

			// Token: 0x060055F9 RID: 22009 RVA: 0x00161C7C File Offset: 0x0016107C
			private void OnCanExecuteChanged(object sender, EventArgs e)
			{
				if (this._source == null)
				{
					return;
				}
				if (sender is CommandManager)
				{
					sender = this._source.Target;
				}
				EventHandler<EventArgs> eventHandler = (EventHandler<EventArgs>)this._originalHandler.Target;
				if (eventHandler != null)
				{
					eventHandler(sender, e);
					return;
				}
				this._manager.ScheduleCleanup();
			}

			// Token: 0x040026AC RID: 9900
			private CanExecuteChangedEventManager _manager;

			// Token: 0x040026AD RID: 9901
			private WeakReference _source;

			// Token: 0x040026AE RID: 9902
			private WeakReference _originalHandler;

			// Token: 0x040026AF RID: 9903
			private EventHandler _onCanExecuteChangedHandler;
		}
	}
}
