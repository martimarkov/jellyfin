using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Jellyfin.Data.Entities;
using MediaBrowser.Controller.Dto;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Model.Dto;
using Microsoft.AspNetCore.Mvc;
using Genre = Jellyfin.Data.Entities.Libraries.Genre;

namespace Jellyfin.Server.Implementations.Library.Interfaces;

/// <summary>
/// The Genre Manager.
/// </summary>
public interface IGenreManager
{
    /// <summary>
    /// Add a new genre.
    /// </summary>
    /// <param name="genreName">The genre name.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Genre AddGenre(string genreName);

    /// <summary>
    /// Add a new genre.
    /// </summary>
    /// <param name="genreName">The genre name.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<Genre> AddGenreAsync(string genreName);

    /// <summary>
    /// Get all genres.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<List<Genre>> GetGenresAsync();

    /// <summary>
    /// Refreshes the metadata for a genre asynchronously.
    /// </summary>
    /// <param name="item">The genre to refresh.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task RefreshMetadataAsync(Genre item);

    /// <summary>
    /// Gets the genre DTO.
    /// </summary>
    /// <param name="item">The genre.</param>
    /// <param name="options">The DTO options.</param>
    /// <param name="user">The user.</param>
    /// <returns>Task{GenreInfoDto}.</returns>
    Task<BaseItemDto> GetGenreDto(Genre item, DtoOptions options, User? user = null);

    /// <summary>
    /// Retrieves a genre by its ID.
    /// </summary>
    /// <param name="id">The ID of the genre to retrieve.</param>
    /// <returns>The genre with the specified ID.</returns>
    Task<Genre> GetGenreByIdAsync(int id);
}
