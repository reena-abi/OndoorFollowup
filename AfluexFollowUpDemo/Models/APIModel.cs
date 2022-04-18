using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AfluexFollowUpDemo.Models
{
    public class APIModel
    {
        public class LoginModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public DataSet Login()
            {
                SqlParameter[] para =
                    {
                new SqlParameter("@UserName",UserName),
                new SqlParameter("@Password",Password)
            };
                DataSet ds = DBHelper.ExecuteQuery("LoginProc", para);
                return ds;
            }

        }
        public class Crypto
        {
            public static string Encrypt(string clearText)
            {
                try
                {
                    string EncryptionKey = "ABCDEHJKLMNHBJKOAAAA";
                    byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                    using (Aes encryptor = Aes.Create())
                    {
                        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                        encryptor.Key = pdb.GetBytes(32);
                        encryptor.IV = pdb.GetBytes(16);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(clearBytes, 0, clearBytes.Length);
                                cs.Close();
                            }
                            clearText = Convert.ToBase64String(ms.ToArray());
                        }
                    }
                }
                catch { clearText = ""; }
                return clearText;
            }
            public static string Decrypt(string cipherText)
            {
                try
                {
                    string EncryptionKey = "ABCDEHJKLMNHBJKOAAAA";
                    cipherText = cipherText.Replace(" ", "+");
                    byte[] cipherBytes = Convert.FromBase64String(cipherText);
                    using (Aes encryptor = Aes.Create())
                    {
                        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                        encryptor.Key = pdb.GetBytes(32);
                        encryptor.IV = pdb.GetBytes(16);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(cipherBytes, 0, cipherBytes.Length);
                                cs.Close();
                            }
                            cipherText = Encoding.Unicode.GetString(ms.ToArray());
                        }
                    }
                }
                catch (Exception ex) { cipherText = ""; }
                return cipherText;

            }


        }
        public class ProductRequest
        {
            //public string Pk_CategoryId { get; set; }
            public DataSet ProductCategorylist()
            {
                SqlParameter[] para =
                    {
                      // new SqlParameter("@Pk_CategoryId",Pk_CategoryId)

            };
                DataSet ds = DBHelper.ExecuteQuery("SP_GetProductCategory", para);
                return ds;
            }
        }
        public class CategoryDetails
        {
            public string Pk_CategoryId { get; set; }
            public string CategoryName { get; set; }
        }
        public class SubCategoryDetails
        {
            public string Pk_ProductCategoryId { get; set; }
            public string ProductCategoryName { get; set; }
            public string Pk_CategoryId { get; set; }

        }

        public class SaveProcpect
        {
            public string ContactPerson { get; set; }
            public string ContactEmailId { get; set; }
            public string ContactNo { get; set; }
            public string Fk_IndustryCategoryId { get; set; } 
            public string CompanyName { get; set; }
            public string CompanyContactNo { get; set; }
            public string Address { get; set; }
            public string Pincode { get; set; }
            public string Designation { get; set; }
            public string WebSite { get; set; }
            public string SkypeId { get; set; }
            public string FacebookId { get; set; }
            public string LinkedInId { get; set; }
            public string ApproximateEmployee { get; set; }
            public string ApproximateCompanyTurnOver { get; set; }
           
            public string FirstInstructionDate { get; set; }
            public string Fk_ExpectedProductCategoryId { get; set; }
            public string Fk_SourceId { get; set; }
            public string Fk_ExecutiveId { get; set; }
            public string Fk_ModeInterActionId { get; set; }
            public string FollowupDate { get; set; }
            public string Description { get; set; }
            public string TwitterId { get; set; }
            public string anniversary { get; set; }
            public string dob { get; set; }
            public string AddedBy { get; set; }
          
            public DataSet insertProcpect()
            {
                SqlParameter[] para = {
                                      new SqlParameter("@ContactPerson",ContactPerson),
                                      new SqlParameter("@ContactEmailId",ContactEmailId),
                                      new SqlParameter("@ContactNo",ContactNo),
                                      new SqlParameter("@Fk_IndustryCategoryId",Fk_IndustryCategoryId),
                                      new SqlParameter("@CompanyName",CompanyName),
                                      new SqlParameter("@CompanyContactNo",CompanyContactNo),
                                      new SqlParameter("@Address",Address),
                                      new SqlParameter("@Pincode",Pincode),
                                      new SqlParameter("@Designation",Designation),
                                      new SqlParameter("@WebSite",WebSite),
                                      new SqlParameter("@SkypeId",SkypeId),
                                      new SqlParameter("@FacebookId",FacebookId),
                                      new SqlParameter("@TwitterId",TwitterId),
                                      new SqlParameter("@LinkedInId",LinkedInId),
                                      new SqlParameter("@ApproximateEmployee",ApproximateEmployee),
                                      new SqlParameter("@ApproximateCompanyTurnOver",ApproximateCompanyTurnOver),
                                      new SqlParameter("@FirstInstructionDate",FirstInstructionDate),
                                      new SqlParameter("@Fk_ExpectedProductCategoryId",Fk_ExpectedProductCategoryId),
                                      new SqlParameter("@Fk_SourceId",Fk_SourceId),
                                      new SqlParameter("@Fk_ExecutiveId",Fk_ExecutiveId),
                                      new SqlParameter("@Fk_ModeInterActionId",Fk_ModeInterActionId),
                                      new SqlParameter("@FollowupDate",FollowupDate),
                                      new SqlParameter("@Description",Description),
                                      new SqlParameter("@dob",dob),
                                      new SqlParameter("@anniversary",anniversary),
                                      new SqlParameter("@AddedBy",AddedBy)
                                  };
                DataSet ds = DBHelper.ExecuteQuery("InsertProcpectLeads", para);
                return ds;
            }

            public static string ConvertToSystemDate(string InputDate, string InputFormat)
            {
                string DateString = "";
                DateTime Dt;

                string[] DatePart = (InputDate).Split(new string[] { "-", @"/" }, StringSplitOptions.None);

                if (InputFormat == "dd-MMM-yyyy" || InputFormat == "dd/MMM/yyyy" || InputFormat == "dd/MM/yyyy" || InputFormat == "dd-MM-yyyy")
                {
                    string Day = DatePart[0];
                    string Month = DatePart[1];
                    string Year = DatePart[2];

                    if (Month.Length > 2)
                        DateString = InputDate;
                    else
                        DateString = Month + "/" + Day + "/" + Year;
                }
                else if (InputFormat == "MM/dd/yyyy" || InputFormat == "MM-dd-yyyy")
                {
                    DateString = InputDate;
                }
                else
                {
                    throw new Exception("Invalid Date");
                }

                try
                {
                    //Dt = DateTime.Parse(DateString);
                    //return Dt.ToString("MM/dd/yyyy");
                    return DateString;
                }
                catch
                {
                    throw new Exception("Invalid Date");
                }

            }



        }

        public class ProspectList
        {
            public string Pk_ProcpectId { get; set; }
            public string ContactPerson { get; set; }
            //public string ContactNo { get; set; }
            //public string CompanyName { get; set; }
            public string EmployeeId { get; set; }
            public DataSet ProspectDetails()
            {
                SqlParameter[] para = {
                                      new SqlParameter("@Pk_ProcpectId", Pk_ProcpectId),
                                      new SqlParameter("@ContactPerson", ContactPerson),
                                      //new SqlParameter("@ContactNo", ContactNo),
                                      //new SqlParameter("@CompanyName", CompanyName),
                                     new SqlParameter("@EmployeeId",EmployeeId)

                                  };
                DataSet ds = DBHelper.ExecuteQuery("GetProspectList", para);
                return ds;
            }
        }

        public class ProspectLst
        {
            public string Pk_ProcpectId { get; set; }
            public string ContactEmailId { get; set; }
            public string Fk_IndustryCategoryId { get; set; }
            public string CompanyContactNo { get; set; }
            public string Address { get; set; }
            public string ContactPerson { get; set; }
            public string CompanyName { get; set; }
            public string ContactNo { get; set; }
        }
        public class Followlistdate
        {
            public string CompletedMeeting { get; set; }
            public string ScheduleMeeting { get; set; }
            public string AssignMeeting { get; set; }
        }
        public class TodayVisit
        {
            public string EmployeeId { get; set; }
            public string name { get; set; }
            public DataSet GetDashBoardTodaylsit()
            {

                SqlParameter[] para ={
                                      new SqlParameter ("@FromDate",DateTime.Now.ToString("MM/dd/yyyy")),
                                      new SqlParameter ("@ToDate",DateTime.Now.ToString("MM/dd/yyyy")),
                                       new SqlParameter ("@name",name),
                                      new SqlParameter ("@EmployeeId",EmployeeId),
                                      };
                DataSet ds = DBHelper.ExecuteQuery("GetDashBoardDetails", para);//("SP_DashboardTodayList", para);
                return ds;
            }
        }
        public class Lead
        {
        //   public string mobile { get; set; }
            public string ProcpectID { get; set; }
            public string LeadID { get; set; }
            public string Fk_ProcpectName { get; set; }
            public string ContactNo { get; set; }
            public string FirstInstructionDate { get; set; }
            public string ProductCategoryName { get; set; }
            public string Fk_SourceName { get; set; }
            public string Fk_ExecutiveName { get; set; }
            public string Fk_ModeInterActionName { get; set; }
            public string FollowupDate { get; set; }
            public string Description { get; set; }
            public string schedulemeeting { get; set; }
          
        }


        public class CurrentLocation {

            public string EmployeeID { get; set; }
            public decimal  Latitude { get; set; }
            public decimal Longitude { get; set; }

            public DataSet addCurrentLocation()
            {
                SqlParameter[] para ={
                new SqlParameter("@FK_Loginid",EmployeeID),
                new SqlParameter("@lat",Latitude),
                new SqlParameter("@long",Longitude)
            };
                DataSet ds = DBHelper.ExecuteQuery("SP_tblCurrentlocation", para);
                return ds;
            }


        }


    
        public class usertype
        {
            public DataSet BindUserType()
            {

                DataSet ds = DBHelper.ExecuteQuery("GetUserTypeName");
                return ds;
            }
        }
        public class usertypelst
        {
            public string userName { get; set; }
            public string Pk_UsertypeID { get; set; }
        }

        public class EmpReg
        {
            public string Fk_UserTypeId { get; set; }
            public string Name { get; set; }
            public string ProfilePic { get; set; }
            public string ContactNo { get; set; }
            public string EmailId { get; set; }
            public string Address { get; set; }
            public string CreatedBy { get; set; }

            public DataSet SaveEmployeeRegistration()
            {
                SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserTypeId",Fk_UserTypeId),
                                      new SqlParameter("@Name",Name),
                                      new SqlParameter("@ContactNo",ContactNo),
                                      new SqlParameter("@EmailId",EmailId),
                                      new SqlParameter("@Address",Address),
                                      new SqlParameter("@userimage",ProfilePic),
                                      new SqlParameter("@CreatedBy",CreatedBy)
                                  };
                DataSet ds = DBHelper.ExecuteQuery("SP_EmployeeRegistration", para);
                return ds;
            }
        }

        public class Empreg_List
        {
            public string name { get; set;}

            public DataSet EmployeeRegistationList()
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@name",name),
                };
                DataSet ds = DBHelper.ExecuteQuery("SP_GetEmployeeRegstrationList", para);
                return ds;
            }
        }


        //public class uploadprofile
        //{
        //    public string ProfilePic { get; set; }
        //    public string loginid { get; set; }
        //    public DataSet UpdateProFileimage()
        //    {
        //        SqlParameter[] pare =
        //        { 
        //            new SqlParameter("@userimage",ProfilePic),
        //            new SqlParameter("@loginid",loginid),
        //        };
        //        DataSet ds = DBHelper.ExecuteQuery("SP_updateUserImage", pare);
        //        return ds;
        //    }
            
        //}

        public class emplist
        {
            public string loginid { get; set; }
            public string Password { get; set; }
            public string name { get; set; }
            public string contactno { get; set; }
            public string emailid { get; set; }
            public string Fk_UserTypeId { get; set; }
            public string username { get; set; }
            public string Time { get; set; }
            public string Date { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            
        }

        public class businesschancelst
        {
            public DataSet ListBusinessChance()
            {
                SqlParameter[] param = {
                    //new SqlParameter("@Pk_BusinessChanceId", Pk_BusinessChanceId) 
                };
                DataSet ds = DBHelper.ExecuteQuery("ListBusinessChance");
                return ds;
            }
        }
        public class LstBusinessChanse
        {
            public string Pk_BusinessChanceId { get; set; }
            public string BusinessChanceName { get; set; }
        }
        public class StateListlst
        {
            public string State { get; set; }
                public string City { get; set; }
        }
        public class StateList
        {
            public string Pincode { get; set; }
            public DataSet GetStateCity()
            {
                SqlParameter[] para ={new SqlParameter ("@PinCode",Pincode),
                               };
                DataSet ds = DBHelper.ExecuteQuery("GetStateCity", para);
                return ds;
            }
        }


        public class LeadList
        {
            public string Pk_ProcpectId { get; set; }
            public DataSet GetLead()
            {
                SqlParameter[] para ={new SqlParameter ("@Pk_ProcpectId",Pk_ProcpectId),
                               };
                DataSet ds = DBHelper.ExecuteQuery("SP_GetLead", para);
                return ds;
            }

        }

         public class LeadListlst
        {
            public string Lead_id { get; set; }
            public string Procpect_Id { get; set; }
            public string Pk_ProductCategoryId { get; set; }
            public string ProductCategoryName { get; set; }
            public string ContactPerson { get; set; }
        }


        public class DWRList
        {
           
            public string Pk_ProcpectId { get; set; }
            public string Fk_LeadId { get; set; }
            public DataSet GetDWRList()
            { SqlParameter [] para={ new SqlParameter("@Fk_LeadId",Fk_LeadId),
                                     new SqlParameter("@Pk_ProcpectId",Pk_ProcpectId) };
                DataSet ds = DBHelper.ExecuteQuery("SP_GetDWRList", para);//SP_GetDWRList
                return ds;
            }
        }
        public class dwrlst
        {
            public string PK_ListID { get; set; }
            public string LeadID { get; set; }
            public string Pk_ProcpectId { get; set; }
            public string ContactPerson { get; set; }
           public string MobileNo { get; set; }
            public string Remark { get; set; }
            public string BusinessChanceName { get; set; }
            public string BusinessChanceID { get; set; }
            public string InterActionName { get; set; }
            public string ExecutiveName { get; set; }
            public string MeetingDate { get; set; }
            public string MeetingTime { get; set; }
            public string LeadName { get; set; }
            public string FirstInstructionDate { get; set; }
            public string FollowupDate { get; set; }
         //   public string Description { get; set; }

        }

        public class allProspectList

       {
            public string LoginID { get; set; }
            public string ContactPerson { get; set; }
            public DataSet GetProspectList()
            {
                SqlParameter[] param = { new SqlParameter("@LoginID",LoginID ),
                                         new SqlParameter("@ContactPerson",ContactPerson )
                                   };
                DataSet ds = DBHelper.ExecuteQuery("SP_ProspectLIst", param);
                return ds;
            }
        }

        public class lstProspect
        {
            public string Pk_ProcpectId { get; set; }
            public string ProcpectName { get; set; }

        }


        public class savelead
       {
            public string  Procpect_Id { get; set; }
            public string ExpectedProductCategoryID { get; set; }
            public string ExecutiveNameID  { get; set; }
            public string SourceDataID { get; set; }
            public string FModeInterActionID { get; set; }
            public string FollowupDate { get; set; }
            public string Description { get; set; }
            public string FirstInstructionDate { get; set; }
            public string LoginID { get; set; }
            public DataSet InsertLead()
            {
                SqlParameter[] param = { new SqlParameter("@Fk_ProcpectId",Procpect_Id),
                                   new SqlParameter("@Fk_ExpectedProductCategoryId",ExpectedProductCategoryID),
                                   new SqlParameter("@Fk_SourceId",SourceDataID),
                                   new SqlParameter("@Fk_ExecutiveId",ExecutiveNameID),
                                   new SqlParameter("@Fk_ModeInterActionId",FModeInterActionID),
                                   new SqlParameter("@Description",Description),
                                   new SqlParameter("@FollowupDate",FollowupDate),
                                   new SqlParameter("@FirstInstructionDate",FirstInstructionDate),
                                   new SqlParameter("@AddedBy",LoginID)};
                DataSet ds = DBHelper.ExecuteQuery("InsertLead", param);
                return ds;
            }
        }

        public class leadLIst
        {
            public string loginid { get; set; }
            public string name { get; set; }
            public DataSet LeadList()
            {
                SqlParameter[] param = { new SqlParameter("@Pk_LeadeId", null),
                    new SqlParameter("@name",name),
                new SqlParameter("@AddedBy", loginid)

            };
                DataSet ds = DBHelper.ExecuteQuery("LeadList", param);
                return ds;
            }
        }
        public class LstLead
        {
            public string Pk_LeadeId { get; set; }
            public string ProcpectID { get; set; }
            public string Fk_ProcpectName { get; set; }
            public string Mobile { get; set; }
            public string EmailID { get; set; }
            public string FirstInstructionDate { get; set; }
            public string Fk_ExpectedProductCategoryName { get; set; }
            public string Fk_SourceName { get; set; }
            public string Fk_ExecutiveName { get; set; }
            public string Fk_ModeInterActionName { get; set; }
            public string FollowupDate { get; set; }
            public string Description { get; set; }

        }

        public class SAVEDWR
        {
            public string ProcpectId { get; set; }
            public string LeadId { get; set; }
            public string NextFollowupDate { get; set; }
            public string Remark { get; set; }
            public string BusinessChanceId { get; set; }
            public string ModeInterActionId { get; set; }
          //  public string SubmittedByName { get; set; }
            public string MeetingDate { get; set; }
            public string MeetingTime { get; set; }
            public string LoginID { get; set; }
            public DataSet SAVE_DWR()
            {
                SqlParameter[] param = { new SqlParameter("@Fk_ProcpectId",ProcpectId),
                                    new SqlParameter("@Fk_LeadId", LeadId),
                                    new SqlParameter("@NextFollowupDate", NextFollowupDate),
                                    new SqlParameter("@Remark", Remark),
                                    new SqlParameter("@Fk_BusinessChanceId", BusinessChanceId),
                                    new SqlParameter("@Fk_ModeInterActionId", ModeInterActionId),
                                    new SqlParameter("@Fk_ExecutiveId", LoginID),
                                    new SqlParameter("@MeetingDate", MeetingDate),
                                    new SqlParameter("@MeetingTime", MeetingTime),
                                   new SqlParameter("@AddedBy",LoginID)};
                DataSet ds = DBHelper.ExecuteQuery("InsertDWR", param);
                return ds;

            }
        }
         public class SourceList
        {
            public DataSet ListDataSource()
            {
                DataSet ds = DBHelper.ExecuteQuery("SP_ListDataSource");
                return ds;
            }
               
        }
        
        public class ListSourceData
        {
            public string SourceId { get; set; }
            public string SourceName { get; set; }
        }
      
        public class AllEMpProspectLead
        {
            public string LoginID { get; set; }
          
            public DataSet GetProspectList()
            {
                SqlParameter[] param = { new SqlParameter("@Pk_ProcpectId",null ),
                                         new SqlParameter("@ContactPerson",null ),
                                         new SqlParameter("@EmployeeId",LoginID ),
                                   };
                DataSet ds = DBHelper.ExecuteQuery("GetProspectList", param);
                return ds;
            }
        }
        public class lstLeadProspect
        {
            public string Pk_ProcpectId { get; set; }
            public string ProcpectName { get; set; }

        }
        public class AllEMpProspectLeadDetails
        {
            public string LoginID { get; set; }
            public string ProcpectId { get; set; }
            public DataSet GetProspectList()
            {
                SqlParameter[] param = { new SqlParameter("@Pk_ProcpectId",ProcpectId ),
                                         new SqlParameter("@ContactPerson",null ),
                                         new SqlParameter("@EmployeeId",LoginID ),
                                   };
                DataSet ds = DBHelper.ExecuteQuery("GetProspectList", param);
                return ds;
            }
        }
        public class ProspectDetailesLst
        {
            public string ProcpectId { get; set; }
            public string ProcpectName { get; set; }
            public string ContactNo { get; set; }
         
            public string CompanyName { get; set; }
            public string CompanyEmailId { get; set; }
            public string CompanyAddress { get; set; }
            public string CompanyContactNo { get; set; }
        }

        public class AllLeadListDetails
        {
            public string LoginID { get; set; }
            public string name { get; set; }
            public DataSet GetLeadList()
            {
                SqlParameter[] param = {
                                         new SqlParameter("@Pk_LeadeId",null ),
                                         new SqlParameter("@name",name),
                                         new SqlParameter("@AddedBy",LoginID ),
                                   };
                DataSet ds = DBHelper.ExecuteQuery("LeadList", param);
                return ds;
            }

        }
        public class LeadListLst
        {
            public string ProcpectId          { get; set; }
            public string LeadId              { get; set; }
            public string ContactPerson       { get; set; }
            public string ContactEmailId       { get; set; }
            public string ContactNo            { get; set; }
            public string ProductCategoryName { get; set; }
            public string ProductCategoryId   { get; set; }
            public string SourceName          { get; set; }
            public string InterActionName     { get; set; }
            public string FollowupDate         { get; set; }
            public string FirstInstructionDate { get; set; }
            public string Description          { get; set; }
        }
        public class ListToday
        {
            public string ContactPerson { get; set; }
            public string ProcpectID { get; set; }
            public string LeadID { get; set; }
            public string Mobile { get; set; }
            public string FirstInstructionDate { get; set; }
            public string ProductCategoryName { get; set; }
            public string FollowupDate { get; set; }
            public string DataSource { get; set; }
            public string ExecutiveName { get; set; }
            public string ModeOfInteraction { get; set; }
            public string Description { get; set; }
        }
        public class ListNextFollowup
        {
            public string ProcpectID { get; set; }
            public string LeadID { get; set; }
            public string ContactPerson { get; set; }
            public string Mobile { get; set; }
            public string FirstInstructionDate { get; set; }
            public string ProductCategoryName { get; set; }
            public string FollowupDate { get; set; }
            public string DataSource { get; set; }
            public string ExecutiveName { get; set; }
            public string ModeOfInteraction { get; set; }
            public string Description { get; set; }
        }
        public class ListPending
        {
            public string ProcpectID { get; set; }
            public string LeadID { get; set; }
            public string Mobile { get; set; }
            public string ContactPerson { get; set; }
            public string FirstInstructionDate { get; set; }
            public string ProductCategoryName { get; set; }
            public string FollowupDate { get; set; }
            public string DataSource { get; set; }
            public string ExecutiveName { get; set; }
            public string ModeOfInteraction { get; set; }
            public string Description { get; set; }
        }
        public class AllFollowupReport
        {
            public string EmployeeId { get; set; }
            public DataSet GetDashBoardDetails()
            {
              
                SqlParameter[] para ={
                                      new SqlParameter ("@FromDate",DateTime.Now.ToString("MM/dd/yyyy")),
                                     new SqlParameter ("@ToDate", DateTime.Now.ToString("MM/dd/yyyy")),
                                     new SqlParameter ("@EmployeeId",EmployeeId),


                                 };
                DataSet ds = DBHelper.ExecuteQuery("GetDashBoardDetails", para);
                return ds;
            }
        }
        public class AllFollowupList
        {
            public List<ListToday> lstToday { get; set; }
            public List<ListNextFollowup> lstNextFollowup { get; set; }
            public List<ListPending> lstPending { get; set; }
        }

        public class UpdateProcpect
        {
            public string ProcpectId { get; set; }
            public string ContactEmailId { get; set; }
            public string ContactNo { get; set; }
            public string Fk_IndustryCategoryId { get; set; }
            public string CompanyName { get; set; }
            public string CompanyContactNo { get; set; }
            public string Address { get; set; }
            public string Pincode { get; set; }
            public string Designation { get; set; }
            public string WebSite { get; set; }
            public string SkypeId { get; set; }
            public string FacebookId { get; set; }
            public string LinkedInId { get; set; }
            public string ApproximateEmployee { get; set; }
            public string ApproximateCompanyTurnOver { get; set; }

          //  public string FirstInstructionDate { get; set; }
            public string Fk_ExpectedProductCategoryId { get; set; }
            public string Fk_SourceId { get; set; }
           // public string Fk_ExecutiveId { get; set; }
            public string Fk_ModeInterActionId { get; set; }
          //  public string FollowupDate { get; set; }
           // public string Description { get; set; }
            public string TwitterId { get; set; }
            public string anniversary { get; set; }
            public string dob { get; set; }
            public string AddedBy { get; set; }

            public DataSet UpdateProcpectData()
            {
                SqlParameter[] para = {
                                     new SqlParameter("@Pk_ProcpectId",ProcpectId),
                                      new SqlParameter("@ContactEmailId",ContactEmailId),
                                      new SqlParameter("@ContactNo",ContactNo),
                                      new SqlParameter("@Fk_IndustryCategoryId",Fk_IndustryCategoryId),
                                      new SqlParameter("@CompanyName",CompanyName),
                                      new SqlParameter("@CompanyContactNo",CompanyContactNo),
                                      new SqlParameter("@Address",Address),
                                      new SqlParameter("@Pincode",Pincode),
                                     new SqlParameter("@Designation",Designation),
                                      new SqlParameter("@WebSite",WebSite),
                                      new SqlParameter("@SkypeId",SkypeId),
                                      new SqlParameter("@FacebookId",FacebookId),
                                      new SqlParameter("@TwitterId",TwitterId),
                                      new SqlParameter("@LinkedInId",LinkedInId),
                                      new SqlParameter("@ApproximateEmployee",ApproximateEmployee),
                                      new SqlParameter("@ApproximateCompanyTurnOver",ApproximateCompanyTurnOver),
                                   //   new SqlParameter("@FirstInstructionDate",FirstInstructionDate),
                                      new SqlParameter("@Fk_ExpectedProductCategoryId",Fk_ExpectedProductCategoryId),
                                      new SqlParameter("@Fk_SourceId",Fk_SourceId),
                                      new SqlParameter("@Fk_ExecutiveId",AddedBy),
                                      new SqlParameter("@Fk_ModeInterActionId",Fk_ModeInterActionId),
                                   //   new SqlParameter("@FollowupDate",FollowupDate),
                                   //   new SqlParameter("@Description",Description),
                                      new SqlParameter("@dob",dob),
                                      new SqlParameter("@anniversary",anniversary),
                                      new SqlParameter("@AddedBy",AddedBy)
                                  };
                DataSet ds = DBHelper.ExecuteQuery("UpdateProcpectLeads", para);
                return ds;
            }





        }

        public class AllProcpectDetails
        {
            public string ProcpectId { get; set; }
            public string LoginID { get; set; }
            public DataSet GetallProcpect()
            {

                SqlParameter[] para ={
                                     
                                     new SqlParameter ("@Pk_ProcpectId", ProcpectId),
                                     new SqlParameter ("@AddedBy",LoginID),


                                 };
                DataSet ds = DBHelper.ExecuteQuery("getallProcpectDetails", para);
                return ds;
            }
        }
        public class AllProcpecData
        {
            public string ProcpectId           { get; set; }
            public string ContactPerson         { get; set; }
            public string ContactEmailId          { get; set; }
            public string ContactNo            { get; set; }
            public string Fk_IndustryCategoryId { get; set; }
            public string CategoryName          { get; set; }
            public string CompanyName          { get; set; }
            public string CompanyContactNo     { get; set; }
            public string Address              { get; set; }
            public string Pincode             { get; set; }
            public string Designation         { get; set; }
            public string WebSite                 { get; set; }
            public string SkypeId             { get; set; }
            public string FacebookId          { get; set; }
            public string TwitterId          { get; set; }
            public string LinkedInId           { get; set; }
            public string ApproximateEmployee { get; set; }
            public string ApproximateCompanyTurnOver { get; set; }
            public string dob                       { get; set; }
            public string anniversary { get; set; }
            public string Fk_ExpectedProductCategoryId { get; set; }
            public string ProductCategoryName { get; set; }
            public string Fk_ExecutiveId { get; set; }
            public string Fk_SourceId { get; set; }
            public string Fk_ModeInterActionId { get; set; }
            
               
        }
    }
}