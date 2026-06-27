using System;
using System.Diagnostics;

namespace MS.Internal
{
	// Token: 0x020006A4 RID: 1700
	internal static class TraceAnimation
	{
		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06004A76 RID: 19062 RVA: 0x001224BC File Offset: 0x001218BC
		public static AvTraceDetails StoryboardBegin
		{
			get
			{
				if (TraceAnimation._StoryboardBegin == null)
				{
					TraceAnimation._StoryboardBegin = new AvTraceDetails(1, new string[]
					{
						"Storyboard has begun",
						"Storyboard",
						"StoryboardName",
						"TargetElement",
						"NameScope"
					});
				}
				return TraceAnimation._StoryboardBegin;
			}
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06004A77 RID: 19063 RVA: 0x00122510 File Offset: 0x00121910
		public static AvTraceDetails StoryboardPause
		{
			get
			{
				if (TraceAnimation._StoryboardPause == null)
				{
					TraceAnimation._StoryboardPause = new AvTraceDetails(2, new string[]
					{
						"Storyboard has been paused",
						"Storyboard",
						"StoryboardName",
						"TargetElement"
					});
				}
				return TraceAnimation._StoryboardPause;
			}
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06004A78 RID: 19064 RVA: 0x0012255C File Offset: 0x0012195C
		public static AvTraceDetails StoryboardRemove
		{
			get
			{
				if (TraceAnimation._StoryboardRemove == null)
				{
					TraceAnimation._StoryboardRemove = new AvTraceDetails(3, new string[]
					{
						"Storyboard has been removed",
						"Storyboard",
						"StoryboardName",
						"TargetElement"
					});
				}
				return TraceAnimation._StoryboardRemove;
			}
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06004A79 RID: 19065 RVA: 0x001225A8 File Offset: 0x001219A8
		public static AvTraceDetails StoryboardResume
		{
			get
			{
				if (TraceAnimation._StoryboardResume == null)
				{
					TraceAnimation._StoryboardResume = new AvTraceDetails(4, new string[]
					{
						"Storyboard has been resumed",
						"Storyboard",
						"StoryboardName",
						"TargetElement"
					});
				}
				return TraceAnimation._StoryboardResume;
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06004A7A RID: 19066 RVA: 0x001225F4 File Offset: 0x001219F4
		public static AvTraceDetails StoryboardStop
		{
			get
			{
				if (TraceAnimation._StoryboardStop == null)
				{
					TraceAnimation._StoryboardStop = new AvTraceDetails(5, new string[]
					{
						"Storyboard has been stopped",
						"Storyboard",
						"StoryboardName",
						"TargetElement"
					});
				}
				return TraceAnimation._StoryboardStop;
			}
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06004A7B RID: 19067 RVA: 0x00122640 File Offset: 0x00121A40
		public static AvTraceDetails StoryboardNotApplied
		{
			get
			{
				if (TraceAnimation._StoryboardNotApplied == null)
				{
					TraceAnimation._StoryboardNotApplied = new AvTraceDetails(6, new string[]
					{
						"Unable to perform action because the specified Storyboard was never applied to this object for interactive control.",
						"Action",
						"Storyboard",
						"TargetElement"
					});
				}
				return TraceAnimation._StoryboardNotApplied;
			}
		}

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06004A7C RID: 19068 RVA: 0x0012268C File Offset: 0x00121A8C
		public static AvTraceDetails AnimateStorageValidationFailed
		{
			get
			{
				if (TraceAnimation._AnimateStorageValidationFailed == null)
				{
					TraceAnimation._AnimateStorageValidationFailed = new AvTraceDetails(7, new string[]
					{
						"Animated property failed validation. Animated value not set.",
						"AnimationStorage",
						"AnimatedValue",
						"AnimatedObject",
						"AnimatedProperty"
					});
				}
				return TraceAnimation._AnimateStorageValidationFailed;
			}
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06004A7D RID: 19069 RVA: 0x001226E0 File Offset: 0x00121AE0
		public static AvTraceDetails AnimateStorageValidationNoLongerFailing
		{
			get
			{
				if (TraceAnimation._AnimateStorageValidationNoLongerFailing == null)
				{
					TraceAnimation._AnimateStorageValidationNoLongerFailing = new AvTraceDetails(8, new string[]
					{
						"Animated property no longer failing validation.",
						"AnimationStorage",
						"AnimatedValue",
						"AnimatedObject",
						"AnimatedProperty"
					});
				}
				return TraceAnimation._AnimateStorageValidationNoLongerFailing;
			}
		}

		// Token: 0x06004A7E RID: 19070 RVA: 0x00122734 File Offset: 0x00121B34
		public static void Trace(TraceEventType type, AvTraceDetails traceDetails, params object[] parameters)
		{
			TraceAnimation._avTrace.Trace(type, traceDetails.Id, traceDetails.Message, traceDetails.Labels, parameters);
		}

		// Token: 0x06004A7F RID: 19071 RVA: 0x00122760 File Offset: 0x00121B60
		public static void Trace(TraceEventType type, AvTraceDetails traceDetails)
		{
			TraceAnimation._avTrace.Trace(type, traceDetails.Id, traceDetails.Message, traceDetails.Labels, new object[0]);
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x00122790 File Offset: 0x00121B90
		public static void Trace(TraceEventType type, AvTraceDetails traceDetails, object p1)
		{
			TraceAnimation._avTrace.Trace(type, traceDetails.Id, traceDetails.Message, traceDetails.Labels, new object[]
			{
				p1
			});
		}

		// Token: 0x06004A81 RID: 19073 RVA: 0x001227C4 File Offset: 0x00121BC4
		public static void Trace(TraceEventType type, AvTraceDetails traceDetails, object p1, object p2)
		{
			TraceAnimation._avTrace.Trace(type, traceDetails.Id, traceDetails.Message, traceDetails.Labels, new object[]
			{
				p1,
				p2
			});
		}

		// Token: 0x06004A82 RID: 19074 RVA: 0x001227FC File Offset: 0x00121BFC
		public static void Trace(TraceEventType type, AvTraceDetails traceDetails, object p1, object p2, object p3)
		{
			TraceAnimation._avTrace.Trace(type, traceDetails.Id, traceDetails.Message, traceDetails.Labels, new object[]
			{
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004A83 RID: 19075 RVA: 0x0012283C File Offset: 0x00121C3C
		public static void TraceActivityItem(AvTraceDetails traceDetails, params object[] parameters)
		{
			TraceAnimation._avTrace.TraceStartStop(traceDetails.Id, traceDetails.Message, traceDetails.Labels, parameters);
		}

		// Token: 0x06004A84 RID: 19076 RVA: 0x00122868 File Offset: 0x00121C68
		public static void TraceActivityItem(AvTraceDetails traceDetails)
		{
			TraceAnimation._avTrace.TraceStartStop(traceDetails.Id, traceDetails.Message, traceDetails.Labels, new object[0]);
		}

		// Token: 0x06004A85 RID: 19077 RVA: 0x00122898 File Offset: 0x00121C98
		public static void TraceActivityItem(AvTraceDetails traceDetails, object p1)
		{
			TraceAnimation._avTrace.TraceStartStop(traceDetails.Id, traceDetails.Message, traceDetails.Labels, new object[]
			{
				p1
			});
		}

		// Token: 0x06004A86 RID: 19078 RVA: 0x001228CC File Offset: 0x00121CCC
		public static void TraceActivityItem(AvTraceDetails traceDetails, object p1, object p2)
		{
			TraceAnimation._avTrace.TraceStartStop(traceDetails.Id, traceDetails.Message, traceDetails.Labels, new object[]
			{
				p1,
				p2
			});
		}

		// Token: 0x06004A87 RID: 19079 RVA: 0x00122904 File Offset: 0x00121D04
		public static void TraceActivityItem(AvTraceDetails traceDetails, object p1, object p2, object p3)
		{
			TraceAnimation._avTrace.TraceStartStop(traceDetails.Id, traceDetails.Message, traceDetails.Labels, new object[]
			{
				p1,
				p2,
				p3
			});
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06004A88 RID: 19080 RVA: 0x00122940 File Offset: 0x00121D40
		public static bool IsEnabled
		{
			get
			{
				return TraceAnimation._avTrace != null && TraceAnimation._avTrace.IsEnabled;
			}
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06004A89 RID: 19081 RVA: 0x00122960 File Offset: 0x00121D60
		public static bool IsEnabledOverride
		{
			get
			{
				return TraceAnimation._avTrace.IsEnabledOverride;
			}
		}

		// Token: 0x06004A8A RID: 19082 RVA: 0x00122978 File Offset: 0x00121D78
		public static void Refresh()
		{
			TraceAnimation._avTrace.Refresh();
		}

		// Token: 0x04001F84 RID: 8068
		private static AvTrace _avTrace = new AvTrace(() => PresentationTraceSources.AnimationSource, delegate()
		{
			PresentationTraceSources._AnimationSource = null;
		});

		// Token: 0x04001F85 RID: 8069
		private static AvTraceDetails _StoryboardBegin;

		// Token: 0x04001F86 RID: 8070
		private static AvTraceDetails _StoryboardPause;

		// Token: 0x04001F87 RID: 8071
		private static AvTraceDetails _StoryboardRemove;

		// Token: 0x04001F88 RID: 8072
		private static AvTraceDetails _StoryboardResume;

		// Token: 0x04001F89 RID: 8073
		private static AvTraceDetails _StoryboardStop;

		// Token: 0x04001F8A RID: 8074
		private static AvTraceDetails _StoryboardNotApplied;

		// Token: 0x04001F8B RID: 8075
		private static AvTraceDetails _AnimateStorageValidationFailed;

		// Token: 0x04001F8C RID: 8076
		private static AvTraceDetails _AnimateStorageValidationNoLongerFailing;
	}
}
