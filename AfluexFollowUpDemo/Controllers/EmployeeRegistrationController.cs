using AfluexFollowUpDemo.Filter;
using AfluexFollowUpDemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace AfluexFollowUpDemo.Controllers
{
    public class EmployeeRegistrationController : BaseController
    {
        // GET: EmployeeRegistration
        public ActionResult EmployeeRegistration(string Pk_Id)
        {
            EmployeeRegistration model = new EmployeeRegistration();
            #region BindUsertype
            int count = 0;
            List<SelectListItem> ddlUserName = new List<SelectListItem>();
            DataSet ds = model.BindUserType();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlUserName.Add(new SelectListItem { Text = "Select User", Value = "0" });
                    }
                    ddlUserName.Add(new SelectListItem { Text = r["UserName"].ToString(), Value = r["Pk_UserTypeID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlUserName = ddlUserName;
            #endregion
            if (Pk_Id != null)
            {
                EmployeeRegistration obj = new EmployeeRegistration();
                try
                {
                    obj.Pk_Id = Pk_Id;
                    DataSet ds1 = obj.GetEmployeeList();
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        obj.Pk_Id = ds1.Tables[0].Rows[0]["Pk_Id"].ToString();
                        obj.Fk_UserTypeId = ds1.Tables[0].Rows[0]["Fk_UserTypeId"].ToString();
                        obj.Name = ds1.Tables[0].Rows[0]["Name"].ToString();
                        obj.ContactNo = ds1.Tables[0].Rows[0]["ContactNo"].ToString();
                        obj.EmailId = ds1.Tables[0].Rows[0]["EmailId"].ToString();
                        obj.Address = ds1.Tables[0].Rows[0]["Address"].ToString();
                        obj.postedFile = ds1.Tables[0].Rows[0]["UserImage"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    TempData["ServiceError"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        [ActionName("EmployeeRegistration")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveEmployeeRegistration(EmployeeRegistration obj, HttpPostedFileBase postedFile)
        {
            if (TempData["Error"] == null)
            {
                ViewBag.errormsg = "none";

            }
            string FormName = "";
            string Controller = "";
            try
            {
                if (postedFile != null)
                {
                    obj.UserImage = "../SoftwareImages/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(obj.UserImage)));
                }

                string mailbody = "";
                obj.CreatedBy = Session["UserID"].ToString();
                DataSet ds = obj.SaveEmployeeRegistration();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        try
                        {
                            //string str2 = BLSMS.Registration(ds.Tables[0].Rows[0]["Name"].ToString(), ds.Tables[0].Rows[0]["LoginId"].ToString(), //ds.Tables[0].Rows[0]["Password"].ToString());
                          //  BLSMS.SendSMS(obj.ContactNo, str2);

                            try
                            {

                                mailbody = obj.EmailId + ";" + "Dear :"+(ds.Tables[0].Rows[0]["Name"].ToString())+ " Your LoginID :" +ds.Tables[0].Rows[0]["LoginId"].ToString()+" and Password :"+ ds.Tables[0].Rows[0]["Password"].ToString();
                                // var fromAddress = new MailAddress("contact.afluex@gmail.com");
                                var fromAddress = new MailAddress("developer5.afluex@gmail.com");
                                var toAddress = new MailAddress(obj.EmailId);

                                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                                {

                                    Host = "smtp.gmail.com",
                                    Port = 587,
                                    EnableSsl = true,
                                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                                    UseDefaultCredentials = false,
                                    //  Credentials = new NetworkCredential(fromAddress.Address, "krishna@9919")
                                    Credentials = new NetworkCredential(fromAddress.Address, "Afluex@123")

                                };
                                using (var message = new MailMessage(fromAddress, toAddress)
                                {
                                    IsBodyHtml = true,
                                    Subject = "Registration",
                                    Body = mailbody,
                                })
                                    smtp.Send(message);
                            }
                            catch (Exception ex)
                            {
                                TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            }
                            finally
                            {
                                
                            }
                        }
                        catch { }

                        TempData["Success"] = "Employee Registration Successfully!\nName : " + ds.Tables[0].Rows[0]["Name"].ToString() + ", Login ID : " + ds.Tables[0].Rows[0]["LoginId"].ToString() + ", Password : " + ds.Tables[0].Rows[0]["Password"].ToString();
                        FormName = "EmployeeRegistration";
                        Controller = "EmployeeRegistration";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "EmployeeRegistration";
                        Controller = "EmployeeRegistration";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);
        }

        //[HttpPost]
        //[ActionName("GetEmpolyeeRegistrationList")]
        //[OnAction(ButtonName = "GetDetails")]
        public ActionResult EmpolyeeRegistrationList()
        {
            EmployeeRegistration model = new EmployeeRegistration();
            List<EmployeeRegistration> lst = new List<EmployeeRegistration>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.Pk_Id = Session["UserID"].ToString();
            model.Fk_UserTypeId = model.Fk_UserTypeId == "0" ? null : model.Fk_UserTypeId;
            DataSet ds1 = model.FilterEmployee();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {

                    EmployeeRegistration obj = new EmployeeRegistration();
                    obj.Pk_Id = r["Pk_Id"].ToString();
                    obj.Fk_UserTypeId = r["UserName"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Password = r["Password"].ToString();
                    obj.ContactNo = r["ContactNo"].ToString();
                    obj.EmailId = r["EmailId"].ToString();
                    obj.Address = r["Address"].ToString();
                    obj.UserImage = string.IsNullOrEmpty(r["UserImage"].ToString()) ? " ../SoftwareImages/d2.jpg" : r["UserImage"].ToString();
                    lst.Add(obj);
                }
                model.lstemployee = lst;
            }

            #region BindUsertype
            int count = 0;
            List<SelectListItem> ddlUserName = new List<SelectListItem>();
            DataSet ds = model.BindUserType();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlUserName.Add(new SelectListItem { Text = "Select User", Value = "0" });
                    }
                    ddlUserName.Add(new SelectListItem { Text = r["UserName"].ToString(), Value = r["Pk_UserTypeID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlUserName = ddlUserName;

            #endregion BindUsertype
            return View(model);
        }
        public ActionResult DeleteEmployeeRegistration(string Pk_Id)
        {
            EmployeeRegistration obj = new EmployeeRegistration();
            try
            {
                obj.DeletedBy = Session["UserID"].ToString();
                obj.Pk_Id = Pk_Id;
                DataSet ds = new DataSet();
                ds = obj.DeleteEmployee();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Success"] = "Employee Deleted Successfully";
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
            ViewBag.saverrormsg = "";
            return RedirectToAction("EmpolyeeRegistrationList");
        }
        [HttpPost]
        [ActionName("EmployeeRegistration")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateEmployeeRegistration(EmployeeRegistration obj, string Pk_Id, HttpPostedFileBase postedFile)
        {
            try
            {
                if (postedFile != null)
                {
                    obj.UserImage = "../SoftwareImages/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(obj.UserImage)));
                }

                obj.UpdatedBy = Session["UserID"].ToString();
                obj.Pk_Id = obj.Pk_Id;
                DataSet ds = new DataSet();
                ds = obj.UpdateEmployeeRegistration();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        Session["dt"] = null;
                        TempData["Success"] = "Employee Registration Updated Successfully";
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
            return RedirectToAction("EmployeeRegistration");
        }

        [HttpPost]
        [ActionName("EmpolyeeRegistrationList")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult FilterEmployee(EmployeeRegistration model)
        {
            #region BindUsertype
            int count = 0;
            List<SelectListItem> ddlUserName = new List<SelectListItem>();
            DataSet ds1 = model.BindUserType();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlUserName.Add(new SelectListItem { Text = "Select User", Value = "0" });
                    }
                    ddlUserName.Add(new SelectListItem { Text = r["UserName"].ToString(), Value = r["Pk_UserTypeID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlUserName = ddlUserName;

            #endregion BindUsertype
            List<EmployeeRegistration> lst = new List<EmployeeRegistration>();
            try
            {
                model.Pk_Id = Session["UserID"].ToString();
                model.Fk_UserTypeId= model.Fk_UserTypeId == "0" ? null : model.Fk_UserTypeId;
                DataSet ds = model.FilterEmployee();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables.Count > 0)
                {

                    foreach (DataRow r in ds.Tables[0].Rows)
                    {

                        EmployeeRegistration obj = new EmployeeRegistration();
                        obj.Pk_Id = r["Pk_Id"].ToString();
                        obj.Fk_UserTypeId = r["UserName"].ToString();
                        obj.Name = r["Name"].ToString();
                        obj.Password = r["Password"].ToString();
                        obj.ContactNo = r["ContactNo"].ToString();
                        obj.EmailId = r["EmailId"].ToString();
                        obj.Address = r["Address"].ToString();
                        obj.UserImage = string.IsNullOrEmpty(r["UserImage"].ToString()) ? " ../SoftwareImages/d2.jpg" : r["UserImage"].ToString();
                        lst.Add(obj);
                    }
                    model.lstemployee = lst;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(model);

        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }


    }
}
