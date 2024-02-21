using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Entity
{
    public class VM_MixDesign_Remarks
    {
        public int SrNo { get; set; }
        public string Description { get; set; }
        public string Time { get; set; }
        public string Value { get; set; }
        public string Slump { get; set; }
        public string Unit { get; set; }
        public string Notes { get; set; }
        public bool IsSelected { get; set; }
    }
}
