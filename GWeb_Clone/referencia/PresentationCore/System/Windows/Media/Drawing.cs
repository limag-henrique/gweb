using System;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;

namespace System.Windows.Media
{
	/// <summary>Classe abstrata que descreve um desenho 2D. Essa classe não pode ser herdada por seu código.</summary>
	// Token: 0x0200037E RID: 894
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public abstract class Drawing : Animatable, IDrawingContent, DUCE.IResource
	{
		// Token: 0x06002068 RID: 8296 RVA: 0x00084398 File Offset: 0x00083798
		internal Drawing()
		{
		}

		/// <summary>Obtém os limites alinhados por eixo do conteúdo do desenho.</summary>
		/// <returns>Os limites alinhada por eixo do conteúdo do desenho.</returns>
		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06002069 RID: 8297 RVA: 0x000843AC File Offset: 0x000837AC
		public Rect Bounds
		{
			get
			{
				base.ReadPreamble();
				return this.GetBounds();
			}
		}

		// Token: 0x0600206A RID: 8298
		internal abstract void WalkCurrentValue(DrawingContextWalker ctx);

		// Token: 0x0600206B RID: 8299 RVA: 0x000843C8 File Offset: 0x000837C8
		Rect IDrawingContent.GetContentBounds(BoundsDrawingContextWalker ctx)
		{
			this.WalkCurrentValue(ctx);
			return ctx.Bounds;
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x000843E4 File Offset: 0x000837E4
		void IDrawingContent.WalkContent(DrawingContextWalker ctx)
		{
			this.WalkCurrentValue(ctx);
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x000843F8 File Offset: 0x000837F8
		bool IDrawingContent.HitTestPoint(Point point)
		{
			return DrawingServices.HitTestPoint(this, point);
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x0008440C File Offset: 0x0008380C
		IntersectionDetail IDrawingContent.HitTestGeometry(PathGeometry geometry)
		{
			return DrawingServices.HitTestGeometry(this, geometry);
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x00084420 File Offset: 0x00083820
		void IDrawingContent.PropagateChangedHandler(EventHandler handler, bool adding)
		{
			if (!base.IsFrozen)
			{
				if (adding)
				{
					this.Changed += handler;
					return;
				}
				this.Changed -= handler;
			}
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x00084448 File Offset: 0x00083848
		internal Rect GetBounds()
		{
			BoundsDrawingContextWalker boundsDrawingContextWalker = new BoundsDrawingContextWalker();
			this.WalkCurrentValue(boundsDrawingContextWalker);
			return boundsDrawingContextWalker.Bounds;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Drawing" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002071 RID: 8305 RVA: 0x00084468 File Offset: 0x00083868
		public new Drawing Clone()
		{
			return (Drawing)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Drawing" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002072 RID: 8306 RVA: 0x00084480 File Offset: 0x00083880
		public new Drawing CloneCurrentValue()
		{
			return (Drawing)base.CloneCurrentValue();
		}

		// Token: 0x06002073 RID: 8307
		internal abstract DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel);

		// Token: 0x06002074 RID: 8308 RVA: 0x00084498 File Offset: 0x00083898
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			DUCE.ResourceHandle result;
			using (CompositionEngineLock.Acquire())
			{
				result = this.AddRefOnChannelCore(channel);
			}
			return result;
		}

		// Token: 0x06002075 RID: 8309
		internal abstract void ReleaseOnChannelCore(DUCE.Channel channel);

		// Token: 0x06002076 RID: 8310 RVA: 0x000844E0 File Offset: 0x000838E0
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			using (CompositionEngineLock.Acquire())
			{
				this.ReleaseOnChannelCore(channel);
			}
		}

		// Token: 0x06002077 RID: 8311
		internal abstract DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel);

		// Token: 0x06002078 RID: 8312 RVA: 0x00084528 File Offset: 0x00083928
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handleCore;
			using (CompositionEngineLock.Acquire())
			{
				handleCore = this.GetHandleCore(channel);
			}
			return handleCore;
		}

		// Token: 0x06002079 RID: 8313
		internal abstract int GetChannelCountCore();

		// Token: 0x0600207A RID: 8314 RVA: 0x00084570 File Offset: 0x00083970
		int DUCE.IResource.GetChannelCount()
		{
			return this.GetChannelCountCore();
		}

		// Token: 0x0600207B RID: 8315
		internal abstract DUCE.Channel GetChannelCore(int index);

		// Token: 0x0600207C RID: 8316 RVA: 0x00084584 File Offset: 0x00083984
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this.GetChannelCore(index);
		}
	}
}
