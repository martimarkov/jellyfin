using System;
using System.Collections.Generic;
using System.IO;
using Jellyfin.Data.Entities.Libraries;

namespace Jellyfin.Data.Interfaces
{
    /// <summary>
    /// An abstraction representing an entity that has a specific path.
    /// </summary>
    public interface IHasPath
    {
        /// <summary>
        /// Retrieves the path of an entity with the specified name.
        /// </summary>
        /// <param name="modelPath">The model path.</param>
        /// <param name="name">The name of the entity.</param>
        /// <param name="normalizeName">Indicates if the name should be normalized.</param>
        /// <returns>The path of the entity.</returns>
        public static string GetPath(string modelPath, string name, bool normalizeName = true)
        {
            // Trim the period at the end because windows will have a hard time with that
            var validName = normalizeName ? FileSystemHelper.GetValidFilename(name).Trim().TrimEnd('.') : name;

            return Path.Combine(modelPath, validName);
        }
    }
}
