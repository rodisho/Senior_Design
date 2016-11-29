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
        public string City { get; set; }
        [Display(Name="Zip Code")]
        public string ZipCode { get; set; }
        public string Website { get; set; }
        [Display(Name = "Funding Agencies")]
        public string FundingAgencies { get; set; }
        [Display(Name = "Year Founded")]
        public string YearFounded { get; set; }
        public string CreatedBy { get; set; }
    }
}
