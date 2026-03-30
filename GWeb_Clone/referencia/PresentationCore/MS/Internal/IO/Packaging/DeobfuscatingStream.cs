using System;
using System.IO;
using System.IO.Packaging;
using MS.Internal.PresentationCore;

namespace MS.Internal.IO.Packaging
{
	// Token: 0x020007AE RID: 1966
	internal class DeobfuscatingStream : Stream
	{
		// Token: 0x06005291 RID: 21137 RVA: 0x00149334 File Offset: 0x00148734
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			long position = this._obfuscatedStream.Position;
			int num = this._obfuscatedStream.Read(buffer, offset, count);
			this.Deobfuscate(buffer, offset, num, position);
			return num;
		}

		// Token: 0x06005292 RID: 21138 RVA: 0x00149370 File Offset: 0x00148770
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			throw new NotSupportedException(SR.Get("WriteNotSupported"));
		}

		// Token: 0x06005293 RID: 21139 RVA: 0x00149394 File Offset: 0x00148794
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckDisposed();
			return this._obfuscatedStream.Seek(offset, origin);
		}

		// Token: 0x06005294 RID: 21140 RVA: 0x001493B4 File Offset: 0x001487B4
		public override void SetLength(long newLength)
		{
			this.CheckDisposed();
			throw new NotSupportedException(SR.Get("SetLengthNotSupported"));
		}

		// Token: 0x06005295 RID: 21141 RVA: 0x001493D8 File Offset: 0x001487D8
		public override void Flush()
		{
			this.CheckDisposed();
			this._obfuscatedStream.Flush();
		}

		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x06005296 RID: 21142 RVA: 0x001493F8 File Offset: 0x001487F8
		// (set) Token: 0x06005297 RID: 21143 RVA: 0x00149418 File Offset: 0x00148818
		public override long Position
		{
			get
			{
				this.CheckDisposed();
				return this._obfuscatedStream.Position;
			}
			set
			{
				this.CheckDisposed();
				this._obfuscatedStream.Position = value;
			}
		}

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x06005298 RID: 21144 RVA: 0x00149438 File Offset: 0x00148838
		public override long Length
		{
			get
			{
				return this._obfuscatedStream.Length;
			}
		}

		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x06005299 RID: 21145 RVA: 0x00149450 File Offset: 0x00148850
		public override bool CanRead
		{
			get
			{
				return this._obfuscatedStream != null && this._obfuscatedStream.CanRead;
			}
		}

		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x0600529A RID: 21146 RVA: 0x00149474 File Offset: 0x00148874
		public override bool CanSeek
		{
			get
			{
				return this._obfuscatedStream != null && this._obfuscatedStream.CanSeek;
			}
		}

		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x0600529B RID: 21147 RVA: 0x00149498 File Offset: 0x00148898
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600529C RID: 21148 RVA: 0x001494A8 File Offset: 0x001488A8
		internal DeobfuscatingStream(Stream obfuscatedStream, Uri streamUri, bool leaveOpen)
		{
			if (obfuscatedStream == null)
			{
				throw new ArgumentNullException("obfuscatedStream");
			}
			Uri partUri = PackUriHelper.GetPartUri(streamUri);
			if (partUri == null)
			{
				throw new InvalidOperationException(SR.Get("InvalidPartName"));
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(streamUri.GetComponents(UriComponents.Path | UriComponents.KeepDelimiter, UriFormat.UriEscaped));
			this._guid = DeobfuscatingStream.GetGuidByteArray(fileNameWithoutExtension);
			this._obfuscatedStream = obfuscatedStream;
			this._ownObfuscatedStream = !leaveOpen;
		}

		// Token: 0x0600529D RID: 21149 RVA: 0x00149518 File Offset: 0x00148918
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this._obfuscatedStream != null && this._ownObfuscatedStream)
					{
						this._obfuscatedStream.Close();
					}
					this._obfuscatedStream = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600529E RID: 21150 RVA: 0x00149570 File Offset: 0x00148970
		private void CheckDisposed()
		{
			if (this._obfuscatedStream == null)
			{
				throw new ObjectDisposedException(null, SR.Get("Media_StreamClosed"));
			}
		}

		// Token: 0x0600529F RID: 21151 RVA: 0x00149598 File Offset: 0x00148998
		private void Deobfuscate(byte[] buffer, int offset, int count, long readPosition)
		{
			if (readPosition >= 32L || count <= 0)
			{
				return;
			}
			int i = (int)(Math.Min(32L, readPosition + (long)count) - readPosition);
			int num = this._guid.Length - (int)readPosition % this._guid.Length - 1;
			int num2 = offset;
			while (i > 0)
			{
				if (num < 0)
				{
					num = this._guid.Length - 1;
				}
				int num3 = num2;
				buffer[num3] ^= this._guid[num];
				i--;
				num2++;
				num--;
			}
		}

		// Token: 0x060052A0 RID: 21152 RVA: 0x00149614 File Offset: 0x00148A14
		private static byte[] GetGuidByteArray(string guidString)
		{
			if (guidString.IndexOf('-') == -1)
			{
				throw new ArgumentException(SR.Get("InvalidPartName"));
			}
			Guid guid = new Guid(guidString);
			string text = guid.ToString("N");
			byte[] array = new byte[16];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Convert.ToByte(text.Substring(i * 2, 2), 16);
			}
			return array;
		}

		// Token: 0x04002543 RID: 9539
		private Stream _obfuscatedStream;

		// Token: 0x04002544 RID: 9540
		private byte[] _guid;

		// Token: 0x04002545 RID: 9541
		private bool _ownObfuscatedStream;

		// Token: 0x04002546 RID: 9542
		private const long ObfuscatedLength = 32L;
	}
}
