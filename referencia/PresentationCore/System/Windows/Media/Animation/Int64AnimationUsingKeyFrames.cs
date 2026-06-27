using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Markup;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Int64" /> junto com um conjunto de <see cref="P:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames.KeyFrames" />.</summary>
	// Token: 0x020004F7 RID: 1271
	[ContentProperty("KeyFrames")]
	public class Int64AnimationUsingKeyFrames : Int64AnimationBase, IKeyFrameAnimation, IAddChild
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" />.</summary>
		// Token: 0x06003986 RID: 14726 RVA: 0x000E42B0 File Offset: 0x000E36B0
		public Int64AnimationUsingKeyFrames()
		{
			this._areKeyTimesValid = true;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003987 RID: 14727 RVA: 0x000E42CC File Offset: 0x000E36CC
		public new Int64AnimationUsingKeyFrames Clone()
		{
			return (Int64AnimationUsingKeyFrames)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06003988 RID: 14728 RVA: 0x000E42E4 File Offset: 0x000E36E4
		public new Int64AnimationUsingKeyFrames CloneCurrentValue()
		{
			return (Int64AnimationUsingKeyFrames)base.CloneCurrentValue();
		}

		/// <summary>Faz com que esta instância do objeto <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" /> não modificável ou determina se ela pode se tornar não modificável.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> se este método deve simplesmente determinar se esta instância pode ser congelada. <see langword="false" /> se esta instância, na verdade, deve congelar a si mesma quando este método é chamado.</param>
		/// <returns>Se <paramref name="isChecking" /> é true, este método retorna <see langword="true" /> caso esta instância possa se tornar somente leitura ou então <see langword="false" /> se ela não pode se tornar somente leitura. Se <paramref name="isChecking" /> é false, este método retorna <see langword="true" /> se esta instância agora é somente leitura ou <see langword="false" />, se ela não pode tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x06003989 RID: 14729 RVA: 0x000E42FC File Offset: 0x000E36FC
		protected override bool FreezeCore(bool isChecking)
		{
			bool flag = base.FreezeCore(isChecking);
			flag &= Freezable.Freeze(this._keyFrames, isChecking);
			if (flag & !this._areKeyTimesValid)
			{
				this.ResolveKeyTimes();
			}
			return flag;
		}

		/// <summary>Chamado quando o objeto <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" /> atual é modificado.</summary>
		// Token: 0x0600398A RID: 14730 RVA: 0x000E4334 File Offset: 0x000E3734
		protected override void OnChanged()
		{
			this._areKeyTimesValid = false;
			base.OnChanged();
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" />.</returns>
		// Token: 0x0600398B RID: 14731 RVA: 0x000E4350 File Offset: 0x000E3750
		protected override Freezable CreateInstanceCore()
		{
			return new Int64AnimationUsingKeyFrames();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" /> a ser clonado.</param>
		// Token: 0x0600398C RID: 14732 RVA: 0x000E4364 File Offset: 0x000E3764
		protected override void CloneCore(Freezable sourceFreezable)
		{
			Int64AnimationUsingKeyFrames sourceAnimation = (Int64AnimationUsingKeyFrames)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(sourceAnimation, false);
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" /> a ser clonado.</param>
		// Token: 0x0600398D RID: 14733 RVA: 0x000E4388 File Offset: 0x000E3788
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			Int64AnimationUsingKeyFrames sourceAnimation = (Int64AnimationUsingKeyFrames)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(sourceAnimation, true);
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" /> especificado.</summary>
		/// <param name="source">O objeto <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" /> a ser clonado.</param>
		// Token: 0x0600398E RID: 14734 RVA: 0x000E43AC File Offset: 0x000E37AC
		protected override void GetAsFrozenCore(Freezable source)
		{
			Int64AnimationUsingKeyFrames sourceAnimation = (Int64AnimationUsingKeyFrames)source;
			base.GetAsFrozenCore(source);
			this.CopyCommon(sourceAnimation, false);
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="source">O <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" /> a ser copiado e congelado.</param>
		// Token: 0x0600398F RID: 14735 RVA: 0x000E43D0 File Offset: 0x000E37D0
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			Int64AnimationUsingKeyFrames sourceAnimation = (Int64AnimationUsingKeyFrames)source;
			base.GetCurrentValueAsFrozenCore(source);
			this.CopyCommon(sourceAnimation, true);
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x000E43F4 File Offset: 0x000E37F4
		private void CopyCommon(Int64AnimationUsingKeyFrames sourceAnimation, bool isCurrentValueClone)
		{
			this._areKeyTimesValid = sourceAnimation._areKeyTimesValid;
			if (this._areKeyTimesValid && sourceAnimation._sortedResolvedKeyFrames != null)
			{
				this._sortedResolvedKeyFrames = (ResolvedKeyFrameEntry[])sourceAnimation._sortedResolvedKeyFrames.Clone();
			}
			if (sourceAnimation._keyFrames != null)
			{
				if (isCurrentValueClone)
				{
					this._keyFrames = (Int64KeyFrameCollection)sourceAnimation._keyFrames.CloneCurrentValue();
				}
				else
				{
					this._keyFrames = sourceAnimation._keyFrames.Clone();
				}
				base.OnFreezablePropertyChanged(null, this._keyFrames);
			}
		}

		/// <summary>Adiciona um objeto filho.</summary>
		/// <param name="child">O objeto filho a ser adicionado.</param>
		// Token: 0x06003991 RID: 14737 RVA: 0x000E4474 File Offset: 0x000E3874
		void IAddChild.AddChild(object child)
		{
			base.WritePreamble();
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			this.AddChild(child);
			base.WritePostscript();
		}

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> filho a este <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" />.</summary>
		/// <param name="child">O objeto a ser adicionado como filho deste <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" />.</param>
		/// <exception cref="T:System.ArgumentException">O parâmetro <paramref name="child" /> não é um <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" />.</exception>
		// Token: 0x06003992 RID: 14738 RVA: 0x000E44A4 File Offset: 0x000E38A4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void AddChild(object child)
		{
			Int64KeyFrame int64KeyFrame = child as Int64KeyFrame;
			if (int64KeyFrame != null)
			{
				this.KeyFrames.Add(int64KeyFrame);
				return;
			}
			throw new ArgumentException(SR.Get("Animation_ChildMustBeKeyFrame"), "child");
		}

		/// <summary>Adiciona o conteúdo do texto de um nó ao objeto.</summary>
		/// <param name="childText">O texto a ser adicionado ao objeto.</param>
		// Token: 0x06003993 RID: 14739 RVA: 0x000E44E0 File Offset: 0x000E38E0
		void IAddChild.AddText(string childText)
		{
			if (childText == null)
			{
				throw new ArgumentNullException("childText");
			}
			this.AddText(childText);
		}

		/// <summary>Adiciona uma cadeia de caracteres de texto como um filho deste <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" />.</summary>
		/// <param name="childText">O texto adicionado ao <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" />.</param>
		/// <exception cref="T:System.InvalidOperationException">Um <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" /> não aceita texto como um filho, portanto, esse método gerará esta exceção, a menos que uma classe derivada tenha substituído esse comportamento, o que permite que o texto seja adicionado.</exception>
		// Token: 0x06003994 RID: 14740 RVA: 0x000E4504 File Offset: 0x000E3904
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void AddText(string childText)
		{
			throw new InvalidOperationException(SR.Get("Animation_NoTextChildren"));
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado por esta instância do <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação de host.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela instância atual.</returns>
		// Token: 0x06003995 RID: 14741 RVA: 0x000E4520 File Offset: 0x000E3920
		protected sealed override long GetCurrentValueCore(long defaultOriginValue, long defaultDestinationValue, AnimationClock animationClock)
		{
			if (this._keyFrames == null)
			{
				return defaultDestinationValue;
			}
			if (!this._areKeyTimesValid)
			{
				this.ResolveKeyTimes();
			}
			if (this._sortedResolvedKeyFrames == null)
			{
				return defaultDestinationValue;
			}
			TimeSpan value = animationClock.CurrentTime.Value;
			int num = this._sortedResolvedKeyFrames.Length;
			int num2 = num - 1;
			int i;
			for (i = 0; i < num; i++)
			{
				if (!(value > this._sortedResolvedKeyFrames[i]._resolvedKeyTime))
				{
					break;
				}
			}
			while (i < num2 && value == this._sortedResolvedKeyFrames[i + 1]._resolvedKeyTime)
			{
				i++;
			}
			long num3;
			if (i == num)
			{
				num3 = this.GetResolvedKeyFrameValue(num2);
			}
			else if (value == this._sortedResolvedKeyFrames[i]._resolvedKeyTime)
			{
				num3 = this.GetResolvedKeyFrameValue(i);
			}
			else
			{
				long baseValue;
				double keyFrameProgress;
				if (i == 0)
				{
					if (this.IsAdditive)
					{
						baseValue = AnimatedTypeHelpers.GetZeroValueInt64(defaultOriginValue);
					}
					else
					{
						baseValue = defaultOriginValue;
					}
					keyFrameProgress = value.TotalMilliseconds / this._sortedResolvedKeyFrames[0]._resolvedKeyTime.TotalMilliseconds;
				}
				else
				{
					int num4 = i - 1;
					TimeSpan resolvedKeyTime = this._sortedResolvedKeyFrames[num4]._resolvedKeyTime;
					baseValue = this.GetResolvedKeyFrameValue(num4);
					TimeSpan timeSpan = value - resolvedKeyTime;
					TimeSpan timeSpan2 = this._sortedResolvedKeyFrames[i]._resolvedKeyTime - resolvedKeyTime;
					keyFrameProgress = timeSpan.TotalMilliseconds / timeSpan2.TotalMilliseconds;
				}
				num3 = this.GetResolvedKeyFrame(i).InterpolateValue(baseValue, keyFrameProgress);
			}
			if (this.IsCumulative)
			{
				double num5 = (double)(animationClock.CurrentIteration - 1).Value;
				if (num5 > 0.0)
				{
					num3 = AnimatedTypeHelpers.AddInt64(num3, AnimatedTypeHelpers.ScaleInt64(this.GetResolvedKeyFrameValue(num2), num5));
				}
			}
			if (this.IsAdditive)
			{
				return AnimatedTypeHelpers.AddInt64(defaultOriginValue, num3);
			}
			return num3;
		}

		/// <summary>Forneça uma <see cref="T:System.Windows.Duration" /> natural personalizada quando a propriedade <see cref="T:System.Windows.Duration" /> for definida como <see cref="P:System.Windows.Duration.Automatic" />.</summary>
		/// <param name="clock">O <see cref="T:System.Windows.Media.Animation.Clock" /> cuja duração natural é desejada.</param>
		/// <returns>Se o último quadro-chave dessa animação for um <see cref="T:System.Windows.Media.Animation.KeyTime" />, esse valor será usado como a <see cref="P:System.Windows.Media.Animation.Clock.NaturalDuration" />; caso contrário, ela será de um segundo.</returns>
		// Token: 0x06003996 RID: 14742 RVA: 0x000E4714 File Offset: 0x000E3B14
		protected sealed override Duration GetNaturalDurationCore(Clock clock)
		{
			return new Duration(this.LargestTimeSpanKeyTime);
		}

		/// <summary>Obtém ou define uma coleção ordenada P:System.Windows.Media.Animation.IKeyFrameAnimation.KeyFrames associada a esta sequência de animação.</summary>
		/// <returns>Um <see cref="T:System.Collections.IList" /> de <see cref="P:System.Windows.Media.Animation.IKeyFrameAnimation.KeyFrames" />.</returns>
		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06003997 RID: 14743 RVA: 0x000E472C File Offset: 0x000E3B2C
		// (set) Token: 0x06003998 RID: 14744 RVA: 0x000E4740 File Offset: 0x000E3B40
		IList IKeyFrameAnimation.KeyFrames
		{
			get
			{
				return this.KeyFrames;
			}
			set
			{
				this.KeyFrames = (Int64KeyFrameCollection)value;
			}
		}

		/// <summary>Obtém ou define a coleção de objetos <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> que definem a animação.</summary>
		/// <returns>A coleção de <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> objetos que definem a animação. O valor padrão é <see cref="P:System.Windows.Media.Animation.Int64KeyFrameCollection.Empty" />.</returns>
		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06003999 RID: 14745 RVA: 0x000E475C File Offset: 0x000E3B5C
		// (set) Token: 0x0600399A RID: 14746 RVA: 0x000E47B8 File Offset: 0x000E3BB8
		public Int64KeyFrameCollection KeyFrames
		{
			get
			{
				base.ReadPreamble();
				if (this._keyFrames == null)
				{
					if (base.IsFrozen)
					{
						this._keyFrames = Int64KeyFrameCollection.Empty;
					}
					else
					{
						base.WritePreamble();
						this._keyFrames = new Int64KeyFrameCollection();
						base.OnFreezablePropertyChanged(null, this._keyFrames);
						base.WritePostscript();
					}
				}
				return this._keyFrames;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.WritePreamble();
				if (value != this._keyFrames)
				{
					base.OnFreezablePropertyChanged(this._keyFrames, value);
					this._keyFrames = value;
					base.WritePostscript();
				}
			}
		}

		/// <summary>Retornará true se o valor da propriedade <see cref="P:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames.KeyFrames" /> desta instância do <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" /> precisar ser serializado por valor.</summary>
		/// <returns>
		///   <see langword="true" /> se o valor da propriedade precisar ser serializado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600399B RID: 14747 RVA: 0x000E47FC File Offset: 0x000E3BFC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeKeyFrames()
		{
			base.ReadPreamble();
			return this._keyFrames != null && this._keyFrames.Count > 0;
		}

		/// <summary>Obtém um valor que especifica se o valor de saída da animação é adicionado ao valor base da propriedade que está sendo animada.</summary>
		/// <returns>
		///   <see langword="true" /> Se a animação adicionará o valor de saída para o valor base da propriedade sendo animada, em vez de substituí-la; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x0600399C RID: 14748 RVA: 0x000E4828 File Offset: 0x000E3C28
		// (set) Token: 0x0600399D RID: 14749 RVA: 0x000E4848 File Offset: 0x000E3C48
		public bool IsAdditive
		{
			get
			{
				return (bool)base.GetValue(AnimationTimeline.IsAdditiveProperty);
			}
			set
			{
				base.SetValueInternal(AnimationTimeline.IsAdditiveProperty, BooleanBoxes.Box(value));
			}
		}

		/// <summary>Obtém ou define um valor que especifica se o valor da animação acumula quando ela se repete.</summary>
		/// <returns>
		///   <see langword="true" /> Se a animação acumula seus valores ao seu <see cref="P:System.Windows.Media.Animation.Timeline.RepeatBehavior" /> propriedade faz com que ele Repita sua duração simples; caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x0600399E RID: 14750 RVA: 0x000E4868 File Offset: 0x000E3C68
		// (set) Token: 0x0600399F RID: 14751 RVA: 0x000E4888 File Offset: 0x000E3C88
		public bool IsCumulative
		{
			get
			{
				return (bool)base.GetValue(AnimationTimeline.IsCumulativeProperty);
			}
			set
			{
				base.SetValueInternal(AnimationTimeline.IsCumulativeProperty, BooleanBoxes.Box(value));
			}
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x000E48A8 File Offset: 0x000E3CA8
		private long GetResolvedKeyFrameValue(int resolvedKeyFrameIndex)
		{
			return this.GetResolvedKeyFrame(resolvedKeyFrameIndex).Value;
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x000E48C4 File Offset: 0x000E3CC4
		private Int64KeyFrame GetResolvedKeyFrame(int resolvedKeyFrameIndex)
		{
			return this._keyFrames[this._sortedResolvedKeyFrames[resolvedKeyFrameIndex]._originalKeyFrameIndex];
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x060039A2 RID: 14754 RVA: 0x000E48F0 File Offset: 0x000E3CF0
		private TimeSpan LargestTimeSpanKeyTime
		{
			get
			{
				bool flag = false;
				TimeSpan timeSpan = TimeSpan.Zero;
				if (this._keyFrames != null)
				{
					int count = this._keyFrames.Count;
					for (int i = 0; i < count; i++)
					{
						KeyTime keyTime = this._keyFrames[i].KeyTime;
						if (keyTime.Type == KeyTimeType.TimeSpan)
						{
							flag = true;
							if (keyTime.TimeSpan > timeSpan)
							{
								timeSpan = keyTime.TimeSpan;
							}
						}
					}
				}
				if (flag)
				{
					return timeSpan;
				}
				return TimeSpan.FromSeconds(1.0);
			}
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x000E4970 File Offset: 0x000E3D70
		private void ResolveKeyTimes()
		{
			int num = 0;
			if (this._keyFrames != null)
			{
				num = this._keyFrames.Count;
			}
			if (num == 0)
			{
				this._sortedResolvedKeyFrames = null;
				this._areKeyTimesValid = true;
				return;
			}
			this._sortedResolvedKeyFrames = new ResolvedKeyFrameEntry[num];
			int i;
			for (i = 0; i < num; i++)
			{
				this._sortedResolvedKeyFrames[i]._originalKeyFrameIndex = i;
			}
			TimeSpan resolvedKeyTime = TimeSpan.Zero;
			Duration duration = base.Duration;
			if (duration.HasTimeSpan)
			{
				resolvedKeyTime = duration.TimeSpan;
			}
			else
			{
				resolvedKeyTime = this.LargestTimeSpanKeyTime;
			}
			int num2 = num - 1;
			ArrayList arrayList = new ArrayList();
			bool flag = false;
			i = 0;
			while (i < num)
			{
				KeyTime keyTime = this._keyFrames[i].KeyTime;
				switch (keyTime.Type)
				{
				case KeyTimeType.Uniform:
				case KeyTimeType.Paced:
					if (i == num2)
					{
						this._sortedResolvedKeyFrames[i]._resolvedKeyTime = resolvedKeyTime;
						i++;
					}
					else if (i == 0 && keyTime.Type == KeyTimeType.Paced)
					{
						this._sortedResolvedKeyFrames[i]._resolvedKeyTime = TimeSpan.Zero;
						i++;
					}
					else
					{
						if (keyTime.Type == KeyTimeType.Paced)
						{
							flag = true;
						}
						Int64AnimationUsingKeyFrames.KeyTimeBlock keyTimeBlock = default(Int64AnimationUsingKeyFrames.KeyTimeBlock);
						keyTimeBlock.BeginIndex = i;
						while (++i < num2)
						{
							KeyTimeType type = this._keyFrames[i].KeyTime.Type;
							if (type == KeyTimeType.Percent || type == KeyTimeType.TimeSpan)
							{
								break;
							}
							if (type == KeyTimeType.Paced)
							{
								flag = true;
							}
						}
						keyTimeBlock.EndIndex = i;
						arrayList.Add(keyTimeBlock);
					}
					break;
				case KeyTimeType.Percent:
					this._sortedResolvedKeyFrames[i]._resolvedKeyTime = TimeSpan.FromMilliseconds(keyTime.Percent * resolvedKeyTime.TotalMilliseconds);
					i++;
					break;
				case KeyTimeType.TimeSpan:
					this._sortedResolvedKeyFrames[i]._resolvedKeyTime = keyTime.TimeSpan;
					i++;
					break;
				}
			}
			for (int j = 0; j < arrayList.Count; j++)
			{
				Int64AnimationUsingKeyFrames.KeyTimeBlock keyTimeBlock2 = (Int64AnimationUsingKeyFrames.KeyTimeBlock)arrayList[j];
				TimeSpan timeSpan = TimeSpan.Zero;
				if (keyTimeBlock2.BeginIndex > 0)
				{
					timeSpan = this._sortedResolvedKeyFrames[keyTimeBlock2.BeginIndex - 1]._resolvedKeyTime;
				}
				long num3 = (long)(keyTimeBlock2.EndIndex - keyTimeBlock2.BeginIndex + 1);
				TimeSpan t = TimeSpan.FromTicks((this._sortedResolvedKeyFrames[keyTimeBlock2.EndIndex]._resolvedKeyTime - timeSpan).Ticks / num3);
				i = keyTimeBlock2.BeginIndex;
				TimeSpan timeSpan2 = timeSpan + t;
				while (i < keyTimeBlock2.EndIndex)
				{
					this._sortedResolvedKeyFrames[i]._resolvedKeyTime = timeSpan2;
					timeSpan2 += t;
					i++;
				}
			}
			if (flag)
			{
				this.ResolvePacedKeyTimes();
			}
			Array.Sort<ResolvedKeyFrameEntry>(this._sortedResolvedKeyFrames);
			this._areKeyTimesValid = true;
		}

		// Token: 0x060039A4 RID: 14756 RVA: 0x000E4C44 File Offset: 0x000E4044
		private void ResolvePacedKeyTimes()
		{
			int num = 1;
			int num2 = this._sortedResolvedKeyFrames.Length - 1;
			do
			{
				if (this._keyFrames[num].KeyTime.Type == KeyTimeType.Paced)
				{
					int num3 = num;
					List<double> list = new List<double>();
					TimeSpan resolvedKeyTime = this._sortedResolvedKeyFrames[num - 1]._resolvedKeyTime;
					double num4 = 0.0;
					long from = this._keyFrames[num - 1].Value;
					do
					{
						long value = this._keyFrames[num].Value;
						num4 += AnimatedTypeHelpers.GetSegmentLengthInt64(from, value);
						list.Add(num4);
						from = value;
						num++;
					}
					while (num < num2 && this._keyFrames[num].KeyTime.Type == KeyTimeType.Paced);
					num4 += AnimatedTypeHelpers.GetSegmentLengthInt64(from, this._keyFrames[num].Value);
					TimeSpan timeSpan = this._sortedResolvedKeyFrames[num]._resolvedKeyTime - resolvedKeyTime;
					int i = 0;
					int num5 = num3;
					while (i < list.Count)
					{
						this._sortedResolvedKeyFrames[num5]._resolvedKeyTime = resolvedKeyTime + TimeSpan.FromMilliseconds(list[i] / num4 * timeSpan.TotalMilliseconds);
						i++;
						num5++;
					}
				}
				else
				{
					num++;
				}
			}
			while (num < num2);
		}

		// Token: 0x040016B8 RID: 5816
		private Int64KeyFrameCollection _keyFrames;

		// Token: 0x040016B9 RID: 5817
		private ResolvedKeyFrameEntry[] _sortedResolvedKeyFrames;

		// Token: 0x040016BA RID: 5818
		private bool _areKeyTimesValid;

		// Token: 0x020008BD RID: 2237
		private struct KeyTimeBlock
		{
			// Token: 0x04002954 RID: 10580
			public int BeginIndex;

			// Token: 0x04002955 RID: 10581
			public int EndIndex;
		}
	}
}
