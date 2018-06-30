using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VkNet.Enums.SafetyEnums;
using VkNet.Utils;

namespace VkNet.Model
{
	/// <summary>
	/// Объект, описывающий беседу с пользователем.
	/// https://vk.com/dev/objects/conversation
	/// </summary>
	[Serializable]
	class Conversation
	{
		/// <summary>
		/// Информация о ссылках на предпросмотр фотографий беседы.
		/// </summary>
		public Previews PhotoPreviews { get; set; }

		/// <summary>
		/// Идентификатор последнего прочитанного сообщения текущим пользователем
		/// </summary>
		public ulong? InRead { get; set; }

		/// <summary>
		/// Идентификатор последнего прочитанного сообщения собеседником
		/// </summary>
		public ulong? OutRead { get; set; }


		/// <summary>
		/// Идентификаторы участников беседы.
		/// </summary>
		public ReadOnlyCollection<long> ChatActive { get; set; }

		/// <summary>
		/// Настройки уведомлений для беседы, если они есть. sound и disabled_until
		/// </summary>
		public ChatPushSettings PushSettings { get; set; }

		/// <summary>
		/// Количество участников беседы.
		/// </summary>
		public int? UsersCount { get; set; }

		/// <summary>
		/// Идентификатор создателя беседы.
		/// </summary>
		public long? AdminId { get; set; }

		/// <summary>
		/// поле передано, если это служебное сообщение
		/// </summary>
		/// <remarks>
		/// строка, может быть chat_photo_update или chat_photo_remove, а с версии 5.14 еще
		/// и chat_create, chat_title_update,
		/// chat_invite_user, chat_kick_user
		/// </remarks>
		public MessageAction Action { get; set; }

	}
}
