using System;
using System.ComponentModel;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Media.Converters;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Fornece uma implementação básica para armazenar em cache um <see cref="T:System.Windows.UIElement" />.</summary>
	// Token: 0x0200036B RID: 875
	[TypeConverter(typeof(CacheModeConverter))]
	[ValueSerializer(typeof(CacheModeValueSerializer))]
	public abstract class CacheMode : Animatable, DUCE.IResource
	{
		// Token: 0x06001EC5 RID: 7877 RVA: 0x0007C7E4 File Offset: 0x0007BBE4
		internal CacheMode()
		{
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x0007C7F8 File Offset: 0x0007BBF8
		internal static CacheMode Parse(string value)
		{
			if (value == "BitmapCache")
			{
				return new BitmapCache();
			}
			throw new FormatException(SR.Get("Parsers_IllegalToken"));
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x0007C830 File Offset: 0x0007BC30
		internal virtual bool CanSerializeToString()
		{
			return false;
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x0007C840 File Offset: 0x0007BC40
		internal virtual string ConvertToString(string format, IFormatProvider provider)
		{
			return base.ToString();
		}

		/// <summary>Cria um clone modificável do <see cref="T:System.Windows.Media.CacheMode" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência do objeto, esse método copia as expressões (que talvez não possam mais ser resolvidas), mas não as animações nem seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true." /></returns>
		// Token: 0x06001EC9 RID: 7881 RVA: 0x0007C854 File Offset: 0x0007BC54
		public new CacheMode Clone()
		{
			return (CacheMode)base.Clone();
		}

		/// <summary>Cria um clone modificável (cópia profunda) do <see cref="T:System.Windows.Media.CacheMode" /> usando seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true." /></returns>
		// Token: 0x06001ECA RID: 7882 RVA: 0x0007C86C File Offset: 0x0007BC6C
		public new CacheMode CloneCurrentValue()
		{
			return (CacheMode)base.CloneCurrentValue();
		}

		// Token: 0x06001ECB RID: 7883
		internal abstract DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel);

		// Token: 0x06001ECC RID: 7884 RVA: 0x0007C884 File Offset: 0x0007BC84
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			DUCE.ResourceHandle result;
			using (CompositionEngineLock.Acquire())
			{
				result = this.AddRefOnChannelCore(channel);
			}
			return result;
		}

		// Token: 0x06001ECD RID: 7885
		internal abstract void ReleaseOnChannelCore(DUCE.Channel channel);

		// Token: 0x06001ECE RID: 7886 RVA: 0x0007C8CC File Offset: 0x0007BCCC
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			using (CompositionEngineLock.Acquire())
			{
				this.ReleaseOnChannelCore(channel);
			}
		}

		// Token: 0x06001ECF RID: 7887
		internal abstract DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel);

		// Token: 0x06001ED0 RID: 7888 RVA: 0x0007C914 File Offset: 0x0007BD14
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handleCore;
			using (CompositionEngineLock.Acquire())
			{
				handleCore = this.GetHandleCore(channel);
			}
			return handleCore;
		}

		// Token: 0x06001ED1 RID: 7889
		internal abstract int GetChannelCountCore();

		// Token: 0x06001ED2 RID: 7890 RVA: 0x0007C95C File Offset: 0x0007BD5C
		int DUCE.IResource.GetChannelCount()
		{
			return this.GetChannelCountCore();
		}

		// Token: 0x06001ED3 RID: 7891
		internal abstract DUCE.Channel GetChannelCore(int index);

		// Token: 0x06001ED4 RID: 7892 RVA: 0x0007C970 File Offset: 0x0007BD70
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this.GetChannelCore(index);
		}
	}
}
