using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32.SafeHandles;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace MS.Internal.IO.Packaging
{
	// Token: 0x020007B1 RID: 1969
	[FriendAccessAllowed]
	internal class ByteRangeDownloader : IDisposable
	{
		// Token: 0x060052CE RID: 21198 RVA: 0x0014A92C File Offset: 0x00149D2C
		[SecurityCritical]
		internal ByteRangeDownloader(Uri requestedUri, string tempFileName, SafeWaitHandle eventHandle) : this(requestedUri, eventHandle)
		{
			if (tempFileName == null)
			{
				throw new ArgumentNullException("tempFileName");
			}
			if (tempFileName.Length <= 0)
			{
				throw new ArgumentException(SR.Get("InvalidTempFileName"), "tempFileName");
			}
			this._tempFileStream = File.Open(tempFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
		}

		// Token: 0x060052CF RID: 21199 RVA: 0x0014A97C File Offset: 0x00149D7C
		[SecurityCritical]
		internal ByteRangeDownloader(Uri requestedUri, Stream tempStream, SafeWaitHandle eventHandle, Mutex fileMutex) : this(requestedUri, eventHandle)
		{
			this._tempFileStream = tempStream;
			this._fileMutex = fileMutex;
		}

		// Token: 0x060052D0 RID: 21200 RVA: 0x0014A9A0 File Offset: 0x00149DA0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060052D1 RID: 21201 RVA: 0x0014A9BC File Offset: 0x00149DBC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				object syncObject = this._syncObject;
				lock (syncObject)
				{
					if (!this._disposed)
					{
						try
						{
							if (this.FileMutex == null && this._tempFileStream != null)
							{
								this._tempFileStream.Close();
							}
						}
						finally
						{
							this._requestedUri = null;
							this._byteRangesInProgress = null;
							this._requestsOnWait = null;
							this._byteRangesAvailable = null;
							this._tempFileStream = null;
							this._eventHandle = null;
							this._proxy = null;
							this._credentials = null;
							this._cachePolicy = null;
							this._disposed = true;
						}
					}
				}
			}
		}

		// Token: 0x060052D2 RID: 21202 RVA: 0x0014AA8C File Offset: 0x00149E8C
		internal int[,] GetDownloadedByteRanges()
		{
			int[,] array = null;
			this.CheckDisposed();
			object syncObject = this._syncObject;
			lock (syncObject)
			{
				this.CheckErroredOutCondition();
				int num = this._byteRangesAvailable.Count / 2;
				array = new int[num, 2];
				for (int i = 0; i < num; i++)
				{
					array[i, 0] = (int)this._byteRangesAvailable[i * 2];
					array[i, 1] = (int)this._byteRangesAvailable[i * 2 + 1];
				}
				this._byteRangesAvailable.Clear();
			}
			return array;
		}

		// Token: 0x060052D3 RID: 21203 RVA: 0x0014AB48 File Offset: 0x00149F48
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal void RequestByteRanges(int[,] byteRanges)
		{
			this.CheckDisposed();
			if (byteRanges == null)
			{
				throw new ArgumentNullException("byteRanges");
			}
			ByteRangeDownloader.CheckTwoDimensionalByteRanges(byteRanges);
			this._firstRequestMade = true;
			object syncObject = this._syncObject;
			lock (syncObject)
			{
				this.CheckErroredOutCondition();
				if (this._byteRangesInProgress == null)
				{
					this._webRequest = this.CreateHttpWebRequest(byteRanges);
					this._byteRangesInProgress = byteRanges;
					this._webRequest.BeginGetResponse(new AsyncCallback(this.ResponseCallback), this);
				}
				else
				{
					if (this._requestsOnWait == null)
					{
						this._requestsOnWait = new ArrayList(2);
					}
					for (int i = 0; i < byteRanges.GetLength(0); i++)
					{
						this._requestsOnWait.Add(byteRanges[i, 0]);
						this._requestsOnWait.Add(byteRanges[i, 1]);
					}
				}
			}
		}

		// Token: 0x060052D4 RID: 21204 RVA: 0x0014AC44 File Offset: 0x0014A044
		internal static int[,] ConvertByteRanges(int[] inByteRanges)
		{
			ByteRangeDownloader.CheckOneDimensionalByteRanges(inByteRanges);
			int[,] array = new int[inByteRanges.Length / 2, 2];
			int i = 0;
			int num = 0;
			while (i < inByteRanges.Length)
			{
				array[num, 0] = inByteRanges[i];
				array[num, 1] = inByteRanges[i + 1];
				i++;
				i++;
				num++;
			}
			return array;
		}

		// Token: 0x060052D5 RID: 21205 RVA: 0x0014AC94 File Offset: 0x0014A094
		internal static int[] ConvertByteRanges(int[,] inByteRanges)
		{
			int[] array = new int[inByteRanges.Length];
			int i = 0;
			int num = 0;
			while (i < inByteRanges.GetLength(0))
			{
				array[num] = inByteRanges[i, 0];
				array[++num] = inByteRanges[i, 1];
				i++;
				num++;
			}
			return array;
		}

		// Token: 0x17001130 RID: 4400
		// (set) Token: 0x060052D6 RID: 21206 RVA: 0x0014ACE0 File Offset: 0x0014A0E0
		internal IWebProxy Proxy
		{
			[SecurityCritical]
			set
			{
				this.CheckDisposed();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!this._firstRequestMade)
				{
					this._proxy = value;
					return;
				}
				throw new InvalidOperationException(SR.Get("RequestAlreadyStarted"));
			}
		}

		// Token: 0x17001131 RID: 4401
		// (set) Token: 0x060052D7 RID: 21207 RVA: 0x0014AD20 File Offset: 0x0014A120
		internal ICredentials Credentials
		{
			set
			{
				this.CheckDisposed();
				this._credentials = value;
			}
		}

		// Token: 0x17001132 RID: 4402
		// (set) Token: 0x060052D8 RID: 21208 RVA: 0x0014AD3C File Offset: 0x0014A13C
		internal RequestCachePolicy CachePolicy
		{
			set
			{
				this.CheckDisposed();
				if (!this._firstRequestMade)
				{
					this._cachePolicy = value;
					return;
				}
				throw new InvalidOperationException(SR.Get("RequestAlreadyStarted"));
			}
		}

		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x060052D9 RID: 21209 RVA: 0x0014AD70 File Offset: 0x0014A170
		internal Mutex FileMutex
		{
			get
			{
				this.CheckDisposed();
				return this._fileMutex;
			}
		}

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x060052DA RID: 21210 RVA: 0x0014AD8C File Offset: 0x0014A18C
		internal bool ErroredOut
		{
			get
			{
				this.CheckDisposed();
				object syncObject = this._syncObject;
				bool erroredOut;
				lock (syncObject)
				{
					erroredOut = this._erroredOut;
				}
				return erroredOut;
			}
		}

		// Token: 0x060052DB RID: 21211 RVA: 0x0014ADE0 File Offset: 0x0014A1E0
		private void CheckDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(null, SR.Get("ByteRangeDownloaderDisposed"));
			}
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x0014AE08 File Offset: 0x0014A208
		[SecurityCritical]
		private ByteRangeDownloader(Uri requestedUri, SafeWaitHandle eventHandle)
		{
			if (requestedUri == null)
			{
				throw new ArgumentNullException("requestedUri");
			}
			if (string.Compare(requestedUri.Scheme, Uri.UriSchemeHttp, StringComparison.Ordinal) != 0 && string.Compare(requestedUri.Scheme, Uri.UriSchemeHttps, StringComparison.Ordinal) != 0)
			{
				throw new ArgumentException(SR.Get("InvalidScheme"), "requestedUri");
			}
			if (eventHandle == null)
			{
				throw new ArgumentNullException("eventHandle");
			}
			if (eventHandle.IsInvalid || eventHandle.IsClosed)
			{
				throw new ArgumentException(SR.Get("InvalidEventHandle"), "eventHandle");
			}
			this._requestedUri = requestedUri;
			this._eventHandle = eventHandle;
		}

		// Token: 0x060052DD RID: 21213 RVA: 0x0014AED0 File Offset: 0x0014A2D0
		private void CheckErroredOutCondition()
		{
			if (this._erroredOut)
			{
				throw new InvalidOperationException(SR.Get("ByteRangeDownloaderErroredOut"), this._erroredOutException);
			}
		}

		// Token: 0x060052DE RID: 21214 RVA: 0x0014AEFC File Offset: 0x0014A2FC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private HttpWebRequest CreateHttpWebRequest(int[,] byteRanges)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WpfWebRequestHelper.CreateRequest(this._requestedUri);
			httpWebRequest.ProtocolVersion = HttpVersion.Version11;
			httpWebRequest.Method = "GET";
			new WebPermission(PermissionState.Unrestricted).Assert();
			try
			{
				httpWebRequest.Proxy = this._proxy;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			httpWebRequest.Credentials = this._credentials;
			httpWebRequest.CachePolicy = this._cachePolicy;
			for (int i = 0; i < byteRanges.GetLength(0); i++)
			{
				httpWebRequest.AddRange(byteRanges[i, 0], byteRanges[i, 0] + byteRanges[i, 1] - 1);
			}
			return httpWebRequest;
		}

		// Token: 0x060052DF RID: 21215 RVA: 0x0014AFB8 File Offset: 0x0014A3B8
		[SecurityCritical]
		private void RaiseEvent(bool throwExceptionOnError)
		{
			if (this._eventHandle != null && !this._eventHandle.IsInvalid && !this._eventHandle.IsClosed && UnsafeNativeMethods.SetEvent(this._eventHandle) == 0 && throwExceptionOnError)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x060052E0 RID: 21216 RVA: 0x0014B004 File Offset: 0x0014A404
		[SecurityCritical]
		private void ResponseCallback(IAsyncResult ar)
		{
			HttpWebResponse httpWebResponse = null;
			object syncObject = this._syncObject;
			lock (syncObject)
			{
				try
				{
					if (this._disposed)
					{
						return;
					}
					httpWebResponse = (HttpWebResponse)WpfWebRequestHelper.EndGetResponse(this._webRequest, ar);
					if (httpWebResponse.StatusCode == HttpStatusCode.PartialContent)
					{
						int num = this._byteRangesInProgress[0, 0];
						int num2 = num + this._byteRangesInProgress[0, 1] - 1;
						if (ByteRangeDownloader.CheckContentRange(httpWebResponse.Headers, num, ref num2))
						{
							if (this.WriteByteRange(httpWebResponse, num, num2 - num + 1))
							{
								this._byteRangesAvailable.Add(num);
								this._byteRangesAvailable.Add(num2 - num + 1);
							}
							else
							{
								this._erroredOut = true;
							}
						}
						else
						{
							this._erroredOut = true;
							this._erroredOutException = new NotSupportedException(SR.Get("ByteRangeRequestIsNotSupported"));
						}
					}
					else
					{
						this._erroredOut = true;
					}
				}
				catch (Exception ex)
				{
					this._erroredOut = true;
					this._erroredOutException = ex;
					throw ex;
				}
				catch
				{
					this._erroredOut = true;
					this._erroredOutException = null;
					throw;
				}
				finally
				{
					if (httpWebResponse != null)
					{
						httpWebResponse.Close();
					}
					this.RaiseEvent(!this._erroredOut);
				}
				if (!this._erroredOut)
				{
					this.ProcessWaitQueue();
				}
			}
		}

		// Token: 0x060052E1 RID: 21217 RVA: 0x0014B1B0 File Offset: 0x0014A5B0
		private bool Write(Stream s, int offset, int length)
		{
			this._tempFileStream.Seek((long)offset, SeekOrigin.Begin);
			while (length > 0)
			{
				int num = s.Read(this._buffer, 0, 4096);
				if (num == 0)
				{
					break;
				}
				this._tempFileStream.Write(this._buffer, 0, num);
				length -= num;
			}
			if (length != 0)
			{
				return false;
			}
			this._tempFileStream.Flush();
			return true;
		}

		// Token: 0x060052E2 RID: 21218 RVA: 0x0014B214 File Offset: 0x0014A614
		private bool WriteByteRange(HttpWebResponse response, int offset, int length)
		{
			bool result = false;
			using (Stream responseStream = response.GetResponseStream())
			{
				if (this._buffer == null)
				{
					this._buffer = new byte[4096];
				}
				if (this._fileMutex != null)
				{
					try
					{
						this._fileMutex.WaitOne();
						object isolatedStorageFileLock = PackagingUtilities.IsolatedStorageFileLock;
						lock (isolatedStorageFileLock)
						{
							return this.Write(responseStream, offset, length);
						}
					}
					finally
					{
						this._fileMutex.ReleaseMutex();
					}
				}
				result = this.Write(responseStream, offset, length);
			}
			return result;
		}

		// Token: 0x060052E3 RID: 21219 RVA: 0x0014B2F0 File Offset: 0x0014A6F0
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void ProcessWaitQueue()
		{
			if (this._requestsOnWait != null && this._requestsOnWait.Count > 0)
			{
				this._byteRangesInProgress[0, 0] = (int)this._requestsOnWait[0];
				this._byteRangesInProgress[0, 1] = (int)this._requestsOnWait[1];
				this._requestsOnWait.RemoveRange(0, 2);
				this._webRequest = this.CreateHttpWebRequest(this._byteRangesInProgress);
				this._webRequest.BeginGetResponse(new AsyncCallback(this.ResponseCallback), this);
				return;
			}
			this._byteRangesInProgress = null;
		}

		// Token: 0x060052E4 RID: 21220 RVA: 0x0014B398 File Offset: 0x0014A798
		private static void CheckOneDimensionalByteRanges(int[] byteRanges)
		{
			if (byteRanges.Length < 2 || byteRanges.Length % 2 != 0)
			{
				throw new ArgumentException(SR.Get("InvalidByteRanges", new object[]
				{
					"byteRanges"
				}));
			}
			for (int i = 0; i < byteRanges.Length; i++)
			{
				if (byteRanges[i] < 0 || byteRanges[i + 1] <= 0)
				{
					throw new ArgumentException(SR.Get("InvalidByteRanges", new object[]
					{
						"byteRanges"
					}));
				}
				i++;
			}
		}

		// Token: 0x060052E5 RID: 21221 RVA: 0x0014B410 File Offset: 0x0014A810
		private static void CheckTwoDimensionalByteRanges(int[,] byteRanges)
		{
			if (byteRanges.GetLength(0) <= 0 || byteRanges.GetLength(1) != 2)
			{
				throw new ArgumentException(SR.Get("InvalidByteRanges", new object[]
				{
					"byteRanges"
				}));
			}
			for (int i = 0; i < byteRanges.GetLength(0); i++)
			{
				if (byteRanges[i, 0] < 0 || byteRanges[i, 1] <= 0)
				{
					throw new ArgumentException(SR.Get("InvalidByteRanges", new object[]
					{
						"byteRanges"
					}));
				}
			}
		}

		// Token: 0x060052E6 RID: 21222 RVA: 0x0014B498 File Offset: 0x0014A898
		private static bool CheckContentRange(WebHeaderCollection responseHeaders, int beginOffset, ref int endOffset)
		{
			string text = responseHeaders["Content-Range"];
			if (text == null)
			{
				return false;
			}
			text = text.ToUpperInvariant();
			if (text.Length == 0 || !text.StartsWith("BYTES ", StringComparison.Ordinal))
			{
				return false;
			}
			int num = text.IndexOf('-');
			if (num == -1)
			{
				return false;
			}
			int num2 = int.Parse(text.Substring("BYTES ".Length, num - "BYTES ".Length), NumberStyles.None, NumberFormatInfo.InvariantInfo);
			text = text.Substring(num + 1);
			num = text.IndexOf('/');
			if (num == -1)
			{
				return false;
			}
			int num3 = int.Parse(text.Substring(0, num), NumberStyles.None, NumberFormatInfo.InvariantInfo);
			text = text.Substring(num + 1);
			if (string.CompareOrdinal(text, "*") != 0)
			{
				int.Parse(text, NumberStyles.None, NumberFormatInfo.InvariantInfo);
			}
			bool flag = num2 <= num3 && beginOffset == num2;
			if (flag && num3 < endOffset)
			{
				endOffset = num3;
			}
			return flag;
		}

		// Token: 0x04002563 RID: 9571
		private bool _firstRequestMade;

		// Token: 0x04002564 RID: 9572
		private bool _disposed;

		// Token: 0x04002565 RID: 9573
		private object _syncObject = new object();

		// Token: 0x04002566 RID: 9574
		private bool _erroredOut;

		// Token: 0x04002567 RID: 9575
		private Exception _erroredOutException;

		// Token: 0x04002568 RID: 9576
		private Uri _requestedUri;

		// Token: 0x04002569 RID: 9577
		private RequestCachePolicy _cachePolicy;

		// Token: 0x0400256A RID: 9578
		[SecurityCritical]
		private IWebProxy _proxy;

		// Token: 0x0400256B RID: 9579
		private ICredentials _credentials;

		// Token: 0x0400256C RID: 9580
		private CookieContainer _cookieContainer = new CookieContainer(1);

		// Token: 0x0400256D RID: 9581
		[SecurityCritical]
		private SafeWaitHandle _eventHandle;

		// Token: 0x0400256E RID: 9582
		private Mutex _fileMutex;

		// Token: 0x0400256F RID: 9583
		private Stream _tempFileStream;

		// Token: 0x04002570 RID: 9584
		private ArrayList _byteRangesAvailable = new ArrayList(2);

		// Token: 0x04002571 RID: 9585
		private ArrayList _requestsOnWait;

		// Token: 0x04002572 RID: 9586
		private int[,] _byteRangesInProgress;

		// Token: 0x04002573 RID: 9587
		private HttpWebRequest _webRequest;

		// Token: 0x04002574 RID: 9588
		private byte[] _buffer;

		// Token: 0x04002575 RID: 9589
		private const int WriteBufferSize = 4096;

		// Token: 0x04002576 RID: 9590
		private const int TimeOut = 5000;

		// Token: 0x04002577 RID: 9591
		private const int Offset_Index = 0;

		// Token: 0x04002578 RID: 9592
		private const int Length_Index = 1;

		// Token: 0x04002579 RID: 9593
		private const string ByteRangeUnit = "BYTES ";

		// Token: 0x0400257A RID: 9594
		private const string ContentRangeHeader = "Content-Range";
	}
}
