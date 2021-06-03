using System;

namespace MoveDictionaries.Core.Dtos
{
	public class DictionaryDto
	{
		public string Name { get; set; }
		public int Id { get; set; }
		public Guid Key { get; set; }
		public Guid ParentKey { get; set; }
		public bool HasChildren { get; set; }
	}
}