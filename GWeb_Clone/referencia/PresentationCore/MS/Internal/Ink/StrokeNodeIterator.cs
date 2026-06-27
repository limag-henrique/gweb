using System;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;

namespace MS.Internal.Ink
{
	// Token: 0x020007C2 RID: 1986
	internal class StrokeNodeIterator
	{
		// Token: 0x060053B4 RID: 21428 RVA: 0x0015118C File Offset: 0x0015058C
		internal static StrokeNodeIterator GetIterator(Stroke stroke, DrawingAttributes drawingAttributes)
		{
			if (stroke == null)
			{
				throw new ArgumentNullException("stroke");
			}
			if (drawingAttributes == null)
			{
				throw new ArgumentNullException("drawingAttributes");
			}
			StylusPointCollection stylusPoints = drawingAttributes.FitToCurve ? stroke.GetBezierStylusPoints() : stroke.StylusPoints;
			return StrokeNodeIterator.GetIterator(stylusPoints, drawingAttributes);
		}

		// Token: 0x060053B5 RID: 21429 RVA: 0x001511DC File Offset: 0x001505DC
		internal static StrokeNodeIterator GetIterator(StylusPointCollection stylusPoints, DrawingAttributes drawingAttributes)
		{
			if (stylusPoints == null)
			{
				throw new ArgumentNullException("stylusPoints");
			}
			if (drawingAttributes == null)
			{
				throw new ArgumentNullException("drawingAttributes");
			}
			StrokeNodeOperations operations = StrokeNodeOperations.CreateInstance(drawingAttributes.StylusShape);
			bool usePressure = !drawingAttributes.IgnorePressure;
			return new StrokeNodeIterator(stylusPoints, operations, usePressure);
		}

		// Token: 0x060053B6 RID: 21430 RVA: 0x0015122C File Offset: 0x0015062C
		private static float GetNormalizedPressureFactor(float stylusPointPressureFactor)
		{
			return 1.5f * stylusPointPressureFactor + 0.25f;
		}

		// Token: 0x060053B7 RID: 21431 RVA: 0x00151248 File Offset: 0x00150648
		internal StrokeNodeIterator(StylusShape nodeShape) : this(null, StrokeNodeOperations.CreateInstance(nodeShape), false)
		{
		}

		// Token: 0x060053B8 RID: 21432 RVA: 0x00151264 File Offset: 0x00150664
		internal StrokeNodeIterator(DrawingAttributes drawingAttributes) : this(null, StrokeNodeOperations.CreateInstance((drawingAttributes == null) ? null : drawingAttributes.StylusShape), !(drawingAttributes == null) && !drawingAttributes.IgnorePressure)
		{
		}

		// Token: 0x060053B9 RID: 21433 RVA: 0x001512A4 File Offset: 0x001506A4
		internal StrokeNodeIterator(StylusPointCollection stylusPoints, StrokeNodeOperations operations, bool usePressure)
		{
			this._stylusPoints = stylusPoints;
			if (operations == null)
			{
				throw new ArgumentNullException("operations");
			}
			this._operations = operations;
			this._usePressure = usePressure;
		}

		// Token: 0x060053BA RID: 21434 RVA: 0x001512DC File Offset: 0x001506DC
		internal StrokeNodeIterator GetIteratorForNextSegment(StylusPointCollection stylusPoints)
		{
			if (stylusPoints == null)
			{
				throw new ArgumentNullException("stylusPoints");
			}
			if (this._stylusPoints != null && this._stylusPoints.Count > 0 && stylusPoints.Count > 0)
			{
				StylusPoint item = stylusPoints[0];
				StylusPoint stylusPoint = this._stylusPoints[this._stylusPoints.Count - 1];
				item.X = stylusPoint.X;
				item.Y = stylusPoint.Y;
				item.PressureFactor = stylusPoint.PressureFactor;
				stylusPoints.Insert(0, item);
			}
			return new StrokeNodeIterator(stylusPoints, this._operations, this._usePressure);
		}

		// Token: 0x060053BB RID: 21435 RVA: 0x0015137C File Offset: 0x0015077C
		internal StrokeNodeIterator GetIteratorForNextSegment(Point[] points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			StylusPointCollection stylusPointCollection = new StylusPointCollection(points);
			if (this._stylusPoints != null && this._stylusPoints.Count > 0)
			{
				stylusPointCollection.Insert(0, this._stylusPoints[this._stylusPoints.Count - 1]);
			}
			return new StrokeNodeIterator(stylusPointCollection, this._operations, this._usePressure);
		}

		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x060053BC RID: 21436 RVA: 0x001513E8 File Offset: 0x001507E8
		internal int Count
		{
			get
			{
				if (this._stylusPoints == null)
				{
					return 0;
				}
				return this._stylusPoints.Count;
			}
		}

		// Token: 0x1700116F RID: 4463
		internal StrokeNode this[int index]
		{
			get
			{
				return this[index, (index == 0) ? -1 : (index - 1)];
			}
		}

		// Token: 0x17001170 RID: 4464
		internal StrokeNode this[int index, int previousIndex]
		{
			get
			{
				if (this._stylusPoints == null || index < 0 || index >= this._stylusPoints.Count || previousIndex < -1 || previousIndex >= index)
				{
					throw new IndexOutOfRangeException();
				}
				StylusPoint stylusPoint = this._stylusPoints[index];
				StylusPoint stylusPoint2 = (previousIndex == -1) ? default(StylusPoint) : this._stylusPoints[previousIndex];
				float pressure = 1f;
				float pressure2 = 1f;
				if (this._usePressure)
				{
					pressure = StrokeNodeIterator.GetNormalizedPressureFactor(stylusPoint.PressureFactor);
					pressure2 = StrokeNodeIterator.GetNormalizedPressureFactor(stylusPoint2.PressureFactor);
				}
				StrokeNodeData nodeData = new StrokeNodeData((Point)stylusPoint, pressure);
				StrokeNodeData empty = StrokeNodeData.Empty;
				if (previousIndex != -1)
				{
					empty = new StrokeNodeData((Point)stylusPoint2, pressure2);
				}
				return new StrokeNode(this._operations, previousIndex + 1, nodeData, empty, index == this._stylusPoints.Count - 1);
			}
		}

		// Token: 0x040025CB RID: 9675
		private bool _usePressure;

		// Token: 0x040025CC RID: 9676
		private StrokeNodeOperations _operations;

		// Token: 0x040025CD RID: 9677
		private StylusPointCollection _stylusPoints;
	}
}
