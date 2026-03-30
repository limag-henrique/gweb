using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006ED RID: 1773
	internal class UnsafeUshortArray : UshortBuffer
	{
		// Token: 0x06004C77 RID: 19575 RVA: 0x0012BC88 File Offset: 0x0012B088
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal UnsafeUshortArray(CheckedUShortPointer array, int arrayLength)
		{
			this._array = array.Probe(0, arrayLength);
			this._arrayLength.Value = arrayLength;
		}

		// Token: 0x17000FAD RID: 4013
		public unsafe override ushort this[int index]
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				Invariant.Assert(index >= 0 && index < this._arrayLength.Value);
				return this._array[index];
			}
			[SecurityTreatAsSafe]
			[SecurityCritical]
			set
			{
				Invariant.Assert(index >= 0 && index < this._arrayLength.Value);
				this._array[index] = value;
			}
		}

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x06004C7A RID: 19578 RVA: 0x0012BD20 File Offset: 0x0012B120
		public override int Length
		{
			get
			{
				return this._arrayLength.Value;
			}
		}

		// Token: 0x04002137 RID: 8503
		[SecurityCritical]
		private unsafe ushort* _array;

		// Token: 0x04002138 RID: 8504
		private SecurityCriticalDataForSet<int> _arrayLength;
	}
}
