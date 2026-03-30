using System;
using System.Collections.Generic;
using System.Windows.Media;
using MS.Internal.Ink;
using MS.Internal.PresentationCore;

namespace System.Windows.Ink
{
	// Token: 0x0200033A RID: 826
	[FriendAccessAllowed]
	internal class Renderer
	{
		// Token: 0x06001C0C RID: 7180 RVA: 0x000722E4 File Offset: 0x000716E4
		internal Renderer()
		{
			this._rootVisual = new ContainerVisual();
			this._highlightersRoot = new ContainerVisual();
			this._regularInkVisuals = new ContainerVisual();
			this._incrementalRenderingVisuals = new ContainerVisual();
			VisualCollection children = this._rootVisual.Children;
			children.Add(this._highlightersRoot);
			children.Add(this._regularInkVisuals);
			children.Add(this._incrementalRenderingVisuals);
			this._highContrast = false;
			this._visuals = new Dictionary<Stroke, Renderer.StrokeVisual>();
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001C0D RID: 7181 RVA: 0x00072374 File Offset: 0x00071774
		internal Visual RootVisual
		{
			get
			{
				return this._rootVisual;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001C0E RID: 7182 RVA: 0x00072388 File Offset: 0x00071788
		// (set) Token: 0x06001C0F RID: 7183 RVA: 0x000723C8 File Offset: 0x000717C8
		internal StrokeCollection Strokes
		{
			get
			{
				if (this._strokes == null)
				{
					this._strokes = new StrokeCollection();
					this._strokes.StrokesChangedInternal += this.OnStrokesChanged;
				}
				return this._strokes;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value == this._strokes)
				{
					return;
				}
				if (this._strokes != null)
				{
					this._strokes.StrokesChangedInternal -= this.OnStrokesChanged;
					foreach (Renderer.StrokeVisual strokeVisual in this._visuals.Values)
					{
						this.StopListeningOnStrokeEvents(strokeVisual.Stroke);
						this.DetachVisual(strokeVisual);
					}
					this._visuals.Clear();
				}
				this._strokes = value;
				foreach (Stroke stroke in this._strokes)
				{
					Renderer.StrokeVisual strokeVisual2 = new Renderer.StrokeVisual(stroke, this);
					this._visuals.Add(stroke, strokeVisual2);
					this.StartListeningOnStrokeEvents(strokeVisual2.Stroke);
					this.AttachVisual(strokeVisual2, true);
				}
				this._strokes.StrokesChangedInternal += this.OnStrokesChanged;
			}
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x00072504 File Offset: 0x00071904
		internal void AttachIncrementalRendering(Visual visual, DrawingAttributes drawingAttributes)
		{
			if (visual == null)
			{
				throw new ArgumentNullException("visual");
			}
			if (drawingAttributes == null)
			{
				throw new ArgumentNullException("drawingAttributes");
			}
			bool flag = false;
			if (this._attachedVisuals != null)
			{
				using (List<Visual>.Enumerator enumerator = this._attachedVisuals.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Visual visual2 = enumerator.Current;
						if (visual == visual2)
						{
							flag = true;
							throw new InvalidOperationException(SR.Get("CannotAttachVisualTwice"));
						}
					}
					goto IL_7C;
				}
			}
			this._attachedVisuals = new List<Visual>();
			IL_7C:
			if (!flag)
			{
				ContainerVisual containerVisual = drawingAttributes.IsHighlighter ? this.GetContainerVisual(drawingAttributes) : this._incrementalRenderingVisuals;
				containerVisual.Children.Add(visual);
				this._attachedVisuals.Add(visual);
			}
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x000725E0 File Offset: 0x000719E0
		internal void DetachIncrementalRendering(Visual visual)
		{
			if (visual == null)
			{
				throw new ArgumentNullException("visual");
			}
			if (this._attachedVisuals == null || !this._attachedVisuals.Remove(visual))
			{
				throw new InvalidOperationException(SR.Get("VisualCannotBeDetached"));
			}
			this.DetachVisual(visual);
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x00072628 File Offset: 0x00071A28
		internal bool ContainsAttachedIncrementalRenderingVisual(Visual visual)
		{
			return visual != null && this._attachedVisuals != null && this._attachedVisuals.Contains(visual);
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x00072650 File Offset: 0x00071A50
		internal bool AttachedVisualIsPositionedCorrectly(Visual visual, DrawingAttributes drawingAttributes)
		{
			if (visual == null || drawingAttributes == null || this._attachedVisuals == null || !this._attachedVisuals.Contains(visual))
			{
				return false;
			}
			ContainerVisual containerVisual = drawingAttributes.IsHighlighter ? this.GetContainerVisual(drawingAttributes) : this._incrementalRenderingVisuals;
			ContainerVisual containerVisual2 = VisualTreeHelper.GetParent(visual) as ContainerVisual;
			return containerVisual2 != null && containerVisual == containerVisual2;
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x000726B0 File Offset: 0x00071AB0
		internal void TurnHighContrastOn(Color strokeColor)
		{
			if (!this._highContrast || strokeColor != this._highContrastColor)
			{
				this._highContrast = true;
				this._highContrastColor = strokeColor;
				this.UpdateStrokeVisuals();
			}
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x000726E8 File Offset: 0x00071AE8
		internal void TurnHighContrastOff()
		{
			if (this._highContrast)
			{
				this._highContrast = false;
				this.UpdateStrokeVisuals();
			}
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x0007270C File Offset: 0x00071B0C
		internal bool IsHighContrast()
		{
			return this._highContrast;
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x00072720 File Offset: 0x00071B20
		public Color GetHighContrastColor()
		{
			return this._highContrastColor;
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x00072734 File Offset: 0x00071B34
		private void OnStrokesChanged(object sender, StrokeCollectionChangedEventArgs eventArgs)
		{
			StrokeCollection added = eventArgs.Added;
			StrokeCollection removed = eventArgs.Removed;
			foreach (Stroke stroke in added)
			{
				if (this._visuals.ContainsKey(stroke))
				{
					throw new ArgumentException(SR.Get("DuplicateStrokeAdded"));
				}
				Renderer.StrokeVisual strokeVisual = new Renderer.StrokeVisual(stroke, this);
				this._visuals.Add(stroke, strokeVisual);
				this.StartListeningOnStrokeEvents(strokeVisual.Stroke);
				this.AttachVisual(strokeVisual, false);
			}
			foreach (Stroke key in removed)
			{
				Renderer.StrokeVisual strokeVisual2 = null;
				if (!this._visuals.TryGetValue(key, out strokeVisual2))
				{
					throw new ArgumentException(SR.Get("UnknownStroke3"));
				}
				this.DetachVisual(strokeVisual2);
				this.StopListeningOnStrokeEvents(strokeVisual2.Stroke);
				this._visuals.Remove(key);
			}
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x00072860 File Offset: 0x00071C60
		private void OnStrokeInvalidated(object sender, EventArgs eventArgs)
		{
			Stroke stroke = (Stroke)sender;
			Renderer.StrokeVisual strokeVisual;
			if (!this._visuals.TryGetValue(stroke, out strokeVisual))
			{
				throw new ArgumentException(SR.Get("UnknownStroke1"));
			}
			if (strokeVisual.CachedIsHighlighter != stroke.DrawingAttributes.IsHighlighter || (stroke.DrawingAttributes.IsHighlighter && StrokeRenderer.GetHighlighterColor(strokeVisual.CachedColor) != StrokeRenderer.GetHighlighterColor(stroke.DrawingAttributes.Color)))
			{
				this.DetachVisual(strokeVisual);
				this.AttachVisual(strokeVisual, false);
				strokeVisual.CachedIsHighlighter = stroke.DrawingAttributes.IsHighlighter;
				strokeVisual.CachedColor = stroke.DrawingAttributes.Color;
			}
			strokeVisual.Update();
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00072910 File Offset: 0x00071D10
		private void UpdateStrokeVisuals()
		{
			foreach (Renderer.StrokeVisual strokeVisual in this._visuals.Values)
			{
				strokeVisual.Update();
			}
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x00072974 File Offset: 0x00071D74
		private void AttachVisual(Renderer.StrokeVisual visual, bool buildingStrokeCollection)
		{
			if (visual.Stroke.DrawingAttributes.IsHighlighter)
			{
				ContainerVisual containerVisual = this.GetContainerVisual(visual.Stroke.DrawingAttributes);
				int index = 0;
				for (int i = containerVisual.Children.Count - 1; i >= 0; i--)
				{
					if (containerVisual.Children[i] is Renderer.StrokeVisual)
					{
						index = i + 1;
						break;
					}
				}
				containerVisual.Children.Insert(index, visual);
				return;
			}
			Renderer.StrokeVisual strokeVisual = null;
			int num;
			if (buildingStrokeCollection)
			{
				Stroke stroke = visual.Stroke;
				num = Math.Min(this._visuals.Count, this._strokes.Count);
				while (--num >= 0)
				{
					if (this._strokes[num] == stroke)
					{
						break;
					}
				}
			}
			else
			{
				num = this._strokes.IndexOf(visual.Stroke);
			}
			while (--num >= 0)
			{
				Stroke stroke2 = this._strokes[num];
				if (!stroke2.DrawingAttributes.IsHighlighter && this._visuals.TryGetValue(stroke2, out strokeVisual) && VisualTreeHelper.GetParent(strokeVisual) != null)
				{
					VisualCollection children = ((ContainerVisual)VisualTreeHelper.GetParent(strokeVisual)).Children;
					int num2 = children.IndexOf(strokeVisual);
					children.Insert(num2 + 1, visual);
					break;
				}
			}
			if (num < 0)
			{
				ContainerVisual containerVisual2 = this.GetContainerVisual(visual.Stroke.DrawingAttributes);
				containerVisual2.Children.Insert(0, visual);
			}
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x00072AD4 File Offset: 0x00071ED4
		private void DetachVisual(Visual visual)
		{
			ContainerVisual containerVisual = (ContainerVisual)VisualTreeHelper.GetParent(visual);
			if (containerVisual != null)
			{
				VisualCollection children = containerVisual.Children;
				children.Remove(visual);
				Renderer.HighlighterContainerVisual highlighterContainerVisual = containerVisual as Renderer.HighlighterContainerVisual;
				if (highlighterContainerVisual != null && highlighterContainerVisual.Children.Count == 0 && this._highlighters != null && this._highlighters.ContainsValue(highlighterContainerVisual))
				{
					this.DetachVisual(highlighterContainerVisual);
					this._highlighters.Remove(highlighterContainerVisual.Color);
				}
			}
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x00072B44 File Offset: 0x00071F44
		private void StartListeningOnStrokeEvents(Stroke stroke)
		{
			stroke.Invalidated += this.OnStrokeInvalidated;
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x00072B64 File Offset: 0x00071F64
		private void StopListeningOnStrokeEvents(Stroke stroke)
		{
			stroke.Invalidated -= this.OnStrokeInvalidated;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x00072B84 File Offset: 0x00071F84
		private ContainerVisual GetContainerVisual(DrawingAttributes drawingAttributes)
		{
			if (drawingAttributes.IsHighlighter)
			{
				Color highlighterColor = StrokeRenderer.GetHighlighterColor(drawingAttributes.Color);
				Renderer.HighlighterContainerVisual highlighterContainerVisual;
				if (this._highlighters == null || !this._highlighters.TryGetValue(highlighterColor, out highlighterContainerVisual))
				{
					if (this._highlighters == null)
					{
						this._highlighters = new Dictionary<Color, Renderer.HighlighterContainerVisual>();
					}
					highlighterContainerVisual = new Renderer.HighlighterContainerVisual(highlighterColor);
					highlighterContainerVisual.Opacity = StrokeRenderer.HighlighterOpacity;
					this._highlightersRoot.Children.Add(highlighterContainerVisual);
					this._highlighters.Add(highlighterColor, highlighterContainerVisual);
				}
				else if (VisualTreeHelper.GetParent(highlighterContainerVisual) == null)
				{
					this._highlightersRoot.Children.Add(highlighterContainerVisual);
				}
				return highlighterContainerVisual;
			}
			return this._regularInkVisuals;
		}

		// Token: 0x04000F30 RID: 3888
		private ContainerVisual _rootVisual;

		// Token: 0x04000F31 RID: 3889
		private ContainerVisual _highlightersRoot;

		// Token: 0x04000F32 RID: 3890
		private ContainerVisual _incrementalRenderingVisuals;

		// Token: 0x04000F33 RID: 3891
		private ContainerVisual _regularInkVisuals;

		// Token: 0x04000F34 RID: 3892
		private Dictionary<Stroke, Renderer.StrokeVisual> _visuals;

		// Token: 0x04000F35 RID: 3893
		private Dictionary<Color, Renderer.HighlighterContainerVisual> _highlighters;

		// Token: 0x04000F36 RID: 3894
		private StrokeCollection _strokes;

		// Token: 0x04000F37 RID: 3895
		private List<Visual> _attachedVisuals;

		// Token: 0x04000F38 RID: 3896
		private bool _highContrast;

		// Token: 0x04000F39 RID: 3897
		private Color _highContrastColor = Colors.White;

		// Token: 0x02000852 RID: 2130
		private class StrokeVisual : DrawingVisual
		{
			// Token: 0x060056FE RID: 22270 RVA: 0x001642CC File Offset: 0x001636CC
			internal StrokeVisual(Stroke stroke, Renderer renderer)
			{
				if (stroke == null)
				{
					throw new ArgumentNullException("stroke");
				}
				this._stroke = stroke;
				this._renderer = renderer;
				this._cachedColor = stroke.DrawingAttributes.Color;
				this._cachedIsHighlighter = stroke.DrawingAttributes.IsHighlighter;
				this.Update();
			}

			// Token: 0x170011E5 RID: 4581
			// (get) Token: 0x060056FF RID: 22271 RVA: 0x00164324 File Offset: 0x00163724
			internal Stroke Stroke
			{
				get
				{
					return this._stroke;
				}
			}

			// Token: 0x06005700 RID: 22272 RVA: 0x00164338 File Offset: 0x00163738
			internal void Update()
			{
				using (DrawingContext drawingContext = base.RenderOpen())
				{
					bool flag = this._renderer.IsHighContrast();
					if (!flag || !this._stroke.DrawingAttributes.IsHighlighter)
					{
						DrawingAttributes drawingAttributes;
						if (flag)
						{
							drawingAttributes = this._stroke.DrawingAttributes.Clone();
							drawingAttributes.Color = this._renderer.GetHighContrastColor();
						}
						else if (this._stroke.DrawingAttributes.IsHighlighter)
						{
							drawingAttributes = StrokeRenderer.GetHighlighterAttributes(this._stroke, this._stroke.DrawingAttributes);
						}
						else
						{
							drawingAttributes = this._stroke.DrawingAttributes;
						}
						this._stroke.DrawInternal(drawingContext, drawingAttributes, this._stroke.IsSelected);
					}
				}
			}

			// Token: 0x06005701 RID: 22273 RVA: 0x00164410 File Offset: 0x00163810
			protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParams)
			{
				return null;
			}

			// Token: 0x170011E6 RID: 4582
			// (get) Token: 0x06005702 RID: 22274 RVA: 0x00164420 File Offset: 0x00163820
			// (set) Token: 0x06005703 RID: 22275 RVA: 0x00164434 File Offset: 0x00163834
			internal bool CachedIsHighlighter
			{
				get
				{
					return this._cachedIsHighlighter;
				}
				set
				{
					this._cachedIsHighlighter = value;
				}
			}

			// Token: 0x170011E7 RID: 4583
			// (get) Token: 0x06005704 RID: 22276 RVA: 0x00164448 File Offset: 0x00163848
			// (set) Token: 0x06005705 RID: 22277 RVA: 0x0016445C File Offset: 0x0016385C
			internal Color CachedColor
			{
				get
				{
					return this._cachedColor;
				}
				set
				{
					this._cachedColor = value;
				}
			}

			// Token: 0x04002811 RID: 10257
			private Stroke _stroke;

			// Token: 0x04002812 RID: 10258
			private bool _cachedIsHighlighter;

			// Token: 0x04002813 RID: 10259
			private Color _cachedColor;

			// Token: 0x04002814 RID: 10260
			private Renderer _renderer;
		}

		// Token: 0x02000853 RID: 2131
		private class HighlighterContainerVisual : ContainerVisual
		{
			// Token: 0x06005706 RID: 22278 RVA: 0x00164470 File Offset: 0x00163870
			internal HighlighterContainerVisual(Color color)
			{
				this._color = color;
			}

			// Token: 0x170011E8 RID: 4584
			// (get) Token: 0x06005707 RID: 22279 RVA: 0x0016448C File Offset: 0x0016388C
			internal Color Color
			{
				get
				{
					return this._color;
				}
			}

			// Token: 0x04002815 RID: 10261
			private Color _color;
		}
	}
}
