using System;

namespace System.Windows.Input
{
	// Token: 0x020002C0 RID: 704
	internal static class StylusPointPropertyInfoDefaults
	{
		// Token: 0x06001509 RID: 5385 RVA: 0x0004E370 File Offset: 0x0004D770
		internal static StylusPointPropertyInfo GetStylusPointPropertyInfoDefault(StylusPointProperty stylusPointProperty)
		{
			if (stylusPointProperty.Id == StylusPointPropertyIds.X)
			{
				return StylusPointPropertyInfoDefaults.X;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.Y)
			{
				return StylusPointPropertyInfoDefaults.Y;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.Z)
			{
				return StylusPointPropertyInfoDefaults.Z;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.Width)
			{
				return StylusPointPropertyInfoDefaults.Width;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.Height)
			{
				return StylusPointPropertyInfoDefaults.Height;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.SystemTouch)
			{
				return StylusPointPropertyInfoDefaults.SystemTouch;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.PacketStatus)
			{
				return StylusPointPropertyInfoDefaults.PacketStatus;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.SerialNumber)
			{
				return StylusPointPropertyInfoDefaults.SerialNumber;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.NormalPressure)
			{
				return StylusPointPropertyInfoDefaults.NormalPressure;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.TangentPressure)
			{
				return StylusPointPropertyInfoDefaults.TangentPressure;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.ButtonPressure)
			{
				return StylusPointPropertyInfoDefaults.ButtonPressure;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.XTiltOrientation)
			{
				return StylusPointPropertyInfoDefaults.XTiltOrientation;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.YTiltOrientation)
			{
				return StylusPointPropertyInfoDefaults.YTiltOrientation;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.AzimuthOrientation)
			{
				return StylusPointPropertyInfoDefaults.AzimuthOrientation;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.AltitudeOrientation)
			{
				return StylusPointPropertyInfoDefaults.AltitudeOrientation;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.TwistOrientation)
			{
				return StylusPointPropertyInfoDefaults.TwistOrientation;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.PitchRotation)
			{
				return StylusPointPropertyInfoDefaults.PitchRotation;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.RollRotation)
			{
				return StylusPointPropertyInfoDefaults.RollRotation;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.YawRotation)
			{
				return StylusPointPropertyInfoDefaults.YawRotation;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.TipButton)
			{
				return StylusPointPropertyInfoDefaults.TipButton;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.BarrelButton)
			{
				return StylusPointPropertyInfoDefaults.BarrelButton;
			}
			if (stylusPointProperty.Id == StylusPointPropertyIds.SecondaryTipButton)
			{
				return StylusPointPropertyInfoDefaults.SecondaryTipButton;
			}
			if (stylusPointProperty.IsButton)
			{
				return StylusPointPropertyInfoDefaults.DefaultButton;
			}
			return StylusPointPropertyInfoDefaults.DefaultValue;
		}

		// Token: 0x04000B4B RID: 2891
		internal static readonly StylusPointPropertyInfo X = new StylusPointPropertyInfo(StylusPointProperties.X, int.MinValue, int.MaxValue, StylusPointPropertyUnit.Centimeters, 1000f);

		// Token: 0x04000B4C RID: 2892
		internal static readonly StylusPointPropertyInfo Y = new StylusPointPropertyInfo(StylusPointProperties.Y, int.MinValue, int.MaxValue, StylusPointPropertyUnit.Centimeters, 1000f);

		// Token: 0x04000B4D RID: 2893
		internal static readonly StylusPointPropertyInfo Z = new StylusPointPropertyInfo(StylusPointProperties.Z, int.MinValue, int.MaxValue, StylusPointPropertyUnit.Centimeters, 1000f);

		// Token: 0x04000B4E RID: 2894
		internal static readonly StylusPointPropertyInfo Width = new StylusPointPropertyInfo(StylusPointProperties.Width, int.MinValue, int.MaxValue, StylusPointPropertyUnit.Centimeters, 1000f);

		// Token: 0x04000B4F RID: 2895
		internal static readonly StylusPointPropertyInfo Height = new StylusPointPropertyInfo(StylusPointProperties.Height, int.MinValue, int.MaxValue, StylusPointPropertyUnit.Centimeters, 1000f);

		// Token: 0x04000B50 RID: 2896
		internal static readonly StylusPointPropertyInfo SystemTouch = new StylusPointPropertyInfo(StylusPointProperties.SystemTouch, 0, 1, StylusPointPropertyUnit.None, 1f);

		// Token: 0x04000B51 RID: 2897
		internal static readonly StylusPointPropertyInfo PacketStatus = new StylusPointPropertyInfo(StylusPointProperties.PacketStatus, int.MinValue, int.MaxValue, StylusPointPropertyUnit.None, 1f);

		// Token: 0x04000B52 RID: 2898
		internal static readonly StylusPointPropertyInfo SerialNumber = new StylusPointPropertyInfo(StylusPointProperties.SerialNumber, int.MinValue, int.MaxValue, StylusPointPropertyUnit.None, 1f);

		// Token: 0x04000B53 RID: 2899
		internal static readonly StylusPointPropertyInfo NormalPressure = new StylusPointPropertyInfo(StylusPointProperties.NormalPressure, 0, 1023, StylusPointPropertyUnit.None, 1f);

		// Token: 0x04000B54 RID: 2900
		internal static readonly StylusPointPropertyInfo TangentPressure = new StylusPointPropertyInfo(StylusPointProperties.TangentPressure, 0, 1023, StylusPointPropertyUnit.None, 1f);

		// Token: 0x04000B55 RID: 2901
		internal static readonly StylusPointPropertyInfo ButtonPressure = new StylusPointPropertyInfo(StylusPointProperties.ButtonPressure, 0, 1023, StylusPointPropertyUnit.None, 1f);

		// Token: 0x04000B56 RID: 2902
		internal static readonly StylusPointPropertyInfo XTiltOrientation = new StylusPointPropertyInfo(StylusPointProperties.XTiltOrientation, 0, 3600, StylusPointPropertyUnit.Degrees, 10f);

		// Token: 0x04000B57 RID: 2903
		internal static readonly StylusPointPropertyInfo YTiltOrientation = new StylusPointPropertyInfo(StylusPointProperties.YTiltOrientation, 0, 3600, StylusPointPropertyUnit.Degrees, 10f);

		// Token: 0x04000B58 RID: 2904
		internal static readonly StylusPointPropertyInfo AzimuthOrientation = new StylusPointPropertyInfo(StylusPointProperties.AzimuthOrientation, 0, 3600, StylusPointPropertyUnit.Degrees, 10f);

		// Token: 0x04000B59 RID: 2905
		internal static readonly StylusPointPropertyInfo AltitudeOrientation = new StylusPointPropertyInfo(StylusPointProperties.AltitudeOrientation, -900, 900, StylusPointPropertyUnit.Degrees, 10f);

		// Token: 0x04000B5A RID: 2906
		internal static readonly StylusPointPropertyInfo TwistOrientation = new StylusPointPropertyInfo(StylusPointProperties.TwistOrientation, 0, 3600, StylusPointPropertyUnit.Degrees, 10f);

		// Token: 0x04000B5B RID: 2907
		internal static readonly StylusPointPropertyInfo PitchRotation = new StylusPointPropertyInfo(StylusPointProperties.PitchRotation, int.MinValue, int.MaxValue, StylusPointPropertyUnit.None, 1f);

		// Token: 0x04000B5C RID: 2908
		internal static readonly StylusPointPropertyInfo RollRotation = new StylusPointPropertyInfo(StylusPointProperties.RollRotation, int.MinValue, int.MaxValue, StylusPointPropertyUnit.None, 1f);

		// Token: 0x04000B5D RID: 2909
		internal static readonly StylusPointPropertyInfo YawRotation = new StylusPointPropertyInfo(StylusPointProperties.YawRotation, int.MinValue, int.MaxValue, StylusPointPropertyUnit.None, 1f);

		// Token: 0x04000B5E RID: 2910
		internal static readonly StylusPointPropertyInfo TipButton = new StylusPointPropertyInfo(StylusPointProperties.TipButton, 0, 1, StylusPointPropertyUnit.None, 1f);

		// Token: 0x04000B5F RID: 2911
		internal static readonly StylusPointPropertyInfo BarrelButton = new StylusPointPropertyInfo(StylusPointProperties.BarrelButton, 0, 1, StylusPointPropertyUnit.None, 1f);

		// Token: 0x04000B60 RID: 2912
		internal static readonly StylusPointPropertyInfo SecondaryTipButton = new StylusPointPropertyInfo(StylusPointProperties.SecondaryTipButton, 0, 1, StylusPointPropertyUnit.None, 1f);

		// Token: 0x04000B61 RID: 2913
		internal static readonly StylusPointPropertyInfo DefaultValue = new StylusPointPropertyInfo(new StylusPointProperty(Guid.NewGuid(), false), int.MinValue, int.MaxValue, StylusPointPropertyUnit.None, 1f);

		// Token: 0x04000B62 RID: 2914
		internal static readonly StylusPointPropertyInfo DefaultButton = new StylusPointPropertyInfo(new StylusPointProperty(Guid.NewGuid(), true), 0, 1, StylusPointPropertyUnit.None, 1f);
	}
}
