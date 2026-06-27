using System;
using System.ComponentModel;
using System.Windows.Input.Manipulations;
using System.Windows.Media;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Contém métodos para obter e atualizar informações sobre uma manipulação.</summary>
	// Token: 0x0200026F RID: 623
	public static class Manipulation
	{
		/// <summary>Obtém um valor que indica se uma manipulação está associada ao elemento especificado.</summary>
		/// <param name="element">O elemento a ser verificado.</param>
		/// <returns>
		///   <see langword="true" /> se uma manipulação estiver associada ao elemento especificado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060011A0 RID: 4512 RVA: 0x000424DC File Offset: 0x000418DC
		public static bool IsManipulationActive(UIElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return Manipulation.GetActiveManipulationDevice(element) != null;
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00042500 File Offset: 0x00041900
		private static ManipulationDevice GetActiveManipulationDevice(UIElement element)
		{
			ManipulationDevice manipulationDevice = ManipulationDevice.GetManipulationDevice(element);
			if (manipulationDevice != null && manipulationDevice.IsManipulationActive)
			{
				return manipulationDevice;
			}
			return null;
		}

		/// <summary>Interrompe a manipulação e começa inércia no elemento especificado.</summary>
		/// <param name="element">O elemento no qual começar a inércia.</param>
		// Token: 0x060011A2 RID: 4514 RVA: 0x00042524 File Offset: 0x00041924
		public static void StartInertia(UIElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			ManipulationDevice manipulationDevice = ManipulationDevice.GetManipulationDevice(element);
			if (manipulationDevice != null)
			{
				manipulationDevice.CompleteManipulation(true);
			}
		}

		/// <summary>Conclui a manipulação ativa no elemento especificado. Quando chamado, a entrada de manipulação não é mais controlada e interrompe a inércia no elemento especificado.</summary>
		/// <param name="element">O elemento no qual concluir a manipulação.</param>
		// Token: 0x060011A3 RID: 4515 RVA: 0x00042550 File Offset: 0x00041950
		public static void CompleteManipulation(UIElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (!Manipulation.TryCompleteManipulation(element))
			{
				throw new InvalidOperationException(SR.Get("Manipulation_ManipulationNotActive"));
			}
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00042584 File Offset: 0x00041984
		internal static bool TryCompleteManipulation(UIElement element)
		{
			ManipulationDevice manipulationDevice = ManipulationDevice.GetManipulationDevice(element);
			if (manipulationDevice != null)
			{
				manipulationDevice.CompleteManipulation(false);
				return true;
			}
			return false;
		}

		/// <summary>Define o modo de manipulação para o elemento especificado.</summary>
		/// <param name="element">O elemento no qual definir o modo de manipulação.</param>
		/// <param name="mode">O novo modo de manipulação.</param>
		// Token: 0x060011A5 RID: 4517 RVA: 0x000425A8 File Offset: 0x000419A8
		public static void SetManipulationMode(UIElement element, ManipulationModes mode)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			ManipulationDevice activeManipulationDevice = Manipulation.GetActiveManipulationDevice(element);
			if (activeManipulationDevice != null)
			{
				activeManipulationDevice.ManipulationMode = mode;
				return;
			}
			throw new InvalidOperationException(SR.Get("Manipulation_ManipulationNotActive"));
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Input.ManipulationModes" /> para o elemento especificado.</summary>
		/// <param name="element">O elemento para o qual obter o modo de manipulação.</param>
		/// <returns>Um dos valores de enumeração.</returns>
		// Token: 0x060011A6 RID: 4518 RVA: 0x000425E4 File Offset: 0x000419E4
		public static ManipulationModes GetManipulationMode(UIElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			ManipulationDevice manipulationDevice = ManipulationDevice.GetManipulationDevice(element);
			if (manipulationDevice != null)
			{
				return manipulationDevice.ManipulationMode;
			}
			return ManipulationModes.None;
		}

		/// <summary>Define o elemento que estabelece as coordenadas para a manipulação do elemento especificado.</summary>
		/// <param name="element">O elemento ao qual a manipulação está associada.</param>
		/// <param name="container">O contêiner que define o espaço de coordenadas.</param>
		// Token: 0x060011A7 RID: 4519 RVA: 0x00042614 File Offset: 0x00041A14
		public static void SetManipulationContainer(UIElement element, IInputElement container)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			ManipulationDevice activeManipulationDevice = Manipulation.GetActiveManipulationDevice(element);
			if (activeManipulationDevice != null)
			{
				activeManipulationDevice.ManipulationContainer = container;
				return;
			}
			throw new InvalidOperationException(SR.Get("Manipulation_ManipulationNotActive"));
		}

		/// <summary>Obtém o contêiner que define as coordenadas para a manipulação.</summary>
		/// <param name="element">O elemento no qual há uma manipulação ativa.</param>
		/// <returns>O contêiner que define o espaço de coordenadas.</returns>
		// Token: 0x060011A8 RID: 4520 RVA: 0x00042650 File Offset: 0x00041A50
		public static IInputElement GetManipulationContainer(UIElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			ManipulationDevice manipulationDevice = ManipulationDevice.GetManipulationDevice(element);
			if (manipulationDevice != null)
			{
				return manipulationDevice.ManipulationContainer;
			}
			return null;
		}

		/// <summary>Define a dinamização da manipulação de ponto único do elemento especificado.</summary>
		/// <param name="element">O elemento que tem uma manipulação ativa.</param>
		/// <param name="pivot">Um objeto que descreve a dinamização.</param>
		// Token: 0x060011A9 RID: 4521 RVA: 0x00042680 File Offset: 0x00041A80
		public static void SetManipulationPivot(UIElement element, ManipulationPivot pivot)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			ManipulationDevice activeManipulationDevice = Manipulation.GetActiveManipulationDevice(element);
			if (activeManipulationDevice != null)
			{
				activeManipulationDevice.ManipulationPivot = pivot;
				return;
			}
			throw new InvalidOperationException(SR.Get("Manipulation_ManipulationNotActive"));
		}

		/// <summary>Retorna um objeto que descreve como uma rotação ocorre com um ponto de entrada do usuário.</summary>
		/// <param name="element">O elemento no qual há uma manipulação.</param>
		/// <returns>Um objeto que descreve como uma rotação ocorre com um ponto de entrada do usuário.</returns>
		// Token: 0x060011AA RID: 4522 RVA: 0x000426BC File Offset: 0x00041ABC
		public static ManipulationPivot GetManipulationPivot(UIElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			ManipulationDevice manipulationDevice = ManipulationDevice.GetManipulationDevice(element);
			if (manipulationDevice != null)
			{
				return manipulationDevice.ManipulationPivot;
			}
			return null;
		}

		/// <summary>Associa um objeto <see cref="T:System.Windows.Input.IManipulator" /> ao elemento especificado.</summary>
		/// <param name="element">O elemento ao qual associar o manipulador.</param>
		/// <param name="manipulator">O objeto que fornece a posição da entrada que cria ou é adicionada a uma manipulação.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> é <see langword="null" />.  
		///
		/// ou - 
		/// <paramref name="manipulator" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">A propriedade <see cref="P:System.Windows.UIElement.IsManipulationEnabled" /> no elemento é <see langword="false" />.</exception>
		// Token: 0x060011AB RID: 4523 RVA: 0x000426EC File Offset: 0x00041AEC
		public static void AddManipulator(UIElement element, IManipulator manipulator)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (manipulator == null)
			{
				throw new ArgumentNullException("manipulator");
			}
			if (!element.IsManipulationEnabled)
			{
				throw new InvalidOperationException(SR.Get("Manipulation_ManipulationNotEnabled"));
			}
			ManipulationDevice manipulationDevice = ManipulationDevice.AddManipulationDevice(element);
			manipulationDevice.AddManipulator(manipulator);
		}

		/// <summary>Remove a associação entre o objeto <see cref="T:System.Windows.Input.IManipulator" /> e o elemento especificados.</summary>
		/// <param name="element">O elemento do qual remover o manipulador associado.</param>
		/// <param name="manipulator">O objeto que fornece a posição da entrada.</param>
		// Token: 0x060011AC RID: 4524 RVA: 0x0004273C File Offset: 0x00041B3C
		public static void RemoveManipulator(UIElement element, IManipulator manipulator)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (manipulator == null)
			{
				throw new ArgumentNullException("manipulator");
			}
			if (!Manipulation.TryRemoveManipulator(element, manipulator))
			{
				throw new InvalidOperationException(SR.Get("Manipulation_ManipulationNotActive"));
			}
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00042780 File Offset: 0x00041B80
		internal static bool TryRemoveManipulator(UIElement element, IManipulator manipulator)
		{
			ManipulationDevice manipulationDevice = ManipulationDevice.GetManipulationDevice(element);
			if (manipulationDevice != null)
			{
				manipulationDevice.RemoveManipulator(manipulator);
				return true;
			}
			return false;
		}

		/// <summary>Adiciona parâmetros à manipulação do elemento especificado.</summary>
		/// <param name="element">O elemento que tem a manipulação à qual o parâmetro é adicionado.</param>
		/// <param name="parameter">O parâmetro a adicionar.</param>
		// Token: 0x060011AE RID: 4526 RVA: 0x000427A4 File Offset: 0x00041BA4
		[Browsable(false)]
		public static void SetManipulationParameter(UIElement element, ManipulationParameters2D parameter)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (parameter == null)
			{
				throw new ArgumentNullException("parameter");
			}
			ManipulationDevice activeManipulationDevice = Manipulation.GetActiveManipulationDevice(element);
			if (activeManipulationDevice != null)
			{
				activeManipulationDevice.SetManipulationParameters(parameter);
				return;
			}
			throw new InvalidOperationException(SR.Get("Manipulation_ManipulationNotActive"));
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x000427F0 File Offset: 0x00041BF0
		internal static UIElement FindManipulationParent(Visual visual)
		{
			while (visual != null)
			{
				UIElement uielement = visual as UIElement;
				if (uielement != null && uielement.IsManipulationEnabled)
				{
					return uielement;
				}
				visual = (VisualTreeHelper.GetParent(visual) as Visual);
			}
			return null;
		}

		// Token: 0x040009A3 RID: 2467
		internal static readonly RoutedEvent ManipulationStartingEvent = EventManager.RegisterRoutedEvent("ManipulationStarting", RoutingStrategy.Bubble, typeof(EventHandler<ManipulationStartingEventArgs>), typeof(ManipulationDevice));

		// Token: 0x040009A4 RID: 2468
		internal static readonly RoutedEvent ManipulationStartedEvent = EventManager.RegisterRoutedEvent("ManipulationStarted", RoutingStrategy.Bubble, typeof(EventHandler<ManipulationStartedEventArgs>), typeof(ManipulationDevice));

		// Token: 0x040009A5 RID: 2469
		internal static readonly RoutedEvent ManipulationDeltaEvent = EventManager.RegisterRoutedEvent("ManipulationDelta", RoutingStrategy.Bubble, typeof(EventHandler<ManipulationDeltaEventArgs>), typeof(ManipulationDevice));

		// Token: 0x040009A6 RID: 2470
		internal static readonly RoutedEvent ManipulationInertiaStartingEvent = EventManager.RegisterRoutedEvent("ManipulationInertiaStarting", RoutingStrategy.Bubble, typeof(EventHandler<ManipulationInertiaStartingEventArgs>), typeof(ManipulationDevice));

		// Token: 0x040009A7 RID: 2471
		internal static readonly RoutedEvent ManipulationBoundaryFeedbackEvent = EventManager.RegisterRoutedEvent("ManipulationBoundaryFeedback", RoutingStrategy.Bubble, typeof(EventHandler<ManipulationBoundaryFeedbackEventArgs>), typeof(ManipulationDevice));

		// Token: 0x040009A8 RID: 2472
		internal static readonly RoutedEvent ManipulationCompletedEvent = EventManager.RegisterRoutedEvent("ManipulationCompleted", RoutingStrategy.Bubble, typeof(EventHandler<ManipulationCompletedEventArgs>), typeof(ManipulationDevice));
	}
}
