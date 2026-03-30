using System;

namespace System.Windows.Media.Composition
{
	// Token: 0x02000642 RID: 1602
	internal struct VisualProxy
	{
		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06004810 RID: 18448 RVA: 0x0011A218 File Offset: 0x00119618
		internal int Count
		{
			get
			{
				if (this._tail != null)
				{
					int num = this._tail.Length;
					bool flag = this._tail[num - 1].Channel == null;
					return 1 + num - (flag ? 1 : 0);
				}
				if (this._head.Channel != null)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06004811 RID: 18449 RVA: 0x0011A26C File Offset: 0x0011966C
		internal bool IsOnAnyChannel
		{
			get
			{
				return this.Count != 0;
			}
		}

		// Token: 0x06004812 RID: 18450 RVA: 0x0011A284 File Offset: 0x00119684
		internal bool IsOnChannel(DUCE.Channel channel)
		{
			int num = this.Find(channel);
			if (num == -2)
			{
				return false;
			}
			if (num == -1)
			{
				return !this._head.Handle.IsNull;
			}
			return !this._tail[num].Handle.IsNull;
		}

		// Token: 0x06004813 RID: 18451 RVA: 0x0011A2D4 File Offset: 0x001196D4
		internal bool CreateOrAddRefOnChannel(object instance, DUCE.Channel channel, DUCE.ResourceType resourceType)
		{
			int num = this.Find(channel);
			int count = this.Count;
			if (num == -2)
			{
				if (this._head.Channel == null)
				{
					this._head.Channel = channel;
					this._head.Flags = VisualProxyFlags.None;
					channel.CreateOrAddRefOnChannel(instance, ref this._head.Handle, resourceType);
				}
				else
				{
					if (this._tail == null)
					{
						this._tail = new VisualProxy.Proxy[2];
					}
					else if (count > this._tail.Length)
					{
						this.ResizeTail(2);
					}
					VisualProxy.Proxy proxy;
					proxy.Channel = channel;
					proxy.Flags = VisualProxyFlags.None;
					proxy.Handle = DUCE.ResourceHandle.Null;
					channel.CreateOrAddRefOnChannel(instance, ref proxy.Handle, resourceType);
					this._tail[count - 1] = proxy;
				}
				return true;
			}
			if (num == -1)
			{
				channel.CreateOrAddRefOnChannel(instance, ref this._head.Handle, resourceType);
			}
			else
			{
				channel.CreateOrAddRefOnChannel(instance, ref this._tail[num].Handle, resourceType);
			}
			return false;
		}

		// Token: 0x06004814 RID: 18452 RVA: 0x0011A3D0 File Offset: 0x001197D0
		internal bool ReleaseOnChannel(DUCE.Channel channel)
		{
			int num = this.Find(channel);
			bool flag = false;
			int count = this.Count;
			if (num == -1)
			{
				if (channel.ReleaseOnChannel(this._head.Handle))
				{
					if (count == 1)
					{
						this._head = default(VisualProxy.Proxy);
					}
					else
					{
						this._head = this._tail[count - 2];
					}
					flag = true;
				}
			}
			else
			{
				if (num < 0)
				{
					return false;
				}
				if (channel.ReleaseOnChannel(this._tail[num].Handle))
				{
					if (num != count - 2)
					{
						this._tail[num] = this._tail[count - 2];
					}
					flag = true;
				}
			}
			if (flag && this._tail != null)
			{
				if (count == 2)
				{
					this._tail = null;
				}
				else if (count == this._tail.Length)
				{
					this.ResizeTail(-2);
				}
				else
				{
					this._tail[count - 2] = default(VisualProxy.Proxy);
				}
			}
			return flag;
		}

		// Token: 0x06004815 RID: 18453 RVA: 0x0011A4B4 File Offset: 0x001198B4
		internal DUCE.Channel GetChannel(int index)
		{
			if (index < this.Count)
			{
				if (index == 0)
				{
					return this._head.Channel;
				}
				if (index > 0)
				{
					return this._tail[index - 1].Channel;
				}
			}
			return null;
		}

		// Token: 0x06004816 RID: 18454 RVA: 0x0011A4F4 File Offset: 0x001198F4
		internal DUCE.ResourceHandle GetHandle(DUCE.Channel channel)
		{
			return this.GetHandle(this.Find(channel) + 1);
		}

		// Token: 0x06004817 RID: 18455 RVA: 0x0011A510 File Offset: 0x00119910
		internal DUCE.ResourceHandle GetHandle(int index)
		{
			if (index < this.Count)
			{
				if (index == 0)
				{
					return this._head.Handle;
				}
				if (index > 0)
				{
					return this._tail[index - 1].Handle;
				}
			}
			return DUCE.ResourceHandle.Null;
		}

		// Token: 0x06004818 RID: 18456 RVA: 0x0011A554 File Offset: 0x00119954
		internal VisualProxyFlags GetFlags(DUCE.Channel channel)
		{
			return this.GetFlags(this.Find(channel) + 1);
		}

		// Token: 0x06004819 RID: 18457 RVA: 0x0011A570 File Offset: 0x00119970
		internal VisualProxyFlags GetFlags(int index)
		{
			if (index < this.Count)
			{
				if (index == 0)
				{
					return this._head.Flags;
				}
				if (index > 0)
				{
					return this._tail[index - 1].Flags;
				}
			}
			return VisualProxyFlags.None;
		}

		// Token: 0x0600481A RID: 18458 RVA: 0x0011A5B0 File Offset: 0x001199B0
		internal void SetFlags(DUCE.Channel channel, bool value, VisualProxyFlags flags)
		{
			this.SetFlags(this.Find(channel) + 1, value, flags);
		}

		// Token: 0x0600481B RID: 18459 RVA: 0x0011A5D0 File Offset: 0x001199D0
		internal void SetFlags(int index, bool value, VisualProxyFlags flags)
		{
			if (index < this.Count)
			{
				if (index == 0)
				{
					this._head.Flags = (value ? (this._head.Flags | flags) : (this._head.Flags & ~flags));
					return;
				}
				if (index > 0)
				{
					this._tail[index - 1].Flags = (value ? (this._tail[index - 1].Flags | flags) : (this._tail[index - 1].Flags & ~flags));
				}
			}
		}

		// Token: 0x0600481C RID: 18460 RVA: 0x0011A660 File Offset: 0x00119A60
		internal void SetFlagsOnAllChannels(bool value, VisualProxyFlags flags)
		{
			if (this._head.Channel != null)
			{
				this._head.Flags = (value ? (this._head.Flags | flags) : (this._head.Flags & ~flags));
				int i = 0;
				int num = this.Count - 1;
				while (i < num)
				{
					this._tail[i].Flags = (value ? (this._tail[i].Flags | flags) : (this._tail[i].Flags & ~flags));
					i++;
				}
			}
		}

		// Token: 0x0600481D RID: 18461 RVA: 0x0011A6FC File Offset: 0x00119AFC
		internal bool CheckFlagsOnAllChannels(VisualProxyFlags conjunctionFlags)
		{
			if (this._head.Channel != null)
			{
				if ((this._head.Flags & conjunctionFlags) != conjunctionFlags)
				{
					return false;
				}
				int i = 0;
				int num = this.Count - 1;
				while (i < num)
				{
					if ((this._tail[i].Flags & conjunctionFlags) != conjunctionFlags)
					{
						return false;
					}
					i++;
				}
			}
			return true;
		}

		// Token: 0x0600481E RID: 18462 RVA: 0x0011A758 File Offset: 0x00119B58
		private int Find(DUCE.Channel channel)
		{
			if (this._head.Channel == channel)
			{
				return -1;
			}
			if (this._tail != null)
			{
				int i = 0;
				int num = this.Count - 1;
				while (i < num)
				{
					if (this._tail[i].Channel == channel)
					{
						return i;
					}
					i++;
				}
			}
			return -2;
		}

		// Token: 0x0600481F RID: 18463 RVA: 0x0011A7AC File Offset: 0x00119BAC
		private void ResizeTail(int delta)
		{
			int num = this._tail.Length + delta;
			VisualProxy.Proxy[] array = new VisualProxy.Proxy[num];
			Array.Copy(this._tail, array, Math.Min(this._tail.Length, num));
			this._tail = array;
		}

		// Token: 0x04001BC4 RID: 7108
		private const int PROXY_NOT_FOUND = -2;

		// Token: 0x04001BC5 RID: 7109
		private const int PROXY_STORED_INLINE = -1;

		// Token: 0x04001BC6 RID: 7110
		private VisualProxy.Proxy _head;

		// Token: 0x04001BC7 RID: 7111
		private VisualProxy.Proxy[] _tail;

		// Token: 0x0200095E RID: 2398
		private struct Proxy
		{
			// Token: 0x04002CAB RID: 11435
			internal DUCE.Channel Channel;

			// Token: 0x04002CAC RID: 11436
			internal VisualProxyFlags Flags;

			// Token: 0x04002CAD RID: 11437
			internal DUCE.ResourceHandle Handle;
		}
	}
}
