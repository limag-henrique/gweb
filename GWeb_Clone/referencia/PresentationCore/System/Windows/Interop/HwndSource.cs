using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.Automation;
using MS.Internal.Interop;
using MS.Internal.PresentationCore;
using MS.Utility;
using MS.Win32;

namespace System.Windows.Interop
{
	/// <summary>Apresenta conteúdo do WPF (Windows Presentation Foundation) em uma janela do Win32.</summary>
	// Token: 0x0200032B RID: 811
	public class HwndSource : PresentationSource, IDisposable, IWin32Window, IKeyboardInputSink
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Interop.HwndSource" /> com um estilo de classe, estilo, estilo estendido, posição x-y, nome e janela pai especificados.</summary>
		/// <param name="classStyle">Os estilos da classe Win32 para a janela.</param>
		/// <param name="style">Os estilos Win32 para a janela.</param>
		/// <param name="exStyle">Os estilos Win32 estendidos para a janela.</param>
		/// <param name="x">A posição da borda esquerda da janela.</param>
		/// <param name="y">A posição da borda superior da janela.</param>
		/// <param name="name">O nome da janela.</param>
		/// <param name="parent">O identificador da janela pai da janela.</param>
		// Token: 0x06001B57 RID: 6999 RVA: 0x0006D8E8 File Offset: 0x0006CCE8
		[SecurityCritical]
		public HwndSource(int classStyle, int style, int exStyle, int x, int y, string name, IntPtr parent)
		{
			SecurityHelper.DemandUIWindowPermission();
			HwndSourceParameters parameters = new HwndSourceParameters(name);
			parameters.WindowClassStyle = classStyle;
			parameters.WindowStyle = style;
			parameters.ExtendedWindowStyle = exStyle;
			parameters.SetPosition(x, y);
			parameters.ParentWindow = parent;
			this.Initialize(parameters);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Interop.HwndSource" /> com um estilo de classe, estilo, estilo estendido, posição x-y, largura, altura, nome e janela pai especificados e especificando se a janela tem dimensionamento automático.</summary>
		/// <param name="classStyle">Os estilos da classe Win32 para a janela.</param>
		/// <param name="style">Os estilos Win32 para a janela.</param>
		/// <param name="exStyle">Os estilos Win32 estendidos para a janela.</param>
		/// <param name="x">A posição da borda esquerda da janela.</param>
		/// <param name="y">A posição da borda superior da janela.</param>
		/// <param name="width">A largura da janela.</param>
		/// <param name="height">A altura da janela.</param>
		/// <param name="name">O nome da janela.</param>
		/// <param name="parent">O identificador da janela pai da janela.</param>
		/// <param name="adjustSizingForNonClientArea">
		///   <see langword="true" /> para que o gerenciador de layouts inclua a área não de cliente para dimensionamento; caso contrário, <see langword="false" />.</param>
		// Token: 0x06001B58 RID: 7000 RVA: 0x0006D93C File Offset: 0x0006CD3C
		[SecurityCritical]
		public HwndSource(int classStyle, int style, int exStyle, int x, int y, int width, int height, string name, IntPtr parent, bool adjustSizingForNonClientArea)
		{
			SecurityHelper.DemandUIWindowPermission();
			HwndSourceParameters parameters = new HwndSourceParameters(name, width, height);
			parameters.WindowClassStyle = classStyle;
			parameters.WindowStyle = style;
			parameters.ExtendedWindowStyle = exStyle;
			parameters.SetPosition(x, y);
			parameters.ParentWindow = parent;
			parameters.AdjustSizingForNonClientArea = adjustSizingForNonClientArea;
			this.Initialize(parameters);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Interop.HwndSource" /> com um estilo de classe, estilo, estilo estendido, posição x-y, largura, altura, nome e janela pai especificados.</summary>
		/// <param name="classStyle">Os estilos da classe Win32 para a janela.</param>
		/// <param name="style">Os estilos Win32 para a janela.</param>
		/// <param name="exStyle">Os estilos Win32 estendidos para a janela.</param>
		/// <param name="x">A posição da borda esquerda da janela.</param>
		/// <param name="y">A posição da borda superior da janela.</param>
		/// <param name="width">A largura da janela.</param>
		/// <param name="height">A altura da janela.</param>
		/// <param name="name">O nome da janela.</param>
		/// <param name="parent">O identificador da janela pai da janela.</param>
		// Token: 0x06001B59 RID: 7001 RVA: 0x0006D9A0 File Offset: 0x0006CDA0
		[SecurityCritical]
		public HwndSource(int classStyle, int style, int exStyle, int x, int y, int width, int height, string name, IntPtr parent)
		{
			SecurityHelper.DemandUIWindowPermission();
			HwndSourceParameters parameters = new HwndSourceParameters(name, width, height);
			parameters.WindowClassStyle = classStyle;
			parameters.WindowStyle = style;
			parameters.ExtendedWindowStyle = exStyle;
			parameters.SetPosition(x, y);
			parameters.ParentWindow = parent;
			this.Initialize(parameters);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Interop.HwndSource" /> usando uma estrutura que contém as configurações iniciais.</summary>
		/// <param name="parameters">Uma estrutura que contém os parâmetros necessários para criar a janela.</param>
		// Token: 0x06001B5A RID: 7002 RVA: 0x0006D9F8 File Offset: 0x0006CDF8
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		public HwndSource(HwndSourceParameters parameters)
		{
			this.Initialize(parameters);
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x0006DA14 File Offset: 0x0006CE14
		[SecurityCritical]
		private void Initialize(HwndSourceParameters parameters)
		{
			this._mouse = new SecurityCriticalDataClass<HwndMouseInputProvider>(new HwndMouseInputProvider(this));
			this._keyboard = new SecurityCriticalDataClass<HwndKeyboardInputProvider>(new HwndKeyboardInputProvider(this));
			this._layoutHook = new HwndWrapperHook(this.LayoutFilterMessage);
			this._inputHook = new HwndWrapperHook(this.InputFilterMessage);
			this._hwndTargetHook = new HwndWrapperHook(this.HwndTargetFilterMessage);
			this._publicHook = new HwndWrapperHook(this.PublicHooksFilterMessage);
			HwndWrapperHook[] array = new HwndWrapperHook[4];
			array[0] = this._hwndTargetHook;
			array[1] = this._layoutHook;
			array[2] = this._inputHook;
			HwndWrapperHook[] array2 = array;
			if (parameters.HwndSourceHook != null)
			{
				Delegate[] invocationList = parameters.HwndSourceHook.GetInvocationList();
				for (int i = invocationList.Length - 1; i >= 0; i--)
				{
					this._hooks += (HwndSourceHook)invocationList[i];
				}
				array2[3] = this._publicHook;
			}
			this._restoreFocusMode = parameters.RestoreFocusMode;
			this._acquireHwndFocusInMenuMode = parameters.AcquireHwndFocusInMenuMode;
			if (parameters.EffectivePerPixelOpacity)
			{
				parameters.ExtendedWindowStyle |= 524288;
			}
			else
			{
				parameters.ExtendedWindowStyle &= -524289;
			}
			this._constructionParameters = parameters;
			this._hwndWrapper = new HwndWrapper(parameters.WindowClassStyle, parameters.WindowStyle, parameters.ExtendedWindowStyle, parameters.PositionX, parameters.PositionY, parameters.Width, parameters.Height, parameters.WindowName, parameters.ParentWindow, array2);
			this._hwndTarget = new HwndTarget(this._hwndWrapper.Handle);
			this._hwndTarget.UsesPerPixelOpacity = parameters.EffectivePerPixelOpacity;
			if (this._hwndTarget.UsesPerPixelOpacity)
			{
				this._hwndTarget.BackgroundColor = Colors.Transparent;
				UnsafeNativeMethods.CriticalSetWindowTheme(new HandleRef(this, this._hwndWrapper.Handle), "", "");
			}
			this._constructionParameters = null;
			if (!parameters.HasAssignedSize)
			{
				this._sizeToContent = SizeToContent.WidthAndHeight;
			}
			this._adjustSizingForNonClientArea = parameters.AdjustSizingForNonClientArea;
			this._treatAncestorsAsNonClientArea = parameters.TreatAncestorsAsNonClientArea;
			this._weakShutdownHandler = new HwndSource.WeakEventDispatcherShutdown(this, base.Dispatcher);
			this._hwndWrapper.Disposed += this.OnHwndDisposed;
			if (StylusLogic.IsStylusAndTouchSupportEnabled)
			{
				if (StylusLogic.IsPointerStackEnabled)
				{
					this._stylus = new SecurityCriticalDataClass<IStylusInputProvider>(new HwndPointerInputProvider(this));
				}
				else
				{
					this._stylus = new SecurityCriticalDataClass<IStylusInputProvider>(new HwndStylusInputProvider(this));
				}
			}
			this._appCommand = new SecurityCriticalDataClass<HwndAppCommandInputProvider>(new HwndAppCommandInputProvider(this));
			if (parameters.TreatAsInputRoot)
			{
				this._weakPreprocessMessageHandler = new HwndSource.WeakEventPreprocessMessage(this, false);
			}
			base.AddSource();
			if (this._hwndWrapper.Handle != IntPtr.Zero && SecurityHelper.CallerHasPermissionWithAppDomainOptimization(new IPermission[]
			{
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode)
			}))
			{
				DragDrop.RegisterDropTarget(this._hwndWrapper.Handle);
				this._registeredDropTargetCount++;
			}
		}

		/// <summary>Libera todos os recursos gerenciados usados pelo <see cref="T:System.Windows.Interop.HwndSource" /> e aciona o evento <see cref="E:System.Windows.Interop.HwndSource.Disposed" />.</summary>
		// Token: 0x06001B5C RID: 7004 RVA: 0x0006DCF8 File Offset: 0x0006D0F8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Adiciona um manipulador de eventos que recebe todas as mensagens da janela.</summary>
		/// <param name="hook">A implementação do manipulador (com base no delegado <see cref="T:System.Windows.Interop.HwndSourceHook" />) que recebe as mensagens da janela.</param>
		// Token: 0x06001B5D RID: 7005 RVA: 0x0006DD14 File Offset: 0x0006D114
		[SecurityCritical]
		public void AddHook(HwndSourceHook hook)
		{
			SecurityHelper.DemandUIWindowPermission();
			Verify.IsNotNull<HwndSourceHook>(hook, "hook");
			this.CheckDisposed(true);
			if (this._hooks == null)
			{
				this._hwndWrapper.AddHook(this._publicHook);
			}
			this._hooks += hook;
		}

		/// <summary>Remove os manipuladores de eventos que foram adicionados por <see cref="M:System.Windows.Interop.HwndSource.AddHook(System.Windows.Interop.HwndSourceHook)" />.</summary>
		/// <param name="hook">O manipulador de eventos a ser removido.</param>
		// Token: 0x06001B5E RID: 7006 RVA: 0x0006DD58 File Offset: 0x0006D158
		[SecurityCritical]
		public void RemoveHook(HwndSourceHook hook)
		{
			SecurityHelper.DemandUIWindowPermission();
			this._hooks -= hook;
			if (this._hooks == null)
			{
				this._hwndWrapper.RemoveHook(this._publicHook);
			}
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x0006DD8C File Offset: 0x0006D18C
		[SecurityCritical]
		internal override IInputProvider GetInputProvider(Type inputDevice)
		{
			if (inputDevice == typeof(MouseDevice))
			{
				if (this._mouse == null)
				{
					return null;
				}
				return this._mouse.Value;
			}
			else if (inputDevice == typeof(KeyboardDevice))
			{
				if (this._keyboard == null)
				{
					return null;
				}
				return this._keyboard.Value;
			}
			else
			{
				if (!(inputDevice == typeof(StylusDevice)))
				{
					return null;
				}
				if (this._stylus == null)
				{
					return null;
				}
				return this._stylus.Value;
			}
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x0006DE14 File Offset: 0x0006D214
		internal void ChangeDpi(HwndDpiChangedEventArgs e)
		{
			this.OnDpiChanged(e);
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x0006DE28 File Offset: 0x0006D228
		internal void ChangeDpi(HwndDpiChangedAfterParentEventArgs e)
		{
			this.OnDpiChangedAfterParent(e);
		}

		/// <summary>Chamado quando o DPI for alterado para a janela.</summary>
		/// <param name="e">Os argumentos de evento</param>
		// Token: 0x06001B62 RID: 7010 RVA: 0x0006DE3C File Offset: 0x0006D23C
		[SecuritySafeCritical]
		protected virtual void OnDpiChanged(HwndDpiChangedEventArgs e)
		{
			HwndDpiChangedEventHandler dpiChanged = this.DpiChanged;
			if (dpiChanged != null)
			{
				dpiChanged(this, e);
			}
			if (!e.Handled)
			{
				HwndTarget hwndTarget = this._hwndTarget;
				if (hwndTarget != null)
				{
					hwndTarget.OnDpiChanged(e);
				}
				if (this.IsLayoutActive())
				{
					base.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(this.SetLayoutSize));
					base.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new DispatcherOperationCallback(PresentationSource.FireContentRendered), this);
					return;
				}
				InputManager.SafeCurrentNotifyHitTestInvalidated();
			}
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x0006DEB8 File Offset: 0x0006D2B8
		[SecuritySafeCritical]
		private void OnDpiChangedAfterParent(HwndDpiChangedAfterParentEventArgs e)
		{
			if (this._hwndTarget != null)
			{
				HwndDpiChangedEventArgs hwndDpiChangedEventArgs = (HwndDpiChangedEventArgs)e;
				HwndDpiChangedEventHandler dpiChanged = this.DpiChanged;
				if (dpiChanged != null)
				{
					dpiChanged(this, hwndDpiChangedEventArgs);
				}
				if (!hwndDpiChangedEventArgs.Handled)
				{
					HwndTarget hwndTarget = this._hwndTarget;
					if (hwndTarget != null)
					{
						hwndTarget.OnDpiChangedAfterParent(e);
					}
				}
				if (this.IsLayoutActive())
				{
					base.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(this.SetLayoutSize));
					base.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new DispatcherOperationCallback(PresentationSource.FireContentRendered), this);
					return;
				}
				InputManager.SafeCurrentNotifyHitTestInvalidated();
			}
		}

		/// <summary>Ocorre quando o método <see cref="M:System.Windows.Interop.HwndSource.Dispose" /> é chamado neste objeto.</summary>
		// Token: 0x14000174 RID: 372
		// (add) Token: 0x06001B64 RID: 7012 RVA: 0x0006DF48 File Offset: 0x0006D348
		// (remove) Token: 0x06001B65 RID: 7013 RVA: 0x0006DF80 File Offset: 0x0006D380
		public event EventHandler Disposed;

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.Interop.HwndSource.SizeToContent" /> muda.</summary>
		// Token: 0x14000175 RID: 373
		// (add) Token: 0x06001B66 RID: 7014 RVA: 0x0006DFB8 File Offset: 0x0006D3B8
		// (remove) Token: 0x06001B67 RID: 7015 RVA: 0x0006DFF0 File Offset: 0x0006D3F0
		public event EventHandler SizeToContentChanged;

		/// <summary>Ocorre quando o valor de DPIs do monitor deste HWND é alterado ou quando o HWND é movido para um monitor com um valor de DPIs diferente.</summary>
		// Token: 0x14000176 RID: 374
		// (add) Token: 0x06001B68 RID: 7016 RVA: 0x0006E028 File Offset: 0x0006D428
		// (remove) Token: 0x06001B69 RID: 7017 RVA: 0x0006E060 File Offset: 0x0006D460
		public event HwndDpiChangedEventHandler DpiChanged;

		/// <summary>Obtém um valor que indica se <see cref="M:System.Windows.Interop.HwndSource.Dispose" /> foi chamado neste <see cref="T:System.Windows.Interop.HwndSource" />.</summary>
		/// <returns>
		///   <see langword="true" /> Se o objeto tiver sido <see cref="M:System.Windows.Interop.HwndSource.Dispose" /> chamado nele; caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x0006E098 File Offset: 0x0006D498
		public override bool IsDisposed
		{
			get
			{
				return this._isDisposed;
			}
		}

		/// <summary>Obtém ou define o <see cref="P:System.Windows.Media.CompositionTarget.RootVisual" /> da janela.</summary>
		/// <returns>O objeto visual raiz da janela.</returns>
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x0006E0AC File Offset: 0x0006D4AC
		// (set) Token: 0x06001B6C RID: 7020 RVA: 0x0006E0D0 File Offset: 0x0006D4D0
		public override Visual RootVisual
		{
			[SecurityCritical]
			get
			{
				if (this._isDisposed)
				{
					return null;
				}
				return this._rootVisual.Value;
			}
			[SecurityCritical]
			[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
			set
			{
				this.CheckDisposed(true);
				this.RootVisualInternal = value;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (set) Token: 0x06001B6D RID: 7021 RVA: 0x0006E0EC File Offset: 0x0006D4EC
		private Visual RootVisualInternal
		{
			[SecurityCritical]
			set
			{
				if (this._rootVisual.Value != value)
				{
					Visual value2 = this._rootVisual.Value;
					if (value != null)
					{
						this._rootVisual.Value = value;
						if (this._rootVisual.Value is UIElement)
						{
							((UIElement)this._rootVisual.Value).LayoutUpdated += this.OnLayoutUpdated;
						}
						if (this._hwndTarget != null && !this._hwndTarget.IsDisposed)
						{
							this._hwndTarget.RootVisual = this._rootVisual.Value;
						}
						UIElement.PropagateResumeLayout(null, value);
					}
					else
					{
						this._rootVisual.Value = null;
						if (this._hwndTarget != null && !this._hwndTarget.IsDisposed)
						{
							this._hwndTarget.RootVisual = null;
						}
					}
					if (value2 != null)
					{
						if (value2 is UIElement)
						{
							((UIElement)value2).LayoutUpdated -= this.OnLayoutUpdated;
						}
						UIElement.PropagateSuspendLayout(value2);
					}
					base.RootChanged(value2, this._rootVisual.Value);
					if (this.IsLayoutActive())
					{
						this.SetLayoutSize();
						base.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new DispatcherOperationCallback(PresentationSource.FireContentRendered), this);
					}
					else
					{
						InputManager.SafeCurrentNotifyHitTestInvalidated();
					}
					if (this._keyboard != null)
					{
						this._keyboard.Value.OnRootChanged(value2, this._rootVisual.Value);
					}
				}
				if (value != null && this._hwndTarget != null && !this._hwndTarget.IsDisposed && EventMap.HasListeners)
				{
					this._hwndTarget.EnsureAutomationPeer(value);
				}
			}
		}

		/// <summary>Obtém uma sequência de coletores de entrada registrados.</summary>
		/// <returns>Coletores de uma enumeração de entrada do teclado.</returns>
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x0006E274 File Offset: 0x0006D674
		public IEnumerable<IKeyboardInputSink> ChildKeyboardInputSinks
		{
			get
			{
				if (this._keyboardInputSinkChildren != null)
				{
					foreach (IKeyboardInputSite keyboardInputSite in this._keyboardInputSinkChildren)
					{
						yield return keyboardInputSite.Sink;
					}
					List<HwndSourceKeyboardInputSite>.Enumerator enumerator = default(List<HwndSourceKeyboardInputSite>.Enumerator);
				}
				yield break;
				yield break;
			}
		}

		/// <summary>Retorna o objeto <see cref="T:System.Windows.Interop.HwndSource" /> da janela especificada.</summary>
		/// <param name="hwnd">O identificador da janela fornecido.</param>
		/// <returns>O objeto <see cref="T:System.Windows.Interop.HwndSource" /> para a janela especificada pelo identificador de janela <paramref name="hwnd" />.</returns>
		// Token: 0x06001B6F RID: 7023 RVA: 0x0006E294 File Offset: 0x0006D694
		[SecurityCritical]
		public static HwndSource FromHwnd(IntPtr hwnd)
		{
			SecurityHelper.DemandUIWindowPermission();
			return HwndSource.CriticalFromHwnd(hwnd);
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x0006E2AC File Offset: 0x0006D6AC
		[SecurityCritical]
		internal static HwndSource CriticalFromHwnd(IntPtr hwnd)
		{
			if (hwnd == IntPtr.Zero)
			{
				throw new ArgumentException(SR.Get("NullHwnd"));
			}
			HwndSource result = null;
			foreach (object obj in PresentationSource.CriticalCurrentSources)
			{
				PresentationSource presentationSource = (PresentationSource)obj;
				HwndSource hwndSource = presentationSource as HwndSource;
				if (hwndSource != null && hwndSource.CriticalHandle == hwnd)
				{
					if (!hwndSource.IsDisposed)
					{
						result = hwndSource;
						break;
					}
					break;
				}
			}
			return result;
		}

		/// <summary>Obtém o gerenciador visual para a janela hospedada.</summary>
		/// <returns>O Gerenciador visual.</returns>
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x0006E350 File Offset: 0x0006D750
		public new HwndTarget CompositionTarget
		{
			[SecurityCritical]
			[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
			get
			{
				if (this._isDisposed)
				{
					return null;
				}
				if (this._hwndTarget != null && this._hwndTarget.IsDisposed)
				{
					return null;
				}
				return this._hwndTarget;
			}
		}

		/// <summary>Obtém o destino visual da janela.</summary>
		/// <returns>Retorna o destino visual da janela.</returns>
		// Token: 0x06001B72 RID: 7026 RVA: 0x0006E384 File Offset: 0x0006D784
		[SecurityCritical]
		protected override CompositionTarget GetCompositionTargetCore()
		{
			return this.CompositionTarget;
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x0006E398 File Offset: 0x0006D798
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal override void OnEnterMenuMode()
		{
			this.IsInExclusiveMenuMode = !this._acquireHwndFocusInMenuMode;
			if (this.IsInExclusiveMenuMode)
			{
				this._weakMenuModeMessageHandler = new HwndSource.WeakEventPreprocessMessage(this, true);
				UnsafeNativeMethods.HideCaret(new HandleRef(this, IntPtr.Zero));
			}
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x0006E3DC File Offset: 0x0006D7DC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal override void OnLeaveMenuMode()
		{
			if (this.IsInExclusiveMenuMode)
			{
				this._weakMenuModeMessageHandler.Dispose();
				this._weakMenuModeMessageHandler = null;
				UnsafeNativeMethods.ShowCaret(new HandleRef(this, IntPtr.Zero));
			}
			this.IsInExclusiveMenuMode = false;
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x0006E41C File Offset: 0x0006D81C
		// (set) Token: 0x06001B76 RID: 7030 RVA: 0x0006E430 File Offset: 0x0006D830
		internal bool IsInExclusiveMenuMode { get; private set; }

		/// <summary>Ocorre quando o layout faz com que o <see cref="T:System.Windows.Interop.HwndSource" /> seja redimensionado automaticamente.</summary>
		// Token: 0x14000177 RID: 375
		// (add) Token: 0x06001B77 RID: 7031 RVA: 0x0006E444 File Offset: 0x0006D844
		// (remove) Token: 0x06001B78 RID: 7032 RVA: 0x0006E47C File Offset: 0x0006D87C
		public event AutoResizedEventHandler AutoResized;

		// Token: 0x06001B79 RID: 7033 RVA: 0x0006E4B4 File Offset: 0x0006D8B4
		[SecurityCritical]
		private void OnLayoutUpdated(object obj, EventArgs args)
		{
			UIElement uielement = this._rootVisual.Value as UIElement;
			if (uielement != null)
			{
				Size renderSize = uielement.RenderSize;
				if (this._previousSize == null || !DoubleUtil.AreClose(this._previousSize.Value.Width, renderSize.Width) || !DoubleUtil.AreClose(this._previousSize.Value.Height, renderSize.Height))
				{
					this._previousSize = new Size?(renderSize);
					if (this._sizeToContent != SizeToContent.Manual && !this._isWindowInMinimizeState)
					{
						this.Resize(renderSize);
					}
				}
			}
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x0006E554 File Offset: 0x0006D954
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void Resize(Size newSize)
		{
			try
			{
				this._myOwnUpdate = true;
				if (this.IsUsable)
				{
					NativeMethods.RECT rect = this.AdjustWindowSize(newSize);
					int cx = rect.right - rect.left;
					int cy = rect.bottom - rect.top;
					UnsafeNativeMethods.SetWindowPos(new HandleRef(this, this._hwndWrapper.Handle), new HandleRef(null, IntPtr.Zero), 0, 0, cx, cy, 22);
					if (this.AutoResized != null)
					{
						this.AutoResized(this, new AutoResizedEventArgs(newSize));
					}
				}
			}
			finally
			{
				this._myOwnUpdate = false;
			}
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x0006E5FC File Offset: 0x0006D9FC
		[SecurityCritical]
		internal void ShowSystemMenu()
		{
			IntPtr ancestor = UnsafeNativeMethods.GetAncestor(new HandleRef(this, this.CriticalHandle), 2);
			UnsafeNativeMethods.PostMessage(new HandleRef(this, ancestor), WindowMessage.WM_SYSCOMMAND, new IntPtr(61696), new IntPtr(32));
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x0006E640 File Offset: 0x0006DA40
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal Point TransformToDevice(Point pt)
		{
			return this._hwndTarget.TransformToDevice.Transform(pt);
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x0006E664 File Offset: 0x0006DA64
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal Point TransformFromDevice(Point pt)
		{
			return this._hwndTarget.TransformFromDevice.Transform(pt);
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x0006E688 File Offset: 0x0006DA88
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private NativeMethods.RECT AdjustWindowSize(Size newSize)
		{
			Point point = this.TransformToDevice(new Point(newSize.Width, newSize.Height));
			this.RoundDeviceSize(ref point);
			NativeMethods.RECT result = new NativeMethods.RECT(0, 0, (int)point.X, (int)point.Y);
			if (!this._adjustSizingForNonClientArea && !this.UsesPerPixelOpacity)
			{
				int dwStyle = NativeMethods.IntPtrToInt32((IntPtr)SafeNativeMethods.GetWindowStyle(new HandleRef(this, this._hwndWrapper.Handle), false));
				int dwExStyle = NativeMethods.IntPtrToInt32((IntPtr)SafeNativeMethods.GetWindowStyle(new HandleRef(this, this._hwndWrapper.Handle), true));
				SafeNativeMethods.AdjustWindowRectEx(ref result, dwStyle, false, dwExStyle);
			}
			return result;
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x0006E730 File Offset: 0x0006DB30
		private void RoundDeviceSize(ref Point size)
		{
			UIElement uielement = this._rootVisual.Value as UIElement;
			if (uielement != null && uielement.SnapsToDevicePixels)
			{
				size = new Point((double)DoubleUtil.DoubleToInt(size.X), (double)DoubleUtil.DoubleToInt(size.Y));
				return;
			}
			size = new Point(Math.Ceiling(size.X), Math.Ceiling(size.Y));
		}

		/// <summary>Obtém o identificador de janela para este <see cref="T:System.Windows.Interop.HwndSource" />.</summary>
		/// <returns>O identificador da janela.</returns>
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x0006E7A0 File Offset: 0x0006DBA0
		public IntPtr Handle
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandUIWindowPermission();
				return this.CriticalHandle;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x0006E7B8 File Offset: 0x0006DBB8
		internal IntPtr CriticalHandle
		{
			[SecurityCritical]
			[FriendAccessAllowed]
			get
			{
				if (this._hwndWrapper != null)
				{
					return this._hwndWrapper.Handle;
				}
				return IntPtr.Zero;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x0006E7E0 File Offset: 0x0006DBE0
		internal HwndWrapper HwndWrapper
		{
			[SecurityCritical]
			get
			{
				return this._hwndWrapper;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x0006E7F4 File Offset: 0x0006DBF4
		internal bool HasCapture
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				IntPtr capture = SafeNativeMethods.GetCapture();
				return capture == this.CriticalHandle;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001B84 RID: 7044 RVA: 0x0006E814 File Offset: 0x0006DC14
		internal bool IsHandleNull
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				return this._hwndWrapper.Handle == IntPtr.Zero;
			}
		}

		/// <summary>Obtém o identificador de janela para o <see cref="T:System.Windows.Interop.HwndSource" />. O identificador de janela é empacotado como parte de uma estrutura <see cref="T:System.Runtime.InteropServices.HandleRef" />.</summary>
		/// <returns>Uma estrutura que contém o identificador de janela para este <see cref="T:System.Windows.Interop.HwndSource" />.</returns>
		// Token: 0x06001B85 RID: 7045 RVA: 0x0006E838 File Offset: 0x0006DC38
		public HandleRef CreateHandleRef()
		{
			return new HandleRef(this, this.Handle);
		}

		/// <summary>Obtém ou define se a janela é dimensionada para seu conteúdo e como ela é dimensionada.</summary>
		/// <returns>Um dos valores de enumeração. O valor padrão é <see cref="F:System.Windows.SizeToContent.Manual" />, que especifica que a janela não é dimensionada para seu conteúdo.</returns>
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x0006E854 File Offset: 0x0006DC54
		// (set) Token: 0x06001B87 RID: 7047 RVA: 0x0006E870 File Offset: 0x0006DC70
		public SizeToContent SizeToContent
		{
			get
			{
				this.CheckDisposed(true);
				return this._sizeToContent;
			}
			set
			{
				this.CheckDisposed(true);
				if (!HwndSource.IsValidSizeToContent(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(SizeToContent));
				}
				if (this._sizeToContent == value)
				{
					return;
				}
				this._sizeToContent = value;
				if (this.IsLayoutActive())
				{
					this.SetLayoutSize();
				}
			}
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x0006E8C4 File Offset: 0x0006DCC4
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private bool IsLayoutActive()
		{
			return this._rootVisual.Value is UIElement && this._hwndTarget != null && !this._hwndTarget.IsDisposed;
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x0006E8FC File Offset: 0x0006DCFC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void SetLayoutSize()
		{
			UIElement uielement = this._rootVisual.Value as UIElement;
			if (uielement == null)
			{
				return;
			}
			uielement.InvalidateMeasure();
			bool flag = EventTrace.IsEnabled(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info);
			long num = 0L;
			if (this._sizeToContent == SizeToContent.WidthAndHeight)
			{
				Size availableSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
				if (flag)
				{
					num = this._hwndWrapper.Handle.ToInt64();
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientLayoutBegin, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, new object[]
					{
						num,
						EventTrace.LayoutSource.HwndSource_SetLayoutSize
					});
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientMeasureBegin, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, num);
				}
				uielement.Measure(availableSize);
				if (flag)
				{
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientMeasureEnd, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, 1);
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientArrangeBegin, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, num);
				}
				uielement.Arrange(new Rect(default(Point), uielement.DesiredSize));
				if (flag)
				{
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientArrangeEnd, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, 1);
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientLayoutEnd, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info);
				}
			}
			else
			{
				Size sizeFromHwnd = this.GetSizeFromHwnd();
				Size size = new Size((this._sizeToContent == SizeToContent.Width) ? double.PositiveInfinity : sizeFromHwnd.Width, (this._sizeToContent == SizeToContent.Height) ? double.PositiveInfinity : sizeFromHwnd.Height);
				if (flag)
				{
					num = this._hwndWrapper.Handle.ToInt64();
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientLayoutBegin, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, new object[]
					{
						num,
						EventTrace.LayoutSource.HwndSource_SetLayoutSize
					});
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientMeasureBegin, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, num);
				}
				uielement.Measure(size);
				if (flag)
				{
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientMeasureEnd, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, 1);
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientArrangeBegin, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, num);
				}
				if (this._sizeToContent == SizeToContent.Width)
				{
					size = new Size(uielement.DesiredSize.Width, sizeFromHwnd.Height);
				}
				else if (this._sizeToContent == SizeToContent.Height)
				{
					size = new Size(sizeFromHwnd.Width, uielement.DesiredSize.Height);
				}
				else
				{
					size = sizeFromHwnd;
				}
				uielement.Arrange(new Rect(default(Point), size));
				if (flag)
				{
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientArrangeEnd, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, 1);
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientLayoutEnd, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info);
				}
			}
			uielement.UpdateLayout();
		}

		/// <summary>Obtém um valor que declara se a opacidade por pixel da janela do conteúdo de origem é respeitada.</summary>
		/// <returns>
		///   <see langword="true" /> Se o sistema usa opacidade por pixel; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001B8A RID: 7050 RVA: 0x0006EBE4 File Offset: 0x0006DFE4
		public bool UsesPerPixelOpacity
		{
			[SecurityCritical]
			get
			{
				this.CheckDisposed(true);
				HwndTarget compositionTarget = this.CompositionTarget;
				return this._hwndTarget != null && this._hwndTarget.UsesPerPixelOpacity;
			}
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x0006EC14 File Offset: 0x0006E014
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private Size GetSizeFromHwnd()
		{
			NativeMethods.RECT rect = new NativeMethods.RECT(0, 0, 0, 0);
			if (this._adjustSizingForNonClientArea)
			{
				this.GetNonClientRect(ref rect);
			}
			else
			{
				SafeNativeMethods.GetClientRect(new HandleRef(this, this._hwndWrapper.Handle), ref rect);
			}
			Point point = this.TransformFromDevice(new Point((double)(rect.right - rect.left), (double)(rect.bottom - rect.top)));
			return new Size(point.X, point.Y);
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x0006EC94 File Offset: 0x0006E094
		[SecurityCritical]
		private IntPtr HwndTargetFilterMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			IntPtr intPtr = IntPtr.Zero;
			if (this.IsUsable)
			{
				HwndTarget hwndTarget = this._hwndTarget;
				if (hwndTarget != null)
				{
					intPtr = hwndTarget.HandleMessage((WindowMessage)msg, wParam, lParam);
					if (intPtr != IntPtr.Zero)
					{
						handled = true;
					}
				}
			}
			return intPtr;
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x0006ECD8 File Offset: 0x0006E0D8
		[SecurityCritical]
		private IntPtr LayoutFilterMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			IntPtr zero = IntPtr.Zero;
			UIElement uielement = this._rootVisual.Value as UIElement;
			if (this.IsUsable && uielement != null)
			{
				if (msg <= 70)
				{
					if (msg != 5)
					{
						if (msg == 70)
						{
							this.Process_WM_WINDOWPOSCHANGING(uielement, hwnd, (WindowMessage)msg, wParam, lParam);
						}
					}
					else
					{
						this.Process_WM_SIZE(uielement, hwnd, (WindowMessage)msg, wParam, lParam);
					}
				}
				else if (msg != 274)
				{
					if (msg == 532)
					{
						this.DisableSizeToContent(uielement, hwnd);
					}
				}
				else
				{
					int num = NativeMethods.IntPtrToInt32(wParam) & 65520;
					if (num == 61488 || num == 61440)
					{
						this.DisableSizeToContent(uielement, hwnd);
					}
				}
			}
			if (!handled && (this._constructionParameters != null || this.IsUsable))
			{
				bool flag = (this._constructionParameters != null) ? ((HwndSourceParameters)this._constructionParameters).EffectivePerPixelOpacity : this._hwndTarget.UsesPerPixelOpacity;
				if (msg == 131 && flag)
				{
					if (wParam == IntPtr.Zero)
					{
						zero = IntPtr.Zero;
						handled = true;
					}
					else
					{
						zero = IntPtr.Zero;
						handled = true;
					}
				}
			}
			return zero;
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x0006EDE8 File Offset: 0x0006E1E8
		[SecurityCritical]
		private void Process_WM_WINDOWPOSCHANGING(UIElement rootUIElement, IntPtr hwnd, WindowMessage msg, IntPtr wParam, IntPtr lParam)
		{
			if (!this._myOwnUpdate && this.SizeToContent != SizeToContent.Manual)
			{
				NativeMethods.RECT rect = this.AdjustWindowSize(rootUIElement.RenderSize);
				int num = rect.right - rect.left;
				int num2 = rect.bottom - rect.top;
				NativeMethods.WINDOWPOS windowpos = (NativeMethods.WINDOWPOS)UnsafeNativeMethods.PtrToStructure(lParam, typeof(NativeMethods.WINDOWPOS));
				bool flag = false;
				if ((windowpos.flags & 1) == 1)
				{
					NativeMethods.RECT rect2 = new NativeMethods.RECT(0, 0, 0, 0);
					SafeNativeMethods.GetWindowRect(new HandleRef(this, this._hwndWrapper.Handle), ref rect2);
					if (num != rect2.right - rect2.left || num2 != rect2.bottom - rect2.top)
					{
						windowpos.flags &= -2;
						windowpos.cx = num;
						windowpos.cy = num2;
						flag = true;
					}
				}
				else
				{
					bool flag2 = this.SizeToContent != SizeToContent.Height;
					bool flag3 = this.SizeToContent != SizeToContent.Width;
					if (flag2 && windowpos.cx != num)
					{
						windowpos.cx = num;
						flag = true;
					}
					if (flag3 && windowpos.cy != num2)
					{
						windowpos.cy = num2;
						flag = true;
					}
				}
				if (flag)
				{
					Marshal.StructureToPtr(windowpos, lParam, true);
				}
			}
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x0006EF24 File Offset: 0x0006E324
		[SecurityCritical]
		private void Process_WM_SIZE(UIElement rootUIElement, IntPtr hwnd, WindowMessage msg, IntPtr wParam, IntPtr lParam)
		{
			int num = NativeMethods.SignedLOWORD(lParam);
			int num2 = NativeMethods.SignedHIWORD(lParam);
			Point point = new Point((double)num, (double)num2);
			bool flag = EventTrace.IsEnabled(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info);
			long num3 = 0L;
			this._isWindowInMinimizeState = (NativeMethods.IntPtrToInt32(wParam) == 1);
			if (!this._myOwnUpdate && this._sizeToContent != SizeToContent.WidthAndHeight && !this._isWindowInMinimizeState)
			{
				Point pt = new Point(point.X, point.Y);
				if (this._adjustSizingForNonClientArea)
				{
					NativeMethods.RECT rect = new NativeMethods.RECT(0, 0, (int)point.X, (int)point.Y);
					this.GetNonClientRect(ref rect);
					pt.X = (double)rect.Width;
					pt.Y = (double)rect.Height;
				}
				pt = this.TransformFromDevice(pt);
				Size size = new Size((this._sizeToContent == SizeToContent.Width) ? double.PositiveInfinity : pt.X, (this._sizeToContent == SizeToContent.Height) ? double.PositiveInfinity : pt.Y);
				if (this._adjustSizingForNonClientArea)
				{
					rootUIElement.InvalidateMeasure();
				}
				if (flag)
				{
					num3 = this._hwndWrapper.Handle.ToInt64();
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientLayoutBegin, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, new object[]
					{
						num3,
						EventTrace.LayoutSource.HwndSource_WMSIZE
					});
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientMeasureBegin, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, num3);
				}
				rootUIElement.Measure(size);
				if (this._sizeToContent == SizeToContent.Width)
				{
					size = new Size(rootUIElement.DesiredSize.Width, pt.Y);
				}
				else if (this._sizeToContent == SizeToContent.Height)
				{
					size = new Size(pt.X, rootUIElement.DesiredSize.Height);
				}
				else
				{
					size = new Size(pt.X, pt.Y);
				}
				if (flag)
				{
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientMeasureEnd, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, 1);
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientArrangeBegin, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, num3);
				}
				rootUIElement.Arrange(new Rect(default(Point), size));
				if (flag)
				{
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientArrangeEnd, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info, 1);
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientLayoutEnd, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordLayout, EventTrace.Level.Info);
				}
				rootUIElement.UpdateLayout();
			}
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x0006F1A0 File Offset: 0x0006E5A0
		[SecurityCritical]
		private void DisableSizeToContent(UIElement rootUIElement, IntPtr hwnd)
		{
			if (this._sizeToContent != SizeToContent.Manual)
			{
				this._sizeToContent = SizeToContent.Manual;
				Size sizeFromHwnd = this.GetSizeFromHwnd();
				rootUIElement.Measure(sizeFromHwnd);
				rootUIElement.Arrange(new Rect(default(Point), sizeFromHwnd));
				rootUIElement.UpdateLayout();
				if (this.SizeToContentChanged != null)
				{
					this.SizeToContentChanged(this, EventArgs.Empty);
				}
			}
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x0006F200 File Offset: 0x0006E600
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void GetNonClientRect(ref NativeMethods.RECT rc)
		{
			IntPtr handle = IntPtr.Zero;
			if (this._treatAncestorsAsNonClientArea)
			{
				handle = UnsafeNativeMethods.GetAncestor(new HandleRef(this, this.CriticalHandle), 2);
			}
			else
			{
				handle = this.CriticalHandle;
			}
			SafeNativeMethods.GetWindowRect(new HandleRef(this, handle), ref rc);
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x0006F244 File Offset: 0x0006E644
		[SecurityCritical]
		private IntPtr InputFilterMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			IntPtr result = IntPtr.Zero;
			if (msg == 2)
			{
				this.DisposeStylusInputProvider();
			}
			if (!this._isDisposed && this._stylus != null && !handled)
			{
				result = this._stylus.Value.FilterMessage(hwnd, (WindowMessage)msg, wParam, lParam, ref handled);
			}
			if (!this._isDisposed && this._mouse != null && !handled)
			{
				result = this._mouse.Value.FilterMessage(hwnd, (WindowMessage)msg, wParam, lParam, ref handled);
			}
			if (!this._isDisposed && this._keyboard != null && !handled)
			{
				result = this._keyboard.Value.FilterMessage(hwnd, (WindowMessage)msg, wParam, lParam, ref handled);
				if (msg - 256 <= 7)
				{
					this._lastKeyboardMessage = default(MSG);
				}
			}
			if (!this._isDisposed && this._appCommand != null && !handled)
			{
				result = this._appCommand.Value.FilterMessage(hwnd, (WindowMessage)msg, wParam, lParam, ref handled);
			}
			return result;
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x0006F330 File Offset: 0x0006E730
		[SecurityCritical]
		private IntPtr PublicHooksFilterMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			IntPtr result = IntPtr.Zero;
			if (this._hooks != null)
			{
				Delegate[] invocationList = this._hooks.GetInvocationList();
				for (int i = invocationList.Length - 1; i >= 0; i--)
				{
					HwndSourceHook hwndSourceHook = (HwndSourceHook)invocationList[i];
					result = hwndSourceHook(hwnd, msg, wParam, lParam, ref handled);
					if (handled)
					{
						break;
					}
				}
			}
			if (msg != 2)
			{
				if (msg == 130)
				{
					this.OnNoMoreWindowMessages();
				}
			}
			else
			{
				this.DisposeStylusInputProvider();
			}
			return result;
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x0006F3A4 File Offset: 0x0006E7A4
		[SecurityCritical]
		private void DisposeStylusInputProvider()
		{
			if (this._stylus != null)
			{
				SecurityCriticalDataClass<IStylusInputProvider> stylus = this._stylus;
				this._stylus = null;
				stylus.Value.Dispose();
			}
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x0006F3D4 File Offset: 0x0006E7D4
		[SecurityCritical]
		private void OnPreprocessMessageThunk(ref MSG msg, ref bool handled)
		{
			if (handled)
			{
				return;
			}
			WindowMessage message = (WindowMessage)msg.message;
			if (message - WindowMessage.WM_KEYFIRST <= 7)
			{
				HwndSource.MSGDATA msgdata = new HwndSource.MSGDATA();
				msgdata.msg = msg;
				msgdata.handled = handled;
				object obj = Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Send, new DispatcherOperationCallback(this.OnPreprocessMessage), msgdata);
				if (obj != null)
				{
					handled = (bool)obj;
				}
				msg = msgdata.msg;
			}
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x0006F444 File Offset: 0x0006E844
		[SecurityCritical]
		private object OnPreprocessMessage(object param)
		{
			HwndSource.MSGDATA msgdata = (HwndSource.MSGDATA)param;
			if (!((IKeyboardInputSink)this).HasFocusWithin() && !this.IsInExclusiveMenuMode)
			{
				return msgdata.handled;
			}
			ModifierKeys systemModifierKeys = HwndKeyboardInputProvider.GetSystemModifierKeys();
			switch (msgdata.msg.message)
			{
			case 256:
			case 260:
			{
				HwndSource._eatCharMessages = true;
				DispatcherOperation dispatcherOperation = base.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new DispatcherOperationCallback(HwndSource.RestoreCharMessages), null);
				base.Dispatcher.CriticalRequestProcessing(true);
				msgdata.handled = this.CriticalTranslateAccelerator(ref msgdata.msg, systemModifierKeys);
				if (!msgdata.handled)
				{
					HwndSource._eatCharMessages = false;
					dispatcherOperation.Abort();
				}
				if (this.IsInExclusiveMenuMode)
				{
					if (!msgdata.handled)
					{
						UnsafeNativeMethods.TranslateMessage(ref msgdata.msg);
					}
					msgdata.handled = true;
				}
				break;
			}
			case 257:
			case 261:
				msgdata.handled = this.CriticalTranslateAccelerator(ref msgdata.msg, systemModifierKeys);
				if (this.IsInExclusiveMenuMode)
				{
					msgdata.handled = true;
				}
				break;
			case 258:
			case 259:
			case 262:
			case 263:
				if (!HwndSource._eatCharMessages)
				{
					new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
					try
					{
						msgdata.handled = ((IKeyboardInputSink)this).TranslateChar(ref msgdata.msg, systemModifierKeys);
						if (!msgdata.handled)
						{
							msgdata.handled = ((IKeyboardInputSink)this).OnMnemonic(ref msgdata.msg, systemModifierKeys);
						}
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					if (!msgdata.handled)
					{
						this._keyboard.Value.ProcessTextInputAction(msgdata.msg.hwnd, (WindowMessage)msgdata.msg.message, msgdata.msg.wParam, msgdata.msg.lParam, ref msgdata.handled);
					}
				}
				if (this.IsInExclusiveMenuMode)
				{
					if (!msgdata.handled)
					{
						SafeNativeMethods.MessageBeep(0);
					}
					msgdata.handled = true;
				}
				break;
			}
			return msgdata.handled;
		}

		/// <summary>Registra a interface <see cref="T:System.Windows.Interop.IKeyboardInputSink" /> de um componente independente.</summary>
		/// <param name="sink">O coletor <see cref="T:System.Windows.Interop.IKeyboardInputSink" /> do componente independente.</param>
		/// <returns>O site <see cref="T:System.Windows.Interop.IKeyboardInputSite" /> do componente independente.</returns>
		// Token: 0x06001B97 RID: 7063 RVA: 0x0006F63C File Offset: 0x0006EA3C
		[SecurityCritical]
		[UIPermission(SecurityAction.Demand, Unrestricted = true)]
		protected IKeyboardInputSite RegisterKeyboardInputSinkCore(IKeyboardInputSink sink)
		{
			this.CheckDisposed(true);
			if (sink == null)
			{
				throw new ArgumentNullException("sink");
			}
			if (sink.KeyboardInputSite != null)
			{
				throw new ArgumentException(SR.Get("KeyboardSinkAlreadyOwned"));
			}
			HwndSourceKeyboardInputSite hwndSourceKeyboardInputSite = new HwndSourceKeyboardInputSite(this, sink);
			if (this._keyboardInputSinkChildren == null)
			{
				this._keyboardInputSinkChildren = new List<HwndSourceKeyboardInputSite>();
			}
			this._keyboardInputSinkChildren.Add(hwndSourceKeyboardInputSite);
			return hwndSourceKeyboardInputSite;
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Windows.Interop.IKeyboardInputSink.RegisterKeyboardInputSink(System.Windows.Interop.IKeyboardInputSink)" />.</summary>
		/// <param name="sink">O coletor <see cref="T:System.Windows.Interop.IKeyboardInputSink" /> do componente independente.</param>
		/// <returns>O site <see cref="T:System.Windows.Interop.IKeyboardInputSite" /> do componente independente.</returns>
		// Token: 0x06001B98 RID: 7064 RVA: 0x0006F6A0 File Offset: 0x0006EAA0
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		IKeyboardInputSite IKeyboardInputSink.RegisterKeyboardInputSink(IKeyboardInputSink sink)
		{
			return this.RegisterKeyboardInputSinkCore(sink);
		}

		/// <summary>Processa a entrada do teclado no nível da mensagem por pressionamento de tecla.</summary>
		/// <param name="msg">A mensagem e seus dados associados. Não modifique esta estrutura. É passado por referência apenas por razões de desempenho.</param>
		/// <param name="modifiers">Teclas modificadoras.</param>
		/// <returns>
		///   <see langword="true" /> se a mensagem tiver sido manipulada pela implementação de método; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001B99 RID: 7065 RVA: 0x0006F6B4 File Offset: 0x0006EAB4
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		protected virtual bool TranslateAcceleratorCore(ref MSG msg, ModifierKeys modifiers)
		{
			SecurityHelper.DemandUnmanagedCode();
			return this.CriticalTranslateAccelerator(ref msg, modifiers);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Windows.Interop.IKeyboardInputSink.TranslateAccelerator(System.Windows.Interop.MSG@,System.Windows.Input.ModifierKeys)" />.</summary>
		/// <param name="msg">A mensagem e seus dados associados. Não modifique esta estrutura. É passado por referência apenas por razões de desempenho.</param>
		/// <param name="modifiers">Teclas modificadoras.</param>
		/// <returns>
		///   <see langword="true" /> se a mensagem tiver sido manipulada pela implementação de método; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001B9A RID: 7066 RVA: 0x0006F6D0 File Offset: 0x0006EAD0
		[SecurityCritical]
		bool IKeyboardInputSink.TranslateAccelerator(ref MSG msg, ModifierKeys modifiers)
		{
			return this.TranslateAcceleratorCore(ref msg, modifiers);
		}

		/// <summary>Define se o foco estará na primeira parada de tabulação ou na última parada de tabulação do coletor.</summary>
		/// <param name="request">Especifica se o foco deve ser definido para a primeira ou a última parada de tabulação.</param>
		/// <returns>
		///   <see langword="true" /> se o foco tiver sido definido como solicitado; <see langword="false" />, se não houver nenhuma parada de tabulação.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="request" /> é <see langword="null" />.</exception>
		// Token: 0x06001B9B RID: 7067 RVA: 0x0006F6E8 File Offset: 0x0006EAE8
		protected virtual bool TabIntoCore(TraversalRequest request)
		{
			bool result = false;
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			UIElement uielement = this._rootVisual.Value as UIElement;
			if (uielement != null)
			{
				result = uielement.MoveFocus(request);
			}
			return result;
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Windows.Interop.IKeyboardInputSink.TabInto(System.Windows.Input.TraversalRequest)" />.</summary>
		/// <param name="request">Especifica se o foco deve ser definido para a primeira ou a última parada de tabulação.</param>
		/// <returns>
		///   <see langword="true" /> se o foco tiver sido definido como solicitado; <see langword="false" />, se não houver nenhuma parada de tabulação.</returns>
		// Token: 0x06001B9C RID: 7068 RVA: 0x0006F724 File Offset: 0x0006EB24
		bool IKeyboardInputSink.TabInto(TraversalRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			return this.TabIntoCore(request);
		}

		/// <summary>Obtém ou define uma referência à interface <see cref="T:System.Windows.Interop.IKeyboardInputSite" /> do contêiner do componente.</summary>
		/// <returns>Uma referência para o recipiente <see cref="T:System.Windows.Interop.IKeyboardInputSite" /> interface; ou <see langword="null" /> não se for atribuído a nenhum site. O padrão é <see langword="null" />.</returns>
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x0006F748 File Offset: 0x0006EB48
		// (set) Token: 0x06001B9E RID: 7070 RVA: 0x0006F760 File Offset: 0x0006EB60
		protected IKeyboardInputSite KeyboardInputSiteCore
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandUnmanagedCode();
				return this._keyboardInputSite;
			}
			[SecurityCritical]
			[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			set
			{
				SecurityHelper.DemandUnmanagedCode();
				this._keyboardInputSite = value;
			}
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="P:System.Windows.Interop.IKeyboardInputSink.KeyboardInputSite" />.</summary>
		/// <returns>Uma referência para o contêiner <see cref="T:System.Windows.Interop.IKeyboardInputSite" /> interface.</returns>
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x0006F77C File Offset: 0x0006EB7C
		// (set) Token: 0x06001BA0 RID: 7072 RVA: 0x0006F790 File Offset: 0x0006EB90
		IKeyboardInputSite IKeyboardInputSink.KeyboardInputSite
		{
			get
			{
				return this.KeyboardInputSiteCore;
			}
			[SecurityCritical]
			set
			{
				this.KeyboardInputSiteCore = value;
			}
		}

		/// <summary>Chamado quando uma das teclas mnemônicas (teclas de acesso) para esse coletor é invocada.</summary>
		/// <param name="msg">A mensagem para os dados associados e mnemônicos.</param>
		/// <param name="modifiers">Teclas modificadoras.</param>
		/// <returns>
		///   <see langword="true" /> se a mensagem foi manipulada; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="msg" /> não é WM_KEYDOWN, WM_SYSKEYDOWN, WM_CHAR ou WM_DEADCHAR.</exception>
		// Token: 0x06001BA1 RID: 7073 RVA: 0x0006F7A4 File Offset: 0x0006EBA4
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		protected virtual bool OnMnemonicCore(ref MSG msg, ModifierKeys modifiers)
		{
			SecurityHelper.DemandUnmanagedCode();
			WindowMessage message = (WindowMessage)msg.message;
			if (message - WindowMessage.WM_CHAR > 1)
			{
				if (message - WindowMessage.WM_SYSCHAR > 1)
				{
					throw new ArgumentException(SR.Get("OnlyAcceptsKeyMessages"));
				}
				string text = new string((char)((int)msg.wParam), 1);
				if (text != null && text.Length > 0)
				{
					DependencyObject dependencyObject = Keyboard.FocusedElement as DependencyObject;
					HwndSource hwndSource = (dependencyObject == null) ? null : (PresentationSource.CriticalFromVisual(dependencyObject) as HwndSource);
					if (hwndSource != null && hwndSource != this && this.IsInExclusiveMenuMode)
					{
						return ((IKeyboardInputSink)hwndSource).OnMnemonic(ref msg, modifiers);
					}
					if (AccessKeyManager.IsKeyRegistered(this, text))
					{
						AccessKeyManager.ProcessKey(this, text, false);
						return true;
					}
				}
			}
			this._lastKeyboardMessage = msg;
			if (this._keyboardInputSinkChildren != null && !this.IsInExclusiveMenuMode)
			{
				foreach (HwndSourceKeyboardInputSite hwndSourceKeyboardInputSite in this._keyboardInputSinkChildren)
				{
					if (((IKeyboardInputSite)hwndSourceKeyboardInputSite).Sink.OnMnemonic(ref msg, modifiers))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Windows.Interop.IKeyboardInputSink.OnMnemonic(System.Windows.Interop.MSG@,System.Windows.Input.ModifierKeys)" />.</summary>
		/// <param name="msg">A mensagem para os dados associados e mnemônicos. Não modifique a estrutura dessa mensagem. É passado por referência apenas por razões de desempenho.</param>
		/// <param name="modifiers">Teclas modificadoras.</param>
		/// <returns>
		///   <see langword="true" /> se a mensagem foi manipulada; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001BA2 RID: 7074 RVA: 0x0006F8D0 File Offset: 0x0006ECD0
		[SecurityCritical]
		bool IKeyboardInputSink.OnMnemonic(ref MSG msg, ModifierKeys modifiers)
		{
			return this.OnMnemonicCore(ref msg, modifiers);
		}

		/// <summary>Processa as mensagens de entrada WM_CHAR, WM_SYSCHAR, WM_DEADCHAR e WM_SYSDEADCHAR antes que o método <see cref="M:System.Windows.Interop.IKeyboardInputSink.OnMnemonic(System.Windows.Interop.MSG@,System.Windows.Input.ModifierKeys)" /> seja chamado.</summary>
		/// <param name="msg">A mensagem e seus dados associados. Não modifique esta estrutura. É passado por referência apenas por razões de desempenho.</param>
		/// <param name="modifiers">Teclas modificadoras.</param>
		/// <returns>
		///   <see langword="true" /> se a mensagem foi processada e <see cref="M:System.Windows.Interop.IKeyboardInputSink.OnMnemonic(System.Windows.Interop.MSG@,System.Windows.Input.ModifierKeys)" /> não deve ser chamado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001BA3 RID: 7075 RVA: 0x0006F8E8 File Offset: 0x0006ECE8
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		protected virtual bool TranslateCharCore(ref MSG msg, ModifierKeys modifiers)
		{
			SecurityHelper.DemandUnmanagedCode();
			if (this.HasFocus || this.IsInExclusiveMenuMode)
			{
				return false;
			}
			IKeyboardInputSink childSinkWithFocus = this.ChildSinkWithFocus;
			return childSinkWithFocus != null && childSinkWithFocus.TranslateChar(ref msg, modifiers);
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Windows.Interop.IKeyboardInputSink.TranslateChar(System.Windows.Interop.MSG@,System.Windows.Input.ModifierKeys)" />.</summary>
		/// <param name="msg">A mensagem e seus dados associados. Não modifique esta estrutura. É passado por referência apenas por razões de desempenho.</param>
		/// <param name="modifiers">Teclas modificadoras.</param>
		/// <returns>
		///   <see langword="true" /> se a mensagem foi processada e <see cref="M:System.Windows.Interop.IKeyboardInputSink.OnMnemonic(System.Windows.Interop.MSG@,System.Windows.Input.ModifierKeys)" /> não deve ser chamado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001BA4 RID: 7076 RVA: 0x0006F920 File Offset: 0x0006ED20
		[SecurityCritical]
		bool IKeyboardInputSink.TranslateChar(ref MSG msg, ModifierKeys modifiers)
		{
			return this.TranslateCharCore(ref msg, modifiers);
		}

		/// <summary>Obtém um valor que indica se o coletor ou um de seus componentes independentes tem foco.</summary>
		/// <returns>
		///   <see langword="true" /> se o coletor ou um de seus componentes independentes tiver foco; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001BA5 RID: 7077 RVA: 0x0006F938 File Offset: 0x0006ED38
		protected virtual bool HasFocusWithinCore()
		{
			if (this.HasFocus)
			{
				return true;
			}
			if (this._keyboardInputSinkChildren == null)
			{
				return false;
			}
			foreach (HwndSourceKeyboardInputSite hwndSourceKeyboardInputSite in this._keyboardInputSinkChildren)
			{
				if (((IKeyboardInputSite)hwndSourceKeyboardInputSite).Sink.HasFocusWithin())
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.Windows.Media.FamilyTypefaceCollection.System#Collections#IList#Remove(System.Object)" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o coletor ou um de seus componentes independentes tiver foco; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001BA6 RID: 7078 RVA: 0x0006F9B8 File Offset: 0x0006EDB8
		bool IKeyboardInputSink.HasFocusWithin()
		{
			return this.HasFocusWithinCore();
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.RestoreFocusMode" /> para a janela.</summary>
		/// <returns>O <see cref="T:System.Windows.Input.RestoreFocusMode" /> para a janela.</returns>
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x0006F9CC File Offset: 0x0006EDCC
		public RestoreFocusMode RestoreFocusMode
		{
			get
			{
				return this._restoreFocusMode;
			}
		}

		/// <summary>Obtém ou define o valor <see cref="P:System.Windows.Interop.HwndSource.AcquireHwndFocusInMenuMode" /> padrão para novas instâncias de <see cref="T:System.Windows.Interop.HwndSource" />.</summary>
		/// <returns>
		///   <see langword="true" /> para adquirir o foco do Win32 para a janela que contém quando o usuário interage com menus; o WPF Caso contrário, <see langword="false" />. O padrão é <see langword="true" />.</returns>
		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x0006F9E0 File Offset: 0x0006EDE0
		// (set) Token: 0x06001BA9 RID: 7081 RVA: 0x0006FA10 File Offset: 0x0006EE10
		public static bool DefaultAcquireHwndFocusInMenuMode
		{
			get
			{
				if (HwndSource._defaultAcquireHwndFocusInMenuMode == null)
				{
					HwndSource._defaultAcquireHwndFocusInMenuMode = new bool?(true);
				}
				return HwndSource._defaultAcquireHwndFocusInMenuMode.Value;
			}
			set
			{
				HwndSource._defaultAcquireHwndFocusInMenuMode = new bool?(value);
			}
		}

		/// <summary>Obtém o valor que determina se é necessário adquirir o foco do Win32 para a janela que contém o WPF para este <see cref="T:System.Windows.Interop.HwndSource" />.</summary>
		/// <returns>
		///   <see langword="true" /> para adquirir o foco do Win32 para a janela que contém quando o usuário interage com menus; o WPF Caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x0006FA28 File Offset: 0x0006EE28
		public bool AcquireHwndFocusInMenuMode
		{
			get
			{
				return this._acquireHwndFocusInMenuMode;
			}
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0006FA3C File Offset: 0x0006EE3C
		[SecurityCritical]
		internal void CriticalUnregisterKeyboardInputSink(HwndSourceKeyboardInputSite site)
		{
			if (this._isDisposed)
			{
				return;
			}
			if (this._keyboardInputSinkChildren != null && !this._keyboardInputSinkChildren.Remove(site))
			{
				throw new InvalidOperationException(SR.Get("KeyboardSinkNotAChild"));
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x0006FA78 File Offset: 0x0006EE78
		private IKeyboardInputSink ChildSinkWithFocus
		{
			get
			{
				IKeyboardInputSink result = null;
				if (this._keyboardInputSinkChildren == null)
				{
					return null;
				}
				foreach (HwndSourceKeyboardInputSite hwndSourceKeyboardInputSite in this._keyboardInputSinkChildren)
				{
					IKeyboardInputSite keyboardInputSite = hwndSourceKeyboardInputSite;
					if (keyboardInputSite.Sink.HasFocusWithin())
					{
						result = keyboardInputSite.Sink;
						break;
					}
				}
				return result;
			}
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x0006FAF8 File Offset: 0x0006EEF8
		[FriendAccessAllowed]
		[SecurityCritical]
		internal bool CriticalTranslateAccelerator(ref MSG msg, ModifierKeys modifiers)
		{
			WindowMessage message = (WindowMessage)msg.message;
			if (message - WindowMessage.WM_KEYFIRST > 1 && message - WindowMessage.WM_SYSKEYDOWN > 1)
			{
				throw new ArgumentException(SR.Get("OnlyAcceptsKeyMessages"));
			}
			if (this._keyboard == null)
			{
				return false;
			}
			bool flag = false;
			if (HwndSource.PerThreadData.TranslateAcceleratorCallDepth == 0)
			{
				this._lastKeyboardMessage = msg;
				if (this.HasFocus || this.IsInExclusiveMenuMode)
				{
					this._keyboard.Value.ProcessKeyAction(ref msg, ref flag);
					return flag;
				}
				IKeyboardInputSink childSinkWithFocus = this.ChildSinkWithFocus;
				IInputElement forceTarget = (IInputElement)childSinkWithFocus;
				try
				{
					HwndSource.PerThreadData.TranslateAcceleratorCallDepth++;
					Keyboard.PrimaryDevice.ForceTarget = forceTarget;
					this._keyboard.Value.ProcessKeyAction(ref msg, ref flag);
					return flag;
				}
				finally
				{
					Keyboard.PrimaryDevice.ForceTarget = null;
					HwndSource.PerThreadData.TranslateAcceleratorCallDepth--;
				}
			}
			int virtualKey = HwndKeyboardInputProvider.GetVirtualKey(msg.wParam, msg.lParam);
			int scanCode = HwndKeyboardInputProvider.GetScanCode(msg.wParam, msg.lParam);
			bool isExtendedKey = HwndKeyboardInputProvider.IsExtendedKey(msg.lParam);
			Key key = KeyInterop.KeyFromVirtualKey(virtualKey);
			RoutedEvent routedEvent = null;
			RoutedEvent routedEvent2 = null;
			switch (msg.message)
			{
			case 256:
			case 260:
				routedEvent = Keyboard.PreviewKeyDownEvent;
				routedEvent2 = Keyboard.KeyDownEvent;
				break;
			case 257:
			case 261:
				routedEvent = Keyboard.PreviewKeyUpEvent;
				routedEvent2 = Keyboard.KeyUpEvent;
				break;
			}
			bool hasFocus = this.HasFocus;
			IKeyboardInputSink keyboardInputSink = (hasFocus || this.IsInExclusiveMenuMode) ? null : this.ChildSinkWithFocus;
			IInputElement inputElement = keyboardInputSink as IInputElement;
			if (inputElement == null && hasFocus)
			{
				inputElement = Keyboard.PrimaryDevice.FocusedElement;
				if (inputElement != null && PresentationSource.CriticalFromVisual((DependencyObject)inputElement) != this)
				{
					inputElement = null;
				}
			}
			try
			{
				Keyboard.PrimaryDevice.ForceTarget = (keyboardInputSink as IInputElement);
				if (inputElement != null)
				{
					KeyEventArgs keyEventArgs = new KeyEventArgs(Keyboard.PrimaryDevice, this, msg.time, key);
					keyEventArgs.ScanCode = scanCode;
					keyEventArgs.IsExtendedKey = isExtendedKey;
					keyEventArgs.RoutedEvent = routedEvent;
					inputElement.RaiseEvent(keyEventArgs);
					flag = keyEventArgs.Handled;
				}
				if (!flag)
				{
					KeyEventArgs keyEventArgs2 = new KeyEventArgs(Keyboard.PrimaryDevice, this, msg.time, key);
					keyEventArgs2.ScanCode = scanCode;
					keyEventArgs2.IsExtendedKey = isExtendedKey;
					keyEventArgs2.RoutedEvent = routedEvent2;
					if (inputElement != null)
					{
						inputElement.RaiseEvent(keyEventArgs2);
						flag = keyEventArgs2.Handled;
					}
					if (!flag)
					{
						InputManager.UnsecureCurrent.RaiseTranslateAccelerator(keyEventArgs2);
						flag = keyEventArgs2.Handled;
					}
				}
			}
			finally
			{
				Keyboard.PrimaryDevice.ForceTarget = null;
			}
			return flag;
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x0006FDAC File Offset: 0x0006F1AC
		internal static object RestoreCharMessages(object unused)
		{
			HwndSource._eatCharMessages = false;
			return null;
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x0006FDC0 File Offset: 0x0006F1C0
		internal bool IsRepeatedKeyboardMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam)
		{
			return msg == this._lastKeyboardMessage.message && !(hwnd != this._lastKeyboardMessage.hwnd) && !(wParam != this._lastKeyboardMessage.wParam) && !(lParam != this._lastKeyboardMessage.lParam);
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x0006FE20 File Offset: 0x0006F220
		private void OnHwndDisposed(object sender, EventArgs args)
		{
			this._inRealHwndDispose = true;
			this.Dispose();
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x0006FE3C File Offset: 0x0006F23C
		private void OnNoMoreWindowMessages()
		{
			this._hooks = null;
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x0006FE50 File Offset: 0x0006F250
		private void OnShutdownFinished(object sender, EventArgs args)
		{
			this.Dispose();
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x0006FE64 File Offset: 0x0006F264
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposing)
			{
				this._isDisposing = true;
				if (this.Disposed != null)
				{
					try
					{
						this.Disposed(this, EventArgs.Empty);
					}
					catch
					{
					}
					this.Disposed = null;
				}
				base.ClearContentRenderedListeners();
				this.RootVisualInternal = null;
				base.RemoveSource();
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
				try
				{
					IKeyboardInputSite keyboardInputSite = ((IKeyboardInputSink)this).KeyboardInputSite;
					if (keyboardInputSite != null)
					{
						keyboardInputSite.Unregister();
						((IKeyboardInputSink)this).KeyboardInputSite = null;
					}
					this._keyboardInputSinkChildren = null;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				if (!this._inRealHwndDispose)
				{
					this.DisposeStylusInputProvider();
				}
				if (this._hwndTarget != null)
				{
					this._hwndTarget.Dispose();
					this._hwndTarget = null;
				}
				if (this._hwndWrapper != null)
				{
					if (this._hwndWrapper.Handle != IntPtr.Zero && this._registeredDropTargetCount > 0)
					{
						DragDrop.RevokeDropTarget(this._hwndWrapper.Handle);
						this._registeredDropTargetCount--;
					}
					this._hwndWrapper.Disposed -= this.OnHwndDisposed;
					if (!this._inRealHwndDispose)
					{
						this._hwndWrapper.Dispose();
					}
				}
				if (this._mouse != null)
				{
					this._mouse.Value.Dispose();
					this._mouse = null;
				}
				if (this._keyboard != null)
				{
					this._keyboard.Value.Dispose();
					this._keyboard = null;
				}
				if (this._appCommand != null)
				{
					this._appCommand.Value.Dispose();
					this._appCommand = null;
				}
				if (this._weakShutdownHandler != null)
				{
					this._weakShutdownHandler.Dispose();
					this._weakShutdownHandler = null;
				}
				if (this._weakPreprocessMessageHandler != null)
				{
					this._weakPreprocessMessageHandler.Dispose();
					this._weakPreprocessMessageHandler = null;
				}
				this._isDisposed = true;
			}
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x00070058 File Offset: 0x0006F458
		private void CheckDisposed(bool verifyAccess)
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(null, SR.Get("HwndSourceDisposed"));
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x00070080 File Offset: 0x0006F480
		private bool IsUsable
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				return !this._isDisposed && this._hwndTarget != null && !this._hwndTarget.IsDisposed;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x000700B0 File Offset: 0x0006F4B0
		private bool HasFocus
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				return UnsafeNativeMethods.GetFocus() == this.CriticalHandle;
			}
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x000700D0 File Offset: 0x0006F4D0
		private static bool IsValidSizeToContent(SizeToContent value)
		{
			return value == SizeToContent.Manual || value == SizeToContent.Width || value == SizeToContent.Height || value == SizeToContent.WidthAndHeight;
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x000700F0 File Offset: 0x0006F4F0
		private static HwndSource.ThreadDataBlob PerThreadData
		{
			get
			{
				object data = Thread.GetData(HwndSource._threadSlot);
				HwndSource.ThreadDataBlob threadDataBlob;
				if (data == null)
				{
					threadDataBlob = new HwndSource.ThreadDataBlob();
					Thread.SetData(HwndSource._threadSlot, threadDataBlob);
				}
				else
				{
					threadDataBlob = (HwndSource.ThreadDataBlob)data;
				}
				return threadDataBlob;
			}
		}

		// Token: 0x14000178 RID: 376
		// (add) Token: 0x06001BB9 RID: 7097 RVA: 0x00070128 File Offset: 0x0006F528
		// (remove) Token: 0x06001BBA RID: 7098 RVA: 0x00070160 File Offset: 0x0006F560
		private event HwndSourceHook _hooks;

		// Token: 0x04000EB2 RID: 3762
		private object _constructionParameters;

		// Token: 0x04000EB3 RID: 3763
		private bool _isDisposed;

		// Token: 0x04000EB4 RID: 3764
		private bool _isDisposing;

		// Token: 0x04000EB5 RID: 3765
		private bool _inRealHwndDispose;

		// Token: 0x04000EB6 RID: 3766
		private bool _adjustSizingForNonClientArea;

		// Token: 0x04000EB7 RID: 3767
		private bool _treatAncestorsAsNonClientArea;

		// Token: 0x04000EB8 RID: 3768
		private bool _myOwnUpdate;

		// Token: 0x04000EB9 RID: 3769
		private bool _isWindowInMinimizeState;

		// Token: 0x04000EBA RID: 3770
		private int _registeredDropTargetCount;

		// Token: 0x04000EBB RID: 3771
		private SizeToContent _sizeToContent;

		// Token: 0x04000EBC RID: 3772
		private Size? _previousSize;

		// Token: 0x04000EBD RID: 3773
		[SecurityCritical]
		private HwndWrapper _hwndWrapper;

		// Token: 0x04000EBE RID: 3774
		[SecurityCritical]
		private HwndTarget _hwndTarget;

		// Token: 0x04000EBF RID: 3775
		private SecurityCriticalDataForSet<Visual> _rootVisual;

		// Token: 0x04000EC1 RID: 3777
		private SecurityCriticalDataClass<HwndMouseInputProvider> _mouse;

		// Token: 0x04000EC2 RID: 3778
		private SecurityCriticalDataClass<HwndKeyboardInputProvider> _keyboard;

		// Token: 0x04000EC3 RID: 3779
		private SecurityCriticalDataClass<IStylusInputProvider> _stylus;

		// Token: 0x04000EC4 RID: 3780
		private SecurityCriticalDataClass<HwndAppCommandInputProvider> _appCommand;

		// Token: 0x04000EC5 RID: 3781
		private HwndSource.WeakEventDispatcherShutdown _weakShutdownHandler;

		// Token: 0x04000EC6 RID: 3782
		private HwndSource.WeakEventPreprocessMessage _weakPreprocessMessageHandler;

		// Token: 0x04000EC7 RID: 3783
		private HwndSource.WeakEventPreprocessMessage _weakMenuModeMessageHandler;

		// Token: 0x04000EC8 RID: 3784
		private static LocalDataStoreSlot _threadSlot = Thread.AllocateDataSlot();

		// Token: 0x04000EC9 RID: 3785
		private RestoreFocusMode _restoreFocusMode;

		// Token: 0x04000ECA RID: 3786
		[ThreadStatic]
		private static bool? _defaultAcquireHwndFocusInMenuMode;

		// Token: 0x04000ECB RID: 3787
		private bool _acquireHwndFocusInMenuMode;

		// Token: 0x04000ECC RID: 3788
		private MSG _lastKeyboardMessage;

		// Token: 0x04000ECD RID: 3789
		private List<HwndSourceKeyboardInputSite> _keyboardInputSinkChildren;

		// Token: 0x04000ECE RID: 3790
		[SecurityCritical]
		private IKeyboardInputSite _keyboardInputSite;

		// Token: 0x04000ECF RID: 3791
		[SecurityCritical]
		private HwndWrapperHook _layoutHook;

		// Token: 0x04000ED0 RID: 3792
		[SecurityCritical]
		private HwndWrapperHook _inputHook;

		// Token: 0x04000ED1 RID: 3793
		[SecurityCritical]
		private HwndWrapperHook _hwndTargetHook;

		// Token: 0x04000ED2 RID: 3794
		[SecurityCritical]
		private HwndWrapperHook _publicHook;

		// Token: 0x04000ED3 RID: 3795
		[ThreadStatic]
		internal static bool _eatCharMessages;

		// Token: 0x0200084D RID: 2125
		private class MSGDATA
		{
			// Token: 0x04002806 RID: 10246
			public MSG msg;

			// Token: 0x04002807 RID: 10247
			public bool handled;
		}

		// Token: 0x0200084E RID: 2126
		private class ThreadDataBlob
		{
			// Token: 0x04002808 RID: 10248
			public int TranslateAcceleratorCallDepth;
		}

		// Token: 0x0200084F RID: 2127
		private class WeakEventDispatcherShutdown : WeakReference
		{
			// Token: 0x060056EF RID: 22255 RVA: 0x00163FA8 File Offset: 0x001633A8
			public WeakEventDispatcherShutdown(HwndSource source, Dispatcher that) : base(source)
			{
				this._that = that;
				this._that.ShutdownFinished += this.OnShutdownFinished;
			}

			// Token: 0x060056F0 RID: 22256 RVA: 0x00163FDC File Offset: 0x001633DC
			public void OnShutdownFinished(object sender, EventArgs e)
			{
				HwndSource hwndSource = this.Target as HwndSource;
				if (hwndSource != null)
				{
					hwndSource.OnShutdownFinished(sender, e);
					return;
				}
				this.Dispose();
			}

			// Token: 0x060056F1 RID: 22257 RVA: 0x00164008 File Offset: 0x00163408
			public void Dispose()
			{
				if (this._that != null)
				{
					this._that.ShutdownFinished -= this.OnShutdownFinished;
				}
			}

			// Token: 0x04002809 RID: 10249
			private Dispatcher _that;
		}

		// Token: 0x02000850 RID: 2128
		private class WeakEventPreprocessMessage : WeakReference
		{
			// Token: 0x060056F2 RID: 22258 RVA: 0x00164034 File Offset: 0x00163434
			[SecurityCritical]
			public WeakEventPreprocessMessage(HwndSource source, bool addToFront) : base(source)
			{
				this._addToFront = addToFront;
				this._handler = new ThreadMessageEventHandler(this.OnPreprocessMessage);
				if (addToFront)
				{
					ComponentDispatcher.CriticalAddThreadPreprocessMessageHandlerFirst(this._handler);
					return;
				}
				ComponentDispatcher.ThreadPreprocessMessage += this._handler;
			}

			// Token: 0x060056F3 RID: 22259 RVA: 0x0016407C File Offset: 0x0016347C
			[SecurityCritical]
			public void OnPreprocessMessage(ref MSG msg, ref bool handled)
			{
				HwndSource hwndSource = this.Target as HwndSource;
				if (hwndSource != null)
				{
					hwndSource.OnPreprocessMessageThunk(ref msg, ref handled);
					return;
				}
				this.Dispose();
			}

			// Token: 0x060056F4 RID: 22260 RVA: 0x001640A8 File Offset: 0x001634A8
			[SecurityCritical]
			[SecurityTreatAsSafe]
			public void Dispose()
			{
				if (this._addToFront)
				{
					ComponentDispatcher.CriticalRemoveThreadPreprocessMessageHandlerFirst(this._handler);
				}
				else
				{
					ComponentDispatcher.ThreadPreprocessMessage -= this._handler;
				}
				this._handler = null;
			}

			// Token: 0x0400280A RID: 10250
			private bool _addToFront;

			// Token: 0x0400280B RID: 10251
			private ThreadMessageEventHandler _handler;
		}
	}
}
