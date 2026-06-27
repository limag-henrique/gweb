using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MS.Internal.PresentationCore
{
	// Token: 0x020007EB RID: 2027
	internal static class AnimatedTypeHelpers
	{
		// Token: 0x060054EB RID: 21739 RVA: 0x0015E5A8 File Offset: 0x0015D9A8
		internal static byte InterpolateByte(byte from, byte to, double progress)
		{
			return (byte)((int)from + (int)(((double)(to - from) + 0.5) * progress));
		}

		// Token: 0x060054EC RID: 21740 RVA: 0x0015E5CC File Offset: 0x0015D9CC
		internal static Color InterpolateColor(Color from, Color to, double progress)
		{
			return from + (to - from) * (float)progress;
		}

		// Token: 0x060054ED RID: 21741 RVA: 0x0015E5F0 File Offset: 0x0015D9F0
		internal static decimal InterpolateDecimal(decimal from, decimal to, double progress)
		{
			return from + (to - from) * (decimal)progress;
		}

		// Token: 0x060054EE RID: 21742 RVA: 0x0015E618 File Offset: 0x0015DA18
		internal static double InterpolateDouble(double from, double to, double progress)
		{
			return from + (to - from) * progress;
		}

		// Token: 0x060054EF RID: 21743 RVA: 0x0015E62C File Offset: 0x0015DA2C
		internal static short InterpolateInt16(short from, short to, double progress)
		{
			if (progress == 0.0)
			{
				return from;
			}
			if (progress == 1.0)
			{
				return to;
			}
			double num = (double)(to - from);
			num *= progress;
			num += ((num > 0.0) ? 0.5 : -0.5);
			return from + (short)num;
		}

		// Token: 0x060054F0 RID: 21744 RVA: 0x0015E688 File Offset: 0x0015DA88
		internal static int InterpolateInt32(int from, int to, double progress)
		{
			if (progress == 0.0)
			{
				return from;
			}
			if (progress == 1.0)
			{
				return to;
			}
			double num = (double)(to - from);
			num *= progress;
			num += ((num > 0.0) ? 0.5 : -0.5);
			return from + (int)num;
		}

		// Token: 0x060054F1 RID: 21745 RVA: 0x0015E6E4 File Offset: 0x0015DAE4
		internal static long InterpolateInt64(long from, long to, double progress)
		{
			if (progress == 0.0)
			{
				return from;
			}
			if (progress == 1.0)
			{
				return to;
			}
			double num = (double)(to - from);
			num *= progress;
			num += ((num > 0.0) ? 0.5 : -0.5);
			return from + (long)num;
		}

		// Token: 0x060054F2 RID: 21746 RVA: 0x0015E740 File Offset: 0x0015DB40
		internal static Point InterpolatePoint(Point from, Point to, double progress)
		{
			return from + (to - from) * progress;
		}

		// Token: 0x060054F3 RID: 21747 RVA: 0x0015E760 File Offset: 0x0015DB60
		internal static Point3D InterpolatePoint3D(Point3D from, Point3D to, double progress)
		{
			return from + (to - from) * progress;
		}

		// Token: 0x060054F4 RID: 21748 RVA: 0x0015E780 File Offset: 0x0015DB80
		internal static Quaternion InterpolateQuaternion(Quaternion from, Quaternion to, double progress, bool useShortestPath)
		{
			return Quaternion.Slerp(from, to, progress, useShortestPath);
		}

		// Token: 0x060054F5 RID: 21749 RVA: 0x0015E798 File Offset: 0x0015DB98
		internal static Rect InterpolateRect(Rect from, Rect to, double progress)
		{
			return new Rect
			{
				Location = new Point(from.Location.X + (to.Location.X - from.Location.X) * progress, from.Location.Y + (to.Location.Y - from.Location.Y) * progress),
				Size = new Size(from.Size.Width + (to.Size.Width - from.Size.Width) * progress, from.Size.Height + (to.Size.Height - from.Size.Height) * progress)
			};
		}

		// Token: 0x060054F6 RID: 21750 RVA: 0x0015E88C File Offset: 0x0015DC8C
		internal static Rotation3D InterpolateRotation3D(Rotation3D from, Rotation3D to, double progress)
		{
			return new QuaternionRotation3D(AnimatedTypeHelpers.InterpolateQuaternion(from.InternalQuaternion, to.InternalQuaternion, progress, true));
		}

		// Token: 0x060054F7 RID: 21751 RVA: 0x0015E8B4 File Offset: 0x0015DCB4
		internal static float InterpolateSingle(float from, float to, double progress)
		{
			return from + (float)((double)(to - from) * progress);
		}

		// Token: 0x060054F8 RID: 21752 RVA: 0x0015E8CC File Offset: 0x0015DCCC
		internal static Size InterpolateSize(Size from, Size to, double progress)
		{
			return (Size)AnimatedTypeHelpers.InterpolateVector((Vector)from, (Vector)to, progress);
		}

		// Token: 0x060054F9 RID: 21753 RVA: 0x0015E8F0 File Offset: 0x0015DCF0
		internal static Vector InterpolateVector(Vector from, Vector to, double progress)
		{
			return from + (to - from) * progress;
		}

		// Token: 0x060054FA RID: 21754 RVA: 0x0015E910 File Offset: 0x0015DD10
		internal static Vector3D InterpolateVector3D(Vector3D from, Vector3D to, double progress)
		{
			return from + (to - from) * progress;
		}

		// Token: 0x060054FB RID: 21755 RVA: 0x0015E930 File Offset: 0x0015DD30
		internal static byte AddByte(byte value1, byte value2)
		{
			return value1 + value2;
		}

		// Token: 0x060054FC RID: 21756 RVA: 0x0015E944 File Offset: 0x0015DD44
		internal static Color AddColor(Color value1, Color value2)
		{
			return value1 + value2;
		}

		// Token: 0x060054FD RID: 21757 RVA: 0x0015E958 File Offset: 0x0015DD58
		internal static decimal AddDecimal(decimal value1, decimal value2)
		{
			return value1 + value2;
		}

		// Token: 0x060054FE RID: 21758 RVA: 0x0015E96C File Offset: 0x0015DD6C
		internal static double AddDouble(double value1, double value2)
		{
			return value1 + value2;
		}

		// Token: 0x060054FF RID: 21759 RVA: 0x0015E97C File Offset: 0x0015DD7C
		internal static short AddInt16(short value1, short value2)
		{
			return value1 + value2;
		}

		// Token: 0x06005500 RID: 21760 RVA: 0x0015E990 File Offset: 0x0015DD90
		internal static int AddInt32(int value1, int value2)
		{
			return value1 + value2;
		}

		// Token: 0x06005501 RID: 21761 RVA: 0x0015E9A0 File Offset: 0x0015DDA0
		internal static long AddInt64(long value1, long value2)
		{
			return value1 + value2;
		}

		// Token: 0x06005502 RID: 21762 RVA: 0x0015E9B0 File Offset: 0x0015DDB0
		internal static Point AddPoint(Point value1, Point value2)
		{
			return new Point(value1.X + value2.X, value1.Y + value2.Y);
		}

		// Token: 0x06005503 RID: 21763 RVA: 0x0015E9E0 File Offset: 0x0015DDE0
		internal static Point3D AddPoint3D(Point3D value1, Point3D value2)
		{
			return new Point3D(value1.X + value2.X, value1.Y + value2.Y, value1.Z + value2.Z);
		}

		// Token: 0x06005504 RID: 21764 RVA: 0x0015EA20 File Offset: 0x0015DE20
		internal static Quaternion AddQuaternion(Quaternion value1, Quaternion value2)
		{
			return value1 * value2;
		}

		// Token: 0x06005505 RID: 21765 RVA: 0x0015EA34 File Offset: 0x0015DE34
		internal static float AddSingle(float value1, float value2)
		{
			return value1 + value2;
		}

		// Token: 0x06005506 RID: 21766 RVA: 0x0015EA44 File Offset: 0x0015DE44
		internal static Size AddSize(Size value1, Size value2)
		{
			return new Size(value1.Width + value2.Width, value1.Height + value2.Height);
		}

		// Token: 0x06005507 RID: 21767 RVA: 0x0015EA74 File Offset: 0x0015DE74
		internal static Vector AddVector(Vector value1, Vector value2)
		{
			return value1 + value2;
		}

		// Token: 0x06005508 RID: 21768 RVA: 0x0015EA88 File Offset: 0x0015DE88
		internal static Vector3D AddVector3D(Vector3D value1, Vector3D value2)
		{
			return value1 + value2;
		}

		// Token: 0x06005509 RID: 21769 RVA: 0x0015EA9C File Offset: 0x0015DE9C
		internal static Rect AddRect(Rect value1, Rect value2)
		{
			return new Rect(AnimatedTypeHelpers.AddPoint(value1.Location, value2.Location), AnimatedTypeHelpers.AddSize(value1.Size, value2.Size));
		}

		// Token: 0x0600550A RID: 21770 RVA: 0x0015EAD4 File Offset: 0x0015DED4
		internal static Rotation3D AddRotation3D(Rotation3D value1, Rotation3D value2)
		{
			if (value1 == null)
			{
				value1 = Rotation3D.Identity;
			}
			if (value2 == null)
			{
				value2 = Rotation3D.Identity;
			}
			return new QuaternionRotation3D(AnimatedTypeHelpers.AddQuaternion(value1.InternalQuaternion, value2.InternalQuaternion));
		}

		// Token: 0x0600550B RID: 21771 RVA: 0x0015EB0C File Offset: 0x0015DF0C
		internal static byte SubtractByte(byte value1, byte value2)
		{
			return value1 - value2;
		}

		// Token: 0x0600550C RID: 21772 RVA: 0x0015EB20 File Offset: 0x0015DF20
		internal static Color SubtractColor(Color value1, Color value2)
		{
			return value1 - value2;
		}

		// Token: 0x0600550D RID: 21773 RVA: 0x0015EB34 File Offset: 0x0015DF34
		internal static decimal SubtractDecimal(decimal value1, decimal value2)
		{
			return value1 - value2;
		}

		// Token: 0x0600550E RID: 21774 RVA: 0x0015EB48 File Offset: 0x0015DF48
		internal static double SubtractDouble(double value1, double value2)
		{
			return value1 - value2;
		}

		// Token: 0x0600550F RID: 21775 RVA: 0x0015EB58 File Offset: 0x0015DF58
		internal static short SubtractInt16(short value1, short value2)
		{
			return value1 - value2;
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x0015EB6C File Offset: 0x0015DF6C
		internal static int SubtractInt32(int value1, int value2)
		{
			return value1 - value2;
		}

		// Token: 0x06005511 RID: 21777 RVA: 0x0015EB7C File Offset: 0x0015DF7C
		internal static long SubtractInt64(long value1, long value2)
		{
			return value1 - value2;
		}

		// Token: 0x06005512 RID: 21778 RVA: 0x0015EB8C File Offset: 0x0015DF8C
		internal static Point SubtractPoint(Point value1, Point value2)
		{
			return new Point(value1.X - value2.X, value1.Y - value2.Y);
		}

		// Token: 0x06005513 RID: 21779 RVA: 0x0015EBBC File Offset: 0x0015DFBC
		internal static Point3D SubtractPoint3D(Point3D value1, Point3D value2)
		{
			return new Point3D(value1.X - value2.X, value1.Y - value2.Y, value1.Z - value2.Z);
		}

		// Token: 0x06005514 RID: 21780 RVA: 0x0015EBFC File Offset: 0x0015DFFC
		internal static Quaternion SubtractQuaternion(Quaternion value1, Quaternion value2)
		{
			value2.Invert();
			return value1 * value2;
		}

		// Token: 0x06005515 RID: 21781 RVA: 0x0015EC18 File Offset: 0x0015E018
		internal static float SubtractSingle(float value1, float value2)
		{
			return value1 - value2;
		}

		// Token: 0x06005516 RID: 21782 RVA: 0x0015EC28 File Offset: 0x0015E028
		internal static Size SubtractSize(Size value1, Size value2)
		{
			return new Size(value1.Width - value2.Width, value1.Height - value2.Height);
		}

		// Token: 0x06005517 RID: 21783 RVA: 0x0015EC58 File Offset: 0x0015E058
		internal static Vector SubtractVector(Vector value1, Vector value2)
		{
			return value1 - value2;
		}

		// Token: 0x06005518 RID: 21784 RVA: 0x0015EC6C File Offset: 0x0015E06C
		internal static Vector3D SubtractVector3D(Vector3D value1, Vector3D value2)
		{
			return value1 - value2;
		}

		// Token: 0x06005519 RID: 21785 RVA: 0x0015EC80 File Offset: 0x0015E080
		internal static Rect SubtractRect(Rect value1, Rect value2)
		{
			return new Rect(AnimatedTypeHelpers.SubtractPoint(value1.Location, value2.Location), AnimatedTypeHelpers.SubtractSize(value1.Size, value2.Size));
		}

		// Token: 0x0600551A RID: 21786 RVA: 0x0015ECB8 File Offset: 0x0015E0B8
		internal static Rotation3D SubtractRotation3D(Rotation3D value1, Rotation3D value2)
		{
			return new QuaternionRotation3D(AnimatedTypeHelpers.SubtractQuaternion(value1.InternalQuaternion, value2.InternalQuaternion));
		}

		// Token: 0x0600551B RID: 21787 RVA: 0x0015ECDC File Offset: 0x0015E0DC
		internal static double GetSegmentLengthBoolean(bool from, bool to)
		{
			if (from != to)
			{
				return 1.0;
			}
			return 0.0;
		}

		// Token: 0x0600551C RID: 21788 RVA: 0x0015ED00 File Offset: 0x0015E100
		internal static double GetSegmentLengthByte(byte from, byte to)
		{
			return (double)Math.Abs((int)(to - from));
		}

		// Token: 0x0600551D RID: 21789 RVA: 0x0015ED18 File Offset: 0x0015E118
		internal static double GetSegmentLengthChar(char from, char to)
		{
			if (from != to)
			{
				return 1.0;
			}
			return 0.0;
		}

		// Token: 0x0600551E RID: 21790 RVA: 0x0015ED3C File Offset: 0x0015E13C
		internal static double GetSegmentLengthColor(Color from, Color to)
		{
			return (double)(Math.Abs(to.ScA - from.ScA) + Math.Abs(to.ScR - from.ScR) + Math.Abs(to.ScG - from.ScG) + Math.Abs(to.ScB - from.ScB));
		}

		// Token: 0x0600551F RID: 21791 RVA: 0x0015EDA0 File Offset: 0x0015E1A0
		internal static double GetSegmentLengthDecimal(decimal from, decimal to)
		{
			return (double)Math.Abs(to - from);
		}

		// Token: 0x06005520 RID: 21792 RVA: 0x0015EDC0 File Offset: 0x0015E1C0
		internal static double GetSegmentLengthDouble(double from, double to)
		{
			return Math.Abs(to - from);
		}

		// Token: 0x06005521 RID: 21793 RVA: 0x0015EDD8 File Offset: 0x0015E1D8
		internal static double GetSegmentLengthInt16(short from, short to)
		{
			return (double)Math.Abs((int)(to - from));
		}

		// Token: 0x06005522 RID: 21794 RVA: 0x0015EDF0 File Offset: 0x0015E1F0
		internal static double GetSegmentLengthInt32(int from, int to)
		{
			return (double)Math.Abs(to - from);
		}

		// Token: 0x06005523 RID: 21795 RVA: 0x0015EE08 File Offset: 0x0015E208
		internal static double GetSegmentLengthInt64(long from, long to)
		{
			return (double)Math.Abs(to - from);
		}

		// Token: 0x06005524 RID: 21796 RVA: 0x0015EE20 File Offset: 0x0015E220
		internal static double GetSegmentLengthMatrix(Matrix from, Matrix to)
		{
			if (from != to)
			{
				return 1.0;
			}
			return 0.0;
		}

		// Token: 0x06005525 RID: 21797 RVA: 0x0015EE4C File Offset: 0x0015E24C
		internal static double GetSegmentLengthObject(object from, object to)
		{
			return 1.0;
		}

		// Token: 0x06005526 RID: 21798 RVA: 0x0015EE64 File Offset: 0x0015E264
		internal static double GetSegmentLengthPoint(Point from, Point to)
		{
			return Math.Abs((to - from).Length);
		}

		// Token: 0x06005527 RID: 21799 RVA: 0x0015EE88 File Offset: 0x0015E288
		internal static double GetSegmentLengthPoint3D(Point3D from, Point3D to)
		{
			return Math.Abs((to - from).Length);
		}

		// Token: 0x06005528 RID: 21800 RVA: 0x0015EEAC File Offset: 0x0015E2AC
		internal static double GetSegmentLengthQuaternion(Quaternion from, Quaternion to)
		{
			from.Invert();
			return (to * from).Angle;
		}

		// Token: 0x06005529 RID: 21801 RVA: 0x0015EED0 File Offset: 0x0015E2D0
		internal static double GetSegmentLengthRect(Rect from, Rect to)
		{
			double segmentLengthPoint = AnimatedTypeHelpers.GetSegmentLengthPoint(from.Location, to.Location);
			double segmentLengthSize = AnimatedTypeHelpers.GetSegmentLengthSize(from.Size, to.Size);
			return Math.Sqrt(segmentLengthPoint * segmentLengthPoint + segmentLengthSize * segmentLengthSize);
		}

		// Token: 0x0600552A RID: 21802 RVA: 0x0015EF14 File Offset: 0x0015E314
		internal static double GetSegmentLengthRotation3D(Rotation3D from, Rotation3D to)
		{
			return AnimatedTypeHelpers.GetSegmentLengthQuaternion(from.InternalQuaternion, to.InternalQuaternion);
		}

		// Token: 0x0600552B RID: 21803 RVA: 0x0015EF34 File Offset: 0x0015E334
		internal static double GetSegmentLengthSingle(float from, float to)
		{
			return (double)Math.Abs(to - from);
		}

		// Token: 0x0600552C RID: 21804 RVA: 0x0015EF4C File Offset: 0x0015E34C
		internal static double GetSegmentLengthSize(Size from, Size to)
		{
			return Math.Abs(((Vector)to - (Vector)from).Length);
		}

		// Token: 0x0600552D RID: 21805 RVA: 0x0015EF78 File Offset: 0x0015E378
		internal static double GetSegmentLengthString(string from, string to)
		{
			if (from != to)
			{
				return 1.0;
			}
			return 0.0;
		}

		// Token: 0x0600552E RID: 21806 RVA: 0x0015EFA4 File Offset: 0x0015E3A4
		internal static double GetSegmentLengthVector(Vector from, Vector to)
		{
			return Math.Abs((to - from).Length);
		}

		// Token: 0x0600552F RID: 21807 RVA: 0x0015EFC8 File Offset: 0x0015E3C8
		internal static double GetSegmentLengthVector3D(Vector3D from, Vector3D to)
		{
			return Math.Abs((to - from).Length);
		}

		// Token: 0x06005530 RID: 21808 RVA: 0x0015EFEC File Offset: 0x0015E3EC
		internal static byte ScaleByte(byte value, double factor)
		{
			return (byte)((double)value * factor);
		}

		// Token: 0x06005531 RID: 21809 RVA: 0x0015F000 File Offset: 0x0015E400
		internal static Color ScaleColor(Color value, double factor)
		{
			return value * (float)factor;
		}

		// Token: 0x06005532 RID: 21810 RVA: 0x0015F018 File Offset: 0x0015E418
		internal static decimal ScaleDecimal(decimal value, double factor)
		{
			return value * (decimal)factor;
		}

		// Token: 0x06005533 RID: 21811 RVA: 0x0015F034 File Offset: 0x0015E434
		internal static double ScaleDouble(double value, double factor)
		{
			return value * factor;
		}

		// Token: 0x06005534 RID: 21812 RVA: 0x0015F044 File Offset: 0x0015E444
		internal static short ScaleInt16(short value, double factor)
		{
			return (short)((double)value * factor);
		}

		// Token: 0x06005535 RID: 21813 RVA: 0x0015F058 File Offset: 0x0015E458
		internal static int ScaleInt32(int value, double factor)
		{
			return (int)((double)value * factor);
		}

		// Token: 0x06005536 RID: 21814 RVA: 0x0015F06C File Offset: 0x0015E46C
		internal static long ScaleInt64(long value, double factor)
		{
			return (long)((double)value * factor);
		}

		// Token: 0x06005537 RID: 21815 RVA: 0x0015F080 File Offset: 0x0015E480
		internal static Point ScalePoint(Point value, double factor)
		{
			return new Point(value.X * factor, value.Y * factor);
		}

		// Token: 0x06005538 RID: 21816 RVA: 0x0015F0A4 File Offset: 0x0015E4A4
		internal static Point3D ScalePoint3D(Point3D value, double factor)
		{
			return new Point3D(value.X * factor, value.Y * factor, value.Z * factor);
		}

		// Token: 0x06005539 RID: 21817 RVA: 0x0015F0D4 File Offset: 0x0015E4D4
		internal static Quaternion ScaleQuaternion(Quaternion value, double factor)
		{
			return new Quaternion(value.Axis, value.Angle * factor);
		}

		// Token: 0x0600553A RID: 21818 RVA: 0x0015F0F8 File Offset: 0x0015E4F8
		internal static Rect ScaleRect(Rect value, double factor)
		{
			return new Rect
			{
				Location = new Point(value.Location.X * factor, value.Location.Y * factor),
				Size = new Size(value.Size.Width * factor, value.Size.Height * factor)
			};
		}

		// Token: 0x0600553B RID: 21819 RVA: 0x0015F16C File Offset: 0x0015E56C
		internal static Rotation3D ScaleRotation3D(Rotation3D value, double factor)
		{
			return new QuaternionRotation3D(AnimatedTypeHelpers.ScaleQuaternion(value.InternalQuaternion, factor));
		}

		// Token: 0x0600553C RID: 21820 RVA: 0x0015F18C File Offset: 0x0015E58C
		internal static float ScaleSingle(float value, double factor)
		{
			return (float)((double)value * factor);
		}

		// Token: 0x0600553D RID: 21821 RVA: 0x0015F1A0 File Offset: 0x0015E5A0
		internal static Size ScaleSize(Size value, double factor)
		{
			return (Size)((Vector)value * factor);
		}

		// Token: 0x0600553E RID: 21822 RVA: 0x0015F1C0 File Offset: 0x0015E5C0
		internal static Vector ScaleVector(Vector value, double factor)
		{
			return value * factor;
		}

		// Token: 0x0600553F RID: 21823 RVA: 0x0015F1D4 File Offset: 0x0015E5D4
		internal static Vector3D ScaleVector3D(Vector3D value, double factor)
		{
			return value * factor;
		}

		// Token: 0x06005540 RID: 21824 RVA: 0x0015F1E8 File Offset: 0x0015E5E8
		internal static bool IsValidAnimationValueBoolean(bool value)
		{
			return true;
		}

		// Token: 0x06005541 RID: 21825 RVA: 0x0015F1F8 File Offset: 0x0015E5F8
		internal static bool IsValidAnimationValueByte(byte value)
		{
			return true;
		}

		// Token: 0x06005542 RID: 21826 RVA: 0x0015F208 File Offset: 0x0015E608
		internal static bool IsValidAnimationValueChar(char value)
		{
			return true;
		}

		// Token: 0x06005543 RID: 21827 RVA: 0x0015F218 File Offset: 0x0015E618
		internal static bool IsValidAnimationValueColor(Color value)
		{
			return true;
		}

		// Token: 0x06005544 RID: 21828 RVA: 0x0015F228 File Offset: 0x0015E628
		internal static bool IsValidAnimationValueDecimal(decimal value)
		{
			return true;
		}

		// Token: 0x06005545 RID: 21829 RVA: 0x0015F238 File Offset: 0x0015E638
		internal static bool IsValidAnimationValueDouble(double value)
		{
			return !AnimatedTypeHelpers.IsInvalidDouble(value);
		}

		// Token: 0x06005546 RID: 21830 RVA: 0x0015F250 File Offset: 0x0015E650
		internal static bool IsValidAnimationValueInt16(short value)
		{
			return true;
		}

		// Token: 0x06005547 RID: 21831 RVA: 0x0015F260 File Offset: 0x0015E660
		internal static bool IsValidAnimationValueInt32(int value)
		{
			return true;
		}

		// Token: 0x06005548 RID: 21832 RVA: 0x0015F270 File Offset: 0x0015E670
		internal static bool IsValidAnimationValueInt64(long value)
		{
			return true;
		}

		// Token: 0x06005549 RID: 21833 RVA: 0x0015F280 File Offset: 0x0015E680
		internal static bool IsValidAnimationValueMatrix(Matrix value)
		{
			return true;
		}

		// Token: 0x0600554A RID: 21834 RVA: 0x0015F290 File Offset: 0x0015E690
		internal static bool IsValidAnimationValuePoint(Point value)
		{
			return !AnimatedTypeHelpers.IsInvalidDouble(value.X) && !AnimatedTypeHelpers.IsInvalidDouble(value.Y);
		}

		// Token: 0x0600554B RID: 21835 RVA: 0x0015F2BC File Offset: 0x0015E6BC
		internal static bool IsValidAnimationValuePoint3D(Point3D value)
		{
			return !AnimatedTypeHelpers.IsInvalidDouble(value.X) && !AnimatedTypeHelpers.IsInvalidDouble(value.Y) && !AnimatedTypeHelpers.IsInvalidDouble(value.Z);
		}

		// Token: 0x0600554C RID: 21836 RVA: 0x0015F2F8 File Offset: 0x0015E6F8
		internal static bool IsValidAnimationValueQuaternion(Quaternion value)
		{
			return !AnimatedTypeHelpers.IsInvalidDouble(value.X) && !AnimatedTypeHelpers.IsInvalidDouble(value.Y) && !AnimatedTypeHelpers.IsInvalidDouble(value.Z) && !AnimatedTypeHelpers.IsInvalidDouble(value.W);
		}

		// Token: 0x0600554D RID: 21837 RVA: 0x0015F340 File Offset: 0x0015E740
		internal static bool IsValidAnimationValueRect(Rect value)
		{
			return !AnimatedTypeHelpers.IsInvalidDouble(value.Location.X) && !AnimatedTypeHelpers.IsInvalidDouble(value.Location.Y) && !AnimatedTypeHelpers.IsInvalidDouble(value.Size.Width) && !AnimatedTypeHelpers.IsInvalidDouble(value.Size.Height) && !value.IsEmpty;
		}

		// Token: 0x0600554E RID: 21838 RVA: 0x0015F3B4 File Offset: 0x0015E7B4
		internal static bool IsValidAnimationValueRotation3D(Rotation3D value)
		{
			return AnimatedTypeHelpers.IsValidAnimationValueQuaternion(value.InternalQuaternion);
		}

		// Token: 0x0600554F RID: 21839 RVA: 0x0015F3CC File Offset: 0x0015E7CC
		internal static bool IsValidAnimationValueSingle(float value)
		{
			return !AnimatedTypeHelpers.IsInvalidDouble((double)value);
		}

		// Token: 0x06005550 RID: 21840 RVA: 0x0015F3E8 File Offset: 0x0015E7E8
		internal static bool IsValidAnimationValueSize(Size value)
		{
			return !AnimatedTypeHelpers.IsInvalidDouble(value.Width) && !AnimatedTypeHelpers.IsInvalidDouble(value.Height);
		}

		// Token: 0x06005551 RID: 21841 RVA: 0x0015F414 File Offset: 0x0015E814
		internal static bool IsValidAnimationValueString(string value)
		{
			return true;
		}

		// Token: 0x06005552 RID: 21842 RVA: 0x0015F424 File Offset: 0x0015E824
		internal static bool IsValidAnimationValueVector(Vector value)
		{
			return !AnimatedTypeHelpers.IsInvalidDouble(value.X) && !AnimatedTypeHelpers.IsInvalidDouble(value.Y);
		}

		// Token: 0x06005553 RID: 21843 RVA: 0x0015F450 File Offset: 0x0015E850
		internal static bool IsValidAnimationValueVector3D(Vector3D value)
		{
			return !AnimatedTypeHelpers.IsInvalidDouble(value.X) && !AnimatedTypeHelpers.IsInvalidDouble(value.Y) && !AnimatedTypeHelpers.IsInvalidDouble(value.Z);
		}

		// Token: 0x06005554 RID: 21844 RVA: 0x0015F48C File Offset: 0x0015E88C
		internal static byte GetZeroValueByte(byte baseValue)
		{
			return 0;
		}

		// Token: 0x06005555 RID: 21845 RVA: 0x0015F49C File Offset: 0x0015E89C
		internal static Color GetZeroValueColor(Color baseValue)
		{
			return Color.FromScRgb(0f, 0f, 0f, 0f);
		}

		// Token: 0x06005556 RID: 21846 RVA: 0x0015F4C4 File Offset: 0x0015E8C4
		internal static decimal GetZeroValueDecimal(decimal baseValue)
		{
			return 0m;
		}

		// Token: 0x06005557 RID: 21847 RVA: 0x0015F4D8 File Offset: 0x0015E8D8
		internal static double GetZeroValueDouble(double baseValue)
		{
			return 0.0;
		}

		// Token: 0x06005558 RID: 21848 RVA: 0x0015F4F0 File Offset: 0x0015E8F0
		internal static short GetZeroValueInt16(short baseValue)
		{
			return 0;
		}

		// Token: 0x06005559 RID: 21849 RVA: 0x0015F500 File Offset: 0x0015E900
		internal static int GetZeroValueInt32(int baseValue)
		{
			return 0;
		}

		// Token: 0x0600555A RID: 21850 RVA: 0x0015F510 File Offset: 0x0015E910
		internal static long GetZeroValueInt64(long baseValue)
		{
			return 0L;
		}

		// Token: 0x0600555B RID: 21851 RVA: 0x0015F520 File Offset: 0x0015E920
		internal static Point GetZeroValuePoint(Point baseValue)
		{
			return default(Point);
		}

		// Token: 0x0600555C RID: 21852 RVA: 0x0015F538 File Offset: 0x0015E938
		internal static Point3D GetZeroValuePoint3D(Point3D baseValue)
		{
			return default(Point3D);
		}

		// Token: 0x0600555D RID: 21853 RVA: 0x0015F550 File Offset: 0x0015E950
		internal static Quaternion GetZeroValueQuaternion(Quaternion baseValue)
		{
			return Quaternion.Identity;
		}

		// Token: 0x0600555E RID: 21854 RVA: 0x0015F564 File Offset: 0x0015E964
		internal static float GetZeroValueSingle(float baseValue)
		{
			return 0f;
		}

		// Token: 0x0600555F RID: 21855 RVA: 0x0015F578 File Offset: 0x0015E978
		internal static Size GetZeroValueSize(Size baseValue)
		{
			return default(Size);
		}

		// Token: 0x06005560 RID: 21856 RVA: 0x0015F590 File Offset: 0x0015E990
		internal static Vector GetZeroValueVector(Vector baseValue)
		{
			return default(Vector);
		}

		// Token: 0x06005561 RID: 21857 RVA: 0x0015F5A8 File Offset: 0x0015E9A8
		internal static Vector3D GetZeroValueVector3D(Vector3D baseValue)
		{
			return default(Vector3D);
		}

		// Token: 0x06005562 RID: 21858 RVA: 0x0015F5C0 File Offset: 0x0015E9C0
		internal static Rect GetZeroValueRect(Rect baseValue)
		{
			return new Rect(default(Point), default(Vector));
		}

		// Token: 0x06005563 RID: 21859 RVA: 0x0015F5E4 File Offset: 0x0015E9E4
		internal static Rotation3D GetZeroValueRotation3D(Rotation3D baseValue)
		{
			return Rotation3D.Identity;
		}

		// Token: 0x06005564 RID: 21860 RVA: 0x0015F5F8 File Offset: 0x0015E9F8
		private static bool IsInvalidDouble(double value)
		{
			return double.IsInfinity(value) || DoubleUtil.IsNaN(value);
		}
	}
}
