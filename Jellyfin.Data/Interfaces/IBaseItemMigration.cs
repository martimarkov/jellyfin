using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;
using Jellyfin.Data.Entities;
using Jellyfin.Data.Entities.Libraries;
using Jellyfin.Data.Enums;
using Jellyfin.Extensions;
using MediaBrowser.Common.Extensions;
using MediaBrowser.Controller.Entities;

namespace Jellyfin.Data.Interfaces;

/// <summary>
/// Used only for migration purposes while BaseItem still holds entities in the DB.
/// </summary>
public interface IBaseItemMigration
{
    /// <summary>
    /// Gets or sets the name of the item.
    /// </summary>
    /// <remarks>
    /// This property represents the name of the item.
    /// </remarks>
    string Name { get; set; }

    /// <summary>
    /// Gets a value indicating whether the item is a base item.
    /// </summary>
    [JsonIgnore]
    public bool IsBaseItem { get; }

    /// <summary>
    /// Gets the id that should be used to key display prefs for this item.
    /// Default is based on the type for everything except actual generic folders.
    /// </summary>
    /// <value>The display prefs id.</value>
    [JsonIgnore]
    Guid DisplayPreferencesId
    {
        get
        {
            return (GetType().FullName ?? string.Empty).GetMD5();
        }
    }

    /// <summary>
    /// Gets a value indicating whether the item supports inherited parent images.
    /// </summary>
    /// <remarks>
    /// If this property is set to <c>true</c>, the item can inherit images from its parent item if it doesn't have its own images.
    /// </remarks>
    bool SupportsInheritedParentImages { get; }

    /// <summary>
    /// Gets the display parent of the item.
    /// </summary>
    /// <remarks>
    /// This property represents the parent folder of the item that is used for display purposes.
    /// It is used to determine the hierarchy and organization of the items in the user interface.
    /// </remarks>
    IBaseItemMigration? DisplayParent { get; }

    /// <summary>
    /// Retrieves the information about an image associated with an item.
    /// </summary>
    /// <param name="imageType">The type of the image. Refer to <see cref="Jellyfin.Data.Enums.ImageType"/> for possible values.</param>
    /// <param name="imageIndex">The index of the image. Defaults to 0 if not specified.</param>
    /// <returns>The <see cref="Jellyfin.Data.Entities.Libraries.ItemImageInfo"/> object containing information about the image, or null if no image is found.</returns>
    ItemImageInfo? GetImageInfo(ImageType imageType, int imageIndex);

    /// <summary>
    /// Removes the specified image from the item.
    /// </summary>
    /// <param name="image">The <see cref="Jellyfin.Data.Entities.Libraries.ItemImageInfo"/> object representing the image to be removed.</param>
    void RemoveImage(ItemImageInfo image);

    /// <summary>
    /// Retrieves the default primary image aspect ratio for an item.
    /// </summary>
    /// <returns>The default primary image aspect ratio.</returns>
    public double GetDefaultPrimaryImageAspectRatio()
    {
        return 0;
    }

    /// <summary>
    /// Retrieves a collection of images of the specified type for the current base item.
    /// </summary>
    /// <param name="imageType">The type of images to retrieve.</param>
    /// <returns>A collection of <see cref="ItemImageInfo"/> objects representing the images of the specified type for the current base item.</returns>
    IEnumerable<ItemImageInfo> GetImages(ImageType imageType);

    /// <summary>
    /// Retrieves the Etag for the current user.
    /// </summary>
    /// <param name="user">The user for whom to retrieve the Etag.</param>
    /// <returns>The Etag for the user.</returns>
    public string GetEtag(User? user)
    {
        var list = GetEtagValues(user);

        return string.Join('|', list).GetMD5().ToString("N", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Returns the Etag value for the specified user.
    /// </summary>
    /// <param name="user">The user for whom to calculate the Etag.</param>
    /// <returns>The Etag value as a string.</returns>
    protected IList<string> GetEtagValues(User? user);

    /// <summary>
    /// Gets the base item kind.
    /// </summary>
    /// <returns>
    /// The base item kind.
    /// </returns>
    BaseItemKind GetBaseItemKind();

    /// <summary>
    /// Retrieves the parent folder of the current folder.
    /// </summary>
    /// <returns>The parent folder of the current folder.</returns>
    IBaseItemMigration? GetParent();

    /// <summary>
    /// Get the owner of the item.
    /// </summary>
    /// <returns>The owner of the item as a Folder object.</returns>
    IBaseItemMigration? GetOwner();

    /// <summary>
    /// Gets the image path.
    /// </summary>
    /// <param name="imageType">Type of the image.</param>
    /// <param name="imageIndex">Index of the image.</param>
    /// <returns>System.String.</returns>
    /// <exception cref="ArgumentNullException">Item is null.</exception>
    string GetImagePath(ImageType imageType, int imageIndex);
}
