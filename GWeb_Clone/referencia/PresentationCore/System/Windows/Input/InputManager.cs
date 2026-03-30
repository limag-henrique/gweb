using System;
using System.Collections;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.Permissions;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Gerencia todos os sistemas de entra na WPF (Windows Presentation Foundation).</summary>
	// Token: 0x0200024D RID: 589
	public sealed class InputManager : DispatcherObject
	{
		/// <summary>Obtém o <see cref="T:System.Windows.Input.InputManager" /> associado ao thread atual.</summary>
		/// <returns>O Gerenciador de entrada.</returns>
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x0003D948 File Offset: 0x0003CD48
		public static InputManager Current
		{
			[SecurityCritical]
			[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				return InputManager.GetCurrentInputManagerImpl();
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x0003D95C File Offset: 0x0003CD5C
		internal static InputManager UnsecureCurrent
		{
			[FriendAccessAllowed]
			[SecurityCritical]
			get
			{
				return InputManager.GetCurrentInputManagerImpl();
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x0003D970 File Offset: 0x0003CD70
		internal static bool IsSynchronizedInput
		{
			get
			{
				return InputManager._isSynchronizedInput;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x0003D984 File Offset: 0x0003CD84
		internal static RoutedEvent[] SynchronizedInputEvents
		{
			get
			{
				return InputManager._synchronizedInputEvents;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x0003D998 File Offset: 0x0003CD98
		internal static RoutedEvent[] PairedSynchronizedInputEvents
		{
			get
			{
				return InputManager._pairedSynchronizedInputEvents;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x0003D9AC File Offset: 0x0003CDAC
		internal static SynchronizedInputType SynchronizeInputType
		{
			get
			{
				return InputManager._synchronizedInputType;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0003D9C0 File Offset: 0x0003CDC0
		internal static DependencyObject ListeningElement
		{
			get
			{
				return InputManager._listeningElement;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x0003D9D4 File Offset: 0x0003CDD4
		// (set) Token: 0x06001066 RID: 4198 RVA: 0x0003D9E8 File Offset: 0x0003CDE8
		internal static SynchronizedInputStates SynchronizedInputState
		{
			get
			{
				return InputManager._synchronizedInputState;
			}
			set
			{
				InputManager._synchronizedInputState = value;
			}
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0003D9FC File Offset: 0x0003CDFC
		[SecurityCritical]
		private static InputManager GetCurrentInputManagerImpl()
		{
			Dispatcher currentDispatcher = Dispatcher.CurrentDispatcher;
			InputManager inputManager = currentDispatcher.InputManager as InputManager;
			if (inputManager == null)
			{
				inputManager = new InputManager();
				currentDispatcher.InputManager = inputManager;
			}
			return inputManager;
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0003DA30 File Offset: 0x0003CE30
		[SecurityCritical]
		private InputManager()
		{
			if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
			{
				throw new InvalidOperationException(SR.Get("RequiresSTA"));
			}
			this._stagingArea = new Stack();
			this._primaryKeyboardDevice = new Win32KeyboardDevice(this);
			this._primaryMouseDevice = new Win32MouseDevice(this);
			this._primaryCommandDevice = new CommandDevice(this);
			this._continueProcessingStagingAreaCallback = new DispatcherOperationCallback(this.ContinueProcessingStagingArea);
			this._hitTestInvalidatedAsyncOperation = null;
			this._hitTestInvalidatedAsyncCallback = new DispatcherOperationCallback(this.HitTestInvalidatedAsyncCallback);
			this._layoutUpdatedCallback = new EventHandler(this.OnLayoutUpdated);
			ContextLayoutManager.From(base.Dispatcher).LayoutEvents.Add(this._layoutUpdatedCallback);
			this._inputTimer = new DispatcherTimer(DispatcherPriority.Background);
			this._inputTimer.Tick += this.ValidateInputDevices;
			this._inputTimer.Interval = TimeSpan.FromMilliseconds(125.0);
		}

		/// <summary>Ocorre quando o <see cref="T:System.Windows.Input.InputManager" /> começa a processar o item de entrada.</summary>
		// Token: 0x14000153 RID: 339
		// (add) Token: 0x06001069 RID: 4201 RVA: 0x0003DB30 File Offset: 0x0003CF30
		// (remove) Token: 0x0600106A RID: 4202 RVA: 0x0003DB44 File Offset: 0x0003CF44
		public event PreProcessInputEventHandler PreProcessInput
		{
			[SecurityCritical]
			[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			add
			{
				this._preProcessInput += value;
			}
			[SecurityCritical]
			[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			remove
			{
				this._preProcessInput -= value;
			}
		}

		/// <summary>Ocorre quando os manipuladores <see cref="E:System.Windows.Input.InputManager.PreProcessInput" /> tiverem terminado de processar a entrada, se a entrada não tiver sido cancelada.</summary>
		// Token: 0x14000154 RID: 340
		// (add) Token: 0x0600106B RID: 4203 RVA: 0x0003DB58 File Offset: 0x0003CF58
		// (remove) Token: 0x0600106C RID: 4204 RVA: 0x0003DB6C File Offset: 0x0003CF6C
		public event NotifyInputEventHandler PreNotifyInput
		{
			[SecurityCritical]
			[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			add
			{
				this._preNotifyInput += value;
			}
			[SecurityCritical]
			[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			remove
			{
				this._preNotifyInput -= value;
			}
		}

		/// <summary>Ocorre após os manipuladores <see cref="E:System.Windows.Input.InputManager.PreNotifyInput" /> terem terminado de processar a entrada e eventos Windows Presentation Foundation (WPF) correspondente terem sido gerados.</summary>
		// Token: 0x14000155 RID: 341
		// (add) Token: 0x0600106D RID: 4205 RVA: 0x0003DB80 File Offset: 0x0003CF80
		// (remove) Token: 0x0600106E RID: 4206 RVA: 0x0003DB94 File Offset: 0x0003CF94
		public event NotifyInputEventHandler PostNotifyInput
		{
			[SecurityCritical]
			[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			add
			{
				this._postNotifyInput += value;
			}
			[SecurityCritical]
			[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			remove
			{
				this._postNotifyInput -= value;
			}
		}

		/// <summary>Ocorre após os manipuladores <see cref="E:System.Windows.Input.InputManager.PreNotifyInput" /> terem terminado de processar a entrada.</summary>
		// Token: 0x14000156 RID: 342
		// (add) Token: 0x0600106F RID: 4207 RVA: 0x0003DBA8 File Offset: 0x0003CFA8
		// (remove) Token: 0x06001070 RID: 4208 RVA: 0x0003DBBC File Offset: 0x0003CFBC
		public event ProcessInputEventHandler PostProcessInput
		{
			[SecurityCritical]
			[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			add
			{
				this._postProcessInput += value;
			}
			[SecurityCritical]
			[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			remove
			{
				this._postProcessInput -= value;
			}
		}

		// Token: 0x14000157 RID: 343
		// (add) Token: 0x06001071 RID: 4209 RVA: 0x0003DBD0 File Offset: 0x0003CFD0
		// (remove) Token: 0x06001072 RID: 4210 RVA: 0x0003DBE4 File Offset: 0x0003CFE4
		internal event KeyEventHandler TranslateAccelerator
		{
			[FriendAccessAllowed]
			[SecurityCritical]
			add
			{
				this._translateAccelerator += value;
			}
			[FriendAccessAllowed]
			[SecurityCritical]
			remove
			{
				this._translateAccelerator -= value;
			}
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x0003DBF8 File Offset: 0x0003CFF8
		[SecurityCritical]
		internal void RaiseTranslateAccelerator(KeyEventArgs e)
		{
			if (this._translateAccelerator != null)
			{
				this._translateAccelerator(this, e);
			}
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0003DC1C File Offset: 0x0003D01C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal InputProviderSite RegisterInputProvider(IInputProvider inputProvider)
		{
			SecurityHelper.DemandUnrestrictedUIPermission();
			InputProviderSite inputProviderSite = new InputProviderSite(this, inputProvider);
			this._inputProviders[inputProvider] = inputProviderSite;
			return inputProviderSite;
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0003DC44 File Offset: 0x0003D044
		[SecurityCritical]
		internal void UnregisterInputProvider(IInputProvider inputProvider)
		{
			this._inputProviders.Remove(inputProvider);
		}

		/// <summary>Obtém uma coleção de <see cref="P:System.Windows.Input.InputManager.InputProviders" /> registrado com o <see cref="T:System.Windows.Input.InputManager" />.</summary>
		/// <returns>Fornece a coleção de entrada.</returns>
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x0003DC60 File Offset: 0x0003D060
		public ICollection InputProviders
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandUnrestrictedUIPermission();
				return this.UnsecureInputProviders;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x0003DC78 File Offset: 0x0003D078
		internal ICollection UnsecureInputProviders
		{
			[SecurityCritical]
			get
			{
				return this._inputProviders.Keys;
			}
		}

		/// <summary>Obtém o dispositivo primário de teclado.</summary>
		/// <returns>O dispositivo de teclado.</returns>
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x0003DC90 File Offset: 0x0003D090
		public KeyboardDevice PrimaryKeyboardDevice
		{
			get
			{
				return this._primaryKeyboardDevice;
			}
		}

		/// <summary>Obtém o dispositivo primário de mouse.</summary>
		/// <returns>O dispositivo de mouse.</returns>
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x0003DCA4 File Offset: 0x0003D0A4
		public MouseDevice PrimaryMouseDevice
		{
			get
			{
				return this._primaryMouseDevice;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x0003DCB8 File Offset: 0x0003D0B8
		internal StylusLogic StylusLogic
		{
			[FriendAccessAllowed]
			[SecurityCritical]
			get
			{
				return StylusLogic.CurrentStylusLogic;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x0003DCCC File Offset: 0x0003D0CC
		internal CommandDevice PrimaryCommandDevice
		{
			get
			{
				return this._primaryCommandDevice;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x0003DCE0 File Offset: 0x0003D0E0
		// (set) Token: 0x0600107D RID: 4221 RVA: 0x0003DCF4 File Offset: 0x0003D0F4
		internal bool InDragDrop
		{
			get
			{
				return this._inDragDrop;
			}
			set
			{
				this._inDragDrop = value;
			}
		}

		/// <summary>Obtém um valor que representa o dispositivo de entrada associado ao evento de entrada mais recente.</summary>
		/// <returns>O dispositivo de entrada.</returns>
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x0003DD08 File Offset: 0x0003D108
		// (set) Token: 0x0600107F RID: 4223 RVA: 0x0003DD1C File Offset: 0x0003D11C
		public InputDevice MostRecentInputDevice
		{
			get
			{
				return this._mostRecentInputDevice;
			}
			internal set
			{
				this._mostRecentInputDevice = value;
			}
		}

		/// <summary>Chamado por componentes para entrar no modo de menu.</summary>
		/// <param name="menuSite">O menu a inserir.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="menuSite" /> é <see langword="null" />.</exception>
		// Token: 0x06001080 RID: 4224 RVA: 0x0003DD30 File Offset: 0x0003D130
		public void PushMenuMode(PresentationSource menuSite)
		{
			if (menuSite == null)
			{
				throw new ArgumentNullException("menuSite");
			}
			menuSite.VerifyAccess();
			menuSite.PushMenuMode();
			this._menuModeCount++;
			if (1 == this._menuModeCount)
			{
				EventHandler enterMenuMode = this.EnterMenuMode;
				if (enterMenuMode != null)
				{
					enterMenuMode(null, EventArgs.Empty);
				}
			}
		}

		/// <summary>Chamado por componentes para sair do modo de menu.</summary>
		/// <param name="menuSite">O menu para sair.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="menuSite" /> é <see langword="null" />.</exception>
		// Token: 0x06001081 RID: 4225 RVA: 0x0003DD84 File Offset: 0x0003D184
		public void PopMenuMode(PresentationSource menuSite)
		{
			if (menuSite == null)
			{
				throw new ArgumentNullException("menuSite");
			}
			menuSite.VerifyAccess();
			if (this._menuModeCount <= 0)
			{
				throw new InvalidOperationException();
			}
			menuSite.PopMenuMode();
			this._menuModeCount--;
			if (this._menuModeCount == 0)
			{
				EventHandler leaveMenuMode = this.LeaveMenuMode;
				if (leaveMenuMode != null)
				{
					leaveMenuMode(null, EventArgs.Empty);
				}
			}
		}

		/// <summary>Obtém um valor que indica se este <see cref="T:System.Windows.Interop.ComponentDispatcher" /> está no modo de menu.</summary>
		/// <returns>
		///   <see langword="true" /> Se este <see cref="T:System.Windows.Interop.ComponentDispatcher" /> estiver no modo de menu; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x0003DDE8 File Offset: 0x0003D1E8
		public bool IsInMenuMode
		{
			get
			{
				return this._menuModeCount > 0;
			}
		}

		/// <summary>Ocorre quando um controle entra no modo de menu chamando o método <see cref="M:System.Windows.Input.InputManager.PushMenuMode(System.Windows.PresentationSource)" />.</summary>
		// Token: 0x14000158 RID: 344
		// (add) Token: 0x06001083 RID: 4227 RVA: 0x0003DE00 File Offset: 0x0003D200
		// (remove) Token: 0x06001084 RID: 4228 RVA: 0x0003DE38 File Offset: 0x0003D238
		public event EventHandler EnterMenuMode;

		/// <summary>Ocorre quando um controle sai do modo de menu chamando o método <see cref="M:System.Windows.Input.InputManager.PopMenuMode(System.Windows.PresentationSource)" />.</summary>
		// Token: 0x14000159 RID: 345
		// (add) Token: 0x06001085 RID: 4229 RVA: 0x0003DE70 File Offset: 0x0003D270
		// (remove) Token: 0x06001086 RID: 4230 RVA: 0x0003DEA8 File Offset: 0x0003D2A8
		public event EventHandler LeaveMenuMode;

		/// <summary>Ocorre quando o resultado de um teste de clique pode ter mudado.</summary>
		// Token: 0x1400015A RID: 346
		// (add) Token: 0x06001087 RID: 4231 RVA: 0x0003DEE0 File Offset: 0x0003D2E0
		// (remove) Token: 0x06001088 RID: 4232 RVA: 0x0003DF18 File Offset: 0x0003D318
		public event EventHandler HitTestInvalidatedAsync;

		// Token: 0x06001089 RID: 4233 RVA: 0x0003DF50 File Offset: 0x0003D350
		internal void NotifyHitTestInvalidated()
		{
			if (this._hitTestInvalidatedAsyncOperation == null)
			{
				this._hitTestInvalidatedAsyncOperation = base.Dispatcher.BeginInvoke(DispatcherPriority.Input, this._hitTestInvalidatedAsyncCallback, null);
				return;
			}
			if (this._hitTestInvalidatedAsyncOperation.Priority == DispatcherPriority.Inactive)
			{
				this.ValidateInputDevices(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0003DF98 File Offset: 0x0003D398
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static void SafeCurrentNotifyHitTestInvalidated()
		{
			InputManager.UnsecureCurrent.NotifyHitTestInvalidated();
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0003DFB0 File Offset: 0x0003D3B0
		private object HitTestInvalidatedAsyncCallback(object arg)
		{
			this._hitTestInvalidatedAsyncOperation = null;
			if (this.HitTestInvalidatedAsync != null)
			{
				this.HitTestInvalidatedAsync(this, EventArgs.Empty);
			}
			return null;
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x0003DFE0 File Offset: 0x0003D3E0
		private void OnLayoutUpdated(object sender, EventArgs e)
		{
			this.NotifyHitTestInvalidated();
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0003DFF4 File Offset: 0x0003D3F4
		internal void InvalidateInputDevices()
		{
			if (this._hitTestInvalidatedAsyncOperation == null)
			{
				this._hitTestInvalidatedAsyncOperation = base.Dispatcher.BeginInvoke(DispatcherPriority.Inactive, this._hitTestInvalidatedAsyncCallback, null);
				this._inputTimer.IsEnabled = true;
			}
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0003E030 File Offset: 0x0003D430
		private void ValidateInputDevices(object sender, EventArgs e)
		{
			if (this._hitTestInvalidatedAsyncOperation != null)
			{
				this._hitTestInvalidatedAsyncOperation.Priority = DispatcherPriority.Input;
			}
			this._inputTimer.IsEnabled = false;
		}

		/// <summary>Processa a entrada especificada de forma síncrona.</summary>
		/// <param name="input">A entrada a ser processada.</param>
		/// <returns>
		///   <see langword="true" /> se todos os eventos de entrada foram tratados; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> é <see langword="null" />.</exception>
		// Token: 0x0600108F RID: 4239 RVA: 0x0003E060 File Offset: 0x0003D460
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public bool ProcessInput(InputEventArgs input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this.PushMarker();
			this.PushInput(input, null);
			this.RequestContinueProcessingStagingArea();
			return this.ProcessStagingArea();
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0003E09C File Offset: 0x0003D49C
		[SecurityCritical]
		internal StagingAreaInputItem PushInput(StagingAreaInputItem inputItem)
		{
			this._stagingArea.Push(inputItem);
			return inputItem;
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0003E0B8 File Offset: 0x0003D4B8
		[SecurityCritical]
		internal StagingAreaInputItem PushInput(InputEventArgs input, StagingAreaInputItem promote)
		{
			StagingAreaInputItem stagingAreaInputItem = new StagingAreaInputItem(false);
			stagingAreaInputItem.Reset(input, promote);
			return this.PushInput(stagingAreaInputItem);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0003E0DC File Offset: 0x0003D4DC
		[SecurityCritical]
		internal StagingAreaInputItem PushMarker()
		{
			StagingAreaInputItem inputItem = new StagingAreaInputItem(true);
			return this.PushInput(inputItem);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0003E0F8 File Offset: 0x0003D4F8
		[SecurityCritical]
		internal StagingAreaInputItem PopInput()
		{
			object obj = null;
			if (this._stagingArea.Count > 0)
			{
				obj = this._stagingArea.Pop();
			}
			return obj as StagingAreaInputItem;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0003E128 File Offset: 0x0003D528
		[SecurityCritical]
		internal StagingAreaInputItem PeekInput()
		{
			object obj = null;
			if (this._stagingArea.Count > 0)
			{
				obj = this._stagingArea.Peek();
			}
			return obj as StagingAreaInputItem;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0003E158 File Offset: 0x0003D558
		[SecurityCritical]
		internal object ContinueProcessingStagingArea(object unused)
		{
			this._continueProcessingStagingArea = false;
			if (this._stagingArea.Count > 0)
			{
				this.RequestContinueProcessingStagingArea();
				this.ProcessStagingArea();
			}
			return null;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0003E188 File Offset: 0x0003D588
		internal static bool StartListeningSynchronizedInput(DependencyObject d, SynchronizedInputType inputType)
		{
			object synchronizedInputLock = InputManager._synchronizedInputLock;
			bool result;
			lock (synchronizedInputLock)
			{
				if (InputManager._isSynchronizedInput)
				{
					result = false;
				}
				else
				{
					InputManager._isSynchronizedInput = true;
					InputManager._synchronizedInputState = SynchronizedInputStates.NoOpportunity;
					InputManager._listeningElement = d;
					InputManager._synchronizedInputType = inputType;
					InputManager._synchronizedInputEvents = SynchronizedInputHelper.MapInputTypeToRoutedEvents(inputType);
					InputManager._pairedSynchronizedInputEvents = SynchronizedInputHelper.MapInputTypeToRoutedEvents(SynchronizedInputHelper.GetPairedInputType(inputType));
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0003E210 File Offset: 0x0003D610
		internal static void CancelSynchronizedInput()
		{
			object synchronizedInputLock = InputManager._synchronizedInputLock;
			lock (synchronizedInputLock)
			{
				InputManager._isSynchronizedInput = false;
				InputManager._synchronizedInputState = SynchronizedInputStates.NoOpportunity;
				InputManager._listeningElement = null;
				InputManager._synchronizedInputEvents = null;
				InputManager._pairedSynchronizedInputEvents = null;
				if (InputManager._synchronizedInputAsyncClearOperation != null)
				{
					InputManager._synchronizedInputAsyncClearOperation.Abort();
					InputManager._synchronizedInputAsyncClearOperation = null;
				}
			}
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0003E28C File Offset: 0x0003D68C
		[SecurityCritical]
		private bool ProcessStagingArea()
		{
			bool result = false;
			NotifyInputEventArgs notifyInputEventArgs = (this._notifyInputEventArgs != null) ? this._notifyInputEventArgs : new NotifyInputEventArgs();
			ProcessInputEventArgs processInputEventArgs = (this._processInputEventArgs != null) ? this._processInputEventArgs : new ProcessInputEventArgs();
			PreProcessInputEventArgs preProcessInputEventArgs = (this._preProcessInputEventArgs != null) ? this._preProcessInputEventArgs : new PreProcessInputEventArgs();
			this._notifyInputEventArgs = null;
			this._processInputEventArgs = null;
			this._preProcessInputEventArgs = null;
			StagingAreaInputItem stagingAreaInputItem;
			while ((stagingAreaInputItem = this.PopInput()) != null && !stagingAreaInputItem.IsMarker)
			{
				if (this._preProcessInput != null)
				{
					preProcessInputEventArgs.Reset(stagingAreaInputItem, this);
					Delegate[] invocationList = this._preProcessInput.GetInvocationList();
					for (int i = invocationList.Length - 1; i >= 0; i--)
					{
						PreProcessInputEventHandler preProcessInputEventHandler = (PreProcessInputEventHandler)invocationList[i];
						preProcessInputEventHandler(this, preProcessInputEventArgs);
					}
				}
				if (!preProcessInputEventArgs.Canceled)
				{
					if (this._preNotifyInput != null)
					{
						notifyInputEventArgs.Reset(stagingAreaInputItem, this);
						Delegate[] invocationList2 = this._preNotifyInput.GetInvocationList();
						for (int j = invocationList2.Length - 1; j >= 0; j--)
						{
							NotifyInputEventHandler notifyInputEventHandler = (NotifyInputEventHandler)invocationList2[j];
							notifyInputEventHandler(this, notifyInputEventArgs);
						}
					}
					InputEventArgs input = stagingAreaInputItem.Input;
					DependencyObject dependencyObject = input.Source as DependencyObject;
					if ((dependencyObject == null || !InputElement.IsValid(dependencyObject as IInputElement)) && input.Device != null)
					{
						dependencyObject = (input.Device.Target as DependencyObject);
					}
					if (InputManager._isSynchronizedInput && SynchronizedInputHelper.IsMappedEvent(input) && Array.IndexOf<RoutedEvent>(InputManager.SynchronizedInputEvents, input.RoutedEvent) < 0 && Array.IndexOf<RoutedEvent>(InputManager.PairedSynchronizedInputEvents, input.RoutedEvent) < 0)
					{
						if (!SynchronizedInputHelper.ShouldContinueListening(input))
						{
							InputManager._synchronizedInputState = SynchronizedInputStates.Discarded;
							SynchronizedInputHelper.RaiseAutomationEvents();
							InputManager.CancelSynchronizedInput();
						}
						else
						{
							InputManager._synchronizedInputAsyncClearOperation = base.Dispatcher.BeginInvoke(new Action(delegate()
							{
								InputManager._synchronizedInputState = SynchronizedInputStates.Discarded;
								SynchronizedInputHelper.RaiseAutomationEvents();
								InputManager.CancelSynchronizedInput();
							}), DispatcherPriority.Background, new object[0]);
						}
					}
					else if (dependencyObject != null)
					{
						if (InputElement.IsUIElement(dependencyObject))
						{
							UIElement uielement = (UIElement)dependencyObject;
							uielement.RaiseEvent(input, true);
						}
						else if (InputElement.IsContentElement(dependencyObject))
						{
							ContentElement contentElement = (ContentElement)dependencyObject;
							contentElement.RaiseEvent(input, true);
						}
						else if (InputElement.IsUIElement3D(dependencyObject))
						{
							UIElement3D uielement3D = (UIElement3D)dependencyObject;
							uielement3D.RaiseEvent(input, true);
						}
						if (InputManager._isSynchronizedInput && SynchronizedInputHelper.IsListening(InputManager._listeningElement, input))
						{
							if (!SynchronizedInputHelper.ShouldContinueListening(input))
							{
								SynchronizedInputHelper.RaiseAutomationEvents();
								InputManager.CancelSynchronizedInput();
							}
							else
							{
								InputManager._synchronizedInputAsyncClearOperation = base.Dispatcher.BeginInvoke(new Action(delegate()
								{
									SynchronizedInputHelper.RaiseAutomationEvents();
									InputManager.CancelSynchronizedInput();
								}), DispatcherPriority.Background, new object[0]);
							}
						}
					}
					if (this._postNotifyInput != null)
					{
						notifyInputEventArgs.Reset(stagingAreaInputItem, this);
						Delegate[] invocationList3 = this._postNotifyInput.GetInvocationList();
						for (int k = invocationList3.Length - 1; k >= 0; k--)
						{
							NotifyInputEventHandler notifyInputEventHandler2 = (NotifyInputEventHandler)invocationList3[k];
							notifyInputEventHandler2(this, notifyInputEventArgs);
						}
					}
					if (this._postProcessInput != null)
					{
						processInputEventArgs.Reset(stagingAreaInputItem, this);
						this.RaiseProcessInputEventHandlers(this._postProcessInput, processInputEventArgs);
						if (stagingAreaInputItem.Input.RoutedEvent == InputManager.PreviewInputReportEvent && !stagingAreaInputItem.Input.Handled)
						{
							InputReportEventArgs inputReportEventArgs = (InputReportEventArgs)stagingAreaInputItem.Input;
							this.PushInput(new InputReportEventArgs(inputReportEventArgs.Device, inputReportEventArgs.Report)
							{
								RoutedEvent = InputManager.InputReportEvent
							}, stagingAreaInputItem);
						}
					}
					if (input.Handled)
					{
						result = true;
					}
				}
			}
			this._notifyInputEventArgs = notifyInputEventArgs;
			this._processInputEventArgs = processInputEventArgs;
			this._preProcessInputEventArgs = preProcessInputEventArgs;
			this._notifyInputEventArgs.Reset(null, null);
			this._processInputEventArgs.Reset(null, null);
			this._preProcessInputEventArgs.Reset(null, null);
			return result;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0003E63C File Offset: 0x0003DA3C
		[SecurityCritical]
		[UserInitiatedRoutedEventPermission(SecurityAction.Assert)]
		private void RaiseProcessInputEventHandlers(ProcessInputEventHandler postProcessInput, ProcessInputEventArgs processInputEventArgs)
		{
			processInputEventArgs.StagingItem.Input.MarkAsUserInitiated();
			try
			{
				Delegate[] invocationList = postProcessInput.GetInvocationList();
				for (int i = invocationList.Length - 1; i >= 0; i--)
				{
					ProcessInputEventHandler processInputEventHandler = (ProcessInputEventHandler)invocationList[i];
					processInputEventHandler(this, processInputEventArgs);
				}
			}
			finally
			{
				processInputEventArgs.StagingItem.Input.ClearUserInitiated();
			}
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0003E6B0 File Offset: 0x0003DAB0
		private void RequestContinueProcessingStagingArea()
		{
			if (!this._continueProcessingStagingArea)
			{
				base.Dispatcher.BeginInvoke(DispatcherPriority.Input, this._continueProcessingStagingAreaCallback, null);
				this._continueProcessingStagingArea = true;
			}
		}

		// Token: 0x1400015B RID: 347
		// (add) Token: 0x0600109B RID: 4251 RVA: 0x0003E6E0 File Offset: 0x0003DAE0
		// (remove) Token: 0x0600109C RID: 4252 RVA: 0x0003E718 File Offset: 0x0003DB18
		[method: SecurityCritical]
		private event PreProcessInputEventHandler _preProcessInput;

		// Token: 0x1400015C RID: 348
		// (add) Token: 0x0600109D RID: 4253 RVA: 0x0003E750 File Offset: 0x0003DB50
		// (remove) Token: 0x0600109E RID: 4254 RVA: 0x0003E788 File Offset: 0x0003DB88
		[method: SecurityCritical]
		private event NotifyInputEventHandler _preNotifyInput;

		// Token: 0x1400015D RID: 349
		// (add) Token: 0x0600109F RID: 4255 RVA: 0x0003E7C0 File Offset: 0x0003DBC0
		// (remove) Token: 0x060010A0 RID: 4256 RVA: 0x0003E7F8 File Offset: 0x0003DBF8
		[method: SecurityCritical]
		private event NotifyInputEventHandler _postNotifyInput;

		// Token: 0x1400015E RID: 350
		// (add) Token: 0x060010A1 RID: 4257 RVA: 0x0003E830 File Offset: 0x0003DC30
		// (remove) Token: 0x060010A2 RID: 4258 RVA: 0x0003E868 File Offset: 0x0003DC68
		[method: SecurityCritical]
		private event ProcessInputEventHandler _postProcessInput;

		// Token: 0x1400015F RID: 351
		// (add) Token: 0x060010A3 RID: 4259 RVA: 0x0003E8A0 File Offset: 0x0003DCA0
		// (remove) Token: 0x060010A4 RID: 4260 RVA: 0x0003E8D8 File Offset: 0x0003DCD8
		[method: SecurityCritical]
		private event KeyEventHandler _translateAccelerator;

		// Token: 0x040008C9 RID: 2249
		internal static readonly RoutedEvent PreviewInputReportEvent = GlobalEventManager.RegisterRoutedEvent("PreviewInputReport", RoutingStrategy.Tunnel, typeof(InputReportEventHandler), typeof(InputManager));

		// Token: 0x040008CA RID: 2250
		[FriendAccessAllowed]
		internal static readonly RoutedEvent InputReportEvent = GlobalEventManager.RegisterRoutedEvent("InputReport", RoutingStrategy.Bubble, typeof(InputReportEventHandler), typeof(InputManager));

		// Token: 0x040008CD RID: 2253
		private int _menuModeCount;

		// Token: 0x040008CF RID: 2255
		private DispatcherOperationCallback _continueProcessingStagingAreaCallback;

		// Token: 0x040008D0 RID: 2256
		private bool _continueProcessingStagingArea;

		// Token: 0x040008D1 RID: 2257
		private NotifyInputEventArgs _notifyInputEventArgs;

		// Token: 0x040008D2 RID: 2258
		private ProcessInputEventArgs _processInputEventArgs;

		// Token: 0x040008D3 RID: 2259
		private PreProcessInputEventArgs _preProcessInputEventArgs;

		// Token: 0x040008D9 RID: 2265
		[SecurityCritical]
		private Hashtable _inputProviders = new Hashtable();

		// Token: 0x040008DA RID: 2266
		private KeyboardDevice _primaryKeyboardDevice;

		// Token: 0x040008DB RID: 2267
		private MouseDevice _primaryMouseDevice;

		// Token: 0x040008DC RID: 2268
		private CommandDevice _primaryCommandDevice;

		// Token: 0x040008DD RID: 2269
		private bool _inDragDrop;

		// Token: 0x040008DE RID: 2270
		private DispatcherOperationCallback _hitTestInvalidatedAsyncCallback;

		// Token: 0x040008DF RID: 2271
		private DispatcherOperation _hitTestInvalidatedAsyncOperation;

		// Token: 0x040008E0 RID: 2272
		private EventHandler _layoutUpdatedCallback;

		// Token: 0x040008E1 RID: 2273
		[SecurityCritical]
		private Stack _stagingArea;

		// Token: 0x040008E2 RID: 2274
		private InputDevice _mostRecentInputDevice;

		// Token: 0x040008E3 RID: 2275
		private DispatcherTimer _inputTimer;

		// Token: 0x040008E4 RID: 2276
		private static bool _isSynchronizedInput;

		// Token: 0x040008E5 RID: 2277
		private static DependencyObject _listeningElement;

		// Token: 0x040008E6 RID: 2278
		private static RoutedEvent[] _synchronizedInputEvents;

		// Token: 0x040008E7 RID: 2279
		private static RoutedEvent[] _pairedSynchronizedInputEvents;

		// Token: 0x040008E8 RID: 2280
		private static SynchronizedInputType _synchronizedInputType;

		// Token: 0x040008E9 RID: 2281
		private static SynchronizedInputStates _synchronizedInputState = SynchronizedInputStates.NoOpportunity;

		// Token: 0x040008EA RID: 2282
		private static DispatcherOperation _synchronizedInputAsyncClearOperation;

		// Token: 0x040008EB RID: 2283
		private static object _synchronizedInputLock = new object();
	}
}
