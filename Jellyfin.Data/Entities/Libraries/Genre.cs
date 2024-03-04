using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text.Json.Serialization;
using Jellyfin.Data.Enums;
using Jellyfin.Data.Interfaces;
using Jellyfin.Server.Implementations;

namespace Jellyfin.Data.Entities.Libraries
{
    /// <summary>
    /// An entity representing a genre.
    /// </summary>
    public class Genre : ISortable, IHasConcurrencyToken, ILibraryModel, IHasIntId, IHasPath, IBaseItemMigration, IHasImages
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Genre"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Genre(string name)
        {
            Name = name;
            DateCreated = DateTime.UtcNow;
            DateModified = DateCreated;
        }

        /// <inheritdoc />
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <remarks>
        /// Indexed, Required, Max length = 255.
        /// </remarks>
        [MaxLength(255)]
        [StringLength(255)]
        public override string Name { get; set; }

        /// <inheritdoc />
        [ConcurrencyCheck]
        public uint RowVersion { get; private set; }

        /// <inheritdoc />
        public DateTime DateCreated { get; set; }

        /// <inheritdoc />
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Gets represents the image information associated with a genre.
        /// </summary>
        [JsonIgnore]
        public ICollection<ItemImageInfo> ImageInfos { get; } = new List<ItemImageInfo>();

        /// <inheritdoc />
        public bool IsBaseItem => false;

        /// <inheritdoc />
        public IBaseItemMigration? DisplayParent { get; }

        /// <inheritdoc />
        public bool SupportsInheritedParentImages => false;

        /// <inheritdoc />
        public void OnSavingChanges()
        {
            RowVersion++;
        }

        /// <inheritdoc />
        public ItemImageInfo? GetImageInfo(ImageType imageType, int imageIndex)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void RemoveImage(ItemImageInfo image)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<ItemImageInfo> GetImages(ImageType imageType)
        {
            if (imageType == ImageType.Chapter)
            {
                throw new ArgumentException("No image info for chapter images");
            }

            // Yield return is more performant than LINQ Where on an Array
            var imageInfos = ImageInfos.Where(p => p.Type == imageType);
            return imageInfos;
        }

        /// <inheritdoc />
        public IList<string> GetEtagValues(User? user)
        {
            return new List<string>
            {
                DateModified.Ticks.ToString(CultureInfo.InvariantCulture)
            };
        }

        /// <inheritdoc />
        public BaseItemKind GetBaseItemKind()
        {
            return BaseItemKind.Genre;
        }

        /// <inheritdoc />
        public IBaseItemMigration? GetParent()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IBaseItemMigration? GetOwner()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string GetImagePath(ImageType imageType, int imageIndex)
        {
            throw new NotImplementedException();
        }
    }
}
