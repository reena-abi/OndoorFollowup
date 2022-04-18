using AfluexFollowUpDemo.Filter;
using AfluexFollowUpDemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace AfluexFollowUpDemo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAction(User obj)
        {
            if (obj.LoginId == null)
            {
                ViewBag.errormsg = "";
                TempData["Login"] = "Please Enter LoginId";
                
                return RedirectToAction("Index");

            }
            if (obj.Password == null)
            {
                ViewBag.errormsg = "";
                TempData["Login"] = "Please Enter Password";
                return RedirectToAction("Index");
            }
            if (obj.LoginId.Trim() == "")
            {
                ViewBag.errormsg = "";
                TempData["Login"] = "Please Enter LoginId";
                return RedirectToAction("Index");

            }
            if (obj.Password.Trim() == "")
            {
                ViewBag.errormsg = "";
                TempData["Login"] = "Please Enter Password";
                return RedirectToAction("Index");

            }

            try
            {
                User Modal = new User();
                DataSet ds = obj.Login();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["Fk_UserTypeId"].ToString() == "1")
                    {
                        ViewBag.errormsg = "";
                        Session["UserID"] = ds.Tables[0].Rows[0]["Pk_Id"].ToString();
                        Session["LoginID"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["Username"] = ds.Tables[0].Rows[0]["Name"].ToString();
                        Session["FK_UserTypeID"] = ds.Tables[0].Rows[0]["Fk_UserTypeId"].ToString();

                        return RedirectToAction("DashBoard", "Home");

                    }

                    if (ds.Tables[0].Rows[0]["Fk_UserTypeId"].ToString() != "1")
                    {
                        ViewBag.errormsg = "";
                        Session["UserID"] = ds.Tables[0].Rows[0]["Pk_Id"].ToString();
                        Session["LoginID"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["Username"] = ds.Tables[0].Rows[0]["Name"].ToString();
                        Session["FK_UserTypeID"] = ds.Tables[0].Rows[0]["Fk_UserTypeId"].ToString();
                        return RedirectToAction("EmployeeDashBoard", "Employee");

                    }

                    else
                    {
                        ViewBag.errormsg = "";
                        TempData["Login"] = "Incorrect LoginId Or Password";
                        return RedirectToAction("Index");

                    }

                }
                else
                {
                    ViewBag.errormsg = "";
                    TempData["Login"] = "Incorrect LoginId Or Password";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.errormsg = "";
                TempData["Login"] = ex.Message;
                return RedirectToAction("Index");

            }



        }

        public ActionResult DashBoard()
        {

            Lead obj = new Lead();
            List<Lead> lst1 = new List<Lead>();
            List<Lead> lstnext = new List<Lead>();
            List<Lead> lstpending = new List<Lead>();
            obj.FromDate = DateTime.Now.ToString("MM/dd/yyyy");
            obj.ToDate = DateTime.Now.ToString("MM/dd/yyyy");
            DataSet ds = obj.GetDashBoardDetails();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                #region TodayFollowup
                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    Lead obj1 = new Lead();
                    obj1.Fk_ProcpectId = r["ContactPerson"].ToString();
                    obj1.ContactNo = r["ContactNo"].ToString();
                    obj1.FirstInstructionDate = r["FirstInstructionDate"].ToString();
                    obj1.Fk_ExpectedProductCategoryId = r["ProductCategoryName"].ToString();
                    obj1.Fk_SourceId = r["SourceName"].ToString();
                    obj1.Fk_ExecutiveId = r["Name"].ToString();
                    obj1.Fk_ModeInterActionId = r["InterActionName"].ToString();
                    obj1.FollowupDate = r["FollowupDate"].ToString();
                    obj1.Description = r["Description"].ToString();
                    obj1.PrevioussDate = r["Previoussdate"].ToString();
                    lst1.Add(obj1);
                }
                #endregion TodayFollowup
                obj.lstLead = lst1;


            }
            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                #region NextFollowup
                foreach (DataRow r in ds.Tables[1].Rows)
                {

                    Lead obj1 = new Lead();

                    obj1.Fk_ProcpectId = r["ContactPerson"].ToString();
                    obj1.ContactNo = r["ContactNo"].ToString();
                    obj1.FirstInstructionDate = r["FirstInstructionDate"].ToString();
                    obj1.Fk_ExpectedProductCategoryId = r["ProductCategoryName"].ToString();
                    obj1.Fk_SourceId = r["SourceName"].ToString();
                    obj1.Fk_ExecutiveId = r["Name"].ToString();
                    obj1.Fk_ModeInterActionId = r["InterActionName"].ToString();
                    obj1.FollowupDate = r["FollowupDate"].ToString();
                    obj1.Description = r["Description"].ToString();
                    obj1.PrevioussDate = r["Previoussdate"].ToString();
                    lstnext.Add(obj1);
                }
                #endregion NextFollowup
                obj.lstnextLead = lstnext;
            }

            if (ds != null && ds.Tables[2].Rows.Count > 0)
            {
                #region PendingFollowup
                foreach (DataRow r in ds.Tables[2].Rows)
                {

                    Lead obj1 = new Lead();
                    obj1.Fk_ProcpectId = r["ContactPerson"].ToString();
                    obj1.ContactNo = r["ContactNo"].ToString();
                    obj1.Fk_ProcpectId = r["ContactPerson"].ToString();
                    obj1.FirstInstructionDate = r["FirstInstructionDate"].ToString();
                    obj1.Fk_ExpectedProductCategoryId = r["ProductCategoryName"].ToString();
                    obj1.Fk_SourceId = r["SourceName"].ToString();
                    obj1.Fk_ExecutiveId = r["Name"].ToString();
                    obj1.Fk_ModeInterActionId = r["InterActionName"].ToString();
                    obj1.FollowupDate = r["FollowupDate"].ToString();
                    obj1.Description = r["Description"].ToString();
                    obj1.PrevioussDate = r["Previoussdate"].ToString();
                    lstpending.Add(obj1);
                }
                obj.lstpending = lstpending;
                #endregion PendingFollowup
            }
            return View(obj);
        }
        public ActionResult ForgetPassword()
        {

            return View();
        }
        [HttpPost]
        [ActionName("ForgetPassword")]
        [OnAction(ButtonName = "btnforget")]
        public ActionResult ChangePassword(ForgotPassword model)
        {
            if (model.LoginId == null)
            {
                ViewBag.errormsg = "";
                TempData["Error"] = "Please Enter LoginId";
                return RedirectToAction("ForgetPassword");

            }
            if (model.EmailId == null)
            {
                ViewBag.errormsg = "";
                TempData["Error"] = "Please Enter EmailId";
                return RedirectToAction("ForgetPassword");
            }

            DataSet ds = model.PasswordForget();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    try
                    {
                        if (model.EmailId != null)
                        {
                            string mailbody = "";

                            mailbody = "Dear Member,<br> your Passoword is : " + ds.Tables[0].Rows[0]["Password"].ToString();
                            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                            {
                                Host = "smtp.gmail.com",
                                Port = 587,
                                EnableSsl = true,
                                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                                UseDefaultCredentials = true,
                                Credentials = new NetworkCredential("developer5.afluex@gmail.com", "Afluex@123")
                            };
                            using (var message = new MailMessage("developer5.afluex@gmail.com", model.EmailId)
                            {
                                IsBodyHtml = true,
                                Subject = "Recover Password",
                                Body = mailbody
                            })
                                smtp.Send(message);
                               TempData["Login"] = "Your Password Has Been Send On your EmailId";

                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                    }
                else
                {
                    TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            
            return RedirectToAction("ForgetPassword", "Home");
        }
         

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

    }
}