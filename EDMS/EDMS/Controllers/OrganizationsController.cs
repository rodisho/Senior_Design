﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EDMS.Models;
using EDMS.ViewModels;

namespace EDMS.Controllers
{
    public class OrganizationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Organizations
        public ActionResult Index()
        {
            string query = "";
            if (User.IsInRole("User"))
            {
                query = "SELECT orgs.Id,orgs.Name, orgs.MissionStatement, orgs.CreatedBy, CASE WHEN orgs.AddressIsVisible = 'False' THEN ' ' WHEN orgs.AddressIsVisible = 'True'	THEN Address END AS Address, CASE WHEN orgs.YearFoundedIsVisible = 'False' THEN ' ' WHEN orgs.YearFoundedIsVisible = 'True'	THEN YearFounded END AS YearFounded FROM Organizations orgs WHERE orgs.IsVisible = 'true' OR orgs.CreatedBy = '" + User.Identity.Name + "'";
            }
            else
            if(User.IsInRole("Admin"))
            {
                query = "SELECT orgs.Id,orgs.Name, orgs.MissionStatement, orgs.CreatedBy, CASE WHEN orgs.AddressIsVisible = 'False' THEN ' ' WHEN orgs.AddressIsVisible = 'True'	THEN Address END AS Address, CASE WHEN orgs.YearFoundedIsVisible = 'False' THEN ' ' WHEN orgs.YearFoundedIsVisible = 'True'	THEN YearFounded END AS YearFounded FROM Organizations orgs";
            }
            else
            {
                query = "SELECT orgs.Id,orgs.Name, orgs.MissionStatement, orgs.CreatedBy, CASE WHEN orgs.AddressIsVisible = 'False' THEN ' ' WHEN orgs.AddressIsVisible = 'True'	THEN Address END AS Address, CASE WHEN orgs.YearFoundedIsVisible = 'False' THEN ' ' WHEN orgs.YearFoundedIsVisible = 'True'	THEN YearFounded END AS YearFounded FROM Organizations orgs WHERE IsVisible = 'true'";

            }
            IEnumerable<OrgInfoGroup> data = db.Database.SqlQuery<OrgInfoGroup>(query);

            return View(data.ToList());
        }

      

        // GET: Organizations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        [Authorize]
        public ActionResult Survey()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Survey([Bind(Include = "Id,Name,Address, YearFounded, PhoneNumber, WebPage, Email, WhoFounded, ReasonForFounding,TaxExemptNonProfitStatus, MissionStatement, KeyWords, KeyActivities, NumberOfEmployees, NumberOfVolunteers, NumberOfProjectPartners, Budget, CommunitiesNeighborhoodZipcode, FundingSoureces, Constituency, FundingSources, FundingAmount, PartnerOrganizations, OrganizationsAware, CollaboratedYesNo, Strengths, AddressIsVisible, YearFoundedIsVisible")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                organization.AddressIsVisible = true;
                organization.YearFoundedIsVisible = true;
                db.Database.ExecuteSqlCommand("UPDATE AspNetUsers SET EmailConfirmed = 'True' WHERE UserName = '" + User.Identity.Name + "'");
                db.SaveChanges();     
                organization.isVisible = false;
                organization.CompletedSurvey = true;
               
                organization.CreatedBy = User.Identity.Name.ToString();
                db.Organizations.Add(organization);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(organization);
        }

        // GET: Organizations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Organizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address, YearFounded, WhoFounded, ReasonForFounding,TaxExemptNonProfitStatus, MissionStatement, KeyWords, KeyActivities, NumberOfEmployees, NumberOfVolunteers, NumberOfProjectPartners, Budget, CommunitiesNeighborhoodZipcode, FundingSoureces, Constituency, FundingSources, FundingAmount, PartnerOrganizations, AddressIsVisible, YearFoundedIsVisible")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                organization.AddressIsVisible = true;
                organization.YearFoundedIsVisible = true;
                if(User.IsInRole("User"))
                {
                    organization.isVisible = false;
                }
                else
                if(User.IsInRole("Admin"))
                {
                    organization.isVisible = true;
                }
                organization.CreatedBy = User.Identity.Name.ToString();
                db.Organizations.Add(organization);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(organization);
        }

        [ChildActionOnly]
        public ActionResult _MenuPartial()
        {
            
            string query = "";

            query = "SELECT usr.EmailConfirmed FROM AspNetUsers usr WHERE usr.UserName = '" + User.Identity.Name + "'";
            IEnumerable<SurveyVerified> data = db.Database.SqlQuery<SurveyVerified>(query);
            if (!data.ToList().Any())
                return null;
            return PartialView(data.ToList()[0]);

        }

        // GET: Organizations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }

            if(organization.CreatedBy != User.Identity.Name && !User.IsInRole("Admin"))
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // POST: Organizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address, YearFounded, WhoFounded, ReasonForFounding,TaxExemptNonProfitStatus, MissionStatement, KeyWords, KeyActivities, NumberOfEmployees, NumberOfVolunteers, NumberOfProjectPartners, Budget, CommunitiesNeighborhoodZipcode, Constituency, FundingSoureces, FundingSources, FundingAmount, PartnerOrganizations, CreatedBy, AddressIsVisible, YearFoundedIsVisible, isVisible")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(organization);
        }

        // GET: Organizations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // POST: Organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Organization organization = db.Organizations.Find(id);
            db.Organizations.Remove(organization);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
