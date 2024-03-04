using System;
using System.Text;
using Jellyfin.Data.Entities.Libraries;
using Jellyfin.Data.Enums;
using Jellyfin.Data.Interfaces;
using Jellyfin.Extensions;

namespace Jellyfin.Data;

/// <summary>
/// Helpers for BaseItem.
/// </summary>
public static class BaseItemHelpers
{
    /// <summary>
    /// Modifies and sorts chunks in a name string.
    /// </summary>
    /// <param name="name">The name string containing chunks to be modified and sorted.</param>
    /// <returns>The modified and sorted name string.</returns>
    public static string ModifySortChunks(ReadOnlySpan<char> name)
    {
        static void AppendChunk(StringBuilder builder, bool isDigitChunk, ReadOnlySpan<char> chunk)
        {
            if (isDigitChunk && chunk.Length < 10)
            {
                builder.Append('0', 10 - chunk.Length);
            }

            builder.Append(chunk);
        }

        if (name.IsEmpty)
        {
            return string.Empty;
        }

        var builder = new StringBuilder(name.Length);

        int chunkStart = 0;
        bool isDigitChunk = char.IsDigit(name[0]);
        for (int i = 0; i < name.Length; i++)
        {
            var isDigit = char.IsDigit(name[i]);
            if (isDigit != isDigitChunk)
            {
                AppendChunk(builder, isDigitChunk, name.Slice(chunkStart, i - chunkStart));
                chunkStart = i;
                isDigitChunk = isDigit;
            }
        }

        AppendChunk(builder, isDigitChunk, name.Slice(chunkStart));

        // logger.LogDebug("ModifySortChunks Start: {0} End: {1}", name, builder.ToString());
        return builder.ToString().RemoveDiacritics();
    }

    /// <summary>
    /// Gets the Guid id of the given IBaseItemMigration item.
    /// </summary>
    /// <param name="item">The item for which to get the Guid id.</param>
    /// <returns>The Guid id of the item.</returns>
    public static Guid GetGuidId(this IBaseItemMigration item)
    {
        var id = item.GetType().GetProperty("Id")?.GetValue(item, null);
        BaseItemKind? baseItemKind = null;
        if (item.GetType() == typeof(Genre))
        {
            baseItemKind = BaseItemKind.Genre;
        }

        if (baseItemKind != null)
        {
            var baseItemKindEncoded = ((int)baseItemKind).ToString("D4", default);
            var idEncoded = ((int)(id ?? 0)).ToString("D12", default);
            return new Guid($"99999999-0000-0000-{baseItemKindEncoded}-{idEncoded}");
        }

        return (Guid)(id ?? Guid.Empty);
    }
}
