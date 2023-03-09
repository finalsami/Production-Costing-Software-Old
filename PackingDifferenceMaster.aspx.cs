using BusinessAccessLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Production_Costing_Software
{
    public partial class PackingDifferenceMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetLoginDetails();
            GetUserRights();
            if (!IsPostBack)
            {
                lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
                UserIdAdmin.Text = Session["UserId"].ToString();
                lblGroupId.Text = Session["GroupId"].ToString();
                //lblRoleId.Text = Session["RoleId"].ToString();
                lblUserId.Text = Session["UserId"].ToString();

                Grid_PackingDifferenceMasterData();
                DisplayView();
            }
        }
        public void GetLoginDetails()
        {
            if (Session["UserName"] != null && Session["UserName"].ToString().ToUpper() != "")
            {
                lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
                UserIdAdmin.Text = Session["UserId"].ToString();
                lblGroupId.Text = Session["GroupId"].ToString();
                //lblRoleId.Text = Session["RoleId"].ToString();
                lblUserId.Text = Session["UserId"].ToString();
            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }
        public void GetUserRights()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[5]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[5]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[5]["CanDelete"]).ToString();


        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[5]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[5]["CanEdit"]).ToString();

        }
        public void Grid_PackingDifferenceMasterData()
        {
            DataTable dtIsMasterPacking = new DataTable();
            ClsPackingDifferenceMaster cls = new ClsPackingDifferenceMaster();

            DataTable dtCWFE = new DataTable();
            dtCWFE = cls.CHECK_PackingDifferenceMaster();
            if (dtCWFE.Rows.Count > 0)

            {
                Grid_PackingDifferenceMaster.DataSource = dtCWFE;
                Grid_PackingDifferenceMaster.DataBind();

                foreach (GridViewRow row2 in Grid_PackingDifferenceMaster.Rows)
                {
                    DataRowView data = (DataRowView)row2.DataItem;
                    TextBox txtType = (TextBox)row2.FindControl("SuggPackDiffPerLtrtxt");
                    TextBox txtType1 = (TextBox)row2.FindControl("SuggPackDiffPerLtrGptxt");
                    TextBox txtType2= (TextBox)row2.FindControl("SuggPackDiffPerLtrAgrostartxt");
                    TextBox txtType3= (TextBox)row2.FindControl("SuggPackDiffPerLtrGramofonetxt");
                    TextBox txtType4= (TextBox)row2.FindControl("SuggPackDiffPerLtrMPPLtxt");
                    TextBox txtType5= (TextBox)row2.FindControl("SuggPackDiffPerLtrDehaattxt");
                    String SuggPackDiffPerLtr = txtType.Text;
                    lblIsMasterPacking.Text = (row2.FindControl("lblIsMasterPacking") as Label).Text;

                    if (lblIsMasterPacking.Text == "True")
                    {
                        txtType.Enabled = false;
                        txtType1.Enabled = false;
                        txtType2.Enabled = false;
                        txtType3.Enabled = false;
                        txtType4.Enabled = false;
                        txtType5.Enabled = false;
                        row2.BackColor = System.Drawing.Color.Cornsilk;
                        row2.Font.Bold = true;
                    }

                }
            }
            else
            {
                Grid_PackingDifferenceMaster.DataSource = dtIsMasterPacking = cls.Get_PackingDifferenceMaster();
                Grid_PackingDifferenceMaster.DataBind();
                foreach (GridViewRow row2 in Grid_PackingDifferenceMaster.Rows)
                {
                    DataRowView data = (DataRowView)row2.DataItem;
                    TextBox txtType = (TextBox)row2.FindControl("SuggPackDiffPerLtrtxt");
                    TextBox txtType1 = (TextBox)row2.FindControl("SuggPackDiffPerLtrGptxt");
                    TextBox txtType2 = (TextBox)row2.FindControl("SuggPackDiffPerLtrAgrostartxt");
                    TextBox txtType3 = (TextBox)row2.FindControl("SuggPackDiffPerLtrGramofonetxt");
                    TextBox txtType4 = (TextBox)row2.FindControl("SuggPackDiffPerLtrMPPLtxt");
                    TextBox txtType5 = (TextBox)row2.FindControl("SuggPackDiffPerLtrDehaattxt");

                    String SuggPackDiffPerLtr = txtType.Text;
                    lblIsMasterPacking.Text = (row2.FindControl("lblIsMasterPacking") as Label).Text;

                    if (lblIsMasterPacking.Text == "True")
                    {
                        txtType.Enabled = false;
                        txtType1.Enabled = false;
                        txtType2.Enabled = false;
                        txtType3.Enabled = false;
                        txtType4.Enabled = false;
                        txtType5.Enabled = false;
                        row2.BackColor = System.Drawing.Color.Cornsilk;
                        row2.Font.Bold = true;
                    }

                }
            }

        }



        protected void SuggPackDiffPerLtrtxt_TextChanged(object sender, EventArgs e)
        {
            decimal SuggPackDiffPerLtr = 0;
            decimal TotalAmtPerUnitMaster = 0;
            decimal PackingSize = 0;
            int PackingMeasurement = 0;


            TextBox btn = (TextBox)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

           
                if (gvr.FindControl("lblMasterPack") != null as Label)
                {
                    SuggPackDiffPerLtr = (Convert.ToDecimal((gvr.FindControl("SuggPackDiffPerLtrtxt") as TextBox).Text));
                    (gvr.FindControl("SuggPackDiffPerLtrGptxt") as TextBox).Text = (gvr.FindControl("SuggPackDiffPerLtrtxt") as TextBox).Text;
                    (gvr.FindControl("SuggPackDiffPerLtrAgrostartxt") as TextBox).Text = (gvr.FindControl("SuggPackDiffPerLtrtxt") as TextBox).Text;
                    (gvr.FindControl("SuggPackDiffPerLtrGramofonetxt") as TextBox).Text = (gvr.FindControl("SuggPackDiffPerLtrtxt") as TextBox).Text;
                    (gvr.FindControl("SuggPackDiffPerLtrMPPLtxt") as TextBox).Text = (gvr.FindControl("SuggPackDiffPerLtrtxt") as TextBox).Text;
                    (gvr.FindControl("SuggPackDiffPerLtrDehaattxt") as TextBox).Text = (gvr.FindControl("SuggPackDiffPerLtrtxt") as TextBox).Text;
                TotalAmtPerUnitMaster = (Convert.ToDecimal((gvr.FindControl("lblMasterPack") as Label).Text));
                    PackingSize = (Convert.ToDecimal((gvr.FindControl("lblPackingSize") as Label).Text));
                    PackingMeasurement = (Convert.ToInt32((gvr.FindControl("lblPackingMeasurement") as Label).Text));
                    (gvr.FindControl("lblnewNRVtxt") as Label).Text = ((TotalAmtPerUnitMaster) + (SuggPackDiffPerLtr)).ToString("0.00");
                }
             

            
        }

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            ClsPackingDifferenceMaster cls = new ClsPackingDifferenceMaster();
            ProPackingDifferenceMaster pro = new ProPackingDifferenceMaster();
            int status = 0;



            DataTable dtCWFE = new DataTable();
            dtCWFE = cls.CHECK_PackingDifferenceMaster();
            if (dtCWFE.Rows.Count > 0)
            {
                foreach (GridViewRow row1 in Grid_PackingDifferenceMaster.Rows)
                {

                    pro.Fk_CWFE_Id = (Convert.ToInt32((row1.FindControl("lblProductwise_Labor_cost_Id") as Label).Text));
                    pro.Pack_Diff = (Convert.ToDecimal((row1.FindControl("lblPackDifference") as Label).Text));
                    pro.UnitMeasuremnt_Id = (Convert.ToInt32((row1.FindControl("lblPackingMeasurement") as Label).Text));
                    pro.PackSize = (Convert.ToDecimal((row1.FindControl("lblPackingSize") as Label).Text));
                    pro.Fk_BPM_Id = (Convert.ToInt32((row1.FindControl("lblFk_BPM_Id") as Label).Text));
                    pro.PMRM_Category_Id = (Convert.ToInt32((row1.FindControl("lblPM_RM_Category_Id") as Label).Text));
                    pro.NRV = (Convert.ToDecimal((row1.FindControl("lblTotalAmtPerLiter") as Label).Text));
                    pro.IsMasterPacking = Convert.ToBoolean((row1.FindControl("lblIsMasterPacking") as Label).Text);

                    pro.Suggest_Pack_Diff = (Convert.ToDecimal((row1.FindControl("SuggPackDiffPerLtrtxt") as TextBox).Text));
                    pro.Gp_Pack_Diff_Ltr = (Convert.ToDecimal((row1.FindControl("SuggPackDiffPerLtrGptxt") as TextBox).Text));
                    pro.Agro_Pack_Diff_Ltr = (Convert.ToDecimal((row1.FindControl("SuggPackDiffPerLtrAgrostartxt") as TextBox).Text));
                    pro.GramoFone_Pack_Diff_Ltr = (Convert.ToDecimal((row1.FindControl("SuggPackDiffPerLtrGramofonetxt") as TextBox).Text));
                    pro.MPPL_Pack_Diff_Ltr = (Convert.ToDecimal((row1.FindControl("SuggPackDiffPerLtrMPPLtxt") as TextBox).Text));
                    pro.Dehaat_Pack_Diff_Ltr = (Convert.ToDecimal((row1.FindControl("SuggPackDiffPerLtrDehaattxt") as TextBox).Text));
                    pro.TotalCostPerLtr = (Convert.ToDecimal((row1.FindControl("lblMasterPack") as Label).Text));

                    pro.newNRV = (pro.TotalCostPerLtr + pro.Suggest_Pack_Diff);
                    status = cls.UPDATE_PackingDifference(pro);

                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated!')", true);
                Grid_PackingDifferenceMasterData();

            }
            else
            {
                foreach (GridViewRow row2 in Grid_PackingDifferenceMaster.Rows)
                {

                    //pro.PackingDiff_Id = Convert.ToInt32(dtCWFE.Rows[i]["Packing_Difference_Id"]);
                    pro.Fk_CWFE_Id = (Convert.ToInt32((row2.FindControl("lblProductwise_Labor_cost_Id") as Label).Text));
                    pro.Fk_BPM_Id = (Convert.ToInt32((row2.FindControl("lblFk_BPM_Id") as Label).Text));
                    pro.PMRM_Category_Id = (Convert.ToInt32((row2.FindControl("lblPM_RM_Category_Id") as Label).Text));

                    pro.PackSize = Convert.ToDecimal((row2.FindControl("lblPackingSize") as Label).Text);
                    pro.UnitMeasuremnt_Id = Convert.ToInt32((row2.FindControl("lblPackingMeasurement") as Label).Text);
                    pro.Pack_Diff = Convert.ToDecimal((row2.FindControl("lblPackDifference") as Label).Text);
                    pro.NRV = Convert.ToDecimal((row2.FindControl("lblTotalAmtPerLiter") as Label).Text);
                    pro.Suggest_Pack_Diff = (Convert.ToDecimal((row2.FindControl("SuggPackDiffPerLtrtxt") as TextBox).Text));
                    pro.Gp_Pack_Diff_Ltr = (Convert.ToDecimal((row2.FindControl("SuggPackDiffPerLtrGptxt") as TextBox).Text));
                    pro.Agro_Pack_Diff_Ltr = (Convert.ToDecimal((row2.FindControl("SuggPackDiffPerLtrAgrostartxt") as TextBox).Text));
                    pro.GramoFone_Pack_Diff_Ltr = (Convert.ToDecimal((row2.FindControl("SuggPackDiffPerLtrGramofonetxt") as TextBox).Text));
                    pro.MPPL_Pack_Diff_Ltr = (Convert.ToDecimal((row2.FindControl("SuggPackDiffPerLtrMPPLtxt") as TextBox).Text));
                    pro.Dehaat_Pack_Diff_Ltr = (Convert.ToDecimal((row2.FindControl("SuggPackDiffPerLtrDehaattxt") as TextBox).Text));
                    pro.newNRV = (Convert.ToDecimal((row2.FindControl("lblnewNRVtxt") as Label).Text));
                    pro.IsMasterPacking = Convert.ToBoolean((row2.FindControl("lblIsMasterPacking") as Label).Text);
                    status = cls.INSERT_PackingDifference(pro);

                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted!')", true);
                Grid_PackingDifferenceMasterData();

            }

        }

        protected void Grid_PackingDifferenceMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

    }
}
