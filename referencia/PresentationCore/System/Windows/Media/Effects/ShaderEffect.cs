using System;
using System.Collections.Generic;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Effects
{
	/// <summary>Fornece um efeito de bitmap personalizado usando um <see cref="T:System.Windows.Media.Effects.PixelShader" />.</summary>
	// Token: 0x02000605 RID: 1541
	public abstract class ShaderEffect : Effect
	{
		// Token: 0x06004694 RID: 18068 RVA: 0x00114A78 File Offset: 0x00113E78
		internal override Rect GetRenderBounds(Rect contentBounds)
		{
			Point point = default(Point);
			Point point2 = default(Point);
			point.X = contentBounds.TopLeft.X - this.PaddingLeft;
			point.Y = contentBounds.TopLeft.Y - this.PaddingTop;
			point2.X = contentBounds.BottomRight.X + this.PaddingRight;
			point2.Y = contentBounds.BottomRight.Y + this.PaddingBottom;
			return new Rect(point, point2);
		}

		/// <summary>Obtém ou define um valor que indica se a textura de saída do efeito é maior que sua textura de entrada ao longo da borda superior.</summary>
		/// <returns>O preenchimento ao longo da borda superior do efeito.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">O valor fornecido é menor que 0.</exception>
		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06004695 RID: 18069 RVA: 0x00114B10 File Offset: 0x00113F10
		// (set) Token: 0x06004696 RID: 18070 RVA: 0x00114B2C File Offset: 0x00113F2C
		protected double PaddingTop
		{
			get
			{
				base.ReadPreamble();
				return this._topPadding;
			}
			set
			{
				base.WritePreamble();
				if (value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Get("Effect_ShaderEffectPadding"));
				}
				this._topPadding = value;
				base.RegisterForAsyncUpdateResource();
				base.WritePostscript();
			}
		}

		/// <summary>Obtém ou define um valor que indica se a textura de saída do efeito é maior que sua textura de entrada ao longo da borda inferior.</summary>
		/// <returns>O preenchimento ao longo da borda inferior do efeito.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">O valor fornecido é menor que 0.</exception>
		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06004697 RID: 18071 RVA: 0x00114B7C File Offset: 0x00113F7C
		// (set) Token: 0x06004698 RID: 18072 RVA: 0x00114B98 File Offset: 0x00113F98
		protected double PaddingBottom
		{
			get
			{
				base.ReadPreamble();
				return this._bottomPadding;
			}
			set
			{
				base.WritePreamble();
				if (value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Get("Effect_ShaderEffectPadding"));
				}
				this._bottomPadding = value;
				base.RegisterForAsyncUpdateResource();
				base.WritePostscript();
			}
		}

		/// <summary>Obtém ou define um valor que indica se a textura de saída do efeito é maior que sua textura de entrada ao longo da borda esquerda.</summary>
		/// <returns>O preenchimento ao longo da borda esquerda do efeito.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">O valor fornecido é menor que 0.</exception>
		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06004699 RID: 18073 RVA: 0x00114BE8 File Offset: 0x00113FE8
		// (set) Token: 0x0600469A RID: 18074 RVA: 0x00114C04 File Offset: 0x00114004
		protected double PaddingLeft
		{
			get
			{
				base.ReadPreamble();
				return this._leftPadding;
			}
			set
			{
				base.WritePreamble();
				if (value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Get("Effect_ShaderEffectPadding"));
				}
				this._leftPadding = value;
				base.RegisterForAsyncUpdateResource();
				base.WritePostscript();
			}
		}

		/// <summary>Obtém ou define um valor que indica se a textura de saída do efeito é maior que sua textura de entrada ao longo da borda direita.</summary>
		/// <returns>O preenchimento ao longo da borda direita do efeito.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">O valor fornecido é menor que 0.</exception>
		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x0600469B RID: 18075 RVA: 0x00114C54 File Offset: 0x00114054
		// (set) Token: 0x0600469C RID: 18076 RVA: 0x00114C70 File Offset: 0x00114070
		protected double PaddingRight
		{
			get
			{
				base.ReadPreamble();
				return this._rightPadding;
			}
			set
			{
				base.WritePreamble();
				if (value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Get("Effect_ShaderEffectPadding"));
				}
				this._rightPadding = value;
				base.RegisterForAsyncUpdateResource();
				base.WritePostscript();
			}
		}

		/// <summary>Obtém ou define um valor que indica o registro do sombreador a ser usado para as derivativas parciais das coordenadas de textura em relação ao espaço da tela.</summary>
		/// <returns>O índice do registro que contém as derivativas parciais.</returns>
		/// <exception cref="T:System.InvalidOperationException">Foi feita uma tentativa de definir a propriedade <see cref="P:System.Windows.Media.Effects.ShaderEffect.DdxUvDdyUvRegisterIndex" /> mais de uma vez ou após o processamento inicial do efeito.</exception>
		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x0600469D RID: 18077 RVA: 0x00114CC0 File Offset: 0x001140C0
		// (set) Token: 0x0600469E RID: 18078 RVA: 0x00114CDC File Offset: 0x001140DC
		protected int DdxUvDdyUvRegisterIndex
		{
			get
			{
				base.ReadPreamble();
				return this._ddxUvDdyUvRegisterIndex;
			}
			set
			{
				base.WritePreamble();
				this._ddxUvDdyUvRegisterIndex = value;
				base.WritePostscript();
			}
		}

		// Token: 0x0600469F RID: 18079 RVA: 0x00114CFC File Offset: 0x001140FC
		private void PixelShaderPropertyChangedHook(DependencyPropertyChangedEventArgs e)
		{
			PixelShader pixelShader = (PixelShader)e.OldValue;
			if (pixelShader != null)
			{
				pixelShader._shaderBytecodeChanged -= this.OnPixelShaderBytecodeChanged;
			}
			PixelShader pixelShader2 = (PixelShader)e.NewValue;
			if (pixelShader2 != null)
			{
				pixelShader2._shaderBytecodeChanged += this.OnPixelShaderBytecodeChanged;
			}
			this.OnPixelShaderBytecodeChanged(this.PixelShader, null);
		}

		// Token: 0x060046A0 RID: 18080 RVA: 0x00114D5C File Offset: 0x0011415C
		private void OnPixelShaderBytecodeChanged(object sender, EventArgs e)
		{
			PixelShader pixelShader = (PixelShader)sender;
			if (pixelShader != null && pixelShader.ShaderMajorVersion == 2 && pixelShader.ShaderMinorVersion == 0 && this.UsesPS30OnlyRegisters())
			{
				throw new InvalidOperationException(SR.Get("Effect_20ShaderUsing30Registers"));
			}
		}

		// Token: 0x060046A1 RID: 18081 RVA: 0x00114D9C File Offset: 0x0011419C
		private bool UsesPS30OnlyRegisters()
		{
			if (this._intCount > 0U || this._intRegisters != null || this._boolCount > 0U || this._boolRegisters != null)
			{
				return true;
			}
			if (this._floatRegisters != null)
			{
				for (int i = 32; i < this._floatRegisters.Count; i++)
				{
					if (this._floatRegisters[i] != null)
					{
						return true;
					}
				}
			}
			if (this._samplerData != null)
			{
				for (int j = 4; j < this._samplerData.Count; j++)
				{
					if (this._samplerData[j] != null)
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Notifica o efeito de que a constante ou a amostra de sombreador correspondente à propriedade de dependência especificada deve ser atualizada.</summary>
		/// <param name="dp">A propriedade de dependência a ser atualizada.</param>
		// Token: 0x060046A2 RID: 18082 RVA: 0x00114E3C File Offset: 0x0011423C
		protected void UpdateShaderValue(DependencyProperty dp)
		{
			if (dp != null)
			{
				base.WritePreamble();
				object value = base.GetValue(dp);
				PropertyMetadata metadata = dp.GetMetadata(this);
				if (metadata != null)
				{
					PropertyChangedCallback propertyChangedCallback = metadata.PropertyChangedCallback;
					if (propertyChangedCallback != null)
					{
						propertyChangedCallback(this, new DependencyPropertyChangedEventArgs(dp, value, value));
					}
				}
				base.WritePostscript();
			}
		}

		/// <summary>Associa um valor da propriedade de dependência a um registro constante de float de um sombreador de pixel.</summary>
		/// <param name="floatRegisterIndex">O índice do registro do sombreador associado à propriedade de dependência.</param>
		/// <returns>Um representante de <see cref="T:System.Windows.PropertyChangedCallback" /> que associa uma propriedade de dependência e o registro de constante de sombreador especificado pelo <paramref name="floatRegisterIndex" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">A propriedade de dependência é um tipo desconhecido.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="floatRegisterIndex" /> é maior ou igual a 32 ou <paramref name="floatRegisterIndex" /> é menor que 0.</exception>
		// Token: 0x060046A3 RID: 18083 RVA: 0x00114E84 File Offset: 0x00114284
		protected static PropertyChangedCallback PixelShaderConstantCallback(int floatRegisterIndex)
		{
			return delegate(DependencyObject obj, DependencyPropertyChangedEventArgs args)
			{
				ShaderEffect shaderEffect = obj as ShaderEffect;
				if (shaderEffect != null)
				{
					shaderEffect.UpdateShaderConstant(args.Property, args.NewValue, floatRegisterIndex);
				}
			};
		}

		/// <summary>Associa um valor da propriedade de dependência a um registro de amostra de sombreador de pixel.</summary>
		/// <param name="samplerRegisterIndex">O índice da amostra do sombreador associado à propriedade de dependência.</param>
		/// <returns>Um representante de <see cref="T:System.Windows.PropertyChangedCallback" /> que associa uma propriedade de dependência e o registro de amostra de sombreador especificado pelo <paramref name="samplerRegisterIndex" />.</returns>
		// Token: 0x060046A4 RID: 18084 RVA: 0x00114EAC File Offset: 0x001142AC
		protected static PropertyChangedCallback PixelShaderSamplerCallback(int samplerRegisterIndex)
		{
			return ShaderEffect.PixelShaderSamplerCallback(samplerRegisterIndex, SamplingMode.Auto);
		}

		/// <summary>Associa um valor da propriedade de dependência a um registro de amostra de um sombreador de pixel e a um <see cref="T:System.Windows.Media.Effects.SamplingMode" />.</summary>
		/// <param name="samplerRegisterIndex">O índice da amostra do sombreador associado à propriedade de dependência.</param>
		/// <param name="samplingMode">O <see cref="T:System.Windows.Media.Effects.SamplingMode" /> da amostra de sombreador.</param>
		/// <returns>Um representante de <see cref="T:System.Windows.PropertyChangedCallback" /> que associa uma propriedade de dependência e o registro de amostra de sombreador especificado pelo <paramref name="samplerRegisterIndex" />.</returns>
		// Token: 0x060046A5 RID: 18085 RVA: 0x00114EC0 File Offset: 0x001142C0
		protected static PropertyChangedCallback PixelShaderSamplerCallback(int samplerRegisterIndex, SamplingMode samplingMode)
		{
			return delegate(DependencyObject obj, DependencyPropertyChangedEventArgs args)
			{
				ShaderEffect shaderEffect = obj as ShaderEffect;
				if (shaderEffect != null && args.IsAValueChange)
				{
					shaderEffect.UpdateShaderSampler(args.Property, args.NewValue, samplerRegisterIndex, samplingMode);
				}
			};
		}

		/// <summary>Associa uma propriedade de dependência a um registro de amostra de sombreador.</summary>
		/// <param name="dpName">O nome da propriedade de dependência.</param>
		/// <param name="ownerType">O tipo do efeito que tem a propriedade de dependência.</param>
		/// <param name="samplerRegisterIndex">O índice da amostra do sombreador associado à propriedade de dependência.</param>
		/// <returns>Uma propriedade de dependência associada à amostra de sombreador especificada pelo <paramref name="samplerRegisterIndex" />.</returns>
		// Token: 0x060046A6 RID: 18086 RVA: 0x00114EF0 File Offset: 0x001142F0
		protected static DependencyProperty RegisterPixelShaderSamplerProperty(string dpName, Type ownerType, int samplerRegisterIndex)
		{
			return ShaderEffect.RegisterPixelShaderSamplerProperty(dpName, ownerType, samplerRegisterIndex, SamplingMode.Auto);
		}

		/// <summary>Associa uma propriedade de dependência um registro de amostra do sombreador e um <see cref="T:System.Windows.Media.Effects.SamplingMode" />.</summary>
		/// <param name="dpName">O nome da propriedade de dependência.</param>
		/// <param name="ownerType">O tipo do efeito que tem a propriedade de dependência.</param>
		/// <param name="samplerRegisterIndex">O índice da amostra do sombreador associado à propriedade de dependência.</param>
		/// <param name="samplingMode">O <see cref="T:System.Windows.Media.Effects.SamplingMode" /> da amostra de sombreador.</param>
		/// <returns>Uma propriedade de dependência associada à amostra de sombreador especificada pelo <paramref name="samplerRegisterIndex" />.</returns>
		// Token: 0x060046A7 RID: 18087 RVA: 0x00114F08 File Offset: 0x00114308
		protected static DependencyProperty RegisterPixelShaderSamplerProperty(string dpName, Type ownerType, int samplerRegisterIndex, SamplingMode samplingMode)
		{
			return DependencyProperty.Register(dpName, typeof(Brush), ownerType, new UIPropertyMetadata(Effect.ImplicitInput, ShaderEffect.PixelShaderSamplerCallback(samplerRegisterIndex, samplingMode)));
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x00114F38 File Offset: 0x00114338
		private void UpdateShaderConstant(DependencyProperty dp, object newValue, int registerIndex)
		{
			base.WritePreamble();
			Type left = ShaderEffect.DetermineShaderConstantType(dp.PropertyType, this.PixelShader);
			if (left == null)
			{
				throw new InvalidOperationException(SR.Get("Effect_ShaderConstantType", new object[]
				{
					dp.PropertyType.Name
				}));
			}
			int num = 32;
			string id = "Effect_Shader20ConstantRegisterLimit";
			if (this.PixelShader != null && this.PixelShader.ShaderMajorVersion >= 3)
			{
				if (left == typeof(float))
				{
					num = 224;
					id = "Effect_Shader30FloatConstantRegisterLimit";
				}
				else if (left == typeof(int))
				{
					num = 16;
					id = "Effect_Shader30IntConstantRegisterLimit";
				}
				else if (left == typeof(bool))
				{
					num = 16;
					id = "Effect_Shader30BoolConstantRegisterLimit";
				}
			}
			if (registerIndex >= num || registerIndex < 0)
			{
				throw new ArgumentException(SR.Get(id), "dp");
			}
			if (left == typeof(float))
			{
				MilColorF value;
				ShaderEffect.ConvertValueToMilColorF(newValue, out value);
				ShaderEffect.StashInPosition<MilColorF>(ref this._floatRegisters, registerIndex, value, num, ref this._floatCount);
			}
			else if (left == typeof(int))
			{
				MilColorI value2;
				ShaderEffect.ConvertValueToMilColorI(newValue, out value2);
				ShaderEffect.StashInPosition<MilColorI>(ref this._intRegisters, registerIndex, value2, num, ref this._intCount);
			}
			else if (left == typeof(bool))
			{
				ShaderEffect.StashInPosition<bool>(ref this._boolRegisters, registerIndex, (bool)newValue, num, ref this._boolCount);
			}
			base.PropertyChanged(dp);
			base.WritePostscript();
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x001150B4 File Offset: 0x001144B4
		private void UpdateShaderSampler(DependencyProperty dp, object newValue, int registerIndex, SamplingMode samplingMode)
		{
			base.WritePreamble();
			if (newValue != null && !typeof(VisualBrush).IsInstanceOfType(newValue) && !typeof(BitmapCacheBrush).IsInstanceOfType(newValue) && !typeof(ImplicitInputBrush).IsInstanceOfType(newValue) && !typeof(ImageBrush).IsInstanceOfType(newValue))
			{
				throw new ArgumentException(SR.Get("Effect_ShaderSamplerType"), "dp");
			}
			int num = 4;
			string id = "Effect_Shader20SamplerRegisterLimit";
			if (this.PixelShader != null && this.PixelShader.ShaderMajorVersion >= 3)
			{
				num = 8;
				id = "Effect_Shader30SamplerRegisterLimit";
			}
			if (registerIndex >= num || registerIndex < 0)
			{
				throw new ArgumentException(SR.Get(id));
			}
			ShaderEffect.SamplerData newSampler = new ShaderEffect.SamplerData
			{
				_brush = (Brush)newValue,
				_samplingMode = samplingMode
			};
			this.StashSamplerDataInPosition(registerIndex, newSampler, num);
			base.PropertyChanged(dp);
			base.WritePostscript();
		}

		// Token: 0x060046AA RID: 18090 RVA: 0x00115198 File Offset: 0x00114598
		private static void StashInPosition<T>(ref List<T?> list, int position, T value, int maxIndex, ref uint count) where T : struct
		{
			if (list == null)
			{
				list = new List<T?>(maxIndex);
			}
			if (list.Count <= position)
			{
				int num = position - list.Count + 1;
				for (int i = 0; i < num; i++)
				{
					list.Add(null);
				}
			}
			if (list[position] == null)
			{
				count += 1U;
			}
			list[position] = new T?(value);
		}

		// Token: 0x060046AB RID: 18091 RVA: 0x0011520C File Offset: 0x0011460C
		private void StashSamplerDataInPosition(int position, ShaderEffect.SamplerData newSampler, int maxIndex)
		{
			if (this._samplerData == null)
			{
				this._samplerData = new List<ShaderEffect.SamplerData?>(maxIndex);
			}
			if (this._samplerData.Count <= position)
			{
				int num = position - this._samplerData.Count + 1;
				for (int i = 0; i < num; i++)
				{
					this._samplerData.Add(null);
				}
			}
			if (this._samplerData[position] == null)
			{
				this._samplerCount += 1U;
			}
			Dispatcher dispatcher = base.Dispatcher;
			if (dispatcher != null)
			{
				ShaderEffect.SamplerData? samplerData = this._samplerData[position];
				Brush resource = null;
				if (samplerData != null)
				{
					ShaderEffect.SamplerData value = samplerData.Value;
					resource = value._brush;
				}
				Brush brush = newSampler._brush;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = ((DUCE.IResource)this).GetChannelCount();
					for (int j = 0; j < channelCount; j++)
					{
						DUCE.Channel channel = ((DUCE.IResource)this).GetChannel(j);
						base.ReleaseResource(resource, channel);
						base.AddRefResource(brush, channel);
					}
				}
			}
			this._samplerData[position] = new ShaderEffect.SamplerData?(newSampler);
		}

		// Token: 0x060046AC RID: 18092 RVA: 0x0011534C File Offset: 0x0011474C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private unsafe void ManualUpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			checked
			{
				if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
				{
					if (this.PixelShader == null)
					{
						throw new InvalidOperationException(SR.Get("Effect_ShaderPixelShaderSet"));
					}
					DUCE.MILCMD_SHADEREFFECT milcmd_SHADEREFFECT;
					milcmd_SHADEREFFECT.Type = MILCMD.MilCmdShaderEffect;
					milcmd_SHADEREFFECT.Handle = this._duceResource.GetHandle(channel);
					milcmd_SHADEREFFECT.TopPadding = this._topPadding;
					milcmd_SHADEREFFECT.BottomPadding = this._bottomPadding;
					milcmd_SHADEREFFECT.LeftPadding = this._leftPadding;
					milcmd_SHADEREFFECT.RightPadding = this._rightPadding;
					milcmd_SHADEREFFECT.DdxUvDdyUvRegisterIndex = this.DdxUvDdyUvRegisterIndex;
					milcmd_SHADEREFFECT.hPixelShader = ((DUCE.IResource)this.PixelShader).GetHandle(channel);
					milcmd_SHADEREFFECT.ShaderConstantFloatRegistersSize = 2U * this._floatCount;
					milcmd_SHADEREFFECT.DependencyPropertyFloatValuesSize = 16U * this._floatCount;
					milcmd_SHADEREFFECT.ShaderConstantIntRegistersSize = 2U * this._intCount;
					milcmd_SHADEREFFECT.DependencyPropertyIntValuesSize = 16U * this._intCount;
					milcmd_SHADEREFFECT.ShaderConstantBoolRegistersSize = 2U * this._boolCount;
					milcmd_SHADEREFFECT.DependencyPropertyBoolValuesSize = 4U * this._boolCount;
					milcmd_SHADEREFFECT.ShaderSamplerRegistrationInfoSize = 8U * this._samplerCount;
					milcmd_SHADEREFFECT.DependencyPropertySamplerValuesSize = (uint)(unchecked((long)sizeof(DUCE.ResourceHandle)) * (long)(unchecked((ulong)this._samplerCount)));
					channel.BeginCommand(unchecked((byte*)(&milcmd_SHADEREFFECT)), sizeof(DUCE.MILCMD_SHADEREFFECT), (int)(milcmd_SHADEREFFECT.ShaderConstantFloatRegistersSize + milcmd_SHADEREFFECT.DependencyPropertyFloatValuesSize + milcmd_SHADEREFFECT.ShaderConstantIntRegistersSize + milcmd_SHADEREFFECT.DependencyPropertyIntValuesSize + milcmd_SHADEREFFECT.ShaderConstantBoolRegistersSize + milcmd_SHADEREFFECT.DependencyPropertyBoolValuesSize + milcmd_SHADEREFFECT.ShaderSamplerRegistrationInfoSize + milcmd_SHADEREFFECT.DependencyPropertySamplerValuesSize));
					this.AppendRegisters<MilColorF>(channel, this._floatRegisters);
					if (this._floatRegisters != null)
					{
						for (int i = 0; i < this._floatRegisters.Count; i++)
						{
							MilColorF? milColorF = this._floatRegisters[i];
							if (milColorF != null)
							{
								MilColorF value = milColorF.Value;
								channel.AppendCommandData(unchecked((byte*)(&value)), sizeof(MilColorF));
							}
						}
					}
					this.AppendRegisters<MilColorI>(channel, this._intRegisters);
					if (this._intRegisters != null)
					{
						for (int j = 0; j < this._intRegisters.Count; j++)
						{
							MilColorI? milColorI = this._intRegisters[j];
							if (milColorI != null)
							{
								MilColorI value2 = milColorI.Value;
								channel.AppendCommandData(unchecked((byte*)(&value2)), sizeof(MilColorI));
							}
						}
					}
					this.AppendRegisters<bool>(channel, this._boolRegisters);
					if (this._boolRegisters != null)
					{
						for (int k = 0; k < this._boolRegisters.Count; k++)
						{
							bool? flag = this._boolRegisters[k];
							if (flag != null)
							{
								int num = flag.Value ? 1 : 0;
								channel.AppendCommandData(unchecked((byte*)(&num)), 4);
							}
						}
					}
					if (this._samplerCount > 0U)
					{
						int count = this._samplerData.Count;
						for (int l = 0; l < count; l++)
						{
							ShaderEffect.SamplerData? samplerData = this._samplerData[l];
							unchecked
							{
								if (samplerData != null)
								{
									ShaderEffect.SamplerData value3 = samplerData.Value;
									channel.AppendCommandData((byte*)(&l), 4);
									int samplingMode = (int)value3._samplingMode;
									channel.AppendCommandData((byte*)(&samplingMode), 4);
								}
							}
						}
					}
					if (this._samplerCount > 0U)
					{
						for (int m = 0; m < this._samplerData.Count; m++)
						{
							ShaderEffect.SamplerData? samplerData2 = this._samplerData[m];
							if (samplerData2 != null)
							{
								ShaderEffect.SamplerData value4 = samplerData2.Value;
								DUCE.ResourceHandle resourceHandle = (value4._brush != null) ? ((DUCE.IResource)value4._brush).GetHandle(channel) : DUCE.ResourceHandle.Null;
								channel.AppendCommandData(unchecked((byte*)(&resourceHandle)), sizeof(DUCE.ResourceHandle));
							}
						}
					}
					channel.EndCommand();
				}
			}
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x001156AC File Offset: 0x00114AAC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private unsafe void AppendRegisters<T>(DUCE.Channel channel, List<T?> list) where T : struct
		{
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] != null)
					{
						short num = (short)i;
						channel.AppendCommandData((byte*)(&num), 2);
					}
				}
			}
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x001156EC File Offset: 0x00114AEC
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_SHADEREFFECT))
			{
				if (this._samplerCount > 0U)
				{
					int count = this._samplerData.Count;
					for (int i = 0; i < count; i++)
					{
						ShaderEffect.SamplerData? samplerData = this._samplerData[i];
						if (samplerData != null)
						{
							ShaderEffect.SamplerData value = samplerData.Value;
							DUCE.IResource brush = value._brush;
							if (brush != null)
							{
								brush.AddRefOnChannel(channel);
							}
						}
					}
				}
				PixelShader pixelShader = this.PixelShader;
				if (pixelShader != null)
				{
					((DUCE.IResource)pixelShader).AddRefOnChannel(channel);
				}
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x00115790 File Offset: 0x00114B90
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				if (this._samplerCount > 0U)
				{
					int count = this._samplerData.Count;
					for (int i = 0; i < count; i++)
					{
						ShaderEffect.SamplerData? samplerData = this._samplerData[i];
						if (samplerData != null)
						{
							ShaderEffect.SamplerData value = samplerData.Value;
							DUCE.IResource brush = value._brush;
							if (brush != null)
							{
								brush.ReleaseOnChannel(channel);
							}
						}
					}
				}
				PixelShader pixelShader = this.PixelShader;
				if (pixelShader != null)
				{
					((DUCE.IResource)pixelShader).ReleaseOnChannel(channel);
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x00115818 File Offset: 0x00114C18
		internal static Type DetermineShaderConstantType(Type type, PixelShader pixelShader)
		{
			Type result = null;
			if (type == typeof(double) || type == typeof(float) || type == typeof(Color) || type == typeof(Point) || type == typeof(Size) || type == typeof(Vector) || type == typeof(Point3D) || type == typeof(Vector3D) || type == typeof(Point4D))
			{
				result = typeof(float);
			}
			else if (pixelShader != null && pixelShader.ShaderMajorVersion >= 3)
			{
				if (type == typeof(int) || type == typeof(uint) || type == typeof(byte) || type == typeof(sbyte) || type == typeof(long) || type == typeof(ulong) || type == typeof(short) || type == typeof(ushort) || type == typeof(char))
				{
					result = typeof(int);
				}
				else if (type == typeof(bool))
				{
					result = typeof(bool);
				}
			}
			return result;
		}

		// Token: 0x060046B1 RID: 18097 RVA: 0x001159C4 File Offset: 0x00114DC4
		internal static void ConvertValueToMilColorF(object value, out MilColorF newVal)
		{
			Type type = value.GetType();
			if (type == typeof(double) || type == typeof(float))
			{
				float a = (type == typeof(double)) ? ((float)((double)value)) : ((float)value);
				newVal.r = (newVal.g = (newVal.b = (newVal.a = a)));
				return;
			}
			if (type == typeof(Color))
			{
				Color color = (Color)value;
				newVal.r = (float)color.R / 255f;
				newVal.g = (float)color.G / 255f;
				newVal.b = (float)color.B / 255f;
				newVal.a = (float)color.A / 255f;
				return;
			}
			if (type == typeof(Point))
			{
				Point point = (Point)value;
				newVal.r = (float)point.X;
				newVal.g = (float)point.Y;
				newVal.b = 1f;
				newVal.a = 1f;
				return;
			}
			if (type == typeof(Size))
			{
				Size size = (Size)value;
				newVal.r = (float)size.Width;
				newVal.g = (float)size.Height;
				newVal.b = 1f;
				newVal.a = 1f;
				return;
			}
			if (type == typeof(Vector))
			{
				Vector vector = (Vector)value;
				newVal.r = (float)vector.X;
				newVal.g = (float)vector.Y;
				newVal.b = 1f;
				newVal.a = 1f;
				return;
			}
			if (type == typeof(Point3D))
			{
				Point3D point3D = (Point3D)value;
				newVal.r = (float)point3D.X;
				newVal.g = (float)point3D.Y;
				newVal.b = (float)point3D.Z;
				newVal.a = 1f;
				return;
			}
			if (type == typeof(Vector3D))
			{
				Vector3D vector3D = (Vector3D)value;
				newVal.r = (float)vector3D.X;
				newVal.g = (float)vector3D.Y;
				newVal.b = (float)vector3D.Z;
				newVal.a = 1f;
				return;
			}
			if (type == typeof(Point4D))
			{
				Point4D point4D = (Point4D)value;
				newVal.r = (float)point4D.X;
				newVal.g = (float)point4D.Y;
				newVal.b = (float)point4D.Z;
				newVal.a = (float)point4D.W;
				return;
			}
			newVal.r = (newVal.b = (newVal.g = (newVal.a = 1f)));
		}

		// Token: 0x060046B2 RID: 18098 RVA: 0x00115CAC File Offset: 0x001150AC
		internal static void ConvertValueToMilColorI(object value, out MilColorI newVal)
		{
			Type type = value.GetType();
			int a;
			if (type == typeof(long))
			{
				a = (int)((long)value);
			}
			else if (type == typeof(ulong))
			{
				a = (int)((ulong)value);
			}
			else if (type == typeof(uint))
			{
				a = (int)((uint)value);
			}
			else if (type == typeof(short))
			{
				a = (int)((short)value);
			}
			else if (type == typeof(ushort))
			{
				a = (int)((ushort)value);
			}
			else if (type == typeof(byte))
			{
				a = (int)((byte)value);
			}
			else if (type == typeof(sbyte))
			{
				a = (int)((sbyte)value);
			}
			else if (type == typeof(char))
			{
				a = (int)((char)value);
			}
			else
			{
				a = (int)value;
			}
			newVal.r = (newVal.g = (newVal.b = (newVal.a = a)));
		}

		/// <summary>Faz com que a instância seja um clone (cópia em profundidade) do <see cref="T:System.Windows.Freezable" /> especificado usando valores de propriedade base (não animados).</summary>
		/// <param name="sourceFreezable">O objeto a ser clonado.</param>
		// Token: 0x060046B3 RID: 18099 RVA: 0x00115DD0 File Offset: 0x001151D0
		protected override void CloneCore(Freezable sourceFreezable)
		{
			ShaderEffect effect = (ShaderEffect)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(effect);
		}

		/// <summary>Torna a instância um clone modificável (cópia em profundidade) do <see cref="T:System.Windows.Freezable" /> especificado usando os valores de propriedade atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Freezable" /> a ser clonado.</param>
		// Token: 0x060046B4 RID: 18100 RVA: 0x00115DF4 File Offset: 0x001151F4
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			ShaderEffect effect = (ShaderEffect)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(effect);
		}

		/// <summary>Torna a instância um clone congelado do <see cref="T:System.Windows.Freezable" /> especificado usando valores de propriedade base (não animados).</summary>
		/// <param name="sourceFreezable">A instância a ser copiada.</param>
		// Token: 0x060046B5 RID: 18101 RVA: 0x00115E18 File Offset: 0x00115218
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			ShaderEffect effect = (ShaderEffect)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			this.CopyCommon(effect);
		}

		/// <summary>Torna a instância atual um clone congelado do <see cref="T:System.Windows.Freezable" /> especificado. Se o objeto tiver propriedades de dependência animadas, seus valores animados atuais serão copiados.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Freezable" /> a ser copiado e congelado.</param>
		// Token: 0x060046B6 RID: 18102 RVA: 0x00115E3C File Offset: 0x0011523C
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			ShaderEffect effect = (ShaderEffect)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			this.CopyCommon(effect);
		}

		// Token: 0x060046B7 RID: 18103 RVA: 0x00115E60 File Offset: 0x00115260
		private void CopyCommon(ShaderEffect effect)
		{
			this._topPadding = effect._topPadding;
			this._bottomPadding = effect._bottomPadding;
			this._leftPadding = effect._leftPadding;
			this._rightPadding = effect._rightPadding;
			if (this._floatRegisters != null)
			{
				this._floatRegisters = new List<MilColorF?>(effect._floatRegisters);
			}
			if (this._samplerData != null)
			{
				this._samplerData = new List<ShaderEffect.SamplerData?>(effect._samplerData);
			}
			this._floatCount = effect._floatCount;
			this._samplerCount = effect._samplerCount;
			this._ddxUvDdyUvRegisterIndex = effect._ddxUvDdyUvRegisterIndex;
		}

		/// <summary>Cria um clone modificável deste objeto <see cref="T:System.Windows.Media.Effects.ShaderEffect" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência desse objeto, esse método copia associações de dados e referências de recurso (que não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável dessa instância. O clone retornado é efetivamente uma cópia profunda do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do clone é <see langword="false" />.</returns>
		// Token: 0x060046B8 RID: 18104 RVA: 0x00115EF4 File Offset: 0x001152F4
		public new ShaderEffect Clone()
		{
			return (ShaderEffect)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Effects.ShaderEffect" />, fazendo cópias em profundidade dos valores do objeto atual. Referências de recursos, associações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060046B9 RID: 18105 RVA: 0x00115F0C File Offset: 0x0011530C
		public new ShaderEffect CloneCurrentValue()
		{
			return (ShaderEffect)base.CloneCurrentValue();
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x00115F24 File Offset: 0x00115324
		private static void PixelShaderPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ShaderEffect shaderEffect = (ShaderEffect)d;
			shaderEffect.PixelShaderPropertyChangedHook(e);
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			PixelShader resource = (PixelShader)e.OldValue;
			PixelShader resource2 = (PixelShader)e.NewValue;
			Dispatcher dispatcher = shaderEffect.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = shaderEffect;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						shaderEffect.ReleaseResource(resource, channel);
						shaderEffect.AddRefResource(resource2, channel);
					}
				}
			}
			shaderEffect.PropertyChanged(ShaderEffect.PixelShaderProperty);
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Effects.PixelShader" /> a ser usado para o efeito.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.Effects.PixelShader" /> para o efeito.</returns>
		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x060046BB RID: 18107 RVA: 0x00115FF0 File Offset: 0x001153F0
		// (set) Token: 0x060046BC RID: 18108 RVA: 0x00116010 File Offset: 0x00115410
		protected PixelShader PixelShader
		{
			get
			{
				return (PixelShader)base.GetValue(ShaderEffect.PixelShaderProperty);
			}
			set
			{
				base.SetValueInternal(ShaderEffect.PixelShaderProperty, value);
			}
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x060046BD RID: 18109 RVA: 0x0011602C File Offset: 0x0011542C
		protected override Freezable CreateInstanceCore()
		{
			return (Freezable)Activator.CreateInstance(base.GetType());
		}

		// Token: 0x060046BE RID: 18110 RVA: 0x0011604C File Offset: 0x0011544C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			this.ManualUpdateResource(channel, skipOnChannelCheck);
			base.UpdateResource(channel, skipOnChannelCheck);
		}

		// Token: 0x060046BF RID: 18111 RVA: 0x0011606C File Offset: 0x0011546C
		private DUCE.ResourceHandle GeneratedAddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_SHADEREFFECT))
			{
				PixelShader pixelShader = this.PixelShader;
				if (pixelShader != null)
				{
					((DUCE.IResource)pixelShader).AddRefOnChannel(channel);
				}
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060046C0 RID: 18112 RVA: 0x001160B8 File Offset: 0x001154B8
		private void GeneratedReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				PixelShader pixelShader = this.PixelShader;
				if (pixelShader != null)
				{
					((DUCE.IResource)pixelShader).ReleaseOnChannel(channel);
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x060046C1 RID: 18113 RVA: 0x001160EC File Offset: 0x001154EC
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060046C2 RID: 18114 RVA: 0x00116108 File Offset: 0x00115508
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x060046C3 RID: 18115 RVA: 0x00116120 File Offset: 0x00115520
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x060046C4 RID: 18116 RVA: 0x0011613C File Offset: 0x0011553C
		static ShaderEffect()
		{
			Type typeFromHandle = typeof(ShaderEffect);
			ShaderEffect.PixelShaderProperty = Animatable.RegisterProperty("PixelShader", typeof(PixelShader), typeFromHandle, null, new PropertyChangedCallback(ShaderEffect.PixelShaderPropertyChanged), null, false, null);
		}

		// Token: 0x040019B3 RID: 6579
		private const SamplingMode _defaultSamplingMode = SamplingMode.Auto;

		// Token: 0x040019B4 RID: 6580
		private double _topPadding;

		// Token: 0x040019B5 RID: 6581
		private double _bottomPadding;

		// Token: 0x040019B6 RID: 6582
		private double _leftPadding;

		// Token: 0x040019B7 RID: 6583
		private double _rightPadding;

		// Token: 0x040019B8 RID: 6584
		private List<MilColorF?> _floatRegisters;

		// Token: 0x040019B9 RID: 6585
		private List<MilColorI?> _intRegisters;

		// Token: 0x040019BA RID: 6586
		private List<bool?> _boolRegisters;

		// Token: 0x040019BB RID: 6587
		private List<ShaderEffect.SamplerData?> _samplerData;

		// Token: 0x040019BC RID: 6588
		private uint _floatCount;

		// Token: 0x040019BD RID: 6589
		private uint _intCount;

		// Token: 0x040019BE RID: 6590
		private uint _boolCount;

		// Token: 0x040019BF RID: 6591
		private uint _samplerCount;

		// Token: 0x040019C0 RID: 6592
		private int _ddxUvDdyUvRegisterIndex = -1;

		// Token: 0x040019C1 RID: 6593
		private const int PS_2_0_FLOAT_REGISTER_LIMIT = 32;

		// Token: 0x040019C2 RID: 6594
		private const int PS_3_0_FLOAT_REGISTER_LIMIT = 224;

		// Token: 0x040019C3 RID: 6595
		private const int PS_3_0_INT_REGISTER_LIMIT = 16;

		// Token: 0x040019C4 RID: 6596
		private const int PS_3_0_BOOL_REGISTER_LIMIT = 16;

		// Token: 0x040019C5 RID: 6597
		private const int PS_2_0_SAMPLER_LIMIT = 4;

		// Token: 0x040019C6 RID: 6598
		private const int PS_3_0_SAMPLER_LIMIT = 8;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.ShaderEffect.PixelShader" />.</summary>
		// Token: 0x040019C7 RID: 6599
		protected static readonly DependencyProperty PixelShaderProperty;

		// Token: 0x040019C8 RID: 6600
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x020008DC RID: 2268
		private struct SamplerData
		{
			// Token: 0x04002997 RID: 10647
			public Brush _brush;

			// Token: 0x04002998 RID: 10648
			public SamplingMode _samplingMode;
		}
	}
}
