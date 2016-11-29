using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace EDMS.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string FundingAgencies { get; set; }
        public decimal FundingAmount { get; set; }
        public string YearFounded { get; set; }
        public long NumberOfEmployees { get; set; }
        public bool isVisible { get; set; }
        public string PartnerOrganizations { get; set; }
        public string CreatedBy { get; set; }
        public Organization()
        {
            isVisible = false;
        }

    }

    

    
}