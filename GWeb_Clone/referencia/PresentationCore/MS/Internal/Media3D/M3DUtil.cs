using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using MS.Utility;

namespace MS.Internal.Media3D
{
	// Token: 0x020006F5 RID: 1781
	internal static class M3DUtil
	{
		// Token: 0x06004CB7 RID: 19639 RVA: 0x0012E31C File Offset: 0x0012D71C
		internal static Point3D Interpolate(ref Point3D v0, ref Point3D v1, ref Point3D v2, ref Point barycentric)
		{
			double x = barycentric.X;
			double y = barycentric.Y;
			double num = 1.0 - x - y;
			return new Point3D(num * v0.X + x * v1.X + y * v2.X, num * v0.Y + x * v1.Y + y * v2.Y, num * v0.Z + x * v1.Z + y * v2.Z);
		}

		// Token: 0x06004CB8 RID: 19640 RVA: 0x0012E398 File Offset: 0x0012D798
		private static void AddPointToBounds(ref Point3D point, ref Rect3D bounds)
		{
			if (point.X < bounds.X)
			{
				bounds.SizeX += bounds.X - point.X;
				bounds.X = point.X;
			}
			else if (point.X > bounds.X + bounds.SizeX)
			{
				bounds.SizeX = point.X - bounds.X;
			}
			if (point.Y < bounds.Y)
			{
				bounds.SizeY += bounds.Y - point.Y;
				bounds.Y = point.Y;
			}
			else if (point.Y > bounds.Y + bounds.SizeY)
			{
				bounds.SizeY = point.Y - bounds.Y;
			}
			if (point.Z < bounds.Z)
			{
				bounds.SizeZ += bounds.Z - point.Z;
				bounds.Z = point.Z;
				return;
			}
			if (point.Z > bounds.Z + bounds.SizeZ)
			{
				bounds.SizeZ = point.Z - bounds.Z;
			}
		}

		// Token: 0x06004CB9 RID: 19641 RVA: 0x0012E4C0 File Offset: 0x0012D8C0
		internal static Rect3D ComputeAxisAlignedBoundingBox(Point3DCollection positions)
		{
			if (positions != null)
			{
				FrugalStructList<Point3D> collection = positions._collection;
				if (collection.Count != 0)
				{
					Point3D point3D = collection[0];
					Rect3D result = new Rect3D(point3D.X, point3D.Y, point3D.Z, 0.0, 0.0, 0.0);
					for (int i = 1; i < collection.Count; i++)
					{
						point3D = collection[i];
						M3DUtil.AddPointToBounds(ref point3D, ref result);
					}
					return result;
				}
			}
			return Rect3D.Empty;
		}

		// Token: 0x06004CBA RID: 19642 RVA: 0x0012E550 File Offset: 0x0012D950
		internal static Rect3D ComputeTransformedAxisAlignedBoundingBox(ref Rect3D originalBox, Transform3D transform)
		{
			if (transform == null || transform == Transform3D.Identity)
			{
				return originalBox;
			}
			Matrix3D value = transform.Value;
			return M3DUtil.ComputeTransformedAxisAlignedBoundingBox(ref originalBox, ref value);
		}

		// Token: 0x06004CBB RID: 19643 RVA: 0x0012E580 File Offset: 0x0012D980
		internal static Rect3D ComputeTransformedAxisAlignedBoundingBox(ref Rect3D originalBox, ref Matrix3D matrix)
		{
			if (originalBox.IsEmpty)
			{
				return originalBox;
			}
			if (matrix.IsAffine)
			{
				return M3DUtil.ComputeTransformedAxisAlignedBoundingBoxAffine(ref originalBox, ref matrix);
			}
			return M3DUtil.ComputeTransformedAxisAlignedBoundingBoxNonAffine(ref originalBox, ref matrix);
		}

		// Token: 0x06004CBC RID: 19644 RVA: 0x0012E5B4 File Offset: 0x0012D9B4
		internal static Rect3D ComputeTransformedAxisAlignedBoundingBoxAffine(ref Rect3D originalBox, ref Matrix3D matrix)
		{
			double num = originalBox.X + originalBox.SizeX;
			double num2 = originalBox.Y + originalBox.SizeY;
			double num3 = originalBox.Z + originalBox.SizeZ;
			double num4 = matrix.OffsetX;
			double num5 = matrix.OffsetX;
			double num6 = matrix.M11 * originalBox.X;
			double num7 = matrix.M11 * num;
			if (num7 > num6)
			{
				num4 += num6;
				num5 += num7;
			}
			else
			{
				num4 += num7;
				num5 += num6;
			}
			num6 = matrix.M21 * originalBox.Y;
			num7 = matrix.M21 * num2;
			if (num7 > num6)
			{
				num4 += num6;
				num5 += num7;
			}
			else
			{
				num4 += num7;
				num5 += num6;
			}
			num6 = matrix.M31 * originalBox.Z;
			num7 = matrix.M31 * num3;
			if (num7 > num6)
			{
				num4 += num6;
				num5 += num7;
			}
			else
			{
				num4 += num7;
				num5 += num6;
			}
			double num8 = matrix.OffsetY;
			double num9 = matrix.OffsetY;
			double num10 = matrix.M12 * originalBox.X;
			double num11 = matrix.M12 * num;
			if (num11 > num10)
			{
				num8 += num10;
				num9 += num11;
			}
			else
			{
				num8 += num11;
				num9 += num10;
			}
			num10 = matrix.M22 * originalBox.Y;
			num11 = matrix.M22 * num2;
			if (num11 > num10)
			{
				num8 += num10;
				num9 += num11;
			}
			else
			{
				num8 += num11;
				num9 += num10;
			}
			num10 = matrix.M32 * originalBox.Z;
			num11 = matrix.M32 * num3;
			if (num11 > num10)
			{
				num8 += num10;
				num9 += num11;
			}
			else
			{
				num8 += num11;
				num9 += num10;
			}
			double num12 = matrix.OffsetZ;
			double num13 = matrix.OffsetZ;
			double num14 = matrix.M13 * originalBox.X;
			double num15 = matrix.M13 * num;
			if (num15 > num14)
			{
				num12 += num14;
				num13 += num15;
			}
			else
			{
				num12 += num15;
				num13 += num14;
			}
			num14 = matrix.M23 * originalBox.Y;
			num15 = matrix.M23 * num2;
			if (num15 > num14)
			{
				num12 += num14;
				num13 += num15;
			}
			else
			{
				num12 += num15;
				num13 += num14;
			}
			num14 = matrix.M33 * originalBox.Z;
			num15 = matrix.M33 * num3;
			if (num15 > num14)
			{
				num12 += num14;
				num13 += num15;
			}
			else
			{
				num12 += num15;
				num13 += num14;
			}
			return new Rect3D(num4, num8, num12, num5 - num4, num9 - num8, num13 - num12);
		}

		// Token: 0x06004CBD RID: 19645 RVA: 0x0012E82C File Offset: 0x0012DC2C
		internal static Rect3D ComputeTransformedAxisAlignedBoundingBoxNonAffine(ref Rect3D originalBox, ref Matrix3D matrix)
		{
			double x = originalBox.X;
			double y = originalBox.Y;
			double z = originalBox.Z;
			double x2 = originalBox.X + originalBox.SizeX;
			double y2 = originalBox.Y + originalBox.SizeY;
			double z2 = originalBox.Z + originalBox.SizeZ;
			Point3D[] array = new Point3D[]
			{
				new Point3D(x, y, z),
				new Point3D(x, y, z2),
				new Point3D(x, y2, z),
				new Point3D(x, y2, z2),
				new Point3D(x2, y, z),
				new Point3D(x2, y, z2),
				new Point3D(x2, y2, z),
				new Point3D(x2, y2, z2)
			};
			matrix.Transform(array);
			Point3D point3D = array[0];
			Rect3D result = new Rect3D(point3D.X, point3D.Y, point3D.Z, 0.0, 0.0, 0.0);
			for (int i = 1; i < array.Length; i++)
			{
				point3D = array[i];
				M3DUtil.AddPointToBounds(ref point3D, ref result);
			}
			return result;
		}

		// Token: 0x06004CBE RID: 19646 RVA: 0x0012E974 File Offset: 0x0012DD74
		internal static double GetAspectRatio(Size viewSize)
		{
			return viewSize.Width / viewSize.Height;
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x0012E990 File Offset: 0x0012DD90
		internal static Point GetNormalizedPoint(Point point, Size size)
		{
			return new Point(2.0 * point.X / size.Width - 1.0, -(2.0 * point.Y / size.Height - 1.0));
		}

		// Token: 0x06004CC0 RID: 19648 RVA: 0x0012E9EC File Offset: 0x0012DDEC
		internal static double RadiansToDegrees(double radians)
		{
			return radians * 57.295779513082323;
		}

		// Token: 0x06004CC1 RID: 19649 RVA: 0x0012EA04 File Offset: 0x0012DE04
		internal static double DegreesToRadians(double degrees)
		{
			return degrees * 0.017453292519943295;
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x0012EA1C File Offset: 0x0012DE1C
		internal static Matrix3D GetWorldToViewportTransform3D(Camera camera, Rect viewport)
		{
			return camera.GetViewMatrix() * camera.GetProjectionMatrix(M3DUtil.GetAspectRatio(viewport.Size)) * M3DUtil.GetHomogeneousToViewportTransform3D(viewport);
		}

		// Token: 0x06004CC3 RID: 19651 RVA: 0x0012EA54 File Offset: 0x0012DE54
		internal static Matrix3D GetHomogeneousToViewportTransform3D(Rect viewport)
		{
			double num = viewport.Width / 2.0;
			double num2 = viewport.Height / 2.0;
			double offsetX = viewport.X + num;
			double offsetY = viewport.Y + num2;
			return new Matrix3D(num, 0.0, 0.0, 0.0, 0.0, -num2, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, offsetX, offsetY, 0.0, 1.0);
		}

		// Token: 0x06004CC4 RID: 19652 RVA: 0x0012EB10 File Offset: 0x0012DF10
		internal static Matrix GetHomogeneousToViewportTransform(Rect viewport)
		{
			double num = viewport.Width / 2.0;
			double num2 = viewport.Height / 2.0;
			double offsetX = viewport.X + num;
			double offsetY = viewport.Y + num2;
			return new Matrix(num, 0.0, 0.0, -num2, offsetX, offsetY);
		}

		// Token: 0x06004CC5 RID: 19653 RVA: 0x0012EB74 File Offset: 0x0012DF74
		internal static Matrix3D GetWorldTransformationMatrix(Visual3D visual)
		{
			Viewport3DVisual viewport3DVisual;
			return M3DUtil.GetWorldTransformationMatrix(visual, out viewport3DVisual);
		}

		// Token: 0x06004CC6 RID: 19654 RVA: 0x0012EB8C File Offset: 0x0012DF8C
		internal static Matrix3D GetWorldTransformationMatrix(Visual3D visual3DStart, out Viewport3DVisual viewport)
		{
			DependencyObject dependencyObject = visual3DStart;
			Matrix3D identity = Matrix3D.Identity;
			while (dependencyObject != null)
			{
				Visual3D visual3D = dependencyObject as Visual3D;
				if (visual3D == null)
				{
					break;
				}
				Transform3D transform3D = (Transform3D)visual3D.GetValue(Visual3D.TransformProperty);
				if (transform3D != null)
				{
					transform3D.Append(ref identity);
				}
				dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
			}
			if (dependencyObject != null)
			{
				viewport = (Viewport3DVisual)dependencyObject;
			}
			else
			{
				viewport = null;
			}
			return identity;
		}

		// Token: 0x06004CC7 RID: 19655 RVA: 0x0012EBE4 File Offset: 0x0012DFE4
		internal static bool TryTransformToViewport3DVisual(Visual3D visual3D, out Viewport3DVisual viewport, out Matrix3D matrix)
		{
			matrix = M3DUtil.GetWorldTransformationMatrix(visual3D, out viewport);
			if (viewport != null)
			{
				matrix *= M3DUtil.GetWorldToViewportTransform3D(viewport.Camera, viewport.Viewport);
				return true;
			}
			return false;
		}

		// Token: 0x06004CC8 RID: 19656 RVA: 0x0012EC2C File Offset: 0x0012E02C
		internal static bool IsPointInTriangle(Point p, Point[] triUVVertices, Point3D[] tri3DVertices, out Point3D inters3DPoint)
		{
			inters3DPoint = default(Point3D);
			double num = triUVVertices[0].X - triUVVertices[2].X;
			double num2 = triUVVertices[1].X - triUVVertices[2].X;
			double num3 = triUVVertices[2].X - p.X;
			double num4 = triUVVertices[0].Y - triUVVertices[2].Y;
			double num5 = triUVVertices[1].Y - triUVVertices[2].Y;
			double num6 = triUVVertices[2].Y - p.Y;
			double num7 = num * num5 - num2 * num4;
			if (num7 == 0.0)
			{
				return false;
			}
			double num8 = (num2 * num6 - num3 * num5) / num7;
			num7 = num2 * num4 - num * num5;
			if (num7 == 0.0)
			{
				return false;
			}
			double num9 = (num * num6 - num3 * num4) / num7;
			if (num8 < 0.0 || num8 > 1.0 || num9 < 0.0 || num9 > 1.0 || num8 + num9 > 1.0)
			{
				return false;
			}
			inters3DPoint = (Point3D)(num8 * (Vector3D)tri3DVertices[0] + num9 * (Vector3D)tri3DVertices[1] + (1.0 - num8 - num9) * (Vector3D)tri3DVertices[2]);
			return true;
		}
	}
}
