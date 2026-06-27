using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Markup;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Int32" /> junto com um conjunto de <see cref="P:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames.KeyFrames" />.</summary>
	// Token: 0x020004F3 RID: 1267
	[ContentProperty("KeyFrames")]
	public class Int32AnimationUsingKeyFrames : Int32AnimationBase, IKeyFrameAnimation, IAddChild
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" />.</summary>
		// Token: 0x06003928 RID: 14632 RVA: 0x000E299C File Offset: 0x000E1D9C
		public Int32AnimationUsingKeyFrames()
		{
			this._areKeyTimesValid = true;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003929 RID: 14633 RVA: 0x000E29B8 File Offset: 0x000E1DB8
		public new Int32AnimationUsingKeyFrames Clone()
		{
			return (Int32AnimationUsingKeyFrames)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x0600392A RID: 14634 RVA: 0x000E29D0 File Offset: 0x000E1DD0
		public new Int32AnimationUsingKeyFrames CloneCurrentValue()
		{
			return (Int32AnimationUsingKeyFrames)base.CloneCurrentValue();
		}

		/// <summary>Faz com que esta instância do objeto <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" /> não modificável ou determina se ela pode se tornar não modificável.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> se este método deve simplesmente determinar se esta instância pode ser congelada. <see langword="false" /> se esta instância, na verdade, deve congelar a si mesma quando este método é chamado.</param>
		/// <returns>Se <paramref name="isChecking" /> é true, este método retorna <see langword="true" /> caso esta instância possa se tornar somente leitura ou então <see langword="false" /> se ela não pode se tornar somente leitura. Se <paramref name="isChecking" /> é false, este método retorna <see langword="true" /> se esta instância agora é somente leitura ou <see langword="false" />, se ela não pode tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x0600392B RID: 14635 RVA: 0x000E29E8 File Offset: 0x000E1DE8
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

		/// <summary>Chamado quando o objeto <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" /> atual é modificado.</summary>
		// Token: 0x0600392C RID: 14636 RVA: 0x000E2A20 File Offset: 0x000E1E20
		protected override void OnChanged()
		{
			this._areKeyTimesValid = false;
			base.OnChanged();
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" />.</returns>
		// Token: 0x0600392D RID: 14637 RVA: 0x000E2A3C File Offset: 0x000E1E3C
		protected override Freezable CreateInstanceCore()
		{
			return new Int32AnimationUsingKeyFrames();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" /> a ser clonado.</param>
		// Token: 0x0600392E RID: 14638 RVA: 0x000E2A50 File Offset: 0x000E1E50
		protected override void CloneCore(Freezable sourceFreezable)
		{
			Int32AnimationUsingKeyFrames sourceAnimation = (Int32AnimationUsingKeyFrames)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(sourceAnimation, false);
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" /> a ser clonado.</param>
		// Token: 0x0600392F RID: 14639 RVA: 0x000E2A74 File Offset: 0x000E1E74
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			Int32AnimationUsingKeyFrames sourceAnimation = (Int32AnimationUsingKeyFrames)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(sourceAnimation, true);
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" /> especificado.</summary>
		/// <param name="source">O objeto <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" /> a ser clonado.</param>
		// Token: 0x06003930 RID: 14640 RVA: 0x000E2A98 File Offset: 0x000E1E98
		protected override void GetAsFrozenCore(Freezable source)
		{
			Int32AnimationUsingKeyFrames sourceAnimation = (Int32AnimationUsingKeyFrames)source;
			base.GetAsFrozenCore(source);
			this.CopyCommon(sourceAnimation, false);
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="source">O <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" /> a ser copiado e congelado.</param>
		// Token: 0x06003931 RID: 14641 RVA: 0x000E2ABC File Offset: 0x000E1EBC
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			Int32AnimationUsingKeyFrames sourceAnimation = (Int32AnimationUsingKeyFrames)source;
			base.GetCurrentValueAsFrozenCore(source);
			this.CopyCommon(sourceAnimation, true);
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000E2AE0 File Offset: 0x000E1EE0
		private void CopyCommon(Int32AnimationUsingKeyFrames sourceAnimation, bool isCurrentValueClone)
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
					this._keyFrames = (Int32KeyFrameCollection)sourceAnimation._keyFrames.CloneCurrentValue();
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
		// Token: 0x06003933 RID: 14643 RVA: 0x000E2B60 File Offset: 0x000E1F60
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

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> filho a este <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" />.</summary>
		/// <param name="child">O objeto a ser adicionado como filho deste <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" />.</param>
		/// <exception cref="T:System.ArgumentException">O parâmetro <paramref name="child" /> não é um <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" />.</exception>
		// Token: 0x06003934 RID: 14644 RVA: 0x000E2B90 File Offset: 0x000E1F90
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void AddChild(object child)
		{
			Int32KeyFrame int32KeyFrame = child as Int32KeyFrame;
			if (int32KeyFrame != null)
			{
				this.KeyFrames.Add(int32KeyFrame);
				return;
			}
			throw new ArgumentException(SR.Get("Animation_ChildMustBeKeyFrame"), "child");
		}

		/// <summary>Adiciona o conteúdo do texto de um nó ao objeto.</summary>
		/// <param name="childText">O texto a ser adicionado ao objeto.</param>
		// Token: 0x06003935 RID: 14645 RVA: 0x000E2BCC File Offset: 0x000E1FCC
		void IAddChild.AddText(string childText)
		{
			if (childText == null)
			{
				throw new ArgumentNullException("childText");
			}
			this.AddText(childText);
		}

		/// <summary>Adiciona uma cadeia de caracteres de texto como um filho deste <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" />.</summary>
		/// <param name="childText">O texto adicionado ao <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" />.</param>
		/// <exception cref="T:System.InvalidOperationException">Um <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" /> não aceita texto como um filho, portanto, esse método gerará esta exceção, a menos que uma classe derivada tenha substituído esse comportamento, o que permite que o texto seja adicionado.</exception>
		// Token: 0x06003936 RID: 14646 RVA: 0x000E2BF0 File Offset: 0x000E1FF0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void AddText(string childText)
		{
			throw new InvalidOperationException(SR.Get("Animation_NoTextChildren"));
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado por esta instância do <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação de host.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela instância atual.</returns>
		// Token: 0x06003937 RID: 14647 RVA: 0x000E2C0C File Offset: 0x000E200C
		protected sealed override int GetCurrentValueCore(int defaultOriginValue, int defaultDestinationValue, AnimationClock animationClock)
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
			int num3;
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
				int baseValue;
				double keyFrameProgress;
				if (i == 0)
				{
					if (this.IsAdditive)
					{
						baseValue = AnimatedTypeHelpers.GetZeroValueInt32(defaultOriginValue);
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
					num3 = AnimatedTypeHelpers.AddInt32(num3, AnimatedTypeHelpers.ScaleInt32(this.GetResolvedKeyFrameValue(num2), num5));
				}
			}
			if (this.IsAdditive)
			{
				return AnimatedTypeHelpers.AddInt32(defaultOriginValue, num3);
			}
			return num3;
		}

		/// <summary>Forneça uma <see cref="T:System.Windows.Duration" /> natural personalizada quando a propriedade <see cref="T:System.Windows.Duration" /> for definida como <see cref="P:System.Windows.Duration.Automatic" />.</summary>
		/// <param name="clock">O <see cref="T:System.Windows.Media.Animation.Clock" /> cuja duração natural é desejada.</param>
		/// <returns>Se o último quadro-chave dessa animação for um <see cref="T:System.Windows.Media.Animation.KeyTime" />, esse valor será usado como a <see cref="P:System.Windows.Media.Animation.Clock.NaturalDuration" />; caso contrário, ela será de um segundo.</returns>
		// Token: 0x06003938 RID: 14648 RVA: 0x000E2E00 File Offset: 0x000E2200
		protected sealed override Duration GetNaturalDurationCore(Clock clock)
		{
			return new Duration(this.LargestTimeSpanKeyTime);
		}

		/// <summary>Obtém ou define uma coleção ordenada P:System.Windows.Media.Animation.IKeyFrameAnimation.KeyFrames associada a esta sequência de animação.</summary>
		/// <returns>Um <see cref="T:System.Collections.IList" /> de <see cref="P:System.Windows.Media.Animation.IKeyFrameAnimation.KeyFrames" />.</returns>
		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06003939 RID: 14649 RVA: 0x000E2E18 File Offset: 0x000E2218
		// (set) Token: 0x0600393A RID: 14650 RVA: 0x000E2E2C File Offset: 0x000E222C
		IList IKeyFrameAnimation.KeyFrames
		{
			get
			{
				return this.KeyFrames;
			}
			set
			{
				this.KeyFrames = (Int32KeyFrameCollection)value;
			}
		}

		/// <summary>Obtém ou define a coleção de objetos <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> que definem a animação.</summary>
		/// <returns>A coleção de <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> objetos que definem a animação. O valor padrão é <see cref="P:System.Windows.Media.Animation.Int32KeyFrameCollection.Empty" />.</returns>
		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x0600393B RID: 14651 RVA: 0x000E2E48 File Offset: 0x000E2248
		// (set) Token: 0x0600393C RID: 14652 RVA: 0x000E2EA4 File Offset: 0x000E22A4
		public Int32KeyFrameCollection KeyFrames
		{
			get
			{
				base.ReadPreamble();
				if (this._keyFrames == null)
				{
					if (base.IsFrozen)
					{
						this._keyFrames = Int32KeyFrameCollection.Empty;
					}
					else
					{
						base.WritePreamble();
						this._keyFrames = new Int32KeyFrameCollection();
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

		/// <summary>Retornará true se o valor da propriedade <see cref="P:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames.KeyFrames" /> desta instância do <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" /> precisar ser serializado por valor.</summary>
		/// <returns>
		///   <see langword="true" /> se o valor da propriedade precisar ser serializado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600393D RID: 14653 RVA: 0x000E2EE8 File Offset: 0x000E22E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeKeyFrames()
		{
			base.ReadPreamble();
			return this._keyFrames != null && this._keyFrames.Count > 0;
		}

		/// <summary>Obtém um valor que especifica se o valor de saída da animação é adicionado ao valor base da propriedade que está sendo animada.</summary>
		/// <returns>
		///   <see langword="true" /> Se a animação adicionará o valor de saída para o valor base da propriedade sendo animada, em vez de substituí-la; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x0600393E RID: 14654 RVA: 0x000E2F14 File Offset: 0x000E2314
		// (set) Token: 0x0600393F RID: 14655 RVA: 0x000E2F34 File Offset: 0x000E2334
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
		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06003940 RID: 14656 RVA: 0x000E2F54 File Offset: 0x000E2354
		// (set) Token: 0x06003941 RID: 14657 RVA: 0x000E2F74 File Offset: 0x000E2374
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

		// Token: 0x06003942 RID: 14658 RVA: 0x000E2F94 File Offset: 0x000E2394
		private int GetResolvedKeyFrameValue(int resolvedKeyFrameIndex)
		{
			return this.GetResolvedKeyFrame(resolvedKeyFrameIndex).Value;
		}

		// Token: 0x06003943 RID: 14659 RVA: 0x000E2FB0 File Offset: 0x000E23B0
		private Int32KeyFrame GetResolvedKeyFrame(int resolvedKeyFrameIndex)
		{
			return this._keyFrames[this._sortedResolvedKeyFrames[resolvedKeyFrameIndex]._originalKeyFrameIndex];
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06003944 RID: 14660 RVA: 0x000E2FDC File Offset: 0x000E23DC
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

		// Token: 0x06003945 RID: 14661 RVA: 0x000E305C File Offset: 0x000E245C
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
						Int32AnimationUsingKeyFrames.KeyTimeBlock keyTimeBlock = default(Int32AnimationUsingKeyFrames.KeyTimeBlock);
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
				Int32AnimationUsingKeyFrames.KeyTimeBlock keyTimeBlock2 = (Int32AnimationUsingKeyFrames.KeyTimeBlock)arrayList[j];
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

		// Token: 0x06003946 RID: 14662 RVA: 0x000E3330 File Offset: 0x000E2730
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
					int from = this._keyFrames[num - 1].Value;
					do
					{
						int value = this._keyFrames[num].Value;
						num4 += AnimatedTypeHelpers.GetSegmentLengthInt32(from, value);
						list.Add(num4);
						from = value;
						num++;
					}
					while (num < num2 && this._keyFrames[num].KeyTime.Type == KeyTimeType.Paced);
					num4 += AnimatedTypeHelpers.GetSegmentLengthInt32(from, this._keyFrames[num].Value);
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

		// Token: 0x040016AC RID: 5804
		private Int32KeyFrameCollection _keyFrames;

		// Token: 0x040016AD RID: 5805
		private ResolvedKeyFrameEntry[] _sortedResolvedKeyFrames;

		// Token: 0x040016AE RID: 5806
		private bool _areKeyTimesValid;

		// Token: 0x020008BC RID: 2236
		private struct KeyTimeBlock
		{
			// Token: 0x04002952 RID: 10578
			public int BeginIndex;

			// Token: 0x04002953 RID: 10579
			public int EndIndex;
		}
	}
}
