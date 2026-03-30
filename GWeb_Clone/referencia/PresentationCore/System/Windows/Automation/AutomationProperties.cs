using System;
using MS.Internal.KnownBoxes;

namespace System.Windows.Automation
{
	/// <summary>Fornece um meio de obter ou definir o valor das propriedades associadas da instância do elemento <see cref="T:System.Windows.Automation.Peers.AutomationPeer" />.</summary>
	// Token: 0x0200030B RID: 779
	public static class AutomationProperties
	{
		/// <summary>Define a propriedade anexada <see cref="P:System.Windows.Automation.AutomationProperties.AutomationId" /> do <see cref="T:System.Windows.DependencyObject" /> especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> no qual definir a propriedade.</param>
		/// <param name="value">O valor do identificador Automação da interface do usuário a ser definido.</param>
		// Token: 0x060018DF RID: 6367 RVA: 0x00062E08 File Offset: 0x00062208
		public static void SetAutomationId(DependencyObject element, string value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(AutomationProperties.AutomationIdProperty, value);
		}

		/// <summary>Obtém a <see cref="P:System.Windows.Automation.AutomationProperties.AutomationId" /> propriedade anexada do especificado <see cref="T:System.Windows.DependencyObject" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> a ser verificado.</param>
		/// <returns>O identificador Automação da interface do usuário do elemento especificado.</returns>
		// Token: 0x060018E0 RID: 6368 RVA: 0x00062E30 File Offset: 0x00062230
		public static string GetAutomationId(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (string)element.GetValue(AutomationProperties.AutomationIdProperty);
		}

		/// <summary>Define a propriedade anexada <see cref="P:System.Windows.Automation.AutomationProperties.Name" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> no qual definir a propriedade.</param>
		/// <param name="value">O valor de nome a ser definido.</param>
		// Token: 0x060018E1 RID: 6369 RVA: 0x00062E5C File Offset: 0x0006225C
		public static void SetName(DependencyObject element, string value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(AutomationProperties.NameProperty, value);
		}

		/// <summary>Obtém a <see cref="P:System.Windows.Automation.AutomationProperties.Name" /> propriedade anexada do especificado <see cref="T:System.Windows.DependencyObject" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> a ser verificado.</param>
		/// <returns>O nome do elemento especificado.</returns>
		// Token: 0x060018E2 RID: 6370 RVA: 0x00062E84 File Offset: 0x00062284
		public static string GetName(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (string)element.GetValue(AutomationProperties.NameProperty);
		}

		/// <summary>Define a propriedade anexada <see cref="P:System.Windows.Automation.AutomationProperties.HelpText" /> do <see cref="T:System.Windows.DependencyObject" /> especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> no qual definir a propriedade.</param>
		/// <param name="value">O valor do texto de ajuda a ser definido.</param>
		// Token: 0x060018E3 RID: 6371 RVA: 0x00062EB0 File Offset: 0x000622B0
		public static void SetHelpText(DependencyObject element, string value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(AutomationProperties.HelpTextProperty, value);
		}

		/// <summary>Obtém a <see cref="P:System.Windows.Automation.AutomationProperties.HelpText" /> propriedade anexada do especificado <see cref="T:System.Windows.DependencyObject" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> a ser verificado.</param>
		/// <returns>Uma cadeia de caracteres que contém o texto de ajuda do elemento especificado. A cadeia de caracteres retornada geralmente é o mesmo texto que é fornecido na dica de ferramenta para o controle.</returns>
		// Token: 0x060018E4 RID: 6372 RVA: 0x00062ED8 File Offset: 0x000622D8
		public static string GetHelpText(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (string)element.GetValue(AutomationProperties.HelpTextProperty);
		}

		/// <summary>Define a propriedade anexada <see cref="P:System.Windows.Automation.AutomationProperties.AcceleratorKey" /> do <see cref="T:System.Windows.DependencyObject" /> especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> no qual definir a propriedade.</param>
		/// <param name="value">O valor da chave de acelerador a ser definido.</param>
		// Token: 0x060018E5 RID: 6373 RVA: 0x00062F04 File Offset: 0x00062304
		public static void SetAcceleratorKey(DependencyObject element, string value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(AutomationProperties.AcceleratorKeyProperty, value);
		}

		/// <summary>Obtém a <see cref="P:System.Windows.Automation.AutomationProperties.AcceleratorKey" /> propriedade anexada do especificado <see cref="T:System.Windows.DependencyObject" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> a ser verificado.</param>
		/// <returns>Uma cadeia de caracteres que contém a tecla de atalho.</returns>
		// Token: 0x060018E6 RID: 6374 RVA: 0x00062F2C File Offset: 0x0006232C
		public static string GetAcceleratorKey(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (string)element.GetValue(AutomationProperties.AcceleratorKeyProperty);
		}

		/// <summary>Define a propriedade anexada <see cref="P:System.Windows.Automation.AutomationProperties.AccessKey" /> do <see cref="T:System.Windows.DependencyObject" /> especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> no qual definir a propriedade.</param>
		/// <param name="value">O valor da chave de acesso a ser definido.</param>
		// Token: 0x060018E7 RID: 6375 RVA: 0x00062F58 File Offset: 0x00062358
		public static void SetAccessKey(DependencyObject element, string value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(AutomationProperties.AccessKeyProperty, value);
		}

		/// <summary>Obtém a <see cref="P:System.Windows.Automation.AutomationProperties.AccessKey" /> propriedade anexada do especificado <see cref="T:System.Windows.DependencyObject" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> a ser verificado.</param>
		/// <returns>A chave de acesso do elemento especificado.</returns>
		// Token: 0x060018E8 RID: 6376 RVA: 0x00062F80 File Offset: 0x00062380
		public static string GetAccessKey(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (string)element.GetValue(AutomationProperties.AccessKeyProperty);
		}

		/// <summary>Define a propriedade anexada <see cref="P:System.Windows.Automation.AutomationProperties.ItemStatus" /> do <see cref="T:System.Windows.DependencyObject" /> especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> no qual definir a propriedade.</param>
		/// <param name="value">O valor de status do item a ser definido.</param>
		// Token: 0x060018E9 RID: 6377 RVA: 0x00062FAC File Offset: 0x000623AC
		public static void SetItemStatus(DependencyObject element, string value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(AutomationProperties.ItemStatusProperty, value);
		}

		/// <summary>Obtém a <see cref="P:System.Windows.Automation.AutomationProperties.ItemStatus" /> propriedade anexada do especificado <see cref="T:System.Windows.DependencyObject" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> a ser verificado.</param>
		/// <returns>O <see cref="P:System.Windows.Automation.AutomationElement.AutomationElementInformation.ItemStatus" /> de um determinado elemento.</returns>
		// Token: 0x060018EA RID: 6378 RVA: 0x00062FD4 File Offset: 0x000623D4
		public static string GetItemStatus(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (string)element.GetValue(AutomationProperties.ItemStatusProperty);
		}

		/// <summary>Define a propriedade anexada <see cref="P:System.Windows.Automation.AutomationProperties.ItemType" /> do <see cref="T:System.Windows.DependencyObject" /> especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> no qual definir a propriedade.</param>
		/// <param name="value">O valor de tipo de item a ser definido.</param>
		// Token: 0x060018EB RID: 6379 RVA: 0x00063000 File Offset: 0x00062400
		public static void SetItemType(DependencyObject element, string value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(AutomationProperties.ItemTypeProperty, value);
		}

		/// <summary>Obtém a <see cref="P:System.Windows.Automation.AutomationProperties.ItemType" /> propriedade anexada do especificado <see cref="T:System.Windows.DependencyObject" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> a ser verificado.</param>
		/// <returns>O <see cref="P:System.Windows.Automation.AutomationElement.AutomationElementInformation.ItemType" /> de um determinado elemento.</returns>
		// Token: 0x060018EC RID: 6380 RVA: 0x00063028 File Offset: 0x00062428
		public static string GetItemType(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (string)element.GetValue(AutomationProperties.ItemTypeProperty);
		}

		/// <summary>Define a propriedade anexada <see cref="P:System.Windows.Automation.AutomationProperties.IsColumnHeader" /> do <see cref="T:System.Windows.DependencyObject" /> especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> no qual definir a propriedade.</param>
		/// <param name="value">O valor a ser definido, <see langword="true" /> se o elemento deve ser um cabeçalho de coluna, caso contrário, <see langword="false" /></param>
		// Token: 0x060018ED RID: 6381 RVA: 0x00063054 File Offset: 0x00062454
		public static void SetIsColumnHeader(DependencyObject element, bool value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(AutomationProperties.IsColumnHeaderProperty, value);
		}

		/// <summary>Obtém a <see cref="P:System.Windows.Automation.AutomationProperties.IsColumnHeader" /> propriedade anexada do especificado <see cref="T:System.Windows.DependencyObject" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> a ser verificado.</param>
		/// <returns>Um <see langword="boolean" /> que indica se o elemento especificado é um <see cref="F:System.Windows.Automation.TablePattern.ColumnHeadersProperty" />.</returns>
		// Token: 0x060018EE RID: 6382 RVA: 0x0006307C File Offset: 0x0006247C
		public static bool GetIsColumnHeader(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (bool)element.GetValue(AutomationProperties.IsColumnHeaderProperty);
		}

		/// <summary>Define a propriedade anexada <see cref="P:System.Windows.Automation.AutomationProperties.IsRowHeader" /> do <see cref="T:System.Windows.DependencyObject" /> especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> no qual definir a propriedade.</param>
		/// <param name="value">O valor a ser definido, <see langword="true" /> se o elemento deve ser um cabeçalho de linha, caso contrário, <see langword="false" />.</param>
		// Token: 0x060018EF RID: 6383 RVA: 0x000630A8 File Offset: 0x000624A8
		public static void SetIsRowHeader(DependencyObject element, bool value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(AutomationProperties.IsRowHeaderProperty, value);
		}

		/// <summary>Obtém a <see cref="P:System.Windows.Automation.AutomationProperties.IsRowHeader" /> propriedade anexada do especificado <see cref="T:System.Windows.DependencyObject" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> a ser verificado.</param>
		/// <returns>Um <see langword="boolean" /> que indica se o elemento especificado é um <see cref="F:System.Windows.Automation.TablePattern.RowHeadersProperty" />.</returns>
		// Token: 0x060018F0 RID: 6384 RVA: 0x000630D0 File Offset: 0x000624D0
		public static bool GetIsRowHeader(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (bool)element.GetValue(AutomationProperties.IsRowHeaderProperty);
		}

		/// <summary>Define a propriedade anexada <see cref="P:System.Windows.Automation.AutomationProperties.IsRequiredForForm" /> do <see cref="T:System.Windows.DependencyObject" /> especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> no qual definir a propriedade.</param>
		/// <param name="value">O valor a ser definido, <see langword="true" /> se o elemento deve ser preenchido em um formulário, caso contrário, <see langword="false" />.</param>
		// Token: 0x060018F1 RID: 6385 RVA: 0x000630FC File Offset: 0x000624FC
		public static void SetIsRequiredForForm(DependencyObject element, bool value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(AutomationProperties.IsRequiredForFormProperty, value);
		}

		/// <summary>Obtém a <see cref="P:System.Windows.Automation.AutomationProperties.IsRequiredForForm" /> propriedade anexada do especificado <see cref="T:System.Windows.DependencyObject" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> a ser verificado.</param>
		/// <returns>Um <see langword="boolean" /> que indica se o elemento especificado é <see cref="P:System.Windows.Automation.AutomationElement.AutomationElementInformation.IsRequiredForForm" />.</returns>
		// Token: 0x060018F2 RID: 6386 RVA: 0x00063124 File Offset: 0x00062524
		public static bool GetIsRequiredForForm(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (bool)element.GetValue(AutomationProperties.IsRequiredForFormProperty);
		}

		/// <summary>Define a propriedade anexada <see cref="P:System.Windows.Automation.AutomationProperties.LabeledBy" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> no qual definir a propriedade.</param>
		/// <param name="value">O rotulado pelo valor a ser definido.</param>
		// Token: 0x060018F3 RID: 6387 RVA: 0x00063150 File Offset: 0x00062550
		public static void SetLabeledBy(DependencyObject element, UIElement value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(AutomationProperties.LabeledByProperty, value);
		}

		/// <summary>Obtém a <see cref="P:System.Windows.Automation.AutomationProperties.LabeledBy" /> propriedade anexada do especificado <see cref="T:System.Windows.DependencyObject" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> a ser verificado.</param>
		/// <returns>O elemento que é o destino do rótulo.</returns>
		// Token: 0x060018F4 RID: 6388 RVA: 0x00063178 File Offset: 0x00062578
		public static UIElement GetLabeledBy(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (UIElement)element.GetValue(AutomationProperties.LabeledByProperty);
		}

		/// <summary>Define a propriedade anexada <see cref="P:System.Windows.Automation.AutomationProperties.IsOffscreenBehavior" /> do <see cref="T:System.Windows.DependencyObject" /> especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> no qual definir a propriedade.</param>
		/// <param name="value">Um valor que especifica como um elemento determina como a propriedade <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsOffscreen" /> é determinada.</param>
		// Token: 0x060018F5 RID: 6389 RVA: 0x000631A4 File Offset: 0x000625A4
		public static void SetIsOffscreenBehavior(DependencyObject element, IsOffscreenBehavior value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(AutomationProperties.IsOffscreenBehaviorProperty, value);
		}

		/// <summary>Obtém a <see cref="P:System.Windows.Automation.AutomationProperties.IsOffscreenBehavior" /> propriedade anexada do especificado <see cref="T:System.Windows.DependencyObject" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> a ser verificado.</param>
		/// <returns>Um valor que especifica como um elemento determina como a propriedade <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsOffscreen" /> é determinada.</returns>
		// Token: 0x060018F6 RID: 6390 RVA: 0x000631D0 File Offset: 0x000625D0
		public static IsOffscreenBehavior GetIsOffscreenBehavior(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (IsOffscreenBehavior)element.GetValue(AutomationProperties.IsOffscreenBehaviorProperty);
		}

		/// <summary>Define a propriedade anexada <see cref="P:System.Windows.Automation.AutomationProperties.LiveSetting" /> do <see cref="T:System.Windows.DependencyObject" /> especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> a ser verificado.</param>
		/// <param name="value">O valor a ser atribuído à propriedade.</param>
		// Token: 0x060018F7 RID: 6391 RVA: 0x000631FC File Offset: 0x000625FC
		public static void SetLiveSetting(DependencyObject element, AutomationLiveSetting value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(AutomationProperties.LiveSettingProperty, value);
		}

		/// <summary>Obtém a <see cref="P:System.Windows.Automation.AutomationProperties.LiveSetting" /> propriedade anexada do especificado <see cref="T:System.Windows.DependencyObject" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.DependencyObject" /> a ser verificado.</param>
		/// <returns>O valor de <see cref="T:System.Windows.Automation.AutomationLiveSetting" /> para <paramref name="element" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> é <see langword="null" />.</exception>
		// Token: 0x060018F8 RID: 6392 RVA: 0x00063228 File Offset: 0x00062628
		public static AutomationLiveSetting GetLiveSetting(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (AutomationLiveSetting)element.GetValue(AutomationProperties.LiveSettingProperty);
		}

		/// <summary>Define a propriedade anexada <see cref="P:System.Windows.Automation.AutomationProperties.PositionInSet" /> do <see cref="T:System.Windows.DependencyObject" /> especificado.</summary>
		/// <param name="element">O objeto de dependência no qual definir a propriedade.</param>
		/// <param name="value">O valor a ser definido.</param>
		// Token: 0x060018F9 RID: 6393 RVA: 0x00063254 File Offset: 0x00062654
		public static void SetPositionInSet(DependencyObject element, int value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(AutomationProperties.PositionInSetProperty, value);
		}

		/// <summary>Obtém o valor da propriedade <see cref="P:System.Windows.Automation.AutomationProperties.PositionInSet" /> para o <see cref="T:System.Windows.DependencyObject" /> especificado.</summary>
		/// <param name="element">O objeto de dependência no qual obter o valor da propriedade.</param>
		/// <returns>O valor da propriedade anexada <see cref="P:System.Windows.Automation.AutomationProperties.PositionInSet" /> de <paramref name="element" />.</returns>
		// Token: 0x060018FA RID: 6394 RVA: 0x00063280 File Offset: 0x00062680
		public static int GetPositionInSet(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (int)element.GetValue(AutomationProperties.PositionInSetProperty);
		}

		/// <summary>Define a propriedade anexada <see cref="P:System.Windows.Automation.AutomationProperties.SizeOfSet" /> do <see cref="T:System.Windows.DependencyObject" /> especificado.</summary>
		/// <param name="element">O objeto de dependência no qual definir a propriedade.</param>
		/// <param name="value">O valor a ser definido.</param>
		// Token: 0x060018FB RID: 6395 RVA: 0x000632AC File Offset: 0x000626AC
		public static void SetSizeOfSet(DependencyObject element, int value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(AutomationProperties.SizeOfSetProperty, value);
		}

		/// <summary>Obtém o valor da propriedade anexada <see cref="P:System.Windows.Automation.AutomationProperties.SizeOfSet" /> do <see cref="T:System.Windows.DependencyObject" /> especificado.</summary>
		/// <param name="element">O objeto de dependência no qual obter o valor da propriedade.</param>
		/// <returns>O valor da propriedade anexada <see cref="P:System.Windows.Automation.AutomationProperties.SizeOfSet" /> de <paramref name="element" />.</returns>
		// Token: 0x060018FC RID: 6396 RVA: 0x000632D8 File Offset: 0x000626D8
		public static int GetSizeOfSet(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (int)element.GetValue(AutomationProperties.SizeOfSetProperty);
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x00063304 File Offset: 0x00062704
		public static void SetHeadingLevel(DependencyObject element, AutomationHeadingLevel value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(AutomationProperties.HeadingLevelProperty, value);
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x00063330 File Offset: 0x00062730
		public static AutomationHeadingLevel GetHeadingLevel(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (AutomationHeadingLevel)element.GetValue(AutomationProperties.HeadingLevelProperty);
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0006335C File Offset: 0x0006275C
		public static void SetIsDialog(DependencyObject element, bool value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(AutomationProperties.IsDialogProperty, value);
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x00063384 File Offset: 0x00062784
		public static bool GetIsDialog(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (bool)element.GetValue(AutomationProperties.IsDialogProperty);
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x000633B0 File Offset: 0x000627B0
		private static bool IsNotNull(object value)
		{
			return value != null;
		}

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Automation.AutomationProperties.AutomationId" /> anexada.</summary>
		// Token: 0x04000D5E RID: 3422
		public static readonly DependencyProperty AutomationIdProperty = DependencyProperty.RegisterAttached("AutomationId", typeof(string), typeof(AutomationProperties), new UIPropertyMetadata(string.Empty), new ValidateValueCallback(AutomationProperties.IsNotNull));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Automation.AutomationProperties.Name" /> anexada.</summary>
		// Token: 0x04000D5F RID: 3423
		public static readonly DependencyProperty NameProperty = DependencyProperty.RegisterAttached("Name", typeof(string), typeof(AutomationProperties), new UIPropertyMetadata(string.Empty), new ValidateValueCallback(AutomationProperties.IsNotNull));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Automation.AutomationProperties.HelpText" /> anexada.</summary>
		// Token: 0x04000D60 RID: 3424
		public static readonly DependencyProperty HelpTextProperty = DependencyProperty.RegisterAttached("HelpText", typeof(string), typeof(AutomationProperties), new UIPropertyMetadata(string.Empty), new ValidateValueCallback(AutomationProperties.IsNotNull));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Automation.AutomationProperties.AcceleratorKey" /> anexada.</summary>
		// Token: 0x04000D61 RID: 3425
		public static readonly DependencyProperty AcceleratorKeyProperty = DependencyProperty.RegisterAttached("AcceleratorKey", typeof(string), typeof(AutomationProperties), new UIPropertyMetadata(string.Empty), new ValidateValueCallback(AutomationProperties.IsNotNull));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Automation.AutomationProperties.AccessKey" /> anexada.</summary>
		// Token: 0x04000D62 RID: 3426
		public static readonly DependencyProperty AccessKeyProperty = DependencyProperty.RegisterAttached("AccessKey", typeof(string), typeof(AutomationProperties), new UIPropertyMetadata(string.Empty), new ValidateValueCallback(AutomationProperties.IsNotNull));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Automation.AutomationProperties.ItemStatus" /> anexada.</summary>
		// Token: 0x04000D63 RID: 3427
		public static readonly DependencyProperty ItemStatusProperty = DependencyProperty.RegisterAttached("ItemStatus", typeof(string), typeof(AutomationProperties), new UIPropertyMetadata(string.Empty), new ValidateValueCallback(AutomationProperties.IsNotNull));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Automation.AutomationProperties.ItemType" /> anexada.</summary>
		// Token: 0x04000D64 RID: 3428
		public static readonly DependencyProperty ItemTypeProperty = DependencyProperty.RegisterAttached("ItemType", typeof(string), typeof(AutomationProperties), new UIPropertyMetadata(string.Empty), new ValidateValueCallback(AutomationProperties.IsNotNull));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Automation.AutomationProperties.IsColumnHeader" /> anexada.</summary>
		// Token: 0x04000D65 RID: 3429
		public static readonly DependencyProperty IsColumnHeaderProperty = DependencyProperty.RegisterAttached("IsColumnHeader", typeof(bool), typeof(AutomationProperties), new UIPropertyMetadata(BooleanBoxes.FalseBox));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Automation.AutomationProperties.IsRowHeader" /> anexada.</summary>
		// Token: 0x04000D66 RID: 3430
		public static readonly DependencyProperty IsRowHeaderProperty = DependencyProperty.RegisterAttached("IsRowHeader", typeof(bool), typeof(AutomationProperties), new UIPropertyMetadata(BooleanBoxes.FalseBox));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Automation.AutomationProperties.IsRequiredForForm" /> anexada.</summary>
		// Token: 0x04000D67 RID: 3431
		public static readonly DependencyProperty IsRequiredForFormProperty = DependencyProperty.RegisterAttached("IsRequiredForForm", typeof(bool), typeof(AutomationProperties), new UIPropertyMetadata(BooleanBoxes.FalseBox));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Automation.AutomationProperties.LabeledBy" /> anexada.</summary>
		// Token: 0x04000D68 RID: 3432
		public static readonly DependencyProperty LabeledByProperty = DependencyProperty.RegisterAttached("LabeledBy", typeof(UIElement), typeof(AutomationProperties), new UIPropertyMetadata(null));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Automation.AutomationProperties.IsOffscreenBehavior" />.</summary>
		// Token: 0x04000D69 RID: 3433
		public static readonly DependencyProperty IsOffscreenBehaviorProperty = DependencyProperty.RegisterAttached("IsOffscreenBehavior", typeof(IsOffscreenBehavior), typeof(AutomationProperties), new UIPropertyMetadata(IsOffscreenBehavior.Default));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Automation.AutomationProperties.LiveSetting" /> anexada.</summary>
		// Token: 0x04000D6A RID: 3434
		public static readonly DependencyProperty LiveSettingProperty = DependencyProperty.RegisterAttached("LiveSetting", typeof(AutomationLiveSetting), typeof(AutomationProperties), new UIPropertyMetadata(AutomationLiveSetting.Off));

		/// <summary>Identifica a propriedade anexada PositionInSet.</summary>
		// Token: 0x04000D6B RID: 3435
		public static readonly DependencyProperty PositionInSetProperty = DependencyProperty.RegisterAttached("PositionInSet", typeof(int), typeof(AutomationProperties), new UIPropertyMetadata(-1));

		/// <summary>Identifica a propriedade anexada SizeOfSet.</summary>
		// Token: 0x04000D6C RID: 3436
		public static readonly DependencyProperty SizeOfSetProperty = DependencyProperty.RegisterAttached("SizeOfSet", typeof(int), typeof(AutomationProperties), new UIPropertyMetadata(-1));

		// Token: 0x04000D6D RID: 3437
		public static readonly DependencyProperty HeadingLevelProperty = DependencyProperty.RegisterAttached("HeadingLevel", typeof(AutomationHeadingLevel), typeof(AutomationProperties), new UIPropertyMetadata(AutomationHeadingLevel.None));

		// Token: 0x04000D6E RID: 3438
		public static readonly DependencyProperty IsDialogProperty = DependencyProperty.RegisterAttached("IsDialog", typeof(bool), typeof(AutomationProperties), new UIPropertyMetadata(false));

		// Token: 0x04000D6F RID: 3439
		internal const int AutomationPositionInSetDefault = -1;

		// Token: 0x04000D70 RID: 3440
		internal const int AutomationSizeOfSetDefault = -1;
	}
}
