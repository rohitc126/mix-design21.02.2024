using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entity
{
     public class MixDesign_CAL
     {
         [DataType(DataType.Date)]
         [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
         public Nullable<System.DateTime> LTS_Test_DT { get; set; }
        
         public decimal? LTSS_ID { get; set; }
         public List<MIX_DESIGN_DATA_Dtl> MIX_DESIGN_DATA_Dtl_List { get; set; }   
    }
     public class MIX_DESIGN_DATA_Dtl
     {
         public decimal? LTS_ID { get; set; }
         public string TEST_CODE { get; set; }
         public decimal? Grade_Id { get; set; }
         public string custName { get; set; }
         public string TEST_NAME { get; set; }
         public string LabName { get; set; }
         public bool IsSelected { get; set; }
         public string hdnSelectedRowIds { get; set; }
     }
}
