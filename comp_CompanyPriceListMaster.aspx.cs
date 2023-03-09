using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using BusinessAccessLayer;
using System.Data;
using System.Text.RegularExpressions;

namespace Production_Costing_Software
{
    public partial class comp_CompanyPriceListMaster : System.Web.UI.Page
    {
        int User_Id;
        string CompanyName;
        int Company_Id;
        Comp_ProCompanyPriceListMaster pro = new Comp_ProCompanyPriceListMaster();
        protected void Page_Load(object sender, EventArgs e)
        {
            GetLoginDetails();


            if (!IsPostBack)
            {

                Grid_CompanyPriceListData();
                BulkProductDropDownListCombo();
            }
        }
        public void GetLoginDetails()
        {
            if (Session["CompanyMasterList_Name"]!=null)
            {
                User_Id = Convert.ToInt32(Session["UserId"]);
                CompanyName = Session["CompanyMasterList_Name"].ToString();
                Company_Id = Convert.ToInt32(Session["CompanyMaster_Id"]);
            }
            else
            {
                Response.Redirect("~/login.aspx");
            }

        }
        public void Grid_CompanyPriceListData()
        {
            Cls_comp_CompanyPriceListMaster cls = new Cls_comp_CompanyPriceListMaster();
            Comp_ProCompanyPriceListMaster pro = new Comp_ProCompanyPriceListMaster();
            pro.Fk_CompanyList_Id = Company_Id;
            Grid_CompanyPriceList.DataSource = cls.Get_CompanyPriceListMaster(pro);
            Grid_CompanyPriceList.DataBind();
        }
        public void BulkProductDropDownListCombo()
        {
            ClsProductwiseLabourCost cls = new ClsProductwiseLabourCost();

            DataTable dt = new DataTable();
            //pro.User_Id = User_Id;
            dt = cls.GetBulkProduct_From_ProductwiseLabourCost();

            BulkproductDropDown.DataSource = dt;
            BulkproductDropDown.DataTextField = "BPM_Product_Name";


            BulkproductDropDown.DataValueField = "Productwise_Labor_cost_Id";
            BulkproductDropDown.DataBind();
            BulkproductDropDown.Items.Insert(0, "Select");
        }

        protected void PriceListAddBtn_Click(object sender, EventArgs e)
        {
            ClsProductwiseLabourCost clsPWLC = new ClsProductwiseLabourCost();
            Comp_ProCompanyPriceListMaster pro = new Comp_ProCompanyPriceListMaster();
            pro.Fk_PWLC_Id = Convert.ToInt32(BulkproductDropDown.SelectedValue);
            int PWLC_Id = Convert.ToInt32(BulkproductDropDown.SelectedValue);
            DataTable dt = new DataTable();
            dt = clsPWLC.Get_ProductwiseLabourCosById(User_Id, PWLC_Id);

           
            pro.PackingSize =Convert.ToDecimal(dt.Rows[0]["Packing_Size"]);
            pro.Fk_UnitMeasurement = Convert.ToInt32(dt.Rows[0]["Fk_UnitMeasurement_Id"]);

            pro.Cost_Information = Convert.ToDecimal(CostInformationtxt.Text);
            pro.Addition_Cost = Convert.ToDecimal(AdditionalCosttxt.Text);
            pro.FectoryExpencePer = Convert.ToDecimal(FactoryExpencePertxt.Text);
            pro.MarketedByChargesPer = Convert.ToDecimal(MarketByChargestxt.Text);
            pro.OtherPer = Convert.ToDecimal(OtherPertxt.Text);
            pro.ProfitPer = Convert.ToDecimal(ProfitPertxt.Text);
            pro.Total_Unit_Amount = Convert.ToDecimal(TotalAmountPerUnitttxt.Text);
            pro.Total_Liter_Amount = Convert.ToDecimal(TotalAmountPerLiterttxt.Text);
            pro.Fk_CompanyList_Id = Company_Id;
            
            Cls_comp_CompanyPriceListMaster cls = new Cls_comp_CompanyPriceListMaster();

            int status = cls.Insert_Comp_Insert_CompanyPriceListMaster(pro);

            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

                Grid_CompanyPriceListData();
                ClearData();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed')", true);

            }
        }
        public void ClearData()
        {
            BulkproductDropDown.SelectedIndex = 0;

            //lblcomp_ProductMaster_Id.Text = "";

        }
        protected void PriceListUpdateBtn_Click(object sender, EventArgs e)
        {

        }

        protected void PriceListCancelBtn_Click(object sender, EventArgs e)
        {

        }

        protected void GridEditCompanyPriceListBtn_Click(object sender, EventArgs e)
        {

        }

        protected void GridDelCompanyPriceListBtn_Click(object sender, EventArgs e)
        {

        }

        protected void ChkIsPackingMaster_CheckedChanged(object sender, EventArgs e)
        {
            Comp_ProCompanyPriceListMaster pro = new Comp_ProCompanyPriceListMaster();

            if (ChkIsPackingMaster.Checked == true)
            {
                pro.IsMasterPacking = 1;
            }
            else
            {
                pro.IsMasterPacking = 0;
            }
        }
    }
}