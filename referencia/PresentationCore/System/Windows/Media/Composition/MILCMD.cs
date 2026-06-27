using System;

namespace System.Windows.Media.Composition
{
	// Token: 0x02000634 RID: 1588
	internal enum MILCMD
	{
		// Token: 0x04001AEF RID: 6895
		MilCmdInvalid,
		// Token: 0x04001AF0 RID: 6896
		MilCmdTransportSyncFlush,
		// Token: 0x04001AF1 RID: 6897
		MilCmdTransportDestroyResourcesOnChannel,
		// Token: 0x04001AF2 RID: 6898
		MilCmdPartitionRegisterForNotifications,
		// Token: 0x04001AF3 RID: 6899
		MilCmdChannelRequestTier,
		// Token: 0x04001AF4 RID: 6900
		MilCmdPartitionSetVBlankSyncMode,
		// Token: 0x04001AF5 RID: 6901
		MilCmdPartitionNotifyPresent,
		// Token: 0x04001AF6 RID: 6902
		MilCmdChannelCreateResource,
		// Token: 0x04001AF7 RID: 6903
		MilCmdChannelDeleteResource,
		// Token: 0x04001AF8 RID: 6904
		MilCmdChannelDuplicateHandle,
		// Token: 0x04001AF9 RID: 6905
		MilCmdD3DImage,
		// Token: 0x04001AFA RID: 6906
		MilCmdD3DImagePresent,
		// Token: 0x04001AFB RID: 6907
		MilCmdBitmapSource,
		// Token: 0x04001AFC RID: 6908
		MilCmdBitmapInvalidate,
		// Token: 0x04001AFD RID: 6909
		MilCmdDoubleResource,
		// Token: 0x04001AFE RID: 6910
		MilCmdColorResource,
		// Token: 0x04001AFF RID: 6911
		MilCmdPointResource,
		// Token: 0x04001B00 RID: 6912
		MilCmdRectResource,
		// Token: 0x04001B01 RID: 6913
		MilCmdSizeResource,
		// Token: 0x04001B02 RID: 6914
		MilCmdMatrixResource,
		// Token: 0x04001B03 RID: 6915
		MilCmdPoint3DResource,
		// Token: 0x04001B04 RID: 6916
		MilCmdVector3DResource,
		// Token: 0x04001B05 RID: 6917
		MilCmdQuaternionResource,
		// Token: 0x04001B06 RID: 6918
		MilCmdMediaPlayer,
		// Token: 0x04001B07 RID: 6919
		MilCmdRenderData,
		// Token: 0x04001B08 RID: 6920
		MilCmdEtwEventResource,
		// Token: 0x04001B09 RID: 6921
		MilCmdVisualCreate,
		// Token: 0x04001B0A RID: 6922
		MilCmdVisualSetOffset,
		// Token: 0x04001B0B RID: 6923
		MilCmdVisualSetTransform,
		// Token: 0x04001B0C RID: 6924
		MilCmdVisualSetEffect,
		// Token: 0x04001B0D RID: 6925
		MilCmdVisualSetCacheMode,
		// Token: 0x04001B0E RID: 6926
		MilCmdVisualSetClip,
		// Token: 0x04001B0F RID: 6927
		MilCmdVisualSetAlpha,
		// Token: 0x04001B10 RID: 6928
		MilCmdVisualSetRenderOptions,
		// Token: 0x04001B11 RID: 6929
		MilCmdVisualSetContent,
		// Token: 0x04001B12 RID: 6930
		MilCmdVisualSetAlphaMask,
		// Token: 0x04001B13 RID: 6931
		MilCmdVisualRemoveAllChildren,
		// Token: 0x04001B14 RID: 6932
		MilCmdVisualRemoveChild,
		// Token: 0x04001B15 RID: 6933
		MilCmdVisualInsertChildAt,
		// Token: 0x04001B16 RID: 6934
		MilCmdVisualSetGuidelineCollection,
		// Token: 0x04001B17 RID: 6935
		MilCmdVisualSetScrollableAreaClip,
		// Token: 0x04001B18 RID: 6936
		MilCmdViewport3DVisualSetCamera,
		// Token: 0x04001B19 RID: 6937
		MilCmdViewport3DVisualSetViewport,
		// Token: 0x04001B1A RID: 6938
		MilCmdViewport3DVisualSet3DChild,
		// Token: 0x04001B1B RID: 6939
		MilCmdVisual3DSetContent,
		// Token: 0x04001B1C RID: 6940
		MilCmdVisual3DSetTransform,
		// Token: 0x04001B1D RID: 6941
		MilCmdVisual3DRemoveAllChildren,
		// Token: 0x04001B1E RID: 6942
		MilCmdVisual3DRemoveChild,
		// Token: 0x04001B1F RID: 6943
		MilCmdVisual3DInsertChildAt,
		// Token: 0x04001B20 RID: 6944
		MilCmdHwndTargetCreate,
		// Token: 0x04001B21 RID: 6945
		MilCmdHwndTargetSuppressLayered,
		// Token: 0x04001B22 RID: 6946
		MilCmdTargetUpdateWindowSettings,
		// Token: 0x04001B23 RID: 6947
		MilCmdGenericTargetCreate,
		// Token: 0x04001B24 RID: 6948
		MilCmdTargetSetRoot,
		// Token: 0x04001B25 RID: 6949
		MilCmdTargetSetClearColor,
		// Token: 0x04001B26 RID: 6950
		MilCmdTargetInvalidate,
		// Token: 0x04001B27 RID: 6951
		MilCmdTargetSetFlags,
		// Token: 0x04001B28 RID: 6952
		MilCmdHwndTargetDpiChanged,
		// Token: 0x04001B29 RID: 6953
		MilCmdGlyphRunCreate,
		// Token: 0x04001B2A RID: 6954
		MilCmdDoubleBufferedBitmap,
		// Token: 0x04001B2B RID: 6955
		MilCmdDoubleBufferedBitmapCopyForward,
		// Token: 0x04001B2C RID: 6956
		MilCmdPartitionNotifyPolicyChangeForNonInteractiveMode,
		// Token: 0x04001B2D RID: 6957
		MilDrawLine,
		// Token: 0x04001B2E RID: 6958
		MilDrawLineAnimate,
		// Token: 0x04001B2F RID: 6959
		MilDrawRectangle,
		// Token: 0x04001B30 RID: 6960
		MilDrawRectangleAnimate,
		// Token: 0x04001B31 RID: 6961
		MilDrawRoundedRectangle,
		// Token: 0x04001B32 RID: 6962
		MilDrawRoundedRectangleAnimate,
		// Token: 0x04001B33 RID: 6963
		MilDrawEllipse,
		// Token: 0x04001B34 RID: 6964
		MilDrawEllipseAnimate,
		// Token: 0x04001B35 RID: 6965
		MilDrawGeometry,
		// Token: 0x04001B36 RID: 6966
		MilDrawImage,
		// Token: 0x04001B37 RID: 6967
		MilDrawImageAnimate,
		// Token: 0x04001B38 RID: 6968
		MilDrawGlyphRun,
		// Token: 0x04001B39 RID: 6969
		MilDrawDrawing,
		// Token: 0x04001B3A RID: 6970
		MilDrawVideo,
		// Token: 0x04001B3B RID: 6971
		MilDrawVideoAnimate,
		// Token: 0x04001B3C RID: 6972
		MilPushClip,
		// Token: 0x04001B3D RID: 6973
		MilPushOpacityMask,
		// Token: 0x04001B3E RID: 6974
		MilPushOpacity,
		// Token: 0x04001B3F RID: 6975
		MilPushOpacityAnimate,
		// Token: 0x04001B40 RID: 6976
		MilPushTransform,
		// Token: 0x04001B41 RID: 6977
		MilPushGuidelineSet,
		// Token: 0x04001B42 RID: 6978
		MilPushGuidelineY1,
		// Token: 0x04001B43 RID: 6979
		MilPushGuidelineY2,
		// Token: 0x04001B44 RID: 6980
		MilPushEffect,
		// Token: 0x04001B45 RID: 6981
		MilPop,
		// Token: 0x04001B46 RID: 6982
		MilCmdAxisAngleRotation3D,
		// Token: 0x04001B47 RID: 6983
		MilCmdQuaternionRotation3D,
		// Token: 0x04001B48 RID: 6984
		MilCmdPerspectiveCamera,
		// Token: 0x04001B49 RID: 6985
		MilCmdOrthographicCamera,
		// Token: 0x04001B4A RID: 6986
		MilCmdMatrixCamera,
		// Token: 0x04001B4B RID: 6987
		MilCmdModel3DGroup,
		// Token: 0x04001B4C RID: 6988
		MilCmdAmbientLight,
		// Token: 0x04001B4D RID: 6989
		MilCmdDirectionalLight,
		// Token: 0x04001B4E RID: 6990
		MilCmdPointLight,
		// Token: 0x04001B4F RID: 6991
		MilCmdSpotLight,
		// Token: 0x04001B50 RID: 6992
		MilCmdGeometryModel3D,
		// Token: 0x04001B51 RID: 6993
		MilCmdMeshGeometry3D,
		// Token: 0x04001B52 RID: 6994
		MilCmdMaterialGroup,
		// Token: 0x04001B53 RID: 6995
		MilCmdDiffuseMaterial,
		// Token: 0x04001B54 RID: 6996
		MilCmdSpecularMaterial,
		// Token: 0x04001B55 RID: 6997
		MilCmdEmissiveMaterial,
		// Token: 0x04001B56 RID: 6998
		MilCmdTransform3DGroup,
		// Token: 0x04001B57 RID: 6999
		MilCmdTranslateTransform3D,
		// Token: 0x04001B58 RID: 7000
		MilCmdScaleTransform3D,
		// Token: 0x04001B59 RID: 7001
		MilCmdRotateTransform3D,
		// Token: 0x04001B5A RID: 7002
		MilCmdMatrixTransform3D,
		// Token: 0x04001B5B RID: 7003
		MilCmdPixelShader,
		// Token: 0x04001B5C RID: 7004
		MilCmdImplicitInputBrush,
		// Token: 0x04001B5D RID: 7005
		MilCmdBlurEffect,
		// Token: 0x04001B5E RID: 7006
		MilCmdDropShadowEffect,
		// Token: 0x04001B5F RID: 7007
		MilCmdShaderEffect,
		// Token: 0x04001B60 RID: 7008
		MilCmdDrawingImage,
		// Token: 0x04001B61 RID: 7009
		MilCmdTransformGroup,
		// Token: 0x04001B62 RID: 7010
		MilCmdTranslateTransform,
		// Token: 0x04001B63 RID: 7011
		MilCmdScaleTransform,
		// Token: 0x04001B64 RID: 7012
		MilCmdSkewTransform,
		// Token: 0x04001B65 RID: 7013
		MilCmdRotateTransform,
		// Token: 0x04001B66 RID: 7014
		MilCmdMatrixTransform,
		// Token: 0x04001B67 RID: 7015
		MilCmdLineGeometry,
		// Token: 0x04001B68 RID: 7016
		MilCmdRectangleGeometry,
		// Token: 0x04001B69 RID: 7017
		MilCmdEllipseGeometry,
		// Token: 0x04001B6A RID: 7018
		MilCmdGeometryGroup,
		// Token: 0x04001B6B RID: 7019
		MilCmdCombinedGeometry,
		// Token: 0x04001B6C RID: 7020
		MilCmdPathGeometry,
		// Token: 0x04001B6D RID: 7021
		MilCmdSolidColorBrush,
		// Token: 0x04001B6E RID: 7022
		MilCmdLinearGradientBrush,
		// Token: 0x04001B6F RID: 7023
		MilCmdRadialGradientBrush,
		// Token: 0x04001B70 RID: 7024
		MilCmdImageBrush,
		// Token: 0x04001B71 RID: 7025
		MilCmdDrawingBrush,
		// Token: 0x04001B72 RID: 7026
		MilCmdVisualBrush,
		// Token: 0x04001B73 RID: 7027
		MilCmdBitmapCacheBrush,
		// Token: 0x04001B74 RID: 7028
		MilCmdDashStyle,
		// Token: 0x04001B75 RID: 7029
		MilCmdPen,
		// Token: 0x04001B76 RID: 7030
		MilCmdGeometryDrawing,
		// Token: 0x04001B77 RID: 7031
		MilCmdGlyphRunDrawing,
		// Token: 0x04001B78 RID: 7032
		MilCmdImageDrawing,
		// Token: 0x04001B79 RID: 7033
		MilCmdVideoDrawing,
		// Token: 0x04001B7A RID: 7034
		MilCmdDrawingGroup,
		// Token: 0x04001B7B RID: 7035
		MilCmdGuidelineSet,
		// Token: 0x04001B7C RID: 7036
		MilCmdBitmapCache
	}
}
