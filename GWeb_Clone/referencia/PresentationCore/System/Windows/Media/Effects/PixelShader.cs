using System;
using System.IO;
using System.IO.Packaging;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Navigation;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Effects
{
	/// <summary>Fornece um wrapper gerenciado em um sombreador de pixel HLSL (High Level Shading Language).</summary>
	// Token: 0x02000614 RID: 1556
	public sealed class PixelShader : Animatable, DUCE.IResource
	{
		/// <summary>Atribui o <see cref="T:System.IO.Stream" /> a ser usado como a origem do código de bytes HLSL.</summary>
		/// <param name="source">O fluxo do qual ler o código de bytes HLSL.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> é <see langword="null" />.</exception>
		// Token: 0x06004799 RID: 18329 RVA: 0x00118600 File Offset: 0x00117A00
		public void SetStreamSource(Stream source)
		{
			base.WritePreamble();
			this.LoadPixelShaderFromStreamIntoMemory(source);
			base.WritePostscript();
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x0600479A RID: 18330 RVA: 0x00118620 File Offset: 0x00117A20
		// (set) Token: 0x0600479B RID: 18331 RVA: 0x00118634 File Offset: 0x00117A34
		internal short ShaderMajorVersion { get; private set; }

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x0600479C RID: 18332 RVA: 0x00118648 File Offset: 0x00117A48
		// (set) Token: 0x0600479D RID: 18333 RVA: 0x0011865C File Offset: 0x00117A5C
		internal short ShaderMinorVersion { get; private set; }

		/// <summary>Ocorre quando o thread de renderização não pode processar o sombreador de pixel.</summary>
		// Token: 0x140001D2 RID: 466
		// (add) Token: 0x0600479E RID: 18334 RVA: 0x00118670 File Offset: 0x00117A70
		// (remove) Token: 0x0600479F RID: 18335 RVA: 0x0011868C File Offset: 0x00117A8C
		public static event EventHandler InvalidPixelShaderEncountered
		{
			add
			{
				MediaContext currentMediaContext = MediaContext.CurrentMediaContext;
				currentMediaContext.InvalidPixelShaderEncountered += value;
			}
			remove
			{
				MediaContext currentMediaContext = MediaContext.CurrentMediaContext;
				currentMediaContext.InvalidPixelShaderEncountered -= value;
			}
		}

		// Token: 0x060047A0 RID: 18336 RVA: 0x001186A8 File Offset: 0x00117AA8
		private void UriSourcePropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			Uri uri = (Uri)e.NewValue;
			Stream stream = null;
			try
			{
				if (uri != null)
				{
					if (!uri.IsAbsoluteUri)
					{
						uri = BaseUriHelper.GetResolvedUri(BaseUriHelper.BaseUri, uri);
					}
					if (!uri.IsFile && !PackUriHelper.IsPackUri(uri))
					{
						throw new ArgumentException(SR.Get("Effect_SourceUriMustBeFileOrPack"));
					}
					stream = WpfWebRequestHelper.CreateRequestAndGetResponseStream(uri);
				}
				this.LoadPixelShaderFromStreamIntoMemory(stream);
			}
			finally
			{
				if (stream != null)
				{
					stream.Dispose();
				}
			}
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x00118738 File Offset: 0x00117B38
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void LoadPixelShaderFromStreamIntoMemory(Stream source)
		{
			SecurityHelper.DemandUIWindowPermission();
			this._shaderBytecode = new SecurityCriticalData<byte[]>(null);
			if (source != null)
			{
				if (!source.CanSeek)
				{
					throw new InvalidOperationException(SR.Get("Effect_ShaderSeekableStream"));
				}
				int num = (int)source.Length;
				if (num % 4 != 0)
				{
					throw new InvalidOperationException(SR.Get("Effect_ShaderBytecodeSize"));
				}
				BinaryReader binaryReader = new BinaryReader(source);
				this._shaderBytecode = new SecurityCriticalData<byte[]>(new byte[num]);
				int num2 = binaryReader.Read(this._shaderBytecode.Value, 0, num);
				if (this._shaderBytecode.Value != null && this._shaderBytecode.Value.Length > 3)
				{
					this.ShaderMajorVersion = (short)this._shaderBytecode.Value[1];
					this.ShaderMinorVersion = (short)this._shaderBytecode.Value[0];
				}
				else
				{
					this.ShaderMajorVersion = (this.ShaderMinorVersion = 0);
				}
			}
			base.RegisterForAsyncUpdateResource();
			if (this._shaderBytecodeChanged != null)
			{
				this._shaderBytecodeChanged(this, null);
			}
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x00118830 File Offset: 0x00117C30
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private unsafe void ManualUpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			checked
			{
				if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
				{
					DUCE.MILCMD_PIXELSHADER milcmd_PIXELSHADER;
					milcmd_PIXELSHADER.Type = MILCMD.MilCmdPixelShader;
					milcmd_PIXELSHADER.Handle = this._duceResource.GetHandle(channel);
					milcmd_PIXELSHADER.PixelShaderBytecodeSize = ((this._shaderBytecode.Value == null) ? 0U : ((uint)this._shaderBytecode.Value.Length));
					milcmd_PIXELSHADER.ShaderRenderMode = this.ShaderRenderMode;
					milcmd_PIXELSHADER.CompileSoftwareShader = CompositionResourceManager.BooleanToUInt32(this.ShaderMajorVersion != 3 || this.ShaderMinorVersion != 0);
					channel.BeginCommand(unchecked((byte*)(&milcmd_PIXELSHADER)), sizeof(DUCE.MILCMD_PIXELSHADER), (int)milcmd_PIXELSHADER.PixelShaderBytecodeSize);
					if (milcmd_PIXELSHADER.PixelShaderBytecodeSize > 0U)
					{
						byte[] array;
						byte* pbCommandData;
						if ((array = this._shaderBytecode.Value) == null || array.Length == 0)
						{
							pbCommandData = null;
						}
						else
						{
							pbCommandData = &array[0];
						}
						channel.AppendCommandData(pbCommandData, (int)milcmd_PIXELSHADER.PixelShaderBytecodeSize);
						array = null;
					}
					channel.EndCommand();
				}
			}
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x00118918 File Offset: 0x00117D18
		protected override void CloneCore(Freezable sourceFreezable)
		{
			PixelShader shader = (PixelShader)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(shader);
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x0011893C File Offset: 0x00117D3C
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			PixelShader shader = (PixelShader)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(shader);
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x00118960 File Offset: 0x00117D60
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			PixelShader shader = (PixelShader)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			this.CopyCommon(shader);
		}

		// Token: 0x060047A6 RID: 18342 RVA: 0x00118984 File Offset: 0x00117D84
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			PixelShader shader = (PixelShader)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			this.CopyCommon(shader);
		}

		// Token: 0x060047A7 RID: 18343 RVA: 0x001189A8 File Offset: 0x00117DA8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void CopyCommon(PixelShader shader)
		{
			byte[] value = shader._shaderBytecode.Value;
			byte[] array = null;
			if (value != null)
			{
				array = new byte[value.Length];
				value.CopyTo(array, 0);
			}
			this._shaderBytecode = new SecurityCriticalData<byte[]>(array);
		}

		// Token: 0x140001D3 RID: 467
		// (add) Token: 0x060047A8 RID: 18344 RVA: 0x001189E4 File Offset: 0x00117DE4
		// (remove) Token: 0x060047A9 RID: 18345 RVA: 0x00118A1C File Offset: 0x00117E1C
		internal event EventHandler _shaderBytecodeChanged;

		/// <summary>Cria um clone modificável deste objeto <see cref="T:System.Windows.Media.Effects.PixelShader" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência desse objeto, esse método copia associações de dados e referências de recurso (que não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável dessa instância. O clone retornado é efetivamente uma cópia profunda do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do clone é <see langword="false" />.</returns>
		// Token: 0x060047AA RID: 18346 RVA: 0x00118A54 File Offset: 0x00117E54
		public new PixelShader Clone()
		{
			return (PixelShader)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Effects.PixelShader" />, fazendo cópias em profundidade dos valores do objeto atual. Referências de recursos, associações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060047AB RID: 18347 RVA: 0x00118A6C File Offset: 0x00117E6C
		public new PixelShader CloneCurrentValue()
		{
			return (PixelShader)base.CloneCurrentValue();
		}

		// Token: 0x060047AC RID: 18348 RVA: 0x00118A84 File Offset: 0x00117E84
		private static void UriSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PixelShader pixelShader = (PixelShader)d;
			pixelShader.UriSourcePropertyChangedHook(e);
			pixelShader.PropertyChanged(PixelShader.UriSourceProperty);
		}

		// Token: 0x060047AD RID: 18349 RVA: 0x00118AAC File Offset: 0x00117EAC
		private static void ShaderRenderModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PixelShader pixelShader = (PixelShader)d;
			pixelShader.PropertyChanged(PixelShader.ShaderRenderModeProperty);
		}

		/// <summary>Obtém ou define uma referência do URI de pacote ao código de bytes HLSL no assembly.</summary>
		/// <returns>A referência de URI de pacote para o código de bytes HLSL no assembly.</returns>
		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x060047AE RID: 18350 RVA: 0x00118ACC File Offset: 0x00117ECC
		// (set) Token: 0x060047AF RID: 18351 RVA: 0x00118AEC File Offset: 0x00117EEC
		public Uri UriSource
		{
			get
			{
				return (Uri)base.GetValue(PixelShader.UriSourceProperty);
			}
			set
			{
				base.SetValueInternal(PixelShader.UriSourceProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se a renderização de hardware ou de software deve ser usada.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Effects.ShaderRenderMode" /> valor que indica se deve forçar o uso de renderização de hardware ou software para o efeito.</returns>
		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x060047B0 RID: 18352 RVA: 0x00118B08 File Offset: 0x00117F08
		// (set) Token: 0x060047B1 RID: 18353 RVA: 0x00118B28 File Offset: 0x00117F28
		public ShaderRenderMode ShaderRenderMode
		{
			get
			{
				return (ShaderRenderMode)base.GetValue(PixelShader.ShaderRenderModeProperty);
			}
			set
			{
				base.SetValueInternal(PixelShader.ShaderRenderModeProperty, value);
			}
		}

		// Token: 0x060047B2 RID: 18354 RVA: 0x00118B48 File Offset: 0x00117F48
		protected override Freezable CreateInstanceCore()
		{
			return new PixelShader();
		}

		// Token: 0x060047B3 RID: 18355 RVA: 0x00118B5C File Offset: 0x00117F5C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			this.ManualUpdateResource(channel, skipOnChannelCheck);
			base.UpdateResource(channel, skipOnChannelCheck);
		}

		// Token: 0x060047B4 RID: 18356 RVA: 0x00118B7C File Offset: 0x00117F7C
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handle;
			using (CompositionEngineLock.Acquire())
			{
				if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_PIXELSHADER))
				{
					this.AddRefOnChannelAnimations(channel);
					this.UpdateResource(channel, true);
				}
				handle = this._duceResource.GetHandle(channel);
			}
			return handle;
		}

		// Token: 0x060047B5 RID: 18357 RVA: 0x00118BEC File Offset: 0x00117FEC
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			using (CompositionEngineLock.Acquire())
			{
				if (this._duceResource.ReleaseOnChannel(channel))
				{
					this.ReleaseOnChannelAnimations(channel);
				}
			}
		}

		// Token: 0x060047B6 RID: 18358 RVA: 0x00118C40 File Offset: 0x00118040
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handle;
			using (CompositionEngineLock.Acquire())
			{
				handle = this._duceResource.GetHandle(channel);
			}
			return handle;
		}

		// Token: 0x060047B7 RID: 18359 RVA: 0x00118C90 File Offset: 0x00118090
		int DUCE.IResource.GetChannelCount()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x060047B8 RID: 18360 RVA: 0x00118CA8 File Offset: 0x001180A8
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x060047B9 RID: 18361 RVA: 0x00118CC4 File Offset: 0x001180C4
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060047BA RID: 18362 RVA: 0x00118CD4 File Offset: 0x001180D4
		static PixelShader()
		{
			Type typeFromHandle = typeof(PixelShader);
			PixelShader.UriSourceProperty = Animatable.RegisterProperty("UriSource", typeof(Uri), typeFromHandle, null, new PropertyChangedCallback(PixelShader.UriSourcePropertyChanged), null, false, null);
			PixelShader.ShaderRenderModeProperty = Animatable.RegisterProperty("ShaderRenderMode", typeof(ShaderRenderMode), typeFromHandle, ShaderRenderMode.Auto, new PropertyChangedCallback(PixelShader.ShaderRenderModePropertyChanged), new ValidateValueCallback(ValidateEnums.IsShaderRenderModeValid), false, null);
		}

		// Token: 0x04001A1D RID: 6685
		private SecurityCriticalData<byte[]> _shaderBytecode;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.PixelShader.UriSource" />.</summary>
		// Token: 0x04001A1F RID: 6687
		public static readonly DependencyProperty UriSourceProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.PixelShader.ShaderRenderMode" />.</summary>
		// Token: 0x04001A20 RID: 6688
		public static readonly DependencyProperty ShaderRenderModeProperty;

		// Token: 0x04001A21 RID: 6689
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x04001A22 RID: 6690
		internal static Uri s_UriSource;

		// Token: 0x04001A23 RID: 6691
		internal const ShaderRenderMode c_ShaderRenderMode = ShaderRenderMode.Auto;
	}
}
