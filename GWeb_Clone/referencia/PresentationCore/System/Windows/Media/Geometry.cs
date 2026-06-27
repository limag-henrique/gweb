using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Media.Converters;
using System.Windows.Threading;
using MS.Internal;
using MS.Win32.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>As classes que derivam dessa classe base abstrata definem formas geométricas. Os objetos <see cref="T:System.Windows.Media.Geometry" /> podem ser usados para recorte, teste de clique e renderização de dados gráficos 2D.</summary>
	// Token: 0x020003AC RID: 940
	[ValueSerializer(typeof(GeometryValueSerializer))]
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	[TypeConverter(typeof(GeometryConverter))]
	public abstract class Geometry : Animatable, IFormattable, DUCE.IResource
	{
		/// <summary>Cria um clone modificável do <see cref="T:System.Windows.Media.Geometry" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002347 RID: 9031 RVA: 0x0008E5D0 File Offset: 0x0008D9D0
		public new Geometry Clone()
		{
			return (Geometry)base.Clone();
		}

		/// <summary>Cria um clone modificável do objeto <see cref="T:System.Windows.Media.Geometry" />, fazendo cópias em profundidade dos valores atuais do objeto. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002348 RID: 9032 RVA: 0x0008E5E8 File Offset: 0x0008D9E8
		public new Geometry CloneCurrentValue()
		{
			return (Geometry)base.CloneCurrentValue();
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x0008E600 File Offset: 0x0008DA00
		private static void TransformPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Geometry geometry = (Geometry)d;
			geometry.TransformPropertyChangedHook(e);
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			Transform resource = (Transform)e.OldValue;
			Transform resource2 = (Transform)e.NewValue;
			Dispatcher dispatcher = geometry.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = geometry;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						geometry.ReleaseResource(resource, channel);
						geometry.AddRefResource(resource2, channel);
					}
				}
			}
			geometry.PropertyChanged(Geometry.TransformProperty);
		}

		/// <summary>Obtém ou define o objeto <see cref="T:System.Windows.Media.Transform" /> aplicado a um <see cref="T:System.Windows.Media.Geometry" />.</summary>
		/// <returns>A transformação aplicada ao <see cref="T:System.Windows.Media.Geometry" />. Observe que esse valor pode ser um único <see cref="T:System.Windows.Media.Transform" /> ou um <see cref="T:System.Windows.Media.TransformCollection" /> convertido em um <see cref="T:System.Windows.Media.Transform" />.</returns>
		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x0600234A RID: 9034 RVA: 0x0008E6CC File Offset: 0x0008DACC
		// (set) Token: 0x0600234B RID: 9035 RVA: 0x0008E6EC File Offset: 0x0008DAEC
		public Transform Transform
		{
			get
			{
				return (Transform)base.GetValue(Geometry.TransformProperty);
			}
			set
			{
				base.SetValueInternal(Geometry.TransformProperty, value);
			}
		}

		// Token: 0x0600234C RID: 9036
		internal abstract DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel);

		// Token: 0x0600234D RID: 9037 RVA: 0x0008E708 File Offset: 0x0008DB08
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			DUCE.ResourceHandle result;
			using (CompositionEngineLock.Acquire())
			{
				result = this.AddRefOnChannelCore(channel);
			}
			return result;
		}

		// Token: 0x0600234E RID: 9038
		internal abstract void ReleaseOnChannelCore(DUCE.Channel channel);

		// Token: 0x0600234F RID: 9039 RVA: 0x0008E750 File Offset: 0x0008DB50
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			using (CompositionEngineLock.Acquire())
			{
				this.ReleaseOnChannelCore(channel);
			}
		}

		// Token: 0x06002350 RID: 9040
		internal abstract DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel);

		// Token: 0x06002351 RID: 9041 RVA: 0x0008E798 File Offset: 0x0008DB98
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handleCore;
			using (CompositionEngineLock.Acquire())
			{
				handleCore = this.GetHandleCore(channel);
			}
			return handleCore;
		}

		// Token: 0x06002352 RID: 9042
		internal abstract int GetChannelCountCore();

		// Token: 0x06002353 RID: 9043 RVA: 0x0008E7E0 File Offset: 0x0008DBE0
		int DUCE.IResource.GetChannelCount()
		{
			return this.GetChannelCountCore();
		}

		// Token: 0x06002354 RID: 9044
		internal abstract DUCE.Channel GetChannelCore(int index);

		// Token: 0x06002355 RID: 9045 RVA: 0x0008E7F4 File Offset: 0x0008DBF4
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this.GetChannelCore(index);
		}

		/// <summary>Cria uma representação de cadeia de caracteres do objeto com base na cultura atual.</summary>
		/// <returns>Uma representação de cadeia de caracteres do objeto.</returns>
		// Token: 0x06002356 RID: 9046 RVA: 0x0008E808 File Offset: 0x0008DC08
		public override string ToString()
		{
			base.ReadPreamble();
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação da cadeia de caracteres do objeto usando as informações de formatação específicas da cultura especificadas.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura ou <see langword="null" /> para usar a cultura atual.</param>
		/// <returns>Uma representação de cadeia de caracteres do objeto.</returns>
		// Token: 0x06002357 RID: 9047 RVA: 0x0008E824 File Offset: 0x0008DC24
		public string ToString(IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(null, provider);
		}

		/// <summary>Formata o valor da instância atual usando o formato especificado.</summary>
		/// <param name="format">O formato a ser usado.  
		///
		/// ou - 
		/// Uma referência nula (<see langword="Nothing" /> no Visual Basic) para usar o formato padrão definido para o tipo da implementação <see cref="T:System.IFormattable" />.</param>
		/// <param name="provider">O provedor a ser usado para formatar o valor.  
		///
		/// ou - 
		/// Uma referência nula (<see langword="Nothing" /> no Visual Basic) para obter as informações de formato numérico da configuração de localidade atual do sistema operacional.</param>
		/// <returns>O valor da instância atual no formato especificado.</returns>
		// Token: 0x06002358 RID: 9048 RVA: 0x0008E840 File Offset: 0x0008DC40
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(format, provider);
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x0008E85C File Offset: 0x0008DC5C
		internal virtual string ConvertToString(string format, IFormatProvider provider)
		{
			return base.ToString();
		}

		/// <summary>Cria um nova instância <see cref="T:System.Windows.Media.Geometry" /> na cadeia de caracteres especificada usando a cultura atual.</summary>
		/// <param name="source">Uma cadeia de caracteres que descreve a geometria a ser criada.</param>
		/// <returns>Um nova instância <see cref="T:System.Windows.Media.Geometry" /> criada na cadeia de caracteres especificada.</returns>
		// Token: 0x0600235A RID: 9050 RVA: 0x0008E870 File Offset: 0x0008DC70
		public static Geometry Parse(string source)
		{
			IFormatProvider invariantEnglishUS = TypeConverterHelper.InvariantEnglishUS;
			return Parsers.ParseGeometry(source, invariantEnglishUS);
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x0008E88C File Offset: 0x0008DC8C
		static Geometry()
		{
			Type typeFromHandle = typeof(Geometry);
			Geometry.TransformProperty = Animatable.RegisterProperty("Transform", typeof(Transform), typeFromHandle, Transform.Identity, new PropertyChangedCallback(Geometry.TransformPropertyChanged), null, false, null);
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x0008E8F0 File Offset: 0x0008DCF0
		internal Geometry()
		{
		}

		/// <summary>Obtém um objeto vazio.</summary>
		/// <returns>O objeto geometry vazia.</returns>
		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x0600235D RID: 9053 RVA: 0x0008E904 File Offset: 0x0008DD04
		public static Geometry Empty
		{
			get
			{
				return Geometry.s_empty;
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Rect" /> que especifica a caixa delimitadora alinhada por eixo do <see cref="T:System.Windows.Media.Geometry" />.</summary>
		/// <returns>A caixa delimitadora alinhada por eixo do <see cref="T:System.Windows.Media.Geometry" />.</returns>
		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600235E RID: 9054 RVA: 0x0008E918 File Offset: 0x0008DD18
		public virtual Rect Bounds
		{
			get
			{
				return PathGeometry.GetPathBounds(this.GetPathGeometryData(), null, Matrix.Identity, Geometry.StandardFlatteningTolerance, ToleranceType.Absolute, false);
			}
		}

		/// <summary>Obtém a tolerância padrão usada para a aproximação poligonal.</summary>
		/// <returns>A tolerância padrão, 0,25.</returns>
		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x0600235F RID: 9055 RVA: 0x0008E940 File Offset: 0x0008DD40
		public static double StandardFlatteningTolerance
		{
			get
			{
				return 0.25;
			}
		}

		/// <summary>Retorna um retângulo alinhado por eixo exatamente grande o suficiente para conter a geometria depois que ela foi contornada com o <see cref="T:System.Windows.Media.Pen" /> especificado, considerando o fator de tolerância especificado.</summary>
		/// <param name="pen">Um objeto que determina a área do traço da geometria.</param>
		/// <param name="tolerance">Os limites máximos na distância entre pontos na aproximação poligonal da geometria. Valores menores produzem resultados mais precisos, mas causam a execução lenta. Se <paramref name="tolerance" /> for menor que 0,000001, 0,000001 será usado.</param>
		/// <param name="type">Um dos valores <see cref="T:System.Windows.Media.ToleranceType" /> que especifica se o fator de tolerância é um valor absoluto ou relativo à área da geometria.</param>
		/// <returns>Um retângulo alinhado por eixo que é exatamente grande o suficiente para conter a geometria contornada.</returns>
		// Token: 0x06002360 RID: 9056 RVA: 0x0008E958 File Offset: 0x0008DD58
		public virtual Rect GetRenderBounds(Pen pen, double tolerance, ToleranceType type)
		{
			base.ReadPreamble();
			Matrix identity = Matrix.Identity;
			return this.GetBoundsInternal(pen, identity, tolerance, type);
		}

		/// <summary>Retorna um retângulo alinhado por eixo exatamente grande o suficiente para conter a geometria depois que ela foi contornada com o <see cref="T:System.Windows.Media.Pen" /> especificado.</summary>
		/// <param name="pen">Um objeto que determina a área do traço da geometria.</param>
		/// <returns>Um retângulo alinhado por eixo que é exatamente grande o suficiente para conter a geometria contornada.</returns>
		// Token: 0x06002361 RID: 9057 RVA: 0x0008E97C File Offset: 0x0008DD7C
		public Rect GetRenderBounds(Pen pen)
		{
			base.ReadPreamble();
			Matrix identity = Matrix.Identity;
			return this.GetBoundsInternal(pen, identity, Geometry.StandardFlatteningTolerance, ToleranceType.Absolute);
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x0008E9A4 File Offset: 0x0008DDA4
		internal virtual bool AreClose(Geometry geometry)
		{
			return false;
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x0008E9B4 File Offset: 0x0008DDB4
		internal virtual Rect GetBoundsInternal(Pen pen, Matrix matrix, double tolerance, ToleranceType type)
		{
			if (this.IsObviouslyEmpty())
			{
				return Rect.Empty;
			}
			Geometry.PathGeometryData pathGeometryData = this.GetPathGeometryData();
			return PathGeometry.GetPathBounds(pathGeometryData, pen, matrix, tolerance, type, true);
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x0008E9E4 File Offset: 0x0008DDE4
		internal Rect GetBoundsInternal(Pen pen, Matrix matrix)
		{
			return this.GetBoundsInternal(pen, matrix, Geometry.StandardFlatteningTolerance, ToleranceType.Absolute);
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x0008EA00 File Offset: 0x0008DE00
		[SecurityCritical]
		internal unsafe static Rect GetBoundsHelper(Pen pen, Matrix* pWorldMatrix, Point* pPoints, byte* pTypes, uint pointCount, uint segmentCount, Matrix* pGeometryMatrix, double tolerance, ToleranceType type, bool fSkipHollows)
		{
			double[] array = null;
			bool flag = Pen.ContributesToBounds(pen);
			MIL_PEN_DATA mil_PEN_DATA;
			if (flag)
			{
				pen.GetBasicPenData(&mil_PEN_DATA, out array);
			}
			MilMatrix3x2D milMatrix3x2D;
			if (pGeometryMatrix != null)
			{
				milMatrix3x2D = CompositionResourceManager.MatrixToMilMatrix3x2D(ref *pGeometryMatrix);
			}
			MilMatrix3x2D milMatrix3x2D2 = CompositionResourceManager.MatrixToMilMatrix3x2D(ref *pWorldMatrix);
			double[] array2;
			double* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			Rect empty;
			int num = MilCoreApi.MilUtility_PolygonBounds(&milMatrix3x2D2, flag ? (&mil_PEN_DATA) : null, (array == null) ? null : ptr, pPoints, pTypes, pointCount, segmentCount, (pGeometryMatrix == null) ? null : (&milMatrix3x2D), tolerance, type == ToleranceType.Relative, fSkipHollows, &empty);
			if (num == -2003304438)
			{
				empty = Rect.Empty;
			}
			else
			{
				HRESULT.Check(num);
			}
			array2 = null;
			return empty;
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x0008EAAC File Offset: 0x0008DEAC
		internal virtual void TransformPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x0008EABC File Offset: 0x0008DEBC
		internal Geometry GetTransformedCopy(Transform transform)
		{
			Geometry geometry = this.Clone();
			Transform transform2 = this.Transform;
			if (transform != null && !transform.IsIdentity)
			{
				if (transform2 == null || transform2.IsIdentity)
				{
					geometry.Transform = transform;
				}
				else
				{
					geometry.Transform = new MatrixTransform(transform2.Value * transform.Value);
				}
			}
			return geometry;
		}

		/// <summary>Obtém um valor que indica se o valor da propriedade <see cref="P:System.Windows.Media.Geometry.Transform" /> deve ser serializado.</summary>
		/// <returns>
		///   <see langword="true" /> se o valor da propriedade <see cref="P:System.Windows.Media.Geometry.Transform" /> da geometria deve ser serializado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002368 RID: 9064 RVA: 0x0008EB14 File Offset: 0x0008DF14
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeTransform()
		{
			Transform transform = this.Transform;
			return transform != null && !transform.IsIdentity;
		}

		/// <summary>Obtém a área, dentro da tolerância especificada, da região de preenchimento do objeto <see cref="T:System.Windows.Media.Geometry" />.</summary>
		/// <param name="tolerance">Os limites máximos na distância entre pontos na aproximação poligonal da geometria. Valores menores produzem resultados mais precisos, mas causam a execução lenta. Se <paramref name="tolerance" /> for menor que 0,000001, 0,000001 será usado.</param>
		/// <param name="type">Um dos valores <see cref="T:System.Windows.Media.ToleranceType" /> que especifica se o fator de tolerância é um valor absoluto ou relativo à área da geometria.</param>
		/// <returns>A área da região preenchida da geometria.</returns>
		// Token: 0x06002369 RID: 9065 RVA: 0x0008EB38 File Offset: 0x0008DF38
		[SecurityCritical]
		public unsafe virtual double GetArea(double tolerance, ToleranceType type)
		{
			base.ReadPreamble();
			if (this.IsObviouslyEmpty())
			{
				return 0.0;
			}
			Geometry.PathGeometryData pathGeometryData = this.GetPathGeometryData();
			if (pathGeometryData.IsEmpty())
			{
				return 0.0;
			}
			byte[] array;
			byte* pPathData;
			if ((array = pathGeometryData.SerializedData) == null || array.Length == 0)
			{
				pPathData = null;
			}
			else
			{
				pPathData = &array[0];
			}
			double result;
			int num = MilCoreApi.MilUtility_GeometryGetArea(pathGeometryData.FillRule, pPathData, pathGeometryData.Size, &pathGeometryData.Matrix, tolerance, type == ToleranceType.Relative, &result);
			if (num == -2003304438)
			{
				result = 0.0;
			}
			else
			{
				HRESULT.Check(num);
			}
			array = null;
			return result;
		}

		/// <summary>Obtém a área da região preenchida do objeto <see cref="T:System.Windows.Media.Geometry" />.</summary>
		/// <returns>A área da região preenchida da geometria.</returns>
		// Token: 0x0600236A RID: 9066 RVA: 0x0008EBD8 File Offset: 0x0008DFD8
		public double GetArea()
		{
			return this.GetArea(Geometry.StandardFlatteningTolerance, ToleranceType.Absolute);
		}

		/// <summary>Determina se o objeto está vazio.</summary>
		/// <returns>
		///   <see langword="true" /> se a geometria estiver vazia; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600236B RID: 9067
		public abstract bool IsEmpty();

		/// <summary>Determina se o objeto pode ter segmentos curvos.</summary>
		/// <returns>
		///   <see langword="true" /> se o objeto de geometria pode ter segmentos curvos; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600236C RID: 9068
		public abstract bool MayHaveCurves();

		/// <summary>Indica se a geometria contém o <see cref="T:System.Windows.Point" /> especificado, considerando a margem de erro especificada.</summary>
		/// <param name="hitPoint">O ponto a ser testado quanto ao confinamento.</param>
		/// <param name="tolerance">Os limites máximos na distância entre pontos na aproximação poligonal da geometria. Valores menores produzem resultados mais precisos, mas causam a execução lenta. Se <paramref name="tolerance" /> for menor que 0,000001, 0,000001 será usado.</param>
		/// <param name="type">Um dos valores <see cref="T:System.Windows.Media.ToleranceType" /> que especifica se o fator de tolerância é um valor absoluto ou relativo à área da geometria.</param>
		/// <returns>
		///   <see langword="true" /> se a geometria contém <paramref name="hitPoint" />, considerando a margem de erro especificada; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600236D RID: 9069 RVA: 0x0008EBF4 File Offset: 0x0008DFF4
		public bool FillContains(Point hitPoint, double tolerance, ToleranceType type)
		{
			return this.ContainsInternal(null, hitPoint, tolerance, type);
		}

		/// <summary>Indica se a geometria contém o <see cref="T:System.Windows.Point" /> especificado.</summary>
		/// <param name="hitPoint">O ponto a ser testado quanto ao confinamento.</param>
		/// <returns>
		///   <see langword="true" /> se a geometria contiver o <paramref name="hitPoint" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600236E RID: 9070 RVA: 0x0008EC0C File Offset: 0x0008E00C
		public bool FillContains(Point hitPoint)
		{
			return this.ContainsInternal(null, hitPoint, Geometry.StandardFlatteningTolerance, ToleranceType.Absolute);
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Point" /> especificado está contido no traço produzido aplicando o <see cref="T:System.Windows.Media.Pen" /> especificado à geometria, considerando a margem de erro especificada.</summary>
		/// <param name="pen">Um objeto que define o traço de uma geometria.</param>
		/// <param name="hitPoint">O ponto a ser testado quanto ao confinamento.</param>
		/// <param name="tolerance">Os limites máximos na distância entre pontos na aproximação poligonal da geometria. Valores menores produzem resultados mais precisos, mas causam a execução lenta. Se <paramref name="tolerance" /> for menor que 0,000001, 0,000001 será usado.</param>
		/// <param name="type">Um dos valores <see cref="T:System.Windows.Media.ToleranceType" /> que especifica se o fator de tolerância é um valor absoluto ou relativo à área da geometria.</param>
		/// <returns>
		///   <see langword="true" /> se o traço criado aplicando a <see cref="T:System.Windows.Media.Pen" /> especificada à geometria contém o ponto especificado, considerando o fator de tolerância especificado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600236F RID: 9071 RVA: 0x0008EC28 File Offset: 0x0008E028
		public bool StrokeContains(Pen pen, Point hitPoint, double tolerance, ToleranceType type)
		{
			return pen != null && this.ContainsInternal(pen, hitPoint, tolerance, type);
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x0008EC48 File Offset: 0x0008E048
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe virtual bool ContainsInternal(Pen pen, Point hitPoint, double tolerance, ToleranceType type)
		{
			if (this.IsObviouslyEmpty())
			{
				return false;
			}
			Geometry.PathGeometryData pathGeometryData = this.GetPathGeometryData();
			if (pathGeometryData.IsEmpty())
			{
				return false;
			}
			bool result = false;
			double[] array = null;
			MIL_PEN_DATA mil_PEN_DATA;
			if (pen != null)
			{
				pen.GetBasicPenData(&mil_PEN_DATA, out array);
			}
			byte[] array2;
			byte* pPathData;
			if ((array2 = pathGeometryData.SerializedData) == null || array2.Length == 0)
			{
				pPathData = null;
			}
			else
			{
				pPathData = &array2[0];
			}
			double[] array3;
			double* pDashArray;
			if ((array3 = array) == null || array3.Length == 0)
			{
				pDashArray = null;
			}
			else
			{
				pDashArray = &array3[0];
			}
			int num = MilCoreApi.MilUtility_PathGeometryHitTest(&pathGeometryData.Matrix, (pen == null) ? null : (&mil_PEN_DATA), pDashArray, pathGeometryData.FillRule, pPathData, pathGeometryData.Size, tolerance, type == ToleranceType.Relative, &hitPoint, out result);
			if (num == -2003304438)
			{
				result = false;
			}
			else
			{
				HRESULT.Check(num);
			}
			array3 = null;
			array2 = null;
			return result;
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x0008ED0C File Offset: 0x0008E10C
		[SecurityCritical]
		internal unsafe bool ContainsInternal(Pen pen, Point hitPoint, double tolerance, ToleranceType type, Point* pPoints, uint pointCount, byte* pTypes, uint typeCount)
		{
			bool result = false;
			MilMatrix3x2D milMatrix3x2D = CompositionResourceManager.TransformToMilMatrix3x2D(this.Transform);
			double[] array = null;
			MIL_PEN_DATA mil_PEN_DATA;
			if (pen != null)
			{
				pen.GetBasicPenData(&mil_PEN_DATA, out array);
			}
			double[] array2;
			double* pDashArray;
			if ((array2 = array) == null || array2.Length == 0)
			{
				pDashArray = null;
			}
			else
			{
				pDashArray = &array2[0];
			}
			int num = MilCoreApi.MilUtility_PolygonHitTest(&milMatrix3x2D, (pen == null) ? null : (&mil_PEN_DATA), pDashArray, pPoints, pTypes, pointCount, typeCount, tolerance, type == ToleranceType.Relative, &hitPoint, out result);
			if (num == -2003304438)
			{
				result = false;
			}
			else
			{
				HRESULT.Check(num);
			}
			array2 = null;
			return result;
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Point" /> especificado está contido no traço produzido aplicando o <see cref="T:System.Windows.Media.Pen" /> especificado à geometria.</summary>
		/// <param name="pen">Um objeto que determina a área do traço da geometria.</param>
		/// <param name="hitPoint">O ponto a ser testado quanto ao confinamento.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="hitPoint" /> estiver contido no traço produzido aplicando o <see cref="T:System.Windows.Media.Pen" /> especificado à geometria; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002372 RID: 9074 RVA: 0x0008ED90 File Offset: 0x0008E190
		public bool StrokeContains(Pen pen, Point hitPoint)
		{
			return this.StrokeContains(pen, hitPoint, Geometry.StandardFlatteningTolerance, ToleranceType.Absolute);
		}

		/// <summary>Indica se a geometria atual contém o <see cref="T:System.Windows.Media.Geometry" /> especificado, considerando a margem de erro especificada.</summary>
		/// <param name="geometry">A geometria a ser testada quanto ao confinamento.</param>
		/// <param name="tolerance">Os limites máximos na distância entre pontos na aproximação poligonal das geometrias. Valores menores produzem resultados mais precisos, mas causam a execução lenta. Se <paramref name="tolerance" /> for menor que 0,000001, 0,000001 será usado.</param>
		/// <param name="type">Um dos valores <see cref="T:System.Windows.Media.ToleranceType" /> que especifica se o fator de tolerância é um valor absoluto ou relativo à área da geometria.</param>
		/// <returns>
		///   <see langword="true" /> se a geometria atual contém <paramref name="geometry" />, considerando a margem de erro especificada; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002373 RID: 9075 RVA: 0x0008EDAC File Offset: 0x0008E1AC
		public bool FillContains(Geometry geometry, double tolerance, ToleranceType type)
		{
			IntersectionDetail intersectionDetail = this.FillContainsWithDetail(geometry, tolerance, type);
			return intersectionDetail == IntersectionDetail.FullyContains;
		}

		/// <summary>Indica se a geometria atual contém completamente o <see cref="T:System.Windows.Media.Geometry" /> especificado.</summary>
		/// <param name="geometry">A geometria a ser testada quanto ao confinamento.</param>
		/// <returns>
		///   <see langword="true" /> se a geometria atual contém completamente <paramref name="geometry" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002374 RID: 9076 RVA: 0x0008EDC8 File Offset: 0x0008E1C8
		public bool FillContains(Geometry geometry)
		{
			return this.FillContains(geometry, Geometry.StandardFlatteningTolerance, ToleranceType.Absolute);
		}

		/// <summary>Retorna um valor que descreve a interseção entre a geometria atual e a geometria especificada, considerando a margem de erro especificada.</summary>
		/// <param name="geometry">A geometria a ser testada quanto ao confinamento.</param>
		/// <param name="tolerance">Os limites máximos na distância entre pontos na aproximação poligonal das geometrias. Valores menores produzem resultados mais precisos, mas causam a execução lenta. Se <paramref name="tolerance" /> for menor que 0,000001, 0,000001 será usado.</param>
		/// <param name="type">Um dos valores <see cref="T:System.Windows.Media.ToleranceType" /> que especifica se o fator de tolerância é um valor absoluto ou relativo à área da geometria.</param>
		/// <returns>Um dos valores de enumeração.</returns>
		// Token: 0x06002375 RID: 9077 RVA: 0x0008EDE4 File Offset: 0x0008E1E4
		public virtual IntersectionDetail FillContainsWithDetail(Geometry geometry, double tolerance, ToleranceType type)
		{
			base.ReadPreamble();
			if (this.IsObviouslyEmpty() || geometry == null || geometry.IsObviouslyEmpty())
			{
				return IntersectionDetail.Empty;
			}
			return PathGeometry.HitTestWithPathGeometry(this, geometry, tolerance, type);
		}

		/// <summary>Retorna um valor que descreve a interseção entre a geometria atual e a geometria especificada.</summary>
		/// <param name="geometry">A geometria a ser testada quanto ao confinamento.</param>
		/// <returns>Um dos valores de enumeração.</returns>
		// Token: 0x06002376 RID: 9078 RVA: 0x0008EE18 File Offset: 0x0008E218
		public IntersectionDetail FillContainsWithDetail(Geometry geometry)
		{
			return this.FillContainsWithDetail(geometry, Geometry.StandardFlatteningTolerance, ToleranceType.Absolute);
		}

		/// <summary>Obtém um valor que descreve a interseção entre a <see cref="T:System.Windows.Media.Geometry" /> especificada e o traço criado aplicando-se a <see cref="T:System.Windows.Media.Pen" /> especificada à geometria atual, considerando a margem de erro especificada.</summary>
		/// <param name="pen">Um objeto que determina a área do traço da geometria atual.</param>
		/// <param name="geometry">A geometria a ser testada quanto ao confinamento.</param>
		/// <param name="tolerance">Os limites máximos na distância entre pontos na aproximação poligonal das geometrias. Valores menores produzem resultados mais precisos, mas causam a execução lenta. Se <paramref name="tolerance" /> for menor que 0,000001, 0,000001 será usado.</param>
		/// <param name="type">Um dos valores <see cref="T:System.Windows.Media.ToleranceType" /> que especifica se o fator de tolerância é um valor absoluto ou relativo à área da geometria.</param>
		/// <returns>Um dos valores de enumeração.</returns>
		// Token: 0x06002377 RID: 9079 RVA: 0x0008EE34 File Offset: 0x0008E234
		public IntersectionDetail StrokeContainsWithDetail(Pen pen, Geometry geometry, double tolerance, ToleranceType type)
		{
			if (this.IsObviouslyEmpty() || geometry == null || geometry.IsObviouslyEmpty() || pen == null)
			{
				return IntersectionDetail.Empty;
			}
			PathGeometry widenedPathGeometry = this.GetWidenedPathGeometry(pen);
			return PathGeometry.HitTestWithPathGeometry(widenedPathGeometry, geometry, tolerance, type);
		}

		/// <summary>Retorna um valor que descreve a interseção entre a <see cref="T:System.Windows.Media.Geometry" /> especificada e o traço criado aplicando-se a <see cref="T:System.Windows.Media.Pen" /> especificada à geometria atual.</summary>
		/// <param name="pen">Um objeto que determina a área do traço da geometria atual.</param>
		/// <param name="geometry">A geometria a ser testada quanto ao confinamento.</param>
		/// <returns>Um dos valores de enumeração.</returns>
		// Token: 0x06002378 RID: 9080 RVA: 0x0008EE6C File Offset: 0x0008E26C
		public IntersectionDetail StrokeContainsWithDetail(Pen pen, Geometry geometry)
		{
			return this.StrokeContainsWithDetail(pen, geometry, Geometry.StandardFlatteningTolerance, ToleranceType.Absolute);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.PathGeometry" />, dentro da tolerância especificada, que é uma aproximação poligonal do objeto <see cref="T:System.Windows.Media.Geometry" />.</summary>
		/// <param name="tolerance">Os limites máximos na distância entre pontos na aproximação poligonal da geometria. Valores menores produzem resultados mais precisos, mas causam a execução lenta. Se <paramref name="tolerance" /> for menor que 0,000001, 0,000001 será usado.</param>
		/// <param name="type">Um dos valores <see cref="T:System.Windows.Media.ToleranceType" /> que especifica se o fator de tolerância é um valor absoluto ou relativo à área da geometria.</param>
		/// <returns>A aproximação poligonal do <see cref="T:System.Windows.Media.Geometry" />.</returns>
		// Token: 0x06002379 RID: 9081 RVA: 0x0008EE88 File Offset: 0x0008E288
		[SecurityCritical]
		public unsafe virtual PathGeometry GetFlattenedPathGeometry(double tolerance, ToleranceType type)
		{
			base.ReadPreamble();
			if (this.IsObviouslyEmpty())
			{
				return new PathGeometry();
			}
			Geometry.PathGeometryData pathGeometryData = this.GetPathGeometryData();
			if (pathGeometryData.IsEmpty())
			{
				return new PathGeometry();
			}
			byte[] array;
			byte* pPathData;
			if ((array = pathGeometryData.SerializedData) == null || array.Length == 0)
			{
				pPathData = null;
			}
			else
			{
				pPathData = &array[0];
			}
			FillRule fillRule = FillRule.Nonzero;
			PathGeometry.FigureList figureList = new PathGeometry.FigureList();
			int num = UnsafeNativeMethods.MilCoreApi.MilUtility_PathGeometryFlatten(&pathGeometryData.Matrix, pathGeometryData.FillRule, pPathData, pathGeometryData.Size, tolerance, type == ToleranceType.Relative, new PathGeometry.AddFigureToListDelegate(figureList.AddFigureToList), out fillRule);
			PathGeometry result;
			if (num == -2003304438)
			{
				result = new PathGeometry();
			}
			else
			{
				HRESULT.Check(num);
				result = new PathGeometry(figureList.Figures, fillRule, null);
			}
			array = null;
			return result;
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.PathGeometry" /> que é uma aproximação poligonal do objeto <see cref="T:System.Windows.Media.Geometry" />.</summary>
		/// <returns>A aproximação poligonal do <see cref="T:System.Windows.Media.Geometry" />.</returns>
		// Token: 0x0600237A RID: 9082 RVA: 0x0008EF44 File Offset: 0x0008E344
		public PathGeometry GetFlattenedPathGeometry()
		{
			return this.GetFlattenedPathGeometry(Geometry.StandardFlatteningTolerance, ToleranceType.Absolute);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.PathGeometry" /> que é a forma definida pelo traço na <see cref="T:System.Windows.Media.Geometry" /> produzido pela <see cref="T:System.Windows.Media.Pen" /> especificada, considerando o fator de tolerância especificado.</summary>
		/// <param name="pen">O objeto usado para definir a área do traço da geometria.</param>
		/// <param name="tolerance">Os limites máximos na distância entre pontos na aproximação poligonal da geometria. Valores menores produzem resultados mais precisos, mas causam a execução lenta. Se <paramref name="tolerance" /> for menor que 0,000001, 0,000001 será usado.</param>
		/// <param name="type">Um dos valores <see cref="T:System.Windows.Media.ToleranceType" /> que especifica se o fator de tolerância é um valor absoluto ou relativo à área da geometria.</param>
		/// <returns>A geometria, ampliada por <paramref name="pen" />.</returns>
		// Token: 0x0600237B RID: 9083 RVA: 0x0008EF60 File Offset: 0x0008E360
		[SecurityCritical]
		public unsafe virtual PathGeometry GetWidenedPathGeometry(Pen pen, double tolerance, ToleranceType type)
		{
			base.ReadPreamble();
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (this.IsObviouslyEmpty())
			{
				return new PathGeometry();
			}
			Geometry.PathGeometryData pathGeometryData = this.GetPathGeometryData();
			if (pathGeometryData.IsEmpty())
			{
				return new PathGeometry();
			}
			PathGeometry result = null;
			double[] array = null;
			MIL_PEN_DATA mil_PEN_DATA;
			pen.GetBasicPenData(&mil_PEN_DATA, out array);
			byte[] array2;
			byte* pPathData;
			if ((array2 = pathGeometryData.SerializedData) == null || array2.Length == 0)
			{
				pPathData = null;
			}
			else
			{
				pPathData = &array2[0];
			}
			FillRule fillRule = FillRule.Nonzero;
			PathGeometry.FigureList figureList = new PathGeometry.FigureList();
			GCHandle gchandle = default(GCHandle);
			if (array != null)
			{
				gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			}
			try
			{
				int num = UnsafeNativeMethods.MilCoreApi.MilUtility_PathGeometryWiden(&mil_PEN_DATA, (double*)((array == null) ? null : ((void*)gchandle.AddrOfPinnedObject())), &pathGeometryData.Matrix, pathGeometryData.FillRule, pPathData, pathGeometryData.Size, tolerance, type == ToleranceType.Relative, new PathGeometry.AddFigureToListDelegate(figureList.AddFigureToList), out fillRule);
				if (num == -2003304438)
				{
					result = new PathGeometry();
				}
				else
				{
					HRESULT.Check(num);
					result = new PathGeometry(figureList.Figures, fillRule, null);
				}
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			array2 = null;
			return result;
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.PathGeometry" /> que é a forma definida pelo traço na <see cref="T:System.Windows.Media.Geometry" /> produzido pela <see cref="T:System.Windows.Media.Pen" /> especificada.</summary>
		/// <param name="pen">Um objeto que determina a área do traço da geometria.</param>
		/// <returns>A geometria contornada.</returns>
		// Token: 0x0600237C RID: 9084 RVA: 0x0008F094 File Offset: 0x0008E494
		public PathGeometry GetWidenedPathGeometry(Pen pen)
		{
			return this.GetWidenedPathGeometry(pen, Geometry.StandardFlatteningTolerance, ToleranceType.Absolute);
		}

		/// <summary>Combina as duas geometrias usando o <see cref="T:System.Windows.Media.GeometryCombineMode" /> especificado e o fator de tolerância e aplica a transformação especificada à geometria resultante.</summary>
		/// <param name="geometry1">A primeira geometria a combinar.</param>
		/// <param name="geometry2">A segunda geometria a combinar.</param>
		/// <param name="mode">Um dos valores de enumeração que especifica como as geometrias são combinadas.</param>
		/// <param name="transform">Uma transformação a ser aplicada à geometria combinada ou <see langword="null" />.</param>
		/// <param name="tolerance">Os limites máximos na distância entre pontos na aproximação poligonal das geometrias. Valores menores produzem resultados mais precisos, mas causam a execução lenta. Se <paramref name="tolerance" /> for menor que 0,000001, 0,000001 será usado.</param>
		/// <param name="type">Um dos valores <see cref="T:System.Windows.Media.ToleranceType" /> que especifica se o fator de tolerância é um valor absoluto ou relativo à área da geometria.</param>
		/// <returns>A geometria combinada.</returns>
		// Token: 0x0600237D RID: 9085 RVA: 0x0008F0B0 File Offset: 0x0008E4B0
		public static PathGeometry Combine(Geometry geometry1, Geometry geometry2, GeometryCombineMode mode, Transform transform, double tolerance, ToleranceType type)
		{
			return PathGeometry.InternalCombine(geometry1, geometry2, mode, transform, tolerance, type);
		}

		/// <summary>Combina as duas geometrias usando o <see cref="T:System.Windows.Media.GeometryCombineMode" /> especificado e aplica a transformação especificada à geometria resultante.</summary>
		/// <param name="geometry1">A primeira geometria a combinar.</param>
		/// <param name="geometry2">A segunda geometria a combinar.</param>
		/// <param name="mode">Um dos valores de enumeração que especifica como as geometrias são combinadas.</param>
		/// <param name="transform">Uma transformação a ser aplicada à geometria combinada ou <see langword="null" />.</param>
		/// <returns>A geometria combinada.</returns>
		// Token: 0x0600237E RID: 9086 RVA: 0x0008F0CC File Offset: 0x0008E4CC
		public static PathGeometry Combine(Geometry geometry1, Geometry geometry2, GeometryCombineMode mode, Transform transform)
		{
			return PathGeometry.InternalCombine(geometry1, geometry2, mode, transform, Geometry.StandardFlatteningTolerance, ToleranceType.Absolute);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.PathGeometry" />, dentro da tolerância especificada, que é um contorno simplificado da região preenchida da <see cref="T:System.Windows.Media.Geometry" />.</summary>
		/// <param name="tolerance">Os limites máximos na distância entre pontos na aproximação poligonal da geometria. Valores menores produzem resultados mais precisos, mas causam a execução lenta. Se <paramref name="tolerance" /> for menor que 0,000001, 0,000001 será usado.</param>
		/// <param name="type">Um dos valores <see cref="T:System.Windows.Media.ToleranceType" /> que especifica se o fator de tolerância é um valor absoluto ou relativo à área da geometria.</param>
		/// <returns>Um contorno simplificado da região preenchida da <see cref="T:System.Windows.Media.Geometry" />.</returns>
		// Token: 0x0600237F RID: 9087 RVA: 0x0008F0E8 File Offset: 0x0008E4E8
		[SecurityCritical]
		public unsafe virtual PathGeometry GetOutlinedPathGeometry(double tolerance, ToleranceType type)
		{
			base.ReadPreamble();
			if (this.IsObviouslyEmpty())
			{
				return new PathGeometry();
			}
			Geometry.PathGeometryData pathGeometryData = this.GetPathGeometryData();
			if (pathGeometryData.IsEmpty())
			{
				return new PathGeometry();
			}
			byte[] array;
			byte* ptr;
			if ((array = pathGeometryData.SerializedData) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			Invariant.Assert(ptr != null);
			FillRule fillRule = FillRule.Nonzero;
			PathGeometry.FigureList figureList = new PathGeometry.FigureList();
			int num = UnsafeNativeMethods.MilCoreApi.MilUtility_PathGeometryOutline(&pathGeometryData.Matrix, pathGeometryData.FillRule, ptr, pathGeometryData.Size, tolerance, type == ToleranceType.Relative, new PathGeometry.AddFigureToListDelegate(figureList.AddFigureToList), out fillRule);
			PathGeometry result;
			if (num == -2003304438)
			{
				result = new PathGeometry();
			}
			else
			{
				HRESULT.Check(num);
				result = new PathGeometry(figureList.Figures, fillRule, null);
			}
			array = null;
			return result;
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.PathGeometry" /> que é um contorno simplificado da região preenchida da <see cref="T:System.Windows.Media.Geometry" />.</summary>
		/// <returns>Um contorno simplificado da região preenchida da <see cref="T:System.Windows.Media.Geometry" />.</returns>
		// Token: 0x06002380 RID: 9088 RVA: 0x0008F1B4 File Offset: 0x0008E5B4
		public PathGeometry GetOutlinedPathGeometry()
		{
			return this.GetOutlinedPathGeometry(Geometry.StandardFlatteningTolerance, ToleranceType.Absolute);
		}

		// Token: 0x06002381 RID: 9089
		internal abstract PathGeometry GetAsPathGeometry();

		// Token: 0x06002382 RID: 9090
		internal abstract Geometry.PathGeometryData GetPathGeometryData();

		// Token: 0x06002383 RID: 9091 RVA: 0x0008F1D0 File Offset: 0x0008E5D0
		internal PathFigureCollection GetPathFigureCollection()
		{
			return this.GetTransformedFigureCollection(null);
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x0008F1E4 File Offset: 0x0008E5E4
		internal Matrix GetCombinedMatrix(Transform transform)
		{
			Matrix matrix = Matrix.Identity;
			Transform transform2 = this.Transform;
			if (transform2 != null && !transform2.IsIdentity)
			{
				matrix = transform2.Value;
				if (transform != null && !transform.IsIdentity)
				{
					matrix *= transform.Value;
				}
			}
			else if (transform != null && !transform.IsIdentity)
			{
				matrix = transform.Value;
			}
			return matrix;
		}

		// Token: 0x06002385 RID: 9093
		internal abstract PathFigureCollection GetTransformedFigureCollection(Transform transform);

		// Token: 0x06002386 RID: 9094 RVA: 0x0008F240 File Offset: 0x0008E640
		internal virtual bool IsObviouslyEmpty()
		{
			return this.IsEmpty();
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x0008F254 File Offset: 0x0008E654
		internal virtual bool CanSerializeToString()
		{
			return false;
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x0008F264 File Offset: 0x0008E664
		internal static Geometry.PathGeometryData GetEmptyPathGeometryData()
		{
			return Geometry.s_emptyPathGeometryData;
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x0008F278 File Offset: 0x0008E678
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private unsafe static Geometry.PathGeometryData MakeEmptyPathGeometryData()
		{
			Geometry.PathGeometryData pathGeometryData = default(Geometry.PathGeometryData);
			pathGeometryData.FillRule = FillRule.EvenOdd;
			pathGeometryData.Matrix = CompositionResourceManager.MatrixToMilMatrix3x2D(Matrix.Identity);
			int num = sizeof(MIL_PATHGEOMETRY);
			pathGeometryData.SerializedData = new byte[num];
			byte[] array;
			byte* ptr;
			if ((array = pathGeometryData.SerializedData) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			MIL_PATHGEOMETRY* ptr2 = (MIL_PATHGEOMETRY*)ptr;
			ptr2->FigureCount = 0U;
			ptr2->Size = (uint)num;
			array = null;
			return pathGeometryData;
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x0008F2EC File Offset: 0x0008E6EC
		private static Geometry MakeEmptyGeometry()
		{
			Geometry geometry = new StreamGeometry();
			geometry.Freeze();
			return geometry;
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Geometry.Transform" />.</summary>
		// Token: 0x04001146 RID: 4422
		public static readonly DependencyProperty TransformProperty;

		// Token: 0x04001147 RID: 4423
		internal static Transform s_Transform = Transform.Identity;

		// Token: 0x04001148 RID: 4424
		private const double c_tolerance = 0.25;

		// Token: 0x04001149 RID: 4425
		private static Geometry s_empty = Geometry.MakeEmptyGeometry();

		// Token: 0x0400114A RID: 4426
		private static Geometry.PathGeometryData s_emptyPathGeometryData = Geometry.MakeEmptyPathGeometryData();

		// Token: 0x02000873 RID: 2163
		internal struct PathGeometryData
		{
			// Token: 0x06005781 RID: 22401 RVA: 0x00165A90 File Offset: 0x00164E90
			[SecurityCritical]
			[SecurityTreatAsSafe]
			internal unsafe bool IsEmpty()
			{
				if (this.SerializedData == null || this.SerializedData.Length == 0)
				{
					return true;
				}
				byte[] serializedData;
				byte* ptr;
				if ((serializedData = this.SerializedData) == null || serializedData.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &serializedData[0];
				}
				MIL_PATHGEOMETRY* ptr2 = (MIL_PATHGEOMETRY*)ptr;
				return ptr2->FigureCount <= 0U;
			}

			// Token: 0x1700120D RID: 4621
			// (get) Token: 0x06005782 RID: 22402 RVA: 0x00165ADC File Offset: 0x00164EDC
			internal unsafe uint Size
			{
				[SecurityTreatAsSafe]
				[SecurityCritical]
				get
				{
					if (this.SerializedData == null || this.SerializedData.Length == 0)
					{
						return 0U;
					}
					byte[] serializedData;
					byte* ptr;
					if ((serializedData = this.SerializedData) == null || serializedData.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &serializedData[0];
					}
					MIL_PATHGEOMETRY* ptr2 = (MIL_PATHGEOMETRY*)ptr;
					uint num = (ptr2 == null) ? 0U : ptr2->Size;
					Invariant.Assert(num <= (uint)this.SerializedData.Length);
					return num;
				}
			}

			// Token: 0x0400288A RID: 10378
			internal FillRule FillRule;

			// Token: 0x0400288B RID: 10379
			internal MilMatrix3x2D Matrix;

			// Token: 0x0400288C RID: 10380
			internal byte[] SerializedData;
		}
	}
}
