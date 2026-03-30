using System;
using System.Security;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using MS.Internal;

namespace System.Windows.Input
{
	// Token: 0x02000243 RID: 579
	internal static class InputElement
	{
		// Token: 0x06001013 RID: 4115 RVA: 0x0003CAF4 File Offset: 0x0003BEF4
		internal static bool IsValid(IInputElement e)
		{
			DependencyObject o = e as DependencyObject;
			return InputElement.IsValid(o);
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0003CB10 File Offset: 0x0003BF10
		internal static bool IsValid(DependencyObject o)
		{
			return InputElement.IsUIElement(o) || InputElement.IsContentElement(o) || InputElement.IsUIElement3D(o);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0003CB38 File Offset: 0x0003BF38
		internal static bool IsUIElement(DependencyObject o)
		{
			return InputElement.UIElementType.IsInstanceOfType(o);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0003CB50 File Offset: 0x0003BF50
		internal static bool IsUIElement3D(DependencyObject o)
		{
			return InputElement.UIElement3DType.IsInstanceOfType(o);
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0003CB68 File Offset: 0x0003BF68
		internal static bool IsContentElement(DependencyObject o)
		{
			return InputElement.ContentElementType.IsInstanceOfType(o);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0003CB80 File Offset: 0x0003BF80
		internal static DependencyObject GetContainingUIElement(DependencyObject o, bool onlyTraverse2D)
		{
			DependencyObject result = null;
			if (o != null)
			{
				Visual reference;
				Visual3D reference2;
				if (InputElement.IsUIElement(o))
				{
					result = o;
				}
				else if (InputElement.IsUIElement3D(o) && !onlyTraverse2D)
				{
					result = o;
				}
				else if (InputElement.IsContentElement(o))
				{
					DependencyObject dependencyObject = ContentOperations.GetParent((ContentElement)o);
					if (dependencyObject != null)
					{
						result = InputElement.GetContainingUIElement(dependencyObject, onlyTraverse2D);
					}
					else
					{
						dependencyObject = ((ContentElement)o).GetUIParentCore();
						if (dependencyObject != null)
						{
							result = InputElement.GetContainingUIElement(dependencyObject, onlyTraverse2D);
						}
					}
				}
				else if ((reference = (o as Visual)) != null)
				{
					DependencyObject parent = VisualTreeHelper.GetParent(reference);
					if (parent != null)
					{
						result = InputElement.GetContainingUIElement(parent, onlyTraverse2D);
					}
				}
				else if (!onlyTraverse2D && (reference2 = (o as Visual3D)) != null)
				{
					DependencyObject parent2 = VisualTreeHelper.GetParent(reference2);
					if (parent2 != null)
					{
						result = InputElement.GetContainingUIElement(parent2, onlyTraverse2D);
					}
				}
			}
			return result;
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0003CC34 File Offset: 0x0003C034
		internal static DependencyObject GetContainingUIElement(DependencyObject o)
		{
			return InputElement.GetContainingUIElement(o, false);
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0003CC48 File Offset: 0x0003C048
		internal static IInputElement GetContainingInputElement(DependencyObject o, bool onlyTraverse2D)
		{
			IInputElement result = null;
			if (o != null)
			{
				Visual reference;
				Visual3D reference2;
				if (InputElement.IsUIElement(o))
				{
					result = (UIElement)o;
				}
				else if (InputElement.IsContentElement(o))
				{
					result = (ContentElement)o;
				}
				else if (InputElement.IsUIElement3D(o) && !onlyTraverse2D)
				{
					result = (UIElement3D)o;
				}
				else if ((reference = (o as Visual)) != null)
				{
					DependencyObject parent = VisualTreeHelper.GetParent(reference);
					if (parent != null)
					{
						result = InputElement.GetContainingInputElement(parent, onlyTraverse2D);
					}
				}
				else if (!onlyTraverse2D && (reference2 = (o as Visual3D)) != null)
				{
					DependencyObject parent2 = VisualTreeHelper.GetParent(reference2);
					if (parent2 != null)
					{
						result = InputElement.GetContainingInputElement(parent2, onlyTraverse2D);
					}
				}
			}
			return result;
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0003CCD4 File Offset: 0x0003C0D4
		internal static IInputElement GetContainingInputElement(DependencyObject o)
		{
			return InputElement.GetContainingInputElement(o, false);
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0003CCE8 File Offset: 0x0003C0E8
		internal static DependencyObject GetContainingVisual(DependencyObject o)
		{
			DependencyObject dependencyObject = null;
			if (o != null)
			{
				if (InputElement.IsUIElement(o))
				{
					dependencyObject = (Visual)o;
				}
				else if (InputElement.IsUIElement3D(o))
				{
					dependencyObject = (Visual3D)o;
				}
				else if (InputElement.IsContentElement(o))
				{
					DependencyObject dependencyObject2 = ContentOperations.GetParent((ContentElement)o);
					if (dependencyObject2 != null)
					{
						dependencyObject = InputElement.GetContainingVisual(dependencyObject2);
					}
					else
					{
						dependencyObject2 = ((ContentElement)o).GetUIParentCore();
						if (dependencyObject2 != null)
						{
							dependencyObject = InputElement.GetContainingVisual(dependencyObject2);
						}
					}
				}
				else
				{
					dependencyObject = (o as Visual);
					if (dependencyObject == null)
					{
						dependencyObject = (o as Visual3D);
					}
				}
			}
			return dependencyObject;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0003CD68 File Offset: 0x0003C168
		internal static DependencyObject GetRootVisual(DependencyObject o)
		{
			return InputElement.GetRootVisual(o, true);
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0003CD7C File Offset: 0x0003C17C
		internal static DependencyObject GetRootVisual(DependencyObject o, bool enable2DTo3DTransition)
		{
			DependencyObject dependencyObject = InputElement.GetContainingVisual(o);
			DependencyObject parent;
			while (dependencyObject != null && (parent = VisualTreeHelper.GetParent(dependencyObject)) != null && (enable2DTo3DTransition || !(dependencyObject is Visual) || !(parent is Visual3D)))
			{
				dependencyObject = parent;
			}
			return dependencyObject;
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0003CDB8 File Offset: 0x0003C1B8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static Point TranslatePoint(Point pt, DependencyObject from, DependencyObject to)
		{
			bool flag = false;
			return InputElement.TranslatePoint(pt, from, to, out flag);
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0003CDD4 File Offset: 0x0003C1D4
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static Point TranslatePoint(Point pt, DependencyObject from, DependencyObject to, out bool translated)
		{
			translated = false;
			Point point = pt;
			DependencyObject containingVisual = InputElement.GetContainingVisual(from);
			Visual visual = InputElement.GetRootVisual(from) as Visual;
			Visual visual2 = containingVisual as Visual;
			if (containingVisual != null && visual2 == null)
			{
				visual2 = VisualTreeHelper.GetContainingVisual2D(containingVisual);
			}
			if (visual2 != null && visual != null)
			{
				GeneralTransform generalTransform;
				Matrix matrix;
				bool flag = visual2.TrySimpleTransformToAncestor(visual, false, out generalTransform, out matrix);
				if (flag)
				{
					point = matrix.Transform(point);
				}
				else if (!generalTransform.TryTransform(point, out point))
				{
					return default(Point);
				}
				if (to != null)
				{
					DependencyObject containingVisual2 = InputElement.GetContainingVisual(to);
					Visual visual3 = InputElement.GetRootVisual(to) as Visual;
					if (containingVisual2 == null || visual3 == null)
					{
						return default(Point);
					}
					if (visual != visual3)
					{
						HwndSource hwndSource = PresentationSource.CriticalFromVisual(visual) as HwndSource;
						HwndSource hwndSource2 = PresentationSource.CriticalFromVisual(visual3) as HwndSource;
						if (hwndSource == null || !(hwndSource.CriticalHandle != IntPtr.Zero) || hwndSource.CompositionTarget == null || hwndSource2 == null || !(hwndSource2.CriticalHandle != IntPtr.Zero) || hwndSource2.CompositionTarget == null)
						{
							return default(Point);
						}
						point = PointUtil.RootToClient(point, hwndSource);
						Point pointScreen = PointUtil.ClientToScreen(point, hwndSource);
						point = PointUtil.ScreenToClient(pointScreen, hwndSource2);
						point = PointUtil.ClientToRoot(point, hwndSource2);
					}
					Visual visual4 = containingVisual2 as Visual;
					if (visual4 == null)
					{
						visual4 = VisualTreeHelper.GetContainingVisual2D(containingVisual2);
					}
					GeneralTransform generalTransform2;
					Matrix matrix2;
					bool flag2 = visual4.TrySimpleTransformToAncestor(visual3, true, out generalTransform2, out matrix2);
					if (flag2)
					{
						point = matrix2.Transform(point);
					}
					else
					{
						if (generalTransform2 == null)
						{
							return default(Point);
						}
						if (!generalTransform2.TryTransform(point, out point))
						{
							return default(Point);
						}
					}
				}
				translated = true;
				return point;
			}
			return default(Point);
		}

		// Token: 0x040008B6 RID: 2230
		private static DependencyObjectType ContentElementType = DependencyObjectType.FromSystemTypeInternal(typeof(ContentElement));

		// Token: 0x040008B7 RID: 2231
		private static DependencyObjectType UIElementType = DependencyObjectType.FromSystemTypeInternal(typeof(UIElement));

		// Token: 0x040008B8 RID: 2232
		private static DependencyObjectType UIElement3DType = DependencyObjectType.FromSystemTypeInternal(typeof(UIElement3D));
	}
}
