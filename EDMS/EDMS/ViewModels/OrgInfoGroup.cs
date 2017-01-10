using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EDMS.ViewModels
{
    public class OrgInfoGroup
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public bool AddressIsVisible { get; set; }
        public bool YearFoundedIsVisible { get; set; }
        public string Address
        {
            get;set;
        }
        public string YearFounded
        {
            get;set;
        }
        [Display(Name = "Mission Statement")]
        public string MissionStatement { get; set; }
        public string CreatedBy { get; set; }
    }
}
