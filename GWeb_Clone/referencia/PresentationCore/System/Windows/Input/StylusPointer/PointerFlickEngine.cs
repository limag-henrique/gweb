using System;

namespace System.Windows.Input.StylusPointer
{
	// Token: 0x020002E9 RID: 745
	internal class PointerFlickEngine
	{
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x0005AC28 File Offset: 0x0005A028
		// (set) Token: 0x06001733 RID: 5939 RVA: 0x0005AC3C File Offset: 0x0005A03C
		internal PointerFlickEngine.FlickResult Result { get; private set; } = new PointerFlickEngine.FlickResult();

		// Token: 0x06001734 RID: 5940 RVA: 0x0005AC50 File Offset: 0x0005A050
		internal PointerFlickEngine(PointerStylusDevice stylusDevice)
		{
			this._stylusDevice = stylusDevice;
			this._timePeriod = 8.0;
			this._timePeriodAlpha = 0.001;
			this._collectingData = false;
			this._analyzingData = false;
			this._previousFlickDataValid = false;
			this._allowPressFlicks = true;
			this.Reset();
			this.SetTolerance(0.5);
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x0005ACC8 File Offset: 0x0005A0C8
		internal void Reset()
		{
			this.ResetResult();
			this._collectingData = false;
			this._analyzingData = false;
			this._movedEnoughFromPenDown = !this._allowPressFlicks;
			this._canDetectFlick = true;
			this._lastPhysicalPointValid = false;
			this._distance = 0.0;
			this._drag = default(Rect);
			this._flickStartPhysical = default(Point);
			this._flickStartTablet = default(Point);
			this._elapsedTime = 0.0;
			this._flickLength = 0.0;
			this._flickDirectionRadians = 0.0;
			this._flickPathDistance = 0.0;
			this._flickTime = 0.0;
			this._flickTimeLowVelocity = 0.0;
			this._previousFlickDataValid = false;
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x0005AD9C File Offset: 0x0005A19C
		internal void ResetResult()
		{
			this.Result.CanBeFlick = true;
			this.Result.IsLengthOk = false;
			this.Result.IsSpeedOk = false;
			this.Result.IsCurvatureOk = false;
			this.Result.IsLiftOk = false;
			this.Result.DirectionDeg = 0;
			this.Result.PhysicalLength = 0;
			this.Result.TabletLength = 0;
			this.Result.PhysicalStart = default(Point);
			this.Result.TabletStart = default(Point);
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x0005AE34 File Offset: 0x0005A234
		internal void Update(RawStylusInputReport rsir, bool initial = false)
		{
			if (this._stylusDevice.TabletDevice.Type != TabletDeviceType.Stylus)
			{
				return;
			}
			RawStylusActions actions = rsir.Actions;
			if (actions != RawStylusActions.Down)
			{
				if (actions == RawStylusActions.Up)
				{
					if (this._canDetectFlick)
					{
						this.ProcessPacket(rsir, false);
						if (this._analyzingData)
						{
							this.Analyze(true);
						}
					}
					this._collectingData = false;
					this._analyzingData = false;
					return;
				}
				if (actions != RawStylusActions.Move)
				{
					return;
				}
				if (this._canDetectFlick)
				{
					this.ProcessPacket(rsir, initial);
					if (this._analyzingData)
					{
						this.Analyze(false);
					}
				}
			}
			else
			{
				this.Reset();
				this._collectingData = true;
				this.ProcessPacket(rsir, true);
				if (this._analyzingData)
				{
					this.Analyze(false);
					return;
				}
			}
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x0005AEDC File Offset: 0x0005A2DC
		private void UpdateTimePeriod(int tickCount, bool initial)
		{
			if (!this._collectingData)
			{
				return;
			}
			if (!initial)
			{
				double num = (double)(tickCount - this._previousTickCount);
				if (num >= 0.0 && num <= 1000.0)
				{
					this._timePeriod = (1.0 - this._timePeriodAlpha) * this._timePeriod + this._timePeriodAlpha * num;
				}
			}
			this._previousTickCount = tickCount;
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x0005AF44 File Offset: 0x0005A344
		private void ProcessPacket(RawStylusInputReport rsir, bool initial)
		{
			this.UpdateTimePeriod(rsir.Timestamp, initial);
			if (!this._collectingData)
			{
				return;
			}
			Point lastTabletPoint = rsir.GetLastTabletPoint();
			Point physicalCoordinates = this.GetPhysicalCoordinates(lastTabletPoint);
			if (initial)
			{
				this._flickStartPhysical = physicalCoordinates;
				this._flickStartTablet = lastTabletPoint;
				this._elapsedTime = 0.0;
				this.SetStableRect();
			}
			else
			{
				this._elapsedTime += this._timePeriod;
			}
			if (!this._movedEnoughFromPenDown)
			{
				if (this._lastPhysicalPointValid)
				{
					double num = this.Distance(this._lastPhysicalPoint, physicalCoordinates);
					this._distance += num;
					if (num > 19.0 || num >= this._flickMaximumStationaryDisplacementX)
					{
						this._movedEnoughFromPenDown = true;
					}
				}
				if (!this._movedEnoughFromPenDown && (!this._drag.Contains(physicalCoordinates) || this._elapsedTime > 3000.0))
				{
					this._movedEnoughFromPenDown = true;
				}
				this._lastPhysicalPoint = physicalCoordinates;
				this._lastPhysicalPointValid = true;
			}
			if (this._movedEnoughFromPenDown && !this._analyzingData)
			{
				this.CheckWithThreshold(physicalCoordinates);
			}
			if (this._analyzingData)
			{
				this.AddPoint(physicalCoordinates, lastTabletPoint);
			}
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x0005B060 File Offset: 0x0005A460
		private void Analyze(bool decide)
		{
			this.Result.CanBeFlick = true;
			this.Result.IsLengthOk = true;
			this.Result.IsSpeedOk = true;
			this.Result.IsCurvatureOk = true;
			this.Result.IsLiftOk = true;
			this.Result.DirectionDeg = Convert.ToInt32(this.RadiansToDegrees(this._flickDirectionRadians));
			this.Result.PhysicalStart = this._flickStartPhysical;
			this.Result.TabletStart = this._flickStartTablet;
			this.Result.PhysicalLength = Convert.ToInt32(0.5 + this.Distance(this.Result.PhysicalStart, this._previousFlickData.PhysicalPoint));
			this.Result.TabletLength = Convert.ToInt32(0.5 + this.Distance(this.Result.TabletStart, this._previousFlickData.TabletPoint));
			double num = this._flickPathDistance - this._flickLength;
			double num2 = 1.0;
			if (this._flickLength > 0.0)
			{
				num2 = this._flickPathDistance / this._flickLength;
			}
			if (this._flickTimeLowVelocity > this._flickMaximumStationaryTime)
			{
				this.Result.CanBeFlick = false;
				this.Result.IsLiftOk = false;
			}
			if (this._flickTime > this._flickMaximumTime)
			{
				this.Result.CanBeFlick = false;
				this.Result.IsSpeedOk = false;
			}
			if ((num2 > this._flickMaximumLengthRatio && this._flickLength > 500.0 && num > 200.0) || num > 300.0)
			{
				this.Result.CanBeFlick = false;
				this.Result.IsCurvatureOk = false;
			}
			if (this._flickLength < this._flickMinimumLength && decide)
			{
				this.Result.CanBeFlick = false;
				this.Result.IsLengthOk = false;
			}
			if (!this.Result.CanBeFlick || decide)
			{
				this._collectingData = false;
				this._analyzingData = false;
			}
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x0005B26C File Offset: 0x0005A66C
		private void AddPoint(Point physicalPoint, Point tabletPoint)
		{
			PointerFlickEngine.FlickRecognitionData flickRecognitionData = new PointerFlickEngine.FlickRecognitionData
			{
				PhysicalPoint = physicalPoint,
				TabletPoint = tabletPoint,
				Time = 0.0,
				Displacement = 0.0,
				Velocity = 0.0
			};
			if (this._previousFlickDataValid)
			{
				flickRecognitionData.Time = this._previousFlickData.Time + this._timePeriod;
				flickRecognitionData.Displacement = this.Distance(physicalPoint, this._previousFlickData.PhysicalPoint);
				flickRecognitionData.Velocity = flickRecognitionData.Displacement / this._timePeriod;
			}
			else
			{
				this._flickPathDistance = this.Distance(physicalPoint, this._flickStartPhysical);
			}
			this._flickLength = this.Distance(physicalPoint, this._flickStartPhysical);
			this._flickDirectionRadians = Math.Atan2(flickRecognitionData.PhysicalPoint.Y - this._flickStartPhysical.Y, flickRecognitionData.PhysicalPoint.X - this._flickStartPhysical.X);
			this._flickPathDistance += flickRecognitionData.Displacement;
			this._flickTime += this._timePeriod;
			this._flickTimeLowVelocity += ((flickRecognitionData.Velocity < this._flickMinimumVelocity) ? this._timePeriod : 0.0);
			this._previousFlickDataValid = true;
			this._previousFlickData = flickRecognitionData;
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0005B3CC File Offset: 0x0005A7CC
		private void CheckWithThreshold(Point physicalPoint)
		{
			this._analyzingData = (this.Distance(physicalPoint, this._flickStartPhysical) > 100.0 || this._elapsedTime > 150.0);
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x0005B40C File Offset: 0x0005A80C
		private void SetStableRect()
		{
			if (this._collectingData)
			{
				this._drag = new Rect(this._flickStartPhysical, new Size(this._flickMaximumStationaryDisplacementX, this._flickMaximumStationaryDisplacementY));
			}
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x0005B444 File Offset: 0x0005A844
		private double RadiansToDegrees(double radians)
		{
			return (180.0 * radians / 3.1415926535897931 + 360.0) % 360.0;
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x0005B47C File Offset: 0x0005A87C
		private double Distance(Point p1, Point p2)
		{
			return Math.Sqrt(Math.Pow(p1.X - p2.X, 2.0) + Math.Pow(p1.Y - p2.Y, 2.0));
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x0005B4CC File Offset: 0x0005A8CC
		private Point GetPhysicalCoordinates(Point tabletPoint)
		{
			double num = (double)(this._stylusDevice.PointerTabletDevice.DeviceInfo.DeviceRect.right - this._stylusDevice.PointerTabletDevice.DeviceInfo.DeviceRect.left);
			double num2 = (double)(this._stylusDevice.PointerTabletDevice.DeviceInfo.DeviceRect.top - this._stylusDevice.PointerTabletDevice.DeviceInfo.DeviceRect.bottom);
			double width = this._stylusDevice.PointerTabletDevice.DeviceInfo.SizeInfo.TabletSize.Width;
			double height = this._stylusDevice.PointerTabletDevice.DeviceInfo.SizeInfo.TabletSize.Height;
			return new Point(tabletPoint.X * num / width, tabletPoint.Y * num2 / height);
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x0005B5A4 File Offset: 0x0005A9A4
		private bool SetTolerance(double tolerance)
		{
			bool flag = tolerance > 0.0 && tolerance < 1.0;
			if (flag)
			{
				this._flickMinimumLength = tolerance * 800.0 + (1.0 - tolerance) * 400.0;
				this._flickMaximumLengthRatio = tolerance * 1.01 + (1.0 - tolerance) * 1.1;
				this._flickMinimumVelocity = tolerance * 19.0 + (1.0 - tolerance) * 8.0;
				this._flickMaximumTime = tolerance * 200.0 + (1.0 - tolerance) * 300.0;
				this._flickMaximumStationaryTime = tolerance * 45.0 + (1.0 - tolerance) * 45.0;
				this._flickMaximumStationaryDisplacementX = tolerance * 0.0 + (1.0 - tolerance) * 150.0;
				this._flickMaximumStationaryDisplacementY = tolerance * 0.0 + (1.0 - tolerance) * 150.0;
				this._tolerance = tolerance;
			}
			return flag;
		}

		// Token: 0x04000C9A RID: 3226
		private const double ThresholdTime = 150.0;

		// Token: 0x04000C9B RID: 3227
		private const double ThresholdLength = 100.0;

		// Token: 0x04000C9C RID: 3228
		private const double RelaxedFlickMinimumLength = 400.0;

		// Token: 0x04000C9D RID: 3229
		private const double RelaxedFlickMaximumLengthRatio = 1.1;

		// Token: 0x04000C9E RID: 3230
		private const double RelaxedFlickMinimumVelocity = 8.0;

		// Token: 0x04000C9F RID: 3231
		private const double RelaxedFlickMaximumTime = 300.0;

		// Token: 0x04000CA0 RID: 3232
		private const double RelaxedFlickMaximumStationaryTime = 45.0;

		// Token: 0x04000CA1 RID: 3233
		private const double RelaxedFlickMaxStationaryDispX = 150.0;

		// Token: 0x04000CA2 RID: 3234
		private const double RelaxedFlickMaxStationaryDispY = 150.0;

		// Token: 0x04000CA3 RID: 3235
		private const double PreciseFlickMinimumLength = 800.0;

		// Token: 0x04000CA4 RID: 3236
		private const double PreciseFlickMaximumLengthRatio = 1.01;

		// Token: 0x04000CA5 RID: 3237
		private const double PreciseFlickMinimumVelocity = 19.0;

		// Token: 0x04000CA6 RID: 3238
		private const double PreciseFlickMaximumTime = 200.0;

		// Token: 0x04000CA7 RID: 3239
		private const double PreciseFlickMaximumStationaryTime = 45.0;

		// Token: 0x04000CA8 RID: 3240
		private const double PreciseFlickMaxStationaryDispX = 0.0;

		// Token: 0x04000CA9 RID: 3241
		private const double PreciseFlickMaxStationaryDispY = 0.0;

		// Token: 0x04000CAA RID: 3242
		private bool _collectingData;

		// Token: 0x04000CAB RID: 3243
		private bool _analyzingData;

		// Token: 0x04000CAC RID: 3244
		private bool _lastPhysicalPointValid;

		// Token: 0x04000CAD RID: 3245
		private bool _movedEnoughFromPenDown;

		// Token: 0x04000CAE RID: 3246
		private bool _canDetectFlick;

		// Token: 0x04000CAF RID: 3247
		private bool _allowPressFlicks;

		// Token: 0x04000CB0 RID: 3248
		private bool _previousFlickDataValid;

		// Token: 0x04000CB1 RID: 3249
		private Point _flickStartPhysical;

		// Token: 0x04000CB2 RID: 3250
		private Point _flickStartTablet;

		// Token: 0x04000CB3 RID: 3251
		private Point _lastPhysicalPoint;

		// Token: 0x04000CB4 RID: 3252
		private PointerStylusDevice _stylusDevice;

		// Token: 0x04000CB5 RID: 3253
		private double _distance;

		// Token: 0x04000CB6 RID: 3254
		private double _flickDirectionRadians;

		// Token: 0x04000CB7 RID: 3255
		private double _flickPathDistance;

		// Token: 0x04000CB8 RID: 3256
		private double _flickLength;

		// Token: 0x04000CB9 RID: 3257
		private double _flickTimeLowVelocity;

		// Token: 0x04000CBA RID: 3258
		private double _flickMaximumStationaryTime;

		// Token: 0x04000CBB RID: 3259
		private double _flickMaximumLengthRatio;

		// Token: 0x04000CBC RID: 3260
		private double _flickMinimumLength;

		// Token: 0x04000CBD RID: 3261
		private double _flickMinimumVelocity;

		// Token: 0x04000CBE RID: 3262
		private double _flickMaximumStationaryDisplacementX;

		// Token: 0x04000CBF RID: 3263
		private double _flickMaximumStationaryDisplacementY;

		// Token: 0x04000CC0 RID: 3264
		private double _tolerance;

		// Token: 0x04000CC1 RID: 3265
		private PointerFlickEngine.FlickRecognitionData _previousFlickData;

		// Token: 0x04000CC2 RID: 3266
		private Rect _drag;

		// Token: 0x04000CC3 RID: 3267
		private double _timePeriod;

		// Token: 0x04000CC4 RID: 3268
		private double _timePeriodAlpha;

		// Token: 0x04000CC5 RID: 3269
		private int _previousTickCount;

		// Token: 0x04000CC6 RID: 3270
		private double _elapsedTime;

		// Token: 0x04000CC7 RID: 3271
		private double _flickTime;

		// Token: 0x04000CC8 RID: 3272
		private double _flickMaximumTime;

		// Token: 0x02000823 RID: 2083
		internal class FlickResult
		{
			// Token: 0x170011B1 RID: 4529
			// (get) Token: 0x0600563B RID: 22075 RVA: 0x00162644 File Offset: 0x00161A44
			// (set) Token: 0x0600563C RID: 22076 RVA: 0x00162658 File Offset: 0x00161A58
			internal Point PhysicalStart { get; set; }

			// Token: 0x170011B2 RID: 4530
			// (get) Token: 0x0600563D RID: 22077 RVA: 0x0016266C File Offset: 0x00161A6C
			// (set) Token: 0x0600563E RID: 22078 RVA: 0x00162680 File Offset: 0x00161A80
			internal Point TabletStart { get; set; }

			// Token: 0x170011B3 RID: 4531
			// (get) Token: 0x0600563F RID: 22079 RVA: 0x00162694 File Offset: 0x00161A94
			// (set) Token: 0x06005640 RID: 22080 RVA: 0x001626A8 File Offset: 0x00161AA8
			internal int PhysicalLength { get; set; }

			// Token: 0x170011B4 RID: 4532
			// (get) Token: 0x06005641 RID: 22081 RVA: 0x001626BC File Offset: 0x00161ABC
			// (set) Token: 0x06005642 RID: 22082 RVA: 0x001626D0 File Offset: 0x00161AD0
			internal int TabletLength { get; set; }

			// Token: 0x170011B5 RID: 4533
			// (get) Token: 0x06005643 RID: 22083 RVA: 0x001626E4 File Offset: 0x00161AE4
			// (set) Token: 0x06005644 RID: 22084 RVA: 0x001626F8 File Offset: 0x00161AF8
			internal int DirectionDeg { get; set; }

			// Token: 0x170011B6 RID: 4534
			// (get) Token: 0x06005645 RID: 22085 RVA: 0x0016270C File Offset: 0x00161B0C
			// (set) Token: 0x06005646 RID: 22086 RVA: 0x00162720 File Offset: 0x00161B20
			internal bool CanBeFlick { get; set; }

			// Token: 0x170011B7 RID: 4535
			// (get) Token: 0x06005647 RID: 22087 RVA: 0x00162734 File Offset: 0x00161B34
			// (set) Token: 0x06005648 RID: 22088 RVA: 0x00162748 File Offset: 0x00161B48
			internal bool IsLengthOk { get; set; }

			// Token: 0x170011B8 RID: 4536
			// (get) Token: 0x06005649 RID: 22089 RVA: 0x0016275C File Offset: 0x00161B5C
			// (set) Token: 0x0600564A RID: 22090 RVA: 0x00162770 File Offset: 0x00161B70
			internal bool IsSpeedOk { get; set; }

			// Token: 0x170011B9 RID: 4537
			// (get) Token: 0x0600564B RID: 22091 RVA: 0x00162784 File Offset: 0x00161B84
			// (set) Token: 0x0600564C RID: 22092 RVA: 0x00162798 File Offset: 0x00161B98
			internal bool IsCurvatureOk { get; set; }

			// Token: 0x170011BA RID: 4538
			// (get) Token: 0x0600564D RID: 22093 RVA: 0x001627AC File Offset: 0x00161BAC
			// (set) Token: 0x0600564E RID: 22094 RVA: 0x001627C0 File Offset: 0x00161BC0
			internal bool IsLiftOk { get; set; }
		}

		// Token: 0x02000824 RID: 2084
		private class FlickRecognitionData
		{
			// Token: 0x170011BB RID: 4539
			// (get) Token: 0x06005650 RID: 22096 RVA: 0x001627E8 File Offset: 0x00161BE8
			// (set) Token: 0x06005651 RID: 22097 RVA: 0x001627FC File Offset: 0x00161BFC
			internal Point PhysicalPoint { get; set; }

			// Token: 0x170011BC RID: 4540
			// (get) Token: 0x06005652 RID: 22098 RVA: 0x00162810 File Offset: 0x00161C10
			// (set) Token: 0x06005653 RID: 22099 RVA: 0x00162824 File Offset: 0x00161C24
			internal double Time { get; set; }

			// Token: 0x170011BD RID: 4541
			// (get) Token: 0x06005654 RID: 22100 RVA: 0x00162838 File Offset: 0x00161C38
			// (set) Token: 0x06005655 RID: 22101 RVA: 0x0016284C File Offset: 0x00161C4C
			internal double Displacement { get; set; }

			// Token: 0x170011BE RID: 4542
			// (get) Token: 0x06005656 RID: 22102 RVA: 0x00162860 File Offset: 0x00161C60
			// (set) Token: 0x06005657 RID: 22103 RVA: 0x00162874 File Offset: 0x00161C74
			internal double Velocity { get; set; }

			// Token: 0x170011BF RID: 4543
			// (get) Token: 0x06005658 RID: 22104 RVA: 0x00162888 File Offset: 0x00161C88
			// (set) Token: 0x06005659 RID: 22105 RVA: 0x0016289C File Offset: 0x00161C9C
			internal Point TabletPoint { get; set; }
		}
	}
}
