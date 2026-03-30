using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace MS.Internal.AppModel
{
	// Token: 0x020007A6 RID: 1958
	[FriendAccessAllowed]
	internal class CustomCredentialPolicy : ICredentialPolicy
	{
		// Token: 0x06005249 RID: 21065 RVA: 0x00148228 File Offset: 0x00147628
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public CustomCredentialPolicy()
		{
			this._environmentPermissionSet = new PermissionSet(null);
			this._environmentPermissionSet.AddPermission(new EnvironmentPermission(EnvironmentPermissionAccess.Read, "USERDOMAIN"));
			this._environmentPermissionSet.AddPermission(new EnvironmentPermission(EnvironmentPermissionAccess.Read, "USERNAME"));
		}

		// Token: 0x0600524A RID: 21066 RVA: 0x00148278 File Offset: 0x00147678
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static void EnsureCustomCredentialPolicy()
		{
			if (!CustomCredentialPolicy._initialized)
			{
				object lockObj = CustomCredentialPolicy._lockObj;
				lock (lockObj)
				{
					if (!CustomCredentialPolicy._initialized)
					{
						new SecurityPermission(SecurityPermissionFlag.ControlPolicy).Assert();
						try
						{
							if (AuthenticationManager.CredentialPolicy == null)
							{
								AuthenticationManager.CredentialPolicy = new CustomCredentialPolicy();
							}
							CustomCredentialPolicy._initialized = true;
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
				}
			}
		}

		// Token: 0x0600524B RID: 21067 RVA: 0x00148310 File Offset: 0x00147710
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public bool ShouldSendCredential(Uri challengeUri, WebRequest request, NetworkCredential credential, IAuthenticationModule authenticationModule)
		{
			SecurityZone securityZone = CustomCredentialPolicy.MapUrlToZone(challengeUri);
			if (securityZone > SecurityZone.Trusted)
			{
				if (securityZone - SecurityZone.Internet > 1)
				{
				}
				return !this.IsDefaultCredentials(credential);
			}
			return true;
		}

		// Token: 0x0600524C RID: 21068 RVA: 0x0014833C File Offset: 0x0014773C
		[SecurityCritical]
		private bool IsDefaultCredentials(NetworkCredential credential)
		{
			this._environmentPermissionSet.Assert();
			bool result;
			try
			{
				result = (credential == CredentialCache.DefaultCredentials);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x0600524D RID: 21069 RVA: 0x00148384 File Offset: 0x00147784
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static SecurityZone MapUrlToZone(Uri uri)
		{
			CustomCredentialPolicy.EnsureSecurityManager();
			int num;
			CustomCredentialPolicy._securityManager.MapUrlToZone(BindUriHelper.UriToString(uri), out num, 0);
			switch (num)
			{
			case 0:
				return SecurityZone.MyComputer;
			case 1:
				return SecurityZone.Intranet;
			case 2:
				return SecurityZone.Trusted;
			case 3:
				return SecurityZone.Internet;
			case 4:
				return SecurityZone.Untrusted;
			default:
				return SecurityZone.NoZone;
			}
		}

		// Token: 0x0600524E RID: 21070 RVA: 0x001483D0 File Offset: 0x001477D0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private static void EnsureSecurityManager()
		{
			if (CustomCredentialPolicy._securityManager == null)
			{
				object lockObj = CustomCredentialPolicy._lockObj;
				lock (lockObj)
				{
					if (CustomCredentialPolicy._securityManager == null)
					{
						CustomCredentialPolicy._securityManager = (UnsafeNativeMethods.IInternetSecurityManager)new CustomCredentialPolicy.InternetSecurityManager();
					}
				}
			}
		}

		// Token: 0x0400251C RID: 9500
		[SecurityCritical]
		private static UnsafeNativeMethods.IInternetSecurityManager _securityManager;

		// Token: 0x0400251D RID: 9501
		[SecurityCritical]
		private static object _lockObj = new object();

		// Token: 0x0400251E RID: 9502
		[SecurityCritical]
		private static bool _initialized = false;

		// Token: 0x0400251F RID: 9503
		[SecurityCritical]
		private PermissionSet _environmentPermissionSet;

		// Token: 0x020009FA RID: 2554
		[ComVisible(false)]
		[Guid("7b8a2d94-0ac9-11d1-896c-00c04Fb6bfc4")]
		[ComImport]
		private class InternetSecurityManager
		{
			// Token: 0x06005BDF RID: 23519
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern InternetSecurityManager();
		}
	}
}
