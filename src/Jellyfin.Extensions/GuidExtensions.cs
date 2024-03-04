using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Jellyfin.Extensions;

/// <summary>
/// Guid specific extensions.
/// </summary>
public static class GuidExtensions
{
    /// <summary>
    /// Determine whether the guid is default.
    /// </summary>
    /// <param name="guid">The guid.</param>
    /// <returns>Whether the guid is the default value.</returns>
    public static bool IsEmpty(this Guid guid)
        => guid.Equals(default);

    /// <summary>
    /// Determine whether the guid is null or default.
    /// </summary>
    /// <param name="guid">The guid.</param>
    /// <returns>Whether the guid is null or the default valueF.</returns>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this Guid? guid)
        => guid is null || guid.Value.IsEmpty();

    /// <summary>
    /// Gets the integer ID of the given GUID.
    /// </summary>
    /// <param name="itemId">The item for which to retrieve the ID.</param>
    /// <returns>The integer ID of the item.</returns>
    /// <exception cref="System.NotImplementedException">Thrown when the item does not have a valid ID.</exception>
    public static string GetIntId(this Guid itemId)
    {
        if (itemId.ToString().StartsWith("99999999", StringComparison.Ordinal))
        {
            string[] sections = itemId.ToString().Split('-');
            int realId = int.Parse(sections[4], CultureInfo.InvariantCulture);
            return realId.ToString(CultureInfo.InvariantCulture);
        }

        throw new NotImplementedException();
    }
}
