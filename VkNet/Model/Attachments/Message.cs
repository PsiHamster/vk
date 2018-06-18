using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VkNet.Enums;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Attachments;
using VkNet.Utils;

namespace VkNet.Model
{
	/// <summary>
	/// Личное сообщение пользователя.
	/// См. описание http://vk.com/dev/message
	/// </summary>
	[DebuggerDisplay(value: "[{UserId}-{Id}] {Body}")]
	[Serializable]
	public class Message : MediaAttachment
	{
		/// <summary>
		/// Подарок.
		/// </summary>
		static Message()
		{
			RegisterType(type: typeof(Message), match: "message");
		}

		#region Методы

		/// <summary>
		/// </summary>
		/// <param name="response"> </param>
		/// <returns> </returns>
		public static Message FromJson(VkResponse response)
		{
			if (response.ContainsKey(key: "message"))
			{
				response = response[key: "message"];
			}

			var message = new Message
			{
					Id = response[key: "id"]
					, Date = response[key: "date"]
					, FromId = response[key: "from_id"]
					, PeerId= response[key: "peer_id"]
					, Out = response[key: "out"]
					, Text = response[key: "text"]
					, Attachments = response[key: "attachments"].ToReadOnlyCollectionOf<Attachment>(selector: x => x)
					, Geo = response[key: "geo"]
					, ForwardedMessages = response[key: "fwd_messages"].ToReadOnlyCollectionOf<Message>(selector: x => x)
					, Important = response[key: "important"]
					, UpdateTime = response[key: "update_time"]
					, RandomId = response[key: "random_id"]
					, ConversationMessageId = response[key: "conversation_message_id"]
					, Payload = response[key : "payload"]
					, Keyboard = response[key: "keyboard"]
					, IsHidden = response[key: "is_hidden"]
					, Action = response[key: "action"]
			};

			return message;
		}

		#endregion

		#region Стандартные поля

		/// <summary>
		/// Дата отправки сообщения.
		/// </summary>
		[JsonConverter(converterType: typeof(UnixDateTimeConverter))]
		public DateTime? Date { get; set; }

		/// <summary>
		/// Идентификатор автора сообщения.
		/// </summary>
		public long? FromId { get; set; }

		/// <summary>
		/// Идентификатор автора сообщения.
		/// </summary>
		public long? PeerId { get; set; }

		/// <summary>
		/// Текст сообщения.
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// идентификатор, используемый при отправке сообщения. Возвращается только для
		/// исходящих сообщений.
		/// </summary>
		[JsonProperty(propertyName: "random_id")]
		public long RandomId { get; set; }

		/// <summary>
		/// тип сообщения (0 — полученное, 1 — отправленное, не возвращается для
		/// пересланных сообщений).
		/// </summary>
		[JsonProperty(propertyName: "out")]
		public bool? Out { get; set; }
		
		/// <summary>
		/// Массив медиа-вложений (прикреплений).
		/// </summary>
		public ReadOnlyCollection<Attachment> Attachments { get; set; }

		/// <summary>
		/// Массив пересланных сообщений (если есть).
		/// </summary>
		public ReadOnlyCollection<Message> ForwardedMessages { get; set; }

		/// <summary>
		/// Является ли сообщение важным.
		/// </summary>
		public bool Important { get; set; }

		/// <summary>
		/// Гео данные.
		/// </summary>
		public Geo Geo { get; set; }

		/// <summary>
		/// сервисное поле для сообщений ботам (полезная нагрузка).
		/// </summary>
		public string Payload { get; set; }

		/// <summary>
		/// Клавиатура, присланная ботом
		/// </summary>
		public MessageKeyboard Keyboard { get; set; }

		/// <summary>
		/// информация о сервиинформация о сервисном действии с чатомсном действии с чатом
		/// </summary>
		public ConversationAction Action { get; set; }

		#endregion

		#region недокументированные

		/// <summary>
		/// Id сообщения в этом диалоге.
		/// </summary>
		public long? ConversationMessageId { get; set; }


		public bool? IsHidden { get; set; }

		/// <summary>
		/// </summary>
		[JsonProperty(propertyName: "update_time")]
		public string UpdateTime { get; set; }

		#endregion

	}
}