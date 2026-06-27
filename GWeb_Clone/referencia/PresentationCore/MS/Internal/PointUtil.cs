using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace MS.Internal
{
	// Token: 0x02000679 RID: 1657
	[FriendAccessAllowed]
	internal static class PointUtil
	{
		// Token: 0x0600491F RID: 18719 RVA: 0x0011D538 File Offset: 0x0011C938
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public static Point ClientToRoot(Point point, PresentationSource presentationSource)
		{
			bool flag = true;
			return PointUtil.TryClientToRoot(point, presentationSource, true, out flag);
		}

		// Token: 0x06004920 RID: 18720 RVA: 0x0011D554 File Offset: 0x0011C954
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public static Point TryClientToRoot(Point point, PresentationSource presentationSource, bool throwOnError, out bool success)
		{
			if (throwOnError || (presentationSource != null && presentationSource.CompositionTarget != null && !presentationSource.CompositionTarget.IsDisposed))
			{
				point = presentationSource.CompositionTarget.TransformFromDevice.Transform(point);
				point = PointUtil.TryApplyVisualTransform(point, presentationSource.RootVisual, true, throwOnError, out success);
				return point;
			}
			success = false;
			return new Point(0.0, 0.0);
		}

		// Token: 0x06004921 RID: 18721 RVA: 0x0011D5C4 File Offset: 0x0011C9C4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public static Point RootToClient(Point point, PresentationSource presentationSource)
		{
			point = PointUtil.ApplyVisualTransform(point, presentationSource.RootVisual, false);
			point = presentationSource.CompositionTarget.TransformToDevice.Transform(point);
			return point;
		}

		// Token: 0x06004922 RID: 18722 RVA: 0x0011D5F8 File Offset: 0x0011C9F8
		public static Point ApplyVisualTransform(Point point, Visual v, bool inverse)
		{
			bool flag = true;
			return PointUtil.TryApplyVisualTransform(point, v, inverse, true, out flag);
		}

		// Token: 0x06004923 RID: 18723 RVA: 0x0011D614 File Offset: 0x0011CA14
		public static Point TryApplyVisualTransform(Point point, Visual v, bool inverse, bool throwOnError, out bool success)
		{
			success = true;
			if (v != null)
			{
				Matrix visualTransform = PointUtil.GetVisualTransform(v);
				if (inverse)
				{
					if (!throwOnError && !visualTransform.HasInverse)
					{
						success = false;
						return new Point(0.0, 0.0);
					}
					visualTransform.Invert();
				}
				point = visualTransform.Transform(point);
			}
			return point;
		}

		// Token: 0x06004924 RID: 18724 RVA: 0x0011D670 File Offset: 0x0011CA70
		internal static Matrix GetVisualTransform(Visual v)
		{
			if (v != null)
			{
				Matrix matrix = Matrix.Identity;
				Transform transform = VisualTreeHelper.GetTransform(v);
				if (transform != null)
				{
					Matrix value = transform.Value;
					matrix = Matrix.Multiply(matrix, value);
				}
				Vector offset = VisualTreeHelper.GetOffset(v);
				matrix.Translate(offset.X, offset.Y);
				return matrix;
			}
			return Matrix.Identity;
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x0011D6C4 File Offset: 0x0011CAC4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public static Point ClientToScreen(Point pointClient, PresentationSource presentationSource)
		{
			HwndSource hwndSource = presentationSource as HwndSource;
			if (hwndSource == null)
			{
				return pointClient;
			}
			HandleRef handleRef = new HandleRef(hwndSource, hwndSource.CriticalHandle);
			NativeMethods.POINT pt = PointUtil.FromPoint(pointClient);
			NativeMethods.POINT pt2 = PointUtil.AdjustForRightToLeft(pt, handleRef);
			UnsafeNativeMethods.ClientToScreen(handleRef, pt2);
			return PointUtil.ToPoint(pt2);
		}

		// Token: 0x06004926 RID: 18726 RVA: 0x0011D708 File Offset: 0x0011CB08
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static Point ScreenToClient(Point pointScreen, PresentationSource presentationSource)
		{
			HwndSource hwndSource = presentationSource as HwndSource;
			if (hwndSource == null)
			{
				return pointScreen;
			}
			HandleRef handleRef = new HandleRef(hwndSource, hwndSource.CriticalHandle);
			NativeMethods.POINT pt = PointUtil.FromPoint(pointScreen);
			SafeNativeMethods.ScreenToClient(handleRef, pt);
			pt = PointUtil.AdjustForRightToLeft(pt, handleRef);
			return PointUtil.ToPoint(pt);
		}

		// Token: 0x06004927 RID: 18727 RVA: 0x0011D74C File Offset: 0x0011CB4C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static Rect ElementToRoot(Rect rectElement, Visual element, PresentationSource presentationSource)
		{
			GeneralTransform generalTransform = element.TransformToAncestor(presentationSource.RootVisual);
			return generalTransform.TransformBounds(rectElement);
		}

		// Token: 0x06004928 RID: 18728 RVA: 0x0011D770 File Offset: 0x0011CB70
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static Rect RootToClient(Rect rectRoot, PresentationSource presentationSource)
		{
			CompositionTarget compositionTarget = presentationSource.CompositionTarget;
			Matrix visualTransform = PointUtil.GetVisualTransform(compositionTarget.RootVisual);
			Rect rect = Rect.Transform(rectRoot, visualTransform);
			Matrix transformToDevice = compositionTarget.TransformToDevice;
			return Rect.Transform(rect, transformToDevice);
		}

		// Token: 0x06004929 RID: 18729 RVA: 0x0011D7AC File Offset: 0x0011CBAC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static Rect ClientToScreen(Rect rectClient, HwndSource hwndSource)
		{
			Point point = PointUtil.ClientToScreen(rectClient.TopLeft, hwndSource);
			Point point2 = PointUtil.ClientToScreen(rectClient.BottomRight, hwndSource);
			return new Rect(point, point2);
		}

		// Token: 0x0600492A RID: 18730 RVA: 0x0011D7DC File Offset: 0x0011CBDC
		internal static NativeMethods.POINT AdjustForRightToLeft(NativeMethods.POINT pt, HandleRef handleRef)
		{
			int windowStyle = SafeNativeMethods.GetWindowStyle(handleRef, true);
			if ((windowStyle & 4194304) == 4194304)
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				SafeNativeMethods.GetClientRect(handleRef, ref rect);
				pt.x = rect.right - pt.x;
			}
			return pt;
		}

		// Token: 0x0600492B RID: 18731 RVA: 0x0011D824 File Offset: 0x0011CC24
		internal static NativeMethods.RECT AdjustForRightToLeft(NativeMethods.RECT rc, HandleRef handleRef)
		{
			int windowStyle = SafeNativeMethods.GetWindowStyle(handleRef, true);
			if ((windowStyle & 4194304) == 4194304)
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				SafeNativeMethods.GetClientRect(handleRef, ref rect);
				int num = rc.right - rc.left;
				rc.right = rect.right - rc.left;
				rc.left = rc.right - num;
			}
			return rc;
		}

		// Token: 0x0600492C RID: 18732 RVA: 0x0011D88C File Offset: 0x0011CC8C
		internal static NativeMethods.POINT FromPoint(Point point)
		{
			return new NativeMethods.POINT(DoubleUtil.DoubleToInt(point.X), DoubleUtil.DoubleToInt(point.Y));
		}

		// Token: 0x0600492D RID: 18733 RVA: 0x0011D8B8 File Offset: 0x0011CCB8
		internal static Point ToPoint(NativeMethods.POINT pt)
		{
			return new Point((double)pt.x, (double)pt.y);
		}

		// Token: 0x0600492E RID: 18734 RVA: 0x0011D8D8 File Offset: 0x0011CCD8
		internal static NativeMethods.RECT FromRect(Rect rect)
		{
			return new NativeMethods.RECT
			{
				top = DoubleUtil.DoubleToInt(rect.Y),
				left = DoubleUtil.DoubleToInt(rect.X),
				bottom = DoubleUtil.DoubleToInt(rect.Bottom),
				right = DoubleUtil.DoubleToInt(rect.Right)
			};
		}

		// Token: 0x0600492F RID: 18735 RVA: 0x0011D93C File Offset: 0x0011CD3C
		internal static Rect ToRect(NativeMethods.RECT rc)
		{
			return new Rect
			{
				X = (double)rc.left,
				Y = (double)rc.top,
				Width = (double)(rc.right - rc.left),
				Height = (double)(rc.bottom - rc.top)
			};
		}
	}
}
