using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using BusinessAccessLayer;
using DataAccessLayer;
using Button = System.Web.UI.WebControls.Button;

namespace Production_Costing_Software
{
    public partial class MasterPacking : System.Web.UI.Page
    {
        int User_Id;
        int status;
        ProPackingMaster pro = new ProPackingMaster();
        protected void Page_Load(object sender, EventArgs e)
        {
            GetLoginDetails();
            if (!IsPostBack)
            {
                BulkProductDropDownListCombo();
                PackingSizeDropDown.Items.Insert(0, "Select");
                Grid_MasterPackingData();
            }
        }
        public void GetLoginDetails()
        {

            User_Id = Convert.ToInt32(Session["UserId"]);

        }
        public void BulkProductDropDownListCombo()
        {
            ClsPackingMaster cls = new ClsPackingMaster();
            DataTable dt = new DataTable();

            dt = cls.Get_BulkProductMasterFromSubPackingMaterial();
            //dt.Columns.Add("BPMValue", typeof(string), "Fk_BPM_Id + ' (' + Fk_PM_RM_Category_Id + ')'+'['+Packing_Size+'-'+Measurement+ ']'").ToString();
            //lblPackSize.Text = dt.Rows[0]["Packing_Size"].ToString();
            //lblPackMeasurement.Text = dt.Rows[0]["Fk_UnitMeasurement_Id"].ToString();

            DataView dvOptions = new DataView(dt);
            dvOptions.Sort = "BPM_Product_Name";
            BulkproductDropdownlist.DataSource = dt;
            BulkproductDropdownlist.DataTextField = "BPM_Product_Name";

            BulkproductDropdownlist.DataValueField = "Fk_BPM_Id";
            BulkproductDropdownlist.DataBind();
            BulkproductDropdownlist.Items.Insert(0, "Select");
            PackingSizeDropDown.Enabled = false;
        }
        protected void BulkproductDropdownlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BulkproductDropdownlist.SelectedValue != "Select")
            {
                PackingSizeDropDown.Enabled = true;

                ClsPackingMaster cls = new ClsPackingMaster();
                int BPM_Id = Convert.ToInt32(BulkproductDropdownlist.SelectedValue);
                DataTable dt = new DataTable();
                dt = cls.Get_PacksizeByBPM_Id(BPM_Id);

                dt.Columns.Add("PackValue", typeof(string), "Pack_size + ' (' + Pack_Measurement + ')'").ToString();

                PackingSizeDropDown.DataSource = dt;
                PackingSizeDropDown.DataTextField = "Measurement";
                PackingSizeDropDown.DataValueField = "PackValue";
                PackingSizeDropDown.DataBind();
                PackingSizeDropDown.Items.Insert(0, "Select");

            }
            if (BulkproductDropdownlist.SelectedValue == "Select")
            {
                PackingSizeDropDown.Enabled = false;
                PackingSizeDropDown.SelectedIndex = 0;
            }
        }

        protected void PackingMaster_AddBtn_Click(object sender, EventArgs e)
        {
            ClsPackingMaster cls = new ClsPackingMaster();

            DataTable dt = new DataTable();
            dt = cls.Get_MasterPackingAll();

            //lblBPM_Id.Text = dt.Rows[0]["Fk_BPM_Id"].ToString();

            foreach (DataRow row in dt.Rows)
            {
                if (BulkproductDropdownlist.SelectedValue == row["Fk_BPM_Id"].ToString())
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bulk Product Can not Add Multiple Time!')", true);
                    lblMasterPacking_Id.Text = row["MasterPacking_Id"].ToString();
                    //return;
                    if (DialogResult.Yes == MessageBox.Show("Do You Want Change ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        // do what u want
                        pro.PackSize = decimal.Parse(lblPackSize.Text);
                        pro.PackMeasurement = Convert.ToInt32(lblPackMeasurement.Text);
                        pro.BPM_Id = Convert.ToInt32(BulkproductDropdownlist.SelectedValue);
                        pro.ChkIsPAckingMaster = Convert.ToBoolean(ChkIsPckingMaster.Checked);
                        pro.PackingMaster_Id = Convert.ToInt32(lblMasterPacking_Id.Text);
                        status = cls.Update_MasterPacking(pro);

                        if (status > 0)
                        {


                            ClearData();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
                            Grid_MasterPackingData();
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Not Updated')", true);
                        ClearData();
                        return;
                       
                    }
                }


            }

            pro.PackSize = decimal.Parse(lblPackSize.Text);
            pro.PackMeasurement = Convert.ToInt32(lblPackMeasurement.Text);
            pro.BPM_Id = Convert.ToInt32(BulkproductDropdownlist.SelectedValue);
            pro.ChkIsPAckingMaster = Convert.ToBoolean(ChkIsPckingMaster.Checked);

            status = cls.Insert_MasterPacking(pro);

            if (status > 0)
            {


                ClearData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                Grid_MasterPackingData();

            }

            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Fail')", true);

            }

        }
        public void ClearData()
        {
            BulkproductDropdownlist.SelectedIndex = 0;
            PackingSizeDropDown.SelectedIndex = 0;
            ChkIsPckingMaster.Checked = false;
            PackingSizeDropDown.Enabled = false;
        }
        public void Grid_MasterPackingData()
        {
            ClsPackingMaster cls = new ClsPackingMaster();
            Grid_MasterPacking.DataSource = cls.Get_MasterPackingAll();
            Grid_MasterPacking.DataBind();
        }
        protected void PackingMaster_UpdateBtn_Click(object sender, EventArgs e)
        {
            ClsPackingMaster cls = new ClsPackingMaster();

            pro.PackSize = decimal.Parse(lblPackSize.Text);
            pro.PackMeasurement = Convert.ToInt32(lblPackMeasurement.Text);
            pro.BPM_Id = Convert.ToInt32(BulkproductDropdownlist.SelectedValue);
            pro.ChkIsPAckingMaster = Convert.ToBoolean(ChkIsPckingMaster.Checked);

            int status = cls.Update_MasterPacking(pro);

            if (status > 0)
            {


                ClearData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                Grid_MasterPackingData();
                ClearData();
            }

            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Fail')", true);

            }
        }

        protected void PackingLossCancelBtn_Click(object sender, EventArgs e)
        {
            BulkproductDropdownlist.SelectedIndex = 0;
            PackingSizeDropDown.SelectedIndex = 0;
            ChkIsPckingMaster.Checked = false;
        }

        protected void PackingSizeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string PackSizeAll = PackingSizeDropDown.SelectedValue.ToString();
            string PackMeasurement = PackSizeAll.Split('(', ')')[1];
            String PackSize = Regex.Match(PackSizeAll, @"\d+").Value;
            lblPackSize.Text = PackSize;
            lblPackMeasurement.Text = PackMeasurement;

        }

        protected void EditMasterPacking_Click(object sender, EventArgs e)
        {

        }

        protected void DelMasterPacking_Click(object sender, EventArgs e)
        {
            Button Edit = sender as Button;
            GridViewRow gdrow = Edit.NamingContainer as GridViewRow;
            int MasterPacking_Id = Convert.ToInt32(Grid_MasterPacking.DataKeys[gdrow.RowIndex].Value.ToString());
            lblMasterPacking_Id.Text = MasterPacking_Id.ToString();
            DataTable dt = new DataTable();
            ClsPackingMaster cls = new ClsPackingMaster();
            pro.PackingMaster_Id = MasterPacking_Id;

            int status = cls.Delete_MasterPacking(pro);
            if (status > 0)
            {

                ClearData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);
                Grid_MasterPackingData();
                ClearData();
            }

            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Fail')", true);

            }
        }
    }
}