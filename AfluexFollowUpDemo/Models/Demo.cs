using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AfluexFollowUpDemo.Models
{
    public class Demo
    {
        public string usieid { get; set; }

        public DataSet Demologin()
        {
            SqlParameter[] para =
            {
             new SqlParameter("@SELECT_ACTION",usieid)
        };
            DataSet ds = DBHelper.ExecuteQuery("[SP_DELETE_DTLS_SUB_CATEGORY]", para);
                return ds;
        }
    }
}