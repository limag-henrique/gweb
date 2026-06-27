using System;
using System.Windows.Input;

namespace System.Windows
{
	/// <summary>Estabelece os eventos comuns e também as propriedades e os métodos relacionados a eventos para processamento de entrada básico pelos elementos WPF (Windows Presentation Foundation).</summary>
	// Token: 0x020001C5 RID: 453
	public interface IInputElement
	{
		/// <summary>Gera o evento roteado que é especificado pela propriedade <see cref="P:System.Windows.RoutedEventArgs.RoutedEvent" /> dentro do <see cref="T:System.Windows.RoutedEventArgs" /> fornecido.</summary>
		/// <param name="e">Uma instância da classe <see cref="T:System.Windows.RoutedEventArgs" /> que contém o identificador de evento a ser acionado.</param>
		// Token: 0x06000B98 RID: 2968
		void RaiseEvent(RoutedEventArgs e);

		/// <summary>Adiciona um manipulador de eventos roteados para um evento roteado específico a um elemento.</summary>
		/// <param name="routedEvent">O identificador do evento roteado sendo manipulado.</param>
		/// <param name="handler">Uma referência à implementação do manipulador.</param>
		// Token: 0x06000B99 RID: 2969
		void AddHandler(RoutedEvent routedEvent, Delegate handler);

		/// <summary>Remove todas as instâncias do manipulador de eventos roteados especificado desse elemento.</summary>
		/// <param name="routedEvent">O identificador do evento roteado ao qual o manipulador está anexado.</param>
		/// <param name="handler">A implementação do manipulador específico a ser removido da coleção de manipuladores de eventos.</param>
		// Token: 0x06000B9A RID: 2970
		void RemoveHandler(RoutedEvent routedEvent, Delegate handler);

		/// <summary>Obtém um valor que indica se o ponteiro do mouse está localizado sobre esse elemento (incluindo os elementos filhos visuais que estão dentro de seus limites).</summary>
		/// <returns>
		///   <see langword="true" /> se o ponteiro do mouse estiver sobre o elemento ou seus elementos filho; caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000B9B RID: 2971
		bool IsMouseOver { get; }

		/// <summary>Ocorre quando o botão esquerdo do mouse é pressionado enquanto o ponteiro do mouse está sobre o elemento.</summary>
		// Token: 0x14000115 RID: 277
		// (add) Token: 0x06000B9C RID: 2972
		// (remove) Token: 0x06000B9D RID: 2973
		event MouseButtonEventHandler PreviewMouseLeftButtonDown;

		/// <summary>Ocorre quando o botão esquerdo do mouse é pressionado enquanto o ponteiro do mouse está sobre o elemento.</summary>
		// Token: 0x14000116 RID: 278
		// (add) Token: 0x06000B9E RID: 2974
		// (remove) Token: 0x06000B9F RID: 2975
		event MouseButtonEventHandler MouseLeftButtonDown;

		/// <summary>Ocorre quando o botão esquerdo do mouse é liberado enquanto o ponteiro do mouse está sobre o elemento.</summary>
		// Token: 0x14000117 RID: 279
		// (add) Token: 0x06000BA0 RID: 2976
		// (remove) Token: 0x06000BA1 RID: 2977
		event MouseButtonEventHandler PreviewMouseLeftButtonUp;

		/// <summary>Ocorre quando o botão esquerdo do mouse é liberado enquanto o ponteiro do mouse está sobre o elemento.</summary>
		// Token: 0x14000118 RID: 280
		// (add) Token: 0x06000BA2 RID: 2978
		// (remove) Token: 0x06000BA3 RID: 2979
		event MouseButtonEventHandler MouseLeftButtonUp;

		/// <summary>Ocorre quando o botão direito do mouse é pressionado enquanto o ponteiro do mouse está sobre o elemento.</summary>
		// Token: 0x14000119 RID: 281
		// (add) Token: 0x06000BA4 RID: 2980
		// (remove) Token: 0x06000BA5 RID: 2981
		event MouseButtonEventHandler PreviewMouseRightButtonDown;

		/// <summary>Ocorre quando o botão direito do mouse é pressionado enquanto o ponteiro do mouse está sobre o elemento.</summary>
		// Token: 0x1400011A RID: 282
		// (add) Token: 0x06000BA6 RID: 2982
		// (remove) Token: 0x06000BA7 RID: 2983
		event MouseButtonEventHandler MouseRightButtonDown;

		/// <summary>Ocorre quando o botão direito do mouse é liberado enquanto o ponteiro do mouse está sobre o elemento.</summary>
		// Token: 0x1400011B RID: 283
		// (add) Token: 0x06000BA8 RID: 2984
		// (remove) Token: 0x06000BA9 RID: 2985
		event MouseButtonEventHandler PreviewMouseRightButtonUp;

		/// <summary>Ocorre quando o botão direito do mouse é liberado enquanto o ponteiro do mouse está sobre o elemento.</summary>
		// Token: 0x1400011C RID: 284
		// (add) Token: 0x06000BAA RID: 2986
		// (remove) Token: 0x06000BAB RID: 2987
		event MouseButtonEventHandler MouseRightButtonUp;

		/// <summary>Ocorre quando o ponteiro do mouse se move enquanto está sobre o elemento.</summary>
		// Token: 0x1400011D RID: 285
		// (add) Token: 0x06000BAC RID: 2988
		// (remove) Token: 0x06000BAD RID: 2989
		event MouseEventHandler PreviewMouseMove;

		/// <summary>Ocorre quando o ponteiro do mouse se move enquanto está sobre o elemento.</summary>
		// Token: 0x1400011E RID: 286
		// (add) Token: 0x06000BAE RID: 2990
		// (remove) Token: 0x06000BAF RID: 2991
		event MouseEventHandler MouseMove;

		/// <summary>Ocorre quando o botão de rolagem do mouse se move enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x1400011F RID: 287
		// (add) Token: 0x06000BB0 RID: 2992
		// (remove) Token: 0x06000BB1 RID: 2993
		event MouseWheelEventHandler PreviewMouseWheel;

		/// <summary>Ocorre quando o botão de rolagem do mouse se move enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x14000120 RID: 288
		// (add) Token: 0x06000BB2 RID: 2994
		// (remove) Token: 0x06000BB3 RID: 2995
		event MouseWheelEventHandler MouseWheel;

		/// <summary>Obtém um valor que indica se o ponteiro do mouse está sobre esse elemento no sentido de teste de clique mais rígido.</summary>
		/// <returns>
		///   <see langword="true" /> Se o ponteiro do mouse está sobre este elemento; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000BB4 RID: 2996
		bool IsMouseDirectlyOver { get; }

		/// <summary>Ocorre quando o ponteiro do mouse entra nos limites deste elemento.</summary>
		// Token: 0x14000121 RID: 289
		// (add) Token: 0x06000BB5 RID: 2997
		// (remove) Token: 0x06000BB6 RID: 2998
		event MouseEventHandler MouseEnter;

		/// <summary>Ocorre quando o ponteiro do mouse sai dos limites deste elemento.</summary>
		// Token: 0x14000122 RID: 290
		// (add) Token: 0x06000BB7 RID: 2999
		// (remove) Token: 0x06000BB8 RID: 3000
		event MouseEventHandler MouseLeave;

		/// <summary>Ocorre quando o elemento captura o mouse.</summary>
		// Token: 0x14000123 RID: 291
		// (add) Token: 0x06000BB9 RID: 3001
		// (remove) Token: 0x06000BBA RID: 3002
		event MouseEventHandler GotMouseCapture;

		/// <summary>Ocorre quando este elemento perde a captura do mouse.</summary>
		// Token: 0x14000124 RID: 292
		// (add) Token: 0x06000BBB RID: 3003
		// (remove) Token: 0x06000BBC RID: 3004
		event MouseEventHandler LostMouseCapture;

		/// <summary>Obtém um valor que indica se o mouse é capturado para esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento tiver a captura do mouse; caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000BBD RID: 3005
		bool IsMouseCaptured { get; }

		/// <summary>Tenta forçar a captura do mouse para esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se o mouse for capturado com êxito; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000BBE RID: 3006
		bool CaptureMouse();

		/// <summary>Libera a captura do mouse, se esse elemento tiver mantido a captura.</summary>
		// Token: 0x06000BBF RID: 3007
		void ReleaseMouseCapture();

		/// <summary>Obtém um valor que indica se a caneta está localizada sobre esse elemento (ou os elementos filhos visuais que estão dentro de seus limites).</summary>
		/// <returns>
		///   <see langword="true" /> se o cursor da caneta estiver sobre o elemento ou seus elementos filho; caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000BC0 RID: 3008
		bool IsStylusOver { get; }

		/// <summary>Ocorre quando a caneta toca o digitalizador enquanto está sobre esse elemento.</summary>
		// Token: 0x14000125 RID: 293
		// (add) Token: 0x06000BC1 RID: 3009
		// (remove) Token: 0x06000BC2 RID: 3010
		event StylusDownEventHandler PreviewStylusDown;

		/// <summary>Ocorre quando a caneta toca o digitalizador enquanto está sobre esse elemento.</summary>
		// Token: 0x14000126 RID: 294
		// (add) Token: 0x06000BC3 RID: 3011
		// (remove) Token: 0x06000BC4 RID: 3012
		event StylusDownEventHandler StylusDown;

		/// <summary>Ocorre quando a caneta é retirada do digitalizador enquanto está sobre esse elemento.</summary>
		// Token: 0x14000127 RID: 295
		// (add) Token: 0x06000BC5 RID: 3013
		// (remove) Token: 0x06000BC6 RID: 3014
		event StylusEventHandler PreviewStylusUp;

		/// <summary>Ocorre quando a caneta é retirada do digitalizador enquanto está sobre esse elemento.</summary>
		// Token: 0x14000128 RID: 296
		// (add) Token: 0x06000BC7 RID: 3015
		// (remove) Token: 0x06000BC8 RID: 3016
		event StylusEventHandler StylusUp;

		/// <summary>Ocorre quando a caneta se move enquanto está sobre o elemento.</summary>
		// Token: 0x14000129 RID: 297
		// (add) Token: 0x06000BC9 RID: 3017
		// (remove) Token: 0x06000BCA RID: 3018
		event StylusEventHandler PreviewStylusMove;

		/// <summary>Ocorre quando o cursor da caneta se move sobre o elemento.</summary>
		// Token: 0x1400012A RID: 298
		// (add) Token: 0x06000BCB RID: 3019
		// (remove) Token: 0x06000BCC RID: 3020
		event StylusEventHandler StylusMove;

		/// <summary>Ocorre quando a caneta se move sobre um elemento, mas sem tocar o digitalizador.</summary>
		// Token: 0x1400012B RID: 299
		// (add) Token: 0x06000BCD RID: 3021
		// (remove) Token: 0x06000BCE RID: 3022
		event StylusEventHandler PreviewStylusInAirMove;

		/// <summary>Ocorre quando a caneta se move sobre um elemento, mas sem tocar o digitalizador.</summary>
		// Token: 0x1400012C RID: 300
		// (add) Token: 0x06000BCF RID: 3023
		// (remove) Token: 0x06000BD0 RID: 3024
		event StylusEventHandler StylusInAirMove;

		/// <summary>Ocorre quando o cursor da caneta entra nos limites do elemento.</summary>
		// Token: 0x1400012D RID: 301
		// (add) Token: 0x06000BD1 RID: 3025
		// (remove) Token: 0x06000BD2 RID: 3026
		event StylusEventHandler StylusEnter;

		/// <summary>Ocorre quando o cursor da caneta sai dos limites do elemento.</summary>
		// Token: 0x1400012E RID: 302
		// (add) Token: 0x06000BD3 RID: 3027
		// (remove) Token: 0x06000BD4 RID: 3028
		event StylusEventHandler StylusLeave;

		/// <summary>Ocorre quando a caneta está perto o suficiente do digitalizador para ser detectada.</summary>
		// Token: 0x1400012F RID: 303
		// (add) Token: 0x06000BD5 RID: 3029
		// (remove) Token: 0x06000BD6 RID: 3030
		event StylusEventHandler PreviewStylusInRange;

		/// <summary>Ocorre quando a caneta está perto o suficiente do digitalizador para ser detectada.</summary>
		// Token: 0x14000130 RID: 304
		// (add) Token: 0x06000BD7 RID: 3031
		// (remove) Token: 0x06000BD8 RID: 3032
		event StylusEventHandler StylusInRange;

		/// <summary>Ocorre quando a caneta está longe demais do digitalizador para ser detectada.</summary>
		// Token: 0x14000131 RID: 305
		// (add) Token: 0x06000BD9 RID: 3033
		// (remove) Token: 0x06000BDA RID: 3034
		event StylusEventHandler PreviewStylusOutOfRange;

		/// <summary>Ocorre quando a caneta está longe demais do digitalizador para ser detectada.</summary>
		// Token: 0x14000132 RID: 306
		// (add) Token: 0x06000BDB RID: 3035
		// (remove) Token: 0x06000BDC RID: 3036
		event StylusEventHandler StylusOutOfRange;

		/// <summary>Ocorre quando um dos diversos gestos da caneta é detectado, por exemplo, <see cref="F:System.Windows.Input.SystemGesture.Tap" /> ou <see cref="F:System.Windows.Input.SystemGesture.Drag" />.</summary>
		// Token: 0x14000133 RID: 307
		// (add) Token: 0x06000BDD RID: 3037
		// (remove) Token: 0x06000BDE RID: 3038
		event StylusSystemGestureEventHandler PreviewStylusSystemGesture;

		/// <summary>Ocorre quando um dos diversos gestos da caneta é detectado, por exemplo, <see cref="F:System.Windows.Input.SystemGesture.Tap" /> ou <see cref="F:System.Windows.Input.SystemGesture.Drag" />.</summary>
		// Token: 0x14000134 RID: 308
		// (add) Token: 0x06000BDF RID: 3039
		// (remove) Token: 0x06000BE0 RID: 3040
		event StylusSystemGestureEventHandler StylusSystemGesture;

		/// <summary>Ocorre quando o botão da caneta é pressionado enquanto a caneta está sobre esse elemento.</summary>
		// Token: 0x14000135 RID: 309
		// (add) Token: 0x06000BE1 RID: 3041
		// (remove) Token: 0x06000BE2 RID: 3042
		event StylusButtonEventHandler StylusButtonDown;

		/// <summary>Ocorre quando o botão da caneta é pressionado enquanto a caneta está sobre esse elemento.</summary>
		// Token: 0x14000136 RID: 310
		// (add) Token: 0x06000BE3 RID: 3043
		// (remove) Token: 0x06000BE4 RID: 3044
		event StylusButtonEventHandler PreviewStylusButtonDown;

		/// <summary>Ocorre quando o botão da caneta é liberado enquanto a caneta está sobre esse elemento.</summary>
		// Token: 0x14000137 RID: 311
		// (add) Token: 0x06000BE5 RID: 3045
		// (remove) Token: 0x06000BE6 RID: 3046
		event StylusButtonEventHandler PreviewStylusButtonUp;

		/// <summary>Ocorre quando o botão da caneta é liberado enquanto a caneta está sobre esse elemento.</summary>
		// Token: 0x14000138 RID: 312
		// (add) Token: 0x06000BE7 RID: 3047
		// (remove) Token: 0x06000BE8 RID: 3048
		event StylusButtonEventHandler StylusButtonUp;

		/// <summary>Obtém um valor que indica se a caneta está sobre esse elemento no sentido de teste de clique mais rígido.</summary>
		/// <returns>
		///   <see langword="true" /> Se a caneta está sobre o elemento. Caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000BE9 RID: 3049
		bool IsStylusDirectlyOver { get; }

		/// <summary>Ocorre quando o elemento captura a caneta.</summary>
		// Token: 0x14000139 RID: 313
		// (add) Token: 0x06000BEA RID: 3050
		// (remove) Token: 0x06000BEB RID: 3051
		event StylusEventHandler GotStylusCapture;

		/// <summary>Ocorre quando este elemento perde a captura da caneta.</summary>
		// Token: 0x1400013A RID: 314
		// (add) Token: 0x06000BEC RID: 3052
		// (remove) Token: 0x06000BED RID: 3053
		event StylusEventHandler LostStylusCapture;

		/// <summary>Obtém um valor que indica se a caneta é capturada para esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento tem captura da caneta; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000BEE RID: 3054
		bool IsStylusCaptured { get; }

		/// <summary>Tenta forçar a captura da caneta para esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se a caneta for capturada com êxito, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000BEF RID: 3055
		bool CaptureStylus();

		/// <summary>Libera a captura da caneta, se esse elemento tiver mantido a captura.</summary>
		// Token: 0x06000BF0 RID: 3056
		void ReleaseStylusCapture();

		/// <summary>Ocorre quando uma tecla é pressionada enquanto o teclado está focalizado neste elemento.</summary>
		// Token: 0x1400013B RID: 315
		// (add) Token: 0x06000BF1 RID: 3057
		// (remove) Token: 0x06000BF2 RID: 3058
		event KeyEventHandler PreviewKeyDown;

		/// <summary>Ocorre quando uma tecla é pressionada enquanto o teclado está focalizado neste elemento.</summary>
		// Token: 0x1400013C RID: 316
		// (add) Token: 0x06000BF3 RID: 3059
		// (remove) Token: 0x06000BF4 RID: 3060
		event KeyEventHandler KeyDown;

		/// <summary>Ocorre quando uma tecla é liberada enquanto o teclado está focalizado neste elemento.</summary>
		// Token: 0x1400013D RID: 317
		// (add) Token: 0x06000BF5 RID: 3061
		// (remove) Token: 0x06000BF6 RID: 3062
		event KeyEventHandler PreviewKeyUp;

		/// <summary>Ocorre quando uma tecla é liberada enquanto o teclado está focalizado neste elemento.</summary>
		// Token: 0x1400013E RID: 318
		// (add) Token: 0x06000BF7 RID: 3063
		// (remove) Token: 0x06000BF8 RID: 3064
		event KeyEventHandler KeyUp;

		/// <summary>Obtém um valor que indica se o foco do teclado em qualquer lugar dentro dos limites do elemento, incluindo se o foco do teclado, está dentro dos limites de todos os objetos visuais filhos.</summary>
		/// <returns>
		///   <see langword="true" /> se o foco do teclado está no elemento ou em seus elementos filho; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000BF9 RID: 3065
		bool IsKeyboardFocusWithin { get; }

		/// <summary>Obtém um valor que indica se esse elemento tem o foco do teclado.</summary>
		/// <returns>
		///   <see langword="true" /> se esse elemento tiver o foco do teclado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000BFA RID: 3066
		bool IsKeyboardFocused { get; }

		/// <summary>Tenta focar o teclado nesse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se o foco do teclado for movido para esse elemento ou já estiver nesse elemento; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000BFB RID: 3067
		bool Focus();

		/// <summary>Ocorre quando o teclado está focalizado neste elemento.</summary>
		// Token: 0x1400013F RID: 319
		// (add) Token: 0x06000BFC RID: 3068
		// (remove) Token: 0x06000BFD RID: 3069
		event KeyboardFocusChangedEventHandler PreviewGotKeyboardFocus;

		/// <summary>Ocorre quando o teclado está focalizado neste elemento.</summary>
		// Token: 0x14000140 RID: 320
		// (add) Token: 0x06000BFE RID: 3070
		// (remove) Token: 0x06000BFF RID: 3071
		event KeyboardFocusChangedEventHandler GotKeyboardFocus;

		/// <summary>Ocorre quando o teclado não está mais focalizado neste elemento.</summary>
		// Token: 0x14000141 RID: 321
		// (add) Token: 0x06000C00 RID: 3072
		// (remove) Token: 0x06000C01 RID: 3073
		event KeyboardFocusChangedEventHandler PreviewLostKeyboardFocus;

		/// <summary>Ocorre quando o teclado não está mais focalizado neste elemento.</summary>
		// Token: 0x14000142 RID: 322
		// (add) Token: 0x06000C02 RID: 3074
		// (remove) Token: 0x06000C03 RID: 3075
		event KeyboardFocusChangedEventHandler LostKeyboardFocus;

		/// <summary>Obtém um valor que indica se esse elemento está habilitado no UI (interface do usuário).</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento estiver habilitado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000C04 RID: 3076
		bool IsEnabled { get; }

		/// <summary>Obtém ou define um valor que indica se é possível definir o foco para este elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento puder ter o foco definido para ele; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000C05 RID: 3077
		// (set) Token: 0x06000C06 RID: 3078
		bool Focusable { get; set; }

		/// <summary>Ocorre quando este elemento obtém texto de forma independente de dispositivo.</summary>
		// Token: 0x14000143 RID: 323
		// (add) Token: 0x06000C07 RID: 3079
		// (remove) Token: 0x06000C08 RID: 3080
		event TextCompositionEventHandler PreviewTextInput;

		/// <summary>Ocorre quando este elemento obtém texto de forma independente de dispositivo.</summary>
		// Token: 0x14000144 RID: 324
		// (add) Token: 0x06000C09 RID: 3081
		// (remove) Token: 0x06000C0A RID: 3082
		event TextCompositionEventHandler TextInput;
	}
}
