using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace BusinessLayer.Entity
{
    public class VM_MIX_DESIGN_LIST
    {

        public string TM_CODE { get; set; } 

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> From_DT { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> To_DT { get; set; }

        [Required(ErrorMessage = "Select Grade")]
        public decimal? Grade_Id { get; set; }
        public SelectList Grade_List { get; set; }

    }
}
