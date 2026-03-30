using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using MS.Internal.PresentationCore;

namespace MS.Internal.Media
{
	// Token: 0x020006F8 RID: 1784
	internal static class XamlSerializationHelper
	{
		// Token: 0x06004CD7 RID: 19671 RVA: 0x0012F0C0 File Offset: 0x0012E4C0
		[FriendAccessAllowed]
		internal static bool SerializePoint3D(BinaryWriter writer, string stringValues)
		{
			Point3DCollection point3DCollection = Point3DCollection.Parse(stringValues);
			writer.Write((uint)point3DCollection.Count);
			for (int i = 0; i < point3DCollection.Count; i++)
			{
				Point3D point3D = point3DCollection[i];
				XamlSerializationHelper.WriteDouble(writer, point3D.X);
				XamlSerializationHelper.WriteDouble(writer, point3D.Y);
				XamlSerializationHelper.WriteDouble(writer, point3D.Z);
			}
			return true;
		}

		// Token: 0x06004CD8 RID: 19672 RVA: 0x0012F124 File Offset: 0x0012E524
		[FriendAccessAllowed]
		internal static bool SerializeVector3D(BinaryWriter writer, string stringValues)
		{
			Vector3DCollection vector3DCollection = Vector3DCollection.Parse(stringValues);
			writer.Write((uint)vector3DCollection.Count);
			for (int i = 0; i < vector3DCollection.Count; i++)
			{
				Vector3D vector3D = vector3DCollection[i];
				XamlSerializationHelper.WriteDouble(writer, vector3D.X);
				XamlSerializationHelper.WriteDouble(writer, vector3D.Y);
				XamlSerializationHelper.WriteDouble(writer, vector3D.Z);
			}
			return true;
		}

		// Token: 0x06004CD9 RID: 19673 RVA: 0x0012F188 File Offset: 0x0012E588
		[FriendAccessAllowed]
		internal static bool SerializePoint(BinaryWriter writer, string stringValue)
		{
			PointCollection pointCollection = PointCollection.Parse(stringValue);
			writer.Write((uint)pointCollection.Count);
			for (int i = 0; i < pointCollection.Count; i++)
			{
				Point point = pointCollection[i];
				XamlSerializationHelper.WriteDouble(writer, point.X);
				XamlSerializationHelper.WriteDouble(writer, point.Y);
			}
			return true;
		}

		// Token: 0x06004CDA RID: 19674 RVA: 0x0012F1DC File Offset: 0x0012E5DC
		internal static void WriteDouble(BinaryWriter writer, double value)
		{
			if (value == 0.0)
			{
				writer.Write(1);
				return;
			}
			if (value == 1.0)
			{
				writer.Write(2);
				return;
			}
			if (value == -1.0)
			{
				writer.Write(3);
				return;
			}
			int value2 = 0;
			if (XamlSerializationHelper.CanConvertToInteger(value, ref value2))
			{
				writer.Write(4);
				writer.Write(value2);
				return;
			}
			writer.Write(5);
			writer.Write(value);
		}

		// Token: 0x06004CDB RID: 19675 RVA: 0x0012F250 File Offset: 0x0012E650
		internal static double ReadDouble(BinaryReader reader)
		{
			switch (reader.ReadByte())
			{
			case 1:
				return 0.0;
			case 2:
				return 1.0;
			case 3:
				return -1.0;
			case 4:
				return XamlSerializationHelper.ReadScaledInteger(reader);
			case 5:
				return reader.ReadDouble();
			default:
				throw new ArgumentException(SR.Get("FloatUnknownBamlType"));
			}
		}

		// Token: 0x06004CDC RID: 19676 RVA: 0x0012F2C0 File Offset: 0x0012E6C0
		internal static double ReadScaledInteger(BinaryReader reader)
		{
			double num = (double)reader.ReadInt32();
			return num * 1E-06;
		}

		// Token: 0x06004CDD RID: 19677 RVA: 0x0012F2E4 File Offset: 0x0012E6E4
		internal static bool CanConvertToInteger(double doubleValue, ref int intValue)
		{
			double num = doubleValue * 1000000.0;
			double num2 = Math.Floor(num);
			if (num2 > 2147483647.0 || num2 < -2147483648.0)
			{
				return false;
			}
			if (num - num2 > 4.94065645841247E-324)
			{
				return false;
			}
			intValue = (int)num;
			return true;
		}

		// Token: 0x0400215D RID: 8541
		private const double scaleFactor = 1000000.0;

		// Token: 0x0400215E RID: 8542
		private const double inverseScaleFactor = 1E-06;

		// Token: 0x020009CE RID: 2510
		internal enum SerializationFloatType : byte
		{
			// Token: 0x04002E03 RID: 11779
			Unknown,
			// Token: 0x04002E04 RID: 11780
			Zero,
			// Token: 0x04002E05 RID: 11781
			One,
			// Token: 0x04002E06 RID: 11782
			MinusOne,
			// Token: 0x04002E07 RID: 11783
			ScaledInteger,
			// Token: 0x04002E08 RID: 11784
			Double,
			// Token: 0x04002E09 RID: 11785
			Other
		}
	}
}
