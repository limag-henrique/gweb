using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Threading;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace System.Windows.Input
{
	/// <summary>Fornece recursos para gerenciar idiomas de entrada na WPF (Windows Presentation Foundation).</summary>
	// Token: 0x02000249 RID: 585
	public sealed class InputLanguageManager : DispatcherObject
	{
		/// <summary>Define o valor da propriedade anexada <see cref="P:System.Windows.Input.InputLanguageManager.InputLanguage" /> no objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência no qual a propriedade anexada <see cref="P:System.Windows.Input.InputLanguageManager.InputLanguage" /> é definida.</param>
		/// <param name="inputLanguage">Um objeto <see cref="T:System.Globalization.CultureInfo" /> que representa o novo valor para a propriedade anexada <see cref="P:System.Windows.Input.InputLanguageManager.InputLanguage" />.</param>
		/// <exception cref="T:System.ArgumentNullException">Gerada quando <paramref name="target" /> é <see langword="null" />.</exception>
		// Token: 0x06001032 RID: 4146 RVA: 0x0003D0EC File Offset: 0x0003C4EC
		public static void SetInputLanguage(DependencyObject target, CultureInfo inputLanguage)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			target.SetValue(InputLanguageManager.InputLanguageProperty, inputLanguage);
		}

		/// <summary>Retorna o valor da propriedade <see cref="P:System.Windows.Input.InputLanguageManager.InputLanguage" /> anexada para um objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência para o qual recuperar o idioma de entrada.</param>
		/// <returns>Um objeto <see cref="T:System.Globalization.CultureInfo" /> que representa o idioma de entrada para o objeto de dependência especificado.</returns>
		/// <exception cref="T:System.ArgumentNullException">Gerada quando <paramref name="target" /> é <see langword="null" />.</exception>
		// Token: 0x06001033 RID: 4147 RVA: 0x0003D114 File Offset: 0x0003C514
		[TypeConverter(typeof(CultureInfoIetfLanguageTagConverter))]
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static CultureInfo GetInputLanguage(DependencyObject target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return (CultureInfo)target.GetValue(InputLanguageManager.InputLanguageProperty);
		}

		/// <summary>Define o valor da propriedade de dependência <see cref="P:System.Windows.Input.InputLanguageManager.RestoreInputLanguage" /> no objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência para o qual definir o valor de <see cref="P:System.Windows.Input.InputLanguageManager.RestoreInputLanguage" />.</param>
		/// <param name="restore">Um valor booliano para definir o <see cref="P:System.Windows.Input.InputLanguageManager.RestoreInputLanguage" /> anexado à propriedade.</param>
		/// <exception cref="T:System.ArgumentNullException">Gerada quando <paramref name="target" /> é <see langword="null" />.</exception>
		// Token: 0x06001034 RID: 4148 RVA: 0x0003D140 File Offset: 0x0003C540
		public static void SetRestoreInputLanguage(DependencyObject target, bool restore)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			target.SetValue(InputLanguageManager.RestoreInputLanguageProperty, restore);
		}

		/// <summary>Retorna o valor da propriedade anexada <see cref="P:System.Windows.Input.InputLanguageManager.RestoreInputLanguage" /> para um objeto de dependência especificado.</summary>
		/// <param name="target">O objeto de dependência para o qual recuperar o valor de <see cref="P:System.Windows.Input.InputLanguageManager.RestoreInputLanguage" />.</param>
		/// <returns>O valor atual do <see cref="P:System.Windows.Input.InputLanguageManager.RestoreInputLanguage" /> para o objeto de dependência especificado.</returns>
		/// <exception cref="T:System.ArgumentNullException">Gerada quando <paramref name="target" /> é <see langword="null" />.</exception>
		// Token: 0x06001035 RID: 4149 RVA: 0x0003D168 File Offset: 0x0003C568
		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static bool GetRestoreInputLanguage(DependencyObject target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return (bool)target.GetValue(InputLanguageManager.RestoreInputLanguageProperty);
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0003D194 File Offset: 0x0003C594
		private InputLanguageManager()
		{
			this.RegisterInputLanguageSource(new InputLanguageSource(this));
		}

		/// <summary>Registra uma origem de idioma de entrada com o <see cref="T:System.Windows.Input.InputLanguageManager" />.</summary>
		/// <param name="inputLanguageSource">Um objeto que especifica o idioma de entrada para registrar.  Este objeto deve implementar a interface <see cref="T:System.Windows.Input.IInputLanguageSource" />.</param>
		/// <exception cref="T:System.ArgumentNullException">Gerada quando <paramref name="inputLanguageSource" /> é <see langword="null" />.</exception>
		// Token: 0x06001037 RID: 4151 RVA: 0x0003D1B4 File Offset: 0x0003C5B4
		public void RegisterInputLanguageSource(IInputLanguageSource inputLanguageSource)
		{
			if (inputLanguageSource == null)
			{
				throw new ArgumentNullException("inputLanguageSource");
			}
			this._source = inputLanguageSource;
			if ((this._InputLanguageChanged != null || this._InputLanguageChanging != null) && InputLanguageManager.IsMultipleKeyboardLayout)
			{
				this._source.Initialize();
			}
		}

		/// <summary>Relata a conclusão de uma alteração do idioma de entrada para o <see cref="T:System.Windows.Input.InputLanguageManager" />.</summary>
		/// <param name="newLanguageId">Um objeto <see cref="T:System.Globalization.CultureInfo" /> que representa o novo idioma de entrada.</param>
		/// <param name="previousLanguageId">Um objeto <see cref="T:System.Globalization.CultureInfo" /> que representa o idioma de entrada anterior.</param>
		/// <exception cref="T:System.ArgumentNullException">Gerado quando <paramref name="newLanguageId" /> ou <paramref name="previousLanguageId" /> for <see langword="null" />.</exception>
		// Token: 0x06001038 RID: 4152 RVA: 0x0003D1F8 File Offset: 0x0003C5F8
		public void ReportInputLanguageChanged(CultureInfo newLanguageId, CultureInfo previousLanguageId)
		{
			if (newLanguageId == null)
			{
				throw new ArgumentNullException("newLanguageId");
			}
			if (previousLanguageId == null)
			{
				throw new ArgumentNullException("previousLanguageId");
			}
			if (!previousLanguageId.Equals(this._previousLanguageId))
			{
				this._previousLanguageId = null;
			}
			if (this._InputLanguageChanged != null)
			{
				InputLanguageChangedEventArgs e = new InputLanguageChangedEventArgs(newLanguageId, previousLanguageId);
				this._InputLanguageChanged(this, e);
			}
		}

		/// <summary>Relata o início de uma alteração do idioma de entrada para o <see cref="T:System.Windows.Input.InputLanguageManager" />.</summary>
		/// <param name="newLanguageId">Um objeto <see cref="T:System.Globalization.CultureInfo" /> que representa o novo idioma de entrada.</param>
		/// <param name="previousLanguageId">Um objeto <see cref="T:System.Globalization.CultureInfo" /> que representa o idioma de entrada anterior.</param>
		/// <returns>
		///   <see langword="true" /> para indicar que a alteração relatada do idioma de entrada foi aceita; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">Gerado quando <paramref name="newLanguageId" /> ou <paramref name="previousLanguageId" /> for <see langword="null" />.</exception>
		// Token: 0x06001039 RID: 4153 RVA: 0x0003D254 File Offset: 0x0003C654
		public bool ReportInputLanguageChanging(CultureInfo newLanguageId, CultureInfo previousLanguageId)
		{
			if (newLanguageId == null)
			{
				throw new ArgumentNullException("newLanguageId");
			}
			if (previousLanguageId == null)
			{
				throw new ArgumentNullException("previousLanguageId");
			}
			bool result = true;
			if (this._InputLanguageChanging != null)
			{
				InputLanguageChangingEventArgs inputLanguageChangingEventArgs = new InputLanguageChangingEventArgs(newLanguageId, previousLanguageId);
				this._InputLanguageChanging(this, inputLanguageChangingEventArgs);
				result = !inputLanguageChangingEventArgs.Rejected;
			}
			return result;
		}

		/// <summary>Obtém o Gerenciador de idioma de entrada associado ao contexto atual.</summary>
		/// <returns>Um <see cref="T:System.Windows.Input.InputLanguageManager" /> objeto associado ao contexto atual.  
		/// Esta propriedade não tem valor padrão.</returns>
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x0003D2AC File Offset: 0x0003C6AC
		public static InputLanguageManager Current
		{
			get
			{
				if (InputMethod.Current.InputLanguageManager == null)
				{
					InputMethod.Current.InputLanguageManager = new InputLanguageManager();
				}
				return InputMethod.Current.InputLanguageManager;
			}
		}

		/// <summary>Obtém ou define o idioma de entrada atual.</summary>
		/// <returns>Um objeto <see cref="T:System.Globalization.CultureInfo" /> que representa o idioma de entrada atualmente selecionado. Essa propriedade não pode ser definida como <see langword="null" />. O valor padrão é <see cref="P:System.Globalization.CultureInfo.InvariantCulture" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">Gerado quando é feita uma tentativa de definir essa propriedade como <see langword="null" />.</exception>
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x0003D2E0 File Offset: 0x0003C6E0
		// (set) Token: 0x0600103C RID: 4156 RVA: 0x0003D328 File Offset: 0x0003C728
		[TypeConverter(typeof(CultureInfoIetfLanguageTagConverter))]
		public CultureInfo CurrentInputLanguage
		{
			get
			{
				if (this._source != null)
				{
					return this._source.CurrentInputLanguage;
				}
				IntPtr keyboardLayout = SafeNativeMethods.GetKeyboardLayout(0);
				if (keyboardLayout == IntPtr.Zero)
				{
					return CultureInfo.InvariantCulture;
				}
				return new CultureInfo((int)((short)NativeMethods.IntPtrToInt32(keyboardLayout)));
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.SetSourceCurrentLanguageId(value);
			}
		}

		/// <summary>Obtém um enumerador para idiomas de entrada disponíveis no momento.</summary>
		/// <returns>Um enumerador para idiomas de entrada disponíveis no momento, ou <see langword="null" /> se nenhum idiomas de entrada estão disponíveis. Esta propriedade não tem valor padrão.</returns>
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x0003D34C File Offset: 0x0003C74C
		public IEnumerable AvailableInputLanguages
		{
			get
			{
				if (this._source == null)
				{
					return null;
				}
				return this._source.InputLanguageList;
			}
		}

		/// <summary>Ocorre quando uma alteração do idioma de entrada é concluída.</summary>
		// Token: 0x1400014F RID: 335
		// (add) Token: 0x0600103E RID: 4158 RVA: 0x0003D370 File Offset: 0x0003C770
		// (remove) Token: 0x0600103F RID: 4159 RVA: 0x0003D3BC File Offset: 0x0003C7BC
		public event InputLanguageEventHandler InputLanguageChanged
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this._InputLanguageChanged == null && this._InputLanguageChanging == null && InputLanguageManager.IsMultipleKeyboardLayout && this._source != null)
				{
					this._source.Initialize();
				}
				this._InputLanguageChanged += value;
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._InputLanguageChanged -= value;
				if (this._InputLanguageChanged == null && this._InputLanguageChanging == null && InputLanguageManager.IsMultipleKeyboardLayout && this._source != null)
				{
					this._source.Uninitialize();
				}
			}
		}

		/// <summary>Ocorre quando uma alteração do idioma de entrada é iniciada.</summary>
		// Token: 0x14000150 RID: 336
		// (add) Token: 0x06001040 RID: 4160 RVA: 0x0003D408 File Offset: 0x0003C808
		// (remove) Token: 0x06001041 RID: 4161 RVA: 0x0003D454 File Offset: 0x0003C854
		public event InputLanguageEventHandler InputLanguageChanging
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this._InputLanguageChanged == null && this._InputLanguageChanging == null && InputLanguageManager.IsMultipleKeyboardLayout && this._source != null)
				{
					this._source.Initialize();
				}
				this._InputLanguageChanging += value;
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._InputLanguageChanging -= value;
				if (this._InputLanguageChanged == null && this._InputLanguageChanging == null && InputLanguageManager.IsMultipleKeyboardLayout && this._source != null)
				{
					this._source.Uninitialize();
				}
			}
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0003D4A0 File Offset: 0x0003C8A0
		internal void Focus(DependencyObject focus, DependencyObject focused)
		{
			CultureInfo cultureInfo = null;
			if (focus != null)
			{
				cultureInfo = (CultureInfo)focus.GetValue(InputLanguageManager.InputLanguageProperty);
			}
			if (cultureInfo == null || cultureInfo.Equals(CultureInfo.InvariantCulture))
			{
				if (focused != null)
				{
					if (this._previousLanguageId != null && (bool)focused.GetValue(InputLanguageManager.RestoreInputLanguageProperty))
					{
						this.SetSourceCurrentLanguageId(this._previousLanguageId);
					}
					this._previousLanguageId = null;
					return;
				}
			}
			else
			{
				CultureInfo currentInputLanguage = this._source.CurrentInputLanguage;
				this.SetSourceCurrentLanguageId(cultureInfo);
				this._previousLanguageId = currentInputLanguage;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x0003D520 File Offset: 0x0003C920
		internal IInputLanguageSource Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x0003D534 File Offset: 0x0003C934
		internal static bool IsMultipleKeyboardLayout
		{
			get
			{
				int keyboardLayoutList = SafeNativeMethods.GetKeyboardLayoutList(0, null);
				return keyboardLayoutList > 1;
			}
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0003D550 File Offset: 0x0003C950
		private void SetSourceCurrentLanguageId(CultureInfo languageId)
		{
			if (this._source == null)
			{
				throw new InvalidOperationException(SR.Get("InputLanguageManager_NotReadyToChangeCurrentLanguage"));
			}
			this._source.CurrentInputLanguage = languageId;
		}

		// Token: 0x14000151 RID: 337
		// (add) Token: 0x06001046 RID: 4166 RVA: 0x0003D584 File Offset: 0x0003C984
		// (remove) Token: 0x06001047 RID: 4167 RVA: 0x0003D5BC File Offset: 0x0003C9BC
		private event InputLanguageEventHandler _InputLanguageChanged;

		// Token: 0x14000152 RID: 338
		// (add) Token: 0x06001048 RID: 4168 RVA: 0x0003D5F4 File Offset: 0x0003C9F4
		// (remove) Token: 0x06001049 RID: 4169 RVA: 0x0003D62C File Offset: 0x0003CA2C
		private event InputLanguageEventHandler _InputLanguageChanging;

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Input.InputLanguageManager.InputLanguage" /> anexada.</summary>
		// Token: 0x040008BE RID: 2238
		public static readonly DependencyProperty InputLanguageProperty = DependencyProperty.RegisterAttached("InputLanguage", typeof(CultureInfo), typeof(InputLanguageManager), new PropertyMetadata(CultureInfo.InvariantCulture));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Input.InputLanguageManager.RestoreInputLanguage" /> anexada.</summary>
		// Token: 0x040008BF RID: 2239
		public static readonly DependencyProperty RestoreInputLanguageProperty = DependencyProperty.RegisterAttached("RestoreInputLanguage", typeof(bool), typeof(InputLanguageManager), new PropertyMetadata(false));

		// Token: 0x040008C2 RID: 2242
		private CultureInfo _previousLanguageId;

		// Token: 0x040008C3 RID: 2243
		private IInputLanguageSource _source;
	}
}
