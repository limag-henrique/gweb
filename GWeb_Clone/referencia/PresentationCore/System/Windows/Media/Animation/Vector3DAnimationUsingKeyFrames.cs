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
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Windows.Media.Media3D.Vector3D" /> junto com um conjunto de <see cref="P:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames.KeyFrames" />.</summary>
	// Token: 0x02000563 RID: 1379
	[ContentProperty("KeyFrames")]
	public class Vector3DAnimationUsingKeyFrames : Vector3DAnimationBase, IKeyFrameAnimation, IAddChild
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" />.</summary>
		// Token: 0x06003FC6 RID: 16326 RVA: 0x000FA1DC File Offset: 0x000F95DC
		public Vector3DAnimationUsingKeyFrames()
		{
			this._areKeyTimesValid = true;
		}

		/// <summary>Retorna um clone modificável desta instância.</summary>
		/// <returns>Um clone modificável dessa instância. O clone retornado é efetivamente uma cópia em profundidade do objeto atual, embora algumas cópias possam ser adiadas pelo período necessário para melhorar o desempenho. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da cópia é <see langword="false" />.</returns>
		// Token: 0x06003FC7 RID: 16327 RVA: 0x000FA1F8 File Offset: 0x000F95F8
		public new Vector3DAnimationUsingKeyFrames Clone()
		{
			return (Vector3DAnimationUsingKeyFrames)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" />, fazendo cópias em profundidade dos valores do objeto atual.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será false mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> de origem for true.</returns>
		// Token: 0x06003FC8 RID: 16328 RVA: 0x000FA210 File Offset: 0x000F9610
		public new Vector3DAnimationUsingKeyFrames CloneCurrentValue()
		{
			return (Vector3DAnimationUsingKeyFrames)base.CloneCurrentValue();
		}

		/// <summary>Faz com que esta instância do objeto <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" /> não modificável ou determina se ela pode se tornar não modificável.</summary>
		/// <param name="isChecking">
		///   <see langword="true" /> a ser verificado se esta instância puder ser congelada; <see langword="false" /> para congelar esta instância.</param>
		/// <returns>Se <paramref name="isChecking" /> é true, este método retorna <see langword="true" /> caso esta instância possa se tornar somente leitura ou então <see langword="false" /> se ela não pode se tornar somente leitura.  
		/// Se <paramref name="isChecking" /> for false, esse método retornará <see langword="true" /> se essa instância agora for somente leitura ou <see langword="false" />, se ela não puder se tornar somente leitura, com o efeito colateral de ter começado a alterar o status de congelamento desse objeto.</returns>
		// Token: 0x06003FC9 RID: 16329 RVA: 0x000FA228 File Offset: 0x000F9628
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

		/// <summary>Chamado quando o objeto <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" /> atual é modificado.</summary>
		// Token: 0x06003FCA RID: 16330 RVA: 0x000FA260 File Offset: 0x000F9660
		protected override void OnChanged()
		{
			this._areKeyTimesValid = false;
			base.OnChanged();
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" />.</returns>
		// Token: 0x06003FCB RID: 16331 RVA: 0x000FA27C File Offset: 0x000F967C
		protected override Freezable CreateInstanceCore()
		{
			return new Vector3DAnimationUsingKeyFrames();
		}

		/// <summary>Faz com que os <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" /> especificados sejam clones modificáveis (cópia em profundidade) do objeto atual, usando valores de propriedade base (não animados e não associados a dados).</summary>
		/// <param name="sourceFreezable">Destino para o clone modificável do objeto atual.</param>
		// Token: 0x06003FCC RID: 16332 RVA: 0x000FA290 File Offset: 0x000F9690
		protected override void CloneCore(Freezable sourceFreezable)
		{
			Vector3DAnimationUsingKeyFrames sourceAnimation = (Vector3DAnimationUsingKeyFrames)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(sourceAnimation, false);
		}

		/// <summary>Faz com que os <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" /> especificados sejam clones modificáveis (cópia em profundidade) do objeto atual usando os valores de propriedade atuais.</summary>
		/// <param name="sourceFreezable">Os <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" /> a serem clonados.</param>
		// Token: 0x06003FCD RID: 16333 RVA: 0x000FA2B4 File Offset: 0x000F96B4
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			Vector3DAnimationUsingKeyFrames sourceAnimation = (Vector3DAnimationUsingKeyFrames)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(sourceAnimation, true);
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" /> especificado.</summary>
		/// <param name="source">O objeto <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" /> a ser clonado.</param>
		// Token: 0x06003FCE RID: 16334 RVA: 0x000FA2D8 File Offset: 0x000F96D8
		protected override void GetAsFrozenCore(Freezable source)
		{
			Vector3DAnimationUsingKeyFrames sourceAnimation = (Vector3DAnimationUsingKeyFrames)source;
			base.GetAsFrozenCore(source);
			this.CopyCommon(sourceAnimation, false);
		}

		/// <summary>Torna essa instância uma cópia do <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" /> especificado.</summary>
		/// <param name="source">O <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" /> a ser copiado e congelado</param>
		// Token: 0x06003FCF RID: 16335 RVA: 0x000FA2FC File Offset: 0x000F96FC
		protected override void GetCurrentValueAsFrozenCore(Freezable source)
		{
			Vector3DAnimationUsingKeyFrames sourceAnimation = (Vector3DAnimationUsingKeyFrames)source;
			base.GetCurrentValueAsFrozenCore(source);
			this.CopyCommon(sourceAnimation, true);
		}

		// Token: 0x06003FD0 RID: 16336 RVA: 0x000FA320 File Offset: 0x000F9720
		private void CopyCommon(Vector3DAnimationUsingKeyFrames sourceAnimation, bool isCurrentValueClone)
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
					this._keyFrames = (Vector3DKeyFrameCollection)sourceAnimation._keyFrames.CloneCurrentValue();
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
		// Token: 0x06003FD1 RID: 16337 RVA: 0x000FA3A0 File Offset: 0x000F97A0
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

		/// <summary>Adiciona um <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> filho a este <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" />.</summary>
		/// <param name="child">O objeto a ser adicionado como filho deste <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" />.</param>
		/// <exception cref="T:System.ArgumentException">O parâmetro <paramref name="child" /> não é um <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" />.</exception>
		// Token: 0x06003FD2 RID: 16338 RVA: 0x000FA3D0 File Offset: 0x000F97D0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void AddChild(object child)
		{
			Vector3DKeyFrame vector3DKeyFrame = child as Vector3DKeyFrame;
			if (vector3DKeyFrame != null)
			{
				this.KeyFrames.Add(vector3DKeyFrame);
				return;
			}
			throw new ArgumentException(SR.Get("Animation_ChildMustBeKeyFrame"), "child");
		}

		/// <summary>Adiciona o conteúdo do texto de um nó ao objeto.</summary>
		/// <param name="childText">O texto a ser adicionado ao objeto.</param>
		// Token: 0x06003FD3 RID: 16339 RVA: 0x000FA40C File Offset: 0x000F980C
		void IAddChild.AddText(string childText)
		{
			if (childText == null)
			{
				throw new ArgumentNullException("childText");
			}
			this.AddText(childText);
		}

		/// <summary>Adiciona uma cadeia de caracteres de texto como um filho deste <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" />.</summary>
		/// <param name="childText">O texto adicionado ao <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" />.</param>
		/// <exception cref="T:System.InvalidOperationException">Um <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" /> não aceita texto como um filho, portanto, esse método gerará esta exceção, a menos que uma classe derivada tenha substituído esse comportamento, o que permite que o texto seja adicionado.</exception>
		// Token: 0x06003FD4 RID: 16340 RVA: 0x000FA430 File Offset: 0x000F9830
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void AddText(string childText)
		{
			throw new InvalidOperationException(SR.Get("Animation_NoTextChildren"));
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado por esta instância do <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação de host.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela instância atual.</returns>
		// Token: 0x06003FD5 RID: 16341 RVA: 0x000FA44C File Offset: 0x000F984C
		protected sealed override Vector3D GetCurrentValueCore(Vector3D defaultOriginValue, Vector3D defaultDestinationValue, AnimationClock animationClock)
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
			Vector3D vector3D;
			if (i == num)
			{
				vector3D = this.GetResolvedKeyFrameValue(num2);
			}
			else if (value == this._sortedResolvedKeyFrames[i]._resolvedKeyTime)
			{
				vector3D = this.GetResolvedKeyFrameValue(i);
			}
			else
			{
				Vector3D baseValue;
				double keyFrameProgress;
				if (i == 0)
				{
					if (this.IsAdditive)
					{
						baseValue = AnimatedTypeHelpers.GetZeroValueVector3D(defaultOriginValue);
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
				vector3D = this.GetResolvedKeyFrame(i).InterpolateValue(baseValue, keyFrameProgress);
			}
			if (this.IsCumulative)
			{
				double num4 = (double)(animationClock.CurrentIteration - 1).Value;
				if (num4 > 0.0)
				{
					vector3D = AnimatedTypeHelpers.AddVector3D(vector3D, AnimatedTypeHelpers.ScaleVector3D(this.GetResolvedKeyFrameValue(num2), num4));
				}
			}
			if (this.IsAdditive)
			{
				return AnimatedTypeHelpers.AddVector3D(defaultOriginValue, vector3D);
			}
			return vector3D;
		}

		/// <summary>Forneça uma <see cref="T:System.Windows.Duration" /> natural personalizada quando a propriedade <see cref="T:System.Windows.Duration" /> for definida como <see cref="P:System.Windows.Duration.Automatic" />.</summary>
		/// <param name="clock">O <see cref="T:System.Windows.Media.Animation.Clock" /> cuja duração natural é desejada.</param>
		/// <returns>Se o último quadro-chave dessa animação for um <see cref="T:System.Windows.Media.Animation.KeyTime" />, esse valor será usado como a <see cref="P:System.Windows.Media.Animation.Clock.NaturalDuration" />; caso contrário, ela será de um segundo.</returns>
		// Token: 0x06003FD6 RID: 16342 RVA: 0x000FA640 File Offset: 0x000F9A40
		protected sealed override Duration GetNaturalDurationCore(Clock clock)
		{
			return new Duration(this.LargestTimeSpanKeyTime);
		}

		/// <summary>Obtém ou define uma coleção ordenada P:System.Windows.Media.Animation.IKeyFrameAnimation.KeyFrames associada a esta sequência de animação.</summary>
		/// <returns>Um <see cref="T:System.Collections.IList" /> de <see cref="P:System.Windows.Media.Animation.IKeyFrameAnimation.KeyFrames" />.</returns>
		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06003FD7 RID: 16343 RVA: 0x000FA658 File Offset: 0x000F9A58
		// (set) Token: 0x06003FD8 RID: 16344 RVA: 0x000FA66C File Offset: 0x000F9A6C
		IList IKeyFrameAnimation.KeyFrames
		{
			get
			{
				return this.KeyFrames;
			}
			set
			{
				this.KeyFrames = (Vector3DKeyFrameCollection)value;
			}
		}

		/// <summary>Obtém ou define a coleção de objetos <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> que definem a animação.</summary>
		/// <returns>A coleção de <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> objetos que definem a animação. O valor padrão é <see cref="P:System.Windows.Media.Animation.Vector3DKeyFrameCollection.Empty" />.</returns>
		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06003FD9 RID: 16345 RVA: 0x000FA688 File Offset: 0x000F9A88
		// (set) Token: 0x06003FDA RID: 16346 RVA: 0x000FA6E4 File Offset: 0x000F9AE4
		public Vector3DKeyFrameCollection KeyFrames
		{
			get
			{
				base.ReadPreamble();
				if (this._keyFrames == null)
				{
					if (base.IsFrozen)
					{
						this._keyFrames = Vector3DKeyFrameCollection.Empty;
					}
					else
					{
						base.WritePreamble();
						this._keyFrames = new Vector3DKeyFrameCollection();
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

		/// <summary>Retornará true se o valor da propriedade <see cref="P:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames.KeyFrames" /> desta instância do <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" /> precisar ser serializado por valor.</summary>
		/// <returns>
		///   <see langword="true" /> se o valor da propriedade precisar ser serializado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003FDB RID: 16347 RVA: 0x000FA728 File Offset: 0x000F9B28
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeKeyFrames()
		{
			base.ReadPreamble();
			return this._keyFrames != null && this._keyFrames.Count > 0;
		}

		/// <summary>Obtém um valor que especifica se o valor de saída da animação é adicionado ao valor base da propriedade que está sendo animada.</summary>
		/// <returns>
		///   <see langword="true" /> Se a animação adicionará o valor de saída para o valor base da propriedade sendo animada, em vez de substituí-la; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06003FDC RID: 16348 RVA: 0x000FA754 File Offset: 0x000F9B54
		// (set) Token: 0x06003FDD RID: 16349 RVA: 0x000FA774 File Offset: 0x000F9B74
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
		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06003FDE RID: 16350 RVA: 0x000FA794 File Offset: 0x000F9B94
		// (set) Token: 0x06003FDF RID: 16351 RVA: 0x000FA7B4 File Offset: 0x000F9BB4
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

		// Token: 0x06003FE0 RID: 16352 RVA: 0x000FA7D4 File Offset: 0x000F9BD4
		private Vector3D GetResolvedKeyFrameValue(int resolvedKeyFrameIndex)
		{
			return this.GetResolvedKeyFrame(resolvedKeyFrameIndex).Value;
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x000FA7F0 File Offset: 0x000F9BF0
		private Vector3DKeyFrame GetResolvedKeyFrame(int resolvedKeyFrameIndex)
		{
			return this._keyFrames[this._sortedResolvedKeyFrames[resolvedKeyFrameIndex]._originalKeyFrameIndex];
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06003FE2 RID: 16354 RVA: 0x000FA81C File Offset: 0x000F9C1C
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

		// Token: 0x06003FE3 RID: 16355 RVA: 0x000FA89C File Offset: 0x000F9C9C
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
						Vector3DAnimationUsingKeyFrames.KeyTimeBlock keyTimeBlock = default(Vector3DAnimationUsingKeyFrames.KeyTimeBlock);
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
				Vector3DAnimationUsingKeyFrames.KeyTimeBlock keyTimeBlock2 = (Vector3DAnimationUsingKeyFrames.KeyTimeBlock)arrayList[j];
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

		// Token: 0x06003FE4 RID: 16356 RVA: 0x000FAB70 File Offset: 0x000F9F70
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
					Vector3D from = this._keyFrames[num - 1].Value;
					do
					{
						Vector3D value = this._keyFrames[num].Value;
						num4 += AnimatedTypeHelpers.GetSegmentLengthVector3D(from, value);
						list.Add(num4);
						from = value;
						num++;
					}
					while (num < num2 && this._keyFrames[num].KeyTime.Type == KeyTimeType.Paced);
					num4 += AnimatedTypeHelpers.GetSegmentLengthVector3D(from, this._keyFrames[num].Value);
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

		// Token: 0x0400177D RID: 6013
		private Vector3DKeyFrameCollection _keyFrames;

		// Token: 0x0400177E RID: 6014
		private ResolvedKeyFrameEntry[] _sortedResolvedKeyFrames;

		// Token: 0x0400177F RID: 6015
		private bool _areKeyTimesValid;

		// Token: 0x020008C9 RID: 2249
		private struct KeyTimeBlock
		{
			// Token: 0x0400296E RID: 10606
			public int BeginIndex;

			// Token: 0x0400296F RID: 10607
			public int EndIndex;
		}
	}
}
