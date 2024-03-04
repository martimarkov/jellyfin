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
    /// Class GenresValidator.
    /// </summary>
    public class GenresValidator
    {
        /// <summary>
        /// The library manager.
        /// </summary>
        private readonly ILibraryManager _libraryManager;
        private readonly IGenreManager _genreManager;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<GenresValidator> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenresValidator"/> class.
        /// </summary>
        /// <param name="libraryManager">The library manager.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="genreManager">The genre manager.</param>
        public GenresValidator(ILibraryManager libraryManager, ILogger<GenresValidator> logger, IGenreManager genreManager)
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
        public async Task Run(IProgress<double> progress, CancellationToken cancellationToken)
        {
            var genres = await _genreManager.GetGenresAsync().ConfigureAwait(false);

            var numComplete = 0;
            var count = genres.Count;

            foreach (var item in genres)
            {
                try
                {
                    await _genreManager.RefreshMetadataAsync(item).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    // Don't clutter the log
                    throw;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error refreshing {GenreName}", item.Name);
                }

                numComplete++;
                double percent = numComplete;
                percent /= count;
                percent *= 100;

                progress.Report(percent);
            }

            progress.Report(100);
        }
    }
}
