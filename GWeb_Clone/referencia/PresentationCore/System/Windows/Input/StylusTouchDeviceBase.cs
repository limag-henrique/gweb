using System;
using System.Security;
using System.Windows.Media;

namespace System.Windows.Input
{
	// Token: 0x020002C6 RID: 710
	internal abstract class StylusTouchDeviceBase : TouchDevice
	{
		// Token: 0x06001519 RID: 5401 RVA: 0x0004E9F4 File Offset: 0x0004DDF4
		[SecuritySafeCritical]
		internal StylusTouchDeviceBase(StylusDeviceBase stylusDevice) : base(stylusDevice.Id)
		{
			this.StylusDevice = stylusDevice;
			StylusDeviceBase stylusDevice2 = this.StylusDevice;
			StylusPointDescription stylusPointDescription;
			if (stylusDevice2 == null)
			{
				stylusPointDescription = null;
			}
			else
			{
				TabletDevice tabletDevice = stylusDevice2.TabletDevice;
				if (tabletDevice == null)
				{
					stylusPointDescription = null;
				}
				else
				{
					TabletDeviceBase tabletDeviceImpl = tabletDevice.TabletDeviceImpl;
					stylusPointDescription = ((tabletDeviceImpl != null) ? tabletDeviceImpl.StylusPointDescription : null);
				}
			}
			this._stylusPointDescription = (stylusPointDescription ?? this._stylusPointDescription);
			this.PromotingToOther = true;
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0004EA98 File Offset: 0x0004DE98
		public override TouchPoint GetTouchPoint(IInputElement relativeTo)
		{
			Point position = this.StylusDevice.GetPosition(relativeTo);
			Rect bounds = this.GetBounds(this.StylusDevice.RawStylusPoint, position, relativeTo);
			return new TouchPoint(this, position, bounds, this._lastAction);
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0004EAD4 File Offset: 0x0004DED4
		private Rect GetBounds(StylusPoint stylusPoint, Point position, IInputElement relativeTo)
		{
			GeneralTransform elementToRoot;
			GeneralTransform rootToElement;
			this.GetRootTransforms(relativeTo, out elementToRoot, out rootToElement);
			return this.GetBounds(stylusPoint, position, relativeTo, elementToRoot, rootToElement);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0004EAF8 File Offset: 0x0004DEF8
		private Rect GetBounds(StylusPoint stylusPoint, Point position, IInputElement relativeTo, GeneralTransform elementToRoot, GeneralTransform rootToElement)
		{
			double stylusPointWidthOrHeight = this.GetStylusPointWidthOrHeight(stylusPoint, true);
			double stylusPointWidthOrHeight2 = this.GetStylusPointWidthOrHeight(stylusPoint, false);
			Point point;
			if (elementToRoot == null || !elementToRoot.TryTransform(position, out point))
			{
				point = position;
			}
			Rect rect = new Rect(point.X - stylusPointWidthOrHeight * 0.5, point.Y - stylusPointWidthOrHeight2 * 0.5, stylusPointWidthOrHeight, stylusPointWidthOrHeight2);
			if (rootToElement != null)
			{
				rect = rootToElement.TransformBounds(rect);
			}
			return rect;
		}

		// Token: 0x0600151D RID: 5405
		protected abstract double GetStylusPointWidthOrHeight(StylusPoint stylusPoint, bool isWidth);

		// Token: 0x0600151E RID: 5406 RVA: 0x0004EB68 File Offset: 0x0004DF68
		public override TouchPointCollection GetIntermediateTouchPoints(IInputElement relativeTo)
		{
			StylusPointCollection stylusPoints = this.StylusDevice.GetStylusPoints(relativeTo, this._stylusPointDescription);
			int count = stylusPoints.Count;
			TouchPointCollection touchPointCollection = new TouchPointCollection();
			GeneralTransform elementToRoot;
			GeneralTransform rootToElement;
			this.GetRootTransforms(relativeTo, out elementToRoot, out rootToElement);
			for (int i = 0; i < count; i++)
			{
				StylusPoint stylusPoint = stylusPoints[i];
				Point position = new Point(stylusPoint.X, stylusPoint.Y);
				Rect bounds = this.GetBounds(stylusPoint, position, relativeTo, elementToRoot, rootToElement);
				TouchPoint item = new TouchPoint(this, position, bounds, this._lastAction);
				touchPointCollection.Add(item);
			}
			return touchPointCollection;
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0004EBF8 File Offset: 0x0004DFF8
		[SecuritySafeCritical]
		private void GetRootTransforms(IInputElement relativeTo, out GeneralTransform elementToRoot, out GeneralTransform rootToElement)
		{
			GeneralTransform generalTransform;
			rootToElement = (generalTransform = null);
			elementToRoot = generalTransform;
			DependencyObject containingVisual = InputElement.GetContainingVisual(relativeTo as DependencyObject);
			if (containingVisual != null)
			{
				PresentationSource presentationSource = PresentationSource.CriticalFromVisual(containingVisual);
				Visual visual = (presentationSource != null) ? presentationSource.RootVisual : null;
				Visual containingVisual2D = VisualTreeHelper.GetContainingVisual2D(containingVisual);
				if (visual != null && containingVisual2D != null)
				{
					elementToRoot = containingVisual2D.TransformToAncestor(visual);
					rootToElement = visual.TransformToDescendant(containingVisual2D);
				}
			}
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x0004EC54 File Offset: 0x0004E054
		[SecurityCritical]
		internal void ChangeActiveSource(PresentationSource activeSource)
		{
			base.SetActiveSource(activeSource);
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0004EC68 File Offset: 0x0004E068
		[SecurityCritical]
		internal void OnActivate()
		{
			base.Activate();
			StylusTouchDeviceBase._activeDeviceCount++;
			if (StylusTouchDeviceBase._activeDeviceCount == 1)
			{
				this.IsPrimary = true;
				this.OnActivateImpl();
			}
			this.PromotingToOther = true;
		}

		// Token: 0x06001522 RID: 5410
		protected abstract void OnActivateImpl();

		// Token: 0x06001523 RID: 5411 RVA: 0x0004ECA4 File Offset: 0x0004E0A4
		[SecurityCritical]
		internal void OnDeactivate()
		{
			base.Deactivate();
			this.PromotingToOther = true;
			this.DownHandled = false;
			StylusTouchDeviceBase._activeDeviceCount--;
			this.OnDeactivateImpl();
			this.IsPrimary = false;
		}

		// Token: 0x06001524 RID: 5412
		protected abstract void OnDeactivateImpl();

		// Token: 0x06001525 RID: 5413 RVA: 0x0004ECE0 File Offset: 0x0004E0E0
		internal bool OnDown()
		{
			this._lastAction = TouchAction.Down;
			this.DownHandled = base.ReportDown();
			return this.DownHandled;
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0004ED08 File Offset: 0x0004E108
		internal bool OnMove()
		{
			this._lastAction = TouchAction.Move;
			return base.ReportMove();
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0004ED24 File Offset: 0x0004E124
		internal bool OnUp()
		{
			this._lastAction = TouchAction.Up;
			return base.ReportUp();
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x0004ED40 File Offset: 0x0004E140
		// (set) Token: 0x06001529 RID: 5417 RVA: 0x0004ED54 File Offset: 0x0004E154
		public bool PromotingToOther { get; protected set; }

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x0004ED68 File Offset: 0x0004E168
		// (set) Token: 0x0600152B RID: 5419 RVA: 0x0004ED7C File Offset: 0x0004E17C
		internal bool DownHandled { get; private set; }

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x0004ED90 File Offset: 0x0004E190
		// (set) Token: 0x0600152D RID: 5421 RVA: 0x0004EDA4 File Offset: 0x0004E1A4
		internal StylusDeviceBase StylusDevice { get; private set; }

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x0004EDB8 File Offset: 0x0004E1B8
		// (set) Token: 0x0600152F RID: 5423 RVA: 0x0004EDCC File Offset: 0x0004E1CC
		internal bool IsPrimary { get; private set; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x0004EDE0 File Offset: 0x0004E1E0
		internal static int ActiveDeviceCount
		{
			get
			{
				return StylusTouchDeviceBase._activeDeviceCount;
			}
		}

		// Token: 0x04000B82 RID: 2946
		[ThreadStatic]
		private static int _activeDeviceCount;

		// Token: 0x04000B83 RID: 2947
		private TouchAction _lastAction = TouchAction.Move;

		// Token: 0x04000B84 RID: 2948
		private StylusPointDescription _stylusPointDescription = new StylusPointDescription(new StylusPointPropertyInfo[]
		{
			StylusPointPropertyInfoDefaults.X,
			StylusPointPropertyInfoDefaults.Y,
			StylusPointPropertyInfoDefaults.NormalPressure,
			StylusPointPropertyInfoDefaults.Width,
			StylusPointPropertyInfoDefaults.Height
		});

		// Token: 0x04000B85 RID: 2949
		internal const double CentimetersPerInch = 2.54;
	}
}
