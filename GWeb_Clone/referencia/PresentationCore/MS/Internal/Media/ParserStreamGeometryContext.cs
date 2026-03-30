using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using MS.Internal.PresentationCore;

namespace MS.Internal.Media
{
	// Token: 0x020006F9 RID: 1785
	internal class ParserStreamGeometryContext : StreamGeometryContext
	{
		// Token: 0x06004CDE RID: 19678 RVA: 0x0012F334 File Offset: 0x0012E734
		internal ParserStreamGeometryContext(BinaryWriter bw)
		{
			this._bw = bw;
		}

		// Token: 0x06004CDF RID: 19679 RVA: 0x0012F358 File Offset: 0x0012E758
		internal void SetFillRule(FillRule fillRule)
		{
			bool @bool = ParserStreamGeometryContext.FillRuleToBool(fillRule);
			byte value = ParserStreamGeometryContext.PackByte(ParserStreamGeometryContext.ParserGeometryContextOpCodes.FillRule, @bool, false);
			this._bw.Write(value);
		}

		// Token: 0x06004CE0 RID: 19680 RVA: 0x0012F384 File Offset: 0x0012E784
		public override void BeginFigure(Point startPoint, bool isFilled, bool isClosed)
		{
			this.FinishFigure();
			this._startPoint = startPoint;
			this._isFilled = isFilled;
			this._isClosed = isClosed;
			this._figureStreamPosition = this.CurrentStreamPosition;
			this.SerializePointAndTwoBools(ParserStreamGeometryContext.ParserGeometryContextOpCodes.BeginFigure, startPoint, isFilled, isClosed);
		}

		// Token: 0x06004CE1 RID: 19681 RVA: 0x0012F3C4 File Offset: 0x0012E7C4
		public override void LineTo(Point point, bool isStroked, bool isSmoothJoin)
		{
			this.SerializePointAndTwoBools(ParserStreamGeometryContext.ParserGeometryContextOpCodes.LineTo, point, isStroked, isSmoothJoin);
		}

		// Token: 0x06004CE2 RID: 19682 RVA: 0x0012F3DC File Offset: 0x0012E7DC
		public override void QuadraticBezierTo(Point point1, Point point2, bool isStroked, bool isSmoothJoin)
		{
			this.SerializePointAndTwoBools(ParserStreamGeometryContext.ParserGeometryContextOpCodes.QuadraticBezierTo, point1, isStroked, isSmoothJoin);
			XamlSerializationHelper.WriteDouble(this._bw, point2.X);
			XamlSerializationHelper.WriteDouble(this._bw, point2.Y);
		}

		// Token: 0x06004CE3 RID: 19683 RVA: 0x0012F418 File Offset: 0x0012E818
		public override void BezierTo(Point point1, Point point2, Point point3, bool isStroked, bool isSmoothJoin)
		{
			this.SerializePointAndTwoBools(ParserStreamGeometryContext.ParserGeometryContextOpCodes.BezierTo, point1, isStroked, isSmoothJoin);
			XamlSerializationHelper.WriteDouble(this._bw, point2.X);
			XamlSerializationHelper.WriteDouble(this._bw, point2.Y);
			XamlSerializationHelper.WriteDouble(this._bw, point3.X);
			XamlSerializationHelper.WriteDouble(this._bw, point3.Y);
		}

		// Token: 0x06004CE4 RID: 19684 RVA: 0x0012F47C File Offset: 0x0012E87C
		public override void PolyLineTo(IList<Point> points, bool isStroked, bool isSmoothJoin)
		{
			this.SerializeListOfPointsAndTwoBools(ParserStreamGeometryContext.ParserGeometryContextOpCodes.PolyLineTo, points, isStroked, isSmoothJoin);
		}

		// Token: 0x06004CE5 RID: 19685 RVA: 0x0012F494 File Offset: 0x0012E894
		public override void PolyQuadraticBezierTo(IList<Point> points, bool isStroked, bool isSmoothJoin)
		{
			this.SerializeListOfPointsAndTwoBools(ParserStreamGeometryContext.ParserGeometryContextOpCodes.PolyQuadraticBezierTo, points, isStroked, isSmoothJoin);
		}

		// Token: 0x06004CE6 RID: 19686 RVA: 0x0012F4AC File Offset: 0x0012E8AC
		public override void PolyBezierTo(IList<Point> points, bool isStroked, bool isSmoothJoin)
		{
			this.SerializeListOfPointsAndTwoBools(ParserStreamGeometryContext.ParserGeometryContextOpCodes.PolyBezierTo, points, isStroked, isSmoothJoin);
		}

		// Token: 0x06004CE7 RID: 19687 RVA: 0x0012F4C4 File Offset: 0x0012E8C4
		public override void ArcTo(Point point, Size size, double rotationAngle, bool isLargeArc, SweepDirection sweepDirection, bool isStroked, bool isSmoothJoin)
		{
			this.SerializePointAndTwoBools(ParserStreamGeometryContext.ParserGeometryContextOpCodes.ArcTo, point, isStroked, isSmoothJoin);
			byte b = 0;
			if (isLargeArc)
			{
				b = 15;
			}
			if (ParserStreamGeometryContext.SweepToBool(sweepDirection))
			{
				b |= 240;
			}
			this._bw.Write(b);
			XamlSerializationHelper.WriteDouble(this._bw, size.Width);
			XamlSerializationHelper.WriteDouble(this._bw, size.Height);
			XamlSerializationHelper.WriteDouble(this._bw, rotationAngle);
		}

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06004CE8 RID: 19688 RVA: 0x0012F534 File Offset: 0x0012E934
		internal bool FigurePending
		{
			get
			{
				return this._figureStreamPosition > -1;
			}
		}

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06004CE9 RID: 19689 RVA: 0x0012F54C File Offset: 0x0012E94C
		internal int CurrentStreamPosition
		{
			get
			{
				return checked((int)this._bw.Seek(0, SeekOrigin.Current));
			}
		}

		// Token: 0x06004CEA RID: 19690 RVA: 0x0012F568 File Offset: 0x0012E968
		internal void FinishFigure()
		{
			if (this.FigurePending)
			{
				int currentStreamPosition = this.CurrentStreamPosition;
				this._bw.Seek(this._figureStreamPosition, SeekOrigin.Begin);
				this.SerializePointAndTwoBools(ParserStreamGeometryContext.ParserGeometryContextOpCodes.BeginFigure, this._startPoint, this._isFilled, this._isClosed);
				this._bw.Seek(currentStreamPosition, SeekOrigin.Begin);
			}
		}

		// Token: 0x06004CEB RID: 19691 RVA: 0x0012F5C0 File Offset: 0x0012E9C0
		internal override void DisposeCore()
		{
		}

		// Token: 0x06004CEC RID: 19692 RVA: 0x0012F5D0 File Offset: 0x0012E9D0
		internal override void SetClosedState(bool closed)
		{
			this._isClosed = closed;
		}

		// Token: 0x06004CED RID: 19693 RVA: 0x0012F5E4 File Offset: 0x0012E9E4
		internal void MarkEOF()
		{
			this.FinishFigure();
			this._bw.Write(8);
		}

		// Token: 0x06004CEE RID: 19694 RVA: 0x0012F604 File Offset: 0x0012EA04
		internal static void Deserialize(BinaryReader br, StreamGeometryContext sc, StreamGeometry geometry)
		{
			bool flag = false;
			while (!flag)
			{
				byte b = br.ReadByte();
				switch (ParserStreamGeometryContext.UnPackOpCode(b))
				{
				case ParserStreamGeometryContext.ParserGeometryContextOpCodes.BeginFigure:
					ParserStreamGeometryContext.DeserializeBeginFigure(br, b, sc);
					break;
				case ParserStreamGeometryContext.ParserGeometryContextOpCodes.LineTo:
					ParserStreamGeometryContext.DeserializeLineTo(br, b, sc);
					break;
				case ParserStreamGeometryContext.ParserGeometryContextOpCodes.QuadraticBezierTo:
					ParserStreamGeometryContext.DeserializeQuadraticBezierTo(br, b, sc);
					break;
				case ParserStreamGeometryContext.ParserGeometryContextOpCodes.BezierTo:
					ParserStreamGeometryContext.DeserializeBezierTo(br, b, sc);
					break;
				case ParserStreamGeometryContext.ParserGeometryContextOpCodes.PolyLineTo:
					ParserStreamGeometryContext.DeserializePolyLineTo(br, b, sc);
					break;
				case ParserStreamGeometryContext.ParserGeometryContextOpCodes.PolyQuadraticBezierTo:
					ParserStreamGeometryContext.DeserializePolyQuadraticBezierTo(br, b, sc);
					break;
				case ParserStreamGeometryContext.ParserGeometryContextOpCodes.PolyBezierTo:
					ParserStreamGeometryContext.DeserializePolyBezierTo(br, b, sc);
					break;
				case ParserStreamGeometryContext.ParserGeometryContextOpCodes.ArcTo:
					ParserStreamGeometryContext.DeserializeArcTo(br, b, sc);
					break;
				case ParserStreamGeometryContext.ParserGeometryContextOpCodes.Closed:
					flag = true;
					break;
				case ParserStreamGeometryContext.ParserGeometryContextOpCodes.FillRule:
					ParserStreamGeometryContext.DeserializeFillRule(br, b, geometry);
					break;
				}
			}
		}

		// Token: 0x06004CEF RID: 19695 RVA: 0x0012F6B8 File Offset: 0x0012EAB8
		private static void DeserializeFillRule(BinaryReader br, byte firstByte, StreamGeometry geometry)
		{
			bool value;
			bool flag;
			ParserStreamGeometryContext.UnPackBools(firstByte, out value, out flag);
			FillRule fillRule = ParserStreamGeometryContext.BoolToFillRule(value);
			geometry.FillRule = fillRule;
		}

		// Token: 0x06004CF0 RID: 19696 RVA: 0x0012F6E0 File Offset: 0x0012EAE0
		private static void DeserializeBeginFigure(BinaryReader br, byte firstByte, StreamGeometryContext sc)
		{
			Point startPoint;
			bool isFilled;
			bool isClosed;
			ParserStreamGeometryContext.DeserializePointAndTwoBools(br, firstByte, out startPoint, out isFilled, out isClosed);
			sc.BeginFigure(startPoint, isFilled, isClosed);
		}

		// Token: 0x06004CF1 RID: 19697 RVA: 0x0012F704 File Offset: 0x0012EB04
		private static void DeserializeLineTo(BinaryReader br, byte firstByte, StreamGeometryContext sc)
		{
			Point point;
			bool isStroked;
			bool isSmoothJoin;
			ParserStreamGeometryContext.DeserializePointAndTwoBools(br, firstByte, out point, out isStroked, out isSmoothJoin);
			sc.LineTo(point, isStroked, isSmoothJoin);
		}

		// Token: 0x06004CF2 RID: 19698 RVA: 0x0012F728 File Offset: 0x0012EB28
		private static void DeserializeQuadraticBezierTo(BinaryReader br, byte firstByte, StreamGeometryContext sc)
		{
			Point point = default(Point);
			Point point2;
			bool isStroked;
			bool isSmoothJoin;
			ParserStreamGeometryContext.DeserializePointAndTwoBools(br, firstByte, out point2, out isStroked, out isSmoothJoin);
			point.X = XamlSerializationHelper.ReadDouble(br);
			point.Y = XamlSerializationHelper.ReadDouble(br);
			sc.QuadraticBezierTo(point2, point, isStroked, isSmoothJoin);
		}

		// Token: 0x06004CF3 RID: 19699 RVA: 0x0012F770 File Offset: 0x0012EB70
		private static void DeserializeBezierTo(BinaryReader br, byte firstByte, StreamGeometryContext sc)
		{
			Point point = default(Point);
			Point point2 = default(Point);
			Point point3;
			bool isStroked;
			bool isSmoothJoin;
			ParserStreamGeometryContext.DeserializePointAndTwoBools(br, firstByte, out point3, out isStroked, out isSmoothJoin);
			point.X = XamlSerializationHelper.ReadDouble(br);
			point.Y = XamlSerializationHelper.ReadDouble(br);
			point2.X = XamlSerializationHelper.ReadDouble(br);
			point2.Y = XamlSerializationHelper.ReadDouble(br);
			sc.BezierTo(point3, point, point2, isStroked, isSmoothJoin);
		}

		// Token: 0x06004CF4 RID: 19700 RVA: 0x0012F7DC File Offset: 0x0012EBDC
		private static void DeserializePolyLineTo(BinaryReader br, byte firstByte, StreamGeometryContext sc)
		{
			bool isStroked;
			bool isSmoothJoin;
			IList<Point> points = ParserStreamGeometryContext.DeserializeListOfPointsAndTwoBools(br, firstByte, out isStroked, out isSmoothJoin);
			sc.PolyLineTo(points, isStroked, isSmoothJoin);
		}

		// Token: 0x06004CF5 RID: 19701 RVA: 0x0012F800 File Offset: 0x0012EC00
		private static void DeserializePolyQuadraticBezierTo(BinaryReader br, byte firstByte, StreamGeometryContext sc)
		{
			bool isStroked;
			bool isSmoothJoin;
			IList<Point> points = ParserStreamGeometryContext.DeserializeListOfPointsAndTwoBools(br, firstByte, out isStroked, out isSmoothJoin);
			sc.PolyQuadraticBezierTo(points, isStroked, isSmoothJoin);
		}

		// Token: 0x06004CF6 RID: 19702 RVA: 0x0012F824 File Offset: 0x0012EC24
		private static void DeserializePolyBezierTo(BinaryReader br, byte firstByte, StreamGeometryContext sc)
		{
			bool isStroked;
			bool isSmoothJoin;
			IList<Point> points = ParserStreamGeometryContext.DeserializeListOfPointsAndTwoBools(br, firstByte, out isStroked, out isSmoothJoin);
			sc.PolyBezierTo(points, isStroked, isSmoothJoin);
		}

		// Token: 0x06004CF7 RID: 19703 RVA: 0x0012F848 File Offset: 0x0012EC48
		private static void DeserializeArcTo(BinaryReader br, byte firstByte, StreamGeometryContext sc)
		{
			Size size = default(Size);
			Point point;
			bool isStroked;
			bool isSmoothJoin;
			ParserStreamGeometryContext.DeserializePointAndTwoBools(br, firstByte, out point, out isStroked, out isSmoothJoin);
			byte b = br.ReadByte();
			bool isLargeArc = (b & 15) > 0;
			SweepDirection sweepDirection = ParserStreamGeometryContext.BoolToSweep((b & 240) > 0);
			size.Width = XamlSerializationHelper.ReadDouble(br);
			size.Height = XamlSerializationHelper.ReadDouble(br);
			double rotationAngle = XamlSerializationHelper.ReadDouble(br);
			sc.ArcTo(point, size, rotationAngle, isLargeArc, sweepDirection, isStroked, isSmoothJoin);
		}

		// Token: 0x06004CF8 RID: 19704 RVA: 0x0012F8BC File Offset: 0x0012ECBC
		private static void UnPackBools(byte packedByte, out bool bool1, out bool bool2)
		{
			bool1 = ((packedByte & 16) > 0);
			bool2 = ((packedByte & 32) > 0);
		}

		// Token: 0x06004CF9 RID: 19705 RVA: 0x0012F8DC File Offset: 0x0012ECDC
		private static void UnPackBools(byte packedByte, out bool bool1, out bool bool2, out bool bool3, out bool bool4)
		{
			bool1 = ((packedByte & 16) > 0);
			bool2 = ((packedByte & 32) > 0);
			bool3 = ((packedByte & 64) > 0);
			bool4 = ((packedByte & 128) > 0);
		}

		// Token: 0x06004CFA RID: 19706 RVA: 0x0012F914 File Offset: 0x0012ED14
		private static ParserStreamGeometryContext.ParserGeometryContextOpCodes UnPackOpCode(byte packedByte)
		{
			return (ParserStreamGeometryContext.ParserGeometryContextOpCodes)(packedByte & 15);
		}

		// Token: 0x06004CFB RID: 19707 RVA: 0x0012F928 File Offset: 0x0012ED28
		private static IList<Point> DeserializeListOfPointsAndTwoBools(BinaryReader br, byte firstByte, out bool bool1, out bool bool2)
		{
			ParserStreamGeometryContext.UnPackBools(firstByte, out bool1, out bool2);
			int num = br.ReadInt32();
			IList<Point> list = new List<Point>(num);
			for (int i = 0; i < num; i++)
			{
				Point item = new Point(XamlSerializationHelper.ReadDouble(br), XamlSerializationHelper.ReadDouble(br));
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06004CFC RID: 19708 RVA: 0x0012F974 File Offset: 0x0012ED74
		private static void DeserializePointAndTwoBools(BinaryReader br, byte firstByte, out Point point, out bool bool1, out bool bool2)
		{
			bool isScaledInt = false;
			bool isScaledInt2 = false;
			ParserStreamGeometryContext.UnPackBools(firstByte, out bool1, out bool2, out isScaledInt, out isScaledInt2);
			point = new Point(ParserStreamGeometryContext.DeserializeDouble(br, isScaledInt), ParserStreamGeometryContext.DeserializeDouble(br, isScaledInt2));
		}

		// Token: 0x06004CFD RID: 19709 RVA: 0x0012F9AC File Offset: 0x0012EDAC
		private static double DeserializeDouble(BinaryReader br, bool isScaledInt)
		{
			if (isScaledInt)
			{
				return XamlSerializationHelper.ReadScaledInteger(br);
			}
			return XamlSerializationHelper.ReadDouble(br);
		}

		// Token: 0x06004CFE RID: 19710 RVA: 0x0012F9CC File Offset: 0x0012EDCC
		private static SweepDirection BoolToSweep(bool value)
		{
			if (!value)
			{
				return SweepDirection.Counterclockwise;
			}
			return SweepDirection.Clockwise;
		}

		// Token: 0x06004CFF RID: 19711 RVA: 0x0012F9E0 File Offset: 0x0012EDE0
		private static bool SweepToBool(SweepDirection sweep)
		{
			return sweep != SweepDirection.Counterclockwise;
		}

		// Token: 0x06004D00 RID: 19712 RVA: 0x0012F9F4 File Offset: 0x0012EDF4
		private static FillRule BoolToFillRule(bool value)
		{
			if (!value)
			{
				return FillRule.EvenOdd;
			}
			return FillRule.Nonzero;
		}

		// Token: 0x06004D01 RID: 19713 RVA: 0x0012FA08 File Offset: 0x0012EE08
		private static bool FillRuleToBool(FillRule fill)
		{
			return fill != FillRule.EvenOdd;
		}

		// Token: 0x06004D02 RID: 19714 RVA: 0x0012FA1C File Offset: 0x0012EE1C
		private void SerializePointAndTwoBools(ParserStreamGeometryContext.ParserGeometryContextOpCodes opCode, Point point, bool bool1, bool bool2)
		{
			int scaledIntValue = 0;
			int scaledIntValue2 = 0;
			bool flag = XamlSerializationHelper.CanConvertToInteger(point.X, ref scaledIntValue);
			bool flag2 = XamlSerializationHelper.CanConvertToInteger(point.Y, ref scaledIntValue2);
			this._bw.Write(ParserStreamGeometryContext.PackByte(opCode, bool1, bool2, flag, flag2));
			this.SerializeDouble(point.X, flag, scaledIntValue);
			this.SerializeDouble(point.Y, flag2, scaledIntValue2);
		}

		// Token: 0x06004D03 RID: 19715 RVA: 0x0012FA80 File Offset: 0x0012EE80
		private void SerializeListOfPointsAndTwoBools(ParserStreamGeometryContext.ParserGeometryContextOpCodes opCode, IList<Point> points, bool bool1, bool bool2)
		{
			byte value = ParserStreamGeometryContext.PackByte(opCode, bool1, bool2);
			this._bw.Write(value);
			this._bw.Write(points.Count);
			for (int i = 0; i < points.Count; i++)
			{
				XamlSerializationHelper.WriteDouble(this._bw, points[i].X);
				XamlSerializationHelper.WriteDouble(this._bw, points[i].Y);
			}
		}

		// Token: 0x06004D04 RID: 19716 RVA: 0x0012FAFC File Offset: 0x0012EEFC
		private void SerializeDouble(double value, bool isScaledInt, int scaledIntValue)
		{
			if (isScaledInt)
			{
				this._bw.Write(scaledIntValue);
				return;
			}
			XamlSerializationHelper.WriteDouble(this._bw, value);
		}

		// Token: 0x06004D05 RID: 19717 RVA: 0x0012FB28 File Offset: 0x0012EF28
		private static byte PackByte(ParserStreamGeometryContext.ParserGeometryContextOpCodes opCode, bool bool1, bool bool2)
		{
			return ParserStreamGeometryContext.PackByte(opCode, bool1, bool2, false, false);
		}

		// Token: 0x06004D06 RID: 19718 RVA: 0x0012FB40 File Offset: 0x0012EF40
		private static byte PackByte(ParserStreamGeometryContext.ParserGeometryContextOpCodes opCode, bool bool1, bool bool2, bool bool3, bool bool4)
		{
			byte b = (byte)opCode;
			if (b >= 16)
			{
				throw new ArgumentException(SR.Get("UnknownPathOperationType"));
			}
			if (bool1)
			{
				b |= 16;
			}
			if (bool2)
			{
				b |= 32;
			}
			if (bool3)
			{
				b |= 64;
			}
			if (bool4)
			{
				b |= 128;
			}
			return b;
		}

		// Token: 0x0400215F RID: 8543
		private const byte HighNibble = 240;

		// Token: 0x04002160 RID: 8544
		private const byte LowNibble = 15;

		// Token: 0x04002161 RID: 8545
		private const byte SetBool1 = 16;

		// Token: 0x04002162 RID: 8546
		private const byte SetBool2 = 32;

		// Token: 0x04002163 RID: 8547
		private const byte SetBool3 = 64;

		// Token: 0x04002164 RID: 8548
		private const byte SetBool4 = 128;

		// Token: 0x04002165 RID: 8549
		private BinaryWriter _bw;

		// Token: 0x04002166 RID: 8550
		private Point _startPoint;

		// Token: 0x04002167 RID: 8551
		private bool _isClosed;

		// Token: 0x04002168 RID: 8552
		private bool _isFilled;

		// Token: 0x04002169 RID: 8553
		private int _figureStreamPosition = -1;

		// Token: 0x020009CF RID: 2511
		private enum ParserGeometryContextOpCodes : byte
		{
			// Token: 0x04002E0B RID: 11787
			BeginFigure,
			// Token: 0x04002E0C RID: 11788
			LineTo,
			// Token: 0x04002E0D RID: 11789
			QuadraticBezierTo,
			// Token: 0x04002E0E RID: 11790
			BezierTo,
			// Token: 0x04002E0F RID: 11791
			PolyLineTo,
			// Token: 0x04002E10 RID: 11792
			PolyQuadraticBezierTo,
			// Token: 0x04002E11 RID: 11793
			PolyBezierTo,
			// Token: 0x04002E12 RID: 11794
			ArcTo,
			// Token: 0x04002E13 RID: 11795
			Closed,
			// Token: 0x04002E14 RID: 11796
			FillRule
		}
	}
}
