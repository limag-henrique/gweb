using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Xaml;

namespace System.Windows.Diagnostics
{
	// Token: 0x0200031D RID: 797
	internal static class XamlSourceInfoHelper
	{
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x000679E4 File Offset: 0x00066DE4
		internal static bool IsXamlSourceInfoEnabled
		{
			get
			{
				return XamlSourceInfoHelper.s_sourceInfoTable != null;
			}
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x000679FC File Offset: 0x00066DFC
		static XamlSourceInfoHelper()
		{
			XamlSourceInfoHelper.InitializeEnableXamlSourceInfo(null);
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x00067A1C File Offset: 0x00066E1C
		[SecuritySafeCritical]
		private static void InitializeEnableXamlSourceInfo(string value)
		{
			if (VisualDiagnostics.IsEnabled && VisualDiagnostics.IsEnvironmentVariableSet(value, "ENABLE_XAML_DIAGNOSTICS_SOURCE_INFO") && XamlSourceInfoHelper.InitializeXamlObjectEventArgs())
			{
				XamlSourceInfoHelper.s_sourceInfoTable = new ConditionalWeakTable<object, XamlSourceInfo>();
				return;
			}
			XamlSourceInfoHelper.s_sourceInfoTable = null;
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x00067A58 File Offset: 0x00066E58
		private static bool InitializeXamlObjectEventArgs()
		{
			Type typeFromHandle = typeof(XamlObjectEventArgs);
			XamlSourceInfoHelper.s_sourceBamlUriProperty = typeFromHandle.GetProperty("SourceBamlUri", BindingFlags.Instance | BindingFlags.Public);
			XamlSourceInfoHelper.s_elementLineNumberProperty = typeFromHandle.GetProperty("ElementLineNumber", BindingFlags.Instance | BindingFlags.Public);
			XamlSourceInfoHelper.s_elementLinePositionProperty = typeFromHandle.GetProperty("ElementLinePosition", BindingFlags.Instance | BindingFlags.Public);
			return !(XamlSourceInfoHelper.s_sourceBamlUriProperty == null) && !(XamlSourceInfoHelper.s_sourceBamlUriProperty.PropertyType != typeof(Uri)) && !(XamlSourceInfoHelper.s_elementLineNumberProperty == null) && !(XamlSourceInfoHelper.s_elementLineNumberProperty.PropertyType != typeof(int)) && !(XamlSourceInfoHelper.s_elementLinePositionProperty == null) && !(XamlSourceInfoHelper.s_elementLinePositionProperty.PropertyType != typeof(int));
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x00067B24 File Offset: 0x00066F24
		internal static void SetXamlSourceInfo(object obj, XamlObjectEventArgs args, Uri overrideSourceUri)
		{
			if (XamlSourceInfoHelper.s_sourceInfoTable != null && args != null)
			{
				Uri sourceUri = overrideSourceUri ?? ((Uri)XamlSourceInfoHelper.s_sourceBamlUriProperty.GetValue(args));
				int elementLineNumber = (int)XamlSourceInfoHelper.s_elementLineNumberProperty.GetValue(args);
				int elementLinePosition = (int)XamlSourceInfoHelper.s_elementLinePositionProperty.GetValue(args);
				XamlSourceInfoHelper.SetXamlSourceInfo(obj, sourceUri, elementLineNumber, elementLinePosition);
			}
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x00067B7C File Offset: 0x00066F7C
		internal static void SetXamlSourceInfo(object obj, Uri sourceUri, int elementLineNumber, int elementLinePosition)
		{
			if (XamlSourceInfoHelper.s_sourceInfoTable != null && obj != null && (elementLineNumber != 0 || elementLinePosition != 0))
			{
				if (obj is string || obj.GetType().IsValueType)
				{
					return;
				}
				object obj2 = XamlSourceInfoHelper.s_lock;
				lock (obj2)
				{
					XamlSourceInfoHelper.s_sourceInfoTable.Remove(obj);
					XamlSourceInfoHelper.s_sourceInfoTable.Add(obj, new XamlSourceInfo(sourceUri, elementLineNumber, elementLinePosition));
				}
			}
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x00067C08 File Offset: 0x00067008
		internal static XamlSourceInfo GetXamlSourceInfo(object obj)
		{
			XamlSourceInfo result = null;
			if (XamlSourceInfoHelper.s_sourceInfoTable != null && obj != null && XamlSourceInfoHelper.s_sourceInfoTable.TryGetValue(obj, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x04000E0A RID: 3594
		private static ConditionalWeakTable<object, XamlSourceInfo> s_sourceInfoTable;

		// Token: 0x04000E0B RID: 3595
		private static object s_lock = new object();

		// Token: 0x04000E0C RID: 3596
		private static PropertyInfo s_sourceBamlUriProperty;

		// Token: 0x04000E0D RID: 3597
		private static PropertyInfo s_elementLineNumberProperty;

		// Token: 0x04000E0E RID: 3598
		private static PropertyInfo s_elementLinePositionProperty;
	}
}
