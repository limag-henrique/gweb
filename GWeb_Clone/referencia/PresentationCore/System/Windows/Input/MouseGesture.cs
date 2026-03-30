using System;
using System.ComponentModel;
using System.Windows.Markup;

namespace System.Windows.Input
{
	/// <summary>Define um gesto de entrada de mouse que pode ser usado para invocar um comando.</summary>
	// Token: 0x0200021E RID: 542
	[ValueSerializer(typeof(MouseGestureValueSerializer))]
	[TypeConverter(typeof(MouseGestureConverter))]
	public class MouseGesture : InputGesture
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.MouseGesture" />.</summary>
		// Token: 0x06000E97 RID: 3735 RVA: 0x00037438 File Offset: 0x00036838
		public MouseGesture()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.MouseGesture" /> usando o <see cref="T:System.Windows.Input.MouseAction" /> especificado.</summary>
		/// <param name="mouseAction">A ação associada a esse gesto.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="mouseAction" /> não é um valor <see cref="T:System.Windows.Input.MouseAction" /> válido.</exception>
		// Token: 0x06000E98 RID: 3736 RVA: 0x0003744C File Offset: 0x0003684C
		public MouseGesture(MouseAction mouseAction) : this(mouseAction, ModifierKeys.None)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.MouseGesture" /> usando o <see cref="T:System.Windows.Input.MouseAction" /> e <see cref="T:System.Windows.Input.ModifierKeys" /> especificados.</summary>
		/// <param name="mouseAction">A ação associada a esse gesto.</param>
		/// <param name="modifiers">Os modificadores associados a esse gesto.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="mouseAction" /> não é um valor de <see cref="T:System.Windows.Input.MouseAction" /> válido 
		///
		/// ou - 
		/// <paramref name="modifiers" /> não é um valor <see cref="T:System.Windows.Input.ModifierKeys" /> válido.</exception>
		// Token: 0x06000E99 RID: 3737 RVA: 0x00037464 File Offset: 0x00036864
		public MouseGesture(MouseAction mouseAction, ModifierKeys modifiers)
		{
			if (!MouseGesture.IsDefinedMouseAction(mouseAction))
			{
				throw new InvalidEnumArgumentException("mouseAction", (int)mouseAction, typeof(MouseAction));
			}
			if (!ModifierKeysConverter.IsDefinedModifierKeys(modifiers))
			{
				throw new InvalidEnumArgumentException("modifiers", (int)modifiers, typeof(ModifierKeys));
			}
			this._modifiers = modifiers;
			this._mouseAction = mouseAction;
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Input.MouseAction" /> associado a esse gesto.</summary>
		/// <returns>A ação de mouse associada a esse gesto. O valor padrão é <see cref="F:System.Windows.Input.MouseAction.None" />.</returns>
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x000374C4 File Offset: 0x000368C4
		// (set) Token: 0x06000E9B RID: 3739 RVA: 0x000374D8 File Offset: 0x000368D8
		public MouseAction MouseAction
		{
			get
			{
				return this._mouseAction;
			}
			set
			{
				if (!MouseGesture.IsDefinedMouseAction(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(MouseAction));
				}
				if (this._mouseAction != value)
				{
					this._mouseAction = value;
					this.OnPropertyChanged("MouseAction");
				}
			}
		}

		/// <summary>Obtém ou define as teclas modificadoras associadas a este <see cref="T:System.Windows.Input.MouseGesture" />.</summary>
		/// <returns>As teclas modificadoras associadas a esse gesto. O valor padrão é <see cref="F:System.Windows.Input.ModifierKeys.None" />.</returns>
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x00037520 File Offset: 0x00036920
		// (set) Token: 0x06000E9D RID: 3741 RVA: 0x00037534 File Offset: 0x00036934
		public ModifierKeys Modifiers
		{
			get
			{
				return this._modifiers;
			}
			set
			{
				if (!ModifierKeysConverter.IsDefinedModifierKeys(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ModifierKeys));
				}
				if (this._modifiers != value)
				{
					this._modifiers = value;
					this.OnPropertyChanged("Modifiers");
				}
			}
		}

		/// <summary>Determina se <see cref="T:System.Windows.Input.MouseGesture" /> corresponde à entrada associada ao objeto <see cref="T:System.Windows.Input.InputEventArgs" /> especificado.</summary>
		/// <param name="targetElement">O destino.</param>
		/// <param name="inputEventArgs">Os dados do evento de entrada a comparar a esse gesto.</param>
		/// <returns>
		///   <see langword="true" /> se os dados do evento corresponderem a este <see cref="T:System.Windows.Input.MouseGesture" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000E9E RID: 3742 RVA: 0x0003757C File Offset: 0x0003697C
		public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
		{
			MouseAction mouseAction = MouseGesture.GetMouseAction(inputEventArgs);
			return mouseAction != MouseAction.None && this.MouseAction == mouseAction && this.Modifiers == Keyboard.Modifiers;
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x000375B0 File Offset: 0x000369B0
		internal static bool IsDefinedMouseAction(MouseAction mouseAction)
		{
			return mouseAction >= MouseAction.None && mouseAction <= MouseAction.MiddleDoubleClick;
		}

		// Token: 0x1400014C RID: 332
		// (add) Token: 0x06000EA0 RID: 3744 RVA: 0x000375CC File Offset: 0x000369CC
		// (remove) Token: 0x06000EA1 RID: 3745 RVA: 0x00037604 File Offset: 0x00036A04
		internal event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0003763C File Offset: 0x00036A3C
		internal virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00037664 File Offset: 0x00036A64
		internal static MouseAction GetMouseAction(InputEventArgs inputArgs)
		{
			MouseAction result = MouseAction.None;
			MouseEventArgs mouseEventArgs = inputArgs as MouseEventArgs;
			if (mouseEventArgs != null)
			{
				if (inputArgs is MouseWheelEventArgs)
				{
					result = MouseAction.WheelClick;
				}
				else
				{
					MouseButtonEventArgs mouseButtonEventArgs = inputArgs as MouseButtonEventArgs;
					switch (mouseButtonEventArgs.ChangedButton)
					{
					case MouseButton.Left:
						if (mouseButtonEventArgs.ClickCount == 2)
						{
							result = MouseAction.LeftDoubleClick;
						}
						else if (mouseButtonEventArgs.ClickCount == 1)
						{
							result = MouseAction.LeftClick;
						}
						break;
					case MouseButton.Middle:
						if (mouseButtonEventArgs.ClickCount == 2)
						{
							result = MouseAction.MiddleDoubleClick;
						}
						else if (mouseButtonEventArgs.ClickCount == 1)
						{
							result = MouseAction.MiddleClick;
						}
						break;
					case MouseButton.Right:
						if (mouseButtonEventArgs.ClickCount == 2)
						{
							result = MouseAction.RightDoubleClick;
						}
						else if (mouseButtonEventArgs.ClickCount == 1)
						{
							result = MouseAction.RightClick;
						}
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x04000850 RID: 2128
		private MouseAction _mouseAction;

		// Token: 0x04000851 RID: 2129
		private ModifierKeys _modifiers;
	}
}
