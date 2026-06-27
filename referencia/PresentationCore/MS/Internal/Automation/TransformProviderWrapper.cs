using System;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace MS.Internal.Automation
{
	// Token: 0x020007A0 RID: 1952
	internal class TransformProviderWrapper : MarshalByRefObject, ITransformProvider
	{
		// Token: 0x06005211 RID: 21009 RVA: 0x001478A4 File Offset: 0x00146CA4
		private TransformProviderWrapper(AutomationPeer peer, ITransformProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x06005212 RID: 21010 RVA: 0x001478C8 File Offset: 0x00146CC8
		public void Move(double x, double y)
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.Move), new double[]
			{
				x,
				y
			});
		}

		// Token: 0x06005213 RID: 21011 RVA: 0x001478FC File Offset: 0x00146CFC
		public void Resize(double width, double height)
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.Resize), new double[]
			{
				width,
				height
			});
		}

		// Token: 0x06005214 RID: 21012 RVA: 0x00147930 File Offset: 0x00146D30
		public void Rotate(double degrees)
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.Rotate), degrees);
		}

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x06005215 RID: 21013 RVA: 0x0014795C File Offset: 0x00146D5C
		public bool CanMove
		{
			get
			{
				return (bool)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetCanMove), null);
			}
		}

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x06005216 RID: 21014 RVA: 0x00147988 File Offset: 0x00146D88
		public bool CanResize
		{
			get
			{
				return (bool)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetCanResize), null);
			}
		}

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x06005217 RID: 21015 RVA: 0x001479B4 File Offset: 0x00146DB4
		public bool CanRotate
		{
			get
			{
				return (bool)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetCanRotate), null);
			}
		}

		// Token: 0x06005218 RID: 21016 RVA: 0x001479E0 File Offset: 0x00146DE0
		internal static object Wrap(AutomationPeer peer, object iface)
		{
			return new TransformProviderWrapper(peer, (ITransformProvider)iface);
		}

		// Token: 0x06005219 RID: 21017 RVA: 0x001479FC File Offset: 0x00146DFC
		private object Move(object arg)
		{
			double[] array = (double[])arg;
			this._iface.Move(array[0], array[1]);
			return null;
		}

		// Token: 0x0600521A RID: 21018 RVA: 0x00147A24 File Offset: 0x00146E24
		private object Resize(object arg)
		{
			double[] array = (double[])arg;
			this._iface.Resize(array[0], array[1]);
			return null;
		}

		// Token: 0x0600521B RID: 21019 RVA: 0x00147A4C File Offset: 0x00146E4C
		private object Rotate(object arg)
		{
			this._iface.Rotate((double)arg);
			return null;
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x00147A6C File Offset: 0x00146E6C
		private object GetCanMove(object unused)
		{
			return this._iface.CanMove;
		}

		// Token: 0x0600521D RID: 21021 RVA: 0x00147A8C File Offset: 0x00146E8C
		private object GetCanResize(object unused)
		{
			return this._iface.CanResize;
		}

		// Token: 0x0600521E RID: 21022 RVA: 0x00147AAC File Offset: 0x00146EAC
		private object GetCanRotate(object unused)
		{
			return this._iface.CanRotate;
		}

		// Token: 0x04002512 RID: 9490
		private AutomationPeer _peer;

		// Token: 0x04002513 RID: 9491
		private ITransformProvider _iface;
	}
}
