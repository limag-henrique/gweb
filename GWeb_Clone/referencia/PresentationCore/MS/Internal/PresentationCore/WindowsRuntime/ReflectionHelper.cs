using System;
using System.Reflection;

namespace MS.Internal.PresentationCore.WindowsRuntime
{
	// Token: 0x020007ED RID: 2029
	internal static class ReflectionHelper
	{
		// Token: 0x06005569 RID: 21865 RVA: 0x0015F6CC File Offset: 0x0015EACC
		public static TResult ReflectionStaticCall<TResult>(this Type type, string methodName)
		{
			MethodInfo method = type.GetMethod(methodName, Type.EmptyTypes);
			if (method == null)
			{
				throw new MissingMethodException(methodName);
			}
			object obj = method.Invoke(null, null);
			return (TResult)((object)obj);
		}

		// Token: 0x0600556A RID: 21866 RVA: 0x0015F708 File Offset: 0x0015EB08
		public static TResult ReflectionStaticCall<TResult, TArg>(this Type type, string methodName, TArg arg)
		{
			MethodInfo method = type.GetMethod(methodName, new Type[]
			{
				typeof(TArg)
			});
			if (method == null)
			{
				throw new MissingMethodException(methodName);
			}
			object obj = method.Invoke(null, new object[]
			{
				arg
			});
			return (TResult)((object)obj);
		}

		// Token: 0x0600556B RID: 21867 RVA: 0x0015F760 File Offset: 0x0015EB60
		public static TResult ReflectionCall<TResult>(this object obj, string methodName)
		{
			object obj2 = obj.ReflectionCall(methodName);
			return (TResult)((object)obj2);
		}

		// Token: 0x0600556C RID: 21868 RVA: 0x0015F77C File Offset: 0x0015EB7C
		public static object ReflectionCall(this object obj, string methodName)
		{
			MethodInfo method = obj.GetType().GetMethod(methodName, Type.EmptyTypes);
			if (method == null)
			{
				throw new MissingMethodException(methodName);
			}
			return method.Invoke(obj, null);
		}

		// Token: 0x0600556D RID: 21869 RVA: 0x0015F7B8 File Offset: 0x0015EBB8
		public static object ReflectionCall<TArg1>(this object obj, string methodName, TArg1 arg1)
		{
			MethodInfo method = obj.GetType().GetMethod(methodName, new Type[]
			{
				typeof(TArg1)
			});
			if (method == null)
			{
				throw new MissingMethodException(methodName);
			}
			return method.Invoke(obj, new object[]
			{
				arg1
			});
		}

		// Token: 0x0600556E RID: 21870 RVA: 0x0015F810 File Offset: 0x0015EC10
		public static TResult ReflectionCall<TResult, TArg1>(this object obj, string methodName, TArg1 arg1)
		{
			object obj2 = obj.ReflectionCall(methodName, arg1);
			return (TResult)((object)obj2);
		}

		// Token: 0x0600556F RID: 21871 RVA: 0x0015F82C File Offset: 0x0015EC2C
		public static object ReflectionCall<TArg1, TArg2>(this object obj, string methodName, TArg1 arg1, TArg2 arg2)
		{
			MethodInfo method = obj.GetType().GetMethod(methodName, new Type[]
			{
				typeof(TArg1),
				typeof(TArg2)
			});
			if (method == null)
			{
				throw new MissingMethodException(methodName);
			}
			return method.Invoke(obj, new object[]
			{
				arg1,
				arg2
			});
		}

		// Token: 0x06005570 RID: 21872 RVA: 0x0015F898 File Offset: 0x0015EC98
		public static TResult ReflectionCall<TResult, TArg1, TArg2>(this object obj, string methodName, TArg1 arg1, TArg2 arg2)
		{
			object obj2 = obj.ReflectionCall(methodName, arg1, arg2);
			return (TResult)((object)obj2);
		}

		// Token: 0x06005571 RID: 21873 RVA: 0x0015F8B8 File Offset: 0x0015ECB8
		public static TResult ReflectionGetField<TResult>(this object obj, string fieldName)
		{
			FieldInfo field = obj.GetType().GetField(fieldName);
			if (field == null)
			{
				throw new MissingFieldException(fieldName);
			}
			object value = field.GetValue(obj);
			return (TResult)((object)value);
		}

		// Token: 0x06005572 RID: 21874 RVA: 0x0015F8F0 File Offset: 0x0015ECF0
		public static object ReflectionNew(this Type type)
		{
			ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
			if (constructor == null)
			{
				string message = type.FullName + "." + type.Name + "()";
				throw new MissingMethodException(message);
			}
			return constructor.Invoke(null);
		}

		// Token: 0x06005573 RID: 21875 RVA: 0x0015F93C File Offset: 0x0015ED3C
		public static object ReflectionNew<TArg1>(this Type type, TArg1 arg1)
		{
			ConstructorInfo constructor = type.GetConstructor(new Type[]
			{
				typeof(TArg1)
			});
			if (constructor == null)
			{
				string message = string.Format("{0}.{1}({2})", type.FullName, type.Name, typeof(TArg1).Name);
				throw new MissingMethodException(message);
			}
			return constructor.Invoke(new object[]
			{
				arg1
			});
		}

		// Token: 0x06005574 RID: 21876 RVA: 0x0015F9B0 File Offset: 0x0015EDB0
		public static object ReflectionNew<TArg1, TArg2>(this Type type, TArg1 arg1, TArg2 arg2)
		{
			ConstructorInfo constructor = type.GetConstructor(new Type[]
			{
				typeof(TArg1),
				typeof(TArg2)
			});
			if (constructor == null)
			{
				string message = string.Format("{0}.{1}({2},{3})", new object[]
				{
					type.FullName,
					type.Name,
					typeof(TArg1).Name,
					typeof(TArg2).Name
				});
				throw new MissingMethodException(message);
			}
			return constructor.Invoke(new object[]
			{
				arg1,
				arg2
			});
		}

		// Token: 0x06005575 RID: 21877 RVA: 0x0015FA5C File Offset: 0x0015EE5C
		public static TResult ReflectionGetProperty<TResult>(this object obj, string propertyName)
		{
			Type type = obj.GetType();
			PropertyInfo property = type.GetProperty(propertyName);
			if (property == null)
			{
				throw new MissingMemberException(propertyName);
			}
			return (TResult)((object)property.GetValue(obj));
		}

		// Token: 0x06005576 RID: 21878 RVA: 0x0015FA94 File Offset: 0x0015EE94
		public static object ReflectionGetProperty(this object obj, string propertyName)
		{
			return obj.ReflectionGetProperty(propertyName);
		}

		// Token: 0x06005577 RID: 21879 RVA: 0x0015FAA8 File Offset: 0x0015EEA8
		public static TResult ReflectionStaticGetProperty<TResult>(this Type type, string propertyName)
		{
			PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Static);
			if (property == null)
			{
				throw new MissingMemberException(propertyName);
			}
			return (TResult)((object)property.GetValue(null));
		}
	}
}
