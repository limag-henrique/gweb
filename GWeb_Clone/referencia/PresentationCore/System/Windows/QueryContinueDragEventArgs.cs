using System;
using MS.Internal.PresentationCore;

namespace System.Windows
{
	/// <summary>Contém argumentos para o evento <see cref="E:System.Windows.DragDrop.QueryContinueDrag" />.</summary>
	// Token: 0x020001D0 RID: 464
	public sealed class QueryContinueDragEventArgs : RoutedEventArgs
	{
		// Token: 0x06000C6D RID: 3181 RVA: 0x0002F924 File Offset: 0x0002ED24
		internal QueryContinueDragEventArgs(bool escapePressed, DragDropKeyStates dragDropKeyStates)
		{
			DragDrop.IsValidDragDropKeyStates(dragDropKeyStates);
			this._escapePressed = escapePressed;
			this._dragDropKeyStates = dragDropKeyStates;
		}

		/// <summary>Obtém um valor booliano que indica se a tecla ESC foi pressionada.</summary>
		/// <returns>Um valor booliano que indica se a tecla ESC foi pressionada. <see langword="true" /> Se a ESC foi pressionado; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0002F94C File Offset: 0x0002ED4C
		public bool EscapePressed
		{
			get
			{
				return this._escapePressed;
			}
		}

		/// <summary>Obtém uma enumeração do sinalizador que indica o estado atual das teclas SHIFT, CTRL e ALT, bem como o estado dos botões do mouse.</summary>
		/// <returns>Um ou mais membros a <see cref="T:System.Windows.DragDropKeyStates" /> enumeração do sinalizador.</returns>
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x0002F960 File Offset: 0x0002ED60
		public DragDropKeyStates KeyStates
		{
			get
			{
				return this._dragDropKeyStates;
			}
		}

		/// <summary>Obtém ou define o status atual da operação do tipo "arrastar e soltar" associada.</summary>
		/// <returns>Um membro do <see cref="T:System.Windows.DragAction" /> enumeração que indica o status atual da operação de arrastar e soltar associado.</returns>
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x0002F974 File Offset: 0x0002ED74
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x0002F988 File Offset: 0x0002ED88
		public DragAction Action
		{
			get
			{
				return this._action;
			}
			set
			{
				if (!DragDrop.IsValidDragAction(value))
				{
					throw new ArgumentException(SR.Get("DragDrop_DragActionInvalid", new object[]
					{
						"value"
					}));
				}
				this._action = value;
			}
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0002F9C4 File Offset: 0x0002EDC4
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			QueryContinueDragEventHandler queryContinueDragEventHandler = (QueryContinueDragEventHandler)genericHandler;
			queryContinueDragEventHandler(genericTarget, this);
		}

		// Token: 0x04000729 RID: 1833
		private bool _escapePressed;

		// Token: 0x0400072A RID: 1834
		private DragDropKeyStates _dragDropKeyStates;

		// Token: 0x0400072B RID: 1835
		private DragAction _action;
	}
}
