using System;
using System.Reflection;
using System.Windows.Input;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Interop
{
	/// <summary>Contém os parâmetros que são usados para criar um objeto <see cref="T:System.Windows.Interop.HwndSource" /> usando o construtor <see cref="M:System.Windows.Interop.HwndSource.#ctor(System.Windows.Interop.HwndSourceParameters)" />.</summary>
	// Token: 0x02000323 RID: 803
	public struct HwndSourceParameters
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Interop.HwndSourceParameters" /> com um nome de janela especificado.</summary>
		/// <param name="name">O nome da janela.</param>
		// Token: 0x06001AAE RID: 6830 RVA: 0x00068C70 File Offset: 0x00068070
		public HwndSourceParameters(string name)
		{
			this = default(HwndSourceParameters);
			this._styleBits = 268435456;
			this._styleBits |= 12582912;
			this._styleBits |= 524288;
			this._styleBits |= 262144;
			this._styleBits |= 131072;
			this._styleBits |= 65536;
			this._styleBits |= 33554432;
			this._width = 1;
			this._height = 1;
			this._x = int.MinValue;
			this._y = int.MinValue;
			this.WindowName = name;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Interop.HwndSourceParameters" /> com um nome de janela e um tamanho inicial especificados.</summary>
		/// <param name="name">O nome da janela.</param>
		/// <param name="width">A largura da janela, em pixels.</param>
		/// <param name="height">A altura da janela, em pixels.</param>
		// Token: 0x06001AAF RID: 6831 RVA: 0x00068D28 File Offset: 0x00068128
		public HwndSourceParameters(string name, int width, int height)
		{
			this = new HwndSourceParameters(name);
			this.Width = width;
			this.Height = height;
		}

		/// <summary>Retorna o código hash para essa instância <see cref="T:System.Windows.Interop.HwndSourceParameters" />.</summary>
		/// <returns>Um código de hash do inteiro assinado de 32 bits.</returns>
		// Token: 0x06001AB0 RID: 6832 RVA: 0x00068D4C File Offset: 0x0006814C
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Obtém ou define o estilo da classe Microsoft Windows para a janela.</summary>
		/// <returns>O estilo de classe de janela. Ver estilos de classe de janela para obter informações detalhadas. O padrão é 0 (nenhum estilo de classe de janela).</returns>
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x00068D6C File Offset: 0x0006816C
		// (set) Token: 0x06001AB2 RID: 6834 RVA: 0x00068D80 File Offset: 0x00068180
		public int WindowClassStyle
		{
			get
			{
				return this._classStyleBits;
			}
			set
			{
				this._classStyleBits = value;
			}
		}

		/// <summary>Obtém ou define o estilo da janela.</summary>
		/// <returns>O estilo da janela. Consulte a CreateWindowEx função para obter uma lista completa de bits de estilo. Padrões: WS_VISIBLE WS_CAPTION, WS_SYSMENU, WS_THICKFRAME, WS_MINIMIZEBOX, WS_MAXIMIZEBOX, WS_CLIPCHILDREN</returns>
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x00068D94 File Offset: 0x00068194
		// (set) Token: 0x06001AB4 RID: 6836 RVA: 0x00068DA8 File Offset: 0x000681A8
		public int WindowStyle
		{
			get
			{
				return this._styleBits;
			}
			set
			{
				this._styleBits = (value | 33554432);
			}
		}

		/// <summary>Obtém ou define os estilos Microsoft Windows estendidos para a janela.</summary>
		/// <returns>Os estilos de janela estendidos. Ver CreateWindowEx para obter uma lista desses estilos. O padrão é 0 (nenhum estilo de janela estendidos).</returns>
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x00068DC4 File Offset: 0x000681C4
		// (set) Token: 0x06001AB6 RID: 6838 RVA: 0x00068DD8 File Offset: 0x000681D8
		public int ExtendedWindowStyle
		{
			get
			{
				return this._extendedStyleBits;
			}
			set
			{
				this._extendedStyleBits = value;
			}
		}

		/// <summary>Define os valores usados para o posicionamento de tela da janela para o <see cref="T:System.Windows.Interop.HwndSource" />.</summary>
		/// <param name="x">A posição da borda esquerda da janela.</param>
		/// <param name="y">A posição da borda superior da janela.</param>
		// Token: 0x06001AB7 RID: 6839 RVA: 0x00068DEC File Offset: 0x000681EC
		public void SetPosition(int x, int y)
		{
			this._x = x;
			this._y = y;
		}

		/// <summary>Obtém ou define a posição da borda esquerda da janela.</summary>
		/// <returns>A posição da borda esquerda da janela. O padrão é CW_USEDEFAULT, como processado por CreateWindow.</returns>
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x00068E08 File Offset: 0x00068208
		// (set) Token: 0x06001AB9 RID: 6841 RVA: 0x00068E1C File Offset: 0x0006821C
		public int PositionX
		{
			get
			{
				return this._x;
			}
			set
			{
				this._x = value;
			}
		}

		/// <summary>Obtém ou define a posição da borda superior da janela.</summary>
		/// <returns>A posição da borda superior da janela. O padrão é CW_USEDEFAULT, como processado por CreateWindow.</returns>
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x00068E30 File Offset: 0x00068230
		// (set) Token: 0x06001ABB RID: 6843 RVA: 0x00068E44 File Offset: 0x00068244
		public int PositionY
		{
			get
			{
				return this._y;
			}
			set
			{
				this._y = value;
			}
		}

		/// <summary>Define os valores usados para o tamanho da janela para o <see cref="T:System.Windows.Interop.HwndSource" />.</summary>
		/// <param name="width">A largura da janela, em pixels do dispositivo.</param>
		/// <param name="height">A altura da janela, em pixels do dispositivo.</param>
		// Token: 0x06001ABC RID: 6844 RVA: 0x00068E58 File Offset: 0x00068258
		public void SetSize(int width, int height)
		{
			this._width = width;
			this._height = height;
			this._hasAssignedSize = true;
		}

		/// <summary>Obtém ou define um valor que indica a largura da janela.</summary>
		/// <returns>A largura da janela, em pixels do dispositivo. O valor padrão é 1.</returns>
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x00068E7C File Offset: 0x0006827C
		// (set) Token: 0x06001ABE RID: 6846 RVA: 0x00068E90 File Offset: 0x00068290
		public int Width
		{
			get
			{
				return this._width;
			}
			set
			{
				this._width = value;
				this._hasAssignedSize = true;
			}
		}

		/// <summary>Obtém ou define um valor que indica a altura da janela.</summary>
		/// <returns>A altura da janela, em pixels do dispositivo. O valor padrão é 1.</returns>
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x00068EAC File Offset: 0x000682AC
		// (set) Token: 0x06001AC0 RID: 6848 RVA: 0x00068EC0 File Offset: 0x000682C0
		public int Height
		{
			get
			{
				return this._height;
			}
			set
			{
				this._height = value;
				this._hasAssignedSize = true;
			}
		}

		/// <summary>Obtém um valor que indica se um tamanho foi atribuído.</summary>
		/// <returns>
		///   <see langword="true" /> Se o tamanho da janela foi atribuído; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />, a menos que a estrutura foi criada com fornecido de altura e largura, nesse caso, o valor é <see langword="true" />.</returns>
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x00068EDC File Offset: 0x000682DC
		public bool HasAssignedSize
		{
			get
			{
				return this._hasAssignedSize;
			}
		}

		/// <summary>Obtém ou define o nome da janela.</summary>
		/// <returns>O nome da janela.</returns>
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x00068EF0 File Offset: 0x000682F0
		// (set) Token: 0x06001AC3 RID: 6851 RVA: 0x00068F04 File Offset: 0x00068304
		public string WindowName
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		/// <summary>Obtém ou define o identificador de janela (HWND) pai para a janela de criada.</summary>
		/// <returns>O HWND da janela pai.</returns>
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x00068F18 File Offset: 0x00068318
		// (set) Token: 0x06001AC5 RID: 6853 RVA: 0x00068F2C File Offset: 0x0006832C
		public IntPtr ParentWindow
		{
			get
			{
				return this._parent;
			}
			set
			{
				this._parent = value;
			}
		}

		/// <summary>Obtém ou define o gancho da mensagem para a janela.</summary>
		/// <returns>O gancho da mensagem para a janela.</returns>
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x00068F40 File Offset: 0x00068340
		// (set) Token: 0x06001AC7 RID: 6855 RVA: 0x00068F54 File Offset: 0x00068354
		public HwndSourceHook HwndSourceHook
		{
			get
			{
				return this._hwndSourceHook;
			}
			set
			{
				this._hwndSourceHook = value;
			}
		}

		/// <summary>Obtém ou define um valor que indica se a área não cliente deve ser incluída dimensionamento.</summary>
		/// <returns>
		///   <see langword="true" /> Se o Gerenciador de layout a lógica de dimensionamento deve incluir a área não cliente; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x00068F68 File Offset: 0x00068368
		// (set) Token: 0x06001AC9 RID: 6857 RVA: 0x00068F7C File Offset: 0x0006837C
		public bool AdjustSizingForNonClientArea
		{
			get
			{
				return this._adjustSizingForNonClientArea;
			}
			set
			{
				this._adjustSizingForNonClientArea = value;
			}
		}

		/// <summary>Obtém ou define um valor que indica se as janelas pai do <see cref="T:System.Windows.Interop.HwndSource" /> devem ser consideradas a área não de cliente da janela durante as passagens de layout.</summary>
		/// <returns>
		///   <see langword="true" /> Se pai windows do <see cref="T:System.Windows.Interop.HwndSource" /> deve ser considerada a área não cliente da janela durante o layout passes.; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x00068F90 File Offset: 0x00068390
		// (set) Token: 0x06001ACB RID: 6859 RVA: 0x00068FA4 File Offset: 0x000683A4
		public bool TreatAncestorsAsNonClientArea
		{
			get
			{
				return this._treatAncestorsAsNonClientArea;
			}
			set
			{
				this._treatAncestorsAsNonClientArea = value;
			}
		}

		/// <summary>Obtém um valor que declara se a opacidade por pixel da janela do conteúdo de origem é respeitada.</summary>
		/// <returns>
		///   <see langword="true" /> Se usando a opacidade por pixel; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001ACC RID: 6860 RVA: 0x00068FB8 File Offset: 0x000683B8
		// (set) Token: 0x06001ACD RID: 6861 RVA: 0x00068FCC File Offset: 0x000683CC
		public bool UsesPerPixelOpacity
		{
			get
			{
				return this._usesPerPixelOpacity;
			}
			set
			{
				this._usesPerPixelOpacity = value;
			}
		}

		/// <summary>Obtém um valor que declara se a transparência por pixel da janela do conteúdo de origem é respeitada.</summary>
		/// <returns>
		///   <see langword="true" /> Se usar transparência por pixel; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x00068FE0 File Offset: 0x000683E0
		// (set) Token: 0x06001ACF RID: 6863 RVA: 0x00068FF4 File Offset: 0x000683F4
		public bool UsesPerPixelTransparency
		{
			get
			{
				return this._usesPerPixelTransparency;
			}
			set
			{
				this._usesPerPixelTransparency = value;
			}
		}

		/// <summary>Obtém ou define como o WPF controla a restauração do foco para a janela.</summary>
		/// <returns>Um dos valores de enumeração que especifica como o WPF lida com a restauração do foco da janela. O padrão é <see cref="F:System.Windows.Input.RestoreFocusMode.Auto" />.</returns>
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x00069008 File Offset: 0x00068408
		// (set) Token: 0x06001AD1 RID: 6865 RVA: 0x00069034 File Offset: 0x00068434
		public RestoreFocusMode RestoreFocusMode
		{
			get
			{
				RestoreFocusMode? restoreFocusMode = this._restoreFocusMode;
				if (restoreFocusMode == null)
				{
					return Keyboard.DefaultRestoreFocusMode;
				}
				return restoreFocusMode.GetValueOrDefault();
			}
			set
			{
				this._restoreFocusMode = new RestoreFocusMode?(value);
			}
		}

		/// <summary>Obtém ou define o valor que determina se é necessário adquirir o foco do Win32 para a janela que contém o WPF quando um <see cref="T:System.Windows.Interop.HwndSource" /> é criado.</summary>
		/// <returns>
		///   <see langword="true" /> para adquirir o foco do Win32 para a janela que contém quando o usuário interage com menus; o WPF Caso contrário, <see langword="false" />. <see langword="null" /> Para usar o valor de <see cref="P:System.Windows.Interop.HwndSource.DefaultAcquireHwndFocusInMenuMode" />.</returns>
		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x00069050 File Offset: 0x00068450
		// (set) Token: 0x06001AD3 RID: 6867 RVA: 0x0006907C File Offset: 0x0006847C
		public bool AcquireHwndFocusInMenuMode
		{
			get
			{
				bool? acquireHwndFocusInMenuMode = this._acquireHwndFocusInMenuMode;
				if (acquireHwndFocusInMenuMode == null)
				{
					return HwndSource.DefaultAcquireHwndFocusInMenuMode;
				}
				return acquireHwndFocusInMenuMode.GetValueOrDefault();
			}
			set
			{
				this._acquireHwndFocusInMenuMode = new bool?(value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o <see cref="T:System.Windows.Interop.HwndSource" /> deve receber mensagens de janela emitidas pela bomba de mensagens por meio do <see cref="T:System.Windows.Interop.ComponentDispatcher" />.</summary>
		/// <returns>
		///   <see langword="true" /> Se o <see cref="T:System.Windows.Interop.HwndSource" /> deve receber mensagens de janela emitidas pela bomba de mensagens por meio de <see cref="T:System.Windows.Interop.ComponentDispatcher" />; caso contrário, <see langword="false" />.  O padrão é <see langword="true" /> se o <see cref="T:System.Windows.Interop.HwndSource" /> corresponde a uma janela de nível superior; caso contrário, o padrão é <see langword="false" />.</returns>
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x00069098 File Offset: 0x00068498
		// (set) Token: 0x06001AD5 RID: 6869 RVA: 0x000690CC File Offset: 0x000684CC
		public bool TreatAsInputRoot
		{
			get
			{
				bool? treatAsInputRoot = this._treatAsInputRoot;
				if (treatAsInputRoot == null)
				{
					return (this._styleBits & 1073741824) == 0;
				}
				return treatAsInputRoot.GetValueOrDefault();
			}
			set
			{
				this._treatAsInputRoot = new bool?(value);
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x000690E8 File Offset: 0x000684E8
		internal bool EffectivePerPixelOpacity
		{
			get
			{
				if (!this._usesPerPixelTransparency)
				{
					return this._usesPerPixelOpacity && (this.WindowStyle & 1073741824) == 0;
				}
				if (this._usesPerPixelOpacity)
				{
					throw new InvalidOperationException(SR.Get("UsesPerPixelOpacityIsObsolete"));
				}
				return HwndSourceParameters.PlatformSupportsTransparentChildWindows || (this.WindowStyle & 1073741824) == 0;
			}
		}

		/// <summary>Determina se esta estrutura <see cref="T:System.Windows.Interop.HwndSourceParameters" /> é igual a outra estrutura <see cref="T:System.Windows.Interop.HwndSourceParameters" />.</summary>
		/// <param name="a">A primeira estrutura <see cref="T:System.Windows.Interop.HwndSourceParameters" /> a ser comparada.</param>
		/// <param name="b">A segunda estrutura <see cref="T:System.Windows.Interop.HwndSourceParameters" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se as estruturas forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001AD7 RID: 6871 RVA: 0x00069148 File Offset: 0x00068548
		public static bool operator ==(HwndSourceParameters a, HwndSourceParameters b)
		{
			return a.Equals(b);
		}

		/// <summary>Determina se uma estrutura <see cref="T:System.Windows.Interop.HwndSourceParameters" /> não é igual a outra estrutura <see cref="T:System.Windows.Interop.HwndSourceParameters" />.</summary>
		/// <param name="a">A primeira estrutura <see cref="T:System.Windows.Interop.HwndSourceParameters" /> a ser comparada.</param>
		/// <param name="b">A segunda estrutura <see cref="T:System.Windows.Interop.HwndSourceParameters" /> a ser comparada.</param>
		/// <returns>
		///   <see langword="true" /> se as estruturas não forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001AD8 RID: 6872 RVA: 0x00069160 File Offset: 0x00068560
		public static bool operator !=(HwndSourceParameters a, HwndSourceParameters b)
		{
			return !a.Equals(b);
		}

		/// <summary>Determina se esta estrutura é igual ao objeto especificado.</summary>
		/// <param name="obj">Os objetos a serem testados quanto à igualdade.</param>
		/// <returns>
		///   <see langword="true" /> se a comparação for igual; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001AD9 RID: 6873 RVA: 0x00069178 File Offset: 0x00068578
		public override bool Equals(object obj)
		{
			return obj != null && this.Equals((HwndSourceParameters)obj);
		}

		/// <summary>Determina se esta estrutura é igual à estrutura <see cref="T:System.Windows.Interop.HwndSourceParameters" /> especificada.</summary>
		/// <param name="obj">A estrutura a ser testada quanto à igualdade.</param>
		/// <returns>
		///   <see langword="true" /> se as estruturas forem iguais; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001ADA RID: 6874 RVA: 0x00069198 File Offset: 0x00068598
		public bool Equals(HwndSourceParameters obj)
		{
			return this._classStyleBits == obj._classStyleBits && this._styleBits == obj._styleBits && this._extendedStyleBits == obj._extendedStyleBits && this._x == obj._x && this._y == obj._y && this._width == obj._width && this._height == obj._height && this._name == obj._name && this._parent == obj._parent && this._hwndSourceHook == obj._hwndSourceHook && this._adjustSizingForNonClientArea == obj._adjustSizingForNonClientArea && this._hasAssignedSize == obj._hasAssignedSize && this._usesPerPixelOpacity == obj._usesPerPixelOpacity && this._usesPerPixelTransparency == obj._usesPerPixelTransparency;
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x0006928C File Offset: 0x0006868C
		internal static bool PlatformSupportsTransparentChildWindows
		{
			get
			{
				return HwndSourceParameters._platformSupportsTransparentChildWindows;
			}
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x000692A0 File Offset: 0x000686A0
		internal static void SetPlatformSupportsTransparentChildWindowsForTestingOnly(bool value)
		{
			if (string.Compare(Assembly.GetEntryAssembly().GetName().Name, "drthwndsource", true) == 0)
			{
				HwndSourceParameters._platformSupportsTransparentChildWindows = value;
			}
		}

		// Token: 0x04000E4C RID: 3660
		private int _classStyleBits;

		// Token: 0x04000E4D RID: 3661
		private int _styleBits;

		// Token: 0x04000E4E RID: 3662
		private int _extendedStyleBits;

		// Token: 0x04000E4F RID: 3663
		private int _x;

		// Token: 0x04000E50 RID: 3664
		private int _y;

		// Token: 0x04000E51 RID: 3665
		private int _width;

		// Token: 0x04000E52 RID: 3666
		private int _height;

		// Token: 0x04000E53 RID: 3667
		private string _name;

		// Token: 0x04000E54 RID: 3668
		private IntPtr _parent;

		// Token: 0x04000E55 RID: 3669
		private HwndSourceHook _hwndSourceHook;

		// Token: 0x04000E56 RID: 3670
		private bool _adjustSizingForNonClientArea;

		// Token: 0x04000E57 RID: 3671
		private bool _hasAssignedSize;

		// Token: 0x04000E58 RID: 3672
		private bool _usesPerPixelOpacity;

		// Token: 0x04000E59 RID: 3673
		private bool _usesPerPixelTransparency;

		// Token: 0x04000E5A RID: 3674
		private bool? _treatAsInputRoot;

		// Token: 0x04000E5B RID: 3675
		private bool _treatAncestorsAsNonClientArea;

		// Token: 0x04000E5C RID: 3676
		private RestoreFocusMode? _restoreFocusMode;

		// Token: 0x04000E5D RID: 3677
		private bool? _acquireHwndFocusInMenuMode;

		// Token: 0x04000E5E RID: 3678
		private static bool _platformSupportsTransparentChildWindows = Utilities.IsOSWindows8OrNewer;
	}
}
