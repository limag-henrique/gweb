using System;

namespace System.Windows.Ink
{
	// Token: 0x02000342 RID: 834
	internal static class ApplicationGestureHelper
	{
		// Token: 0x06001C48 RID: 7240 RVA: 0x00073754 File Offset: 0x00072B54
		internal static bool IsDefined(ApplicationGesture applicationGesture)
		{
			if (applicationGesture <= ApplicationGesture.SemicircleRight)
			{
				if (applicationGesture <= ApplicationGesture.Check)
				{
					if (applicationGesture != ApplicationGesture.AllGestures && applicationGesture - ApplicationGesture.NoGesture > 5)
					{
						return false;
					}
				}
				else if (applicationGesture - ApplicationGesture.Curlicue > 1 && applicationGesture - ApplicationGesture.Circle > 1 && applicationGesture - ApplicationGesture.SemicircleLeft > 1)
				{
					return false;
				}
			}
			else if (applicationGesture <= ApplicationGesture.Right)
			{
				if (applicationGesture - ApplicationGesture.ChevronUp > 3 && applicationGesture - ApplicationGesture.ArrowUp > 3 && applicationGesture - ApplicationGesture.Up > 3)
				{
					return false;
				}
			}
			else if (applicationGesture - ApplicationGesture.UpDown > 15 && applicationGesture != ApplicationGesture.Exclamation && applicationGesture - ApplicationGesture.Tap > 1)
			{
				return false;
			}
			return true;
		}

		// Token: 0x04000F77 RID: 3959
		internal static readonly int CountOfValues = 44;
	}
}
