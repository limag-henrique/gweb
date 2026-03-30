using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Windows;
using System.Windows.Media;
using MS.Internal.PresentationCore;
using MS.Internal.Text.TextInterface;

namespace MS.Internal.FontFace
{
	// Token: 0x0200076E RID: 1902
	internal struct TypefaceCollection : ICollection<Typeface>, IEnumerable<Typeface>, IEnumerable
	{
		// Token: 0x0600502E RID: 20526 RVA: 0x00140D58 File Offset: 0x00140158
		public TypefaceCollection(System.Windows.Media.FontFamily fontFamily, MS.Internal.Text.TextInterface.FontFamily family)
		{
			this._fontFamily = fontFamily;
			this._family = family;
			this._familyTypefaceCollection = null;
		}

		// Token: 0x0600502F RID: 20527 RVA: 0x00140D7C File Offset: 0x0014017C
		public TypefaceCollection(System.Windows.Media.FontFamily fontFamily, FamilyTypefaceCollection familyTypefaceCollection)
		{
			this._fontFamily = fontFamily;
			this._familyTypefaceCollection = familyTypefaceCollection;
			this._family = null;
		}

		// Token: 0x06005030 RID: 20528 RVA: 0x00140DA0 File Offset: 0x001401A0
		public void Add(Typeface item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005031 RID: 20529 RVA: 0x00140DB4 File Offset: 0x001401B4
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005032 RID: 20530 RVA: 0x00140DC8 File Offset: 0x001401C8
		public bool Contains(Typeface item)
		{
			foreach (Typeface typeface in this)
			{
				if (typeface.Equals(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005033 RID: 20531 RVA: 0x00140E28 File Offset: 0x00140228
		public void CopyTo(Typeface[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(SR.Get("Collection_BadRank"));
			}
			if (arrayIndex < 0 || arrayIndex >= array.Length || arrayIndex + this.Count > array.Length)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			foreach (Typeface typeface in this)
			{
				array[arrayIndex++] = typeface;
			}
		}

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x06005034 RID: 20532 RVA: 0x00140EC8 File Offset: 0x001402C8
		public int Count
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				if (this._family != null)
				{
					return checked((int)this._family.Count);
				}
				return this._familyTypefaceCollection.Count;
			}
		}

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x06005035 RID: 20533 RVA: 0x00140EF8 File Offset: 0x001402F8
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005036 RID: 20534 RVA: 0x00140F08 File Offset: 0x00140308
		public bool Remove(Typeface item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005037 RID: 20535 RVA: 0x00140F1C File Offset: 0x0014031C
		public IEnumerator<Typeface> GetEnumerator()
		{
			return new TypefaceCollection.Enumerator(this);
		}

		// Token: 0x06005038 RID: 20536 RVA: 0x00140F3C File Offset: 0x0014033C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new TypefaceCollection.Enumerator(this);
		}

		// Token: 0x04002478 RID: 9336
		private System.Windows.Media.FontFamily _fontFamily;

		// Token: 0x04002479 RID: 9337
		private MS.Internal.Text.TextInterface.FontFamily _family;

		// Token: 0x0400247A RID: 9338
		private FamilyTypefaceCollection _familyTypefaceCollection;

		// Token: 0x020009EE RID: 2542
		private struct Enumerator : IEnumerator<Typeface>, IDisposable, IEnumerator
		{
			// Token: 0x06005BA9 RID: 23465 RVA: 0x00170734 File Offset: 0x0016FB34
			public Enumerator(TypefaceCollection typefaceCollection)
			{
				this._typefaceCollection = typefaceCollection;
				if (typefaceCollection._family != null)
				{
					this._familyEnumerator = ((IEnumerable<Font>)typefaceCollection._family).GetEnumerator();
					this._familyTypefaceEnumerator = null;
					return;
				}
				this._familyTypefaceEnumerator = ((IEnumerable<FamilyTypeface>)typefaceCollection._familyTypefaceCollection).GetEnumerator();
				this._familyEnumerator = null;
			}

			// Token: 0x170012BD RID: 4797
			// (get) Token: 0x06005BAA RID: 23466 RVA: 0x00170784 File Offset: 0x0016FB84
			public Typeface Current
			{
				[SecurityTreatAsSafe]
				[SecurityCritical]
				get
				{
					if (this._typefaceCollection._family != null)
					{
						Font font = this._familyEnumerator.Current;
						return new Typeface(this._typefaceCollection._fontFamily, new System.Windows.FontStyle((int)font.Style), new System.Windows.FontWeight((int)font.Weight), new System.Windows.FontStretch((int)font.Stretch));
					}
					FamilyTypeface familyTypeface = this._familyTypefaceEnumerator.Current;
					return new Typeface(this._typefaceCollection._fontFamily, familyTypeface.Style, familyTypeface.Weight, familyTypeface.Stretch);
				}
			}

			// Token: 0x06005BAB RID: 23467 RVA: 0x0017080C File Offset: 0x0016FC0C
			public void Dispose()
			{
			}

			// Token: 0x170012BE RID: 4798
			// (get) Token: 0x06005BAC RID: 23468 RVA: 0x0017081C File Offset: 0x0016FC1C
			object IEnumerator.Current
			{
				get
				{
					return ((IEnumerator<Typeface>)this).Current;
				}
			}

			// Token: 0x06005BAD RID: 23469 RVA: 0x0017083C File Offset: 0x0016FC3C
			public bool MoveNext()
			{
				if (this._familyEnumerator != null)
				{
					return this._familyEnumerator.MoveNext();
				}
				return this._familyTypefaceEnumerator.MoveNext();
			}

			// Token: 0x06005BAE RID: 23470 RVA: 0x00170868 File Offset: 0x0016FC68
			public void Reset()
			{
				if (this._typefaceCollection._family != null)
				{
					this._familyEnumerator = ((IEnumerable<Font>)this._typefaceCollection._family).GetEnumerator();
					this._familyTypefaceEnumerator = null;
					return;
				}
				this._familyTypefaceEnumerator = ((IEnumerable<FamilyTypeface>)this._typefaceCollection._familyTypefaceCollection).GetEnumerator();
				this._familyEnumerator = null;
			}

			// Token: 0x04002EF8 RID: 12024
			private IEnumerator<Font> _familyEnumerator;

			// Token: 0x04002EF9 RID: 12025
			private IEnumerator<FamilyTypeface> _familyTypefaceEnumerator;

			// Token: 0x04002EFA RID: 12026
			private TypefaceCollection _typefaceCollection;
		}
	}
}
