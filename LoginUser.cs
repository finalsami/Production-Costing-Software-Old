using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Production_Costing_Software
{
    public class LoginUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsValid()
        {
            int count = 0;


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ProductionCostingSoftware"].ConnectionString);

            SqlCommand cmd = new SqlCommand("SELECT * FROM pcs_UserLogin where UserName ='" + UserName + "' and Password ='" + Password + "'", con);
            con.Open();
            count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count > 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}