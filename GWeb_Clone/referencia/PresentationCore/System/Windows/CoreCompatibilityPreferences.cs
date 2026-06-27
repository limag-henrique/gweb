using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Runtime.Versioning;
using MS.Internal.PresentationCore;

namespace System.Windows
{
	/// <summary>Contém propriedades que especificam como um aplicativo deve se comportar em relação a recursos WPF que estão no assembly PresentationCore.</summary>
	// Token: 0x02000190 RID: 400
	public static class CoreCompatibilityPreferences
	{
		// Token: 0x06000573 RID: 1395 RVA: 0x00019A84 File Offset: 0x00018E84
		static CoreCompatibilityPreferences()
		{
			NameValueCollection nameValueCollection = null;
			try
			{
				nameValueCollection = ConfigurationManager.AppSettings;
			}
			catch (ConfigurationErrorsException)
			{
			}
			if (nameValueCollection != null)
			{
				CoreCompatibilityPreferences.SetIncludeAllInkInBoundingBoxFromAppSettings(nameValueCollection);
				CoreCompatibilityPreferences.SetEnableMultiMonitorDisplayClippingFromAppSettings(nameValueCollection);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00019AEC File Offset: 0x00018EEC
		internal static bool TargetsAtLeast_Desktop_V4_5
		{
			get
			{
				return BinaryCompatibility.TargetsAtLeast_Desktop_V4_5;
			}
		}

		/// <summary>Obtém ou define um valor que indica se o usuário precisa usar a tecla ALT para invocar um atalho.</summary>
		/// <returns>
		///   <see langword="true" /> Se o usuário precisa usar a tecla ALT para invocar um atalho; Caso contrário, <see langword="false" />.  O padrão é <see langword="false" />.</returns>
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00019B00 File Offset: 0x00018F00
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x00019B14 File Offset: 0x00018F14
		public static bool IsAltKeyRequiredInAccessKeyDefaultScope
		{
			get
			{
				return CoreCompatibilityPreferences._isAltKeyRequiredInAccessKeyDefaultScope;
			}
			set
			{
				object lockObject = CoreCompatibilityPreferences._lockObject;
				lock (lockObject)
				{
					if (CoreCompatibilityPreferences._isSealed)
					{
						throw new InvalidOperationException(SR.Get("CompatibilityPreferencesSealed", new object[]
						{
							"IsAltKeyRequiredInAccessKeyDefaultScope",
							"CoreCompatibilityPreferences"
						}));
					}
					CoreCompatibilityPreferences._isAltKeyRequiredInAccessKeyDefaultScope = value;
				}
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00019B8C File Offset: 0x00018F8C
		internal static bool GetIsAltKeyRequiredInAccessKeyDefaultScope()
		{
			CoreCompatibilityPreferences.Seal();
			return CoreCompatibilityPreferences.IsAltKeyRequiredInAccessKeyDefaultScope;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00019BA4 File Offset: 0x00018FA4
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x00019BB8 File Offset: 0x00018FB8
		internal static bool IncludeAllInkInBoundingBox
		{
			get
			{
				return CoreCompatibilityPreferences._includeAllInkInBoundingBox;
			}
			set
			{
				object lockObject = CoreCompatibilityPreferences._lockObject;
				lock (lockObject)
				{
					if (CoreCompatibilityPreferences._isSealed)
					{
						throw new InvalidOperationException(SR.Get("CompatibilityPreferencesSealed", new object[]
						{
							"IncludeAllInkInBoundingBox",
							"CoreCompatibilityPreferences"
						}));
					}
					CoreCompatibilityPreferences._includeAllInkInBoundingBox = value;
				}
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00019C30 File Offset: 0x00019030
		internal static bool GetIncludeAllInkInBoundingBox()
		{
			CoreCompatibilityPreferences.Seal();
			return CoreCompatibilityPreferences.IncludeAllInkInBoundingBox;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00019C48 File Offset: 0x00019048
		private static void SetIncludeAllInkInBoundingBoxFromAppSettings(NameValueCollection appSettings)
		{
			string value = appSettings["IncludeAllInkInBoundingBox"];
			bool includeAllInkInBoundingBox;
			if (bool.TryParse(value, out includeAllInkInBoundingBox))
			{
				CoreCompatibilityPreferences.IncludeAllInkInBoundingBox = includeAllInkInBoundingBox;
			}
		}

		/// <summary>Obtém ou define um valor que indica se é preciso habilitar o recorte em uma exibição de vários monitores.</summary>
		/// <returns>
		///   <see langword="true" /> Para habilitar o recorte em uma exibição de vários monitor; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00019C74 File Offset: 0x00019074
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x00019C88 File Offset: 0x00019088
		public static bool? EnableMultiMonitorDisplayClipping
		{
			get
			{
				return CoreCompatibilityPreferences.GetEnableMultiMonitorDisplayClipping();
			}
			set
			{
				object lockObject = CoreCompatibilityPreferences._lockObject;
				lock (lockObject)
				{
					if (CoreCompatibilityPreferences._isSealed)
					{
						throw new InvalidOperationException(SR.Get("CompatibilityPreferencesSealed", new object[]
						{
							"DisableMultimonDisplayClipping",
							"CoreCompatibilityPreferences"
						}));
					}
					CoreCompatibilityPreferences._enableMultiMonitorDisplayClipping = value;
				}
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00019D00 File Offset: 0x00019100
		internal static bool? GetEnableMultiMonitorDisplayClipping()
		{
			CoreCompatibilityPreferences.Seal();
			return CoreCompatibilityPreferences._enableMultiMonitorDisplayClipping;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00019D18 File Offset: 0x00019118
		private static void SetEnableMultiMonitorDisplayClippingFromAppSettings(NameValueCollection appSettings)
		{
			string value = appSettings["EnableMultiMonitorDisplayClipping"];
			bool value2;
			if (bool.TryParse(value, out value2))
			{
				CoreCompatibilityPreferences.EnableMultiMonitorDisplayClipping = new bool?(value2);
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00019D48 File Offset: 0x00019148
		private static void Seal()
		{
			if (!CoreCompatibilityPreferences._isSealed)
			{
				object lockObject = CoreCompatibilityPreferences._lockObject;
				lock (lockObject)
				{
					CoreCompatibilityPreferences._isSealed = true;
				}
			}
		}

		// Token: 0x0400052E RID: 1326
		private static bool _isAltKeyRequiredInAccessKeyDefaultScope = false;

		// Token: 0x0400052F RID: 1327
		private static bool _includeAllInkInBoundingBox = true;

		// Token: 0x04000530 RID: 1328
		private static bool? _enableMultiMonitorDisplayClipping = null;

		// Token: 0x04000531 RID: 1329
		private static bool _isSealed;

		// Token: 0x04000532 RID: 1330
		private static object _lockObject = new object();
	}
}
