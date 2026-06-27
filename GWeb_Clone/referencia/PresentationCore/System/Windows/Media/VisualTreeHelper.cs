using System;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;
using MS.Internal;
using MS.Internal.Media;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Fornece métodos de utilitário que executam tarefas comuns envolvendo nós em uma árvore visual.</summary>
	// Token: 0x0200044D RID: 1101
	public static class VisualTreeHelper
	{
		// Token: 0x06002D9E RID: 11678 RVA: 0x000B6A1C File Offset: 0x000B5E1C
		private static void CheckVisualReferenceArgument(DependencyObject reference)
		{
			if (reference == null)
			{
				throw new ArgumentNullException("reference");
			}
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x000B6A38 File Offset: 0x000B5E38
		[FriendAccessAllowed]
		internal static bool IsVisualType(DependencyObject reference)
		{
			return reference is Visual || reference is Visual3D;
		}

		/// <summary>Retorna o número de filhos que o objeto visual especificado contém.</summary>
		/// <param name="reference">O visual pai que é referenciado como um <see cref="T:System.Windows.DependencyObject" />.</param>
		/// <returns>O número de visuais filho que o visual pai contém.</returns>
		// Token: 0x06002DA0 RID: 11680 RVA: 0x000B6A58 File Offset: 0x000B5E58
		public static int GetChildrenCount(DependencyObject reference)
		{
			Visual visual;
			Visual3D visual3D;
			VisualTreeUtils.AsNonNullVisual(reference, out visual, out visual3D);
			if (visual3D != null)
			{
				return visual3D.InternalVisual2DOr3DChildrenCount;
			}
			return visual.InternalVisual2DOr3DChildrenCount;
		}

		/// <summary>Retorna o objeto filho visual do índice de coleção especificado dentro de um pai especificado.</summary>
		/// <param name="reference">O visual pai, referenciado como um <see cref="T:System.Windows.DependencyObject" />.</param>
		/// <param name="childIndex">O índice que representa o visual filho que é contido pelo <paramref name="reference" />.</param>
		/// <returns>O valor de índice do objeto filho visual.</returns>
		// Token: 0x06002DA1 RID: 11681 RVA: 0x000B6A80 File Offset: 0x000B5E80
		public static DependencyObject GetChild(DependencyObject reference, int childIndex)
		{
			Visual visual;
			Visual3D visual3D;
			VisualTreeUtils.AsNonNullVisual(reference, out visual, out visual3D);
			if (visual3D != null)
			{
				return visual3D.InternalGet2DOr3DVisualChild(childIndex);
			}
			return visual.InternalGet2DOr3DVisualChild(childIndex);
		}

		/// <summary>Obtém as informações de DPI com as quais esse Visual é medido e renderizado.</summary>
		/// <param name="visual">O objeto de destino do Visual.</param>
		/// <returns>Um valor de DPIScale.</returns>
		// Token: 0x06002DA2 RID: 11682 RVA: 0x000B6AAC File Offset: 0x000B5EAC
		public static DpiScale GetDpi(Visual visual)
		{
			return visual.GetDpi();
		}

		/// <summary>Atualiza as informações de DPI de um Visual. Só pode ser chamado em um Visual sem pai.</summary>
		/// <param name="visual">O destino de um objeto Visual.</param>
		/// <param name="dpiInfo">Informações de DPI para o Visual de destino.</param>
		// Token: 0x06002DA3 RID: 11683 RVA: 0x000B6AC0 File Offset: 0x000B5EC0
		public static void SetRootDpi(Visual visual, DpiScale dpiInfo)
		{
			if (dpiInfo == null)
			{
				throw new NullReferenceException("dpiInfo cannot be null");
			}
			if (visual.InternalVisualParent != null)
			{
				throw new InvalidOperationException("UpdateDPI should only be called on the root of a Visual tree");
			}
			DpiFlags dpiFlags = DpiUtil.UpdateDpiScalesAndGetIndex(dpiInfo.PixelsPerInchX, dpiInfo.PixelsPerInchY);
			visual.RecursiveSetDpiScaleVisualFlags(new DpiRecursiveChangeArgs(dpiFlags, visual.GetDpi(), dpiInfo));
		}

		/// <summary>Retorna um valor <see cref="T:System.Windows.DependencyObject" /> que representa o pai do objeto visual.</summary>
		/// <param name="reference">O visual cujo pai é retornado.</param>
		/// <returns>O pai do visual.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reference" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="reference" /> não é um objeto <see cref="T:System.Windows.Media.Visual" /> ou <see cref="T:System.Windows.Media.Media3D.Visual3D" />.</exception>
		// Token: 0x06002DA4 RID: 11684 RVA: 0x000B6B1C File Offset: 0x000B5F1C
		public static DependencyObject GetParent(DependencyObject reference)
		{
			Visual visual;
			Visual3D visual3D;
			VisualTreeUtils.AsNonNullVisual(reference, out visual, out visual3D);
			if (visual3D != null)
			{
				return visual3D.InternalVisualParent;
			}
			return visual.InternalVisualParent;
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x000B6B44 File Offset: 0x000B5F44
		[FriendAccessAllowed]
		internal static DependencyObject GetParentInternal(DependencyObject reference)
		{
			Visual visual;
			Visual3D visual3D;
			VisualTreeUtils.AsVisualInternal(reference, out visual, out visual3D);
			if (visual != null)
			{
				return visual.InternalVisualParent;
			}
			if (visual3D != null)
			{
				return visual3D.InternalVisualParent;
			}
			return null;
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x000B6B74 File Offset: 0x000B5F74
		internal static Visual GetContainingVisual2D(DependencyObject reference)
		{
			Visual visual = null;
			while (reference != null)
			{
				visual = (reference as Visual);
				if (visual != null)
				{
					break;
				}
				reference = VisualTreeHelper.GetParent(reference);
			}
			return visual;
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x000B6B9C File Offset: 0x000B5F9C
		internal static Visual3D GetContainingVisual3D(DependencyObject reference)
		{
			Visual3D visual3D = null;
			while (reference != null)
			{
				visual3D = (reference as Visual3D);
				if (visual3D != null)
				{
					break;
				}
				reference = VisualTreeHelper.GetParent(reference);
			}
			return visual3D;
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x000B6BC4 File Offset: 0x000B5FC4
		internal static bool IsAncestorOf(DependencyObject reference, DependencyObject descendant)
		{
			Visual visual;
			Visual3D visual3D;
			VisualTreeUtils.AsNonNullVisual(reference, out visual, out visual3D);
			if (visual3D != null)
			{
				return visual3D.IsAncestorOf(descendant);
			}
			return visual.IsAncestorOf(descendant);
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x000B6BF0 File Offset: 0x000B5FF0
		internal static bool IsAncestorOf(DependencyObject ancestor, DependencyObject descendant, Type stopType)
		{
			if (ancestor == null)
			{
				throw new ArgumentNullException("ancestor");
			}
			if (descendant == null)
			{
				throw new ArgumentNullException("descendant");
			}
			VisualTreeUtils.EnsureVisual(ancestor);
			VisualTreeUtils.EnsureVisual(descendant);
			DependencyObject dependencyObject = descendant;
			while (dependencyObject != null && dependencyObject != ancestor && !stopType.IsInstanceOfType(dependencyObject))
			{
				Visual visual;
				Visual3D visual3D;
				if ((visual = (dependencyObject as Visual)) != null)
				{
					dependencyObject = visual.InternalVisualParent;
				}
				else if ((visual3D = (dependencyObject as Visual3D)) != null)
				{
					dependencyObject = visual3D.InternalVisualParent;
				}
				else
				{
					dependencyObject = null;
				}
			}
			return dependencyObject == ancestor;
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000B6C68 File Offset: 0x000B6068
		internal static DependencyObject FindCommonAncestor(DependencyObject reference, DependencyObject otherVisual)
		{
			Visual visual;
			Visual3D visual3D;
			VisualTreeUtils.AsNonNullVisual(reference, out visual, out visual3D);
			if (visual3D != null)
			{
				return visual3D.FindCommonVisualAncestor(otherVisual);
			}
			return visual.FindCommonVisualAncestor(otherVisual);
		}

		/// <summary>Retorna a região de recorte do <see cref="T:System.Windows.Media.Visual" /> especificado como um valor <see cref="T:System.Windows.Media.Geometry" />.</summary>
		/// <param name="reference">O <see cref="T:System.Windows.Media.Visual" /> cujo valor da região de recorte é retornado.</param>
		/// <returns>O valor da região de recorte do <see cref="T:System.Windows.Media.Visual" /> retornado como um tipo <see cref="T:System.Windows.Media.Geometry" />.</returns>
		// Token: 0x06002DAB RID: 11691 RVA: 0x000B6C94 File Offset: 0x000B6094
		public static Geometry GetClip(Visual reference)
		{
			VisualTreeHelper.CheckVisualReferenceArgument(reference);
			return reference.VisualClip;
		}

		/// <summary>Retorna a opacidade do <see cref="T:System.Windows.Media.Visual" />.</summary>
		/// <param name="reference">O <see cref="T:System.Windows.Media.Visual" /> cujo valor de opacidade é retornado.</param>
		/// <returns>Um valor do tipo <see cref="T:System.Double" /> que representa o valor de opacidade do <see cref="T:System.Windows.Media.Visual" />.</returns>
		// Token: 0x06002DAC RID: 11692 RVA: 0x000B6CB0 File Offset: 0x000B60B0
		public static double GetOpacity(Visual reference)
		{
			VisualTreeHelper.CheckVisualReferenceArgument(reference);
			return reference.VisualOpacity;
		}

		/// <summary>Retorna um valor <see cref="T:System.Windows.Media.Brush" /> que representa a máscara de opacidade do <see cref="T:System.Windows.Media.Visual" />.</summary>
		/// <param name="reference">O <see cref="T:System.Windows.Media.Visual" /> cujo valor da máscara de opacidade é retornado.</param>
		/// <returns>Um valor do tipo <see cref="T:System.Windows.Media.Brush" /> que representa o valor da máscara de opacidade do <see cref="T:System.Windows.Media.Visual" />.</returns>
		// Token: 0x06002DAD RID: 11693 RVA: 0x000B6CCC File Offset: 0x000B60CC
		public static Brush GetOpacityMask(Visual reference)
		{
			VisualTreeHelper.CheckVisualReferenceArgument(reference);
			return reference.VisualOpacityMask;
		}

		/// <summary>Retorna o deslocamento do <see cref="T:System.Windows.Media.Visual" />.</summary>
		/// <param name="reference">O <see cref="T:System.Windows.Media.Visual" /> cujo deslocamento é retornado.</param>
		/// <returns>Um <see cref="T:System.Windows.Vector" /> que representa o valor de deslocamento do <see cref="T:System.Windows.Media.Visual" />.</returns>
		// Token: 0x06002DAE RID: 11694 RVA: 0x000B6CE8 File Offset: 0x000B60E8
		public static Vector GetOffset(Visual reference)
		{
			VisualTreeHelper.CheckVisualReferenceArgument(reference);
			return reference.VisualOffset;
		}

		/// <summary>Retorna um valor <see cref="T:System.Windows.Media.Transform" /> para o <see cref="T:System.Windows.Media.Visual" />.</summary>
		/// <param name="reference">O <see cref="T:System.Windows.Media.Visual" /> cujo valor de transformação é retornado.</param>
		/// <returns>O valor de transformação do <see cref="T:System.Windows.Media.Visual" /> ou <see langword="null" /> se <paramref name="reference" /> não tiver uma transformação definida.</returns>
		// Token: 0x06002DAF RID: 11695 RVA: 0x000B6D04 File Offset: 0x000B6104
		public static Transform GetTransform(Visual reference)
		{
			VisualTreeHelper.CheckVisualReferenceArgument(reference);
			return reference.VisualTransform;
		}

		/// <summary>Retorna uma coleção de diretrizes de coordenada X (vertical).</summary>
		/// <param name="reference">O <see cref="T:System.Windows.Media.Visual" /> cuja coleção de diretrizes de coordenada X é retornada.</param>
		/// <returns>A coleção de diretrizes de coordenada X do <see cref="T:System.Windows.Media.Visual" />.</returns>
		// Token: 0x06002DB0 RID: 11696 RVA: 0x000B6D20 File Offset: 0x000B6120
		public static DoubleCollection GetXSnappingGuidelines(Visual reference)
		{
			VisualTreeHelper.CheckVisualReferenceArgument(reference);
			return reference.VisualXSnappingGuidelines;
		}

		/// <summary>Retorna uma coleção de diretrizes de coordenada Y (horizontal).</summary>
		/// <param name="reference">O <see cref="T:System.Windows.Media.Visual" /> cuja coleção de diretrizes de coordenada Y é retornada.</param>
		/// <returns>A coleção de diretrizes de coordenada Y do <see cref="T:System.Windows.Media.Visual" />.</returns>
		// Token: 0x06002DB1 RID: 11697 RVA: 0x000B6D3C File Offset: 0x000B613C
		public static DoubleCollection GetYSnappingGuidelines(Visual reference)
		{
			VisualTreeHelper.CheckVisualReferenceArgument(reference);
			return reference.VisualYSnappingGuidelines;
		}

		/// <summary>Retorna o conteúdo do desenho do <see cref="T:System.Windows.Media.Visual" /> especificado.</summary>
		/// <param name="reference">O <see cref="T:System.Windows.Media.Visual" /> cujo conteúdo do desenho é retornado.</param>
		/// <returns>O conteúdo do desenho do <see cref="T:System.Windows.Media.Visual" /> retornado como um tipo <see cref="T:System.Windows.Media.DrawingGroup" />.</returns>
		// Token: 0x06002DB2 RID: 11698 RVA: 0x000B6D58 File Offset: 0x000B6158
		public static DrawingGroup GetDrawing(Visual reference)
		{
			VisualTreeHelper.CheckVisualReferenceArgument(reference);
			return reference.GetDrawing();
		}

		/// <summary>Retorna o retângulo da caixa delimitadora armazenada em cache para o <see cref="T:System.Windows.Media.Visual" /> especificado.</summary>
		/// <param name="reference">O <see cref="T:System.Windows.Media.Visual" /> cujo valor da caixa delimitadora é calculado.</param>
		/// <returns>O retângulo da caixa delimitadora para o <see cref="T:System.Windows.Media.Visual" />.</returns>
		// Token: 0x06002DB3 RID: 11699 RVA: 0x000B6D74 File Offset: 0x000B6174
		public static Rect GetContentBounds(Visual reference)
		{
			VisualTreeHelper.CheckVisualReferenceArgument(reference);
			return reference.VisualContentBounds;
		}

		/// <summary>Retorna o retângulo da caixa delimitadora armazenada em cache para o <see cref="T:System.Windows.Media.Media3D.Visual3D" /> especificado.</summary>
		/// <param name="reference">O visual 3D cujo valor da caixa delimitadora é calculado.</param>
		/// <returns>O retângulo 3D da caixa delimitadora para o <see cref="T:System.Windows.Media.Media3D.Visual3D" />.</returns>
		// Token: 0x06002DB4 RID: 11700 RVA: 0x000B6D90 File Offset: 0x000B6190
		public static Rect3D GetContentBounds(Visual3D reference)
		{
			VisualTreeHelper.CheckVisualReferenceArgument(reference);
			return reference.VisualContentBounds;
		}

		/// <summary>Retorna a união de todas as caixas delimitadoras de conteúdo para todos os descendentes do <see cref="T:System.Windows.Media.Visual" />, o que inclui a caixa delimitadora de conteúdo do <see cref="T:System.Windows.Media.Visual" />.</summary>
		/// <param name="reference">O <see cref="T:System.Windows.Media.Visual" /> cujo valor da caixa delimitadora para todos os descendentes é computado.</param>
		/// <returns>O retângulo da caixa delimitadora para o <see cref="T:System.Windows.Media.Visual" /> especificado.</returns>
		// Token: 0x06002DB5 RID: 11701 RVA: 0x000B6DAC File Offset: 0x000B61AC
		public static Rect GetDescendantBounds(Visual reference)
		{
			VisualTreeHelper.CheckVisualReferenceArgument(reference);
			return reference.VisualDescendantBounds;
		}

		/// <summary>Retorna a união de todas as caixas delimitadoras de conteúdo para todos os descendentes do <see cref="T:System.Windows.Media.Media3D.Visual3D" /> especificado, o que inclui a caixa delimitadora de conteúdo do <see cref="T:System.Windows.Media.Media3D.Visual3D" />.</summary>
		/// <param name="reference">O visual 3D cujo valor da caixa delimitadora para todos os descendentes é calculado.</param>
		/// <returns>Retorna o retângulo 3D da caixa delimitadora para o visual 3D.</returns>
		// Token: 0x06002DB6 RID: 11702 RVA: 0x000B6DC8 File Offset: 0x000B61C8
		public static Rect3D GetDescendantBounds(Visual3D reference)
		{
			VisualTreeHelper.CheckVisualReferenceArgument(reference);
			return reference.VisualDescendantBounds;
		}

		/// <summary>Retorna o valor <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> para o <see cref="T:System.Windows.Media.Visual" /> especificado.</summary>
		/// <param name="reference">O <see cref="T:System.Windows.Media.Visual" /> que contém o efeito de bitmap.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> para <see cref="T:System.Windows.Media.Visual" />.</returns>
		// Token: 0x06002DB7 RID: 11703 RVA: 0x000B6DE4 File Offset: 0x000B61E4
		public static BitmapEffect GetBitmapEffect(Visual reference)
		{
			VisualTreeHelper.CheckVisualReferenceArgument(reference);
			return reference.VisualBitmapEffect;
		}

		/// <summary>Retorna o valor <see cref="T:System.Windows.Media.Effects.BitmapEffectInput" /> para o <see cref="T:System.Windows.Media.Visual" /> especificado.</summary>
		/// <param name="reference">O <see cref="T:System.Windows.Media.Visual" /> que contém o valor de entrada do efeito de bitmap.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Effects.BitmapEffectInput" /> para <see cref="T:System.Windows.Media.Visual" />.</returns>
		// Token: 0x06002DB8 RID: 11704 RVA: 0x000B6E00 File Offset: 0x000B6200
		public static BitmapEffectInput GetBitmapEffectInput(Visual reference)
		{
			VisualTreeHelper.CheckVisualReferenceArgument(reference);
			return reference.VisualBitmapEffectInput;
		}

		/// <summary>Obtém o efeito de bitmap do <see cref="T:System.Windows.Media.Visual" /> especificado.</summary>
		/// <param name="reference">O <see cref="T:System.Windows.Media.Visual" /> para o qual o efeito de bitmap será obtido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Effects.Effect" /> aplicado a <paramref name="reference" />.</returns>
		// Token: 0x06002DB9 RID: 11705 RVA: 0x000B6E1C File Offset: 0x000B621C
		public static Effect GetEffect(Visual reference)
		{
			VisualTreeHelper.CheckVisualReferenceArgument(reference);
			return reference.VisualEffect;
		}

		/// <summary>Recupera a representação armazenada em cache do <see cref="T:System.Windows.Media.Visual" /> especificado.</summary>
		/// <param name="reference">O <see cref="T:System.Windows.Media.Visual" /> para o qual o <see cref="T:System.Windows.Media.CacheMode" /> será obtido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.CacheMode" /> para <paramref name="reference" />.</returns>
		// Token: 0x06002DBA RID: 11706 RVA: 0x000B6E38 File Offset: 0x000B6238
		public static CacheMode GetCacheMode(Visual reference)
		{
			VisualTreeHelper.CheckVisualReferenceArgument(reference);
			return reference.VisualCacheMode;
		}

		/// <summary>Retorna o modo de borda do <see cref="T:System.Windows.Media.Visual" /> especificado como um valor <see cref="T:System.Windows.Media.EdgeMode" />.</summary>
		/// <param name="reference">O <see cref="T:System.Windows.Media.Visual" /> cujo valor do modo de borda é retornado.</param>
		/// <returns>O valor <see cref="T:System.Windows.Media.EdgeMode" /> do <see cref="T:System.Windows.Media.Visual" />.</returns>
		// Token: 0x06002DBB RID: 11707 RVA: 0x000B6E54 File Offset: 0x000B6254
		public static EdgeMode GetEdgeMode(Visual reference)
		{
			VisualTreeHelper.CheckVisualReferenceArgument(reference);
			return reference.VisualEdgeMode;
		}

		/// <summary>Retorna o primeiro objeto <see cref="T:System.Windows.Media.Visual" /> de um teste de clique especificando um <see cref="T:System.Windows.Point" />.</summary>
		/// <param name="reference">O <see cref="T:System.Windows.Media.Visual" /> ao qual será aplicado o teste de clique.</param>
		/// <param name="point">O valor do ponto no qual será feito o teste de clique.</param>
		/// <returns>O resultado do teste de clique do <see cref="T:System.Windows.Media.Visual" />, retornado como um tipo <see cref="T:System.Windows.Media.HitTestResult" />.</returns>
		// Token: 0x06002DBC RID: 11708 RVA: 0x000B6E70 File Offset: 0x000B6270
		public static HitTestResult HitTest(Visual reference, Point point)
		{
			return VisualTreeHelper.HitTest(reference, point, true);
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x000B6E88 File Offset: 0x000B6288
		[FriendAccessAllowed]
		internal static HitTestResult HitTest(Visual reference, Point point, bool include2DOn3D)
		{
			VisualTreeHelper.CheckVisualReferenceArgument(reference);
			return reference.HitTest(point, include2DOn3D);
		}

		/// <summary>Inicia um teste de clique no <see cref="T:System.Windows.Media.Visual" /> especificado, com métodos <see cref="T:System.Windows.Media.HitTestFilterCallback" /> e <see cref="T:System.Windows.Media.HitTestResultCallback" /> definidos pelo chamador.</summary>
		/// <param name="reference">O <see cref="T:System.Windows.Media.Visual" /> ao qual será aplicado o teste de clique.</param>
		/// <param name="filterCallback">O método que representa o valor do retorno de chamada do filtro do teste de clique.</param>
		/// <param name="resultCallback">O método que representa o valor do retorno de chamada do resultado do teste de clique.</param>
		/// <param name="hitTestParameters">O valor do parâmetro ao qual o teste de clique será comparado.</param>
		// Token: 0x06002DBE RID: 11710 RVA: 0x000B6EA4 File Offset: 0x000B62A4
		public static void HitTest(Visual reference, HitTestFilterCallback filterCallback, HitTestResultCallback resultCallback, HitTestParameters hitTestParameters)
		{
			VisualTreeHelper.CheckVisualReferenceArgument(reference);
			reference.HitTest(filterCallback, resultCallback, hitTestParameters);
		}

		/// <summary>Inicia um teste de clique no <see cref="T:System.Windows.Media.Media3D.Visual3D" /> especificado, com métodos <see cref="T:System.Windows.Media.HitTestFilterCallback" /> e <see cref="T:System.Windows.Media.HitTestResultCallback" /> definidos pelo chamador.</summary>
		/// <param name="reference">O <see cref="T:System.Windows.Media.Media3D.Visual3D" /> ao qual será aplicado o teste de clique.</param>
		/// <param name="filterCallback">O método que representa o valor do retorno de chamada do filtro do teste de clique.</param>
		/// <param name="resultCallback">O método que representa o valor do retorno de chamada do resultado do teste de clique.</param>
		/// <param name="hitTestParameters">O valor do parâmetro 3D com o qual o teste de clique será executado.</param>
		// Token: 0x06002DBF RID: 11711 RVA: 0x000B6EC0 File Offset: 0x000B62C0
		public static void HitTest(Visual3D reference, HitTestFilterCallback filterCallback, HitTestResultCallback resultCallback, HitTestParameters3D hitTestParameters)
		{
			VisualTreeHelper.CheckVisualReferenceArgument(reference);
			reference.HitTest(filterCallback, resultCallback, hitTestParameters);
		}
	}
}
