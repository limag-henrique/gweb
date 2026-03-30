using System;

namespace System.Windows.Media
{
	/// <summary>Permite que aplicativos WPF consultem a camada de renderização atual para seu objeto <see cref="T:System.Windows.Threading.Dispatcher" /> associado e sejam registrados para notificações de alterações.</summary>
	// Token: 0x02000433 RID: 1075
	public static class RenderCapability
	{
		/// <summary>Obtém um valor que indica a camada de renderização do thread atual.</summary>
		/// <returns>Um valor <see cref="T:System.Int32" /> cuja palavra de ordem superior corresponde à camada de renderização para o thread atual.</returns>
		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06002C13 RID: 11283 RVA: 0x000B0178 File Offset: 0x000AF578
		public static int Tier
		{
			get
			{
				MediaContext currentMediaContext = MediaContext.CurrentMediaContext;
				return currentMediaContext.Tier;
			}
		}

		/// <summary>Obtém um valor que indica se a versão do sombreador de pixel especificada é compatível.</summary>
		/// <param name="majorVersionRequested">A versão principal do sombreador de pixel.</param>
		/// <param name="minorVersionRequested">A versão secundária do sombreador de pixel.</param>
		/// <returns>
		///   <see langword="true" /> se a versão de sombreador de pixel é compatível com a versão atual do WPF; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002C14 RID: 11284 RVA: 0x000B0194 File Offset: 0x000AF594
		public static bool IsPixelShaderVersionSupported(short majorVersionRequested, short minorVersionRequested)
		{
			bool result = false;
			if ((majorVersionRequested == 2 && minorVersionRequested == 0) || (majorVersionRequested == 3 && minorVersionRequested == 0))
			{
				MediaContext currentMediaContext = MediaContext.CurrentMediaContext;
				byte b = (byte)(currentMediaContext.PixelShaderVersion >> 8 & 255U);
				byte b2 = (byte)(currentMediaContext.PixelShaderVersion & 255U);
				if ((short)b >= majorVersionRequested)
				{
					result = true;
				}
				else if ((short)b == majorVersionRequested && (short)b2 >= minorVersionRequested)
				{
					result = true;
				}
			}
			return result;
		}

		/// <summary>Obtém um valor que indica se a versão do sombreador de pixel especificada pode ser renderizada em software no sistema atual.</summary>
		/// <param name="majorVersionRequested">A versão principal do sombreador de pixel.</param>
		/// <param name="minorVersionRequested">A versão secundária do sombreador de pixel.</param>
		/// <returns>
		///   <see langword="true" /> se o sombreador de pixel pode ser renderizado em software no sistema atual; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002C15 RID: 11285 RVA: 0x000B01E8 File Offset: 0x000AF5E8
		public static bool IsPixelShaderVersionSupportedInSoftware(short majorVersionRequested, short minorVersionRequested)
		{
			bool result = false;
			if (majorVersionRequested == 2 && minorVersionRequested == 0)
			{
				MediaContext currentMediaContext = MediaContext.CurrentMediaContext;
				result = currentMediaContext.HasSSE2Support;
			}
			return result;
		}

		/// <summary>Obtém um valor que indica se o sistema pode renderizar efeitos de bitmap no software.</summary>
		/// <returns>
		///   <see langword="true" /> Se o sistema pode renderizar efeitos de bitmap no software; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06002C16 RID: 11286 RVA: 0x000B020C File Offset: 0x000AF60C
		[Obsolete("IsShaderEffectSoftwareRenderingSupported property is deprecated.  Use IsPixelShaderVersionSupportedInSoftware static method instead.")]
		public static bool IsShaderEffectSoftwareRenderingSupported
		{
			get
			{
				MediaContext currentMediaContext = MediaContext.CurrentMediaContext;
				return currentMediaContext.HasSSE2Support;
			}
		}

		/// <summary>Obtém o número máximo de slots de instrução compatível com a versão de sombreador de pixel especificada.</summary>
		/// <param name="majorVersionRequested">A versão principal do sombreador de pixel.</param>
		/// <param name="minorVersionRequested">A versão secundária do sombreador de pixel.</param>
		/// <returns>96 para Sombreador de Pixel 2.0, 512 ou superior para Sombreador de Pixel 3.0 ou então 0 para qualquer outra versão do Sombreador de Pixel.</returns>
		// Token: 0x06002C17 RID: 11287 RVA: 0x000B0228 File Offset: 0x000AF628
		public static int MaxPixelShaderInstructionSlots(short majorVersionRequested, short minorVersionRequested)
		{
			if (majorVersionRequested == 2 && minorVersionRequested == 0)
			{
				return 96;
			}
			if (majorVersionRequested == 3 && minorVersionRequested == 0)
			{
				MediaContext currentMediaContext = MediaContext.CurrentMediaContext;
				return (int)currentMediaContext.MaxPixelShader30InstructionSlots;
			}
			return 0;
		}

		/// <summary>Obtém a altura e largura máximas para a criação de bitmap do dispositivo de hardware subjacente.</summary>
		/// <returns>Um <see cref="T:System.Windows.Size" /> que representa a largura máxima e a altura para a criação de bitmap de hardware.</returns>
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06002C18 RID: 11288 RVA: 0x000B0254 File Offset: 0x000AF654
		public static Size MaxHardwareTextureSize
		{
			get
			{
				MediaContext currentMediaContext = MediaContext.CurrentMediaContext;
				return currentMediaContext.MaxTextureSize;
			}
		}

		/// <summary>Ocorre quando a camada de renderização foi alterada para o objeto <see cref="T:System.Windows.Threading.Dispatcher" /> do thread atual.</summary>
		// Token: 0x140001B7 RID: 439
		// (add) Token: 0x06002C19 RID: 11289 RVA: 0x000B0270 File Offset: 0x000AF670
		// (remove) Token: 0x06002C1A RID: 11290 RVA: 0x000B028C File Offset: 0x000AF68C
		public static event EventHandler TierChanged
		{
			add
			{
				MediaContext currentMediaContext = MediaContext.CurrentMediaContext;
				currentMediaContext.TierChanged += value;
			}
			remove
			{
				MediaContext currentMediaContext = MediaContext.CurrentMediaContext;
				currentMediaContext.TierChanged -= value;
			}
		}

		// Token: 0x0400141C RID: 5148
		private const string IsShaderEffectSoftwareRenderingSupported_Deprecated = "IsShaderEffectSoftwareRenderingSupported property is deprecated.  Use IsPixelShaderVersionSupportedInSoftware static method instead.";
	}
}
