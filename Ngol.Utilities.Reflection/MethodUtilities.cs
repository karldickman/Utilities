using System;
using System.Reflection;

namespace Ngol.Utilities.Reflection
{
    /// <summary>
    /// Useful extensions for working with methods.
    /// </summary>
    public static class MethodUtilities
    {
        /// <summary>
        /// The <see cref="BindingFlags" /> used to find an instance method.
        /// </summary>
        private static readonly BindingFlags InstanceFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        /// The <see cref="BindingFlags" /> used to find a static method.
        /// </summary>
        private static readonly BindingFlags StaticFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        /// Invoke a method on an instance.
        /// </summary>
        /// <param name="instance">
        /// The instance on which to invoke the method.
        /// </param>
        /// <param name="methodName">
        /// The name of the method to invoke.
        /// </param>
        /// <param name="arguments">
        /// The arguments to pass to the method.
        /// </param>
        /// <typeparam name="TInstance">
        /// The type of the <paramref name="instance"/>.
        /// </typeparam>
        /// <typeparam name="TOutput">
        /// The value expected to be returned from the function.
        /// </typeparam>
        /// <returns>
        /// The value returned by the method.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="instance"/> is <see langword="null" />.
        /// </exception>
        public static TOutput InvokeMethod<TInstance, TOutput>(this TInstance instance, string methodName, params object[] arguments)
        {
            if(instance == null)
                throw new ArgumentNullException("instance");
            return (TOutput)instance.InvokeMethod(typeof(TInstance), methodName, arguments);
        }

        /// <summary>
        /// Invoke a method on an instance.
        /// </summary>
        /// <param name="instance">
        /// The instance on which to invoke the method.
        /// </param>
        /// <param name="methodName">
        /// The name of the method to invoke.
        /// </param>
        /// <param name="arguments">
        /// The arguments to pass to the method.
        /// </param>
        /// <returns>
        /// The value returned by the method.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="instance"/> is <see langword="null" />.
        /// </exception>
        public static object InvokeMethod(this object instance, string methodName, params object[] arguments)
        {
            if(instance == null)
                throw new ArgumentNullException("instance");
            return instance.InvokeMethod(instance.GetType(), methodName, arguments);
        }

        /// <summary>
        /// Invoke a method on an instance.
        /// </summary>
        /// <param name="instance">
        /// The instance on which to invoke the method.
        /// </param>
        /// <param name="type">
        /// The of the <paramref name="instance"/>.
        /// </param>
        /// <param name="methodName">
        /// The name of the method to invoke.
        /// </param>
        /// <param name="arguments">
        /// The arguments to pass to the method.
        /// </param>
        /// <returns>
        /// The value returned by the method.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="instance"/> is <see langword="null" />.
        /// </exception>
        public static object InvokeMethod(this object instance, Type type, string methodName, params object[] arguments)
        {
            if(instance == null)
                throw new ArgumentNullException("instance");
            return InvokeMethod(instance, type, methodName, arguments, InstanceFlags);
        }

        /// <summary>
        /// Invoke a method on a class.
        /// </summary>
        /// <typeparam name="TClass">
        /// The class on which to invoke the method.
        /// </typeparam>
        /// <param name="methodName">
        /// The name of the method to invoke.
        /// </param>
        /// <param name="arguments">
        /// The arguments to pass to the method.
        /// </param>
        /// <typeparam name="TOutput">
        /// The expected return type of the method.
        /// </typeparam>
        /// <returns>
        /// The value returned by the method.
        /// </returns>
        public static TOutput InvokeMethod<TClass, TOutput>(string methodName, params object[] arguments)
        {
            return InvokeMethod<TOutput>(typeof(TClass), methodName, arguments);
        }

        /// <summary>
        /// Invoke a method on a class.
        /// </summary>
        /// <param name="type">
        /// The class on which to invoke the method.
        /// </param>
        /// <param name="methodName">
        /// The name of the method to invoke.
        /// </param>
        /// <param name="arguments">
        /// The arguments to pass to the method.
        /// </param>
        /// <typeparam name="TOutput">
        /// The expected return type of the method.
        /// </typeparam>
        /// <returns>
        /// The value returned by the method.
        /// </returns>
        public static TOutput InvokeMethod<TOutput>(this Type type, string methodName, params object[] arguments)
        {
            return (TOutput)InvokeMethod(type, methodName, arguments);
        }

        /// <summary>
        /// Invoke a method on a class.
        /// </summary>
        /// <param name="type">
        /// The class on which to invoke the method.
        /// </param>
        /// <param name="methodName">
        /// The name of the method to invoke.
        /// </param>
        /// <param name="arguments">
        /// The arguments to pass to the method.
        /// </param>
        /// <returns>
        /// The value returned by the method.
        /// </returns>
        public static object InvokeMethod(this Type type, string methodName, params object[] arguments)
        {
            return InvokeMethod(null, type, methodName, arguments, StaticFlags);
        }

        /// <summary>
        /// Invoke a method on an object.
        /// </summary>
        /// <param name="instance">
        /// The instance on which to invoke the method.
        /// Should be <see langword="null" /> if invoking a static method.
        /// </param>
        /// <param name="type">
        /// The of the <paramref name="instance"/>.
        /// </param>
        /// <param name="methodName">
        /// The name of the method to invoke.
        /// </param>
        /// <param name="arguments">
        /// The arguments to pass to the method.
        /// </param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags" /> that specify how the
        /// search for the method is conducted.
        /// </param>
        /// <returns>
        /// The return value of the method, or <see langword="null" /> in the case of a constructor.
        /// </returns>
        /// <exception cref="AmbiguousMatchException">
        /// Thrown if more than one method is found with the specified name and matching the specified binding constraints.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if
        /// <list type="bullet">
        /// <item><paramref name="bindingFlags"/> is zero.</item>
        /// <item>The elements of the <paramref name="arguments"/> array do not match the signature of the method or
        /// constructor reflected by this instance.</item>
        /// </list>
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="methodName"/> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The type that decalres the method is an open generic type.
        /// </exception>
        /// <exception cref="MethodAccessException">
        /// The caller does not have permission to execute the constructor.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// The <paramref name="instance"/> is a method builder.
        /// </exception>
        /// <exception cref="TargetException">
        /// Thrown if
        /// <list type="bullet">
        /// <item>The <paramref name="instance"/> parameter is <see langword="null" /> and the method is not static.</item>
        /// <item>The method is not declared or inherited by the <paramref name="type"/>.</item>
        /// <item>A static constructor is invoked, and <paramref name="instance"/> is neither
        /// <see langword="null" /> nor an instance of the class that declared the constructor.</item>
        /// </list>
        /// </exception>
        /// <exception cref="TargetInvocationException">
        /// Thrown if
        /// <list type="bullet">
        /// <item>The invoked method or constructor throws an exception.</item>
        /// <item>The current instance is a <see cref="System.Reflection.Emit.DynamicMethod" /> that contains unverifiable code.</item>
        /// </list>
        /// </exception>
        /// <exception cref="TargetParameterCountException">
        /// The <paramref name="arguments"/> array does not contain the correct number of arguments.
        /// </exception>
        private static object InvokeMethod(object instance, Type type, string methodName, object[] arguments, BindingFlags bindingFlags)
        {
            if(bindingFlags == 0)
                throw new ArgumentException("Cannot invoke a null method");
            MethodInfo methodInfo = type.GetMethod(methodName, bindingFlags);
            if(methodInfo == null)
            {
                throw new ArgumentException(string.Format("There is no method {0}() on {1}", methodName, type.Name));
            }
            return methodInfo.Invoke(instance, arguments);
        }
    }
}

