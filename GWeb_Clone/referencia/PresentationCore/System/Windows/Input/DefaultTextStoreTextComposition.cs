using System;
using System.Runtime.InteropServices;
using System.Security;
using MS.Win32;

namespace System.Windows.Input
{
	// Token: 0x02000234 RID: 564
	internal class DefaultTextStoreTextComposition : TextComposition
	{
		// Token: 0x06000FC2 RID: 4034 RVA: 0x0003BE08 File Offset: 0x0003B208
		[SecurityCritical]
		internal DefaultTextStoreTextComposition(InputManager inputManager, IInputElement source, string text, TextCompositionAutoComplete autoComplete) : base(inputManager, source, text, autoComplete)
		{
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0003BE20 File Offset: 0x0003B220
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public override void Complete()
		{
			UnsafeNativeMethods.ITfContext transitoryContext = this.GetTransitoryContext();
			UnsafeNativeMethods.ITfContextOwnerCompositionServices tfContextOwnerCompositionServices = transitoryContext as UnsafeNativeMethods.ITfContextOwnerCompositionServices;
			UnsafeNativeMethods.ITfCompositionView composition = this.GetComposition(transitoryContext);
			if (composition != null)
			{
				tfContextOwnerCompositionServices.TerminateComposition(composition);
				Marshal.ReleaseComObject(composition);
			}
			Marshal.ReleaseComObject(transitoryContext);
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0003BE5C File Offset: 0x0003B25C
		[SecurityCritical]
		private UnsafeNativeMethods.ITfContext GetTransitoryContext()
		{
			DefaultTextStore defaultTextStore = DefaultTextStore.Current;
			UnsafeNativeMethods.ITfDocumentMgr transitoryDocumentManager = defaultTextStore.TransitoryDocumentManager;
			UnsafeNativeMethods.ITfContext result;
			transitoryDocumentManager.GetBase(out result);
			Marshal.ReleaseComObject(transitoryDocumentManager);
			return result;
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0003BE88 File Offset: 0x0003B288
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private UnsafeNativeMethods.ITfCompositionView GetComposition(UnsafeNativeMethods.ITfContext context)
		{
			UnsafeNativeMethods.ITfCompositionView[] array = new UnsafeNativeMethods.ITfCompositionView[1];
			UnsafeNativeMethods.ITfContextComposition tfContextComposition = (UnsafeNativeMethods.ITfContextComposition)context;
			UnsafeNativeMethods.IEnumITfCompositionView enumITfCompositionView;
			tfContextComposition.EnumCompositions(out enumITfCompositionView);
			int num;
			enumITfCompositionView.Next(1, array, out num);
			Marshal.ReleaseComObject(enumITfCompositionView);
			return array[0];
		}
	}
}
