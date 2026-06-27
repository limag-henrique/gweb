using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using MS.Internal.PresentationCore;

namespace MS.Internal.Media
{
	// Token: 0x020006F7 RID: 1783
	internal static class VisualTreeUtils
	{
		// Token: 0x06004CCC RID: 19660 RVA: 0x0012EE68 File Offset: 0x0012E268
		internal static void PropagateFlags(DependencyObject element, VisualFlags flags, VisualProxyFlags proxyFlags)
		{
			Visual visual;
			Visual3D e;
			VisualTreeUtils.AsVisualInternal(element, out visual, out e);
			if (visual != null)
			{
				Visual.PropagateFlags(visual, flags, proxyFlags);
				return;
			}
			Visual3D.PropagateFlags(e, flags, proxyFlags);
		}

		// Token: 0x06004CCD RID: 19661 RVA: 0x0012EE94 File Offset: 0x0012E294
		internal static void SetFlagsToRoot(DependencyObject element, bool value, VisualFlags flags)
		{
			Visual visual;
			Visual3D visual3D;
			VisualTreeUtils.AsVisualInternal(element, out visual, out visual3D);
			if (visual != null)
			{
				visual.SetFlagsToRoot(value, flags);
				return;
			}
			if (visual3D != null)
			{
				visual3D.SetFlagsToRoot(value, flags);
			}
		}

		// Token: 0x06004CCE RID: 19662 RVA: 0x0012EEC4 File Offset: 0x0012E2C4
		internal static DependencyObject FindFirstAncestorWithFlagsAnd(DependencyObject element, VisualFlags flags)
		{
			Visual visual;
			Visual3D visual3D;
			VisualTreeUtils.AsVisualInternal(element, out visual, out visual3D);
			if (visual != null)
			{
				return visual.FindFirstAncestorWithFlagsAnd(flags);
			}
			if (visual3D != null)
			{
				return visual3D.FindFirstAncestorWithFlagsAnd(flags);
			}
			return null;
		}

		// Token: 0x06004CCF RID: 19663 RVA: 0x0012EEF4 File Offset: 0x0012E2F4
		internal static PointHitTestResult AsNearestPointHitTestResult(HitTestResult result)
		{
			if (result == null)
			{
				return null;
			}
			PointHitTestResult pointHitTestResult = result as PointHitTestResult;
			if (pointHitTestResult != null)
			{
				return pointHitTestResult;
			}
			RayHitTestResult rayHitTestResult = result as RayHitTestResult;
			if (rayHitTestResult == null)
			{
				return null;
			}
			Visual3D visual3D = rayHitTestResult.VisualHit;
			Matrix3D identity = Matrix3D.Identity;
			for (;;)
			{
				if (visual3D.Transform != null)
				{
					identity.Append(visual3D.Transform.Value);
				}
				Visual3D visual3D2 = visual3D.InternalVisualParent as Visual3D;
				if (visual3D2 == null)
				{
					break;
				}
				visual3D = visual3D2;
			}
			Viewport3DVisual viewport3DVisual = visual3D.InternalVisualParent as Viewport3DVisual;
			if (viewport3DVisual != null)
			{
				Point4D point = (Point4D)rayHitTestResult.PointHit * identity;
				Point pointHit = viewport3DVisual.WorldToViewport(point);
				return new PointHitTestResult(viewport3DVisual, pointHit);
			}
			return null;
		}

		// Token: 0x06004CD0 RID: 19664 RVA: 0x0012EF94 File Offset: 0x0012E394
		internal static void EnsureNonNullVisual(DependencyObject element)
		{
			VisualTreeUtils.EnsureVisual(element, false);
		}

		// Token: 0x06004CD1 RID: 19665 RVA: 0x0012EFA8 File Offset: 0x0012E3A8
		internal static void EnsureVisual(DependencyObject element)
		{
			VisualTreeUtils.EnsureVisual(element, true);
		}

		// Token: 0x06004CD2 RID: 19666 RVA: 0x0012EFBC File Offset: 0x0012E3BC
		private static void EnsureVisual(DependencyObject element, bool allowNull)
		{
			if (element == null)
			{
				if (!allowNull)
				{
					throw new ArgumentNullException("element");
				}
				return;
			}
			else
			{
				if (!(element is Visual) && !(element is Visual3D))
				{
					throw new ArgumentException(SR.Get("Visual_NotAVisual"));
				}
				element.VerifyAccess();
				return;
			}
		}

		// Token: 0x06004CD3 RID: 19667 RVA: 0x0012F004 File Offset: 0x0012E404
		internal static void AsNonNullVisual(DependencyObject element, out Visual visual, out Visual3D visual3D)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			VisualTreeUtils.AsVisual(element, out visual, out visual3D);
		}

		// Token: 0x06004CD4 RID: 19668 RVA: 0x0012F028 File Offset: 0x0012E428
		internal static void AsVisual(DependencyObject element, out Visual visual, out Visual3D visual3D)
		{
			bool flag = VisualTreeUtils.AsVisualHelper(element, out visual, out visual3D);
			if (element != null)
			{
				if (!flag)
				{
					throw new InvalidOperationException(SR.Get("Visual_NotAVisual", new object[]
					{
						element.GetType()
					}));
				}
				element.VerifyAccess();
			}
		}

		// Token: 0x06004CD5 RID: 19669 RVA: 0x0012F06C File Offset: 0x0012E46C
		internal static bool AsVisualInternal(DependencyObject element, out Visual visual, out Visual3D visual3D)
		{
			bool flag = VisualTreeUtils.AsVisualHelper(element, out visual, out visual3D);
			if (!flag)
			{
			}
			return flag;
		}

		// Token: 0x06004CD6 RID: 19670 RVA: 0x0012F088 File Offset: 0x0012E488
		private static bool AsVisualHelper(DependencyObject element, out Visual visual, out Visual3D visual3D)
		{
			Visual visual2 = element as Visual;
			if (visual2 != null)
			{
				visual = visual2;
				visual3D = null;
				return true;
			}
			Visual3D visual3D2 = element as Visual3D;
			if (visual3D2 != null)
			{
				visual = null;
				visual3D = visual3D2;
				return true;
			}
			visual = null;
			visual3D = null;
			return false;
		}

		// Token: 0x0400215C RID: 8540
		public const string BitmapEffectObsoleteMessage = "BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.";
	}
}
