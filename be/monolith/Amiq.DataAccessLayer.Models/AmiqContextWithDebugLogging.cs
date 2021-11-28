using Amiq.DataAccessLayer.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccessLayer.Models
{
    public sealed class AmiqContextWithDebugLogging : AmiqContext
    {
        public readonly ILoggerFactory AmiqLoggerFactory;

        public AmiqContextWithDebugLogging()
        {
            AmiqLoggerFactory = LoggerFactory.Create(builder => { builder.AddProvider(new DebugLoggerProvider()); });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLoggerFactory(AmiqLoggerFactory);
        }
    }
}
