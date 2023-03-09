using BusinessAccessLayer;
using DataAccessLayer;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Production_Costing_Software
{
    public partial class CategoryMappingMaster : System.Web.UI.Page
    {
        int UserId;
        string Shipper_Id;
        int Company_Id;
        ProCategoryMappingMaster pro = new ProCategoryMappingMaster();
        ClsCategoryMappingMaster cls = new ClsCategoryMappingMaster();
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
                ProductCategoryCombo();
                BulkProductMasterCombo();
                TradeNameListData();
                Grid_CategoryMappingMasterData();
                DisplayView();
            }
        }

        public void GetUserRights()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[28]["GroupId"]);
            lblCanEdit.Text = Convert.ToBoolean(dtMenuList.Rows[28]["CanEdit"]).ToString();
            lblCanDelete.Text = Convert.ToBoolean(dtMenuList.Rows[28]["CanDelete"]).ToString();
            if (lblCanEdit.Text == "True")
            {
                if (lblCategoryMapping_Id.Text != "")
                {
                    AddCategoryMapping.Visible = false;
                    CancelCategoryMapping.Visible = true;
                    UpdateCategoryMapping.Visible = true;
                }
                else
                {
                    AddCategoryMapping.Visible = true;
                    CancelCategoryMapping.Visible = true;
                    UpdateCategoryMapping.Visible = false;
                }
            }
            else
            {
                AddCategoryMapping.Visible = false;
                CancelCategoryMapping.Visible = false;
            }

        }
        protected void DisplayView()
        {
            ClsMenuDisplay cls = new ClsMenuDisplay();
            ProRoleMaster pro = new ProRoleMaster();
            DataTable dtMenuList = new DataTable();
            pro.GroupId = Convert.ToInt32(lblGroupId.Text);
            dtMenuList = cls.Get_MenuListByGroup(pro);
            int GroupId = Convert.ToInt32(dtMenuList.Rows[28]["GroupId"]);

            if (Convert.ToBoolean(dtMenuList.Rows[28]["CanEdit"]) == true)
            {
                if (lblCategoryMapping_Id.Text!="")
                {
                    AddCategoryMapping.Visible = false;
                    CancelCategoryMapping.Visible = true;
                    UpdateCategoryMapping.Visible = true;
                }
                else
                {
                    AddCategoryMapping.Visible = true;
                    CancelCategoryMapping.Visible = true;
                    UpdateCategoryMapping.Visible = false;
                }
                

            }
            else
            {
                AddCategoryMapping.Visible = false;
                CancelCategoryMapping.Visible = false;

            }


        }

        public void GetLoginDetails()
        {
            if (Session["UserName"]!= null )
            {
                lblUserNametxt.Text = Session["UserName"].ToString().ToUpper();
                UserIdAdmin.Text = Session["UserId"].ToString();
                lblGroupId.Text = Session["GroupId"].ToString();
                //lblRoleId.Text = Session["RoleId"].ToString();
                lblUserId.Text = Session["UserId"].ToString();
                UserId = Convert.ToInt32(Session["UserId"].ToString());
                Company_Id = Convert.ToInt32(Session["CompanyMaster_Id"]);

            }
            else
            {
                Response.Redirect("~/Login.aspx");

            }

        }
        public void BulkProductMasterCombo()
        {
            Cls_comp_CompanyWiseFactoryExpence cls = new Cls_comp_CompanyWiseFactoryExpence();
            Comp_ProCoWiseFactoryExpenceMaster pro = new Comp_ProCoWiseFactoryExpenceMaster();
            DataTable dt = new DataTable();
            pro.Fk_CompanyList_Id = Company_Id;
            dt = cls.Get_BPM_From_Comp_FectoryExpence(pro);

            dt.Columns.Add("BPMValue", typeof(string), "Fk_BPM_Id + ' (' + PM_RM_Category_Id +')'").ToString();
            BulkProductNameDropDownList.DataSource = dt;
            BulkProductNameDropDownList.DataTextField = "BPM_Product_Name";

            BulkProductNameDropDownList.DataValueField = "BPMValue";
            BulkProductNameDropDownList.DataBind();
            BulkProductNameDropDownList.Items.Insert(0, "Select");
        }
        public void ProductCategoryCombo()
        {
            ClsProductCategoryMaster cls = new ClsProductCategoryMaster();
            ProductCategoryDropDown.DataSource = cls.Get_ProductCategoryMasterAll();
            ProductCategoryDropDown.DataTextField = "ProductCategoryName";
            ProductCategoryDropDown.DataValueField = "Product_Category_Id";
            ProductCategoryDropDown.DataBind();
            ProductCategoryDropDown.Items.Insert(0, "Select");


        }
        public void TradeNameListData()
        {
            ClsTradeNameMaster cls = new ClsTradeNameMaster();
            ProTradeNameMaster pro = new ProTradeNameMaster();
            pro.Comapny_Id = Company_Id;
            TradeNameDropdown.DataSource = cls.Get_TradeNameMasterAll(pro);
            TradeNameDropdown.DataTextField = "TradeName";
            TradeNameDropdown.DataValueField = "TradeName_Id";
            TradeNameDropdown.DataBind();
            TradeNameDropdown.Items.Insert(0, "Select");


        }
        protected void AddCategoryMapping_Click(object sender, EventArgs e)
        {
            //pro.TradeName_Id = Convert.ToInt32(TradeNameDropdown.SelectedValue);
            //Shipper_Id = Shippertype_Id.Split('(', ')')[1];
            //PackSizeBracket = Shippertype_Id.Split('(', ')')[2];
            //PackSize = Regex.Match(PackSizeBracket, @"\d+").Value;
            //lblPackSize.Text = PackSize;
            //lblPMRM_Category_Id.Text = Shipper_Id;
            //PackUniMeasurement = PackSizeBracket.Split('-', ']')[1];
            //lblPackMeasurement.Text = PackUniMeasurement;
            //lbl_BPM_Id.Text = Get_BPM_Id;
            //lblBPM_Id.Text = Regex.Match(BulkProductNameDropDownList.SelectedValue, @"\d+").Value;

            //pro.BPM_Id = Convert.ToInt32(BulkProductNameDropDownList.SelectedValue);
            DataTable dt = new DataTable();
            pro.Comapny_Id = Company_Id;
            dt = cls.Get_ProductCategoryMasterAll(pro);

            //foreach (DataRow row in dt.Rows)
            //{
            //    string Fk_ProductCategory_Id = row["Fk_ProductCategory_Id"].ToString();
            //    string Fk_TradeName_Id = row["Fk_TradeName_Id"].ToString();
            //    string Fk_BPM_Id = row["Fk_BPM_Id"].ToString();
            //    string PM_RM_Category_id = row["PM_RM_Category_id"].ToString();

            //    if (Fk_BPM_Id == pro.BPM_Id.ToString() && PM_RM_Category_id==pro.PMRM_Category_Id.ToString())
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Match Can not Inserted!')", true);
            //        return;
            //    }
            //}
            pro.TradeName_Id = Convert.ToInt32(TradeNameDropdown.SelectedValue);

            pro.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
            pro.ProductCategory_Id = Convert.ToInt32(ProductCategoryDropDown.SelectedValue);
            //pro.PackSize = decimal.Parse(lblPAckSize.Text);
            //pro.PackMeasurement = Convert.ToInt32(lblPackMeasurement.Text);
            pro.PMRM_Category_Id = Convert.ToInt32(lblPMRM_Category_Id.Text);
            pro.Comapny_Id = Company_Id;
            int status = cls.Insert_CategoryMappingMaster(pro);
            if (status > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                Grid_CategoryMappingMasterData();
                ClearData();
            }
        }
        public void Grid_CategoryMappingMasterData()
        {
            ClsCategoryMappingMaster cls = new ClsCategoryMappingMaster();
            pro.Comapny_Id = Company_Id;
            Grid_CategoryMappingMaster.DataSource = cls.Get_ProductCategoryMasterAll(pro);
            Grid_CategoryMappingMaster.DataBind();

        }
        protected void UpdateCategoryMapping_Click(object sender, EventArgs e)
        {
            pro.TradeName_Id = Convert.ToInt32(TradeNameDropdown.SelectedValue);
            pro.BPM_Id =Convert.ToInt32(lblBPM_Id.Text);
            pro.ProductCategory_Id = Convert.ToInt32(ProductCategoryDropDown.SelectedValue);
            pro.CategoryMapping_Id = Convert.ToInt32(lblCategoryMapping_Id.Text);
            //pro.PackSize = decimal.Parse(lblPAckSize.Text);
            //pro.PackMeasurement = Convert.ToInt32(lblPackMeasurement.Text);
            pro.PMRM_Category_Id = Convert.ToInt32(lblPMRM_Category_Id.Text);
            pro.Comapny_Id = Company_Id;
            DataTable dt = new DataTable();
            dt = cls.Get_ProductCategoryMasterAll(pro);

            //foreach (DataRow row in dt.Rows)
            //{
            //    //string Fk_ProductCategory_Id = row["Fk_ProductCategory_Id"].ToString();
            //    //string Fk_TradeName_Id = row["Fk_TradeName_Id"].ToString();
            //    string Fk_BPM_Id = row["Fk_BPM_Id"].ToString();
            //    string PM_RM_Category_id = row["PM_RM_Category_id"].ToString();
            //    if (Fk_BPM_Id == pro.BPM_Id.ToString() && PM_RM_Category_id == pro.PMRM_Category_Id.ToString())
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Add Multiple BulkProduct !')", true);
            //        return;
            //    }
            //}
            int status = cls.Update_CategoryMappingMaster(pro);
            if (status > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
                Grid_CategoryMappingMasterData();
                ClearData();
            }
        }

        protected void CancelCategoryMapping_Click(object sender, EventArgs e)
        {
            BulkProductNameDropDownList.SelectedIndex = 0;
            ProductCategoryDropDown.SelectedIndex = 0;

            TradeNameDropdown.SelectedIndex = 0;
            lblCategoryMapping_Id.Text = "";
            UpdateCategoryMapping.Visible = false;
            AddCategoryMapping.Visible = true;

        }
        public void ClearData()
        {
            BulkProductNameDropDownList.SelectedIndex = 0;
            ProductCategoryDropDown.SelectedIndex = 0;

            TradeNameDropdown.SelectedIndex = 0;
            lblCategoryMapping_Id.Text = "";
            UpdateCategoryMapping.Visible = false;
            AddCategoryMapping.Visible = true;
        }
        protected void DelCategoryMapping_Click(object sender, EventArgs e)
        {
            Button DeleteBtn = sender as Button;
            GridViewRow gdrow = DeleteBtn.NamingContainer as GridViewRow;
            int CategoryMapping_Id = Convert.ToInt32(Grid_CategoryMappingMaster.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.CategoryMapping_Id = CategoryMapping_Id;
            lblCategoryMapping_Id.Text = CategoryMapping_Id.ToString();
            DataTable dt = new DataTable();
            dt = cls.Get_CategoryMappingMasterById(pro);

            lblBPM_Id.Text = dt.Rows[0]["Fk_BPM_Id"].ToString();
            lblPMRM_Category_Id.Text = dt.Rows[0]["Fk_PM_RM_Category_Id"].ToString();
            pro.PMRM_Category_Id = Convert.ToInt32(lblPMRM_Category_Id.Text);
            pro.BPM_Id = Convert.ToInt32(lblBPM_Id.Text);
            pro.Comapny_Id = Company_Id;
            int status = cls.Delete_CategoryMappingMaster(pro);
            if (status > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);
                Grid_CategoryMappingMasterData();
                ClearData();
            }
        }

        protected void EditCategoryMapping_Click(object sender, EventArgs e)
        {
            Button EditBtn = sender as Button;
            GridViewRow gdrow = EditBtn.NamingContainer as GridViewRow;
            int CategoryMapping_Id = Convert.ToInt32(Grid_CategoryMappingMaster.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.CategoryMapping_Id = CategoryMapping_Id;
            lblCategoryMapping_Id.Text = CategoryMapping_Id.ToString();
            DataTable dt = new DataTable();
            pro.User_Id = UserId;
            BulkProductMasterCombo();
            dt = cls.Get_CategoryMappingMasterById(pro);

            BulkProductNameDropDownList.SelectedValue = dt.Rows[0]["Fk_BPM_Id"].ToString() + " (" + dt.Rows[0]["Fk_PM_RM_Category_Id"].ToString() + ")";

            //BulkProductNameDropDownList.SelectedValue = (dt.Rows[0]["Fk_BPM_Id"] + " (" + dt.Rows[0]["PM_RM_Category_id"] + ")").ToString();

            ProductCategoryDropDown.SelectedValue = dt.Rows[0]["Fk_ProductCategory_Id"].ToString();

            TradeNameDropdown.SelectedValue = dt.Rows[0]["Fk_TradeName_Id"].ToString();
            lblBPM_Id.Text = dt.Rows[0]["Fk_BPM_Id"].ToString();
            lblPMRM_Category_Id.Text = dt.Rows[0]["Fk_PM_RM_Category_Id"].ToString();
            UpdateCategoryMapping.Visible = true;
            AddCategoryMapping.Visible = false;
        }





        protected void BulkProductNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {


            string BPM_Full_Id = BulkProductNameDropDownList.SelectedValue.ToString();

            string Shippertype_Id = BPM_Full_Id;

            Shipper_Id = Shippertype_Id.Split('(', ')')[1];
            lblPMRM_Category_Id.Text = Shipper_Id;

            string Get_BPM_Id = Regex.Match(Shippertype_Id, @"\d+").Value;
            lblBPM_Id.Text = Get_BPM_Id;
            //lblPackSize.Text = PackSize;
            //lblPMRM_Category_Id.Text = Shipper_Id;

            //lblPackMeasurement.Text = PackUniMeasurement;

            //PackingSizeDropDownListData();
            DataTable dt = new DataTable();
            pro.Comapny_Id = Company_Id;
            lblCompany_Id.Text = Company_Id.ToString();
            dt = cls.Get_ProductCategoryMasterAll(pro);
            foreach (DataRow row in dt.Rows)
            {
                //string Fk_ProductCategory_Id = row["Fk_ProductCategory_Id"].ToString();
                //string Fk_TradeName_Id = row["Fk_TradeName_Id"].ToString();
                string Fk_BPM_Id = row["Fk_BPM_Id"].ToString();
                string PM_RM_Category_id = row["PM_RM_Category_id"].ToString();
                string Company_Id = (row["Fk_Company_Id"]).ToString();

                if (Fk_BPM_Id == lblBPM_Id.Text && PM_RM_Category_id == lblPMRM_Category_Id.Text && Company_Id == lblCompany_Id.Text)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Can not Add Multiple BulkProduct !')", true);
                    ClearData();
                    return;
                }
            }
        }
    }
}