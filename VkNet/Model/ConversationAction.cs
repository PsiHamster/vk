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
	/// Объект, описывающий действие с чатом.
	/// https://vk.com/dev/objects/message
	/// </summary>
	[Serializable]
	public class ConversationAction
	{

		#region Методы

		/// <summary>
		/// </summary>
		/// <param name="response"> </param>
		/// <returns> </returns>
		public static ConversationAction FromJson(VkResponse response) {

			var action = new ConversationAction {
				Type = response[key: "type"]
				, MemberId = response[key: "member_id"]
				, Text = response[key: "text"]
				, Email = response[key: "email"]
				, Photo = response[key: "photo"]
			};

			return action;
		}

		#endregion

		/// <summary>
		/// поле передано, если это служебное сообщение
		/// </summary>
		/// <remarks>
		/// строка, может быть chat_photo_update или chat_photo_remove, а с версии 5.14 еще
		/// и chat_create, chat_title_update,
		/// chat_invite_user, chat_kick_user
		/// </remarks>
		public MessageAction Type { get; set; }

		/// <summary>
		/// идентификатор пользователя (если &gt; 0) или email
		/// (если &lt; 0), которого пригласили или исключили
		/// (для служебных сообщений с type = chat_invite_user,
		/// chat_invite_user_by_link или chat_kick_user).
		/// Идентификатор пользователя, который закрепил/открепил сообщение
		/// для action = chat_pin_message или chat_unpin_message.
		/// </summary>
		public long? MemberId { get; set; }

		/// <summary>
		/// Название беседы.
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// email, который пригласили или исключили.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Изображение-обложка чата
		/// </summary>
		public ChatPhoto Photo { get; set; }
	}
}
