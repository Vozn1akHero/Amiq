using Amiq.DataAccessLayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccessLayer.Utils
{
    class DaoBase
    {
        protected AmiqContext _amiqContext = new AmiqContext();
    }
}
