using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace MS.Internal
{
	// Token: 0x02000674 RID: 1652
	internal sealed class StringCharacterBuffer : CharacterBuffer
	{
		// Token: 0x06004901 RID: 18689 RVA: 0x0011D0A8 File Offset: 0x0011C4A8
		public StringCharacterBuffer(string characterString)
		{
			if (characterString == null)
			{
				throw new ArgumentNullException("characterString");
			}
			this._string = characterString;
		}

		// Token: 0x17000F1D RID: 3869
		public override char this[int characterOffset]
		{
			get
			{
				return this._string[characterOffset];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06004904 RID: 18692 RVA: 0x0011D100 File Offset: 0x0011C500
		public override int Count
		{
			get
			{
				return this._string.Length;
			}
		}

		// Token: 0x06004905 RID: 18693 RVA: 0x0011D118 File Offset: 0x0011C518
		[SecurityCritical]
		public unsafe override char* GetCharacterPointer()
		{
			return null;
		}

		// Token: 0x06004906 RID: 18694 RVA: 0x0011D128 File Offset: 0x0011C528
		[SecurityCritical]
		public unsafe override IntPtr PinAndGetCharacterPointer(int offset, out GCHandle gcHandle)
		{
			gcHandle = GCHandle.Alloc(this._string, GCHandleType.Pinned);
			return new IntPtr((void*)((byte*)gcHandle.AddrOfPinnedObject().ToPointer() + (IntPtr)offset * 2));
		}

		// Token: 0x06004907 RID: 18695 RVA: 0x0011D160 File Offset: 0x0011C560
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public override void UnpinCharacterPointer(GCHandle gcHandle)
		{
			gcHandle.Free();
		}

		// Token: 0x06004908 RID: 18696 RVA: 0x0011D174 File Offset: 0x0011C574
		public override void AppendToStringBuilder(StringBuilder stringBuilder, int characterOffset, int characterLength)
		{
			if (characterLength < 0 || characterOffset + characterLength > this._string.Length)
			{
				characterLength = this._string.Length - characterOffset;
			}
			stringBuilder.Append(this._string, characterOffset, characterLength);
		}

		// Token: 0x04001CAF RID: 7343
		private string _string;
	}
}
