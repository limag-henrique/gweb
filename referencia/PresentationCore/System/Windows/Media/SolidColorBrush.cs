using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Pinta uma área com uma cor sólida.</summary>
	// Token: 0x020003EF RID: 1007
	public sealed class SolidColorBrush : Brush
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.SolidColorBrush" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002785 RID: 10117 RVA: 0x0009F154 File Offset: 0x0009E554
		public new SolidColorBrush Clone()
		{
			return (SolidColorBrush)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.SolidColorBrush" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002786 RID: 10118 RVA: 0x0009F16C File Offset: 0x0009E56C
		public new SolidColorBrush CloneCurrentValue()
		{
			return (SolidColorBrush)base.CloneCurrentValue();
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x0009F184 File Offset: 0x0009E584
		private static void ColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SolidColorBrush solidColorBrush = (SolidColorBrush)d;
			solidColorBrush.PropertyChanged(SolidColorBrush.ColorProperty);
		}

		/// <summary>Obtém ou define a cor desse <see cref="T:System.Windows.Media.SolidColorBrush" />.</summary>
		/// <returns>A cor do pincel. O valor padrão é <see cref="P:System.Windows.Media.Colors.Transparent" />.</returns>
		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06002788 RID: 10120 RVA: 0x0009F1A4 File Offset: 0x0009E5A4
		// (set) Token: 0x06002789 RID: 10121 RVA: 0x0009F1C4 File Offset: 0x0009E5C4
		public Color Color
		{
			get
			{
				return (Color)base.GetValue(SolidColorBrush.ColorProperty);
			}
			set
			{
				base.SetValueInternal(SolidColorBrush.ColorProperty, value);
			}
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x0009F1E4 File Offset: 0x0009E5E4
		protected override Freezable CreateInstanceCore()
		{
			return new SolidColorBrush();
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x0009F1F8 File Offset: 0x0009E5F8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Transform transform = base.Transform;
				Transform relativeTransform = base.RelativeTransform;
				DUCE.ResourceHandle hTransform;
				if (transform == null || transform == Transform.Identity)
				{
					hTransform = DUCE.ResourceHandle.Null;
				}
				else
				{
					hTransform = ((DUCE.IResource)transform).GetHandle(channel);
				}
				DUCE.ResourceHandle hRelativeTransform;
				if (relativeTransform == null || relativeTransform == Transform.Identity)
				{
					hRelativeTransform = DUCE.ResourceHandle.Null;
				}
				else
				{
					hRelativeTransform = ((DUCE.IResource)relativeTransform).GetHandle(channel);
				}
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(Brush.OpacityProperty, channel);
				DUCE.ResourceHandle animationResourceHandle2 = base.GetAnimationResourceHandle(SolidColorBrush.ColorProperty, channel);
				DUCE.MILCMD_SOLIDCOLORBRUSH milcmd_SOLIDCOLORBRUSH;
				milcmd_SOLIDCOLORBRUSH.Type = MILCMD.MilCmdSolidColorBrush;
				milcmd_SOLIDCOLORBRUSH.Handle = this._duceResource.GetHandle(channel);
				if (animationResourceHandle.IsNull)
				{
					milcmd_SOLIDCOLORBRUSH.Opacity = base.Opacity;
				}
				milcmd_SOLIDCOLORBRUSH.hOpacityAnimations = animationResourceHandle;
				milcmd_SOLIDCOLORBRUSH.hTransform = hTransform;
				milcmd_SOLIDCOLORBRUSH.hRelativeTransform = hRelativeTransform;
				if (animationResourceHandle2.IsNull)
				{
					milcmd_SOLIDCOLORBRUSH.Color = CompositionResourceManager.ColorToMilColorF(this.Color);
				}
				milcmd_SOLIDCOLORBRUSH.hColorAnimations = animationResourceHandle2;
				channel.SendCommand((byte*)(&milcmd_SOLIDCOLORBRUSH), sizeof(DUCE.MILCMD_SOLIDCOLORBRUSH));
			}
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x0009F300 File Offset: 0x0009E700
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_SOLIDCOLORBRUSH))
			{
				Transform transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).AddRefOnChannel(channel);
				}
				Transform relativeTransform = base.RelativeTransform;
				if (relativeTransform != null)
				{
					((DUCE.IResource)relativeTransform).AddRefOnChannel(channel);
				}
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x0009F360 File Offset: 0x0009E760
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				Transform transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).ReleaseOnChannel(channel);
				}
				Transform relativeTransform = base.RelativeTransform;
				if (relativeTransform != null)
				{
					((DUCE.IResource)relativeTransform).ReleaseOnChannel(channel);
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x0009F3A4 File Offset: 0x0009E7A4
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x0009F3C0 File Offset: 0x0009E7C0
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x0009F3D8 File Offset: 0x0009E7D8
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06002791 RID: 10129 RVA: 0x0009F3F4 File Offset: 0x0009E7F4
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x0009F404 File Offset: 0x0009E804
		static SolidColorBrush()
		{
			Type typeFromHandle = typeof(SolidColorBrush);
			SolidColorBrush.ColorProperty = Animatable.RegisterProperty("Color", typeof(Color), typeFromHandle, Colors.Transparent, new PropertyChangedCallback(SolidColorBrush.ColorPropertyChanged), null, true, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.SolidColorBrush" /> sem cores nem animações.</summary>
		// Token: 0x06002793 RID: 10131 RVA: 0x0009F468 File Offset: 0x0009E868
		public SolidColorBrush()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.SolidColorBrush" /> com o <see cref="T:System.Windows.Media.Color" /> especificado.</summary>
		/// <param name="color">A cor a ser aplicada no preenchimento.</param>
		// Token: 0x06002794 RID: 10132 RVA: 0x0009F47C File Offset: 0x0009E87C
		public SolidColorBrush(Color color)
		{
			this.Color = color;
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x0009F498 File Offset: 0x0009E898
		[FriendAccessAllowed]
		internal static bool SerializeOn(BinaryWriter writer, string stringValue)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			KnownColor knownColor = KnownColors.ColorStringToKnownColor(stringValue);
			SolidColorBrush.TwoWayDictionary<SolidColorBrush, string> obj = SolidColorBrush.s_knownSolidColorBrushStringCache;
			lock (obj)
			{
				if (SolidColorBrush.s_knownSolidColorBrushStringCache.ContainsValue(stringValue))
				{
					knownColor = KnownColors.ArgbStringToKnownColor(stringValue);
				}
			}
			if (knownColor != KnownColor.UnknownColor)
			{
				writer.Write(1);
				writer.Write((uint)knownColor);
				return true;
			}
			stringValue = stringValue.Trim();
			if (stringValue.Length > 3)
			{
				writer.Write(2);
				writer.Write(stringValue);
				return true;
			}
			return false;
		}

		/// <summary>Este membro dá suporte à infraestrutura WPF e não se destina a ser usado diretamente do código.</summary>
		/// <param name="reader">O leitor binário do qual ler o <see cref="T:System.Windows.Media.SolidColorBrush" />.</param>
		/// <returns>O <see cref="T:System.Windows.Media.SolidColorBrush" /> desserializado.</returns>
		// Token: 0x06002796 RID: 10134 RVA: 0x0009F53C File Offset: 0x0009E93C
		public static object DeserializeFrom(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return SolidColorBrush.DeserializeFrom(reader, null);
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x0009F560 File Offset: 0x0009E960
		internal static object DeserializeFrom(BinaryReader reader, ITypeDescriptorContext context)
		{
			SolidColorBrush.SerializationBrushType serializationBrushType = (SolidColorBrush.SerializationBrushType)reader.ReadByte();
			if (serializationBrushType == SolidColorBrush.SerializationBrushType.KnownSolidColor)
			{
				uint argb = reader.ReadUInt32();
				SolidColorBrush solidColorBrush = KnownColors.SolidColorBrushFromUint(argb);
				SolidColorBrush.TwoWayDictionary<SolidColorBrush, string> obj = SolidColorBrush.s_knownSolidColorBrushStringCache;
				lock (obj)
				{
					if (!SolidColorBrush.s_knownSolidColorBrushStringCache.ContainsKey(solidColorBrush))
					{
						string value = solidColorBrush.Color.ConvertToString(null, TypeConverterHelper.InvariantEnglishUS);
						SolidColorBrush.s_knownSolidColorBrushStringCache[solidColorBrush] = value;
					}
				}
				return solidColorBrush;
			}
			if (serializationBrushType == SolidColorBrush.SerializationBrushType.OtherColor)
			{
				string text = reader.ReadString();
				BrushConverter brushConverter = new BrushConverter();
				return brushConverter.ConvertFromInvariantString(context, text);
			}
			throw new Exception(SR.Get("BrushUnknownBamlType"));
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x0009F620 File Offset: 0x0009EA20
		internal override bool CanSerializeToString()
		{
			return !base.HasAnimatedProperties && !base.HasAnyExpression() && base.Transform.IsIdentity && DoubleUtil.AreClose(base.Opacity, 1.0);
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x0009F664 File Offset: 0x0009EA64
		internal override string ConvertToString(string format, IFormatProvider provider)
		{
			string text = this.Color.ConvertToString(format, provider);
			if (format == null && provider == TypeConverterHelper.InvariantEnglishUS && KnownColors.IsKnownSolidColorBrush(this))
			{
				SolidColorBrush.TwoWayDictionary<SolidColorBrush, string> obj = SolidColorBrush.s_knownSolidColorBrushStringCache;
				lock (obj)
				{
					string text2 = null;
					if (SolidColorBrush.s_knownSolidColorBrushStringCache.TryGetValue(this, out text2))
					{
						text = text2;
					}
					else
					{
						SolidColorBrush.s_knownSolidColorBrushStringCache[this] = text;
					}
				}
			}
			return text;
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.SolidColorBrush.Color" />.</summary>
		// Token: 0x0400125C RID: 4700
		public static readonly DependencyProperty ColorProperty;

		// Token: 0x0400125D RID: 4701
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x0400125E RID: 4702
		internal static Color s_Color = Colors.Transparent;

		// Token: 0x0400125F RID: 4703
		private static SolidColorBrush.TwoWayDictionary<SolidColorBrush, string> s_knownSolidColorBrushStringCache = new SolidColorBrush.TwoWayDictionary<SolidColorBrush, string>(SolidColorBrush.ReferenceEqualityComparer<string>.Singleton);

		// Token: 0x0200087E RID: 2174
		internal enum SerializationBrushType : byte
		{
			// Token: 0x040028AC RID: 10412
			Unknown,
			// Token: 0x040028AD RID: 10413
			KnownSolidColor,
			// Token: 0x040028AE RID: 10414
			OtherColor
		}

		// Token: 0x0200087F RID: 2175
		internal class TwoWayDictionary<TKey, TValue>
		{
			// Token: 0x060057AE RID: 22446 RVA: 0x00166724 File Offset: 0x00165B24
			public TwoWayDictionary() : this(null, null)
			{
			}

			// Token: 0x060057AF RID: 22447 RVA: 0x0016673C File Offset: 0x00165B3C
			public TwoWayDictionary(IEqualityComparer<TKey> keyComparer) : this(keyComparer, null)
			{
			}

			// Token: 0x060057B0 RID: 22448 RVA: 0x00166754 File Offset: 0x00165B54
			public TwoWayDictionary(IEqualityComparer<TValue> valueComparer) : this(null, valueComparer)
			{
			}

			// Token: 0x060057B1 RID: 22449 RVA: 0x0016676C File Offset: 0x00165B6C
			public TwoWayDictionary(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
			{
				this.fwdDictionary = new Dictionary<TKey, TValue>(keyComparer);
				this.revDictionary = new Dictionary<TValue, List<TKey>>(valueComparer);
			}

			// Token: 0x060057B2 RID: 22450 RVA: 0x00166798 File Offset: 0x00165B98
			public bool TryGetValue(TKey key, out TValue value)
			{
				return this.fwdDictionary.TryGetValue(key, out value);
			}

			// Token: 0x060057B3 RID: 22451 RVA: 0x001667B4 File Offset: 0x00165BB4
			public bool TryGetKeys(TValue value, out List<TKey> keys)
			{
				return this.revDictionary.TryGetValue(value, out keys);
			}

			// Token: 0x060057B4 RID: 22452 RVA: 0x001667D0 File Offset: 0x00165BD0
			public bool ContainsValue(TValue value)
			{
				return this.revDictionary.ContainsKey(value);
			}

			// Token: 0x060057B5 RID: 22453 RVA: 0x001667EC File Offset: 0x00165BEC
			public bool ContainsKey(TKey key)
			{
				return this.fwdDictionary.ContainsKey(key);
			}

			// Token: 0x1700121B RID: 4635
			public TValue this[TKey key]
			{
				get
				{
					return this.fwdDictionary[key];
				}
				set
				{
					this.fwdDictionary[key] = value;
					List<TKey> list;
					if (!this.revDictionary.TryGetValue(value, out list))
					{
						list = new List<TKey>();
						this.revDictionary[value] = list;
					}
					list.Add(key);
				}
			}

			// Token: 0x060057B8 RID: 22456 RVA: 0x00166868 File Offset: 0x00165C68
			public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
			{
				return this.fwdDictionary.GetEnumerator();
			}

			// Token: 0x040028AF RID: 10415
			private Dictionary<TKey, TValue> fwdDictionary;

			// Token: 0x040028B0 RID: 10416
			private Dictionary<TValue, List<TKey>> revDictionary;
		}

		// Token: 0x02000880 RID: 2176
		internal class ReferenceEqualityComparer<T> : EqualityComparer<T> where T : class
		{
			// Token: 0x060057B9 RID: 22457 RVA: 0x00166880 File Offset: 0x00165C80
			public override bool Equals(T x, T y)
			{
				return x == y;
			}

			// Token: 0x060057BA RID: 22458 RVA: 0x0016689C File Offset: 0x00165C9C
			public override int GetHashCode(T obj)
			{
				return RuntimeHelpers.GetHashCode(obj);
			}

			// Token: 0x040028B1 RID: 10417
			internal static SolidColorBrush.ReferenceEqualityComparer<T> Singleton = new SolidColorBrush.ReferenceEqualityComparer<T>();
		}
	}
}
