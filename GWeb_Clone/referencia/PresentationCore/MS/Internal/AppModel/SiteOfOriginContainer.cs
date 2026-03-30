using System;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.Security;
using System.Security.Permissions;
using System.Windows.Navigation;
using MS.Internal.PresentationCore;

namespace MS.Internal.AppModel
{
	// Token: 0x020007A7 RID: 1959
	internal class SiteOfOriginContainer : Package
	{
		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x0600524F RID: 21071 RVA: 0x00148434 File Offset: 0x00147834
		internal static Uri SiteOfOrigin
		{
			[FriendAccessAllowed]
			get
			{
				Uri uri = SiteOfOriginContainer.SiteOfOriginForClickOnceApp;
				if (uri == null)
				{
					uri = BaseUriHelper.FixFileUri(new Uri(AppDomain.CurrentDomain.BaseDirectory));
				}
				return uri;
			}
		}

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x06005250 RID: 21072 RVA: 0x00148468 File Offset: 0x00147868
		internal static Uri SiteOfOriginForClickOnceApp
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			[FriendAccessAllowed]
			get
			{
				if (SiteOfOriginContainer._siteOfOriginForClickOnceApp == null)
				{
					if (SiteOfOriginContainer._browserSource.Value != null)
					{
						SiteOfOriginContainer._siteOfOriginForClickOnceApp = new SecurityCriticalDataForSet<Uri>?(new SecurityCriticalDataForSet<Uri>(SiteOfOriginContainer._browserSource.Value));
					}
					else if (ApplicationDeployment.IsNetworkDeployed)
					{
						SiteOfOriginContainer._siteOfOriginForClickOnceApp = new SecurityCriticalDataForSet<Uri>?(new SecurityCriticalDataForSet<Uri>(SiteOfOriginContainer.GetDeploymentUri()));
					}
					else
					{
						SiteOfOriginContainer._siteOfOriginForClickOnceApp = new SecurityCriticalDataForSet<Uri>?(new SecurityCriticalDataForSet<Uri>(null));
					}
				}
				Invariant.Assert(SiteOfOriginContainer._siteOfOriginForClickOnceApp != null);
				return SiteOfOriginContainer._siteOfOriginForClickOnceApp.Value.Value;
			}
		}

		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x06005251 RID: 21073 RVA: 0x001484FC File Offset: 0x001478FC
		// (set) Token: 0x06005252 RID: 21074 RVA: 0x00148514 File Offset: 0x00147914
		internal static Uri BrowserSource
		{
			get
			{
				return SiteOfOriginContainer._browserSource.Value;
			}
			[SecurityCritical]
			[FriendAccessAllowed]
			set
			{
				SiteOfOriginContainer._browserSource.Value = value;
			}
		}

		// Token: 0x06005253 RID: 21075 RVA: 0x0014852C File Offset: 0x0014792C
		internal SiteOfOriginContainer() : base(FileAccess.Read)
		{
		}

		// Token: 0x06005254 RID: 21076 RVA: 0x00148540 File Offset: 0x00147940
		public override bool PartExists(Uri uri)
		{
			return true;
		}

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x06005255 RID: 21077 RVA: 0x00148550 File Offset: 0x00147950
		// (set) Token: 0x06005256 RID: 21078 RVA: 0x00148568 File Offset: 0x00147968
		internal static bool TraceSwitchEnabled
		{
			get
			{
				return SiteOfOriginContainer._traceSwitch.Enabled;
			}
			[SecurityTreatAsSafe]
			[SecurityCritical]
			set
			{
				SiteOfOriginContainer._traceSwitch.Enabled = value;
			}
		}

		// Token: 0x06005257 RID: 21079 RVA: 0x00148580 File Offset: 0x00147980
		protected override PackagePart GetPartCore(Uri uri)
		{
			return new SiteOfOriginPart(this, uri);
		}

		// Token: 0x06005258 RID: 21080 RVA: 0x00148594 File Offset: 0x00147994
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private static Uri GetDeploymentUri()
		{
			Invariant.Assert(ApplicationDeployment.IsNetworkDeployed);
			AppDomain currentDomain = AppDomain.CurrentDomain;
			ApplicationIdentity applicationIdentity = null;
			string uriString = null;
			SecurityPermission securityPermission = new SecurityPermission(SecurityPermissionFlag.ControlDomainPolicy);
			securityPermission.Assert();
			try
			{
				applicationIdentity = currentDomain.ApplicationIdentity;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			SecurityPermission securityPermission2 = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
			securityPermission2.Assert();
			try
			{
				uriString = applicationIdentity.CodeBase;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return new Uri(new Uri(uriString), new Uri(".", UriKind.Relative));
		}

		// Token: 0x06005259 RID: 21081 RVA: 0x00148640 File Offset: 0x00147A40
		protected override PackagePart CreatePartCore(Uri uri, string contentType, CompressionOption compressionOption)
		{
			return null;
		}

		// Token: 0x0600525A RID: 21082 RVA: 0x00148650 File Offset: 0x00147A50
		protected override void DeletePartCore(Uri uri)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600525B RID: 21083 RVA: 0x00148664 File Offset: 0x00147A64
		protected override PackagePart[] GetPartsCore()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600525C RID: 21084 RVA: 0x00148678 File Offset: 0x00147A78
		protected override void FlushCore()
		{
			throw new NotSupportedException();
		}

		// Token: 0x04002520 RID: 9504
		internal static BooleanSwitch _traceSwitch = new BooleanSwitch("SiteOfOrigin", "SiteOfOriginContainer and SiteOfOriginPart trace messages");

		// Token: 0x04002521 RID: 9505
		private static SecurityCriticalDataForSet<Uri> _browserSource;

		// Token: 0x04002522 RID: 9506
		private static SecurityCriticalDataForSet<Uri>? _siteOfOriginForClickOnceApp;
	}
}
