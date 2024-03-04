using System.Text.Json.Serialization;

namespace Jellyfin.Data.Interfaces
{
    /// <summary>
    /// An abstraction representing an entity that has a specific path.
    /// </summary>
    public interface IHasDynamicPath
    {
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        [JsonIgnore]
        string Path { get; set; }
    }
}
