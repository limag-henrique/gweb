using System;

namespace System.Windows.Automation.Peers
{
	/// <summary>Especifica o <see cref="T:System.Windows.Automation.ControlType" /> exposto ao cliente de Automação de Interface do Usuário.</summary>
	// Token: 0x02000311 RID: 785
	public enum AutomationControlType
	{
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.Button" />.</summary>
		// Token: 0x04000DA0 RID: 3488
		Button,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.Calendar" />.</summary>
		// Token: 0x04000DA1 RID: 3489
		Calendar,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.CheckBox" />.</summary>
		// Token: 0x04000DA2 RID: 3490
		CheckBox,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.ComboBox" />.</summary>
		// Token: 0x04000DA3 RID: 3491
		ComboBox,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.Edit" />.</summary>
		// Token: 0x04000DA4 RID: 3492
		Edit,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.Hyperlink" />.</summary>
		// Token: 0x04000DA5 RID: 3493
		Hyperlink,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.Image" />.</summary>
		// Token: 0x04000DA6 RID: 3494
		Image,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.ListItem" />.</summary>
		// Token: 0x04000DA7 RID: 3495
		ListItem,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.List" />.</summary>
		// Token: 0x04000DA8 RID: 3496
		List,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.Menu" />.</summary>
		// Token: 0x04000DA9 RID: 3497
		Menu,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.MenuBar" />.</summary>
		// Token: 0x04000DAA RID: 3498
		MenuBar,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.MenuItem" />.</summary>
		// Token: 0x04000DAB RID: 3499
		MenuItem,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.ProgressBar" />.</summary>
		// Token: 0x04000DAC RID: 3500
		ProgressBar,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.RadioButton" />.</summary>
		// Token: 0x04000DAD RID: 3501
		RadioButton,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.ScrollBar" />.</summary>
		// Token: 0x04000DAE RID: 3502
		ScrollBar,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.Slider" />.</summary>
		// Token: 0x04000DAF RID: 3503
		Slider,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.Spinner" />.</summary>
		// Token: 0x04000DB0 RID: 3504
		Spinner,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.StatusBar" />.</summary>
		// Token: 0x04000DB1 RID: 3505
		StatusBar,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.Tab" />.</summary>
		// Token: 0x04000DB2 RID: 3506
		Tab,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.TabItem" />.</summary>
		// Token: 0x04000DB3 RID: 3507
		TabItem,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.Text" />.</summary>
		// Token: 0x04000DB4 RID: 3508
		Text,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.ToolBar" />.</summary>
		// Token: 0x04000DB5 RID: 3509
		ToolBar,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.ToolTip" />.</summary>
		// Token: 0x04000DB6 RID: 3510
		ToolTip,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.Tree" />.</summary>
		// Token: 0x04000DB7 RID: 3511
		Tree,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.TreeItem" />.</summary>
		// Token: 0x04000DB8 RID: 3512
		TreeItem,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.Custom" />.</summary>
		// Token: 0x04000DB9 RID: 3513
		Custom,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.Group" />.</summary>
		// Token: 0x04000DBA RID: 3514
		Group,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.Thumb" />.</summary>
		// Token: 0x04000DBB RID: 3515
		Thumb,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.DataGrid" />.</summary>
		// Token: 0x04000DBC RID: 3516
		DataGrid,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.DataItem" />.</summary>
		// Token: 0x04000DBD RID: 3517
		DataItem,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.Document" />.</summary>
		// Token: 0x04000DBE RID: 3518
		Document,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.SplitButton" />.</summary>
		// Token: 0x04000DBF RID: 3519
		SplitButton,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.Window" />.</summary>
		// Token: 0x04000DC0 RID: 3520
		Window,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.Pane" />.</summary>
		// Token: 0x04000DC1 RID: 3521
		Pane,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.Header" />.</summary>
		// Token: 0x04000DC2 RID: 3522
		Header,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.HeaderItem" />.</summary>
		// Token: 0x04000DC3 RID: 3523
		HeaderItem,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.Table" />.</summary>
		// Token: 0x04000DC4 RID: 3524
		Table,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.TitleBar" />.</summary>
		// Token: 0x04000DC5 RID: 3525
		TitleBar,
		/// <summary>Um tipo de controle <see cref="F:System.Windows.Automation.ControlType.Separator" />.</summary>
		// Token: 0x04000DC6 RID: 3526
		Separator
	}
}
