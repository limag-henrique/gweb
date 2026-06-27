using System;
using System.Net;
using System.Security;
using System.Threading;
using MS.Internal;
using MS.Internal.IO.Packaging;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.IO.Packaging
{
	/// <summary>Representa uma resposta de um <see cref="T:System.IO.Packaging.PackWebRequest" />.</summary>
	// Token: 0x02000186 RID: 390
	public sealed class PackWebResponse : WebResponse
	{
		// Token: 0x060003AE RID: 942 RVA: 0x00014FD4 File Offset: 0x000143D4
		[SecurityCritical]
		internal PackWebResponse(Uri uri, Uri innerUri, Uri partName, WebRequest innerRequest)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (innerUri == null)
			{
				throw new ArgumentNullException("innerUri");
			}
			if (innerRequest == null)
			{
				throw new ArgumentNullException("innerRequest");
			}
			this._lockObject = new object();
			this._uri = uri;
			this._innerUri = innerUri;
			this._partName = partName;
			this._webRequest = innerRequest;
			this._mimeType = null;
			this._responseAvailable = new ManualResetEvent(false);
			if (innerRequest.Timeout != -1)
			{
				this._timeoutTimer = new Timer(new TimerCallback(this.TimeoutCallback), null, innerRequest.Timeout, -1);
			}
			this._webRequest.BeginGetResponse(new AsyncCallback(this.ResponseCallback), this);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001509C File Offset: 0x0001449C
		internal PackWebResponse(Uri uri, Uri innerUri, Uri partName, Package cacheEntry, bool cachedPackageIsThreadSafe)
		{
			this._lockObject = new object();
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (innerUri == null)
			{
				throw new ArgumentNullException("innerUri");
			}
			if (partName == null)
			{
				throw new ArgumentNullException("partName");
			}
			if (cacheEntry == null)
			{
				throw new ArgumentNullException("cacheEntry");
			}
			this._uri = uri;
			this._innerUri = innerUri;
			this._partName = partName;
			this._mimeType = null;
			this._cachedResponse = new PackWebResponse.CachedResponse(this, cacheEntry, cachedPackageIsThreadSafe);
		}

		/// <summary>Obtém o fluxo de resposta que está contido no <see cref="T:System.IO.Packaging.PackWebResponse" />.</summary>
		/// <returns>O fluxo de resposta.</returns>
		// Token: 0x060003B0 RID: 944 RVA: 0x00015134 File Offset: 0x00014534
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public override Stream GetResponseStream()
		{
			this.CheckDisposed();
			if (this.FromPackageCache)
			{
				return this._cachedResponse.GetResponseStream();
			}
			EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordXPS, EventTrace.Event.WClientDRXGetStreamBegin);
			if (this._responseStream == null)
			{
				this.WaitForResponse();
				long contentLength = this._fullResponse.ContentLength;
				this._responseStream = this._fullResponse.GetResponseStream();
				if (!this._responseStream.CanSeek || !this._innerUri.IsFile)
				{
					this._responseStream = new NetStream(this._responseStream, contentLength, this._innerUri, this._webRequest, this._fullResponse);
					this._responseStream = new BufferedStream(this._responseStream);
				}
				if (this._partName == null)
				{
					this._fullStreamLength = contentLength;
					this._mimeType = WpfWebRequestHelper.GetContentType(this._fullResponse);
					this._responseStream = new ResponseStream(this._responseStream, this);
				}
				else
				{
					Package package = Package.Open(this._responseStream);
					if (!package.PartExists(this._partName))
					{
						throw new WebException(SR.Get("WebResponsePartNotFound"));
					}
					PackagePart part = package.GetPart(this._partName);
					Stream stream = part.GetStream(FileMode.Open, FileAccess.Read);
					this._mimeType = new ContentType(part.ContentType);
					this._fullStreamLength = stream.Length;
					this._responseStream = new ResponseStream(stream, this, this._responseStream, package);
				}
				if (this._fullStreamLength >= 0L)
				{
					this._lengthAvailable = true;
				}
			}
			EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordXPS, EventTrace.Event.WClientDRXGetStreamEnd);
			return this._responseStream;
		}

		/// <summary>Fecha o fluxo para esta solicitação.</summary>
		// Token: 0x060003B1 RID: 945 RVA: 0x000152B4 File Offset: 0x000146B4
		public override void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Obtém o objeto <see cref="T:System.Net.WebResponse" /> interno para a resposta.</summary>
		/// <returns>Os dados de resposta como um <see cref="T:System.Net.WebResponse" />.</returns>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x000152C8 File Offset: 0x000146C8
		public WebResponse InnerResponse
		{
			get
			{
				this.CheckDisposed();
				if (this.FromPackageCache)
				{
					return null;
				}
				this.WaitForResponse();
				return this._fullResponse;
			}
		}

		/// <summary>Obtém a coleção de <see cref="P:System.Net.WebResponse.Headers" /> Web para esta resposta.</summary>
		/// <returns>A coleção da resposta Web de <see cref="P:System.Net.WebResponse.Headers" />.</returns>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x000152F4 File Offset: 0x000146F4
		public override WebHeaderCollection Headers
		{
			get
			{
				this.CheckDisposed();
				if (this.FromPackageCache)
				{
					return this._cachedResponse.Headers;
				}
				this.WaitForResponse();
				return this._fullResponse.Headers;
			}
		}

		/// <summary>Obtém o URI (Uniform Resource Identifier) do arquivo de resposta.</summary>
		/// <returns>O URI da resposta.</returns>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0001532C File Offset: 0x0001472C
		public override Uri ResponseUri
		{
			get
			{
				this.CheckDisposed();
				if (this.FromPackageCache)
				{
					return this._uri;
				}
				this.WaitForResponse();
				return PackUriHelper.Create(this._fullResponse.ResponseUri, this._partName);
			}
		}

		/// <summary>Obtém um valor que indica se a resposta é do cache do pacote ou de uma solicitação Web.</summary>
		/// <returns>
		///   <see langword="true" /> Se a resposta do cache do pacote; <see langword="false" /> se a resposta for de uma solicitação da Web.</returns>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0001536C File Offset: 0x0001476C
		public override bool IsFromCache
		{
			get
			{
				this.CheckDisposed();
				if (this.FromPackageCache)
				{
					return true;
				}
				this.WaitForResponse();
				return this._fullResponse.IsFromCache;
			}
		}

		/// <summary>Obtém o tipo de conteúdo MIME (Multipurpose Internet Mail Extensions) do conteúdo do fluxo de resposta.</summary>
		/// <returns>O MIME tipo de conteúdo do fluxo.</returns>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0001539C File Offset: 0x0001479C
		public override string ContentType
		{
			get
			{
				this.CheckDisposed();
				if (!this.FromPackageCache)
				{
					this.WaitForResponse();
				}
				if (this._mimeType == null)
				{
					this.GetResponseStream();
				}
				return this._mimeType.ToString();
			}
		}

		/// <summary>Obtém o comprimento do conteúdo da resposta.</summary>
		/// <returns>O comprimento de conteúdo, em bytes.</returns>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x000153D8 File Offset: 0x000147D8
		public override long ContentLength
		{
			get
			{
				this.CheckDisposed();
				if (this.FromPackageCache)
				{
					return this._cachedResponse.ContentLength;
				}
				this.WaitForResponse();
				if (!this._lengthAvailable)
				{
					this._fullStreamLength = this.GetResponseStream().Length;
					this._lengthAvailable = true;
				}
				return this._fullStreamLength;
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001542C File Offset: 0x0001482C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void AbortResponse()
		{
			try
			{
				if (!this._responseAvailable.WaitOne(0, false))
				{
					this._webRequest.Abort();
				}
			}
			catch (NotImplementedException)
			{
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00015474 File Offset: 0x00014874
		protected override void Dispose(bool disposing)
		{
			if (!this._disposed && disposing)
			{
				if (this._disposed)
				{
					return;
				}
				if (this.FromPackageCache)
				{
					this._cachedResponse.Close();
					this._cachedResponse = null;
					return;
				}
				object lockObject = this._lockObject;
				lock (lockObject)
				{
					try
					{
						this.AbortResponse();
						this._disposed = true;
						if (this._responseStream != null)
						{
							this._responseStream.Close();
						}
						if (this._fullResponse != null)
						{
							((IDisposable)this._fullResponse).Dispose();
						}
						this._responseAvailable.Close();
						if (this._timeoutTimer != null)
						{
							this._timeoutTimer.Dispose();
						}
					}
					finally
					{
						this._timeoutTimer = null;
						this._responseStream = null;
						this._fullResponse = null;
						this._responseAvailable = null;
					}
				}
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00015578 File Offset: 0x00014978
		private bool FromPackageCache
		{
			get
			{
				return this._cachedResponse != null;
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00015590 File Offset: 0x00014990
		private void CheckDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("PackWebResponse");
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000155B0 File Offset: 0x000149B0
		[SecurityCritical]
		private void ResponseCallback(IAsyncResult ar)
		{
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				try
				{
					if (!this._disposed)
					{
						if (this._timeoutTimer != null)
						{
							this._timeoutTimer.Dispose();
						}
						this._fullResponse = WpfWebRequestHelper.EndGetResponse(this._webRequest, ar);
					}
				}
				catch (WebException responseException)
				{
					this._responseException = responseException;
					this._responseError = true;
				}
				catch
				{
					this._responseError = true;
					throw;
				}
				finally
				{
					this._timeoutTimer = null;
					if (!this._disposed)
					{
						this._responseAvailable.Set();
					}
				}
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000156A8 File Offset: 0x00014AA8
		private void WaitForResponse()
		{
			this._responseAvailable.WaitOne();
			if (!this._responseError)
			{
				return;
			}
			if (this._responseException == null)
			{
				throw new WebException(SR.Get("WebResponseFailure"));
			}
			throw this._responseException;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000156E8 File Offset: 0x00014AE8
		private void TimeoutCallback(object stateInfo)
		{
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				if (!this._disposed)
				{
					try
					{
						if (!this._responseAvailable.WaitOne(0, false))
						{
							this._responseError = true;
							this._responseException = new WebException(SR.Get("WebRequestTimeout", null), WebExceptionStatus.Timeout);
						}
						if (this._timeoutTimer != null)
						{
							this._timeoutTimer.Dispose();
						}
					}
					finally
					{
						this._timeoutTimer = null;
						if (!this._disposed)
						{
							this._responseAvailable.Set();
						}
					}
				}
			}
		}

		// Token: 0x040004A9 RID: 1193
		private ContentType _mimeType;

		// Token: 0x040004AA RID: 1194
		private const int _bufferSize = 4096;

		// Token: 0x040004AB RID: 1195
		private Uri _uri;

		// Token: 0x040004AC RID: 1196
		private Uri _innerUri;

		// Token: 0x040004AD RID: 1197
		private Uri _partName;

		// Token: 0x040004AE RID: 1198
		private bool _disposed;

		// Token: 0x040004AF RID: 1199
		[SecurityCritical]
		private WebRequest _webRequest;

		// Token: 0x040004B0 RID: 1200
		private WebResponse _fullResponse;

		// Token: 0x040004B1 RID: 1201
		private long _fullStreamLength;

		// Token: 0x040004B2 RID: 1202
		private Stream _responseStream;

		// Token: 0x040004B3 RID: 1203
		private bool _responseError;

		// Token: 0x040004B4 RID: 1204
		private Exception _responseException;

		// Token: 0x040004B5 RID: 1205
		private Timer _timeoutTimer;

		// Token: 0x040004B6 RID: 1206
		private ManualResetEvent _responseAvailable;

		// Token: 0x040004B7 RID: 1207
		private bool _lengthAvailable;

		// Token: 0x040004B8 RID: 1208
		private PackWebResponse.CachedResponse _cachedResponse;

		// Token: 0x040004B9 RID: 1209
		private object _lockObject;

		// Token: 0x020007F1 RID: 2033
		private class CachedResponse
		{
			// Token: 0x0600557F RID: 21887 RVA: 0x0015FB3C File Offset: 0x0015EF3C
			internal CachedResponse(PackWebResponse parent, Package cacheEntry, bool cachedPackageIsThreadSafe)
			{
				this._parent = parent;
				this._cacheEntry = cacheEntry;
				this._cachedPackageIsThreadSafe = cachedPackageIsThreadSafe;
			}

			// Token: 0x06005580 RID: 21888 RVA: 0x0015FB64 File Offset: 0x0015EF64
			internal Stream GetResponseStream()
			{
				Package cacheEntry = this._cacheEntry;
				lock (cacheEntry)
				{
					if (this._parent._responseStream == null)
					{
						if (!(this._parent._partName == null))
						{
							PackagePart part = this._cacheEntry.GetPart(this._parent._partName);
							Stream stream = part.GetStream(FileMode.Open, FileAccess.Read);
							if (!this._cachedPackageIsThreadSafe)
							{
								stream = new SynchronizingStream(stream, this._cacheEntry);
							}
							this._parent._mimeType = new ContentType(part.ContentType);
							this._parent._lengthAvailable = stream.CanSeek;
							if (stream.CanSeek)
							{
								this._parent._fullStreamLength = stream.Length;
							}
							this._parent._responseStream = stream;
						}
					}
				}
				return this._parent._responseStream;
			}

			// Token: 0x06005581 RID: 21889 RVA: 0x0015FC60 File Offset: 0x0015F060
			internal void Close()
			{
				try
				{
					this._parent._disposed = true;
					if (this._parent._responseStream != null)
					{
						this._parent._responseStream.Close();
					}
				}
				finally
				{
					this._cacheEntry = null;
					this._parent._uri = null;
					this._parent._mimeType = null;
					this._parent._innerUri = null;
					this._parent._partName = null;
					this._parent._responseStream = null;
					this._parent = null;
				}
			}

			// Token: 0x17001192 RID: 4498
			// (get) Token: 0x06005582 RID: 21890 RVA: 0x0015FD00 File Offset: 0x0015F100
			internal WebHeaderCollection Headers
			{
				get
				{
					return new WebHeaderCollection();
				}
			}

			// Token: 0x17001193 RID: 4499
			// (get) Token: 0x06005583 RID: 21891 RVA: 0x0015FD14 File Offset: 0x0015F114
			public long ContentLength
			{
				get
				{
					if (!this._parent._lengthAvailable)
					{
						this.GetResponseStream();
					}
					return this._parent._fullStreamLength;
				}
			}

			// Token: 0x04002682 RID: 9858
			private PackWebResponse _parent;

			// Token: 0x04002683 RID: 9859
			private Package _cacheEntry;

			// Token: 0x04002684 RID: 9860
			private bool _cachedPackageIsThreadSafe;
		}
	}
}
