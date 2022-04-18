using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AfluexFollowUpDemo.Filter;
using System.Web.Mvc;
using AfluexFollowUpDemo.Models;
using System.Data;
using System.IO;
using System.Net.Mail;
using static AfluexFollowUpDemo.Models.APIModel;

namespace AfluexFollowUpDemo.Controllers
{
    public class MasterController : BaseController
    {
        //public ActionResult CategoryMaster()
        //{
        //    return View();
        //}

        [HttpPost]
        [ActionName("CategoryMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult CategoryMaster(Master model)
        {
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                DataSet ds = model.InsertCategory();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Error"] = "Category is Successfully Added";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("CategoryMaster", "Master");
        }
        public ActionResult InterActionMaster(string PK_InterActionId)
        {
            Master model = new Master();
            if (PK_InterActionId != null)
            {
                Master obj = new Master();
                try
                {
                    obj.PK_InterActionId = PK_InterActionId;
                    DataSet ds = obj.ListInterAction();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.PK_InterActionId = ds.Tables[0].Rows[0]["PK_InterActionId"].ToString();
                        obj.InterActionName = ds.Tables[0].Rows[0]["InterActionName"].ToString();

                    }
                }
                catch (Exception ex)
                {
                    TempData["Master"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        [ActionName("InterActionMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult InterAction(Master model)
        {
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                DataSet ds = model.InsertInterAction();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Error"] = "InterAction is Successfully Added";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("InterActionMaster");
        }
        [HttpPost]
        [ActionName("InterActionMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateInterActionList(string PK_InterActionId, string InterActionName)
        {
            string FormName = "";
            string Controller = "";
            Master obj = new Master();
            try
            {
                obj.PK_InterActionId = PK_InterActionId;
                obj.InterActionName = InterActionName.Trim();
                obj.UpdatedBy = Session["UserID"].ToString();
                DataSet ds = obj.UpdateInterAction();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "InterActionList";
                        Controller = "Master";

                        TempData["Delete"] = "InterAction is Successfully Updated!";
                    }
                    else
                    {
                        //Session["PK_InterActionId"] = PK_InterActionId;
                        FormName = "InterActionMaster";
                        Controller = "Master";
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);

        }

        public ActionResult DataSourceMaster(string Pk_SourceId)
        {
            Master model = new Master();
            if (Pk_SourceId != null)
            {
                Master obj = new Master();
                try
                {
                    obj.Pk_SourceId = Pk_SourceId;
                    DataSet ds = obj.ListDataSource();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Pk_SourceId = ds.Tables[0].Rows[0]["Pk_SourceId"].ToString();
                        obj.SourceName = ds.Tables[0].Rows[0]["SourceName"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    TempData["Master"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        [ActionName("DataSourceMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult DataSourceMaster(Master model)
        {
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                DataSet ds = model.InsertDataSource();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Error"] = "Source Of Data is Successfully Added";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("DataSourceMaster");
        }


        public ActionResult ProspectActivityMaster(string Pk_ActivityId)
        {
            Master model = new Master();

            if (Pk_ActivityId != null)
            {
                Master obj = new Master();
                try
                {
                    obj.Pk_ActivityId = Pk_ActivityId;
                    DataSet ds = obj.ListActivity();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Pk_ActivityId = ds.Tables[0].Rows[0]["Pk_ActivityId"].ToString();
                        obj.ActivityName = ds.Tables[0].Rows[0]["ActivityName"].ToString();

                    }
                }
                catch (Exception ex)
                {
                    TempData["Master"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [ActionName("ProspectActivityMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult ProspectActivityMaster(Master model)
        {
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                DataSet ds = model.InsertProspectActivity();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Error"] = "Prospect Activity is Successfully Added";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ProspectActivityMaster");
        }

        public ActionResult ProspectActivityList(string Pk_ActivityId)
        {
            Master model = new Master();
            List<Master> lst1 = new List<Master>();

            DataSet ds = model.ListActivity();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_ActivityId = r["Pk_ActivityId"].ToString();
                    obj.ActivityName = r["ActivityName"].ToString();

                    lst1.Add(obj);
                }
                model.lstActivity = lst1;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("ProspectActivityMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateProspectActivityList(string Pk_ActivityId, string ActivityName)
        {
            string FormName = "";
            string Controller = "";
            Master obj = new Master();
            try
            {
                obj.Pk_ActivityId = Pk_ActivityId;
                obj.ActivityName = ActivityName.Trim();
                obj.UpdatedBy = Session["UserID"].ToString();
                DataSet ds = obj.UpdateActivity();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "ProspectActivityList";
                        Controller = "Master";

                        TempData["Delete"] = "Prospect Activity is Successfully Updated!";
                    }
                    else
                    {
                        //Session["Pk_ActivityId"] = Pk_ActivityId;
                        FormName = "ProspectActivityMaster";
                        Controller = "Master";
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);

        }

        public ActionResult BusinessChanceMaster(string Pk_BusinessChanceId)
        {
            Master model = new Master();

            if (Pk_BusinessChanceId != null)
            {
                Master obj = new Master();
                try
                {
                    obj.Pk_BusinessChanceId = Pk_BusinessChanceId;
                    DataSet ds = obj.ListBusinessChance();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Pk_BusinessChanceId = ds.Tables[0].Rows[0]["Pk_BusinessChanceId"].ToString();
                        obj.ChanceName = ds.Tables[0].Rows[0]["ChanceName"].ToString();

                    }
                }
                catch (Exception ex)
                {
                    TempData["Master"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [ActionName("BusinessChanceMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult BusinessChanceMaster(Master model)
        {
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                DataSet ds = model.InsertBusinessChance();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Error"] = "Business Chance is Successfully Added";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("BusinessChanceMaster");
        }

        public ActionResult BusinessChanceList(string Pk_BusinessChanceId)
        {
            Master model = new Master();
            //  model.Pk_BusinessChanceId = Pk_BusinessChanceId;
            List<Master> lst1 = new List<Master>();

            DataSet ds = model.ListBusinessChance();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_BusinessChanceId = r["Pk_BusinessChanceId"].ToString();
                    obj.ChanceName = r["ChanceName"].ToString();

                    lst1.Add(obj);
                }
                model.lstChance = lst1;
            }
            return View(model);


        }

        [HttpPost]
        [ActionName("BusinessChanceMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateBusinessChanceList(string Pk_BusinessChanceId, string ChanceName)
        {
            string FormName = "";
            string Controller = "";
            Master obj = new Master();
            try
            {
                obj.Pk_BusinessChanceId = Pk_BusinessChanceId;
                obj.ChanceName = ChanceName.Trim();
                obj.UpdatedBy = Session["UserID"].ToString();
                DataSet ds = obj.UpdateBusinessChance();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "BusinessChanceList";
                        Controller = "Master";

                        TempData["Delete"] = "Business Chance is Successfully Updated!";
                    }
                    else
                    {
                        //Session["Pk_BusinessChanceId"] = Pk_BusinessChanceId;
                        FormName = "BusinessChanceMaster";
                        Controller = "Master";
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);

        }



        public ActionResult CategoryMaster(string Pk_CategoryId)
        {
            Master model = new Master();

            if (Pk_CategoryId != null)
            {
                Master obj = new Master();
                try
                {
                    obj.Pk_CategoryId = Pk_CategoryId;
                    DataSet ds = obj.ListCategory();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Pk_CategoryId = ds.Tables[0].Rows[0]["Pk_CategoryId"].ToString();
                        obj.CategoryName = ds.Tables[0].Rows[0]["CategoryName"].ToString();

                    }
                }
                catch (Exception ex)
                {
                    TempData["Master"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult InterActionList(string PK_InterActionId)
        {
            Master model = new Master();
            List<Master> lst1 = new List<Master>();

            DataSet ds = model.ListInterAction();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_InterActionId = r["PK_InterActionId"].ToString();
                    obj.InterActionName = r["InterActionName"].ToString();

                    lst1.Add(obj);
                }
                model.lstInterAction = lst1;
            }
            return View(model);

        }


        public ActionResult CategoryList(string Pk_ProductCategoryId)
        {
            Master model = new Master();
            List<Master> lst1 = new List<Master>();
            DataSet ds = model.ListCategory();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_CategoryId = r["Pk_CategoryId"].ToString();
                    obj.CategoryName = r["CategoryName"].ToString();

                    lst1.Add(obj);
                }
                model.lstCategory = lst1;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("CategoryMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateCategoryList(string Pk_CategoryId, string CategoryName)
        {
            string FormName = "";
            string Controller = "";
            Master obj = new Master();
            try
            {
                obj.Pk_CategoryId = Pk_CategoryId;
                obj.CategoryName = CategoryName.Trim();
                obj.UpdatedBy = Session["UserID"].ToString();
                DataSet ds = obj.UpdateCategory();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "CategoryList";
                        Controller = "Master";

                        TempData["Delete"] = "Category is Successfully Updated!";
                    }
                    else
                    {
                        Session["Pk_CategoryId"] = Pk_CategoryId;
                        FormName = "CategoryMaster";
                        Controller = "Master";
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);

        }


        public ActionResult DataSourceList(string Pk_SourceId)
        {
            Master model = new Master();
            List<Master> lst1 = new List<Master>();

            DataSet ds = model.ListDataSource();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_SourceId = r["Pk_SourceId"].ToString();
                    obj.SourceName = r["SourceName"].ToString();

                    lst1.Add(obj);
                }
                model.lstSource = lst1;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("DataSourceMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateDataSourceList(string Pk_SourceId, string SourceName)
        {
            string FormName = "";
            string Controller = "";
            Master obj = new Master();
            try
            {
                obj.Pk_SourceId = Pk_SourceId;
                obj.SourceName = SourceName.Trim();
                obj.UpdatedBy = Session["UserID"].ToString();
                DataSet ds = obj.UpdateDataSource();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "DataSourceList";
                        Controller = "Master";

                        TempData["Delete"] = "Data Source is Successfully Updated!";
                    }
                    else
                    {
                        //Session["Pk_SourceId"] = Pk_SourceId;
                        FormName = "DataSourceMaster";
                        Controller = "Master";
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);

        }

        public ActionResult ProductCategoryMaster(string Pk_ProductCategoryId)
        {
            Master model = new Master();

            if (Pk_ProductCategoryId != null)
            {
                Master obj = new Master();
                try
                {
                    obj.Pk_ProductCategoryId = Pk_ProductCategoryId;
                    DataSet ds = obj.ListProductCategory();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Pk_ProductCategoryId = ds.Tables[0].Rows[0]["Pk_ProductCategoryId"].ToString();
                        obj.ProductCategoryName = ds.Tables[0].Rows[0]["ProductCategoryName"].ToString();

                    }
                }
                catch (Exception ex)
                {
                    TempData["Master"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        [ActionName("ProductCategoryMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult ProductCategoryMaster(Master model)
        {
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                DataSet ds = model.ExpectedProductCategoryInsert();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Error"] = "Product Category is Successfully Added";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ProductCategoryMaster");
        }

        public ActionResult ProductCategoryList(string Pk_ProductCategoryId)
        {
            Master model = new Master();
            List<Master> lst1 = new List<Master>();

            DataSet ds = model.ListProductCategory();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_ProductCategoryId = r["Pk_ProductCategoryId"].ToString();
                    obj.ProductCategoryName = r["ProductCategoryName"].ToString();

                    lst1.Add(obj);
                }
                model.lstProductCategory = lst1;
            }
            return View(model);

        }

        [HttpPost]
        [ActionName("ProductCategoryMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateProductCategoryList(string Pk_ProductCategoryId, string ProductCategoryName)
        {
            string FormName = "";
            string Controller = "";
            Master obj = new Master();
            try
            {
                obj.Pk_ProductCategoryId = Pk_ProductCategoryId;
                obj.ProductCategoryName = ProductCategoryName.Trim();
                obj.UpdatedBy = Session["UserID"].ToString();
                DataSet ds = obj.UpdateProductCategory();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "ProductCategoryList";
                        Controller = "Master";

                        TempData["Delete"] = "ProductCategory is Successfully Updated!";
                    }
                    else
                    {
                        //Session["Pk_ProductCategoryId"] = Pk_ProductCategoryId;
                        FormName = "ProductCategoryMaster";
                        Controller = "Master";
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);

        }

        #region DeleteCategoryMaster
        public ActionResult DeleteCategoryMaster(string Pk_CategoryId)
        {
            Master obj = new Master();
            try
            {
                obj.DeletedBy = Session["UserID"].ToString();
                obj.Pk_CategoryId = Pk_CategoryId;
                DataSet ds = new DataSet();
                ds = obj.DeleteCategoryMaster();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Success"] = "Category Deleted Successfully";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

            }
            //ViewBag.saverrormsg = "";
            return RedirectToAction("CategoryList", "Master");
        }
        #endregion

        #region DeleteInterractionMaster
        public ActionResult DeleteInterractionMaster(string PK_InterActionId)
        {
            Master obj = new Master();
            try
            {
                obj.DeletedBy = Session["UserID"].ToString();
                obj.PK_InterActionId = PK_InterActionId;
                DataSet ds = new DataSet();
                ds = obj.DeleteInterractionMaster();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Success"] = "InterAction Deleted Successfully";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

            }
            //ViewBag.saverrormsg = "";
            return RedirectToAction("InterActionList", "Master");
        }
        #endregion

        #region DeleteDataSourceMaster
        public ActionResult DeleteDataSourceMaster(string Pk_SourceId)
        {
            Master obj = new Master();
            try
            {
                obj.DeletedBy = Session["UserID"].ToString();
                obj.Pk_SourceId = Pk_SourceId;
                DataSet ds = new DataSet();
                ds = obj.DeleteDataSourceMaster();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Success"] = "DataSource Deleted Successfully";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

            }
            //ViewBag.saverrormsg = "";
            return RedirectToAction("DataSourceList", "Master");
        }
        #endregion

        #region DeleteProspectActivityMaster
        public ActionResult DeleteProspectActivityMaster(string Pk_ActivityId)
        {
            Master obj = new Master();
            try
            {
                obj.DeletedBy = Session["UserID"].ToString();
                obj.Pk_ActivityId = Pk_ActivityId;
                DataSet ds = new DataSet();
                ds = obj.DeleteProspectActivityMaster();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Success"] = "DataSource Deleted Successfully";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

            }
            //ViewBag.saverrormsg = "";
            return RedirectToAction("ProspectActivityList", "Master");
        }
        #endregion

        #region DeleteBusinessChance
        public ActionResult DeleteBusinessChance(string Pk_BusinessChanceId)
        {
            Master obj = new Master();
            try
            {
                obj.DeletedBy = Session["UserID"].ToString();
                obj.Pk_BusinessChanceId = Pk_BusinessChanceId;
                DataSet ds = new DataSet();
                ds = obj.DeleteBusinessChance();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Success"] = "Business Chance Deleted Successfully";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

            }
            //ViewBag.saverrormsg = "";
            return RedirectToAction("BusinessChanceList", "Master");
        }
        #endregion

        #region DeleteProductCategory
        public ActionResult DeleteProductCategory(string Pk_ProductCategoryId)
        {
            Master obj = new Master();
            try
            {
                obj.DeletedBy = Session["UserID"].ToString();
                obj.Pk_ProductCategoryId = Pk_ProductCategoryId;
                DataSet ds = new DataSet();
                ds = obj.DeleteProductCategory();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Success"] = "Product Category Deleted Successfully";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

            }
            //ViewBag.saverrormsg = "";
            return RedirectToAction("ProductCategoryList", "Master");
        }
        #endregion

        public ActionResult SendEmail()
        {
            Master model = new Master();
            try
            {
                model.SenderEmailDisplay = "developer5.afluex@gmail.com";
                //model.SenderEmail = "prakher.afluex@gmail.com";
                //model.SenderPassword = "Baby8542816119";

                List<Master> lst = new List<Master>();
                DataSet ds = model.GetEmailData();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Master obj = new Master();
                        obj.Name = dr["Name"].ToString();
                        obj.Email = dr["Email"].ToString();
                        obj.Description = dr["Description"].ToString();

                        lst.Add(obj);
                    }
                    model.lstVendor = lst;
                }

                int count4 = 0;
                List<SelectListItem> ddlTemplates = new List<SelectListItem>();
                DataSet dsTemplate = model.GetAllTemplates();
                if (dsTemplate != null && dsTemplate.Tables.Count > 0 && dsTemplate.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsTemplate.Tables[0].Rows)
                    {
                        if (count4 == 0)
                        {
                            ddlTemplates.Add(new SelectListItem { Text = "Select Template", Value = "0" });
                        }
                        ddlTemplates.Add(new SelectListItem { Text = r["TemplateSubject"].ToString(), Value = r["PK_TemplateID"].ToString() });
                        count4 = count4 + 1;
                    }
                }
                ViewBag.ddlTemplates = ddlTemplates;
            }
            catch (Exception ex)
            {

            }
            return View(model);

        }
        [HttpPost]
        [ActionName("SendEmail")]
        [OnAction(ButtonName = "btnSendEmail")]
        public ActionResult SendEmailAction(Master model)
        {

            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();
            try
            {

                int ctrCheck = 0;
                string ctrRowCount = Request["hdRows"].ToString();
                string recipientEmail = "";
                string chk = "";

                string signature = "<br/><br/><br/><br/><br/><table><tr><td><img src='https://www.bazarprofit.com/js/afluex-logo.png' /></td><td><b>A.K. Chauhan | 7310000414 / 413 / 412</b><br>Afluex Multiservices LLP<br>supportow@afluex.com<br>www.afluex.com<br>D-54, 2<sup>nd</sup>Floor, Vibhuti Khand, Near OLA Office, Lucknow, UP - 226010<br>We also provide IT Development Services</td></tr></table><br/><u>Terms & Conditions:</u>Read Before Reply</br><ul><li>All Proposed Sites are Subject to availability at the time of final confirmation.</li><li>If the site is booked for less than one month, extension for the same will depend upon availability of the site.</li><li>Tax will be charged extra as per gst applicable.</li><li>Send scanned P.O before start campaign along with signed & stamped on your letterhead.</li><li>Payment 50% Advance before campaign and rest 50% after 15 days or mid of the campaign.</li><li>Project/ Campaign will execute after advance Payment.</li><li>In case of any damage or theft of the flex/media the client shall bear the cost of the flex/media or provide the new flex/media.</li><li>Approval of the proposal or design along with the attachment otherwise the company will not accept the approval.</li><li>Please check everything before approval because once any project have approved other company will not responsible for any loss or mistake.</li><li>All disputes are Subject to Lucknow jurisdiction</li></ul>";

                for (int i = 0; i < int.Parse(ctrRowCount); i++)
                {
                    chk = Request["chkEmail_" + i];
                    if (chk == "on")
                    {
                        recipientEmail += Request["txtEmail_" + i].ToString() + ";";
                    }
                }
                SmtpClient mailServer = new SmtpClient("smtp.gmail.com", 587);
                mailServer.EnableSsl = true;
                // mailServer.Credentials = new System.Net.NetworkCredential("afluex.outdoor@gmail.com", "krishna@9919");
                mailServer.Credentials = new System.Net.NetworkCredential("developer5.afluex@gmail.com", "Afluex@123");
                MailMessage myMail = new MailMessage();
                myMail.Subject = model.Subject;
                myMail.Body = model.EmailBodyHTML + signature;
                myMail.From = new MailAddress("developer5.afluex@gmail.com", "Afluex Multiservices LLP");
                myMail.To.Add("developer2.afluex@gmail.com");

                myMail.IsBodyHtml = true;
                foreach (var emailid in recipientEmail.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    myMail.Bcc.Add(emailid);
                }
                HttpPostedFileBase file = Request.Files["postedfile"];
                if (file != null && file.ContentLength > 0)
                {
                    if (file.ContentLength < 12288000)
                    {
                        myMail.Body = "<html><body>" + model.EmailBodyHTML + signature + "</html></body>";
                        string filename = Path.GetFileName(file.FileName);
                        var attachment = new Attachment(file.InputStream, filename);
                        myMail.Attachments.Add(attachment);
                    }
                    else
                    {
                        string uploadFilename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        model.SelectedFilePath = "../EmailAttachments/" + uploadFilename;
                        file.SaveAs(Path.Combine(Server.MapPath(model.SelectedFilePath)));
                        myMail.Body = "<html><body>" + model.EmailBodyHTML;
                        myMail.Body += "<br/> Attachment Link : http://followup.afluex.com/EmailAttachments/" + uploadFilename;
                        myMail.Body += signature + "</html></body>";
                    }
                }

                mailServer.Send(myMail);
                ctrCheck++;
                TempData["Class"] = "alert alert-success";
                TempData["Success"] = "Email sent successfully";
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["ERROR"] = "ERROR : " + ex.Message;
            }
            return RedirectToAction("SendEmail");
        }



        public ActionResult AddEmailTemplate()
        {
            return View();
        }
        [HttpPost]
        [ActionName("AddEmailTemplate")]
        [OnAction(ButtonName = "btnSaveTemplate")]
        public ActionResult SaveEmailTemplate(Master model, HttpPostedFileBase postedfile)
        {
            try
            {
                if (postedfile != null)
                {
                    model.SelectedFilePath = "../SoftwareImages/" + Guid.NewGuid() + Path.GetExtension(postedfile.FileName);
                    postedfile.SaveAs(Path.Combine(Server.MapPath(model.SelectedFilePath)));
                }
                model.AddedBy = Session["UserID"].ToString();

                DataSet ds = model.SaveEmailTemplate();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["Success"] = "Template saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("AddEmailTemplate");
        }
        public ActionResult TemplateChange(string tid)
        {
            Master model = new Master();
            try
            {
                model.PK_TemplateID = tid;
                DataSet ds = model.GetAllTemplates();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.Result = "1";
                    model.SelectedFilePath = ds.Tables[0].Rows[0]["FilePath"].ToString();
                    model.Subject = ds.Tables[0].Rows[0]["TemplateSubject"].ToString();
                    model.Body = ds.Tables[0].Rows[0]["TemplateBody"].ToString();
                }
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteEmail(string id)
        {
            Master model = new Master();
            try
            {
                model.PK_EmailID = Crypto.Decrypt(id);
                model.AddedBy = Session["UserID"].ToString();

                DataSet ds = model.DeletEmail();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Success"] = "Email deleted successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("EmailMaster");
        }
        public ActionResult EmailMaster()
        {
            Master model = new Master();
            try
            {
                List<Master> lst = new List<Master>();
                DataSet ds = model.GetEmailData();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Master obj = new Master();
                        obj.EncryptKey = Crypto.Encrypt(dr["PK_EmailID"].ToString());
                        obj.PK_EmailID = dr["PK_EmailID"].ToString();
                        obj.Name = dr["Name"].ToString();
                        obj.Email = dr["Email"].ToString();
                        obj.Description = dr["Description"].ToString();

                        lst.Add(obj);
                    }
                    model.lstVendor = lst;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        [HttpPost]
        [ActionName("EmailMaster")]
        [OnAction(ButtonName = "btnSaveEmail")]
        public ActionResult SaveEmails(Master model)
        {
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                DataSet ds = model.SaveEmails();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Success"] = "Email data saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ERROR"] = "ERROR : " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("EmailMaster");
        }


        public ActionResult ChangeEmployeePassword()
        {
            ForgotPassword obj = new ForgotPassword();
            return View();
        }

        [HttpPost]
        [ActionName("ChangeEmployeePassword")]
        [OnAction(ButtonName = "btnChange")]
        public ActionResult UpdatePassword(ForgotPassword obj)
        {
            try
            {
                obj.LoginId = Session["LoginID"].ToString();
                obj.UserType = Session["FK_UserTypeID"].ToString();
                obj.UpdatedBy = Session["UserID"].ToString();
                DataSet ds = obj.UpdatePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Success"] = "Password updated Successfully";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ChangeEmployeePassword", "Master");
        }

    }
}