using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPCReaderWriter.Models
{
    public class DatabaseConnectionResultModel
    {
        public bool Connected { get; set; }
        public DatabaseConnectionErrorModel[] Errors { get; set; }
    }
}
