using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPCReaderWriter.Helpers;

namespace EPCReaderWriter.Repositories
{
    public abstract class RepositoryBase
    {
        protected string ConnectionString { get; set; }

        public RepositoryBase()
        {
            ConnectionString = ConnectionStringBuilder.GetConnectionString();
        }
    }
}
