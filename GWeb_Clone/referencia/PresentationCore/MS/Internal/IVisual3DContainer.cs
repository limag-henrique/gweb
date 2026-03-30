using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace MS.Internal
{
	// Token: 0x0200068F RID: 1679
	internal interface IVisual3DContainer
	{
		// Token: 0x060049D1 RID: 18897
		void AddChild(Visual3D child);

		// Token: 0x060049D2 RID: 18898
		void RemoveChild(Visual3D child);

		// Token: 0x060049D3 RID: 18899
		int GetChildrenCount();

		// Token: 0x060049D4 RID: 18900
		Visual3D GetChild(int index);

		// Token: 0x060049D5 RID: 18901
		void VerifyAPIReadOnly();

		// Token: 0x060049D6 RID: 18902
		void VerifyAPIReadOnly(DependencyObject other);

		// Token: 0x060049D7 RID: 18903
		void VerifyAPIReadWrite();

		// Token: 0x060049D8 RID: 18904
		void VerifyAPIReadWrite(DependencyObject other);
	}
}
