using System;
using System.Collections.Generic;
using MS.Internal.PresentationCore;

namespace MS.Internal
{
	// Token: 0x02000680 RID: 1664
	[FriendAccessAllowed]
	internal class SizeLimitedCache<K, V>
	{
		// Token: 0x06004963 RID: 18787 RVA: 0x0011E3C0 File Offset: 0x0011D7C0
		public SizeLimitedCache(int maximumItems)
		{
			if (maximumItems <= 0)
			{
				throw new ArgumentOutOfRangeException("maximumItems");
			}
			this._maximumItems = maximumItems;
			this._permanentCount = 0;
			this._begin = new SizeLimitedCache<K, V>.Node(default(K), default(V), false);
			this._end = new SizeLimitedCache<K, V>.Node(default(K), default(V), false);
			this._begin.Next = this._end;
			this._end.Previous = this._begin;
			this._nodeLookup = new Dictionary<K, SizeLimitedCache<K, V>.Node>();
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06004964 RID: 18788 RVA: 0x0011E45C File Offset: 0x0011D85C
		public int MaximumItems
		{
			get
			{
				return this._maximumItems;
			}
		}

		// Token: 0x06004965 RID: 18789 RVA: 0x0011E470 File Offset: 0x0011D870
		public void Add(K key, V resource, bool isPermanent)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (resource == null)
			{
				throw new ArgumentNullException("resource");
			}
			if (!this._nodeLookup.ContainsKey(key))
			{
				SizeLimitedCache<K, V>.Node node = new SizeLimitedCache<K, V>.Node(key, resource, isPermanent);
				if (!isPermanent)
				{
					if (this.IsFull())
					{
						this.RemoveOldest();
					}
					this.InsertAtEnd(node);
				}
				else
				{
					this._permanentCount++;
				}
				this._nodeLookup[key] = node;
				return;
			}
			SizeLimitedCache<K, V>.Node node2 = this._nodeLookup[key];
			if (!node2.IsPermanent)
			{
				this.RemoveFromList(node2);
			}
			if (!node2.IsPermanent && isPermanent)
			{
				this._permanentCount++;
			}
			else if (node2.IsPermanent && !isPermanent)
			{
				this._permanentCount--;
				if (this.IsFull())
				{
					this.RemoveOldest();
				}
			}
			node2.IsPermanent = isPermanent;
			node2.Resource = resource;
			if (!isPermanent)
			{
				this.InsertAtEnd(node2);
			}
		}

		// Token: 0x06004966 RID: 18790 RVA: 0x0011E568 File Offset: 0x0011D968
		public void Remove(K key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!this._nodeLookup.ContainsKey(key))
			{
				return;
			}
			SizeLimitedCache<K, V>.Node node = this._nodeLookup[key];
			this._nodeLookup.Remove(key);
			if (!node.IsPermanent)
			{
				this.RemoveFromList(node);
				return;
			}
			this._permanentCount--;
		}

		// Token: 0x06004967 RID: 18791 RVA: 0x0011E5D0 File Offset: 0x0011D9D0
		public V Get(K key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!this._nodeLookup.ContainsKey(key))
			{
				return default(V);
			}
			SizeLimitedCache<K, V>.Node node = this._nodeLookup[key];
			if (!node.IsPermanent)
			{
				this.RemoveFromList(node);
				this.InsertAtEnd(node);
			}
			return node.Resource;
		}

		// Token: 0x06004968 RID: 18792 RVA: 0x0011E634 File Offset: 0x0011DA34
		private void RemoveOldest()
		{
			SizeLimitedCache<K, V>.Node next = this._begin.Next;
			this._nodeLookup.Remove(next.Key);
			this.RemoveFromList(next);
		}

		// Token: 0x06004969 RID: 18793 RVA: 0x0011E668 File Offset: 0x0011DA68
		private void InsertAtEnd(SizeLimitedCache<K, V>.Node node)
		{
			node.Next = this._end;
			node.Previous = this._end.Previous;
			node.Previous.Next = node;
			this._end.Previous = node;
		}

		// Token: 0x0600496A RID: 18794 RVA: 0x0011E6AC File Offset: 0x0011DAAC
		private void RemoveFromList(SizeLimitedCache<K, V>.Node node)
		{
			node.Previous.Next = node.Next;
			node.Next.Previous = node.Previous;
		}

		// Token: 0x0600496B RID: 18795 RVA: 0x0011E6DC File Offset: 0x0011DADC
		private bool IsFull()
		{
			return this._nodeLookup.Count - this._permanentCount >= this._maximumItems;
		}

		// Token: 0x04001CD8 RID: 7384
		private int _maximumItems;

		// Token: 0x04001CD9 RID: 7385
		private int _permanentCount;

		// Token: 0x04001CDA RID: 7386
		private SizeLimitedCache<K, V>.Node _begin;

		// Token: 0x04001CDB RID: 7387
		private SizeLimitedCache<K, V>.Node _end;

		// Token: 0x04001CDC RID: 7388
		private Dictionary<K, SizeLimitedCache<K, V>.Node> _nodeLookup;

		// Token: 0x020009A7 RID: 2471
		private class Node
		{
			// Token: 0x06005A57 RID: 23127 RVA: 0x0016B434 File Offset: 0x0016A834
			public Node(K key, V resource, bool isPermanent)
			{
				this.Key = key;
				this.Resource = resource;
				this.IsPermanent = isPermanent;
			}

			// Token: 0x1700126F RID: 4719
			// (get) Token: 0x06005A58 RID: 23128 RVA: 0x0016B45C File Offset: 0x0016A85C
			// (set) Token: 0x06005A59 RID: 23129 RVA: 0x0016B470 File Offset: 0x0016A870
			public K Key
			{
				get
				{
					return this._key;
				}
				set
				{
					this._key = value;
				}
			}

			// Token: 0x17001270 RID: 4720
			// (get) Token: 0x06005A5A RID: 23130 RVA: 0x0016B484 File Offset: 0x0016A884
			// (set) Token: 0x06005A5B RID: 23131 RVA: 0x0016B498 File Offset: 0x0016A898
			public V Resource
			{
				get
				{
					return this._resource;
				}
				set
				{
					this._resource = value;
				}
			}

			// Token: 0x17001271 RID: 4721
			// (get) Token: 0x06005A5C RID: 23132 RVA: 0x0016B4AC File Offset: 0x0016A8AC
			// (set) Token: 0x06005A5D RID: 23133 RVA: 0x0016B4C0 File Offset: 0x0016A8C0
			public bool IsPermanent
			{
				get
				{
					return this._isPermanent;
				}
				set
				{
					this._isPermanent = value;
				}
			}

			// Token: 0x17001272 RID: 4722
			// (get) Token: 0x06005A5E RID: 23134 RVA: 0x0016B4D4 File Offset: 0x0016A8D4
			// (set) Token: 0x06005A5F RID: 23135 RVA: 0x0016B4E8 File Offset: 0x0016A8E8
			public SizeLimitedCache<K, V>.Node Next
			{
				get
				{
					return this._next;
				}
				set
				{
					this._next = value;
				}
			}

			// Token: 0x17001273 RID: 4723
			// (get) Token: 0x06005A60 RID: 23136 RVA: 0x0016B4FC File Offset: 0x0016A8FC
			// (set) Token: 0x06005A61 RID: 23137 RVA: 0x0016B510 File Offset: 0x0016A910
			public SizeLimitedCache<K, V>.Node Previous
			{
				get
				{
					return this._previous;
				}
				set
				{
					this._previous = value;
				}
			}

			// Token: 0x04002D89 RID: 11657
			private V _resource;

			// Token: 0x04002D8A RID: 11658
			private K _key;

			// Token: 0x04002D8B RID: 11659
			private bool _isPermanent;

			// Token: 0x04002D8C RID: 11660
			private SizeLimitedCache<K, V>.Node _next;

			// Token: 0x04002D8D RID: 11661
			private SizeLimitedCache<K, V>.Node _previous;
		}
	}
}
