using System;

namespace Jellyfin.Server.Implementations;

/// <summary>
/// Used only for migrations.
/// </summary>
public interface ILibraryModel
{
    /// <summary>
    /// Gets or sets the date created.
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// Gets or sets the date modified.
    /// </summary>
    public DateTime DateModified { get; set; }
}
