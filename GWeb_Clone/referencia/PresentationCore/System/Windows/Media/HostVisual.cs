using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Media.Composition;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Representa um objeto <see cref="T:System.Windows.Media.Visual" /> que pode ser conectado em qualquer lugar a uma árvore visual pai.</summary>
	// Token: 0x02000412 RID: 1042
	public class HostVisual : ContainerVisual
	{
		/// <summary>Implementa <see cref="M:System.Windows.Media.HostVisual.HitTestCore(System.Windows.Media.PointHitTestParameters)" /> para fornecer o comportamento do teste de clique base (retornando <see cref="T:System.Windows.Media.PointHitTestParameters" />).</summary>
		/// <param name="hitTestParameters">Um valor do tipo <see cref="T:System.Windows.Media.PointHitTestParameters" />.</param>
		/// <returns>Retorna um valor de tipo <see cref="T:System.Windows.Media.HitTestResult" />. A propriedade <see cref="P:System.Windows.Media.HitTestResult.VisualHit" /> contém o objeto visual que foi clicado.</returns>
		// Token: 0x06002A0B RID: 10763 RVA: 0x000A8B98 File Offset: 0x000A7F98
		protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
		{
			return null;
		}

		/// <summary>Implementa <see cref="M:System.Windows.Media.HostVisual.HitTestCore(System.Windows.Media.GeometryHitTestParameters)" /> para fornecer o comportamento do teste de clique base (retornando <see cref="T:System.Windows.Media.GeometryHitTestParameters" />).</summary>
		/// <param name="hitTestParameters">Um valor do tipo <see cref="T:System.Windows.Media.GeometryHitTestParameters" />.</param>
		/// <returns>Retorna um valor de tipo <see cref="T:System.Windows.Media.GeometryHitTestResult" />. A propriedade <see cref="P:System.Windows.Media.GeometryHitTestResult.VisualHit" /> contém o elemento visual que foi clicado.</returns>
		// Token: 0x06002A0C RID: 10764 RVA: 0x000A8BA8 File Offset: 0x000A7FA8
		protected override GeometryHitTestResult HitTestCore(GeometryHitTestParameters hitTestParameters)
		{
			return null;
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x000A8BB8 File Offset: 0x000A7FB8
		internal override Rect GetContentBounds()
		{
			return Rect.Empty;
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x000A8BCC File Offset: 0x000A7FCC
		internal override void RenderContent(RenderContext ctx, bool isOnChannel)
		{
			this.EnsureHostedVisualConnected(ctx.Channel);
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x000A8BE8 File Offset: 0x000A7FE8
		internal override void FreeContent(DUCE.Channel channel)
		{
			using (CompositionEngineLock.Acquire())
			{
				if (!this.DoPendingDisconnect(channel))
				{
					this.DisconnectHostedVisual(channel, true);
				}
			}
			base.FreeContent(channel);
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x000A8C40 File Offset: 0x000A8040
		internal void BeginHosting(VisualTarget target)
		{
			using (CompositionEngineLock.Acquire())
			{
				if (this._target != null)
				{
					throw new InvalidOperationException(SR.Get("VisualTarget_AnotherTargetAlreadyConnected"));
				}
				this._target = target;
				if (base.CheckAccess())
				{
					this.Invalidate();
				}
				else
				{
					base.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new DispatcherOperationCallback(delegate(object args)
					{
						this.Invalidate();
						return null;
					}), null);
				}
			}
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x000A8CCC File Offset: 0x000A80CC
		internal void EndHosting()
		{
			using (CompositionEngineLock.Acquire())
			{
				this.DisconnectHostedVisualOnAllChannels();
				this._target = null;
			}
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x000A8D18 File Offset: 0x000A8118
		internal object DoHandleDuplication(object channel)
		{
			DUCE.ResourceHandle resourceHandle = DUCE.ResourceHandle.Null;
			using (CompositionEngineLock.Acquire())
			{
				resourceHandle = this._target._contentRoot.DuplicateHandle(this._target.OutOfBandChannel, (DUCE.Channel)channel);
				this._target.OutOfBandChannel.CloseBatch();
				this._target.OutOfBandChannel.Commit();
			}
			return resourceHandle;
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x000A8DA8 File Offset: 0x000A81A8
		private void EnsureHostedVisualConnected(DUCE.Channel channel)
		{
			if (!channel.IsSynchronous && this._target != null && !this._connectedChannels.ContainsKey(channel))
			{
				DUCE.ResourceHandle hChild = DUCE.ResourceHandle.Null;
				bool flag = true;
				if (this._target.CheckAccess())
				{
					bool flag2 = this._target._contentRoot.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_VISUAL);
					hChild = this._target._contentRoot.GetHandle(channel);
				}
				else
				{
					object obj = this._target.Dispatcher.Invoke(DispatcherPriority.Normal, TimeSpan.FromMilliseconds(1000.0), new DispatcherOperationCallback(this.DoHandleDuplication), channel);
					if (obj != null)
					{
						hChild = (DUCE.ResourceHandle)obj;
					}
					else
					{
						flag = false;
					}
				}
				if (flag)
				{
					if (!hChild.IsNull)
					{
						using (CompositionEngineLock.Acquire())
						{
							DUCE.CompositionNode.InsertChildAt(this._proxy.GetHandle(channel), hChild, 0U, channel);
						}
						Dispatcher value = Dispatcher.FromThread(Thread.CurrentThread);
						this._connectedChannels.Add(channel, value);
						base.SetFlags(channel, true, VisualProxyFlags.IsContentNodeConnected);
						return;
					}
				}
				else
				{
					base.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new DispatcherOperationCallback(delegate(object args)
					{
						this.Invalidate();
						return null;
					}), null);
				}
			}
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x000A8EEC File Offset: 0x000A82EC
		private void DisconnectHostedVisualOnAllChannels()
		{
			IDictionaryEnumerator dictionaryEnumerator = this._connectedChannels.GetEnumerator();
			while (dictionaryEnumerator.MoveNext())
			{
				this.DisconnectHostedVisual((DUCE.Channel)dictionaryEnumerator.Key, false);
			}
			this._connectedChannels.Clear();
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x000A8F34 File Offset: 0x000A8334
		private void DisconnectHostedVisual(DUCE.Channel channel, bool removeChannelFromCollection)
		{
			Dispatcher dispatcher;
			if (this._target != null && this._connectedChannels.TryGetValue(channel, out dispatcher))
			{
				if (CoreAppContextSwitches.HostVisualDisconnectsOnWrongThread || (dispatcher != null && dispatcher.CheckAccess()))
				{
					this.Disconnect(channel, dispatcher, this._proxy.GetHandle(channel), this._target._contentRoot.GetHandle(channel), this._target._contentRoot);
				}
				else if (dispatcher != null)
				{
					DispatcherOperation op = dispatcher.BeginInvoke(DispatcherPriority.Normal, new DispatcherOperationCallback(this.DoDisconnectHostedVisual), channel);
					HostVisual._disconnectData = new HostVisual.DisconnectData(op, channel, dispatcher, this, this._proxy.GetHandle(channel), this._target._contentRoot.GetHandle(channel), this._target._contentRoot, HostVisual._disconnectData);
				}
				if (removeChannelFromCollection)
				{
					this._connectedChannels.Remove(channel);
				}
			}
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x000A9008 File Offset: 0x000A8408
		private object DoDisconnectHostedVisual(object arg)
		{
			using (CompositionEngineLock.Acquire())
			{
				this.DoPendingDisconnect((DUCE.Channel)arg);
			}
			return null;
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x000A9058 File Offset: 0x000A8458
		private bool DoPendingDisconnect(DUCE.Channel channel)
		{
			HostVisual.DisconnectData disconnectData = HostVisual._disconnectData;
			HostVisual.DisconnectData disconnectData2 = null;
			while (disconnectData != null && (disconnectData.HostVisual != this || disconnectData.Channel != channel))
			{
				disconnectData2 = disconnectData;
				disconnectData = disconnectData.Next;
			}
			if (disconnectData == null)
			{
				return false;
			}
			if (disconnectData2 == null)
			{
				HostVisual._disconnectData = disconnectData.Next;
			}
			else
			{
				disconnectData2.Next = disconnectData.Next;
			}
			disconnectData.DispatcherOperation.Abort();
			this.Disconnect(disconnectData.Channel, disconnectData.ChannelDispatcher, disconnectData.HostHandle, disconnectData.TargetHandle, disconnectData.ContentRoot);
			return true;
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x000A90E0 File Offset: 0x000A84E0
		private void Disconnect(DUCE.Channel channel, Dispatcher channelDispatcher, DUCE.ResourceHandle hostHandle, DUCE.ResourceHandle targetHandle, DUCE.MultiChannelResource contentRoot)
		{
			if (!CoreAppContextSwitches.HostVisualDisconnectsOnWrongThread)
			{
				channelDispatcher.VerifyAccess();
			}
			DUCE.CompositionNode.RemoveChild(hostHandle, targetHandle, channel);
			contentRoot.ReleaseOnChannel(channel);
			base.SetFlags(channel, false, VisualProxyFlags.IsContentNodeConnected);
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x000A911C File Offset: 0x000A851C
		private void Invalidate()
		{
			base.SetFlagsOnAllChannels(true, VisualProxyFlags.IsContentDirty);
			base.PropagateChangedFlags();
		}

		// Token: 0x040012FD RID: 4861
		private VisualTarget _target;

		// Token: 0x040012FE RID: 4862
		private Dictionary<DUCE.Channel, Dispatcher> _connectedChannels = new Dictionary<DUCE.Channel, Dispatcher>();

		// Token: 0x040012FF RID: 4863
		private static HostVisual.DisconnectData _disconnectData;

		// Token: 0x0200088F RID: 2191
		private class DisconnectData
		{
			// Token: 0x1700122E RID: 4654
			// (get) Token: 0x0600580A RID: 22538 RVA: 0x0016746C File Offset: 0x0016686C
			// (set) Token: 0x0600580B RID: 22539 RVA: 0x00167480 File Offset: 0x00166880
			public DispatcherOperation DispatcherOperation { get; private set; }

			// Token: 0x1700122F RID: 4655
			// (get) Token: 0x0600580C RID: 22540 RVA: 0x00167494 File Offset: 0x00166894
			// (set) Token: 0x0600580D RID: 22541 RVA: 0x001674A8 File Offset: 0x001668A8
			public DUCE.Channel Channel { get; private set; }

			// Token: 0x17001230 RID: 4656
			// (get) Token: 0x0600580E RID: 22542 RVA: 0x001674BC File Offset: 0x001668BC
			// (set) Token: 0x0600580F RID: 22543 RVA: 0x001674D0 File Offset: 0x001668D0
			public Dispatcher ChannelDispatcher { get; private set; }

			// Token: 0x17001231 RID: 4657
			// (get) Token: 0x06005810 RID: 22544 RVA: 0x001674E4 File Offset: 0x001668E4
			// (set) Token: 0x06005811 RID: 22545 RVA: 0x001674F8 File Offset: 0x001668F8
			public HostVisual HostVisual { get; private set; }

			// Token: 0x17001232 RID: 4658
			// (get) Token: 0x06005812 RID: 22546 RVA: 0x0016750C File Offset: 0x0016690C
			// (set) Token: 0x06005813 RID: 22547 RVA: 0x00167520 File Offset: 0x00166920
			public DUCE.ResourceHandle HostHandle { get; private set; }

			// Token: 0x17001233 RID: 4659
			// (get) Token: 0x06005814 RID: 22548 RVA: 0x00167534 File Offset: 0x00166934
			// (set) Token: 0x06005815 RID: 22549 RVA: 0x00167548 File Offset: 0x00166948
			public DUCE.ResourceHandle TargetHandle { get; private set; }

			// Token: 0x17001234 RID: 4660
			// (get) Token: 0x06005816 RID: 22550 RVA: 0x0016755C File Offset: 0x0016695C
			// (set) Token: 0x06005817 RID: 22551 RVA: 0x00167570 File Offset: 0x00166970
			public DUCE.MultiChannelResource ContentRoot { get; private set; }

			// Token: 0x17001235 RID: 4661
			// (get) Token: 0x06005818 RID: 22552 RVA: 0x00167584 File Offset: 0x00166984
			// (set) Token: 0x06005819 RID: 22553 RVA: 0x00167598 File Offset: 0x00166998
			public HostVisual.DisconnectData Next { get; set; }

			// Token: 0x0600581A RID: 22554 RVA: 0x001675AC File Offset: 0x001669AC
			public DisconnectData(DispatcherOperation op, DUCE.Channel channel, Dispatcher dispatcher, HostVisual hostVisual, DUCE.ResourceHandle hostHandle, DUCE.ResourceHandle targetHandle, DUCE.MultiChannelResource contentRoot, HostVisual.DisconnectData next)
			{
				this.DispatcherOperation = op;
				this.Channel = channel;
				this.ChannelDispatcher = dispatcher;
				this.HostVisual = hostVisual;
				this.HostHandle = hostHandle;
				this.TargetHandle = targetHandle;
				this.ContentRoot = contentRoot;
				this.Next = next;
			}
		}
	}
}
