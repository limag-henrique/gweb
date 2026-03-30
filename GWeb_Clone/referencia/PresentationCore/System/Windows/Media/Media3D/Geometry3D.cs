using System;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal.Media3D;

namespace System.Windows.Media.Media3D
{
	/// <summary>As classes derivadas dessa classe base abstrata definem formas geométricas 3D. A classe de objetos <see cref="T:System.Windows.Media.Media3D.Geometry3D" /> pode ser usada para teste de acertos e renderização dos dados gráficos 3D.</summary>
	// Token: 0x0200045D RID: 1117
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public abstract class Geometry3D : Animatable, DUCE.IResource
	{
		// Token: 0x06002E77 RID: 11895 RVA: 0x000B9468 File Offset: 0x000B8868
		internal Geometry3D()
		{
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Media.Media3D.Rect3D" /> que especifica a caixa delimitadora alinhada por eixo desse <see cref="T:System.Windows.Media.Media3D.Geometry3D" />.</summary>
		/// <returns>Delimitação <see cref="T:System.Windows.Media.Media3D.Rect3D" /> para o <see cref="T:System.Windows.Media.Media3D.Geometry3D" />.</returns>
		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06002E78 RID: 11896
		public abstract Rect3D Bounds { get; }

		// Token: 0x06002E79 RID: 11897 RVA: 0x000B947C File Offset: 0x000B887C
		internal void RayHitTest(RayHitTestParameters rayParams, FaceType facesToHit)
		{
			Rect3D bounds = this.Bounds;
			if (bounds.IsEmpty)
			{
				return;
			}
			Point3D point3D;
			Vector3D vector3D;
			rayParams.GetLocalLine(out point3D, out vector3D);
			if (LineUtil.ComputeLineBoxIntersection(ref point3D, ref vector3D, ref bounds, rayParams.IsRay))
			{
				this.RayHitTestCore(rayParams, facesToHit);
			}
		}

		// Token: 0x06002E7A RID: 11898
		internal abstract void RayHitTestCore(RayHitTestParameters rayParams, FaceType hitTestableFaces);

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.Geometry3D" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002E7B RID: 11899 RVA: 0x000B94C0 File Offset: 0x000B88C0
		public new Geometry3D Clone()
		{
			return (Geometry3D)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.Geometry3D" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002E7C RID: 11900 RVA: 0x000B94D8 File Offset: 0x000B88D8
		public new Geometry3D CloneCurrentValue()
		{
			return (Geometry3D)base.CloneCurrentValue();
		}

		// Token: 0x06002E7D RID: 11901
		internal abstract DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel);

		// Token: 0x06002E7E RID: 11902 RVA: 0x000B94F0 File Offset: 0x000B88F0
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			DUCE.ResourceHandle result;
			using (CompositionEngineLock.Acquire())
			{
				result = this.AddRefOnChannelCore(channel);
			}
			return result;
		}

		// Token: 0x06002E7F RID: 11903
		internal abstract void ReleaseOnChannelCore(DUCE.Channel channel);

		// Token: 0x06002E80 RID: 11904 RVA: 0x000B9538 File Offset: 0x000B8938
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			using (CompositionEngineLock.Acquire())
			{
				this.ReleaseOnChannelCore(channel);
			}
		}

		// Token: 0x06002E81 RID: 11905
		internal abstract DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel);

		// Token: 0x06002E82 RID: 11906 RVA: 0x000B9580 File Offset: 0x000B8980
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handleCore;
			using (CompositionEngineLock.Acquire())
			{
				handleCore = this.GetHandleCore(channel);
			}
			return handleCore;
		}

		// Token: 0x06002E83 RID: 11907
		internal abstract int GetChannelCountCore();

		// Token: 0x06002E84 RID: 11908 RVA: 0x000B95C8 File Offset: 0x000B89C8
		int DUCE.IResource.GetChannelCount()
		{
			return this.GetChannelCountCore();
		}

		// Token: 0x06002E85 RID: 11909
		internal abstract DUCE.Channel GetChannelCore(int index);

		// Token: 0x06002E86 RID: 11910 RVA: 0x000B95DC File Offset: 0x000B89DC
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this.GetChannelCore(index);
		}
	}
}
