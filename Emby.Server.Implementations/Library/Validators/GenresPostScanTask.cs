using System;
using System.Threading;
using System.Threading.Tasks;
using Jellyfin.Server.Implementations.Library.Interfaces;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Persistence;
using Microsoft.Extensions.Logging;

namespace Emby.Server.Implementations.Library.Validators
{
    /// <summary>
    /// Class GenresPostScanTask.
    /// </summary>
    public class GenresPostScanTask : ILibraryPostScanTask
    {
        /// <summary>
        /// The _library manager.
        /// </summary>
        private readonly ILibraryManager _libraryManager;
        private readonly ILogger<GenresValidator> _logger;
        private readonly IGenreManager _genreManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenresPostScanTask" /> class.
        /// </summary>
        /// <param name="libraryManager">The library manager.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="genreManager">The item repository.</param>
        public GenresPostScanTask(
            ILibraryManager libraryManager,
            ILogger<GenresValidator> logger,
            IGenreManager genreManager)
        {
            _libraryManager = libraryManager;
            _logger = logger;
            _genreManager = genreManager;
        }

        /// <summary>
        /// Runs the specified progress.
        /// </summary>
        /// <param name="progress">The progress.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task.</returns>
        public Task Run(IProgress<double> progress, CancellationToken cancellationToken)
        {
            return new GenresValidator(_libraryManager, _logger, _genreManager).Run(progress, cancellationToken);
        }
    }
}
