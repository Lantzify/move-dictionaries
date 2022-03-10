#if NETCOREAPP
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.DependencyInjection;
#else
using Umbraco.Core;
using Umbraco.Web.Trees;
using Umbraco.Core.Composing;
using MoveDictionaries.Core.Helpers;
#endif

namespace MoveDictionaries.Core.Compositions
{
#if NETCOREAPP
	public class MoveDictionariesMigrationComposer : IComposer
	{
		public void Compose(IUmbracoBuilder builder)
		{
			builder.AddNotificationHandler<MenuRenderingNotification, MenuRenderingNotificationHandler>();
		}
	}
#else
	[RuntimeLevel(MinLevel = RuntimeLevel.Run)]
	public class MoveDictionariesComposer : ComponentComposer<MoveDictionariesComponent>, IUserComposer
	{
		public override void Compose(Composition composition)
		{
			composition.Components().Append<MoveDictionariesComponent>();
		}
	}

	public class MoveDictionariesComponent : IComponent
	{
		public void Initialize()
		{
			TreeControllerBase.MenuRendering += TreeControllerBase_MenuRendering;
		}

		void TreeControllerBase_MenuRendering(TreeControllerBase sender, MenuRenderingEventArgs e)
		{
			if (sender.TreeAlias.Equals("dictionary") && e.NodeId != "-1")
			{
				e.Menu.Items.Insert(2, MenuItemHelper.CreateMenuItem());
			}
		}

		public void Terminate()
		{
			TreeControllerBase.MenuRendering -= TreeControllerBase_MenuRendering;
		}
	}
#endif
}