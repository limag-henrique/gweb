using System;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007D9 RID: 2009
	internal abstract class DataXform
	{
		// Token: 0x06005491 RID: 21649
		internal abstract void Transform(int data, ref int xfData, ref int extra);

		// Token: 0x06005492 RID: 21650
		internal abstract void ResetState();

		// Token: 0x06005493 RID: 21651
		internal abstract int InverseTransform(int xfData, int extra);
	}
}
