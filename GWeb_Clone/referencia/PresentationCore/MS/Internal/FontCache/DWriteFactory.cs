using System;
using System.IO;
using System.Security;
using MS.Internal.PresentationCore;
using MS.Internal.Text.TextInterface;

namespace MS.Internal.FontCache
{
	// Token: 0x02000770 RID: 1904
	internal static class DWriteFactory
	{
		// Token: 0x06005042 RID: 20546 RVA: 0x00141104 File Offset: 0x00140504
		[SecuritySafeCritical]
		static DWriteFactory()
		{
			DWriteFactory._factory = Factory.Create(FactoryType.Shared, new FontSourceCollectionFactory(), new FontSourceFactory());
			LocalizedErrorMsgs.EnumeratorNotStarted = SR.Get("Enumerator_NotStarted");
			LocalizedErrorMsgs.EnumeratorReachedEnd = SR.Get("Enumerator_ReachedEnd");
		}

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x06005043 RID: 20547 RVA: 0x00141154 File Offset: 0x00140554
		internal static Factory Instance
		{
			[SecurityCritical]
			get
			{
				return DWriteFactory._factory;
			}
		}

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x06005044 RID: 20548 RVA: 0x00141168 File Offset: 0x00140568
		internal static FontCollection SystemFontCollection
		{
			[SecurityCritical]
			get
			{
				if (DWriteFactory._systemFontCollection == null)
				{
					object systemFontCollectionLock = DWriteFactory._systemFontCollectionLock;
					lock (systemFontCollectionLock)
					{
						if (DWriteFactory._systemFontCollection == null)
						{
							DWriteFactory._systemFontCollection = DWriteFactory.Instance.GetSystemFontCollection();
						}
					}
				}
				return DWriteFactory._systemFontCollection;
			}
		}

		// Token: 0x06005045 RID: 20549 RVA: 0x001411D0 File Offset: 0x001405D0
		[SecurityCritical]
		private static FontCollection GetFontCollectionFromFileOrFolder(Uri fontCollectionUri, bool isFolder)
		{
			if (!Factory.IsLocalUri(fontCollectionUri))
			{
				return DWriteFactory.Instance.GetFontCollection(fontCollectionUri);
			}
			string text;
			if (!isFolder)
			{
				text = Directory.GetParent(fontCollectionUri.LocalPath).FullName + Path.DirectorySeparatorChar.ToString();
			}
			else
			{
				text = fontCollectionUri.LocalPath;
			}
			if (string.Compare((text.Length > 0 && text[text.Length - 1] != Path.DirectorySeparatorChar) ? (text + Path.DirectorySeparatorChar.ToString()) : text, Util.WindowsFontsUriObject.LocalPath, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return DWriteFactory.SystemFontCollection;
			}
			return DWriteFactory.Instance.GetFontCollection(new Uri(text));
		}

		// Token: 0x06005046 RID: 20550 RVA: 0x00141280 File Offset: 0x00140680
		[SecurityCritical]
		internal static FontCollection GetFontCollectionFromFolder(Uri fontCollectionUri)
		{
			return DWriteFactory.GetFontCollectionFromFileOrFolder(fontCollectionUri, true);
		}

		// Token: 0x06005047 RID: 20551 RVA: 0x00141294 File Offset: 0x00140694
		[SecurityCritical]
		internal static FontCollection GetFontCollectionFromFile(Uri fontCollectionUri)
		{
			return DWriteFactory.GetFontCollectionFromFileOrFolder(fontCollectionUri, false);
		}

		// Token: 0x04002482 RID: 9346
		[SecurityCritical]
		private static Factory _factory;

		// Token: 0x04002483 RID: 9347
		private static FontCollection _systemFontCollection = null;

		// Token: 0x04002484 RID: 9348
		private static object _systemFontCollectionLock = new object();
	}
}
