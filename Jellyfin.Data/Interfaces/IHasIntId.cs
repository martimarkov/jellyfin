using System.ComponentModel.DataAnnotations.Schema;

namespace Jellyfin.Data.Interfaces;

/// <summary>
/// Represents an interface for objects that have an integer identifier.
/// </summary>
public interface IHasIntId
{
    /// <summary>
    /// Gets the id.
    /// </summary>
    /// <remarks>
    /// Identity, Indexed, Required.
    /// </remarks>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; }
}
