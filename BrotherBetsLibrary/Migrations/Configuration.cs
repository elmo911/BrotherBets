using System.Data.Entity.Migrations;
using BrotherBetsLibrary.Data;
using BrotherBetsLibrary.Models;

namespace BrotherBetsLibrary.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BrotherBetContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BrotherBetContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Brothers.AddOrUpdate(b=> b.Name, 
                new Brother {Name = "Josh" },
                new Brother {Name = "Sean" },
                new Brother {Name = "Brandon" },
                new Brother {Name = "Jacob" },
                new Brother {Name = "Hieu" },
                new Brother {Name = "Jonathan" });
            context.SaveChanges();
        }
    }
}
