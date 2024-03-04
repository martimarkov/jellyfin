using System.Text.Json.Serialization;

namespace Jellyfin.Data.Interfaces;

/// <summary>
/// Represents an object that has a rating.
/// </summary>
public interface IHasRating
{
    /// <summary>
    /// Gets or sets the official rating.
    /// </summary>
    /// <value>The official rating.</value>
    [JsonIgnore]
    string OfficialRating { get; set; }

    /// <summary>
    /// Gets or sets the critic rating.
    /// </summary>
    /// <value>The critic rating.</value>
    [JsonIgnore]
    float? CriticRating { get; set; }

    /// <summary>
    /// Gets or sets the custom rating.
    /// </summary>
    /// <value>The custom rating.</value>
    [JsonIgnore]
    string CustomRating { get; set; }
}
