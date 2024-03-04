#pragma warning disable CS1591

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Jellyfin.Data.Enums;
using Jellyfin.Server.Implementations;

namespace Jellyfin.Data.Entities.Libraries
{
    public class ItemImageInfo : ILibraryModel
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <remarks>
        /// Identity, Indexed, Required.
        /// </remarks>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public required string Path { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public ImageType Type { get; set; }

        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>The date modified.</value>
        public DateTime DateModified { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the blurhash.
        /// </summary>
        /// <value>The blurhash.</value>
        public string? BlurHash { get; set; }

        [JsonIgnore]
        public bool IsLocalFile => !Path.StartsWith("http", StringComparison.OrdinalIgnoreCase);
    }
}
