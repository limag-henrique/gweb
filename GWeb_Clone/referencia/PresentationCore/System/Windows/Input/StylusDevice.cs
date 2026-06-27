using System;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using System.Windows.Media;

namespace System.Windows.Input
{
	/// <summary>Representa uma caneta eletrônica usada com um Tablet PC.</summary>
	// Token: 0x020002B1 RID: 689
	public sealed class StylusDevice : InputDevice
	{
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0004B470 File Offset: 0x0004A870
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x0004B484 File Offset: 0x0004A884
		internal StylusDeviceBase StylusDeviceImpl { get; set; }

		// Token: 0x06001446 RID: 5190 RVA: 0x0004B498 File Offset: 0x0004A898
		internal StylusDevice(StylusDeviceBase impl)
		{
			if (impl == null)
			{
				throw new ArgumentNullException("impl");
			}
			this.StylusDeviceImpl = impl;
		}

		/// <summary>Obtém o elemento que recebe entrada.</summary>
		/// <returns>O <see cref="T:System.Windows.IInputElement" /> objeto que recebe entrada.</returns>
		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x0004B4C0 File Offset: 0x0004A8C0
		public override IInputElement Target
		{
			get
			{
				base.VerifyAccess();
				return this.StylusDeviceImpl.Target;
			}
		}

		/// <summary>Obtém um valor que indica se um dispositivo de caneta é válido.</summary>
		/// <returns>
		///   <see langword="true" /> para indicar uma caneta é válido. Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x0004B4E0 File Offset: 0x0004A8E0
		public bool IsValid
		{
			get
			{
				return this.StylusDeviceImpl.IsValid;
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.PresentationSource" /> que relata a entrada atual para a caneta.</summary>
		/// <returns>O <see cref="T:System.Windows.PresentationSource" /> que relata a entrada atual para a caneta.</returns>
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x0004B4F8 File Offset: 0x0004A8F8
		public override PresentationSource ActiveSource
		{
			[SecurityCritical]
			[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
			get
			{
				return this.StylusDeviceImpl.ActiveSource;
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.IInputElement" /> sobre o qual o ponteiro está posicionado.</summary>
		/// <returns>O elemento que o ponteiro está sobre.</returns>
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x0004B510 File Offset: 0x0004A910
		public IInputElement DirectlyOver
		{
			get
			{
				base.VerifyAccess();
				return this.StylusDeviceImpl.DirectlyOver;
			}
		}

		/// <summary>Obtém o elemento que capturou a caneta.</summary>
		/// <returns>O <see cref="T:System.Windows.IInputElement" /> que capturou a caneta.</returns>
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x0004B530 File Offset: 0x0004A930
		public IInputElement Captured
		{
			get
			{
				base.VerifyAccess();
				return this.StylusDeviceImpl.Captured;
			}
		}

		/// <summary>Associa a caneta para o elemento especificado.</summary>
		/// <param name="element">O elemento ao qual a caneta está associada.</param>
		/// <param name="captureMode">Um dos valores de <see cref="T:System.Windows.Input.CaptureMode" />.</param>
		/// <returns>
		///   <see langword="true" /> se o elemento de entrada for capturado com êxito; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> ou <paramref name="captureMode" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="element" /> não é <see cref="T:System.Windows.UIElement" /> nem <see cref="T:System.Windows.FrameworkContentElement" />.</exception>
		// Token: 0x0600144C RID: 5196 RVA: 0x0004B550 File Offset: 0x0004A950
		public bool Capture(IInputElement element, CaptureMode captureMode)
		{
			return this.StylusDeviceImpl.Capture(element, captureMode);
		}

		/// <summary>Associa a entrada da caneta para o elemento especificado.</summary>
		/// <param name="element">O elemento ao qual a caneta está associada.</param>
		/// <returns>
		///   <see langword="true" /> se o elemento de entrada for capturado com êxito; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="element" /> não é <see cref="T:System.Windows.UIElement" /> nem <see cref="T:System.Windows.FrameworkContentElement" />.</exception>
		// Token: 0x0600144D RID: 5197 RVA: 0x0004B56C File Offset: 0x0004A96C
		public bool Capture(IInputElement element)
		{
			return this.Capture(element, CaptureMode.Element);
		}

		/// <summary>Sincroniza o cursor e a interface do usuário.</summary>
		// Token: 0x0600144E RID: 5198 RVA: 0x0004B584 File Offset: 0x0004A984
		[SecurityCritical]
		public void Synchronize()
		{
			this.StylusDeviceImpl.Synchronize();
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.TabletDevice" /> que representa o digitalizador associado ao <see cref="T:System.Windows.Input.StylusDevice" /> atual.</summary>
		/// <returns>O <see cref="T:System.Windows.Input.TabletDevice" /> representa o digitalizador associado atual <see cref="T:System.Windows.Input.StylusDevice" />.</returns>
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x0004B59C File Offset: 0x0004A99C
		public TabletDevice TabletDevice
		{
			get
			{
				return this.StylusDeviceImpl.TabletDevice;
			}
		}

		/// <summary>Obtém o nome da caneta.</summary>
		/// <returns>O nome da caneta.</returns>
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x0004B5B4 File Offset: 0x0004A9B4
		public string Name
		{
			get
			{
				base.VerifyAccess();
				return this.StylusDeviceImpl.Name;
			}
		}

		/// <summary>Retorna o nome do dispositivo de caneta.</summary>
		/// <returns>O nome do <see cref="T:System.Windows.Input.StylusDevice" />.</returns>
		// Token: 0x06001451 RID: 5201 RVA: 0x0004B5D4 File Offset: 0x0004A9D4
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}({1})", new object[]
			{
				base.ToString(),
				this.Name
			});
		}

		/// <summary>Obtém o identificador para o dispositivo de caneta.</summary>
		/// <returns>O identificador para o dispositivo de caneta.</returns>
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x0004B608 File Offset: 0x0004AA08
		public int Id
		{
			get
			{
				base.VerifyAccess();
				return this.StylusDeviceImpl.Id;
			}
		}

		/// <summary>Retorna um <see cref="T:System.Windows.Input.StylusPointCollection" /> que contém objetos <see cref="T:System.Windows.Input.StylusPoint" /> coletados da caneta.</summary>
		/// <param name="relativeTo">O <see cref="T:System.Windows.IInputElement" /> para o qual as coordenadas (x, y) em <see cref="T:System.Windows.Input.StylusPointCollection" /> são mapeadas.</param>
		/// <returns>Um <see cref="T:System.Windows.Input.StylusPointCollection" /> que contém objetos <see cref="T:System.Windows.Input.StylusPoint" /> que a caneta coletou.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="relativeTo" /> não é <see cref="T:System.Windows.UIElement" /> nem <see cref="T:System.Windows.FrameworkContentElement" />.</exception>
		// Token: 0x06001453 RID: 5203 RVA: 0x0004B628 File Offset: 0x0004AA28
		public StylusPointCollection GetStylusPoints(IInputElement relativeTo)
		{
			base.VerifyAccess();
			return this.StylusDeviceImpl.GetStylusPoints(relativeTo);
		}

		/// <summary>Retorna um <see cref="T:System.Windows.Input.StylusPointCollection" /> que contém objetos <see cref="T:System.Windows.Input.StylusPoint" /> coletados da caneta. Usa o <see cref="T:System.Windows.Input.StylusPointDescription" /> especificado.</summary>
		/// <param name="relativeTo">O <see cref="T:System.Windows.IInputElement" /> para o qual as coordenadas (x y) em <see cref="T:System.Windows.Input.StylusPointCollection" /> são mapeadas.</param>
		/// <param name="subsetToReformatTo">O <see cref="T:System.Windows.Input.StylusPointDescription" /> a ser usado pelo <see cref="T:System.Windows.Input.StylusPointCollection" />.</param>
		/// <returns>Um <see cref="T:System.Windows.Input.StylusPointCollection" /> que contém objetos <see cref="T:System.Windows.Input.StylusPoint" /> coletados da caneta.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="relativeTo" /> não é <see cref="T:System.Windows.UIElement" /> nem <see cref="T:System.Windows.FrameworkContentElement" />.</exception>
		// Token: 0x06001454 RID: 5204 RVA: 0x0004B648 File Offset: 0x0004AA48
		public StylusPointCollection GetStylusPoints(IInputElement relativeTo, StylusPointDescription subsetToReformatTo)
		{
			return this.StylusDeviceImpl.GetStylusPoints(relativeTo, subsetToReformatTo);
		}

		/// <summary>Obtém os botões de caneta na caneta.</summary>
		/// <returns>Uma referência a um <see cref="T:System.Windows.Input.StylusButtonCollection" /> objeto que representa todos os botões na caneta.</returns>
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x0004B664 File Offset: 0x0004AA64
		public StylusButtonCollection StylusButtons
		{
			get
			{
				base.VerifyAccess();
				return this.StylusDeviceImpl.StylusButtons;
			}
		}

		/// <summary>Obtém a posição da caneta.</summary>
		/// <param name="relativeTo">O <see cref="T:System.Windows.IInputElement" /> para o qual as coordenadas (x, y) são mapeadas.</param>
		/// <returns>Um <see cref="T:System.Windows.Point" /> que representa a posição da caneta em relação ao <paramref name="relativeTo" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="relativeTo" /> não é <see cref="T:System.Windows.UIElement" /> nem <see cref="T:System.Windows.FrameworkContentElement" />.</exception>
		// Token: 0x06001456 RID: 5206 RVA: 0x0004B684 File Offset: 0x0004AA84
		[SecurityCritical]
		public Point GetPosition(IInputElement relativeTo)
		{
			base.VerifyAccess();
			return this.StylusDeviceImpl.GetPosition(relativeTo);
		}

		/// <summary>Obtém se a caneta está posicionada acima, mas não em contato com o digitalizador.</summary>
		/// <returns>
		///   <see langword="true" /> Se a caneta está posicionada acima, mas não em contato com o digitalizador; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x0004B6A4 File Offset: 0x0004AAA4
		public bool InAir
		{
			get
			{
				base.VerifyAccess();
				return this.StylusDeviceImpl.InAir;
			}
		}

		/// <summary>Obtém um valor que indica se a dica secundária da caneta está em uso.</summary>
		/// <returns>
		///   <see langword="true" /> Se a dica secundária da caneta estiver em uso. Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0004B6C4 File Offset: 0x0004AAC4
		public bool Inverted
		{
			get
			{
				base.VerifyAccess();
				return this.StylusDeviceImpl.Inverted;
			}
		}

		/// <summary>Obtém um valor que indica se a caneta está no alcance do digitalizador.</summary>
		/// <returns>
		///   <see langword="true" /> Se a caneta está dentro do alcance do digitalizador; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x0004B6E4 File Offset: 0x0004AAE4
		public bool InRange
		{
			get
			{
				base.VerifyAccess();
				return this.StylusDeviceImpl.InRange;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x0004B704 File Offset: 0x0004AB04
		internal int DoubleTapDeltaX
		{
			get
			{
				return this.StylusDeviceImpl.DoubleTapDeltaX;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x0004B71C File Offset: 0x0004AB1C
		internal int DoubleTapDeltaY
		{
			get
			{
				return this.StylusDeviceImpl.DoubleTapDeltaY;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x0004B734 File Offset: 0x0004AB34
		internal int DoubleTapDeltaTime
		{
			[SecuritySafeCritical]
			get
			{
				return this.StylusDeviceImpl.DoubleTapDeltaTime;
			}
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0004B74C File Offset: 0x0004AB4C
		internal Point GetMouseScreenPosition(MouseDevice mouseDevice)
		{
			return this.StylusDeviceImpl.GetMouseScreenPosition(mouseDevice);
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0004B768 File Offset: 0x0004AB68
		[SecuritySafeCritical]
		internal MouseButtonState GetMouseButtonState(MouseButton mouseButton, MouseDevice mouseDevice)
		{
			return this.StylusDeviceImpl.GetMouseButtonState(mouseButton, mouseDevice);
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0004B784 File Offset: 0x0004AB84
		internal static IInputElement LocalHitTest(PresentationSource inputSource, Point pt)
		{
			return MouseDevice.LocalHitTest(pt, inputSource);
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0004B798 File Offset: 0x0004AB98
		internal static IInputElement GlobalHitTest(PresentationSource inputSource, Point pt)
		{
			return MouseDevice.GlobalHitTest(pt, inputSource);
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0004B7AC File Offset: 0x0004ABAC
		internal static GeneralTransform GetElementTransform(IInputElement relativeTo)
		{
			GeneralTransform result = Transform.Identity;
			DependencyObject dependencyObject = relativeTo as DependencyObject;
			if (dependencyObject != null)
			{
				Visual containingVisual2D = VisualTreeHelper.GetContainingVisual2D(InputElement.GetContainingVisual(dependencyObject));
				Visual containingVisual2D2 = VisualTreeHelper.GetContainingVisual2D(InputElement.GetRootVisual(dependencyObject));
				GeneralTransform generalTransform = containingVisual2D2.TransformToDescendant(containingVisual2D);
				if (generalTransform != null)
				{
					result = generalTransform;
				}
			}
			return result;
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x0004B7F4 File Offset: 0x0004ABF4
		internal T As<T>() where T : StylusDeviceBase
		{
			return this.StylusDeviceImpl as T;
		}
	}
}
