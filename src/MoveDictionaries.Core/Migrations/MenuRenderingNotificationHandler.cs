#if NETCOREAPP
using Umbraco.Cms.Core.Events;
using MoveDictionaries.Core.Helpers;
using Umbraco.Cms.Core.Notifications;

namespace MoveDictionaries.Core.Migrations
{
	public class MenuRenderingNotificationHandler : INotificationHandler<MenuRenderingNotification>
	{
		public void Handle(MenuRenderingNotification notification)
		{
			if (notification.TreeAlias == "dictionary" && notification.NodeId != "-1")
			{
				notification.Menu.Items.Insert(2, MenuItemHelper.CreateMenuItem());
			}
		}
	}
}
#endif