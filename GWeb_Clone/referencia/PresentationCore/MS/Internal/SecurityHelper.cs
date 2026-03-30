using System;
using System.ComponentModel;
using System.IO.Packaging;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Markup;
using System.Windows.Navigation;
using MS.Internal.AppModel;
using MS.Internal.Permissions;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace MS.Internal
{
	// Token: 0x02000671 RID: 1649
	internal static class SecurityHelper
	{
		// Token: 0x060048BA RID: 18618 RVA: 0x0011C4F8 File Offset: 0x0011B8F8
		[SecuritySafeCritical]
		internal static bool CheckUnmanagedCodePermission()
		{
			try
			{
				SecurityHelper.DemandUnmanagedCode();
			}
			catch (SecurityException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060048BB RID: 18619 RVA: 0x0011C530 File Offset: 0x0011B930
		[SecurityCritical]
		internal static void DemandUnmanagedCode()
		{
			if (SecurityHelper._unmanagedCodePermission == null)
			{
				SecurityHelper._unmanagedCodePermission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
			}
			SecurityHelper._unmanagedCodePermission.Demand();
		}

		// Token: 0x060048BC RID: 18620 RVA: 0x0011C55C File Offset: 0x0011B95C
		[SecurityCritical]
		internal static CodeAccessPermission CreateUserInitiatedRoutedEventPermission()
		{
			if (SecurityHelper._userInitiatedRoutedEventPermission == null)
			{
				SecurityHelper._userInitiatedRoutedEventPermission = new UserInitiatedRoutedEventPermission();
			}
			return SecurityHelper._userInitiatedRoutedEventPermission;
		}

		// Token: 0x060048BD RID: 18621 RVA: 0x0011C580 File Offset: 0x0011B980
		[SecuritySafeCritical]
		internal static bool CallerHasUserInitiatedRoutedEventPermission()
		{
			try
			{
				SecurityHelper.CreateUserInitiatedRoutedEventPermission().Demand();
			}
			catch (SecurityException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060048BE RID: 18622 RVA: 0x0011C5C0 File Offset: 0x0011B9C0
		[SecuritySafeCritical]
		internal static bool IsFullTrustCaller()
		{
			try
			{
				if (SecurityHelper._fullTrustPermissionSet == null)
				{
					SecurityHelper._fullTrustPermissionSet = new PermissionSet(PermissionState.Unrestricted);
				}
				SecurityHelper._fullTrustPermissionSet.Demand();
			}
			catch (SecurityException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060048BF RID: 18623 RVA: 0x0011C610 File Offset: 0x0011BA10
		[SecuritySafeCritical]
		internal static bool CallerHasPermissionWithAppDomainOptimization(params IPermission[] permissionsToCheck)
		{
			if (permissionsToCheck == null)
			{
				return true;
			}
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			for (int i = 0; i < permissionsToCheck.Length; i++)
			{
				permissionSet.AddPermission(permissionsToCheck[i]);
			}
			PermissionSet permissionSet2 = AppDomain.CurrentDomain.PermissionSet;
			return permissionSet.IsSubsetOf(permissionSet2);
		}

		// Token: 0x060048C0 RID: 18624 RVA: 0x0011C658 File Offset: 0x0011BA58
		[SecuritySafeCritical]
		internal static bool AppDomainHasPermission(IPermission permissionToCheck)
		{
			Invariant.Assert(permissionToCheck != null);
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(permissionToCheck);
			return permissionSet.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet);
		}

		// Token: 0x060048C1 RID: 18625 RVA: 0x0011C690 File Offset: 0x0011BA90
		[SecurityCritical]
		internal static Uri GetBaseDirectory(AppDomain domain)
		{
			Uri result = null;
			new FileIOPermission(PermissionState.Unrestricted).Assert();
			try
			{
				result = new Uri(domain.BaseDirectory);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x060048C2 RID: 18626 RVA: 0x0011C6DC File Offset: 0x0011BADC
		internal static Uri ExtractUriForClickOnceDeployedApp()
		{
			return SiteOfOriginContainer.SiteOfOriginForClickOnceApp;
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x0011C6F0 File Offset: 0x0011BAF0
		[SecurityCritical]
		internal static void BlockCrossDomainForHttpsApps(Uri uri)
		{
			Uri uri2 = SecurityHelper.ExtractUriForClickOnceDeployedApp();
			if (uri2 != null && uri2.Scheme == Uri.UriSchemeHttps)
			{
				if (uri.IsUnc || uri.IsFile)
				{
					new FileIOPermission(FileIOPermissionAccess.Read, uri.LocalPath).Demand();
					return;
				}
				new WebPermission(NetworkAccess.Connect, BindUriHelper.UriToString(uri)).Demand();
			}
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x0011C754 File Offset: 0x0011BB54
		[SecurityCritical]
		internal static void EnforceUncContentAccessRules(Uri contentUri)
		{
			Invariant.Assert(contentUri.IsUnc);
			Uri uri = SecurityHelper.ExtractUriForClickOnceDeployedApp();
			if (uri == null)
			{
				return;
			}
			int num = SecurityHelper.MapUrlToZoneWrapper(uri);
			bool flag = num >= 3;
			bool flag2 = num == 1 && uri.Scheme == Uri.UriSchemeHttps;
			if (flag || flag2)
			{
				new FileIOPermission(FileIOPermissionAccess.Read, contentUri.LocalPath).Demand();
			}
		}

		// Token: 0x060048C5 RID: 18629 RVA: 0x0011C7BC File Offset: 0x0011BBBC
		[SecurityCritical]
		internal static int MapUrlToZoneWrapper(Uri uri)
		{
			int num = 0;
			object obj = null;
			int num2 = UnsafeNativeMethods.CoInternetCreateSecurityManager(null, out obj, 0);
			if (NativeMethods.Failed(num2))
			{
				throw new Win32Exception(num2);
			}
			UnsafeNativeMethods.IInternetSecurityManager internetSecurityManager = (UnsafeNativeMethods.IInternetSecurityManager)obj;
			string pwszUrl = BindUriHelper.UriToString(uri);
			if (uri.IsFile)
			{
				internetSecurityManager.MapUrlToZone(pwszUrl, out num, 1);
			}
			else
			{
				internetSecurityManager.MapUrlToZone(pwszUrl, out num, 0);
			}
			if (num < 0)
			{
				throw new SecurityException(SR.Get("Invalid_URI"));
			}
			obj = null;
			return num;
		}

		// Token: 0x060048C6 RID: 18630 RVA: 0x0011C830 File Offset: 0x0011BC30
		[SecurityCritical]
		internal static void DemandFilePathDiscoveryWriteRead()
		{
			new FileIOPermission(PermissionState.None)
			{
				AllFiles = (FileIOPermissionAccess.Read | FileIOPermissionAccess.Write | FileIOPermissionAccess.PathDiscovery)
			}.Demand();
		}

		// Token: 0x060048C7 RID: 18631 RVA: 0x0011C854 File Offset: 0x0011BC54
		[SecurityCritical]
		internal static PermissionSet ExtractAppDomainPermissionSetMinusSiteOfOrigin()
		{
			PermissionSet permissionSet = AppDomain.CurrentDomain.PermissionSet;
			Uri siteOfOrigin = SiteOfOriginContainer.SiteOfOrigin;
			CodeAccessPermission codeAccessPermission = null;
			if (siteOfOrigin.Scheme == Uri.UriSchemeFile)
			{
				codeAccessPermission = new FileIOPermission(PermissionState.Unrestricted);
			}
			else if (siteOfOrigin.Scheme == Uri.UriSchemeHttp)
			{
				codeAccessPermission = new WebPermission(PermissionState.Unrestricted);
			}
			if (codeAccessPermission != null && permissionSet.GetPermission(codeAccessPermission.GetType()) != null)
			{
				permissionSet.RemovePermission(codeAccessPermission.GetType());
			}
			return permissionSet;
		}

		// Token: 0x060048C8 RID: 18632 RVA: 0x0011C8C8 File Offset: 0x0011BCC8
		[SecuritySafeCritical]
		internal static bool CallerHasSerializationPermission()
		{
			try
			{
				if (SecurityHelper._serializationSecurityPermission == null)
				{
					SecurityHelper._serializationSecurityPermission = new SecurityPermission(SecurityPermissionFlag.SerializationFormatter);
				}
				SecurityHelper._serializationSecurityPermission.Demand();
			}
			catch (SecurityException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060048C9 RID: 18633 RVA: 0x0011C91C File Offset: 0x0011BD1C
		[SecuritySafeCritical]
		internal static bool CallerHasAllClipboardPermission()
		{
			try
			{
				SecurityHelper.DemandAllClipboardPermission();
			}
			catch (SecurityException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060048CA RID: 18634 RVA: 0x0011C954 File Offset: 0x0011BD54
		[SecurityCritical]
		internal static void DemandAllClipboardPermission()
		{
			if (SecurityHelper._uiPermissionAllClipboard == null)
			{
				SecurityHelper._uiPermissionAllClipboard = new UIPermission(UIPermissionClipboard.AllClipboard);
			}
			SecurityHelper._uiPermissionAllClipboard.Demand();
		}

		// Token: 0x060048CB RID: 18635 RVA: 0x0011C980 File Offset: 0x0011BD80
		[SecurityCritical]
		internal static void DemandPathDiscovery(string path)
		{
			new FileIOPermission(FileIOPermissionAccess.PathDiscovery, path).Demand();
		}

		// Token: 0x060048CC RID: 18636 RVA: 0x0011C99C File Offset: 0x0011BD9C
		[SecuritySafeCritical]
		internal static bool CheckEnvironmentPermission()
		{
			try
			{
				SecurityHelper.DemandEnvironmentPermission();
			}
			catch (SecurityException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060048CD RID: 18637 RVA: 0x0011C9D4 File Offset: 0x0011BDD4
		[SecurityCritical]
		internal static void DemandEnvironmentPermission()
		{
			if (SecurityHelper._unrestrictedEnvironmentPermission == null)
			{
				SecurityHelper._unrestrictedEnvironmentPermission = new EnvironmentPermission(PermissionState.Unrestricted);
			}
			SecurityHelper._unrestrictedEnvironmentPermission.Demand();
		}

		// Token: 0x060048CE RID: 18638 RVA: 0x0011CA00 File Offset: 0x0011BE00
		[SecurityCritical]
		internal static void DemandUriDiscoveryPermission(Uri uri)
		{
			CodeAccessPermission codeAccessPermission = SecurityHelper.CreateUriDiscoveryPermission(uri);
			if (codeAccessPermission != null)
			{
				codeAccessPermission.Demand();
			}
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x0011CA20 File Offset: 0x0011BE20
		[SecurityCritical]
		internal static CodeAccessPermission CreateUriDiscoveryPermission(Uri uri)
		{
			if (uri.GetType().IsSubclassOf(typeof(Uri)))
			{
				SecurityHelper.DemandInfrastructurePermission();
			}
			if (uri.IsFile)
			{
				return new FileIOPermission(FileIOPermissionAccess.PathDiscovery, uri.LocalPath);
			}
			return null;
		}

		// Token: 0x060048D0 RID: 18640 RVA: 0x0011CA60 File Offset: 0x0011BE60
		[SecurityCritical]
		internal static CodeAccessPermission CreateUriReadPermission(Uri uri)
		{
			if (uri.GetType().IsSubclassOf(typeof(Uri)))
			{
				SecurityHelper.DemandInfrastructurePermission();
			}
			if (uri.IsFile)
			{
				return new FileIOPermission(FileIOPermissionAccess.Read, uri.LocalPath);
			}
			return null;
		}

		// Token: 0x060048D1 RID: 18641 RVA: 0x0011CAA0 File Offset: 0x0011BEA0
		[SecurityCritical]
		internal static void DemandUriReadPermission(Uri uri)
		{
			CodeAccessPermission codeAccessPermission = SecurityHelper.CreateUriReadPermission(uri);
			if (codeAccessPermission != null)
			{
				codeAccessPermission.Demand();
			}
		}

		// Token: 0x060048D2 RID: 18642 RVA: 0x0011CAC0 File Offset: 0x0011BEC0
		[SecuritySafeCritical]
		internal static bool CallerHasPathDiscoveryPermission(string path)
		{
			bool result;
			try
			{
				SecurityHelper.DemandPathDiscovery(path);
				result = true;
			}
			catch (SecurityException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x060048D3 RID: 18643 RVA: 0x0011CAFC File Offset: 0x0011BEFC
		internal static PermissionSet EnvelopePermissionSet
		{
			[SecurityCritical]
			get
			{
				if (SecurityHelper._envelopePermissionSet == null)
				{
					SecurityHelper._envelopePermissionSet = SecurityHelper.CreateEnvelopePermissionSet();
				}
				return SecurityHelper._envelopePermissionSet;
			}
		}

		// Token: 0x060048D4 RID: 18644 RVA: 0x0011CB20 File Offset: 0x0011BF20
		[SecurityCritical]
		private static PermissionSet CreateEnvelopePermissionSet()
		{
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(new RightsManagementPermission());
			permissionSet.AddPermission(new CompoundFileIOPermission());
			return permissionSet;
		}

		// Token: 0x060048D5 RID: 18645 RVA: 0x0011CB50 File Offset: 0x0011BF50
		[SecuritySafeCritical]
		internal static Exception GetExceptionForHR(int hr)
		{
			return Marshal.GetExceptionForHR(hr, new IntPtr(-1));
		}

		// Token: 0x060048D6 RID: 18646 RVA: 0x0011CB6C File Offset: 0x0011BF6C
		[SecuritySafeCritical]
		internal static void ThrowExceptionForHR(int hr)
		{
			Marshal.ThrowExceptionForHR(hr, new IntPtr(-1));
		}

		// Token: 0x060048D7 RID: 18647 RVA: 0x0011CB88 File Offset: 0x0011BF88
		[SecuritySafeCritical]
		internal static int GetHRForException(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			int hrforException = Marshal.GetHRForException(exception);
			Marshal.GetHRForException(new Exception());
			return hrforException;
		}

		// Token: 0x060048D8 RID: 18648 RVA: 0x0011CBB8 File Offset: 0x0011BFB8
		[SecurityCritical]
		internal static void DemandRegistryPermission()
		{
			if (SecurityHelper._unrestrictedRegistryPermission == null)
			{
				SecurityHelper._unrestrictedRegistryPermission = new RegistryPermission(PermissionState.Unrestricted);
			}
			SecurityHelper._unrestrictedRegistryPermission.Demand();
		}

		// Token: 0x060048D9 RID: 18649 RVA: 0x0011CBE4 File Offset: 0x0011BFE4
		[SecurityCritical]
		internal static void DemandUIWindowPermission()
		{
			if (SecurityHelper._allWindowsUIPermission == null)
			{
				SecurityHelper._allWindowsUIPermission = new UIPermission(UIPermissionWindow.AllWindows);
			}
			SecurityHelper._allWindowsUIPermission.Demand();
		}

		// Token: 0x060048DA RID: 18650 RVA: 0x0011CC10 File Offset: 0x0011C010
		[SecurityCritical]
		internal static void DemandInfrastructurePermission()
		{
			if (SecurityHelper._infrastructurePermission == null)
			{
				SecurityHelper._infrastructurePermission = new SecurityPermission(SecurityPermissionFlag.Infrastructure);
			}
			SecurityHelper._infrastructurePermission.Demand();
		}

		// Token: 0x060048DB RID: 18651 RVA: 0x0011CC40 File Offset: 0x0011C040
		[SecurityCritical]
		internal static void DemandMediaPermission(MediaPermissionAudio audioPermissionToDemand, MediaPermissionVideo videoPermissionToDemand, MediaPermissionImage imagePermissionToDemand)
		{
			new MediaPermission(audioPermissionToDemand, videoPermissionToDemand, imagePermissionToDemand).Demand();
		}

		// Token: 0x060048DC RID: 18652 RVA: 0x0011CC5C File Offset: 0x0011C05C
		[SecuritySafeCritical]
		internal static bool CallerHasMediaPermission(MediaPermissionAudio audioPermissionToDemand, MediaPermissionVideo videoPermissionToDemand, MediaPermissionImage imagePermissionToDemand)
		{
			bool result;
			try
			{
				new MediaPermission(audioPermissionToDemand, videoPermissionToDemand, imagePermissionToDemand).Demand();
				result = true;
			}
			catch (SecurityException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060048DD RID: 18653 RVA: 0x0011CC9C File Offset: 0x0011C09C
		[SecurityCritical]
		internal static void DemandUnrestrictedUIPermission()
		{
			if (SecurityHelper._unrestrictedUIPermission == null)
			{
				SecurityHelper._unrestrictedUIPermission = new UIPermission(PermissionState.Unrestricted);
			}
			SecurityHelper._unrestrictedUIPermission.Demand();
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x060048DE RID: 18654 RVA: 0x0011CCC8 File Offset: 0x0011C0C8
		internal static bool AppDomainGrantedUnrestrictedUIPermission
		{
			[SecurityCritical]
			get
			{
				if (SecurityHelper._appDomainGrantedUnrestrictedUIPermission == null)
				{
					SecurityHelper._appDomainGrantedUnrestrictedUIPermission = new bool?(SecurityHelper.AppDomainHasPermission(new UIPermission(PermissionState.Unrestricted)));
				}
				return SecurityHelper._appDomainGrantedUnrestrictedUIPermission.Value;
			}
		}

		// Token: 0x060048DF RID: 18655 RVA: 0x0011CD00 File Offset: 0x0011C100
		[SecurityCritical]
		internal static void DemandFileIOReadPermission(string fileName)
		{
			new FileIOPermission(FileIOPermissionAccess.Read, fileName).Demand();
		}

		// Token: 0x060048E0 RID: 18656 RVA: 0x0011CD1C File Offset: 0x0011C11C
		[SecurityCritical]
		internal static void DemandMediaAccessPermission(string uri)
		{
			CodeAccessPermission codeAccessPermission = SecurityHelper.CreateMediaAccessPermission(uri);
			if (codeAccessPermission != null)
			{
				codeAccessPermission.Demand();
			}
		}

		// Token: 0x060048E1 RID: 18657 RVA: 0x0011CD3C File Offset: 0x0011C13C
		[SecurityCritical]
		internal static CodeAccessPermission CreateMediaAccessPermission(string uri)
		{
			CodeAccessPermission result = null;
			if (uri != null)
			{
				if (string.Compare("image", uri, true, TypeConverterHelper.InvariantEnglishUS) == 0)
				{
					result = new MediaPermission(MediaPermissionAudio.NoAudio, MediaPermissionVideo.NoVideo, MediaPermissionImage.AllImage);
				}
				else if (string.Compare(BaseUriHelper.GetResolvedUri(BaseUriHelper.BaseUri, new Uri(uri, UriKind.RelativeOrAbsolute)).Scheme, PackUriHelper.UriSchemePack, true, TypeConverterHelper.InvariantEnglishUS) != 0 && !SecurityHelper.CallerHasWebPermission(new Uri(uri, UriKind.RelativeOrAbsolute)))
				{
					result = new MediaPermission(MediaPermissionAudio.NoAudio, MediaPermissionVideo.NoVideo, MediaPermissionImage.AllImage);
				}
			}
			else
			{
				result = new MediaPermission(MediaPermissionAudio.NoAudio, MediaPermissionVideo.NoVideo, MediaPermissionImage.AllImage);
			}
			return result;
		}

		// Token: 0x060048E2 RID: 18658 RVA: 0x0011CDB8 File Offset: 0x0011C1B8
		[SecuritySafeCritical]
		internal static bool CallerHasWebPermission(Uri uri)
		{
			bool result;
			try
			{
				SecurityHelper.DemandWebPermission(uri);
				result = true;
			}
			catch (SecurityException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060048E3 RID: 18659 RVA: 0x0011CDF4 File Offset: 0x0011C1F4
		[SecurityCritical]
		internal static void DemandWebPermission(Uri uri)
		{
			string uriString = BindUriHelper.UriToString(uri);
			if (uri.IsFile)
			{
				string localPath = uri.LocalPath;
				new FileIOPermission(FileIOPermissionAccess.Read, localPath).Demand();
				return;
			}
			new WebPermission(NetworkAccess.Connect, uriString).Demand();
		}

		// Token: 0x060048E4 RID: 18660 RVA: 0x0011CE34 File Offset: 0x0011C234
		[SecurityCritical]
		internal static void DemandPlugInSerializerPermissions()
		{
			if (SecurityHelper._plugInSerializerPermissions == null)
			{
				SecurityHelper._plugInSerializerPermissions = new PermissionSet(PermissionState.Unrestricted);
			}
			SecurityHelper._plugInSerializerPermissions.Demand();
		}

		// Token: 0x060048E5 RID: 18661 RVA: 0x0011CE60 File Offset: 0x0011C260
		internal static bool AreStringTypesEqual(string m1, string m2)
		{
			return string.Compare(m1, m2, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x04001CA1 RID: 7329
		private static SecurityPermission _unmanagedCodePermission;

		// Token: 0x04001CA2 RID: 7330
		private static UserInitiatedRoutedEventPermission _userInitiatedRoutedEventPermission;

		// Token: 0x04001CA3 RID: 7331
		private static PermissionSet _fullTrustPermissionSet;

		// Token: 0x04001CA4 RID: 7332
		private static SecurityPermission _serializationSecurityPermission;

		// Token: 0x04001CA5 RID: 7333
		private static UIPermission _uiPermissionAllClipboard;

		// Token: 0x04001CA6 RID: 7334
		private static EnvironmentPermission _unrestrictedEnvironmentPermission;

		// Token: 0x04001CA7 RID: 7335
		private static PermissionSet _envelopePermissionSet;

		// Token: 0x04001CA8 RID: 7336
		private static RegistryPermission _unrestrictedRegistryPermission;

		// Token: 0x04001CA9 RID: 7337
		private static UIPermission _allWindowsUIPermission;

		// Token: 0x04001CAA RID: 7338
		private static SecurityPermission _infrastructurePermission;

		// Token: 0x04001CAB RID: 7339
		private static UIPermission _unrestrictedUIPermission;

		// Token: 0x04001CAC RID: 7340
		[SecurityCritical]
		private static bool? _appDomainGrantedUnrestrictedUIPermission;

		// Token: 0x04001CAD RID: 7341
		private static PermissionSet _plugInSerializerPermissions;
	}
}
