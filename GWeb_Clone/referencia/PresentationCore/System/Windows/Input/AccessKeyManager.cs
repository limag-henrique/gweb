using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Windows.Interop;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace System.Windows.Input
{
	/// <summary>Mantém o registro de todas as chaves de acesso e a manipulação de comandos de teclado de interoperabilidade entre Windows Forms, Win32 e WPF (Windows Presentation Foundation).</summary>
	// Token: 0x02000228 RID: 552
	public sealed class AccessKeyManager
	{
		/// <summary>Associa as chaves de acesso especificadas ao elemento especificado.</summary>
		/// <param name="key">A chave de acesso.</param>
		/// <param name="element">O elemento ao qual associar <paramref name="key" />.</param>
		// Token: 0x06000F42 RID: 3906 RVA: 0x0003A364 File Offset: 0x00039764
		public static void Register(string key, IInputElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			key = AccessKeyManager.NormalizeKey(key);
			AccessKeyManager accessKeyManager = AccessKeyManager.Current;
			Hashtable keyToElements = accessKeyManager._keyToElements;
			lock (keyToElements)
			{
				ArrayList arrayList = (ArrayList)accessKeyManager._keyToElements[key];
				if (arrayList == null)
				{
					arrayList = new ArrayList(1);
					accessKeyManager._keyToElements[key] = arrayList;
				}
				else
				{
					AccessKeyManager.PurgeDead(arrayList, null);
				}
				arrayList.Add(new WeakReference(element));
			}
		}

		/// <summary>Desassocia as chaves de acesso especificadas do elemento especificado.</summary>
		/// <param name="key">A chave de acesso.</param>
		/// <param name="element">O elemento do qual dissociar <paramref name="key" />.</param>
		// Token: 0x06000F43 RID: 3907 RVA: 0x0003A408 File Offset: 0x00039808
		public static void Unregister(string key, IInputElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			key = AccessKeyManager.NormalizeKey(key);
			AccessKeyManager accessKeyManager = AccessKeyManager.Current;
			Hashtable keyToElements = accessKeyManager._keyToElements;
			lock (keyToElements)
			{
				ArrayList arrayList = (ArrayList)accessKeyManager._keyToElements[key];
				if (arrayList != null)
				{
					AccessKeyManager.PurgeDead(arrayList, element);
					if (arrayList.Count == 0)
					{
						accessKeyManager._keyToElements.Remove(key);
					}
				}
			}
		}

		/// <summary>Indica se a chave especificada é registrada como uma chave de acesso no escopo especificado.</summary>
		/// <param name="scope">A fonte de apresentação a consultar para <paramref name="key" />.</param>
		/// <param name="key">A chave a ser consultada.</param>
		/// <returns>
		///   <see langword="true" /> se a chave estiver registrada, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000F44 RID: 3908 RVA: 0x0003A49C File Offset: 0x0003989C
		public static bool IsKeyRegistered(object scope, string key)
		{
			key = AccessKeyManager.NormalizeKey(key);
			AccessKeyManager accessKeyManager = AccessKeyManager.Current;
			List<IInputElement> targetsForScope = accessKeyManager.GetTargetsForScope(scope, key, null, AccessKeyManager.AccessKeyInformation.Empty);
			return targetsForScope != null && targetsForScope.Count > 0;
		}

		/// <summary>Processa as chaves de acesso especificadas como se um evento <see cref="E:System.Windows.UIElement.KeyDown" /> da chave tivesse sido passado para o <see cref="T:System.Windows.Input.AccessKeyManager" />.</summary>
		/// <param name="scope">O escopo para a chave de acesso.</param>
		/// <param name="key">A chave de acesso.</param>
		/// <param name="isMultiple">Indica se a <paramref name="key" /> tem várias correspondências.</param>
		/// <returns>
		///   <see langword="true" /> se houver mais chaves correspondentes, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000F45 RID: 3909 RVA: 0x0003A4D4 File Offset: 0x000398D4
		[SecurityCritical]
		public static bool ProcessKey(object scope, string key, bool isMultiple)
		{
			key = AccessKeyManager.NormalizeKey(key);
			AccessKeyManager accessKeyManager = AccessKeyManager.Current;
			return accessKeyManager.ProcessKeyForScope(scope, key, isMultiple, false) == AccessKeyManager.ProcessKeyResult.MoreMatches;
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0003A4FC File Offset: 0x000398FC
		private static string NormalizeKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			string nextTextElement = StringInfo.GetNextTextElement(key);
			if (key != nextTextElement)
			{
				throw new ArgumentException(SR.Get("AccessKeyManager_NotAUnicodeCharacter", new object[]
				{
					"key"
				}));
			}
			return nextTextElement.ToUpperInvariant();
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.AccessKeyManager.AccessKeyPressed" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x06000F47 RID: 3911 RVA: 0x0003A54C File Offset: 0x0003994C
		public static void AddAccessKeyPressedHandler(DependencyObject element, AccessKeyPressedEventHandler handler)
		{
			UIElement.AddHandler(element, AccessKeyManager.AccessKeyPressedEvent, handler);
		}

		/// <summary>Remove o manipulador de eventos <see cref="E:System.Windows.Input.AccessKeyManager.AccessKeyPressed" /> especificado do objeto especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x06000F48 RID: 3912 RVA: 0x0003A568 File Offset: 0x00039968
		public static void RemoveAccessKeyPressedHandler(DependencyObject element, AccessKeyPressedEventHandler handler)
		{
			UIElement.RemoveHandler(element, AccessKeyManager.AccessKeyPressedEvent, handler);
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0003A584 File Offset: 0x00039984
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private AccessKeyManager()
		{
			InputManager.Current.PostProcessInput += this.PostProcessInput;
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x0003A5BC File Offset: 0x000399BC
		private static AccessKeyManager Current
		{
			get
			{
				if (AccessKeyManager._accessKeyManager == null)
				{
					AccessKeyManager._accessKeyManager = new AccessKeyManager();
				}
				return AccessKeyManager._accessKeyManager;
			}
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0003A5E0 File Offset: 0x000399E0
		[SecurityCritical]
		private void PostProcessInput(object sender, ProcessInputEventArgs e)
		{
			if (e.StagingItem.Input.Handled)
			{
				return;
			}
			if (e.StagingItem.Input.RoutedEvent == Keyboard.KeyDownEvent)
			{
				this.OnKeyDown((KeyEventArgs)e.StagingItem.Input);
				return;
			}
			if (e.StagingItem.Input.RoutedEvent == TextCompositionManager.TextInputEvent)
			{
				this.OnText((TextCompositionEventArgs)e.StagingItem.Input);
			}
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0003A65C File Offset: 0x00039A5C
		[SecurityCritical]
		private AccessKeyManager.ProcessKeyResult ProcessKeyForSender(object sender, string key, bool existsElsewhere, bool userInitiated)
		{
			key = key.ToUpperInvariant();
			IInputElement sender2 = sender as IInputElement;
			List<IInputElement> targetsForSender = this.GetTargetsForSender(sender2, key);
			return this.ProcessKey(targetsForSender, key, existsElsewhere, userInitiated);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0003A68C File Offset: 0x00039A8C
		[SecurityCritical]
		private AccessKeyManager.ProcessKeyResult ProcessKeyForScope(object scope, string key, bool existsElsewhere, bool userInitiated)
		{
			List<IInputElement> targetsForScope = this.GetTargetsForScope(scope, key, null, AccessKeyManager.AccessKeyInformation.Empty);
			return this.ProcessKey(targetsForScope, key, existsElsewhere, userInitiated);
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0003A6B4 File Offset: 0x00039AB4
		[SecurityCritical]
		private AccessKeyManager.ProcessKeyResult ProcessKey(List<IInputElement> targets, string key, bool existsElsewhere, bool userInitiated)
		{
			if (targets != null)
			{
				bool flag = true;
				UIElement uielement = null;
				bool flag2 = false;
				int num = 0;
				for (int i = 0; i < targets.Count; i++)
				{
					UIElement uielement2 = targets[i] as UIElement;
					if (uielement2.IsEnabled)
					{
						if (uielement == null)
						{
							uielement = uielement2;
							num = i;
						}
						else
						{
							if (flag2)
							{
								uielement = uielement2;
								num = i;
							}
							flag = false;
						}
						flag2 = uielement2.HasEffectiveKeyboardFocus;
					}
				}
				if (uielement != null)
				{
					AccessKeyEventArgs accessKeyEventArgs = new AccessKeyEventArgs(key, !flag || existsElsewhere, userInitiated);
					try
					{
						uielement.InvokeAccessKey(accessKeyEventArgs);
					}
					finally
					{
						accessKeyEventArgs.ClearUserInitiated();
					}
					if (num != targets.Count - 1)
					{
						return AccessKeyManager.ProcessKeyResult.MoreMatches;
					}
					return AccessKeyManager.ProcessKeyResult.LastMatch;
				}
			}
			return AccessKeyManager.ProcessKeyResult.NoMatch;
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0003A768 File Offset: 0x00039B68
		[SecurityCritical]
		private void OnText(TextCompositionEventArgs e)
		{
			string text = e.Text;
			if (text == null || text.Length == 0)
			{
				text = e.SystemText;
			}
			if (text != null && text.Length > 0 && this.ProcessKeyForSender(e.OriginalSource, text, false, e.UserInitiated) != AccessKeyManager.ProcessKeyResult.NoMatch)
			{
				e.Handled = true;
			}
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0003A7B8 File Offset: 0x00039BB8
		[SecurityCritical]
		private void OnKeyDown(KeyEventArgs e)
		{
			KeyboardDevice keyboardDevice = (KeyboardDevice)e.Device;
			string text = null;
			Key realKey = e.RealKey;
			if (realKey != Key.Return)
			{
				if (realKey == Key.Escape)
				{
					text = "\u001b";
				}
			}
			else
			{
				text = "\r";
			}
			if (text != null && this.ProcessKeyForSender(e.OriginalSource, text, false, e.UserInitiated) != AccessKeyManager.ProcessKeyResult.NoMatch)
			{
				e.Handled = true;
			}
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0003A814 File Offset: 0x00039C14
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private List<IInputElement> GetTargetsForSender(IInputElement sender, string key)
		{
			AccessKeyManager.AccessKeyInformation infoForElement = this.GetInfoForElement(sender, key);
			return this.GetTargetsForScope(infoForElement.Scope, key, sender, infoForElement);
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0003A83C File Offset: 0x00039C3C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private List<IInputElement> GetTargetsForScope(object scope, string key, IInputElement sender, AccessKeyManager.AccessKeyInformation senderInfo)
		{
			if (scope == null)
			{
				scope = this.CriticalGetActiveSource();
				if (scope == null)
				{
					return null;
				}
			}
			if (CoreCompatibilityPreferences.GetIsAltKeyRequiredInAccessKeyDefaultScope() && scope is PresentationSource && (Keyboard.Modifiers & ModifierKeys.Alt) != ModifierKeys.Alt)
			{
				return null;
			}
			Hashtable keyToElements = this._keyToElements;
			List<IInputElement> list;
			lock (keyToElements)
			{
				list = AccessKeyManager.CopyAndPurgeDead(this._keyToElements[key] as ArrayList);
			}
			if (list == null)
			{
				return null;
			}
			List<IInputElement> list2 = new List<IInputElement>(1);
			for (int i = 0; i < list.Count; i++)
			{
				IInputElement inputElement = list[i];
				if (inputElement != sender)
				{
					if (this.IsTargetable(inputElement))
					{
						AccessKeyManager.AccessKeyInformation infoForElement = this.GetInfoForElement(inputElement, key);
						if (infoForElement.target != null && scope == infoForElement.Scope)
						{
							list2.Add(infoForElement.target);
						}
					}
				}
				else if (senderInfo.target != null)
				{
					list2.Add(senderInfo.target);
				}
			}
			return list2;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0003A940 File Offset: 0x00039D40
		[SecurityCritical]
		private AccessKeyManager.AccessKeyInformation GetInfoForElement(IInputElement element, string key)
		{
			AccessKeyManager.AccessKeyInformation result = default(AccessKeyManager.AccessKeyInformation);
			if (element != null)
			{
				AccessKeyPressedEventArgs accessKeyPressedEventArgs = new AccessKeyPressedEventArgs(key);
				element.RaiseEvent(accessKeyPressedEventArgs);
				result.Scope = accessKeyPressedEventArgs.Scope;
				result.target = accessKeyPressedEventArgs.Target;
				if (result.Scope == null)
				{
					result.Scope = this.GetSourceForElement(element);
				}
			}
			else
			{
				result.Scope = this.CriticalGetActiveSource();
			}
			return result;
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0003A9A8 File Offset: 0x00039DA8
		[SecurityCritical]
		private PresentationSource GetSourceForElement(IInputElement element)
		{
			PresentationSource result = null;
			DependencyObject dependencyObject = element as DependencyObject;
			if (dependencyObject != null)
			{
				DependencyObject containingVisual = InputElement.GetContainingVisual(dependencyObject);
				if (containingVisual != null)
				{
					result = PresentationSource.CriticalFromVisual(containingVisual);
				}
			}
			return result;
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0003A9D4 File Offset: 0x00039DD4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private PresentationSource GetActiveSource()
		{
			IntPtr activeWindow = UnsafeNativeMethods.GetActiveWindow();
			if (activeWindow != IntPtr.Zero)
			{
				return HwndSource.FromHwnd(activeWindow);
			}
			return null;
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0003A9FC File Offset: 0x00039DFC
		[SecurityCritical]
		private PresentationSource CriticalGetActiveSource()
		{
			IntPtr activeWindow = UnsafeNativeMethods.GetActiveWindow();
			if (activeWindow != IntPtr.Zero)
			{
				return HwndSource.CriticalFromHwnd(activeWindow);
			}
			return null;
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0003AA24 File Offset: 0x00039E24
		private bool IsTargetable(IInputElement element)
		{
			DependencyObject containingUIElement = InputElement.GetContainingUIElement((DependencyObject)element);
			return containingUIElement != null && AccessKeyManager.IsVisible(containingUIElement) && AccessKeyManager.IsEnabled(containingUIElement);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0003AA54 File Offset: 0x00039E54
		private static bool IsVisible(DependencyObject element)
		{
			while (element != null)
			{
				UIElement uielement = element as UIElement;
				UIElement3D uielement3D = element as UIElement3D;
				Visibility visibility;
				if (uielement != null)
				{
					visibility = uielement.Visibility;
				}
				else
				{
					visibility = uielement3D.Visibility;
				}
				if (visibility != Visibility.Visible)
				{
					return false;
				}
				element = UIElementHelper.GetUIParent(element);
			}
			return true;
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0003AA98 File Offset: 0x00039E98
		private static bool IsEnabled(DependencyObject element)
		{
			return (bool)element.GetValue(UIElement.IsEnabledProperty);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0003AAB8 File Offset: 0x00039EB8
		private static void PurgeDead(ArrayList elements, object elementToRemove)
		{
			int i = 0;
			while (i < elements.Count)
			{
				WeakReference weakReference = (WeakReference)elements[i];
				object target = weakReference.Target;
				if (target == null || target == elementToRemove)
				{
					elements.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0003AAFC File Offset: 0x00039EFC
		private static List<IInputElement> CopyAndPurgeDead(ArrayList elements)
		{
			if (elements == null)
			{
				return null;
			}
			List<IInputElement> list = new List<IInputElement>(elements.Count);
			int i = 0;
			while (i < elements.Count)
			{
				WeakReference weakReference = (WeakReference)elements[i];
				object target = weakReference.Target;
				if (target == null)
				{
					elements.RemoveAt(i);
				}
				else
				{
					list.Add((IInputElement)target);
					i++;
				}
			}
			return list;
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0003AB58 File Offset: 0x00039F58
		internal static string InternalGetAccessKeyCharacter(DependencyObject d)
		{
			return AccessKeyManager.Current.GetAccessKeyCharacter(d);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0003AB70 File Offset: 0x00039F70
		private string GetAccessKeyCharacter(DependencyObject d)
		{
			WeakReference weakReference = (WeakReference)d.GetValue(AccessKeyManager.AccessKeyElementProperty);
			IInputElement inputElement = (weakReference != null) ? ((IInputElement)weakReference.Target) : null;
			if (inputElement != null)
			{
				AccessKeyPressedEventArgs accessKeyPressedEventArgs = new AccessKeyPressedEventArgs();
				inputElement.RaiseEvent(accessKeyPressedEventArgs);
				if (accessKeyPressedEventArgs.Target == d)
				{
					foreach (object obj in AccessKeyManager.Current._keyToElements)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						ArrayList arrayList = (ArrayList)dictionaryEntry.Value;
						for (int i = 0; i < arrayList.Count; i++)
						{
							WeakReference weakReference2 = (WeakReference)arrayList[i];
							if (weakReference2.Target == inputElement)
							{
								return (string)dictionaryEntry.Key;
							}
						}
					}
				}
			}
			d.ClearValue(AccessKeyManager.AccessKeyElementProperty);
			foreach (object obj2 in AccessKeyManager.Current._keyToElements)
			{
				DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
				ArrayList arrayList2 = (ArrayList)dictionaryEntry2.Value;
				for (int j = 0; j < arrayList2.Count; j++)
				{
					WeakReference weakReference3 = (WeakReference)arrayList2[j];
					IInputElement inputElement2 = (IInputElement)weakReference3.Target;
					if (inputElement2 != null)
					{
						AccessKeyPressedEventArgs accessKeyPressedEventArgs2 = new AccessKeyPressedEventArgs();
						inputElement2.RaiseEvent(accessKeyPressedEventArgs2);
						if (accessKeyPressedEventArgs2.Target != null)
						{
							accessKeyPressedEventArgs2.Target.SetValue(AccessKeyManager.AccessKeyElementProperty, weakReference3);
							if (accessKeyPressedEventArgs2.Target == d)
							{
								return (string)dictionaryEntry2.Key;
							}
						}
					}
				}
			}
			return string.Empty;
		}

		/// <summary>Identifica o evento roteado <see cref="E:System.Windows.Input.AccessKeyManager.AccessKeyPressed" />.</summary>
		// Token: 0x0400085E RID: 2142
		public static readonly RoutedEvent AccessKeyPressedEvent = EventManager.RegisterRoutedEvent("AccessKeyPressed", RoutingStrategy.Bubble, typeof(AccessKeyPressedEventHandler), typeof(AccessKeyManager));

		// Token: 0x0400085F RID: 2143
		private static readonly DependencyProperty AccessKeyElementProperty = DependencyProperty.RegisterAttached("AccessKeyElement", typeof(WeakReference), typeof(AccessKeyManager));

		// Token: 0x04000860 RID: 2144
		private Hashtable _keyToElements = new Hashtable(10);

		// Token: 0x04000861 RID: 2145
		[ThreadStatic]
		private static AccessKeyManager _accessKeyManager;

		// Token: 0x02000809 RID: 2057
		private enum ProcessKeyResult
		{
			// Token: 0x04002716 RID: 10006
			NoMatch,
			// Token: 0x04002717 RID: 10007
			MoreMatches,
			// Token: 0x04002718 RID: 10008
			LastMatch
		}

		// Token: 0x0200080A RID: 2058
		private struct AccessKeyInformation
		{
			// Token: 0x170011A2 RID: 4514
			// (get) Token: 0x06005602 RID: 22018 RVA: 0x00161DD8 File Offset: 0x001611D8
			// (set) Token: 0x06005603 RID: 22019 RVA: 0x00161DEC File Offset: 0x001611EC
			public object Scope
			{
				[SecurityCritical]
				get
				{
					return this._scope;
				}
				set
				{
					this._scope = value;
				}
			}

			// Token: 0x170011A3 RID: 4515
			// (get) Token: 0x06005604 RID: 22020 RVA: 0x00161E00 File Offset: 0x00161200
			public static AccessKeyManager.AccessKeyInformation Empty
			{
				get
				{
					return AccessKeyManager.AccessKeyInformation._empty;
				}
			}

			// Token: 0x04002719 RID: 10009
			public UIElement target;

			// Token: 0x0400271A RID: 10010
			private static AccessKeyManager.AccessKeyInformation _empty;

			// Token: 0x0400271B RID: 10011
			private object _scope;
		}
	}
}
