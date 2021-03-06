using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VkNet.Enums;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace VkNet.Categories
{
	/// <inheritdoc />
	public partial class FriendsCategory
	{
		/// <inheritdoc />
		public Task<VkCollection<User>> GetAsync(FriendsGetParams @params, bool skipAuthorization = false)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () =>
					_vk.Friends.Get(@params: @params, skipAuthorization: skipAuthorization));
		}

		/// <inheritdoc />
		public Task<ReadOnlyCollection<long>> GetAppUsersAsync()
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Friends.GetAppUsers());
		}

		/// <inheritdoc />
		public Task<FriendOnline> GetOnlineAsync(FriendsGetOnlineParams @params)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Friends.GetOnline(@params: @params));
		}

		/// <inheritdoc />
		public Task<ReadOnlyCollection<long>> GetMutualAsync(FriendsGetMutualParams @params)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Friends.GetMutual(@params: @params));
		}

		/// <inheritdoc />
		public Task<ReadOnlyCollection<AreFriendsResult>> AreFriendsAsync(IEnumerable<long> userIds, bool? needSign = null)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Friends.AreFriends(userIds: userIds, needSign: needSign));
		}

		/// <inheritdoc />
		public Task<long> AddListAsync(string name, IEnumerable<long> userIds)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Friends.AddList(name: name, userIds: userIds));
		}

		/// <inheritdoc />
		public Task<bool> DeleteListAsync(long listId)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Friends.DeleteList(listId: listId));
		}

		/// <inheritdoc />
		public Task<VkCollection<FriendList>> GetListsAsync(long? userId = null, bool? returnSystem = null)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Friends.GetLists(userId: userId, returnSystem: returnSystem));
		}

		/// <inheritdoc />
		public Task<bool> EditListAsync(long listId
											, string name = null
											, IEnumerable<long> userIds = null
											, IEnumerable<long> addUserIds = null
											, IEnumerable<long> deleteUserIds = null)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () =>
					_vk.Friends.EditList(listId: listId, name: name, userIds: userIds, addUserIds: addUserIds));
		}

		/// <inheritdoc />
		public Task<bool> DeleteAllRequestsAsync()
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Friends.DeleteAllRequests());
		}

		/// <inheritdoc />
		public Task<AddFriendStatus> AddAsync(long userId
													, string text = ""
													, bool? follow = null
													, long? captchaSid = null
													, string captchaKey = null)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () =>
					_vk.Friends.Add(userId: userId, text: text, follow: follow, captchaSid: captchaSid, captchaKey: captchaKey));
		}

		/// <inheritdoc />
		public Task<FriendsDeleteResult> DeleteAsync(long userId)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Friends.Delete(userId: userId));
		}

		/// <inheritdoc />
		public Task<bool> EditAsync(long userId, IEnumerable<long> listIds)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Friends.Edit(userId: userId, listIds: listIds));
		}

		/// <inheritdoc />
		public Task<ReadOnlyCollection<long>> GetRecentAsync(long? count = null)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Friends.GetRecent(count: count));
		}

		/// <inheritdoc />
		public Task<GetRequestsResult> GetRequestsAsync(FriendsGetRequestsParams @params)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Friends.GetRequests(@params: @params));
		}

		/// <inheritdoc />
		public Task<VkCollection<FriendsGetRequestsResult>> GetRequestsExtendedAsync(FriendsGetRequestsParams @params)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Friends.GetRequestsExtended(@params: @params));
		}

		/// <inheritdoc />
		public Task<VkCollection<User>> GetSuggestionsAsync(FriendsFilter filter = null
																, long? count = null
																, long? offset = null
																, UsersFields fields = null
																, NameCase nameCase = null)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () =>
					_vk.Friends.GetSuggestions(filter: filter, count: count, offset: offset, fields: fields, nameCase: nameCase));
		}

		/// <inheritdoc />
		public Task<ReadOnlyCollection<User>> GetByPhonesAsync(IEnumerable<string> phones, ProfileFields fields)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Friends.GetByPhones(phones: phones, fields: fields));
		}

		/// <inheritdoc />
		public Task<VkCollection<User>> SearchAsync(FriendsSearchParams @params)
		{
			return TypeHelper.TryInvokeMethodAsync(func: () => _vk.Friends.Search(@params: @params));
		}
	}
}