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
    public partial class ProductInterestMaster : System.Web.UI.Page
    {
        ProBulkProductInterestMaster pro = new ProBulkProductInterestMaster();
        ClsBulkProductInterestMaster cls = new ClsBulkProductInterestMaster();
        int UserId;
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
                GridBulkInterestMaster();
                BulkProductDropDownListCombo();
                ClearData();
                DisplayView();
            }

        }
        public void BulkProductDropDownListCombo()
        {
            ClsBulkProductMaster cls = new ClsBulkProductMaster();
            DataTable dt = new DataTable();
            dt = cls.Get_BP_MasterDataCombo(UserId);


            DataView dvOptions = new DataView(dt);
            dvOptions.Sort = "BulkProductName";

            BulkProductNameDropDown.DataSource = dvOptions;

            BulkProductNameDropDown.DataTextField = "BulkProductName";
            BulkProductNameDropDown.DataValueField = "BPM_Product_Id";
            BulkProductNameDropDown.DataBind();
            BulkProductNameDropDown.Items.Insert(0, "Select");
        }

        public void GridBulkInterestMaster()
        {
            pro.User_Id = Convert.ToInt32(lblUserId.Text);
            Grid_ProductInterestMaster.DataSource = cls.Get_BP_InterestMasterData();
            Grid_ProductInterestMaster.DataBind();
        }
        public void ClearData()
        {
            BulkProductNameDropDown.SelectedIndex = 0;
            BulkCostLtrtxt.Text = "0";
            InterestAmttxt.Text = "0";
            InterestPertxt.Text = "0";
            TotalAmounttxt.Text = "0";
            BPM_Interest_UpdateBtn.Visible = false;
            BPM_Interest_AddBtn.Visible = true;

            // code added by harshul 15-4-2022
            /* when we edit the item and then click on cancel the primary id ( lblBulkProduct_Interest_Master_Id) value is not clearing */
            lblBulkProduct_Interest_Master_Id.Text = "";
        }
        public void GetLoginDetails()
        {
            if (Session["UserName"] != null)
            {
                lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
                UserIdAdmin.Text = Session["UserId"].ToString();
                lblGroupId.Text = Session["GroupId"].ToString();
                //lblRoleId.Text = Session["RoleId"].ToString();
                lblUserId.Text = Session["UserId"].ToString();
                UserId = Convert.ToInt32(Session["UserId"]);
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
            int GroupId = Convert.ToInt32(dtMenuList.Rows[14]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[14]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[14]["CanDelete"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[14]["CanEdit"]) == true)
            {
                if (lblBulkProduct_Interest_Master_Id.Text != "")
                {
                    BPM_Interest_AddBtn.Visible = false;
                    BPM_Interest_UpdateBtn.Visible = true;
                    BPMInterestCancelBtn.Visible = true;
                }

                else
                {
                    BPM_Interest_AddBtn.Visible = true;
                    BPM_Interest_UpdateBtn.Visible = false;
                    BPMInterestCancelBtn.Visible = true;

                }
            }
            else
            {
                BPM_Interest_AddBtn.Visible = false;
                BPM_Interest_UpdateBtn.Visible = false;
                BPMInterestCancelBtn.Visible = false;

            }
        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[14]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[14]["CanEdit"]).ToString();
            if (Convert.ToBoolean(dtMenuList.Rows[14]["CanEdit"]) == true)
            {
                if (lblBulkProduct_Interest_Master_Id.Text != "")
                {
                    BPM_Interest_AddBtn.Visible = false;
                    BPM_Interest_UpdateBtn.Visible = true;
                    BPMInterestCancelBtn.Visible = true;
                }

                else
                {
                    BPM_Interest_AddBtn.Visible = true;
                    BPM_Interest_UpdateBtn.Visible = false;
                    BPMInterestCancelBtn.Visible = true;

                }
            }
            else
            {
                BPM_Interest_AddBtn.Visible = false;
                BPM_Interest_UpdateBtn.Visible = false;
                BPMInterestCancelBtn.Visible = false;

            }
        }
        protected void BPM_Interest_AddBtn_Click(object sender, EventArgs e)
        {
            pro.BPM_Product_Id = Convert.ToInt32(BulkProductNameDropDown.SelectedValue);

            pro.BRBOM_BulkCost = decimal.Parse(BulkCostLtrtxt.Text);
            pro.InterestAmount = decimal.Parse(InterestAmttxt.Text);
            pro.InterestPer = decimal.Parse(InterestPertxt.Text);
            pro.TotalAmount = decimal.Parse(TotalAmounttxt.Text);
            pro.User_Id = UserId;

            int status = cls.InsertBulkProductionInterestMasterData(pro);

            if (status > 0)
            {
                GridBulkInterestMaster();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bulk Product Interest Inserted Successfully')", true);
                ClearData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed!')", true);

            }
        }

        protected void BPM_Interest_UpdateBtn_Click(object sender, EventArgs e)
        {
            pro.BPM_Product_Id = Convert.ToInt32(BulkProductNameDropDown.SelectedValue);

            pro.BRBOM_BulkCost = decimal.Parse(BulkCostLtrtxt.Text);
            pro.InterestAmount = decimal.Parse(InterestAmttxt.Text);
            pro.InterestPer = decimal.Parse(InterestPertxt.Text);
            pro.TotalAmount = decimal.Parse(TotalAmounttxt.Text);
            pro.BulkProductInterestMaster_Id = Convert.ToInt32(lblBulkProduct_Interest_Master_Id.Text);
            pro.User_Id = UserId;

            int status = cls.UpdatetBulkProductionInterestMasterData(pro);

            if (status > 0)
            {
                GridBulkInterestMaster();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bulk Product Interest Updated Successfully')", true);
                ClearData();
                BPM_Interest_UpdateBtn.Visible = false;
                BPM_Interest_AddBtn.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Failed!')", true);

            }
        }

        protected void BulkProductNameDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BulkProductNameDropDown.SelectedValue != "Select")
            {

                ClsBulkRecipeBOM cls = new ClsBulkRecipeBOM();
                 ClsBulkProductInterestMaster  clsBPIM= new ClsBulkProductInterestMaster();
                int BPM_Id = Convert.ToInt32(BulkProductNameDropDown.SelectedValue);
                DataTable dtCheckBPM= new DataTable();

                dtCheckBPM = clsBPIM.CheckBPM_For_BulkProductInterestMaster(BPM_Id);
                if (dtCheckBPM.Rows.Count>0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Add Multiple BulkProduct !')", true);
                    BulkProductNameDropDown.SelectedIndex = 0;
                    ClearData();
                    return;
                }
                DataTable dt = new DataTable();
                dt = cls.Get_FinalCostBulk_BRBOMby_BPM_Id(UserId, BPM_Id);
                if (dt.Rows.Count > 0)
                {
                    BulkCostLtrtxt.Text = dt.Rows[0]["FinalCostBulk"].ToString();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bulk Cost / Ltr Not Found !')", true);

                }

            }
        }

        protected void InterestPertxt_TextChanged(object sender, EventArgs e)
        {
            if (InterestPertxt.Text != "0" || InterestPertxt.Text != "")
            {
                InterestAmttxt.Text = (decimal.Parse(BulkCostLtrtxt.Text) * decimal.Parse(InterestPertxt.Text) / 100).ToString("0.000");
                TotalAmounttxt.Text = (decimal.Parse(BulkCostLtrtxt.Text) + decimal.Parse(InterestAmttxt.Text)).ToString("0.000");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Interest !')", true);

            }

        }

        protected void BPMInterestCancelBtn_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void EditBulkProducationInterestBtn_Click(object sender, EventArgs e)
        {
            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int BulkProductInterest_Id = Convert.ToInt32(Grid_ProductInterestMaster.DataKeys[gdrow.RowIndex].Value.ToString());
            lblBulkProduct_Interest_Master_Id.Text = BulkProductInterest_Id.ToString();
            DataTable dt = new DataTable();
            pro.BulkProductInterestMaster_Id = Convert.ToInt32(lblBulkProduct_Interest_Master_Id.Text);
            dt = cls.Get_BP_InterestMasterDataById(pro);
            BulkCostLtrtxt.Text = dt.Rows[0]["BRBOMBulkCost"].ToString();
            BulkProductNameDropDown.SelectedValue = dt.Rows[0]["Fk_BPM_Id"].ToString();
            InterestPertxt.Text = dt.Rows[0]["Interest"].ToString();
            InterestAmttxt.Text = dt.Rows[0]["InterestAmount"].ToString();
            TotalAmounttxt.Text = dt.Rows[0]["TotalAmount"].ToString();


            if (lblBulkProduct_Interest_Master_Id.Text != "")
            {
                BPM_Interest_AddBtn.Visible = false;
                BPM_Interest_UpdateBtn.Visible = true;
                BPMInterestCancelBtn.Visible = true;
            }

            else
            {
                BPM_Interest_AddBtn.Visible = true;
                BPM_Interest_UpdateBtn.Visible = false;
                BPMInterestCancelBtn.Visible = true;

            }
        }

        protected void Grid_DelBtn_Click(object sender, EventArgs e)
        {
            Button Delete = sender as Button;
            GridViewRow gdrow = Delete.NamingContainer as GridViewRow;
            int BulkProductInterest_Id = Convert.ToInt32(Grid_ProductInterestMaster.DataKeys[gdrow.RowIndex].Value.ToString());
            lblBulkProduct_Interest_Master_Id.Text = BulkProductInterest_Id.ToString();
            DataTable dt = new DataTable();
            pro.BulkProductInterestMaster_Id = Convert.ToInt32(lblBulkProduct_Interest_Master_Id.Text);
 
            int status = cls.Delete_BulkProductInterestMaster(pro);

            if (status > 0)
            {
                GridBulkInterestMaster();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bulk Product Interest Deleted Successfully')", true);
                ClearData();
                if (lblBulkProduct_Interest_Master_Id.Text != "")
                {
                    BPM_Interest_AddBtn.Visible = false;
                    BPM_Interest_UpdateBtn.Visible = true;
                    BPMInterestCancelBtn.Visible = true;
                }

                else
                {
                    BPM_Interest_AddBtn.Visible = true;
                    BPM_Interest_UpdateBtn.Visible = false;
                    BPMInterestCancelBtn.Visible = true;

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Failed!')", true);

            }
        }
    }
}