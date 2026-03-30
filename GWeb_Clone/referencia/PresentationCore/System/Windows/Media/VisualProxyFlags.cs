using System;

namespace System.Windows.Media
{
	// Token: 0x0200044E RID: 1102
	[Flags]
	internal enum VisualProxyFlags : uint
	{
		// Token: 0x040014B1 RID: 5297
		None = 0U,
		// Token: 0x040014B2 RID: 5298
		IsSubtreeDirtyForRender = 1U,
		// Token: 0x040014B3 RID: 5299
		IsTransformDirty = 2U,
		// Token: 0x040014B4 RID: 5300
		IsClipDirty = 4U,
		// Token: 0x040014B5 RID: 5301
		IsContentDirty = 8U,
		// Token: 0x040014B6 RID: 5302
		IsOpacityDirty = 16U,
		// Token: 0x040014B7 RID: 5303
		IsOpacityMaskDirty = 32U,
		// Token: 0x040014B8 RID: 5304
		IsOffsetDirty = 64U,
		// Token: 0x040014B9 RID: 5305
		IsClearTypeHintDirty = 128U,
		// Token: 0x040014BA RID: 5306
		IsGuidelineCollectionDirty = 256U,
		// Token: 0x040014BB RID: 5307
		IsEdgeModeDirty = 512U,
		// Token: 0x040014BC RID: 5308
		IsContentConnected = 1024U,
		// Token: 0x040014BD RID: 5309
		IsContentNodeConnected = 2048U,
		// Token: 0x040014BE RID: 5310
		IsConnectedToParent = 4096U,
		// Token: 0x040014BF RID: 5311
		Viewport3DVisual_IsCameraDirty = 8192U,
		// Token: 0x040014C0 RID: 5312
		Viewport3DVisual_IsViewportDirty = 16384U,
		// Token: 0x040014C1 RID: 5313
		IsBitmapScalingModeDirty = 32768U,
		// Token: 0x040014C2 RID: 5314
		IsDeleteResourceInProgress = 65536U,
		// Token: 0x040014C3 RID: 5315
		IsChildrenZOrderDirty = 131072U,
		// Token: 0x040014C4 RID: 5316
		IsEffectDirty = 262144U,
		// Token: 0x040014C5 RID: 5317
		IsCacheModeDirty = 524288U,
		// Token: 0x040014C6 RID: 5318
		IsScrollableAreaClipDirty = 1048576U,
		// Token: 0x040014C7 RID: 5319
		IsTextRenderingModeDirty = 2097152U,
		// Token: 0x040014C8 RID: 5320
		IsTextHintingModeDirty = 4194304U
	}
}
