using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Production_Costing_Software
{
    public partial class PriceList_Other_Actual : System.Web.UI.Page
    {
        string Status = "Actual";
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Status"] = Status;
            Response.Redirect("~/EstimatePriceList.aspx");
           
        }
    }
}