using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace System.Windows.Media
{
	/// <summary>Descreve uma geometria usando comandos de desenho. Essa classe é usada com a classe <see cref="T:System.Windows.Media.StreamGeometry" /> para criar uma geometria leve que não oferece suporte a vinculação de dados, animação ou modificação.</summary>
	// Token: 0x02000441 RID: 1089
	public abstract class StreamGeometryContext : DispatcherObject, IDisposable
	{
		// Token: 0x06002C6B RID: 11371 RVA: 0x000B181C File Offset: 0x000B0C1C
		internal StreamGeometryContext()
		{
		}

		/// <summary>Este membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
		// Token: 0x06002C6C RID: 11372 RVA: 0x000B1830 File Offset: 0x000B0C30
		void IDisposable.Dispose()
		{
			base.VerifyAccess();
			this.DisposeCore();
			GC.SuppressFinalize(this);
		}

		/// <summary>Fecha este contexto e libera seu conteúdo para que ele possa ser renderizado.</summary>
		/// <exception cref="T:System.ObjectDisposedException">Este contexto já foi fechado ou descartado.</exception>
		// Token: 0x06002C6D RID: 11373 RVA: 0x000B1850 File Offset: 0x000B0C50
		public virtual void Close()
		{
			this.DisposeCore();
		}

		/// <summary>Especifica o ponto inicial de uma nova figura.</summary>
		/// <param name="startPoint">O <see cref="T:System.Windows.Point" /> no qual a figura começa.</param>
		/// <param name="isFilled">
		///   <see langword="true" /> para usar a área contida nesta figura para testes de ocorrência, renderização e recorte, caso contrário, <see langword="false" />.</param>
		/// <param name="isClosed">
		///   <see langword="true" /> para fechar a figura, caso contrário, <see langword="false" />. Por exemplo, se duas linhas de conexão forem desenhadas e <paramref name="isClosed" /> for definido como <see langword="false" />, o desenho será de apenas duas linhas, mas se <paramref name="isClosed" /> for definido como <see langword="true" />, as duas linhas serão fechadas para criar um triângulo.</param>
		// Token: 0x06002C6E RID: 11374
		public abstract void BeginFigure(Point startPoint, bool isFilled, bool isClosed);

		/// <summary>Desenha uma linha reta para o <see cref="T:System.Windows.Point" /> especificado.</summary>
		/// <param name="point">O ponto de destino para o final da linha.</param>
		/// <param name="isStroked">
		///   <see langword="true" /> para deixar o segmento tracejado quando uma <see cref="T:System.Windows.Media.Pen" /> for usada para renderizar o segmento, caso contrário, <see langword="false" />.</param>
		/// <param name="isSmoothJoin">
		///   <see langword="true" /> para tratar a junção entre esse segmento e o segmento anterior como um canto quando traçados com um <see cref="T:System.Windows.Media.Pen" />; caso contrário, <see langword="false" />.</param>
		/// <exception cref="T:System.InvalidOperationException">Foi feita uma tentativa para adicionar um segmento sem iniciar uma figura ao chamar o método <see cref="M:System.Windows.Media.StreamGeometryContext.BeginFigure(System.Windows.Point,System.Boolean,System.Boolean)" />.</exception>
		// Token: 0x06002C6F RID: 11375
		public abstract void LineTo(Point point, bool isStroked, bool isSmoothJoin);

		/// <summary>Desenha uma curva de Bezier quadrática.</summary>
		/// <param name="point1">O ponto de controle usado para especificar a forma da curva.</param>
		/// <param name="point2">O ponto de destino do final da curva.</param>
		/// <param name="isStroked">
		///   <see langword="true" /> para deixar o segmento tracejado quando uma <see cref="T:System.Windows.Media.Pen" /> for usada para renderizar o segmento, caso contrário, <see langword="false" />.</param>
		/// <param name="isSmoothJoin">
		///   <see langword="true" /> para tratar a junção entre esse segmento e o segmento anterior como um canto quando traçados com um <see cref="T:System.Windows.Media.Pen" />; caso contrário, <see langword="false" />.</param>
		/// <exception cref="T:System.InvalidOperationException">Foi feita uma tentativa para adicionar um segmento sem iniciar uma figura ao chamar o método <see cref="M:System.Windows.Media.StreamGeometryContext.BeginFigure(System.Windows.Point,System.Boolean,System.Boolean)" />.</exception>
		// Token: 0x06002C70 RID: 11376
		public abstract void QuadraticBezierTo(Point point1, Point point2, bool isStroked, bool isSmoothJoin);

		/// <summary>Desenha uma curva de Bézier para o ponto especificado.</summary>
		/// <param name="point1">O primeiro ponto de controle usado para especificar a forma da curva.</param>
		/// <param name="point2">O segundo ponto de controle usado para especificar a forma da curva.</param>
		/// <param name="point3">O ponto de destino do final da curva.</param>
		/// <param name="isStroked">
		///   <see langword="true" /> para deixar o segmento tracejado quando uma <see cref="T:System.Windows.Media.Pen" /> for usada para renderizar o segmento, caso contrário, <see langword="false" />.</param>
		/// <param name="isSmoothJoin">
		///   <see langword="true" /> para tratar a junção entre esse segmento e o segmento anterior como um canto quando traçados com um <see cref="T:System.Windows.Media.Pen" />; caso contrário, <see langword="false" />.</param>
		/// <exception cref="T:System.InvalidOperationException">Foi feita uma tentativa para adicionar um segmento sem iniciar uma figura ao chamar o método <see cref="M:System.Windows.Media.StreamGeometryContext.BeginFigure(System.Windows.Point,System.Boolean,System.Boolean)" />.</exception>
		// Token: 0x06002C71 RID: 11377
		public abstract void BezierTo(Point point1, Point point2, Point point3, bool isStroked, bool isSmoothJoin);

		/// <summary>Desenha uma ou mais linhas retas conectadas.</summary>
		/// <param name="points">A coleção de pontos que especificam os pontos de destino para uma ou mais linhas retas conectadas.</param>
		/// <param name="isStroked">
		///   <see langword="true" /> para deixar o segmento tracejado quando uma <see cref="T:System.Windows.Media.Pen" /> for usada para renderizar o segmento, caso contrário, <see langword="false" />.</param>
		/// <param name="isSmoothJoin">
		///   <see langword="true" /> para tratar a junção entre esse segmento e o segmento anterior como um canto quando traçados com um <see cref="T:System.Windows.Media.Pen" />; caso contrário, <see langword="false" />.</param>
		/// <exception cref="T:System.InvalidOperationException">Foi feita uma tentativa para adicionar um segmento sem iniciar uma figura ao chamar o método <see cref="M:System.Windows.Media.StreamGeometryContext.BeginFigure(System.Windows.Point,System.Boolean,System.Boolean)" />.</exception>
		// Token: 0x06002C72 RID: 11378
		public abstract void PolyLineTo(IList<Point> points, bool isStroked, bool isSmoothJoin);

		/// <summary>Desenha uma ou mais curvas de Bézier quadráticas.</summary>
		/// <param name="points">A coleção de pontos que especificam os pontos de controle e os pontos de destino para uma ou mais curvas de Bézier quadráticas. O primeiro ponto na lista especifica o ponto de controle da curva, o próximo ponto especifica o ponto de destino, o próximo ponto especifica o ponto de controle da próxima curva e assim por diante. A lista deve conter um número impar de pontos.</param>
		/// <param name="isStroked">
		///   <see langword="true" /> para deixar o segmento tracejado quando uma <see cref="T:System.Windows.Media.Pen" /> for usada para renderizar o segmento, caso contrário, <see langword="false" />.</param>
		/// <param name="isSmoothJoin">
		///   <see langword="true" /> para tratar a junção entre esse segmento e o segmento anterior como um canto quando traçados com um <see cref="T:System.Windows.Media.Pen" />; caso contrário, <see langword="false" />.</param>
		/// <exception cref="T:System.InvalidOperationException">Foi feita uma tentativa para adicionar um segmento sem iniciar uma figura ao chamar o método <see cref="M:System.Windows.Media.StreamGeometryContext.BeginFigure(System.Windows.Point,System.Boolean,System.Boolean)" />.</exception>
		// Token: 0x06002C73 RID: 11379
		public abstract void PolyQuadraticBezierTo(IList<Point> points, bool isStroked, bool isSmoothJoin);

		/// <summary>Desenha uma ou mais curvas de Bézier conectadas.</summary>
		/// <param name="points">A lista de pontos que especificam os pontos de controle e os pontos de destino para uma ou mais curvas de Bézier. O número de pontos nesta lista deve ser um múltiplo de três.</param>
		/// <param name="isStroked">
		///   <see langword="true" /> para deixar o segmento tracejado quando uma <see cref="T:System.Windows.Media.Pen" /> for usada para renderizar o segmento, caso contrário, <see langword="false" />.</param>
		/// <param name="isSmoothJoin">
		///   <see langword="true" /> para tratar a junção entre esse segmento e o segmento anterior como um canto quando traçados com um <see cref="T:System.Windows.Media.Pen" />; caso contrário, <see langword="false" />.</param>
		/// <exception cref="T:System.InvalidOperationException">Foi feita uma tentativa para adicionar um segmento sem iniciar uma figura ao chamar o método <see cref="M:System.Windows.Media.StreamGeometryContext.BeginFigure(System.Windows.Point,System.Boolean,System.Boolean)" />.</exception>
		// Token: 0x06002C74 RID: 11380
		public abstract void PolyBezierTo(IList<Point> points, bool isStroked, bool isSmoothJoin);

		/// <summary>Desenha um arco até o ponto especificado.</summary>
		/// <param name="point">O ponto de destino para o final do arco.</param>
		/// <param name="size">O raio (metade da largura e metade da altura) de uma elipse cujo perímetro é usado para desenhar o ângulo. Se a elipse for muito arredondada em todos os lados, o arco será arredondado, se ela for quase plana, o arco também será. Por exemplo, uma largura e uma altura muito grandes representariam uma elipse muito grande, o que resultaria em uma pequena curvatura do ângulo.</param>
		/// <param name="rotationAngle">O ângulo de rotação da elipse que especifica a curva. A curvatura do arco pode ser girada com esse parâmetro.</param>
		/// <param name="isLargeArc">
		///   <see langword="true" /> para desenhar o arco com mais de 180 graus, caso contrário, <see langword="false" />.</param>
		/// <param name="sweepDirection">Um valor que indica se o arco é desenhado na direção <see cref="F:System.Windows.Media.SweepDirection.Clockwise" /> ou <see cref="F:System.Windows.Media.SweepDirection.Counterclockwise" />.</param>
		/// <param name="isStroked">
		///   <see langword="true" /> para deixar o segmento tracejado quando uma <see cref="T:System.Windows.Media.Pen" /> for usada para renderizar o segmento, caso contrário, <see langword="false" />.</param>
		/// <param name="isSmoothJoin">
		///   <see langword="true" /> para tratar a junção entre esse segmento e o segmento anterior como um canto quando traçados com um <see cref="T:System.Windows.Media.Pen" />; caso contrário, <see langword="false" />.</param>
		// Token: 0x06002C75 RID: 11381
		public abstract void ArcTo(Point point, Size size, double rotationAngle, bool isLargeArc, SweepDirection sweepDirection, bool isStroked, bool isSmoothJoin);

		// Token: 0x06002C76 RID: 11382 RVA: 0x000B1864 File Offset: 0x000B0C64
		internal virtual void DisposeCore()
		{
		}

		// Token: 0x06002C77 RID: 11383
		internal abstract void SetClosedState(bool closed);
	}
}
