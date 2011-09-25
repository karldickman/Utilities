using System;
using System.Reflection;
using Ngol.Utilities.Reflection;

namespace Ngol.Utilities.Reflection.Extensions
{
    /// <summary>
    /// Useful extensions for working with properties through reflection.
    /// </summary>
    public static class PropertyExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="instance">
        ///
        /// </param>
        /// <param name="propertyName">
        /// A <see cref="System.String"/>
        /// </param>
        public static TPropertyType GetProperty<TInstance, TPropertyType>(this TInstance instance, string propertyName)
        {
            return (TPropertyType)GetProperty(instance, typeof(TInstance), propertyName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="instance">
        /// A <see cref="System.Object"/>
        /// </param>
        /// <param name="propertyName">
        /// A <see cref="System.String"/>
        /// </param>
        public static object GetProperty(this object instance, string propertyName)
        {
            return GetProperty(instance, instance.GetType(), propertyName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="instance">
        /// A <see cref="System.Object"/>
        /// </param>
        /// <param name="type">
        /// A <see cref="Type"/>
        /// </param>
        /// <param name="propertyName">
        /// A <see cref="System.String"/>
        /// </param>
        public static object GetProperty(this object instance, Type type, string propertyName)
        {
            return GetProperty(instance, type, propertyName, CommonBindingFlags.InstanceFlags);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="propertyName">
        /// A <see cref="System.String"/>
        /// </param>
        public static TPropertyType GetProperty<TClass, TPropertyType>(string propertyName)
        {
            return (TPropertyType)GetProperty(typeof(TClass), propertyName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type">
        /// A <see cref="Type"/>
        /// </param>
        /// <param name="propertyName">
        /// A <see cref="System.String"/>
        /// </param>
        public static TPropertyType GetProperty<TPropertyType>(this Type type, string propertyName)
        {
            return (TPropertyType)GetProperty(type, propertyName);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="type">
        /// A <see cref="Type"/>
        /// </param>
        /// <param name="propertyName">
        /// A <see cref="System.String"/>
        /// </param>
        public static object GetProperty(this Type type, string propertyName)
        {
            return GetProperty(null, type, propertyName, CommonBindingFlags.StaticFlags);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="instance">
        ///
        /// </param>
        /// <param name="propertyName">
        /// A <see cref="System.String"/>
        /// </param>
        /// <param name="value">
        ///
        /// </param>
        public static void SetProperty<TInstance, TPropertyType>(this TInstance instance, string propertyName, TPropertyType value)
        {
            SetProperty(instance, typeof(TInstance), propertyName, value);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="instance">
        /// A <see cref="System.Object"/>
        /// </param>
        /// <param name="propertyName">
        /// A <see cref="System.String"/>
        /// </param>
        /// <param name="value">
        /// A <see cref="System.Object"/>
        /// </param>
        public static void SetProperty(this object instance, string propertyName, object value)
        {
            SetProperty(instance, instance.GetType(), propertyName, value);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="instance">
        /// A <see cref="System.Object"/>
        /// </param>
        /// <param name="type">
        /// A <see cref="Type"/>
        /// </param>
        /// <param name="propertyName">
        /// A <see cref="System.String"/>
        /// </param>
        /// <param name="value">
        /// A <see cref="System.Object"/>
        /// </param>
        public static void SetProperty(this object instance, Type type, string propertyName, object value)
        {
            SetProperty(instance, type, propertyName, value, CommonBindingFlags.InstanceFlags);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="propertyName">
        /// A <see cref="System.String"/>
        /// </param>
        /// <param name="value">
        /// </param>
        public static void SetProperty<TClass, TPropertyType>(string propertyName, TPropertyType value)
        {
            SetProperty(typeof(TClass), propertyName, value);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="type">
        /// A <see cref="Type"/>
        /// </param>
        /// <param name="propertyName">
        /// A <see cref="System.String"/>
        /// </param>
        /// <param name="value">
        ///
        /// </param>
        public static void SetProperty<TPropertyType>(this Type type, string propertyName, TPropertyType value)
        {
            SetProperty(type, propertyName, value);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="type">
        /// A <see cref="Type"/>
        /// </param>
        /// <param name="propertyName">
        /// A <see cref="System.String"/>
        /// </param>
        /// <param name="value">
        /// A <see cref="System.Object"/>
        /// </param>
        public static void SetProperty(this Type type, string propertyName, object value)
        {
            SetProperty(null, type, propertyName, value, CommonBindingFlags.StaticFlags);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="instance">
        /// A <see cref="System.Object"/>
        /// </param>
        /// <param name="type">
        /// A <see cref="Type"/>
        /// </param>
        /// <param name="propertyName">
        /// A <see cref="System.String"/>
        /// </param>
        /// <param name="bindingFlags">
        /// A <see cref="BindingFlags"/>
        /// </param>
        private static object GetProperty(object instance, Type type, string propertyName, BindingFlags bindingFlags)
        {
            if(type == null)
                throw new ArgumentNullException("type");
            PropertyInfo propertyInfo = type.GetProperty(propertyName, bindingFlags);
            if(propertyInfo == null)
            {
                throw new ArgumentException(string.Format("There is no property {0} on type {1}.", propertyName, type.Name));
            }
            return propertyInfo.GetValue(instance, null);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="instance">
        /// A <see cref="System.Object"/>
        /// </param>
        /// <param name="type">
        /// A <see cref="Type"/>
        /// </param>
        /// <param name="propertyName">
        /// A <see cref="System.String"/>
        /// </param>
        /// <param name="value">
        /// A <see cref="System.Object"/>
        /// </param>
        /// <param name="bindingFlags">
        /// A <see cref="BindingFlags"/>
        /// </param>
        private static void SetProperty(object instance, Type type, string propertyName, object value, BindingFlags bindingFlags)
        {
            if(type == null)
                throw new ArgumentNullException("type");
            PropertyInfo propertyInfo = type.GetProperty(propertyName, bindingFlags);
            if(propertyInfo == null)
            {
                throw new ArgumentException(string.Format("There is no property {0} on type {1}.", propertyName, type.Name));
            }
            propertyInfo.SetValue(instance, value, null);
        }
    }
}

