using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AfluexFollowUpDemo.Models
{
    public class EmployeeRegistration
    {
        public string AddedBy { get; set; }
        public string Pk_Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
        public string Address { get; set; }
        public string UserImage { get; set; }
        public string Pk_UserTypeID { get; set; }
        public string Fk_UserTypeId { get; set; }
        public string CreatedBy { get; set; }
        public string DeletedBy { get; set; }
        public List<EmployeeRegistration> lstemployee { get; set; }
        public string postedFile { get; set; }
        public string UpdatedBy { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public DataSet BindUserType()
        {

            DataSet ds = DBHelper.ExecuteQuery("GetUserTypeName");
            return ds;
        }

        public DataSet GetEmployeeList()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_Id", Pk_Id) };
                                    //new SqlParameter("@Name", User_Type_Name)
        DataSet ds = DBHelper.ExecuteQuery("GetEmployeeRegistration", para);
            return ds;
        }


    public DataSet UpdateEmployeeRegistration()
    {
        SqlParameter[] para = {
                                      new SqlParameter("@Pk_Id",Pk_Id),
                                      new SqlParameter("@Fk_UserTypeId",Fk_UserTypeId),
                                      new SqlParameter("@Name",Name),
                                      new SqlParameter("@ContactNo",ContactNo),
                                      new SqlParameter("@EmailId",EmailId),
                                      new SqlParameter("@Address",Address),
                                      new SqlParameter("@UserImage",UserImage),
                                      new SqlParameter("@UpdatedBy",UpdatedBy)
                                  };
        DataSet ds = DBHelper.ExecuteQuery("UpdateEmployeeRegistration", para);
        return ds;
    }
    public DataSet DeleteEmployee()
    {
        SqlParameter[] para = { new SqlParameter("@Pk_Id", Pk_Id), new SqlParameter("@DeletedBy", DeletedBy) };
        DataSet ds = DBHelper.ExecuteQuery("DeleteEmployeeRegistration", para);
        return ds;

    }

    public DataSet SaveEmployeeRegistration()
    {
        SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserTypeId",Fk_UserTypeId),
                                      new SqlParameter("@Name",Name),
                                      new SqlParameter("@ContactNo",ContactNo),
                                      new SqlParameter("@EmailId",EmailId),
                                      new SqlParameter("@Address",Address),
                                      new SqlParameter("@UserImage",UserImage),
                                      new SqlParameter("@CreatedBy",CreatedBy)
                                  };
        DataSet ds = DBHelper.ExecuteQuery("EmployeeRegistration", para);
        return ds;
    }

    public DataSet FilterEmployee()
    {
        SqlParameter[] para = {
                                      //new SqlParameter("@Pk_Id",Pk_Id),
                                      new SqlParameter("@Fk_UserTypeId",Fk_UserTypeId),
                                      new SqlParameter("@Name",Name),
                                      new SqlParameter("@ContactNo",ContactNo)
                                  };
        DataSet ds = DBHelper.ExecuteQuery("GetEmployeeRegistration", para);
        return ds;
    }
}
}