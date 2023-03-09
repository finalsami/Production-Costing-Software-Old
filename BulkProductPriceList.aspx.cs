using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
namespace Production_Costing_Software
{
    public partial class BulkProductPriceList : System.Web.UI.Page
    {
        ClsRMMaster cls = new ClsRMMaster();
        int UserId;
        protected void Page_Load(object sender, EventArgs e)
        {
            GetLoginDetails();

            if (!Page.IsPostBack)
            {
                MainCategoryDataCombo();
                BulkProductDropDownListCombo();
            }
        }
        public void GetLoginDetails()
        {

            UserId = Convert.ToInt32(Session["UserId"]);

        }
        protected void MainCategoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MainCategoryDropdown.SelectedIndex == 0)
            {
                BulkProductDropDownList.DataSource = cls.Get_RM_MasterDataAll(UserId);
                BulkProductDropDownList.DataTextField = "RM_Name";
                BulkProductDropDownList.DataValueField = "RM_Id";

                BulkProductDropDownList.DataBind();
                BulkProductDropDownList.Items.Insert(0, "Select");

                //Grid_BR_BOM_MasterDataList.DataSource = cls.Get_BP_MasterData(UserId);
                //Grid_BR_BOM_MasterDataList.DataBind();
            }
            else
            {
                int MainCategory_Id = Convert.ToInt32(MainCategoryDropdown.SelectedValue);

                BulkProductDropDownList.DataSource = cls.Get_RM_MasterByCategoryId(UserId, MainCategory_Id);
                BulkProductDropDownList.DataTextField = "RM_Name";
                BulkProductDropDownList.DataValueField = "RM_Id";
                BulkProductDropDownList.DataBind();
                BulkProductDropDownList.Items.Insert(0, "Select");

                //Grid_BR_BOM_MasterDataList.DataSource = cls.Get_BP_MasterDataById(UserId, MainCategory_Id);
                //Grid_BR_BOM_MasterDataList.DataBind();

            }
        }

        public void MainCategoryDataCombo()
        {
            ClsMainCategoryMaster cls = new ClsMainCategoryMaster();
            MainCategoryDropdown.DataSource = cls.GetMainCategoryData();

            MainCategoryDropdown.DataTextField = "MainCategory_Name";
            MainCategoryDropdown.DataValueField = "PkMainCategory_Id";
            MainCategoryDropdown.Items.Insert(0, "Select");
            MainCategoryDropdown.DataBind();

        }
        public void BulkProductDropDownListCombo()
        {
            BulkProductDropDownList.DataSource = cls.Get_RM_MasterDataAll(UserId);
            BulkProductDropDownList.DataTextField = "RM_Name";
            BulkProductDropDownList.DataValueField = "RM_Id";
            BulkProductDropDownList.DataBind();
            BulkProductDropDownList.Items.Insert(0, "Select");
        }
    }
}