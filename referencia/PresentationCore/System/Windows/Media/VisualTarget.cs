using System;
using System.Security;
using System.Windows.Media.Composition;

namespace System.Windows.Media
{
	/// <summary>Fornece a funcionalidade para conectar uma árvore visual a outra árvore visual entre limites de thread.</summary>
	// Token: 0x0200044F RID: 1103
	public class VisualTarget : CompositionTarget
	{
		/// <summary>Construtor que cria um novo <see cref="T:System.Windows.Media.VisualTarget" />.</summary>
		/// <param name="hostVisual">Um valor do tipo <see cref="T:System.Windows.Media.HostVisual" />.</param>
		// Token: 0x06002DC0 RID: 11712 RVA: 0x000B6EDC File Offset: 0x000B62DC
		public VisualTarget(HostVisual hostVisual)
		{
			if (hostVisual == null)
			{
				throw new ArgumentNullException("hostVisual");
			}
			this._hostVisual = hostVisual;
			this._connected = false;
			MediaContext.RegisterICompositionTarget(base.Dispatcher, this);
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x000B6F18 File Offset: 0x000B6318
		private void BeginHosting()
		{
			try
			{
				this._hostVisual.BeginHosting(this);
				this._connected = true;
			}
			catch
			{
				MediaContext.UnregisterICompositionTarget(base.Dispatcher, this);
				throw;
			}
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x000B6F68 File Offset: 0x000B6368
		internal override void CreateUCEResources(DUCE.Channel channel, DUCE.Channel outOfBandChannel)
		{
			this._outOfBandChannel = outOfBandChannel;
			base.CreateUCEResources(channel, outOfBandChannel);
			base.StateChangedCallback(new object[]
			{
				CompositionTarget.HostStateFlags.None
			});
			bool flag = this._contentRoot.CreateOrAddRefOnChannel(this, outOfBandChannel, DUCE.ResourceType.TYPE_VISUAL);
			this._contentRoot.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_VISUAL);
			this.BeginHosting();
		}

		/// <summary>Retorna uma matriz que pode ser usada para transformar as coordenadas do <see cref="T:System.Windows.Media.VisualTarget" /> no dispositivo de destino de renderização.</summary>
		/// <returns>Obtém um valor do tipo <see cref="T:System.Windows.Media.Matrix" />.</returns>
		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06002DC3 RID: 11715 RVA: 0x000B6FC0 File Offset: 0x000B63C0
		public override Matrix TransformToDevice
		{
			get
			{
				base.VerifyAPIReadOnly();
				Matrix worldTransform = base.WorldTransform;
				worldTransform.Invert();
				return worldTransform;
			}
		}

		/// <summary>Retorna uma matriz que pode ser usada para transformar as coordenadas do dispositivo de destino de renderização no <see cref="T:System.Windows.Media.VisualTarget" />.</summary>
		/// <returns>Obtém um valor do tipo <see cref="T:System.Windows.Media.Matrix" />.</returns>
		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06002DC4 RID: 11716 RVA: 0x000B6FE4 File Offset: 0x000B63E4
		public override Matrix TransformFromDevice
		{
			get
			{
				base.VerifyAPIReadOnly();
				return base.WorldTransform;
			}
		}

		/// <summary>Limpa o estado associado ao <see cref="T:System.Windows.Interop.HwndTarget" />.</summary>
		// Token: 0x06002DC5 RID: 11717 RVA: 0x000B7000 File Offset: 0x000B6400
		[SecurityCritical]
		public override void Dispose()
		{
			try
			{
				base.VerifyAccess();
				if (!base.IsDisposed && this._hostVisual != null && this._connected)
				{
					this.RootVisual = null;
					MediaContext.UnregisterICompositionTarget(base.Dispatcher, this);
				}
			}
			finally
			{
				base.Dispose();
			}
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x000B7064 File Offset: 0x000B6464
		private void EndHosting()
		{
			this._hostVisual.EndHosting();
			this._connected = false;
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000B7084 File Offset: 0x000B6484
		internal override void ReleaseUCEResources(DUCE.Channel channel, DUCE.Channel outOfBandChannel)
		{
			this.EndHosting();
			this._contentRoot.ReleaseOnChannel(channel);
			if (this._contentRoot.IsOnChannel(outOfBandChannel))
			{
				this._contentRoot.ReleaseOnChannel(outOfBandChannel);
			}
			base.ReleaseUCEResources(channel, outOfBandChannel);
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x000B70C8 File Offset: 0x000B64C8
		internal DUCE.Channel OutOfBandChannel
		{
			get
			{
				return this._outOfBandChannel;
			}
		}

		// Token: 0x040014C9 RID: 5321
		private DUCE.Channel _outOfBandChannel;

		// Token: 0x040014CA RID: 5322
		private HostVisual _hostVisual;

		// Token: 0x040014CB RID: 5323
		private bool _connected;
	}
}
