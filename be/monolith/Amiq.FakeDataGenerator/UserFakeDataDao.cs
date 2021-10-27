using Amiq.DataAccess.Models.Models;
using System.Transactions;

namespace Amiq.FakeDataGenerator
{
    internal class UserFakeDataDao : IFakeDataGenerator
    {
        private AmiqContext amiqContext = new AmiqContext();

        private List<(string name, string surname)> namesAndSurnames = new List<(string name, string surname)>();


        public void ReadNamesFromFile()
        {

        }

        public async Task GenerateFakeDataAsync(int rows)
        {
            using var t = new CommittableTransaction(new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });
            try
            {
                var users = new List<User>();
                foreach (int i in Enumerable.Range(1, rows))
                {
                    users.Add(new User());
                }
                t.Commit();
            } catch (Exception ex)
            {
                t.Rollback();
            }
        }
    }
}