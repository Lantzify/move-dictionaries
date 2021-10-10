#if NETCOREAPP
using Umbraco.Cms.Core.Models.Trees;
#else
using Umbraco.Web.Models.Trees;
#endif



namespace MoveDictionaries.Core.Helpers
{
	public class MenuItemHelper
	{
		public static MenuItem CreateMenuItem()
		{
			var menuItem = new MenuItem("dictionaryMove", "Move")
			{
				Icon = "enter",
				SeparatorBefore = true,
				OpensDialog = true,
			};

			menuItem.AdditionalData.Add("actionView", "/App_Plugins/MoveDictionaries/moveDictionaries.view.html");
			menuItem.AdditionalData["jsAction"] = "moveDictionariesManager.dictionaryMove";

			return menuItem;
		}
	}
}