using System;
using System.ComponentModel;
using System.Security;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	// Token: 0x02000215 RID: 533
	[FriendAccessAllowed]
	[TypeConverter("System.Windows.Input.CommandConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
	internal interface ISecureCommand : ICommand
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000E4F RID: 3663
		PermissionSet UserInitiatedPermission { get; }
	}
}
