#nullable disable

#pragma warning disable CA1002, CA1707, CS1591

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Jellyfin.Data.Entities;
using Jellyfin.Data.Interfaces;
using MediaBrowser.Controller.Dto;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.Entities;

namespace MediaBrowser.Controller.Library
{
    /// <summary>
    /// Interface IUserDataManager.
    /// </summary>
    public interface IUserDataManager
    {
        /// <summary>
        /// Occurs when [user data saved].
        /// </summary>
        event EventHandler<UserDataSaveEventArgs> UserDataSaved;

        /// <summary>
        /// Saves the user data.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="item">The item.</param>
        /// <param name="userData">The user data.</param>
        /// <param name="reason">The reason.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        void SaveUserData(Guid userId, BaseItem item, UserItemData userData, UserDataSaveReason reason, CancellationToken cancellationToken);

        void SaveUserData(User user, BaseItem item, UserItemData userData, UserDataSaveReason reason, CancellationToken cancellationToken);

        /// <summary>
        /// Save the provided user data for the given user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="item">The item.</param>
        /// <param name="userDataDto">The reason for updating the user data.</param>
        /// <param name="reason">The reason.</param>
        void SaveUserData(User user, BaseItem item, UpdateUserItemDataDto userDataDto, UserDataSaveReason reason);

        /// <summary>
        /// Gets the user data for a BaseItem.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="item">The BaseItem.</param>
        /// <returns>The User data.</returns>
        UserItemData GetUserData(User user, BaseItem item);

        /// <summary>
        /// Gets the user data for a BaseItem.
        /// </summary>
        /// <remarks>Async operation.</remarks>
        /// <param name="user">The user.</param>
        /// <param name="item">The BaseItem.</param>
        /// <returns>The User data.</returns>
        Task<UserItemData> GetUserDataAsync(User user, IBaseItemMigration item);

        /// <summary>
        /// Gets the user data dto.
        /// </summary>
        /// <param name="item">Item to use.</param>
        /// <param name="user">User to use.</param>
        /// <returns>User data dto.</returns>
        UserItemDataDto GetUserDataDto(IBaseItemMigration item, User user);

        /// <summary>
        /// Gets the user data dto.
        /// </summary>
        /// <param name="item">Item to use.</param>
        /// <param name="user">User to use.</param>
        /// <returns>User data dto.</returns>
        Task<UserItemDataDto> GetUserDataDtoAsync(IBaseItemMigration item, User user);

        UserItemDataDto GetUserDataDto(IBaseItemMigration item, BaseItemDto itemDto, User user, DtoOptions options);

        Task<UserItemDataDto> GetUserDataDtoAsync(IBaseItemMigration item, BaseItemDto itemDto, User user, DtoOptions options);

        /// <summary>
        /// Get all user data for the given user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The user item data.</returns>
        Task<List<UserItemData>> GetAllUserData(User user);

        /// <summary>
        /// Updates playstate for an item and returns true or false indicating if it was played to completion.
        /// </summary>
        /// <param name="item">Item to update.</param>
        /// <param name="data">Data to update.</param>
        /// <param name="reportedPositionTicks">New playstate.</param>
        /// <returns>True if playstate was updated.</returns>
        bool UpdatePlayState(BaseItem item, UserItemData data, long? reportedPositionTicks);
    }
}
