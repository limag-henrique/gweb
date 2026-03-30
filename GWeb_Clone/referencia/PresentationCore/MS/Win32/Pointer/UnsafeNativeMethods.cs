using System;
using System.Runtime.InteropServices;
using System.Security;

namespace MS.Win32.Pointer
{
	// Token: 0x0200064A RID: 1610
	internal class UnsafeNativeMethods
	{
		// Token: 0x06004834 RID: 18484
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool GetPointerDevices([In] [Out] ref uint deviceCount, [In] [Out] UnsafeNativeMethods.POINTER_DEVICE_INFO[] devices);

		// Token: 0x06004835 RID: 18485
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool GetPointerDeviceCursors([In] IntPtr device, [In] [Out] ref uint cursorCount, [In] [Out] UnsafeNativeMethods.POINTER_DEVICE_CURSOR_INFO[] cursors);

		// Token: 0x06004836 RID: 18486
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool GetPointerInfo([In] uint pointerId, [In] [Out] ref UnsafeNativeMethods.POINTER_INFO pointerInfo);

		// Token: 0x06004837 RID: 18487
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool GetPointerInfoHistory([In] uint pointerId, [In] [Out] ref uint entriesCount, [In] [Out] UnsafeNativeMethods.POINTER_INFO[] pointerInfo);

		// Token: 0x06004838 RID: 18488
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool GetPointerDeviceProperties([In] IntPtr device, [In] [Out] ref uint propertyCount, [In] [Out] UnsafeNativeMethods.POINTER_DEVICE_PROPERTY[] pointerProperties);

		// Token: 0x06004839 RID: 18489
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool GetPointerDeviceRects([In] IntPtr device, [In] [Out] ref UnsafeNativeMethods.RECT pointerDeviceRect, [In] [Out] ref UnsafeNativeMethods.RECT displayRect);

		// Token: 0x0600483A RID: 18490
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool GetPointerCursorId([In] uint pointerId, [In] [Out] ref uint cursorId);

		// Token: 0x0600483B RID: 18491
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool GetPointerPenInfo([In] uint pointerId, [In] [Out] ref UnsafeNativeMethods.POINTER_PEN_INFO penInfo);

		// Token: 0x0600483C RID: 18492
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool GetPointerTouchInfo([In] uint pointerId, [In] [Out] ref UnsafeNativeMethods.POINTER_TOUCH_INFO touchInfo);

		// Token: 0x0600483D RID: 18493
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool GetRawPointerDeviceData([In] uint pointerId, [In] uint historyCount, [In] uint propertiesCount, [In] UnsafeNativeMethods.POINTER_DEVICE_PROPERTY[] pProperties, [In] [Out] int[] pValues);

		// Token: 0x0600483E RID: 18494
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("ninput.dll", SetLastError = true)]
		internal static extern void CreateInteractionContext(out IntPtr interactionContext);

		// Token: 0x0600483F RID: 18495
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("ninput.dll", SetLastError = true)]
		internal static extern void DestroyInteractionContext([In] IntPtr interactionContext);

		// Token: 0x06004840 RID: 18496
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("ninput.dll", SetLastError = true)]
		internal static extern void SetInteractionConfigurationInteractionContext([In] IntPtr interactionContext, [In] uint configurationCount, [In] UnsafeNativeMethods.INTERACTION_CONTEXT_CONFIGURATION[] configuration);

		// Token: 0x06004841 RID: 18497
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("ninput.dll", PreserveSig = false, SetLastError = true)]
		internal static extern void RegisterOutputCallbackInteractionContext([In] IntPtr interactionContext, [In] UnsafeNativeMethods.INTERACTION_CONTEXT_OUTPUT_CALLBACK outputCallback, [In] [Optional] IntPtr clientData);

		// Token: 0x06004842 RID: 18498
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("ninput.dll", SetLastError = true)]
		internal static extern void SetPropertyInteractionContext([In] IntPtr interactionContext, [In] UnsafeNativeMethods.INTERACTION_CONTEXT_PROPERTY contextProperty, [In] uint value);

		// Token: 0x06004843 RID: 18499
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("ninput.dll", PreserveSig = false, SetLastError = true)]
		internal static extern void BufferPointerPacketsInteractionContext([In] IntPtr interactionContext, [In] uint entriesCount, [In] UnsafeNativeMethods.POINTER_INFO[] pointerInfo);

		// Token: 0x06004844 RID: 18500
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("ninput.dll", PreserveSig = false, SetLastError = true)]
		internal static extern void ProcessBufferedPacketsInteractionContext([In] IntPtr interactionContext);

		// Token: 0x04001BCD RID: 7117
		internal const int POINTER_DEVICE_PRODUCT_STRING_MAX = 520;

		// Token: 0x02000982 RID: 2434
		[Flags]
		internal enum TOUCH_FLAGS : uint
		{
			// Token: 0x04002CB1 RID: 11441
			TOUCH_FLAG_NONE = 0U
		}

		// Token: 0x02000983 RID: 2435
		[Flags]
		internal enum TOUCH_MASK : uint
		{
			// Token: 0x04002CB3 RID: 11443
			TOUCH_MASK_NONE = 0U,
			// Token: 0x04002CB4 RID: 11444
			TOUCH_MASK_CONTACTAREA = 1U,
			// Token: 0x04002CB5 RID: 11445
			TOUCH_MASK_ORIENTATION = 2U,
			// Token: 0x04002CB6 RID: 11446
			TOUCH_MASK_PRESSURE = 4U
		}

		// Token: 0x02000984 RID: 2436
		[Flags]
		internal enum PEN_MASK : uint
		{
			// Token: 0x04002CB8 RID: 11448
			PEN_MASK_NONE = 0U,
			// Token: 0x04002CB9 RID: 11449
			PEN_MASK_PRESSURE = 1U,
			// Token: 0x04002CBA RID: 11450
			PEN_MASK_ROTATION = 2U,
			// Token: 0x04002CBB RID: 11451
			PEN_MASK_TILT_X = 4U,
			// Token: 0x04002CBC RID: 11452
			PEN_MASK_TILT_Y = 8U
		}

		// Token: 0x02000985 RID: 2437
		[Flags]
		internal enum PEN_FLAGS : uint
		{
			// Token: 0x04002CBE RID: 11454
			PEN_FLAG_NONE = 0U,
			// Token: 0x04002CBF RID: 11455
			PEN_FLAG_BARREL = 1U,
			// Token: 0x04002CC0 RID: 11456
			PEN_FLAG_INVERTED = 2U,
			// Token: 0x04002CC1 RID: 11457
			PEN_FLAG_ERASER = 4U
		}

		// Token: 0x02000986 RID: 2438
		internal enum POINTER_DEVICE_CURSOR_TYPE : uint
		{
			// Token: 0x04002CC3 RID: 11459
			POINTER_DEVICE_CURSOR_TYPE_UNKNOWN,
			// Token: 0x04002CC4 RID: 11460
			POINTER_DEVICE_CURSOR_TYPE_TIP,
			// Token: 0x04002CC5 RID: 11461
			POINTER_DEVICE_CURSOR_TYPE_ERASER,
			// Token: 0x04002CC6 RID: 11462
			POINTER_DEVICE_CURSOR_TYPE_MAX = 4294967295U
		}

		// Token: 0x02000987 RID: 2439
		internal enum POINTER_DEVICE_TYPE : uint
		{
			// Token: 0x04002CC8 RID: 11464
			POINTER_DEVICE_TYPE_INTEGRATED_PEN = 1U,
			// Token: 0x04002CC9 RID: 11465
			POINTER_DEVICE_TYPE_EXTERNAL_PEN,
			// Token: 0x04002CCA RID: 11466
			POINTER_DEVICE_TYPE_TOUCH,
			// Token: 0x04002CCB RID: 11467
			POINTER_DEVICE_TYPE_TOUCH_PAD,
			// Token: 0x04002CCC RID: 11468
			POINTER_DEVICE_TYPE_MAX = 4294967295U
		}

		// Token: 0x02000988 RID: 2440
		internal enum POINTER_INPUT_TYPE : uint
		{
			// Token: 0x04002CCE RID: 11470
			PT_POINTER = 1U,
			// Token: 0x04002CCF RID: 11471
			PT_TOUCH,
			// Token: 0x04002CD0 RID: 11472
			PT_PEN,
			// Token: 0x04002CD1 RID: 11473
			PT_MOUSE,
			// Token: 0x04002CD2 RID: 11474
			PT_TOUCHPAD
		}

		// Token: 0x02000989 RID: 2441
		[Flags]
		internal enum POINTER_FLAGS : uint
		{
			// Token: 0x04002CD4 RID: 11476
			POINTER_FLAG_NONE = 0U,
			// Token: 0x04002CD5 RID: 11477
			POINTER_FLAG_NEW = 1U,
			// Token: 0x04002CD6 RID: 11478
			POINTER_FLAG_INRANGE = 2U,
			// Token: 0x04002CD7 RID: 11479
			POINTER_FLAG_INCONTACT = 4U,
			// Token: 0x04002CD8 RID: 11480
			POINTER_FLAG_FIRSTBUTTON = 16U,
			// Token: 0x04002CD9 RID: 11481
			POINTER_FLAG_SECONDBUTTON = 32U,
			// Token: 0x04002CDA RID: 11482
			POINTER_FLAG_THIRDBUTTON = 64U,
			// Token: 0x04002CDB RID: 11483
			POINTER_FLAG_FOURTHBUTTON = 128U,
			// Token: 0x04002CDC RID: 11484
			POINTER_FLAG_FIFTHBUTTON = 256U,
			// Token: 0x04002CDD RID: 11485
			POINTER_FLAG_PRIMARY = 8192U,
			// Token: 0x04002CDE RID: 11486
			POINTER_FLAG_CONFIDENCE = 16384U,
			// Token: 0x04002CDF RID: 11487
			POINTER_FLAG_CANCELED = 32768U,
			// Token: 0x04002CE0 RID: 11488
			POINTER_FLAG_DOWN = 65536U,
			// Token: 0x04002CE1 RID: 11489
			POINTER_FLAG_UPDATE = 131072U,
			// Token: 0x04002CE2 RID: 11490
			POINTER_FLAG_UP = 262144U,
			// Token: 0x04002CE3 RID: 11491
			POINTER_FLAG_WHEEL = 524288U,
			// Token: 0x04002CE4 RID: 11492
			POINTER_FLAG_HWHEEL = 1048576U,
			// Token: 0x04002CE5 RID: 11493
			POINTER_FLAG_CAPTURECHANGED = 2097152U,
			// Token: 0x04002CE6 RID: 11494
			POINTER_FLAG_HASTRANSFORM = 4194304U
		}

		// Token: 0x0200098A RID: 2442
		internal enum POINTER_BUTTON_CHANGE_TYPE : uint
		{
			// Token: 0x04002CE8 RID: 11496
			POINTER_CHANGE_NONE,
			// Token: 0x04002CE9 RID: 11497
			POINTER_CHANGE_FIRSTBUTTON_DOWN,
			// Token: 0x04002CEA RID: 11498
			POINTER_CHANGE_FIRSTBUTTON_UP,
			// Token: 0x04002CEB RID: 11499
			POINTER_CHANGE_SECONDBUTTON_DOWN,
			// Token: 0x04002CEC RID: 11500
			POINTER_CHANGE_SECONDBUTTON_UP,
			// Token: 0x04002CED RID: 11501
			POINTER_CHANGE_THIRDBUTTON_DOWN,
			// Token: 0x04002CEE RID: 11502
			POINTER_CHANGE_THIRDBUTTON_UP,
			// Token: 0x04002CEF RID: 11503
			POINTER_CHANGE_FOURTHBUTTON_DOWN,
			// Token: 0x04002CF0 RID: 11504
			POINTER_CHANGE_FOURTHBUTTON_UP,
			// Token: 0x04002CF1 RID: 11505
			POINTER_CHANGE_FIFTHBUTTON_DOWN,
			// Token: 0x04002CF2 RID: 11506
			POINTER_CHANGE_FIFTHBUTTON_UP
		}

		// Token: 0x0200098B RID: 2443
		internal enum INTERACTION_CONTEXT_PROPERTY : uint
		{
			// Token: 0x04002CF4 RID: 11508
			INTERACTION_CONTEXT_PROPERTY_MEASUREMENT_UNITS = 1U,
			// Token: 0x04002CF5 RID: 11509
			INTERACTION_CONTEXT_PROPERTY_INTERACTION_UI_FEEDBACK,
			// Token: 0x04002CF6 RID: 11510
			INTERACTION_CONTEXT_PROPERTY_FILTER_POINTERS,
			// Token: 0x04002CF7 RID: 11511
			INTERACTION_CONTEXT_PROPERTY_MAX = 4294967295U
		}

		// Token: 0x0200098C RID: 2444
		[Flags]
		internal enum CROSS_SLIDE_FLAGS : uint
		{
			// Token: 0x04002CF9 RID: 11513
			CROSS_SLIDE_FLAGS_NONE = 0U,
			// Token: 0x04002CFA RID: 11514
			CROSS_SLIDE_FLAGS_SELECT = 1U,
			// Token: 0x04002CFB RID: 11515
			CROSS_SLIDE_FLAGS_SPEED_BUMP = 2U,
			// Token: 0x04002CFC RID: 11516
			CROSS_SLIDE_FLAGS_REARRANGE = 4U,
			// Token: 0x04002CFD RID: 11517
			CROSS_SLIDE_FLAGS_MAX = 4294967295U
		}

		// Token: 0x0200098D RID: 2445
		internal enum MANIPULATION_RAILS_STATE : uint
		{
			// Token: 0x04002CFF RID: 11519
			MANIPULATION_RAILS_STATE_UNDECIDED,
			// Token: 0x04002D00 RID: 11520
			MANIPULATION_RAILS_STATE_FREE,
			// Token: 0x04002D01 RID: 11521
			MANIPULATION_RAILS_STATE_RAILED,
			// Token: 0x04002D02 RID: 11522
			MANIPULATION_RAILS_STATE_MAX = 4294967295U
		}

		// Token: 0x0200098E RID: 2446
		[Flags]
		internal enum INTERACTION_FLAGS : uint
		{
			// Token: 0x04002D04 RID: 11524
			INTERACTION_FLAG_NONE = 0U,
			// Token: 0x04002D05 RID: 11525
			INTERACTION_FLAG_BEGIN = 1U,
			// Token: 0x04002D06 RID: 11526
			INTERACTION_FLAG_END = 2U,
			// Token: 0x04002D07 RID: 11527
			INTERACTION_FLAG_CANCEL = 4U,
			// Token: 0x04002D08 RID: 11528
			INTERACTION_FLAG_INERTIA = 8U,
			// Token: 0x04002D09 RID: 11529
			INTERACTION_FLAG_MAX = 4294967295U
		}

		// Token: 0x0200098F RID: 2447
		internal enum INTERACTION_ID : uint
		{
			// Token: 0x04002D0B RID: 11531
			INTERACTION_ID_NONE,
			// Token: 0x04002D0C RID: 11532
			INTERACTION_ID_MANIPULATION,
			// Token: 0x04002D0D RID: 11533
			INTERACTION_ID_TAP,
			// Token: 0x04002D0E RID: 11534
			INTERACTION_ID_SECONDARY_TAP,
			// Token: 0x04002D0F RID: 11535
			INTERACTION_ID_HOLD,
			// Token: 0x04002D10 RID: 11536
			INTERACTION_ID_DRAG,
			// Token: 0x04002D11 RID: 11537
			INTERACTION_ID_CROSS_SLIDE,
			// Token: 0x04002D12 RID: 11538
			INTERACTION_ID_MAX = 4294967295U
		}

		// Token: 0x02000990 RID: 2448
		[Flags]
		internal enum INTERACTION_CONFIGURATION_FLAGS : uint
		{
			// Token: 0x04002D14 RID: 11540
			INTERACTION_CONFIGURATION_FLAG_NONE = 0U,
			// Token: 0x04002D15 RID: 11541
			INTERACTION_CONFIGURATION_FLAG_MANIPULATION = 1U,
			// Token: 0x04002D16 RID: 11542
			INTERACTION_CONFIGURATION_FLAG_MANIPULATION_TRANSLATION_X = 2U,
			// Token: 0x04002D17 RID: 11543
			INTERACTION_CONFIGURATION_FLAG_MANIPULATION_TRANSLATION_Y = 4U,
			// Token: 0x04002D18 RID: 11544
			INTERACTION_CONFIGURATION_FLAG_MANIPULATION_ROTATION = 8U,
			// Token: 0x04002D19 RID: 11545
			INTERACTION_CONFIGURATION_FLAG_MANIPULATION_SCALING = 16U,
			// Token: 0x04002D1A RID: 11546
			INTERACTION_CONFIGURATION_FLAG_MANIPULATION_TRANSLATION_INERTIA = 32U,
			// Token: 0x04002D1B RID: 11547
			INTERACTION_CONFIGURATION_FLAG_MANIPULATION_ROTATION_INERTIA = 64U,
			// Token: 0x04002D1C RID: 11548
			INTERACTION_CONFIGURATION_FLAG_MANIPULATION_SCALING_INERTIA = 128U,
			// Token: 0x04002D1D RID: 11549
			INTERACTION_CONFIGURATION_FLAG_MANIPULATION_RAILS_X = 256U,
			// Token: 0x04002D1E RID: 11550
			INTERACTION_CONFIGURATION_FLAG_MANIPULATION_RAILS_Y = 512U,
			// Token: 0x04002D1F RID: 11551
			INTERACTION_CONFIGURATION_FLAG_MANIPULATION_EXACT = 1024U,
			// Token: 0x04002D20 RID: 11552
			INTERACTION_CONFIGURATION_FLAG_CROSS_SLIDE = 1U,
			// Token: 0x04002D21 RID: 11553
			INTERACTION_CONFIGURATION_FLAG_CROSS_SLIDE_HORIZONTAL = 2U,
			// Token: 0x04002D22 RID: 11554
			INTERACTION_CONFIGURATION_FLAG_CROSS_SLIDE_SELECT = 4U,
			// Token: 0x04002D23 RID: 11555
			INTERACTION_CONFIGURATION_FLAG_CROSS_SLIDE_SPEED_BUMP = 8U,
			// Token: 0x04002D24 RID: 11556
			INTERACTION_CONFIGURATION_FLAG_CROSS_SLIDE_REARRANGE = 16U,
			// Token: 0x04002D25 RID: 11557
			INTERACTION_CONFIGURATION_FLAG_CROSS_SLIDE_EXACT = 32U,
			// Token: 0x04002D26 RID: 11558
			INTERACTION_CONFIGURATION_FLAG_TAP = 1U,
			// Token: 0x04002D27 RID: 11559
			INTERACTION_CONFIGURATION_FLAG_TAP_DOUBLE = 2U,
			// Token: 0x04002D28 RID: 11560
			INTERACTION_CONFIGURATION_FLAG_SECONDARY_TAP = 1U,
			// Token: 0x04002D29 RID: 11561
			INTERACTION_CONFIGURATION_FLAG_HOLD = 1U,
			// Token: 0x04002D2A RID: 11562
			INTERACTION_CONFIGURATION_FLAG_HOLD_MOUSE = 2U,
			// Token: 0x04002D2B RID: 11563
			INTERACTION_CONFIGURATION_FLAG_DRAG = 1U,
			// Token: 0x04002D2C RID: 11564
			INTERACTION_CONFIGURATION_FLAG_MAX = 4294967295U
		}

		// Token: 0x02000991 RID: 2449
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct POINTER_TOUCH_INFO
		{
			// Token: 0x04002D2D RID: 11565
			internal UnsafeNativeMethods.POINTER_INFO pointerInfo;

			// Token: 0x04002D2E RID: 11566
			internal UnsafeNativeMethods.TOUCH_FLAGS touchFlags;

			// Token: 0x04002D2F RID: 11567
			internal UnsafeNativeMethods.TOUCH_MASK touchMask;

			// Token: 0x04002D30 RID: 11568
			internal UnsafeNativeMethods.RECT rcContact;

			// Token: 0x04002D31 RID: 11569
			internal UnsafeNativeMethods.RECT rcContactRaw;

			// Token: 0x04002D32 RID: 11570
			internal uint orientation;

			// Token: 0x04002D33 RID: 11571
			internal uint pressure;
		}

		// Token: 0x02000992 RID: 2450
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct POINTER_DEVICE_PROPERTY
		{
			// Token: 0x04002D34 RID: 11572
			internal int logicalMin;

			// Token: 0x04002D35 RID: 11573
			internal int logicalMax;

			// Token: 0x04002D36 RID: 11574
			internal int physicalMin;

			// Token: 0x04002D37 RID: 11575
			internal int physicalMax;

			// Token: 0x04002D38 RID: 11576
			internal uint unit;

			// Token: 0x04002D39 RID: 11577
			internal uint unitExponent;

			// Token: 0x04002D3A RID: 11578
			internal ushort usagePageId;

			// Token: 0x04002D3B RID: 11579
			internal ushort usageId;
		}

		// Token: 0x02000993 RID: 2451
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct POINTER_DEVICE_CURSOR_INFO
		{
			// Token: 0x04002D3C RID: 11580
			internal uint cursorId;

			// Token: 0x04002D3D RID: 11581
			internal UnsafeNativeMethods.POINTER_DEVICE_CURSOR_TYPE cursor;
		}

		// Token: 0x02000994 RID: 2452
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct POINTER_DEVICE_INFO
		{
			// Token: 0x04002D3E RID: 11582
			internal uint displayOrientation;

			// Token: 0x04002D3F RID: 11583
			internal IntPtr device;

			// Token: 0x04002D40 RID: 11584
			internal UnsafeNativeMethods.POINTER_DEVICE_TYPE pointerDeviceType;

			// Token: 0x04002D41 RID: 11585
			internal IntPtr monitor;

			// Token: 0x04002D42 RID: 11586
			internal uint startingCursorId;

			// Token: 0x04002D43 RID: 11587
			internal ushort maxActiveContacts;

			// Token: 0x04002D44 RID: 11588
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 520)]
			internal string productString;
		}

		// Token: 0x02000995 RID: 2453
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct POINT
		{
			// Token: 0x06005A37 RID: 23095 RVA: 0x0016B0E8 File Offset: 0x0016A4E8
			public override string ToString()
			{
				return string.Format("X: {0}, Y: {1}", this.X, this.Y);
			}

			// Token: 0x04002D45 RID: 11589
			internal int X;

			// Token: 0x04002D46 RID: 11590
			internal int Y;
		}

		// Token: 0x02000996 RID: 2454
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct POINTER_PEN_INFO
		{
			// Token: 0x04002D47 RID: 11591
			internal UnsafeNativeMethods.POINTER_INFO pointerInfo;

			// Token: 0x04002D48 RID: 11592
			internal UnsafeNativeMethods.PEN_FLAGS penFlags;

			// Token: 0x04002D49 RID: 11593
			internal UnsafeNativeMethods.PEN_MASK penMask;

			// Token: 0x04002D4A RID: 11594
			internal uint pressure;

			// Token: 0x04002D4B RID: 11595
			internal uint rotation;

			// Token: 0x04002D4C RID: 11596
			internal int tiltX;

			// Token: 0x04002D4D RID: 11597
			internal int tiltY;
		}

		// Token: 0x02000997 RID: 2455
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct POINTER_INFO
		{
			// Token: 0x04002D4E RID: 11598
			internal UnsafeNativeMethods.POINTER_INPUT_TYPE pointerType;

			// Token: 0x04002D4F RID: 11599
			internal uint pointerId;

			// Token: 0x04002D50 RID: 11600
			internal uint frameId;

			// Token: 0x04002D51 RID: 11601
			internal UnsafeNativeMethods.POINTER_FLAGS pointerFlags;

			// Token: 0x04002D52 RID: 11602
			internal IntPtr sourceDevice;

			// Token: 0x04002D53 RID: 11603
			internal IntPtr hwndTarget;

			// Token: 0x04002D54 RID: 11604
			internal UnsafeNativeMethods.POINT ptPixelLocation;

			// Token: 0x04002D55 RID: 11605
			internal UnsafeNativeMethods.POINT ptHimetricLocation;

			// Token: 0x04002D56 RID: 11606
			internal UnsafeNativeMethods.POINT ptPixelLocationRaw;

			// Token: 0x04002D57 RID: 11607
			internal UnsafeNativeMethods.POINT ptHimetricLocationRaw;

			// Token: 0x04002D58 RID: 11608
			internal uint dwTime;

			// Token: 0x04002D59 RID: 11609
			internal uint historyCount;

			// Token: 0x04002D5A RID: 11610
			internal int inputData;

			// Token: 0x04002D5B RID: 11611
			internal uint dwKeyStates;

			// Token: 0x04002D5C RID: 11612
			internal ulong PerformanceCount;

			// Token: 0x04002D5D RID: 11613
			internal UnsafeNativeMethods.POINTER_BUTTON_CHANGE_TYPE ButtonChangeType;
		}

		// Token: 0x02000998 RID: 2456
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct RECT
		{
			// Token: 0x04002D5E RID: 11614
			internal int left;

			// Token: 0x04002D5F RID: 11615
			internal int top;

			// Token: 0x04002D60 RID: 11616
			internal int right;

			// Token: 0x04002D61 RID: 11617
			internal int bottom;
		}

		// Token: 0x02000999 RID: 2457
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct MANIPULATION_VELOCITY
		{
			// Token: 0x04002D62 RID: 11618
			internal float velocityX;

			// Token: 0x04002D63 RID: 11619
			internal float velocityY;

			// Token: 0x04002D64 RID: 11620
			internal float velocityExpansion;

			// Token: 0x04002D65 RID: 11621
			internal float velocityAngular;
		}

		// Token: 0x0200099A RID: 2458
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct MANIPULATION_TRANSFORM
		{
			// Token: 0x04002D66 RID: 11622
			internal float translationX;

			// Token: 0x04002D67 RID: 11623
			internal float translationY;

			// Token: 0x04002D68 RID: 11624
			internal float scale;

			// Token: 0x04002D69 RID: 11625
			internal float expansion;

			// Token: 0x04002D6A RID: 11626
			internal float rotation;
		}

		// Token: 0x0200099B RID: 2459
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct INTERACTION_ARGUMENTS_MANIPULATION
		{
			// Token: 0x04002D6B RID: 11627
			internal UnsafeNativeMethods.MANIPULATION_TRANSFORM delta;

			// Token: 0x04002D6C RID: 11628
			internal UnsafeNativeMethods.MANIPULATION_TRANSFORM cumulative;

			// Token: 0x04002D6D RID: 11629
			internal UnsafeNativeMethods.MANIPULATION_VELOCITY velocity;

			// Token: 0x04002D6E RID: 11630
			internal UnsafeNativeMethods.MANIPULATION_RAILS_STATE railsState;
		}

		// Token: 0x0200099C RID: 2460
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct INTERACTION_CONTEXT_CONFIGURATION
		{
			// Token: 0x04002D6F RID: 11631
			internal UnsafeNativeMethods.INTERACTION_ID interactionId;

			// Token: 0x04002D70 RID: 11632
			internal UnsafeNativeMethods.INTERACTION_CONFIGURATION_FLAGS enable;
		}

		// Token: 0x0200099D RID: 2461
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct INTERACTION_ARGUMENTS_TAP
		{
			// Token: 0x04002D71 RID: 11633
			internal uint count;
		}

		// Token: 0x0200099E RID: 2462
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct INTERACTION_ARGUMENTS_CROSS_SLIDE
		{
			// Token: 0x04002D72 RID: 11634
			internal UnsafeNativeMethods.CROSS_SLIDE_FLAGS flags;
		}

		// Token: 0x0200099F RID: 2463
		[StructLayout(LayoutKind.Explicit)]
		internal struct INTERACTION_CONTEXT_OUTPUT_UNION
		{
			// Token: 0x04002D73 RID: 11635
			[FieldOffset(0)]
			internal UnsafeNativeMethods.INTERACTION_ARGUMENTS_MANIPULATION manipulation;

			// Token: 0x04002D74 RID: 11636
			[FieldOffset(0)]
			internal UnsafeNativeMethods.INTERACTION_ARGUMENTS_TAP tap;

			// Token: 0x04002D75 RID: 11637
			[FieldOffset(0)]
			internal UnsafeNativeMethods.INTERACTION_ARGUMENTS_CROSS_SLIDE crossSlide;
		}

		// Token: 0x020009A0 RID: 2464
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct INTERACTION_CONTEXT_OUTPUT
		{
			// Token: 0x04002D76 RID: 11638
			internal UnsafeNativeMethods.INTERACTION_ID interactionId;

			// Token: 0x04002D77 RID: 11639
			internal UnsafeNativeMethods.INTERACTION_FLAGS interactionFlags;

			// Token: 0x04002D78 RID: 11640
			internal UnsafeNativeMethods.POINTER_INPUT_TYPE inputType;

			// Token: 0x04002D79 RID: 11641
			internal float x;

			// Token: 0x04002D7A RID: 11642
			internal float y;

			// Token: 0x04002D7B RID: 11643
			internal UnsafeNativeMethods.INTERACTION_CONTEXT_OUTPUT_UNION arguments;
		}

		// Token: 0x020009A1 RID: 2465
		// (Invoke) Token: 0x06005A39 RID: 23097
		internal delegate void INTERACTION_CONTEXT_OUTPUT_CALLBACK(IntPtr clientData, ref UnsafeNativeMethods.INTERACTION_CONTEXT_OUTPUT output);
	}
}
