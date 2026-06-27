using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Markup;
using System.Windows.Media.Media3D;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Windows.Media.Media3D.Quaternion" /> junto com um conjunto de <see cref="P:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames.KeyFrames" /> em um <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> especificado.</summary>
	// Token: 0x02000533 RID: 1331
	[ContentProperty("KeyFrames")]
	public class QuaternionAnimationUsingKeyFrames : QuaternionAnimationBase, IKeyFrameAnimation, IAddChild
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" />.</summary>
		// Token: 0x06003C8F RID: 15503 RVA: 0x000EE1B0 File Offset: 0x000ED5B0
		public QuaternionAnimationUsingKeyFrames()
		{
			this._areKeyTimesValid = true;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003C90 RID: 15504 RVA: 0x000EE1CC File Offset: 0x000ED5CC
		public new QuaternionAnimationUsingKeyFrames Clone()
		{
			return (QuaternionAnimationUsingKeyFrames)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06003C91 RID: 15505 RVA: 0x000EE1E4 File Offset: 0x000ED5E4
		public new QuaternionAnimationUsingKeyFrames CloneCurrentValue()
		{
			return (QuaternionAnimationUsingKeyFrames)base.CloneCurrentValue();
		}

		/// <summary>Faz com que esta instância do objeto <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" /> não modificável ou determina se ela pode se tornar não modificável.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> a ser verificado se esta instância puder ser congelada; <see langword="false" /> para congelar esta instância.</param>
		/// <returns>Se <paramref name="isChecking" /> é true, este método retorna <see langword="true" /> caso esta instância possa se tornar somente leitura ou então <see langword="false" /> se ela não pode se tornar somente leitura. Se <paramref name="isChecking" /> é false, este método retorna <see langword="true" /> se esta instância agora é somente leitura ou <see langword="false" />, se ela não pode tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x06003C92 RID: 15506 RVA: 0x000EE1FC File Offset: 0x000ED5FC
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

		/// <summary>Chamado quando o objeto <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" /> atual é modificado.</summary>
		// Token: 0x06003C93 RID: 15507 RVA: 0x000EE234 File Offset: 0x000ED634
		protected override void OnChanged()
		{
			this._areKeyTimesValid = false;
			base.OnChanged();
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" />.</returns>
		// Token: 0x06003C94 RID: 15508 RVA: 0x000EE250 File Offset: 0x000ED650
		protected override Freezable CreateInstanceCore()
		{
			return new QuaternionAnimationUsingKeyFrames();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" /> a ser clonado.</param>
		// Token: 0x06003C95 RID: 15509 RVA: 0x000EE264 File Offset: 0x000ED664
		protected override void CloneCore(Freezable sourceFreezable)
		{
			QuaternionAnimationUsingKeyFrames sourceAnimation = (QuaternionAnimationUsingKeyFrames)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(sourceAnimation, false);
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" /> a ser clonado.</param>
		// Token: 0x06003C96 RID: 15510 RVA: 0x000EE288 File Offset: 0x000ED688
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			QuaternionAnimationUsingKeyFrames sourceAnimation = (QuaternionAnimationUsingKeyFrames)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(sourceAnimation, true);
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" /> especificado.</summary>
		/// <param name="source">O objeto <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" /> a ser clonado.</param>
		// Token: 0x06003C97 RID: 15511 RVA: 0x000EE2AC File Offset: 0x000ED6AC
		protected override void GetAsFrozenCore(Freezable source)
		{
			QuaternionAnimationUsingKeyFrames sourceAnimation = (QuaternionAnimationUsingKeyFrames)source;
			base.GetAsFrozenCore(source);
			this.CopyCommon(sourceAnimation, false);
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="source">O <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" /> a ser copiado e congelado.</param>
		// Token: 0x06003C98 RID: 15512 RVA: 0x000EE2D0 File Offset: 0x000ED6D0
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			QuaternionAnimationUsingKeyFrames sourceAnimation = (QuaternionAnimationUsingKeyFrames)source;
			base.GetCurrentValueAsFrozenCore(source);
			this.CopyCommon(sourceAnimation, true);
		}

		// Token: 0x06003C99 RID: 15513 RVA: 0x000EE2F4 File Offset: 0x000ED6F4
		private void CopyCommon(QuaternionAnimationUsingKeyFrames sourceAnimation, bool isCurrentValueClone)
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
					this._keyFrames = (QuaternionKeyFrameCollection)sourceAnimation._keyFrames.CloneCurrentValue();
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
		// Token: 0x06003C9A RID: 15514 RVA: 0x000EE374 File Offset: 0x000ED774
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

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> filho a este <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" />.</summary>
		/// <param name="child">O objeto a ser adicionado como filho deste <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" />.</param>
		/// <exception cref="T:System.ArgumentException">O parâmetro <paramref name="child" /> não é um <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" />.</exception>
		// Token: 0x06003C9B RID: 15515 RVA: 0x000EE3A4 File Offset: 0x000ED7A4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void AddChild(object child)
		{
			QuaternionKeyFrame quaternionKeyFrame = child as QuaternionKeyFrame;
			if (quaternionKeyFrame != null)
			{
				this.KeyFrames.Add(quaternionKeyFrame);
				return;
			}
			throw new ArgumentException(SR.Get("Animation_ChildMustBeKeyFrame"), "child");
		}

		/// <summary>Adiciona o conteúdo do texto de um nó ao objeto.</summary>
		/// <param name="childText">O texto a ser adicionado ao objeto.</param>
		// Token: 0x06003C9C RID: 15516 RVA: 0x000EE3E0 File Offset: 0x000ED7E0
		void IAddChild.AddText(string childText)
		{
			if (childText == null)
			{
				throw new ArgumentNullException("childText");
			}
			this.AddText(childText);
		}

		/// <summary>Adiciona uma cadeia de caracteres de texto como um filho deste <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" />.</summary>
		/// <param name="childText">O texto adicionado ao <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" />.</param>
		/// <exception cref="T:System.InvalidOperationException">Um <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" /> não aceita texto como um filho, portanto, esse método gerará esta exceção, a menos que uma classe derivada tenha substituído esse comportamento, o que permite que o texto seja adicionado.</exception>
		// Token: 0x06003C9D RID: 15517 RVA: 0x000EE404 File Offset: 0x000ED804
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void AddText(string childText)
		{
			throw new InvalidOperationException(SR.Get("Animation_NoTextChildren"));
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado por esta instância do <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação de host.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela instância atual.</returns>
		// Token: 0x06003C9E RID: 15518 RVA: 0x000EE420 File Offset: 0x000ED820
		protected sealed override Quaternion GetCurrentValueCore(Quaternion defaultOriginValue, Quaternion defaultDestinationValue, AnimationClock animationClock)
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
			Quaternion quaternion;
			if (i == num)
			{
				quaternion = this.GetResolvedKeyFrameValue(num2);
			}
			else if (value == this._sortedResolvedKeyFrames[i]._resolvedKeyTime)
			{
				quaternion = this.GetResolvedKeyFrameValue(i);
			}
			else
			{
				Quaternion baseValue;
				double keyFrameProgress;
				if (i == 0)
				{
					if (this.IsAdditive)
					{
						baseValue = AnimatedTypeHelpers.GetZeroValueQuaternion(defaultOriginValue);
					}
					else
					{
						baseValue = defaultOriginValue;
					}
					keyFrameProgress = value.TotalMilliseconds / this._sortedResolvedKeyFrames[0]._resolvedKeyTime.TotalMilliseconds;
				}
				else
				{
					int num3 = i - 1;
					TimeSpan resolvedKeyTime = this._sortedResolvedKeyFrames[num3]._resolvedKeyTime;
					baseValue = this.GetResolvedKeyFrameValue(num3);
					TimeSpan timeSpan = value - resolvedKeyTime;
					TimeSpan timeSpan2 = this._sortedResolvedKeyFrames[i]._resolvedKeyTime - resolvedKeyTime;
					keyFrameProgress = timeSpan.TotalMilliseconds / timeSpan2.TotalMilliseconds;
				}
				quaternion = this.GetResolvedKeyFrame(i).InterpolateValue(baseValue, keyFrameProgress);
			}
			if (this.IsCumulative)
			{
				double num4 = (double)(animationClock.CurrentIteration - 1).Value;
				if (num4 > 0.0)
				{
					quaternion = AnimatedTypeHelpers.AddQuaternion(quaternion, AnimatedTypeHelpers.ScaleQuaternion(this.GetResolvedKeyFrameValue(num2), num4));
				}
			}
			if (this.IsAdditive)
			{
				return AnimatedTypeHelpers.AddQuaternion(defaultOriginValue, quaternion);
			}
			return quaternion;
		}

		/// <summary>Forneça uma <see cref="T:System.Windows.Duration" /> natural personalizada quando a propriedade <see cref="T:System.Windows.Duration" /> for definida como <see cref="P:System.Windows.Duration.Automatic" />.</summary>
		/// <param name="clock">O <see cref="T:System.Windows.Media.Animation.Clock" /> cuja duração natural é desejada.</param>
		/// <returns>Se o último quadro-chave dessa animação for um <see cref="T:System.Windows.Media.Animation.KeyTime" />, esse valor será usado como a <see cref="P:System.Windows.Media.Animation.Clock.NaturalDuration" />; caso contrário, ela será de um segundo.</returns>
		// Token: 0x06003C9F RID: 15519 RVA: 0x000EE614 File Offset: 0x000EDA14
		protected sealed override Duration GetNaturalDurationCore(Clock clock)
		{
			return new Duration(this.LargestTimeSpanKeyTime);
		}

		/// <summary>Obtém ou define uma coleção ordenada P:System.Windows.Media.Animation.IKeyFrameAnimation.KeyFrames associada a esta sequência de animação.</summary>
		/// <returns>Um <see cref="T:System.Collections.IList" /> de <see cref="P:System.Windows.Media.Animation.IKeyFrameAnimation.KeyFrames" />.</returns>
		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06003CA0 RID: 15520 RVA: 0x000EE62C File Offset: 0x000EDA2C
		// (set) Token: 0x06003CA1 RID: 15521 RVA: 0x000EE640 File Offset: 0x000EDA40
		IList IKeyFrameAnimation.KeyFrames
		{
			get
			{
				return this.KeyFrames;
			}
			set
			{
				this.KeyFrames = (QuaternionKeyFrameCollection)value;
			}
		}

		/// <summary>Obtém ou define a coleção de objetos <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> que definem a animação.</summary>
		/// <returns>A coleção de <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> objetos que definem a animação. O valor padrão é <see cref="P:System.Windows.Media.Animation.QuaternionKeyFrameCollection.Empty" />.</returns>
		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06003CA2 RID: 15522 RVA: 0x000EE65C File Offset: 0x000EDA5C
		// (set) Token: 0x06003CA3 RID: 15523 RVA: 0x000EE6B8 File Offset: 0x000EDAB8
		public QuaternionKeyFrameCollection KeyFrames
		{
			get
			{
				base.ReadPreamble();
				if (this._keyFrames == null)
				{
					if (base.IsFrozen)
					{
						this._keyFrames = QuaternionKeyFrameCollection.Empty;
					}
					else
					{
						base.WritePreamble();
						this._keyFrames = new QuaternionKeyFrameCollection();
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

		/// <summary>Retornará true se o valor da propriedade <see cref="P:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames.KeyFrames" /> desta instância do <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" /> precisar ser serializado por valor.</summary>
		/// <returns>
		///   <see langword="true" /> se o valor da propriedade precisar ser serializado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003CA4 RID: 15524 RVA: 0x000EE6FC File Offset: 0x000EDAFC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeKeyFrames()
		{
			base.ReadPreamble();
			return this._keyFrames != null && this._keyFrames.Count > 0;
		}

		/// <summary>Obtém um valor que especifica se o valor de saída da animação é adicionado ao valor base da propriedade que está sendo animada.</summary>
		/// <returns>
		///   <see langword="true" /> Se a animação adicionará o valor de saída para o valor base da propriedade sendo animada, em vez de substituí-la; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06003CA5 RID: 15525 RVA: 0x000EE728 File Offset: 0x000EDB28
		// (set) Token: 0x06003CA6 RID: 15526 RVA: 0x000EE748 File Offset: 0x000EDB48
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
		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06003CA7 RID: 15527 RVA: 0x000EE768 File Offset: 0x000EDB68
		// (set) Token: 0x06003CA8 RID: 15528 RVA: 0x000EE788 File Offset: 0x000EDB88
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

		// Token: 0x06003CA9 RID: 15529 RVA: 0x000EE7A8 File Offset: 0x000EDBA8
		private Quaternion GetResolvedKeyFrameValue(int resolvedKeyFrameIndex)
		{
			return this.GetResolvedKeyFrame(resolvedKeyFrameIndex).Value;
		}

		// Token: 0x06003CAA RID: 15530 RVA: 0x000EE7C4 File Offset: 0x000EDBC4
		private QuaternionKeyFrame GetResolvedKeyFrame(int resolvedKeyFrameIndex)
		{
			return this._keyFrames[this._sortedResolvedKeyFrames[resolvedKeyFrameIndex]._originalKeyFrameIndex];
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06003CAB RID: 15531 RVA: 0x000EE7F0 File Offset: 0x000EDBF0
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

		// Token: 0x06003CAC RID: 15532 RVA: 0x000EE870 File Offset: 0x000EDC70
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
						QuaternionAnimationUsingKeyFrames.KeyTimeBlock keyTimeBlock = default(QuaternionAnimationUsingKeyFrames.KeyTimeBlock);
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
				QuaternionAnimationUsingKeyFrames.KeyTimeBlock keyTimeBlock2 = (QuaternionAnimationUsingKeyFrames.KeyTimeBlock)arrayList[j];
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

		// Token: 0x06003CAD RID: 15533 RVA: 0x000EEB44 File Offset: 0x000EDF44
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
					Quaternion from = this._keyFrames[num - 1].Value;
					do
					{
						Quaternion value = this._keyFrames[num].Value;
						num4 += AnimatedTypeHelpers.GetSegmentLengthQuaternion(from, value);
						list.Add(num4);
						from = value;
						num++;
					}
					while (num < num2 && this._keyFrames[num].KeyTime.Type == KeyTimeType.Paced);
					num4 += AnimatedTypeHelpers.GetSegmentLengthQuaternion(from, this._keyFrames[num].Value);
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

		// Token: 0x04001714 RID: 5908
		private QuaternionKeyFrameCollection _keyFrames;

		// Token: 0x04001715 RID: 5909
		private ResolvedKeyFrameEntry[] _sortedResolvedKeyFrames;

		// Token: 0x04001716 RID: 5910
		private bool _areKeyTimesValid;

		// Token: 0x020008C2 RID: 2242
		private struct KeyTimeBlock
		{
			// Token: 0x0400295E RID: 10590
			public int BeginIndex;

			// Token: 0x0400295F RID: 10591
			public int EndIndex;
		}
	}
}
