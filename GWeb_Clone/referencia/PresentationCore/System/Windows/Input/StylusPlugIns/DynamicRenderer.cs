using System;
using System.Collections.Generic;
using System.Security;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Threading;
using MS.Internal.Ink;
using MS.Internal.PresentationCore;

namespace System.Windows.Input.StylusPlugIns
{
	/// <summary>Desenha tinta em uma superfície conforme o usuário move a caneta eletrônica.</summary>
	// Token: 0x020002F4 RID: 756
	public class DynamicRenderer : StylusPlugIn
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.StylusPlugIns.DynamicRenderer" />.</summary>
		// Token: 0x060017F6 RID: 6134 RVA: 0x00060134 File Offset: 0x0005F534
		public DynamicRenderer()
		{
			this._zeroSizedFrozenRect = new RectangleGeometry(new Rect(0.0, 0.0, 0.0, 0.0));
			this._zeroSizedFrozenRect.Freeze();
		}

		/// <summary>Limpa a renderização do traço atual e o redesenha.</summary>
		/// <param name="stylusDevice">O dispositivo de caneta atual.</param>
		/// <param name="stylusPoints">Os pontos de caneta a serem redesenhados.</param>
		/// <exception cref="T:System.ArgumentException">Nem a caneta nem o mouse estão inoperantes.</exception>
		// Token: 0x060017F7 RID: 6135 RVA: 0x000601B4 File Offset: 0x0005F5B4
		public virtual void Reset(StylusDevice stylusDevice, StylusPointCollection stylusPoints)
		{
			if (this._mainContainerVisual == null || this._applicationDispatcher == null || !base.IsActiveForInput)
			{
				return;
			}
			this._applicationDispatcher.VerifyAccess();
			bool flag = (stylusDevice != null) ? stylusDevice.InAir : (Mouse.PrimaryDevice.LeftButton == MouseButtonState.Released);
			if (flag)
			{
				throw new ArgumentException(SR.Get("Stylus_MustBeDownToCallReset"), "stylusDevice");
			}
			using (this._applicationDispatcher.DisableProcessing())
			{
				object _siLock = this.__siLock;
				lock (_siLock)
				{
					this.AbortAllStrokes();
					DynamicRenderer.StrokeInfo strokeInfo = new DynamicRenderer.StrokeInfo(this.DrawingAttributes, (stylusDevice != null) ? stylusDevice.Id : 0, Environment.TickCount, this.GetCurrentHostVisual());
					this._strokeInfoList.Add(strokeInfo);
					strokeInfo.IsReset = true;
					if (stylusPoints != null)
					{
						this.RenderPackets(stylusPoints, strokeInfo);
					}
				}
			}
		}

		/// <summary>Obtém a raiz visual para o <see cref="T:System.Windows.Input.StylusPlugIns.DynamicRenderer" />.</summary>
		/// <returns>A raiz <see cref="T:System.Windows.Media.Visual" /> para o <see cref="T:System.Windows.Input.StylusPlugIns.DynamicRenderer" />.</returns>
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x000602CC File Offset: 0x0005F6CC
		public Visual RootVisual
		{
			get
			{
				if (this._mainContainerVisual == null)
				{
					this.CreateInkingVisuals();
				}
				return this._mainContainerVisual;
			}
		}

		/// <summary>Ocorre quando o <see cref="T:System.Windows.Input.StylusPlugIns.DynamicRenderer" /> é adicionado a um elemento.</summary>
		// Token: 0x060017F9 RID: 6137 RVA: 0x000602F0 File Offset: 0x0005F6F0
		protected override void OnAdded()
		{
			this._applicationDispatcher = base.Element.Dispatcher;
			if (base.IsActiveForInput)
			{
				this.CreateRealTimeVisuals();
			}
		}

		/// <summary>Ocorre quando o <see cref="T:System.Windows.Input.StylusPlugIns.StylusPlugIn" /> é removido de um elemento.</summary>
		// Token: 0x060017FA RID: 6138 RVA: 0x0006031C File Offset: 0x0005F71C
		protected override void OnRemoved()
		{
			this.DestroyRealTimeVisuals();
			this._applicationDispatcher = null;
		}

		/// <summary>Ocorre quando a propriedade <see cref="P:System.Windows.Input.StylusPlugIns.StylusPlugIn.IsActiveForInput" /> muda.</summary>
		// Token: 0x060017FB RID: 6139 RVA: 0x00060338 File Offset: 0x0005F738
		protected override void OnIsActiveForInputChanged()
		{
			if (base.IsActiveForInput)
			{
				this.CreateRealTimeVisuals();
				return;
			}
			this.DestroyRealTimeVisuals();
		}

		/// <summary>Ocorre em um thread de caneta quando o cursor do mouse entra nos limites de um elemento.</summary>
		/// <param name="rawStylusInput">Um <see cref="T:System.Windows.Input.StylusPlugIns.RawStylusInput" /> que contém informações sobre a entrada da caneta.</param>
		/// <param name="confirmed">
		///   <see langword="true" /> se a caneta de fato adentrou os limites do elemento; caso contrário, <see langword="false" />.</param>
		// Token: 0x060017FC RID: 6140 RVA: 0x0006035C File Offset: 0x0005F75C
		protected override void OnStylusEnter(RawStylusInput rawStylusInput, bool confirmed)
		{
			this.HandleStylusEnterLeave(rawStylusInput, true, confirmed);
		}

		/// <summary>Ocorre em um thread de caneta quando o cursor do mouse deixa os limites de um elemento.</summary>
		/// <param name="rawStylusInput">Um <see cref="T:System.Windows.Input.StylusPlugIns.RawStylusInput" /> que contém informações sobre a entrada da caneta.</param>
		/// <param name="confirmed">
		///   <see langword="true" /> se a caneta de fato deixou os limites do elemento; caso contrário, <see langword="false" />.</param>
		// Token: 0x060017FD RID: 6141 RVA: 0x00060374 File Offset: 0x0005F774
		protected override void OnStylusLeave(RawStylusInput rawStylusInput, bool confirmed)
		{
			this.HandleStylusEnterLeave(rawStylusInput, false, confirmed);
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x0006038C File Offset: 0x0005F78C
		private void HandleStylusEnterLeave(RawStylusInput rawStylusInput, bool isEnter, bool isConfirmed)
		{
			if (isConfirmed)
			{
				DynamicRenderer.StrokeInfo strokeInfo = this.FindStrokeInfo(rawStylusInput.Timestamp);
				if (strokeInfo != null && rawStylusInput.StylusDeviceId == strokeInfo.StylusId && ((isEnter && rawStylusInput.Timestamp > strokeInfo.StartTime) || (!isEnter && !strokeInfo.SeenUp)))
				{
					this.TransitionStrokeVisuals(strokeInfo, true);
				}
			}
		}

		/// <summary>Ocorre quando a propriedade <see cref="P:System.Windows.Input.StylusPlugIns.StylusPlugIn.Enabled" /> muda.</summary>
		// Token: 0x060017FF RID: 6143 RVA: 0x000603E0 File Offset: 0x0005F7E0
		protected override void OnEnabledChanged()
		{
			if (!base.Enabled)
			{
				this.AbortAllStrokes();
			}
		}

		/// <summary>Ocorre em um thread no pool de threads de caneta quando a caneta eletrônica toca o digitalizador.</summary>
		/// <param name="rawStylusInput">Um <see cref="T:System.Windows.Input.StylusPlugIns.RawStylusInput" /> que contém informações sobre a entrada da caneta.</param>
		// Token: 0x06001800 RID: 6144 RVA: 0x000603FC File Offset: 0x0005F7FC
		protected override void OnStylusDown(RawStylusInput rawStylusInput)
		{
			if (this._mainContainerVisual != null)
			{
				object _siLock = this.__siLock;
				DynamicRenderer.StrokeInfo strokeInfo;
				lock (_siLock)
				{
					strokeInfo = this.FindStrokeInfo(rawStylusInput.Timestamp);
					if (strokeInfo != null)
					{
						return;
					}
					strokeInfo = new DynamicRenderer.StrokeInfo(this.DrawingAttributes, rawStylusInput.StylusDeviceId, rawStylusInput.Timestamp, this.GetCurrentHostVisual());
					this._strokeInfoList.Add(strokeInfo);
				}
				rawStylusInput.NotifyWhenProcessed(strokeInfo);
				this.RenderPackets(rawStylusInput.GetStylusPoints(), strokeInfo);
			}
		}

		/// <summary>Ocorre em um thread de caneta quando a caneta eletrônica se move sobre o digitalizador.</summary>
		/// <param name="rawStylusInput">Um <see cref="T:System.Windows.Input.StylusPlugIns.RawStylusInput" /> que contém informações sobre a entrada da caneta.</param>
		// Token: 0x06001801 RID: 6145 RVA: 0x0006049C File Offset: 0x0005F89C
		protected override void OnStylusMove(RawStylusInput rawStylusInput)
		{
			if (this._mainContainerVisual != null)
			{
				DynamicRenderer.StrokeInfo strokeInfo = this.FindStrokeInfo(rawStylusInput.Timestamp);
				if (strokeInfo != null && strokeInfo.StylusId == rawStylusInput.StylusDeviceId && strokeInfo.IsTimestampAfter(rawStylusInput.Timestamp))
				{
					strokeInfo.LastTime = rawStylusInput.Timestamp;
					this.RenderPackets(rawStylusInput.GetStylusPoints(), strokeInfo);
				}
			}
		}

		/// <summary>Ocorre em um thread de caneta quando o usuário erguer a caneta eletrônica do digitalizador.</summary>
		/// <param name="rawStylusInput">Um <see cref="T:System.Windows.Input.StylusPlugIns.RawStylusInput" /> que contém informações sobre a entrada da caneta.</param>
		// Token: 0x06001802 RID: 6146 RVA: 0x000604F8 File Offset: 0x0005F8F8
		protected override void OnStylusUp(RawStylusInput rawStylusInput)
		{
			if (this._mainContainerVisual != null)
			{
				DynamicRenderer.StrokeInfo strokeInfo = this.FindStrokeInfo(rawStylusInput.Timestamp);
				if (strokeInfo != null && (strokeInfo.StylusId == rawStylusInput.StylusDeviceId || (rawStylusInput.StylusDeviceId == 0 && (strokeInfo.IsReset || (strokeInfo.IsTimestampAfter(rawStylusInput.Timestamp) && this.IsStylusUp(strokeInfo.StylusId))))))
				{
					strokeInfo.SeenUp = true;
					strokeInfo.LastTime = rawStylusInput.Timestamp;
					rawStylusInput.NotifyWhenProcessed(strokeInfo);
				}
			}
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x00060574 File Offset: 0x0005F974
		private bool IsStylusUp(int stylusId)
		{
			TabletDeviceCollection tabletDevices = Tablet.TabletDevices;
			for (int i = 0; i < tabletDevices.Count; i++)
			{
				TabletDevice tabletDevice = tabletDevices[i];
				for (int j = 0; j < tabletDevice.StylusDevices.Count; j++)
				{
					StylusDevice stylusDevice = tabletDevice.StylusDevices[j];
					if (stylusId == stylusDevice.Id)
					{
						return stylusDevice.InAir;
					}
				}
			}
			return true;
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x000605D8 File Offset: 0x0005F9D8
		private void OnRenderComplete()
		{
			DynamicRenderer.StrokeInfo renderCompleteStrokeInfo = this._renderCompleteStrokeInfo;
			if (renderCompleteStrokeInfo != null)
			{
				if (renderCompleteStrokeInfo.StrokeHV.Clip == null)
				{
					this.TransitionComplete(renderCompleteStrokeInfo);
					this._renderCompleteStrokeInfo = null;
					return;
				}
				this.RemoveDynamicRendererVisualAndNotifyWhenDone(renderCompleteStrokeInfo);
			}
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x00060614 File Offset: 0x0005FA14
		private void RemoveDynamicRendererVisualAndNotifyWhenDone(DynamicRenderer.StrokeInfo si)
		{
			if (si != null)
			{
				DynamicRendererThreadManager renderingThread = this._renderingThread;
				if (renderingThread != null)
				{
					renderingThread.ThreadDispatcher.BeginInvoke(DispatcherPriority.Send, new DispatcherOperationCallback(delegate(object unused)
					{
						if (si.StrokeRTICV != null)
						{
							if (this._onDRThreadRenderComplete == null)
							{
								this._onDRThreadRenderComplete = new EventHandler(this.OnDRThreadRenderComplete);
							}
							this._renderCompleteDRThreadStrokeInfoList.Enqueue(si);
							if (!this._waitingForDRThreadRenderComplete)
							{
								((ContainerVisual)si.StrokeHV.VisualTarget.RootVisual).Children.Remove(si.StrokeRTICV);
								si.StrokeRTICV = null;
								MediaContext.From(renderingThread.ThreadDispatcher).RenderComplete += this._onDRThreadRenderComplete;
								this._waitingForDRThreadRenderComplete = true;
							}
						}
						else
						{
							this.NotifyAppOfDRThreadRenderComplete(si);
						}
						return null;
					}), null);
				}
			}
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x00060674 File Offset: 0x0005FA74
		private void NotifyAppOfDRThreadRenderComplete(DynamicRenderer.StrokeInfo si)
		{
			Dispatcher applicationDispatcher = this._applicationDispatcher;
			if (applicationDispatcher != null)
			{
				applicationDispatcher.BeginInvoke(DispatcherPriority.Send, new DispatcherOperationCallback(delegate(object unused)
				{
					if (si == this._renderCompleteStrokeInfo)
					{
						if (si.StrokeHV.Clip != null)
						{
							si.StrokeHV.Clip = null;
							this.NotifyOnNextRenderComplete();
						}
						else
						{
							this.TransitionComplete(si);
						}
					}
					else
					{
						this.TransitionComplete(si);
					}
					return null;
				}), null);
			}
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x000606B8 File Offset: 0x0005FAB8
		private void OnDRThreadRenderComplete(object sender, EventArgs e)
		{
			DynamicRendererThreadManager renderingThread = this._renderingThread;
			if (renderingThread != null)
			{
				Dispatcher threadDispatcher = renderingThread.ThreadDispatcher;
				if (threadDispatcher != null)
				{
					if (this._renderCompleteDRThreadStrokeInfoList.Count > 0)
					{
						DynamicRenderer.StrokeInfo si = this._renderCompleteDRThreadStrokeInfoList.Dequeue();
						this.NotifyAppOfDRThreadRenderComplete(si);
					}
					if (this._renderCompleteDRThreadStrokeInfoList.Count == 0)
					{
						MediaContext.From(threadDispatcher).RenderComplete -= this._onDRThreadRenderComplete;
						this._waitingForDRThreadRenderComplete = false;
						return;
					}
					DynamicRenderer.StrokeInfo siNext = this._renderCompleteDRThreadStrokeInfoList.Peek();
					if (siNext.StrokeRTICV != null)
					{
						threadDispatcher.BeginInvoke(DispatcherPriority.Send, new DispatcherOperationCallback(delegate(object unused)
						{
							((ContainerVisual)siNext.StrokeHV.VisualTarget.RootVisual).Children.Remove(siNext.StrokeRTICV);
							siNext.StrokeRTICV = null;
							return null;
						}), null);
					}
				}
			}
		}

		/// <summary>Ocorre em um thread de interface do usuário do aplicativo quando a caneta eletrônica toca o digitalizador.</summary>
		/// <param name="callbackData">O objeto que o aplicativo passou para o método <see cref="M:System.Windows.Input.StylusPlugIns.RawStylusInput.NotifyWhenProcessed(System.Object)" />.</param>
		/// <param name="targetVerified">
		///   <see langword="true" /> se a entrada da caneta foi encaminhada corretamente para o <see cref="T:System.Windows.Input.StylusPlugIns.StylusPlugIn" />; caso contrário, <see langword="false" />.</param>
		// Token: 0x06001808 RID: 6152 RVA: 0x00060764 File Offset: 0x0005FB64
		protected override void OnStylusDownProcessed(object callbackData, bool targetVerified)
		{
			DynamicRenderer.StrokeInfo strokeInfo = callbackData as DynamicRenderer.StrokeInfo;
			if (strokeInfo == null)
			{
				return;
			}
			if (!targetVerified)
			{
				this.TransitionStrokeVisuals(strokeInfo, true);
			}
		}

		/// <summary>Ocorre em um thread de interface do usuário do aplicativo quando o usuário levanta a caneta eletrônica do digitalizador.</summary>
		/// <param name="callbackData">O objeto que o aplicativo passou para o método <see cref="M:System.Windows.Input.StylusPlugIns.RawStylusInput.NotifyWhenProcessed(System.Object)" />.</param>
		/// <param name="targetVerified">
		///   <see langword="true" /> se a entrada da caneta foi encaminhada corretamente para o <see cref="T:System.Windows.Input.StylusPlugIns.StylusPlugIn" />; caso contrário, <see langword="false" />.</param>
		// Token: 0x06001809 RID: 6153 RVA: 0x00060788 File Offset: 0x0005FB88
		protected override void OnStylusUpProcessed(object callbackData, bool targetVerified)
		{
			DynamicRenderer.StrokeInfo strokeInfo = callbackData as DynamicRenderer.StrokeInfo;
			if (strokeInfo == null)
			{
				return;
			}
			this.TransitionStrokeVisuals(strokeInfo, !targetVerified);
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x000607AC File Offset: 0x0005FBAC
		private void OnInternalRenderComplete(object sender, EventArgs e)
		{
			MediaContext.From(this._applicationDispatcher).RenderComplete -= this._onRenderComplete;
			this._waitingForRenderComplete = false;
			using (this._applicationDispatcher.DisableProcessing())
			{
				this.OnRenderComplete();
			}
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x00060814 File Offset: 0x0005FC14
		private void NotifyOnNextRenderComplete()
		{
			if (this._applicationDispatcher == null)
			{
				return;
			}
			this._applicationDispatcher.VerifyAccess();
			if (this._onRenderComplete == null)
			{
				this._onRenderComplete = new EventHandler(this.OnInternalRenderComplete);
			}
			if (!this._waitingForRenderComplete)
			{
				MediaContext.From(this._applicationDispatcher).RenderComplete += this._onRenderComplete;
				this._waitingForRenderComplete = true;
			}
		}

		/// <summary>Desenha a tinta em tempo real para que ela pareça “fluir” da caneta eletrônica ou outro dispositivo apontador.</summary>
		/// <param name="drawingContext">O objeto <see cref="T:System.Windows.Media.DrawingContext" /> no qual o traço é renderizado.</param>
		/// <param name="stylusPoints">O <see cref="T:System.Windows.Input.StylusPointCollection" /> que representa o segmento do traço a ser desenhado.</param>
		/// <param name="geometry">Um <see cref="T:System.Windows.Media.Geometry" /> que representa o caminho do ponteiro do mouse.</param>
		/// <param name="fillBrush">Um Pincel que especifica a aparência do traço atual.</param>
		// Token: 0x0600180C RID: 6156 RVA: 0x00060874 File Offset: 0x0005FC74
		protected virtual void OnDraw(DrawingContext drawingContext, StylusPointCollection stylusPoints, Geometry geometry, Brush fillBrush)
		{
			if (drawingContext == null)
			{
				throw new ArgumentNullException("drawingContext");
			}
			drawingContext.DrawGeometry(fillBrush, null, geometry);
		}

		/// <summary>Ocorre quando a propriedade <see cref="P:System.Windows.Input.StylusPlugIns.DynamicRenderer.DrawingAttributes" /> muda.</summary>
		// Token: 0x0600180D RID: 6157 RVA: 0x0006089C File Offset: 0x0005FC9C
		protected virtual void OnDrawingAttributesReplaced()
		{
		}

		/// <summary>Retorna um <see cref="T:System.Windows.Threading.Dispatcher" /> para o thread de renderização.</summary>
		/// <returns>Um <see cref="T:System.Windows.Threading.Dispatcher" /> para o thread de renderização.</returns>
		// Token: 0x0600180E RID: 6158 RVA: 0x000608AC File Offset: 0x0005FCAC
		protected Dispatcher GetDispatcher()
		{
			if (this._renderingThread == null)
			{
				return null;
			}
			return this._renderingThread.ThreadDispatcher;
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x000608D0 File Offset: 0x0005FCD0
		private void RenderPackets(StylusPointCollection stylusPoints, DynamicRenderer.StrokeInfo si)
		{
			if (stylusPoints.Count == 0 || this._applicationDispatcher == null)
			{
				return;
			}
			si.StrokeNodeIterator = si.StrokeNodeIterator.GetIteratorForNextSegment(stylusPoints);
			if (si.StrokeNodeIterator != null)
			{
				Geometry strokeGeometry;
				Rect rect;
				StrokeRenderer.CalcGeometryAndBounds(si.StrokeNodeIterator, si.DrawingAttributes, false, out strokeGeometry, out rect);
				if (this._applicationDispatcher.CheckAccess())
				{
					if (si.StrokeCV == null)
					{
						si.StrokeCV = new ContainerVisual();
						if (!si.DrawingAttributes.IsHighlighter)
						{
							si.StrokeCV.Opacity = si.Opacity;
						}
						this._mainRawInkContainerVisual.Children.Add(si.StrokeCV);
					}
					DrawingVisual drawingVisual = new DrawingVisual();
					DrawingContext drawingContext = drawingVisual.RenderOpen();
					try
					{
						this.OnDraw(drawingContext, stylusPoints, strokeGeometry, si.FillBrush);
					}
					finally
					{
						drawingContext.Close();
					}
					if (si.StrokeCV != null)
					{
						si.StrokeCV.Children.Add(drawingVisual);
						return;
					}
				}
				else
				{
					DynamicRendererThreadManager renderingThread = this._renderingThread;
					Dispatcher dispatcher = (renderingThread != null) ? renderingThread.ThreadDispatcher : null;
					if (dispatcher != null)
					{
						dispatcher.BeginInvoke(DispatcherPriority.Send, new DispatcherOperationCallback(delegate(object unused)
						{
							SolidColorBrush fillBrush = si.FillBrush;
							if (fillBrush != null)
							{
								if (si.StrokeRTICV == null)
								{
									si.StrokeRTICV = new ContainerVisual();
									if (!si.DrawingAttributes.IsHighlighter)
									{
										si.StrokeRTICV.Opacity = si.Opacity;
									}
									((ContainerVisual)si.StrokeHV.VisualTarget.RootVisual).Children.Add(si.StrokeRTICV);
								}
								DrawingVisual drawingVisual2 = new DrawingVisual();
								DrawingContext drawingContext2 = drawingVisual2.RenderOpen();
								try
								{
									this.OnDraw(drawingContext2, stylusPoints, strokeGeometry, fillBrush);
								}
								finally
								{
									drawingContext2.Close();
								}
								si.StrokeRTICV.Children.Add(drawingVisual2);
							}
							return null;
						}), null);
					}
				}
			}
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x00060A80 File Offset: 0x0005FE80
		private void AbortAllStrokes()
		{
			object _siLock = this.__siLock;
			lock (_siLock)
			{
				while (this._strokeInfoList.Count > 0)
				{
					this.TransitionStrokeVisuals(this._strokeInfoList[0], true);
				}
			}
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x00060AEC File Offset: 0x0005FEEC
		private void TransitionStrokeVisuals(DynamicRenderer.StrokeInfo si, bool abortStroke)
		{
			this.RemoveStrokeInfo(si);
			if (si.StrokeCV != null)
			{
				if (this._mainRawInkContainerVisual != null)
				{
					this._mainRawInkContainerVisual.Children.Remove(si.StrokeCV);
				}
				si.StrokeCV = null;
			}
			si.FillBrush = null;
			if (this._rawInkHostVisual1 == null)
			{
				return;
			}
			bool flag = false;
			if (!abortStroke && this._renderCompleteStrokeInfo == null)
			{
				using (this._applicationDispatcher.DisableProcessing())
				{
					object _siLock = this.__siLock;
					lock (_siLock)
					{
						if (si.StrokeHV.HasSingleReference)
						{
							si.StrokeHV.Clip = this._zeroSizedFrozenRect;
							this._renderCompleteStrokeInfo = si;
							flag = true;
						}
					}
				}
			}
			if (flag)
			{
				this.NotifyOnNextRenderComplete();
				return;
			}
			this.RemoveDynamicRendererVisualAndNotifyWhenDone(si);
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x00060BF0 File Offset: 0x0005FFF0
		private DynamicRenderer.DynamicRendererHostVisual GetCurrentHostVisual()
		{
			if (this._currentHostVisual == null)
			{
				this._currentHostVisual = this._rawInkHostVisual1;
			}
			else
			{
				HostVisual hostVisual = (this._renderCompleteStrokeInfo != null) ? this._renderCompleteStrokeInfo.StrokeHV : null;
				if (this._currentHostVisual.InUse)
				{
					if (this._currentHostVisual == this._rawInkHostVisual1)
					{
						if (!this._rawInkHostVisual2.InUse || this._rawInkHostVisual1 == hostVisual)
						{
							this._currentHostVisual = this._rawInkHostVisual2;
						}
					}
					else if (!this._rawInkHostVisual1.InUse || this._rawInkHostVisual2 == hostVisual)
					{
						this._currentHostVisual = this._rawInkHostVisual1;
					}
				}
			}
			return this._currentHostVisual;
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00060C94 File Offset: 0x00060094
		private void TransitionComplete(DynamicRenderer.StrokeInfo si)
		{
			using (this._applicationDispatcher.DisableProcessing())
			{
				object _siLock = this.__siLock;
				lock (_siLock)
				{
					si.StrokeHV.RemoveStrokeInfoRef(si);
				}
			}
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x00060D1C File Offset: 0x0006011C
		private void RemoveStrokeInfo(DynamicRenderer.StrokeInfo si)
		{
			object _siLock = this.__siLock;
			lock (_siLock)
			{
				this._strokeInfoList.Remove(si);
			}
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x00060D70 File Offset: 0x00060170
		private DynamicRenderer.StrokeInfo FindStrokeInfo(int timestamp)
		{
			object _siLock = this.__siLock;
			lock (_siLock)
			{
				for (int i = 0; i < this._strokeInfoList.Count; i++)
				{
					DynamicRenderer.StrokeInfo strokeInfo = this._strokeInfoList[i];
					if (strokeInfo.IsTimestampWithin(timestamp))
					{
						return strokeInfo;
					}
				}
			}
			return null;
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Ink.DrawingAttributes" /> que especifica a aparência da tinta renderizada.</summary>
		/// <returns>O <see cref="T:System.Windows.Ink.DrawingAttributes" /> que especifica a aparência da tinta renderizada.</returns>
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x00060DEC File Offset: 0x000601EC
		// (set) Token: 0x06001817 RID: 6167 RVA: 0x00060E00 File Offset: 0x00060200
		public DrawingAttributes DrawingAttributes
		{
			get
			{
				return this._drawAttrsSource;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._drawAttrsSource = value;
				this.OnDrawingAttributesReplaced();
			}
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00060E30 File Offset: 0x00060230
		private void CreateInkingVisuals()
		{
			if (this._mainContainerVisual == null)
			{
				this._mainContainerVisual = new ContainerVisual();
				this._mainRawInkContainerVisual = new ContainerVisual();
				this._mainContainerVisual.Children.Add(this._mainRawInkContainerVisual);
			}
			if (base.IsActiveForInput)
			{
				using (base.Element.Dispatcher.DisableProcessing())
				{
					this.CreateRealTimeVisuals();
				}
			}
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00060EC0 File Offset: 0x000602C0
		private void CreateRealTimeVisuals()
		{
			if (this._mainContainerVisual != null && this._rawInkHostVisual1 == null)
			{
				this._rawInkHostVisual1 = new DynamicRenderer.DynamicRendererHostVisual();
				this._rawInkHostVisual2 = new DynamicRenderer.DynamicRendererHostVisual();
				this._currentHostVisual = null;
				this._mainContainerVisual.Children.Add(this._rawInkHostVisual1);
				this._mainContainerVisual.Children.Add(this._rawInkHostVisual2);
				this._renderingThread = DynamicRendererThreadManager.GetCurrentThreadInstance();
			}
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x00060F34 File Offset: 0x00060334
		private void DestroyRealTimeVisuals()
		{
			if (this._mainContainerVisual != null && this._rawInkHostVisual1 != null)
			{
				if (this._waitingForRenderComplete)
				{
					MediaContext.From(this._applicationDispatcher).RenderComplete -= this._onRenderComplete;
					this._waitingForRenderComplete = false;
				}
				this._mainContainerVisual.Children.Remove(this._rawInkHostVisual1);
				this._mainContainerVisual.Children.Remove(this._rawInkHostVisual2);
				this._renderCompleteStrokeInfo = null;
				DynamicRendererThreadManager renderingThread = this._renderingThread;
				Dispatcher drDispatcher = (renderingThread != null) ? renderingThread.ThreadDispatcher : null;
				if (drDispatcher != null)
				{
					drDispatcher.BeginInvoke(DispatcherPriority.Send, new DispatcherOperationCallback(delegate(object unused)
					{
						this._renderCompleteDRThreadStrokeInfoList.Clear();
						drDispatcher = renderingThread.ThreadDispatcher;
						if (drDispatcher != null && this._waitingForDRThreadRenderComplete)
						{
							MediaContext.From(drDispatcher).RenderComplete -= this._onDRThreadRenderComplete;
						}
						this._waitingForDRThreadRenderComplete = false;
						return null;
					}), null);
				}
				this._renderingThread = null;
				this._rawInkHostVisual1 = null;
				this._rawInkHostVisual2 = null;
				this._currentHostVisual = null;
				this.AbortAllStrokes();
			}
		}

		// Token: 0x04000D16 RID: 3350
		private Dispatcher _applicationDispatcher;

		// Token: 0x04000D17 RID: 3351
		private Geometry _zeroSizedFrozenRect;

		// Token: 0x04000D18 RID: 3352
		private DrawingAttributes _drawAttrsSource = new DrawingAttributes();

		// Token: 0x04000D19 RID: 3353
		private List<DynamicRenderer.StrokeInfo> _strokeInfoList = new List<DynamicRenderer.StrokeInfo>();

		// Token: 0x04000D1A RID: 3354
		private ContainerVisual _mainContainerVisual;

		// Token: 0x04000D1B RID: 3355
		private ContainerVisual _mainRawInkContainerVisual;

		// Token: 0x04000D1C RID: 3356
		private DynamicRenderer.DynamicRendererHostVisual _rawInkHostVisual1;

		// Token: 0x04000D1D RID: 3357
		private DynamicRenderer.DynamicRendererHostVisual _rawInkHostVisual2;

		// Token: 0x04000D1E RID: 3358
		private DynamicRenderer.DynamicRendererHostVisual _currentHostVisual;

		// Token: 0x04000D1F RID: 3359
		private EventHandler _onRenderComplete;

		// Token: 0x04000D20 RID: 3360
		private bool _waitingForRenderComplete;

		// Token: 0x04000D21 RID: 3361
		private object __siLock = new object();

		// Token: 0x04000D22 RID: 3362
		private DynamicRenderer.StrokeInfo _renderCompleteStrokeInfo;

		// Token: 0x04000D23 RID: 3363
		private DynamicRendererThreadManager _renderingThread;

		// Token: 0x04000D24 RID: 3364
		private EventHandler _onDRThreadRenderComplete;

		// Token: 0x04000D25 RID: 3365
		private bool _waitingForDRThreadRenderComplete;

		// Token: 0x04000D26 RID: 3366
		private Queue<DynamicRenderer.StrokeInfo> _renderCompleteDRThreadStrokeInfoList = new Queue<DynamicRenderer.StrokeInfo>();

		// Token: 0x02000828 RID: 2088
		private class StrokeInfo
		{
			// Token: 0x06005660 RID: 22112 RVA: 0x00162938 File Offset: 0x00161D38
			public StrokeInfo(DrawingAttributes drawingAttributes, int stylusDeviceId, int startTimestamp, DynamicRenderer.DynamicRendererHostVisual hostVisual)
			{
				this._stylusId = stylusDeviceId;
				this._startTime = startTimestamp;
				this._lastTime = this._startTime;
				this._drawingAttributes = drawingAttributes.Clone();
				this._strokeNodeIterator = new StrokeNodeIterator(this._drawingAttributes);
				Color color = this._drawingAttributes.Color;
				this._opacity = (this._drawingAttributes.IsHighlighter ? 0.0 : ((double)color.A / (double)StrokeRenderer.SolidStrokeAlpha));
				color.A = StrokeRenderer.SolidStrokeAlpha;
				SolidColorBrush solidColorBrush = new SolidColorBrush(color);
				solidColorBrush.Freeze();
				this._fillBrush = solidColorBrush;
				this._strokeHV = hostVisual;
				hostVisual.AddStrokeInfoRef(this);
			}

			// Token: 0x170011C0 RID: 4544
			// (get) Token: 0x06005661 RID: 22113 RVA: 0x001629EC File Offset: 0x00161DEC
			public int StylusId
			{
				get
				{
					return this._stylusId;
				}
			}

			// Token: 0x170011C1 RID: 4545
			// (get) Token: 0x06005662 RID: 22114 RVA: 0x00162A00 File Offset: 0x00161E00
			public int StartTime
			{
				get
				{
					return this._startTime;
				}
			}

			// Token: 0x170011C2 RID: 4546
			// (get) Token: 0x06005663 RID: 22115 RVA: 0x00162A14 File Offset: 0x00161E14
			// (set) Token: 0x06005664 RID: 22116 RVA: 0x00162A28 File Offset: 0x00161E28
			public int LastTime
			{
				get
				{
					return this._lastTime;
				}
				set
				{
					this._lastTime = value;
				}
			}

			// Token: 0x170011C3 RID: 4547
			// (get) Token: 0x06005665 RID: 22117 RVA: 0x00162A3C File Offset: 0x00161E3C
			// (set) Token: 0x06005666 RID: 22118 RVA: 0x00162A50 File Offset: 0x00161E50
			public ContainerVisual StrokeCV
			{
				get
				{
					return this._strokeCV;
				}
				set
				{
					this._strokeCV = value;
				}
			}

			// Token: 0x170011C4 RID: 4548
			// (get) Token: 0x06005667 RID: 22119 RVA: 0x00162A64 File Offset: 0x00161E64
			// (set) Token: 0x06005668 RID: 22120 RVA: 0x00162A78 File Offset: 0x00161E78
			public ContainerVisual StrokeRTICV
			{
				get
				{
					return this._strokeRTICV;
				}
				set
				{
					this._strokeRTICV = value;
				}
			}

			// Token: 0x170011C5 RID: 4549
			// (get) Token: 0x06005669 RID: 22121 RVA: 0x00162A8C File Offset: 0x00161E8C
			// (set) Token: 0x0600566A RID: 22122 RVA: 0x00162AA0 File Offset: 0x00161EA0
			public bool SeenUp
			{
				get
				{
					return this._seenUp;
				}
				set
				{
					this._seenUp = value;
				}
			}

			// Token: 0x170011C6 RID: 4550
			// (get) Token: 0x0600566B RID: 22123 RVA: 0x00162AB4 File Offset: 0x00161EB4
			// (set) Token: 0x0600566C RID: 22124 RVA: 0x00162AC8 File Offset: 0x00161EC8
			public bool IsReset
			{
				get
				{
					return this._isReset;
				}
				set
				{
					this._isReset = value;
				}
			}

			// Token: 0x170011C7 RID: 4551
			// (get) Token: 0x0600566D RID: 22125 RVA: 0x00162ADC File Offset: 0x00161EDC
			// (set) Token: 0x0600566E RID: 22126 RVA: 0x00162AF0 File Offset: 0x00161EF0
			public StrokeNodeIterator StrokeNodeIterator
			{
				get
				{
					return this._strokeNodeIterator;
				}
				set
				{
					if (value == null)
					{
						throw new ArgumentNullException("StrokeNodeIterator");
					}
					this._strokeNodeIterator = value;
				}
			}

			// Token: 0x170011C8 RID: 4552
			// (get) Token: 0x0600566F RID: 22127 RVA: 0x00162B14 File Offset: 0x00161F14
			// (set) Token: 0x06005670 RID: 22128 RVA: 0x00162B28 File Offset: 0x00161F28
			public SolidColorBrush FillBrush
			{
				get
				{
					return this._fillBrush;
				}
				set
				{
					this._fillBrush = value;
				}
			}

			// Token: 0x170011C9 RID: 4553
			// (get) Token: 0x06005671 RID: 22129 RVA: 0x00162B3C File Offset: 0x00161F3C
			public DrawingAttributes DrawingAttributes
			{
				get
				{
					return this._drawingAttributes;
				}
			}

			// Token: 0x170011CA RID: 4554
			// (get) Token: 0x06005672 RID: 22130 RVA: 0x00162B50 File Offset: 0x00161F50
			public double Opacity
			{
				get
				{
					return this._opacity;
				}
			}

			// Token: 0x170011CB RID: 4555
			// (get) Token: 0x06005673 RID: 22131 RVA: 0x00162B64 File Offset: 0x00161F64
			public DynamicRenderer.DynamicRendererHostVisual StrokeHV
			{
				get
				{
					return this._strokeHV;
				}
			}

			// Token: 0x06005674 RID: 22132 RVA: 0x00162B78 File Offset: 0x00161F78
			public bool IsTimestampWithin(int timestamp)
			{
				if (!this.SeenUp)
				{
					return true;
				}
				if (this.StartTime < this.LastTime)
				{
					return timestamp >= this.StartTime && timestamp <= this.LastTime;
				}
				return timestamp >= this.StartTime || timestamp <= this.LastTime;
			}

			// Token: 0x06005675 RID: 22133 RVA: 0x00162BCC File Offset: 0x00161FCC
			public bool IsTimestampAfter(int timestamp)
			{
				if (this.SeenUp)
				{
					return false;
				}
				if (this.LastTime >= this.StartTime)
				{
					return timestamp >= this.LastTime || (this.LastTime > 0 && timestamp < 0);
				}
				return timestamp >= this.LastTime && timestamp <= this.StartTime;
			}

			// Token: 0x0400279B RID: 10139
			private int _stylusId;

			// Token: 0x0400279C RID: 10140
			private int _startTime;

			// Token: 0x0400279D RID: 10141
			private int _lastTime;

			// Token: 0x0400279E RID: 10142
			private ContainerVisual _strokeCV;

			// Token: 0x0400279F RID: 10143
			private ContainerVisual _strokeRTICV;

			// Token: 0x040027A0 RID: 10144
			private bool _seenUp;

			// Token: 0x040027A1 RID: 10145
			private bool _isReset;

			// Token: 0x040027A2 RID: 10146
			private SolidColorBrush _fillBrush;

			// Token: 0x040027A3 RID: 10147
			private DrawingAttributes _drawingAttributes;

			// Token: 0x040027A4 RID: 10148
			private StrokeNodeIterator _strokeNodeIterator;

			// Token: 0x040027A5 RID: 10149
			private double _opacity;

			// Token: 0x040027A6 RID: 10150
			private DynamicRenderer.DynamicRendererHostVisual _strokeHV;
		}

		// Token: 0x02000829 RID: 2089
		private class DynamicRendererHostVisual : HostVisual
		{
			// Token: 0x170011CC RID: 4556
			// (get) Token: 0x06005676 RID: 22134 RVA: 0x00162C24 File Offset: 0x00162024
			internal bool InUse
			{
				get
				{
					return this._strokeInfoList.Count > 0;
				}
			}

			// Token: 0x170011CD RID: 4557
			// (get) Token: 0x06005677 RID: 22135 RVA: 0x00162C40 File Offset: 0x00162040
			internal bool HasSingleReference
			{
				get
				{
					return this._strokeInfoList.Count == 1;
				}
			}

			// Token: 0x06005678 RID: 22136 RVA: 0x00162C5C File Offset: 0x0016205C
			internal void AddStrokeInfoRef(DynamicRenderer.StrokeInfo si)
			{
				this._strokeInfoList.Add(si);
			}

			// Token: 0x06005679 RID: 22137 RVA: 0x00162C78 File Offset: 0x00162078
			internal void RemoveStrokeInfoRef(DynamicRenderer.StrokeInfo si)
			{
				this._strokeInfoList.Remove(si);
			}

			// Token: 0x170011CE RID: 4558
			// (get) Token: 0x0600567A RID: 22138 RVA: 0x00162C94 File Offset: 0x00162094
			internal VisualTarget VisualTarget
			{
				[SecuritySafeCritical]
				get
				{
					if (this._visualTarget == null)
					{
						this._visualTarget = new VisualTarget(this);
						this._visualTarget.RootVisual = new ContainerVisual();
					}
					return this._visualTarget;
				}
			}

			// Token: 0x040027A7 RID: 10151
			private VisualTarget _visualTarget;

			// Token: 0x040027A8 RID: 10152
			private List<DynamicRenderer.StrokeInfo> _strokeInfoList = new List<DynamicRenderer.StrokeInfo>();
		}
	}
}
