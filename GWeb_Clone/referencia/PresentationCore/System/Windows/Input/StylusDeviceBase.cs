using System;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Threading;

namespace System.Windows.Input
{
	// Token: 0x020002B2 RID: 690
	internal abstract class StylusDeviceBase : DispatcherObject, IDisposable
	{
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x0004B814 File Offset: 0x0004AC14
		// (set) Token: 0x06001464 RID: 5220 RVA: 0x0004B828 File Offset: 0x0004AC28
		internal StylusDevice StylusDevice { get; private set; }

		// Token: 0x06001465 RID: 5221 RVA: 0x0004B83C File Offset: 0x0004AC3C
		internal T As<T>() where T : StylusDeviceBase
		{
			return this as T;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0004B854 File Offset: 0x0004AC54
		protected StylusDeviceBase()
		{
			this.StylusDevice = new StylusDevice(this);
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0004B874 File Offset: 0x0004AC74
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001468 RID: 5224
		protected abstract void Dispose(bool disposing);

		// Token: 0x06001469 RID: 5225 RVA: 0x0004B890 File Offset: 0x0004AC90
		~StylusDeviceBase()
		{
			this.Dispose(false);
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x0600146A RID: 5226
		internal abstract IInputElement Target { get; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600146B RID: 5227
		internal abstract PresentationSource ActiveSource { get; }

		// Token: 0x0600146C RID: 5228
		internal abstract void UpdateEventStylusPoints(RawStylusInputReport report, bool resetIfNoOverride);

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x0600146D RID: 5229
		internal abstract PresentationSource CriticalActiveSource { get; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x0600146E RID: 5230
		internal abstract StylusPoint RawStylusPoint { get; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600146F RID: 5231
		internal abstract bool IsValid { get; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001470 RID: 5232
		internal abstract IInputElement DirectlyOver { get; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001471 RID: 5233
		internal abstract IInputElement Captured { get; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001472 RID: 5234
		internal abstract CaptureMode CapturedMode { get; }

		// Token: 0x06001473 RID: 5235
		internal abstract bool Capture(IInputElement element, CaptureMode captureMode);

		// Token: 0x06001474 RID: 5236
		internal abstract bool Capture(IInputElement element);

		// Token: 0x06001475 RID: 5237
		internal abstract void Synchronize();

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001476 RID: 5238
		internal abstract TabletDevice TabletDevice { get; }

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001477 RID: 5239
		internal abstract string Name { get; }

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001478 RID: 5240
		internal abstract int Id { get; }

		// Token: 0x06001479 RID: 5241
		internal abstract StylusPointCollection GetStylusPoints(IInputElement relativeTo);

		// Token: 0x0600147A RID: 5242
		internal abstract StylusPointCollection GetStylusPoints(IInputElement relativeTo, StylusPointDescription subsetToReformatTo);

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x0600147B RID: 5243
		internal abstract StylusButtonCollection StylusButtons { get; }

		// Token: 0x0600147C RID: 5244
		internal abstract Point GetPosition(IInputElement relativeTo);

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600147D RID: 5245
		internal abstract bool InAir { get; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x0600147E RID: 5246
		internal abstract bool Inverted { get; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600147F RID: 5247
		internal abstract bool InRange { get; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001480 RID: 5248
		internal abstract int DoubleTapDeltaX { get; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001481 RID: 5249
		internal abstract int DoubleTapDeltaY { get; }

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001482 RID: 5250
		internal abstract int DoubleTapDeltaTime { get; }

		// Token: 0x06001483 RID: 5251
		internal abstract Point GetMouseScreenPosition(MouseDevice mouseDevice);

		// Token: 0x06001484 RID: 5252
		internal abstract MouseButtonState GetMouseButtonState(MouseButton mouseButton, MouseDevice mouseDevice);

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001485 RID: 5253
		// (set) Token: 0x06001486 RID: 5254
		internal abstract int TapCount { get; set; }

		// Token: 0x06001487 RID: 5255
		internal abstract StylusPlugInCollection GetCapturedPlugInCollection(ref bool elementHasCapture);

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001488 RID: 5256
		// (set) Token: 0x06001489 RID: 5257
		internal abstract StylusPlugInCollection CurrentVerifiedTarget { get; set; }

		// Token: 0x04000AFF RID: 2815
		protected bool _disposed;
	}
}
