using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Markup;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Windows.Vector" /> junto com um conjunto de <see cref="P:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames.KeyFrames" />.</summary>
	// Token: 0x02000567 RID: 1383
	[ContentProperty("KeyFrames")]
	public class VectorAnimationUsingKeyFrames : VectorAnimationBase, IKeyFrameAnimation, IAddChild
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" />.</summary>
		// Token: 0x06004024 RID: 16420 RVA: 0x000FBB40 File Offset: 0x000FAF40
		public VectorAnimationUsingKeyFrames()
		{
			this._areKeyTimesValid = true;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06004025 RID: 16421 RVA: 0x000FBB5C File Offset: 0x000FAF5C
		public new VectorAnimationUsingKeyFrames Clone()
		{
			return (VectorAnimationUsingKeyFrames)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06004026 RID: 16422 RVA: 0x000FBB74 File Offset: 0x000FAF74
		public new VectorAnimationUsingKeyFrames CloneCurrentValue()
		{
			return (VectorAnimationUsingKeyFrames)base.CloneCurrentValue();
		}

		/// <summary>Faz com que esta instância do objeto <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" /> não modificável ou determina se ela pode se tornar não modificável.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> a ser verificado se esta instância puder ser congelada; <see langword="false" /> para congelar esta instância.</param>
		/// <returns>Se <paramref name="isChecking" /> é true, este método retorna <see langword="true" /> caso esta instância possa se tornar somente leitura ou então <see langword="false" /> se ela não pode se tornar somente leitura.  
		/// Se <paramref name="isChecking" /> é false, este método retorna <see langword="true" /> se esta instância agora é somente leitura ou <see langword="false" />, se ela não pode tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento deste objeto.</returns>
		// Token: 0x06004027 RID: 16423 RVA: 0x000FBB8C File Offset: 0x000FAF8C
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

		/// <summary>Chamado quando o objeto <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" /> atual é modificado.</summary>
		// Token: 0x06004028 RID: 16424 RVA: 0x000FBBC4 File Offset: 0x000FAFC4
		protected override void OnChanged()
		{
			this._areKeyTimesValid = false;
			base.OnChanged();
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" />.</returns>
		// Token: 0x06004029 RID: 16425 RVA: 0x000FBBE0 File Offset: 0x000FAFE0
		protected override Freezable CreateInstanceCore()
		{
			return new VectorAnimationUsingKeyFrames();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" /> a ser clonado.</param>
		// Token: 0x0600402A RID: 16426 RVA: 0x000FBBF4 File Offset: 0x000FAFF4
		protected override void CloneCore(Freezable sourceFreezable)
		{
			VectorAnimationUsingKeyFrames sourceAnimation = (VectorAnimationUsingKeyFrames)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(sourceAnimation, false);
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" /> a ser clonado.</param>
		// Token: 0x0600402B RID: 16427 RVA: 0x000FBC18 File Offset: 0x000FB018
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			VectorAnimationUsingKeyFrames sourceAnimation = (VectorAnimationUsingKeyFrames)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(sourceAnimation, true);
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" /> especificado.</summary>
		/// <param name="source">O objeto <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" /> a ser clonado.</param>
		// Token: 0x0600402C RID: 16428 RVA: 0x000FBC3C File Offset: 0x000FB03C
		protected override void GetAsFrozenCore(Freezable source)
		{
			VectorAnimationUsingKeyFrames sourceAnimation = (VectorAnimationUsingKeyFrames)source;
			base.GetAsFrozenCore(source);
			this.CopyCommon(sourceAnimation, false);
		}

		/// <summary>Torna essa instância um clone congelado do <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" /> especificado. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="source">O <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" /> a ser copiado e congelado.</param>
		// Token: 0x0600402D RID: 16429 RVA: 0x000FBC60 File Offset: 0x000FB060
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			VectorAnimationUsingKeyFrames sourceAnimation = (VectorAnimationUsingKeyFrames)source;
			base.GetCurrentValueAsFrozenCore(source);
			this.CopyCommon(sourceAnimation, true);
		}

		// Token: 0x0600402E RID: 16430 RVA: 0x000FBC84 File Offset: 0x000FB084
		private void CopyCommon(VectorAnimationUsingKeyFrames sourceAnimation, bool isCurrentValueClone)
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
					this._keyFrames = (VectorKeyFrameCollection)sourceAnimation._keyFrames.CloneCurrentValue();
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
		// Token: 0x0600402F RID: 16431 RVA: 0x000FBD04 File Offset: 0x000FB104
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

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> filho a este <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" />.</summary>
		/// <param name="child">O objeto a ser adicionado como filho deste <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" />.</param>
		/// <exception cref="T:System.ArgumentException">O parâmetro <paramref name="child" /> não é um <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" />.</exception>
		// Token: 0x06004030 RID: 16432 RVA: 0x000FBD34 File Offset: 0x000FB134
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void AddChild(object child)
		{
			VectorKeyFrame vectorKeyFrame = child as VectorKeyFrame;
			if (vectorKeyFrame != null)
			{
				this.KeyFrames.Add(vectorKeyFrame);
				return;
			}
			throw new ArgumentException(SR.Get("Animation_ChildMustBeKeyFrame"), "child");
		}

		/// <summary>Adiciona o conteúdo do texto de um nó ao objeto.</summary>
		/// <param name="childText">O texto a ser adicionado ao objeto.</param>
		// Token: 0x06004031 RID: 16433 RVA: 0x000FBD70 File Offset: 0x000FB170
		void IAddChild.AddText(string childText)
		{
			if (childText == null)
			{
				throw new ArgumentNullException("childText");
			}
			this.AddText(childText);
		}

		/// <summary>Adiciona uma cadeia de caracteres de texto como um filho deste <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" />.</summary>
		/// <param name="childText">O texto adicionado ao <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" />.</param>
		/// <exception cref="T:System.InvalidOperationException">Um <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" /> não aceita texto como um filho, portanto, esse método gerará esta exceção, a menos que uma classe derivada tenha substituído esse comportamento, o que permite que o texto seja adicionado.</exception>
		// Token: 0x06004032 RID: 16434 RVA: 0x000FBD94 File Offset: 0x000FB194
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void AddText(string childText)
		{
			throw new InvalidOperationException(SR.Get("Animation_NoTextChildren"));
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado por esta instância do <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação de host.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela instância atual.</returns>
		// Token: 0x06004033 RID: 16435 RVA: 0x000FBDB0 File Offset: 0x000FB1B0
		protected sealed override Vector GetCurrentValueCore(Vector defaultOriginValue, Vector defaultDestinationValue, AnimationClock animationClock)
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
			Vector vector;
			if (i == num)
			{
				vector = this.GetResolvedKeyFrameValue(num2);
			}
			else if (value == this._sortedResolvedKeyFrames[i]._resolvedKeyTime)
			{
				vector = this.GetResolvedKeyFrameValue(i);
			}
			else
			{
				Vector baseValue;
				double keyFrameProgress;
				if (i == 0)
				{
					if (this.IsAdditive)
					{
						baseValue = AnimatedTypeHelpers.GetZeroValueVector(defaultOriginValue);
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
				vector = this.GetResolvedKeyFrame(i).InterpolateValue(baseValue, keyFrameProgress);
			}
			if (this.IsCumulative)
			{
				double num4 = (double)(animationClock.CurrentIteration - 1).Value;
				if (num4 > 0.0)
				{
					vector = AnimatedTypeHelpers.AddVector(vector, AnimatedTypeHelpers.ScaleVector(this.GetResolvedKeyFrameValue(num2), num4));
				}
			}
			if (this.IsAdditive)
			{
				return AnimatedTypeHelpers.AddVector(defaultOriginValue, vector);
			}
			return vector;
		}

		/// <summary>Forneça uma <see cref="T:System.Windows.Duration" /> natural personalizada quando a propriedade <see cref="T:System.Windows.Duration" /> for definida como <see cref="P:System.Windows.Duration.Automatic" />.</summary>
		/// <param name="clock">O <see cref="T:System.Windows.Media.Animation.Clock" /> cuja duração natural é desejada.</param>
		/// <returns>Se o último quadro-chave dessa animação for um <see cref="T:System.Windows.Media.Animation.KeyTime" />, esse valor será usado como a <see cref="P:System.Windows.Media.Animation.Clock.NaturalDuration" />; caso contrário, ela será de um segundo.</returns>
		// Token: 0x06004034 RID: 16436 RVA: 0x000FBFA4 File Offset: 0x000FB3A4
		protected sealed override Duration GetNaturalDurationCore(Clock clock)
		{
			return new Duration(this.LargestTimeSpanKeyTime);
		}

		/// <summary>Obtém ou define uma coleção ordenada P:System.Windows.Media.Animation.IKeyFrameAnimation.KeyFrames associada a esta sequência de animação.</summary>
		/// <returns>Um <see cref="T:System.Collections.IList" /> de <see cref="P:System.Windows.Media.Animation.IKeyFrameAnimation.KeyFrames" />.</returns>
		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06004035 RID: 16437 RVA: 0x000FBFBC File Offset: 0x000FB3BC
		// (set) Token: 0x06004036 RID: 16438 RVA: 0x000FBFD0 File Offset: 0x000FB3D0
		IList IKeyFrameAnimation.KeyFrames
		{
			get
			{
				return this.KeyFrames;
			}
			set
			{
				this.KeyFrames = (VectorKeyFrameCollection)value;
			}
		}

		/// <summary>Obtém ou define a coleção de objetos <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> que definem a animação.</summary>
		/// <returns>A coleção de <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> objetos que definem a animação. O valor padrão é <see cref="P:System.Windows.Media.Animation.VectorKeyFrameCollection.Empty" />.</returns>
		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06004037 RID: 16439 RVA: 0x000FBFEC File Offset: 0x000FB3EC
		// (set) Token: 0x06004038 RID: 16440 RVA: 0x000FC048 File Offset: 0x000FB448
		public VectorKeyFrameCollection KeyFrames
		{
			get
			{
				base.ReadPreamble();
				if (this._keyFrames == null)
				{
					if (base.IsFrozen)
					{
						this._keyFrames = VectorKeyFrameCollection.Empty;
					}
					else
					{
						base.WritePreamble();
						this._keyFrames = new VectorKeyFrameCollection();
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

		/// <summary>Retornará true se o valor da propriedade <see cref="P:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames.KeyFrames" /> desta instância do <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" /> precisar ser serializado por valor.</summary>
		/// <returns>
		///   <see langword="true" /> se o valor da propriedade precisar ser serializado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06004039 RID: 16441 RVA: 0x000FC08C File Offset: 0x000FB48C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeKeyFrames()
		{
			base.ReadPreamble();
			return this._keyFrames != null && this._keyFrames.Count > 0;
		}

		/// <summary>Obtém um valor que especifica se o valor de saída da animação é adicionado ao valor base da propriedade que está sendo animada.</summary>
		/// <returns>
		///   <see langword="true" /> Se a animação adicionará o valor de saída para o valor base da propriedade sendo animada, em vez de substituí-la; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x0600403A RID: 16442 RVA: 0x000FC0B8 File Offset: 0x000FB4B8
		// (set) Token: 0x0600403B RID: 16443 RVA: 0x000FC0D8 File Offset: 0x000FB4D8
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
		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x0600403C RID: 16444 RVA: 0x000FC0F8 File Offset: 0x000FB4F8
		// (set) Token: 0x0600403D RID: 16445 RVA: 0x000FC118 File Offset: 0x000FB518
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

		// Token: 0x0600403E RID: 16446 RVA: 0x000FC138 File Offset: 0x000FB538
		private Vector GetResolvedKeyFrameValue(int resolvedKeyFrameIndex)
		{
			return this.GetResolvedKeyFrame(resolvedKeyFrameIndex).Value;
		}

		// Token: 0x0600403F RID: 16447 RVA: 0x000FC154 File Offset: 0x000FB554
		private VectorKeyFrame GetResolvedKeyFrame(int resolvedKeyFrameIndex)
		{
			return this._keyFrames[this._sortedResolvedKeyFrames[resolvedKeyFrameIndex]._originalKeyFrameIndex];
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06004040 RID: 16448 RVA: 0x000FC180 File Offset: 0x000FB580
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

		// Token: 0x06004041 RID: 16449 RVA: 0x000FC200 File Offset: 0x000FB600
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
						VectorAnimationUsingKeyFrames.KeyTimeBlock keyTimeBlock = default(VectorAnimationUsingKeyFrames.KeyTimeBlock);
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
				VectorAnimationUsingKeyFrames.KeyTimeBlock keyTimeBlock2 = (VectorAnimationUsingKeyFrames.KeyTimeBlock)arrayList[j];
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

		// Token: 0x06004042 RID: 16450 RVA: 0x000FC4D4 File Offset: 0x000FB8D4
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
					Vector from = this._keyFrames[num - 1].Value;
					do
					{
						Vector value = this._keyFrames[num].Value;
						num4 += AnimatedTypeHelpers.GetSegmentLengthVector(from, value);
						list.Add(num4);
						from = value;
						num++;
					}
					while (num < num2 && this._keyFrames[num].KeyTime.Type == KeyTimeType.Paced);
					num4 += AnimatedTypeHelpers.GetSegmentLengthVector(from, this._keyFrames[num].Value);
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

		// Token: 0x04001789 RID: 6025
		private VectorKeyFrameCollection _keyFrames;

		// Token: 0x0400178A RID: 6026
		private ResolvedKeyFrameEntry[] _sortedResolvedKeyFrames;

		// Token: 0x0400178B RID: 6027
		private bool _areKeyTimesValid;

		// Token: 0x020008CA RID: 2250
		private struct KeyTimeBlock
		{
			// Token: 0x04002970 RID: 10608
			public int BeginIndex;

			// Token: 0x04002971 RID: 10609
			public int EndIndex;
		}
	}
}
