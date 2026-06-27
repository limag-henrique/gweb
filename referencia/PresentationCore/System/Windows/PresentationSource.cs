using System;
using System.Collections;
using System.Security;
using System.Security.Permissions;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows
{
	/// <summary>Fornece uma base abstrata às classes que apresentam conteúdo de outra tecnologia como parte de um cenário de interoperação. Além disso, essa classe fornece métodos estáticos para trabalhar com essas origens, bem como a arquitetura básica da apresentação da camada visual.</summary>
	// Token: 0x020001CF RID: 463
	[UIPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public abstract class PresentationSource : DispatcherObject
	{
		// Token: 0x06000C4B RID: 3147 RVA: 0x0002F1C0 File Offset: 0x0002E5C0
		[SecurityCritical]
		[UIPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		internal virtual IInputProvider GetInputProvider(Type inputDevice)
		{
			return null;
		}

		/// <summary>Retorna a fonte na qual um <see cref="T:System.Windows.Media.Visual" /> fornecido é apresentado.</summary>
		/// <param name="visual">O <see cref="T:System.Windows.Media.Visual" /> para o qual localizar a fonte.</param>
		/// <returns>A <see cref="T:System.Windows.PresentationSource" /> na qual o visual está sendo apresentado ou <see langword="null" />, se o <paramref name="visual" /> for descartado.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="visual" /> é <see langword="null" />.</exception>
		// Token: 0x06000C4C RID: 3148 RVA: 0x0002F1D0 File Offset: 0x0002E5D0
		[SecurityCritical]
		public static PresentationSource FromVisual(Visual visual)
		{
			SecurityHelper.DemandUIWindowPermission();
			return PresentationSource.CriticalFromVisual(visual);
		}

		/// <summary>Retorna a fonte na qual um <see cref="T:System.Windows.DependencyObject" /> fornecido é apresentado.</summary>
		/// <param name="dependencyObject">O <see cref="T:System.Windows.DependencyObject" /> para o qual localizar a fonte.</param>
		/// <returns>O <see cref="T:System.Windows.PresentationSource" /> no qual o objeto de dependência está sendo apresentado.</returns>
		// Token: 0x06000C4D RID: 3149 RVA: 0x0002F1E8 File Offset: 0x0002E5E8
		[SecurityCritical]
		public static PresentationSource FromDependencyObject(DependencyObject dependencyObject)
		{
			SecurityHelper.DemandUIWindowPermission();
			return PresentationSource.CriticalFromVisual(dependencyObject);
		}

		/// <summary>Adiciona um manipulador para o evento <see langword="SourceChanged" /> para o elemento fornecido.</summary>
		/// <param name="element">O elemento ao qual adicionar o manipulador.</param>
		/// <param name="handler">A implementação do manipulador a adicionar.</param>
		// Token: 0x06000C4E RID: 3150 RVA: 0x0002F200 File Offset: 0x0002E600
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		public static void AddSourceChangedHandler(IInputElement element, SourceChangedEventHandler handler)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (!InputElement.IsValid(element))
			{
				throw new ArgumentException(SR.Get("Invalid_IInputElement"), "element");
			}
			DependencyObject dependencyObject = (DependencyObject)element;
			if (handler != null)
			{
				if (InputElement.IsUIElement(dependencyObject))
				{
					UIElement uielement = dependencyObject as UIElement;
					uielement.AddHandler(PresentationSource.SourceChangedEvent, handler);
					FrugalObjectList<RoutedEventHandlerInfo> frugalObjectList = uielement.EventHandlersStore[PresentationSource.SourceChangedEvent];
					if (1 == frugalObjectList.Count)
					{
						uielement.VisualAncestorChanged += uielement.OnVisualAncestorChanged;
						PresentationSource.AddElementToWatchList(uielement);
						return;
					}
				}
				else if (InputElement.IsUIElement3D(dependencyObject))
				{
					UIElement3D uielement3D = dependencyObject as UIElement3D;
					uielement3D.AddHandler(PresentationSource.SourceChangedEvent, handler);
					FrugalObjectList<RoutedEventHandlerInfo> frugalObjectList = uielement3D.EventHandlersStore[PresentationSource.SourceChangedEvent];
					if (1 == frugalObjectList.Count)
					{
						uielement3D.VisualAncestorChanged += uielement3D.OnVisualAncestorChanged;
						PresentationSource.AddElementToWatchList(uielement3D);
						return;
					}
				}
				else
				{
					ContentElement contentElement = dependencyObject as ContentElement;
					contentElement.AddHandler(PresentationSource.SourceChangedEvent, handler);
					FrugalObjectList<RoutedEventHandlerInfo> frugalObjectList = contentElement.EventHandlersStore[PresentationSource.SourceChangedEvent];
					if (1 == frugalObjectList.Count)
					{
						PresentationSource.AddElementToWatchList(contentElement);
					}
				}
			}
		}

		/// <summary>Remove um manipulador para o evento <see langword="SourceChanged" /> do elemento fornecido.</summary>
		/// <param name="e">O elemento do qual remover o manipulador.</param>
		/// <param name="handler">A implementação do manipulador a remover.</param>
		// Token: 0x06000C4F RID: 3151 RVA: 0x0002F31C File Offset: 0x0002E71C
		public static void RemoveSourceChangedHandler(IInputElement e, SourceChangedEventHandler handler)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (!InputElement.IsValid(e))
			{
				throw new ArgumentException(SR.Get("Invalid_IInputElement"), "e");
			}
			DependencyObject dependencyObject = (DependencyObject)e;
			if (handler != null)
			{
				FrugalObjectList<RoutedEventHandlerInfo> frugalObjectList = null;
				if (InputElement.IsUIElement(dependencyObject))
				{
					UIElement uielement = dependencyObject as UIElement;
					uielement.RemoveHandler(PresentationSource.SourceChangedEvent, handler);
					EventHandlersStore eventHandlersStore = uielement.EventHandlersStore;
					if (eventHandlersStore != null)
					{
						frugalObjectList = eventHandlersStore[PresentationSource.SourceChangedEvent];
					}
					if (frugalObjectList == null || frugalObjectList.Count == 0)
					{
						uielement.VisualAncestorChanged -= uielement.OnVisualAncestorChanged;
						PresentationSource.RemoveElementFromWatchList(uielement);
						return;
					}
				}
				else if (InputElement.IsUIElement3D(dependencyObject))
				{
					UIElement3D uielement3D = dependencyObject as UIElement3D;
					uielement3D.RemoveHandler(PresentationSource.SourceChangedEvent, handler);
					EventHandlersStore eventHandlersStore = uielement3D.EventHandlersStore;
					if (eventHandlersStore != null)
					{
						frugalObjectList = eventHandlersStore[PresentationSource.SourceChangedEvent];
					}
					if (frugalObjectList == null || frugalObjectList.Count == 0)
					{
						uielement3D.VisualAncestorChanged -= uielement3D.OnVisualAncestorChanged;
						PresentationSource.RemoveElementFromWatchList(uielement3D);
						return;
					}
				}
				else
				{
					ContentElement contentElement = dependencyObject as ContentElement;
					contentElement.RemoveHandler(PresentationSource.SourceChangedEvent, handler);
					EventHandlersStore eventHandlersStore = contentElement.EventHandlersStore;
					if (eventHandlersStore != null)
					{
						frugalObjectList = eventHandlersStore[PresentationSource.SourceChangedEvent];
					}
					if (frugalObjectList == null || frugalObjectList.Count == 0)
					{
						PresentationSource.RemoveElementFromWatchList(contentElement);
					}
				}
			}
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0002F454 File Offset: 0x0002E854
		[FriendAccessAllowed]
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		internal static void OnAncestorChanged(ContentElement ce)
		{
			if (ce == null)
			{
				throw new ArgumentNullException("ce");
			}
			if ((bool)ce.GetValue(PresentationSource.GetsSourceChangedEventProperty))
			{
				PresentationSource.UpdateSourceOfElement(ce, null, null);
			}
		}

		/// <summary>Obtém o destino visual dos elementos visuais apresentados na origem.</summary>
		/// <returns>Um destino visual (instância de um <see cref="T:System.Windows.Media.CompositionTarget" /> classe derivada).</returns>
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x0002F48C File Offset: 0x0002E88C
		public CompositionTarget CompositionTarget
		{
			[SecurityCritical]
			[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
			get
			{
				return this.GetCompositionTargetCore();
			}
		}

		/// <summary>Quando substituído em uma classe derivada, obtém ou define a raiz visual apresentada na origem.</summary>
		/// <returns>O visual raiz.</returns>
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000C52 RID: 3154
		// (set) Token: 0x06000C53 RID: 3155
		public abstract Visual RootVisual { get; [SecurityCritical] [UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)] [UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)] set; }

		// Token: 0x06000C54 RID: 3156 RVA: 0x0002F4A0 File Offset: 0x0002E8A0
		internal void PushMenuMode()
		{
			this._menuModeCount++;
			if (1 == this._menuModeCount)
			{
				this.OnEnterMenuMode();
			}
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0002F4CC File Offset: 0x0002E8CC
		internal void PopMenuMode()
		{
			if (this._menuModeCount <= 0)
			{
				throw new InvalidOperationException();
			}
			this._menuModeCount--;
			if (this._menuModeCount == 0)
			{
				this.OnLeaveMenuMode();
			}
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0002F504 File Offset: 0x0002E904
		internal virtual void OnEnterMenuMode()
		{
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0002F514 File Offset: 0x0002E914
		internal virtual void OnLeaveMenuMode()
		{
		}

		/// <summary>Quando substituído em uma classe derivada, obtém um valor que declara se o objeto é descartado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o objeto é descartado; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000C58 RID: 3160
		public abstract bool IsDisposed { get; }

		/// <summary>Retorna uma lista de origens.</summary>
		/// <returns>Uma lista de referências fracas.</returns>
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x0002F524 File Offset: 0x0002E924
		public static IEnumerable CurrentSources
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandUIWindowPermission();
				return PresentationSource.CriticalCurrentSources;
			}
		}

		/// <summary>Ocorre quando o conteúdo é processado e está pronto para interação com o usuário.</summary>
		// Token: 0x14000145 RID: 325
		// (add) Token: 0x06000C5A RID: 3162 RVA: 0x0002F53C File Offset: 0x0002E93C
		// (remove) Token: 0x06000C5B RID: 3163 RVA: 0x0002F574 File Offset: 0x0002E974
		public event EventHandler ContentRendered;

		/// <summary>Quando substituído em uma classe derivada, retorna um destino visual para a origem especificada.</summary>
		/// <returns>Retorna um <see cref="T:System.Windows.Media.CompositionTarget" /> que é o destino para a renderização do visual.</returns>
		// Token: 0x06000C5C RID: 3164
		protected abstract CompositionTarget GetCompositionTargetCore();

		/// <summary>Fornece notificação de que a raiz <see cref="T:System.Windows.Media.Visual" /> foi alterada.</summary>
		/// <param name="oldRoot">O <see cref="T:System.Windows.Media.Visual" /> raiz antigo.</param>
		/// <param name="newRoot">O <see cref="T:System.Windows.Media.Visual" /> raiz novo.</param>
		// Token: 0x06000C5D RID: 3165 RVA: 0x0002F5AC File Offset: 0x0002E9AC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected void RootChanged(Visual oldRoot, Visual newRoot)
		{
			PresentationSource presentationSource = null;
			if (oldRoot == newRoot)
			{
				return;
			}
			if (oldRoot != null)
			{
				presentationSource = PresentationSource.CriticalGetPresentationSourceFromElement(oldRoot, PresentationSource.RootSourceProperty);
				oldRoot.ClearValue(PresentationSource.RootSourceProperty);
			}
			if (newRoot != null)
			{
				newRoot.SetValue(PresentationSource.RootSourceProperty, new SecurityCriticalDataForMultipleGetAndSet<PresentationSource>(this));
			}
			UIElement uielement = oldRoot as UIElement;
			UIElement uielement2 = newRoot as UIElement;
			if (uielement != null)
			{
				uielement.UpdateIsVisibleCache();
			}
			if (uielement2 != null)
			{
				uielement2.UpdateIsVisibleCache();
			}
			if (uielement != null)
			{
				uielement.OnPresentationSourceChanged(false);
			}
			if (uielement2 != null)
			{
				uielement2.OnPresentationSourceChanged(true);
			}
			foreach (object obj in PresentationSource._watchers)
			{
				DependencyObject dependencyObject = (DependencyObject)obj;
				if (dependencyObject.Dispatcher == base.Dispatcher)
				{
					PresentationSource presentationSource2 = PresentationSource.CriticalGetPresentationSourceFromElement(dependencyObject, PresentationSource.CachedSourceProperty);
					if (presentationSource == presentationSource2 || presentationSource2 == null)
					{
						PresentationSource.UpdateSourceOfElement(dependencyObject, null, null);
					}
				}
			}
		}

		/// <summary>Adiciona uma instância de classe derivada <see cref="T:System.Windows.PresentationSource" /> à lista de fontes de apresentação conhecidas.</summary>
		// Token: 0x06000C5E RID: 3166 RVA: 0x0002F674 File Offset: 0x0002EA74
		protected void AddSource()
		{
			PresentationSource._sources.Add(this);
		}

		/// <summary>Remove uma instância de classe derivada <see cref="T:System.Windows.PresentationSource" /> da lista de fontes de apresentação conhecidas.</summary>
		// Token: 0x06000C5F RID: 3167 RVA: 0x0002F690 File Offset: 0x0002EA90
		protected void RemoveSource()
		{
			PresentationSource._sources.Remove(this);
		}

		/// <summary>Define a lista de ouvintes para o evento <see cref="E:System.Windows.PresentationSource.ContentRendered" /> para <see langword="null" />.</summary>
		// Token: 0x06000C60 RID: 3168 RVA: 0x0002F6AC File Offset: 0x0002EAAC
		protected void ClearContentRenderedListeners()
		{
			this.ContentRendered = null;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0002F6C0 File Offset: 0x0002EAC0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static void OnVisualAncestorChanged(DependencyObject uie, AncestorChangedEventArgs e)
		{
			if ((bool)uie.GetValue(PresentationSource.GetsSourceChangedEventProperty))
			{
				PresentationSource.UpdateSourceOfElement(uie, e.Ancestor, e.OldParent);
			}
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002F6F4 File Offset: 0x0002EAF4
		[FriendAccessAllowed]
		[SecurityCritical]
		internal static PresentationSource CriticalFromVisual(DependencyObject v)
		{
			return PresentationSource.CriticalFromVisual(v, true);
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002F708 File Offset: 0x0002EB08
		[FriendAccessAllowed]
		[SecurityCritical]
		internal static PresentationSource CriticalFromVisual(DependencyObject v, bool enable2DTo3DTransition)
		{
			if (v == null)
			{
				throw new ArgumentNullException("v");
			}
			PresentationSource presentationSource = PresentationSource.FindSource(v, enable2DTo3DTransition);
			if (presentationSource != null && presentationSource.IsDisposed)
			{
				presentationSource = null;
			}
			return presentationSource;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0002F73C File Offset: 0x0002EB3C
		internal static object FireContentRendered(object arg)
		{
			PresentationSource presentationSource = (PresentationSource)arg;
			if (presentationSource.ContentRendered != null)
			{
				presentationSource.ContentRendered(arg, EventArgs.Empty);
			}
			return null;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002F76C File Offset: 0x0002EB6C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		[FriendAccessAllowed]
		internal static bool UnderSamePresentationSource(params DependencyObject[] visuals)
		{
			if (visuals == null || visuals.Length == 0)
			{
				return true;
			}
			PresentationSource presentationSource = PresentationSource.CriticalFromVisual(visuals[0]);
			int num = visuals.Length;
			for (int i = 1; i < num; i++)
			{
				PresentationSource presentationSource2 = PresentationSource.CriticalFromVisual(visuals[i]);
				if (presentationSource2 != presentationSource)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x0002F7AC File Offset: 0x0002EBAC
		internal static IEnumerable CriticalCurrentSources
		{
			[SecurityCritical]
			get
			{
				return PresentationSource._sources;
			}
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0002F7C0 File Offset: 0x0002EBC0
		[SecurityCritical]
		private static PresentationSource CriticalGetPresentationSourceFromElement(DependencyObject dObject, DependencyProperty dp)
		{
			SecurityCriticalDataForMultipleGetAndSet<PresentationSource> securityCriticalDataForMultipleGetAndSet = (SecurityCriticalDataForMultipleGetAndSet<PresentationSource>)dObject.GetValue(dp);
			PresentationSource result;
			if (securityCriticalDataForMultipleGetAndSet == null || securityCriticalDataForMultipleGetAndSet.Value == null)
			{
				result = null;
			}
			else
			{
				result = securityCriticalDataForMultipleGetAndSet.Value;
			}
			return result;
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0002F7F4 File Offset: 0x0002EBF4
		[SecurityCritical]
		private static void AddElementToWatchList(DependencyObject element)
		{
			if (PresentationSource._watchers.Add(element))
			{
				element.SetValue(PresentationSource.CachedSourceProperty, new SecurityCriticalDataForMultipleGetAndSet<PresentationSource>(PresentationSource.FindSource(element)));
				element.SetValue(PresentationSource.GetsSourceChangedEventProperty, true);
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0002F830 File Offset: 0x0002EC30
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private static void RemoveElementFromWatchList(DependencyObject element)
		{
			if (PresentationSource._watchers.Remove(element))
			{
				element.ClearValue(PresentationSource.CachedSourceProperty);
				element.ClearValue(PresentationSource.GetsSourceChangedEventProperty);
			}
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002F860 File Offset: 0x0002EC60
		[SecurityCritical]
		private static PresentationSource FindSource(DependencyObject o)
		{
			return PresentationSource.FindSource(o, true);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0002F874 File Offset: 0x0002EC74
		[SecurityCritical]
		private static PresentationSource FindSource(DependencyObject o, bool enable2DTo3DTransition)
		{
			PresentationSource result = null;
			DependencyObject rootVisual = InputElement.GetRootVisual(o, enable2DTo3DTransition);
			if (rootVisual != null)
			{
				result = PresentationSource.CriticalGetPresentationSourceFromElement(rootVisual, PresentationSource.RootSourceProperty);
			}
			return result;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0002F89C File Offset: 0x0002EC9C
		[SecurityCritical]
		private static bool UpdateSourceOfElement(DependencyObject doTarget, DependencyObject doAncestor, DependencyObject doOldParent)
		{
			bool result = false;
			PresentationSource presentationSource = PresentationSource.FindSource(doTarget);
			PresentationSource presentationSource2 = PresentationSource.CriticalGetPresentationSourceFromElement(doTarget, PresentationSource.CachedSourceProperty);
			if (presentationSource2 != presentationSource)
			{
				doTarget.SetValue(PresentationSource.CachedSourceProperty, new SecurityCriticalDataForMultipleGetAndSet<PresentationSource>(presentationSource));
				SourceChangedEventArgs sourceChangedEventArgs = new SourceChangedEventArgs(presentationSource2, presentationSource);
				sourceChangedEventArgs.RoutedEvent = PresentationSource.SourceChangedEvent;
				if (InputElement.IsUIElement(doTarget))
				{
					((UIElement)doTarget).RaiseEvent(sourceChangedEventArgs);
				}
				else if (InputElement.IsContentElement(doTarget))
				{
					((ContentElement)doTarget).RaiseEvent(sourceChangedEventArgs);
				}
				else
				{
					((UIElement3D)doTarget).RaiseEvent(sourceChangedEventArgs);
				}
				result = true;
			}
			return result;
		}

		// Token: 0x04000720 RID: 1824
		private int _menuModeCount;

		// Token: 0x04000722 RID: 1826
		[SecurityCritical]
		private static readonly DependencyProperty RootSourceProperty = DependencyProperty.RegisterAttached("RootSource", typeof(SecurityCriticalDataForMultipleGetAndSet<PresentationSource>), typeof(PresentationSource), new PropertyMetadata(null));

		// Token: 0x04000723 RID: 1827
		[SecurityCritical]
		private static readonly DependencyProperty CachedSourceProperty = DependencyProperty.RegisterAttached("CachedSource", typeof(SecurityCriticalDataForMultipleGetAndSet<PresentationSource>), typeof(PresentationSource), new PropertyMetadata(null));

		// Token: 0x04000724 RID: 1828
		private static readonly DependencyProperty GetsSourceChangedEventProperty = DependencyProperty.RegisterAttached("IsBeingWatched", typeof(bool), typeof(PresentationSource), new PropertyMetadata(false));

		// Token: 0x04000725 RID: 1829
		private static readonly RoutedEvent SourceChangedEvent = EventManager.RegisterRoutedEvent("SourceChanged", RoutingStrategy.Direct, typeof(SourceChangedEventHandler), typeof(PresentationSource));

		// Token: 0x04000726 RID: 1830
		private static object _globalLock = new object();

		// Token: 0x04000727 RID: 1831
		private static WeakReferenceList _sources = new WeakReferenceList(PresentationSource._globalLock);

		// Token: 0x04000728 RID: 1832
		private static WeakReferenceList _watchers = new WeakReferenceList(PresentationSource._globalLock);
	}
}
