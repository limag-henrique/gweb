using System;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Microsoft.Win32;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Diagnostics
{
	/// <summary>Habilita ferramentas para analisar árvores visuais XAML.</summary>
	// Token: 0x02000319 RID: 793
	public static class VisualDiagnostics
	{
		// Token: 0x14000171 RID: 369
		// (add) Token: 0x06001A1C RID: 6684 RVA: 0x0006754C File Offset: 0x0006694C
		// (remove) Token: 0x06001A1D RID: 6685 RVA: 0x00067580 File Offset: 0x00066980
		private static event EventHandler<VisualTreeChangeEventArgs> s_visualTreeChanged;

		/// <summary>Ocorre quando um depurador gerenciado é anexado e qualquer elemento é adicionado ou removido da árvore visual de outro elemento, independentemente de um dos elementos serem descendentes de algum outro elemento específico.</summary>
		// Token: 0x14000172 RID: 370
		// (add) Token: 0x06001A1F RID: 6687 RVA: 0x000675D0 File Offset: 0x000669D0
		// (remove) Token: 0x06001A20 RID: 6688 RVA: 0x000675F0 File Offset: 0x000669F0
		public static event EventHandler<VisualTreeChangeEventArgs> VisualTreeChanged
		{
			add
			{
				if (VisualDiagnostics.EnableHelper.IsVisualTreeChangeEnabled)
				{
					VisualDiagnostics.s_visualTreeChanged += value;
					VisualDiagnostics.s_HasVisualTreeChangedListeners = true;
				}
			}
			remove
			{
				VisualDiagnostics.s_visualTreeChanged -= value;
			}
		}

		/// <summary>Habilita o evento <see cref="E:System.Windows.Diagnostics.VisualDiagnostics.VisualTreeChanged" />.</summary>
		// Token: 0x06001A21 RID: 6689 RVA: 0x00067604 File Offset: 0x00066A04
		public static void EnableVisualTreeChanged()
		{
			VisualDiagnostics.EnableHelper.EnableVisualTreeChanged();
		}

		/// <summary>Desabilita o evento <see cref="E:System.Windows.Diagnostics.VisualDiagnostics.VisualTreeChanged" />.</summary>
		// Token: 0x06001A22 RID: 6690 RVA: 0x00067618 File Offset: 0x00066A18
		public static void DisableVisualTreeChanged()
		{
			VisualDiagnostics.EnableHelper.DisableVisualTreeChanged();
		}

		/// <summary>Retorna uma instância de <see cref="T:System.Windows.Diagnostics.XamlSourceInfo" /> que contém informações sobre o documento XAML de origem XAML para o objeto especificado.</summary>
		/// <param name="obj">O objeto para o qual a origem XAML deve ser localizada.</param>
		/// <returns>Informações sobre o documento de origem XAML, se disponível, caso contrário, <see langword="null" />.</returns>
		// Token: 0x06001A23 RID: 6691 RVA: 0x0006762C File Offset: 0x00066A2C
		public static XamlSourceInfo GetXamlSourceInfo(object obj)
		{
			return XamlSourceInfoHelper.GetXamlSourceInfo(obj);
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x00067640 File Offset: 0x00066A40
		internal static void OnVisualChildChanged(DependencyObject parent, DependencyObject child, bool isAdded)
		{
			EventHandler<VisualTreeChangeEventArgs> eventHandler = VisualDiagnostics.s_visualTreeChanged;
			if (eventHandler != null && VisualDiagnostics.EnableHelper.IsVisualTreeChangeEnabled)
			{
				int num;
				VisualTreeChangeType visualTreeChangeType;
				if (isAdded)
				{
					num = VisualDiagnostics.GetChildIndex(parent, child);
					visualTreeChangeType = VisualTreeChangeType.Add;
				}
				else
				{
					num = -1;
					visualTreeChangeType = VisualTreeChangeType.Remove;
				}
				VisualDiagnostics.RaiseVisualTreeChangedEvent(eventHandler, new VisualTreeChangeEventArgs(parent, child, num, visualTreeChangeType), visualTreeChangeType == VisualTreeChangeType.Add && num == 0 && VisualTreeHelper.GetParent(parent) == null);
			}
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x00067694 File Offset: 0x00066A94
		[SecuritySafeCritical]
		private static void RaiseVisualTreeChangedEvent(EventHandler<VisualTreeChangeEventArgs> visualTreeChanged, VisualTreeChangeEventArgs args, bool isPotentialOuterChange)
		{
			bool flag = VisualDiagnostics.s_IsVisualTreeChangedInProgress;
			HwndSource hwndSource = VisualDiagnostics.s_ActiveHwndSource;
			try
			{
				VisualDiagnostics.s_IsVisualTreeChangedInProgress = true;
				if (isPotentialOuterChange)
				{
					VisualDiagnostics.s_ActiveHwndSource = (PresentationSource.FromDependencyObject(args.Parent) as HwndSource);
				}
				visualTreeChanged(null, args);
			}
			finally
			{
				VisualDiagnostics.s_IsVisualTreeChangedInProgress = flag;
				VisualDiagnostics.s_ActiveHwndSource = hwndSource;
			}
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x00067700 File Offset: 0x00066B00
		private static int GetChildIndex(DependencyObject parent, DependencyObject child)
		{
			int num = -1;
			Visual visual = child as Visual;
			if (visual != null)
			{
				num = visual._parentIndex;
			}
			else
			{
				Visual3D visual3D = child as Visual3D;
				if (visual3D != null)
				{
					num = visual3D.ParentIndex;
				}
			}
			if (num < 0)
			{
				int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
				for (int i = 0; i < childrenCount; i++)
				{
					DependencyObject child2 = VisualTreeHelper.GetChild(parent, i);
					if (child2 == child)
					{
						num = i;
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001A27 RID: 6695 RVA: 0x00067760 File Offset: 0x00066B60
		internal static bool IsEnabled
		{
			get
			{
				return VisualDiagnostics.s_IsEnabled;
			}
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00067774 File Offset: 0x00066B74
		internal static void VerifyVisualTreeChange(DependencyObject d)
		{
			if (VisualDiagnostics.s_HasVisualTreeChangedListeners)
			{
				VisualDiagnostics.VerifyVisualTreeChangeCore(d);
			}
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x00067790 File Offset: 0x00066B90
		private static void VerifyVisualTreeChangeCore(DependencyObject d)
		{
			if (VisualDiagnostics.s_IsVisualTreeChangedInProgress && !VisualDiagnostics.EnableHelper.AllowChangesDuringVisualTreeChanged(d))
			{
				throw new InvalidOperationException(SR.Get("ReentrantVisualTreeChangeError", new object[]
				{
					"VisualTreeChanged"
				}));
			}
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x000677CC File Offset: 0x00066BCC
		[SecurityCritical]
		internal static bool IsEnvironmentVariableSet(string value, string environmentVariable)
		{
			if (value != null)
			{
				return VisualDiagnostics.IsEnvironmentValueSet(value);
			}
			new EnvironmentPermission(EnvironmentPermissionAccess.Read, environmentVariable).Assert();
			try
			{
				value = Environment.GetEnvironmentVariable(environmentVariable);
			}
			finally
			{
				CodeAccessPermission.RevertAll();
			}
			return VisualDiagnostics.IsEnvironmentValueSet(value);
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00067824 File Offset: 0x00066C24
		internal static bool IsEnvironmentValueSet(string value)
		{
			value = (value ?? string.Empty).Trim().ToLowerInvariant();
			return !(value == string.Empty) && !(value == "0") && !(value == "false");
		}

		// Token: 0x04000DFA RID: 3578
		private static bool s_isDebuggerCheckDisabledForTestPurposes;

		// Token: 0x04000DFB RID: 3579
		private static readonly bool s_IsEnabled = !CoreAppContextSwitches.DisableDiagnostics;

		// Token: 0x04000DFD RID: 3581
		private static bool s_HasVisualTreeChangedListeners;

		// Token: 0x04000DFE RID: 3582
		[ThreadStatic]
		private static bool s_IsVisualTreeChangedInProgress;

		// Token: 0x04000DFF RID: 3583
		[SecurityCritical]
		[ThreadStatic]
		private static HwndSource s_ActiveHwndSource;

		// Token: 0x02000849 RID: 2121
		private static class EnableHelper
		{
			// Token: 0x060056D9 RID: 22233 RVA: 0x00163A5C File Offset: 0x00162E5C
			static EnableHelper()
			{
				if (VisualDiagnostics.IsEnabled)
				{
					VisualDiagnostics.EnableHelper.s_IsDevMode = VisualDiagnostics.EnableHelper.GetDevModeFromRegistry();
					VisualDiagnostics.EnableHelper.s_IsEnableVisualTreeChangedAllowed = VisualDiagnostics.EnableHelper.PrecomputeIsEnableVisualTreeChangedAllowed();
				}
			}

			// Token: 0x060056DA RID: 22234 RVA: 0x00163A84 File Offset: 0x00162E84
			internal static void EnableVisualTreeChanged()
			{
				if (!VisualDiagnostics.EnableHelper.IsEnableVisualTreeChangedAllowed)
				{
					throw new InvalidOperationException(SR.Get("MethodCallNotAllowed", new object[]
					{
						"EnableVisualTreeChanged"
					}));
				}
				VisualDiagnostics.EnableHelper.s_IsVisualTreeChangedEnabled = true;
			}

			// Token: 0x060056DB RID: 22235 RVA: 0x00163ABC File Offset: 0x00162EBC
			internal static void DisableVisualTreeChanged()
			{
				VisualDiagnostics.EnableHelper.s_IsVisualTreeChangedEnabled = false;
			}

			// Token: 0x170011DF RID: 4575
			// (get) Token: 0x060056DC RID: 22236 RVA: 0x00163AD0 File Offset: 0x00162ED0
			internal static bool IsVisualTreeChangeEnabled
			{
				get
				{
					return VisualDiagnostics.IsEnabled && (VisualDiagnostics.EnableHelper.s_IsVisualTreeChangedEnabled || Debugger.IsAttached || VisualDiagnostics.s_isDebuggerCheckDisabledForTestPurposes);
				}
			}

			// Token: 0x060056DD RID: 22237 RVA: 0x00163AFC File Offset: 0x00162EFC
			internal static bool AllowChangesDuringVisualTreeChanged(DependencyObject d)
			{
				bool? flag;
				bool flag2;
				if (VisualDiagnostics.EnableHelper.s_AllowChangesDuringVisualTreeChanged != null)
				{
					flag = VisualDiagnostics.EnableHelper.s_AllowChangesDuringVisualTreeChanged;
					flag2 = true;
					return (flag.GetValueOrDefault() == flag2 & flag != null) || VisualDiagnostics.EnableHelper.IsChangePermitted(d);
				}
				if (VisualDiagnostics.EnableHelper.IsChangePermitted(d))
				{
					return true;
				}
				VisualDiagnostics.EnableHelper.s_AllowChangesDuringVisualTreeChanged = new bool?(CoreAppContextSwitches.AllowChangesDuringVisualTreeChanged);
				flag = VisualDiagnostics.EnableHelper.s_AllowChangesDuringVisualTreeChanged;
				flag2 = true;
				bool flag3 = flag.GetValueOrDefault() == flag2 & flag != null;
				flag = VisualDiagnostics.EnableHelper.s_AllowChangesDuringVisualTreeChanged;
				flag2 = true;
				return flag.GetValueOrDefault() == flag2 & flag != null;
			}

			// Token: 0x060056DE RID: 22238 RVA: 0x00163B88 File Offset: 0x00162F88
			[SecuritySafeCritical]
			private static bool IsChangePermitted(DependencyObject d)
			{
				return VisualDiagnostics.s_ActiveHwndSource != null && d != null && VisualDiagnostics.s_ActiveHwndSource != PresentationSource.FromDependencyObject(d);
			}

			// Token: 0x060056DF RID: 22239 RVA: 0x00163BB4 File Offset: 0x00162FB4
			[SecuritySafeCritical]
			private static bool? PrecomputeIsEnableVisualTreeChangedAllowed()
			{
				if (!VisualDiagnostics.IsEnabled)
				{
					return new bool?(false);
				}
				if (VisualDiagnostics.EnableHelper.IsDevMode)
				{
					return new bool?(true);
				}
				if (VisualDiagnostics.IsEnvironmentVariableSet(null, "ENABLE_XAML_DIAGNOSTICS_VISUAL_TREE_NOTIFICATIONS"))
				{
					return new bool?(true);
				}
				return null;
			}

			// Token: 0x060056E0 RID: 22240 RVA: 0x00163BFC File Offset: 0x00162FFC
			[SecuritySafeCritical]
			private static bool GetDevModeFromRegistry()
			{
				new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\AppModelUnlock").Assert();
				try
				{
					RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\AppModelUnlock");
					if (registryKey != null)
					{
						using (registryKey)
						{
							object value = registryKey.GetValue("AllowDevelopmentWithoutDevLicense");
							if (value is int)
							{
								return (int)value != 0;
							}
						}
					}
				}
				finally
				{
					CodeAccessPermission.RevertAll();
				}
				return false;
			}

			// Token: 0x170011E0 RID: 4576
			// (get) Token: 0x060056E1 RID: 22241 RVA: 0x00163C9C File Offset: 0x0016309C
			private static bool IsDevMode
			{
				get
				{
					return VisualDiagnostics.EnableHelper.s_IsDevMode;
				}
			}

			// Token: 0x170011E1 RID: 4577
			// (get) Token: 0x060056E2 RID: 22242 RVA: 0x00163CB0 File Offset: 0x001630B0
			private static bool IsEnableVisualTreeChangedAllowed
			{
				get
				{
					bool? flag = VisualDiagnostics.EnableHelper.s_IsEnableVisualTreeChangedAllowed;
					if (flag == null)
					{
						return Debugger.IsAttached;
					}
					return flag.GetValueOrDefault();
				}
			}

			// Token: 0x040027F3 RID: 10227
			private static readonly bool s_IsDevMode;

			// Token: 0x040027F4 RID: 10228
			private static readonly bool? s_IsEnableVisualTreeChangedAllowed;

			// Token: 0x040027F5 RID: 10229
			private static bool s_IsVisualTreeChangedEnabled;

			// Token: 0x040027F6 RID: 10230
			private static bool? s_AllowChangesDuringVisualTreeChanged;

			// Token: 0x040027F7 RID: 10231
			private const string c_enableVisualTreeNotificationsEnvironmentVariable = "ENABLE_XAML_DIAGNOSTICS_VISUAL_TREE_NOTIFICATIONS";

			// Token: 0x040027F8 RID: 10232
			private const string c_devmodeRegKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\AppModelUnlock";

			// Token: 0x040027F9 RID: 10233
			private const string c_devmodeRegKeyFullPath = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\AppModelUnlock";

			// Token: 0x040027FA RID: 10234
			private const string c_devmodeValueName = "AllowDevelopmentWithoutDevLicense";
		}
	}
}
