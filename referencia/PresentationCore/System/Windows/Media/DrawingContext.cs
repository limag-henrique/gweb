using System;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Threading;
using MS.Utility;

namespace System.Windows.Media
{
	/// <summary>Descreve o conteúdo visual usando os comandos draw, push e pop.</summary>
	// Token: 0x02000381 RID: 897
	public abstract class DrawingContext : DispatcherObject, IDisposable
	{
		// Token: 0x060020C3 RID: 8387 RVA: 0x000856F0 File Offset: 0x00084AF0
		internal DrawingContext()
		{
		}

		/// <summary>Desenha texto formatado no local especificado.</summary>
		/// <param name="formattedText">O texto formatado a ser desenhado.</param>
		/// <param name="origin">O local em que o texto deve ser desenhado.</param>
		/// <exception cref="T:System.ObjectDisposedException">O objeto já foi fechado ou descartado.</exception>
		// Token: 0x060020C4 RID: 8388 RVA: 0x00085704 File Offset: 0x00084B04
		public void DrawText(FormattedText formattedText, Point origin)
		{
			EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordGeneral, EventTrace.Level.Verbose, EventTrace.Event.WClientStringBegin, "DrawingContext.DrawText Start");
			this.VerifyApiNonstructuralChange();
			if (formattedText == null)
			{
				return;
			}
			formattedText.Draw(this, origin);
			EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordGeneral, EventTrace.Level.Verbose, EventTrace.Event.WClientStringEnd, "DrawingContext.DrawText End");
		}

		/// <summary>Fecha o <see cref="T:System.Windows.Media.DrawingContext" /> e libera o conteúdo. Depois disso, o <see cref="T:System.Windows.Media.DrawingContext" /> não pode ser modificado.</summary>
		/// <exception cref="T:System.ObjectDisposedException">Este objeto já foi fechado ou descartado.</exception>
		// Token: 0x060020C5 RID: 8389
		public abstract void Close();

		/// <summary>Este membro dá suporte à infraestrutura Windows Presentation Foundation (WPF) e não se destina a ser usado diretamente do código.</summary>
		// Token: 0x060020C6 RID: 8390 RVA: 0x00085740 File Offset: 0x00084B40
		void IDisposable.Dispose()
		{
			base.VerifyAccess();
			this.DisposeCore();
			GC.SuppressFinalize(this);
		}

		/// <summary>Libera todos os recursos usados pelo <see cref="T:System.Windows.Media.DrawingContext" />.</summary>
		/// <exception cref="T:System.ObjectDisposedException">O objeto já foi fechado ou descartado.</exception>
		// Token: 0x060020C7 RID: 8391
		protected abstract void DisposeCore();

		/// <summary>Este membro dá suporte à infraestrutura WPF e não se destina a ser usado diretamente do código.</summary>
		// Token: 0x060020C8 RID: 8392 RVA: 0x00085760 File Offset: 0x00084B60
		protected virtual void VerifyApiNonstructuralChange()
		{
			base.VerifyAccess();
		}

		/// <summary>Desenha uma linha entre os pontos especificados usando o <see cref="T:System.Windows.Media.Pen" /> especificado.</summary>
		/// <param name="pen">A caneta com a qual a linha será traçada.</param>
		/// <param name="point0">O ponto inicial da linha.</param>
		/// <param name="point1">O ponto de extremidade da linha.</param>
		// Token: 0x060020C9 RID: 8393
		public abstract void DrawLine(Pen pen, Point point0, Point point1);

		/// <summary>Desenha uma linha entre os pontos especificados usando o <see cref="T:System.Windows.Media.Pen" /> especificado e aplica os relógios de animação especificados.</summary>
		/// <param name="pen">A caneta para traçar a linha.</param>
		/// <param name="point0">O ponto inicial da linha.</param>
		/// <param name="point0Animations">O relógio com o qual animar o ponto de partida da linha ou <see langword="null" /> para nenhuma animação. Esse relógio deve ser criado com base em um <see cref="T:System.Windows.Media.Animation.AnimationTimeline" /> que pode animar objetos <see cref="T:System.Windows.Point" />.</param>
		/// <param name="point1">O ponto de extremidade da linha.</param>
		/// <param name="point1Animations">O relógio com o qual animar o ponto de término da linha ou <see langword="null" /> para nenhuma animação. Esse relógio deve ser criado com base em um <see cref="T:System.Windows.Media.Animation.AnimationTimeline" /> que pode animar objetos <see cref="T:System.Windows.Point" />.</param>
		// Token: 0x060020CA RID: 8394
		public abstract void DrawLine(Pen pen, Point point0, AnimationClock point0Animations, Point point1, AnimationClock point1Animations);

		/// <summary>Desenha um retângulo com o <see cref="T:System.Windows.Media.Brush" /> e <see cref="T:System.Windows.Media.Pen" /> especificados. A caneta e o pincel podem ser <see langword="null" />.</summary>
		/// <param name="brush">O pincel com o qual preencher o retângulo.  Isso é opcional e pode ser <see langword="null" />. Se o pincel for <see langword="null" />, nenhum preenchimento será desenhado.</param>
		/// <param name="pen">A caneta com a qual traçar o retângulo.  Isso é opcional e pode ser <see langword="null" />. Se a caneta for <see langword="null" />, nenhum traço será desenhado.</param>
		/// <param name="rectangle">O retângulo a ser desenhado.</param>
		// Token: 0x060020CB RID: 8395
		public abstract void DrawRectangle(Brush brush, Pen pen, Rect rectangle);

		/// <summary>Desenha um retângulo com o <see cref="T:System.Windows.Media.Brush" /> e <see cref="T:System.Windows.Media.Pen" /> especificado e aplica os relógios de animação especificados.</summary>
		/// <param name="brush">O pincel com o qual preencher o retângulo.  Isso é opcional e pode ser <see langword="null" />. Se o pincel for <see langword="null" />, nenhum preenchimento será desenhado.</param>
		/// <param name="pen">A caneta com a qual traçar o retângulo.  Isso é opcional e pode ser <see langword="null" />. Se a caneta for <see langword="null" />, nenhum traço será desenhado.</param>
		/// <param name="rectangle">O retângulo a ser desenhado.</param>
		/// <param name="rectangleAnimations">O relógio com o qual o tamanho e as dimensões do retângulo são animados ou <see langword="null" /> para nenhuma animação. Esse relógio deve ser criado com base em um <see cref="T:System.Windows.Media.Animation.AnimationTimeline" /> que pode animar objetos <see cref="T:System.Windows.Rect" />.</param>
		// Token: 0x060020CC RID: 8396
		public abstract void DrawRectangle(Brush brush, Pen pen, Rect rectangle, AnimationClock rectangleAnimations);

		/// <summary>Desenha um retângulo arredondado com o <see cref="T:System.Windows.Media.Brush" /> e <see cref="T:System.Windows.Media.Pen" /> especificados.</summary>
		/// <param name="brush">O pincel usado para preencher o retângulo.</param>
		/// <param name="pen">A caneta usada para traçar o retângulo.</param>
		/// <param name="rectangle">O retângulo a ser desenhado.</param>
		/// <param name="radiusX">O raio da dimensão X dos cantos arredondados.  Esse valor será ser fixado no intervalo de 0 a <see cref="P:System.Windows.Rect.Width" />/2.</param>
		/// <param name="radiusY">O raio da dimensão Y dos cantos arredondados.  Esse valor será ser fixado como um valor entre 0 e <see cref="P:System.Windows.Rect.Height" />/2.</param>
		// Token: 0x060020CD RID: 8397
		public abstract void DrawRoundedRectangle(Brush brush, Pen pen, Rect rectangle, double radiusX, double radiusY);

		/// <summary>Desenha um retângulo arredondado com o <see cref="T:System.Windows.Media.Brush" /> e <see cref="T:System.Windows.Media.Pen" /> especificado e aplica os relógios de animação especificados.</summary>
		/// <param name="brush">O pincel usado para preencher o retângulo ou <see langword="null" /> para nenhum preenchimento.</param>
		/// <param name="pen">A caneta usada para traçar o retângulo ou <see langword="null" /> para nenhum traço.</param>
		/// <param name="rectangle">O retângulo a ser desenhado.</param>
		/// <param name="rectangleAnimations">O relógio com o qual o tamanho e as dimensões do retângulo são animados ou <see langword="null" /> para nenhuma animação. Esse relógio deve ser criado com base em um <see cref="T:System.Windows.Media.Animation.AnimationTimeline" /> que pode animar objetos <see cref="T:System.Windows.Rect" />.</param>
		/// <param name="radiusX">O raio da dimensão X dos cantos arredondados.  Esse valor será ser fixado no intervalo de 0 a <see cref="P:System.Windows.Rect.Width" />/2</param>
		/// <param name="radiusXAnimations">O relógio com o qual o valor <paramref name="radiusX" /> do retângulo é animado ou <see langword="null" /> para nenhuma animação. Esse relógio deve ser criado com base em um <see cref="T:System.Windows.Media.Animation.AnimationTimeline" /> que pode animar valores <see cref="T:System.Double" />.</param>
		/// <param name="radiusY">O raio da dimensão Y dos cantos arredondados.  Esse valor será ser fixado como um valor entre 0 e <see cref="P:System.Windows.Rect.Height" />/2.</param>
		/// <param name="radiusYAnimations">O relógio com o qual o valor <paramref name="radiusY" /> do retângulo é animado ou <see langword="null" /> para nenhuma animação. Esse relógio deve ser criado com base em um <see cref="T:System.Windows.Media.Animation.AnimationTimeline" /> que pode animar valores <see cref="T:System.Double" />.</param>
		// Token: 0x060020CE RID: 8398
		public abstract void DrawRoundedRectangle(Brush brush, Pen pen, Rect rectangle, AnimationClock rectangleAnimations, double radiusX, AnimationClock radiusXAnimations, double radiusY, AnimationClock radiusYAnimations);

		/// <summary>Desenha uma elipse com o <see cref="T:System.Windows.Media.Brush" /> e <see cref="T:System.Windows.Media.Pen" /> especificados.</summary>
		/// <param name="brush">O pincel com o qual preencher a elipse.  Isso é opcional e pode ser <see langword="null" />. Se o pincel for <see langword="null" />, nenhum preenchimento será desenhado.</param>
		/// <param name="pen">A caneta com a qual traçar a elipse.  Isso é opcional e pode ser <see langword="null" />. Se a caneta for <see langword="null" />, nenhum traço será desenhado.</param>
		/// <param name="center">A localização do centro da elipse.</param>
		/// <param name="radiusX">O raio horizontal da elipse.</param>
		/// <param name="radiusY">O raio vertical da elipse.</param>
		// Token: 0x060020CF RID: 8399
		public abstract void DrawEllipse(Brush brush, Pen pen, Point center, double radiusX, double radiusY);

		/// <summary>Desenha uma elipse com o <see cref="T:System.Windows.Media.Brush" /> e <see cref="T:System.Windows.Media.Pen" /> especificado e aplica os relógios de animação especificados.</summary>
		/// <param name="brush">O pincel com o qual preencher a elipse.  Isso é opcional e pode ser <see langword="null" />. Se o pincel for <see langword="null" />, nenhum preenchimento será desenhado.</param>
		/// <param name="pen">A caneta com a qual traçar a elipse.  Isso é opcional e pode ser <see langword="null" />. Se a caneta for <see langword="null" />, nenhum traço será desenhado.</param>
		/// <param name="center">A localização do centro da elipse.</param>
		/// <param name="centerAnimations">O relógio com o qual animar a posição do centro da elipse ou <see langword="null" /> para nenhuma animação. Esse relógio deve ser criado com base em um <see cref="T:System.Windows.Media.Animation.AnimationTimeline" /> que pode animar objetos <see cref="T:System.Windows.Point" />.</param>
		/// <param name="radiusX">O raio horizontal da elipse.</param>
		/// <param name="radiusXAnimations">O relógio com o qual animar o raio x ou <see langword="null" /> para nenhuma animação. Esse relógio deve ser criado com base em um <see cref="T:System.Windows.Media.Animation.AnimationTimeline" /> que pode animar objetos <see cref="T:System.Double" />.</param>
		/// <param name="radiusY">O raio vertical da elipse.</param>
		/// <param name="radiusYAnimations">O relógio com o qual animar o raio y ou <see langword="null" /> para nenhuma animação. Esse relógio deve ser criado com base em um <see cref="T:System.Windows.Media.Animation.AnimationTimeline" /> que pode animar objetos <see cref="T:System.Double" />.</param>
		// Token: 0x060020D0 RID: 8400
		public abstract void DrawEllipse(Brush brush, Pen pen, Point center, AnimationClock centerAnimations, double radiusX, AnimationClock radiusXAnimations, double radiusY, AnimationClock radiusYAnimations);

		/// <summary>Desenha a <see cref="T:System.Windows.Media.Geometry" /> especificada usando o <see cref="T:System.Windows.Media.Brush" /> e <see cref="T:System.Windows.Media.Pen" /> especificados.</summary>
		/// <param name="brush">O <see cref="T:System.Windows.Media.Brush" /> com o qual preencher a <see cref="T:System.Windows.Media.Geometry" />. Isso é opcional e pode ser <see langword="null" />. Se o pincel for <see langword="null" />, nenhum preenchimento será desenhado.</param>
		/// <param name="pen">A <see cref="T:System.Windows.Media.Pen" /> com a qual traçar a <see cref="T:System.Windows.Media.Geometry" />. Isso é opcional e pode ser <see langword="null" />. Se a caneta for <see langword="null" />, nenhum traço será desenhado.</param>
		/// <param name="geometry">O <see cref="T:System.Windows.Media.Geometry" /> a ser desenhado.</param>
		// Token: 0x060020D1 RID: 8401
		public abstract void DrawGeometry(Brush brush, Pen pen, Geometry geometry);

		/// <summary>Desenha uma imagem na região definida pelo <see cref="T:System.Windows.Rect" /> especificado.</summary>
		/// <param name="imageSource">A imagem a ser desenhada.</param>
		/// <param name="rectangle">A região na qual desenhar bitmapSource.</param>
		// Token: 0x060020D2 RID: 8402
		public abstract void DrawImage(ImageSource imageSource, Rect rectangle);

		/// <summary>Desenha uma imagem na região definida pelo <see cref="T:System.Windows.Rect" /> especificado e aplica o relógio de animação especificado.</summary>
		/// <param name="imageSource">A imagem a ser desenhada.</param>
		/// <param name="rectangle">A região na qual desenhar bitmapSource.</param>
		/// <param name="rectangleAnimations">O relógio com o qual o tamanho e as dimensões do retângulo são animados ou <see langword="null" /> para nenhuma animação. Esse relógio deve ser criado com base em um <see cref="T:System.Windows.Media.Animation.AnimationTimeline" /> que pode animar objetos <see cref="T:System.Windows.Rect" />.</param>
		// Token: 0x060020D3 RID: 8403
		public abstract void DrawImage(ImageSource imageSource, Rect rectangle, AnimationClock rectangleAnimations);

		/// <summary>Desenha o texto especificado.</summary>
		/// <param name="foregroundBrush">O pincel usado para pintar o texto.</param>
		/// <param name="glyphRun">O texto a ser desenhado.</param>
		// Token: 0x060020D4 RID: 8404
		public abstract void DrawGlyphRun(Brush foregroundBrush, GlyphRun glyphRun);

		/// <summary>Desenha o objeto <see cref="T:System.Windows.Media.Drawing" /> especificado.</summary>
		/// <param name="drawing">O desenho a ser acrescentado.</param>
		// Token: 0x060020D5 RID: 8405
		public abstract void DrawDrawing(Drawing drawing);

		/// <summary>Desenha um vídeo na região especificada.</summary>
		/// <param name="player">A mídia para desenhar.</param>
		/// <param name="rectangle">A região na qual o <paramref name="player" /> será desenhado.</param>
		// Token: 0x060020D6 RID: 8406
		public abstract void DrawVideo(MediaPlayer player, Rect rectangle);

		/// <summary>Desenha um vídeo na região especificada e aplica o relógio de animação especificado.</summary>
		/// <param name="player">A mídia para desenhar.</param>
		/// <param name="rectangle">A área em que desenhar a mídia.</param>
		/// <param name="rectangleAnimations">O relógio com o qual o tamanho e as dimensões do retângulo são animados ou <see langword="null" /> para nenhuma animação. Esse relógio deve ser criado com base em um <see cref="T:System.Windows.Media.Animation.AnimationTimeline" /> que pode animar objetos <see cref="T:System.Windows.Rect" />.</param>
		// Token: 0x060020D7 RID: 8407
		public abstract void DrawVideo(MediaPlayer player, Rect rectangle, AnimationClock rectangleAnimations);

		/// <summary>Envia a região de corte especificada no contexto do desenho.</summary>
		/// <param name="clipGeometry">A região de corte a ser aplicada aos comandos de desenho subsequentes.</param>
		// Token: 0x060020D8 RID: 8408
		public abstract void PushClip(Geometry clipGeometry);

		/// <summary>Envia por push a máscara de opacidade especificada no contexto de desenho.</summary>
		/// <param name="opacityMask">A máscara de opacidade a ser aplicada aos desenhos posteriores. Os valores alfa desse pincel determinam a opacidade do desenho ao qual ela é aplicada.</param>
		// Token: 0x060020D9 RID: 8409
		public abstract void PushOpacityMask(Brush opacityMask);

		/// <summary>Envia por push a configuração de opacidade especificada no contexto de desenho.</summary>
		/// <param name="opacity">O fator de opacidade a ser aplicado aos comandos de desenho subsequentes. Esse fator é cumulativo com operações de <see cref="M:System.Windows.Media.DrawingContext.PushOpacity(System.Double)" /> anteriores.</param>
		// Token: 0x060020DA RID: 8410
		public abstract void PushOpacity(double opacity);

		/// <summary>Envia por push a configuração de opacidade especificada no contexto do desenho e aplica o relógio de animação especificado.</summary>
		/// <param name="opacity">O fator de opacidade a ser aplicado aos comandos de desenho subsequentes. Esse fator é cumulativo com operações de <see cref="M:System.Windows.Media.DrawingContext.PushOpacity(System.Double)" /> anteriores.</param>
		/// <param name="opacityAnimations">O relógio com o qual o valor da opacidade é animado ou <see langword="null" /> para nenhuma animação. Esse relógio deve ser criado com base em um <see cref="T:System.Windows.Media.Animation.AnimationTimeline" /> que pode animar valores <see cref="T:System.Double" />.</param>
		// Token: 0x060020DB RID: 8411
		public abstract void PushOpacity(double opacity, AnimationClock opacityAnimations);

		/// <summary>Envia por push o <see cref="T:System.Windows.Media.Transform" /> especificado no contexto de desenho.</summary>
		/// <param name="transform">A transformação a ser aplicada aos comandos de desenho subsequentes.</param>
		// Token: 0x060020DC RID: 8412
		public abstract void PushTransform(Transform transform);

		/// <summary>Envia por push o <see cref="T:System.Windows.Media.GuidelineSet" /> especificado no contexto de desenho.</summary>
		/// <param name="guidelines">A definição de diretriz a ser aplicada aos comandos de desenho subsequentes.</param>
		// Token: 0x060020DD RID: 8413
		public abstract void PushGuidelineSet(GuidelineSet guidelines);

		// Token: 0x060020DE RID: 8414
		internal abstract void PushGuidelineY1(double coordinate);

		// Token: 0x060020DF RID: 8415
		internal abstract void PushGuidelineY2(double leadingCoordinate, double offsetToDrivenCoordinate);

		/// <summary>Envia por push o <see cref="T:System.Windows.Media.Effects.BitmapEffect" /> especificado no contexto de desenho.</summary>
		/// <param name="effect">O efeito a ser aplicado aos desenhos posteriores.</param>
		/// <param name="effectInput">A área à qual o efeito é aplicado ou <see langword="null" />, se o efeito deve ser aplicado a toda a área de desenhos posteriores.</param>
		// Token: 0x060020E0 RID: 8416
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		public abstract void PushEffect(BitmapEffect effect, BitmapEffectInput effectInput);

		/// <summary>Exibe a última operação de máscara de opacidade, opacidade, recortar, efeito ou transformação que foi enviada por push no contexto de desenho.</summary>
		// Token: 0x060020E1 RID: 8417
		public abstract void Pop();
	}
}
