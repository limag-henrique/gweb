using System;
using System.IO;

namespace MS.Internal.IO.Packaging
{
	// Token: 0x020007AD RID: 1965
	internal class SynchronizingStream : Stream
	{
		// Token: 0x06005281 RID: 21121 RVA: 0x00148DA4 File Offset: 0x001481A4
		internal SynchronizingStream(Stream stream, object syncRoot)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (syncRoot == null)
			{
				throw new ArgumentNullException("syncRoot");
			}
			this._baseStream = stream;
			this._syncRoot = syncRoot;
		}

		// Token: 0x06005282 RID: 21122 RVA: 0x00148DE4 File Offset: 0x001481E4
		public override int Read(byte[] buffer, int offset, int count)
		{
			object syncRoot = this._syncRoot;
			int result;
			lock (syncRoot)
			{
				this.CheckDisposed();
				result = this._baseStream.Read(buffer, offset, count);
			}
			return result;
		}

		// Token: 0x06005283 RID: 21123 RVA: 0x00148E40 File Offset: 0x00148240
		public override int ReadByte()
		{
			object syncRoot = this._syncRoot;
			int result;
			lock (syncRoot)
			{
				this.CheckDisposed();
				result = this._baseStream.ReadByte();
			}
			return result;
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x00148E9C File Offset: 0x0014829C
		public override void WriteByte(byte b)
		{
			object syncRoot = this._syncRoot;
			lock (syncRoot)
			{
				this.CheckDisposed();
				this._baseStream.WriteByte(b);
			}
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x00148EF4 File Offset: 0x001482F4
		public override long Seek(long offset, SeekOrigin origin)
		{
			object syncRoot = this._syncRoot;
			long result;
			lock (syncRoot)
			{
				this.CheckDisposed();
				result = this._baseStream.Seek(offset, origin);
			}
			return result;
		}

		// Token: 0x06005286 RID: 21126 RVA: 0x00148F50 File Offset: 0x00148350
		public override void SetLength(long newLength)
		{
			object syncRoot = this._syncRoot;
			lock (syncRoot)
			{
				this.CheckDisposed();
				this._baseStream.SetLength(newLength);
			}
		}

		// Token: 0x06005287 RID: 21127 RVA: 0x00148FA8 File Offset: 0x001483A8
		public override void Write(byte[] buf, int offset, int count)
		{
			object syncRoot = this._syncRoot;
			lock (syncRoot)
			{
				this.CheckDisposed();
				this._baseStream.Write(buf, offset, count);
			}
		}

		// Token: 0x06005288 RID: 21128 RVA: 0x00149004 File Offset: 0x00148404
		public override void Flush()
		{
			object syncRoot = this._syncRoot;
			lock (syncRoot)
			{
				this.CheckDisposed();
				this._baseStream.Flush();
			}
		}

		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x06005289 RID: 21129 RVA: 0x0014905C File Offset: 0x0014845C
		public override bool CanRead
		{
			get
			{
				object syncRoot = this._syncRoot;
				bool result;
				lock (syncRoot)
				{
					result = (this._baseStream != null && this._baseStream.CanRead);
				}
				return result;
			}
		}

		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x0600528A RID: 21130 RVA: 0x001490BC File Offset: 0x001484BC
		public override bool CanSeek
		{
			get
			{
				object syncRoot = this._syncRoot;
				bool result;
				lock (syncRoot)
				{
					result = (this._baseStream != null && this._baseStream.CanSeek);
				}
				return result;
			}
		}

		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x0600528B RID: 21131 RVA: 0x0014911C File Offset: 0x0014851C
		public override bool CanWrite
		{
			get
			{
				object syncRoot = this._syncRoot;
				bool result;
				lock (syncRoot)
				{
					result = (this._baseStream != null && this._baseStream.CanWrite);
				}
				return result;
			}
		}

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x0600528C RID: 21132 RVA: 0x0014917C File Offset: 0x0014857C
		// (set) Token: 0x0600528D RID: 21133 RVA: 0x001491D8 File Offset: 0x001485D8
		public override long Position
		{
			get
			{
				object syncRoot = this._syncRoot;
				long position;
				lock (syncRoot)
				{
					this.CheckDisposed();
					position = this._baseStream.Position;
				}
				return position;
			}
			set
			{
				object syncRoot = this._syncRoot;
				lock (syncRoot)
				{
					this.CheckDisposed();
					this._baseStream.Position = value;
				}
			}
		}

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x0600528E RID: 21134 RVA: 0x00149230 File Offset: 0x00148630
		public override long Length
		{
			get
			{
				object syncRoot = this._syncRoot;
				long length;
				lock (syncRoot)
				{
					this.CheckDisposed();
					length = this._baseStream.Length;
				}
				return length;
			}
		}

		// Token: 0x0600528F RID: 21135 RVA: 0x0014928C File Offset: 0x0014868C
		protected override void Dispose(bool disposing)
		{
			object syncRoot = this._syncRoot;
			lock (syncRoot)
			{
				try
				{
					if (disposing && this._baseStream != null)
					{
						this._baseStream.Close();
					}
				}
				finally
				{
					base.Dispose(disposing);
					this._baseStream = null;
				}
			}
		}

		// Token: 0x06005290 RID: 21136 RVA: 0x00149314 File Offset: 0x00148714
		private void CheckDisposed()
		{
			if (this._baseStream == null)
			{
				throw new ObjectDisposedException("Stream");
			}
		}

		// Token: 0x04002541 RID: 9537
		private Stream _baseStream;

		// Token: 0x04002542 RID: 9538
		private object _syncRoot;
	}
}
