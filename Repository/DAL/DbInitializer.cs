using System.Data.Entity;

namespace Repository.DAL
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<DBEntityContext>
    {
        protected override void Seed(DBEntityContext context)
        {
            base.Seed(context);
        }
    }
}
