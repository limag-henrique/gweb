using System;
using System.Collections;
using MS.Utility;

namespace MS.Internal
{
	// Token: 0x02000692 RID: 1682
	internal class SpanVector : IEnumerable
	{
		// Token: 0x060049ED RID: 18925 RVA: 0x0011F9E4 File Offset: 0x0011EDE4
		public SpanVector(object defaultObject) : this(defaultObject, default(FrugalStructList<Span>))
		{
		}

		// Token: 0x060049EE RID: 18926 RVA: 0x0011FA04 File Offset: 0x0011EE04
		internal SpanVector(object defaultObject, FrugalStructList<Span> spans)
		{
			this._defaultObject = defaultObject;
			this._spans = spans;
		}

		// Token: 0x060049EF RID: 18927 RVA: 0x0011FA28 File Offset: 0x0011EE28
		public IEnumerator GetEnumerator()
		{
			return new SpanEnumerator(this);
		}

		// Token: 0x060049F0 RID: 18928 RVA: 0x0011FA3C File Offset: 0x0011EE3C
		private void Add(Span span)
		{
			this._spans.Add(span);
		}

		// Token: 0x060049F1 RID: 18929 RVA: 0x0011FA58 File Offset: 0x0011EE58
		internal virtual void Delete(int index, int count, ref SpanPosition latestPosition)
		{
			this.DeleteInternal(index, count);
			if (index <= latestPosition.Index)
			{
				latestPosition = default(SpanPosition);
			}
		}

		// Token: 0x060049F2 RID: 18930 RVA: 0x0011FA80 File Offset: 0x0011EE80
		private void DeleteInternal(int index, int count)
		{
			for (int i = index + count - 1; i >= index; i--)
			{
				this._spans.RemoveAt(i);
			}
		}

		// Token: 0x060049F3 RID: 18931 RVA: 0x0011FAAC File Offset: 0x0011EEAC
		private void Insert(int index, int count)
		{
			for (int i = 0; i < count; i++)
			{
				this._spans.Insert(index, new Span(null, 0));
			}
		}

		// Token: 0x060049F4 RID: 18932 RVA: 0x0011FAD8 File Offset: 0x0011EED8
		internal bool FindSpan(int cp, SpanPosition latestPosition, out SpanPosition spanPosition)
		{
			int count = this._spans.Count;
			int i;
			int j;
			if (cp == 0)
			{
				i = 0;
				j = 0;
			}
			else if (cp >= latestPosition.CP || cp * 2 < latestPosition.CP)
			{
				if (cp >= latestPosition.CP)
				{
					i = latestPosition.Index;
					j = latestPosition.CP;
				}
				else
				{
					i = 0;
					j = 0;
				}
				while (i < count)
				{
					int length = this._spans[i].length;
					if (cp < j + length)
					{
						break;
					}
					j += length;
					i++;
				}
			}
			else
			{
				i = latestPosition.Index;
				for (j = latestPosition.CP; j > cp; j -= this._spans[--i].length)
				{
				}
			}
			spanPosition = new SpanPosition(i, j);
			return i != count;
		}

		// Token: 0x060049F5 RID: 18933 RVA: 0x0011FBA0 File Offset: 0x0011EFA0
		public void SetValue(int first, int length, object element)
		{
			this.Set(first, length, element, SpanVector._equals, default(SpanPosition));
		}

		// Token: 0x060049F6 RID: 18934 RVA: 0x0011FBC8 File Offset: 0x0011EFC8
		public SpanPosition SetValue(int first, int length, object element, SpanPosition spanPosition)
		{
			return this.Set(first, length, element, SpanVector._equals, spanPosition);
		}

		// Token: 0x060049F7 RID: 18935 RVA: 0x0011FBE8 File Offset: 0x0011EFE8
		public void SetReference(int first, int length, object element)
		{
			this.Set(first, length, element, SpanVector._referenceEquals, default(SpanPosition));
		}

		// Token: 0x060049F8 RID: 18936 RVA: 0x0011FC10 File Offset: 0x0011F010
		public SpanPosition SetReference(int first, int length, object element, SpanPosition spanPosition)
		{
			return this.Set(first, length, element, SpanVector._referenceEquals, spanPosition);
		}

		// Token: 0x060049F9 RID: 18937 RVA: 0x0011FC30 File Offset: 0x0011F030
		private SpanPosition Set(int first, int length, object element, Equals equals, SpanPosition spanPosition)
		{
			bool flag = this.FindSpan(first, spanPosition, out spanPosition);
			int num = spanPosition.Index;
			int num2 = spanPosition.CP;
			if (!flag)
			{
				if (num2 < first)
				{
					this.Add(new Span(this._defaultObject, first - num2));
				}
				if (this.Count > 0 && equals(this._spans[this.Count - 1].element, element))
				{
					this._spans[this.Count - 1].length += length;
					if (num == this.Count)
					{
						num2 += length;
					}
				}
				else
				{
					this.Add(new Span(element, length));
				}
			}
			else
			{
				int num3 = num;
				int num4 = num2;
				while (num3 < this.Count && num4 + this._spans[num3].length <= first + length)
				{
					num4 += this._spans[num3].length;
					num3++;
				}
				if (first == num2)
				{
					if (num > 0 && equals(this._spans[num - 1].element, element))
					{
						num--;
						num2 -= this._spans[num].length;
						first = num2;
						length += this._spans[num].length;
					}
				}
				else if (equals(this._spans[num].element, element))
				{
					length = first + length - num2;
					first = num2;
				}
				if (num3 < this.Count && equals(this._spans[num3].element, element))
				{
					length = num4 + this._spans[num3].length - first;
					num4 += this._spans[num3].length;
					num3++;
				}
				if (num3 >= this.Count)
				{
					if (num2 < first)
					{
						if (this.Count != num + 2 && !this.Resize(num + 2))
						{
							throw new OutOfMemoryException();
						}
						this._spans[num].length = first - num2;
						this._spans[num + 1] = new Span(element, length);
					}
					else
					{
						if (this.Count != num + 1 && !this.Resize(num + 1))
						{
							throw new OutOfMemoryException();
						}
						this._spans[num] = new Span(element, length);
					}
				}
				else
				{
					object element2 = null;
					int length2 = 0;
					if (first + length > num4)
					{
						element2 = this._spans[num3].element;
						length2 = num4 + this._spans[num3].length - (first + length);
					}
					int num5 = 1 + ((first > num2) ? 1 : 0) - (num3 - num);
					if (num5 < 0)
					{
						this.DeleteInternal(num + 1, -num5);
					}
					else if (num5 > 0)
					{
						this.Insert(num + 1, num5);
						for (int i = 0; i < num5; i++)
						{
							this._spans[num + 1 + i] = new Span(null, 0);
						}
					}
					if (num2 < first)
					{
						this._spans[num].length = first - num2;
						num++;
						num2 = first;
					}
					this._spans[num] = new Span(element, length);
					num++;
					num2 += length;
					if (num4 < first + length)
					{
						this._spans[num] = new Span(element2, length2);
					}
				}
			}
			return new SpanPosition(num, num2);
		}

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x060049FA RID: 18938 RVA: 0x0011FF7C File Offset: 0x0011F37C
		public int Count
		{
			get
			{
				return this._spans.Count;
			}
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x060049FB RID: 18939 RVA: 0x0011FF94 File Offset: 0x0011F394
		public object Default
		{
			get
			{
				return this._defaultObject;
			}
		}

		// Token: 0x17000F53 RID: 3923
		public Span this[int index]
		{
			get
			{
				return this._spans[index];
			}
		}

		// Token: 0x060049FD RID: 18941 RVA: 0x0011FFC4 File Offset: 0x0011F3C4
		private bool Resize(int targetCount)
		{
			if (targetCount > this.Count)
			{
				for (int i = 0; i < targetCount - this.Count; i++)
				{
					this._spans.Add(new Span(null, 0));
				}
			}
			else if (targetCount < this.Count)
			{
				this.DeleteInternal(targetCount, this.Count - targetCount);
			}
			return true;
		}

		// Token: 0x04001EE5 RID: 7909
		private static Equals _referenceEquals = new Equals(object.ReferenceEquals);

		// Token: 0x04001EE6 RID: 7910
		private static Equals _equals = new Equals(object.Equals);

		// Token: 0x04001EE7 RID: 7911
		private FrugalStructList<Span> _spans;

		// Token: 0x04001EE8 RID: 7912
		private object _defaultObject;
	}
}
