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
    public partial class comp_ProductCategoryMaster : System.Web.UI.Page
    {
        int User_Id;
        string CompanyName;
        int Company_Id;
        Comp_ProProductCategoryMaster pro = new Comp_ProProductCategoryMaster();
        Cls_comp_ProductCategoryMaster cls = new Cls_comp_ProductCategoryMaster();
        protected void Page_Load(object sender, EventArgs e)
        {
            GetLoginDetails();


            if (!IsPostBack)
            {

                Grid_comp_ProductCategoryMaster();
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
        protected void AddProductCategoryMasterBtn_Click(object sender, EventArgs e)
        {
            pro.comp_CompanyMaster_Id = Convert.ToInt32(ComapnyMasterDropdownList.SelectedValue);
            pro.comp_ProductCategoryName = ProductCategorytxt.Text;

            int status = cls.Insert_Comp_ProductCategoryMasterData(pro);

            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

                Grid_comp_ProductCategoryMaster();
                ClearData();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed')", true);

            }
        }
        public void Grid_comp_ProductCategoryMaster()
        {
            Cls_comp_ProductCategoryMaster cls = new Cls_comp_ProductCategoryMaster();
            Comp_ProProductCategoryMaster pro = new Comp_ProProductCategoryMaster();

            pro.comp_CompanyMaster_Id = Company_Id;
            Grid_comp_ProductCategoryMasterList.DataSource = cls.Get_Comp_ProductCategoryMasterData(pro);
            Grid_comp_ProductCategoryMasterList.DataBind();
        }
        public void ClearData()
        {
            //ComapnyMasterDropdownList.SelectedIndex = 0;
            ProductCategorytxt.Text = "";
            lblProductCategoryMaster_Id.Text = "";
        }
        protected void GridEditProductCategoryMasterBtn_Click(object sender, EventArgs e)
        {
            Cls_comp_ProductCategoryMaster cls = new Cls_comp_ProductCategoryMaster();

            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int ProductCategoryMaster_Id = Convert.ToInt32(Grid_comp_ProductCategoryMasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            lblProductCategoryMaster_Id.Text = ProductCategoryMaster_Id.ToString();
            DataTable dt = new DataTable();
            pro.comp_ProductCategoryMaster_Id = ProductCategoryMaster_Id;
            dt = cls.Get_Comp_ProductCategoryMasterDataBy_Id(pro);



            ComapnyMasterDropdownList.SelectedValue = dt.Rows[0]["Fk_CompanyMaster_Id"].ToString();
            ProductCategorytxt.Text = dt.Rows[0]["ProductCategoryName"].ToString();
            UpdateProductCategoryMasterBtn.Visible = true;

            AddProductCategoryMasterBtn.Visible = false;
        }

        protected void UpdateProductCategoryMasterBtn_Click(object sender, EventArgs e)
        {
            pro.comp_CompanyMaster_Id = Convert.ToInt32(ComapnyMasterDropdownList.SelectedValue);
            pro.comp_ProductCategoryName = ProductCategorytxt.Text;
            pro.comp_ProductCategoryMaster_Id = Convert.ToInt32(lblProductCategoryMaster_Id.Text);
            int status = cls.Update_Comp_ProductCategoryMasterData(pro);

            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);

                Grid_comp_ProductCategoryMaster();
                ClearData();
                UpdateProductCategoryMasterBtn.Visible = false;

                AddProductCategoryMasterBtn.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Update Failed')", true);

            }
        }

        protected void CancelProductCategoryMasterBtn_Click(object sender, EventArgs e)
        {
            //ComapnyMasterDropdownList.SelectedIndex = 0;
            ProductCategorytxt.Text = "";
            lblProductCategoryMaster_Id.Text = "";
            UpdateProductCategoryMasterBtn.Visible = false;

            AddProductCategoryMasterBtn.Visible = true;
        }

        protected void GridDelProductCategoryMasterBtn_Click(object sender, EventArgs e)
        {
            Cls_comp_ProductCategoryMaster cls = new Cls_comp_ProductCategoryMaster();
            Button DeleBtn = sender as Button;
            GridViewRow gdrow = DeleBtn.NamingContainer as GridViewRow;
            int ProductCategoryMaster_Id = Convert.ToInt32(Grid_comp_ProductCategoryMasterList.DataKeys[gdrow.RowIndex].Value.ToString());
            pro.comp_ProductCategoryMaster_Id = ProductCategoryMaster_Id;
            int status = cls.Delete_Comp_ProductCategoryMasterData(pro);
            if (status > 0)
            {
                Grid_comp_ProductCategoryMaster();
                ClearData();
            }
        }
    }
}