﻿using Newtonsoft.Json.Linq;
using VkNet.Model.RequestParams;

namespace VkNet.Categories
{

	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System;

	using Utils;
	using Enums;
	using Model;
	using Model.Attachments;

	/// <summary>
	/// Методы для работы с фотографиями.
	/// </summary>
	public class PhotoCategory
	{
		private readonly VkApi _vk;

		internal PhotoCategory(VkApi vk)
		{
			_vk = vk;
		}

		/// <summary>
		/// Создает пустой альбом для фотографий.
		/// </summary>
		/// <param name="params">Параметры запроса.</param>
		/// <returns>
		/// После успешного выполнения возвращает объект <see cref="PhotoAlbum" />
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.createAlbum" />.
		/// </remarks>
		[ApiVersion("5.37")]
		public PhotoAlbum CreateAlbum(PhotoCreateAlbumParams @params)
		{
			return _vk.Call("photos.createAlbum", @params);
		}

		/// <summary>
		/// Редактирует данные альбома для фотографий пользователя.
		/// </summary>
		/// <param name="params">Параметры запроса.</param>
		/// <returns>
		/// После успешного выполнения возвращает <c>true</c>.
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.editAlbum" />.
		/// </remarks>
		[ApiVersion("5.37")]
		public bool EditAlbum(PhotoEditAlbumParams @params)
		{
			return _vk.Call("photos.editAlbum", @params);
		}

		/// <summary>
		/// Возвращает список альбомов пользователя или сообщества.
		/// </summary>
		/// <param name="count">Количество альбомов.</param>
		/// <param name="params">Параметры запроса.</param>
		/// <returns>
		/// Возвращает список объектов <see cref="PhotoAlbum" />
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.getAlbums" />.
		/// </remarks>
		[ApiVersion("5.40")]
		public ReadOnlyCollection<PhotoAlbum> GetAlbums(out int count, PhotoGetAlbumsParams @params)
		{
			var response = _vk.Call("photos.getAlbums", @params);
			count = response["count"];
			return response["items"].ToReadOnlyCollectionOf<PhotoAlbum>(x => x);
		}

		/// <summary>
		/// Возвращает список фотографий в альбоме.
		/// </summary>
		/// <param name="count">Количество альбомов.</param>
		/// <param name="params">Параметры запроса.</param>
		/// <returns>После успешного выполнения возвращает список объектов <see cref="Photo"/>.</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.get"/>.
		/// </remarks>
		[ApiMethodName("photos.get", Skip = true)]
		[ApiVersion("5.37")]
		public ReadOnlyCollection<Photo> Get(out int count, PhotoGetParams @params)
		{
			var response = _vk.Call("photos.get", @params);
			count = response["count"];
			return response.ToReadOnlyCollectionOf<Photo>(x => x);
		}

		/// <summary>
		/// Возвращает количество доступных альбомов пользователя или сообщества.
		/// </summary>
		/// <param name="userId">Идентификатор пользователя, количество альбомов которого необходимо получить.</param>
		/// <param name="groupId">Идентификатор сообщества, количество альбомов которого необходимо получить. </param>
		/// <returns>После успешного выполнения возвращает количество альбомов с учетом настроек приватности.</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.getAlbumsCount"/>.
		/// </remarks>
		[ApiVersion("5.37")]
		public int GetAlbumsCount(long? userId = null, long? groupId = null)
		{
			var parameters = new VkParameters
				{
					{"user_id", userId},
					{"group_id", groupId}
				};

			return _vk.Call("photos.getAlbumsCount", parameters);
		}

		/// <summary>
		/// Возвращает список фотографий со страницы пользователя или сообщества.
		/// </summary>
		/// <param name="ownerId">Идентификатор пользователя или сообщества, фотографии которого нужно получить. Обратите внимание, идентификатор сообщества в параметре owner_id необходимо указывать со знаком "-" — например, owner_id=-1 соответствует идентификатору сообщества ВКонтакте API (club1)</param>
		/// <param name="photoIds">Идентификаторы фотографий, информацию о которых необходимо вернуть</param>
		/// <param name="rev">Порядок сортировки фотографий (1 — антихронологический, 0 — хронологический). флаг, может принимать значения 1 или 0</param>
		/// <param name="extended"><c>true</c> — будет возвращено дополнительное поле likes. По умолчанию поле likes не возвращается. флаг, может принимать значения 1 или 0</param>
		/// <param name="feedType">Тип новости, получаемый в поле type метода newsfeed.get. строка</param>
		/// <param name="feed">Unixtime, который может быть получен методом newsfeed.get в поле date, для получения всех фотографий загруженных пользователем в определённый день либо на которых пользователь был отмечен. Также нужно указать параметр uid пользователя, с которым произошло событие</param>
		/// <param name="photoSizes">Возвращать ли размеры фотографий в специальном формате</param>
		/// <param name="count">Положительное число, максимальное значение 1000</param>
		/// <param name="offset">Положительное число</param>
		/// <returns>После успешного выполнения возвращает массив объектов <see cref="Photo"/>. В случае, если запись на стене о том, что была обновлена фотография профиля, не удалена, будет возвращено дополнительное поле post_id, содержащее идентификатор записи на стене.</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.getProfile"/>.
		/// </remarks>
		[ApiVersion("5.9")]
		[Obsolete("Данный метод устарел и может быть отключён через некоторое время, пожалуйста, избегайте его использования.")]
		public ReadOnlyCollection<Photo> GetProfile(long? ownerId = null, IEnumerable<long> photoIds = null, bool? rev = null, bool? extended = null, string feedType = null, DateTime? feed = null, bool? photoSizes = null, ulong? count = null, ulong? offset = null)
		{
			var parameters = new VkParameters
				{
					{"owner_id", ownerId},
					{"photo_ids", photoIds},
					{"rev", rev},
					{"extended", extended},
					{"feed_type", feedType},
					{"feed", feed},
					{"photo_sizes", photoSizes},
					{"count", count},
					{"offset", offset}
				};

			VkResponseArray response = _vk.Call("photos.getProfile", parameters);

			return response.ToReadOnlyCollectionOf<Photo>(x => x);
		}

		/// <summary>
		/// Возвращает информацию о фотографиях по их идентификаторам.
		/// </summary>
		/// <param name="photos">Перечисленные через запятую идентификаторы, которые представляют собой идущие через знак подчеркивания id пользователей, разместивших фотографии, и id самих фотографий. Чтобы получить информацию о фотографии в альбоме группы, вместо id пользователя следует указать -id группы.
		/// <example>
		/// Пример значения photos: 1_129207899,6492_135055734, -20629724_271945303
		/// </example>
		/// <remarks>
		/// Некоторые фотографии, идентификаторы которых могут быть получены через API, закрыты приватностью, и не будут получены. В этом случае следует использовать ключ доступа фотографии (access_key) в её идентификаторе. Пример значения photos: 1_129207899_220df2876123d3542f, 6492_135055734_e0a9bcc31144f67fbd
		///
		/// Поле access_key будет возвращено вместе с остальными данными фотографии в методах, которые возвращают фотографии, закрытые приватностью но доступные в данном контексте. Например данное поле имеют фотографии, возвращаемые методом newsfeed.get. список строк, разделенных через запятую, обязательный параметр
		/// </remarks>
		/// </param>
		/// <param name="extended">True — будут возвращены дополнительные поля likes, comments, tags, can_comment, can_repost. Поля comments и tags содержат только количество объектов. По умолчанию данные поля не возвращается</param>
		/// <param name="photoSizes">Возвращать ли доступные размеры фотографии в специальном формате</param>
		/// <returns>После успешного выполнения возвращает список объектов photo</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.getById"/>.
		/// </remarks>
		[ApiMethodName("photos.getById", Skip = true)]
		[ApiVersion("5.37")]
		public ReadOnlyCollection<Photo> GetById(IEnumerable<string> photos, bool? extended = null, bool? photoSizes = null)
		{
			var parameters = new VkParameters
				{
					{"photos", photos},
					{"extended", extended},
					{"photo_sizes", photoSizes}
				};

			VkResponseArray response = _vk.Call("photos.getById", parameters);

			return response.ToReadOnlyCollectionOf<Photo>(x => x);
		}

		/// <summary>
		/// Возвращает адрес сервера для загрузки фотографий.
		/// </summary>
		/// <param name="albumId">Идентификатор альбома</param>
		/// <param name="groupId">Идентификатор сообщества, которому принадлежит альбом (если необходимо загрузить фотографию в альбом сообщества)</param>
		/// <returns>После успешного выполнения возвращает объект <see cref="UploadServerInfo"/></returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.getUploadServer"/>.
		/// </remarks>
		[ApiVersion("5.42")]
		public UploadServerInfo GetUploadServer(long albumId, long? groupId = null)
		{
			var parameters = new VkParameters
				{
					{"album_id", albumId},
					{"group_id", groupId}
				};

			return _vk.Call("photos.getUploadServer", parameters);
		}

		/// <summary>
		/// Возвращает адрес сервера для загрузки фотографии на страницу пользователя.
		/// </summary>
		/// <returns>После успешного выполнения возвращает объект с единственным полем upload_url. </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.getProfileUploadServer"/>.
		/// </remarks>
		[Obsolete("Данный метод устарел и может быть отключён через некоторое время, пожалуйста, избегайте его использования. Используйте метод GetOwnerPhotoUploadServer")]
		public UploadServerInfo GetProfileUploadServer()
		{
			return GetOwnerPhotoUploadServer();
		}

		/// <summary>
		/// Возвращает адрес сервера для загрузки фотографии на страницу пользователя.
		/// </summary>
		/// <param name="ownerId">Идентификатор сообщества или текущего пользователя.</param>
		/// <returns>
		/// После успешного выполнения возвращает объект с единственным полем upload_url.
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.getOwnerPhotoUploadServer" />.
		/// </remarks>
		[ApiVersion("5.42")]
		public UploadServerInfo GetOwnerPhotoUploadServer(long? ownerId = null)
		{
			var parameters = new VkParameters
				{
					{"owner_id", ownerId}
				};
			return _vk.Call("photos.getOwnerPhotoUploadServer", parameters);
		}

		/// <summary>
		/// Позволяет получить адрес для загрузки фотографий мультидиалогов.
		/// </summary>
		/// <param name="chatId">Идентификатор беседы, для которой нужно загрузить фотографию</param>
		/// <param name="cropX">Положительное число</param>
		/// <param name="cropY">Положительное число</param>
		/// <param name="cropWidth">Ширина фотографии после обрезки в px, минимальное значение 200</param>
		/// <returns>После успешного выполнения возвращает объект с единственным полем upload_url. </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.getChatUploadServer"/>.
		/// </remarks>
		[ApiMethodName("photos.getChatUploadServer", Skip = true)]
		[ApiVersion("5.37")]
		public UploadServerInfo GetChatUploadServer(ulong chatId, ulong? cropX = null, ulong? cropY = null, ulong? cropWidth = null)
		{
			var parameters = new VkParameters
				{
					{ "chat_id", chatId },
					{ "crop_x", cropX },
					{ "crop_y", cropY },
					{ "crop_width", cropWidth }
				};

			return _vk.Call("photos.getChatUploadServer", parameters);
		}

		/// <summary>
		/// Сохраняет фотографию пользователя после успешной загрузки.
		/// </summary>
		/// <param name="server">Параметр, возвращаемый в результате загрузки фотографии на сервер.</param>
		/// <param name="hash">Параметр, возвращаемый в результате загрузки фотографии на сервер.</param>
		/// <param name="photo">Параметр, возвращаемый в результате загрузки фотографии на сервер.</param>
		/// <returns>После успешного выполнения возвращает объект, содержащий поля photo_hash и photo_src (при работе через VK.api метод вернёт поля photo_src, photo_src_big, photo_src_small). Параметр photo_hash необходим для подтверждения пользователем изменения его фотографии через вызов метода saveProfilePhoto Javascript API. Поле photo_src содержит путь к загруженной фотографии. </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.saveProfilePhoto"/>.
		/// </remarks>
		[ApiMethodName("photos.saveProfilePhoto", Skip = true)]
		[ApiVersion("5.9")]
		[Obsolete("Данный метод устарел и может быть отключён через некоторое время, пожалуйста, избегайте его использования. Используйте метод SaveOwnerPhoto")]
		public Photo SaveProfilePhoto(string server = null, string hash = null, string photo = null)
		{
			var response = @"{
				""server"": " + server + @"
				""photo"":" + photo + @"
				""hash"": " + hash + @"
			}";
			return SaveOwnerPhoto(response);
		}

		/// <summary>
		/// Сохраняет фотографию пользователя после успешной загрузки.
		/// </summary>
		/// <param name="response">Параметр, возвращаемый в результате загрузки фотографии на сервер.</param>
		/// <returns>После успешного выполнения возвращает объект, содержащий поля photo_hash и photo_src (при работе через VK.api метод вернёт поля photo_src, photo_src_big, photo_src_small). Параметр photo_hash необходим для подтверждения пользователем изменения его фотографии через вызов метода saveProfilePhoto Javascript API. Поле photo_src содержит путь к загруженной фотографии. </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.saveOwnerPhoto"/>.
		/// </remarks>
		[ApiVersion("5.42")]
		public Photo SaveOwnerPhoto(string response)
		{
			var responseJson = JObject.Parse(response);
			var server = responseJson["server"].ToString();
			var hash = responseJson["hash"].ToString();
			var photo = responseJson["photo"].ToString();
			var parameters = new VkParameters
				{
					{ "server", server },
					{ "hash", hash },
					{ "photo", photo }
				};

			return _vk.Call("photos.saveOwnerPhoto", parameters);
		}

		/// <summary>
		/// Сохраняет фотографии после успешной загрузки на URI, полученный методом <see cref="GetWallUploadServer"/>.
		/// </summary>
		/// <param name="userId">Идентификатор пользователя, на стену которого нужно сохранить фотографию</param>
		/// <param name="groupId">Идентификатор сообщества, на стену которого нужно сохранить фотографию</param>
		/// <param name="response">Параметр, возвращаемый в результате загрузки фотографии на сервер</param>
		// <returns>После успешного выполнения возвращает массив с загруженной фотографией, возвращённый объект имеет поля id, pid, aid, owner_id, src, src_big, src_small, created. В случае наличия фотографий в высоком разрешении также будут возвращены адреса с названиями src_xbig и src_xxbig. </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.saveWallPhoto"/>.
		/// </remarks>
		[ApiMethodName("photos.saveWallPhoto", Skip = true)]
		[ApiVersion("5.42")]
		public ReadOnlyCollection<Photo> SaveWallPhoto(string response, ulong? userId = null, ulong? groupId = null)
		{
			var responseJson = JObject.Parse(response);
			var server = responseJson["server"].ToString();
			var hash = responseJson["hash"].ToString();
			var photo = responseJson["photo"].ToString();
			var parameters = new VkParameters
				{
					{ "user_id", userId },
					{ "group_id", groupId },
					{ "photo", photo },
					{ "server", server },
					{ "hash", hash }
				};

			VkResponseArray responseVk = _vk.Call("photos.saveWallPhoto", parameters);
			return responseVk.ToReadOnlyCollectionOf<Photo>(x => x);
		}

		/// <summary>
		/// Возвращает адрес сервера для загрузки фотографии на стену пользователя или сообщества.
		/// </summary>
		/// <param name="groupId">Идентификатор сообщества, на стену которого нужно загрузить фото (без знака «минус»)</param>
		/// <returns>После успешного выполнения возвращает объект <see cref="UploadServerInfo"/>.</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.getWallUploadServer"/>.
		/// </remarks>
		[ApiMethodName("photos.getWallUploadServer", Skip = true)]
		[ApiVersion("5.42")]
		public UploadServerInfo GetWallUploadServer(long? groupId = null)
		{
			var parameters = new VkParameters
				{
					{"group_id", groupId}
				};

			return _vk.Call("photos.getWallUploadServer", parameters);
		}

		/// <summary>
		/// Возвращает адрес сервера для загрузки фотографии в личное сообщение пользователю.
		/// </summary>
		/// <returns>После успешного выполнения возвращает объект <see cref="UploadServerInfo"/>.</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.getMessagesUploadServer"/>.
		/// </remarks>
		[ApiVersion("5.42")]
		public UploadServerInfo GetMessagesUploadServer()
		{
			return _vk.Call("photos.getMessagesUploadServer", VkParameters.Empty);
		}

		/// <summary>
		/// Сохраняет фотографию после успешной загрузки на URI, полученный методом <see cref="GetMessagesUploadServer"/>.
		/// </summary>
		/// <param name="response">Параметр, возвращаемый в результате загрузки фотографии на сервер</param>
		/// <returns>После успешного выполнения возвращает массив с загруженной фотографией, возвращённый объект имеет поля id, pid, aid, owner_id, src, src_big, src_small, created. В случае наличия фотографий в высоком разрешении также будут возвращены адреса с названиями src_xbig и src_xxbig. </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.saveMessagesPhoto"/>.
		/// </remarks>
		[ApiVersion("5.42")]
		public ReadOnlyCollection<Photo> SaveMessagesPhoto(string response)
		{
			var responseJson = JObject.Parse(response);
			var server = responseJson["server"].ToString();
			var hash = responseJson["hash"].ToString();
			var photo = responseJson["photo"].ToString();
			var parameters = new VkParameters
				{
					{ "photo", photo },
					{ "hash", hash },
					{ "server", server }
				};
			VkResponseArray result = _vk.Call("photos.saveMessagesPhoto", parameters);
			return result.ToReadOnlyCollectionOf<Photo>(x => x);
		}

		/// <summary>
		/// Позволяет пожаловаться на фотографию.
		/// </summary>
		/// <param name="ownerId">Идентификатор пользователя или сообщества, которому принадлежит фотография</param>
		/// <param name="photoId">Идентификатор фотографии</param>
		/// <param name="reason">Тип жалобы</param>
		/// <returns>После успешного выполнения возвращает true.</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.report"/>.
		/// </remarks>
		[ApiMethodName("photos.report", Skip = true)]
		[ApiVersion("5.37")]
		public bool Report(long ownerId, ulong photoId, ReportReason reason)
		{
			var parameters = new VkParameters
				{
					{"owner_id", ownerId},
					{"photo_id", photoId},
					{"reason", reason}
				};

			return _vk.Call("photos.report", parameters);
		}

		/// <summary>
		/// Позволяет пожаловаться на комментарий к фотографии.
		/// </summary>
		/// <param name="ownerId">Идентификатор владельца фотографии к которой оставлен комментарий</param>
		/// <param name="commentId">Идентификатор комментария</param>
		/// <param name="reason">Тип жалобы</param>
		/// <returns>После успешного выполнения возвращает true.</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.reportComment"/>.
		/// </remarks>
		[ApiMethodName("photos.reportComment", Skip = true)]
		[ApiVersion("5.37")]
		public bool ReportComment(long ownerId, ulong commentId, ReportReason reason)
		{
			var parameters = new VkParameters
				{
					{"owner_id", ownerId},
					{"comment_id", commentId},
					{"reason", reason}
				};

			return _vk.Call("photos.reportComment", parameters);
		}

		/// <summary>
		/// Осуществляет поиск изображений по местоположению или описанию.
		/// </summary>
		/// <param name="count">Количество альбомов.</param>
		/// <param name="params">Параметры запроса.</param>
		/// <returns>
		/// После успешного выполнения возвращает список объектов фотографий.
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.search" />.
		/// </remarks>
		[ApiVersion("5.37")]
		public ReadOnlyCollection<Photo> Search(out int count, PhotoSearchParams @params)
		{
			var response = _vk.Call("photos.search", @params, true);
			count = response["count"];
			return response["items"].ToReadOnlyCollectionOf<Photo>(x => x);
		}

		/// <summary>
		/// Сохраняет фотографии после успешной загрузки.
		/// </summary>
		/// <param name="params">Параметры запроса.</param>
		/// <returns>
		/// После успешного выполнения возвращает список объектов <see cref="Photo" />.
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.save" />.
		/// </remarks>
		[ApiVersion("5.42")]
		public ReadOnlyCollection<Photo> Save(PhotoSaveParams @params)
		{
			VkResponseArray response = _vk.Call("photos.save", @params);
			return response.ToReadOnlyCollectionOf<Photo>(x => x);
		}

		/// <summary>
		/// Позволяет скопировать фотографию в альбом "Сохраненные фотографии"
		/// </summary>
		/// <param name="ownerId">Идентификатор владельца фотографии</param>
		/// <param name="photoId">Индентификатор фотографии</param>
		/// <param name="accessKey">Специальный код доступа для приватных фотографий</param>
		/// <returns>Возвращает идентификатор созданной фотографии.</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.copy"/>.
		/// </remarks>
		[ApiMethodName("photos.copy", Skip = true)]
		[ApiVersion("5.37")]
		public long Copy(long ownerId, ulong photoId, string accessKey = null)
		{
			var parameters = new VkParameters
				{
					{"owner_id", ownerId},
					{"photo_id", photoId},
					{"access_key", accessKey}
				};

			return _vk.Call("photos.copy", parameters);
		}

		/// <summary>
		/// Изменяет описание у выбранной фотографии.
		/// </summary>
		/// <param name="params">Параметры запроса.</param>
		/// <returns>После успешного выполнения возвращает <c>true</c>.</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.edit"/>.
		/// </remarks>
		[ApiMethodName("photos.edit", Skip = true)]
		[ApiVersion("5.37")]
		public bool Edit(PhotoEditParams @params)
		{
			return _vk.Call("photos.edit", @params);
		}

		/// <summary>
		/// Переносит фотографию из одного альбома в другой.
		/// </summary>
		/// <param name="ownerId">Идентификатор пользователя или сообщества, которому принадлежит фотография. Обратите внимание, идентификатор сообщества в параметре owner_id необходимо указывать со знаком "-" — например, owner_id=-1 соответствует идентификатору сообщества ВКонтакте API (club1)</param>
		/// <param name="targetAlbumId">Идентификатор альбома, в который нужно переместить фотографию</param>
		/// <param name="photoId">Идентификатор фотографии, которую нужно перенести</param>
		/// <returns>После успешного выполнения возвращает <c>true</c>.</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.move"/>.
		/// </remarks>
		[ApiMethodName("photos.move", Skip = true)]
		[ApiVersion("5.37")]
		public bool Move(long targetAlbumId, ulong photoId, long? ownerId = null)
		{
			var parameters = new VkParameters
				{
					{"owner_id", ownerId},
					{"target_album_id", targetAlbumId},
					{"photo_id", photoId}
				};

			return _vk.Call("photos.move", parameters);
		}

		/// <summary>
		/// Делает фотографию обложкой альбома.
		/// </summary>
		/// <param name="ownerId">Идентификатор пользователя или сообщества, которому принадлежит фотография. Обратите внимание, идентификатор сообщества в параметре owner_id необходимо указывать со знаком "-" — например, owner_id=-1 соответствует идентификатору сообщества ВКонтакте API (club1)</param>
		/// <param name="photoId">Идентификатор фотографии</param>
		/// <param name="albumId">Идентификатор альбома</param>
		/// <returns>После успешного выполнения возвращает <c>true</c>.</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.makeCover"/>.
		/// </remarks>
		[ApiMethodName("photos.makeCover", Skip = true)]
		[ApiVersion("5.37")]
		public bool MakeCover(ulong photoId, long? ownerId = null, long? albumId = null)
		{
			var parameters = new VkParameters
				{
					{"owner_id", ownerId},
					{"photo_id", photoId},
					{"album_id", albumId}
				};

			return _vk.Call("photos.makeCover", parameters);
		}

		/// <summary>
		/// Меняет порядок альбома в списке альбомов пользователя.
		/// </summary>
		/// <param name="ownerId">Идентификатор пользователя или сообщества, которому принадлежит альбом. Обратите внимание, идентификатор сообщества в параметре owner_id необходимо указывать со знаком "-" — например, owner_id=-1 соответствует идентификатору сообщества ВКонтакте API (club1)</param>
		/// <param name="albumId">Идентификатор альбома</param>
		/// <param name="before">Идентификатор альбома, перед которым следует поместить альбом</param>
		/// <param name="after">Идентификатор альбома, после которого следует поместить альбом</param>
		/// <returns>После успешного выполнения возвращает <c>true</c>.</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.reorderAlbums"/>.
		/// </remarks>
		[ApiMethodName("photos.reorderAlbums", Skip = true)]
		[ApiVersion("5.37")]
		public bool ReorderAlbums(long albumId, long? ownerId = null, long? before = null, long? after = null)
		{
			var parameters = new VkParameters
				{
					{"owner_id", ownerId},
					{"album_id", albumId},
					{"before", before},
					{"after", after}
				};

			return _vk.Call("photos.reorderAlbums", parameters);
		}

		/// <summary>
		/// Меняет порядок фотографии в списке фотографий альбома пользователя.
		/// </summary>
		/// <param name="ownerId">Идентификатор пользователя или сообщества, которому принадлежит фотография. Обратите внимание, идентификатор сообщества в параметре owner_id необходимо указывать со знаком "-" — например, owner_id=-1 соответствует идентификатору сообщества ВКонтакте API (club1)</param>
		/// <param name="photoId">Идентификатор фотографии</param>
		/// <param name="before">Идентификатор фотографии, перед которой следует поместить фотографию</param>
		/// <param name="after">Идентификатор фотографии, после которой следует поместить фотографию</param>
		/// <returns>После успешного выполнения возвращает true.</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.reorderPhotos"/>.
		/// </remarks>
		[ApiMethodName("photos.reorderPhotos", Skip = true)]
		[ApiVersion("5.37")]
		public bool ReorderPhotos(ulong photoId, long? ownerId = null, long? before = null, long? after = null)
		{
			var parameters = new VkParameters
				{
					{"owner_id", ownerId},
					{"photo_id", photoId},
					{"before", before},
					{"after", after}
				};

			return _vk.Call("photos.reorderPhotos", parameters);
		}

		/// <summary>
		/// Возвращает все фотографии пользователя или сообщества в антихронологическом порядке.
		/// </summary>
		/// <param name="count">Количество пользователей, которым нравится текущая фотография.</param>
		/// <param name="params">Параметры запроса.</param>
		/// <returns>
		/// После успешного выполнения возвращает список объектов <see cref="Photo" />.
		/// <remarks>
		/// Если был задан параметр extended — будет возвращено поле likes:
		/// user_likes: 1 — текущему пользователю нравится данная фотография, 0 - не указано.
		/// count — количество пользователей, которым нравится текущая фотография.
		/// Если был задан параметр photo_sizes=1, вместо полей width и height возвращаются размеры копий фотографии в специальном формате.
		/// </remarks>
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.getAll" />.
		/// </remarks>
		[ApiVersion("5.37")]
		public ReadOnlyCollection<Photo> GetAll(out int count, PhotoGetAllParams @params)
		{
			var response = _vk.Call("photos.getAll", @params);
			count = response["count"];
			return response["items"].ToReadOnlyCollectionOf<Photo>(x => x);
		}

		/// <summary>
		/// Возвращает список фотографий, на которых отмечен пользователь
		/// </summary>
		/// <param name="count">Количество.</param>
		/// <param name="params">Параметры запроса.</param>
		/// <returns>После успешного выполнения возвращает список объектов photo. </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.getUserPhotos"/>.
		/// </remarks>
		[ApiMethodName("photos.getUserPhotos")]
		[VkValue("userId", 178964623)]
		[VkValue("count", 2)]
		[VkValue("offset", 3)]
		[ApiVersion("5.37")]
		public ReadOnlyCollection<Photo> GetUserPhotos(out int count, PhotoGetUserPhotosParams @params)
		{
			var response = _vk.Call("photos.getUserPhotos", @params);
			count = response["count"];
			return response["items"].ToReadOnlyCollectionOf<Photo>(x => x);
		}

		/// <summary>
		/// Удаляет указанный альбом для фотографий у текущего пользователя
		/// </summary>
		/// <param name="albumId">Идентификатор альбома</param>
		/// <param name="groupId">Идентификатор сообщества, в котором размещен альбом.</param>
		/// <returns>После успешного выполнения возвращает true.</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.deleteAlbum"/>.
		/// </remarks>
		[ApiVersion("5.37")]
		public bool DeleteAlbum(long albumId, long? groupId = null)
		{
			var parameters = new VkParameters
				{
					{"album_id", albumId},
					{"group_id", groupId}
				};

			return _vk.Call("photos.deleteAlbum", parameters);
		}

		/// <summary>
		/// Удаление фотографии на сайте.
		/// </summary>
		/// <param name="ownerId">Идентификатор пользователя или сообщества, которому принадлежит фотография. Обратите внимание, идентификатор сообщества в параметре owner_id необходимо указывать со знаком "-" — например, owner_id=-1 соответствует идентификатору сообщества ВКонтакте API (club1)</param>
		/// <param name="photoId">Идентификатор фотографии</param>
		/// <returns>После успешного выполнения возвращает true.</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.delete"/>.
		/// </remarks>
		[ApiMethodName("photos.delete", Skip = true)]
		[ApiVersion("5.37")]
		public bool Delete(ulong photoId, long? ownerId = null)
		{
			var parameters = new VkParameters
				{
					{ "owner_id", ownerId },
					{ "photo_id", photoId }
				};

			return _vk.Call("photos.delete", parameters);
		}

		/// <summary>
		/// Восстанавливает удаленную фотографию.
		/// </summary>
		/// <param name="ownerId">Идентификатор пользователя или сообщества, которому принадлежит фотография. Обратите внимание, идентификатор сообщества в параметре owner_id необходимо указывать со знаком "-" — например, owner_id=-1 соответствует идентификатору сообщества ВКонтакте API (club1)</param>
		/// <param name="photoId">Идентификатор фотографии</param>
		/// <returns>После успешного выполнения возвращает <c>true</c>.</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.restore"/>.
		/// </remarks>
		[ApiMethodName("photos.restore", Skip = true)]
		[ApiVersion("5.37")]
		public bool Restore(ulong photoId, long? ownerId = null)
		{
			var parameters = new VkParameters
				{
					{ "owner_id", ownerId },
					{ "photo_id", photoId }
				};

			return _vk.Call("photos.restore", parameters);
		}

		/// <summary>
		/// Подтверждает отметку на фотографии.
		/// </summary>
		/// <param name="ownerId">Идентификатор пользователя или сообщества, которому принадлежит фотография. Обратите внимание, идентификатор сообщества в параметре owner_id необходимо указывать со знаком "-" — например, owner_id=-1 соответствует идентификатору сообщества ВКонтакте API (club1)</param>
		/// <param name="photoId">Идентификатор фотографии</param>
		/// <param name="tagId">Идентификатор отметки на фотографии</param>
		/// <returns>После успешного выполнения возвращает <c>true</c>.</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.confirmTag"/>.
		/// </remarks>
		[ApiMethodName("photos.confirmTag", Skip = true)]
		[ApiVersion("5.37")]
		public bool ConfirmTag(ulong photoId, ulong tagId, long? ownerId = null)
		{
			var parameters = new VkParameters
				{
					{"owner_id", ownerId},
					{"photo_id", photoId},
					{"tag_id", tagId}
				};

			return _vk.Call("photos.confirmTag", parameters);
		}

		/// <summary>
		/// Возвращает список комментариев к фотографии.
		/// </summary>
		/// <param name="count">Количество.</param>
		/// <param name="params">Параметры запроса.</param>
		/// <returns>
		/// После успешного выполнения возвращает список объектов <see cref="Comment" />.
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.getComments" />.
		/// </remarks>
		[ApiMethodName("photos.getComments")]
		[VkValue("owner_id", 1)]
		[VkValue("photo_id", 263219735)]
		[ApiVersion("5.37")]
		public ReadOnlyCollection<Comment> GetComments(out int count, PhotoGetCommentsParams @params)
		{
			var response = _vk.Call("photos.getComments", @params);
			count = response["count"];
			return response["items"].ToReadOnlyCollectionOf<Comment>(x => x);
		}

		/// <summary>
		/// Возвращает отсортированный в антихронологическом порядке список всех комментариев к конкретному альбому или ко всем альбомам пользователя.
		/// </summary>
		/// <param name="count">Количество комментариев</param>
		/// <param name="params">Параметры запроса.</param>
		/// <returns>
		/// После успешного выполнения возвращает список объектов <see cref="Comment" />.
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.getAllComments" />.
		/// </remarks>
		[ApiMethodName("photos.getAllComments", Skip = true)]
		[ApiVersion("5.37")]
		public ReadOnlyCollection<Comment> GetAllComments(out int count, PhotoGetAllCommentsParams @params)
		{
			var response = _vk.Call("photos.getAllComments", @params);
			count = response["count"];
			return response["items"].ToReadOnlyCollectionOf<Comment>(x => x);
		}

		/// <summary>
		/// Создает новый комментарий к фотографии.
		/// </summary>
		/// <param name="params">Параметры запроса.</param>
		/// <returns>
		/// После успешного выполнения возвращает идентификатор созданного комментария.
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.createComment" />.
		/// </remarks>
		[ApiMethodName("photos.createComment", Skip = true)]
		[ApiVersion("5.37")]
		public long CreateComment(PhotoCreateCommentParams @params)
		{
			return _vk.Call("photos.createComment", @params);
		}

		/// <summary>
		/// Удаляет комментарий к фотографии.
		/// </summary>
		/// <param name="ownerId">Идентификатор пользователя или сообщества, которому принадлежит фотография. Обратите внимание, идентификатор сообщества в параметре owner_id необходимо указывать со знаком "-" — например, owner_id=-1 соответствует идентификатору сообщества ВКонтакте API (club1)</param>
		/// <param name="commentId">Идентификатор комментария</param>
		/// <returns>После успешного выполнения возвращает true (false, если комментарий не найден).
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.deleteComment"/>.
		/// </remarks>
		[ApiMethodName("photos.deleteComment", Skip = true)]
		[ApiVersion("5.37")]
		public bool DeleteComment(ulong commentId, long? ownerId = null)
		{
			var parameters = new VkParameters
				{
					{ "owner_id", ownerId },
					{ "comment_id", commentId }
				};

			return _vk.Call("photos.deleteComment", parameters);
		}

		/// <summary>
		/// Восстанавливает удаленный комментарий к фотографии.
		/// </summary>
		/// <param name="ownerId">Идентификатор пользователя или сообщества, которому принадлежит фотография. Обратите внимание, идентификатор сообщества в параметре owner_id необходимо указывать со знаком "-" — например, owner_id=-1 соответствует идентификатору сообщества ВКонтакте API (club1)</param>
		/// <param name="commentId">Идентификатор удаленного комментария</param>
		/// <returns>После успешного выполнения возвращает true (false, если комментарий с таким идентификатором не является удаленным).</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.restoreComment"/>.
		/// </remarks>
		[ApiMethodName("photos.restoreComment", Skip = true)]
		[ApiVersion("5.37")]
		public long RestoreComment(ulong commentId, long? ownerId = null)
		{
			var parameters = new VkParameters
				{
					{"owner_id", ownerId},
					{"comment_id", commentId}
				};

			return _vk.Call("photos.restoreComment", parameters);
		}

		/// <summary>
		/// Изменяет текст комментария к фотографии.
		/// </summary>
		/// <param name="ownerId">Идентификатор пользователя или сообщества, которому принадлежит фотография. Обратите внимание, идентификатор сообщества в параметре owner_id необходимо указывать со знаком "-" — например, owner_id=-1 соответствует идентификатору сообщества ВКонтакте API (club1)</param>
		/// <param name="commentId">Идентификатор комментария</param>
		/// <param name="message">Новый текст комментария (является обязательным, если не задан параметр attachments)</param>
		/// <param name="attachments">Новый список объектов, приложенных к комментарию и разделённых символом ",". Поле attachments представляется в формате: &lt;type&gt;&lt;owner_id&gt;_&lt;media_id&gt;,&lt;type&gt;&lt;owner_id&gt;_&lt;media_id&gt; &lt;type&gt; — тип медиа-вложения:
		/// photo — фотография
		/// video — видеозапись
		/// audio — аудиозапись
		/// doc — документ
		/// &lt;owner_id&gt; — идентификатор владельца медиа-вложения
		/// &lt;media_id&gt; — идентификатор медиа-вложения.
		/// <example>
		/// Например:
		/// photo100172_166443618,photo66748_265827614
		/// </example>
		/// Параметр является обязательным, если не задан параметр message. список строк, разделенных через запятую</param>
		/// <returns>После успешного выполнения возвращает true.</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.editComment"/>.
		/// </remarks>
		[ApiMethodName("photos.editComment", Skip = true)]
		[ApiVersion("5.37")]
		public bool EditComment(ulong commentId, string message, long? ownerId = null, IEnumerable<MediaAttachment> attachments = null)
		{
			var parameters = new VkParameters
				{
					{"owner_id", ownerId},
					{"comment_id", commentId},
					{"message", message},
					{"attachments", attachments}
				};

			return _vk.Call("photos.editComment", parameters);
		}

		/// <summary>
		/// Возвращает список отметок на фотографии.
		/// </summary>
		/// <param name="ownerId">Идентификатор пользователя или сообщества, которому принадлежит фотография. Обратите внимание, идентификатор сообщества в параметре owner_id необходимо указывать со знаком "-" — например, owner_id=-1 соответствует идентификатору сообщества ВКонтакте API (club1)</param>
		/// <param name="photoId">Идентификатор фотографии</param>
		/// <param name="accessKey">Строковый ключ доступа, который може быть получен при получении объекта фотографии</param>
		/// <returns>После успешного выполнения возвращает массив объектов <see cref="Tag"/>.</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.getTags"/>.
		/// </remarks>
		[ApiMethodName("photos.getTags", Skip = true)]
		[ApiVersion("5.37")]
		public ReadOnlyCollection<Tag> GetTags(ulong photoId, long? ownerId = null, string accessKey = null)
		{
			var parameters = new VkParameters
				{
					{"owner_id", ownerId},
					{"photo_id", photoId},
					{"access_key", accessKey}
				};

			VkResponseArray response = _vk.Call("photos.getTags", parameters);

			return response.ToReadOnlyCollectionOf<Tag>(x => x);
		}

		/// <summary>
		/// Добавляет отметку на фотографию.
		/// </summary>
		/// <param name="params">Параметры запроса.</param>
		/// <returns>
		/// После успешного выполнения возвращает идентификатор созданной отметки.
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.putTag" />.
		/// </remarks>
		[ApiMethodName("photos.putTag", Skip = true)]
		[ApiVersion("5.37")]
		public ulong PutTag(PhotoPutTagParams @params)
		{
			return _vk.Call("photos.putTag", @params);
		}

		/// <summary>
		/// Удаляет отметку с фотографии.
		/// </summary>
		/// <param name="ownerId">Идентификатор пользователя или сообщества, которому принадлежит фотография. Обратите внимание, идентификатор сообщества в параметре owner_id необходимо указывать со знаком "-" — например, owner_id=-1 соответствует идентификатору сообщества ВКонтакте API (club1)</param>
		/// <param name="photoId">Идентификатор фотографии</param>
		/// <param name="tagId">Идентификатор отметки</param>
		/// <returns>После успешного выполнения возвращает <c>true</c>.</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.removeTag"/>.
		/// </remarks>
		[ApiMethodName("photos.removeTag", Skip = true)]
		[ApiVersion("5.37")]
		public bool RemoveTag(ulong tagId, ulong photoId, long? ownerId = null)
		{
			var parameters = new VkParameters
				{
					{"owner_id", ownerId},
					{"photo_id", photoId},
					{"tag_id", tagId}
				};

			return _vk.Call("photos.removeTag", parameters);
		}

		/// <summary>
		/// Возвращает список фотографий, на которых есть непросмотренные отметки.
		/// </summary>
		/// <param name="countTotal">Общее количество.</param>
		/// <param name="offset">Смещение, необходимое для получения определённого подмножества фотографий</param>
		/// <param name="count">Количество фотографий, которые необходимо вернуть. положительное число, максимальное значение 100, по умолчанию 20</param>
		/// <returns>
		/// После успешного выполнения возвращает список объектов <see cref="Photo" />.
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <seealso cref="https://vk.com/dev/photos.getNewTags" />.
		/// </remarks>
		[ApiMethodName("photos.getNewTags", Skip = true)]
		[ApiVersion("5.37")]
		public ReadOnlyCollection<Photo> GetNewTags(out int countTotal, uint? offset = null, uint? count = null)
		{
			var parameters = new VkParameters
				{
					{"offset", offset},
					{"count", count}
				};

			var response = _vk.Call("photos.getNewTags", parameters);
			countTotal = response["count"];
			return response["items"].ToReadOnlyCollectionOf<Photo>(x => x);
		}

		/// <summary>
		/// Возвращает адрес сервера для загрузки фотографии товаров сообщества.
		/// </summary>
		/// <param name="groupId">Идентификатор сообщества, для которого необходимо загрузить фотографию товара. целое число (целое число).</param>
		/// <param name="mainPhoto">Является ли фотография обложкой товара  (1 — фотография для обложки, 0 — дополнительная фотография) флаг, может принимать значения 1 или 0 (флаг, может принимать значения 1 или 0).</param>
		/// <param name="cropX">Координата x для обрезки фотографии. положительное число (положительное число).</param>
		/// <param name="cropY">Координата y для обрезки фотографии. положительное число (положительное число).</param>
		/// <param name="cropWidth">Ширина фотографии после обрезки в px. положительное число, минимальное значение 200 (положительное число, минимальное значение 200).</param>
		/// <returns>
		/// После успешного выполнения возвращает объект с единственным полем upload_url.
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <see href="http://vk.com/dev/photos.getMarketUploadServer" />.
		/// </remarks>
		[ApiVersion("5.42")]
		public UploadServerInfo GetMarketUploadServer(long groupId, bool? mainPhoto = null, long? cropX = null, long? cropY = null, long? cropWidth = null)
		{
			var parameters = new VkParameters {
				{ "group_id", groupId },
				{ "main_photo", mainPhoto },
				{ "crop_x", cropX },
				{ "crop_y", cropY },
				{ "crop_width", cropWidth }
			};

			return _vk.Call("photos.getMarketUploadServer", parameters);
		}


		/// <summary>
		/// Возвращает адрес сервера для загрузки фотографии подборки товаров в сообществе.
		/// </summary>
		/// <param name="groupId">Идентификатор сообщества, для которого необходимо загрузить фотографию подборки товаров. целое число (целое число).</param>
		/// <returns>
		/// .
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <see href="http://vk.com/dev/photos.getMarketAlbumUploadServer" />.
		/// </remarks>
		[ApiVersion("5.42")]
		public UploadServerInfo GetMarketAlbumUploadServer(long groupId)
		{
			var parameters = new VkParameters {
				{ "group_id", groupId }
			};

			return _vk.Call("photos.getMarketAlbumUploadServer", parameters);
		}


		/// <summary>
		/// Сохраняет фотографии после успешной загрузки на URI, полученный методом photos.getMarketUploadServer.
		/// </summary>
		/// <param name="groupId">Идентификатор группы, для которой нужно загрузить фотографию. положительное число (положительное число).</param>
		/// <param name="response">Параметр, возвращаемый в результате загрузки фотографии на сервер. строка, обязательный параметр (строка, обязательный параметр).</param>
		/// <returns>
		/// После успешного выполнения возвращает массив, содержащий объект с загруженной фотографией.
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <see href="http://vk.com/dev/photos.saveMarketPhoto" />.
		/// </remarks>
		[ApiVersion("5.42")]
		public ReadOnlyCollection<Photo> SaveMarketPhoto(long groupId, string response)
		{
			var responseJson = JObject.Parse(response);
			var server = responseJson["server"].ToString();
			var hash = responseJson["hash"].ToString();
			var photo = responseJson["photo"].ToString();
			var cropData = responseJson["crop_data"].ToString();
			var cropHash = responseJson["crop_hash"].ToString();
			var parameters = new VkParameters {
				{ "group_id", groupId },
				{ "photo", photo },
				{ "server", server },
				{ "hash", hash },
				{ "crop_data", cropData },
				{ "crop_hash", cropHash }
			};

			return _vk.Call("photos.saveMarketPhoto", parameters).ToReadOnlyCollectionOf<Photo>(x => x);
		}


		/// <summary>
		/// Сохраняет фотографии после успешной загрузки на URI, полученный методом photos.getMarketAlbumUploadServer.
		/// </summary>
		/// <param name="groupId">Идентификатор группы, для которой нужно загрузить фотографию. положительное число, обязательный параметр (положительное число, обязательный параметр).</param>
		/// <param name="response">Параметр, возвращаемый в результате загрузки фотографии на сервер. строка, обязательный параметр (строка, обязательный параметр).</param>
		/// <returns>
		/// После успешного выполнения возвращает массив, содержащий объект с загруженной фотографией.
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <see href="http://vk.com/dev/photos.saveMarketAlbumPhoto" />.
		/// </remarks>
		[ApiVersion("5.42")]
		public ReadOnlyCollection<Photo> SaveMarketAlbumPhoto(long groupId, string response)
		{
			var responseJson = JObject.Parse(response);
			var server = responseJson["server"].ToString();
			var hash = responseJson["hash"].ToString();
			var photo = responseJson["photo"].ToString();
			var parameters = new VkParameters {
				{ "group_id", groupId },
				{ "photo", photo },
				{ "server", server },
				{ "hash", hash }
			};

			return _vk.Call("photos.saveMarketAlbumPhoto", parameters).ToReadOnlyCollectionOf<Photo>(x => x);
		}
	}
}