using System;
using System.Diagnostics.Contracts;
using System.Diagnostics.Tracing;

namespace MS.Internal.Telemetry.PresentationCore
{
	// Token: 0x020007A9 RID: 1961
	internal sealed class EventSourceActivity : IDisposable
	{
		// Token: 0x06005264 RID: 21092 RVA: 0x00148884 File Offset: 0x00147C84
		internal EventSourceActivity(EventSource eventSource) : this(eventSource, default(EventSourceOptions))
		{
		}

		// Token: 0x06005265 RID: 21093 RVA: 0x001488A4 File Offset: 0x00147CA4
		internal EventSourceActivity(EventSource eventSource, EventSourceOptions startStopOptions) : this(eventSource, startStopOptions, Guid.Empty)
		{
		}

		// Token: 0x06005266 RID: 21094 RVA: 0x001488C0 File Offset: 0x00147CC0
		internal EventSourceActivity(EventSource eventSource, EventSourceOptions startStopOptions, Guid parentActivityId)
		{
			this._id = Guid.NewGuid();
			base..ctor();
			Contract.Requires<ArgumentNullException>(eventSource != null, "eventSource");
			this._eventSource = eventSource;
			this._startStopOptions = startStopOptions;
			this._parentId = parentActivityId;
		}

		// Token: 0x06005267 RID: 21095 RVA: 0x00148904 File Offset: 0x00147D04
		internal EventSourceActivity(EventSourceActivity parentActivity) : this(parentActivity, default(EventSourceOptions))
		{
		}

		// Token: 0x06005268 RID: 21096 RVA: 0x00148924 File Offset: 0x00147D24
		internal EventSourceActivity(EventSourceActivity parentActivity, EventSourceOptions startStopOptions)
		{
			this._id = Guid.NewGuid();
			base..ctor();
			Contract.Requires<ArgumentNullException>(parentActivity != null, "parentActivity");
			this._eventSource = parentActivity.EventSource;
			this._startStopOptions = startStopOptions;
			this._parentId = parentActivity.Id;
		}

		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x06005269 RID: 21097 RVA: 0x00148970 File Offset: 0x00147D70
		internal EventSource EventSource
		{
			get
			{
				return this._eventSource;
			}
		}

		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x0600526A RID: 21098 RVA: 0x00148984 File Offset: 0x00147D84
		internal Guid Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x0600526B RID: 21099 RVA: 0x00148998 File Offset: 0x00147D98
		internal void Start(string eventName)
		{
			Contract.Requires<ArgumentNullException>(eventName != null, "eventName");
			EventSourceActivity.EmptyStruct instance = EventSourceActivity.EmptyStruct.Instance;
			this.Start<EventSourceActivity.EmptyStruct>(eventName, ref instance);
		}

		// Token: 0x0600526C RID: 21100 RVA: 0x001489C4 File Offset: 0x00147DC4
		internal void Start<T>(string eventName, T data)
		{
			this.Start<T>(eventName, ref data);
		}

		// Token: 0x0600526D RID: 21101 RVA: 0x001489DC File Offset: 0x00147DDC
		internal void Stop(string eventName)
		{
			Contract.Requires<ArgumentNullException>(eventName != null, "eventName");
			EventSourceActivity.EmptyStruct instance = EventSourceActivity.EmptyStruct.Instance;
			this.Stop<EventSourceActivity.EmptyStruct>(eventName, ref instance);
		}

		// Token: 0x0600526E RID: 21102 RVA: 0x00148A08 File Offset: 0x00147E08
		internal void Stop<T>(string eventName, T data)
		{
			this.Stop<T>(eventName, ref data);
		}

		// Token: 0x0600526F RID: 21103 RVA: 0x00148A20 File Offset: 0x00147E20
		internal void Write(string eventName)
		{
			Contract.Requires<ArgumentNullException>(eventName != null, "eventName");
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			EventSourceActivity.EmptyStruct instance = EventSourceActivity.EmptyStruct.Instance;
			this.Write<EventSourceActivity.EmptyStruct>(eventName, ref eventSourceOptions, ref instance);
		}

		// Token: 0x06005270 RID: 21104 RVA: 0x00148A54 File Offset: 0x00147E54
		internal void Write(string eventName, EventSourceOptions options)
		{
			Contract.Requires<ArgumentNullException>(eventName != null, "eventName");
			EventSourceActivity.EmptyStruct instance = EventSourceActivity.EmptyStruct.Instance;
			this.Write<EventSourceActivity.EmptyStruct>(eventName, ref options, ref instance);
		}

		// Token: 0x06005271 RID: 21105 RVA: 0x00148A80 File Offset: 0x00147E80
		internal void Write<T>(string eventName, T data)
		{
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			this.Write<T>(eventName, ref eventSourceOptions, ref data);
		}

		// Token: 0x06005272 RID: 21106 RVA: 0x00148AA0 File Offset: 0x00147EA0
		internal void Write<T>(string eventName, EventSourceOptions options, T data)
		{
			this.Write<T>(eventName, ref options, ref data);
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x00148AB8 File Offset: 0x00147EB8
		public void Dispose()
		{
			if (this._state == EventSourceActivity.State.Started)
			{
				this._state = EventSourceActivity.State.Stopped;
				EventSourceActivity.EmptyStruct instance = EventSourceActivity.EmptyStruct.Instance;
				this._eventSource.Write<EventSourceActivity.EmptyStruct>("Dispose", ref this._startStopOptions, ref this._id, ref EventSourceActivity._emptyGuid, ref instance);
			}
		}

		// Token: 0x06005274 RID: 21108 RVA: 0x00148B00 File Offset: 0x00147F00
		private void Start<T>(string eventName, ref T data)
		{
			if (this._state != EventSourceActivity.State.Initialized)
			{
				throw new InvalidOperationException();
			}
			this._state = EventSourceActivity.State.Started;
			this._startStopOptions.Opcode = EventOpcode.Start;
			this._eventSource.Write<T>(eventName, ref this._startStopOptions, ref this._id, ref this._parentId, ref data);
			this._startStopOptions.Opcode = EventOpcode.Stop;
		}

		// Token: 0x06005275 RID: 21109 RVA: 0x00148B5C File Offset: 0x00147F5C
		private void Write<T>(string eventName, ref EventSourceOptions options, ref T data)
		{
			if (this._state != EventSourceActivity.State.Started)
			{
				throw new InvalidOperationException();
			}
			this._eventSource.Write<T>(eventName, ref options, ref this._id, ref EventSourceActivity._emptyGuid, ref data);
		}

		// Token: 0x06005276 RID: 21110 RVA: 0x00148B94 File Offset: 0x00147F94
		private void Stop<T>(string eventName, ref T data)
		{
			if (this._state != EventSourceActivity.State.Started)
			{
				throw new InvalidOperationException();
			}
			this._state = EventSourceActivity.State.Stopped;
			this._eventSource.Write<T>(eventName, ref this._startStopOptions, ref this._id, ref EventSourceActivity._emptyGuid, ref data);
		}

		// Token: 0x04002527 RID: 9511
		private static Guid _emptyGuid;

		// Token: 0x04002528 RID: 9512
		private readonly EventSource _eventSource;

		// Token: 0x04002529 RID: 9513
		private EventSourceOptions _startStopOptions;

		// Token: 0x0400252A RID: 9514
		private Guid _parentId;

		// Token: 0x0400252B RID: 9515
		private Guid _id;

		// Token: 0x0400252C RID: 9516
		private EventSourceActivity.State _state;

		// Token: 0x020009FB RID: 2555
		private enum State
		{
			// Token: 0x04002F18 RID: 12056
			Initialized,
			// Token: 0x04002F19 RID: 12057
			Started,
			// Token: 0x04002F1A RID: 12058
			Stopped
		}

		// Token: 0x020009FC RID: 2556
		[EventData]
		private class EmptyStruct
		{
			// Token: 0x06005BE0 RID: 23520 RVA: 0x001712C0 File Offset: 0x001706C0
			private EmptyStruct()
			{
			}

			// Token: 0x170012C9 RID: 4809
			// (get) Token: 0x06005BE1 RID: 23521 RVA: 0x001712D4 File Offset: 0x001706D4
			internal static EventSourceActivity.EmptyStruct Instance
			{
				get
				{
					if (EventSourceActivity.EmptyStruct._instance == null)
					{
						EventSourceActivity.EmptyStruct._instance = new EventSourceActivity.EmptyStruct();
					}
					return EventSourceActivity.EmptyStruct._instance;
				}
			}

			// Token: 0x04002F1B RID: 12059
			private static EventSourceActivity.EmptyStruct _instance;
		}
	}
}
