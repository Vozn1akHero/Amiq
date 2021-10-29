using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.FakeDataGenerator
{
    internal interface IFakeDataGenerator
    {
        void ReadNamesFromFile(int rows);
        void GenerateFakeData();
    }
}
