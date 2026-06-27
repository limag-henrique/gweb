using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Markup;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Single" /> junto com um conjunto de <see cref="P:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames.KeyFrames" />.</summary>
	// Token: 0x02000542 RID: 1346
	[ContentProperty("KeyFrames")]
	public class SingleAnimationUsingKeyFrames : SingleAnimationBase, IKeyFrameAnimation, IAddChild
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" />.</summary>
		// Token: 0x06003DB4 RID: 15796 RVA: 0x000F2E78 File Offset: 0x000F2278
		public SingleAnimationUsingKeyFrames()
		{
			this._areKeyTimesValid = true;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003DB5 RID: 15797 RVA: 0x000F2E94 File Offset: 0x000F2294
		public new SingleAnimationUsingKeyFrames Clone()
		{
			return (SingleAnimationUsingKeyFrames)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06003DB6 RID: 15798 RVA: 0x000F2EAC File Offset: 0x000F22AC
		public new SingleAnimationUsingKeyFrames CloneCurrentValue()
		{
			return (SingleAnimationUsingKeyFrames)base.CloneCurrentValue();
		}

		/// <summary>Faz com que esta instância do objeto <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" /> não modificável ou determina se ela pode se tornar não modificável.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> a ser verificado se esta instância puder ser congelada; <see langword="false" /> para congelar esta instância.</param>
		/// <returns>Se <paramref name="isChecking" /> é <see langword="true" />, este método retorna <see langword="true" /> se esta instância pode se tornar somente leitura ou <see langword="false" /> se ela não pode se tornar somente leitura. Se <paramref name="isChecking" /> é <see langword="false" />, este método retorna <see langword="true" /> se esta instância agora é somente leitura ou <see langword="false" /> se ela não pode tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x06003DB7 RID: 15799 RVA: 0x000F2EC4 File Offset: 0x000F22C4
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

		/// <summary>Chamado quando o objeto <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" /> atual é modificado.</summary>
		// Token: 0x06003DB8 RID: 15800 RVA: 0x000F2EFC File Offset: 0x000F22FC
		protected override void OnChanged()
		{
			this._areKeyTimesValid = false;
			base.OnChanged();
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" />.</returns>
		// Token: 0x06003DB9 RID: 15801 RVA: 0x000F2F18 File Offset: 0x000F2318
		protected override Freezable CreateInstanceCore()
		{
			return new SingleAnimationUsingKeyFrames();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" /> a ser clonado.</param>
		// Token: 0x06003DBA RID: 15802 RVA: 0x000F2F2C File Offset: 0x000F232C
		protected override void CloneCore(Freezable sourceFreezable)
		{
			SingleAnimationUsingKeyFrames sourceAnimation = (SingleAnimationUsingKeyFrames)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(sourceAnimation, false);
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" /> a ser clonado.</param>
		// Token: 0x06003DBB RID: 15803 RVA: 0x000F2F50 File Offset: 0x000F2350
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			SingleAnimationUsingKeyFrames sourceAnimation = (SingleAnimationUsingKeyFrames)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(sourceAnimation, true);
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" /> especificado.</summary>
		/// <param name="source">O objeto <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" /> a ser clonado.</param>
		// Token: 0x06003DBC RID: 15804 RVA: 0x000F2F74 File Offset: 0x000F2374
		protected override void GetAsFrozenCore(Freezable source)
		{
			SingleAnimationUsingKeyFrames sourceAnimation = (SingleAnimationUsingKeyFrames)source;
			base.GetAsFrozenCore(source);
			this.CopyCommon(sourceAnimation, false);
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="source">O <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" /> a ser copiado e congelado.</param>
		// Token: 0x06003DBD RID: 15805 RVA: 0x000F2F98 File Offset: 0x000F2398
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			SingleAnimationUsingKeyFrames sourceAnimation = (SingleAnimationUsingKeyFrames)source;
			base.GetCurrentValueAsFrozenCore(source);
			this.CopyCommon(sourceAnimation, true);
		}

		// Token: 0x06003DBE RID: 15806 RVA: 0x000F2FBC File Offset: 0x000F23BC
		private void CopyCommon(SingleAnimationUsingKeyFrames sourceAnimation, bool isCurrentValueClone)
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
					this._keyFrames = (SingleKeyFrameCollection)sourceAnimation._keyFrames.CloneCurrentValue();
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
		// Token: 0x06003DBF RID: 15807 RVA: 0x000F303C File Offset: 0x000F243C
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

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> filho a este <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" />.</summary>
		/// <param name="child">O objeto a ser adicionado como filho deste <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" />.</param>
		/// <exception cref="T:System.ArgumentException">O parâmetro <paramref name="child" /> não é um <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" />.</exception>
		// Token: 0x06003DC0 RID: 15808 RVA: 0x000F306C File Offset: 0x000F246C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void AddChild(object child)
		{
			SingleKeyFrame singleKeyFrame = child as SingleKeyFrame;
			if (singleKeyFrame != null)
			{
				this.KeyFrames.Add(singleKeyFrame);
				return;
			}
			throw new ArgumentException(SR.Get("Animation_ChildMustBeKeyFrame"), "child");
		}

		/// <summary>Adiciona o conteúdo do texto de um nó ao objeto.</summary>
		/// <param name="childText">O texto a ser adicionado ao objeto.</param>
		// Token: 0x06003DC1 RID: 15809 RVA: 0x000F30A8 File Offset: 0x000F24A8
		void IAddChild.AddText(string childText)
		{
			if (childText == null)
			{
				throw new ArgumentNullException("childText");
			}
			this.AddText(childText);
		}

		/// <summary>Adiciona uma cadeia de caracteres de texto como um filho deste <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" />.</summary>
		/// <param name="childText">O texto adicionado ao <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" />.</param>
		/// <exception cref="T:System.InvalidOperationException">Uma classe derivada não substituiu o comportamento padrão, que não permite que texto seja adicionado.</exception>
		// Token: 0x06003DC2 RID: 15810 RVA: 0x000F30CC File Offset: 0x000F24CC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void AddText(string childText)
		{
			throw new InvalidOperationException(SR.Get("Animation_NoTextChildren"));
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado por esta instância do <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação de host.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela instância atual.</returns>
		// Token: 0x06003DC3 RID: 15811 RVA: 0x000F30E8 File Offset: 0x000F24E8
		protected sealed override float GetCurrentValueCore(float defaultOriginValue, float defaultDestinationValue, AnimationClock animationClock)
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
			float num3;
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
				float baseValue;
				double keyFrameProgress;
				if (i == 0)
				{
					if (this.IsAdditive)
					{
						baseValue = AnimatedTypeHelpers.GetZeroValueSingle(defaultOriginValue);
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
					num3 = AnimatedTypeHelpers.AddSingle(num3, AnimatedTypeHelpers.ScaleSingle(this.GetResolvedKeyFrameValue(num2), num5));
				}
			}
			if (this.IsAdditive)
			{
				return AnimatedTypeHelpers.AddSingle(defaultOriginValue, num3);
			}
			return num3;
		}

		/// <summary>Forneça uma <see cref="T:System.Windows.Duration" /> natural personalizada quando a propriedade <see cref="T:System.Windows.Duration" /> for definida como <see cref="P:System.Windows.Duration.Automatic" />.</summary>
		/// <param name="clock">O <see cref="T:System.Windows.Media.Animation.Clock" /> cuja duração natural é desejada.</param>
		/// <returns>Se o último quadro-chave dessa animação for um <see cref="T:System.Windows.Media.Animation.KeyTime" />, esse valor será usado como a <see cref="P:System.Windows.Media.Animation.Clock.NaturalDuration" />; caso contrário, ela será de um segundo.</returns>
		// Token: 0x06003DC4 RID: 15812 RVA: 0x000F32DC File Offset: 0x000F26DC
		protected sealed override Duration GetNaturalDurationCore(Clock clock)
		{
			return new Duration(this.LargestTimeSpanKeyTime);
		}

		/// <summary>Obtém ou define uma coleção ordenada P:System.Windows.Media.Animation.IKeyFrameAnimation.KeyFrames associada a esta sequência de animação.</summary>
		/// <returns>Um <see cref="T:System.Collections.IList" /> de <see cref="P:System.Windows.Media.Animation.IKeyFrameAnimation.KeyFrames" />.</returns>
		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06003DC5 RID: 15813 RVA: 0x000F32F4 File Offset: 0x000F26F4
		// (set) Token: 0x06003DC6 RID: 15814 RVA: 0x000F3308 File Offset: 0x000F2708
		IList IKeyFrameAnimation.KeyFrames
		{
			get
			{
				return this.KeyFrames;
			}
			set
			{
				this.KeyFrames = (SingleKeyFrameCollection)value;
			}
		}

		/// <summary>Obtém ou define a coleção de objetos <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> que definem a animação.</summary>
		/// <returns>A coleção de <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> objetos que definem a animação. O valor padrão é <see cref="P:System.Windows.Media.Animation.SingleKeyFrameCollection.Empty" />.</returns>
		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06003DC7 RID: 15815 RVA: 0x000F3324 File Offset: 0x000F2724
		// (set) Token: 0x06003DC8 RID: 15816 RVA: 0x000F3380 File Offset: 0x000F2780
		public SingleKeyFrameCollection KeyFrames
		{
			get
			{
				base.ReadPreamble();
				if (this._keyFrames == null)
				{
					if (base.IsFrozen)
					{
						this._keyFrames = SingleKeyFrameCollection.Empty;
					}
					else
					{
						base.WritePreamble();
						this._keyFrames = new SingleKeyFrameCollection();
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

		/// <summary>Retornará true se o valor da propriedade <see cref="P:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames.KeyFrames" /> desta instância do <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" /> precisar ser serializado por valor.</summary>
		/// <returns>
		///   <see langword="true" /> se o valor da propriedade precisar ser serializado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003DC9 RID: 15817 RVA: 0x000F33C4 File Offset: 0x000F27C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeKeyFrames()
		{
			base.ReadPreamble();
			return this._keyFrames != null && this._keyFrames.Count > 0;
		}

		/// <summary>Obtém um valor que especifica se o valor de saída da animação é adicionado ao valor base da propriedade que está sendo animada.</summary>
		/// <returns>
		///   <see langword="true" /> Se a animação adicionará o valor de saída para o valor base da propriedade sendo animada, em vez de substituí-la; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06003DCA RID: 15818 RVA: 0x000F33F0 File Offset: 0x000F27F0
		// (set) Token: 0x06003DCB RID: 15819 RVA: 0x000F3410 File Offset: 0x000F2810
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
		///   <see langword="true" /> Se a animação acumula seus valores ao seu <see cref="P:System.Windows.Media.Animation.Timeline.RepeatBehavior" /> propriedade faz com que ele Repita sua duração simples; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06003DCC RID: 15820 RVA: 0x000F3430 File Offset: 0x000F2830
		// (set) Token: 0x06003DCD RID: 15821 RVA: 0x000F3450 File Offset: 0x000F2850
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

		// Token: 0x06003DCE RID: 15822 RVA: 0x000F3470 File Offset: 0x000F2870
		private float GetResolvedKeyFrameValue(int resolvedKeyFrameIndex)
		{
			return this.GetResolvedKeyFrame(resolvedKeyFrameIndex).Value;
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x000F348C File Offset: 0x000F288C
		private SingleKeyFrame GetResolvedKeyFrame(int resolvedKeyFrameIndex)
		{
			return this._keyFrames[this._sortedResolvedKeyFrames[resolvedKeyFrameIndex]._originalKeyFrameIndex];
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06003DD0 RID: 15824 RVA: 0x000F34B8 File Offset: 0x000F28B8
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

		// Token: 0x06003DD1 RID: 15825 RVA: 0x000F3538 File Offset: 0x000F2938
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
						SingleAnimationUsingKeyFrames.KeyTimeBlock keyTimeBlock = default(SingleAnimationUsingKeyFrames.KeyTimeBlock);
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
				SingleAnimationUsingKeyFrames.KeyTimeBlock keyTimeBlock2 = (SingleAnimationUsingKeyFrames.KeyTimeBlock)arrayList[j];
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

		// Token: 0x06003DD2 RID: 15826 RVA: 0x000F380C File Offset: 0x000F2C0C
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
					float from = this._keyFrames[num - 1].Value;
					do
					{
						float value = this._keyFrames[num].Value;
						num4 += AnimatedTypeHelpers.GetSegmentLengthSingle(from, value);
						list.Add(num4);
						from = value;
						num++;
					}
					while (num < num2 && this._keyFrames[num].KeyTime.Type == KeyTimeType.Paced);
					num4 += AnimatedTypeHelpers.GetSegmentLengthSingle(from, this._keyFrames[num].Value);
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

		// Token: 0x04001739 RID: 5945
		private SingleKeyFrameCollection _keyFrames;

		// Token: 0x0400173A RID: 5946
		private ResolvedKeyFrameEntry[] _sortedResolvedKeyFrames;

		// Token: 0x0400173B RID: 5947
		private bool _areKeyTimesValid;

		// Token: 0x020008C5 RID: 2245
		private struct KeyTimeBlock
		{
			// Token: 0x04002964 RID: 10596
			public int BeginIndex;

			// Token: 0x04002965 RID: 10597
			public int EndIndex;
		}
	}
}
