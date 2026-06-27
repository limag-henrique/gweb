using System;
using System.Security;
using MS.Internal;

namespace System.Windows
{
	/// <summary>Fornece dados para o evento SourceChanged, usado para a interoperação. Essa classe não pode ser herdada.</summary>
	// Token: 0x020001DC RID: 476
	public sealed class SourceChangedEventArgs : RoutedEventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.SourceChangedEventArgs" />, usando as informações fornecidas para as fontes novas e antigas.</summary>
		/// <param name="oldSource">O <see cref="T:System.Windows.PresentationSource" /> antigo sobre o qual este manipulador está sendo notificado.</param>
		/// <param name="newSource">O <see cref="T:System.Windows.PresentationSource" /> novo sobre o qual este manipulador está sendo notificado.</param>
		// Token: 0x06000CBB RID: 3259 RVA: 0x00030568 File Offset: 0x0002F968
		[SecurityCritical]
		public SourceChangedEventArgs(PresentationSource oldSource, PresentationSource newSource) : this(oldSource, newSource, null, null)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.SourceChangedEventArgs" />, usando as informações fornecidas para as fontes novas e antigas, o elemento que esta alteração afeta e o pai anterior relatado desse elemento.</summary>
		/// <param name="oldSource">O <see cref="T:System.Windows.PresentationSource" /> antigo sobre o qual este manipulador está sendo notificado.</param>
		/// <param name="newSource">O <see cref="T:System.Windows.PresentationSource" /> novo sobre o qual este manipulador está sendo notificado.</param>
		/// <param name="element">O elemento cujo pai foi alterado, fazendo com que a fonte fosse alterada.</param>
		/// <param name="oldParent">O pai antigo do elemento cujo pai foi alterado, fazendo com que a fonte fosse alterada.</param>
		// Token: 0x06000CBC RID: 3260 RVA: 0x00030580 File Offset: 0x0002F980
		[SecurityCritical]
		public SourceChangedEventArgs(PresentationSource oldSource, PresentationSource newSource, IInputElement element, IInputElement oldParent)
		{
			this._oldSource = new SecurityCriticalData<PresentationSource>(oldSource);
			this._newSource = new SecurityCriticalData<PresentationSource>(newSource);
			this._element = element;
			this._oldParent = oldParent;
		}

		/// <summary>Obtém a fonte antiga envolvida nessa alteração de fonte.</summary>
		/// <returns>O <see cref="T:System.Windows.PresentationSource" /> antigo.</returns>
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x000305BC File Offset: 0x0002F9BC
		public PresentationSource OldSource
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandUIWindowPermission();
				return this._oldSource.Value;
			}
		}

		/// <summary>Obtém a nova fonte envolvida nessa alteração de fonte.</summary>
		/// <returns>O novo <see cref="T:System.Windows.PresentationSource" />.</returns>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x000305DC File Offset: 0x0002F9DC
		public PresentationSource NewSource
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandUIWindowPermission();
				return this._newSource.Value;
			}
		}

		/// <summary>Obtém o elemento cujo pai foi alterado, fazendo com que as informações de fonte da apresentação fossem alteradas.</summary>
		/// <returns>O elemento que está relatando a alteração.</returns>
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x000305FC File Offset: 0x0002F9FC
		public IInputElement Element
		{
			get
			{
				return this._element;
			}
		}

		/// <summary>Obtém o pai anterior do elemento cujo pai foi alterado, fazendo com que as informações de fonte da apresentação fossem alteradas.</summary>
		/// <returns>A origem do elemento pai anterior.</returns>
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00030610 File Offset: 0x0002FA10
		public IInputElement OldParent
		{
			get
			{
				return this._oldParent;
			}
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00030624 File Offset: 0x0002FA24
		protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			SourceChangedEventHandler sourceChangedEventHandler = (SourceChangedEventHandler)genericHandler;
			sourceChangedEventHandler(genericTarget, this);
		}

		// Token: 0x04000753 RID: 1875
		private SecurityCriticalData<PresentationSource> _oldSource;

		// Token: 0x04000754 RID: 1876
		private SecurityCriticalData<PresentationSource> _newSource;

		// Token: 0x04000755 RID: 1877
		private IInputElement _element;

		// Token: 0x04000756 RID: 1878
		private IInputElement _oldParent;
	}
}
