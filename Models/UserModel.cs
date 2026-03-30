using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPCReaderWriter.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}