using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Production_Costing_Software
{
    public partial class WelcomePageCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();

                }
                UserIdAdmin.Text = Session["UserId"].ToString();
                lblCompanyMasterList_Id.Text = Session["CompanyMaster_Id"].ToString();
                lblCompanyName.Text = Session["CompanyMasterList_Name"].ToString();

            }
            catch (Exception)
            {
                Response.Redirect("~/Login.aspx");

            }

        }
    }
}
