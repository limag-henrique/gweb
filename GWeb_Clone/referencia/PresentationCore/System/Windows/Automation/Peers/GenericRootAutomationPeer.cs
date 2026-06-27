using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using MS.Internal;
using MS.Win32;

namespace System.Windows.Automation.Peers
{
	/// <summary>Expõe tipos <see cref="T:System.Windows.Interop.HwndSource" /> à Automação de Interface do Usuário.</summary>
	// Token: 0x02000316 RID: 790
	public class GenericRootAutomationPeer : UIElementAutomationPeer
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Automation.Peers.GenericRootAutomationPeer" />.</summary>
		/// <param name="owner">O <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.GenericRootAutomationPeer" />.</param>
		// Token: 0x060019CD RID: 6605 RVA: 0x00066628 File Offset: 0x00065A28
		public GenericRootAutomationPeer(UIElement owner) : base(owner)
		{
		}

		/// <summary>Obtém o nome do <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.GenericRootAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetClassName" />.</summary>
		/// <returns>Uma cadeia de caracteres que contém "Painel".</returns>
		// Token: 0x060019CE RID: 6606 RVA: 0x0006663C File Offset: 0x00065A3C
		protected override string GetClassNameCore()
		{
			return "Pane";
		}

		/// <summary>Obtém o tipo de controle para o <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.GenericRootAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetAutomationControlType" />.</summary>
		/// <returns>O valor de enumeração <see cref="F:System.Windows.Automation.Peers.AutomationControlType.Pane" />.</returns>
		// Token: 0x060019CF RID: 6607 RVA: 0x00066650 File Offset: 0x00065A50
		protected override AutomationControlType GetAutomationControlTypeCore()
		{
			return AutomationControlType.Pane;
		}

		/// <summary>Obtém o rótulo de texto do <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.GenericRootAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetName" />.</summary>
		/// <returns>A cadeia de caracteres que contém o rótulo.</returns>
		// Token: 0x060019D0 RID: 6608 RVA: 0x00066660 File Offset: 0x00065A60
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected override string GetNameCore()
		{
			string text = base.GetNameCore();
			if (text == string.Empty)
			{
				IntPtr hwnd = base.Hwnd;
				if (hwnd != IntPtr.Zero)
				{
					try
					{
						StringBuilder stringBuilder;
						if (CoreAppContextSwitches.OptOutIgnoreWin32SetLastError)
						{
							stringBuilder = new StringBuilder(512);
							UnsafeNativeMethods.GetWindowText(new HandleRef(null, hwnd), stringBuilder, stringBuilder.Capacity);
						}
						else
						{
							stringBuilder = new StringBuilder();
							UnsafeNativeMethods.GetWindowTextNoThrow(new HandleRef(null, hwnd), stringBuilder, 0);
						}
						text = stringBuilder.ToString();
					}
					catch (Win32Exception)
					{
					}
					if (text == null)
					{
						text = string.Empty;
					}
				}
			}
			return text;
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Rect" /> que representa o retângulo delimitador do <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.GenericRootAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetBoundingRectangle" />.</summary>
		/// <returns>O retângulo delimitador.</returns>
		// Token: 0x060019D1 RID: 6609 RVA: 0x00066704 File Offset: 0x00065B04
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected override Rect GetBoundingRectangleCore()
		{
			Rect result = new Rect(0.0, 0.0, 0.0, 0.0);
			IntPtr hwnd = base.Hwnd;
			if (hwnd != IntPtr.Zero)
			{
				NativeMethods.RECT rect = new NativeMethods.RECT(0, 0, 0, 0);
				try
				{
					SafeNativeMethods.GetWindowRect(new HandleRef(null, hwnd), ref rect);
				}
				catch (Win32Exception)
				{
				}
				result = new Rect((double)rect.left, (double)rect.top, (double)(rect.right - rect.left), (double)(rect.bottom - rect.top));
			}
			return result;
		}
	}
}
