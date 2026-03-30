using System;

namespace System.Windows.Input
{
	// Token: 0x0200023E RID: 574
	internal interface IMouseInputProvider : IInputProvider
	{
		// Token: 0x06000FEC RID: 4076
		bool SetCursor(Cursor cursor);

		// Token: 0x06000FED RID: 4077
		bool CaptureMouse();

		// Token: 0x06000FEE RID: 4078
		void ReleaseMouseCapture();

		// Token: 0x06000FEF RID: 4079
		int GetIntermediatePoints(IInputElement relativeTo, Point[] points);
	}
}
