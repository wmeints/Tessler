using System;
using System.Globalization;

namespace InfoSupport.Tessler.Util
{
    public static class Guard
    {
        /// <summary>
        /// Ensures that the provided parameter is not null.
        /// </summary>
        /// <param name="parameter">The object to validate and test for a null value.</param>
        /// <param name="parameterName">The name of the parameter passed into the calling method.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the provided <paramref name="parameter"/> is null.
        /// The <paramref name="parameterName"/>is passed into the ArgumentNullException </exception>
        /// <example>This sample ensures that the dependencyResolver and assembliesProvider parameters are not null
        /// before setting the class's properties to the provided values.
        /// <code>
        /// public ApplicationBootstrapper(IDependencyResolver dependencyResolver, IAssembliesProvider assembliesProvider)
        /// {
        ///     Guard.ArgumentNotNull(dependencyResolver, "dependencyResolver");
        ///     Guard.ArgumentNotNull(assembliesProvider, "assembliesProvider");
        ///
        ///     ...
        /// }
        /// </code>
        /// </example>
        public static void ArgumentNotNull(object parameter, string parameterName)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// Ensures that the provided string parameter is not null nor empty.
        /// </summary>
        /// <param name="parameter">The object to validate and test for a null or empty value.</param>
        /// <param name="parameterName">The name of the parameter passed into the calling method.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the provided <paramref name="parameter"/> is null.
        /// The <paramref name="parameterName"/>is passed into the ArgumentNullException. </exception>
        /// <exception cref="System.ArgumentException">Thrown when the provided <paramref name="parameter"/> is empty.
        /// The <paramref name="parameterName"/>is passed into the ArgumentException. </exception>
        /// <example>This sample ensures that the userName and userToFollow parameters are not null nor empty.
        /// <code>
        /// public void Follow(string userName, string userToFollow)
        /// {
        ///     Guard.ArgumentNotNullOrEmpty(userName, "userName");
        ///     Guard.ArgumentNotNullOrEmpty(userToFollow, "userToFollow");
        ///
        ///     ...
        /// }
        /// </code>
        /// </example>
        public static void ArgumentNotNullOrEmpty(string parameter, string parameterName)
        {
            ArgumentNotNull(parameter, parameterName);
            if (string.IsNullOrEmpty(parameter))
            {
                throw new ArgumentException("Parameter cannot be empty", parameterName);
            }
        }

        /// <summary>
        /// Ensures that the provided integer parameter is between the minimum and maximum values.
        /// </summary>
        /// <param name="parameter">The number to validate.</param>
        /// <param name="minimumValue">The minimum value allowed for the <paramref name="parameter"/>.</param>
        /// <param name="maximumValue">The maximum value allowed for the <paramref name="parameter"/>.</param>
        /// <param name="parameterName">The name of the parameter passed into the calling method.</param>
        /// <exception cref="System.ArgumentException">Thrown when the provided <paramref name="parameter"/> is
        /// less than the <paramref name="minimumValue"/> or greater than the <paramref name="maximumValue"/>.
        /// The <paramref name="parameterName"/>is passed into the ArgumentException. </exception>
        /// <exception cref="System.ArgumentException">Thrown when the provided <paramref name="parameter"/> is empty.
        /// The <paramref name="parameterName"/>is passed into the ArgumentException. </exception>
        /// <example>This sample ensures that page is between 1 and Int32.MaxSize (inclusive) and that pageSize
        /// is between 1 and 100 (inclusive).
        /// <code>
        /// public void SomeMethod(int page, int pageSize)
        /// {
        ///     Guard.ArgumentInRange(page, 1, Int32.MaxValue, "page");
        ///     Guard.ArgumentInRange(pageSize, 1, 100, "pageSize");
        ///     ...
        /// }
        /// </code>
        /// </example>
        public static void ArgumentInRange(int parameter, int minimumValue, int maximumValue, string parameterName)
        {
            if ((parameter < minimumValue) || (parameter > maximumValue))
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "The value for '{0}' must be greater than or equal to {1} and less than or equal to {2}",
                        parameterName,
                        minimumValue,
                        maximumValue),
                    parameterName);
            }
        }
    }
}