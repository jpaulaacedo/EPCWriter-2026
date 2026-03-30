using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPCReaderWriter.Models
{
    public class SubCategoryModel
    {
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PropertyNo { get; set; }
    }
}
