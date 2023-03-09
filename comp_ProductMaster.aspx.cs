using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using BusinessAccessLayer;
using System.Data;

namespace Production_Costing_Software
{
    public partial class comp_ProductMaster : System.Web.UI.Page
    {
        int User_Id;
        string CompanyName;
        int Company_Id;
        Comp_ProProductMaster pro = new Comp_ProProductMaster();
        protected void Page_Load(object sender, EventArgs e)
        {
            GetLoginDetails();


            if (!IsPostBack)
            {

                Grid_comp_ProductMasterListData();
                BulkProductDropDownListCombo();
                TradeNameDropDownListCombo();
            }
        }
        public void BulkProductDropDownListCombo()
        {
            ClsProductwiseLabourCost cls = new ClsProductwiseLabourCost();

            DataTable dt = new DataTable();
            //pro.User_Id = User_Id;
            dt = cls.GetBulkProduct_From_ProductwiseLabourCost();

            BulkproductDropdownList.DataSource = dt;
            BulkproductDropdownList.DataTextField = "BPM_Product_Name";


            BulkproductDropdownList.DataValueField = "Productwise_Labor_cost_Id";
            BulkproductDropdownList.DataBind();
            BulkproductDropdownList.Items.Insert(0, "Select");
        }
        public void TradeNameDropDownListCombo()
        {
            Cls_comp_TradeNameMaster cls = new Cls_comp_TradeNameMaster();

            DataTable dt = new DataTable();
            //pro.User_Id = User_Id;
            Comp_ProTradeNameMaster comp_pro = new Comp_ProTradeNameMaster();
            comp_pro.comp_CompanyMaster_Id = Company_Id;
            dt = cls.Get_Comp_TradeNameMasterData(comp_pro);

            TradeNameDropdownList.DataSource = dt;
            TradeNameDropdownList.DataTextField = "comp_TradeName";


            TradeNameDropdownList.DataValueField = "comp_TradeName_Id";
            TradeNameDropdownList.DataBind();
            TradeNameDropdownList.Items.Insert(0, "Select");
        }
        public void Grid_comp_ProductMasterListData()
        {
            Cls_comp_ProductMaster cls = new Cls_comp_ProductMaster();
            Comp_ProProductMaster pro = new Comp_ProProductMaster();
            pro.comp_CompanyList_Id = Company_Id;
            Grid_comp_ProductMasterList.DataSource = cls.Get_Comp_ProductMasterData(pro);
            Grid_comp_ProductMasterList.DataBind();
        }
        public void GetLoginDetails()
        {

            User_Id = Convert.ToInt32(Session["UserId"]);
            CompanyName = Session["CompanyMasterList_Name"].ToString();
            Company_Id = Convert.ToInt32(Session["CompanyMaster_Id"]);
        }
        protected void AddProductMaster_Click(object sender, EventArgs e)
        {
            Cls_comp_ProductMaster cls = new Cls_comp_ProductMaster();
            pro.comp_TradeNameMaster_Id = Convert.ToInt32(TradeNameDropdownList.SelectedValue);
            pro.comp_BulkProduct_Id = Convert.ToInt32(BulkproductDropdownList.SelectedValue);
            pro.comp_CompanyList_Id = Company_Id;
            int status = cls.Insert_Comp_ProductMasterData(pro);

            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

                Grid_comp_ProductMasterListData();
                ClearData();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed')", true);

            }
        }
        public void ClearData()
        {
            BulkproductDropdownList.SelectedIndex = 0;
            TradeNameDropdownList.SelectedIndex = 0;
            lblcomp_ProductMaster_Id.Text = "";

        }

        protected void GridEditProductMasterBtn_Click(object sender, EventArgs e)
        {
            Cls_comp_ProductMaster cls = new Cls_comp_ProductMaster();

            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int comp_ProdcutMaster_Id = Convert.ToInt32(Grid_comp_ProductMasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            lblcomp_ProductMaster_Id.Text = comp_ProdcutMaster_Id.ToString();
            DataTable dt = new DataTable();
            pro.comp_ProductMaster_Id = comp_ProdcutMaster_Id;
            dt = cls.Get_Comp_ProductMasterDataBy_Id(pro);
            //TradeNameDropDownListCombo();
            TradeNameDropdownList.SelectedValue = dt.Rows[0]["Fk_TradeNameMaster_Id"].ToString();
            BulkproductDropdownList.SelectedValue = dt.Rows[0]["Fk_ProductwiseLabourCost_Id"].ToString();

            UpdateProductMaster.Visible = true;

            AddProductMaster.Visible = false;
        }

        protected void GridDelProductMasterBtn_Click(object sender, EventArgs e)
        {
            Cls_comp_ProductMaster cls = new Cls_comp_ProductMaster();

            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int comp_ProductMaster_Id = Convert.ToInt32(Grid_comp_ProductMasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.comp_ProductMaster_Id = comp_ProductMaster_Id;
            int status = cls.Delete_Comp_ProductMasterData(pro);
            if (status > 0)
            {
                Grid_comp_ProductMasterListData();
                ClearData();
            }

        }

        protected void UpdateProductMaster_Click(object sender, EventArgs e)
        {
            Cls_comp_ProductMaster cls = new Cls_comp_ProductMaster();
            pro.comp_ProductMaster_Id = Convert.ToInt32(lblcomp_ProductMaster_Id.Text);
            pro.comp_BulkProduct_Id = Convert.ToInt32(BulkproductDropdownList.SelectedValue);
            pro.comp_TradeNameMaster_Id = Convert.ToInt32(TradeNameDropdownList.SelectedValue);
            pro.comp_CompanyList_Id = Company_Id;
            int status = cls.Update_Comp_ProductMasterData(pro);

            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Successfully')", true);

                Grid_comp_ProductMasterListData();
                ClearData();
                UpdateProductMaster.Visible = false;

                AddProductMaster.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Failed')", true);

            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            BulkproductDropdownList.SelectedIndex = 0;
            TradeNameDropdownList.SelectedIndex = 0;
            lblcomp_ProductMaster_Id.Text = "";
            UpdateProductMaster.Visible = false;
            AddProductMaster.Visible = true;
        }
    }
}