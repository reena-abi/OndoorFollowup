using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfluexFollowUpDemo.Models
{
    public class Master
    {
        public string SenderEmail { get; set; }
        public string SenderPassword { get; set; }
        public string PK_TemplateID { get; set; }
       
        public string Side { get; set; }
        public string SiteImage { get; set; }
        public string SiteOwner { get; set; }
        public string Comments { get; set; }
        public string CartRate { get; set; }
        public string Quantity { get; set; }
        public string Facing { get; set; }
        public string SiteID { get; set; }
        public string VendorID { get; set; }
        public string Rational { get; set; }
        public string Location { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string ActivityName { get; set; }
        public string Pk_ActivityId { get; set; }
        public string ProductCategoryName { get; set; }
        public string Pk_ProductCategoryId { get; set; }
        public string Area { get; set; }
        public string SiteName { get; set; }
        public string Pk_CategoryId { get; set; }
        public string ChanceName { get; set; }
        public string Pk_BusinessChanceId { get; set; }
        public string SourceName { get; set; }
        public string Pk_SourceId { get; set; }
        public string PK_InterActionId { get; set; }
        public List<Master> lstChance { get; set; }
        public List<Master> lstProductCategory { get; set; }
        public List<Master> lstActivity { get; set; }
        public List<Master> lstSource { get; set; }
        public List<Master> lstCategory { get; set; }
        public List<Master> lstSites { get; set; }
        public List<Master> lstInterAction { get; set; }
        public DataTable dtSiteMaster { get; set; }
        public string MediaTypeID { get; set; }
        public string MediaTypeName { get; set; }
        public string MediaVehicleName { get; set; }
        public string InterActionName { get; set; }
        public string MediaVehicleID { get; set; }
        public string ServiceId { get; set; }
        public string HSNCode { get; set; }
        public string ServiceName { get; set; }
        public string CGST { get; set; }
        public string CategoryName { get; set; }
        public string IGST { get; set; }
        public string SGST { get; set; }
        public string DateFormat { get; set; }
        public string AddedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public string Subject { get; set; }
        [AllowHtml]
        public string EmailBodyHTML { get; set; }
        public string SelectedFilePath { get; set; }
        public string Result { get; set; }
        public List<Master> lstTemplates { get; set; }
        public List<Master> lstVendor { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string PK_EmailID { get; set; }
        public string EncryptKey { get; set; }
        public string SenderEmailDisplay { get; set; }
        public DataSet SaveEmails()
        {
            SqlParameter[] para ={   new SqlParameter ("@Name", Name),
                                     new SqlParameter ("@Email", Email),
                                     new SqlParameter ("@Description", Description),
                                     new SqlParameter("@AddedBy", AddedBy), };

            DataSet ds = DBHelper.ExecuteQuery("SaveEmailData", para);
            return ds;
        }
        public DataSet GetEmailData()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetEmail");
            return ds;
        }

        public DataSet DeletEmail()
        {
            SqlParameter[] para ={   new SqlParameter ("@PK_EmailID", PK_EmailID),
                                     new SqlParameter ("@DeletedBy", AddedBy), };
            DataSet ds = DBHelper.ExecuteQuery("DeleteEmail", para);
            return ds;
        }
        public DataSet GetAllTemplates()
        {
            SqlParameter[] para = { new SqlParameter("@PK_TemplateID", PK_TemplateID) };
            DataSet ds = DBHelper.ExecuteQuery("GetAllTemplates", para);
            return ds;
        }
        public DataSet ListDataSource()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_SourceId", Pk_SourceId) };
            DataSet ds = DBHelper.ExecuteQuery("ListDataSource", param);
            return ds;
        }
        public DataSet InsertDataSource()
        {
            SqlParameter[] param = {new SqlParameter("@SourceName",SourceName),
                                   new SqlParameter("@AddedBy",AddedBy)};
            DataSet ds = DBHelper.ExecuteQuery("InsertDataSource", param);
            return ds;
        }
        public DataSet UpdateDataSource()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_SourceId", Pk_SourceId),
                                   new SqlParameter("@SourceName",SourceName),
                                   new SqlParameter("@UpdatedBy",UpdatedBy)};
            DataSet ds = DBHelper.ExecuteQuery("UpdateDataSource", param);
            return ds;
        }
        public DataSet ListInterAction()
        {
            SqlParameter[] param = { new SqlParameter("@PK_InterActionId", PK_InterActionId) };
            DataSet ds = DBHelper.ExecuteQuery("ListInterAction", param);
            return ds;
        }
        public DataSet UpdateInterAction()
        {
            SqlParameter[] param = { new SqlParameter("@PK_InterActionId", PK_InterActionId),
                                   new SqlParameter("@InterActionName",InterActionName),
                                   new SqlParameter("@UpdatedBy",UpdatedBy)};
            DataSet ds = DBHelper.ExecuteQuery("UpdateInterAction", param);
            return ds;
        }
        public DataSet InsertCategory()
        {
            SqlParameter[] param = {new SqlParameter("@CategoryName",CategoryName),
                                   new SqlParameter("@AddedBy",AddedBy)};
            DataSet ds = DBHelper.ExecuteQuery("InsertCategory", param);
            return ds;
        }
        public DataSet ListCategory()
        {
            SqlParameter[] param = { new SqlParameter("@PK_CategoryId", Pk_CategoryId) };
            DataSet ds = DBHelper.ExecuteQuery("ListCategory", param);
            return ds;
        }
        public DataSet ListProductCategory()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_ProductCategoryId", Pk_ProductCategoryId) };
            DataSet ds = DBHelper.ExecuteQuery("GetProductList", param);
            return ds;
        }
        public DataSet UpdateCategory()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_CategoryId", Pk_CategoryId),
                                   new SqlParameter("@CategoryName",CategoryName),
                                   new SqlParameter("@UpdatedBy",UpdatedBy)};
            DataSet ds = DBHelper.ExecuteQuery("UpdateCategory", param);
            return ds;
        }

        public DataSet InsertInterAction()
        {
            SqlParameter[] param = {new SqlParameter("@InterActionName",InterActionName),
                                   new SqlParameter("@AddedBy",AddedBy)};
            DataSet ds = DBHelper.ExecuteQuery("InsertInterAction", param);
            return ds;
        }
        public DataSet InsertProspectActivity()
        {
            SqlParameter[] param = {new SqlParameter("@ActivityName",ActivityName),
                                   new SqlParameter("@AddedBy",AddedBy)};
            DataSet ds = DBHelper.ExecuteQuery("InsertProspectActivity", param);
            return ds;
        }
        public DataSet ListActivity()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_ActivityId", Pk_ActivityId) };
            DataSet ds = DBHelper.ExecuteQuery("ListActivity", param);
            return ds;
        }
        public DataSet UpdateActivity()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_ActivityId", Pk_ActivityId),
                                   new SqlParameter("@ActivityName",ActivityName),
                                   new SqlParameter("@UpdatedBy",UpdatedBy)};
            DataSet ds = DBHelper.ExecuteQuery("UpdateProspectActivity", param);
            return ds;
        }

        public DataSet ListBusinessChance()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_BusinessChanceId", Pk_BusinessChanceId) };
            DataSet ds = DBHelper.ExecuteQuery("ListBusinessChance", param);
            return ds;
        }
        public DataSet InsertBusinessChance()
        {
            SqlParameter[] param = {new SqlParameter("@ChanceName",ChanceName),
                                   new SqlParameter("@AddedBy",AddedBy)};
            DataSet ds = DBHelper.ExecuteQuery("InsertBusinessChance", param);
            return ds;
        }

        public DataSet UpdateBusinessChance()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_BusinessChanceId", Pk_BusinessChanceId),
                                   new SqlParameter("@ChanceName",ChanceName),
                                   new SqlParameter("@UpdatedBy",UpdatedBy)};
            DataSet ds = DBHelper.ExecuteQuery("UpdateBusiness", param);
            return ds;
        }
        public DataSet ExpectedProductCategoryInsert()
        {
            SqlParameter[] param = {new SqlParameter("@ProductCategoryName",ProductCategoryName),
                                   new SqlParameter("@AddedBy",AddedBy)};
            DataSet ds = DBHelper.ExecuteQuery("ExpectedProductCategoryInsert", param);
            return ds;
        }
        public DataSet UpdateProductCategory()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_ProductCategoryId", Pk_ProductCategoryId),
                                   new SqlParameter("@ProductCategoryName",ProductCategoryName),
                                   new SqlParameter("@UpdatedBy",UpdatedBy)};
            DataSet ds = DBHelper.ExecuteQuery("UpdateExpectedProductCategory", param);
            return ds;
        }
        
        public DataSet DeleteCategoryMaster()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_CategoryId", Pk_CategoryId),
                new SqlParameter("@DeletedBy", DeletedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteCategory", para);
            return ds;

        }

        public DataSet DeleteInterractionMaster()
        {
            SqlParameter[] para = { new SqlParameter("@PK_InterActionId", PK_InterActionId),
                new SqlParameter("@DeletedBy", DeletedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteInterractionMode", para);
            return ds;

        }

        public DataSet DeleteDataSourceMaster()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_SourceId", Pk_SourceId),
                new SqlParameter("@DeletedBy", DeletedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteDataSource", para);
            return ds;

        }

        public DataSet DeleteProspectActivityMaster()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_ActivityId", Pk_ActivityId),
                new SqlParameter("@DeletedBy", DeletedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteProspectActivity", para);
            return ds;

        }

        public DataSet DeleteBusinessChance()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_BusinessChanceId", Pk_BusinessChanceId),
                new SqlParameter("@DeletedBy", DeletedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteBusinessChance", para);
            return ds;

        }

        public DataSet DeleteProductCategory()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_ProductCategoryId", Pk_ProductCategoryId),
                new SqlParameter("@DeletedBy", DeletedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteProductCategory", para);
            return ds;

        }

        public DataSet SaveEmailTemplate()
        {
            SqlParameter[] para ={   new SqlParameter ("@TemplateSubject", Subject),
                                     new SqlParameter ("@TemplateBody", EmailBodyHTML),
                                     new SqlParameter ("@FilePath", SelectedFilePath),
                                     new SqlParameter("@AddedBy", AddedBy), };
            DataSet ds = DBHelper.ExecuteQuery("SaveEmailTemplate", para);
            return ds;
        }

    }
}
