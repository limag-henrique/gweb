using System;
using System.IO;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006CA RID: 1738
	internal class FontTable
	{
		// Token: 0x06004B7A RID: 19322 RVA: 0x00126DF0 File Offset: 0x001261F0
		[SecurityCritical]
		public FontTable(byte[] data)
		{
			this.m_data = data;
			if (data != null)
			{
				this.m_length = (uint)data.Length;
				return;
			}
			this.m_length = 0U;
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06004B7B RID: 19323 RVA: 0x00126E20 File Offset: 0x00126220
		public bool IsPresent
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				return this.m_data != null;
			}
		}

		// Token: 0x06004B7C RID: 19324 RVA: 0x00126E38 File Offset: 0x00126238
		[SecurityCritical]
		public ushort GetUShort(int offset)
		{
			Invariant.Assert(this.m_data != null);
			if ((long)(offset + 1) >= (long)((ulong)this.m_length))
			{
				throw new FileFormatException();
			}
			return (ushort)(((int)this.m_data[offset] << 8) + (int)this.m_data[offset + 1]);
		}

		// Token: 0x06004B7D RID: 19325 RVA: 0x00126E7C File Offset: 0x0012627C
		[SecurityCritical]
		public short GetShort(int offset)
		{
			Invariant.Assert(this.m_data != null);
			if ((long)(offset + 1) >= (long)((ulong)this.m_length))
			{
				throw new FileFormatException();
			}
			return (short)(((int)this.m_data[offset] << 8) + (int)this.m_data[offset + 1]);
		}

		// Token: 0x06004B7E RID: 19326 RVA: 0x00126EC0 File Offset: 0x001262C0
		[SecurityCritical]
		public uint GetUInt(int offset)
		{
			Invariant.Assert(this.m_data != null);
			if ((long)(offset + 3) >= (long)((ulong)this.m_length))
			{
				throw new FileFormatException();
			}
			return (uint)(((int)this.m_data[offset] << 24) + ((int)this.m_data[offset + 1] << 16) + ((int)this.m_data[offset + 2] << 8) + (int)this.m_data[offset + 3]);
		}

		// Token: 0x06004B7F RID: 19327 RVA: 0x00126F20 File Offset: 0x00126320
		[SecurityCritical]
		public ushort GetOffset(int offset)
		{
			Invariant.Assert(this.m_data != null);
			if ((long)(offset + 1) >= (long)((ulong)this.m_length))
			{
				throw new FileFormatException();
			}
			return (ushort)(((int)this.m_data[offset] << 8) + (int)this.m_data[offset + 1]);
		}

		// Token: 0x04002087 RID: 8327
		public const int InvalidOffset = 2147483647;

		// Token: 0x04002088 RID: 8328
		public const int NullOffset = 0;

		// Token: 0x04002089 RID: 8329
		private byte[] m_data;

		// Token: 0x0400208A RID: 8330
		[SecurityCritical]
		private uint m_length;
	}
}
