using System;

namespace System.Windows
{
	// Token: 0x0200018C RID: 396
	internal struct ClassHandlers
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x00015CE8 File Offset: 0x000150E8
		public override bool Equals(object o)
		{
			return this.Equals((ClassHandlers)o);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00015D04 File Offset: 0x00015104
		public bool Equals(ClassHandlers classHandlers)
		{
			return classHandlers.RoutedEvent == this.RoutedEvent && classHandlers.Handlers == this.Handlers;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00015D30 File Offset: 0x00015130
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00015D50 File Offset: 0x00015150
		public static bool operator ==(ClassHandlers classHandlers1, ClassHandlers classHandlers2)
		{
			return classHandlers1.Equals(classHandlers2);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00015D68 File Offset: 0x00015168
		public static bool operator !=(ClassHandlers classHandlers1, ClassHandlers classHandlers2)
		{
			return !classHandlers1.Equals(classHandlers2);
		}

		// Token: 0x040004BE RID: 1214
		internal RoutedEvent RoutedEvent;

		// Token: 0x040004BF RID: 1215
		internal RoutedEventHandlerInfoList Handlers;

		// Token: 0x040004C0 RID: 1216
		internal bool HasSelfHandlers;
	}
}
