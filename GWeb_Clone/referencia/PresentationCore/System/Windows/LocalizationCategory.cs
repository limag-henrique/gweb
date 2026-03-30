using System;

namespace System.Windows
{
	/// <summary>Especifica o valor da categoria de um <see cref="T:System.Windows.LocalizabilityAttribute" /> para uma classe ou membro de classe BAML (XAML binário).</summary>
	// Token: 0x020001CA RID: 458
	public enum LocalizationCategory
	{
		/// <summary>O recurso não pertence a uma categoria padrão.</summary>
		// Token: 0x04000709 RID: 1801
		None,
		/// <summary>Para um trecho de texto longo.</summary>
		// Token: 0x0400070A RID: 1802
		Text,
		/// <summary>Para uma única linha de texto, tal como o texto usado para um título.</summary>
		// Token: 0x0400070B RID: 1803
		Title,
		/// <summary>Um <see cref="T:System.Windows.Controls.Label" /> ou controle relacionado.</summary>
		// Token: 0x0400070C RID: 1804
		Label,
		/// <summary>Um <see cref="T:System.Windows.Controls.Button" /> ou controle relacionado.</summary>
		// Token: 0x0400070D RID: 1805
		Button,
		/// <summary>Um <see cref="T:System.Windows.Controls.CheckBox" /> ou controle relacionado.</summary>
		// Token: 0x0400070E RID: 1806
		CheckBox,
		/// <summary>Um <see cref="T:System.Windows.Controls.ComboBox" /> ou controle relacionado, tal como <see cref="T:System.Windows.Controls.ComboBoxItem" />.</summary>
		// Token: 0x0400070F RID: 1807
		ComboBox,
		/// <summary>Um <see cref="T:System.Windows.Controls.ListBox" /> ou controle relacionado, tal como <see cref="T:System.Windows.Controls.ListBoxItem" />.</summary>
		// Token: 0x04000710 RID: 1808
		ListBox,
		/// <summary>Um <see cref="T:System.Windows.Controls.Menu" /> ou controle relacionado, tal como <see cref="T:System.Windows.Controls.MenuItem" />.</summary>
		// Token: 0x04000711 RID: 1809
		Menu,
		/// <summary>Um <see cref="T:System.Windows.Controls.RadioButton" /> ou controle relacionado.</summary>
		// Token: 0x04000712 RID: 1810
		RadioButton,
		/// <summary>Um <see cref="T:System.Windows.Controls.ToolTip" /> ou controle relacionado.</summary>
		// Token: 0x04000713 RID: 1811
		ToolTip,
		/// <summary>Um <see cref="T:System.Windows.Documents.Hyperlink" /> ou controle relacionado.</summary>
		// Token: 0x04000714 RID: 1812
		Hyperlink,
		/// <summary>Para os painéis que podem conter texto.</summary>
		// Token: 0x04000715 RID: 1813
		TextFlow,
		/// <summary>Dados XML.</summary>
		// Token: 0x04000716 RID: 1814
		XmlData,
		/// <summary>Dados relacionados à fonte como nome, estilo ou tamanho da fonte.</summary>
		// Token: 0x04000717 RID: 1815
		Font,
		/// <summary>Herda sua categoria de um nó pai.</summary>
		// Token: 0x04000718 RID: 1816
		Inherit,
		/// <summary>Não localizar este recurso. Isso não se aplica a nenhum nó filho que possa existir.</summary>
		// Token: 0x04000719 RID: 1817
		Ignore,
		/// <summary>Não traduzir este recurso, nem nenhum nó filho cuja categoria for definida como Herdar.</summary>
		// Token: 0x0400071A RID: 1818
		NeverLocalize
	}
}
