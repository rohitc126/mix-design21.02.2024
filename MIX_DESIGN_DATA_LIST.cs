using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entity
{
   public class MIX_DESIGN_DATA_LIST
    {
        public decimal TM_ID { get; set; }
        public string TM_CODE { get; set; }
        public string TRIAL_DATE { get; set; }
        public string Grade_Name { get; set; }
        public string Client_Name { get; set; }
        public decimal CUBIC_METER { get; set; } 
        public string ADDED_BY_CODE { get; set; }
        public string ADDED_BY { get; set; }
        public decimal? Sc_Ref_ID { get; set; }
        public int? IS_LOCKED { get; set; }
        
    }
}
