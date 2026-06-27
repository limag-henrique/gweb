using System;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Threading;

namespace System.Windows.Media.Media3D
{
	/// <summary>Especifica qual parte da cena 3D é processada pelo elemento <see cref="T:System.Windows.Media.Media3D.Viewport3DVisual" /> ou <see cref="T:System.Windows.Controls.Viewport3D" />.</summary>
	// Token: 0x02000454 RID: 1108
	public abstract class Camera : Animatable, IFormattable, DUCE.IResource
	{
		// Token: 0x06002DF4 RID: 11764 RVA: 0x000B7D80 File Offset: 0x000B7180
		internal Camera()
		{
		}

		// Token: 0x06002DF5 RID: 11765
		internal abstract RayHitTestParameters RayFromViewportPoint(Point point, Size viewSize, Rect3D boundingRect, out double distanceAdjustment);

		// Token: 0x06002DF6 RID: 11766
		internal abstract Matrix3D GetViewMatrix();

		// Token: 0x06002DF7 RID: 11767
		internal abstract Matrix3D GetProjectionMatrix(double aspectRatio);

		// Token: 0x06002DF8 RID: 11768 RVA: 0x000B7D94 File Offset: 0x000B7194
		internal static void PrependInverseTransform(Transform3D transform, ref Matrix3D viewMatrix)
		{
			if (transform != null && transform != Transform3D.Identity)
			{
				Camera.PrependInverseTransform(transform.Value, ref viewMatrix);
			}
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x000B7DB8 File Offset: 0x000B71B8
		internal static void PrependInverseTransform(Matrix3D matrix, ref Matrix3D viewMatrix)
		{
			if (!matrix.InvertCore())
			{
				viewMatrix = new Matrix3D(double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN);
				return;
			}
			viewMatrix.Prepend(matrix);
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.Camera" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002DFA RID: 11770 RVA: 0x000B7E74 File Offset: 0x000B7274
		public new Camera Clone()
		{
			return (Camera)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.Camera" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002DFB RID: 11771 RVA: 0x000B7E8C File Offset: 0x000B728C
		public new Camera CloneCurrentValue()
		{
			return (Camera)base.CloneCurrentValue();
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x000B7EA4 File Offset: 0x000B72A4
		private static void TransformPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			Camera camera = (Camera)d;
			Transform3D resource = (Transform3D)e.OldValue;
			Transform3D resource2 = (Transform3D)e.NewValue;
			Dispatcher dispatcher = camera.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = camera;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						camera.ReleaseResource(resource, channel);
						camera.AddRefResource(resource2, channel);
					}
				}
			}
			camera.PropertyChanged(Camera.TransformProperty);
		}

		/// <summary>Obtém ou define o Transform3D aplicado à câmera.</summary>
		/// <returns>Transform3D aplicado à câmera.</returns>
		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06002DFD RID: 11773 RVA: 0x000B7F6C File Offset: 0x000B736C
		// (set) Token: 0x06002DFE RID: 11774 RVA: 0x000B7F8C File Offset: 0x000B738C
		public Transform3D Transform
		{
			get
			{
				return (Transform3D)base.GetValue(Camera.TransformProperty);
			}
			set
			{
				base.SetValueInternal(Camera.TransformProperty, value);
			}
		}

		// Token: 0x06002DFF RID: 11775
		internal abstract DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel);

		// Token: 0x06002E00 RID: 11776 RVA: 0x000B7FA8 File Offset: 0x000B73A8
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			DUCE.ResourceHandle result;
			using (CompositionEngineLock.Acquire())
			{
				result = this.AddRefOnChannelCore(channel);
			}
			return result;
		}

		// Token: 0x06002E01 RID: 11777
		internal abstract void ReleaseOnChannelCore(DUCE.Channel channel);

		// Token: 0x06002E02 RID: 11778 RVA: 0x000B7FF0 File Offset: 0x000B73F0
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			using (CompositionEngineLock.Acquire())
			{
				this.ReleaseOnChannelCore(channel);
			}
		}

		// Token: 0x06002E03 RID: 11779
		internal abstract DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel);

		// Token: 0x06002E04 RID: 11780 RVA: 0x000B8038 File Offset: 0x000B7438
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handleCore;
			using (CompositionEngineLock.Acquire())
			{
				handleCore = this.GetHandleCore(channel);
			}
			return handleCore;
		}

		// Token: 0x06002E05 RID: 11781
		internal abstract int GetChannelCountCore();

		// Token: 0x06002E06 RID: 11782 RVA: 0x000B8080 File Offset: 0x000B7480
		int DUCE.IResource.GetChannelCount()
		{
			return this.GetChannelCountCore();
		}

		// Token: 0x06002E07 RID: 11783
		internal abstract DUCE.Channel GetChannelCore(int index);

		// Token: 0x06002E08 RID: 11784 RVA: 0x000B8094 File Offset: 0x000B7494
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this.GetChannelCore(index);
		}

		/// <summary>Cria uma representação de cadeia de caracteres desse objeto com base nas configurações de cultura atuais.</summary>
		/// <returns>A representação de cadeia de caracteres desse objeto.</returns>
		// Token: 0x06002E09 RID: 11785 RVA: 0x000B80A8 File Offset: 0x000B74A8
		public override string ToString()
		{
			base.ReadPreamble();
			return this.ConvertToString(null, null);
		}

		/// <summary>Cria uma representação de cadeia de caracteres da Câmera.</summary>
		/// <param name="provider">Informações de formatação específicas da cultura.</param>
		/// <returns>A representação de cadeia de caracteres desse objeto.</returns>
		// Token: 0x06002E0A RID: 11786 RVA: 0x000B80C4 File Offset: 0x000B74C4
		public string ToString(IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(null, provider);
		}

		/// <summary>Formata o valor da instância atual usando o formato especificado.</summary>
		/// <param name="format">O formato a ser usado.  
		///
		/// ou - 
		/// Uma referência nula (<see langword="Nothing" /> no Visual Basic) para usar o formato padrão definido para o tipo da implementação <see cref="T:System.IFormattable" />.</param>
		/// <param name="provider">O provedor a ser usado para formatar o valor.  
		///
		/// ou - 
		/// Uma referência nula (<see langword="Nothing" /> no Visual Basic) para obter as informações de formato numérico da configuração de localidade atual do sistema operacional.</param>
		/// <returns>O valor da instância atual no formato especificado.</returns>
		// Token: 0x06002E0B RID: 11787 RVA: 0x000B80E0 File Offset: 0x000B74E0
		string IFormattable.ToString(string format, IFormatProvider provider)
		{
			base.ReadPreamble();
			return this.ConvertToString(format, provider);
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x000B80FC File Offset: 0x000B74FC
		internal virtual string ConvertToString(string format, IFormatProvider provider)
		{
			return base.ToString();
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x000B8110 File Offset: 0x000B7510
		static Camera()
		{
			Type typeFromHandle = typeof(Camera);
			Camera.TransformProperty = Animatable.RegisterProperty("Transform", typeof(Transform3D), typeFromHandle, Transform3D.Identity, new PropertyChangedCallback(Camera.TransformPropertyChanged), null, false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.Camera.Transform" />.</summary>
		// Token: 0x040014E3 RID: 5347
		public static readonly DependencyProperty TransformProperty;

		// Token: 0x040014E4 RID: 5348
		internal static Transform3D s_Transform = Transform3D.Identity;
	}
}
