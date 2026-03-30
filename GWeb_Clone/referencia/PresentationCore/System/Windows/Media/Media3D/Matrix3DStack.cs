using System;
using System.Collections.Generic;

namespace System.Windows.Media.Media3D
{
	// Token: 0x02000464 RID: 1124
	internal class Matrix3DStack
	{
		// Token: 0x06002F2C RID: 12076 RVA: 0x000BD308 File Offset: 0x000BC708
		public void Clear()
		{
			this._stack.Clear();
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x000BD320 File Offset: 0x000BC720
		public Matrix3D Pop()
		{
			Matrix3D top = this.Top;
			this._stack.RemoveAt(this._stack.Count - 1);
			return top;
		}

		// Token: 0x06002F2E RID: 12078 RVA: 0x000BD350 File Offset: 0x000BC750
		public void Push(Matrix3D matrix)
		{
			if (this._stack.Count > 0)
			{
				matrix.Append(this.Top);
			}
			this._stack.Add(matrix);
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06002F2F RID: 12079 RVA: 0x000BD384 File Offset: 0x000BC784
		public int Count
		{
			get
			{
				return this._stack.Count;
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06002F30 RID: 12080 RVA: 0x000BD39C File Offset: 0x000BC79C
		public bool IsEmpty
		{
			get
			{
				return this._stack.Count == 0;
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06002F31 RID: 12081 RVA: 0x000BD3B8 File Offset: 0x000BC7B8
		public Matrix3D Top
		{
			get
			{
				return this._stack[this._stack.Count - 1];
			}
		}

		// Token: 0x04001521 RID: 5409
		private readonly List<Matrix3D> _stack = new List<Matrix3D>();
	}
}
