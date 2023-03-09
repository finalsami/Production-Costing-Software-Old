using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;
namespace Production_Costing_Software
{
    public partial class PackingLossMaster : System.Web.UI.Page
    {
        int UserId;
        ProPackingLossMaster pro = new ProPackingLossMaster();
        ClsPackingLossMaster cls = new ClsPackingLossMaster();
        protected void Page_Load(object sender, EventArgs e)
        {
            GetLoginDetails();
            if (!IsPostBack)
            {
                Grid_PackingLossMasterData();
                PackingSizeDropDownListCombo();
                ClearData();
            }

        }
        public void GetLoginDetails()
        {

            UserId = Convert.ToInt32(Session["UserId"]);

        }
        public void Grid_PackingLossMasterData()
        {
            Grid_PackingLossMaster.DataSource = cls.Get_PackingLossMaster();
            Grid_PackingLossMaster.DataBind();
        }
        public void ClearData()
        {
            //BulkProductNameDropDown.SelectedIndex = 0;
            //BulkCostLtrtxt.Text = "0";
            //InterestAmttxt.Text = "0";
            //InterestPertxt.Text = "0";
            //TotalAmounttxt.Text = "0";
            //BPM_Interest_UpdateBtn.Visible = false;
            //BPM_Interest_AddBtn.Visible = true;
        }
        public void PackingSizeDropDownListCombo()
        {
            ClsPackingMateriaMaster cls = new ClsPackingMateriaMaster();
            DataTable dt = new DataTable();
            dt = cls.Get_PackSizeFromSubpackingMaterialMaster();


            DataView dvOptions = new DataView(dt);
            dvOptions.Sort = "Measurement";

            PackingSizeDropDown.DataSource = dvOptions;

            PackingSizeDropDown.DataTextField = "Measurement";
            PackingSizeDropDown.DataValueField = "Pack_Measurement";
            PackingSizeDropDown.DataBind();
            PackingSizeDropDown.Items.Insert(0, "Select");
        }

        protected void PackingSizeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
          

        }

        protected void PackingLoss_AddBtn_Click(object sender, EventArgs e)
        {
            pro.PackMeasurement = Convert.ToInt32(PackingSizeDropDown.SelectedValue);

            pro.PackSize = decimal.Parse(PackingSizetxt.Text);
            pro.PackingLossPercent = decimal.Parse(PackingLosstxt.Text);

            int status = cls.Insert_PackingLossMaster(pro);

            if (status > 0)
            {
                Grid_PackingLossMasterData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Packing Loss Inserted Successfully')", true);
                ClearData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Failed!')", true);

            }
        }

        protected void PackingLoss_UpdateBtn_Click(object sender, EventArgs e)
        {

        }

        protected void EditPackLossBtn_Click(object sender, EventArgs e)
        {

        }

        protected void DelPackLossBtn_Click(object sender, EventArgs e)
        {

        }
    }
}