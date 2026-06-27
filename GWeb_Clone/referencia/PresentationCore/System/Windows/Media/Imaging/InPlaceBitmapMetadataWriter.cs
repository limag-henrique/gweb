using System;
using System.Security;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media.Imaging
{
	/// <summary>Permite atualizações in-loco de blocos existentes de <see cref="T:System.Windows.Media.Imaging.BitmapMetadata" />.</summary>
	// Token: 0x020005EF RID: 1519
	public sealed class InPlaceBitmapMetadataWriter : BitmapMetadata
	{
		// Token: 0x060045A5 RID: 17829 RVA: 0x0010F8C8 File Offset: 0x0010ECC8
		private InPlaceBitmapMetadataWriter()
		{
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x0010F8DC File Offset: 0x0010ECDC
		[SecurityCritical]
		internal InPlaceBitmapMetadataWriter(SafeMILHandle fmeHandle, SafeMILHandle metadataHandle, object syncObject) : base(metadataHandle, false, false, syncObject)
		{
			this._fmeHandle = fmeHandle;
		}

		// Token: 0x060045A7 RID: 17831 RVA: 0x0010F8FC File Offset: 0x0010ECFC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static InPlaceBitmapMetadataWriter CreateFromFrameDecode(BitmapSourceSafeMILHandle frameHandle, object syncObject)
		{
			Invariant.Assert(frameHandle != null);
			SafeMILHandle safeMILHandle = null;
			SafeMILHandle metadataHandle = null;
			using (FactoryMaker factoryMaker = new FactoryMaker())
			{
				lock (syncObject)
				{
					HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreateFastMetadataEncoderFromFrameDecode(factoryMaker.ImagingFactoryPtr, frameHandle, out safeMILHandle));
				}
			}
			HRESULT.Check(UnsafeNativeMethods.WICFastMetadataEncoder.GetMetadataQueryWriter(safeMILHandle, out metadataHandle));
			return new InPlaceBitmapMetadataWriter(safeMILHandle, metadataHandle, syncObject);
		}

		// Token: 0x060045A8 RID: 17832 RVA: 0x0010F9A0 File Offset: 0x0010EDA0
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static InPlaceBitmapMetadataWriter CreateFromDecoder(SafeMILHandle decoderHandle, object syncObject)
		{
			Invariant.Assert(decoderHandle != null);
			SafeMILHandle safeMILHandle = null;
			SafeMILHandle metadataHandle = null;
			using (FactoryMaker factoryMaker = new FactoryMaker())
			{
				lock (syncObject)
				{
					HRESULT.Check(UnsafeNativeMethods.WICImagingFactory.CreateFastMetadataEncoderFromDecoder(factoryMaker.ImagingFactoryPtr, decoderHandle, out safeMILHandle));
				}
			}
			HRESULT.Check(UnsafeNativeMethods.WICFastMetadataEncoder.GetMetadataQueryWriter(safeMILHandle, out metadataHandle));
			return new InPlaceBitmapMetadataWriter(safeMILHandle, metadataHandle, syncObject);
		}

		/// <summary>Obtém um valor que indica se os metadados de imagem podem ser salvos com êxito.</summary>
		/// <returns>
		///   <see langword="true" /> se os metadados de bitmap podem ser gravados com êxito; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060045A9 RID: 17833 RVA: 0x0010FA44 File Offset: 0x0010EE44
		[SecurityCritical]
		public bool TrySave()
		{
			Invariant.Assert(this._fmeHandle != null);
			object syncObject = base.SyncObject;
			int hr;
			lock (syncObject)
			{
				hr = UnsafeNativeMethods.WICFastMetadataEncoder.Commit(this._fmeHandle);
			}
			return HRESULT.Succeeded(hr);
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Imaging.InPlaceBitmapMetadataWriter" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060045AA RID: 17834 RVA: 0x0010FAAC File Offset: 0x0010EEAC
		public new InPlaceBitmapMetadataWriter Clone()
		{
			return (InPlaceBitmapMetadataWriter)base.Clone();
		}

		// Token: 0x060045AB RID: 17835 RVA: 0x0010FAC4 File Offset: 0x0010EEC4
		protected override Freezable CreateInstanceCore()
		{
			throw new InvalidOperationException(SR.Get("Image_InplaceMetadataNoCopy"));
		}

		// Token: 0x060045AC RID: 17836 RVA: 0x0010FAE0 File Offset: 0x0010EEE0
		protected override void CloneCore(Freezable sourceFreezable)
		{
			throw new InvalidOperationException(SR.Get("Image_InplaceMetadataNoCopy"));
		}

		// Token: 0x060045AD RID: 17837 RVA: 0x0010FAFC File Offset: 0x0010EEFC
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			throw new InvalidOperationException(SR.Get("Image_InplaceMetadataNoCopy"));
		}

		// Token: 0x060045AE RID: 17838 RVA: 0x0010FB18 File Offset: 0x0010EF18
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			throw new InvalidOperationException(SR.Get("Image_InplaceMetadataNoCopy"));
		}

		// Token: 0x060045AF RID: 17839 RVA: 0x0010FB34 File Offset: 0x0010EF34
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			throw new InvalidOperationException(SR.Get("Image_InplaceMetadataNoCopy"));
		}

		// Token: 0x04001947 RID: 6471
		[SecurityCritical]
		private SafeMILHandle _fmeHandle;
	}
}
