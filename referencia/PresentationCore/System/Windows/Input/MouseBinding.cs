using System;
using System.ComponentModel;
using System.Windows.Markup;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Associa um <see cref="T:System.Windows.Input.MouseGesture" /> a um <see cref="T:System.Windows.Input.RoutedCommand" /> (ou a outra implementação de <see cref="T:System.Windows.Input.ICommand" />).</summary>
	// Token: 0x0200021D RID: 541
	public class MouseBinding : InputBinding
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.MouseBinding" />.</summary>
		// Token: 0x06000E85 RID: 3717 RVA: 0x000370E8 File Offset: 0x000364E8
		public MouseBinding()
		{
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x000370FC File Offset: 0x000364FC
		internal MouseBinding(ICommand command, MouseAction mouseAction) : this(command, new MouseGesture(mouseAction))
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.MouseBinding" /> usando o comando e o gesto de mouse especificados.</summary>
		/// <param name="command">O comando associado ao gesto.</param>
		/// <param name="gesture">O gesto associado ao comando.</param>
		// Token: 0x06000E87 RID: 3719 RVA: 0x00037118 File Offset: 0x00036518
		public MouseBinding(ICommand command, MouseGesture gesture) : base(command, gesture)
		{
			this.SynchronizePropertiesFromGesture(gesture);
			gesture.PropertyChanged += this.OnMouseGesturePropertyChanged;
		}

		/// <summary>Obtém ou define o gesto associado a essa <see cref="T:System.Windows.Input.MouseBinding" />.</summary>
		/// <returns>O gesto.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Windows.Input.MouseBinding.Gesture" /> é definido como <see langword="null" />.</exception>
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x00037148 File Offset: 0x00036548
		// (set) Token: 0x06000E89 RID: 3721 RVA: 0x00037160 File Offset: 0x00036560
		[TypeConverter(typeof(MouseGestureConverter))]
		[ValueSerializer(typeof(MouseGestureValueSerializer))]
		public override InputGesture Gesture
		{
			get
			{
				return base.Gesture as MouseGesture;
			}
			set
			{
				MouseGesture mouseGesture = this.Gesture as MouseGesture;
				MouseGesture mouseGesture2 = value as MouseGesture;
				if (mouseGesture2 == null)
				{
					throw new ArgumentException(SR.Get("InputBinding_ExpectedInputGesture", new object[]
					{
						typeof(MouseGesture)
					}));
				}
				base.Gesture = mouseGesture2;
				this.SynchronizePropertiesFromGesture(mouseGesture2);
				if (mouseGesture != mouseGesture2)
				{
					if (mouseGesture != null)
					{
						mouseGesture.PropertyChanged -= this.OnMouseGesturePropertyChanged;
					}
					mouseGesture2.PropertyChanged += this.OnMouseGesturePropertyChanged;
					return;
				}
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Input.MouseAction" /> associado a esse <see cref="T:System.Windows.Input.MouseBinding" />.</summary>
		/// <returns>A ação do mouse.  O padrão é <see cref="F:System.Windows.Input.MouseAction.None" />.</returns>
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x000371E0 File Offset: 0x000365E0
		// (set) Token: 0x06000E8B RID: 3723 RVA: 0x00037200 File Offset: 0x00036600
		public MouseAction MouseAction
		{
			get
			{
				return (MouseAction)base.GetValue(MouseBinding.MouseActionProperty);
			}
			set
			{
				base.SetValue(MouseBinding.MouseActionProperty, value);
			}
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00037220 File Offset: 0x00036620
		private static void OnMouseActionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			MouseBinding mouseBinding = (MouseBinding)d;
			mouseBinding.SynchronizeGestureFromProperties((MouseAction)e.NewValue);
		}

		/// <summary>Cria uma instância de um <see cref="T:System.Windows.Input.MouseBinding" />.</summary>
		/// <returns>O novo objeto.</returns>
		// Token: 0x06000E8D RID: 3725 RVA: 0x00037248 File Offset: 0x00036648
		protected override Freezable CreateInstanceCore()
		{
			return new MouseBinding();
		}

		/// <summary>Copia os valores base (não animados) das propriedades do objeto especificado.</summary>
		/// <param name="sourceFreezable">O objeto a ser clonado.</param>
		// Token: 0x06000E8E RID: 3726 RVA: 0x0003725C File Offset: 0x0003665C
		protected override void CloneCore(Freezable sourceFreezable)
		{
			base.CloneCore(sourceFreezable);
			this.CloneGesture();
		}

		/// <summary>Copia os valores atuais das propriedades do objeto especificado.</summary>
		/// <param name="sourceFreezable">O objeto a ser clonado.</param>
		// Token: 0x06000E8F RID: 3727 RVA: 0x00037278 File Offset: 0x00036678
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			base.CloneCurrentValueCore(sourceFreezable);
			this.CloneGesture();
		}

		/// <summary>Cria a instância um clone congelado do <see cref="T:System.Windows.Freezable" /> especificado usando valores de propriedade base (não animados).</summary>
		/// <param name="sourceFreezable">O objeto a ser clonado.</param>
		// Token: 0x06000E90 RID: 3728 RVA: 0x00037294 File Offset: 0x00036694
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			base.GetAsFrozenCore(sourceFreezable);
			this.CloneGesture();
		}

		/// <summary>Cria a instância atual de um clone congelado do <see cref="T:System.Windows.Freezable" /> especificado. Se o objeto tiver propriedades de dependência animadas, seus valores animados atuais serão copiados.</summary>
		/// <param name="sourceFreezable">O objeto a ser clonado.</param>
		// Token: 0x06000E91 RID: 3729 RVA: 0x000372B0 File Offset: 0x000366B0
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			this.CloneGesture();
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x000372CC File Offset: 0x000366CC
		private void SynchronizePropertiesFromGesture(MouseGesture mouseGesture)
		{
			if (!this._settingGesture)
			{
				this._settingGesture = true;
				try
				{
					this.MouseAction = mouseGesture.MouseAction;
				}
				finally
				{
					this._settingGesture = false;
				}
			}
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0003731C File Offset: 0x0003671C
		private void SynchronizeGestureFromProperties(MouseAction mouseAction)
		{
			if (!this._settingGesture)
			{
				this._settingGesture = true;
				try
				{
					if (this.Gesture == null)
					{
						this.Gesture = new MouseGesture(mouseAction);
					}
					else
					{
						((MouseGesture)this.Gesture).MouseAction = mouseAction;
					}
				}
				finally
				{
					this._settingGesture = false;
				}
			}
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x00037388 File Offset: 0x00036788
		private void OnMouseGesturePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (string.Compare(e.PropertyName, "MouseAction", StringComparison.Ordinal) == 0)
			{
				MouseGesture mouseGesture = this.Gesture as MouseGesture;
				if (mouseGesture != null)
				{
					this.SynchronizePropertiesFromGesture(mouseGesture);
				}
			}
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x000373C0 File Offset: 0x000367C0
		private void CloneGesture()
		{
			MouseGesture mouseGesture = this.Gesture as MouseGesture;
			if (mouseGesture != null)
			{
				mouseGesture.PropertyChanged += this.OnMouseGesturePropertyChanged;
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Input.MouseBinding.MouseAction" />.</summary>
		// Token: 0x0400084D RID: 2125
		public static readonly DependencyProperty MouseActionProperty = DependencyProperty.Register("MouseAction", typeof(MouseAction), typeof(MouseBinding), new UIPropertyMetadata(MouseAction.None, new PropertyChangedCallback(MouseBinding.OnMouseActionPropertyChanged)));

		// Token: 0x0400084E RID: 2126
		private bool _settingGesture;
	}
}
