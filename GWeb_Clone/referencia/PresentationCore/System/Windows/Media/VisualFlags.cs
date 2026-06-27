using System;

namespace System.Windows.Media
{
	// Token: 0x0200044B RID: 1099
	[Flags]
	internal enum VisualFlags : uint
	{
		// Token: 0x04001489 RID: 5257
		None = 0U,
		// Token: 0x0400148A RID: 5258
		IsSubtreeDirtyForPrecompute = 1U,
		// Token: 0x0400148B RID: 5259
		ShouldPostRender = 2U,
		// Token: 0x0400148C RID: 5260
		IsUIElement = 4U,
		// Token: 0x0400148D RID: 5261
		IsLayoutSuspended = 8U,
		// Token: 0x0400148E RID: 5262
		IsVisualChildrenIterationInProgress = 16U,
		// Token: 0x0400148F RID: 5263
		Are3DContentBoundsValid = 32U,
		// Token: 0x04001490 RID: 5264
		FindCommonAncestor = 64U,
		// Token: 0x04001491 RID: 5265
		IsLayoutIslandRoot = 128U,
		// Token: 0x04001492 RID: 5266
		UseLayoutRounding = 256U,
		// Token: 0x04001493 RID: 5267
		VisibilityCache_Visible = 512U,
		// Token: 0x04001494 RID: 5268
		VisibilityCache_TakesSpace = 1024U,
		// Token: 0x04001495 RID: 5269
		RegisteredForAncestorChanged = 2048U,
		// Token: 0x04001496 RID: 5270
		SubTreeHoldsAncestorChanged = 4096U,
		// Token: 0x04001497 RID: 5271
		NodeIsCyclicBrushRoot = 8192U,
		// Token: 0x04001498 RID: 5272
		NodeHasEffect = 16384U,
		// Token: 0x04001499 RID: 5273
		IsViewport3DVisual = 32768U,
		// Token: 0x0400149A RID: 5274
		ReentrancyFlag = 65536U,
		// Token: 0x0400149B RID: 5275
		HasChildren = 131072U,
		// Token: 0x0400149C RID: 5276
		BitmapEffectEmulationDisabled = 262144U,
		// Token: 0x0400149D RID: 5277
		DpiScaleFlag1 = 524288U,
		// Token: 0x0400149E RID: 5278
		DpiScaleFlag2 = 1048576U,
		// Token: 0x0400149F RID: 5279
		TreeLevelBit0 = 2097152U,
		// Token: 0x040014A0 RID: 5280
		TreeLevelBit1 = 4194304U,
		// Token: 0x040014A1 RID: 5281
		TreeLevelBit2 = 8388608U,
		// Token: 0x040014A2 RID: 5282
		TreeLevelBit3 = 16777216U,
		// Token: 0x040014A3 RID: 5283
		TreeLevelBit4 = 33554432U,
		// Token: 0x040014A4 RID: 5284
		TreeLevelBit5 = 67108864U,
		// Token: 0x040014A5 RID: 5285
		TreeLevelBit6 = 134217728U,
		// Token: 0x040014A6 RID: 5286
		TreeLevelBit7 = 268435456U,
		// Token: 0x040014A7 RID: 5287
		TreeLevelBit8 = 536870912U,
		// Token: 0x040014A8 RID: 5288
		TreeLevelBit9 = 1073741824U,
		// Token: 0x040014A9 RID: 5289
		TreeLevelBit10 = 2147483648U
	}
}
