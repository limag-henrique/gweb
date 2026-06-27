using System;

namespace System.Windows
{
	/// <summary>Contém argumentos para o evento <see cref="E:System.Windows.DragDrop.GiveFeedback" />.</summary>
	// Token: 0x020001C0 RID: 448
	public sealed class GiveFeedbackEventArgs : RoutedEventArgs
	{
		// Token: 0x06000B73 RID: 2931 RVA: 0x0002D5FC File Offset: 0x0002C9FC
		internal GiveFeedbackEventArgs(DragDropEffects effects, bool useDefaultCursors)
		{
			DragDrop.IsValidDragDropEffects(effects);
			this._effects = effects;
			this._useDefaultCursors = useDefaultCursors;
		}

		/// <summary>Obtém um valor que indica os efeitos da operação do tipo "arrastar e soltar".</summary>
		/// <returns>Um membro do <see cref="T:System.Windows.DragDropEffects" /> enumeração que indica os efeitos da operação de arrastar e soltar.</returns>
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0002D624 File Offset: 0x0002CA24
		public DragDropEffects Effects
		{
			get
			{
				return this._effects;
			}
		}

		/// <summary>Obtém ou define um valor booliano que indica se o comportamento padrão de comentários do cursor deve ser usado para a operação do tipo "arrastar e soltar" associada.</summary>
		/// <returns>Um valor booliano que indica se o comportamento padrão de comentários do cursor deve ser usado para a operação de arrastar e soltar associada. <see langword="true" /> Para usar o comportamento padrão do cursor de comentários; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0002D638 File Offset: 0x0002CA38
		// (set) Token: 0x06000B76 RID: 2934 RVA: 0x0002D64C File Offset: 0x0002CA4C
		public bool UseDefaultCursors
		{
			get
			{
				return this._useDefaultCursors;
			}
			set
			{
				this._useDefaultCursors = value;
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0002D660 File Offset: 0x0002CA60
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			GiveFeedbackEventHandler giveFeedbackEventHandler = (GiveFeedbackEventHandler)genericHandler;
			giveFeedbackEventHandler(genericTarget, this);
		}

		// Token: 0x040006D8 RID: 1752
		private DragDropEffects _effects;

		// Token: 0x040006D9 RID: 1753
		private bool _useDefaultCursors;
	}
}
