using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Markup;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Define um gesto de entrada de teclado que pode ser usado para invocar um comando.</summary>
	// Token: 0x02000217 RID: 535
	[TypeConverter(typeof(KeyGestureConverter))]
	[ValueSerializer(typeof(KeyGestureValueSerializer))]
	public class KeyGesture : InputGesture
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.KeyGesture" /> com o <see cref="T:System.Windows.Input.Key" /> especificado.</summary>
		/// <param name="key">A chave associada a este gesto.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="key" /> não é um <see cref="T:System.Windows.Input.Key" /> válido.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="key" /> não é um <see cref="T:System.Windows.Input.KeyGesture" /> válido.</exception>
		// Token: 0x06000E5F RID: 3679 RVA: 0x000366E4 File Offset: 0x00035AE4
		public KeyGesture(Key key) : this(key, ModifierKeys.None)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.KeyGesture" /> com o <see cref="T:System.Windows.Input.Key" /> e <see cref="T:System.Windows.Input.ModifierKeys" /> especificados.</summary>
		/// <param name="key">A chave associada ao gesto.</param>
		/// <param name="modifiers">As teclas modificadoras associadas ao gesto.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="modifiers" /> não é um <see cref="T:System.Windows.Input.ModifierKeys" /> válido  
		///
		/// ou - 
		/// <paramref name="key" /> não é um <see cref="T:System.Windows.Input.Key" /> válido.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="key" /> e <paramref name="modifiers" /> não formam um <see cref="T:System.Windows.Input.KeyGesture" /> válido.</exception>
		// Token: 0x06000E60 RID: 3680 RVA: 0x000366FC File Offset: 0x00035AFC
		public KeyGesture(Key key, ModifierKeys modifiers) : this(key, modifiers, string.Empty, true)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.KeyGesture" /> com a cadeia de caracteres de exibição, <see cref="T:System.Windows.Input.Key" /> e <see cref="T:System.Windows.Input.ModifierKeys" /> especificados.</summary>
		/// <param name="key">A chave associada ao gesto.</param>
		/// <param name="modifiers">As teclas modificadoras associadas ao gesto.</param>
		/// <param name="displayString">Uma representação da cadeia de caracteres do <see cref="T:System.Windows.Input.KeyGesture" />.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="modifiers" /> não é um <see cref="T:System.Windows.Input.ModifierKeys" /> válido  
		///
		/// ou - 
		/// <paramref name="key" /> não é um <see cref="T:System.Windows.Input.Key" /> válido.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="displayString" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="key" /> e <paramref name="modifiers" /> não formam um <see cref="T:System.Windows.Input.KeyGesture" /> válido.</exception>
		// Token: 0x06000E61 RID: 3681 RVA: 0x00036718 File Offset: 0x00035B18
		public KeyGesture(Key key, ModifierKeys modifiers, string displayString) : this(key, modifiers, displayString, true)
		{
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00036730 File Offset: 0x00035B30
		internal KeyGesture(Key key, ModifierKeys modifiers, bool validateGesture) : this(key, modifiers, string.Empty, validateGesture)
		{
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0003674C File Offset: 0x00035B4C
		private KeyGesture(Key key, ModifierKeys modifiers, string displayString, bool validateGesture)
		{
			if (!ModifierKeysConverter.IsDefinedModifierKeys(modifiers))
			{
				throw new InvalidEnumArgumentException("modifiers", (int)modifiers, typeof(ModifierKeys));
			}
			if (!KeyGesture.IsDefinedKey(key))
			{
				throw new InvalidEnumArgumentException("key", (int)key, typeof(Key));
			}
			if (displayString == null)
			{
				throw new ArgumentNullException("displayString");
			}
			if (validateGesture && !KeyGesture.IsValid(key, modifiers))
			{
				throw new NotSupportedException(SR.Get("KeyGesture_Invalid", new object[]
				{
					modifiers,
					key
				}));
			}
			this._modifiers = modifiers;
			this._key = key;
			this._displayString = displayString;
		}

		/// <summary>Obtém as teclas modificadoras associadas a este <see cref="T:System.Windows.Input.KeyGesture" />.</summary>
		/// <returns>As teclas modificadoras associadas ao gesto. O valor padrão é <see cref="F:System.Windows.Input.ModifierKeys.None" />.</returns>
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x000367F4 File Offset: 0x00035BF4
		public ModifierKeys Modifiers
		{
			get
			{
				return this._modifiers;
			}
		}

		/// <summary>Obtém a chave associada a este <see cref="T:System.Windows.Input.KeyGesture" />.</summary>
		/// <returns>A chave associada ao gesto.  O valor padrão é <see cref="F:System.Windows.Input.Key.None" />.</returns>
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x00036808 File Offset: 0x00035C08
		public Key Key
		{
			get
			{
				return this._key;
			}
		}

		/// <summary>Obtém uma representação da cadeia de caracteres desse <see cref="T:System.Windows.Input.KeyGesture" />.</summary>
		/// <returns>A cadeia de caracteres de exibição para este <see cref="T:System.Windows.Input.KeyGesture" />. O valor padrão é <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0003681C File Offset: 0x00035C1C
		public string DisplayString
		{
			get
			{
				return this._displayString;
			}
		}

		/// <summary>Retorna uma cadeia de caracteres que pode ser usada para exibir o <see cref="T:System.Windows.Input.KeyGesture" />.</summary>
		/// <param name="culture">As informações específicas à cultura.</param>
		/// <returns>A cadeia de caracteres a exibir</returns>
		// Token: 0x06000E67 RID: 3687 RVA: 0x00036830 File Offset: 0x00035C30
		public string GetDisplayStringForCulture(CultureInfo culture)
		{
			if (!string.IsNullOrEmpty(this._displayString))
			{
				return this._displayString;
			}
			return (string)KeyGesture._keyGestureConverter.ConvertTo(null, culture, this, typeof(string));
		}

		/// <summary>Determina se este <see cref="T:System.Windows.Input.KeyGesture" /> corresponde à entrada associada ao objeto <see cref="T:System.Windows.Input.InputEventArgs" /> especificado.</summary>
		/// <param name="targetElement">O destino.</param>
		/// <param name="inputEventArgs">Os dados de evento de entrada aos quais comparar esse gesto.</param>
		/// <returns>
		///   <see langword="true" /> se os dados do evento corresponderem a este <see cref="T:System.Windows.Input.KeyGesture" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000E68 RID: 3688 RVA: 0x00036870 File Offset: 0x00035C70
		public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
		{
			KeyEventArgs keyEventArgs = inputEventArgs as KeyEventArgs;
			return keyEventArgs != null && KeyGesture.IsDefinedKey(keyEventArgs.Key) && this.Key == keyEventArgs.RealKey && this.Modifiers == Keyboard.Modifiers;
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x000368B4 File Offset: 0x00035CB4
		internal static bool IsDefinedKey(Key key)
		{
			return key >= Key.None && key <= Key.OemClear;
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x000368D4 File Offset: 0x00035CD4
		internal static bool IsValid(Key key, ModifierKeys modifiers)
		{
			if ((key < Key.F1 || key > Key.F24) && (key < Key.NumPad0 || key > Key.Divide))
			{
				if ((modifiers & (ModifierKeys.Alt | ModifierKeys.Control | ModifierKeys.Windows)) != ModifierKeys.None)
				{
					return key - Key.LWin > 1 && key - Key.LeftCtrl > 3;
				}
				if ((key >= Key.D0 && key <= Key.D9) || (key >= Key.A && key <= Key.Z))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x00036924 File Offset: 0x00035D24
		internal static void AddGesturesFromResourceStrings(string keyGestures, string displayStrings, InputGestureCollection gestures)
		{
			while (!string.IsNullOrEmpty(keyGestures))
			{
				int num = keyGestures.IndexOf(";", StringComparison.Ordinal);
				string keyGestureToken;
				if (num >= 0)
				{
					keyGestureToken = keyGestures.Substring(0, num);
					keyGestures = keyGestures.Substring(num + 1);
				}
				else
				{
					keyGestureToken = keyGestures;
					keyGestures = string.Empty;
				}
				num = displayStrings.IndexOf(";", StringComparison.Ordinal);
				string keyDisplayString;
				if (num >= 0)
				{
					keyDisplayString = displayStrings.Substring(0, num);
					displayStrings = displayStrings.Substring(num + 1);
				}
				else
				{
					keyDisplayString = displayStrings;
					displayStrings = string.Empty;
				}
				KeyGesture keyGesture = KeyGesture.CreateFromResourceStrings(keyGestureToken, keyDisplayString);
				if (keyGesture != null)
				{
					gestures.Add(keyGesture);
				}
			}
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x000369B0 File Offset: 0x00035DB0
		internal static KeyGesture CreateFromResourceStrings(string keyGestureToken, string keyDisplayString)
		{
			if (!string.IsNullOrEmpty(keyDisplayString))
			{
				keyGestureToken = keyGestureToken + "," + keyDisplayString;
			}
			return KeyGesture._keyGestureConverter.ConvertFromInvariantString(keyGestureToken) as KeyGesture;
		}

		// Token: 0x0400083B RID: 2107
		private ModifierKeys _modifiers;

		// Token: 0x0400083C RID: 2108
		private Key _key;

		// Token: 0x0400083D RID: 2109
		private string _displayString;

		// Token: 0x0400083E RID: 2110
		private const string MULTIPLEGESTURE_DELIMITER = ";";

		// Token: 0x0400083F RID: 2111
		private static TypeConverter _keyGestureConverter = new KeyGestureConverter();
	}
}
