using System;

namespace System.Windows
{
	// Token: 0x02000188 RID: 392
	internal static class ValidateEnums
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x000159F4 File Offset: 0x00014DF4
		public static bool IsTextDecorationLocationValid(object valueObject)
		{
			TextDecorationLocation textDecorationLocation = (TextDecorationLocation)valueObject;
			return textDecorationLocation == TextDecorationLocation.Underline || textDecorationLocation == TextDecorationLocation.OverLine || textDecorationLocation == TextDecorationLocation.Strikethrough || textDecorationLocation == TextDecorationLocation.Baseline;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00015A1C File Offset: 0x00014E1C
		public static bool IsTextDecorationUnitValid(object valueObject)
		{
			TextDecorationUnit textDecorationUnit = (TextDecorationUnit)valueObject;
			return textDecorationUnit == TextDecorationUnit.FontRecommended || textDecorationUnit == TextDecorationUnit.FontRenderingEmSize || textDecorationUnit == TextDecorationUnit.Pixel;
		}
	}
}
