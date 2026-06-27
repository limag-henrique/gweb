using System;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal;

namespace System.Windows.Media.Effects
{
	/// <summary>Fornece um efeito de bitmap personalizado.</summary>
	// Token: 0x02000610 RID: 1552
	[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
	public abstract class Effect : Animatable, DUCE.IResource
	{
		// Token: 0x06004761 RID: 18273 RVA: 0x00117DBC File Offset: 0x001171BC
		static Effect()
		{
			Effect.ImplicitInput.Freeze();
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Brush" /> que, quando usado como uma entrada para um <see cref="T:System.Windows.Media.Effects.Effect" />, faz com que o bitmap do <see cref="T:System.Windows.UIElement" /> ao qual o <see cref="T:System.Windows.Media.Effects.Effect" /> é aplicado para essa entrada.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.Brush" /> que atua como entrada.</returns>
		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06004762 RID: 18274 RVA: 0x00117DE0 File Offset: 0x001171E0
		// (set) Token: 0x06004763 RID: 18275 RVA: 0x00117DF4 File Offset: 0x001171F4
		[Browsable(false)]
		public static Brush ImplicitInput { get; private set; } = new ImplicitInputBrush();

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Effects.Effect" />.</summary>
		// Token: 0x06004764 RID: 18276 RVA: 0x00117E08 File Offset: 0x00117208
		[SecuritySafeCritical]
		protected Effect()
		{
			SecurityHelper.DemandUIWindowPermission();
		}

		// Token: 0x06004765 RID: 18277
		internal abstract Rect GetRenderBounds(Rect contentBounds);

		/// <summary>Quando substituído em uma classe derivada, transforma a entrada do mouse e os sistemas de coordenadas por meio do efeito.</summary>
		/// <returns>A transformação a ser aplicada. O padrão é a transformação de identidade.</returns>
		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06004766 RID: 18278 RVA: 0x00117E2C File Offset: 0x0011722C
		protected internal virtual GeneralTransform EffectMapping
		{
			get
			{
				return Transform.Identity;
			}
		}

		// Token: 0x06004767 RID: 18279 RVA: 0x00117E40 File Offset: 0x00117240
		internal GeneralTransform CoerceToUnitSpaceGeneralTransform(GeneralTransform gt, Rect worldBounds)
		{
			GeneralTransform result;
			if (gt == Transform.Identity)
			{
				result = Transform.Identity;
			}
			else
			{
				if (this._mruWorldBounds != worldBounds || this._mruInnerGeneralTransform != gt)
				{
					this._mruWorldBounds = worldBounds;
					this._mruInnerGeneralTransform = gt;
					this._mruWorldSpaceGeneralTransform = new Effect.UnitSpaceCoercingGeneralTransform(worldBounds, gt);
				}
				result = this._mruWorldSpaceGeneralTransform;
			}
			return result;
		}

		// Token: 0x06004768 RID: 18280 RVA: 0x00117E98 File Offset: 0x00117298
		private static Point UnitToWorldUnsafe(Point unitPoint, Rect worldBounds)
		{
			return new Point(worldBounds.Left + unitPoint.X * worldBounds.Width, worldBounds.Top + unitPoint.Y * worldBounds.Height);
		}

		// Token: 0x06004769 RID: 18281 RVA: 0x00117ED8 File Offset: 0x001172D8
		internal static Point? UnitToWorld(Point unitPoint, Rect worldBounds)
		{
			if (!worldBounds.IsEmpty)
			{
				return new Point?(Effect.UnitToWorldUnsafe(unitPoint, worldBounds));
			}
			return null;
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x00117F04 File Offset: 0x00117304
		internal static Point? WorldToUnit(Point worldPoint, Rect worldBounds)
		{
			if (worldBounds.Width == 0.0 || worldBounds.Height == 0.0)
			{
				return null;
			}
			return new Point?(new Point((worldPoint.X - worldBounds.Left) / worldBounds.Width, (worldPoint.Y - worldBounds.Top) / worldBounds.Height));
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x00117F78 File Offset: 0x00117378
		internal static Rect UnitToWorld(Rect unitRect, Rect worldBounds)
		{
			if (!worldBounds.IsEmpty)
			{
				return new Rect(Effect.UnitToWorldUnsafe(unitRect.TopLeft, worldBounds), Effect.UnitToWorldUnsafe(unitRect.BottomRight, worldBounds));
			}
			return Rect.Empty;
		}

		// Token: 0x0600476C RID: 18284 RVA: 0x00117FB4 File Offset: 0x001173B4
		internal static Rect? WorldToUnit(Rect worldRect, Rect worldBounds)
		{
			Point? point = Effect.WorldToUnit(worldRect.TopLeft, worldBounds);
			Point? point2 = Effect.WorldToUnit(worldRect.BottomRight, worldBounds);
			if (point == null || point2 == null)
			{
				return null;
			}
			return new Rect?(new Rect(point.Value, point2.Value));
		}

		/// <summary>Cria um clone modificável deste objeto <see cref="T:System.Windows.Media.Effects.Effect" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência desse objeto, esse método copia associações de dados e referências de recurso (que não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável dessa instância. O clone retornado é efetivamente uma cópia profunda do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do clone é <see langword="false" />.</returns>
		// Token: 0x0600476D RID: 18285 RVA: 0x00118014 File Offset: 0x00117414
		public new Effect Clone()
		{
			return (Effect)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Effects.Effect" />, fazendo cópias em profundidade dos valores do objeto atual. Referências de recursos, associações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x0600476E RID: 18286 RVA: 0x0011802C File Offset: 0x0011742C
		public new Effect CloneCurrentValue()
		{
			return (Effect)base.CloneCurrentValue();
		}

		// Token: 0x0600476F RID: 18287
		internal abstract DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel);

		// Token: 0x06004770 RID: 18288 RVA: 0x00118044 File Offset: 0x00117444
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			DUCE.ResourceHandle result;
			using (CompositionEngineLock.Acquire())
			{
				result = this.AddRefOnChannelCore(channel);
			}
			return result;
		}

		// Token: 0x06004771 RID: 18289
		internal abstract void ReleaseOnChannelCore(DUCE.Channel channel);

		// Token: 0x06004772 RID: 18290 RVA: 0x0011808C File Offset: 0x0011748C
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			using (CompositionEngineLock.Acquire())
			{
				this.ReleaseOnChannelCore(channel);
			}
		}

		// Token: 0x06004773 RID: 18291
		internal abstract DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel);

		// Token: 0x06004774 RID: 18292 RVA: 0x001180D4 File Offset: 0x001174D4
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handleCore;
			using (CompositionEngineLock.Acquire())
			{
				handleCore = this.GetHandleCore(channel);
			}
			return handleCore;
		}

		// Token: 0x06004775 RID: 18293
		internal abstract int GetChannelCountCore();

		// Token: 0x06004776 RID: 18294 RVA: 0x0011811C File Offset: 0x0011751C
		int DUCE.IResource.GetChannelCount()
		{
			return this.GetChannelCountCore();
		}

		// Token: 0x06004777 RID: 18295
		internal abstract DUCE.Channel GetChannelCore(int index);

		// Token: 0x06004778 RID: 18296 RVA: 0x00118130 File Offset: 0x00117530
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this.GetChannelCore(index);
		}

		// Token: 0x04001A08 RID: 6664
		private Rect _mruWorldBounds = Rect.Empty;

		// Token: 0x04001A09 RID: 6665
		private GeneralTransform _mruInnerGeneralTransform;

		// Token: 0x04001A0A RID: 6666
		private GeneralTransform _mruWorldSpaceGeneralTransform;

		// Token: 0x020008DF RID: 2271
		private class UnitSpaceCoercingGeneralTransform : GeneralTransform
		{
			// Token: 0x0600590D RID: 22797 RVA: 0x0016958C File Offset: 0x0016898C
			public UnitSpaceCoercingGeneralTransform(Rect worldBounds, GeneralTransform innerTransform)
			{
				this._worldBounds = worldBounds;
				this._innerTransform = innerTransform;
				this._isInverse = false;
			}

			// Token: 0x1700125E RID: 4702
			// (get) Token: 0x0600590E RID: 22798 RVA: 0x001695B4 File Offset: 0x001689B4
			public override GeneralTransform Inverse
			{
				get
				{
					if (this._inverseTransform == null)
					{
						this._inverseTransform = (Effect.UnitSpaceCoercingGeneralTransform)base.Clone();
						this._inverseTransform._isInverse = !this._isInverse;
					}
					return this._inverseTransform;
				}
			}

			// Token: 0x0600590F RID: 22799 RVA: 0x001695F4 File Offset: 0x001689F4
			public override Rect TransformBounds(Rect rect)
			{
				Point point = default(Point);
				Point point2 = default(Point);
				if (!this.TryTransform(rect.TopLeft, out point) || !this.TryTransform(rect.BottomRight, out point2))
				{
					return Rect.Empty;
				}
				return new Rect(point, point2);
			}

			// Token: 0x06005910 RID: 22800 RVA: 0x00169648 File Offset: 0x00168A48
			public override bool TryTransform(Point inPoint, out Point result)
			{
				bool result2 = false;
				result = default(Point);
				Point? point = Effect.WorldToUnit(inPoint, this._worldBounds);
				if (point != null)
				{
					GeneralTransform correctInnerTransform = this.GetCorrectInnerTransform();
					Point unitPoint;
					if (correctInnerTransform.TryTransform(point.Value, out unitPoint))
					{
						Point? point2 = Effect.UnitToWorld(unitPoint, this._worldBounds);
						if (point2 != null)
						{
							result = point2.Value;
							result2 = true;
						}
					}
				}
				return result2;
			}

			// Token: 0x06005911 RID: 22801 RVA: 0x001696B4 File Offset: 0x00168AB4
			protected override Freezable CreateInstanceCore()
			{
				return new Effect.UnitSpaceCoercingGeneralTransform(this._worldBounds, this._innerTransform)
				{
					_isInverse = this._isInverse
				};
			}

			// Token: 0x06005912 RID: 22802 RVA: 0x001696E0 File Offset: 0x00168AE0
			private GeneralTransform GetCorrectInnerTransform()
			{
				GeneralTransform result;
				if (this._isInverse)
				{
					if (this._innerTransformInverse == null)
					{
						this._innerTransformInverse = this._innerTransform.Inverse;
					}
					result = this._innerTransformInverse;
				}
				else
				{
					result = this._innerTransform;
				}
				return result;
			}

			// Token: 0x0400299C RID: 10652
			private readonly Rect _worldBounds;

			// Token: 0x0400299D RID: 10653
			private readonly GeneralTransform _innerTransform;

			// Token: 0x0400299E RID: 10654
			private GeneralTransform _innerTransformInverse;

			// Token: 0x0400299F RID: 10655
			private bool _isInverse;

			// Token: 0x040029A0 RID: 10656
			private Effect.UnitSpaceCoercingGeneralTransform _inverseTransform;
		}
	}
}
