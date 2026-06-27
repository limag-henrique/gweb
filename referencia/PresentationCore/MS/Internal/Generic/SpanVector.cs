using System;
using System.Collections;
using System.Collections.Generic;
using MS.Utility;

namespace MS.Internal.Generic
{
	// Token: 0x020006A8 RID: 1704
	internal struct SpanVector<T> : IEnumerable<Span<T>>, IEnumerable
	{
		// Token: 0x06004A96 RID: 19094 RVA: 0x00122CE0 File Offset: 0x001220E0
		internal SpanVector(T defaultValue)
		{
			this = new SpanVector<T>(defaultValue, default(FrugalStructList<Span<T>>));
		}

		// Token: 0x06004A97 RID: 19095 RVA: 0x00122D00 File Offset: 0x00122100
		private SpanVector(T defaultValue, FrugalStructList<Span<T>> spanList)
		{
			this._defaultValue = defaultValue;
			this._spanList = spanList;
		}

		// Token: 0x06004A98 RID: 19096 RVA: 0x00122D1C File Offset: 0x0012211C
		public IEnumerator<Span<T>> GetEnumerator()
		{
			return new SpanVector<T>.SpanEnumerator<T>(this);
		}

		// Token: 0x06004A99 RID: 19097 RVA: 0x00122D3C File Offset: 0x0012213C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06004A9A RID: 19098 RVA: 0x00122D50 File Offset: 0x00122150
		private void Add(Span<T> span)
		{
			this._spanList.Add(span);
		}

		// Token: 0x06004A9B RID: 19099 RVA: 0x00122D6C File Offset: 0x0012216C
		internal void Delete(int index, int count)
		{
			for (int i = index + count - 1; i >= index; i--)
			{
				this._spanList.RemoveAt(i);
			}
		}

		// Token: 0x06004A9C RID: 19100 RVA: 0x00122D98 File Offset: 0x00122198
		private void Insert(int index, int count)
		{
			for (int i = 0; i < count; i++)
			{
				this._spanList.Insert(index, default(Span<T>));
			}
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x00122DC8 File Offset: 0x001221C8
		internal void Set(int first, int length, T value)
		{
			int num = 0;
			int num2 = 0;
			while (num < this.Count && num2 + this._spanList[num].Length <= first)
			{
				num2 += this._spanList[num].Length;
				num++;
			}
			if (num >= this.Count)
			{
				if (num2 < first)
				{
					this.Add(new Span<T>(this._defaultValue, first - num2));
				}
				if (this.Count > 0)
				{
					Span<T> span = this._spanList[this.Count - 1];
					if (span.Value.Equals(value))
					{
						Span<T> span2 = this._spanList[this.Count - 1];
						this._spanList[this.Count - 1] = new Span<T>(span2.Value, span2.Length + length);
						return;
					}
				}
				this.Add(new Span<T>(value, length));
				return;
			}
			int num3 = num;
			int num4 = num2;
			while (num3 < this.Count && num4 + this._spanList[num3].Length <= first + length)
			{
				num4 += this._spanList[num3].Length;
				num3++;
			}
			if (first == num2)
			{
				if (num > 0)
				{
					Span<T> span = this._spanList[num - 1];
					if (span.Value.Equals(value))
					{
						num--;
						num2 -= this._spanList[num].Length;
						first = num2;
						length += this._spanList[num].Length;
					}
				}
			}
			else
			{
				Span<T> span = this._spanList[num];
				if (span.Value.Equals(value))
				{
					length = first + length - num2;
					first = num2;
				}
			}
			if (num3 < this.Count)
			{
				Span<T> span = this._spanList[num3];
				if (span.Value.Equals(value))
				{
					length = num4 + this._spanList[num3].Length - first;
					num4 += this._spanList[num3].Length;
					num3++;
				}
			}
			if (num3 < this.Count)
			{
				Span<T> span = default(Span<T>);
				T value2 = span.Value;
				int length2 = 0;
				if (first + length > num4)
				{
					value2 = this._spanList[num3].Value;
					length2 = num4 + this._spanList[num3].Length - (first + length);
				}
				int num5 = 1 + ((first > num2) ? 1 : 0) - (num3 - num);
				if (num5 < 0)
				{
					this.Delete(num + 1, -num5);
				}
				else if (num5 > 0)
				{
					this.Insert(num + 1, num5);
					for (int i = 0; i < num5; i++)
					{
						int index = num + 1 + i;
						span = default(Span<T>);
						this._spanList[index] = span;
					}
				}
				if (num2 < first)
				{
					Span<T> span3 = this._spanList[num];
					this._spanList[num] = new Span<T>(span3.Value, first - num2);
					num++;
				}
				this._spanList[num] = new Span<T>(value, length);
				num++;
				if (num4 < first + length)
				{
					this._spanList[num] = new Span<T>(value2, length2);
				}
				return;
			}
			if (num2 < first)
			{
				if (this.Count != num + 2 && !this.Resize(num + 2))
				{
					throw new OutOfMemoryException();
				}
				Span<T> span4 = this._spanList[num];
				this._spanList[num] = new Span<T>(span4.Value, first - num2);
				this._spanList[num + 1] = new Span<T>(value, length);
				return;
			}
			else
			{
				if (this.Count != num + 1 && !this.Resize(num + 1))
				{
					throw new OutOfMemoryException();
				}
				this._spanList[num] = new Span<T>(value, length);
				return;
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06004A9E RID: 19102 RVA: 0x00123198 File Offset: 0x00122598
		internal int Count
		{
			get
			{
				return this._spanList.Count;
			}
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06004A9F RID: 19103 RVA: 0x001231B0 File Offset: 0x001225B0
		internal T DefaultValue
		{
			get
			{
				return this._defaultValue;
			}
		}

		// Token: 0x17000F70 RID: 3952
		internal Span<T> this[int index]
		{
			get
			{
				return this._spanList[index];
			}
		}

		// Token: 0x06004AA1 RID: 19105 RVA: 0x001231E0 File Offset: 0x001225E0
		private bool Resize(int targetCount)
		{
			if (targetCount > this.Count)
			{
				for (int i = 0; i < targetCount - this.Count; i++)
				{
					this._spanList.Add(default(Span<T>));
				}
			}
			else if (targetCount < this.Count)
			{
				this.Delete(targetCount, this.Count - targetCount);
			}
			return true;
		}

		// Token: 0x04001F94 RID: 8084
		private FrugalStructList<Span<T>> _spanList;

		// Token: 0x04001F95 RID: 8085
		private T _defaultValue;

		// Token: 0x020009B6 RID: 2486
		private struct SpanEnumerator<U> : IEnumerator<Span<U>>, IDisposable, IEnumerator
		{
			// Token: 0x06005AA6 RID: 23206 RVA: 0x0016BFF8 File Offset: 0x0016B3F8
			internal SpanEnumerator(SpanVector<U> vector)
			{
				this._vector = vector;
				this._current = -1;
			}

			// Token: 0x06005AA7 RID: 23207 RVA: 0x0016C014 File Offset: 0x0016B414
			void IDisposable.Dispose()
			{
			}

			// Token: 0x17001280 RID: 4736
			// (get) Token: 0x06005AA8 RID: 23208 RVA: 0x0016C024 File Offset: 0x0016B424
			public Span<U> Current
			{
				get
				{
					return this._vector[this._current];
				}
			}

			// Token: 0x17001281 RID: 4737
			// (get) Token: 0x06005AA9 RID: 23209 RVA: 0x0016C044 File Offset: 0x0016B444
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06005AAA RID: 23210 RVA: 0x0016C05C File Offset: 0x0016B45C
			public bool MoveNext()
			{
				this._current++;
				return this._current < this._vector.Count;
			}

			// Token: 0x06005AAB RID: 23211 RVA: 0x0016C08C File Offset: 0x0016B48C
			public void Reset()
			{
				this._current = -1;
			}

			// Token: 0x04002DAB RID: 11691
			private SpanVector<U> _vector;

			// Token: 0x04002DAC RID: 11692
			private int _current;
		}
	}
}
