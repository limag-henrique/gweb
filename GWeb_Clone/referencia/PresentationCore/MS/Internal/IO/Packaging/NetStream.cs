using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using MS.Internal.PresentationCore;

namespace MS.Internal.IO.Packaging
{
	// Token: 0x020007AF RID: 1967
	internal class NetStream : Stream
	{
		// Token: 0x060052A1 RID: 21153 RVA: 0x0014967C File Offset: 0x00148A7C
		[SecurityCritical]
		internal NetStream(Stream responseStream, long fullStreamLength, Uri uri, WebRequest originalRequest, WebResponse originalResponse)
		{
			Invariant.Assert(uri != null);
			Invariant.Assert(responseStream != null);
			Invariant.Assert(originalRequest != null);
			Invariant.Assert(originalResponse != null);
			this._uri = uri;
			this._fullStreamLength = fullStreamLength;
			this._responseStream = responseStream;
			this._originalRequest = originalRequest;
			if (fullStreamLength > 0L && (string.Compare(uri.Scheme, Uri.UriSchemeHttp, StringComparison.Ordinal) == 0 || string.Compare(uri.Scheme, Uri.UriSchemeHttps, StringComparison.Ordinal) == 0))
			{
				this._allowByteRangeRequests = true;
				this._readEventHandles[1] = new AutoResetEvent(false);
			}
			this._readEventHandles[0] = new AutoResetEvent(false);
			this.StartFullDownload();
		}

		// Token: 0x060052A2 RID: 21154 RVA: 0x00149758 File Offset: 0x00148B58
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			PackagingUtilities.VerifyStreamReadArgs(this, buffer, offset, count);
			if (count == 0)
			{
				return count;
			}
			int num = 0;
			checked
			{
				if (offset + count > buffer.Length)
				{
					throw new ArgumentException(SR.Get("IOBufferOverflow"), "buffer");
				}
				int data = this.GetData(new NetStream.Block(this._position, count));
				count = Math.Min(data, count);
				if (count > 0)
				{
					try
					{
						this._tempFileMutex.WaitOne();
						object isolatedStorageFileLock = PackagingUtilities.IsolatedStorageFileLock;
						lock (isolatedStorageFileLock)
						{
							this._tempFileStream.Seek(this._position, SeekOrigin.Begin);
							num = this._tempFileStream.Read(buffer, offset, count);
						}
					}
					finally
					{
						this._tempFileMutex.ReleaseMutex();
					}
					this._position += unchecked((long)num);
				}
				return num;
			}
		}

		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x060052A3 RID: 21155 RVA: 0x00149854 File Offset: 0x00148C54
		public override bool CanRead
		{
			get
			{
				return !this._disposed;
			}
		}

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x060052A4 RID: 21156 RVA: 0x0014986C File Offset: 0x00148C6C
		public override bool CanSeek
		{
			get
			{
				return !this._disposed;
			}
		}

		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x060052A5 RID: 21157 RVA: 0x00149884 File Offset: 0x00148C84
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060052A6 RID: 21158 RVA: 0x00149894 File Offset: 0x00148C94
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckDisposed();
			checked
			{
				long num;
				switch (origin)
				{
				case SeekOrigin.Begin:
					num = offset;
					break;
				case SeekOrigin.Current:
					num = this._position + offset;
					break;
				case SeekOrigin.End:
					num = this.Length + offset;
					break;
				default:
					throw new ArgumentOutOfRangeException("origin", SR.Get("SeekOriginInvalid"));
				}
				if (num < 0L)
				{
					throw new ArgumentException(SR.Get("SeekNegative"));
				}
				this._position = num;
				return this._position;
			}
		}

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x060052A7 RID: 21159 RVA: 0x00149910 File Offset: 0x00148D10
		// (set) Token: 0x060052A8 RID: 21160 RVA: 0x0014992C File Offset: 0x00148D2C
		public override long Position
		{
			get
			{
				this.CheckDisposed();
				return this._position;
			}
			set
			{
				this.CheckDisposed();
				if (value < 0L)
				{
					throw new ArgumentException(SR.Get("SeekNegative"));
				}
				this._position = value;
			}
		}

		// Token: 0x060052A9 RID: 21161 RVA: 0x0014995C File Offset: 0x00148D5C
		public override void SetLength(long newLength)
		{
			throw new NotSupportedException(SR.Get("SetLengthNotSupported"));
		}

		// Token: 0x060052AA RID: 21162 RVA: 0x00149978 File Offset: 0x00148D78
		public override void Write(byte[] buf, int offset, int count)
		{
			throw new NotSupportedException(SR.Get("WriteNotSupported"));
		}

		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x060052AB RID: 21163 RVA: 0x00149994 File Offset: 0x00148D94
		public override long Length
		{
			get
			{
				this.CheckDisposed();
				if (this._fullStreamLength < 0L)
				{
					long position = this._position;
					this._position = this._highWaterMark;
					byte[] array = new byte[4096];
					while (this.Read(array, 0, array.Length) > 0)
					{
					}
					this._position = position;
				}
				return this._fullStreamLength;
			}
		}

		// Token: 0x060052AC RID: 21164 RVA: 0x001499EC File Offset: 0x00148DEC
		public override void Flush()
		{
		}

		// Token: 0x060052AD RID: 21165 RVA: 0x001499FC File Offset: 0x00148DFC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected override void Dispose(bool disposing)
		{
			try
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
								this._disposed = true;
								if (this._readEventHandles[0] != null)
								{
									this._readEventHandles[0].Set();
								}
								if (this._readEventHandles[1] != null)
								{
									this._readEventHandles[1].Set();
								}
								this.FreeByteRangeDownloader();
								if (this._readEventHandles[0] != null)
								{
									this._readEventHandles[0].Close();
									this._readEventHandles[0] = null;
								}
								if (this._readEventHandles[1] != null)
								{
									this._readEventHandles[1].Close();
									this._readEventHandles[1] = null;
								}
								if (this._responseStream != null)
								{
									this._responseStream.Close();
								}
								this.FreeTempFile();
							}
							finally
							{
								this._responseStream = null;
								this._readEventHandles = null;
								this._byteRangesAvailable = null;
								this._readBuf = null;
							}
						}
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060052AE RID: 21166 RVA: 0x00149B4C File Offset: 0x00148F4C
		private void StartFullDownload()
		{
			this._highWaterMark = 0L;
			this._readBuf = new byte[4096];
			object isoStoreSyncRoot = PackagingUtilities.IsoStoreSyncRoot;
			lock (isoStoreSyncRoot)
			{
				object isolatedStorageFileLock = PackagingUtilities.IsolatedStorageFileLock;
				lock (isolatedStorageFileLock)
				{
					this._tempFileStream = PackagingUtilities.CreateUserScopedIsolatedStorageFileStreamWithRandomName(3, out this._tempFileName);
				}
			}
			this._responseStream.BeginRead(this._readBuf, 0, this._readBuf.Length, new AsyncCallback(this.ReadCallBack), this);
		}

		// Token: 0x060052AF RID: 21167 RVA: 0x00149C18 File Offset: 0x00149018
		private void CheckDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("Stream");
			}
		}

		// Token: 0x060052B0 RID: 21168 RVA: 0x00149C3C File Offset: 0x0014903C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void ReadCallBack(IAsyncResult ar)
		{
			object syncObject = this._syncObject;
			checked
			{
				lock (syncObject)
				{
					try
					{
						if (this._disposed)
						{
							return;
						}
						int num = this._responseStream.EndRead(ar);
						if (num > 0)
						{
							try
							{
								this._tempFileMutex.WaitOne();
								object isolatedStorageFileLock = PackagingUtilities.IsolatedStorageFileLock;
								lock (isolatedStorageFileLock)
								{
									this._tempFileStream.Seek(this._highWaterMark, SeekOrigin.Begin);
									this._tempFileStream.Write(this._readBuf, 0, num);
									this._tempFileStream.Flush();
								}
								this._highWaterMark += unchecked((long)num);
								goto IL_C1;
							}
							finally
							{
								this._tempFileMutex.ReleaseMutex();
							}
						}
						if (this._fullStreamLength < 0L)
						{
							this._fullStreamLength = this._highWaterMark;
						}
						IL_C1:
						if (this._fullStreamLength == this._highWaterMark)
						{
							this._fullDownloadComplete = true;
						}
					}
					finally
					{
						if (!this._disposed && this._readEventHandles[0] != null)
						{
							this._readEventHandles[0].Set();
						}
					}
				}
			}
		}

		// Token: 0x060052B1 RID: 21169 RVA: 0x00149DB8 File Offset: 0x001491B8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void EnsureDownloader()
		{
			if (this._byteRangeDownloader == null)
			{
				this._byteRangeDownloader = new ByteRangeDownloader(this._uri, this._tempFileStream, this._readEventHandles[1].SafeWaitHandle, this._tempFileMutex);
				new WebPermission(PermissionState.Unrestricted).Assert();
				try
				{
					this._byteRangeDownloader.Proxy = this._originalRequest.Proxy;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				this._byteRangeDownloader.Credentials = this._originalRequest.Credentials;
				this._byteRangeDownloader.CachePolicy = this._originalRequest.CachePolicy;
				this._byteRangesAvailable = new ArrayList();
			}
		}

		// Token: 0x060052B2 RID: 21170 RVA: 0x00149E78 File Offset: 0x00149278
		private void MakeByteRangeRequest(NetStream.Block block)
		{
			if (block.Offset > (long)(2147483647 - block.Length + 1))
			{
				return;
			}
			this.EnsureDownloader();
			if (block.Length < 4096)
			{
				block.Length = 4096;
			}
			this.TrimBlockToStreamLength(block);
			if (block.Length > 0)
			{
				int[,] array = new int[1, 2];
				array[0, 0] = checked((int)block.Offset);
				array[0, 1] = block.Length;
				this._byteRangeDownloader.RequestByteRanges(array);
				this._inAdditionalRequest = true;
			}
		}

		// Token: 0x060052B3 RID: 21171 RVA: 0x00149F04 File Offset: 0x00149304
		private void GetByteRangeData()
		{
			int[,] downloadedByteRanges = this._byteRangeDownloader.GetDownloadedByteRanges();
			if (downloadedByteRanges.GetLength(0) > 0)
			{
				this._byteRangesAvailable.Insert(0, new NetStream.Block(0L, (int)this._highWaterMark));
				for (int i = 0; i < downloadedByteRanges.GetLength(0); i++)
				{
					this._byteRangesAvailable.Add(new NetStream.Block((long)downloadedByteRanges[i, 0], downloadedByteRanges[i, 1]));
				}
				this._byteRangesAvailable.Sort();
				this.MergeByteRanges(this._byteRangesAvailable);
				this._inAdditionalRequest = false;
			}
		}

		// Token: 0x060052B4 RID: 21172 RVA: 0x00149F94 File Offset: 0x00149394
		private int BytesInByteRangeAvailable(NetStream.Block block)
		{
			int num = 0;
			if (this._byteRangesAvailable != null)
			{
				this.TrimBlockToStreamLength(block);
				foreach (object obj in this._byteRangesAvailable)
				{
					NetStream.Block block2 = (NetStream.Block)obj;
					if (block2.Offset <= block.Offset && block2.End > block.Offset)
					{
						num = Math.Min(block.Length, checked((int)(block2.End - block.Offset)));
					}
					if (num > 0)
					{
						break;
					}
					if (block2.Offset >= block.End)
					{
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x060052B5 RID: 21173 RVA: 0x0014A054 File Offset: 0x00149454
		private int TrimByteRangeRequest(NetStream.Block block)
		{
			int num = 0;
			checked
			{
				if (this._byteRangesAvailable != null)
				{
					foreach (object obj in this._byteRangesAvailable)
					{
						NetStream.Block block2 = (NetStream.Block)obj;
						if (block.End <= block2.Offset)
						{
							break;
						}
						if (block.Offset >= block2.Offset && block2.End > block.Offset)
						{
							if (block.End <= block2.End)
							{
								num = block.Length;
							}
							else
							{
								num = (int)(block2.End - block.Offset);
								block.Offset = block2.End;
							}
							block.Length -= num;
						}
						if (block.Offset <= block2.Offset && block.End > block2.Offset && block.End <= block2.End)
						{
							block.Length = (int)(block2.Offset - block.Offset);
						}
						if (num > 0)
						{
							break;
						}
					}
				}
				return num;
			}
		}

		// Token: 0x060052B6 RID: 21174 RVA: 0x0014A17C File Offset: 0x0014957C
		private void MergeByteRanges(ArrayList ranges)
		{
			int num = 0;
			checked
			{
				while (num + 1 < ranges.Count)
				{
					NetStream.Block block = (NetStream.Block)ranges[num];
					while (block.Mergeable((NetStream.Block)ranges[num + 1]))
					{
						block.Merge((NetStream.Block)ranges[num + 1]);
						ranges.RemoveAt(num + 1);
						if (num + 1 >= ranges.Count)
						{
							break;
						}
					}
					num++;
				}
			}
		}

		// Token: 0x060052B7 RID: 21175 RVA: 0x0014A1EC File Offset: 0x001495EC
		private int HandleByteRangeReadEvent(NetStream.Block block)
		{
			int num = 0;
			checked
			{
				if (this._highWaterMark > block.Offset)
				{
					num = (int)Math.Min(unchecked((long)block.Length), this._highWaterMark - block.Offset);
				}
				if (num == block.Length)
				{
					this._additionalRequestThreshold *= 2U;
				}
				else if (!this._byteRangeDownloader.ErroredOut)
				{
					this.GetByteRangeData();
					num = this.BytesInByteRangeAvailable(block);
				}
				else
				{
					this._allowByteRangeRequests = false;
				}
				return num;
			}
		}

		// Token: 0x060052B8 RID: 21176 RVA: 0x0014A264 File Offset: 0x00149664
		private int HandleFullDownloadReadEvent(NetStream.Block block)
		{
			int result = 0;
			if (this._fullDownloadComplete)
			{
				this.TrimBlockToStreamLength(block);
				result = block.Length;
			}
			else
			{
				this._responseStream.BeginRead(this._readBuf, 0, this._readBuf.Length, new AsyncCallback(this.ReadCallBack), this);
				if (this._highWaterMark > block.Offset)
				{
					result = checked((int)Math.Min(unchecked((long)block.Length), this._highWaterMark - block.Offset));
				}
			}
			return result;
		}

		// Token: 0x060052B9 RID: 21177 RVA: 0x0014A2E0 File Offset: 0x001496E0
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private int GetData(NetStream.Block block)
		{
			this.TrimBlockToStreamLength(block);
			if (block.Length == 0)
			{
				return 0;
			}
			int num = 0;
			while (num == 0)
			{
				object syncObject = this._syncObject;
				lock (syncObject)
				{
					if (this._highWaterMark > block.Offset)
					{
						num = (int)Math.Min((long)block.Length, this._highWaterMark - block.Offset);
					}
					else
					{
						num = this.TrimByteRangeRequest(block);
						if (this._allowByteRangeRequests && !this._inAdditionalRequest && this._highWaterMark <= (long)(9223372036854775807UL - (ulong)this._additionalRequestThreshold) && block.Offset > this._highWaterMark + (long)((ulong)this._additionalRequestThreshold) && (this._byteRangeDownloader == null || !this._byteRangeDownloader.ErroredOut) && block.Length > 0)
						{
							this.MakeByteRangeRequest(block);
						}
					}
				}
				if (num == 0)
				{
					NetStream.ReadEvent readEvent;
					if (this._allowByteRangeRequests)
					{
						WaitHandle[] readEventHandles = this._readEventHandles;
						int num2 = WaitHandle.WaitAny(readEventHandles);
						if (num2 > 128)
						{
							num2 -= 128;
						}
						readEvent = (NetStream.ReadEvent)num2;
					}
					else
					{
						readEvent = NetStream.ReadEvent.FullDownloadReadEvent;
						this._readEventHandles[(int)readEvent].WaitOne();
					}
					object syncObject2 = this._syncObject;
					lock (syncObject2)
					{
						if (readEvent == NetStream.ReadEvent.ByteRangeReadEvent)
						{
							num = this.HandleByteRangeReadEvent(block);
						}
						else
						{
							num = this.HandleFullDownloadReadEvent(block);
							if (this._fullDownloadComplete)
							{
								this.ReleaseFullDownloadResources();
								break;
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x0014A488 File Offset: 0x00149888
		private void TrimBlockToStreamLength(NetStream.Block block)
		{
			if (this._fullStreamLength >= 0L)
			{
				block.Length = checked((int)Math.Min(unchecked((long)block.Length), this._fullStreamLength - block.Offset));
			}
		}

		// Token: 0x060052BB RID: 21179 RVA: 0x0014A4C0 File Offset: 0x001498C0
		[SecurityCritical]
		private void ReleaseFullDownloadResources()
		{
			if (this._readBuf != null)
			{
				this._byteRangesAvailable = null;
				this._readBuf = null;
				try
				{
					try
					{
						this.FreeByteRangeDownloader();
						if (this._readEventHandles[0] != null)
						{
							this._readEventHandles[0].Close();
							this._readEventHandles[0] = null;
						}
					}
					finally
					{
						if (this._responseStream != null)
						{
							this._responseStream.Close();
						}
					}
				}
				finally
				{
					this._responseStream = null;
				}
			}
		}

		// Token: 0x060052BC RID: 21180 RVA: 0x0014A560 File Offset: 0x00149960
		[SecurityCritical]
		private void FreeByteRangeDownloader()
		{
			if (this._byteRangeDownloader != null)
			{
				try
				{
					((IDisposable)this._byteRangeDownloader).Dispose();
					if (this._readEventHandles[1] != null)
					{
						this._readEventHandles[1].Close();
						this._readEventHandles[1] = null;
					}
				}
				finally
				{
					this._byteRangeDownloader = null;
				}
			}
		}

		// Token: 0x060052BD RID: 21181 RVA: 0x0014A5C8 File Offset: 0x001499C8
		private void FreeTempFile()
		{
			bool flag = false;
			Invariant.Assert(this._tempFileStream != null);
			try
			{
				flag = this._tempFileMutex.WaitOne(5000, false);
				object isolatedStorageFileLock = PackagingUtilities.IsolatedStorageFileLock;
				lock (isolatedStorageFileLock)
				{
					this._tempFileStream.Close();
				}
			}
			finally
			{
				if (flag)
				{
					this._tempFileMutex.ReleaseMutex();
					this._tempFileMutex.Close();
				}
				this._tempFileStream = null;
				this._tempFileName = null;
				this._tempFileMutex = null;
			}
		}

		// Token: 0x04002547 RID: 9543
		private Uri _uri;

		// Token: 0x04002548 RID: 9544
		[SecurityCritical]
		private WebRequest _originalRequest;

		// Token: 0x04002549 RID: 9545
		private Stream _tempFileStream;

		// Token: 0x0400254A RID: 9546
		private long _position;

		// Token: 0x0400254B RID: 9547
		private object _syncObject = new object();

		// Token: 0x0400254C RID: 9548
		private volatile bool _disposed;

		// Token: 0x0400254D RID: 9549
		private const int _readTimeOut = 40000;

		// Token: 0x0400254E RID: 9550
		private const int _additionalRequestMinSize = 4096;

		// Token: 0x0400254F RID: 9551
		private const int _bufferSize = 4096;

		// Token: 0x04002550 RID: 9552
		private const int _tempFileSyncTimeout = 5000;

		// Token: 0x04002551 RID: 9553
		private uint _additionalRequestThreshold = 16384U;

		// Token: 0x04002552 RID: 9554
		private Stream _responseStream;

		// Token: 0x04002553 RID: 9555
		private byte[] _readBuf;

		// Token: 0x04002554 RID: 9556
		private string _tempFileName;

		// Token: 0x04002555 RID: 9557
		private long _fullStreamLength;

		// Token: 0x04002556 RID: 9558
		private volatile bool _fullDownloadComplete;

		// Token: 0x04002557 RID: 9559
		private long _highWaterMark;

		// Token: 0x04002558 RID: 9560
		[SecurityCritical]
		private EventWaitHandle[] _readEventHandles = new EventWaitHandle[2];

		// Token: 0x04002559 RID: 9561
		private Mutex _tempFileMutex = new Mutex(false);

		// Token: 0x0400255A RID: 9562
		private bool _allowByteRangeRequests;

		// Token: 0x0400255B RID: 9563
		private ByteRangeDownloader _byteRangeDownloader;

		// Token: 0x0400255C RID: 9564
		private bool _inAdditionalRequest;

		// Token: 0x0400255D RID: 9565
		private ArrayList _byteRangesAvailable;

		// Token: 0x020009FE RID: 2558
		private class Block : IComparable
		{
			// Token: 0x06005BE5 RID: 23525 RVA: 0x0017133C File Offset: 0x0017073C
			internal Block(long offset, int length)
			{
				this._offset = offset;
				this._length = length;
			}

			// Token: 0x170012CB RID: 4811
			// (get) Token: 0x06005BE6 RID: 23526 RVA: 0x00171360 File Offset: 0x00170760
			internal long End
			{
				get
				{
					return checked(this._offset + unchecked((long)this._length));
				}
			}

			// Token: 0x170012CC RID: 4812
			// (get) Token: 0x06005BE7 RID: 23527 RVA: 0x0017137C File Offset: 0x0017077C
			// (set) Token: 0x06005BE8 RID: 23528 RVA: 0x00171390 File Offset: 0x00170790
			internal long Offset
			{
				get
				{
					return this._offset;
				}
				set
				{
					this._offset = value;
				}
			}

			// Token: 0x170012CD RID: 4813
			// (get) Token: 0x06005BE9 RID: 23529 RVA: 0x001713A4 File Offset: 0x001707A4
			// (set) Token: 0x06005BEA RID: 23530 RVA: 0x001713B8 File Offset: 0x001707B8
			internal int Length
			{
				get
				{
					return this._length;
				}
				set
				{
					this._length = value;
				}
			}

			// Token: 0x06005BEB RID: 23531 RVA: 0x001713CC File Offset: 0x001707CC
			int IComparable.CompareTo(object x)
			{
				NetStream.Block block = (NetStream.Block)x;
				if (this._offset < block._offset)
				{
					return -1;
				}
				if (this._offset > block._offset)
				{
					return 1;
				}
				if (this._length == block._length)
				{
					return 0;
				}
				if (this._length < block._length)
				{
					return -1;
				}
				return 1;
			}

			// Token: 0x06005BEC RID: 23532 RVA: 0x00171424 File Offset: 0x00170824
			internal bool Mergeable(NetStream.Block b)
			{
				checked
				{
					if (this._offset <= b._offset)
					{
						return this._offset + unchecked((long)this._length) - b._offset >= 0L;
					}
					return b._offset + unchecked((long)b._length) - this._offset >= 0L;
				}
			}

			// Token: 0x06005BED RID: 23533 RVA: 0x00171478 File Offset: 0x00170878
			internal void Merge(NetStream.Block b)
			{
				this._length = checked((int)(Math.Max(this._offset + unchecked((long)this._length), b._offset + unchecked((long)b._length)) - this._offset));
			}

			// Token: 0x04002F1D RID: 12061
			private long _offset;

			// Token: 0x04002F1E RID: 12062
			private int _length;
		}

		// Token: 0x020009FF RID: 2559
		private enum ReadEvent
		{
			// Token: 0x04002F20 RID: 12064
			FullDownloadReadEvent,
			// Token: 0x04002F21 RID: 12065
			ByteRangeReadEvent,
			// Token: 0x04002F22 RID: 12066
			MaxReadEventEnum
		}
	}
}
