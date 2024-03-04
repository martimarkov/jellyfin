using System.Collections.Generic;
using System.Text.Json.Serialization;
using Jellyfin.Data.Entities.Libraries;

namespace Jellyfin.Data.Interfaces;

/// <summary>
/// Represents an entity that has a collection of image information.
/// </summary>
public interface IHasImages
{
    /// <summary>
    /// Gets represents the image information associated with a genre.
    /// </summary>
    [JsonIgnore]
    public ICollection<ItemImageInfo> ImageInfos { get; }
}
