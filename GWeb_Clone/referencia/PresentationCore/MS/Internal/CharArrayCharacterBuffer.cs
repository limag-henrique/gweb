using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace MS.Internal
{
	// Token: 0x02000673 RID: 1651
	internal sealed class CharArrayCharacterBuffer : CharacterBuffer
	{
		// Token: 0x060048F9 RID: 18681 RVA: 0x0011CFA4 File Offset: 0x0011C3A4
		public CharArrayCharacterBuffer(char[] characterArray)
		{
			if (characterArray == null)
			{
				throw new ArgumentNullException("characterArray");
			}
			this._characterArray = characterArray;
		}

		// Token: 0x17000F1B RID: 3867
		public override char this[int characterOffset]
		{
			get
			{
				return this._characterArray[characterOffset];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x060048FC RID: 18684 RVA: 0x0011CFF8 File Offset: 0x0011C3F8
		public override int Count
		{
			get
			{
				return this._characterArray.Length;
			}
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x0011D010 File Offset: 0x0011C410
		[SecurityCritical]
		public unsafe override char* GetCharacterPointer()
		{
			return null;
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x0011D020 File Offset: 0x0011C420
		[SecurityCritical]
		public unsafe override IntPtr PinAndGetCharacterPointer(int offset, out GCHandle gcHandle)
		{
			gcHandle = GCHandle.Alloc(this._characterArray, GCHandleType.Pinned);
			return new IntPtr((void*)((byte*)gcHandle.AddrOfPinnedObject().ToPointer() + (IntPtr)offset * 2));
		}

		// Token: 0x060048FF RID: 18687 RVA: 0x0011D058 File Offset: 0x0011C458
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public override void UnpinCharacterPointer(GCHandle gcHandle)
		{
			gcHandle.Free();
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x0011D06C File Offset: 0x0011C46C
		public override void AppendToStringBuilder(StringBuilder stringBuilder, int characterOffset, int characterLength)
		{
			if (characterLength < 0 || characterOffset + characterLength > this._characterArray.Length)
			{
				characterLength = this._characterArray.Length - characterOffset;
			}
			stringBuilder.Append(this._characterArray, characterOffset, characterLength);
		}

		// Token: 0x04001CAE RID: 7342
		private char[] _characterArray;
	}
}
