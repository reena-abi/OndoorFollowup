using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfluexFollowUpDemo.Models
{
    public class Common
    {
    
       
    
    
      
      //  public string Prospect { get; set; }
       // public string Fk_ProcpectId { get; set; }
        //public List<Lead> ddlProspect { get; set; }
        //public List<Lead> ddlProductCategory { get; set; }
        //public List<Lead> ddlDataSource { get; set; }
        //public List<Lead> ddlName { get; set; }
        //public List<Lead> ddlInterAction { get; set; }
      ///  public List<Lead> lstLead { get; set; }
     
     

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
}