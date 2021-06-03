using MoveDictionaries.Core.Dtos;
using Umbraco.Core.Services;
using Umbraco.Web.WebApi;
using System.Web.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using Umbraco.Core.Models;
using System.Linq;

namespace MoveDictionaries.Core.Controllers
{
	public class MoveDictionariesApiController : UmbracoAuthorizedApiController
	{
		private readonly ILocalizationService _localizationService;
		public MoveDictionariesApiController(ILocalizationService localizationService)
		{
			_localizationService = localizationService;
		}

		[HttpPost]
		public bool MoveDictionary(DictionaryDto dto)
		{
			try
			{
				var dictionary = _localizationService.GetDictionaryItemById(dto.Id);

				if (dto.ParentKey == Guid.Parse("00000000-0000-0000-0000-000000000000"))
					dictionary.ParentId = null;
				else
					dictionary.ParentId = dto.ParentKey;

				_localizationService.Save(dictionary);
			}
			catch (Exception ex)
			{
				return false;
			}

			return true;
		}
	}
}
