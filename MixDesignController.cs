using BusinessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using BusinessLayer.DAL;
using System.Configuration; 

namespace eSGBIZ.Controllers
{
    public class MixDesignController : BaseController
    {

        #region Rohit
        [Authorize]
        public ActionResult MixDesignCal()
        {
            ViewBag.Header = "Mix Design";
            return View();
        }

        public ActionResult _MixDesignCal_Dtl(decimal? LTS_ID, string LTS_Test_DT)
        {
            List<MIX_DESIGN_DATA_Dtl> dtl = new DAL_MIX_DESIGN().LAB_TEST_SCHEDULE_LIST(LTS_ID);
            MixDesign_CAL SCTEST = new MixDesign_CAL();
            SCTEST.MIX_DESIGN_DATA_Dtl_List = dtl;
            return PartialView("_MixDesignCal_Dtl", SCTEST);
        }

        [HttpPost]
        [SubmitButtonSelector(Name = "OK")]
        [ActionName("_MixDesignCal_Dtl")]
        public ActionResult _MixDesignCal_Dtl_Save(MixDesign_CAL SCTEST)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                try
                {

                    SCTEST.LTS_Test_DT = SCTEST.LTS_Test_DT.HasValue
               ? new DateTime(
                 SCTEST.LTS_Test_DT.Value.Year,
                 SCTEST.LTS_Test_DT.Value.Month,
                 SCTEST.LTS_Test_DT.Value.Day,
                 DateTime.Now.Hour,
                 DateTime.Now.Minute,
                 DateTime.Now.Second)
             : DateTime.Now;
                    TempData["LTSS_ID"] = SCTEST.LTSS_ID;

                    TempData["LTS_Test_DT"] = SCTEST.LTS_Test_DT.Value.ToString("dd MMMM yyyy hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                    return RedirectToAction("MixDesign_Entry", "MixDesign", new { LTS_ID = SCTEST.LTSS_ID });
                }
                catch (Exception ex)
                {
                    Danger(string.Format("<b>Error:</b>" + ex.Message), true);
                }
            }
            else
            {
                Danger(string.Format("<b>Error:102 :</b>" + string.Join("; ", ModelState.Values.SelectMany(z => z.Errors).Select(z => z.ErrorMessage))), true);
            }
            return View(SCTEST);
        }

        [Authorize]
        public ActionResult MixDesign_Entry(decimal? LTS_ID)
        {
            ViewBag.Header = "Mix Design Entry";
            VM_Mix_Design_Entry _mixDesign = new VM_Mix_Design_Entry();
            try
            {
                object LTSS_ID_Object = TempData.Peek("LTSS_ID");
                string LTSS_ID = LTSS_ID_Object != null ? LTSS_ID_Object.ToString() : null;
                object LTS_Test_DT_Object = TempData.Peek("LTS_Test_DT");
                string LTS_Test_DT = LTS_Test_DT_Object != null ? LTS_Test_DT_Object.ToString() : null;

                _mixDesign.TrailDate = !string.IsNullOrEmpty(LTS_Test_DT)
              ? DateTime.ParseExact(LTS_Test_DT, "dd MMMM yyyy hh:mm tt", System.Globalization.CultureInfo.InvariantCulture)
              : (DateTime?)null;

                if (!string.IsNullOrEmpty(LTS_ID.ToString()))
                {
                    MIX_DESIGN_DATA_Dtl labTestScheduleList = new DAL_MIX_DESIGN().LAB_TEST_SCHEDULE(LTS_ID);

                    _mixDesign.Sc_Ref_ID = labTestScheduleList.LTS_ID;
                    _mixDesign.Sc_Ref_No = labTestScheduleList.TEST_CODE;
                    _mixDesign.Client_Name = labTestScheduleList.custName;
                    _mixDesign.Grade_Id = labTestScheduleList.Grade_Id;
                }

            }
            catch (Exception ex)
            {
                Danger("<b>Exception occurred.</b>", true);

            }
            List<GradeMaster> gradeList = new DAL_Common().GetGradeList();
            _mixDesign.Grade_List = new SelectList(gradeList, "Grade_Id", "Grade_Name");
            List<MaterialMaster> materialList = new DAL_Common().GetMaterialList();
            _mixDesign.Material_List = new SelectList(materialList, "Material_Id", "Material_Name");
            return View(_mixDesign);
        }

        [HttpPost]
        [SubmitButtonSelector(Name = "Skip")]
        [ActionName("_MixDesignCal_Dtl")]
        public ActionResult MixDesign_Entry_Skip(MixDesign_CAL SCTEST)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                try
                {
                    MixDesign_CAL nullSCTest = new MixDesign_CAL
                    {
                        LTSS_ID = null,
                        LTS_Test_DT = null,

                    };
                    return RedirectToAction("MixDesign_Entry", new { LTS_ID = SCTEST.LTSS_ID = (decimal?)null });
                }
                catch (Exception ex)
                {
                    Danger(string.Format("<b>Error:</b>" + ex.Message), true);
                }
            }

            else
            {
                Danger(string.Format("<b>Error:102 :</b>" + string.Join("; ", ModelState.Values.SelectMany(z => z.Errors).Select(z => z.ErrorMessage))), true);
            }
            return View(SCTEST);
        }



        /*
        public ActionResult MixDesign_Entry()
        {
            ViewBag.Header = "Mix Design Entry";
            VM_Mix_Design_Entry _mixDesign = new VM_Mix_Design_Entry();

            List<GradeMaster> gradeList = new DAL_Common().GetGradeList();
            _mixDesign.Grade_List = new SelectList(gradeList, "Grade_Id", "Grade_Name");

            List<MaterialMaster> materialList = new DAL_Common().GetMaterialList();
            _mixDesign.Material_List = new SelectList(materialList, "Material_Id", "Material_Name");

            return View(_mixDesign);
        }*/
       

        #endregion

       


        [HttpPost]
        [SubmitButtonSelector(Name = "Save")]
        [ActionName("MixDesign_Entry")]
        public ActionResult MixDesign_Entry_Save(VM_Mix_Design_Entry _mixDesign)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                try
                {
                    ResultMessage objMst;
                    string result = new DAL_MIX_DESIGN().INSERT_MIX_DESIGN(emp.Employee_Code, _mixDesign, out objMst);

                    if (result == "")
                    {
                        Success(string.Format("<b>Mix design inserted successfully. Sample No. : </b> <b style='color:red'> " + objMst.CODE + "</b>"), true);
                        return RedirectToAction("MixDesign_Entry", "MixDesign");
                    }
                    else
                    {
                        Danger(string.Format("<b>Error:</b>" + result), true);
                    }
                }
                catch (Exception ex)
                {
                    Danger(string.Format("<b>Error:</b>" + ex.Message), true);
                }
            }
            else
            {
                Danger(string.Format("<b>Error:102 :</b>" + string.Join("; ", ModelState.Values.SelectMany(z => z.Errors).Select(z => z.ErrorMessage))), true);
            }

            List<GradeMaster> gradeList = new DAL_Common().GetGradeList();
            _mixDesign.Grade_List = new SelectList(gradeList, "Grade_Id", "Grade_Name");

            List<MaterialMaster> materialList = new DAL_Common().GetMaterialList();
            _mixDesign.Material_List = new SelectList(materialList, "Material_Id", "Material_Name");


            return View(_mixDesign);
        }


        [Authorize]
        public ActionResult MixDesignList()
        {
            ViewBag.Header = "Mix Design";
            VM_MIX_DESIGN_LIST _mixDesign = new VM_MIX_DESIGN_LIST();

            List<GradeMaster> gradeList = new DAL_Common().GetGradeList();
            _mixDesign.Grade_List = new SelectList(gradeList, "Grade_Id", "Grade_Name"); 

            return View(_mixDesign);

        }

        public ActionResult _MixDesign_List()
        {
            return PartialView("_MixDesign_List");
        }

        [HttpPost]
        public ActionResult _MixDesign_Data_List(string grade, string tCode, DateTime fDate, DateTime tDate)
        {
            // Server Side Processing
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];
            int totalRow = 0;

            VM_MIX_DESIGN_LIST _mixDesign = new VM_MIX_DESIGN_LIST();
            List<MIX_DESIGN_DATA_LIST> MixDesignDatas = new List<MIX_DESIGN_DATA_LIST>();
            try
            {
                if (string.IsNullOrEmpty( grade))
                {
                    grade = "0"; 
                }
                _mixDesign.Grade_Id = Convert.ToInt32( grade);
                _mixDesign.TM_CODE = tCode;
                _mixDesign.From_DT = fDate;
                _mixDesign.To_DT = tDate;

                MixDesignDatas = new DAL_MIX_DESIGN().Select_MixDesign_Data_List(_mixDesign);

                totalRow = MixDesignDatas.Count();

            }
            catch (Exception ex)
            {
                //logger.Error(ex, "Error : _CNs_Data_List ", ex.Message);
                Danger(string.Format("<b>Exception occured.</b>"), true);
            }

            if (!string.IsNullOrEmpty(searchValue)) // Filter Operation
            {
                MixDesignDatas = MixDesignDatas.
                    Where(x => x.TM_CODE.ToLower().Contains(searchValue.ToLower()) || x.Grade_Name.ToLower().Contains(searchValue.ToLower())
                        || x.Client_Name.ToLower().Contains(searchValue.ToLower())
                         ).ToList<MIX_DESIGN_DATA_LIST>();

            }
            int totalRowFilter = MixDesignDatas.Count();
            // sorting
            //sievesDatas = sievesDatas.OrderBy(sortColumnName + " " + sortDirection).ToList<SIEVE_ANALYSIS_DATA_LIST>();

            // Paging
            if (length == -1)
            {
                length = totalRow;
            }
            MixDesignDatas = MixDesignDatas.Skip(start).Take(length).ToList<MIX_DESIGN_DATA_LIST>();

            var jsonResult = Json(new { data = MixDesignDatas, draw = Request["draw"], recordsTotal = totalRow, recordsFiltered = totalRowFilter }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult MixDesign_View(decimal TM_ID)
        {
            MIX_DESIGN_VIEW _objMixDesign = new MIX_DESIGN_VIEW();
            _objMixDesign = new DAL_MIX_DESIGN().VIEW_MIX_DESIGN(TM_ID);
            return PartialView("MixDesign_View", _objMixDesign);

        }

        public FileResult ShowDocument(string FilePath)
        {
            string DMS_Path = ConfigurationManager.AppSettings["DMSPATH"].ToString();
            string directoryPath = DMS_Path + "MIX_DESIGN\\" + FilePath;

            //return File(Server.MapPath("~/Files/") + FilePath, GetMimeType(FilePath));
            return File(directoryPath, GetMimeType(FilePath));
        }

        private string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }



        public ActionResult MixDesign_Edit(decimal TM_ID)
        {
            MIX_DESIGN_VIEW _objMixDesign = new MIX_DESIGN_VIEW();
            _objMixDesign = new DAL_MIX_DESIGN().VIEW_MIX_DESIGN(TM_ID);
            return PartialView("MixDesign_Edit", _objMixDesign);

        }
        [HttpPost]
        [SubmitButtonSelector(Name = "Update")]
        [ActionName("MixDesign_Edit")]
        public ActionResult MixDesign_Strength_Update(MIX_DESIGN_VIEW _objEdit)
        {
         
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                try
                {
                    ResultMessage objMst;
                    string result = new DAL_MIX_DESIGN().UPDATE_MIX_DESIGN_STRENGTH(emp.Employee_Code, _objEdit, out objMst);

                    if (result == "")
                    {
                        Success(string.Format("<b>Mix Design Strength  successfully Updated. <b style='color:red'> " + "</b>"), true);
                        return RedirectToAction("MixDesign_Edit", "MixDesign", new { TM_ID = objMst.ID });
                    }
                    else
                    {
                        Danger(string.Format("<b>Error:</b>" + result), true);
                    }
                }
                catch (Exception ex)
                {
                    Danger(string.Format("<b>Error:</b>" + ex.Message), true);
                }
            }
            else
            {
                Danger(string.Format("<b>Error:102 :</b>" + string.Join("; ", ModelState.Values.SelectMany(z => z.Errors).Select(z => z.ErrorMessage))), true);
            }
            return View(_objEdit);
        }


        [HttpPost]
        [SubmitButtonSelector(Name = "Save")]
        [ActionName("MixDesign_Edit")]
        public ActionResult MixDesign_Strength_Save(MIX_DESIGN_VIEW _objEdit)
        {

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                try
                {
                    ResultMessage objMst;

                    string result = new DAL_MIX_DESIGN().INSERT_MIX_DESIGN_STRENGTH(emp.Employee_Code, _objEdit, out objMst);

                    if (result == "")
                    {

                        Success(string.Format("<b>Mix Design Strength  successfully Inserted. <b style='color:red'> " + "</b>"), true);
                        return RedirectToAction("MixDesign_Edit", "MixDesign", new { TM_ID = objMst.ID });
                    }
                    else
                    {
                        Danger(string.Format("<b>Error:</b>" + result), true);
                    }
                }
                catch (Exception ex)
                {
                    Danger(string.Format("<b>Error:</b>" + ex.Message), true);
                }
            }
            else
            {
                Danger(string.Format("<b>Error:102 :</b>" + string.Join("; ", ModelState.Values.SelectMany(z => z.Errors).Select(z => z.ErrorMessage))), true);
            }

            return View(_objEdit);
        }

        [HttpPost]
        [SubmitButtonSelector(Name = "Lock")]
        [ActionName("MixDesign_Edit")]
        public ActionResult MixDesign_Strength_Lock(MIX_DESIGN_VIEW _objEdit)
        {
            ModelState["UploadFile"] = new ModelState();
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                try
                {
                    ResultMessage objMst;
                    //decimal TM_ID = _objEdit.TM_ID;
                    string result = new DAL_MIX_DESIGN().UPDATE_MIX_DESIGN_STRENGTH_LOCK(emp.Employee_Code, _objEdit.TM_ID, out objMst);

                    if (result == "")
                    {

                        Success(string.Format(" <b style='color:red'> " +  "</b> Mix Design Strength is locked successfully."), true);
                        return RedirectToAction("MixDesign_Edit", "MixDesign", new { TM_ID = _objEdit.TM_ID });
                    }
                    else
                    {
                        Danger(string.Format("<b>Error:</b>" + result), true);
                    }
                }
                catch (Exception ex)
                {
                    Danger(string.Format("<b>Error:</b>" + ex.Message), true);
                }
            }
            else
            {
                Danger(string.Format("<b>Error:102 :</b>" + string.Join("; ", ModelState.Values.SelectMany(z => z.Errors).Select(z => z.ErrorMessage))), true);
            }
            return View(_objEdit);
        }

        [HttpPost]
        public ActionResult MixDesign_Strength_Lock(decimal TM_ID)
        {

            ResultMessage objMst;
            string result = new DAL_MIX_DESIGN().UPDATE_MIX_DESIGN_STRENGTH_LOCK(emp.Employee_Code, TM_ID, out objMst);

            if (result == "")
            {
                result = "1";
            }
            else
            {
                result = "0";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }    
    }
}
