using System;
using System.Windows.Input;
using MS.Internal.PresentationCore;

namespace System.Windows
{
	/// <summary>Contém argumentos relevantes para todos os eventos do tipo "arrastar e soltar" (<see cref="E:System.Windows.DragDrop.DragEnter" />, <see cref="E:System.Windows.DragDrop.DragLeave" />, <see cref="E:System.Windows.DragDrop.DragOver" /> e <see cref="E:System.Windows.DragDrop.Drop" />).</summary>
	// Token: 0x020001AA RID: 426
	public sealed class DragEventArgs : RoutedEventArgs
	{
		// Token: 0x06000672 RID: 1650 RVA: 0x0001DBEC File Offset: 0x0001CFEC
		internal DragEventArgs(IDataObject data, DragDropKeyStates dragDropKeyStates, DragDropEffects allowedEffects, DependencyObject target, Point point)
		{
			DragDrop.IsValidDragDropKeyStates(dragDropKeyStates);
			DragDrop.IsValidDragDropEffects(allowedEffects);
			this._data = data;
			this._dragDropKeyStates = dragDropKeyStates;
			this._allowedEffects = allowedEffects;
			this._target = target;
			this._dropPoint = point;
			this._effects = allowedEffects;
		}

		/// <summary>Retorna um ponto de soltura relativo a um <see cref="T:System.Windows.IInputElement" /> especificado.</summary>
		/// <param name="relativeTo">Um objeto <see cref="T:System.Windows.IInputElement" /> para o qual obter um ponto de soltura relativo.</param>
		/// <returns>Um ponto de soltura relativo ao elemento especificado em <paramref name="relativeTo" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">Acionado quando <paramref name="relativeTo" /> é null.</exception>
		// Token: 0x06000673 RID: 1651 RVA: 0x0001DC3C File Offset: 0x0001D03C
		public Point GetPosition(IInputElement relativeTo)
		{
			if (relativeTo == null)
			{
				throw new ArgumentNullException("relativeTo");
			}
			Point result = new Point(0.0, 0.0);
			if (this._target != null)
			{
				result = InputElement.TranslatePoint(this._dropPoint, this._target, (DependencyObject)relativeTo);
			}
			return result;
		}

		/// <summary>Obtém um objeto de dados que contém os dados associados ao evento de arrastar correspondente.</summary>
		/// <returns>Um objeto de dados que contém os dados associados ao evento de arrastar correspondente.</returns>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0001DC94 File Offset: 0x0001D094
		public IDataObject Data
		{
			get
			{
				return this._data;
			}
		}

		/// <summary>Obtém uma enumeração do sinalizador que indica o estado atual das teclas SHIFT, CTRL e ALT, bem como o estado dos botões do mouse.</summary>
		/// <returns>Um ou mais membros a <see cref="T:System.Windows.DragDropKeyStates" /> enumeração do sinalizador.</returns>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x0001DCA8 File Offset: 0x0001D0A8
		public DragDropKeyStates KeyStates
		{
			get
			{
				return this._dragDropKeyStates;
			}
		}

		/// <summary>Obtém um membro da enumeração <see cref="T:System.Windows.DragDropEffects" /> que especifica quais operações são permitidas pelo originador do evento de arrastar.</summary>
		/// <returns>Um membro do <see cref="T:System.Windows.DragDropEffects" /> enumeração que especifica quais operações são permitidas pelo originador do evento arrastar.</returns>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x0001DCBC File Offset: 0x0001D0BC
		public DragDropEffects AllowedEffects
		{
			get
			{
				return this._allowedEffects;
			}
		}

		/// <summary>Obtém ou define a operação do tipo "arrastar e soltar".</summary>
		/// <returns>Um membro da enumeração <see cref="T:System.Windows.DragDropEffects" /> que especifica a operação do tipo “arrastar e soltar” de destino.</returns>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0001DCD0 File Offset: 0x0001D0D0
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x0001DCE4 File Offset: 0x0001D0E4
		public DragDropEffects Effects
		{
			get
			{
				return this._effects;
			}
			set
			{
				if (!DragDrop.IsValidDragDropEffects(value))
				{
					throw new ArgumentException(SR.Get("DragDrop_DragDropEffectsInvalid", new object[]
					{
						"value"
					}));
				}
				this._effects = value;
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001DD20 File Offset: 0x0001D120
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			DragEventHandler dragEventHandler = (DragEventHandler)genericHandler;
			dragEventHandler(genericTarget, this);
		}

		// Token: 0x0400059A RID: 1434
		private IDataObject _data;

		// Token: 0x0400059B RID: 1435
		private DragDropKeyStates _dragDropKeyStates;

		// Token: 0x0400059C RID: 1436
		private DragDropEffects _allowedEffects;

		// Token: 0x0400059D RID: 1437
		private DragDropEffects _effects;

		// Token: 0x0400059E RID: 1438
		private DependencyObject _target;

		// Token: 0x0400059F RID: 1439
		private Point _dropPoint;
	}
}
