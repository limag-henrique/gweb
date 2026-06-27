using System;
using System.Collections;
using MS.Internal.PresentationCore;

namespace MS.Internal
{
	// Token: 0x0200067A RID: 1658
	internal class GenericEnumerator : IEnumerator
	{
		// Token: 0x06004930 RID: 18736 RVA: 0x0011D998 File Offset: 0x0011CD98
		private GenericEnumerator()
		{
		}

		// Token: 0x06004931 RID: 18737 RVA: 0x0011D9AC File Offset: 0x0011CDAC
		internal GenericEnumerator(IList array, GenericEnumerator.GetGenerationIDDelegate getGenerationID)
		{
			this._array = array;
			this._count = this._array.Count;
			this._position = -1;
			this._getGenerationID = getGenerationID;
			this._originalGenerationID = this._getGenerationID();
		}

		// Token: 0x06004932 RID: 18738 RVA: 0x0011D9F8 File Offset: 0x0011CDF8
		private void VerifyCurrent()
		{
			if (-1 == this._position || this._position >= this._count)
			{
				throw new InvalidOperationException(SR.Get("Enumerator_VerifyContext"));
			}
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06004933 RID: 18739 RVA: 0x0011DA2C File Offset: 0x0011CE2C
		object IEnumerator.Current
		{
			get
			{
				this.VerifyCurrent();
				return this._current;
			}
		}

		// Token: 0x06004934 RID: 18740 RVA: 0x0011DA48 File Offset: 0x0011CE48
		public bool MoveNext()
		{
			if (this._getGenerationID() != this._originalGenerationID)
			{
				throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
			}
			this._position++;
			if (this._position >= this._count)
			{
				this._position = this._count;
				return false;
			}
			this._current = this._array[this._position];
			return true;
		}

		// Token: 0x06004935 RID: 18741 RVA: 0x0011DABC File Offset: 0x0011CEBC
		public void Reset()
		{
			if (this._getGenerationID() != this._originalGenerationID)
			{
				throw new InvalidOperationException(SR.Get("Enumerator_CollectionChanged"));
			}
			this._position = -1;
		}

		// Token: 0x04001CB6 RID: 7350
		private IList _array;

		// Token: 0x04001CB7 RID: 7351
		private object _current;

		// Token: 0x04001CB8 RID: 7352
		private int _count;

		// Token: 0x04001CB9 RID: 7353
		private int _position;

		// Token: 0x04001CBA RID: 7354
		private int _originalGenerationID;

		// Token: 0x04001CBB RID: 7355
		private GenericEnumerator.GetGenerationIDDelegate _getGenerationID;

		// Token: 0x020009A4 RID: 2468
		// (Invoke) Token: 0x06005A48 RID: 23112
		internal delegate int GetGenerationIDDelegate();
	}
}
