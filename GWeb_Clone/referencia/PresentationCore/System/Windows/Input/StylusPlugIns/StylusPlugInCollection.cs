using System;
using System.Collections.ObjectModel;
using System.Security;
using System.Windows.Media;
using MS.Internal.PresentationCore;

namespace System.Windows.Input.StylusPlugIns
{
	/// <summary>Representa uma coleção de objetos <see cref="T:System.Windows.Input.StylusPlugIns.StylusPlugIn" />.</summary>
	// Token: 0x020002FB RID: 763
	public sealed class StylusPlugInCollection : Collection<StylusPlugIn>
	{
		// Token: 0x06001855 RID: 6229 RVA: 0x000619DC File Offset: 0x00060DDC
		protected override void InsertItem(int index, StylusPlugIn plugIn)
		{
			this._element.VerifyAccess();
			if (plugIn == null)
			{
				throw new ArgumentNullException("plugIn", SR.Get("Stylus_PlugInIsNull"));
			}
			if (base.IndexOf(plugIn) != -1)
			{
				throw new ArgumentException(SR.Get("Stylus_PlugInIsDuplicated"), "plugIn");
			}
			Action <>9__1;
			this.ExecuteWithPotentialDispatcherDisable(delegate
			{
				if (this._stylusPlugInCollectionImpl.IsActiveForInput)
				{
					StylusPlugInCollection <>4__this = this;
					Action action;
					if ((action = <>9__1) == null)
					{
						action = (<>9__1 = delegate()
						{
							this.<>n__0(index, plugIn);
							plugIn.Added(this);
						});
					}
					<>4__this.ExecuteWithPotentialLock(action);
					return;
				}
				this.EnsureEventsHooked();
				this.<>n__0(index, plugIn);
				try
				{
					plugIn.Added(this);
				}
				finally
				{
					this._stylusPlugInCollectionImpl.UpdateState(this._element);
				}
			});
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x00061A64 File Offset: 0x00060E64
		protected override void ClearItems()
		{
			this._element.VerifyAccess();
			if (base.Count != 0)
			{
				this.ExecuteWithPotentialDispatcherDisable(delegate
				{
					if (this._stylusPlugInCollectionImpl.IsActiveForInput)
					{
						this.ExecuteWithPotentialLock(delegate
						{
							while (base.Count > 0)
							{
								this.RemoveItem(0);
							}
						});
						return;
					}
					while (base.Count > 0)
					{
						this.RemoveItem(0);
					}
				});
			}
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x00061A98 File Offset: 0x00060E98
		protected override void RemoveItem(int index)
		{
			this._element.VerifyAccess();
			Action <>9__1;
			this.ExecuteWithPotentialDispatcherDisable(delegate
			{
				if (this._stylusPlugInCollectionImpl.IsActiveForInput)
				{
					StylusPlugInCollection <>4__this = this;
					Action action;
					if ((action = <>9__1) == null)
					{
						action = (<>9__1 = delegate()
						{
							StylusPlugIn stylusPlugIn2 = this.<>n__1(index);
							this.<>n__2(index);
							try
							{
								this.EnsureEventsUnhooked();
							}
							finally
							{
								stylusPlugIn2.Removed();
							}
						});
					}
					<>4__this.ExecuteWithPotentialLock(action);
					return;
				}
				StylusPlugIn stylusPlugIn = this.<>n__1(index);
				this.<>n__2(index);
				try
				{
					this.EnsureEventsUnhooked();
				}
				finally
				{
					stylusPlugIn.Removed();
				}
			});
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x00061AD8 File Offset: 0x00060ED8
		protected override void SetItem(int index, StylusPlugIn plugIn)
		{
			this._element.VerifyAccess();
			if (plugIn == null)
			{
				throw new ArgumentNullException("plugIn", SR.Get("Stylus_PlugInIsNull"));
			}
			if (base.IndexOf(plugIn) != -1)
			{
				throw new ArgumentException(SR.Get("Stylus_PlugInIsDuplicated"), "plugIn");
			}
			Action <>9__1;
			this.ExecuteWithPotentialDispatcherDisable(delegate
			{
				if (this._stylusPlugInCollectionImpl.IsActiveForInput)
				{
					StylusPlugInCollection <>4__this = this;
					Action action;
					if ((action = <>9__1) == null)
					{
						action = (<>9__1 = delegate()
						{
							StylusPlugIn stylusPlugIn2 = this.<>n__1(index);
							this.<>n__3(index, plugIn);
							try
							{
								stylusPlugIn2.Removed();
							}
							finally
							{
								plugIn.Added(this);
							}
						});
					}
					<>4__this.ExecuteWithPotentialLock(action);
					return;
				}
				StylusPlugIn stylusPlugIn = this.<>n__1(index);
				this.<>n__3(index, plugIn);
				try
				{
					stylusPlugIn.Removed();
				}
				finally
				{
					plugIn.Added(this);
				}
			});
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x00061B60 File Offset: 0x00060F60
		internal StylusPlugInCollection(UIElement element)
		{
			this._stylusPlugInCollectionImpl = StylusPlugInCollectionBase.Create(this);
			this._element = element;
			this._isEnabledChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnIsEnabledChanged);
			this._isVisibleChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnIsVisibleChanged);
			this._isHitTestVisibleChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnIsHitTestVisibleChanged);
			this._sourceChangedEventHandler = new SourceChangedEventHandler(this.OnSourceChanged);
			this._layoutChangedEventHandler = new EventHandler(this.OnLayoutUpdated);
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x0600185A RID: 6234 RVA: 0x00061BE0 File Offset: 0x00060FE0
		internal UIElement Element
		{
			get
			{
				return this._element;
			}
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x00061BF4 File Offset: 0x00060FF4
		internal void UpdateRect()
		{
			if (this._element.IsArrangeValid && this._element.IsEnabled && this._element.IsVisible && this._element.IsHitTestVisible)
			{
				this._rc = new Rect(default(Point), this._element.RenderSize);
				Visual containingVisual2D = VisualTreeHelper.GetContainingVisual2D(InputElement.GetRootVisual(this._element));
				try
				{
					this._viewToElement = containingVisual2D.TransformToDescendant(this._element);
					goto IL_A4;
				}
				catch (InvalidOperationException)
				{
					this._rc = default(Rect);
					this._viewToElement = Transform.Identity;
					goto IL_A4;
				}
			}
			this._rc = default(Rect);
			IL_A4:
			if (this._viewToElement == null)
			{
				this._viewToElement = Transform.Identity;
			}
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00061CD4 File Offset: 0x000610D4
		internal bool IsHit(Point pt)
		{
			Point point = pt;
			this._viewToElement.TryTransform(point, out point);
			return this._rc.Contains(point);
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x00061D00 File Offset: 0x00061100
		internal GeneralTransform ViewToElement
		{
			get
			{
				return this._viewToElement;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x00061D14 File Offset: 0x00061114
		internal Rect Rect
		{
			get
			{
				return this._rc;
			}
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x00061D28 File Offset: 0x00061128
		internal void FireEnterLeave(bool isEnter, RawStylusInput rawStylusInput, bool confirmed)
		{
			if (this._stylusPlugInCollectionImpl.IsActiveForInput)
			{
				this.ExecuteWithPotentialLock(delegate
				{
					for (int j = 0; j < this.Count; j++)
					{
						this.<>n__1(j).StylusEnterLeave(isEnter, rawStylusInput, confirmed);
					}
				});
				return;
			}
			for (int i = 0; i < base.Count; i++)
			{
				base[i].StylusEnterLeave(isEnter, rawStylusInput, confirmed);
			}
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x00061DA8 File Offset: 0x000611A8
		internal void FireRawStylusInput(RawStylusInput args)
		{
			try
			{
				if (this._stylusPlugInCollectionImpl.IsActiveForInput)
				{
					this.ExecuteWithPotentialLock(delegate
					{
						for (int j = 0; j < this.Count; j++)
						{
							StylusPlugIn stylusPlugIn2 = this.<>n__1(j);
							args.CurrentNotifyPlugIn = stylusPlugIn2;
							stylusPlugIn2.RawStylusInput(args);
						}
					});
				}
				else
				{
					for (int i = 0; i < base.Count; i++)
					{
						StylusPlugIn stylusPlugIn = base[i];
						args.CurrentNotifyPlugIn = stylusPlugIn;
						stylusPlugIn.RawStylusInput(args);
					}
				}
			}
			finally
			{
				args.CurrentNotifyPlugIn = null;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x00061E48 File Offset: 0x00061248
		internal bool IsActiveForInput
		{
			get
			{
				return this._stylusPlugInCollectionImpl.IsActiveForInput;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x00061E60 File Offset: 0x00061260
		internal object SyncRoot
		{
			get
			{
				return this._stylusPlugInCollectionImpl.SyncRoot;
			}
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x00061E78 File Offset: 0x00061278
		internal void OnLayoutUpdated(object sender, EventArgs e)
		{
			if (this._stylusPlugInCollectionImpl.IsActiveForInput)
			{
				this.ExecuteWithPotentialDispatcherDisable(delegate
				{
					this.ExecuteWithPotentialLock(delegate
					{
						this.UpdateRect();
					});
				});
			}
			else
			{
				this.UpdateRect();
			}
			if (this._lastRenderTransform != this._element.RenderTransform)
			{
				if (this._renderTransformChangedEventHandler != null)
				{
					this._lastRenderTransform.Changed -= this._renderTransformChangedEventHandler;
					this._renderTransformChangedEventHandler = null;
				}
				this._lastRenderTransform = this._element.RenderTransform;
			}
			if (this._lastRenderTransform != null)
			{
				if (this._lastRenderTransform.IsFrozen)
				{
					if (this._renderTransformChangedEventHandler != null)
					{
						this._renderTransformChangedEventHandler = null;
						return;
					}
				}
				else if (this._renderTransformChangedEventHandler == null)
				{
					this._renderTransformChangedEventHandler = new EventHandler(this.OnRenderTransformChanged);
					this._lastRenderTransform.Changed += this._renderTransformChangedEventHandler;
				}
			}
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x00061F40 File Offset: 0x00061340
		internal void ExecuteWithPotentialLock(Action action)
		{
			if (this._stylusPlugInCollectionImpl.SyncRoot != null)
			{
				object syncRoot = this._stylusPlugInCollectionImpl.SyncRoot;
				lock (syncRoot)
				{
					action();
					return;
				}
			}
			action();
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x00061FA4 File Offset: 0x000613A4
		internal void ExecuteWithPotentialDispatcherDisable(Action action)
		{
			if (this._stylusPlugInCollectionImpl.SyncRoot != null)
			{
				using (this._element.Dispatcher.DisableProcessing())
				{
					action();
					return;
				}
			}
			action();
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x00062008 File Offset: 0x00061408
		[SecuritySafeCritical]
		private void EnsureEventsHooked()
		{
			if (base.Count == 0)
			{
				this.UpdateRect();
				this._element.IsEnabledChanged += this._isEnabledChangedEventHandler;
				this._element.IsVisibleChanged += this._isVisibleChangedEventHandler;
				this._element.IsHitTestVisibleChanged += this._isHitTestVisibleChangedEventHandler;
				PresentationSource.AddSourceChangedHandler(this._element, this._sourceChangedEventHandler);
				this._element.LayoutUpdated += this._layoutChangedEventHandler;
				if (this._element.RenderTransform != null && !this._element.RenderTransform.IsFrozen && this._renderTransformChangedEventHandler == null)
				{
					this._renderTransformChangedEventHandler = new EventHandler(this.OnRenderTransformChanged);
					this._element.RenderTransform.Changed += this._renderTransformChangedEventHandler;
				}
			}
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x000620CC File Offset: 0x000614CC
		private void EnsureEventsUnhooked()
		{
			if (base.Count == 0)
			{
				this._element.IsEnabledChanged -= this._isEnabledChangedEventHandler;
				this._element.IsVisibleChanged -= this._isVisibleChangedEventHandler;
				this._element.IsHitTestVisibleChanged -= this._isHitTestVisibleChangedEventHandler;
				if (this._renderTransformChangedEventHandler != null)
				{
					this._element.RenderTransform.Changed -= this._renderTransformChangedEventHandler;
				}
				PresentationSource.RemoveSourceChangedHandler(this._element, this._sourceChangedEventHandler);
				this._element.LayoutUpdated -= this._layoutChangedEventHandler;
				this.ExecuteWithPotentialDispatcherDisable(delegate
				{
					this._stylusPlugInCollectionImpl.Unhook();
				});
			}
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x0006216C File Offset: 0x0006156C
		private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this._stylusPlugInCollectionImpl.UpdateState(this._element);
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x0006218C File Offset: 0x0006158C
		private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this._stylusPlugInCollectionImpl.UpdateState(this._element);
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x000621AC File Offset: 0x000615AC
		private void OnIsHitTestVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this._stylusPlugInCollectionImpl.UpdateState(this._element);
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x000621CC File Offset: 0x000615CC
		private void OnRenderTransformChanged(object sender, EventArgs e)
		{
			this.OnLayoutUpdated(sender, e);
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x000621E4 File Offset: 0x000615E4
		private void OnSourceChanged(object sender, SourceChangedEventArgs e)
		{
			this._stylusPlugInCollectionImpl.UpdateState(this._element);
		}

		// Token: 0x04000D35 RID: 3381
		private StylusPlugInCollectionBase _stylusPlugInCollectionImpl;

		// Token: 0x04000D36 RID: 3382
		private UIElement _element;

		// Token: 0x04000D37 RID: 3383
		private Rect _rc;

		// Token: 0x04000D38 RID: 3384
		private GeneralTransform _viewToElement;

		// Token: 0x04000D39 RID: 3385
		private Transform _lastRenderTransform;

		// Token: 0x04000D3A RID: 3386
		private DependencyPropertyChangedEventHandler _isEnabledChangedEventHandler;

		// Token: 0x04000D3B RID: 3387
		private DependencyPropertyChangedEventHandler _isVisibleChangedEventHandler;

		// Token: 0x04000D3C RID: 3388
		private DependencyPropertyChangedEventHandler _isHitTestVisibleChangedEventHandler;

		// Token: 0x04000D3D RID: 3389
		private EventHandler _renderTransformChangedEventHandler;

		// Token: 0x04000D3E RID: 3390
		private SourceChangedEventHandler _sourceChangedEventHandler;

		// Token: 0x04000D3F RID: 3391
		private EventHandler _layoutChangedEventHandler;
	}
}
