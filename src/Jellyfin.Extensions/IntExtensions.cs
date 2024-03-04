using System;
using System.Text.RegularExpressions;

namespace Jellyfin.Extensions
{
    /// <summary>
    /// Provides extensions methods for <see cref="string" />.
    /// </summary>
    public static partial class IntExtensions
    {
        /// <summary>
        /// Converts the integer value to a Guid.
        /// </summary>
        /// <param name="value">The value to convert to Guid.</param>
        /// <returns>The Guid value.</returns>
        public static Guid ToGuid(this int value)
            => new(value, 0, 0, new byte[8]);
    }
}
