using System;
using System.Security;

namespace MS.Internal.FontCache
{
	// Token: 0x02000772 RID: 1906
	internal sealed class CanonicalFontFamilyReference
	{
		// Token: 0x0600504F RID: 20559 RVA: 0x00141374 File Offset: 0x00140774
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public static CanonicalFontFamilyReference Create(Uri baseUri, string normalizedString)
		{
			string text;
			string stringToUnescape;
			if (CanonicalFontFamilyReference.SplitFontFamilyReference(normalizedString, out text, out stringToUnescape))
			{
				Uri uri = null;
				string text2 = null;
				bool flag = false;
				if (text == null || Util.IsReferenceToWindowsFonts(text))
				{
					text2 = text;
					flag = true;
				}
				else if (Uri.TryCreate(text, UriKind.Absolute, out uri))
				{
					flag = Util.IsSupportedSchemeForAbsoluteFontFamilyUri(uri);
				}
				else if (baseUri != null && Util.IsEnumerableFontUriScheme(baseUri))
				{
					flag = Uri.TryCreate(baseUri, text, out uri);
				}
				if (flag)
				{
					string familyName = Uri.UnescapeDataString(stringToUnescape);
					if (text2 != null)
					{
						return new CanonicalFontFamilyReference(text2, familyName);
					}
					return new CanonicalFontFamilyReference(uri, familyName);
				}
			}
			return CanonicalFontFamilyReference._unresolved;
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x06005050 RID: 20560 RVA: 0x001413FC File Offset: 0x001407FC
		public static CanonicalFontFamilyReference Unresolved
		{
			get
			{
				return CanonicalFontFamilyReference._unresolved;
			}
		}

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x06005051 RID: 20561 RVA: 0x00141410 File Offset: 0x00140810
		public string FamilyName
		{
			get
			{
				return this._familyName;
			}
		}

		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x06005052 RID: 20562 RVA: 0x00141424 File Offset: 0x00140824
		// (set) Token: 0x06005053 RID: 20563 RVA: 0x00141438 File Offset: 0x00140838
		public string EscapedFileName { get; [SecurityCritical] private set; }

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x06005054 RID: 20564 RVA: 0x0014144C File Offset: 0x0014084C
		public Uri LocationUri
		{
			get
			{
				return this._absoluteLocationUri;
			}
		}

		// Token: 0x06005055 RID: 20565 RVA: 0x00141460 File Offset: 0x00140860
		public bool Equals(CanonicalFontFamilyReference other)
		{
			return other != null && other._absoluteLocationUri == this._absoluteLocationUri && other.EscapedFileName == this.EscapedFileName && other._familyName == this._familyName;
		}

		// Token: 0x06005056 RID: 20566 RVA: 0x001414AC File Offset: 0x001408AC
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CanonicalFontFamilyReference);
		}

		// Token: 0x06005057 RID: 20567 RVA: 0x001414C8 File Offset: 0x001408C8
		public override int GetHashCode()
		{
			if (this._absoluteLocationUri == null && this.EscapedFileName == null)
			{
				return this._familyName.GetHashCode();
			}
			int hash = (this._absoluteLocationUri != null) ? this._absoluteLocationUri.GetHashCode() : this.EscapedFileName.GetHashCode();
			hash = HashFn.HashMultiply(hash) + this._familyName.GetHashCode();
			return HashFn.HashScramble(hash);
		}

		// Token: 0x06005058 RID: 20568 RVA: 0x00141538 File Offset: 0x00140938
		[SecurityCritical]
		private CanonicalFontFamilyReference(string escapedFileName, string familyName)
		{
			this.EscapedFileName = escapedFileName;
			this._familyName = familyName;
		}

		// Token: 0x06005059 RID: 20569 RVA: 0x0014155C File Offset: 0x0014095C
		private CanonicalFontFamilyReference(Uri absoluteLocationUri, string familyName)
		{
			this._absoluteLocationUri = absoluteLocationUri;
			this._familyName = familyName;
		}

		// Token: 0x0600505A RID: 20570 RVA: 0x00141580 File Offset: 0x00140980
		private static bool SplitFontFamilyReference(string normalizedString, out string locationString, out string escapedFamilyName)
		{
			int num;
			if (normalizedString[0] == '#')
			{
				locationString = null;
				num = 1;
			}
			else
			{
				int num2 = normalizedString.IndexOf('#');
				locationString = normalizedString.Substring(0, num2);
				num = num2 + 1;
			}
			if (num < normalizedString.Length)
			{
				escapedFamilyName = normalizedString.Substring(num);
				return true;
			}
			escapedFamilyName = null;
			return false;
		}

		// Token: 0x0400248C RID: 9356
		private Uri _absoluteLocationUri;

		// Token: 0x0400248D RID: 9357
		private string _familyName;

		// Token: 0x0400248E RID: 9358
		private static readonly CanonicalFontFamilyReference _unresolved = new CanonicalFontFamilyReference(null, string.Empty);
	}
}
