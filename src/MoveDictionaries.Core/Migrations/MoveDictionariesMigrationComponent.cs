#if NETFRAMEWORK
using Umbraco.Web.Trees;
using Umbraco.Core.Composing;
using MoveDictionaries.Core.Helpers;

namespace MoveDictionaries.Core.Compositions
{
	public class MoveDictionariesMigrationComponent : IComponent
	{
		public void Initialize()
		{
			TreeControllerBase.MenuRendering += TreeControllerBase_MenuRendering;
		}

		void TreeControllerBase_MenuRendering(TreeControllerBase sender, MenuRenderingEventArgs e)
		{
			if (sender.TreeAlias == "dictionary" && e.NodeId != "-1")
			{
				e.Menu.Items.Insert(2, MenuItemHelper.CreateMenuItem());
			}
		}
		public void Terminate()
		{
			TreeControllerBase.MenuRendering -= TreeControllerBase_MenuRendering;
		}
	}
}
#endif