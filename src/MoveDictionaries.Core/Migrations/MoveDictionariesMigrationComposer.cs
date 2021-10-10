#if NETCOREAPP
using MoveDictionaries.Core.Migrations;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
#else
using Umbraco.Core;
using Umbraco.Core.Composing;
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
	[RuntimeLevel(MinLevel =RuntimeLevel.Run)]
	public class MoveDictionariesMigrationComposer : ComponentComposer<MoveDictionariesMigrationComponent>, IUserComposer
	{
		public override void Compose(Composition composition)
		{
			composition.Components().Append<MoveDictionariesMigrationComponent>();
		}
	}
#endif
}