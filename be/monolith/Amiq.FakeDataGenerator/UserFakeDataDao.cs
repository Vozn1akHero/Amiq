using Amiq.DataAccess.Models.Models;
using System.Transactions;

namespace Amiq.FakeDataGenerator
{
    internal class UserFakeDataDao : IFakeDataGenerator
    {
        private AmiqContext amiqContext = new AmiqContext();

        private List<(int sex, string name, string surname)> namesAndSurnames = new List<(int sex, string name, string surname)>();

        public void ReadNamesFromFile(int rows)
        {
            var femaleNames = File.ReadLines(System.IO.Path.GetFullPath(@"..\..\..\") + "female-names.txt");
            var maleNames = File.ReadLines(System.IO.Path.GetFullPath(@"..\..\..\") + "male-names.txt");
            var surnames = File.ReadLines(System.IO.Path.GetFullPath(@"..\..\..\") + "surnames.txt");

            int maxRan = new List<int> { femaleNames.Count(), maleNames.Count(), surnames.Count() }.Min();

            foreach (int i in Enumerable.Range(1, rows))
            {
                int randNameSurname = new Random().Next(0, maxRan);
                int curSex = new Random().Next(0, 2);
                namesAndSurnames.Add((
                    curSex, 
                    curSex == 0 ? maleNames.ElementAt(randNameSurname) : femaleNames.ElementAt(randNameSurname),
                    surnames.ElementAt(randNameSurname)
                ));
            }
        }

        public void GenerateFakeData()
        {
            //using var t = new CommittableTransaction(new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });
            using var t = amiqContext.Database.BeginTransaction();
            try
            {
                var users = new List<User>();
                int index = 0;
                foreach(var item in namesAndSurnames)
                {
                    index++;
                    users.Add(new User { 
                        Login = Path.GetRandomFileName().Replace(".", "").Substring(0, 8),
                        Name = item.name,
                        Surname = item.surname,
                        Sex = item.sex == 0 ? "M" : "F",
                        Password = BCrypt.Net.BCrypt.HashPassword("123pass"),
                        Birthdate = DateTime.Now.AddYears(-new Random().Next(20, 50)),
                        Email = $"test{index}@test.com",
                        AvatarPath = $"thispersondoesnotexistcom{new Random().Next(1, 23)}.jpg",
                        ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
                    });
                }
                amiqContext.Users.AddRange(users);
                amiqContext.SaveChanges();
                t.Commit();
            } catch (Exception ex)
            {
                t.Rollback();
            }
        }
    }
}