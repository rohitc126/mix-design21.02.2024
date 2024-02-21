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
    public class VM_Mix_Design_Entry
    {

        [Required(ErrorMessage = "Select Trail Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> TrailDate { get; set; }

        public string TrailMixDesignNo { get; set; }

        [Required(ErrorMessage = "Select Grade")]
        public decimal? Grade_Id { get; set; }
        public SelectList Grade_List { get; set; }

        public string Client_Name { get; set; }

        public string Sc_Ref_No { get; set; }
        public decimal? Sc_Ref_ID { get; set; }

        [Required(ErrorMessage = "Enter Cubic Meter")]
        public decimal? Cubic_Meter { get; set; }

        public SelectList Material_List { get; set; }

        public SelectList Unit_List { get; set; }

        public SelectList Time_List { get; set; }
        public SelectList Collapse_LIST { get; set; }
        public bool IsSelected { get; set; }

        public List<VM_MixDesign_Material> Material_Data { get; set; }

        public List<VM_MixDesign_Remarks> Remarks_Data { get; set; }

        public HttpPostedFileBase UploadFile { get; set; }
      
        public VM_Mix_Design_Entry()
        {
            Unit_List = new SelectList(new List<SelectListItem>
                                    { 
                                        new SelectListItem { Text = "Kg", Value = "Kg" },
                                        new SelectListItem { Text = "Nos", Value = "Nos" },
                                        new SelectListItem { Text = "MT", Value = "MT" }, 
                                        new SelectListItem { Text = "Liter", Value = "Liter" }, 
 
                                    }, "Value", "Text");


            Time_List = new SelectList(new List<SelectListItem>
                                    { 
                                        new SelectListItem { Text = "Initial", Value = "Initial" },
                                        new SelectListItem { Text = "30 min", Value = "30" },
                                        new SelectListItem { Text = "60 min", Value = "60" },
                                        new SelectListItem { Text = "90 min", Value = "90" }, 
                                        new SelectListItem { Text = "120 min", Value = "120" }, 
                                        new SelectListItem { Text = "150 min", Value = "150" },
                                        new SelectListItem { Text = "180 min", Value = "180" },
                                        new SelectListItem { Text = "210 min", Value = "210" },
                                        new SelectListItem { Text = "270 min", Value = "270" },
                                        new SelectListItem { Text = "300 min", Value = "300" },
                                        new SelectListItem { Text = "315 min", Value = "315" },
                                        new SelectListItem { Text = "320 min", Value = "320" },
                                        new SelectListItem { Text = "350 min", Value = "350" },
 
                                    }, "Value", "Text");


            Collapse_LIST = new SelectList(new List<SelectListItem>
                            { 
                                new SelectListItem { Text = "Collapse", Value = "Collapse" }, 
                                new SelectListItem { Text = "", Value = "" }, 
                                     
                            }, "Value", "Text");

            Material_Data = new List<VM_MixDesign_Material>();
            Material_Data.Add(new VM_MixDesign_Material { SrNo = 1 });

            Remarks_Data = new List<VM_MixDesign_Remarks>();
            Remarks_Data.Add(new VM_MixDesign_Remarks { SrNo = 1 });

        }
    }
   
}
