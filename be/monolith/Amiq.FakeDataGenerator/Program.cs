using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.FakeDataGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
        }

        private static void GenerateUsers()
        {
            var dao = new UserFakeDataDao();
            dao.ReadNamesFromFile(500);
            dao.GenerateFakeData();
        }
    }
}
