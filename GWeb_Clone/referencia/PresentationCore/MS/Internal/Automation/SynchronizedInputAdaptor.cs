using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using MS.Internal.PresentationCore;

namespace MS.Internal.Automation
{
	// Token: 0x0200079A RID: 1946
	internal class SynchronizedInputAdaptor : ISynchronizedInputProvider
	{
		// Token: 0x060051B4 RID: 20916 RVA: 0x001467EC File Offset: 0x00145BEC
		internal SynchronizedInputAdaptor(DependencyObject owner)
		{
			Invariant.Assert(owner != null);
			this._owner = owner;
		}

		// Token: 0x060051B5 RID: 20917 RVA: 0x00146810 File Offset: 0x00145C10
		void ISynchronizedInputProvider.StartListening(SynchronizedInputType inputType)
		{
			if (inputType != SynchronizedInputType.KeyDown && inputType != SynchronizedInputType.KeyUp && inputType != SynchronizedInputType.MouseLeftButtonDown && inputType != SynchronizedInputType.MouseLeftButtonUp && inputType != SynchronizedInputType.MouseRightButtonDown && inputType != SynchronizedInputType.MouseRightButtonUp)
			{
				throw new ArgumentException(SR.Get("Automation_InvalidSynchronizedInputType", new object[]
				{
					inputType
				}));
			}
			UIElement uielement = this._owner as UIElement;
			if (uielement != null)
			{
				if (!uielement.StartListeningSynchronizedInput(inputType))
				{
					throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
				}
			}
			else
			{
				ContentElement contentElement = this._owner as ContentElement;
				if (contentElement != null)
				{
					if (!contentElement.StartListeningSynchronizedInput(inputType))
					{
						throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
					}
				}
				else
				{
					UIElement3D uielement3D = (UIElement3D)this._owner;
					if (!uielement3D.StartListeningSynchronizedInput(inputType))
					{
						throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
					}
				}
			}
		}

		// Token: 0x060051B6 RID: 20918 RVA: 0x001468CC File Offset: 0x00145CCC
		void ISynchronizedInputProvider.Cancel()
		{
			UIElement uielement = this._owner as UIElement;
			if (uielement != null)
			{
				uielement.CancelSynchronizedInput();
				return;
			}
			ContentElement contentElement = this._owner as ContentElement;
			if (contentElement != null)
			{
				contentElement.CancelSynchronizedInput();
				return;
			}
			UIElement3D uielement3D = (UIElement3D)this._owner;
			uielement3D.CancelSynchronizedInput();
		}

		// Token: 0x04002507 RID: 9479
		private readonly DependencyObject _owner;
	}
}
