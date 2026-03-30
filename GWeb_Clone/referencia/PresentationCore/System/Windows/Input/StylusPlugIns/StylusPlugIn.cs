using System;

namespace System.Windows.Input.StylusPlugIns
{
	/// <summary>Representa um plug-in que pode ser adicionado à propriedade <see cref="P:System.Windows.UIElement.StylusPlugIns" /> de um controle.</summary>
	// Token: 0x020002FA RID: 762
	public abstract class StylusPlugIn
	{
		// Token: 0x0600183E RID: 6206 RVA: 0x0006164C File Offset: 0x00060A4C
		internal void Added(StylusPlugInCollection plugInCollection)
		{
			this._pic = plugInCollection;
			this.OnAdded();
			this.InvalidateIsActiveForInput();
		}

		/// <summary>Ocorre quando o <see cref="T:System.Windows.Input.StylusPlugIns.StylusPlugIn" /> é adicionado a um elemento.</summary>
		// Token: 0x0600183F RID: 6207 RVA: 0x0006166C File Offset: 0x00060A6C
		protected virtual void OnAdded()
		{
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x0006167C File Offset: 0x00060A7C
		internal void Removed()
		{
			if (this._activeForInput)
			{
				this.InvalidateIsActiveForInput();
			}
			this.OnRemoved();
			this._pic = null;
		}

		/// <summary>Ocorre quando o <see cref="T:System.Windows.Input.StylusPlugIns.StylusPlugIn" /> é removido de um elemento.</summary>
		// Token: 0x06001841 RID: 6209 RVA: 0x000616A4 File Offset: 0x00060AA4
		protected virtual void OnRemoved()
		{
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x000616B4 File Offset: 0x00060AB4
		internal void StylusEnterLeave(bool isEnter, RawStylusInput rawStylusInput, bool confirmed)
		{
			if (this.__enabled && this._pic != null)
			{
				if (isEnter)
				{
					this.OnStylusEnter(rawStylusInput, confirmed);
					return;
				}
				this.OnStylusLeave(rawStylusInput, confirmed);
			}
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x000616E8 File Offset: 0x00060AE8
		internal void RawStylusInput(RawStylusInput rawStylusInput)
		{
			if (this.__enabled && this._pic != null)
			{
				RawStylusActions actions = rawStylusInput.Report.Actions;
				if (actions == RawStylusActions.Down)
				{
					this.OnStylusDown(rawStylusInput);
					return;
				}
				if (actions != RawStylusActions.Up)
				{
					if (actions != RawStylusActions.Move)
					{
						return;
					}
					this.OnStylusMove(rawStylusInput);
					return;
				}
				else
				{
					this.OnStylusUp(rawStylusInput);
				}
			}
		}

		/// <summary>Ocorre em um thread de caneta quando o cursor do mouse entra nos limites de um elemento.</summary>
		/// <param name="rawStylusInput">Um <see cref="T:System.Windows.Input.StylusPlugIns.RawStylusInput" /> que contém informações sobre a entrada da caneta.</param>
		/// <param name="confirmed">
		///   <see langword="true" /> se a caneta de fato adentrou os limites do elemento; caso contrário, <see langword="false" />.</param>
		// Token: 0x06001844 RID: 6212 RVA: 0x00061738 File Offset: 0x00060B38
		protected virtual void OnStylusEnter(RawStylusInput rawStylusInput, bool confirmed)
		{
		}

		/// <summary>Ocorre em um thread de caneta quando o cursor do mouse deixa os limites de um elemento.</summary>
		/// <param name="rawStylusInput">Um <see cref="T:System.Windows.Input.StylusPlugIns.RawStylusInput" /> que contém informações sobre a entrada da caneta.</param>
		/// <param name="confirmed">
		///   <see langword="true" /> se a caneta de fato deixou os limites do elemento; caso contrário, <see langword="false" />.</param>
		// Token: 0x06001845 RID: 6213 RVA: 0x00061748 File Offset: 0x00060B48
		protected virtual void OnStylusLeave(RawStylusInput rawStylusInput, bool confirmed)
		{
		}

		/// <summary>Ocorre em um thread no pool de threads de caneta quando a caneta eletrônica toca o digitalizador.</summary>
		/// <param name="rawStylusInput">Um <see cref="T:System.Windows.Input.StylusPlugIns.RawStylusInput" /> que contém informações sobre a entrada da caneta.</param>
		// Token: 0x06001846 RID: 6214 RVA: 0x00061758 File Offset: 0x00060B58
		protected virtual void OnStylusDown(RawStylusInput rawStylusInput)
		{
		}

		/// <summary>Ocorre em um thread de caneta quando a caneta eletrônica se move sobre o digitalizador.</summary>
		/// <param name="rawStylusInput">Um <see cref="T:System.Windows.Input.StylusPlugIns.RawStylusInput" /> que contém informações sobre a entrada da caneta.</param>
		// Token: 0x06001847 RID: 6215 RVA: 0x00061768 File Offset: 0x00060B68
		protected virtual void OnStylusMove(RawStylusInput rawStylusInput)
		{
		}

		/// <summary>Ocorre em um thread de caneta quando o usuário erguer a caneta eletrônica do digitalizador.</summary>
		/// <param name="rawStylusInput">Um <see cref="T:System.Windows.Input.StylusPlugIns.RawStylusInput" /> que contém informações sobre a entrada da caneta.</param>
		// Token: 0x06001848 RID: 6216 RVA: 0x00061778 File Offset: 0x00060B78
		protected virtual void OnStylusUp(RawStylusInput rawStylusInput)
		{
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x00061788 File Offset: 0x00060B88
		internal void FireCustomData(object callbackData, RawStylusActions action, bool targetVerified)
		{
			if (this.__enabled && this._pic != null)
			{
				if (action == RawStylusActions.Down)
				{
					this.OnStylusDownProcessed(callbackData, targetVerified);
					return;
				}
				if (action != RawStylusActions.Up)
				{
					if (action != RawStylusActions.Move)
					{
						return;
					}
					this.OnStylusMoveProcessed(callbackData, targetVerified);
					return;
				}
				else
				{
					this.OnStylusUpProcessed(callbackData, targetVerified);
				}
			}
		}

		/// <summary>Ocorre em um thread de interface do usuário do aplicativo quando a caneta eletrônica toca o digitalizador.</summary>
		/// <param name="callbackData">O objeto que o aplicativo passou para o método <see cref="M:System.Windows.Input.StylusPlugIns.RawStylusInput.NotifyWhenProcessed(System.Object)" />.</param>
		/// <param name="targetVerified">
		///   <see langword="true" /> se a entrada da caneta foi encaminhada corretamente para o <see cref="T:System.Windows.Input.StylusPlugIns.StylusPlugIn" />; caso contrário, <see langword="false" />.</param>
		// Token: 0x0600184A RID: 6218 RVA: 0x000617D0 File Offset: 0x00060BD0
		protected virtual void OnStylusDownProcessed(object callbackData, bool targetVerified)
		{
		}

		/// <summary>Ocorre em um thread de interface do usuário do aplicativo quando a caneta eletrônica se move sobre o digitalizador.</summary>
		/// <param name="callbackData">O objeto que o aplicativo passou para o método <see cref="M:System.Windows.Input.StylusPlugIns.RawStylusInput.NotifyWhenProcessed(System.Object)" />.</param>
		/// <param name="targetVerified">
		///   <see langword="true" /> se a entrada da caneta foi encaminhada corretamente para o <see cref="T:System.Windows.Input.StylusPlugIns.StylusPlugIn" />; caso contrário, <see langword="false" />.</param>
		// Token: 0x0600184B RID: 6219 RVA: 0x000617E0 File Offset: 0x00060BE0
		protected virtual void OnStylusMoveProcessed(object callbackData, bool targetVerified)
		{
		}

		/// <summary>Ocorre em um thread de interface do usuário do aplicativo quando o usuário levanta a caneta eletrônica do digitalizador.</summary>
		/// <param name="callbackData">O objeto que o aplicativo passou para o método <see cref="M:System.Windows.Input.StylusPlugIns.RawStylusInput.NotifyWhenProcessed(System.Object)" />.</param>
		/// <param name="targetVerified">
		///   <see langword="true" /> se a entrada da caneta foi encaminhada corretamente para o <see cref="T:System.Windows.Input.StylusPlugIns.StylusPlugIn" />; caso contrário, <see langword="false" />.</param>
		// Token: 0x0600184C RID: 6220 RVA: 0x000617F0 File Offset: 0x00060BF0
		protected virtual void OnStylusUpProcessed(object callbackData, bool targetVerified)
		{
		}

		/// <summary>Obtém o <see cref="T:System.Windows.UIElement" /> ao qual o <see cref="T:System.Windows.Input.StylusPlugIns.StylusPlugIn" /> está anexo.</summary>
		/// <returns>O <see cref="T:System.Windows.UIElement" /> ao qual o <see cref="T:System.Windows.Input.StylusPlugIns.StylusPlugIn" /> está anexado.</returns>
		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x00061800 File Offset: 0x00060C00
		public UIElement Element
		{
			get
			{
				if (this._pic == null)
				{
					return null;
				}
				return this._pic.Element;
			}
		}

		/// <summary>Obtém os limites de cache do elemento.</summary>
		/// <returns>Os limites de cache do elemento.</returns>
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x00061824 File Offset: 0x00060C24
		public Rect ElementBounds
		{
			get
			{
				if (this._pic == null)
				{
					return default(Rect);
				}
				return this._pic.Rect;
			}
		}

		/// <summary>Obtém ou define se o <see cref="T:System.Windows.Input.StylusPlugIns.StylusPlugIn" /> está ativo.</summary>
		/// <returns>
		///   <see langword="true" /> Se o <see cref="T:System.Windows.Input.StylusPlugIns.StylusPlugIn" /> estiver ativa; caso contrário, <see langword="false" />. O padrão é <see langword="true" />.</returns>
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x00061850 File Offset: 0x00060C50
		// (set) Token: 0x06001850 RID: 6224 RVA: 0x00061868 File Offset: 0x00060C68
		public bool Enabled
		{
			get
			{
				return this.__enabled;
			}
			set
			{
				if (this._pic != null)
				{
					this._pic.Element.VerifyAccess();
				}
				if (value != this.__enabled)
				{
					if (this._pic != null && this._pic.IsActiveForInput)
					{
						using (this._pic.Element.Dispatcher.DisableProcessing())
						{
							this._pic.ExecuteWithPotentialLock(delegate
							{
								this.__enabled = value;
								if (!value)
								{
									this.InvalidateIsActiveForInput();
									this.OnEnabledChanged();
									return;
								}
								this.OnEnabledChanged();
								this.InvalidateIsActiveForInput();
							});
							return;
						}
					}
					this.__enabled = value;
					if (!value)
					{
						this.InvalidateIsActiveForInput();
						this.OnEnabledChanged();
						return;
					}
					this.OnEnabledChanged();
					this.InvalidateIsActiveForInput();
				}
			}
		}

		/// <summary>Ocorre quando a propriedade <see cref="P:System.Windows.Input.StylusPlugIns.StylusPlugIn.Enabled" /> muda.</summary>
		// Token: 0x06001851 RID: 6225 RVA: 0x00061954 File Offset: 0x00060D54
		protected virtual void OnEnabledChanged()
		{
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x00061964 File Offset: 0x00060D64
		internal void InvalidateIsActiveForInput()
		{
			bool flag = this._pic != null && (this.Enabled && this._pic.Contains(this)) && this._pic.IsActiveForInput;
			if (flag != this._activeForInput)
			{
				this._activeForInput = flag;
				this.OnIsActiveForInputChanged();
			}
		}

		/// <summary>Obtém se o <see cref="T:System.Windows.Input.StylusPlugIns.StylusPlugIn" /> pode aceitar a entrada.</summary>
		/// <returns>
		///   <see langword="true" /> Se o <see cref="T:System.Windows.Input.StylusPlugIns.StylusPlugIn" /> é capaz de aceitar a entrada; caso contrário <see langword="false" />.</returns>
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x000619B8 File Offset: 0x00060DB8
		public bool IsActiveForInput
		{
			get
			{
				return this._activeForInput;
			}
		}

		/// <summary>Ocorre quando a propriedade <see cref="P:System.Windows.Input.StylusPlugIns.StylusPlugIn.IsActiveForInput" /> muda.</summary>
		// Token: 0x06001854 RID: 6228 RVA: 0x000619CC File Offset: 0x00060DCC
		protected virtual void OnIsActiveForInputChanged()
		{
		}

		// Token: 0x04000D32 RID: 3378
		private volatile bool __enabled = true;

		// Token: 0x04000D33 RID: 3379
		private bool _activeForInput;

		// Token: 0x04000D34 RID: 3380
		private StylusPlugInCollection _pic;
	}
}
