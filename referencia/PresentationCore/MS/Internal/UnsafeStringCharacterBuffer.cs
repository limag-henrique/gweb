using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using MS.Internal.PresentationCore;

namespace MS.Internal
{
	// Token: 0x02000675 RID: 1653
	internal sealed class UnsafeStringCharacterBuffer : CharacterBuffer
	{
		// Token: 0x06004909 RID: 18697 RVA: 0x0011D1B4 File Offset: 0x0011C5B4
		[SecurityCritical]
		public unsafe UnsafeStringCharacterBuffer(char* characterString, int length)
		{
			if (characterString == null)
			{
				throw new ArgumentNullException("characterString");
			}
			if (length <= 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.Get("ParameterValueMustBeGreaterThanZero"));
			}
			this._unsafeString = characterString;
			this._length = length;
		}

		// Token: 0x17000F1F RID: 3871
		public unsafe override char this[int characterOffset]
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				if (characterOffset >= this._length || characterOffset < 0)
				{
					throw new ArgumentOutOfRangeException("characterOffset", SR.Get("ParameterMustBeBetween", new object[]
					{
						0,
						this._length
					}));
				}
				return this._unsafeString[characterOffset];
			}
			[SecurityCritical]
			[SecurityTreatAsSafe]
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x0600490C RID: 18700 RVA: 0x0011D26C File Offset: 0x0011C66C
		public override int Count
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				return this._length;
			}
		}

		// Token: 0x0600490D RID: 18701 RVA: 0x0011D280 File Offset: 0x0011C680
		[SecurityCritical]
		public unsafe override char* GetCharacterPointer()
		{
			return this._unsafeString;
		}

		// Token: 0x0600490E RID: 18702 RVA: 0x0011D294 File Offset: 0x0011C694
		[SecurityCritical]
		public unsafe override IntPtr PinAndGetCharacterPointer(int offset, out GCHandle gcHandle)
		{
			gcHandle = default(GCHandle);
			return new IntPtr((void*)(this._unsafeString + offset));
		}

		// Token: 0x0600490F RID: 18703 RVA: 0x0011D2B8 File Offset: 0x0011C6B8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public override void UnpinCharacterPointer(GCHandle gcHandle)
		{
		}

		// Token: 0x06004910 RID: 18704 RVA: 0x0011D2C8 File Offset: 0x0011C6C8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public override void AppendToStringBuilder(StringBuilder stringBuilder, int characterOffset, int characterLength)
		{
			if (characterOffset >= this._length || characterOffset < 0)
			{
				throw new ArgumentOutOfRangeException("characterOffset", SR.Get("ParameterMustBeBetween", new object[]
				{
					0,
					this._length
				}));
			}
			if (characterLength < 0 || characterOffset + characterLength > this._length)
			{
				throw new ArgumentOutOfRangeException("characterLength", SR.Get("ParameterMustBeBetween", new object[]
				{
					0,
					this._length - characterOffset
				}));
			}
			stringBuilder.Append(new string(this._unsafeString, characterOffset, characterLength));
		}

		// Token: 0x04001CB0 RID: 7344
		[SecurityCritical]
		private unsafe char* _unsafeString;

		// Token: 0x04001CB1 RID: 7345
		[SecurityCritical]
		private int _length;
	}
}
