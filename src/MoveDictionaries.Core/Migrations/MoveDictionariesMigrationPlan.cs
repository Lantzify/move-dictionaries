#if NETCOREAPP
using Umbraco.Cms.Infrastructure.Migrations;
#else
using Umbraco.Core.Migrations;
#endif

namespace MoveDictionaries.Core.Migrations
{
	public class MoveDictionariesMigrationPlan : MigrationPlan
	{
		public MoveDictionariesMigrationPlan() : base("MoveDictionaries")
		{
			
		}
	}
}
