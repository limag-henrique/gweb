using System;
using System.IO;
using System.IO.Packaging;
using MS.Utility;

namespace MS.Internal.IO.Packaging
{
	// Token: 0x020007B0 RID: 1968
	internal class ResponseStream : Stream
	{
		// Token: 0x060052BE RID: 21182 RVA: 0x0014A684 File Offset: 0x00149A84
		internal ResponseStream(Stream s, PackWebResponse response, Stream owningStream, Package container)
		{
			this.Init(s, response, owningStream, container);
		}

		// Token: 0x060052BF RID: 21183 RVA: 0x0014A6A4 File Offset: 0x00149AA4
		internal ResponseStream(Stream s, PackWebResponse response)
		{
			this.Init(s, response, null, null);
		}

		// Token: 0x060052C0 RID: 21184 RVA: 0x0014A6C4 File Offset: 0x00149AC4
		private void Init(Stream s, PackWebResponse response, Stream owningStream, Package container)
		{
			this._innerStream = s;
			this._response = response;
			this._owningStream = owningStream;
			this._container = container;
		}

		// Token: 0x060052C1 RID: 21185 RVA: 0x0014A6F0 File Offset: 0x00149AF0
		public override int Read(byte[] buffer, int offset, int count)
		{
			EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordXPS, EventTrace.Level.Verbose, EventTrace.Event.WClientDRXReadStreamBegin, count);
			this.CheckDisposed();
			int num = this._innerStream.Read(buffer, offset, count);
			EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordXPS, EventTrace.Level.Verbose, EventTrace.Event.WClientDRXReadStreamEnd, num);
			return num;
		}

		// Token: 0x060052C2 RID: 21186 RVA: 0x0014A73C File Offset: 0x00149B3C
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckDisposed();
			return this._innerStream.Seek(offset, origin);
		}

		// Token: 0x060052C3 RID: 21187 RVA: 0x0014A75C File Offset: 0x00149B5C
		public override void SetLength(long newLength)
		{
			this.CheckDisposed();
			this._innerStream.SetLength(newLength);
		}

		// Token: 0x060052C4 RID: 21188 RVA: 0x0014A77C File Offset: 0x00149B7C
		public override void Write(byte[] buf, int offset, int count)
		{
			this.CheckDisposed();
			this._innerStream.Write(buf, offset, count);
		}

		// Token: 0x060052C5 RID: 21189 RVA: 0x0014A7A0 File Offset: 0x00149BA0
		public override void Flush()
		{
			this.CheckDisposed();
			this._innerStream.Flush();
		}

		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x060052C6 RID: 21190 RVA: 0x0014A7C0 File Offset: 0x00149BC0
		public override bool CanRead
		{
			get
			{
				return !this._closed && this._innerStream.CanRead;
			}
		}

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x060052C7 RID: 21191 RVA: 0x0014A7E4 File Offset: 0x00149BE4
		public override bool CanSeek
		{
			get
			{
				return !this._closed && this._innerStream.CanSeek;
			}
		}

		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x060052C8 RID: 21192 RVA: 0x0014A808 File Offset: 0x00149C08
		public override bool CanWrite
		{
			get
			{
				return !this._closed && this._innerStream.CanWrite;
			}
		}

		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x060052C9 RID: 21193 RVA: 0x0014A82C File Offset: 0x00149C2C
		// (set) Token: 0x060052CA RID: 21194 RVA: 0x0014A84C File Offset: 0x00149C4C
		public override long Position
		{
			get
			{
				this.CheckDisposed();
				return this._innerStream.Position;
			}
			set
			{
				this.CheckDisposed();
				this._innerStream.Position = value;
			}
		}

		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x060052CB RID: 21195 RVA: 0x0014A86C File Offset: 0x00149C6C
		public override long Length
		{
			get
			{
				this.CheckDisposed();
				return this._innerStream.Length;
			}
		}

		// Token: 0x060052CC RID: 21196 RVA: 0x0014A88C File Offset: 0x00149C8C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && !this._closed)
				{
					this._container = null;
					this._innerStream.Close();
					if (this._owningStream != null)
					{
						this._owningStream.Close();
					}
				}
			}
			finally
			{
				this._innerStream = null;
				this._owningStream = null;
				this._response = null;
				this._closed = true;
				base.Dispose(disposing);
			}
		}

		// Token: 0x060052CD RID: 21197 RVA: 0x0014A90C File Offset: 0x00149D0C
		private void CheckDisposed()
		{
			if (this._closed)
			{
				throw new ObjectDisposedException("ResponseStream");
			}
		}

		// Token: 0x0400255E RID: 9566
		private bool _closed;

		// Token: 0x0400255F RID: 9567
		private Stream _innerStream;

		// Token: 0x04002560 RID: 9568
		private Package _container;

		// Token: 0x04002561 RID: 9569
		private Stream _owningStream;

		// Token: 0x04002562 RID: 9570
		private PackWebResponse _response;
	}
}
