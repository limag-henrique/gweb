using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security;
using MS.Win32.Pointer;

namespace System.Windows.Input.StylusPointer
{
	// Token: 0x020002F2 RID: 754
	internal class PointerTabletDeviceInfo : TabletDeviceInfo
	{
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x0005FB40 File Offset: 0x0005EF40
		// (set) Token: 0x060017DF RID: 6111 RVA: 0x0005FB54 File Offset: 0x0005EF54
		internal UnsafeNativeMethods.POINTER_DEVICE_PROPERTY[] SupportedPointerProperties { get; private set; }

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x0005FB68 File Offset: 0x0005EF68
		// (set) Token: 0x060017E1 RID: 6113 RVA: 0x0005FB7C File Offset: 0x0005EF7C
		internal int SupportedButtonPropertyIndex { get; private set; }

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x0005FB90 File Offset: 0x0005EF90
		// (set) Token: 0x060017E3 RID: 6115 RVA: 0x0005FBA4 File Offset: 0x0005EFA4
		internal StylusButtonCollection StylusButtons { get; private set; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x0005FBB8 File Offset: 0x0005EFB8
		internal IntPtr Device
		{
			get
			{
				return this._deviceInfo.device;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x0005FBD0 File Offset: 0x0005EFD0
		// (set) Token: 0x060017E6 RID: 6118 RVA: 0x0005FBE4 File Offset: 0x0005EFE4
		internal bool UsingFakePressure { get; private set; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x0005FBF8 File Offset: 0x0005EFF8
		// (set) Token: 0x060017E8 RID: 6120 RVA: 0x0005FC0C File Offset: 0x0005F00C
		internal UnsafeNativeMethods.RECT DeviceRect { get; private set; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x0005FC20 File Offset: 0x0005F020
		// (set) Token: 0x060017EA RID: 6122 RVA: 0x0005FC34 File Offset: 0x0005F034
		internal UnsafeNativeMethods.RECT DisplayRect { get; private set; }

		// Token: 0x060017EB RID: 6123 RVA: 0x0005FC48 File Offset: 0x0005F048
		internal PointerTabletDeviceInfo(int id, UnsafeNativeMethods.POINTER_DEVICE_INFO deviceInfo)
		{
			this._deviceInfo = deviceInfo;
			this.Id = id;
			this.Name = this._deviceInfo.productString;
			this.PlugAndPlayId = this._deviceInfo.productString;
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x0005FC8C File Offset: 0x0005F08C
		internal bool TryInitialize()
		{
			this.InitializeDeviceType();
			bool flag = this.TryInitializeSupportedStylusPointProperties();
			if (flag)
			{
				flag = this.TryInitializeDeviceRects();
			}
			return flag;
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x0005FCB4 File Offset: 0x0005F0B4
		private void InitializeDeviceType()
		{
			switch (this._deviceInfo.pointerDeviceType)
			{
			case UnsafeNativeMethods.POINTER_DEVICE_TYPE.POINTER_DEVICE_TYPE_INTEGRATED_PEN:
				this.DeviceType = TabletDeviceType.Stylus;
				this.HardwareCapabilities |= TabletHardwareCapabilities.Integrated;
				break;
			case UnsafeNativeMethods.POINTER_DEVICE_TYPE.POINTER_DEVICE_TYPE_EXTERNAL_PEN:
				this.DeviceType = TabletDeviceType.Stylus;
				break;
			case UnsafeNativeMethods.POINTER_DEVICE_TYPE.POINTER_DEVICE_TYPE_TOUCH:
				this.DeviceType = TabletDeviceType.Touch;
				this.HardwareCapabilities |= TabletHardwareCapabilities.Integrated;
				break;
			case UnsafeNativeMethods.POINTER_DEVICE_TYPE.POINTER_DEVICE_TYPE_TOUCH_PAD:
				this.DeviceType = TabletDeviceType.Touch;
				break;
			}
			this.HardwareCapabilities |= TabletHardwareCapabilities.HardProximity;
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x0005FD34 File Offset: 0x0005F134
		[SecuritySafeCritical]
		private bool TryInitializeSupportedStylusPointProperties()
		{
			uint num = 0U;
			this.PressureIndex = -1;
			this.UsingFakePressure = true;
			bool pointerDeviceProperties = UnsafeNativeMethods.GetPointerDeviceProperties(this.Device, ref num, null);
			if (pointerDeviceProperties)
			{
				this.SupportedPointerProperties = new UnsafeNativeMethods.POINTER_DEVICE_PROPERTY[num];
				pointerDeviceProperties = UnsafeNativeMethods.GetPointerDeviceProperties(this.Device, ref num, this.SupportedPointerProperties);
				if (pointerDeviceProperties)
				{
					List<StylusPointProperty> list = new List<StylusPointProperty>
					{
						StylusPointPropertyInfoDefaults.X,
						StylusPointPropertyInfoDefaults.Y,
						StylusPointPropertyInfoDefaults.NormalPressure
					};
					List<StylusPointProperty> list2 = new List<StylusPointProperty>();
					List<UnsafeNativeMethods.POINTER_DEVICE_PROPERTY> list3 = new List<UnsafeNativeMethods.POINTER_DEVICE_PROPERTY>
					{
						default(UnsafeNativeMethods.POINTER_DEVICE_PROPERTY),
						default(UnsafeNativeMethods.POINTER_DEVICE_PROPERTY)
					};
					List<UnsafeNativeMethods.POINTER_DEVICE_PROPERTY> list4 = new List<UnsafeNativeMethods.POINTER_DEVICE_PROPERTY>();
					bool flag = false;
					foreach (UnsafeNativeMethods.POINTER_DEVICE_PROPERTY pointer_DEVICE_PROPERTY in this.SupportedPointerProperties)
					{
						StylusPointPropertyInfo stylusPointPropertyInfo = PointerStylusPointPropertyInfoHelper.CreatePropertyInfo(pointer_DEVICE_PROPERTY);
						if (stylusPointPropertyInfo != null)
						{
							if (stylusPointPropertyInfo.Id == StylusPointPropertyIds.NormalPressure)
							{
								flag = true;
								list[StylusPointDescription.RequiredPressureIndex] = stylusPointPropertyInfo;
								list3.Insert(StylusPointDescription.RequiredPressureIndex, pointer_DEVICE_PROPERTY);
							}
							else if (stylusPointPropertyInfo.Id == StylusPointPropertyIds.X)
							{
								list[StylusPointDescription.RequiredXIndex] = stylusPointPropertyInfo;
								list3[StylusPointDescription.RequiredXIndex] = pointer_DEVICE_PROPERTY;
							}
							else if (stylusPointPropertyInfo.Id == StylusPointPropertyIds.Y)
							{
								list[StylusPointDescription.RequiredYIndex] = stylusPointPropertyInfo;
								list3[StylusPointDescription.RequiredYIndex] = pointer_DEVICE_PROPERTY;
							}
							else if (stylusPointPropertyInfo.IsButton)
							{
								list2.Add(stylusPointPropertyInfo);
								list4.Add(pointer_DEVICE_PROPERTY);
							}
							else
							{
								list.Add(stylusPointPropertyInfo);
								list3.Add(pointer_DEVICE_PROPERTY);
							}
						}
					}
					if (flag)
					{
						this.PressureIndex = StylusPointDescription.RequiredPressureIndex;
						this.UsingFakePressure = false;
						this.HardwareCapabilities |= TabletHardwareCapabilities.SupportsPressure;
					}
					list.AddRange(list2);
					this.SupportedButtonPropertyIndex = list3.Count;
					list3.AddRange(list4);
					this.StylusPointProperties = new ReadOnlyCollection<StylusPointProperty>(list);
					this.SupportedPointerProperties = list3.ToArray();
				}
			}
			return pointerDeviceProperties;
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x0005FF48 File Offset: 0x0005F348
		[SecuritySafeCritical]
		private bool TryInitializeDeviceRects()
		{
			UnsafeNativeMethods.RECT deviceRect = default(UnsafeNativeMethods.RECT);
			UnsafeNativeMethods.RECT rect = default(UnsafeNativeMethods.RECT);
			bool pointerDeviceRects = UnsafeNativeMethods.GetPointerDeviceRects(this._deviceInfo.device, ref deviceRect, ref rect);
			if (pointerDeviceRects)
			{
				this.DeviceRect = deviceRect;
				this.DisplayRect = rect;
				this.SizeInfo = new TabletDeviceSizeInfo(new Size((double)this.SupportedPointerProperties[StylusPointDescription.RequiredXIndex].logicalMax, (double)this.SupportedPointerProperties[StylusPointDescription.RequiredYIndex].logicalMax), new Size((double)(rect.right - rect.left), (double)(rect.bottom - rect.top)));
			}
			return pointerDeviceRects;
		}

		// Token: 0x04000D0E RID: 3342
		private UnsafeNativeMethods.POINTER_DEVICE_INFO _deviceInfo;
	}
}
