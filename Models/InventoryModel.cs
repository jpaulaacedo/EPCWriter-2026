using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPCReaderWriter
{
    public class InventoryModel
    {
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string Epc { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public DateTime DateAcquired { get; set; }
        public string PropertyNumber { get; set; }
        public string FundCluster { get; set; }
        public string UnitMeasure { get; set; }
        public string UnitValue { get; set; }
        public bool Serviceable { get; set; }
        public string QtyPhysicalCard { get; set; }
        public string ItemType { get; set; }
        public int? QtyPhysicalCount { get; set; }
        public int? Lifespan { get; set; }
        public int? UserId { get; set; }
        public int? LocationId { get; set; }
    }
}