using System;
using System.Threading.Tasks;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Utils;

namespace VkNet.Categories
{
	/// <inheritdoc />
	public partial class FaveCategory
	{
		/// <inheritdoc />
		public Task<VkCollection<User>> GetUsersAsync(int? count = null, int? offset = null)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Fave.GetUsers(count: count, offset: offset));
		}

		/// <inheritdoc />
		public Task<VkCollection<Photo>> GetPhotosAsync(int? count = null, int? offset = null, bool? photoSizes = null)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () =>
					_vk.Fave.GetPhotos(count: count, offset: offset, photoSizes: photoSizes));
		}

		/// <inheritdoc />
		public Task<WallGetObject> GetPostsAsync(int? count = null, int? offset = null, bool extended = false)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Fave.GetPosts(count: count, offset: offset, extended: extended));
		}

		/// <inheritdoc />
		public Task<FaveVideoEx> GetVideosAsync(int? count = null, int? offset = null, bool extended = false)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Fave.GetVideos(count: count, offset: offset, extended: extended));
		}

		/// <inheritdoc />
		public Task<VkCollection<ExternalLink>> GetLinksAsync(int? count = null, int? offset = null)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Fave.GetLinks(count: count, offset: offset));
		}

		/// <inheritdoc />
		public Task<bool> AddUserAsync(long userId)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Fave.AddUser(userId: userId));
		}

		/// <inheritdoc />
		public Task<bool> RemoveUserAsync(long userId)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Fave.RemoveUser(userId: userId));
		}

		/// <inheritdoc />
		public Task<bool> AddGroupAsync(long groupId)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Fave.AddGroup(groupId: groupId));
		}

		/// <inheritdoc />
		public Task<bool> RemoveGroupAsync(long groupId)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Fave.RemoveGroup(groupId: groupId));
		}

		/// <inheritdoc />
		public Task<bool> AddLinkAsync(Uri link, string text)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Fave.AddLink(link: link, text: text));
		}

		/// <inheritdoc />
		public Task<bool> RemoveLinkAsync(string linkId)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Fave.RemoveLink(linkId: linkId));
		}

		/// <inheritdoc />
		public Task<VkCollection<Market>> GetMarketItemsAsync(ulong? count = null, ulong? offset = null, bool? extended = null)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () =>
					_vk.Fave.GetMarketItems(count: count, offset: offset, extended: extended));
		}
	}
}