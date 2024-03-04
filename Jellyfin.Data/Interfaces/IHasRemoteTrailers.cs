using System.Collections.Generic;
using MediaBrowser.Model.Entities;

namespace Jellyfin.Data.Interfaces;

/// <summary>
/// Represents an entity that has a collection of remote trailers.
/// </summary>
public interface IHasRemoteTrailers
{
    /// <summary>
    /// Gets or sets the remote trailers.
    /// </summary>
    /// <value>The remote trailers.</value>
    public IReadOnlyList<MediaUrl> RemoteTrailers { get; set; }
}
