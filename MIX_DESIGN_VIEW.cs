using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entity
{
    public class MIX_DESIGN_VIEW  //: MixDesign_Strength
    {
        public decimal TM_ID { get; set; } 
        public string TM_CODE { get; set; }
        public string GRADE_NAME { get; set; }  
        public string TRIAL_DATE { get; set; }
        public string Client_Name { get; set; }
        public string ADDED_BY_CODE { get; set; }
        public string ADDED_BY { get; set; }
        public decimal CUBIC_METER { get; set; }
        public int IS_FILE_UPLOAD { get; set; }
        public string FILE_PATH { get; set; }
        public decimal? Sc_Ref_ID { get; set; }
        public List<MIX_DESIGN_DTL> mixDesignDtlList { get; set; }

       public List<MIX_DESIGN_REMARKS> mixDesignRemarksList { get; set; }

      //----------- Added By : Rohit : 19/02/2024 ----------
       public decimal TMS_ID { get; set; }
       public int IS_LOCKED { get; set; }
       public decimal? SAR_1_DAYS { get; set; }
       public decimal? SAR_3_DAYS { get; set; }
       public decimal? SAR_7_DAYS { get; set; }
       public decimal? SAR_14_DAYS { get; set; }
       public decimal? SAR_28_DAYS { get; set; }
       public decimal? IS_1_DAYS { get; set; }
       public decimal? IS_3_DAYS { get; set; }
       public decimal? IS_7_DAYS { get; set; }
       public decimal? IS_14_DAYS { get; set; }
       public decimal? IS_28_DAYS { get; set; }
      //-----------  Rohit End ----------
    
    }
}
