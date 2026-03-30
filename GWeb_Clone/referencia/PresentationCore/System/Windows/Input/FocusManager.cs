using System;
using System.ComponentModel;
using System.Security;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using MS.Internal.KnownBoxes;

namespace System.Windows.Input
{
	/// <summary>Fornece um conjunto de métodos estáticos, propriedades anexadas e eventos para determinar e definir escopos de foco e para definir o elemento focalizado dentro do escopo.</summary>
	// Token: 0x02000237 RID: 567
	public static class FocusManager
	{
		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.FocusManager.GotFocus" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x06000FCE RID: 4046 RVA: 0x0003BF80 File Offset: 0x0003B380
		public static void AddGotFocusHandler(DependencyObject element, RoutedEventHandler handler)
		{
			UIElement.AddHandler(element, FocusManager.GotFocusEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.FocusManager.GotFocus" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x06000FCF RID: 4047 RVA: 0x0003BF9C File Offset: 0x0003B39C
		public static void RemoveGotFocusHandler(DependencyObject element, RoutedEventHandler handler)
		{
			UIElement.RemoveHandler(element, FocusManager.GotFocusEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.FocusManager.LostFocus" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x06000FD0 RID: 4048 RVA: 0x0003BFB8 File Offset: 0x0003B3B8
		public static void AddLostFocusHandler(DependencyObject element, RoutedEventHandler handler)
		{
			UIElement.AddHandler(element, FocusManager.LostFocusEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.FocusManager.LostFocus" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x06000FD1 RID: 4049 RVA: 0x0003BFD4 File Offset: 0x0003B3D4
		public static void RemoveLostFocusHandler(DependencyObject element, RoutedEventHandler handler)
		{
			UIElement.RemoveHandler(element, FocusManager.LostFocusEvent, handler);
		}

		/// <summary>Obtém o elemento com o foco lógico dentro do escopo de foco especificado.</summary>
		/// <param name="element">O elemento com o foco lógico no escopo de foco especificado.</param>
		/// <returns>O elemento no escopo de foco especificado com foco lógico.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> é <see langword="null" />.</exception>
		// Token: 0x06000FD2 RID: 4050 RVA: 0x0003BFF0 File Offset: 0x0003B3F0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public static IInputElement GetFocusedElement(DependencyObject element)
		{
			return FocusManager.GetFocusedElement(element, false);
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0003C004 File Offset: 0x0003B404
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static IInputElement GetFocusedElement(DependencyObject element, bool validate)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			DependencyObject dependencyObject = (DependencyObject)element.GetValue(FocusManager.FocusedElementProperty);
			if (validate && dependencyObject != null && PresentationSource.CriticalFromVisual(element) != PresentationSource.CriticalFromVisual(dependencyObject))
			{
				FocusManager.SetFocusedElement(element, null);
				dependencyObject = null;
			}
			return (IInputElement)dependencyObject;
		}

		/// <summary>Determina o foco lógico no elemento especificado.</summary>
		/// <param name="element">O escopo do foco no qual o elemento especificado se torna <see cref="P:System.Windows.Input.FocusManager.FocusedElement" />.</param>
		/// <param name="value">O elemento a receber foco lógico.</param>
		// Token: 0x06000FD4 RID: 4052 RVA: 0x0003C058 File Offset: 0x0003B458
		public static void SetFocusedElement(DependencyObject element, IInputElement value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(FocusManager.FocusedElementProperty, value);
		}

		/// <summary>Define o <see cref="T:System.Windows.DependencyObject" /> especificado como um escopo de foco.</summary>
		/// <param name="element">O elemento para criar um escopo de foco.</param>
		/// <param name="value">
		///   <see langword="true" /> se <paramref name="element" /> é um escopo de foco; caso contrário, <see langword="false" />.</param>
		// Token: 0x06000FD5 RID: 4053 RVA: 0x0003C080 File Offset: 0x0003B480
		public static void SetIsFocusScope(DependencyObject element, bool value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(FocusManager.IsFocusScopeProperty, value);
		}

		/// <summary>Determina se o <see cref="T:System.Windows.DependencyObject" /> especificado é um escopo de foco.</summary>
		/// <param name="element">O elemento do qual ler a propriedade anexada.</param>
		/// <returns>
		///   <see langword="true" /> se <see cref="P:System.Windows.Input.FocusManager.IsFocusScope" /> é definido como <see langword="true" /> no elemento especificado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000FD6 RID: 4054 RVA: 0x0003C0A8 File Offset: 0x0003B4A8
		public static bool GetIsFocusScope(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (bool)element.GetValue(FocusManager.IsFocusScopeProperty);
		}

		/// <summary>Determina o ancestral mais próximo do elemento especificado que tem <see cref="P:System.Windows.Input.FocusManager.IsFocusScope" /> definido como <see langword="true" />.</summary>
		/// <param name="element">O elemento do qual obter o escopo de foco mais próximo.</param>
		/// <returns>O escopo do foco do elemento especificado.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> é <see langword="null" />.</exception>
		// Token: 0x06000FD7 RID: 4055 RVA: 0x0003C0D4 File Offset: 0x0003B4D4
		public static DependencyObject GetFocusScope(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return FocusManager._GetFocusScope(element);
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0003C0F8 File Offset: 0x0003B4F8
		private static void OnFocusedElementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			IInputElement element = (IInputElement)e.NewValue;
			DependencyObject dependencyObject = (DependencyObject)e.OldValue;
			DependencyObject dependencyObject2 = (DependencyObject)e.NewValue;
			if (dependencyObject != null)
			{
				dependencyObject.ClearValue(UIElement.IsFocusedPropertyKey);
			}
			if (dependencyObject2 != null)
			{
				DependencyObject dependencyObject3 = Keyboard.FocusedElement as DependencyObject;
				dependencyObject2.SetValue(UIElement.IsFocusedPropertyKey, BooleanBoxes.TrueBox);
				DependencyObject dependencyObject4 = Keyboard.FocusedElement as DependencyObject;
				if (dependencyObject3 == dependencyObject4 && dependencyObject2 != dependencyObject4 && (dependencyObject4 == null || FocusManager.GetRoot(dependencyObject2) == FocusManager.GetRoot(dependencyObject4)))
				{
					Keyboard.Focus(element);
				}
			}
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0003C188 File Offset: 0x0003B588
		private static DependencyObject GetRoot(DependencyObject element)
		{
			if (element == null)
			{
				return null;
			}
			DependencyObject result = null;
			DependencyObject dependencyObject = element;
			ContentElement contentElement = element as ContentElement;
			if (contentElement != null)
			{
				dependencyObject = contentElement.GetUIParent();
			}
			while (dependencyObject != null)
			{
				result = dependencyObject;
				dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
			}
			return result;
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0003C1C0 File Offset: 0x0003B5C0
		private static DependencyObject _GetFocusScope(DependencyObject d)
		{
			if (d == null)
			{
				return null;
			}
			if ((bool)d.GetValue(FocusManager.IsFocusScopeProperty))
			{
				return d;
			}
			UIElement uielement = d as UIElement;
			if (uielement != null)
			{
				DependencyObject uiparentCore = uielement.GetUIParentCore();
				if (uiparentCore != null)
				{
					return FocusManager.GetFocusScope(uiparentCore);
				}
			}
			else
			{
				ContentElement contentElement = d as ContentElement;
				if (contentElement != null)
				{
					DependencyObject uiparent = contentElement.GetUIParent(true);
					if (uiparent != null)
					{
						return FocusManager._GetFocusScope(uiparent);
					}
				}
				else
				{
					UIElement3D uielement3D = d as UIElement3D;
					if (uielement3D != null)
					{
						DependencyObject uiparentCore2 = uielement3D.GetUIParentCore();
						if (uiparentCore2 != null)
						{
							return FocusManager.GetFocusScope(uiparentCore2);
						}
					}
				}
			}
			if (d is Visual || d is Visual3D)
			{
				DependencyObject parent = VisualTreeHelper.GetParent(d);
				if (parent != null)
				{
					return FocusManager._GetFocusScope(parent);
				}
			}
			return d;
		}

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.FocusManager.GotFocus" /> anexado.</summary>
		// Token: 0x0400089A RID: 2202
		public static readonly RoutedEvent GotFocusEvent = EventManager.RegisterRoutedEvent("GotFocus", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FocusManager));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.FocusManager.LostFocus" /> anexado.</summary>
		// Token: 0x0400089B RID: 2203
		public static readonly RoutedEvent LostFocusEvent = EventManager.RegisterRoutedEvent("LostFocus", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FocusManager));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Input.FocusManager.FocusedElement" /> anexada.</summary>
		// Token: 0x0400089C RID: 2204
		public static readonly DependencyProperty FocusedElementProperty = DependencyProperty.RegisterAttached("FocusedElement", typeof(IInputElement), typeof(FocusManager), new PropertyMetadata(new PropertyChangedCallback(FocusManager.OnFocusedElementChanged)));

		/// <summary>Identifica a propriedade <see cref="P:System.Windows.Input.FocusManager.IsFocusScope" /> anexada.</summary>
		// Token: 0x0400089D RID: 2205
		public static readonly DependencyProperty IsFocusScopeProperty = DependencyProperty.RegisterAttached("IsFocusScope", typeof(bool), typeof(FocusManager), new PropertyMetadata(BooleanBoxes.FalseBox));

		// Token: 0x0400089E RID: 2206
		private static readonly UncommonField<bool> IsFocusedElementSet = new UncommonField<bool>();

		// Token: 0x0400089F RID: 2207
		private static readonly UncommonField<WeakReference> FocusedElementWeakCacheField = new UncommonField<WeakReference>();

		// Token: 0x040008A0 RID: 2208
		private static readonly UncommonField<bool> IsFocusedElementCacheValid = new UncommonField<bool>();

		// Token: 0x040008A1 RID: 2209
		private static readonly UncommonField<WeakReference> FocusedElementCache = new UncommonField<WeakReference>();
	}
}
