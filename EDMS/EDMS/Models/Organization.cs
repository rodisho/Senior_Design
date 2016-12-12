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
        public string Address { get; set; }
        public string YearFounded { get; set; }
        public string WhoFounded { get; set; }
        public string ReasonForFounding { get; set; }
        public string TaxExemptNonProfitStatus { get; set; }
        public string MissionStatement { get; set; }
        public string KeyWords { get; set; }
        public string KeyActivities { get; set; }
        public long NumberOfEmployees { get; set; }
        public long NumberOfVolunteers { get; set; }
        public long NumberOfProjectPartners { get; set; }
        public decimal Budget { get; set; }
        public string CommunitiesNeighborhoodZipcode { get; set; }
        public string Constituency { get; set; }
        public string FundingSoureces { get; set; }
        public decimal FundingAmount { get; set; }
        public bool isVisible { get; set; }
        public string PartnerOrganizations { get; set; }
        public string CreatedBy { get; set; }
        public Organization()
        {
            isVisible = false;
        }

    }

    

    
}