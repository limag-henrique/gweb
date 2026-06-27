using System;
using System.ComponentModel;
using System.Security;
using System.Windows.Markup;
using System.Windows.Media.Composition;
using System.Windows.Media.Converters;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Define a funcionalidade de habilita transformações em um plano 2-D. As transformações incluem rotação (<see cref="T:System.Windows.Media.RotateTransform" />), ajuste de escala (<see cref="T:System.Windows.Media.ScaleTransform" />), distorção (<see cref="T:System.Windows.Media.SkewTransform" />) e translação (<see cref="T:System.Windows.Media.TranslateTransform" />). Essa hierarquia de classes difere da estrutura <see cref="T:System.Windows.Media.Matrix" /> porque trata-se de uma classe e dá suporte à semântica de enumeração e animação.</summary>
	// Token: 0x020003F8 RID: 1016
	[ValueSerializer(typeof(TransformValueSerializer))]
	[TypeConverter(typeof(TransformConverter))]
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public abstract class Transform : GeneralTransform, DUCE.IResource
	{
		/// <summary>Cria uma cópia modificável deste <see cref="T:System.Windows.Media.Transform" /> fazendo cópias em profundidade de seus valores.</summary>
		/// <returns>Uma cópia em profundidade modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado retorna <see langword="false" /> mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem é <see langword="true." /></returns>
		// Token: 0x06002822 RID: 10274 RVA: 0x000A1478 File Offset: 0x000A0878
		public new Transform Clone()
		{
			return (Transform)base.Clone();
		}

		/// <summary>Cria um clone modificável deste objeto <see cref="T:System.Windows.Media.Transform" /> fazendo cópias em profundidade de seus valores. Esse método não copia referências de recurso, associações de dados ou animações, embora ele copie os valores atuais.</summary>
		/// <returns>Uma cópia em profundidade modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado é <see langword="false" /> mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem é <see langword="true" />.</returns>
		// Token: 0x06002823 RID: 10275 RVA: 0x000A1490 File Offset: 0x000A0890
		public new Transform CloneCurrentValue()
		{
			return (Transform)base.CloneCurrentValue();
		}

		// Token: 0x06002824 RID: 10276
		internal abstract DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel);

		// Token: 0x06002825 RID: 10277 RVA: 0x000A14A8 File Offset: 0x000A08A8
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			DUCE.ResourceHandle result;
			using (CompositionEngineLock.Acquire())
			{
				result = this.AddRefOnChannelCore(channel);
			}
			return result;
		}

		// Token: 0x06002826 RID: 10278
		internal abstract void ReleaseOnChannelCore(DUCE.Channel channel);

		// Token: 0x06002827 RID: 10279 RVA: 0x000A14F0 File Offset: 0x000A08F0
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			using (CompositionEngineLock.Acquire())
			{
				this.ReleaseOnChannelCore(channel);
			}
		}

		// Token: 0x06002828 RID: 10280
		internal abstract DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel);

		// Token: 0x06002829 RID: 10281 RVA: 0x000A1538 File Offset: 0x000A0938
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handleCore;
			using (CompositionEngineLock.Acquire())
			{
				handleCore = this.GetHandleCore(channel);
			}
			return handleCore;
		}

		// Token: 0x0600282A RID: 10282
		internal abstract int GetChannelCountCore();

		// Token: 0x0600282B RID: 10283 RVA: 0x000A1580 File Offset: 0x000A0980
		int DUCE.IResource.GetChannelCount()
		{
			return this.GetChannelCountCore();
		}

		// Token: 0x0600282C RID: 10284
		internal abstract DUCE.Channel GetChannelCore(int index);

		// Token: 0x0600282D RID: 10285 RVA: 0x000A1594 File Offset: 0x000A0994
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this.GetChannelCore(index);
		}

		/// <summary>Cria um novo <see cref="T:System.Windows.Media.Transform" /> da representação de cadeia de caracteres especificada de uma matriz de transformação.</summary>
		/// <param name="source">Seis valores <see cref="T:System.Double" /> delimitados por vírgula que descrevem o novo <see cref="T:System.Windows.Media.Transform" />. Consulte também os comentários.</param>
		/// <returns>Uma nova transformação construída na cadeia de caracteres especificada.</returns>
		// Token: 0x0600282E RID: 10286 RVA: 0x000A15A8 File Offset: 0x000A09A8
		public static Transform Parse(string source)
		{
			IFormatProvider invariantEnglishUS = TypeConverterHelper.InvariantEnglishUS;
			return Parsers.ParseTransform(source, invariantEnglishUS);
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x000A15C4 File Offset: 0x000A09C4
		internal Transform()
		{
		}

		/// <summary>Obtém uma transformação de identidade.</summary>
		/// <returns>Uma transformação de identidade.</returns>
		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06002830 RID: 10288 RVA: 0x000A15D8 File Offset: 0x000A09D8
		public static Transform Identity
		{
			get
			{
				return System.Windows.Media.Transform.s_identity;
			}
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x000A15EC File Offset: 0x000A09EC
		private static Transform MakeIdentityTransform()
		{
			Transform transform = new MatrixTransform(Matrix.Identity);
			transform.Freeze();
			return transform;
		}

		/// <summary>Obtém a transformação atual como um objeto <see cref="T:System.Windows.Media.Matrix" />.</summary>
		/// <returns>A transformação de matriz atual.</returns>
		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06002832 RID: 10290
		public abstract Matrix Value { get; }

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06002833 RID: 10291
		internal abstract bool IsIdentity { get; }

		// Token: 0x06002834 RID: 10292 RVA: 0x000A160C File Offset: 0x000A0A0C
		internal virtual bool CanSerializeToString()
		{
			return false;
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x000A161C File Offset: 0x000A0A1C
		internal virtual void TransformRect(ref Rect rect)
		{
			Matrix value = this.Value;
			MatrixUtil.TransformRect(ref rect, ref value);
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x000A1638 File Offset: 0x000A0A38
		internal virtual void MultiplyValueByMatrix(ref Matrix result, ref Matrix matrixToMultiplyBy)
		{
			result = this.Value;
			MatrixUtil.MultiplyMatrix(ref result, ref matrixToMultiplyBy);
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x000A1658 File Offset: 0x000A0A58
		[SecurityCritical]
		internal unsafe virtual void ConvertToD3DMATRIX(D3DMATRIX* milMatrix)
		{
			Matrix value = this.Value;
			MILUtilities.ConvertToD3DMATRIX(&value, milMatrix);
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x000A1678 File Offset: 0x000A0A78
		internal static void GetTransformValue(Transform transform, out Matrix currentTransformValue)
		{
			if (transform != null)
			{
				currentTransformValue = transform.Value;
				return;
			}
			currentTransformValue = Matrix.Identity;
		}

		/// <summary>Tenta transformar o ponto especificado e retorna um valor que indica se a transformação foi bem-sucedida.</summary>
		/// <param name="inPoint">O ponto a ser transformado.</param>
		/// <param name="result">O resultado de transformar <paramref name="inPoint" />.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="inPoint" /> foi transformado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002839 RID: 10297 RVA: 0x000A16A0 File Offset: 0x000A0AA0
		public override bool TryTransform(Point inPoint, out Point result)
		{
			result = this.Value.Transform(inPoint);
			return true;
		}

		/// <summary>Transforma a caixa delimitadora especificada e retorna uma caixa delimitadora alinhada por eixo exatamente grande o suficiente para contê-la.</summary>
		/// <param name="rect">A caixa delimitadora a ser transformada.</param>
		/// <returns>A menor caixa delimitadora alinhada por eixo que pode conter o <paramref name="rect" /> transformado.</returns>
		// Token: 0x0600283A RID: 10298 RVA: 0x000A16C4 File Offset: 0x000A0AC4
		public override Rect TransformBounds(Rect rect)
		{
			this.TransformRect(ref rect);
			return rect;
		}

		/// <summary>Obtém o inverso dessa transformação, se ele existir.</summary>
		/// <returns>O inverso dessa transformação, se ele existir. Caso contrário, <see langword="null" />.</returns>
		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x0600283B RID: 10299 RVA: 0x000A16DC File Offset: 0x000A0ADC
		public override GeneralTransform Inverse
		{
			get
			{
				base.ReadPreamble();
				Matrix value = this.Value;
				if (!value.HasInverse)
				{
					return null;
				}
				value.Invert();
				return new MatrixTransform(value);
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x0600283C RID: 10300 RVA: 0x000A1710 File Offset: 0x000A0B10
		internal override Transform AffineTransform
		{
			[FriendAccessAllowed]
			get
			{
				return this;
			}
		}

		// Token: 0x04001292 RID: 4754
		private static Transform s_identity = System.Windows.Media.Transform.MakeIdentityTransform();
	}
}
