using System;
using System.Security;
using System.Windows.Media.Composition;

namespace System.Windows.Media
{
	// Token: 0x02000435 RID: 1077
	internal static class Renderer
	{
		// Token: 0x06002C1F RID: 11295 RVA: 0x000B0300 File Offset: 0x000AF700
		[SecurityCritical]
		public static void Render(IntPtr pRenderTarget, DUCE.Channel channel, Visual visual, int width, int height, double dpiX, double dpiY)
		{
			Renderer.Render(pRenderTarget, channel, visual, width, height, dpiX, dpiY, Matrix.Identity, Rect.Empty);
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x000B0328 File Offset: 0x000AF728
		[SecurityCritical]
		internal static void Render(IntPtr pRenderTarget, DUCE.Channel channel, Visual visual, int width, int height, double dpiX, double dpiY, Matrix worldTransform, Rect windowClip)
		{
			DUCE.Resource resource = default(DUCE.Resource);
			DUCE.Resource resource2 = default(DUCE.Resource);
			DUCE.ResourceHandle hCompositionTarget = DUCE.ResourceHandle.Null;
			DUCE.ResourceHandle resourceHandle = DUCE.ResourceHandle.Null;
			Matrix matrix = new Matrix(dpiX * 0.010416666666666666, 0.0, 0.0, dpiY * 0.010416666666666666, 0.0, 0.0);
			matrix = worldTransform * matrix;
			MatrixTransform matrixTransform = new MatrixTransform(matrix);
			DUCE.ResourceHandle hTransform = ((DUCE.IResource)matrixTransform).AddRefOnChannel(channel);
			try
			{
				resource.CreateOrAddRefOnChannel(resource, channel, DUCE.ResourceType.TYPE_GENERICRENDERTARGET);
				hCompositionTarget = resource.Handle;
				DUCE.CompositionTarget.PrintInitialize(hCompositionTarget, pRenderTarget, width, height, channel);
				resource2.CreateOrAddRefOnChannel(resource2, channel, DUCE.ResourceType.TYPE_VISUAL);
				resourceHandle = resource2.Handle;
				DUCE.CompositionNode.SetTransform(resourceHandle, hTransform, channel);
				DUCE.CompositionTarget.SetRoot(hCompositionTarget, resourceHandle, channel);
				channel.CloseBatch();
				channel.Commit();
				RenderContext renderContext = new RenderContext();
				renderContext.Initialize(channel, resourceHandle);
				visual.Precompute();
				visual.Render(renderContext, 0U);
				channel.CloseBatch();
				channel.Commit();
				channel.Present();
				MediaContext currentMediaContext = MediaContext.CurrentMediaContext;
				currentMediaContext.NotifySyncChannelMessage(channel);
			}
			finally
			{
				if (!resourceHandle.IsNull)
				{
					DUCE.CompositionNode.RemoveAllChildren(resourceHandle, channel);
					((DUCE.IResource)visual).ReleaseOnChannel(channel);
					resource2.ReleaseOnChannel(channel);
				}
				if (!hTransform.IsNull)
				{
					((DUCE.IResource)matrixTransform).ReleaseOnChannel(channel);
				}
				if (!hCompositionTarget.IsNull)
				{
					DUCE.CompositionTarget.SetRoot(hCompositionTarget, DUCE.ResourceHandle.Null, channel);
					resource.ReleaseOnChannel(channel);
				}
				channel.CloseBatch();
				channel.Commit();
				channel.Present();
				MediaContext currentMediaContext2 = MediaContext.CurrentMediaContext;
				currentMediaContext2.NotifySyncChannelMessage(channel);
			}
		}
	}
}
