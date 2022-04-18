using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using AfluexFollowUpDemo;
using System.Web.Http;
using System.Data;
using System.IO;
using System.Web;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using static AfluexFollowUpDemo.Models.APIModel;
using System.Web.Script.Serialization;


namespace AfluexFollowUpDemo.Controllers
{
    public class WebAPIController : ApiController
    {


        [Route("Login")]
        [HttpPost]
        public HttpResponseMessage Login(LoginModel model)
        {
            try
            {
                DataSet ds = model.Login();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK,
                            new
                            {
                                StatusCode = HttpStatusCode.OK,
                                Message = "Login Successful",
                                Data = new
                                {
                                    PK_UserId = ds.Tables[0].Rows[0]["Pk_Id"].ToString(),
                                    LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString(),
                                    Name = ds.Tables[0].Rows[0]["Name"].ToString(),
                                    Password = ds.Tables[0].Rows[0]["Password"].ToString(),
                                    EmailId = ds.Tables[0].Rows[0]["EmailId"].ToString(),
                                    Fk_UserTypeId = ds.Tables[0].Rows[0]["Fk_UserTypeId"].ToString(),
                                    UserName = ds.Tables[0].Rows[0]["UserName"].ToString(),
                                    Admin = ds.Tables[0].Rows[0]["Admin"].ToString(),

                                }
                            });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              //  Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
                              Message = "Invalid User ID  and Password",

                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             // Message = "Error: ",
                             Message = "Invalid User ID  and Password",
                         });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        // Message = "Error: " + ex.Message,
                        Message = "Invalid User ID  and Password",

                    });
            }

        }
        [Route("ProductCategoryList")]
        [HttpPost]
        public HttpResponseMessage ProductCategoryList()
        {
            ProductRequest objreports = new ProductRequest();
            List<CategoryDetails> lst = new List<CategoryDetails>();
            List<SubCategoryDetails> lst1 = new List<SubCategoryDetails>();
            DataSet ds = objreports.ProductCategorylist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    CategoryDetails obj = new CategoryDetails();
                    obj.Pk_CategoryId = r["Pk_CategoryId"].ToString();
                    obj.CategoryName = r["CategoryName"].ToString();
                    lst.Add(obj);
                }
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[1].Rows)
                {
                    SubCategoryDetails obj = new SubCategoryDetails();
                    obj.Pk_ProductCategoryId = r["Pk_ProductCategoryId"].ToString();
                    obj.ProductCategoryName = r["ProductCategoryName"].ToString();
                    obj.Pk_CategoryId = r["Fk_CategoryId"].ToString();
                    lst1.Add(obj);
                }
                return Request.CreateResponse(HttpStatusCode.OK,
              new
              {
                  StatusCode = HttpStatusCode.OK,
                  Message = "Record Found",
                  lstCategory = lst,
                  lstSubCategory = lst1
              });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
             new
             {
                 StatusCode = HttpStatusCode.InternalServerError,
                 Message = "Record Not Found",
                 lstSubCategory = "Record Not Found"
             });
            }
        }
        [Route("AddProcpect")]
        [HttpPost]
        public HttpResponseMessage AddProcpect(SaveProcpect obj)
        {
            obj.FirstInstructionDate = string.IsNullOrEmpty(obj.FirstInstructionDate) ? null : SaveProcpect.ConvertToSystemDate(obj.FirstInstructionDate, "dd/MM/yyyy");
            obj.FollowupDate = string.IsNullOrEmpty(obj.FollowupDate) ? null : SaveProcpect.ConvertToSystemDate(obj.FollowupDate, "dd/MM/yyyy");
            try
            {
                // obj.AddedBy = Pk_ProcpectId;
                DataSet ds = obj.insertProcpect();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Procpect saved successfully",
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString()
                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.InternalServerError,
                            Message = "Error occurred"
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [Route("GetProspecctList")]
        [HttpPost]
        public HttpResponseMessage GetProspecctList(ProspectList model)
        {
            List<ProspectLst> lst1 = new List<ProspectLst>();
            try
            {
                DataSet ds = model.ProspectDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        ProspectLst obj = new ProspectLst();
                        obj.Pk_ProcpectId = r["Pk_ProcpectId"].ToString();
                        obj.ContactPerson = r["ContactPerson"].ToString();
                        obj.ContactEmailId = r["ContactEmailId"].ToString();
                        obj.ContactNo = r["ContactNo"].ToString();
                        obj.Fk_IndustryCategoryId = r["Fk_IndustryCategoryId"].ToString();
                        obj.CompanyName = r["CompanyName"].ToString();
                        obj.CompanyContactNo = r["CompanyContactNo"].ToString();
                        obj.Address = r["Address"].ToString();
                        lst1.Add(obj);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.OK,
                      Message = "Record Found",
                      lstSubCategory = lst1
                  });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                 new
                 {
                     StatusCode = HttpStatusCode.InternalServerError,
                     Message = "Record Not Found",

                 });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                               new
                               {
                                   StatusCode = HttpStatusCode.InternalServerError,
                                   Message = "Record Not Found",

                               });
            }
        }

        [Route("GetDashBoardTodayFollowup")]
        [HttpPost]
        public HttpResponseMessage GetDashBoardTodayFollowup(TodayVisit model)
        {

            List<ListToday> lstToday = new List<ListToday>();
            List<ListNextFollowup> lstNextFollowup = new List<ListNextFollowup>();
            List<ListPending> lstPending = new List<ListPending>();
            AllFollowupList obj = new AllFollowupList();
            List<Lead> lst3 = new List<Lead>();
            Lead obj3 = new Lead();
            try
            {
                // model.FromDate = DateTime.Now.ToString("MM/dd/yyyy");
                // model.ToDate =   DateTime.Now.ToString("MM/dd/yyyy");
                DataSet ds = model.GetDashBoardTodaylsit();
                // if (ds != null && ds.Tables[1].Rows.Count > 0)
                {
                    Followlistdate obj1 = new Followlistdate();
                    obj1.CompletedMeeting = ds.Tables[3].Rows[0]["CompletedMeeting"].ToString();
                    obj1.ScheduleMeeting = ds.Tables[3].Rows[0]["ScheduleMeeting"].ToString();
                    obj1.AssignMeeting = ds.Tables[3].Rows[0]["AssignMeeting"].ToString();
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        ListToday ob = new ListToday();
                        ob.ProcpectID = r["Fk_ProcpectId"].ToString();
                        ob.LeadID = r["Fk_LeadId"].ToString();
                        ob.ContactPerson = r["ContactPerson"].ToString();
                        ob.Mobile = r["ContactNo"].ToString();
                        ob.FirstInstructionDate = r["FirstInstructionDate"].ToString();
                        ob.ProductCategoryName = r["ProductCategoryName"].ToString();
                        ob.DataSource = r["SourceName"].ToString();
                        ob.ExecutiveName = r["Name"].ToString();
                        ob.ModeOfInteraction = r["InterActionName"].ToString();
                        ob.FollowupDate = r["FollowupDate"].ToString();
                        ob.Description = r["Description"].ToString();
                        lstToday.Add(ob);
                    }
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[1].Rows)
                    {
                        ListNextFollowup ob = new ListNextFollowup();
                        ob.ProcpectID = r["Fk_ProcpectId"].ToString();
                        ob.LeadID = r["Fk_LeadId"].ToString();
                        ob.Mobile = r["ContactNo"].ToString();
                        ob.ContactPerson = r["ContactPerson"].ToString();
                        ob.FirstInstructionDate = r["FirstInstructionDate"].ToString();
                        ob.ProductCategoryName = r["ProductCategoryName"].ToString();
                        ob.DataSource = r["SourceName"].ToString();
                        ob.ExecutiveName = r["Name"].ToString();
                        ob.ModeOfInteraction = r["InterActionName"].ToString();
                        ob.FollowupDate = r["FollowupDate"].ToString();
                        ob.Description = r["Description"].ToString();
                        lstNextFollowup.Add(ob);
                    }
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[2].Rows)
                    {

                        ListPending ob = new ListPending();
                        ob.ProcpectID = r["Fk_ProcpectId"].ToString();
                        ob.LeadID = r["Fk_LeadId"].ToString();
                        ob.Mobile = r["ContactNo"].ToString();
                        ob.ContactPerson = r["ContactPerson"].ToString();
                        ob.FirstInstructionDate = r["FirstInstructionDate"].ToString();
                        ob.ProductCategoryName = r["ProductCategoryName"].ToString();
                        ob.DataSource = r["SourceName"].ToString();
                        ob.ExecutiveName = r["Name"].ToString();
                        ob.ModeOfInteraction = r["InterActionName"].ToString();
                        ob.FollowupDate = r["FollowupDate"].ToString();
                        ob.Description = r["Description"].ToString();
                        lstPending.Add(ob);
                    }


                    return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.OK,
                      Message = "Record Found",
                      CompletedMeeting = ds.Tables[3].Rows[0]["CompletedMeeting"].ToString(),
                      ScheduleMeeting = ds.Tables[3].Rows[0]["ScheduleMeeting"].ToString(),
                      AssignMeeting = ds.Tables[3].Rows[0]["AssignMeeting"].ToString(),
                      TodayList = lstToday,
                      NextFollowUp = lstNextFollowup,
                      PendingList = lstPending


                  });
                }


                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                 new
                 {
                     StatusCode = HttpStatusCode.OK,
                     Message = "No schedule meeting for today",
                     CompletedMeeting = ds.Tables[1].Rows[0]["CompletedMeeting"].ToString(),
                     ScheduleMeeting = ds.Tables[1].Rows[0]["ScheduleMeeting"].ToString(),
                     AssignMeeting = ds.Tables[1].Rows[0]["AssignMeeting"].ToString(),
                     TodayList = lstToday,
                     NextFollowUp = lstNextFollowup,
                     PendingList = lstPending
                 });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.InternalServerError,
                      Message = "Record Not Found",
                  });
            }
        }

        [Route("AddCuurentLogtion")]
        [HttpPost]
        public HttpResponseMessage AddCuurentLogtion(CurrentLocation obj)
        {

            try
            {
                // obj.AddedBy = Pk_ProcpectId;
                DataSet ds = obj.addCurrentLocation();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Add Location saved successfully",
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = "Employee already exists"
                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.InternalServerError,
                            Message = "Error occurred"
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [Route("GetUsertype")]
        [HttpPost]
        public HttpResponseMessage GetUsertype()
        {
            usertype obj = new usertype();
            List<usertypelst> lst = new List<usertypelst>();
            try
            {
                DataSet dt = obj.BindUserType();

                if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Tables[0].Rows)
                    {
                        usertypelst obj1 = new usertypelst();
                        obj1.userName = r["userName"].ToString();
                        obj1.Pk_UsertypeID = r["Pk_UsertypeID"].ToString();
                        lst.Add(obj1);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
             new
             {
                 StatusCode = HttpStatusCode.OK,
                 Message = "Record Found",
                 usertypelst = lst,

             });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "No Record Found",
                       usertypelst = lst,

                   });

                }

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.InternalServerError,
                      Message = "Record Not Found",
                  });
            }
        }

        [Route("EmployeeRegistration")]
        [HttpPost]
        public HttpResponseMessage EmployeeRegistration()
        {
            EmpReg model = new EmpReg();
            try
            {
                Random rn = new Random();
                string Results = string.Empty;
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                model.Fk_UserTypeId = HttpContext.Current.Request.Params["Fk_UserTypeId"];
                model.Name = HttpContext.Current.Request.Params["Name"];
                model.ContactNo = HttpContext.Current.Request.Params["ContactNo"];
                model.EmailId = HttpContext.Current.Request.Params["EmailId"];
                model.ContactNo = HttpContext.Current.Request.Params["ContactNo"];
                model.Address = HttpContext.Current.Request.Params["Address"];
                model.CreatedBy = HttpContext.Current.Request.Params["CreatedBy"];
                var file = HttpContext.Current.Request.Files[0];
                model.ProfilePic = rn.Next(10, 100) + "_photo_" + file.FileName;
                string folderPath = HttpContext.Current.Server.MapPath("~/images/ProfilePicture/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                file.SaveAs(folderPath + model.ProfilePic);
                model.ProfilePic = "/images/ProfilePicture/" + model.ProfilePic;
                DataSet ds = model.SaveEmployeeRegistration();
                if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                              new
                              {
                                  StatusCode = HttpStatusCode.OK,
                                  Message = "Employee registration has been successfully",
                              });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Employee already exists"
                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.InternalServerError,
                            Message = "Error occurred"
                        });
                }
            }

            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        //[Route("UpdateUserImage")]
        //[HttpPost]
        //public HttpResponseMessage UpdateUserImage()
        //{
        //    uploadprofile model = new uploadprofile();
        //    try
        //    {
        //        Random rn = new Random();
        //        string Results = string.Empty;
        //        if (!Request.Content.IsMimeMultipartContent())
        //        {
        //            throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //        }
        //        model.loginid = HttpContext.Current.Request.Params["loginid"];
        //        var file = HttpContext.Current.Request.Files[0];
        //        model.ProfilePic = rn.Next(10, 100) + "_photo_" + file.FileName;
        //        string folderPath = HttpContext.Current.Server.MapPath("~/images/ProfilePicture/");
        //        if (!Directory.Exists(folderPath))
        //        {
        //            Directory.CreateDirectory(folderPath);
        //        }
        //        file.SaveAs(folderPath + model.ProfilePic);
        //        model.ProfilePic = "/images/ProfilePicture/" + model.ProfilePic;
        //        DataSet ds = model.UpdateProFileimage();
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
        //            {
        //                return Request.CreateResponse(HttpStatusCode.OK,
        //               new
        //               {
        //                   StatusCode = HttpStatusCode.OK,
        //                   Message = "Profile Pic Uploaded Successfully",
        //               });
        //            }
        //            else
        //            {
        //                return Request.CreateResponse(HttpStatusCode.OK,
        //               new
        //               {
        //                   StatusCode = HttpStatusCode.OK,
        //                   Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
        //               });
        //            }
        //        }
        //        else
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK,
        //               new
        //               {
        //                   StatusCode = HttpStatusCode.InternalServerError,
        //                   Message = "Error Occurred",
        //               });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK,
        //               new
        //               {
        //                   StatusCode = HttpStatusCode.InternalServerError,
        //                   Message = ex.Message,
        //               });
        //    }
        //}
        [Route("GetEmployeeRegistrationList")]
        [HttpPost]
        public HttpResponseMessage GetEmployeeRegistrationList(Empreg_List obj)
        {

            List<emplist> lst = new List<emplist>();
            try
            {
                DataSet dt = obj.EmployeeRegistationList();

                if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Tables[0].Rows)
                    {
                        emplist obj1 = new emplist();
                        obj1.loginid = r["loginid"].ToString();
                        obj1.Password = r["password"].ToString();
                        obj1.name = r["name"].ToString();
                        obj1.contactno = r["contactno"].ToString();
                        obj1.emailid = r["emailid"].ToString();
                        obj1.Fk_UserTypeId = r["Fk_UserTypeId"].ToString();
                        obj1.username = r["username"].ToString();
                        obj1.Time = r["Time"].ToString();
                        obj1.Date = r["Date"].ToString();
                        obj1.Latitude = r["lat"].ToString();
                        obj1.Longitude = r["long"].ToString();
                        lst.Add(obj1);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
             new
             {
                 StatusCode = HttpStatusCode.OK,
                 Message = "Record Found",
                 EmpRegistationList = lst,

             });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "No Record Found",
                       EmpRegistationList = lst,

                   });

                }

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.InternalServerError,
                      Message = "Record Not Found",
                  });
            }
        }

        [Route("businesschance")]
        [HttpPost]
        public HttpResponseMessage businesschance()
        {
            List<LstBusinessChanse> lst = new List<LstBusinessChanse>();
            try
            {
                businesschancelst obj = new businesschancelst();
                DataSet ds = obj.ListBusinessChance();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        LstBusinessChanse list1 = new LstBusinessChanse();
                        list1.Pk_BusinessChanceId = r["Pk_BusinessChanceId"].ToString();
                        list1.BusinessChanceName = r["ChanceName"].ToString();
                        lst.Add(list1);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = (HttpStatusCode.OK),
                            Message = "Record Found",
                            businesschancelist = lst
                        });

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = (HttpStatusCode.InternalServerError),
                            Message = "Record Not Found",
                            // businesschancelist = lst
                        });

                }
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.OK,
                              new
                              {
                                  StatusCode = HttpStatusCode.InternalServerError,
                                  Message = "Record Not Found",

                              });


            }

        }


        [Route("StateList")]
        [HttpPost]
        public HttpResponseMessage StateList(StateList obj)
        {

            List<StateListlst> lst = new List<StateListlst>();

            DataSet ds = obj.GetStateCity();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    StateListlst obj1 = new StateListlst();
                    obj1.State = r["StateName"].ToString();
                    obj1.City = r["CityName"].ToString();
                   
                    lst.Add(obj1);
                }
                return Request.CreateResponse(HttpStatusCode.OK,
              new
              {
                  StatusCode = HttpStatusCode.OK,
                  Message = "Record Found",
                  StateListlst = lst,

              });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
             new
             {
                 StatusCode = HttpStatusCode.InternalServerError,
                 Message = "Record Not Found",
                 lstSubCategory = "Record Not Found"
             });
            }
        }


        [Route("LeadList")]
        [HttpPost]
        public HttpResponseMessage LeadList(LeadList obj)
        {

            List<LeadListlst> lst = new List<LeadListlst>();

            DataSet ds = obj.GetLead();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    LeadListlst obj1 = new LeadListlst();
                    obj1.Pk_ProductCategoryId = r["Pk_ProductCategoryId"].ToString();
                    obj1.Lead_id = r["Pk_LeadId"].ToString();
                    obj1.Procpect_Id = r["Fk_ProcpectId"].ToString();
                    obj1.ProductCategoryName = r["ProductCategoryName"].ToString();
                    obj1.ContactPerson = r["ContactPerson"].ToString();

                    lst.Add(obj1);
                }
                return Request.CreateResponse(HttpStatusCode.OK,
              new
              {
                  StatusCode = HttpStatusCode.OK,
                  Message = "Record Found",
                  LeadListlst = lst,

              });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
             new
             {
                 StatusCode = HttpStatusCode.OK,
                 Message = "Record Not Found",
                 LeadListlst = lst,

             });
            }
        }

        [Route("DWRList")]
        [HttpPost]
        public HttpResponseMessage DWRList(DWRList obj1)
        {

            List<dwrlst> lst = new List<dwrlst>();
            DataSet ds = obj1.GetDWRList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    dwrlst obj = new dwrlst();
                    obj.PK_ListID = r["Pk_DwrId"].ToString();
                    obj.LeadID = r["LeadId"].ToString();
                    obj.ContactPerson = r["ContactPerson"].ToString();
                    obj.MobileNo = r["ContactNo"].ToString();
                    obj.LeadName = r["ProductCategoryName"].ToString();
                    obj.Remark = r["Remark"].ToString();
                    obj.BusinessChanceName = r["ChanceName"].ToString();
                    obj.BusinessChanceID = r["Pk_BusinessChanceId"].ToString();
                    obj.InterActionName = r["InterActionName"].ToString();
                    obj.ExecutiveName = r["Name"].ToString();
                    obj.MeetingDate = r["MeetingDate"].ToString();
                    obj.MeetingTime = r["MeetingTime"].ToString();
                    obj.FollowupDate = r["FollowupDate"].ToString();
                    obj.FirstInstructionDate = r["FirstInstructionDate"].ToString();
                    obj.Pk_ProcpectId = r["Fk_ProcpectId"].ToString();


                    lst.Add(obj);
                }


                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.OK,
                      Message = "Record Found",
                      dwrlst = lst,

                  });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
             new
             {
                 StatusCode = HttpStatusCode.InternalServerError,
                 Message = "Record Not Found",
                 dwrlst = lst
             });
            }
        }

        [Route("AllProcpectIdList")]
        [HttpPost]
        public HttpResponseMessage AllProcpectIdList(allProspectList obj1)
        {

            List<lstProspect> lst = new List<lstProspect>();
            DataSet ds = obj1.GetProspectList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    lstProspect obj = new lstProspect();
                    obj.Pk_ProcpectId = r["Pk_ProcpectId"].ToString();
                    obj.ProcpectName = r["ContactPerson"].ToString();


                    lst.Add(obj);
                }


                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.OK,
                      Message = "Record Found",
                      lstProspect = lst,

                  });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
             new
             {
                 StatusCode = HttpStatusCode.InternalServerError,
                 Message = "Record Not Found",
                 lstProspect = lst
             });
            }
        }


        [Route("AddLead")]
        [HttpPost]
        public HttpResponseMessage AddLead(savelead obj)
        {

            try
            {

                DataSet ds = obj.InsertLead();
                if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                              new
                              {
                                  StatusCode = HttpStatusCode.OK,
                                  Message = "Lead Added has been successfully",
                              });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Lead already exists"
                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.InternalServerError,
                            Message = "Error occurred"
                        });
                }
            }

            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }

        [Route("AllProspectsList")]
        [HttpPost]
        public HttpResponseMessage AllProspectsList(leadLIst obj1)
        {

            List<LstLead> lst = new List<LstLead>();
            DataSet ds = obj1.LeadList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    LstLead obj = new LstLead();
                    obj.Pk_LeadeId = r["Pk_LeadId"].ToString();
                    obj.ProcpectID = r["Fk_ProcpectId"].ToString();
                    obj.Fk_ProcpectName = r["ContactPerson"].ToString();

                    obj.EmailID = r["ContactEmailId"].ToString();
                    obj.Mobile = r["ContactNo"].ToString();
                    obj.FirstInstructionDate = r["FirstInstructionDate"].ToString();
                    obj.Fk_ExpectedProductCategoryName = r["ProductCategoryName"].ToString();
                    obj.Fk_SourceName = r["SourceName"].ToString();
                    obj.Fk_ExecutiveName = r["Name"].ToString();
                    obj.Fk_ModeInterActionName = r["InterActionName"].ToString();
                    obj.FollowupDate = r["FollowupDate"].ToString();
                    obj.Description = r["Description"].ToString();


                    lst.Add(obj);
                }


                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.OK,
                      Message = "Record Found",
                      LstProspectsList = lst,

                  });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
             new
             {
                 StatusCode = HttpStatusCode.InternalServerError,
                 Message = "Record Not Found",
                 LstProspectsList = lst
             });
            }
        }


        [Route("AddDWR")]
        [HttpPost]
        public HttpResponseMessage AddDWR(SAVEDWR obj)
        {

            try
            {
                obj.MeetingDate = DateTime.Now.ToString("MM/dd/yyyy");
                obj.NextFollowupDate = DateTime.Now.ToString("MM/dd/yyyy");
                DataSet ds = obj.SAVE_DWR();
                if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                              new
                              {
                                  StatusCode = HttpStatusCode.OK,
                                  Message = "Daily Work Report is Successfully Added",
                              });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Daily Work Report already exists"
                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.InternalServerError,
                            Message = "Error occurred"
                        });
                }
            }

            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }


        [Route("GetSourceList")]
        [HttpPost]
        public HttpResponseMessage GetSourceList()
        {
            List<ListSourceData> lst1 = new List<ListSourceData>();
            SourceList model = new SourceList();
            try
            {
                DataSet ds = model.ListDataSource();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        ListSourceData obj = new ListSourceData();
                        obj.SourceId = r["Pk_SourceId"].ToString();
                        obj.SourceName = r["SourceName"].ToString();

                        lst1.Add(obj);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.OK,
                      Message = "Record Found",
                      lstSourceList = lst1
                  });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                 new
                 {
                     StatusCode = HttpStatusCode.InternalServerError,
                     lstSourceList = lst1

                 });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                 new
                 {
                     StatusCode = HttpStatusCode.InternalServerError,
                     Message = "Record Not Found",
                 });
            }
        }

        [Route("AllEMPLeadProspect")]
        [HttpPost]
        public HttpResponseMessage AllEMPLeadProspect(AllEMpProspectLead obj1)
        {

            List<lstLeadProspect> lst = new List<lstLeadProspect>();
            DataSet ds = obj1.GetProspectList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    lstLeadProspect obj = new lstLeadProspect();
                    obj.Pk_ProcpectId = r["Pk_ProcpectId"].ToString();
                    obj.ProcpectName = r["ContactPerson"].ToString();


                    lst.Add(obj);
                }


                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.OK,
                      Message = "Record Found",
                      lstLeadProspect = lst,

                  });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
             new
             {
                 StatusCode = HttpStatusCode.InternalServerError,
                 Message = "Record Not Found",
                 lstLeadProspect = lst
             });
            }
        }

        [Route("AllEMPLeadProspectDetailes")]
        [HttpPost]
        public HttpResponseMessage AllEMPLeadProspectDetailes(AllEMpProspectLeadDetails obj1)
        {

            List<ProspectDetailesLst> lst = new List<ProspectDetailesLst>();
            DataSet ds = obj1.GetProspectList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    ProspectDetailesLst obj = new ProspectDetailesLst();
                    obj.ProcpectId = r["Pk_ProcpectId"].ToString();
                    obj.ProcpectName = r["ContactPerson"].ToString();
                    obj.ContactNo = r["ContactNo"].ToString();

                    obj.CompanyName = r["CompanyName"].ToString();
                    obj.CompanyEmailId = r["ContactEmailId"].ToString();
                    obj.CompanyContactNo = r["CompanyContactNo"].ToString();
                    obj.CompanyAddress = r["Address"].ToString();

                    lst.Add(obj);
                }


                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.OK,
                      Message = "Record Found",
                      LstProspectDetailesLst = lst,

                  });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
             new
             {
                 StatusCode = HttpStatusCode.InternalServerError,
                 Message = "Record Not Found",
                 LstProspectDetailesLst = lst
             });
            }
        }


        [Route("GetEmployeeLeadList")]
        [HttpPost]
        public HttpResponseMessage GetEmployeeLeadList(AllLeadListDetails obj1)
        {

            List<LeadListLst> lst = new List<LeadListLst>();
            DataSet ds = obj1.GetLeadList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    LeadListLst obj = new LeadListLst();
                    obj.ProcpectId = r["Fk_ProcpectId"].ToString();
                    obj.LeadId = r["Pk_LeadId"].ToString();
                    obj.ContactPerson = r["ContactPerson"].ToString();
                    obj.ContactEmailId = r["ContactEmailId"].ToString();
                    obj.ContactNo = r["ContactNo"].ToString();
                    obj.ProductCategoryName = r["ProductCategoryName"].ToString();
                    obj.ProductCategoryId = r["Fk_ExpectedProductCategoryId"].ToString();
                    obj.SourceName = r["SourceName"].ToString();
                    obj.InterActionName = r["InterActionName"].ToString();
                    obj.FollowupDate = r["FollowupDate"].ToString();
                    obj.FirstInstructionDate = r["FirstInstructionDate"].ToString();
                    obj.Description = r["Description"].ToString();
                    lst.Add(obj);
                }


                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.OK,
                      Message = "Record Found",
                      LstLeadListLst = lst,

                  });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
             new
             {
                 StatusCode = HttpStatusCode.InternalServerError,
                 Message = "Record Not Found",
                 LstLeadListLst = lst
             });
            }
        }

        //[Route("FollowupReport")]
        //[HttpPost]
        //public HttpResponseMessage FollowupReport(AllFollowupReport obj1)
        //{

        //    List<ListToday> lstToday = new List<ListToday>();
        //    List<ListNextFollowup> lstNextFollowup = new List<ListNextFollowup>();
        //    List<ListPending> lstPending = new List<ListPending>();
        //    AllFollowupList obj = new AllFollowupList();
        //    DataSet ds = obj1.GetDashBoardDetails();
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count>0)
        //    {
        //        foreach (DataRow r in ds.Tables[0].Rows)
        //        {
        //            ListToday ob = new ListToday();
        //            ob.ContactPerson = r["ContactPerson"].ToString();
        //            ob.FirstInstructionDate = r["FirstInstructionDate"].ToString();
        //            ob.ProductCategoryName = r["ProductCategoryName"].ToString();
        //            ob.DataSource = r["SourceName"].ToString();
        //            ob.ExecutiveName = r["Name"].ToString();
        //            ob.ModeOfInteraction = r["InterActionName"].ToString();
        //            ob.FollowupDate = r["FollowupDate"].ToString();
        //            ob.Description = r["Description"].ToString();
        //            lstToday.Add(ob);
        //        }
        //    }
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
        //    {
        //        foreach (DataRow r in ds.Tables[1].Rows)
        //        {
        //            ListNextFollowup ob = new ListNextFollowup();
        //            ob.ContactPerson = r["ContactPerson"].ToString();
        //            ob.FirstInstructionDate = r["FirstInstructionDate"].ToString();
        //            ob.ProductCategoryName = r["ProductCategoryName"].ToString();
        //            ob.DataSource = r["SourceName"].ToString();
        //            ob.ExecutiveName = r["Name"].ToString();
        //            ob.ModeOfInteraction = r["InterActionName"].ToString();
        //            ob.FollowupDate = r["FollowupDate"].ToString();
        //            ob.Description = r["Description"].ToString();
        //            lstNextFollowup.Add(ob);
        //        }
        //    }
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
        //    {
        //        foreach (DataRow r in ds.Tables[2].Rows)
        //        {

        //            ListPending ob = new ListPending();
        //            ob.ContactPerson = r["ContactPerson"].ToString();
        //            ob.FirstInstructionDate = r["FirstInstructionDate"].ToString();
        //            ob.ProductCategoryName = r["ProductCategoryName"].ToString();
        //            ob.DataSource = r["SourceName"].ToString();
        //            ob.ExecutiveName = r["Name"].ToString();
        //            ob.ModeOfInteraction = r["InterActionName"].ToString();
        //            ob.FollowupDate = r["FollowupDate"].ToString();
        //            ob.Description = r["Description"].ToString();
        //            lstPending.Add(ob);
        //        }
        //        return Request.CreateResponse(HttpStatusCode.OK,
        //      new
        //      {
        //          StatusCode = HttpStatusCode.OK,
        //          Message = "Record Found",
        //          TodayList = lstToday,
        //          NextFollowUp = lstNextFollowup,
        //          PendingList = lstPending
        //      });
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError,
        //     new
        //     {
        //         StatusCode = HttpStatusCode.InternalServerError,
        //         Message = "Record Not Found",
        //         //lstSubCategory = "Record Not Found"
        //         TodayList = lstToday,
        //         NextFollowUp = lstNextFollowup,
        //         PendingList = lstPending

        //     });
        //    }

        //}  

        [Route("UpdateProcpect")]
        [HttpPost]
        public HttpResponseMessage UpdateProcpect(UpdateProcpect obj)
        {
            obj.dob = string.IsNullOrEmpty(obj.dob) ? null : SaveProcpect.ConvertToSystemDate(obj.dob, "dd/MM/yyyy");
            obj.anniversary = string.IsNullOrEmpty(obj.anniversary) ? null : SaveProcpect.ConvertToSystemDate(obj.anniversary, "dd/MM/yyyy");
            try
            {
                // obj.AddedBy = Pk_ProcpectId;
                DataSet ds = obj.UpdateProcpectData();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Procpect Update successfully",
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString()
                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.InternalServerError,
                            Message = "Error occurred"
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }

        [Route("GetAllProcpectDetails")]
        [HttpPost]
        public HttpResponseMessage GetAllProcpectDetails(AllProcpectDetails obj)
        {

            try
            {

                DataSet ds = obj.GetallProcpect();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count >0 )
                    {

                        return Request.CreateResponse(HttpStatusCode.OK,
                            new
                            {
                                StatusCode = HttpStatusCode.OK,
                                Message = "Login Successful",
                                Data = new
                                {
                                   ProcpectId           = ds.Tables[0].Rows[0]["Pk_ProcpectId"].ToString(),
                                   ContactPerson          = ds.Tables[0].Rows[0]["ContactPerson"].ToString(),
                                   ContactEmailId       = ds.Tables[0].Rows[0]["ContactEmailId"].ToString(),
                                   ContactNo              = ds.Tables[0].Rows[0]["ContactNo"].ToString(),
                                   Fk_IndustryCategoryId  = ds.Tables[0].Rows[0]["Fk_IndustryCategoryId"].ToString(),
                                   CategoryName           = ds.Tables[0].Rows[0]["CategoryName"].ToString(),
                                   CompanyName            =   ds.Tables[0].Rows[0]["CompanyName"].ToString(),
                                   CompanyContactNo        =      ds.Tables[0].Rows[0]["CompanyContactNo"].ToString(),
                                    Address                     = ds.Tables[0].Rows[0]["Address"].ToString(),
                                    Pincode              = ds.Tables[0].Rows[0]["Pincode"].ToString(),
                                    Designation               = ds.Tables[0].Rows[0]["Designation"].ToString(),
                                    WebSite                 = ds.Tables[0].Rows[0]["WebSite"].ToString(),
                                    SkypeId                = ds.Tables[0].Rows[0]["SkypeId"].ToString(),
                                    FacebookId              = ds.Tables[0].Rows[0]["FacebookId"].ToString(),
                                    TwitterId              = ds.Tables[0].Rows[0]["TwitterId"].ToString(),
                                    LinkedInId              = ds.Tables[0].Rows[0]["LinkedInId"].ToString(),
                                    ApproximateEmployee  = ds.Tables[0].Rows[0]["ApproximateEmployee"].ToString(),
                                    ApproximateCompanyTurnOver = ds.Tables[0].Rows[0]["ApproximateCompanyTurnOver"].ToString(),
                                    dob = ds.Tables[0].Rows[0]["dob"].ToString(),
                                    anniversary = ds.Tables[0].Rows[0]["anniversary"].ToString(),
                                    Fk_ExpectedProductCategoryId = ds.Tables[0].Rows[0]["Fk_ExpectedProductCategoryId"].ToString(),
                                    ProductCategoryName = ds.Tables[0].Rows[0]["ProductCategoryName"].ToString(),
                                    Fk_ExecutiveId = ds.Tables[0].Rows[0]["Fk_ExecutiveId"].ToString(),
                                    Fk_SourceId = ds.Tables[0].Rows[0]["Fk_SourceId"].ToString(),
                                    Fk_ModeInterActionId = ds.Tables[0].Rows[0]["Fk_ModeInterActionId"].ToString(),


                                }
                            });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                                  //  Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
                                  Message = "No record Found",

                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                                 // Message = "Error: ",
                                 Message = "No record Found",
                         });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                            // Message = "Error: " + ex.Message,
                            Message = "No record Found",

                    });
            }
        }
    }
}




