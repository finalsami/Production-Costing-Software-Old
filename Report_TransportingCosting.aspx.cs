using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;
namespace Production_Costing_Software
{
    public partial class Report_TransportingCosting : System.Web.UI.Page
    {
        ProTransportationCostFactors pro = new ProTransportationCostFactors();
        ClsTransportationCostFactors cls = new ClsTransportationCostFactors();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Company_Id"].ToString()!=null)
            {
                lblCompany_Id.Text = Session["Company_Id"].ToString().ToUpper();
                lblStateID.Text = Session["StateID"].ToString();
                lblTransportationCostFactors_Id.Text = Session["TransportationCostFactors_Id"].ToString();
                Get_TransportationCostingReportByStateIdAndMasterPack();

            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }
        public void Get_TransportationCostingReportByStateId()
        {
            DataTable dtReport = new DataTable();

            pro.Transportation_State_Id = Convert.ToInt32(lblStateID.Text);
            pro.TransportationCostFactors_Id = Convert.ToInt32(lblTransportationCostFactors_Id.Text);
            pro.Company_Id = Convert.ToInt32(lblCompany_Id.Text);
            dtReport = cls.Get_TransportationCostingReportByStateId(pro);

            if (dtReport.Rows.Count>0)
            {
                lblStateName.Text = dtReport.Rows[0]["StateName"].ToString();
            
                Grid_TransportReportList.DataSource = dtReport;
                Grid_TransportReportList.DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Data Found !')", true);

            }

        }
        public void Get_TransportationCostingReportByStateIdAndMasterPack()
        {
            DataTable dtReport = new DataTable();

            pro.Transportation_State_Id = Convert.ToInt32(lblStateID.Text);
            pro.TransportationCostFactors_Id = Convert.ToInt32(lblTransportationCostFactors_Id.Text);
            pro.Company_Id = Convert.ToInt32(lblCompany_Id.Text);
            dtReport = cls.Get_TransportationCostingReportByStateIdAndMasterPack(pro);
            lblStateName.Text = dtReport.Rows[0]["StateName"].ToString();
            Grid_TransportReportList.DataSource = dtReport;
            Grid_TransportReportList.DataBind();
        }

        protected void MasterAllOrMasterDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MasterAllOrMasterDropdown.SelectedValue=="1")
            {
                Get_TransportationCostingReportByStateIdAndMasterPack();

            }
            else
            {
                Get_TransportationCostingReportByStateId();
            }
        }
    }
}