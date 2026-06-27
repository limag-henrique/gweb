using System;
using System.Security;
using MS.Internal.FontCache;

namespace MS.Internal.FontFace
{
	// Token: 0x02000768 RID: 1896
	internal struct FontFamilyIdentifier
	{
		// Token: 0x06004FF7 RID: 20471 RVA: 0x00140218 File Offset: 0x0013F618
		internal FontFamilyIdentifier(string friendlyName, Uri baseUri)
		{
			this._friendlyName = friendlyName;
			this._baseUri = baseUri;
			this._tokenCount = ((friendlyName != null) ? -1 : 0);
			this._canonicalReferences = null;
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x00140248 File Offset: 0x0013F648
		internal FontFamilyIdentifier(FontFamilyIdentifier first, FontFamilyIdentifier second)
		{
			first.Canonicalize();
			second.Canonicalize();
			this._friendlyName = null;
			this._tokenCount = first._tokenCount + second._tokenCount;
			this._baseUri = null;
			if (first._tokenCount == 0)
			{
				this._canonicalReferences = second._canonicalReferences;
				return;
			}
			if (second._tokenCount == 0)
			{
				this._canonicalReferences = first._canonicalReferences;
				return;
			}
			this._canonicalReferences = new CanonicalFontFamilyReference[this._tokenCount];
			int num = 0;
			foreach (CanonicalFontFamilyReference canonicalFontFamilyReference in first._canonicalReferences)
			{
				this._canonicalReferences[num++] = canonicalFontFamilyReference;
			}
			foreach (CanonicalFontFamilyReference canonicalFontFamilyReference2 in second._canonicalReferences)
			{
				this._canonicalReferences[num++] = canonicalFontFamilyReference2;
			}
		}

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x06004FF9 RID: 20473 RVA: 0x00140314 File Offset: 0x0013F714
		internal string Source
		{
			get
			{
				return this._friendlyName;
			}
		}

		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x06004FFA RID: 20474 RVA: 0x00140328 File Offset: 0x0013F728
		internal Uri BaseUri
		{
			get
			{
				return this._baseUri;
			}
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x0014033C File Offset: 0x0013F73C
		public bool Equals(FontFamilyIdentifier other)
		{
			if (this._friendlyName == other._friendlyName && this._baseUri == other._baseUri)
			{
				return true;
			}
			int count = this.Count;
			if (other.Count != count)
			{
				return false;
			}
			if (count != 0)
			{
				this.Canonicalize();
				other.Canonicalize();
				for (int i = 0; i < count; i++)
				{
					if (!this._canonicalReferences[i].Equals(other._canonicalReferences[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06004FFC RID: 20476 RVA: 0x001403BC File Offset: 0x0013F7BC
		public override bool Equals(object obj)
		{
			return obj is FontFamilyIdentifier && this.Equals((FontFamilyIdentifier)obj);
		}

		// Token: 0x06004FFD RID: 20477 RVA: 0x001403E0 File Offset: 0x0013F7E0
		public override int GetHashCode()
		{
			int hash = 1;
			if (this.Count != 0)
			{
				this.Canonicalize();
				foreach (CanonicalFontFamilyReference canonicalFontFamilyReference in this._canonicalReferences)
				{
					hash = HashFn.HashMultiply(hash) + canonicalFontFamilyReference.GetHashCode();
				}
			}
			return HashFn.HashScramble(hash);
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x06004FFE RID: 20478 RVA: 0x0014042C File Offset: 0x0013F82C
		internal int Count
		{
			get
			{
				if (this._tokenCount < 0)
				{
					this._tokenCount = FontFamilyIdentifier.CountTokens(this._friendlyName);
				}
				return this._tokenCount;
			}
		}

		// Token: 0x1700109C RID: 4252
		internal CanonicalFontFamilyReference this[int tokenIndex]
		{
			get
			{
				if (tokenIndex < 0 || tokenIndex >= this.Count)
				{
					throw new ArgumentOutOfRangeException("tokenIndex");
				}
				if (this._canonicalReferences != null)
				{
					return this._canonicalReferences[tokenIndex];
				}
				int startIndex;
				int length;
				int i = FontFamilyIdentifier.FindToken(this._friendlyName, 0, out startIndex, out length);
				for (int j = 0; j < tokenIndex; j++)
				{
					i = FontFamilyIdentifier.FindToken(this._friendlyName, i, out startIndex, out length);
				}
				return this.GetCanonicalReference(startIndex, length);
			}
		}

		// Token: 0x06005000 RID: 20480 RVA: 0x001404C8 File Offset: 0x0013F8C8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal void Canonicalize()
		{
			if (this._canonicalReferences != null)
			{
				return;
			}
			int count = this.Count;
			if (count == 0)
			{
				return;
			}
			FontFamilyIdentifier.BasedFriendlyName key = new FontFamilyIdentifier.BasedFriendlyName(this._baseUri, this._friendlyName);
			CanonicalFontFamilyReference[] array = TypefaceMetricsCache.ReadonlyLookup(key) as CanonicalFontFamilyReference[];
			if (array == null)
			{
				array = new CanonicalFontFamilyReference[count];
				int startIndex;
				int length;
				int i = FontFamilyIdentifier.FindToken(this._friendlyName, 0, out startIndex, out length);
				array[0] = this.GetCanonicalReference(startIndex, length);
				for (int j = 1; j < count; j++)
				{
					i = FontFamilyIdentifier.FindToken(this._friendlyName, i, out startIndex, out length);
					array[j] = this.GetCanonicalReference(startIndex, length);
				}
				TypefaceMetricsCache.Add(key, array);
			}
			this._canonicalReferences = array;
		}

		// Token: 0x06005001 RID: 20481 RVA: 0x0014056C File Offset: 0x0013F96C
		private static int CountTokens(string friendlyName)
		{
			int num = 0;
			int num3;
			int num4;
			int num2 = FontFamilyIdentifier.FindToken(friendlyName, 0, out num3, out num4);
			while (num2 >= 0 && ++num != 32)
			{
				num2 = FontFamilyIdentifier.FindToken(friendlyName, num2, out num3, out num4);
			}
			return num;
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x001405A4 File Offset: 0x0013F9A4
		private static int FindToken(string friendlyName, int i, out int tokenIndex, out int tokenLength)
		{
			int length = friendlyName.Length;
			while (i < length)
			{
				while (i < length && char.IsWhiteSpace(friendlyName[i]))
				{
					i++;
				}
				int num = i;
				while (i < length)
				{
					if (friendlyName[i] == ',')
					{
						if (i + 1 >= length || friendlyName[i + 1] != ',')
						{
							break;
						}
						i += 2;
					}
					else
					{
						if (friendlyName[i] == '\0')
						{
							break;
						}
						i++;
					}
				}
				int num2 = i;
				while (num2 > num && char.IsWhiteSpace(friendlyName[num2 - 1]))
				{
					num2--;
				}
				if (num < num2)
				{
					tokenIndex = num;
					tokenLength = num2 - num;
					return i + 1;
				}
				i++;
			}
			tokenIndex = length;
			tokenLength = 0;
			return -1;
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x00140650 File Offset: 0x0013FA50
		private CanonicalFontFamilyReference GetCanonicalReference(int startIndex, int length)
		{
			string normalizedFontFamilyReference = Util.GetNormalizedFontFamilyReference(this._friendlyName, startIndex, length);
			FontFamilyIdentifier.BasedNormalizedName key = new FontFamilyIdentifier.BasedNormalizedName(this._baseUri, normalizedFontFamilyReference);
			CanonicalFontFamilyReference canonicalFontFamilyReference = TypefaceMetricsCache.ReadonlyLookup(key) as CanonicalFontFamilyReference;
			if (canonicalFontFamilyReference == null)
			{
				canonicalFontFamilyReference = CanonicalFontFamilyReference.Create(this._baseUri, normalizedFontFamilyReference);
				TypefaceMetricsCache.Add(key, canonicalFontFamilyReference);
			}
			return canonicalFontFamilyReference;
		}

		// Token: 0x0400246C RID: 9324
		private string _friendlyName;

		// Token: 0x0400246D RID: 9325
		private Uri _baseUri;

		// Token: 0x0400246E RID: 9326
		private int _tokenCount;

		// Token: 0x0400246F RID: 9327
		private CanonicalFontFamilyReference[] _canonicalReferences;

		// Token: 0x04002470 RID: 9328
		internal const char FamilyNameDelimiter = ',';

		// Token: 0x04002471 RID: 9329
		internal const int MaxFamilyNamePerFamilyMapTarget = 32;

		// Token: 0x020009E8 RID: 2536
		private sealed class BasedFriendlyName : FontFamilyIdentifier.BasedName
		{
			// Token: 0x06005B8E RID: 23438 RVA: 0x0017037C File Offset: 0x0016F77C
			public BasedFriendlyName(Uri baseUri, string name) : base(baseUri, name)
			{
			}

			// Token: 0x06005B8F RID: 23439 RVA: 0x00170394 File Offset: 0x0016F794
			public override int GetHashCode()
			{
				return base.InternalGetHashCode(1);
			}

			// Token: 0x06005B90 RID: 23440 RVA: 0x001703A8 File Offset: 0x0016F7A8
			public override bool Equals(object obj)
			{
				return base.InternalEquals(obj as FontFamilyIdentifier.BasedFriendlyName);
			}
		}

		// Token: 0x020009E9 RID: 2537
		private sealed class BasedNormalizedName : FontFamilyIdentifier.BasedName
		{
			// Token: 0x06005B91 RID: 23441 RVA: 0x001703C4 File Offset: 0x0016F7C4
			public BasedNormalizedName(Uri baseUri, string name) : base(baseUri, name)
			{
			}

			// Token: 0x06005B92 RID: 23442 RVA: 0x001703DC File Offset: 0x0016F7DC
			public override int GetHashCode()
			{
				return base.InternalGetHashCode(int.MaxValue);
			}

			// Token: 0x06005B93 RID: 23443 RVA: 0x001703F4 File Offset: 0x0016F7F4
			public override bool Equals(object obj)
			{
				return base.InternalEquals(obj as FontFamilyIdentifier.BasedNormalizedName);
			}
		}

		// Token: 0x020009EA RID: 2538
		private abstract class BasedName
		{
			// Token: 0x06005B94 RID: 23444 RVA: 0x00170410 File Offset: 0x0016F810
			protected BasedName(Uri baseUri, string name)
			{
				this._baseUri = baseUri;
				this._name = name;
			}

			// Token: 0x06005B95 RID: 23445
			public abstract override int GetHashCode();

			// Token: 0x06005B96 RID: 23446
			public abstract override bool Equals(object obj);

			// Token: 0x06005B97 RID: 23447 RVA: 0x00170434 File Offset: 0x0016F834
			protected int InternalGetHashCode(int seed)
			{
				int num = seed;
				if (this._baseUri != null)
				{
					num += HashFn.HashMultiply(this._baseUri.GetHashCode());
				}
				if (this._name != null)
				{
					num = HashFn.HashMultiply(num) + this._name.GetHashCode();
				}
				return HashFn.HashScramble(num);
			}

			// Token: 0x06005B98 RID: 23448 RVA: 0x00170488 File Offset: 0x0016F888
			protected bool InternalEquals(FontFamilyIdentifier.BasedName other)
			{
				return other != null && other._baseUri == this._baseUri && other._name == this._name;
			}

			// Token: 0x04002EF0 RID: 12016
			private Uri _baseUri;

			// Token: 0x04002EF1 RID: 12017
			private string _name;
		}
	}
}
