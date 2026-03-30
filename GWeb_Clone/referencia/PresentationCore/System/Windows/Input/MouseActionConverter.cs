using System;
using System.ComponentModel;
using System.Globalization;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Converte um objeto <see cref="T:System.Windows.Input.MouseAction" /> de e em outros tipos.</summary>
	// Token: 0x0200021B RID: 539
	public class MouseActionConverter : TypeConverter
	{
		/// <summary>Determina se um objeto do tipo especificado pode ser convertido em uma instância de <see cref="T:System.Windows.Input.MouseAction" />, usando o contexto especificado.</summary>
		/// <param name="context">Um contexto de formato que fornece informações sobre o ambiente do qual este conversor está sendo invocado.</param>
		/// <param name="sourceType">O tipo que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se esse conversor puder realizar a operação, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000E7A RID: 3706 RVA: 0x00036D74 File Offset: 0x00036174
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		/// <summary>Determina se uma instância do <see cref="T:System.Windows.Input.MouseAction" /> pode ser convertida no tipo especificado, usando o contexto especificado.</summary>
		/// <param name="context">Um contexto de formato que fornece informações sobre o ambiente do qual este conversor está sendo invocado.</param>
		/// <param name="destinationType">O tipo que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se esse conversor puder realizar a operação, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000E7B RID: 3707 RVA: 0x00036D98 File Offset: 0x00036198
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) && context != null && context.Instance != null && MouseActionConverter.IsDefinedMouseAction((MouseAction)context.Instance);
		}

		/// <summary>Tenta converter o objeto especificado em um <see cref="T:System.Windows.Input.MouseAction" />, usando o contexto especificado.</summary>
		/// <param name="context">Um contexto de formato que fornece informações sobre o ambiente do qual este conversor está sendo invocado.</param>
		/// <param name="culture">Informações específicas à cultura.</param>
		/// <param name="source">O objeto a ser convertido.</param>
		/// <returns>O objeto convertido.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="source" /> não pode ser convertido.</exception>
		// Token: 0x06000E7C RID: 3708 RVA: 0x00036DD4 File Offset: 0x000361D4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object source)
		{
			if (source == null || !(source is string))
			{
				throw base.GetConvertFromException(source);
			}
			string text = ((string)source).Trim();
			text = text.ToUpper(CultureInfo.InvariantCulture);
			if (text == string.Empty)
			{
				return MouseAction.None;
			}
			uint num = <PrivateImplementationDetails><PresentationCoreCSharp_netmodule>.ComputeStringHash(text);
			MouseAction mouseAction;
			if (num <= 1645369192U)
			{
				if (num != 992238400U)
				{
					if (num != 1060535247U)
					{
						if (num == 1645369192U)
						{
							if (text == "LEFTCLICK")
							{
								mouseAction = MouseAction.LeftClick;
								goto IL_142;
							}
						}
					}
					else if (text == "RIGHTCLICK")
					{
						mouseAction = MouseAction.RightClick;
						goto IL_142;
					}
				}
				else if (text == "RIGHTDOUBLECLICK")
				{
					mouseAction = MouseAction.RightDoubleClick;
					goto IL_142;
				}
			}
			else if (num <= 2489465264U)
			{
				if (num != 2077277671U)
				{
					if (num == 2489465264U)
					{
						if (text == "MIDDLECLICK")
						{
							mouseAction = MouseAction.MiddleClick;
							goto IL_142;
						}
					}
				}
				else if (text == "LEFTDOUBLECLICK")
				{
					mouseAction = MouseAction.LeftDoubleClick;
					goto IL_142;
				}
			}
			else if (num != 3445007316U)
			{
				if (num == 3554986863U)
				{
					if (text == "MIDDLEDOUBLECLICK")
					{
						mouseAction = MouseAction.MiddleDoubleClick;
						goto IL_142;
					}
				}
			}
			else if (text == "WHEELCLICK")
			{
				mouseAction = MouseAction.WheelClick;
				goto IL_142;
			}
			throw new NotSupportedException(SR.Get("Unsupported_MouseAction", new object[]
			{
				text
			}));
			IL_142:
			return mouseAction;
		}

		/// <summary>Tenta converter um <see cref="T:System.Windows.Input.MouseAction" /> no tipo especificado, usando o contexto especificado.</summary>
		/// <param name="context">Um contexto de formato que fornece informações sobre o ambiente do qual este conversor está sendo invocado.</param>
		/// <param name="culture">Informações específicas à cultura.</param>
		/// <param name="value">O objeto a ser convertido.</param>
		/// <param name="destinationType">O tipo no qual converter o objeto.</param>
		/// <returns>O objeto convertido.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="value" /> não é mapeado para um <see cref="T:System.Windows.Input.MouseAction" /> válido.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> não pode ser convertido.</exception>
		// Token: 0x06000E7D RID: 3709 RVA: 0x00036F34 File Offset: 0x00036334
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value != null)
			{
				MouseAction mouseAction = (MouseAction)value;
				if (MouseActionConverter.IsDefinedMouseAction(mouseAction))
				{
					string text = null;
					switch (mouseAction)
					{
					case MouseAction.None:
						text = string.Empty;
						break;
					case MouseAction.LeftClick:
						text = "LeftClick";
						break;
					case MouseAction.RightClick:
						text = "RightClick";
						break;
					case MouseAction.MiddleClick:
						text = "MiddleClick";
						break;
					case MouseAction.WheelClick:
						text = "WheelClick";
						break;
					case MouseAction.LeftDoubleClick:
						text = "LeftDoubleClick";
						break;
					case MouseAction.RightDoubleClick:
						text = "RightDoubleClick";
						break;
					case MouseAction.MiddleDoubleClick:
						text = "MiddleDoubleClick";
						break;
					}
					if (text != null)
					{
						return text;
					}
				}
				throw new InvalidEnumArgumentException("value", (int)mouseAction, typeof(MouseAction));
			}
			throw base.GetConvertToException(value, destinationType);
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00037010 File Offset: 0x00036410
		internal static bool IsDefinedMouseAction(MouseAction mouseAction)
		{
			return mouseAction >= MouseAction.None && mouseAction <= MouseAction.MiddleDoubleClick;
		}
	}
}
