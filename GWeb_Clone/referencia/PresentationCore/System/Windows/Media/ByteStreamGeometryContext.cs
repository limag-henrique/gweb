using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Media.Composition;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media
{
	// Token: 0x0200036A RID: 874
	internal class ByteStreamGeometryContext : CapacityStreamGeometryContext
	{
		// Token: 0x06001EAA RID: 7850 RVA: 0x0007BC9C File Offset: 0x0007B09C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe ByteStreamGeometryContext()
		{
			MIL_PATHGEOMETRY mil_PATHGEOMETRY = default(MIL_PATHGEOMETRY);
			this.AppendData((byte*)(&mil_PATHGEOMETRY), sizeof(MIL_PATHGEOMETRY));
			this._currentPathGeometryData.Size = (uint)sizeof(MIL_PATHGEOMETRY);
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x0007BCE8 File Offset: 0x0007B0E8
		public override void Close()
		{
			this.VerifyApi();
			((IDisposable)this).Dispose();
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x0007BD04 File Offset: 0x0007B104
		[SecurityCritical]
		public unsafe override void BeginFigure(Point startPoint, bool isFilled, bool isClosed)
		{
			this.VerifyApi();
			this.FinishFigure();
			int currOffset = this._currOffset;
			MIL_PATHFIGURE mil_PATHFIGURE;
			this.AppendData((byte*)(&mil_PATHFIGURE), sizeof(MIL_PATHFIGURE));
			this._currentPathFigureDataOffset = currOffset;
			this._currentPathFigureData.StartPoint = startPoint;
			this._currentPathFigureData.Flags = (this._currentPathFigureData.Flags | (isFilled ? MilPathFigureFlags.IsFillable : ((MilPathFigureFlags)0)));
			this._currentPathFigureData.Flags = (this._currentPathFigureData.Flags | (isClosed ? MilPathFigureFlags.IsClosed : ((MilPathFigureFlags)0)));
			this._currentPathFigureData.BackSize = this._lastFigureSize;
			this._currentPathFigureData.Size = (uint)(this._currOffset - this._currentPathFigureDataOffset);
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x0007BD9C File Offset: 0x0007B19C
		[SecuritySafeCritical]
		public unsafe override void LineTo(Point point, bool isStroked, bool isSmoothJoin)
		{
			this.VerifyApi();
			Point* ptr = stackalloc Point[checked(unchecked((UIntPtr)1) * (UIntPtr)sizeof(Point))];
			*ptr = point;
			this.GenericPolyTo(ptr, 1, isStroked, isSmoothJoin, false, MIL_SEGMENT_TYPE.MilSegmentPolyLine);
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x0007BDD0 File Offset: 0x0007B1D0
		[SecuritySafeCritical]
		public unsafe override void QuadraticBezierTo(Point point1, Point point2, bool isStroked, bool isSmoothJoin)
		{
			this.VerifyApi();
			Point* ptr = stackalloc Point[checked(unchecked((UIntPtr)2) * (UIntPtr)sizeof(Point))];
			*ptr = point1;
			ptr[1] = point2;
			this.GenericPolyTo(ptr, 2, isStroked, isSmoothJoin, true, MIL_SEGMENT_TYPE.MilSegmentPolyQuadraticBezier);
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x0007BE14 File Offset: 0x0007B214
		[SecuritySafeCritical]
		public unsafe override void BezierTo(Point point1, Point point2, Point point3, bool isStroked, bool isSmoothJoin)
		{
			this.VerifyApi();
			Point* ptr = stackalloc Point[checked(unchecked((UIntPtr)3) * (UIntPtr)sizeof(Point))];
			*ptr = point1;
			ptr[1] = point2;
			ptr[2] = point3;
			this.GenericPolyTo(ptr, 3, isStroked, isSmoothJoin, true, MIL_SEGMENT_TYPE.MilSegmentPolyBezier);
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x0007BE68 File Offset: 0x0007B268
		public override void PolyLineTo(IList<Point> points, bool isStroked, bool isSmoothJoin)
		{
			this.VerifyApi();
			this.GenericPolyTo(points, isStroked, isSmoothJoin, false, 1, MIL_SEGMENT_TYPE.MilSegmentPolyLine);
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x0007BE88 File Offset: 0x0007B288
		public override void PolyQuadraticBezierTo(IList<Point> points, bool isStroked, bool isSmoothJoin)
		{
			this.VerifyApi();
			this.GenericPolyTo(points, isStroked, isSmoothJoin, true, 2, MIL_SEGMENT_TYPE.MilSegmentPolyQuadraticBezier);
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x0007BEA8 File Offset: 0x0007B2A8
		public override void PolyBezierTo(IList<Point> points, bool isStroked, bool isSmoothJoin)
		{
			this.VerifyApi();
			this.GenericPolyTo(points, isStroked, isSmoothJoin, true, 3, MIL_SEGMENT_TYPE.MilSegmentPolyBezier);
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x0007BEC8 File Offset: 0x0007B2C8
		[SecurityCritical]
		public unsafe override void ArcTo(Point point, Size size, double rotationAngle, bool isLargeArc, SweepDirection sweepDirection, bool isStroked, bool isSmoothJoin)
		{
			this.VerifyApi();
			if (this._currentPathFigureDataOffset == -1)
			{
				throw new InvalidOperationException(SR.Get("StreamGeometry_NeedBeginFigure"));
			}
			this.FinishSegment();
			MIL_SEGMENT_ARC mil_SEGMENT_ARC = default(MIL_SEGMENT_ARC);
			mil_SEGMENT_ARC.Type = MIL_SEGMENT_TYPE.MilSegmentArc;
			mil_SEGMENT_ARC.Flags |= (isStroked ? ((MILCoreSegFlags)0) : MILCoreSegFlags.SegIsAGap);
			mil_SEGMENT_ARC.Flags |= (isSmoothJoin ? MILCoreSegFlags.SegSmoothJoin : ((MILCoreSegFlags)0));
			mil_SEGMENT_ARC.Flags |= MILCoreSegFlags.SegIsCurved;
			mil_SEGMENT_ARC.BackSize = this._lastSegmentSize;
			mil_SEGMENT_ARC.Point = point;
			mil_SEGMENT_ARC.Size = size;
			mil_SEGMENT_ARC.XRotation = rotationAngle;
			mil_SEGMENT_ARC.LargeArc = (isLargeArc ? 1U : 0U);
			mil_SEGMENT_ARC.Sweep = ((sweepDirection == SweepDirection.Clockwise) ? 1U : 0U);
			int currOffset = this._currOffset;
			this.AppendData((byte*)(&mil_SEGMENT_ARC), sizeof(MIL_SEGMENT_ARC));
			this._lastSegmentSize = (uint)sizeof(MIL_SEGMENT_ARC);
			this._currentPathFigureData.Flags = (this._currentPathFigureData.Flags | (isStroked ? ((MilPathFigureFlags)0) : MilPathFigureFlags.HasGaps));
			this._currentPathFigureData.Flags = (this._currentPathFigureData.Flags | MilPathFigureFlags.HasCurves);
			this._currentPathFigureData.Count = this._currentPathFigureData.Count + 1U;
			this._currentPathFigureData.Size = (uint)(this._currOffset - this._currentPathFigureDataOffset);
			this._currentPathFigureData.OffsetToLastSegment = (uint)(currOffset - this._currentPathFigureDataOffset);
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x0007C008 File Offset: 0x0007B408
		internal byte[] GetData()
		{
			this.ShrinkToFit();
			return this._chunkList[0];
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x0007C028 File Offset: 0x0007B428
		internal override void SetClosedState(bool isClosed)
		{
			if (this._currentPathFigureDataOffset == -1)
			{
				throw new InvalidOperationException(SR.Get("StreamGeometry_NeedBeginFigure"));
			}
			this._currentPathFigureData.Flags = (this._currentPathFigureData.Flags & ~MilPathFigureFlags.IsClosed);
			this._currentPathFigureData.Flags = (this._currentPathFigureData.Flags | (isClosed ? MilPathFigureFlags.IsClosed : ((MilPathFigureFlags)0)));
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x0007C078 File Offset: 0x0007B478
		private void VerifyApi()
		{
			base.VerifyAccess();
			if (this._disposed)
			{
				throw new ObjectDisposedException("ByteStreamGeometryContext");
			}
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x0007C0A0 File Offset: 0x0007B4A0
		protected virtual void CloseCore(byte[] geometryData)
		{
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x0007C0B0 File Offset: 0x0007B4B0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void DisposeCore()
		{
			if (!this._disposed)
			{
				this.FinishFigure();
				fixed (MIL_PATHGEOMETRY* ptr = &this._currentPathGeometryData)
				{
					MIL_PATHGEOMETRY* pbData = ptr;
					this.OverwriteData((byte*)pbData, 0, sizeof(MIL_PATHGEOMETRY));
				}
				this.ShrinkToFit();
				this.CloseCore(this._chunkList[0]);
				this._disposed = true;
			}
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x0007C108 File Offset: 0x0007B508
		[SecurityCritical]
		private unsafe void ReadData(byte* pbData, int bufferOffset, int cbDataSize)
		{
			Invariant.Assert(cbDataSize >= 0);
			Invariant.Assert(bufferOffset >= 0);
			Invariant.Assert(this._currOffset >= checked(bufferOffset + cbDataSize));
			this.ReadWriteData(true, pbData, cbDataSize, 0, ref bufferOffset);
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x0007C14C File Offset: 0x0007B54C
		[SecurityCritical]
		private unsafe void OverwriteData(byte* pbData, int bufferOffset, int cbDataSize)
		{
			Invariant.Assert(cbDataSize >= 0);
			int num = checked(bufferOffset + cbDataSize);
			Invariant.Assert(num <= this._currOffset);
			this.ReadWriteData(false, pbData, cbDataSize, 0, ref bufferOffset);
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x0007C188 File Offset: 0x0007B588
		[SecurityCritical]
		private unsafe void AppendData(byte* pbData, int cbDataSize)
		{
			Invariant.Assert(cbDataSize >= 0);
			int currOffset = checked(this._currOffset + cbDataSize);
			if (this._chunkList.Count == 0)
			{
				byte[] value = ByteStreamGeometryContext.AcquireChunkFromPool();
				this._chunkList.Add(value);
			}
			this.ReadWriteData(false, pbData, cbDataSize, this._chunkList.Count - 1, ref this._currChunkOffset);
			this._currOffset = currOffset;
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x0007C1F0 File Offset: 0x0007B5F0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe void ShrinkToFit()
		{
			if (this._chunkList.Count > 1 || this._chunkList[0].Length != this._currOffset)
			{
				byte[] array = new byte[this._currOffset];
				byte[] array2;
				byte* pbData;
				if ((array2 = array) == null || array2.Length == 0)
				{
					pbData = null;
				}
				else
				{
					pbData = &array2[0];
				}
				this.ReadData(pbData, 0, this._currOffset);
				array2 = null;
				ByteStreamGeometryContext.ReturnChunkToPool(this._chunkList[0]);
				if (this._chunkList.Count == 1)
				{
					this._chunkList[0] = array;
					return;
				}
				this._chunkList = default(FrugalStructList<byte[]>);
				this._chunkList.Add(array);
			}
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x0007C2A0 File Offset: 0x0007B6A0
		[SecurityCritical]
		private unsafe void ReadWriteData(bool reading, byte* pbData, int cbDataSize, int currentChunk, ref int bufferOffset)
		{
			Invariant.Assert(cbDataSize >= 0);
			while (bufferOffset > this._chunkList[currentChunk].Length)
			{
				bufferOffset -= this._chunkList[currentChunk].Length;
				currentChunk++;
			}
			while (cbDataSize > 0)
			{
				int num = Math.Min(cbDataSize, this._chunkList[currentChunk].Length - bufferOffset);
				if (num > 0)
				{
					Invariant.Assert(this._chunkList[currentChunk] != null && this._chunkList[currentChunk].Length >= bufferOffset + num);
					Invariant.Assert(this._chunkList[currentChunk].Length != 0);
					if (reading)
					{
						Marshal.Copy(this._chunkList[currentChunk], bufferOffset, (IntPtr)((void*)pbData), num);
					}
					else
					{
						Marshal.Copy((IntPtr)((void*)pbData), this._chunkList[currentChunk], bufferOffset, num);
					}
					cbDataSize -= num;
					pbData += num;
					bufferOffset += num;
				}
				if (cbDataSize > 0)
				{
					checked
					{
						currentChunk++;
					}
					if (this._chunkList.Count == currentChunk)
					{
						Invariant.Assert(!reading);
						int num2 = Math.Min(2 * this._chunkList[this._chunkList.Count - 1].Length, 1048576);
						this._chunkList.Add(new byte[num2]);
					}
					bufferOffset = 0;
				}
			}
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x0007C40C File Offset: 0x0007B80C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private unsafe void FinishFigure()
		{
			if (this._currentPathFigureDataOffset != -1)
			{
				this.FinishSegment();
				fixed (MIL_PATHFIGURE* ptr = &this._currentPathFigureData)
				{
					MIL_PATHFIGURE* pbData = ptr;
					this.OverwriteData((byte*)pbData, this._currentPathFigureDataOffset, sizeof(MIL_PATHFIGURE));
				}
				this._currentPathGeometryData.Flags = (this._currentPathGeometryData.Flags | (((this._currentPathFigureData.Flags & MilPathFigureFlags.HasCurves) != (MilPathFigureFlags)0) ? MilPathGeometryFlags.HasCurves : ((MilPathGeometryFlags)0)));
				this._currentPathGeometryData.Flags = (this._currentPathGeometryData.Flags | (((this._currentPathFigureData.Flags & MilPathFigureFlags.HasGaps) != (MilPathFigureFlags)0) ? MilPathGeometryFlags.HasGaps : ((MilPathGeometryFlags)0)));
				this._currentPathGeometryData.Flags = (this._currentPathGeometryData.Flags | (((this._currentPathFigureData.Flags & MilPathFigureFlags.IsFillable) == (MilPathFigureFlags)0) ? MilPathGeometryFlags.HasHollows : ((MilPathGeometryFlags)0)));
				this._currentPathGeometryData.FigureCount = this._currentPathGeometryData.FigureCount + 1U;
				this._currentPathGeometryData.Size = (uint)this._currOffset;
				this._lastFigureSize = this._currentPathFigureData.Size;
				this._currentPathFigureDataOffset = -1;
				this._currentPathFigureData = default(MIL_PATHFIGURE);
				this._lastSegmentSize = 0U;
			}
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x0007C500 File Offset: 0x0007B900
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private unsafe void FinishSegment()
		{
			if (this._currentPolySegmentDataOffset != -1)
			{
				fixed (MIL_SEGMENT_POLY* ptr = &this._currentPolySegmentData)
				{
					MIL_SEGMENT_POLY* pbData = ptr;
					this.OverwriteData((byte*)pbData, this._currentPolySegmentDataOffset, sizeof(MIL_SEGMENT_POLY));
				}
				this._lastSegmentSize = (uint)((long)sizeof(MIL_SEGMENT_POLY) + (long)sizeof(Point) * (long)((ulong)this._currentPolySegmentData.Count));
				if ((this._currentPolySegmentData.Flags & MILCoreSegFlags.SegIsAGap) != (MILCoreSegFlags)0)
				{
					this._currentPathFigureData.Flags = (this._currentPathFigureData.Flags | MilPathFigureFlags.HasGaps);
				}
				if ((this._currentPolySegmentData.Flags & MILCoreSegFlags.SegIsCurved) != (MILCoreSegFlags)0)
				{
					this._currentPathFigureData.Flags = (this._currentPathFigureData.Flags | MilPathFigureFlags.HasCurves);
				}
				this._currentPathFigureData.Count = this._currentPathFigureData.Count + 1U;
				this._currentPathFigureData.Size = (uint)(this._currOffset - this._currentPathFigureDataOffset);
				this._currentPathFigureData.OffsetToLastSegment = (uint)(this._currentPolySegmentDataOffset - this._currentPathFigureDataOffset);
				this._currentPolySegmentDataOffset = -1;
				this._currentPolySegmentData = default(MIL_SEGMENT_POLY);
			}
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x0007C5F0 File Offset: 0x0007B9F0
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private unsafe void GenericPolyTo(IList<Point> points, bool isStroked, bool isSmoothJoin, bool hasCurves, int pointCountMultiple, MIL_SEGMENT_TYPE segmentType)
		{
			if (this._currentPathFigureDataOffset == -1)
			{
				throw new InvalidOperationException(SR.Get("StreamGeometry_NeedBeginFigure"));
			}
			if (points == null)
			{
				return;
			}
			int num = points.Count;
			num -= num % pointCountMultiple;
			if (num <= 0)
			{
				return;
			}
			this.GenericPolyToHelper(isStroked, isSmoothJoin, hasCurves, segmentType);
			for (int i = 0; i < num; i++)
			{
				Point point = points[i];
				this.AppendData((byte*)(&point), sizeof(Point));
				this._currentPolySegmentData.Count = this._currentPolySegmentData.Count + 1U;
			}
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x0007C66C File Offset: 0x0007BA6C
		[SecurityCritical]
		private unsafe void GenericPolyTo(Point* points, int count, bool isStroked, bool isSmoothJoin, bool hasCurves, MIL_SEGMENT_TYPE segmentType)
		{
			if (this._currentPathFigureDataOffset == -1)
			{
				throw new InvalidOperationException(SR.Get("StreamGeometry_NeedBeginFigure"));
			}
			this.GenericPolyToHelper(isStroked, isSmoothJoin, hasCurves, segmentType);
			this.AppendData((byte*)points, sizeof(Point) * count);
			this._currentPolySegmentData.Count = this._currentPolySegmentData.Count + (uint)count;
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x0007C6C0 File Offset: 0x0007BAC0
		[SecurityCritical]
		private unsafe void GenericPolyToHelper(bool isStroked, bool isSmoothJoin, bool hasCurves, MIL_SEGMENT_TYPE segmentType)
		{
			if (this._currentPolySegmentDataOffset != -1 && (this._currentPolySegmentData.Type != segmentType || (this._currentPolySegmentData.Flags & MILCoreSegFlags.SegIsAGap) == (MILCoreSegFlags)0 != isStroked || (this._currentPolySegmentData.Flags & MILCoreSegFlags.SegSmoothJoin) > (MILCoreSegFlags)0 != isSmoothJoin))
			{
				this.FinishSegment();
			}
			if (this._currentPolySegmentDataOffset == -1)
			{
				int currOffset = this._currOffset;
				MIL_SEGMENT_POLY mil_SEGMENT_POLY;
				this.AppendData((byte*)(&mil_SEGMENT_POLY), sizeof(MIL_SEGMENT_POLY));
				this._currentPolySegmentDataOffset = currOffset;
				this._currentPolySegmentData.Type = segmentType;
				this._currentPolySegmentData.Flags = (this._currentPolySegmentData.Flags | (isStroked ? ((MILCoreSegFlags)0) : MILCoreSegFlags.SegIsAGap));
				this._currentPolySegmentData.Flags = (this._currentPolySegmentData.Flags | (hasCurves ? MILCoreSegFlags.SegIsCurved : ((MILCoreSegFlags)0)));
				this._currentPolySegmentData.Flags = (this._currentPolySegmentData.Flags | (isSmoothJoin ? MILCoreSegFlags.SegSmoothJoin : ((MILCoreSegFlags)0)));
				this._currentPolySegmentData.BackSize = this._lastSegmentSize;
			}
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x0007C79C File Offset: 0x0007BB9C
		private static byte[] AcquireChunkFromPool()
		{
			byte[] pooledChunk = ByteStreamGeometryContext._pooledChunk;
			if (pooledChunk == null)
			{
				return new byte[2048];
			}
			ByteStreamGeometryContext._pooledChunk = null;
			return pooledChunk;
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x0007C7C4 File Offset: 0x0007BBC4
		private static void ReturnChunkToPool(byte[] chunk)
		{
			if (chunk.Length == 2048)
			{
				ByteStreamGeometryContext._pooledChunk = chunk;
			}
		}

		// Token: 0x0400100C RID: 4108
		private bool _disposed;

		// Token: 0x0400100D RID: 4109
		private int _currChunkOffset;

		// Token: 0x0400100E RID: 4110
		private FrugalStructList<byte[]> _chunkList;

		// Token: 0x0400100F RID: 4111
		private int _currOffset;

		// Token: 0x04001010 RID: 4112
		private MIL_PATHGEOMETRY _currentPathGeometryData;

		// Token: 0x04001011 RID: 4113
		private MIL_PATHFIGURE _currentPathFigureData;

		// Token: 0x04001012 RID: 4114
		private int _currentPathFigureDataOffset = -1;

		// Token: 0x04001013 RID: 4115
		private MIL_SEGMENT_POLY _currentPolySegmentData;

		// Token: 0x04001014 RID: 4116
		private int _currentPolySegmentDataOffset = -1;

		// Token: 0x04001015 RID: 4117
		private uint _lastSegmentSize;

		// Token: 0x04001016 RID: 4118
		private uint _lastFigureSize;

		// Token: 0x04001017 RID: 4119
		private const int c_defaultChunkSize = 2048;

		// Token: 0x04001018 RID: 4120
		private const int c_maxChunkSize = 1048576;

		// Token: 0x04001019 RID: 4121
		[ThreadStatic]
		private static byte[] _pooledChunk;
	}
}
