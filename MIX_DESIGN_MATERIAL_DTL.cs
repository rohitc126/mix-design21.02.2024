using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entity
{
    public class MIX_DESIGN_DTL
    {
        public decimal TMD_ID { get; set; }
        public decimal TM_ID { get; set; }
        public int MATERIAL_ID { get; set; }
        public string Material_Name { get; set; }
        public string BRAND { get; set; }
        public string UNIT { get; set; }
        public decimal SSD_MIX { get; set; }
        public decimal ABSORPTION { get; set; }
        public decimal MOISTURE { get; set; }
        public decimal CORRECTION_VALUE { get; set; }
        public decimal CORRECT_MIX { get; set; }
        public decimal TRIAL_BATCH { get; set; }
        public string MATERIAL_BATCH_NO { get; set; }
    }
}
