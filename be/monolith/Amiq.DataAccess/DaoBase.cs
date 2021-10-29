using Amiq.DataAccess.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccess
{
    public class DaoBase
    {
        protected AmiqContext AmiqContext { get; set; } = new AmiqContext();
    }
}
