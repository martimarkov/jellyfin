using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jellyfin.Data;
using Jellyfin.Data.Entities;
using Jellyfin.Data.Entities.Libraries;
using Jellyfin.Data.Enums;
using Jellyfin.Extensions;
using Jellyfin.Server.Implementations.Library.Interfaces;
using MediaBrowser.Common;
using MediaBrowser.Controller.Dto;
using MediaBrowser.Model.Dto;
using Microsoft.EntityFrameworkCore;

namespace Jellyfin.Server.Implementations.Library.Managers;

/// <inheritdoc />
public class GenreManager : IGenreManager
{
    private readonly IDbContextFactory<LibraryDbContext> _provider;
    private readonly IApplicationHost _appHost;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenreManager"/> class.
    /// </summary>
    /// <param name="provider">The LibraryDb context.</param>
    /// <param name="appHost">Instance of the <see cref="IApplicationHost"/> interface.</param>
    public GenreManager(
        IDbContextFactory<LibraryDbContext> provider,
        IApplicationHost appHost)
    {
        _provider = provider;
        _appHost = appHost;
    }

    /// <inheritdoc />
    public Genre AddGenre(string genreName)
    {
        return AddGenreAsync(genreName).GetAwaiter().GetResult();
    }

    /// <inheritdoc />
    public async Task<Genre> AddGenreAsync(string genreName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(genreName);

        var dbContext = await _provider.CreateDbContextAsync().ConfigureAwait(false);
        Genre? genre;
        await using (dbContext.ConfigureAwait(false))
        {
            genre = await dbContext.Genres.FirstOrDefaultAsync(e => e.Name == genreName).ConfigureAwait(false);
            if (genre == null)
            {
                genre = new Genre(genreName);
                await dbContext.Genres.AddAsync(genre).ConfigureAwait(false);
            }

            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        return genre;
    }

    /// <inheritdoc />
    public async Task<List<Genre>> GetGenresAsync()
    {
        var dbContext = await _provider.CreateDbContextAsync().ConfigureAwait(false);
        List<Genre>? genres;
        await using (dbContext.ConfigureAwait(false))
        {
            genres = await dbContext.Genres.ToListAsync().ConfigureAwait(false);
        }

        return genres;
    }

    /// <inheritdoc />
    public Task RefreshMetadataAsync(Genre item)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<BaseItemDto> GetGenreDto(Genre item, DtoOptions options, User? user = null)
    {
        var dto = new BaseItemDto
        {
            ServerId = _appHost.SystemId,
            Name = item.Name,
            Id = item.GetGuidId(),
            Type = BaseItemKind.Genre
        };

        return Task.FromResult(dto);
    }

    /// <inheritdoc />
    public async Task<Genre> GetGenreByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Id needs to be greater than 0.");
        }

        var dbContext = await _provider.CreateDbContextAsync().ConfigureAwait(false);
        Genre? genre = null;
        await using (dbContext.ConfigureAwait(false))
        {
            genre = await dbContext.Genres.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        }

        if (genre == null)
        {
            throw new KeyNotFoundException("No genre found with given id.");
        }

        return genre;
    }
}
