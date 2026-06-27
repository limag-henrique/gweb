using System;

namespace System.Windows.Media.Imaging
{
	// Token: 0x020005CC RID: 1484
	internal static class ValidateEnums
	{
		// Token: 0x0600434D RID: 17229 RVA: 0x00104E1C File Offset: 0x0010421C
		public static bool IsRotationValid(object valueObject)
		{
			Rotation rotation = (Rotation)valueObject;
			return rotation == Rotation.Rotate0 || rotation == Rotation.Rotate90 || rotation == Rotation.Rotate180 || rotation == Rotation.Rotate270;
		}
	}
}
