using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Web.Trees;
using Umbraco.Web.Models.Trees;

namespace MoveDictionaries.Core.Compositions
{
	public class TreeComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			composition.Components().Append<TreeComponent>();
		}
	}

	public class TreeComponent : IComponent
	{
		public void Initialize()
		{
			TreeControllerBase.MenuRendering += TreeControllerBase_MenuRendering;
		}

		void TreeControllerBase_MenuRendering(TreeControllerBase sender, MenuRenderingEventArgs e)
		{
			if (sender.TreeAlias == "dictionary" && e.NodeId != "-1")
			{
				var i = new MenuItem("dictionaryMove", "Move");
				i.Icon = "enter";
				i.SeparatorBefore = true;
				i.AdditionalData.Add("actionView", "/App_Plugins/MoveDictionaries/moveDictionaries.view.html");
				i.AdditionalData["jsAction"] = "moveDictionariesManager.dictionaryMove";
				i.OpensDialog = true;
				e.Menu.Items.Insert(2, i);
			}
		}
		public void Terminate()
		{
			TreeControllerBase.MenuRendering -= TreeControllerBase_MenuRendering;
		}
	}
}
