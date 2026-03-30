using System;
using System.IO;
using System.IO.Packaging;
using System.Net;

namespace MS.Internal.AppModel
{
	// Token: 0x020007A8 RID: 1960
	internal class SiteOfOriginPart : PackagePart
	{
		// Token: 0x0600525E RID: 21086 RVA: 0x001486B0 File Offset: 0x00147AB0
		internal SiteOfOriginPart(Package container, Uri uri) : base(container, uri)
		{
		}

		// Token: 0x0600525F RID: 21087 RVA: 0x001486DC File Offset: 0x00147ADC
		protected override Stream GetStreamCore(FileMode mode, FileAccess access)
		{
			return this.GetStreamAndSetContentType(false);
		}

		// Token: 0x06005260 RID: 21088 RVA: 0x001486F0 File Offset: 0x00147AF0
		protected override string GetContentTypeCore()
		{
			this.GetStreamAndSetContentType(true);
			return this._contentType.ToString();
		}

		// Token: 0x06005261 RID: 21089 RVA: 0x00148710 File Offset: 0x00147B10
		private Stream GetStreamAndSetContentType(bool onlyNeedContentType)
		{
			object globalLock = this._globalLock;
			Stream result;
			lock (globalLock)
			{
				if (onlyNeedContentType && this._contentType != MS.Internal.ContentType.Empty)
				{
					result = null;
				}
				else if (this._cacheStream != null)
				{
					Stream cacheStream = this._cacheStream;
					this._cacheStream = null;
					result = cacheStream;
				}
				else
				{
					if (this._absoluteLocation == null)
					{
						string text = base.Uri.ToString();
						Invariant.Assert(text[0] == '/');
						string relativeUri = text.Substring(1);
						this._absoluteLocation = new Uri(SiteOfOriginContainer.SiteOfOrigin, relativeUri);
					}
					Stream stream;
					if (SecurityHelper.AreStringTypesEqual(this._absoluteLocation.Scheme, Uri.UriSchemeFile))
					{
						stream = this.HandleFileSource(onlyNeedContentType);
					}
					else
					{
						stream = this.HandleWebSource(onlyNeedContentType);
					}
					result = stream;
				}
			}
			return result;
		}

		// Token: 0x06005262 RID: 21090 RVA: 0x00148800 File Offset: 0x00147C00
		private Stream HandleFileSource(bool onlyNeedContentType)
		{
			if (this._contentType == MS.Internal.ContentType.Empty)
			{
				this._contentType = MimeTypeMapper.GetMimeTypeFromUri(base.Uri);
			}
			if (!onlyNeedContentType)
			{
				return File.OpenRead(this._absoluteLocation.LocalPath);
			}
			return null;
		}

		// Token: 0x06005263 RID: 21091 RVA: 0x00148840 File Offset: 0x00147C40
		private Stream HandleWebSource(bool onlyNeedContentType)
		{
			WebResponse webResponse = WpfWebRequestHelper.CreateRequestAndGetResponse(this._absoluteLocation);
			Stream responseStream = webResponse.GetResponseStream();
			if (this._contentType == MS.Internal.ContentType.Empty)
			{
				this._contentType = WpfWebRequestHelper.GetContentType(webResponse);
			}
			if (onlyNeedContentType)
			{
				this._cacheStream = responseStream;
			}
			return responseStream;
		}

		// Token: 0x04002523 RID: 9507
		private Uri _absoluteLocation;

		// Token: 0x04002524 RID: 9508
		private ContentType _contentType = MS.Internal.ContentType.Empty;

		// Token: 0x04002525 RID: 9509
		private Stream _cacheStream;

		// Token: 0x04002526 RID: 9510
		private object _globalLock = new object();
	}
}
