using System;
using System.Security;
using System.Windows;
using System.Windows.Media;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x0200000A RID: 10
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class DWriteTypeConverter
	{
		// Token: 0x06000242 RID: 578 RVA: 0x0000B66C File Offset: 0x0000AA6C
		internal static TextFormattingMode Convert(DWRITE_MEASURING_MODE dwriteMeasuringMode)
		{
			if (dwriteMeasuringMode == (DWRITE_MEASURING_MODE)0)
			{
				return TextFormattingMode.Ideal;
			}
			if (dwriteMeasuringMode == (DWRITE_MEASURING_MODE)1)
			{
				return TextFormattingMode.Display;
			}
			if (dwriteMeasuringMode != (DWRITE_MEASURING_MODE)2)
			{
				throw new InvalidOperationException();
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000AFF0 File Offset: 0x0000A3F0
		internal static DWRITE_MEASURING_MODE Convert(TextFormattingMode measuringMode)
		{
			if (measuringMode == TextFormattingMode.Ideal)
			{
				return (DWRITE_MEASURING_MODE)0;
			}
			if (measuringMode != TextFormattingMode.Display)
			{
				throw new InvalidOperationException();
			}
			return (DWRITE_MEASURING_MODE)1;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000B5DC File Offset: 0x0000A9DC
		internal static InformationalStringID Convert(DWRITE_INFORMATIONAL_STRING_ID dwriteInformationStringID)
		{
			switch (dwriteInformationStringID)
			{
			case (DWRITE_INFORMATIONAL_STRING_ID)0:
				return InformationalStringID.None;
			case (DWRITE_INFORMATIONAL_STRING_ID)1:
				return InformationalStringID.CopyrightNotice;
			case (DWRITE_INFORMATIONAL_STRING_ID)2:
				return InformationalStringID.VersionStrings;
			case (DWRITE_INFORMATIONAL_STRING_ID)3:
				return InformationalStringID.Trademark;
			case (DWRITE_INFORMATIONAL_STRING_ID)4:
				return InformationalStringID.Manufacturer;
			case (DWRITE_INFORMATIONAL_STRING_ID)5:
				return InformationalStringID.Designer;
			case (DWRITE_INFORMATIONAL_STRING_ID)6:
				return InformationalStringID.DesignerURL;
			case (DWRITE_INFORMATIONAL_STRING_ID)7:
				return InformationalStringID.Description;
			case (DWRITE_INFORMATIONAL_STRING_ID)8:
				return InformationalStringID.FontVendorURL;
			case (DWRITE_INFORMATIONAL_STRING_ID)9:
				return InformationalStringID.LicenseDescription;
			case (DWRITE_INFORMATIONAL_STRING_ID)10:
				return InformationalStringID.LicenseInfoURL;
			case (DWRITE_INFORMATIONAL_STRING_ID)11:
				return InformationalStringID.WIN32FamilyNames;
			case (DWRITE_INFORMATIONAL_STRING_ID)12:
				return InformationalStringID.Win32SubFamilyNames;
			case (DWRITE_INFORMATIONAL_STRING_ID)13:
				return InformationalStringID.PreferredFamilyNames;
			case (DWRITE_INFORMATIONAL_STRING_ID)14:
				return InformationalStringID.PreferredSubFamilyNames;
			case (DWRITE_INFORMATIONAL_STRING_ID)15:
				return InformationalStringID.SampleText;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000B558 File Offset: 0x0000A958
		internal static DWRITE_INFORMATIONAL_STRING_ID Convert(InformationalStringID informationStringID)
		{
			switch (informationStringID)
			{
			case InformationalStringID.None:
				return (DWRITE_INFORMATIONAL_STRING_ID)0;
			case InformationalStringID.CopyrightNotice:
				return (DWRITE_INFORMATIONAL_STRING_ID)1;
			case InformationalStringID.VersionStrings:
				return (DWRITE_INFORMATIONAL_STRING_ID)2;
			case InformationalStringID.Trademark:
				return (DWRITE_INFORMATIONAL_STRING_ID)3;
			case InformationalStringID.Manufacturer:
				return (DWRITE_INFORMATIONAL_STRING_ID)4;
			case InformationalStringID.Designer:
				return (DWRITE_INFORMATIONAL_STRING_ID)5;
			case InformationalStringID.DesignerURL:
				return (DWRITE_INFORMATIONAL_STRING_ID)6;
			case InformationalStringID.Description:
				return (DWRITE_INFORMATIONAL_STRING_ID)7;
			case InformationalStringID.FontVendorURL:
				return (DWRITE_INFORMATIONAL_STRING_ID)8;
			case InformationalStringID.LicenseDescription:
				return (DWRITE_INFORMATIONAL_STRING_ID)9;
			case InformationalStringID.LicenseInfoURL:
				return (DWRITE_INFORMATIONAL_STRING_ID)10;
			case InformationalStringID.WIN32FamilyNames:
				return (DWRITE_INFORMATIONAL_STRING_ID)11;
			case InformationalStringID.Win32SubFamilyNames:
				return (DWRITE_INFORMATIONAL_STRING_ID)12;
			case InformationalStringID.PreferredFamilyNames:
				return (DWRITE_INFORMATIONAL_STRING_ID)13;
			case InformationalStringID.PreferredSubFamilyNames:
				return (DWRITE_INFORMATIONAL_STRING_ID)14;
			case InformationalStringID.SampleText:
				return (DWRITE_INFORMATIONAL_STRING_ID)15;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000B528 File Offset: 0x0000A928
		internal unsafe static Point Convert(DWRITE_GLYPH_OFFSET dwriteGlyphOffset)
		{
			return new Point
			{
				X = dwriteGlyphOffset,
				Y = (double)(*(ref dwriteGlyphOffset + 4))
			};
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000B4A8 File Offset: 0x0000A8A8
		internal unsafe static ValueType Convert(DWRITE_MATRIX dwriteMatrix)
		{
			ValueType valueType = default(DWriteMatrix);
			((DWriteMatrix)valueType).Dx = *(ref dwriteMatrix + 16);
			((DWriteMatrix)valueType).Dy = *(ref dwriteMatrix + 20);
			((DWriteMatrix)valueType).M11 = dwriteMatrix;
			((DWriteMatrix)valueType).M12 = *(ref dwriteMatrix + 4);
			((DWriteMatrix)valueType).M21 = *(ref dwriteMatrix + 8);
			((DWriteMatrix)valueType).M22 = *(ref dwriteMatrix + 12);
			return valueType;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000B438 File Offset: 0x0000A838
		internal unsafe static DWRITE_MATRIX Convert(ValueType matrix)
		{
			DWRITE_MATRIX m;
			*(ref m + 16) = ((DWriteMatrix)matrix).Dx;
			*(ref m + 20) = ((DWriteMatrix)matrix).Dy;
			m = ((DWriteMatrix)matrix).M11;
			*(ref m + 4) = ((DWriteMatrix)matrix).M12;
			*(ref m + 8) = ((DWriteMatrix)matrix).M21;
			*(ref m + 12) = ((DWriteMatrix)matrix).M22;
			return m;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000B330 File Offset: 0x0000A730
		internal unsafe static FontMetrics Convert(DWRITE_FONT_METRICS dwriteFontMetrics)
		{
			return new FontMetrics
			{
				Ascent = *(ref dwriteFontMetrics + 2),
				CapHeight = *(ref dwriteFontMetrics + 8),
				Descent = *(ref dwriteFontMetrics + 4),
				DesignUnitsPerEm = dwriteFontMetrics,
				LineGap = *(ref dwriteFontMetrics + 6),
				StrikethroughPosition = *(ref dwriteFontMetrics + 16),
				StrikethroughThickness = *(ref dwriteFontMetrics + 18),
				UnderlinePosition = *(ref dwriteFontMetrics + 12),
				UnderlineThickness = *(ref dwriteFontMetrics + 14),
				XHeight = *(ref dwriteFontMetrics + 10)
			};
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000B3B8 File Offset: 0x0000A7B8
		internal unsafe static DWRITE_FONT_METRICS Convert(FontMetrics fontMetrics)
		{
			DWRITE_FONT_METRICS designUnitsPerEm;
			*(ref designUnitsPerEm + 2) = (short)fontMetrics.Ascent;
			*(ref designUnitsPerEm + 8) = (short)fontMetrics.CapHeight;
			*(ref designUnitsPerEm + 4) = (short)fontMetrics.Descent;
			designUnitsPerEm = fontMetrics.DesignUnitsPerEm;
			*(ref designUnitsPerEm + 6) = fontMetrics.LineGap;
			*(ref designUnitsPerEm + 16) = fontMetrics.StrikethroughPosition;
			*(ref designUnitsPerEm + 18) = (short)fontMetrics.StrikethroughThickness;
			*(ref designUnitsPerEm + 12) = fontMetrics.UnderlinePosition;
			*(ref designUnitsPerEm + 14) = (short)fontMetrics.UnderlineThickness;
			*(ref designUnitsPerEm + 10) = (short)fontMetrics.XHeight;
			return designUnitsPerEm;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000B30C File Offset: 0x0000A70C
		internal static DWRITE_FONT_STYLE Convert(FontStyle fontStyle)
		{
			if (fontStyle == FontStyle.Normal)
			{
				return (DWRITE_FONT_STYLE)0;
			}
			if (fontStyle == FontStyle.Oblique)
			{
				return (DWRITE_FONT_STYLE)1;
			}
			if (fontStyle != FontStyle.Italic)
			{
				throw new InvalidOperationException();
			}
			return (DWRITE_FONT_STYLE)2;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000B30C File Offset: 0x0000A70C
		internal static FontStyle Convert(DWRITE_FONT_STYLE fontStyle)
		{
			if (fontStyle == (DWRITE_FONT_STYLE)0)
			{
				return FontStyle.Normal;
			}
			if (fontStyle == (DWRITE_FONT_STYLE)1)
			{
				return FontStyle.Oblique;
			}
			if (fontStyle != (DWRITE_FONT_STYLE)2)
			{
				throw new InvalidOperationException();
			}
			return FontStyle.Italic;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000B2B0 File Offset: 0x0000A6B0
		internal static DWRITE_FONT_STRETCH Convert(FontStretch fontStrech)
		{
			switch (fontStrech)
			{
			case FontStretch.Undefined:
				return (DWRITE_FONT_STRETCH)0;
			case FontStretch.UltraCondensed:
				return (DWRITE_FONT_STRETCH)1;
			case FontStretch.ExtraCondensed:
				return (DWRITE_FONT_STRETCH)2;
			case FontStretch.Condensed:
				return (DWRITE_FONT_STRETCH)3;
			case FontStretch.SemiCondensed:
				return (DWRITE_FONT_STRETCH)4;
			case FontStretch.Normal:
				return (DWRITE_FONT_STRETCH)5;
			case FontStretch.SemiExpanded:
				return (DWRITE_FONT_STRETCH)6;
			case FontStretch.Expanded:
				return (DWRITE_FONT_STRETCH)7;
			case FontStretch.ExtraExpanded:
				return (DWRITE_FONT_STRETCH)8;
			case FontStretch.UltraExpanded:
				return (DWRITE_FONT_STRETCH)9;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000B2B0 File Offset: 0x0000A6B0
		internal static FontStretch Convert(DWRITE_FONT_STRETCH fontStrech)
		{
			switch (fontStrech)
			{
			case (DWRITE_FONT_STRETCH)0:
				return FontStretch.Undefined;
			case (DWRITE_FONT_STRETCH)1:
				return FontStretch.UltraCondensed;
			case (DWRITE_FONT_STRETCH)2:
				return FontStretch.ExtraCondensed;
			case (DWRITE_FONT_STRETCH)3:
				return FontStretch.Condensed;
			case (DWRITE_FONT_STRETCH)4:
				return FontStretch.SemiCondensed;
			case (DWRITE_FONT_STRETCH)5:
				return FontStretch.Normal;
			case (DWRITE_FONT_STRETCH)6:
				return FontStretch.SemiExpanded;
			case (DWRITE_FONT_STRETCH)7:
				return FontStretch.Expanded;
			case (DWRITE_FONT_STRETCH)8:
				return FontStretch.ExtraExpanded;
			case (DWRITE_FONT_STRETCH)9:
				return FontStretch.UltraExpanded;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000B218 File Offset: 0x0000A618
		internal static FontFaceType Convert(DWRITE_FONT_FACE_TYPE fontFaceType)
		{
			switch (fontFaceType)
			{
			case (DWRITE_FONT_FACE_TYPE)0:
				return FontFaceType.CFF;
			case (DWRITE_FONT_FACE_TYPE)1:
				return FontFaceType.TrueType;
			case (DWRITE_FONT_FACE_TYPE)2:
				return FontFaceType.TrueTypeCollection;
			case (DWRITE_FONT_FACE_TYPE)3:
				return FontFaceType.Type1;
			case (DWRITE_FONT_FACE_TYPE)4:
				return FontFaceType.Vector;
			case (DWRITE_FONT_FACE_TYPE)5:
				return FontFaceType.Bitmap;
			case (DWRITE_FONT_FACE_TYPE)6:
				return FontFaceType.Unknown;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000B1D0 File Offset: 0x0000A5D0
		internal static DWRITE_FONT_FACE_TYPE Convert(FontFaceType fontFaceType)
		{
			switch (fontFaceType)
			{
			case FontFaceType.CFF:
				return (DWRITE_FONT_FACE_TYPE)0;
			case FontFaceType.TrueType:
				return (DWRITE_FONT_FACE_TYPE)1;
			case FontFaceType.TrueTypeCollection:
				return (DWRITE_FONT_FACE_TYPE)2;
			case FontFaceType.Type1:
				return (DWRITE_FONT_FACE_TYPE)3;
			case FontFaceType.Vector:
				return (DWRITE_FONT_FACE_TYPE)4;
			case FontFaceType.Bitmap:
				return (DWRITE_FONT_FACE_TYPE)5;
			case FontFaceType.Unknown:
				return (DWRITE_FONT_FACE_TYPE)6;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000B1A0 File Offset: 0x0000A5A0
		internal static byte Convert(FontSimulations fontSimulations)
		{
			sbyte b = (sbyte)fontSimulations;
			int num = b;
			if (num == 0)
			{
				return 0;
			}
			if (num == 1)
			{
				return 1;
			}
			if (num == 2)
			{
				return 2;
			}
			if (num != 3)
			{
				throw new InvalidOperationException();
			}
			return 3;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000B170 File Offset: 0x0000A570
		internal static FontSimulations Convert(DWRITE_FONT_SIMULATIONS fontSimulations)
		{
			sbyte b = (sbyte)fontSimulations;
			int num = b;
			if (num == 0)
			{
				return FontSimulations.None;
			}
			if (num == 1)
			{
				return FontSimulations.Bold;
			}
			if (num == 2)
			{
				return FontSimulations.Oblique;
			}
			if (b == 3)
			{
				return FontSimulations.Bold | FontSimulations.Oblique;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000B264 File Offset: 0x0000A664
		internal static FontFileType Convert(DWRITE_FONT_FILE_TYPE dwriteFontFileType)
		{
			switch (dwriteFontFileType)
			{
			case (DWRITE_FONT_FILE_TYPE)0:
				return FontFileType.Unknown;
			case (DWRITE_FONT_FILE_TYPE)1:
				return FontFileType.CFF;
			case (DWRITE_FONT_FILE_TYPE)2:
				return FontFileType.TrueType;
			case (DWRITE_FONT_FILE_TYPE)3:
				return FontFileType.TrueTypeCollection;
			case (DWRITE_FONT_FILE_TYPE)4:
				return FontFileType.Type1PFM;
			case (DWRITE_FONT_FILE_TYPE)5:
				return FontFileType.Type1PFB;
			case (DWRITE_FONT_FILE_TYPE)6:
				return FontFileType.Vector;
			case (DWRITE_FONT_FILE_TYPE)7:
				return FontFileType.Bitmap;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000B0C4 File Offset: 0x0000A4C4
		internal static DWRITE_FONT_WEIGHT Convert(FontWeight fontWeight)
		{
			if (fontWeight <= FontWeight.DemiBold)
			{
				if (fontWeight == FontWeight.DemiBold)
				{
					return (DWRITE_FONT_WEIGHT)600;
				}
				if (fontWeight == FontWeight.Thin)
				{
					return (DWRITE_FONT_WEIGHT)100;
				}
				if (fontWeight == FontWeight.ExtraLight)
				{
					return (DWRITE_FONT_WEIGHT)200;
				}
				if (fontWeight == FontWeight.Light)
				{
					return (DWRITE_FONT_WEIGHT)300;
				}
				if (fontWeight == FontWeight.Normal)
				{
					return (DWRITE_FONT_WEIGHT)400;
				}
				if (fontWeight == FontWeight.Medium)
				{
					return (DWRITE_FONT_WEIGHT)500;
				}
			}
			else
			{
				if (fontWeight == FontWeight.Bold)
				{
					return (DWRITE_FONT_WEIGHT)700;
				}
				if (fontWeight == FontWeight.ExtraBold)
				{
					return (DWRITE_FONT_WEIGHT)800;
				}
				if (fontWeight == FontWeight.Black)
				{
					return (DWRITE_FONT_WEIGHT)900;
				}
				if (fontWeight == FontWeight.ExtraBlack)
				{
					return (DWRITE_FONT_WEIGHT)950;
				}
			}
			if (fontWeight - (FontWeight)1 <= 998)
			{
				return (DWRITE_FONT_WEIGHT)fontWeight;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000B010 File Offset: 0x0000A410
		internal static FontWeight Convert(DWRITE_FONT_WEIGHT fontWeight)
		{
			if (fontWeight <= (DWRITE_FONT_WEIGHT)500)
			{
				if (fontWeight == (DWRITE_FONT_WEIGHT)500)
				{
					return FontWeight.Medium;
				}
				if (fontWeight == (DWRITE_FONT_WEIGHT)100)
				{
					return FontWeight.Thin;
				}
				if (fontWeight == (DWRITE_FONT_WEIGHT)200)
				{
					return FontWeight.ExtraLight;
				}
				if (fontWeight == (DWRITE_FONT_WEIGHT)300)
				{
					return FontWeight.Light;
				}
				if (fontWeight != (DWRITE_FONT_WEIGHT)350 && fontWeight == (DWRITE_FONT_WEIGHT)400)
				{
					return FontWeight.Normal;
				}
			}
			else
			{
				if (fontWeight == (DWRITE_FONT_WEIGHT)600)
				{
					return FontWeight.DemiBold;
				}
				if (fontWeight == (DWRITE_FONT_WEIGHT)700)
				{
					return FontWeight.Bold;
				}
				if (fontWeight == (DWRITE_FONT_WEIGHT)800)
				{
					return FontWeight.ExtraBold;
				}
				if (fontWeight == (DWRITE_FONT_WEIGHT)900)
				{
					return FontWeight.Black;
				}
				if (fontWeight == (DWRITE_FONT_WEIGHT)950)
				{
					return FontWeight.ExtraBlack;
				}
			}
			if (fontWeight - (DWRITE_FONT_WEIGHT)1 <= 998)
			{
				return (FontWeight)fontWeight;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000AFF0 File Offset: 0x0000A3F0
		internal static DWRITE_FACTORY_TYPE Convert(FactoryType factoryType)
		{
			if (factoryType == FactoryType.Shared)
			{
				return (DWRITE_FACTORY_TYPE)0;
			}
			if (factoryType != FactoryType.Isolated)
			{
				throw new InvalidOperationException();
			}
			return (DWRITE_FACTORY_TYPE)1;
		}
	}
}
