using System;
using System.IO;
using System.IO.Packaging;
using System.Net;
using System.Net.Cache;

namespace MS.Internal.IO.Packaging
{
	// Token: 0x020007B3 RID: 1971
	internal class PseudoWebRequest : WebRequest
	{
		// Token: 0x060052EF RID: 21231 RVA: 0x0014B79C File Offset: 0x0014AB9C
		internal PseudoWebRequest(Uri uri, Uri packageUri, Uri partUri, Package cacheEntry)
		{
			this._uri = uri;
			this._innerUri = packageUri;
			this._partName = partUri;
			this._cacheEntry = cacheEntry;
			this.SetDefaults();
		}

		// Token: 0x060052F0 RID: 21232 RVA: 0x0014B7D4 File Offset: 0x0014ABD4
		public override Stream GetRequestStream()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060052F1 RID: 21233 RVA: 0x0014B7E8 File Offset: 0x0014ABE8
		public override WebResponse GetResponse()
		{
			Invariant.Assert(false, "PackWebRequest must handle this method.");
			return null;
		}

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x060052F2 RID: 21234 RVA: 0x0014B804 File Offset: 0x0014AC04
		// (set) Token: 0x060052F3 RID: 21235 RVA: 0x0014B820 File Offset: 0x0014AC20
		public override RequestCachePolicy CachePolicy
		{
			get
			{
				Invariant.Assert(false, "PackWebRequest must handle this method.");
				return null;
			}
			set
			{
				Invariant.Assert(false, "PackWebRequest must handle this method.");
			}
		}

		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x060052F4 RID: 21236 RVA: 0x0014B838 File Offset: 0x0014AC38
		// (set) Token: 0x060052F5 RID: 21237 RVA: 0x0014B84C File Offset: 0x0014AC4C
		public override string ConnectionGroupName
		{
			get
			{
				return this._connectionGroupName;
			}
			set
			{
				this._connectionGroupName = value;
			}
		}

		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x060052F6 RID: 21238 RVA: 0x0014B860 File Offset: 0x0014AC60
		// (set) Token: 0x060052F7 RID: 21239 RVA: 0x0014B874 File Offset: 0x0014AC74
		public override long ContentLength
		{
			get
			{
				return (long)this._contentLength;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x060052F8 RID: 21240 RVA: 0x0014B888 File Offset: 0x0014AC88
		// (set) Token: 0x060052F9 RID: 21241 RVA: 0x0014B89C File Offset: 0x0014AC9C
		public override string ContentType
		{
			get
			{
				return this._contentType;
			}
			set
			{
				this._contentType = value;
			}
		}

		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x060052FA RID: 21242 RVA: 0x0014B8B0 File Offset: 0x0014ACB0
		// (set) Token: 0x060052FB RID: 21243 RVA: 0x0014B8C4 File Offset: 0x0014ACC4
		public override ICredentials Credentials
		{
			get
			{
				return this._credentials;
			}
			set
			{
				this._credentials = value;
			}
		}

		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x060052FC RID: 21244 RVA: 0x0014B8D8 File Offset: 0x0014ACD8
		// (set) Token: 0x060052FD RID: 21245 RVA: 0x0014B900 File Offset: 0x0014AD00
		public override WebHeaderCollection Headers
		{
			get
			{
				if (this._headers == null)
				{
					this._headers = new WebHeaderCollection();
				}
				return this._headers;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._headers = value;
			}
		}

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x060052FE RID: 21246 RVA: 0x0014B924 File Offset: 0x0014AD24
		// (set) Token: 0x060052FF RID: 21247 RVA: 0x0014B938 File Offset: 0x0014AD38
		public override string Method
		{
			get
			{
				return this._method;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._method = value;
			}
		}

		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x06005300 RID: 21248 RVA: 0x0014B95C File Offset: 0x0014AD5C
		// (set) Token: 0x06005301 RID: 21249 RVA: 0x0014B970 File Offset: 0x0014AD70
		public override bool PreAuthenticate
		{
			get
			{
				return this._preAuthenticate;
			}
			set
			{
				this._preAuthenticate = value;
			}
		}

		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x06005302 RID: 21250 RVA: 0x0014B984 File Offset: 0x0014AD84
		// (set) Token: 0x06005303 RID: 21251 RVA: 0x0014B9AC File Offset: 0x0014ADAC
		public override IWebProxy Proxy
		{
			get
			{
				if (this._proxy == null)
				{
					this._proxy = WebRequest.DefaultWebProxy;
				}
				return this._proxy;
			}
			set
			{
				this._proxy = value;
			}
		}

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x06005304 RID: 21252 RVA: 0x0014B9C0 File Offset: 0x0014ADC0
		// (set) Token: 0x06005305 RID: 21253 RVA: 0x0014B9D4 File Offset: 0x0014ADD4
		public override int Timeout
		{
			get
			{
				return this._timeout;
			}
			set
			{
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._timeout = value;
			}
		}

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x06005306 RID: 21254 RVA: 0x0014B9FC File Offset: 0x0014ADFC
		// (set) Token: 0x06005307 RID: 21255 RVA: 0x0014BA24 File Offset: 0x0014AE24
		public override bool UseDefaultCredentials
		{
			get
			{
				if (this.IsScheme(Uri.UriSchemeFtp))
				{
					throw new NotSupportedException();
				}
				return this._useDefaultCredentials;
			}
			set
			{
				if (this.IsScheme(Uri.UriSchemeFtp))
				{
					throw new NotSupportedException();
				}
				this._useDefaultCredentials = value;
			}
		}

		// Token: 0x06005308 RID: 21256 RVA: 0x0014BA4C File Offset: 0x0014AE4C
		private bool IsScheme(string schemeName)
		{
			return string.CompareOrdinal(this._innerUri.Scheme, schemeName) == 0;
		}

		// Token: 0x06005309 RID: 21257 RVA: 0x0014BA70 File Offset: 0x0014AE70
		private void SetDefaults()
		{
			this._connectionGroupName = string.Empty;
			this._contentType = null;
			this._credentials = null;
			this._headers = null;
			this._preAuthenticate = false;
			this._proxy = null;
			if (this.IsScheme(Uri.UriSchemeHttp))
			{
				this._timeout = 100000;
				this._method = "GET";
			}
			else
			{
				this._timeout = -1;
			}
			if (this.IsScheme(Uri.UriSchemeFtp))
			{
				this._method = "RETR";
			}
			this._useDefaultCredentials = false;
			this._contentLength = -1;
		}

		// Token: 0x0400257D RID: 9597
		private Uri _uri;

		// Token: 0x0400257E RID: 9598
		private Uri _innerUri;

		// Token: 0x0400257F RID: 9599
		private Uri _partName;

		// Token: 0x04002580 RID: 9600
		private Package _cacheEntry;

		// Token: 0x04002581 RID: 9601
		private string _connectionGroupName;

		// Token: 0x04002582 RID: 9602
		private string _contentType;

		// Token: 0x04002583 RID: 9603
		private int _contentLength;

		// Token: 0x04002584 RID: 9604
		private string _method;

		// Token: 0x04002585 RID: 9605
		private ICredentials _credentials;

		// Token: 0x04002586 RID: 9606
		private WebHeaderCollection _headers;

		// Token: 0x04002587 RID: 9607
		private bool _preAuthenticate;

		// Token: 0x04002588 RID: 9608
		private IWebProxy _proxy;

		// Token: 0x04002589 RID: 9609
		private int _timeout;

		// Token: 0x0400258A RID: 9610
		private bool _useDefaultCredentials;
	}
}
