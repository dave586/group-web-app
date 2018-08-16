﻿using System;
using System.Collections.Generic;
//using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;
using WebApplication1.Models;
using GroupQuestionnaireApp;
using GroupQuestionnaireApp.EFModel;
using System.Web.Script.Serialization;

namespace WebApplication1.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class TestController : BaseController
    {
        private RequestContext _homeContext;

        public RequestContext HomeContext
        {
            set { _homeContext = value; }
        }

        public ActionResult StartTest (ClientSelectedProgram viewModel)
        {
            using (OQDevSNAPEntities dbContext = new OQDevSNAPEntities())
            {
                GroupProgram gp = dbContext.GroupPrograms.Include("GroupPackageActivity").FirstOrDefault(p => p.ID == viewModel.ProgramID);
                GroupPackageActivity gpa = dbContext.GroupPackageActivities.Include("GroupPackageName").FirstOrDefault(g => g.PackageType == viewModel.PackageType);
                foreach (string selectedProgram in viewModel.Questionnaires)
                {

                }
            }
            return null;
        }
        //public ActionResult StartTest(ClientSelectedProgram viewModel)
        //{
        //    using (OQDevSNAPEntities1 dbContext = new OQDevSNAPEntities1())
        //    {

        //        foreach (string selectedTest in viewModel.SelectedProgram)
        //        {
        //            GroupProgram pro = new GroupProgram();

        //            int proID = 0;
        //            int.TryParse(selectedTest, out proID);

        //            if (proID > 100)
        //            {
        //                pro.Program = dbContext.GroupPrograms.FirstOrDefault(p => p.ProgramName == "WAS");
        //                pro.ID = proID;
        //            }
        //        }
        //    }
        //    return NextTest();
        //}

        //public ActionResult NextTest (Guid responseID, RequestContext _requestContext)
        //{
        //    ModelState.Clear();
        //    using (OQDevSNAPEntities1 dbContext = new OQDevSNAPEntities1())
        //    {
        //        //GroupProgram pro = dbContext.GroupPrograms.Include('GroupPackageActivity').Include('GroupActivityType').FirstOrDefault()

        //        ClientTestDisplay ctd = new ClientTestDisplay();
        //        return View("~/Views/Test/CompleteTest.cshtml", ctd);
        //    }
        //}
    }
}