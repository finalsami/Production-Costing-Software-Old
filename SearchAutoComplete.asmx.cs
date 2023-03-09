using BusinessAccessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Production_Costing_Software
{
    /// <summary>
    /// Summary description for SearchAutoComplete
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SearchAutoComplete : System.Web.Services.WebService
    {

        [WebMethod]
        public DataTable SearchRMData(ProRMMaster pro,int MainCategory_Id)
        {

            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand("sp_AutoComplete_Search_RM", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchRM_Name", pro.Search);
                cmd.Parameters.AddWithValue("@MainCategory_Id", MainCategory_Id);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                cmd.Dispose();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                dt.Dispose();
            }
            return dt;
        }
    }
}
