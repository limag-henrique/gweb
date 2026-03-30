using System;
using System.ComponentModel;
using System.Security;

namespace System.Windows.Input
{
	// Token: 0x02000223 RID: 547
	[TypeConverter("System.Windows.Input.CommandConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
	internal class SecureUICommand : RoutedUICommand, ISecureCommand, ICommand
	{
		// Token: 0x06000ED1 RID: 3793 RVA: 0x00038114 File Offset: 0x00037514
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal SecureUICommand(PermissionSet userInitiated, string name, Type ownerType, byte commandId) : base(name, ownerType, commandId)
		{
			this._userInitiated = userInitiated;
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x00038134 File Offset: 0x00037534
		public PermissionSet UserInitiatedPermission
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				return this._userInitiated;
			}
		}

		// Token: 0x04000859 RID: 2137
		[SecurityCritical]
		private readonly PermissionSet _userInitiated;
	}
}
