using System;

namespace Jellyfin.Data.Interfaces;

/// <summary>
/// Represents an entity that has metadata.
/// </summary>
public interface IHasMetadata
{
    /// <summary>
    /// Gets or sets the preferred country code for metadata.
    /// </summary>
    string PreferredMetadataCountryCode { get; set; }

    /// <summary>
    /// Gets or sets the preferred metadata language for the item.
    /// </summary>
    string PreferredMetadataLanguage { get; set; }

    /// <summary>
    /// Gets or sets the overview of the base item.
    /// </summary>
    string Overview { get; set; }

    /// <summary>
    /// Gets or sets the original title of the base item.
    /// </summary>
    string OriginalTitle { get; set; }

    /// <summary>
    /// Gets the display parent ID.
    /// </summary>
    Guid DisplayParentId { get; }

    /// <summary>
    /// Gets or sets the premiere date of the item.
    /// </summary>
    DateTime? PremiereDate { get; set; }

    /// <summary>
    /// Gets or sets the production year of the item.
    /// </summary>
    int? ProductionYear { get; set; }

    /// <summary>
    /// Gets or sets the tagline of the item.
    /// </summary>
    string Tagline { get; set; }
}
