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
    public partial class comp_TradeNameMaster : System.Web.UI.Page
    {
        int User_Id;
        string CompanyName;
        int Company_Id;
        ClsCompanyMaster cls = new ClsCompanyMaster();
        Comp_ProTradeNameMaster comp_pro = new Comp_ProTradeNameMaster();

        protected void Page_Load(object sender, EventArgs e)
        {
            GetLoginDetails();


            if (!IsPostBack)
            {

                GridComp_TradeNameMasterList();
                CompanyMasterDropDownListCombo();

            }
        }
        public void GetLoginDetails()
        {

            User_Id = Convert.ToInt32(Session["UserId"]);
            CompanyName = Session["CompanyMasterList_Name"].ToString();
            Company_Id = Convert.ToInt32(Session["CompanyMaster_Id"]);

        }
        public void CompanyMasterDropDownListCombo()
        {
            ClsCompanyMaster cls = new ClsCompanyMaster();

            DataTable dt = new DataTable();
            //pro.User_Id = User_Id;
            dt = cls.Get_CompanyMasterData();

            ComapnyMasterDropdownList.DataSource = dt;
            ComapnyMasterDropdownList.DataTextField = "CompanyMaster_Name";

            ComapnyMasterDropdownList.DataValueField = "CompanyMaster_Id";
            ComapnyMasterDropdownList.DataBind();
            //ComapnyMasterDropdownList.Items.Insert(0, "Select");
            ComapnyMasterDropdownList.SelectedValue = Company_Id.ToString();
            ComapnyMasterDropdownList.Enabled = false;
        }


        public void ClearData()
        {
            //ComapnyMasterDropdownList.SelectedIndex = 0;
            TradeNametxt.Text = "";
        }
        public void GridComp_TradeNameMasterList()
        {
            Cls_comp_TradeNameMaster cls = new Cls_comp_TradeNameMaster();
            Comp_ProTradeNameMaster comp_pro = new Comp_ProTradeNameMaster();
            comp_pro.comp_CompanyMaster_Id = Company_Id;
            Grid_comp_TradeNameMasterList.DataSource = cls.Get_Comp_TradeNameMasterData(comp_pro);
            Grid_comp_TradeNameMasterList.DataBind();
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            //ComapnyMasterDropdownList.SelectedIndex = 0;
            TradeNametxt.Text = "";
            lblcomp_TradeName_Id.Text = "";
            UpdateTradeNameMaster.Visible = false;

            AddTradeNameMaster.Visible = true;
        }

        protected void GridDelTradeNameMasterBtn_Click(object sender, EventArgs e)
        {
            Cls_comp_TradeNameMaster cls = new Cls_comp_TradeNameMaster();

            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int comp_TradeName_Id = Convert.ToInt32(Grid_comp_TradeNameMasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            comp_pro.comp_TradeNameMaster_Id = comp_TradeName_Id;
            comp_pro.comp_CompanyMaster_Id = Company_Id;
            int status = cls.Delete_Comp_TradeNameMasterData(comp_pro);
            if (status > 0)
            {
                GridComp_TradeNameMasterList();
                ClearData();
            }
        }

        protected void GridEditTradeNameMasterBtn_Click(object sender, EventArgs e)
        {
            Cls_comp_TradeNameMaster cls = new Cls_comp_TradeNameMaster();

            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int comp_TradeName_Id = Convert.ToInt32(Grid_comp_TradeNameMasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            lblcomp_TradeName_Id.Text = comp_TradeName_Id.ToString();
            DataTable dt = new DataTable();
            comp_pro.comp_TradeNameMaster_Id = comp_TradeName_Id;
            dt = cls.Get_Comp_TradeNameMasterDataBy_Id(comp_pro);



            ComapnyMasterDropdownList.SelectedValue = dt.Rows[0]["Fk_CompanyMaster_Id"].ToString();
            TradeNametxt.Text = dt.Rows[0]["comp_TradeName"].ToString();
            UpdateTradeNameMaster.Visible = true;

            AddTradeNameMaster.Visible = false;
        }



        protected void AddTradeNameMaster_Click(object sender, EventArgs e)
        {
            Cls_comp_TradeNameMaster cls = new Cls_comp_TradeNameMaster();
            comp_pro.comp_CompanyMaster_Id = Convert.ToInt32(ComapnyMasterDropdownList.SelectedValue);
            comp_pro.comp_TradeNameMaster = TradeNametxt.Text;

            int status = cls.Insert_Comp_TradeNameMasterData(comp_pro);

            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

                GridComp_TradeNameMasterList();
                ClearData();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed')", true);

            }
        }

        protected void UpdateTradeNameMaster_Click(object sender, EventArgs e)
        {
            Cls_comp_TradeNameMaster cls = new Cls_comp_TradeNameMaster();
            comp_pro.comp_TradeNameMaster_Id = Convert.ToInt32(lblcomp_TradeName_Id.Text);
            comp_pro.comp_CompanyMaster_Id = Convert.ToInt32(ComapnyMasterDropdownList.SelectedValue);
            comp_pro.comp_TradeNameMaster = TradeNametxt.Text;

            int status = cls.Update_Comp_TradeNameMasterData(comp_pro);

            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Successfully')", true);

                GridComp_TradeNameMasterList();
                ClearData();
                UpdateTradeNameMaster.Visible = false;

                AddTradeNameMaster.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Failed')", true);

            }
        }
    }
}