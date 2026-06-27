using System;
using System.Windows.Media.Animation;
using MS.Internal.KnownBoxes;

namespace System.Windows.Media
{
	/// <summary>Representa um segmento de um objeto <see cref="T:System.Windows.Media.PathFigure" />.</summary>
	// Token: 0x020003C4 RID: 964
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public abstract class PathSegment : Animatable
	{
		/// <summary>Cria uma cópia modificável deste <see cref="T:System.Windows.Media.PathSegment" /> fazendo cópias em profundidade de seus valores.</summary>
		/// <returns>Uma cópia em profundidade modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado retorna <see langword="false" /> mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem é <see langword="true." /></returns>
		// Token: 0x060025BA RID: 9658 RVA: 0x00096DD4 File Offset: 0x000961D4
		public new PathSegment Clone()
		{
			return (PathSegment)base.Clone();
		}

		/// <summary>Cria uma cópia modificável deste objeto <see cref="T:System.Windows.Media.PathSegment" /> fazendo cópias em profundidade de seus valores. Este método não copia referências de recurso, associações de dados e animações, embora ele copie os valores atuais.</summary>
		/// <returns>Uma cópia em profundidade modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> será <see langword="false" /> mesmo que a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem seja <see langword="true" />.</returns>
		// Token: 0x060025BB RID: 9659 RVA: 0x00096DEC File Offset: 0x000961EC
		public new PathSegment CloneCurrentValue()
		{
			return (PathSegment)base.CloneCurrentValue();
		}

		/// <summary>Obtém ou define um valor que indica se o segmento é traçado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o segmento é traçado quando um <see cref="T:System.Windows.Media.Pen" /> é usado para renderizar o segmento; caso contrário, o segmento não é traçado. O padrão é <see langword="true" />.</returns>
		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060025BC RID: 9660 RVA: 0x00096E04 File Offset: 0x00096204
		// (set) Token: 0x060025BD RID: 9661 RVA: 0x00096E24 File Offset: 0x00096224
		public bool IsStroked
		{
			get
			{
				return (bool)base.GetValue(PathSegment.IsStrokedProperty);
			}
			set
			{
				base.SetValueInternal(PathSegment.IsStrokedProperty, BooleanBoxes.Box(value));
			}
		}

		/// <summary>Obtém ou define um valor que indica se a junção entre este <see cref="T:System.Windows.Media.PathSegment" /> e o <see cref="T:System.Windows.Media.PathSegment" /> anterior é tratada como um canto quando ela é traçada com um <see cref="T:System.Windows.Media.Pen" />.</summary>
		/// <returns>
		///   <see langword="true" /> Se a junção entre esse <see cref="T:System.Windows.Media.PathSegment" /> e a anterior <see cref="T:System.Windows.Media.PathSegment" /> não deve ser tratado como um canto; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060025BE RID: 9662 RVA: 0x00096E44 File Offset: 0x00096244
		// (set) Token: 0x060025BF RID: 9663 RVA: 0x00096E64 File Offset: 0x00096264
		public bool IsSmoothJoin
		{
			get
			{
				return (bool)base.GetValue(PathSegment.IsSmoothJoinProperty);
			}
			set
			{
				base.SetValueInternal(PathSegment.IsSmoothJoinProperty, BooleanBoxes.Box(value));
			}
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x00096E84 File Offset: 0x00096284
		static PathSegment()
		{
			Type typeFromHandle = typeof(PathSegment);
			PathSegment.IsStrokedProperty = Animatable.RegisterProperty("IsStroked", typeof(bool), typeFromHandle, true, null, null, false, null);
			PathSegment.IsSmoothJoinProperty = Animatable.RegisterProperty("IsSmoothJoin", typeof(bool), typeFromHandle, false, null, null, false, null);
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x00096EE4 File Offset: 0x000962E4
		internal PathSegment()
		{
		}

		// Token: 0x060025C2 RID: 9666
		internal abstract void AddToFigure(Matrix matrix, PathFigure figure, ref Point current);

		// Token: 0x060025C3 RID: 9667 RVA: 0x00096EF8 File Offset: 0x000962F8
		internal virtual bool IsEmpty()
		{
			return false;
		}

		// Token: 0x060025C4 RID: 9668
		internal abstract bool IsCurved();

		// Token: 0x060025C5 RID: 9669
		internal abstract string ConvertToString(string format, IFormatProvider provider);

		// Token: 0x060025C6 RID: 9670
		internal abstract void SerializeData(StreamGeometryContext ctx);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.PathSegment.IsStroked" />.</summary>
		// Token: 0x040011A1 RID: 4513
		public static readonly DependencyProperty IsStrokedProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.PathSegment.IsSmoothJoin" />.</summary>
		// Token: 0x040011A2 RID: 4514
		public static readonly DependencyProperty IsSmoothJoinProperty;

		// Token: 0x040011A3 RID: 4515
		internal const bool c_IsStroked = true;

		// Token: 0x040011A4 RID: 4516
		internal const bool c_IsSmoothJoin = false;

		// Token: 0x040011A5 RID: 4517
		internal const bool c_isStrokedDefault = true;
	}
}
