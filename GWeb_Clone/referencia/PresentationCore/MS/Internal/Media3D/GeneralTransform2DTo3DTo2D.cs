using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using MS.Internal.PresentationCore;

namespace MS.Internal.Media3D
{
	// Token: 0x020006F1 RID: 1777
	internal class GeneralTransform2DTo3DTo2D : GeneralTransform
	{
		// Token: 0x06004C88 RID: 19592 RVA: 0x0012C10C File Offset: 0x0012B50C
		internal GeneralTransform2DTo3DTo2D(Viewport2DVisual3D visual3D, Visual fromVisual)
		{
			this.IsInverse = false;
			this._geometry = new MeshGeometry3D();
			this._geometry.Positions = visual3D.InternalPositionsCache;
			this._geometry.TextureCoordinates = visual3D.InternalTextureCoordinatesCache;
			this._geometry.TriangleIndices = visual3D.InternalTriangleIndicesCache;
			this._geometry.Freeze();
			Visual visual = visual3D.Visual;
			Visual visual2 = (fromVisual == visual._parent) ? visual : fromVisual;
			this._visualBrushBounds = visual.CalculateSubgraphRenderBoundsOuterSpace();
			this._visualBounds = visual2.CalculateSubgraphRenderBoundsInnerSpace();
			GeneralTransformGroup generalTransformGroup = new GeneralTransformGroup();
			generalTransformGroup.Children.Add(visual2.TransformToAncestor(visual));
			generalTransformGroup.Children.Add(visual.TransformToOuterSpace());
			generalTransformGroup.Freeze();
			this._transform2D = generalTransformGroup;
			this._transform2DInverse = this._transform2D.Inverse;
			if (this._transform2DInverse != null)
			{
				this._transform2DInverse.Freeze();
			}
			Viewport3DVisual viewport3DVisual = (Viewport3DVisual)VisualTreeHelper.GetContainingVisual2D(visual3D);
			this._camera = viewport3DVisual.Camera;
			if (this._camera != null)
			{
				this._camera = (Camera)viewport3DVisual.Camera.GetCurrentValueAsFrozen();
			}
			this._viewSize = viewport3DVisual.Viewport.Size;
			this._boundingRect = viewport3DVisual.ComputeSubgraphBounds3D();
			this._objectToViewport = visual3D.TransformToAncestor(viewport3DVisual);
			if (this._objectToViewport != null)
			{
				this._objectToViewport.Freeze();
			}
			this._worldTransformation = M3DUtil.GetWorldTransformationMatrix(visual3D);
			this._validEdgesCache = null;
		}

		// Token: 0x06004C89 RID: 19593 RVA: 0x0012C280 File Offset: 0x0012B680
		internal GeneralTransform2DTo3DTo2D()
		{
		}

		// Token: 0x06004C8A RID: 19594 RVA: 0x0012C294 File Offset: 0x0012B694
		public override bool TryTransform(Point inPoint, out Point result)
		{
			if (this.IsInverse)
			{
				return this.TryInverseTransform(inPoint, out result);
			}
			return this.TryRegularTransform(inPoint, out result);
		}

		// Token: 0x06004C8B RID: 19595 RVA: 0x0012C2BC File Offset: 0x0012B6BC
		private bool TryInverseTransform(Point inPoint, out Point result)
		{
			bool foundIntersection = false;
			if (this._camera != null)
			{
				double distanceAdjustment;
				RayHitTestParameters rayHitTestParameters = this._camera.RayFromViewportPoint(inPoint, this._viewSize, this._boundingRect, out distanceAdjustment);
				rayHitTestParameters.PushVisualTransform(new MatrixTransform3D(this._worldTransformation));
				Point pointHit = default(Point);
				this._geometry.RayHitTest(rayHitTestParameters, FaceType.Front);
				rayHitTestParameters.RaiseCallback(delegate(HitTestResult rawresult)
				{
					RayHitTestResult rayHitTestResult = rawresult as RayHitTestResult;
					if (rayHitTestResult != null)
					{
						foundIntersection = Viewport2DVisual3D.GetIntersectionInfo(rayHitTestResult, out pointHit);
					}
					return HitTestResultBehavior.Stop;
				}, null, HitTestResultBehavior.Continue, distanceAdjustment);
				if (!foundIntersection)
				{
					foundIntersection = this.HandleOffMesh(inPoint, out pointHit);
				}
				result = Viewport2DVisual3D.TextureCoordsToVisualCoords(pointHit, this._visualBrushBounds);
			}
			else
			{
				result = default(Point);
			}
			return foundIntersection;
		}

		// Token: 0x06004C8C RID: 19596 RVA: 0x0012C380 File Offset: 0x0012B780
		private bool HandleOffMesh(Point mousePos, out Point outPoint)
		{
			Point[] array = new Point[4];
			if (this._validEdgesCache == null)
			{
				array[0] = this._transform2D.Transform(new Point(this._visualBounds.Left, this._visualBounds.Top));
				array[1] = this._transform2D.Transform(new Point(this._visualBounds.Right, this._visualBounds.Top));
				array[2] = this._transform2D.Transform(new Point(this._visualBounds.Right, this._visualBounds.Bottom));
				array[3] = this._transform2D.Transform(new Point(this._visualBounds.Left, this._visualBounds.Bottom));
				Point[] array2 = new Point[4];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = Viewport2DVisual3D.VisualCoordsToTextureCoords(array[i], this._visualBrushBounds);
				}
				this._validEdgesCache = this.GrabValidEdges(array2);
			}
			return this.FindClosestIntersection(mousePos, this._validEdgesCache, out outPoint);
		}

		// Token: 0x06004C8D RID: 19597 RVA: 0x0012C49C File Offset: 0x0012B89C
		private List<HitTestEdge> GrabValidEdges(Point[] visualTexCoordBounds)
		{
			List<HitTestEdge> list = new List<HitTestEdge>();
			Dictionary<GeneralTransform2DTo3DTo2D.Edge, GeneralTransform2DTo3DTo2D.EdgeInfo> dictionary = new Dictionary<GeneralTransform2DTo3DTo2D.Edge, GeneralTransform2DTo3DTo2D.EdgeInfo>();
			Point3DCollection positions = this._geometry.Positions;
			PointCollection textureCoordinates = this._geometry.TextureCoordinates;
			Int32Collection triangleIndices = this._geometry.TriangleIndices;
			if (positions == null || textureCoordinates == null)
			{
				return new List<HitTestEdge>();
			}
			Matrix3D matrix3D = this._worldTransformation * this._camera.GetViewMatrix();
			try
			{
				matrix3D.Invert();
			}
			catch (InvalidOperationException)
			{
				return new List<HitTestEdge>();
			}
			Point3D camPosObjSpace = matrix3D.Transform(new Point3D(0.0, 0.0, 0.0));
			Rect empty = Rect.Empty;
			for (int i = 0; i < visualTexCoordBounds.Length; i++)
			{
				empty.Union(visualTexCoordBounds[i]);
			}
			Point3D[] array = new Point3D[3];
			Point[] array2 = new Point[3];
			if (triangleIndices == null || triangleIndices.Count == 0)
			{
				int count = textureCoordinates.Count;
				int num = positions.Count;
				num -= num % 3;
				for (int j = 0; j < num; j += 3)
				{
					Rect empty2 = Rect.Empty;
					for (int k = 0; k < 3; k++)
					{
						array[k] = positions[j + k];
						if (j + k < count)
						{
							array2[k] = textureCoordinates[j + k];
						}
						else
						{
							array2[k] = new Point(0.0, 0.0);
						}
						empty2.Union(array2[k]);
					}
					if (empty.IntersectsWith(empty2))
					{
						this.ProcessTriangle(array, array2, visualTexCoordBounds, list, dictionary, camPosObjSpace);
					}
				}
			}
			else
			{
				int count2 = triangleIndices.Count;
				int count3 = positions.Count;
				int count4 = textureCoordinates.Count;
				int[] array3 = new int[3];
				for (int l = 2; l < count2; l += 3)
				{
					Rect empty3 = Rect.Empty;
					bool flag = true;
					bool flag2 = true;
					for (int m = 0; m < 3; m++)
					{
						array3[m] = triangleIndices[l - 2 + m];
						if (array3[m] < 0 || array3[m] >= count3)
						{
							flag2 = false;
							break;
						}
						if (array3[m] < 0 || array3[m] >= count4)
						{
							flag = false;
							break;
						}
						array[m] = positions[array3[m]];
						array2[m] = textureCoordinates[array3[m]];
						empty3.Union(array2[m]);
					}
					if (!flag2)
					{
						break;
					}
					if (flag && empty.IntersectsWith(empty3))
					{
						this.ProcessTriangle(array, array2, visualTexCoordBounds, list, dictionary, camPosObjSpace);
					}
				}
			}
			foreach (GeneralTransform2DTo3DTo2D.Edge edge in dictionary.Keys)
			{
				GeneralTransform2DTo3DTo2D.EdgeInfo edgeInfo = dictionary[edge];
				if (edgeInfo._hasFrontFace && edgeInfo._numSharing == 1)
				{
					this.HandleSilhouetteEdge(edgeInfo._uv1, edgeInfo._uv2, edge._start, edge._end, visualTexCoordBounds, list);
				}
			}
			if (this._objectToViewport != null)
			{
				for (int n = 0; n < list.Count; n++)
				{
					list[n].Project(this._objectToViewport);
				}
			}
			else
			{
				list = new List<HitTestEdge>();
			}
			return list;
		}

		// Token: 0x06004C8E RID: 19598 RVA: 0x0012C810 File Offset: 0x0012BC10
		private void ProcessTriangle(Point3D[] p, Point[] uv, Point[] visualTexCoordBounds, List<HitTestEdge> edgeList, Dictionary<GeneralTransform2DTo3DTo2D.Edge, GeneralTransform2DTo3DTo2D.EdgeInfo> adjInformation, Point3D camPosObjSpace)
		{
			Vector3D vector = Vector3D.CrossProduct(p[1] - p[0], p[2] - p[0]);
			Vector3D vector2 = camPosObjSpace - p[0];
			if (vector.X != 0.0 || vector.Y != 0.0 || vector.Z != 0.0)
			{
				double num = Vector3D.DotProduct(vector, vector2);
				if (num > 0.0)
				{
					this.ProcessTriangleEdges(p, uv, visualTexCoordBounds, GeneralTransform2DTo3DTo2D.PolygonSide.FRONT, edgeList, adjInformation);
					this.ProcessVisualBoundsIntersections(p, uv, visualTexCoordBounds, edgeList);
					return;
				}
				this.ProcessTriangleEdges(p, uv, visualTexCoordBounds, GeneralTransform2DTo3DTo2D.PolygonSide.BACK, edgeList, adjInformation);
			}
		}

		// Token: 0x06004C8F RID: 19599 RVA: 0x0012C8CC File Offset: 0x0012BCCC
		private void ProcessVisualBoundsIntersections(Point3D[] p, Point[] uv, Point[] visualTexCoordBounds, List<HitTestEdge> edgeList)
		{
			List<Point3D> list = new List<Point3D>();
			List<Point> list2 = new List<Point>();
			for (int i = 0; i < visualTexCoordBounds.Length; i++)
			{
				Point point = visualTexCoordBounds[i];
				Point point2 = visualTexCoordBounds[(i + 1) % visualTexCoordBounds.Length];
				list.Clear();
				list2.Clear();
				bool flag = false;
				for (int j = 0; j < uv.Length; j++)
				{
					Point point3 = uv[j];
					Point point4 = uv[(j + 1) % uv.Length];
					Point3D point3D = p[j];
					Point3D point3D2 = p[(j + 1) % p.Length];
					if (Math.Max(point.X, point2.X) >= Math.Min(point3.X, point4.X) && Math.Min(point.X, point2.X) <= Math.Max(point3.X, point4.X) && Math.Max(point.Y, point2.Y) >= Math.Min(point3.Y, point4.Y) && Math.Min(point.Y, point2.Y) <= Math.Max(point3.Y, point4.Y))
					{
						bool flag2 = false;
						Vector vector = point4 - point3;
						double num = this.IntersectRayLine(point3, vector, point, point2, out flag2);
						if (flag2)
						{
							this.HandleCoincidentLines(point, point2, point3D, point3D2, point3, point4, edgeList);
							flag = true;
							break;
						}
						if (num >= 0.0 && num <= 1.0)
						{
							Point point5 = point3 + vector * num;
							Point3D item = point3D + (point3D2 - point3D) * num;
							double length = (point - point2).Length;
							if ((point5 - point).Length < length && (point5 - point2).Length < length)
							{
								list.Add(item);
								list2.Add(point5);
							}
						}
					}
				}
				if (!flag)
				{
					Point3D p3;
					Point3D p4;
					if (list.Count >= 2)
					{
						edgeList.Add(new HitTestEdge(list[0], list[1], list2[0], list2[1]));
					}
					else if (list.Count == 1)
					{
						Point3D p2;
						if (M3DUtil.IsPointInTriangle(point, uv, p, out p2))
						{
							edgeList.Add(new HitTestEdge(list[0], p2, list2[0], point));
						}
						if (M3DUtil.IsPointInTriangle(point2, uv, p, out p2))
						{
							edgeList.Add(new HitTestEdge(list[0], p2, list2[0], point2));
						}
					}
					else if (M3DUtil.IsPointInTriangle(point, uv, p, out p3) && M3DUtil.IsPointInTriangle(point2, uv, p, out p4))
					{
						edgeList.Add(new HitTestEdge(p3, p4, point, point2));
					}
				}
			}
		}

		// Token: 0x06004C90 RID: 19600 RVA: 0x0012CBB4 File Offset: 0x0012BFB4
		private void HandleCoincidentLines(Point visUV1, Point visUV2, Point3D tri3D1, Point3D tri3D2, Point triUV1, Point triUV2, List<HitTestEdge> edgeList)
		{
			Point uv;
			Point3D p;
			Point uv2;
			Point3D p2;
			if (Math.Abs(visUV1.X - visUV2.X) > Math.Abs(visUV1.Y - visUV2.Y))
			{
				Point point;
				Point point2;
				if (visUV1.X <= visUV2.X)
				{
					point = visUV1;
					point2 = visUV2;
				}
				else
				{
					point = visUV2;
					point2 = visUV1;
				}
				Point point3;
				Point3D point3D;
				Point point4;
				Point3D point3D2;
				if (triUV1.X <= triUV2.X)
				{
					point3 = triUV1;
					point3D = tri3D1;
					point4 = triUV2;
					point3D2 = tri3D2;
				}
				else
				{
					point3 = triUV2;
					point3D = tri3D2;
					point4 = triUV1;
					point3D2 = tri3D1;
				}
				if (point.X < point3.X)
				{
					uv = point3;
					p = point3D;
				}
				else
				{
					uv = point;
					p = point3D + (point.X - point3.X) / (point4.X - point3.X) * (point3D2 - point3D);
				}
				if (point2.X > point4.X)
				{
					uv2 = point4;
					p2 = point3D2;
				}
				else
				{
					uv2 = point2;
					p2 = point3D + (point2.X - point3.X) / (point4.X - point3.X) * (point3D2 - point3D);
				}
			}
			else
			{
				Point point;
				Point point2;
				if (visUV1.Y <= visUV2.Y)
				{
					point = visUV1;
					point2 = visUV2;
				}
				else
				{
					point = visUV2;
					point2 = visUV1;
				}
				Point point3;
				Point3D point3D;
				Point point4;
				Point3D point3D2;
				if (triUV1.Y <= triUV2.Y)
				{
					point3 = triUV1;
					point3D = tri3D1;
					point4 = triUV2;
					point3D2 = tri3D2;
				}
				else
				{
					point3 = triUV2;
					point3D = tri3D2;
					point4 = triUV1;
					point3D2 = tri3D1;
				}
				if (point.Y < point3.Y)
				{
					uv = point3;
					p = point3D;
				}
				else
				{
					uv = point;
					p = point3D + (point.Y - point3.Y) / (point4.Y - point3.Y) * (point3D2 - point3D);
				}
				if (point2.Y > point4.Y)
				{
					uv2 = point4;
					p2 = point3D2;
				}
				else
				{
					uv2 = point2;
					p2 = point3D + (point2.Y - point3.Y) / (point4.Y - point3.Y) * (point3D2 - point3D);
				}
			}
			edgeList.Add(new HitTestEdge(p, p2, uv, uv2));
		}

		// Token: 0x06004C91 RID: 19601 RVA: 0x0012CDD4 File Offset: 0x0012C1D4
		private double IntersectRayLine(Point o, Vector d, Point p1, Point p2, out bool coinc)
		{
			coinc = false;
			double num = p2.Y - p1.Y;
			double num2 = p2.X - p1.X;
			if (num2 == 0.0)
			{
				if (d.X == 0.0)
				{
					coinc = (o.X == p1.X);
					return -1.0;
				}
				return (p2.X - o.X) / d.X;
			}
			else
			{
				double num3 = (o.X - p1.X) * num / num2 - o.Y + p1.Y;
				double num4 = d.Y - d.X * num / num2;
				if (num4 == 0.0)
				{
					double num5 = -o.X * num / num2 + o.Y;
					double num6 = -p1.X * num / num2 + p1.Y;
					coinc = (num5 == num6);
					return -1.0;
				}
				return num3 / num4;
			}
		}

		// Token: 0x06004C92 RID: 19602 RVA: 0x0012CEE0 File Offset: 0x0012C2E0
		private void ProcessTriangleEdges(Point3D[] p, Point[] uv, Point[] visualTexCoordBounds, GeneralTransform2DTo3DTo2D.PolygonSide polygonSide, List<HitTestEdge> edgeList, Dictionary<GeneralTransform2DTo3DTo2D.Edge, GeneralTransform2DTo3DTo2D.EdgeInfo> adjInformation)
		{
			for (int i = 0; i < p.Length; i++)
			{
				Point3D point3D = p[i];
				Point3D point3D2 = p[(i + 1) % p.Length];
				GeneralTransform2DTo3DTo2D.Edge edge;
				Point uv2;
				Point uv3;
				if (point3D.X < point3D2.X || (point3D.X == point3D2.X && point3D.Y < point3D2.Y) || (point3D.X == point3D2.X && point3D.Y == point3D2.Y && point3D.Z < point3D.Z))
				{
					edge = new GeneralTransform2DTo3DTo2D.Edge(point3D, point3D2);
					uv2 = uv[i];
					uv3 = uv[(i + 1) % p.Length];
				}
				else
				{
					edge = new GeneralTransform2DTo3DTo2D.Edge(point3D2, point3D);
					uv3 = uv[i];
					uv2 = uv[(i + 1) % p.Length];
				}
				GeneralTransform2DTo3DTo2D.EdgeInfo edgeInfo;
				if (adjInformation.ContainsKey(edge))
				{
					edgeInfo = adjInformation[edge];
				}
				else
				{
					edgeInfo = new GeneralTransform2DTo3DTo2D.EdgeInfo();
					adjInformation[edge] = edgeInfo;
				}
				edgeInfo._numSharing++;
				bool flag = edgeInfo._hasBackFace && edgeInfo._hasFrontFace;
				if (polygonSide == GeneralTransform2DTo3DTo2D.PolygonSide.FRONT)
				{
					edgeInfo._hasFrontFace = true;
					edgeInfo._uv1 = uv2;
					edgeInfo._uv2 = uv3;
				}
				else
				{
					edgeInfo._hasBackFace = true;
				}
				if (!flag && edgeInfo._hasBackFace && edgeInfo._hasFrontFace)
				{
					this.HandleSilhouetteEdge(edgeInfo._uv1, edgeInfo._uv2, edge._start, edge._end, visualTexCoordBounds, edgeList);
				}
			}
		}

		// Token: 0x06004C93 RID: 19603 RVA: 0x0012D05C File Offset: 0x0012C45C
		private void HandleSilhouetteEdge(Point uv1, Point uv2, Point3D p3D1, Point3D p3D2, Point[] bounds, List<HitTestEdge> edgeList)
		{
			List<Point3D> list = new List<Point3D>();
			List<Point> list2 = new List<Point>();
			Vector vector = uv2 - uv1;
			for (int i = 0; i < bounds.Length; i++)
			{
				Point point = bounds[i];
				Point point2 = bounds[(i + 1) % bounds.Length];
				if (Math.Max(point.X, point2.X) >= Math.Min(uv1.X, uv2.X) && Math.Min(point.X, point2.X) <= Math.Max(uv1.X, uv2.X) && Math.Max(point.Y, point2.Y) >= Math.Min(uv1.Y, uv2.Y) && Math.Min(point.Y, point2.Y) <= Math.Max(uv1.Y, uv2.Y))
				{
					bool flag = false;
					double num = this.IntersectRayLine(uv1, vector, point, point2, out flag);
					if (flag)
					{
						return;
					}
					if (num >= 0.0 && num <= 1.0)
					{
						Point point3 = uv1 + vector * num;
						Point3D item = p3D1 + (p3D2 - p3D1) * num;
						double length = (point - point2).Length;
						if ((point3 - point).Length < length && (point3 - point2).Length < length)
						{
							list.Add(item);
							list2.Add(point3);
						}
					}
				}
			}
			if (list.Count >= 2)
			{
				edgeList.Add(new HitTestEdge(list[0], list[1], list2[0], list2[1]));
				return;
			}
			if (list.Count == 1)
			{
				if (this.IsPointInPolygon(bounds, uv1))
				{
					edgeList.Add(new HitTestEdge(list[0], p3D1, list2[0], uv1));
				}
				if (this.IsPointInPolygon(bounds, uv2))
				{
					edgeList.Add(new HitTestEdge(list[0], p3D2, list2[0], uv2));
					return;
				}
			}
			else if (this.IsPointInPolygon(bounds, uv1) && this.IsPointInPolygon(bounds, uv2))
			{
				edgeList.Add(new HitTestEdge(p3D1, p3D2, uv1, uv2));
			}
		}

		// Token: 0x06004C94 RID: 19604 RVA: 0x0012D2B8 File Offset: 0x0012C6B8
		private bool IsPointInPolygon(Point[] polygon, Point p)
		{
			bool flag = false;
			for (int i = 0; i < polygon.Length; i++)
			{
				double num = Vector.CrossProduct(polygon[(i + 1) % polygon.Length] - polygon[i], polygon[i] - p);
				bool flag2 = num > 0.0;
				if (i == 0)
				{
					flag = flag2;
				}
				else if (flag != flag2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004C95 RID: 19605 RVA: 0x0012D31C File Offset: 0x0012C71C
		private bool FindClosestIntersection(Point mousePos, List<HitTestEdge> edges, out Point finalPoint)
		{
			bool result = false;
			double num = double.MaxValue;
			Point uv = default(Point);
			finalPoint = default(Point);
			int i = 0;
			int count = edges.Count;
			while (i < count)
			{
				Vector vector = mousePos - edges[i]._p1Transformed;
				Vector vector2 = edges[i]._p2Transformed - edges[i]._p1Transformed;
				double num2 = vector2 * vector2;
				Point point;
				double length;
				if (num2 == 0.0)
				{
					point = edges[i]._p1Transformed;
					length = vector.Length;
				}
				else
				{
					double num3 = vector2 * vector;
					if (num3 < 0.0)
					{
						point = edges[i]._p1Transformed;
					}
					else if (num3 > num2)
					{
						point = edges[i]._p2Transformed;
					}
					else
					{
						point = edges[i]._p1Transformed + num3 / num2 * vector2;
					}
					length = (mousePos - point).Length;
				}
				if (length < num)
				{
					num = length;
					if (num2 != 0.0)
					{
						uv = (point - edges[i]._p1Transformed).Length / Math.Sqrt(num2) * (edges[i]._uv2 - edges[i]._uv1) + edges[i]._uv1;
					}
					else
					{
						uv = edges[i]._uv1;
					}
				}
				i++;
			}
			if (num != 1.7976931348623157E+308)
			{
				Point point2 = Viewport2DVisual3D.TextureCoordsToVisualCoords(uv, this._visualBrushBounds);
				if (this._transform2DInverse != null)
				{
					Point point3 = this._transform2DInverse.Transform(point2);
					if (point3.X <= this._visualBounds.Left + 1.0)
					{
						point3.X -= 2.0;
					}
					if (point3.Y <= this._visualBounds.Top + 1.0)
					{
						point3.Y -= 2.0;
					}
					if (point3.X >= this._visualBounds.Right - 1.0)
					{
						point3.X += 2.0;
					}
					if (point3.Y >= this._visualBounds.Bottom - 1.0)
					{
						point3.Y += 2.0;
					}
					Point pt = this._transform2D.Transform(point3);
					finalPoint = Viewport2DVisual3D.VisualCoordsToTextureCoords(pt, this._visualBrushBounds);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06004C96 RID: 19606 RVA: 0x0012D5DC File Offset: 0x0012C9DC
		private bool TryRegularTransform(Point inPoint, out Point result)
		{
			Point point = Viewport2DVisual3D.VisualCoordsToTextureCoords(inPoint, this._visualBrushBounds);
			Point3D inPoint2;
			if (this._objectToViewport != null && Viewport2DVisual3D.Get3DPointFor2DCoordinate(point, out inPoint2, this._geometry.Positions, this._geometry.TextureCoordinates, this._geometry.TriangleIndices))
			{
				return this._objectToViewport.TryTransform(inPoint2, out result);
			}
			result = default(Point);
			return false;
		}

		// Token: 0x06004C97 RID: 19607 RVA: 0x0012D640 File Offset: 0x0012CA40
		public override Rect TransformBounds(Rect rect)
		{
			rect.Intersect(this._visualBrushBounds);
			List<HitTestEdge> list = this.GrabValidEdges(new Point[]
			{
				Viewport2DVisual3D.VisualCoordsToTextureCoords(rect.TopLeft, this._visualBrushBounds),
				Viewport2DVisual3D.VisualCoordsToTextureCoords(rect.TopRight, this._visualBrushBounds),
				Viewport2DVisual3D.VisualCoordsToTextureCoords(rect.BottomRight, this._visualBrushBounds),
				Viewport2DVisual3D.VisualCoordsToTextureCoords(rect.BottomLeft, this._visualBrushBounds)
			});
			Rect empty = Rect.Empty;
			if (list != null)
			{
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					empty.Union(list[i]._p1Transformed);
					empty.Union(list[i]._p2Transformed);
					i++;
				}
			}
			return empty;
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06004C98 RID: 19608 RVA: 0x0012D714 File Offset: 0x0012CB14
		public override GeneralTransform Inverse
		{
			get
			{
				GeneralTransform2DTo3DTo2D generalTransform2DTo3DTo2D = (GeneralTransform2DTo3DTo2D)base.Clone();
				generalTransform2DTo3DTo2D.IsInverse = !this.IsInverse;
				return generalTransform2DTo3DTo2D;
			}
		}

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06004C99 RID: 19609 RVA: 0x0012D740 File Offset: 0x0012CB40
		internal override Transform AffineTransform
		{
			[FriendAccessAllowed]
			get
			{
				return null;
			}
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06004C9A RID: 19610 RVA: 0x0012D750 File Offset: 0x0012CB50
		// (set) Token: 0x06004C9B RID: 19611 RVA: 0x0012D764 File Offset: 0x0012CB64
		internal bool IsInverse
		{
			get
			{
				return this._fInverse;
			}
			set
			{
				this._fInverse = value;
			}
		}

		// Token: 0x06004C9C RID: 19612 RVA: 0x0012D778 File Offset: 0x0012CB78
		protected override Freezable CreateInstanceCore()
		{
			return new GeneralTransform2DTo3DTo2D();
		}

		// Token: 0x06004C9D RID: 19613 RVA: 0x0012D78C File Offset: 0x0012CB8C
		protected override void CloneCore(Freezable sourceFreezable)
		{
			GeneralTransform2DTo3DTo2D transform = (GeneralTransform2DTo3DTo2D)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(transform);
		}

		// Token: 0x06004C9E RID: 19614 RVA: 0x0012D7B0 File Offset: 0x0012CBB0
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			GeneralTransform2DTo3DTo2D transform = (GeneralTransform2DTo3DTo2D)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(transform);
		}

		// Token: 0x06004C9F RID: 19615 RVA: 0x0012D7D4 File Offset: 0x0012CBD4
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			GeneralTransform2DTo3DTo2D transform = (GeneralTransform2DTo3DTo2D)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			this.CopyCommon(transform);
		}

		// Token: 0x06004CA0 RID: 19616 RVA: 0x0012D7F8 File Offset: 0x0012CBF8
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			GeneralTransform2DTo3DTo2D transform = (GeneralTransform2DTo3DTo2D)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			this.CopyCommon(transform);
		}

		// Token: 0x06004CA1 RID: 19617 RVA: 0x0012D81C File Offset: 0x0012CC1C
		private void CopyCommon(GeneralTransform2DTo3DTo2D transform)
		{
			this._fInverse = transform._fInverse;
			this._geometry = transform._geometry;
			this._visualBounds = transform._visualBounds;
			this._visualBrushBounds = transform._visualBrushBounds;
			this._transform2D = transform._transform2D;
			this._transform2DInverse = transform._transform2DInverse;
			this._camera = transform._camera;
			this._viewSize = transform._viewSize;
			this._boundingRect = transform._boundingRect;
			this._worldTransformation = transform._worldTransformation;
			this._objectToViewport = transform._objectToViewport;
			this._validEdgesCache = null;
		}

		// Token: 0x04002146 RID: 8518
		private bool _fInverse;

		// Token: 0x04002147 RID: 8519
		private MeshGeometry3D _geometry;

		// Token: 0x04002148 RID: 8520
		private Rect _visualBounds;

		// Token: 0x04002149 RID: 8521
		private Rect _visualBrushBounds;

		// Token: 0x0400214A RID: 8522
		private GeneralTransform _transform2D;

		// Token: 0x0400214B RID: 8523
		private GeneralTransform _transform2DInverse;

		// Token: 0x0400214C RID: 8524
		private Camera _camera;

		// Token: 0x0400214D RID: 8525
		private Size _viewSize;

		// Token: 0x0400214E RID: 8526
		private Rect3D _boundingRect;

		// Token: 0x0400214F RID: 8527
		private Matrix3D _worldTransformation;

		// Token: 0x04002150 RID: 8528
		private GeneralTransform3DTo2D _objectToViewport;

		// Token: 0x04002151 RID: 8529
		private List<HitTestEdge> _validEdgesCache;

		// Token: 0x04002152 RID: 8530
		private const double BUFFER_SIZE = 2.0;

		// Token: 0x020009C9 RID: 2505
		private struct Edge
		{
			// Token: 0x06005AFA RID: 23290 RVA: 0x0016CEE4 File Offset: 0x0016C2E4
			public Edge(Point3D s, Point3D e)
			{
				this._start = s;
				this._end = e;
			}

			// Token: 0x04002DF2 RID: 11762
			public Point3D _start;

			// Token: 0x04002DF3 RID: 11763
			public Point3D _end;
		}

		// Token: 0x020009CA RID: 2506
		private class EdgeInfo
		{
			// Token: 0x06005AFB RID: 23291 RVA: 0x0016CF00 File Offset: 0x0016C300
			public EdgeInfo()
			{
				this._hasFrontFace = (this._hasBackFace = false);
				this._numSharing = 0;
			}

			// Token: 0x04002DF4 RID: 11764
			public bool _hasFrontFace;

			// Token: 0x04002DF5 RID: 11765
			public bool _hasBackFace;

			// Token: 0x04002DF6 RID: 11766
			public Point _uv1;

			// Token: 0x04002DF7 RID: 11767
			public Point _uv2;

			// Token: 0x04002DF8 RID: 11768
			public int _numSharing;
		}

		// Token: 0x020009CB RID: 2507
		private enum PolygonSide
		{
			// Token: 0x04002DFA RID: 11770
			FRONT,
			// Token: 0x04002DFB RID: 11771
			BACK
		}
	}
}
