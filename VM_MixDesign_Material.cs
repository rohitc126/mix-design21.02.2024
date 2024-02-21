using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entity
{
    public class VM_MixDesign_Material
    {
        public int SrNo { get; set; }
        public int? Material_Id { get; set; }
        public string Brand { get; set; }
        public string Unit { get; set; }
        public decimal? SSD_Mix { get; set; }
        public decimal? Absorption { get; set; }
        public decimal? Moisture { get; set; }
        public decimal? Correction_Value { get; set; }
        public decimal? Correct_Mix { get; set; }
        public decimal? TRIAL_BATCH { get; set; }
        public string MaterialBatch_No { get; set; } 
    }
}
