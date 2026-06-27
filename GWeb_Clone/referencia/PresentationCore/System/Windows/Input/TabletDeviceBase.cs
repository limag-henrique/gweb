using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Security;
using System.Windows.Media;
using System.Windows.Threading;

namespace System.Windows.Input
{
	// Token: 0x020002CA RID: 714
	internal abstract class TabletDeviceBase : DispatcherObject, IDisposable
	{
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x0004F750 File Offset: 0x0004EB50
		// (set) Token: 0x0600156B RID: 5483 RVA: 0x0004F764 File Offset: 0x0004EB64
		internal TabletDevice TabletDevice { get; private set; }

		// Token: 0x0600156C RID: 5484 RVA: 0x0004F778 File Offset: 0x0004EB78
		internal T As<T>() where T : TabletDeviceBase
		{
			return this as T;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0004F790 File Offset: 0x0004EB90
		protected TabletDeviceBase(TabletDeviceInfo info)
		{
			this.TabletDevice = new TabletDevice(this);
			this._tabletInfo = info;
			if (this._tabletInfo.DeviceType == TabletDeviceType.Touch)
			{
				this._multiTouchSystemGestureLogic = new MultiTouchSystemGestureLogic();
			}
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0004F7DC File Offset: 0x0004EBDC
		[SecurityCritical]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0004F7F8 File Offset: 0x0004EBF8
		[SecurityCritical]
		protected virtual void Dispose(bool disposing)
		{
			this._disposed = true;
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0004F80C File Offset: 0x0004EC0C
		[SecurityCritical]
		~TabletDeviceBase()
		{
			this.Dispose(false);
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001571 RID: 5489
		internal abstract IInputElement Target { get; }

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001572 RID: 5490
		internal abstract PresentationSource ActiveSource { get; }

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x0004F848 File Offset: 0x0004EC48
		internal int Id
		{
			get
			{
				base.VerifyAccess();
				return this._tabletInfo.Id;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x0004F868 File Offset: 0x0004EC68
		internal string Name
		{
			get
			{
				base.VerifyAccess();
				return this._tabletInfo.Name;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x0004F888 File Offset: 0x0004EC88
		internal string ProductId
		{
			get
			{
				base.VerifyAccess();
				return this._tabletInfo.PlugAndPlayId;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x0004F8A8 File Offset: 0x0004ECA8
		internal TabletHardwareCapabilities TabletHardwareCapabilities
		{
			get
			{
				base.VerifyAccess();
				return this._tabletInfo.HardwareCapabilities;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x0004F8C8 File Offset: 0x0004ECC8
		internal ReadOnlyCollection<StylusPointProperty> SupportedStylusPointProperties
		{
			get
			{
				base.VerifyAccess();
				return this._tabletInfo.StylusPointProperties;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x0004F8E8 File Offset: 0x0004ECE8
		internal TabletDeviceType Type
		{
			get
			{
				base.VerifyAccess();
				return this._tabletInfo.DeviceType;
			}
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0004F908 File Offset: 0x0004ED08
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}({1})", new object[]
			{
				base.ToString(),
				this.Name
			});
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x0600157A RID: 5498
		internal abstract StylusDeviceCollection StylusDevices { get; }

		// Token: 0x0600157B RID: 5499 RVA: 0x0004F93C File Offset: 0x0004ED3C
		[SecurityCritical]
		internal SystemGesture? GenerateStaticGesture(RawStylusInputReport stylusInputReport)
		{
			MultiTouchSystemGestureLogic multiTouchSystemGestureLogic = this._multiTouchSystemGestureLogic;
			if (multiTouchSystemGestureLogic == null)
			{
				return null;
			}
			return multiTouchSystemGestureLogic.GenerateStaticGesture(stylusInputReport);
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x0004F964 File Offset: 0x0004ED64
		internal Matrix TabletToScreen
		{
			get
			{
				return new Matrix(this._tabletInfo.SizeInfo.ScreenSize.Width / this._tabletInfo.SizeInfo.TabletSize.Width, 0.0, 0.0, this._tabletInfo.SizeInfo.ScreenSize.Height / this._tabletInfo.SizeInfo.TabletSize.Height, 0.0, 0.0);
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x0004F9F0 File Offset: 0x0004EDF0
		internal Size TabletSize
		{
			get
			{
				return this._tabletInfo.SizeInfo.TabletSize;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x0004FA10 File Offset: 0x0004EE10
		internal Size ScreenSize
		{
			get
			{
				return this._tabletInfo.SizeInfo.ScreenSize;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600157F RID: 5503
		internal abstract Size DoubleTapSize { get; }

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x0004FA30 File Offset: 0x0004EE30
		internal StylusPointDescription StylusPointDescription
		{
			get
			{
				if (this._stylusPointDescription == null)
				{
					ReadOnlyCollection<StylusPointProperty> supportedStylusPointProperties = this.SupportedStylusPointProperties;
					List<StylusPointPropertyInfo> list = new List<StylusPointPropertyInfo>();
					foreach (StylusPointProperty stylusPointProperty in supportedStylusPointProperties)
					{
						list.Add((stylusPointProperty is StylusPointPropertyInfo) ? ((StylusPointPropertyInfo)stylusPointProperty) : new StylusPointPropertyInfo(stylusPointProperty));
					}
					this._stylusPointDescription = new StylusPointDescription(list, this._tabletInfo.PressureIndex);
				}
				return this._stylusPointDescription;
			}
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0004FACC File Offset: 0x0004EECC
		protected static uint GetPropertyValue(StylusPointPropertyInfo propertyInfo)
		{
			uint result = 1000U;
			StylusPointPropertyUnit unit = propertyInfo.Unit;
			if (unit != StylusPointPropertyUnit.Inches)
			{
				if (unit == StylusPointPropertyUnit.Centimeters)
				{
					if (propertyInfo.Resolution != 0f)
					{
						result = (uint)((float)((propertyInfo.Maximum - propertyInfo.Minimum) * 100) / propertyInfo.Resolution);
					}
				}
			}
			else if (propertyInfo.Resolution != 0f)
			{
				result = (uint)((float)((propertyInfo.Maximum - propertyInfo.Minimum) * 254) / propertyInfo.Resolution);
			}
			return result;
		}

		// Token: 0x04000BA2 RID: 2978
		private const uint DefaultPropertyValue = 1000U;

		// Token: 0x04000BA4 RID: 2980
		protected bool _disposed;

		// Token: 0x04000BA5 RID: 2981
		protected Size _doubleTapSize = Size.Empty;

		// Token: 0x04000BA6 RID: 2982
		protected bool _forceUpdateSizeDeltas;

		// Token: 0x04000BA7 RID: 2983
		private MultiTouchSystemGestureLogic _multiTouchSystemGestureLogic;

		// Token: 0x04000BA8 RID: 2984
		protected TabletDeviceInfo _tabletInfo;

		// Token: 0x04000BA9 RID: 2985
		protected StylusPointDescription _stylusPointDescription;
	}
}
