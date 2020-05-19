using Comic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comic.LocalDatabase
{
    public class UserHistoryDb
    {
        public static void CreateIfNotExistLocalDatabase()
        {
            using (LocalDatabaseContext context = new LocalDatabaseContext(LocalDatabaseContext.DBConnectionString))
            {
                if (!context.DatabaseExists())
                {
                    context.CreateDatabase();
                }
            }
        }
        
        public void Insert(UserHistories userHistory)
        {
            using (LocalDatabaseContext context = new LocalDatabaseContext(LocalDatabaseContext.DBConnectionString))
            {
                var deletedItems = from History in context.UserHistories
                                  where History.NovelId == userHistory.NovelId
                                  select History;
                context.UserHistories.DeleteAllOnSubmit(deletedItems);
                context.UserHistories.InsertOnSubmit(userHistory);
                context.SubmitChanges();
            }
        }

        public IEnumerable<UserHistories> GetAllHistoryNovels()
        {
            using (LocalDatabaseContext context = new LocalDatabaseContext(LocalDatabaseContext.DBConnectionString))
            {
                return context.UserHistories
                    .OrderByDescending(item=>item.TimeCreated)
                    .ToList<UserHistories>();
            }
        }

        public void DeleteAllItems()
        {
            using (LocalDatabaseContext context = new LocalDatabaseContext(LocalDatabaseContext.DBConnectionString))
            {
                var allHistoryItems=context.UserHistories.ToList<UserHistories>();
                context.UserHistories.DeleteAllOnSubmit(allHistoryItems);
                context.SubmitChanges();
            }
        }
    }
}
