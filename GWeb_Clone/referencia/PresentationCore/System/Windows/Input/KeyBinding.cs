using System;
using System.ComponentModel;
using System.Windows.Markup;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Associa um <see cref="T:System.Windows.Input.KeyGesture" /> a um <see cref="T:System.Windows.Input.RoutedCommand" /> (ou outra implementação <see cref="T:System.Windows.Input.ICommand" />).</summary>
	// Token: 0x02000216 RID: 534
	public class KeyBinding : InputBinding
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.KeyBinding" />.</summary>
		// Token: 0x06000E50 RID: 3664 RVA: 0x0003641C File Offset: 0x0003581C
		public KeyBinding()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.KeyBinding" /> usando o <see cref="T:System.Windows.Input.ICommand" /> e <see cref="T:System.Windows.Input.KeyGesture" /> especificados.</summary>
		/// <param name="command">O comando a ser associado a <paramref name="gesture" />.</param>
		/// <param name="gesture">A combinação de teclas a associar a <paramref name="command" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="command" /> ou <paramref name="gesture" /> é <see langword="null" />.</exception>
		// Token: 0x06000E51 RID: 3665 RVA: 0x00036430 File Offset: 0x00035830
		public KeyBinding(ICommand command, KeyGesture gesture) : base(command, gesture)
		{
			this.SynchronizePropertiesFromGesture(gesture);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.KeyBinding" /> usando o <see cref="T:System.Windows.Input.ICommand" /> especificado e o <see cref="T:System.Windows.Input.Key" /> e o <see cref="T:System.Windows.Input.ModifierKeys" /> especificados que serão convertidos em um <see cref="T:System.Windows.Input.KeyGesture" />.</summary>
		/// <param name="command">O comando a ser invocado.</param>
		/// <param name="key">A tecla a ser associada a <paramref name="command" />.</param>
		/// <param name="modifiers">Os modificadores a serem associados a <paramref name="command" />.</param>
		// Token: 0x06000E52 RID: 3666 RVA: 0x0003644C File Offset: 0x0003584C
		public KeyBinding(ICommand command, Key key, ModifierKeys modifiers) : this(command, new KeyGesture(key, modifiers))
		{
		}

		/// <summary>Obtém ou define o gesto associado a essa <see cref="T:System.Windows.Input.KeyBinding" />.</summary>
		/// <returns>A sequência de teclas. O valor padrão é <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">o valor para o qual o <paramref name="gesture" /> está sendo definido não é um <see cref="T:System.Windows.Input.KeyGesture" />.</exception>
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x00036468 File Offset: 0x00035868
		// (set) Token: 0x06000E54 RID: 3668 RVA: 0x00036480 File Offset: 0x00035880
		[ValueSerializer(typeof(KeyGestureValueSerializer))]
		[TypeConverter(typeof(KeyGestureConverter))]
		public override InputGesture Gesture
		{
			get
			{
				return base.Gesture as KeyGesture;
			}
			set
			{
				KeyGesture keyGesture = value as KeyGesture;
				if (keyGesture != null)
				{
					base.Gesture = value;
					this.SynchronizePropertiesFromGesture(keyGesture);
					return;
				}
				throw new ArgumentException(SR.Get("InputBinding_ExpectedInputGesture", new object[]
				{
					typeof(KeyGesture)
				}));
			}
		}

		/// <summary>Obtém ou define a <see cref="T:System.Windows.Input.ModifierKeys" /> do <see cref="T:System.Windows.Input.KeyGesture" /> associado a essa <see cref="T:System.Windows.Input.KeyBinding" />.</summary>
		/// <returns>As teclas modificadoras do <see cref="T:System.Windows.Input.KeyGesture" />.  O valor padrão é <see cref="F:System.Windows.Input.ModifierKeys.None" />.</returns>
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x000364C8 File Offset: 0x000358C8
		// (set) Token: 0x06000E56 RID: 3670 RVA: 0x000364E8 File Offset: 0x000358E8
		public ModifierKeys Modifiers
		{
			get
			{
				return (ModifierKeys)base.GetValue(KeyBinding.ModifiersProperty);
			}
			set
			{
				base.SetValue(KeyBinding.ModifiersProperty, value);
			}
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00036508 File Offset: 0x00035908
		private static void OnModifiersPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			KeyBinding keyBinding = (KeyBinding)d;
			keyBinding.SynchronizeGestureFromProperties(keyBinding.Key, (ModifierKeys)e.NewValue);
		}

		/// <summary>Obtém ou define a <see cref="T:System.Windows.Input.Key" /> do <see cref="T:System.Windows.Input.KeyGesture" /> associado a essa <see cref="T:System.Windows.Input.KeyBinding" />.</summary>
		/// <returns>A parte principal do <see cref="T:System.Windows.Input.KeyGesture" />. O valor padrão é <see cref="F:System.Windows.Input.Key.None" />.</returns>
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x00036534 File Offset: 0x00035934
		// (set) Token: 0x06000E59 RID: 3673 RVA: 0x00036554 File Offset: 0x00035954
		public Key Key
		{
			get
			{
				return (Key)base.GetValue(KeyBinding.KeyProperty);
			}
			set
			{
				base.SetValue(KeyBinding.KeyProperty, value);
			}
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00036574 File Offset: 0x00035974
		private static void OnKeyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			KeyBinding keyBinding = (KeyBinding)d;
			keyBinding.SynchronizeGestureFromProperties((Key)e.NewValue, keyBinding.Modifiers);
		}

		/// <summary>Cria uma instância de um <see cref="T:System.Windows.Input.KeyBinding" />.</summary>
		/// <returns>O novo objeto.</returns>
		// Token: 0x06000E5B RID: 3675 RVA: 0x000365A0 File Offset: 0x000359A0
		protected override Freezable CreateInstanceCore()
		{
			return new KeyBinding();
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x000365B4 File Offset: 0x000359B4
		private void SynchronizePropertiesFromGesture(KeyGesture keyGesture)
		{
			if (!this._settingGesture)
			{
				this._settingGesture = true;
				try
				{
					this.Key = keyGesture.Key;
					this.Modifiers = keyGesture.Modifiers;
				}
				finally
				{
					this._settingGesture = false;
				}
			}
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00036610 File Offset: 0x00035A10
		private void SynchronizeGestureFromProperties(Key key, ModifierKeys modifiers)
		{
			if (!this._settingGesture)
			{
				this._settingGesture = true;
				try
				{
					this.Gesture = new KeyGesture(key, modifiers, false);
				}
				finally
				{
					this._settingGesture = false;
				}
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Input.KeyBinding.Modifiers" />.</summary>
		// Token: 0x04000838 RID: 2104
		public static readonly DependencyProperty ModifiersProperty = DependencyProperty.Register("Modifiers", typeof(ModifierKeys), typeof(KeyBinding), new UIPropertyMetadata(ModifierKeys.None, new PropertyChangedCallback(KeyBinding.OnModifiersPropertyChanged)));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Input.KeyBinding.Key" />.</summary>
		// Token: 0x04000839 RID: 2105
		public static readonly DependencyProperty KeyProperty = DependencyProperty.Register("Key", typeof(Key), typeof(KeyBinding), new UIPropertyMetadata(Key.None, new PropertyChangedCallback(KeyBinding.OnKeyPropertyChanged)));

		// Token: 0x0400083A RID: 2106
		private bool _settingGesture;
	}
}
