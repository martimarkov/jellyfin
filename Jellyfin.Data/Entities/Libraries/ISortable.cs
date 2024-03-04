using System;
using System.Text.Json.Serialization;

namespace Jellyfin.Data.Entities.Libraries;

/// <summary>
/// Represents an abstract class that provides functionality for sorting items.
/// </summary>
public abstract class ISortable
{
    /// <summary>
    /// Gets or sets the name of the item.
    /// </summary>
    public virtual string Name { get; set; } = null!;

    /// <summary>
    /// Gets the name of the sort.
    /// </summary>
    /// <value>The name of the sort.</value>
    [JsonIgnore]
    public string SortName
    {
        get
        {
           return BaseItemHelpers.ModifySortChunks(Name).ToLowerInvariant();
        }
    }
}
