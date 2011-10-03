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
        /// Get the value of the instance property with the specified name.
        /// </summary>
        /// <param name="instance">
        /// The instance on which to get the property value.
        /// </param>
        /// <param name="propertyName">
        /// The name of the property whose value to get.
        /// </param>
        /// <returns>
        /// The value of the property that matches the specified requirements, if found.
        /// </returns>
        /// <exception cref="AmbiguousMatchException">
        /// More than one static property is found with the specified name.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="type"/> or <paramref name="propertyName"/> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if
        /// <list type="bullet">
        /// <item>
        /// No property was found matching the specified requirements.
        /// </item>
        /// <item>
        /// The property's get accessor was not found.
        /// </item>
        /// </list>
        /// </exception>
        /// <exception cref="MethodAccessException">
        /// There was an illegal attempt to access a private or protected method inside a class.
        /// </exception>
        /// <exception cref="TargetInvocationException">
        /// An error occurred while retrieving the property value.  The <paramref name="Exception.InnerException"/>
        /// property indicates the reason for the error.
        /// </exception>
        public static object GetProperty(this object instance, string propertyName)
        {
            if(instance == null)
                throw new ArgumentNullException("instance");
            return GetProperty(instance, instance.GetType(), propertyName, CommonBindingFlags.InstanceFlags);
        }

        /// <summary>
        /// Get the value of the static property with the specified name.
        /// </summary>
        /// <param name="type">
        /// The <see cref="Type"/> which defines the property.
        /// </param>
        /// <param name="propertyName">
        /// The name of the property whose value to get.
        /// </param>
        /// <returns>
        /// The value of the property that matches the specified requirements, if found.
        /// </returns>
        /// <exception cref="AmbiguousMatchException">
        /// More than one static property is found with the specified name.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="type"/> or <paramref name="propertyName"/> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if
        /// <list type="bullet">
        /// <item>
        /// No property was found matching the specified requirements.
        /// </item>
        /// <item>
        /// The property's get accessor was not found.
        /// </item>
        /// </list>
        /// </exception>
        /// <exception cref="MethodAccessException">
        /// There was an illegal attempt to access a private or protected method inside a class.
        /// </exception>
        /// <exception cref="TargetInvocationException">
        /// An error occurred while retrieving the property value.  The <paramref name="Exception.InnerException"/>
        /// property indicates the reason for the error.
        /// </exception>
        public static object GetProperty(this Type type, string propertyName)
        {
            return GetProperty(null, type, propertyName, CommonBindingFlags.StaticFlags);
        }

        /// <summary>
        /// Set the value of the instance property with the specified name.
        /// </summary>
        /// <param name="instance">
        /// The instance on which to get the property value.
        /// </param>
        /// <param name="propertyName">
        /// The name of the property whose value to set.
        /// </param>
        /// <param name="value">
        /// The value to apply to the property.
        /// </param>
        /// <exception cref="AmbiguousMatchException">
        /// More than one instance property is found with the specified name.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if
        /// <list type="bullet">
        /// <item>
        /// No property was found matching the specified requirements.
        /// </item>
        /// <item>
        /// The property's set accessor was not found.
        /// </item>
        /// </list>
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="propertyName"/> is <see langword="null" />.
        /// </exception>
        /// <exception cref="MethodAccessException">
        /// There was an illegal attempt to access a private or protected method inside a class.
        /// </exception>
        /// <exception cref="TargetInvocationException">
        /// An error occurred while setting the property value.  The <paramref name="Exception.InnerException"/>
        /// property indicates the reason for the error.
        /// </exception>
        public static void SetProperty(this object instance, string propertyName, object value)
        {
            if(instance == null)
                throw new ArgumentNullException("instance");
            SetProperty(instance, instance.GetType(), propertyName, value, CommonBindingFlags.InstanceFlags);
        }

        /// <summary>
        /// Set the value of the static property with the specified name.
        /// </summary>
        /// <param name="type">
        /// The <see cref="Type"/> which defines the property.
        /// </param>
        /// <param name="propertyName">
        /// The name of the property whose value to set.
        /// </param>
        /// <param name="value">
        /// The value to apply to the property.
        /// </param>
        /// <exception cref="AmbiguousMatchException">
        /// More than one static property is found with the specified name.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="type"/> or <paramref name="propertyName"/> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if
        /// <list type="bullet">
        /// <item>
        /// No property was found matching the specified requirements.
        /// </item>
        /// <item>
        /// The property's set accessor was not found.
        /// </item>
        /// </list>
        /// </exception>
        /// <exception cref="MethodAccessException">
        /// There was an illegal attempt to access a private or protected method inside a class.
        /// </exception>
        /// <exception cref="TargetInvocationException">
        /// An error occurred while setting the property value.  The <paramref name="Exception.InnerException"/>
        /// property indicates the reason for the error.
        /// </exception>
        public static void SetProperty(this Type type, string propertyName, object value)
        {
            SetProperty(null, type, propertyName, value, CommonBindingFlags.StaticFlags);
        }

        /// <summary>
        /// Get the value in a property with the specified name.
        /// </summary>
        /// <param name="instance">
        /// The instance on which to get the property value.  Should be
        /// <see langword="null" /> if the property is a static one.
        /// </param>
        /// <param name="type">
        /// The <see cref="Type" /> which defines the property.
        /// </param>
        /// <param name="propertyName">
        /// The name of the property whose value to get.
        /// </param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags" /> that specify how the search for the property is conducted.
        /// </param>
        /// <returns>
        /// The value of the property that matches the specified requirements, if found.
        /// </returns>
        /// <exception cref="AmbiguousMatchException">
        /// More than one property is found with the specified name and matching the specified binding constraints.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="type"/> or <paramref name="propertyName"/> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if
        /// <list type="bullet">
        /// <item>
        /// No property was found matching the specified requirements.
        /// </item>
        /// <item>
        /// The property's get accessor was not found.
        /// </item>
        /// </list>
        /// </exception>
        /// <exception cref="MethodAccessException">
        /// There was an illegal attempt to access a private or protected method inside a class.
        /// </exception>
        /// <exception cref="TargetInvocationException">
        /// An error occurred while retrieving the property value.  The <paramref name="Exception.InnerException"/>
        /// property indicates the reason for the error.
        /// </exception>
        private static object GetProperty(object instance, Type type, string propertyName, BindingFlags bindingFlags)
        {
            if(type == null)
                throw new ArgumentNullException("type");
            PropertyInfo propertyInfo = type.GetProperty(propertyName, bindingFlags);
            if(propertyInfo == null)
                throw new ArgumentException(string.Format("There is no property {0} on type {1}.", propertyName, type.Name));
            return propertyInfo.GetValue(instance, null);
        }

        /// <summary>
        /// Set the value in a property with the specified name.
        /// </summary>
        /// <param name="instance">
        /// The instance on which to get the property value.  Should be
        /// <see langword="null" /> if the property is a static one.
        /// </param>
        /// <param name="type">
        /// The <see cref="Type" /> which defines the property.
        /// </param>
        /// <param name="propertyName">
        /// The name of the property to set.
        /// </param>
        /// <param name="value">
        /// The value to store to the property.
        /// </param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags" /> that specify how the search for the property is conducted.
        /// </param>
        /// <exception cref="AmbiguousMatchException">
        /// More than one property is found with the specified name and matching the specified binding constraints.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="type"/> or <paramref name="propertyName"/> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if
        /// <list type="bullet">
        /// <item>
        /// No property was found matching the specified requirements.
        /// </item>
        /// <item>
        /// The property's set accessor was not found.
        /// </item>
        /// </list>
        /// </exception>
        /// <exception cref="MethodAccessException">
        /// There was an illegal attempt to access a private or protected method inside a class.
        /// </exception>
        /// <exception cref="TargetInvocationException">
        /// An error occurred while setting the property value.  The <paramref name="Exception.InnerException"/>
        /// property indicates the reason for the error.
        /// </exception>
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

